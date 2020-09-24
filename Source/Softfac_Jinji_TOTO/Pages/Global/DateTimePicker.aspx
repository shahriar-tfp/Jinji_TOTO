<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DateTimePicker.aspx.vb" Inherits="Pages_Global_DateTimePicker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Global : Date Time Picker Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="DateTimePicker" runat="server">
    <div>
	<asp:Panel ID="pnlCalendar" runat="server">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr style="height:30px" valign="top">
                <td align="left">
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td align="right">
                    <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                     <asp:Calendar id="calCalendar" runat="server" Width="100%" Height="136px" ShowTitle="true" DayNameFormat="FirstTwoLetters" SelectionMode="Day"
                        CssClass="wordstyle" BorderColor="#3333ff" FirstDayOfWeek="Monday" BackColor="#EFD5FF">
                        <TitleStyle BackColor="#004A95" ForeColor="#FFFFFF" />
                        <NextPrevStyle BackColor="#004A95" ForeColor="#FFFFFF" />
                        <TodayDayStyle BackColor="#FFD8D7" ForeColor="#00009D" />
                        <DayStyle BackColor="#E6FAFF" ForeColor="#0000cc" HorizontalAlign="Right" />
                        <WeekendDayStyle BackColor="#E6FAFF" ForeColor="#0000cc" />
                        <OtherMonthDayStyle BackColor="#EEEEEE" ForeColor="#33cccc" />
                        <SelectorStyle BackColor="#FFD8D7" ForeColor="#FFD8D7" />
                    </asp:Calendar>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center"><asp:TextBox ID="txtCalendar" Width="100%" runat="server"></asp:TextBox></td>
            </tr>
         </table>
         <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td colspan="3"></td>
            </tr>
            <tr>
                <td style="height: 25px; width:60px" align="right"><asp:Label id="lblHour" text="Hour" width="60px" runat="server"></asp:Label></td>
                <td style="width:5px"></td>
                <td style="height: 25px; width:70px" align="left"><asp:DropDownList id="ddlHour" width="70px" runat="server" AutoPostBack="true"></asp:DropDownList></td>
            </tr> 
            <tr>
                <td style="height: 25px; width:60px" align="right"><asp:Label id="lblMinute" text="Minute" width="60px" runat="server"></asp:Label></td>
                <td style="width:5px"></td>
                <td style="height: 25px; width:70px" align="left"><asp:DropDownList id="ddlMinute" width="70px" runat="server" AutoPostBack="true"></asp:DropDownList></td>
            </tr>
            <asp:Panel ID="pnlsecond" runat="server" Visible="false" >
                <tr>
                    <td style="height: 25px; width:60px" align="right"><asp:Label id="lblSecond" text="Second" width="60px" runat="server"></asp:Label></td>
                    <td style="width:5px"></td>
                    <td style="height: 25px; width:70px" align="left"><asp:DropDownList id="ddlSecond" width="70px" runat="server" AutoPostBack="true"></asp:DropDownList></td>
                </tr>
            </asp:Panel>
            <tr>
                <td style="height: 25px; width:60px" align="right"><asp:Label id="lblAmPm" text="AM/PM" width="60px" runat="server"></asp:Label></td>
                <td style="width:5px"></td>
                <td style="height: 25px; width:70px" align="left"><asp:DropDownList id="ddlAmPm" width="70px" runat="server" AutoPostBack="true"></asp:DropDownList></td>
            </tr>
         </table>
         <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="center"><asp:ImageButton id="imgBtnSelect" height="21px" width="74px" runat="server" /></td>
            </tr>
            <tr>
                <td align="center"><asp:Label ID="lblError" runat="server" CssClass="wordstyle4"></asp:Label></td>
            </tr>
         </table>  
    </asp:Panel>
    <asp:Literal id="ltrDate" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
