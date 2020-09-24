
<%@ Page Language="VB" AutoEventWireup="false" EnableEventValidation="false"  CodeFile="Employee_Leave_Application.aspx.vb" Inherits="Pages_Leave_Employee_Leave_Application" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>JINJI : Leave - Employee Application Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
    
    <script type="text/javascript">
    function focus_txtempid()
    {
        if (document.getElementById("txtEmployee_Profile_ID").value == " Employee Code ") 
            document.getElementById("txtEmployee_Profile_ID").value = "";
        if (document.getElementById("txtEmployee_Profile_ID").value == "")
            document.getElementById("txtEmployee_Profile_ID").style.color="black";
        if (document.getElementById("txtEmployee_Profile_ID").value != " Employee Code ")
        {
            document.getElementById("txtEmployee_Profile_ID").value = "";
            document.getElementById("txtEmployee_Profile_ID").style.color="black";
        }
    }
    function focus_txtempname()
    {
        if (document.getElementById("txtEmployee_Name").value == " Employee Name ") 
            document.getElementById("txtEmployee_Name").value = "";
        if (document.getElementById("txtEmployee_Name").value == "")
            document.getElementById("txtEmployee_Name").style.color="black";
        if (document.getElementById("txtEmployee_Name").value != " Employee Name ")
        {
            document.getElementById("txtEmployee_Name").value = "";
            document.getElementById("txtEmployee_Name").style.color="black";
        }
    }
    function blur_txtempid()
    {
        if (document.getElementById("txtEmployee_Profile_ID").value == "") 
        {
            document.getElementById("txtEmployee_Profile_ID").value = " Employee Code ";
            document.getElementById("txtEmployee_Profile_ID").style.color="#A2A2A2";
        } 
    }
    function blur_txtempname()
    {
        if (document.getElementById("txtEmployee_Name").value == "") 
        {
            document.getElementById("txtEmployee_Name").value = " Employee Name ";
            document.getElementById("txtEmployee_Name").style.color="#A2A2A2";
        }
    }
    </script>
