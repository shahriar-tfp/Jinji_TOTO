<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AppraisalReport.aspx.vb" Inherits="PAGES_EAPPRAISAL_APPRAISALREPORT" %>

<%@ Register Assembly="GrapeCity.ActiveReports.Web.v8, Version=8.0.133.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff"
    Namespace="GrapeCity.ActiveReports.Web" TagPrefix="ActiveReportsWeb" %>
       
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>eAppraisal - Report</title>
</head>
<body bgcolor="beige">
    <form id="form1" runat="server">
    <div>
    <table>
       <tr>
       <td align="left">
       <asp:Label ID="lblTarget" runat="server" Text="Appraisal: "/>
       <asp:LinkButton ID="lnkbtnTarget" Text="Target & Achievement" runat="server"/>
       <asp:Label ID="lblPageSep1" Text="|" runat="server"/>
       <asp:LinkButton ID="lnkbtnSkills" Text="Skills"   runat="server"/>
       <asp:Label ID="lblPageSep4" Text="|" runat="server"/>
          <asp:LinkButton ID="lnkbtnComment" Text="Comments"   runat="server"/>
       <asp:Label ID="lblPageSep2" Text="|" runat="server"/>
       <asp:LinkButton ID="lnkReport" Text="Report"  ForeColor="Red" runat="server"/>
       <asp:Label ID="lblPageSep3" Text="|" runat="server"/>
       <asp:LinkButton ID="lnkManual" Text="Help" runat="server" />
       </td>
       <td align="right">
       <asp:Label ID="lblUser" runat="server"/> | 
       </td>
       </tr>
       <tr>
       <td valign="top" colspan="2" style="width:1090px; height:1px"><hr id="Hr1" runat="server" /></td>
       </tr>
    </table>
    
    
    
         <table cellspacing="0" cellpadding="3">
         <tr>
         <td align="left"><asp:RadioButton ID="optTarget" Text="Target" GroupName="Report"  runat="server" AutoPostBack="True" Checked="True" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:RadioButton ID="optSkill" Text="Skill" GroupName="Report"  runat="server" AutoPostBack="True" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:RadioButton ID="optComment" Text="Comments" GroupName="Report"  runat="server" AutoPostBack="True" /></td>
         </tr>
         <tr>
         <td align="left">
         <asp:RadioButton ID="optPersonal" Text="Personal" GroupName="Type"  runat="server" AutoPostBack="True" Checked="True" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:RadioButton ID="optSubordinate" Text="Subordinate" GroupName="Type"  runat="server" AutoPostBack="True" />&nbsp;
         <asp:DropDownList ID="cboSubordinate" runat="server" AutoPostBack="True" Width="400px" Enabled="False"></asp:DropDownList>
            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:Label ID="lblAppraisal" Text="Appraisal Period" runat="server"></asp:Label>&nbsp;
         <asp:DropDownList ID="cboAppraisal" runat="server" AutoPostBack="True" Width="220px"></asp:DropDownList>
         </td>
         </tr>
         </table><br />
       <ActiveReportsWeb:WebViewer ID="WebViewer1" runat="server" ViewerType="FlashViewer" Height="700px" Width="100%">
        </ActiveReportsWeb:WebViewer>
         
        
    
    </div>
    <asp:HiddenField ID="hfLevel" runat="server" />
    </form>
</body>
</html>
