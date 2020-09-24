<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Process_Carry_Forward.aspx.vb" Inherits="Pages_Leave_Process_Carry_Forward" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>JINJI : Leave - Process Carry Forward Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server">
    <form id="Process_Carry_Forward" runat="server">
    <div>
    <asp:Panel id="pnlTitle" runat="server" visible="True">
    <asp:placeholder id="MyPH" Runat="server"></asp:placeholder>
    <table id="Table1" style="LEFT: 10px; POSITION:absolute; TOP: 10px" cellspacing="0" cellpadding="0" border="0" runat="server">
		<tr>
			<td>
			    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                    <tr>
                        <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif);width :5px"></td>
                        <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); vertical-align:bottom"><asp:Label ID="lblTitle" runat="server"></asp:Label></td>
                    </tr>
                </table>
				<table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
					<tr>
					    <td>
					        <table id="Table4" cellspacing="0" cellpadding="0" border="0" runat="server">
					            <tr>
					                <td style ="height:15px"></td>
					            </tr>
					        </table> 
					        <table id="Table5" cellspacing="0" cellpadding="0" border="0" runat="server">
					            <tr>
					                <td style="width:25px"><asp:image id="imgCompany_Profile_ID" runat="server" Height="21px" Width="21px"></asp:image></td>
						            <td style="width:125px"><asp:label id="lblCompany_Profile_ID" runat="server" Width="140px"></asp:label></td>
						            <td style="width:155px"><asp:Textbox id="txtCompany_Profile_ID" runat="server" Width="150px"></asp:Textbox></td>  
						            <td style="width:5px"></td>
						            <td style="width:305px"><asp:Textbox id="txtCompany_Name" runat="server" Width="300px"></asp:Textbox></td> 
						            <td style="width:30px"><asp:Imagebutton id="imgBtnCompany_Profile_ID" runat="server" Height="21px"></asp:Imagebutton></td>
					            </tr>
					            <tr>
					                <td><asp:image id="imgOption_Leave_Type" runat="server" Height="21px" Width="21px"></asp:image></td>
					                <td><asp:label id="lblOption_Leave_Type" runat="server" Width="140px"></asp:label></td>
					                <td colspan="4"><asp:dropdownlist id="ddlOption_Leave_Type" AutoPostBack="true" runat="server" Width="300"></asp:dropdownlist></td>
					            </tr>
					            <tr>
					                <td><asp:image id="imgDate" runat="server" Height="21px" Width="21px"></asp:image></td>
					                <td><asp:label id="lblDate" runat="server" Width="140px"></asp:label></td>
					                <td><asp:Textbox id="txtDate" runat="server" Width="150px"></asp:Textbox></td>
					            </tr>
					            <tr>
					                <td><asp:image id="imgExpiry_Date" runat="server" Height="21px" Width="21px"></asp:image></td>
					                <td><asp:label id="lblExpiry_Date" runat="server" Width="140px"></asp:label></td>
					                <td><asp:Textbox id="txtExpiry_Date" runat="server" Width="150px"></asp:Textbox></td>
					                <td colspan="3"><asp:Imagebutton id="imgBtnExpiry_Date" runat="server" Height="21px"></asp:Imagebutton></td>
					            </tr>
					        </table>
					    </td> 
					</tr> 
					<tr>
					    <td style="height:15px"></td>    
                    </tr>
                    <tr>    
                        <td>    
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
	                                <%--pnlbutton--%>
		                            <asp:Panel ID="pnlbutton" runat="server" Visible="true">
	                                <td align="left">
                                        <asp:Imagebutton id="imgBtnUpdate" runat="server" Width="74" Height="21" OnClientClick="return confirm('Confirmation! Revise Data ?')"></asp:Imagebutton>
                                        <asp:Imagebutton id="imgBtnClear" runat="server" Width="74" Height="21"></asp:Imagebutton>
		                            </td>
		                            </asp:Panel> 	
	                                <td>
                                    <%--pnlresult--%>
			                        <asp:Panel ID="pnlresult" runat="server" Visible="true">
			                            <table cellspacing="0" cellpadding="0" border="0">
					                        <tr>
                                                <td><asp:Image ID="Image1" ImageUrl="../../../images/company/default/gif/px1.gif" Width="5px" runat="server" /></td>
                                                <td align="left"><asp:Label id="lblresult" runat="server" CssClass="wordstyle2"></asp:Label></td>
                                            </tr>
				                        </table> 
			                        </asp:Panel>
	                                </td>
	                            </tr>
	                        </table>
					    </td>
					</tr>
				</table> 
			</td> 
		</tr>
	</table></asp:Panel> 
    </div>
    </form>
</body>
</html>
