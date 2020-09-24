<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EMPLOYEE_ASSIGNSKILL_VIEW.aspx.vb" Inherits="Pages_TOTO_KPI_EMPLOYEE_ASSIGNSKILL_VIEW" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMPLOYEE ASSIGNSKILL View</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="EMPLOYEE_ASSIGNSKILL_VIEW" runat="server">
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

    <asp:ImageButton ID="imgKeyOCP_ID_DEPARTMENT" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOCP_ID_DEPARTMENT" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlOCP_ID_DEPARTMENT" runat="server" Width="150px" AutoPostBack="true"></asp:dropdownlist>
    <asp:ImageButton ID="imgbtnOCP_ID_DEPARTMENT" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOCP_ID_SECTION" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOCP_ID_SECTION" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlOCP_ID_SECTION" runat="server" Width="150px" AutoPostBack="true"></asp:dropdownlist>
    <asp:ImageButton ID="imgbtnOCP_ID_SECTION" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyEMPLOYEE_PROFILE_ID" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblEMPLOYEE_PROFILE_ID" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlEMPLOYEE_PROFILE_ID" runat="server" Width="150px" AutoPostBack="true"></asp:dropdownlist>
    <asp:ImageButton ID="imgbtnEMPLOYEE_PROFILE_ID" runat="server" Width="21px" Height="21px"/>

    <asp:ImageButton ID="imgKeyYEAR" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblYEAR" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlYEAR" runat="server" Width="150px"  AutoPostBack="true"></asp:dropdownlist>
    <asp:ImageButton ID="imgbtnYEAR" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOPTION_PROCESS" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_PROCESS" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_PROCESS" runat="server" Width="150px" AutoPostBack="true"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_PROCESS" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOPTION_CATEGORY" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_CATEGORY" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_CATEGORY" runat="server" Width="150px" AutoPostBack="true"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_CATEGORY" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOPTION_MODEL" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_MODEL" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_MODEL" runat="server" Width="150px" AutoPostBack="true"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_MODEL" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOPTION_COUNTRY" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_COUNTRY" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_COUNTRY" runat="server" Width="150px" AutoPostBack="true"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_COUNTRY" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOPTION_STATION" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_STATION" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_STATION" runat="server" Width="150px" AutoPostBack="true"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_STATION" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyEVALUATION" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblEVALUATION" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlEVALUATION" runat="server" Width="150px" AutoPostBack="true"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnEVALUATION" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgkeyDIRECT" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblDIRECT" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlDIRECT" runat="server" Width="150px" AutoPostBack="true"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnDIRECT" runat="server" Width="21px" Height="21px"/>

    <asp:imagebutton id="imgBtnPrint" runat="server" Width="74" Height="21" ></asp:imagebutton>
    <asp:label id="lblresult2" runat="server" Height="22" Visible="False"></asp:label>
    <asp:Image ID="imgBottom" runat="server" Visible="false" />
    </asp:Panel>
    <asp:Panel ID="pnlGridView" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
	   <tr><td>	    
	    <div id="divGridview1" style="top: 340px;left: 15px;position :absolute; width: 98%; height: 400px;overflow: scroll">
            <asp:gridview id="gv1" Width="100%" runat="server" AutoGenerateColumns="false" Visible="true"
                cellspacing="0" cellpadding="1" EmptyDataText="No data found!" OnRowDeleting="gv1_RowDeleting" 
                Caption='<table border="0" width="100%" cellpadding="0" cellspacing="0"><tr><td><font color="#ff0000">Default view Top 100 most urgent only. Please use "Filtering" to specify your selection</font></td></tr></table>' CaptionAlign="Top">
                <AlternatingRowStyle BackColor="#F2F4FF" />
                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
                <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                <Columns>
                <asp:TemplateField HeaderText="Department" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblDepartment" Text='<%# Container.DataItem(0) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Section" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblSection" Text='<%# Container.DataItem(1) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Year" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblYear" Text='<%# Container.DataItem(2) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Level" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblLevel" Text='<%# Container.DataItem(3) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Process" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblProcess" Text='<%# Container.DataItem(4) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Item / Sub-Process" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblItem" Text='<%# Container.DataItem(5) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Model" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblModel" Text='<%# Container.DataItem(6) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Country" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblCountry" Text='<%# Container.DataItem(7) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Station" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblStation" Text='<%# Container.DataItem(8) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Employee" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblEmployee" Text='<%# Container.DataItem(9) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Evaluation" Visible="True">
				     <ItemTemplate>
					      <!--//<asp:HyperLink ID="grvimgPreview1" runat="server" Visible = "true" OnClick="grvimgPreview1_Click" NavigateUrl='<%# Container.DataItem(10) %>' Text='<%# Container.DataItem(11) %>'/>//-->
					      <asp:LinkButton runat="server" ID="grvLinkButton" Visible="true" OnClick="LinkButton_Click" Text="<%# Container.DataItem(11) %>" CommandArgument="<%# Container.DataItem(9) %>" CommandName="<%# Container.DataItem(10) %>" ></asp:LinkButton>
					      <asp:HiddenField ID="grvUrl" runat="server" Value="<%# Container.DataItem(10) %>" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Delete" Visible="True">
				     <ItemTemplate>
					      <asp:LinkButton ID="LBtnDelete" runat="server" Visible = "true" CommandName="Delete" OnClientClick="return confirm('Confirmation! Delete Data(s) ?')">Delete</asp:LinkButton>
				     </ItemTemplate>
			     </asp:TemplateField>
                </Columns>
            </asp:gridview>
            <asp:gridview id="gv2" Width="100%" runat="server" AutoGenerateColumns="true" Visible="false"
                cellspacing="0" cellpadding="1" EmptyDataText="No data found!"
                Caption='<table border="0" width="100%" cellpadding="0" cellspacing="0"><tr><td><font color="#ff0000">Evaluation Form Skill</font></td></tr></table>' CaptionAlign="Top">
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
	         </table>
	         </asp:Panel>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
