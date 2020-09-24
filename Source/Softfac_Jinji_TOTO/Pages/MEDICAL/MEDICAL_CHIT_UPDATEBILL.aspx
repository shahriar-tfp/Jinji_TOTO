<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MEDICAL_CHIT_UPDATEBILL.aspx.vb" Inherits="Pages_MEDICAL_MEDICAL_CHIT_UPDATEBILL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>JINJI : MEDICAL - CHIT UPDATEBILL</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="MEDICAL_CHIT_UPDATEBILL" runat="server">
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
    <asp:TextBox ID="txtBENEFICIARYVISITER" runat="server" Width="157px"></asp:TextBox>
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
    
    <asp:imagebutton id="imgKeyTYPE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
    <asp:label id="lblTYPE" runat="server" Width="140px" ></asp:label>
    <asp:dropdownlist id="ddlTYPE" runat="server" Width="150px" ></asp:dropdownlist>
    <asp:imagebutton id="imgbtnTYPE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
    
    <asp:imagebutton id="imgKeySTATUS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
    <asp:label id="lblSTATUS" runat="server" Width="140px" ></asp:label>
    <asp:dropdownlist id="ddlSTATUS" runat="server" Width="150px" ></asp:dropdownlist>
    <asp:imagebutton id="imgbtnSTATUS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
    
    <asp:ImageButton ID="imgkeyAUTOAPPROVALNO" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblAUTOAPPROVALNO" runat="server" Width="120px"></asp:Label>
    <asp:textbox ID="txtAUTOAPPROVALNO" runat="server" Width="157px" AutoPostBack="true"></asp:textbox>
    <asp:ImageButton ID="imgbtnAUTOAPPROVALNO" runat="server" Width="21px" Height="21px"/>
    
    <asp:imagebutton id="imgKeyBILLNO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
    <asp:label id="lblBILLNO" runat="server" Width="140px" ></asp:label>
    <asp:textbox id="txtBILLNO" runat="server" Width="150px" ></asp:textbox>
    <asp:imagebutton id="imgbtnBILLNO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
        
    <asp:imagebutton id="imgKeyBILLDATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
    <asp:label id="lblBILLDATE" runat="server" Width="140px" ></asp:label>
    <asp:textbox id="txtBILLDATE" runat="server" Width="150px" ></asp:textbox>
    <asp:imagebutton id="imgbtnBILLDATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

    <asp:imagebutton id="imgKeyBILLAMT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
    <asp:label id="lblBILLAMT" runat="server" Width="140px" ></asp:label>
    <asp:textbox id="txtBILLAMT" runat="server" Width="150px" ></asp:textbox>
    <asp:imagebutton id="imgbtnBILLAMT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

    <asp:imagebutton id="imgKeyPAYMENTMONTH" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
    <asp:label id="lblPAYMENTMONTH" runat="server" Width="140px" ></asp:label>
    <asp:textbox id="txtPAYMENTMONTH" runat="server" Width="150px" ></asp:textbox>
    <asp:imagebutton id="imgbtnPAYMENTMONTH" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

    <asp:imagebutton id="imgBtnUpdate" runat="server" Width="74" Height="21" OnClientClick="return confirm('Confirm Update!')"></asp:imagebutton>
    <asp:label id="lblresult2" runat="server" Height="22" Visible="False"></asp:label>
    <asp:Image ID="imgBottom" runat="server" Visible="false" />
    </asp:Panel>
    <asp:Panel ID="pnlReport" runat="server">
    </asp:Panel>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
