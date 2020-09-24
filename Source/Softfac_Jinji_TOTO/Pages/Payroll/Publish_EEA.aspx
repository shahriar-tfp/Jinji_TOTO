<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Publish_EEA.aspx.vb" Inherits="Pages_Payroll_Publish_EEA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>JINJI : Payroll - Publish E-EA Form Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server">
    <form id="Payroll_Process_Vw" runat="server">
    <div>
    <asp:Panel ID="pnlPayroll" runat="server">
    <table id="Table6" cellpadding="0" cellspacing="0" width="100%" border="0" runat="server">
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
    <asp:Panel ID="pnlEdit" runat="server">
        <asp:Image ID="imgTop" runat="server" Height="30px" />
        <table>
            <tr>
                <td>
                    <asp:ImageButton ID="imgKeyYEAR_MONTH" runat="server" Width="21px" Height="21px"/>
                    <asp:Label ID="lblYEAR_MONTH" runat="server" Width="120px"></asp:Label>
                    <asp:Dropdownlist ID="ddlYEAR" runat="server" Width="70px" AutoPostBack="true"></asp:Dropdownlist>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="imgKeyFILTER_BY" runat="server" Width="21px" Height="21px"/>
                    <asp:Label ID="lblFILTER_BY" runat="server" Width="120px"></asp:Label>
                    <asp:DropDownList ID="ddlFILTER_BY" runat="server" Width="157px" AutoPostBack="true" ></asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:imagebutton id="imgKeyEMPLOYEE_PROFILE_ID" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
                    <asp:label id="lblEMPLOYEE_PROFILE_ID" runat="server" Width="120px"></asp:label>
                    <asp:textbox id="txtEMPLOYEE_PROFILE_ID" runat="server" Width="150px"></asp:textbox>
                    <asp:imagebutton id="imgbtnEMPLOYEE_PROFILE_ID" runat="server" Height="21px" ></asp:imagebutton>
                </td>
                <td>
                    <asp:imagebutton id="imgKeyOPTION_PAY_TYPE" runat="server" Width="21px" Height="21px"></asp:imagebutton>
                    <asp:label id="lblOPTION_PAY_TYPE" runat="server" Width="120px"></asp:label>
                    <asp:dropdownlist id="ddlOPTION_PAY_TYPE" runat="server" Width="157px"></asp:dropdownlist>
                    <asp:imagebutton id="imgbtnOPTION_PAY_TYPE" runat="server" Height="21px"></asp:imagebutton>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:imagebutton id="imgKeyOCP_ID_DIVISION" runat="server" Width="21px" Height="21px"></asp:imagebutton>
                    <asp:label id="lblOCP_ID_DIVISON" runat="server" Width="120px"></asp:label>
                    <asp:textbox id="txtOCP_ID_DIVISION" runat="server" Width="150px"></asp:textbox>
                    <asp:imagebutton id="imgbtnOCP_ID_DIVISION" runat="server" Height="21px"></asp:imagebutton>
                </td>
                <td>
                    <asp:imagebutton id="imgKeyOCP_ID_DEPARTMENT" runat="server" Width="21px" Height="21px"></asp:imagebutton>
                    <asp:label id="lblOCP_ID_DEPARTMENT" runat="server" Width="120px"></asp:label>
                    <asp:textbox id="txtOCP_ID_DEPARTMENT" runat="server" Width="150px"></asp:textbox>
                    <asp:imagebutton id="imgbtnOCP_ID_DEPARTMENT" runat="server" Height="21px"></asp:imagebutton>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:imagebutton id="imgKeyOCP_ID_SECTION" runat="server" Width="21px" Height="21px"></asp:imagebutton>
                    <asp:label id="lblOCP_ID_SECTION" runat="server" Width="120px"></asp:label>
                    <asp:textbox id="txtOCP_ID_SECTION" runat="server" Width="150px"></asp:textbox>
                    <asp:imagebutton id="imgbtnOCP_ID_SECTION" runat="server" Height="21px"></asp:imagebutton>
                </td>
                <td>
                    <asp:imagebutton id="imgKeyOCP_ID_TMS" runat="server" Width="21px" Height="21px"></asp:imagebutton>
                    <asp:label id="lblOCP_ID_TMS" runat="server" Width="120px"></asp:label>
                    <asp:textbox id="txtOCP_ID_TMS" runat="server" Width="150px"></asp:textbox>
                    <asp:imagebutton id="imgbtnOCP_ID_TMS" runat="server" Height="21px"></asp:imagebutton>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:imagebutton id="imgKeyOCP_ID_JOB_GRADE" runat="server" Width="21px" Height="21px"></asp:imagebutton>
                    <asp:label id="lblOCP_ID_JOB_GRADE" runat="server" Width="120px"></asp:label>
                    <asp:textbox id="txtOCP_ID_JOB_GRADE" runat="server" Width="150px"></asp:textbox>
                    <asp:imagebutton id="imgbtnOCP_ID_JOB_GRADE" runat="server" Height="21px"></asp:imagebutton>
                </td>
                <td>
                    <asp:imagebutton id="imgKeyOCP_ID_JOB_TITLE" runat="server" Width="21px" Height="21px"></asp:imagebutton>
                    <asp:label id="lblOCP_ID_JOB_TITLE" runat="server" Width="120px"></asp:label>
                    <asp:textbox id="txtOCP_ID_JOB_TITLE" runat="server" Width="150px"></asp:textbox>
                    <asp:imagebutton id="imgbtnOCP_ID_JOB_TITLE" runat="server" Height="21px"></asp:imagebutton>
                </td>
            </tr>
        </table>
        <table id="tblEditButton" runat="server">
            <tr align="left">
                <td>
                    <asp:ImageButton ID="imgbtnUpdate" runat="server" Width="74" Height="21" OnClientClick="return confirm('Confirm Publish To E-EA Form and E-PCB II ?')"></asp:ImageButton>
                    <asp:ImageButton ID="imgbtnUpdate2" runat="server" Width="74" Height="21" OnClientClick="return confirm('Confirm UNPublish?')"></asp:ImageButton>
                    <asp:ImageButton ID="imgbtnFILTER" runat="server" Width="74" Height="21"/>
                    <asp:imagebutton id="imgbtnPROCESS" runat="server" Width="74" Height="21" OnClientClick="return confirm('Confirm Process ?')"></asp:imagebutton>
                </td>
            </tr>
        </table>
        <center>
        <table id="tblLISTBOX" runat="server" visible="false">
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
        </table>
        </center>
        <table id="tblButton" runat="server" visible="false">
            <tr>
                <td>
                    <asp:imagebutton id="imgBtnSearch" runat="server" Width="74" Height="21" ></asp:imagebutton>
                    <asp:imagebutton id="imgBtnClear" runat="server" Width="74" Height="21" ></asp:imagebutton>
                    <asp:imagebutton id="imgBtnCancel" runat="server" Width="74" Height="21" ></asp:imagebutton>
                </td>
            </tr>
        </table>
        <asp:Image ID="imgBottom" runat="server" Visible="false" />
    </asp:Panel>
    <div>
        <asp:panel id="pnlGridview" ScrollBars="Auto" runat="server" visible="true">
	        <%--<div style="border-top-style: double; border-right-style: double; border-left-style: double; border-bottom-style: double;border-color :#F2F4FF;">--%>
	            <asp:GridView id="myGridView" Width="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
	                cellspacing="0" cellpadding="1" EmptyDataText="No data found!">
	                <AlternatingRowStyle BackColor="#F2F4FF" />
	                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
	                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
	                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
	                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                    <Columns>
                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="35" >
                            <ItemTemplate>
                                <center><asp:CheckBox ID="chkSelect" runat="server" /></center>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                
                <asp:GridView id="gvHistory" Width="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
	                cellspacing="0" cellpadding="1" EmptyDataText="No data found!" Visible="false">
	                <AlternatingRowStyle BackColor="#F2F4FF" />
	                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
	                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
	                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
	                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                    <Columns>
                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="35" >
                            <ItemTemplate>
                                <center><asp:CheckBox ID="chkSelect" runat="server" /></center>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            <%--</div>--%>
        </asp:panel>
        <table id="tblCheck" runat="server">
            <tr>
                <td>
                    <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" AutoPostBack="true"/>
                    <asp:CheckBox ID="chkClearAll" runat="server" Text="Clear All" AutoPostBack="true"/>
                </td>
            </tr>
        </table>
        <asp:panel id="pnlPrevNext" runat="server" visible="false">
			<table cellspacing="0" cellpadding="0" width="100%" border="0">
		        <tr>
			        <td align="left">
				        <p class="wordstyle">&nbsp;Page&nbsp;
					        <asp:Label id="CurrentPage" runat="server" CssClass="wordstyle"></asp:Label>&nbsp;of&nbsp;
					        <asp:Label id="TotalPages" runat="server" CssClass="wordstyle"></asp:Label>
					        <asp:Label id="lblTotal" runat="server" CssClass="wordstyle"></asp:Label></p>
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
				    <td>&nbsp;</td>
		        </tr>
	        </table>
	    </asp:panel>
    </div>
    <!--// Button //-->  
    <br />
    <asp:Panel ID="pnlButtonPayroll" runat="server" Visible="True">
    
    <asp:label id="lblresultPayroll2" runat="server" Height="22" Visible="False"></asp:label>
    </asp:Panel>
    <center>
    <asp:Panel ID="pnlProgress" runat="server" Visible="false">
    <div >
        <table id="Table5" width="50%" cellspacing="0" cellpadding="0" border="0">
		    <tr>
			    <td align="center" valign="middle" style="background-color:#cccccc; height:33px; width:100%">
				    <asp:label id="lblSysName" Height="18px" CssClass="wordstyle3" runat="server">Process done.</asp:label>
				</td>
		    </tr>
		    <tr>
			    <td align="center" style="background-color:#ffffff; height:13px; width:100%">&nbsp;</td>
		    </tr>
	    </table>
		<table id="Table10" width="50%" style="border-color:#000066" cellspacing="0" cellpadding="0" border="1">
			<tr>
				<td>
					<table id="Table12" cellspacing="0" cellpadding="0" border="0" style="width:100%">
                        <tr>
                            <td>
                                <asp:GridView id="gvMessage" Width="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
	                cellspacing="0" cellpadding="1" EmptyDataText="No data found!">
	                <AlternatingRowStyle BackColor="#F2F4FF" />
	                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
	                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
	                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i"  />
	                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="lightgray" Height="21" CssClass="dgstyle_h" ForeColor="black" />
                    <Columns>
                    </Columns>
                    </asp:GridView>
                            </td>
                        </tr>
					</table>
				</td>
			</tr>
		</table>
		<table id="Table13" width="50%" cellpadding="0" cellspacing="0" border="0" visible="false">
			<tr>
			    <td align="center" style="background-color:#ffffff; height:13px">&nbsp;</td>
		    </tr>
			<tr>
				<td align="center" valign="middle" style="background-color:#cccccc; height:33px; width:100%">
					<asp:Label ID="lblCopyRight" CssClass="wordstyle9" runat="server">Copyright 2008 Softfac Technology Sdn Bhd<br /><i>All Rights Reserved</i>
					</asp:Label>
				</td>
			</tr>
		</table>  
    </div>
    </asp:Panel>
    </center>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
