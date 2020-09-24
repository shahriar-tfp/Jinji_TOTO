<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PreApproved_Overtime.aspx.vb" Inherits="PAGES_Roster_PreApproved_Overtime" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>RWEM_OT_APPLICATION</title>
<link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server"> 
<form id="PreApproved_Overtime" runat="server">
<div>
<asp:Panel ID="pnlEdit" runat="server">
<!--// 1 //-->
<asp:imagebutton id="imgKeyCOMPANY_PROFILE_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCOMPANY_PROFILE_CODE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCOMPANY_PROFILE_CODE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCOMPANY_PROFILE_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 2 //-->
<asp:imagebutton id="imgKeyFORMID" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblFORMID" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtFORMID" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnFORMID" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 3 //-->
<asp:imagebutton id="imgKeyEMPLOYEE_PROFILE_ID" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblEMPLOYEE_PROFILE_ID" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtEMPLOYEE_PROFILE_ID" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnEMPLOYEE_PROFILE_ID" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 4 //-->
<asp:imagebutton id="imgKeyDATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblDATE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtDATE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnDATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 5 //-->
<asp:imagebutton id="imgKeyOCP_ID_DIVISION" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOCP_ID_DIVISION" runat="server" Width="140px" ></asp:label>
<asp:dropdownlist id="ddlOCP_ID_DIVISION" runat="server" Width="150px" autopostback =true></asp:dropdownlist>
<asp:HiddenField ID="txtOCP_ID_DIVISION" runat="server"></asp:HiddenField>
<asp:imagebutton id="imgbtnOCP_ID_DIVISION" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 6 //-->
<asp:imagebutton id="imgKeyOCP_ID_DEPARTMENT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOCP_ID_DEPARTMENT" runat="server" Width="140px" ></asp:label>
<asp:dropdownlist id="ddlOCP_ID_DEPARTMENT" runat="server" Width="150px" autopostback =true></asp:dropdownlist>
<asp:HiddenField ID="txtOCP_ID_DEPARTMENT" runat="server"></asp:HiddenField>
<asp:imagebutton id="imgbtnOCP_ID_DEPARTMENT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 6.5 //-->
<asp:imagebutton id="imgKeyOCP_ID_TMS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOCP_ID_TMS" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtOCP_ID_TMS" runat="server" Width="150px" visible="false"></asp:textbox>
<asp:dropdownlist id="ddlOCP_ID_TMS" runat="server" Width="150px" autopostback =true></asp:dropdownlist>
<asp:imagebutton id="imgbtnOCP_ID_TMS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 7 //-->
<asp:imagebutton id="imgKeyOCP_ID_SHIFT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOCP_ID_SHIFT" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtOCP_ID_SHIFT" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnOCP_ID_SHIFT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 8 //-->
<asp:imagebutton id="imgKeyOCP_ID_DAY_TYPE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOCP_ID_DAY_TYPE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtOCP_ID_DAY_TYPE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnOCP_ID_DAY_TYPE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 9 //-->
<asp:imagebutton id="imgKeyACTUAL_WORK_HOUR" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblACTUAL_WORK_HOUR" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtACTUAL_WORK_HOUR" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnACTUAL_WORK_HOUR" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 10 //-->
<asp:imagebutton id="imgKeyTOBE_WORK_HOUR" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblTOBE_WORK_HOUR" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtTOBE_WORK_HOUR" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnTOBE_WORK_HOUR" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 11 //-->
<asp:imagebutton id="imgKeyOVERTIME_HOUR" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOVERTIME_HOUR" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtOVERTIME_HOUR" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnOVERTIME_HOUR" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 12 //-->
<asp:imagebutton id="imgKeyOCP_ID_SHIFT_CHANGE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOCP_ID_SHIFT_CHANGE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtOCP_ID_SHIFT_CHANGE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnOCP_ID_SHIFT_CHANGE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 13 //-->
<asp:imagebutton id="imgKeyREASON" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblREASON" runat="server" Width="140px" ></asp:label>
<asp:dropdownlist id="ddlREASON" runat="server" Width="150px" ></asp:dropdownlist>
<asp:imagebutton id="imgbtnREASON" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 14 //-->
<!--<asp:imagebutton id="imgKeyOPTION_STATUS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOPTION_STATUS" runat="server" Width="140px" ></asp:label>
<asp:dropdownlist id="ddlOPTION_STATUS" runat="server" Width="150px" autopostback =true></asp:dropdownlist>
<asp:imagebutton id="imgbtnOPTION_STATUS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
-->
<!--// 15 //-->
<!--<asp:imagebutton id="imgKeyOPTION_PAY_STATUS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOPTION_PAY_STATUS" runat="server" Width="140px" ></asp:label>
<asp:dropdownlist id="ddlOPTION_PAY_STATUS" runat="server" Width="150px" autopostback =true></asp:dropdownlist>
<asp:imagebutton id="imgbtnOPTION_PAY_STATUS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
-->
<asp:HiddenField ID="txtAction" runat="server" />

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

