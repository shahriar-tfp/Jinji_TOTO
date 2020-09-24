
Partial Class ShowScreen
    Inherits System.Web.UI.Page
    Private foldername As String = "0"
    Private fileview As String = "no"
    Private sUrl As String = System.AppDomain.CurrentDomain.BaseDirectory() + "\Pages\forum\"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("foldername") IsNot Nothing Then
            foldername = Request.QueryString("foldername").ToString()
        End If
        If Request.QueryString("view") IsNot Nothing Then
            fileview = Request.QueryString("view").ToString()
        End If

        If Page.IsPostBack Then

        Else
            If fileview = "yes" Then
                'Page.RegisterStartupScript("ScreenFile", "<script>alert('aaa')</script>")
                ScreenUpload.Visible = False
                button1.Visible = False
                image1.ImageUrl = "./Upload/" + foldername + "/ScreenCapture.jpg"
            Else
                'Page.RegisterStartupScript("ScreenFile", "<script>alert('bbb')</script>")
                image1.Visible = False
            End If

        End If

    End Sub
    Protected Sub UploadAndShow(ByVal sender As Object, ByVal e As System.EventArgs)
        If ScreenUpload.HasFile Then
            Dim extension As String = Right(ScreenUpload.FileName.ToString(), 3)
            If extension = "jpg" Or extension = "png" Or extension = "bmp" Or extension = "gif" Then
                Dim filePath As String = sUrl + "Upload\\" + foldername + "\\" + ScreenUpload.FileName
                Dim screenPath As String = sUrl + "Upload\\" + foldername + "\\ScreenCapture.jpg"
                If System.IO.File.Exists(screenPath) Then
                    System.IO.File.Delete(screenPath)
                End If
                ScreenUpload.SaveAs(filePath)

                System.IO.File.Copy(filePath, screenPath, True)

                Dim imgUrl As String = "./Upload/" + foldername + "/" + ScreenUpload.FileName
                imagediv.InnerHtml = "<img id='showimage' runat='server' alt='Capture Screen File' src='" + imgUrl + "'/>"


            Else
                Page.RegisterStartupScript("ScreenFile", "<script>alert('Jpg, Bmp, Png and Gif file is possible for Screen Capture File')</script>")
                Return
            End If
        End If
    End Sub
End Class
