Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.web.Configuration

Partial Class Pages_Approval_Skill_Approval
    Inherits System.Web.UI.Page

#Region "Public Declaration"
    Private WithEvents mySQL As New clsSQL, myDS, myDS1, myDS2, myDS3, myDS4, myDS10 As New DataSet, mySetting As New clsGlobalSetting, myMsg As New clsMessage
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Public ssql As String, i As Integer, j As Integer, SearchByPage As Boolean, RecFound As Boolean, CountRecord As Integer
    Public AllowView, AllowInsert, AllowUpdate, AllowDelete, AllowPrint As Boolean
    Public _currentPageNumber As Integer, _totalPage As Double = 1, _totalRecords As Integer
    Public myDE As New DictionaryEntry, myHT As Hashtable
    Public myDataType As New clsGlobalSetting.DataType
    Public myTB1, myTB2 As New DataTable
    Public myTB3, myTB4 As New DataTable
    Dim subcustomerID2, SubItem2 As String
#End Region

#Region "Private Declaration"
    Private WithEvents myDT1 As New DataTable, myDT2 As New DataTable
    Dim myDR1 As DataRow, myDR2 As DataRow, rcPerPage As Integer, autonum As Integer
    Dim ssql1, ssql2, ssql3, ssql4 As String
    Dim rowPosition As String = "R_", rowPositionM As String = "RM_", panelPosition As String = "Display_"
    Dim buttonPosition1 As String = "B1_", buttonPosition2 As String = "B2_", buttonPosition3 As String = "B3_", labelPosition1 As String = "L1_"
    Dim logic, check As Boolean
    Dim grdItem As GridViewRow
    Dim chkSelected, chkSelected1, chkSelected2 As CheckBox
    Dim dtSelectedDate As DateTime
    Dim stPeriod, stDateApplyOn, PrevLeaveID, LeaveID, iID, StopChecking, mstBalance As String
    Dim ApplicationEnd, ContinueLoop As Boolean
    Dim Day, TotalDay As Decimal
    Dim Err1, Err2, Err3, Err4, Err5, Err6, vDate1, vDate2, vDate3, vDate4, vDate5, vDate6, Message As String
    Dim vCount, vCounter, CountDay, CountSequence, ContinueCount As Integer
    Dim Action, DateApplyOn, DateApplyFor, EmpID, EmpCode, Leave, Period, CurrentDate, TimeIn, TimeOut As String
    Dim strEmpid, strMasterskill, strYear, strDate As String
    Dim Counter, CurrentYear, SysYear As Integer
    Dim strPath As String = "../../Images"
    Dim btnColourDef, btnColourAlt As String
    Dim EmployeeCode As String
#End Region

