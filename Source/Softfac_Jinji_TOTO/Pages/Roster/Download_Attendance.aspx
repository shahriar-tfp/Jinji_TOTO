<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Download_Attendance.aspx.vb" Inherits="Pages_Roster_Download_Attendance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>JINJI : Roster - Download Attendance Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body id="body" runat="server">
    <form id="Download_Attendance" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="5"><asp:Label ID="lblTitle" runat="server" Width="500px"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="5" height="15px"></td>
            </tr>
            <tr>
                <td align="left" valign="top"><asp:Label ID="lblBrowse" runat="server"></asp:Label></td> 
                <td align="left"><asp:FileUpload ID="uplFile" runat="server"/></td>
                <td align="left"><asp:Label ID="lblGroupBY" runat="server"></asp:Label></td>
                <td align="left"><asp:DropDownList ID="ddlGroupBy" runat="server" Width="157px" AutoPostBack="true"></asp:DropDownList></td>
                <td align="left" colspan="3"></td>
            </tr>
            <tr><td colspan="2"></td>
            <td colspan="2"><asp:label id="lbllstleft" runat="server" Width="50px"></asp:label></td>
            <td colspan="1"></td>
            <td colspan="2"><asp:label id="lbllstright" runat="server" Width="50px"></asp:label>
            </tr>
            <tr>
                <td colspan="2"><asp:ListBox ID="lstTextFile" runat="server" SelectionMode="Multiple" Height="200px" Width="300px"></asp:ListBox></td>
                <td colspan="2" rowspan="5"><asp:ListBox ID="lstLeft" runat="server" SelectionMode="Multiple" Height="350px" Width="300px"></asp:ListBox></td>
                <td colspan="1"><asp:imagebutton id="imgBtnAddItem" runat="server" Width="24" Height="20"></asp:imagebutton></td>
                <td colspan="2"  rowspan="5"><asp:ListBox ID="lstRight" runat="server" SelectionMode="Multiple" Height="350px" Width="300px"></asp:ListBox></td>
            </tr>
            <tr>
                <td colspan="2"><asp:CheckBox ID="chkOverwrite" runat="server" Font-Size="11px" Font-Bold="true" Font-Names="Arial Unicode MS" /></td>
                <td colspan ="1"><asp:imagebutton id="imgBtnAddAll" runat="server" Width="24" Height="20"></asp:imagebutton></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:ImageButton ID="imgbtnAdd" runat="server" />
                    <asp:ImageButton ID="imgbtnDelete" runat="server" />
                </td>
                <td colspan ="1"><asp:imagebutton id="imgBtnRemoveAll" runat="server" Width="24" Height="20"></asp:imagebutton></td>
                
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Image ID="imgkeyDateStart" runat="server" Width="25px" Height="25px" />
                    <asp:Label ID="lblDateStart" runat="server" Width="170px"></asp:Label>
                    <asp:TextBox ID="txtDateStart" runat="server" Width="155px"></asp:TextBox>
                    <asp:ImageButton ID="imgbtnDateStart" runat="server" />
                </td>
                <td colspan="1"><asp:imagebutton id="imgBtnRemoveItem" runat="server" Width="24" Height="20"></asp:imagebutton></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Image ID="imgkeyDateEnd" runat="server" Width="25px" Height="25px" />
                    <asp:Label ID="lblDateEnd" runat="server" Width="170px"></asp:Label>
                    <asp:TextBox ID="txtDateEnd" runat="server" Width="155px"></asp:TextBox>
                    <asp:ImageButton ID="imgbtnDateEnd" runat="server" />
                </td>
                <td colspan="1" height="5px"></td>
            </tr>
            <tr>
                <td colspan="2" height="5px"></td>
                <td colspan="2" height="5px"></td>
                <td colspan="1" height="5px"></td>
                <td colspan="2" height="5px"></td>
            </tr>
        </table>
        <table>
            <tr>
                <td width="95px"><asp:ImageButton ID="imgbtnProcess" runat="server" /></td>
                <td width="200px">Any amended TMS, Shift and Calendar Please press below button to reprocess!<asp:ImageButton ID="imgbtnReProcess" runat="server" /></td>
                <td><asp:Label ID="lblMessage" runat="server"></asp:Label></td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>
