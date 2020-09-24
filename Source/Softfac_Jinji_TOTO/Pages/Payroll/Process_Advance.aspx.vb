Imports System.Data

Partial Class Pages_Payroll_Process_Advance
    Inherits System.Web.UI.Page
    Private WithEvents myDS As New DataSet, myDS2 As New DataSet, mySetting As New clsGlobalSetting, mySQL As New clsSQL
    Dim btnColourDef, btnColourAlt As String
    Dim strPath As String = "../../Images"
    Dim logic As Boolean
    Dim SearchByPage As Boolean = False
    Dim SearchCriteria As Boolean = False
    Dim ssql As String, ssql2 As String, ssql3 As String, i As Integer
    Public _currentPageNumber As Integer, _totalPage As Double = 1, _totalRecords As Integer

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
            pnlGridview.Width = CInt(Session("GVwidth")) - 20
            'pnlGridview.Height = CInt(Session("GVheight"))

            PagePreload()
        End If
    End Sub
    Private Sub PagePreload()
        Session("Module") = "PAYROLL"
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")

        _currentPageNumber = 1
        lblYEAR_MONTH.Text = "Year / Month"
        lblYEAR_MONTH.CssClass = "wordstyle3"
        lblPAY_CYCLE.Text = "Pay Cycle"
        lblPAY_CYCLE.CssClass = "wordstyle3"
        lblFILTER_BY.Text = "Filter By"
        lblFILTER_BY.CssClass = "wordstyle3"
        lblOCP_ID_Salary.Text = "Advance Code"
        lblOCP_ID_Salary.CssClass = "wordstyle3"
        lblJoin_Date.Text = "Join Date"
        lblJoin_Date.CssClass = "wordstyle3"
        lblResign_Date.Text = "Resign Date"
        lblResign_Date.CssClass = "wordstyle3"
        lblCATEGORY_LEVEL.Text = "Category Level"
        lblCATEGORY_LEVEL.CssClass = "wordstyle3"

        lblEMPLOYEE_PROFILE_ID.Text = "Employee Profile ID"
        lblEMPLOYEE_PROFILE_ID.CssClass = "wordstyle3"
        lblOCP_ID_DIVISON.Text = "Division"
        lblOCP_ID_DIVISON.CssClass = "wordstyle3"
        lblOCP_ID_DEPARTMENT.Text = "Department"
        lblOCP_ID_DEPARTMENT.CssClass = "wordstyle3"
        lblOCP_ID_SECTION.Text = "Section"
        lblOCP_ID_SECTION.CssClass = "wordstyle3"
        lblOCP_ID_TMS.Text = "TMS"
        lblOCP_ID_TMS.CssClass = "wordstyle3"
        lblOCP_ID_JOB_GRADE.Text = "Job Grade"
        lblOCP_ID_JOB_GRADE.CssClass = "wordstyle3"
        lblOCP_ID_JOB_TITLE.Text = "Job Title"
        lblOCP_ID_JOB_TITLE.CssClass = "wordstyle3"
        lblOPTION_PAY_TYPE.Text = "Pay Type"
        lblOPTION_PAY_TYPE.CssClass = "wordstyle3"
        lblOPTION_RACE.Text = "Race"
        lblOPTION_RACE.CssClass = "wordstyle3"
        lblOPTION_RELIGION.Text = "Religion"
        lblOPTION_RELIGION.CssClass = "wordstyle3"
        lblPay_Type.Text = "By Type"
        lblPay_Type.CssClass = "wordstyle3"
        lblAmount.Text = "Amount"
        lblAmount.CssClass = "wordstyle3"
        lblMethod.Text = "Calc Method"
        lblMethod.CssClass = "wordstyle3"

        pnlMETHOD.Visible = False

        imgbtnOPTION_PAY_TYPE.Visible = False
        pnlGridview.Visible = False
        tblCheck.Visible = False
        'imgkeyJoin_Date.Visible = False
        'imgkeyResign_Date.Visible = False
        SetInvisibleAll()
        txtYEAR.Text = DatePart(DateInterval.Year, Now())
        Dim ddlMonthssql As String, myDS5 As DataSet

        ddlMonthssql = "Create Table #Result([Code] nvarchar(50), [Name] nvarchar(255)); "
        ddlMonthssql = ddlMonthssql + "Insert Into #Result Select '01','JANUARY';Insert Into #Result Select '02','FEBRUARY';Insert Into #Result Select '03','MARCH';"
        ddlMonthssql = ddlMonthssql + "Insert Into #Result Select '04','APRIL';Insert Into #Result Select '05','MAY';Insert Into #Result Select '06','JUNE';"
        ddlMonthssql = ddlMonthssql + "Insert Into #Result Select '07','JULY';Insert Into #Result Select '08','AUGUST';Insert Into #Result Select '09','SEPTEMBER';"
        ddlMonthssql = ddlMonthssql + "Insert Into #Result Select '10','OCTOBER';Insert Into #Result Select '11','NOVEMBER';Insert Into #Result Select '12','DECEMBER';"
        ddlMonthssql = ddlMonthssql + " Select * From #Result; Drop Table #Result"
        myDS5 = mySQL.ExecuteSQL(ddlMonthssql)
        If myDS5.Tables.Count > 0 Then
            If myDS5.Tables(0).Columns.Count > 1 Then
                ddlMONTH.DataTextField = "Name"
                ddlMONTH.DataValueField = "Code"
                ddlMONTH.DataSource = myDS5
                ddlMONTH.DataBind()
            End If
        End If
        myDS5 = Nothing
        ddlMonthssql = DatePart(DateInterval.Month, Now())
        If Len(ddlMonthssql) = 1 Then
            ddlMonthssql = "0" + ddlMonthssql
        End If
        ddlMONTH.SelectedValue = ddlMonthssql
        Dim ddlcategoryssql As String
        ddlcategoryssql = "Create Table #Result([Code] nvarchar(50), [Name] nvarchar(255)); "
        ddlcategoryssql = ddlcategoryssql + "select distinct Code, Name from organisation_Code_Profile_vw where Option_Type = 'CATEGORY_GROUP' order by code"
        myDS5 = mySQL.ExecuteSQL(ddlcategoryssql)
        If myDS5.Tables.Count > 0 Then
            If myDS5.Tables(0).Columns.Count > 1 Then
                ddlCATEGORY_LEVEL.DataTextField = "Name"
                ddlCATEGORY_LEVEL.DataValueField = "Code"
                ddlCATEGORY_LEVEL.DataSource = myDS5
                ddlCATEGORY_LEVEL.DataBind()
            End If
        End If
        myDS5 = Nothing

        mySetting.GetBtnImgUrl(imgbtnFILTER, Session("Company").ToString, btnColourDef, "btnFilter.png")
        mySetting.GetBtnImgUrl(imgbtnPROCESS, Session("Company").ToString, btnColourDef, "btnProcess.png")
        mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourDef, "btnSearch.png")
        mySetting.GetBtnImgUrl(imgBtnClear, Session("Company").ToString, btnColourDef, "btnClear.png")
        mySetting.GetBtnImgUrl(imgBtnCancel, Session("Company").ToString, btnColourDef, "btnCancel.png")
        mySetting.GetBtnImgUrl(imgBtnGoToPage, Session("Company").ToString, btnColourDef, "btngo.png")
        mySetting.SetTextBoxPressEnterGoToImageButton(txtGoToPage, imgBtnGoToPage)

        mySetting.GetImgUrl(imgKeyYEAR_MONTH, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyPAY_CYCLE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyFILTER_BY, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgkeyPay_Type, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgkeyAmount, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgkeyMethod, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgkeyJoin_Date, clsGlobalSetting.ImageType._BLANK, Session("Company").ToString)
        mySetting.GetImgUrl(imgkeyResign_Date, clsGlobalSetting.ImageType._BLANK, Session("Company").ToString)

        mySetting.GetImgUrl(imgKeyEMPLOYEE_PROFILE_ID, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyOCP_ID_Salary, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyOCP_ID_DIVISION, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyOCP_ID_DEPARTMENT, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyOCP_ID_SECTION, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyOCP_ID_TMS, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyOCP_ID_JOB_GRADE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyOCP_ID_JOB_TITLE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyOPTION_PAY_TYPE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyOption_Race, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)

        mySetting.GetImgUrl(imgbtnEMPLOYEE_PROFILE_ID, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgBtnOCP_ID_Salary, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnOCP_ID_DIVISION, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnOCP_ID_DEPARTMENT, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnOCP_ID_SECTION, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnOCP_ID_TMS, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnOCP_ID_JOB_GRADE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnOCP_ID_JOB_TITLE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnOPTION_PAY_TYPE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnJoin_Date , clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnResign_Date, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)

        mySetting.GetBtnImgUrl(imgbtnSelectAll, Session("Company").ToString, btnColourDef, "addall.png")
        mySetting.GetBtnImgUrl(imgbtnSelectOne, Session("Company").ToString, btnColourDef, "additem.png")
        mySetting.GetBtnImgUrl(imgbtnRemoveAll, Session("Company").ToString, btnColourDef, "removeall.png")
        mySetting.GetBtnImgUrl(imgbtnRemoveOne, Session("Company").ToString, btnColourDef, "removeitem.png")

        mySetting.GetImgTypeUrl(imgTop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Filter.png")
        mySetting.GetImgTypeUrl(imgBottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")

        body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

        mySetting.GetDropdownlistValue(Form.ID, "OPTION_PAY_CYCLE", ddlPAY_CYCLE)
        If ddlPAY_CYCLE.Items.Count >= 2 Then
            ddlPAY_CYCLE.SelectedIndex = 1
        End If
        mySetting.GetDropdownlistValue(Form.ID, "OPTION_FILTER_BY", ddlFILTER_BY)
        If ddlFILTER_BY.Items.Count >= 2 Then
            ddlFILTER_BY.SelectedIndex = 0
        End If
        mySetting.GetDropdownlistValue(Form.ID, "OPTION_PAY_TYPE", ddlOPTION_PAY_TYPE)
        mySetting.GetDropdownlistValue("Employee_Profile", "OPTION_RACE", ddlOPTION_RACE)
        mySetting.GetDropdownlistValue("Employee_Profile", "OPTION_RELIGION", ddlOPTION_RELIGION)
        mySetting.GetDropdownlistValue(Form.ID, "PAY_TYPE", ddlPay_Type)
        mySetting.GetDropdownlistValue(Form.ID, "PAY_METHOD", ddlMethod)

        mySetting.GetLookupValue_ImageButton(imgbtnEMPLOYEE_PROFILE_ID, Form.ID, "txtEMPLOYEE_PROFILE_ID", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "EMPLOYEE_PROFILE_ID" & """," & """" & """," & """" & Session("EmpID").ToString & """")
        mySetting.GetLookupValue_ImageButton(imgBtnOCP_ID_Salary, Form.ID, "txtOCP_ID_Salary", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_SALARY" & """," & """" & """," & """" & Session("EmpID").ToString & """")
        mySetting.GetLookupValue_ImageButton(imgbtnOCP_ID_DIVISION, Form.ID, "txtOCP_ID_DIVISION", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_DIVISION" & """," & """" & """," & """" & Session("EmpID").ToString & """")
        mySetting.GetLookupValue_ImageButton(imgbtnOCP_ID_DEPARTMENT, Form.ID, "txtOCP_ID_DEPARTMENT", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_DEPARTMENT" & """," & """" & """," & """" & Session("EmpID").ToString & """")
        mySetting.GetLookupValue_ImageButton(imgbtnOCP_ID_SECTION, Form.ID, "OCP_ID_SECTION", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_SECTION" & """," & """" & """," & """" & Session("EmpID").ToString & """")
        mySetting.GetLookupValue_ImageButton(imgbtnOCP_ID_TMS, Form.ID, "txtOCP_ID_TMS", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_TMS" & """," & """" & """," & """" & Session("EmpID").ToString & """")
        mySetting.GetLookupValue_ImageButton(imgbtnOCP_ID_JOB_GRADE, Form.ID, "txtOCP_ID_JOB_GRADE", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_JOB_GRADE" & """," & """" & """," & """" & Session("EmpID").ToString & """")
        mySetting.GetLookupValue_ImageButton(imgbtnOCP_ID_JOB_TITLE, Form.ID, "txtOCP_ID_JOB_TITLE", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_JOB_TITLE" & """," & """" & """," & """" & Session("EmpID").ToString & """")
        mySetting.PopUpCalendar_ImageButton(imgbtnJoin_Date, Form.ID, "txtJoin_Date")
        mySetting.PopUpCalendar_ImageButton(imgbtnResign_Date, Form.ID, "txtResign_Date")
        'get Page Title
        ssql = "exec sp_sa_GetPageTitle '" & Form.ID & "'"
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            lblTitle.Text = myDS.Tables(0).Rows(0).Item(0)
            lblTitle.CssClass = "wordstyle4"
        End If
        myDS = Nothing
        pnlButtonPayroll.Visible = False

        SetInvisibleEmployeeProfileID()
        'imgBtnSearch_Click(Nothing, Nothing)
    End Sub

    Protected Sub imgbtnFILTER_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnFILTER.Click
        lblTitle.Visible = True
        pnlProgress.Visible = False
        tblEditButton.Visible = False
        tblButton.Visible = True
        pnlGridview.Visible = True
        SetVisibleAll()
        ClearAll()
    End Sub

    Protected Sub imgBtnCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnCancel.Click
        tblEditButton.Visible = True
        tblButton.Visible = False
        SetInvisibleAll()
    End Sub

    Protected Sub imgBtnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSearch.Click
        pnlGridview.Visible = True
        BindGrid()
        'If SearchCriteria = False Then
        '    ShowMessage("No searching criteria was specified!")
        '    ddlOPTION_PAY_TYPE.Focus()
        '    Exit Sub
        'End If
        tblEditButton.Visible = True
        tblButton.Visible = False
        SetInvisibleAll()
        tblCheck.Visible = True
        'chkSelectAll.Checked = True
        'For i As Integer = 0 To myGridView.Rows.Count - 1
        '    Dim chkBox As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
        '    chkBox.Checked = True
        'Next
        chkClearAll.Checked = False
    End Sub
    Private Sub BindGrid()
        Try

            ssql = ""
            ssql2 = ""
            ssql3 = ""
            If ddlPAY_CYCLE.Visible = True Then
                If ddlPAY_CYCLE.SelectedValue <> "" Then
                    SearchCriteria = True
                    ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                    ssql2 = ssql2 & "OPTION_PAY_CYCLE" & "@@"
                    If Right(Trim(ddlPAY_CYCLE.SelectedValue.ToString), 5) = "CYCLE" Then
                        ssql3 = ssql3 & Right(Trim(ddlPAY_CYCLE.SelectedValue.ToString), 5) & "@@"
                    Else
                        ssql3 = ssql3 & Trim(ddlPAY_CYCLE.SelectedValue.ToString) & "@@"
                    End If
                End If
            End If

            If ddlCATEGORY_LEVEL.Visible = True Then
                If ddlCATEGORY_LEVEL.SelectedValue <> "" Then
                    SearchCriteria = True
                    ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                    ssql2 = ssql2 & "OCP_ID_CATEGORY_GROUP" & "@@"
                    If Right(Trim(ddlCATEGORY_LEVEL.SelectedValue.ToString), 5) = "CYCLE" Then
                        ssql3 = ssql3 & Right(Trim(ddlCATEGORY_LEVEL.SelectedValue.ToString), 5) & "@@"
                    Else
                        ssql3 = ssql3 & Trim(ddlCATEGORY_LEVEL.SelectedValue.ToString) & "@@"
                    End If
                End If
            End If

            If txtEMPLOYEE_PROFILE_ID.Visible = True Then
                If txtEMPLOYEE_PROFILE_ID.Text.ToString.Trim <> "" Then
                    SearchCriteria = True
                    ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                    ssql2 = ssql2 & "EMPLOYEE_PROFILE_ID" & "@@"
                    ssql3 = ssql3 & Trim(txtEMPLOYEE_PROFILE_ID.Text.ToString) & "@@"
                End If
            End If

            If ddlOPTION_PAY_TYPE.Visible = True Then
                If ddlOPTION_PAY_TYPE.SelectedValue <> "" Then
                    SearchCriteria = True
                    ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                    ssql2 = ssql2 & "OPTION_PAY_TYPE" & "@@"
                    ssql3 = ssql3 & Trim(ddlOPTION_PAY_TYPE.SelectedValue.ToString) & "@@"
                End If
            End If

            If ddlOPTION_PAY_TYPE.Visible = True Then
                If ddlOPTION_PAY_TYPE.SelectedValue <> "" Then
                    SearchCriteria = True
                    ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                    ssql2 = ssql2 & "OPTION_PAY_TYPE" & "@@"
                    ssql3 = ssql3 & Trim(ddlOPTION_PAY_TYPE.SelectedValue.ToString) & "@@"
                End If
            End If

            If ddlOPTION_RELIGION.Visible = True Then
                If ddlOPTION_RELIGION.SelectedValue <> "" Then
                    SearchCriteria = True
                    ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                    ssql2 = ssql2 & "OPTION_RELIGION" & "@@"
                    ssql3 = ssql3 & Trim(ddlOPTION_RELIGION.SelectedValue.ToString) & "@@"
                End If
            End If

            If ddlOPTION_RACE.Visible = True Then
                If ddlOPTION_RACE.SelectedValue <> "" Then
                    SearchCriteria = True
                    ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                    ssql2 = ssql2 & "OPTION_RACE" & "@@"
                    ssql3 = ssql3 & Trim(ddlOPTION_RACE.SelectedValue.ToString) & "@@"
                End If
            End If

            If ddlFILTER_BY.Visible = True Then
                If ddlFILTER_BY.SelectedValue = "R" Then
                    SearchCriteria = True
                    ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                    ssql2 = ssql2 & "OPTION_RESIGN_FLAG" & "@@"
                    ssql3 = ssql3 & Trim(ddlFILTER_BY.SelectedValue.ToString) & "@@"
                End If
            End If

            If txtOCP_ID_DIVISION.Visible = True Then
                If txtOCP_ID_DIVISION.Text.ToString.Trim <> "" Then
                    SearchCriteria = True
                    ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                    ssql2 = ssql2 & "OCP_ID_DIVISION" & "@@"
                    ssql3 = ssql3 & txtOCP_ID_DIVISION.Text.ToString.Trim & "@@"
                End If
            End If

            If txtOCP_ID_DEPARTMENT.Visible = True Then
                If txtOCP_ID_DEPARTMENT.Text.ToString.Trim <> "" Then
                    SearchCriteria = True
                    ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                    ssql2 = ssql2 & "OCP_ID_DEPARTMENT" & "@@"
                    ssql3 = ssql3 & txtOCP_ID_DEPARTMENT.Text.ToString.Trim & "@@"
                End If
            End If

            If txtOCP_ID_SECTION.Visible = True Then
                If txtOCP_ID_SECTION.Text.ToString.Trim <> "" Then
                    SearchCriteria = True
                    ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                    ssql2 = ssql2 & "OCP_ID_SECTION" & "@@"
                    ssql3 = ssql3 & txtOCP_ID_SECTION.Text.ToString.Trim & "@@"
                End If
            End If

            If txtOCP_ID_TMS.Visible = True Then
                If txtOCP_ID_TMS.Text.ToString.Trim <> "" Then
                    SearchCriteria = True
                    ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                    ssql2 = ssql2 & "OCP_ID_TMS" & "@@"
                    ssql3 = ssql3 & txtOCP_ID_TMS.Text.ToString.Trim & "@@"
                End If
            End If

            If txtOCP_ID_JOB_GRADE.Visible = True Then
                If txtOCP_ID_JOB_GRADE.Text.ToString.Trim <> "" Then
                    SearchCriteria = True
                    ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                    ssql2 = ssql2 & "OCP_ID_JOB_GRADE" & "@@"
                    ssql3 = ssql3 & txtOCP_ID_JOB_GRADE.Text.ToString.Trim & "@@"
                End If
            End If

            If txtOCP_ID_JOB_TITLE.Visible = True Then
                If txtOCP_ID_JOB_TITLE.Text.ToString.Trim <> "" Then
                    ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                    ssql2 = ssql2 & "OCP_ID_JOB_TITLE" & "@@"
                    ssql3 = ssql3 & txtOCP_ID_JOB_TITLE.Text.ToString.Trim & "@@"
                End If
            End If

            If Len(ssql) > 2 Then
                If Right(ssql, 2) = "@@" Then
                    ssql = Left(ssql, Len(ssql) - 2)
                End If
            End If
            If Len(ssql2) > 2 Then
                If Right(ssql2, 2) = "@@" Then
                    ssql2 = Left(ssql2, Len(ssql2) - 2)
                End If
            End If
            If Len(ssql3) > 2 Then
                If Right(ssql3, 2) = "@@" Then
                    ssql3 = Left(ssql3, Len(ssql3) - 2)
                End If
            End If
            If SearchCriteria = False Then
                ssql = "Exec sp_pr_GetPayrollMonth N'" & Session("Company").ToString & "',N'" & Session("EmpID").ToString & "',N'" & _
                                           txtYEAR.Text & "',N'" & ddlMONTH.SelectedValue & "',N'" & ddlPAY_CYCLE.SelectedValue & "',N'OPTION_PAY_CYCLE',N'" & ddlPAY_CYCLE.SelectedValue & "'"
            Else
                ssql = "Exec sp_pr_GetPayrollMonth N'" & Session("Company").ToString & "',N'" & Session("EmpID").ToString & "',N'" & _
                                           txtYEAR.Text & "',N'" & ddlMONTH.SelectedValue & "',N'" & ddlPAY_CYCLE.SelectedValue & "',N'" & ssql2 & "',N'" & ssql3 & "'"
            End If
            myDS = New DataSet
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If Not myDS Is Nothing Then
                myGridView.DataSource = myDS.Tables(1).DefaultView
                myGridView.DataBind()

                If ((myGridView.Rows.Count + 2) * 26) < CInt(Session("GVheight")) Then
                    'pnlGridview.Height = (CInt(Left(myGridView.Height.ToString, Len(myGridView.Height.ToString) - 2)) + 30)
                    pnlGridview.Height = ((myGridView.Rows.Count + 2) * 26)
                Else
                    pnlGridview.Height = CInt(Session("GVheight"))
                End If

                If myDS.Tables(1).Rows.Count > 0 Then
                    pnlButtonPayroll.Visible = True
                Else
                    pnlButtonPayroll.Visible = False
                End If
                chkSelectAll.Visible = True
                chkClearAll.Visible = True
            Else
                pnlProgress.Visible = False
                pnlGridview.Visible = False
                chkSelectAll.Visible = False
                chkClearAll.Visible = False
                ShowMessage("No " & ddlFILTER_BY.SelectedItem.Text.ToString & " was founded!")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlFILTER_BY_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFILTER_BY.SelectedIndexChanged

        If ddlFILTER_BY.SelectedValue.ToString.ToUpper = "E" Then
            SetVisibleEmployeeProfileID()
            pnlProgress.Visible = False
            pnlGridview.Visible = False
            chkSelectAll.Visible = False
            chkClearAll.Visible = False
        ElseIf ddlFILTER_BY.SelectedValue.ToString.ToUpper = "" Then
            SetInvisibleEmployeeProfileID()
            pnlProgress.Visible = False
            pnlGridview.Visible = False
            chkSelectAll.Visible = False
            chkClearAll.Visible = False
        Else
            SetInvisibleEmployeeProfileID()
            pnlProgress.Visible = False
            imgBtnSearch_Click(Nothing, Nothing)
        End If

    End Sub
    Private Sub BindListBox(ByVal lstBox As ListBox)
        mySetting.BindListBox(lstBox, "Exec sp_pr_ProcessPayroll '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','" & ddlFILTER_BY.SelectedItem.Value.ToString & "','','FILTER'", 0, 2)
    End Sub
    Private Sub ClearListBox(ByVal lstBox As ListBox)
        lstBox.Items.Clear()
    End Sub
    Protected Sub imgbtnSelectOne_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSelectOne.Click
        mySetting.AddDeleteListBoxItem(lstLeft, lstRight)
    End Sub

    Protected Sub imgbtnSelectAll_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSelectAll.Click
        mySetting.AddDeleteAllListBoxItem(lstLeft, lstRight)
    End Sub

    Protected Sub imgbtnRemoveOne_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnRemoveOne.Click
        mySetting.AddDeleteListBoxItem(lstRight, lstLeft)
    End Sub

    Protected Sub imgbtnRemoveAll_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnRemoveAll.Click
        mySetting.AddDeleteAllListBoxItem(lstRight, lstLeft)
    End Sub
    Private Sub ClearAll()
        If ddlOPTION_PAY_TYPE.Items.Count > 1 Then
            ddlOPTION_PAY_TYPE.SelectedIndex = 0
        End If
        If ddlOption_Race.Items.Count > 1 Then
            ddlOption_Race.SelectedIndex = 0
        End If
        txtOCP_ID_DIVISION.Text = ""
        txtOCP_ID_DEPARTMENT.Text = ""
        txtOCP_ID_JOB_GRADE.Text = ""
        txtOCP_ID_JOB_TITLE.Text = ""
        txtOCP_ID_SECTION.Text = ""
        txtOCP_ID_TMS.Text = ""
    End Sub
    Private Sub SetVisibleAll()
        imgKeyOPTION_PAY_TYPE.Visible = True
        imgKeyOPTION_RACE.Visible = True
        imgKeyOPTION_RELIGION.Visible = True
        imgKeyOCP_ID_DIVISION.Visible = True
        imgKeyOCP_ID_DEPARTMENT.Visible = True
        imgKeyOCP_ID_SECTION.Visible = True
        imgKeyOCP_ID_TMS.Visible = True
        imgKeyOCP_ID_JOB_GRADE.Visible = True
        imgKeyOCP_ID_JOB_TITLE.Visible = True

        lblOPTION_PAY_TYPE.Visible = True
        lblOPTION_RACE.Visible = True
        lblOPTION_RELIGION.Visible = True
        lblOCP_ID_DIVISON.Visible = True
        lblOCP_ID_DEPARTMENT.Visible = True
        lblOCP_ID_SECTION.Visible = True
        lblOCP_ID_TMS.Visible = True
        lblOCP_ID_JOB_GRADE.Visible = True
        lblOCP_ID_JOB_TITLE.Visible = True

        ddlOPTION_PAY_TYPE.Visible = True
        ddlOPTION_RACE.Visible = True
        ddlOPTION_RELIGION.Visible = True
        txtOCP_ID_DIVISION.Visible = True
        txtOCP_ID_DEPARTMENT.Visible = True
        txtOCP_ID_SECTION.Visible = True
        txtOCP_ID_TMS.Visible = True
        txtOCP_ID_JOB_GRADE.Visible = True
        txtOCP_ID_JOB_TITLE.Visible = True

        imgbtnOCP_ID_DIVISION.Visible = True
        imgbtnOCP_ID_DEPARTMENT.Visible = True
        imgbtnOCP_ID_SECTION.Visible = True
        imgbtnOCP_ID_TMS.Visible = True
        imgbtnOCP_ID_JOB_GRADE.Visible = True
        imgbtnOCP_ID_JOB_TITLE.Visible = True
    End Sub
    Private Sub SetInvisibleAll()
        imgKeyOPTION_PAY_TYPE.Visible = False
        imgKeyOCP_ID_DIVISION.Visible = False
        imgKeyOCP_ID_DEPARTMENT.Visible = False
        imgKeyOCP_ID_SECTION.Visible = False
        imgKeyOCP_ID_TMS.Visible = False
        imgKeyOCP_ID_JOB_GRADE.Visible = False
        imgKeyOCP_ID_JOB_TITLE.Visible = False
        imgKeyOPTION_RACE.Visible = False
        imgKeyOPTION_RELIGION.Visible = False

        lblOPTION_PAY_TYPE.Visible = False
        lblOCP_ID_DIVISON.Visible = False
        lblOCP_ID_DEPARTMENT.Visible = False
        lblOCP_ID_SECTION.Visible = False
        lblOCP_ID_TMS.Visible = False
        lblOCP_ID_JOB_GRADE.Visible = False
        lblOCP_ID_JOB_TITLE.Visible = False
        lblOPTION_RACE.Visible = False
        lblOPTION_RELIGION.Visible = False

        ddlOPTION_PAY_TYPE.Visible = False
        ddlOPTION_RACE.Visible = False
        ddlOPTION_RELIGION.Visible = False
        txtOCP_ID_DIVISION.Visible = False
        txtOCP_ID_DEPARTMENT.Visible = False
        txtOCP_ID_SECTION.Visible = False
        txtOCP_ID_TMS.Visible = False
        txtOCP_ID_JOB_GRADE.Visible = False
        txtOCP_ID_JOB_TITLE.Visible = False

        imgbtnOCP_ID_DIVISION.Visible = False
        imgbtnOCP_ID_DEPARTMENT.Visible = False
        imgbtnOCP_ID_SECTION.Visible = False
        imgbtnOCP_ID_TMS.Visible = False
        imgbtnOCP_ID_JOB_GRADE.Visible = False
        imgbtnOCP_ID_JOB_TITLE.Visible = False
    End Sub

    Protected Sub imgBtnClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnClear.Click
        ddlOPTION_PAY_TYPE.SelectedIndex = 0
        ddlOPTION_RACE.SelectedIndex = 0
        ddlOPTION_RELIGION.SelectedIndex = 0
        txtOCP_ID_DIVISION.Text = ""
        txtOCP_ID_DEPARTMENT.Text = ""
        txtOCP_ID_SECTION.Text = ""
        txtOCP_ID_TMS.Text = ""
        txtOCP_ID_JOB_GRADE.Text = ""
        txtOCP_ID_JOB_TITLE.Text = ""
        txtEMPLOYEE_PROFILE_ID.Text = ""
    End Sub

    Protected Sub imgbtnPROCESS_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnPROCESS.Click
        If ValidateProcess() = True Then
            Dim SPID As String = mySQL.GetSPID
            Dim total As Integer, empid As String, salaryID As String

            total = 0
            Session("SPID") = SPID

            If pnlGridview.Visible = True Then
                For i = 0 To myGridView.Rows.Count - 1
                    Dim chkBox As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
                    If chkBox.Checked = True Then
                        myGridView.Rows(i).Cells(1).Text = "Calculating...."
                        empid = ""
                        salaryID = ""
                        ssql2 = "select dbo.fn_ReturnEmpIDByCodeName(N'" & Session("Company").ToString & "',N'" & myGridView.Rows(i).Cells(2).Text.ToString.Replace("&amp;", "&").Replace("'", "''") & "'),dbo.fn_GetCOMOCPIDByCodeName(N'" & txtOCP_ID_Salary.Text & "','SALARY','" & Session("Company").ToString & "')"
                        myDS2 = mySQL.ExecuteSQL(ssql2, Session("Company").ToString, Session("EmpID").ToString)
                        If myDS2.Tables.Count > 0 Then
                            If myDS2.Tables(0).Rows.Count > 0 Then
                                empid = myDS2.Tables(0).Rows(0).Item(0).ToString
                                salaryID = myDS2.Tables(0).Rows(0).Item(1).ToString
                            Else
                                empid = ""
                                salaryID = ""
                            End If
                        Else
                            empid = ""
                            salaryID = ""
                        End If
                        ssql2 = Nothing
                        myDS2 = Nothing

                        ssql = "Exec sp_pr_insUpdDelEmpAdvance N'" & Session("Company").ToString & "',N'" & _
                             Trim(salaryID) & "',N'" & empid & "',N'" & _
                             Trim(txtYEAR.Text.ToString) & "',N'" & Trim(ddlMONTH.SelectedValue) & "',N'" & ddlPAY_CYCLE.SelectedValue & "',N'" & ddlPay_Type.SelectedValue & _
                             "',N'" & ddlMethod.SelectedValue & "',N'" & Trim(txtAmount.Text) & "',N'" & txtJoin_Date.Text & "',N'" & txtResign_Date.Text & _
                             "',N'',N'" & "ADD" & "',N'" & Session("EmpID").ToString & "'"
                        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

                        If myDS.Tables.Count > 0 Then
                            If myDS.Tables(0).Rows.Count > 0 Then
                                myGridView.Rows(i).Cells(1).Text = myDS.Tables(0).Rows(0).Item(0).ToString
                                total = total + 1
                            Else
                                myGridView.Rows(i).Cells(1).Text = ""
                            End If
                        Else
                            myGridView.Rows(i).Cells(1).Text = ""
                        End If
                    End If
                Next
            Else
                empid = ""
                salaryID = ""
                ssql2 = "select dbo.fn_ReturnEmpIDByCodeName(N'" & Session("Company").ToString & "',N'" & txtEMPLOYEE_PROFILE_ID.Text.Replace("&amp;", "&").Replace("'", "''") & "'),dbo.fn_GetCOMOCPIDByCodeName(N'" & txtOCP_ID_Salary.Text & "','SALARY','" & Session("Company").ToString & "')"
                myDS2 = mySQL.ExecuteSQL(ssql2, Session("Company").ToString, Session("EmpID").ToString)
                If myDS2.Tables.Count > 0 Then
                    If myDS2.Tables(0).Rows.Count > 0 Then
                        empid = myDS2.Tables(0).Rows(0).Item(0).ToString
                        salaryID = myDS2.Tables(0).Rows(0).Item(1).ToString
                    Else
                        empid = ""
                        salaryID = ""
                    End If
                Else
                    empid = ""
                    salaryID = ""
                End If
                ssql2 = Nothing
                myDS2 = Nothing

                ssql = "Exec sp_pr_insUpdDelEmpAdvance N'" & Session("Company").ToString & "',N'" & _
                             Trim(salaryID) & "',N'" & empid & "',N'" & _
                             Trim(txtYEAR.Text.ToString) & "',N'" & Trim(ddlMONTH.SelectedValue) & "',N'" & ddlPAY_CYCLE.SelectedValue & "',N'" & ddlPay_Type.SelectedValue & _
                             "',N'" & ddlMethod.SelectedValue & "',N'" & Trim(txtAmount.Text) & "',N'" & txtJoin_Date.Text & "',N'" & txtResign_Date.Text & _
                             "',N'',N'" & "ADD" & "',N'" & Session("EmpID").ToString & "'"
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

                If myDS.Tables.Count > 0 Then
                    If myDS.Tables(0).Rows.Count > 0 Then
                        total = total + 1
                    End If
                End If
            End If
            ShowMessage("Process Completed! Total Employee(s) Processed " + CStr(total))
            'pnlProgress.Visible = True
            'pnlGridview.Visible = False
            'tblCheck.Visible = False
            'pnlButtonPayroll.Visible = False
        End If
    End Sub
    Private Function ValidateProcess() As Boolean
        If ddlPay_Type.SelectedValue = "" Then
            ShowMessage("Cannot be Blank! Pay Type")
            ddlPay_Type.Focus()
            Return False
        End If
        If txtAmount.Text = "" Then
            ShowMessage("Cannot be Blank! Amount")
            txtAmount.Focus()
            Return False
        End If
        If txtOCP_ID_Salary.Text = "" Then
            ShowMessage("Cannot be Blank! Advance Code")
            txtOCP_ID_Salary.Focus()
            Return False
        End If
        If ddlPay_Type.SelectedValue = "PERCENTAGE" And ddlMethod.SelectedValue = "" Then
            ShowMessage("Cannot be Blank! Method")
            ddlMethod.Focus()
            Return False
        End If
        If pnlGridview.Visible = True Then
            For i As Integer = 0 To myGridView.Rows.Count - 1
                Dim chk As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
                If chk.Checked = True Then
                    Return True
                    Exit For
                End If
            Next
            ShowMessage("Please select at least 1 employee to proceed process!")
            Return False
        Else
            Return True
        End If
        Return True
    End Function
    Private Sub ShowMessage(ByVal message As String)
        Dim scriptString As String = "<script language=JavaScript>"
        scriptString += "alert('" + message + "');"
        scriptString += "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "ShowMessage", scriptString)
    End Sub

    Protected Sub chkSelectAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        If chkSelectAll.Checked = True Then
            For i As Integer = 0 To myGridView.Rows.Count - 1
                Dim chkBox As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
                chkBox.Checked = True
            Next
            chkClearAll.Checked = False
        End If
    End Sub

    Protected Sub chkClearAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkClearAll.CheckedChanged
        If chkClearAll.Checked = True Then
            For i As Integer = 0 To myGridView.Rows.Count - 1
                Dim chkBox As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
                chkBox.Checked = False
            Next
            chkSelectAll.Checked = False
        End If
    End Sub
    Private Sub SetVisibleEmployeeProfileID()
        imgKeyEMPLOYEE_PROFILE_ID.Visible = True
        lblEMPLOYEE_PROFILE_ID.Visible = True
        txtEMPLOYEE_PROFILE_ID.Visible = True
        imgbtnEMPLOYEE_PROFILE_ID.Visible = True
        imgbtnFILTER.Visible = False
    End Sub
    Private Sub SetInvisibleEmployeeProfileID()
        imgKeyEMPLOYEE_PROFILE_ID.Visible = False
        lblEMPLOYEE_PROFILE_ID.Visible = False
        txtEMPLOYEE_PROFILE_ID.Visible = False
        imgbtnEMPLOYEE_PROFILE_ID.Visible = False
        imgbtnFILTER.Visible = True
    End Sub

    Protected Sub ddlPAY_CYCLE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPAY_CYCLE.SelectedIndexChanged
        If ddlFILTER_BY.SelectedValue.ToString.ToUpper <> "E" And ddlFILTER_BY.SelectedValue.ToString.ToUpper <> "" Then
            gvMessage.Visible = False
            pnlProgress.Visible = False
            pnlGridview.Visible = True
            tblCheck.Visible = True
            pnlButtonPayroll.Visible = True
            BindGrid()
        End If
    End Sub
    Protected Sub ddlMONTH_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMONTH.SelectedIndexChanged
        If ddlFILTER_BY.SelectedValue.ToString.ToUpper <> "E" And ddlFILTER_BY.SelectedValue.ToString.ToUpper <> "" Then
            gvMessage.Visible = False
            pnlProgress.Visible = False
            pnlGridview.Visible = True
            tblCheck.Visible = True
            pnlButtonPayroll.Visible = True
            BindGrid()
        End If
    End Sub

    Protected Sub ddlPay_Type_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPay_Type.SelectedIndexChanged
        If ddlPay_Type.SelectedValue = "PERCENTAGE" Then
            pnlMETHOD.Visible = True
        Else
            pnlMETHOD.Visible = False
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
