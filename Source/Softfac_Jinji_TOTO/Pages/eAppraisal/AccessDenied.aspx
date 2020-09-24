<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AccessDenied.aspx.vb" Inherits="PAGES_EAPPRAISAL_ACCESSDENIED" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>eAppraisal - Access Denied</title>
    <link href="images/favicon.ico" rel="shortcut icon" />
</head>
<body>
<center>
    <form id="form1" runat="server">
    <div>
     <table width="100%">
     <tr>
<td colspan="3" align="right"><br />
<asp:Label ID="lblUser" runat="server" /> |<!--// 
<asp:LinkButton ID="lnkMain" Text="Main" runat="server"/> | 
<asp:LinkButton ID="lnkLogOut" Text="Logout" runat="server"/>//-->&nbsp;
</td>
</tr>
<tr>
    <td valign="top" colspan="3" style="height:1px"><hr id="Hr1" runat="server" /></td>
    </tr>
    <tr>
    <td style="height:100%; width:100%">
      <h1>You have no granted permission to the system. Thank you.
      </h1>
         </td>
         </tr>
       </table>
    </div>
    </form>
    </center>
</body>
</html>
