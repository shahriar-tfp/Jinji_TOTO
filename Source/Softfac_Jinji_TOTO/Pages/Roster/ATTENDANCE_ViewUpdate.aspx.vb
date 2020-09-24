Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.DateTime
Imports System.Globalization

Partial Class Pages_Roster_ATTENDANCE_ViewUpdate
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
        Session("Module") = "ROSTER"
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")

        mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourDef, "btnSearch.png")
        mySetting.GetBtnImgUrl(imgBtnSubmit, Session("Company").ToString, btnColourDef, "btnSubmit.png")
        mySetting.GetBtnImgUrl(ImgBtnDelete, Session("Company").ToString, btnColourDef, "btnDelete.png")
        mySetting.GetBtnImgUrl(imgBtnSelect, Session("Company").ToString, btnColourDef, "btnSelect.png")

        mySetting.GetImgUrl(imgKeyDATEFROM, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyDATETO, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyEMPLOYEE_PROFILE_ID, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnDATEFROM, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnDATETO, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnEMPLOYEE_PROFILE_ID, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgBtnDateIn, clsGlobalSetting.ImageType._DATETIME, Session("Company").ToString)
        mySetting.GetImgUrl(imgBtnDateOut, clsGlobalSetting.ImageType._DATETIME, Session("Company").ToString)
        mySetting.GetBtnImgUrl(imgbtnReProcess, Session("Company").ToString, btnColourDef, "btnProcess.png")
        mySetting.DateTimePicker_ImageButton(imgBtnDateIn, Form.ID, "txtDateIn")
        mySetting.DateTimePicker_ImageButton(imgBtnDateOut, Form.ID, "txtDateOut")
        mySetting.GetLookupValue_ImageButton(imgbtnEmployee, Form.ID, "txtEmployee", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "EMPLOYEE_PROFILE_ID" & """," & """" & """," & """" & Session("EmpID").ToString & """")
        mySetting.GetImgBtnUrl(imgbtnEmployee, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)

        mySetting.GetImgTypeUrl(imgTop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Filter.png")
        mySetting.GetImgTypeUrl(imgBottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")

        lblReason.Visible = True
        txtReason.Visible = True
        imgbtnReason.Visible = False

        'get Page Title
        ssql = "exec sp_sa_GetPageTitle '" & Form.ID & "'"
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            lblTitle.Text = myDS.Tables(0).Rows(0).Item(0)
            lblTitle.CssClass = "wordstyle4"
        End If
        myDS = Nothing

        myDS = New DataSet
        myDS = mySetting.GetPageFieldSetting(Session("Company"), Form.ID, Session("EmpID"))
        If myDS.Tables.Count > 1 Then
            myDT1 = New DataTable
            myDT1 = myDS.Tables(0)
            myDT2 = New DataTable
            myDT2 = myDS.Tables(1)

            If myDT1.Rows.Count > 0 Then
                myDR1 = myDT1.Rows(0)
                Page.Title = myDR1(1)
                If myDR1(3) = "YES" Then
                    AllowView = True
                    Session("View") = "TRUE"
                Else
                    AllowView = False
                    Session("View") = "FALSE"
                    pnlEdit.Visible = False
                    ShowMessage("You are not allow to view this page!")
                    Exit Sub
                End If

                If myDR1(4) = "YES" Then
                    AllowInsert = True
                    Session("Insert") = "TRUE"
                    imgBtnSubmit.Visible = True

                Else
                    AllowInsert = False
                    Session("Insert") = "FALSE"
                    imgBtnSubmit.Visible = False
                End If
                If myDR1(5) = "YES" Then
                    AllowUpdate = True
                    Session("Update") = "TRUE"
                Else
                    AllowUpdate = False
                    Session("Update") = "FALSE"
                End If
                If myDR1(6) = "YES" Then
                    AllowDelete = True
                    Session("Delete") = "TRUE"
                    ImgBtnDelete.Visible = True
                Else
                    AllowDelete = False
                    Session("Delete") = "FALSE"
                    ImgBtnDelete.Visible = False
                End If
                If myDR1(7) = "YES" Then
                    AllowPrint = True
                    Session("Print") = "TRUE"
                Else
                    AllowPrint = False
                    Session("Print") = "FALSE"
                End If

                imgBtnPrint.Visible = AllowPrint
            End If

            If myDT2.Rows.Count > 0 Then
                For i = 0 To myDT2.Rows.Count - 1
                    Dim myLabel As Label = Page.FindControl("lbl" & myDT2.Rows(i).Item(2).ToString)
                    Dim myImageButton As ImageButton = Page.FindControl("imgbtn" & myDT2.Rows(i).Item(2).ToString)
                    Dim myImageKey As ImageButton = Page.FindControl("imgkey" & myDT2.Rows(i).Item(2).ToString)

                    myLabel.Text = myDT2.Rows(i).Item(3).ToString
                    myLabel.CssClass = "wordstyle12"
                    mySetting.GetImgUrl(myImageKey, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
                    Select Case myDT2.Rows(i).Item(6).ToString
                        Case "DATE"
                            mySetting.GetImgBtnUrl(myImageButton, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
                        Case "DATETIME"
                            mySetting.GetImgBtnUrl(myImageButton, clsGlobalSetting.ImageType._DATETIME, Session("Company").ToString)
                        Case "TIME"
                            mySetting.GetImgBtnUrl(myImageButton, clsGlobalSetting.ImageType._TIME, Session("Company").ToString)
                        Case Else
                            mySetting.GetImgBtnUrl(myImageButton, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
                    End Select
                    If myDT2.Rows(i).Item(9).ToString = "NO" Then
                        myLabel.Visible = False
                        myImageKey.Visible = False
                        myImageButton.Visible = False
                        Select Case myDT2.Rows(i).Item(6).ToString
                            Case "OPTION"
                                Dim myDDL As DropDownList = Page.FindControl("ddl" & myDT2.Rows(i).Item(2).ToString)
                                mySetting.GetDropdownlistValue(Form.ID, myDT2.Rows(i).Item(2).ToString, myDDL)
                                myDDL.Visible = False
                                myDDL = Nothing
                                myImageButton.Visible = False
                            Case Else
                                Dim myTextBox As TextBox = Page.FindControl("txt" & myDT2.Rows(i).Item(2).ToString)
                                myTextBox.Visible = False
                                myTextBox = Nothing
                        End Select
                    End If
                    'Else
                    Select Case myDT2.Rows(i).Item(6).ToString
                        Case "OPTION"
                            Dim myDDL As DropDownList = Page.FindControl("ddl" & myDT2.Rows(i).Item(2).ToString)
                            mySetting.GetDropdownlistValue(Form.ID, myDT2.Rows(i).Item(2).ToString, myDDL)
                            myDDL = Nothing
                            myImageButton.Visible = False
                        Case "LOOKUP"
                            mySetting.GetLookupValue_ImageButton(myImageButton, Form.ID, "txt" & myDT2.Rows(i).Item(2).ToString, "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & myDT2.Rows(i).Item(2).ToString & """," & """" & """," & """" & Session("EmpID").ToString & """")
                            Dim myTextBox As TextBox = Page.FindControl("txt" & myDT2.Rows(i).Item(2).ToString)
                            myTextBox.MaxLength = CInt(myDT2.Rows(i).Item(7).ToString)
                            myTextBox = Nothing
                        Case "DATE"
                            mySetting.PopUpCalendar_ImageButton(myImageButton, Form.ID, "txt" & myDT2.Rows(i).Item(2).ToString)
                            Dim myTextBox As TextBox = Page.FindControl("txt" & myDT2.Rows(i).Item(2).ToString)
                            myTextBox.MaxLength = CInt(myDT2.Rows(i).Item(7).ToString)
                            myTextBox = Nothing
                        Case "DATETIME"
                            mySetting.DateTimePicker_ImageButton(myImageButton, Form.ID, "txt" & myDT2.Rows(i).Item(2).ToString)
                            Dim myTextBox As TextBox = Page.FindControl("txt" & myDT2.Rows(i).Item(2).ToString)
                            myTextBox.MaxLength = CInt(myDT2.Rows(i).Item(7).ToString)
                            myTextBox = Nothing
                        Case "TIME"
                            mySetting.PopUpTime_ImageButton(myImageButton, Form.ID, "txt" & myDT2.Rows(i).Item(2).ToString)
                            Dim myTextBox As TextBox = Page.FindControl("txt" & myDT2.Rows(i).Item(2).ToString)
                            myTextBox.MaxLength = CInt(myDT2.Rows(i).Item(7).ToString)
                            myTextBox = Nothing
                        Case "DECIMAL", "INTEGER"
                            Dim myTextBox As TextBox = Page.FindControl("txt" & myDT2.Rows(i).Item(2).ToString)
                            myTextBox.MaxLength = CInt(myDT2.Rows(i).Item(7).ToString)
                            myTextBox = Nothing
                        Case Else
                            Dim myTextBox As TextBox = Page.FindControl("txt" & myDT2.Rows(i).Item(2).ToString)
                            myTextBox.MaxLength = CInt(myDT2.Rows(i).Item(7).ToString)
                            If myDT2.Rows(i).Item(11).ToString.Trim = "YES" Then
                                If myDT2.Rows(i).Item(12).ToString = "YES" Then
                                    myTextBox.TextMode = TextBoxMode.Password
                                End If
                                If myDT2.Rows(i).Item(4).ToString = "YES" Then
                                    mySetting.ConvertUppercase(myTextBox)
                                End If
                            End If
                            myTextBox = Nothing
                    End Select
                    myLabel = Nothing
                    myImageKey = Nothing
                    myImageButton = Nothing
                Next
            End If
            myDS = Nothing
            myDT1 = Nothing
            myDT2 = Nothing
        End If

        AutoAdjustPosition("2")

        Dim ddlMonthssql As String, myDS5 As DataSet

        ddlMonthssql = "Create Table #Result([Code] nvarchar(255), [Name] nvarchar(255)); Insert Into #Result Select '','';"
        ddlMonthssql = ddlMonthssql + "Insert Into #Result Select distinct ltrim(rtrim(ocp_id_shift)), ltrim(rtrim(ocp_id_shift)) from employee_attendance_temp order by ltrim(rtrim(ocp_id_shift));"
        ddlMonthssql = ddlMonthssql + " Select * From #Result; Drop Table #Result"
        myDS5 = mySQL.ExecuteSQL(ddlMonthssql)
        If myDS5.Tables.Count > 0 Then
            If myDS5.Tables(0).Columns.Count > 1 Then
                ddlOption_Reason_Type.DataTextField = "Name"
                ddlOption_Reason_Type.DataValueField = "Code"
                ddlOption_Reason_Type.DataSource = myDS5
                ddlOption_Reason_Type.DataBind()
            End If
        End If
        myDS5 = Nothing

        imgBtnPrint.CssClass = buttonPosition1

    End Sub

    Protected Sub imgBtnPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnPrint.Click
        Dim clsRpt As New clsReportServices, myTargetURL As String = ""
        myTargetURL = clsRpt.GetPDFReportURL()
        ssql = "select dbo.fn_UnDisplayDate('" & txtDATEFROM.Text & "','" & Session("Company").ToString & "','" & Session("Module").ToString & "'),dbo.fn_UnDisplayDate('" & txtDATETO.Text & "','" & Session("Company").ToString & "','" & Session("Module").ToString & "'),@@spid"
        myDS = New DataSet()
        myDS = mySQL.ExecuteSQL(ssql)
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                ssql1 = "Exec sp_tms_SelAttendance '" & Session("Company").ToString & "','" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "','','','" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & myDS.Tables(0).Rows(0).Item(1).ToString & "','" & Session("EmpID").ToString & "',100,1"
                myDS1 = New DataSet()
                myDS1 = mySQL.ExecuteSQL(ssql1)
                If myDS1.Tables.Count = 3 Then
                    gv1.DataSource = myDS1.Tables(0)
                    gv1.DataBind()
                    gv2.DataSource = myDS1.Tables(1)
                    gv2.DataBind()
                    If txtDATEFROM.Text = "" Then
                        txtDATEFROM.Text = myDS1.Tables(2).Rows(0).Item(0).ToString

                    End If
                    If txtDATETO.Text = "" Then
                        txtDATETO.Text = myDS1.Tables(2).Rows(0).Item(1).ToString

                    End If
                    'gv3.DataSource = myDS1.Tables(2)
                    'gv3.DataBind()
                End If
            End If
        End If

        ssql = Nothing
        myDS = Nothing
        'myTargetURL = myTargetURL & "&OrganID=JAVA&month=1&year=2008&SPID=50&Type=ADD&paytype=MONTHLYt&userID=hrsa&rdTemplate=pr_GovEPF"
        'Response.Write("<script>window.open('" & myTargetURL & "');</script>")
    End Sub

    Private Sub ShowMessage(ByVal message As String)
        Dim scriptString As String = "<script language=JavaScript>"
        scriptString += "alert('" + message + "');"
        scriptString += "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "ShowMessage", scriptString)
    End Sub

    Sub AutoAdjustPosition(ByVal strSelect As String)

        '//-- Description --------------------------------------------------------------------------//
        'AutoPositioning actually is the enhance version of AutoAdjustPosition,
        'It used to replace previous version of function.

        '//-----------------------------------------------------------------------------------------//

        Try
            'get field position
            Dim code As Integer = 1
            Dim dt As Integer = 3
            Dim posType As Integer = 4
            Dim autonum2 As Integer = 0
            Dim txtnum As Integer = 0
            Dim txtWidth As Integer = 0
            Dim ddlWidth As Integer = 0
            Dim AblWidth As Integer = 0
            Dim EctWidth As Integer = 0

            'get width
            If CInt(Session("GVwidth")) > 680 Then
                AblWidth = (CInt(Session("GVwidth")) - 680)
                EctWidth = AblWidth / 2
                txtWidth = 150 + EctWidth
                ddlWidth = 157 + EctWidth
            Else
                txtWidth = 150
                ddlWidth = 157
                EctWidth = 0
            End If

            'get positioning
            ssql = "exec sp_sa_get_fields_position '" & Form.ID & "'"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            autonum = 0
            If myDS.Tables(0).Rows.Count > 0 Then
                For i = 0 To myDS.Tables(0).Rows.Count - 1
                    Dim myImage As Image = Page.FindControl("imgKey" & Trim(myDS.Tables(0).Rows(i).Item(code)))
                    Dim myLabel As Label = Page.FindControl("lbl" & Trim(myDS.Tables(0).Rows(i).Item(code)))
                    Dim myImageButton As ImageButton = Page.FindControl("imgBtn" & Trim(myDS.Tables(0).Rows(i).Item(code)))
                    Select Case UCase(Trim(myDS.Tables(0).Rows(i).Item(dt).ToString))

                        Case "OPTION"
                            Dim myDropdownlist As DropDownList = Page.FindControl("ddl" & Trim(myDS.Tables(0).Rows(i).Item(code)))
                            Select Case UCase(Trim(myDS.Tables(0).Rows(i).Item(posType).ToString))
                                Case "0"
                                    If myLabel.Visible = True And myDropdownlist.Visible = True Then
                                        autonum = autonum + 1
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myLabel.CssClass = "wordstyle12"
                                        myLabel.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myLabel.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myLabel.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myDropdownlist.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myDropdownlist.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myDropdownlist.Style.Add("position", "absolute")
                                        myDropdownlist.Width = ddlWidth
                                        autonum = autonum + 1
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                    End If
                                Case "1"
                                    If myLabel.Visible = True And myDropdownlist.Visible = True Then
                                        autonum2 = autonum
                                        autonum2 = autonum2 / 4
                                        If autonum2 Mod 2 <> 0 Then
                                            autonum2 = (autonum2 + 3) / 2
                                            autonum = autonum + 5
                                        Else
                                            autonum2 = (autonum2 + 2) / 2
                                            autonum = autonum + 1
                                        End If
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myLabel.CssClass = "wordstyle12"
                                        myLabel.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myLabel.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myLabel.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myDropdownlist.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myDropdownlist.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myDropdownlist.Style.Add("position", "absolute")
                                        myDropdownlist.Width = ddlWidth
                                        autonum = autonum + 1
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                    End If
                                Case "2", "3", "4", "5", "6", "7", "8", "9", "10"
                                    txtnum = CInt(Trim(myDS.Tables(0).Rows(i).Item(posType).ToString))
                                    txtnum = txtnum * 4 'number increase in 4x (per item)
                                    If myLabel.Visible = True And myDropdownlist.Visible = True Then
                                        autonum2 = autonum
                                        autonum2 = autonum2 / 4
                                        If autonum2 Mod 2 <> 0 Then
                                            autonum2 = (autonum2 + 3) / 2
                                            autonum = autonum + 5
                                        Else
                                            autonum2 = (autonum2 + 2) / 2
                                            autonum = autonum + 1
                                        End If
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myLabel.CssClass = "wordstyle12"
                                        myLabel.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myLabel.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myLabel.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myDropdownlist.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myDropdownlist.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myDropdownlist.Style.Add("position", "absolute")
                                        myDropdownlist.Width = ddlWidth
                                        autonum = autonum + 1
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                        autonum = autonum + txtnum
                                    End If
                                Case "MIXED"
                                    If myLabel.Visible = True And myDropdownlist.Visible = True Then
                                        autonum = autonum - 3
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 3
                                        myLabel.Visible = False
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                        myDropdownlist.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myDropdownlist.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myDropdownlist.Style.Add("position", "absolute")
                                        myDropdownlist.Width = ddlWidth
                                        autonum = autonum + 4
                                    End If
                                Case Else
                                    If myLabel.Visible = True And myDropdownlist.Visible = True Then
                                        autonum = autonum + 1
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myLabel.CssClass = "wordstyle12"
                                        myLabel.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myLabel.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myLabel.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myDropdownlist.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myDropdownlist.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myDropdownlist.Style.Add("position", "absolute")
                                        myDropdownlist.Width = ddlWidth
                                        autonum = autonum + 1
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                    End If
                            End Select

                        Case Else
                            Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(0).Rows(i).Item(code)))
                            Select Case UCase(Trim(myDS.Tables(0).Rows(i).Item(posType).ToString))
                                Case "0"
                                    If myLabel.Visible = True And myTextBox.Visible = True Then
                                        autonum = autonum + 1
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myLabel.CssClass = "wordstyle12"
                                        myLabel.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myLabel.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myLabel.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myTextBox.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myTextBox.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myTextBox.Style.Add("position", "absolute")
                                        myTextBox.Width = txtWidth
                                        autonum = autonum + 1
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                    End If
                                Case "1"
                                    If myLabel.Visible = True And myTextBox.Visible = True Then
                                        autonum2 = autonum
                                        autonum2 = autonum2 / 4
                                        If autonum2 Mod 2 <> 0 Then
                                            autonum2 = (autonum2 + 3) / 2
                                            autonum = autonum + 5
                                        Else
                                            autonum2 = (autonum2 + 2) / 2
                                            autonum = autonum + 1
                                        End If
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myLabel.CssClass = "wordstyle12"
                                        myLabel.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myLabel.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myLabel.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myTextBox.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myTextBox.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myTextBox.Style.Add("position", "absolute")
                                        myTextBox.Width = txtWidth
                                        autonum = autonum + 1
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                    End If
                                Case "2", "3", "4", "5", "6", "7", "8", "9", "10"
                                    txtnum = CInt(Trim(myDS.Tables(0).Rows(i).Item(posType).ToString))
                                    txtnum = txtnum * 4 'number increase in 4x (per item)
                                    If myLabel.Visible = True And myTextBox.Visible = True Then
                                        autonum2 = autonum
                                        autonum2 = autonum2 / 4
                                        If autonum2 Mod 2 <> 0 Then
                                            autonum2 = (autonum2 + 3) / 2
                                            autonum = autonum + 5
                                        Else
                                            autonum2 = (autonum2 + 2) / 2
                                            autonum = autonum + 1
                                        End If
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myLabel.CssClass = "wordstyle12"
                                        myLabel.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myLabel.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myLabel.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myTextBox.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myTextBox.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myTextBox.Style.Add("position", "absolute")
                                        myTextBox.Width = txtWidth
                                        autonum = autonum + 1
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                        autonum = autonum + txtnum
                                    End If
                                Case "MIXED"
                                    If myLabel.Visible = True And myTextBox.Visible = True Then
                                        autonum = autonum - 3
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 3
                                        myLabel.Visible = False
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                        myTextBox.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myTextBox.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myTextBox.Style.Add("position", "absolute")
                                        myTextBox.Width = txtWidth
                                        autonum = autonum + 4
                                    End If
                                Case Else
                                    If myLabel.Visible = True And myTextBox.Visible = True Then
                                        autonum = autonum + 1
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myLabel.CssClass = "wordstyle12"
                                        myLabel.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myLabel.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myLabel.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myTextBox.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myTextBox.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myTextBox.Style.Add("position", "absolute")
                                        myTextBox.Width = txtWidth
                                        autonum = autonum + 1
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                    End If
                            End Select
                    End Select
                Next
            End If

            autonum = autonum / 4
            If autonum Mod 2 <> 0 Then
                autonum = autonum + 1
            End If
            autonum = autonum / 2

            buttonPosition1 = buttonPosition1 & autonum - 1
            buttonPosition2 = buttonPosition2 & autonum - 1
            buttonPosition3 = buttonPosition3 & autonum - 1

            labelPosition1 = labelPosition1 & autonum - 1
            lblresult2.CssClass = labelPosition1
            imgTop.CssClass = "Display_0"
            imgBottom.CssClass = panelPosition & autonum
        Catch ex As Exception
            lblresult2.Text = "[AutoAdjustPosition]Error: " & ex.Message
            'SetFieldToFalse()
        End Try

    End Sub

    Private Function ValidatePrint(ByVal myds As DataSet) As Boolean
        If myds.Tables(0).Rows(0).Item(0).ToString = "" Then
            lblresult2.Text = "[" & lblEMPLOYEE_PROFILE_ID.Text & "] Is A Required Field!"
            txtEMPLOYEE_PROFILE_ID.Focus()
            Return False
        ElseIf IsDate(mySetting.ConvertDateToDecimal(txtDATEFROM.Text, Session("Company").ToString, Session("Module").ToString)) = False Then
            lblresult2.Text = "Invalid Input Date Format For [" & lblDATEFROM.Text & "] !"
            txtDATEFROM.Focus()
            Return False
        ElseIf IsDate(mySetting.ConvertDateToDecimal(txtDATETO.Text, Session("Company").ToString, Session("Module").ToString)) = False Then
            lblresult2.Text = "Invalid Input Date Format For [" & lblDATETO.Text & "] !"
            txtDATETO.Focus()
            Return False
        Else
            Return True
        End If
    End Function

    Protected Sub imgBtnSubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSubmit.Click
        Dim CurrentDateTime As String, PayCycle As String
        CurrentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss")
        If txtEmployee.Text = "" Then
            lblresult2.Text = "Cannot be Blank [" & lblEmployee.Text & "] !"
            MsgBox(lblresult2.Text)
            txtEmployee.Focus()
        ElseIf DateTime.TryParseExact(mySetting.UnDisplayDateTime(txtDateIn.Text, Session("Company").ToString, Session("Module").ToString), "yyyyMMddHHmmss", Nothing, DateTimeStyles.None, Nothing) = False And txtDateIn.Text <> "" Then
            lblresult2.Text = "Invalid Input Date Format For [" & lblDateIn.Text & "] !"
            MsgBox(lblresult2.Text)
            txtDateIn.Focus()
        ElseIf txtDateIn.Text = "" Then
            lblresult2.Text = "Cannot Blank On [" & lblDateIn.Text & "] !"
            MsgBox(lblresult2.Text)
            txtDateIn.Focus()
        ElseIf DateTime.TryParseExact(mySetting.UnDisplayDateTime(txtDateOut.Text, Session("Company").ToString, Session("Module").ToString), "yyyyMMddHHmmss", Nothing, DateTimeStyles.None, Nothing) = False And txtDateOut.Text <> "" Then
            lblresult2.Text = "Invalid Input Date Format For [" & lblDateOut.Text & "] !"
            MsgBox(lblresult2.Text)
            txtDateOut.Focus()
        ElseIf txtDateOut.Text = "" Then
            lblresult2.Text = "Cannot Blank On [" & lblDateOut.Text & "] !"
            MsgBox(lblresult2.Text)
            txtDateOut.Focus()
        ElseIf txtReason.Text = "" Then
            lblresult2.Text = "Cannot Blank On [" & lblReason.Text & "] !"
            MsgBox(lblresult2.Text)
            txtReason.Focus()
            'ElseIf ddlOption_Reason_Type.SelectedValue = "" Then
            '    lblresult2.Text = "Cannot Blank On [" & lblOption_Reason_Type.Text & "] !"
            '    MsgBox(lblresult2.Text)
            '    ddlOption_Reason_Type.Focus()
        Else
            ssql = "select dbo.fn_ReturnEmpIDByCodeName('" & Session("Company").ToString & "','" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "'),dbo.fn_UnDisplayDate('" & txtDATEFROM.Text & "','" & Session("Company").ToString & "','" & Session("Module").ToString & "'),dbo.fn_UnDisplayDate('" & txtDATETO.Text & "','" & Session("Company").ToString & "','" & Session("Module").ToString & "'),isnull(dbo.fn_UnDisplayDatetime('" & txtDateIn.Text & "','" & Session("Company").ToString & "','" & Session("Module").ToString & "'),''),isnull(dbo.fn_UnDisplayDateTime('" & txtDateOut.Text & "','" & Session("Company").ToString & "','" & Session("Module").ToString & "'),'')" & _
            ",isnull(dbo.fn_UnDisplayDatetime('" & HbDateIn.Value & "','" & Session("Company").ToString & "','" & Session("Module").ToString & "'),''),isnull(dbo.fn_UnDisplayDateTime('" & HbDateOut.Value & "','" & Session("Company").ToString & "','" & Session("Module").ToString & "'),''),@@spid"
            myDS = New DataSet()
            myDS = mySQL.ExecuteSQL(ssql)
            If myDS.Tables.Count > 0 Then
                If myDS.Tables(0).Rows.Count > 0 Then
                    ssql2 = "Select Option_Pay_Cycle from employee_salary where Employee_Profile_id = '" & myDS.Tables(0).Rows(0).Item(0).ToString & "'"
                    myDS2 = mySQL.ExecuteSQL(ssql2, Session("Company").ToString, Session("EmpID").ToString)

                    PayCycle = "MONTHLY"

                    If myDS2.Tables.Count > 0 Then
                        If myDS2.Tables(0).Rows.Count > 0 Then
                            PayCycle = myDS2.Tables(0).Rows(0).Item(0).ToString
                        End If
                    Else
                        PayCycle = "MONTHLY"
                    End If
                    ssql2 = Nothing
                    myDS2 = Nothing
                    Dim Date3 As String

                    If txtDateIn.Text = "" Then
                        Date3 = myDS.Tables(0).Rows(0).Item(4).ToString
                    Else
                        Date3 = myDS.Tables(0).Rows(0).Item(3).ToString
                    End If

                    ssql2 = "Exec sp_sa_chkTransactionLockDate '" & Session("Company") & "','" _
                           & Session("EmpID").ToString & "','" & Date3 & "'," & 1 & "," & 0 & ",'" & PayCycle & "'"
                    myDS2 = mySQL.ExecuteSQL(ssql2, Session("Company").ToString, Session("EmpID").ToString)

                    If Trim(myDS2.Tables(0).Rows(0).Item(0).ToString) = "" Then
                        ssql1 = "exec sp_tms_delattendance '" & Session("Company").ToString & "','" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & myDS.Tables(0).Rows(0).Item(5).ToString & "','" & Right(myDS.Tables(0).Rows(0).Item(5).ToString, 6) & "','" & myDS.Tables(0).Rows(0).Item(6).ToString & "','" & Right(myDS.Tables(0).Rows(0).Item(6).ToString, 6) & "','DEL','" & Session("EmpID").ToString & "'"
                        mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)

                        ssql1 = "Exec sp_tms_calEmpOTLate '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','','" & myDS.Tables(0).Rows(0).Item(3).ToString & "','" & myDS.Tables(0).Rows(0).Item(3).ToString & "','" & myDS.Tables(0).Rows(0).Item(4).ToString & "','" & myDS.Tables(0).Rows(0).Item(4).ToString & "','','1','" & txtReason.Text & "',0"
                        myDS1 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)

                        If myDS1.Tables.Count > 0 Then
                            If myDS.Tables(0).Rows.Count > 0 Then
                                If Not HbDateIn.Value = "" And HbDateOut.Value = "" Then
                                    ssql1 = "Exec sp_tms_calEmpOTLate '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','','" & myDS.Tables(0).Rows(0).Item(5).ToString & "','" & myDS.Tables(0).Rows(0).Item(5).ToString & "','" & myDS.Tables(0).Rows(0).Item(6).ToString & "','" & myDS.Tables(0).Rows(0).Item(6).ToString & "','','1','" & txtReason.Text & "',0"
                                    myDS1 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
                                End If

                                If myDS1.Tables(0).Rows(0).Item(0).ToString = "1" Then
                                    MsgBox("Inter Department Transfer Record Not Found !")
                                End If
                                If myDS1.Tables(0).Rows(0).Item(0).ToString = "2" Then
                                    MsgBox("Shift not setup correctly!")
                                End If
                                If myDS1.Tables(0).Rows(0).Item(0).ToString = "3" Then
                                    MsgBox("Calendar not setup correctly!")
                                End If
                                If myDS1.Tables(0).Rows(0).Item(0).ToString = "4" Then
                                    MsgBox("Duplicate Attendance Found!")
                                End If
                            End If
                        End If
                        txtEmployee.Text = ""
                        HbEmployee.Value = ""
                        txtDateIn.Text = ""
                        HbDateIn.Value = ""
                        txtDateOut.Text = ""
                        HbDateOut.Value = ""
                        'ddlOption_Reason_Type.SelectedIndex = -1
                        txtReason.Text = ""
                        If txtDATEFROM.Text <> "" And txtDATETO.Text <> "" Then
                            imgBtnPrint_Click(Nothing, Nothing)
                        End If
                    Else
                        MsgBox("The following date already locked! (" & Date3 & ") ")
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub MsgBox(ByVal msg As String)
        Dim strScript As String = "<script language=" & """" & "javascript" & """" & ">"
        strScript &= "alert('" & msg & "');"
        strScript &= "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "Error!", strScript)
    End Sub

    Protected Sub ImgBtnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnDelete.Click
        Dim PayCycle As String
        If ActionValidateDelete() Then
            For i = 0 To gv1.Rows.Count - 1
                Dim chkDelete As CheckBox = gv1.Rows(i).Cells(0).Controls(1)
                If chkDelete.Checked Then 'And gv1.Rows(i).Cells(4).Text.Replace("&nbsp;", "&").Replace("&amp;", "&").ToUpper = "BY HR" Then
                    ssql = "select dbo.fn_ReturnEmpIDByCodeName('" & Session("Company").ToString & "','" & gv2.Rows(i).Cells(1).Text.Replace("'", "''") & "'),dbo.fn_UnDisplayDateTime('" & gv2.Rows(i).Cells(3).Text & "','" & Session("Company").ToString & "','" & Session("Module").ToString & "'),dbo.fn_UnDisplayDateTime('" & gv2.Rows(i).Cells(4).Text & "','" & Session("Company").ToString & "','" & Session("Module").ToString & "'),@@spid"
                    myDS = New DataSet()
                    myDS = mySQL.ExecuteSQL(ssql)
                    If myDS.Tables.Count > 0 Then
                        If myDS.Tables(0).Rows.Count > 0 Then
                            ssql2 = "Select Option_Pay_Cycle from employee_salary where Employee_Profile_id = '" & myDS.Tables(0).Rows(0).Item(0).ToString & "'"
                            myDS2 = mySQL.ExecuteSQL(ssql2, Session("Company").ToString, Session("EmpID").ToString)

                            PayCycle = "MONTHLY"

                            If myDS2.Tables.Count > 0 Then
                                If myDS2.Tables(0).Rows.Count > 0 Then
                                    PayCycle = myDS2.Tables(0).Rows(0).Item(0).ToString
                                End If
                            Else
                                PayCycle = "MONTHLY"
                            End If
                            ssql2 = Nothing
                            myDS2 = Nothing
                            Dim Date3 As String

                            If LTrim(Server.HtmlDecode(gv2.Rows(i).Cells(3).Text)) = "" Then
                                Date3 = myDS.Tables(0).Rows(0).Item(4).ToString
                            Else
                                Date3 = myDS.Tables(0).Rows(0).Item(3).ToString
                            End If

                            ssql2 = "Exec sp_sa_chkTransactionLockDate '" & Session("Company") & "','" _
                                   & Session("EmpID").ToString & "','" & Date3 & "'," & 1 & "," & 0 & ",'" & PayCycle & "'"
                            myDS2 = mySQL.ExecuteSQL(ssql2, Session("Company").ToString, Session("EmpID").ToString)

                            If Trim(myDS2.Tables(0).Rows(0).Item(0).ToString) = "" Then
                                ssql1 = "exec sp_tms_delattendance '" & Session("Company").ToString & "','" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & myDS.Tables(0).Rows(0).Item(1).ToString & "','" & Right(myDS.Tables(0).Rows(0).Item(1).ToString, 6) & "','" & myDS.Tables(0).Rows(0).Item(2).ToString & "','" & Right(myDS.Tables(0).Rows(0).Item(2).ToString, 6) & "','DEL','" & Session("EmpID").ToString & "'"
                                mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
                                txtDateIn.Text = ""
                                txtDateOut.Text = ""
                                'ddlOption_Reason_Type.SelectedIndex = -1
                                txtReason.Text = ""
                            Else
                                MsgBox("The following date already locked! (" & Date3 & ") ")
                            End If
                        End If
                    End If
                End If
            Next
            imgBtnPrint_Click(Nothing, Nothing)
        End If
    End Sub
    Private Function ActionValidateDelete() As Boolean
        RecFound = False
        For i = 0 To gv1.Rows.Count - 1
            Dim chkDelete As CheckBox = gv1.Rows(i).Cells(0).Controls(1)
            If chkDelete.Checked Then
                RecFound = True
                Exit For
            End If
            chkDelete = Nothing
        Next
        If RecFound = True Then
            Return True
        Else
            ShowMessage("No row selected for deleting. Please select at least one row!")
            Return False
        End If
    End Function

    Protected Sub imgbtnReProcess_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnReProcess.Click
        ssql = "exec [sp_tms_insUpdRawData] "
        mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

        Dim strScript As String = "<script language=" & """" & "javascript" & """" & ">"
        strScript &= "alert('Process Completed!');"
        strScript &= "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "Process Completed!", strScript)
    End Sub

    Protected Sub ddlOption_Reason_Type_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOption_Reason_Type.SelectedIndexChanged
        If ddlOption_Reason_Type.SelectedValue = "OTHERS" Then
            lblReason.Visible = True
            txtReason.Visible = True
            imgbtnReason.Visible = True
        Else
            lblReason.Visible = False
            txtReason.Visible = False
            imgbtnReason.Visible = False
            txtReason.Text = ""
        End If
    End Sub

    Protected Sub imgBtnSelect_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSelect.Click
        If ActionValidateEdit() Then
            For i = 0 To gv1.Rows.Count - 1
                Dim chkEdit As CheckBox = gv1.Rows(i).Cells(0).Controls(1)
                If chkEdit.Checked Then
                    chkEdit.Checked = False
                    txtEmployee.Text = Server.HtmlDecode(gv1.Rows(i).Cells(1).Text)
                    HbEmployee.Value = Server.HtmlDecode(gv1.Rows(i).Cells(1).Text)
                    txtDateIn.Text = Server.HtmlDecode(gv1.Rows(i).Cells(3).Text)
                    HbDateIn.Value = Server.HtmlDecode(gv1.Rows(i).Cells(3).Text)
                    txtDateOut.Text = Server.HtmlDecode(gv1.Rows(i).Cells(4).Text)
                    HbDateOut.Value = Server.HtmlDecode(gv1.Rows(i).Cells(4).Text)
                    txtReason.Text = Server.HtmlDecode(gv1.Rows(i).Cells(13).Text)
                End If
            Next
        End If
    End Sub

    Private Function ActionValidateEdit() As Boolean
        RecFound = False
        CountRecord = 0
        For i = 0 To gv1.Rows.Count - 1
            Dim chkEdit As CheckBox = gv1.Rows(i).Cells(0).Controls(1)
            If chkEdit.Checked Then
                RecFound = True
                CountRecord = CountRecord + 1
            End If
            chkEdit = Nothing
        Next
        If RecFound = True Then
            If CountRecord > 1 Then
                ShowMessage("You select " & CStr(CountRecord) & " rows for editing, which is invalid. Please select only 1 row!")
                Return False
            Else
                Return True
            End If
        Else
            ShowMessage("No row selected for editing. Please select at least one row!")
            Return False
        End If
    End Function
End Class
