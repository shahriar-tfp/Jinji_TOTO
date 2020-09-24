<%@ page language="VB" autoeventwireup="false" inherits="Reports" CodeFile="Reports.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Selecting...</title>
    <link href="_StyleSheets/hcrmStyles1.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="JavaScript">
        window.onload = maxWindow;
//        window.onbeforeunload = confirmExit;
        function confirmExit()
        {
            return confirm('Are you sure you want to exit?');
        }
    
        function maxWindow()
        {
            window.moveTo(0,0);
            if (document.all)
            {
                top.window.resizeTo(screen.availWidth,screen.availHeight);
            }
            else if (document.layers||document.getElementById)
            {
                if (top.window.outerHeight<screen.availHeight||top.window.outerWidth<screen.availWidth)
                {
                    top.window.outerHeight = screen.availHeight;
                    top.window.outerWidth = screen.availWidth;
                }
            }
        }
    </script>
</head>
<body>
    <form id="Default" runat="server">
    <div >
        <asp:Panel ID="pnlFilterField" runat="server" >
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server"/>
                        <asp:ImageButton ID="imgbtnPreview" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSelect" runat="server" Text="Please check to select criteria before proceed!" ForeColor="#0000ff"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:CheckBox ID="chkSelectAll" runat="server" Text="Check All" AutoPostBack="true" />
                    <asp:CheckBox ID="chkDeselectAll" runat="server" Text="Uncheck All" AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                    <asp:gridview id="myGridView" Width="100%" runat="server" AutoGenerateColumns="true"
		        cellspacing="0" cellpadding="1" EmptyDataText="No data found!">
		        <AlternatingRowStyle BackColor="#F2F4FF" />
		        <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
		        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
		        <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
		        <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#001B97" Height="21" ForeColor="#ffffcc" />
                <Columns>
                    <asp:TemplateField HeaderText="Select" ItemStyle-Width="35">
                        <ItemTemplate>
                            <center>
                            <asp:CheckBox ID="chkSelect" runat="server" Checked="true" />
                            </center>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:gridview>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
