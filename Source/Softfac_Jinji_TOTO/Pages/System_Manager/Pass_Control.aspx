<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Pass_Control.aspx.vb" Inherits="Pages_System_Manager_Pass_Control" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Password Control Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server">
    <form id="Pass_Control" runat="server">
    <div>
    <asp:Panel id="pnlTitle" runat="server" visible="True">
    <asp:placeholder id="MyPH" Runat="server"></asp:placeholder>
    <table id="Table1" style="LEFT: 10px; POSITION:absolute; TOP: 10px" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
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
						            <td style="width:125px"><asp:label id="lblCompany_Profile_ID" runat="server" Width="120px"></asp:label></td>
						            <td style="width:155px"><asp:textbox id="txtCompany_Profile_ID" runat="server" Width="150px"></asp:textbox></td>  
						            <td style="width:5px"></td>
						            <td style="width:305px"><asp:textbox id="txtCompany_Name" runat="server" Width="300px"></asp:textbox></td> 
						            <td style="width:30px"><asp:imagebutton id="imgBtnCompany_Profile_ID" runat="server" Height="21px"></asp:imagebutton></td>
					            </tr>
					        </table> 
					        <table id="Table6" cellspacing="0" cellpadding="0" border="0" runat="server">
					            <tr>
					                <td colspan="6" style="height:10px"></td>
					            </tr>
					            <tr>
					                <td><asp:Image ID="imgBlank02" Width="5px" Height="25px" runat="server" /></td>
					                <td>
					                    <asp:LinkButton id="lnkBtnViewPassSetting" runat="server" CssClass="wordstyle" Text="[ Password Setting ]" Enabled="false" ></asp:LinkButton>
					                    <asp:LinkButton id="lnkBtnClosePassSetting" runat="server" CssClass="wordstyle11" Text="[ Password Setting ]" Visible="false" ></asp:LinkButton>
					                </td>
					                <td><asp:Image ID="imgBlank03" Width="5px" Height="25px" runat="server" /></td>
					                <td>
					                    <asp:LinkButton id="lnkBtnViewResetPass" runat="server" CssClass="wordstyle" Text="[ Reset Password ]" Enabled="false" ></asp:LinkButton>
					                    <asp:LinkButton id="lnkBtnCloseResetPass" runat="server" CssClass="wordstyle11" Text="[ Reset Password ]" Visible="false" ></asp:LinkButton>
					                </td>
					            </tr>
					        </table>
					    </td> 
					</tr> 
					<tr>
					    <td>
				            <table id="Table7" cellspacing="0" cellpadding="0" border="0" runat="server">
				                <tr>
				                    <td style="height:2px"></td>
				                </tr>
				            </table> 
				            <%--Panel Part 1--%>
					        <asp:Panel id="pnlpart1" runat="server" visible="False">
					            <table id="Table8" cellspacing="0" cellpadding="0" border="0" runat="server">
				                   <tr>
				                       <td style="height:5px"></td>
				                   </tr>
				                </table>
				                <asp:Panel id="pnlpart2" runat="server" visible="False">
				                    
			                        <table id="Table16" cellspacing="0" cellpadding="0" border="0" runat="server">
	                                   <tr>
	                                       <td style="width:5px">&nbsp;</td>
	                                       <td><asp:Label id="lblPass_Length" Width="120px" CssClass="wordstyle" runat="server"></asp:Label></td>
	                                       <td style="width:5px">&nbsp;</td>
	                                       <td><asp:Textbox id="txtPass_Length" Width="150px" runat="server"></asp:Textbox></td>
	                                       <td style="width:10px">&nbsp;</td>
	                                       <td><asp:Label id="lblPass_Max_Length" Width="150px" CssClass="wordstyle" runat="server"></asp:Label></td>
	                                       <td style="width:5px">&nbsp;</td>
	                                       <td><asp:Textbox id="txtPass_Max_Length" Width="150px" runat="server"></asp:Textbox></td>
	                                   </tr>
	                                   <tr>
	                                       <td style="width:5px">&nbsp;</td>
	                                       <td><asp:Label id="lblPass_Age" Width="120px" CssClass="wordstyle" runat="server"></asp:Label></td>
	                                       <td style="width:5px">&nbsp;</td>
	                                       <td><asp:Textbox id="txtPass_Age" Width="150px" runat="server"></asp:Textbox></td>
	                                       <td style="width:10px">&nbsp;</td>
	                                       <td><asp:Label id="lblPass_Complexity" Width="120px" CssClass="wordstyle" runat="server"></asp:Label></td>
	                                       <td style="width:5px">&nbsp;</td>
	                                       <td><asp:Dropdownlist id="ddlPass_Complexity" Width="157px" runat="server"></asp:Dropdownlist></td>
	                                   </tr> 
	                                   <tr>
	                                       <td style="width:10px">&nbsp;</td>
	                                       <td><asp:Label id="lblExpired_Alert" Width="150px" CssClass="wordstyle" runat="server"></asp:Label></td>
	                                       <td style="width:5px">&nbsp;</td>
	                                       <td><asp:Textbox id="txtExpired_Alert" Width="150px" runat="server"></asp:Textbox></td>
	                                       <td style="width:10px">&nbsp;</td>
	                                       <td><asp:Label id="lblPass_Level" Width="120px" CssClass="wordstyle" runat="server"></asp:Label></td>
	                                       <td style="width:5px">&nbsp;</td>
	                                       <td><asp:Textbox id="txtPass_Level" Width="150px" runat="server"></asp:Textbox></td>
	                                   </tr>
	                                   <tr>
				                           <td style="width:5px">&nbsp;</td>
	                                       <td><asp:Label id="lblDefault_Pass" Width="150px" CssClass="wordstyle" runat="server"></asp:Label></td>
	                                       <td style="width:5px">&nbsp;</td>
	                                       <td><asp:Textbox id="txtDefault_Pass" Width="150px" runat="server"></asp:Textbox></td>
	                                       <td colspan="4">&nbsp;</td>
				                       </tr>
		                           </table>
		                       </asp:Panel>      
			                   <asp:Panel id="pnlpart3" runat="server" visible="False">
		                           <table id="Table17" cellspacing="0" cellpadding="0" border="0" runat="server">
			                            <tr>
			                                <td style="width:5px">&nbsp;</td>
		                                    <td>
		                                        <%--<div style="border-top-style: double; border-right-style: double; border-left-style: double; border-bottom-style: double;border-color :#F2F4FF;">--%>
		                                            <table id="Table9" cellspacing="0" cellpadding="0" border="0" runat="server">
		                                                <tr>
		                                                    <td align="Left"><asp:CheckBox ID="CheckBoxDefault" Checked="false" AutoPostBack="true" Width="150px" BorderStyle="None" CssClass="wordstyle" Runat="server" /></td>
		                                                    <td align="Left"><asp:CheckBox ID="CheckBoxManual" Checked="false" AutoPostBack="true" Width="150px" BorderStyle="None" CssClass="wordstyle" Runat="server" /></td>
		                                                </tr>
		                                            </table>
		                                        <%--</div>--%>
		                                    </td>
		                                </tr>
		                                <tr>
		                                    <td style="width:5px">&nbsp;</td>
		                                    <td>
		                                        <asp:Panel id="pnlAuto" runat="server" visible="False">
		                                            <table id="Table14" cellspacing="0" cellpadding="0" border="0" runat="server">
	                                                    <tr>
	                                                        <td style="width:5px">&nbsp;</td>
	                                                        <td style="width:5px">&nbsp;</td>
	                                                    </tr>
	                                                </table>  
		                                            <%--<div style="border-top-style: double; border-right-style: double; border-left-style: double; border-bottom-style: double;border-color :#F2F4FF;">--%>
		                                                <table id="Table10" cellspacing="0" cellpadding="0" border="0" runat="server">
	                                                        <tr>
	                                                            <td><asp:label id="lbllstleft" runat="server" Width="270px"></asp:label></td>
	                                                            <td style="width:50px">&nbsp;</td>
	                                                            <td><asp:label id="lbllstright" runat="server" Width="270px"></asp:label></td>
	                                                        </tr>
	                                                        <tr>
	                                                            <td>
	                                                                <div style="border-top-style: none; border-right-style:groove; border-left-style: groove; border-bottom-style: none;">
                                                                        <asp:panel id="PnlLeftList" ScrollBars="Horizontal" Width="270px" runat="server" visible="true">
	                                                                        <asp:ListBox id="lstleft" Width="270px" Height="200px" SelectionMode="Multiple" CssClass="wordstyle" runat="server" ></asp:ListBox>
	                                                                    </asp:panel> 
	                                                                </div> 
	                                                            </td>
	                                                            <td>
	                                                                <table id="Table12" cellspacing="0" cellpadding="0" border="0" runat="server">
	                                                                    <tr>
	                                                                        <td style="width:50px; height:10px">&nbsp;</td>
	                                                                    </tr>
	                                                                    <tr>
	                                                                        <td align="center"><asp:imagebutton id="imgBtnAddAll" runat="server" Width="24" Height="20"></asp:imagebutton></td>
	                                                                    </tr>
	                                                                    <tr><td style="height:5px"></td></tr>
	                                                                    <tr>
	                                                                        <td align="center"><asp:imagebutton id="imgBtnAddItem" runat="server" Width="24" Height="20"></asp:imagebutton></td>
	                                                                    </tr>
	                                                                    <tr><td style="height:5px"></td></tr>
	                                                                    <tr>
	                                                                        <td align="center"><asp:imagebutton id="imgBtnRemoveItem" runat="server" Width="24" Height="20"></asp:imagebutton></td>
	                                                                    </tr>
	                                                                    <tr><td style="height:5px"></td></tr>
	                                                                    <tr>
	                                                                        <td align="center"><asp:imagebutton id="imgBtnRemoveAll" runat="server" Width="24" Height="20"></asp:imagebutton></td>
	                                                                    </tr>
	                                                                    <tr>
	                                                                        <td style="width:50px; height:50px">&nbsp;</td>
	                                                                    </tr>
	                                                                </table>
	                                                            </td>
	                                                            <td>
	                                                                <div style="border-top-style: none; border-right-style:groove; border-left-style: groove; border-bottom-style: none;">
                                                                        <asp:panel id="PnlRightList" ScrollBars="Horizontal" Width="270px" runat="server" visible="true">
                                                                            <asp:ListBox id="lstright" Width="270px" Height="200px" SelectionMode="Multiple" CssClass="wordstyle" runat="server" ></asp:ListBox>
                                                                        </asp:panel>     
                                                                    </div>        
                                                                </td>
	                                                        </tr> 
	                                                        <tr>
                                                                <td style="height:2px">&nbsp;</td>
                                                            </tr>
	                                                    </table>
	                                                    <asp:Panel id="pnlManual" runat="server" visible="False">
	                                                        <table id="Table11" cellspacing="0" cellpadding="0" border="0" runat="server">    
	                                                            <tr>
	                                                                <td><asp:Label id="lblPassword" Width="115px" CssClass="wordstyle" runat="server"></asp:Label></td>
	                                                                <td style="width:5px"></td>
	                                                                <td><asp:Textbox id="txtPassword" Width="150px" TextMode="Password" runat="server"></asp:Textbox></td>
	                                                                <td style="width:50px;">&nbsp;</td>
	                                                                <td><asp:Label id="lblChange_Pass" Width="120px" CssClass="wordstyle" runat="server"></asp:Label></td>
	                                                                <td style="width:5px"></td>
	                                                                <td><asp:Dropdownlist id="ddlChange_Pass" Width="157px" CssClass="wordstyle" runat="server"></asp:Dropdownlist></td>
	                                                            </tr>
	                                                        </table>   
	                                                    </asp:Panel>
		                                            <%--</div>--%> 
		                                        </asp:Panel> 
		                                    </td>
		                                </tr>
		                            </table>
		                       </asp:Panel>
		                   </asp:Panel>
					    </td>
					</tr>
					<tr>
					    <td>
					        <table cellspacing="0" cellpadding="0" border="0">
	                            <tr>
	                                <td style="width:10px">&nbsp;</td>
                                </tr>
	                            <tr>
	                                <%--pnlbutton--%>
		                            <asp:Panel ID="pnlbutton" runat="server" Visible="true">
                                    <td style="width:2px">&nbsp;</td>
	                                <td align="left">
                                        <asp:imagebutton id="imgBtnSubmit" runat="server" Width="74" Height="21" OnClientClick="return confirm('Confirmation! Submit Data ?')"></asp:imagebutton>
                                        <asp:imagebutton id="imgBtnClear" runat="server" Width="74" Height="21"></asp:imagebutton>
		                            </td>
		                            </asp:Panel> 	
	                                <td>
                                    <%--pnlresult--%>
			                        <asp:Panel ID="pnlresult" runat="server" Visible="true">
			                            <table cellspacing="0" cellpadding="0" border="0">
					                        <tr>
                                                <td><asp:Image ID="Image1" ImageUrl="../../images/company/default/gif/px1.gif" Width="5px" runat="server" /></td>
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
