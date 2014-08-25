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
    /// Summary description for Winner
    /// </summary>
    public class Winner : MyASHXBase
    {
        public override void WriteHTML()
        {
            try
            {
                LoadHeader_Sub mHeader_Sub = new LoadHeader_Sub();
                LoadHeader mHeader = new LoadHeader();
                mHeader.Title = "Khách hàng trúng thưởng";
                mHeader.Header_Sub = mHeader_Sub.GetHTML();
                Write(mHeader.GetHTML());

                LoadWinner mWinner = new LoadWinner();
                Write(mWinner.GetHTML());
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