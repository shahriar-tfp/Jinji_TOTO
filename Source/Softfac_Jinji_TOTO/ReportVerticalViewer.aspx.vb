
Partial Class ReportVerticalViewer
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            WebViewer.ClearCachedReport()
            WebViewer.Report = CType(Session("rptHorizontal"), rptHorizontal)
        End If
    End Sub
End Class
