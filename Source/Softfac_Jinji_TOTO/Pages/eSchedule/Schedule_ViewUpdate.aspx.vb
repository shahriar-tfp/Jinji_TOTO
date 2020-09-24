Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.DateTime
Imports System.Globalization

Partial Class PAGES_ESCHEDULE_SCHEDULE_VIEWUPDATE
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
    Dim empcode As String
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
        Session("Module") = "ESCHEDULE"
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")
        txtDateApproved.Enabled = False
        ddlApprovedBy.Enabled = False
        txtRemarks.Enabled = False
        imgBtnSubmit.Visible = False
        imgBtnUpdate.Visible = False
        imgBtnCancel.visible = False
        disabled()

        mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourDef, "btnSearch.png")
        mySetting.GetBtnImgUrl(imgBtnAdd, Session("Company").ToString, btnColourDef, "btnAdd.png")
        mySetting.GetBtnImgUrl(imgBtnEdit, Session("Company").ToString, btnColourDef, "btnEdit.png")
        mySetting.GetBtnImgUrl(imgBtnSubmit, Session("Company").ToString, btnColourDef, "btnSubmit.png")
        mySetting.GetBtnImgUrl(imgBtnUpdate, Session("Company").ToString, btnColourDef, "btnUpdate.png")
        mySetting.GetBtnImgUrl(ImgBtnDelete, Session("Company").ToString, btnColourDef, "btnDelete.png")
        mySetting.GetBtnImgUrl(imgBtnCancel, Session("Company").ToString, btnColourDef, "btnCancel.png")

        mySetting.GetImgUrl(imgKeyDATEFROM, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyDATETO, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyEMPLOYEE_PROFILE_ID, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnDATEFROM, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnDATETO, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnEMPLOYEE_PROFILE_ID, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgBtnDate, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnDateApproved, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
        mySetting.GetBtnImgUrl(imgbtnReProcess, Session("Company").ToString, btnColourDef, "btnProcess.png")
        mySetting.PopUpCalendar_ImageButton(imgBtnDate, Form.ID, "txtDate")
        mySetting.PopUpCalendar_ImageButton(imgbtnDateApproved, Form.ID, "txtDateApproved")

        mySetting.GetImgTypeUrl(imgTop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Filter.png")
        mySetting.GetImgTypeUrl(imgBottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")


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
                Else
                    AllowInsert = False
                    Session("Insert") = "FALSE"
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
                Else
                    AllowDelete = False
                    Session("Delete") = "FALSE"
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

        ddlMonthssql = "Create Table #Result([No] int,[Code] nvarchar(255), [Name] nvarchar(255)); Insert Into #Result Select 0,'','';"
        ddlMonthssql = ddlMonthssql + "Insert Into #Result Select distinct seq,ltrim(rtrim(leaveid)), ltrim(rtrim(LeaveDesc)) from sa_leavetype order by seq;"
        ddlMonthssql = ddlMonthssql + " Select * From #Result; Drop Table #Result"
        myDS5 = mySQL.ExecuteSQL(ddlMonthssql)
        If myDS5.Tables.Count > 0 Then
            If myDS5.Tables(0).Columns.Count > 1 Then
                ddlLeaveType.DataTextField = "Name"
                ddlLeaveType.DataValueField = "Code"
                ddlLeaveType.DataSource = myDS5
                ddlLeaveType.DataBind()
            End If
        End If
        myDS5 = Nothing
        ddlMonthssql = Nothing


        ddlMonthssql = "Create Table #Result([No] int,[Code] nvarchar(255), [Name] nvarchar(255)); Insert Into #Result Select 0,'','';"
        ddlMonthssql = ddlMonthssql + "Insert Into #Result Select distinct seq,ltrim(rtrim(ID)), ltrim(rtrim(IDDesc)) from sa_reference where type = 'leavePeriod' order by seq;"
        ddlMonthssql = ddlMonthssql + " Select * From #Result; Drop Table #Result"
        myDS5 = mySQL.ExecuteSQL(ddlMonthssql)
        If myDS5.Tables.Count > 0 Then
            If myDS5.Tables(0).Columns.Count > 1 Then
                ddlPeriod.DataTextField = "Name"
                ddlPeriod.DataValueField = "Code"
                ddlPeriod.DataSource = myDS5
                ddlPeriod.DataBind()
            End If
        End If
        myDS5 = Nothing
        ddlMonthssql = Nothing

        ddlMonthssql = "Create Table #Result([No] int,[Code] nvarchar(255), [Name] nvarchar(255)); Insert Into #Result Select 0,'','';"
        ddlMonthssql = ddlMonthssql + "Insert Into #Result Select distinct seq,ltrim(rtrim(ID)), ltrim(rtrim(IDDesc)) from sa_reference where type = 'LeaveStatus' order by seq;"
        ddlMonthssql = ddlMonthssql + " Select * From #Result; Drop Table #Result"
        myDS5 = mySQL.ExecuteSQL(ddlMonthssql)
        If myDS5.Tables.Count > 0 Then
            If myDS5.Tables(0).Columns.Count > 1 Then
                ddlStatus.DataTextField = "Name"
                ddlStatus.DataValueField = "Code"
                ddlStatus.DataSource = myDS5
                ddlStatus.DataBind()
            End If
        End If
        myDS5 = Nothing
        ddlMonthssql = Nothing

        imgBtnPrint.CssClass = buttonPosition1

    End Sub

    Protected Sub imgBtnPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnPrint.Click
        Dim clsRpt As New clsReportServices, myTargetURL As String = ""
        myTargetURL = clsRpt.GetPDFReportURL()
        ssql = "select dbo.fn_ReturnEmpIDByCodeName('" & Session("Company").ToString & "','" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "'),dbo.fn_UnDisplayDate('" & txtDATEFROM.Text & "','" & Session("Company").ToString & "','" & Session("Module").ToString & "'),dbo.fn_UnDisplayDate('" & txtDATETO.Text & "','" & Session("Company").ToString & "','" & Session("Module").ToString & "'),@@spid"
        myDS = New DataSet()
        myDS = mySQL.ExecuteSQL(ssql)
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                ssql1 = "exec sp_ls_sel_LeaveApplication '" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "','" & txtDATEFROM.Text & "','" & _
                          txtDATETO.Text & "','CODENAME'"
                myDS1 = New DataSet()
                myDS1 = mySQL.ExecuteSQL(ssql1)
                If myDS1.Tables.Count = 1 Then
                    gv1.DataSource = myDS1.Tables(0)
                    gv1.DataBind()
                    gv2.DataSource = myDS1.Tables(0)
                    gv2.DataBind()
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

        empcode = mySetting.GetEmployeeCodeByCodeName_ReturnString(Session("Company").ToString, txtEMPLOYEE_PROFILE_ID.Text)
        ssql = "select dbo.fn_ReturnEmpIDByCodeName('" & Session("Company").ToString & "','" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "'),dbo.fn_UnDisplayDate('" & txtDATEFROM.Text & "','" & Session("Company").ToString & "','" & Session("Module").ToString & "'),dbo.fn_UnDisplayDate('" & txtDATETO.Text & "','" & Session("Company").ToString & "','" & Session("Module").ToString & "'),@@spid"
        myDS = New DataSet()
        myDS = mySQL.ExecuteSQL(ssql)

        If ValidateSubmit(myDS) Then
            Dim strDateApproved, strDestination As String

            If txtDate.Text = "" Then
                strDateApproved = "01/01/1900 00:00:00"
            Else
                strDateApproved = txtDate.Text
            End If

            If Trim(txtDestination.Text) = "" Then
                strDestination = "---"
            Else
                strDestination = Trim(txtDestination.Text)
            End If

            ssql1 = "exec sp_ls_InsDel_LeaveApplication '" & empcode & "','" & "A" & "','" & _
                           txtDate.Text & "','" & _
                            Trim(ddlLeaveType.SelectedValue) & "','" & _
                            Trim(ddlPeriod.SelectedValue) & "','" & _
                            Trim(txtReason.Text) & "','" & _
                            strDestination & "','" & _
                            Session("EmpID").ToString & "','" & Now & "','" & _
                            Trim(ddlStatus.SelectedValue) & "','" & _
                            Trim(ddlApprovedBy.SelectedValue) & "','" & _
                            strDateApproved & "','" & _
                            Trim(txtRemarks.Text) & "','" & _
                            "ADD" & "'"
            mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
            Dim strScript As String = "<script language=" & """" & "javascript" & """" & ">"
            strScript &= "alert('Submit Completed!');"
            strScript &= "<" & "/script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "Process Completed!", strScript)
            imgBtnPrint_Click(Nothing, Nothing)
            iniatial()
        End If
        
    End Sub

    Private Function ValidateSubmit(ByVal mycustomData As DataSet) As Boolean
        ValidateSubmit = False

        If mycustomData.Tables(0).Rows(0).Item(0).ToString = "" Then
            lblresult2.Text = "Cannot be Blank [" & lblEMPLOYEE_PROFILE_ID.Text & "] !"
            MsgBox(lblresult2.Text)
            txtEMPLOYEE_PROFILE_ID.Focus()
            Exit Function
        End If

        If ddlPeriod.SelectedValue = "" Then
            lblresult2.Text = "Cannot be Blank [" & lblPeriod.Text & "] !"
            MsgBox(lblresult2.Text)
            ddlPeriod.Focus()
            Exit Function
        End If

        If ddlLeaveType.SelectedValue = "" Then
            lblresult2.Text = "Cannot be Blank [" & lblLeaveType.Text & "] !"
            MsgBox(lblresult2.Text)
            ddlLeaveType.Focus()
            Exit Function
        End If

        If txtDate.Text = "" Then
            lblresult2.Text = "Cannot be Blank [" & lblDate.Text & "] !"
            MsgBox(lblresult2.Text)
            txtDate.Focus()
            Exit Function

        End If
        If DateTime.TryParseExact(mySetting.UnDisplayDateTime(txtDate.Text, Session("Company").ToString, Session("Module").ToString), "yyyyMMddHHmmss", Nothing, DateTimeStyles.None, Nothing) = False And txtDate.Text <> "" Then
            lblresult2.Text = "Invalid Input Date Format For [" & lblDate.Text & "] !"
            MsgBox(lblresult2.Text)
            txtDate.Focus()
            Exit Function
        End If

        If Trim(ddlLeaveType.SelectedValue) = "BT" And txtDestination.Text = "" Then
            lblresult2.Text = "Cannot be Blank [" & lblDestination.Text & "] !"
            MsgBox(lblresult2.Text)
            txtDestination.Focus()
            Exit Function
        End If

        If txtReason.Text = "" Then
            lblresult2.Text = "Cannot be Blank [" & lblReason.Text & "] !"
            MsgBox(lblresult2.Text)
            txtReason.Focus()
            Exit Function
        End If

        If ddlStatus.SelectedValue = "" Then
            lblresult2.Text = "Cannot be Blank [" & lblStatus.Text & "] !"
            MsgBox(lblresult2.Text)
            ddlStatus.Focus()
            Exit Function
        End If

        If Trim(ddlStatus.SelectedValue) <> "P" Then
            If ddlApprovedBy.SelectedValue = "" Then
                lblresult2.Text = "Cannot be Blank [" & lblApprovedBy.Text & "] !"
                MsgBox(lblresult2.Text)
                ddlApprovedBy.Focus()
                Exit Function
            End If

            If txtDateApproved.Text = "" Then
                lblresult2.Text = "Cannot be Blank [" & lblDateApproved.Text & "] !"
                MsgBox(lblresult2.Text)
                txtDateApproved.Focus()
                Exit Function

            End If
            If DateTime.TryParseExact(mySetting.UnDisplayDateTime(txtDateApproved.Text, Session("Company").ToString, Session("Module").ToString), "yyyyMMddHHmmss", Nothing, DateTimeStyles.None, Nothing) = False And txtDateApproved.Text <> "" Then
                lblresult2.Text = "Invalid Input Date Format For [" & lblDateApproved.Text & "] !"
                MsgBox(lblresult2.Text)
                txtDateApproved.Focus()
                Exit Function
            End If

            If Trim(ddlStatus.SelectedValue) = "R" Then
                If txtRemarks.Text = "" Then
                    lblresult2.Text = "Cannot be Blank [" & lblRemarks.Text & "] !"
                    MsgBox(lblresult2.Text)
                    txtRemarks.Focus()
                    Exit Function
                End If
            End If
        End If

        empcode = mySetting.GetEmployeeCodeByCodeName_ReturnString(Session("Company").ToString, txtEMPLOYEE_PROFILE_ID.Text)
        ssql4 = "exec sp_ls_Chk_LeaveApplication '" & empcode & "','" & "A" & "','" & _
                          txtDate.Text & "','" & Trim(ddlLeaveType.SelectedValue) & "','" & _
                          Trim(ddlPeriod.SelectedValue) & "'"

        myDS2 = mySQL.ExecuteSQL(ssql4, Session("Company").ToString, Session("EmpID").ToString)

        If myDS2.Tables.Count > 0 Then
            If myDS2.Tables(0).Rows.Count > 0 Then
                lblresult2.Text = "Duplicate record found!!"
                MsgBox(lblresult2.Text)
                Exit Function
            End If
        End If
        ValidateSubmit = True

    End Function

    Private Function ValidateUpdate(ByVal mycustomData As DataSet) As Boolean
        ValidateUpdate = False

        If mycustomData.Tables(0).Rows(0).Item(0).ToString = "" Then
            lblresult2.Text = "Cannot be Blank [" & lblEMPLOYEE_PROFILE_ID.Text & "] !"
            MsgBox(lblresult2.Text)
            txtEMPLOYEE_PROFILE_ID.Focus()
            Exit Function
        End If

        If ddlPeriod.SelectedValue = "" Then
            lblresult2.Text = "Cannot be Blank [" & lblPeriod.Text & "] !"
            MsgBox(lblresult2.Text)
            ddlPeriod.Focus()
            Exit Function
        End If

        If ddlLeaveType.SelectedValue = "" Then
            lblresult2.Text = "Cannot be Blank [" & lblLeaveType.Text & "] !"
            MsgBox(lblresult2.Text)
            ddlLeaveType.Focus()
            Exit Function
        End If

        If txtDate.Text = "" Then
            lblresult2.Text = "Cannot be Blank [" & lblDate.Text & "] !"
            MsgBox(lblresult2.Text)
            txtDate.Focus()
            Exit Function

        End If
        If DateTime.TryParseExact(mySetting.UnDisplayDateTime(txtDate.Text, Session("Company").ToString, Session("Module").ToString), "yyyyMMddHHmmss", Nothing, DateTimeStyles.None, Nothing) = False And txtDate.Text <> "" Then
            lblresult2.Text = "Invalid Input Date Format For [" & lblDate.Text & "] !"
            MsgBox(lblresult2.Text)
            txtDate.Focus()
            Exit Function
        End If

        If Trim(ddlLeaveType.SelectedValue) = "BT" And txtDestination.Text = "" Then
            lblresult2.Text = "Cannot be Blank [" & lblDestination.Text & "] !"
            MsgBox(lblresult2.Text)
            txtDestination.Focus()
            Exit Function
        End If

        If txtReason.Text = "" Then
            lblresult2.Text = "Cannot be Blank [" & lblReason.Text & "] !"
            MsgBox(lblresult2.Text)
            txtReason.Focus()
            Exit Function
        End If

        If ddlStatus.SelectedValue = "" Then
            lblresult2.Text = "Cannot be Blank [" & lblStatus.Text & "] !"
            MsgBox(lblresult2.Text)
            ddlStatus.Focus()
            Exit Function
        End If

        If Trim(ddlStatus.SelectedValue) <> "P" Then
            If ddlApprovedBy.SelectedValue = "" Then
                lblresult2.Text = "Cannot be Blank [" & lblApprovedBy.Text & "] !"
                MsgBox(lblresult2.Text)
                ddlApprovedBy.Focus()
                Exit Function
            End If

            If txtDateApproved.Text = "" Then
                lblresult2.Text = "Cannot be Blank [" & lblDateApproved.Text & "] !"
                MsgBox(lblresult2.Text)
                txtDateApproved.Focus()
                Exit Function

            End If
            If DateTime.TryParseExact(mySetting.UnDisplayDateTime(txtDateApproved.Text, Session("Company").ToString, Session("Module").ToString), "yyyyMMddHHmmss", Nothing, DateTimeStyles.None, Nothing) = False And txtDateApproved.Text <> "" Then
                lblresult2.Text = "Invalid Input Date Format For [" & lblDateApproved.Text & "] !"
                MsgBox(lblresult2.Text)
                txtDateApproved.Focus()
                Exit Function
            End If

            If Trim(ddlStatus.SelectedValue) = "R" Then
                If txtRemarks.Text = "" Then
                    lblresult2.Text = "Cannot be Blank [" & lblRemarks.Text & "] !"
                    MsgBox(lblresult2.Text)
                    txtRemarks.Focus()
                    Exit Function
                End If
            End If
        End If

        'empcode = mySetting.GetEmployeeCodeByCodeName_ReturnString(Session("Company").ToString, txtEMPLOYEE_PROFILE_ID.Text)
        'ssql4 = "exec sp_ls_Chk_LeaveApplication '" & empcode & "','" & "A" & "','" & _
        '                  txtDate.Text & "','" & Trim(ddlLeaveType.SelectedValue) & "','" & _
        '                  Trim(ddlPeriod.SelectedValue) & "'"

        'myDS2 = mySQL.ExecuteSQL(ssql4, Session("Company").ToString, Session("EmpID").ToString)

        'If myDS2.Tables.Count > 0 Then
        '    If myDS2.Tables(0).Rows.Count > 0 Then
        '        lblresult2.Text = "Duplicate record found!!"
        '        MsgBox(lblresult2.Text)
        '        Exit Function
        '    End If
        'End If
        ValidateUpdate = True

    End Function
    Private Sub MsgBox(ByVal msg As String)
        Dim strScript As String = "<script language=" & """" & "javascript" & """" & ">"
        strScript &= "alert('" & msg & "');"
        strScript &= "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "Error!", strScript)
    End Sub

    Protected Sub ImgBtnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnDelete.Click
        Dim strEmpID As String
        Dim strPeriod As String
        Dim strDate As String
        Dim bSelected As Boolean = True
        If ActionValidateDelete() Then
            For i = 0 To gv1.Rows.Count - 1
                Dim chkDelete As CheckBox = gv1.Rows(i).Cells(0).Controls(1)
                If chkDelete.Checked Then
                    strEmpID = gv1.Rows(i).Cells(2).Text.ToUpper.ToString
                    strPeriod = gv1.Rows(i).Cells(14).Text.ToUpper.ToString
                    strDate = gv1.Rows(i).Cells(4).Text.ToUpper.ToString

                    ssql = "Exec sp_ls_InsDel_LeaveApplication '" & strEmpID & "','" & "A" & "','" & strDate & "','" & "Type" & "','" & _
                                        strPeriod & "','" & "reason" & "','" & "destinaton" & "','" & Session("EmpID").ToString & "','" & Now & "','" & "Status" & "','" & _
                                        "Superior" & "','" & "ApprovedDate" & "','" & "Remark" & "','" & "DEL" & "'"
                    mySQL.ExecuteSQL(ssql)

                End If
            Next
            imgBtnPrint_Click(Nothing, Nothing)
        End If
        iniatial()
    End Sub
    Private Sub iniatial()
        ddlLeaveType.SelectedValue = ""
        ddlApprovedBy.SelectedValue = ""
        ddlPeriod.SelectedValue = ""
        ddlStatus.SelectedValue = ""
        txtDate.Text = ""
        txtDateApproved.Text = ""
        txtDestination.Text = ""
        txtReason.Text = ""
        txtRemarks.Text = ""
        ddlApprovedBy.Enabled = False
        txtDateApproved.Enabled = False
        txtRemarks.Enabled = False
        imgBtnCancel_click(Nothing, Nothing)

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

    Protected Sub imgbtnReProcess_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnReProcess.Click
        ssql = "exec [sp_tms_insUpdRawData] "
        mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

        Dim strScript As String = "<script language=" & """" & "javascript" & """" & ">"
        strScript &= "alert('Process Completed!');"
        strScript &= "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "Process Completed!", strScript)
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStatus.SelectedIndexChanged
        If (ddlStatus.SelectedValue = "A" Or ddlStatus.SelectedValue = "R") And Not txtEMPLOYEE_PROFILE_ID.Text = "" Then
            ddlApprovedBy.Enabled = True
            txtDateApproved.Enabled = True
            imgbtnDateApproved.Enabled = True
            txtRemarks.Enabled = True
            Dim ddlapplevel As String, mydsapplevel As DataSet

            ddlapplevel = "Create Table #Result([type] nvarchar(255),[No] int,[Code] nvarchar(255), [Name] nvarchar(255)); Insert Into #Result Select '',0,'','';"
            ddlapplevel = ddlapplevel + "Insert Into #Result Select distinct a.type,seq,ltrim(rtrim(a.SupervisorID)), ltrim(rtrim(b.empname)) from ls_EmpApproval a, is_empmaster b, Employee_CodeName_Vw c where a.supervisorid = b.empid and a.empid = c.code and c.codename = '"
            ddlapplevel = ddlapplevel + txtEMPLOYEE_PROFILE_ID.Text + "' order by a.type desc,ltrim(rtrim(a.SupervisorID));"
            ddlapplevel = ddlapplevel + " Select * From #Result; Drop Table #Result"
            mydsapplevel = mySQL.ExecuteSQL(ddlapplevel)
            If mydsapplevel.Tables.Count > 0 Then
                If mydsapplevel.Tables(0).Columns.Count > 1 Then
                    ddlApprovedBy.DataTextField = "Name"
                    ddlApprovedBy.DataValueField = "Code"
                    ddlApprovedBy.DataSource = mydsapplevel
                    ddlApprovedBy.DataBind()
                End If
            End If
            mydsapplevel = Nothing
            ddlapplevel = Nothing
        Else
            ddlApprovedBy.Enabled = False
            txtDateApproved.Enabled = False
            imgbtnDateApproved.Enabled = False
            txtRemarks.Enabled = False
        End If
    End Sub

    Private Sub enabled()
        ddlLeaveType.Enabled = True
        ddlPeriod.Enabled = True
        txtDate.Enabled = True
        txtDestination.Enabled = True
        txtReason.Enabled = True
        ddlStatus.Enabled = True
    End Sub

    Private Sub disabled()
        ddlLeaveType.Enabled = False
        ddlPeriod.Enabled = False
        txtDate.Enabled = False
        txtDestination.Enabled = False
        txtReason.Enabled = False
        ddlStatus.Enabled = False
        ddlLeaveType.SelectedIndex = -1
        ddlPeriod.SelectedIndex = -1
        ddlStatus.SelectedIndex = -1
        txtDate.Text = ""
        txtDateApproved.Text = ""
        txtDestination.Text = ""
        txtReason.Text = ""
        txtRemarks.Text = ""
        ddlApprovedBy.SelectedIndex = -1
    End Sub

    Protected Sub imgBtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAdd.Click
        imgBtnSubmit.visible = True
        imgBtnAdd.visible = False
        imgBtnEdit.Visible = False
        imgBtnCancel.visible = True
        imgBtnUpdate.Visible = False
        enabled()
    End Sub

    Protected Sub imgBtnEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnEdit.Click
        imgBtnSubmit.visible = False
        imgBtnAdd.visible = False
        imgBtnEdit.Visible = False
        imgBtnCancel.visible = True
        imgBtnUpdate.visible = True
        enabled()
        Dim strEmpID As String
        Dim strPeriod As String
        Dim strDate As String
        Dim bSelected As Boolean = True
        If ActionValidateEdit() Then
            For i = 0 To gv1.Rows.Count - 1
                Dim chkDelete As CheckBox = gv1.Rows(i).Cells(0).Controls(1)
                If chkDelete.Checked Then
                    strEmpID = gv1.Rows(i).Cells(2).Text.ToUpper.ToString
                    strPeriod = gv1.Rows(i).Cells(14).Text.ToUpper.ToString
                    strDate = gv1.Rows(i).Cells(4).Text.ToUpper.ToString

                    If ddlLeaveType.Items.FindByText(gv1.Rows(i).Cells(6).Text.ToUpper.ToString) Is Nothing Then
                        ddlLeaveType.SelectedIndex = -1
                    Else
                        ddlLeaveType.SelectedValue = ddlLeaveType.Items.FindByText(gv1.Rows(i).Cells(6).Text.ToUpper.ToString).Value
                    End If
                    If ddlPeriod.Items.FindByValue(gv1.Rows(i).Cells(14).Text.ToUpper.ToString) Is Nothing Then
                        ddlPeriod.SelectedIndex = -1
                    Else
                        ddlPeriod.SelectedValue = ddlPeriod.Items.FindByValue(gv1.Rows(i).Cells(14).Text.ToUpper.ToString).Value
                    End If
                    txtDate.Text = gv1.Rows(i).Cells(4).Text.ToUpper.ToString
                    txtDestination.Text = gv1.Rows(i).Cells(7).Text.ToUpper.ToString
                    txtReason.Text = gv1.Rows(i).Cells(9).Text.ToUpper.ToString
                    If ddlStatus.Items.FindByText(gv1.Rows(i).Cells(8).Text.ToString.Trim) Is Nothing Then
                        ddlStatus.SelectedIndex = -1
                    Else
                        ddlStatus.SelectedValue = ddlStatus.Items.FindByText(gv1.Rows(i).Cells(8).Text.ToString.Trim).Value
                    End If
                    If ddlStatus.SelectedValue = "P" Then
                        txtDateApproved.Text = ""
                        txtRemarks.Text = ""
                        ddlApprovedBy.SelectedIndex = -1
                    Else
                        txtDateApproved.Text = gv1.Rows(i).Cells(12).Text.ToUpper.ToString
                        txtRemarks.Text = gv1.Rows(i).Cells(13).Text.ToUpper.ToString
                        If ddlApprovedBy.Items.FindByText(gv1.Rows(i).Cells(11).Text.ToString) Is Nothing Then
                            ddlApprovedBy.SelectedIndex = -1
                        Else
                            ddlApprovedBy.SelectedItem.Selected = gv1.Rows(i).Cells(11).Text.ToString
                        End If
                    End If

                End If
            Next
            imgBtnPrint_Click(Nothing, Nothing)
        End If

    End Sub

    Protected Sub imgBtnCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnCancel.Click
        disabled()
        imgBtnSubmit.visible = False
        imgBtnAdd.visible = True
        imgBtnEdit.Visible = True
        imgBtnCancel.visible = False
        imgBtnUpdate.visible = False
    End Sub

    Protected Sub imgBtnUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnUpdate.Click
        empcode = mySetting.GetEmployeeCodeByCodeName_ReturnString(Session("Company").ToString, txtEMPLOYEE_PROFILE_ID.Text)
        ssql = "select dbo.fn_ReturnEmpIDByCodeName('" & Session("Company").ToString & "','" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "'),dbo.fn_UnDisplayDate('" & txtDATEFROM.Text & "','" & Session("Company").ToString & "','" & Session("Module").ToString & "'),dbo.fn_UnDisplayDate('" & txtDATETO.Text & "','" & Session("Company").ToString & "','" & Session("Module").ToString & "'),@@spid"
        myDS = New DataSet()
        myDS = mySQL.ExecuteSQL(ssql)

        If ValidateUpdate(myDS) Then
            Dim strDateApproved, strDestination As String

            If txtDate.Text = "" Then
                strDateApproved = "01/01/1900 00:00:00"
            Else
                strDateApproved = txtDate.Text
            End If

            If Trim(txtDestination.Text) = "" Then
                strDestination = "---"
            Else
                strDestination = Trim(txtDestination.Text)
            End If

            ssql1 = "exec sp_ls_InsDel_LeaveApplication '" & empcode & "','" & "A" & "','" & _
                           txtDate.Text & "','" & _
                            Trim(ddlLeaveType.SelectedValue) & "','" & _
                            Trim(ddlPeriod.SelectedValue) & "','" & _
                            Trim(txtReason.Text) & "','" & _
                            strDestination & "','" & _
                            Session("EmpID").ToString & "','" & Now & "','" & _
                            Trim(ddlStatus.SelectedValue) & "','" & _
                            Trim(ddlApprovedBy.SelectedValue) & "','" & _
                            strDateApproved & "','" & _
                            Trim(txtRemarks.Text) & "','" & _
                            "UPD" & "'"
            mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
            Dim strScript As String = "<script language=" & """" & "javascript" & """" & ">"
            strScript &= "alert('Updated Completed!');"
            strScript &= "<" & "/script>"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "Process Completed!", strScript)
            imgBtnPrint_Click(Nothing, Nothing)
            iniatial()
        End If
    End Sub
End Class

