using System;
using System.Collections.Generic;
using System.Text;
using MyUtility;
using System.Data;
using System.IO;
using MyFamousMan.Service;
using MyFamousMan.Report;
using MyFamousMan.Sub;
using System.Web;
namespace MyEmailReport.Report
{
    public class EmailReport
    {
        /// <summary>
        /// Key của chuỗi kết nối trong config
        /// </summary>
        public string ConnectionKey = "SQLConnecton_FamousMan";

        public string EmailAccount = string.Empty;
        public string Password = string.Empty;
        public string EmailReceiveList = string.Empty;
        public string Subject = string.Empty;

        public string Format = string.Empty;
        public string Format_TR_Repeat = string.Empty;

        
        public void Init()
        {
            try
            {
                EmailAccount = MyConfig.GetKeyInConfigFile("EmailReport_EmailAccount");
                Password = MyConfig.GetKeyInConfigFile("EmailReport_Password");
                EmailReceiveList = MyConfig.GetKeyInConfigFile("EmailReport_EmailReceiveList");
                Subject = MyConfig.GetKeyInConfigFile("EmailReport_Subject");

                Format = MyFile.ReadFile(MyFile.GetFullPathFile("\\Templates\\ReportByDay.htm"));
                Format_TR_Repeat = MyFile.ReadFile(MyFile.GetFullPathFile("\\Templates\\ReportByDay_TR_Repeat.htm"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        public string BuildEmail()
        {
            try
            {
                DateTime BeginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                EndDate = EndDate.AddDays(1);

                RP_Sub mRP_Sub = new RP_Sub(ConnectionKey);

                StringBuilder mBuild_TR = new StringBuilder(string.Empty);

                DataTable mFullData = mRP_Sub.Select(3, BeginDate.ToString(MyConfig.DateFormat_InsertToDB), EndDate.ToString(MyConfig.DateFormat_InsertToDB));

                if (mFullData == null || mFullData.Rows.Count < 1)
                {
                    return string.Empty;
                }                
                double TotalSub_Month = 0;
                double TotalUnSub_Month = 0;
                double TotalRenewSuccess_Month = 0;
                double TotalSale_Month = 0;

                foreach (DataRowView mRow in mFullData.DefaultView)
                {
                   
                    StringBuilder mBuild_TD = new StringBuilder(string.Empty);

                    double UseRate = 0;
                    if ((double)mRow["SubTotal"] > 0)
                        UseRate = ((double)mRow["UseYes"] / (double)mRow["SubTotal"])*100;

                    mBuild_TR.Append(string.Format(Format_TR_Repeat, 
                                                    new string[] {  ((DateTime)mRow["ReportDay"]).ToString(MyConfig.ShortDateFormat),
                                                                    ((double)mRow["SubTotal"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["SubSuccess"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["SubSMS"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["SubWAP"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["SubWEB"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["SubOther"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["UnsubTotal"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["UnsubSuccess"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["UnSubSMS"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["UnSubCSKH"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["UnsubExtend"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["UnsubOther"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["RenewTotal"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["RenewSuccess"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["RenewFail"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["RenewRate"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["UseNot"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["UseYes"]).ToString(MyConfig.IntFormat),
                                                                    UseRate.ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["SaleReg"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["SaleRenew"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["SaleContent"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["SaleTotal"]).ToString(MyConfig.IntFormat),
                                                                    ((double)mRow["RateSaleDay"]).ToString(MyConfig.IntFormat)
                                                                    
                                                                }));
                    TotalSub_Month += (double)mRow["SubSuccess"];
                    TotalUnSub_Month += (double)mRow["UnsubSuccess"];
                    TotalRenewSuccess_Month += (double)mRow["RenewSuccess"];
                    TotalSale_Month += (double)mRow["SaleTotal"];
                }
                //string Time = ReportDay.ToString(MyConfig.ShortDateFormat) + " LÚC " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
                string Time = DateTime.Now.ToString("MM/yyyy");
                return string.Format(Format, Time, mBuild_TR.ToString(),    TotalSub_Month.ToString(MyConfig.IntFormat),
                                                                            TotalUnSub_Month.ToString(MyConfig.IntFormat),
                                                                            TotalRenewSuccess_Month.ToString(MyConfig.IntFormat),
                                                                            TotalSale_Month.ToString(MyConfig.IntFormat));
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
                throw ex;
            }
        }

        public void Run()
        {
            try
            {
                Console.WriteLine("Bat dau chay chuong trinh");
                Console.WriteLine("Lay thong tin config, va khoi tao");
                Init();

                string pSubject = Subject + DateTime.Now.ToString("dd-MM-yyyy") + " LÚC " + DateTime.Now.Hour + ":" + DateTime.Now.Minute;

                Console.WriteLine("Build Noi dung Email");
                string Body = BuildEmail();
                Console.WriteLine("Bat dau gui email...");

                bool Result = MySendEmail.SendEmail_Google_New(EmailAccount, Password, EmailReceiveList, pSubject, Body, string.Empty);

                MyLogfile.LogEmail(Subject, Body, EmailAccount, Password);

                if (!Result)
                {
                    Console.WriteLine("---GUI EMAIL KHONG THANH CONG----");
                }

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError("_Error", ex, false);
                Console.WriteLine("---CO LOI XAY RA----");
            }
        }
    }
}
