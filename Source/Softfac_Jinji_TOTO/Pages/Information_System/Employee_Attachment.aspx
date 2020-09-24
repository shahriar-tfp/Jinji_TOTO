<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Employee_Attachment.aspx.vb" Inherits="Pages_Information_System_Employee_Attachment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>JINJI : Info - Employee Attachment Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <script type="text/javascript">
    function focus_txtempid()
    {
        if (document.getElementById("txtSEARCH_EMPLOYEE_CODE").value == " Employee Code ") 
            document.getElementById("txtSEARCH_EMPLOYEE_CODE").value = "";
        if (document.getElementById("txtSEARCH_EMPLOYEE_CODE").value == "")
            document.getElementById("txtSEARCH_EMPLOYEE_CODE").style.color="black";
        if (document.getElementById("txtSEARCH_EMPLOYEE_CODE").value != " Employee Code ")
        {
            document.getElementById("txtSEARCH_EMPLOYEE_CODE").value = "";
            document.getElementById("txtSEARCH_EMPLOYEE_CODE").style.color="black";
        }
    }
    function focus_txtempname()
    {
        if (document.getElementById("txtSEARCH_EMPLOYEE_NAME").value == " Employee Name ") 
            document.getElementById("txtSEARCH_EMPLOYEE_NAME").value = "";
        if (document.getElementById("txtSEARCH_EMPLOYEE_NAME").value == "")
            document.getElementById("txtSEARCH_EMPLOYEE_NAME").style.color="black";
        if (document.getElementById("txtSEARCH_EMPLOYEE_NAME").value != " Employee Name ")
        {
            document.getElementById("txtSEARCH_EMPLOYEE_NAME").value = "";
            document.getElementById("txtSEARCH_EMPLOYEE_NAME").style.color="black";
        }
    }
    function focus_txtdepartmentname()
    {
        if (document.getElementById("txtDEPARTMENT_NAME").value == " Department Name ") 
            document.getElementById("txtDEPARTMENT_NAME").value = "";
        if (document.getElementById("txtDEPARTMENT_NAME").value == "")
            document.getElementById("txtDEPARTMENT_NAME").style.color="black";
        if (document.getElementById("txtDEPARTMENT_NAME").value != " Department Name ")
        {
            document.getElementById("txtDEPARTMENT_NAME").value = "";
            document.getElementById("txtDEPARTMENT_NAME").style.color="black";
        }
    }
    function blur_txtempid()
    {
        if (document.getElementById("txtSEARCH_EMPLOYEE_CODE").value == "") 
        {
            document.getElementById("txtSEARCH_EMPLOYEE_CODE").value = " Employee Code ";
            document.getElementById("txtSEARCH_EMPLOYEE_CODE").style.color="#A2A2A2";
        } 
    }
    function blur_txtempname()
    {
        if (document.getElementById("txtSEARCH_EMPLOYEE_NAME").value == "") 
        {
            document.getElementById("txtSEARCH_EMPLOYEE_NAME").value = " Employee Name ";
            document.getElementById("txtSEARCH_EMPLOYEE_NAME").style.color="#A2A2A2";
        }
    }
    function blur_txtdepartmentname()
    {
        if (document.getElementById("txtDEPARTMENT_NAME").value == "") 
        {
            document.getElementById("txtDEPARTMENT_NAME").value = " Department Name ";
            document.getElementById("txtDEPARTMENT_NAME").style.color="#A2A2A2";
        }
    }
    </script>
