<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CalendarEvent.aspx.vb" Inherits="Pages_Global_Calendar_Event" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Global : Calendar Event Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="CalendarEvent" runat="server">
    <div>
    <table>
				<tr>
					<td>
						<asp:Calendar id="calEvents" runat="server" BackColor="White" Width="250px" DayNameFormat="FirstLetter"
							ForeColor="Black" Height="200px" Font-Size="8pt" Font-Names="Verdana" BorderColor="#999999"
							CellPadding="4">
							<TodayDayStyle ForeColor="Black" BackColor="#CCCCCC"></TodayDayStyle>
							<SelectorStyle BackColor="#CCCCCC"></SelectorStyle>
							<NextPrevStyle VerticalAlign="Bottom"></NextPrevStyle>
							<DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#CCCCCC"></DayHeaderStyle>
							<SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#666666"></SelectedDayStyle>
							<TitleStyle Font-Bold="True" BorderColor="Black" BackColor="#999999"></TitleStyle>
							<WeekendDayStyle BackColor="#FFFFCC"></WeekendDayStyle>
							<OtherMonthDayStyle ForeColor="#808080"></OtherMonthDayStyle>
						</asp:Calendar>
					</td>
					<td>
					    <asp:GridView ID="gvEvents" runat="server" AutoGenerateColumns="false" GridLines="vertical" ForeColor="Black"
					        BorderWidth="1px" CellPadding="4" BorderStyle="None" PagerSettings-Mode="numeric">
					        <SelectedRowStyle Font-Bold="true" ForeColor="white" BackColor="#CE5D5A"/>
					        <AlternatingRowStyle BackColor="White"/>
					        <RowStyle BackColor="#F7F7DE"/>
					        <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#6B696B"/>
					        <FooterStyle BackColor="#CCCC99"/>
					        <Columns>
					            <asp:BoundField DataField="Description" HeaderText="Description" />
					            <asp:TemplateField>
					                <ItemTemplate>
					                    <asp:HyperLink ID="hlURL" runat="server" Text='<%#Container.DataItem("URL")%>' NavigateUrl='<%#Container.DataItem("URL")%>'></asp:HyperLink>
					                </ItemTemplate>
					            </asp:TemplateField>
					        </Columns>
					        <PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#F7F7DE" />
					    </asp:GridView>
					</td>
				</tr>
			</table>
    </div>
    </form>
</body>
</html>
