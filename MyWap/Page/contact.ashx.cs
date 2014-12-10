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
    /// Summary description for contact
    /// </summary>
    public class contact : MyWapBase
    {
        public override void WriteHTML()
        {
            try
            {
                MyLoadHeader mHeader = new MyLoadHeader(MSISDN);
                mHeader.Title = "Liên hệ";
                Write(mHeader.GetHTML());

                MyLoadContact mContact = new MyLoadContact();
                Write(mContact.GetHTML());

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
