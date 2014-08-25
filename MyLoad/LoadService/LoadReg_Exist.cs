using System;
using System.Collections.Generic;
using System.Text;
using MyBase.MyLoad;
using MyUtility;
using System.Web;

namespace MyLoad.LoadService
{
    public class LoadReg_Exist : MyLoadBase
    {
        public string MSISDN = string.Empty;
        public LoadReg_Exist(string MSISDN)
        {
            this.MSISDN = MSISDN;
            mTemplatePath = "~/Templates/Service/Reg_Exist.htm";
            Init();
        }

        // Hàm trả về chuỗi có chứa mã HTML
        protected override string BuildHTML()
        {
            try
            {
                string ConfirmPara = MSISDN + "|" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string ConfirmPara_Encode = MySecurity.AES.Encrypt(ConfirmPara, MySetting.AdminSetting.SpecialKey);
                string[] Arr = new string[] { MSISDN, HttpUtility.UrlEncode(ConfirmPara_Encode) };
                return mLoadTempLate.LoadTemplateByArray(mTemplatePath, Arr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
