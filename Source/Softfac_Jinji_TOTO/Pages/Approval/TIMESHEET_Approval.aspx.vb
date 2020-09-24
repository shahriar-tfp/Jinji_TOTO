Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.web.Configuration

Partial Class Pages_Approval_TIMESHEET_Approval
    Inherits System.Web.UI.Page

#Region "Public Declaration"
    Private WithEvents mySQL As New clsSQL, myDS, myDS1, myDS2, myDS3, myDS4, myDS10 As New DataSet, mySetting As New clsGlobalSetting, myMsg As New clsMessage
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Public ssql As String, i As Integer, j As Integer, SearchByPage As Boolean, RecFound As Boolean, CountRecord As Integer
    Public AllowView, AllowInsert, AllowUpdate, AllowDelete, AllowPrint As Boolean
    Public _currentPageNumber As Integer, _totalPage As Double = 1, _totalRecords As Integer
    Public myDE As New DictionaryEntry, myHT As Hashtable
    Public myDataType As New clsGlobalSetting.DataType
#End Region

#Region "Private Declaration"
    Private WithEvents myDT1 As New DataTable, myDT2 As New DataTable
    Dim myDR1 As DataRow, myDR2 As DataRow, rcPerPage As Integer, autonum As Integer
    Dim ssql1, ssql2, ssql3, ssql4 As String
    Dim rowPosition As String = "R_", rowPositionM As String = "RM_", panelPosition As String = "Display_"
    Dim buttonPosition1 As String = "B1_", buttonPosition2 As String = "B2_", buttonPosition3 As String = "B3_", labelPosition1 As String = "L1_"
    Dim logic As Boolean
    Dim grdItem As GridViewRow
    Dim chkSelected, chkSelected1, chkSelected2 As CheckBox
    Dim dtSelectedDate As DateTime
    Dim stPeriod, stDateApplyOn, PrevLeaveID, LeaveID, iID, StopChecking, mstBalance As String
    Dim ApplicationEnd, ContinueLoop As Boolean
    Dim Day, TotalDay As Decimal
    Dim Err1, Err2, Err3, Err4, Err5, Err6, vDate1, vDate2, vDate3, vDate4, vDate5, vDate6, Message As String
    Dim vCount, vCounter, CountDay, CountSequence, ContinueCount As Integer
    Dim Action, DateApplyFor, strempid, CurrentDate As String
    Dim Counter, CurrentYear, SysYear As Integer
    Dim strPath As String = "../../Images"
    Dim btnColourDef, btnColourAlt As String
#End Region

#Region "Page Setting"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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
        End If

    End Sub

    Sub PagePreload()
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

        'disable button details
        imgBtnOTDetail.Visible = False
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
        chkAEmpGroup.Text = "By Employee Group"
        lblAID.Text = "Employee Code"
        lblAName.Text = "Employee Name"
        lblADateFrom.Text = "Date From"
        lblADateTo.Text = "Date To"

        'get panel part 3 name
        'optPreOTApproved.Text = "Pre OT Approved"
        'optApprovedOT.Text = "Approved OT"
        'optPreOTApproved.Checked = True

        'get panel part 4 name
        chkPOH.Text = "POH (Pre OT)"
        chkAOH.Text = "POH = AOH (Actual OT)"
        txtPOH.MaxLength = "2"
        txtAOH.MaxLength = "2"
        txtPOH.Text = "##"
        txtPOH.CssClass = "wordstyle7"
        txtAOH.Text = "##"
        txtAOH.CssClass = "wordstyle7"
        txtPOH.Attributes.Add("onFocus", "focus_txtpoh()")
        txtPOH.Attributes.Add("onblur", "blur_txtpoh()")
        txtAOH.Attributes.Add("onFocus", "focus_txtaoh()")
        txtAOH.Attributes.Add("onblur", "blur_txtaoh()")

        'get chk sel name
        chkAAll.Text = "Approve All"
        chkRAll.Text = "Reject All"

        'get image 
        mySetting.GetImgUrl(imgCompany_Profile_ID, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)

        mySetting.GetImgTypeUrl(imgBlank02, Session("Company").ToString, "Png", "blank.png")
        mySetting.GetImgTypeUrl(imgBlank03, Session("Company").ToString, "Png", "blank.png")
        mySetting.GetImgTypeUrl(imgBlank04, Session("Company").ToString, "Png", "blank.png")
        mySetting.GetImgTypeUrl(imgBlank05, Session("Company").ToString, "Png", "blank.png")
        mySetting.GetImgTypeUrl(imgBlank06, Session("Company").ToString, "Png", "blank.png")

        'get image button
        mySetting.GetImgBtnUrl(imgBtnCompany_Profile_ID, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgBtnAID, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgBtnAName, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgBtnADateFrom, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgBtnADateTo, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)

        mySetting.GetBtnImgUrl(imgBtnOTDetail, Session("Company").ToString, btnColourDef, "btnDetails.png")
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

        'get calendar setting
        mySetting.PopUpCalendar_ImageButton(imgBtnADateFrom, Form.ID, "txtADateFrom")
        mySetting.PopUpCalendar_ImageButton(imgBtnADateTo, Form.ID, "txtADateTo")

        body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

        myDS = mySetting.GetPageFieldSetting(Session("Company"), "TIMESHEET_APPROVAL", Session("EmpID"))
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
                Else
                    AllowView = False
                    pnlresult.Visible = False
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

    End Sub

    Sub GetRequireData()

        ssql = "Exec sp_app_GetInfo 'LeaveApproval','" & Session("Company") & "','" & Session("Module") & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            If UCase(myDS.Tables(0).Rows(0).Item(0).ToString) = "SUCCESS" Then
                Session("CurrentDate") = myDS.Tables(0).Rows(0).Item(1).ToString
                SysYear = CInt(myDS.Tables(0).Rows(0).Item(3).ToString)
                lnkBtnViewPerOT.Enabled = True
                lnkBtnViewAppPerOT.Enabled = True
                lnkBtnViewOTDone.Enabled = True
                lnkBtnViewOT.Enabled = True
                lnkBtnViewAppOT.Enabled = True
            Else
                pnlpart1.Visible = False
                pnlbutton.Visible = False
                lblresult.Text = "Error Found! Data fail to retreive..."
                Exit Sub
            End If
        End If

    End Sub

