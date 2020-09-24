<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TimePicker.aspx.vb" Inherits="Pages_Global_TimePicker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Global : Time Picker Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="TimePicker" runat="server">
    <div>
    <asp:Panel ID="pnlCalendar" runat="server">
        <center>
                <asp:Label ID="lblError" runat="server" CssClass="wordstyle4"></asp:Label>
        </center>
    </asp:Panel>
                <table cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td colspan="2" height="5"></td>
                    </tr>
                    <tr>
                        <td align="left" ><asp:Label id="lblHour" text="Hour" width="60px" runat="server"></asp:Label></td>
                        <td align="center" ><asp:DropDownList id="ddlHour" width="70px" runat="server" ></asp:DropDownList></td>
                    </tr> 
                    <tr>
                        <td height ="25" align="left"><asp:Label id="lblMinute" text="Minute" width="60px" runat="server"></asp:Label></td>
                        <td height ="25" align="center" ><asp:DropDownList id="ddlMinute" width="70px" runat="server" ></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td align="left" ><asp:Label id="lblSecond" text="Second" width="60px" runat="server"></asp:Label></td>
                        <td align="center" ><asp:DropDownList id="ddlSecond" width="70px" runat="server"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td height ="25" align="left" ><asp:Label id="lblAmPm" text="AM/PM" width="60px" runat="server"></asp:Label></td>
                        <td height ="25" align="center" ><asp:DropDownList id="ddlAmPm" width="70px" runat="server" ></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" height="30"><asp:ImageButton id="imgBtnSelect" height="21px" width="74px" runat="server" /></td>
                    </tr>
                </table>
	<asp:Literal id="ltrDate" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>

