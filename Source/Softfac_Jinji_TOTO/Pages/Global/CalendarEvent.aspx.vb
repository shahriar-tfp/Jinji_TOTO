Imports System
Imports System.DateTime
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Partial Class Pages_Global_Calendar_Event
    Inherits System.Web.UI.Page
    Dim dtEvents As New DataTable
    Protected Sub CalendarEvent_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CalendarEvent.Load
        'If Not IsPostBack Then
        BuildEventTable()
        'End If
    End Sub

    Protected Sub calEvents_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles calEvents.DayRender
        Dim dr() As DataRow = dtEvents.Select(String.Format("Date >= #{0}# AND Date < #{1}#", e.Day.Date.ToShortDateString, e.Day.Date.AddDays(1).ToShortDateString))
        Dim dr1 As DataRow
        For Each dr1 In dr
            Dim img As New System.Web.UI.WebControls.Image
            img.ImageUrl = "../../Images/Default/gif/dot.gif"
            img.ToolTip = dr1("Description").ToString
            e.Cell.Controls.Add(img)
            e.Cell.BackColor = Color.Firebrick
        Next

        '         DataRow[] rows = socialEvents.Select(
        '       String.Format(
        '          "Date >= #{0}# AND Date < #{1}#", 
        '          e.Day.Date.ToShortDateString(),
        'e.Day.Date.AddDays(1).ToShortDateString()
        '       )
        '    );

        ' foreach(DataRow row in rows)
        ' {                         
        '    System.Web.UI.WebControls.Image image;
        '    image = new System.Web.UI.WebControls.Image();
        '    image.ImageUrl = "dot.gif";
        '    image.ToolTip = row["Description"].ToString();
        '    e.Cell.Controls.Add(image);
        '    e.Cell.BackColor = Color.Firebrick;
        ' }
    End Sub

    Protected Sub calEvents_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calEvents.SelectionChanged
        Dim dvView As System.Data.DataView
        dvView = dtEvents.DefaultView
        dvView.RowFilter = String.Format("Date >= #{0}# AND Date < #{1}#", calEvents.SelectedDate.ToShortDateString, calEvents.SelectedDate.AddDays(1).ToShortDateString)
        If dvView.Count > 0 Then
            gvEvents.Visible = True
            gvEvents.DataSource = dvView
            gvEvents.DataBind()
        Else
            gvEvents.Visible = False
        End If
    End Sub
    Private Sub BuildEventTable()

        dtEvents.Columns.Add(New DataColumn("Date", GetType(DateTime)))
        dtEvents.Columns.Add(New DataColumn("Description", GetType(String)))
        dtEvents.Columns.Add(New DataColumn("URL", GetType(String)))

        Dim dr As DataRow
        dr = dtEvents.NewRow
        dr("Date") = DateTime.Now.AddDays(-5)
        dr("Description") = "Softfac"
        dr("URL") = "http://www.softfac.com"
        dtEvents.Rows.Add(dr)

        dr = dtEvents.NewRow
        dr("Date") = DateTime.Now.AddDays(-10)
        dr("Description") = "MBP"
        dr("URL") = "http://www.mbp.com.my"
        dtEvents.Rows.Add(dr)

        dr = dtEvents.NewRow
        dr("Date") = DateTime.Now.AddDays(3)
        dr("Description") = "TenInfo"
        dr("URL") = "http://www.teninfo.com.my"
        dtEvents.Rows.Add(dr)

        dr = dtEvents.NewRow
        dr("Date") = DateTime.Now.AddDays(7)
        dr("Description") = "MSN"
        dr("URL") = "http://www.msn.com"
        dtEvents.Rows.Add(dr)
    End Sub
End Class
