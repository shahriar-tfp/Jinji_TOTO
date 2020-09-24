<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FolderList.aspx.vb" Inherits="FolderList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
	<link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet"/>	    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DataList id="fileList" runat="server"
            BorderColor="black"
            CellPadding="3"
            Font-Names="Verdana"
            Font-Size="8pt"
            HeaderStyle-BackColor="#aaaadd"
            AlternatingItemStyle-BackColor="Gainsboro"
            OnItemCommand="MyDataList_ItemCommand"
            DataKeyField="FullName"
            >
              <HeaderTemplate>
                <table border="0" style="width:600px">
                    <tr>
                        <td align="left" style="width:40%">File Name</td>
                        <td align="center" style="width:30%">Last Write Time</td>
                        <td align="center" style="width:20%">Size(byte)</td>
                        <td align="center" style="width:10%">Download</td>
                    </tr>
                </table>
              </HeaderTemplate>              
              <ItemTemplate>
                <table border="0" style="width:600px">
                    <tr>
                        <td align="left" style="width:40%">
                            <%#DataBinder.Eval(Container.DataItem, "Name")%>
                        </td>
                        <td align="center" style="width:30%">
                            <%#DataBinder.Eval(Container.DataItem, "LastWriteTime")%>
                        </td>
                        <td align="center" style="width:20%">
                            <%#DataBinder.Eval(Container.DataItem, "Length")%>                        
                        </td>
                        <td align="center" style="width:10%">
                            <asp:linkbutton ID="Linkbutton1" Text="Download" CommandName="Download" runat="server" CssClass="button_style"/>
                        </td>                        
                    </tr>
                </table>              
              </ItemTemplate>
        </asp:DataList>
        
    </div>
    </form>
</body>
</html>
