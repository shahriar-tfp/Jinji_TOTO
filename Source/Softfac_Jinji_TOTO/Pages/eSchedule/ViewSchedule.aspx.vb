Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class PAGES_ESCHEDULE_VIEWSCHEDULE
    Inherits System.Web.UI.Page

    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting, MyKajima As New clsKajimaWeb
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")

    Dim mMyConnection As New System.Data.SqlClient.SqlConnection
    Dim mMyCommand As New System.Data.SqlClient.SqlCommand
    Dim mMyDataReader As System.Data.SqlClient.SqlDataReader

    Dim mstrNo As String
    Dim mstrEmpName As String

    Dim mstrMon_Plan As String
    Dim mstrMon_Apply As String
    Dim mstrTue_Plan As String
    Dim mstrTue_Apply As String
    Dim mstrWed_Plan As String
    Dim mstrWed_Apply As String
    Dim mstrThu_Plan As String
    Dim mstrThu_Apply As String
    Dim mstrFri_Plan As String
    Dim mstrFri_Apply As String
    Dim mstrSat_Plan As String
    Dim mstrSat_Apply As String
    Dim mstrSun_Plan As String
    Dim mstrSun_Apply As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim lstItem As New ListItem

        If Session("EmpID") = "" Then
            Response.Redirect("../Global/SessionTimeOut.aspx")
        Else
            Session("PreviousPage") = Request.RawUrl
            'gstrEmpID = "I008"
            'lblUser.Text = Session("EmpID")
            lblUser.Text = Session("username")
        End If

        mMyConnection.ConnectionString = mySQL.GetConnectionString()
        mMyConnection.Open()
        mMyCommand.Connection = mMyConnection


        If Not IsPostBack Then
            mMyCommand.CommandText = "sp_sa_Sel_LeaveType '" & "" & "','" & "Web_View" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            While mMyDataReader.Read()
                lstItem = New ListItem
                lstItem.Text = mMyDataReader("LeaveID") & " | " & mMyDataReader("LeaveDesc")
                lstItem.Value = mMyDataReader("LeaveID")
                cboLegend.Items.Add(lstItem)
            End While
            mMyDataReader.Close()
        End If

    End Sub

    Private Sub LeaveSchedule_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        Dim iYear, iMonth, iDay As Integer

        lnkbtnLeaveApplication.ForeColor = Nothing
        lnkbtnLeaveSchedule.ForeColor = Drawing.Color.Red
        lnkbtnLeaveApproval.ForeColor = Nothing

        iYear = Year(Now)
        iMonth = Month(Now)
        iDay = Day(Now)

        If Not IsPostBack Then
            hfWeek.Value = 1

            cboYear.Items.Add(iYear - 1)
            cboYear.Items.Add(iYear)
            cboYear.Items.Add(iYear + 1)

            cboYear.SelectedValue = iYear
            cboMonth.SelectedValue = iMonth
        End If

        If optDepartment.Checked Then
            cboDepartment.Enabled = True
            txtEmp.Text = ""
            txtEmp.Enabled = False
            btnSearch.Enabled = False
            GetDepartment()

            If Not IsPostBack Then
                mMyCommand.CommandText = "select department = isnull(department, '') from is_empmaster where empid = '" & Session("EmpID") & "'"
                mMyDataReader = mMyCommand.ExecuteReader

                If mMyDataReader.Read() Then
                    cboDepartment.SelectedValue = mMyDataReader("department")
                End If
                mMyDataReader.Close()
            End If
        Else
            cboDepartment.Items.Clear()
            cboDepartment.Enabled = False
            txtEmp.Enabled = True
            btnSearch.Enabled = True
        End If

        SetGrid()
        FillDetails()

        If hfMaxNo.Value > 0 Then
            If Val(lblNo1.Text) + 19 < hfMaxNo.Value Then
                lblRecords.Text = "Records " & (hfPage.Value + 1).ToString & " - " & (hfPage.Value + 20).ToString & " of " & hfMaxNo.Value.ToString
            Else
                lblRecords.Text = "Records " & (hfPage.Value + 1).ToString & " - " & hfMaxNo.Value.ToString & " of " & hfMaxNo.Value.ToString
            End If
        Else
            lblRecords.Text = "No record"
        End If




    End Sub
    Private Sub GetDepartment()

        Dim lstItem, lstSelectedItem As New ListItem
        Dim iLoop As Integer

        If cboDepartment.SelectedIndex <> -1 Then
            lstSelectedItem = cboDepartment.Items(cboDepartment.SelectedIndex)
        End If

        cboDepartment.Items.Clear()

        mMyCommand.CommandText = "select distinct department = isnull(department, '') from is_empmaster where department <> '' and resignflag <> 'R' order by department"
        mMyDataReader = mMyCommand.ExecuteReader

        While mMyDataReader.Read()
            lstItem = New ListItem
            lstItem.Text = mMyDataReader("department")
            lstItem.Value = mMyDataReader("department")
            cboDepartment.Items.Add(lstItem)
        End While
        mMyDataReader.Close()

        For iLoop = 0 To cboDepartment.Items.Count - 1
            If cboDepartment.Items(iLoop).Value = lstSelectedItem.Value Then
                cboDepartment.Items(iLoop).Selected = True
                Exit For
            End If
        Next

    End Sub
    Private Sub SetGrid()

        Dim iLoop As Integer

        For iLoop = 1 To 20
            Select Case iLoop
                Case "1"
                    SetInvisible(lblNo1, lblEmpName1, lblSun1_Plan, lblSun1_Apply, lblMon1_Plan, lblMon1_Apply, lblTue1_Plan, lblTue1_Apply, lblWed1_Plan, lblWed1_Apply, lblThu1_Plan, lblThu1_Apply, lblFri1_Plan, lblFri1_Apply, lblSat1_Plan, lblSat1_Apply)
                Case "2"
                    SetInvisible(lblNo2, lblEmpName2, lblSun2_Plan, lblSun2_Apply, lblMon2_Plan, lblMon2_Apply, lblTue2_Plan, lblTue2_Apply, lblWed2_Plan, lblWed2_Apply, lblThu2_Plan, lblThu2_Apply, lblFri2_Plan, lblFri2_Apply, lblSat2_Plan, lblSat2_Apply)
                Case "3"
                    SetInvisible(lblNo3, lblEmpName3, lblSun3_Plan, lblSun3_Apply, lblMon3_Plan, lblMon3_Apply, lblTue3_Plan, lblTue3_Apply, lblWed3_Plan, lblWed3_Apply, lblThu3_Plan, lblThu3_Apply, lblFri3_Plan, lblFri3_Apply, lblSat3_Plan, lblSat3_Apply)
                Case "4"
                    SetInvisible(lblNo4, lblEmpName4, lblSun4_Plan, lblSun4_Apply, lblMon4_Plan, lblMon4_Apply, lblTue4_Plan, lblTue4_Apply, lblWed4_Plan, lblWed4_Apply, lblThu4_Plan, lblThu4_Apply, lblFri4_Plan, lblFri4_Apply, lblSat4_Plan, lblSat4_Apply)
                Case "5"
                    SetInvisible(lblNo5, lblEmpName5, lblSun5_Plan, lblSun5_Apply, lblMon5_Plan, lblMon5_Apply, lblTue5_Plan, lblTue5_Apply, lblWed5_Plan, lblWed5_Apply, lblThu5_Plan, lblThu5_Apply, lblFri5_Plan, lblFri5_Apply, lblSat5_Plan, lblSat5_Apply)
                Case "6"
                    SetInvisible(lblNo6, lblEmpName6, lblSun6_Plan, lblSun6_Apply, lblMon6_Plan, lblMon6_Apply, lblTue6_Plan, lblTue6_Apply, lblWed6_Plan, lblWed6_Apply, lblThu6_Plan, lblThu6_Apply, lblFri6_Plan, lblFri6_Apply, lblSat6_Plan, lblSat6_Apply)
                Case "7"
                    SetInvisible(lblNo7, lblEmpName7, lblSun7_Plan, lblSun7_Apply, lblMon7_Plan, lblMon7_Apply, lblTue7_Plan, lblTue7_Apply, lblWed7_Plan, lblWed7_Apply, lblThu7_Plan, lblThu7_Apply, lblFri7_Plan, lblFri7_Apply, lblSat7_Plan, lblSat7_Apply)
                Case "8"
                    SetInvisible(lblNo8, lblEmpName8, lblSun8_Plan, lblSun8_Apply, lblMon8_Plan, lblMon8_Apply, lblTue8_Plan, lblTue8_Apply, lblWed8_Plan, lblWed8_Apply, lblThu8_Plan, lblThu8_Apply, lblFri8_Plan, lblFri8_Apply, lblSat8_Plan, lblSat8_Apply)
                Case "9"
                    SetInvisible(lblNo9, lblEmpName9, lblSun9_Plan, lblSun9_Apply, lblMon9_Plan, lblMon9_Apply, lblTue9_Plan, lblTue9_Apply, lblWed9_Plan, lblWed9_Apply, lblThu9_Plan, lblThu9_Apply, lblFri9_Plan, lblFri9_Apply, lblSat9_Plan, lblSat9_Apply)
                Case "10"
                    SetInvisible(lblNo10, lblEmpName10, lblSun10_Plan, lblSun10_Apply, lblMon10_Plan, lblMon10_Apply, lblTue10_Plan, lblTue10_Apply, lblWed10_Plan, lblWed10_Apply, lblThu10_Plan, lblThu10_Apply, lblFri10_Plan, lblFri10_Apply, lblSat10_Plan, lblSat10_Apply)
                Case "11"
                    SetInvisible(lblNo11, lblEmpName11, lblSun11_Plan, lblSun11_Apply, lblMon11_Plan, lblMon11_Apply, lblTue11_Plan, lblTue11_Apply, lblWed11_Plan, lblWed11_Apply, lblThu11_Plan, lblThu11_Apply, lblFri11_Plan, lblFri11_Apply, lblSat11_Plan, lblSat11_Apply)
                Case "12"
                    SetInvisible(lblNo12, lblEmpName12, lblSun12_Plan, lblSun12_Apply, lblMon12_Plan, lblMon12_Apply, lblTue12_Plan, lblTue12_Apply, lblWed12_Plan, lblWed12_Apply, lblThu12_Plan, lblThu12_Apply, lblFri12_Plan, lblFri12_Apply, lblSat12_Plan, lblSat12_Apply)
                Case "13"
                    SetInvisible(lblNo13, lblEmpName13, lblSun13_Plan, lblSun13_Apply, lblMon13_Plan, lblMon13_Apply, lblTue13_Plan, lblTue13_Apply, lblWed13_Plan, lblWed13_Apply, lblThu13_Plan, lblThu13_Apply, lblFri13_Plan, lblFri13_Apply, lblSat13_Plan, lblSat13_Apply)
                Case "14"
                    SetInvisible(lblNo14, lblEmpName14, lblSun14_Plan, lblSun14_Apply, lblMon14_Plan, lblMon14_Apply, lblTue14_Plan, lblTue14_Apply, lblWed14_Plan, lblWed14_Apply, lblThu14_Plan, lblThu14_Apply, lblFri14_Plan, lblFri14_Apply, lblSat14_Plan, lblSat14_Apply)
                Case "15"
                    SetInvisible(lblNo15, lblEmpName15, lblSun15_Plan, lblSun15_Apply, lblMon15_Plan, lblMon15_Apply, lblTue15_Plan, lblTue15_Apply, lblWed15_Plan, lblWed15_Apply, lblThu15_Plan, lblThu15_Apply, lblFri15_Plan, lblFri15_Apply, lblSat15_Plan, lblSat15_Apply)
                Case "16"
                    SetInvisible(lblNo16, lblEmpName16, lblSun16_Plan, lblSun16_Apply, lblMon16_Plan, lblMon16_Apply, lblTue16_Plan, lblTue16_Apply, lblWed16_Plan, lblWed16_Apply, lblThu16_Plan, lblThu16_Apply, lblFri16_Plan, lblFri16_Apply, lblSat16_Plan, lblSat16_Apply)
                Case "17"
                    SetInvisible(lblNo17, lblEmpName17, lblSun17_Plan, lblSun17_Apply, lblMon17_Plan, lblMon17_Apply, lblTue17_Plan, lblTue17_Apply, lblWed17_Plan, lblWed17_Apply, lblThu17_Plan, lblThu17_Apply, lblFri17_Plan, lblFri17_Apply, lblSat17_Plan, lblSat17_Apply)
                Case "18"
                    SetInvisible(lblNo18, lblEmpName18, lblSun18_Plan, lblSun18_Apply, lblMon18_Plan, lblMon18_Apply, lblTue18_Plan, lblTue18_Apply, lblWed18_Plan, lblWed18_Apply, lblThu18_Plan, lblThu18_Apply, lblFri18_Plan, lblFri18_Apply, lblSat18_Plan, lblSat18_Apply)
                Case "19"
                    SetInvisible(lblNo19, lblEmpName19, lblSun19_Plan, lblSun19_Apply, lblMon19_Plan, lblMon19_Apply, lblTue19_Plan, lblTue19_Apply, lblWed19_Plan, lblWed19_Apply, lblThu19_Plan, lblThu19_Apply, lblFri19_Plan, lblFri19_Apply, lblSat19_Plan, lblSat19_Apply)
                Case "20"
                    SetInvisible(lblNo20, lblEmpName20, lblSun20_Plan, lblSun20_Apply, lblMon20_Plan, lblMon20_Apply, lblTue20_Plan, lblTue20_Apply, lblWed20_Plan, lblWed20_Apply, lblThu20_Plan, lblThu20_Apply, lblFri20_Plan, lblFri20_Apply, lblSat20_Plan, lblSat20_Apply)
            End Select
        Next

    End Sub

    Private Sub SetInvisible(ByRef lblNo As Label, ByRef lblEmpName As Label, _
                             ByRef lblSun_Plan As Label, ByRef lblSun_Apply As Label, _
                             ByRef lblMon_Plan As Label, ByRef lblMon_Apply As Label, _
                             ByRef lblTue_Plan As Label, ByRef lblTue_Apply As Label, _
                             ByRef lblWed_Plan As Label, ByRef lblWed_Apply As Label, _
                             ByRef lblThu_Plan As Label, ByRef lblThu_Apply As Label, _
                             ByRef lblFri_Plan As Label, ByRef lblFri_Apply As Label, _
                             ByRef lblSat_Plan As Label, ByRef lblSat_Apply As Label)

        lblNo.Visible = False
        lblEmpName.Visible = False

        lblSun_Plan.Visible = False
        lblSun_Apply.Visible = False

        lblMon_Plan.Visible = False
        lblMon_Apply.Visible = False

        lblTue_Plan.Visible = False
        lblTue_Apply.Visible = False

        lblWed_Plan.Visible = False
        lblWed_Apply.Visible = False

        lblThu_Plan.Visible = False
        lblThu_Apply.Visible = False

        lblFri_Plan.Visible = False
        lblFri_Apply.Visible = False

        lblSat_Plan.Visible = False
        lblSat_Apply.Visible = False

    End Sub

    Private Sub FillDetails()

        mMyCommand.CommandText = "sp_ls_LeaveSchedule " & cboYear.SelectedValue & "," & cboMonth.SelectedValue & "," & hfWeek.Value & ",'" & "" & "','" & _
                                 "" & "','" & "Day" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read() Then
            If Val(mMyDataReader("monday_date").ToString.Substring(3, 2)) = cboMonth.SelectedValue Then
                lblMonday.Text = "Monday" & " " & Left(mMyDataReader("monday_date"), 2)
            Else
                lblMonday.Text = "Monday"
            End If

            If Val(mMyDataReader("tuesday_date").ToString.Substring(3, 2)) = cboMonth.SelectedValue Then
                lblTuesday.Text = "Tuesday" & " " & Left(mMyDataReader("tuesday_date"), 2)
            Else
                lblTuesday.Text = "Tuesday"
            End If

            If Val(mMyDataReader("wednesday_date").ToString.Substring(3, 2)) = cboMonth.SelectedValue Then
                lblWednesday.Text = "Wednesday" & " " & Left(mMyDataReader("wednesday_date"), 2)
            Else
                lblWednesday.Text = "Wednesday"
            End If

            If Val(mMyDataReader("thursday_date").ToString.Substring(3, 2)) = cboMonth.SelectedValue Then
                lblThursday.Text = "Thursday" & " " & Left(mMyDataReader("thursday_date"), 2)
            Else
                lblThursday.Text = "Thursday"
            End If

            If Val(mMyDataReader("friday_date").ToString.Substring(3, 2)) = cboMonth.SelectedValue Then
                lblFriday.Text = "Friday" & " " & Left(mMyDataReader("friday_date"), 2)
            Else
                lblFriday.Text = "Friday"
            End If

            If Val(mMyDataReader("saturday_date").ToString.Substring(3, 2)) = cboMonth.SelectedValue Then
                lblSaturday.Text = "Saturday" & " " & Left(mMyDataReader("saturday_date"), 2)
            Else
                lblSaturday.Text = "Saturday"
            End If

            If Val(mMyDataReader("sunday_date").ToString.Substring(3, 2)) = cboMonth.SelectedValue Then
                lblSunday.Text = "Sunday" & " " & Left(mMyDataReader("sunday_date"), 2)
            Else
                lblSunday.Text = "Sunday"
            End If

            hfWeek.Value = mMyDataReader("week")
        End If
        mMyDataReader.Close()

        Dim strSrcBy, strCriteria As String

        If optDepartment.Checked Then
            strSrcBy = "Department"
            strCriteria = cboDepartment.SelectedValue
        Else
            strSrcBy = "Employee"
            strCriteria = Replace(txtEmp.Text, "'", "''")
        End If

        hfMaxNo.Value = 0

        mMyCommand.CommandText = "sp_ls_LeaveSchedule " & cboYear.SelectedValue & "," & cboMonth.SelectedValue & "," & hfWeek.Value & ",'" & strSrcBy & "','" & _
                                 strCriteria & "','" & "Schedule" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        While mMyDataReader.Read()
            hfMaxNo.Value = Val(mMyDataReader("no"))
            mstrNo = Val(mMyDataReader("no"))
            mstrEmpName = mMyDataReader("empname")

            If Val(mMyDataReader("monday_date").ToString.Substring(3, 2)) = cboMonth.SelectedValue Then
                mstrMon_Plan = mMyDataReader("monday_plan")
                mstrMon_Apply = mMyDataReader("monday_apply")
            Else
                mstrMon_Plan = ""
                mstrMon_Apply = ""
            End If

            If Val(mMyDataReader("tuesday_date").ToString.Substring(3, 2)) = cboMonth.SelectedValue Then
                mstrTue_Plan = mMyDataReader("tuesday_plan")
                mstrTue_Apply = mMyDataReader("tuesday_apply")
            Else
                mstrTue_Plan = ""
                mstrTue_Apply = ""
            End If

            If Val(mMyDataReader("wednesday_date").ToString.Substring(3, 2)) = cboMonth.SelectedValue Then
                mstrWed_Plan = mMyDataReader("wednesday_plan")
                mstrWed_Apply = mMyDataReader("wednesday_apply")
            Else
                mstrWed_Plan = ""
                mstrWed_Apply = ""
            End If


            If Val(mMyDataReader("thursday_date").ToString.Substring(3, 2)) = cboMonth.SelectedValue Then
                mstrThu_Plan = mMyDataReader("thursday_plan")
                mstrThu_Apply = mMyDataReader("thursday_apply")
            Else
                mstrThu_Plan = ""
                mstrThu_Apply = ""
            End If

            If Val(mMyDataReader("friday_date").ToString.Substring(3, 2)) = cboMonth.SelectedValue Then
                mstrFri_Plan = mMyDataReader("friday_plan")
                mstrFri_Apply = mMyDataReader("friday_apply")
            Else
                mstrFri_Plan = ""
                mstrFri_Apply = ""
            End If

            If Val(mMyDataReader("saturday_date").ToString.Substring(3, 2)) = cboMonth.SelectedValue Then
                mstrSat_Plan = mMyDataReader("saturday_plan")
                mstrSat_Apply = mMyDataReader("saturday_apply")
            Else
                mstrSat_Plan = ""
                mstrSat_Apply = ""
            End If

            If Val(mMyDataReader("sunday_date").ToString.Substring(3, 2)) = cboMonth.SelectedValue Then
                mstrSun_Plan = mMyDataReader("sunday_plan")
                mstrSun_Apply = mMyDataReader("sunday_apply")
            Else
                mstrSun_Plan = ""
                mstrSun_Apply = ""
            End If

            Select Case mMyDataReader("no")
                Case 1 + hfPage.Value
                    FillDetails2(lblNo1, lblEmpName1, lblMon1_Plan, lblMon1_Apply, lblTue1_Plan, lblTue1_Apply, lblWed1_Plan, lblWed1_Apply, lblThu1_Plan, lblThu1_Apply, lblFri1_Plan, lblFri1_Apply, lblSat1_Plan, lblSat1_Apply, lblSun1_Plan, lblSun1_Apply)
                Case 2 + hfPage.Value
                    FillDetails2(lblNo2, lblEmpName2, lblMon2_Plan, lblMon2_Apply, lblTue2_Plan, lblTue2_Apply, lblWed2_Plan, lblWed2_Apply, lblThu2_Plan, lblThu2_Apply, lblFri2_Plan, lblFri2_Apply, lblSat2_Plan, lblSat2_Apply, lblSun2_Plan, lblSun2_Apply)
                Case 3 + hfPage.Value
                    FillDetails2(lblNo3, lblEmpName3, lblMon3_Plan, lblMon3_Apply, lblTue3_Plan, lblTue3_Apply, lblWed3_Plan, lblWed3_Apply, lblThu3_Plan, lblThu3_Apply, lblFri3_Plan, lblFri3_Apply, lblSat3_Plan, lblSat3_Apply, lblSun3_Plan, lblSun3_Apply)
                Case 4 + hfPage.Value
                    FillDetails2(lblNo4, lblEmpName4, lblMon4_Plan, lblMon4_Apply, lblTue4_Plan, lblTue4_Apply, lblWed4_Plan, lblWed4_Apply, lblThu4_Plan, lblThu4_Apply, lblFri4_Plan, lblFri4_Apply, lblSat4_Plan, lblSat4_Apply, lblSun4_Plan, lblSun4_Apply)
                Case 5 + hfPage.Value
                    FillDetails2(lblNo5, lblEmpName5, lblMon5_Plan, lblMon5_Apply, lblTue5_Plan, lblTue5_Apply, lblWed5_Plan, lblWed5_Apply, lblThu5_Plan, lblThu5_Apply, lblFri5_Plan, lblFri5_Apply, lblSat5_Plan, lblSat5_Apply, lblSun5_Plan, lblSun5_Apply)
                Case 6 + hfPage.Value
                    FillDetails2(lblNo6, lblEmpName6, lblMon6_Plan, lblMon6_Apply, lblTue6_Plan, lblTue6_Apply, lblWed6_Plan, lblWed6_Apply, lblThu6_Plan, lblThu6_Apply, lblFri6_Plan, lblFri6_Apply, lblSat6_Plan, lblSat6_Apply, lblSun6_Plan, lblSun6_Apply)
                Case 7 + hfPage.Value
                    FillDetails2(lblNo7, lblEmpName7, lblMon7_Plan, lblMon7_Apply, lblTue7_Plan, lblTue7_Apply, lblWed7_Plan, lblWed7_Apply, lblThu7_Plan, lblThu7_Apply, lblFri7_Plan, lblFri7_Apply, lblSat7_Plan, lblSat7_Apply, lblSun7_Plan, lblSun7_Apply)
                Case 8 + hfPage.Value
                    FillDetails2(lblNo8, lblEmpName8, lblMon8_Plan, lblMon8_Apply, lblTue8_Plan, lblTue8_Apply, lblWed8_Plan, lblWed8_Apply, lblThu8_Plan, lblThu8_Apply, lblFri8_Plan, lblFri8_Apply, lblSat8_Plan, lblSat8_Apply, lblSun8_Plan, lblSun8_Apply)
                Case 9 + hfPage.Value
                    FillDetails2(lblNo9, lblEmpName9, lblMon9_Plan, lblMon9_Apply, lblTue9_Plan, lblTue9_Apply, lblWed9_Plan, lblWed9_Apply, lblThu9_Plan, lblThu9_Apply, lblFri9_Plan, lblFri9_Apply, lblSat9_Plan, lblSat9_Apply, lblSun9_Plan, lblSun9_Apply)
                Case 10 + hfPage.Value
                    FillDetails2(lblNo10, lblEmpName10, lblMon10_Plan, lblMon10_Apply, lblTue10_Plan, lblTue10_Apply, lblWed10_Plan, lblWed10_Apply, lblThu10_Plan, lblThu10_Apply, lblFri10_Plan, lblFri10_Apply, lblSat10_Plan, lblSat10_Apply, lblSun10_Plan, lblSun10_Apply)
                Case 11 + hfPage.Value
                    FillDetails2(lblNo11, lblEmpName11, lblMon11_Plan, lblMon11_Apply, lblTue11_Plan, lblTue11_Apply, lblWed11_Plan, lblWed11_Apply, lblThu11_Plan, lblThu11_Apply, lblFri11_Plan, lblFri11_Apply, lblSat11_Plan, lblSat11_Apply, lblSun11_Plan, lblSun11_Apply)
                Case 12 + hfPage.Value
                    FillDetails2(lblNo12, lblEmpName12, lblMon12_Plan, lblMon12_Apply, lblTue12_Plan, lblTue12_Apply, lblWed12_Plan, lblWed12_Apply, lblThu12_Plan, lblThu12_Apply, lblFri12_Plan, lblFri12_Apply, lblSat12_Plan, lblSat12_Apply, lblSun12_Plan, lblSun12_Apply)
                Case 13 + hfPage.Value
                    FillDetails2(lblNo13, lblEmpName13, lblMon13_Plan, lblMon13_Apply, lblTue13_Plan, lblTue13_Apply, lblWed13_Plan, lblWed13_Apply, lblThu13_Plan, lblThu13_Apply, lblFri13_Plan, lblFri13_Apply, lblSat13_Plan, lblSat13_Apply, lblSun13_Plan, lblSun13_Apply)
                Case 14 + hfPage.Value
                    FillDetails2(lblNo14, lblEmpName14, lblMon14_Plan, lblMon14_Apply, lblTue14_Plan, lblTue14_Apply, lblWed14_Plan, lblWed14_Apply, lblThu14_Plan, lblThu14_Apply, lblFri14_Plan, lblFri14_Apply, lblSat14_Plan, lblSat14_Apply, lblSun14_Plan, lblSun14_Apply)
                Case 15 + hfPage.Value
                    FillDetails2(lblNo15, lblEmpName15, lblMon15_Plan, lblMon15_Apply, lblTue15_Plan, lblTue15_Apply, lblWed15_Plan, lblWed15_Apply, lblThu15_Plan, lblThu15_Apply, lblFri15_Plan, lblFri15_Apply, lblSat15_Plan, lblSat15_Apply, lblSun15_Plan, lblSun15_Apply)
                Case 16 + hfPage.Value
                    FillDetails2(lblNo16, lblEmpName16, lblMon16_Plan, lblMon16_Apply, lblTue16_Plan, lblTue16_Apply, lblWed16_Plan, lblWed16_Apply, lblThu16_Plan, lblThu16_Apply, lblFri16_Plan, lblFri16_Apply, lblSat16_Plan, lblSat16_Apply, lblSun16_Plan, lblSun16_Apply)
                Case 17 + hfPage.Value
                    FillDetails2(lblNo17, lblEmpName17, lblMon17_Plan, lblMon17_Apply, lblTue17_Plan, lblTue17_Apply, lblWed17_Plan, lblWed17_Apply, lblThu17_Plan, lblThu17_Apply, lblFri17_Plan, lblFri17_Apply, lblSat17_Plan, lblSat17_Apply, lblSun17_Plan, lblSun17_Apply)
                Case 18 + hfPage.Value
                    FillDetails2(lblNo18, lblEmpName18, lblMon18_Plan, lblMon18_Apply, lblTue18_Plan, lblTue18_Apply, lblWed18_Plan, lblWed18_Apply, lblThu18_Plan, lblThu18_Apply, lblFri18_Plan, lblFri18_Apply, lblSat18_Plan, lblSat18_Apply, lblSun18_Plan, lblSun18_Apply)
                Case 19 + hfPage.Value
                    FillDetails2(lblNo19, lblEmpName19, lblMon19_Plan, lblMon19_Apply, lblTue19_Plan, lblTue19_Apply, lblWed19_Plan, lblWed19_Apply, lblThu19_Plan, lblThu19_Apply, lblFri19_Plan, lblFri19_Apply, lblSat19_Plan, lblSat19_Apply, lblSun19_Plan, lblSun19_Apply)
                Case 20 + hfPage.Value
                    FillDetails2(lblNo20, lblEmpName20, lblMon20_Plan, lblMon20_Apply, lblTue20_Plan, lblTue20_Apply, lblWed20_Plan, lblWed20_Apply, lblThu20_Plan, lblThu20_Apply, lblFri20_Plan, lblFri20_Apply, lblSat20_Plan, lblSat20_Apply, lblSun20_Plan, lblSun20_Apply)
            End Select

        End While
        mMyDataReader.Close()




    End Sub

    Private Sub FillDetails2(ByRef lblNo As Label, ByRef lblEmpName As Label, _
                             ByRef lblMon_Plan As Label, ByRef lblMon_Apply As Label, _
                             ByRef lblTue_Plan As Label, ByRef lblTue_Apply As Label, _
                             ByRef lblWed_Plan As Label, ByRef lblWed_Apply As Label, _
                             ByRef lblThu_Plan As Label, ByRef lblThu_Apply As Label, _
                             ByRef lblFri_Plan As Label, ByRef lblFri_Apply As Label, _
                             ByRef lblSat_Plan As Label, ByRef lblSat_Apply As Label, _
                             ByRef lblSun_Plan As Label, ByRef lblSun_Apply As Label)

        lblNo.Visible = True
        lblEmpName.Visible = True

        lblMon_Plan.Visible = True
        lblMon_Apply.Visible = True

        lblTue_Plan.Visible = True
        lblTue_Apply.Visible = True

        lblWed_Plan.Visible = True
        lblWed_Apply.Visible = True

        lblThu_Plan.Visible = True
        lblThu_Apply.Visible = True

        lblFri_Plan.Visible = True
        lblFri_Apply.Visible = True

        lblSat_Plan.Visible = True
        lblSat_Apply.Visible = True

        lblSun_Plan.Visible = True
        lblSun_Apply.Visible = True

        lblNo.Text = mstrNo
        lblEmpName.Text = mstrEmpName
        lblEmpName.ToolTip = mstrEmpName

        lblMon_Plan.Text = mstrMon_Plan
        lblMon_Apply.Text = mstrMon_Apply

        lblTue_Plan.Text = mstrTue_Plan
        lblTue_Apply.Text = mstrTue_Apply

        lblWed_Plan.Text = mstrWed_Plan
        lblWed_Apply.Text = mstrWed_Apply

        lblThu_Plan.Text = mstrThu_Plan
        lblThu_Apply.Text = mstrThu_Apply

        lblFri_Plan.Text = mstrFri_Plan
        lblFri_Apply.Text = mstrFri_Apply

        lblSat_Plan.Text = mstrSat_Plan
        lblSat_Apply.Text = mstrSat_Apply

        lblSun_Plan.Text = mstrSun_Plan
        lblSun_Apply.Text = mstrSun_Apply


        'If mstrNo = 11 Then Response.Write("-" & mstrEmpName & "-")

    End Sub


    Private Sub lnkbtnLeaveSchedule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLeaveSchedule.Click

        Response.Redirect("viewschedule.aspx", True)

    End Sub

    Private Sub lnkbtnLeaveApplication_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLeaveApplication.Click

        Response.Redirect("scheduleapplication.aspx", True)

    End Sub

    Private Sub lnkbtnLeaveApproval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLeaveApproval.Click

        Response.Redirect("scheduleapproval.aspx", True)

    End Sub

    Private Sub LeaveSchedule_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload

        mMyConnection.Close()

    End Sub


    Private Sub lnkbtnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnNext.Click

        hfWeek.Value = hfWeek.Value + 1

        'If hfWeek.Value = 7 Then hfWeek.Value = 6



    End Sub

    Private Sub lnkbtnPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnPrevious.Click

        hfWeek.Value = hfWeek.Value - 1

        If hfWeek.Value = 0 Then hfWeek.Value = 1

    End Sub
    Private Sub lnkNextPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNextPage.Click

        hfPage.Value = Val(hfPage.Value) + 20

        If Val(hfPage.Value) > Val(hfMaxNo.Value) Then hfPage.Value = Val(hfPage.Value) - 20
        'lnkbtnNext.ForeColor = Drawing.Color.Red

    End Sub

    Private Sub lnkPreviousPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPreviousPage.Click

        hfPage.Value = Val(hfPage.Value) - 20

        If Val(hfPage.Value) < 0 Then hfPage.Value = 0
        'lnkbtnPrevious.ForeColor = Drawing.Color.Red

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        hfPage.Value = 0

    End Sub

    Private Sub optEmp_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optEmp.CheckedChanged

        hfPage.Value = 0

    End Sub

    Private Sub optDepartment_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optDepartment.CheckedChanged

        hfPage.Value = 0

    End Sub

    Private Sub cboMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMonth.SelectedIndexChanged

        hfWeek.Value = 1

    End Sub

    Private Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged

        hfWeek.Value = 1

    End Sub
    Private Sub lnkChangePwd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkChangePwd.Click

        Response.Redirect("changePwd.aspx", True)

    End Sub

    Private Sub lnkLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogout.Click

        Session("EmpID") = ""
        Response.Redirect("../Global/SessionTimeOut.aspx")

    End Sub

    Private Sub lnkbtnLeaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLeaveReport.Click

        Response.Redirect("scheduleReport.aspx", True)

    End Sub
    Protected Sub lnkMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkMain.Click

        Dim strURL As String
        Dim strCurrentDay, strCurrentMonth, strCurrentYear As String

        strCurrentDay = Now.Day.ToString
        strCurrentMonth = Now.Month.ToString
        strCurrentYear = Now.Year.ToString

        If strCurrentDay.Length = 1 Then strCurrentDay = "0" & strCurrentDay

        If strCurrentMonth.Length = 1 Then strCurrentMonth = "0" & strCurrentMonth

        strURL = "/home/main.aspx?" & MyKajima.EncryptedText(Session("EmpID"))

        Session("EmpID") = ""
        Response.Redirect(strURL, True)

    End Sub
End Class