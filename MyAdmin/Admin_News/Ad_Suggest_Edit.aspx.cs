using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using MyUtility;
using MyFamousMan;
using MyFamousMan.News;
using MyFamousMan.Service;
namespace MyAdmin.Admin_News
{
    public partial class Ad_Suggest_Edit : System.Web.UI.Page
    {
        public GetRole mGetRole;
        Suggest mSuggest = new Suggest();
        int EditID = 0;
        public int QuestionID = 0;

        public string ParentPath = "../Admin_News/Ad_Suggest.aspx";

        /// <summary>
        /// Lưu dữ liêu service mỗi lần thay đổi select index của ddl_service
        /// </summary>
        public DataTable SaveService
        {
            get
            {
                if (ViewState["SaveService"] == null)
                    return null;
                else
                    return (DataTable)ViewState["SaveService"];

            }
            set
            {
                ViewState["SaveService"] = value;
            }

        }
        private void BindCombo(int type)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                if (EditID > 0)
                {
                    lbtn_Save.Visible = lbtn_Accept.Visible = mGetRole.EditRole;
                    link_Add.Visible = mGetRole.AddRole;
                }
                else
                {
                    lbtn_Save.Visible = lbtn_Accept.Visible = link_Add.Visible = mGetRole.AddRole;
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
                    mGetRole = new GetRole(MySetting.AdminSetting.ListPage.Suggest, Member.MemberGroupID());
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

                //Lấy memberID nếu là trước hợp Sửa
                EditID = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

                QuestionID = Request.QueryString["QID"] == null ? 0 : int.Parse(Request.QueryString["QID"]);

                ParentPath = ParentPath + "?QID=" + QuestionID.ToString();
                MyAdmin.MasterPages.Admin mMaster = (MyAdmin.MasterPages.Admin)Page.Master;
                mMaster.str_PageTitle = mGetRole.PageName;
                mMaster.str_TitleSearchBox = "Thông tin về " + mGetRole.PageName;

                if (!IsPostBack)
                {
                    //Nếu là Edit
                    if (EditID > 0)
                    {
                        DataTable mTable = mSuggest.Select(1, EditID.ToString());

                        //Lưu lại thông tin OldData để lưu vào MemberLog
                        ViewState["OldData"] = MyXML.GetXML(mTable);

                        if (mTable != null && mTable.Rows.Count > 0)
                        {
                            #region MyRegion
                            DataRow mRow = mTable.Rows[0];

                            tbx_MT.Value = mRow["MT"].ToString();
                            tbx_NotifyMT.Value = mRow["NotifyMT"].ToString();

                            tbx_OrderNumber.Value = mRow["OrderNumber"].ToString();
                            chk_Active.Checked = (bool)mRow["IsActive"];
                            #endregion
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }

        }
        private void AddNewRow(ref DataSet mSet)
        {
            MyConvert.ConvertDateColumnToStringColumn(ref mSet);
            DataRow mNewRow = mSet.Tables["Child"].NewRow();

            if (EditID > 0)
                mNewRow["SuggestID"] = EditID;

            mNewRow["QuestionID"] = QuestionID;

            mNewRow["MT"] = tbx_MT.Value;
            mNewRow["NotifyMT"] = tbx_NotifyMT.Value;
            mNewRow["CreateDate"] = DateTime.Now.ToString(MyConfig.DateFormat_InsertToDB);

            int OrderNumber = 0;
            if (int.TryParse(tbx_OrderNumber.Value, out OrderNumber))
            {
                mNewRow["OrderNumber"] = OrderNumber;
            }
            mNewRow["IsActive"] = chk_Active.Checked;
            mSet.Tables["Child"].Rows.Add(mNewRow);

            mSet.AcceptChanges();
        }

        private void Save(bool IsApply)
        {
            try
            {

                DataSet mSet = mSuggest.CreateDataSet();
                AddNewRow(ref mSet);
                //Nếu là Edit
                if (EditID > 0)
                {
                    if (mSuggest.Update(0, mSet.GetXml()))
                    {
                        #region Log member
                        MemberLog mLog = new MemberLog();
                        MemberLog.ActionType Action = MemberLog.ActionType.Update;
                        mLog.Insert("Suggest", ViewState["OldData"].ToString(), mSet.GetXml(), Action, true, string.Empty);
                        #endregion

                        if (IsApply)
                            MyMessage.ShowMessage("Cập nhật dữ liệu thành công.");
                        else
                        {
                            Response.Redirect(ParentPath, false);
                        }
                    }
                    else
                    {
                        MyMessage.ShowMessage("Cập nhật dữ liệu (KHÔNG) thành công!");
                    }
                }
                else
                {
                    if (mSuggest.Insert(0, mSet.GetXml()))
                    {
                        #region Log member
                        MemberLog mLog = new MemberLog();
                        MemberLog.ActionType Action = MemberLog.ActionType.Insert;
                        mLog.Insert("Suggest", string.Empty, mSet.GetXml(), Action, true, string.Empty);
                        #endregion

                        if (IsApply)
                            MyMessage.ShowMessage("Cập nhật dữ liệu thành công.");
                        else
                        {
                            Response.Redirect(ParentPath, false);
                        }
                    }
                    else
                    {
                        MyMessage.ShowMessage("Cập nhật dữ liệu (KHÔNG) thành công!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                Save(false);
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.SaveDataError, "Chilinh");
            }
        }

        protected void lbtn_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                Save(true);
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.SaveDataError, "Chilinh");
            }
        }

        protected void lbtn_Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ad_Suggest_Edit.aspx?QID=" + QuestionID.ToString());
        }

    }
}
