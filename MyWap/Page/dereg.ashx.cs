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
    /// Summary description for accept
    /// </summary>
    public class dereg : MyWapBase
    {
        public override void WriteHTML()
        {
            string Para = string.Empty;           

            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            string BeforeDate = string.Empty;
            try
            {
                Para = Request.QueryString["para"];

                MyLoadHeader mHeader = new MyLoadHeader(MSISDN);
                Write(mHeader.GetHTML());

                if (!string.IsNullOrEmpty(Para))
                {
                    string Para_Decode = MySecurity.AES.Decrypt(Para, MySetting.AdminSetting.SpecialKey);
                    if (!string.IsNullOrEmpty(Para_Decode))
                    {
                        string[] arr = Para_Decode.Split('|');
                        if (arr.Length == 2)
                        {
                            MSISDN = arr[0];
                            BeforeDate = arr[1];
                        }
                    }
                }
                else
                {
                    MyLoadNote mNote = new MyLoadNote("Thông tin không hợp lệ, xin vui lòng thử lại với thông tin khác.");
                    Write(mNote.GetHTML());
                    return;
                }

                //kiểm tra nếu truy cập quá nhanh
                if (Session["RequestTime_Accept"] != null)
                {
                    DateTime RequestTime = (DateTime)Session["RequestTime_Accept"];
                    TimeSpan Interval_Time = DateTime.Now - RequestTime;
                    if (Interval_Time.TotalSeconds < 60)
                    {
                        MyLoadNote mNote = new MyLoadNote("Bạn đang truy cập lặp lại quá nhanh, xin vui lòng chờ trong 1 phút và hãy thử lại.");
                        Write(mNote.GetHTML());
                        return;
                    }
                }

                Session["RequestTime_Accept"] = DateTime.Now;

             
                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                string MSISDN_Temp = MSISDN;
                if (!MyCheck.CheckPhoneNumber(ref MSISDN_Temp, ref mTelco, "84") || mTelco != MyConfig.Telco.Vinaphone)
                {
                    MyLoadNote mNote = new MyLoadNote("Số điện thoại không đúng hoặc không thuộc mạng Vinaphone.");
                    Write(mNote.GetHTML());
                    return;
                }
                MSISDN = MSISDN_Temp;

                MyService.ActionSoapClient mClient = new MyService.ActionSoapClient();
                string Signature = MSISDN + "|HBWap|" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                Signature = MySecurity.AES.Encrypt(Signature, MySetting.AdminSetting.RegWSKey);
                System.Net.ServicePointManager.Expect100Continue = false;
                string Result = mClient.Dereg((int)MyConfig.ChannelType.WAP, Signature, "HUY");
                string[] Arr_Result = Result.Split('|');

                ErrorCode = Arr_Result[0];
                ErrorDesc = Arr_Result[1];

                switch (ErrorCode)
                {
                    case "1":
                        ErrorDesc = "Bạn đã hủy công dịch vụ Nhận diện người nổi tiếng.";
                        break;
                    case "0":
                        ErrorDesc = "Hủy dịch vụ không thành công, xin vui lòng thử lại sau ít phút.";
                        break;
                    case "2":
                        ErrorDesc = "Bạn đã Hủy dịch vụ này trước đây.";
                        break;
                    case "3":
                        ErrorDesc = "Hủy dịch vụ không thành công, xin vui lòng thử lại sau ít phút.";
                        break;
                    case "-1":
                        ErrorDesc = "Hủy dịch vụ không thành công, xin vui lòng thử lại sau ít phút.";
                        break;
                    case "-2":
                        ErrorDesc = "Hủy dịch vụ không thành công, xin vui lòng thử lại sau ít phút.";
                        break;
                    case "4":
                        ErrorDesc = "Bạn chưa tiến hành đăng ký dịch vụ, nên không thể hủy đăng ký.";
                        break;
                    case "5":
                        ErrorDesc = "Bạn chưa tiến hành đăng ký dịch vụ, nên không thể hủy đăng ký.";
                        break;
                    case "6":
                        ErrorDesc = "Bạn chưa tiến hành đăng ký dịch vụ, nên không thể hủy đăng ký.";
                        break;
                }

                MyLoadNote mNote_1 = new MyLoadNote(ErrorDesc);
                Write(mNote_1.GetHTML());
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError("_Error", ex, false, MyNotice.EndUserError.LoadDataError, "Chilinh");
                Write(MyNotice.EndUserError.LoadDataError);
            }
            finally
            {
                MyLoadFooter mFooter = new MyLoadFooter();
                Write(mFooter.GetHTML());

            }

        }
    }
}