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
    /// Summary description for reg
    /// </summary>
    public class reg : MyWapBase
    {

        public override void WriteHTML()
        {
            try
            {
                if (string.IsNullOrEmpty(MSISDN))
                {
                    vnp.GetMSISDN mVNPGet = new vnp.GetMSISDN();
                    MSISDN = mVNPGet.GetMSISDN_VNP();
                }

                if (string.IsNullOrEmpty(MSISDN))
                {
                    if (Request.QueryString["para"] == null)
                    {
                        Response.Redirect(MyConfig.Domain + "/VNPLogin.aspx");
                        return;
                    }

                    string Para = Request.QueryString["para"];
                    if (!string.IsNullOrEmpty(Para))
                    {
                        string Para_Decode = MySecurity.AES.Decrypt(Para, MySetting.AdminSetting.SpecialKey);
                        MSISDN = string.Empty;

                        if (!string.IsNullOrEmpty(Para_Decode))
                        {
                            string[] arr = Para_Decode.Split('|');
                            if (arr.Length == 2)
                            {
                                MSISDN = arr[0];
                            }
                        }
                    }
                }

                MyLoadHeader mHeader = new MyLoadHeader(MSISDN);
                mHeader.Title = "Nhận diện người nổi tiếng";
                Write(mHeader.GetHTML());

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
                string Result_Check = mClient.Check(Signature);
                string[] Arr_Result_Check = Result_Check.Split('|');

                //Thuê bao đã đăng ký trước đó
                if (Arr_Result_Check[0].Equals("2"))
                {
                    MyLoadReg_Exist mReg_Exist = new MyLoadReg_Exist(MSISDN);
                    Write(mReg_Exist.GetHTML());
                }
                else
                {
                    MyLoadReg_NotExist mReg_NotExist = new MyLoadReg_NotExist(MSISDN);
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
                MyLoadFooter mFooter = new MyLoadFooter();
                Write(mFooter.GetHTML());
            }
        }
    }
}