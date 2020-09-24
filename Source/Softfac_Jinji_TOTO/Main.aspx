<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Main.aspx.vb" Inherits="Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>JINJI : Main Page </title>
<meta name="Generator" content="EditPlus" />
<meta name="Author" content="" />
<meta name="Keywords" content="" />
<meta name="Description" content="" />
<link id="pageCss11" runat="server" rel="stylesheet" type="text/css" />

<style type="text/css">
body, img, div, table, td {
	background: url(images/theme1/iepngfix.htc);
}
html, body {
    background-color: transparent;
}
</style>

</head>
<body id="body" runat="server">
<form id="Main" runat="server">
<div align="center" style="height:150px">
    <table border="0" cellpadding="0" cellspacing="0" width="95%" class="main_img">
        <tr>
	        <td id="tdLayout01" runat="server" colspan="3" align="left" valign="top" width="100%" >
	            <img id="imgLayout01" runat="server" src="" border="0" vspace="0" hspace="0" alt="" />
		    </td>
	    </tr>
	    <tr>
	        <td colspan="3">
	            <img id="imgLayout06" runat="server" src="" border="0" vspace="0" hspace="0" alt="" />
	        </td>
	    </tr>
	    <tr>
		    <td align="center" valign="top" width="60%">
		        <table border="0" cellpadding="0" cellspacing="0" width="100%">
			        <tr>
				        <td colspan="3" align="left" valign="middle" width="100%">
				            <table border="0" cellpadding="0" cellspacing="0" width="100%">
					            <tr>
						            <td align="left" valign="middle" width="5">
						                <img id="imgLayout02" runat="server" src="" width="5" height="22" border="0" alt="" /><br />
						            </td>
						            <td align="left" valign="middle" class="main_header_bar" width="100%">Employee Attendance Info</td>
						            <td align="right" valign="middle" width="5">
						                <img id="imgLayout03" runat="server" src="" width="5" height="22" border="0" alt="" /><br />
						            </td>
					            </tr>
					            <tr>
					                <td colspan="3" align="left" valign="middle" class="alert">
	                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
	                                        <tr>
		                                        <td>
		                                            <div style="margin: 5px 0px 5px 0px">
		                                                <asp:Label ID="lblTotalEmployee" runat="server" CssClass="wordstyle4" ></asp:Label>
		                                            </div> 
		                                        </td>
	                                        </tr> 
	                                        <tr>
	                                            <td>
	                                                <div style="margin: 5px 0px 5px 0px"> 
	                                                <asp:gridview id="gv1" Width="100%" runat="server" AutoGenerateColumns="true"
		                                                cellspacing="0" cellpadding="1" EmptyDataText="No data found!">
		                                                <AlternatingRowStyle BackColor="#F2F4FF" />
		                                                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
		                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
		                                                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
		                                                <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                                                        <Columns>
                                                        </Columns>
                                                    </asp:gridview>
	                                                </div>
	                                            </td>
	                                        </tr>
	                                        <tr>
	                                            <td>
	                                                <div style="margin: 5px 0px 5px 0px">
	                                                <asp:gridview id="gv2" Width="100%" runat="server" AutoGenerateColumns="true"
		                                                cellspacing="0" cellpadding="1" EmptyDataText="No data found!">
		                                                <AlternatingRowStyle BackColor="#F2F4FF" />
		                                                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
		                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
		                                                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
		                                                <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                                                        <Columns>
                                                        </Columns>
                                                    </asp:gridview>
                                                    </div> 
	                                            </td>
	                                        </tr>
	                                        <tr>
	                                            <td>
	                                                <div style="margin: 5px 0px 5px 0px">
	                                                <asp:gridview id="gv3" Width="100%" runat="server" AutoGenerateColumns="true"
		                                                cellspacing="0" cellpadding="1" EmptyDataText="No data found!">
		                                                <AlternatingRowStyle BackColor="#F2F4FF" />
		                                                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
		                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
		                                                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
		                                                <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                                                        <Columns>
                                                        </Columns>
                                                    </asp:gridview>
                                                    </div> 
	                                            </td>
	                                        </tr>
	                                        <tr>
	                                            <td>
	                                                <div style="margin: 5px 0px 5px 0px">
	                                                <asp:gridview id="gv4" Width="100%" runat="server" AutoGenerateColumns="true"
		                                                cellspacing="0" cellpadding="1" EmptyDataText="No data found!">
		                                                <AlternatingRowStyle BackColor="#F2F4FF" />
		                                                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
		                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
		                                                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
		                                                <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                                                        <Columns>
                                                        </Columns>
                                                    </asp:gridview>
                                                    </div> 
	                                            </td>
	                                        </tr>
	                                        <tr>
	                                            <td>
	                                                <div style="margin: 5px 0px 5px 0px">
	                                                <asp:gridview id="gv5" Width="100%" runat="server" AutoGenerateColumns="true"
		                                                cellspacing="0" cellpadding="1" EmptyDataText="No data found!">
		                                                <AlternatingRowStyle BackColor="#F2F4FF" />
		                                                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
		                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
		                                                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
		                                                <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                                                        <Columns>
                                                        </Columns>
                                                    </asp:gridview>
                                                    </div> 
	                                            </td>
	                                        </tr>
	                                    </table>  
					                </td>
					            </tr>
				            </table>
				        </td>
			        </tr>
		        </table>
		    </td>
		    <td align="center" valign="top" width="2%"></td>
		    <td align="center" valign="top" width="38%">
		        <table border="0" cellpadding="0" cellspacing="0" width="100%">
			        <tr>
				        <td align="left" valign="middle" width="5">
				            <img id="imgLayout04" runat="server" width="5" height="22" border="0" alt=""><br />
				        </td>
				        <td align="left" valign="middle" class="main_header_bar" width="100%">Company Announcement</td>
				        <td align="right" valign="middle" width="5">
				            <img id="imgLayout05" runat="server" width="5" height="22" border="0" alt=""><br />
				        </td>
			        </tr>
			        <tr>
				        <td colspan="3" align="left" valign="middle" class="alert">
				        <!--Content-->
				        <iframe allowtransparency="true" src="News.aspx" name="News" width="99%" height="242px" scrolling="auto" frameborder="0">
				        [ Your user agent does not support frames or is currently configured not to display frames. However, you may visit <a href="#" target="New" class="text2">the related document.</a> ]
				        </iframe>
				        <!--End Content-->
				        </td>
			        </tr>
		        </table>
		    </td>
	    </tr>
    </table>
   </div>
    </form>
</body>
</html>
