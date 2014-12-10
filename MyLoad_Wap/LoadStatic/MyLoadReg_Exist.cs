using System;
using System.Collections.Generic;
using System.Web;
using MyBase.MyWap;
using MyUtility;
using System.Text;
using MyBase.MyLoad;
using MyFamousMan.Service;
using System.Data;
using MyLoad_Wap.LoadStatic;
using MyLoad_Wap.LoadService;

namespace MyLoad_Wap.LoadStatic
{
    public class MyLoadReg_Exist : MyLoadBase
    {
         public string MSISDN = string.Empty;
         public MyLoadReg_Exist(string MSISDN)
        {
            this.MSISDN = MSISDN;
            mTemplatePath = "~/Templates/Static/Reg_Exist.htm";

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
