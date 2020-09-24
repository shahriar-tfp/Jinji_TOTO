Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Windows.Forms.MessageBox
Imports System.Windows.Forms.MessageBoxButtons


Partial Class Pages_Leave_Employee_Leave_Application
    Inherits System.Web.UI.Page

#Region "Public Declaration"
    Private WithEvents mySQL As New clsSQL, myDS, myDS1, myDS2, myDS3, myDS4, myDS10 As New DataSet, mySetting As New clsGlobalSetting, myMsg As New clsMessage
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Public ssql As String, i As Integer, j As Integer, SearchByPage As Boolean, RecFound As Boolean, CountRecord As Integer
    Public AllowView As Boolean, AllowInsert As Boolean, AllowUpdate As Boolean, AllowDelete As Boolean
    Public _currentPageNumber As Integer, _totalPage As Double = 1, _totalRecords As Integer
    Public myDE As New DictionaryEntry, myHT As Hashtable
    Public myDataType As New clsGlobalSetting.DataType
    Dim strLeaveType, strFromTime, strToTime As String
    Dim PayCycle As String
#End Region

#Region "Private Declaration"
    Private WithEvents myDT1 As New DataTable, myDT2 As New DataTable
    Dim myDR1 As DataRow, myDR2 As DataRow, rcPerPage As Integer, autonum As Integer
    Dim ssql1, ssql2, ssql3 As String, ssql4 As String
    Dim rowPosition As String = "R_", rowPositionM As String = "RM_", panelPosition As String = "Display_"
    Dim buttonPosition1 As String = "B1_", buttonPosition2 As String = "B2_", buttonPosition3 As String = "B3_", labelPosition1 As String = "L1_"
    Dim logic As Boolean
    Dim grdItem As GridViewRow
    Dim chkSelected As CheckBox
    Dim dtSelectedDate As DateTime
    Dim stPeriod, stDateApplyOn, PrevLeaveID, LeaveID, iID, StopChecking, mstBalance As String
    Dim ApplicationEnd, ContinueLoop As Boolean
    Dim Day, TotalDay As Decimal
    Dim Err1, Err2, Err3, Err4, Err5, Err6, vDate1, vDate2, vDate3, vDate4, vDate5, vDate6, Message As String
    Dim vCount, vCounter, CountDay, CountSequence, ContinueCount As Integer
    Dim Action, DateApplyFor, strempid, CurrentDate As String
    Dim btnColourDef, btnColourAlt As String
    Dim clsMsg As New clsMessage
    Dim reason As String
    Dim strPath As String = "../../Images"
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Session.IsNewSession Then
            Response.Redirect("../Global/SessionTimeOut.aspx")
        End If

        If Not Page.IsPostBack Then
            Response.CacheControl = "no-cache"
            Response.AddHeader("Pragma", "no-cache")
            Response.Expires = -1

            Dim gvwidth As String = Session("ScreenWidth")
            Dim gvwidth2 As String = Session("GVwidth")
            Dim gvheight As String = (Session("ScreenHeight") / 2) - 50

            If Session("ScreenWidth") = 0 Then
                Session("ScreenWidth") = "1024"
                Session("GVwidth") = Session("ScreenWidth") - 360
            End If
            If Session("ScreenHeight") = 0 Then
                Session("ScreenHeight") = "768"
                Session("GVheight") = (Session("ScreenHeight") / 2) - 50
            End If

            pnlgridview1.Width = CInt(Session("GVwidth")) - 15
            pnlgridview2.Width = CInt(Session("GVwidth")) - 15
            pnlgridview2.Height = CInt(Session("GVheight")) - 140
            pnlTitle.Width = CInt(Session("GVwidth")) - 15
            pnlempinfo.Width = CInt(Session("GVwidth")) - 15
            pnlCancellation.Width = CInt(Session("GVwidth")) - 15
            'pnlpart1.Width = CInt(Session("GVwidth")) - 10
            'pnlpart2.Width = CInt(Session("GVwidth")) - 10
            pnlaction.Width = CInt(Session("GVwidth")) - 15
            pnlCalendar.Width = CInt(Session("GVwidth")) - 15
            pnlDayInfo.Width = CInt(Session("GVwidth")) - 15
            PagePreload()
            SetlinkPanelToDefault()
            InitializeEventCalendar()
            EmployeeChecking()
        End If

    End Sub

    Sub PagePreload()

        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")
        Session("Module") = "Leave"
        Session("action") = ""
        _currentPageNumber = 1
        Session("currentpage1") = _currentPageNumber
        Session("currentpage2") = _currentPageNumber
        Session("currentpage3") = _currentPageNumber
        FirstPage1.Enabled = False
        PrevPage1.Enabled = False
        NextPage1.Enabled = False
        LastPage1.Enabled = False
        FirstPage2.Enabled = False
        PrevPage2.Enabled = False
        NextPage2.Enabled = False
        LastPage2.Enabled = False
        FirstPage3.Enabled = False
        PrevPage3.Enabled = False
        NextPage3.Enabled = False
        LastPage3.Enabled = False
        pnlByTIME.Visible = False

        Session("curLeaveEmpID") = ""
        Session("CurLeaveID") = ""
        Session("CurLeaveName") = ""
        Session("CurSysDate") = ""
        Session("CurSysDateTime") = ""
        Session("SecurityRole") = ""

        txtEmployee_Profile_ID.Text = " Employee Code "
        txtEmployee_Profile_ID.CssClass = "wordstyle7"
        txtEmployee_Name.Text = " Employee Name "
        txtEmployee_Name.CssClass = "wordstyle7"
        txtEmployee_Profile_ID.Attributes.Add("onFocus", "focus_txtempid()")
        txtEmployee_Profile_ID.Attributes.Add("onblur", "blur_txtempid()")
        txtEmployee_Name.Attributes.Add("onFocus", "focus_txtempname()")
        txtEmployee_Name.Attributes.Add("onblur", "blur_txtempname()")

        lblEmployee_Profile_ID.CssClass = "wordstyle10"
        Image1.Width = 5
        Image1.Height = 5

        'get Page Title
        ssql = "exec sp_sa_GetPageTitle '" & Form.ID & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            lblTitle.Text = myDS.Tables(0).Rows(0).Item(0)
            lblTitle.CssClass = "wordstyle4"
        End If
        myDS = Nothing

        'get image 
        mySetting.GetImgUrl(imgEmployee_Profile_ID, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgDate_Apply_For, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgOption_Period, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgReason, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgOption_Status, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgTime_Apply_For, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgStandby_Employee, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)

        imgStandby_Employee.Visible = False
        mySetting.GetImgTypeUrl(imgblank01, Session("Company").ToString, "Png", "blank.png")
        mySetting.GetImgTypeUrl(imgBlank02, Session("Company").ToString, "Png", "blank.png")
        mySetting.GetImgTypeUrl(imgBlank03, Session("Company").ToString, "Png", "blank.png")
        mySetting.GetImgTypeUrl(imgBlank04, Session("Company").ToString, "Png", "blank.png")
        mySetting.GetImgTypeUrl(imgBlank05, Session("Company").ToString, "Png", "blank.png")
        mySetting.GetImgTypeUrl(imgBlank06, Session("Company").ToString, "Png", "blank.png")

        'get image button
        mySetting.GetImgBtnUrl(imgBtnEmployee_Profile_ID, clsGlobalSetting.ImageType._SEARCH, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgBtnDate_Apply_From, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgBtnDate_Apply_To, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgBtnTime_Apply_For, clsGlobalSetting.ImageType._TIME, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgBtnTime_Apply_To, clsGlobalSetting.ImageType._TIME, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgBtnStandby_Employee, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)

        mySetting.GetBtnImgUrl(imgBtnSubmit, Session("Company").ToString, btnColourDef, "btnSubmit.png")
        mySetting.GetBtnImgUrl(imgBtnClear, Session("Company").ToString, btnColourDef, "btnClear.png")
        mySetting.GetBtnImgUrl(imgBtnSelect, Session("Company").ToString, btnColourDef, "btnSelect.png")
        mySetting.GetBtnImgUrl(imgBtnApply, Session("Company").ToString, btnColourDef, "btnSubmit.png")
        mySetting.GetBtnImgUrl(imgBtnGoToPage1, Session("Company").ToString, btnColourDef, "btngo.png")
        mySetting.GetBtnImgUrl(imgBtnGoToPage2, Session("Company").ToString, btnColourDef, "btngo.png")

        'calendar setting
        mySetting.PopUpCalendar_ImageButton(imgBtnDate_Apply_From, Form.ID, "txtDate_Apply_From")
        mySetting.PopUpCalendar_ImageButton(imgBtnDate_Apply_To, Form.ID, "txtDate_Apply_To")
        mySetting.PopUpTime_ImageButton(imgBtnTime_Apply_For, Form.ID, "txtTime_Apply_For")
        mySetting.PopUpTime_ImageButton(imgBtnTime_Apply_To, Form.ID, "txtTime_Apply_To")

        'go button setting
        mySetting.SetTextBoxPressEnterGoToImageButton(txtEmployee_Profile_ID, imgBtnEmployee_Profile_ID)
        mySetting.SetTextBoxPressEnterGoToImageButton(txtEmployee_Name, imgBtnEmployee_Profile_ID)
        mySetting.SetTextBoxPressEnterGoToImageButton(txtGoToPage1, imgBtnGoToPage1)
        mySetting.SetTextBoxPressEnterGoToImageButton(txtGoToPage2, imgBtnGoToPage2)
        mySetting.SetTextBoxPressEnterGoToImageButton(txtGoToPage3, imgBtnGoToPage3)

        'option field setting
        mySetting.GetDropdownlistValue(Form.ID, "OPTION_PERIOD", ddlOption_Period)
        mySetting.GetDropdownlistValue(Form.ID, "OPTION_STATUS", ddlOption_Status)
        ddlOption_Status.SelectedIndex = 1

        mySetting.GetLookupValue_ImageButton(imgBtnStandby_Employee, Form.ID, "txtStandby_Employee", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """,'Standby_Employee'," & """" & "" & """," & """" & Session("EmpID").ToString & """")

        body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

        myDS = mySetting.GetPageFieldSetting(Session("Company"), Form.ID, Session("EmpID"))
        If myDS.Tables.Count > 1 Then
            myDT1 = myDS.Tables(0)
            myDT2 = myDS.Tables(1)

            If myDT1.Rows.Count > 0 Then
                myDR1 = myDT1.Rows(0)

                Page.Title = myDR1(1)
                If myDR1(3) = "YES" Then
                    AllowView = True
                    Table2.Visible = True
                Else
                    AllowView = False
                    Table2.Visible = False
                    ShowMessage("You are not allow to view this page!")
                    Exit Sub
                End If

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
                myGridview3.PageSize = Session("rcPerPage")
            Else
                lblresult.Text = "[Page Setting Error]: No setting found for this page!"
                Exit Sub
            End If

            If myDT2.Rows.Count > 0 Then
                'get label info
                lblEmployee_Profile_ID.Text = myDT2.Rows(0).Item(3).ToString
                lblDate_Apply_For.Text = "Date Apply From" 'myDT2.Rows(2).Item(2).ToString
                lblTime_Apply_For.Text = "Time Apply From"
                lblStandby_Employee.Text = "Standby Employee"
                lblOption_Period.Text = myDT2.Rows(3).Item(3).ToString
                lblOption_Status.Text = myDT2.Rows(7).Item(3).ToString
                lblReason.Text = myDT2.Rows(8).Item(3).ToString
            End If

            myDS = Nothing
            myDT1 = Nothing
            myDT2 = Nothing
        Else
            lblresult.Text = "[Field Setting Error]: No setting found for this page!"
            Exit Sub
        End If

    End Sub

    Sub EmployeeChecking()

        'ssql = "Select ISNULL(Employee_Profile_ID,'USER'), Code, Name From User_Profile Where Company_Profile_Code = '" & _
        '       Session("Company") & "' and Code = '" & Session("EmpID") & "'"
        ssql = "Select ISNULL(a.Security_Role_Profile_Code,'USER'), a.Code, b.[Name] From User_Profile a left outer join Employee_Codename_vw b on a.Employee_Profile_Id = b.Employee_Profile_ID  " & _
               "Where a.Company_Profile_Code = '" & Session("Company") & "' " & _
               "and a.Code = '" & Session("EmpID") & "'"

        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            Session("SecurityRole") = UCase(myDS.Tables(0).Rows(0).Item(0))
            If Left(UCase(myDS.Tables(0).Rows(0).Item(0)), 3) <> "EMP" Then

            Else
                txtEmployee_Profile_ID.Text = myDS.Tables(0).Rows(0).Item(1)
                txtEmployee_Name.Text = myDS.Tables(0).Rows(0).Item(2)

                lblresult1.Text = ""
                txtEmployee_Name.Enabled = False
                txtEmployee_Profile_ID.Enabled = False
                imgBtnEmployee_Profile_ID.Enabled = False
                Session("currentpage1") = 1
                BindGridInfo()
            End If
        End If
        myDS = Nothing

    End Sub

    Protected Sub imgBtnEmployee_Profile_ID_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnEmployee_Profile_ID.Click

        SetlinkPanelToDefault()

        If Trim(txtEmployee_Profile_ID.Text) = "Employee Code" And Trim(txtEmployee_Name.Text) = "Employee Name" Then
            lblresult.Text = lblEmployee_Profile_ID.Text & " is a require filed..."
            Exit Sub
            'txtEmployee_Name.Text = "%"
        End If
        lblresult1.Text = ""
        Session("currentpage1") = 1
        BindGridInfo()

    End Sub

    Private Sub ShowMessage(ByVal message As String)

        Dim scriptString As String = "<script language=JavaScript>"
        scriptString += "alert('" + message + "');"
        scriptString += "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "ShowMessage", scriptString)

    End Sub

#Region "Panel GridView"

    Protected Sub myGridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles myGridView1.RowDataBound
        e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor; this.style.backgroundColor='silver';")
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
    End Sub

    Protected Sub myGridview2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles myGridview2.RowDataBound
        e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor; this.style.backgroundColor='silver';")
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
    End Sub

#End Region

#Region "Panel Calendar"

    Sub InitializeEventCalendar()

        'Bind Data ddlYear
        ssql = "Exec sp_tms_GenerateYear " & 80 & "," & 20
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                ddlYear.DataValueField = "year"
                ddlYear.DataTextField = "year"
                ddlYear.DataSource = myDS.Tables(0).DefaultView
                ddlYear.DataBind()
            End If
        End If
        myDS = Nothing
        mySetting.ArrangeDDLSelectedIndex(ddlYear, clsGlobalSetting.DDLSelection.SelectedValue, DatePart(DateInterval.Year, Now()))
        Session("SelectedYear") = ddlYear.SelectedValue

        'Bind Data ddlMonth
        ssql = "Exec sp_tms_GenerateMonth 1,12"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                ddlMonth.DataValueField = "month"
                ddlMonth.DataTextField = "desc"
                ddlMonth.DataSource = myDS.Tables(0).DefaultView
                ddlMonth.DataBind()
            End If
        End If
        myDS = Nothing
        mySetting.ArrangeDDLSelectedIndex(ddlMonth, clsGlobalSetting.DDLSelection.SelectedValue, DatePart(DateInterval.Month, Now()))
        Session("SelectedMonth") = ddlMonth.SelectedValue

    End Sub

    Private Sub BindCalendar()

        If Trim(txtEmployeeCode.Text) = "" Then
            SetlinkPanelToDefault()
            lblresult.Text = "No employee selected!"
            Exit Sub
        End If

        Dim EmployeeCode As String = ""
        ssql = "Select Top 1 Employee_Profile_ID from Employee_CodeName_Vw Where Code = N'" & Trim(txtEmployeeCode.Text) & "' And [Name] = N'" & Trim(txtEmployeeName.Text).Replace("'", "''") & "' And Company_Profile_Code = '" & Session("Company") & "' Order By Effective_Date Desc"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            EmployeeCode = myDS.Tables(0).Rows(0).Item(0).ToString
        Else
            lblresult.Text = "[BindCalendar]Error: Data fail to retrieve..."
            myDS = Nothing
            Exit Sub
        End If
        myDS = Nothing

        'Bind Data calEventCalendar
        ssql = "Exec sp_tms_GetCalendarRecords '" & Session("Company") & "','WC [Working Calendar]','','','" & Session("SelectedYear").ToString & "','" & Session("SelectedMonth").ToString & "','Leave_Enquiry','" & EmployeeCode & "'"
        myDS10 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

        If myDS10.Tables.Count > 0 Then
            If myDS10.Tables(0).Rows.Count > 0 Then
                myDS10.Tables(0).PrimaryKey = New DataColumn() {myDS10.Tables(0).Columns("date")}
            End If
        End If

    End Sub

    Protected Sub calEventCalendar_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles calEventCalendar.DayRender

        Try
            Dim strEvents As New StringBuilder
            Dim strEdit As New StringBuilder
            Dim dr As DataRow
            i = i + 1

            'BindCalendar()

            strEvents.Append("<span>")
            Dim myLabel As New Label
            myLabel.ID = "lbl" & i
            dr = myDS10.Tables(0).Rows.Find(e.Day.Date)
            If Not dr Is Nothing Then
                strEvents.Append("<br />")
                'strEvents.Append("<br />" & dr("GenCode").ToString)
                myLabel.Text = dr("GenCode").ToString
            Else
                strEvents.Append("<br>&nbsp;")
                myLabel.Text = ""
            End If
            strEvents.Append("</span>")
            e.Cell.Controls.Add(New LiteralControl(strEvents.ToString))
            e.Cell.Controls.Add(myLabel)

            'Dim colorCode As System.Drawing.Color = System.Drawing.Color.FromName(dr("ColorCode").ToString)
            'e.Cell.BackColor = colorCode

            If dtSelectedDate = e.Day.Date Then
                dtSelectedDate = Nothing
                'mySetting.ArrangeDDLSelectedIndex(ddlKeepHistory, clsGlobalSetting.DDLSelection.SelectedText, Trim(myLabel.Text))
                'mySetting.ArrangeDDLSelectedIndex(ddlEdit, clsGlobalSetting.DDLSelection.SelectedText, Trim(myLabel.Text))
            End If
        Catch ex As Exception
            lblresult.Text = "[calEventCalendar_DayRender]Error: " & ex.Message
        End Try

    End Sub

    Protected Sub calEventCalendar_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles calEventCalendar.VisibleMonthChanged

        Session("SelectedYear") = e.NewDate.Year.ToString
        Session("SelectedMonth") = e.NewDate.Month.ToString
        BindCalendar()
        pnlDayInfo.Visible = False
        mySetting.ArrangeDDLSelectedIndex(ddlYear, clsGlobalSetting.DDLSelection.SelectedValue, e.NewDate.Year.ToString)
        mySetting.ArrangeDDLSelectedIndex(ddlMonth, clsGlobalSetting.DDLSelection.SelectedValue, e.NewDate.Month.ToString)

    End Sub

    Protected Sub calEventCalendar_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calEventCalendar.SelectionChanged

        Dim myDate As DateTime
        Dim sDay, sMonth, sYear, sDate, sContent As String
        dtSelectedDate = calEventCalendar.SelectedDate
        myDate = calEventCalendar.SelectedDate

        sDay = DatePart(DateInterval.Day, myDate)
        sMonth = DatePart(DateInterval.Month, myDate)
        sYear = DatePart(DateInterval.Year, myDate)

        If Len(sDay) = 1 And Len(sDay) < 2 Then
            sDay = "0" & sDay
        End If
        If Len(sMonth) = 1 And Len(sMonth) < 2 Then
            sMonth = "0" & sMonth
        End If

        sDate = sYear & sMonth & sDay & "000000"

        'validate & get employee info
        If Trim(txtEmployeeCode.Text) = "" Then
            SetlinkPanelToDefault()
            lblresult.Text = "No employee selected!"
            Exit Sub
        End If

        Dim EmployeeCode As String = ""
        ssql = "Select Top 1 Employee_Profile_ID from Employee_CodeName_Vw Where Code = N'" & Trim(txtEmployeeCode.Text) & "' And [Name] = N'" & Trim(txtEmployeeName.Text).Replace("'", "''") & "' And Company_Profile_Code = '" & Session("Company") & "' Order By Effective_Date Desc"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            EmployeeCode = myDS.Tables(0).Rows(0).Item(0).ToString
        Else
            lblresult.Text = "[BindCalendar]Error: Data fail to retrieve..."
            myDS = Nothing
            Exit Sub
        End If
        myDS = Nothing

        'rebind to get the data selected
        ssql = "Exec sp_tms_GetCalendarRecords '" & Session("Company") & "','WC [Working Calendar]','','','" & Session("SelectedYear").ToString & "','" & Session("SelectedMonth").ToString & "','Leave_Enquiry','" & EmployeeCode & "'"
        myDS3 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS3.Tables.Count > 0 Then
            If myDS3.Tables(0).Rows.Count > 0 Then
                For i = 0 To myDS3.Tables(0).Rows.Count - 1
                    If myDS3.Tables(0).Rows(i).Item(1).ToString = sDate Then
                        sContent = myDS3.Tables(0).Rows(i).Item(10).ToString
                        txtDateSel.Text = sDay.ToString & "/" & sMonth.ToString & "/" & sYear.ToString
                        'txtWorkDay.Text = myDS3.Tables(0).Rows(i).Item(4).ToString
                        pnlDayInfo.Visible = True
                        'get leave info if exist
                        If myDS3.Tables(0).Rows(i).Item(5).ToString <> "" Then
                            ssql = "Exec sp_ls_ReturnCalSelInfo 'type2','" & myDS3.Tables(0).Rows(i).Item(5).ToString & "','" & myDS3.Tables(0).Rows(i).Item(6).ToString & "','" & myDS3.Tables(0).Rows(i).Item(9).ToString & "'"
                            myDS4 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                            If myDS4.Tables.Count > 0 Then
                                gvLeaveInfo.DataSource = myDS4.Tables(0)
                                gvLeaveInfo.DataBind()
                            End If
                            myDS4 = Nothing
                        Else
                            gvLeaveInfo.DataSource = Nothing
                            gvLeaveInfo.DataBind()
                        End If
                        Exit For
                    End If
                Next
            End If
        End If
        myDS3 = Nothing

        BindCalendar()

    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlYear.SelectedIndexChanged

        Session("SelectedYear") = ddlYear.SelectedValue
        pnlDayInfo.Visible = False
        BindCalendar()
        calEventCalendar.VisibleDate = New Date(ddlYear.SelectedValue, ddlMonth.SelectedValue, 1)

    End Sub

    Protected Sub ddlMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMonth.SelectedIndexChanged

        Session("SelectedMonth") = ddlMonth.SelectedValue
        pnlDayInfo.Visible = False
        BindCalendar()
        calEventCalendar.VisibleDate = New Date(ddlYear.SelectedValue, ddlMonth.SelectedValue, 1)

    End Sub

#End Region

#Region "Panel Action"

    Protected Sub imgBtnSelect_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSelect.Click

        Try
            Dim getnum As Integer = CheckForSelectGv1()
            If getnum = 0 Then
                lblresult1.Text = "Invalid action: No row selected..."
                Exit Sub
            ElseIf getnum = 1 Then
                Try
                    For Each grdItem In myGridView1.Rows
                        chkSelected = grdItem.FindControl("chkSelect1")
                        If chkSelected.Checked Then
                            txtEmployee_Profile_ID.Text = CType(grdItem.FindControl("grvlblcode"), Label).Text
                            txtEmployee_Name.Text = CType(grdItem.FindControl("grvlblname"), Label).Text
                            txtEmployeeCode.Text = CType(grdItem.FindControl("grvlblcode"), Label).Text
                            txtEmployeeName.Text = CType(grdItem.FindControl("grvlblname"), Label).Text
                            chkSelected.Checked = False
                            lnkBtnViewInfo.Enabled = True
                            'LnkBtnViewInfo_Action() 'direct display when select
                            lnkBtnViewRecord.Enabled = True
                            LnkBtnViewRecord_Action() 'direct display when select
                            lnkBtnViewEnquiry.Enabled = True
                            lnkBtnViewCancellation.Enabled = True
                            pnlgridview1.Visible = False
                            pnlprevnext1.Visible = False
                            'reinitialize calendar control
                            InitializeEventCalendar()
                            Exit For
                        End If
                    Next
                Catch ex As Exception
                    lblresult1.Text = "Error: " & ex.Message
                End Try
            ElseIf getnum > 1 Then
                lblresult1.Text = "Invalid action: Only one row can be selected..."
            End If
        Catch ex As Exception
            lblresult.Text = "[imgBtnSelect_Click]Error: " & ex.Message
        End Try

    End Sub

    Protected Sub imgBtnClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnClear.Click

        txtDate_Apply_From.Text = ""
        txtDate_Apply_To.Text = ""
        txtReason.Text = ""
        ddlOption_Period.SelectedIndex = 0

    End Sub

    Protected Sub imgBtnSubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSubmit.Click

        Try
            Dim strLeaveTypeName, sOrganisation, sOrgdate As String
            strLeaveType = ""
            strLeaveTypeName = ""

            'get leave type form gv
            Dim getnum As Integer = CheckForSelectGv2()
            If getnum = 0 Then
                lblresult.Visible = True
                lblresult.Text = "Invalid action: No row selected..."
                Exit Sub
            ElseIf getnum = 1 Then
                Try
                    i = 0
                    For Each grdItem In myGridview2.Rows
                        chkSelected = grdItem.FindControl("chkSelect2")
                        If chkSelected.Checked Then
                            strLeaveType = Replace(myGridview2.Rows(i).Cells(1).Text, "amp;", "")
                            chkSelected.Checked = False
                            Exit For
                        End If
                        i = i + 1
                    Next
                Catch ex As Exception
                    lblresult1.Text = "Error: " & ex.Message
                End Try
            ElseIf getnum > 1 Then
                lblresult1.Text = "Invalid action: Only one row can be selected..."
            End If

            'get require data b4 apply
            ssql = "Exec sp_ls_GetEmpLeaveData '" & Session("Company") & "','" & Session("Module") & _
                    "','" & Trim(txtEmployeeCode.Text) & "','" & strLeaveType & "','" & Session("EmpID").ToString & "'"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables(0).Rows.Count > 0 Then
                If myDS.Tables(0).Rows(0).Item(0).ToString = "" Then
                    Session("curLeaveEmpID") = myDS.Tables(0).Rows(0).Item(1).ToString
                    Session("CurLeaveID") = myDS.Tables(0).Rows(0).Item(2).ToString
                    Session("CurLeaveName") = myDS.Tables(0).Rows(0).Item(3).ToString
                    Session("CurSysDate") = myDS.Tables(0).Rows(0).Item(4).ToString
                    Session("CurSysDateTime") = myDS.Tables(0).Rows(0).Item(5).ToString
                Else
                    Session("curLeaveEmpID") = ""
                    Session("CurLeaveID") = ""
                    Session("CurLeaveName") = ""
                    Session("CurSysDate") = ""
                    Session("CurSysDateTime") = ""
                    lblresult.Text = "Process Terminated! " & myDS.Tables(0).Rows(0).Item(0).ToString
                    myDS = Nothing
                    Exit Sub
                End If
            Else
                lblresult.Text = "[SysErr] Data fail to retrieve!"
                myDS = Nothing
                Exit Sub
            End If
            myDS = Nothing

            Dim dateapplychk As String

            If txtDate_Apply_From.Text = txtDate_Apply_To.Text Then
                dateapplychk = mySetting.UnDisplayDateTime(txtDate_Apply_From.Text, Session("Company").ToString, Session("Module").ToString)
            Else
                dateapplychk = mySetting.UnDisplayDateTime(txtDate_Apply_From.Text, Session("Company").ToString, Session("Module").ToString)
            End If
            'Validate employee status
            ssql = "Exec sp_is_chkResignStatus '" & Session("Company") & "','" _
                   & Session("curLeaveEmpID") & "','" & dateapplychk & "'"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

            If myDS.Tables(0).Rows.Count > 0 Then
                If myDS.Tables(0).Rows(0).Item(0).ToString = "RESIGN" Then
                    If myDS.Tables(1).Rows.Count > 0 Then
                        If myDS.Tables(1).Rows(0).Item(0).ToString > 31 Then
                            lblresult.Text = "This Employee has been resigned on " & myDS.Tables(0).Rows(0).Item(1).ToString
                            myDS = Nothing
                            Exit Sub
                        End If
                    Else
                        lblresult.Text = "This Employee has been resigned on " & myDS.Tables(0).Rows(0).Item(1).ToString
                        myDS = Nothing
                        Exit Sub
                    End If
                ElseIf myDS.Tables(0).Rows(0).Item(0).ToString = "INTER_TRANS" Or myDS.Tables(0).Rows(0).Item(0).ToString = "RESIGN_PAID" Then
                    lblresult.Text = "This Employee has been " & myDS.Tables(0).Rows(0).Item(2).ToString & " on " & myDS.Tables(0).Rows(0).Item(1).ToString
                    myDS = Nothing
                    Exit Sub
                End If
            End If
            myDS = Nothing


            'Validate employee current company
            ssql = "Exec sp_is_checkEmpCurrentLoc '" & Session("curLeaveEmpID") & "','B4Status',''"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables(0).Rows.Count > 0 Then
                sOrganisation = myDS.Tables(0).Rows(0).Item(0).ToString
                sOrgdate = myDS.Tables(0).Rows(0).Item(1).ToString

                If Session("Company") <> sOrganisation Then
                    ' employee oledi transfer to the other company
                    If sOrganisation <> "" And sOrgdate <> "" Then
                        lblresult.Text = "Employee already transfer to " & sOrganisation & " on " & sOrgdate
                        'exit update function
                        myDS = Nothing
                        Exit Sub
                    End If
                End If
            End If
            myDS = Nothing

            'field validation
            If ValidateInput() = True Then
                If CheckAvailableApplyLeave() = True Then
                    InsDateRangeApplication()
                    'data col
                End If
            End If

        Catch ex As Exception
            lblresult.Text = "[imgBtnSubmit_Click]Error: " & ex.Message
        End Try

    End Sub

    Protected Sub imgBtnApply_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnApply.Click

        Try
            'Dim strLeaveType, strStatus As String
            If ActionValidateDelete3() Then
                Dim getnum As Integer = 0
                Dim getnum1 As Integer = 0
                Dim errmsg As String = ""
                For i = 0 To myGridview3.Rows.Count - 1
                    Dim chkDelete As CheckBox = myGridview3.Rows(i).Cells(0).Controls(1)
                    If chkDelete.Checked Then
                        ssql = mySetting.GetSQLParameter2(Form.ID, clsGlobalSetting.SQLAction.DELETE_Statement3, myGridview4, i, Session("Company"), Session("Module"))
                        'Checking if status is pending
                        ssql1 = "Select * " & Right(ssql, Len(ssql) - 7) & " And Option_Status = 'PENDING'"
                        ssql2 = "Exec sp_ls_chkLeaveCancel '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & myGridview4.Rows(i).Cells(3).Text.ToString.Replace("&amp;", "&") & "'"
                        myDS = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
                        If myDS.Tables(0).Rows.Count > 0 Then
                            mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                            getnum += 1
                        Else
                            myDS1 = mySQL.ExecuteSQL(ssql2, Session("Company").ToString, Session("EmpID").ToString)
                            If myDS1.Tables(0).Rows(0).Item(0).ToString = "SUCESS" Then
                                mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                                getnum += 1
                            Else
                                getnum1 += 1
                            End If
                        End If
                        chkDelete.Checked = False
                        myDS = Nothing
                    End If
                Next
                Session("currentpage3") = "1"
                LnkBtnViewCancellation_Action()
                If getnum > 0 And getnum1 > 0 Then
                    errmsg = getnum & " data(s) delete successfully and " & getnum1 & " data(s) fail to delete..."
                ElseIf getnum > 0 Then
                    errmsg = getnum & " data(s) delete successfully..."
                ElseIf getnum1 > 0 Then
                    errmsg = getnum1 & " data(s) fail to delete..."
                End If
                lblresult.Text = errmsg
            End If
        Catch ex As Exception
            lblresult.Text = "[imgBtnApply_Click]Error: " & ex.Message
        End Try

    End Sub

    Protected Sub lnkBtnViewInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnViewInfo.Click

        LnkBtnViewInfo_Action()

    End Sub

    Protected Sub lnkBtnCloseInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnCloseInfo.Click

        lnkBtnCloseInfo.Visible = False
        lnkBtnViewInfo.Visible = True
        pnlempinfo.Visible = False
        lnkBtnCloseInfo.CssClass = "wordstyle"
        lblresult.Text = ""

    End Sub

    Protected Sub lnkBtnViewRecord_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnViewRecord.Click

        LnkBtnViewRecord_Action()

    End Sub

    Protected Sub lnkBtnCloseRecord_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnCloseRecord.Click

        lnkBtnCloseRecord_Action()

    End Sub

    Protected Sub lnkBtnViewEnquiry_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnViewEnquiry.Click

        If Trim(txtEmployeeCode.Text) = "" Then
            SetlinkPanelToDefault()
            lblresult.Text = "No employee selected!"
            Exit Sub
        End If

        lnkBtnCloseEnquiry.Visible = True
        lnkBtnViewEnquiry.Visible = False
        lnkBtnCloseEnquiry.CssClass = "wordstyle11"
        pnlCalendar.Visible = True
        lblleaveEnquiry.Visible = True
        lblleaveEnquiry.Text = "Leave Enquiry"
        lblleaveEnquiry.CssClass = "wordstyle11"
        'InitializeEventCalendar()
        BindCalendar()

        lnkBtnCloseRecord_Action()
        lnkBtnCloseCancellation_Action()

    End Sub

    Protected Sub lnkBtnCloseEnquiry_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnCloseEnquiry.Click

        lnkBtnCloseEnquiry_Action()

    End Sub

    Protected Sub lnkBtnViewCancellation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnViewCancellation.Click

        LnkBtnViewCancellation_Action()

    End Sub

    Protected Sub lnkBtnCloseCancellation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnCloseCancellation.Click

        lnkBtnCloseCancellation_Action()

    End Sub

    Protected Sub ddlOption_Status_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOption_Status.SelectedIndexChanged

        Session("currentPage3") = "1"
        LnkBtnViewCancellation_Action()

    End Sub


    Protected Sub ddlOption_Period_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOption_Period.SelectedIndexChanged

        If ddlOption_Period.SelectedValue = "TIME" Then
            pnlByTIME.Visible = True
        Else
            pnlByTIME.Visible = False
        End If
    End Sub

#End Region

#Region "Sub & Function"

    Sub LnkBtnViewInfo_Action()

        Try
            If Trim(txtEmployeeCode.Text) = "" Then
                SetlinkPanelToDefault()
                lblresult.Text = "No employee selected!"
                Exit Sub
            End If

            ssql = "exec sp_sa_leave_user_info '" & Session("Company") & "','" & txtEmployeeCode.Text & "'"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            lblresult.Text = ""
            If myDS.Tables(0).Rows.Count > 0 Then
                lblempinfo.Text = " Employee Information"
                lblempinfo.CssClass = "wordstyle11"
                lnkBtnViewInfo.Enabled = True
                pnlempinfo.Visible = True
                lnkBtnCloseInfo.Visible = True
                lnkBtnViewInfo.Visible = False
                lnkBtnCloseInfo.CssClass = "wordstyle11"

                lblEmployeeCode.Text = "Employee Code"
                lblEmployeeName.Text = "Employee Name"

                lblDateJoin.Text = myDS.Tables(0).Columns(1).ToString
                If Trim(myDS.Tables(0).Rows(0).Item(1)) = "" Then
                    txtDateJoin.Text = "Not Available"
                Else
                    txtDateJoin.Text = myDS.Tables(0).Rows(0).Item(1)
                End If

                lblDateConfirm.Text = myDS.Tables(0).Columns(2).ToString
                If Trim(myDS.Tables(0).Rows(0).Item(2)) = "" Then
                    txtDateConfirm.Text = "Not Available"
                Else
                    txtDateConfirm.Text = myDS.Tables(0).Rows(0).Item(2)
                End If

                lblLenghtOfService.Text = myDS.Tables(0).Columns(3).ToString
                If Trim(myDS.Tables(0).Rows(0).Item(3)) = "" Then
                    txtLenghtOfService.Text = "Not Available"
                Else
                    txtLenghtOfService.Text = myDS.Tables(0).Rows(0).Item(3)
                End If

                lblStatus.Text = myDS.Tables(0).Columns(4).ToString
                If Trim(myDS.Tables(0).Rows(0).Item(4)) = "" Then
                    txtStatus.Text = "Not Available"
                Else
                    txtStatus.Text = myDS.Tables(0).Rows(0).Item(4)
                End If

                lblResignDate.Text = myDS.Tables(0).Columns(5).ToString
                If Trim(myDS.Tables(0).Rows(0).Item(5)) = "" Then
                    txtResignDate.Text = "Not Available"
                Else
                    txtResignDate.Text = myDS.Tables(0).Rows(0).Item(5)
                End If

                lblDepartment.Text = myDS.Tables(0).Columns(6).ToString
                If Trim(myDS.Tables(0).Rows(0).Item(6)) = "" Then
                    txtDepartment.Text = "Not Available"
                Else
                    txtDepartment.Text = myDS.Tables(0).Rows(0).Item(6)
                End If

                lblLine.Text = myDS.Tables(0).Columns(7).ToString
                If Trim(myDS.Tables(0).Rows(0).Item(7)) = "" Then
                    txtLine.Text = "Not Available"
                Else
                    txtLine.Text = myDS.Tables(0).Rows(0).Item(7)
                End If

                lblJobGrade.Text = myDS.Tables(0).Columns(8).ToString
                If Trim(myDS.Tables(0).Rows(0).Item(8)) = "" Then
                    txtJobGrade.Text = "Not Available"
                Else
                    txtJobGrade.Text = myDS.Tables(0).Rows(0).Item(8)
                End If
                lblJobGrade.Visible = False
                lblJobGradeQ.Visible = False
                txtJobGrade.Visible = False

                lblJobTitle.Text = myDS.Tables(0).Columns(9).ToString
                If Trim(myDS.Tables(0).Rows(0).Item(9)) = "" Then
                    txtJobTitle.Text = "Not Available"
                Else
                    txtJobTitle.Text = myDS.Tables(0).Rows(0).Item(9)
                End If
                lblJobTitle.Visible = False
                lblJobTitleQ.Visible = False
                txtJobTitle.Visible = False

                lblSupervisor.Text = myDS.Tables(0).Columns(10).ToString
                If Trim(myDS.Tables(0).Rows(0).Item(10)) = "" Then
                    txtSupervisor.Text = "Not Available"
                Else
                    txtSupervisor.Text = myDS.Tables(0).Rows(0).Item(10)
                End If
            Else
                pnlempinfo.Visible = False
                lnkBtnViewInfo.Enabled = False
                lblresult.Text = "Employee information not available..."
            End If
            myDS = Nothing
        Catch ex As Exception
            lblresult.Text = "[LnkBtnViewInfo_Action]Error: " & ex.Message
        End Try

    End Sub

    Sub LnkBtnViewRecord_Action()

        Try
            If Trim(txtEmployeeCode.Text) = "" Then
                SetlinkPanelToDefault()
                lblresult.Text = "No employee selected!"
                Exit Sub
            End If

            btnColourDef = Session("strTheme")
            btnColourAlt = Session("strThemeAlt")

            Dim EmployeeCodeField As String = "Employee_Profile_ID"
            Dim EmployeeCode As String = ""
            Dim TmsID As String = ""
            ssql = "Select Top 1 a.Employee_Profile_ID,dbo.fn_GetOCPCodeByID(b.OCP_ID_TMS),c.option_pay_Cycle from Employee_CodeName_Vw a, employee_status b,employee_salary c Where a.employee_profile_id = b.employee_profile_id and b.employee_profile_id = c.employee_profile_id and Code = N'" & Trim(txtEmployeeCode.Text) & "' And [Name] = N'" & Trim(txtEmployeeName.Text).Replace("'", "''") & "' And a.Company_Profile_Code = '" & Session("Company") & "' Order By Effective_Date Desc"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables(0).Rows.Count > 0 Then
                EmployeeCode = myDS.Tables(0).Rows(0).Item(0).ToString
                Select Case myDS.Tables(0).Rows(0).Item(1).ToString
                    Case "A", "B", "C", "YA", "YB", "YC", "ZA", "ZB", "ZC"
                        ddlOption_Period.SelectedValue = "FULL"
                        ddlOption_Period.Enabled = False
                    Case Else
                        ddlOption_Period.Enabled = True
                End Select
                Session("PayCycle") = myDS.Tables(0).Rows(0).Item(2).ToString
            Else
                lblresult.Text = "[LnkBtnViewRecord_Action]Error: Data fail to retrieve..."
                myDS = Nothing
                Exit Sub
            End If
            myDS = Nothing

            mySetting.GetLookupValue_ImageButton(imgBtnStandby_Employee, Form.ID, "txtStandby_Employee", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """,""Standby_Employee""," & """" & EmployeeCode & """," & """" & Session("EmpID").ToString & """")

            ssql = "Exec sp_ls_cal_emp_leave_balance '" & Session("Company") & "','" & EmployeeCode & "'"
            mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

            'ssql = "exec sp_sa_getTableRecords2 '" & Session("Company") & "','" & Session("Module") & "','Employee_Leave_Balance','" & myGridview2.PageSize & "','" & Session("currentpage2") & "','[" & EmployeeCodeField & "]','" & EmployeeCode & "','','','','','','','','','" & Session("EmpID").ToString & "'"
            ssql = "exec sp_sa_getTableRecords2 '" & Session("Company") & "','" & Session("Module") & "','Employee_Leave_Balance','" & myGridview2.PageSize & "','" & Session("currentpage2") & "','[" & EmployeeCodeField & "]','" & EmployeeCode & "','ENTITLEMENT_YEAR','" & System.DateTime.Now.Year.ToString() & "','','','','','','','" & Session("EmpID").ToString & "'"
            
myDS2 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            lblresult.Text = ""
            If myDS2.Tables(0).Rows.Count > 0 Then
                lblleaveSummary.Visible = True
                lblleaveSummary.Text = "Leave Summary"
                lblleaveSummary.CssClass = "wordstyle11"
                pnlgridview2.Visible = True
                lnkBtnCloseRecord.Visible = True
                lnkBtnViewRecord.Visible = False
                lnkBtnViewRecord.Enabled = True
                lnkBtnCloseRecord.CssClass = "wordstyle11"
                lnkBtnCloseEnquiry_Action()
                lnkBtnCloseCancellation_Action()

                If myDS2.Tables.Count > 1 And CInt(myDS2.Tables(1).Rows.Count) > 0 Then
                    myGridview2.DataSource = myDS2.Tables(1)
                    myGridview2.DataBind()
                    pnlpart2.Visible = True
                    pnlgridview2.Visible = True
                    pnlprevnext2.Visible = True
                    pnlaction.Visible = True
                    lblresult.Text = ""
                    imgBtnGoToPage2.Enabled = True
                    mySetting.GetBtnImgUrl(imgBtnGoToPage2, Session("Company").ToString, btnColourDef, "btnGo.png")

                    'GridView Column Width Setting
                    'logic = False
                    'myDS1 = mySetting.GetGridViewWidth("employee_leave_balance")
                    'myDT1 = myDS1.Tables(0)
                    'myDT2 = myDS1.Tables(1)
                    'Dim value As Decimal
                    'If myDT2.Rows.Count > 0 Then

                    '    If CInt(myDT1.Rows(0).Item(1).ToString) < CInt(Session("GVwidth")) Then
                    '        If CInt(myDT1.Rows(0).Item(1).ToString) = 0 Then
                    '            logic = True
                    '        Else
                    '            value = CInt(Session("GVwidth")) / CInt(myDT1.Rows(0).Item(1).ToString)
                    '        End If
                    '    Else
                    '        value = 1
                    '        myGridview2.Width = CInt(myDT1.Rows(0).Item(1).ToString)
                    '    End If
                    '    If logic = False Then
                    '        For i = 0 To myDT2.Rows.Count - 1
                    '            j = CInt(myDT2.Rows(i).Item(0).ToString)
                    '            If CInt(myDT2.Rows(i).Item(2).ToString) = 0 Then
                    '                myGridview2.Rows(0).Cells(j).Width = 50 * value
                    '            Else
                    '                myGridview2.Rows(0).Cells(j).Width = CInt(myDT2.Rows(i).Item(2).ToString) * value
                    '            End If
                    '        Next
                    '    End If
                    'End If
                    'myDS1 = Nothing
                    'myDT1 = Nothing
                    'myDT2 = Nothing

                    lbltotal2.Text = " ( " & myDS2.Tables(0).Rows(0).Item(0) & " record(s) ) "
                    CurrentPage2.Text = Session("currentPage2")

                    _totalRecords = myDS2.Tables(0).Rows(0).Item(0)
                    _totalPage = _totalRecords / myGridview2.PageSize
                    TotalPages2.Text = (System.Math.Ceiling(_totalPage)).ToString()
                    _totalPage = Double.Parse(TotalPages2.Text)
                    Session("TotalPages2") = TotalPages2.Text

                    If Session("currentpage2") = 1 Then
                        FirstPage2.Enabled = False
                        PrevPage2.Enabled = False
                        If _totalRecords > myGridview2.PageSize Then
                            NextPage2.Enabled = True
                            LastPage2.Enabled = True
                        Else
                            NextPage2.Enabled = False
                            LastPage2.Enabled = False
                        End If
                    ElseIf Session("currentpage2") > 1 Then
                        FirstPage2.Enabled = True
                        PrevPage2.Enabled = True
                        If Session("currentpage2") = Session("TotalPages2") Then
                            NextPage2.Enabled = False
                            LastPage2.Enabled = False
                        Else
                            NextPage2.Enabled = True
                            LastPage2.Enabled = True
                        End If
                    End If
                    'If Session("currentpage2") + 1 = Session("TotalPages2") Then
                    '    NextPage2.Enabled = False
                    'End If
                    'If Session("currentpage2") - 1 = 1 Then
                    '    PrevPage2.Enabled = False
                    'End If
                Else
                    pnlgridview2.Visible = False
                    pnlprevnext2.Visible = False
                    pnlaction.Visible = False
                    lnkBtnCloseRecord.Visible = False
                    lnkBtnViewRecord.Visible = True
                    lnkBtnViewRecord.Enabled = False
                    lblresult.Text = "Leave summary not available..."
                    imgBtnGoToPage2.Enabled = False
                    mySetting.GetBtnImgUrl(imgBtnGoToPage2, Session("Company").ToString, btnColourAlt, "btnGo.png")
                End If
                myDS2 = Nothing
            End If
        Catch ex As Exception
            lblresult.Text = "[LnkBtnViewRecord_Action]Error: " & ex.Message
        End Try

    End Sub

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            Dim HeaderGrid As GridView = CType(sender, GridView)
            Dim HeaderGridRow As GridViewRow = New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell As TableCell = New TableCell()

            HeaderCell = New TableCell()
            HeaderCell.Text = " "
            HeaderCell.RowSpan = 1
            HeaderCell.HorizontalAlign = HorizontalAlign.Left
            HeaderGridRow.Cells.Add(HeaderCell)
            HeaderCell = New TableCell()
            HeaderCell.Text = " "
            HeaderCell.RowSpan = 1
            HeaderCell.HorizontalAlign = HorizontalAlign.Left
            HeaderGridRow.Cells.Add(HeaderCell)
            HeaderCell = New TableCell()
            HeaderCell.Text = " "
            HeaderCell.RowSpan = 1
            HeaderCell.HorizontalAlign = HorizontalAlign.Left
            HeaderGridRow.Cells.Add(HeaderCell)
            HeaderCell = New TableCell()
            HeaderCell.Text = Year(Now())
            HeaderCell.ColumnSpan = 5
            HeaderCell.HorizontalAlign = HorizontalAlign.Center
            HeaderGridRow.Cells.Add(HeaderCell)

            myGridview2.Controls(0).Controls.AddAt(0, HeaderGridRow)
            e.Row.Cells(2).Visible = False
        End If
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            e.Row.Cells(2).Visible = False
        End If
    End Sub

    Sub LnkBtnViewCancellation_Action()

        Try
            If Trim(txtEmployeeCode.Text) = "" Then
                SetlinkPanelToDefault()
                lblresult.Text = "No employee selected!"
                Exit Sub
            End If

            btnColourDef = Session("strTheme")
            btnColourAlt = Session("strThemeAlt")

            lnkBtnCloseRecord_Action()
            lnkBtnCloseEnquiry_Action()

            Dim EmployeeCodeField As String = "Employee_Profile_ID"
            Dim EmployeeCode As String = ""
            ssql = "Select Top 1 Employee_Profile_ID from Employee_CodeName_Vw Where Code = N'" & Trim(txtEmployeeCode.Text) & "' And [Name] = N'" & Trim(txtEmployeeName.Text).Replace("'", "''") & "' And Company_Profile_Code = '" & Session("Company") & "' Order By Effective_Date Desc"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables(0).Rows.Count > 0 Then
                EmployeeCode = myDS.Tables(0).Rows(0).Item(0).ToString
            Else
                lblresult.Text = "[LnkBtnViewCancellation_Action]Error: Data fail to retrieve..."
                myDS = Nothing
                Exit Sub
            End If
            myDS = Nothing

            Dim OptionStatusField As String = "Option_Status"
            Dim OptionStatus As String = ddlOption_Status.SelectedValue
            If OptionStatus = "BOTH" Then
                OptionStatus = ""
            End If

            ssql = "exec sp_sa_getTableRecords2 '" & Session("Company") & "','" & Session("Module") & "','Employee_Leave_Application','" & myGridview3.PageSize & "','" & Session("currentpage3") & "','[" & EmployeeCodeField & "]','" & EmployeeCode & "','[" & OptionStatusField & "]','" & OptionStatus & "'"
            myDS2 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            ssql = "exec sp_sa_compareTableRecords2 '" & Session("Company") & "','" & Session("Module") & "','Employee_Leave_Application','" & myGridview3.PageSize & "','" & Session("currentpage3") & "','[" & EmployeeCodeField & "]','" & EmployeeCode & "','[" & OptionStatusField & "]','" & OptionStatus & "'"
            myDS3 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            lblresult.Text = ""
            If myDS2.Tables(0).Rows.Count > 0 Then
                lblleaveCancellation.Visible = True
                lblleaveCancellation.Text = "Leave Cancellation"
                lblleaveCancellation.CssClass = "wordstyle11"
                lnkBtnCloseCancellation.Visible = True
                lnkBtnViewCancellation.Visible = False
                'lnkBtnViewCancellation.Enabled = True
                lnkBtnCloseCancellation.CssClass = "wordstyle11"
                myGridview3.DataSource = myDS2.Tables(1)
                myGridview3.DataBind()
                myGridview4.DataSource = myDS3.Tables(1)
                myGridview4.DataBind()
                pnlCancellation.Visible = True
                lblresult.Text = ""
                If myDS2.Tables.Count > 1 And CInt(myDS2.Tables(1).Rows.Count) > 0 Then
                    imgBtnGoToPage3.Enabled = True
                    mySetting.GetBtnImgUrl(imgBtnGoToPage3, Session("Company").ToString, btnColourDef, "btnGo.png")

                    'GridView Column Width Setting
                    'logic = False
                    'myDS1 = mySetting.GetGridViewWidth("employee_leave_application")
                    'myDT1 = myDS1.Tables(0)
                    'myDT2 = myDS1.Tables(1)
                    'Dim value As Decimal
                    'If myDT2.Rows.Count > 0 Then

                    '    If CInt(myDT1.Rows(0).Item(1).ToString) < CInt(Session("GVwidth")) Then
                    '        If CInt(myDT1.Rows(0).Item(1).ToString) = 0 Then
                    '            logic = True
                    '        Else
                    '            value = CInt(Session("GVwidth")) / CInt(myDT1.Rows(0).Item(1).ToString)
                    '        End If
                    '    Else
                    '        value = 1
                    '        myGridview3.Width = CInt(myDT1.Rows(0).Item(1).ToString)
                    '    End If
                    '    If logic = False Then
                    '        For i = 0 To myDT2.Rows.Count - 1
                    '            j = CInt(myDT2.Rows(i).Item(0).ToString)
                    '            If CInt(myDT2.Rows(i).Item(2).ToString) = 0 Then
                    '                myGridview3.Rows(0).Cells(j).Width = 50 * value
                    '            Else
                    '                myGridview3.Rows(0).Cells(j).Width = CInt(myDT2.Rows(i).Item(2).ToString) * value
                    '            End If
                    '        Next
                    '    End If
                    'End If
                    'myDS1 = Nothing
                    'myDT1 = Nothing
                    'myDT2 = Nothing

                    lbltotal3.Text = " ( " & myDS2.Tables(0).Rows(0).Item(0) & " record(s) ) "
                    'If CInt(CurrentPage3.Text) *  myGridview2.PageSize
                    '(CInt(CurrentPage3.Text) *  (myGridview2.PageSize - 1)) + 1
                    '(CInt(CurrentPage3.Text) *  (myGridview2.PageSize)) - 1
                    CurrentPage3.Text = Session("currentPage3")

                    _totalRecords = myDS2.Tables(0).Rows(0).Item(0)
                    _totalPage = _totalRecords / myGridview2.PageSize
                    TotalPages3.Text = (System.Math.Ceiling(_totalPage)).ToString()
                    _totalPage = Double.Parse(TotalPages3.Text)
                    Session("TotalPages3") = TotalPages3.Text

                    If Session("currentpage3") = 1 Then
                        FirstPage3.Enabled = False
                        PrevPage3.Enabled = False
                        If _totalRecords > myGridview3.PageSize Then
                            NextPage3.Enabled = True
                            LastPage3.Enabled = True
                        Else
                            NextPage3.Enabled = False
                            LastPage3.Enabled = False
                        End If
                    ElseIf Session("currentpage3") > 1 Then
                        FirstPage3.Enabled = True
                        PrevPage3.Enabled = True
                        If Session("currentpage3") = Session("TotalPages3") Then
                            NextPage3.Enabled = False
                            LastPage3.Enabled = False
                        Else
                            NextPage3.Enabled = True
                            LastPage3.Enabled = True
                        End If
                    End If
                Else
                    If OptionStatus = "" Then
                        pnlCancellation.Visible = False
                        lnkBtnCloseCancellation.Visible = False
                        lnkBtnViewCancellation.Visible = True
                        lnkBtnViewCancellation.Enabled = False
                        imgBtnGoToPage3.Enabled = False
                        mySetting.GetBtnImgUrl(imgBtnGoToPage3, Session("Company").ToString, btnColourAlt, "btnGo.png")
                    End If
                    lblresult.Text = "Leave record not available..."
                End If
                myDS2 = Nothing
            Else

            End If
        Catch ex As Exception
            lblresult.Text = "[LnkBtnViewCancellation_Action]Error: " & ex.Message
        End Try

    End Sub

    Sub lnkBtnCloseRecord_Action()

        lnkBtnCloseRecord.Visible = False
        lnkBtnViewRecord.Visible = True
        lnkBtnViewRecord.CssClass = "wordstyle"
        pnlpart2.Visible = False
        pnlgridview2.Visible = False
        pnlprevnext2.Visible = False
        lblresult.Text = ""
        pnlaction.Visible = False
        lblleaveSummary.Visible = False

    End Sub

    Sub lnkBtnCloseEnquiry_Action()

        lnkBtnCloseEnquiry.Visible = False
        lnkBtnViewEnquiry.Visible = True
        lnkBtnViewEnquiry.CssClass = "wordstyle"
        pnlCalendar.Visible = False
        pnlDayInfo.Visible = False

    End Sub

    Sub lnkBtnCloseCancellation_Action()

        lnkBtnCloseCancellation.Visible = False
        lnkBtnViewCancellation.Visible = True
        lnkBtnViewCancellation.CssClass = "wordstyle"
        pnlCancellation.Visible = False

    End Sub

    Sub SetlinkPanelToDefault()
        pnlgridview1.Visible = False
        pnlprevnext1.Visible = False
        pnlgridview2.Visible = False
        pnlprevnext2.Visible = False
        pnlempinfo.Visible = False
        pnlaction.Visible = False
        pnlpart2.Visible = False
        pnlCalendar.Visible = False
        pnlCancellation.Visible = False
        pnlByTIME.Visible = False

        lnkBtnCloseInfo.Visible = False
        lnkBtnCloseRecord.Visible = False
        lnkBtnCloseEnquiry.Visible = False
        lnkBtnCloseCancellation.Visible = False
        lnkBtnViewInfo.Visible = True
        lnkBtnViewRecord.Visible = True
        lnkBtnViewEnquiry.Visible = True
        lnkBtnViewCancellation.Visible = True
        lnkBtnViewInfo.Enabled = False
        lnkBtnViewRecord.Enabled = False
        lnkBtnViewEnquiry.Enabled = False
        lnkBtnViewCancellation.Enabled = False

    End Sub

    Sub SetlinkPanelToDefault2()

        pnlgridview1.Visible = False
        pnlprevnext1.Visible = False
        pnlgridview2.Visible = False
        pnlprevnext2.Visible = False
        pnlempinfo.Visible = False
        pnlaction.Visible = False
        pnlpart2.Visible = False

        lnkBtnCloseInfo.Visible = False
        lnkBtnCloseRecord.Visible = False
        lnkBtnCloseEnquiry.Visible = False
        lnkBtnViewInfo.Visible = True
        lnkBtnViewRecord.Visible = True
        lnkBtnViewEnquiry.Visible = True
        'lnkBtnViewInfo.Enabled = False
        'lnkBtnViewRecord.Enabled = False
        'lnkBtnViewEnquiry.Enabled = False

    End Sub

    Sub BindGridInfo()

        Try
            If Trim(txtEmployee_Profile_ID.Text) <> "Employee Code" And Trim(txtEmployee_Name.Text) <> "Employee Name" Then
                ssql = "exec sp_Generate_Query_Filter '" & Session("Company") & "','" & Session("Module") & "','Employee_Personal_CodeName_Vw@@Employee_Personal_CodeName_Vw','code@@Name','" & Trim(txtEmployee_Profile_ID.Text) & "@@" & Trim(txtEmployee_Name.Text.Replace("'", "''''")) & "','" & myGridView1.PageSize & "','" & Session("currentpage1") & "','" & Session("EmpID") & "'"
            ElseIf Trim(txtEmployee_Profile_ID.Text) <> "Employee Code" Then
                ssql = "exec sp_Generate_Query_Filter '" & Session("Company") & "','" & Session("Module") & "','Employee_Personal_CodeName_Vw','code','" & Trim(txtEmployee_Profile_ID.Text) & "','" & myGridView1.PageSize & "','" & Session("currentpage1") & "','" & Session("EmpID") & "'"
            ElseIf Trim(txtEmployee_Name.Text) <> "Employee Name" Then
                ssql = "exec sp_Generate_Query_Filter '" & Session("Company") & "','" & Session("Module") & "','Employee_Personal_CodeName_Vw','name','" & Trim(txtEmployee_Name.Text.Replace("'", "''''")) & "','" & myGridView1.PageSize & "','" & Session("currentpage1") & "','" & Session("EmpID") & "'"
            End If

            btnColourDef = Session("strTheme")
            btnColourAlt = Session("strThemeAlt")

            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables.Count > 1 And CInt(myDS.Tables(0).Rows(0).Item(0)) > 0 Then
                myGridView1.DataSource = myDS.Tables(1)
                myGridView1.DataBind()
                pnlgridview1.Visible = True
                pnlprevnext1.Visible = True
                lblresult.Text = ""
                imgBtnSelect.Enabled = True
                imgBtnGoToPage1.Enabled = True
                mySetting.GetBtnImgUrl(imgBtnSelect, Session("Company").ToString, btnColourDef, "btnSelect.png")
                mySetting.GetBtnImgUrl(imgBtnGoToPage1, Session("Company").ToString, btnColourDef, "btnGo.png")

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

                If CInt(myDS.Tables(0).Rows(0).Item(0).ToString) = 1 Then
                    For Each grdItem In myGridView1.Rows
                        txtEmployee_Profile_ID.Text = CType(grdItem.FindControl("grvlblcode"), Label).Text
                        txtEmployee_Name.Text = CType(grdItem.FindControl("grvlblname"), Label).Text
                        txtEmployeeCode.Text = CType(grdItem.FindControl("grvlblcode"), Label).Text
                        txtEmployeeName.Text = CType(grdItem.FindControl("grvlblname"), Label).Text
                        lnkBtnViewInfo.Enabled = True
                        'LnkBtnViewInfo_Action() 'direct display when select
                        lnkBtnViewRecord.Enabled = True
                        LnkBtnViewRecord_Action() 'direct display when select
                        lnkBtnViewEnquiry.Enabled = True
                        lnkBtnViewCancellation.Enabled = True
                        pnlgridview1.Visible = False
                        pnlprevnext1.Visible = False
                        Exit For
                    Next
                End If

            Else
                pnlgridview1.Visible = False
                pnlprevnext1.Visible = False
                imgBtnSelect.Enabled = False
                imgBtnGoToPage1.Enabled = False
                mySetting.GetBtnImgUrl(imgBtnSelect, Session("Company").ToString, btnColourAlt, "btnSelect.png")
                mySetting.GetBtnImgUrl(imgBtnGoToPage1, Session("Company").ToString, btnColourAlt, "btnGo.png")
                lblresult.Text = "No data found..."
            End If
            myDS = Nothing
        Catch ex As Exception
            myDS = Nothing
            lblresult.Text = "[BindGridInfo]Error: " & ex.Message
        End Try

    End Sub

    Sub BindGridleave()

        LnkBtnViewRecord_Action()

    End Sub

    Sub BindGridCancellation()

        LnkBtnViewCancellation_Action()

    End Sub

    Sub ClearActionField()
        txtDate_Apply_From.Text = ""
        txtDate_Apply_To.Text = ""
        ddlOption_Period.SelectedIndex = 0
        txtReason.Text = ""
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
        BindGridInfo()

    End Sub

    Public Sub NavigationLink2_Click(ByVal sender As Object, ByVal e As CommandEventArgs)

        Select Case e.CommandName
            Case "First"
                Session("currentpage2") = 1
            Case "Prev"
                Session("currentpage2") = Integer.Parse(CurrentPage2.Text) - 1
            Case "Next"
                Session("currentpage2") = Integer.Parse(CurrentPage2.Text) + 1
            Case "Last"
                Session("currentpage2") = Integer.Parse(TotalPages2.Text)
        End Select
        lblresult.Text = ""
        BindGridleave()

    End Sub

    Public Sub NavigationLink3_Click(ByVal sender As Object, ByVal e As CommandEventArgs)

        Select Case e.CommandName
            Case "First"
                Session("currentpage3") = 1
            Case "Prev"
                Session("currentpage3") = Integer.Parse(CurrentPage3.Text) - 1
            Case "Next"
                Session("currentpage3") = Integer.Parse(CurrentPage3.Text) + 1
            Case "Last"
                Session("currentpage3") = Integer.Parse(TotalPages3.Text)
        End Select
        lblresult.Text = ""
        BindGridCancellation()

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
                    BindGridInfo()
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

    Protected Sub imgBtnGoToPage2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnGoToPage2.Click

        If txtGoToPage2.Text = "" Then
            lblresult.Text = "Request failed! Value required..."
            txtGoToPage2.Focus()
            Exit Sub
        End If

        Try
            Dim pagestr As String = Trim(txtGoToPage2.Text)
            Dim pagenum As Integer = CInt(pagestr)
            Dim firstnum As Integer = 0
            Dim lastnum As Integer = 0

            If pagenum <= CInt(Session("TotalPages2")) And pagenum > 0 Then
                If pagenum = CInt(Session("currentPage2")) Then
                    lblresult.Text = "Request failed! You are looking for the same page..."
                    txtGoToPage2.Focus()
                Else
                    lblresult.Text = ""
                    Session("currentpage2") = pagenum
                    BindGridleave()
                End If
                txtGoToPage2.Text = ""
            Else
                If pagenum = 1 Or pagenum > CInt(Session("TotalPages2")) And CInt(Session("TotalPages2")) = 1 Then
                    lblresult.Text = "Request failed! Only one page available..."
                Else
                    If CInt(Session("currentPage2")) = 1 Then
                        firstnum = 1
                    Else
                        firstnum = 0
                    End If
                    If CInt(Session("currentPage2")) = CInt(Session("TotalPages2")) Then
                        lastnum = CInt(Session("TotalPages2"))
                    Else
                        lastnum = CInt(Session("TotalPages2")) + 1
                    End If
                    lblresult.Text = "Request failed! Page number must between " & firstnum & " and " & lastnum & "..."
                End If
                txtGoToPage2.Text = ""
                txtGoToPage2.Focus()
            End If

        Catch ex As Exception
            lblresult.Text = "Request failed! Invalid value enter..."
            txtGoToPage2.Text = ""
            txtGoToPage2.Focus()
            Exit Sub
        End Try

    End Sub

    Protected Sub imgBtnGoToPage3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnGoToPage3.Click

        If txtGoToPage3.Text = "" Then
            lblresult.Text = "Request failed! Value required..."
            txtGoToPage3.Focus()
            Exit Sub
        End If

        Try
            Dim pagestr As String = Trim(txtGoToPage3.Text)
            Dim pagenum As Integer = CInt(pagestr)
            Dim firstnum As Integer = 0
            Dim lastnum As Integer = 0

            If pagenum <= CInt(Session("TotalPages3")) And pagenum > 0 Then
                If pagenum = CInt(Session("currentPage3")) Then
                    lblresult.Text = "Request failed! You are looking for the same page..."
                    txtGoToPage3.Focus()
                Else
                    lblresult.Text = ""
                    Session("currentpage3") = pagenum
                    BindGridCancellation()
                End If
                txtGoToPage3.Text = ""
            Else
                If pagenum = 1 Or pagenum > CInt(Session("TotalPages3")) And CInt(Session("TotalPages3")) = 1 Then
                    lblresult.Text = "Request failed! Only one page available..."
                Else
                    If CInt(Session("currentPage3")) = 1 Then
                        firstnum = 1
                    Else
                        firstnum = 0
                    End If
                    If CInt(Session("currentPage3")) = CInt(Session("TotalPages3")) Then
                        lastnum = CInt(Session("TotalPages3"))
                    Else
                        lastnum = CInt(Session("TotalPages3")) + 1
                    End If
                    lblresult.Text = "Request failed! Page number must between " & firstnum & " and " & lastnum & "..."
                End If
                txtGoToPage3.Text = ""
                txtGoToPage3.Focus()
            End If

        Catch ex As Exception
            lblresult.Text = "Request failed! Invalid value enter..."
            txtGoToPage3.Text = ""
            txtGoToPage3.Focus()
            Exit Sub
        End Try

    End Sub

    Private Function CheckForSelectGv1() As Integer

        Dim v_num As Integer = 0

        Try
            For Each grdItem In myGridView1.Rows
                chkSelected = grdItem.FindControl("chkSelect1")
                If chkSelected.Checked = True Then
                    v_num += 1
                End If
            Next
            Return v_num
        Catch ex As Exception
            pnlresult.Visible = True
            lblresult.Text = ""
            lblresult.Text = "[CheckForSelectGv1]Error: " & ex.Message
        End Try

    End Function

    Private Function CheckForSelectGv2() As Integer

        Dim v_num As Integer = 0

        Try
            For Each grdItem In myGridview2.Rows
                chkSelected = grdItem.FindControl("chkSelect2")
                If chkSelected.Checked = True Then
                    v_num += 1
                End If
            Next
            Return v_num
        Catch ex As Exception
            pnlresult.Visible = True
            lblresult.Text = ""
            lblresult.Text = "[CheckForSelectGv2]Error: " & ex.Message
        End Try

    End Function

    Private Function CheckForSelectGv3() As Integer

        Dim v_num As Integer = 0

        Try
            For Each grdItem In myGridview3.Rows
                chkSelected = grdItem.FindControl("chkSelect3")
                If chkSelected.Checked = True Then
                    v_num += 1
                End If
            Next
            Return v_num
        Catch ex As Exception
            pnlresult.Visible = True
            lblresult.Text = ""
            lblresult.Text = "[CheckForSelectGv3]Error: " & ex.Message
        End Try

    End Function

    Private Function ActionValidateDelete3() As Boolean

        RecFound = False
        For i = 0 To myGridview3.Rows.Count - 1
            Dim chkDelete As CheckBox = myGridview3.Rows(i).Cells(0).Controls(1)
            If chkDelete.Checked Then
                RecFound = True
                Exit For
            End If
        Next
        If RecFound = True Then
            Return True
        Else
            lblresult.Text = "Invalid action: No row selected for deleting..."
            Return False
        End If

    End Function

#End Region

#Region "Sub & Function 2"

    Sub Initialize()

        Action = ""

        lstDateApply.Items.Clear()
        txtReason.Text = ""
        txtDate_Apply_From.Text = ""
        txtDate_Apply_To.Text = ""
        txtTime_Apply_For.Text = ""
        txtTime_Apply_To.Text = ""
        txtStandby_Employee.Text = ""
        'ddlOption_Period.SelectedIndex = 0

    End Sub

    Private Function ValidateInput() As Boolean

        Try
            Dim strFromDate As String = Trim(txtDate_Apply_From.Text)
            Dim strToDate As String = Trim(txtDate_Apply_To.Text)
            strFromTime = Trim(txtTime_Apply_For.Text)
            strToTime = Trim(txtTime_Apply_To.Text)
            Dim strNow As String = Session("CurSysDate")
            Dim strCutOffDate As String = strNow

            If Trim(txtEmployeeCode.Text) = "" Then
                SetlinkPanelToDefault()
                lblresult.Text = "No employee selected!"
                Exit Function
            End If

            'field 1 & 2
            If imgDate_Apply_For.Visible = True Then
                If txtDate_Apply_From.Text = "" Then
                    lblresult.Text = lblDate_Apply_For.Text & "(Date From) is a required field!"
                    Return False
                End If
                If txtDate_Apply_To.Text = "" Then
                    lblresult.Text = lblDate_Apply_For.Text & "(Date To) is a required field!"
                    Return False
                End If
            End If

            If txtStandby_Employee.Text <> "" Then
                Dim ssqlStandby As String
                Dim mydsStandby As DataSet

                ssqlStandby = "select Employee_Profile_ID from employee_codename_vw where codename = '" & txtStandby_Employee.Text & "'"
                mydsStandby = mySQL.ExecuteSQL(ssql)

                If mydsStandby.Tables.Count > 0 Then
                    If mydsStandby.Tables(0).Rows.Count <= 0 Then
                        lblresult.Text = "Incorrect input for " & lblStandby_Employee.Text & "!"
                        txtStandby_Employee.Focus()
                        Return False
                    End If
                End If
            End If

            strFromDate = mySetting.ConvertDateToDecimal(strFromDate, Session("Company"), Session("Module"))
            strToDate = mySetting.ConvertDateToDecimal(strToDate, Session("Company"), Session("Module"))
            Session("strFromDate") = strFromDate
            Session("strToDate") = strToDate

            If Len(strFromDate) <> 14 Then
                lblresult.Text = "Incorrect date format (Date From)! Date format must be in " & Session("DateFormat")
                Return False
            End If

            If Len(strToDate) <> 14 Then
                lblresult.Text = "Incorrect date format (Date To)! Date format must be in " & Session("DateFormat")
                Return False
            End If

            'If Session("strFromDate") < Session("CurSysDate") Then
            '    lblresult.Text = "(DateFrom) must be equal or greater than today date!"
            '    Return False
            'ElseIf Session("strToDate") < Session("CurSysDate") Then
            '    lblresult.Text = "(DateTo) must be equal or greater than today date!"
            '    Return False
            'Else
            If Session("strToDate") < Session("strFromDate") Then
                lblresult.Text = "(DateTo) must be equal or greater than (DateFrom)!"
                Return False
            End If

            'field 3
            If imgReason.Visible = True Then
                If txtReason.Text = "" Then
                    lblresult.Text = lblReason.Text & " is a required field!"
                    Return False
                End If
                If mySetting.ValidateInput(txtReason, "Character") = False Then
                    txtReason.Text = ""
                    lblresult.Text = "Please re-type your reason!"
                    Return False
                End If
            End If
            If Len(txtReason.Text) > 255 Then
                lblresult.Text = "Maximum length for " & lblReason.Text & " field must be less than 256 characters!"
                Return False
            End If

            'field 4
            If imgOption_Period.Visible = True Then
                If ddlOption_Period.SelectedValue = "" Then
                    lblresult.Text = lblOption_Period.Text & " is a required field!"
                    Return False
                End If
            End If

            'execute sp get cutoffdate

            ssql = "Exec sp_ls_ApplyLeave '" & Session("Company") & "','" & Trim(txtEmployeeCode.Text) & "','','" & strNow & "','','','','0','0','CUTOFFDATE'"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables(0).Rows.Count > 0 Then
                strCutOffDate = myDS.Tables(0).Rows(0).Item(0)
            End If
            myDS = Nothing

            If Len(strFromDate) <> 14 Then
                lblresult.Text = "[SysErr] Incorrect date format!"
                Return False
            End If

            'checking cut off date

            If strCutOffDate > strFromDate Then
                lblresult.Text = "(DateFrom) must be equal or greater than Cut Off Date!"
                Return False
            End If

            If strFromDate > strToDate Then
                lblresult.Text = "(DateTo) must be equal or greater than (DateFrom)!"
                Return False
            End If

            ssql = "Exec sp_ls_ApplyLeave '" & Session("Company") & "','" & Trim(txtEmployeeCode.Text) & "','" & Session("CurLeaveID").ToString & "','" & strFromDate & "','','','','0','0','SOD'"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables.Count > 0 Then
                lblresult.Text = "[" + Session("CurLeaveName").ToString + "] Only Can Apply On SOD date."
                Return False
            End If
            myDS = Nothing


            If ddlOption_Period.SelectedValue = "TIME" Then
                ssql = "Exec sp_ls_ApplyLeave '" & Session("Company") & "','" & Trim(txtEmployeeCode.Text) & "','" & Session("CurLeaveID").ToString & "','','','','','0','0','TIME'"
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

                If myDS.Tables.Count > 0 Then
                    lblresult.Text = strLeaveType + " cannot Apply By Minutes."
                    Return False
                End If
                myDS = Nothing

                If txtTime_Apply_For.Text = "" Then
                    lblresult.Text = lblTime_Apply_For.Text & " (Time From) is a required field!"
                    Return False
                End If
                If txtTime_Apply_To.Text = "" Then
                    lblresult.Text = lblTime_Apply_For.Text & " (Time To) is a required field!"
                    Return False
                End If

                strFromTime = mySetting.UnDisplayTime(strFromTime, Session("Company"), Session("Module"))
                strToTime = mySetting.UnDisplayTime(strToTime, Session("Company"), Session("Module"))

                If Len(strFromTime) <> 6 Then
                    lblresult.Text = "[SysErr] Incorrect [FromTime] time format!"
                    Return False
                End If

                If Len(strToTime) <> 6 Then
                    lblresult.Text = "[SysErr] Incorrect [ToTime] time format!"
                    Return False
                End If

                If strFromTime = strToTime Then
                    lblresult.Text = "(TimeTo) must be greater than (TimeFrom)!"
                    Return False
                End If

            End If
            'Checking duplicate date
            'Dim strPeriod As String = ""

            'If ddlOption_Period.SelectedValue = "FULL" Then
            '    strPeriod = "1.0"
            'ElseIf ddlOption_Period.SelectedValue = "1STHALF" Then
            '    strPeriod = "0.5"
            'ElseIf ddlOption_Period.SelectedValue = "2NDHALF" Then
            '    strPeriod = "0.5"
            'End If

            'ssql = "Exec sp_ls_validateDateSelected '" & Session("Company") & "','" & Session("Module") & "','" & strempid & _
            '       "','" & strPeriod & "','" & ddlOption_Period.SelectedValue & "','" & strFromDate & "','" & strToDate & "'"
            'myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            'If myDS.Tables(0).Rows.Count > 0 Then
            '    If UCase(myDS.Tables(0).Rows(0).Item(0).ToString) = "FALSE" Then
            '        myDS = Nothing
            '        lblresult.Text = "Date selected not available!"
            '        Return False
            '    End If
            'End If
            'myDS = Nothing

            Return True

        Catch ex As Exception
            lblresult.Text = "[ValidateInput]Error: " & ex.Message
        End Try

    End Function

    Private Function CheckAvailableApplyLeave() As Boolean

        'Check Leave Apply Which Available For Those Datejoin Greater Than Setting In Sa_LeaveType
        Try
            If Trim(txtEmployeeCode.Text) = "" Then
                SetlinkPanelToDefault()
                lblresult.Text = "No employee selected!"
                Exit Function
            End If

            ssql1 = "Select Option_Join_Confirm from leave_profile where ocp_id_leave = '" & Session("CurLeaveID").ToString & "'"
            myDS1 = mySQL.ExecuteSQL(ssql1)

            If myDS1.Tables(0).Rows.Count > 0 Then
                If myDS1.Tables(0).Rows(0).Item(0).ToString = "CONFIRM" Then
                    ssql = "Select dbo.fn_DateDiff('Day',confirm_date,dbo.fn_GetCurrentDate(getdate())) as [DayLeftToConfirm] From employee_resign Where company_profile_code='" & Session("Company") & "' and employee_profile_id = dbo.fn_GetEmpID('" & Trim(txtEmployeeCode.Text) & "')"
                Else
                    ssql = "Select dbo.fn_DateDiff('Day',join_date,dbo.fn_GetCurrentDate(getdate())) as [DayLeftToConfirm] From employee_resign Where company_profile_code='" & Session("Company") & "' and employee_profile_id = dbo.fn_GetEmpID('" & Trim(txtEmployeeCode.Text) & "')"
                End If
            End If
            'ssql = "Select dbo.fn_DateDiff('Day',confirm_date,dbo.fn_GetCurrentDate(getdate())) as [DayLeftToConfirm] From employee_resign Where company_profile_code='" & Session("Company") & "' and employee_profile_id = dbo.fn_GetEmpID('" & Trim(txtEmployeeCode.Text) & "')"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables(0).Rows(0).Item(0) > 0 Then
                myDS = Nothing
                Return True
            Else
                lblresult.Text = "Employee who is under probation period cannot apply leave!"
                myDS = Nothing
                Return False
            End If
        Catch ex As Exception
            lblresult.Text = "[CheckAvailableApplyLeave]Error: " & ex.Message
            myDS = Nothing
        End Try

    End Function

    Private Sub InsDateRangeApplication()

        Try
            Dim strFromDate As String
            Dim strToDate As String
            Dim DateApply As String

            If Trim(txtEmployeeCode.Text) = "" Then
                SetlinkPanelToDefault()
                lblresult.Text = "Process Terminate! No employee selected..."
                Exit Sub
            End If

            lstDateApply.Items.Clear()
            strFromDate = Session("strFromDate")
            strToDate = Session("strToDate")
            DateApply = strFromDate

            Do Until DateApply = strToDate
                lstDateApply.Items.Add(DateApply)
                ssql = "Select dbo.fn_DateAdd('Day',1,'" & DateApply & "')"
                myDS1 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                If Trim(myDS1.Tables(0).Rows(0).Item(0).ToString) <> "" Then
                    DateApply = myDS1.Tables(0).Rows(0).Item(0).ToString
                Else
                    lblresult.Text = "Process Terminated! No data return..."
                    myDS1 = Nothing
                    Exit Sub
                End If
                myDS1 = Nothing
            Loop

            lstDateApply.Items.Add(DateApply)
            InsEachDateApplication()

        Catch ex As Exception
            lblresult.Text = "[InsDateRangeApplication]Error: " & ex.Message
            myDS1 = Nothing
        End Try

    End Sub

    Private Sub InsEachDateApplication()

        Try
            stPeriod = ""
            stDateApplyOn = Session("CurSysDate")
            PrevLeaveID = ""
            LeaveID = Session("CurLeaveID")

            If ddlOption_Period.SelectedValue = "FULL" Then
                Day = "1.0"
            ElseIf ddlOption_Period.SelectedValue = "1STHALF" Then
                Day = "0.5"
            ElseIf ddlOption_Period.SelectedValue = "11" Then
                Day = "0.25"
            ElseIf ddlOption_Period.SelectedValue = "12" Then
                Day = "0.25"
            ElseIf ddlOption_Period.SelectedValue = "21" Then
                Day = "0.25"
            ElseIf ddlOption_Period.SelectedValue = "22" Then
                Day = "0.25"
            ElseIf ddlOption_Period.SelectedValue = "2NDHALF" Then
                Day = "0.5"
            ElseIf ddlOption_Period.SelectedValue = "TIME" Then
                Day = GetMin(strFromTime, strToTime)
            End If

            CountDay = lstDateApply.Items.Count
            TotalDay = CountDay * Day

            iID = False

            If chkEachDateApplication() = True Then
                Dim TimeIn, TimeOut, strApproval As String
                Dim TCounter As Integer = 0
                Dim FCounter As Integer = 0
                StopChecking = False
                If ddlOption_Period.SelectedValue = "TIME" Then
                    TimeIn = strFromTime
                    TimeOut = strToTime
                Else
                    TimeIn = "000000"
                    TimeOut = "000000"
                End If
                If Session("SecurityRole").ToString = "ADM" Then
                    strApproval = "NO"
                Else
                    strApproval = "YES"
                End If

                vCounter = 0

                Do Until vCounter = lstDateApply.Items.Count
                    DateApplyFor = lstDateApply.Items(vCounter).ToString
                    If chkLeaveSequence() = True Then
                        If ddlOption_Period.SelectedValue = "FULL" And mstBalance = "0.5" Then
                            Day = "0.5"
                            ssql = "Exec sp_ls_insLeaveApplication '" & Session("Company") & "','" _
                                  & Session("curLeaveEmpID") & "','" & LeaveID & "','" _
                                  & stDateApplyOn & "','" & DateApplyFor & "','" & Day & "','" _
                                  & "1STHALF" & "','" & Session("EmpID") & "','" & Trim(txtReason.Text) & "','','" & Action & "','" _
                                  & TimeIn & "','" & TimeOut & "','" & strApproval & "','" & txtStandby_Employee.Text & "'"
                            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                            If myDS.Tables.Count > 0 Then
                                If UCase(myDS.Tables(0).Rows(0).Item(0).ToString) = "SUCCESS" Then
                                    TCounter = TCounter + 1
                                Else
                                    FCounter = FCounter + 1
                                End If
                            End If
                            myDS = Nothing

                            TotalDay = TotalDay - Day

                            If chkLeaveSequence() = True Then
                                ssql = "Exec sp_ls_insLeaveApplication '" & Session("Company") & "','" _
                                & Session("curLeaveEmpID") & "','" & LeaveID & "','" _
                                & Session("CurSysDateTime") & "','" & DateApplyFor & "','" & Day & "','" _
                                & "2NDHALF" & "','" & Session("EmpID") & "','" & Trim(txtReason.Text) & "','','" & Action & "','" _
                                & TimeIn & "','" & TimeOut & "','" & strApproval & "','" & txtStandby_Employee.Text & "'"
                                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                                myDS = Nothing
                            End If

                            TotalDay = TotalDay - Day

                            If ddlOption_Period.SelectedValue = "1STHALF" Or ddlOption_Period.SelectedValue = "2NDHALF" Then
                                Day = 0.5
                            ElseIf ddlOption_Period.SelectedValue = "11" Or ddlOption_Period.SelectedValue = "12" Or ddlOption_Period.SelectedValue = "21" Or ddlOption_Period.SelectedValue = "22" Then
                                Day = 0.25
                            Else
                                Day = 1.0
                            End If
                        Else
                            ssql = "Exec sp_ls_insLeaveApplication '" & Session("Company") & "','" _
                                   & Session("curLeaveEmpID") & "','" & LeaveID & "','" _
                                   & Session("CurSysDateTime") & "','" & DateApplyFor & "','" & Day & "','" _
                                   & ddlOption_Period.SelectedValue & "','" & Session("EmpID") & "','" & Trim(txtReason.Text) & "','','" & Action & "','" _
                                   & TimeIn & "','" & TimeOut & "','" & strApproval & "','" & txtStandby_Employee.Text & "'"
                            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                            If myDS.Tables.Count > 0 Then
                                If UCase(myDS.Tables(0).Rows(0).Item(0).ToString) = "SUCCESS" Then
                                    TCounter = TCounter + 1
                                Else
                                    FCounter = FCounter + 1
                                End If
                            End If
                            myDS = Nothing

                            TotalDay = TotalDay - Day
                        End If

                    End If
                    vCounter = vCounter + 1
                    If ApplicationEnd = True Then
                        Initialize()
                        Exit Sub
                    End If
                Loop
                reason = txtReason.Text
                'Dim clsRpt As New clsReportServices, myTargetURL As String = ""
                'myTargetURL = clsRpt.GetPDFReportURL()
                'myTargetURL = myTargetURL & "&Company_Profile_Code=" & Session("Company").ToString & "&Employee_Profile_ID=" & Session("curLeaveEmpID").ToString & "&LeaveCode=" & Session("CurLeaveID") & "&fromDate=" & Session("strFromDate") & "&Todate=" & Session("strToDate") & "&reason=" & reason & "&rdTemplate=Leave_ApplicationForm"
                Initialize()

                If TCounter + FCounter = TCounter Then
                    LnkBtnViewRecord_Action() 'refresh GV to get the latest leave balance
                    lblresult.Text = TCounter & " leave record(s) apply successfully!" + lblresult1.Text
                    'Response.Write("<script>window.open('" & myTargetURL & "');</script>")
                    'imgBtnPrintReport_Click(nothing, Nothing)
                ElseIf TCounter + FCounter = FCounter Then
                    lblresult.Text = "No leave apply successfully!" + lblresult1.Text
                Else
                    LnkBtnViewRecord_Action() 'refresh GV to get the latest leave balance
                    lblresult.Text = TCounter & " leave record(s) apply successfully while " & FCounter & " leave record(s) has been cancelled!" + lblresult1.Text
                End If

            End If
        Catch ex As Exception
            lblresult.Text = "[InsEachDateApplication]Error: " & ex.Message
            myDS = Nothing
        End Try

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
            lblresult1.Text = ""

            chkEachDateApplication = False
            vCounter = 0
            strempid = Session("curLeaveEmpID")
            CurrentDate = Session("CurSysDate")

            While vCounter < lstDateApply.Items.Count

                Action = "ADD"
                DateApplyFor = Trim(lstDateApply.Items(vCounter).ToString)

                If ddlOption_Period.SelectedValue = "TIME" Then
                    ssql = "Exec sp_ls_chkLeaveApplication '" & Session("Company") & "','" _
                           & strempid & "','" & Session("CurLeaveID") & "','" & CurrentDate & "','" _
                           & DateApplyFor & "','" & Day & "','" _
                           & ddlOption_Period.SelectedValue & "','" & TotalDay & "',0,'','" _
                           & strFromTime & "','" & strToTime & "'"
                Else
                    ssql = "Exec sp_ls_chkLeaveApplication '" & Session("Company") & "','" _
                           & strempid & "','" & Session("CurLeaveID") & "','" & CurrentDate & "','" _
                           & DateApplyFor & "','" & Day & "','" _
                           & ddlOption_Period.SelectedValue & "','" & TotalDay & "',0,''"
                End If
                myDS1 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

                If myDS1.Tables.Count > 0 Then
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
                        lstDateApply.Items.RemoveAt(vCounter)
                        ssql1 = "Select dbo.fn_DisplayDate('" & DateApplyFor & "','" & Session("Company") & "','" & Session("Module") & "')"
                        myDS3 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
                        vDate2 = myDS3.Tables(0).Rows(0).Item(0).ToString() & " " & vDate2
                        myDS3 = Nothing
                        vCounter = vCounter - 1
                    ElseIf Trim(myDS1.Tables(0).Rows(0).Item(0).ToString) = "6" Then
                        lblresult.Text = "Date Apply Must Less Than Or Equal to Current Date"
                        MsgBox(lblresult.Text)
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
                        MsgBox(lblresult.Text)
                        Return False

                    ElseIf Trim(myDS1.Tables(0).Rows(0).Item(0).ToString) = "8" Then
                        lblresult.Text = "Days apply exceed maximum limit that allow in one time!"
                        MsgBox(lblresult.Text)
                        Return False

                    ElseIf Trim(myDS1.Tables(0).Rows(0).Item(0).ToString) = "9" Then
                        lblresult.Text = "Balance Is Not Available!"
                        MsgBox(lblresult.Text)
                        Return False
                    ElseIf Trim(myDS1.Tables(0).Rows(0).Item(0).ToString) = "10" Then
                        ssql1 = "Select decimal_value1 from Parameter where Company_Profile_Code = '" & Session("Company").ToString & "' and Code = 'APPLYBEFORE'and Module_Profile_Code = 'LEAVE'"
                        myDS3 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
                        If CDbl(myDS3.Tables(0).Rows(0).Item(0).ToString) > 0 Then
                            lblresult.Text = "This Leave (" & Session("CurLeaveName") & ") must be apply after (" & myDS3.Tables(0).Rows(0).Item(0).ToString & ") days!"
                        Else
                            lblresult.Text = "This Leave (" & Session("CurLeaveName") & ") must be apply before (" & Math.Abs(Math.Round(CDbl(myDS3.Tables(0).Rows(0).Item(0).ToString), 2)) & ") days!"
                        End If
                        'lblresult.Text = "Balance Is Not Available!"
                        MsgBox(lblresult.Text)
                        Return False
                    ElseIf Trim(myDS1.Tables(0).Rows(0).Item(0).ToString) = "11" Then
                        lblresult.Text = "This Leave (" & Session("CurLeaveName") & ") Only Can Apply On SOD Date!"
                        Return False
                    End If
                Else
                    ssql = "Exec sp_sa_chkTransactionLockDate '" & Session("Company") & "','" _
                           & Session("EmpID").ToString & "','" & DateApplyFor & "'," & 1 & "," & 0 & ",'" & Session("PayCycle").ToString & "'"
                myDS2 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                If Trim(myDS2.Tables(0).Rows(0).Item(0).ToString) <> "" Then
                    vDate3 = DateApplyFor & " " & vDate3
                End If
                myDS2 = Nothing
                End If
                myDS1 = Nothing

                vCounter = vCounter + 1
            End While

            If vDate1 <> "" Then   ' Duplicate Record
                Err1 = "The following date was duplicate! (" & vDate1 & ")"
            End If

            If vDate2 <> "" Then   ' Not Allow to apply Leave
                Err2 = "Following day(s)(RD/PH/OD) is not allow to apply leave! (" & vDate2 & ") "
                lblresult1.Text = Err2
                MsgBox(lblresult1.Text)
                Err2 = ""
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
                MsgBox(lblresult.Text)
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
        Dim CurrentLeaveName As String


        If iID = True Then
            LeaveID = Session("CurLeaveID")
        End If

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
                    & Session("curLeaveEmpID") & "','" & LeaveID & "','" _
                    & CurrentDate & "','" & DateApplyFor & "','" _
                    & Day & "','" & ddlOption_Period.SelectedValue & "'," & TotalDay & ",'" _
                    & "0" & "','" & StopChecking & "'," & vCount & ",''"

                    myDS1 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                    If Trim(myDS1.Tables(0).Rows.Count) > 0 Then
                        LeaveID = myDS1.Tables(0).Rows(0).Item(1).ToString
                        CurrentLeaveID = LeaveID
                        mstBalance = myDS1.Tables(0).Rows(0).Item(2).ToString

                        myDS10 = mySetting.GetCodeNamebyOCPID(Session("Company").ToString, CurrentLeaveID)

                        CurrentLeaveName = ""
                        If myDS10.Tables.Count > 0 Then
                            CurrentLeaveName = myDS10.Tables(0).Rows(0).Item(0).ToString
                        End If
                        myDS10 = Nothing

                        If myDS1.Tables(0).Rows(0).Item(0).ToString = "Insert" And CurrentLeaveID <> PrevLeaveID And _
                            Session("CurLeaveID") <> CurrentLeaveID And LeaveIDSequence < vCount Then
                            myDS10 = mySetting.GetCodeNamebyOCPID(Session("Company").ToString, CurrentLeaveID)

                            CurrentLeaveName = ""
                            If myDS10.Tables.Count > 0 Then
                                CurrentLeaveName = myDS10.Tables(0).Rows(0).Item(0).ToString
                            End If
                            myDS10 = Nothing

                            lblresult.Text = "The leave you apply is not available! Will Apply " & CurrentLeaveName & "!"
                            MsgBox(lblresult.Text)
                            'StopChecking = True
                            'Action = "CANCEL"
                            'myDS1 = Nothing
                            'Exit Function
                            'StopChecking = True
                            PrevLeaveID = CurrentLeaveID
                            Action = "ADD"
                            If vCount < CountSequence Then
                                chkLeaveSequence = True
                                ContinueLoop = True
                                ContinueCount = vCount
                                myDS1 = Nothing
                                Exit Function
                            End If

                        ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "Insert" And CurrentLeaveID <> PrevLeaveID Then
                            iID = True
                            chkLeaveSequence = True
                            myDS1 = Nothing
                            Exit Function

                        ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "1" Then
                            lblresult.Text = "Balance Is Not Available!"
                            chkLeaveSequence = True
                            StopChecking = True
                            Action = "CANCEL"
                            myDS1 = Nothing
                            MsgBox(lblresult.Text)
                            Exit Function

                        ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "2" And CurrentLeaveID <> PrevLeaveID Then
                            
                            lblresult.Text = "The leave you apply is not available. Will Apply " & CurrentLeaveName & "!"
                            MsgBox(lblresult.Text)
                            'StopChecking = True
                            'Action = "CANCEL"
                            'myDS1 = Nothing
                            'Exit Function
                            'If ShowSysMessage("248", Trim(Left(colText(2), 50)), "", "2") = 7 Then
                            '    'RetrieveStatusBarText(SBR_Saved_Cancel)
                            '    lblresult.Text = "The leave you apply is not available!"
                            '    StopChecking = True
                            '    Action = "CANCEL"
                            '    myDS1 = Nothing
                            '    Exit Function
                            'Else
                            '    'StopChecking = True
                            PrevLeaveID = CurrentLeaveID
                            Action = "ADD"
                            If vCount < CountSequence Then
                                chkLeaveSequence = True
                                ContinueLoop = True
                                ContinueCount = vCount
                                myDS1 = Nothing
                                Exit Function
                            End If
                            'End If

                        ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "2" And CurrentLeaveID = PrevLeaveID _
                                And myDS1.Tables(0).Rows(0).Item(2).ToString = "False" And vCount = CountSequence - 1 Then
                            lblresult.Text = "Leave Balance Is Not Available!"
                            chkLeaveSequence = True
                            StopChecking = True
                            Action = "CANCEL"
                            myDS1 = Nothing
                            MsgBox(lblresult.Text)
                            Exit Function

                        ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "2" And CurrentLeaveID = PrevLeaveID _
                                And myDS1.Tables(0).Rows(0).Item(2).ToString = "True" Then
                            chkLeaveSequence = True
                            Action = "ADD"
                            myDS1 = Nothing
                            Exit Function

                        ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "3" Then
                            'txtDate_Apply_From.Text = DateApplyFor
                            'lblresult.Text = "Leave Balance Is Not Available!"
                            'chkLeaveSequence = True
                            'Action = "CANCEL"
                            'myDS1 = Nothing
                            'Exit Function
                            'If ShowSysMessage("247", mskFromDate, "", "2") = 7 Then
                            '    'RetrieveStatusBarText(SBR_Saved_Cancel)
                            '    Action = "CANCEL"
                            'Else
                            chkLeaveSequence = True
                            StopChecking = True
                            Action = "ADD"
                            '    myDS1 = Nothing
                            '    Exit Function
                            'End If

                        ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "4" Then
                            txtDate_Apply_From.Text = DateApplyFor
                            lblresult.Text = "Leave Balance Is Not Available!"
                            chkLeaveSequence = True
                            Action = "CANCEL"
                            ApplicationEnd = True
                            myDS1 = Nothing
                            MsgBox(lblresult.Text)
                            Exit Function
                            'If ShowSysMessage("247", mskFromDate, "", "2") = 7 Then
                            '    'RetrieveStatusBarText(SBR_Saved_Cancel)
                            '    Action = "CANCEL"
                            '    ApplicationEnd = True
                            '    myDS1 = Nothing
                            '    Exit Function
                            'Else
                            '    chkLeaveSequence = True
                            '    StopChecking = True
                            '    Action = "ADD"
                            '    myDS1 = Nothing
                            '    Exit Function
                            'End If

                        ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "0" Or (myDS1.Tables(0).Rows(0).Item(0).ToString = "2" And CurrentLeaveID = PrevLeaveID _
                                And myDS1.Tables(0).Rows(0).Item(0).ToString = "False") Then
                            LeaveID = Session("CurLeaveID")
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
                & Session("curLeaveEmpID") & "','" & LeaveID & "','" _
                & CurrentDate & "','" & DateApplyFor & "','" & Day & "','" _
                & "0" & "','" & StopChecking & "','',''"

                myDS1 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                If Trim(myDS1.Tables.Count) > 0 Then
                    'move inside
                    LeaveID = myDS1.Tables(0).Rows(0).Item(1).ToString
                    If myDS1.Tables(0).Rows(0).Item(0).ToString = "1" Or myDS1.Tables(0).Rows(0).Item(0).ToString = "3" Then
                        lblresult.Text = "Leave Balance Is Not Available!"
                        Action = "CANCEL"
                        StopChecking = True
                        myDS1 = Nothing
                        MsgBox(lblresult.Text)
                        Exit Function
                    ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "2" Then
                        'txtDate_Apply_From.Text = DateApplyFor
                        'lblresult.Text = "Leave Balance Is Not Available!"
                        'StopChecking = False
                        'Action = "CANCEL"
                        'myDS1 = Nothing
                        'Exit Function
                        'If ShowSysMessage("247", mskFromDate, "", "2") = 7 Then
                        '    'RetrieveStatusBarText(SBR_Saved_Cancel)
                        '    StopChecking = False
                        '    Action = "CANCEL"
                        '    Exit Function
                        'Else
                        chkLeaveSequence = True
                        StopChecking = True
                        Action = "ADD"
                        'End If

                    ElseIf myDS1.Tables(0).Rows(0).Item(0).ToString = "0" Then
                        'RetrieveStatusBarText(SBR_Saved_Cancel)
                        Action = "CANCEL"
                    End If
                End If

            Catch ex As Exception
                lblresult.Text = "[chkLeaveSequence:sp3]Error: " & ex.Message
            End Try

        End If
        myDS1 = Nothing
        chkLeaveSequence = True

    End Function

    Private Function GetMin(ByVal FromTime As String, ByVal ToTime As String) As Decimal

        If FromTime > ToTime Then
            ToTime = "19000102" + ToTime
        Else
            ToTime = "19000101" + ToTime
        End If
        FromTime = "19000101" + FromTime
        
        ssql = "Exec sp_ls_GetDayLeaveApplyMinute '" & Session("Company") & "','" & Session("Module") & "','" & FromTime & "','" & Totime & "'"
        myDS = mySQL.ExecuteSQL(ssql)

        If myDS.Tables(0).Rows.Count > 0 Then
            GetMin = myDS.Tables(0).Rows(0).Item(0).ToString
        Else
            GetMin = 0.0
        End If
    End Function
    Private Sub MsgBox(ByVal msg As String)
        Dim strScript As String = "<script language=" & """" & "javascript" & """" & ">"
        strScript &= "alert('" & msg & "');"
        strScript &= "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "Error!", strScript)
    End Sub
#End Region

End Class
