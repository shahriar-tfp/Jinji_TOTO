
Partial Class detectscreen
    Inherits System.Web.UI.Page

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load

        If Request.QueryString("action") <> "" Then
            Session("ScreenResolution") = Request.QueryString("res").ToString()
            Dim PagePath As String = Session("PagePath")
            Response.Redirect(PagePath)
        End If

    End Sub

End Class
