using System;
using System.Collections.Generic;
using System.Web;
using MyLoad.LoadStatic;
using MyUtility;
using System.Text;
using MyBase.MyWeb;
namespace MyWeb
{
    /// <summary>
    /// Summary description for Notify
    /// </summary>
    public class Notify : MyASHXBase
    {
        public override void WriteHTML()
        {
            try
            {
                LoadHeader mHeader = new LoadHeader();
                mHeader.Title = "Thông báo";
                LoadHeader_Sub mHeader_Sub = new LoadHeader_Sub();
                mHeader.Header_Sub = mHeader_Sub.GetHTML();
                Write(mHeader.GetHTML());

                LoadNote mHome = new LoadNote("RẤT TIẾC, ĐƯỜNG DẪN KHÔNG HỢP LỆ, XIN VUI LÒNG THỬ LẠI VỚI ĐƯỜNG DẪN ĐÚNG");
                Write(mHome.GetHTML());
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