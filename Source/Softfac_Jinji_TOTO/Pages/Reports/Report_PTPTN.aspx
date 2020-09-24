<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Report_PTPTN.aspx.vb" Inherits="Pages_Reports_Report_PTPTN" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>JINJI : Report - PCB Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="Report_PTPTN" runat="server">
    <div>
    <asp:Panel ID="pnlPCB" runat="server">
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

    <asp:ImageButton ID="imgKeyCOMPANY_PROFILE_CODE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblCOMPANY_PROFILE_CODE" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtCOMPANY_PROFILE_CODE" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnCOMPANY_PROFILE_CODE" runat="server" Width="21px" Height="21px"/>

    <asp:ImageButton ID="imgKeyYEAR_MONTH" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblYEAR_MONTH" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtYEAR_MONTH" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnYEAR_MONTH" runat="server" Width="21px" Height="21px"/>

    <asp:ImageButton ID="imgKeyOPTION_PAY_TYPE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_PAY_TYPE" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_PAY_TYPE" runat="server" Width="157px"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_PAY_TYPE" runat="server" Width="21px" Height="21px"/>

    <asp:ImageButton ID="imgKeyOPTION_LOCATION" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_LOCATION" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_LOCATION" runat="server" Width="157px"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_LOCATION" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOPTION_REPORT" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_REPORT" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_REPORT" runat="server" Width="157px"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_REPORT" runat="server" Width="21px" Height="21px"/>

    <asp:ImageButton ID="imgKeyPIC" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblPIC" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtPIC" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnPIC" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyPIC_POS" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblPIC_POS" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtPIC_POS" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnPIC_POS" runat="server" Width="21px" Height="21px"/>

    <asp:ImageButton ID="imgKeyPIC_TEL" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblPIC_TEL" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtPIC_TEL" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnPIC_TEL" runat="server" Width="21px" Height="21px"/>

    <asp:ImageButton ID="imgKeyPIC_EMAIL" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblPIC_EMAIL" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtPIC_EMAIL" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnPIC_EMAIL" runat="server" Width="21px" Height="21px"/>

    <asp:ImageButton ID="imgKeyCHEQUE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblCHEQUE" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtCHEQUE" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnCHEQUE" runat="server" Width="21px" Height="21px"/>

    <asp:imagebutton id="imgBtnPrint" runat="server" Width="74" Height="21" ></asp:imagebutton>
    <asp:label id="lblresult2" runat="server" Height="22" Visible="False"></asp:label>
    <asp:Image ID="imgBottom" runat="server" Visible="false" />
    </asp:Panel>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
