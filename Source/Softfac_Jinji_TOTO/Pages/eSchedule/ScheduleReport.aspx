<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScheduleReport.aspx.vb" Inherits="PAGES_ESCHEDULE_SCHEDULEREPORT" %>

<%@ Register Assembly="GrapeCity.ActiveReports.Web.v8, Version=8.0.133.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff"
    Namespace="GrapeCity.ActiveReports.Web" TagPrefix="ActiveReportsWeb" %>
       
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>eSchedule - Schedule Report</title>

</head>
<body  bgcolor="beige">
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
    
    <table cellspacing="0">
    <tr>
    <td align="left" colspan="2" style="height: 46px">
    <asp:RadioButton ID="optPersonal" Text="Personal" runat="server" GroupName="Type" AutoPostBack="True" Checked="True" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:RadioButton ID="optSubordinate" Text="Subordinate" runat="server" GroupName="Type" AutoPostBack="True" />&nbsp;
    <asp:DropDownList ID="cboSubordinate" runat="server" Width="375px" Enabled="False" AutoPostBack="True" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:Button ID="btnReport" text="Retrieve" runat="server" /></td>
    </tr>
    <tr>
    <td></td>
    </tr>
    <tr>
    <td>
    <asp:radiobutton ID="optDetail" Text="Details" runat="server" GroupName="Report" AutoPostBack="true" Checked="True" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFrom" runat="server" Text="Date Period"/>&nbsp;
    <asp:TextBox id="txtFromDate" MaxLength="10"  runat="server" Width="95px" />&nbsp;
    <asp:Label ID="lblTo" runat="server" Text=" ~"/>&nbsp;
    <asp:TextBox id="txtToDate"  MaxLength="10"   runat="server" Width="95px" />
       &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblSchedule" runat="server" Text="Schedule"/>&nbsp;
    <asp:DropDownList ID="cboSchedule" runat="server" Width="240px" AutoPostBack="True" /></td>
    </tr>
    <tr>
    <td>
    <asp:radiobutton ID="optSummary" Text="Summary"  runat="server"  groupname="Report" AutoPostBack="true"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="Date As At"/>&nbsp;&nbsp;
       <asp:TextBox id="txtDate"  MaxLength="10" runat="server" Enabled="False" Width="95px" />
       &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <asp:Label ID="lblType" runat="server" Text="Type"/>&nbsp;
    <asp:DropDownList ID="ddlType" runat="server" Width="240px" AutoPostBack="True" >
    <asp:ListItem Value="P">Plan</asp:ListItem>
          <asp:ListItem Value="A">Apply</asp:ListItem>
          </asp:DropDownList>
       <asp:Label ID="lblError" Font-Bold="True" ForeColor="Brown" runat="server" />
    </td>
    </tr>
    </table>
    <br />
       <ActiveReportsWeb:WebViewer ID="WebViewer1" runat="server" ViewerType="FlashViewer" Height="700px" Width="100%">
        </ActiveReportsWeb:WebViewer>
    </div>
    </form>
</body>
</html>
