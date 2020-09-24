<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Calendar_Profile.aspx.vb" Inherits="Pages_Roster_Calendar_Profile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>JINJI : Roster - Calendar Profile Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server">
    <form id="Calendar_Profile" runat="server">
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
                                            <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td align="left" width="140px">
                                                        <asp:Label ID="lblOCP_ID_CALENDAR" runat="Server" Width="140px" CssClass="wordstyle3"></asp:Label>
                                                    </td>
                                                    <td align="left" width="150px">
                                                        <asp:TextBox ID="txtOCP_ID_CALENDAR" runat="server" Width="150px" ></asp:TextBox>
                                                    </td>   
                                                    <td align="left" width="25px">  
                                                        <asp:ImageButton ID="imgbtnOCP_ID_CALENDAR" runat="server" Height="21px" />
                                                    </td>   
                                                    <td style ="width:14px"></td> 
                                                    <td align="left" width="140px">     
                                                        <asp:Label ID="lblOptionType" runat="server" CssClass="wordstyle3" Width="140px"></asp:Label>
                                                    </td>   
                                                    <td align="left" width="150px">                                                         
                                                        <asp:DropDownList ID="ddlOptionType" runat="server" Width="157px" AutoPostBack="true"></asp:DropDownList>
                                                    </td> 
                                                    <td align="left" width="50px">&nbsp;</td>   
                                                </tr> 
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
                        <td style ="height:15px"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlEdit" runat="server">
                                <asp:Panel ID="pnlTypeA" Visible="false" runat="server">
                                    <table  cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="left" width="100px">
                                                <asp:Label ID="lblTypeA1" CssClass="wordstyle3" Width="100px" Runat="server"></asp:Label>
                                            </td>
                                            <td align="left" width="40px">
                                                <asp:TextBox ID="txtTypeA1" Width="30px" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left" width="150px">
                                                <asp:DropDownList ID="ddlTypeA1" runat="server" Width="157px" AutoPostBack="false"></asp:DropDownList>
                                            </td>
                                            <td style ="width:25px">&nbsp;</td> 
                                            <td align="left" width="100px">
                                                <asp:Label ID="lblTypeA2" CssClass="wordstyle3" Width="100px" Runat="server"></asp:Label>
                                            </td>
                                            <td align="left" width="40px">
                                                <asp:TextBox ID="txtTypeA2" Width="30px" runat="server"></asp:TextBox>
                                            </td>    
                                            <td align="left" width="150px">    
                                                <asp:DropDownList ID="ddlTypeA2" runat="server" Width="157px" AutoPostBack="false"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" width="100px">
                                                <asp:Label ID="lblTypeA3" CssClass="wordstyle3" Width="100px" Runat="server"></asp:Label>
                                            </td>
                                            <td align="left" width="40px">
                                                <asp:TextBox ID="txtTypeA3" Width="30px" runat="server"></asp:TextBox>
                                            </td>
                                            <td align="left" width="150px">    
                                                <asp:DropDownList ID="ddlTypeA3" runat="server" Width="157px" AutoPostBack="false"></asp:DropDownList>
                                            </td>
                                            <td colspan="4">&nbsp;</td> 
                                        </tr>
                                    </table> 
                                </asp:Panel>
                                <asp:Panel ID="pnlTypeB" Visible="false" runat="server">
                                    <table  cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="left" width="20px"><asp:CheckBox ID="chkMon" runat="server" /></td> 
                                            <td align="left" width="120px"><asp:Label ID="lblMon" CssClass="wordstyle3" Width="120px" Runat="server"></asp:Label></td> 
                                            <td align="left" width="150px"><asp:DropDownList ID="ddlMon" runat="server" Width="157px" AutoPostBack="false"></asp:DropDownList></td> 
                                            <td style ="width:25px"></td> 
                                            <td align="left" width="20px"><asp:CheckBox ID="chkTue" runat="server" /></td> 
                                            <td align="left" width="120px"><asp:Label ID="lblTue" CssClass="wordstyle3" Width="120px" Runat="server"></asp:Label></td> 
                                            <td align="left" width="150px"><asp:DropDownList ID="ddlTue" runat="server" Width="157px" AutoPostBack="false"></asp:DropDownList></td> 
                                        </tr>
                                        <tr>
                                            <td><asp:CheckBox ID="chkWed" runat="server" /></td> 
                                            <td><asp:Label ID="lblWed" CssClass="wordstyle3" Width="120px" Runat="server"></asp:Label></td> 
                                            <td><asp:DropDownList ID="ddlWed" runat="server" Width="157px" AutoPostBack="false"></asp:DropDownList></td> 
                                            <td></td> 
                                            <td><asp:CheckBox ID="chkThu" runat="server" /></td> 
                                            <td><asp:Label ID="lblThu" CssClass="wordstyle3" Width="120px" Runat="server"></asp:Label></td> 
                                            <td><asp:DropDownList ID="ddlThu" runat="server" Width="157px" AutoPostBack="false"></asp:DropDownList></td> 
                                        </tr>
                                        <tr>
                                            <td><asp:CheckBox ID="chkFri" runat="server" /></td> 
                                            <td><asp:Label ID="lblFri" CssClass="wordstyle3" Width="120px" Runat="server"></asp:Label></td> 
                                            <td><asp:DropDownList ID="ddlFri" runat="server" Width="157px" AutoPostBack="false"></asp:DropDownList></td> 
                                            <td></td> 
                                            <td><asp:CheckBox ID="chkSat" runat="server" /></td> 
                                            <td><asp:Label ID="lblSat" CssClass="wordstyle3" Width="120px" Runat="server"></asp:Label></td> 
                                            <td><asp:DropDownList ID="ddlSat" runat="server" Width="157px" AutoPostBack="false"></asp:DropDownList></td> 
                                        </tr>
                                        <tr>
                                            <td><asp:CheckBox ID="chkSun" runat="server" /></td> 
                                            <td><asp:Label ID="lblSun" CssClass="wordstyle3" Width="120px" Runat="server"></asp:Label></td> 
                                            <td><asp:DropDownList ID="ddlSun" runat="server" Width="157px" AutoPostBack="false"></asp:DropDownList></td> 
                                            <td colspan="4">&nbsp;</td> 
                                        </tr> 
                                    </table>         
                                </asp:Panel>
                                <asp:Panel ID="pnlTypeC" Visible="false" runat="server">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="left" width="140px">
                                                <asp:Label ID="lblFromDate" runat="server" Text="From Date" CssClass="wordstyle3" Width="140px"></asp:Label>
                                            </td>
                                            <td align="left" width="150px">
                                                <asp:TextBox ID="txtFromDate" runat="server" Width="150px" ></asp:TextBox>
                                            </td>
                                            <td align="left" width="25px">
                                                <asp:ImageButton ID="imgbtnFromDate" runat="server" />
                                            </td>    
                                            <td style ="width:14px"></td> 
                                            <td align="left" width="140px">
                                                <asp:Label ID="lblToDate" runat="server" CssClass="wordstyle3" Text="To Date" Width="140px"></asp:Label>
                                            </td>
                                            <td align="left" width="150px">
                                                <asp:TextBox ID="txtToDate" runat="server" Width="150px" ></asp:TextBox>
                                            </td>
                                            <td align="left" width="50px">
                                                <asp:ImageButton ID="imgbtnToDate" runat="server" />
                                            </td> 
                                        </tr>
                                    </table>       
                                </asp:Panel>
                                <asp:Panel ID="pnlTypeD" Visible="false" runat="server">
                                    <table  cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="left" width="140px">
                                                <asp:Label ID="lblOCP_ID_DAY_TYPE" runat="server" CssClass="wordstyle3" Text="Day Type" Width="140px"></asp:Label>
                                            </td>
                                            <td align="left" width="150px">
                                                <asp:TextBox ID="txtOCP_ID_DAY_TYPE" runat="server" Width="150px" ></asp:TextBox>
                                            </td>
                                            <td align="left" width="25px">
                                                <asp:ImageButton ID="imgbtnOCP_ID_DAY_TYPE" runat="server" Height="21px" />
                                            </td> 
                                            <td colspan="4">&nbsp;</td>
                                        </tr>
                                    </table> 
                                </asp:Panel>     
                                <asp:Panel ID="pnlTypeE" Visible="false" runat="server">
                                    <table  cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td style ="height:15px"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnSubmit" runat="server"  OnClientClick="return confirm('Are you sure you want to submit?')" />
                                                <asp:ImageButton ID="btnCancel" runat="server" />
                                                <asp:Label id="lblResult" runat="server" CssClass="wordstyle2"></asp:Label>
                                            </td>
                                        </tr> 
                                    </table> 
                                </asp:Panel>           
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td> 
        </tr>         
    </table> 
    </div>
</form>
</body>
</html>

