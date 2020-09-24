<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Leave_Approval.aspx.vb" Inherits="Pages_Approval_Leave_Approval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>JINJI : Leave Approval Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server">
    <form id="Leave_Approval" runat="server">
    <div>
    <asp:Panel id="pnlTitle" runat="server" visible="True">
    <asp:placeholder id="MyPH" Runat="server"></asp:placeholder>
    <table id="Table1" style="LEFT: 10px; POSITION:absolute; TOP: 10px" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
		<tr>
			<td>
			    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                    <tr>
                        <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif);width :5px"></td>
                        <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); vertical-align:bottom"><asp:Label ID="lblTitle" runat="server"></asp:Label></td>
                    </tr>
                </table>
				<table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
					<tr>
					    <td>
					        <table id="Table4" cellspacing="0" cellpadding="0" border="0" runat="server">
					            <tr>
					                <td style ="height:15px"></td>
					            </tr>
					        </table> 
					        <table id="Table5" cellspacing="0" cellpadding="0" border="0" runat="server">
					            <tr>
					                <td style="width:25px"><asp:image id="imgCompany_Profile_ID" runat="server" Height="21px" Width="21px"></asp:image></td>
						            <td style="width:125px"><asp:label id="lblCompany_Profile_ID" runat="server" Width="120px"></asp:label></td>
						            <td style="width:155px"><asp:Textbox id="txtCompany_Profile_ID" runat="server" Width="150px"></asp:Textbox></td>  
						            <td style="width:5px"></td>
						            <td style="width:305px"><asp:Textbox id="txtCompany_Name" runat="server" Width="300px"></asp:Textbox></td> 
						            <td style="width:30px"><asp:Imagebutton id="imgBtnCompany_Profile_ID" runat="server" Height="21px"></asp:Imagebutton></td>
					            </tr>
					        </table> 
					        <table id="Table6" cellspacing="0" cellpadding="0" border="0" runat="server">
					            <tr>
					                <td colspan="6" style="height:10px"></td>
					            </tr>
					            <tr>
					                <td><asp:Image ID="imgBlank02" Width="5px" Height="25px" runat="server" /></td>
					                <td>
					                    <asp:LinkButton id="lnkBtnViewPending" runat="server" CssClass="wordstyle" Text="[ Pending Leave ]" Enabled="false" ></asp:LinkButton>
					                    <asp:LinkButton id="lnkBtnClosePending" runat="server" CssClass="wordstyle11" Text="[ Pending Leave ]" Visible="false" ></asp:LinkButton>
					                </td>
					                <td><asp:Image ID="imgBlank03" Width="10px" Height="25px" runat="server" /></td>
					                <td>
					                    <asp:LinkButton id="lnkBtnViewApproved" runat="server" CssClass="wordstyle" Text="[ Approved Leave ]" Enabled="false" ></asp:LinkButton>
					                    <asp:LinkButton id="lnkBtnCloseApproved" runat="server" CssClass="wordstyle11" Text="[ Approved Leave ]" Visible="false" ></asp:LinkButton>
					                </td>
					                <td><asp:Image ID="imgBlank04" Width="10px" Height="25px" runat="server" /></td>
					                <td>
					                    <asp:LinkButton id="lnkBtnViewReject" runat="server" CssClass="wordstyle" Text="[ Reject Leave ]" Enabled="false" ></asp:LinkButton>
					                    <asp:LinkButton id="lnkBtnCloseReject" runat="server" CssClass="wordstyle11" Text="[ Reject Leave ]" Visible="false" ></asp:LinkButton>
					                </td>
					                <td><asp:Image ID="imgBlank05" Width="10px" Height="25px" runat="server" /></td>
					                <td>
					                    <asp:LinkButton id="lnkBtnViewBalance" runat="server" CssClass="wordstyle" Text="[ Leave Balance ]" Enabled="false" ></asp:LinkButton>
					                    <asp:LinkButton id="lnkBtnCloseBalance" runat="server" CssClass="wordstyle11" Text="[ Leave Balance ]" Visible="false" ></asp:LinkButton>
					                </td>
					            </tr>
					        </table>
					    </td> 
					</tr> 
					<tr>
					    <td>
				            <table id="Table7" cellspacing="0" cellpadding="0" border="0" runat="server">
				                <tr>
				                    <td style="height:2px"></td>
				                </tr>
				            </table> 
				            <asp:Panel id="pnlBalance" runat="server" visible="False" style=" background-color:#EFEFEF">
			                  <table cellspacing="0" cellpadding="0" border="0" width="100%" runat="server">
					            <tr>
					                <td style="height:5px"></td>
					            </tr>
					        </table> 
					        <table cellspacing="0" cellpadding="0" border="0" width="100%" runat="server">
					            <tr>
		                                <td style="width:5px">&nbsp;</td>
		                                <td style="width:100%" align="left">
		                                <asp:Label id="lblBalAID" Width="120px" CssClass="wordstyle" runat="server"></asp:Label>
		                                <asp:Dropdownlist id="ddlAID" Width="80%" AutoPostBack="True" CssClass="wordstyle" runat="server"></asp:Dropdownlist>
		                                   </td>
	                               </tr>
		                       </table>
			 
			                  <table cellspacing="0" cellpadding="0" border="0" width="100%" runat="server">
				                    <tr>
				                        <td>
				                            <div style="margin-left:5px; margin-top:5px; margin-right:5px">
					                            <asp:panel id="pnlBalVw" ScrollBars="Auto" runat="server" visible="true">
						                            <asp:gridview id="BalGv" Width="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
							                            CssClass="wordstyle" cellspacing="0" cellpadding="1" EmptyDataText="No data found!">
                                                        <AlternatingRowStyle BackColor="#F2F4FF" />
							                            <EditRowStyle BackColor="#FFC7C6" CssClass="dgstyle_i" />
							                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
							                            <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
							                            <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
						                            </asp:gridview>
								                </asp:panel>
								             </div>
				                        </td>
				                    </tr> 
				                </table>
                        </asp:Panel>
				            <%--Panel Part 1--%>
					        <asp:Panel id="pnlpart1" runat="server" visible="False">
					           <table id="Table8" cellspacing="0" cellpadding="0" border="0" runat="server">
				                   <tr>
				                       <td style="height:5px"></td>
				                   </tr>
				               </table>
				               <asp:Panel id="pnlpart3" runat="server" visible="False">
			                       <table id="Table17" cellspacing="0" cellpadding="0" border="0" runat="server">
			                            <tr>
			                                <td style="width:5px">&nbsp;</td>
			                                <td style="width:200px"><asp:RadioButton id="optQFRLeave" GroupName="RQualified" AutoPostBack="true" Width="180px" CssClass="wordstyle" runat="server" /></td>
			                                <td style="width:30px" align="right"><asp:Label id="lblYear" Width="30px" CssClass="wordstyle" runat="server"></asp:Label></td>
			                                <td style="width:5px">&nbsp;</td>
			                                <td style="width:105px"><asp:Dropdownlist id="ddlYear" CssClass="wordstyle" runat="server" Width="100"></asp:Dropdownlist></td>      
			                            </tr>
				                        <tr>
				                            <td style="width:5px">&nbsp;</td>
				                            <td style="width:200px"><asp:RadioButton id="optRLDetails" GroupName="RQualified" AutoPostBack="true" Width="180px" CssClass="wordstyle" runat="server" /></td>
			                                <td style="width:5px" colspan="3">&nbsp;</td>
			                            </tr>
			                        </table>
			                        <table id="Table13" cellspacing="0" cellpadding="0" border="0" runat="server">
				                       <tr>
				                           <td style="height:5px"></td>
				                       </tr>
				                    </table>
			                   </asp:Panel> 
                                
				               <asp:Panel id="pnlpart2" runat="server" visible="False">
				                   <table id="Table16" cellspacing="0" cellpadding="0" border="0" width="99%" runat="server">
		                               <tr>
		                                   <td style="width:5px">&nbsp;</td>
		                                   <td style="width:125px"><asp:CheckBox ID="chkAEmployee" Checked="true" AutoPostBack="true" Width="120px" BorderStyle="None" CssClass="wordstyle" Runat="server" /></td>
		                                   <td style="width:120px" align="right">
		                                        <asp:Label id="lblAID" Width="120px" CssClass="wordstyle" runat="server"></asp:Label>
		                                        <asp:Dropdownlist id="ddlOption_Type" Width="120px" AutoPostBack="True" CssClass="wordstyle" runat="server"></asp:Dropdownlist>
		                                   </td>
		                                   <td style="width:5px">&nbsp;</td>
		                                   <td style="width:100%"><asp:Textbox id="txtAID" Width="98%" CssClass="wordstyle" runat="server"></asp:Textbox></td>
		                                   <td style="width:25px"><asp:Imagebutton id="imgBtnAID" Height="21px" runat="server"></asp:Imagebutton></td>
		                                   <td style="width:80px" align="right"><asp:Label id="lblADateFrom" Width="80px" CssClass="wordstyle" runat="server"></asp:Label></td>
		                                   <td style="width:5px">&nbsp;</td>
		                                   <td style="width:155px"><asp:Textbox id="txtADateFrom" Width="150px" CssClass="wordstyle" runat="server"></asp:Textbox></td> 
		                                   <td style="width:30px"><asp:Imagebutton id="imgBtnADateFrom" Height="21px" runat="server"></asp:Imagebutton></td> 
		                               </tr>
		                               <tr>
		                                   <td style="width:5px">&nbsp;</td>
		                                   <td style="width:125px"><asp:CheckBox ID="chkADepartment" Checked="false" AutoPostBack="true" Width="120px" BorderStyle="None" CssClass="wordstyle" Runat="server" /></td>
		                                   <td align="right"><asp:Label id="lblAName" Width="120px" runat="server"></asp:Label></td>
		                                   <td style="width:5px"></td>
		                                   <td><asp:Textbox id="txtANAME" Width="98%" CssClass="wordstyle" runat="server"></asp:Textbox></td> 
		                                   <td><asp:Imagebutton id="imgBtnAName" Height="21px" runat="server"></asp:Imagebutton></td>
		                                   <td align="right"><asp:Label id="lblADateTo" Width="80px" CssClass="wordstyle" runat="server"></asp:Label></td>
		                                   <td style="width:5px"></td>
			                               <td><asp:Textbox id="txtADateTo" Width="150px" CssClass="wordstyle" runat="server"></asp:Textbox></td>
			                               <td><asp:Imagebutton id="imgBtnADateTo" Height="21px" runat="server"></asp:Imagebutton></td>
		                               </tr>
		                               <tr>
		                                   <td style="width:5px">&nbsp;</td>
		                                   <td style="width:125px"><asp:CheckBox ID="chkListALL" Checked="false" AutoPostBack="true" Width="120px" BorderStyle="None" CssClass="wordstyle" Runat="server" /></td>
		                                   <td align="right"></td>
		                                   <td style="width:5px"></td>
		                                   <td></td> 
		                                   <td></td>
		                                   <td align="right"></td>
		                                   <td style="width:5px"></td>
			                               <td></td>
			                               <td></td>
		                               </tr> 
			                       </table>
			                   </asp:Panel>
			                  
					           <table id="Table9" cellspacing="0" cellpadding="0" border="0" width="100%" runat="server">
				                    <tr>
				                        <td>
				                            <div style="margin-left:5px; margin-top:5px; margin-right:5px">
					                            <asp:panel id="pnlgridview1" ScrollBars="Auto" runat="server" visible="true">
						                            <asp:gridview id="myGridView1" Width="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
							                            CssClass="wordstyle" cellspacing="0" cellpadding="1" EmptyDataText="No data found!">
                                                        <AlternatingRowStyle BackColor="#F2F4FF" />
							                            <EditRowStyle BackColor="#FFC7C6" CssClass="dgstyle_i" />
							                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
							                            <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
							                            <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
							                            <columns>
							                                <%--<asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
									                            <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
									                            <ItemTemplate>
										                            <asp:CheckBox ID="chkS1" BorderStyle="None" Runat="server" />
									                            </ItemTemplate>
								                            </asp:TemplateField>--%>
								                            <asp:TemplateField HeaderText="Approve" ItemStyle-HorizontalAlign="Center">
									                            <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
									                            <ItemTemplate>
										                            <asp:CheckBox ID="chkA1" BorderStyle="None" Runat="server" />
									                            </ItemTemplate>
								                            </asp:TemplateField>
								                            <asp:TemplateField HeaderText="Reject" ItemStyle-HorizontalAlign="Center">
									                            <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
									                            <ItemTemplate>
										                            <asp:CheckBox ID="chkR1" BorderStyle="None" Runat="server" />
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
										                    <%--<asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
									                            <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
									                            <ItemTemplate>
										                            <asp:CheckBox ID="chkS2" BorderStyle="None" Runat="server" />
									                            </ItemTemplate>
								                            </asp:TemplateField>--%>
								                            <asp:TemplateField HeaderText="Approve" ItemStyle-HorizontalAlign="Center">
									                            <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
									                            <ItemTemplate>
										                            <asp:CheckBox ID="chkA2" BorderStyle="None" Runat="server" />
									                            </ItemTemplate>
								                            </asp:TemplateField>
								                            <asp:TemplateField HeaderText="Reject" ItemStyle-HorizontalAlign="Center">
									                            <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
									                            <ItemTemplate>
										                            <asp:CheckBox ID="chkR2" BorderStyle="None" Runat="server" />
									                            </ItemTemplate>
								                            </asp:TemplateField>
							                            </columns>
									                </asp:gridview>
								                </asp:panel>
								                <asp:panel ID="pnlListBox" runat="server" Visible ="false" >
								                    <asp:ListBox ID="lstEmpID" CssClass="wordstyle" runat="server"></asp:ListBox>
								                    <asp:ListBox ID="lstLeaveApply" CssClass="wordstyle" runat="server"></asp:ListBox>
								                    <asp:ListBox ID="lstLeaveTotal" CssClass="wordstyle" runat="server"></asp:ListBox>
								                </asp:panel> 
								            </div>
				                        </td>
				                    </tr> 
				                    <tr>
								        <td>
									        <asp:panel id="pnlprevnext1" runat="server" visible="true">
										        <table cellspacing="0" cellpadding="0" width="100%" border="0">
									                <tr>
									                    <td style="width:5px">&nbsp;</td>
										                <td align="left" style="width:200px">
											                <p class="wordstyle">&nbsp;Page&nbsp;
												                <asp:Label id="CurrentPage1" runat="server" CssClass="wordstyle"></asp:Label>&nbsp;of&nbsp;
												                <asp:Label id="TotalPages1" runat="server" CssClass="wordstyle"></asp:Label>
												                <asp:Label id="lbltotal1" runat="server" CssClass="wordstyle"></asp:Label></p>
										                </td>
										                <td align="center">
											                <asp:LinkButton id="FirstPage1" runat="server" CssClass="wordstyle" CommandName="First" OnCommand="NavigationLink1_Click"
												                Text="[ First ]"></asp:LinkButton>
											                <asp:LinkButton id="PrevPage1" runat="server" CssClass="wordstyle" CommandName="Prev" OnCommand="NavigationLink1_Click"
												                Text="[ Prev ]"></asp:LinkButton>
											                <asp:LinkButton id="NextPage1" runat="server" CssClass="wordstyle" CommandName="Next" OnCommand="NavigationLink1_Click"
												                Text="[ Next ]"></asp:LinkButton>
											                <asp:LinkButton id="LastPage1" runat="server" CssClass="wordstyle" CommandName="Last" OnCommand="NavigationLink1_Click"
												                Text="[ Last ]"></asp:LinkButton></td>
										                <td align="right">
											                <asp:Label id="lblGoToPage1" runat="server" Text="Go To Page" CssClass="wordstyle1"></asp:Label>
											                <asp:Textbox id="txtGoToPage1" runat="server" Width="35px" CssClass="toppos"></asp:Textbox>
											                <asp:Imagebutton id="imgBtnGoToPage1" Height="21px" ImageAlign="AbsBottom" Runat="server"></asp:Imagebutton></td>
											            <td style="width:5px">&nbsp;</td>
									                </tr>
								                </table>
								                <table id="Table11" cellspacing="0" cellpadding="0" border="0" runat="server">
				                                   <tr>
				                                       <td style="height:5px"><asp:CheckBox ID="chkAAll" AutoPostBack="true" Width="120px" CssClass="wordstyle" BorderStyle="None" Runat="server" visible="true"/></td>
				                                       <td style="height:5px"><asp:CheckBox ID="chkRAll" AutoPostBack="true" Width="120px" CssClass="wordstyle" BorderStyle="None" Runat="server" visible="true"/></td>
				                                   </tr>
				                                </table>
									        </asp:panel>
								        </td>
							        </tr>
				                </table>
				            </asp:Panel> 
					    </td>
					</tr>
					<tr>
					    <td>
					        <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
	                                <%--pnlbutton--%>
		                            <asp:Panel ID="pnlbutton" runat="server" Visible="false">
                                    <td style="width:2px">&nbsp;</td>
	                                <td align="left">
                                        <asp:Imagebutton id="imgBtnLeaveDetail" runat="server" Height="21px" Width="74px"></asp:Imagebutton>
                                        <asp:Imagebutton id="imgBtnUpdate" runat="server" Width="74" Height="21" OnClientClick="return confirm('Confirmation! Revise Data ?')"></asp:Imagebutton>
                                        <asp:Imagebutton id="imgBtnCancel" runat="server" Width="74" Height="21"></asp:Imagebutton>
                                        <asp:Imagebutton id="imgBtnSearch" runat="server" Width="74" Height="21"></asp:Imagebutton>
                                        <asp:Imagebutton id="imgBtnPrint" runat="server" Height="21px" Width="74px"></asp:Imagebutton>
		                            </td>
		                            </asp:Panel> 	
	                                <td>
                                    <%--pnlresult--%>
			                        <asp:Panel ID="pnlresult" runat="server" Visible="true">
			                            <table cellspacing="0" cellpadding="0" border="0">
					                        <tr>
                                                <td><asp:Image ID="Image1" ImageUrl="../../../images/company/default/gif/px1.gif" Width="5px" runat="server" /></td>
                                                <td align="left"><asp:Label id="lblresult" runat="server" CssClass="wordstyle2"></asp:Label></td>
                                            </tr>
				                        </table> 
			                        </asp:Panel>
	                                </td>
	                            </tr>
	                        </table>
					    </td>
					</tr>
				</table> 
			</td> 
		</tr>
	</table></asp:Panel> 
    </div>
    </form>
</body>
</html>
