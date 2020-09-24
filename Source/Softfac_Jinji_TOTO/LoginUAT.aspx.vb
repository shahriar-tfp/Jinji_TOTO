Imports System
Imports System.Data
Imports System.IO
Imports System.Diagnostics

Partial Class Login
    Inherits System.Web.UI.Page
    Private Declare Ansi Function GetPrivateProfileString _
          Lib "kernel32.dll" Alias "GetPrivateProfileStringA" _
          (ByVal lpApplicationName As String, _
          ByVal lpKeyName As String, ByVal lpDefault As String, _
          ByVal lpReturnedString As System.Text.StringBuilder, _
          ByVal nSize As Integer, ByVal lpFileName As String) _
          As Integer

    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting, myDS, myDS1 As New DataSet, MyDT As DataTable
    Dim ssql, ssql1 As String
    Dim strPath As String = "Images"
    Dim objSubsCookie As New HttpCookie("myTheme")

    Protected Sub Page_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DataBinding

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session("Module") = "SYSTEM_MANAGER"
            Session("Language") = "Eng"
            Session("LoginStatus") = ""
            Session("CompanyName") = "400"

            Dim basepath As String = Request.ServerVariables.Item("URL")
            basepath = basepath.Substring(0, basepath.LastIndexOf("/") + 1)
            Session("BasePath") = basepath

            'Items Control
            SetFieldToFalse()
            pnlEmp.Visible = True
            pnlPass.Visible = True
            pnlCom.Visible = True
            txtCompany.Visible = False
            Session("LoginStatus") = "ConfirmCom"
            ssql = "Select Code,name from company_Profile where Code='400'"
            myDS = mySQL.ExecuteSQL(ssql)
            If myDS.Tables.Count > 0 Then
                If myDS.Tables(0).Columns.Count > 1 Then
                    ddlCompany.DataTextField = "Name"
                    ddlCompany.DataValueField = "Code"
                    ddlCompany.DataSource = myDS
                    ddlCompany.DataBind()
                End If
            End If
            ssql = Nothing
            myDS = Nothing
            imgBtnLogin.Visible = True
            'lblSysName.Text = "Human Capital Resources Management"
            lblTitle.Text = "Please enter your Employee ID and Password"

            If mySQL.ConnectSQLServer = True Then
                Session.Timeout = mySetting.SessionTimeOut
                'set Resolution
                If Session("ScreenResolution") = "" Then
                    Session("PagePath") = "../../Login.aspx" 'determaine ur path here
                    Response.Redirect("Pages/Global/DetectScreen.aspx")
                Else
                    Dim nsArray() As String = Split(Session("ScreenResolution").ToString(), "x", 2)
                    Session("ScreenWidth") = CInt(nsArray(0))
                    Session("ScreenHeight") = CInt(nsArray(1))
                    Session("GVwidth") = Session("ScreenWidth") - 350
                    Session("GVheight") = (Session("ScreenHeight") / 2) - 50

                    If CInt(nsArray(0)) < 1024 And CInt(nsArray(1)) < 768 Then
                        ShowMessage("Please SET your Window resolution to 1024x768 or above for better view!")
                    End If
                    'lblscreenresolution.Text = CInt(nsArray(0)) & " (px) in width and " & CInt(nsArray(1)) & " (px) in height..."
                End If
                PagePreload()
            Else
                lblError.Visible = True
                lblError.Text = ""
                lblError.Text = "Failed to connect database. Please contact PIC."
            End If

            'RunNetAdmin()

        Else
            lblError.Visible = True
        End If
    End Sub

    Private Sub PagePreload()
        lblError.Visible = False
        lblError.Text = ""
        txtEmployeeID.Text = ""
        txtEmployeeID.Focus()


        'Default screen control
        Dim xPlus, yPlus As Integer
        Dim strXplus, strYplus As String

        xPlus = (CInt(Session("ScreenWidth")) - 560) / 2
        yPlus = (CInt(Session("ScreenHeight")) - 530) / 2
        strXplus = xPlus.ToString & "px"
        strYplus = yPlus.ToString & "px"

        divLogin.Style.Add("margin-left ", strXplus)
        divLogin.Style.Add("margin-top ", strYplus)


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
        imgBtnTheme1.ImageUrl = strPath & "/" & Session("strTheme") & "/theme_blue.gif"
        imgBtnTheme2.ImageUrl = strPath & "/" & Session("strTheme") & "/theme_brown.gif"
        imgBtnTheme3.ImageUrl = strPath & "/" & Session("strTheme") & "/theme_black.gif"
        imgBtnTheme4.ImageUrl = strPath & "/" & Session("strTheme") & "/theme_green.gif"
        imgBtnTheme5.ImageUrl = strPath & "/" & Session("strTheme") & "/theme_red.gif"
        imgBtnTheme6.ImageUrl = strPath & "/" & Session("strTheme") & "/theme_orange.gif"

        'set image button path
        imgBtnLogin.ImageUrl = strPath & "/Company/Default/" & Session("strTheme") & "/btnLogin.png"
        imgBtnSubmit.ImageUrl = strPath & "/Company/Default/" & Session("strTheme") & "/btnSubmit.png"
        imgBtnOk.ImageUrl = strPath & "/Company/Default/" & Session("strTheme") & "/btnOk.png"
        imgBtnYes.ImageUrl = strPath & "/Company/Default/" & Session("strTheme") & "/btnYes.png"
        imgBtnNo.ImageUrl = strPath & "/Company/Default/" & Session("strTheme") & "/btnNo.png"

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
        imgLayout11.Src = strPath & "/" & Session("strTheme") & "/login_hcrp.png"
        imgLayout12.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout13.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
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
        imgLayout29.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout30.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout31.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout32.Src = strPath & "/" & Session("strTheme") & "/blank.gif"

    End Sub

    Protected Sub imgBtnTheme1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnTheme1.Click
        Session("strTheme") = "Theme1"
        'Response.Cookies.Set(New HttpCookie("myTheme", Session("strTheme")))
        objSubsCookie.Value = Session("strTheme")
        objSubsCookie.Expires = DateAdd(DateInterval.Year, 1, Now)
        Response.Cookies.Set(objSubsCookie)
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub imgBtnTheme2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnTheme2.Click
        Session("strTheme") = "Theme2"
        'Response.Cookies.Set(New HttpCookie("myTheme", Session("strTheme")))
        objSubsCookie.Value = Session("strTheme")
        objSubsCookie.Expires = DateAdd(DateInterval.Year, 1, Now)
        Response.Cookies.Set(objSubsCookie)
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub imgBtnTheme3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnTheme3.Click
        Session("strTheme") = "Theme3"
        'Response.Cookies.Set(New HttpCookie("myTheme", Session("strTheme")))
        objSubsCookie.Value = Session("strTheme")
        objSubsCookie.Expires = DateAdd(DateInterval.Year, 1, Now)
        Response.Cookies.Set(objSubsCookie)
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub imgBtnTheme4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnTheme4.Click
        Session("strTheme") = "Theme4"
        'Response.Cookies.Set(New HttpCookie("myTheme", Session("strTheme")))
        objSubsCookie.Value = Session("strTheme")
        objSubsCookie.Expires = DateAdd(DateInterval.Year, 1, Now)
        Response.Cookies.Set(objSubsCookie)
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub imgBtnTheme5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnTheme5.Click
        Session("strTheme") = "Theme5"
        'Response.Cookies.Set(New HttpCookie("myTheme", Session("strTheme")))
        objSubsCookie.Value = Session("strTheme")
        objSubsCookie.Expires = DateAdd(DateInterval.Year, 1, Now)
        Response.Cookies.Set(objSubsCookie)
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub imgBtnTheme6_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnTheme6.Click
        Session("strTheme") = "Theme6"
        'Response.Cookies.Set(New HttpCookie("myTheme", Session("strTheme")))
        objSubsCookie.Value = Session("strTheme")
        objSubsCookie.Expires = DateAdd(DateInterval.Year, 1, Now)
        Response.Cookies.Set(objSubsCookie)
        Response.Redirect("Default.aspx")
    End Sub

    Public Function GetString(ByVal Section As String, ByVal Key As String, ByVal [Default] As String, ByVal strFileName As String) As String
        ' Returns a string from your INI file
        Dim intCharCount As Integer
        Dim objResult As New System.Text.StringBuilder(256)
        intCharCount = GetPrivateProfileString(Section, Key, [Default], objResult, objResult.Capacity, strFilename)
        If intCharCount > 0 Then
            GetString = Mid(objResult.ToString, 1, intCharCount)
        Else
            GetString = ""
        End If
    End Function

    Private Sub ShowMessage(ByVal message As String)

        Dim scriptString As String = "<script language=JavaScript>"
        scriptString += "alert('" + message + "');"
        scriptString += "<" & "/script>"
        Page.ClientScript.RegisterStartupScript(GetType(Page), "ShowMessage", scriptString)

    End Sub

    Private Sub WriteCookies()
        Dim strCookiesPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Cookies)
        Dim dirInfo As DirectoryInfo = New DirectoryInfo(strCookiesPath)
        Dim fileInfo As IO.FileInfo() = dirInfo.GetFiles()
        Dim subFileInfo As IO.FileInfo

        Dim strFileName As String = ""
        If File.Exists(strCookiesPath & "\" & Environment.UserName.ToString.ToLower & "@" & HttpContext.Current.Request.Url.Host & "[1].txt") Then
            strFileName = Environment.UserName.ToString.ToLower & "@" & HttpContext.Current.Request.Url.Host & "[1].txt"
        ElseIf File.Exists(strCookiesPath & "\" & Environment.UserName.ToString.ToLower & "@" & HttpContext.Current.Request.Url.Host & "[2].txt") Then
            strFileName = Environment.UserName.ToString.ToLower & "@" & HttpContext.Current.Request.Url.Host & "[2].txt"
        End If
        If strFileName.Trim = "" Then
            strFileName = Environment.UserName.ToString.ToLower & "@" & HttpContext.Current.Request.Url.Host & "[1].txt"
        End If
        If File.Exists(strCookiesPath & "\" & strFileName) = False Then
            Response.Cookies("userInfo")("userName") = "patrick"
            Response.Cookies("userInfo")("lastVisit") = DateTime.Now.ToString()
            Response.Cookies("userInfo").Expires = DateTime.Now.AddDays(1)

            Dim aCookie As New HttpCookie("userInfo")
            aCookie.Values("userName") = "patrick"
            aCookie.Values("lastVisit") = DateTime.Now.ToString()
            aCookie.Expires = DateTime.Now.AddDays(1)
            Response.Cookies.Add(aCookie)
        Else
            Dim booCount As Boolean = False
            For Each subFileInfo In fileInfo
                If subFileInfo.Name.ToString.Trim = strFileName Then
                    If File.Exists(strCookiesPath & "\" & strFileName) Then
                        'Request.Cookies("SoftFacHCRM").Values("User") = ""
                        'Request.Cookies("SoftFacHCRM").Values("Host") = ""
                        'Request.Cookies("SoftFacHCRM").Values("IP") = ""
                        'Request.Cookies("SoftFacHCRM").Values("MAC") = ""
                        'Request.Cookies("SoftFacHCRM").Values("CPU") = ""
                        Request.Cookies("SoftFacHCRM").Values("LastLogin") = Now()
                        Request.Cookies("SoftFacHCRM").Expires = Now.AddDays(1)
                        booCount = True
                        Exit For
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub WriteConfig()
        Dim strConfigPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Cookies)

        Dim strFileName As String = "SoftFac_HCRM.dat"
        Dim strFileFullPath As String = strConfigPath & "\" & "SoftFac_HCRM.dat"
        If File.Exists(strFileFullPath) = False Then
            Dim streamWriter As New StreamWriter(strFileFullPath)
            streamWriter.WriteLine("[Network]")
            streamWriter.WriteLine("Host=")
            streamWriter.WriteLine("User=")
            streamWriter.WriteLine("IP=")
            streamWriter.WriteLine("MAC=")
            streamWriter.WriteLine("CPU=")
            streamWriter.WriteLine("LastLogin=" & Now.Date.ToString)
            streamWriter.Close()
            streamWriter.Dispose()
            streamWriter = Nothing
        End If
    End Sub

    Private Sub RunNetAdmin()
        Try
            'Dim startinfo As System.Diagnostics.ProcessStartInfo
            'Dim pstart As New System.Diagnostics.Process
            ''startinfo = New System.Diagnostics.ProcessStartInfo(System.AppDomain.CurrentDomain.BaseDirectory & "Bin\NetConfig_1_0_0_3.application")
            'startinfo = New System.Diagnostics.ProcessStartInfo(System.AppDomain.CurrentDomain.BaseDirectory & "Bin\setup.exe")
            'pstart.StartInfo = startinfo
            'startinfo.UseShellExecute = True
            'startinfo.WindowStyle = ProcessWindowStyle.Maximized
            'pstart.Start()
            'System.Threading.Thread.Sleep(3000)
            ''pstart.Kill()
            ''pstart.WaitForExit(10000)
            'startinfo = Nothing
            'pstart = Nothing
            'Response.ContentType = "application\octet-stream"
            'Dim filename As String = New String(System.AppDomain.CurrentDomain.BaseDirectory & "Bin\setup.exe")
            'Dim downloadFile As System.IO.FileStream = New System.IO.FileStream(filename, IO.FileMode.Open)
            'Response.Write(downloadFile.Length() & "#")
            'downloadFile.Close()
            'Response.WriteFile(filename)
            'Response.Flush()
            'Response.End()
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub LoadNetAdmin()
        Try
            Dim scriptString As String = "<script language=JavaScript>"
            scriptString &= "window.open('Bin/setup.exe');"
            scriptString &= "<" & "/script>"
            Page.ClientScript.RegisterStartupScript(GetType(Page), "Net Config", scriptString)
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub WriteNetworkConfigToSQL()
        Dim strFileFullPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Cookies) & "\" & "SoftFac_HCRM.dat"
        Dim strHost As String = GetString("Network", "Host", "(none)", strFileFullPath)
        Dim strUser As String = GetString("Network", "User", "(none)", strFileFullPath)
        Dim strIP As String = GetString("Network", "IP", "(none)", strFileFullPath)
        Dim strMAC As String = GetString("Network", "MAC", "(none)", strFileFullPath)
        Dim strCPU As String = GetString("Network", "CPU", "(none)", strFileFullPath)
        Dim strLastLogin As String = GetString("Network", "LastLogin", "(none)", strFileFullPath)
        Dim htb As New Hashtable
        htb.Add("Host", strHost)
        htb.Add("User", strUser)
        htb.Add("IP", strIP)
        htb.Add("MAC", strMAC)
        htb.Add("CPU", strCPU)
        htb.Add("LastLogin", strLastLogin)
        Session("NetworkConfig") = htb
        Session("NetworkConfig") = CType(Session("NetworkConfig"), Hashtable)
        ssql = "Insert Into Security_Control(MAC_Address,IP_Address,Remote_Address,Computer_Name,Computer_Login_Name,Login_Datetime,Logout_Datetime,Company_Profile_Code,User_Profile_Code) " & _
                "Values('" & strMAC & "','" & strIP & "','" & Request.ServerVariables("Remote_Addr").ToString & "','" & strHost & "','" & strUser & _
                "',dbo.fn_GetCurrentDateTime(GetDate()),'19000101000000','" & Session("Company").ToString & "','" & Session("EmpID").ToString & "')"
        mySQL.ExecuteSQLNonQuery(ssql, Session("Company").ToString, Session("EmpID").ToString)
    End Sub

    Protected Sub imgBtnSubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSubmit.Click

        If ValidateInputNewPass() = False Then
            pnlResult.Visible = True
            lblError.Text = lblNewPassword.Text & " field is Required!"
            txtNewPassword.Focus()
            Exit Sub
        End If
        If ValidateInputNewPassC() = False Then
            pnlResult.Visible = True
            lblError.Text = lblCNewPassword.Text & " field is Required!"
            txtPassword.Focus()
            Exit Sub
        End If
        If txtNewPassword.Text <> txtCNewPassword.Text Then
            pnlResult.Visible = True
            lblError.Text = "New password not match!"
            txtNewPassword.Focus()
            Exit Sub
        End If

        If Session("LoginStatus") = "ChangePass" Then
            ssql = "Exec sp_sa_validateSystemLogin '" & "Change_Pass" & "','" & Session("Company") & "','" & Session("EmpID") & "','" & Session("TPass") & "','" & Trim(txtNewPassword.Text) & "'"
        ElseIf Session("LoginStatus") = "ChangePassEmp" Then
            ssql = "Exec sp_sa_validateSystemLogin '" & "Change_Pass_FTL_Emp" & "','" & Session("Company") & "','" & Session("EmpID") & "','" & Session("TPass") & "','" & Trim(txtNewPassword.Text) & "','" & Session("TString") & "'"
        ElseIf Session("LoginStatus") = "ChangePassUsr" Then
            ssql = "Exec sp_sa_validateSystemLogin '" & "Change_Pass_FTL_Usr" & "','" & Session("Company") & "','" & Session("EmpID") & "','" & Session("TPass") & "','" & Trim(txtNewPassword.Text) & "','" & Session("TString") & "'"
        End If

        Try
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables.Count > 0 Then
                If myDS.Tables(0).Rows.Count > 0 Then
                    If myDS.Tables(0).Rows(0).Item(0).ToString = "E08" Then
                        SetFieldToFalse()
                        pnlNewPass.Visible = True
                        pnlResult.Visible = True
                        imgBtnSubmit.Visible = True
                        lblError.Text = "Password lenght must at lease " & myDS.Tables(0).Rows(0).Item(4).ToString
                        txtNewPassword.Focus()

                    ElseIf myDS.Tables(0).Rows(0).Item(0).ToString = "E09" Then
                        SetFieldToFalse()
                        pnlNewPass.Visible = True
                        pnlResult.Visible = True
                        imgBtnSubmit.Visible = True
                        lblError.Text = "Mix of password is required!"
                        txtNewPassword.Focus()

                    ElseIf myDS.Tables(0).Rows(0).Item(0).ToString = "E10" Then
                        SetFieldToFalse()
                        pnlNewPass.Visible = True
                        pnlResult.Visible = True
                        imgBtnSubmit.Visible = True
                        lblError.Text = "Password should not start with " & myDS.Tables(0).Rows(0).Item(4).ToString
                        txtNewPassword.Focus()

                    ElseIf myDS.Tables(0).Rows(0).Item(0).ToString = "E13" Then
                        SetFieldToFalse()
                        pnlNewPass.Visible = True
                        pnlResult.Visible = True
                        imgBtnSubmit.Visible = True
                        lblError.Text = "Password should be differ with previous password!"
                        txtNewPassword.Focus()

                    ElseIf myDS.Tables(0).Rows(0).Item(0).ToString = "SUCCESS" Then
                        Session("Company") = myDS.Tables(0).Rows(0).Item(1).ToString
                        Session("EmpID") = myDS.Tables(0).Rows(0).Item(2).ToString
                        Session("Password") = myDS.Tables(0).Rows(0).Item(3).ToString

                        'Alert to change pass
                        ssql1 = "Exec sp_sa_validateSystemLogin '" & "Pass_Expired_Alert" & "','" & Session("Company") & "400','" & Session("EmpID") & "','" & "" & "','" & "" & "'"
                        myDS1 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
                        If myDS1.Tables(0).Rows.Count > 0 Then
                            If myDS1.Tables(0).Rows(0).Item(0).ToString = "E12" Then
                                lblTitle.Text = "Your password will be expired within " & myDS1.Tables(0).Rows(0).Item(4).ToString & " day(s). Click on [YES] to change it!"
                                SetFieldToFalse()
                                imgBtnYes.Visible = True
                                imgBtnNo.Visible = True
                            ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "SUCCESS" Then
                                lblTitle.Text = "Your password change successfully! Click on [Ok] to continue"
                                SetFieldToFalse()
                                imgBtnOk.Visible = True

                                'Enable pnl
                                'pnlEmp.Visible = True
                                'pnlPass.Visible = True
                                'Auto fill up
                                'lblEmployeeID.Enabled = False
                                'txtEmployeeID.Enabled = False
                                'txtEmployeeID.Text = myDS.Tables(0).Rows(0).Item(2).ToString
                                'lblPassword.Enabled = False
                                'txtPassword.Enabled = False
                                'txtPassword.Text = myDS.Tables(0).Rows(0).Item(3).ToString
                            End If
                        End If
                        myDS1 = Nothing

                    End If
                    '---------------------------------------------------------'
                    'E08 - Pass Not Meet System Requirement
                    'E09 - Pass Complexity Not Meet
                    'E10 - Pass Format Not meet System Requirement
                    'E13 - Similar with prev pass
                    'SUCCESS - First level checking pass 
                    '---------------------------------------------------------'
                End If
            End If

        Catch ex As Exception
            pnlResult.Visible = True
            lblError.Text = "Error By " & ex.Source & " : " & ex.Message.ToString
            myDS = Nothing
        End Try
        myDS = Nothing

    End Sub

    Protected Sub imgBtnLogin_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnLogin.Click
        Dim sql As New clsSQL
        HttpContext.Current.Session("ConnectionString") = sql.GetConnectionString()
        HttpContext.Current.Session("ProviderString") = sql.GetProviderString()

        Call rdServer.rdSession.SessionStart()
        If Session("LoginStatus") = "AccountLock" Then
            lblTitle.Text = "Please enter your Employee ID and Password"
        End If

        If Session("LoginStatus") = "ConfirmCom" Then
            If ValidateInputCom() = False Then
                pnlResult.Visible = True
                lblError.Text = lblCompany.Text & " field is Required!"
                txtCompany.Focus()
                ddlCompany.Focus()
                Exit Sub
            End If
        Else
            If ValidateInputID() = False And ValidateInputPass() = False Then
                pnlResult.Visible = True
                lblError.Text = lblEmployeeID.Text & " & " & lblPassword.Text & " field are Required!"
                txtEmployeeID.Focus()
                Exit Sub
            End If
            If ValidateInputID() = False Then
                pnlResult.Visible = True
                lblError.Text = lblEmployeeID.Text & " field is Required!"
                txtEmployeeID.Focus()
                Exit Sub
            End If
            If ValidateInputPass() = False Then
                pnlResult.Visible = True
                lblError.Text = lblPassword.Text & " field is Required!"
                txtPassword.Focus()
                Exit Sub
            End If
        End If

        'employee validation
        Try
            If Session("LoginStatus") = "ConfirmCom" And txtCompany.Visible = True Then
                ssql = "Exec sp_sa_validateSystemLogin '" & "Employee_Validation" & "','" & Trim(txtCompany.Text) & "','" & Session("EmpID") & "','" & Session("TPass") & "','" & "" & "'"
            ElseIf txtCompany.Visible = True Then
                ssql = "Exec sp_sa_validateSystemLogin '" & "Employee_Validation" & "','" & Trim(txtCompany.Text) & "','" & Trim(txtEmployeeID.Text) & "','" & Trim(txtPassword.Text) & "','" & Trim(txtNewPassword.Text) & "'"
            ElseIf Session("LoginStatus") = "ConfirmCom" And txtCompany.Visible = False Then
                ssql = "Exec sp_sa_validateSystemLogin '" & "Employee_Validation" & "','" & Trim(ddlCompany.SelectedValue) & "','" & Trim(txtEmployeeID.Text) & "','" & Trim(txtPassword.Text) & "','" & Trim(txtNewPassword.Text) & "'"
            ElseIf txtCompany.Visible = False Then
                ssql = "Exec sp_sa_validateSystemLogin '" & "Employee_Validation" & "','" & Trim(ddlCompany.SelectedValue) & "','" & Trim(txtEmployeeID.Text) & "','" & Trim(txtPassword.Text) & "','" & Trim(txtNewPassword.Text) & "'"
            End If
            myDS = mySQL.ExecuteSQL(ssql)
            If myDS.Tables.Count > 0 Then
                If myDS.Tables(0).Rows.Count > 0 Then

                    If myDS.Tables(0).Rows(0).Item(0).ToString = "E01" Or myDS.Tables(0).Rows(0).Item(0).ToString = "E02" Then
                        SetFieldToFalse()
                        pnlResult.Visible = True
                        lblError.Text = "Invalid " & lblEmployeeID.Text & " Or " & lblPassword.Text
                        lblTitle.Text = "Please enter your Employee ID and Password"
                        pnlEmp.Visible = True
                        pnlPass.Visible = True
                        pnlCom.Visible = True
                        imgBtnLogin.Visible = True
                        txtEmployeeID.Focus()
                        Session("LoginStatus") = "InvalidUser"

                    ElseIf myDS.Tables(0).Rows(0).Item(0).ToString = "E03" Then
                        SetFieldToFalse()
                        pnlResult.Visible = True
                        lblError.Text = "Invalid " & lblEmployeeID.Text & " Or " & lblPassword.Text
                        lblTitle.Text = "Please enter your Employee ID and Password"
                        pnlEmp.Visible = True
                        pnlPass.Visible = True
                        pnlCom.Visible = True
                        imgBtnLogin.Visible = True
                        txtEmployeeID.Focus()
                        Session("LoginStatus") = "InvalidPass"

                    ElseIf myDS.Tables(0).Rows(0).Item(0).ToString = "E04" Then
                        SetFieldToFalse()
                        SetFieldToDefault()
                        pnlResult.Visible = True
                        lblError.Text = "Your account has been locked..."
                        lblTitle.Text = "Please contact your Admin about this issue"
                        Session("LoginStatus") = "AccountLock"

                    ElseIf myDS.Tables(0).Rows(0).Item(0).ToString = "E05" Or myDS.Tables(0).Rows(0).Item(0).ToString = "E14" Then
                        SetFieldToFalse()
                        lblTitle.Text = "Please enter your new password (password change: on first time login)"
                        pnlNewPass.Visible = True
                        txtNewPassword.Focus()
                        imgBtnSubmit.Visible = True
                        Session("Company") = myDS.Tables(0).Rows(0).Item(1).ToString
                        Session("EmpID") = myDS.Tables(0).Rows(0).Item(2).ToString
                        Session("TPass") = myDS.Tables(0).Rows(0).Item(3).ToString
                        Session("TString") = myDS.Tables(0).Rows(0).Item(4).ToString.Replace("'", "''")
                        If myDS.Tables(0).Rows(0).Item(0).ToString = "E05" Then
                            Session("LoginStatus") = "ChangePassEmp"
                        Else
                            Session("LoginStatus") = "ChangePassUsr"
                        End If

                    ElseIf myDS.Tables(0).Rows(0).Item(0).ToString = "E06" Then
                        SetFieldToFalse()
                        lblTitle.Text = "Please enter your new password (password change: request by system)"
                        pnlNewPass.Visible = True
                        txtNewPassword.Focus()
                        imgBtnSubmit.Visible = True
                        Session("Company") = myDS.Tables(0).Rows(0).Item(1).ToString
                        Session("EmpID") = myDS.Tables(0).Rows(0).Item(2).ToString
                        Session("TPass") = myDS.Tables(0).Rows(0).Item(3).ToString
                        Session("MessageType") = myDS.Tables(0).Rows(0).Item(5).ToString
                        Session("LoginStatus") = "ChangePass"

                    ElseIf myDS.Tables(0).Rows(0).Item(0).ToString = "E07" Then
                        SetFieldToFalse()
                        lblTitle.Text = "Please enter your new password (password change: expired)"
                        pnlNewPass.Visible = True
                        txtNewPassword.Focus()
                        imgBtnSubmit.Visible = True
                        Session("Company") = myDS.Tables(0).Rows(0).Item(1).ToString
                        Session("EmpID") = myDS.Tables(0).Rows(0).Item(2).ToString
                        Session("TPass") = myDS.Tables(0).Rows(0).Item(3).ToString
                        Session("MessageType") = myDS.Tables(0).Rows(0).Item(5).ToString
                        Session("LoginStatus") = "ChangePass"

                    ElseIf myDS.Tables(0).Rows(0).Item(0).ToString = "E11" Then
                        SetFieldToFalse()
                        lblTitle.Text = "Please enter your company code"
                        pnlCom.Visible = True
                        imgBtnLogin.Visible = True
                        txtCompany.Focus()
                        Session("EmpID") = myDS.Tables(0).Rows(0).Item(2).ToString
                        Session("TPass") = myDS.Tables(0).Rows(0).Item(3).ToString
                        Session("MessageType") = myDS.Tables(0).Rows(0).Item(5).ToString
                        Session("LoginStatus") = "ConfirmCom"

                    ElseIf myDS.Tables(0).Rows(0).Item(0).ToString = "SUCCESS" Then
                        Session("Company") = myDS.Tables(0).Rows(0).Item(1).ToString
                        Session("EmpID") = myDS.Tables(0).Rows(0).Item(2).ToString
                        Session("Password") = myDS.Tables(0).Rows(0).Item(3).ToString
                        Session("MessageType") = myDS.Tables(0).Rows(0).Item(5).ToString
                        Session("CompanyName") = myDS.Tables(0).Rows(0).Item(6).ToString

                        'Alert to change pass
                        ssql1 = "Exec sp_sa_validateSystemLogin '" & "Pass_Expired_Alert" & "','" & Session("Company") & "','" & Session("EmpID") & "','" & "" & "','" & "" & "'"
                        myDS1 = mySQL.ExecuteSQL(ssql1)
                        If myDS1.Tables(0).Rows.Count > 0 Then
                            If myDS1.Tables(0).Rows(0).Item(0).ToString = "E12" Then
                                lblTitle.Text = "Your password will be expired within " & myDS1.Tables(0).Rows(0).Item(4).ToString & " day(s). Click on [YES] to change it!"
                                SetFieldToFalse()
                                imgBtnYes.Visible = True
                                imgBtnNo.Visible = True
                            ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "SUCCESS" Then
                                Session("LoginStatus") = "SUCCESS"
                                WriteNetworkConfigToSQL()
                                Response.Redirect("Default.aspx")
                            End If
                        End If
                        myDS1 = Nothing
                    End If

                    '---------------------------------------------------------'
                    'E01 - Employee Code Not Exist
                    'E02 - Company Not Found - Bcos Of Employee Code Not Exist
                    'E03 - Pass Not Correct
                    'E04 - Account Has Been Locked
                    'E05 - Required To Change Pass (FTL_emp)
                    'E14 - Required To Change Pass (FTL_usr)
                    'E06 - Required To Change Pass (Force To Change)
                    'E07 - Required To Change Pass (Expired)
                    'E08 - Pass Not Meet System Requirement
                    'E09 - Pass Complexity Not Meet
                    'E10 - Pass Format Not meet System Requirement
                    'E11 - More Than One Company Found
                    'E12 - System Alert to Change Password
                    'SUCCESS - First level checking pass 
                    '---------------------------------------------------------'

                End If
            End If
        Catch ex As Exception
            pnlResult.Visible = True
            lblError.Text = "Error By " & ex.Source & " : " & ex.Message.ToString
            myDS = Nothing
        End Try
        myDS = Nothing

    End Sub

    Protected Sub imgBtnYes_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnYes.Click

        lblTitle.Text = "Please enter your new password"
        SetFieldToFalse()
        pnlNewPass.Visible = True
        txtNewPassword.Focus()
        imgBtnSubmit.Visible = True
        Session("LoginStatus") = "ChangePass"

    End Sub

    Protected Sub imgBtnNo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnNo.Click
        Session("LoginStatus") = "SUCCESS"
        WriteNetworkConfigToSQL()
        Response.Redirect("Default.aspx")

    End Sub

    Protected Sub imgBtnOk_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnOk.Click
        Session("LoginStatus") = "SUCCESS"
        WriteNetworkConfigToSQL()
        Response.Redirect("Default.aspx")

    End Sub

    Private Function ValidateInputID() As Boolean
        If Trim(txtEmployeeID.Text) = "" Then
            Return False
        End If
        Return True
    End Function

    Private Function ValidateInputPass() As Boolean
        If Trim(txtPassword.Text) = "" Then
            Return False
        End If
        Return True
    End Function

    Private Function ValidateInputNewPass() As Boolean
        If Trim(txtNewPassword.Text) = "" Then
            Return False
        End If
        Return True
    End Function

    Private Function ValidateInputNewPassC() As Boolean
        If Trim(txtCNewPassword.Text) = "" Then
            Return False
        End If
        Return True
    End Function

    Private Function ValidateInputCom() As Boolean
        If Trim(txtCompany.Text) = "" And txtCompany.Visible = True Then
            Return False
        ElseIf ddlCompany.SelectedValue = "" And ddlCompany.Visible = True Then
            Return False
        End If
        Return True
    End Function

    Sub SetFieldToFalse()
        pnlEmp.Visible = False
        pnlPass.Visible = False
        pnlNewPass.Visible = False
        pnlCom.Visible = False
        pnlResult.Visible = False
        imgBtnLogin.Visible = False
        imgBtnSubmit.Visible = False
        imgBtnYes.Visible = False
        imgBtnNo.Visible = False
        imgBtnOk.Visible = False

        txtEmployeeID.Text = ""
        txtPassword.Text = ""
        txtNewPassword.Text = ""
        txtCNewPassword.Text = ""
        txtCompany.Text = ""
    End Sub

    Sub SetFieldToDefault()
        pnlEmp.Visible = True
        pnlPass.Visible = True
        imgBtnLogin.Visible = True
        pnlCom.Visible = True
    End Sub

    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad

    End Sub
End Class