#End Region

#Region "Panel Action"

    Protected Sub lnkBtnViewPerOT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnViewPerOT.Click
        lnkBtnViewPerOT_Click()
    End Sub

    Protected Sub lnkBtnClosePerOT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnClosePerOT.Click
        lnkBtnClosePerOT_Click()
    End Sub

    Protected Sub lnkBtnViewAppPerOT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnViewAppPerOT.Click
        lnkBtnViewAppPerOT_Click()
    End Sub

    Protected Sub lnkBtnCloseAppPerOT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnCloseAppPerOT.Click
        lnkBtnCloseAppPerOT_Click()
    End Sub

    Protected Sub lnkBtnViewOTDone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnViewOTDone.Click
        lnkBtnViewOTDone_Click()
    End Sub

    Protected Sub lnkBtnCloseOTDone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnCloseOTDone.Click
        lnkBtnCloseOTDone_Click()
    End Sub

    Protected Sub lnkBtnViewOT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnViewOT.Click
        lnkBtnViewOT_Click()
    End Sub

    Protected Sub lnkBtnCloseOT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnCloseOT.Click
        lnkBtnCloseOT_Click()
    End Sub

    Protected Sub lnkBtnViewAppOT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnViewAppOT.Click
        lnkBtnViewAppOT_Click()
    End Sub

    Protected Sub lnkBtnCloseAppOT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnCloseAppOT.Click
        lnkBtnCloseAppOT_Click()
    End Sub

    Protected Sub chkAEmployee_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAEmployee.CheckedChanged
        chkAEmployee.Checked = True
        chkADepartment.Checked = False
        chkAEmpGroup.Checked = False
        OptionCheckedChange1()
        ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "EMPLOYEE_CODE" & """," & """" & """," & """" & Session("EmpID").ToString & """"
        mySetting.GetLookupValue_ImageButton(imgBtnAID, Form.ID, "txtAID", "Code", ssql)
        ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "EMPLOYEE_NAME" & """," & """" & """," & """" & Session("EmpID").ToString & """"
        mySetting.GetLookupValue_ImageButton(imgBtnAName, Form.ID, "txtAName", "Name", ssql)
        txtAID.Text = ""
        txtANAME.Text = ""
        lblAID.Visible = True
        ddlOption_Type.Visible = False
    End Sub

    Protected Sub chkAEmpGroup_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAEmpGroup.CheckedChanged
        chkAEmpGroup.Checked = True
        chkADepartment.Checked = False
        chkAEmployee.Checked = False
        OptionCheckedChange1()
        ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "EMPGROUP_CODE" & """," & """" & """," & """" & Session("EmpID").ToString & """"
        mySetting.GetLookupValue_ImageButton(imgBtnAID, Form.ID, "txtAID", "Code", ssql)
        ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "EMPGROUP_NAME" & """," & """" & """," & """" & Session("EmpID").ToString & """"
        mySetting.GetLookupValue_ImageButton(imgBtnAName, Form.ID, "txtAName", "Name", ssql)
        txtAID.Text = ""
        txtANAME.Text = ""
    End Sub

    Protected Sub chkADepartment_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkADepartment.CheckedChanged
        chkADepartment.Checked = True
        chkAEmployee.Checked = False
        chkAEmpGroup.Checked = False
        OptionCheckedChange1()
        txtAID.Text = ""
        txtANAME.Text = ""
        lblAID.Visible = False
        ddlOption_Type.Visible = True
        ddlOption_Type.SelectedIndex = 0
    End Sub

    Protected Sub chkPOH_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPOH.CheckedChanged
        If chkPOH.Checked = True Then
            chkAOH.Checked = False
            txtPOH.Text = "##"
            txtPOH.CssClass = "wordstyle7"
            txtAOH.Text = "##"
            txtAOH.CssClass = "wordstyle7"
        End If
    End Sub

    Protected Sub chkAOH_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAOH.CheckedChanged
        If chkAOH.Checked = True Then
            chkPOH.Checked = False
            txtPOH.Text = "##"
            txtPOH.CssClass = "wordstyle7"
            txtAOH.Text = "##"
            txtAOH.CssClass = "wordstyle7"
        End If
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

    Protected Sub imgBtnOTDetail_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnOTDetail.Click

        Session("action") = "detail"
        lblresult.Text = ""

    End Sub

    Protected Sub imgBtnUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnUpdate.Click

        Session("action") = "update"
        lblresult.Text = ""

        If CheckForARGv1() > 1 Then
            lblresult.Text = "Please confirm the selection! Only one action can be seleted per row data..."
            Exit Sub
        Else
            If CheckForAGv1() = 0 And CheckForRGv1() = 0 Then
                lblresult.Text = "Please confirm the selection! No row selected..."
                Exit Sub
            Else
                If lnkBtnClosePerOT.Visible = True Then
                    If chkPOH.Checked = True Then
                        If txtPOH.Text = "##" And txtAOH.Text = "##" Then
                            lblresult.Text = "Pre OT value can not be null!"
                            Exit Sub
                        End If
                    End If
                    ApprovedPendingPreOT()
                    RejectPendingPreOT()
                    SubLoadSSQL("PENDING", "PREOTPENDING")
                    RetrieveDetail()
                ElseIf lnkBtnCloseAppPerOT.Visible = True Then
                    If chkPOH.Checked = True Then
                        If txtPOH.Text = "##" And txtAOH.Text = "##" Then
                            lblresult.Text = "Pre OT value can not be null!"
                            Exit Sub
                        End If
                    End If
                    ChangeApprovedPreOT()
                    RejectApprovedPreOT()
                    SubLoadSSQL("APPROVED", "PREOTAPPROVED")
                    RetrieveDetail()
                ElseIf lnkBtnCloseOTDone.Visible = True Then
                    ChangeApprovedOTDoneDis()
                    RejectOTDoneDis()
                    SubLoadSSQL("APPROVED", "OTDONE")
                    RetrieveDetail()
                ElseIf lnkBtnCloseOT.Visible = True Then
                    If chkPOH.Checked = True Then
                        If txtPOH.Text = "##" And txtAOH.Text = "##" Then
                            lblresult.Text = "Pre OT value can not be null!"
                            Exit Sub
                        End If
                    End If
                    ApprovedNoPreOTDone()
                    RejectNoPreOTDone()
                    SubLoadSSQL("PENDING", "NOPREOTDONE")
                    RetrieveDetail()
                ElseIf lnkBtnCloseAppOT.Visible = True Then
                    ChangeApprovedOT()
                    RejectApprovedOT()
                    SubLoadSSQL("APPROVED", "APPROVEDOT")
                    RetrieveDetail()
                End If
            End If
        End If

    End Sub

    Protected Sub imgBtnCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnCancel.Click

        Session("action") = "cancel"
        lblresult.Text = ""
        If Session("TabStatus") = "PREOTPENDING" Then
            lnkBtnClosePerOT_Click()
        ElseIf Session("TabStatus") = "PREOTAPPROVED" Then
            lnkBtnViewAppPerOT_Click()
        ElseIf Session("TabStatus") = "OTDONE" Then
            lnkBtnCloseOTDone_Click()
        ElseIf Session("TabStatus") = "NOPREOTDONE" Then
            lnkBtnCloseOT_Click()
        ElseIf Session("TabStatus") = "APPROVEDOT" Then
            lnkBtnCloseAppOT_Click()
        End If

    End Sub

    Protected Sub imgBtnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSearch.Click

        Session("action") = "search"
        lblresult.Text = ""
        SearchDetail()

    End Sub

    Protected Sub imgBtnPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnPrint.Click

        Session("action") = "print"
        lblresult.Text = ""

    End Sub

