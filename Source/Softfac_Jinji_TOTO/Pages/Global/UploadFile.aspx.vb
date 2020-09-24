
Partial Class Pages_Global_UploadFile
    Inherits System.Web.UI.Page
    Dim mySQL As New clsSQL
    Sub Upload_Click(ByVal Source As Object, ByVal e As EventArgs)
        Try
            Dim myFile As HtmlInputFile = Page.FindControl("myFile")
            If Not (myFile.PostedFile Is Nothing) Then
                Dim intFileNameLength As Integer
                Dim strFileNamePath As String = ""
                Dim strFileNameOnly As String = ""

                'Logic to find the FileName (excluding the path)
                strFileNamePath = myFile.PostedFile.FileName
                intFileNameLength = InStr(1, StrReverse(strFileNamePath), "\")
                strFileNameOnly = Mid(strFileNamePath, (Len(strFileNamePath) - intFileNameLength) + 2)

                myFile.PostedFile.SaveAs(mySQL.GetProjectPath & "Upload\" & strFileNameOnly)
                lblError.Visible = True
                lblError.Text = "File Upload Success."
            End If
        Catch ex As Exception
            lblError.Visible = True
            lblError.Text = ex.Message.ToString
        End Try
    End Sub

End Class
