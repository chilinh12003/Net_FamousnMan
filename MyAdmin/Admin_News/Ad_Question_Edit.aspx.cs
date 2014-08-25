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
    public partial class Ad_Question_Edit : System.Web.UI.Page
    {
        public GetRole mGetRole;
        Question mQuestion = new Question();
        int EditID = 0;

        public string ParentPath = "../Admin_News/Ad_Question.aspx";

        private void BindCombo(int type)
        {
            try
            {
                Category mCate = new Category();

                switch (type)
                {

                    case 1:
                        sel_Status.DataSource = MyEnum.CrateDatasourceFromEnum(typeof(Question.Status), true);
                        sel_Status.DataTextField = "Text";
                        sel_Status.DataValueField = "ID";
                        sel_Status.DataBind();
                        break;
                }
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
                    mGetRole = new GetRole(MySetting.AdminSetting.ListPage.Question, Member.MemberGroupID());
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

                MyAdmin.MasterPages.Admin mMaster = (MyAdmin.MasterPages.Admin)Page.Master;
                mMaster.str_PageTitle = mGetRole.PageName;
                mMaster.str_TitleSearchBox = "Thông tin về " + mGetRole.PageName;

                if (!IsPostBack)
                {

                    BindCombo(1);

                    tbx_PlayDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    //Nếu là Edit
                    if (EditID > 0)
                    {
                        DataTable mTable = mQuestion.Select(1, EditID.ToString());

                        //Lưu lại thông tin OldData để lưu vào MemberLog
                        ViewState["OldData"] = MyXML.GetXML(mTable);

                        if (mTable != null && mTable.Rows.Count > 0)
                        {
                            #region MyRegion
                            DataRow mRow = mTable.Rows[0];

                            if (mRow["StatusID"] != DBNull.Value)
                                sel_Status.SelectedIndex = sel_Status.Items.IndexOf(sel_Status.Items.FindByValue(mRow["StatusID"].ToString()));


                            tbx_QuestionName.Value = mRow["QuestionName"].ToString();
                            tbx_RightAnswer.Value = mRow["RightAnswer"].ToString();

                            tbx_Prize.Value = mRow["Prize"].ToString();
                            tbx_Price.Value = mRow["Price"].ToString();

                            if (mRow["PlayDate"] != DBNull.Value)
                            {
                                DateTime mDateTime = (DateTime)mRow["PlayDate"];
                                tbx_PlayDate.Value = mDateTime.ToString(MyConfig.ShortDateFormat);
                            }

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
                mNewRow["QuestionID"] = EditID;

            mNewRow["QuestionName"] = tbx_QuestionName.Value;
            mNewRow["RightAnswer"] = tbx_RightAnswer.Value;
            mNewRow["CreateDate"] = DateTime.Now.ToString(MyConfig.DateFormat_InsertToDB);


            mNewRow["Prize"] = tbx_Prize.Value;
            mNewRow["Price"] = tbx_Price.Value;


            if (sel_Status.SelectedIndex >= 0 && sel_Status.Items.Count > 0)
            {
                mNewRow["StatusID"] = int.Parse(sel_Status.Value);
                mNewRow["StatusName"] = sel_Status.Items[sel_Status.SelectedIndex].Text;
            }

            if (tbx_PlayDate.Value.Length > 0)
            {
                int Hour = 0;
                int Minute = 0;
                int Second = 0;
                DateTime TempDate = DateTime.ParseExact(tbx_PlayDate.Value, "dd/MM/yyyy", null);
                mNewRow["PlayDate"] = new DateTime(TempDate.Year, TempDate.Month, TempDate.Day, Hour, Minute, Second).ToString(MyConfig.DateFormat_InsertToDB);
            }


            mSet.Tables["Child"].Rows.Add(mNewRow);

            mSet.AcceptChanges();
        }

        private void Save(bool IsApply)
        {
            try
            {
                DataSet mSet = mQuestion.CreateDataSet();
                AddNewRow(ref mSet);
                //Nếu là Edit
                if (EditID > 0)
                {
                    if (mQuestion.Update(0, mSet.GetXml()))
                    {
                        #region Log member
                        MemberLog mLog = new MemberLog();
                        MemberLog.ActionType Action = MemberLog.ActionType.Update;
                        mLog.Insert("Question", ViewState["OldData"].ToString(), mSet.GetXml(), Action, true, string.Empty);
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
                    if (mQuestion.Insert(0, mSet.GetXml()))
                    {
                        #region Log member
                        MemberLog mLog = new MemberLog();
                        MemberLog.ActionType Action = MemberLog.ActionType.Insert;
                        mLog.Insert("Question", string.Empty, mSet.GetXml(), Action, true, string.Empty);
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


    }
}