#Region "Page Setting"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            If Session.IsNewSession Then
                Response.Redirect("../Global/SessionTimeOut.aspx")
            End If

            If Not Page.IsPostBack Then
                Response.CacheControl = "no-cache"
                Response.AddHeader("Pragma", "no-cache")
                Response.Expires = -1

                If Session("ScreenWidth") = 0 Then
                    Session("ScreenWidth") = "1024"
                    Session("GVwidth") = Session("ScreenWidth") - 360
                End If
                If Session("ScreenHeight") = 0 Then
                    Session("ScreenHeight") = "768"
                    Session("GVheight") = (Session("ScreenHeight") / 2) - 50
                End If

                pnlTitle.Width = CInt(Session("GVwidth")) - 10
                PagePreload()
                GetRequireData()
                chkAAll.Visible = False
                chkRAll.Visible = False

                'load pending tab by default
                lnkBtnViewPending_Click()
            End If

        Catch ex As Exception
            lblresult.Text = "[Page_Load]Error : " & ex.Message
        End Try

    End Sub

    Sub PagePreload()

        Try
            btnColourDef = Session("strTheme")
            btnColourAlt = Session("strThemeAlt")
            Session("Module") = "Approval"
            Session("action") = ""
            _currentPageNumber = 1
            Session("currentpage1") = _currentPageNumber
            FirstPage1.Enabled = False
            PrevPage1.Enabled = False
            NextPage1.Enabled = False
            LastPage1.Enabled = False

            'disable leave details button
            imgBtnLeaveDetail.Visible = False
            ddlOption_Type.Visible = False

            'bind into ddlOption_Type
            mySetting.GetDropdownlistValue(Form.ID, "Option_Type", ddlOption_Type)

            'get Page Title
            ssql = "exec sp_sa_GetPageTitle '" & Form.ID & "'"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables(0).Rows.Count > 0 Then
                lblTitle.Text = myDS.Tables(0).Rows(0).Item(0)
                lblTitle.CssClass = "wordstyle4"
            End If
            myDS = Nothing

            'get Company Code & Name
            lblCompany_Profile_ID.Text = "Organisation"
            txtCompany_Profile_ID.Text = Session("Company")
            ssql = "Select Name From Company_Profile Where Code ='" & Session("Company") & "'"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables(0).Rows.Count > 0 Then
                txtCompany_Name.Text = myDS.Tables(0).Rows(0).Item(0)
            Else
                txtCompany_Name.Text = ""
            End If
            myDS = Nothing
            txtCompany_Profile_ID.ReadOnly = True
            txtCompany_Name.ReadOnly = True
            imgBtnCompany_Profile_ID.Visible = False
            lblCompany_Profile_ID.CssClass = "wordstyle10"

            'get panel part 2 name
            chkAEmployee.Text = "By Employee"
            chkADepartment.Text = "By Others"
            lblAID.Text = "Employee Code"
            lblAName.Text = "Employee Name"
            lblADateFrom.Text = "Date From"
            lblADateTo.Text = "Date To"

            'get panel part 3 name
            optQFRLeave.Text = "Qualified Reject Claim"
            lblYear.Text = "Year"
            optRLDetails.Text = "Reject Claim Details"

            'get chk sel name
            chkAAll.Text = "Approve All"
            chkRAll.Text = "Reject All"

            'get panel Balance name
            lblBalAID.Text = "Employee Name"

            'get image 
            mySetting.GetImgUrl(imgCompany_Profile_ID, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)

            mySetting.GetImgTypeUrl(imgBlank02, Session("Company").ToString, "Png", "blank.png")
            mySetting.GetImgTypeUrl(imgBlank03, Session("Company").ToString, "Png", "blank.png")
            mySetting.GetImgTypeUrl(imgBlank04, Session("Company").ToString, "Png", "blank.png")
            mySetting.GetImgTypeUrl(imgBlank05, Session("Company").ToString, "Png", "blank.png")

            'get image button
            mySetting.GetImgBtnUrl(imgBtnCompany_Profile_ID, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
            mySetting.GetImgBtnUrl(imgBtnAID, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
            mySetting.GetImgBtnUrl(imgBtnAName, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
            mySetting.GetImgBtnUrl(imgBtnADateFrom, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
            mySetting.GetImgBtnUrl(imgBtnADateTo, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)

            mySetting.GetBtnImgUrl(imgBtnLeaveDetail, Session("Company").ToString, btnColourDef, "btnDetails.png")
            mySetting.GetBtnImgUrl(imgBtnUpdate, Session("Company").ToString, btnColourDef, "btnUpdate.png")
            mySetting.GetBtnImgUrl(imgBtnCancel, Session("Company").ToString, btnColourDef, "btnCancel.png")
            mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourDef, "btnSearch.png")
            mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourDef, "btnPrint.png")
            mySetting.GetBtnImgUrl(imgBtnGoToPage1, Session("Company").ToString, btnColourDef, "btngo.png")
            mySetting.SetTextBoxPressEnterGoToImageButton(txtGoToPage1, imgBtnGoToPage1)

            'get lookup setting
            ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "EMPLOYEE_CODE" & """," & """" & """," & """" & Session("EmpID").ToString & """"
            mySetting.GetLookupValue_ImageButton(imgBtnAID, Form.ID, "txtAID", "Code", ssql)
            ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "EMPLOYEE_NAME" & """," & """" & """," & """" & Session("EmpID").ToString & """"
            mySetting.GetLookupValue_ImageButton(imgBtnAName, Form.ID, "txtANAME", "Name", ssql)

            'Blind Employee.
            ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "EMPLOYEE_CODE" & """,'" & "'," & """" & Session("EmpID").ToString & """"
            myDS = mySQL.ExecuteSQL(ssql)
            If myDS.Tables.Count > 0 Then
                If myDS.Tables(0).Columns.Count > 1 Then
                    ddlAID.DataTextField = "Name"
                    ddlAID.DataValueField = "Code"
                    ddlAID.DataSource = myDS
                    ddlAID.DataBind()
                End If
            End If
            myDS = Nothing

            Dim EmployeeCode As String = "%"
            Dim CompanyCode As String = ""
            If ddlAID.SelectedValue <> "" Then
                ssql = "Select Top 1 Employee_Profile_ID,company_profile_code from Employee_CodeName_Vw Where Code = N'" & Trim(ddlAID.SelectedValue) & "' And [Name] = N'" & Trim(ddlAID.SelectedItem.Text).Replace("'", "''") & "' Order By Effective_Date Desc"
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                If myDS.Tables(0).Rows.Count > 0 Then
                    EmployeeCode = myDS.Tables(0).Rows(0).Item(0).ToString
                    CompanyCode = myDS.Tables(0).Rows(0).Item(1).ToString
                    'ssql = "Exec sp_ls_cal_emp_leave_balance '" & CompanyCode & "','" & EmployeeCode & "'"
                    'mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                Else
                    EmployeeCode = "%"
                    myDS = Nothing
                End If
                myDS = Nothing
            End If
            ssql2 = "exec sp_sa_getTableRecords2 '" & Session("Company") & "','LEAVE','Employee_Leave_Balance','100','1','[EMPLOYEE_PROFILE_ID]','" & EmployeeCode & "'"
            myDS1 = mySQL.ExecuteSQL(ssql2)

            If myDS1.Tables.Count = 2 Then
                BalGv.DataSource = myDS1.Tables(1)
                BalGv.DataBind()
            End If
            myDS1 = Nothing

            'get calendar setting
            mySetting.PopUpCalendar_ImageButton(imgBtnADateFrom, Form.ID, "txtADateFrom")
            mySetting.PopUpCalendar_ImageButton(imgBtnADateTo, Form.ID, "txtADateTo")

            body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

            myDS = mySetting.GetPageFieldSetting(Session("Company"), Form.ID, Session("EmpID"))
            If myDS.Tables.Count > 1 Then
                myDT1 = myDS.Tables(0)

                If myDT1.Rows.Count > 0 Then
                    myDR1 = myDT1.Rows(0)
                Else
                    lblresult.Text = "[Page Setting Error]: No setting found for this page!"
                    Exit Sub
                End If

                If myDT1.Rows.Count > 0 Then
                    myDR1 = myDT1.Rows(0)

                    Page.Title = myDR1(1)
                    If myDR1(3) = "YES" Then
                        AllowView = True
                        Table1.Visible = True
                    Else
                        AllowView = False
                        pnlresult.Visible = False
                        Table1.Visible = False
                        ShowMessage("You are not allow to view this page!")
                        Exit Sub
                    End If

                    If myDR1(4) = "YES" Then AllowInsert = True Else AllowInsert = False
                    If myDR1(5) = "YES" Then AllowUpdate = True Else AllowUpdate = False
                    If myDR1(6) = "YES" Then AllowDelete = True Else AllowDelete = False
                    If myDR1(7) = "YES" Then AllowPrint = True Else AllowPrint = False

                    Session("AllowView") = AllowView
                    Session("AllowInsert") = AllowInsert
                    Session("AllowUpdate") = AllowUpdate
                    Session("AllowDelete") = AllowDelete
                    Session("AllowPrint") = AllowPrint

                    imgBtnUpdate.Visible = AllowUpdate
                    imgBtnSearch.Visible = AllowView
                    imgBtnCancel.Visible = AllowView
                    imgBtnPrint.Visible = AllowPrint

                    'Get GV page per record
                    If myDR1(8) <= 0 Then
                        ssql = "exec sp_sa_getParameter '" & Session("Company") & "', '" & Session("Module") & "', 'NO_OF_RECORD'"
                        myDS2 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                        If myDS2.Tables(0).Rows.Count > 0 Then
                            Session("rcPerPage") = myDS2.Tables(0).Rows(0).Item(9)
                        End If
                        myDS2 = Nothing
                    Else
                        Session("rcPerPage") = myDR1(8)
                    End If
                    myGridView1.PageSize = Session("rcPerPage")
                    myGridview2.PageSize = Session("rcPerPage")
                Else
                    lblresult.Text = "[Page Setting Error]: No setting found for this page!"
                    Exit Sub
                End If

                myDS = Nothing
                myDT1 = Nothing
                myDT2 = Nothing
            Else
                lblresult.Text = "[Field Setting Error]: No setting found for this page!"
                Exit Sub
            End If

        Catch ex As Exception
            lblresult.Text = "[PagePreload]Error : " & ex.Message
        End Try

    End Sub

    Sub GetRequireData()

        Try
            ssql = "Exec sp_app_GetInfo 'LeaveApproval','" & Session("Company") & "','" & Session("Module") & "'"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables(0).Rows.Count > 0 Then
                If UCase(myDS.Tables(0).Rows(0).Item(0).ToString) = "SUCCESS" Then
                    Session("CurrentDate") = myDS.Tables(0).Rows(0).Item(1).ToString
                    SysYear = CInt(myDS.Tables(0).Rows(0).Item(3).ToString)
                    lnkBtnViewPending.Enabled = True
                    lnkBtnViewApproved.Enabled = True
                    lnkBtnViewReject.Enabled = True
                    lnkBtnViewBalance.Enabled = True
                Else
                    pnlpart1.Visible = False
                    pnlpart2.Visible = False
                    pnlpart3.Visible = False
                    pnlbutton.Visible = False
                    lblresult.Text = "Error Found! Data fail to retreive..."
                    Exit Sub
                End If
            End If

            Counter = SysYear - 2
            Do Until Counter = SysYear + 1
                ddlYear.Items.Add(Counter)
                Counter = Counter + 1
            Loop
            ddlYear.SelectedValue = SysYear

        Catch ex As Exception
            lblresult.Text = "[GetRequireData]Error : " & ex.Message
        End Try

    End Sub

#End Region

#Region "Panel Action"

    Protected Sub lnkBtnViewPending_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnViewPending.Click

        lnkBtnViewPending_Click()

    End Sub

    Protected Sub lnkBtnClosePending_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnClosePending.Click

        lnkBtnClosePending_Click()

    End Sub

    Protected Sub lnkBtnViewApproved_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnViewApproved.Click

        lnkBtnViewApproved_Click()

    End Sub

    Protected Sub lnkBtnCloseApproved_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnCloseApproved.Click

        lnkBtnCloseApproved_Click()

    End Sub

    Protected Sub lnkBtnViewReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnViewReject.Click

        lnkBtnViewReject_Click()

    End Sub

    Protected Sub lnkBtnCloseReject_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnCloseReject.Click

        lnkBtnCloseReject_Click()

    End Sub

    Protected Sub lnkBtnViewBalance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnViewBalance.Click

        lnkBtnViewBalance_Click()

    End Sub

    Protected Sub lnkBtnCloseBalance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnCloseBalance.Click

        lnkBtnCloseBalance_Click()

    End Sub

    Protected Sub chkAEmployee_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAEmployee.CheckedChanged

        chkAEmployee.Checked = True
        chkADepartment.Checked = False
        OptionCheckedChange1()
        ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "EMPLOYEE_CODE" & """," & """" & """," & """" & Session("EmpID").ToString & """"
        mySetting.GetLookupValue_ImageButton(imgBtnAID, Form.ID, "txtAID", "Code", ssql)
        ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "EMPLOYEE_NAME" & """," & """" & """," & """" & Session("EmpID").ToString & """"
        mySetting.GetLookupValue_ImageButton(imgBtnAName, Form.ID, "txtAName", "Name", ssql)
        txtAID.Text = ""
        txtAName.Text = ""
        lblAID.Visible = True
        lblAName.Visible = True
        txtAName.Visible = True
        imgBtnAName.Visible = True
        ddlOption_Type.Visible = False

    End Sub

    Protected Sub chkADepartment_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkADepartment.CheckedChanged

        chkADepartment.Checked = True
        chkAEmployee.Checked = False
        OptionCheckedChange1()
        txtAID.Text = ""
        txtAName.Text = ""
        lblAID.Visible = False
        lblAName.Visible = False
        txtAName.Visible = False
        imgBtnAName.Visible = False
        ddlOption_Type.Visible = True
        ddlOption_Type.SelectedIndex = 0
        imgBtnUpdate.Enabled = False
        ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & ddlOption_Type.SelectedValue & """," & """" & """," & """" & Session("EmpID").ToString & """"
        mySetting.GetLookupValue_ImageButton(imgBtnAID, Form.ID, "txtAID", "CodeName", ssql)

    End Sub

    Protected Sub optRLDetails_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optRLDetails.CheckedChanged

        ddlYear.Enabled = False
        pnlpart2.Visible = True
        ClearField()

    End Sub

    Protected Sub optQFRLeave_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optQFRLeave.CheckedChanged

        ddlYear.Enabled = True
        pnlpart2.Visible = False
        ClearField()

    End Sub

    Protected Sub chkAAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAAll.CheckedChanged

        chkRAll.Checked = False
        OptionCheckedChange2()

    End Sub

    Protected Sub chkRAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRAll.CheckedChanged

        chkAAll.Checked = False
        OptionCheckedChange2()

    End Sub

    Protected Sub ddlOption_Type_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOption_Type.SelectedIndexChanged

        txtAID.Text = ""
        ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & ddlOption_Type.SelectedValue & """," & """" & """," & """" & Session("EmpID").ToString & """"
        mySetting.GetLookupValue_ImageButton(imgBtnAID, Form.ID, "txtAID", "CodeName", ssql)

    End Sub

    Protected Sub imgBtnLeaveDetail_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnLeaveDetail.Click

        Session("action") = "detail"
        lblresult.Text = ""

    End Sub

    Protected Sub imgBtnUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnUpdate.Click

        Session("action") = "update"
        check = True
        lblresult.Text = ""
        If CheckForARGv1() > 1 Then
            lblresult.Text = "Please confirm the selection! Only one action can be seleted per row data..."
            Exit Sub
        Else
            'If CheckForAGv1() = 0 And CheckForRGv1() = 0 Then
            '    lblresult.Text = "Please confirm the selection! No row selected..."
            '    Exit Sub
            'Else
            If lnkBtnClosePending.Visible = True Then
                For Me.i = 0 To myGridView1.Rows.Count - 1
                    'Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(0).Controls(1)
                    'If chkCursor.Checked = True Then
                    'If chkCursor.Checked = True Then
                    Dim Count As Integer = lstEmpID.Items.Count
                    j = 0
                    logic = True
                    While j < Count
                        If myGridview2.Rows(i).Cells(4).Text = lstEmpID.Items(j).Text Then
                            logic = False
                        End If
                        j = j + 1
                    End While
                    If logic = True Then
                        lstEmpID.Items.Add(myGridview2.Rows(i).Cells(4).Text)
                    End If
                    'End If
                    'End If
                    'chkCursor = Nothing
                Next

                For Me.i = 0 To lstEmpID.Items.Count - 1
                    'leave recalculate
                    ssql = "Select code From Employee_CodeName_Vw Where Employee_Profile_ID = '" & lstEmpID.Items(i).Text & "'"
                    myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                    If myDS.Tables.Count > 0 Then
                        If myDS.Tables(0).Rows.Count > 0 Then
                            EmpCode = myDS.Tables(0).Rows(0).Item(0).ToString
                        Else
                            lblresult.Text = "System failed to retrieve data!"
                            myDS = Nothing
                            Exit Sub
                        End If
                    Else
                        lblresult.Text = "System failed to retrieve data!"
                        myDS = Nothing
                        Exit Sub
                    End If
                    myDS = Nothing
                Next

                UpdPendingMedicalToAppMedical()
                If check = True Then
                    UpdPendingMedicalToReject()
                End If
                If check = True Then
                    SubLoadSSQL(Session("TabStatus"))
                    RetrieveDetail()
                End If
            ElseIf lnkBtnCloseApproved.Visible = True Then
                UpdApprovedMedical()
                SubLoadSSQL(Session("TabStatus"))
                RetrieveDetail()
            End If
        End If

        'End If

    End Sub

    Protected Sub imgBtnCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnCancel.Click

        Session("action") = "cancel"
        lblresult.Text = ""
        If Session("TabStatus") = "PENDING" Then
            lnkBtnClosePending_Click()
        ElseIf Session("TabStatus") = "APPROVED" Then
            lnkBtnCloseApproved_Click()
        ElseIf Session("TabStatus") = "REJECT" Then
            lnkBtnCloseReject_Click()
        End If

    End Sub

    Protected Sub imgBtnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSearch.Click
        Session("currentPage1") = 1
        Session("action") = "search"
        lblresult.Text = ""
        SearchDetail(Session("TabStatus"))

    End Sub

    Protected Sub imgBtnPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnPrint.Click

        Session("action") = "print"
        lblresult.Text = ""

    End Sub

#End Region

#Region "Sub & Function"

    Public Sub SubLoadSSQL(ByVal Status As String)

        Dim FromDate As String = ""
        Dim ToDate As String = ""
        ssql = "Select dbo.fn_DateAdd('Day',-120,'" & Session("CurrentDate") & "')"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            FromDate = myDS.Tables(0).Rows(0).Item(0).ToString
        End If
        myDS = Nothing
        ToDate = "19000101000000"
        If Status = "PENDING" Then
            FromDate = "19000101000000"
        End If

        ssql = "Exec sp_KPI_SkillApproval '" & Session("Company") & "','" & Session("EmpID") & "','','" & Status & _
               "','','','" & FromDate & "','" & ToDate & "','','" & myGridView1.PageSize & "','" & Session("currentPage1") & "'"

    End Sub

    Public Sub RetrieveDetail()

        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")

        If myDS.Tables.Count = 7 Then
            If myDS.Tables(1).Rows.Count = 0 Then
                If Session("action") = "search" Then
                    lblresult.Text = "No Data Found..."
                Else
                    pnlbutton.Visible = True
                    imgBtnCancel.Enabled = False
                    'imgBtnSearch.Enabled = False
                    imgBtnPrint.Enabled = False

                    'If Session("action") = "APPROVED" Then
                    '    pnlpart2.Visible = False
                    'ElseIf Session("action") = "REJECT" Then
                    '    pnlpart2.Visible = False
                    '    pnlpart3.Visible = False
                    'End If

                    mySetting.GetBtnImgUrl(imgBtnCancel, Session("Company").ToString, btnColourAlt, "btnCancel.png")
                    mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourAlt, "btnSearch.png")
                    mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourAlt, "btnPrint.png")
                End If

                imgBtnLeaveDetail.Enabled = False
                imgBtnUpdate.Enabled = False
                imgBtnGoToPage1.Enabled = False

                mySetting.GetBtnImgUrl(imgBtnLeaveDetail, Session("Company").ToString, btnColourAlt, "btnDetails.png")
                mySetting.GetBtnImgUrl(imgBtnUpdate, Session("Company").ToString, btnColourAlt, "btnUpdate.png")
                mySetting.GetBtnImgUrl(imgBtnGoToPage1, Session("Company").ToString, btnColourAlt, "btnGo.png")

                CurrentPage1.Text = myDS.Tables(0).Rows(0).Item(0)
                TotalPages1.Text = myDS.Tables(0).Rows(0).Item(0)
                lbltotal1.Text = " ( " & myDS.Tables(0).Rows(0).Item(0) & " record(s) ) "
                myGridView1.DataSource = myDS.Tables(1)
                myGridView1.DataBind()

                FirstPage1.Enabled = False
                PrevPage1.Enabled = False
                NextPage1.Enabled = False
                LastPage1.Enabled = False
                chkAAll.Enabled = False
                chkRAll.Enabled = False
                Exit Sub
            Else
                imgBtnLeaveDetail.Enabled = True
                imgBtnUpdate.Enabled = True
                imgBtnCancel.Enabled = True
                imgBtnSearch.Enabled = True
                imgBtnPrint.Enabled = True
                imgBtnGoToPage1.Enabled = True
                chkAAll.Enabled = True
                chkRAll.Enabled = True
                FirstPage1.Enabled = True
                PrevPage1.Enabled = True
                NextPage1.Enabled = True
                LastPage1.Enabled = True
                mySetting.GetBtnImgUrl(imgBtnLeaveDetail, Session("Company").ToString, btnColourDef, "btnDetails.png")
                mySetting.GetBtnImgUrl(imgBtnUpdate, Session("Company").ToString, btnColourDef, "btnUpdate.png")
                mySetting.GetBtnImgUrl(imgBtnCancel, Session("Company").ToString, btnColourDef, "btnCancel.png")
                mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourDef, "btnSearch.png")
                mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourDef, "btnPrint.png")
                mySetting.GetBtnImgUrl(imgBtnGoToPage1, Session("Company").ToString, btnColourDef, "btnGo.png")
            End If

            myTB1 = myDS.Tables(3)
            Session("myTB1") = myTB1
            myTB2 = myDS.Tables(4)
            Session("myTB2") = myTB2
            myTB3 = myDS.Tables(5)
            Session("myTB3") = myTB3
            myTB4 = myDS.Tables(6)
            Session("myTB4") = myTB4
            myGridView1.DataSource = myDS.Tables(1)
            myGridView1.DataBind()
            myGridview2.DataSource = myDS.Tables(2)
            myGridview2.DataBind()
            pnlbutton.Visible = True

            If Session("TabStatus") = "PENDING" Then
                Enable_Approve()
                Enable_Reject()
                chkAAll.Enabled = True
                chkRAll.Enabled = True
            ElseIf Session("TabStatus") = "PAID" Then
                Disable_Approve()
                Enable_Reject()
                chkAAll.Enabled = False
                chkRAll.Enabled = True
            ElseIf Session("TabStatus") = "REJECT" Then
                Disable_Approve()
                Disable_Reject()
                chkAAll.Enabled = False
                chkRAll.Enabled = False
            End If

            lbltotal1.Text = " ( " & myDS.Tables(0).Rows(0).Item(0) & " record(s) ) "
            CurrentPage1.Text = Session("currentPage1")

            _totalRecords = myDS.Tables(0).Rows(0).Item(0)
            _totalPage = _totalRecords / myGridView1.PageSize
            TotalPages1.Text = (System.Math.Ceiling(_totalPage)).ToString()
            _totalPage = Double.Parse(TotalPages1.Text)
            Session("TotalPages1") = TotalPages1.Text

            If Session("currentpage1") = 1 Then
                FirstPage1.Enabled = False
                PrevPage1.Enabled = False
                If _totalRecords > myGridView1.PageSize Then
                    NextPage1.Enabled = True
                    LastPage1.Enabled = True
                Else
                    NextPage1.Enabled = False
                    LastPage1.Enabled = False
                End If
            ElseIf Session("currentpage1") > 1 Then
                FirstPage1.Enabled = True
                PrevPage1.Enabled = True
                If Session("currentpage1") = Session("TotalPages1") Then
                    NextPage1.Enabled = False
                    LastPage1.Enabled = False
                Else
                    NextPage1.Enabled = True
                    LastPage1.Enabled = True
                End If
            End If
        Else
            pnlbutton.Visible = False
        End If

        myDS = Nothing

        'GridView Column Width Setting
        logic = False
        myDS1 = mySetting.GetGridViewWidth(Form.ID)
        myDT1 = myDS1.Tables(0)
        myDT2 = myDS1.Tables(1)
        Dim value As Decimal
        If myDT2.Rows.Count > 0 Then

            If CInt(myDT1.Rows(0).Item(1).ToString) < CInt(Session("GVwidth")) Then
                If CInt(myDT1.Rows(0).Item(1).ToString) = 0 Then
                    logic = True
                Else
                    value = CInt(Session("GVwidth")) / CInt(myDT1.Rows(0).Item(1).ToString)
                End If
            Else
                value = 1
                myGridView1.Width = CInt(myDT1.Rows(0).Item(1).ToString)
                myGridview2.Width = CInt(myDT1.Rows(0).Item(1).ToString)
            End If
            If logic = False Then
                For Me.i = 0 To myDT2.Rows.Count - 1
                    j = CInt(myDT2.Rows(i).Item(0).ToString)
                    If CInt(myDT2.Rows(i).Item(2).ToString) = 0 Then
                        myGridView1.Rows(0).Cells(j).Width = 50 * value
                        myGridview2.Rows(0).Cells(j).Width = 50 * value
                    Else
                        myGridView1.Rows(0).Cells(j).Width = CInt(myDT2.Rows(i).Item(2).ToString) * value
                        myGridview2.Rows(0).Cells(j).Width = CInt(myDT2.Rows(i).Item(2).ToString) * value
                    End If
                Next
            End If
        End If
        myDS1 = Nothing
        myDT1 = Nothing
        myDT2 = Nothing

    End Sub

    Public Sub SearchDetail(ByVal Status As String)

        Dim vType As String = ""
        Dim vID As String = ""
        Dim vName As String = ""
        Dim FromDate As String = ""
        Dim ToDate As String = ""

        'Check From Date
        If mySetting.CheckTextNull(txtADateFrom.Text) = False Then
            ssql = "Select dbo.fn_DateAdd('Day',-60,'" & Session("CurrentDate") & "')"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables(0).Rows.Count > 0 Then
                FromDate = myDS.Tables(0).Rows(0).Item(0).ToString
            End If
            myDS = Nothing
        Else
            FromDate = mySetting.ConvertDateToDecimal(txtADateFrom.Text, Session("Company"), Session("Module"))
            If Len(FromDate) <> 14 Then
                lblresult.Text = "Incorrect date format of field " & lblADateFrom.Text & "!"
                Exit Sub
            End If
        End If

        If Status = "PENDING" Then
            FromDate = "19000101000000"
        End If

        'Check To Date
        If mySetting.CheckTextNull(txtADateTo.Text) = False Then
            ToDate = "19000101000000"
        Else
            If mySetting.CheckTextNull(txtADateFrom.Text) = False Then
                ToDate = "19000101000000"
            Else
                ToDate = mySetting.ConvertDateToDecimal(txtADateTo.Text, Session("Company"), Session("Module"))
                If Len(ToDate) <> 14 Then
                    lblresult.Text = "Incorrect date format of field " & lblADateTo.Text & "!"
                    Exit Sub
                End If
            End If
        End If

        'Check Date Order
        If FromDate > ToDate = True And ToDate <> "19000101000000" Then
            lblresult.Text = "Field " & lblADateFrom.Text & " should not be greater than " & lblADateTo.Text
            Exit Sub
        End If

        'Get filter value
        If chkAEmployee.Checked = True Then
            vType = "EMPID"

            If txtAID.Text <> "" Then
                'ssql = "Select Employee_Profile_ID From Employee_CodeName_Vw Where Company_Profile_Code = '" & Session("Company") & _
                '       "' And Code = '" & txtAID.Text & "'"
                'myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                'If myDS.Tables(0).Rows.Count > 0 Then
                '    vID = myDS.Tables(0).Rows(0).Item(0).ToString & "%"
                'End If
                'myDS = Nothing
                vID = txtAID.Text
            Else
                vID = ""
            End If

            If txtAName.Text <> "" Then
                If txtAID.Text = "" Then
                    'ssql = "Select Employee_Profile_ID From Employee_CodeName_Vw Where Company_Profile_Code = '" & Session("Company") & _
                    '       "' And Name = '" & txtAName.Text & "'"
                    'myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                    'If myDS.Tables(0).Rows.Count > 0 Then
                    '    vID = myDS.Tables(0).Rows(0).Item(0).ToString & "%"
                    'End If
                    'myDS = Nothing
                    vName = txtANAME.Text
                Else
                    vName = txtANAME.Text
                End If
            Else
                vName = ""
            End If

            If vName = "" And vID = "" Then
                vID = "%"
            End If

        Else
            vType = ddlOption_Type.SelectedValue

            If txtAID.Text <> "" Then
                'ssql = "Select ID From Organisation_Code_Profile_Vw Where Company_Profile_Code = '" & Session("Company") & _
                '       "' And Option_Type = '" & ddlOption_Type.SelectedValue & "' And CodeName = '" & txtAID.Text & "'"
                'myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                'If myDS.Tables(0).Rows.Count > 0 Then
                '    vID = myDS.Tables(0).Rows(0).Item(0).ToString & "%"
                'End If
                'myDS = Nothing
                vID = txtAID.Text.Replace("[", "%[").Replace("]", "]%")

            Else
                vID = "%"
            End If

            vName = ""

        End If

        If lnkBtnClosePending.Visible = True Or lnkBtnCloseApproved.Visible = True Then

            ssql = "Exec sp_md_MedicalApproval '" & Session("Company") & "','" & Session("EmpID") & "','','" & Status & _
                   "','" & vID & "','" & vName & "','" & FromDate & "','" & ToDate & "','" & vType & "','" & myGridView1.PageSize & "','" & Session("currentPage1") & "'"
            RetrieveDetail()

        ElseIf lnkBtnCloseReject.Visible = True Then

            If optRLDetails.Checked = True Then
                ssql = "Exec sp_md_MedicalApproval '" & Session("Company") & "','" & Session("EmpID") & "','','" & Status & _
                       "','" & vID & "','" & vName & "','" & FromDate & "','" & ToDate & "','" & vType & "','" & myGridView1.PageSize & "','" & Session("currentPage1") & "'"
                RetrieveDetail()
            Else
                ssql = "Exec sp_ls_calQualifiedRejectLeave '" & Session("Company") & "'," _
                      & ddlYear.Text & ",'" & Session("EmpID") & "','" & vID & "','" & vName & "','" & vType & "','" & myGridView1.PageSize & "','" & Session("currentPage1") & "'"
                RetrieveDetail()
            End If

        End If

    End Sub

    Private Sub UpdPendingMedicalToAppMedical()

        For Me.i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(0).Controls(1)
            Dim chkCursor1 As CheckBox = myGridview2.Rows(i).Cells(0).Controls(1)
            Dim row As GridViewRow = TryCast(chkCursor.NamingContainer, GridViewRow)
            Dim row1 As GridViewRow = TryCast(chkCursor1.NamingContainer, GridViewRow)
            Dim gv As GridView = TryCast(row.FindControl("gvOrders"), GridView)
            Dim gv1 As GridView = TryCast(row1.FindControl("gvOrders2"), GridView)
            'If chkCursor.Checked = True Then
            For Each gvRow As GridViewRow In gv.Rows
                Dim chkDA1 As Boolean = TryCast(gvRow.FindControl("chkDA1"), RadioButton).Checked
                Dim gvRow2 As GridViewRow = TryCast(gv1.Rows(gvRow.RowIndex).Cells(0).Controls(1).NamingContainer, GridViewRow)
                Dim gvSub As GridView = TryCast(gvRow.FindControl("gvSubOrders"), GridView)
                Dim gvSub1 As GridView = TryCast(gvRow2.FindControl("gvSubOrders2"), GridView)
                For Each gvSubRow As GridViewRow In gvSub.Rows
                    Dim chkSDA1 As RadioButton = TryCast(gvSubRow.FindControl("chkSDA1"), RadioButton)
                    'Dim CommentMessage As TextBox = TryCast(gvSubRow.FindControl("grvlblComment"), TextBox)

                    If chkSDA1.Checked Then

                        strEmpid = Trim(gvSub1.Rows(gvSubRow.RowIndex).Cells(2).Text)
                        strYear = Trim(gvSub1.Rows(gvSubRow.RowIndex).Cells(3).Text)
                        strMasterskill = Trim(gvSub1.Rows(gvSubRow.RowIndex).Cells(15).Text)
                        strDate = Trim(gvSub1.Rows(gvSubRow.RowIndex).Cells(16).Text)

                        ssql = "Exec sp_KPI_updSkillApproval '" & Session("Company") & "','" _
                               & strEmpid & "','" & strYear & "','" _
                               & strMasterskill & "','" & strDate & "','" & Session("EmpID") & "','" & "PASS" & "'"
                        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                        myDS = Nothing

                        chkSDA1.Checked = False
                    End If
                Next
                chkDA1 = False
            Next
            'chkCursor.Checked = False
            'End If
            chkCursor = Nothing
        Next

    End Sub

    Private Sub UpdPendingMedicalToReject()

        For Me.i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(0).Controls(1)
            Dim chkCursor1 As CheckBox = myGridview2.Rows(i).Cells(0).Controls(1)
            Dim row As GridViewRow = TryCast(chkCursor.NamingContainer, GridViewRow)
            Dim row1 As GridViewRow = TryCast(chkCursor1.NamingContainer, GridViewRow)
            Dim gv As GridView = TryCast(row.FindControl("gvOrders"), GridView)
            Dim gv1 As GridView = TryCast(row1.FindControl("gvOrders2"), GridView)
            'If chkCursor.Checked = True Then
            For Each gvRow As GridViewRow In gv.Rows
                Dim chkRA1 As RadioButton = TryCast(gvRow.FindControl("chkDR1"), RadioButton)
                Dim gvRow2 As GridViewRow = TryCast(gv1.Rows(gvRow.RowIndex).Cells(0).Controls(1).NamingContainer, GridViewRow)
                Dim gvSub As GridView = TryCast(gvRow.FindControl("gvSubOrders"), GridView)
                Dim gvSub1 As GridView = TryCast(gvRow2.FindControl("gvSubOrders2"), GridView)
                For Each gvSubRow As GridViewRow In gvSub.Rows
                    Dim chkSDA1 As RadioButton = TryCast(gvSubRow.FindControl("chkSDR1"), RadioButton)
                    'Dim CommentMessage As TextBox = TryCast(gvSubRow.FindControl("grvlblComment"), TextBox)

                    If chkSDA1.Checked Then

                        strEmpid = Trim(gvSub1.Rows(gvSubRow.RowIndex).Cells(2).Text)
                        strYear = Trim(gvSub1.Rows(gvSubRow.RowIndex).Cells(3).Text)
                        strMasterskill = Trim(gvSub1.Rows(gvSubRow.RowIndex).Cells(15).Text)
                        strDate = Trim(gvSub1.Rows(gvSubRow.RowIndex).Cells(16).Text)

                        ssql = "Exec sp_KPI_updSkillApproval '" & Session("Company") & "','" _
                               & strEmpid & "','" & strYear & "','" _
                               & strMasterskill & "','" & strDate & "','" & Session("EmpID") & "','" & "REJECT" & "'"
                        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                        myDS = Nothing

                        chkSDA1.Checked = False
                    End If
                Next
                chkRA1.Checked = False
            Next
            'chkCursor.Checked = False
            'End If
            chkCursor = Nothing
        Next
    End Sub

    Private Sub UpdApprovedMedical()

        For Me.i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(0).Controls(1)
            Dim chkCursor1 As CheckBox = myGridview2.Rows(i).Cells(0).Controls(1)
            Dim row As GridViewRow = TryCast(chkCursor.NamingContainer, GridViewRow)
            Dim row1 As GridViewRow = TryCast(chkCursor1.NamingContainer, GridViewRow)
            Dim gv As GridView = TryCast(row.FindControl("gvOrders"), GridView)
            Dim gv1 As GridView = TryCast(row1.FindControl("gvOrders2"), GridView)
            'If chkCursor.Checked = True Then
            For Each gvRow As GridViewRow In gv.Rows
                Dim chkDA1 As RadioButton = TryCast(gvRow.FindControl("chkDR1"), RadioButton)
                Dim gvRow2 As GridViewRow = TryCast(gv1.Rows(gvRow.RowIndex).Cells(0).Controls(1).NamingContainer, GridViewRow)
                Dim gvSub As GridView = TryCast(gvRow.FindControl("gvSubOrders"), GridView)
                Dim gvSub1 As GridView = TryCast(gvRow2.FindControl("gvSubOrders2"), GridView)
                For Each gvSubRow As GridViewRow In gvSub.Rows
                    Dim chkSDA1 As RadioButton = TryCast(gvSubRow.FindControl("chkSDR1"), RadioButton)
                    'Dim CommentMessage As TextBox = TryCast(gvSubRow.FindControl("grvlblComment"), TextBox)
                    If chkSDA1.Checked Then

                        strEmpid = Trim(gvSub1.Rows(gvSubRow.RowIndex).Cells(2).Text)
                        strYear = Trim(gvSub1.Rows(gvSubRow.RowIndex).Cells(3).Text)
                        strMasterskill = Trim(gvSub1.Rows(gvSubRow.RowIndex).Cells(15).Text)
                        strDate = Trim(gvSub1.Rows(gvSubRow.RowIndex).Cells(16).Text)
                        
                        ssql = "Exec sp_KPI_updSkillApproval '" & Session("Company") & "','" _
                               & strEmpid & "','" & strYear & "','" _
                               & strMasterskill & "','" & strDate & "','" & Session("EmpID") & "','" & "REJECT" & "'"
                        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                        myDS = Nothing

                        chkSDA1.Checked = False
                    End If
                Next
                chkDA1.Checked = False
                chkDA1 = Nothing
            Next
            'chkCursor.Checked = False
            'End If
            chkCursor = Nothing
        Next

    End Sub

    Private Function chkEachDateApplication() As Boolean

        Try
            Err1 = ""
            Err2 = ""
            Err3 = ""
            Err4 = ""
            Err5 = ""
            Err6 = ""
            vDate1 = ""
            vDate2 = ""
            vDate3 = ""
            vDate4 = ""
            vDate5 = ""
            vDate6 = ""
            Message = ""

            chkEachDateApplication = False
            'EmpID = Session("curLeaveEmpID")
            'CurrentDate = Session("CurSysDate")

            Action = "ADD"
            'DateApplyFor = Trim(lstDateApply.Items(vCounter).ToString)

            ssql = "Exec sp_ls_chkLeaveApplication '" & Session("Company") & "','" _
                   & EmpID & "','" & Session("CurLeaveID") & "','" & CurrentDate & "','" _
                   & DateApplyFor & "','" & Day & "','" _
                   & Period & "','" & TotalDay & "',0,''"
            myDS1 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

            If Trim(myDS1.Tables(0).Rows(0).Item(0).ToString) <> "" Then
                If Trim(myDS1.Tables(0).Rows(0).Item(0).ToString) = "5" Then
                    ssql1 = "Select dbo.fn_DisplayDate('" & DateApplyFor & "','" & Session("Company") & "','" & Session("Module") & "')"
                    myDS3 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
                    vDate1 = vDate1 & " " & myDS3.Tables(0).Rows(0).Item(0).ToString()
                    myDS3 = Nothing
                    'vDate1 = DateApplyFor & " " & vDate1

                ElseIf Trim(myDS1.Tables(0).Rows(0).Item(0).ToString) = "4" Then
                    'If tabCalendar.Tab = 1 Then
                    '    lstDateApply.Items.Remove(vCounter)
                    'Else
                    '    vDate2 = DateApplyFor & " " & vDate2
                    '    lblresut.Text = "Following day(s) is not allow to apply leave"
                    'End If
                    'lstDateApply.Items.Remove(vCounter)
                ElseIf Trim(myDS1.Tables(0).Rows(0).Item(0).ToString) = "6" Then
                    lblresult.Text = "Date Apply Must Less Than Or Equal to Current Date"
                    Return False

                ElseIf Trim(myDS1.Tables(0).Rows(0).Item(0).ToString) = "1" Then
                    ssql1 = "Select dbo.fn_DisplayDate('" & DateApplyFor & "','" & Session("Company") & "','" & Session("Module") & "')"
                    myDS3 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
                    vDate4 = vDate4 & " " & myDS3.Tables(0).Rows(0).Item(0).ToString()
                    myDS3 = Nothing
                    'vDate4 = DateApplyFor & " " & vDate4

                ElseIf Trim(myDS1.Tables(0).Rows(0).Item(0).ToString) = "2" Then
                    ssql1 = "Select dbo.fn_DisplayDate('" & DateApplyFor & "','" & Session("Company") & "','" & Session("Module") & "')"
                    myDS3 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
                    vDate5 = vDate5 & " " & myDS3.Tables(0).Rows(0).Item(0).ToString()
                    myDS3 = Nothing
                    'vDate5 = DateApplyFor & " " & vDate5

                ElseIf Trim(myDS1.Tables(0).Rows(0).Item(0).ToString) = "3" Then
                    ssql1 = "Select dbo.fn_DisplayDate('" & DateApplyFor & "','" & Session("Company") & "','" & Session("Module") & "')"
                    myDS3 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
                    vDate6 = vDate6 & " " & myDS3.Tables(0).Rows(0).Item(0).ToString()
                    myDS3 = Nothing
                    'vDate6 = DateApplyFor & " " & vDate6

                ElseIf Trim(myDS1.Tables(0).Rows(0).Item(0).ToString) = "7" Then
                    lblresult.Text = "This leave (" & Session("CurLeaveName") & ") can not be apply more than one time in a year!"
                    Return False

                ElseIf Trim(myDS1.Tables(0).Rows(0).Item(0).ToString) = "8" Then
                    lblresult.Text = "Days apply exceed maximum limit that allow in one time!"
                    Return False

                ElseIf Trim(myDS1.Tables(0).Rows(0).Item(0).ToString) = "9" Then
                    lblresult.Text = "Balance Is Not Available!"
                    Return False

                End If
            Else
                ssql = "Exec sp_sa_chkTransactionLockDate '" & Session("Company") & "','" _
                       & EmpID & "','" & DateApplyFor & "'," & 1 & "," & 0
                myDS2 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                If Trim(myDS2.Tables(0).Rows(0).Item(0).ToString) <> "" Then
                    vDate3 = DateApplyFor & " " & vDate3
                End If
                myDS2 = Nothing
            End If
            myDS1 = Nothing


            If vDate1 <> "" Then   ' Duplicate Record
                Err1 = "The following date not available! (" & vDate1 & ")"
            End If

            If vDate2 <> "" Then   ' Not Allow to apply Leave
                Err2 = "Following day(s) is not allow to apply leave! (" & vDate2 & ") "
            End If

            If vDate3 <> "" Then   ' Lock Date
                Err3 = "The following date already locked! (" & vDate3 & ") "
            End If

            If vDate4 <> "" Then   ' Total Day Out Of Range
                Err4 = "Total Day Out Of Range! (" & vDate4 & ") "
            End If

            If vDate5 <> "" Then   ' Inter Department Transfer Record Not Found
                Err5 = "Inter Department Transfer Record Not Found! (" & vDate5 & ") "
            End If

            If vDate6 <> "" Then   ' CalendarID Not Found
                Err6 = "CalendarID Not Found! (" & vDate6 & ")"
            End If

            If Err1 <> "" Or Err2 <> "" Or Err3 <> "" Or Err4 <> "" Or Err5 <> "" Or Err6 <> "" Then
                If Err1 <> "" Then Message = Chr(13) + Chr(13) + Err1
                If Err2 <> "" Then Message = Message + Chr(13) + Chr(13) + Err2
                If Err3 <> "" Then Message = Message + Chr(13) + Chr(13) + Err3
                If Err4 <> "" Then Message = Message + Chr(13) + Chr(13) + Err4
                If Err5 <> "" Then Message = Message + Chr(13) + Chr(13) + Err5
                If Err6 <> "" Then Message = Message + Chr(13) + Chr(13) + Err6

                lblresult.Text = Message
                Action = "CANCEL"
                'tabCalendar.Tab = 0
                Return False
            End If

            Return True

        Catch ex As Exception
            lblresult.Text = "[chkEachDateApplication]Error: " & ex.Message
        End Try

    End Function

    Private Function chkLeaveSequence() As Boolean

        Dim CurrentLeaveID As String
        Dim LeaveIDSequence As Integer

        LeaveID = Leave
        StopChecking = False

        'sp1
        Try
            ssql = "Exec sp_ls_selLeaveSequence '" & Session("Company") & "','" & LeaveID & "'"
            myDS1 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If Trim(myDS1.Tables(0).Rows.Count) > 0 Then
                CountSequence = myDS1.Tables(0).Rows(0).Item(0).ToString
                LeaveIDSequence = myDS1.Tables(0).Rows(0).Item(1).ToString
            End If
            myDS1 = Nothing
        Catch ex As Exception
            lblresult.Text = "[chkLeaveSequence:sp1]Error: " & ex.Message
        End Try

        chkLeaveSequence = False
        If CountSequence > 0 Then

            If ContinueLoop = True Then
                vCount = ContinueCount
            Else
                vCount = 0
            End If

            Do Until vCount = CountSequence
                'sp2
                Try
                    ssql = "Exec sp_ls_chkLeaveSequence '" & Session("Company") & "','" _
                    & EmpID & "','" & LeaveID & "','" _
                    & CurrentDate & "','" & DateApplyFor & "','" _
                    & Day & "','" & Period & "'," & TotalDay & ",'" _
                    & "0" & "','" & StopChecking & "'," & vCount & ",''"

                    myDS1 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                    If Trim(myDS1.Tables(0).Rows.Count) > 0 Then
                        LeaveID = myDS1.Tables(0).Rows(0).Item(1).ToString
                        CurrentLeaveID = LeaveID
                        mstBalance = myDS1.Tables(0).Rows(0).Item(2).ToString

                        If myDS1.Tables(0).Rows(0).Item(0).ToString = "Insert" And CurrentLeaveID <> PrevLeaveID And _
                            Leave <> CurrentLeaveID And LeaveIDSequence < vCount Then
                            lblresult.Text = "The leave you apply is not available!"
                            myDS1 = Nothing
                            Exit Function

                        ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "Insert" And CurrentLeaveID <> PrevLeaveID Then
                            myDS1 = Nothing
                            Exit Function

                        ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "1" Then
                            lblresult.Text = "Balance Is Not Available!"
                            myDS1 = Nothing
                            Exit Function

                        ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "2" And CurrentLeaveID <> PrevLeaveID Then
                            lblresult.Text = "The leave you apply is not available!"
                            myDS1 = Nothing
                            Exit Function

                        ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "2" And CurrentLeaveID = PrevLeaveID _
                                And myDS1.Tables(0).Rows(0).Item(2).ToString = "False" And vCount = CountSequence - 1 Then
                            lblresult.Text = "Leave Balance Is Not Available!"
                            myDS1 = Nothing
                            Exit Function

                        ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "2" And CurrentLeaveID = PrevLeaveID _
                                And myDS1.Tables(0).Rows(0).Item(2).ToString = "True" Then
                            chkLeaveSequence = True
                            Action = "ADD"
                            myDS1 = Nothing
                            Exit Function

                        ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "3" Then
                            lblresult.Text = "Leave Balance Is Not Available!"
                            myDS1 = Nothing
                            Exit Function

                        ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "4" Then
                            lblresult.Text = "Leave Balance Is Not Available!"
                            myDS1 = Nothing
                            Exit Function

                        ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "0" Or (myDS1.Tables(0).Rows(0).Item(0).ToString = "2" And CurrentLeaveID = PrevLeaveID _
                                And myDS1.Tables(0).Rows(0).Item(0).ToString = "False") Then
                            LeaveID = Leave
                        End If
                    End If

                Catch ex As Exception
                    lblresult.Text = "[chkLeaveSequence:sp2]Error: " & ex.Message
                End Try

                vCount = vCount + 1
            Loop

        Else
            'sp3
            Try
                ssql = "Exec sp_ls_chkNoLeaveSequence '" & Session("Company") & "','" _
                & EmpID & "','" & LeaveID & "','" _
                & CurrentDate & "','" & DateApplyFor & "','" & Day & "','" _
                & "0" & "','" & StopChecking & "','',''"

                myDS1 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                If Trim(myDS1.Tables.Count) > 0 Then
                    'move inside
                    LeaveID = myDS1.Tables(0).Rows(0).Item(1).ToString
                    If myDS1.Tables(0).Rows(0).Item(0).ToString = "1" Or myDS1.Tables(0).Rows(0).Item(0).ToString = "3" Then
                        lblresult.Text = "Leave Balance Is Not Available!"
                        myDS1 = Nothing
                        Exit Function
                    ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "2" Then
                        lblresult.Text = "Leave Balance Is Not Available!"
                        myDS1 = Nothing
                        Exit Function

                    ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "0" Then
                        myDS1 = Nothing
                        Exit Function
                    End If
                End If

            Catch ex As Exception
                lblresult.Text = "[chkLeaveSequence:sp3]Error: " & ex.Message
            End Try

        End If
        myDS1 = Nothing
        chkLeaveSequence = True

    End Function

