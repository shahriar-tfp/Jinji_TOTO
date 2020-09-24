<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MASTERSKILL_WEIGHTAGE_ViewEdit.aspx.vb" Inherits="Pages_TOTO_KPI_MASTERSKILL_WEIGHTAGE_ViewEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>MASTERSKILL WEIGHTAGE ViewEdit</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="MASTERSKILL_WEIGHTAGE_ViewEdit" runat="server">
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

    <asp:ImageButton ID="imgKeyYEAR" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblYEAR" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlYEAR" runat="server" Width="150px"></asp:dropdownlist>
    <asp:ImageButton ID="imgbtnYEAR" runat="server" Width="21px" Height="21px"/>

    <asp:imagebutton id="imgBtnPrint" runat="server" Width="74" Height="21" ></asp:imagebutton>
    <asp:label id="lblresult2" runat="server" Height="22" Visible="False"></asp:label>
    <asp:Image ID="imgBottom" runat="server" Visible="false" />
    </asp:Panel>
    <asp:Panel ID="pnlGridView" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
	   <tr><td>	    
	    <div id="divGridview1" style="top: 180px;left: 15px;position :absolute; width: 100%; height: 200px;overflow: scroll">
            <asp:gridview id="gv1" Width="100%" runat="server" AutoGenerateColumns="false" Visible="true"
                cellspacing="0" cellpadding="1" EmptyDataText="No data found!" 
                Caption='<table border="0" width="100%" cellpadding="0" cellspacing="0"><tr><td><font color="#ff0000">Skill Weightage</font></td></tr></table>' CaptionAlign="Top">
                <AlternatingRowStyle BackColor="#F2F4FF" />
                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
                <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                <Columns>
                <asp:TemplateField HeaderText="Level" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblLevel" Text='<%# Container.DataItem(0) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Process" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblProcess" Text='<%# Container.DataItem(1) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Item / Sub-Process" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblItem" Text='<%# Container.DataItem(2) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Model" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblModel" Text='<%# Container.DataItem(3) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Country" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblCountry" Text='<%# Container.DataItem(4) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Station" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblStation" Text='<%# Container.DataItem(5) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Special Skill Weightage (%)" Visible="True">
				     <ItemTemplate>
					      <asp:textbox ID="grvlblSpecialSkill" Text='<%# Container.DataItem(6) %>' Runat="server" OnTextChanged="Special_TextChanged" AutoPostBack="true" Width="50px"/>
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
                </Columns>
            </asp:gridview>
	         </div></td></tr>
	         <tr><td><div id="divGridview2" style="top: 400px;left: 15px;position :absolute; width: 98%; height: 140px;"><asp:Label ID="lblTotalSpecial" runat="server" text="Total Special Skill" Width="150px"></asp:Label><asp:textbox ID="txtTotalSpecialSkill" runat="server" text="" Width="150px" Enabled = "false"></asp:textbox>
	         <asp:imagebutton id="imgBtnUpdate" runat="server" Width="74" Height="21" ></asp:imagebutton></div></td></tr>
	         </table>
	         </asp:Panel>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
