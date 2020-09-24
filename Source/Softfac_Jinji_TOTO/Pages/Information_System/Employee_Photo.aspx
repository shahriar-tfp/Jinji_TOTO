<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Employee_Photo.aspx.vb" Inherits="Pages_Information_System_Employee_Photo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>JINJI : Info - Employee Photo Page</title>
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
    </script>
</head>
<body id="body" runat="server">
    <form id="Employee_Photo" runat="server">
    <div>
        <table id="Table7" cellspacing="0" cellpadding="0" border="0" width="100%" runat="server">
            <tr>
                <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); width :5px"></td>
                <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); vertical-align:bottom"></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height:15px"></td>
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
            <tr>
                <td style="height:15px"></td>
            </tr>
            <tr>
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
            <tr>
                <td>
                    <asp:Panel id="pnlpart1" runat="server" visible="True">
					       <table id="Table2" cellspacing="0" cellpadding="0" border="0" width="100%" runat="server">
				                <tr>
				                    <td>
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
									                    <asp:TemplateField HeaderText="Photo" Visible="True">
										                    <ItemTemplate>
										                        <asp:Image ID="grvimgPreview" runat="server" ImageAlign="Middle" ImageUrl='<%# Container.DataItem(6) %>' />
										                    </ItemTemplate>
									                    </asp:TemplateField>
									                    <asp:TemplateField HeaderText="Image Name" Visible="False">
										                    <ItemTemplate>
											                    <asp:Label ID="grvlblImageName" Text='<%# Container.DataItem(5) %>' Runat="server" />
										                    </ItemTemplate>
									                    </asp:TemplateField>
									                    <asp:TemplateField HeaderText="Image Path" Visible="False">
										                    <ItemTemplate>
											                    <asp:Label ID="grvlblImagePath" Text='<%# Container.DataItem(6) %>' Runat="server" />
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
					                                <td style="width:110px" align="left" colspan="2"><asp:Image ID="imgblank01" Width="5px" Height="5px" runat="server" /><asp:ImageButton id="imgBtnSelect" runat="server" Height="21px" Width="74px" /></td>
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
                                    <asp:Image ID="imgPreview" runat="server" Width="120px" Height="120px" />
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
                        <asp:Panel ID="pnlPreview" runat="server" ScrollBars="Vertical">
                            <table cellspacing="0" cellpadding="0" border="0" >
                                <tr>
                                    <td>
                                        <asp:gridview id="myGridView" Width="25%" runat="server" AutoGenerateColumns="false"
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
                                                <%--<asp:TemplateField HeaderText="Upload" HeaderStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" CommandName="Upload" OnClientClick="return confirm('Are you sure you want to upload?')"></asp:Button>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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
