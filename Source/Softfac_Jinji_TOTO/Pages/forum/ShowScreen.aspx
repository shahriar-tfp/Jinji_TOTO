<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ShowScreen.aspx.vb" Inherits="ShowScreen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet"/>	
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="ScreenUpload" runat="server" Width="300px" />
        <asp:Button ID="button1" runat="server" Text="Upload & View" Width="90px" CssClass="button_style" OnClick="UploadAndShow"  />
        <input type="button" id="closebutton" value="Close" class="button_style" onclick="javascript:window.close()" />
        <br />
        <br />
        <asp:Image ID="image1" runat="server" AlternateText="Capture Screen File" BorderWidth="2" BorderColor="AliceBlue" />
        <div id="imagediv" runat="server" ></div>
    </div>
    </form>
</body>
</html>
