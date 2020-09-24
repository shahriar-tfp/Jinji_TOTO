<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EMP_RPT_RND.aspx.vb" Inherits="PAGES_EMP_RPT_EMP_RPT_RND" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>JINJI : Employee Report R&D Page</title>
<link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body>
<form id="EMP_RPT_RND" runat="server">
<div>
<asp:Panel ID="pnlEdit" runat="server">
<!--// 1 //-->
<asp:imagebutton id="imgKeyCOMPANY_PROFILE_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCOMPANY_PROFILE_CODE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCOMPANY_PROFILE_CODE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCOMPANY_PROFILE_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 2 //-->
<asp:imagebutton id="imgKeyEMPLOYEE_PROFILE_ID" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblEMPLOYEE_PROFILE_ID" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtEMPLOYEE_PROFILE_ID" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnEMPLOYEE_PROFILE_ID" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 3 //-->
<asp:imagebutton id="imgKeyOPT_MODULE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOPT_MODULE" runat="server" Width="140px" ></asp:label>
<asp:dropdownlist id="ddlOPT_MODULE" runat="server" Width="150px" ></asp:dropdownlist>
<asp:imagebutton id="imgbtnOPT_MODULE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 4 //-->
<asp:imagebutton id="imgKeyTASK" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblTASK" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtTASK" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnTASK" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 5 //-->
<asp:imagebutton id="imgKeySTART_DATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblSTART_DATE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtSTART_DATE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnSTART_DATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 6 //-->
<asp:imagebutton id="imgKeyECD" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblECD" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtECD" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnECD" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 7 //-->
<asp:imagebutton id="imgKeyACD" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblACD" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtACD" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnACD" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 8 //-->
<asp:imagebutton id="imgKeyOPTION_STATUS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOPTION_STATUS" runat="server" Width="140px" ></asp:label>
<asp:dropdownlist id="ddlOPTION_STATUS" runat="server" Width="150px" ></asp:dropdownlist>
<asp:imagebutton id="imgbtnOPTION_STATUS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 9 //-->
<asp:imagebutton id="imgKeyREMARK" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblREMARK" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtREMARK" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnREMARK" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

    <!--// Panel //-->
    <div id="divtop" style="height: 30px; width: 760px">
       <table id="Table3" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" runat="server">
        <tr>
            <td>
                <asp:Image ID="imgtop" Height="30px" Width="760px" runat="server" />
            </td>
        </tr>
       </table>
    </div>
    <div id="divbottom" style="height: 30px; width: 760px">
       <table id="Table4" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" runat="server">
        <tr>
            <td>
                <asp:Image ID="imgbottom" Height="30px" Width="760px" runat="server" />
            </td>
        </tr>
       </table>
    </div>
    <!--// Button //-->  
    <asp:imagebutton id="imgBtnSubmit" runat="server" Width="74" Height="21" OnClientClick="return confirm('Are you sure you want to submit?')"></asp:imagebutton>
    <asp:imagebutton id="imgBtnSearch" runat="server" Width="74" Height="21"></asp:imagebutton>
    <asp:imagebutton id="imgBtnUpdate" runat="server" Width="74" Height="21" OnClientClick="return confirm('Are you sure you want to update?')"></asp:imagebutton>
    <asp:imagebutton id="imgBtnClear" runat="server" Width="74" Height="21"></asp:imagebutton>
    <asp:imagebutton id="imgBtnCancel" runat="server" Width="74" Height="21"></asp:imagebutton>
	<asp:label id="lblresult2" runat="server" Height="22" Visible="False" width="450"></asp:label>
	<asp:placeholder id="MyPH" Runat="server"></asp:placeholder></asp:Panel>
	<asp:panel id="pnldescription" runat="server" visible="False">
	<table id="Table7" style="left: 15px; position: absolute; top: 10px" width="750px" cellspacing="0" cellpadding="0" border="0" runat="server">
        <tr>
            <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar.gif); width :5px"></td>
            <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar.gif); vertical-align:bottom"><asp:Label ID="lblTitle2" runat="server"></asp:Label></td>
        </tr>
    </table>
    </asp:panel>
	<asp:panel id="pnlMain" runat="server" >
	<table id="tblUserCategoryGroup" style="Z-INDEX: 115; LEFT: 10px; POSITION: absolute; TOP: 10px" borderColor="#e4d6fe"
					cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" runat="server">
					<tr>
						<td>
						<table id="Table6" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                                <tr>
                                    <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar.gif); width :5px"></td>
                                    <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar.gif); vertical-align:bottom"><asp:Label ID="lblTitle" runat="server"></asp:Label></td>
                                </tr>
                            </table>
							<table id="tblGridView" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" runat="server">
								<tr>
									<td>
										<asp:panel id="pnlGridview" runat="server" visible="true" ScrollBars="auto">
											<div style="border-top-style: double; border-right-style: double; border-left-style: double; border-bottom-style: double;border-color :#F2F4FF;">
												<asp:gridview id="myGridView" Width="100%" runat="server" AutoGenerateColumns=true
													CellSpacing="0" CellPadding="1" EmptyDataText="No data found!">
													<AlternatingRowStyle BackColor="#F2F4FF" />
													<EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
													<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
													<Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
													<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
    <Columns>
