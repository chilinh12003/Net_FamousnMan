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
    /// Summary description for Contact
    /// </summary>
    public class Contact : MyASHXBase
    {
        public override void WriteHTML()
        {
            try
            {
                
                LoadHeader_Sub mHeader_Sub = new LoadHeader_Sub();
                LoadHeader mHeader = new LoadHeader();
                mHeader.Title = "Liên hệ";
                mHeader.Header_Sub = mHeader_Sub.GetHTML();
                Write(mHeader.GetHTML());

                LoadContact mContact = new LoadContact();
                Write(mContact.GetHTML());

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