Imports System
Imports System.Data
Imports System.IO

Partial Class Forgot_Password
    Inherits System.Web.UI.Page

    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting, myDS, myDS1 As New DataSet, MyDT As DataTable
    Dim ssql, ssql1, strFilePath As String
    Dim strPath As String = "Images"
    Dim objSubsCookie As New HttpCookie("myTheme")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Session("Module") = "SYSTEM_MANAGER"
            Session("LoginStatus") = ""
            Session("CompanyName") = ""
            pnlPassCode.Visible = True
            pnlCom.Visible = False
            PagePreload()

            lblTitle.Text = "Forgot your Password?"
            lblTitle2.Text = "Enter your Employee Code to start the password recovery process"
            lblTitle3.Text = "Forgot your Employee Code?"
            lblTitle4.Text = "Enter your email to start the password recovery process"
            lblEmpcode.Text = "Employee Code"
            lblEmail.Text = "E-Mail"
            lblCom.Text = "Company Code"
        End If

    End Sub

    Private Sub PagePreload()

        'Default screen control
        Dim xPlus, yPlus As Integer
        Dim strXplus, strYplus As String

        xPlus = (CInt(Session("ScreenWidth")) - 560) / 2
        yPlus = (CInt(Session("ScreenHeight")) - 530) / 2
        strXplus = xPlus.ToString & "px"
        strYplus = yPlus.ToString & "px"

        divForgotPass.Style.Add("margin-left ", strXplus)
        divForgotPass.Style.Add("margin-top ", strYplus)


        'Default Theme Control
        If Not Request.Cookies("myTheme") Is Nothing Then
            Session("strTheme") = Request.Cookies("myTheme").Value
        End If

        If Session("strTheme") = "" Then
            Session("strTheme") = "Theme1"
        End If
        Session("strThemeAlt") = Session("strTheme")

        'Set Css
        pageCss1.Href = strPath & "/" & Session("strTheme") & "/Style.css"
        pageCss2.Href = "App_Themes/hcrmStyles1.css"

        'Set Theme ImgBtn
        'imgBtnTheme1.ImageUrl = strPath & "/" & Session("strTheme") & "/theme_blue.gif"
        'imgBtnTheme2.ImageUrl = strPath & "/" & Session("strTheme") & "/theme_brown.gif"

        'set image button path
        imgBtnSubmit1.ImageUrl = strPath & "/Company/Default/" & Session("strTheme") & "/btnSubmit.png"
        imgBtnSubmit2.ImageUrl = strPath & "/Company/Default/" & Session("strTheme") & "/btnSubmit.png"
        imgBtnSubmit3.ImageUrl = strPath & "/Company/Default/" & Session("strTheme") & "/btnSubmit.png"

        'Set images path
        tdLayout01.Style("background-image") = strPath & "/" & Session("strTheme") & "/shadow_topmid.png"
        tdLayout01.Style("width") = "100%"
        tdLayout02.Style("background-image") = strPath & "/" & Session("strTheme") & "/inner_topmid.png"
        tdLayout02.Style("width") = "100%"
        tdLayout03.Style("background-image") = strPath & "/" & Session("strTheme") & "/inner_leftmid.png"
        tdLayout03.Style("width") = "9px"
        tdLayout04.Style("background-image") = strPath & "/" & Session("strTheme") & "/inner_rightmid.png"
        tdLayout04.Style("width") = "9px"
        tdLayout05.Style("background-image") = strPath & "/" & Session("strTheme") & "/inner_bottommid.png"
        tdLayout06.Style("background-image") = strPath & "/" & Session("strTheme") & "/shadow_leftmid.png"
        tdLayout06.Style("width") = "5px"
        tdLayout07.Style("background-image") = strPath & "/" & Session("strTheme") & "/shadow_rightmid.png"
        tdLayout08.Style("background-image") = strPath & "/" & Session("strTheme") & "/shadow_bottommid.png"

        imgLayout01.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout02.Src = strPath & "/" & Session("strTheme") & "/shadow_topleft.png"
        imgLayout03.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout04.Src = strPath & "/" & Session("strTheme") & "/shadow_topright.png"
        imgLayout05.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout06.Src = strPath & "/" & Session("strTheme") & "/shadow_lefttop.png"
        imgLayout07.Src = strPath & "/" & Session("strTheme") & "/inner_topleft.png"
        imgLayout08.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout09.Src = strPath & "/" & Session("strTheme") & "/inner_topright.png"
        imgLayout10.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        'imgLayout11.Src = strPath & "/" & Session("strTheme") & "/login_hcrp.png"
        imgLayout12.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        'imgLayout13.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout14.Src = strPath & "/" & Session("strTheme") & "/softfac_small_logo.gif"
        imgLayout15.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout16.Src = strPath & "/" & Session("strTheme") & "/inner_bottomleft.png"
        imgLayout17.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout18.Src = strPath & "/" & Session("strTheme") & "/inner_bottomright.png"
        imgLayout19.Src = strPath & "/" & Session("strTheme") & "/shadow_righttop.png"
        imgLayout20.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout21.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout22.Src = strPath & "/" & Session("strTheme") & "/shadow_leftbottom.png"
        imgLayout23.Src = strPath & "/" & Session("strTheme") & "/shadow_rightbottom.png"
        imgLayout24.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout25.Src = strPath & "/" & Session("strTheme") & "/shadow_bottomleft.png"
        imgLayout26.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout27.Src = strPath & "/" & Session("strTheme") & "/shadow_bottomright.png"
        imgLayout28.Src = strPath & "/" & Session("strTheme") & "/blank.gif"

    End Sub

    Protected Sub imgBtnSubmit1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSubmit1.Click

        If txtEmpCode.Text <> "" Then
            ssql = "Exec sp_sa_passRecovery 'getPass','','" & txtEmpCode.Text & "',''"
            lblError.Text = ""
            'Try
            myDS = mySQL.ExecuteSQL(ssql)
            If myDS.Tables.Count > 0 Then
                If myDS.Tables(0).Rows.Count > 0 Then
                    If myDS.Tables(0).Rows(0).Item(0).ToString = "E01" Then
                        pnlPassCode.Visible = False
                        pnlCom.Visible = True
                        lblTitle2.Text = "Enter your Company Code to continue the password recovery process"
                        txtEmail.Text = ""
                    ElseIf myDS.Tables(0).Rows(0).Item(0).ToString = "E02" Then
                        lblError.Text = "Data require not exist!"
                    ElseIf UCase(myDS.Tables(0).Rows(0).Item(0).ToString) = "SUCCESS" Then
                        'Display new pass function

                        If myDS.Tables(0).Rows(0).Item(3).ToString() = "" Then
                            lblError.Text = "There is no Email Address."
                        Else
                            Session("PassReco_ID") = UCase(myDS.Tables(0).Rows(0).Item(1).ToString())
                            'Session("PassReco_PS") = myDS.Tables(0).Rows(0).Item(2).ToString
                            Session("PassReco_PS") = myDS.Tables(0).Rows(0).Item(4).ToString
                            Session("PassReco_EM") = myDS.Tables(0).Rows(0).Item(3).ToString
                            SendMail(Session("PassReco_ID").ToString, Session("PassReco_PS").ToString, Session("PassReco_EM").ToString)
                            'Response.Redirect("Thank.aspx")
                        End If

                    Else
                        lblError.Text = "Unexpected Error Found!"
                    End If
                End If
            End If
            'Catch ex As Exception
            'lblError.Text = "Error By " & ex.Source & " : " & ex.Message.ToString
            'myDS = Nothing
            'End Try
            myDS = Nothing
        Else
            lblError.Text = lblEmpcode.Text & " is required!"
        End If

    End Sub

    Protected Sub imgBtnSubmit2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSubmit2.Click

        If txtEmail.Text <> "" Then
            ssql = "Exec sp_sa_passRecovery 'getCodePass','','','" & txtEmail.Text & "'"
            lblError.Text = ""
            'Try
            myDS = mySQL.ExecuteSQL(ssql)
            If myDS.Tables.Count > 0 Then
                If myDS.Tables(0).Rows.Count > 0 Then
                    If myDS.Tables(0).Rows(0).Item(0).ToString = "E01" Then
                        pnlPassCode.Visible = False
                        pnlCom.Visible = True
                        lblTitle2.Text = "Enter your Company Code to continue the password recovery process"
                        txtEmpCode.Text = ""
                        'txtEmail.Text = myDS.Tables(0).Rows(0).Item(3).ToString
                    ElseIf myDS.Tables(0).Rows(0).Item(0).ToString = "E02" Then
                        lblError.Text = "Data require not exist!"
                    ElseIf myDS.Tables(0).Rows(0).Item(0).ToString = "SUCCESS" Then
                        'Display new pass function
                        If myDS.Tables(0).Rows(0).Item(3).ToString() = "" Then
                            lblError.Text = "There is no Email Address."
                        Else
                            Session("PassReco_ID") = UCase(myDS.Tables(0).Rows(0).Item(1).ToString())
                            'Session("PassReco_PS") = myDS.Tables(0).Rows(0).Item(2).ToString
                            Session("PassReco_PS") = myDS.Tables(0).Rows(0).Item(4).ToString
                            Session("PassReco_EM") = myDS.Tables(0).Rows(0).Item(3).ToString
                            SendMail(Session("PassReco_ID").ToString, Session("PassReco_PS").ToString, Session("PassReco_EM").ToString)
                            'Response.Redirect("Thank.aspx")
                        End If
                    Else
                            lblError.Text = "Unexpected Error Found!"
                        End If
                End If
            End If
            'Catch ex As Exception
            'lblError.Text = "Error By " & ex.Source & " : " & ex.Message.ToString
            'myDS = Nothing
            'End Try
            myDS = Nothing
        Else
            lblError.Text = lblEmail.Text & " is required!"
        End If


    End Sub

    Protected Sub imgBtnSubmit3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSubmit3.Click

        If txtEmpCode.Text <> "" Then
            ssql = "Exec sp_sa_passRecovery 'getPassCom','" & txtCom.Text & "','" & txtEmpCode.Text & "',''"
        Else
            ssql = "Exec sp_sa_passRecovery 'getCodePassCom','" & txtCom.Text & "','','" & txtEmail.Text & "'"
        End If
        lblError.Text = ""
        'Try
        myDS = mySQL.ExecuteSQL(ssql)
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                If myDS.Tables(0).Rows(0).Item(0).ToString = "E01" Then
                    'pnlPassCode.Visible = False
                    'pnlCom.Visible = True

                    pnlPassCode.Visible = True
                    pnlCom.Visible = False

                    lblTitle2.Text = "Try this again, Enter your Employee code to get your password"
                ElseIf myDS.Tables(0).Rows(0).Item(0).ToString = "E02" Then
                    lblError.Text = "Data require not exist!"
                ElseIf myDS.Tables(0).Rows(0).Item(0).ToString = "SUCCESS" Then
                    'Display new pass function
                    If myDS.Tables(0).Rows(0).Item(3).ToString() = "" Then
                        lblError.Text = "There is no Email Address."
                    Else
                        Session("PassReco_ID") = UCase(myDS.Tables(0).Rows(0).Item(1).ToString())
                        'Session("PassReco_PS") = myDS.Tables(0).Rows(0).Item(2).ToString
                        Session("PassReco_PS") = myDS.Tables(0).Rows(0).Item(4).ToString
                        Session("PassReco_EM") = myDS.Tables(0).Rows(0).Item(3).ToString
                        SendMail(Session("PassReco_ID").ToString, Session("PassReco_PS").ToString, Session("PassReco_EM").ToString)
                        'Response.Redirect("Thank.aspx")
                    End If
                Else
                    lblError.Text = "Unexpected Error Found!"
                End If
            End If
        End If
        'Catch ex As Exception
        'lblError.Text = "Error By " & ex.Source & " : " & ex.Message.ToString
        'myDS = Nothing
        'End Try
        myDS = Nothing

    End Sub

    Public Sub SendMail(ByVal strCode As String, ByVal strPass As String, ByVal strMailTo As String)

        strFilePath = System.AppDomain.CurrentDomain.BaseDirectory()
        strFilePath = Replace(strFilePath, "/", "\")

        If File.Exists(strFilePath & "Setup\Setup.ini") Then
            Try
                Dim ReadIniFile As New clsReadFile(strFilePath & "\Setup\Setup.ini")
                Dim strMailAdd As String = ReadIniFile.GetString("Mail", "AdminEmail", "(none)")
                Dim strMailPass As String = ReadIniFile.GetString("Mail", "AdminPass", "(none)")
                Dim strMailDsp As String = ReadIniFile.GetString("Mail", "MailDisplay", "(none)")
                Dim strExServer As String = ReadIniFile.GetString("Mail", "ExcServer", "(none)")
                Dim mailClient As New System.Net.Mail.SmtpClient()
                Dim basicAuthenticationInfo As New System.Net.NetworkCredential(strMailAdd, strMailPass)
                Dim from As New System.Net.Mail.MailAddress(strMailAdd, strMailDsp)
                Dim toEmail As New System.Net.Mail.MailAddress(strMailTo)
                Dim message As New System.Net.Mail.MailMessage()
                Dim strBody As String = "<html><body><table border=0 cellpadding=0 cellspacing=0 width=400><tr><td colspan=3><b>The information below is strictly for the authorised user.</b></td></tr>" & _
                                        "<tr height=20><td colspan=3>&nbsp;</td></tr><tr><td width:140 align=left>User/Employee Code</td><td width=10 align=center>:</td><td width:250 align=left>" & strCode.ToString & "</td> " & _
                                        "</tr><tr><td width=140 align=left>Password</td><td width=10 align=center>:</td><td width=250 align=left>" & strPass.ToString & "</td></tr></table><br /><br /></body></html>"

                message.Subject = "Password Recovery"
                message.Body = strBody
                message.To.Add(toEmail)
                message.From = from
                message.IsBodyHtml = True
                'mailClient.Host = "myexch01.mydom.net"
                mailClient.Host = strExServer
                mailClient.Port = "25"
                mailClient.UseDefaultCredentials = True
                mailClient.Credentials = basicAuthenticationInfo
                mailClient.Send(message)

            Catch ex As Exception
                Response.Redirect("Thank.aspx?error=fail")
            End Try
        Else
            ShowMessage("Missing of Setup.ini File!")
        End If
        Response.Redirect("Thank.aspx?error=ok")

    End Sub

    Private Sub ShowMessage(ByVal message As String)

        Dim scriptString As String = "<script language=JavaScript>"
        scriptString += "alert('" + message + "');"
        scriptString += "<" & "/script>"
        Page.ClientScript.RegisterStartupScript(GetType(Page), "ShowMessage", scriptString)

    End Sub

End Class
