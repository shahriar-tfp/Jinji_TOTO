<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UsrEmpInfo.aspx.vb" Inherits="Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>JINJI : Top Page</title>
    <link href="App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="UsrEmpInfo" runat="server">
    <div>
	    <table cellspacing="0" cellpadding="0" width="100%" border="0">
	        <tr>
	            <td>
	                <asp:Image ID="imgPreview" runat="server" Width="60px" Height="60px" Visible="false" />
	            </td>
	            <td align="left">
	               <div style="border-top-style: double; border-right-style: double; border-left-style: double; border-bottom-style: double;border-color :#F2F4FF;">
	                   <table cellspacing="0" cellpadding="0" width="100%" border="0">
	                      <tr>
	                        <td style="width:5px"></td>
	                        <td style="width:150px"><asp:Label ID="lblName" runat="server"></asp:Label></td>
	                        <td style="width:150px"><asp:Label ID="lblCode" runat="server"></asp:Label></td>
	                      </tr>
	                      <asp:Panel ID = "pnlEmpInfo" Visible="false" runat="server">
	                          <tr>
	                            <td style="width:5px"></td>
	                            <td style="width:150px"><asp:Label ID="lblDepartment" runat="server"></asp:Label></td>
	                            <td style="width:150px"><asp:Label ID="lblJobGrade" runat="server"></asp:Label></td>
	                          </tr>
	                          <tr>
	                            <td></td>
	                            <td><asp:Label ID="lblJobTitle" runat="server"></asp:Label></td>
	                            <td><asp:Label ID="lblSupervisor" runat="server"></asp:Label></td>
	                          </tr>
	                          <tr>
	                            <td></td>
	                            <td><asp:Label ID="lblLeaveApprovalLevel1" runat="server"></asp:Label></td>
	                            <td><asp:Label ID="lblLeaveApprovalUser1" runat="server"></asp:Label></td>
	                          </tr>
	                          <tr>
	                            <td></td>
	                            <td><asp:Label ID="lblLeaveApprovalLevel2" runat="server"></asp:Label></td>
	                            <td><asp:Label ID="lblLeaveApprovalUser2" runat="server"></asp:Label></td>
	                          </tr>
	                      </asp:Panel> 
	                   </table> 
	               </div>
	            </td>
	        </tr>
	    </table>
			   
    </div>
    </form>
</body>
</html>
