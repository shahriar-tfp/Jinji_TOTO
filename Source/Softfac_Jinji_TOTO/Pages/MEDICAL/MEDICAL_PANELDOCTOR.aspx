<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MEDICAL_PANELDOCTOR.aspx.vb" Inherits="PAGES_MEDICAL_MEDICAL_PANELDOCTOR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>MEDICAL_PANELDOCTOR</title>
<link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server"> 
<form id="MEDICAL_PANELDOCTOR" runat="server">
<div>
<asp:Panel ID="pnlEdit" runat="server">
<!--// 1 //-->
<asp:imagebutton id="imgKeyCOMPANY_PROFILE_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCOMPANY_PROFILE_CODE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCOMPANY_PROFILE_CODE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCOMPANY_PROFILE_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 2 //-->
<asp:imagebutton id="imgKeyCODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCODE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCODE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 3 //-->
<asp:imagebutton id="imgKeyNAME" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblNAME" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtNAME" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnNAME" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 4 //-->
<asp:imagebutton id="imgKeyPICNAME" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblPICNAME" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtPICNAME" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnPICNAME" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 5 //-->
<asp:imagebutton id="imgKeyADDR1" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblADDR1" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtADDR1" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnADDR1" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 6 //-->
<asp:imagebutton id="imgKeyADDR2" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblADDR2" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtADDR2" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnADDR2" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 7 //-->
<asp:imagebutton id="imgKeyPOSTCODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblPOSTCODE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtPOSTCODE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnPOSTCODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 8 //-->
<asp:imagebutton id="imgKeyCOUNTRY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCOUNTRY" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCOUNTRY" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCOUNTRY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 9 //-->
<asp:imagebutton id="imgKeyTELNO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblTELNO" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtTELNO" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnTELNO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 10 //-->
<asp:imagebutton id="imgKeyFAXNO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblFAXNO" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtFAXNO" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnFAXNO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 11 //-->
<asp:imagebutton id="imgKeyEMAIL" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblEMAIL" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtEMAIL" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnEMAIL" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 12 //-->
<asp:imagebutton id="imgKeyHANDPHONE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblHANDPHONE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtHANDPHONE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnHANDPHONE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 13 //-->
<asp:imagebutton id="imgKeyCREDITLIMIT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCREDITLIMIT" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCREDITLIMIT" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCREDITLIMIT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 14 //-->
<asp:imagebutton id="imgKeyACTIVE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblACTIVE" runat="server" Width="140px" ></asp:label>
<asp:dropdownlist id="ddlACTIVE" runat="server" Width="150px" ></asp:dropdownlist>
<asp:imagebutton id="imgbtnACTIVE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 15 //-->
<asp:imagebutton id="imgKeyREMARK1" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblREMARK1" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtREMARK1" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnREMARK1" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 16 //-->
<asp:imagebutton id="imgKeyREMARK2" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblREMARK2" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtREMARK2" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnREMARK2" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

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
       <table id="Table3" cellSpacing="0" cellPadding="0" align="center" border="0" runat="server">
        <tr>
            <td>
                <asp:Image ID="imgtop" Height="30px" runat="server" />
            </td>
        </tr>
       </table>
    </div>
    <div id="divbottom" style="height: 30px; width: 100%">
       <table id="Table4" cellSpacing="0" cellPadding="0" align="center" border="0" runat="server">
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
					cellSpacing="0" cellPadding="0" align="center" border="0" runat="server">
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
							<table id="tblGridView" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" runat="server">
								<tr>
									<td>
										<asp:panel id="pnlGridview" runat="server" visible="true" ScrollBars="auto">
											<asp:gridview id="myGridView" Width="100%" runat="server" AutoGenerateColumns=true
												CellSpacing="0" CellPadding="1" EmptyDataText="No data found!">
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
												</asp:GridView>
											</asp:Panel>
											<asp:SqlDataSource ID="myDSR" runat=server></asp:SqlDataSource>
										</asp:panel>
									</td>
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
