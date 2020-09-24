Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class PAGES_EAPPRAISAL_ACCESSDENIED
    Inherits System.Web.UI.Page

    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting, MyKajima As New clsKajimaWeb
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("EmpID") = "" Then
            Response.Redirect("../Global/SessionTimeOut.aspx", True)
        Else
            lblUser.Text = Session("username")
        End If

    End Sub

    Protected Sub lnkMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkMain.Click

        Dim strURL As String
        Dim strCurrentDay, strCurrentMonth, strCurrentYear As String

        strCurrentDay = Now.Day.ToString
        strCurrentMonth = Now.Month.ToString
        strCurrentYear = Now.Year.ToString

        If strCurrentDay.Length = 1 Then strCurrentDay = "0" & strCurrentDay

        If strCurrentMonth.Length = 1 Then strCurrentMonth = "0" & strCurrentMonth

        strURL = "/home/main.aspx?" & MyKajima.EncryptedText(Session("EmpID"))

        Session("EmpID") = ""
        Response.Redirect(strURL, True)

    End Sub
    Private Sub lnkLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogOut.Click

        Session("EmpID") = ""
        Response.Redirect("../Global/SessionTimeOut.aspx", True)

    End Sub
End Class