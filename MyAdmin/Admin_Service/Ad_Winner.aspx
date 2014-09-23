﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_Winner.aspx.cs" Inherits="MyAdmin.Admin_Service.Ad_Winner" %>


<%@ Register Src="../Admin_Control/Admin_Paging.ascx" TagName="Admin_Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Tools" runat="server">
    <a href="javascript:void(0);" onclick="return EditData();" runat="server" id="link_Edit">
        <span class="Edit"></span>
        Sửa </a>
    <asp:LinkButton runat="server" ID="lbtn_Delete" OnClientClick="return BeforeDeteleData();"
        ToolTip="Xóa tất cả mục đã chọn" OnClick="lbtn_Delete_Click">
        <span class="Delete"></span>
            Xóa
    </asp:LinkButton>
    <a href="Ad_Member_Edit.aspx" runat="server" id="link_Add">
        <span class="Add"></span>
        Thêm </a>
    <asp:LinkButton runat="server" ID="lbtn_Active" OnClientClick="return Active();"
        ToolTip="Xóa tất cả mục đã chọn" OnClick="lbtn_Active_Click">
        <span class="Active"></span>
       K.Hoat
    </asp:LinkButton>
    <asp:LinkButton runat="server" ID="lbtn_UnActive" OnClick="lbtn_UnActive_Click" OnClientClick="return UnActive();">
        <span class="UnActive"></span>
       Hủy KH
    </asp:LinkButton>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_ToolBox" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Search" runat="server">
    <label>
        Số điện thoại:</label>
    <input type="text" runat="server" id="tbx_Search" />

    <asp:Button runat="server" ID="btn_Search" Text="Tìm kiếm" OnClick="btn_Search_Click" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_Content" runat="server">
    <table class="Data" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr class="Table_Header">
                <th class="Table_TL"></th>
                <th width="10">STT
                </th>
                <th>
                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_1" CommandArgument="QuestionID ASC" OnClick="lbtn_Sort_Click">Mã câu hỏi</asp:LinkButton>
                </th>
                <th>
                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_2" CommandArgument="PlayDate DESC" OnClick="lbtn_Sort_Click">Ngày chơi</asp:LinkButton>
                </th>
                <th>
                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_3" CommandArgument="MSISDN ASC" OnClick="lbtn_Sort_Click">Thuê bao</asp:LinkButton>
                </th>
                <th>
                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_4" CommandArgument="SuggestID ASC" OnClick="lbtn_Sort_Click">Mã dữ kiện</asp:LinkButton>
                </th>
                <th>Dự đoán của KH
                </th>
                <th>Kết quả
                </th>
                <th>
                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_5" CommandArgument="BuyCount ASC" OnClick="lbtn_Sort_Click">SL Mua Dữ kiện</asp:LinkButton>
                </th>
                <th>
                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_6" CommandArgument="CorrectCount ASC" OnClick="lbtn_Sort_Click">SL Dự đoán đúng</asp:LinkButton>
                </th>
                <th>
                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_7" CommandArgument="IncorrectCount ASC" OnClick="lbtn_Sort_Click">SL Dự đoán sai</asp:LinkButton>
                </th>
                <th>
                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_8" CommandArgument="ReceiveDate DESC" OnClick="lbtn_Sort_Click">Ngày dự đoán</asp:LinkButton>
                </th>
                <th>Họ và Tên</th>
                <th>Địa chỉ</th>
                <th>Giải thưởng</th>
                 <th>OnWeb</th>
                <th align="center" width="10">
                    <input type="checkbox" onclick="SelectCheckBox_All(this);" /></th>
                <th class="Table_TR"></th>
            </tr>
            <asp:Repeater runat="server" ID="rpt_Data">
                <ItemTemplate>
                    <tr class="Table_Row_1">
                        <td class="Table_ML_1"></td>
                        <td>
                            <%#(Container.ItemIndex + PageIndex).ToString()%>
                        </td>
                        <td>
                            <%#Eval("QuestionID")%>
                        </td>
                        <td>
                            <%#Eval("PlayDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("ReceiveDate")).ToString(MyUtility.MyConfig.ShortDateFormat)%>
                        </td>
                        <td>
                            <%#Eval("MSISDN")%>
                        </td>
                        <td>
                            <%#Eval("SuggestID")%>
                        </td>
                        <td>
                            <%#Eval("UserAnswer")%>
                        </td>
                        <td>
                            <%#Eval("RightAnswer") %>
                        </td>
                        <td>
                            <%#Eval("BuyCount")%>
                        </td>
                        <td>
                            <%#Eval("CorrectCount")%>
                        </td>
                        <td>
                            <%#Eval("IncorrectCount")%>
                        </td>
                        <td>
                            <%#Eval("ReceiveDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("ReceiveDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                        </td>
                        <td>
                            <%#Eval("WinnerName")%>
                        </td>
                        <td>
                            <%#Eval("Address")%>
                        </td>
                        <td>
                            <%#Eval("Prize")%>
                        </td>
                        <td>
                           <img src="<%#(((bool)Eval("IsActive"))?"../Images/Buttons/Active_Grid.png":"../Images/Buttons/UnActive_Grid.png") %>" />
                        </td>
                        <td align="center" width="10">
                            <%#"<input type='checkbox' id='CheckAll_" + Container.ItemIndex.ToString() + "' value='" + Eval("QuestionID").ToString() + "' />"%>
                        </td>
                        <td class="Table_MR_1"></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Table_Row_2">
                        <td class="Table_ML_2"></td>
                        <td>
                            <%#(Container.ItemIndex + PageIndex).ToString()%>
                        </td>
                        <td>
                            <%#Eval("QuestionID")%>
                        </td>
                        <td>
                            <%#Eval("PlayDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("ReceiveDate")).ToString(MyUtility.MyConfig.ShortDateFormat)%>
                        </td>
                        <td>
                            <%#Eval("MSISDN")%>
                        </td>
                        <td>
                            <%#Eval("SuggestID")%>
                        </td>
                        <td>
                            <%#Eval("UserAnswer")%>
                        </td>
                        <td>
                            <%#Eval("RightAnswer") %>
                        </td>
                        <td>
                            <%#Eval("BuyCount")%>
                        </td>
                        <td>
                            <%#Eval("CorrectCount")%>
                        </td>
                        <td>
                            <%#Eval("IncorrectCount")%>
                        </td>
                        <td>
                            <%#Eval("ReceiveDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("ReceiveDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                        </td>
                        <td>
                            <%#Eval("WinnerName")%>
                        </td>
                        <td>
                            <%#Eval("Address")%>
                        </td>
                        <td>
                            <%#Eval("Prize")%>
                        </td>
                        <td>
                           <img src="<%#(((bool)Eval("IsActive"))?"../Images/Buttons/Active_Grid.png":"../Images/Buttons/UnActive_Grid.png") %>" />
                        </td>
                        <td align="center" width="10">
                            <%#"<input type='checkbox' id='CheckAll_" + Container.ItemIndex.ToString() + "' value='" + Eval("QuestionID").ToString() + "' />"%>
                        </td>
                        <td class="Table_MR_2"></td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <div class="Table_Footer">
        <div class="Table_BL">
            <uc1:Admin_Paging ID="Admin_Paging1" runat="server" />
        </div>
        <div class="Table_BR">
        </div>
    </div>
    <div class="Div_Hidden">
        <input type="hidden" runat="server" id="hid_ListCheckAll" />
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_Javascript" runat="server">
    <script language="javascript" type="text/javascript">
        hid_ListCheckAll = document.getElementById("<%=hid_ListCheckAll.ClientID %>");

        ReCheck_CheckboxOnGrid();

        function EditData() {
            if (BeforeEditData()) {
                document.location = '../Admin_Service/Ad_Winner_Edit.aspx?ID=' + hid_ListCheckAll.value;

                return true;
            }
            return false;
        }

        function Active() {
            if (GetAllCheck('Xin hãy chọn ít nhất một mục để kích hoạt')) {
                return true;
            }
            else {
                return false;
            }
        }
        function UnActive() {
            if (GetAllCheck('Xin hãy chọn ít nhất một mục để hủy kích hoạt')) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>

