using System;
using System.Collections.Generic;
using System.Text;
using MyBase.MyLoad;
using System.Web;
using MyUtility;
namespace MyLoad_Wap.LoadStatic
{
    public class MyLoadHeader:MyLoadBase
    {
        public string CSSLink = string.Empty;
        public string JSLink = string.Empty;
        public string HeaderScript = string.Empty;
        public string Title = string.Empty;
        public string ImgURL = string.Empty;
        public string Description = string.Empty;
        public string MSISDN = string.Empty;
        public MyLoadHeader()
        {
            mTemplatePath = "~/Templates/Static/Header.htm";
            Init();
        }
        public MyLoadHeader(string MSISDN)
            : this()
        {
            this.MSISDN = MSISDN;
        }
        // Hàm trả về chuỗi có chứa mã HTML
        protected override string BuildHTML()
        {
            try
            {
                if (!MyUtility.MyCheck.CheckMobileDevide())
                {
                    MyCurrent.CurrentPage.Response.Redirect(MySetting.WebSetting.LinkWeb);
                    return string.Empty;
                }

                string[] arr = { Title, MSISDN };
                return mLoadTempLate.LoadTemplateByArray(mTemplatePath, arr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
