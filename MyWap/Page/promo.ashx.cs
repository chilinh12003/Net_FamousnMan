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


namespace MyWap.Page
{
    /// <summary>
    /// Summary description for promo
    /// </summary>
    public class promo : MyWapBase
    {
        public override void WriteHTML()
        {
            try
            {
                MyLoadHeader mHeader = new MyLoadHeader(MSISDN);
                mHeader.Title = "Khuyến mại";
                Write(mHeader.GetHTML());

                MyLoadPromo mPromo = new MyLoadPromo();
                Write(mPromo.GetHTML());

                MyLoadFooter mFooter = new MyLoadFooter();
                Write(mFooter.GetHTML());

            }
            catch (Exception ex)
            {
                mLog.Error(ex);
                Write(MyNotice.EndUserError.LoadDataError);
            }

        }
    }
}
