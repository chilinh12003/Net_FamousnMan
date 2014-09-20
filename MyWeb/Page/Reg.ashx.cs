using System;
using System.Collections.Generic;
using System.Web;
using MyLoad.LoadStatic;
using MyUtility;
using System.Text;
using MyBase.MyWeb;
using MyLoad.LoadService;
namespace MyWeb.Page
{
    /// <summary>
    /// Summary description for Home
    /// </summary>
    public class Reg : MyASHXBase
    {
        public override void WriteHTML()
        {
            try
            {
               
                LoadHeader mHeader = new LoadHeader();
                mHeader.Title = "Đăng ký dịch vụ";
                LoadHeader_Sub mHeader_Sub = new LoadHeader_Sub();
                mHeader.Header_Sub = mHeader_Sub.GetHTML();
                Write(mHeader.GetHTML());

                if (Request.QueryString["para"] == null)
                {
                    Response.Redirect(MyConfig.Domain + "/VNPLogin.aspx");
                    return;
                }

                string Para = Request.QueryString["para"];
                if(string.IsNullOrEmpty(Para))
                {

                    LoadReg_Invalid mInvalid = new LoadReg_Invalid();
                    Write(mInvalid.GetHTML());
                    return;
                }
                string Para_Decode = MySecurity.AES.Decrypt(Para, MySetting.AdminSetting.SpecialKey);
                string MSISDN = string.Empty;

                if (!string.IsNullOrEmpty(Para_Decode))
                {
                    string[] arr = Para_Decode.Split('|');
                    if (arr.Length == 2)
                    {
                        MSISDN = arr[0];
                    }
                }

                if (string.IsNullOrEmpty(MSISDN))
                {
                    LoadReg_Invalid mInvalid = new LoadReg_Invalid();
                    Write(mInvalid.GetHTML());
                    return;
                }

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;

                if(!MyCheck.CheckPhoneNumber(ref MSISDN,ref mTelco,"84") || mTelco != MyConfig.Telco.Vinaphone)
                {
                    LoadReg_Invalid mInvalid = new LoadReg_Invalid();
                    Write(mInvalid.GetHTML());
                    return;
                }

                Session["MSISDN"] = MySecurity.AES.Encrypt(MSISDN, MySetting.AdminSetting.RegWSKey);

                MyService.ActionSoapClient mClient = new MyService.ActionSoapClient();
                string Signature = MSISDN + "|HBWeb|" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                Signature = MySecurity.AES.Encrypt(Signature, MySetting.AdminSetting.RegWSKey);
                System.Net.ServicePointManager.Expect100Continue = false;
                string Result_Check = mClient.Check(Signature);
                string[] Arr_Result_Check = Result_Check.Split('|');

                //Nếu đã từng sử dụng dịch vụ thì chuyển sang các trang thể thao 
                if (Arr_Result_Check[0].Equals("2"))
                {
                    LoadReg_Exist mReg_Exist = new LoadReg_Exist(MSISDN);
                    Write(mReg_Exist.GetHTML());
                }
                else
                {
                    LoadReg_NotExist mReg_NotExist = new LoadReg_NotExist(MSISDN);
                    Write(mReg_NotExist.GetHTML());
                }

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