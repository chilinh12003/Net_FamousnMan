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
    /// Summary description for faq
    /// </summary>
    public class faq : MyASHXBase
    {
        public override void WriteHTML()
        {
            try
            {
               

              
                LoadHeader mHeader = new LoadHeader();
                mHeader.Title = "Hỏi đáp";
                LoadHeader_Sub mHeader_Sub = new LoadHeader_Sub();
                mHeader.Header_Sub = mHeader_Sub.GetHTML();
                Write(mHeader.GetHTML());

                LoadFAQ mFAQ = new LoadFAQ();
                Write(mFAQ.GetHTML());

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