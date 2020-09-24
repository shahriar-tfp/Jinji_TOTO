<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Employee_Increment_View.aspx.vb" Inherits="Pages_InformationSystem_Employee_Increment_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>JINJI : Info - Employee Increment View Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="Employee_Increment_View" runat="server">
    <div>
    <asp:Panel ID="pnlSalary_Master" runat="server">
    <table id="Table6" cellpadding="0" cellspacing="0" width="100%" border="0" runat="server">
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
    <asp:Panel ID="pnlEdit" runat="server">
    <asp:Image ID="imgTop" runat="server" Height="30px" />

    <asp:ImageButton ID="imgKeyEMPLOYEE_PROFILE_ID" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblEMPLOYEE_PROFILE_ID" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtEMPLOYEE_PROFILE_ID" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnEMPLOYEE_PROFILE_ID" runat="server" Width="21px" Height="21px"/>

    <asp:ImageButton ID="imgKeyPOSITION" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblPOSITION" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtPOSITION" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnPOSITION" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyCOSTBLOCK" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblCOSTBLOCK" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtCOSTBLOCK" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnCOSTBLOCK" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeySUPCODE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblSUPCODE" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtSUPCODE" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnSUPCODE" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeySALARY" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblSALARY" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtSALARY" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnSALARY" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyGRADE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblGRADE" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtGRADE" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnGRADE" runat="server" Width="21px" Height="21px"/>

    <asp:imagebutton id="imgBtnPrint" runat="server" Width="74" Height="21" ></asp:imagebutton>
    <asp:label id="lblresult2" runat="server" Height="22" Visible="False"></asp:label>
    <asp:Image ID="imgBottom" runat="server" Visible="false" />
    </asp:Panel>
    <asp:Panel ID="pnlGridView" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
	   <tr><td>	    
	    <div id="divGridview1" style="top: 250px;left: 15px;position :absolute; width: 98%; height: 150px; overflow: scroll">
            <asp:gridview id="gv1" Width="100%" runat="server" AutoGenerateColumns="true" Visible="true"
                cellspacing="0" cellpadding="1" EmptyDataText="No data found!" 
                Caption='<table border="0" width="100%" cellpadding="0" cellspacing="0"><tr><td><font color="#ff0000">Atendance</font></td></tr></table>' CaptionAlign="Top">
                <AlternatingRowStyle BackColor="#F2F4FF" />
                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
                <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                <Columns>
            <asp:TemplateField HeaderText="Select" ItemStyle-Width="35">
                <ItemTemplate>
                    <center>
                    <asp:CheckBox ID="chkSelect" runat="server" />
                    </center>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
            </asp:gridview>
            <asp:gridview id="gv2" Width="100%" runat="server" AutoGenerateColumns="true" Visible="false"
                cellspacing="0" cellpadding="1" EmptyDataText="No data found!"
                Caption='<table border="0" width="100%" cellpadding="0" cellspacing="0"><tr><td><font color="#ff0000">Raw Data</font></td></tr></table>' CaptionAlign="Top">
                <AlternatingRowStyle BackColor="#F2F4FF" />
                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
                <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                <Columns>
            <asp:TemplateField HeaderText="Select" ItemStyle-Width="35">
                <ItemTemplate>
                    <center>
                    <asp:CheckBox ID="chkSelect" runat="server" />
                    </center>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
            </asp:gridview>
	         </div></td></tr>
	    <!--//      <tr><td>	    
	    <div id="divGridview3" style="top: 420px;left: 15px;position :absolute; width: 100%; height: 50px;">
            <asp:gridview id="gv3" Width="100%" runat="server" AutoGenerateColumns="true" Visible="true"
                cellspacing="0" cellpadding="1" EmptyDataText="No data found!"
                Caption='<table border="0" width="100%" cellpadding="0" cellspacing="0"><tr><td><font color="#ff0000">PreApproved OT</font></td></tr></table>' CaptionAlign="Top">
                <AlternatingRowStyle BackColor="#F2F4FF" />
                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
                <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                <Columns>
                </Columns>
            </asp:gridview>
	         </div></td></tr>//-->
	         </table>
	         <asp:Panel ID="pnlEditInOut" runat="server">
	         <div id="div1" style="top: 420px;left: 15px;position :absolute; width: 98%; height: 140px;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
	   <tr><td>
	   <asp:ImageButton ID="imgKeyEFFECTIVE_DATE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblEFFECTIVE_DATE" runat="server" Width="140px"></asp:Label>
    <asp:TextBox ID="txtEFFECTIVE_DATE" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnEFFECTIVE_DATE" runat="server" Width="21px" Height="21px"/>
	<asp:imagebutton id="imgKeyOPTION_TYPE_INCREMENT" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblOPTION_TYPE_INCREMENT" runat="server" Width="140px"></asp:label>
    <asp:dropdownlist id="ddlOPTION_TYPE_INCREMENT" runat="server" Width="157px"></asp:dropdownlist>
    <asp:imagebutton id="imgbtnOPTION_TYPE_INCREMENT" runat="server" Height="21px"></asp:imagebutton>
    </td></tr>
    <tr><td>
    <asp:imagebutton id="imgKeyOLD_SALARY" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblOLD_SALARY" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtOLD_SALARY" runat="server" Width="150px" AutoPostBack="true"></asp:textbox>
    <asp:imagebutton id="imgbtnOLD_SALARY" runat="server" Height="21px"></asp:imagebutton>
    <asp:imagebutton id="imgKeyOCP_ID_JOB_GRADE_NEW" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblOCP_ID_JOB_GRADE_NEW" runat="server" Width="140px"></asp:label>
    <asp:dropdownlist id="ddlOCP_ID_JOB_GRADE_NEW" runat="server" Width="157px" ></asp:dropdownlist>
    <asp:imagebutton id="imgbtnOCP_ID_JOB_GRADE_NEW" runat="server" Height="21px"></asp:imagebutton>
    </td></tr>
    <tr><td>
    <asp:imagebutton id="imgKeyOCP_ID_APPRAISAL_NEW" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblOCP_ID_APPRAISAL_NEW" runat="server" Width="140px"></asp:label>
    <asp:dropdownlist id="ddlOCP_ID_APPRAISAL_NEW" runat="server" Width="157px" AutoPostBack="true"></asp:dropdownlist>
    <asp:imagebutton id="imgbtnOCP_ID_APPRAISAL_NEW" runat="server" Height="21px"></asp:imagebutton>
    <asp:imagebutton id="imgKeyOCP_ID_JOB_TITLE_NEW" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblOCP_ID_JOB_TITLE_NEW" runat="server" Width="140px"></asp:label>
    <asp:dropdownlist id="ddlOCP_ID_JOB_TITLE_NEW" runat="server" Width="157px" ></asp:dropdownlist>
    <asp:imagebutton id="imgbtnOCP_ID_JOB_TITLE_NEW" runat="server" Height="21px"></asp:imagebutton>
    </td></tr>
    <tr><td>
    <asp:imagebutton id="imgKeyINCREMENT" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblINCREMENT" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtINCREMENT" runat="server" Width="150px" AutoPostBack="true"></asp:textbox>
    <asp:imagebutton id="imgbtnINCREMENT" runat="server" Height="21px"></asp:imagebutton>
    <asp:imagebutton id="imgKeyPERADJ" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblPERADJ" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtPERADJ" runat="server" Width="150px" AutoPostBack="true"></asp:textbox>
    <asp:imagebutton id="imgbtnPERADJ" runat="server" Height="21px"></asp:imagebutton>
    </td></tr>
    <tr><td>
    <asp:imagebutton id="imgKeyPROMOTION" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblPROMOTION" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtPROMOTION" runat="server" Width="150px" AutoPostBack="true"></asp:textbox>
    <asp:imagebutton id="imgbtnPROMOTION" runat="server" Height="21px"></asp:imagebutton>
    <asp:imagebutton id="imgKeyNEW_SALARY" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblNEW_SALARY" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtNEW_SALARY" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgbtnNEW_SALARY" runat="server" Height="21px"></asp:imagebutton>
    </td></tr>
    <tr><td>
    <asp:imagebutton id="imgKeyTOTALADJ" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblTOTALADJ" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtTOTALADJ" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgbtnTOTALADJ" runat="server" Height="21px"></asp:imagebutton>
    <asp:imagebutton id="imgKeyTOTALADJPERC" runat="server" Width="21px" Height="21px"></asp:imagebutton>
    <asp:label id="lblTOTALADJPERC" runat="server" Width="140px"></asp:label>
    <asp:textbox id="txtTOTALADJPERC" runat="server" Width="150px"></asp:textbox>
    <asp:imagebutton id="imgbtnTOTALADJPERC" runat="server" Height="21px"></asp:imagebutton>
    </td></tr>
    <tr><td><asp:ImageButton id="imgBtnAdd" runat="server" Width="74" Height="21"/><asp:ImageButton id="imgBtnEdit" runat="server" Width="74" Height="21"/>
    <asp:ImageButton id="ImgBtnDelete" runat="server" Width="74" Height="21" OnClientClick="return confirm('Confirmation! Delete Data(s) ?')"/>
   </td></tr>
   <tr><td><asp:ImageButton id="imgbtnSubmit" runat="server" Width="74" Height="21" OnClientClick="return confirm('Are you sure you want to submit?')"/><asp:ImageButton id="imgbtnCancel" runat="server" Width="74" Height="21"/>
   </td></tr>
   <tr><td><asp:ImageButton id="imgbtnUpdate" runat="server" Width="74" Height="21" OnClientClick="return confirm('Are you sure you want to update?')"/><asp:ImageButton id="imgbtnCancel2" runat="server" Width="74" Height="21"/>
   </td></tr>
</table></div></asp:Panel>
	         </asp:Panel>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
