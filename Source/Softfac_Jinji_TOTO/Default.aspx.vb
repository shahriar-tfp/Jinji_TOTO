
Partial Class Default_Page
    Inherits System.Web.UI.Page

    Protected Sub Default_Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles DefaultPage.Load
        If Not Page.IsPostBack Then

            Dim objBrowsCap As HttpBrowserCapabilities
            Dim objSubsCookie As New HttpCookie("myTheme")
            Dim blnCookies As Boolean

            'Determine if cookies are accepted
            objBrowsCap = Request.Browser
            If objBrowsCap.Cookies Then
                blnCookies = True
            Else
                blnCookies = False
                ShowMessage("Your Browser Does Not Support Cookies!")
            End If

            'Add a Session Cookie
            If blnCookies = True Then
                If Not Request.Cookies("myTheme") Is Nothing Then
                    Session("strTheme") = Request.Cookies("myTheme").Value
                Else
                    If Session("strTheme") <> "" Then
                        objSubsCookie.Value = Session("strTheme")
                        objSubsCookie.Expires = DateAdd(DateInterval.Year, 1, Now)
                        Response.Cookies.Set(objSubsCookie)
                    Else
                        If Not Request.Cookies("myTheme") Is Nothing Then
                            If Request.Cookies("myTheme").Value = "Theme1" Then
                                Session("strTheme") = "Theme1"
                            ElseIf Request.Cookies("myTheme").Value = "Theme2" Then
                                Session("strTheme") = "Theme2"
                            Else
                                Session("strTheme") = "Theme1"
                            End If
                        Else
                            Session("strTheme") = "Theme1"
                        End If
                        objSubsCookie.Value = Session("strTheme")
                        objSubsCookie.Expires = DateAdd(DateInterval.Year, 1, Now)
                        Response.Cookies.Set(objSubsCookie)
                    End If
                End If
            Else
                If Session("strTheme") = "" Then
                    Session("strTheme") = "Theme1"
                End If
            End If

            If Session("Company") <> "" And Session("EmpID") <> "" And Session("LoginStatus") = "SUCCESS" Then
                If Session("ScreenResolution") = "" Then
                    Session("PagePath") = "../../Home.aspx" 'determaine ur path here
                    Response.Redirect("Pages/Global/DetectScreen.aspx")
                Else
                    Response.Redirect("Home.aspx")
                End If
                'Response.Redirect("Login.aspx")
            Else
                If Session("ScreenResolution") = "" Then
                    Session("PagePath") = "../../Login.aspx"
                    Response.Redirect("Pages/Global/DetectScreen.aspx")
                Else
                    Response.Redirect("Login.aspx")
                End If
            End If

        End If

    End Sub

    Private Sub ShowMessage(ByVal message As String)
        Dim scriptString As String = "<script language=JavaScript>"
        scriptString += "alert('" + message + "');"
        scriptString += "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "ShowMessage", scriptString)
    End Sub

End Class

