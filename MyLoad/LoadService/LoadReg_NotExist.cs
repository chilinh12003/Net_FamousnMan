using System;
using System.Collections.Generic;
using System.Text;
using MyBase.MyLoad;
using MyUtility;
using System.Web;
namespace MyLoad.LoadService
{
    public class LoadReg_NotExist : MyLoadBase
    {      
        public string MSISDN = string.Empty;
        public LoadReg_NotExist(string MSISDN)
        {
            this.MSISDN = MSISDN;
            mTemplatePath = "~/Templates/Service/Reg_NotExist.htm";

            Init();
        }
        
        // Hàm trả về chuỗi có chứa mã HTML
        protected override string BuildHTML()
        {
            try
            {
              
                string[] Arr = new string[] { MSISDN };
                return mLoadTempLate.LoadTemplateByArray(mTemplatePath, Arr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
