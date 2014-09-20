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
    public class MOSync
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


        MOLog mMOlog = null;      

        RP_MO mRP_MO = null;
        RP_MOTotal mRP_MOTotal = null;

        public MOSync()
        {
            try
            {
                mMOlog = new MOLog(ConnectionKey_Source);  
               
                mRP_MO = new RP_MO(ConnectionKey_Des);
                mRP_MOTotal = new RP_MOTotal(ConnectionKey_Des);
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
        List<RP_MO> Get_RP_MO_ByDate(DateTime ReportDay)
        {
            try
            {
                DataTable mTable = mRP_MO.Select(1, ReportDay.ToString(MyConfig.DateFormat_InsertToDB));
                return mRP_MO.Convert(mTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        RP_MOTotal Get_RP_MOTotalByDate(DateTime ReportDay)
        {
            try
            {
                DataTable mTable = mRP_MOTotal.Select(1, ReportDay.ToString(MyConfig.DateFormat_InsertToDB));

                return mRP_MOTotal.Convert(mTable);
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
        /// Ngày đầu tiên dịch vụ chạy
        /// </summary>
        DateTime FirstDateOfService = new DateTime(DateTime.Now.Year, 8, 01, 0, 0, 0); 

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

        /// <summary>
        /// Lấy 1 đối tượng từ list thông qua MOType
        /// </summary>
        /// <param name="mList"></param>
        /// <param name="mMOType"></param>
        /// <returns></returns>
        RP_MO GetByMOType(List<RP_MO> mList, RP_MO.MOType mMOType)
        {
            try
            {
                if(mList == null || mList.Count < 1)
                    new RP_MO(ConnectionKey_Des);

                foreach (RP_MO mObj in mList)
                {
                    if (mObj.mMOType == mMOType)
                        return mObj;
                }
                return new RP_MO(ConnectionKey_Des);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Run()
        {
            try
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("BAT DAU CHAY CHUONG TRINH MO_SYNC");

                if (StartDate != DateTime.MinValue)
                {
                    BeginDate = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, 0, 0, 0);
                }               
                
                while (!StopThread)
                {
                    try
                    {
                        if (BeginDate > DateTime.Now)
                        {
                            BeginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                        }
                        //Ngày hiện tại
                        List<RP_MO> mList_Current = new List<RP_MO>();

                        //Ngày hôm qua
                        List<RP_MO> mList_Previos = Get_RP_MO_ByDate(BeginDate.AddDays(-1));

                        //Ngày hiện tại
                        RP_MOTotal mRP_MOTotal_Current = new RP_MOTotal(ConnectionKey_Des);
                        mRP_MOTotal_Current.ReportDay = BeginDate;

                        //Ngày hôm qua
                        RP_MOTotal mRP_MOTotal_Previous = Get_RP_MOTotalByDate(BeginDate.AddDays(-1));

                        foreach (RP_MO.MOType mItem in (RP_MO.MOType[])Enum.GetValues(typeof(RP_MO.MOType)))
                        {
                            if (mItem == RP_MO.MOType.Nothing)
                                continue;

                            RP_MO mObj = new RP_MO(ConnectionKey_Des);
                            mObj.ReportDay = BeginDate;
                            mObj.mMOType = mItem;

                            mList_Current.Add(mObj);
                        }

                        for (int PID = 0; PID <= MaxPID; PID++)
                        {
                            if (StopThread)
                                break;

                            Console.WriteLine("BeginDate: " + BeginDate.ToString(MyConfig.LongDateFormat) + " || Lay du lieu voi pid = " + PID.ToString());

                            #region Lấy thông tin cho MO Đăng ký
                            RP_MO mRP_MO_Current_Reg = GetByMOType(mList_Current, RP_MO.MOType.Register);
                            mRP_MO_Current_Reg.SubCorrect += double.Parse(mMOlog.Select(2, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            mRP_MO_Current_Reg.MOCorrect += double.Parse(mMOlog.Select(9, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                        EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            mRP_MO_Current_Reg.MOChargeSuccess += double.Parse(mMOlog.Select(15, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                        EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            mRP_MO_Current_Reg.MOSale += double.Parse(mMOlog.Select(17, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                       EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            #endregion

                            #region Lấy thông tin cho MO Hủy
                            RP_MO mRP_MO_Current_Dereg = GetByMOType(mList_Current, RP_MO.MOType.Deregister);
                            mRP_MO_Current_Dereg.SubCorrect += double.Parse(mMOlog.Select(3, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            mRP_MO_Current_Dereg.MOCorrect += double.Parse(mMOlog.Select(10, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                        EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            mRP_MO_Current_Dereg.MOChargeSuccess += 0;

                            mRP_MO_Current_Dereg.MOSale += 0;

                            #endregion

                            #region Lấy thông tin cho MO Mua dữ kiện
                            RP_MO mRP_MO_Current_Buy = GetByMOType(mList_Current, RP_MO.MOType.BuyContent);
                            mRP_MO_Current_Buy.SubCorrect += double.Parse(mMOlog.Select(4, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            mRP_MO_Current_Buy.MOCorrect += double.Parse(mMOlog.Select(11, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                        EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            mRP_MO_Current_Buy.MOChargeSuccess += double.Parse(mMOlog.Select(16, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                        EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            mRP_MO_Current_Buy.MOSale += double.Parse(mMOlog.Select(18, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                       EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            #endregion

                            #region Lấy thông tin cho MO dự đoán
                            RP_MO mRP_MO_Current_Answer = GetByMOType(mList_Current, RP_MO.MOType.Answer);
                            mRP_MO_Current_Answer.SubCorrect += double.Parse(mMOlog.Select(5, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            mRP_MO_Current_Answer.MOCorrect += double.Parse(mMOlog.Select(12, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                        EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            mRP_MO_Current_Answer.MOChargeSuccess += 0;

                            mRP_MO_Current_Answer.MOSale += 0;

                            #endregion

                            #region Lấy thông tin cho MO sai cú pháp
                            RP_MO mRP_MO_Current_INV = GetByMOType(mList_Current, RP_MO.MOType.Invalid);
                            mRP_MO_Current_INV.SubCorrect += double.Parse(mMOlog.Select(6, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            mRP_MO_Current_INV.MOCorrect += double.Parse(mMOlog.Select(13, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                        EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            mRP_MO_Current_INV.MOChargeSuccess += 0;

                            mRP_MO_Current_INV.MOSale += 0;

                            #endregion

                            #region Lấy thông tin cho MO khác
                            RP_MO mRP_MO_Current_Other = GetByMOType(mList_Current, RP_MO.MOType.Other);
                            mRP_MO_Current_Other.SubCorrect += double.Parse(mMOlog.Select(7, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                         EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            mRP_MO_Current_Other.MOCorrect += double.Parse(mMOlog.Select(14, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                        EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            mRP_MO_Current_Other.MOChargeSuccess += 0;

                            mRP_MO_Current_Other.MOSale += 0;

                            #endregion

                            ////LẤY THÔNG TIN CHO TABLE RP_MOTOTAL
                            #region MOTotal


                            mRP_MOTotal_Current.SubCorrectTotal += double.Parse(mMOlog.Select(19, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                      EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            mRP_MOTotal_Current.SubTotal += double.Parse(mMOlog.Select(20, PID.ToString(), BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                     EndDate.ToString(MyConfig.DateFormat_InsertToDB)).Rows[0][0].ToString());

                            mRP_MOTotal_Current.Sub1Correct += double.Parse(mMOlog.Select(8, PID.ToString(),
                                                                          FirstDateOfService.ToString(MyConfig.DateFormat_InsertToDB),
                                                                          BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                          "1", "1").Rows[0][0].ToString());
                            mRP_MOTotal_Current.Sub2Correct += double.Parse(mMOlog.Select(8, PID.ToString(),
                                                                            FirstDateOfService.ToString(MyConfig.DateFormat_InsertToDB),
                                                                            BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                             "2", "3").Rows[0][0].ToString());
                            mRP_MOTotal_Current.Sub4Correct += double.Parse(mMOlog.Select(8, PID.ToString(),
                                                                            FirstDateOfService.ToString(MyConfig.DateFormat_InsertToDB),
                                                                            BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                             "4", "5").Rows[0][0].ToString());
                            mRP_MOTotal_Current.Sub5Correct += double.Parse(mMOlog.Select(8, PID.ToString(),
                                                                            FirstDateOfService.ToString(MyConfig.DateFormat_InsertToDB),
                                                                            BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                             "6", "10").Rows[0][0].ToString());
                            mRP_MOTotal_Current.Sub10Correct += double.Parse(mMOlog.Select(8, PID.ToString(),
                                                                            FirstDateOfService.ToString(MyConfig.DateFormat_InsertToDB),
                                                                            BeginDate.ToString(MyConfig.DateFormat_InsertToDB),
                                                                             "11", "1000000").Rows[0][0].ToString());

                            if (PID == MaxPID)
                            {
                                mRP_MOTotal_Current.MOTotal = mRP_MO_Current_Reg.MOCorrect + mRP_MO_Current_Dereg.MOCorrect +
                                                                mRP_MO_Current_Buy.MOCorrect + mRP_MO_Current_Answer.MOCorrect +
                                                                mRP_MO_Current_INV.MOCorrect + mRP_MO_Current_Other.MOCorrect;

                                mRP_MOTotal_Current.MOCorrectTotal = mRP_MO_Current_Reg.MOCorrect + mRP_MO_Current_Dereg.MOCorrect +
                                                               mRP_MO_Current_Buy.MOCorrect + mRP_MO_Current_Answer.MOCorrect +
                                                                mRP_MO_Current_Other.MOCorrect;
                               

                                if (mRP_MOTotal_Current.SubTotal > 0)
                                    mRP_MOTotal_Current.MOCorrectRate = mRP_MOTotal_Current.MOCorrectTotal / mRP_MOTotal_Current.SubTotal;
                                else if (mRP_MOTotal_Current.MOCorrectTotal > 0)
                                {
                                    mRP_MOTotal_Current.MOCorrectRate = 100;
                                }

                                mRP_MOTotal_Current.MOSaleTotal = mRP_MO_Current_Reg.MOSale + mRP_MO_Current_Buy.MOSale;

                                double TotalSub_Use = mRP_MO_Current_Buy.SubCorrect;
                                double TotalSub_Use_Previous = GetByMOType(mList_Previos, RP_MO.MOType.BuyContent).SubCorrect;
                                if (TotalSub_Use_Previous > 0)
                                    mRP_MOTotal_Current.RateSubByDay = (TotalSub_Use / TotalSub_Use_Previous) * 100 - 100;
                                else if(TotalSub_Use > 0)
                                    mRP_MOTotal_Current.RateSubByDay = 100;


                                double TotalSale = mRP_MO_Current_Reg.MOSale + mRP_MO_Current_Buy.MOSale;
                                double TotalSale_Previous = GetByMOType(mList_Previos, RP_MO.MOType.Register).MOSale +
                                                                GetByMOType(mList_Previos, RP_MO.MOType.BuyContent).MOSale;

                                if (TotalSale_Previous > 0)
                                    mRP_MOTotal_Current.RateSaleByDay = (TotalSale / TotalSale_Previous) * 100 - 100;
                                else if (TotalSale > 0)
                                    mRP_MOTotal_Current.RateSaleByDay = 100;
                            }

                            #endregion
                        }

                        //Insert vào table RP_MO
                        DataSet mSet = mRP_MO.CreateDataSet();
                        DataTable mTable = mSet.Tables[0];

                        foreach (RP_MO mObj in mList_Current)
                        {
                            mObj.AddNewRow(ref mTable);
                        }                  

                        MyConvert.ConvertDateColumnToStringColumn(ref mSet);
                        mRP_MO.Insert(0, mSet.GetXml());


                        //Insert vào table RP_MOTotal
                        DataSet mSet_MOTotal = mRP_MOTotal.CreateDataSet();
                        DataTable mTable_MOTotal = mSet_MOTotal.Tables[0];
                        mRP_MOTotal_Current.AddNewRow(ref mTable_MOTotal);

                        MyConvert.ConvertDateColumnToStringColumn(ref mSet_MOTotal);
                        mRP_MOTotal.Insert(0, mSet_MOTotal.GetXml());

                        BeginDate = BeginDate.AddDays(1);
                    }
                    catch (Exception ex)
                    {
                        MyLogfile.WriteLogError(ex);
                    }

                    if (BeginDate < DateTime.Now)
                        continue;

                   
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("CHUONG TRINH MO_SYNC SE DELAY " + (SleepSecond / 60).ToString() + " phut.");
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
