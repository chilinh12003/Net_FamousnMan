﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MyConnect.SQLServer;
using MyUtility;
using System.Web;
using System.ComponentModel;
using MyFamousMan.Service;

namespace MyFamousMan.Report
{
    public class RP_Sub_Partner
    {
        MyExecuteData mExec;
        MyGetData mGet;
        string KeyConnect_InConfig = string.Empty;


        public RP_Sub_Partner()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public RP_Sub_Partner(string KeyConnect_InConfig)
        {
            this.KeyConnect_InConfig = KeyConnect_InConfig;
            if (string.IsNullOrEmpty(this.KeyConnect_InConfig))
            {
                mExec = new MyExecuteData();
                mGet = new MyGetData();
            }
            else
            {
                mExec = new MyExecuteData(KeyConnect_InConfig);
                mGet = new MyGetData(KeyConnect_InConfig);
            }
        }

        public DataSet CreateDataSet()
        {
            try
            {
                string[] mPara = { "Type" };
                string[] mValue = { "0" };
                DataSet mSet = mGet.GetDataSet("Sp_RP_Sub_Partner_Select", mPara, mValue);
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

        public DataTable Select(int Type, string Para_1)
        {
            try
            {
                string[] mPara = { "Type", "Para_1" };
                string[] mValue = { Type.ToString(), Para_1 };
                return mGet.GetDataTable("Sp_RP_Sub_Partner_Select", mPara, mValue);
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
        /// <para>Type = 2: Lấy theo ngày (Para_1 = BeginDate, Para_2 = Endate)</para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <param name="Para_2"></param>
        /// <returns></returns>
        public DataTable Select(int Type, string Para_1, string Para_2)
        {
            try
            {
                string[] mPara = { "Type", "Para_1", "Para_2" };
                string[] mValue = { Type.ToString(), Para_1, Para_2 };
                return mGet.GetDataTable("Sp_RP_Sub_Partner_Select", mPara, mValue);
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
        /// <para>Type = 4: Lấy theo ngày và theo PartnerID (Para_1 = BeginDate, Para_2 = Endate, Para_3 = PartnerID)</para>
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
                return mGet.GetDataTable("Sp_RP_Sub_Partner_Select", mPara, mValue);
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
                if (mExec.ExecProcedure("Sp_RP_Sub_Partner_Insert", mpara, mValue) > 0)
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

        public bool Update(int? Type, string XMLContent)
        {
            try
            {
                string[] mpara = { "Type", "XMLContent" };
                string[] mValue = { Type.ToString(), XMLContent };
                if (mExec.ExecProcedure("Sp_RP_Sub_Partner_Update", mpara, mValue) > 0)
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

        public int TotalRow(int? Type, DateTime BeginDate, DateTime EndDate, int PartnerID)
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
                string[] mPara = { "Type", "BeginDate", "EndDate", "PartnerID", "IsTotalRow" };
                string[] mValue = { Type.ToString(), str_BeginDate, str_EndDate, PartnerID.ToString(), true.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_RP_Sub_Partner_Search", mPara, mValue);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable Search(int? Type, int BeginRow, int EndRow, DateTime BeginDate, DateTime EndDate, int PartnerID, string OrderBy)
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

                string[] mpara = { "Type", "BeginRow", "EndRow", "BeginDate", "EndDate", "PartnerID", "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), str_BeginDate, str_EndDate, PartnerID.ToString(), OrderBy, false.ToString() };
                DataTable mTable = mGet.GetDataTable("Sp_RP_Sub_Partner_Search", mpara, mValue);

                DataColumn mCol_2 = new DataColumn("PartnerName", typeof(string));
                mTable.Columns.Add(mCol_2);

                MyFamousMan.Partner mPartner = null;
                if (string.IsNullOrEmpty(this.KeyConnect_InConfig))
                {
                    mPartner = new MyFamousMan.Partner();
                }
                else
                {
                    mPartner = new MyFamousMan.Partner(this.KeyConnect_InConfig);
                }

                DataTable mTable_Partner = mPartner.Select(4);

                foreach (DataRow mRow in mTable.Rows)
                {
                    mTable_Partner.DefaultView.RowFilter = "PartnerID = '" + mRow["PartnerID"].ToString() + "'";
                    if (mTable_Partner.DefaultView.Count > 0)
                    {
                        mRow["PartnerName"] = mTable_Partner.DefaultView[0]["PartnerName"].ToString();
                    }
                    else if(mRow["PartnerID"] == DBNull.Value || (int)mRow["PartnerID"] == 0)
                    {
                        mRow["PartnerName"] = "HB";
                    }
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
