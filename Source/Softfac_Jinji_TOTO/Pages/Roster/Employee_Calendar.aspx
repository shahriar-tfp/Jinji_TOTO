<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Employee_Calendar.aspx.vb" Inherits="Pages_Roster_Employee_Calendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>JINJI : Roster - Employee Calendar Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server">
    <form id="Employee_Calendar" runat="server">
        <div>
    	<asp:panel id="pnlDescription" runat="server" >
            <table id="Table6" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                <tr>
                    <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); width :5px" ></td>
                    <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); vertical-align:bottom">
                    <asp:Label ID="lblTitle" runat="server"></asp:Label></td>
                </tr>
            </table>
        </asp:panel>
     <asp:Panel ID="pnlCalendar" runat="server">
     <br />
        <table  cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td >
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td align="left" width="140px"><asp:Label ID="lblEMPLOYEE_PROFILE_ID" runat="Server" Text="Calendar Type" Width="140px" CssClass="wordstyle3"></asp:Label></td> 
                            <td align="left" width="150px"><asp:TextBox ID="txtEMPLOYEE_PROFILE_ID" runat="server" Width="150px" ></asp:TextBox></td>
                            <td align="left" width="25px"><asp:ImageButton ID="imgbtnEMPLOYEE_PROFILE_ID" runat="server" Height="21px" /></td>
                            <td align="left" width="14px"></td> 
                            <td align="left" width="140px"></td> 
                            <td align="left" width="150px"></td>
                            <td align="left" width="25px"></td>
                        </tr> 
                        <tr>
                            <td align="left" width="140px"><asp:Label ID="lblYear" runat="server" CssClass="wordstyle3" Width="140px">Year</asp:Label></td> 
                            <td align="left" width="150px"><asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" Width="157px"></asp:DropDownList></td>
                            <td align="left" width="25px"></td>
                            <td align="left" width="14px"></td> 
                            <td align="left" width="140px"><asp:Label ID="lblMonth" runat="Server" CssClass="wordstyle3" Width="140px">Month</asp:Label></td> 
                            <td align="left" width="150px"><asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" Width="157px"></asp:DropDownList></td>
                            <td align="left" width="25px"></td>
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
     <br />
     <asp:Panel ID="pnlEdit" runat="server">
        <table  cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td align="left" width="140px"><asp:Label ID="lblFromDate" runat="server" Text="From Date" CssClass="wordstyle3" Width="140px"></asp:Label></td> 
                <td align="left" width="150px"><asp:TextBox ID="txtFromDate" runat="server" Width="150px" ></asp:TextBox></td>
                <td align="left" width="25px"><asp:ImageButton ID="imgbtnFromDate" runat="server" /></td>
                <td align="left" width="14px"></td>
                <td align="left" width="140px"><asp:Label ID="lblToDate" runat="server" CssClass="wordstyle3" Text="To Date" Width="140px"></asp:Label></td>
                <td align="left" width="150px"><asp:TextBox ID="txtToDate" runat="server" Width="150px" ></asp:TextBox></td>
                <td align="left" width="25px"><asp:ImageButton ID="imgbtnToDate" runat="server" /></td>
            </tr>
            <tr>
                <td align="left" width="140px"><asp:Label ID="lblOCP_ID_DAY_TYPE" runat="server" CssClass="wordstyle3" Text="Day Type" Width="140px"></asp:Label></td> 
                <td align="left" width="150px"><asp:TextBox ID="txtOCP_ID_DAY_TYPE" runat="server" Width="150px" ></asp:TextBox></td>
                <td align="left" width="25px"><asp:ImageButton ID="imgbtnOCP_ID_DAY_TYPE" runat="server" Height="21px" /></td>
                <td align="left" width="14px"></td>
                <td align="left" width="140px"></td>
                <td align="left" width="150px"></td>
                <td align="left" width="25px"></td>
            </tr>
            <tr>
                <td colspan="7" height="5px"></td>
            </tr>
        </table>                    
        <table>
            <tr>
                <td><asp:ImageButton ID="btnSubmit" runat="server" OnClientClick="return confirm('Are you sure you want to submit?')" /></td>
                <td width="95px"><asp:ImageButton ID="btnCancel" runat="server" /></td>
                <td valign="top"><asp:Label id="lblResult" runat="server" CssClass="wordstyle2" Visible="false"></asp:Label></td> 
            </tr>
        </table>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
