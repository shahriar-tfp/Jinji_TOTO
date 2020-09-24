<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SessionTimeOut.aspx.vb" Inherits="Pages_Global_SessionTimeOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Global : Session TimeOut PAGE</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
        <SCRIPT language="JavaScript">
		function Redirect()
		{
			top.location.href= "../../Login.aspx";
		}
		function RedirectWithDelay()
		{
			window.setTimeout("Redirect();", 3000);
		}
		</SCRIPT>
</head>
<body onload="RedirectWithDelay();">
    <form id="SessionTimeOut" runat="server">
    <div align="center">
        <table id="Table1" width="50%" cellSpacing="0" cellPadding="0" align="center" border="0">
		    <tr>
			    <td class="heading" align="center" width="100%" bgColor="#cccccc" height="24">Human 
				    Capital Resources Management
			    </td>
		    </tr>
		    <tr>
			    <td align="center" width="100%" bgColor="#ffffff" height="13">&nbsp;</td>
		    </tr>
	    </table>
		<table id="Table2" width="50%" borderColor="#000066" cellSpacing="0" cellPadding="0" align="center" border="1">
			<tr bgColor="#000066">
				<td class="white">
					<div align="center"><font color="#ffffff">Your Session Time Out!</font></div>
				</td>
			</tr>
			<tr>
				<td>
					<table id="Table3" cellSpacing="0" cellPadding="0" align="center" border="0">
					    <tr>
						    <td>&nbsp;</td>
						</tr>
						<tr>
							<td align="center">
								<asp:Label id="lblresult" runat="server" CssClass="wordstyle2">Please wait! System will post you to login page...</asp:Label> 
						    </td>
						</tr>
						<tr>
						    <td>&nbsp;</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<table id="Table4" width="50%" cellSpacing="0" cellPadding="0" border="0">
			<tr>
			    <td align="center" bgColor="#ffffff" height="13">&nbsp;</td>
		    </tr>
			<tr>
				<td align="center" bgColor="#cccccc">
					<asp:Label ID="lblCopyRight" runat="server">Copyright 2008 Softfac Technology Sdn Bhd <br /><i>All Rights Reserved</i>.
					</asp:Label>
				</td>
			</tr>
		</table>  
    </div>
    </form>
</body>
</html>
