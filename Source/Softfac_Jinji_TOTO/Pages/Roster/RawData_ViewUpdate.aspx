<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RawData_ViewUpdate.aspx.vb" Inherits="Pages_Roster_RawData_ViewUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>JINJI : Roster - RawData View Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="RawData_ViewUpdate" runat="server">
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

    <asp:ImageButton ID="imgKeyDATEFROM" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblDATEFROM" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtDATEFROM" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnDATEFROM" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyDATETO" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblDATETO" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtDATETO" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnDATETO" runat="server" Width="21px" Height="21px"/>

    <asp:imagebutton id="imgBtnPrint" runat="server" Width="74" Height="21" ></asp:imagebutton>
    <asp:label id="lblresult2" runat="server" Height="22" Visible="False"></asp:label>
    <asp:Image ID="imgBottom" runat="server" Visible="false" />
    </asp:Panel>
    <asp:Panel ID="pnlGridView" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
	   <tr><td>	    
	    <div id="divGridview1" style="top: 200px;left: 15px;position :absolute; width: 98%; height: 150px; overflow: scroll">
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
	         </asp:Panel>
	         <asp:Panel ID="pnlEditInOut" runat="server">
	         <div id="div1" style="top: 420px;left: 15px;position :absolute; width: 98%; height: 140px;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
	   <tr><td><asp:Label ID="lblDateIn" Text="Date In" runat="server" Width="120px" Font-Bold="true"></asp:Label>
    <asp:TextBox ID="txtDateIn" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgBtnDateIn" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblDateOut" Text="Date Out" runat="server" Width="120px" Font-Bold="true"></asp:Label>
    <asp:TextBox ID="txtDateOut" runat="server" Width="150px" ></asp:TextBox>
    <asp:ImageButton ID="imgBtnDateOut" runat="server" Width="21px" Height="21px"/>
    </td></tr>
    <tr><td><asp:Label ID="lblspace1" Text="&nbsp;" runat="server" Width="10px"></asp:Label></td></tr>
    <tr><td><asp:Label ID="lblOption_Reason_Type" Text="Reason Type" runat="server" Width="120px" Font-Bold="true"></asp:Label>
    <asp:dropdownlist ID="ddlOption_Reason_Type" runat="server" Width="180px" AutoPostBack=true></asp:dropdownlist>
    <asp:Label ID="lblReason" Text="Reason" runat="server" Width="120px" Font-Bold="true"></asp:Label>
    <asp:TextBox ID="txtReason" runat="server" Width="150px" ></asp:TextBox>
    <asp:ImageButton ID="imgbtnReason" runat="server" Width="21px" Height="21px"/>
    </td></tr>
      <tr><td><asp:Label ID="Label1" Text="&nbsp;" runat="server" Width="10px"></asp:Label></td></tr>
    <tr><td><asp:ImageButton id="imgBtnSubmit" runat="server" Width="74" Height="21"/><asp:ImageButton id="ImgBtnDelete" runat="server" Width="74" Height="21"/>
    <asp:imagebutton id="imgbtnReProcess" runat="server" Height="21px" Width="74px"></asp:imagebutton></td></tr>
</table></div></asp:Panel>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
