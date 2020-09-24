Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Extensibility

Partial Class ReportViewer
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            Dim viewprinter As New GrapeCity.ActiveReports.Extensibility.Printing.Printer()
            viewprinter.PrinterName = ""
            WebViewer1.ClearCachedReport()
            WebViewer1.ViewerType = Web.ViewerType.FlashViewer
            WebViewer1.FlashViewerOptions.Zoom = 30%
            WebViewer1.Report = CType(Session("rptGenericReport"), SectionReport)
        End If
    End Sub

End Class