</head>
<body id="body" runat="server">
    <form id="Employee_Attachment" runat="server">
    <div>
        <table id="Table7" cellspacing="0" cellpadding="0" border="0" runat="server">
            <tr>
                <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); width :5px"></td>
                <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); vertical-align:bottom"><asp:Label ID="lblTitle" runat="server"></asp:Label></td>
            </tr>
        </table>
    </div>
    <div>
        <table cellspacing="0" cellpadding="0" width="100%" border="0" >
            <tr>
                <td>
                    <asp:Image ID="imgtop" Height="30px" runat="server" />
                </td>
            </tr>
        </table>
        
        <table>
            <tr>
                <td>
                    <asp:RadioButton ID="rdobtnByEmployee" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <table id="Table1" cellspacing="0" cellpadding="0" border="0" runat="server">
			            <tr>
			            
			                <td style="width:25px"><asp:imagebutton id="imgKeySEARCH_EMPLOYEE" runat="server" Height="21px" Width="21px"></asp:imagebutton></td>
				            <td style="width:125px"><asp:label id="lblSEARCH_EMPLOYEE" runat="server" Width="150px"></asp:label></td>
				            <td style="width:155px"><asp:textbox id="txtSEARCH_EMPLOYEE_CODE" runat="server" Width="150px"></asp:textbox></td>  
				            <td style="width:5px"></td>
				            <td style="width:305px"><asp:textbox id="txtSEARCH_EMPLOYEE_NAME" runat="server" Width="300px"></asp:textbox></td> 
				            <td style="width:30px"><asp:imagebutton id="imgbtnSEARCH_EMPLOYEE" runat="server" Height="21px"></asp:imagebutton></td>
			            
			            </tr>
			        </table>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:RadioButton ID="rdobtnByDepartment" runat="server" AutoPostBack="true" Visible=false />
                </td>
                <td>
                    <table id="Table3" cellspacing="0" cellpadding="0" border="0" runat="server" visible=false >
			            <tr>
			                <td style="width:25px"><asp:imagebutton id="imgKeyDEPARTMENT" runat="server" Height="21px" Width="21px"></asp:imagebutton></td>
				            <td style="width:125px"><asp:label id="lblDEPARTMENT" runat="server" Width="150px"></asp:label></td>
				            <td style="width:155px"><asp:dropdownlist id="ddlDEPARTMENT_CODE" runat="server" Width="155px"></asp:dropdownlist></td>  
				            <td style="width:5px"></td>
				            <td style="width:305px"><asp:textbox id="txtDEPARTMENT_NAME" runat="server" Width="300px"></asp:textbox></td> 
				            <td style="width:30px"><asp:imagebutton id="imgbtnSEARCH_DEPARTMENT" runat="server" Height="21px"></asp:imagebutton></td>
			            </tr>
			        </table>
                </td>
            </tr>
        </table>
        
        <table>
            <tr>
                <td>
                    <asp:Panel id="pnlpart1" runat="server" visible="True">
					       <table id="Table2" cellspacing="0" cellpadding="0" border="0" width="100%" runat="server">
				                <tr>
				                    <td>
				                        <%--<div style="border-right-style:double; border-left-style:double; border-bottom-style:double; border-top-style:double; border-color:#F2F4FF; margin-left:5px; margin-top:5px; margin-right:5px">--%>
					                        <asp:panel id="pnlgridview1" ScrollBars="Auto" runat="server" visible="false">
						                        <asp:gridview id="myGridView1" Width="100%" runat="server" AutoGenerateColumns="false" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
							                        cellspacing="0" cellpadding="1" EmptyDataText="No data found!" BackColor="#F2F4FF">
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
								                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
									                        <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
									                        <ItemTemplate>
										                        <asp:CheckBox ID="chkDelete" BorderStyle="None" Runat="server" />
									                        </ItemTemplate>
								                        </asp:TemplateField>
								                        <asp:TemplateField HeaderText="Employee Profile Code" Visible="True">
										                    <ItemTemplate>
											                    <asp:Label ID="grvlblEmployee_Profile_ID" Text='<%# Container.DataItem(1) %>' Runat="server" />
										                    </ItemTemplate>
									                    </asp:TemplateField>
									                    <asp:TemplateField HeaderText="Code" Visible="False">
										                    <ItemTemplate>
											                    <asp:Label ID="grvlblCode" Text='<%# Container.DataItem(2) %>' Runat="server" />
										                    </ItemTemplate>
									                    </asp:TemplateField>
									                    <asp:TemplateField HeaderText="Name" Visible="False">
										                    <ItemTemplate>
											                    <asp:Label ID="grvlblName" Text='<%# Container.DataItem(3) %>' Runat="server" />
										                    </ItemTemplate>
									                    </asp:TemplateField>
									                    <asp:TemplateField HeaderText="ID" Visible="False">
										                    <ItemTemplate>
											                    <asp:Label ID="grvlblID" Text='<%# Container.DataItem(5) %>' Runat="server" />
										                    </ItemTemplate>
									                    </asp:TemplateField>
									                    <asp:TemplateField HeaderText="File" Visible="True">
										                    <ItemTemplate>
										                        <asp:HyperLink ID="grvimgPreview" runat="server" href="<%# Container.DataItem(6) %>" text="<%# Container.DataItem(5) %>"/>
										                    </ItemTemplate>
									                    </asp:TemplateField>
									                    <asp:TemplateField HeaderText="File Name" Visible="False">
										                    <ItemTemplate>
											                    <asp:label ID="grvlblFileName" text='<%# Container.DataItem(5) %>' Runat="server" />
										                    </ItemTemplate>
									                    </asp:TemplateField>
									                    <asp:TemplateField HeaderText="Server Path" Visible="False">
										                    <ItemTemplate>
											                    <asp:Label ID="grvlblServerPath" Text='<%# Container.DataItem(6) %>' Runat="server" />
										                    </ItemTemplate>
									                    </asp:TemplateField>
									                    <asp:TemplateField HeaderText="Effective Date" Visible="False">
										                    <ItemTemplate>
											                    <asp:Label ID="grvlblEffectiveDate" Text='<%# Container.DataItem(4) %>' Runat="server" />
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
											            <asp:TextBox id="txtGoToPage1" runat="server" Width="35px" CssClass="toppos"></asp:TextBox>
											            <asp:ImageButton id="imgBtnGoToPage1" Height="21px" ImageAlign="AbsBottom" Runat="server"></asp:ImageButton></td>
											        <td style="width:5px">&nbsp;</td>
									            </tr>
									            <tr>
					                                <td style="width:110px" align="left" colspan="2"><asp:Image ID="imgblank01" Width="5px" Height="5px" runat="server" /><asp:ImageButton id="imgBtnSelect" runat="server" Height="21px" Width="74px" /><asp:ImageButton id="imgBtnDelete" runat="server" Height="21px" Width="74px" /></td>
					                                <td colspan="3"><asp:Label id="lblResult" runat="server" CssClass="wordstyle2"></asp:Label></td>
					                            </tr>
								            </table>
									    </asp:panel>
								    </td>
							    </tr>
				            </table></asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlEmployee" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <asp:imagebutton id="imgKeyEMPLOYEE_PROFILE_ID" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
                                    <asp:label id="lblEMPLOYEE_PROFILE_ID" runat="server" Width="140px"></asp:label>
                                    <asp:textbox id="txtEMPLOYEE_PROFILE_ID" runat="server" Width="150px"></asp:textbox>
                                    <asp:imagebutton id="imgbtnEMPLOYEE_PROFILE_ID" runat="server" Height="21px"></asp:imagebutton>
                                    
                                    <asp:ImageButton ID="imgKeyEFFECTIVE_DATE" runat="server" Width="21px" Height="21px" />
                                    <asp:Label ID="lblEFFECTIVE_DATE" runat="server" Width="140px" ></asp:Label>
                                    <asp:TextBox ID="txtEFFECTIVE_DATE" runat="server" Width="150px" ></asp:TextBox>
                                    <asp:ImageButton ID="imgbtnEFFECTIVE_DATE" runat="server" Width="21px" Height="21px" />
                                </td>
                            </tr>
                            <tr>
                         <td>
       <!--// <table>
            <tr>
                <td>
                    <asp:Label ID="lblLeft" runat="server"></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblRight" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ListBox id="lstLeft" Width="250px" Height="170px" SelectionMode="Multiple" runat="server" ></asp:ListBox>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgbtnSelectOne" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgbtnRemoveOne" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgbtnSelectAll" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgbtnRemoveAll" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:ListBox id="lstRight" Width="250px" Height="170px" SelectionMode="Multiple" runat="server" ></asp:ListBox>
                </td>
            </tr>
        </table>//-->
                                    <asp:image ID="imgPreview" runat="server" Width="120px" Height="120px" />
                                    <asp:gridview id="gvFiles" Width="20%" runat="server" AutoGenerateColumns="false"
													                    cellspacing="0" cellpadding="1" EmptyDataText="No data found!" Visible="false">
													                    <AlternatingRowStyle BackColor="#F2F4FF" />
													                    <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
													                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
													                    <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
													                    <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                        <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                                            <Columns>
                                                <%--<asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?')"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <%--<asp:ImageField DataImageUrlField="Image" HeaderText="Image" HeaderStyle-HorizontalAlign="center">
                                                    <ControlStyle Height="120px" Width="120px"/>
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:ImageField>--%>
                                                <asp:BoundField HeaderText="File Name" HeaderStyle-HorizontalAlign="center" DataField="File Name" />
                                                <asp:BoundField HeaderText="Employee Profile ID" HeaderStyle-HorizontalAlign="center" DataField="Employee Profile ID" />
                                                <asp:BoundField HeaderText="Effective Date" HeaderStyle-HorizontalAlign="center" DataField="Effective Date" />
                                                <asp:ButtonField HeaderText="Upload" HeaderStyle-HorizontalAlign="center" ButtonType="Button" Text="Upload" CommandName="Upload" />
                                            </Columns>
                                        </asp:gridview>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <table id="tblUpload" runat="server">
                            <tr>
                                <td style="width: 349px">
                                    <asp:FileUpload ID="filUpload" runat="server" Width="344px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 349px">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 349px">
                                    <asp:Button ID="btnPreview" runat="server" Text="Preview" Height="24px" Width="66px"/>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <asp:Panel ID="pnlPreview" runat="server" ScrollBars="Vertical" Height="270px" visible=false >
                            <table cellspacing="0" cellpadding="0" border="0" >
                                <tr>
                                    <td>
                                        <asp:gridview id="myGridView" Width="20%" runat="server" AutoGenerateColumns="false"
													                    cellspacing="0" cellpadding="1" EmptyDataText="No data found!" Visible="false">
													                    <AlternatingRowStyle BackColor="#F2F4FF" />
													                    <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
													                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
													                    <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
													                    <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                        <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete?')"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:ImageField DataImageUrlField="Image" HeaderText="Image" HeaderStyle-HorizontalAlign="center">
                                                    <ControlStyle Height="120px" Width="120px"/>
                                                    <ItemStyle HorizontalAlign="center" />
                                                </asp:ImageField>
                                                <asp:BoundField HeaderText="Employee Profile ID" HeaderStyle-HorizontalAlign="center" DataField="Employee Profile ID" />
                                                <asp:BoundField HeaderText="Effective Date" HeaderStyle-HorizontalAlign="center" DataField="Effective Date" />
                                                <asp:ButtonField HeaderText="Upload" HeaderStyle-HorizontalAlign="center" ButtonType="Button" Text="Upload" CommandName="Upload" />
                                            </Columns>
                                        </asp:gridview>
                                    </td>
                                </tr>
                            </table>
                       </asp:Panel>
                       
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="imgbottom" Height="30px" Visible="false" runat="server" />
                </td>
            </tr>
       </table>
    </div>
    </form>
</body>
</html>
