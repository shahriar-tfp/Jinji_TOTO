<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Report_Payroll_Yearly_Details.aspx.vb" Inherits="Pages_Reports_Report_Payroll_Yearly_Details" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>JINJI : Report - Payroll Yearly Details Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="REPORT_PAYROLL_YEARLY_DETAILS" runat="server">
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
    
    <asp:ImageButton ID="imgKeyYEAR" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblYEAR" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlYEAR" runat="server" Width="150px"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnYEAR" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOPTION_PAY_CYCLE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_PAY_CYCLE" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_PAY_CYCLE" runat="server" Width="157px"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_PAY_CYCLE" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOPTION_GROUP" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOPTION_GROUP" runat="server" Width="120px"></asp:Label>
    <asp:DropDownList ID="ddlOPTION_GROUP" runat="server" Width="157px"></asp:DropDownList>
    <asp:ImageButton ID="imgbtnOPTION_GROUP" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyGROUP_CODE" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblGROUP_CODE" runat="server" Width="120px"></asp:Label>
    <asp:TextBox ID="txtGROUP_CODE" runat="server" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="imgbtnGROUP_CODE" runat="server" Width="21px" Height="21px"/>
    <asp:ImageButton ID="imgbtnGROUP_CODE2" runat="server" Width="21px" Height="21px"/>
    
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

    <asp:imagebutton id="imgBtnPrint" runat="server" Width="74" Height="21" ></asp:imagebutton>
    <asp:imagebutton id="imgBtnAddAll" runat="server" Width="24" Height="20"></asp:imagebutton>
    <asp:imagebutton id="imgBtnAddItem" runat="server" Width="24" Height="20"></asp:imagebutton>
    <asp:imagebutton id="imgBtnRemoveAll" runat="server" Width="24" Height="20"></asp:imagebutton>
    <asp:imagebutton id="imgBtnRemoveItem" runat="server" Width="24" Height="20"></asp:imagebutton>
    <asp:label id="lblresult2" runat="server" Height="22" Visible="False"></asp:label>
    <asp:Image ID="imgBottom" runat="server" Visible="false" />
    </asp:Panel>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
