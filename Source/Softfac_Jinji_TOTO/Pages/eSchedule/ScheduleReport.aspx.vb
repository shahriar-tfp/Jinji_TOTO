Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Extensibility

Partial Class PAGES_ESCHEDULE_SCHEDULEREPORT
    Inherits System.Web.UI.Page

    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting, MyKajima As New clsKajimaWeb
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Dim mMyConnection As New System.Data.SqlClient.SqlConnection
    Dim mMyCommand As New System.Data.SqlClient.SqlCommand
    Dim mMyDataReader As System.Data.SqlClient.SqlDataReader
    Dim viewprinter As New GrapeCity.ActiveReports.Extensibility.Printing.Printer()
    Dim Param1, Param2, Param3, Param4, Param5, Param6, Param7, ssql As String
    Dim rpx As New SectionReport
    Dim ds As New DataSet()
    Dim xtr As New System.Xml.XmlTextReader(Server.MapPath("~") & "\_Templates\Ar\ls_leaveDetails.rpx")

    'Dim objReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
    'Dim myTable As CrystalDecisions.CrystalReports.Engine.Table
    'Dim myTableLogOnInfos As CrystalDecisions.Shared.TableLogOnInfos '= frmViewReport.rptViewer.LogOnInfo()
    'Dim gReportParam() As CrystalDecisions.Shared.ParameterDiscreteValue

    Private Sub ScheduleReport_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        If Session("EmpID") = "" Then
            Response.Redirect("../Global/SessionTimeOut.aspx")
        Else
            Session("PreviousPage") = Request.RawUrl
            lblUser.Text = Session("username")
        End If

        mMyConnection.ConnectionString = mySQL.GetConnectionString()
        mMyConnection.Open()
        mMyCommand.Connection = mMyConnection


        If Not IsPostBack Then
            Dim lstItem As New ListItem

            lnkbtnLeaveApplication.ForeColor = Nothing
            lnkbtnLeaveSchedule.ForeColor = Nothing
            lnkbtnLeaveApproval.ForeColor = Nothing
            lnkbtnLeaveReport.ForeColor = System.Drawing.Color.Red

            txtFromDate.Text = "01/01/" + Strings.Mid(DateNow, 7, 4)
            txtToDate.Text = "31/12/" + Strings.Mid(DateNow, 7, 4)

            cboSchedule.Items.Add("")
            cboSchedule.Items(0).Enabled = False

            lstItem = New ListItem
            lstItem.Text = "-- All --"
            lstItem.Value = "ALL"
            cboSchedule.Items.Add(lstItem)

            cboSchedule.SelectedValue = cboSchedule.Items(1).Value

            mMyCommand.CommandText = "sp_sa_Sel_LeaveType '" & "" & "','" & "Web_View" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            While mMyDataReader.Read()
                lstItem = New ListItem
                lstItem.Text = mMyDataReader("LeaveID") & " | " & mMyDataReader("LeaveDesc")
                lstItem.Value = mMyDataReader("LeaveID")
                cboSchedule.Items.Add(lstItem)
            End While
            mMyDataReader.Close()

            ssql = "Exec sp_sa_GetSecurityRole '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','CHK'"
            ds = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    'If ds.Tables(0).Rows(0).Item(0).ToString.ToUpper = "ADM" Then
                    ddlType.Visible = True
                    lblType.Visible = True
                    'Else
                    'ddlType.Visible = False
                    'lblType.Visible = False
                    ddlType.SelectedValue = "A"
                    'End If
                End If
            End If
            RetrieveReport()
        End If

    End Sub

    Private Function DateNow() As String

        mMyCommand.CommandText = "select convert(varchar, getdate(), 103)"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            DateNow = mMyDataReader(0)
        End If
        mMyDataReader.Close()

    End Function

    Private Sub GetSubordindate()

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


        mMyCommand.CommandText = "sp_ls_Sel_LeaveView '" & Session("EmpID") & "'"
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


    Private Sub DetailsReport()

        If IsDate(txtFromDate.Text) Then
            If IsDate(txtToDate.Text) Then

                rpx = New SectionReport
                ds = New DataSet()
                xtr = New System.Xml.XmlTextReader(Server.MapPath("~") & "\_Templates\Ar\ls_leaveDetails.rpx")

                If optPersonal.Checked Then
                    Param1 = Session("EmpID")
                    Param7 = "web"
                Else
                    If cboSubordinate.SelectedValue = "A" Then
                        Param1 = Session("EmpID")
                        Param7 = "web2"
                    Else
                        Param1 = cboSubordinate.SelectedValue
                        Param7 = "web"
                    End If
                End If


                Param3 = "ALL"
                Param4 = cboSchedule.SelectedValue
                Param5 = txtFromDate.Text
                Param6 = txtToDate.Text
                Param2 = ddlType.SelectedValue

                ssql = "Exec sp_rpt_ls_LeaveApplication '" & Param1 & "','" & Param2 & "','" & Param3 & "','" & Param4 & "','" & Param5 & "','" & Param6 & "','" & Param7 & "'"

                ds = mySQL.ExecuteSQL(ssql)

                rpx.LoadLayout(xtr)
                rpx.DataSource = ds.Tables(0)
                Session("rptGenericReport") = Nothing
                Session("rptGenericReport") = rpx
                'Dim myTargetURL As String = "../../ReportViewer.aspx"
                'Response.Redirect(myTargetURL)
                viewprinter.PrinterName = ""
                WebViewer1.ClearCachedReport()
                WebViewer1.ViewerType = GrapeCity.ActiveReports.Web.ViewerType.FlashViewer
                WebViewer1.Report = CType(rpx, SectionReport)

                xtr.Close()
            End If
        End If

    End Sub
    Private Sub SummaryReport()

        If IsDate(txtDate.Text) Then
            rpx = New SectionReport
            ds = New DataSet()
            xtr = New System.Xml.XmlTextReader(Server.MapPath("~") & "\_Templates\Ar\ls_leaveSummary.rpx")

            If optPersonal.Checked Then
                Param1 = Session("EmpID")
                Param4 = "web"
            Else
                If cboSubordinate.SelectedValue = "A" Then
                    Param1 = Session("EmpID")
                    Param4 = "web2"
                Else
                    Param1 = cboSubordinate.SelectedValue
                    Param4 = "web"
                End If
            End If

            Param2 = 0
            Param3 = txtDate.Text

            ssql = "Exec sp_rpt_ls_EmpEntitlement '" & Param1 & "','" & Param2 & "','" & Param3 & "','" & Param4 & "'"

            ds = mySQL.ExecuteSQL(ssql)

            rpx.LoadLayout(xtr)
            rpx.DataSource = ds.Tables(0)
            Session("rptGenericReport") = Nothing
            Session("rptGenericReport") = rpx
            viewprinter.PrinterName = ""
            WebViewer1.ClearCachedReport()
            WebViewer1.ViewerType = GrapeCity.ActiveReports.Web.ViewerType.FlashViewer
            WebViewer1.Report = CType(Session("rptGenericReport"), SectionReport)

            xtr.Close()
        End If


    End Sub
    Private Function IsDate(ByVal strParam As String) As Boolean

        IsDate = False

        Dim strDay, strMonth, strYear, strDate As String

        strDay = Strings.Mid(strParam, 1, 2)
        strMonth = Strings.Mid(strParam, 4, 2)
        strYear = Strings.Mid(strParam, 7, 4)
        strDate = strMonth + "/" + strDay + "/" + strYear

        'Response.Write(strParam)
        'strDate = "01/01/2009"

        mMyCommand.CommandText = "select isdate('" + strDate + "')"
        mMyDataReader = mMyCommand.ExecuteReader



        If mMyDataReader.Read() Then
            If mMyDataReader(0) = "0" Then
                mMyDataReader.Close()
                lblError.Text = "Invalid Date."
                Exit Function
            End If
        End If
        mMyDataReader.Close()

        lblError.Text = ""
        IsDate = True


    End Function

    Protected Sub optPersonal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPersonal.CheckedChanged

        cboSubordinate.Items.Clear()
        cboSubordinate.Enabled = False
        RetrieveReport()

    End Sub

    Protected Sub optSubordinate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSubordinate.CheckedChanged

        GetSubordindate()
        cboSubordinate.Enabled = True
        RetrieveReport()

    End Sub

    Private Sub RetrieveReport()

        If optDetail.Checked Then
            DetailsReport()
        Else
            SummaryReport()
        End If

    End Sub

    Protected Sub optDetail_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDetail.CheckedChanged

        If optDetail.Checked Then
            txtFromDate.Enabled = True
            txtToDate.Enabled = True

            txtFromDate.Text = "01/01/" + Strings.Mid(DateNow, 7, 4)
            txtToDate.Text = "31/12/" + Strings.Mid(DateNow, 7, 4)



            'dtpFromDate.txtDate.Text = "01/" + Now.ToShortDateString.Substring(3, 2) + "/" + Now.ToShortDateString.Substring(6, 4)
            'dtpToDate.txtDate.Text = Now.AddMonths(1).AddDays(-Now.Day).ToShortDateString.Substring(0, 2) + "/" + Now.ToShortDateString.Substring(3, 2) + "/" + Now.ToShortDateString.Substring(6, 4)



            txtDate.Enabled = False
            txtDate.Text = ""

            cboSchedule.Items(0).Enabled = False
            cboSchedule.SelectedValue = cboSchedule.Items(1).Value
            cboSchedule.Enabled = True

            RetrieveReport()
        End If

    End Sub

    Protected Sub optSummary_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSummary.CheckedChanged

        If optSummary.Checked Then
            txtFromDate.Text = ""
            txtToDate.Text = ""


            txtFromDate.Enabled = False
            txtToDate.Enabled = False



            txtDate.Text = "31/12/" + Strings.Mid(DateNow, 7, 4)
            txtDate.Enabled = True

            cboSchedule.Items(0).Enabled = True
            cboSchedule.SelectedIndex = 0
            cboSchedule.Enabled = False

            RetrieveReport()
        End If


    End Sub

    Protected Sub cboSubordinate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSubordinate.SelectedIndexChanged

        RetrieveReport()

    End Sub

    Protected Sub cboSchedule_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSchedule.SelectedIndexChanged

        RetrieveReport()

    End Sub






    Protected Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click

        RetrieveReport()

    End Sub

    Private Sub lnkbtnLeaveApplication_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLeaveApplication.Click

        Response.Redirect("scheduleapplication.aspx", True)

    End Sub

    Private Sub lnkbtnLeaveApproval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLeaveApproval.Click

        Response.Redirect("scheduleapproval.aspx", True)

    End Sub

    Private Sub lnkbtnLeaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLeaveReport.Click

        Response.Redirect("schedulereport.aspx", True)

    End Sub

    Private Sub lnkbtnLeaveSchedule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLeaveSchedule.Click

        Response.Redirect("viewschedule.aspx", True)

    End Sub

    Private Sub lnkChangePwd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkChangePwd.Click

        Response.Redirect("changepwd.aspx", True)

    End Sub

    Private Sub lnkLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogout.Click

        Session("EmpID") = ""
        Response.Redirect("../Global/SessionTimeOut.aspx")

    End Sub

    Private Sub ScheduleReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

    Protected Sub ddlType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlType.SelectedIndexChanged
        RetrieveReport()
    End Sub
End Class