<asp:label id="lblAdjustOVERTIME_HOUR" runat="server" Width="140px"></asp:label>
<asp:textbox id="txtAdjustOVERTIME_HOUR" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnAdjustOVERTIME_HOUR" runat="server" Width="21px" Height="21px"></asp:imagebutton>
<asp:label id="lblAdjustOCP_ID_SHIFT_CHANGE" runat="server" Width="140px"></asp:label>
<asp:textbox id="txtAdjustOCP_ID_SHIFT_CHANGE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnAdjustOCP_ID_SHIFT_CHANGE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<asp:label id="lblEditSubO" runat="server" Width="140px" text="Sub Ordinator"></asp:label>
<asp:dropdownlist id="ddlEditSubO" runat="server" Width="350px" autopostback="true"></asp:dropdownlist>

<div id="divGridview" style="top: 225px;left: 15px;position :absolute; width: 100%; height: 400px; overflow: scroll">
    <asp:panel id="pnlgridview1" ScrollBars="Auto" runat="server" visible="true" Height="100%">
    <asp:CheckBox ID="chkAAll" AutoPostBack="true" Width="120px" CssClass="wordstyle" BorderStyle="None" Runat="server" visible="true" />
        <asp:gridview id="myGridView1" Width="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
            CssClass="wordstyle" cellspacing="0" cellpadding="1" EmptyDataText="No data found!">
            <AlternatingRowStyle BackColor="#F2F4FF" />
            <EditRowStyle BackColor="#FFC7C6" CssClass="dgstyle_i" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
            <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
            <columns>
                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkS1" BorderStyle="None" Runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </columns>
        </asp:gridview>
    </asp:panel>
    <asp:panel id="pnlgridview2" ScrollBars="Horizontal" runat="server" visible="false">
        <asp:gridview id="myGridview2" Width="100%" Height="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
            CssClass="wordstyle" cellspacing="0" cellpadding="1" EmptyDataText="No data found!">
            <AlternatingRowStyle BackColor="#F2F4FF" />
            <EditRowStyle BackColor="#FFC7C6" CssClass="dgstyle_i" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
            <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
            <columns>
                <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                    <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkS2" BorderStyle="None" Runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </columns>
        </asp:gridview>
    </asp:panel>
</div>

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
					        <table id="Table90" cellspacing="0" cellpadding="0" border="0" runat="server">
					            <tr>
					                <td style ="height:15px"><asp:label id="lblSubO" runat="server" Width="140px" text="Sub Ordinator"></asp:label><asp:dropdownlist id="ddlSubO" runat="server" Width="350px" autopostback="true"></asp:dropdownlist></td>
					                <td style ="height:15px"> &nbsp;</td>
					            </tr>
					        </table>
					        <table id="Table9" cellspacing="0" cellpadding="0" border="0" runat="server">
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
