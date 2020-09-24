<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OVERALL_SKILL_VIEW.aspx.vb" Inherits="Pages_TOTO_KPI_OVERALL_SKILL_VIEW" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>OVERALL SKILL View</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />

</head>
<body>
    <form id="OVERALL_SKILL_VIEW" runat="server">
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
    
    <asp:ImageButton ID="imgKeyPROCESS" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblPROCESS" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlPROCESS" runat="server" Width="150px" AutoPostBack="true"></asp:dropdownlist>
    <asp:ImageButton ID="imgbtnPROCESS" runat="server" Width="21px" Height="21px"/>

    <asp:ImageButton ID="imgKeyITEM" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblITEM" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlITEM" runat="server" Width="150px" AutoPostBack="true"></asp:dropdownlist>
    <asp:ImageButton ID="imgbtnITEM" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyMODEL" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblMODEL" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlMODEL" runat="server" Width="150px" AutoPostBack="true"></asp:dropdownlist>
    <asp:ImageButton ID="imgBtnMODEL" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyCOUNTRY" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblCOUNTRY" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlCOUNTRY" runat="server" Width="150px" AutoPostBack="true"></asp:dropdownlist>
    <asp:ImageButton ID="imgBtnCOUNTRY" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyYEAR" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblYEAR" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlYEAR" runat="server" Width="150px" AutoPostBack="true"></asp:dropdownlist>
    <asp:ImageButton ID="imgbtnYEAR" runat="server" Width="21px" Height="21px"/>

    <asp:imagebutton id="imgBtnPrint" runat="server" Width="74" Height="21" visible="true"></asp:imagebutton>
    <asp:imagebutton id="imgBtnPreview" runat="server" Width="74" Height="21" visible="true"></asp:imagebutton>
    <asp:imagebutton id="imgBtnClear" runat="server" Width="74" Height="21" visible="true"></asp:imagebutton>
    <asp:label id="lblresult2" runat="server" Height="22" Visible="False"></asp:label>
    <asp:Image ID="imgBottom" runat="server" Visible="false" />
    </asp:Panel>
    <asp:Panel ID="pnlGridView" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
	   <tr><td>	    
	    <!--//<div id="divGridview1" style="top: 300px;left: 15px;position :absolute; width: 100%; height: 200px;overflow:scroll " visible="false">
            <asp:gridview id="gv1" Width="100%" runat="server" AutoGenerateColumns="true" Visible="false"
                cellspacing="0" cellpadding="1" EmptyDataText="No data found!" 
                Caption='<table border="0" width="100%" cellpadding="0" cellspacing="0"><tr><td><font color="#ff0000">Identify Critical Skill at Each Dept/Section</font></td></tr></table>' CaptionAlign="Top">
                <AlternatingRowStyle BackColor="#F2F4FF" />
                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
                <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                <Columns>
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
	         </div>//-->
	         <div id="divChart1" style="top: 300px;left: 15px;position :absolute; width: 100%; height: 100%;overflow:scroll ">
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
    </div></td></tr>
	         </table>
	         </asp:Panel>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
