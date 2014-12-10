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
    /// Summary description for Home
    /// </summary>
    public class Home : MyASHXBase
    {
        public override void WriteHTML()
        {
            try
            {
               
                LoadHeader mHeader = new LoadHeader();
                mHeader.Title = "Nhận diện người nối tiếng";
                Write(mHeader.GetHTML());

                LoadHome mHome = new LoadHome();
                Write(mHome.GetHTML());
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