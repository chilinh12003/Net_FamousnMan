using System;
using System.Collections.Generic;
using System.Text;
using MyUtility;
using System.Data;
using System.IO;
using MyFamousMan.Service;
using MyFamousMan.Report;
using MyFamousMan.Sub;
namespace MyTool.ReportSync
{
    public class ReportSync
    {
        /// <summary>
        /// Cho biết dừng thread hay không
        /// </summary>
        public static bool StopThread = false;

        /// <summary>
        /// Key của chuỗi kết nối trong config
        /// </summary>
        public string ConnectionKey_Source = "ConnectionKey_Source";
        public string ConnectionKey_Des = "ConnectionKey_Des";
        
        public int MaxPID = 50;
    
        /// <summary>
        /// Thời gian delay cho mỗi một lần chạy
        /// </summary>
        public static int SleepSecond
        {
            get
            {
                try
                {
                    int Temp = 1;
                    int.TryParse(MyConfig.GetKeyInConfigFile("SleepSecond"), out Temp);
                    return Temp;
                }
                catch
                {
                    return 1;
                }
            }
        }

        ChargeLog mChargeLog = null;

        RP_Sub mRP_Sub = null;

        Subscriber mSub = null;
        UnSubscriber mUnSub = null;

        public ReportSync()
        {
            try
            {
                mChargeLog = new ChargeLog(ConnectionKey_Source);
                mSub = new Subscriber(ConnectionKey_Source);
                mUnSub = new UnSubscriber(ConnectionKey_Source);

                mRP_Sub = new RP_Sub(ConnectionKey_Des);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
            
        /// <summary>
        /// Lấy dữ liệu trong Table RP_Sub theo ngày hiện tại
        /// </summary>
        /// <param name="ReportDay"></param>
        /// <returns></returns>
        RP_Sub Get_RPSub_ByDate(DateTime ReportDay)
        {
            try
            {
                RP_Sub mObj = new RP_Sub(this.ConnectionKey_Des);

                ReportDay = new DateTime(ReportDay.Year, ReportDay.Month, ReportDay.Day, 0, 0, 0);
                DataTable mTable = mRP_Sub.Select(1, ReportDay.ToString(MyConfig.DateFormat_InsertToDB));
                mObj.ReportDay = ReportDay;

                if (mTable == null || mTable.Rows.Count < 1)
                    return mObj;

                mObj = mObj.Convert(mTable);
                return mObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        DateTime BeginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

        DateTime StartDate
        {
            get
            {
                if (!string.IsNullOrEmpty(MyConfig.GetKeyInConfigFile("StartDate")))
                {

                    return DateTime.ParseExact(MyConfig.GetKeyInConfigFile("StartDate"), "yyyy-MM-dd", null);
                }
                return DateTime.MinValue;
            }
        }

        DateTime EndDate
        {
            get
            {
                return BeginDate.AddDays(1);
            }
        }

        /// <summary>
        /// ngày đầu tiên của tháng
        /// </summary>
        DateTime FirstDateOfMonth
        {
            get
            {
                return new DateTime(BeginDate.Year, BeginDate.Month, 1, 0, 0, 0);
            }
        }

        public void Run()
        {
            try
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("BAT DAU CHAY CHUONG TRINH");

                if (StartDate != DateTime.MinValue)
                {
                    BeginDate = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, 0, 0, 0);
                }

                DataTable mTable_RP_Sub = mRP_Sub.Select(2, (new DateTime(BeginDate.Year, BeginDate.Month, 1)).ToString(MyConfig.DateFormat_InsertToDB),
                                                            (new DateTime(BeginDate.Year, BeginDate.Month, DateTime.DaysInMonth(BeginDate.Year, BeginDate.Month))).ToString(MyConfig.DateFormat_InsertToDB)
                                                            );
                //Ngày hiện tại
                RP_Sub mRP_Sub_Current = new RP_Sub(this.ConnectionKey_Des);
                while (!StopThread)
                {
                    try
                    {
                        if (BeginDate > DateTime.Now)
                        {
                            BeginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                        }

                        //Ngày hôm qua
                        RP_Sub mRP_Sub_Previous = Get_RPSub_ByDate(BeginDate.AddDays(-1));

                        mRP_Sub_Current.Clear();
                        mRP_Sub_Current.ReportDay = BeginDate;

                        //Lấy thông tin tổng quan
                        mRP_Sub_Current.SubTotal = double.Parse(mSub.Select(7, string.Empty).Rows[0][0].ToString());

                        for (int PID = 0; PID <= MaxPID; PID++)
                        {
                            if (StopThread)
                                break;

                            Console.WriteLine("BeginDate:"+BeginDate.ToString(MyConfig.LongDateFormat)+" || Lay du lieu voi pid = " + PID.ToString());
              
                            #region Thông tin Đăng ký

                            //Tổng đăng ký (Thành công + không thành công
                            mRP_Sub_Current.SubNew += double.Parse(mChargeLog.Select(10, PID.ToString(), ((int)ChargeLog.ChargeType.REG).ToString(),
                                                                         BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                         );
                            mRP_Sub_Current.SubSuccess += double.Parse(mChargeLog.Select(11, PID.ToString(), ((int)ChargeLog.ChargeType.REG).ToString(),
                                                                         ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                         BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                         );
                            //Đăng ký mới thành công qua kênh SMS
                            mRP_Sub_Current.SubSMS += double.Parse(mChargeLog.Select(9, PID.ToString(), ((int)ChargeLog.ChargeType.REG).ToString(),
                                                                            ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                            ((int)MyConfig.ChannelType.SMS).ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                            );
                            mRP_Sub_Current.SubIVR += double.Parse(mChargeLog.Select(9, PID.ToString(), ((int)ChargeLog.ChargeType.REG).ToString(),
                                                                           ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                           ((int)MyConfig.ChannelType.IVR).ToString(),
                                                                           BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                           );
                            mRP_Sub_Current.SubWEB += double.Parse(mChargeLog.Select(9, PID.ToString(), ((int)ChargeLog.ChargeType.REG).ToString(),
                                                                          ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                          ((int)MyConfig.ChannelType.WEB).ToString(),
                                                                          BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                          );
                            mRP_Sub_Current.SubWAP += double.Parse(mChargeLog.Select(9, PID.ToString(), ((int)ChargeLog.ChargeType.REG).ToString(),
                                                                          ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                          ((int)MyConfig.ChannelType.WAP).ToString(),
                                                                          BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                          );
                            mRP_Sub_Current.SubUSSD += double.Parse(mChargeLog.Select(9, PID.ToString(), ((int)ChargeLog.ChargeType.REG).ToString(),
                                                                          ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                          ((int)MyConfig.ChannelType.USSD).ToString(),
                                                                          BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                          );
                            mRP_Sub_Current.SubAPP += double.Parse(mChargeLog.Select(9, PID.ToString(), ((int)ChargeLog.ChargeType.REG).ToString(),
                                                                          ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                          ((int)MyConfig.ChannelType.CLIENT).ToString(),
                                                                          BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                          );
                            #endregion

                            #region Thông tin Hủy đăng ký
                            mRP_Sub_Current.UnsubTotal += double.Parse(mChargeLog.Select(11, PID.ToString(), ((int)ChargeLog.ChargeType.UNREG).ToString(),
                                                                                          ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                                          BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                                          );

                            mRP_Sub_Current.UnSubAPP += double.Parse(mChargeLog.Select(9, PID.ToString(), ((int)ChargeLog.ChargeType.UNREG).ToString(),
                                                                         ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                         ((int)MyConfig.ChannelType.CLIENT).ToString(),
                                                                         BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                         );
                            mRP_Sub_Current.UnSubCSKH += double.Parse(mChargeLog.Select(9, PID.ToString(), ((int)ChargeLog.ChargeType.UNREG).ToString(),
                                                                         ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                         ((int)MyConfig.ChannelType.CSKH).ToString(),
                                                                         BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                         );
                            mRP_Sub_Current.UnsubExtend += double.Parse(mChargeLog.Select(9, PID.ToString(), ((int)ChargeLog.ChargeType.UNREG).ToString(),
                                                                         ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                         ((int)MyConfig.ChannelType.MAXRETRY).ToString(),
                                                                         BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                         );
                            mRP_Sub_Current.UnSubIVR += double.Parse(mChargeLog.Select(9, PID.ToString(), ((int)ChargeLog.ChargeType.UNREG).ToString(),
                                                                         ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                         ((int)MyConfig.ChannelType.IVR).ToString(),
                                                                         BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                         );


                            mRP_Sub_Current.UnSubSMS += double.Parse(mChargeLog.Select(9, PID.ToString(), ((int)ChargeLog.ChargeType.UNREG).ToString(),
                                                                         ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                         ((int)MyConfig.ChannelType.SMS).ToString(),
                                                                         BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                         );

                            mRP_Sub_Current.UnSubUSSD += double.Parse(mChargeLog.Select(9, PID.ToString(), ((int)ChargeLog.ChargeType.UNREG).ToString(),
                                                                        ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                        ((int)MyConfig.ChannelType.USSD).ToString(),
                                                                        BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                        );
                            mRP_Sub_Current.UnSubWAP += double.Parse(mChargeLog.Select(9, PID.ToString(), ((int)ChargeLog.ChargeType.UNREG).ToString(),
                                                                        ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                        ((int)MyConfig.ChannelType.WAP).ToString(),
                                                                        BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                        );
                            mRP_Sub_Current.UnSubWEB += double.Parse(mChargeLog.Select(9, PID.ToString(), ((int)ChargeLog.ChargeType.UNREG).ToString(),
                                                                       ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                       ((int)MyConfig.ChannelType.WEB).ToString(),
                                                                       BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                       );

                            mRP_Sub_Current.UnsubSelf = mRP_Sub_Current.UnSubWEB + mRP_Sub_Current.UnSubWAP + mRP_Sub_Current.UnSubSMS;


                            #endregion

                            #region Thông tin Sử dụng dịch vụ

                            mRP_Sub_Current.UseByDay += double.Parse(mChargeLog.Select(13, PID.ToString(), ((int)ChargeLog.ChargeType.CONTENT).ToString(),
                                                                          BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                          );

                            mRP_Sub_Current.UseByMonth += double.Parse(mChargeLog.Select(13, PID.ToString(), ((int)ChargeLog.ChargeType.CONTENT).ToString(),
                                                                          FirstDateOfMonth.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                          );
                          
                            #endregion

                            #region Thông tin gia hạn
                            mRP_Sub_Current.RenewTotal += double.Parse(mChargeLog.Select(13, PID.ToString(), ((int)ChargeLog.ChargeType.RENEW).ToString(),
                                                                                            BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                                            EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                                            );

                            mRP_Sub_Current.RenewSuccess += double.Parse(mChargeLog.Select(11, PID.ToString(), ((int)ChargeLog.ChargeType.RENEW).ToString(),
                                                                        ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                        BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                        EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                        );

                            mRP_Sub_Current.RenewFail = mRP_Sub_Current.RenewTotal - mRP_Sub_Current.RenewSuccess;

                            if (mRP_Sub_Current.RenewTotal > 0)
                                mRP_Sub_Current.RenewRate = (mRP_Sub_Current.RenewSuccess / mRP_Sub_Current.RenewTotal) * 100;
                            else if (mRP_Sub_Current.RenewSuccess > 0)
                                mRP_Sub_Current.RenewRate = 100;
                            #endregion

                            #region Tần suất sử dụng

                            mRP_Sub_Current.UseFew += double.Parse(mChargeLog.Select(12, PID.ToString(), ((int)ChargeLog.ChargeType.CONTENT).ToString(),
                                                                        BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                        EndDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                        "1", "5").Rows[0][0].ToString()
                                                                        );
                            mRP_Sub_Current.UseModerate += double.Parse(mChargeLog.Select(12, PID.ToString(), ((int)ChargeLog.ChargeType.CONTENT).ToString(),
                                                                       BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                       EndDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                       "6", "10").Rows[0][0].ToString()
                                                                       );
                            mRP_Sub_Current.UseMuch += double.Parse(mChargeLog.Select(12, PID.ToString(), ((int)ChargeLog.ChargeType.CONTENT).ToString(),
                                                                      BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                      EndDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                      "11", "15").Rows[0][0].ToString()
                                                                      );
                            mRP_Sub_Current.UserVeryMuch += double.Parse(mChargeLog.Select(12, PID.ToString(), ((int)ChargeLog.ChargeType.CONTENT).ToString(),
                                                                      BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                      EndDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                      "16", "100").Rows[0][0].ToString()
                                                                      );

                            mRP_Sub_Current.UseNot = mRP_Sub_Current.SubTotal - (mRP_Sub_Current.UseFew + mRP_Sub_Current.UseModerate +
                                                        mRP_Sub_Current.UseMuch + mRP_Sub_Current.UserVeryMuch);
                            #endregion

                            #region Doanh thu
                            mRP_Sub_Current.SaleReg += double.Parse(mChargeLog.Select(18, PID.ToString(), ((int)ChargeLog.ChargeType.REG).ToString(),
                                                                                      ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                                      BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                                      EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                                      );
                            mRP_Sub_Current.SaleRenew += double.Parse(mChargeLog.Select(18, PID.ToString(), ((int)ChargeLog.ChargeType.RENEW).ToString(),
                                                                   ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                   BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                   EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                   );

                            mRP_Sub_Current.SaleContent += double.Parse(mChargeLog.Select(18, PID.ToString(), ((int)ChargeLog.ChargeType.CONTENT).ToString(),
                                                                   ((int)ChargeLog.ChargeStatus.ChargeSuccess).ToString(),
                                                                   BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                   EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString()
                                                                   );
                            mRP_Sub_Current.SaleTotal = mRP_Sub_Current.SaleReg + mRP_Sub_Current.SaleRenew + mRP_Sub_Current.SaleContent;

                            if (mRP_Sub_Current.SubTotal > 0)
                                mRP_Sub_Current.RateUseByDay = (mRP_Sub_Current.UseByDay / mRP_Sub_Current.SubTotal) * 100;
                            else if (mRP_Sub_Current.UseByDay > 0)
                                mRP_Sub_Current.RateUseByDay = 100;

                            if (BeginDate.Day == 1 || mRP_Sub_Previous.IsNull)
                            {
                                mRP_Sub_Current.RateUseByMonth = mRP_Sub_Current.RateUseByDay;
                            }
                            else if(mRP_Sub_Previous.RateUseByDay == 0)
                            {
                                  if (StartDate != DateTime.MinValue)
                                {
                                    mRP_Sub_Current.RateUseByMonth = ((mRP_Sub_Previous.RateUseByMonth * (BeginDate.Day - StartDate.Day) + mRP_Sub_Current.RateUseByDay) / ((BeginDate.Day - StartDate.Day) + 1));
                                }
                                else
                                {
                                    mRP_Sub_Current.RateUseByMonth = ((mRP_Sub_Previous.RateUseByMonth * (BeginDate.Day - 1) + mRP_Sub_Current.RateUseByDay) / BeginDate.Day);
                                }
                            }
                            else
                            {
                                if (StartDate != DateTime.MinValue)
                                {
                                    mRP_Sub_Current.RateUseByMonth = ((mRP_Sub_Previous.RateUseByDay * (BeginDate.Day - StartDate.Day) + mRP_Sub_Current.RateUseByDay) / ((BeginDate.Day - StartDate.Day) + 1));
                                }
                                else
                                {
                                    mRP_Sub_Current.RateUseByMonth = ((mRP_Sub_Previous.RateUseByDay * (BeginDate.Day - 1) + mRP_Sub_Current.RateUseByDay) / BeginDate.Day);
                                }
                            }

                            if (mRP_Sub_Previous.SaleTotal > 0)
                                mRP_Sub_Current.RateSaleDay = (mRP_Sub_Current.SaleTotal / mRP_Sub_Previous.SaleTotal) * 100-100;
                            else if (mRP_Sub_Current.SaleTotal > 0)
                                mRP_Sub_Current.RateSaleDay = 100;
                            #endregion
                        }

                        DataSet mSet = mRP_Sub.CreateDataSet();
                        DataTable mTable = mSet.Tables[0];
                        mRP_Sub_Current.AddNewRow(ref mTable);

                        MyConvert.ConvertDateColumnToStringColumn(ref mSet);
                        mRP_Sub.Insert(0, mSet.GetXml());

                        BeginDate = BeginDate.AddDays(1);
                    }
                    catch (Exception ex)
                    {
                        MyLogfile.WriteLogError(ex);
                    }

                    if (BeginDate < DateTime.Now)
                        continue;

                   
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("CHUONG TRINH SE DELAY " + (SleepSecond / 60).ToString() + " phut.");
                    System.Threading.Thread.Sleep(SleepSecond * 1000);
                }
            }
                
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
            }           
        }
    }
}
