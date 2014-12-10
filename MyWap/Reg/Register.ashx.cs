using System;
using System.Collections.Generic;
using System.Web;
using MyBase.MyWap;
using MyUtility;
using System.Text;
using MyLoad_Wap.LoadStatic;
using MyLoad_Wap.LoadService;
using MyFamousMan.Service;
using System.Data;
namespace MyWap.Reg
{
    /// <summary>
    /// Summary description for Register
    /// </summary>
    public class Register : MyWapBase
    {
        Keyword mKeyword = new Keyword();
        vnp.GetMSISDN mGetMSISDN = new vnp.GetMSISDN();


        public override void WriteHTML()
        {
            try
            {
                if (string.IsNullOrEmpty(MSISDN))
                {
                    MSISDN = mGetMSISDN.GetMSISDN_VNP();
                }

                MyLoadHeader mHeader = new MyLoadHeader(MSISDN);
                mHeader.Title = "Nhận diện người nổi tiếng";
                Write(mHeader.GetHTML());

                Write(BuildRegister());
            }
            catch (Exception ex)
            {
                mLog.Error(ex);
                Write(MyNotice.EndUserError.LoadDataError);
            }
            finally
            {
                MyLoadFooter mFooter = new MyLoadFooter();
                Write(mFooter.GetHTML());
            }
        }


        public string BuildRegister()
        {
            string Para = string.Empty;
            string ServiceName = "Nhận diện người nổi tiếng";
            int KeywordID = 0;
            int PartnerID = 0;
            string Keyword = string.Empty;

            string ErrorCode = string.Empty;
            string ErrorDesc = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["kid"]))
                {
                    int.TryParse(Request.QueryString["kid"].TrimEnd().TrimStart(), out KeywordID);
                }

                // Trả về mã HTML cho header từ template (Fixed)
                if (string.IsNullOrEmpty(MSISDN))
                {
                    MyLoadNote mNote = new MyLoadNote("Không nhận diện được thuê bao khách hàng, xin vui lòng sử dụng 3G hoặc GPRS để truy cập.");
                    return mNote.GetHTML();
                }

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                string MSISDN_TEMP = MSISDN;
                if (!MyCheck.CheckPhoneNumber(ref MSISDN_TEMP, ref mTelco, "84") || mTelco != MyConfig.Telco.Vinaphone)
                {
                    MyLoadNote mNote = new MyLoadNote("Số điện thoại không đúng hoặc không thuộc mạng Vinaphone.");
                    return mNote.GetHTML();
                }

                ////kiểm tra nếu truy cập quá nhanh
                //if (Session["RequestTime"] != null)
                //{
                //    DateTime RequestTime = (DateTime)Session["RequestTime"];
                //    TimeSpan Interval_Time = DateTime.Now - RequestTime;
                //    if (Interval_Time.TotalSeconds < 30)
                //    {
                //        MyLoadNote mNote = new MyLoadNote("Bạn đang truy cập lặp lại quá nhanh, xin vui lòng chờ trong 30 giây và hãy thử lại.");
                //        return mNote.GetHTML();
                //    }
                //}

                //Session["RequestTime"] = DateTime.Now;

                DataTable mTable_Keyword = mKeyword.Select(2, KeywordID.ToString(), string.Empty);

                if (mTable_Keyword.Rows.Count < 1)
                {
                    MyLoadNote mNote = new MyLoadNote("Thông tin của đối tác không hợp lệ, xin vui lòng thử lại với thông tin khác.");
                    return mNote.GetHTML();
                }

                PartnerID = (int)mTable_Keyword.Rows[0]["PartnerID"];
                Keyword = mTable_Keyword.Rows[0]["Keyword"].ToString();


                MyService.ActionSoapClient mClient = new MyService.ActionSoapClient();
                string Signature = MSISDN + "|HBWap|" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                Signature = MySecurity.AES.Encrypt(Signature, MySetting.AdminSetting.RegWSKey);
                System.Net.ServicePointManager.Expect100Continue = false;

                //nếu chưa từng sử dụng dịch vụ lần nào và keyword này là yêu cầu confirm
                if ((bool)mTable_Keyword.Rows[0]["IsConfirm"])
                {
                    MyLoadReg_Confirm mReg_Confirm = new MyLoadReg_Confirm(MSISDN,Keyword,PartnerID);
                    return mReg_Confirm.GetHTML();
                }
                //nếu không thì đăng ký ngay
                string Result = mClient.Reg((int)MyConfig.ChannelType.WAP, Signature, Keyword);
                string[] Arr_Result = Result.Split('|');

                ErrorCode = Arr_Result[0];
                ErrorDesc = string.Empty;

                switch (ErrorCode)
                {
                    case "1":
                        ErrorDesc = "Chúc mừng bạn đã đăng ký thành công dịch vụ " + ServiceName + ".";
                        break;
                    case "0":
                        ErrorDesc = "Đăng ký dịch vụ không thành công, xin vui lòng thử lại sau ít phút.";
                        break;
                    case "2":
                        ErrorDesc = "Bạn đã đăng ký dịch vụ này trước đây.";
                        break;
                    case "3":
                        ErrorDesc = "Đăng ký không thành công, xin vui lòng thử lại sau ít phút.";
                        break;
                    case "-1":
                        ErrorDesc = "Đăng ký không thành công, xin vui lòng thử lại sau ít phút.";
                        break;
                    case "-2":
                        ErrorDesc = "Đăng ký không thành công, xin vui lòng thử lại sau ít phút.";
                        break;
                }


            }
            catch (Exception ex)
            {
                ErrorDesc = "Xin lỗi,hệ thống đang quá tải, xin vui lòng thử lại sau ít phút.";
                mLog.Error(ex);
                Write(MyNotice.EndUserError.LoadDataError);
            }
            finally
            {

                mLog.Debug("REGISTER", "REGISTER INFO: PartnerID:" + PartnerID.ToString() + "|Keyword:" + Keyword + "|MSISDN:" + MSISDN + "|ErrorCode:" + ErrorCode + "|ErrorDesc:" + ErrorDesc);

            }
            MyLoadNote mNote_1 = new MyLoadNote(ErrorDesc);
            return mNote_1.GetHTML();

        }
    }
}