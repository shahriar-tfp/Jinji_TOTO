Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.web.Configuration

Partial Class Pages_Leave_Process_Carry_Forward
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
    Dim logic, check As Boolean
    Dim grdItem As GridViewRow
    Dim chkSelected, chkSelected1, chkSelected2 As CheckBox
    Dim dtSelectedDate As DateTime
    Dim stPeriod, stDateApplyOn, PrevLeaveID, LeaveID, iID, StopChecking, mstBalance As String
    Dim ApplicationEnd, ContinueLoop As Boolean
    Dim Day, TotalDay As Decimal
    Dim Err1, Err2, Err3, Err4, Err5, Err6, vDate1, vDate2, vDate3, vDate4, vDate5, vDate6, Message As String
    Dim vCount, vCounter, CountDay, CountSequence, ContinueCount As Integer
    Dim Action, DateApplyOn, DateApplyFor, EmpID, EmpCode, Leave, Period, CurrentDate, TempDate, DateProcessFor, ExpiryDate As String
    Dim Counter, CurrentYear, SysYear As Integer
    Dim strPath As String = "../../Images"
    Dim btnColourDef, btnColourAlt As String
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

            txtDate.MaxLength = 10
            txtExpiry_Date.MaxLength = 10

            'bind into ddlOption_Type
            myDS = mySQL.ExecuteSQL("Create Table #Result([Code] nvarchar(50), [Name] nvarchar(255)); Insert Into #Result Select '',''; Insert Into #Result Select ID 'Code',Name 'Name' From Organisation_Code_Profile_Vw where Option_Type='LEAVE' And Company_Profile_Code='" & Session("Company") & "' And ID In (Select OCP_ID_Leave From Leave_Profile Where Option_Carry_Forward = 'MANUAL') Order By Name; Select * From #Result; Drop Table #Result")
            If myDS.Tables.Count > 0 Then
                If myDS.Tables(0).Columns.Count > 1 Then
                    ddlOption_Leave_Type.DataTextField = "Name"
                    ddlOption_Leave_Type.DataValueField = "Code"
                    ddlOption_Leave_Type.DataSource = myDS
                    ddlOption_Leave_Type.DataBind()
                End If
            End If

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

            lblOption_Leave_Type.Text = "Leave Type"
            lblOption_Leave_Type.CssClass = "wordstyle10"
            lblDate.Text = "Year To Carry Forward"
            lblDate.CssClass = "wordstyle10"
            lblExpiry_Date.Text = "Expiry_Date"
            lblExpiry_Date.CssClass = "wordstyle10"

            'get image 
            mySetting.GetImgUrl(imgCompany_Profile_ID, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
            mySetting.GetImgUrl(imgOption_Leave_Type, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
            mySetting.GetImgUrl(imgDate, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
            mySetting.GetImgUrl(imgExpiry_Date, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)

            'get image button
            mySetting.GetImgBtnUrl(imgBtnCompany_Profile_ID, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
            mySetting.GetImgBtnUrl(imgBtnExpiry_Date, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)

            mySetting.GetBtnImgUrl(imgBtnUpdate, Session("Company").ToString, btnColourDef, "btnProcess.png")
            mySetting.GetBtnImgUrl(imgBtnClear, Session("Company").ToString, btnColourDef, "btnClear.png")

            'get calendar setting
            mySetting.PopUpCalendar_ImageButton(imgBtnExpiry_Date, Form.ID, "txtExpiry_Date")

            body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

        Catch ex As Exception
            lblresult.Text = "[PagePreload]Error : " & ex.Message
        End Try

    End Sub

#End Region

#Region "Sub & Function"

    Protected Sub imgBtnClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnClear.Click
        ddlOption_Leave_Type.SelectedIndex = 0
        lblDate.Text = "Year To Carry Forward"
        txtDate.Text = ""
        txtExpiry_Date.Text = ""
        lblresult.Text = ""
    End Sub

    Protected Sub imgBtnUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnUpdate.Click

        lblresult.Text = ""

        'checking for require field
        If imgCompany_Profile_ID.Visible = True Then
            If txtCompany_Profile_ID.Text = "" Then
                lblresult.Text = lblCompany_Profile_ID.Text & " field is required..."
                txtCompany_Profile_ID.Focus()
                Exit Sub
            End If
        End If

        If imgOption_Leave_Type.Visible = True Then
            If ddlOption_Leave_Type.SelectedIndex = 0 Then
                lblresult.Text = lblOption_Leave_Type.Text & " field is required..."
                ddlOption_Leave_Type.Focus()
                Exit Sub
            End If
        End If

        If imgDate.Visible = True Then
            If txtDate.Text = "" Then
                lblresult.Text = lblDate.Text & " field is required..."
                txtDate.Focus()
                Exit Sub
            End If
        End If

        If imgExpiry_Date.Visible = True Then
            If txtExpiry_Date.Text = "" Then
                lblresult.Text = lblExpiry_Date.Text & " field is required..."
                txtExpiry_Date.Focus()
                Exit Sub
            End If
        End If

        'date field checking
        If Session("strService") = "YEARLY" Then
            If txtDate.Text.Length <> 4 Then
                lblresult.Text = "Incorrect format for " & lblDate.Text & " field!"
                txtDate.Focus()
                Exit Sub
            End If
        ElseIf Session("strService") = "MONTHLY" Then
            If txtDate.Text.Length = 1 Then
                txtDate.Text = "0" & txtDate.Text
            End If
        ElseIf Session("strService") = "DAILY" Then
            If txtDate.Text.Length <> 10 Then
                lblresult.Text = "Incorrect format for " & lblDate.Text & " field!"
                txtDate.Focus()
                Exit Sub
            End If
        End If

        'checking for daily only
        'Dim DateDiffV As String = ""
        'Dim CurrentDT As String = ""
        'Dim TempDateV As String = ""
        'If Session("strService") = "DAILY" Then
        '    'Date Format Validation
        '    TempDateV = txtDate.Text & "/" & CStr(Now.Year)
        '    TempDateV = mySetting.ConvertDateToDecimal(TempDateV, Session("Company"), Session("Module"))
        '    If TempDateV.Length <> 14 Then
        '        lblresult.Text = "Incorrect format for " & lblDate.Text & " field!"
        '        txtDate.Focus()
        '        Exit Sub
        '    End If

        '    ssql = "Select dbo.fn_datediff('Day','" & CStr(Now.Year) & Right(txtDate.Text, 2) & Left(txtDate.Text, 2) & "000000" & "',dbo.fn_GetCurrentDate(getDate()))"
        '    myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        '    If myDS.Tables(0).Rows.Count > 0 Then
        '        DateDiffV = myDS.Tables(0).Rows(0).Item(0).ToString
        '    End If
        'End If
        'myDS = Nothing

        If Session("strService") = "YEARLY" Then
            DateProcessFor = "01/01/" & CStr(CInt(txtDate.Text) + 2)
        ElseIf Session("strService") = "MONTHLY" And Val(txtDate.Text) >= Now.Month Then
            DateProcessFor = "01/" & txtDate.Text & "/" & CStr(Now.Year)
        ElseIf Session("strService") = "MONTHLY" And Val(txtDate.Text) < Now.Month Then
            DateProcessFor = "01/" & txtDate.Text & "/" & CStr(Now.Year + 1)
        ElseIf Session("strService") = "DAILY" Then
            DateProcessFor = txtDate.Text
            'ElseIf Session("strService") = "DAILY" And CInt(DateDiffV) < 0 Then
            '    DateProcessFor = Left(txtDate.Text, 2) & "/" & Right(txtDate.Text, 2) & "/" & CStr(Now.Year)
            'ElseIf Session("strService") = "DAILY" And CInt(DateDiffV) >= 0 Then
            '    DateProcessFor = Left(txtDate.Text, 2) & "/" & Right(txtDate.Text, 2) & "/" & CStr(Now.Year + 1)
        End If

        'Date Format Validation
        DateProcessFor = mySetting.ConvertDateToDecimal(DateProcessFor, Session("Company"), Session("Module"))
        If DateProcessFor.Length <> 14 Then
            lblresult.Text = "Incorrect format for " & lblDate.Text & " field!"
            txtDate.Focus()
            Exit Sub
        End If
        ExpiryDate = mySetting.ConvertDateToDecimal(txtExpiry_Date.Text, Session("Company"), Session("Module"))
        If ExpiryDate.Length <> 14 Then
            lblresult.Text = "Incorrect format for " & lblExpiry_Date.Text & " field!"
            txtExpiry_Date.Focus()
            Exit Sub
        End If

        'Date Comparasion Checking
        ssql = "Select dbo.fn_datediff('Day','" & DateProcessFor & "','" & ExpiryDate & "')"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            If myDS.Tables(0).Rows(0).Item(0).ToString <= 0 Then
                lblresult.Text = "Invalid Date Format!"
                txtExpiry_Date.Focus()
                Exit Sub
            End If
        End If
        myDS = Nothing

        If Session("strService") = "DAILY" Then
            ssql = "Exec sp_ls_CalCarryForward1 '" & Session("Company") & "','" _
            & ddlOption_Leave_Type.SelectedValue & "','" & DateProcessFor & "','" _
            & ExpiryDate & "',1,'" & Session("EmpID") & "'"
        Else
            ssql = "Exec sp_ls_CalCarryForward1 '" & Session("Company") & "','" _
            & ddlOption_Leave_Type.SelectedValue & "','" & txtDate.Text & "','" _
            & ExpiryDate & "',1,'" & Session("EmpID") & "'"
        End If

        mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)


    End Sub

    Protected Sub ddlOption_Leave_Type_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOption_Leave_Type.SelectedIndexChanged

        If ddlOption_Leave_Type.SelectedIndex <> 0 Then
            ssql = "Select Option_Year_Service From Leave_Profile Where OCP_ID_Leave = '" & ddlOption_Leave_Type.SelectedValue & "'"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables(0).Rows.Count > 0 Then
                Session("strService") = myDS.Tables(0).Rows(0).Item(0).ToString
                txtDate.Text = ""
                If UCase(Session("strService")) = "DAILY" Then
                    lblDate.Text = "Year To Carry Forward (dd/mm/yyyy)"
                    txtDate.MaxLength = 10
                ElseIf UCase(Session("strService")) = "MONTHLY" Then
                    lblDate.Text = "Year To Carry Forward (mm)"
                    txtDate.MaxLength = 2
                ElseIf UCase(Session("strService")) = "YEARLY" Then
                    lblDate.Text = "Year To Carry Forward (yyyy)"
                    txtDate.MaxLength = 4
                End If
            End If
            myDS = Nothing
        Else
            lblDate.Text = "Year To Carry Forward"
            txtDate.Text = ""
        End If

    End Sub

#End Region


End Class
