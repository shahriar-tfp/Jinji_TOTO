<%@ Page Language="VB" AutoEventWireup="false" CodeFile="User_Profile.aspx.vb" Inherits="Pages_System_User_Profile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>JINJI : Organisation - User Profile Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server">
    <form id="user_profile" runat="server">
    <div>
    <asp:Panel ID="pnledit" runat="server" visible="true">
    <!--// Panel //-->
    <div id="divtop" style="height:30px; width:100%">
       <table id="Table3" cellspacing="0" cellpadding="0" border="0" runat="server">
        <tr>
            <td>
                <asp:Image ID="imgtop" Height="30px" runat="server" />
            </td>
        </tr>
       </table>
    </div>
    <div id="divbottom" style="height:30px; width:100%">
       <table id="Table4" cellspacing="0" cellpadding="0" border="0" runat="server">
        <tr>
            <td>
                <asp:Image ID="imgbottom" Height="30px" Visible="false" runat="server" />
            </td>
        </tr>
       </table>
    </div>
    <div id="div2" style="width:100%">
    <!--// 1 //-->
    <asp:imagebutton id="imgCOMPANY_PROFILE_CODE" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblCOMPANY_PROFILE_CODE" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtCOMPANY_PROFILE_CODE" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgBtnCOMPANY_PROFILE_CODE" runat="server" Height="21px"></asp:imagebutton>
    <!--// 2 //-->
    <asp:imagebutton id="imgCODE" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblCODE" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtCODE" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgBtnCODE" runat="server" Height="21px"></asp:imagebutton>
    <!--// 3 //-->
    <asp:imagebutton id="imgNAME" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblNAME" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtNAME" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgBtnNAME" runat="server" Height="21px"></asp:imagebutton>
    <!--// 4 //-->
    <asp:imagebutton id="imgPASSWORD" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblPASSWORD" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtPASSWORD" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgBtnPASSWORD" runat="server" Height="21px"></asp:imagebutton>
    
    <asp:imagebutton id="imgCONFIRM_PASSWORD" runat="server" Width="21px" Height="21px" Visible="false"></asp:imagebutton>
    <asp:label id="lblCONFIRM_PASSWORD" runat="server" Width="140px" Visible="false" Text="Confirm Password"></asp:label>
    <asp:textbox id="txtCONFIRM_PASSWORD" runat="server" Width="150px" TextMode="Password" Visible="false"></asp:textbox>
    <!--// 5 //-->
    <asp:imagebutton id="imgPASSWORD_KEY_CODE" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblPASSWORD_KEY_CODE" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtPASSWORD_KEY_CODE" runat="server" Width="150px" TextMode="Password"></asp:textbox>
    <asp:imagebutton id="imgBtnPASSWORD_KEY_CODE" runat="server" Height="21px"></asp:imagebutton>
    <!--// 6 //-->
    <asp:imagebutton id="imgPASSWORD_LAST_UPDATE_DATE" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblPASSWORD_LAST_UPDATE_DATE" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtPASSWORD_LAST_UPDATE_DATE" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgBtnPASSWORD_LAST_UPDATE_DATE" runat="server" Height="21px"></asp:imagebutton>
    <!--// 7 //-->
    <asp:imagebutton id="imgLAST_LOGIN_DATE_TIME" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblLAST_LOGIN_DATE_TIME" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtLAST_LOGIN_DATE_TIME" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgBtnLAST_LOGIN_DATE_TIME" runat="server" Height="21px"></asp:imagebutton>
    <!--// 8 //-->
    <asp:imagebutton id="imgPASSWORD_DUE_DAY" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblPASSWORD_DUE_DAY" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtPASSWORD_DUE_DAY" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgBtnPASSWORD_DUE_DAY" runat="server" Height="21px"></asp:imagebutton>
    <!--// 9 //-->
    <asp:imagebutton id="imgEMPLOYEE_PROFILE_ID" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblEMPLOYEE_PROFILE_ID" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtEMPLOYEE_PROFILE_ID" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgBtnEMPLOYEE_PROFILE_ID" runat="server" Height="21px"></asp:imagebutton>
    <!--// 10 //-->
    <asp:imagebutton id="imgLANGUAGE_PROFILE_CODE" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblLANGUAGE_PROFILE_CODE" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtLANGUAGE_PROFILE_CODE" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgBtnLANGUAGE_PROFILE_CODE" runat="server" Height="21px"></asp:imagebutton>
    <!--// 11 //-->
    <asp:imagebutton id="imgLOGIN_COUNT" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblLOGIN_COUNT" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtLOGIN_COUNT" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgBtnLOGIN_COUNT" runat="server" Height="21px"></asp:imagebutton>
    <!--// 12 //-->
    <asp:imagebutton id="imgLOGIN_FAIL_COUNT" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblLOGIN_FAIL_COUNT" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtLOGIN_FAIL_COUNT" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgBtnLOGIN_FAIL_COUNT" runat="server" Height="21px"></asp:imagebutton>
    <!--// 13 //-->
    <asp:imagebutton id="imgEXPIRY_DATE" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblEXPIRY_DATE" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtEXPIRY_DATE" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgBtnEXPIRY_DATE" runat="server" Height="21px"></asp:imagebutton>
    <!--// 14 //-->
    <asp:imagebutton id="imgOPTION_CHANGE_PASSWORD_ON_NEXT_LOGON" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblOPTION_CHANGE_PASSWORD_ON_NEXT_LOGON" runat="server" Width="140px"></asp:label>
    <asp:dropdownlist id="ddlOPTION_CHANGE_PASSWORD_ON_NEXT_LOGON" runat="server" Width="157px"></asp:dropdownlist>
    <asp:imagebutton id="imgBtnOPTION_CHANGE_PASSWORD_ON_NEXT_LOGON" runat="server" Height="21px"></asp:imagebutton>
    <!--// 15 //-->
    <asp:imagebutton id="imgOPTION_LOCK" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblOPTION_LOCK" runat="server" Width="140px"></asp:label>
    <asp:dropdownlist id="ddlOPTION_LOCK" runat="server" Width="157px"></asp:dropdownlist>
    <asp:imagebutton id="imgBtnOPTION_LOCK" runat="server" Height="21px"></asp:imagebutton>
    <!--// 16 //-->
    <asp:imagebutton id="imgOPTION_RECRUIT_USER" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblOPTION_RECRUIT_USER" runat="server" Width="140px"></asp:label>
    <asp:dropdownlist id="ddlOPTION_RECRUIT_USER" runat="server" Width="157px"></asp:dropdownlist>
    <asp:imagebutton id="imgBtnOPTION_RECRUIT_USER" runat="server" Height="21px"></asp:imagebutton>
    <!--// 17 //-->
    <asp:imagebutton id="imgSECURITY_ROLE_PROFILE_CODE" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblSECURITY_ROLE_PROFILE_CODE" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtSECURITY_ROLE_PROFILE_CODE" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgBtnSECURITY_ROLE_PROFILE_CODE" runat="server" Height="21px"></asp:imagebutton>
    <!--// 18 //-->
    <asp:imagebutton id="imgEMAIL_ADDRESS" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblEMAIL_ADDRESS" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtEMAIL_ADDRESS" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgBtnEMAIL_ADDRESS" runat="server" Height="21px"></asp:imagebutton>
    </div>
    <!--// Button //-->  
    <asp:imagebutton id="imgBtnSubmit" runat="server" Width="74" Height="21" OnClientClick="return confirm('Confirmation! Submit Data ?')"></asp:imagebutton>
    <asp:imagebutton id="imgBtnSearch" runat="server" Width="74" Height="21"></asp:imagebutton>
    <asp:imagebutton id="imgBtnUpdate" runat="server" Width="74" Height="21" OnClientClick="return confirm('Confirmation! Revise Data ?')"></asp:imagebutton>
    <asp:imagebutton id="imgBtnClear" runat="server" Width="74" Height="21"></asp:imagebutton>
    <asp:imagebutton id="imgBtnCancel" runat="server" Width="74" Height="21"></asp:imagebutton>
	<asp:label id="lblresult2" runat="server" Height="22" Visible="False"></asp:label>
	</asp:Panel>
	<asp:placeholder id="MyPH" Runat="server"></asp:placeholder>
	<asp:panel id="pnldescription" runat="server" visible="False">
	<table id="Table7" style="left: 10px; position: absolute; top: 10px" cellspacing="0" cellpadding="0" border="0" runat="server">
        <tr>
            <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); width :5px"></td>
            <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); vertical-align:bottom"><asp:Label ID="lblTitle2" runat="server"></asp:Label></td>
        </tr>
    </table>
    </asp:panel> 
	<asp:panel id="pnlmain" runat="server" visible="true">
	<table id="Table1" style="LEFT: 10px; POSITION: absolute; TOP: 10px" cellspacing="0" cellpadding="0" border="0" runat="server">
					<tr>
						<td>
						    <table id="Table6" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                                <tr>
                                    <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); width :5px"></td>
                                    <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); vertical-align:bottom"><asp:Label ID="lblTitle" runat="server"></asp:Label></td>
                                </tr>
                            </table>
                            <table id="Table5" cellspacing="0" cellpadding="0" border="0" runat="server">
					            <tr>
					                <td style ="height:15px"></td>
					            </tr>
					        </table> 
							<table id="TABLE2" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
								<tr>
									<td>
										<asp:panel id="pnlgridview" ScrollBars="Auto" runat="server" visible="true">
											<%--<div style="border-top-style: double; border-right-style: double; border-left-style: double; border-bottom-style: double;border-color :#F2F4FF;">--%>
												<asp:gridview id="myGridView" Width="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
													cellspacing="0" cellpadding="1" EmptyDataText="No data found!">
                                                    <AlternatingRowStyle BackColor="#F2F4FF" />
													<EditRowStyle BackColor="#FFC7C6" CssClass="dgstyle_i" />
													<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
													<Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
													<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
													<columns>
														<asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
															<HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
															<ItemTemplate>
																<asp:CheckBox ID="chkDelete" BorderStyle="None" Runat="server" />
															</ItemTemplate>
														</asp:TemplateField>
													</columns>
												</asp:gridview>
												<asp:SqlDataSource ID="myDSR" runat="server"></asp:SqlDataSource>
											<%--</div>--%>
										</asp:panel>
										<asp:panel id="pnlCompare" ScrollBars="Auto" runat="server" visible="false">
											<%--<div style="border-top-style: double; border-right-style: double; border-left-style: double; border-bottom-style: double;border-color :#F2F4FF;">--%>
												<asp:gridview id="myGridView1" Width="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
													cellspacing="0" cellpadding="1" EmptyDataText="No data found!">
                                                    <AlternatingRowStyle BackColor="#F2F4FF" />
													<EditRowStyle BackColor="#FFC7C6" CssClass="dgstyle_i" />
													<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
													<Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
													<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
													<columns>
														<asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
															<HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
															<ItemTemplate>
																<asp:CheckBox ID="chkDelete1" BorderStyle="None" Runat="server" />
															</ItemTemplate>
														</asp:TemplateField>
													</columns>
												</asp:gridview>
											<%--</div>--%>
										</asp:panel>
									</td>
								</tr>
								<tr>
									<td>
										<asp:panel id="pnlprevnext" runat="server" visible="true">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
										        <tr>
											        <td align="left">
												        <p class="wordstyle">&nbsp;Page&nbsp;
													        <asp:Label id="CurrentPage" runat="server" CssClass="wordstyle"></asp:Label>&nbsp;of&nbsp;
													        <asp:Label id="TotalPages" runat="server" CssClass="wordstyle"></asp:Label>
													        <asp:Label id="lbltotal" runat="server" CssClass="wordstyle"></asp:Label></p>
											        </td>
											        <td align="center">
												        <asp:LinkButton id="FirstPage" runat="server" CssClass="wordstyle" CommandName="First" OnCommand="NavigationLink_Click"
													        Text="[ First ]"></asp:LinkButton>
												        <asp:LinkButton id="PrevPage" runat="server" CssClass="wordstyle" CommandName="Prev" OnCommand="NavigationLink_Click"
													        Text="[ Prev ]"></asp:LinkButton>
												        <asp:LinkButton id="NextPage" runat="server" CssClass="wordstyle" CommandName="Next" OnCommand="NavigationLink_Click"
													        Text="[ Next ]"></asp:LinkButton>
												        <asp:LinkButton id="LastPage" runat="server" CssClass="wordstyle" CommandName="Last" OnCommand="NavigationLink_Click"
													        Text="[ Last ]"></asp:LinkButton></td>
											        <td align="right">
												        <asp:Label id="lblGoToPage" runat="server" Text="Go To Page" CssClass="wordstyle1"></asp:Label>
												        <asp:TextBox id="txtGoToPage" runat="server" Width="35px" CssClass="toppos"></asp:TextBox>
												        <asp:ImageButton id="imgBtnGoToPage" Height="21px" ImageAlign="AbsBottom" Runat="server"></asp:ImageButton></td>
												    <td style="width:5px">&nbsp;</td>
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
										</asp:panel>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
	</asp:panel> 
    </div>
    </form>
</body>
</html>
