<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Report_APS.aspx.vb" Inherits="Pages_Reports_Report_APS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>JINJI : Report - APS</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="Report_APS" runat="server">
    <div>
    <asp:Panel ID="pnlAPS" runat="server">
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

    <asp:ImageButton ID="imgKeyOPTION_BANK" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_BANK" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_BANK" runat="server" Width="157px"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_BANK" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOPTION_PAY_TYPE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_PAY_TYPE" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_PAY_TYPE" runat="server" Width="157px" AutoPostBack="true"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_PAY_TYPE" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOPTION_PAY_CYCLE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_PAY_CYCLE" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_PAY_CYCLE" runat="server" Width="157px"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_PAY_CYCLE" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyCREDIT_DATE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblCREDIT_DATE" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtCREDIT_DATE" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnCREDIT_DATE" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOPTION_REPORT" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_REPORT" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_REPORT" runat="server" Width="157px"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_REPORT" runat="server" Width="21px" Height="21px"/>

<!--// 4 //-->
	<asp:image id="imgOPTION_TYPE" runat="server" Height="21px" Width="21px"></asp:image>
	<asp:label id="lblOPTION_TYPE" runat="server" Width="140px"></asp:label>
	<asp:dropdownlist id="ddlOPTION_TYPE" runat="server" Width="150px" AutoPostBack="true"></asp:dropdownlist>
	<asp:imagebutton id="imgBtnOPTION_TYPE" runat="server" Height="21px"></asp:imagebutton>
	<!--// 4 //-->
	<asp:image id="imgOCP_ID" runat="server" Height="21px" Width="21px"></asp:image>
	<asp:label id="lblOCP_ID" runat="server" Width="140px"></asp:label>
	<asp:dropdownlist id="ddlOCP_ID" runat="server" Width="150px"></asp:dropdownlist>
	<asp:imagebutton id="imgBtnOCP_ID" runat="server" Height="21px"></asp:imagebutton>
	<!--// 5 //-->
	<asp:image id="imgEMPLOYEE_PROFILE_ID" runat="server" Height="21px" Width="21px"></asp:image>
	<asp:label id="lblEMPLOYEE_PROFILE_ID" runat="server" Width="140px"></asp:label>
	<asp:textbox id="txtEMPLOYEE_PROFILE_ID" runat="server" Width="150px"></asp:textbox>
	<asp:imagebutton id="imgBtnEMPLOYEE_PROFILE_ID" runat="server" Height="21px"></asp:imagebutton>
	<asp:imagebutton id="imgBtnEMPLOYEE_PROFILE_ID2" runat="server" Height="21px"></asp:imagebutton>
	
	<!--// listbox //-->
    <asp:panel id="pnllstleft" runat="server" Width="150px" visible="false">
    <table id="Table8" cellspacing="0" cellpadding="0" border="0" runat="server">
        <tr>
            <td>
                <asp:label id="lbllstleft" runat="server" Width="150px"></asp:label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ListBox id="lstleft" Width="157px" Height="170px" SelectionMode="Multiple" runat="server" ></asp:ListBox>
            </td>
        </tr>
       </table>
    </asp:panel> 
    <asp:panel id="pnllstright" runat="server" Width="150px" visible="false">
    <table id="Table5" cellspacing="0" cellpadding="0" border="0" runat="server">
        <tr>
            <td>
                <asp:label id="lbllstright" runat="server" Width="150px"></asp:label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ListBox id="lstright" Width="157px" Height="170px" SelectionMode="Multiple" runat="server" ></asp:ListBox>
            </td>
        </tr>
       </table>
    </asp:panel> 
	<!--// Panel //-->

    <asp:imagebutton id="imgBtnAddAll" runat="server" Width="24" Height="20"></asp:imagebutton>
    <asp:imagebutton id="imgBtnAddItem" runat="server" Width="24" Height="20"></asp:imagebutton>
    <asp:imagebutton id="imgBtnRemoveAll" runat="server" Width="24" Height="20"></asp:imagebutton>
    <asp:imagebutton id="imgBtnRemoveItem" runat="server" Width="24" Height="20"></asp:imagebutton>
    <asp:imagebutton id="imgBtnSaveAs" runat="server" Width="74" Height="21" ></asp:imagebutton>
    <asp:label id="lblresult2" runat="server" Height="22" Visible="False"></asp:label>
    <asp:Image ID="imgBottom" runat="server" Visible="false" />
    </asp:Panel>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
