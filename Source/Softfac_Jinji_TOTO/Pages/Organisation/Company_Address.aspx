<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Company_Address.aspx.vb" Inherits="Pages_System_Company_Address" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>JINJI : Organisation - Company Address Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server">
    <form id="company_address" runat="server">
    <div>
    <asp:Panel ID="pnledit" runat="server" visible="true">
    <!--// 1 //-->
    <asp:image id="imgCOMPANY_PROFILE_CODE" runat="server" Height="21px" Width="21px"></asp:image>
    <asp:label id="lblCOMPANY_PROFILE_CODE" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtCOMPANY_PROFILE_CODE" runat="server"></asp:textbox>
    <asp:imagebutton id="imgBtnCOMPANY_PROFILE_CODE" runat="server" Height="21px"></asp:imagebutton>
    <!--// 2 //-->
    <asp:image id="imgOPTION_ADDRESS_TYPE" runat="server" Width="21px" Height="21px"></asp:image>
    <asp:label id="lblOPTION_ADDRESS_TYPE" runat="server" Width="140px"></asp:label>
    <asp:dropdownlist id="ddlOPTION_ADDRESS_TYPE" runat="server" Width="157px"></asp:dropdownlist>
    <asp:imagebutton id="imgBtnOPTION_ADDRESS_TYPE" runat="server" Height="21px"></asp:imagebutton>
    <!--// 3 //-->
    <asp:image id="imgLOT" runat="server" Width="21px" Height="21px"></asp:image>
    <asp:label id="lblLOT" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtLOT" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgBtnLOT" runat="server" Height="21px"></asp:imagebutton>
    <!--// 4 //-->
    <asp:image id="imgSTREET" runat="server" Width="21px" Height="21px"></asp:image>
    <asp:label id="lblSTREET" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtSTREET" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgBtnSTREET" runat="server" Height="21px"></asp:imagebutton>
    <!--// 5 //-->
    <asp:image id="imgPOSTAL" runat="server" Width="21px" Height="21px"></asp:image>
    <asp:label id="lblPOSTAL" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtPOSTAL" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgBtnPOSTAL" runat="server" Height="21px"></asp:imagebutton>
    <!--// 6 //-->
	<asp:image id="imgADDRESS_CITY_CODE" runat="server" Height="21px" Width="21px"></asp:image>
	<asp:label id="lblADDRESS_CITY_CODE" runat="server" Width="140px"></asp:label>
	<asp:textbox id="txtADDRESS_CITY_CODE" runat="server" Width="150px"></asp:textbox>
	<asp:imagebutton id="imgBtnADDRESS_CITY_CODE" runat="server" Height="21px"></asp:imagebutton>
	<!--// 7 //-->
	<asp:image id="imgADDRESS_STATE_CODE" runat="server" Height="21px" Width="21px"></asp:image>
	<asp:label id="lblADDRESS_STATE_CODE" runat="server" Width="140px"></asp:label>
	<asp:textbox id="txtADDRESS_STATE_CODE" runat="server" Width="150px"></asp:textbox>
	<asp:imagebutton id="imgBtnADDRESS_STATE_CODE" runat="server" Height="21px"></asp:imagebutton>
	<!--// 8 //-->
	<asp:image id="imgADDRESS_COUNTRY_CODE" runat="server" Height="21px" Width="21px"></asp:image>
	<asp:label id="lblADDRESS_COUNTRY_CODE" runat="server" Width="140px"></asp:label>
	<asp:textbox id="txtADDRESS_COUNTRY_CODE" runat="server" Width="150px"></asp:textbox>
	<asp:imagebutton id="imgBtnADDRESS_COUNTRY_CODE" runat="server" Height="21px"></asp:imagebutton>
	<!--// 9 //-->
	<asp:image id="imgADDRESS_REGION_CODE" runat="server" Height="21px" Width="21px"></asp:image>
	<asp:label id="lblADDRESS_REGION_CODE" runat="server" Width="140px"></asp:label>
	<asp:textbox id="txtADDRESS_REGION_CODE" runat="server" Width="150px"></asp:textbox>
	<asp:imagebutton id="imgBtnADDRESS_REGION_CODE" runat="server" Height="21px"></asp:imagebutton>
	<!--// 10 //-->
	<asp:image id="imgEFFECTIVE_DATE" runat="server" Height="21px" Width="21px"></asp:image>
	<asp:label id="lblEFFECTIVE_DATE" runat="server" Width="140px"></asp:label>
	<asp:textbox id="txtEFFECTIVE_DATE" runat="server" Width="150px"></asp:textbox>
	<asp:imagebutton id="imgBtnEFFECTIVE_DATE" runat="server" Height="21px"></asp:imagebutton>
	<!--// Panel //-->
    <div id="divtop" style="height: 30px; width: 100%">
       <table id="Table3" cellspacing="0" cellpadding="0" border="0" runat="server">
        <tr>
            <td>
                <asp:Image ID="imgtop" Height="30px" runat="server" />
            </td>
        </tr>
       </table>
    </div>
    <div id="divbottom" style="height: 30px; width: 100%">
       <table id="Table4" cellspacing="0" cellpadding="0" border="0" runat="server">
        <tr>
            <td>
                <asp:Image ID="imgbottom" Height="30px" Visible="false" runat="server" />
            </td>
        </tr>
       </table>
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
                            <table id="Table9" cellspacing="0" cellpadding="0" border="0" runat="server">
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
										</asp:panel></td>
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
