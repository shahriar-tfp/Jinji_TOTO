Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Extensibility

Partial Class PAGES_EAPPRAISAL_APPRAISALREPORT
    Inherits System.Web.UI.Page

    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting, MyKajima As New clsKajimaWeb
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Dim mMyConnection As New System.Data.SqlClient.SqlConnection
    Dim mMyCommand As New System.Data.SqlClient.SqlCommand
    Dim mMyDataReader As System.Data.SqlClient.SqlDataReader

    Dim mstrEmpID As String
    Dim mstrCurrentPeriod As String

    Dim viewprinter As New GrapeCity.ActiveReports.Extensibility.Printing.Printer()
    Dim Param1, Param2, Param3, Param4, Param5, Param6, Param7, ssql As String
    Dim rpx As New SectionReport
    Dim ds As New DataSet()
    Dim xtr As New System.Xml.XmlTextReader(Server.MapPath("~") & "\_Templates\Ar\EmployeeApprasalForm.rpx")

    Private Sub AppraisalReport_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        mMyConnection.ConnectionString = mySQL.GetConnectionString()
        mMyConnection.Open()
        mMyCommand.Connection = mMyConnection

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim lstItem As New ListItem



        If Session("EmpID") = "" Then
            Response.Redirect("../Global/SessionTimeOut.aspx")
        Else
            Session("PreviousPage") = Request.RawUrl

            lblUser.Text = Session("username")

            'If optSubordinate.Checked Then
            '    mstrEmpID = cboSubordinate.SelectedValue
            'Else
            mstrEmpID = Session("EmpID")
            'End If
        End If

        mMyCommand.CommandText = "sp_ap_sel_AppraisalPeriod '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & "CurrentPeriod" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            mstrCurrentPeriod = mMyDataReader("period")
        End If
        mMyDataReader.Close()

        If Not IsPostBack Then
            mMyCommand.CommandText = "sp_ap_sel_AppraisalPeriod '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & "AllPeriod" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            While mMyDataReader.Read()
                lstItem = New ListItem

                lstItem.Text = mMyDataReader("desc")
                lstItem.Value = mMyDataReader("period")
                cboAppraisal.Items.Add(lstItem)
            End While
            mMyDataReader.Close()

            cboAppraisal.SelectedValue = mstrCurrentPeriod

            mMyCommand.CommandText = "sp_ap_Sel_AppraisalGroup '" & cboAppraisal.SelectedValue & "','" & Session("EmpID") & "','" & mstrEmpID & "','" & _
                                 "Level" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            If mMyDataReader.Read Then
                hfLevel.Value = mMyDataReader("level")
            End If
            mMyDataReader.Close()

            GenerateReport()
        End If

    End Sub

    Private Sub GetSubordinate()

        Dim lstItem As New ListItem

        cboSubordinate.Items.Clear()

        mMyCommand.CommandText = "sp_ap_Sel_AppraisalGroup '" & cboAppraisal.SelectedValue & "','" & Session("EmpID") & "','" & mstrEmpID & "','" & _
                                 "Subordinate" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        While mMyDataReader.Read()
            lstItem = New ListItem

            lstItem.Text = mMyDataReader("empname")
            lstItem.Value = mMyDataReader("empid")
            cboSubordinate.Items.Add(lstItem)
        End While
        mMyDataReader.Close()

        If cboSubordinate.Items.Count > 0 Then
            lstItem = New ListItem
            lstItem.Text = "-- ALL --"
            lstItem.Value = "A"
            cboSubordinate.Items.Insert(0, lstItem)
        End If

        cboSubordinate.Enabled = True

    End Sub

    Private Sub optSubordinate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optSubordinate.CheckedChanged

        GetSubordinate()
        GenerateReport()

    End Sub

    Private Sub lnkbtnSkills_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnSkills.Click

        Response.Redirect("StaffSkills.aspx", True)

    End Sub

    Protected Sub lnkbtnComment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnComment.Click
        Response.Redirect("staffComment.aspx", True)
    End Sub

    Private Sub lnkbtnTarget_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnTarget.Click

        Response.Redirect("StaffTarget.aspx", True)

    End Sub

    Protected Sub lnkManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkManual.Click

        mMyCommand.CommandText = "sp_ap_Sel_AppraisalGroup '" & cboAppraisal.SelectedValue & "','" & Session("EmpID") & "','" & mstrEmpID & "','" & _
                                   "Manual" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            If mMyDataReader("level") = 0 Then
                Response.Redirect("Appraisee Manual.pdf", True)
            ElseIf mMyDataReader("level") = 1 Then
                Response.Redirect("1st Appraiser Manual.pdf", True)
            ElseIf mMyDataReader("level") = 2 Then
                Response.Redirect("2nd Appraiser Manual.pdf", True)
            End If
        Else
            Response.Redirect("Appraisee Manual.pdf", True)
        End If
        mMyDataReader.Close()

    End Sub

    Private Sub cboAppraisal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAppraisal.SelectedIndexChanged

        If optSubordinate.Checked Then
            GetSubordinate()
        End If

        GenerateReport()

    End Sub

    Private Sub optPersonal_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optPersonal.CheckedChanged

        cboSubordinate.Items.Clear()
        cboSubordinate.Enabled = False

        GenerateReport()

    End Sub

    Protected Sub optTarget_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTarget.CheckedChanged

        GenerateReport()

    End Sub

    Private Sub GenerateReport()

        Dim strEmpID, strType As String
        Dim spid As String = mySQL.GetSPID()
        rpx = New SectionReport
        ds = New DataSet()

        If optTarget.Checked Or optComment.Checked Then
            xtr = New System.Xml.XmlTextReader(Server.MapPath("~") & "\_Templates\Ar\EmployeeApprasalForm.rpx")

            If optComment.Checked Then
                xtr = New System.Xml.XmlTextReader(Server.MapPath("~") & "\_Templates\Ar\ApprasalCommentSheet.rpx")
            End If
            If optPersonal.Checked Then
                strEmpID = Session("EmpID")
                strType = "Target"
                If optComment.Checked Then
                    strType = "Comment"
                End If
            Else
                strEmpID = cboSubordinate.SelectedValue
                If hfLevel.Value = "3" Then
                    strType = "TARGET_DESKTOP"
                    If optComment.Checked Then
                        strType = "COMMENT_DESKTOP"
                    End If

                    If strEmpID = "A" Then
                        For i = 1 To cboSubordinate.Items.Count - 1
                            ssql = "Exec sp_rpt_InsDelSelection " & spid & ",'" & cboSubordinate.Items(i).Value & "','ADD'"
                            mySQL.ExecuteSQL(ssql)
                        Next
                    Else
                        ssql = "Exec sp_rpt_InsDelSelection " & spid & ",'" & cboSubordinate.SelectedValue & "','ADD'"
                        mySQL.ExecuteSQL(ssql)
                    End If
                ElseIf strEmpID = "A" Then
                    strType = "Target_All"
                    If optComment.Checked Then
                        strType = "Comment_All"
                    End If
                Else
                    strType = "Target_Sub"
                    If optComment.Checked Then
                        strType = "Comment_Sub"
                    End If
                End If
            End If
        ElseIf optSkill.Checked Then
            If cboAppraisal.SelectedValue = "200812" Then
                xtr = New System.Xml.XmlTextReader(Server.MapPath("~") & "\_Templates\Ar\EmpSkillApprasalSheet.rpx")
            Else
                xtr = New System.Xml.XmlTextReader(Server.MapPath("~") & "\_Templates\Ar\EmpSkillApprasalSheet.rpx")
            End If

            If optPersonal.Checked Then
                strEmpID = Session("EmpID")
                strType = "Skill"
            Else
                strEmpID = cboSubordinate.SelectedValue

                If hfLevel.Value = "3" Then
                    strType = "SKILL_DESKTOP"

                    If strEmpID = "A" Then
                        For i = 1 To cboSubordinate.Items.Count - 1
                            ssql = "Exec sp_rpt_InsDelSelection " & spid & ",'" & cboSubordinate.Items(i).Value & "','ADD'"
                            mySQL.ExecuteSQL(ssql)
                        Next
                    Else
                        ssql = "Exec sp_rpt_InsDelSelection " & spid & ",'" & cboSubordinate.SelectedValue & "','ADD'"
                        mySQL.ExecuteSQL(ssql)
                    End If
                ElseIf strEmpID = "A" Then
                    strType = "Skill_All"
                Else
                    strType = "Skill_Sub"
                End If
            End If
        End If

        If hfLevel.Value = "3" And Not optPersonal.Checked Then
            Param1 = cboAppraisal.SelectedValue
            Param2 = spid
            Param4 = strType

            ssql = "Exec sp_rpt_ap_AppraisalForm_desktop '" & Param1 & "','" & Param2 & "','" & Param4 & "'"
        Else
            Param1 = cboAppraisal.SelectedValue
            Param2 = strEmpID
            Param3 = Session("EmpID")
            Param4 = strType

            ssql = "Exec sp_rpt_ap_AppraisalForm '" & Param1 & "','" & Param2 & "','" & Param3 & "','" & Param4 & "'"
        End If
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

        If hfLevel.Value = "3" And Not optPersonal.Checked Then
            ssql = "Exec sp_rpt_InsDelSelection " & spid & ",'','DEL'"
            mySQL.ExecuteSQL(ssql)
        End If


    End Sub

    Private Sub optSkill_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optSkill.CheckedChanged

        GenerateReport()

    End Sub

    Private Sub cboSubordinate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSubordinate.SelectedIndexChanged

        GenerateReport()

    End Sub

    Private Sub lnkReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkReport.Click

        Response.Redirect("AppraisalReport.aspx", True)

    End Sub

    Protected Sub optComment_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optComment.CheckedChanged

        GenerateReport()
    End Sub
End Class