<%--            <asp:TemplateField ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <asp:LinkButton ID="btnGridEdit" runat="server" CommandName="Edit" Text="Edit" Width="25"/>
                    <asp:LinkButton ID="btnGridDelete" runat="server" CommandName="Delete" Text="Delete" Width="35"
                     OnClientClick="return confirm('Are you sure you want to delete?')" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="btnGridUpdate" runat="server" CommandName="Update" Text="Update" Width="30"
                     OnClientClick="return confirm('Are you sure you want to update?')"/>
                    <asp:LinkButton ID="btnGridCancel" runat="server" CommandName="Cancel" Text="Cancel" Width="30"/>
                </EditItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Select" ItemStyle-Width="35">
                <ItemTemplate>
                    <center>
                    <asp:CheckBox ID="chkSelect" runat="server" />
                    </center>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </asp:GridView>
                <asp:Panel ID="pnlHistory" runat="server" Visible="false">
        <asp:gridview id="gvHistory" Width="100%" runat="server" AutoGenerateColumns=true
													CellSpacing="0" CellPadding="1" EmptyDataText="No data found!" >
													<AlternatingRowStyle BackColor="#F2F4FF" />
													<EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
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
        </asp:GridView></asp:Panel>
        <asp:SqlDataSource ID="myDSR" runat=server></asp:SqlDataSource></div> 
	</asp:panel></td>
								</tr>
								<tr>
									<td>
										<asp:panel id="pnlPrevNext" runat="server" visible="true">
											<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										        <tr>
											        <td align="left">
												        <P class="wordstyle">&nbsp;Page&nbsp;
													        <asp:Label id="CurrentPage" runat="server" CssClass="wordstyle"></asp:Label>&nbsp;of&nbsp;
													        <asp:Label id="TotalPages" runat="server" CssClass="wordstyle"></asp:Label>
													        <asp:Label id="lblTotal" runat="server" CssClass="wordstyle"></asp:Label></P>
											        </td>
											        <td align="center">
												        <asp:LinkButton id="lnkbtnFirstPage" runat="server" CssClass="wordstyle" Text="[First]" ></asp:LinkButton>
												        <asp:LinkButton id="lnkbtnPrevPage" runat="server" CssClass="wordstyle" Text="[Previous]" ></asp:LinkButton>
													<asp:LinkButton id="lnkbtnNextPage" runat="server" CssClass="wordstyle" Text="[Next]" ></asp:LinkButton>
													<asp:LinkButton id="lnkbtnLastPage" runat="server" CssClass="wordstyle" Text="[Last]" ></asp:LinkButton></td>
											        <td align="left">
												        <asp:Label id="lblGoToPage" runat="server" Text="Go To Page" CssClass="wordstyle1"></asp:Label>
												        <asp:TextBox id="txtGoToPage" runat="server" Width="35px" CssClass="toppos"></asp:TextBox>
												        <asp:ImageButton id="imgBtnGoToPage" Height="21px" ImageAlign="AbsBottom" Runat="server"></asp:ImageButton></td>
												    <td width="5px">&nbsp;</td>
										        </tr>
									        </table>
										</asp:panel></td>
								</tr>
								<tr>
									<td>
										<asp:panel id="pnlaction" runat="server" visible="true">
											<table cellspacing="0" cellpadding="0" border="0">
												<tr>
													<td>&nbsp;</td>
													<td>&nbsp;</td>
													<td>&nbsp;</td>
												</tr>
												<tr>
												    <td style="width:2px">&nbsp;</td>
												    <td align="left">
														<asp:imagebutton id="imgBtnAdd" runat="server" Height="21px" Width="74px"></asp:imagebutton>
														<asp:imagebutton id="imgBtnEdit" runat="server" Height="21px" Width="74px"></asp:imagebutton>
														<asp:imagebutton id="imgBtnDelete" runat="server" Height="21px" Width="74px" OnClientClick="return confirm('Confirmation! Delete Data(s) ?')"></asp:imagebutton>
														<asp:imagebutton id="imgBtnFilter" runat="server" Height="21px" Width="74px"></asp:imagebutton>
														<asp:imagebutton id="imgBtnPrint" runat="server" Height="21px" Width="74px"></asp:imagebutton>
													</td>
													<td align="left">
													    <asp:panel id="pnlresult" runat="server" visible="true">
									                        <table cellspacing="0" cellpadding="0" border="0">
												                <tr>
										                            <td><asp:Image ID="Image1" ImageUrl="../../../images/company/default/gif/px1.gif" Width="5px" runat="server" /></td>
											                        <td align="left"><asp:Label id="lblresult" runat="server" CssClass="wordstyle2"></asp:Label></td>
										                        </tr>
									                        </table>
								                        </asp:panel> 
													</td>
												</tr>
											</table>
										</asp:panel></td>
								</tr>
							</table>
						</td>
						<td><asp:Image ID="imgBlank" ImageUrl="../../../images/company/default/gif/px1.gif" Width="5px" runat="server" /></td>
					</tr>
				</table>
	</asp:panel> 
    </div>
    </form>
</body>
</html>
