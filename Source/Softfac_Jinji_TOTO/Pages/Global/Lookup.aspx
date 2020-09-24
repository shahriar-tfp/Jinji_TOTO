<%@ Page Language="VB" AutoEventWireup="false" EnableEventValidation="false" CodeFile="Lookup.aspx.vb" Inherits="Pages_Global_Lookup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Global : Lookup Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
  .hiddencol
  {
    display: none;
  }
</style>
</head>
<body>
    <form id="Lookup" runat="server">
    <div>
        <asp:literal id="ltrValue" runat="server"></asp:literal>
		<div style="width: 340px; overflow: auto; border-top-style: double; border-right-style: double; border-left-style: double; border-bottom-style: double;border-color :#00115C;" align="center">	
		    <table align="left">
<%--                <tr>
		            <td>
		                <asp:ImageButton ID="imgbtnSearch" runat="server" />
		                <asp:ImageButton ID="imgbtnCancel" runat="server" />
		            </td>
		        </tr>--%>
		        <tr>
		            <td>
		                <asp:Label ID="lblSearchCode" runat="server"></asp:Label>
		                <asp:TextBox ID="txtSearchCode" runat="server"></asp:TextBox>
		                <asp:Label ID="lblSearchName" runat="server"></asp:Label>
		                <asp:TextBox ID="txtSearchName" runat="server"></asp:TextBox>
		                <asp:ImageButton ID="imgbtnFilter" runat="server" Width="25px" Height="25px" />
		            </td>
		        </tr>
		        <tr>
		            <td>
		                <asp:gridview id="grdLookup" Width="100%" runat="server" cellpadding="4" AllowPaging="True" AutoGenerateColumns="False"
				            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" EmptyDataText="No records found!" >
				            <SelectedRowStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999" />
				            <RowStyle Font-Names="Tahoma" ForeColor="#003399" CssClass="fontSize10" BackColor="White" />
				            <HeaderStyle Font-Names="Tahoma" Font-Bold="True" Height="17px" ForeColor="#FFFFFF" CssClass="fontSize12" BackColor="#003399"></HeaderStyle>
				            <FooterStyle Height="15px" />
				            <PagerSettings Mode="NumericFirstLast" />
				            <PagerStyle BackColor="#003399" ForeColor="#FFFFFF" Height="15px" CssClass="fontSize12" />
				            <Columns>
					            <asp:ButtonField Visible="false" Text="Select" CommandName="Select"/>
					            <asp:BoundField HeaderText="Display" DataField="Display" Visible="false" SortExpression="Display"  />
					            <asp:BoundField HeaderText="CodeName" DataField="CodeName" ItemStyle-HorizontalAlign="Left" SortExpression="CodeName" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
					            <asp:BoundField HeaderText="Code" DataField="Code" ItemStyle-HorizontalAlign="Left" SortExpression="Code"/>
					            <asp:BoundField HeaderText="Name" DataField="Name" ItemStyle-HorizontalAlign="Left" SortExpression="Name"/>
				            </Columns>
		                </asp:gridview>
		            </td>
		        </tr>
		        <tr>
		            <td>
		                <asp:Label ID="lblMessage" runat="server"></asp:Label>
		            </td>
		        </tr>
		    </table>
<%--		    <table align="left">
		        <tr>
		            <td>
		                <asp:ImageButton ID="imgbtnSearch" runat="server" />
		                <asp:ImageButton ID="imgbtnCancel" runat="server" />
		            </td>
		        </tr>
		        <tr>
		            <td>
		                <asp:gridview id="grdLookup" Width="100%" runat="server" cellpadding="4" AllowPaging="True" AutoGenerateColumns="False"
				            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
				            <SelectedRowStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999" />
				            <RowStyle Font-Names="Tahoma" ForeColor="#003399" CssClass="fontSize10" BackColor="White" />
				            <HeaderStyle Font-Names="Tahoma" Font-Bold="True" Height="17px" ForeColor="#FFFFFF" CssClass="fontSize12" BackColor="#003399"></HeaderStyle>
				            <FooterStyle Height="15px" />
				            <PagerSettings Mode="NumericFirstLast" />
				            <PagerStyle BackColor="#003399" ForeColor="#FFFFFF" Height="15px" CssClass="fontSize12" />
				            <Columns>
					            <asp:ButtonField Visible="false" Text="Select" CommandName="Select"/>
					            <asp:BoundField HeaderText="Display" DataField="Display" Visible="false"  />
					            <asp:BoundField HeaderText="Code" DataField="Code" ItemStyle-HorizontalAlign="Left" />
					            <asp:BoundField HeaderText="Name" DataField="Name" ItemStyle-HorizontalAlign="Left" />
				            </Columns>
		                </asp:gridview>
		            </td>
		        </tr>
		    </table>--%>
        </div>
    </div>
    </form>
</body>
</html>
