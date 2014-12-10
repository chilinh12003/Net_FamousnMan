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
    /// Summary description for Rule
    /// </summary>
    public class Rule : MyASHXBase
    {
        public override void WriteHTML()
        {
            try
            {
                LoadHeader_Sub mHeader_Sub = new LoadHeader_Sub();
                LoadHeader mHeader = new LoadHeader();
                mHeader.Title = "Thể lệ giải thưởng";
                mHeader.Header_Sub = mHeader_Sub.GetHTML();
                Write(mHeader.GetHTML());

                LoadRule mRule = new LoadRule();
                Write(mRule.GetHTML());

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