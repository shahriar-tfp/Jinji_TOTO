﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportViewer.aspx.vb" Inherits="ReportViewer" %>

<%@ Register Assembly="GrapeCity.ActiveReports.Web.v8, Version=8.0.133.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff"
    Namespace="GrapeCity.ActiveReports.Web" TagPrefix="ActiveReportsWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>


<body> 

    <form id="form1" runat="server">
    <div>
        <ActiveReportsWeb:WebViewer ID="WebViewer1" runat="server" ViewerType="FlashViewer" Height="700px" Width="100%">
        </ActiveReportsWeb:WebViewer>
        
    </div>
    </form>
</body>
</html>
