<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MASTERSKILL_EVOLUTIONFORM_CommonSkill.aspx.vb" Inherits="PAGES_TOTO_KPI_MASTERSKILL_EVOLUTIONFORM_CommonSkill" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"" http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>MASTERSKILL_EVOLUTIONFORM_CommonSkill</title>
<link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />

</head>
<body id="body" runat="server"> 

<form id="MASTERSKILL_EVOLUTIONFORM_CommonSkill" runat="server">
    <div>
    <asp:Panel ID="pnlPayslip" runat="server">
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
    <asp:TextBox ID="txtEMPLOYEE_PROFILE_ID" runat="server" Width="150px" AutoPostBack=true></asp:TextBox>
    <asp:ImageButton ID="imgbtnEMPLOYEE_PROFILE_ID" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOPTION_YEAR" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_YEAR" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_YEAR" runat="server" Width="150px" AutoPostBack=true></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_YEAR" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgkeyOPTION_MONTH" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_MONTH" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_MONTH" runat="server" Width="150px" AutoPostBack=true></asp:DropDownList>
    <asp:ImageButton ID="imgBtnOPTION_MONTH" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyEVALUATION" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblEVALUATION" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlEVALUATION" runat="server" Width="150px" AutoPostBack="true"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnEVALUATION" runat="server" Width="21px" Height="21px"/>
       
    <asp:ImageButton ID="imgKeyOPTION_LEVEL" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_LEVEL" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_LEVEL" runat="server" Width="150px" AutoPostBack=true></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_LEVEL" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOPTION_PROCESS" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_PROCESS" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_PROCESS" runat="server" Width="150px" AutoPostBack=true></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_PROCESS" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOPTION_CATEGORY" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_CATEGORY" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_CATEGORY" runat="server" Width="150px" AutoPostBack=true></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_CATEGORY" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyGROUP_CODE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblGROUP_CODE" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlGROUP_CODE" runat="server" Width="150px" AutoPostBack=true></asp:DropDownList>
    <asp:ImageButton ID="imgbtnGROUP_CODE" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyWEIGHTAGE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblWEIGHTAGE" runat="server" Width="120px"></asp:Label>
    <asp:textbox ID="txtWEIGHTAGE" runat="server" Width="50px" AutoPostBack=true></asp:textbox>
    <asp:ImageButton ID="imgbtnWEIGHTAGE" runat="server" Width="21px" Height="21px"/>
    	         
    <!--// listbox //-->
    <asp:panel id="pnllstleft" runat="server" Width="150px" visible="false">
    <table id="Table5" cellspacing="0" cellpadding="0" border="0" runat="server">
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
    <table id="Table8" cellspacing="0" cellpadding="0" border="0" runat="server">
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

    
    </asp:Panel>
    <asp:Panel ID="pnlGridView" runat="server">
    <div id="divGridview1" style="top: 250px;left: 15px;position :absolute; width: 98%;">
    <div style="margin-left: auto; margin-right: auto; text-align: center;">
    <asp:Label ID="lblSpecialHeader" runat="server" Text="Special Skill" Font-Bold="true"
        CssClass="StrongText"></asp:Label>
    </div>
    <table id="tblSpecialSkill" border="1" cellpadding="0" cellspacing="0" width="100%" runat="server">
    <tr><th style="width: 80%;position :absolute;"><asp:Label ID="lblSpecialSkill2" runat="server" Visible="true" Text="Skill" Height="21px"></asp:Label></th><th style="width: 15%;"><asp:Label ID="lblPass" runat="server" Height="21px" Visible="true" Text="Pass / Fail"></asp:Label></th></tr>
    <tr><td style="width: 80%;"><asp:Label ID="lblSPSkill_1" runat="server" Visible="true" Text="1."></asp:Label></td><td style="width: 15%; "><asp:DropDownList ID="ddlSPSKILL_1" runat="server" Width="150px" AutoPostBack=true  Visible="true"></asp:DropDownList></td></tr>
    <tr><td style="width: 80%;"><asp:Label ID="lblSPSkill_2" runat="server" Visible="true" Text="2."></asp:Label></td><td style="width: 15%;"><asp:DropDownList ID="ddlSPSKILL_2" runat="server" Width="150px" AutoPostBack=true Visible="true"></asp:DropDownList></td></tr>
    <tr><td style="width: 80%;"><asp:Label ID="lblSPSkill_3" runat="server" Visible="true" Text="3."></asp:Label></td><td style="width: 15%;"><asp:DropDownList ID="ddlSPSKILL_3" runat="server" Width="150px" AutoPostBack=true Visible="true"></asp:DropDownList></td></tr>
    <tr><td style="width: 80%;"><asp:Label ID="lblSPSkill_4" runat="server" Visible="true" Text="4."></asp:Label></td><td style="width: 15%;"><asp:DropDownList ID="ddlSPSKILL_4" runat="server" Width="150px" AutoPostBack=true Visible="true"></asp:DropDownList></td></tr>
    <tr><td style="width: 80%;"><asp:Label ID="lblSPSkill_5" runat="server" Visible="true" Text="5."></asp:Label></td><td style="width: 15%;"><asp:DropDownList ID="ddlSPSKILL_5" runat="server" Width="150px" AutoPostBack=true Visible="true"></asp:DropDownList></td></tr>
    <tr><td style="width: 80%;"><asp:Label ID="lblSPSkill_6" runat="server" Visible="false"></asp:Label></td><td style="width: 15%;"><asp:DropDownList ID="ddlSPSKILL_6" runat="server" Width="150px" AutoPostBack=true Visible="false"></asp:DropDownList></td></tr>
    <tr><td style="width: 80%;"><asp:Label ID="lblSPSkill_7" runat="server" Visible="false"></asp:Label></td><td style="width: 15%;"><asp:DropDownList ID="ddlSPSKILL_7" runat="server" Width="150px" AutoPostBack=true Visible="false"></asp:DropDownList></td></tr>
    <tr><td style="width: 80%;"><asp:Label ID="lblSPSkill_8" runat="server" Visible="false"></asp:Label></td><td style="width: 15%;"><asp:DropDownList ID="ddlSPSKILL_8" runat="server" Width="150px" AutoPostBack=true Visible="false"></asp:DropDownList></td></tr>
    <tr><td style="width: 80%;"><asp:Label ID="lblSPSkill_9" runat="server" Visible="false"></asp:Label></td><td style="width: 15%;"><asp:DropDownList ID="ddlSPSKILL_9" runat="server" Width="150px" AutoPostBack=true Visible="false"></asp:DropDownList></td></tr>
    <tr><td style="width: 80%;"><asp:Label ID="lblSPSkill_10" runat="server" Visible="false"></asp:Label></td><td style="width: 15%;"><asp:DropDownList ID="ddlSPSKILL_10" runat="server" Width="150px" AutoPostBack=true Visible="false"></asp:DropDownList></td></tr>
    <tr><td style="width: 80%;"><asp:Label ID="lblWTS" runat="server" Visible="true" Text="WTS (second)" Width="60%"></asp:Label><asp:textBox ID="txtActualWTS" runat="server" Visible="true" Enabled="false" AutoPostBack="true"></asp:textBox>
    <asp:HiddenField ID="SpecialSkill1" runat="server" /></td>
    <td style="width: 15%;"><asp:Label ID="lblTimer" runat="server" Width="50px" Visible="true" style="background-color:black; color:White"></asp:Label>
    <asp:HiddenField id="TimerH" runat ="server"/>
    <button id="btnStart" runat="server" onclick="myStartFunction();return false" >Start</button><button id="btnStop" runat="server" style="display: none;" onclick="myStopFunction();return false">Stop</button></td></tr>
    </table>
    
    <br />
    <table id="tblSpecialSkill2" border="1" cellpadding="0" cellspacing="0" width="100%" runat="server">
    <tr>
    <th><asp:Label ID="lblSName1" runat="server" Text="Skill"></asp:Label></th>
    <th><asp:Label ID="lblSName2" runat="server" Text="Last Year"></asp:Label></th>
    <th><asp:Label ID="lblSName3" runat="server" Text="Current"></asp:Label></th>
    <th><asp:Label ID="lblSName4" runat="server" Text="New"></asp:Label></th>
    <th><asp:Label ID="lblSName5" runat="server" Text="Evaluation"></asp:Label></th>
    </tr>
    <tr>
    <td><asp:Label ID="lblSSkill" runat="server" Width="300px" Text="Point"></asp:Label></td>
    <td><asp:TextBox ID="txtSSkillLY" runat="server" enabled="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSSkillC" runat="server" enabled="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSSkillN" runat="server" enabled="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSSkillE" runat="server" enabled="false" AutoPostBack="true" MaxLength=5></asp:TextBox>
    <asp:HiddenField ID="hfSpecialSkill" runat="server" /></td>
    </tr>
    </table>
    <script language="javascript" type="text/javascript">
        var myVar
        var t = 0.00;
        function myStartFunction() {
            document.getElementById("btnStart").style.display = "none";
            document.getElementById("btnStop").style.display = "inline";
            myVar = setInterval(function() { myTimer() }, 10);
        }

        function myTimer() {
            t = t + 0.01;
            document.getElementById("lblTimer").innerHTML = t.toFixed(2); ;
        }

        function myStopFunction() {
            document.getElementById("btnStart").style.display = "inline";
            document.getElementById("btnStop").style.display = "none";
            document.getElementById("TimerH").value = document.getElementById("lblTimer").innerHTML;
            clearInterval(myVar);
        }