#End Region

#Region "Sub & Function 2"


    Public Sub ClearField()

        txtAID.Text = ""
        txtAName.Text = ""
        txtADateFrom.Text = ""
        txtADateTo.Text = ""
        lblresult.Text = ""
        chkAAll.Checked = False
        chkRAll.Checked = False

    End Sub

    Public Sub OptionCheckedChange1()
        If chkAEmployee.Checked = True Then
            lblAID.Text = "Employee Code"
            lblAName.Text = "Employee Name"
        ElseIf chkADepartment.Checked = True Then
            lblAID.Text = "Division"
            lblAName.Text = "Department"
        End If
    End Sub

    Public Sub OptionCheckedChange2()
        If chkAAll.Checked = True Then
            CheckAll_Approve()
        Else
            UnCheckAll_Approve()
        End If
        If chkRAll.Checked = True Then
            CheckAll_Reject()
        Else
            UnCheckAll_Reject()
        End If
    End Sub

    Public Sub NavigationLink1_Click(ByVal sender As Object, ByVal e As CommandEventArgs)

        Select Case e.CommandName
            Case "First"
                Session("currentpage1") = 1
            Case "Prev"
                Session("currentpage1") = Integer.Parse(CurrentPage1.Text) - 1
            Case "Next"
                Session("currentpage1") = Integer.Parse(CurrentPage1.Text) + 1
            Case "Last"
                Session("currentpage1") = Integer.Parse(TotalPages1.Text)
        End Select
        lblresult.Text = ""
        If Session("action") = "search" Then
            SearchDetail(Session("TabStatus"))
        Else
            SubLoadSSQL(Session("TabStatus"))
        End If

        RetrieveDetail()

    End Sub

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim customerId As String = myGridView1.DataKeys(e.Row.RowIndex).Value.ToString()
            Dim gvOrders As GridView = TryCast(e.Row.FindControl("gvOrders"), GridView)
            Dim filterExp As String = "Item = '" & customerId & "'"
            gvOrders.ToolTip = customerId
            Dim abd As DataTable
            abd = myTB1.Clone()
            Dim drarray() As DataRow
            drarray = myTB1.Select(filterExp)
            Dim i As Integer
            For i = 0 To (drarray.Length - 1)
                abd.ImportRow(drarray(i))
            Next

            gvOrders.DataSource = abd
            gvOrders.DataBind()

        End If
    End Sub

    Protected Sub OnSubRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim customerId As String = e.Row.Cells(10).Text.Trim()
            Dim SubItem As String = e.Row.Cells(11).Text.Trim()
            subcustomerID2 = customerId
            SubItem2 = SubItem
            Dim gvSubOrders As GridView = TryCast(e.Row.FindControl("gvSubOrders"), GridView)
            Dim filterExp As String = "SubItem = '" & SubItem & "' and Item = '" & customerId & "'"
            Dim abd As DataTable
            abd = myTB3.Clone()
            Dim drarray() As DataRow
            drarray = myTB3.Select(filterExp)
            Dim i As Integer
            For i = 0 To (drarray.Length - 1)
                abd.ImportRow(drarray(i))
            Next

            gvSubOrders.DataSource = abd
            gvSubOrders.DataBind()

        End If
    End Sub
    Protected Sub OnSub2RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim customerId As String = subcustomerID2
            Dim SubItem As String = SubItem2
            Dim filterExp As String = "SubItem = '" & SubItem & "' and Item = '" & customerId & "'"
            Dim abd As DataTable
            abd = myTB3.Clone()
            Dim drarray() As DataRow
            drarray = myTB3.Select(filterExp)
            Dim i As Integer
            For i = 0 To (drarray.Length - 1)
                abd.ImportRow(drarray(i))
            Next
            Dim drarray2 As DataRow
            drarray2 = abd.Rows(e.Row.RowIndex)
        End If
    End Sub



    Protected Sub chkA1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chkAll As CheckBox = TryCast(sender, CheckBox)
        Dim row As GridViewRow = TryCast(chkAll.NamingContainer, GridViewRow)
        If chkAll.Checked = True Then
            TryCast(row.FindControl("chkR1"), CheckBox).Checked = False
        End If
        Dim gv As GridView = TryCast(row.FindControl("gvOrders"), GridView)
        For Each gvRow As GridViewRow In gv.Rows
            TryCast(gvRow.FindControl("chkDA1"), RadioButton).Checked = chkAll.Checked
            TryCast(gvRow.FindControl("chkDR1"), RadioButton).Checked = False
            Dim gv2 As GridView = TryCast(gvRow.FindControl("gvSubOrders"), GridView)
            For Each gvRow2 As GridViewRow In gv2.Rows
                TryCast(gvRow2.FindControl("chkSDA1"), RadioButton).Checked = chkAll.Checked
                TryCast(gvRow2.FindControl("chkSDR1"), RadioButton).Checked = False
            Next
        Next
    End Sub


    Protected Sub chkDA1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chkAll As CheckBox = TryCast(sender, CheckBox)
        Dim row As GridViewRow = TryCast(chkAll.NamingContainer, GridViewRow)
        If chkAll.Checked = True Then
            TryCast(row.FindControl("chkDR1"), CheckBox).Checked = False
        End If
        Dim gv As GridView = TryCast(row.FindControl("gvSubOrders"), GridView)
        For Each gvRow As GridViewRow In gv.Rows
            TryCast(gvRow.FindControl("chkSDA1"), RadioButton).Checked = chkAll.Checked
            TryCast(gvRow.FindControl("chkSDR1"), RadioButton).Checked = False
        Next
    End Sub

    Protected Sub chkR1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chkAll As CheckBox = TryCast(sender, CheckBox)
        Dim row As GridViewRow = TryCast(chkAll.NamingContainer, GridViewRow)
        If chkAll.Checked = True Then
            TryCast(row.FindControl("chkA1"), CheckBox).Checked = False
        End If
        Dim gv As GridView = TryCast(row.FindControl("gvOrders"), GridView)
        For Each gvRow As GridViewRow In gv.Rows
            TryCast(gvRow.FindControl("chkDA1"), RadioButton).Checked = False
            TryCast(gvRow.FindControl("chkDR1"), RadioButton).Checked = chkAll.Checked
            Dim gv2 As GridView = TryCast(gvRow.FindControl("gvSubOrders"), GridView)
            For Each gvRow2 As GridViewRow In gv2.Rows
                TryCast(gvRow2.FindControl("chkSDR1"), RadioButton).Checked = chkAll.Checked
                'TryCast(gvRow2.FindControl("grvlblComment"), TextBox).Text = hComment.Value
                TryCast(gvRow2.FindControl("chkSDA1"), RadioButton).Checked = False
            Next
        Next
    End Sub

    Protected Sub chkDR1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chkAll As CheckBox = TryCast(sender, CheckBox)
        Dim row As GridViewRow = TryCast(chkAll.NamingContainer, GridViewRow)
        If chkAll.Checked = True Then
            TryCast(row.FindControl("chkDA1"), CheckBox).Checked = False
        End If
        Dim gv As GridView = TryCast(row.FindControl("gvSubOrders"), GridView)
        For Each gvRow As GridViewRow In gv.Rows
            TryCast(gvRow.FindControl("chkSDA1"), RadioButton).Checked = False
            'TryCast(gvRow.FindControl("grvlblComment"), TextBox).Text = hComment.Value
            TryCast(gvRow.FindControl("chkSDR1"), RadioButton).Checked = chkAll.Checked
        Next
    End Sub

    Protected Sub OnRowDataBound2(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim customerId As String = myGridview2.DataKeys(e.Row.RowIndex).Value.ToString()
            Dim gvOrders2 As GridView = TryCast(e.Row.FindControl("gvOrders2"), GridView)
            Dim filterExp As String = "Item = '" & customerId & "'"
            Dim abd As DataTable
            abd = myTB2.Clone()
            Dim drarray() As DataRow
            drarray = myTB2.Select(filterExp)
            Dim i As Integer
            For i = 0 To (drarray.Length - 1)
                abd.ImportRow(drarray(i))
            Next

            gvOrders2.DataSource = abd
            gvOrders2.DataBind()
        End If
    End Sub

    Protected Sub OnSubRowDataBound2(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim customerId As String = e.Row.Cells(10).Text.Trim()
            Dim SubItem As String = e.Row.Cells(11).Text.Trim()
            Dim gvSubOrders As GridView = TryCast(e.Row.FindControl("gvSubOrders2"), GridView)
            Dim filterExp As String = "SubItem = '" & SubItem & "' and Item = '" & customerId & "'"
            Dim abd As DataTable
            abd = myTB4.Clone()
            Dim drarray() As DataRow
            drarray = myTB4.Select(filterExp)
            Dim i As Integer
            For i = 0 To (drarray.Length - 1)
                abd.ImportRow(drarray(i))
            Next

            gvSubOrders.DataSource = abd
            gvSubOrders.DataBind()

        End If
    End Sub

    Protected Sub imgBtnGoToPage1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnGoToPage1.Click

        If txtGoToPage1.Text = "" Then
            lblresult.Text = "Request failed! Value required..."
            txtGoToPage1.Focus()
            Exit Sub
        End If

        Try
            Dim pagestr As String = Trim(txtGoToPage1.Text)
            Dim pagenum As Integer = CInt(pagestr)
            Dim firstnum As Integer = 0
            Dim lastnum As Integer = 0

            If pagenum <= CInt(Session("TotalPages1")) And pagenum > 0 Then
                If pagenum = CInt(Session("currentPage1")) Then
                    lblresult.Text = "Request failed! You are looking for the same page..."
                    txtGoToPage1.Focus()
                Else
                    lblresult.Text = ""
                    Session("currentpage1") = pagenum
                    SubLoadSSQL(Session("TabStatus"))
                    RetrieveDetail()
                End If
                txtGoToPage1.Text = ""
            Else
                If pagenum = 1 Or pagenum > CInt(Session("TotalPages1")) And CInt(Session("TotalPages1")) = 1 Then
                    lblresult.Text = "Request failed! Only one page available..."
                Else
                    If CInt(Session("currentPage1")) = 1 Then
                        firstnum = 1
                    Else
                        firstnum = 0
                    End If
                    If CInt(Session("currentPage1")) = CInt(Session("TotalPages1")) Then
                        lastnum = CInt(Session("TotalPages1"))
                    Else
                        lastnum = CInt(Session("TotalPages1")) + 1
                    End If
                    lblresult.Text = "Request failed! Page number must between " & firstnum & " and " & lastnum & "..."
                End If
                txtGoToPage1.Text = ""
                txtGoToPage1.Focus()
            End If

        Catch ex As Exception
            lblresult.Text = "Request failed! Invalid value enter..."
            txtGoToPage1.Text = ""
            txtGoToPage1.Focus()
            Exit Sub
        End Try

    End Sub

    Private Sub lnkBtnViewPending_Click()

        lnkBtnViewPending.Visible = False
        lnkBtnClosePending.Visible = True
        pnlpart1.Visible = True

        lnkBtnViewApproved.Visible = True
        lnkBtnCloseApproved.Visible = False
        pnlpart2.Visible = True

        lnkBtnViewReject.Visible = True
        lnkBtnCloseReject.Visible = False
        pnlpart3.Visible = False

        Session("currentpage1") = "1"
        Session("TabStatus") = "PENDING"
        Session("action") = Session("TabStatus")
        SubLoadSSQL(Session("TabStatus"))
        RetrieveDetail()
        ClearField()
        lstEmpID.Items.Clear()
        lstLeaveApply.Items.Clear()
        lstLeaveTotal.Items.Clear()

    End Sub

    Private Sub lnkBtnClosePending_Click()
        lnkBtnViewPending.Visible = True
        lnkBtnClosePending.Visible = False
        pnlpart1.Visible = False
        pnlbutton.Visible = False
    End Sub

    Private Sub lnkBtnViewApproved_Click()

        lnkBtnViewApproved.Visible = False
        lnkBtnCloseApproved.Visible = True
        pnlpart2.Visible = True

        lnkBtnViewPending.Visible = True
        lnkBtnClosePending.Visible = False
        pnlpart1.Visible = True

        lnkBtnViewReject.Visible = True
        lnkBtnCloseReject.Visible = False
        pnlpart3.Visible = False

        Session("currentpage1") = "1"
        Session("TabStatus") = "PASS"
        Session("action") = Session("TabStatus")
        SubLoadSSQL(Session("TabStatus"))
        RetrieveDetail()
        ClearField()

    End Sub

    Private Sub lnkBtnCloseApproved_Click()
        lnkBtnViewApproved.Visible = True
        lnkBtnCloseApproved.Visible = False
        pnlpart2.Visible = False
        pnlpart1.Visible = False
        pnlbutton.Visible = False
    End Sub

    Private Sub lnkBtnViewReject_Click()

        lnkBtnViewReject.Visible = False
        lnkBtnCloseReject.Visible = True
        pnlpart3.Visible = True

        lnkBtnViewPending.Visible = True
        lnkBtnClosePending.Visible = False
        pnlpart1.Visible = True

        lnkBtnViewApproved.Visible = True
        lnkBtnCloseApproved.Visible = False
        pnlpart2.Visible = True

        Session("currentpage1") = "1"
        Session("TabStatus") = "REJECT"
        Session("action") = Session("TabStatus")
        SubLoadSSQL(Session("TabStatus"))
        RetrieveDetail()
        ClearField()

        optRLDetails.Checked = True
        optQFRLeave.Checked = False
        ddlYear.Enabled = False

        'button setting
        imgBtnUpdate.Enabled = False
        mySetting.GetBtnImgUrl(imgBtnUpdate, Session("Company").ToString, btnColourAlt, "btnUpdate.png")

    End Sub

    Private Sub lnkBtnCloseReject_Click()
        lnkBtnViewReject.Visible = True
        lnkBtnCloseReject.Visible = False
        pnlpart3.Visible = False
        pnlpart2.Visible = False
        pnlpart1.Visible = False
        pnlbutton.Visible = False
    End Sub

    Private Sub lnkBtnCloseBalance_Click()
        lnkBtnViewBalance.Visible = True
        lnkBtnCloseBalance.Visible = False
        pnlBalance.Visible = False
    End Sub
    Private Sub lnkBtnViewBalance_Click()
        lnkBtnViewBalance.Visible = False
        lnkBtnCloseBalance.Visible = True
        pnlBalance.Visible = True
    End Sub

    Private Function CheckForSGv1() As Integer

        Dim v_num As Integer = 0

        Try
            For Each grdItem In myGridView1.Rows
                chkSelected = grdItem.FindControl("chkS1")
                If chkSelected.Checked = True Then
                    v_num += 1
                End If
            Next
            Return v_num
        Catch ex As Exception
            pnlresult.Visible = True
            lblresult.Text = ""
            lblresult.Text = "[CheckForSGv1]Error: " & ex.Message
        End Try

    End Function

    Private Function CheckForAGv1() As Integer

        Dim v_num As Integer = 0

        Try
            For Each grdItem In myGridView1.Rows
                chkSelected = grdItem.FindControl("chkA1")
                If chkSelected.Checked = True Then
                    v_num += 1
                End If
            Next
            Return v_num
        Catch ex As Exception
            pnlresult.Visible = True
            lblresult.Text = ""
            lblresult.Text = "[CheckForAGv1]Error: " & ex.Message
        End Try

    End Function

    Private Function CheckForRGv1() As Integer

        Dim v_num As Integer = 0

        Try
            For Each grdItem In myGridView1.Rows
                chkSelected = grdItem.FindControl("chkR1")
                If chkSelected.Checked = True Then
                    v_num += 1
                End If
            Next
            Return v_num
        Catch ex As Exception
            pnlresult.Visible = True
            lblresult.Text = ""
            lblresult.Text = "[CheckForRGv1]Error: " & ex.Message
        End Try

    End Function

    Private Function CheckForARGv1() As Integer

        Dim v_num As Integer = 0

        Try
            For Each grdItem In myGridView1.Rows
                chkSelected1 = grdItem.FindControl("chkA1")
                chkSelected2 = grdItem.FindControl("chkR1")
                If chkSelected1.Checked = True And chkSelected2.Checked = True Then
                    v_num += 1
                End If
            Next
            Return v_num
        Catch ex As Exception
            pnlresult.Visible = True
            lblresult.Text = ""
            lblresult.Text = "[CheckForARGv1]Error: " & ex.Message
        End Try

    End Function

    Private Sub ShowMessage(ByVal message As String)

        Dim scriptString As String = "<script language=JavaScript>"
        scriptString += "alert('" + message + "');"
        scriptString += "<" & "/script>"
        Page.ClientScript.RegisterStartupScript(GetType(Page), "ShowMessage", scriptString)

    End Sub

    Private Sub CheckAll_Approve()
        For Me.i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(0).Controls(1)
            chkCursor.Checked = True
            chkCursor = Nothing
        Next
    End Sub

    Private Sub UnCheckAll_Approve()
        For Me.i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(0).Controls(1)
            chkCursor.Checked = False
            chkCursor = Nothing
        Next
    End Sub

    Private Sub CheckAll_Reject()
        For Me.i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(1).Controls(1)
            chkCursor.Checked = True
            chkCursor = Nothing
        Next
    End Sub

    Private Sub UnCheckAll_Reject()
        For Me.i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(1).Controls(1)
            chkCursor.Checked = False
            chkCursor = Nothing
        Next
    End Sub

    Private Sub Enable_Approve()
        For Me.i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(0).Controls(1)
            Dim row As GridViewRow = TryCast(chkCursor.NamingContainer, GridViewRow)
            Dim gv As GridView = TryCast(row.FindControl("gvOrders"), GridView)
            For Each gvRow As GridViewRow In gv.Rows
                TryCast(gvRow.FindControl("chkDA1"), RadioButton).Enabled = True
                Dim gv2 As GridView = TryCast(gvRow.FindControl("gvSubOrders"), GridView)
                For Each gvRow2 As GridViewRow In gv2.Rows
                    TryCast(gvRow2.FindControl("chkSDA1"), RadioButton).Enabled = True
                Next
            Next
            chkCursor.Enabled = True

            chkCursor = Nothing
        Next
    End Sub

    Private Sub Disable_Approve()
        For Me.i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(0).Controls(1)
            Dim row As GridViewRow = TryCast(chkCursor.NamingContainer, GridViewRow)
            Dim gv As GridView = TryCast(row.FindControl("gvOrders"), GridView)
            For Each gvRow As GridViewRow In gv.Rows
                TryCast(gvRow.FindControl("chkDA1"), RadioButton).Enabled = False
                Dim gv2 As GridView = TryCast(gvRow.FindControl("gvSubOrders"), GridView)
                For Each gvRow2 As GridViewRow In gv2.Rows
                    TryCast(gvRow2.FindControl("chkSDA1"), RadioButton).Enabled = False
                Next
            Next
            chkCursor.Enabled = False
            chkCursor = Nothing
        Next
    End Sub

    Private Sub Enable_Reject()
        For Me.i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(1).Controls(1)
            Dim row As GridViewRow = TryCast(chkCursor.NamingContainer, GridViewRow)
            Dim gv As GridView = TryCast(row.FindControl("gvOrders"), GridView)
            For Each gvRow As GridViewRow In gv.Rows
                TryCast(gvRow.FindControl("chkDR1"), RadioButton).Enabled = True
                Dim gv2 As GridView = TryCast(gvRow.FindControl("gvSubOrders"), GridView)
                For Each gvRow2 As GridViewRow In gv2.Rows
                    TryCast(gvRow2.FindControl("chkSDR1"), RadioButton).Enabled = True
                Next
            Next
            chkCursor.Enabled = True
            chkCursor = Nothing
        Next
    End Sub

    Private Sub Disable_Reject()
        For Me.i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(1).Controls(1)
            Dim row As GridViewRow = TryCast(chkCursor.NamingContainer, GridViewRow)
            Dim gv As GridView = TryCast(row.FindControl("gvOrders"), GridView)
            For Each gvRow As GridViewRow In gv.Rows
                TryCast(gvRow.FindControl("chkDR1"), RadioButton).Enabled = False
                Dim gv2 As GridView = TryCast(gvRow.FindControl("gvSubOrders"), GridView)
                For Each gvRow2 As GridViewRow In gv2.Rows
                    TryCast(gvRow2.FindControl("chkSDR1"), RadioButton).Enabled = False
                Next
            Next
            chkCursor.Enabled = False
            chkCursor = Nothing
        Next
    End Sub

    Private Sub Visible_Approve()
        For Me.i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(0).Controls(1)
            chkCursor.Visible = True
            chkCursor = Nothing
        Next
    End Sub

    Private Sub Invisible_Approve()
        For Me.i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(0).Controls(1)
            chkCursor.Visible = False
            chkCursor = Nothing
        Next
    End Sub

    Private Sub Visible_Reject()
        For Me.i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(1).Controls(1)
            chkCursor.Visible = True
            chkCursor = Nothing
        Next
    End Sub

    Private Sub Invisible_Reject()
        For Me.i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(1).Controls(1)
            chkCursor.Visible = False
            chkCursor = Nothing
        Next
    End Sub

#End Region

    Protected Sub ddlAID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAID.SelectedIndexChanged
        Dim EmployeeCode As String = ""
        Dim CompanyCode As String = ""
        ssql = "Select Top 1 Employee_Profile_ID,Company_Profile_Code from Employee_CodeName_Vw Where Code = N'" & Trim(ddlAID.SelectedValue) & "' And [Name] = N'" & Trim(ddlAID.SelectedItem.Text).Replace("'", "''") & "' Order By Effective_Date Desc"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            EmployeeCode = myDS.Tables(0).Rows(0).Item(0).ToString
            CompanyCode = myDS.Tables(0).Rows(0).Item(1).ToString
            ssql = "Exec sp_ls_cal_emp_leave_balance '" & CompanyCode & "','" & EmployeeCode & "'"
            mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        Else
            EmployeeCode = "%"
            myDS = Nothing
        End If
        myDS = Nothing

        ssql2 = "exec sp_sa_getTableRecords2 '" & Session("Company") & "','LEAVE','Employee_Leave_Balance','100','1','[EMPLOYEE_PROFILE_ID]','" & EmployeeCode & "'"
        myDS1 = mySQL.ExecuteSQL(ssql2)

        If myDS1.Tables.Count = 2 Then
            BalGv.DataSource = myDS1.Tables(1)
            BalGv.DataBind()
        End If
        myDS1 = Nothing
    End Sub

End Class