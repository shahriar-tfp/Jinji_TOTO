Imports System
Imports System.Data
Imports System.Data.SqlClient
Partial Class Pages_Payroll_Employee_Bonus_Payslip
    Inherits System.Web.UI.Page
#Region "Public Declaration"
    Private WithEvents mySQL As New clsSQL, myDS As New DataSet, myDS2 As New DataSet, mySetting As New clsGlobalSetting
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Public ssql As String, i As Integer, j As Integer, SearchByPage As Boolean, RecFound As Boolean, CountRecord As Integer
    Public AllowView, AllowInsert, AllowUpdate, AllowDelete, AllowPrint As Boolean
    Public _currentPageNumber As Integer, _totalPage As Double = 1, _totalRecords As Integer
    Public myDE As New DictionaryEntry, myHT As Hashtable
    Public myDataType As New clsGlobalSetting.DataType, SearchByFilter As Boolean = False
    Dim intColumn As Integer
#End Region
#Region "Private Declaration"
    Private WithEvents myDT1 As New DataTable, myDT2 As New DataTable
    Dim myDR1 As DataRow, myDR2 As DataRow, rcPerPage As Integer, autonum As Integer
    Dim ssql2 As String, ssql3 As String, ssql4 As String
    Dim rowPosition As String = "R_", rowPositionM As String = "RM_", panelPosition As String = "Display_"
    Dim buttonPosition1 As String = "B1_", buttonPosition2 As String = "B2_", buttonPosition3 As String = "B3_", labelPosition1 As String = "L1_"
    Dim btnColourDef, btnColourAlt As String
    Dim strPath As String = "../../Images"
    Dim logic As Boolean

