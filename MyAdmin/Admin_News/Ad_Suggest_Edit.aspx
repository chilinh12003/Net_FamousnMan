<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_Suggest_Edit.aspx.cs" Inherits="MyAdmin.Admin_News.Ad_Suggest_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="server">
    <link href="../Calendar/dhtmlgoodies_calendar/dhtmlgoodies_calendar.css" rel="stylesheet" type="text/css" />
    <script src="../Calendar/dhtmlgoodies_calendar/dhtmlgoodies_calendar.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Tools" runat="server">
    <a href='Ad_Suggest.aspx?QID=<%=this.QuestionID %>' id="link_Cancel"><span class="Cancel"></span>Hủy </a>
    <asp:LinkButton runat="server" ID="lbtn_Save" OnClick="lbtn_Save_Click" OnClientClick="return CheckAll();">
     <span class="Save"></span>
            Lưu
    </asp:LinkButton>
    <asp:LinkButton runat="server" ID="lbtn_Accept" OnClick="lbtn_Apply_Click" OnClientClick="return CheckAll();">
     <span class="Accept"></span>
            Apply
    </asp:LinkButton>
     <asp:LinkButton runat="server" ID="link_Add" ToolTip="Thêm" OnClick="lbtn_Add_Click">
        <span class="Add"></span>
       Thêm
    </asp:LinkButton>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_ToolBox" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Search" runat="server">
    <div class="Edit_Left">
        <div class="NewLine">
            <div class="Edit_Title" style="height: 50px;">
                Nội dung:</div>
            <div class="Edit_Control_Editor">
                <textarea id="tbx_MT" runat="server" style="float: left; height: 50px; width: 99%;"></textarea>
            </div>
        </div>
        <div class="NewLine">
            <div class="Edit_Title" style="height: 50px;">
                Notify:</div>
            <div class="Edit_Control_Editor">
                <textarea id="tbx_NotifyMT" runat="server" style="float: left; height: 50px; width: 99%;"></textarea>
            </div>
        </div>
        <div class="Edit_Title">
            Thứ tự:</div>
        <div class="Edit_Control">
            <input type="text" runat="server" id="tbx_OrderNumber" value="1" onkeypress="return isNumberKey_int(event);" />
        </div>
        <div class="Edit_Title">
            Kích hoạt:</div>
        <div class="Edit_Control">
            <input type="checkbox" runat="server" checked="checked" id="chk_Active" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_Content" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_Javascript" runat="server">
    <script language="javascript" type="text/javascript">
            

    </script>
</asp:Content>
