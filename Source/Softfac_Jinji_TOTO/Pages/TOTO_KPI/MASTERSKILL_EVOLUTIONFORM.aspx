<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MASTERSKILL_EVOLUTIONFORM.aspx.vb" Inherits="PAGES_TOTO_KPI_MASTERSKILL_EVOLUTIONFORM" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>MASTERSKILL_EVOLUTIONFORM</title>
<link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server"> 
<form id="MASTERSKILL_EVOLUTIONFORM" runat="server">
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
<asp:imagebutton id="imgKeyMASTERSKILL" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblMASTERSKILL" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtMASTERSKILL" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnMASTERSKILL" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 4 //-->
<asp:imagebutton id="imgKeyDEPARTMENT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblDEPARTMENT" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtDEPARTMENT" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnDEPARTMENT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 5 //-->
<asp:imagebutton id="imgKeyDATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblDATE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtDATE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnDATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 6 //-->
<asp:imagebutton id="imgKeySTATUS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblSTATUS" runat="server" Width="140px" ></asp:label>
<asp:dropdownlist id="ddlSTATUS" runat="server" Width="150px" ></asp:dropdownlist>
<asp:imagebutton id="imgbtnSTATUS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 7 //-->
<asp:imagebutton id="imgKeyLEVEL" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblLEVEL" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtLEVEL" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnLEVEL" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 8 //-->
<asp:imagebutton id="imgKeyWTS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblWTS" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtWTS" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnWTS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 9 //-->
<asp:imagebutton id="imgKeyACTUAL_WTS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblACTUAL_WTS" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtACTUAL_WTS" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnACTUAL_WTS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 10 //-->
<asp:imagebutton id="imgKeyWTS_STATUS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblWTS_STATUS" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtWTS_STATUS" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnWTS_STATUS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 11 //-->
<asp:imagebutton id="imgKeyEXAM_RESULT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblEXAM_RESULT" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtEXAM_RESULT" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnEXAM_RESULT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 12 //-->
<asp:imagebutton id="imgKeyFINAL_STATUS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblFINAL_STATUS" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtFINAL_STATUS" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnFINAL_STATUS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<asp:ImageButton ID="imgKeySkill1" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblSkill1" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtSkill1" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnSkill1" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeySkill2" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblSkill2" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtSkill2" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnSkill2" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeySkill3" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblSkill3" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtSkill3" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnSkill3" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeySkill4" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblSkill4" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtSkill4" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnSkill4" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeySkill5" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblSkill5" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtSkill5" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnSkill5" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeySkill6" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblSkill6" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtSkill6" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnSkill6" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeySkill7" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblSkill7" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtSkill7" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnSkill7" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeySkill8" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblSkill8" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtSkill8" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnSkill8" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeySkill9" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblSkill9" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtSkill9" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnSkill9" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeySkill10" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblSkill10" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtSkill10" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnSkill10" runat="server" Width="21px" Height="21px"/>

<!--// 19 //-->
<asp:imagebutton id="imgKeyEVALUATORBY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblEVALUATORBY" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtEVALUATORBY" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnEVALUATORBY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 20 //-->
<asp:imagebutton id="imgKeyREMARK" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblREMARK" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtREMARK" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnREMARK" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 21 //-->
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
