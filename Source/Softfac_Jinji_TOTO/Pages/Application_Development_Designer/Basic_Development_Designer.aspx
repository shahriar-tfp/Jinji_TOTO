<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Basic_Development_Designer.aspx.vb" Inherits="Pages_Application_Development_Designer_Basic_Development_Designer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>JINJI : Basic Development Designer Page</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />

</head>
<body>
    <form id="Basic_Development_Designer" runat="server">
    <div>
    <table id="Table1" cellspacing="0" cellpadding="0" border="0" runat="server">
        <tr>
            <td style="background-image:url(../../Images/Default/gif/org_title_bar20.gif); width :5px"></td>
            <td style="background-image:url(../../Images/Default/gif/org_title_bar20.gif); vertical-align:bottom">
            <asp:Label ID="lblTitle" runat="server"></asp:Label></td>
        </tr>
    </table>
    </div>
    <br />
    <div>
        <asp:Menu ID="mnuStep" Width="216px" runat="server" Orientation="Horizontal" StaticEnableDefaultPopOutImage="False">
            <Items>
                <%--Add a MenuItem for each tab.--%>
                <asp:MenuItem Text=" Page | " Value="0" ToolTip="[Create Page]" ></asp:MenuItem>
                <asp:MenuItem Text=" Fields | " Value="1" ToolTip="[Creating Fields]" ></asp:MenuItem>
                <asp:MenuItem Text=" Option | " Value="2" ToolTip="[Creating Option]" ></asp:MenuItem>
                <asp:MenuItem Text=" Lookup | " Value="3" ToolTip="[Creating Lookup]" ></asp:MenuItem>
                <asp:MenuItem Text=" Security " Value="4" ToolTip="[Assign Permission]" ></asp:MenuItem>
            </Items>
        </asp:Menu>
        <asp:MultiView ID="mtvStep" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwPage" runat="server"  >
                <asp:Panel ID="pnlPage" runat="server" BorderStyle="Groove" Width="400px">
                    <table>
                        <tr align="right">
                            <td>
                                <asp:LinkButton ID="lnkbtnPageNext" runat="server" Text="[Next]"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitlePage" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgKeyMODULE_PROFILE_CODE" runat="server" Width="21px" Height="21px" />
                                <asp:Label ID="lblMODULE_PROFILE_CODE" runat="server" Text="Module Code" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                <asp:TextBox ID="txtMODULE_PROFILE_CODE" runat="server" Width="150px" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:TextBox>
                                <asp:ImageButton ID="imgbtnMODULE_PROFILE_CODE" runat="server" width="21px" Height="21px"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgKeyTABLE" runat="server" Width="21px" Height="21px" />
                                <asp:Label ID="lblTABLE" runat="server" Text="Page Name" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                <asp:TextBox ID="txtTABLE" runat="server" Width="150px" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:TextBox>
                                <asp:ImageButton ID="imgbtnTABLE" runat="server" width="21px" Height="21px"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPageMessage" runat="server" ForeColor="red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:View>
            <asp:View ID="vwField" runat="server">
                <asp:Panel ID="pnlField" runat="server" BorderStyle="Groove" Width="100%">
                <table>
                    <tr align="right">
                        <td>
                            <asp:LinkButton ID="lnkbtnFieldBack" runat="server" Text="[Back]"></asp:LinkButton>
                            <asp:LinkButton ID="lnkbtnFieldNext" runat="server" Text="[Next]"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        
                            <table>
                                <tr >
                                    <td>
                                        <asp:Label ID="lblTitleField" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlFieldMain" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkCompanyProfileCode" runat="server" Text="Include Company_Profile_Code" AutoPostBack="true" Font-Names="Arial Unicode MS" Font-Size="12px" />
                                        <asp:CheckBox ID="chkEmployeeProfileID" runat="server" Text="Include Employee_Profile_ID" AutoPostBack="true" Font-Names="Arial Unicode MS" Font-Size="12px" />
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgKeyFIELD_CODE" runat="server" Width="21px" Height="21px" />
                                        <asp:Label ID="lblFIELD_CODE" runat="server" Text="Field Code" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                        <asp:TextBox ID="txtFIELD_CODE" runat="server" Width="150px" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnFIELD_CODE" runat="server" width="21px" Height="21px"/>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgKeyFIELD_NAME" runat="server" Width="21px" Height="21px" />
                                        <asp:Label ID="lblFIELD_NAME" runat="server" Text="Field Name" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                        <asp:TextBox ID="txtFIELD_NAME" runat="server" Width="150px" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnFIELD_NAME" runat="server" width="21px" Height="21px"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgKeySEQUENCE_NO" runat="server" Width="21px" Height="21px" />
                                        <asp:Label ID="lblSEQUENCE_NO" runat="server" Text="Sequence No" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                        <asp:TextBox ID="txtSEQUENCE_NO" runat="server" Width="150px" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnSEQUENCE_NO" runat="server" width="21px" Height="21px"/>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgKeyOPTION_PRIMARY_KEY" runat="server" Width="21px" Height="21px"/>
                                        <asp:Label ID="lblOPTION_PRIMARY_KEY" runat="server" Text="Is Primary Key" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                        <asp:DropDownList ID="ddlOPTION_PRIMARY_KEY" runat="server" Width="155px"></asp:DropDownList>
                                        <asp:ImageButton ID="imgbtnOPTION_PRIMARY_KEY" runat="server" Width="21px" Height="21px"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgKeyOPTION_DATA_TYPE" runat="server" Width="21px" Height="21px"/>
                                        <asp:Label ID="lblOPTION_DATA_TYPE" runat="server" Text="Data Type" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                        <asp:DropDownList ID="ddlOPTION_DATA_TYPE" runat="server" Width="155px"></asp:DropDownList>
                                        <asp:ImageButton ID="imgbtnOPTION_DATA_TYPE" runat="server" Width="21px" Height="21px"/>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgKeyLENGTH" runat="server" Width="21px" Height="21px"/>
                                        <asp:Label ID="lblLENGTH" runat="server" Text="Max Length" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                        <asp:TextBox ID="txtLENGTH" runat="server" Width="150px" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnLENGTH" runat="server" Width="21px" Height="21px"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgKeyOPTION_VIEW_LIST" runat="server" Width="21px" Height="21px" />
                                        <asp:Label ID="lblOPTION_VIEW_LIST" runat="server" Text="Option View List" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                        <asp:DropDownList ID="ddlOPTION_VIEW_LIST" runat="server" Width="155px"></asp:DropDownList>
                                        <asp:ImageButton ID="imgbtnOPTION_VIEW_LIST" runat="server" width="21px" Height="21px"/>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgKeyOPTION_VIEW_CARD" runat="server" Width="21px" Height="21px" />
                                        <asp:Label ID="lblOPTION_VIEW_CARD" runat="server" Text="Option View Card" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                        <asp:DropDownList ID="ddlOPTION_VIEW_CARD" runat="server" Width="155px"></asp:DropDownList>
                                        <asp:ImageButton ID="imgbtnOPTION_VIEW_CARD" runat="server" width="21px" Height="21px"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgKeyOPTION_ENABLED" runat="server" Width="21px" Height="21px" />
                                        <asp:Label ID="lblOPTION_ENABLED" runat="server" Text="Option Enabled (Add)" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                        <asp:DropDownList ID="ddlOPTION_ENABLED" runat="server" Width="155px"></asp:DropDownList>
                                        <asp:ImageButton ID="imgbtnOPTION_ENABLED" runat="server" width="21px" Height="21px"/>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgKeyOPTION_EDITABLE" runat="server" Width="21px" Height="21px" />
                                        <asp:Label ID="lblOPTION_EDITABLE" runat="server" Text="Option Editable (Edit)" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                        <asp:DropDownList ID="ddlOPTION_EDITABLE" runat="server" Width="155px"></asp:DropDownList>
                                        <asp:ImageButton ID="imgbtnOPTION_EDITABLE" runat="server" width="21px" Height="21px"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgKeyOPTION_SET_FILTER" runat="server" Width="21px" Height="21px" />
                                        <asp:Label ID="lblOPTION_SET_FILTER" runat="server" Text="Option Set Filter" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                        <asp:DropDownList ID="ddlOPTION_SET_FILTER" runat="server" Width="155px"></asp:DropDownList>
                                        <asp:ImageButton ID="imgbtnOPTION_SET_FILTER" runat="server" width="21px" Height="21px"/>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgKeyOPTION_MANDATORY" runat="server" Width="21px" Height="21px" />
                                        <asp:Label ID="lblOPTION_MANDATORY" runat="server" Text="Option Mandatory" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                        <asp:DropDownList ID="ddlOPTION_MANDATORY" runat="server" Width="155px"></asp:DropDownList>
                                        <asp:ImageButton ID="imgbtnOPTION_MANDATORY" runat="server" width="21px" Height="21px"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgKeyOPTION_PASSWORD" runat="server" Width="21px" Height="21px" />
                                        <asp:Label ID="lblOPTION_PASSWORD" runat="server" Text="Option Password" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                        <asp:DropDownList ID="ddlOPTION_PASSWORD" runat="server" Width="155px"></asp:DropDownList>
                                        <asp:ImageButton ID="imgbtnOPTION_PASSWORD" runat="server" width="21px" Height="21px"/>
                                    </td>
                                    <td>
                                    
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ImageButton id="imgbtnFieldSubmit" runat="server" Width="74" Height="21" 
                                            ></asp:ImageButton>
                                        <asp:ImageButton id="imgbtnFieldUpdate" runat="server" Width="74" Height="21" 
                                            ></asp:ImageButton>
                                        <asp:ImageButton id="imgbtnFieldRefresh" runat="server" Width="74" Height="21" ></asp:ImageButton>
                                        <asp:ImageButton id="imgbtnFieldCancel" runat="server" Width="74" Height="21" ></asp:ImageButton>
                                    </td>
                                </tr>
                            </table>
                            </asp:Panel>
                            <table>
                                <tr>
                                    <td>
                                        <asp:GridView id="gvField" Width="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
				                                CellSpacing="0" CellPadding="1" EmptyDataText="Please click [ADD] to add new item." EmptyDataRowStyle-BackColor="lightyellow">
				                                <AlternatingRowStyle BackColor="#F2F4FF" />
				                                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
				                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
				                                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
				                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select" ItemStyle-Width="35" >
                                                        <ItemTemplate>
                                                            <center><asp:CheckBox ID="chkSelect" runat="server" /></center>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>
                                        <asp:ImageButton id="imgBtnFieldAdd" runat="server" Height="21px" Width="74px" ></asp:ImageButton>
                                        <asp:ImageButton id="imgBtnFieldEdit" runat="server" Height="21px" Width="74px" ></asp:ImageButton>
				                        <asp:ImageButton id="imgBtnFieldDelete" runat="server" Height="21px" Width="74px" OnClientClick="return confirm('Confirmation! Delete Data(s) ?')" ></asp:ImageButton>
				                        <asp:ImageButton ID="imgbtnFieldCREATE" runat="server" Height="21px" Width="74px" OnClientClick="return confirm('Confirm Process ?')" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblFieldMessage" runat="server" ForeColor="red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        
                        </td>
                    </tr>
                </table>
                </asp:Panel>
            </asp:View>
            <asp:View ID="vwOption" runat="server">
                <asp:Panel ID="pnlOption" runat="server" BorderStyle="Groove" Width="400px">
                    <table>
                        <tr align="right">
                            <td>
                                <asp:LinkButton ID="lnkbtnOptionBack" runat="server" Text="[Back]"></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnOptionNext" runat="server" Text="[Next]"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitleOption" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <asp:Panel ID="pnlOptionMain" runat="server">
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgKeyOPTION_FIELD_NAME" runat="server" Width="21px" Height="21px" />
                                <asp:Label ID="lblOPTION_FIELD_NAME" runat="server" Text="Field Name" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                <asp:DropDownList ID="ddlOPTION_FIELD_NAME" runat="server" Width="155px"></asp:DropDownList>
                                <asp:ImageButton ID="imgbtnOPTION_FIELD_NAME" runat="server" width="21px" Height="21px"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgKeyOPTION_CODE" runat="server" Width="21px" Height="21px" />
                                <asp:Label ID="lblOPTION_CODE" runat="server" Text="Option Code" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                <asp:TextBox ID="txtOPTION_CODE" runat="server" Width="150px" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:TextBox>
                                <asp:ImageButton ID="imgbtnOPTION_CODE" runat="server" width="21px" Height="21px"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgKeyOPTION_NAME" runat="server" Width="21px" Height="21px" />
                                <asp:Label ID="lblOPTION_NAME" runat="server" Text="Option Name" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                <asp:TextBox ID="txtOPTION_NAME" runat="server" Width="150px" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:TextBox>
                                <asp:ImageButton ID="imgbtnOPTION_NAME" runat="server" width="21px" Height="21px"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgKeyOPTION_DEFAULT" runat="server" Width="21px" Height="21px" />
                                <asp:Label ID="lblOPTION_DEFAULT" runat="server" Text="Set As Default" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                <asp:DropDownList ID="ddlOPTION_DEFAULT" runat="server" Width="155px"></asp:DropDownList>
                                <asp:ImageButton ID="imgbtnOPTION_DEFAULT" runat="server" width="21px" Height="21px"/>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton id="imgbtnOptionSubmit" runat="server" Width="74" Height="21" 
                                    ></asp:ImageButton>
                                <asp:ImageButton id="imgbtnOptionUpdate" runat="server" Width="74" Height="21" 
                                    ></asp:ImageButton>
                                <asp:ImageButton id="imgbtnOptionRefresh" runat="server" Width="74" Height="21" ></asp:ImageButton>
                                <asp:ImageButton id="imgbtnOptionCancel" runat="server" Width="74" Height="21" ></asp:ImageButton>
                            </td>
                        </tr>
                        </asp:Panel>
                        <tr>
                            <td>
                                <asp:GridView id="gvOption" Width="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
				                        CellSpacing="0" CellPadding="1" EmptyDataText="Please click [ADD] to add new item." EmptyDataRowStyle-BackColor="lightyellow">
				                        <AlternatingRowStyle BackColor="#F2F4FF" />
				                        <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
				                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
				                        <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
				                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" ItemStyle-Width="35" >
                                                <ItemTemplate>
                                                    <center><asp:CheckBox ID="chkSelect" runat="server" /></center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <asp:Panel ID="pnlOptionButton" runat="server">
                        <tr>
                            <td>
                                <asp:ImageButton id="imgBtnOptionAdd" runat="server" Height="21px" Width="74px" ></asp:ImageButton>
                                <asp:ImageButton id="imgBtnOptionEdit" runat="server" Height="21px" Width="74px" ></asp:ImageButton>
				                <asp:ImageButton id="imgBtnOptionDelete" runat="server" Height="21px" Width="74px" OnClientClick="return confirm('Confirmation! Delete Data(s) ?')" ></asp:ImageButton>
				                <asp:ImageButton ID="imgbtnOptionCREATE" runat="server" Height="21px" Width="74px" OnClientClick="return confirm('Confirm Process ?')" />
                            </td>
                        </tr>
                        </asp:Panel>
                        <tr>
                            <td>
                                <asp:Label ID="lblOptionMessage" runat="server" ForeColor="red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:View>
            <asp:View ID="vwLookup" runat="server">
                <asp:Panel ID="pnlLookup" runat="server" BorderStyle="Groove" Width="400px">
                    <table>
                        <tr align="right">
                            <td>
                                <asp:LinkButton ID="lnkbtnLookupBack" runat="server" Text="[Back]"></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnLookupNext" runat="server" Text="[Next]"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTitleLookup" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <asp:Panel ID="pnlLookupMain" runat="server">
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgKeyLOOKUP_FIELD_NAME" runat="server" Width="21px" Height="21px" />
                                <asp:Label ID="lblLOOKUP_FIELD_NAME" runat="server" Text="Field Name" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                <asp:DropDownList ID="ddlLOOKUP_FIELD_NAME" runat="server" Width="155px"></asp:DropDownList>
                                <asp:ImageButton ID="imgbtnLOOKUP_FIELD_NAME" runat="server" width="21px" Height="21px"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgKeyLOOKUP_TABLE_VIEW" runat="server" Width="21px" Height="21px" />
                                <asp:Label ID="lblLOOKUP_TABLE_VIEW" runat="server" Text="Lookup Code" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                <asp:DropDownList ID="ddlLOOKUP_TABLE_VIEW" runat="server" Width="155px" AutoPostBack="true"></asp:DropDownList>
                                <asp:ImageButton ID="imgbtnLOOKUP_TABLE_VIEW" runat="server" width="21px" Height="21px"/>
                            </td>
                        </tr>
                        <asp:Panel ID="pnlLookupSub" runat="server">
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgKeyLOOKUP_OPTION_TYPE" runat="server" Width="21px" Height="21px" />
                                <asp:Label ID="lblLOOKUP_OPTION_TYPE" runat="server" Text="Option Type" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                <asp:DropDownList ID="ddlLOOKUP_OPTION_TYPE" runat="server" Width="155px"></asp:DropDownList>
                                <asp:ImageButton ID="imgbtnLOOKUP_OPTION_TYPE" runat="server" width="21px" Height="21px"/>
                            </td>
                        </tr>
                        </asp:Panel>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton id="imgbtnLookupSubmit" runat="server" Width="74" Height="21" 
                                    ></asp:ImageButton>
                                <asp:ImageButton id="imgbtnLookupUpdate" runat="server" Width="74" Height="21" 
                                    ></asp:ImageButton>
                                <asp:ImageButton id="imgbtnLookupRefresh" runat="server" Width="74" Height="21" ></asp:ImageButton>
                                <asp:ImageButton id="imgbtnLookupCancel" runat="server" Width="74" Height="21" ></asp:ImageButton>
                            </td>
                        </tr>
                        </asp:Panel>
                        <tr>
                            <td>
                                <asp:GridView id="gvLookup" Width="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
				                        CellSpacing="0" CellPadding="1" EmptyDataText="Please click [ADD] to add new item." EmptyDataRowStyle-BackColor="lightyellow">
				                        <AlternatingRowStyle BackColor="#F2F4FF" />
				                        <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
				                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
				                        <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
				                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select" ItemStyle-Width="35" >
                                                <ItemTemplate>
                                                    <center><asp:CheckBox ID="chkSelect" runat="server" /></center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <asp:Panel ID="pnlLookupButton" runat="server">
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton id="imgBtnLookupAdd" runat="server" Height="21px" Width="74px" ></asp:ImageButton>
                                <asp:ImageButton id="imgBtnLookupEdit" runat="server" Height="21px" Width="74px" ></asp:ImageButton>
				                <asp:ImageButton id="imgBtnLookupDelete" runat="server" Height="21px" Width="74px" OnClientClick="return confirm('Confirmation! Delete Data(s) ?')" ></asp:ImageButton>
				                <asp:ImageButton ID="imgbtnLookupCREATE" runat="server" Height="21px" Width="74px" OnClientClick="return confirm('Confirm Process ?')" />
                            </td>
                        </tr>
                        </asp:Panel>
                        <tr>
                            <td>
                                <asp:Label ID="lblLookupMessage" runat="server" ForeColor="red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:View>
            <asp:View ID="vwSecurity" runat="server">
                <asp:Panel ID="pnlSecurity" runat="server" BorderStyle="Groove" Width="100%">
                    <table>
                        <tr align="right">
                            <td>
                                <asp:LinkButton ID="lnkbtnSecurityBack" runat="server" Text="[Back]"></asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnSecurityNext" runat="server" OnClientClick="return confirm('Confirm To Create New Page?')" Text="[Finish]"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTitleSecurity" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="pnlSecurityMain" runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblLeft" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblRight" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:ListBox id="lstLeft" Width="250px" Height="170px" SelectionMode="Multiple" runat="server" ></asp:ListBox>
                                                                </td>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="imgbtnSelectOne" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="imgbtnRemoveOne" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="imgbtnSelectAll" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="imgbtnRemoveAll" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <asp:ListBox id="lstRight" Width="250px" Height="170px" SelectionMode="Multiple" runat="server" ></asp:ListBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="imgKeySECURITY_OPTION_ALLOW_VIEW" runat="server" Width="21px" Height="21px" />
                                                    <asp:Label ID="lblSECURITY_OPTION_ALLOW_VIEW" runat="server" Text="Option Allow View" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                                    <asp:DropDownList ID="ddlSECURITY_OPTION_ALLOW_VIEW" runat="server" Width="155px"></asp:DropDownList>
                                                    <asp:ImageButton ID="imgbtnSECURITY_OPTION_ALLOW_VIEW" runat="server" width="21px" Height="21px"/>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgKeySECURITY_OPTION_ALLOW_INSERT" runat="server" Width="21px" Height="21px" />
                                                    <asp:Label ID="lblSECURITY_OPTION_ALLOW_INSERT" runat="server" Text="Option Allow Insert" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                                    <asp:DropDownList ID="ddlSECURITY_OPTION_ALLOW_INSERT" runat="server" Width="155px"></asp:DropDownList>
                                                    <asp:ImageButton ID="imgbtnSECURITY_OPTION_ALLOW_INSERT" runat="server" width="21px" Height="21px"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="imgKeySECURITY_OPTION_ALLOW_UPDATE" runat="server" Width="21px" Height="21px" />
                                                    <asp:Label ID="lblSECURITY_OPTION_ALLOW_UPDATE" runat="server" Text="Option Allow Update" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                                    <asp:DropDownList ID="ddlSECURITY_OPTION_ALLOW_UPDATE" runat="server" Width="155px"></asp:DropDownList>
                                                    <asp:ImageButton ID="imgbtnSECURITY_OPTION_ALLOW_UPDATE" runat="server" width="21px" Height="21px"/>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgKeySECURITY_OPTION_ALLOW_DELETE" runat="server" Width="21px" Height="21px" />
                                                    <asp:Label ID="lblSECURITY_OPTION_ALLOW_DELETE" runat="server" Text="Option Allow Delete" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                                    <asp:DropDownList ID="ddlSECURITY_OPTION_ALLOW_DELETE" runat="server" Width="155px"></asp:DropDownList>
                                                    <asp:ImageButton ID="imgbtnSECURITY_OPTION_ALLOW_DELETE" runat="server" width="21px" Height="21px"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="imgKeySECURITY_OPTION_ALLOW_PRINT" runat="server" Width="21px" Height="21px" />
                                                    <asp:Label ID="lblSECURITY_OPTION_ALLOW_PRINT" runat="server" Text="Option Allow Print" Width="140px" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="11px"></asp:Label>
                                                    <asp:DropDownList ID="ddlSECURITY_OPTION_ALLOW_PRINT" runat="server" Width="155px"></asp:DropDownList>
                                                    <asp:ImageButton ID="imgbtnSECURITY_OPTION_ALLOW_PRINT" runat="server" width="21px" Height="21px"/>
                                                </td>
                                                <td>
                                                
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                
                                                </td>
                                                <td>
                                                
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                            <td>
                                                <asp:ImageButton id="imgbtnSecuritySubmit" runat="server" Width="74" Height="21" 
                                                    ></asp:ImageButton>
                                                <asp:ImageButton id="imgbtnSecurityUpdate" runat="server" Width="74" Height="21" 
                                                    ></asp:ImageButton>
                                                <asp:ImageButton id="imgbtnSecurityRefresh" runat="server" Width="74" Height="21" ></asp:ImageButton>
                                                <asp:ImageButton id="imgbtnSecurityCancel" runat="server" Width="74" Height="21" ></asp:ImageButton>
                                            </td>
                                        </tr>
                                        </table>
                                    </asp:Panel>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:GridView id="gvSecurity" Width="100%" runat="server" AutoGenerateColumns="true" AutoGenerateEditButton="false" AutoGenerateDeleteButton="false" AutoGenerateSelectButton="false" 
				                                    CellSpacing="0" CellPadding="1" EmptyDataText="Please click [ADD] to add new item." EmptyDataRowStyle-BackColor="lightyellow">
				                                    <AlternatingRowStyle BackColor="#F2F4FF" />
				                                    <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
				                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
				                                    <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
				                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select" ItemStyle-Width="35" >
                                                            <ItemTemplate>
                                                                <center><asp:CheckBox ID="chkSelect" runat="server" /></center>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ImageButton id="imgbtnSecurityAdd" runat="server" Height="21px" Width="74px" ></asp:ImageButton>
                                            <asp:ImageButton id="imgbtnSecurityEdit" runat="server" Height="21px" Width="74px" ></asp:ImageButton>
				                            <asp:ImageButton id="imgbtnSecurityDelete" runat="server" Height="21px" Width="74px" OnClientClick="return confirm('Confirmation! Delete Data(s) ?')" ></asp:ImageButton>
				                            <asp:ImageButton ID="imgbtnSecurityCreate" runat="server" Height="21px" Width="74px" OnClientClick="return confirm('Confirm Process ?')" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSecurityMessage" runat="server" ForeColor="red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    
                </asp:Panel>
            </asp:View>
        </asp:MultiView>
    </div>
    <div>
        <asp:Label ID="lblMessage" runat="Server" ForeColor="Red" Font-Bold="true" Font-Size="11px"></asp:Label>
    </div>
    </form>
</body>
</html>