</head>
<body id="body" runat="server">
    <form id="Employee_Leave_Application" runat="server">
    <div>
    <asp:Panel id="pnlTitle" runat="server" visible="True">
    <asp:placeholder id="MyPH" Runat="server"></asp:placeholder>
    <table id="Table2" style="LEFT: 10px; POSITION: absolute; TOP: 10px" cellspacing="0" cellpadding="0" border="0" runat="server">
		<tr>
			<td>
			    <table cellspacing="0" cellpadding="0" border="0" runat="server">
                    <tr>
                        <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif);width :5px"></td>
                        <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); vertical-align:bottom"><asp:Label ID="lblTitle" runat="server"></asp:Label></td>
                    </tr>
                </table>
				<table cellspacing="0" cellpadding="0" border="0" runat="server">
					<tr>
					    <td>
					        <table cellspacing="0" cellpadding="0" border="0" runat="server">
					            <tr>
					                <td style ="height:15px"></td>
					            </tr>
					        </table> 
					        <table cellspacing="0" cellpadding="0" border="0" runat="server">
					            <tr>
					                <td style="width:25px"><asp:image id="imgEmployee_Profile_ID" runat="server" Height="21px" Width="21px"></asp:image></td>
						            <td style="width:125px"><asp:label id="lblEmployee_Profile_ID" runat="server" Width="120px"></asp:label></td>
						            <td style="width:155px"><asp:textbox id="txtEmployee_Profile_ID" runat="server" Width="150px"></asp:textbox></td>  
						            <td style="width:5px"></td>
						            <td style="width:305px"><asp:textbox id="txtEmployee_Name" runat="server" Width="300px"></asp:textbox></td> 
						            <td style="width:30px"><asp:imagebutton id="imgBtnEmployee_Profile_ID" runat="server" Height="21px"></asp:imagebutton></td>
					            </tr>
					        </table> 
					        <table cellspacing="0" cellpadding="0" border="0" runat="server">
					            <tr>
					                <td colspan="8" style="height:10px"></td>
					            </tr>
					            <tr>
					                <td><asp:Image ID="imgBlank02" Width="5px" Height="25px" runat="server" /></td>
					                <td>
					                    <asp:LinkButton id="lnkBtnViewInfo" runat="server" CssClass="wordstyle" Text="[ Employee Information ]" Enabled="false" ></asp:LinkButton>
					                    <asp:LinkButton id="lnkBtnCloseInfo" runat="server" CssClass="wordstyle11" Text="[ Employee Information ]" Visible="false" ></asp:LinkButton>
					                </td>
					                <td><asp:Image ID="imgBlank03" Width="10px" Height="25px" runat="server" /></td>
					                <td>
					                    <asp:LinkButton id="lnkBtnViewRecord" runat="server" CssClass="wordstyle" Text="[ Leave Application & Summary ]" Enabled="false" ></asp:LinkButton>
					                    <asp:LinkButton id="lnkBtnCloseRecord" runat="server" CssClass="wordstyle11" Text="[ Leave Application & Summary ]" Visible="false" ></asp:LinkButton>
					                </td>
					                <td><asp:Image ID="imgBlank04" Width="10px" Height="25px" runat="server" /></td>
					                <td>
					                    <asp:LinkButton id="lnkBtnViewEnquiry" runat="server" CssClass="wordstyle" Text="[ Leave Enquiry ]" Enabled="false" ></asp:LinkButton>
					                    <asp:LinkButton id="lnkBtnCloseEnquiry" runat="server" CssClass="wordstyle11" Text="[ Leave Enquiry ]" Visible="false" ></asp:LinkButton>
					                </td>
					                <td><asp:Image ID="imgBlank05" Width="10px" Height="25px" runat="server" /></td>
					                <td>
					                    <asp:LinkButton id="lnkBtnViewCancellation" runat="server" CssClass="wordstyle" Text="[ Leave Cancellation / Leave History ]" Enabled="false" ></asp:LinkButton>
					                    <asp:LinkButton id="lnkBtnCloseCancellation" runat="server" CssClass="wordstyle11" Text="[ Leave Cancellation / Leave History ]" Visible="false" ></asp:LinkButton>
					                </td>
					            </tr>
					        </table> 
					    </td> 
					</tr> 
					<tr>
					    <td>
					        <asp:panel id="pnlempinfo" ScrollBars="Auto" runat="server" visible="false">
					        <table cellspacing="0" cellpadding="0" border="0" width="100%" runat="server">
					            <tr>
					                <td style="height:5px"></td>
					            </tr>
					        </table> 
					        <table cellspacing="0" cellpadding="0" border="0" width="100%" runat="server">
					            <tr style=" background-color:#EFEFEF">
					                <td>
					                    <table id="Table3" cellspacing="0" cellpadding="0" border="0" runat="server">
					                        <tr>
					                            <td colspan="6">&nbsp;<asp:Label id="lblempinfo" runat="server"></asp:Label></td>
					                        </tr>
					                        <tr>
					                            <td style="width:120px">&nbsp;<asp:label id="lblEmployeeCode" runat="server" CssClass="wordstyle" Width="120px"></asp:label></td>
					                            <td style="width:5px">:</td>
						                        <td style="width:190px"><asp:label id="txtEmployeeCode" runat="server" CssClass="wordstyle" Width="190px"></asp:label></td>  
						                        <td style="width:120px">&nbsp;<asp:label id="lblEmployeeName" runat="server" CssClass="wordstyle" Width="120px"></asp:label></td>
						                        <td style="width:5px">:</td>
						                        <td style="width:190px"><asp:label id="txtEmployeeName" runat="server" CssClass="wordstyle" Width="190px"></asp:label></td>  
					                        </tr>
					                        <tr>
					                            <td style="width:120px">&nbsp;<asp:label id="lblDateJoin" runat="server" CssClass="wordstyle" Width="120px"></asp:label></td>
					                            <td style="width:5px">:</td>
						                        <td style="width:190px"><asp:label id="txtDateJoin" runat="server" CssClass="wordstyle" Width="190px"></asp:label></td>  
						                        <td style="width:120px">&nbsp;<asp:label id="lblDateConfirm" runat="server" CssClass="wordstyle" Width="120px"></asp:label></td>
						                        <td style="width:5px">:</td>
						                        <td style="width:190px"><asp:label id="txtDateConfirm" runat="server" CssClass="wordstyle" Width="190px"></asp:label></td>  
						                     </tr> 
						                     <tr visible="false">
						                        <td style="width:120px">&nbsp;<asp:label id="lblResignDate" runat="server" CssClass="wordstyle" Width="120px"></asp:label></td>
						                        <td style="width:5px">:</td>
						                        <td style="width:190px"><asp:label id="txtResignDate" runat="server" CssClass="wordstyle" Width="190px"></asp:label></td>  
						                        <td style="width:120px"></td>
						                        <td style="width:5px"></td>
						                        <td style="width:190px"></td>
						                     </tr>
						                     <tr>
						                        <td style="width:120px">&nbsp;<asp:label id="lblLenghtOfService" runat="server" CssClass="wordstyle" Width="120px"></asp:label></td>
						                        <td style="width:5px">:</td>
						                        <td style="width:190px"><asp:label id="txtLenghtOfService" runat="server" CssClass="wordstyle" Width="190px"></asp:label></td>    
						                        <td style="width:120px">&nbsp;<asp:label id="lblStatus" runat="server" CssClass="wordstyle" Width="120px"></asp:label></td>
						                        <td style="width:5px">:</td>
						                        <td style="width:190px"><asp:label id="txtStatus" runat="server" CssClass="wordstyle" Width="190px"></asp:label></td>
						                     </tr> 
						                     <tr>
						                        <td style="width:120px">&nbsp;<asp:label id="lblJobGrade" runat="server" CssClass="wordstyle" Width="120px"></asp:label></td>
						                        <td style="width:5px"><asp:label id="lblJobGradeQ" text=":" runat="server" CssClass="wordstyle"></asp:label></td>
						                        <td style="width:190px"><asp:label id="txtJobGrade" runat="server" CssClass="wordstyle" Width="190px"></asp:label></td>  
						                        <td style="width:120px">&nbsp;<asp:label id="lblJobTitle" runat="server" CssClass="wordstyle" Width="120px"></asp:label></td>
						                        <td style="width:5px"><asp:label id="lblJobTitleQ" text=":" runat="server" CssClass="wordstyle"></asp:label></td>
						                        <td style="width:190px"><asp:label id="txtJobTitle" runat="server" CssClass="wordstyle" Width="190px"></asp:label></td>  
						                     </tr>
						                     <tr>
						                        <td style="width:120px">&nbsp;<asp:label id="lblSupervisor" runat="server" CssClass="wordstyle" Width="120px"></asp:label></td>
						                        <td style="width:5px">:</td>
						                        <td style="width:190px"><asp:label id="txtSupervisor" runat="server" CssClass="wordstyle" Width="190px"></asp:label></td>
						                        <td style="width:120px">&nbsp;<asp:label id="lblDepartment" runat="server" CssClass="wordstyle" Width="120px"></asp:label></td>
						                        <td style="width:5px">:</td>
						                        <td style="width:190px"><asp:label id="txtDepartment" runat="server" CssClass="wordstyle" Width="190px"></asp:label></td>  
						                     </tr>
						                     <tr>
						                        <td style="width:120px">&nbsp;<asp:label id="lblLine" runat="server" CssClass="wordstyle" Width="120px"></asp:label></td>
						                        <td style="width:5px">:</td>
						                        <td style="width:190px"><asp:label id="txtLine" runat="server" CssClass="wordstyle" Width="190px"></asp:label></td>  
						                        <td style="width:120px"></td>
						                        <td style="width:5px"></td>
						                        <td style="width:190px"></td>
						                     </tr>
					                    </table> 
					                </td>
					            </tr>
					       </table>
					       </asp:panel>
					       <asp:Panel id="pnlpart1" runat="server" visible="True">
					       <table cellspacing="0" cellpadding="0" border="0" runat="server">
				                <tr>
				                    <td>
				                        <%--<div style="border-right-style:double; border-left-style:double; border-bottom-style:double; border-top-style:double; border-color:#F2F4FF; margin-left:5px; margin-top:5px; margin-right:5px">--%>
					                        <asp:panel id="pnlgridview1" ScrollBars="Auto" runat="server" visible="false">
						                        <asp:gridview id="myGridView1" runat="server" AutoGenerateColumns="false" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
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
										                        <asp:CheckBox ID="chkSelect1" BorderStyle="None" Runat="server" />
									                        </ItemTemplate>
								                        </asp:TemplateField>
								                        <asp:TemplateField HeaderText="Employee Profile Code" Visible="True">
										                    <ItemTemplate>
											                    <asp:Label ID="grvlblcode" Text='<%# Container.DataItem("Code") %>' Runat="server" />
										                    </ItemTemplate>
									                    </asp:TemplateField>
									                    <asp:TemplateField HeaderText="Salutation" Visible="True">
										                    <ItemTemplate>
											                    <asp:Label ID="grvlbloption_title" Text='<%# Container.DataItem("Title") %>' Runat="server" />
										                    </ItemTemplate>
									                    </asp:TemplateField>
									                    <asp:TemplateField HeaderText="Employee Name" Visible="True">
										                    <ItemTemplate>
											                    <asp:Label ID="grvlblname" Text='<%# Container.DataItem("Name") %>' Runat="server" />
										                    </ItemTemplate>
									                    </asp:TemplateField>
							                        </columns>
						                        </asp:gridview>
								            </asp:panel>
								        <%--</div>--%>
				                    </td>
				                </tr> 
				                <tr>
								    <td>
									    <asp:panel id="pnlprevnext1" runat="server" visible="false">
										    <table cellspacing="0" cellpadding="0" border="0" width="100%">
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
											            <asp:TextBox id="txtGoToPage1" runat="server" Width="35px" CssClass="toppos"></asp:TextBox>
											            <asp:ImageButton id="imgBtnGoToPage1" Height="21px" ImageAlign="AbsBottom" Runat="server"></asp:ImageButton></td>
											        <td style="width:5px">&nbsp;</td>
									            </tr>
									            <tr>
					                                <td style="width:110px" align="left" colspan="2"><asp:Image ID="imgblank01" Width="5px" Height="5px" runat="server" /><asp:ImageButton id="imgBtnSelect" runat="server" Height="21px" Width="74px" /></td>
					                                <td colspan="3"><asp:Label id="lblresult1" runat="server" CssClass="wordstyle2"></asp:Label></td>
					                            </tr>
								            </table>
									    </asp:panel>
								    </td>
							    </tr>
				            </table></asp:Panel> 
				            <!-- grv 2 -->
					    </td>
					</tr>
					<tr>
						<td>
						    <asp:Panel id="pnlpart2" runat="server" visible="false">
						        <table id="Table1" cellspacing="0" cellpadding="0" border="0" runat="server">
						            <tr >
					                    <td style="height:5px"></td>
					                </tr>
						            <tr >
						                <td style="width:330px; height:15px">&nbsp;<asp:Label id="lblleaveSummary" runat="server" Width="320px" Visible="false"></asp:Label></td>
						            </tr> 
				                    <tr >
				                        <td>
				                            <%--<div style="border-right-style:double; border-left-style:double; border-bottom-style:double; border-top-style:double; border-color:#F2F4FF; margin-left:5px; margin-top:5px; margin-right:5px">--%>
							                    <asp:panel id="pnlgridview2" ScrollBars="Horizontal" runat="server" visible="false">
									                <asp:gridview id="myGridview2" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
									                    OnRowCreated="OnRowDataBound"
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
													                <asp:CheckBox ID="chkSelect2" BorderStyle="None" Runat="server" />
												                </ItemTemplate>
											                </asp:TemplateField>
										                </columns>
									                </asp:gridview>
								                </asp:panel>
								            <%--</div>--%>
				                        </td>
				                    </tr> 
				                    <tr >
								        <td>
									        <asp:panel id="pnlprevnext2" runat="server" visible="false">
										        <table cellspacing="0" cellpadding="0" border="0" width="100%">
									                <tr>
									                    <td style="width:5px">&nbsp;</td>
										                <td align="left">
											                <p class="wordstyle">&nbsp;Page&nbsp;
												                <asp:Label id="CurrentPage2" runat="server" CssClass="wordstyle"></asp:Label>&nbsp;of&nbsp;
												                <asp:Label id="TotalPages2" runat="server" CssClass="wordstyle"></asp:Label>
												                <asp:Label id="lbltotal2" runat="server" CssClass="wordstyle"></asp:Label></p>
										                </td>
										                <td align="center">
											                <asp:LinkButton id="FirstPage2" runat="server" CssClass="wordstyle" CommandName="First" OnCommand="NavigationLink2_Click"
												                Text="[ First ]"></asp:LinkButton>
											                <asp:LinkButton id="PrevPage2" runat="server" CssClass="wordstyle" CommandName="Prev" OnCommand="NavigationLink2_Click"
												                Text="[ Prev ]"></asp:LinkButton>
											                <asp:LinkButton id="NextPage2" runat="server" CssClass="wordstyle" CommandName="Next" OnCommand="NavigationLink2_Click"
												                Text="[ Next ]"></asp:LinkButton>
											                <asp:LinkButton id="LastPage2" runat="server" CssClass="wordstyle" CommandName="Last" OnCommand="NavigationLink2_Click"
												                Text="[ Last ]"></asp:LinkButton></td>
										                <td align="right">
											                <asp:Label id="lblGoToPage2" runat="server" Text="Go To Page" CssClass="wordstyle1"></asp:Label>
											                <asp:TextBox id="txtGoToPage2" runat="server" Width="35px" CssClass="toppos"></asp:TextBox>
											                <asp:ImageButton id="imgBtnGoToPage2" Height="21px" ImageAlign="AbsBottom" Runat="server"></asp:ImageButton></td>
											            <td style="width:5px">&nbsp;</td>
									                </tr>
								                </table>
									        </asp:panel>
								        </td>
							        </tr>
				                </table> 
				            </asp:Panel> 
				            <%--pnlaction--%>
				            <asp:panel id="pnlaction" runat="server" visible="false">
				                <table cellspacing="0" cellpadding="0" width="100%" border="0">
							        <tr>
							            <td>&nbsp;</td>
							        </tr> 
							    </table> 
							    <asp:panel id="pnlByTIME" runat="server" visible="false">
							    <table cellspacing="0" cellpadding="0" border="0">
							    <tr>
									    <td style="width:25px"><asp:image id="imgTime_Apply_For" runat="server" Height="21px" Width="21px"></asp:image></td> 
										<td style="width:145px"><asp:Label id="lblTime_Apply_For" runat="server" Width="140px" CssClass="wordstyle"></asp:Label></td>
										<td style="width:161px"><asp:textbox id="txtTime_Apply_For" runat="server" Width="150px" CssClass="wordstyle"></asp:textbox></td>
										<td style="width:25px"><asp:imagebutton id="imgBtnTime_Apply_For" runat="server" Height="21px"></asp:imagebutton></td> 
										<td style="width:30px; text-align:center">To</td>
										<td style="width:155px;"><asp:textbox id="txtTime_Apply_To" runat="server" Width="150px" CssClass="wordstyle"></asp:textbox></td>
										<td style="width:25px"><asp:imagebutton id="imgBtnTime_Apply_To" runat="server" Height="21px"></asp:imagebutton></td> 
										<td style="width:200px"></td>
									</tr>
							    </table>
							    </asp:panel>
								<table cellspacing="0" cellpadding="0" border="0">
									<tr>
									    <td style="width:25px"><asp:image id="imgDate_Apply_For" runat="server" Height="21px" Width="21px"></asp:image></td> 
										<td style="width:145px"><asp:Label id="lblDate_Apply_For" runat="server" Width="140px" CssClass="wordstyle"></asp:Label></td>
										<td style="width:155px"><asp:textbox id="txtDate_Apply_From" runat="server" Width="150px" CssClass="wordstyle"></asp:textbox></td>
										<td style="width:25px"><asp:imagebutton id="imgBtnDate_Apply_From" runat="server" Height="21px"></asp:imagebutton></td> 
										<td style="width:30px; text-align:center">To</td>
										<td style="width:155px;"><asp:textbox id="txtDate_Apply_To" runat="server" Width="150px" CssClass="wordstyle"></asp:textbox></td>
										<td style="width:25px"><asp:imagebutton id="imgBtnDate_Apply_To" runat="server" Height="21px"></asp:imagebutton></td> 
										<td style="width:200px"></td>
									</tr>
									<tr>
									    <td><asp:image id="imgOption_Period" runat="server" Height="21px" Width="21px"></asp:image></td> 
										<td><asp:label id="lblOption_Period" runat="server" Width="140px" CssClass="wordstyle"></asp:label></td>
										<td><asp:dropdownlist id="ddlOption_Period" runat="server" Width="155px" CssClass="wordstyle" AutoPostBack="true"></asp:dropdownlist></td>
										<td colspan="5">&nbsp;</td>
									</tr>
									<tr>
									    <td><asp:image id="imgStandby_Employee" runat="server" Height="21px" Width="21px"></asp:image></td> 
										<td><asp:label id="lblStandby_Employee" runat="server" Width="140px" CssClass="wordstyle"></asp:label></td>
										<td><asp:textbox id="txtStandby_Employee" runat="server" Width="155px" CssClass="wordstyle"></asp:textbox></td>
										<td><asp:imagebutton id="imgBtnStandby_Employee" runat="server" Height="21px"></asp:imagebutton></td> 
										<td colspan="4">&nbsp;</td>
									</tr>
									<tr>
									    <td style="vertical-align:top"><asp:image id="imgReason" runat="server" Height="21px" Width="21px"></asp:image></td> 
										<td style="vertical-align:top"><asp:Label id="lblReason" runat="server" Width="140px" CssClass="wordstyle"></asp:Label></td>
										<td colspan="6"><asp:textbox id="txtReason" TextMode="MultiLine" runat="server" Width="362px" Height="32px" CssClass="wordstyle"></asp:textbox></td>
										
									</tr>
									<tr>
									    <td colspan="2" align="left" height="10px"></td>
									    <td colspan="6" align="left" style="vertical-align:bottom">
										    <asp:imagebutton id="imgBtnSubmit" runat="server" Height="21px" Width="74px"></asp:imagebutton>
										    <asp:imagebutton id="imgBtnClear" runat="server" Height="21px" Width="74px"></asp:imagebutton>

										</td>
									</tr>
								</table>
								<table cellspacing="0" cellpadding="0" width="100%" border="0">
							        <tr>
							            <td><asp:Panel ID="pnlListBox" runat="server" Visible ="false" >
							                <asp:ListBox ID="lstDateApply" runat="server"></asp:ListBox>
							                </asp:Panel>
							            </td>
							        </tr> 
							    </table> 
							</asp:panel>
							<%--pnlcalendar--%>
							<asp:Panel ID="pnlCalendar" ScrollBars="Auto" runat="server" Visible = "false" >
							    <table cellspacing="0" cellpadding="0" border="0" width="100%">
							        <tr >
					                    <td style="height:5px"></td>
					                </tr>
							        <tr >
							            <td style="height:15px">&nbsp;
						                    <asp:Label id="lblleaveEnquiry" runat="server" Width="320px" Visible="false"></asp:Label>
						                </td>
							        </tr>
							        <tr >
					                    <td style="height:5px"></td>
					                </tr>
									<tr >
										<td align="left">
										    <%--<div style="border-right-style:double; border-left-style:double; border-top-style:double; border-color:#F2F4FF; margin-left:5px; margin-top:5px; margin-right:5px; margin-bottom:0px">--%>
										        <table cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td style="width:10px"></td>
                                                        <td align="left" style="width:60px">
                                                            <asp:Label ID="lblYear" Width="60px" runat="server" Text="Year: " CssClass="wordstyle"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width:90px">
                                                            <asp:DropDownList ID="ddlYear" runat="server" Width="90px" CssClass="wordstyle" AutoPostBack="True"></asp:DropDownList>
                                                        </td>
                                                        <td style="width:10px"></td>
                                                        <td align="left" style="width:60px">
                                                            <asp:Label ID="lblMonth" runat="Server" Width="60px" Text="Month: " CssClass="wordstyle"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width:90px">
                                                            <asp:DropDownList ID="ddlMonth" runat="server" Width="90px" CssClass="wordstyle" AutoPostBack="True"></asp:DropDownList>
                                                        </td> 
                                                        <td style="width:10px"></td>
                                                    </tr>
                                                 </table> 
                                             <%--</div>--%> 
                                             <%--<div style="border-right-style:double; border-left-style:double; border-bottom-style:double; border-top-style:double; border-color:#F2F4FF; margin-left:5px; margin-top:0px; margin-right:5px; margin-bottom:5px">--%>
                                                 <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:calendar ID="calEventCalendar" runat="server" ShowGridLines="true" NextPrevFormat="FullMonth"   
				                                                ShowTitle="true" DayNameFormat="Full" SelectionMode="Day" Width="100%" cellpadding="2"
				                                                CssClass="wordstyle" BorderColor="#3333ff" FirstDayOfWeek="Monday" BackColor="#EFD5FF">
                                                                <TitleStyle BackColor="#004A95" ForeColor="#FFFFFF" Font-Bold="true" />
				                                                <NextPrevStyle BackColor="#004A95" ForeColor="#FFFFFF" Font-Bold="true" />
				                                                <TodayDayStyle BackColor="#FFD8D7" ForeColor="#00009D" />
				                                                <DayStyle BackColor="#E6FAFF" ForeColor="#0000cc" HorizontalAlign="Right" />
				                                                <WeekendDayStyle BackColor="#E6FAFF" ForeColor="#0000cc" />
				                                                <OtherMonthDayStyle BackColor="#EEEEEE" ForeColor="#33cccc" />
				                                                <SelectorStyle BackColor="#73A9FB" ForeColor="#33cccc" />
                                                            </asp:calendar>
                                                        </td>
                                                    </tr>
                                                    <tr >
					                                    <td style="height:5px"></td>
					                                </tr>
                                                </table>
                                            <%--</div>--%>
                                            <asp:Panel ID="pnlDayInfo" runat="server" Visible = "false" >
                                            <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td align="center">
                                                        <div style="border-right-style:double; border-left-style:double; border-top-style:double; border-bottom-style:double; border-color:#F2F4FF; margin-left:5px; margin-top:5px; margin-right:5px; margin-bottom:5px">
                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td style="width:10px"></td>
                                                                <td style="text-align:right; width:200px">
                                                                    <asp:label id="lblDateSel" runat="server" Width="180px" Text="Date(D/M/Y) : " CssClass="wordstyle"></asp:label>
                                                                </td>
                                                                <td style="text-align:left; width:200px">
                                                                    <asp:label id="txtDateSel" runat="server" Width="200px" BorderStyle="None" CssClass="wordstyle"></asp:label>
                                                                </td>
                                                                <td style="width:10px"></td>
                                                            </tr> 
                                                            <tr>
                                                                <td style="width:10px"></td>
                                                                <td style="text-align:right; vertical-align:top; width:200px">
                                                                    <asp:label id="lblLeaveInfo" runat="server" Width="180px" Text="Reference : " CssClass="wordstyle"></asp:label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:GridView id="gvLeaveInfo" Width="100%" CssClass="wordstyle" runat="server"></asp:GridView>
                                                                </td>
                                                                <td style="width:10px"></td>
                                                            </tr>
                                                        </table></div>
                                                    </td>
                                                </tr>
                                            </table> 
                                            </asp:Panel> 
										</td>
									</tr>
									<tr >
					                    <td style="height:5px"></td>
					                </tr>
								</table> 
							</asp:Panel>
							<%--pnlcancellation--%>
							<asp:Panel ID="pnlCancellation" runat="server" Visible = "false" >
							<table cellspacing="0" cellpadding="0" border="0" width="100%">
							        <tr >
					                    <td style="height:5px"></td>
					                </tr>
						            <tr >
						                <td style="width:330px; height:15px">&nbsp;<asp:Label id="lblleaveCancellation" runat="server" Width="320px" Visible="false"></asp:Label></td>
						            </tr> 
									<tr style="height:30px;" >
									    <td>
									        <table cellspacing="0" cellpadding="0" border="0">
									            <tr>
									                <td style="width:25px" align="left"><asp:image id="imgOption_Status" runat="server" Height="21px" Width="21px"></asp:image></td>
										            <td style="width:145px" align="left"><asp:label id="lblOption_Status" runat="server" Width="140px" CssClass="wordstyle"></asp:label></td>
						                            <td style="width:235px" align="left"><asp:DropDownList id="ddlOption_Status" runat="server" Width="157px" CssClass="wordstyle" AutoPostBack="true"></asp:DropDownList></td> 
									            </tr>
									        </table> 
									    </td>
									</tr>
									<tr >
									    <td>
				                            <%--<div style="border-right-style:double; border-left-style:double; border-bottom-style:double; border-top-style:double; border-color:#F2F4FF; margin-left:5px; margin-top:5px; margin-right:5px">--%>
								                <asp:gridview id="myGridview3" Height="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
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
												                <asp:CheckBox ID="chkSelect3" BorderStyle="None" Runat="server" />
											                </ItemTemplate>
										                </asp:TemplateField>
									                </columns>
								                </asp:gridview>
								            <%--</div>--%>
								            <asp:panel id="pnlCompare" ScrollBars="Auto" runat="server" visible="false">
								                <%--<div style="border-right-style:double; border-left-style:double; border-bottom-style:double; border-top-style:double; border-color:#F2F4FF; margin-left:5px; margin-top:5px; margin-right:5px">--%>
											        <asp:gridview id="myGridview4" Width="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
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
															        <asp:CheckBox ID="chkSelect4" BorderStyle="None" Runat="server" />
														        </ItemTemplate>
													        </asp:TemplateField>
												        </columns>
											        </asp:gridview>
											     <%--</div>--%>
											 </asp:panel>
				                        </td>
									</tr>
									<tr >
									    <td>
									        <table cellspacing="0" cellpadding="0" width="100%" border="0">
								                <tr>
								                    <td style="width:5px">&nbsp;</td>
									                <td align="left">
										                <p class="wordstyle">&nbsp;Page&nbsp;
											                <asp:Label id="CurrentPage3" runat="server" CssClass="wordstyle"></asp:Label>&nbsp;of&nbsp;
											                <asp:Label id="TotalPages3" runat="server" CssClass="wordstyle"></asp:Label>
											                <asp:Label id="lbltotal3" runat="server" CssClass="wordstyle"></asp:Label></p>
									                </td>
									                <td align="center">
										                <asp:LinkButton id="FirstPage3" runat="server" CssClass="wordstyle" CommandName="First" OnCommand="NavigationLink3_Click"
											                Text="[ First ]"></asp:LinkButton>
										                <asp:LinkButton id="PrevPage3" runat="server" CssClass="wordstyle" CommandName="Prev" OnCommand="NavigationLink3_Click"
											                Text="[ Prev ]"></asp:LinkButton>
										                <asp:LinkButton id="NextPage3" runat="server" CssClass="wordstyle" CommandName="Next" OnCommand="NavigationLink3_Click"
											                Text="[ Next ]"></asp:LinkButton>
										                <asp:LinkButton id="LastPage3" runat="server" CssClass="wordstyle" CommandName="Last" OnCommand="NavigationLink3_Click"
											                Text="[ Last ]"></asp:LinkButton></td>
									                <td align="right">
										                <asp:Label id="lblGoToPage3" runat="server" Text="Go To Page" CssClass="wordstyle1"></asp:Label>
										                <asp:TextBox id="txtGoToPage3" runat="server" Width="35px" CssClass="toppos"></asp:TextBox>
										                <asp:ImageButton id="imgBtnGoToPage3" Height="21px" ImageAlign="AbsBottom" Runat="server"></asp:ImageButton></td>
										            <td style="width:5px">&nbsp;</td>
								                </tr>
								                <tr>
					                                <td style="width:110px" align="left" colspan="5">
					                                    <asp:Image ID="imgBlank06" Width="5px" Height="5px" runat="server" />
					                                    <asp:ImageButton id="imgBtnApply" runat="server" Height="21px" Width="74px" />
					                                </td>
					                            </tr>
							                </table>
									    </td>
									</tr>
								</table> 
							</asp:Panel> 
							<%--pnlresult--%>
							<asp:Panel ID="pnlresult" runat="server" Visible = "true" >
							    <table cellspacing="0" cellpadding="0" border="0">
									<tr align="left" style="height:30px">
			                            <td><asp:Image ID="Image1" ImageUrl="../../../images/company/default/png/blank.png" Width="5px" runat="server" /></td>
				                        <td align="left"><asp:Label id="lblresult" runat="server" CssClass="wordstyle2"></asp:Label></td>
			                        </tr>
								</table> 
							</asp:Panel>
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
