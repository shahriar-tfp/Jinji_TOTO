<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UploadFile.aspx.vb" Inherits="Pages_Global_UploadFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Global : Upload File Page</title>
</head>
<body>
    <form id="UploadFile" runat="server">
    <div>
        <input id="myFile" runat="server" type="file" onclick="Upload"/>
        <br />
        <asp:Label ID="lblError" runat="server"></asp:Label>
         <Input id="Submit" Type="Submit" Value="Upload" 
             OnServerClick="Upload_Click" Runat="Server">
    </div>
    </form>
</body>
</html>
