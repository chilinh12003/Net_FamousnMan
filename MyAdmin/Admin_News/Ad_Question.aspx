<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="Ad_Question.aspx.cs" Inherits="MyAdmin.Admin_News.Ad_Question" %>

<%@ Register Src="../Admin_Control/Admin_Paging.ascx" TagName="Admin_Paging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Tools" runat="server">
    <a href="javascript:void(0);" onclick="return EditData();" runat="server" id="link_Edit"><span class="Edit"></span>Sửa </a>
    <asp:LinkButton runat="server" ID="lbtn_Delete" OnClientClick="return BeforeDeteleData();" ToolTip="Xóa tất cả mục đã chọn" OnClick="lbtn_Delete_Click">
        <span class="Delete"></span>
            Xóa
    </asp:LinkButton>
    <a href="Ad_Question_Edit.aspx" runat="server" id="link_Add"><span class="Add"></span>Thêm </a>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_ToolBox" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Search" runat="server">
  
        <label>
            Từ khóa:</label>
        <input type="text" runat="server" id="tbx_Search" /> 
 
    <select runat="server" id="sel_SearchType">
        <option value="0">- - Tìm theo tất cả - - </option>
    </select>
  
    <select runat="server" id="sel_Status">
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
                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_1" CommandArgument="QuestionID ASC" OnClick="lbtn_Sort_Click">Mã</asp:LinkButton>
                </th>
                <th>
                    Tiêu đề
                </th>              
                <th>
                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_2" CommandArgument="PlayDate DESC" OnClick="lbtn_Sort_Click">Thời gian gửi tin</asp:LinkButton>
                </th>
                  <th>
                    <asp:LinkButton runat="server" CssClass="Sort" ID="lbtn_Sort_3" CommandArgument="StatusID DESC" OnClick="lbtn_Sort_Click">Tình trạng</asp:LinkButton>
                </th>              
                <th>
                    Trả lời
                </th>
                <th>
                    Giải thưởng
                </th>
                <th>
                    Giá trị
                </th>
                <th>Dữ kiện</th>
                <th align="center" width="10">
                    <input type="checkbox" onclick="SelectCheckBox_All(this);" />
                </th>
                <th class="Table_TR">
                </th>
            </tr>
            <asp:Repeater runat="server" ID="rpt_Data">
                <ItemTemplate>
                    <tr class="Table_Row_1">
                        <td class="Table_ML_1">
                        </td>
                        <td>
                            <%#(Container.ItemIndex + PageIndex).ToString()%>
                        </td>                       
                        <td>
                            <%#Eval("QuestionID") %>
                        </td>
                        <td>
                            <a href="Ad_Question_Edit.aspx?ID=<%#Eval("QuestionID") %>">
                                <%#Eval("QuestionName")%></a>
                        </td>                                        
                        <td>
                            <%#Eval("PlayDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("PlayDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                        </td>
                         <td>
                            <%#Eval("StatusName")%>
                        </td>    
                         <td>
                            <%#Eval("RightAnswer")%>
                        </td>
                         <td>
                            <%#Eval("Prize")%>
                        </td>
                         <td>
                            <%#Eval("Price")%>
                        </td>
                         <td>
                            <a href="Ad_Suggest.aspx?QID=<%#Eval("QuestionID") %>">
                                <img src="../Images/Buttons/Go_en.png" /></a>
                        </td>    
                        <td align="center" width="10">
                            <%#"<input type='checkbox' id='CheckAll_" + Container.ItemIndex.ToString() + "' value='" + Eval("QuestionID").ToString() + "' />"%>
                        </td>
                        
                        <td class="Table_MR_1">
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Table_Row_2">
                        <td class="Table_ML_2">
                        </td>
                        <td>
                            <%#(Container.ItemIndex + PageIndex).ToString()%>
                        </td>                       
                        <td>
                            <%#Eval("QuestionID") %>
                        </td>
                        <td>
                            <a href="Ad_Question_Edit.aspx?ID=<%#Eval("QuestionID") %>">
                                <%#Eval("QuestionName")%></a>
                        </td>                                       
                        <td>
                            <%#Eval("PlayDate") == DBNull.Value ? string.Empty : ((DateTime)Eval("PlayDate")).ToString(MyUtility.MyConfig.LongDateFormat)%>
                        </td>
                         <td>
                            <%#Eval("StatusName")%>
                        </td>    
                         <td>
                            <%#Eval("RightAnswer")%>
                        </td>
                         <td>
                            <%#Eval("Prize")%>
                        </td>
                         <td>
                            <%#Eval("Price")%>
                        </td>
                         <td>
                            <a href="Ad_Suggest.aspx?QID=<%#Eval("QuestionID") %>">
                                <img src="../Images/Buttons/Go_en.png" /></a>
                        </td>    
                        <td align="center" width="10">
                            <%#"<input type='checkbox' id='CheckAll_" + Container.ItemIndex.ToString() + "' value='" + Eval("QuestionID").ToString() + "' />"%>
                        </td>
                        <td class="Table_MR_2">
                        </td>
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

        function EditData()
        {
            if (BeforeEditData())
            {
                document.location = '../Admin_News/Ad_Question_Edit.aspx?ID=' + hid_ListCheckAll.value;

                return true;
            }
            return false;
        }
       
    </script>
</asp:Content>
