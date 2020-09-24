<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StaffTarget.aspx.vb" Inherits="PAGES_EAPPRAISAL_STAFFTARGET" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>eAppraisal - Target & Achievement</title>
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
    
    if (seconds<50)
    {
        document.forms["form1"]["d2"].value = ""
    }
    
    
     if (minute<0)
    {
       minute+=1 
       seconds=0
          
    }

   document.forms["form1"]["d2"].value = minute + " Minutes " + seconds + " Seconds" 
    
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
          <asp:LinkButton ID="lnkbtnTarget" Text="Target & Achievement" ForeColor="Red" runat="server"/>
          <asp:Label ID="lblPageSep1" Text="|" runat="server"/>
          <asp:LinkButton ID="lnkbtnSkills" Text="Skills"   runat="server"/>
          <asp:Label ID="lblPageSep4" Text="|" runat="server"/>
          <asp:LinkButton ID="lnkbtnComment" Text="Comments"   runat="server"/>
          <asp:Label ID="lblPageSep2" Text="|" runat="server"/>
          <asp:LinkButton ID="lnkReport" Text="Report"  runat="server"/>
          <asp:Label ID="lblPageSep3" Text="|" runat="server"/>
          <asp:LinkButton ID="lnkManual" Text="Help" runat="server" />
          <%--<a href="Appraisee Manual.pdf">Help</a>--%>
          </td>
          <td align="right">
          <asp:Label ID="lblUser" runat="server"/> |  <!--//<asp:LinkButton ID="lnkMain" Text="Main" runat="server" /><asp:LinkButton ID="lnkChangePwd" Text="Change Password" runat="server" Visible="False" /> | <asp:LinkButton ID="lnkLogout" runat="server" Text="Logout" />//-->
          </td>
          </tr>
          <tr>
          <td valign="top" colspan="2" style="width:1040px; height:1px"><hr id="Hr1" runat="server" /></td>
          </tr>
          </table>
          <table>
          <tr>
          <td align="center" style="width:200px"><asp:Label ID="lblAppraiseeSession" Width="200px" BorderColor="Black" BorderStyle="Solid" Font-Bold="True"  BorderWidth="2px" runat="server" ForeColor="White" BackColor="SteelBlue" /></td>
          <td align="center" style="width:670px"><input type="text" size="25" name="d2" style="color:white; font-weight: bold; border-left-color: black; border-bottom-color: black; border-top-style: solid; border-top-color: black; border-right-style: solid; border-left-style: solid; background-color: red; border-right-color: black; border-bottom-style: solid; text-align: center; font-size: 105%;" /></td>
          <td align="center"><asp:Label ID="lblAppraiserLevel" runat="server" BackColor="SteelBlue" BorderColor="Black" BorderStyle="Ridge" Font-Bold="True" ForeColor="White" Width="150px" BorderWidth="2px" /></td>
          </tr>
          <tr>
          <td colspan="3">&nbsp;</td>
          </tr>
       </table>
       
       
    
         <table cellpadding="0" cellspacing="2">
         <tr>
         <td align="left">
         <asp:RadioButton ID="optPersonal" Text="Personal" GroupName="Type"  runat="server" AutoPostBack="True" Checked="True" />
         </td>
         </tr>
         <tr>
         <td>
         <asp:RadioButton ID="optSubordinate" Text="Subordinate" GroupName="Type"  runat="server" AutoPostBack="True" />
         <asp:DropDownList ID="cboSubordinate" runat="server" Width="400px" AutoPostBack="True" Enabled="False"></asp:DropDownList>
         <asp:Button ID="btnLock" Text="Lock Appraisee's Session" Width="186px" runat="server" BackColor="Gold" BorderStyle="Solid" Font-Bold="True" ForeColor="Black" /><br />
         <asp:Label ID="lbltest" runat="server" Width="501px"/><asp:Label ID="lblLock" Width="182px" BorderColor="Black" BorderStyle="Solid" Font-Bold="True"  BorderWidth="2px" runat="server" ForeColor="White" BackColor="SteelBlue" />
         
         </td>
         </tr>
         </table>
         
        
         
         <br />
         
         
                  
         <table>
         <tr>
         <td>
          <asp:Label ID="lblAppraisal" Text="Appraisal Period" runat="server"></asp:Label>
         <asp:DropDownList ID="cboAppraisal" runat="server" AutoPostBack="True" Width="220px"></asp:DropDownList>
         </td>
         </tr>
         </table><br />
         
      
         <table>
         <tr align="left">
         <td>
         <asp:Label ID="Label1" Text="Department : " runat="server" />
         </td>
         <td>
         <asp:Label ID="lblDept" runat="server" Width="780px" />
         </td>
         <td>
         <asp:Label ID="Label29" Text="Staff Level" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:
         </td>
         <td align="center">
         <asp:Label ID="lblLevel" runat="server" Width="50px">8</asp:Label>
         </td>
         </tr>
         <tr align="left">
         <td>
         <asp:Label ID="Label28" Text="Designation : " runat="server" />
         </td>
         <td>
         <asp:Label ID="lblDesignation" runat="server" Width="780px" />
         </td>
         <td>
         <asp:Label ID="Label30" Text="Year(s) of Service : " runat="server" />
         </td>
         <td align="center">
         <asp:Label ID="lblYOS" runat="server" Width="50px" >20.8</asp:Label>
         </td>
         </tr>
         <tr>
         <td colspan="4" align="center">
         <asp:Label ID="lblError" runat="server" ForeColor="Black" Font-Bold="True" BorderColor="Black" BorderStyle="Double" Visible="False" Width="1030px"/>
         </td>
         </tr>
         </table>
         
         <table border="3" cellpadding="0" cellspacing="0" style="background-color: honeydew">
         <tr>
         <td align="center" style="background-color: darkcyan;">
         <asp:Label ID="lblDuties" Text=" Principal Duties" runat="server" Height="20px" Font-Bold="True" ForeColor="White"></asp:Label>
         </td>
         </tr>
         <tr>
         <td align="right"  >
         <asp:TextBox ID="txtDuties_1" runat="server" Columns="167" MaxLength="120" ></asp:TextBox><br />
         <asp:TextBox ID="txtDuties_2" runat="server" Columns="167" MaxLength="120" /><br />
         <asp:TextBox ID="txtDuties_3" runat="server" Columns="167" MaxLength="120" /><br />
         </td>
         </tr>
         </table>
         
         <br />
        
         <table  border="3"  cellpadding="0" cellspacing="0" style="background-color: honeydew">
         <tr>
         <td colspan="6" align="center" style="background-color: darkcyan;">
         <asp:Label ID="lblTarget1" Text="Career Target ( 1 )" runat="server" Height="20px" Font-Bold="True" ForeColor="White"></asp:Label>&nbsp;
         </td>
         <td colspan="6"  align="center" style="background-color: darkcyan;">
         <asp:Label ID="lblAchievement1" Text="Achievement ( 1 )" runat="server" Height="20px" Font-Bold="True" ForeColor="White"></asp:Label>
         </td>
         </tr>
         
         <tr>
         <td colspan="6">
         <asp:TextBox ID="txtTarget1_1" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget1_2" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget1_3" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget1_4" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget1_5" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget1_6" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget1_7" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget1_8" runat="server" Columns="80" MaxLength="70" />
         </td>
         
         <td>                                             
         <asp:TextBox ID="txtAchievement1_1" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement1_2" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement1_3" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement1_4" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement1_5" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement1_6" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement1_7" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement1_8" runat="server" Columns="80" MaxLength="70" />
         </td>
         </tr>
                 
         <tr>
         <td colspan="12" >
         
         <table border="1" cellpadding="0" cellspacing="0">
         <tr>
         <td colspan="4" align="center" style="font-weight: bold; color: white; background-color: #336666">
         Appraisee 
         </td>
         <td colspan="4" align="center" style="font-weight: bold; color: white; background-color: #336666">
         1st Appraiser
         </td>
         <td colspan="4" align="center" style="font-weight: bold; color: white; background-color: #336666">
         2nd Appraiser
         </td>
         </tr>
         <tr>
         <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Weightage : <asp:DropDownList ID="cboWeighting_1" Width="80px" runat="server" BackColor="Transparent" >
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>15</asp:ListItem>
            <asp:ListItem>20</asp:ListItem>
            <asp:ListItem>25</asp:ListItem>
            <asp:ListItem>30</asp:ListItem>
            <asp:ListItem>35</asp:ListItem>
            <asp:ListItem>40</asp:ListItem>
            <asp:ListItem>45</asp:ListItem>
            <asp:ListItem>50</asp:ListItem>
            <asp:ListItem>55</asp:ListItem>
            <asp:ListItem>60</asp:ListItem>
            <asp:ListItem>65</asp:ListItem>
            <asp:ListItem>70</asp:ListItem>
            <asp:ListItem>75</asp:ListItem>
            <asp:ListItem>80</asp:ListItem>
            <asp:ListItem>85</asp:ListItem>
            <asp:ListItem>90</asp:ListItem>
            <asp:ListItem>95</asp:ListItem>
            <asp:ListItem>100</asp:ListItem>
         </asp:DropDownList>
         </td>
         <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Scores :  
         <asp:DropDownList ID="cboPoints1_Appraisee" runat="server" Width="80px" >
         </asp:DropDownList>
         </td>
         <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Difficulty : 
          <asp:DropDownList ID="cboDifficulty1_1stAppraiser" runat="server" Width="80px" >
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>Low</asp:ListItem>
            <asp:ListItem>Medium</asp:ListItem>
            <asp:ListItem>High</asp:ListItem>
         </asp:DropDownList>
         </td>
         <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Scores : 
         <asp:DropDownList ID="cboPoints1_1stAppraiser" runat="server" Width="80px" >
         </asp:DropDownList>
         </td>
          <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Difficulty : 
         <asp:DropDownList ID="cboDifficulty1_2ndAppraiser" runat="server" Width="80px" >
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>Low</asp:ListItem>
            <asp:ListItem>Medium</asp:ListItem>
            <asp:ListItem>High</asp:ListItem>
         </asp:DropDownList>
         </td>
         <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Scores : 
          <asp:DropDownList ID="cboPoints1_2ndAppraiser" runat="server" Width="80px" >
         </asp:DropDownList>
         </td>
        
         </tr>
        </table>
         </td>
         </tr>
         </table>
         <br />
         
         <table  border="3"  cellpadding="0" cellspacing="0"  style="background-color: honeydew">
         <tr>
         <td colspan="6" align="center" style="background-color: darkcyan;">
         <asp:Label ID="lblTarget2" Text="Career Target ( 2 )" runat="server" Height="20px" Font-Bold="True" ForeColor="White"></asp:Label>&nbsp;
         </td>
         <td colspan="6"  align="center" style="background-color: darkcyan;">
         <asp:Label ID="lblAchievement2" Text="Achievement  ( 2 )" runat="server" Height="20px" Font-Bold="True" ForeColor="White"></asp:Label>
         </td>
         </tr>
         
         <tr>
         <td colspan="6">
         <asp:TextBox ID="txtTarget2_1" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget2_2" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget2_3" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget2_4" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget2_5" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget2_6" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget2_7" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget2_8" runat="server" Columns="80" MaxLength="70" />
         </td>
         
         <td>                                             
         <asp:TextBox ID="txtAchievement2_1" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement2_2" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement2_3" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement2_4" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement2_5" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement2_6" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement2_7" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement2_8" runat="server" Columns="80" MaxLength="70" />
         </td>
         </tr>
         
         
        
         
        
         
         <tr>
         <td colspan="12">
         
         <table border="1" cellpadding="0" cellspacing="0">
         <tr>
         <td colspan="4" align="center" style="font-weight: bold; color: white; background-color: #336666">
         Appraisee 
         </td>
         <td colspan="4" align="center" style="font-weight: bold; color: white; background-color: #336666">
         1st Appraiser
         </td>
         <td colspan="4" align="center" style="font-weight: bold; color: white; background-color: #336666">
         2nd Appraiser
         </td>
         </tr>
         <tr>
         <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Weightage : <asp:DropDownList ID="cboWeighting_2" Width="80px" runat="server">
          <asp:ListItem></asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>15</asp:ListItem>
            <asp:ListItem>20</asp:ListItem>
            <asp:ListItem>25</asp:ListItem>
            <asp:ListItem>30</asp:ListItem>
            <asp:ListItem>35</asp:ListItem>
            <asp:ListItem>40</asp:ListItem>
            <asp:ListItem>45</asp:ListItem>
            <asp:ListItem>50</asp:ListItem>
            <asp:ListItem>55</asp:ListItem>
            <asp:ListItem>60</asp:ListItem>
            <asp:ListItem>65</asp:ListItem>
            <asp:ListItem>70</asp:ListItem>
            <asp:ListItem>75</asp:ListItem>
            <asp:ListItem>80</asp:ListItem>
            <asp:ListItem>85</asp:ListItem>
            <asp:ListItem>90</asp:ListItem>
            <asp:ListItem>95</asp:ListItem>
            <asp:ListItem>100</asp:ListItem>
            </asp:DropDownList>
         </td>
         <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Scores :  
         <asp:DropDownList ID="cboPoints2_Appraisee" runat="server" Width="80px" >
         </asp:DropDownList>
         </td>
         <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Difficulty : 
          <asp:DropDownList ID="cboDifficulty2_1stAppraiser" runat="server" Width="80px" >
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>Low</asp:ListItem>
            <asp:ListItem>Medium</asp:ListItem>
            <asp:ListItem>High</asp:ListItem>
         </asp:DropDownList>
         </td>
         <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Scores : 
         <asp:DropDownList ID="cboPoints2_1stAppraiser" runat="server" Width="80px" >
         </asp:DropDownList>
         </td>
         <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Difficulty : 
         <asp:DropDownList ID="cboDifficulty2_2ndAppraiser" runat="server" Width="80px" >
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>Low</asp:ListItem>
            <asp:ListItem>Medium</asp:ListItem>
            <asp:ListItem>High</asp:ListItem>
         </asp:DropDownList>
         </td>
         <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Scores : 
          <asp:DropDownList ID="cboPoints2_2ndAppraiser" runat="server" Width="80px" >
         </asp:DropDownList>
         </td>
        
         </tr>
        </table>
         </td>
         </tr>
         </table>
         <br />
         
         <table  border="3"  cellpadding="0" cellspacing="0"  style="background-color: honeydew" id="lblAchievement3">
         <tr>
         <td colspan="6" align="center" style="background-color: darkcyan;">
         <asp:Label ID="lblTarget3" Text="Career Target ( 3 )" runat="server" Height="20px" Font-Bold="True" ForeColor="White"></asp:Label>&nbsp;
         </td>
         <td colspan="6"  align="center" style="background-color: darkcyan;">
         <asp:Label ID="lblAchievement3" Text="Achievement  ( 3 )" runat="server" Height="20px" Font-Bold="True" ForeColor="White"></asp:Label>
         </td>
         </tr>
         
         <tr>
         <td colspan="6">
         <asp:TextBox ID="txtTarget3_1" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget3_2" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget3_3" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget3_4" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget3_5" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget3_6" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget3_7" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtTarget3_8" runat="server" Columns="80" MaxLength="70" />
         </td>
         
         <td>                                             
         <asp:TextBox ID="txtAchievement3_1" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement3_2" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement3_3" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement3_4" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement3_5" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement3_6" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement3_7" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtAchievement3_8" runat="server" Columns="80" MaxLength="70" />
         </td>
         </tr>
        
         <tr>
         <td colspan="12">
         
         <table border="1" cellpadding="0" cellspacing="0">
         <tr>
         <td colspan="4" align="center" style="font-weight: bold; color: white; background-color: #336666">
         Appraisee 
         </td>
         <td colspan="4" align="center" style="font-weight: bold; color: white; background-color: #336666">
         1st Appraiser
         </td>
         <td colspan="4" align="center" style="font-weight: bold; color: white; background-color: #336666">
         2nd Appraiser
         </td>
         </tr>
         <tr>
         <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Weightage : <asp:DropDownList ID="cboWeighting_3" Width="80px" runat="server">
          <asp:ListItem></asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>15</asp:ListItem>
            <asp:ListItem>20</asp:ListItem>
            <asp:ListItem>25</asp:ListItem>
            <asp:ListItem>30</asp:ListItem>
            <asp:ListItem>35</asp:ListItem>
            <asp:ListItem>40</asp:ListItem>
            <asp:ListItem>45</asp:ListItem>
            <asp:ListItem>50</asp:ListItem>
            <asp:ListItem>55</asp:ListItem>
            <asp:ListItem>60</asp:ListItem>
            <asp:ListItem>65</asp:ListItem>
            <asp:ListItem>70</asp:ListItem>
            <asp:ListItem>75</asp:ListItem>
            <asp:ListItem>80</asp:ListItem>
            <asp:ListItem>85</asp:ListItem>
            <asp:ListItem>90</asp:ListItem>
            <asp:ListItem>95</asp:ListItem>
            <asp:ListItem>100</asp:ListItem>
            </asp:DropDownList>
         </td>
         <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Scores :  
         <asp:DropDownList ID="cboPoints3_Appraisee" runat="server" Width="80px" >
         </asp:DropDownList>
         </td>
         <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Difficulty : 
          <asp:DropDownList ID="cboDifficulty3_1stAppraiser" runat="server" Width="80px" >
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>Low</asp:ListItem>
            <asp:ListItem>Medium</asp:ListItem>
            <asp:ListItem>High</asp:ListItem>
         </asp:DropDownList>
         </td>
         <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Scores : 
         <asp:DropDownList ID="cboPoints3_1stAppraiser" runat="server" Width="80px" >
         </asp:DropDownList>
         </td>
          <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Difficulty : 
         <asp:DropDownList ID="cboDifficulty3_2ndAppraiser" runat="server" Width="80px" >
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>Low</asp:ListItem>
            <asp:ListItem>Medium</asp:ListItem>
            <asp:ListItem>High</asp:ListItem>
         </asp:DropDownList>
         </td>
         <td colspan="2" style="width: 171px; font-weight: bold; color: #ffffff; background-color: #336666;" align="right">
         Scores : 
          <asp:DropDownList ID="cboPoints3_2ndAppraiser" runat="server" Width="80px" >
         </asp:DropDownList>
         </td>
        
         </tr>
        </table>
         </td>
         </tr>
         </table>
         <br />   
         
         <table  border="3"  cellpadding="0" cellspacing="0" style="background-color: lavender">
         <tr>
         <td colspan="12" style="background-color: darkcyan; font-weight: bold; color: white;">1st Appraiser's Comment(s)</td>
         </tr>
         <tr>
         <td colspan="6" align="center" style="background-color: darkcyan; ">
         <asp:Label ID="lblComment_Target" Text="Career Target" runat="server" Height="20px" Font-Bold="True" ForeColor="White"></asp:Label>&nbsp;
         </td>
         <td colspan="6"  align="center" style="background-color: darkcyan;">
         <asp:Label ID="lblComment_Achievement" Text="Achievement" runat="server" Height="20px" Font-Bold="True" ForeColor="White"></asp:Label>
         </td>
         </tr>
         
         <tr>
         <td colspan="6">
         <asp:TextBox ID="txtCommentT_1" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtCommentT_2" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtCommentT_3" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtCommentT_4" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtCommentT_5" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtCommentT_6" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtCommentT_7" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtCommentT_8" runat="server" Columns="80" MaxLength="70" />
         </td>
         
         <td>                                             
         <asp:TextBox ID="txtCommentA_1" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtCommentA_2" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtCommentA_3" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtCommentA_4" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtCommentA_5" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtCommentA_6" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtCommentA_7" runat="server" Columns="80" MaxLength="70" /><br />
         <asp:TextBox ID="txtCommentA_8" runat="server" Columns="80" MaxLength="70" />
         </td>
         </tr>
      
         </table>
         <br />
         
         <table>
         <tr>
         <td style="width:520px; height:1px"><hr id="Hr2" runat="server" />
         </td>
         </tr>
          </table>
          <br />
                          
         <table>
         <tr>
         <td style="border-bottom-color: black; border-top-color: black; border-right-width: thin; border-right-color: black;">
         <table>
         <tr>
         <td colspan="2" style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid; width: 490px; border-bottom: black thin solid; background-color: lightslategray;">
         <asp:Label ID="Label9" Text="Rating Points" runat="server" Height="20px" Font-Bold="True" ForeColor="Snow"></asp:Label>
         </td>
         </tr>
         
         <tr>
         
         <td align="left"><asp:Label ID="Label10" runat="server" Font-Size="Medium" >5 = Excellent</asp:Label></td>
         <td align="left"><asp:Label ID="Label11" runat="server" Font-Size="Medium" >- Exceptional success with excellent quality.</asp:Label></td>
         </tr>
         
         <tr>

         <td align="left"><asp:Label ID="Label13" runat="server" Font-Size="Medium" >4 = Good</asp:Label></td>
         <td align="left"><asp:Label ID="Label14" runat="server" Font-Size="Medium" >- Superior success that is exceeded expectation.</asp:Label></td>
         </tr>
        
         <tr>

         <td align="left"><asp:Label ID="Label16" runat="server" Font-Size="Medium" >3 = Average</asp:Label></td>
         <td align="left"><asp:Label ID="Label17" runat="server" Font-Size="Medium" >- Fully success as per expectation.</asp:Label></td>
         </tr>
         
         <tr>

         <td align="left"><asp:Label ID="Label19" runat="server" Font-Size="Medium" >2 = Below Average</asp:Label></td>
         <td align="left"><asp:Label ID="Label20" runat="server" Font-Size="Medium" >- Minimum success - lack of awareness & quality.</asp:Label></td>
         </tr>
         
         <tr>

         <td align="left"><asp:Label ID="Label22" runat="server" Font-Size="Medium" >1 = Poor</asp:Label></td>
         <td align="left"><asp:Label ID="Label23" runat="server" Font-Size="Medium" >- Unsatisfactory that do not meet minimum requirement.</asp:Label></td>
         </tr>
         </table>
         </td>
         <td style="width: 30px">
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         </td>
          <td>
         <table>
         <tr>
         <td colspan="2" style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid; width: 490px; border-bottom: black thin solid; background-color: lightslategray;">
         <asp:Label ID="Label7" Text="Level of Difficulties" runat="server" Height="20px" Font-Bold="True" ForeColor="Snow"></asp:Label>
         </td>
         </tr>
         
         <tr>
         
         <td align="left"><asp:Label ID="Label8" runat="server" Font-Size="Medium" >H = High</asp:Label></td>
         <td align="left"><asp:Label ID="Label12" runat="server" Font-Size="Medium" >- Above the appraisee current level.</asp:Label></td>
         </tr>
         
         <tr>

         <td align="left"><asp:Label ID="Label15" runat="server" Font-Size="Medium" >M = Medium</asp:Label></td>
         <td align="left"><asp:Label ID="Label18" runat="server" Font-Size="Medium" >- Comparable with the appraisee current level.</asp:Label></td>
         </tr>
        
         <tr>

         <td align="left"><asp:Label ID="Label21" runat="server" Font-Size="Medium" >L = Low</asp:Label></td>
         <td align="left"><asp:Label ID="Label24" runat="server" Font-Size="Medium" >- Below the appraisee current level.</asp:Label></td>
         </tr>
         
         <tr>

         <td align="left">&nbsp;</td>
         <td align="left">&nbsp;</td>
         </tr>
         
         <tr>

         <td align="left">&nbsp;</td>
         <td align="left">&nbsp;</td>
         </tr>
         </table>
         </td>
         
         
         </tr>
         </table><br /><br />
         
          
         
         <!--//<table border="3" cellpadding="0" cellspacing="0" style="background-color: lavender">
         <tr>
         <td align="center" style="background-color: darkcyan;">
         <asp:Label ID="lblOverallComment_1st" Text="1st Appraiser's Overall Comment(s)" runat="server" Height="20px" Font-Bold="True" ForeColor="White"></asp:Label>
         </td>
         </tr>
         <tr>
         <td align="right" >
         <asp:TextBox ID="txtOverallComment_1st_1" runat="server" Columns="135" MaxLength="120" ></asp:TextBox><br />
         <asp:TextBox ID="txtOverallComment_1st_2" runat="server" Columns="135" MaxLength="120" /><br />
         <asp:TextBox ID="txtOverallComment_1st_3" runat="server" Columns="135" MaxLength="120" /><br />
         </td>
         <td align="center" style="background-color: darkcyan;">
         <table border="3" cellpadding="0" cellspacing="0" style="background-color: lavender">
         <tr><td colspan = 2 align="center" style="background-color: darkcyan;">
         <asp:Label ID="lblEmployeeScores_1" Text="Employee's Scores" runat="server" Height="20px" Font-Bold="True" ForeColor="White"></asp:Label></td>
         </tr>
         <tr><td align="center" >
         <asp:Label ID="lblTolAchievement_1" Text="Achievement" runat="server" Height="20px" Font-Bold="True" ForeColor="Black"></asp:Label></td>
         <td align="right">
         <asp:TextBox ID="txtTolAchievement_1" runat="server" Columns="10" MaxLength="10" ></asp:TextBox></td>
         </tr>
         <tr><td align="center" >
         <asp:Label ID="lblTolSkill_1" Text="Skill" runat="server" Height="20px" Font-Bold="True" ForeColor="Black"></asp:Label></td>
         <td align="right">
         <asp:TextBox ID="txtTolSkill_1" runat="server" Columns="10" MaxLength="10" ></asp:TextBox></td>
         </tr>
         <tr><td align="center" >
         <asp:Label ID="lblTolScore_1" Text="Total" runat="server" Height="20px" Font-Bold="True" ForeColor="Black"></asp:Label></td>
         <td align="right">
         <asp:TextBox ID="txtTolScore_1" runat="server" Columns="10" MaxLength="10" ></asp:TextBox></td>
         </tr>
         <tr><td align="center" >
         <asp:Label ID="lblRanking_1" Text="Ranking" runat="server" Height="20px" Font-Bold="True" ForeColor="Black"></asp:Label></td>
         <td align="right">
         <asp:DropDownList ID="cboRanking_1" runat="server" Columns="50" MaxLength="30" width = "100px">
            <asp:ListItem Value=""> </asp:ListItem>
            <asp:ListItem Value="1"> 1 - Marks 90% ~ 100% (Excellent)</asp:ListItem>
            <asp:ListItem Value="2"> 2 - Marks 75% ~ 89.9% (Good)</asp:ListItem>
            <asp:ListItem Value="3"> 3 - Marks 50% ~ 74.9% (Average)</asp:ListItem>
            <asp:ListItem Value="4"> 4 - Marks 30% ~ 49.9% (Below Average)</asp:ListItem>
            <asp:ListItem Value="5"> 5 - Marks 0% ~ 29.9% (Poor)</asp:ListItem>
            </asp:DropDownList></td>
         </tr>
         <tr><td align="center" colspan=2>
         <asp:Label ID="lblRanking_1_1" Text="(against 12 months period)" runat="server" Height="20px" Font-Bold="True" ForeColor="Black"></asp:Label></td></tr>
         </table></td>
         </tr>
         </table><br />
         
         <table border="3" cellpadding="0" cellspacing="0" style="background-color: lavender">
         <tr>
         <td align="center" style="background-color: darkcyan;">
         <asp:Label ID="lblFeedback" Text="1st Appraiser's Expectation / Feedback to Appraisee :" runat="server" Height="20px" Font-Bold="True" ForeColor="White"></asp:Label>
         </td>
         </tr>
         <tr>
         <td align="right" >
         <asp:TextBox ID="txtFeedBack_1" runat="server" Columns="165" MaxLength="160" /><br />
         <asp:TextBox ID="txtFeedBack_2" runat="server" Columns="165" MaxLength="160" /><br />
         <asp:TextBox ID="txtFeedBack_3" runat="server" Columns="165" MaxLength="160" /><br />
         </td></tr></table><br />
         
         <table border="3" cellpadding="0" cellspacing="0" style="background-color: lavender">
         <tr>
         <td align="center" style="background-color: darkcyan;">
         <asp:Label ID="lblOverallComment_2nd" Text="2nd Appraiser's Overall Comment(s)" runat="server" Height="20px" Font-Bold="True" ForeColor="White"></asp:Label>
         </td>
         </tr>
         <tr>
         <td align="right"  >
         <asp:TextBox ID="txtOverallComment_2nd_1" runat="server" Columns="135" MaxLength="120" ></asp:TextBox><br />
         <asp:TextBox ID="txtOverallComment_2nd_2" runat="server" Columns="135" MaxLength="120" /><br />
         <asp:TextBox ID="txtOverallComment_2nd_3" runat="server" Columns="135" MaxLength="120" /><br />
         </td>
         <td align="center" style="background-color: darkcyan;">
         <table border="3" cellpadding="0" cellspacing="0" style="background-color: lavender">
         <tr><td colspan = 2 align="center" style="background-color: darkcyan;">
         <asp:Label ID="lblEmployeeScores_2" Text="Employee's Scores" runat="server" Height="20px" Font-Bold="True" ForeColor="White"></asp:Label></td>
         </tr>
         <tr><td align="center" >
         <asp:Label ID="lblTolAchievement_2" Text="Achievement" runat="server" Height="20px" Font-Bold="True" ForeColor="Black"></asp:Label></td>
         <td align="right">
         <asp:TextBox ID="txtTolAchievement_2" runat="server" Columns="10" MaxLength="10" ></asp:TextBox></td>
         </tr>
         <tr><td align="center" >
         <asp:Label ID="lblTolSkill_2" Text="Skill" runat="server" Height="20px" Font-Bold="True" ForeColor="Black"></asp:Label></td>
         <td align="right">
         <asp:TextBox ID="txtTolSkill_2" runat="server" Columns="10" MaxLength="10" ></asp:TextBox></td>
         </tr>
         <tr><td align="center" >
         <asp:Label ID="lblTolScore_2" Text="Total" runat="server" Height="20px" Font-Bold="True" ForeColor="Black"></asp:Label></td>
         <td align="right">
         <asp:TextBox ID="txtTolScore_2" runat="server" Columns="10" MaxLength="10" ></asp:TextBox></td>
         </tr>
         <tr><td align="center">
         <asp:Label ID="lblRanking_2" Text="Ranking" runat="server" Height="20px" Font-Bold="True" ForeColor="Black"></asp:Label></td>
         <td align="right">
         <asp:DropDownList ID="cboRanking_2" runat="server" Columns="50" MaxLength="30" width = "100px">
         <asp:ListItem Value=""> </asp:ListItem>
            <asp:ListItem Value="1"> 1 - Marks 90% ~ 100% (Excellent)</asp:ListItem>
            <asp:ListItem Value="2"> 2 - Marks 75% ~ 89.9% (Good)</asp:ListItem>
            <asp:ListItem Value="3"> 3 - Marks 50% ~ 74.9% (Average)</asp:ListItem>
            <asp:ListItem Value="4"> 4 - Marks 30% ~ 49.9% (Below Average)</asp:ListItem>
            <asp:ListItem Value="5"> 5 - Marks 0% ~ 29.9% (Poor)</asp:ListItem></asp:DropDownList></td>
         </tr>
         <tr><td align="center" colspan=2>
         <asp:Label ID="lblRanking_2_2" Text="(against 12 months period)" runat="server" Height="20px" Font-Bold="True" ForeColor="Black"></asp:Label></td></tr>
         </table></td>
         </tr>
         </table><br />
         <table border="3" cellpadding="0" cellspacing="0" style="background-color: lavender">
         <tr>
         <td align="center" style="background-color: darkcyan;">
         <asp:Label ID="lblTraining" Text="TRAINING NEEDS ANALYSIS (To be completed and recommended by 1st Appraiser)" runat="server" Height="20px" Font-Bold="True" ForeColor="White"></asp:Label>
         </td>
         </tr>
         <tr>
         <td align="left" >
         <asp:label ID = "lblTrain1" Text="[1] Competent to perform daily roles & responsibilities" runat="server" Height="20px" Font-Bold="True"></asp:label>
         <asp:DropDownList ID="ddlTrain1" runat="server" Columns="135" MaxLength="120" autopostback="true">
         <asp:ListItem Value=""> </asp:ListItem>
            <asp:ListItem Value="YES"> Yes</asp:ListItem>
            <asp:ListItem Value="NO">No</asp:ListItem></asp:DropDownList><br />
         <asp:label ID = "lblTrain1_no" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;if No; area to improve " runat="server" Height="20px" Font-Bold="True"></asp:label>
         <asp:TextBox ID="txtTrain1_no" runat="server" Columns="90" MaxLength="80" /><br />
         <asp:label ID = "lblTrain2" Text="[2] Training Needs " runat="server" Height="20px" Font-Bold="True"></asp:label>
         <asp:DropDownList ID="ddlTrain2" runat="server" Columns="135" MaxLength="120" autopostback="true">
         <asp:ListItem Value=""> </asp:ListItem>
            <asp:ListItem Value="YES"> Yes</asp:ListItem>
            <asp:ListItem Value="NO">No</asp:ListItem></asp:DropDownList><br />
            <asp:label ID = "lblTrain2_Yes" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;if Yes; " runat="server" Height="20px" Font-Bold="True"></asp:label><br />
            <asp:label ID = "lblTrain2_2" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-  Type of Training  " runat="server" Height="20px" Font-Bold="True"></asp:label>
            <asp:DropDownList ID="ddlTypeTrain" runat="server" Columns="50" MaxLength="40" autopostback="true">
            <asp:ListItem Value=""> </asp:ListItem>
            <asp:ListItem Value="T"> on-the-job training</asp:ListItem>
            <asp:ListItem Value="E">external</asp:ListItem></asp:DropDownList>
            <asp:label ID = "lblTrain2_3" Text="&nbsp;or others  " runat="server" Height="20px" Font-Bold="True"></asp:label>
            <asp:TextBox ID="txtotherTrain" runat="server" Columns="50" MaxLength="40" /><br />
            <asp:label ID = "lblTrain_Ex" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- if external; focus on specific topic on " runat="server" Height="20px" Font-Bold="True"></asp:label><asp:TextBox ID="txtTrainTopic" runat="server" Columns="90" MaxLength="80" /><br />
            <asp:label ID = "lblMonitor" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- Monitor / supervised by  " runat="server" Height="20px" Font-Bold="True"></asp:label><asp:TextBox ID="txtMonitor" runat="server" Columns="90" MaxLength="80" /><br />
         </td></tr>
         </table>//-->
         
         <asp:Button ID="btnOK" runat="server" Text="OK" Width="70px" UseSubmitBehavior="False" />
       &nbsp;</div>
       <asp:HiddenField ID="hfLevel" runat="server" />
    </form>
    </center>
</body>
</html>
