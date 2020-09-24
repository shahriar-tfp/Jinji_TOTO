Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class PAGES_ESCHEDULE_SCHEDULEAPPROVAL
    Inherits System.Web.UI.Page

    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting, MyKajima As New clsKajimaWeb
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")

    Dim mMyConnection As New System.Data.SqlClient.SqlConnection
    Dim mMyCommand As New System.Data.SqlClient.SqlCommand
    Dim mMyDataReader As System.Data.SqlClient.SqlDataReader

    Dim mSmtpMail As New System.Net.Mail.SmtpClient
    Dim mSmtpMsg As New System.Net.Mail.MailMessage

    Dim miNo As Integer
    Dim mstrName As String = ""
    Dim mstrDate As String
    Dim mstrDay As String
    Dim mstrPeriod As String
    Dim mstrLeave As String
    Dim mstrApplied As String
    Dim mstrReason As String
    Dim mstrDestination As String
    Dim mstrApprovedBy As String
    Dim mstrDateApproved As String
    Dim mstrRemark As String
    Dim mstrEmp As String

    Private Sub LeaveApproval_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        If Session("EmpID") = "" Then
            Response.Redirect("../Global/SessionTimeOut.aspx")
        Else
            Session("PreviousPage") = Request.RawUrl
            lblUser.Text = Session("username")
        End If

        mMyConnection.ConnectionString = mySQL.GetConnectionString()
        mMyConnection.Open()
        mMyCommand.Connection = mMyConnection


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblRequired.Visible = False
        txtRemark.BackColor = Drawing.Color.White

    End Sub

    Private Sub LeaveApproval_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        Dim iYear, iMonth, iDay As Integer
        Dim lstItem As New ListItem
        Dim iLoop As Integer

        lnkbtnLeaveApplication.ForeColor = Nothing
        lnkbtnLeaveSchedule.ForeColor = Nothing
        lnkbtnLeaveApproval.ForeColor = Drawing.Color.Red

        iYear = Year(Now)
        iMonth = Month(Now)
        iDay = Day(Now)

        If Not IsPostBack Then
            FillStatus(cboStatus)
            FillStatus(lblStatus)

            lstItem = New ListItem
            lstItem.Text = "Cancel"
            lstItem.Value = "C"
            lblStatus.Items.Add(lstItem)

            lstItem = New ListItem
            lstItem.Text = "-- NA --"
            lstItem.Value = "N"
            lblStatus.Items.Add(lstItem)

            cboStatus.SelectedIndex = 0

            cboYear.Items.Add(iYear - 1)
            cboYear.Items.Add(iYear)
            cboYear.Items.Add(iYear + 1)

            cboYear.SelectedValue = iYear
            cboMonth.SelectedValue = iMonth
        End If

        If optPersonal.Checked Then
            lblStatus.Items(0).Enabled = False
            lblStatus.Items(1).Enabled = False
            lblStatus.Items(2).Enabled = False

            If cboStatus.SelectedValue = "P" Or cboStatus.SelectedValue = "R" Then
                lblStatus.SelectedIndex = 3
                lblStatus.Items(3).Enabled = True
                lblStatus.Items(4).Enabled = False
            Else
                lblStatus.SelectedIndex = 4
                lblStatus.Items(3).Enabled = False
                lblStatus.Items(4).Enabled = True
            End If

            lblStatus.Enabled = False
            cboSubordinate.Enabled = False
            cboSubordinate.Items.Clear()
        Else
            If cboStatus.SelectedValue = "P" Then
                lblStatus.Items(0).Enabled = False
                lblStatus.Items(1).Enabled = True
                lblStatus.Items(2).Enabled = True

                GetSubordindate("P_SUB_LIST")
            Else
                If cboStatus.SelectedValue = "A" Then
                    lblStatus.Items(0).Enabled = True
                    lblStatus.Items(1).Enabled = False
                    lblStatus.Items(2).Enabled = True
                Else
                    lblStatus.Items(0).Enabled = True
                    lblStatus.Items(1).Enabled = True
                    lblStatus.Items(2).Enabled = False
                End If

                GetSubordindate("A_SUB_LIST")
            End If

            lblStatus.Items(3).Enabled = False
            lblStatus.Items(4).Enabled = False

            lblStatus.Enabled = True
            cboSubordinate.Enabled = True
        End If

        If cboStatus.SelectedValue = "P" Then
            cboMonth.Enabled = False
            For iLoop = 0 To cboMonth.Items.Count - 1
                cboMonth.Items(iLoop).Enabled = cboMonth.Enabled
            Next

            cboYear.Enabled = False
            For iLoop = 0 To cboYear.Items.Count - 1
                cboYear.Items(iLoop).Enabled = cboYear.Enabled
            Next
        Else
            cboMonth.Enabled = True
            For iLoop = 0 To cboMonth.Items.Count - 1
                cboMonth.Items(iLoop).Enabled = cboMonth.Enabled
            Next

            cboYear.Enabled = True
            For iLoop = 0 To cboYear.Items.Count - 1
                cboYear.Items(iLoop).Enabled = cboYear.Enabled
            Next
        End If

        FillDetail()

        txtRemark.Text = ""

        If optPersonal.Checked Then
            If cboStatus.SelectedValue = "P" Or cboStatus.SelectedValue = "R" Then
                btnOK.Enabled = True
            Else
                btnOK.Enabled = False
            End If

            txtRemark.Enabled = False
        Else
            btnOK.Enabled = True
            txtRemark.Enabled = True
        End If

        If hfMaxNo.Value = 0 Then
            btnOK.Enabled = False
            txtRemark.Enabled = False
        End If

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
    Private Sub GetSubordindate(ByVal strType As String)

        Dim lstItem, lstSelectedItem As New ListItem
        Dim iLoop As Integer

        If cboSubordinate.SelectedIndex <> -1 Then
            lstSelectedItem = cboSubordinate.Items(cboSubordinate.SelectedIndex)
        End If

        cboSubordinate.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = "-- All --"
        lstItem.Value = "A"
        cboSubordinate.Items.Add(lstItem)

        mMyCommand.CommandText = "sp_ls_sel_LeaveApproval '" & Session("EmpID") & "','" & "" & "','" & cboStatus.SelectedValue & "'," & cboMonth.SelectedValue & "," & cboYear.SelectedValue & ",'" & strType & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        While mMyDataReader.Read()
            lstItem = New ListItem
            lstItem.Text = mMyDataReader("empname")
            lstItem.Value = mMyDataReader("empid")
            cboSubordinate.Items.Add(lstItem)
        End While
        mMyDataReader.Close()

        For iLoop = 0 To cboSubordinate.Items.Count - 1
            If cboSubordinate.Items(iLoop).Value = lstSelectedItem.Value Then
                cboSubordinate.Items(iLoop).Selected = True
                Exit For
            End If
        Next

    End Sub
    Private Sub FillStatus(ByRef cboCombo As DropDownList)


        Dim lstItem As New ListItem

        mMyCommand.CommandText = "select IDDesc, ID from sa_reference where type = 'LeaveStatus' order by seq"
        mMyDataReader = mMyCommand.ExecuteReader

        While mMyDataReader.Read()
            lstItem = New ListItem
            lstItem.Text = mMyDataReader("IDDesc")
            lstItem.Value = mMyDataReader("ID")
            cboCombo.Items.Add(lstItem)
        End While
        mMyDataReader.Close()

    End Sub
    Private Sub FillDetail()

        Dim strType As String
        Dim strEmp As String = ""

        SetGrid()

        hfMaxNo.Value = 0
        'hfPageLast.Value = 0

        If optPersonal.Checked Then
            strEmp = Session("EmpID")
            If cboStatus.SelectedValue = "P" Then
                strType = "P_PERSONAL"
            Else
                strType = "A_PERSONAL"
            End If
        Else
            If cboStatus.SelectedValue = "P" Then
                If cboSubordinate.SelectedValue = "A" Then
                    strType = "P_SUB_ALL"
                Else
                    strEmp = cboSubordinate.SelectedValue
                    strType = "P_SUB_EMP"
                End If
            Else
                If cboSubordinate.SelectedValue = "A" Then
                    strType = "A_SUB_ALL"
                Else
                    strEmp = cboSubordinate.SelectedValue
                    strType = "A_SUB_EMP"
                End If
            End If
        End If


        mMyCommand.CommandText = "sp_ls_sel_LeaveApproval '" & Session("EmpID") & "','" & strEmp & "','" & cboStatus.SelectedValue & "'," & cboMonth.SelectedValue & "," & cboYear.SelectedValue & ",'" & strType & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        While mMyDataReader.Read()
            hfMaxNo.Value = mMyDataReader("no")
            mstrRemark = ""

            If mMyDataReader("No") > hfPage.Value And mMyDataReader("No") <= hfPage.Value + 20 Then
                miNo = mMyDataReader("no")
                mstrName = mMyDataReader("empname")
                mstrDate = mMyDataReader("date")
                mstrPeriod = mMyDataReader("period")
                mstrLeave = mMyDataReader("leave")
                mstrDestination = mMyDataReader("destination")
                mstrApplied = mMyDataReader("dateapplied")
                mstrReason = mMyDataReader("reason")
                mstrApprovedBy = mMyDataReader("approvedby")
                mstrDateApproved = mMyDataReader("dateapproved")



                If cboStatus.SelectedValue = "P" Then
                    mstrRemark = ""
                ElseIf cboStatus.SelectedValue = "A" Then
                    mstrRemark = "Approved By: " & mstrApprovedBy & vbCrLf & "Approved On: " & mstrDateApproved

                    If mMyDataReader("remark") <> "" Then
                        mstrRemark = mstrRemark & vbCrLf & "Remark: " & mMyDataReader("remark")
                    Else
                        mstrRemark = mstrRemark & vbCrLf & "Remark: ---"

                    End If


                ElseIf cboStatus.SelectedValue = "R" Then
                    mstrRemark = "Rejected By: " & mstrApprovedBy & vbCrLf & "Rejected On: " & mstrDateApproved

                    If mMyDataReader("remark") <> "" Then
                        mstrRemark = mstrRemark & vbCrLf & "Remark: " & mMyDataReader("remark")
                    Else
                        mstrRemark = mstrRemark & vbCrLf & "Remark: ---"
                    End If

                End If

                Select Case mMyDataReader("no")
                    Case 1 + hfPage.Value
                        FillDetails2(lblNo1, ChkBox1, lblName1, lblDate1, lblPeriod1, lblLeave1, lblApplied1)
                    Case 2 + hfPage.Value
                        FillDetails2(lblNo2, ChkBox2, lblName2, lblDate2, lblPeriod2, lblLeave2, lblApplied2)
                    Case 3 + hfPage.Value
                        FillDetails2(lblNo3, ChkBox3, lblName3, lblDate3, lblPeriod3, lblLeave3, lblApplied3)
                    Case 4 + hfPage.Value
                        FillDetails2(lblNo4, ChkBox4, lblName4, lblDate4, lblPeriod4, lblLeave4, lblApplied4)
                    Case 5 + hfPage.Value
                        FillDetails2(lblNo5, ChkBox5, lblName5, lblDate5, lblPeriod5, lblLeave5, lblApplied5)
                    Case 6 + hfPage.Value
                        FillDetails2(lblNo6, ChkBox6, lblName6, lblDate6, lblPeriod6, lblLeave6, lblApplied6)
                    Case 7 + hfPage.Value
                        FillDetails2(lblNo7, ChkBox7, lblName7, lblDate7, lblPeriod7, lblLeave7, lblApplied7)
                    Case 8 + hfPage.Value
                        FillDetails2(lblNo8, ChkBox8, lblName8, lblDate8, lblPeriod8, lblLeave8, lblApplied8)
                    Case 9 + hfPage.Value
                        FillDetails2(lblNo9, ChkBox9, lblName9, lblDate9, lblPeriod9, lblLeave9, lblApplied9)
                    Case 10 + hfPage.Value
                        FillDetails2(lblNo10, ChkBox10, lblName10, lblDate10, lblPeriod10, lblLeave10, lblApplied10)
                    Case 11 + hfPage.Value
                        FillDetails2(lblNo11, ChkBox11, lblName11, lblDate11, lblPeriod11, lblLeave11, lblApplied11)
                    Case 12 + hfPage.Value
                        FillDetails2(lblNo12, ChkBox12, lblName12, lblDate12, lblPeriod12, lblLeave12, lblApplied12)
                    Case 13 + hfPage.Value
                        FillDetails2(lblNo13, ChkBox13, lblName13, lblDate13, lblPeriod13, lblLeave13, lblApplied13)
                    Case 14 + hfPage.Value
                        FillDetails2(lblNo14, ChkBox14, lblName14, lblDate14, lblPeriod14, lblLeave14, lblApplied14)
                    Case 15 + hfPage.Value
                        FillDetails2(lblNo15, ChkBox15, lblName15, lblDate15, lblPeriod15, lblLeave15, lblApplied15)
                    Case 16 + hfPage.Value
                        FillDetails2(lblNo16, ChkBox16, lblName16, lblDate16, lblPeriod16, lblLeave16, lblApplied16)
                    Case 17 + hfPage.Value
                        FillDetails2(lblNo17, ChkBox17, lblName17, lblDate17, lblPeriod17, lblLeave17, lblApplied17)
                    Case 18 + hfPage.Value
                        FillDetails2(lblNo18, ChkBox18, lblName18, lblDate18, lblPeriod18, lblLeave18, lblApplied18)
                    Case 19 + hfPage.Value
                        FillDetails2(lblNo19, ChkBox19, lblName19, lblDate19, lblPeriod19, lblLeave19, lblApplied19)
                    Case 20 + hfPage.Value
                        FillDetails2(lblNo20, ChkBox20, lblName20, lblDate20, lblPeriod20, lblLeave20, lblApplied20)
                End Select
            End If
        End While

        mMyDataReader.Close()



    End Sub
    Private Sub SetGrid()

        Dim iLoop As Integer

        For iLoop = 1 To 20
            Select Case iLoop
                Case "1"
                    SetInvisibleUncheck(lblNo1, ChkBox1, lblName1, lblDate1, lblPeriod1, lblLeave1, lblApplied1)
                Case "2"
                    SetInvisibleUncheck(lblNo2, ChkBox2, lblName2, lblDate2, lblPeriod2, lblLeave2, lblApplied2)
                Case "3"
                    SetInvisibleUncheck(lblNo3, ChkBox3, lblName3, lblDate3, lblPeriod3, lblLeave3, lblApplied3)
                Case "4"
                    SetInvisibleUncheck(lblNo4, ChkBox4, lblName4, lblDate4, lblPeriod4, lblLeave4, lblApplied4)
                Case "5"
                    SetInvisibleUncheck(lblNo5, ChkBox5, lblName5, lblDate5, lblPeriod5, lblLeave5, lblApplied5)
                Case "6"
                    SetInvisibleUncheck(lblNo6, ChkBox6, lblName6, lblDate6, lblPeriod6, lblLeave6, lblApplied6)
                Case "7"
                    SetInvisibleUncheck(lblNo7, ChkBox7, lblName7, lblDate7, lblPeriod7, lblLeave7, lblApplied7)
                Case "8"
                    SetInvisibleUncheck(lblNo8, ChkBox8, lblName8, lblDate8, lblPeriod8, lblLeave8, lblApplied8)
                Case "9"
                    SetInvisibleUncheck(lblNo9, ChkBox9, lblName9, lblDate9, lblPeriod9, lblLeave9, lblApplied9)
                Case "10"
                    SetInvisibleUncheck(lblNo10, ChkBox10, lblName10, lblDate10, lblPeriod10, lblLeave10, lblApplied10)
                Case "11"
                    SetInvisibleUncheck(lblNo11, ChkBox11, lblName11, lblDate11, lblPeriod11, lblLeave11, lblApplied11)
                Case "12"
                    SetInvisibleUncheck(lblNo12, ChkBox12, lblName12, lblDate12, lblPeriod12, lblLeave12, lblApplied12)
                Case "13"
                    SetInvisibleUncheck(lblNo13, ChkBox13, lblName13, lblDate13, lblPeriod13, lblLeave13, lblApplied13)
                Case "14"
                    SetInvisibleUncheck(lblNo14, ChkBox14, lblName14, lblDate14, lblPeriod14, lblLeave14, lblApplied14)
                Case "15"
                    SetInvisibleUncheck(lblNo15, ChkBox15, lblName15, lblDate15, lblPeriod15, lblLeave15, lblApplied15)
                Case "16"
                    SetInvisibleUncheck(lblNo16, ChkBox16, lblName16, lblDate16, lblPeriod16, lblLeave16, lblApplied16)
                Case "17"
                    SetInvisibleUncheck(lblNo17, ChkBox17, lblName17, lblDate17, lblPeriod17, lblLeave17, lblApplied17)
                Case "18"
                    SetInvisibleUncheck(lblNo18, ChkBox18, lblName18, lblDate18, lblPeriod18, lblLeave18, lblApplied18)
                Case "19"
                    SetInvisibleUncheck(lblNo19, ChkBox19, lblName19, lblDate19, lblPeriod19, lblLeave19, lblApplied19)
                Case "20"
                    SetInvisibleUncheck(lblNo20, ChkBox20, lblName20, lblDate20, lblPeriod20, lblLeave20, lblApplied20)
            End Select
        Next


    End Sub
    Private Sub SetInvisibleUncheck(ByRef lblNo As Label, ByRef chkBox As CheckBox, ByRef lblName As Label, ByRef lblDate As Label, ByRef lblPeriod As Label, ByRef lblLeave As Label, ByRef lblApplied As Label)

        lblNo.Visible = False
        chkBox.Visible = False
        lblName.Visible = False
        lblDate.Visible = False
        lblPeriod.Visible = False
        lblLeave.Visible = False
        lblApplied.Visible = False


        chkBox.Checked = False

    End Sub
    Private Sub FillDetails2(ByRef lblNo As Label, ByRef chkBox As CheckBox, ByRef lblName As Label, ByRef lblDate As Label, ByRef lblPeriod As Label, ByRef lblLeave As Label, ByRef lblApplied As Label)

        Dim strToolTip As String

        strToolTip = "Applied on: " & mstrApplied & vbCrLf & "Reason: " & mstrReason & vbCrLf & vbCrLf & mstrRemark

        lblNo.Visible = True
        chkBox.Visible = True
        lblName.Visible = True
        lblDate.Visible = True
        lblPeriod.Visible = True
        lblLeave.Visible = True
        lblApplied.Visible = True

        lblNo.Text = miNo
        lblName.Text = mstrName
        lblDate.Text = mstrDate ' & ", " & mstrDay

        lblPeriod.Text = mstrPeriod
        lblLeave.Text = mstrLeave
        lblApplied.Text = mstrDestination
        'lblApplied.Text = mstrApplied


        lblNo.ToolTip = strToolTip
        lblName.ToolTip = strToolTip
        lblDate.ToolTip = strToolTip
        lblPeriod.ToolTip = strToolTip
        lblLeave.ToolTip = strToolTip
        lblApplied.ToolTip = strToolTip

        If Not IsDate("13" & "/" & "01" & "/" & "2000") Then
            mstrDate = mstrDate.Substring(3, 2) & "/" & mstrDate.Substring(0, 2) & "/" & mstrDate.Substring(6, 4)
        End If

        If cboStatus.SelectedValue = "P" Or cboStatus.SelectedValue = "R" Or mstrDate >= Now() Then
            If lblStatus.SelectedValue <> "N" Then
                chkBox.Enabled = True
            Else
                chkBox.Enabled = False
            End If
        Else
            chkBox.Enabled = False
        End If

        'hfPageLast.Value = lblNo.Text

    End Sub

    Private Sub LeaveApproval_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload

        mMyConnection.Close()

    End Sub

    Protected Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        If lblStatus.SelectedValue <> "A" And lblStatus.SelectedValue <> "C" Then
            If Trim(txtRemark.Text) = "" Then
                txtRemark.BackColor = Drawing.Color.LightSalmon
                Exit Sub
            End If
        End If

        mstrEmp = ""

        Dim iLoop As Integer

        For iLoop = 1 To 20
            Select Case iLoop
                Case "1"
                    Approval(ChkBox1, lblName1, lblDate1, lblPeriod1, lblLeave1)
                Case "2"
                    Approval(ChkBox2, lblName2, lblDate2, lblPeriod2, lblLeave2)
                Case "3"
                    Approval(ChkBox3, lblName3, lblDate3, lblPeriod3, lblLeave3)
                Case "4"
                    Approval(ChkBox4, lblName4, lblDate4, lblPeriod4, lblLeave4)
                Case "5"
                    Approval(ChkBox5, lblName5, lblDate5, lblPeriod5, lblLeave5)
                Case "6"
                    Approval(ChkBox6, lblName6, lblDate6, lblPeriod6, lblLeave6)
                Case "7"
                    Approval(ChkBox7, lblName7, lblDate7, lblPeriod7, lblLeave7)
                Case "8"
                    Approval(ChkBox8, lblName8, lblDate8, lblPeriod8, lblLeave8)
                Case "9"
                    Approval(ChkBox9, lblName9, lblDate9, lblPeriod9, lblLeave9)
                Case "10"
                    Approval(ChkBox10, lblName10, lblDate10, lblPeriod10, lblLeave10)
                Case "11"
                    Approval(ChkBox11, lblName11, lblDate11, lblPeriod11, lblLeave11)
                Case "12"
                    Approval(ChkBox12, lblName12, lblDate12, lblPeriod12, lblLeave12)
                Case "13"
                    Approval(ChkBox13, lblName13, lblDate13, lblPeriod13, lblLeave13)
                Case "14"
                    Approval(ChkBox14, lblName14, lblDate14, lblPeriod14, lblLeave14)
                Case "15"
                    Approval(ChkBox15, lblName15, lblDate15, lblPeriod15, lblLeave15)
                Case "16"
                    Approval(ChkBox16, lblName16, lblDate16, lblPeriod16, lblLeave16)
                Case "17"
                    Approval(ChkBox17, lblName17, lblDate17, lblPeriod17, lblLeave17)
                Case "18"
                    Approval(ChkBox18, lblName18, lblDate18, lblPeriod18, lblLeave18)
                Case "19"
                    Approval(ChkBox19, lblName19, lblDate19, lblPeriod19, lblLeave19)
                Case "20"
                    Approval(ChkBox20, lblName20, lblDate20, lblPeriod20, lblLeave20)
            End Select
        Next

        If mstrEmp <> "" Then
            SendEmail(mstrEmp)

            'If lblStatus.SelectedValue = "R" Then
            '    mMyCommand.CommandText = "sp_ls_web_InsDel_LeaveApplication '" & Session("EmpID") & "','" & "A" & "','" & lblDate.Text & "','" & _
            '               lblLeave.Text & "','" & lblPeriod.Text & "','" & "" & "','" & "" & "','" & Session("EmpID") & "','" & Now & _
            '               "','" & "" & "','" & "CANCEL" & "'"
            '    mMyDataReader = mMyCommand.ExecuteReader
            '    mMyDataReader.Close()
            'End If
        End If

    End Sub

    Private Sub Approval(ByRef chkBox As CheckBox, ByRef lblName As Label, ByRef lblDate As Label, ByRef lblPeriod As Label, ByRef lblLeave As Label)

        Dim ssql As String

        If chkBox.Checked Then
            If optPersonal.Checked Then
                ssql = "Exec sp_ls_web_InsDel_LeaveApplication '" & Session("EmpID") & "','" & "A" & "','" & lblDate.Text & "','" & _
                                    lblLeave.Text & "','" & lblPeriod.Text & "','" & "" & "','" & "" & "','" & Session("EmpID") & "','" & Now & _
                                    "','" & "" & "','" & "CANCEL" & "'"
                mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)


            Else

                'If lblStatus.SelectedValue = "R" And Trim(txtRemark.Text) = "" Then
                '    lblRequired.Visible = True
                '    Exit Sub
                'Else
                '    lblRequired.Visible = False
                'End If

                ssql = " Exec sp_ls_InsUpdDel_LeaveApproval '" & lblName.Text & "','" & lblDate.Text & "','" & lblLeave.Text & "','" & _
                                        lblPeriod.Text & "','" & lblStatus.SelectedValue & "','" & Replace(txtRemark.Text, "'", "''") & "','" & Session("EmpID") & "','" & "ADD" & "'"
                mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

                If mstrEmp <> lblName.Text Then
                    If mstrEmp <> "" Then
                        SendEmail(mstrEmp)

                        'If lblStatus.SelectedValue = "R" Then
                        '    mMyCommand.CommandText = "sp_ls_web_InsDel_LeaveApplication '" & Session("EmpID") & "','" & "A" & "','" & lblDate.Text & "','" & _
                        '               lblLeave.Text & "','" & lblPeriod.Text & "','" & "" & "','" & "" & "','" & Session("EmpID") & "','" & Now & _
                        '               "','" & "" & "','" & "CANCEL" & "'"
                        '    mMyDataReader = mMyCommand.ExecuteReader
                        '    mMyDataReader.Close()
                        'End If
                    End If
                    mstrEmp = lblName.Text
                End If
            End If
        End If

        'My.Computer.Network.DownloadFile("http://mail.kajima.com.my/kmiso/eleave.zip", "C:\SNP\eleave.txt", "", "", False, 1000, True)
        ''My.Computer.Network.UploadFile("C:\SNP\E-Leave.txt", "ftp://mail.kajima.com.my/home/.sites/28/site1/web/kmiso/E-Leave.txt", "admin", "", False, 1000)

    End Sub
    Private Sub SendEmail(ByVal strEmpName As String)

        Dim strCurrentDate As String = ""

        mMyCommand.CommandText = "select currentdate =convert(varchar, getdate(), 103) + ' ' + ltrim(substring(convert(varchar, getdate(), 100), 13, 5)) + ' ' + substring(convert(varchar, getdate(), 100), 18, 2)"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            strCurrentDate = mMyDataReader("currentdate")
        End If
        mMyDataReader.Close()

        Dim mMyConnection2 As New System.Data.SqlClient.SqlConnection
        Dim mMyCommand2 As New System.Data.SqlClient.SqlCommand
        Dim mMyDataReader2 As System.Data.SqlClient.SqlDataReader


        mMyConnection2.ConnectionString = mySQL.GetConnectionString()
        mMyConnection2.Open()
        mMyCommand2.Connection = mMyConnection2

        mSmtpMail.Port = mySQL.GetConnectionStringDetails(clsSQL.SQLInfo._MailPort)
        mSmtpMail.Host = mySQL.GetConnectionStringDetails(clsSQL.SQLInfo._MailServer)
        mSmtpMail.Credentials = MyKajima.sysNetworkCredential
        mSmtpMsg.IsBodyHtml = True

        Dim strStatus As String = ""
        Dim strSubject As String = ""
        Dim strHeader As String = ""
        Dim strColSupervisor As String = ""
        Dim strColDate As String = ""
        Dim strEmpID As String = ""
        Dim strUserName As String = ""

        mMyCommand.CommandText = "select empname from is_empmaster where empid = '" & Session("EmpID") & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            strUserName = mMyDataReader("empname")
        End If
        mMyDataReader.Close()

        If lblStatus.SelectedValue = "P" Then
            strSubject = "Changed To Pending"
            strStatus = "changed to pending"
            strHeader = "Schedule Application - Changed To Pending"
            strColSupervisor = "Changed By"
            strColDate = "Changed On"
        ElseIf lblStatus.SelectedValue = "A" Then
            strSubject = "Approved"
            strStatus = "approved"
            strHeader = "Schedule Application - Approved"
            strColSupervisor = "Approved By"
            strColDate = "Approved On"
        ElseIf lblStatus.SelectedValue = "R" Then
            strSubject = "Rejected"
            strStatus = "rejected"
            strHeader = "Schedule Application - Rejected"
            strColSupervisor = "Rejected By"
            strColDate = "Rejected On"
        End If

        mMyCommand.CommandText = "select top 1 empid from is_empmaster where empname = '" & strEmpName & "' order by DateJoin desc"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            strEmpID = mMyDataReader("empid")
        End If
        mMyDataReader.Close()

        mMyCommand.CommandText = "sp_ls_email_LeaveApplication '" & "" & "','" & strEmpID & "','" & "" & "'," & "0" & "," & "0" & ",'" & "SEND_EMAIL" & "'"
        mMyDataReader = mMyCommand.ExecuteReader


        'mMyCommand.CommandText = "sp_web_ls_Sel_StaffLeave '" & strEmpID & "'," & cboYear.SelectedValue & "," & _
        '                       cboMonth.SelectedValue & ",'" & "" & "','" & "" & "','" & "SEND_EMAIL" & "'"
        'mMyDataReader = mMyCommand.ExecuteReader

        mSmtpMsg.From = New System.Net.Mail.MailAddress(mySQL.GetConnectionStringDetails(clsSQL.SQLInfo._MailUser), "eSchedule")

        mSmtpMsg.To.Clear()
        mSmtpMsg.CC.Clear()
        mSmtpMsg.Bcc.Clear()

        While mMyDataReader.Read()
            If mMyDataReader("type").ToString.ToUpper = "FROM" Then
                mSmtpMsg.To.Add(New System.Net.Mail.MailAddress(mMyDataReader("email"), mMyDataReader("empname")))
            ElseIf mMyDataReader("type").ToString.ToUpper = "TO" Then
                mSmtpMsg.CC.Add(New System.Net.Mail.MailAddress(mMyDataReader("email"), mMyDataReader("empname")))
            Else
                mSmtpMsg.CC.Add(New System.Net.Mail.MailAddress(mMyDataReader("email"), mMyDataReader("empname")))
            End If
        End While
        mMyDataReader.Close()

        mSmtpMsg.Bcc.Add(New System.Net.Mail.MailAddress(mySQL.GetConnectionStringDetails(clsSQL.SQLInfo._MailUser), "eSchedule"))

        mSmtpMsg.Body = "<html>" & _
               "<body>" & _
               "<table>" & _
               "<tr>" & _
               "<td>" & "Dear Sir/Madam," & "</td>" & _
               "</tr>" & _
               "<tr>" & _
               "<td>&nbsp;</td>" & _
               "</tr>" & _
               "<tr>" & _
               "<td>" & "Please be informed that Schedule Application below for <u>" & strEmpName & "</u> has been " & strStatus & " by <u>" & strUserName & "</u> on <u>" & strCurrentDate & "</u>.</td>" & _
               "</tr>" & _
               "</table>" & _
               "<br/>" & _
               "<table  border=""1""  cellspacing=""0"" cellpadding=""5"">" & _
               "<tr>" & _
               "<td colspan=""6"" align=""center"" style=""background-color: darkcyan; color:White; font-weight: bold"">" & strHeader & "</td>" & _
               "</tr>" & _
               "<tr>" & _
               "<td align=""center"" style=""background-color: darkcyan; width:30px; color:White; font-weight: bold"">No.</td>" & _
               "<td align=""center"" style=""background-color: darkcyan; width:175px; color:White; font-weight: bold"">From Date</td>" & _
               "<td align=""center"" style=""background-color: darkcyan; width:175px; color:White; font-weight: bold"">To Date</td>" & _
               "<td align=""center"" style=""background-color: darkcyan; width:100px; color:White; font-weight: bold"">Schedule</td>" & _
               "<td align=""center"" style=""background-color: darkcyan; width:250px; color:White; font-weight: bold"">Destination</td>" & _
               "<td align=""center"" style=""background-color: darkcyan; width:500px; color:White; font-weight: bold"">Reason / Purpose</td>" & _
               "</tr>"
        '"<td align=""center"" style=""background-color: darkcyan; width:280px; color:White; font-weight: bold"">" & strColSupervisor & "</td>" & _
        '      "<td align=""center"" style=""background-color: darkcyan; width:150px; color:White; font-weight: bold"">" & strColDate & "</td>" & _

        Dim strHtmlBody As String = ""

        mMyCommand.CommandText = "sp_ls_email_LeaveApplication '" & Session("EmpID") & "','" & strEmpID & "','" & lblStatus.SelectedValue & "'," & _
                                  cboMonth.SelectedValue & "," & cboYear.SelectedValue & ",'" & lblStatus.SelectedValue & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.HasRows Then
            While mMyDataReader.Read()
                If Val(mMyDataReader("no")) Mod 2 = 0 Then

                    strHtmlBody = strHtmlBody & _
                            "<tr style=""font-size:smaller"">" & _
                            "<td align=""center"" style=""background-color: lavender"">" & mMyDataReader("no") & "</td>" & _
                            "<td align=""center"" style=""background-color: lavender"">" & mMyDataReader("startdate") & "</td>" & _
                            "<td align=""center"" style=""background-color: lavender"">" & mMyDataReader("enddate") & "</td>" & _
                            "<td align=""center"" style=""background-color: lavender"">" & mMyDataReader("leave") & "</td>" & _
                            "<td align=""center"" style=""background-color: lavender"">" & mMyDataReader("destination") & "</td>" & _
                            "<td style=""background-color: lavender"">" & mMyDataReader("reason") & "</td>" & _
                            "</tr>" & _
                            "<tr>" & _
                            "<td colspan=""6"">&nbsp;</td>" & _
                            "</tr>"


                Else

                    strHtmlBody = strHtmlBody & _
                           "<tr style=""font-size:smaller"">" & _
                            "<td align=""center"" style=""background-color: honeydew"">" & mMyDataReader("no") & "</td>" & _
                            "<td align=""center"" style=""background-color: honeydew"">" & mMyDataReader("startdate") & "</td>" & _
                            "<td align=""center"" style=""background-color: honeydew"">" & mMyDataReader("enddate") & "</td>" & _
                            "<td align=""center"" style=""background-color: honeydew"">" & mMyDataReader("leave") & "</td>" & _
                            "<td align=""center"" style=""background-color: honeydew"">" & mMyDataReader("destination") & "</td>" & "</td>" & _
                            "<td style=""background-color: honeydew"">" & mMyDataReader("reason") & "</td>" & _
                            "</tr>" & _
                            "<tr>" & _
                            "<td colspan=""6"">&nbsp;</td>" & _
                            "</tr>"
                    '"<td align=""center"" style=""background-color: honeydew"">" & mMyDataReader("approvedby") & _
                    '        "<td style=""background-color: honeydew"">" & mMyDataReader("dateapproved") & _

                End If

                'mMyCommand2.CommandText = "sp_ls_InsUpdDel_LeaveApproval '" & mMyDataReader("empname") & "','" & mMyDataReader("date") & "','" & mMyDataReader("leave") & "','" & _
                '                           mMyDataReader("period") & "','" & lblStatus.SelectedValue & "','" & "" & "','" & Session("EmpID") & "','" & "UPD" & "'"
                'mMyDataReader2 = mMyCommand2.ExecuteReader
                'mMyDataReader2.Close()


            End While


            mSmtpMsg.Body = mSmtpMsg.Body & strHtmlBody
            mSmtpMsg.Body = mSmtpMsg.Body & _
                           "</table>" & _
                           "<table>" & _
                           "<tr>" & _
                           "<td>&nbsp;</td>" & _
                           "</tr>" & _
                           "<tr>" & _
                           "<td>" & "Thank you." & "</td>" & _
                           "</tr>" & _
                           "<tr>" & _
                           "<td>&nbsp;</td>" & _
                           "</tr>" & _
                           "<tr>" & _
                           "<td>" & "eSchedule" & "</td>" & _
                           "</tr>" & _
                           "<tr>" & _
                           "<td>&nbsp;</td>" & _
                           "</tr>" & _
                           "<tr>" & _
                           "<td style=""font-size:smaller"">" & "This is computer generated email, reply is not required." & "</td>" & _
                           "</tr>" & _
                           "</table>" & _
                           "</body>" & _
                           "</html>"
            '"<tr>" & _
            '              "<td>" & "On behalf of " & strEmpName & "</td>" & _
            '              "</tr>" & _
            mSmtpMsg.Subject = "Schedule Application for " & strEmpName & " - " & strSubject

            'mSmtpMsg.Attachments.Add(New System.Net.Mail.Attachment("D:\Leave Application.txt"))
            mSmtpMail.Send(mSmtpMsg)
        End If

        mMyDataReader.Close()
        mMyConnection2.Close()

    End Sub

    Private Sub lnkbtnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnNext.Click

        hfPage.Value = hfPage.Value + 20

        If Val(hfPage.Value) > Val(hfMaxNo.Value) Then hfPage.Value = hfPage.Value - 20
        'lnkbtnNext.ForeColor = Drawing.Color.Red



    End Sub

    Private Sub lnkbtnPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnPrevious.Click

        hfPage.Value = hfPage.Value - 20

        If Val(hfPage.Value) < 0 Then hfPage.Value = 0
        'lnkbtnPrevious.ForeColor = Drawing.Color.Red

    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboStatus.SelectedIndexChanged

        hfPage.Value = 0

        If optSubordinate.Checked Then
            If cboStatus.SelectedValue = "P" Then
                lblStatus.SelectedIndex = 1
            ElseIf cboStatus.SelectedValue = "A" Or cboStatus.SelectedValue = "R" Then
                lblStatus.SelectedIndex = 0
            End If
        End If

    End Sub


    Protected Sub lnkbtnLeaveApplication_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkbtnLeaveApplication.Click

        Response.Redirect("scheduleapplication.aspx", True)

    End Sub

    Protected Sub lblLeaveApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkbtnLeaveApproval.Click

        Response.Redirect("scheduleapproval.aspx", True)

    End Sub

    Private Sub optSubordinate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optSubordinate.CheckedChanged

        hfPage.Value = 0

        If optSubordinate.Checked Then
            If cboStatus.SelectedValue = "P" Then
                lblStatus.SelectedIndex = 1
            ElseIf cboStatus.SelectedValue = "A" Or cboStatus.SelectedValue = "R" Then
                lblStatus.SelectedIndex = 0
            End If
        End If

    End Sub


    Private Sub lnkbtnLeaveSchedule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLeaveSchedule.Click

        Response.Redirect("viewschedule.aspx", True)

    End Sub

    Private Sub cboMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMonth.SelectedIndexChanged

        hfPage.Value = 0

    End Sub

    Private Sub cboYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboYear.SelectedIndexChanged

        hfPage.Value = 0

    End Sub

    Private Sub optPersonal_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optPersonal.CheckedChanged

        hfPage.Value = 0

    End Sub

    Private Sub cboSubordinate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSubordinate.SelectedIndexChanged

        hfPage.Value = 0

    End Sub
    Private Sub lnkChangePwd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkChangePwd.Click

        Response.Redirect("changepwd.aspx", True)

    End Sub

    Private Sub lnkLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogout.Click

        Session("EmpID") = ""
        Response.Redirect("../Global/SessionTimeOut.aspx")

    End Sub

    Private Sub lnkbtnLeaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLeaveReport.Click

        Response.Redirect("schedulereport.aspx", True)

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