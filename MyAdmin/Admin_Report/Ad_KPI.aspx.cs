﻿using System;
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

using System.ComponentModel;
namespace MyAdmin.Admin_Report
{
    public partial class Ad_KPI : System.Web.UI.Page
    {
        public enum KPIType
        {
            [DescriptionAttribute("KPI MO")]
            MO = 1,
            [DescriptionAttribute("KPI MT")]
            MT = 2,
            [DescriptionAttribute("KPI Charge")]
            Charge = 3,

        }

        public GetRole mGetRole;
        public int PageIndex = 1;
        ChargeLog mChargeLog = new ChargeLog();

        private void BindCombo(int type)
        {
            try
            {
                switch (type)
                {
                    case 1:
                        sel_KPIType.DataSource = MyEnum.CrateDatasourceFromEnum(typeof(KPIType), true);
                          sel_KPIType.DataTextField = "Text";
                          sel_KPIType.DataValueField = "ID";
                          sel_KPIType.DataBind();
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
                    mGetRole = new GetRole(MySetting.AdminSetting.ListPage.KPI, Member.MemberGroupID());
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
                    BindCombo(1);
                    BindCombo(2);
                    tbx_FromDate.Value = MyConfig.StartDayOfMonth.ToString(MyConfig.ShortDateFormat);
                    tbx_ToDate.Value = DateTime.Now.ToString(MyConfig.ShortDateFormat);

                    
                }

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }
        }

        public int Total = 0;
        public int TotalSuccess = 0;
        public double percent = 0;

        public int TotalCharge_Reg = 0;
        public int TotalCharge_Reg_Success = 0;
        public double Percent_Charge_Reg = 0;

        public int TotalCharge_Renew = 0;
        public int TotalCharge_Renew_Success = 0;
        public double Percent_Charge_Renew = 0;

        public int TotalCharge = 0;
        public int TotalCharge_Success = 0;
        public double Percent_Charge = 0;

        public int TotalCharge_UnReg = 0;
        public int TotalCharge_UnReg_Success = 0;
        public double Percent_Charge_UnReg = 0;

        public int TotalCharge_BuyContent = 0;
        public int TotalCharge_BuyContent_Success = 0;
        public double Percent_Charge_BuyContent = 0;

        protected void btn_Execute_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime BeginDate = tbx_FromDate.Value.Length > 0 ? DateTime.ParseExact(tbx_FromDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                DateTime EndDate = tbx_ToDate.Value.Length > 0 ? DateTime.ParseExact(tbx_ToDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                EndDate = EndDate.AddHours(23);
                EndDate = EndDate.AddMinutes(59);
                EndDate = EndDate.AddSeconds(59);

                if (BeginDate.Month < 8)
                {
                    MyMessage.ShowError("Chọn Tháng không hợp lệ, Chương trình bắt đầu chạy từ tháng 08/2013.");
                    return;
                }
                if (BeginDate.Month != EndDate.Month)
                {
                    MyMessage.ShowError("Ngày bắt đầu và Ngày kết thúc phải trong cùng 1 tháng");
                    return;
                }

                MyFamousMan.Gateway.KPI mKPI = new MyFamousMan.Gateway.KPI(MySetting.AdminSetting.MySQLConnection_Gateway);

                div_MO.Visible = false;
                div_MT.Visible = false;
                div_Charge.Visible = false;

                if (int.Parse(sel_KPIType.Value) == (int)KPIType.MO)
                {
                    Total = mKPI.GetTotalMO(BeginDate.ToString("yyyyMM"), BeginDate.ToString(MyConfig.DateFormat_InsertToDB), EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                    TotalSuccess = mKPI.GetTotalMOSuccess(BeginDate.ToString("yyyyMM"), BeginDate.ToString(MyConfig.DateFormat_InsertToDB), EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                    div_MO.Visible = true;
                }
                else if (int.Parse(sel_KPIType.Value) == (int)KPIType.MT)
                {
                    Total = mKPI.GetTotalMT(BeginDate.ToString("yyyyMM"), BeginDate.ToString(MyConfig.DateFormat_InsertToDB), EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                    //Total = mKPI.GetTotalMTSuccess(BeginDate.ToString("yyyyMM"), BeginDate.ToString(MyConfig.DateFormat_InsertToDB), EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                    TotalSuccess = mKPI.GetTotalMTSuccess(BeginDate.ToString("yyyyMM"), BeginDate.ToString(MyConfig.DateFormat_InsertToDB), EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                    div_MT.Visible = true;
                }
                else if (int.Parse(sel_KPIType.Value) == (int)KPIType.Charge)
                {
                    TotalCharge_Reg = mChargeLog.GetTotal(ChargeLog.ChargeType.REG, BeginDate, EndDate);
                    TotalCharge_Reg_Success = mChargeLog.GetTotal(ChargeLog.ChargeStatus.ChargeSuccess, ChargeLog.ChargeType.REG, BeginDate, EndDate);

                    TotalCharge_Renew = mChargeLog.GetTotal(ChargeLog.ChargeType.RENEW, BeginDate, EndDate);
                    TotalCharge_Renew_Success = mChargeLog.GetTotal(ChargeLog.ChargeStatus.ChargeSuccess, ChargeLog.ChargeType.RENEW, BeginDate, EndDate);

                    TotalCharge_UnReg = mChargeLog.GetTotal(ChargeLog.ChargeType.UNREG, BeginDate, EndDate);
                    TotalCharge_UnReg_Success = mChargeLog.GetTotal(ChargeLog.ChargeStatus.ChargeSuccess, ChargeLog.ChargeType.UNREG, BeginDate, EndDate);

                    TotalCharge_BuyContent = mChargeLog.GetTotal(ChargeLog.ChargeType.CONTENT, BeginDate, EndDate);
                    TotalCharge_BuyContent_Success = mChargeLog.GetTotal(ChargeLog.ChargeStatus.ChargeSuccess, ChargeLog.ChargeType.CONTENT, BeginDate, EndDate);

                    TotalCharge = mChargeLog.GetTotal( BeginDate, EndDate);
                    TotalCharge_Success = mChargeLog.GetTotal(ChargeLog.ChargeStatus.ChargeSuccess, BeginDate, EndDate);
                    
                    div_Charge.Visible = true;
                }
                if (Total > 0 && TotalSuccess > 0)
                {
                    percent = (double)TotalSuccess / (double)Total*100;
                }
                if (TotalCharge > 0 && TotalCharge_Success > 0)
                {
                    Percent_Charge = (double)TotalCharge_Success / (double)TotalCharge * 100;
                }
                if (TotalCharge_Reg > 0 && TotalCharge_Reg_Success > 0)
                {
                    Percent_Charge_Reg = (double)TotalCharge_Reg_Success / (double)TotalCharge_Reg * 100;
                }

                if (TotalCharge_UnReg > 0 && TotalCharge_UnReg_Success > 0)
                {
                    Percent_Charge_UnReg = (double)TotalCharge_UnReg_Success / (double)TotalCharge_UnReg * 100;
                }

                if (TotalCharge_Renew > 0 && TotalCharge_Renew_Success > 0)
                {
                    Percent_Charge_Renew = (double)TotalCharge_Renew_Success / (double)TotalCharge_Renew * 100;
                }

                if (TotalCharge_BuyContent > 0 && TotalCharge_BuyContent_Success > 0)
                {
                    Percent_Charge_BuyContent = (double)TotalCharge_BuyContent_Success / (double)TotalCharge_BuyContent * 100;
                }

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }

        }
    }
}