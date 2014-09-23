<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_Question_Edit.aspx.cs" Inherits="MyAdmin.Admin_News.Ad_Question_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="server">
    <link href="../Calendar/dhtmlgoodies_calendar/dhtmlgoodies_calendar.css" rel="stylesheet" type="text/css" />
    <script src="../Calendar/dhtmlgoodies_calendar/dhtmlgoodies_calendar.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Tools" runat="server">
    <a href="Ad_Question.aspx" runat="server" id="link_Cancel"><span class="Cancel"></span>Hủy </a>
    <asp:LinkButton runat="server" ID="lbtn_Save" OnClick="lbtn_Save_Click" OnClientClick="return CheckAll();">
     <span class="Save"></span>
            Lưu
    </asp:LinkButton>
    <asp:LinkButton runat="server" ID="lbtn_Accept" OnClick="lbtn_Apply_Click" OnClientClick="return CheckAll();">
     <span class="Accept"></span>
            Apply
    </asp:LinkButton>
    <a href="Ad_Question_Edit.aspx" runat="server" id="link_Add"><span class="Add"></span>Thêm </a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_ToolBox" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Search" runat="server">
    <div class="Edit_Left">
        <div class="Edit_Title" style="height: 40px;">
            Ngày chơi</div>
        <div class="Edit_Control" style="height: 50px;">
            <input type="text" runat="server" id="tbx_PlayDate" style="width: 70px;" />
            <input type="button" value="..." onclick="displayCalendar(document.getElementById('<%=tbx_PlayDate.ClientID %>'),'dd/mm/yyyy',this)" />
        </div>
        <div class="Edit_Title">
            Tiêu đề
        </div>
        <div class="Edit_Control">
            <input type="text" runat="server" id="tbx_QuestionName" style="width: 99%;" />
        </div>
        <div class="Edit_Title" >
            Câu trả lời:</div>
        <div class="Edit_Control">
            <input type="text" runat="server" id="tbx_RightAnswer" style="width: 99%;" />
        </div>
        <div class="Edit_Title" >
            Giải thưởng:</div>
        <div class="Edit_Control">
            <input type="text" runat="server" id="tbx_Prize" style="width: 99%;" />
        </div>
         <div class="Edit_Title" >
            Giá trị Giải thưởng:</div>
        <div class="Edit_Control">
            <input type="text" runat="server" id="tbx_Price" style="width: 99%;" />
        </div>
    </div>
    <div class="Edit_Right">
        <div class="Properties_Header">
            <div class="Properties_Header_In">
                Thông tin chi tiết khác
            </div>
        </div>
        <div class="Properties">
            <div class="Properties_Title">
                Tình trạng:</div>
            <div class="Properties_Control">
                <select runat="server" id="sel_Status">
                </select>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_Content" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_Javascript" runat="server">
    <script language="javascript" type="text/javascript">
            

    </script>
</asp:Content>
