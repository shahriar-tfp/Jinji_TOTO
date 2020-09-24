Imports System.IO
Namespace Forum
    Partial Class Default2
        Inherits System.Web.UI.Page

        Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            'Dim a As New ScreenCapture()
            'Dim image As System.Drawing.Image = a.CaptureScreen()
            'Dim num As Integer = a.StoreToDatabase(image)
            Dim myclas As New clsDataAccess()
            myclas.openConnection()
            Dim folderName As String = myclas.getFolderName()
            myclas.closeConnection()
            Directory.CreateDirectory(Server.MapPath("\\Upload\\" + folderName))
            Directory.CreateDirectory(Server.MapPath("\\Upload\\" + folderName + "\\files"))
            Response.Redirect("NewMessage.aspx?id=" + txtuid.Text.ToString() + "&name=" + Textbox1.Text.ToString() + "&company=" + Textbox2.Text.ToString() + "&reporttitle=We are a friend.&foldername=" + folderName)
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        End Sub
    End Class
End Namespace