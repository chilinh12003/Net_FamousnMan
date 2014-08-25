using System;
using System.Collections.Generic;
using System.Web;
using MyLoad.LoadStatic;
using MyUtility;
using System.Text;
using MyBase.MyWeb;

namespace MyWeb.Page
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    public class Service : MyASHXBase
    {
        public override void WriteHTML()
        {
            try
            {
               

                LoadHeader_Sub mHeader_Sub = new LoadHeader_Sub();
                LoadHeader mHeader = new LoadHeader();
                mHeader.Title = "Thông tin dịch vụ";
                mHeader.Header_Sub = mHeader_Sub.GetHTML();
                Write(mHeader.GetHTML());

                LoadService mService = new LoadService();
                Write(mService.GetHTML());
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError("_Error", ex, false, MyNotice.EndUserError.LoadDataError, "Chilinh");
                Write(MyNotice.EndUserError.LoadDataError);
            }
            finally
            {
                LoadFooter mFooter = new LoadFooter();
                Write(mFooter.GetHTML());
            }
        }
    }
}