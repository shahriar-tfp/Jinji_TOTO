Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class Pages_TimeSheet
    Inherits System.Web.UI.Page

#Region "Public Declaration"
    Private WithEvents mySQL As New clsSQL, myDS As New DataSet, myDS1 As New DataSet, myDS2 As New DataSet, mySetting As New clsGlobalSetting
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Public ssql As String, i As Integer, j As Integer, SearchByPage As Boolean, RecFound As Boolean, CountRecord As Integer
    Public AllowView As Boolean, AllowInsert As Boolean, AllowUpdate As Boolean, AllowDelete As Boolean, AllowPrint As Boolean
    Public _currentPageNumber As Integer, _totalPage As Double = 1, _totalRecords As Integer
    Public myDE As New DictionaryEntry, myHT As Hashtable
    Public myDataType As New clsGlobalSetting.DataType, SearchByFilter As Boolean = False
    Dim intColumn As Integer
#End Region
#Region "Private Declaration"
    Private WithEvents myDT1 As New DataTable, myDT2 As New DataTable
    Dim myDR1 As DataRow, myDR2 As DataRow, rcPerPage As Integer, autonum As Integer
    Dim ssql1 As String, ssql2 As String, ssql3 As String, ssql4 As String
    Dim rowPosition As String = "R_", rowPositionM As String = "RM_", panelPosition As String = "Display_"
    Dim buttonPosition1 As String = "B1_", buttonPosition2 As String = "B2_", buttonPosition3 As String = "B3_", labelPosition1 As String = "L1_"
    Dim btnColourDef, btnColourAlt As String
    Dim strPath As String = "../Images"
    Dim logic As Boolean
    Dim sysYear As Integer, counter As Integer

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.IsNewSession Then
            Response.Redirect("../Global/SessionTimeOut.aspx")
        End If
        If Not IsPostBack Then
            If Session("ScreenWidth") = 0 Then
                Session("ScreenWidth") = "1024"
                Session("GVwidth") = Session("ScreenWidth") - 360
            End If
            If Session("ScreenHeight") = 0 Then
                Session("ScreenHeight") = "768"
                Session("GVheight") = (Session("ScreenHeight") / 2) - 50
            End If
            'pnlGridview.Width = CInt(Session("GVwidth")) - 20
            'pnlGridview.Height = CInt(Session("GVheight"))

            PagePreload()
        End If
    End Sub

    Private Sub PagePreload()
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")
        pnlPayroll.Visible = False
        ddlEmployee_Profile_ID.Enabled = False
        txtCompany_Profile_code.Enabled = False
        mySetting.GetBtnImgUrl(imgBtnSubmit, Session("Company").ToString, btnColourDef, "btnSubmit.png")
        mySetting.GetBtnImgUrl(imgBtnSave, Session("Company").ToString, btnColourDef, "btnSave.png")
        ssql = "Exec sp_ts_SelTimeSheet 'EMPLOYEE','" & Session("Company").ToString & "','" & Session("EmpID").ToString & "'"
        myDS = mySQL.ExecuteSQL(ssql)

        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                txtCompany_Profile_code.Text = myDS.Tables(0).Rows(0).Item(2).ToString
            End If
        End If
        If myDS.Tables(0).Rows.Count > 1 Then
            ddlEmployee_Profile_ID.Enabled = True
        End If
        ddlEmployee_Profile_ID.DataTextField = "Name"
        ddlEmployee_Profile_ID.DataValueField = "Code"
        ddlEmployee_Profile_ID.DataSource = myDS
        ddlEmployee_Profile_ID.DataBind()
        ddlEmployee_Profile_ID.SelectedIndex = 0
        ssql = Nothing
        myDS = Nothing

        ssql = "Exec sp_ts_SelTimeSheet 'MONTH'"
        myDS = mySQL.ExecuteSQL(ssql)
        ddlMonth.DataTextField = "Name"
        ddlMonth.DataValueField = "Code"
        ddlMonth.DataSource = myDS
        ddlMonth.DataBind()
        ssql = Nothing
        myDS = Nothing

        ssql = "Exec sp_ts_SelTimeSheet 'YEAR'"
        myDS = mySQL.ExecuteSQL(ssql)
        ddlYear.DataTextField = "Name"
        ddlYear.DataValueField = "Code"
        ddlYear.DataSource = myDS
        ddlYear.DataBind()
        ssql = Nothing
        myDS = Nothing
    End Sub

    Protected Sub ddlMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMonth.SelectedIndexChanged
        If ddlMonth.SelectedValue <> "" And ddlYear.SelectedValue <> "0" And ddlEmployee_Profile_ID.SelectedValue <> "" And txtCompany_Profile_code.Text <> "" Then
            GetAttendanceHeader()
            GetAttendanceDetails()
        End If
    End Sub

    Private Sub GetAttendanceDetails()
        ssql = "Exec sp_ts_SelTimeSheet 'ATTENDANCEDETAIL','" & txtCompany_Profile_code.Text & "','" & ddlEmployee_Profile_ID.SelectedValue & _
        "','" & ddlMonth.SelectedValue & "','" & ddlYear.SelectedValue & "'"
        myDS = mySQL.ExecuteSQL(ssql)

        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                For i = 0 To myDS.Tables(0).Rows.Count - 1
                    Dim myWKSDayTXT As TextBox = Page.FindControl("txtWKSDay" & myDS.Tables(0).Rows(i).Item(0).ToString)
                    Dim myWKEDayTXT As TextBox = Page.FindControl("txtWKEDay" & myDS.Tables(0).Rows(i).Item(0).ToString)
                    Dim myOTHSDayTXT As TextBox = Page.FindControl("txtOTHSDay" & myDS.Tables(0).Rows(i).Item(0).ToString)
                    Dim myOTHEDayTXT As TextBox = Page.FindControl("txtOTHEDay" & myDS.Tables(0).Rows(i).Item(0).ToString)
                    Dim myRemarkTXT As TextBox = Page.FindControl("txtRemark" & myDS.Tables(0).Rows(i).Item(0).ToString)
                    Dim myDayLabel As Label = Page.FindControl("lblDay" & myDS.Tables(0).Rows(i).Item(0).ToString)
                    Dim myWKHLabel As Label = Page.FindControl("lblWKH" & myDS.Tables(0).Rows(i).Item(0).ToString)
                    Dim myOTHLabel As Label = Page.FindControl("lblOTH" & myDS.Tables(0).Rows(i).Item(0).ToString)

                    myDayLabel.Text = myDS.Tables(0).Rows(i).Item(1).ToString
                    myWKSDayTXT.Text = myDS.Tables(0).Rows(i).Item(2).ToString
                    myWKEDayTXT.Text = myDS.Tables(0).Rows(i).Item(3).ToString
                    myOTHSDayTXT.Text = myDS.Tables(0).Rows(i).Item(5).ToString
                    myOTHEDayTXT.Text = myDS.Tables(0).Rows(i).Item(6).ToString
                    myRemarkTXT.Text = myDS.Tables(0).Rows(i).Item(8).ToString
                    myWKHLabel.Text = myDS.Tables(0).Rows(i).Item(4).ToString
                    myOTHLabel.Text = myDS.Tables(0).Rows(i).Item(7).ToString
                Next
            End If
        End If
        ssql = Nothing
        myDS = Nothing
    End Sub
    Private Sub ClearText()
        For i = 1 To 31
            Dim myWKSDayTXT As TextBox = Page.FindControl("txtWKSDay" & i)
            Dim myWKEDayTXT As TextBox = Page.FindControl("txtWKEDay" & i)
            Dim myOTHSDayTXT As TextBox = Page.FindControl("txtOTHSDay" & i)
            Dim myOTHEDayTXT As TextBox = Page.FindControl("txtOTHEDay" & i)
            Dim myRemarkTXT As TextBox = Page.FindControl("txtRemark" & i)
            Dim myDayLabel As Label = Page.FindControl("lblDay" & i)
            Dim myWKHLabel As Label = Page.FindControl("lblWKH" & i)
            Dim myOTHLabel As Label = Page.FindControl("lblOTH" & i)

            myWKSDayTXT.Text = ""
            myWKEDayTXT.Text = ""
            myOTHSDayTXT.Text = ""
            myOTHEDayTXT.Text = ""
            myRemarkTXT.Text = ""
            myDayLabel.Text = ""
            myWKHLabel.Text = ""
            myOTHLabel.Text = ""
        Next
        lblWKHTotal.Text = ""
        lblOTHTotal.Text = ""
        txtPay.Text = ""
        txtALBAL.Text = ""
        txtAdj.Text = ""
        txtSA.Text = ""
        txtNPL.Text = ""
        txtOT15x.Text = ""
        txtOT20x.Text = ""
        txtOT30x.Text = ""
        txtOther.Text = ""
        txtAF.Text = ""
    End Sub

    Private Sub TextSet(ByVal Status As Boolean)
        For i = 1 To 31
            Dim myWKSDayTXT As TextBox = Page.FindControl("txtWKSDay" & i)
            Dim myWKEDayTXT As TextBox = Page.FindControl("txtWKEDay" & i)
            Dim myOTHSDayTXT As TextBox = Page.FindControl("txtOTHSDay" & i)
            Dim myOTHEDayTXT As TextBox = Page.FindControl("txtOTHEDay" & i)
            Dim myRemarkTXT As TextBox = Page.FindControl("txtRemark" & i)

            myWKSDayTXT.Enabled = status
            myWKEDayTXT.Enabled = status
            myOTHSDayTXT.Enabled = status
            myOTHEDayTXT.Enabled = status
            myRemarkTXT.Enabled = status
        Next
        btnCal.Enabled = status
        imgBtnSave.Enabled = status
        imgBtnSubmit.Enabled = status
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlYear.SelectedIndexChanged
        If ddlMonth.SelectedValue <> "" And ddlYear.SelectedValue <> "0" And ddlEmployee_Profile_ID.SelectedValue <> "" And txtCompany_Profile_code.Text <> "" Then
            GetAttendanceHeader()
            GetAttendanceDetails()
        End If
    End Sub

    Private Sub GetAttendanceHeader()
        ClearText()
        ssql = "Exec sp_ts_SelTimeSheet 'ATTENDANCEHEADER','" & txtCompany_Profile_code.Text & "','" & ddlEmployee_Profile_ID.SelectedValue & _
        "','" & ddlMonth.SelectedValue & "','" & ddlYear.SelectedValue & "'"
        myDS = mySQL.ExecuteSQL(ssql)

        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                If myDS.Tables(0).Rows(0).Item(0).ToString = "CHEQUE" Then
                    rdoCheque.Checked = True
                Else
                    rdoBank.Checked = True
                End If
                txtPay.Text = myDS.Tables(0).Rows(0).Item(1).ToString
                txtALBAL.Text = myDS.Tables(0).Rows(0).Item(2).ToString
                lblWKHTotal.Text = myDS.Tables(0).Rows(0).Item(1).ToString
                lblOTHTotal.Text = myDS.Tables(0).Rows(0).Item(2).ToString
                txtAdj.Text = myDS.Tables(0).Rows(0).Item(3).ToString
                txtSA.Text = myDS.Tables(0).Rows(0).Item(4).ToString
                txtNPL.Text = myDS.Tables(0).Rows(0).Item(5).ToString
                txtOT15x.Text = myDS.Tables(0).Rows(0).Item(6).ToString
                txtOT20x.Text = myDS.Tables(0).Rows(0).Item(7).ToString
                txtOT30x.Text = myDS.Tables(0).Rows(0).Item(8).ToString
                txtOther.Text = myDS.Tables(0).Rows(0).Item(9).ToString
                txtAF.Text = myDS.Tables(0).Rows(0).Item(10).ToString
                txtUser_Profile_code.Text = myDS.Tables(0).Rows(0).Item(11).ToString
                txtTelephoneNo.Text = myDS.Tables(0).Rows(0).Item(12).ToString
                If myDS.Tables(0).Rows(0).Item(13).ToString = "SUBMIT" Or myDS.Tables(0).Rows(0).Item(13).ToString = "APPROVED" Then
                    TextSet(False)
                Else
                    TextSet(True)
                End If
            End If
        End If
    End Sub

    Protected Sub imgBtnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSave.Click
        Dim paymode As String
        If rdoBank.Checked = True Then
            paymode = "BANK"
        Else
            paymode = "CHEQUE"
        End If
        ssql = "exec sp_ts_insUpdTimeSheet 'HEADER','" & txtCompany_Profile_code.Text & "','" & _
        ddlEmployee_Profile_ID.SelectedValue & "','" & ddlMonth.SelectedValue & "','" & ddlYear.SelectedValue & _
        "','" & paymode & "','" & txtPay.Text & "','" & txtALBAL.Text & "','" & txtAdj.Text & "','" & _
        txtSA.Text & "','" & txtNPL.Text & "','" & txtOT15x.Text & "','" & txtOT20x.Text & "','" & _
        txtOT30x.Text & "','" & txtOther.Text & "','" & txtAF.Text & "','" & _
        txtUser_Profile_code.Text & "','" & txtTelephoneNo.Text & "',''"
        myDS = mySQL.ExecuteSQL(ssql)

        ssql = Nothing
        myDS = Nothing
        For i = 1 To 31
            Dim myWKSDayTXT As TextBox = Page.FindControl("txtWKSDay" & i)
            Dim myWKEDayTXT As TextBox = Page.FindControl("txtWKEDay" & i)
            Dim myOTHSDayTXT As TextBox = Page.FindControl("txtOTHSDay" & i)
            Dim myOTHEDayTXT As TextBox = Page.FindControl("txtOTHEDay" & i)
            Dim myRemarkTXT As TextBox = Page.FindControl("txtRemark" & i)
            Dim myDayLabel As Label = Page.FindControl("lblDay" & i)
            Dim myWKHLabel As Label = Page.FindControl("lblWKH" & i)
            Dim myOTHLabel As Label = Page.FindControl("lblOTH" & i)
            If myDayLabel.Text <> "" Then

                ssql = "exec sp_ts_insUpdTimeSheet 'DETAIL','" & txtCompany_Profile_code.Text & "','" & _
                ddlEmployee_Profile_ID.SelectedValue & "','" & ddlMonth.SelectedValue & "','" & ddlYear.SelectedValue & _
                "','" & i & "','" & myWKSDayTXT.Text & "','" & myWKEDayTXT.Text & "','" & myWKHLabel.Text & _
                "','" & myOTHSDayTXT.Text & "','" & myOTHEDayTXT.Text & "','" & myOTHLabel.Text & "','" & _
                myRemarkTXT.Text & "','','','','','',''"
                myDS = mySQL.ExecuteSQL(ssql)

                ssql = Nothing
                myDS = Nothing
            End If
        Next
    End Sub

    Protected Sub btnCal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCal.Click
        Dim OTHour As Integer
        Dim OTMin As Integer
        Dim WKHour As Integer
        Dim WKMin As Integer
        Dim OTMinStr As String
        Dim WKMinStr As String

        For i = 1 To 31
            Dim myWKSDayTXT As TextBox = Page.FindControl("txtWKSDay" & i)
            Dim myWKEDayTXT As TextBox = Page.FindControl("txtWKEDay" & i)
            Dim myOTHSDayTXT As TextBox = Page.FindControl("txtOTHSDay" & i)
            Dim myOTHEDayTXT As TextBox = Page.FindControl("txtOTHEDay" & i)
            Dim myRemarkTXT As TextBox = Page.FindControl("txtRemark" & i)
            Dim myDayLabel As Label = Page.FindControl("lblDay" & i)
            Dim myWKHLabel As Label = Page.FindControl("lblWKH" & i)
            Dim myOTHLabel As Label = Page.FindControl("lblOTH" & i)

            If myWKSDayTXT.Text = "" Or myWKEDayTXT.Text = "" Then
                myWKHLabel.Text = "0:00"
            Else
                ssql = "Exec sp_ts_CalHour '" & myWKSDayTXT.Text & "','" & myWKEDayTXT.Text & "'"
                myDS = mySQL.ExecuteSQL(ssql)

                If myDS.Tables(0).Rows.Count > 0 Then
                    myWKHLabel.Text = myDS.Tables(0).Rows(0).Item(0).ToString + ":" + myDS.Tables(0).Rows(0).Item(1).ToString
                    WKHour = WKHour + myDS.Tables(0).Rows(0).Item(0).ToString
                    WKMin = WKMin + myDS.Tables(0).Rows(0).Item(1).ToString
                End If
                ssql = Nothing
                myDS = Nothing
            End If

            If myOTHSDayTXT.Text = "" Or myOTHEDayTXT.Text = "" Then
                myOTHLabel.Text = "0:00"
            Else
                ssql = "Exec sp_ts_CalHour '" & myOTHSDayTXT.Text & "','" & myOTHEDayTXT.Text & "'"
                myDS = mySQL.ExecuteSQL(ssql)

                If myDS.Tables(0).Rows.Count > 0 Then
                    myOTHLabel.Text = myDS.Tables(0).Rows(0).Item(0).ToString + ":" + myDS.Tables(0).Rows(0).Item(1).ToString
                    OTHour = OTHour + myDS.Tables(0).Rows(0).Item(0).ToString
                    OTMin = OTMin + myDS.Tables(0).Rows(0).Item(1).ToString
                End If
                ssql = Nothing
                myDS = Nothing
            End If
        Next
        If WKMin > 60 Then
            WKHour = WKHour + (WKMin / 60)
            WKMin = WKMin Mod 60
        End If
        If OTMin > 60 Then
            OTHour = OTHour + (OTMin / 60)
            OTMin = OTMin Mod 60
        End If
        OTMinStr = CStr(OTMin)
        If OTMinStr.Length = 1 Then
            OTMinStr = "0" + OTMinStr
        End If
        WKMinStr = CStr(WKMin)
        If WKMinStr.Length = 1 Then
            WKMinStr = "0" + WKMinStr
        End If
        txtALBAL.Text = CStr(OTHour) + ":" + OTMinStr
        txtPay.Text = CStr(WKHour) + ":" + WKMinStr
        lblOTHTotal.Text = CStr(OTHour) + ":" + OTMinStr
        lblWKHTotal.Text = CStr(WKHour) + ":" + WKMinStr
    End Sub

    Protected Sub imgBtnSubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSubmit.Click
        ssql = "exec sp_ts_insUpdTimeSheet 'SUBMIT','" & txtCompany_Profile_code.Text & "','" & _
        ddlEmployee_Profile_ID.SelectedValue & "','" & ddlMonth.SelectedValue & "','" & ddlYear.SelectedValue & _
        "','','','','','','','','','','','','','',''"
        myDS = mySQL.ExecuteSQL(ssql)
    End Sub

    Protected Sub ddlEmployee_Profile_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEmployee_Profile_ID.SelectedIndexChanged
        If ddlMonth.SelectedValue <> "" And ddlYear.SelectedValue <> "0" And ddlEmployee_Profile_ID.SelectedValue <> "" And txtCompany_Profile_code.Text <> "" Then
            GetAttendanceHeader()
            GetAttendanceDetails()
        End If
    End Sub
End Class
