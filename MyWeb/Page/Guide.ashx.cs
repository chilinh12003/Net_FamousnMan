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
    /// Summary description for Guide
    /// </summary>
    public class Guide : MyASHXBase
    {
        public override void WriteHTML()
        {
            try
            {
               

                LoadHeader_Sub mHeader_Sub = new LoadHeader_Sub();
                LoadHeader mHeader = new LoadHeader();
                mHeader.Title = "Hướng dẫn cách chơi";
                mHeader.Header_Sub = mHeader_Sub.GetHTML();
                Write(mHeader.GetHTML());


                LoadGuide mGuide = new LoadGuide();
                Write(mGuide.GetHTML());

            }
            catch (Exception ex)
            {
                 mLog.Error(ex);
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