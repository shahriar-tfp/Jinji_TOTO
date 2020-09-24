<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TD_COURSESCHEDULE_DETAIL.aspx.vb" Inherits="PAGES_TRAINING_TD_COURSESCHEDULE_DETAIL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>TD_COURSESCHEDULE_DETAIL</title>
<link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server"> 
<form id="TD_COURSESCHEDULE_DETAIL" runat="server">
<div>
<asp:Panel ID="pnlEdit" runat="server">
<!--// 1 //-->
<asp:imagebutton id="imgKeyCOMPANY_PROFILE_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCOMPANY_PROFILE_CODE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCOMPANY_PROFILE_CODE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCOMPANY_PROFILE_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 2 //-->
<asp:imagebutton id="imgKeyCOURSE_ID" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCOURSE_ID" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCOURSE_ID" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCOURSE_ID" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 3 //-->
<asp:imagebutton id="imgKeyDATESTART" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblDATESTART" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtDATESTART" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnDATESTART" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 4 //-->
<asp:imagebutton id="imgKeyDATEEND" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblDATEEND" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtDATEEND" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnDATEEND" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 5 //-->
<asp:imagebutton id="imgKeyTIMESTART" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblTIMESTART" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtTIMESTART" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnTIMESTART" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 6 //-->
<asp:imagebutton id="imgKeyEND_TIME" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblEND_TIME" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtEND_TIME" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnEND_TIME" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 7 //-->
<asp:imagebutton id="imgKeyMAXPARTICIPANT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblMAXPARTICIPANT" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtMAXPARTICIPANT" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnMAXPARTICIPANT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 8 //-->
<asp:imagebutton id="imgKeyVENUE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblVENUE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtVENUE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnVENUE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 9 //-->
<asp:imagebutton id="imgKeyBATCH_ID" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblBATCH_ID" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtBATCH_ID" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnBATCH_ID" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 10 //-->
<asp:imagebutton id="imgKeyREFERENCE_ID" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblREFERENCE_ID" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtREFERENCE_ID" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnREFERENCE_ID" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<asp:imagebutton id="imgKeyUSER_PROFILE_CODE_CREATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblUSER_PROFILE_CODE_CREATE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtUSER_PROFILE_CODE_CREATE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnUSER_PROFILE_CODE_CREATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<asp:imagebutton id="imgKeyDATETIME_CREATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblDATETIME_CREATE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtDATETIME_CREATE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnDATETIME_CREATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<asp:imagebutton id="imgKeyUSER_PROFILE_CODE_MODIFY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblUSER_PROFILE_CODE_MODIFY" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtUSER_PROFILE_CODE_MODIFY" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnUSER_PROFILE_CODE_MODIFY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<asp:imagebutton id="imgKeyDATETIME_MODIFY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblDATETIME_MODIFY" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtDATETIME_MODIFY" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnDATETIME_MODIFY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

    <!--// Panel //-->
    <div id="divtop" style="height: 30px; width: 100%">
       <table id="Table3" cellspacing="0" cellpadding="0" align="center" border="0" runat="server">
        <tr>
            <td>
                <asp:Image ID="imgtop" Height="30px" runat="server" />
            </td>
        </tr>
       </table>
    </div>
    <div id="divbottom" style="height: 30px; width: 100%">
       <table id="Table4" cellspacing="0" cellpadding="0" align="center" border="0" runat="server">
        <tr>
            <td>
                <asp:Image ID="imgbottom" Height="30px" Visible="false" runat="server" />
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
	<asp:label id="lblresult2" runat="server" Height="22" Visible="False"></asp:label>
	<asp:placeholder id="MyPH" Runat="server"></asp:placeholder></asp:Panel>
	<asp:panel id="pnldescription" runat="server" visible="False">
	<table id="Table7" style="left: 15px; position: absolute; top: 10px" cellspacing="0" cellpadding="0" border="0" runat="server">
        <tr>
            <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); width :5px"></td>
            <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); vertical-align:bottom"><asp:Label ID="lblTitle2" runat="server"></asp:Label></td>
        </tr>
    </table>
    </asp:panel>
	<asp:panel id="pnlMain" runat="server" >
	<table id="tblUserCategoryGroup" style="Z-INDEX: 115; LEFT: 10px; POSITION: absolute; TOP: 10px" borderColor="#e4d6fe"
					cellspacing="0" cellpadding="0" align="center" border="0" runat="server">
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
							<table id="tblGridView" cellspacing="0" cellpadding="0" width="100%" align="center" border="0" runat="server">
								<tr>
									<td>
										<asp:panel id="pnlGridview" runat="server" visible="true" ScrollBars="auto">
											<asp:gridview id="myGridView" Width="100%" runat="server" AutoGenerateColumns=true
												cellspacing="0" cellpadding="1" EmptyDataText="No data found!">
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
											</asp:GridView>
											<asp:Panel ID="pnlHistory" runat="server" Visible="false">
												<asp:gridview id="gvHistory" Width="100%" runat="server" AutoGenerateColumns=true
													cellspacing="0" cellpadding="1" EmptyDataText="No data found!" >
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
												</asp:GridView>
											</asp:Panel>
											<asp:SqlDataSource ID="myDSR" runat=server></asp:SqlDataSource>
										</asp:panel>
									</td>
								</tr>
								<tr>
									<td>
										<asp:panel id="pnlPrevNext" runat="server" visible="true">
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
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
										</asp:panel>
									</td>
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
