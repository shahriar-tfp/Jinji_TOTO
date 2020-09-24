<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TOTO_PROMOTED_LIST.aspx.vb" Inherits="Pages_TOTO_KPI_TOTO_PROMOTED_LIST" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>TOTO_PROMOTED_LIST View</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />

</head>
<body>
    <form id="TOTO_PROMOTED_LIST" runat="server">
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

<asp:ImageButton ID="imgKeyYEAR" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblYEAR" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlYEAR" runat="server" Width="150px" AutoPostBack="true"></asp:dropdownlist>
    <asp:ImageButton ID="imgbtnYEAR" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyCATEGORY" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblCATEGORY" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlCATEGORY" runat="server" Width="150px" AutoPostBack="true"></asp:dropdownlist>
    <asp:ImageButton ID="imgbtnCATEGORY" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOCP_ID_DEPARTMENT" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOCP_ID_DEPARTMENT" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlOCP_ID_DEPARTMENT" runat="server" Width="150px" AutoPostBack="true"></asp:dropdownlist>
    <asp:ImageButton ID="imgbtnOCP_ID_DEPARTMENT" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOCP_ID_SECTION" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOCP_ID_SECTION" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlOCP_ID_SECTION" runat="server" Width="150px" AutoPostBack="true"></asp:dropdownlist>
    <asp:ImageButton ID="imgbtnOCP_ID_SECTION" runat="server" Width="21px" Height="21px"/>
    

    <asp:imagebutton id="imgBtnPreview" runat="server" Width="74" Height="21" visible="true"></asp:imagebutton>
    <asp:imagebutton id="imgBtnPrint" runat="server" Width="74" Height="21" visible="true"></asp:imagebutton>
    <asp:imagebutton id="imgBtnClear" runat="server" Width="74" Height="21" visible="true"></asp:imagebutton>
    <asp:label id="lblresult2" runat="server" Height="22" Visible="False"></asp:label>
    <asp:Image ID="imgBottom" runat="server" Visible="false" />
    </asp:Panel>
    <asp:Panel ID="pnlGridView" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
	   <tr><td>	    
	    <div id="divGridview1" style="top: 300px;left: 15px;position :absolute; width: 100%; height: 200px;overflow:scroll " visible="true">
	    <asp:Label ID="lblgv1" Text="Promotion Preview" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            <asp:gridview id="gv1" Width="100%" runat="server" AutoGenerateColumns="false" Visible="true"
                cellspacing="0" cellpadding="1" EmptyDataText="No data found!" 
                >
                <AlternatingRowStyle BackColor="#F2F4FF" />
                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
                <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                <Columns>
                <asp:TemplateField HeaderText="Number" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblNumber" Text='<%# Container.DataItem(0) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
                <asp:TemplateField HeaderText="ID" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblID" Text='<%# Container.DataItem(1) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Name" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblName" Text='<%# Container.DataItem(2) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Department" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblDepartment" Text='<%# Container.DataItem(3) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Section" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblSection" Text='<%# Container.DataItem(4) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Current Job Grade" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblJobgrade" Text='<%# Container.DataItem(5) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Common Skill Point" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblCommon" Text='<%# Container.DataItem(6) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Special Skill Point" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblSpecial" Text='<%# Container.DataItem(7) %>' Runat="server" />
				     </ItemTemplate>
			     </asp:TemplateField>
			     <asp:TemplateField HeaderText="Total Skill Point" Visible="True">
				     <ItemTemplate>
					      <asp:Label ID="grvlblTotal" Text='<%# Container.DataItem(8) %>' Runat="server"/>
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
	         </div>
	         <!--//<div id="divChart1" style="top: 300px;left: 15px;position :absolute; width: 100%; height: 100%;overflow:scroll ">
                <asp:Chart ID="Chart1" runat="server" BorderlineColor="Black" 
   BorderlineDashStyle="Solid" BackColor="#B6D6EC" BackGradientStyle="TopBottom" 
   BackSecondaryColor="White" Height="480px" Width="600px">
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </ChartAreas>
                    <Legends>
            <asp:Legend Name="Legend1">
            </asp:Legend>
        </Legends>
                </asp:Chart>
    </div>//--></td></tr>
	         </table>
	         </asp:Panel>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
