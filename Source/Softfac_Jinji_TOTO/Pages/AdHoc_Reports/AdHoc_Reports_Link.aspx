<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AdHoc_Reports_Link.aspx.vb" Inherits="Pages_Ad_Hoc_Reports_AdHoc_Reports_Link" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ad Hoc Reports Link</title>
</head>
<body>
    <form id="ADHOC_REPORTS_LINK" runat="server">
    <div>
        <asp:HyperLink ID="hyplnkREPORT1" runat="server" Text="Payroll System" NavigateUrl="~/Pages/AdHoc_Reports/HRPS/HRPS_Compiled.exe"></asp:HyperLink>
    </div>
    </form>
</body>
</html>