#End Region

#Region "Sub & Function"

    Public Sub SubLoadSSQL(ByVal Status As String, ByVal vtype As String)
        If Status = "PENDING" Then
            Status = "SUBMIT"
        Else
            Status = "APPROVAL"
        End If
        ssql = "Exec sp_ts_ApprRejectTimeSheet '" & Status & "','" & Session("Company") & "','','" & _
        Session("EmpID").ToString & "','',''"

    End Sub

    Public Sub RetrieveDetail()

        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")

        If myDS.Tables.Count = 4 Then
            If myDS.Tables(1).Rows.Count = 0 Then
                If Session("action") = "search" Then
                    lblresult.Text = "No Data Found..."
                Else
                    imgBtnCancel.Enabled = False
                    'imgBtnSearch.Enabled = False
                    imgBtnPrint.Enabled = False

                    mySetting.GetBtnImgUrl(imgBtnCancel, Session("Company").ToString, btnColourAlt, "btnCancel.png")
                    'mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourAlt, "btnSearch.png")
                    mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourAlt, "btnPrint.png")
                End If
                imgBtnOTDetail.Enabled = False
                imgBtnUpdate.Enabled = False
                imgBtnGoToPage1.Enabled = False

                mySetting.GetBtnImgUrl(imgBtnOTDetail, Session("Company").ToString, btnColourAlt, "btnDetails.png")
                mySetting.GetBtnImgUrl(imgBtnUpdate, Session("Company").ToString, btnColourAlt, "btnUpdate.png")
                mySetting.GetBtnImgUrl(imgBtnGoToPage1, Session("Company").ToString, btnColourAlt, "btnGo.png")

                CurrentPage1.Text = myDS.Tables(0).Rows(0).Item(0)
                TotalPages1.Text = myDS.Tables(0).Rows(0).Item(0)
                lbltotal1.Text = " ( " & myDS.Tables(0).Rows(0).Item(0) & " record(s) ) "
                myGridView1.DataSource = myDS.Tables(1)
                myGridView1.DataBind()
                pnlbutton.Visible = True

                FirstPage1.Enabled = False
                PrevPage1.Enabled = False
                NextPage1.Enabled = False
                LastPage1.Enabled = False
                chkAAll.Enabled = False
                chkRAll.Enabled = False

                If lnkBtnClosePerOT.Visible = True Then
                    imgBtnSearch.Enabled = False
                    mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourAlt, "btnSearch.png")
                ElseIf lnkBtnCloseAppPerOT.Visible = True Then
                    imgBtnSearch.Enabled = True
                    mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourDef, "btnSearch.png")
                ElseIf lnkBtnCloseOTDone.Visible = True Then
                    imgBtnSearch.Enabled = False
                    mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourAlt, "btnSearch.png")
                ElseIf lnkBtnCloseOT.Visible = True Then
                    imgBtnSearch.Enabled = False
                    mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourAlt, "btnSearch.png")
                ElseIf lnkBtnCloseAppOT.Visible = True Then
                    imgBtnSearch.Enabled = True
                    mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourDef, "btnSearch.png")
                End If
                Exit Sub
            Else
                imgBtnOTDetail.Enabled = True
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
                mySetting.GetBtnImgUrl(imgBtnOTDetail, Session("Company").ToString, btnColourDef, "btnDetails.png")
                mySetting.GetBtnImgUrl(imgBtnUpdate, Session("Company").ToString, btnColourDef, "btnUpdate.png")
                mySetting.GetBtnImgUrl(imgBtnCancel, Session("Company").ToString, btnColourDef, "btnCancel.png")
                mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourDef, "btnSearch.png")
                mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourDef, "btnPrint.png")
                mySetting.GetBtnImgUrl(imgBtnGoToPage1, Session("Company").ToString, btnColourDef, "btnGo.png")
            End If

            myGridView1.DataSource = myDS.Tables(1)
            myGridView1.DataBind()
            myGridview2.DataSource = myDS.Tables(2)
            myGridview2.DataBind()
            Session("MasterOTType") = myDS.Tables(3).Rows(0).Item(0).ToString
            pnlbutton.Visible = True

            If lnkBtnClosePerOT.Visible = True Then
                myGridView1.HeaderRow.Cells(0).Text = "Approve"
                imgBtnSearch.Enabled = False
                mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourAlt, "btnSearch.png")
            ElseIf lnkBtnCloseAppPerOT.Visible = True Then
                myGridView1.HeaderRow.Cells(0).Text = "Change"
                imgBtnSearch.Enabled = True
                mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourDef, "btnSearch.png")
            ElseIf lnkBtnCloseOTDone.Visible = True Then
                myGridView1.HeaderRow.Cells(0).Text = "Approve"
                imgBtnSearch.Enabled = False
                mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourAlt, "btnSearch.png")
            ElseIf lnkBtnCloseOT.Visible = True Then
                myGridView1.HeaderRow.Cells(0).Text = "Approve"
                imgBtnSearch.Enabled = False
                mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourAlt, "btnSearch.png")
            ElseIf lnkBtnCloseAppOT.Visible = True Then
                myGridView1.HeaderRow.Cells(0).Text = "Change"
                imgBtnSearch.Enabled = True
                mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourDef, "btnSearch.png")
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
                For i = 0 To myDT2.Rows.Count - 1
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

    Public Sub SearchDetail()

        Dim vType As String = ""
        Dim vID As String = ""
        Dim vName As String = ""
        Dim FromDate As String = ""
        Dim ToDate As String = ""
        Dim SelType As String = ""
        Dim SearchType As String = ""
        Dim Status As String = ""

        'Check From Date
        If mySetting.CheckTextNull(txtADateFrom.Text) = False Then
            ssql = "Select dbo.fn_DateAdd('Day',-30,'" & Session("CurrentDate") & "')"
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

        'Check To Date
        If mySetting.CheckTextNull(txtADateTo.Text) = False Then
            ToDate = Session("CurrentDate")
        Else
            If mySetting.CheckTextNull(txtADateFrom.Text) = False Then
                ToDate = Session("CurrentDate")
            Else
                ToDate = mySetting.ConvertDateToDecimal(txtADateTo.Text, Session("Company"), Session("Module"))
                If Len(ToDate) <> 14 Then
                    lblresult.Text = "Incorrect date format of field " & lblADateTo.Text & "!"
                    Exit Sub
                End If
            End If
        End If

        'Check Date Order
        If FromDate > ToDate = True Then
            lblresult.Text = "Field " & lblADateFrom.Text & " should not be greater than " & lblADateTo.Text
            Exit Sub
        End If

        'Get filter value
        If chkAEmployee.Checked = True Then
            SearchType = "EMPID"
            Status = "APPROVED"
            If txtAID.Text <> "" Then
                ssql = "Select Employee_Profile_ID From Employee_CodeName_Vw Where Company_Profile_Code = '" & Session("Company") & _
                       "' And Code = '" & txtAID.Text & "'"
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                If myDS.Tables(0).Rows.Count > 0 Then
                    vID = myDS.Tables(0).Rows(0).Item(0).ToString & "%"
                End If
                myDS = Nothing
            Else
                vID = "%"
            End If

            If txtANAME.Text <> "" Then
                If Trim(txtAID.Text) = "%" Then
                    ssql = "Select Employee_Profile_ID From Employee_CodeName_Vw Where Company_Profile_Code = '" & Session("Company") & _
                           "' And Name = '" & txtANAME.Text & "'"
                    myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                    If myDS.Tables(0).Rows.Count > 0 Then
                        vID = myDS.Tables(0).Rows(0).Item(0).ToString & "%"
                    End If
                    myDS = Nothing
                End If
                vName = ""
            Else
                vName = ""
            End If

        ElseIf chkAEmpGroup.Checked = True Then
            SearchType = "EMPLOYEE_GROUP"
            Status = "APPROVED"
            If txtAID.Text <> "" Then
                ssql = "Select ID From Organisation_Code_Profile_Vw Where Company_Profile_Code = '" & Session("Company") & _
                       "' And Option_Type = 'EMPLOYEE_GROUP' And Code = '" & txtAID.Text & "'"
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                If myDS.Tables(0).Rows.Count > 0 Then
                    vID = myDS.Tables(0).Rows(0).Item(0).ToString & "%"
                End If
                myDS = Nothing
            Else
                vID = "%"
            End If

            If txtANAME.Text <> "" Then
                If Trim(txtAID.Text) = "%" Then
                    ssql = "Select ID From Organisation_Code_Profile_Vw Where Company_Profile_Code = '" & Session("Company") & _
                           "' And Option_Type = 'EMPLOYEE_GROUP' And Name = '" & txtANAME.Text & "'"
                    myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                    If myDS.Tables(0).Rows.Count > 0 Then
                        vID = myDS.Tables(0).Rows(0).Item(0).ToString & "%"
                    End If
                    myDS = Nothing
                End If
                vName = ""
            Else
                vName = ""
            End If

        ElseIf chkADepartment.Checked = True Then
            SearchType = ddlOption_Type.SelectedValue
            Status = "APPROVED"
            If txtAID.Text <> "" Then
                ssql = "Select ID From Organisation_Code_Profile_Vw Where Company_Profile_Code = '" & Session("Company") & _
                       "' And Option_Type = '" & ddlOption_Type.SelectedValue & "' And CodeName = '" & txtAID.Text & "'"
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                If myDS.Tables(0).Rows.Count > 0 Then
                    vID = myDS.Tables(0).Rows(0).Item(0).ToString & "%"
                End If
                myDS = Nothing
            Else
                vID = "%"
            End If

            If txtANAME.Text <> "" Then
                vName = ""
            Else
                vName = ""
            End If
        End If

        If lnkBtnCloseAppPerOT.Visible = True Then
            Session("currentpage1") = "1"
            Session("TabStatus") = "PREOTAPPROVED"
            Session("OptStatus") = "APPROVED"
            SelType = Session("TabStatus")

        ElseIf lnkBtnCloseAppOT.Visible = True Then
            Session("currentpage1") = "1"
            Session("TabStatus") = "APPROVEDOT"
            Session("OptStatus") = "APPROVED"
            SelType = Session("TabStatus")

        End If

        ssql = "Exec sp_tms_OTApproval '" & Session("Company") & "','" _
                & vID & "','" & vName & "','" & FromDate & "','" _
                & ToDate & "','" & Session("EmpID") & "','" _
                & "" & "','" & Status & "','" & SelType & "','" & SearchType _
                & "','" & myGridView1.PageSize & "','" & Session("currentPage1") & "'"
        RetrieveDetail()

    End Sub

    Private Sub ApprovedPendingPreOT()

        Dim empid As String
        Dim vYear As String
        Dim vMonth As String
        Dim OTType As String
        Dim POH As String

        For i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(0).Controls(1)
            If chkCursor.Checked = True Then
                empid = Trim(myGridview2.Rows(i).Cells(3).Text)
                vMonth = Trim(myGridview2.Rows(i).Cells(4).Text)
                vYear = Trim(myGridview2.Rows(i).Cells(5).Text)
                If Session("MasterOTType") = "YES" Then
                    OTType = Trim(myGridview2.Rows(i).Cells(6).Text)
                Else
                    OTType = ""
                End If

                If chkPOH.Checked = True Then
                    If txtPOH.Text = "##" Then
                        txtPOH.Text = "00"
                    End If
                    If txtAOH.Text = "##" Then
                        txtPOH.Text = "00"
                    End If
                    If Len(txtPOH.Text) < 2 Then
                        txtPOH.Text = "0" & txtPOH.Text
                    End If
                    If Len(txtAOH.Text) < 2 Then
                        txtAOH.Text = "0" & txtAOH.Text
                    End If
                    POH = txtPOH.Text & ":" & txtAOH.Text
                ElseIf chkAOH.Checked = True Then
                    If Session("MasterOTType") = "YES" Then
                        POH = Trim(myGridview2.Rows(i).Cells(8).Text)
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(7).Text)
                    End If
                Else
                    If Session("MasterOTType") = "YES" Then
                        POH = Trim(myGridview2.Rows(i).Cells(7).Text)
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(6).Text)
                    End If
                End If

                ssql = "Exec sp_ts_ApprRejectTimesheet 'APPROVED','" & Session("Company") & "','" _
                       & empid & "','" & Session("EmpID") & "','" & vMonth & "','" _
                       & vYear & "'"
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            End If
        Next

    End Sub

    Private Sub RejectPendingPreOT()
        Dim empid As String
        Dim vYear As String
        Dim vMonth As String
        Dim OTType As String
        Dim POH As String
        Dim AOH As String

        For i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(1).Controls(1)
            If chkCursor.Checked = True Then
                empid = Trim(myGridview2.Rows(i).Cells(3).Text)
                vMonth = Trim(myGridview2.Rows(i).Cells(4).Text)
                vYear = Trim(myGridview2.Rows(i).Cells(5).Text)
                AOH = "00:00"
                If Session("MasterOTType") = "YES" Then
                    OTType = Trim(myGridview2.Rows(i).Cells(6).Text)
                    POH = Trim(myGridview2.Rows(i).Cells(7).Text)
                Else
                    OTType = ""
                    POH = Trim(myGridview2.Rows(i).Cells(6).Text)
                End If

                ssql = "Exec sp_ts_ApprRejectTimesheet 'REJECTED','" & Session("Company") & "','" _
                       & empid & "','" & Session("EmpID") & "','" & vMonth & "','" _
                       & vYear & "'"
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            End If
        Next

    End Sub

    Private Sub ChangeApprovedPreOT()
        Dim empid As String
        Dim vDate As String
        Dim OTType As String
        Dim POH As String
        Dim AOH As String

        For i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(0).Controls(1)
            If chkCursor.Checked = True Then
                empid = Trim(myGridview2.Rows(i).Cells(2).Text)
                vDate = Trim(myGridview2.Rows(i).Cells(4).Text)
                If Session("MasterOTType") = "YES" Then
                    OTType = Trim(myGridview2.Rows(i).Cells(5).Text)
                Else
                    OTType = ""
                End If

                AOH = "00:00"
                If chkPOH.Checked = True Then
                    If txtPOH.Text = "##" Then
                        txtPOH.Text = "00"
                    End If
                    If txtAOH.Text = "##" Then
                        txtPOH.Text = "00"
                    End If
                    If Len(txtPOH.Text) < 2 Then
                        txtPOH.Text = "0" & txtPOH.Text
                    End If
                    If Len(txtAOH.Text) < 2 Then
                        txtAOH.Text = "0" & txtAOH.Text
                    End If
                    POH = txtPOH.Text & ":" & txtAOH.Text
                ElseIf chkAOH.Checked = True Then
                    If Session("MasterOTType") = "YES" Then
                        POH = Trim(myGridview2.Rows(i).Cells(7).Text)
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(6).Text)
                    End If
                Else
                    If Session("MasterOTType") = "YES" Then
                        POH = Trim(myGridview2.Rows(i).Cells(6).Text)
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(5).Text)
                    End If
                End If

                ssql = "Exec sp_tms_updOTApproval '" & Session("Company") & "','" _
                       & empid & "','" & vDate & "','" & OTType & "','" _
                       & POH & "','" & AOH & "','" & Session("EmpID") & "','" & "" & "','" & "CHANGE" & "'"
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            End If
        Next

    End Sub

    Private Sub RejectApprovedPreOT()

        Dim empid As String
        Dim vDate As String
        Dim OTType As String
        Dim POH As String
        Dim AOH As String

        For i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(1).Controls(1)
            If chkCursor.Checked = True Then
                empid = Trim(myGridview2.Rows(i).Cells(2).Text)
                vDate = Trim(myGridview2.Rows(i).Cells(4).Text)
                If Session("MasterOTType") = "YES" Then
                    OTType = Trim(myGridview2.Rows(i).Cells(5).Text)
                    AOH = "00:00"
                    If chkPOH.Checked = True Then
                        POH = txtPOH.Text & ":" & txtAOH.Text
                    ElseIf chkAOH.Checked = True Then
                        POH = AOH
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(6).Text)
                    End If
                Else
                    OTType = ""
                    AOH = "00:00"
                    If chkPOH.Checked = True Then
                        POH = txtPOH.Text & ":" & txtAOH.Text
                    ElseIf chkAOH.Checked = True Then
                        POH = AOH
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(5).Text)
                    End If
                End If

                ssql = "Exec sp_tms_updOTApproval '" & Session("Company") & "','" _
                       & empid & "','" & vDate & "','" & OTType & "','" _
                       & POH & "','" & AOH & "','" & Session("EmpID") & "','" & "" & "','" & "REJECT" & "'"
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            End If
        Next

    End Sub

    Private Sub ChangeApprovedOTDoneDis()
        Dim empid As String
        Dim vDate As String
        Dim OTType As String
        Dim POH As String
        Dim AOH As String

        For i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(0).Controls(1)
            If chkCursor.Checked = True Then
                empid = Trim(myGridview2.Rows(i).Cells(2).Text)
                vDate = Trim(myGridview2.Rows(i).Cells(4).Text)
                If Session("MasterOTType") = "YES" Then
                    OTType = Trim(myGridview2.Rows(i).Cells(5).Text)
                    AOH = Trim(myGridview2.Rows(i).Cells(7).Text)
                    If chkPOH.Checked = True Then
                        POH = txtPOH.Text & ":" & txtAOH.Text
                    ElseIf chkAOH.Checked = True Then
                        POH = AOH
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(6).Text)
                    End If
                Else
                    OTType = ""
                    AOH = Trim(myGridview2.Rows(i).Cells(6).Text)
                    If chkPOH.Checked = True Then
                        POH = txtPOH.Text & ":" & txtAOH.Text
                    ElseIf chkAOH.Checked = True Then
                        POH = AOH
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(5).Text)
                    End If
                End If

                ssql = "Exec sp_tms_updOTApproval '" & Session("Company") & "','" _
                       & empid & "','" & vDate & "','" & OTType & "','" _
                       & POH & "','" & AOH & "','" & Session("EmpID") & "','" & "" & "','" & "CHANGE2" & "'"
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            End If
        Next

    End Sub

    Private Sub RejectOTDoneDis()
        Dim empid As String
        Dim vDate As String
        Dim OTType As String
        Dim POH As String
        Dim AOH As String

        For i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(1).Controls(1)
            If chkCursor.Checked = True Then
                empid = Trim(myGridview2.Rows(i).Cells(2).Text)
                vDate = Trim(myGridview2.Rows(i).Cells(4).Text)
                If Session("MasterOTType") = "YES" Then
                    OTType = Trim(myGridview2.Rows(i).Cells(5).Text)
                    AOH = "00:00"
                    If chkPOH.Checked = True Then
                        POH = txtPOH.Text & ":" & txtAOH.Text
                    ElseIf chkAOH.Checked = True Then
                        POH = AOH
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(6).Text)
                    End If
                Else
                    OTType = ""
                    AOH = "00:00"
                    If chkPOH.Checked = True Then
                        POH = txtPOH.Text & ":" & txtAOH.Text
                    ElseIf chkAOH.Checked = True Then
                        POH = AOH
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(5).Text)
                    End If
                End If

                ssql = "Exec sp_tms_updOTApproval '" & Session("Company") & "','" _
                       & empid & "','" & vDate & "','" & OTType & "','" _
                       & POH & "','" & AOH & "','" & Session("EmpID") & "','" & "" & "','" & "REJECT" & "'"
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            End If
        Next

    End Sub

    Private Sub ApprovedNoPreOTDone()

        Dim empid As String
        Dim vDate As String
        Dim OTType As String
        Dim POH As String
        Dim AOH As String

        For i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(0).Controls(1)
            If chkCursor.Checked = True Then
                empid = Trim(myGridview2.Rows(i).Cells(2).Text)
                vDate = Trim(myGridview2.Rows(i).Cells(4).Text)
                If Session("MasterOTType") = "YES" Then
                    OTType = Replace(Trim(myGridview2.Rows(i).Cells(5).Text), "&nbsp;", "", , )
                    AOH = Trim(myGridview2.Rows(i).Cells(7).Text)
                    If chkPOH.Checked = True Then
                        POH = txtPOH.Text & ":" & txtAOH.Text
                    ElseIf chkAOH.Checked = True Then
                        POH = AOH
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(6).Text)
                    End If
                Else
                    OTType = ""
                    AOH = Trim(myGridview2.Rows(i).Cells(6).Text)
                    If chkPOH.Checked = True Then
                        POH = txtPOH.Text & ":" & txtAOH.Text
                    ElseIf chkAOH.Checked = True Then
                        POH = AOH
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(5).Text)
                    End If
                End If

                ssql = "Exec sp_tms_updOTApproval '" & Session("Company") & "','" _
                       & empid & "','" & vDate & "','" & OTType & "','" _
                       & POH & "','" & AOH & "','" & Session("EmpID") & "','" & "" & "','" & "APPROVED" & "'"
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            End If
        Next

    End Sub

    Private Sub RejectNoPreOTDone()
        Dim empid As String
        Dim vDate As String
        Dim OTType As String
        Dim POH As String
        Dim AOH As String

        For i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(1).Controls(1)
            If chkCursor.Checked = True Then
                empid = Trim(myGridview2.Rows(i).Cells(2).Text)
                vDate = Trim(myGridview2.Rows(i).Cells(4).Text)
                If Session("MasterOTType") = "YES" Then
                    OTType = Trim(myGridview2.Rows(i).Cells(5).Text)
                    AOH = "00:00"
                    If chkPOH.Checked = True Then
                        POH = txtPOH.Text & ":" & txtAOH.Text
                    ElseIf chkAOH.Checked = True Then
                        POH = AOH
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(6).Text)
                    End If
                Else
                    OTType = ""
                    AOH = "00:00"
                    If chkPOH.Checked = True Then
                        POH = txtPOH.Text & ":" & txtAOH.Text
                    ElseIf chkAOH.Checked = True Then
                        POH = AOH
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(5).Text)
                    End If
                End If

                ssql = "Exec sp_tms_updOTApproval '" & Session("Company") & "','" _
                       & empid & "','" & vDate & "','" & OTType & "','" _
                       & POH & "','" & AOH & "','" & Session("EmpID") & "','" & "" & "','" & "REJECT" & "'"
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            End If
        Next

    End Sub

    Private Sub ChangeApprovedOT()
        Dim empid As String
        Dim vDate As String
        Dim OTType As String
        Dim POH As String
        Dim AOH As String

        For i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(0).Controls(1)
            If chkCursor.Checked = True Then
                empid = Trim(myGridview2.Rows(i).Cells(2).Text)
                vDate = Trim(myGridview2.Rows(i).Cells(4).Text)

                If Session("MasterOTType") = "YES" Then
                    OTType = Trim(myGridview2.Rows(i).Cells(5).Text)
                    AOH = "00:00"
                    If chkPOH.Checked = True Then
                        POH = txtPOH.Text & ":" & txtAOH.Text
                    ElseIf chkAOH.Checked = True Then
                        POH = AOH
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(6).Text)
                    End If
                Else
                    OTType = ""
                    AOH = "00:00"
                    If chkPOH.Checked = True Then
                        POH = txtPOH.Text & ":" & txtAOH.Text
                    ElseIf chkAOH.Checked = True Then
                        POH = AOH
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(5).Text)
                    End If
                End If

                ssql = "Exec sp_tms_updOTApproval '" & Session("Company") & "','" _
                       & empid & "','" & vDate & "','" & OTType & "','" _
                       & POH & "','" & AOH & "','" & Session("EmpID") & "','" & "" & "','" & "CHANGE" & "'"
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            End If
        Next

    End Sub

    Private Sub RejectApprovedOT()
        Dim empid As String
        Dim vYear As String
        Dim vMonth As String
        Dim OTType As String
        Dim POH As String
        Dim AOH As String

        For i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(1).Controls(1)
            If chkCursor.Checked = True Then
                empid = Trim(myGridview2.Rows(i).Cells(3).Text)
                vMonth = Trim(myGridview2.Rows(i).Cells(4).Text)
                vYear = Trim(myGridview2.Rows(i).Cells(5).Text)
                If Session("MasterOTType") = "YES" Then
                    OTType = Trim(myGridview2.Rows(i).Cells(7).Text)
                    AOH = "00:00"
                    If chkPOH.Checked = True Then
                        POH = txtPOH.Text & ":" & txtAOH.Text
                    ElseIf chkAOH.Checked = True Then
                        POH = AOH
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(7).Text)
                    End If
                Else
                    OTType = ""
                    AOH = "00:00"
                    If chkPOH.Checked = True Then
                        POH = txtPOH.Text & ":" & txtAOH.Text
                    ElseIf chkAOH.Checked = True Then
                        POH = AOH
                    Else
                        POH = Trim(myGridview2.Rows(i).Cells(6).Text)
                    End If
                End If

                ssql = "Exec sp_ts_ApprRejectTimesheet 'REJECTED','" & Session("Company") & "','" _
                       & empid & "','" & Session("EmpID") & "','" & vMonth & "','" _
                       & vYear & "'"
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            End If
        Next

    End Sub

