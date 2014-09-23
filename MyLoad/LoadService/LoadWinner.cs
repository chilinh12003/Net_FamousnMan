using System;
using System.Collections.Generic;
using System.Text;
using MyBase.MyLoad;
using MyFamousMan.Service;
using System.Data;
using MyUtility;
namespace MyLoad.LoadService
{
    public class LoadWinner : MyLoadBase
    {
        public LoadWinner()
        {
            mTemplatePath = "~/Templates/Service/Winner.htm";
            mTemplatePath_Repeat = "~/Templates/Service/Winner_Repeat.htm";
            Init();
        }

        // Hàm trả về chuỗi có chứa mã HTML
        protected override string BuildHTML()
        {
            try
            {
                Winner mWinner = new Winner();
                DataTable mTable = mWinner.Select(2);

                StringBuilder mBuilder = new StringBuilder(string.Empty);
                int Index = 1;
                foreach(DataRow mRow in mTable.Rows)
                {
                    string PlayDate = ((DateTime)mRow["PlayDate"]).ToString(MyConfig.ShortDateFormat);
                    string WinnerName = mRow["WinnerName"] ==DBNull.Value?string.Empty:mRow["WinnerName"].ToString();
                    string MSISDN = mRow["MSISDN"] ==DBNull.Value?string.Empty:mRow["MSISDN"].ToString();
                    if(!string.IsNullOrEmpty(MSISDN))
                    {
                        MSISDN = MSISDN.Substring(0,MSISDN.Length - 2)+"XX";
                    }
                    string Address = mRow["Address"] ==DBNull.Value?string.Empty:mRow["Address"].ToString();
                    string Prize = mRow["Prize"] ==DBNull.Value?string.Empty:mRow["Prize"].ToString();
                    string[] arr = {Index++.ToString(),PlayDate,WinnerName,MSISDN,Address,Prize};
                    mBuilder.Append(mLoadTempLate.LoadTemplateByArray(mTemplatePath_Repeat, arr));
                }

                return mLoadTempLate.LoadTemplateByArray(mTemplatePath, new string[] { mBuilder.ToString() });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
