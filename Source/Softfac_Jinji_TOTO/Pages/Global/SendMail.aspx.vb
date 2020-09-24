Imports System.IO

Partial Class Pages_Reports_SendMail
    Inherits System.Web.UI.Page

    Private Sub sendmail(ByVal strBody As String)

        Dim strFilePath As String
        strFilePath = System.AppDomain.CurrentDomain.BaseDirectory()
        strFilePath = Replace(strFilePath, "/", "\")

        If File.Exists(strFilePath & "Setup\Setup.ini") Then
            Try
                Dim ReadIniFile As New clsReadFile(strFilePath & "\Setup\Setup.ini")
                Dim strMailAdd As String = ReadIniFile.GetString("Mail", "AdminEmail", "(none)")
                Dim strMailPass As String = ReadIniFile.GetString("Mail", "AdminPass", "(none)")
                Dim strMailDsp As String = ReadIniFile.GetString("Mail", "MailDisplay", "(none)")
                Dim strExServer As String = ReadIniFile.GetString("Mail", "ExcServer", "(none)")
                Dim strErrReport As String = ReadIniFile.GetString("Mail", "ErrorServer", "(none)")
                Dim mailClient As New System.Net.Mail.SmtpClient()
                Dim basicAuthenticationInfo As New System.Net.NetworkCredential(strMailAdd, strMailPass)
                Dim from As New System.Net.Mail.MailAddress(strMailAdd, strMailDsp)
                Dim toEmail As New System.Net.Mail.MailAddress(strErrReport)
                Dim message As New System.Net.Mail.MailMessage()

                message.Subject = "Error Report"
                message.Body = strBody
                message.To.Add(toEmail)
                message.From = from
                message.IsBodyHtml = True
                mailClient.Host = strExServer
                mailClient.Port = "25"
                mailClient.UseDefaultCredentials = True
                mailClient.Credentials = basicAuthenticationInfo
                mailClient.Send(message)

            Catch ex As Exception
                ShowMessage(ex.ToString())
            End Try
        Else
            ShowMessage("Missing of Setup.ini File!")
        End If
        ShowMessage("Error has been sent successfully!")



    End Sub
    Private Sub ShowMessage(ByVal message As String)

        Dim scriptString As String = "<script language = ""javascript"">"
        scriptString += "alert(""" + message + """);"
        scriptString += "history.back(-1);"
        scriptString += "</script>"
        Page.ClientScript.RegisterStartupScript(GetType(Page), "ShowMessage", scriptString)

    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        sendmail(Session("strBody").ToString)


    End Sub
End Class
