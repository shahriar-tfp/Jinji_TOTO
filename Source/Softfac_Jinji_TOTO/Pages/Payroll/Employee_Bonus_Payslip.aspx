<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Employee_Bonus_Payslip.aspx.vb" Inherits="Pages_Payroll_Employee_Bonus_Payslip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>JINJI : Payroll - View Payslip Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server">
    <form id="EMPLOYEE_BONUS_PAYSLIP_VW" runat="server">
    <div>
        <table id="Table1"  cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
            <tr>
                <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); width :5px"></td>
                <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); vertical-align:bottom"><asp:Label ID="lblTitle" runat="server"></asp:Label></td>
            </tr>
        </table>
        <table id="Table9" cellspacing="0" cellpadding="0" border="0" runat="server">
            <tr>
                <td style ="height:15px"></td>
            </tr>
		</table>
        <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
            <tr>
                <td style="height: 33px">
                    <asp:Image ID="imgtop" Height="30px" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <%--<asp:imagebutton id="imgKeyCOMPANY_PROFILE_CODE" runat="server" Width="21px" Height="21px"></asp:imagebutton>
                    <asp:label id="lblCOMPANY_PROFILE_CODE" runat="server" Width="140px"></asp:label>
                    <asp:textbox id="txtCOMPANY_PROFILE_CODE" runat="server" Width="150px" ></asp:textbox>
                    <asp:imagebutton id="imgbtnCOMPANY_PROFILE_CODE" runat="server" Height="21px"></asp:imagebutton>--%>
                
                    <asp:imagebutton id="imgKeyEMPLOYEE_PROFILE_ID" runat="server" Width="21px" Height="21px"></asp:imagebutton>
                    <asp:label id="lblEMPLOYEE_PROFILE_ID" runat="server" Width="140px"></asp:label>
                    <asp:textbox id="txtEMPLOYEE_PROFILE_ID" runat="server" Width="150px"></asp:textbox>
                    <asp:imagebutton id="imgbtnEMPLOYEE_PROFILE_ID" runat="server" Height="21px"></asp:imagebutton>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:imagebutton id="imgKeyYEAR" runat="server" Width="21px" Height="21px"></asp:imagebutton>
                    <asp:label id="lblYEAR" runat="server" Width="140px"></asp:label>
    <%--            <asp:textbox id="txtYEAR" runat="server" Width="150px"></asp:textbox>--%>
                    <asp:dropdownlist id="ddlYEAR" runat="server" Width="155px" AutoPostBack="true"></asp:dropdownlist>
                    <asp:imagebutton id="imgbtnYEAR" runat="server" Height="21px"></asp:imagebutton>
                        
                    <asp:imagebutton id="imgKeyMONTH" runat="server" Width="21px" Height="21px"></asp:imagebutton>
                    <asp:label id="lblMONTH" runat="server" Width="140px"></asp:label>
                    <%--<asp:textbox id="txtMONTH" runat="server" Width="150px"></asp:textbox>--%>
                    <asp:dropdownlist id="ddlMONTH" runat="server" Width="155px" AutoPostBack="true"></asp:dropdownlist>
                    <asp:imagebutton id="imgbtnMONTH" runat="server" Height="21px"></asp:imagebutton>
                    
                    <asp:imagebutton id="imgKeyOPTION_PAY_CYCLE" runat="server" Width="21px" Height="21px"></asp:imagebutton>
                    <asp:label id="lblOPTION_PAY_CYCLE" runat="server" Width="140px"></asp:label>
                    <asp:dropdownlist id="ddlOPTION_PAY_CYCLE" runat="server" Width="155px" AutoPostBack="true"></asp:dropdownlist>
                    <asp:imagebutton id="imgbtnOPTION_PAY_CYCLE" runat="server" Height="21px"></asp:imagebutton>
                </td>
            </tr>
            <%--
            <tr>
                <td>
                    <asp:imagebutton id="imgKeyOPTION_PAY_CYCLE" runat="server" Width="21px" Height="21px"></asp:imagebutton>
                    <asp:label id="lblOPTION_PAY_CYCLE" runat="server" Width="140px"></asp:label>
                    <asp:dropdownlist id="ddlOPTION_PAY_CYCLE" runat="server" Width="155px" AutoPostBack="true"></asp:dropdownlist>
                    <asp:imagebutton id="imgbtnOPTION_PAY_CYCLE" runat="server" Height="21px"></asp:imagebutton>
                </td>
            </tr>
            --%>
    </table>
    <asp:Panel ID="pnlSeparate" runat="server">
    <table cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td><asp:Image ID="imgBlank01" Width="10px" Height="25px" runat="server" ImageUrl="../../Images/Company/Default/png/Blank.png" /></td>
            <td>
                <asp:LinkButton ID="lnkbtnShowHideEmp" runat="server"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" border="0" width="100%">
        <tr>
            <td><asp:Image ID="imgBlank02" Width="10px" Height="25px" runat="server" ImageUrl="../../Images/Company/Default/Png/Blank.png" /></td>
            <td>
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tr style="background-color:#EFEFEF">
		                <td>
		                    <asp:panel id="pnlEmpInfo" ScrollBars="Auto" runat="server" visible="True">
		                    <table id="tblEmpInfo" runat="server" cellspacing="0" cellpadding="0" border="0">
		                        <tr >
		                            <td>
		                                <table id="Table5" cellspacing="0" cellpadding="0" border="0" runat="server">
		                                    <tr>
		                                        <td colspan="6">&nbsp;<asp:Label id="lblEmpInfo" runat="server"></asp:Label></td>
		                                    </tr>
		                                    <tr>
		                                        <td style="width:120px">&nbsp;<asp:label id="lblOPTION_PAY_MODE" runat="server" Width="120px"></asp:label></td>
		                                        <td style="width:5px">:</td>
			                                    <td style="width:180px"><asp:textbox id="txtOPTION_PAY_MODE" runat="server" width="180px" enabled="true" BackColor="#EFEFEF" BorderStyle="none"></asp:textbox></td>  
			                                    <td style="width:120px">&nbsp;<asp:label id="lblOPTION_PAY_TYPE" runat="server" Width="120px"></asp:label></td>
			                                    <td style="width:5px">:</td>
			                                    <td style="width:180px"><asp:textbox id="txtOPTION_PAY_TYPE" runat="server" width="180px" enabled="true" BackColor="#EFEFEF" BorderStyle="none"></asp:textbox></td>  
		                                    </tr>
		                                    <tr>
		                                        <td style="width:120px">&nbsp;<asp:label id="lblOPTION_EMPLOYMENT_STATUS" runat="server" Width="120px"></asp:label></td>
		                                        <td style="width:5px">:</td>
			                                    <td style="width:180px"><asp:textbox id="txtOPTION_EMPLOYMENT_STATUS" runat="server" width="180px" enabled="true" BackColor="#EFEFEF" BorderStyle="none"></asp:textbox></td>  
			                                    <td style="width:120px">&nbsp;<asp:label id="lblBASIC_SALARY" runat="server" Width="120px"></asp:label></td>
			                                    <td style="width:5px">:</td>
			                                    <td style="width:180px"><asp:textbox id="txtBASIC_SALARY" runat="server" width="180px" enabled="true" BackColor="#EFEFEF" BorderStyle="none"></asp:textbox></td>  
			                                 </tr> 
			                                 <tr>
			                                    <td style="width:120px">&nbsp;<asp:label id="lblHRP_RATE" runat="server" Width="120px"></asp:label></td>
			                                    <td style="width:5px">:</td>
			                                    <td style="width:180px"><asp:textbox id="txtHRP_RATE" runat="server" width="180px" enabled="true" BackColor="#EFEFEF" BorderStyle="none"></asp:textbox></td>    
			                                    <td style="width:120px">&nbsp;<asp:label id="lblEPF" runat="server" Width="120px"></asp:label></td>
			                                    <td style="width:5px">:</td>
			                                    <td style="width:180px"><asp:textbox id="txtEPF" runat="server" width="180px" enabled="true" BackColor="#EFEFEF" BorderStyle="none"></asp:textbox></td>
			                                 </tr> 
			                                 <tr>
			                                    <td style="width:120px">&nbsp;<asp:label id="lblSOCSO" runat="server" Width="120px"></asp:label></td>
			                                    <td style="width:5px">:</td>
			                                    <td style="width:180px"><asp:textbox id="txtSOCSO" runat="server" width="180px" enabled="true" BackColor="#EFEFEF" BorderStyle="none"></asp:textbox></td>  
			                                    <td style="width:120px">&nbsp;<asp:label id="lblPCB" runat="server" Width="120px"></asp:label></td>
			                                    <td style="width:5px">:</td>
			                                    <td style="width:180px"><asp:textbox id="txtPCB" runat="server" width="180px" enabled="true" BackColor="#EFEFEF" BorderStyle="none"></asp:textbox></td>  
			                                 </tr>
			                                 <tr>
			                                    <td style="width:120px">&nbsp;<asp:label id="lblOPTION_CITIZENSHIP" runat="server" Width="120px"></asp:label></td>
			                                    <td style="width:5px">:</td>
			                                    <td style="width:180px"><asp:textbox id="txtOPTION_CITIZENSHIP" runat="server" width="180px" enabled="true" BackColor="#EFEFEF" BorderStyle="none"></asp:textbox></td>
			                                    <td style="width:120px">&nbsp;<asp:label id="lblOPTION_MARITAL_STATUS" runat="server" Width="120px"></asp:label></td>
			                                    <td style="width:5px">:</td>
			                                    <td style="width:180px"><asp:textbox id="txtOPTION_MARITAL_STATUS" runat="server" Width="180px" enabled="true" BackColor="#EFEFEF" BorderStyle="none"></asp:textbox></td>  
			                                 </tr>
			                                 <tr>
			                                    <td style="width:120px">&nbsp;<asp:label id="lblOPTION_SPOUSE_WORKING_STATUS" runat="server" Width="120px"></asp:label></td>
			                                    <td style="width:5px">:</td>
			                                    <td style="width:180px"><asp:textbox id="txtOPTION_SPOUSE_WORKING_STATUS" runat="server" width="180px" enabled="true" BackColor="#EFEFEF" BorderStyle="none"></asp:textbox></td>
			                                    <td style="width:120px">&nbsp;<asp:label id="lblNO_OF_CHILD" runat="server" Width="120px"></asp:label></td>
			                                    <td style="width:5px">:</td>
			                                    <td style="width:180px"><asp:textbox id="txtNO_OF_CHILD" runat="server" Width="180px" enabled="true" BackColor="#EFEFEF" BorderStyle="none"></asp:textbox></td>  
			                                 </tr>
			                                 <tr>
			                                    <td style="width:120px">&nbsp;<asp:label id="lblJOIN_DATE" runat="server" Width="120px"></asp:label></td>
			                                    <td style="width:5px">:</td>
			                                    <td style="width:180px"><asp:textbox id="txtJOIN_DATE" runat="server" width="180px" enabled="true" BackColor="#EFEFEF" BorderStyle="none"></asp:textbox></td>
			                                    <td style="width:120px">&nbsp;<asp:label id="lblCONFIRM_DATE" runat="server" Width="120px" ></asp:label></td>
			                                    <td style="width:5px">:</td>
			                                    <td style="width:180px"><asp:textbox id="txtCONFIRM_DATE" runat="server" Width="180px" enabled="true" BackColor="#EFEFEF" BorderStyle="none"></asp:textbox></td>  
			                                 </tr>
			                                 <tr>
			                                    <td style="width:120px">&nbsp;<asp:label id="lblRESIGN_DATE" runat="server" Width="120px"></asp:label></td>
			                                    <td style="width:5px">:</td>
			                                    <td style="width:180px"><asp:textbox id="txtRESIGN_DATE" runat="server" width="180px" enabled="true" BackColor="#EFEFEF" BorderStyle="none"></asp:textbox></td>
			                                 </tr>
		                                </table> 
		                            </td>
		                        </tr>
		                   </table></asp:panel>
			               <br />
			               <table>
		                       <tr>
		                           <td>
		                                <asp:ImageButton ID="imgbtnSearch" runat="server" />
		                           </td>
		                       </tr>
			               </table>
		                   <table id="Table6" cellspacing="0" cellpadding="0" border="0" width="100%" runat="server">
	                            <tr>
	                                <td>
	                                    <%--<div style="border-right-style:double; border-left-style:double; border-bottom-style:double; border-top-style:double; border-color:#F2F4FF; margin-left:5px; margin-top:5px; margin-right:5px">--%>
		                                    <asp:panel id="pnlGridView" ScrollBars="Auto" runat="server" visible="false">
			                                    <asp:gridview id="myGridView" Width="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
				                                    cellspacing="0" cellpadding="1" EmptyDataText="No data found!" BackColor="#F2F4FF">
                                                    <AlternatingRowStyle BackColor="#F2F4FF" />
				                                    <EditRowStyle BackColor="#FFC7C6" CssClass="dgstyle_i" />
				                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
				                                    <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
				                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
				                                    <columns>
				                                    </columns>
			                                    </asp:gridview>
					                        </asp:panel>
					                    <%--</div>--%>
	                                </td>
	                            </tr> 
	                        </table>
	                        <br />
	                        <table id="Table4" cellspacing="0" cellpadding="0" border="0" width="100%" runat="server">
	                            <tr>
	                                <td>
	                                    <%--<div style="border-right-style:double; border-left-style:double; border-bottom-style:double; border-top-style:double; border-color:#F2F4FF; margin-left:5px; margin-top:5px; margin-right:5px">--%>
		                                    <asp:panel id="pnlGridView1" ScrollBars="Auto" runat="server" visible="false">
			                                    <asp:gridview id="myGridView1" Width="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
				                                    cellspacing="0" cellpadding="1" EmptyDataText="No data found!" BackColor="#F2F4FF">
                                                    <AlternatingRowStyle BackColor="#F2F4FF" />
				                                    <EditRowStyle BackColor="#FFC7C6" CssClass="dgstyle_i" />
				                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
				                                    <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
				                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
				                                    <columns>
				                                    </columns>
			                                    </asp:gridview>
					                        </asp:panel>
					                    <%--</div>--%>
	                                </td>
	                            </tr> 
	                        </table>
	                        <!-- grv 2 -->
		                </td>
		            </tr>
                </table>
            </td>
        </tr>
    </table> 
    <table id="Table3" cellspacing="0" cellpadding="0" border="0" runat="server">
        <tr>
            <td style ="height:10px"></td>
        </tr>
	</table>
    <table cellspacing="0" cellpadding="0" border="0" width="100%">
        <tr>
            <td>
                <asp:Label ID="lblResult" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Image ID="imgbottom" Height="30px" Visible="false" runat="server" />
            </td>
        </tr>
    </table>
    </asp:Panel>
    </div>
</form>
</body>
</html>
