Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class PAGES_EAPPRAISAL_STAFFSKILLS
    Inherits System.Web.UI.Page

    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting, MyKajima As New clsKajimaWeb
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Dim mMyConnection As New System.Data.SqlClient.SqlConnection
    Dim mMyCommand As New System.Data.SqlClient.SqlCommand
    Dim mMyDataReader As System.Data.SqlClient.SqlDataReader

    Dim mstrEmpID As String
    Dim mstrCurrentPeriod As String
    Dim mbError As Boolean

    Dim mstrAppraisee_1, mstrAppraisee_2, mstrAppraisee_3, mstrAppraisee_4, mstrAppraisee_5, mstrAppraisee_6, mstrAppraisee_7, mstrAppraisee_8, mstrAppraisee_9, mstrAppraisee_10 As String
    Dim mstr1stAppraiser_1, mstr1stAppraiser_2, mstr1stAppraiser_3, mstr1stAppraiser_4, mstr1stAppraiser_5, mstr1stAppraiser_6, mstr1stAppraiser_7, mstr1stAppraiser_8, mstr1stAppraiser_9, mstr1stAppraiser_10 As String
    Dim mstr2ndAppraiser_1 As String

    Dim mstrRating1_Desc1, mstrRating1_Desc2 As String
    Dim mstrRating2_Desc1, mstrRating2_Desc2 As String
    Dim mstrRating3_Desc1, mstrRating3_Desc2 As String
    Dim mstrRating4_Desc1, mstrRating4_Desc2 As String
    Dim mstrRating5_Desc1, mstrRating5_Desc2 As String

    Private Sub StaffSkills_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        mMyConnection.ConnectionString = mySQL.GetConnectionString()
        mMyConnection.Open()
        mMyCommand.Connection = mMyConnection

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim lstItem As New ListItem

        If Session("EmpID") = "" Then
            Response.Redirect("../Global/SessionTimeOut.aspx", True)
        Else
            Session("PreviousPage") = Request.RawUrl

            'lblUser.Text = Session("EmpID")
            lblUser.Text = Session("username")


            If optSubordinate.Checked Then
                mstrEmpID = cboSubordinate.SelectedValue
            Else
                mstrEmpID = Session("EmpID")
            End If
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

            cboAppraisal.Items(0).Enabled = False

            cboAppraisal.SelectedValue = mstrCurrentPeriod

        End If

        Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", "display()", True)


    End Sub
    Private Sub GetSkills()
        Dim strSkillCategory As String = ""

        lblCategory1.Text = ""
        lblCategory1_1.Text = ""
        lblCategory1_2.Text = ""
        lblCategory1_3.Text = ""
        lblCategory1_4.Text = ""
        lblCategory1_5.Text = ""

        lblCategory2.Text = ""
        lblCategory2_1.Text = ""
        lblCategory2_2.Text = ""
        lblCategory2_3.Text = ""
        lblCategory2_4.Text = ""
        lblCategory2_5.Text = ""

        lblCategory3.Text = ""
        lblCategory3_1.Text = ""
        lblCategory3_2.Text = ""
        lblCategory3_3.Text = ""
        lblCategory3_4.Text = ""
        lblCategory3_5.Text = ""

        lblCategory4.Text = ""
        lblCategory4_1.Text = ""
        lblCategory4_2.Text = ""
        lblCategory4_3.Text = ""
        lblCategory4_4.Text = ""
        lblCategory4_5.Text = ""

        lblCategory5.Text = ""
        lblCategory5_1.Text = ""
        lblCategory5_2.Text = ""
        lblCategory5_3.Text = ""
        lblCategory5_4.Text = ""
        lblCategory5_5.Text = ""

        lblCategory6.Text = ""
        lblCategory6_1.Text = ""
        lblCategory6_2.Text = ""
        lblCategory6_3.Text = ""
        lblCategory6_4.Text = ""
        lblCategory6_5.Text = ""

        lblCategory7.Text = ""
        lblCategory7_1.Text = ""
        lblCategory7_2.Text = ""
        lblCategory7_3.Text = ""
        lblCategory7_4.Text = ""
        lblCategory7_5.Text = ""

        lblCategory8.Text = ""
        lblCategory8_1.Text = ""
        lblCategory8_2.Text = ""
        lblCategory8_3.Text = ""
        lblCategory8_4.Text = ""
        lblCategory8_5.Text = ""

        lblCategory9.Text = ""
        lblCategory9_1.Text = ""
        lblCategory9_2.Text = ""
        lblCategory9_3.Text = ""
        lblCategory9_4.Text = ""
        lblCategory9_5.Text = ""

        lblCategory10.Text = ""
        lblCategory10_1.Text = ""
        lblCategory10_2.Text = ""
        lblCategory10_3.Text = ""
        lblCategory10_4.Text = ""
        lblCategory10_5.Text = ""

        'lblCategory1_1.Visible = False
        lblCategory1_2.Visible = False
        lblCategory1_3.Visible = False
        lblCategory1_4.Visible = False
        lblCategory1_5.Visible = False

        'lblCategory2_1.Visible = False
        lblCategory2_2.Visible = False
        lblCategory2_3.Visible = False
        lblCategory2_4.Visible = False
        lblCategory2_5.Visible = False

        'lblCategory3_1.Visible = False
        lblCategory3_2.Visible = False
        lblCategory3_3.Visible = False
        lblCategory3_4.Visible = False
        lblCategory3_5.Visible = False

        'lblCategory4_1.Visible = False
        lblCategory4_2.Visible = False
        lblCategory4_3.Visible = False
        lblCategory4_4.Visible = False
        lblCategory4_5.Visible = False

        'lblCategory5_1.Visible = False
        lblCategory5_2.Visible = False
        lblCategory5_3.Visible = False
        lblCategory5_4.Visible = False
        lblCategory5_5.Visible = False

        'lblCategory6_1.Visible = False
        lblCategory6_2.Visible = False
        lblCategory6_3.Visible = False
        lblCategory6_4.Visible = False
        lblCategory6_5.Visible = False

        'lblCategory7_1.Visible = False
        lblCategory7_2.Visible = False
        lblCategory7_3.Visible = False
        lblCategory7_4.Visible = False
        lblCategory7_5.Visible = False

        'lblCategory8_1.Visible = False
        lblCategory8_2.Visible = False
        lblCategory8_3.Visible = False
        lblCategory8_4.Visible = False
        lblCategory8_5.Visible = False

        'lblCategory9_1.Visible = False
        lblCategory9_2.Visible = False
        lblCategory9_3.Visible = False
        lblCategory9_4.Visible = False
        lblCategory9_5.Visible = False

        'lblCategory10_1.Visible = False
        lblCategory10_2.Visible = False
        lblCategory10_3.Visible = False
        lblCategory10_4.Visible = False
        lblCategory10_5.Visible = False

        mMyCommand.CommandText = "sp_ap_Sel_AppraisalSkills_Category '" & cboAppraisal.SelectedValue & "','" & mstrEmpID & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            strSkillCategory = mMyDataReader("category")

            '    mMyDataReader.Close()
            'Else
            '    mMyDataReader.Close()

            '    mMyCommand.CommandText = "sp_ap_Sel_AppraisalSkills_Category '" & cboAppraisal.SelectedValue & "','" & Session("EmpID") & "'"
            '    mMyDataReader = mMyCommand.ExecuteReader

            '    If mMyDataReader.Read Then
            '        strSkillCategory = mMyDataReader("category")
            '    End If
            '    mMyDataReader.Close()
        End If

        mMyDataReader.Close()

        mMyCommand.CommandText = "sp_ap_Sel_AppraisalSkills '" & cboAppraisal.SelectedValue & "','" & strSkillCategory & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        While mMyDataReader.Read()
            Select Case mMyDataReader("skill_seq")
                Case 1
                    lblCategory1.Text = mMyDataReader("skill")

                    Select Case mMyDataReader("desc_seq")
                        Case 1
                            lblCategory1_1.Text = (mMyDataReader("desc"))
                            lblCategory1_1.Visible = True
                        Case 2
                            lblCategory1_2.Text = (mMyDataReader("desc"))
                            lblCategory1_2.Visible = True
                        Case 3
                            lblCategory1_3.Text = (mMyDataReader("desc"))
                            lblCategory1_3.Visible = True
                        Case 4
                            lblCategory1_4.Text = (mMyDataReader("desc"))
                            lblCategory1_4.Visible = True
                        Case 5
                            lblCategory1_5.Text = (mMyDataReader("desc"))
                            lblCategory1_5.Visible = True
                    End Select

                Case 2
                    lblCategory2.Text = mMyDataReader("skill")

                    Select Case mMyDataReader("desc_seq")
                        Case 1
                            lblCategory2_1.Text = (mMyDataReader("desc"))
                            lblCategory2_1.Visible = True
                        Case 2
                            lblCategory2_2.Text = (mMyDataReader("desc"))
                            lblCategory2_2.Visible = True
                        Case 3
                            lblCategory2_3.Text = (mMyDataReader("desc"))
                            lblCategory2_3.Visible = True
                        Case 4
                            lblCategory2_4.Text = (mMyDataReader("desc"))
                            lblCategory2_4.Visible = True
                        Case 5
                            lblCategory2_5.Text = (mMyDataReader("desc"))
                            lblCategory2_5.Visible = True
                    End Select

                Case 3
                    lblCategory3.Text = mMyDataReader("skill")

                    Select Case mMyDataReader("desc_seq")
                        Case 1
                            lblCategory3_1.Text = (mMyDataReader("desc"))
                            lblCategory3_1.Visible = True
                        Case 2
                            lblCategory3_2.Text = (mMyDataReader("desc"))
                            lblCategory3_2.Visible = True
                        Case 3
                            lblCategory3_3.Text = (mMyDataReader("desc"))
                            lblCategory3_3.Visible = True
                        Case 4
                            lblCategory3_4.Text = (mMyDataReader("desc"))
                            lblCategory3_4.Visible = True
                        Case 5
                            lblCategory3_5.Text = (mMyDataReader("desc"))
                            lblCategory3_5.Visible = True
                    End Select

                Case 4
                    lblCategory4.Text = mMyDataReader("skill")

                    Select Case mMyDataReader("desc_seq")
                        Case 1
                            lblCategory4_1.Text = (mMyDataReader("desc"))
                            lblCategory4_1.Visible = True
                        Case 2
                            lblCategory4_2.Text = (mMyDataReader("desc"))
                            lblCategory4_2.Visible = True
                        Case 3
                            lblCategory4_3.Text = (mMyDataReader("desc"))
                            lblCategory4_3.Visible = True
                        Case 4
                            lblCategory4_4.Text = (mMyDataReader("desc"))
                            lblCategory4_4.Visible = True
                        Case 5
                            lblCategory4_5.Text = (mMyDataReader("desc"))
                            lblCategory4_5.Visible = True
                    End Select

                Case 5
                    lblCategory5.Text = mMyDataReader("skill")

                    Select Case mMyDataReader("desc_seq")
                        Case 1
                            lblCategory5_1.Text = (mMyDataReader("desc"))
                            lblCategory5_1.Visible = True
                        Case 2
                            lblCategory5_2.Text = (mMyDataReader("desc"))
                            lblCategory5_2.Visible = True
                        Case 3
                            lblCategory5_3.Text = (mMyDataReader("desc"))
                            lblCategory5_3.Visible = True
                        Case 4
                            lblCategory5_4.Text = (mMyDataReader("desc"))
                            lblCategory5_4.Visible = True
                        Case 5
                            lblCategory5_5.Text = (mMyDataReader("desc"))
                            lblCategory5_5.Visible = True
                    End Select

                Case 6
                    lblCategory6.Text = mMyDataReader("skill")

                    Select Case mMyDataReader("desc_seq")
                        Case 1
                            lblCategory6_1.Text = (mMyDataReader("desc"))
                            lblCategory6_1.Visible = True
                        Case 2
                            lblCategory6_2.Text = (mMyDataReader("desc"))
                            lblCategory6_2.Visible = True
                        Case 3
                            lblCategory6_3.Text = (mMyDataReader("desc"))
                            lblCategory6_3.Visible = True
                        Case 4
                            lblCategory6_4.Text = (mMyDataReader("desc"))
                            lblCategory6_4.Visible = True
                        Case 5
                            lblCategory6_5.Text = (mMyDataReader("desc"))
                            lblCategory6_5.Visible = True
                    End Select
                Case 7
                    lblCategory7.Text = mMyDataReader("skill")

                    Select Case mMyDataReader("desc_seq")
                        Case 1
                            lblCategory7_1.Text = (mMyDataReader("desc"))
                            lblCategory7_1.Visible = True
                        Case 2
                            lblCategory7_2.Text = (mMyDataReader("desc"))
                            lblCategory7_2.Visible = True
                        Case 3
                            lblCategory7_3.Text = (mMyDataReader("desc"))
                            lblCategory7_3.Visible = True
                        Case 4
                            lblCategory7_4.Text = (mMyDataReader("desc"))
                            lblCategory7_4.Visible = True
                        Case 5
                            lblCategory7_5.Text = (mMyDataReader("desc"))
                            lblCategory7_5.Visible = True
                    End Select

                Case 8
                    lblCategory8.Text = mMyDataReader("skill")

                    Select Case mMyDataReader("desc_seq")
                        Case 1
                            lblCategory8_1.Text = (mMyDataReader("desc"))
                            lblCategory8_1.Visible = True
                        Case 2
                            lblCategory8_2.Text = (mMyDataReader("desc"))
                            lblCategory8_2.Visible = True
                        Case 3
                            lblCategory8_3.Text = (mMyDataReader("desc"))
                            lblCategory8_3.Visible = True
                        Case 4
                            lblCategory8_4.Text = (mMyDataReader("desc"))
                            lblCategory8_4.Visible = True
                        Case 5
                            lblCategory8_5.Text = (mMyDataReader("desc"))
                            lblCategory8_5.Visible = True
                    End Select

                Case 9
                    lblCategory9.Text = mMyDataReader("skill")

                    Select Case mMyDataReader("desc_seq")
                        Case 1
                            lblCategory9_1.Text = (mMyDataReader("desc"))
                            lblCategory9_1.Visible = True
                        Case 2
                            lblCategory9_2.Text = (mMyDataReader("desc"))
                            lblCategory9_2.Visible = True
                        Case 3
                            lblCategory9_3.Text = (mMyDataReader("desc"))
                            lblCategory9_3.Visible = True
                        Case 4
                            lblCategory9_4.Text = (mMyDataReader("desc"))
                            lblCategory9_4.Visible = True
                        Case 5
                            lblCategory9_5.Text = (mMyDataReader("desc"))
                            lblCategory9_5.Visible = True
                    End Select

                Case 10
                    lblCategory10.Text = mMyDataReader("skill")

                    Select Case mMyDataReader("desc_seq")
                        Case 1
                            lblCategory10_1.Text = (mMyDataReader("desc"))
                            lblCategory10_1.Visible = True
                        Case 2
                            lblCategory10_2.Text = (mMyDataReader("desc"))
                            lblCategory10_2.Visible = True
                        Case 3
                            lblCategory10_3.Text = (mMyDataReader("desc"))
                            lblCategory10_3.Visible = True
                        Case 4
                            lblCategory10_4.Text = (mMyDataReader("desc"))
                            lblCategory10_4.Visible = True
                        Case 5
                            lblCategory10_5.Text = (mMyDataReader("desc"))
                            lblCategory10_5.Visible = True
                    End Select
            End Select
        End While

        mMyDataReader.Close()

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

        cboSubordinate.Enabled = True

    End Sub

    Protected Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        If bValidate() Then
            If optPersonal.Checked Then
                mMyCommand.CommandText = "sp_ap_InsUpdDel_EmpAppraisal_Skills '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'," & _
                                        Val(cboAppraisee_1.Text) & "," & Val(cboAppraisee_2.Text) & "," & Val(cboAppraisee_3.Text) & "," & Val(cboAppraisee_4.Text) & "," & Val(cboAppraisee_5.Text) & "," & _
                                        Val(cboAppraisee_6.Text) & "," & Val(cboAppraisee_7.Text) & "," & Val(cboAppraisee_8.Text) & "," & Val(cboAppraisee_9.Text) & "," & Val(cboAppraisee_10.Text) & _
                                        ",'" & "ADD" & "'"
            ElseIf hfLevel.Value = 1 Then
                mMyCommand.CommandText = "sp_ap_InsDel_Appraiser_Skills " & hfLevel.Value & ",'" & Session("EmpID") & "','" & cboSubordinate.SelectedValue & "','" & _
                                          cboAppraisal.SelectedValue & "'," & _
                                          Val(cbo1stAppraiser_1.Text) & "," & Val(cbo1stAppraiser_2.Text) & "," & Val(cbo1stAppraiser_3.Text) & "," & Val(cbo1stAppraiser_4.Text) & "," & _
                                          Val(cbo1stAppraiser_5.Text) & "," & _
                                          Val(cbo1stAppraiser_6.Text) & "," & Val(cbo1stAppraiser_7.Text) & "," & Val(cbo1stAppraiser_8.Text) & "," & Val(cbo1stAppraiser_9.Text) & "," & Val(cbo1stAppraiser_10.Text) & _
                                          ",'" & "ADD" & "'"

            ElseIf hfLevel.Value = 2 Then
                mMyCommand.CommandText = "sp_ap_InsDel_Appraiser_Skills " & hfLevel.Value & ",'" & Session("EmpID") & "','" & cboSubordinate.SelectedValue & "','" & _
                                          cboAppraisal.SelectedValue & "'," & _
                                          Val(cbo2ndAppraiser.Text) & "," & Val(cbo1stAppraiser_2.Text) & "," & Val(cbo1stAppraiser_3.Text) & "," & Val(cbo1stAppraiser_4.Text) & "," & _
                                          Val(cbo1stAppraiser_5.Text) & "," & _
                                          Val(cbo1stAppraiser_6.Text) & "," & Val(cbo1stAppraiser_7.Text) & "," & Val(cbo1stAppraiser_8.Text) & "," & Val(cbo1stAppraiser_9.Text) & "," & Val(cbo1stAppraiser_10.Text) & _
                                          ",'" & "ADD" & "'"
            ElseIf hfLevel.Value = "3" Then
                mMyCommand.CommandText = "sp_ap_InsUpdDel_EmpAppraisal_Skills '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'," & _
                                        Val(cboAppraisee_1.Text) & "," & Val(cboAppraisee_2.Text) & "," & Val(cboAppraisee_3.Text) & "," & Val(cboAppraisee_4.Text) & "," & Val(cboAppraisee_5.Text) & "," & _
                                        Val(cboAppraisee_6.Text) & "," & Val(cboAppraisee_7.Text) & "," & Val(cboAppraisee_8.Text) & "," & Val(cboAppraisee_9.Text) & "," & Val(cboAppraisee_10.Text) & _
                                        ",'" & "ADD" & "'"
                mMyDataReader = mMyCommand.ExecuteReader
                mMyDataReader.Close()

                mMyCommand.CommandText = "sp_ap_InsDel_Appraiser_Skills " & hfLevel.Value & ",'" & Session("EmpID") & "','" & cboSubordinate.SelectedValue & "','" & _
                                          cboAppraisal.SelectedValue & "'," & _
                                          Val(cbo1stAppraiser_1.Text) & "," & Val(cbo1stAppraiser_2.Text) & "," & Val(cbo1stAppraiser_3.Text) & "," & Val(cbo1stAppraiser_4.Text) & "," & _
                                          Val(cbo1stAppraiser_5.Text) & "," & _
                                          Val(cbo1stAppraiser_6.Text) & "," & Val(cbo1stAppraiser_7.Text) & "," & Val(cbo1stAppraiser_8.Text) & "," & Val(cbo1stAppraiser_9.Text) & "," & Val(cbo1stAppraiser_10.Text) & _
                                          ",'" & "ADD" & "'"
                mMyDataReader = mMyCommand.ExecuteReader
                mMyDataReader.Close()

                mMyCommand.CommandText = "sp_ap_InsDel_Appraiser_Skills " & hfLevel.Value & ",'" & Session("EmpID") & "','" & cboSubordinate.SelectedValue & "','" & _
                                          cboAppraisal.SelectedValue & "'," & _
                                          Val(cbo2ndAppraiser.Text) & "," & Val(cbo1stAppraiser_2.Text) & "," & Val(cbo1stAppraiser_3.Text) & "," & Val(cbo1stAppraiser_4.Text) & "," & _
                                          Val(cbo1stAppraiser_5.Text) & "," & _
                                          Val(cbo1stAppraiser_6.Text) & "," & Val(cbo1stAppraiser_7.Text) & "," & Val(cbo1stAppraiser_8.Text) & "," & Val(cbo1stAppraiser_9.Text) & "," & Val(cbo1stAppraiser_10.Text) & _
                                          ",'" & "ADD" & "'"

            End If

            mMyDataReader = mMyCommand.ExecuteReader
            mMyDataReader.Close()
        End If


    End Sub
    Private Function bValidate() As Boolean

        bValidate = True

        lblError.Text = ""
        lblError.Visible = False

        If optPersonal.Checked Then
            If Val(cboAppraisee_1.SelectedValue) = 0 Or Val(cboAppraisee_2.SelectedValue) = 0 Or Val(cboAppraisee_3.SelectedValue) = 0 Or Val(cboAppraisee_4.SelectedValue) = 0 Or Val(cboAppraisee_5.SelectedValue) = 0 Or _
               Val(cboAppraisee_6.SelectedValue) = 0 Or Val(cboAppraisee_7.SelectedValue) = 0 Or Val(cboAppraisee_8.SelectedValue) = 0 Or Val(cboAppraisee_9.SelectedValue) = 0 Or Val(cboAppraisee_10.SelectedValue) = 0 Then

                lblError.Text = "Appraisee's Points incomplete."
                mbError = True
                GoTo ErrorHandle
            End If
        ElseIf hfLevel.Value = 1 Then
            If Val(cbo1stAppraiser_1.SelectedValue) = 0 Or Val(cbo1stAppraiser_2.SelectedValue) = 0 Or Val(cbo1stAppraiser_3.SelectedValue) = 0 Or Val(cbo1stAppraiser_4.SelectedValue) = 0 Or Val(cbo1stAppraiser_5.SelectedValue) = 0 Or _
               Val(cbo1stAppraiser_6.SelectedValue) = 0 Or Val(cbo1stAppraiser_7.SelectedValue) = 0 Or Val(cbo1stAppraiser_8.SelectedValue) = 0 Or Val(cbo1stAppraiser_9.SelectedValue) = 0 Or Val(cbo1stAppraiser_10.SelectedValue) = 0 Then

                lblError.Text = "1st Appraiser's Points incomplete."
                mbError = True
                GoTo ErrorHandle
            End If
        ElseIf hfLevel.Value = 2 Then
            If Val(cbo2ndAppraiser.SelectedValue) = 0 Then
                lblError.Text = "2nd Appraiser's Point incomplete."
                mbError = True
                GoTo ErrorHandle
            End If
        ElseIf hfLevel.Value = "3" Then
            If Val(cboAppraisee_1.SelectedValue) = 0 Or Val(cboAppraisee_2.SelectedValue) = 0 Or Val(cboAppraisee_3.SelectedValue) = 0 Or Val(cboAppraisee_4.SelectedValue) = 0 Or Val(cboAppraisee_5.SelectedValue) = 0 Or _
               Val(cboAppraisee_6.SelectedValue) = 0 Or Val(cboAppraisee_7.SelectedValue) = 0 Or Val(cboAppraisee_8.SelectedValue) = 0 Or Val(cboAppraisee_9.SelectedValue) = 0 Or Val(cboAppraisee_10.SelectedValue) = 0 Then

                lblError.Text = "Appraisee's Points incomplete."
                mbError = True
                GoTo ErrorHandle
            End If
        End If

