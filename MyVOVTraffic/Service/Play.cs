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
    public class Play
    {
        public enum Status
        {
            [DescriptionAttribute("NULL")]
            /// <summary>
            /// 
            /// </summary>
            Nothing=0, 
            [DescriptionAttribute("Dự đoán đúng")]
            /// <summary>
            /// 
            /// </summary>
            CorrectAnswer=100,
            [DescriptionAttribute("Dự đoán sai")]
            /// <summary>
            /// 
            /// </summary>
            IncorrectAnswer=101,
            [DescriptionAttribute("Mua dữ kiện")]
            /// <summary>
            /// 
            /// </summary>
            BuySuggest=200,
        }
        public enum PlayType
        {
            [DescriptionAttribute("NULL")]
            /// <summary>
            /// 
            /// </summary>
            Nothing = 0,
            [DescriptionAttribute("Mua dữ kiện")]
            /// <summary>
            /// 
            /// </summary>
            BuySuggest = 1,
            [DescriptionAttribute("Dự đoán")]
            /// <summary>
            /// 
            /// </summary>
            Answer = 2,
        }
        MyExecuteData mExec;
        MyGetData mGet;

        public Play()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public Play(string KeyConnect_InConfig)
        {
            mExec = new MyExecuteData(KeyConnect_InConfig);
            mGet = new MyGetData(KeyConnect_InConfig);
        }

        public DataSet CreateDataSet()
        {
            try
            {
                string[] mPara = { "Type" };
                string[] mValue = { "0" };
                DataSet mSet = mGet.GetDataSet("Sp_Play_Select", mPara, mValue);
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
        
    }
}
