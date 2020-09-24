Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Partial Class Pages_TOTO_KPI_EMPLOYEE_ASSIGNSKILL_VIEW
    Inherits System.Web.UI.Page
#Region "Public Declaration"
    Private WithEvents mySQL As New clsSQL, myDS As New DataSet, myDS1 As New DataSet, myDS2 As New DataSet, mySetting As New clsGlobalSetting
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Public ssql As String, i As Integer, j As Integer, SearchByPage As Boolean, RecFound As Boolean, CountRecord As Integer
    Public AllowView As Boolean, AllowInsert As Boolean, AllowUpdate As Boolean, AllowDelete As Boolean, AllowPrint As Boolean
    Public _currentPageNumber As Integer, _totalPage As Double = 1, _totalRecords As Integer
    Public myDE As New DictionaryEntry, myHT As Hashtable
    Public myDataType As New clsGlobalSetting.DataType, SearchByFilter As Boolean = False
    Public td1, td2 As DataTable

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
            blindAll()
            blindSkill()
            'blindDepartment()
            'blindSection()
            'blindEmp()
            'blindProcess()
            'blindItem()
            'blindModel()
            'blindCountry()
            'blindStation()
            'blindStatus()

        End If
    End Sub

    Private Sub PagePreload()
        Session("Module") = "TOTO_KPI"
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")

        mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourDef, "btnPrint.png")

        mySetting.GetImgUrl(imgKeyYEAR, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyEMPLOYEE_PROFILE_ID, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnEMPLOYEE_PROFILE_ID, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)

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
                Dim ddlYearssql As String, myDS5 As DataSet

                ddlYearssql = "exec sp_is_GetYear '-50'"
                myDS5 = mySQL.ExecuteSQL(ddlYearssql)
                If myDS5.Tables.Count > 0 Then
                    If myDS5.Tables(0).Columns.Count > 1 Then
                        ddlYEAR.DataTextField = "Name"
                        ddlYEAR.DataValueField = "Code"
                        ddlYEAR.DataSource = myDS5
                        ddlYEAR.DataBind()
                        ddlYEAR.SelectedValue = Year(Now())
                    End If
                End If
                For Each item As ListItem In ddlYEAR.Items

                    If Not chkYear(item.Value) Then
                        item.Attributes.Add("disabled", "disabled")
                    End If
                Next
                myDS5 = Nothing
            End If
            myDS = Nothing
            myDT1 = Nothing
            myDT2 = Nothing
        End If

        AutoAdjustPosition("2")

        imgBtnPrint.CssClass = buttonPosition1

    End Sub

    Private Function chkYear(ByVal newYear As String) As Boolean
        chkYear = False

        Dim sqlChk As String
        Dim dsChk As DataSet

        sqlChk = "exec sp_KPI_ChkModify '" & Session("Company").ToString & "','" & newYear & "'"
        dsChk = mySQL.ExecuteSQL(sqlChk)

        If dsChk.Tables.Count > 0 Then
            If dsChk.Tables(0).Rows.Count > 0 Then
                If dsChk.Tables(0).Rows(0).Item(0).ToString = "ERROR" Then
                    Exit Function
                End If
            End If
        End If

        chkYear = True

    End Function
    Private Sub blindAll()
        ssql1 = "Exec sp_KPI_SelAssignSkill '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','" & ddlEMPLOYEE_PROFILE_ID.SelectedValue & "','" & ddlYEAR.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','" & ddlOCP_ID_SECTION.SelectedValue & "','" & ddlOPTION_PROCESS.SelectedValue & "','" & ddlOPTION_CATEGORY.SelectedValue & "','" & ddlOPTION_MODEL.SelectedValue & "','" & ddlOPTION_COUNTRY.SelectedValue & "','" & ddlOPTION_STATION.SelectedValue & "','" & ddlEVALUATION.SelectedValue & "','" & ddlDIRECT.SelectedValue & "','ALL'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then

            ddlOCP_ID_DEPARTMENT.DataSource = myDS1.Tables(0)
            ddlOCP_ID_DEPARTMENT.DataTextField = "Name"
            ddlOCP_ID_DEPARTMENT.DataValueField = "Code"
            ddlOCP_ID_DEPARTMENT.DataBind()

            ddlOCP_ID_SECTION.DataSource = myDS1.Tables(1)
            ddlOCP_ID_SECTION.DataTextField = "Name"
            ddlOCP_ID_SECTION.DataValueField = "Code"
            ddlOCP_ID_SECTION.DataBind()

            ddlEMPLOYEE_PROFILE_ID.DataSource = myDS1.Tables(2)
            ddlEMPLOYEE_PROFILE_ID.DataTextField = "Name"
            ddlEMPLOYEE_PROFILE_ID.DataValueField = "Code"
            ddlEMPLOYEE_PROFILE_ID.DataBind()

            ddlOPTION_PROCESS.DataSource = myDS1.Tables(3)
            ddlOPTION_PROCESS.DataTextField = "Name"
            ddlOPTION_PROCESS.DataValueField = "Code"
            ddlOPTION_PROCESS.DataBind()

            ddlOPTION_CATEGORY.DataSource = myDS1.Tables(4)
            ddlOPTION_CATEGORY.DataTextField = "Name"
            ddlOPTION_CATEGORY.DataValueField = "Code"
            ddlOPTION_CATEGORY.DataBind()

            ddlOPTION_MODEL.DataSource = myDS1.Tables(5)
            ddlOPTION_MODEL.DataTextField = "Name"
            ddlOPTION_MODEL.DataValueField = "Code"
            ddlOPTION_MODEL.DataBind()

            ddlOPTION_COUNTRY.DataSource = myDS1.Tables(6)
            ddlOPTION_COUNTRY.DataTextField = "Name"
            ddlOPTION_COUNTRY.DataValueField = "Code"
            ddlOPTION_COUNTRY.DataBind()

            ddlOPTION_STATION.DataSource = myDS1.Tables(7)
            ddlOPTION_STATION.DataTextField = "Name"
            ddlOPTION_STATION.DataValueField = "Code"
            ddlOPTION_STATION.DataBind()

            ddlEVALUATION.DataSource = myDS1.Tables(8)
            ddlEVALUATION.DataTextField = "Name"
            ddlEVALUATION.DataValueField = "Code"
            ddlEVALUATION.DataBind()

            ddlDIRECT.DataSource = myDS1.Tables(9)
            ddlDIRECT.DataTextField = "Name"
            ddlDIRECT.DataValueField = "Code"
            ddlDIRECT.DataBind()
        End If
    End Sub

    Private Sub blindSkill()
        ssql1 = "Exec sp_KPI_SelAssignSkill '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','" & ddlEMPLOYEE_PROFILE_ID.SelectedValue & "','" & ddlYEAR.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','" & ddlOCP_ID_SECTION.SelectedValue & "','" & ddlOPTION_PROCESS.SelectedValue & "','" & ddlOPTION_CATEGORY.SelectedValue & "','" & ddlOPTION_MODEL.SelectedValue & "','" & ddlOPTION_COUNTRY.SelectedValue & "','" & ddlOPTION_STATION.SelectedValue & "','" & ddlEVALUATION.SelectedValue & "','" & ddlDIRECT.SelectedValue & "','Search'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count = 2 Then
            ViewState("td1") = myDS1.Tables(0)
            ViewState("td2") = myDS1.Tables(1)
            gv1.DataSource = myDS1.Tables(0)
            gv1.DataBind()
            gv2.DataSource = myDS1.Tables(1)
            gv2.DataBind()
        End If
    End Sub
    Private Sub blindStatus()
        ssql1 = "Exec sp_KPI_SelAssignSkill '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','" & ddlEMPLOYEE_PROFILE_ID.SelectedValue & "','" & ddlYEAR.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','" & ddlOCP_ID_SECTION.SelectedValue & "','','" & ddlOPTION_CATEGORY.SelectedValue & "','" & ddlOPTION_MODEL.SelectedValue & "','" & ddlOPTION_COUNTRY.SelectedValue & "','" & ddlOPTION_STATION.SelectedValue & "','','" & ddlDIRECT.SelectedValue & "','BlindEvalua'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlEVALUATION.DataSource = myDS1.Tables(0)
            ddlEVALUATION.DataTextField = "Name"
            ddlEVALUATION.DataValueField = "Code"
            ddlEVALUATION.DataBind()
        End If
    End Sub

    Private Sub blindProcess()
        ssql1 = "Exec sp_KPI_SelAssignSkill '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','" & ddlEMPLOYEE_PROFILE_ID.SelectedValue & "','" & ddlYEAR.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','" & ddlOCP_ID_SECTION.SelectedValue & "','','" & ddlOPTION_CATEGORY.SelectedValue & "','" & ddlOPTION_MODEL.SelectedValue & "','" & ddlOPTION_COUNTRY.SelectedValue & "','" & ddlOPTION_STATION.SelectedValue & "','" & ddlEVALUATION.SelectedValue & "','" & ddlDIRECT.SelectedValue & "','BlindProcess'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlOPTION_PROCESS.DataSource = myDS1.Tables(0)
            ddlOPTION_PROCESS.DataTextField = "Name"
            ddlOPTION_PROCESS.DataValueField = "Code"
            ddlOPTION_PROCESS.DataBind()

            If myDS1.Tables(0).Rows.Count = 2 And (ddlOPTION_CATEGORY.SelectedValue <> "" Or ddlOPTION_MODEL.SelectedValue <> "" Or ddlOPTION_CATEGORY.SelectedValue <> "" Or ddlOPTION_STATION.SelectedValue <> "") Then
                ddlOPTION_PROCESS.SelectedIndex = 1
            End If
        End If
    End Sub

    Private Sub blindItem()
        ssql1 = "Exec sp_KPI_SelAssignSkill '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','" & ddlEMPLOYEE_PROFILE_ID.SelectedValue & "','" & ddlYEAR.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','" & ddlOCP_ID_SECTION.SelectedValue & "','" & ddlOPTION_PROCESS.SelectedValue & "','','" & ddlOPTION_MODEL.SelectedValue & "','" & ddlOPTION_COUNTRY.SelectedValue & "','" & ddlOPTION_STATION.SelectedValue & "','" & ddlEVALUATION.SelectedValue & "','" & ddlDIRECT.SelectedValue & "','BlindItem'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlOPTION_CATEGORY.DataSource = myDS1.Tables(0)
            ddlOPTION_CATEGORY.DataTextField = "Name"
            ddlOPTION_CATEGORY.DataValueField = "Code"
            ddlOPTION_CATEGORY.DataBind()

            If myDS1.Tables(0).Rows.Count = 2 And (ddlOPTION_CATEGORY.SelectedValue <> "" Or ddlOPTION_MODEL.SelectedValue <> "" Or ddlOPTION_STATION.SelectedValue <> "" Or ddlOPTION_PROCESS.SelectedValue <> "") Then
                ddlOPTION_CATEGORY.SelectedIndex = 1
            End If
        End If
    End Sub

    Private Sub blindModel()
        ssql1 = "Exec sp_KPI_SelAssignSkill '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','" & ddlEMPLOYEE_PROFILE_ID.SelectedValue & "','" & ddlYEAR.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','" & ddlOCP_ID_SECTION.SelectedValue & "','" & ddlOPTION_PROCESS.SelectedValue & "','" & ddlOPTION_CATEGORY.SelectedValue & "','','" & ddlOPTION_COUNTRY.SelectedValue & "','" & ddlOPTION_STATION.SelectedValue & "','" & ddlEVALUATION.SelectedValue & "','" & ddlDIRECT.SelectedValue & "','BlindModel'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlOPTION_MODEL.DataSource = myDS1.Tables(0)
            ddlOPTION_MODEL.DataTextField = "Name"
            ddlOPTION_MODEL.DataValueField = "Code"
            ddlOPTION_MODEL.DataBind()

            If myDS1.Tables(0).Rows.Count = 2 And (ddlOPTION_CATEGORY.SelectedValue <> "" Or ddlOPTION_STATION.SelectedValue <> "" Or ddlOPTION_COUNTRY.SelectedValue <> "" Or ddlOPTION_PROCESS.SelectedValue <> "") Then
                ddlOPTION_MODEL.SelectedIndex = 1
            End If
        End If
    End Sub

    Private Sub blindCountry()
        ssql1 = "Exec sp_KPI_SelAssignSkill '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','" & ddlEMPLOYEE_PROFILE_ID.SelectedValue & "','" & ddlYEAR.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','" & ddlOCP_ID_SECTION.SelectedValue & "','" & ddlOPTION_PROCESS.SelectedValue & "','" & ddlOPTION_CATEGORY.SelectedValue & "','" & ddlOPTION_MODEL.SelectedValue & "','','" & ddlOPTION_STATION.SelectedValue & "','" & ddlEVALUATION.SelectedValue & "','" & ddlDIRECT.SelectedValue & "','BlindCountry'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlOPTION_COUNTRY.DataSource = myDS1.Tables(0)
            ddlOPTION_COUNTRY.DataTextField = "Name"
            ddlOPTION_COUNTRY.DataValueField = "Code"
            ddlOPTION_COUNTRY.DataBind()

            If myDS1.Tables(0).Rows.Count = 2 And (ddlOPTION_STATION.SelectedValue <> "" Or ddlOPTION_MODEL.SelectedValue <> "" Or ddlOPTION_CATEGORY.SelectedValue <> "" Or ddlOPTION_PROCESS.SelectedValue <> "") Then
                ddlOPTION_COUNTRY.SelectedIndex = 1
            End If
        End If
    End Sub

    Private Sub blindStation()
        ssql1 = "Exec sp_KPI_SelAssignSkill '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','" & ddlEMPLOYEE_PROFILE_ID.SelectedValue & "','" & ddlYEAR.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','" & ddlOCP_ID_SECTION.SelectedValue & "','" & ddlOPTION_PROCESS.SelectedValue & "','" & ddlOPTION_CATEGORY.SelectedValue & "','" & ddlOPTION_MODEL.SelectedValue & "','" & ddlOPTION_COUNTRY.SelectedValue & "','','" & ddlEVALUATION.SelectedValue & "','" & ddlDIRECT.SelectedValue & "','BlindStation'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlOPTION_STATION.DataSource = myDS1.Tables(0)
            ddlOPTION_STATION.DataTextField = "Name"
            ddlOPTION_STATION.DataValueField = "Code"
            ddlOPTION_STATION.DataBind()

            If myDS1.Tables(0).Rows.Count = 2 And (ddlOPTION_CATEGORY.SelectedValue <> "" Or ddlOPTION_MODEL.SelectedValue <> "" Or ddlOPTION_CATEGORY.SelectedValue <> "" Or ddlOPTION_PROCESS.SelectedValue <> "") Then
                ddlOPTION_STATION.SelectedIndex = 1
            End If
        End If
    End Sub

    Protected Sub blindDepartment()
        ssql1 = "Exec sp_KPI_SelAssignSkill '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','" & ddlEMPLOYEE_PROFILE_ID.SelectedValue & "','" & ddlYEAR.SelectedValue & "','','" & ddlOCP_ID_SECTION.SelectedValue & "','" & ddlOPTION_PROCESS.SelectedValue & "','" & ddlOPTION_CATEGORY.SelectedValue & "','" & ddlOPTION_MODEL.SelectedValue & "','" & ddlOPTION_COUNTRY.SelectedValue & "','','" & ddlEVALUATION.SelectedValue & "','" & ddlDIRECT.SelectedValue & "','BlindDept'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlOCP_ID_DEPARTMENT.DataSource = myDS1.Tables(0)
            ddlOCP_ID_DEPARTMENT.DataTextField = "Name"
            ddlOCP_ID_DEPARTMENT.DataValueField = "Code"
            ddlOCP_ID_DEPARTMENT.DataBind()
        End If

        If myDS1.Tables(0).Rows.Count = 2 And (ddlOCP_ID_DEPARTMENT.SelectedValue <> "") Then
            ddlOCP_ID_SECTION.SelectedIndex = 1
        End If

    End Sub

    Protected Sub blindSection()
        ssql1 = "Exec sp_KPI_SelAssignSkill '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','" & ddlEMPLOYEE_PROFILE_ID.SelectedValue & "','" & ddlYEAR.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','','" & ddlOPTION_PROCESS.SelectedValue & "','" & ddlOPTION_CATEGORY.SelectedValue & "','" & ddlOPTION_MODEL.SelectedValue & "','" & ddlOPTION_COUNTRY.SelectedValue & "','','" & ddlEVALUATION.SelectedValue & "','" & ddlDIRECT.SelectedValue & "','BlindSect'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlOCP_ID_SECTION.DataSource = myDS1.Tables(0)
            ddlOCP_ID_SECTION.DataTextField = "Name"
            ddlOCP_ID_SECTION.DataValueField = "Code"
            ddlOCP_ID_SECTION.DataBind()
        End If

        If myDS1.Tables(0).Rows.Count = 2 And (ddlOCP_ID_SECTION.SelectedValue <> "") Then
            ddlOCP_ID_DEPARTMENT.SelectedIndex = 1
        End If
    End Sub

    Protected Sub blindEmp()
        ssql1 = "Exec sp_KPI_SelAssignSkill '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','','" & ddlYEAR.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','" & ddlOCP_ID_SECTION.SelectedValue & "','" & ddlOPTION_PROCESS.SelectedValue & "','" & ddlOPTION_CATEGORY.SelectedValue & "','" & ddlOPTION_MODEL.SelectedValue & "','" & ddlOPTION_COUNTRY.SelectedValue & "','','" & ddlEVALUATION.SelectedValue & "','" & ddlDIRECT.SelectedValue & "','BlindEmp'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlEMPLOYEE_PROFILE_ID.DataSource = myDS1.Tables(0)
            ddlEMPLOYEE_PROFILE_ID.DataTextField = "Name"
            ddlEMPLOYEE_PROFILE_ID.DataValueField = "Code"
            ddlEMPLOYEE_PROFILE_ID.DataBind()
        End If
    End Sub

    Private Sub ShowMessage(ByVal message As String)
        Dim scriptString As String = "<script language=JavaScript>"
        scriptString += "alert('" + message + "');"
        scriptString += "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "ShowMessage", scriptString)
    End Sub

    Private Sub ProcessCompleted()
        'txtDateStart.Text = ""
        'txtDateEnd.Text = ""
        'chkOverwrite.Checked = False

        Dim strScript As String = "<script language=" & """" & "javascript" & """" & ">"
        strScript &= "alert('Update Completed!');"
        strScript &= "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "Update Completed!", strScript)
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

    Protected Sub ddlYEAR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlYEAR.SelectedIndexChanged
        blindAll()
        blindSkill()
    End Sub

    Protected Sub ddlOPTION_PROCESS_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOPTION_PROCESS.SelectedIndexChanged
        blindItem()
        blindModel()
        blindCountry()
        blindStation()
        blindDepartment()
        blindSection()
        blindEmp()
        blindSkill()
    End Sub

    Protected Sub ddlOPTION_CATEGORY_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOPTION_CATEGORY.SelectedIndexChanged
        blindProcess()
        blindModel()
        blindCountry()
        blindStation()
        blindDepartment()
        blindSection()
        blindEmp()
        blindSkill()
    End Sub

    Protected Sub ddlOPTION_MODEL_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOPTION_MODEL.SelectedIndexChanged
        blindItem()
        blindCountry()
        blindStation()
        blindProcess()
        blindDepartment()
        blindSection()
        blindEmp()
        blindSkill()
    End Sub

    Protected Sub ddlOPTION_COUNTRY_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOPTION_COUNTRY.SelectedIndexChanged
        blindModel()
        blindStation()
        blindItem()
        blindProcess()
        blindDepartment()
        blindSection()
        blindEmp()
        blindSkill()
    End Sub

    Protected Sub ddlOCP_ID_DEPARTMENT_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOCP_ID_DEPARTMENT.SelectedIndexChanged
        blindSection()
        blindEmp()
        blindProcess()
        blindItem()
        blindModel()
        blindCountry()
        blindStation()
        blindSkill()
    End Sub

    Protected Sub ddlOCP_ID_SECTION_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOCP_ID_SECTION.SelectedIndexChanged
        blindDepartment()
        blindEmp()
        blindProcess()
        blindItem()
        blindModel()
        blindCountry()
        blindStation()
        blindSkill()
    End Sub

    Protected Sub ddlEMPLOYEE_PROFILE_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEMPLOYEE_PROFILE_ID.SelectedIndexChanged
        blindDepartment()
        blindSection()
        blindProcess()
        blindItem()
        blindModel()
        blindCountry()
        blindStation()
        blindSkill()
    End Sub

    Private Sub DeleteCompleted()
        'txtDateStart.Text = ""
        'txtDateEnd.Text = ""
        'chkOverwrite.Checked = False

        Dim strScript As String = "<script language=" & """" & "javascript" & """" & ">"
        strScript &= "alert('Delete Completed!');"
        strScript &= "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "Delete Completed!", strScript)
        lblresult2.Text = ""
    End Sub

    Protected Sub LinkButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lbtn As LinkButton = DirectCast(sender, LinkButton)
        Dim url As String = lbtn.CommandName
        Dim employee As String = lbtn.CommandArgument
        ssql = "select employee_profile_id from Employee_CodeName_Vw where codename = '" & employee.ToString.Replace("'", "''") & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                ssql1 = "exec sp_KPI_chkWeightage '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & ddlYEAR.SelectedValue & "','CHK'"
                myDS2 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
                If myDS2.Tables.Count > 0 Then
                    If myDS2.Tables(0).Rows.Count > 0 Then
                        If myDS2.Tables(0).Rows(0).Item(0).ToString = "ERROR" Then
                            lblresult2.Text = "Weightage for Special Skill is not fully filled up or not equal to 50%!"
                            ShowMessage(lblresult2.Text)
                            ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", lblresult2.Text, True)
                            Exit Sub
                        End If
                    End If
                End If
            End If
        End If
        Response.Redirect(url)
    End Sub
    Protected Sub gv1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)
        Dim empid, year, masterskill As String
        Dim index As Integer = Convert.ToInt32(e.RowIndex)

        td1 = TryCast(ViewState("td1"), DataTable)
        td2 = TryCast(ViewState("td2"), DataTable)

        empid = gv2.Rows(index).Cells(9).Text
        year = gv2.Rows(index).Cells(2).Text
        masterskill = gv2.Rows(index).Cells(13).Text

        td1.Rows(index).Delete()
        td1.AcceptChanges()
        td2.Rows(index).Delete()
        td2.AcceptChanges()

        ViewState("td1") = td1
        ViewState("td2") = td2
        gv1.DataSource = td1
        gv2.DataSource = td2
        gv1.DataBind()
        gv2.DataBind()

        ssql = "Exec sp_KPI_insUpdDelCommonSkill N'" & Session("Company").ToString & "',N'" & empid & "',N'" & masterskill & "','','','','','','','','','','','','0','0','" & Session("EmpID").ToString & "','DEL','" & year & "',''"
        mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

        DeleteCompleted()
    End Sub

    Protected Sub ddlEVALUATION_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEVALUATION.SelectedIndexChanged
        blindDepartment()
        blindSection()
        blindEmp()
        blindProcess()
        blindItem()
        blindModel()
        blindCountry()
        blindStation()
        blindSkill()
    End Sub

    Protected Sub ddlDIRECT_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDIRECT.SelectedIndexChanged
        blindDepartment()
        blindSection()
        blindEmp()
        blindProcess()
        blindItem()
        blindModel()
        blindCountry()
        blindStation()
        blindSkill()
    End Sub
End Class

