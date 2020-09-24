<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WebForm1.aspx.vb" Inherits="PAGES_EAPPRAISAL_STAFFTARGET" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="counter" runat="server">
    <div>
    <input type="text" size="3" name="d2" style="color:Maroon; font-weight: bold;" value="59: 59" />
    <input type="button" name="clickMe" value="Start" onclick="display()"/>


<script type="text/javascript"> 

 var seconds=60
 var minute=59



function display()
{ 
    seconds-=1
 
    if (seconds<0)
    { 
       seconds=59 
       minute-=1 
    } 
    
     if (minute<0)
    {
       minute+=1 
       seconds=0
    }
  
    document.counter.d2.value= minute + ": " + seconds

    
    setTimeout("display()",998)
} 
display() 

</script> </div>
    </form>
</body>
</html>
