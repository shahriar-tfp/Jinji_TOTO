Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.DateTime
Imports System.Globalization
Partial Class Pages_InformationSystem_Employee_Increment_view
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
    Dim buttonPosition4 As String = "B1_", buttonPosition5 As String = "B2_", buttonPosition6 As String = "B3_", buttonPosition7 As String = "B4_"
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
        Session("Module") = "INFORMATION_SYSTEM"
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")

        mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourDef, "btnSearch.png")
        mySetting.GetBtnImgUrl(imgBtnAdd, Session("Company").ToString, btnColourDef, "btnAdd.png")
        mySetting.GetBtnImgUrl(imgBtnEdit, Session("Company").ToString, btnColourDef, "btnEdit.png")
        mySetting.GetBtnImgUrl(imgbtnUpdate, Session("Company").ToString, btnColourDef, "btnUpdate.png")
        mySetting.GetBtnImgUrl(imgbtnSubmit, Session("Company").ToString, btnColourDef, "btnSubmit.png")
        mySetting.GetBtnImgUrl(ImgBtnDelete, Session("Company").ToString, btnColourDef, "btnDelete.png")
        mySetting.GetBtnImgUrl(imgbtnCancel, Session("Company").ToString, btnColourDef, "btnCancel.png")
        mySetting.GetBtnImgUrl(imgbtnCancel2, Session("Company").ToString, btnColourDef, "btnCancel.png")
        mySetting.GetImgUrl(imgbtnEMPLOYEE_PROFILE_ID, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)

        mySetting.GetImgTypeUrl(imgTop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Filter.png")
        mySetting.GetImgTypeUrl(imgBottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")

        mySetting.GetImgUrl(imgKeyEFFECTIVE_DATE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        lblEFFECTIVE_DATE.Text = "Effective Date"
        lblEFFECTIVE_DATE.CssClass = "wordstyle12"
        mySetting.GetImgUrl(imgbtnEFFECTIVE_DATE, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
        mySetting.PopUpCalendar_ImageButton(imgbtnEFFECTIVE_DATE, Form.ID, "txtEFFECTIVE_DATE")

        mySetting.GetImgUrl(imgKeyOPTION_TYPE_INCREMENT, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        lblOPTION_TYPE_INCREMENT.Text = "Type Increment"
        lblOPTION_TYPE_INCREMENT.CssClass = "wordstyle12"
        mySetting.GetImgUrl(imgbtnOPTION_TYPE_INCREMENT, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        imgbtnOPTION_TYPE_INCREMENT.Visible = False
        mySetting.GetDropdownlistValue("Employee_Increment", "OPTION_TYPE_INCREMENT", ddlOPTION_TYPE_INCREMENT)

        mySetting.GetImgUrl(imgKeyOLD_SALARY, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        lblOLD_SALARY.Text = "Old Salary"
        lblOLD_SALARY.CssClass = "wordstyle12"
        mySetting.GetImgUrl(imgbtnOLD_SALARY, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        imgbtnOLD_SALARY.Visible = False

        mySetting.GetImgUrl(imgKeyOCP_ID_JOB_GRADE_NEW, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        lblOCP_ID_JOB_GRADE_NEW.Text = "New Grade"
        lblOCP_ID_JOB_GRADE_NEW.CssClass = "wordstyle12"
        mySetting.GetImgUrl(imgbtnOCP_ID_JOB_GRADE_NEW, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        imgbtnOCP_ID_JOB_GRADE_NEW.Visible = False
        BindJobGradeNew()

        mySetting.GetImgUrl(imgKeyOCP_ID_APPRAISAL_NEW, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        lblOCP_ID_APPRAISAL_NEW.Text = "New Mark"
        lblOCP_ID_APPRAISAL_NEW.CssClass = "wordstyle12"
        mySetting.GetImgUrl(imgbtnOCP_ID_APPRAISAL_NEW, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        imgbtnOCP_ID_APPRAISAL_NEW.Visible = False
        BindAppraisalNew()

        mySetting.GetImgUrl(imgKeyOCP_ID_JOB_TITLE_NEW, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        lblOCP_ID_JOB_TITLE_NEW.Text = "New Position"
        lblOCP_ID_JOB_TITLE_NEW.CssClass = "wordstyle12"
        mySetting.GetImgUrl(imgbtnOCP_ID_JOB_TITLE_NEW, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        imgbtnOCP_ID_JOB_TITLE_NEW.Visible = False
        BindJobTitleNew()

        mySetting.GetImgUrl(imgKeyINCREMENT, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        lblINCREMENT.Text = "Increment"
        lblINCREMENT.CssClass = "wordstyle12"
        mySetting.GetImgUrl(imgbtnINCREMENT, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        imgbtnINCREMENT.Visible = False

        mySetting.GetImgUrl(imgKeyPERADJ, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        lblPERADJ.Text = "PERF.Adjustment"
        lblPERADJ.CssClass = "wordstyle12"
        mySetting.GetImgUrl(imgbtnPERADJ, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        imgbtnPERADJ.Visible = False

        mySetting.GetImgUrl(imgKeyPROMOTION, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        lblPROMOTION.Text = "Promotion/Upgrading"
        lblPROMOTION.CssClass = "wordstyle12"
        mySetting.GetImgUrl(imgbtnPROMOTION, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        imgbtnPROMOTION.Visible = False

        mySetting.GetImgUrl(imgKeyNEW_SALARY, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        lblNEW_SALARY.Text = "New Salary"
        lblNEW_SALARY.CssClass = "wordstyle12"
        mySetting.GetImgUrl(imgbtnNEW_SALARY, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        imgbtnNEW_SALARY.Visible = False

        mySetting.GetImgUrl(imgKeyTOTALADJ, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        lblTOTALADJ.Text = "Total Adj."
        lblTOTALADJ.CssClass = "wordstyle12"
        mySetting.GetImgUrl(imgbtnTOTALADJ, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        imgbtnTOTALADJ.Visible = False

        mySetting.GetImgUrl(imgKeyTOTALADJPERC, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        lblTOTALADJPERC.Text = "Total Adj. Perc(%)"
        lblTOTALADJPERC.CssClass = "wordstyle12"
        mySetting.GetImgUrl(imgbtnTOTALADJPERC, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        imgbtnTOTALADJPERC.Visible = False

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

        AutoAdjustPosition("3")
        AutoAdjustPosition2("3")
        SetEditable()
        enablefield("1")
        imgBtnPrint.CssClass = buttonPosition1

    End Sub

    Protected Sub imgBtnPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnPrint.Click
        Dim clsRpt As New clsReportServices, myTargetURL As String = ""
        myTargetURL = clsRpt.GetPDFReportURL()
        ssql = "select dbo.fn_ReturnEmpIDByCodeName('" & Session("Company").ToString & "','" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "')"
        myDS = New DataSet()
        myDS = mySQL.ExecuteSQL(ssql)
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                ssql1 = "Exec [sp_is_chkselIncrement] '" & Session("Company").ToString & "','" & myDS.Tables(0).Rows(0).Item(0).ToString & "','',0,0,'','','SEL'"
                myDS1 = New DataSet()
                myDS1 = mySQL.ExecuteSQL(ssql1)
                If myDS1.Tables.Count >= 1 Then
                    gv1.DataSource = myDS1.Tables(0)
                    gv1.DataBind()
                    gv2.DataSource = myDS1.Tables(1)
                    gv2.DataBind()
                    txtCOSTBLOCK.Text = myDS1.Tables(2).Rows(0).Item(0).ToString
                    txtPOSITION.Text = myDS1.Tables(2).Rows(0).Item(1).ToString
                    txtSUPCODE.Text = myDS1.Tables(2).Rows(0).Item(2).ToString
                    txtSALARY.Text = myDS1.Tables(2).Rows(0).Item(3).ToString
                    txtGRADE.Text = myDS1.Tables(2).Rows(0).Item(4).ToString
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

    Sub AutoAdjustPosition2(ByVal strSelect As String)

        Dim myPosition As String
        Dim autonum2 As Integer = 0
        Select Case strSelect
            Case "1"
                myPosition = rowPositionM
            Case "2"
                myPosition = rowPosition
            Case Else
                myPosition = rowPosition
        End Select

        'get field position
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

        'get field position
        autonum = 0
        If lblEFFECTIVE_DATE.Visible = True And txtEFFECTIVE_DATE.Visible = True Then
            autonum += 1
            imgKeyEFFECTIVE_DATE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgKeyEFFECTIVE_DATE.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgKeyEFFECTIVE_DATE.Style.Add("position", "absolute")
            autonum += 1
            lblEFFECTIVE_DATE.CssClass = "wordstyle12"
            lblEFFECTIVE_DATE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            lblEFFECTIVE_DATE.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            lblEFFECTIVE_DATE.Style.Add("position", "absolute")
            autonum += 1
            txtEFFECTIVE_DATE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            txtEFFECTIVE_DATE.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            txtEFFECTIVE_DATE.Style.Add("position", "absolute")
            txtEFFECTIVE_DATE.Width = txtWidth
            autonum += 1
            imgbtnEFFECTIVE_DATE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgbtnEFFECTIVE_DATE.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgbtnEFFECTIVE_DATE.Style.Add("position", "absolute")
        End If

        If lblOPTION_TYPE_INCREMENT.Visible = True And ddlOPTION_TYPE_INCREMENT.Visible = True Then
            autonum += 1
            imgKeyOPTION_TYPE_INCREMENT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgKeyOPTION_TYPE_INCREMENT.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgKeyOPTION_TYPE_INCREMENT.Style.Add("position", "absolute")
            autonum += 1
            lblOPTION_TYPE_INCREMENT.CssClass = "wordstyle12"
            lblOPTION_TYPE_INCREMENT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            lblOPTION_TYPE_INCREMENT.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            lblOPTION_TYPE_INCREMENT.Style.Add("position", "absolute")
            autonum += 1
            ddlOPTION_TYPE_INCREMENT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            ddlOPTION_TYPE_INCREMENT.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            ddlOPTION_TYPE_INCREMENT.Style.Add("position", "absolute")
            ddlOPTION_TYPE_INCREMENT.Width = txtWidth
            autonum += 1
            imgbtnOPTION_TYPE_INCREMENT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgbtnOPTION_TYPE_INCREMENT.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgbtnOPTION_TYPE_INCREMENT.Style.Add("position", "absolute")
        End If

        If lblOLD_SALARY.Visible = True And txtOLD_SALARY.Visible = True Then
            autonum += 1
            imgKeyOLD_SALARY.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgKeyOLD_SALARY.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgKeyOLD_SALARY.Style.Add("position", "absolute")
            autonum += 1
            lblOLD_SALARY.CssClass = "wordstyle12"
            lblOLD_SALARY.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            lblOLD_SALARY.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            lblOLD_SALARY.Style.Add("position", "absolute")
            autonum += 1
            txtOLD_SALARY.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            txtOLD_SALARY.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            txtOLD_SALARY.Style.Add("position", "absolute")
            txtOLD_SALARY.Width = ddlWidth
            autonum += 1
            imgbtnOLD_SALARY.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgbtnOLD_SALARY.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgbtnOLD_SALARY.Style.Add("position", "absolute")
        End If
        
        If lblOCP_ID_JOB_GRADE_NEW.Visible = True And ddlOCP_ID_JOB_GRADE_NEW.Visible = True Then
            autonum += 1
            imgKeyOCP_ID_JOB_GRADE_NEW.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgKeyOCP_ID_JOB_GRADE_NEW.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgKeyOCP_ID_JOB_GRADE_NEW.Style.Add("position", "absolute")
            autonum += 1
            lblOCP_ID_JOB_GRADE_NEW.CssClass = "wordstyle12"
            lblOCP_ID_JOB_GRADE_NEW.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            lblOCP_ID_JOB_GRADE_NEW.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            lblOCP_ID_JOB_GRADE_NEW.Style.Add("position", "absolute")
            autonum += 1
            ddlOCP_ID_JOB_GRADE_NEW.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            ddlOCP_ID_JOB_GRADE_NEW.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            ddlOCP_ID_JOB_GRADE_NEW.Style.Add("position", "absolute")
            ddlOCP_ID_JOB_GRADE_NEW.Width = ddlWidth
            autonum += 1
            imgbtnOCP_ID_JOB_GRADE_NEW.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgbtnOCP_ID_JOB_GRADE_NEW.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgbtnOCP_ID_JOB_GRADE_NEW.Style.Add("position", "absolute")
        End If

        If lblOCP_ID_APPRAISAL_NEW.Visible = True And ddlOCP_ID_APPRAISAL_NEW.Visible = True Then
            autonum += 1
            imgKeyOCP_ID_APPRAISAL_NEW.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgKeyOCP_ID_APPRAISAL_NEW.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgKeyOCP_ID_APPRAISAL_NEW.Style.Add("position", "absolute")
            autonum += 1
            lblOCP_ID_APPRAISAL_NEW.CssClass = "wordstyle12"
            lblOCP_ID_APPRAISAL_NEW.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            lblOCP_ID_APPRAISAL_NEW.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            lblOCP_ID_APPRAISAL_NEW.Style.Add("position", "absolute")
            autonum += 1
            ddlOCP_ID_APPRAISAL_NEW.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            ddlOCP_ID_APPRAISAL_NEW.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            ddlOCP_ID_APPRAISAL_NEW.Style.Add("position", "absolute")
            ddlOCP_ID_APPRAISAL_NEW.Width = ddlWidth
            autonum += 1
            imgbtnOCP_ID_APPRAISAL_NEW.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgbtnOCP_ID_APPRAISAL_NEW.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgbtnOCP_ID_APPRAISAL_NEW.Style.Add("position", "absolute")
        End If

        If lblOCP_ID_JOB_TITLE_NEW.Visible = True And ddlOCP_ID_JOB_TITLE_NEW.Visible = True Then
            autonum += 1
            imgKeyOCP_ID_JOB_TITLE_NEW.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgKeyOCP_ID_JOB_TITLE_NEW.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgKeyOCP_ID_JOB_TITLE_NEW.Style.Add("position", "absolute")
            autonum += 1
            lblOCP_ID_JOB_TITLE_NEW.CssClass = "wordstyle12"
            lblOCP_ID_JOB_TITLE_NEW.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            lblOCP_ID_JOB_TITLE_NEW.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            lblOCP_ID_JOB_TITLE_NEW.Style.Add("position", "absolute")
            autonum += 1
            ddlOCP_ID_JOB_TITLE_NEW.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            ddlOCP_ID_JOB_TITLE_NEW.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            ddlOCP_ID_JOB_TITLE_NEW.Style.Add("position", "absolute")
            ddlOCP_ID_JOB_TITLE_NEW.Width = ddlWidth
            autonum += 1
            imgbtnOCP_ID_JOB_TITLE_NEW.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgbtnOCP_ID_JOB_TITLE_NEW.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgbtnOCP_ID_JOB_TITLE_NEW.Style.Add("position", "absolute")
        End If

        If lblINCREMENT.Visible = True And txtINCREMENT.Visible = True Then
            autonum += 1
            imgKeyINCREMENT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgKeyINCREMENT.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgKeyINCREMENT.Style.Add("position", "absolute")
            autonum += 1
            lblINCREMENT.CssClass = "wordstyle12"
            lblINCREMENT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            lblINCREMENT.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            lblINCREMENT.Style.Add("position", "absolute")
            autonum += 1
            txtINCREMENT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            txtINCREMENT.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            txtINCREMENT.Style.Add("position", "absolute")
            txtINCREMENT.Width = ddlWidth
            autonum += 1
            imgbtnINCREMENT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgbtnINCREMENT.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgbtnINCREMENT.Style.Add("position", "absolute")
        End If

        If lblPERADJ.Visible = True And txtPERADJ.Visible = True Then
            autonum += 1
            imgKeyPERADJ.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgKeyPERADJ.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgKeyPERADJ.Style.Add("position", "absolute")
            autonum += 1
            lblPERADJ.CssClass = "wordstyle12"
            lblPERADJ.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            lblPERADJ.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            lblPERADJ.Style.Add("position", "absolute")
            autonum += 1
            txtPERADJ.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            txtPERADJ.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            txtPERADJ.Style.Add("position", "absolute")
            txtPERADJ.Width = ddlWidth
            autonum += 1
            imgbtnPERADJ.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgbtnPERADJ.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgbtnPERADJ.Style.Add("position", "absolute")
        End If

        If lblPROMOTION.Visible = True And txtPROMOTION.Visible = True Then
            autonum += 1
            imgKeyPROMOTION.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgKeyPROMOTION.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgKeyPROMOTION.Style.Add("position", "absolute")
            autonum += 1
            lblPROMOTION.CssClass = "wordstyle12"
            lblPROMOTION.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            lblPROMOTION.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            lblPROMOTION.Style.Add("position", "absolute")
            autonum += 1
            txtPROMOTION.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            txtPROMOTION.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            txtPROMOTION.Style.Add("position", "absolute")
            txtPROMOTION.Width = ddlWidth
            autonum += 1
            imgbtnPROMOTION.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgbtnPROMOTION.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgbtnPROMOTION.Style.Add("position", "absolute")
        End If

        If lblNEW_SALARY.Visible = True And txtNEW_SALARY.Visible = True Then
            autonum += 1
            imgKeyNEW_SALARY.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgKeyNEW_SALARY.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgKeyNEW_SALARY.Style.Add("position", "absolute")
            autonum += 1
            lblNEW_SALARY.CssClass = "wordstyle12"
            lblNEW_SALARY.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            lblNEW_SALARY.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            lblNEW_SALARY.Style.Add("position", "absolute")
            autonum += 1
            txtNEW_SALARY.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            txtNEW_SALARY.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            txtNEW_SALARY.Style.Add("position", "absolute")
            txtNEW_SALARY.Width = ddlWidth
            autonum += 1
            imgbtnNEW_SALARY.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgbtnNEW_SALARY.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgbtnNEW_SALARY.Style.Add("position", "absolute")
        End If

        If lblTOTALADJ.Visible = True And txtTOTALADJ.Visible = True Then
            autonum += 1
            imgKeyTOTALADJ.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgKeyTOTALADJ.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgKeyTOTALADJ.Style.Add("position", "absolute")
            autonum += 1
            lblTOTALADJ.CssClass = "wordstyle12"
            lblTOTALADJ.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            lblTOTALADJ.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            lblTOTALADJ.Style.Add("position", "absolute")
            autonum += 1
            txtTOTALADJ.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            txtTOTALADJ.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            txtTOTALADJ.Style.Add("position", "absolute")
            txtTOTALADJ.Width = ddlWidth
            autonum += 1
            imgbtnTOTALADJ.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgbtnTOTALADJ.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgbtnTOTALADJ.Style.Add("position", "absolute")
        End If

        If lblTOTALADJPERC.Visible = True And txtTOTALADJPERC.Visible = True Then
            autonum += 1
            imgKeyTOTALADJPERC.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgKeyTOTALADJPERC.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgKeyTOTALADJPERC.Style.Add("position", "absolute")
            autonum += 1
            lblTOTALADJPERC.CssClass = "wordstyle12"
            lblTOTALADJPERC.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            lblTOTALADJPERC.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            lblTOTALADJPERC.Style.Add("position", "absolute")
            autonum += 1
            txtTOTALADJPERC.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            txtTOTALADJPERC.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            txtTOTALADJPERC.Style.Add("position", "absolute")
            txtTOTALADJPERC.Width = ddlWidth
            autonum += 1
            imgbtnTOTALADJPERC.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
            imgbtnTOTALADJPERC.Style.Add("top", (Replace(mySetting.GetObjPosition(EctWidth, autonum, "Y"), "px", "") - 90) & "px")
            imgbtnTOTALADJPERC.Style.Add("position", "absolute")
        End If

        myDS = Nothing

        autonum = autonum / 4
        If autonum Mod 2 <> 0 Then
            autonum = autonum + 1
        End If
        autonum = autonum / 2

        'buttonPosition1 = buttonPosition1 & autonum - 1
        'buttonPosition2 = buttonPosition2 & autonum - 1
        'buttonPosition3 = buttonPosition3 & autonum - 1
        buttonPosition4 = buttonPosition4 & autonum - 4
        buttonPosition5 = buttonPosition5 & autonum - 4
        buttonPosition6 = buttonPosition6 & autonum - 4
        buttonPosition7 = buttonPosition7 & autonum - 4

        imgBtnAdd.CssClass = buttonPosition4
        imgBtnEdit.CssClass = buttonPosition5
        ImgBtnDelete.CssClass = buttonPosition6
        imgbtnSubmit.CssClass = buttonPosition4
        imgbtnCancel.CssClass = buttonPosition5
        imgbtnUpdate.CssClass = buttonPosition4
        imgbtnCancel2.CssClass = buttonPosition5

        'labelPosition1 = labelPosition1 & autonum - 1
        'lblresult2.CssClass = labelPosition1
        'lblresult2.Text = "Click on Cancel Button to return..."
        'imgTop.CssClass = "Display_0"
        'imgBottom.CssClass = panelPosition & autonum

    End Sub

    Private Function ValidatePrint(ByVal myds As DataSet) As Boolean
        If myds.Tables(0).Rows(0).Item(0).ToString = "" Then
            lblresult2.Text = "[" & lblEMPLOYEE_PROFILE_ID.Text & "] Is A Required Field!"
            txtEMPLOYEE_PROFILE_ID.Focus()
            Return False
        Else
            Return True
        End If
    End Function
    Private Sub SetEditable()
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL("Exec sp_sa_GetPageFieldSetting '" & Session("Company").ToString & "','" & Form.ID & "','" & Session("EmpID").ToString & "','EDIT'")
        For i = 0 To myDS.Tables(0).Rows.Count - 1
            Dim myImageKey As ImageButton = Page.FindControl("imgKey" & myDS.Tables(0).Rows(i).Item(0).ToString.Trim)
            Dim myImageButton As ImageButton = Page.FindControl("imgbtn" & myDS.Tables(0).Rows(i).Item(0).ToString.Trim)

            If myDS.Tables(0).Rows(i).Item(1).ToString = "YES" Then 'Option_Primary_Key
                myImageButton.Visible = True
                myImageButton.Enabled = False
            Else
                If myDS.Tables(0).Rows(i).Item(2).ToString.Trim = "LOOKUP" Or myDS.Tables(0).Rows(i).Item(2).ToString.Trim = "DATE" Or myDS.Tables(0).Rows(i).Item(2).ToString.Trim = "DATETIME" Then
                    myImageButton.Visible = True
                    myImageButton.Enabled = True
                Else
                    myImageButton.Visible = False
                End If
            End If
            If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" And (myDS.Tables(0).Rows(i).Item(2).ToString.Trim = "LOOKUP" Or myDS.Tables(0).Rows(i).Item(2).ToString.Trim = "DATE" Or myDS.Tables(0).Rows(i).Item(2).ToString.Trim = "DATETIME") Then 'Option_Editable
                myImageButton.Enabled = True
            Else
                myImageButton.Enabled = False
            End If
            myImageButton = Nothing

            If myDS.Tables(0).Rows(i).Item(4).ToString = "YES" Then 'Option_Mandatory
                myImageKey.Visible = True
            Else
                myImageKey.Visible = False
            End If
            myImageKey = Nothing

            Select Case myDS.Tables(0).Rows(i).Item(2).ToString.Trim
                Case "OPTION"
                    Dim myDDL As DropDownList = Page.FindControl("ddl" & myDS.Tables(0).Rows(i).Item(0).ToString.Trim)
                    If myDS.Tables(0).Rows(i).Item(3).ToString.Trim = "YES" Then
                        myDDL.Enabled = True
                    Else
                        myDDL.Enabled = False
                    End If
                    myDDL = Nothing
                Case Else
                    Dim myTXT As TextBox = Page.FindControl("txt" & myDS.Tables(0).Rows(i).Item(0).ToString.Trim)
                    If myDS.Tables(0).Rows(i).Item(3).ToString.Trim = "YES" Then
                        myTXT.Enabled = True
                    Else
                        myTXT.Enabled = False
                    End If
                    myTXT = Nothing
            End Select
        Next
        myDS = Nothing
    End Sub
    Sub BindJobGradeNew()
        ssql = "exec [sp_org_getOCPIDObject] 'JOB_GRADE_0_1','" & Session("Company") & "','','','','','','',''"
        myDS2 = mySQL.ExecuteSQL(ssql)
        If myDS2.Tables.Count > 0 Then
            ddlOCP_ID_JOB_GRADE_NEW.DataTextField = "Name"
            ddlOCP_ID_JOB_GRADE_NEW.DataValueField = "Code"
            ddlOCP_ID_JOB_GRADE_NEW.DataSource = myDS2
            ddlOCP_ID_JOB_GRADE_NEW.DataBind()
        End If
        myDS2 = Nothing
    End Sub

    Sub BindJobTitleNew()
        ssql = "exec [sp_org_getOCPIDObject] 'JOB_TITLE_0_1','" & Session("Company") & "','','','','','','',''"
        myDS2 = mySQL.ExecuteSQL(ssql)
        If myDS2.Tables.Count > 0 Then
            ddlOCP_ID_JOB_TITLE_NEW.DataTextField = "Name"
            ddlOCP_ID_JOB_TITLE_NEW.DataValueField = "Code"
            ddlOCP_ID_JOB_TITLE_NEW.DataSource = myDS2
            ddlOCP_ID_JOB_TITLE_NEW.DataBind()
        End If
        myDS2 = Nothing
    End Sub

    Sub BindAppraisalNew()
        ssql = "exec [sp_org_getOCPIDObject] 'APPRAISAL_0_1','" & Session("Company") & "','','','','','','',''"
        myDS2 = mySQL.ExecuteSQL(ssql)
        If myDS2.Tables.Count > 0 Then
            ddlOCP_ID_APPRAISAL_NEW.DataTextField = "Name"
            ddlOCP_ID_APPRAISAL_NEW.DataValueField = "Code"
            ddlOCP_ID_APPRAISAL_NEW.DataSource = myDS2
            ddlOCP_ID_APPRAISAL_NEW.DataBind()
        End If
        myDS2 = Nothing
    End Sub

    Sub enablefield(ByVal AcStr As String)
        If AcStr = "1" Then
            imgBtnAdd.Visible = True
            imgBtnEdit.Visible = True
            ImgBtnDelete.Visible = True

            imgbtnSubmit.Visible = False
            imgbtnCancel.Visible = False
            imgbtnUpdate.Visible = False
            imgbtnCancel2.Visible = False
            txtEFFECTIVE_DATE.Enabled = False
            imgbtnEFFECTIVE_DATE.Enabled = False
            ddlOPTION_TYPE_INCREMENT.Enabled = False
            txtOLD_SALARY.Enabled = False
            ddlOCP_ID_JOB_GRADE_NEW.Enabled = False
            ddlOCP_ID_APPRAISAL_NEW.Enabled = False
            ddlOCP_ID_JOB_TITLE_NEW.Enabled = False
            txtINCREMENT.Enabled = False
            txtPERADJ.Enabled = False
            txtPROMOTION.Enabled = False
            txtNEW_SALARY.Enabled = False
            txtTOTALADJ.Enabled = False
            txtTOTALADJPERC.Enabled = False
            txtEFFECTIVE_DATE.Text = ""
            txtOLD_SALARY.Text = "0"
            ddlOPTION_TYPE_INCREMENT.SelectedIndex = -1
            ddlOCP_ID_JOB_GRADE_NEW.SelectedIndex = -1
            ddlOCP_ID_JOB_TITLE_NEW.SelectedIndex = -1
            ddlOCP_ID_APPRAISAL_NEW.SelectedIndex = -1
            txtINCREMENT.Text = "0"
            txtPERADJ.Text = "0"
            txtPROMOTION.Text = "0"
            txtNEW_SALARY.Text = "0"
            txtTOTALADJ.Text = "0"
            txtTOTALADJPERC.Text = "0"
        ElseIf AcStr = "2" Then
            imgBtnAdd.Visible = False
            imgBtnEdit.Visible = False
            ImgBtnDelete.Visible = False

            txtOLD_SALARY.Text = txtSALARY.Text
            imgbtnSubmit.Visible = True
            imgbtnCancel.Visible = True
            imgbtnUpdate.Visible = False
            imgbtnCancel2.Visible = False
            txtEFFECTIVE_DATE.Enabled = True
            imgbtnEFFECTIVE_DATE.Enabled = True
            ddlOPTION_TYPE_INCREMENT.Enabled = True
            txtOLD_SALARY.Enabled = True
            ddlOCP_ID_JOB_GRADE_NEW.Enabled = True
            ddlOCP_ID_APPRAISAL_NEW.Enabled = True
            ddlOCP_ID_JOB_TITLE_NEW.Enabled = True
            txtINCREMENT.Enabled = True
            txtPERADJ.Enabled = True
            txtPROMOTION.Enabled = True
            txtNEW_SALARY.Enabled = False
            txtTOTALADJ.Enabled = False
            txtTOTALADJPERC.Enabled = False
        ElseIf AcStr = "3" Then
            imgBtnAdd.Visible = False
            imgBtnEdit.Visible = False
            ImgBtnDelete.Visible = False

            imgbtnSubmit.Visible = False
            imgbtnCancel.Visible = False
            imgbtnUpdate.Visible = True
            imgbtnCancel2.Visible = True
            txtEFFECTIVE_DATE.Enabled = False
            imgbtnEFFECTIVE_DATE.Enabled = False
            ddlOPTION_TYPE_INCREMENT.Enabled = True
            txtOLD_SALARY.Enabled = False
            ddlOCP_ID_JOB_GRADE_NEW.Enabled = True
            ddlOCP_ID_APPRAISAL_NEW.Enabled = True
            ddlOCP_ID_JOB_TITLE_NEW.Enabled = True
            txtINCREMENT.Enabled = True
            txtPERADJ.Enabled = True
            txtPROMOTION.Enabled = True
            txtNEW_SALARY.Enabled = False
            txtTOTALADJ.Enabled = False
            txtTOTALADJPERC.Enabled = False
        End If
    End Sub

    Protected Sub imgBtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAdd.Click
        enablefield("2")
    End Sub

    Protected Sub imgbtnCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnCancel.Click
        enablefield("1")
    End Sub

    Protected Sub imgBtnEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnEdit.Click
        enablefield("3")
        If ActionValidateEdit() = True Then
            For i = 0 To gv1.Rows.Count - 1
                Dim chkEdit As CheckBox = gv1.Rows(i).Cells(0).Controls(1)
                If chkEdit.Checked Then
                    chkEdit.Checked = False
                    txtEFFECTIVE_DATE.Text = gv1.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
                    ddlOPTION_TYPE_INCREMENT.SelectedValue = gv2.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
                    txtOLD_SALARY.Text = gv2.Rows(i).Cells(3).Text.Replace("&nbsp;", "0")
                    mySetting.ArrangeDDLSelectedIndex(ddlOCP_ID_JOB_GRADE_NEW, clsGlobalSetting.DDLSelection.SelectedValue, Replace(Replace(Server.HtmlDecode(gv2.Rows(i).Cells(11).Text.ToString.Trim), "&nbsp;", "&"), "&amp;", "&"))
                    mySetting.ArrangeDDLSelectedIndex(ddlOCP_ID_JOB_TITLE_NEW, clsGlobalSetting.DDLSelection.SelectedValue, Replace(Replace(Server.HtmlDecode(gv2.Rows(i).Cells(13).Text.ToString.Trim), "&nbsp;", "&"), "&amp;", "&"))
                    mySetting.ArrangeDDLSelectedIndex(ddlOCP_ID_APPRAISAL_NEW, clsGlobalSetting.DDLSelection.SelectedValue, Replace(Replace(Server.HtmlDecode(gv2.Rows(i).Cells(15).Text.ToString.Trim), "&nbsp;", "&"), "&amp;", "&"))
                    txtINCREMENT.Text = gv2.Rows(i).Cells(4).Text.Replace("&nbsp;", "0")
                    txtPERADJ.Text = gv2.Rows(i).Cells(5).Text.Replace("&nbsp;", "0")
                    txtPROMOTION.Text = gv2.Rows(i).Cells(6).Text.Replace("&nbsp;", "0")
                    txtNEW_SALARY.Text = gv2.Rows(i).Cells(9).Text.Replace("&nbsp;", "0")
                    txtTOTALADJ.Text = gv2.Rows(i).Cells(7).Text.Replace("&nbsp;", "0")
                    txtTOTALADJPERC.Text = gv2.Rows(i).Cells(8).Text.Replace("&nbsp;", "0")
                End If
            Next
        End If
    End Sub

    Protected Sub imgbtnCancel2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnCancel2.Click
        enablefield("1")
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

    Protected Sub imgbtnSubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSubmit.Click
        If ValidateSubmit() = True Then
            ssql1 = "select Employee_Profile_ID,Company_Profile_Code from employee_Codename_vw where codename = N'" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "'"
            Dim myds1 As DataSet, myds2 As DataSet
            Dim employee_id As String, CompanyID As String
            employee_id = "0"
            CompanyID = "0"
            myds1 = mySQL.ExecuteSQL(ssql1)
            If myds1.Tables(0).Rows.Count > 0 Then
                If myds1.Tables(0).Rows(0).Item(0).ToString > 0 Then
                    employee_id = myds1.Tables(0).Rows(0).Item(0).ToString
                    CompanyID = myds1.Tables(0).Rows(0).Item(1).ToString
                    If CompanyID = Session("Company").ToString Then
                        If CInt(employee_id) > 0 Then
                            ssql = "exec sp_pr_insupddelEmployeeIncrement "
                            ssql = ssql & " N'" & CompanyID & "',N'" & employee_id & "',"
                            ssql = ssql & "N'" & mySetting.UnDisplayDateTime(txtEFFECTIVE_DATE.Text, Session("Company").ToString, Session("Module").ToString) & "',"
                            ssql = ssql & "'" & txtOLD_SALARY.Text & "',"
                            ssql = ssql & "'" & txtINCREMENT.Text & "',"
                            ssql = ssql & "'" & txtPERADJ.Text & "',"
                            ssql = ssql & "'" & txtPROMOTION.Text & "',"
                            ssql = ssql & "'" & txtTOTALADJ.Text & "',"
                            ssql = ssql & "'" & txtTOTALADJPERC.Text & "',"
                            ssql = ssql & "'" & txtNEW_SALARY.Text & "',"
                            ssql = ssql & "'" & ddlOCP_ID_JOB_GRADE_NEW.SelectedValue & "',"
                            ssql = ssql & "'" & ddlOCP_ID_JOB_TITLE_NEW.SelectedValue & "',"
                            ssql = ssql & "'" & ddlOCP_ID_APPRAISAL_NEW.SelectedValue & "',"
                            ssql = ssql & "'" & ddlOPTION_TYPE_INCREMENT.SelectedValue & "',"
                            ssql = ssql & "N'" & Session("EmpID").ToString & "','ADD'"
                            myds2 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                            MsgBox("Submit Successfully!.")
                            enablefield("1")
                        Else
                            MsgBox("Cannot Found Employee Code.")
                        End If
                    Else
                        MsgBox("Invalid Company Employee!.")
                    End If

                Else
                    MsgBox("Cannot Found Employee Code.")
                End If
            Else
                MsgBox("Cannot Found Employee Code.")
            End If
        End If
    End Sub

    Private Sub MsgBox(ByVal msg As String)
        Dim strScript As String = "<script language=" & """" & "javascript" & """" & ">"
        strScript &= "alert('" & msg & "');"
        strScript &= "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "Error!", strScript)
    End Sub

    Public Function ValidateSubmit() As Boolean
        ValidateSubmit = False

        If txtEMPLOYEE_PROFILE_ID.Text = "" Then
            lblresult2.Text = "Cannot Be Blank! [" & lblEMPLOYEE_PROFILE_ID.Text & "] !"
            MsgBox(lblresult2.Text)
            txtEMPLOYEE_PROFILE_ID.Focus()
        ElseIf DateTime.TryParseExact(mySetting.UnDisplayDateTime(txtEFFECTIVE_DATE.Text, Session("Company").ToString, Session("Module").ToString), "yyyyMMddHHmmss", Nothing, DateTimeStyles.None, Nothing) = False And txtEFFECTIVE_DATE.Text <> "" Then
            lblresult2.Text = "Invalid Input Date Format For [" & lblEFFECTIVE_DATE.Text & "] !"
            MsgBox(lblresult2.Text)
            txtEFFECTIVE_DATE.Focus()
            Exit Function
        ElseIf ddlOPTION_TYPE_INCREMENT.SelectedValue = "" Then
            lblresult2.Text = "Cannot Be Blank! [" & lblOPTION_TYPE_INCREMENT.Text & "] !"
            MsgBox(lblresult2.Text)
            ddlOPTION_TYPE_INCREMENT.Focus()
            Exit Function
        ElseIf txtINCREMENT.Text = "" And txtPERADJ.Text = "" And txtPROMOTION.Text = "" Then
            lblresult2.Text = "Cannot Be Blank! [" & lblINCREMENT.Text & "] !"
            MsgBox(lblresult2.Text)
            txtINCREMENT.Focus()
        End If

        ValidateSubmit = True
    End Function

    Protected Sub imgbtnUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnUpdate.Click
        If ValidateSubmit() = True Then
            ssql1 = "select Employee_Profile_ID,Company_Profile_Code from employee_Codename_vw where codename = N'" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "'"
            Dim myds1 As DataSet, myds2 As DataSet
            Dim employee_id As String, CompanyID As String
            employee_id = "0"
            CompanyID = "0"
            myds1 = mySQL.ExecuteSQL(ssql1)
            If myds1.Tables(0).Rows.Count > 0 Then
                If myds1.Tables(0).Rows(0).Item(0).ToString > 0 Then
                    employee_id = myds1.Tables(0).Rows(0).Item(0).ToString
                    CompanyID = myds1.Tables(0).Rows(0).Item(1).ToString
                    If CompanyID = Session("Company").ToString Then
                        If CInt(employee_id) > 0 Then
                            ssql = "exec sp_pr_insupddelEmployeeIncrement "
                            ssql = ssql & " N'" & CompanyID & "',N'" & employee_id & "',"
                            ssql = ssql & "N'" & mySetting.UnDisplayDateTime(txtEFFECTIVE_DATE.Text, Session("Company").ToString, Session("Module").ToString) & "',"
                            ssql = ssql & "'" & txtOLD_SALARY.Text & "',"
                            ssql = ssql & "'" & txtINCREMENT.Text & "',"
                            ssql = ssql & "'" & txtPERADJ.Text & "',"
                            ssql = ssql & "'" & txtPROMOTION.Text & "',"
                            ssql = ssql & "'" & txtTOTALADJ.Text & "',"
                            ssql = ssql & "'" & txtTOTALADJPERC.Text & "',"
                            ssql = ssql & "'" & txtNEW_SALARY.Text & "',"
                            ssql = ssql & "'" & ddlOCP_ID_JOB_GRADE_NEW.SelectedValue & "',"
                            ssql = ssql & "'" & ddlOCP_ID_JOB_TITLE_NEW.SelectedValue & "',"
                            ssql = ssql & "'" & ddlOCP_ID_APPRAISAL_NEW.SelectedValue & "',"
                            ssql = ssql & "'" & ddlOPTION_TYPE_INCREMENT.SelectedValue & "',"
                            ssql = ssql & "N'" & Session("EmpID").ToString & "','ADD'"
                            myds2 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                            MsgBox("Update Successfully!.")
                            enablefield("1")
                        Else
                            MsgBox("Cannot Found Employee Code.")
                        End If
                    Else
                        MsgBox("Invalid Company Employee!.")
                    End If

                Else
                    MsgBox("Cannot Found Employee Code.")
                End If
            Else
                MsgBox("Cannot Found Employee Code.")
            End If
        End If
    End Sub

    Protected Sub ImgBtnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnDelete.Click
        If ActionValidateDelete() = True Then
            For i = 0 To gv1.Rows.Count - 1
                Dim chkEdit As CheckBox = gv1.Rows(i).Cells(0).Controls(1)
                If chkEdit.Checked Then
                    chkEdit.Checked = False
                    ssql1 = "select Employee_Profile_ID,Company_Profile_Code from employee_Codename_vw where codename = N'" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "'"
                    Dim myds1 As DataSet, myds2 As DataSet
                    Dim employee_id As String, CompanyID As String
                    employee_id = "0"
                    CompanyID = "0"
                    myds1 = mySQL.ExecuteSQL(ssql1)
                    If myds1.Tables(0).Rows.Count > 0 Then
                        If myds1.Tables(0).Rows(0).Item(0).ToString > 0 Then
                            employee_id = myds1.Tables(0).Rows(0).Item(0).ToString
                            CompanyID = myds1.Tables(0).Rows(0).Item(1).ToString
                            If CompanyID = Session("Company").ToString Then
                                If CInt(employee_id) > 0 Then
                                    ssql = "exec sp_pr_insupddelEmployeeIncrement "
                                    ssql = ssql & " N'" & CompanyID & "',N'" & employee_id & "',"
                                    ssql = ssql & "N'" & gv2.Rows(i).Cells(1).Text.Replace("&nbsp;", "") & "',"
                                    ssql = ssql & "'" & txtOLD_SALARY.Text & "',"
                                    ssql = ssql & "'" & txtINCREMENT.Text & "',"
                                    ssql = ssql & "'" & txtPERADJ.Text & "',"
                                    ssql = ssql & "'" & txtPROMOTION.Text & "',"
                                    ssql = ssql & "'" & txtTOTALADJ.Text & "',"
                                    ssql = ssql & "'" & txtTOTALADJPERC.Text & "',"
                                    ssql = ssql & "'" & txtNEW_SALARY.Text & "',"
                                    ssql = ssql & "'" & ddlOCP_ID_JOB_GRADE_NEW.SelectedValue & "',"
                                    ssql = ssql & "'" & ddlOCP_ID_JOB_TITLE_NEW.SelectedValue & "',"
                                    ssql = ssql & "'" & ddlOCP_ID_APPRAISAL_NEW.SelectedValue & "',"
                                    ssql = ssql & "'" & ddlOPTION_TYPE_INCREMENT.SelectedValue & "',"
                                    ssql = ssql & "N'" & Session("EmpID").ToString & "','DEL'"
                                    myds2 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                                Else
                                    MsgBox("Cannot Found Employee Code.")
                                End If
                            Else
                                MsgBox("Invalid Company Employee!.")
                            End If

                        Else
                            MsgBox("Cannot Found Employee Code.")
                        End If
                    Else
                        MsgBox("Cannot Found Employee Code.")
                    End If
                End If
            Next
        End If
        imgBtnPrint_Click(Nothing, Nothing)
    End Sub


    Private Function ActionValidateDelete() As Boolean
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
            Return True
        Else
            ShowMessage("No row selected for deleting. Please select at least one row!")
            Return False
        End If
    End Function

    Protected Sub txtINCREMENT_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtINCREMENT.TextChanged
        totalIncrement()
    End Sub

    Private Sub totalIncrement()

        txtTOTALADJ.Text = CDec(txtINCREMENT.Text) + CDec(txtPROMOTION.Text) + CDec(txtPERADJ.Text)
        txtNEW_SALARY.Text = CDec(txtOLD_SALARY.Text) + CDec(txtTOTALADJ.Text)
        txtTOTALADJPERC.Text = Math.Round(CDec(txtTOTALADJ.Text) / CDec(txtOLD_SALARY.Text), 2)
    End Sub

    Protected Sub txtPERADJ_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPERADJ.TextChanged
        totalIncrement()
    End Sub

    Protected Sub txtPROMOTION_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPROMOTION.TextChanged
        totalIncrement()
    End Sub
End Class

