using System;
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
    public class PlayLog
    {
        MyExecuteData mExec;
        MyGetData mGet;

        public PlayLog()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public PlayLog(string KeyConnect_InConfig)
        {
            mExec = new MyExecuteData(KeyConnect_InConfig);
            mGet = new MyGetData(KeyConnect_InConfig);
        }


        /// <summary>
        /// Lấy tổng số dòng cho phân trang
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="SearchContent"></param>
        /// <param name="OrderBy"></param>
        /// <param name="IsActive"></param>
        /// <returns></returns>
        public int TotalRow(int? Type, string SearchContent, int PID, int PlayTypeID, int StatusID, int QuestionID, int SuggestID, DateTime BeginDate, DateTime EndDate)
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

                string[] mPara = { "Type", "SearchContent", "PID", "PlayTypeID", "StatusID", "QuestionID", "SuggestID", "BeginDate", "EndDate", "IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent, PID.ToString(), PlayTypeID.ToString(), StatusID.ToString(), QuestionID.ToString(), SuggestID.ToString(), str_BeginDate, str_EndDate, true.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_PlayLog_Search", mPara, mValue);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lẫy dữ liệu phân trang
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="BeginRow"></param>
        /// <param name="EndRow"></param>
        /// <param name="SearchContent"></param>
        /// <param name="OrderBy"></param>
        /// <param name="IsActive"></param>
        /// <returns></returns>
        public DataTable Search(int? Type, int BeginRow, int EndRow, string SearchContent, int PID, int PlayTypeID, int StatusID, int QuestionID, int SuggestID, DateTime BeginDate, DateTime EndDate, string OrderBy)
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

                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent", "PID", "PlayTypeID", "StatusID", "QuestionID", "SuggestID", "BeginDate", "EndDate", "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent, PID.ToString(), PlayTypeID.ToString(), StatusID.ToString(), QuestionID.ToString(), SuggestID.ToString(), str_BeginDate, str_EndDate, OrderBy, false.ToString() };
                DataTable mTable =  mGet.GetDataTable("Sp_PlayLog_Search", mpara, mValue);
                DataColumn mCol_PlayTypeName = new DataColumn("PlayTypeName", typeof(string));
                DataColumn mCol_StatusName = new DataColumn("StatusName", typeof(string));
                mTable.Columns.Add(mCol_PlayTypeName);
                mTable.Columns.Add(mCol_StatusName);

                foreach (DataRow mRow in mTable.Rows)
                {
                    mRow["StatusName"] = MyEnum.StringValueOf((Play.Status)(int)mRow["StatusID"]);
                    mRow["PlayTypeName"] = MyEnum.StringValueOf((Play.PlayType)(int)mRow["PlayTypeID"]);
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
