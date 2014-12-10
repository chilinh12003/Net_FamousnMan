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

namespace MyWap
{
    /// <summary>
    /// Summary description for Nofity
    /// </summary>
    public class Notify : MyWapBase
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
                mHeader.Title = "Thông báo";
                Write(mHeader.GetHTML());

                MyLoadNote mHome = new MyLoadNote("RẤT TIẾC, ĐƯỜNG DẪN KHÔNG HỢP LỆ, XIN VUI LÒNG THỬ LẠI VỚI ĐƯỜNG DẪN ĐÚNG");
                Write(mHome.GetHTML());

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
