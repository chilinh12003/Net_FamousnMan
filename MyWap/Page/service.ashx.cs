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
    /// Summary description for service
    /// </summary>
    public class service : MyWapBase
    {
        public override void WriteHTML()
        {
            try
            {
                if (string.IsNullOrEmpty(MSISDN))
                {
                    vnp.GetMSISDN mVNPGet = new vnp.GetMSISDN();
                    MSISDN = mVNPGet.GetMSISDN_VNP();
                }

                MyLoadHeader mHeader = new MyLoadHeader(MSISDN);
                mHeader.Title = "Dịch vụ";
                Write(mHeader.GetHTML());

                MyLoadService mService = new MyLoadService();
                Write(mService.GetHTML());

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
