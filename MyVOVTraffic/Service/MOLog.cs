﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MyConnect.SQLServer;
using MyUtility;
using System.Web;
using System.ComponentModel;

namespace MyFamousMan.Service
{
    public class MOLog
    {
        MyExecuteData mExec;
        MyGetData mGet;

        public MOLog()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public MOLog(string KeyConnect_InConfig)
        {
            mExec = new MyExecuteData(KeyConnect_InConfig);
            mGet = new MyGetData(KeyConnect_InConfig);
        }

        public string ConvertMTTypeIDToDescription(DefineMT.MTType mMTType, MyConfig.ChannelType mChannelType)
        {
            try
            {
                if (mChannelType == MyConfig.ChannelType.WAP)
                {
                    if (
                        mMTType == DefineMT.MTType.RegNewSuccess ||
                        mMTType == DefineMT.MTType.RegAgainSuccessFree ||
                        mMTType == DefineMT.MTType.RegAgainSuccessNotFree ||
                        mMTType == DefineMT.MTType.RegRepeatFree ||
                        mMTType == DefineMT.MTType.RegRepeatNotFree ||
                        mMTType == DefineMT.MTType.RegNotEnoughMoney ||
                        mMTType == DefineMT.MTType.RegFail ||
                        mMTType == DefineMT.MTType.RegSystemError ||
                        mMTType == DefineMT.MTType.RegCCOSSuccessFree ||
                        mMTType == DefineMT.MTType.RegCCOSSuccessNotFree
                        )
                    {
                        return "Đăng ký từ wapsite";
                    }
                    else if (
                        mMTType == DefineMT.MTType.DeregSuccess ||
                        mMTType == DefineMT.MTType.DeregNotRegister ||
                        mMTType == DefineMT.MTType.DeregExtendFail ||
                        mMTType == DefineMT.MTType.DeregFail ||
                        mMTType == DefineMT.MTType.DeregSystemError)
                    {
                        return "Hủy đăng ký từ wapsite";
                    }
                    else
                        return "Truy cập wapsite";
                }
                else
                {
                    if (mMTType == DefineMT.MTType.Invalid)
                        return "Gửi MO sai cú pháp";
                    else if (
                        mMTType == DefineMT.MTType.RegNewSuccess ||
                        mMTType == DefineMT.MTType.RegAgainSuccessFree ||
                        mMTType == DefineMT.MTType.RegAgainSuccessNotFree ||
                        mMTType == DefineMT.MTType.RegRepeatFree ||
                        mMTType == DefineMT.MTType.RegRepeatNotFree ||
                        mMTType == DefineMT.MTType.RegNotEnoughMoney ||
                        mMTType == DefineMT.MTType.RegFail ||
                        mMTType == DefineMT.MTType.RegSystemError ||
                        mMTType == DefineMT.MTType.RegCCOSSuccessFree ||
                        mMTType == DefineMT.MTType.RegCCOSSuccessNotFree)
                    {
                        return "Gửi MO DK dịch vụ";
                    }
                    else if (
                        mMTType == DefineMT.MTType.DeregSuccess ||
                        mMTType == DefineMT.MTType.DeregNotRegister ||
                        mMTType == DefineMT.MTType.DeregExtendFail ||
                        mMTType == DefineMT.MTType.DeregFail ||
                        mMTType == DefineMT.MTType.DeregSystemError)
                    {
                        return "Gửi MO HUY dịch vụ";
                    }
                    else if (
                        mMTType == DefineMT.MTType.BuySugAnswerRight ||
                        mMTType == DefineMT.MTType.BuySugExpire ||
                        mMTType == DefineMT.MTType.BuySugFail ||
                        mMTType == DefineMT.MTType.BuySugLimit ||
                        mMTType == DefineMT.MTType.BuySugNotEnoughMoney ||
                        mMTType == DefineMT.MTType.BuySugNotExtend ||
                        mMTType == DefineMT.MTType.BuySugNotify ||
                        mMTType == DefineMT.MTType.BuySugNotReg ||
                        mMTType == DefineMT.MTType.BuySugSuccess

                        )
                    {
                        return "Gửi MO mua dữ kiện";
                    }
                    else if (
                        mMTType == DefineMT.MTType.AnswerExpire ||
                        mMTType == DefineMT.MTType.AnswerFail ||
                        mMTType == DefineMT.MTType.AnswerLimit ||
                        mMTType == DefineMT.MTType.AnswerNotBuy ||
                        mMTType == DefineMT.MTType.AnswerNotReg ||
                        mMTType == DefineMT.MTType.AnswerSuccess ||
                        mMTType == DefineMT.MTType.AnswerWrong

                    )
                    {
                        return "Gửi MO dự đoán";
                    }
                    else
                        return "Gửi MO";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ConvertMTTypeIDToActionName(DefineMT.MTType mMTType, MyConfig.ChannelType mChannelType)
        {
            try
            {
                if (mChannelType == MyConfig.ChannelType.WAP)
                {
                    return "Truy cập WAP";
                }
                else if (mChannelType == MyConfig.ChannelType.CSKH)
                {
                    return "Từ trang CSKH";
                }
                else if (mChannelType == MyConfig.ChannelType.WEB)
                {
                    return "Truy cập WEB";
                }
                else
                    return "Gửi MO";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type">
        /// <para>Type = 2: Lấy Số lượng thuê bao có MO Đăng ký (Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// <para>Type = 3: Lấy Số lượng thuê bao có MO Hủy(Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// <para>Type = 4: Lấy Số lượng thuê bao có MO Mua du kien(Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// <para>Type = 5: Lấy Số lượng thuê bao có MO Trả lời (Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// <para>Type = 6: Lấy Số lượng thuê bao có MO sai cu phap (Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// <para>Type = 7: Lấy Số lượng thuê bao có MO khac (Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// <para>Type = 9: Lấy Số lượng MO Đăng ký (Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// <para>Type = 10: Lấy Số lượng MO Hủy(Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// <para>Type = 11: Lấy Số lượng MO Mua du kien(Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// <para>Type = 12: Lấy Số lượng MO Trả lời (Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// <para>Type = 13: Lấy Số lượng MO sai cu phap (Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// <para>Type = 14: Lấy Số lượng MO khac (Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// <para>Type = 15: Lấy Số lượng MO Đăng ký tính tiền thành công (Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// <para>Type = 16: Lấy Số lượng MO mua nội dung tính tiền thành công (Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// <para>Type = 17: Lấy Tổng tiền MO đăng ký tính tiền thành công (Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// <para>Type = 18: Lấy Tổng tiền MO Mua dữ kiện tính tiền thành công (Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// <para>Type = 19: Lấy số lượng Thuê bao có MO đúng cú (Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// <para>Type = 20: Lấy số lượng Thuê bao (Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate</para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <param name="Para_2"></param>
        /// <param name="Para_3"></param>
        /// <returns></returns>
        public DataTable Select(int Type, string Para_1, string Para_2, string Para_3)
        {
            try
            {
                string[] mPara = { "Type", "Para_1", "Para_2", "Para_3" };
                string[] mValue = { Type.ToString(), Para_1, Para_2, Para_3 };
                DataTable mTable = mGet.GetDataTable("Sp_MOLog_Select", mPara, mValue);

                if (mTable.Columns.Count == 1 && mTable.Rows.Count == 0)
                {
                    DataRow mRow = mTable.NewRow();
                    mRow[0] = 0;
                    mTable.Rows.Add(mRow);
                }
                if (mTable.Columns.Count == 1 && mTable.Rows[0][0] == DBNull.Value)
                {
                    mTable.Rows[0][0] = 0;
                }
                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type">
        /// <para>Type = 8: Lấy số lượng Thuê bao có tần suất MO đúng cú (Para_1 = PID, Para_2 = BeginDate, Para_3 = EndDate, Para_4 = Begin_TanSuat, Para_5 = End_TanSuat</para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <param name="Para_2"></param>
        /// <param name="Para_3"></param>
        /// <param name="Para_4"></param>
        /// <param name="Para_5"></param>
        /// <returns></returns>
        public DataTable Select(int Type, string Para_1, string Para_2, string Para_3, string Para_4, string Para_5)
        {
            try
            {
                string[] mPara = { "Type", "Para_1", "Para_2", "Para_3", "Para_4", "Para_5" };
                string[] mValue = { Type.ToString(), Para_1, Para_2, Para_3, Para_4, Para_5 };
                DataTable mTable = mGet.GetDataTable("Sp_MOLog_Select", mPara, mValue);

                if (mTable.Columns.Count == 1 && mTable.Rows.Count == 0)
                {
                    DataRow mRow = mTable.NewRow();
                    mRow[0] = 0;
                    mTable.Rows.Add(mRow);
                }
                if (mTable.Columns.Count == 1 && mTable.Rows[0][0] == DBNull.Value)
                {
                    mTable.Rows[0][0] = 0;
                }
                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet CreateDataSet()
        {
            try
            {
                string[] mPara = { "Type" };
                string[] mValue = { "0" };
                DataSet mSet = mGet.GetDataSet("Sp_MOLog_Select", mPara, mValue);
                if (mSet != null && mSet.Tables.Count >= 1)
                {
                    mSet.DataSetName = "Parent";
                    mSet.Tables[0].TableName = "Child";
                }
                return mSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(int? Type, string XMLContent)
        {
            try
            {
                string[] mpara = { "Type", "XMLContent" };
                string[] mValue = { Type.ToString(), XMLContent };
                if (mExec.ExecProcedure("Sp_MOLog_Insert", mpara, mValue) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public int TotalRow(int? Type, string SearchContent, int PID,  int MTTypeID, int ChannelTypeID, DateTime BeginDate, DateTime EndDate)
        {
            try
            {
                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }
                string[] mPara = { "Type", "SearchContent", "PID", "MTTypeID", "ChannelTypeID", "BeginDate", "EndDate", "IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent, PID.ToString(),  MTTypeID.ToString(),  ChannelTypeID.ToString(), str_BeginDate, str_EndDate, true.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_MOLog_Search", mPara, mValue);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable Search(int? Type, int BeginRow, int EndRow, string SearchContent, int PID, int MTTypeID,  int ChannelTypeID, DateTime BeginDate, DateTime EndDate, string OrderBy)
        {
            try
            {
                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }

                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent", "PID", "MTTypeID",  "ChannelTypeID", "BeginDate", "EndDate", "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent, PID.ToString(), MTTypeID.ToString(),  ChannelTypeID.ToString(), str_BeginDate, str_EndDate, OrderBy, false.ToString() };
                DataTable mTable = mGet.GetDataTable("Sp_MOLog_Search", mpara, mValue);              

                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TotalRow_Action(int? Type, string SearchContent, int PID, int ServiceID, DateTime BeginDate, DateTime EndDate)
        {
            try
            {

                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }
                string[] mPara = { "Type", "SearchContent", "PID", "ServiceID", "BeginDate", "EndDate", "IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent, PID.ToString(), ServiceID.ToString(), str_BeginDate, str_EndDate, true.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_MOLog_Search_Action", mPara, mValue);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable Search_Action(int? Type, int BeginRow, int EndRow, string SearchContent, int PID, int ServiceID, DateTime BeginDate, DateTime EndDate, string OrderBy)
        {
            try
            {
                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }

                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent", "PID", "ServiceID", "BeginDate", "EndDate", "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent, PID.ToString(), ServiceID.ToString(), str_BeginDate, str_EndDate, OrderBy, false.ToString() };
                DataTable mTable = mGet.GetDataTable("Sp_MOLog_Search_Action", mpara, mValue);
              

                DataColumn mCol_2 = new DataColumn("ActionName", typeof(string));
                mTable.Columns.Add(mCol_2);

                DataColumn mCol_3 = new DataColumn("Description", typeof(string));
                mTable.Columns.Add(mCol_3);

               
                foreach (DataRow mRow in mTable.Rows)
                {
                    mRow["ActionName"] = ConvertMTTypeIDToActionName((DefineMT.MTType)int.Parse(mRow["MTTypeID"].ToString()), (MyConfig.ChannelType)int.Parse(mRow["ChannelTypeID"].ToString()));
                    mRow["Description"] = ConvertMTTypeIDToDescription((DefineMT.MTType)int.Parse(mRow["MTTypeID"].ToString()), (MyConfig.ChannelType)int.Parse(mRow["ChannelTypeID"].ToString()));
                  
                }

                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
