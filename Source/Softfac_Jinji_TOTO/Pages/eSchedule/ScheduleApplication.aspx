<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScheduleApplication.aspx.vb" Inherits="PAGES_ESCHEDULE_SCHEDULEAPPLICATION" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>eSchedule - Schedule Application</title>
    <link href="images/favicon.ico" rel="shortcut icon" />
</head>
<body bgcolor="beige">
<center>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0">
    <tr>
    <td colspan="4">
    
    <table>
    <tr>
    <td align="left" colspan="3">
    <asp:Label ID="lblLeaveOvertime" runat="server" Text="Schedule: "/>
    <asp:LinkButton ID="lnkbtnLeaveApplication" Text="Plan & Application" runat="server" ForeColor="Red"/>
    <asp:Label ID="lblPageSep1" Text="|" runat="server"/>
    <asp:LinkButton ID="lnkbtnLeaveSchedule" Text="View" runat="server"/>
    <asp:Label ID="lblPageSep2" Text="|" runat="server"/>
    <asp:LinkButton ID="lnkbtnLeaveApproval" Text="Approval" runat="server"/>
    <asp:Label ID="lblPageSep3" Text="|" runat="server"/>
    <asp:LinkButton ID="lnkbtnLeaveReport" Text="Report" runat="server"/>
    <asp:Label ID="lblPageSep4" Text="|" runat="server"/>
    <a href="E-Schedule Manual.pdf">Help</a>
    </td>
    <td align="right">
    <asp:Label ID="lblUser" runat="server"/> |  <!--//<asp:LinkButton ID="lnkMain" Text="Main" runat="server" /><asp:LinkButton ID="lnkChangePwd" Text="Change Password" runat="server" Visible="False" /> | <asp:LinkButton ID="lnkLogout" runat="server" Text="Logout" />//-->
    </td>
    </tr>
    <tr>
    <td valign="top" colspan="4" style="width:1190px; height:1px"><hr id="Hr1" runat="server" /></td>
    </tr>
    </table>
    
    </td>
    </tr>
        <tr>
        <td colspan="2"></td>
       <td colspan="2" align="left">
       <table>
      <tr>
      <td colspan="2" align="Left">
        <asp:Label ID="lblType" runat="server" Text="Type" />
      <asp:DropDownList ID="cboType" runat="server" AutoPostBack="True" Width="100px">
          <asp:ListItem Value="P">Plan</asp:ListItem>
          <asp:ListItem Value="A">Apply</asp:ListItem>
      </asp:DropDownList>&nbsp; 
       <asp:Label ID="lblPeriod" runat="server" Text="Period" />
       <asp:DropDownList ID="cboPeriod" runat="server" AutoPostBack="True" Width="100px"/>&nbsp; 
       <asp:Label ID="lblLeave" runat="server"  Text="Schedule" />
       <asp:DropDownList ID="cboLeave" runat="server" AutoPostBack="True" Width="200px"/>
       &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="lblDestination" runat="server" Text="Destination" /> 
      <asp:TextBox ID="txtDestination" runat="server" Height="17px" MaxLength="50" Width="325px" BackColor="White"/>
      </td> 
      </tr>
      <tr>
      <td align="left">
      <asp:Label ID="lblReason" runat="server" Text="Reason / Purpose" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
      <asp:TextBox ID="txtReason" runat="server" Height="17px" MaxLength="150" Width="415px" BackColor="White"/>
      </td>
       <td align="right">
       <asp:CheckBox id="chkEmp" runat="server" AutoPostBack="True" />
        <asp:Label ID="lblEmp" runat="server" Text="On Behalf Of " />
      <asp:DropDownList ID="cboEmp" runat="server" Width="330px" AutoPostBack="True"/>
      </td>
      </tr>
      <tr>
      <td>&nbsp;</td>
      </tr>
      </table>
      </td>
      </tr>
     <tr>
     <td>
     <table border="1" cellpadding="0" cellspacing="0">
     <tr>
     <td align="center" style="background-color: darkgray; border-right: black thin solid; border-left: black thin solid; border-bottom: black thin solid; border-top: black thin solid">
     <asp:Label ID="Label2" runat="server" Text=" Offset Leave Earned " Width="150px" Font-Bold="True" ForeColor="White"/>
     </td>
     </tr>
     <tr>
     <td align="right" style="background-color: gainsboro">
     <asp:Label ID="Label1" runat="server" Text=" Year "/>
      <asp:DropDownList ID="cboOffsetYear" runat="server" AutoPostBack="True" Width="110px"/>
      </td>
      </tr>
      <tr>
      <td>&nbsp;</td>
      </tr>
      <tr>
      <td>
       <asp:ListBox ID="lstOffsetLeave"  runat="server" Width="155px" Height="255px" BackColor="Gainsboro"/>
       </td>
       </tr>
       <tr>
       <td colspan="2" valign="top" style="background-color: darkgray; width: 153px; border-right: black thin solid; border-left: black thin solid; border-bottom: black thin solid; border-top: black thin solid">
      <asp:Label ID="lblTotalOffset"  runat="server" Width="140px" Font-Bold="True" ForeColor="White"/>
      </td>
      </tr>
     </table>
     </td>
     <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
     <td colspan="2" align="left">
     
     <table border="1" cellspacing="0" cellpadding="1">
     <tr>
     <td align="center" colspan="14" style="background-color: darkcyan">
     
     
      
      <asp:Label ID="lblMonth" runat="server" Text="Month " Font-Bold="True" ForeColor="White" />
      <asp:DropDownList ID="cboMonth" runat="server" AutoPostBack="True" Width="100px">
         <asp:ListItem Value="1">January</asp:ListItem>
         <asp:ListItem Value="2">February</asp:ListItem>
         <asp:ListItem Value="3">March</asp:ListItem>
         <asp:ListItem Value="4">April</asp:ListItem>
         <asp:ListItem Value="5">May</asp:ListItem>
         <asp:ListItem Value="6">June</asp:ListItem>
         <asp:ListItem Value="7">July</asp:ListItem>
         <asp:ListItem Value="8">August</asp:ListItem>
         <asp:ListItem Value="9">September</asp:ListItem>
         <asp:ListItem Value="10">October</asp:ListItem>
         <asp:ListItem Value="11">November</asp:ListItem>
         <asp:ListItem Value="12">December</asp:ListItem>
      </asp:DropDownList>&nbsp; &nbsp;
      <asp:Label ID="lblYear" runat="server" Text=" Year " Font-Bold="True" ForeColor="White"/>
      <asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" Width="100px"/>
      </td>
      </tr>
      <tr>
      
     <td colspan="2" align="center" style="width: 68px; background-color: cornflowerblue"><asp:label ID="lblSunday" Text="Monday" runat="server" Width="140px" Font-Bold="True" ForeColor="White" BackColor="Transparent"/></td>
     <td colspan="2" align="center" style="width: 68px; background-color: cornflowerblue"><asp:label ID="lblMonday" Text="Tuesday" runat="server" Width="140px" Font-Bold="True" BackColor="Transparent" ForeColor="White"/></td>
     <td colspan="2" align="center" style="width: 68px; background-color: cornflowerblue"><asp:label ID="lblTuesday" Text="Wednesday" runat="server" Width="140px" Font-Bold="True" BackColor="Transparent" ForeColor="White"/></td>
     <td colspan="2" align="center" style="width: 68px; background-color: cornflowerblue"><asp:label ID="lblWednesday" Text="Thursday" runat="server" Width="140px" Font-Bold="True" BackColor="Transparent" ForeColor="White"/></td>
     <td colspan="2" align="center" style="width: 68px; background-color: cornflowerblue"><asp:label ID="lblThursday" Text="Friday" runat="server" Width="140px" Font-Bold="True" BackColor="Transparent" ForeColor="White"/></td>
     <td colspan="2" align="center" style="width: 68px; background-color: crimson"><asp:label ID="lblFriday" Text="Saturday" runat="server" Width="140px" Font-Bold="True" BackColor="Transparent" ForeColor="White"/></td>
     <td colspan="2" align="center" style="width: 68px; background-color: crimson"><asp:label ID="lblSaturday" Text="Sunday" runat="server" Width="140px" Font-Bold="True" ForeColor="White" BackColor="Transparent"/></td>
     </tr>     
     
     
     <tr>
     <%--<td align="center" style="width: 60px; background-color: lavender;"><asp:label ID="Label3" Text="Plan" runat="server" Width="68px" BackColor="Transparent" BorderColor="Transparent"/></td>--%><%--<td align="center" style="width: 60px; height: 30px; background-color: lavender;"><asp:label ID="Label4" Text="Apply" runat="server" Width="68px" BackColor="Transparent" BorderColor="Transparent"/></td>--%>
     <td align="center" style="width: 60px; background-color: lavender"><asp:label ID="lblPlan1" Text="Plan" runat="server" Width="68px" BackColor="Transparent" BorderColor="Transparent" Font-Bold="True" ForeColor="#404040"/></td><td align="center" style="width: 60px; background-color: lavender"><asp:label ID="lblApply1" Text="Apply" runat="server" Width="68px" BackColor="Transparent" BorderColor="Transparent" Font-Bold="True" ForeColor="#404040"/></td>
     <td align="center" style="width: 60px; background-color: lavender"><asp:label ID="lblPlan2" Text="Plan" runat="server" Width="68px" BackColor="Transparent" BorderColor="Transparent" Font-Bold="True" ForeColor="#404040"/></td><td align="center" style="width: 60px; background-color: lavender"><asp:label ID="lblApply2" Text="Apply" runat="server" Width="68px" BackColor="Transparent" BorderColor="Transparent" Font-Bold="True" ForeColor="#404040"/></td>
     <td align="center" style="width: 68px; background-color: lavender"><asp:label ID="lblPlan3" Text="Plan" runat="server" Width="68px" BackColor="Transparent" BorderColor="Transparent" Font-Bold="True" ForeColor="#404040"/></td><td align="center" style="width: 60px; background-color: lavender"><asp:label ID="lblApply3" Text="Apply" runat="server" Width="68px" BackColor="Transparent" BorderColor="Transparent" Font-Bold="True" ForeColor="#404040"/></td>
     <td align="center" style="width: 60px; background-color: lavender"><asp:label ID="lblPlan4" Text="Plan" runat="server" Width="68px" BackColor="Transparent" BorderColor="Transparent" Font-Bold="True" ForeColor="#404040"/></td><td align="center" style="width: 60px; background-color: lavender"><asp:label ID="lblApply4" Text="Apply" runat="server" Width="68px" BackColor="Transparent" BorderColor="Transparent" Font-Bold="True" ForeColor="#404040"/></td>
     <td align="center" style="width: 60px; background-color: lavender"><asp:label ID="lblPlan5" Text="Plan" runat="server" Width="68px" BackColor="Transparent" BorderColor="Transparent" Font-Bold="True" ForeColor="#404040"/></td><td align="center" style="width: 60px; background-color: lavender"><asp:label ID="lblApply5" Text="Apply" runat="server" Width="68px" BackColor="Transparent" BorderColor="Transparent" Font-Bold="True" ForeColor="#404040"/></td>
     <td align="center" style="width: 60px; background-color: papayawhip"><asp:label ID="lblPlan6" Text="Plan" runat="server" Width="68px" BackColor="Transparent" BorderColor="Transparent" Font-Bold="True" ForeColor="#404040"/></td><td align="center" style="width: 60px; background-color: papayawhip"><asp:label ID="lblApply6" Text="Apply" runat="server" Width="68px" BackColor="Transparent" BorderColor="Transparent" Font-Bold="True" ForeColor="#404040"/></td>
     <td align="center" style="width: 60px; background-color: papayawhip"><asp:label ID="lblPlan7" Text="Plan" runat="server" Width="68px" BackColor="Transparent" BorderColor="Transparent" Font-Bold="True" ForeColor="#404040"/></td><td align="center" style="width: 60px; background-color: papayawhip"><asp:label ID="lblApply7" Text="Apply" runat="server" Width="68px" BackColor="Transparent" BorderColor="Transparent" Font-Bold="True" ForeColor="#404040"/></td>
     </tr>
     
     <tr>
     <td colspan="14"><asp:HiddenField id="lblDayType1" runat="server"/>
     <asp:HiddenField id="lblDayType2" runat="server"/>
     <asp:HiddenField id="lblDayType3" runat="server"/>
     <asp:HiddenField id="lblDayType4" runat="server"/>
     <asp:HiddenField id="lblDayType5" runat="server"/>
     <asp:HiddenField id="lblDayType6" runat="server"/>
     <asp:HiddenField id="lblDayType7" runat="server"/>
     <asp:HiddenField id="lblDayType8" runat="server"/>
     <asp:HiddenField id="lblDayType9" runat="server"/>
     <asp:HiddenField id="lblDayType10" runat="server"/>
     <asp:HiddenField id="lblDayType11" runat="server"/>
     <asp:HiddenField id="lblDayType12" runat="server"/>
     <asp:HiddenField id="lblDayType13" runat="server"/>
     <asp:HiddenField id="lblDayType14" runat="server"/>
     
     <asp:HiddenField id="lblDayType15" runat="server"/>
     <asp:HiddenField id="lblDayType16" runat="server"/>
     <asp:HiddenField id="lblDayType17" runat="server"/>
     <asp:HiddenField id="lblDayType18" runat="server"/>
     <asp:HiddenField id="lblDayType19" runat="server"/>
     <asp:HiddenField id="lblDayType20" runat="server"/>
     <asp:HiddenField id="lblDayType21" runat="server"/>
     <asp:HiddenField id="lblDayType22" runat="server"/>
     <asp:HiddenField id="lblDayType23" runat="server"/>
     <asp:HiddenField id="lblDayType24" runat="server"/>
     <asp:HiddenField id="lblDayType25" runat="server"/>
     <asp:HiddenField id="lblDayType26" runat="server"/>
     <asp:HiddenField id="lblDayType27" runat="server"/>
     <asp:HiddenField id="lblDayType28" runat="server"/>
     <asp:HiddenField id="lblDayType29" runat="server"/>
     <asp:HiddenField id="lblDayType30" runat="server"/>
     <asp:HiddenField id="lblDayType31" runat="server"/>
     <asp:HiddenField id="lblDayType32" runat="server"/>
     <asp:HiddenField id="lblDayType33" runat="server"/>
     <asp:HiddenField id="lblDayType34" runat="server"/>
     <asp:HiddenField id="lblDayType35" runat="server"/>
     &nbsp;</td>
     </tr>
     
     
     
     <tr>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay1" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay2" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay3" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay4" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay5" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay6" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay7" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     </tr>
     
     <tr style="font-size:small">
     <td align="center" style="width: 60px; height: 19px"><asp:label ID="lblPlanData1" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData1" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 68px; height: 19px;"><asp:label ID="lblPlanData2" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData2" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 68px; height: 19px;"><asp:label ID="lblPlanData3" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData3" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 60px; height: 19px;"><asp:label ID="lblPlanData4" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData4" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 60px; height: 19px;"><asp:label ID="lblPlanData5" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 64px"><asp:label ID="lblApplyData5" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 60px; height: 19px;"><asp:label ID="lblPlanData6" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData6" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 60px; height: 19px;"><asp:label ID="lblPlanData7" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData7" Text="" runat="server" Width="68px" Height="18px"/></td>
     </tr>     
     
         
     <tr>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay8" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay9" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay10" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay11" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay12" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay13" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay14" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     </tr>
     
     
     <tr style="font-size:small">
     <td align="center" style="width: 68px; height: 19px"><asp:label ID="lblPlanData8" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData8" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 68px; height: 19px;"><asp:label ID="lblPlanData9" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData9" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 68px; height: 19px;"><asp:label ID="lblPlanData10" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData10" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 68px; height: 19px;"><asp:label ID="lblPlanData11" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData11" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 68px; height: 19px;"><asp:label ID="lblPlanData12" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 64px"><asp:label ID="lblApplyData12" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 68px; height: 19px;"><asp:label ID="lblPlanData13" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData13" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 68px; height: 19px;"><asp:label ID="lblPlanData14" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData14" Text="" runat="server" Width="68px" Height="18px"/></td>
     </tr>  
     
     <tr>
     <td colspan="2" style="height: 19px; width: 68px"><asp:Button ID="btnDay15" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="height: 19px; width: 68px;"><asp:Button ID="btnDay16" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="height: 19px; width: 68px;"><asp:Button ID="btnDay17" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="height: 19px; width: 68px;"><asp:Button ID="btnDay18" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="height: 19px; width: 68px;"><asp:Button ID="btnDay19" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="height: 19px; width: 68px;"><asp:Button ID="btnDay20" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="height: 19px; width: 68px"><asp:Button ID="btnDay21" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     </tr>
     
     <tr style="font-size:small">
     <td align="center" style="width: 60px; height: 19px"><asp:label ID="lblPlanData15" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData15" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 68px; height: 19px;"><asp:label ID="lblPlanData16" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData16" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 68px; height: 19px;"><asp:label ID="lblPlanData17" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData17" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 60px; height: 19px;"><asp:label ID="lblPlanData18" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData18" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 60px; height: 19px;"><asp:label ID="lblPlanData19" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 64px"><asp:label ID="lblApplyData19" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 60px; height: 19px;"><asp:label ID="lblPlanData20" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData20" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 60px; height: 19px;"><asp:label ID="lblPlanData21" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData21" Text="" runat="server" Width="68px" Height="18px"/></td>
     </tr>  
     
     <tr>
     <td colspan="2" style="width: 68px"><asp:Button ID="btnDay22" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px;"><asp:Button ID="btnDay23" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px;"><asp:Button ID="btnDay24" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px;"><asp:Button ID="btnDay25" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px;"><asp:Button ID="btnDay26" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px;"><asp:Button ID="btnDay27" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px"><asp:Button ID="btnDay28" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     </tr>
     
     <tr style="font-size:small">
     <td align="center" style="width: 60px; height: 19px"><asp:label ID="lblPlanData22" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData22" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 68px; height: 19px;"><asp:label ID="lblPlanData23" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData23" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 68px; height: 19px;"><asp:label ID="lblPlanData24" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData24" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 60px; height: 19px;"><asp:label ID="lblPlanData25" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData25" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 60px; height: 19px;"><asp:label ID="lblPlanData26" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 64px"><asp:label ID="lblApplyData26" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 60px; height: 19px;"><asp:label ID="lblPlanData27" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData27" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="width: 60px; height: 19px;"><asp:label ID="lblPlanData28" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData28" Text="" runat="server" Width="68px" Height="18px"/></td>
     </tr>  
     
     <tr>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay29" Text="Apply" runat="server" Height="24px" Width="140px"  /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay30" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay31" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay32" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay33" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay34" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     <td colspan="2" style="width: 68px; height: 19px"><asp:Button ID="btnDay35" Text="Apply" runat="server" Height="24px" Width="140px" /></td>
     </tr>
     
     
     <tr style="font-size:small">
     <td align="center" style="height: 19px; width: 60px"><asp:label ID="lblPlanData29" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px;"><asp:label ID="lblApplyData29" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="height: 19px; width: 68px;"><asp:label ID="lblPlanData30" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px;"><asp:label ID="lblApplyData30" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="height: 19px; width: 68px;"><asp:label ID="lblPlanData31" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px;"><asp:label ID="lblApplyData31" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="height: 19px; width: 60px;"><asp:label ID="lblPlanData32" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px;"><asp:label ID="lblApplyData32" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="height: 19px; width: 60px;"><asp:label ID="lblPlanData33" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 64px;"><asp:label ID="lblApplyData33" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="height: 19px; width: 60px;"><asp:label ID="lblPlanData34" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px;"><asp:label ID="lblApplyData34" Text="" runat="server" Width="68px" Height="18px"/></td>
     <td align="center" style="height: 19px; width: 60px;"><asp:label ID="lblPlanData35" Text="" runat="server" Width="68px" Height="18px"/></td><td align="center" style="width: 60px"><asp:label ID="lblApplyData35" Text="" runat="server" Width="68px" Height="18px"/></td>
     </tr>  
      </table>
      </td>
     </tr>     
     
     <tr>
     <td colspan="4" align="left">
     <table cellpadding="0" cellspacing="0">
     <tr>
     <td align="left">
     <br />
     
     <table cellspacing="0" cellpadding="0" >
     <tr>
     <td align="center" colspan="4" style="font-weight: bold; background-color: darkgray; border-right: black thin solid; border-top: black thin solid; border-left: black thin solid; border-bottom: black thin solid;" >
     <asp:label ID="lblCutOff" runat="server" BackColor="Transparent" ForeColor="White" Width="325px"/>
     </td>
     <td rowspan="4" style="width:38px">&nbsp;</td>
     <td align="center" style="background-color: CadetBlue; height: 19px;">
     <asp:Label ID="Label3" runat="server" Text=" Public Holiday " Width="325px" Font-Bold="True" ForeColor="White"/>
     </td>
     <td rowspan="4" style="width:38px">&nbsp;</td>
     <td colspan="2">
     <asp:Button ID="btnApproval" Text="Submit  Leave/Overtime Application" runat="server" Width="455px"  OnClientClick="return confirm('Are you sure?');" />
     </td>
     
     </tr>
     
     <tr>
     <td style="background-color:darkgray; border-right: black thin solid; border-top-color: white; border-bottom: black thin solid; font-weight: bold; border-left: black thin solid; border-top-width: thin; height: 19px; color: white;" align="center" rowspan="2">Entitlement</td>
     <td style="font-weight: bold; background-color:darkgray; border-top-width: thin; border-right: black thin solid; border-left-width: thin; border-left-color: white; border-top-color: white; border-bottom: black thin solid; height: 19px; color: white;" align="center" colspan="2">Applied</td>
     <td align="center" rowspan="2" style="background-color:darkgray; border-top-width: thin; border-right: black thin solid; border-left-width: thin; border-left-color: white; border-top-color: white; border-bottom: black thin solid; font-weight: bold; height: 19px; color: white;" >Balance</td>
     <%--<td/>--%>
     <td rowspan="3"><asp:ListBox ID="lstHoliday"  runat="server" Width="325px" Height="70px" BackColor="Honeydew"/></td>
     </tr>
     
     <tr>
     <td align="center" style="font-weight: bold; background-color: darkgray; border-top-width: thin; border-right: black thin solid; border-left-color: white; border-top-color: white; border-bottom: black thin solid; height: 19px; border-left-width: thin; color: white;">Pending</td>
     <td align="center" style="background-color:darkgray; border-top-width: thin; border-right: black thin solid; border-left-width: thin; border-left-color: white; border-top-color: white; border-bottom: black thin solid; font-weight: bold; height: 19px; color: white;">Approved</td>
     <%--<td>&nbsp;</td>--%>
     
     <td colspan="2" align="center"><asp:label ID="lblTo" Text="To "  runat="server"/>&nbsp;<asp:textbox ID="lblEmailTo" ReadOnly="true" runat="server" BorderStyle="Inset" BackColor="Honeydew" ForeColor="DarkSlateGray" Width="425px" Font-Bold="True"/></td>
     </tr>
     
     <tr>
     <td align="center" style="background-color:Gainsboro; border-top-width: thin; border-right: black thin solid; border-top-color: white; border-bottom: black thin solid; border-left: black thin solid;"><asp:label ID="lblEnt" runat="server" Width="90px"/></td>
     <td align="center" style="background-color:Gainsboro; border-top-width: thin; border-right: black thin solid; border-left-width: thin; border-left-color: white; border-top-color: white; border-bottom: black thin solid;"><asp:label ID="lblPending" runat="server" Width="75px"/></td>
     <td align="center" style="background-color:Gainsboro; border-top-width: thin; border-right: black thin solid; border-left-width: thin; border-left-color: white; border-top-color: white; border-bottom: black thin solid;"><asp:label ID="lblApproved" runat="server" Width="75px"/></td>
     <td align="center" style="background-color:Gainsboro; border-top-width: thin; border-right: black thin solid; border-left-width: thin; border-left-color: white; width: 76px; border-top-color: white; border-bottom: black thin solid;"><asp:label ID="lblBal" runat="server" Width="75px"/></td>
     <%--<td style="width:38px">&nbsp;</td>--%>
     <td colspan="2" align="center"><asp:label ID="lblCc" Text="Cc " runat="server"/>&nbsp;<asp:textbox ID="lblEmailCc" ReadOnly="true" runat="server" BorderStyle="Inset" BackColor="Honeydew" ForeColor="DarkSlateGray" Width="425px" Font-Bold="True"/></td>
     </tr>
     </table>
     </td>
     
    <%-- 
     <td align="right">
     <br />
     <table cellpadding="0" cellspacing="0">
     <tr>
     <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
     <td align="center" style="background-color: CadetBlue; height: 19px;">
     <asp:Label ID="Label3" runat="server" Text=" Public Holiday " Width="325px" Font-Bold="True" ForeColor="White"/>
     </td>
     </tr>
      <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
      <td rowspan="3">
      <asp:ListBox ID="lstHoliday"  runat="server" Width="325px" Height="70px" BackColor="Honeydew"/>
      </td>
      </tr>
      </table>
      </td>--%>
     
      </tr>
     </table>
     </td>
    
    </tr> 
    </table> 
   
    <asp:HiddenField ID="hfEmp" runat="server" />
    </form>
    

    </center>
</body>


     <%-- <script type="text/javascript"  language="javascript"> 
           alert( 'This is an ALERT message to the user!' )
           
       </script>--%>
    

</html>