#End Region

#Region "Sub & Function 2"

    Public Sub ClearField()

        txtAID.Text = ""
        txtANAME.Text = ""
        txtADateFrom.Text = ""
        txtADateTo.Text = ""
        lblresult.Text = ""
        chkAAll.Checked = False
        chkRAll.Checked = False
        chkPOH.Checked = False
        chkAOH.Checked = False
        txtPOH.Text = "##"
        txtPOH.CssClass = "wordstyle7"
        txtAOH.Text = "##"
        txtAOH.CssClass = "wordstyle7"

    End Sub

    Public Sub OptionCheckedChange1()
        If chkAEmployee.Checked = True Then
            lblAID.Text = "Employee Code"
            lblAName.Text = "Employee Name"
            lblAName.Visible = True
            txtANAME.Visible = True
            imgBtnAName.Visible = True
        ElseIf chkAEmpGroup.Checked = True Then
            lblAID.Text = "Group Code"
            lblAName.Text = "Group Name"
            lblAName.Visible = True
            txtANAME.Visible = True
            imgBtnAName.Visible = True
        ElseIf chkADepartment.Checked = True Then
            lblAID.Text = "Department"
            lblAName.Text = ""
            lblAName.Visible = False
            txtANAME.Visible = False
            imgBtnAName.Visible = False
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
        SubLoadSSQL(Session("OptStatus"), Session("TabStatus"))
        RetrieveDetail()

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
                    SubLoadSSQL(Session("OptStatus"), Session("TabStatus"))
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

    Public Sub lnkBtnViewPerOT_Click()

        lnkBtnViewPerOT.Visible = False
        lnkBtnClosePerOT.Visible = True
        'lnkBtnViewAppPerOT.Visible = True
        'lnkBtnCloseAppPerOT.Visible = False
        'lnkBtnViewOTDone.Visible = True
        'lnkBtnCloseOTDone.Visible = False
        'lnkBtnViewOT.Visible = True
        'lnkBtnCloseOT.Visible = False
        lnkBtnViewAppOT.Visible = True
        lnkBtnCloseAppOT.Visible = False

        pnlpart1.Visible = True
        pnlpart2.Visible = False
        pnlpart3.Visible = False
        'pnlpart4.Visible = True

        Session("currentpage1") = "1"
        Session("TabStatus") = "PREOTPENDING"
        Session("OptStatus") = "PENDING"
        SubLoadSSQL(Session("OptStatus"), Session("TabStatus"))
        RetrieveDetail()
        ClearField()
        chkAOH.Checked = True

    End Sub

    Public Sub lnkBtnClosePerOT_Click()

        lnkBtnViewPerOT.Visible = True
        lnkBtnClosePerOT.Visible = False
        pnlpart1.Visible = False
        pnlbutton.Visible = False

    End Sub

    Public Sub lnkBtnViewAppPerOT_Click()

        lnkBtnViewPerOT.Visible = True
        lnkBtnClosePerOT.Visible = False
        lnkBtnViewAppPerOT.Visible = False
        lnkBtnCloseAppPerOT.Visible = True
        lnkBtnViewOTDone.Visible = True
        lnkBtnCloseOTDone.Visible = False
        lnkBtnViewOT.Visible = True
        lnkBtnCloseOT.Visible = False
        lnkBtnViewAppOT.Visible = True
        lnkBtnCloseAppOT.Visible = False

        pnlpart1.Visible = True
        pnlpart2.Visible = True
        pnlpart3.Visible = False
        pnlpart4.Visible = True

        Session("currentpage1") = "1"
        Session("TabStatus") = "PREOTAPPROVED"
        Session("OptStatus") = "APPROVED"
        SubLoadSSQL(Session("OptStatus"), Session("TabStatus"))
        RetrieveDetail()
        ClearField()
        chkAOH.Checked = True

    End Sub

    Public Sub lnkBtnCloseAppPerOT_Click()

        lnkBtnViewAppPerOT.Visible = True
        lnkBtnCloseAppPerOT.Visible = False
        pnlpart1.Visible = False
        pnlbutton.Visible = False

    End Sub

    Public Sub lnkBtnViewOTDone_Click()

        lnkBtnViewPerOT.Visible = True
        lnkBtnClosePerOT.Visible = False
        lnkBtnViewAppPerOT.Visible = True
        lnkBtnCloseAppPerOT.Visible = False
        lnkBtnViewOTDone.Visible = False
        lnkBtnCloseOTDone.Visible = True
        lnkBtnViewOT.Visible = True
        lnkBtnCloseOT.Visible = False
        lnkBtnViewAppOT.Visible = True
        lnkBtnCloseAppOT.Visible = False

        pnlpart1.Visible = True
        pnlpart2.Visible = False
        pnlpart3.Visible = False
        pnlpart4.Visible = True

        Session("currentpage1") = "1"
        Session("TabStatus") = "OTDONE"
        Session("OptStatus") = "APPROVED"
        SubLoadSSQL(Session("OptStatus"), Session("TabStatus"))
        RetrieveDetail()
        ClearField()
        chkAOH.Checked = True

    End Sub

    Public Sub lnkBtnCloseOTDone_Click()

        lnkBtnViewOTDone.Visible = True
        lnkBtnCloseOTDone.Visible = False
        pnlpart1.Visible = False
        pnlbutton.Visible = False

    End Sub

    Public Sub lnkBtnViewOT_Click()

        lnkBtnViewPerOT.Visible = True
        lnkBtnClosePerOT.Visible = False
        lnkBtnViewAppPerOT.Visible = True
        lnkBtnCloseAppPerOT.Visible = False
        lnkBtnViewOTDone.Visible = True
        lnkBtnCloseOTDone.Visible = False
        lnkBtnViewOT.Visible = False
        lnkBtnCloseOT.Visible = True
        lnkBtnViewAppOT.Visible = True
        lnkBtnCloseAppOT.Visible = False

        pnlpart1.Visible = True
        pnlpart2.Visible = False
        pnlpart3.Visible = False
        pnlpart4.Visible = True

        Session("currentpage1") = "1"
        Session("TabStatus") = "NOPREOTDONE"
        Session("OptStatus") = "PENDING"
        SubLoadSSQL(Session("OptStatus"), Session("TabStatus"))
        RetrieveDetail()
        ClearField()
        chkAOH.Checked = True

    End Sub

    Public Sub lnkBtnCloseOT_Click()

        lnkBtnViewOT.Visible = True
        lnkBtnCloseOT.Visible = False
        pnlpart1.Visible = False
        pnlbutton.Visible = False

    End Sub

    Public Sub lnkBtnViewAppOT_Click()

        lnkBtnViewPerOT.Visible = True
        lnkBtnClosePerOT.Visible = False
        'lnkBtnViewAppPerOT.Visible = True
        'lnkBtnCloseAppPerOT.Visible = False
        'lnkBtnViewOTDone.Visible = True
        'lnkBtnCloseOTDone.Visible = False
        'lnkBtnViewOT.Visible = True
        'lnkBtnCloseOT.Visible = False
        lnkBtnViewAppOT.Visible = False
        lnkBtnCloseAppOT.Visible = True

        pnlpart1.Visible = True
        pnlpart2.Visible = True
        pnlpart3.Visible = False
        'pnlpart4.Visible = True

        Session("currentpage1") = "1"
        Session("TabStatus") = "APPROVEDOT"
        Session("OptStatus") = "APPROVED"
        SubLoadSSQL(Session("OptStatus"), Session("TabStatus"))
        RetrieveDetail()
        ClearField()
        chkAOH.Checked = True

    End Sub

    Public Sub lnkBtnCloseAppOT_Click()

        lnkBtnViewAppOT.Visible = True
        lnkBtnCloseAppOT.Visible = False
        pnlpart1.Visible = False
        pnlbutton.Visible = False

    End Sub

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
        For i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(0).Controls(1)
            chkCursor.Checked = True
            chkCursor = Nothing
        Next
    End Sub

    Private Sub UnCheckAll_Approve()
        For i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(0).Controls(1)
            chkCursor.Checked = False
            chkCursor = Nothing
        Next
    End Sub

    Private Sub CheckAll_Reject()
        For i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(1).Controls(1)
            chkCursor.Checked = True
            chkCursor = Nothing
        Next
    End Sub

    Private Sub UnCheckAll_Reject()
        For i = 0 To myGridView1.Rows.Count - 1
            Dim chkCursor As CheckBox = myGridView1.Rows(i).Cells(1).Controls(1)
            chkCursor.Checked = False
            chkCursor = Nothing
        Next
    End Sub

    Protected Sub myGridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles myGridView1.RowCommand
        If e.CommandName = "View" Then
            Dim index As String = e.CommandArgument.ToString
            Response.Write("<script>window.open('../../Pages/Approval/View_TIMESHEET.aspx?EmployeeID=" & myGridview2.Rows(index).Cells(8).Text & "&Month=" & myGridview2.Rows(index).Cells(4).Text & "&Year=" & myGridview2.Rows(index).Cells(5).Text & "&CompanyID=" & Session("Company").ToString & "','','toolbar=no,menubar=no, scrollbars=yes, resizable=yes,location=n o, status=no');</script>")
        End If
    End Sub
#End Region
End Class