</script>
<br />
<div style="margin-left: auto; margin-right: auto; text-align: center;">
    <asp:Label ID="lblCommonHeader" runat="server" Text="Common Skill" Font-Bold="true"
        CssClass="StrongText"></asp:Label>
</div>
<asp:HiddenField id="CommonSkillCount" runat ="server"/>
<asp:HiddenField id="Jobgrade" runat ="server"/>
<asp:HiddenField id="PromotionPeriod" runat ="server"/>
<asp:HiddenField id="frompoint" runat ="server"/>
<asp:HiddenField id="topoint" runat ="server"/>
<asp:HiddenField id="retentionperiod" runat ="server"/>
<asp:HiddenField id="specialperiod" runat ="server"/>
<asp:HiddenField id="levelup" runat ="server"/>
<table id="CommonSkill" border="1" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <th><asp:Label ID="lblName1" runat="server" Text="Skill"></asp:Label></th>
    <th><asp:Label ID="lblName2" runat="server" Text="Last Year"></asp:Label></th>
    <th><asp:Label ID="lblName3" runat="server" Text="Current"></asp:Label></th>
    <th><asp:Label ID="lblName4" runat="server" Text="New"></asp:Label></th>
    <th><asp:Label ID="lblName5" runat="server" Text="Evaluation"></asp:Label></th>
    </tr>
    <tr>
    <td><asp:Label ID="lblSkill1C" runat="server" Width="300px" Visible="false"></asp:Label></td>
    <td><asp:TextBox ID="txtSkill1LY" runat="server"  Visible="false" ></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill1C" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill1N" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill1E" runat="server"  Visible="false" AutoPostBack="true"></asp:TextBox>
    <asp:HiddenField ID="Skill1" runat="server" /></td>
    </tr>
    <tr>
    <td><asp:Label ID="lblSkill2C" runat="server" Width="300px"  Visible="false"></asp:Label></td>
    <td><asp:TextBox ID="txtSkill2LY" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill2C" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill2N" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill2E" runat="server"  Visible="false" AutoPostBack="true"></asp:TextBox>
    <asp:HiddenField ID="Skill2" runat="server" /></td>
    </tr>
    <tr>
    <td><asp:Label ID="lblSkill3C" runat="server" Width="300px"  Visible="false"></asp:Label></td>
    <td><asp:TextBox ID="txtSkill3LY" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill3C" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill3N" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill3E" runat="server"  Visible="false" AutoPostBack="true"></asp:TextBox>
    <asp:HiddenField ID="Skill3" runat="server" /></td>
    </tr>
    <tr>
    <td><asp:Label ID="lblSkill4C" runat="server" Width="300px"  Visible="false"></asp:Label></td>
    <td><asp:TextBox ID="txtSkill4LY" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill4C" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill4N" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill4E" runat="server"  Visible="false" AutoPostBack="true"></asp:TextBox>
    <asp:HiddenField ID="Skill4" runat="server" /></td>
    </tr>
    <tr>
    <td><asp:Label ID="lblSkill5C" runat="server" Width="300px"  Visible="false"></asp:Label></td>
    <td><asp:TextBox ID="txtSkill5LY" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill5C" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill5N" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill5E" runat="server"  Visible="false" AutoPostBack="true"></asp:TextBox>
    <asp:HiddenField ID="Skill5" runat="server" /></td>
    </tr>
    <tr>
    <td><asp:Label ID="lblSkill6C" runat="server" Width="300px"  Visible="false"></asp:Label></td>
    <td><asp:TextBox ID="txtSkill6LY" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill6C" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill6N" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill6E" runat="server"  Visible="false" AutoPostBack="true"></asp:TextBox>
    <asp:HiddenField ID="Skill6" runat="server" /></td>
    </tr>
    <tr>
    <td><asp:Label ID="lblSkill7C" runat="server" Width="300px"  Visible="false"></asp:Label></td>
    <td><asp:TextBox ID="txtSkill7LY" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill7C" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill7N" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill7E" runat="server"  Visible="false" AutoPostBack="true"></asp:TextBox>
    <asp:HiddenField ID="Skill7" runat="server" /></td>
    </tr>
    <tr>
    <td><asp:Label ID="lblSkill8C" runat="server" Width="300px"  Visible="false"></asp:Label></td>
    <td><asp:TextBox ID="txtSkill8LY" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill8C" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill8N" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill8E" runat="server"  Visible="false" AutoPostBack="true"></asp:TextBox>
    <asp:HiddenField ID="Skill8" runat="server" /></td>
    </tr>
    <tr>
    <td><asp:Label ID="lblSkill9C" runat="server" Width="300px"  Visible="false"></asp:Label></td>
    <td><asp:TextBox ID="txtSkill9LY" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill9C" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill9N" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill9E" runat="server"  Visible="false" AutoPostBack="true"></asp:TextBox>
    <asp:HiddenField ID="Skill9" runat="server" /></td>
    </tr>
    <tr>
    <td><asp:Label ID="lblSkill10C" runat="server" Width="300px"  Visible="false"></asp:Label></td>
    <td><asp:TextBox ID="txtSkill10LY" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill10C" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill10N" runat="server"  Visible="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSkill10E" runat="server"  Visible="false" AutoPostBack="true"></asp:TextBox>
    <asp:HiddenField ID="Skill10" runat="server" /></td>
    </tr>
    </table>
    <br />
    <div style="margin-left: auto; margin-right: auto; text-align: center;">
    <asp:Label ID="lblTotalHeader" runat="server" Text="Total Skill Point" Font-Bold="true"
        CssClass="StrongText"></asp:Label>
    </div>
    <table id="Improvement" border="1" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <th><asp:Label ID="lblIName1" runat="server" Text="Skill"></asp:Label></th>
    <th><asp:Label ID="lblIName2" runat="server" Text="Last Year"></asp:Label></th>
    <th><asp:Label ID="lblIName3" runat="server" Text="Current"></asp:Label></th>
    <th><asp:Label ID="lblIName4" runat="server" Text="New"></asp:Label></th>
    <th><asp:Label ID="lblIName5" runat="server" Text="Improvement"></asp:Label></th>
    </tr>
    <tr>
    <td><asp:Label ID="lblSpecialSkill" runat="server" Width="300px"  text="Special Skill"></asp:Label></td>
    <td><asp:TextBox ID="txtSpecialSkillLY" runat="server"  enabled="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSpecialSkillC" runat="server"  enabled="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSpecialSkillN" runat="server"  enabled="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtSpecialSkillI" runat="server"  enabled="false"></asp:TextBox></td>
    </tr>
    <tr>
    <td><asp:Label ID="lblCommonSkill" runat="server" Width="300px"  text="Common Skill"></asp:Label></td>
    <td><asp:TextBox ID="txtCommonSkillLY" runat="Server" enabled="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtCommonSkillC" runat="Server" enabled="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtCommonSkillN" runat="Server" enabled="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtCommonSkillI" runat="Server" enabled="false"></asp:TextBox></td>
    </tr>
    <tr>
    <td><asp:Label ID="lblTotal" runat="server" Width="300px"  text="TOTAL"></asp:Label></td>
    <td><asp:TextBox ID="txtTotalLY" runat="server"  enabled="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtTotalC" runat="server"  enabled="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtTotalN" runat="server"  enabled="false"></asp:TextBox></td>
    <td><asp:TextBox ID="txtTotalI" runat="server"  enabled="false"></asp:TextBox></td>
    </tr></table>
    </div>
    <div id="divButton" style="top: 450px;left: 15px;position :absolute; width: 98%;" runat="server">
    
    <asp:imagebutton id="imgBtnUpdate" runat="server" Width="74" Height="21" ></asp:imagebutton>
    <asp:imagebutton id="imgBtnCancel" runat="server" Width="74" Height="21" ></asp:imagebutton>
    <asp:imagebutton id="imgBtnDelete" runat="server" Width="74" Height="21" OnClientClick="return confirm('Confirmation! Delete Data(s) ?')" ></asp:imagebutton>
    <asp:imagebutton id="imgBtnAddAll" runat="server" Width="24" Height="20"></asp:imagebutton>
    <asp:imagebutton id="imgBtnAddItem" runat="server" Width="24" Height="20"></asp:imagebutton>
    <asp:imagebutton id="imgBtnRemoveAll" runat="server" Width="24" Height="20"></asp:imagebutton>
    <asp:imagebutton id="imgBtnRemoveItem" runat="server" Width="24" Height="20"></asp:imagebutton>
    <asp:Image ID="imgBottom" runat="server" Visible="false" />
    <asp:label id="lblresult2" runat="server" Height="22" Visible="False"></asp:label>
    </div>
    </asp:panel>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
