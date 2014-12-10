using System;
using System.Collections.Generic;
using System.Web;
using MyLoad.LoadStatic;
using MyUtility;
using System.Text;
using MyBase.MyWeb;
using MyLoad.LoadService;

namespace MyWeb.vnp
{
    /// <summary>
    /// Summary description for RedirectVNP
    /// </summary>
    public class RedirectVNP : MyASHXBase
    {
        /// <summary>
        /// ActionType: 1--> Đăng ký, 2 --> Hủy
        /// </summary>
        int aid = 1;

        public override void WriteHTML()
        {
            try
            {
                if (Request.QueryString["aid"] != null)
                {
                    int.TryParse(Request.QueryString["aid"], out aid);
                }

                LoadHeader mHeader = new LoadHeader();
                mHeader.Title = "Nhận diện người nối tiếng";
                Write(mHeader.GetHTML());


                if (aid == 1)
                {
                    MyCurrent.CurrentPage.Response.Redirect(BuildLink_Reg(MySetting.WebSetting.PackageName), false);
                    return;
                }
                else if (aid == 2)
                {
                    MyCurrent.CurrentPage.Response.Redirect(BuildLink_DeReg(MySetting.WebSetting.PackageName), false);
                    return;
                }
                else
                {
                    LoadReg_Note mNote_1 = new LoadReg_Note("Thông tin không hợp lệ, xin vui lòng kiểm tra lại.");
                    Write(mNote_1.GetHTML());
                }
                
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


        private string VNPLink_Reg
        {
            get
            {
                string temp = MyConfig.GetKeyInConfigFile("VNPLink_Reg");
                if (string.IsNullOrEmpty(temp))
                {
                    return "http://vinaphone.com.vn/vasgate/reg.jsp";
                }
                else return temp;
            }
        }
        private string VNPLink_Dereg
        {
            get
            {
                string temp = MyConfig.GetKeyInConfigFile("VNPLink_Dereg");
                if (string.IsNullOrEmpty(temp))
                {
                    return "http://vinaphone.com.vn/vasgate/unreg.jsp";
                }
                else return temp;
            }
        }
        private string VNPCPName
        {
            get
            {
                string temp = MyConfig.GetKeyInConfigFile("VNPCPName");
                if (string.IsNullOrEmpty(temp))
                {
                    return "HBCOM";
                }
                else return temp;
            }
        }

        private string VNPSecurepass
        {
            get
            {
                string temp = MyConfig.GetKeyInConfigFile("VNPSecurepass");
                if (string.IsNullOrEmpty(temp))
                {
                    return "hbcom!u8Ag7";
                }
                else return temp;
            }
        }
        private string VNPService
        {
            get
            {
                string temp = MyConfig.GetKeyInConfigFile("VNPService");
                if (string.IsNullOrEmpty(temp))
                {
                    return "NGUOINOITIENG";
                }
                else return temp;
            }
        }

        private string BuildLink_Reg(string PackageName)
        {
            string requestid = string.Empty;
            string returnurl = string.Empty;
            string backurl = string.Empty;
            string cp = string.Empty;
            string service = string.Empty;
            string package = string.Empty;
            string requestdatetime = string.Empty;
            string channel = string.Empty;
            string securecode = string.Empty;

            string URL = string.Empty;
            string URL_Encode = string.Empty;
            try
            {
                requestid = MySecurity.CreateCode(9);
                returnurl = MyConfig.Domain;
                returnurl = returnurl.ToLower();
                backurl = MyConfig.Domain;
                backurl = backurl.ToLower();
                cp = VNPCPName;
                service = VNPService;
                package = PackageName;
                requestdatetime = DateTime.Now.ToString("yyyyMMddHHmmss");
                channel = "web";
                securecode = GetMD5Hash(requestid + returnurl + backurl + cp +
                                        service + package + requestdatetime + channel +
                                        VNPSecurepass);

                URL = VNPLink_Reg + "?requestid={0}&returnurl={1}&backurl={2}&cp={3}&service={4}&package={5}&requestdatetime={6}&channel={7}&securecode={8}";
                URL_Encode = string.Format(URL, requestid, HttpUtility.UrlEncode(returnurl),
                                                    HttpUtility.UrlEncode(backurl), cp, service, package, requestdatetime, channel,
                                                    HttpUtility.UrlEncode(securecode));

                return URL_Encode;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                mLog.Debug("Redirect VNP", "URL:" + URL_Encode);
            }
        }

        private string BuildLink_DeReg(string PackageName)
        {
            string requestid = string.Empty;
            string returnurl = string.Empty;
            string backurl = string.Empty;
            string cp = string.Empty;
            string service = string.Empty;
            string package = string.Empty;
            string requestdatetime = string.Empty;
            string channel = string.Empty;
            string securecode = string.Empty;

            string URL = string.Empty;
            string URL_Encode = string.Empty;
            try
            {
                requestid = MySecurity.CreateCode(9);
                returnurl = MyConfig.Domain;
                returnurl = returnurl.ToLower();
                backurl = MyConfig.Domain;
                backurl = backurl.ToLower();
                cp = VNPCPName;
                service = VNPService;
                package = PackageName;
                requestdatetime = DateTime.Now.ToString("yyyyMMddHHmmss");
                channel = "web";
                securecode = GetMD5Hash(requestid + returnurl + backurl + cp +
                                        service + package + requestdatetime + channel +
                                        VNPSecurepass);

                URL = VNPLink_Dereg + "?requestid={0}&returnurl={1}&backurl={2}&cp={3}&service={4}&package={5}&requestdatetime={6}&channel={7}&securecode={8}";
                URL_Encode = string.Format(URL, requestid, HttpUtility.UrlEncode(returnurl),
                                                     HttpUtility.UrlEncode(backurl), cp, service, package, requestdatetime, channel,
                                                     HttpUtility.UrlEncode(securecode));

                return URL_Encode;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                mLog.Debug("Redirect VNP", "URL:" + URL_Encode);
            }
        }
        public String GetMD5Hash(String input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new
            System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            String md5String = s.ToString();
            return md5String;
        }
    }
}