ErrorHandle:
        If mbError Then
            lblError.Visible = True
            lblError.BackColor = Drawing.Color.Red
            lblError.ForeColor = Drawing.Color.White

            If optPersonal.Checked Then
                mstrAppraisee_1 = cboAppraisee_1.SelectedValue
                mstrAppraisee_2 = cboAppraisee_2.SelectedValue
                mstrAppraisee_3 = cboAppraisee_3.SelectedValue
                mstrAppraisee_4 = cboAppraisee_4.SelectedValue
                mstrAppraisee_5 = cboAppraisee_5.SelectedValue
                mstrAppraisee_6 = cboAppraisee_6.SelectedValue
                mstrAppraisee_7 = cboAppraisee_7.SelectedValue
                mstrAppraisee_8 = cboAppraisee_8.SelectedValue
                mstrAppraisee_9 = cboAppraisee_9.SelectedValue
                mstrAppraisee_10 = cboAppraisee_10.SelectedValue
            ElseIf hfLevel.Value = 1 Then
                mstr1stAppraiser_1 = cbo1stAppraiser_1.SelectedValue
                mstr1stAppraiser_2 = cbo1stAppraiser_2.SelectedValue
                mstr1stAppraiser_3 = cbo1stAppraiser_3.SelectedValue
                mstr1stAppraiser_4 = cbo1stAppraiser_4.SelectedValue
                mstr1stAppraiser_5 = cbo1stAppraiser_5.SelectedValue
                mstr1stAppraiser_6 = cbo1stAppraiser_6.SelectedValue
                mstr1stAppraiser_7 = cbo1stAppraiser_7.SelectedValue
                mstr1stAppraiser_8 = cbo1stAppraiser_8.SelectedValue
                mstr1stAppraiser_9 = cbo1stAppraiser_9.SelectedValue
                mstr1stAppraiser_10 = cbo1stAppraiser_10.SelectedValue
            ElseIf hfLevel.Value = 2 Then
                mstr2ndAppraiser_1 = cbo2ndAppraiser.SelectedValue
            ElseIf hfLevel.Value = "3" Then
                mstrAppraisee_1 = cboAppraisee_1.SelectedValue
                mstrAppraisee_2 = cboAppraisee_2.SelectedValue
                mstrAppraisee_3 = cboAppraisee_3.SelectedValue
                mstrAppraisee_4 = cboAppraisee_4.SelectedValue
                mstrAppraisee_5 = cboAppraisee_5.SelectedValue
                mstrAppraisee_6 = cboAppraisee_6.SelectedValue
                mstrAppraisee_7 = cboAppraisee_7.SelectedValue
                mstrAppraisee_8 = cboAppraisee_8.SelectedValue
                mstrAppraisee_9 = cboAppraisee_9.SelectedValue
                mstrAppraisee_10 = cboAppraisee_10.SelectedValue
                mstr1stAppraiser_1 = cbo1stAppraiser_1.SelectedValue
                mstr1stAppraiser_2 = cbo1stAppraiser_2.SelectedValue
                mstr1stAppraiser_3 = cbo1stAppraiser_3.SelectedValue
                mstr1stAppraiser_4 = cbo1stAppraiser_4.SelectedValue
                mstr1stAppraiser_5 = cbo1stAppraiser_5.SelectedValue
                mstr1stAppraiser_6 = cbo1stAppraiser_6.SelectedValue
                mstr1stAppraiser_7 = cbo1stAppraiser_7.SelectedValue
                mstr1stAppraiser_8 = cbo1stAppraiser_8.SelectedValue
                mstr1stAppraiser_9 = cbo1stAppraiser_9.SelectedValue
                mstr1stAppraiser_10 = cbo1stAppraiser_10.SelectedValue
                mstr2ndAppraiser_1 = cbo2ndAppraiser.SelectedValue
            End If

            Exit Function
        End If

        mbError = False
        'lblError.Text = ""
        'lblError.Visible = False

        lblError.Visible = True
        lblError.Text = "Done! Thank you."
        lblError.BackColor = Drawing.Color.Tan
        lblError.ForeColor = Drawing.Color.White


        mstrAppraisee_1 = ""
        mstrAppraisee_2 = ""
        mstrAppraisee_3 = ""
        mstrAppraisee_4 = ""
        mstrAppraisee_5 = ""
        mstrAppraisee_6 = ""
        mstrAppraisee_7 = ""
        mstrAppraisee_8 = ""
        mstrAppraisee_9 = ""
        mstrAppraisee_10 = ""

        mstr1stAppraiser_1 = ""
        mstr1stAppraiser_2 = ""
        mstr1stAppraiser_3 = ""
        mstr1stAppraiser_4 = ""
        mstr1stAppraiser_5 = ""
        mstr1stAppraiser_6 = ""
        mstr1stAppraiser_7 = ""
        mstr1stAppraiser_8 = ""
        mstr1stAppraiser_9 = ""
        mstr1stAppraiser_10 = ""

        mstr2ndAppraiser_1 = ""

        bValidate = True

    End Function

    Private Sub StaffSkills_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        Dim bOpenAppraisee, bOpen1stAppraiser, bOpen2ndAppraiser As Boolean

        bOpenAppraisee = False
        bOpen1stAppraiser = False
        bOpen2ndAppraiser = False

        GetSkillRating()

        Initialise()

        mMyCommand.CommandText = "sp_ap_Sel_AppraisalGroup '" & cboAppraisal.SelectedValue & "','" & Session("EmpID") & "','" & mstrEmpID & "','" & _
                                 "Level" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            hfLevel.Value = mMyDataReader("level")

            lblAppraiserLevel.BackColor = Drawing.Color.SteelBlue
            lblAppraiserLevel.BorderStyle = BorderStyle.Solid
            lblAppraiserLevel.BorderColor = Drawing.Color.Black

            If hfLevel.Value = 1 Then
                lblAppraiserLevel.Text = "1st Appraiser"
            ElseIf hfLevel.Value = 2 Then
                lblAppraiserLevel.Text = "2nd Appraiser"
            ElseIf hfLevel.Value = "3" Then
                lblAppraiserLevel.Text = "Desktop View"
            End If
        Else
            hfLevel.Value = 0

            lblAppraiserLevel.BorderStyle = BorderStyle.None
            lblAppraiserLevel.BackColor = Nothing
            lblAppraiserLevel.BorderColor = Nothing
            lblAppraiserLevel.Text = ""
        End If
        mMyDataReader.Close()

        btnOK.Enabled = False

        mMyCommand.CommandText = "sp_ap_sel_AppraisalPeriod '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & "Cutoff" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read And cboAppraisal.SelectedIndex <> 0 Then
            bOpenAppraisee = mMyDataReader("Open_Appraisee")
            bOpen1stAppraiser = mMyDataReader("Open_1stAppraiser")
            bOpen2ndAppraiser = mMyDataReader("Open_2ndAppraiser")
        End If
        mMyDataReader.Close()

        If optPersonal.Checked Then
            mMyCommand.CommandText = "sp_ap_Sel_AppraisalGroup '" & cboAppraisal.SelectedValue & "','" & Session("EmpID") & "','" & mstrEmpID & "','" & _
                                     "Personal" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            If mMyDataReader.Read() And bOpenAppraisee Then
                cboAppraisee_1.Enabled = True
                cboAppraisee_2.Enabled = True
                cboAppraisee_3.Enabled = True
                cboAppraisee_4.Enabled = True
                cboAppraisee_5.Enabled = True
                cboAppraisee_6.Enabled = True
                cboAppraisee_7.Enabled = True
                cboAppraisee_8.Enabled = True
                cboAppraisee_9.Enabled = True
                cboAppraisee_10.Enabled = True

                btnOK.Enabled = True
            Else
                cboAppraisee_1.Enabled = False
                cboAppraisee_2.Enabled = False
                cboAppraisee_3.Enabled = False
                cboAppraisee_4.Enabled = False
                cboAppraisee_5.Enabled = False
                cboAppraisee_6.Enabled = False
                cboAppraisee_7.Enabled = False
                cboAppraisee_8.Enabled = False
                cboAppraisee_9.Enabled = False
                cboAppraisee_10.Enabled = False

            End If
            mMyDataReader.Close()
        ElseIf hfLevel.Value = "3" Then
            cboAppraisee_1.Enabled = True
            cboAppraisee_2.Enabled = True
            cboAppraisee_3.Enabled = True
            cboAppraisee_4.Enabled = True
            cboAppraisee_5.Enabled = True
            cboAppraisee_6.Enabled = True
            cboAppraisee_7.Enabled = True
            cboAppraisee_8.Enabled = True
            cboAppraisee_9.Enabled = True
            cboAppraisee_10.Enabled = True
        End If

        'Response.Write("1. " & cboAppraisee_1.SelectedValue & "," & cboAppraisee_2.SelectedValue & "," & cboAppraisee_3.SelectedValue & "," & cboAppraisee_4.SelectedValue & "," & cboAppraisee_5.SelectedValue & "," & cboAppraisee_6.SelectedValue & "," & cboAppraisee_7.SelectedValue & "," & cboAppraisee_8.SelectedValue & "," & cboAppraisee_9.SelectedValue & "," & cboAppraisee_10.SelectedValue)

        mMyCommand.CommandText = "sp_ap_Sel_EmpAppraisal_Skills '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & "Skills" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            cboAppraisee_1.SelectedValue = mMyDataReader("1")
            cboAppraisee_2.SelectedValue = mMyDataReader("2")
            cboAppraisee_3.SelectedValue = mMyDataReader("3")
            cboAppraisee_4.SelectedValue = mMyDataReader("4")
            cboAppraisee_5.SelectedValue = mMyDataReader("5")
            cboAppraisee_6.SelectedValue = mMyDataReader("6")
            cboAppraisee_7.SelectedValue = mMyDataReader("7")
            cboAppraisee_8.SelectedValue = mMyDataReader("8")
            cboAppraisee_9.SelectedValue = mMyDataReader("9")
            cboAppraisee_10.SelectedValue = mMyDataReader("10")

            'Response.Write("2. " & mMyDataReader("1") & "," & mMyDataReader("2") & "," & mMyDataReader("3") & "," & mMyDataReader("4") & "," & mMyDataReader("5") & "," & mMyDataReader("6") & "," & mMyDataReader("7") & "," & mMyDataReader("8") & "," & mMyDataReader("9") & "," & mMyDataReader("10"))
        End If
        'Response.Write("3. " & cboAppraisee_1.SelectedValue & "," & cboAppraisee_2.SelectedValue & "," & cboAppraisee_3.SelectedValue & "," & cboAppraisee_4.SelectedValue & "," & cboAppraisee_5.SelectedValue & "," & cboAppraisee_6.SelectedValue & "," & cboAppraisee_7.SelectedValue & "," & cboAppraisee_8.SelectedValue & "," & cboAppraisee_9.SelectedValue & "," & cboAppraisee_10.SelectedValue)

        mMyDataReader.Close()

        lblAppraisee_Total.Text = Val(cboAppraisee_1.SelectedValue) + Val(cboAppraisee_2.SelectedValue) + Val(cboAppraisee_3.SelectedValue) + Val(cboAppraisee_4.SelectedValue) + Val(cboAppraisee_5.SelectedValue) + _
                                  Val(cboAppraisee_6.SelectedValue) + Val(cboAppraisee_7.SelectedValue) + Val(cboAppraisee_8.SelectedValue) + Val(cboAppraisee_9.SelectedValue) + Val(cboAppraisee_10.SelectedValue)





        If optSubordinate.Checked Then
            cbo1stAppraiser_1.Enabled = False
            cbo1stAppraiser_2.Enabled = False
            cbo1stAppraiser_3.Enabled = False
            cbo1stAppraiser_4.Enabled = False
            cbo1stAppraiser_5.Enabled = False
            cbo1stAppraiser_6.Enabled = False
            cbo1stAppraiser_7.Enabled = False
            cbo1stAppraiser_8.Enabled = False
            cbo1stAppraiser_9.Enabled = False
            cbo1stAppraiser_10.Enabled = False

            cbo2ndAppraiser.Enabled = False


            If hfLevel.Value = 1 Or hfLevel.Value = 2 Then
                mMyCommand.CommandText = "sp_ap_Sel_Appraiser_Skills " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & _
                                          cboAppraisal.SelectedValue & "','" & "Skills" & "'"
                mMyDataReader = mMyCommand.ExecuteReader

                If mMyDataReader.Read Then
                    cbo1stAppraiser_1.SelectedValue = mMyDataReader("1")
                    cbo1stAppraiser_2.SelectedValue = mMyDataReader("2")
                    cbo1stAppraiser_3.SelectedValue = mMyDataReader("3")
                    cbo1stAppraiser_4.SelectedValue = mMyDataReader("4")
                    cbo1stAppraiser_5.SelectedValue = mMyDataReader("5")
                    cbo1stAppraiser_6.SelectedValue = mMyDataReader("6")
                    cbo1stAppraiser_7.SelectedValue = mMyDataReader("7")
                    cbo1stAppraiser_8.SelectedValue = mMyDataReader("8")
                    cbo1stAppraiser_9.SelectedValue = mMyDataReader("9")
                    cbo1stAppraiser_10.SelectedValue = mMyDataReader("10")
                End If
                mMyDataReader.Close()

                If hfLevel.Value = 1 And bOpen1stAppraiser Then
                    cbo1stAppraiser_1.Enabled = True
                    cbo1stAppraiser_2.Enabled = True
                    cbo1stAppraiser_3.Enabled = True
                    cbo1stAppraiser_4.Enabled = True
                    cbo1stAppraiser_5.Enabled = True
                    cbo1stAppraiser_6.Enabled = True
                    cbo1stAppraiser_7.Enabled = True
                    cbo1stAppraiser_8.Enabled = True
                    cbo1stAppraiser_9.Enabled = True
                    cbo1stAppraiser_10.Enabled = True

                    btnOK.Enabled = True
                End If
            End If

            If hfLevel.Value = 2 Then
                mMyCommand.CommandText = "sp_ap_Sel_Appraiser_Skills " & "2" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & _
                                        cboAppraisal.SelectedValue & "','" & "Overall" & "'"
                mMyDataReader = mMyCommand.ExecuteReader

                If mMyDataReader.Read Then
                    cbo2ndAppraiser.SelectedValue = mMyDataReader("points")
                End If
                mMyDataReader.Close()

                If bOpen2ndAppraiser Then
                    cbo2ndAppraiser.Enabled = True
                    btnOK.Enabled = True
                End If
            End If

            If hfLevel.Value = "3" Then
                mMyCommand.CommandText = "sp_ap_Sel_Appraiser_Skills " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & _
                                          cboAppraisal.SelectedValue & "','" & "Skills" & "'"
                mMyDataReader = mMyCommand.ExecuteReader

                If mMyDataReader.Read Then
                    cbo1stAppraiser_1.SelectedValue = mMyDataReader("1")
                    cbo1stAppraiser_2.SelectedValue = mMyDataReader("2")
                    cbo1stAppraiser_3.SelectedValue = mMyDataReader("3")
                    cbo1stAppraiser_4.SelectedValue = mMyDataReader("4")
                    cbo1stAppraiser_5.SelectedValue = mMyDataReader("5")
                    cbo1stAppraiser_6.SelectedValue = mMyDataReader("6")
                    cbo1stAppraiser_7.SelectedValue = mMyDataReader("7")
                    cbo1stAppraiser_8.SelectedValue = mMyDataReader("8")
                    cbo1stAppraiser_9.SelectedValue = mMyDataReader("9")
                    cbo1stAppraiser_10.SelectedValue = mMyDataReader("10")
                End If
                mMyDataReader.Close()

                cbo1stAppraiser_1.Enabled = True
                cbo1stAppraiser_2.Enabled = True
                cbo1stAppraiser_3.Enabled = True
                cbo1stAppraiser_4.Enabled = True
                cbo1stAppraiser_5.Enabled = True
                cbo1stAppraiser_6.Enabled = True
                cbo1stAppraiser_7.Enabled = True
                cbo1stAppraiser_8.Enabled = True
                cbo1stAppraiser_9.Enabled = True
                cbo1stAppraiser_10.Enabled = True

                mMyCommand.CommandText = "sp_ap_Sel_Appraiser_Skills " & "2" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & _
                                        cboAppraisal.SelectedValue & "','" & "Overall" & "'"
                mMyDataReader = mMyCommand.ExecuteReader

                If mMyDataReader.Read Then
                    cbo2ndAppraiser.SelectedValue = mMyDataReader("points")
                End If
                mMyDataReader.Close()

                cbo2ndAppraiser.Enabled = True
                btnOK.Enabled = True

            End If
        End If

        lbl1stAppraiser_Total.Text = Val(cbo1stAppraiser_1.SelectedValue) + Val(cbo1stAppraiser_2.SelectedValue) + Val(cbo1stAppraiser_3.SelectedValue) + Val(cbo1stAppraiser_4.SelectedValue) + Val(cbo1stAppraiser_5.SelectedValue) + _
                                     Val(cbo1stAppraiser_6.SelectedValue) + Val(cbo1stAppraiser_7.SelectedValue) + Val(cbo1stAppraiser_8.SelectedValue) + Val(cbo1stAppraiser_9.SelectedValue) + Val(cbo1stAppraiser_10.SelectedValue)

        GetSkills()

        If mbError Then
            If optPersonal.Checked Then
                cboAppraisee_1.SelectedValue = mstrAppraisee_1
                cboAppraisee_2.SelectedValue = mstrAppraisee_2
                cboAppraisee_3.SelectedValue = mstrAppraisee_3
                cboAppraisee_4.SelectedValue = mstrAppraisee_4
                cboAppraisee_5.SelectedValue = mstrAppraisee_5
                cboAppraisee_6.SelectedValue = mstrAppraisee_6
                cboAppraisee_7.SelectedValue = mstrAppraisee_7
                cboAppraisee_8.SelectedValue = mstrAppraisee_8
                cboAppraisee_9.SelectedValue = mstrAppraisee_9
                cboAppraisee_10.SelectedValue = mstrAppraisee_10
            ElseIf hfLevel.Value = 1 Then
                cbo1stAppraiser_1.SelectedValue = mstr1stAppraiser_1
                cbo1stAppraiser_2.SelectedValue = mstr1stAppraiser_2
                cbo1stAppraiser_3.SelectedValue = mstr1stAppraiser_3
                cbo1stAppraiser_4.SelectedValue = mstr1stAppraiser_4
                cbo1stAppraiser_5.SelectedValue = mstr1stAppraiser_5
                cbo1stAppraiser_6.SelectedValue = mstr1stAppraiser_6
                cbo1stAppraiser_7.SelectedValue = mstr1stAppraiser_7
                cbo1stAppraiser_8.SelectedValue = mstr1stAppraiser_8
                cbo1stAppraiser_9.SelectedValue = mstr1stAppraiser_9
                cbo1stAppraiser_10.SelectedValue = mstr1stAppraiser_10
            ElseIf hfLevel.Value = 2 Then
                cbo2ndAppraiser.SelectedValue = mstr2ndAppraiser_1
            ElseIf hfLevel.Value = "3" Then
                cboAppraisee_1.SelectedValue = mstrAppraisee_1
                cboAppraisee_2.SelectedValue = mstrAppraisee_2
                cboAppraisee_3.SelectedValue = mstrAppraisee_3
                cboAppraisee_4.SelectedValue = mstrAppraisee_4
                cboAppraisee_5.SelectedValue = mstrAppraisee_5
                cboAppraisee_6.SelectedValue = mstrAppraisee_6
                cboAppraisee_7.SelectedValue = mstrAppraisee_7
                cboAppraisee_8.SelectedValue = mstrAppraisee_8
                cboAppraisee_9.SelectedValue = mstrAppraisee_9
                cboAppraisee_10.SelectedValue = mstrAppraisee_10
                cbo1stAppraiser_1.SelectedValue = mstr1stAppraiser_1
                cbo1stAppraiser_2.SelectedValue = mstr1stAppraiser_2
                cbo1stAppraiser_3.SelectedValue = mstr1stAppraiser_3
                cbo1stAppraiser_4.SelectedValue = mstr1stAppraiser_4
                cbo1stAppraiser_5.SelectedValue = mstr1stAppraiser_5
                cbo1stAppraiser_6.SelectedValue = mstr1stAppraiser_6
                cbo1stAppraiser_7.SelectedValue = mstr1stAppraiser_7
                cbo1stAppraiser_8.SelectedValue = mstr1stAppraiser_8
                cbo1stAppraiser_9.SelectedValue = mstr1stAppraiser_9
                cbo1stAppraiser_10.SelectedValue = mstr1stAppraiser_10
                cbo2ndAppraiser.SelectedValue = mstr2ndAppraiser_1
            End If
        End If

        AppraiseeSessionStatus()

    End Sub
    Private Sub AppraiseeSessionStatus()

        Dim strDate As String

        mMyCommand.CommandText = "sp_ap_Sel_AppraisalPeriod '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & "LOCK" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            strDate = mMyDataReader("date")
        Else
            strDate = ""
        End If

        If hfLevel.Value = 1 And optSubordinate.Checked And cboAppraisal.SelectedIndex < 2 Then
            btnLock.Visible = True

            If strDate = "" Then
                btnLock.Text = "Lock Appraisee's Session"
                lblLock.Text = ""
                lblLock.Visible = False
            Else
                btnLock.Text = "Unlock Appraisee's Session"
                lblLock.Text = "Session Locked: " & strDate
                lblLock.Visible = True
            End If
        Else
            btnLock.Visible = False
            lblLock.Visible = False

            If cboAppraisal.SelectedIndex < 2 Then
                If strDate = "" Then
                    lblAppraiseeSession.Visible = False
                    lblAppraiseeSession.Text = strDate
                Else
                    lblAppraiseeSession.Visible = True
                    lblAppraiseeSession.Text = "Appraisee's Session Locked: " & strDate
                End If
            Else
                lblAppraiseeSession.Visible = False
            End If


        End If

    End Sub
    Private Sub GetSkillRating()



        Dim iRate As Integer = 0
        Dim iLoop As Integer

        Dim lstItem As ListItem

        'cboAppraisee_1.Items.Clear()
        'cboAppraisee_2.Items.Clear()
        'cboAppraisee_3.Items.Clear()
        'cboAppraisee_4.Items.Clear()
        'cboAppraisee_5.Items.Clear()
        'cboAppraisee_6.Items.Clear()
        'cboAppraisee_7.Items.Clear()
        'cboAppraisee_8.Items.Clear()
        'cboAppraisee_9.Items.Clear()
        'cboAppraisee_10.Items.Clear()

        'cbo1stAppraiser_1.Items.Clear()
        'cbo1stAppraiser_2.Items.Clear()
        'cbo1stAppraiser_3.Items.Clear()
        'cbo1stAppraiser_4.Items.Clear()
        'cbo1stAppraiser_5.Items.Clear()
        'cbo1stAppraiser_6.Items.Clear()
        'cbo1stAppraiser_7.Items.Clear()
        'cbo1stAppraiser_8.Items.Clear()
        'cbo1stAppraiser_9.Items.Clear()
        'cbo1stAppraiser_10.Items.Clear()

        'lstItem = New ListItem
        'lstItem.Text = ""
        'lstItem.Value = ""

        'cboAppraisee_1.Items.Add(lstItem)
        'cboAppraisee_2.Items.Add(lstItem)
        'cboAppraisee_3.Items.Add(lstItem)
        'cboAppraisee_4.Items.Add(lstItem)
        'cboAppraisee_5.Items.Add(lstItem)
        'cboAppraisee_6.Items.Add(lstItem)
        'cboAppraisee_7.Items.Add(lstItem)
        'cboAppraisee_8.Items.Add(lstItem)
        'cboAppraisee_9.Items.Add(lstItem)
        'cboAppraisee_10.Items.Add(lstItem)

        'cbo1stAppraiser_1.Items.Add(lstItem)
        'cbo1stAppraiser_2.Items.Add(lstItem)
        'cbo1stAppraiser_3.Items.Add(lstItem)
        'cbo1stAppraiser_4.Items.Add(lstItem)
        'cbo1stAppraiser_5.Items.Add(lstItem)
        'cbo1stAppraiser_6.Items.Add(lstItem)
        'cbo1stAppraiser_7.Items.Add(lstItem)
        'cbo1stAppraiser_8.Items.Add(lstItem)
        'cbo1stAppraiser_9.Items.Add(lstItem)
        'cbo1stAppraiser_10.Items.Add(lstItem)

        mMyCommand.CommandText = "sp_ap_Sel_AppraisalSkills_Rating '" & cboAppraisal.SelectedValue & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        While mMyDataReader.Read
            If mMyDataReader("Seq") = 1 Then
                mstrRating1_Desc1 = mMyDataReader("Desc1")
                mstrRating1_Desc2 = mMyDataReader("Desc2")
            ElseIf mMyDataReader("Seq") = 2 Then
                mstrRating2_Desc1 = mMyDataReader("Desc1")
                mstrRating2_Desc2 = mMyDataReader("Desc2")
            ElseIf mMyDataReader("Seq") = 3 Then
                mstrRating3_Desc1 = mMyDataReader("Desc1")
                mstrRating3_Desc2 = mMyDataReader("Desc2")
            ElseIf mMyDataReader("Seq") = 4 Then
                mstrRating4_Desc1 = mMyDataReader("Desc1")
                mstrRating4_Desc2 = mMyDataReader("Desc2")
            ElseIf mMyDataReader("Seq") = 5 Then
                mstrRating5_Desc1 = mMyDataReader("Desc1")
                mstrRating5_Desc2 = mMyDataReader("Desc2")
            End If

            iRate = iRate + 1

            'lstItem = New ListItem

            'lstItem.Text = mMyDataReader("Desc1")
            'lstItem.Value = iRate

            'cboAppraisee_1.Items.Add(lstItem)
            'cboAppraisee_2.Items.Add(lstItem)
            'cboAppraisee_3.Items.Add(lstItem)
            'cboAppraisee_4.Items.Add(lstItem)
            'cboAppraisee_5.Items.Add(lstItem)
            'cboAppraisee_6.Items.Add(lstItem)
            'cboAppraisee_7.Items.Add(lstItem)
            'cboAppraisee_8.Items.Add(lstItem)
            'cboAppraisee_9.Items.Add(lstItem)
            'cboAppraisee_10.Items.Add(lstItem)

            'cbo1stAppraiser_1.Items.Add(lstItem)
            'cbo1stAppraiser_2.Items.Add(lstItem)
            'cbo1stAppraiser_3.Items.Add(lstItem)
            'cbo1stAppraiser_4.Items.Add(lstItem)
            'cbo1stAppraiser_5.Items.Add(lstItem)
            'cbo1stAppraiser_6.Items.Add(lstItem)
            'cbo1stAppraiser_7.Items.Add(lstItem)
            'cbo1stAppraiser_8.Items.Add(lstItem)
            'cbo1stAppraiser_9.Items.Add(lstItem)
            'cbo1stAppraiser_10.Items.Add(lstItem)
        End While
        mMyDataReader.Close()

        lblRating1_Desc1.Text = ""
        lblRating1_Desc2.Text = ""
        lblRating2_Desc1.Text = ""
        lblRating2_Desc2.Text = ""
        lblRating3_Desc1.Text = ""
        lblRating3_Desc2.Text = ""
        lblRating4_Desc1.Text = ""
        lblRating4_Desc2.Text = ""
        lblRating5_Desc1.Text = ""
        lblRating5_Desc2.Text = ""

        If mstrRating1_Desc1 <> "" Then
            lblRating1_Desc1.Text = mstrRating1_Desc1
            lblRating1_Desc2.Text = " - " + mstrRating1_Desc2
        End If

        If mstrRating2_Desc1 <> "" Then
            lblRating2_Desc1.Text = mstrRating2_Desc1
            lblRating2_Desc2.Text = " - " + mstrRating2_Desc2
        End If

        If mstrRating3_Desc1 <> "" Then
            lblRating3_Desc1.Text = mstrRating3_Desc1
            lblRating3_Desc2.Text = " - " + mstrRating3_Desc2
        End If

        If mstrRating4_Desc2 <> "" Then
            lblRating4_Desc1.Text = mstrRating4_Desc1
            lblRating4_Desc2.Text = " - " + mstrRating4_Desc2
        End If

        If mstrRating5_Desc1 <> "" Then
            lblRating5_Desc1.Text = mstrRating5_Desc1
            lblRating5_Desc2.Text = " - " + mstrRating5_Desc2
        End If

        'cboAppraisee_1.SelectedIndex = -1
        'cboAppraisee_2.SelectedIndex = -1
        'cboAppraisee_3.SelectedIndex = -1
        'cboAppraisee_4.SelectedIndex = -1
        'cboAppraisee_5.SelectedIndex = -1
        'cboAppraisee_6.SelectedIndex = -1
        'cboAppraisee_7.SelectedIndex = -1
        'cboAppraisee_8.SelectedIndex = -1
        'cboAppraisee_9.SelectedIndex = -1
        'cboAppraisee_10.SelectedIndex = -1

        'cbo1stAppraiser_1.SelectedIndex = -1
        'cbo1stAppraiser_2.SelectedIndex = -1
        'cbo1stAppraiser_3.SelectedIndex = -1
        'cbo1stAppraiser_4.SelectedIndex = -1
        'cbo1stAppraiser_5.SelectedIndex = -1
        'cbo1stAppraiser_6.SelectedIndex = -1
        'cbo1stAppraiser_7.SelectedIndex = -1
        'cbo1stAppraiser_8.SelectedIndex = -1
        'cbo1stAppraiser_9.SelectedIndex = -1
        'cbo1stAppraiser_10.SelectedIndex = -1

        cbo2ndAppraiser.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo2ndAppraiser.Items.Add(lstItem)

        For iLoop = 1 To iRate * 10
            If iLoop >= 10 Then
                lstItem = New ListItem
                lstItem.Text = iLoop
                lstItem.Value = iLoop
                cbo2ndAppraiser.Items.Add(lstItem)
            End If
        Next

        lblPoint.Text = "(Points from 1 ~ " & iRate.ToString & ")"

        'cbo2ndAppraiser.SelectedIndex = -1

        If cboAppraisal.SelectedValue = "200812" Then
            Appraisal200812()
        Else
            LatestAppraisal()
        End If



    End Sub

    Private Sub Appraisal200812()

        Dim lstItem As New ListItem

        cboAppraisee_1.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_1.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_1.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_1.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_1.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = mstrRating4_Desc1
        'lstItem.Value = "4"
        'cboAppraisee_1.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = mstrRating5_Desc1
        'lstItem.Value = "5"
        'cboAppraisee_1.Items.Add(lstItem)

        '=====================================================================

        cboAppraisee_2.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_2.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_2.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_2.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_2.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cboAppraisee_2.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cboAppraisee_2.Items.Add(lstItem)

        '=====================================================================

        cboAppraisee_3.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_3.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_3.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_3.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_3.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cboAppraisee_3.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cboAppraisee_3.Items.Add(lstItem)

        '=====================================================================

        cboAppraisee_4.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_4.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_4.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_4.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_4.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cboAppraisee_4.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cboAppraisee_4.Items.Add(lstItem)

        '=====================================================================

        cboAppraisee_5.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_5.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_5.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_5.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_5.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cboAppraisee_5.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cboAppraisee_5.Items.Add(lstItem)


        '=====================================================================

        cboAppraisee_6.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_6.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_6.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_6.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_6.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cboAppraisee_6.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cboAppraisee_6.Items.Add(lstItem)

        '=====================================================================

        cboAppraisee_7.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_7.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_7.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_7.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_7.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cboAppraisee_7.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cboAppraisee_7.Items.Add(lstItem)

        '=====================================================================

        cboAppraisee_8.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_8.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_8.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_8.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_8.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cboAppraisee_8.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cboAppraisee_8.Items.Add(lstItem)

        '=====================================================================

        cboAppraisee_9.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_9.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_9.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_9.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_9.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cboAppraisee_9.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cboAppraisee_9.Items.Add(lstItem)

        '=====================================================================

        cboAppraisee_10.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_10.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_10.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_10.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_10.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cboAppraisee_10.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cboAppraisee_10.Items.Add(lstItem)

        '========================================================================

        cbo1stAppraiser_1.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_1.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_1.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_1.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_1.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cbo1stAppraiser_1.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cbo1stAppraiser_1.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_2.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_2.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_2.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_2.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_2.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cbo1stAppraiser_2.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cbo1stAppraiser_2.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_3.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_3.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_3.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_3.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_3.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cbo1stAppraiser_3.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cbo1stAppraiser_3.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_4.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_4.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_4.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_4.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_4.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cbo1stAppraiser_4.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cbo1stAppraiser_4.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_5.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_5.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_5.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_5.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_5.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cbo1stAppraiser_5.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cbo1stAppraiser_5.Items.Add(lstItem)


        '=====================================================================

        cbo1stAppraiser_6.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_6.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_6.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_6.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_6.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cbo1stAppraiser_6.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cbo1stAppraiser_6.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_7.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_7.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_7.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_7.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_7.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cbo1stAppraiser_7.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cbo1stAppraiser_7.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_8.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_8.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_8.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_8.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_8.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cbo1stAppraiser_8.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cbo1stAppraiser_8.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_9.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_9.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_9.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_9.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_9.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cbo1stAppraiser_9.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cbo1stAppraiser_9.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_10.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_10.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_10.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_10.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_10.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "4"
        'lstItem.Value = "4"
        'cbo1stAppraiser_10.Items.Add(lstItem)

        'lstItem = New ListItem
        'lstItem.Text = "5"
        'lstItem.Value = "5"
        'cbo1stAppraiser_10.Items.Add(lstItem)


    End Sub

    Private Sub LatestAppraisal()

        Dim lstItem As New ListItem

        cboAppraisee_1.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_1.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_1.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_1.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_1.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cboAppraisee_1.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cboAppraisee_1.Items.Add(lstItem)

        '=====================================================================

        cboAppraisee_2.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_2.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_2.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_2.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_2.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cboAppraisee_2.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cboAppraisee_2.Items.Add(lstItem)

        '=====================================================================

        cboAppraisee_3.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_3.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_3.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_3.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_3.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cboAppraisee_3.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cboAppraisee_3.Items.Add(lstItem)

        '=====================================================================

        cboAppraisee_4.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_4.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_4.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_4.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_4.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cboAppraisee_4.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cboAppraisee_4.Items.Add(lstItem)

        '=====================================================================

        cboAppraisee_5.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_5.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_5.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_5.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_5.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cboAppraisee_5.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cboAppraisee_5.Items.Add(lstItem)

        '=====================================================================

        cboAppraisee_6.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_6.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_6.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_6.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_6.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cboAppraisee_6.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cboAppraisee_6.Items.Add(lstItem)

        '=====================================================================

        cboAppraisee_7.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_7.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_7.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_7.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_7.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cboAppraisee_7.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cboAppraisee_7.Items.Add(lstItem)

        '=====================================================================

        cboAppraisee_8.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_8.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_8.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_8.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_8.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cboAppraisee_8.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cboAppraisee_8.Items.Add(lstItem)

        '=====================================================================

        cboAppraisee_9.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_9.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_9.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_9.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_9.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cboAppraisee_9.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cboAppraisee_9.Items.Add(lstItem)

        '=====================================================================

        cboAppraisee_10.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cboAppraisee_10.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cboAppraisee_10.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cboAppraisee_10.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cboAppraisee_10.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cboAppraisee_10.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cboAppraisee_10.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_1.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_1.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_1.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_1.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_1.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cbo1stAppraiser_1.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cbo1stAppraiser_1.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_2.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_2.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_2.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_2.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_2.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cbo1stAppraiser_2.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cbo1stAppraiser_2.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_3.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_3.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_3.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_3.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_3.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cbo1stAppraiser_3.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cbo1stAppraiser_3.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_4.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_4.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_4.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_4.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_4.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cbo1stAppraiser_4.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cbo1stAppraiser_4.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_5.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_5.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_5.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_5.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_5.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cbo1stAppraiser_5.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cbo1stAppraiser_5.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_6.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_6.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_6.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_6.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_6.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cbo1stAppraiser_6.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cbo1stAppraiser_6.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_7.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_7.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_7.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_7.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_7.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cbo1stAppraiser_7.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cbo1stAppraiser_7.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_8.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_8.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_8.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_8.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_8.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cbo1stAppraiser_8.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cbo1stAppraiser_8.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_9.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_9.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_9.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_9.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_9.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cbo1stAppraiser_9.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cbo1stAppraiser_9.Items.Add(lstItem)

        '=====================================================================

        cbo1stAppraiser_10.Items.Clear()

        lstItem = New ListItem
        lstItem.Text = ""
        lstItem.Value = ""
        cbo1stAppraiser_10.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating1_Desc1
        lstItem.Value = "1"
        cbo1stAppraiser_10.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating2_Desc1
        lstItem.Value = "2"
        cbo1stAppraiser_10.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating3_Desc1
        lstItem.Value = "3"
        cbo1stAppraiser_10.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating4_Desc1
        lstItem.Value = "4"
        cbo1stAppraiser_10.Items.Add(lstItem)

        lstItem = New ListItem
        lstItem.Text = mstrRating5_Desc1
        lstItem.Value = "5"
        cbo1stAppraiser_10.Items.Add(lstItem)



    End Sub
    Private Sub Initialise()

        cboAppraisee_1.SelectedValue = ""
        cboAppraisee_2.SelectedValue = ""
        cboAppraisee_3.SelectedValue = ""
        cboAppraisee_4.SelectedValue = ""
        cboAppraisee_5.SelectedValue = ""
        cboAppraisee_6.SelectedValue = ""
        cboAppraisee_7.SelectedValue = ""
        cboAppraisee_8.SelectedValue = ""
        cboAppraisee_9.SelectedValue = ""
        cboAppraisee_10.SelectedValue = ""

        cbo1stAppraiser_1.SelectedValue = ""
        cbo1stAppraiser_2.SelectedValue = ""
        cbo1stAppraiser_3.SelectedValue = ""
        cbo1stAppraiser_4.SelectedValue = ""
        cbo1stAppraiser_5.SelectedValue = ""
        cbo1stAppraiser_6.SelectedValue = ""
        cbo1stAppraiser_7.SelectedValue = ""
        cbo1stAppraiser_8.SelectedValue = ""
        cbo1stAppraiser_9.SelectedValue = ""
        cbo1stAppraiser_10.SelectedValue = ""

        cbo2ndAppraiser.SelectedValue = ""

    End Sub

    Protected Sub optPersonal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPersonal.CheckedChanged

        cboSubordinate.Items.Clear()
        cboSubordinate.Enabled = False

        cbo1stAppraiser_1.Enabled = False
        cbo1stAppraiser_2.Enabled = False
        cbo1stAppraiser_3.Enabled = False
        cbo1stAppraiser_4.Enabled = False
        cbo1stAppraiser_5.Enabled = False
        cbo1stAppraiser_6.Enabled = False
        cbo1stAppraiser_7.Enabled = False
        cbo1stAppraiser_8.Enabled = False
        cbo1stAppraiser_9.Enabled = False
        cbo1stAppraiser_10.Enabled = False

        cbo2ndAppraiser.Enabled = False

        mbError = False
        lblError.Text = ""
        lblError.Visible = False


    End Sub

    Protected Sub optSubordinate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSubordinate.CheckedChanged

        If hfLevel.Value = "3" Then
            cboAppraisee_1.Enabled = True
            cboAppraisee_2.Enabled = True
            cboAppraisee_3.Enabled = True
            cboAppraisee_4.Enabled = True
            cboAppraisee_5.Enabled = True
            cboAppraisee_6.Enabled = True
            cboAppraisee_7.Enabled = True
            cboAppraisee_8.Enabled = True
            cboAppraisee_9.Enabled = True
            cboAppraisee_10.Enabled = True
        Else
            cboAppraisee_1.Enabled = False
            cboAppraisee_2.Enabled = False
            cboAppraisee_3.Enabled = False
            cboAppraisee_4.Enabled = False
            cboAppraisee_5.Enabled = False
            cboAppraisee_6.Enabled = False
            cboAppraisee_7.Enabled = False
            cboAppraisee_8.Enabled = False
            cboAppraisee_9.Enabled = False
            cboAppraisee_10.Enabled = False
        End If
        

        GetSubordinate()

        mstrEmpID = cboSubordinate.SelectedValue

        mbError = False
        lblError.Text = ""
        lblError.Visible = False

    End Sub


    Private Sub lnkLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogout.Click

        Session("EmpID") = ""
        Response.Redirect("../Global/SessionTimeOut.aspx", True)

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


    Private Sub cboAppraisal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAppraisal.SelectedIndexChanged

        Dim iloop As Integer

        If optSubordinate.Checked Then
            GetSubordinate()

            For iloop = 0 To cboSubordinate.Items.Count - 1
                If cboSubordinate.Items(iloop).Value = mstrEmpID Then
                    cboSubordinate.SelectedValue = mstrEmpID
                End If
            Next

            mstrEmpID = cboSubordinate.SelectedValue
        End If

        mbError = False
        lblError.Text = ""
        lblError.Visible = False

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

    Private Sub StaffSkills_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload

        mMyConnection.Close()

    End Sub

    Private Sub lnkReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkReport.Click

        Response.Redirect("AppraisalReport.aspx", True)

    End Sub

    Protected Sub btnLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLock.Click

        mMyCommand.CommandText = "sp_ap_InsUpdDel_EmpAppraisalPeriod '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
                                 "" & "','" & "" & "','" & "" & "','" & "" & "','" & "" & "','" & "" & "','" & "DEL" & "'"
        mMyDataReader = mMyCommand.ExecuteReader
        mMyDataReader.Close()

        If btnLock.Text = "Lock Appraisee's Session" Then
            mMyCommand.CommandText = "sp_ap_InsUpdDel_EmpAppraisalPeriod '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
                                "" & "','" & "" & "','" & "" & "','" & "" & "','" & "" & "','" & "" & "','" & "ADD2" & "'"
            mMyDataReader = mMyCommand.ExecuteReader
            mMyDataReader.Close()
        End If

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