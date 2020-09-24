<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HR_ESH_PROPERTY_PROFILE.aspx.vb" Inherits="PAGES_HOSTEL_HR_ESH_PROPERTY_PROFILE" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>JINJI : Hostel - Property Profile Page</title>
<link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server">
<form id="HR_ESH_PROPERTY_PROFILE" runat="server">
<div>
<asp:Panel ID="pnlEdit" runat="server">
<!--// 1 //-->
<asp:imagebutton id="imgKeyOCP_ID_PROPERTY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOCP_ID_PROPERTY" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtOCP_ID_PROPERTY" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnOCP_ID_PROPERTY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 2 //-->
<asp:imagebutton id="imgKeyOCP_ID_PROPERTY_AREA" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOCP_ID_PROPERTY_AREA" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtOCP_ID_PROPERTY_AREA" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnOCP_ID_PROPERTY_AREA" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 3 //-->
<asp:imagebutton id="imgKeyLOT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblLOT" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtLOT" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnLOT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 4 //-->
<asp:imagebutton id="imgKeySTREET" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblSTREET" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtSTREET" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnSTREET" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 5 //-->
<asp:imagebutton id="imgKeyPOSTAL" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblPOSTAL" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtPOSTAL" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnPOSTAL" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 6 //-->
<asp:imagebutton id="imgKeyADDRESS_CITY_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblADDRESS_CITY_CODE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtADDRESS_CITY_CODE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnADDRESS_CITY_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 7 //-->
<asp:imagebutton id="imgKeyADDRESS_REGION_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblADDRESS_REGION_CODE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtADDRESS_REGION_CODE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnADDRESS_REGION_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 8 //-->
<asp:imagebutton id="imgKeyADDRESS_STATE_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblADDRESS_STATE_CODE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtADDRESS_STATE_CODE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnADDRESS_STATE_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 9 //-->
<asp:imagebutton id="imgKeyADDRESS_COUNTRY_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblADDRESS_COUNTRY_CODE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtADDRESS_COUNTRY_CODE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnADDRESS_COUNTRY_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 10 //-->
<asp:imagebutton id="imgKeyOWNER_PROPERTY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOWNER_PROPERTY" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtOWNER_PROPERTY" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnOWNER_PROPERTY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 11 //-->
<asp:imagebutton id="imgKeyOWNER_PHONE_NO_HOME" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOWNER_PHONE_NO_HOME" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtOWNER_PHONE_NO_HOME" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnOWNER_PHONE_NO_HOME" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 12 //-->
<asp:imagebutton id="imgKeyOWNER_PHONE_NO_MOBILE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOWNER_PHONE_NO_MOBILE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtOWNER_PHONE_NO_MOBILE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnOWNER_PHONE_NO_MOBILE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 13 //-->
<asp:imagebutton id="imgKeyOWNER_FAX_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOWNER_FAX_NO" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtOWNER_FAX_NO" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnOWNER_FAX_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 14 //-->
<asp:imagebutton id="imgKeyOWNER_BANK_PROFILE_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOWNER_BANK_PROFILE_CODE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtOWNER_BANK_PROFILE_CODE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnOWNER_BANK_PROFILE_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 15 //-->
<asp:imagebutton id="imgKeyOWNER_BANK_BRANCH_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOWNER_BANK_BRANCH_CODE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtOWNER_BANK_BRANCH_CODE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnOWNER_BANK_BRANCH_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 16 //-->
<asp:imagebutton id="imgKeyOWNER_BANK_ACCOUNT_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOWNER_BANK_ACCOUNT_NO" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtOWNER_BANK_ACCOUNT_NO" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnOWNER_BANK_ACCOUNT_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 17 //-->
<asp:imagebutton id="imgKeyCONTRACT_START_DATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCONTRACT_START_DATE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCONTRACT_START_DATE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCONTRACT_START_DATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 18 //-->
<asp:imagebutton id="imgKeyCONTRACT_END_DATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCONTRACT_END_DATE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCONTRACT_END_DATE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCONTRACT_END_DATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 19 //-->
<asp:imagebutton id="imgKeyRENTAL_AMOUNT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblRENTAL_AMOUNT" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtRENTAL_AMOUNT" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnRENTAL_AMOUNT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 20 //-->
<asp:imagebutton id="imgKeyRENTAL_DUE_DAY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblRENTAL_DUE_DAY" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtRENTAL_DUE_DAY" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnRENTAL_DUE_DAY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 21 //-->
<asp:imagebutton id="imgKeyPROPERTY_STATUS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblPROPERTY_STATUS" runat="server" Width="140px" ></asp:label>
<asp:dropdownlist id="ddlPROPERTY_STATUS" runat="server" Width="157px" ></asp:dropdownlist>
<asp:imagebutton id="imgbtnPROPERTY_STATUS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 22 //-->
<asp:imagebutton id="imgKeyOCP_ID_BUS_ROUTE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOCP_ID_BUS_ROUTE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtOCP_ID_BUS_ROUTE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnOCP_ID_BUS_ROUTE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 23 //-->
<asp:imagebutton id="imgKeyOCP_ID_BUS_STOP" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOCP_ID_BUS_STOP" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtOCP_ID_BUS_STOP" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnOCP_ID_BUS_STOP" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 24 //-->
<asp:imagebutton id="imgKeyCARE_TAKER1" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCARE_TAKER1" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCARE_TAKER1" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCARE_TAKER1" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 25 //-->
<asp:imagebutton id="imgKeyCARE_TAKER1_PHONE_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCARE_TAKER1_PHONE_NO" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCARE_TAKER1_PHONE_NO" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCARE_TAKER1_PHONE_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 26 //-->
<asp:imagebutton id="imgKeyCARE_TAKER2" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCARE_TAKER2" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCARE_TAKER2" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCARE_TAKER2" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 27 //-->
<asp:imagebutton id="imgKeyCATE_TAKER2_PHONE_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCATE_TAKER2_PHONE_NO" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCATE_TAKER2_PHONE_NO" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCATE_TAKER2_PHONE_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 28 //-->
<asp:imagebutton id="imgKeyCARE_TAKER3" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCARE_TAKER3" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCARE_TAKER3" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCARE_TAKER3" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 29 //-->
<asp:imagebutton id="imgKeyCARE_TAKER3_PHONE_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCARE_TAKER3_PHONE_NO" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCARE_TAKER3_PHONE_NO" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCARE_TAKER3_PHONE_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 30 //-->
<asp:imagebutton id="imgKeyCARE_TAKER4" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCARE_TAKER4" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCARE_TAKER4" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCARE_TAKER4" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 31 //-->
<asp:imagebutton id="imgKeyCARE_TAKER4_PHONE_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCARE_TAKER4_PHONE_NO" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCARE_TAKER4_PHONE_NO" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCARE_TAKER4_PHONE_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 32 //-->
<asp:imagebutton id="imgKeyCARE_TAKER5" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCARE_TAKER5" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCARE_TAKER5" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCARE_TAKER5" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 33 //-->
<asp:imagebutton id="imgKeyCARE_TAKER5_PHONE_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCARE_TAKER5_PHONE_NO" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCARE_TAKER5_PHONE_NO" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCARE_TAKER5_PHONE_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 34 //-->
<asp:imagebutton id="imgKeyREMARK1" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblREMARK1" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtREMARK1" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnREMARK1" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 35 //-->
<asp:imagebutton id="imgKeyREMARK2" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblREMARK2" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtREMARK2" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnREMARK2" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 36 //-->
<asp:imagebutton id="imgKeyREMARK3" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblREMARK3" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtREMARK3" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnREMARK3" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 37 //-->
<asp:imagebutton id="imgKeyREMARK4" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblREMARK4" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtREMARK4" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnREMARK4" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 38 //-->
<asp:imagebutton id="imgKeyREMARK5" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblREMARK5" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtREMARK5" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnREMARK5" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<asp:imagebutton id="imgKeyUSER_PROFILE_CODE_CREATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblUSER_PROFILE_CODE_CREATE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtUSER_PROFILE_CODE_CREATE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnUSER_PROFILE_CODE_CREATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<asp:imagebutton id="imgKeyDATETIME_CREATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblDATETIME_CREATE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtDATETIME_CREATE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnDATETIME_CREATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<asp:imagebutton id="imgKeyUSER_PROFILE_CODE_MODIFY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblUSER_PROFILE_CODE_MODIFY" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtUSER_PROFILE_CODE_MODIFY" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnUSER_PROFILE_CODE_MODIFY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<asp:imagebutton id="imgKeyDATETIME_MODIFY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblDATETIME_MODIFY" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtDATETIME_MODIFY" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnDATETIME_MODIFY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

    <!--// Panel //-->
    <div id="divtop" style="height: 30px; width: 100%">
       <table id="Table3" cellSpacing="0" cellPadding="0" align="center" border="0" runat="server">
        <tr>
            <td>
                <asp:Image ID="imgtop" Height="30px" runat="server" />
            </td>
        </tr>
       </table>
    </div>
    <div id="divbottom" style="height: 30px; width: 100%">
       <table id="Table4" cellSpacing="0" cellPadding="0" align="center" border="0" runat="server">
        <tr>
            <td>
                <asp:Image ID="imgbottom" Height="30px" Visible="false" runat="server" />
            </td>
        </tr>
       </table>
    </div>
    <!--// Button //-->  
    <asp:imagebutton id="imgBtnSubmit" runat="server" Width="74" Height="21" OnClientClick="return confirm('Are you sure you want to submit?')"></asp:imagebutton>
    <asp:imagebutton id="imgBtnSearch" runat="server" Width="74" Height="21"></asp:imagebutton>
    <asp:imagebutton id="imgBtnUpdate" runat="server" Width="74" Height="21" OnClientClick="return confirm('Are you sure you want to update?')"></asp:imagebutton>
    <asp:imagebutton id="imgBtnClear" runat="server" Width="74" Height="21"></asp:imagebutton>
    <asp:imagebutton id="imgBtnCancel" runat="server" Width="74" Height="21"></asp:imagebutton>
	<asp:label id="lblresult2" runat="server" Height="22" Visible="False"></asp:label>
	<asp:placeholder id="MyPH" Runat="server"></asp:placeholder></asp:Panel>
	<asp:panel id="pnldescription" runat="server" visible="False">
	<table id="Table7" style="left: 15px; position: absolute; top: 10px" cellspacing="0" cellpadding="0" border="0" runat="server">
        <tr>
            <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); width :5px"></td>
            <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); vertical-align:bottom"><asp:Label ID="lblTitle2" runat="server"></asp:Label></td>
        </tr>
    </table>
    </asp:panel>
	<asp:panel id="pnlMain" runat="server" >
	<table id="tblUserCategoryGroup" style="Z-INDEX: 115; LEFT: 10px; POSITION: absolute; TOP: 10px" borderColor="#e4d6fe"
					cellSpacing="0" cellPadding="0" align="center" border="0" runat="server">
					<tr>
						<td>
						    <table id="Table6" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
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
							<table id="tblGridView" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0" runat="server">
								<tr>
									<td>
										<asp:panel id="pnlGridview" runat="server" visible="true" ScrollBars="auto">
											
												<asp:gridview id="myGridView" Width="100%" runat="server" AutoGenerateColumns=true
													CellSpacing="0" CellPadding="1" EmptyDataText="No data found!">
													<AlternatingRowStyle BackColor="#F2F4FF" />
													<EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
													<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
													<Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
													<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
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
        </asp:GridView>
                <asp:Panel ID="pnlHistory" runat="server" Visible="false">
        <asp:gridview id="gvHistory" Width="100%" runat="server" AutoGenerateColumns=true
													CellSpacing="0" CellPadding="1" EmptyDataText="No data found!" >
													<AlternatingRowStyle BackColor="#F2F4FF" />
													<EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
													<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
													<Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
													<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
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
        </asp:GridView></asp:Panel>
        <asp:SqlDataSource ID="myDSR" runat=server></asp:SqlDataSource> 
	</asp:panel></td>
								</tr>
								<tr>
									<td>
										<asp:panel id="pnlPrevNext" runat="server" visible="true">
											<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										        <tr>
											        <td align="left">
												        <P class="wordstyle">&nbsp;Page&nbsp;
													        <asp:Label id="CurrentPage" runat="server" CssClass="wordstyle"></asp:Label>&nbsp;of&nbsp;
													        <asp:Label id="TotalPages" runat="server" CssClass="wordstyle"></asp:Label>
													        <asp:Label id="lblTotal" runat="server" CssClass="wordstyle"></asp:Label></P>
											        </td>
											        <td align="center">
												        <asp:LinkButton id="lnkbtnFirstPage" runat="server" CssClass="wordstyle" Text="[First]" ></asp:LinkButton>
												        <asp:LinkButton id="lnkbtnPrevPage" runat="server" CssClass="wordstyle" Text="[Previous]" ></asp:LinkButton>
													<asp:LinkButton id="lnkbtnNextPage" runat="server" CssClass="wordstyle" Text="[Next]" ></asp:LinkButton>
													<asp:LinkButton id="lnkbtnLastPage" runat="server" CssClass="wordstyle" Text="[Last]" ></asp:LinkButton></td>
											        <td align="left">
												        <asp:Label id="lblGoToPage" runat="server" Text="Go To Page" CssClass="wordstyle1"></asp:Label>
												        <asp:TextBox id="txtGoToPage" runat="server" Width="35px" CssClass="toppos"></asp:TextBox>
												        <asp:ImageButton id="imgBtnGoToPage" Height="21px" ImageAlign="AbsBottom" Runat="server"></asp:ImageButton></td>
												    <td width="5px">&nbsp;</td>
										        </tr>
									        </table>
										</asp:panel></td>
								</tr>
								<tr>
									<td>
										<asp:panel id="pnlaction" runat="server" visible="true">
											<table cellspacing="0" cellpadding="0" border="0">
												<tr>
													<td>&nbsp;</td>
													<td>&nbsp;</td>
													<td>&nbsp;</td>
												</tr>
												<tr>
												    <td style="width:2px">&nbsp;</td>
												    <td align="left">
														<asp:imagebutton id="imgBtnAdd" runat="server" Height="21px" Width="74px"></asp:imagebutton>
														<asp:imagebutton id="imgBtnEdit" runat="server" Height="21px" Width="74px"></asp:imagebutton>
														<asp:imagebutton id="imgBtnDelete" runat="server" Height="21px" Width="74px" OnClientClick="return confirm('Confirmation! Delete Data(s) ?')"></asp:imagebutton>
														<asp:imagebutton id="imgBtnFilter" runat="server" Height="21px" Width="74px"></asp:imagebutton>
														<asp:imagebutton id="imgBtnPrint" runat="server" Height="21px" Width="74px"></asp:imagebutton>
													</td>
													<td align="left">
													    <asp:panel id="pnlresult" runat="server" visible="true">
									                        <table cellspacing="0" cellpadding="0" border="0">
												                <tr>
										                            <td><asp:Image ID="Image1" ImageUrl="../../../images/company/default/gif/px1.gif" Width="5px" runat="server" /></td>
											                        <td align="left"><asp:Label id="lblresult" runat="server" CssClass="wordstyle2"></asp:Label></td>
										                        </tr>
									                        </table>
								                        </asp:panel> 
													</td>
												</tr>
											</table>
										</asp:panel></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
	</asp:panel> 
    </div>
    </form>
</body>
</html>
