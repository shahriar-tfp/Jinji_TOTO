<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EA_FORM_MANUAL.aspx.vb" Inherits="PAGES_REPORTS_EA_FORM_MANUAL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>EA_FORM_MANUAL</title>
<link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server"> 
<form id="EA_FORM_MANUAL" runat="server">
<div>
<asp:Panel ID="pnlEdit" runat="server">
<!--// 1 //-->
<asp:imagebutton id="imgKeyCOMPANY_PROFILE_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCOMPANY_PROFILE_CODE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCOMPANY_PROFILE_CODE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCOMPANY_PROFILE_CODE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 2 //-->
<asp:imagebutton id="imgKeyEMPLOYEE_PROFILE_ID" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblEMPLOYEE_PROFILE_ID" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtEMPLOYEE_PROFILE_ID" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnEMPLOYEE_PROFILE_ID" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 3 //-->
<asp:imagebutton id="imgKeyYEAR" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblYEAR" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtYEAR" runat="server" Width="150px" AutoPostBack="true" ></asp:textbox>
<asp:imagebutton id="imgbtnYEAR" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 4 //-->
<asp:imagebutton id="imgKeySERIAL_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblSERIAL_NO" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtSERIAL_NO" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnSERIAL_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 5 //-->
<asp:imagebutton id="imgKeyALLOWANCE_1" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_1" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_1" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_1" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 6 //-->
<asp:imagebutton id="imgKeyALLOWANCE_2" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_2" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_2" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_2" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 7 //-->
<asp:imagebutton id="imgKeyALLOWANCE_3" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_3" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_3" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_3" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 8 //-->
<asp:imagebutton id="imgKeyALLOWANCE_4" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_4" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_4" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_4" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 9 //-->
<asp:imagebutton id="imgKeyALLOWANCE_5" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_5" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_5" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_5" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 10 //-->
<asp:imagebutton id="imgKeyALLOWANCE_6" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_6" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_6" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_6" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 11 //-->
<asp:imagebutton id="imgKeyALLOWANCE_7" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_7" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_7" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_7" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 12 //-->
<asp:imagebutton id="imgKeyALLOWANCE_8" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_8" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_8" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_8" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 13 //-->
<asp:imagebutton id="imgKeyALLOWANCE_9" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_9" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_9" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_9" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 14 //-->
<asp:imagebutton id="imgKeyALLOWANCE_10" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_10" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_10" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_10" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 15 //-->
<asp:imagebutton id="imgKeyALLOWANCE_11" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_11" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_11" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_11" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 16 //-->
<asp:imagebutton id="imgKeyALLOWANCE_12" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_12" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_12" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_12" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 17 //-->
<asp:imagebutton id="imgKeyALLOWANCE_13" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_13" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_13" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_13" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 18 //-->
<asp:imagebutton id="imgKeyALLOWANCE_14" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_14" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_14" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_14" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 19 //-->
<asp:imagebutton id="imgKeyALLOWANCE_15" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_15" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_15" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_15" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 20 //-->
<asp:imagebutton id="imgKeyALLOWANCE_16" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_16" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_16" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_16" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 21 //-->
<asp:imagebutton id="imgKeyALLOWANCE_17" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_17" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_17" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_17" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 22 //-->
<asp:imagebutton id="imgKeyALLOWANCE_18" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_18" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_18" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_18" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 23 //-->
<asp:imagebutton id="imgKeyALLOWANCE_19" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_19" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_19" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_19" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 24 //-->
<asp:imagebutton id="imgKeyALLOWANCE_20" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_20" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_20" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_20" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 25 //-->
<asp:imagebutton id="imgKeyALLOWANCE_21" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_21" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_21" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_21" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 26 //-->
<asp:imagebutton id="imgKeyALLOWANCE_22" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_22" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_22" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_22" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 27 //-->
<asp:imagebutton id="imgKeyALLOWANCE_23" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_23" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_23" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_23" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 28 //-->
<asp:imagebutton id="imgKeyALLOWANCE_24" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_24" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_24" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_24" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 29 //-->
<asp:imagebutton id="imgKeyALLOWANCE_25" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_25" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_25" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_25" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 30 //-->
<asp:imagebutton id="imgKeyALLOWANCE_26" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_26" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_26" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_26" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 31 //-->
<asp:imagebutton id="imgKeyALLOWANCE_27" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_27" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_27" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_27" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 32 //-->
<asp:imagebutton id="imgKeyALLOWANCE_28" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_28" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_28" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_28" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 33 //-->
<asp:imagebutton id="imgKeyALLOWANCE_29" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_29" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_29" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_29" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 34 //-->
<asp:imagebutton id="imgKeyALLOWANCE_30" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_30" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_30" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_30" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 35 //-->
<asp:imagebutton id="imgKeyALLOWANCE_31" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_31" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_31" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_31" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 36 //-->
<asp:imagebutton id="imgKeyALLOWANCE_32" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_32" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_32" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_32" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 37 //-->
<asp:imagebutton id="imgKeyALLOWANCE_33" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_33" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_33" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_33" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 38 //-->
<asp:imagebutton id="imgKeyALLOWANCE_34" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_34" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_34" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_34" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 39 //-->
<asp:imagebutton id="imgKeyALLOWANCE_35" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblALLOWANCE_35" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtALLOWANCE_35" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnALLOWANCE_35" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 40 //-->
<asp:imagebutton id="imgKeyEPF_EMPLOYEE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblEPF_EMPLOYEE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtEPF_EMPLOYEE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnEPF_EMPLOYEE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 41 //-->
<asp:imagebutton id="imgKeyPCB" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblPCB" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtPCB" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnPCB" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 42 //-->
<asp:imagebutton id="imgKeyCP38" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCP38" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCP38" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCP38" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 43 //-->
<asp:imagebutton id="imgKeyZAKAT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblZAKAT" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtZAKAT" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnZAKAT" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 44 //-->
<asp:imagebutton id="imgKeyJOIN_DATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblJOIN_DATE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtJOIN_DATE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnJOIN_DATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 45 //-->
<asp:imagebutton id="imgKeyRESIGN_DATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblRESIGN_DATE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtRESIGN_DATE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnRESIGN_DATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 46 //-->
<asp:imagebutton id="imgKeyPREVIOUS_COMPANY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblPREVIOUS_COMPANY" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtPREVIOUS_COMPANY" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnPREVIOUS_COMPANY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 47 //-->
<asp:imagebutton id="imgKeyPREVIOUS_ADDR" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblPREVIOUS_ADDR" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtPREVIOUS_ADDR" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnPREVIOUS_ADDR" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 48 //-->
<asp:imagebutton id="imgKeyCOMPANY_NAME" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCOMPANY_NAME" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCOMPANY_NAME" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCOMPANY_NAME" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 49 //-->
<asp:imagebutton id="imgKeyCOMPANY_ADDR" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCOMPANY_ADDR" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCOMPANY_ADDR" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCOMPANY_ADDR" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 50 //-->
<asp:imagebutton id="imgKeyHUSBAND_NAME" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblHUSBAND_NAME" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtHUSBAND_NAME" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnHUSBAND_NAME" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 51 //-->
<asp:imagebutton id="imgKeyHUSBAND_IC" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblHUSBAND_IC" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtHUSBAND_IC" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnHUSBAND_IC" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 52 //-->
<asp:imagebutton id="imgKeyHUSBAND_PCB_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblHUSBAND_PCB_NO" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtHUSBAND_PCB_NO" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnHUSBAND_PCB_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 53 //-->
<asp:imagebutton id="imgKeyNO_OF_CHILD" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblNO_OF_CHILD" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtNO_OF_CHILD" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnNO_OF_CHILD" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 54 //-->
<asp:imagebutton id="imgKeyCOMPANY_PCB_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCOMPANY_PCB_NO" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCOMPANY_PCB_NO" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCOMPANY_PCB_NO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 55 //-->
<asp:imagebutton id="imgKeyBONUS_FROM" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblBONUS_FROM" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtBONUS_FROM" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnBONUS_FROM" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 56 //-->
<asp:imagebutton id="imgKeyBONUS_TO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblBONUS_TO" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtBONUS_TO" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnBONUS_TO" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 57 //-->
<asp:imagebutton id="imgKeyOTHER_SALARY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOTHER_SALARY" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtOTHER_SALARY" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnOTHER_SALARY" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 58 //-->
<asp:imagebutton id="imgKeyCAR_DATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCAR_DATE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCAR_DATE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCAR_DATE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 59 //-->
<asp:imagebutton id="imgKeyCAR_TYPE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCAR_TYPE" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCAR_TYPE" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCAR_TYPE" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 60 //-->
<asp:imagebutton id="imgKeyCAR_YEAR" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCAR_YEAR" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCAR_YEAR" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCAR_YEAR" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 61 //-->
<asp:imagebutton id="imgKeyCAR_MODEL" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblCAR_MODEL" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtCAR_MODEL" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnCAR_MODEL" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 62 //-->
<asp:imagebutton id="imgKeyHOUSE_ADDRESS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblHOUSE_ADDRESS" runat="server" Width="140px" ></asp:label>
<asp:textbox id="txtHOUSE_ADDRESS" runat="server" Width="150px" ></asp:textbox>
<asp:imagebutton id="imgbtnHOUSE_ADDRESS" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

<!--// 63 //-->
<asp:imagebutton id="imgKeyOPTION_BLOCK" runat="server" Width="21px" Height="21px" ></asp:imagebutton>
<asp:label id="lblOPTION_BLOCK" runat="server" Width="140px" ></asp:label>
<asp:dropdownlist id="ddlOPTION_BLOCK" runat="server" Width="150px" ></asp:dropdownlist>
<asp:imagebutton id="imgbtnOPTION_BLOCK" runat="server" Width="21px" Height="21px" ></asp:imagebutton>

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
    <asp:imagebutton id="imgBtnDelete1" runat="server" Height="21px" Width="74px" OnClientClick="return confirm('Confirmation! Delete Data(s) ?')"></asp:imagebutton>
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
                            <table id="Table5" cellspacing="0" cellpadding="0" border="0" runat="server">
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
												</asp:GridView>
											</asp:Panel>
											<asp:SqlDataSource ID="myDSR" runat=server></asp:SqlDataSource>
										</asp:panel>
									</td>
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
										</asp:panel>
									</td>
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
										</asp:panel>
									</td>
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
