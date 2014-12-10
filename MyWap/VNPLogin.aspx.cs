using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyUtility;
using System.IO;
using System.Net;
using System.Web.Security;
using System.Xml;

using DotNetCasClient;
using DotNetCasClient.Utils;
using DotNetCasClient.Validation;
using System.Data;
using MyBase.MyWeb;
namespace MyWap
{
    public partial class VNPLogin : MyASPXBase
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    Response.Redirect(MyConfig.Domain + "/page/home.ashx", false);
        //}

        // Local specific CAS host       
        private const string CASHOST = "https://vinaphone.com.vn/auth/";
        // After the page has been loaded, this routine is called.
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Look for the "ticket=" after the "?" in the URL
                string tkt = Request["ticket"];
                if (string.IsNullOrEmpty(tkt))
                    tkt = Request.QueryString["ticket"];

                string service = MyConfig.Domain + "/VNPLogin.aspx";
             

                if (tkt == null || tkt.Length == 0)
                {
                    string redir = CASHOST + "login?" +
                      "service=" + service;
                    Response.Redirect(redir);
                    return;
                }

                mLog.Debug("SSO", "ticket:" + tkt);

                // Second time (back from CAS) there is a ticket= to validate
                string validateurl = CASHOST + "serviceValidate?" +
                  "ticket=" + tkt + "&" +
                  "service=" + service;
                StreamReader Reader = new StreamReader(new WebClient().OpenRead(validateurl));

                string Response_SSO = Reader.ReadToEnd();
                mLog.Debug("SSO", "Response_SSO:" + Response_SSO);
                DataSet mSet = MyXML.GetDataSetFromXMLString(Response_SSO);

                string MSISDN = string.Empty;

                if (mSet != null && mSet.Tables.Count > 0 && mSet.Tables[0].Rows.Count > 0)
                {
                    MSISDN = mSet.Tables[0].Rows[0][0].ToString();
                }

                string Para = MSISDN + "|" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string Para_Encode = MySecurity.AES.Encrypt(Para, MySetting.AdminSetting.SpecialKey);

                Response.Redirect(MyConfig.Domain + "/Page/Reg.html?para=" + HttpUtility.UrlEncode(Para_Encode));
            }
            catch (Exception ex)
            {
                mLog.Error(ex);
            }
        }
    }
}