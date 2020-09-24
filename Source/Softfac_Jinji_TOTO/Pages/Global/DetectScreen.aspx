<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DetectScreen.aspx.vb" Inherits="detectscreen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Global : Detect Screen Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <script language="javascript"> 
        res = "&res="+screen.width+"x"+screen.height+"&d="+screen.colorDepth 
        top.location.href="detectscreen.aspx?action=set"+res 
    </script>
    </div>
    </form>
</body>
</html>
