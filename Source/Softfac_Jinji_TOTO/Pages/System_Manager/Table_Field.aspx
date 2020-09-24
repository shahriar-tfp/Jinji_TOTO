<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Table_Field.aspx.vb" Inherits="Pages_System_Manager_Table_Field" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Table Field</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server">
    <form id="Table_Field" runat="server">
    <div>
    <table id="Table1" style="LEFT: 10px; POSITION:absolute; TOP: 10px" cellspacing="0" cellpadding="0" border="0" runat="server">
		<tr>
			<td>
			    <table id="Table6" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                    <tr>
                        <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); width :5px"></td>
                        <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); vertical-align:bottom"><asp:Label ID="lblTitle" runat="server"></asp:Label></td>
                    </tr>
                </table>
                
                <table id="Table4" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
					<tr>
					    <td>
					        <table id="Table5" cellspacing="0" cellpadding="0" border="0" runat="server">
					            <tr>
					                <td style ="height:15px"></td>
					            </tr>
					        </table> 
					        
					        <table cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgKeyMODULE_PROFILE_CODE" runat="server" Width="21px" Height="21px"/>
                                        <asp:Label ID="lblMODULE_PROFILE_CODE" runat="server" Width="140px"></asp:Label>
                                        <asp:Dropdownlist ID="ddlMODULE_PROFILE_CODE" runat="server" Width="250px" AutoPostBack="true"></asp:Dropdownlist>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgKeyPAGE_PROFILE_CODE" runat="server" Width="21px" Height="21px"/>
                                        <asp:Label ID="lblPAGE_PROFILE_CODE" runat="server" Width="140px"></asp:Label>
                                        <asp:Dropdownlist ID="ddlPAGE_PROFILE_CODE" runat="server" Width="250px" AutoPostBack="true"></asp:Dropdownlist>
                                    </td>
                                </tr>
                            </table>
                            
                            <table id="tblLinkButton" cellspacing="0" cellpadding="0" border="0" runat="server">
                                <tr>
					                <td colspan="6" style="height:10px"></td>
					            </tr>
					            <tr>
					                <td><asp:Image ID="imgBlank02" Width="5px" Height="25px" runat="server" /></td>
					                <td>
					                    <asp:LinkButton id="lnkbtnViewLIST" runat="server" CssClass="wordstyle" Text="[ List ]" Enabled="false" ></asp:LinkButton>
					                    <asp:LinkButton id="lnkBtnCloseLIST" runat="server" CssClass="wordstyle11" Text="[ List ]" Visible="false" ></asp:LinkButton>
					                </td>
					                <td><asp:Image ID="imgBlank03" Width="5px" Height="25px" runat="server" /></td>
					                <td>
					                    <asp:LinkButton id="lnkBtnViewEDIT" runat="server" CssClass="wordstyle" Text="[ Edit ]" Enabled="false" ></asp:LinkButton>
					                    <asp:LinkButton id="lnkBtnCloseEDIT" runat="server" CssClass="wordstyle11" Text="[ Edit ]" Visible="false" ></asp:LinkButton>
					                </td>
					            </tr>
                            </table>
                            
                            <table id="Table2" cellspacing="0" cellpadding="0" border="0" runat="server">
                                <tr>
					                <td>
			                            <asp:Panel ID="pnlList" runat="server" visible="true" ScrollBars="auto">
                                            <table id="tblGridview" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                                                <tr>
                                                    <td>
                                                        <%--<div style="border-top-style: double; border-right-style: double; border-left-style: double; border-bottom-style: double;border-color :#F2F4FF;">--%>
                                                            <asp:panel id="pnlGridview" runat="server" visible="true" ScrollBars="auto">
                                                                <asp:gridview id="myGridView" Width="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
										                        cellspacing="0" cellpadding="1" EmptyDataText="No data found!" BackColor="#F2F4FF">
                                                                <AlternatingRowStyle BackColor="#F2F4FF" />
										                        <EditRowStyle BackColor="#FFC7C6" CssClass="dgstyle_i" />
										                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
										                        <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
										                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="35">
                                                                            <ItemTemplate>
                                                                                <center>
                                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                </center>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:gridview>
                                                            </asp:panel>   
                                                        <%--</div>--%>
                                                        <asp:Panel ID="pnlHistory" runat="server" ScrollBars="auto" Visible="false">
                                                            <div style="border-top-style: double; border-right-style: double; border-left-style: double; border-bottom-style: double;border-color :#F2F4FF;">
                                                                <asp:gridview id="gvHistory" Width="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
									                            cellspacing="0" cellpadding="1" EmptyDataText="No data found!" BackColor="#F2F4FF">
                                                                <AlternatingRowStyle BackColor="#F2F4FF" />
									                            <EditRowStyle BackColor="#FFC7C6" CssClass="dgstyle_i" />
									                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
									                            <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
									                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="35">
                                                                            <ItemTemplate>
                                                                                <center>
                                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                </center>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:gridview>
                                                            </div>     
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
					                </td> 
					                <td>
					                
					                </td> 
					            </tr> 
					            <tr>
					                <td><asp:Label ID="lblResult" runat="server"></asp:Label></td>
					            </tr>
                            </table> 
                            
                            
                            <%-- Edit panel --%>
                            
                            <asp:Panel ID="pnlEdit" runat="server" Width="100%">
                                <table id="tblTop" cellspacing="0" cellpadding="0" border="0" runat="server">
                                    <tr>
                                        <td>
                                            <asp:Image ID="imgtop" Height="30px" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblEdit" cellspacing="0" cellpadding="0" border="0" runat="server">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgKeySEQUENCE_NO" runat="server" Width="21px" Height="21px"/>
                                            <asp:Label ID="lblSEQUENCE_NO" runat="server" Width="140px"></asp:Label>
                                            <asp:TextBox ID="txtSEQUENCE_NO" runat="server" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnSEQUENCE_NO" runat="server" Width="21px" Height="21px"/>
                                            
                                            <asp:ImageButton ID="imgKeyCODE" runat="server" Width="21px" Height="21px"/>
                                            <asp:Label ID="lblCODE" runat="server" Width="140px"></asp:Label>
                                            <asp:TextBox ID="txtCODE" runat="server" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnCODE" runat="server" Width="21px" Height="21px"/>
                                            
                                            <asp:ImageButton ID="imgKeyNAME" runat="server" Width="21px" Height="21px"/>
                                            <asp:Label ID="lblNAME" runat="server" Width="140px"></asp:Label>
                                            <asp:TextBox ID="txtNAME" runat="server" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnNAME" runat="server" Width="21px" Height="21px"/>
                                            
                                            <asp:ImageButton ID="imgKeyOPTION_DATA_TYPE" runat="server" Width="21px" Height="21px"/>
                                            <asp:Label ID="lblOPTION_DATA_TYPE" runat="server" Width="140px"></asp:Label>
                                            <asp:DropdownList ID="ddlOPTION_DATA_TYPE" runat="server" Width="155px"></asp:DropdownList>
                                            <asp:ImageButton ID="imgbtnOPTION_DATA_TYPE" runat="server" Width="21px" Height="21px"/>
                                            
                                            <asp:ImageButton ID="imgKeyLENGTH" runat="server" Width="21px" Height="21px"/>
                                            <asp:Label ID="lblLENGTH" runat="server" Width="140px"></asp:Label>
                                            <asp:TextBox ID="txtLENGTH" runat="server" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnLENGTH" runat="server" Width="21px" Height="21px"/>
                                            
                                            <asp:ImageButton ID="imgKeyOPTION_EDITABLE" runat="server" Width="21px" Height="21px"/>
                                            <asp:Label ID="lblOPTION_EDITABLE" runat="server" Width="140px"></asp:Label>
                                            <asp:DropdownList ID="ddlOPTION_EDITABLE" runat="server" Width="155px"></asp:DropdownList>
                                            <asp:ImageButton ID="imgbtnOPTION_EDITABLE" runat="server" Width="21px" Height="21px"/>
                                            
                                            <asp:ImageButton ID="imgKeyMIN_VALUE_INTEGER" runat="server" Width="21px" Height="21px"/>
                                            <asp:Label ID="lblMIN_VALUE_INTEGER" runat="server" Width="140px"></asp:Label>
                                            <asp:TextBox ID="txtMIN_VALUE_INTEGER" runat="server" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnMIN_VALUE_INTEGER" runat="server" Width="21px" Height="21px"/>
                                            
                                            <asp:ImageButton ID="imgKeyMAX_VALUE_INTEGER" runat="server" Width="21px" Height="21px"/>
                                            <asp:Label ID="lblMAX_VALUE_INTEGER" runat="server" Width="140px"></asp:Label>
                                            <asp:TextBox ID="txtMAX_VALUE_INTEGER" runat="server" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnMAX_VALUE_INTEGER" runat="server" Width="21px" Height="21px"/>
                                            
                                            <asp:ImageButton ID="imgKeyMIN_VALUE_DECIMAL" runat="server" Width="21px" Height="21px"/>
                                            <asp:Label ID="lblMIN_VALUE_DECIMAL" runat="server" Width="140px"></asp:Label>
                                            <asp:TextBox ID="txtMIN_VALUE_DECIMAL" runat="server" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnMIN_VALUE_DECIMAL" runat="server" Width="21px" Height="21px"/>
                                            
                                            <asp:ImageButton ID="imgKeyMAX_VALUE_DECIMAL" runat="server" Width="21px" Height="21px"/>
                                            <asp:Label ID="lblMAX_VALUE_DECIMAL" runat="server" Width="140px"></asp:Label>
                                            <asp:TextBox ID="txtMAX_VALUE_DECIMAL" runat="server" Width="150px"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnMAX_VALUE_DECIMAL" runat="server" Width="21px" Height="21px"/>
                                            
                                            <asp:ImageButton ID="imgKeyOPTION_VIEW_LIST" runat="server" Width="21px" Height="21px"/>
                                            <asp:Label ID="lblOPTION_VIEW_LIST" runat="server" Width="140px"></asp:Label>
                                            <asp:DropdownList ID="ddlOPTION_VIEW_LIST" runat="server" Width="155px"></asp:DropdownList>
                                            <asp:ImageButton ID="imgbtnOPTION_VIEW_LIST" runat="server" Width="21px" Height="21px"/>
                                            
                                            <asp:ImageButton ID="imgKeyOPTION_VIEW_CARD" runat="server" Width="21px" Height="21px"/>
                                            <asp:Label ID="lblOPTION_VIEW_CARD" runat="server" Width="140px"></asp:Label>
                                            <asp:DropdownList ID="ddlOPTION_VIEW_CARD" runat="server" Width="155px"></asp:DropdownList>
                                            <asp:ImageButton ID="imgbtnOPTION_VIEW_CARD" runat="server" Width="21px" Height="21px"/>
                                        </td>
                                    </tr>
                                </table>
                                
                                <table id="tblBottom" cellspacing="0" cellpadding="0" border="0" runat="server">
                                    <tr>
                                        <td>
                                            <asp:Image ID="imgbottom" Height="30px" runat="server" Visible="false" />
                                        </td>
                                    </tr>  
                                    <tr>
                                        <td style ="height:10px"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgbtnUPDATE" runat="server" OnClientClick="return confirm('Are you sure to update?')"/>
                                            <asp:Label ID="lblResult2" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                               </table>
            
                            </asp:Panel> 
                            
					    </td> 
					</tr> 
				</table> 
					    
			</td>
		</tr>
	</table>
 
    </div>
    </form>
</body>
</html>
