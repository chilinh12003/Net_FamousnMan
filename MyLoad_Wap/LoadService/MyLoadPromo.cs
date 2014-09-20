using System;
using System.Collections.Generic;
using System.Web;
using MyBase.MyWap;
using MyUtility;
using System.Text;
using MyBase.MyLoad;
using MyFamousMan.Service;
using System.Data;
namespace MyLoad_Wap.LoadService
{
    public class MyLoadPromo : MyLoadBase
    {
        public MyLoadPromo()
        {
            mTemplatePath = "~/Templates/Service/Promo.htm";
            Init();
        }

       
        protected override string BuildHTML()
        {
            try
            {
                return mLoadTempLate.LoadTemplate(mTemplatePath);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
