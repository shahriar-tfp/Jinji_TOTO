<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Report_SIP.aspx.vb" Inherits="Pages_Reports_Report_SIP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>JINJI : Report - SIP Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="Report_SIP" runat="server">
    <div>
    <asp:Panel ID="pnlSIP" runat="server">
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
    
    <asp:ImageButton ID="imgKeyOPTION_SIP_TYPE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_SIP_TYPE" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlOPTION_SIP_TYPE" runat="server" Width="150px"></asp:dropdownlist>
    <asp:ImageButton ID="imgbtnOPTION_SIP_TYPE" runat="server" Width="21px" Height="21px"/>
    <!--// New Division SY Tey start//-->
    <asp:ImageButton ID="imgKeyOCP_ID" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOCP_ID" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtOCP_ID" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnOCP_ID" runat="server" Width="21px" Height="21px"/>
    <!--// New Division SY Tey end//-->

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
    
    <asp:ImageButton ID="imgKeyOPTION_PAGE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_PAGE" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_PAGE" runat="server" Width="157px"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_PAGE" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgkeyCHEQUE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblCHEQUE" runat="server" Width="120px"></asp:Label>
    <asp:textbox ID="txtCHEQUE" runat="server" Width="157px"></asp:textbox>
    <asp:ImageButton ID="imgbtnCHEQUE" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgkeyPIC_NAME" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblPIC_NAME" runat="server" Width="120px"></asp:Label>
    <asp:textbox ID="txtPIC_NAME" runat="server" Width="157px"></asp:textbox>
    <asp:ImageButton ID="imgbtnPIC_NAME" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgkeyPIC_NRIC" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblPIC_NRIC" runat="server" Width="120px"></asp:Label>
    <asp:textbox ID="txtPIC_NRIC" runat="server" Width="157px"></asp:textbox>
    <asp:ImageButton ID="imgbtnPIC_NRIC" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgkeyPIC_POSITION" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblPIC_POSITION" runat="server" Width="120px"></asp:Label>
    <asp:textbox ID="txtPIC_POSITION" runat="server" Width="157px"></asp:textbox>
    <asp:ImageButton ID="imgbtnPIC_POSITION" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgkeyPIC_TELNO" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblPIC_TELNO" runat="server" Width="120px"></asp:Label>
    <asp:textbox ID="txtPIC_TELNO" runat="server" Width="157px"></asp:textbox>
    <asp:ImageButton ID="imgbtnPIC_TELNO" runat="server" Width="21px" Height="21px"/>

    <asp:imagebutton id="imgBtnPrint" runat="server" Width="74" Height="21" ></asp:imagebutton>
    <asp:imagebutton id="imgBtnSaveAs" runat="server" Width="74" Height="21" ></asp:imagebutton>
    <asp:imagebutton id="imgBtnPrintSIPA" runat="server" Width="74" Height="21" OnClientClick="return confirm('Confirmation! Print SIP Lampiran A Report. ?')"></asp:imagebutton>
    <asp:label id="lblresult2" runat="server" Height="22" Visible="False"></asp:label>
    <asp:Image ID="imgBottom" runat="server" Visible="false" />
    </asp:Panel>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
