<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_Play.aspx.cs" Inherits="MyAdmin.Admin_Service.Ad_Play" %>

<%@ Register Src="../Admin_Control/Admin_Paging.ascx" TagName="Admin_Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Tools" runat="server">   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_ToolBox" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Search" runat="server">
    <label>
        Số điện thoại:</label>
    <input type="text" runat="server" id="tbx_Search" />   
    <select runat="server" id="sel_Status">
    </select>
     <select runat="server" id="sel_PlayType">
    </select>
    <asp:Button runat="server" ID="btn_Search" Text="Tìm kiếm" OnClick="btn_Search_Click" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_Content" runat="server">
    <table class="Data" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr class="Table_Header">
                <th class="Table_TL">
                </th>
                <th width="10">
                    STT
                </th>
                <th>
                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_1" CommandArgument="MSISDN ASC" OnClick="lbtn_Sort_Click">Thuê bao</asp:LinkButton>
                </th>
                <th>
                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_2" CommandArgument="QuestionID ASC" OnClick="lbtn_Sort_Click">Mã câu hỏi</asp:LinkButton>
                </th>
                <th>
                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_3" CommandArgument="SuggestID ASC" OnClick="lbtn_Sort_Click">Mã dữ kiện</asp:LinkButton>
                </th>
                <th>
                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_4" CommandArgument="PlayTypeID ASC" OnClick="lbtn_Sort_Click">Hình thức</asp:LinkButton>
                </th>
                <th>
                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_5" CommandArgument="OrderNumber ASC" OnClick="lbtn_Sort_Click">Thứ tự dữ kiện</asp:LinkButton>
                </th>
                <th>
                    Dự đoán
                </th>
                <th>
                    Kết quả
                </th>
                <th>
                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_6" CommandArgument="ReceiveDate ASC" OnClick="lbtn_Sort_Click">Ngày dự đoán</asp:LinkButton>
                </th>
                <th class="Table_TR">
                </th>
            </tr>
            <asp:Repeater runat="server" ID="rpt_Data">
                <ItemTemplate>
                    <tr class="Table_Row_1">
                        <td class="Table_ML_1"></td>
                        <td>
                            <%#(Container.ItemIndex + PageIndex).ToString()%>
                        </td>
                        <td>
                            <%#Eval("MSISDN") %>
                        </td>
                        <td>
                            <%#Eval("QuestionID") %>
                        </td>
                         <td>
                            <%#Eval("SuggestID") %>
                        </td>
                         <td>
                            <%#Eval("PlayTypeName") %>
                        </td>
                         <td>
                            <%#Eval("OrderNumber") %>
                        </td>
                         <td>
                            <%#Eval("UserAnswer") %>
                        </td>
                         <td>
                            <%#Eval("StatusName") %>
                        </td>
                        <td>
                            <%#Eval("ReceiveDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("ReceiveDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
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
                            <%#Eval("MSISDN") %>
                        </td>
                        <td>
                            <%#Eval("QuestionID") %>
                        </td>
                         <td>
                            <%#Eval("SuggestID") %>
                        </td>
                         <td>
                            <%#Eval("PlayTypeName") %>
                        </td>
                         <td>
                            <%#Eval("OrderNumber") %>
                        </td>
                         <td>
                            <%#Eval("UserAnswer") %>
                        </td>
                         <td>
                            <%#Eval("StatusName") %>
                        </td>
                        <td>
                            <%#Eval("ReceiveDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("ReceiveDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
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
</asp:Content>
