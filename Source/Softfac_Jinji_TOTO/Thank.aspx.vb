Imports System
Imports System.Data

Partial Class Thank
    Inherits System.Web.UI.Page

    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting, myDS, myDS1 As New DataSet, MyDT As DataTable
    Dim ssql, ssql1 As String
    Dim strPath As String = "Images"
    Dim objSubsCookie As New HttpCookie("myTheme")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Session("Module") = "SYSTEM_MANAGER"
            Session("LoginStatus") = ""
            Session("CompanyName") = ""

            PagePreload()

            If Request.QueryString("error").ToString() = "fail" Then
                lblTitle.Text = "Delivery has failed to these recipients or distribution lists:"
                lblTitle2.Text = "The recipient's e-mail address was not found in the recipient's e-mail system. Microsoft Exchange will not try to redeliver this message for you. "
                lblTitle3.Text = "Please check the e-mail address and try resending this message, or provide the following diagnostic text to your system administrator."
                lblTitle4.Text = "Email : " + Session("PassReco_EM").ToString()
            Else
                'lblSysName.Text = "Human Capital Resources Management"
                lblTitle.Text = "Password Recovery Successful"
                lblTitle2.Text = "Your password have been reset."
                lblTitle3.Text = "Please check your email for your new temporary password."
            End If

            'lblTitle4.Text = "Employee/User Code : " & Session("PassReco_ID").ToString
            'lblTitle4.Visible = False
            lblTitle5.Text = "Password : " & Session("PassReco_PS").ToString
            lblTitle5.Visible = False

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

        divThank.Style.Add("margin-left ", strXplus)
        divThank.Style.Add("margin-top ", strYplus)


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
        'imgSubmit.Src = strPath & "/Company/Default/" & Session("strTheme") & "/btnSubmit.png"
        'imgSubmit2.Src = strPath & "/Company/Default/" & Session("strTheme") & "/btnSubmit.png"

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

End Class
