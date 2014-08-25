using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MyUtility;
using MyFamousMan;
using MyFamousMan.Service;
using MyFamousMan.Sub;

namespace MyAdmin.Admin_CCare
{
    public class SubInfo
    {
        public string MSISDN = "NULL";
        public string FirstDate = "NULL";
        public string EffectiveDate = "NULL";
        public string ExpiryDate = "NULL";
        public string RetryChargeDate = "NULL";
        public string RetryChargeCount = "NULL";
        public string RenewChargeDate = "NULL";
        public string ChannelTypeName = "NULL";
        public string StatusName = "NULL";
        public string PID = "NULL";

        public string SuggestByDay = "NULL";
        public string TotalSuggest = "NULL";
        public string LastSuggestDate = "NULL";

        public string LastAnswer = "NULL";
        public string AnswerByDay = "NULL";
        public string LastAnswerDate = "NULL";
        public string AnswerStatusName = "NULL";

        public string AppID = "NULL";
        public string AppName = "NULL";
        public string UserName = "NULL";
        public string IP = "NULL";
        public string PartnerID = "NULL";
        public string DeregDate = "NULL";

        public SubInfo()
        {

        }
        public SubInfo(DataTable mTable)
        {
            if (mTable == null || mTable.Rows.Count < 1)
            {
                StatusName = MyEnum.StringValueOf(Subscriber.Status.NeverReg);
                return;
            }

            DataRow mRow = mTable.Rows[0];
           
            FirstDate = mRow["FirstDate"] != DBNull.Value ?((DateTime)mRow["FirstDate"]).ToString(MyConfig.LongDateFormat) : "NULL";
            EffectiveDate =  mRow["EffectiveDate"] != DBNull.Value ?((DateTime)mRow["EffectiveDate"]).ToString(MyConfig.LongDateFormat) : "NULL";
            ExpiryDate =  mRow["ExpiryDate"] != DBNull.Value ?((DateTime)mRow["ExpiryDate"]).ToString(MyConfig.LongDateFormat) : "NULL";
            RetryChargeDate =  mRow["RetryChargeDate"] != DBNull.Value ? ((DateTime)mRow["RetryChargeDate"]).ToString(MyConfig.LongDateFormat) : "NULL";
            RetryChargeCount = mRow["RetryChargeCount"] != DBNull.Value ?((int)mRow["RetryChargeCount"]).ToString(MyConfig.IntFormat) : "NULL";
            RenewChargeDate = mRow["RenewChargeDate"] != DBNull.Value ?((DateTime)mRow["RenewChargeDate"]).ToString(MyConfig.LongDateFormat) : "NULL";
            
            ChannelTypeName = mRow["ChannelTypeID"] != null ?((MyConfig.ChannelType)(int)mRow["ChannelTypeID"]).ToString() : "NULL";
            StatusName = mRow["StatusID"] != DBNull.Value ?MyEnum.StringValueOf((Subscriber.Status)(int)mRow["StatusID"]) : "NULL";
            
            SuggestByDay = mRow["SuggestByDay"] != DBNull.Value ?((int)mRow["SuggestByDay"]).ToString(MyConfig.IntFormat) : "NULL";
            TotalSuggest = mRow["TotalSuggest"] != DBNull.Value ?((int)mRow["TotalSuggest"]).ToString(MyConfig.IntFormat) : "NULL";
            LastSuggestDate = mRow["LastSuggestDate"] != DBNull.Value ?((DateTime)mRow["LastSuggestDate"]).ToString(MyConfig.LongDateFormat) : "NULL";

            LastAnswer = mRow["LastAnswer"] != DBNull.Value ?((int)mRow["LastAnswer"]).ToString(MyConfig.IntFormat) : "NULL";
            AnswerByDay = mRow["AnswerByDay"] != DBNull.Value ?((int)mRow["AnswerByDay"]).ToString(MyConfig.IntFormat) : "NULL";
            AnswerStatusName = mRow["AnswerStatusID"] != DBNull.Value ? MyEnum.StringValueOf((Play.Status)(int)mRow["AnswerStatusID"]) : "NULL";
            LastAnswerDate = mRow["LastAnswerDate"] != DBNull.Value ? ((DateTime)mRow["LastAnswerDate"]).ToString(MyConfig.LongDateFormat) : "NULL";

            AppName = mRow["AppName"] != DBNull.Value ? mRow["AppName"].ToString() == "NoThing" ? "NULL" : mRow["AppName"].ToString() : "NULL";
            UserName = mRow["UserName"] != DBNull.Value ? mRow["UserName"].ToString() : "NULL";
            IP = mRow["IP"] != DBNull.Value ?mRow["IP"].ToString() : "NULL";

            DeregDate = mRow["DeregDate"] != DBNull.Value ?((DateTime)mRow["DeregDate"]).ToString(MyConfig.LongDateFormat) : "NULL";
        }
    }
    /// <summary>
    /// Lấy tất cả thông tin của thuê bao đang sử dụng dịch vụ
    /// </summary>
    public partial class Ad_CheckSubInfo : System.Web.UI.Page
    {
        public GetRole mGetRole;
        public int PageIndex = 1;
        MOLog mMOLog = new MOLog();
        ChargeLog mChargeLog = new ChargeLog();

        string MSISDN = string.Empty;

       public SubInfo mSubInfo = new SubInfo();
        private bool CheckPermission()
        {
            try
            {
                if (mGetRole.ViewRole == false)
                {
                    Response.Redirect(mGetRole.URLNotView, false);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.CheckPermissionError, "Chilinh");
                return false;
            }
            return true;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            bool IsRedirect = false;
            try
            {
                //Phân quyền
                if (ViewState["Role"] == null)
                {
                    mGetRole = new GetRole(MySetting.AdminSetting.ListPage.CheckDetailInfo, Member.MemberGroupID());
                }
                else
                {
                    mGetRole = (GetRole)ViewState["Role"];
                }

                if (!CheckPermission())
                {
                    IsRedirect = true;
                }
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }
            if (IsRedirect)
            {
                Response.End();
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MyAdmin.MasterPages.Admin mMaster = (MyAdmin.MasterPages.Admin)Page.Master;
                mMaster.str_PageTitle = mGetRole.PageName;

                MSISDN = tbx_MSISDN.Value;

                if (!IsPostBack)
                {
                  
                }
             
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }
        }

        protected void tbx_Search_Click(object sender, EventArgs e)
        {
            try
            {
                MSISDN = tbx_MSISDN.Value;
                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                if (string.IsNullOrEmpty(MSISDN) || !MyCheck.CheckPhoneNumber(ref MSISDN, ref mTelco, "84") || mTelco != MyConfig.Telco.Vinaphone)
                {
                    MyMessage.ShowError("Số điện thoại không chính xác, xin vui lòng kiểm tra lại.");
                    return;
                }
                tbx_MSISDN.Value = MSISDN;

                int PID = MyPID.GetPIDByPhoneNumber(MSISDN, MySetting.AdminSetting.MaxPID);

                Subscriber mSub = new Subscriber();
                UnSubscriber mUnSub = new UnSubscriber();
                DataTable mTable = mSub.Select(2, PID.ToString(), MSISDN);

                if(mTable.Rows.Count < 1)
                    mTable = mUnSub.Select(2, PID.ToString(), MSISDN);

                mSubInfo = new SubInfo(mTable);
                mSubInfo.MSISDN = MSISDN;
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }
        }
    }
}