#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.IsNewSession Then
            Response.Redirect("../Global/SessionTimeOut.aspx")
        End If
        If Not IsPostBack Then
            Response.CacheControl = "no-cache"
            Response.AddHeader("Pragma", "no-cache")
            Response.Expires = -1
            Session("Module") = "PAYROLL"
            PagePreload()
            EmployeeChecking()
        Else
            lblResult.Visible = False
        End If
    End Sub

    Private Sub PagePreload()

        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")
        Session("Module") = "PAYROLL"
        body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

        mySetting.GetBtnImgUrl(imgbtnSearch, Session("Company").ToString, btnColourDef, "btnSearch.png")
        lnkbtnShowHideEmp.Text = "Show Employee Details"
        pnlEmpInfo.Visible = False
        'get Page Title
        ssql = "exec sp_sa_GetPageTitle '" & Form.ID & "'"
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            lblTitle.Text = myDS.Tables(0).Rows(0).Item(0)
            lblTitle.CssClass = "wordstyle4"
            'lblTitle.Style.Add("top", "5px")
            'lblTitle.Style.Add("left", "15px")
            'lblTitle.Style.Add("position", "absolute")
        End If
        myDS = Nothing

        'Bind Data ddlYear
        ssql = "Exec sp_tms_GenerateYear 5,1"
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql)
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                ddlYEAR.DataValueField = "year"
                ddlYEAR.DataTextField = "year"
                ddlYEAR.DataSource = myDS.Tables(0).DefaultView
                ddlYEAR.DataBind()
            End If
        End If
        myDS = Nothing

        mySetting.ArrangeDDLSelectedIndex(ddlYEAR, clsGlobalSetting.DDLSelection.SelectedValue, DatePart(DateInterval.Year, Now()))
        Session("SelectedYear") = ddlYEAR.SelectedValue

        'Bind Data ddlMonth
        ssql = "Exec sp_tms_GenerateMonth 1,12"
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql)
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                ddlMONTH.DataValueField = "month"
                ddlMONTH.DataTextField = "desc"
                ddlMONTH.DataSource = myDS.Tables(0).DefaultView
                ddlMONTH.DataBind()
            End If
        End If
        myDS = Nothing
        mySetting.ArrangeDDLSelectedIndex(ddlMONTH, clsGlobalSetting.DDLSelection.SelectedValue, DatePart(DateInterval.Month, Now()))
        Session("SelectedMonth") = ddlMONTH.SelectedValue

        mySetting.GetDropdownlistValue(Form.ID, "OPTION_PAY_CYCLE", ddlOPTION_PAY_CYCLE)
        If ddlOPTION_PAY_CYCLE.Items.Count > 1 Then
            ddlOPTION_PAY_CYCLE.SelectedIndex = 1
        End If

        mySetting.GetImgTypeUrl(imgtop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Filter.png")
        mySetting.GetImgTypeUrl(imgbottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")

        mySetting.GetImgBtnUrl(imgKeyEMPLOYEE_PROFILE_ID, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyYEAR, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyMONTH, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyOPTION_PAY_CYCLE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)

        mySetting.GetImgBtnUrl(imgbtnEMPLOYEE_PROFILE_ID, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnYEAR, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnMONTH, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnOPTION_PAY_CYCLE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)

        imgKeyYEAR.Visible = False
        imgKeyMONTH.Visible = False
        imgKeyOPTION_PAY_CYCLE.Visible = False

        imgbtnYEAR.Visible = False
        imgbtnMONTH.Visible = False
        imgbtnOPTION_PAY_CYCLE.Visible = False

        imgKeyEMPLOYEE_PROFILE_ID.CssClass = "R_1"
        lblEMPLOYEE_PROFILE_ID.CssClass = "R_2"
        txtEMPLOYEE_PROFILE_ID.CssClass = "R_3"
        imgbtnEMPLOYEE_PROFILE_ID.CssClass = "R_4"

        lblEMPLOYEE_PROFILE_ID.Style.Add("font-family", "Arial Unicode MS")

        lblYEAR.Style.Add("top", "123px")
        lblYEAR.Style.Add("left", "48px")
        lblYEAR.Style.Add("font-family", "Arial Unicode MS")
        lblYEAR.Style.Add("font-weight", "bold")
        lblYEAR.Style.Add("font-size", "11px")
        lblYEAR.Style.Add("position", "absolute")

        ddlYEAR.Style.Add("top", "123px")
        ddlYEAR.Style.Add("left", "108px")
        ddlYEAR.Style.Add("position", "absolute")

        lblMONTH.Style.Add("top", "123px")
        lblMONTH.Style.Add("left", "268px")
        lblMONTH.Style.Add("font-family", "Arial Unicode MS")
        lblMONTH.Style.Add("font-weight", "bold")
        lblMONTH.Style.Add("font-size", "11px")
        lblMONTH.Style.Add("position", "absolute")

        ddlMONTH.Style.Add("top", "123px")
        ddlMONTH.Style.Add("left", "328px")
        ddlMONTH.Style.Add("position", "absolute")

        lblOPTION_PAY_CYCLE.Style.Add("top", "123px")
        lblOPTION_PAY_CYCLE.Style.Add("left", "488px")
        lblOPTION_PAY_CYCLE.Style.Add("font-family", "Arial Unicode MS")
        lblOPTION_PAY_CYCLE.Style.Add("font-weight", "bold")
        lblOPTION_PAY_CYCLE.Style.Add("font-size", "11px")
        lblOPTION_PAY_CYCLE.Style.Add("position", "absolute")

        ddlOPTION_PAY_CYCLE.Style.Add("top", "123px")
        ddlOPTION_PAY_CYCLE.Style.Add("left", "568px")
        ddlOPTION_PAY_CYCLE.Style.Add("position", "absolute")

        'imgKeyYEAR.CssClass = "R_9"
        'lblYEAR.CssClass = "R_10"
        'ddlYEAR.CssClass = "R_11"
        'imgbtnYEAR.CssClass = "R_12"

        'imgKeyMONTH.CssClass = "R_13"
        'lblMONTH.CssClass = "R_14"
        'ddlMONTH.CssClass = "R_15"
        'imgbtnMONTH.CssClass = "R_16"

        'imgKeyOPTION_PAY_CYCLE.CssClass = "R_17"
        'lblOPTION_PAY_CYCLE.CssClass = "R_18"
        'ddlOPTION_PAY_CYCLE.CssClass = "R_19"
        'imgbtnOPTION_PAY_CYCLE.CssClass = "R_20"

        pnlSeparate.Style.Add("top", "151px")
        pnlSeparate.Style.Add("left", "10px")
        pnlSeparate.Style.Add("position", "absolute")

        'pnlEmpInfo.Style.Add("top", "136px")
        'pnlEmpInfo.Style.Add("left", "20px")
        'pnlEmpInfo.Style.Add("position", "absolute")

        'imgbottom.Style.Add("top", "355px")
        'imgbottom.Style.Add("left", "20px")
        'imgbottom.Style.Add("position", "absolute")

        'pnlGridView.Style.Add("top", "400px")
        'pnlGridView.Style.Add("left", "20px")
        'pnlGridView.Style.Add("position", "absolute")

        'lblResult.Style.Add("top", "335px")
        'lblResult.Style.Add("left", "20px")
        'lblResult.Style.Add("position", "absolute")
        lblResult.CssClass = "wordstyle2"

        lblEmpInfo.Text = "Employee Info"
        lblEmpInfo.Style.Add("font-weight", "bold")
        lblEmpInfo.Style.Add("font-size", "13px")
        lblEmpInfo.ForeColor = Drawing.Color.Blue

        lblEMPLOYEE_PROFILE_ID.Text = "Employee Profile ID"
        lblYEAR.Text = "Year"
        lblMONTH.Text = "Month"
        lblOPTION_PAY_CYCLE.Text = "Pay Cycle"

        lblBASIC_SALARY.Text = "Basic Salary"
        lblOPTION_PAY_MODE.Text = "Option Pay Mode"
        lblOPTION_PAY_TYPE.Text = "Option Pay Type"
        lblOPTION_EMPLOYMENT_STATUS.Text = "Employment Status"
        lblHRP_RATE.Text = "HRP Rate"
        lblEPF.Text = "EPF"
        lblSOCSO.Text = "SOCSO"
        lblOPTION_CITIZENSHIP.Text = "Citizenship"
        lblOPTION_MARITAL_STATUS.Text = "Marital Status"
        lblPCB.Text = "PCB"
        lblJOIN_DATE.Text = "Date Join"
        lblCONFIRM_DATE.Text = "Confirm Date"
        lblRESIGN_DATE.Text = "Resign Date"
        lblOPTION_SPOUSE_WORKING_STATUS.Text = "Option Spouse Working Status"
        lblNO_OF_CHILD.Text = "No Of Child"

        lblBASIC_SALARY.Style.Add("font-size", "11px")
        lblOPTION_PAY_MODE.Style.Add("font-size", "11px")
        lblOPTION_PAY_TYPE.Style.Add("font-size", "11px")
        lblOPTION_EMPLOYMENT_STATUS.Style.Add("font-size", "11px")
        lblHRP_RATE.Style.Add("font-size", "11px")
        lblEPF.Style.Add("font-size", "11px")
        lblSOCSO.Style.Add("font-size", "11px")
        lblOPTION_CITIZENSHIP.Style.Add("font-size", "11px")
        lblOPTION_MARITAL_STATUS.Style.Add("font-size", "11px")
        lblPCB.Style.Add("font-size", "11px")
        lblJOIN_DATE.Style.Add("font-size", "11px")
        lblCONFIRM_DATE.Style.Add("font-size", "11px")
        lblRESIGN_DATE.Style.Add("font-size", "11px")
        lblOPTION_SPOUSE_WORKING_STATUS.Style.Add("font-size", "11px")
        lblNO_OF_CHILD.Style.Add("font-size", "11px")

        lblBASIC_SALARY.Style.Add("font-family", "Arial Unicode MS")
        lblOPTION_PAY_MODE.Style.Add("font-family", "Arial Unicode MS")
        lblOPTION_PAY_TYPE.Style.Add("font-family", "Arial Unicode MS")
        lblOPTION_EMPLOYMENT_STATUS.Style.Add("font-family", "Arial Unicode MS")
        lblHRP_RATE.Style.Add("font-family", "Arial Unicode MS")
        lblEPF.Style.Add("font-family", "Arial Unicode MS")
        lblSOCSO.Style.Add("font-family", "Arial Unicode MS")
        lblOPTION_CITIZENSHIP.Style.Add("font-family", "Arial Unicode MS")
        lblOPTION_MARITAL_STATUS.Style.Add("font-family", "Arial Unicode MS")
        lblPCB.Style.Add("font-family", "Arial Unicode MS")
        lblJOIN_DATE.Style.Add("font-family", "Arial Unicode MS")
        lblCONFIRM_DATE.Style.Add("font-family", "Arial Unicode MS")
        lblRESIGN_DATE.Style.Add("font-family", "Arial Unicode MS")
        lblOPTION_SPOUSE_WORKING_STATUS.Style.Add("font-family", "Arial Unicode MS")
        lblNO_OF_CHILD.Style.Add("font-family", "Arial Unicode MS")

        txtBASIC_SALARY.Style.Add("font-family", "Arial Unicode MS")
        txtOPTION_PAY_MODE.Style.Add("font-family", "Arial Unicode MS")
        txtOPTION_PAY_TYPE.Style.Add("font-family", "Arial Unicode MS")
        txtOPTION_EMPLOYMENT_STATUS.Style.Add("font-family", "Arial Unicode MS")
        txtHRP_RATE.Style.Add("font-family", "Arial Unicode MS")
        txtEPF.Style.Add("font-family", "Arial Unicode MS")
        txtSOCSO.Style.Add("font-family", "Arial Unicode MS")
        txtOPTION_CITIZENSHIP.Style.Add("font-family", "Arial Unicode MS")
        txtOPTION_MARITAL_STATUS.Style.Add("font-family", "Arial Unicode MS")
        txtPCB.Style.Add("font-family", "Arial Unicode MS")
        txtJOIN_DATE.Style.Add("font-family", "Arial Unicode MS")
        txtCONFIRM_DATE.Style.Add("font-family", "Arial Unicode MS")
        txtRESIGN_DATE.Style.Add("font-family", "Arial Unicode MS")
        txtOPTION_SPOUSE_WORKING_STATUS.Style.Add("font-family", "Arial Unicode MS")
        txtNO_OF_CHILD.Style.Add("font-family", "Arial Unicode MS")

        pnlEmpInfo.Height = "160"
        pnlEmpInfo.Width = "750"
        pnlEmpInfo.Style.Add("background-color", "#EFEFEF")

        mySetting.GetLookupValue_ImageButton(imgbtnEMPLOYEE_PROFILE_ID, Form.ID, txtEMPLOYEE_PROFILE_ID.ID.ToString, "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "EMPLOYEE_PROFILE_ID" & """," & """" & """," & """" & Session("EmpID").ToString & """", ddlYEAR.SelectedValue.ToString, ddlMONTH.SelectedValue.ToString, ddlOPTION_PAY_CYCLE.SelectedValue.ToString)
    End Sub

    Sub EmployeeChecking()

        ssql = "Select ISNULL(a.Security_Role_Profile_Code,'USER'), a.Code, b.[Name] From User_Profile a, Employee_CodeName_Vw b " & _
               "Where a.Company_Profile_Code = '" & Session("Company") & "' and b.Company_Profile_Code = '" & Session("Company") & "' " & _
               "and a.Code = '" & Session("EmpID") & "' and b.Code = '" & Session("EmpID") & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            If Left(UCase(myDS.Tables(0).Rows(0).Item(0)), 3) <> "EMP" And Left(UCase(myDS.Tables(0).Rows(0).Item(0)), 3) <> "HOD" Then

            Else
                txtEMPLOYEE_PROFILE_ID.Text = myDS2.Tables(0).Rows(0).Item(1).ToString & " [" & myDS2.Tables(0).Rows(0).Item(2).ToString & "]"
                txtEMPLOYEE_PROFILE_ID.Enabled = False
                imgbtnEMPLOYEE_PROFILE_ID.Enabled = False
                BindGrid()
            End If
        End If
        myDS2 = Nothing

    End Sub

    Private Sub DisplayErrorMessage(ByVal strMessage As String)
        lblResult.Visible = True
        lblResult.Text = strMessage
    End Sub
    Protected Sub MyDLL_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOPTION_PAY_CYCLE.SelectedIndexChanged, ddlYEAR.SelectedIndexChanged, ddlMONTH.SelectedIndexChanged
        If ValidateSearch() = True Then
            BindGrid()
        Else
            pnlGridView.Visible = False
        End If
    End Sub

    Protected Sub myGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles myGridView.RowDataBound
        e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor; this.style.backgroundColor='silver';")
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
    End Sub

    Protected Sub imgbtnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSearch.Click
        If ValidateSearch() = True Then
            BindGrid()
        Else
            pnlGridView.Visible = False
        End If
    End Sub
    Private Sub BindGrid()
        Try
            ssql = "Exec sp_pr_SelEmpBonusDetails N'" & Session("Company").ToString & "',N'" & _
                            Session("Module").ToString & "',N'" & txtEMPLOYEE_PROFILE_ID.Text.ToString & "',N'" & _
                            ddlYEAR.SelectedValue.ToString & "',N'" & ddlMONTH.SelectedValue.ToString & "',N'" & _
                            ddlOPTION_PAY_CYCLE.SelectedValue.ToString & "',N'ALLOW_DEDUCT'"
            myDS = New DataSet
            myDS = mySQL.ExecuteSQL(ssql)
            If Not myDS Is Nothing Then
                pnlGridView.Visible = True
                myGridView.DataSource = myDS.Tables(0).DefaultView
                myGridView.DataBind()
                If myGridView.HeaderRow.Cells.Count > 1 Then
                    myGridView.Rows(myGridView.Rows.Count - 1).Cells(1).ControlStyle.Font.Bold = True
                    myGridView.Rows(myGridView.Rows.Count - 1).Cells(2).ControlStyle.Font.Bold = True
                    myGridView.Rows(myGridView.Rows.Count - 1).Cells(3).ControlStyle.Font.Bold = True
                End If
                If myDS.Tables.Count = 2 Then
                    pnlGridView1.Visible = True
                    myGridView1.DataSource = myDS.Tables(1).DefaultView
                    myGridView1.DataBind()
                End If
            Else
                DisplayErrorMessage("Error In Binding Data For Employee Payroll Details")
            End If
        Catch ex As Exception
            DisplayErrorMessage(ex.Message.ToString)
        End Try
    End Sub
    Private Function ValidateSearch() As Boolean
        If txtEMPLOYEE_PROFILE_ID.Text.ToString.Trim = "" Then
            DisplayErrorMessage("Please select an employee to proceed!")
            txtEMPLOYEE_PROFILE_ID.Focus()
            Return False
        ElseIf ddlMONTH.SelectedValue.ToString.Trim = "" Then
            DisplayErrorMessage("Please select the month to proceed!")
            ddlMONTH.Focus()
            Return False
        ElseIf ddlYEAR.SelectedValue.ToString.Trim = "" Then
            DisplayErrorMessage("Please select the year to proceed!")
            ddlYEAR.Focus()
            Return False
        End If
        Return True
    End Function

    Protected Sub lnkbtnShowHideEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnShowHideEmp.Click
        If pnlEmpInfo.Visible = True Then
            pnlEmpInfo.Visible = False
            lnkbtnShowHideEmp.Text = "Show Employee Info"
        Else
            If txtEMPLOYEE_PROFILE_ID.Text.ToString.Trim <> "" Then
                ssql = "Exec sp_is_SelEmpInfo '" & Session("Company").ToString & "','" & Session("Module").ToString & "','" & txtEMPLOYEE_PROFILE_ID.Text.ToString.Trim & "',''"
                myDS = New DataSet
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                If Not myDS Is Nothing Then
                    If myDS.Tables(0).Rows.Count > 0 Then
                        txtOPTION_PAY_MODE.Text = myDS.Tables(0).Rows(0).Item(2).ToString
                        txtOPTION_PAY_TYPE.Text = myDS.Tables(0).Rows(0).Item(3).ToString
                        txtBASIC_SALARY.Text = myDS.Tables(0).Rows(0).Item(4).ToString
                        txtOPTION_EMPLOYMENT_STATUS.Text = myDS.Tables(0).Rows(0).Item(5).ToString
                        'txtHRP_RATE.Text = ""
                        txtEPF.Text = myDS.Tables(0).Rows(0).Item(6).ToString
                        txtSOCSO.Text = myDS.Tables(0).Rows(0).Item(7).ToString
                        txtPCB.Text = myDS.Tables(0).Rows(0).Item(8).ToString
                        txtOPTION_CITIZENSHIP.Text = myDS.Tables(0).Rows(0).Item(9).ToString
                        txtOPTION_MARITAL_STATUS.Text = myDS.Tables(0).Rows(0).Item(10).ToString
                        txtOPTION_SPOUSE_WORKING_STATUS.Text = myDS.Tables(0).Rows(0).Item(11).ToString
                        txtNO_OF_CHILD.Text = myDS.Tables(0).Rows(0).Item(12).ToString
                        txtJOIN_DATE.Text = myDS.Tables(0).Rows(0).Item(13).ToString
                        txtCONFIRM_DATE.Text = myDS.Tables(0).Rows(0).Item(14).ToString
                        txtRESIGN_DATE.Text = myDS.Tables(0).Rows(0).Item(15).ToString
                    Else
                        DisplayErrorMessage("Cannot find Employee Info For Employee -- " & txtEMPLOYEE_PROFILE_ID.Text.ToString)
                    End If
                Else
                    DisplayErrorMessage("Error In Retrieving Employee Info")
                End If
                pnlEmpInfo.Visible = True
                lnkbtnShowHideEmp.Text = "Hide Employee Info"
            Else
                DisplayErrorMessage("Please select an employee to proceed!")
            End If
        End If
    End Sub
End Class

