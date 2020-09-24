<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScheduleApproval.aspx.vb" Inherits="PAGES_ESCHEDULE_SCHEDULEAPPROVAL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>eSchedule - Schedule Approval</title>
    <link href="images/favicon.ico" rel="shortcut icon" />
</head>
<body bgcolor="beige">
<center>
    <form id="form1" runat="server">
    <div>
    <table>
    <tr>
    <td align="left">
    <asp:Label ID="lblLeaveOvertime" runat="server" Text="Schedule: "/>
    <asp:LinkButton ID="lnkbtnLeaveApplication" Text="Plan & Application" runat="server"/>
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
    <td valign="top" colspan="2" style="width:1250px; height:1px"><hr id="Hr1" runat="server" /></td>
    </tr>
    </table>
    
    <table width="1230px" cellspacing="0">
    <tr>
    <td align="left" style="width:380px">
    <asp:RadioButton ID="optPersonal" Text="Personal" runat="server" GroupName="Type" AutoPostBack="True" />
    </td>
    <td />
    <td />

    </tr>
    <tr>
    <td align="left">
    <asp:RadioButton ID="optSubordinate" Text="Subordinate" runat="server" GroupName="Type" AutoPostBack="True" Checked="True" />
    <asp:DropDownList ID="cboSubordinate" runat="server" AutoPostBack="True" Width="255px" >
    </asp:DropDownList>
    </td>
    <td align="left">
    <asp:Label ID="lblLeaveStatus" runat="server" Text="Status"/>&nbsp;
    <asp:DropDownList ID="cboStatus" runat="server" AutoPostBack="True" Width="100px" />&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblMonth" runat="server" Text="Month"/>&nbsp;
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
    </asp:DropDownList>&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblYear" runat="server" Text="Year"/>&nbsp;
    <asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" Width="100px" />
    </td>
    
    <td align="right">
    <asp:Label ID="lblRecords" Text="Records" runat="server"/>&nbsp;
    <asp:LinkButton ID="lnkbtnPrevious" Text="<" runat="server" Font-Bold="True" Font-Size="Large" ToolTip="Backward"/>
    <asp:Label ID="lblSep2" Text=" | " runat="server" Font-Bold="True"/>
    <asp:LinkButton ID="lnkbtnNext" Text=">" runat="server" Font-Bold="True" Font-Size="Large" ToolTip="Forward"/>
    </td>
    </tr>
    </table>
    
    
 <%--    
    <table width="1130px">
    <tr>
   
    </tr>
    </table>
    
      
    <table  width="1130px">
    <tr align="right" >
   
    </tr>
    </table>--%>
    <br />
   
    <table border="1" cellspacing="0" cellpadding="5">
    
    <tr align="center">
    <td style="background-color: darkcyan"><asp:Label ID="lblNo" runat="server" Text="No." Width="25px" Font-Bold="True" ForeColor="White"/></td>
    
    <td style="background-color: darkcyan"><asp:DropDownList ID="lblStatus" runat="server" Width="80px" AutoPostBack="True" /></td>
    <td style="background-color: darkcyan"><asp:Label ID="lblName" runat="server" Text="Employee" Width="360px" Font-Bold="True" ForeColor="White"/></td>
    <td style="background-color: darkcyan"><asp:Label ID="lblDate" runat="server" Text="Apply For" Width="160px" Font-Bold="True" ForeColor="White"/></td>
    <td style="background-color: darkcyan; width: 203px;"><asp:Label ID="lblPeriod" runat="server" Text="Period" Width="60px" Font-Bold="True" ForeColor="White"/></td>
    <td style="background-color: darkcyan"><asp:Label ID="lblLeave" runat="server" Text="Schedule" Width="220px" Font-Bold="True" ForeColor="White"/></td>
    <td style="background-color: darkcyan"><asp:Label ID="lblApplied" runat="server" Text="Destination" Width="250px" Font-Bold="True" ForeColor="White"/></td>
    </tr>
    
    <tr align="center">
    <td style="background-color: honeydew"><asp:Label ID="lblNo1" runat="server" Text=""/></td>
    <td style="background-color: honeydew"><asp:CheckBox ID="ChkBox1" runat="server" /></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblName1" runat="server"/></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblDate1" runat="server"/></td>
    <td style="background-color: honeydew; width: 203px;"><asp:Label ID="lblPeriod1" runat="server"/></td>
    <td style="background-color: honeydew"><asp:Label ID="lblLeave1" runat="server"/></td>
    <td align="center" style="background-color: honeydew"><asp:Label ID="lblApplied1" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: lavender"><asp:Label ID="lblNo2" runat="server" Text=""/></td>
    <td style="background-color: lavender"><asp:CheckBox ID="ChkBox2" runat="server" /></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblName2" runat="server"/></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblDate2" runat="server"/></td>
    <td style="background-color: lavender; width: 203px;"><asp:Label ID="lblPeriod2" runat="server"/></td>
    <td style="background-color: lavender"><asp:Label ID="lblLeave2" runat="server"/></td>
    <td align="center" style="background-color: lavender"><asp:Label ID="lblApplied2" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: honeydew"><asp:Label ID="lblNo3" runat="server" Text=""/></td>
    <td style="background-color: honeydew"><asp:CheckBox ID="ChkBox3" runat="server" /></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblName3" runat="server"/></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblDate3" runat="server"/></td>
    <td style="background-color: honeydew; width: 203px;"><asp:Label ID="lblPeriod3" runat="server"/></td>
    <td style="background-color: honeydew"><asp:Label ID="lblLeave3" runat="server"/></td>
    <td align="center" style="background-color: honeydew"><asp:Label ID="lblApplied3" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: lavender"><asp:Label ID="lblNo4" runat="server" Text=""/></td>
    <td style="background-color: lavender"><asp:CheckBox ID="ChkBox4" runat="server" /></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblName4" runat="server"/></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblDate4" runat="server"/></td>
    <td style="background-color: lavender; width: 203px;"><asp:Label ID="lblPeriod4" runat="server"/></td>
    <td style="background-color: lavender"><asp:Label ID="lblLeave4" runat="server"/></td>
    <td align="center" style="background-color: lavender"><asp:Label ID="lblApplied4" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: honeydew"><asp:Label ID="lblNo5" runat="server" Text=""/></td>
    <td style="background-color: honeydew"><asp:CheckBox ID="ChkBox5" runat="server" /></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblName5" runat="server"/></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblDate5" runat="server"/></td>
    <td style="background-color: honeydew; width: 203px;"><asp:Label ID="lblPeriod5" runat="server"/></td>
    <td style="background-color: honeydew"><asp:Label ID="lblLeave5" runat="server"/></td>
    <td align="center" style="background-color: honeydew"><asp:Label ID="lblApplied5" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: lavender"><asp:Label ID="lblNo6" runat="server" Text=""/></td>
    <td style="background-color: lavender"><asp:CheckBox ID="ChkBox6" runat="server" /></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblName6" runat="server"/></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblDate6" runat="server"/></td>
    <td style="background-color: lavender; width: 203px;"><asp:Label ID="lblPeriod6" runat="server"/></td>
    <td style="background-color: lavender"><asp:Label ID="lblLeave6" runat="server"/></td>
    <td align="center" style="background-color: lavender"><asp:Label ID="lblApplied6" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: honeydew"><asp:Label ID="lblNo7" runat="server" Text=""/></td>
    <td style="background-color: honeydew"><asp:CheckBox ID="ChkBox7" runat="server" /></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblName7" runat="server"/></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblDate7" runat="server"/></td>
    <td style="background-color: honeydew; width: 203px;"><asp:Label ID="lblPeriod7" runat="server"/></td>
    <td style="background-color: honeydew"><asp:Label ID="lblLeave7" runat="server"/></td>
    <td align="center" style="background-color: honeydew"><asp:Label ID="lblApplied7" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: lavender"><asp:Label ID="lblNo8" runat="server" Text=""/></td>
    <td style="background-color: lavender"><asp:CheckBox ID="ChkBox8" runat="server" /></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblName8" runat="server"/></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblDate8" runat="server"/></td>
    <td style="background-color: lavender; width: 203px;"><asp:Label ID="lblPeriod8" runat="server"/></td>
    <td style="background-color: lavender"><asp:Label ID="lblLeave8" runat="server"/></td>
    <td align="center" style="background-color: lavender"><asp:Label ID="lblApplied8" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: honeydew"><asp:Label ID="lblNo9" runat="server" Text=""/></td>
    <td style="background-color: honeydew"><asp:CheckBox ID="ChkBox9" runat="server" /></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblName9" runat="server"/></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblDate9" runat="server"/></td>
    <td style="background-color: honeydew; width: 203px;"><asp:Label ID="lblPeriod9" runat="server"/></td>
    <td style="background-color: honeydew"><asp:Label ID="lblLeave9" runat="server"/></td>
    <td align="center" style="background-color: honeydew"><asp:Label ID="lblApplied9" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: lavender"><asp:Label ID="lblNo10" runat="server" Text=""/></td>
    <td style="background-color: lavender"><asp:CheckBox ID="ChkBox10" runat="server" /></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblName10" runat="server"/></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblDate10" runat="server"/></td>
    <td style="background-color: lavender; width: 203px;"><asp:Label ID="lblPeriod10" runat="server"/></td>
    <td style="background-color: lavender"><asp:Label ID="lblLeave10" runat="server"/></td>
    <td align="center" style="background-color: lavender"><asp:Label ID="lblApplied10" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: honeydew"><asp:Label ID="lblNo11" runat="server" Text=""/></td>
    <td style="background-color: honeydew"><asp:CheckBox ID="ChkBox11" runat="server" /></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblName11" runat="server"/></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblDate11" runat="server"/></td>
    <td style="background-color: honeydew; width: 203px;"><asp:Label ID="lblPeriod11" runat="server"/></td>
    <td style="background-color: honeydew"><asp:Label ID="lblLeave11" runat="server"/></td>
    <td align="center" style="background-color: honeydew"><asp:Label ID="lblApplied11" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: lavender"><asp:Label ID="lblNo12" runat="server" Text=""/></td>
    <td style="background-color: lavender"><asp:CheckBox ID="ChkBox12" runat="server" /></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblName12" runat="server"/></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblDate12" runat="server"/></td>
    <td style="background-color: lavender; width: 203px;"><asp:Label ID="lblPeriod12" runat="server"/></td>
    <td style="background-color: lavender"><asp:Label ID="lblLeave12" runat="server"/></td>
    <td align="center" style="background-color: lavender"><asp:Label ID="lblApplied12" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: honeydew"><asp:Label ID="lblNo13" runat="server" Text=""/></td>
    <td style="background-color: honeydew"><asp:CheckBox ID="ChkBox13" runat="server" /></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblName13" runat="server"/></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblDate13" runat="server"/></td>
    <td style="background-color: honeydew; width: 203px;"><asp:Label ID="lblPeriod13" runat="server"/></td>
    <td style="background-color: honeydew"><asp:Label ID="lblLeave13" runat="server"/></td>
    <td align="center" style="background-color: honeydew"><asp:Label ID="lblApplied13" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: lavender"><asp:Label ID="lblNo14" runat="server" Text=""/></td>
    <td style="background-color: lavender"><asp:CheckBox ID="ChkBox14" runat="server" /></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblName14" runat="server"/></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblDate14" runat="server"/></td>
    <td style="background-color: lavender; width: 203px;"><asp:Label ID="lblPeriod14" runat="server"/></td>
    <td style="background-color: lavender"><asp:Label ID="lblLeave14" runat="server"/></td>
    <td align="center" style="background-color: lavender"><asp:Label ID="lblApplied14" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: honeydew"><asp:Label ID="lblNo15" runat="server" Text=""/></td>
    <td style="background-color: honeydew"><asp:CheckBox ID="ChkBox15" runat="server" /></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblName15" runat="server"/></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblDate15" runat="server"/></td>
    <td style="background-color: honeydew; width: 203px;"><asp:Label ID="lblPeriod15" runat="server"/></td>
    <td style="background-color: honeydew"><asp:Label ID="lblLeave15" runat="server"/></td>
    <td align="center" style="background-color: honeydew"><asp:Label ID="lblApplied15" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: lavender"><asp:Label ID="lblNo16" runat="server" Text=""/></td>
    <td style="background-color: lavender"><asp:CheckBox ID="ChkBox16" runat="server" /></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblName16" runat="server"/></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblDate16" runat="server"/></td>
    <td style="background-color: lavender; width: 203px;"><asp:Label ID="lblPeriod16" runat="server"/></td>
    <td style="background-color: lavender"><asp:Label ID="lblLeave16" runat="server"/></td>
    <td align="center" style="background-color: lavender"><asp:Label ID="lblApplied16" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: honeydew"><asp:Label ID="lblNo17" runat="server" Text=""/></td>
    <td style="background-color: honeydew"><asp:CheckBox ID="ChkBox17" runat="server" /></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblName17" runat="server"/></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblDate17" runat="server"/></td>
    <td style="background-color: honeydew; width: 203px;"><asp:Label ID="lblPeriod17" runat="server"/></td>
    <td style="background-color: honeydew"><asp:Label ID="lblLeave17" runat="server"/></td>
    <td align="center" style="background-color: honeydew"><asp:Label ID="lblApplied17" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: lavender"><asp:Label ID="lblNo18" runat="server" Text=""/></td>
    <td style="background-color: lavender"><asp:CheckBox ID="ChkBox18" runat="server" /></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblName18" runat="server"/></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblDate18" runat="server"/></td>
    <td style="background-color: lavender; width: 203px;"><asp:Label ID="lblPeriod18" runat="server"/></td>
    <td style="background-color: lavender"><asp:Label ID="lblLeave18" runat="server"/></td>
    <td align="center" style="background-color: lavender"><asp:Label ID="lblApplied18" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: honeydew"><asp:Label ID="lblNo19" runat="server" Text=""/></td>
    <td style="background-color: honeydew"><asp:CheckBox ID="ChkBox19" runat="server" /></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblName19" runat="server"/></td>
    <td align="left" style="background-color: honeydew"><asp:Label ID="lblDate19" runat="server"/></td>
    <td style="background-color: honeydew; width: 203px;"><asp:Label ID="lblPeriod19" runat="server"/></td>
    <td style="background-color: honeydew"><asp:Label ID="lblLeave19" runat="server"/></td>
    <td align="center" style="background-color: honeydew"><asp:Label ID="lblApplied19" runat="server"/></td>
    </tr>
    
    <tr>
    <td style="background-color: lavender"><asp:Label ID="lblNo20" runat="server" Text=""/></td>
    <td style="background-color: lavender"><asp:CheckBox ID="ChkBox20" runat="server" /></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblName20" runat="server"/></td>
    <td align="left" style="background-color: lavender"><asp:Label ID="lblDate20" runat="server"/></td>
    <td style="background-color: lavender; width: 203px;"><asp:Label ID="lblPeriod20" runat="server"/></td>
    <td style="background-color: lavender"><asp:Label ID="lblLeave20" runat="server"/></td>
    <td align="center" style="background-color: lavender"><asp:Label ID="lblApplied20" runat="server"/></td>
    </tr>
    
    </table>
    <br />
    
    <table>
    <tr>
    <td><asp:Label ID="lblRemark" Text="Remark : " runat="server"/></td>
    <td>
    <asp:TextBox ID="txtRemark" runat="server" MaxLength="150" Width="1110px"/>
    <asp:Label ID="lblRequired" Text="*" ForeColor="Red" runat="server" Visible="False"/>
    </td>
    <td>
    <asp:Button ID="btnOK" Text="OK" runat="server" Width="50" OnClientClick="return confirm('Are you sure?');"/>
    </td>
    </tr>
    </table>
    
    <asp:HiddenField ID="hfPage" runat="server" Value="0"/>
    <asp:HiddenField ID="hfMaxNo" runat="server" Value="0"/>
    <asp:HiddenField ID="hfPageLast" runat="server" Value="0"/>
    </div>
    
    
       
    </form>
    
    </center>
</body>
</html>
