<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TD_UserCourseCalendar.aspx.vb" Inherits="Pages_Training_TD_UserCourseCalendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>JINJI : Training - User Course Calendar Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server">
    <form id="TD_UserCourseCalendar" runat="server">
    <div>
    <table id="Table1" style="LEFT: 10px; POSITION:absolute; TOP: 10px" width="100%" cellspacing="0" cellpadding="0" border="0" runat="server">
        <tr>
			<td>
			    <table id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                    <tr>
                        <td>
                            <asp:panel id="pnlDescription" runat="server" >
                                <table id="Table6" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                                    <tr>
                                        <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); width :5px" ></td>
                                        <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); vertical-align:bottom">
                                        <asp:Label ID="lblTitle" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </asp:panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <asp:Panel ID="pnlCalendar" runat="server">
                             <br />
                                <table  cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td >
                                            <table cellpadding="0" cellspacing="0" border="0" width="98%">
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lblYear" runat="server" CssClass="wordstyle3" Width="140px">Year</asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" Width="157px"></asp:DropDownList>
                                                    </td>
                                                    <td align="left">&nbsp;</td>
                                                    <td align="left">&nbsp;</td>
                                                    <td align="left">
                                                        <asp:Label ID="lblMonth" runat="Server" CssClass="wordstyle3" Width="140px">Month</asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" Width="157px"></asp:DropDownList>
                                                    </td>
                                                    <td align="left">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="7">
                                                    <br />
                                                        <asp:calendar ID="calEventCalendar" runat="server" ShowGridLines="true" NextPrevFormat="FullMonth"   
                                                            ShowTitle="true" DayNameFormat="Full" SelectionMode="Day" Width="100%" cellpadding="2"
                                                            CssClass="wordstyle" BorderColor="#3333ff" FirstDayOfWeek="Monday" BackColor="#EFD5FF">
                                                            <TitleStyle BackColor="#004A95" ForeColor="#FFFFFF" Font-Bold="true" />
                                                            <NextPrevStyle BackColor="#004A95" ForeColor="#FFFFFF" Font-Bold="true" />
                                                            <TodayDayStyle BackColor="#FFD8D7" ForeColor="#00009D" />
                                                            <DayStyle BackColor="#E6FAFF" ForeColor="#0000cc" HorizontalAlign="Right" />
                                                            <WeekendDayStyle BackColor="#E6FAFF" ForeColor="#0000cc" />
                                                            <OtherMonthDayStyle BackColor="#EEEEEE" ForeColor="#33cccc" />
                                                            <SelectorStyle BackColor="#73A9FB" ForeColor="#33cccc" />
                                                        </asp:calendar>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                             </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label id="lblResult" runat="server" CssClass="wordstyle2"></asp:Label></td></tr>
                </table>
            </td> 
        </tr>         
    </table> 
    </div>
</form>
</body>
</html>

