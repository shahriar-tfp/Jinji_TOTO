<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Logout.aspx.vb" Inherits="Pages_Global_Logout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>JINJI: LOGOUT PAGE</title>
   <!-- <link href="App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />-->
    <link id="pageCss21" runat="server" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="Logout" runat="server" target="_parent">
    <div align="center">
        <table id="Table1" width="50%" cellspacing="0" cellpadding="0" align="center" border="0">
		    <tr>
			    <td align="center" valign="middle" style="background-color:#cccccc; height:33px; width:100%">
				    <asp:label id="lblSysName" Height="18px" CssClass="wordstyle3" runat="server">Human Capital Resources Management</asp:label>
				</td>
		    </tr>
		    <tr>
			    <td align="center" style="background-color:#ffffff; height:13px; width:100%">&nbsp;</td>
		    </tr>
	    </table>
		<table id="Table2" width="50%" style="border-color:#000066" cellspacing="0" cellpadding="0" align="center" border="1">
			<tr style="background-color:#000066">
				<td align="center" valign="top" style="height:30px">
				    <asp:label id="lblTitle" Height="18px" CssClass="wordstyle8" runat="server">Are you sure you want to log off ?</asp:label>
				</td>
			</tr>
			<tr>
				<td>
					<table id="Table3" cellspacing="0" cellpadding="0" align="center" border="0">
					    <tr>
						    <td>&nbsp;</td>
						    <td>&nbsp;</td>
						    <td>&nbsp;</td>
						</tr>
						<tr>
							<td align="right">
								<asp:button id="imgBtnYes" tabIndex="4" runat="server" Width="64px" Height="24px" Text="Yes"></asp:button>
						    </td>
						    <td>&nbsp;</td>
						    <td align="left">
								<asp:button id="imgBtnNo" tabIndex="4" runat="server" Width="64px" Height="24px" Text="No"></asp:button>
						    </td>
						</tr>
						<tr>
						    <td>&nbsp;</td>
						    <td>&nbsp;</td>
						    <td>&nbsp;</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<table id="Table4" width="50%" cellpadding="0" cellspacing="0" border="0">
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
    </form>
</body>
</html>
