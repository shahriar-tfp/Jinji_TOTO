Imports System.Data

Partial Class Pages_Payroll_Publish_EEA
    Inherits System.Web.UI.Page
    Private WithEvents myDS As New DataSet, myDS2 As New DataSet, mySetting As New clsGlobalSetting, mySQL As New clsSQL
    Dim btnColourDef, btnColourAlt As String
    Dim strPath As String = "../../Images"
    Dim logic As Boolean
    Dim SearchByPage As Boolean = False
    Dim SearchCriteria As Boolean = False
    Dim ssql As String, ssql2 As String, ssql3 As String, i As Integer
    Public _currentPageNumber As Integer, _totalPage As Double = 1, _totalRecords As Integer
    Dim sysYear As Integer, counter As Integer

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
        lblYEAR_MONTH.Text = "Year"
        lblYEAR_MONTH.CssClass = "wordstyle3"
        lblFILTER_BY.Text = "Option Block"
        lblFILTER_BY.CssClass = "wordstyle3"

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

        imgbtnOPTION_PAY_TYPE.Visible = False
        pnlGridview.Visible = False
        tblCheck.Visible = False
        imgbtnPROCESS.Visible = False
        SetInvisibleAll()
        ssql = "Exec sp_app_GetInfo 'Publish_EEA','" & Session("Company") & "','" & Session("Module") & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            If UCase(myDS.Tables(0).Rows(0).Item(0).ToString) = "SUCCESS" Then
                Session("CurrentDate") = myDS.Tables(0).Rows(0).Item(1).ToString
                sysYear = CInt(myDS.Tables(0).Rows(0).Item(3).ToString)
            End If
        End If

        counter = sysYear - 2
        Do Until counter = sysYear + 1
            ddlYEAR.Items.Add(counter)
            counter = counter + 1
        Loop
        ddlYEAR.SelectedValue = sysYear

        mySetting.GetBtnImgUrl(imgbtnFILTER, Session("Company").ToString, btnColourDef, "btnFilter.png")
        mySetting.GetBtnImgUrl(imgbtnPROCESS, Session("Company").ToString, btnColourDef, "btnProcess.png")
        mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourDef, "btnSearch.png")
        mySetting.GetBtnImgUrl(imgBtnClear, Session("Company").ToString, btnColourDef, "btnClear.png")
        mySetting.GetBtnImgUrl(imgBtnCancel, Session("Company").ToString, btnColourDef, "btnCancel.png")
        mySetting.GetBtnImgUrl(imgBtnGoToPage, Session("Company").ToString, btnColourDef, "btngo.png")
        mySetting.GetBtnImgUrl(imgbtnUpdate, Session("Company").ToString, btnColourDef, "btnUpdate.png")
        mySetting.GetBtnImgUrl(imgbtnUpdate2, Session("Company").ToString, btnColourDef, "btnUpdate.png")
        mySetting.SetTextBoxPressEnterGoToImageButton(txtGoToPage, imgBtnGoToPage)

        mySetting.GetImgUrl(imgKeyYEAR_MONTH, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyFILTER_BY, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)

        mySetting.GetImgUrl(imgKeyEMPLOYEE_PROFILE_ID, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyOCP_ID_DIVISION, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyOCP_ID_DEPARTMENT, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyOCP_ID_SECTION, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyOCP_ID_TMS, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyOCP_ID_JOB_GRADE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyOCP_ID_JOB_TITLE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyOPTION_PAY_TYPE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)

        mySetting.GetImgUrl(imgbtnEMPLOYEE_PROFILE_ID, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnOCP_ID_DIVISION, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnOCP_ID_DEPARTMENT, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnOCP_ID_SECTION, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnOCP_ID_TMS, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnOCP_ID_JOB_GRADE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnOCP_ID_JOB_TITLE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnOPTION_PAY_TYPE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)

        mySetting.GetBtnImgUrl(imgbtnSelectAll, Session("Company").ToString, btnColourDef, "addall.png")
        mySetting.GetBtnImgUrl(imgbtnSelectOne, Session("Company").ToString, btnColourDef, "additem.png")
        mySetting.GetBtnImgUrl(imgbtnRemoveAll, Session("Company").ToString, btnColourDef, "removeall.png")
        mySetting.GetBtnImgUrl(imgbtnRemoveOne, Session("Company").ToString, btnColourDef, "removeitem.png")

        mySetting.GetImgTypeUrl(imgTop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Filter.png")
        mySetting.GetImgTypeUrl(imgBottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")

        body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

        mySetting.GetDropdownlistValue("EMPLOYEE_PAYROLL_HEADER", "OPTION_BLOCK", ddlFILTER_BY)
        If ddlFILTER_BY.Items.Count >= 2 Then
            ddlFILTER_BY.SelectedIndex = 0
        End If
        mySetting.GetDropdownlistValue(Form.ID, "OPTION_PAY_TYPE", ddlOPTION_PAY_TYPE)

        mySetting.GetLookupValue_ImageButton(imgbtnEMPLOYEE_PROFILE_ID, Form.ID, "txtEMPLOYEE_PROFILE_ID", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "EMPLOYEE_PROFILE_ID" & """," & """" & """," & """" & Session("EmpID").ToString & """")
        mySetting.GetLookupValue_ImageButton(imgbtnOCP_ID_DIVISION, Form.ID, "txtOCP_ID_DIVISION", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_DIVISION" & """," & """" & """," & """" & Session("EmpID").ToString & """")
        mySetting.GetLookupValue_ImageButton(imgbtnOCP_ID_DEPARTMENT, Form.ID, "txtOCP_ID_DEPARTMENT", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_DEPARTMENT" & """," & """" & """," & """" & Session("EmpID").ToString & """")
        mySetting.GetLookupValue_ImageButton(imgbtnOCP_ID_SECTION, Form.ID, "OCP_ID_SECTION", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_SECTION" & """," & """" & """," & """" & Session("EmpID").ToString & """")
        mySetting.GetLookupValue_ImageButton(imgbtnOCP_ID_TMS, Form.ID, "txtOCP_ID_TMS", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_TMS" & """," & """" & """," & """" & Session("EmpID").ToString & """")
        mySetting.GetLookupValue_ImageButton(imgbtnOCP_ID_JOB_GRADE, Form.ID, "txtOCP_ID_JOB_GRADE", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_JOB_GRADE" & """," & """" & """," & """" & Session("EmpID").ToString & """")
        mySetting.GetLookupValue_ImageButton(imgbtnOCP_ID_JOB_TITLE, Form.ID, "txtOCP_ID_JOB_TITLE", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_JOB_TITLE" & """," & """" & """," & """" & Session("EmpID").ToString & """")

        'get Page Title
        ssql = "exec sp_sa_GetPageTitle 'PUBLISH_EEA'"
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
                ssql = "Exec sp_pr_selPublishEEA N'" & Session("Company").ToString & "',N'" & ddlYEAR.SelectedValue & "',N'" & ddlFILTER_BY.SelectedValue & "'"
                'Else
                '    ssql = "Exec sp_Generate_Query_Filter_WithSecurity N'" & Session("Company").ToString & "',N'" & Session("EmpID").ToString & "',N'" & _
                '                                Session("Module").ToString & "',N'" & ssql & "',N'" & ssql2 & "',N'" & ssql3 & _
                '                                "','99999','" & "1" & "'"
            End If
            myDS = New DataSet
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If Not myDS Is Nothing Then
                myGridView.DataSource = myDS.Tables(0).DefaultView
                myGridView.DataBind()

                If ((myGridView.Rows.Count + 2) * 26) < CInt(Session("GVheight")) Then
                    'pnlGridview.Height = (CInt(Left(myGridView.Height.ToString, Len(myGridView.Height.ToString) - 2)) + 30)
                    pnlGridview.Height = ((myGridView.Rows.Count + 2) * 26)
                Else
                    pnlGridview.Height = CInt(Session("GVheight"))
                End If

                If myDS.Tables(0).Rows.Count > 0 Then
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
        ElseIf ddlFILTER_BY.SelectedValue.ToUpper = "YES" Then
            imgbtnUpdate.Visible = False
            imgbtnUpdate2.Visible = True
            SetInvisibleEmployeeProfileID()
            pnlProgress.Visible = False
            imgBtnSearch_Click(Nothing, Nothing)
        Else
            imgbtnUpdate.Visible = True
            imgbtnUpdate2.Visible = False
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
        txtOCP_ID_DIVISION.Text = ""
        txtOCP_ID_DEPARTMENT.Text = ""
        txtOCP_ID_JOB_GRADE.Text = ""
        txtOCP_ID_JOB_TITLE.Text = ""
        txtOCP_ID_SECTION.Text = ""
        txtOCP_ID_TMS.Text = ""
    End Sub
    Private Sub SetVisibleAll()
        imgKeyOPTION_PAY_TYPE.Visible = True
        imgKeyOCP_ID_DIVISION.Visible = True
        imgKeyOCP_ID_DEPARTMENT.Visible = True
        imgKeyOCP_ID_SECTION.Visible = True
        imgKeyOCP_ID_TMS.Visible = True
        imgKeyOCP_ID_JOB_GRADE.Visible = True
        imgKeyOCP_ID_JOB_TITLE.Visible = True

        lblOPTION_PAY_TYPE.Visible = True
        lblOCP_ID_DIVISON.Visible = True
        lblOCP_ID_DEPARTMENT.Visible = True
        lblOCP_ID_SECTION.Visible = True
        lblOCP_ID_TMS.Visible = True
        lblOCP_ID_JOB_GRADE.Visible = True
        lblOCP_ID_JOB_TITLE.Visible = True

        ddlOPTION_PAY_TYPE.Visible = True
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


        lblOPTION_PAY_TYPE.Visible = False
        lblOCP_ID_DIVISON.Visible = False
        lblOCP_ID_DEPARTMENT.Visible = False
        lblOCP_ID_SECTION.Visible = False
        lblOCP_ID_TMS.Visible = False
        lblOCP_ID_JOB_GRADE.Visible = False
        lblOCP_ID_JOB_TITLE.Visible = False


        ddlOPTION_PAY_TYPE.Visible = False
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
        txtOCP_ID_DIVISION.Text = ""
        txtOCP_ID_DEPARTMENT.Text = ""
        txtOCP_ID_SECTION.Text = ""
        txtOCP_ID_TMS.Text = ""
        txtOCP_ID_JOB_GRADE.Text = ""
        txtOCP_ID_JOB_TITLE.Text = ""
        txtEMPLOYEE_PROFILE_ID.Text = ""
    End Sub

    
    Private Function ValidateProcess() As Boolean
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
        imgbtnFILTER.Visible = False
        'imgbtnFILTER.Visible = True
    End Sub

    
    Protected Sub ddlYEAR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlYEAR.SelectedIndexChanged
        If ddlFILTER_BY.SelectedValue.ToUpper = "YES" Then
            imgbtnUpdate.Visible = False
            imgbtnUpdate2.Visible = True
            SetInvisibleEmployeeProfileID()
            pnlProgress.Visible = False
            imgBtnSearch_Click(Nothing, Nothing)
        Else
            imgbtnUpdate.Visible = True
            imgbtnUpdate2.Visible = False
            SetInvisibleEmployeeProfileID()
            pnlProgress.Visible = False
            imgBtnSearch_Click(Nothing, Nothing)
        End If
    End Sub

    Protected Sub imgbtnUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim SPID As String = mySQL.GetSPID
        Dim ssql1 As String, myDS As DataSet

        If pnlGridview.Visible = True Then
            For i = 0 To myGridView.Rows.Count - 1
                Dim chkBox As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
                If chkBox.Checked = True Then
                    ssql1 = "select isnull(dbo.fn_ReturnEmpIDByCodeName(N'" & Session("Company").ToString & "',N'" & myGridView.Rows(i).Cells(1).Text.ToString.Replace("&amp;", "&").Replace("'", "''") & "'),'')"
                    myDS = mySQL.ExecuteSQL(ssql1)
                    If myDS.Tables(0).Rows(0).Item(0).ToString <> "" Then
                        ssql = "exec sp_pr_UpdPublishEEA '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & ddlYEAR.SelectedValue & "','YES','ADD'"
                        mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                    End If
                    ssql1 = Nothing
                    myDS = Nothing
                End If
            Next
            ssql1 = "exec sp_pr_UpdPublishEEA '','','','Publish Result'"
            myDS = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
            lblSysName.Visible = True
            lblSysName.Text = myDS.Tables(0).Rows(0).Item(0).ToString
            pnlProgress.Visible = True
            pnlGridview.Visible = False
            tblCheck.Visible = False
            ssql1 = Nothing
            myDS = Nothing
        End If
    End Sub

    Protected Sub imgbtnUpdate2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnUpdate2.Click
        Dim SPID As String = mySQL.GetSPID
        Dim ssql1 As String, myDS As DataSet

        If pnlGridview.Visible = True Then
            For i = 0 To myGridView.Rows.Count - 1
                Dim chkBox As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
                If chkBox.Checked = True Then
                    ssql1 = "select isnull(dbo.fn_ReturnEmpIDByCodeName(N'" & Session("Company").ToString & "',N'" & myGridView.Rows(i).Cells(1).Text.ToString.Replace("&amp;", "&").Replace("'", "''") & "'),'')"
                    myDS = mySQL.ExecuteSQL(ssql1)
                    If myDS.Tables(0).Rows(0).Item(0).ToString <> "" Then
                        ssql = "exec sp_pr_UpdPublishEEA '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & ddlYEAR.SelectedValue & "','NO','ADD'"
                        mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                    End If
                    ssql1 = Nothing
                    myDS = Nothing
                End If
            Next
            ssql1 = "exec sp_pr_UpdPublishEEA '','','','UnPublish Result'"
            myDS = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
            lblSysName.Visible = True
            lblSysName.Text = myDS.Tables(0).Rows(0).Item(0).ToString
            pnlProgress.Visible = True
            pnlGridview.Visible = False
            tblCheck.Visible = False
            ssql1 = Nothing
            myDS = Nothing
        End If
    End Sub
End Class
