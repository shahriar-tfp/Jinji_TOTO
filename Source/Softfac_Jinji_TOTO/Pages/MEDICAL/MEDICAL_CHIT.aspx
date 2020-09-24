<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MEDICAL_CHIT.aspx.vb" Inherits="Pages_MEDICAL_MEDICAL_CHIT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>JINJI : MEDICAL - GENERATE CHIT</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="MEDICAL_CHIT" runat="server">
    <div>
    <asp:Panel ID="pnlEPF" runat="server">
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

    <asp:ImageButton ID="imgKeyEMPLOYEE_PROFILE_ID" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblEMPLOYEE_PROFILE_ID" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtEMPLOYEE_PROFILE_ID" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnEMPLOYEE_PROFILE_ID" runat="server" Width="21px" Height="21px"/>

    <asp:ImageButton ID="imgKeyCLAIMTYPE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblCLAIMTYPE" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtCLAIMTYPE" runat="server" Width="157px" AutoPostBack="true"></asp:TextBox>
    <asp:ImageButton ID="imgbtnCLAIMTYPE" runat="server" Width="21px" Height="21px"/>

    <asp:ImageButton ID="imgKeyPANELDOCTOR_PROFILE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblPANELDOCTOR_PROFILE" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtPANELDOCTOR_PROFILE" runat="server" Width="157px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnPANELDOCTOR_PROFILE" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgkeyBENEFICIARYVISITER" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblBENEFICIARYVISITER" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlBENEFICIARYVISITER" runat="server" Width="157px"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnBENEFICIARYVISITER" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgkeyVISITDATE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblVISITDATE" runat="server" Width="120px"></asp:Label>
    <asp:textbox ID="txtVISITDATE" runat="server" Width="157px"></asp:textbox>
    <asp:ImageButton ID="imgbtnVISITDATE" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgkeyREASON" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblREASON" runat="server" Width="120px"></asp:Label>
    <asp:textbox ID="txtREASON" runat="server" Width="157px"></asp:textbox>
    <asp:ImageButton ID="imgbtnREASON" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgkeyENTITLEMENTAMT" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblENTITLEMENTAMT" runat="server" Width="120px"></asp:Label>
    <asp:textbox ID="txtENTITLEMENTAMT" runat="server" Width="157px"></asp:textbox>
    <asp:ImageButton ID="imgbtnENTITLEMENTAMT" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgkeyUPTODATE_BALANCE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblUPTODATE_BALANCE" runat="server" Width="120px"></asp:Label>
    <asp:textbox ID="txtUPTODATE_BALANCE" runat="server" Width="157px"></asp:textbox>
    <asp:ImageButton ID="imgbtnUPTODATE_BALANCE" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgkeyCLAIMAMT" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblCLAIMAMT" runat="server" Width="120px"></asp:Label>
    <asp:textbox ID="txtCLAIMAMT" runat="server" Width="157px"></asp:textbox>
    <asp:ImageButton ID="imgbtnCLAIMAMT" runat="server" Width="21px" Height="21px"/>

    <asp:imagebutton id="imgBtnPrint" runat="server" Width="74" Height="21" OnClientClick="return confirm('Any Claim Beyond Your Amount Entitlement will be deducted from your Salary. Proceed?')"></asp:imagebutton>
    <asp:label id="lblresult2" runat="server" Height="22" Visible="False"></asp:label>
    <asp:Image ID="imgBottom" runat="server" Visible="false" />
    </asp:Panel>
    <asp:Panel ID="pnlReport" runat="server">
    </asp:Panel>
    </asp:Panel>
    <asp:panel id="pnldescription" runat="server" visible="False">
	<table id="Table7" style="left: 15px; position: absolute; top: 10px" cellspacing="0" cellpadding="0" border="0" runat="server">
        <tr>
            <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); width :5px"></td>
            <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); vertical-align:bottom"><asp:Label ID="lblTitle2" runat="server"></asp:Label></td>
        </tr>
    </table>
    <table id="Table1" cellspacing="0" cellpadding="0" border="0" runat="server">
        <tr>
            <td style ="height:15px"></td>
        </tr>
    </table>
    <div align=center>
    <table id="Table2" cellspacing="0" cellpadding="0" border="0" runat="server">
        <tr>
            <td style ="height:15px"><asp:Label ID="lblSucessfull" runat="server" Text="Generate Complete!"></asp:Label></td>
        </tr>
    </table>
    </div>
    </asp:panel>
    </div>
    </form>
</body>
</html>
