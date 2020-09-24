<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StaffSkills.aspx.vb" Inherits="PAGES_EAPPRAISAL_STAFFSKILLS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>eAppraisal - Skills</title>
    <link href="images/favicon.ico" rel="shortcut icon" />
    
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
  
    document.form1.d2.value= minute + " Minutes " + seconds + " Seconds" 

    
    setTimeout("display()",998)
} 

</script>
</head>
<body bgcolor="beige">
<center>
    <form id="form1" runat="server">
    <div>
      <table>
       <tr>
       <td align="left">
       <asp:Label ID="lblTarget" runat="server" Text="Appraisal: "/>
       <asp:LinkButton ID="lnkbtnTarget" Text="Target & Achievement" runat="server"/>
       <asp:Label ID="lblPageSep1" Text="|" runat="server"/>
       <asp:LinkButton ID="lnkbtnSkills" Text="Skills"  ForeColor="Red" runat="server"/>
       <asp:Label ID="lblPageSep4" Text="|" runat="server"/>
          <asp:LinkButton ID="lnkbtnComment" Text="Comments"   runat="server"/>
       <asp:Label ID="lblPageSep2" Text="|" runat="server"/>
       <asp:LinkButton ID="lnkReport" Text="Report"   runat="server"/>
       <asp:Label ID="lblPageSep3" Text="|" runat="server"/>
       <asp:LinkButton ID="lnkManual" Text="Help" runat="server" />
       <%--<a href="Appraisee Manual.pdf">Help</a>--%>
       </td>
       <td align="right">
       <asp:Label ID="lblUser" runat="server"/> | <!--//<asp:LinkButton ID="lnkMain" Text="Main" runat="server" /><asp:LinkButton ID="lnkChangePwd" Text="Change Password" runat="server" Visible="False" /> | <asp:LinkButton ID="lnkLogout" runat="server" Text="Logout" />//-->
       </td>
       </tr>
       <tr>
       <td valign="top" colspan="2" style="width:1160px; height:1px"><hr id="Hr1" runat="server" /></td>
       </tr>
    </table>
    
    <table>
          <tr>
          <%--<td style="width:1000px"></td>--%>
          <td align="center" style="width:200px"><asp:Label ID="lblAppraiseeSession" Width="200px" BorderColor="Black" BorderStyle="Solid" Font-Bold="True"  BorderWidth="2px" runat="server" ForeColor="White" BackColor="SteelBlue" /></td>
          <td align="center" style="width:790px"><input value="" type="text" size="25" name="d2" style="color:white; font-weight: bold; border-left-color: black; border-bottom-color: black; border-top-style: solid; border-top-color: black; border-right-style: solid; border-left-style: solid; background-color: red; border-right-color: black; border-bottom-style: solid; text-align: center; font-size: 105%;" /></td>
          <td align="center"><asp:Label ID="lblAppraiserLevel" runat="server" BackColor="SteelBlue" BorderColor="Black" BorderStyle="Ridge" Font-Bold="True" ForeColor="White" Width="150px" BorderWidth="2px" /></td>
          </tr>
          <tr>
          <td>&nbsp;</td>
          </tr>
       </table>
    
         <table width="1160px">
         <tr>
         <td>&nbsp;</td>
         
         <td align="right" rowspan="5">
         <table border="1" cellspacing="0" cellpadding="3">
         <tr>
         <td align="center" style="border-right: silver thin solid; border-top: silver thin solid; font-weight: bold; border-left: silver thin solid; color: white; border-bottom: silver thin solid; background-color: darkslategray">Rating Points</td>
         </tr>
         <tr>
         <td align="left" style="border-right: silver thin solid; border-top: silver thin solid; font-weight: bold; border-left: silver thin solid; color: white; border-bottom: silver thin solid; background-color: #336666">
         <asp:Label ID="lblRating1_Desc1" Width="150px" runat="server" /><asp:Label ID="lblRating1_Desc2" Width="450px" runat="server" />
         </td>
         </tr>
         <tr>
         <td align="left" style="border-right: silver thin solid; border-top: silver thin solid; font-weight: bold; border-left: silver thin solid; color: white; border-bottom: silver thin solid; background-color: #336666">
         <asp:Label ID="lblRating2_Desc1" Width="150px" runat="server" /><asp:Label ID="lblRating2_Desc2" Width="450px" runat="server" />
         </td>
         </tr>
         <tr>
         <td align="left" style="border-right: silver thin solid; border-top: silver thin solid; font-weight: bold; border-left: silver thin solid; color: white; border-bottom: silver thin solid; background-color: #336666">
         <asp:Label ID="lblRating3_Desc1" Width="150px" runat="server" /><asp:Label ID="lblRating3_Desc2" Width="450px" runat="server" />
         </td>
         </tr>
         <tr>
         <td align="left" style="border-right: silver thin solid; border-top: silver thin solid; font-weight: bold; border-left: silver thin solid; color: white; border-bottom: silver thin solid; background-color: #336666">
         <asp:Label ID="lblRating4_Desc1" Width="150px" runat="server" /><asp:Label ID="lblRating4_Desc2" Width="450px" runat="server" />
         </td>
         </tr>
         <tr>
         <td align="left" style="border-right: silver thin solid; border-top: silver thin solid; font-weight: bold; border-left: silver thin solid; color: white; border-bottom: silver thin solid; background-color: #336666">
         <asp:Label ID="lblRating5_Desc1" Width="150px" runat="server" /><asp:Label ID="lblRating5_Desc2" Width="450px" runat="server" />
         </td>
         </tr>
         </table>
         </td>
         
         </tr>
         
         <tr>
         <td align="left"><asp:RadioButton ID="optPersonal" Text="Personal" GroupName="Type"  runat="server" AutoPostBack="True" Checked="True" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
         </tr>
         <tr>
         <td align="left" valign="bottom">
         <asp:RadioButton ID="optSubordinate" Text="Subordinate" GroupName="Type"  runat="server" AutoPostBack="True" />&nbsp;&nbsp;
         <asp:DropDownList ID="cboSubordinate" runat="server" AutoPostBack="True" Width="400px" Enabled="False"></asp:DropDownList>
         
         </td>
         </tr>
         <tr>
         <td valign="top">
         <asp:Label ID="lblSpace1" runat="server" Width="300px" />
         <asp:Button ID="btnLock" Text="Lock Appraisee's Session" Width="186px" runat="server" BackColor="Gold" BorderStyle="Solid" Font-Bold="True" ForeColor="Black" /><br />
         <asp:Label ID="lblSpace2" runat="server" Width="300px" />
         <asp:Label ID="lblLock" Width="182px" BorderColor="Black" BorderStyle="Solid" Font-Bold="True"  BorderWidth="2px" runat="server" ForeColor="White" BackColor="SteelBlue" />
         </td>
         </tr>
         
         <tr>
         <td align="left">
         <asp:Label ID="lblAppraisal" Text="Appraisal Period" runat="server"></asp:Label>
         <asp:DropDownList ID="cboAppraisal" runat="server" AutoPostBack="True" Width="220px"></asp:DropDownList>
         </td>
         </tr>
         </table><br />
         
         <table width="1090px">
         <tr>
         <td colspan="4" align="center">
         <asp:Label ID="lblError" runat="server" ForeColor="Black" BorderColor="Black" BorderStyle="Double" Font-Bold="True" Visible="False" Width="1150px"/>
         </td>
         </tr>
         </table>
         
         <table id="Skilltable" border="1" cellspacing="0" cellpadding="3">
         <tr align="center">
         <td rowspan="2" style="font-weight: bold; color: white; background-color: darkcyan">No.</td>
         <td rowspan="2" style="font-weight: bold; color: white; background-color: darkcyan">Items</td>
         <td rowspan="2" style="font-weight: bold; color: white; background-color: darkcyan">Description</td>
         <td colspan="3" style="font-weight: bold; color: white; background-color: darkcyan">Points Given By<br />
         <asp:Label ID="lblPoint" Text="(Points from 1 ~ 3)" runat="server"></asp:Label>
         
         </td>
         </tr>
         <tr>
         <td style="font-weight: bold; color: white; background-color: darkcyan">Appraisee</td>
         <td style="font-weight: bold; color: white; background-color: darkcyan">1st Appraiser</td>
        <%-- <td style="font-weight: bold; color: white; background-color: darkcyan">2nd Appraiser</td>--%>
         </tr>
         
         <tr>
         <td rowspan="5" style="background-color: honeydew">1</td>
         <td rowspan="5" align="left" style="background-color: honeydew"><asp:Label ID="lblCategory1" Width="200px" runat="server" /></td>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory1_1" text= "" runat="server" Width="610px" /></td>
         <td rowspan="5" style="background-color: honeydew">
         <asp:DropDownList ID="cboAppraisee_1" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Font-Underline="False" >
         </asp:DropDownList></td>
         <td rowspan="5" style="background-color: honeydew; width: 99px;">
         <asp:DropDownList ID="cbo1stAppraiser_1" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Font-Underline="False" Enabled="False" >
            <%--<asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
         <%--<td rowspan="5" style="background-color: honeydew"><asp:DropDownList ID="cbo2ndAppraiser_1" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Font-Underline="False" ForeColor="Transparent" Enabled="False" >
           <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>
         </asp:DropDownList></td>--%>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory1_2" text= "" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory1_3" text= "" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory1_4" text= "" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory1_5" text= "" runat="server" Width="610px" /></td>
         </tr>
         
         <tr>
         <td rowspan="5" style="background-color: lavender">2</td>
         <td rowspan="5" align="left" style="background-color: lavender"><asp:Label ID="lblCategory2" Width="200px" runat="server" /></td>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory2_1" runat="server" Width="610px" /></td>
         <td rowspan="5" style="background-color: lavender">
         <asp:DropDownList ID="cboAppraisee_2" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" >
           <%--<asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
         <td rowspan="5" style="background-color: lavender; width: 99px;">
         <asp:DropDownList ID="cbo1stAppraiser_2" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Enabled="False" >
           <%-- <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
         
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory2_2" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory2_3" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory2_4" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory2_5" runat="server" Width="610px" /></td>
         </tr>
         
         
         <tr>
         <td rowspan="5" style="background-color: honeydew">3</td>
         <td rowspan="5" align="left" style="background-color: honeydew"><asp:Label ID="lblCategory3" Width="200px" runat="server"/></td>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory3_1" runat="server" Width="610px" /></td>
         <td rowspan="5" style="background-color: honeydew">
         <asp:DropDownList ID="cboAppraisee_3" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" >
            <%--<asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
         <td rowspan="5" style="background-color: honeydew; width: 99px;">
         <asp:DropDownList ID="cbo1stAppraiser_3" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Enabled="False" >
           <%--<asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
         <%--<td rowspan="3" style="background-color: honeydew"><asp:DropDownList ID="cbo2ndAppraiser_3" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Enabled="False" >
            <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>
         </asp:DropDownList></td>--%>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory3_2" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory3_3" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory3_4" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory3_5" runat="server" Width="610px" /></td>
         </tr>
       
         
         <tr>
         <td rowspan="5" style="background-color: lavender">4</td>
         <td rowspan="5" align="left" style="background-color: lavender"><asp:Label ID="lblCategory4" Width="200px" runat="server" /></td>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory4_1" runat="server" Width="610px" /></td>
         <td rowspan="5" style="background-color: lavender">
         <asp:DropDownList ID="cboAppraisee_4" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" >
           <%--asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
         <td rowspan="5" style="background-color: lavender; width: 99px;">
         <asp:DropDownList ID="cbo1stAppraiser_4" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Enabled="False" >
         <%-- <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
         <%--<td rowspan="3" style="background-color: lavender"><asp:DropDownList ID="cbo2ndAppraiser_4" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Enabled="False" >
            <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>
         </asp:DropDownList></td>--%>
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory4_2" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory4_3" runat="server" Width="610px" /></td>
         </tr>
          <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory4_4" runat="server" Width="610px" /></td>
         </tr>
          <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory4_5" runat="server" Width="610px" /></td>
         </tr>
                  
         
         <tr>
         <td rowspan="5" style="background-color: honeydew">5</td>
         <td rowspan="5" align="left" style="background-color: honeydew"><asp:Label ID="lblCategory5" Width="200px" runat="server" /></td>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory5_1" runat="server" Width="610px" /></td>
         <td rowspan="5" style="background-color: honeydew">
         <asp:DropDownList ID="cboAppraisee_5" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" >
            <%--<asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
         <td rowspan="5" style="background-color: honeydew; width: 99px;">
         <asp:DropDownList ID="cbo1stAppraiser_5" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Enabled="False" >
           <%--<asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
         <%--<td rowspan="5" style="background-color: honeydew"><asp:DropDownList ID="cbo2ndAppraiser_5" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Enabled="False" >
            <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>
         </asp:DropDownList></td>--%>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory5_2" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory5_3" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory5_4" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory5_5" runat="server" Width="610px" /></td>
         </tr>
         
         <tr>
         <td rowspan="5" style="background-color: lavender">6</td>
         <td rowspan="5" align="left" style="background-color: lavender"><asp:Label ID="lblCategory6" Width="200px" runat="server" /></td>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory6_1" runat="server" Width="610px" /></td>
         <td rowspan="5" style="background-color: lavender">
         <asp:DropDownList ID="cboAppraisee_6" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" >
           <%--<asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
         <td rowspan="5" style="background-color: lavender; width: 99px;">
         <asp:DropDownList ID="cbo1stAppraiser_6" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Enabled="False" >
            <%--<asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
        <%-- <td rowspan="4" style="background-color: lavender"><asp:DropDownList ID="cbo2ndAppraiser_6" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Enabled="False" >
          <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>
         </asp:DropDownList></td>--%>
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory6_2" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory6_3" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory6_4" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory6_5" runat="server" Width="610px" /></td>
         </tr>
         
         
         <tr>
         <td rowspan="5" style="background-color: honeydew">7</td>
         <td rowspan="5" align="left" style="background-color: honeydew"><asp:Label ID="lblCategory7" Width="200px" runat="server" /></td>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory7_1" runat="server" Width="610px" /></td>
         <td rowspan="5" style="background-color: honeydew">
         <asp:DropDownList ID="cboAppraisee_7" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" >
            <%--<asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
         <td rowspan="5" style="background-color: honeydew; width: 99px;">
         <asp:DropDownList ID="cbo1stAppraiser_7" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Enabled="False" >
          <%--<asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
       <%--  <td rowspan="4" style="background-color: honeydew"><asp:DropDownList ID="cbo2ndAppraiser_7" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Enabled="False" >
            <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>
         </asp:DropDownList></td>--%>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory7_2" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory7_3" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory7_4" runat="server" Width="610px" /></td>
         </tr>
          <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory7_5" runat="server" Width="610px" /></td>
         </tr>
         
         
         <tr>
         <td rowspan="5" style="background-color: lavender">8</td>
         <td rowspan="5" align="left" style="background-color: lavender"><asp:Label ID="lblCategory8" Width="200px" runat="server" /></td>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory8_1" runat="server" Width="610px" /></td>
         <td rowspan="5" style="background-color: lavender">
         <asp:DropDownList ID="cboAppraisee_8" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" >
            <%--<asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
         <td rowspan="5" style="background-color: lavender; width: 99px;">
         <asp:DropDownList ID="cbo1stAppraiser_8" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Enabled="False" >
           <%-- <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
         <%--<td rowspan="4" style="background-color: lavender"><asp:DropDownList ID="cbo2ndAppraiser_8" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Enabled="False" >
           <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>
         </asp:DropDownList></td>--%>
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory8_2" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory8_3" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory8_4" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory8_5" runat="server" Width="610px" /></td>
         </tr>
         
         
         <tr>
         <td rowspan="5" style="background-color: honeydew">9</td>
         <td rowspan="5" align="left" style="background-color: honeydew"><asp:Label ID="lblCategory9" Width="200px" runat="server" /></td>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory9_1" runat="server" Width="610px" /></td>
         <td rowspan="5" style="background-color: honeydew">
         <asp:DropDownList ID="cboAppraisee_9" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" >
          <%--  <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
         <td rowspan="5" style="background-color: honeydew; width: 99px;">
         <asp:DropDownList ID="cbo1stAppraiser_9" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Enabled="False" >
           <%-- <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
        <%-- <td rowspan="3" style="background-color: honeydew"><asp:DropDownList ID="cbo2ndAppraiser_9" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Enabled="False" >
            <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>
         </asp:DropDownList></td>--%>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory9_2" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory9_3" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory9_4" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: honeydew"><asp:Label ID="lblCategory9_5" runat="server" Width="610px" /></td>
         </tr>
         
         <tr>
         <td rowspan="5" style="background-color: lavender">10</td>
         <td rowspan="5" align="left" style="background-color: lavender"><asp:Label ID="lblCategory10" Width="200px" runat="server" /></td>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory10_1" runat="server" Width="610px" /></td>
         <td rowspan="5" style="background-color: lavender">
         <asp:DropDownList ID="cboAppraisee_10" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" >
            <%--<asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
         <td rowspan="5" style="background-color: lavender; width: 99px;">
         <asp:DropDownList ID="cbo1stAppraiser_10" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Enabled="False" >
            <%--<asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>--%>
         </asp:DropDownList></td>
        <%-- <td rowspan="4" style="background-color: lavender"><asp:DropDownList ID="cbo2ndAppraiser_10" runat="server" Width="140px" BackColor="#E0E0E0" Font-Bold="False" Enabled="False" >
           <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">1 - Moderate</asp:ListItem>
            <asp:ListItem Value="2">2 - Good</asp:ListItem>
            <asp:ListItem Value="3">3 - Excellent</asp:ListItem>
         </asp:DropDownList></td>--%>
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory10_2" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory10_3" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory10_4" runat="server" Width="610px" /></td>
         </tr>
         <tr>
         <td align="left" style="background-color: lavender"><asp:Label ID="lblCategory10_5" runat="server" Width="610px" /></td>
         </tr>
         
         <tr>
         <td align="right" colspan="3" style="font-weight: bold; color: white; background-color: darkslategray">Total Points : </td>
         <td style="font-weight: bold; color: white; background-color: darkslategray"><asp:Label ID="lblAppraisee_Total" runat="server" >0</asp:Label></td>
         <td style="font-weight: bold; color: white; background-color: darkslategray"><asp:Label ID="lbl1stAppraiser_Total" runat="server" >0</asp:Label></td>
         <%--<td style="font-weight: bold; color: white; background-color: darkslategray"><asp:Label ID="lbl2ndAppraiser_Total" runat="server" >0</asp:Label></td>--%>
         </tr>
         </table><br />
         
         <table width="1160px">
         <tr align="right">
         <td style="font-weight: bold; color: white; background-color: darkslategray">
            Points Given By
         2nd Appraiser &nbsp;
         <asp:DropDownList ID="cbo2ndAppraiser" runat="server" Width="50px" BackColor="#E0E0E0" Enabled="False" >
            <%--<asp:ListItem></asp:ListItem>
               <asp:ListItem>10</asp:ListItem>
               <asp:ListItem>11</asp:ListItem>
               <asp:ListItem>12</asp:ListItem>
               <asp:ListItem>13</asp:ListItem>
               <asp:ListItem>14</asp:ListItem>
               <asp:ListItem>15</asp:ListItem>
               <asp:ListItem>16</asp:ListItem>
               <asp:ListItem>17</asp:ListItem>
               <asp:ListItem>18</asp:ListItem>
               <asp:ListItem>19</asp:ListItem>
               <asp:ListItem>20</asp:ListItem>
               <asp:ListItem>21</asp:ListItem>
               <asp:ListItem>22</asp:ListItem>
               <asp:ListItem>23</asp:ListItem>
               <asp:ListItem>24</asp:ListItem>
               <asp:ListItem>25</asp:ListItem>
               <asp:ListItem>26</asp:ListItem>
               <asp:ListItem>27</asp:ListItem>
               <asp:ListItem>28</asp:ListItem>
               <asp:ListItem>29</asp:ListItem>
               <asp:ListItem>30</asp:ListItem>--%>
            </asp:DropDownList>
         </td>
         </tr>
         </table><br />
         
         <asp:Button ID="btnOK" runat="server" Text="OK" Width="70px" />
    </div>
    <asp:HiddenField ID="hfLevel" runat="server" />
    </form>
</center>    

</body>
</html>