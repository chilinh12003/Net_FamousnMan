using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MyUtility;
using MyFamousMan;
using MyFamousMan.News;
using MyFamousMan.Service;

namespace MyAdmin.Admin_Service
{
    public partial class Ad_Play : System.Web.UI.Page
    {
        public GetRole mGetRole;
        public int PageIndex = 1;

        Play mPlay = new Play();

        private void BindCombo(int type)
        {
            try
            {
                switch (type)
                {
                    case 1:
                        sel_Status.DataSource = MyEnum.CrateDatasourceFromEnum(typeof(Play.Status), true);
                        sel_Status.DataTextField = "Text";
                        sel_Status.DataValueField = "ID";
                        sel_Status.DataBind();
                        sel_Status.Items.Insert(0, new ListItem("--Tình trạng--", "0"));
                        break;
                    case 2:
                        sel_PlayType.DataSource = MyEnum.CrateDatasourceFromEnum(typeof(Play.PlayType), true);
                        sel_PlayType.DataTextField = "Text";
                        sel_PlayType.DataValueField = "ID";
                        sel_PlayType.DataBind();
                        sel_PlayType.Items.Insert(0, new ListItem("--Mua/Trả lời--", "0"));
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindData()
        {
            Admin_Paging1.ResetLoadData();
        }

        private bool CheckPermission()
        {
            try
            {
                if (mGetRole.ViewRole == false)
                {
                    Response.Redirect(mGetRole.URLNotView, false);
                    return false;
                }

                //link_Add.Visible = mGetRole.AddRole;
                //link_Edit.Visible = mGetRole.EditRole;
                //lbtn_Active.Visible = mGetRole.PublishRole;
                //lbtn_UnActive.Visible = mGetRole.PublishRole;
                //lbtn_Delete.Visible = mGetRole.DeleteRole;

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
                    mGetRole = new GetRole(MySetting.AdminSetting.ListPage.Play, Member.MemberGroupID());
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

                if (!IsPostBack)
                {
                    ViewState["SortBy"] = string.Empty;
                    BindCombo(1);
                    BindCombo(2);
                }

                Admin_Paging1.rpt_Data = rpt_Data;
                Admin_Paging1.GetData_Callback_Change += new MyAdmin.Admin_Control.Admin_Paging.GetData_Callback(Admin_Paging1_GetData_Callback_Change);
                Admin_Paging1.GetTotalPage_Callback_Change += new MyAdmin.Admin_Control.Admin_Paging.GetTotalPage_Callback(Admin_Paging1_GetTotalPage_Callback_Change);
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }
        }

        int Admin_Paging1_GetTotalPage_Callback_Change()
        {
            try
            {
                int? SearchType = null;
                string SearchContent = tbx_Search.Value;
                string SortBy = ViewState["SortBy"].ToString();
                int StatusID = 0;
                int PlayTypeID = 0;
                int QuestionID = 0;
                int SuggestID = 0;
                int PID = 0;


                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                if (mTelco == MyConfig.Telco.Vinaphone)
                {
                    PID = MyPID.GetPIDByPhoneNumber(SearchContent, MySetting.AdminSetting.MaxPID);
                    SearchType = 1;
                }


                if (sel_Status.SelectedIndex > 0)
                {
                    int.TryParse(sel_Status.Value, out StatusID);
                }
                if (sel_PlayType.SelectedIndex > 0)
                {
                    int.TryParse(sel_PlayType.Value, out PlayTypeID);
                }

                return mPlay.TotalRow(SearchType, SearchContent, PID, PlayTypeID, StatusID, QuestionID, SuggestID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        DataTable Admin_Paging1_GetData_Callback_Change()
        {
            try
            {
                int? SearchType = null;
                string SearchContent = tbx_Search.Value;
                string SortBy = ViewState["SortBy"].ToString();
                int StatusID = 0;
                int PlayTypeID = 0;
                int QuestionID = 0;
                int SuggestID = 0;
                int PID = 0;


                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                if (mTelco == MyConfig.Telco.Vinaphone)
                {
                    PID = MyPID.GetPIDByPhoneNumber(SearchContent, MySetting.AdminSetting.MaxPID);
                    SearchType = 1;
                }


                if (sel_Status.SelectedIndex > 0)
                {
                    int.TryParse(sel_Status.Value, out StatusID);
                }
                if (sel_PlayType.SelectedIndex > 0)
                {
                    int.TryParse(sel_PlayType.Value, out PlayTypeID);
                }

                PageIndex = (Admin_Paging1.mPaging.CurrentPageIndex - 1) * Admin_Paging1.mPaging.PageSize + 1;

                return mPlay.Search(SearchType, Admin_Paging1.mPaging.BeginRow, Admin_Paging1.mPaging.EndRow, SearchContent, PID, PlayTypeID, StatusID, QuestionID, SuggestID, SortBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtn_Sort_Click(object sender, EventArgs e)
        {
            try
            {
                lbtn_Sort_1.CssClass = "Sort";
                lbtn_Sort_2.CssClass = "Sort";
                lbtn_Sort_3.CssClass = "Sort";
                lbtn_Sort_4.CssClass = "Sort";
                lbtn_Sort_5.CssClass = "Sort";
                lbtn_Sort_6.CssClass = "Sort";
                //lbtn_Sort_7.CssClass = "Sort";

                LinkButton mLinkButton = (LinkButton)sender;
                ViewState["SortBy"] = mLinkButton.CommandArgument;

                if (mLinkButton.CommandArgument.IndexOf(" ASC") >= 0)
                {
                    mLinkButton.CssClass = "SortActive_Up";
                    mLinkButton.CommandArgument = mLinkButton.CommandArgument.Replace(" ASC", " DESC");
                }
                else
                {
                    mLinkButton.CssClass = "SortActive_Down";
                    mLinkButton.CommandArgument = mLinkButton.CommandArgument.Replace(" DESC", " ASC");
                }

                BindData();
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.SortError, "Chilinh");
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                BindData();
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.SeachError, "Chilinh");
            }
        }


    }
}
