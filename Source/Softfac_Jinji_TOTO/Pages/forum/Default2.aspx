<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="Forum.Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script language="javascript" type="text/javascript">
    	function temp()
    	{
			var elem = document.getElementById("label1");
			var pos = getElementPosition(elem);

			tempdiv.style.left = pos.x;
			tempdiv.style.top = pos.y;
			tempdiv.style.width = '100px';
			tempdiv.style.height = '100px';
    	}
    	function getElementPosition(theElement)
    	{  
    		var posX = 0;  
    		var posY = 0;                
    		while(theElement != null)
    		{    
    			posX += theElement.offsetLeft;    
    			posY += theElement.offsetTop;    
    			theElement = theElement.offsetParent;  
    		}                        		         
    		return {x:posX,y: posY};
    	}
    	function CaptureScreen()
    	{
		    alert("start")
		    a.Path = "C:\\ScreenCapture.jpg"
		    var bsuccess = a.Capture(true)
		    if (bsuccess)
			    alert("Save OK")
		    else
			    alert("Save Failed")    	
    	}
    </script>
</head>
<body onload="temp()">
    <form id="form1" runat="server">
    <div>

    
        <pre>
            Here is Chart.
            Here is Pivot table.
        </pre>
            <label id="label1">User Name:</label><asp:textbox id="txtuid" runat="server" Width="100px">PC011</asp:textbox>
            <asp:textbox id="Textbox1" runat="server" Width="100px">Bertha Rasang</asp:textbox>
            <asp:textbox id="Textbox2" runat="server" Width="100px">PSB</asp:textbox>
        <asp:Button id="Button2" runat="server" Width="90px" Height="24px" Text="Login" CausesValidation="False"  onclick="Button2_Click" ></asp:Button>
		<div id="tempdiv" runat="server" style="position:absolute;">
			What's this ya?
		</div>
    </div>
    </form>
</body>
</html>
