<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewSchedule.aspx.vb" Inherits="PAGES_ESCHEDULE_VIEWSCHEDULE" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>eSchedule - View Schedule</title>
    <link href="images/favicon.ico" rel="shortcut icon" />
</head>
<body bgcolor="beige">
<center>
   <form id="form1" runat="server">
     <%-- <table cellpadding="0" cellspacing="0">
         <tr>
            <td style="height: 83px">--%>
               <table width="1355px">
                  <tr>
                     <td align="left">
                        <asp:label id="lblLeaveOvertime" runat="server" text="Schedule: "></asp:label>
                        <asp:linkbutton id="lnkbtnLeaveApplication" runat="server" forecolor="Red" text="Plan & Application"></asp:linkbutton>
                        <asp:label id="lblPageSep1" runat="server" text="|"></asp:label>
                        <asp:linkbutton id="lnkbtnLeaveSchedule" runat="server" text="View"></asp:linkbutton>
                        <asp:label id="lblPageSep2" runat="server" text="|"></asp:label>
                        <asp:linkbutton id="lnkbtnLeaveApproval" runat="server" text="Approval"></asp:linkbutton>
                        <asp:Label ID="lblPageSep3" Text="|" runat="server"/>
                         <asp:LinkButton ID="lnkbtnLeaveReport" Text="Report" runat="server"/>
                         <asp:Label ID="lblPageSep4" Text="|" runat="server"/>
                         <a href="E-Schedule Manual.pdf">Help</a>
                     </td>
                     <td align="right">
                        <asp:Label ID="lblUser" runat="server"/> | <!--//<asp:LinkButton ID="lnkMain" Text="Main" runat="server" /><asp:LinkButton ID="lnkChangePwd" Text="Change Password" runat="server" Visible="False" /> | <asp:LinkButton ID="lnkLogout" runat="server" Text="Logout" />//-->
                     </td>
                  </tr>
                  <tr>
                     <td colspan="2" style="height: 1px" valign="top">
                        <hr id="Hr1" runat="server" />
                     </td>
                  </tr>
                  <tr>
                     <td colspan="2" align="left">
                        <asp:RadioButton ID="optDepartment" Text="Department" runat="server" Checked="True" GroupName="Type" AutoPostBack="True" />
                        <asp:DropDownList ID="cboDepartment" runat="server" AutoPostBack="True" Width="255px" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="optEmp" Text="Employee" runat="server" GroupName="Type" AutoPostBack="True" />  
                        <asp:TextBox ID="txtEmp" runat="server" MaxLength="150" Width="255px"/>
                        <asp:Button ID="btnSearch" Text="Search" runat="server" Width="60"/>
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <%--</td>
                      <td align="center">--%>
                      <asp:Label ID="lblRecords" Text="Records" runat="server"/>&nbsp;
                      <asp:LinkButton ID="lnkPreviousPage" Text="<" runat="server" Font-Bold="True" Font-Size="Large" ToolTip="Backward"/>
                      <asp:Label ID="lblSep2" Text=" | " runat="server" Font-Bold="True"/>
                      <asp:LinkButton ID="lnkNextPage" Text=">" runat="server" Font-Bold="True" Font-Size="Large" ToolTip="Forward"/>
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:Label ID="lblLegend" runat="server" Text="Legend"></asp:Label>
                      <asp:DropDownList ID="cboLegend" runat="server" Width="220px" />
                      
                      </td>
                  </tr>
                  
               </table>
               <br />
         
               <table border="1" cellpadding="3" cellspacing="0">
                  <tr>
                     <td colspan="16" align="center" style="background-color: darkcyan">
                        <asp:label id="lblWeek" runat="server" text="Week " Font-Bold="True" ForeColor="White"></asp:label>
                     
    
                         <asp:LinkButton ID="lnkbtnPrevious" Text="<" runat="server" ForeColor="White" Font-Bold="True" Font-Size="Large" ToolTip="Previous"/>
                         <asp:Label ID="lblSep1" Text=" | " runat="server" Font-Bold="True" ForeColor="#E0E0E0"/>
                         <asp:LinkButton ID="lnkbtnNext" Text=">" runat="server" ForeColor="White" Font-Bold="True" Font-Size="Large" ToolTip="Next"/>
                         
                          &nbsp; &nbsp;&nbsp;
    
    
                        <asp:label id="lblMonth" runat="server" text="Month " Font-Bold="True" ForeColor="White"></asp:label>
                        <asp:dropdownlist id="cboMonth" runat="server" autopostback="True" width="100px">
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
                        </asp:dropdownlist>
                        &nbsp; &nbsp;
                        <asp:label id="lblYear" runat="server" text=" Year " Font-Bold="True" ForeColor="White"></asp:label>
                        <asp:dropdownlist id="cboYear" runat="server" autopostback="True" width="100px"></asp:dropdownlist>
                     </td>
                    </tr>
                     <tr>
                     <td align="center" rowspan="2" style="width: 25px; background-color: lavender">
                        <asp:label id="lblNo" runat="server" backcolor="Transparent" font-bold="True"
                           forecolor="#404040" text="No." width="25px"></asp:label>
                     </td>
                     <td align="center" rowspan="2" style="width: 250px; background-color: lavender">
                        <asp:label id="lblEmpName" runat="server" backcolor="Transparent" font-bold="True"
                           forecolor="#404040" text="Employee" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" colspan="2" style="width: 68px; background-color: cornflowerblue">
                        <asp:label id="lblMonday" runat="server" backcolor="Transparent" font-bold="True"
                           forecolor="White" text="Monday" width="140px"></asp:label>
                     </td>
                     <td align="center" colspan="2" style="width: 68px; background-color: cornflowerblue">
                        <asp:label id="lblTuesday" runat="server" backcolor="Transparent" font-bold="True"
                           forecolor="White" text="Tuesday" width="140px"></asp:label>
                     </td>
                     <td align="center" colspan="2" style="width: 68px; background-color: cornflowerblue">
                        <asp:label id="lblWednesday" runat="server" backcolor="Transparent" font-bold="True"
                           forecolor="White" text="Wednesday" width="140px"></asp:label>
                     </td>
                     <td align="center" colspan="2" style="width: 68px; background-color: cornflowerblue">
                        <asp:label id="lblThursday" runat="server" backcolor="Transparent" font-bold="True"
                           forecolor="White" text="Thursday" width="140px"></asp:label>
                     </td>
                     <td align="center" colspan="2" style="width: 68px; background-color: cornflowerblue">
                        <asp:label id="lblFriday" runat="server" backcolor="Transparent" font-bold="True"
                           forecolor="White" text="Friday" width="140px"></asp:label>
                     </td>
                     <td align="center" colspan="2" style="width: 68px; background-color: crimson">
                        <asp:label id="lblSaturday" runat="server" backcolor="Transparent" font-bold="True"
                           forecolor="White" text="Saturday" width="140px"></asp:label>
                     </td>
                     <td align="center" colspan="2" style="width: 68px; background-color: crimson">
                        <asp:label id="lblSunday" runat="server" backcolor="Transparent" font-bold="True"
                           forecolor="White" text="Sunday" width="140px"></asp:label>
                     </td>
                  </tr>
                  <tr>
                    
                     <td align="center" style="width: 60px; background-color: lavender">
                        <asp:label id="lblPlan1" runat="server" backcolor="Transparent" bordercolor="Transparent"
                           text="Plan" width="68px" Font-Bold="True" ForeColor="#404040"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender">
                        <asp:label id="lblApply1" runat="server" backcolor="Transparent" bordercolor="Transparent"
                           text="Apply" width="68px" Font-Bold="True" ForeColor="#404040"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender">
                        <asp:label id="lblPlan2" runat="server" backcolor="Transparent" bordercolor="Transparent"
                           text="Plan" width="68px" Font-Bold="True" ForeColor="#404040"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender">
                        <asp:label id="lblApply2" runat="server" backcolor="Transparent" bordercolor="Transparent"
                           text="Apply" width="68px" Font-Bold="True" ForeColor="#404040"></asp:label>
                     </td>
                     <td align="center" style="width: 68px; background-color: lavender">
                        <asp:label id="lblPlan3" runat="server" backcolor="Transparent" bordercolor="Transparent"
                           text="Plan" width="68px" Font-Bold="True" ForeColor="#404040"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender">
                        <asp:label id="lblApply3" runat="server" backcolor="Transparent" bordercolor="Transparent"
                           text="Apply" width="68px" Font-Bold="True" ForeColor="#404040"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender">
                        <asp:label id="lblPlan4" runat="server" backcolor="Transparent" bordercolor="Transparent"
                           text="Plan" width="68px" Font-Bold="True" ForeColor="#404040"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender">
                        <asp:label id="lblApply4" runat="server" backcolor="Transparent" bordercolor="Transparent"
                           text="Apply" width="68px" Font-Bold="True" ForeColor="#404040"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender">
                        <asp:label id="lblPlan5" runat="server" backcolor="Transparent" bordercolor="Transparent"
                           text="Plan" width="68px" Font-Bold="True" ForeColor="#404040"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender">
                        <asp:label id="lblApply5" runat="server" backcolor="Transparent" bordercolor="Transparent"
                           text="Apply" width="68px" Font-Bold="True" ForeColor="#404040"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: papayawhip">
                        <asp:label id="lblPlan6" runat="server" backcolor="Transparent" bordercolor="Transparent"
                           text="Plan" width="68px" Font-Bold="True" ForeColor="#404040"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: papayawhip">
                        <asp:label id="lblApply6" runat="server" backcolor="Transparent" bordercolor="Transparent"
                           text="Apply" width="68px" Font-Bold="True" ForeColor="#404040"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: papayawhip">
                        <asp:label id="lblPlan7" runat="server" backcolor="Transparent" bordercolor="Transparent"
                           text="Plan" width="68px" Font-Bold="True" ForeColor="#404040"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: papayawhip">
                        <asp:label id="lblApply7" runat="server" backcolor="Transparent" bordercolor="Transparent"
                           text="Apply" width="68px" Font-Bold="True" ForeColor="#404040"></asp:label>
                     </td>
                  </tr>
                  <tr>
                     <td colspan="16" style="height: 18px">
                        &nbsp;</td>
                  </tr>
                  <tr style="font-size: small">
                     <td align="center" style="width: 25px; background-color: honeydew;">
                        <asp:label id="lblNo1" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: honeydew; ">
                        <asp:label id="lblEmpName1" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblMon1_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblMon1_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblTue1_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblTue1_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblWed1_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblWed1_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblThu1_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblThu1_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblFri1_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblFri1_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: honeydew;">
                        <asp:label id="lblSat1_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew; border-right: gray thin solid">
                        <asp:label id="lblSat1_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblSun1_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew">
                        <asp:label id="lblSun1_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                     <td align="center" style="width: 25px; background-color: lavender;">
                        <asp:label id="lblNo2" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblEmpName2" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblMon2_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblMon2_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblTue2_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblTue2_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblWed2_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblWed2_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblThu2_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblThu2_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblFri2_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblFri2_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblSat2_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender; border-right: gray thin solid">
                        <asp:label id="lblSat2_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblSun2_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender">
                        <asp:label id="lblSun2_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: honeydew;">
                        <asp:label id="lblNo3" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: honeydew">
                        <asp:label id="lblEmpName3" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblMon3_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblMon3_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblTue3_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblTue3_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblWed3_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblWed3_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblThu3_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblThu3_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblFri3_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblFri3_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: honeydew;">
                        <asp:label id="lblSat3_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew; border-right: gray thin solid">
                        <asp:label id="lblSat3_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblSun3_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew">
                        <asp:label id="lblSun3_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: lavender;">
                        <asp:label id="lblNo4" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: lavender">
                        <asp:label id="lblEmpName4" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblMon4_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblMon4_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblTue4_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblTue4_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblWed4_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblWed4_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblThu4_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblThu4_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblFri4_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblFri4_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblSat4_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender; border-right: gray thin solid">
                        <asp:label id="lblSat4_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblSun4_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender">
                        <asp:label id="lblSun4_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: honeydew;">
                        <asp:label id="lblNo5" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: honeydew">
                        <asp:label id="lblEmpName5" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblMon5_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblMon5_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblTue5_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblTue5_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblWed5_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblWed5_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblThu5_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblThu5_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblFri5_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblFri5_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: honeydew;">
                        <asp:label id="lblSat5_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew; border-right: gray thin solid">
                        <asp:label id="lblSat5_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblSun5_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew">
                        <asp:label id="lblSun5_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: lavender;">
                        <asp:label id="lblNo6" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: lavender">
                        <asp:label id="lblEmpName6" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblMon6_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblMon6_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblTue6_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblTue6_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblWed6_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblWed6_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblThu6_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblThu6_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblFri6_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblFri6_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblSat6_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender; border-right: gray thin solid">
                        <asp:label id="lblSat6_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblSun6_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender">
                        <asp:label id="lblSun6_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: honeydew;">
                        <asp:label id="lblNo7" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: honeydew">
                        <asp:label id="lblEmpName7" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblMon7_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblMon7_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblTue7_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblTue7_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblWed7_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblWed7_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblThu7_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblThu7_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblFri7_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblFri7_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: honeydew;">
                        <asp:label id="lblSat7_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew; border-right: gray thin solid">
                        <asp:label id="lblSat7_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblSun7_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew">
                        <asp:label id="lblSun7_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: lavender;">
                        <asp:label id="lblNo8" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: lavender">
                        <asp:label id="lblEmpName8" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblMon8_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblMon8_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblTue8_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblTue8_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblWed8_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblWed8_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblThu8_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblThu8_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblFri8_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblFri8_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblSat8_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender; border-right: gray thin solid">
                        <asp:label id="lblSat8_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblSun8_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender">
                        <asp:label id="lblSun8_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: honeydew;">
                        <asp:label id="lblNo9" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: honeydew">
                        <asp:label id="lblEmpName9" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblMon9_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblMon9_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblTue9_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblTue9_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblWed9_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblWed9_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblThu9_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblThu9_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblFri9_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblFri9_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: honeydew;">
                        <asp:label id="lblSat9_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew; border-right: gray thin solid">
                        <asp:label id="lblSat9_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblSun9_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew">
                        <asp:label id="lblSun9_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: lavender;">
                        <asp:label id="lblNo10" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblEmpName10" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblMon10_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblMon10_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblTue10_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblTue10_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblWed10_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblWed10_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblThu10_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblThu10_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblFri10_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblFri10_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblSat10_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender; border-right: gray thin solid">
                        <asp:label id="lblSat10_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblSun10_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender">
                        <asp:label id="lblSun10_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: honeydew;">
                        <asp:label id="lblNo11" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: honeydew;">
                        <asp:label id="lblEmpName11" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                    
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblMon11_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblMon11_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblTue11_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblTue11_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblWed11_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblWed11_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblThu11_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblThu11_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblFri11_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblFri11_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: honeydew;">
                        <asp:label id="lblSat11_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew; border-right: gray thin solid">
                        <asp:label id="lblSat11_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     
                      <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblSun11_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblSun11_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: lavender;">
                        <asp:label id="lblNo12" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblEmpName12" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblMon12_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblMon12_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblTue12_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblTue12_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblWed12_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblWed12_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblThu12_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblThu12_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblFri12_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblFri12_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblSat12_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender; border-right: gray thin solid">
                        <asp:label id="lblSat12_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblSun12_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender">
                        <asp:label id="lblSun12_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: honeydew;">
                        <asp:label id="lblNo13" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: honeydew;">
                        <asp:label id="lblEmpName13" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblMon13_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblMon13_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblTue13_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblTue13_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblWed13_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblWed13_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblThu13_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblThu13_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblFri13_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblFri13_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: honeydew;">
                        <asp:label id="lblSat13_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew; border-right: gray thin solid">
                        <asp:label id="lblSat13_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblSun13_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew">
                        <asp:label id="lblSun13_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: lavender;">
                        <asp:label id="lblNo14" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblEmpName14" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblMon14_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblMon14_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblTue14_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblTue14_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblWed14_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblWed14_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblThu14_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblThu14_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblFri14_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblFri14_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblSat14_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender; border-right: gray thin solid">
                        <asp:label id="lblSat14_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblSun14_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender">
                        <asp:label id="lblSun14_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: honeydew;">
                        <asp:label id="lblNo15" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: honeydew;">
                        <asp:label id="lblEmpName15" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblMon15_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblMon15_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblTue15_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblTue15_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblWed15_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblWed15_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblThu15_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblThu15_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblFri15_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblFri15_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: honeydew;">
                        <asp:label id="lblSat15_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew; border-right: gray thin solid">
                        <asp:label id="lblSat15_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblSun15_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew">
                        <asp:label id="lblSun15_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: lavender;">
                        <asp:label id="lblNo16" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblEmpName16" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblMon16_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblMon16_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblTue16_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblTue16_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblWed16_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblWed16_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblThu16_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblThu16_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblFri16_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblFri16_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblSat16_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender; border-right: gray thin solid">
                        <asp:label id="lblSat16_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblSun16_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender">
                        <asp:label id="lblSun16_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: honeydew;">
                        <asp:label id="lblNo17" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: honeydew;">
                        <asp:label id="lblEmpName17" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblMon17_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblMon17_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblTue17_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblTue17_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblWed17_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblWed17_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblThu17_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblThu17_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblFri17_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblFri17_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: honeydew;">
                        <asp:label id="lblSat17_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew; border-right: gray thin solid">
                        <asp:label id="lblSat17_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblSun17_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew">
                        <asp:label id="lblSun17_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: lavender;">
                        <asp:label id="lblNo18" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblEmpName18" runat="server" height="18px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblMon18_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblMon18_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblTue18_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblTue18_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblWed18_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblWed18_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblThu18_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblThu18_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblFri18_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblFri18_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblSat18_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender; border-right: gray thin solid">
                        <asp:label id="lblSat18_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblSun18_Plan" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender">
                        <asp:label id="lblSun18_Apply" runat="server" height="18px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: honeydew;">
                        <asp:label id="lblNo19" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: honeydew;">
                        <asp:label id="lblEmpName19" runat="server" height="19px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblMon19_Plan" runat="server" height="19px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblMon19_Apply" runat="server" height="19px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblTue19_Plan" runat="server" height="19px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblTue19_Apply" runat="server" height="19px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblWed19_Plan" runat="server" height="19px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblWed19_Apply" runat="server" height="19px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblThu19_Plan" runat="server" height="19px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblThu19_Apply" runat="server" height="19px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblFri19_Plan" runat="server" height="19px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;border-right: gray thin solid;">
                        <asp:label id="lblFri19_Apply" runat="server" height="19px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: honeydew;">
                        <asp:label id="lblSat19_Plan" runat="server" height="19px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew; border-right: gray thin solid">
                        <asp:label id="lblSat19_Apply" runat="server" height="19px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew;">
                        <asp:label id="lblSun19_Plan" runat="server" height="19px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: honeydew">
                        <asp:label id="lblSun19_Apply" runat="server" height="19px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
                  <tr style="font-size: small">
                      <td align="center" style="width: 25px; background-color: lavender;">
                        <asp:label id="lblNo20" runat="server" height="18px" text="" width="25px"></asp:label>
                     </td>
                     <td align="left" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblEmpName20" runat="server" height="20px" text="" width="250px"></asp:label>
                     </td>
                     
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblMon20_Plan" runat="server" height="20px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblMon20_Apply" runat="server" height="20px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblTue20_Plan" runat="server" height="20px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblTue20_Apply" runat="server" height="20px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblWed20_Plan" runat="server" height="20px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblWed20_Apply" runat="server" height="20px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblThu20_Plan" runat="server" height="20px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblThu20_Apply" runat="server" height="20px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblFri20_Plan" runat="server" height="20px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;border-right: gray thin solid;">
                        <asp:label id="lblFri20_Apply" runat="server" height="20px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 250px; background-color: lavender;">
                        <asp:label id="lblSat20_Plan" runat="server" height="20px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender; border-right: gray thin solid">
                        <asp:label id="lblSat20_Apply" runat="server" height="20px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender;">
                        <asp:label id="lblSun20_Plan" runat="server" height="20px" text="" width="68px"></asp:label>
                     </td>
                     <td align="center" style="width: 60px; background-color: lavender">
                        <asp:label id="lblSun20_Apply" runat="server" height="20px" text="" width="68px"></asp:label>
                     </td>
                  </tr>
               </table>
               <asp:HiddenField ID="hfWeek" runat="server" Value="1"/>
               <asp:HiddenField ID="hfPage" runat="server" Value="0"/>
               <asp:HiddenField ID="hfMaxNo" runat="server" Value="0"/>
            <%--</td>
         </tr>
      </table>--%>
   </form>
</center>
</body>
</html>