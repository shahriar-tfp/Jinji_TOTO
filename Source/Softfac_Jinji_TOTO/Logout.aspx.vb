
Partial Class Pages_Global_Logout
    Inherits System.Web.UI.Page

    Dim strPath As String = "Images"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pageCss21.Href = "App_Themes/hcrmStyles1.css"
    End Sub

    Protected Sub imgBtnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles imgBtnYes.Click

        Dim key As String
        Dim str As String = ""
        For Each key In Session.Keys
            str &= key + " : " + Session(key).ToString() + vbCrLf
        Next
        Response.Write(str)
        Session.Clear()
        Response.Redirect("Login.aspx")
        'Response.Write("<script language='javascript'> { window.open('','_parent','');window.close();}</script>")
    End Sub

    Protected Sub imgBtnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles imgBtnNo.Click
        Response.Redirect("Home.aspx")
    End Sub
End Class
