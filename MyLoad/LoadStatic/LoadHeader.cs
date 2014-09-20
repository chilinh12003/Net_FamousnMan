using System;
using System.Collections.Generic;
using System.Text;
using MyBase.MyLoad;
using MyUtility;
using System.Web;
namespace MyLoad.LoadStatic
{
    public class LoadHeader : MyLoadBase
    {
        public string Title = string.Empty;
        public string Header_Sub = string.Empty;
        public LoadHeader()
        {
            mTemplatePath = "~/Templates/Static/Header.htm";
            Init();
        }

        // Hàm trả về chuỗi có chứa mã HTML
        protected override string BuildHTML()
        {
            try
            {
                if (MyUtility.MyCheck.CheckMobileDevide())
                {
                    MyCurrent.CurrentPage.Response.Redirect(MySetting.WebSetting.LinkWap);
                    return string.Empty;
                }

                string[] arr = { Title, Header_Sub };
                return mLoadTempLate.LoadTemplateByArray(mTemplatePath, arr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
