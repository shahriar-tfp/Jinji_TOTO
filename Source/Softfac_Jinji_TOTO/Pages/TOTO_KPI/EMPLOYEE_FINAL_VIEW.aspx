<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EMPLOYEE_FINAL_VIEW.aspx.vb" Inherits="Pages_TOTO_KPI_EMPLOYEE_FINAL_VIEW" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMPLOYEE EVALUATION View</title>
    <link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet" />

</head>
<body>
    <form id="EMPLOYEE_FINAL_VIEW" runat="server">
    <div>
    <asp:Panel ID="pnlSalary_Master" runat="server">
    <table id="Table6" cellpadding="0" cellspacing="0" width="100%" border="0" runat="server">
        <tr>
            <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); width :5px"></td>
            <td style="background-image:url(../../Images/Company/Default/gif/org_title_bar20.gif); vertical-align:bottom"><asp:Label ID="lblTitle" runat="server"></asp:Label></td>
        </tr>
    </table>
    <table id="Table9" cellspacing="0" cellpadding="0" border="0" runat="server">
        <tr>
            <td style ="height:15px"></td>
        </tr>
    </table>
    <asp:Panel ID="pnlEdit" runat="server">
    <asp:Image ID="imgTop" runat="server" Height="30px" />

    <asp:ImageButton ID="imgKeyCATEGORY" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblCATEGORY" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlCATEGORY" runat="server" Width="150px" AutoPostBack="true"></asp:dropdownlist>
    <asp:ImageButton ID="imgbtnCATEGORY" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOCP_ID_DEPARTMENT" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOCP_ID_DEPARTMENT" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlOCP_ID_DEPARTMENT" runat="server" Width="150px" AutoPostBack="true"></asp:dropdownlist>
    <asp:ImageButton ID="imgbtnOCP_ID_DEPARTMENT" runat="server" Width="21px" Height="21px"/>
    
    <asp:ImageButton ID="imgKeyOCP_ID_SECTION" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblOCP_ID_SECTION" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlOCP_ID_SECTION" runat="server" Width="150px" AutoPostBack="true"></asp:dropdownlist>
    <asp:ImageButton ID="imgbtnOCP_ID_SECTION" runat="server" Width="21px" Height="21px"/>

    <asp:ImageButton ID="imgKeyYEAR" runat="server" Width="21px" Height="21px"/>
    <asp:Label ID="lblYEAR" runat="server" Width="120px"></asp:Label>
    <asp:dropdownlist ID="ddlYEAR" runat="server" Width="150px" AutoPostBack="true"></asp:dropdownlist>
    <asp:ImageButton ID="imgbtnYEAR" runat="server" Width="21px" Height="21px"/>

    <asp:imagebutton id="imgBtnPrint" runat="server" Width="74" Height="21" visible="true"></asp:imagebutton>
    <asp:imagebutton id="imgBtnPreview" runat="server" Width="74" Height="21" visible="true"></asp:imagebutton>
    <asp:imagebutton id="imgBtnClear" runat="server" Width="74" Height="21" visible="true"></asp:imagebutton>
    <asp:label id="lblresult2" runat="server" Height="22" Visible="False"></asp:label>
    <asp:Image ID="imgBottom" runat="server" Visible="false" />
    </asp:Panel>
    <asp:Panel ID="pnlGridView" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
	   <tr><td>	    
	    <div id="divGridview1" style="top: 250px;left: 15px;position :absolute; width: 100%; height: 200px;overflow:scroll " >
	    <asp:Label ID="lblgv1" Text="" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            <asp:gridview id="gv1" Width="100%" runat="server" AutoGenerateColumns="false" Visible="true"
                cellspacing="0" cellpadding="1" EmptyDataText="No data found!" 
                OnRowDataBound="gv1_RowDataBound"
                OnRowCreated="gv1_RowCreated">
                <AlternatingRowStyle BackColor="#F2F4FF" />
                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
                <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                <Columns>
                <asp:BoundField DataField="Department" HeaderText="Department" ItemStyle-HorizontalAlign="Left" >
                </asp:BoundField>
                <asp:BoundField DataField="Section" HeaderText="Section" ItemStyle-HorizontalAlign="Left" >
                </asp:BoundField>
                <asp:BoundField DataField="Emp ID" HeaderText="Emp ID" ItemStyle-HorizontalAlign="Left" >
                </asp:BoundField>
                <asp:BoundField DataField="Emp Name" HeaderText="Emp Name" ItemStyle-HorizontalAlign="Left" >
                </asp:BoundField>
                <asp:BoundField DataField="Common Skill Map Score Last Year" HeaderText="Last Year" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Common Skill Map Score This Year" HeaderText="This Year" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Special Skill Map Score Last Year" HeaderText="Last Year" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Special Skill Map Score This Year" HeaderText="This Year" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Total Score Last Year" HeaderText="Last Year" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Total Score This Year" HeaderText="This Year" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Skil Improvement Points This Year" HeaderText="Skil Improvement Points This Year" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Skill Improvement" HeaderText="Skill Improvement" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Personal Development" HeaderText="Personal Development" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Kaizen" HeaderText="Kaizen" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Safety Idea" HeaderText="Safety Idea" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Compulsory Training" HeaderText="Compulsory Training" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="P Score" HeaderText="P Score" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Attendance" HeaderText="Attendance" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Attendance Score" HeaderText="Attendance Score" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Minor" HeaderText="Minor" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Serious" HeaderText="Serious" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Gross" HeaderText="Gross" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Misconduct Score" HeaderText="Score" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Total Score" HeaderText="Total Score" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                </Columns>
            </asp:gridview>
            <asp:gridview id="gv2" Width="100%" runat="server" AutoGenerateColumns="false" Visible="false"
                cellspacing="0" cellpadding="1" EmptyDataText="No data found!"
                OnRowDataBound="gv2_RowDataBound"
                OnRowCreated="gv2_RowCreated">
                <AlternatingRowStyle BackColor="#F2F4FF" />
                <EditRowStyle BackColor="#FFC7C6" Font-Size="Smaller" Height="20"/>
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <Rowstyle BackColor="#FFFFFF" CssClass="dgstyle_i" />
                <pagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#988675" Height="21" CssClass="dgstyle_h" />
                <Columns>
                <asp:BoundField DataField="Department" HeaderText="Department" ItemStyle-HorizontalAlign="Left" >
                </asp:BoundField>
                <asp:BoundField DataField="Section" HeaderText="Section" ItemStyle-HorizontalAlign="Left" >
                </asp:BoundField>
                <asp:BoundField DataField="Emp ID" HeaderText="Emp ID" ItemStyle-HorizontalAlign="Left" >
                </asp:BoundField>
                <asp:BoundField DataField="Emp Name" HeaderText="Emp Name" ItemStyle-HorizontalAlign="Left" >
                </asp:BoundField>
                <asp:BoundField DataField="Common Skill Map Score Last Year" HeaderText="Last Year" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Common Skill Map Score This Year" HeaderText="This Year" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Special Skill Map Score Last Year" HeaderText="Last Year" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Special Skill Map Score This Year" HeaderText="This Year" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Total Score Last Year" HeaderText="Last Year" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Total Score This Year" HeaderText="This Year" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Skil Improvement Points This Year" HeaderText="Skil Improvement Points This Year" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Skill Improvement" HeaderText="Skill Improvement" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Personal Development" HeaderText="Personal Development" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Kaizen" HeaderText="Kaizen" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Safety Idea" HeaderText="Safety Idea" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Compulsory Training" HeaderText="Compulsory Training" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="P Score" HeaderText="P Score" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Target Achievement" HeaderText="Target Achievement" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Achievement Score" HeaderText="Achievement Score" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Minor" HeaderText="Minor" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Serious" HeaderText="Serious" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Gross" HeaderText="Gross" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Misconduct Score" HeaderText="Score" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                <asp:BoundField DataField="Total Score" HeaderText="Total Score" ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                </Columns>
            </asp:gridview>
	         </div></td></tr>
	         </table>
	         </asp:Panel>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
