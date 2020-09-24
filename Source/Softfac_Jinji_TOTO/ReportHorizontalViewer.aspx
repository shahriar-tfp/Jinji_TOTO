<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportHorizontalViewer.aspx.vb" Inherits="ReportHorizontalViewer" %>

<%@ Register Assembly="GrapeCity.ActiveReports.Web.v8, Version=8.0.133.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff"
    Namespace="GrapeCity.ActiveReports.Web" TagPrefix="ActiveReportsWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Viewer</title>
</head>
<body>
    <form id="reportViewer" runat="server">
    <ActiveReportsWeb:WebViewer ID="WebViewer" ViewerType="FlashViewer" runat="server" Height="700px" Width="100%">
    </ActiveReportsWeb:WebViewer>    
    </form>
</body>
</html>
