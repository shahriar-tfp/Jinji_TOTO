Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class PAGES_EAPPRAISAL_STAFFCOMMENT
    Inherits System.Web.UI.Page

    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting, MyKajima As New clsKajimaWeb
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Dim mMyConnection As New System.Data.SqlClient.SqlConnection
    Dim mMyCommand As New System.Data.SqlClient.SqlCommand
    Dim mMyDataReader As System.Data.SqlClient.SqlDataReader

    Dim mstrEmpID As String

    Dim mbError As Boolean

    Dim mstrCurrentPeriod As String

    Dim mstrDuties_1, mstrDuties_2, mstrDuties_3 As String

    Dim mstrTarget_1_1, mstrTarget_1_2, mstrTarget_1_3, mstrTarget_1_4, mstrTarget_1_5, mstrTarget_1_6, mstrTarget_1_7, mstrTarget_1_8, mstrWeighting_1 As String
    Dim mstrTarget_2_1, mstrTarget_2_2, mstrTarget_2_3, mstrTarget_2_4, mstrTarget_2_5, mstrTarget_2_6, mstrTarget_2_7, mstrTarget_2_8, mstrWeighting_2 As String
    Dim mstrTarget_3_1, mstrTarget_3_2, mstrTarget_3_3, mstrTarget_3_4, mstrTarget_3_5, mstrTarget_3_6, mstrTarget_3_7, mstrTarget_3_8, mstrWeighting_3 As String

    Dim mstrAchievement_1_1, mstrAchievement_1_2, mstrAchievement_1_3, mstrAchievement_1_4, mstrAchievement_1_5, mstrAchievement_1_6, mstrAchievement_1_7, mstrAchievement_1_8, mstrPoints_1 As String
    Dim mstrAchievement_2_1, mstrAchievement_2_2, mstrAchievement_2_3, mstrAchievement_2_4, mstrAchievement_2_5, mstrAchievement_2_6, mstrAchievement_2_7, mstrAchievement_2_8, mstrPoints_2 As String
    Dim mstrAchievement_3_1, mstrAchievement_3_2, mstrAchievement_3_3, mstrAchievement_3_4, mstrAchievement_3_5, mstrAchievement_3_6, mstrAchievement_3_7, mstrAchievement_3_8, mstrPoints_3 As String

    Dim mstrDifficulty1_1stAppraiser, mstrPoints1_1stAppraiser, mstrDifficulty2_1stAppraiser, mstrPoints2_1stAppraiser, mstrDifficulty3_1stAppraiser, mstrPoints3_1stAppraiser As String
    Dim mstrComment_Target1, mstrComment_Target2, mstrComment_Target3, mstrComment_Target4, mstrComment_Target5, mstrComment_Target6, mstrComment_Target7, mstrComment_Target8 As String
    Dim mstrComment_Achievement1, mstrComment_Achievement2, mstrComment_Achievement3, mstrComment_Achievement4, mstrComment_Achievement5, mstrComment_Achievement6, mstrComment_Achievement7, mstrComment_Achievement8 As String

    Dim mstrDifficulty1_2ndAppraiser, mstrPoints1_2ndAppraiser, mstrDifficulty2_2ndAppraiser, mstrPoints2_2ndAppraiser, mstrDifficulty3_2ndAppraiser, mstrPoints3_2ndAppraiser As String

    Dim mstrOverall_1st_1, mstrOverall_1st_2, mstrOverall_1st_3 As String
    Dim mstrFeedback_1st_1, mstrFeedback_1st_2, mstrFeedback_1st_3 As String
    Dim mstrOverall_2nd_1, mstrOverall_2nd_2, mstrOverall_2nd_3 As String

    Dim mstrAchievement1, mstrSkill1, mstrTotal1, mstrRating1 As String
    Dim mstrAchievement2, mstrSkill2, mstrTotal2, mstrRating2 As String
    Dim mstrTrainDesc1, mstrTrainDesc2, mstrTrainDesc3, mstrTrainDesc4, mstrTrainDesc5, mstrTrainDesc6, mstrTrainDesc7 As String
    Dim changeTrain As String


    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        mMyConnection.ConnectionString = mySQL.GetConnectionString()
        mMyConnection.Open()
        mMyCommand.Connection = mMyConnection

    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim lstItem As New ListItem

        If Session("EmpID") = "" Then
            If Page.ClientQueryString = "" Then
                Response.Redirect("../Global/SessionTimeOut.aspx", True)
            Else
                Session("EmpID") = MyKajima.DecryptedText(Strings.Mid(Page.Request.RawUrl, Strings.InStr(Page.Request.RawUrl, "?") + 1))
            End If

        End If


        If Session("EmpID") <> "" Then
            mMyCommand.CommandText = "select empname from is_empmaster where empid = '" & Session("EmpID") & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            If mMyDataReader.Read Then
                Session("username") = mMyDataReader("empname")
            End If
            mMyDataReader.Close()

            If Not IsPostBack Then
                If Not bIsAppraisal() Then
                    Response.Redirect("AccessDenied.aspx", True)
                End If
            End If

            Session.Timeout = 60

            Session("PreviousPage") = Request.RawUrl

            lblUser.Text = Session("username")

            If optSubordinate.Checked Then
                mstrEmpID = cboSubordinate.SelectedValue
            Else
                mstrEmpID = Session("EmpID")
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
            End If

            If cboPoints1_Appraisee.Items.Count = 0 Then

                mMyCommand.CommandText = "sp_ap_Sel_AppraisalSkills_Rating '" & cboAppraisal.SelectedValue & "'"
                mMyDataReader = mMyCommand.ExecuteReader

                lstItem = New ListItem
                Dim lstItem1 = New ListItem
                Dim lstItem2 = New ListItem
                Dim lstItem3 = New ListItem
                Dim lstItem4 = New ListItem
                Dim lstItem5 = New ListItem
                Dim lstItem6 = New ListItem
                Dim lstItem7 = New ListItem
                Dim lstItem8 = New ListItem

                lstItem.Text = ""
                lstItem.Value = ""
                lstItem1.Text = ""
                lstItem1.Value = ""
                lstItem2.Text = ""
                lstItem2.Value = ""
                lstItem3.Text = ""
                lstItem3.Value = ""
                lstItem4.Text = ""
                lstItem4.Value = ""
                lstItem5.Text = ""
                lstItem5.Value = ""
                lstItem6.Text = ""
                lstItem6.Value = ""
                lstItem7.Text = ""
                lstItem7.Value = ""
                lstItem8.Text = ""
                lstItem8.Value = ""
                cboPoints1_Appraisee.Items.Add(lstItem)
                cboPoints1_1stAppraiser.Items.Add(lstItem1)
                cboPoints1_2ndAppraiser.Items.Add(lstItem2)
                cboPoints2_Appraisee.Items.Add(lstItem3)
                cboPoints2_1stAppraiser.Items.Add(lstItem4)
                cboPoints2_2ndAppraiser.Items.Add(lstItem5)
                cboPoints3_Appraisee.Items.Add(lstItem6)
                cboPoints3_1stAppraiser.Items.Add(lstItem7)
                cboPoints3_2ndAppraiser.Items.Add(lstItem8)
                While mMyDataReader.Read()
                    lstItem = New ListItem
                    lstItem1 = New ListItem
                    lstItem2 = New ListItem
                    lstItem3 = New ListItem
                    lstItem4 = New ListItem
                    lstItem5 = New ListItem
                    lstItem6 = New ListItem
                    lstItem7 = New ListItem
                    lstItem8 = New ListItem

                    lstItem.Text = mMyDataReader("Desc1")
                    lstItem.Value = mMyDataReader("Seq")
                    lstItem1.Text = mMyDataReader("Desc1")
                    lstItem1.Value = mMyDataReader("Seq")
                    lstItem2.Text = mMyDataReader("Desc1")
                    lstItem2.Value = mMyDataReader("Seq")
                    lstItem3.Text = mMyDataReader("Desc1")
                    lstItem3.Value = mMyDataReader("Seq")
                    lstItem4.Text = mMyDataReader("Desc1")
                    lstItem4.Value = mMyDataReader("Seq")
                    lstItem5.Text = mMyDataReader("Desc1")
                    lstItem5.Value = mMyDataReader("Seq")
                    lstItem6.Text = mMyDataReader("Desc1")
                    lstItem6.Value = mMyDataReader("Seq")
                    lstItem7.Text = mMyDataReader("Desc1")
                    lstItem7.Value = mMyDataReader("Seq")
                    lstItem8.Text = mMyDataReader("Desc1")
                    lstItem8.Value = mMyDataReader("Seq")
                    cboPoints1_Appraisee.Items.Add(lstItem)
                    cboPoints1_1stAppraiser.Items.Add(lstItem1)
                    cboPoints1_2ndAppraiser.Items.Add(lstItem2)
                    cboPoints2_Appraisee.Items.Add(lstItem3)
                    cboPoints2_1stAppraiser.Items.Add(lstItem4)
                    cboPoints2_2ndAppraiser.Items.Add(lstItem5)
                    cboPoints3_Appraisee.Items.Add(lstItem6)
                    cboPoints3_1stAppraiser.Items.Add(lstItem7)
                    cboPoints3_2ndAppraiser.Items.Add(lstItem8)
                End While
                mMyDataReader.Close()
                lstItem = Nothing
                lstItem1 = Nothing
                lstItem2 = Nothing
                lstItem3 = Nothing
                lstItem4 = Nothing
                lstItem5 = Nothing
                lstItem6 = Nothing
                lstItem7 = Nothing
                lstItem8 = Nothing
            End If

            Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", "display()", True)
        End If

    End Sub
    Private Function bIsAppraisal() As Boolean

        bIsAppraisal = False

        Dim strCurrentPeriod As String = ""

        mMyCommand.CommandText = "sp_ap_sel_AppraisalPeriod '" & "" & "','" & "" & "','" & "CurrentPeriod" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            strCurrentPeriod = mMyDataReader("period")
        End If
        mMyDataReader.Close()

        mMyCommand.CommandText = "sp_ap_Sel_AppraisalGroup '" & strCurrentPeriod & "','" & Session("EmpID") & "','" & Session("EmpID") & "','" & _
                                "Permission" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            bIsAppraisal = True
        Else
            bIsAppraisal = False
        End If
        mMyDataReader.Close()



    End Function

    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete


        Initialize()

        '----------------------------------------------------------------------------------------------

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
            ElseIf hfLevel.Value = 3 Then
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

        mMyCommand.CommandText = "sp_ap_Sel_EmpMaster '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'"
        'Response.Write(mMyCommand.CommandText)
        mMyDataReader = mMyCommand.ExecuteReader



        If mMyDataReader.Read Then
            lblDept.Text = mMyDataReader("department")
            lblDesignation.Text = mMyDataReader("designation")
            lblLevel.Text = mMyDataReader("level")
            lblYOS.Text = mMyDataReader("yos")
        End If
        mMyDataReader.Close()

        '----------------------------------------------------------------------------------------------

        mMyCommand.CommandText = "sp_ap_Sel_EmpAppraisal_Duties '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & "Duties" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            txtDuties_1.Text = mMyDataReader("Duties_1")
            txtDuties_2.Text = mMyDataReader("Duties_2")
            txtDuties_3.Text = mMyDataReader("Duties_3")
        End If
        mMyDataReader.Close()

        '----------------------------------------------------------------------------------------------

        mMyCommand.CommandText = "sp_ap_Sel_EmpAppraisal_Target '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'," & _
                                 "1" & ",'" & "Target" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            txtTarget1_1.Text = mMyDataReader("target_1")
            txtTarget1_2.Text = mMyDataReader("target_2")
            txtTarget1_3.Text = mMyDataReader("target_3")
            txtTarget1_4.Text = mMyDataReader("target_4")
            txtTarget1_5.Text = mMyDataReader("target_5")
            txtTarget1_6.Text = mMyDataReader("target_6")
            txtTarget1_7.Text = mMyDataReader("target_7")
            txtTarget1_8.Text = mMyDataReader("target_8")
            cboWeighting_1.SelectedValue = mMyDataReader("weighting")

            txtAchievement1_1.Text = mMyDataReader("achievement_1")
            txtAchievement1_2.Text = mMyDataReader("achievement_2")
            txtAchievement1_3.Text = mMyDataReader("achievement_3")
            txtAchievement1_4.Text = mMyDataReader("achievement_4")
            txtAchievement1_5.Text = mMyDataReader("achievement_5")
            txtAchievement1_6.Text = mMyDataReader("achievement_6")
            txtAchievement1_7.Text = mMyDataReader("achievement_7")
            txtAchievement1_8.Text = mMyDataReader("achievement_8")

            cboPoints1_Appraisee.SelectedValue = mMyDataReader("points")
        End If
        mMyDataReader.Close()

        '----------------------------------------------------------------------------------------------

        mMyCommand.CommandText = "sp_ap_Sel_Appraiser_Points " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'," & _
                                "1" & ",'" & "Points" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            If hfLevel.Value > 0 Then cboPoints1_1stAppraiser.SelectedValue = mMyDataReader("points")

            cboDifficulty1_1stAppraiser.SelectedValue = mMyDataReader("difficulty")
        End If
        mMyDataReader.Close()


        '----------------------------------------------------------------------------------------------

        If hfLevel.Value >= 2 Then
            mMyCommand.CommandText = "sp_ap_Sel_Appraiser_Points " & "2" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'," & _
                                 "1" & ",'" & "Points" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            If mMyDataReader.Read Then
                cboPoints1_2ndAppraiser.SelectedValue = mMyDataReader("points")
                cboDifficulty1_2ndAppraiser.SelectedValue = mMyDataReader("difficulty")
            End If
            mMyDataReader.Close()
        End If

        '----------------------------------------------------------------------------------------------

        mMyCommand.CommandText = "sp_ap_Sel_EmpAppraisal_Target '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'," & _
                                "2" & ",'" & "Target" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            txtTarget2_1.Text = mMyDataReader("target_1")
            txtTarget2_2.Text = mMyDataReader("target_2")
            txtTarget2_3.Text = mMyDataReader("target_3")
            txtTarget2_4.Text = mMyDataReader("target_4")
            txtTarget2_5.Text = mMyDataReader("target_5")
            txtTarget2_6.Text = mMyDataReader("target_6")
            txtTarget2_7.Text = mMyDataReader("target_7")
            txtTarget2_8.Text = mMyDataReader("target_8")
            cboWeighting_2.SelectedValue = mMyDataReader("weighting")

            txtAchievement2_1.Text = mMyDataReader("achievement_1")
            txtAchievement2_2.Text = mMyDataReader("achievement_2")
            txtAchievement2_3.Text = mMyDataReader("achievement_3")
            txtAchievement2_4.Text = mMyDataReader("achievement_4")
            txtAchievement2_5.Text = mMyDataReader("achievement_5")
            txtAchievement2_6.Text = mMyDataReader("achievement_6")
            txtAchievement2_7.Text = mMyDataReader("achievement_7")
            txtAchievement2_8.Text = mMyDataReader("achievement_8")

            cboPoints2_Appraisee.SelectedValue = mMyDataReader("points")
        End If
        mMyDataReader.Close()
        '----------------------------------------------------------------------------------------------

        mMyCommand.CommandText = "sp_ap_Sel_Appraiser_Points " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'," & _
                                 "2" & ",'" & "Points" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            If hfLevel.Value > 0 Then cboPoints2_1stAppraiser.SelectedValue = mMyDataReader("points")

            cboDifficulty2_1stAppraiser.SelectedValue = mMyDataReader("difficulty")
        End If
        mMyDataReader.Close()


        '----------------------------------------------------------------------------------------------

        If hfLevel.Value >= 2 Then
            mMyCommand.CommandText = "sp_ap_Sel_Appraiser_Points " & "2" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'," & _
                                 "2" & ",'" & "Points" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            If mMyDataReader.Read Then
                cboPoints2_2ndAppraiser.SelectedValue = mMyDataReader("points")
                cboDifficulty2_2ndAppraiser.SelectedValue = mMyDataReader("difficulty")
            End If
            mMyDataReader.Close()
        End If

        '----------------------------------------------------------------------------------------------

        mMyCommand.CommandText = "sp_ap_Sel_EmpAppraisal_Target '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'," & _
                                "3" & ",'" & "Target" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            txtTarget3_1.Text = mMyDataReader("target_1")
            txtTarget3_2.Text = mMyDataReader("target_2")
            txtTarget3_3.Text = mMyDataReader("target_3")
            txtTarget3_4.Text = mMyDataReader("target_4")
            txtTarget3_5.Text = mMyDataReader("target_5")
            txtTarget3_6.Text = mMyDataReader("target_6")
            txtTarget3_7.Text = mMyDataReader("target_7")
            txtTarget3_8.Text = mMyDataReader("target_8")
            cboWeighting_3.SelectedValue = mMyDataReader("weighting")

            txtAchievement3_1.Text = mMyDataReader("achievement_1")
            txtAchievement3_2.Text = mMyDataReader("achievement_2")
            txtAchievement3_3.Text = mMyDataReader("achievement_3")
            txtAchievement3_4.Text = mMyDataReader("achievement_4")
            txtAchievement3_5.Text = mMyDataReader("achievement_5")
            txtAchievement3_6.Text = mMyDataReader("achievement_6")
            txtAchievement3_7.Text = mMyDataReader("achievement_7")
            txtAchievement3_8.Text = mMyDataReader("achievement_8")

            cboPoints3_Appraisee.SelectedValue = mMyDataReader("points")
        End If
        mMyDataReader.Close()

        '----------------------------------------------------------------------------------------------

        mMyCommand.CommandText = "sp_ap_Sel_Appraiser_Points " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'," & _
                              "3" & ",'" & "Points" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            If hfLevel.Value > 0 Then cboPoints3_1stAppraiser.SelectedValue = mMyDataReader("points")

            cboDifficulty3_1stAppraiser.SelectedValue = mMyDataReader("difficulty")
        End If
        mMyDataReader.Close()

        '----------------------------------------------------------------------------------------------

        If hfLevel.Value >= 2 Then
            mMyCommand.CommandText = "sp_ap_Sel_Appraiser_Points " & "2" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'," & _
                                 "3" & ",'" & "Points" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            If mMyDataReader.Read Then
                cboPoints3_2ndAppraiser.SelectedValue = mMyDataReader("points")
                cboDifficulty3_2ndAppraiser.SelectedValue = mMyDataReader("difficulty")
            End If
            mMyDataReader.Close()
        End If

        '----------------------------------------------------------------------------------------------

        mMyCommand.CommandText = "sp_ap_Sel_Appraiser_Comment " & "1" & ",'" & "T" & "','" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & "Comment" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            txtCommentT_1.Text = mMyDataReader("comment_1")
            txtCommentT_2.Text = mMyDataReader("comment_2")
            txtCommentT_3.Text = mMyDataReader("comment_3")
            txtCommentT_4.Text = mMyDataReader("comment_4")
            txtCommentT_5.Text = mMyDataReader("comment_5")
            txtCommentT_6.Text = mMyDataReader("comment_6")
            txtCommentT_7.Text = mMyDataReader("comment_7")
            txtCommentT_8.Text = mMyDataReader("comment_8")
        End If
        mMyDataReader.Close()

        '----------------------------------------------------------------------------------------------

        mMyCommand.CommandText = "sp_ap_Sel_Appraiser_Training '" & cboAppraisal.SelectedValue & "','" & "1" & "','" & mstrEmpID & "','" & "Feedback" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            txtFeedBack_1.Text = mMyDataReader("Feedback_1")
            txtFeedBack_2.Text = mMyDataReader("Feedback_2")
            txtFeedBack_3.Text = mMyDataReader("Feedback_3")
        End If
        If mstrFeedback_1st_1 <> "" Or mstrFeedback_1st_2 <> "" Or mstrFeedback_1st_3 <> "" Or changeTrain = "Y" Then
            txtFeedBack_1.Text = mstrFeedback_1st_1
            txtFeedBack_2.Text = mstrFeedback_1st_2
            txtFeedBack_3.Text = mstrFeedback_1st_3
        End If
        mMyDataReader.Close()


        If hfLevel.Value > 0 Then
            mMyCommand.CommandText = "sp_ap_Sel_Appraiser_Comment " & "1" & ",'" & "A" & "','" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & "Comment" & "'"
            mMyDataReader = mMyCommand.ExecuteReader



            If mMyDataReader.Read Then
                txtCommentA_1.Text = mMyDataReader("comment_1")
                txtCommentA_2.Text = mMyDataReader("comment_2")
                txtCommentA_3.Text = mMyDataReader("comment_3")
                txtCommentA_4.Text = mMyDataReader("comment_4")
                txtCommentA_5.Text = mMyDataReader("comment_5")
                txtCommentA_6.Text = mMyDataReader("comment_6")
                txtCommentA_7.Text = mMyDataReader("comment_7")
                txtCommentA_8.Text = mMyDataReader("comment_8")
            End If
            mMyDataReader.Close()

            mMyCommand.CommandText = "sp_ap_Sel_Appraisal_OverallComment " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & "Comment" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            If mMyDataReader.Read Then
                txtOverallComment_1st_1.Text = mMyDataReader("comment_1")
                txtOverallComment_1st_2.Text = mMyDataReader("comment_2")
                txtOverallComment_1st_3.Text = mMyDataReader("comment_3")
            End If
            If mstrOverall_1st_1 <> "" Or mstrOverall_1st_2 <> "" Or mstrOverall_1st_3 <> "" Or changeTrain = "Y" Then
                txtOverallComment_1st_1.Text = mstrOverall_1st_1
                txtOverallComment_1st_2.Text = mstrOverall_1st_2
                txtOverallComment_1st_3.Text = mstrOverall_1st_3
            End If
            mMyDataReader.Close()

            mMyCommand.CommandText = "sp_ap_Sel_Appraiser_Rating '" & cboAppraisal.SelectedValue & "','" & "1" & "','" & mstrEmpID & "','" & "RATING" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            If mMyDataReader.Read Then
                txtTolAchievement_1.Text = mMyDataReader("Achievement")
                txtTolSkill_1.Text = mMyDataReader("Skill")
                txtTolScore_1.Text = mMyDataReader("Total")
                cboRanking_1.SelectedValue = mMyDataReader("Rating")
            End If
            If mstrRating1 <> "" Or changeTrain = "Y" Then
                cboRanking_1.SelectedValue = mstrRating1
            End If
            mMyDataReader.Close()

            mMyCommand.CommandText = "sp_ap_Sel_Appraiser_Training '" & cboAppraisal.SelectedValue & "','" & "1" & "','" & mstrEmpID & "','" & "TRAINING" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            If mMyDataReader.Read Then
                ddlTrain1.SelectedValue = mMyDataReader("Desc1")
                txtTrain1_no.Text = mMyDataReader("Desc2")
                ddlTrain2.SelectedValue = mMyDataReader("Desc3")
                ddlTypeTrain.SelectedValue = mMyDataReader("Desc4")
                txtotherTrain.Text = mMyDataReader("Desc5")
                txtTrainTopic.Text = mMyDataReader("Desc6")
                txtMonitor.Text = mMyDataReader("Desc7")
            End If
            If mstrTrainDesc1 <> "" Or mstrTrainDesc2 <> "" Or mstrTrainDesc3 <> "" Or mstrTrainDesc4 <> "" Or mstrTrainDesc5 <> "" Or mstrTrainDesc6 <> "" Or mstrTrainDesc7 <> "" Or changeTrain = "Y" Then
                ddlTrain1.SelectedValue = mstrTrainDesc1
                txtTrain1_no.Text = mstrTrainDesc2
                ddlTrain2.SelectedValue = mstrTrainDesc3
                ddlTypeTrain.SelectedValue = mstrTrainDesc4
                txtotherTrain.Text = mstrTrainDesc5
                txtTrainTopic.Text = mstrTrainDesc6
                txtMonitor.Text = mstrTrainDesc7
            End If
            mMyDataReader.Close()

        End If


        cboPoints1_Appraisee.SelectedValue = mstrPoints_1

        If hfLevel.Value >= 2 Then
            mMyCommand.CommandText = "sp_ap_Sel_Appraisal_OverallComment " & "2" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & "Comment" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            If mMyDataReader.Read Then
                txtOverallComment_2nd_1.Text = mMyDataReader("comment_1")
                txtOverallComment_2nd_2.Text = mMyDataReader("comment_2")
                txtOverallComment_2nd_3.Text = mMyDataReader("comment_3")
            End If
            If mstrOverall_2nd_1 <> "" Or mstrOverall_2nd_2 <> "" Or mstrOverall_2nd_3 <> "" Or changeTrain = "Y" Then
                txtOverallComment_2nd_1.Text = mstrOverall_2nd_1
                txtOverallComment_2nd_2.Text = mstrOverall_2nd_2
                txtOverallComment_2nd_3.Text = mstrOverall_2nd_3
            End If
            mMyDataReader.Close()

            mMyCommand.CommandText = "sp_ap_Sel_Appraiser_Rating '" & cboAppraisal.SelectedValue & "','" & "2" & "','" & mstrEmpID & "','" & "RATING" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            If mMyDataReader.Read Then
                txtTolAchievement_2.Text = mMyDataReader("Achievement")
                txtTolSkill_2.Text = mMyDataReader("Skill")
                txtTolScore_2.Text = mMyDataReader("Total")
                cboRanking_2.SelectedValue = mMyDataReader("Rating")

            End If
            If mstrRating2 <> "" Or changeTrain = "Y" Then
                cboRanking_2.SelectedValue = mstrRating2
            End If
            mMyDataReader.Close()
        End If

        '----------------------------------------------------------------------------------------------

        ControlStatus()

        '----------------------------------------------------------------------------------------------

        If mbError Then
            If optPersonal.Checked Then
                If txtTarget1_1.ReadOnly = False Then
                    txtTarget1_1.Text = mstrTarget_1_1
                    txtTarget1_2.Text = mstrTarget_1_2
                    txtTarget1_3.Text = mstrTarget_1_3
                    txtTarget1_4.Text = mstrTarget_1_4
                    txtTarget1_5.Text = mstrTarget_1_5
                    txtTarget1_6.Text = mstrTarget_1_6
                    txtTarget1_7.Text = mstrTarget_1_7
                    txtTarget1_8.Text = mstrTarget_1_8
                    cboWeighting_1.SelectedValue = mstrWeighting_1

                    txtTarget2_1.Text = mstrTarget_2_1
                    txtTarget2_2.Text = mstrTarget_2_2
                    txtTarget2_3.Text = mstrTarget_2_3
                    txtTarget2_4.Text = mstrTarget_2_4
                    txtTarget2_5.Text = mstrTarget_2_5
                    txtTarget2_6.Text = mstrTarget_2_6
                    txtTarget2_7.Text = mstrTarget_2_7
                    txtTarget2_8.Text = mstrTarget_2_8
                    cboWeighting_2.SelectedValue = mstrWeighting_2

                    txtTarget3_1.Text = mstrTarget_3_1
                    txtTarget3_2.Text = mstrTarget_3_2
                    txtTarget3_3.Text = mstrTarget_3_3
                    txtTarget3_4.Text = mstrTarget_3_4
                    txtTarget3_5.Text = mstrTarget_3_5
                    txtTarget3_6.Text = mstrTarget_3_6
                    txtTarget3_7.Text = mstrTarget_3_7
                    txtTarget3_8.Text = mstrTarget_3_8
                    cboWeighting_3.SelectedValue = mstrWeighting_3
                ElseIf txtAchievement1_1.ReadOnly = False Then
                    txtDuties_1.Text = mstrDuties_1
                    txtDuties_2.Text = mstrDuties_2
                    txtDuties_3.Text = mstrDuties_3

                    txtAchievement1_1.Text = mstrAchievement_1_1
                    txtAchievement1_2.Text = mstrAchievement_1_2
                    txtAchievement1_3.Text = mstrAchievement_1_3
                    txtAchievement1_4.Text = mstrAchievement_1_4
                    txtAchievement1_5.Text = mstrAchievement_1_5
                    txtAchievement1_6.Text = mstrAchievement_1_6
                    txtAchievement1_7.Text = mstrAchievement_1_7
                    txtAchievement1_8.Text = mstrAchievement_1_8
                    cboPoints1_Appraisee.SelectedValue = mstrPoints_1

                    txtAchievement2_1.Text = mstrAchievement_2_1
                    txtAchievement2_2.Text = mstrAchievement_2_2
                    txtAchievement2_3.Text = mstrAchievement_2_3
                    txtAchievement2_4.Text = mstrAchievement_2_4
                    txtAchievement2_5.Text = mstrAchievement_2_5
                    txtAchievement2_6.Text = mstrAchievement_2_6
                    txtAchievement2_7.Text = mstrAchievement_2_7
                    txtAchievement2_8.Text = mstrAchievement_2_8
                    cboPoints2_Appraisee.SelectedValue = mstrPoints_2

                    txtAchievement3_1.Text = mstrAchievement_3_1
                    txtAchievement3_2.Text = mstrAchievement_3_2
                    txtAchievement3_3.Text = mstrAchievement_3_3
                    txtAchievement3_4.Text = mstrAchievement_3_4
                    txtAchievement3_5.Text = mstrAchievement_3_5
                    txtAchievement3_6.Text = mstrAchievement_3_6
                    txtAchievement3_7.Text = mstrAchievement_3_7
                    txtAchievement3_8.Text = mstrAchievement_3_8
                    cboPoints3_Appraisee.SelectedValue = mstrPoints_3
                End If
            Else
                If hfLevel.Value = 1 Then
                    cboDifficulty1_1stAppraiser.SelectedValue = mstrDifficulty1_1stAppraiser
                    cboPoints1_1stAppraiser.SelectedValue = mstrPoints1_1stAppraiser

                    cboDifficulty2_1stAppraiser.SelectedValue = mstrDifficulty2_1stAppraiser
                    cboPoints2_1stAppraiser.SelectedValue = mstrPoints2_1stAppraiser

                    cboDifficulty3_1stAppraiser.SelectedValue = mstrDifficulty3_1stAppraiser
                    cboPoints3_1stAppraiser.SelectedValue = mstrPoints3_1stAppraiser

                    txtCommentT_1.Text = mstrComment_Target1
                    txtCommentT_2.Text = mstrComment_Target2
                    txtCommentT_3.Text = mstrComment_Target3
                    txtCommentT_4.Text = mstrComment_Target4
                    txtCommentT_5.Text = mstrComment_Target5
                    txtCommentT_6.Text = mstrComment_Target6
                    txtCommentT_7.Text = mstrComment_Target7
                    txtCommentT_8.Text = mstrComment_Target8

                    txtCommentA_1.Text = mstrComment_Achievement1
                    txtCommentA_2.Text = mstrComment_Achievement2
                    txtCommentA_3.Text = mstrComment_Achievement3
                    txtCommentA_4.Text = mstrComment_Achievement4
                    txtCommentA_5.Text = mstrComment_Achievement5
                    txtCommentA_6.Text = mstrComment_Achievement6
                    txtCommentA_7.Text = mstrComment_Achievement7
                    txtCommentA_8.Text = mstrComment_Achievement8

                    txtOverallComment_1st_1.Text = mstrOverall_1st_1
                    txtOverallComment_1st_2.Text = mstrOverall_1st_2
                    txtOverallComment_1st_3.Text = mstrOverall_1st_3

                    txtFeedBack_1.Text = mstrFeedback_1st_1
                    txtFeedBack_2.Text = mstrFeedback_1st_2
                    txtFeedBack_3.Text = mstrFeedback_1st_3

                    txtTolAchievement_1.Text = mstrAchievement1
                    txtTolSkill_1.Text = mstrSkill1
                    txtTolScore_1.Text = mstrTotal1
                    cboRanking_1.SelectedValue = mstrRating1


                    ddlTrain1.SelectedValue = mstrTrainDesc1
                    txtTrain1_no.Text = mstrTrainDesc2
                    ddlTrain2.SelectedValue = mstrTrainDesc3
                    ddlTypeTrain.SelectedValue = mstrTrainDesc4
                    txtotherTrain.Text = mstrTrainDesc5
                    txtTrainTopic.Text = mstrTrainDesc6
                    txtMonitor.Text = mstrTrainDesc7

                ElseIf hfLevel.Value = 2 Then
                    cboDifficulty1_2ndAppraiser.SelectedValue = mstrDifficulty1_2ndAppraiser
                    cboPoints1_2ndAppraiser.SelectedValue = mstrPoints1_2ndAppraiser

                    cboDifficulty2_2ndAppraiser.SelectedValue = mstrDifficulty2_2ndAppraiser
                    cboPoints2_2ndAppraiser.SelectedValue = mstrPoints2_2ndAppraiser

                    cboDifficulty3_2ndAppraiser.SelectedValue = mstrDifficulty3_2ndAppraiser
                    cboPoints3_2ndAppraiser.SelectedValue = mstrPoints3_2ndAppraiser

                    txtOverallComment_2nd_1.Text = mstrOverall_2nd_1
                    txtOverallComment_2nd_2.Text = mstrOverall_2nd_2
                    txtOverallComment_2nd_3.Text = mstrOverall_2nd_3

                    txtTolAchievement_2.Text = mstrAchievement2
                    txtTolSkill_2.Text = mstrSkill2
                    txtTolScore_2.Text = mstrTotal2
                    cboRanking_2.SelectedValue = mstrRating2
                ElseIf hfLevel.Value = 3 Then
                    If txtTarget1_1.ReadOnly = False Then
                        txtTarget1_1.Text = mstrTarget_1_1
                        txtTarget1_2.Text = mstrTarget_1_2
                        txtTarget1_3.Text = mstrTarget_1_3
                        txtTarget1_4.Text = mstrTarget_1_4
                        txtTarget1_5.Text = mstrTarget_1_5
                        txtTarget1_6.Text = mstrTarget_1_6
                        txtTarget1_7.Text = mstrTarget_1_7
                        txtTarget1_8.Text = mstrTarget_1_8
                        cboWeighting_1.SelectedValue = mstrWeighting_1

                        txtTarget2_1.Text = mstrTarget_2_1
                        txtTarget2_2.Text = mstrTarget_2_2
                        txtTarget2_3.Text = mstrTarget_2_3
                        txtTarget2_4.Text = mstrTarget_2_4
                        txtTarget2_5.Text = mstrTarget_2_5
                        txtTarget2_6.Text = mstrTarget_2_6
                        txtTarget2_7.Text = mstrTarget_2_7
                        txtTarget2_8.Text = mstrTarget_2_8
                        cboWeighting_2.SelectedValue = mstrWeighting_2

                        txtTarget3_1.Text = mstrTarget_3_1
                        txtTarget3_2.Text = mstrTarget_3_2
                        txtTarget3_3.Text = mstrTarget_3_3
                        txtTarget3_4.Text = mstrTarget_3_4
                        txtTarget3_5.Text = mstrTarget_3_5
                        txtTarget3_6.Text = mstrTarget_3_6
                        txtTarget3_7.Text = mstrTarget_3_7
                        txtTarget3_8.Text = mstrTarget_3_8
                        cboWeighting_3.SelectedValue = mstrWeighting_3
                    ElseIf txtAchievement1_1.ReadOnly = False Then
                        txtDuties_1.Text = mstrDuties_1
                        txtDuties_2.Text = mstrDuties_2
                        txtDuties_3.Text = mstrDuties_3

                        txtAchievement1_1.Text = mstrAchievement_1_1
                        txtAchievement1_2.Text = mstrAchievement_1_2
                        txtAchievement1_3.Text = mstrAchievement_1_3
                        txtAchievement1_4.Text = mstrAchievement_1_4
                        txtAchievement1_5.Text = mstrAchievement_1_5
                        txtAchievement1_6.Text = mstrAchievement_1_6
                        txtAchievement1_7.Text = mstrAchievement_1_7
                        txtAchievement1_8.Text = mstrAchievement_1_8
                        cboPoints1_Appraisee.SelectedValue = mstrPoints_1

                        txtAchievement2_1.Text = mstrAchievement_2_1
                        txtAchievement2_2.Text = mstrAchievement_2_2
                        txtAchievement2_3.Text = mstrAchievement_2_3
                        txtAchievement2_4.Text = mstrAchievement_2_4
                        txtAchievement2_5.Text = mstrAchievement_2_5
                        txtAchievement2_6.Text = mstrAchievement_2_6
                        txtAchievement2_7.Text = mstrAchievement_2_7
                        txtAchievement2_8.Text = mstrAchievement_2_8
                        cboPoints2_Appraisee.SelectedValue = mstrPoints_2

                        txtAchievement3_1.Text = mstrAchievement_3_1
                        txtAchievement3_2.Text = mstrAchievement_3_2
                        txtAchievement3_3.Text = mstrAchievement_3_3
                        txtAchievement3_4.Text = mstrAchievement_3_4
                        txtAchievement3_5.Text = mstrAchievement_3_5
                        txtAchievement3_6.Text = mstrAchievement_3_6
                        txtAchievement3_7.Text = mstrAchievement_3_7
                        txtAchievement3_8.Text = mstrAchievement_3_8
                        cboPoints3_Appraisee.SelectedValue = mstrPoints_3
                    End If
                    cboDifficulty1_1stAppraiser.SelectedValue = mstrDifficulty1_1stAppraiser
                    cboPoints1_1stAppraiser.SelectedValue = mstrPoints1_1stAppraiser

                    cboDifficulty2_1stAppraiser.SelectedValue = mstrDifficulty2_1stAppraiser
                    cboPoints2_1stAppraiser.SelectedValue = mstrPoints2_1stAppraiser

                    cboDifficulty3_1stAppraiser.SelectedValue = mstrDifficulty3_1stAppraiser
                    cboPoints3_1stAppraiser.SelectedValue = mstrPoints3_1stAppraiser

                    txtCommentT_1.Text = mstrComment_Target1
                    txtCommentT_2.Text = mstrComment_Target2
                    txtCommentT_3.Text = mstrComment_Target3
                    txtCommentT_4.Text = mstrComment_Target4
                    txtCommentT_5.Text = mstrComment_Target5
                    txtCommentT_6.Text = mstrComment_Target6
                    txtCommentT_7.Text = mstrComment_Target7
                    txtCommentT_8.Text = mstrComment_Target8

                    txtCommentA_1.Text = mstrComment_Achievement1
                    txtCommentA_2.Text = mstrComment_Achievement2
                    txtCommentA_3.Text = mstrComment_Achievement3
                    txtCommentA_4.Text = mstrComment_Achievement4
                    txtCommentA_5.Text = mstrComment_Achievement5
                    txtCommentA_6.Text = mstrComment_Achievement6
                    txtCommentA_7.Text = mstrComment_Achievement7
                    txtCommentA_8.Text = mstrComment_Achievement8

                    txtOverallComment_1st_1.Text = mstrOverall_1st_1
                    txtOverallComment_1st_2.Text = mstrOverall_1st_2
                    txtOverallComment_1st_3.Text = mstrOverall_1st_3

                    txtFeedBack_1.Text = mstrFeedback_1st_1
                    txtFeedBack_2.Text = mstrFeedback_1st_2
                    txtFeedBack_3.Text = mstrFeedback_1st_3

                    txtTolAchievement_1.Text = mstrAchievement1
                    txtTolSkill_1.Text = mstrSkill1
                    txtTolScore_1.Text = mstrTotal1
                    cboRanking_1.SelectedValue = mstrRating1


                    ddlTrain1.SelectedValue = mstrTrainDesc1
                    txtTrain1_no.Text = mstrTrainDesc2
                    ddlTrain2.SelectedValue = mstrTrainDesc3
                    ddlTypeTrain.SelectedValue = mstrTrainDesc4
                    txtotherTrain.Text = mstrTrainDesc5
                    txtTrainTopic.Text = mstrTrainDesc6
                    txtMonitor.Text = mstrTrainDesc7

                    cboDifficulty1_2ndAppraiser.SelectedValue = mstrDifficulty1_2ndAppraiser
                    cboPoints1_2ndAppraiser.SelectedValue = mstrPoints1_2ndAppraiser

                    cboDifficulty2_2ndAppraiser.SelectedValue = mstrDifficulty2_2ndAppraiser
                    cboPoints2_2ndAppraiser.SelectedValue = mstrPoints2_2ndAppraiser

                    cboDifficulty3_2ndAppraiser.SelectedValue = mstrDifficulty3_2ndAppraiser
                    cboPoints3_2ndAppraiser.SelectedValue = mstrPoints3_2ndAppraiser

                    txtOverallComment_2nd_1.Text = mstrOverall_2nd_1
                    txtOverallComment_2nd_2.Text = mstrOverall_2nd_2
                    txtOverallComment_2nd_3.Text = mstrOverall_2nd_3

                    txtTolAchievement_2.Text = mstrAchievement2
                    txtTolSkill_2.Text = mstrSkill2
                    txtTolScore_2.Text = mstrTotal2
                    cboRanking_2.SelectedValue = mstrRating2
                End If
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
    Private Sub Initialize()

        lblDept.Text = ""
        lblDesignation.Text = ""
        lblLevel.Text = ""
        lblYOS.Text = ""

        txtDuties_1.Text = ""
        txtDuties_2.Text = ""
        txtDuties_3.Text = ""

        '-----------------------------------------------------

        txtTarget1_1.Text = ""
        txtTarget1_2.Text = ""
        txtTarget1_3.Text = ""
        txtTarget1_4.Text = ""
        txtTarget1_5.Text = ""
        txtTarget1_6.Text = ""
        txtTarget1_7.Text = ""
        txtTarget1_8.Text = ""

        txtAchievement1_1.Text = ""
        txtAchievement1_2.Text = ""
        txtAchievement1_3.Text = ""
        txtAchievement1_4.Text = ""
        txtAchievement1_5.Text = ""
        txtAchievement1_6.Text = ""
        txtAchievement1_7.Text = ""
        txtAchievement1_8.Text = ""

        cboWeighting_1.SelectedValue = ""
        cboPoints1_Appraisee.SelectedValue = ""
        cboPoints1_1stAppraiser.SelectedValue = ""
        cboDifficulty1_1stAppraiser.SelectedValue = ""
        cboPoints1_2ndAppraiser.SelectedValue = ""
        cboDifficulty1_2ndAppraiser.SelectedValue = ""

        '-----------------------------------------------------

        txtTarget2_1.Text = ""
        txtTarget2_2.Text = ""
        txtTarget2_3.Text = ""
        txtTarget2_4.Text = ""
        txtTarget2_5.Text = ""
        txtTarget2_6.Text = ""
        txtTarget2_7.Text = ""
        txtTarget2_8.Text = ""

        txtAchievement2_1.Text = ""
        txtAchievement2_2.Text = ""
        txtAchievement2_3.Text = ""
        txtAchievement2_4.Text = ""
        txtAchievement2_5.Text = ""
        txtAchievement2_6.Text = ""
        txtAchievement2_7.Text = ""
        txtAchievement2_8.Text = ""

        cboWeighting_2.SelectedValue = ""
        cboPoints2_Appraisee.SelectedValue = ""
        cboPoints2_1stAppraiser.SelectedValue = ""
        cboDifficulty2_1stAppraiser.SelectedValue = ""
        cboPoints2_2ndAppraiser.SelectedValue = ""
        cboDifficulty2_2ndAppraiser.SelectedValue = ""

        '-----------------------------------------------------

        txtTarget3_1.Text = ""
        txtTarget3_2.Text = ""
        txtTarget3_3.Text = ""
        txtTarget3_4.Text = ""
        txtTarget3_5.Text = ""
        txtTarget3_6.Text = ""
        txtTarget3_7.Text = ""
        txtTarget3_8.Text = ""

        txtAchievement3_1.Text = ""
        txtAchievement3_2.Text = ""
        txtAchievement3_3.Text = ""
        txtAchievement3_4.Text = ""
        txtAchievement3_5.Text = ""
        txtAchievement3_6.Text = ""
        txtAchievement3_7.Text = ""
        txtAchievement3_8.Text = ""

        cboWeighting_3.SelectedValue = ""
        cboPoints3_Appraisee.SelectedValue = ""
        cboPoints3_1stAppraiser.SelectedValue = ""
        cboDifficulty3_1stAppraiser.SelectedValue = ""
        cboPoints3_2ndAppraiser.SelectedValue = ""
        cboDifficulty3_2ndAppraiser.SelectedValue = ""

        '-----------------------------------------------------

        txtCommentT_1.Text = ""
        txtCommentT_2.Text = ""
        txtCommentT_3.Text = ""
        txtCommentT_4.Text = ""
        txtCommentT_5.Text = ""
        txtCommentT_6.Text = ""
        txtCommentT_7.Text = ""
        txtCommentT_8.Text = ""

        txtCommentA_1.Text = ""
        txtCommentA_2.Text = ""
        txtCommentA_3.Text = ""
        txtCommentA_4.Text = ""
        txtCommentA_5.Text = ""
        txtCommentA_6.Text = ""
        txtCommentA_7.Text = ""
        txtCommentA_8.Text = ""

        '-----------------------------------------------------

        txtOverallComment_1st_1.Text = ""
        txtOverallComment_1st_2.Text = ""
        txtOverallComment_1st_3.Text = ""

        txtFeedBack_1.Text = ""
        txtFeedBack_2.Text = ""
        txtFeedBack_3.Text = ""

        txtOverallComment_2nd_1.Text = ""
        txtOverallComment_2nd_2.Text = ""
        txtOverallComment_2nd_3.Text = ""

    End Sub
    Private Sub ControlStatus()

        Dim bIsAppraisee As Boolean = False
        Dim bOpenAppraisee, bOpen1stAppraiser, bOpen2ndAppraiser As Boolean
        Dim strAppraisalStatus As String = ""

        bOpenAppraisee = False
        bOpen1stAppraiser = False
        bOpen2ndAppraiser = False

        mMyCommand.CommandText = "sp_ap_Sel_AppraisalGroup '" & cboAppraisal.SelectedValue & "','" & Session("EmpID") & "','" & mstrEmpID & "','" & _
                                     "Personal" & "'"
        mMyDataReader = mMyCommand.ExecuteReader



        If mMyDataReader.Read Then bIsAppraisee = True

        mMyDataReader.Close()

        mMyCommand.CommandText = "sp_ap_sel_AppraisalPeriod '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & "Status" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read Then
            strAppraisalStatus = mMyDataReader("Status")
        End If
        mMyDataReader.Close()

        If strAppraisalStatus.ToUpper = "CURRENT" Then
            mMyCommand.CommandText = "sp_ap_sel_AppraisalPeriod '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & "Cutoff" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            If mMyDataReader.Read Then
                bOpenAppraisee = mMyDataReader("Open_Appraisee")
                bOpen1stAppraiser = mMyDataReader("Open_1stAppraiser")
                bOpen2ndAppraiser = mMyDataReader("Open_2ndAppraiser")
            End If
            mMyDataReader.Close()
        ElseIf strAppraisalStatus.ToUpper = "NEXT" Then
            If mstrCurrentPeriod <> "" Then
                'mMyCommand.CommandText = "sp_ap_sel_AppraisalPeriod '" & mstrEmpID & "','" & mstrCurrentPeriod & "','" & "Cutoff" & "'"
                mMyCommand.CommandText = "sp_ap_sel_AppraisalPeriod '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & "Cutoff" & "'"
                mMyDataReader = mMyCommand.ExecuteReader

                If mMyDataReader.Read Then
                    bOpenAppraisee = mMyDataReader("Open_Appraisee")
                    bOpen1stAppraiser = mMyDataReader("Open_1stAppraiser")
                    bOpen2ndAppraiser = mMyDataReader("Open_2ndAppraiser")
                End If
                mMyDataReader.Close()


            End If
        End If

        txtDuties_1.ReadOnly = True
        txtDuties_2.ReadOnly = True
        txtDuties_3.ReadOnly = True

        txtDuties_1.BackColor = Drawing.Color.Honeydew
        txtDuties_2.BackColor = Drawing.Color.Honeydew
        txtDuties_3.BackColor = Drawing.Color.Honeydew

        txtDuties_1.BorderWidth = 0
        txtDuties_2.BorderWidth = 0
        txtDuties_3.BorderWidth = 0

        '-----------------------------------------------------

        txtTarget1_1.ReadOnly = True
        txtTarget1_2.ReadOnly = True
        txtTarget1_3.ReadOnly = True
        txtTarget1_4.ReadOnly = True
        txtTarget1_5.ReadOnly = True
        txtTarget1_6.ReadOnly = True
        txtTarget1_7.ReadOnly = True
        txtTarget1_8.ReadOnly = True

        txtAchievement1_1.ReadOnly = True
        txtAchievement1_2.ReadOnly = True
        txtAchievement1_3.ReadOnly = True
        txtAchievement1_4.ReadOnly = True
        txtAchievement1_5.ReadOnly = True
        txtAchievement1_6.ReadOnly = True
        txtAchievement1_7.ReadOnly = True
        txtAchievement1_8.ReadOnly = True

        txtTarget1_1.BackColor = Drawing.Color.Honeydew
        txtTarget1_2.BackColor = Drawing.Color.Honeydew
        txtTarget1_3.BackColor = Drawing.Color.Honeydew
        txtTarget1_4.BackColor = Drawing.Color.Honeydew
        txtTarget1_5.BackColor = Drawing.Color.Honeydew
        txtTarget1_6.BackColor = Drawing.Color.Honeydew
        txtTarget1_7.BackColor = Drawing.Color.Honeydew
        txtTarget1_8.BackColor = Drawing.Color.Honeydew

        txtAchievement1_1.BackColor = Drawing.Color.Honeydew
        txtAchievement1_2.BackColor = Drawing.Color.Honeydew
        txtAchievement1_3.BackColor = Drawing.Color.Honeydew
        txtAchievement1_4.BackColor = Drawing.Color.Honeydew
        txtAchievement1_5.BackColor = Drawing.Color.Honeydew
        txtAchievement1_6.BackColor = Drawing.Color.Honeydew
        txtAchievement1_7.BackColor = Drawing.Color.Honeydew
        txtAchievement1_8.BackColor = Drawing.Color.Honeydew


        txtTarget1_1.BorderWidth = 0
        txtTarget1_2.BorderWidth = 0
        txtTarget1_3.BorderWidth = 0
        txtTarget1_4.BorderWidth = 0
        txtTarget1_5.BorderWidth = 0
        txtTarget1_6.BorderWidth = 0
        txtTarget1_7.BorderWidth = 0
        txtTarget1_8.BorderWidth = 0

        txtAchievement1_1.BorderWidth = 0
        txtAchievement1_2.BorderWidth = 0
        txtAchievement1_3.BorderWidth = 0
        txtAchievement1_4.BorderWidth = 0
        txtAchievement1_5.BorderWidth = 0
        txtAchievement1_6.BorderWidth = 0
        txtAchievement1_7.BorderWidth = 0
        txtAchievement1_8.BorderWidth = 0

        cboWeighting_1.Enabled = False
        cboWeighting_1.BackColor = Drawing.Color.Honeydew

        cboPoints1_Appraisee.Enabled = False
        cboPoints1_1stAppraiser.Enabled = False
        cboDifficulty1_1stAppraiser.Enabled = False
        cboPoints1_2ndAppraiser.Enabled = False
        cboDifficulty1_2ndAppraiser.Enabled = False

        cboPoints1_Appraisee.BackColor = Drawing.Color.Honeydew
        cboPoints1_1stAppraiser.BackColor = Drawing.Color.Lavender
        cboDifficulty1_1stAppraiser.BackColor = Drawing.Color.Lavender
        cboPoints1_2ndAppraiser.BackColor = Drawing.Color.Lavender
        cboDifficulty1_2ndAppraiser.BackColor = Drawing.Color.Lavender

        '-----------------------------------------------------

        txtTarget2_1.ReadOnly = True
        txtTarget2_2.ReadOnly = True
        txtTarget2_3.ReadOnly = True
        txtTarget2_4.ReadOnly = True
        txtTarget2_5.ReadOnly = True
        txtTarget2_6.ReadOnly = True
        txtTarget2_7.ReadOnly = True
        txtTarget2_8.ReadOnly = True

        txtAchievement2_1.ReadOnly = True
        txtAchievement2_2.ReadOnly = True
        txtAchievement2_3.ReadOnly = True
        txtAchievement2_4.ReadOnly = True
        txtAchievement2_5.ReadOnly = True
        txtAchievement2_6.ReadOnly = True
        txtAchievement2_7.ReadOnly = True
        txtAchievement2_8.ReadOnly = True

        txtTarget2_1.BackColor = Drawing.Color.Honeydew
        txtTarget2_2.BackColor = Drawing.Color.Honeydew
        txtTarget2_3.BackColor = Drawing.Color.Honeydew
        txtTarget2_4.BackColor = Drawing.Color.Honeydew
        txtTarget2_5.BackColor = Drawing.Color.Honeydew
        txtTarget2_6.BackColor = Drawing.Color.Honeydew
        txtTarget2_7.BackColor = Drawing.Color.Honeydew
        txtTarget2_8.BackColor = Drawing.Color.Honeydew

        txtAchievement2_1.BackColor = Drawing.Color.Honeydew
        txtAchievement2_2.BackColor = Drawing.Color.Honeydew
        txtAchievement2_3.BackColor = Drawing.Color.Honeydew
        txtAchievement2_4.BackColor = Drawing.Color.Honeydew
        txtAchievement2_5.BackColor = Drawing.Color.Honeydew
        txtAchievement2_6.BackColor = Drawing.Color.Honeydew
        txtAchievement2_7.BackColor = Drawing.Color.Honeydew
        txtAchievement2_8.BackColor = Drawing.Color.Honeydew

        txtTarget2_1.BorderWidth = 0
        txtTarget2_2.BorderWidth = 0
        txtTarget2_3.BorderWidth = 0
        txtTarget2_4.BorderWidth = 0
        txtTarget2_5.BorderWidth = 0
        txtTarget2_6.BorderWidth = 0
        txtTarget2_7.BorderWidth = 0
        txtTarget2_8.BorderWidth = 0

        txtAchievement2_1.BorderWidth = 0
        txtAchievement2_2.BorderWidth = 0
        txtAchievement2_3.BorderWidth = 0
        txtAchievement2_4.BorderWidth = 0
        txtAchievement2_5.BorderWidth = 0
        txtAchievement2_6.BorderWidth = 0
        txtAchievement2_7.BorderWidth = 0
        txtAchievement2_8.BorderWidth = 0


        cboWeighting_2.Enabled = False
        cboPoints2_Appraisee.Enabled = False
        cboPoints2_1stAppraiser.Enabled = False
        cboDifficulty2_1stAppraiser.Enabled = False
        cboPoints2_2ndAppraiser.Enabled = False
        cboDifficulty2_2ndAppraiser.Enabled = False

        cboWeighting_2.BackColor = Drawing.Color.Honeydew
        cboPoints2_Appraisee.BackColor = Drawing.Color.Honeydew
        cboPoints2_1stAppraiser.BackColor = Drawing.Color.Lavender
        cboDifficulty2_1stAppraiser.BackColor = Drawing.Color.Lavender
        cboPoints2_2ndAppraiser.BackColor = Drawing.Color.Lavender
        cboDifficulty2_2ndAppraiser.BackColor = Drawing.Color.Lavender

        '-----------------------------------------------------

        txtTarget3_1.ReadOnly = True
        txtTarget3_2.ReadOnly = True
        txtTarget3_3.ReadOnly = True
        txtTarget3_4.ReadOnly = True
        txtTarget3_5.ReadOnly = True
        txtTarget3_6.ReadOnly = True
        txtTarget3_7.ReadOnly = True
        txtTarget3_8.ReadOnly = True

        txtAchievement3_1.ReadOnly = True
        txtAchievement3_2.ReadOnly = True
        txtAchievement3_3.ReadOnly = True
        txtAchievement3_4.ReadOnly = True
        txtAchievement3_5.ReadOnly = True
        txtAchievement3_6.ReadOnly = True
        txtAchievement3_7.ReadOnly = True
        txtAchievement3_8.ReadOnly = True

        txtTarget3_1.BackColor = Drawing.Color.Honeydew
        txtTarget3_2.BackColor = Drawing.Color.Honeydew
        txtTarget3_3.BackColor = Drawing.Color.Honeydew
        txtTarget3_4.BackColor = Drawing.Color.Honeydew
        txtTarget3_5.BackColor = Drawing.Color.Honeydew
        txtTarget3_6.BackColor = Drawing.Color.Honeydew
        txtTarget3_7.BackColor = Drawing.Color.Honeydew
        txtTarget3_8.BackColor = Drawing.Color.Honeydew

        txtAchievement3_1.BackColor = Drawing.Color.Honeydew
        txtAchievement3_2.BackColor = Drawing.Color.Honeydew
        txtAchievement3_3.BackColor = Drawing.Color.Honeydew
        txtAchievement3_4.BackColor = Drawing.Color.Honeydew
        txtAchievement3_5.BackColor = Drawing.Color.Honeydew
        txtAchievement3_6.BackColor = Drawing.Color.Honeydew
        txtAchievement3_7.BackColor = Drawing.Color.Honeydew
        txtAchievement3_8.BackColor = Drawing.Color.Honeydew

        txtTarget3_1.BorderWidth = 0
        txtTarget3_2.BorderWidth = 0
        txtTarget3_3.BorderWidth = 0
        txtTarget3_4.BorderWidth = 0
        txtTarget3_5.BorderWidth = 0
        txtTarget3_6.BorderWidth = 0
        txtTarget3_7.BorderWidth = 0
        txtTarget3_8.BorderWidth = 0

        txtAchievement3_1.BorderWidth = 0
        txtAchievement3_2.BorderWidth = 0
        txtAchievement3_3.BorderWidth = 0
        txtAchievement3_4.BorderWidth = 0
        txtAchievement3_5.BorderWidth = 0
        txtAchievement3_6.BorderWidth = 0
        txtAchievement3_7.BorderWidth = 0
        txtAchievement3_8.BorderWidth = 0

        cboWeighting_3.Enabled = False
        cboPoints3_Appraisee.Enabled = False
        cboPoints3_1stAppraiser.Enabled = False
        cboDifficulty3_1stAppraiser.Enabled = False
        cboPoints3_2ndAppraiser.Enabled = False
        cboDifficulty3_2ndAppraiser.Enabled = False

        cboWeighting_3.BackColor = Drawing.Color.Honeydew
        cboPoints3_Appraisee.BackColor = Drawing.Color.Honeydew
        cboPoints3_1stAppraiser.BackColor = Drawing.Color.Lavender
        cboDifficulty3_1stAppraiser.BackColor = Drawing.Color.Lavender
        cboPoints3_2ndAppraiser.BackColor = Drawing.Color.Lavender
        cboDifficulty3_2ndAppraiser.BackColor = Drawing.Color.Lavender

        '-----------------------------------------------------

        txtCommentT_1.ReadOnly = True
        txtCommentT_2.ReadOnly = True
        txtCommentT_3.ReadOnly = True
        txtCommentT_4.ReadOnly = True
        txtCommentT_5.ReadOnly = True
        txtCommentT_6.ReadOnly = True
        txtCommentT_7.ReadOnly = True
        txtCommentT_8.ReadOnly = True

        txtCommentA_1.ReadOnly = True
        txtCommentA_2.ReadOnly = True
        txtCommentA_3.ReadOnly = True
        txtCommentA_4.ReadOnly = True
        txtCommentA_5.ReadOnly = True
        txtCommentA_6.ReadOnly = True
        txtCommentA_7.ReadOnly = True
        txtCommentA_8.ReadOnly = True

        txtCommentT_1.BackColor = Drawing.Color.Lavender
        txtCommentT_2.BackColor = Drawing.Color.Lavender
        txtCommentT_3.BackColor = Drawing.Color.Lavender
        txtCommentT_4.BackColor = Drawing.Color.Lavender
        txtCommentT_5.BackColor = Drawing.Color.Lavender
        txtCommentT_6.BackColor = Drawing.Color.Lavender
        txtCommentT_7.BackColor = Drawing.Color.Lavender
        txtCommentT_8.BackColor = Drawing.Color.Lavender

        txtCommentA_1.BackColor = Drawing.Color.Lavender
        txtCommentA_2.BackColor = Drawing.Color.Lavender
        txtCommentA_3.BackColor = Drawing.Color.Lavender
        txtCommentA_4.BackColor = Drawing.Color.Lavender
        txtCommentA_5.BackColor = Drawing.Color.Lavender
        txtCommentA_6.BackColor = Drawing.Color.Lavender
        txtCommentA_7.BackColor = Drawing.Color.Lavender
        txtCommentA_8.BackColor = Drawing.Color.Lavender



        txtCommentT_1.BorderWidth = 0
        txtCommentT_2.BorderWidth = 0
        txtCommentT_3.BorderWidth = 0
        txtCommentT_4.BorderWidth = 0
        txtCommentT_5.BorderWidth = 0
        txtCommentT_6.BorderWidth = 0
        txtCommentT_7.BorderWidth = 0
        txtCommentT_8.BorderWidth = 0

        txtCommentA_1.BorderWidth = 0
        txtCommentA_2.BorderWidth = 0
        txtCommentA_3.BorderWidth = 0
        txtCommentA_4.BorderWidth = 0
        txtCommentA_5.BorderWidth = 0
        txtCommentA_6.BorderWidth = 0
        txtCommentA_7.BorderWidth = 0
        txtCommentA_8.BorderWidth = 0

        txtOverallComment_1st_1.ReadOnly = True
        txtOverallComment_1st_2.ReadOnly = True
        txtOverallComment_1st_3.ReadOnly = True

        txtOverallComment_1st_1.BackColor = Drawing.Color.Lavender
        txtOverallComment_1st_2.BackColor = Drawing.Color.Lavender
        txtOverallComment_1st_3.BackColor = Drawing.Color.Lavender

        txtOverallComment_1st_1.BorderWidth = 0
        txtOverallComment_1st_2.BorderWidth = 0
        txtOverallComment_1st_3.BorderWidth = 0

        txtfeedback_1.ReadOnly = True
        txtfeedback_2.ReadOnly = True
        txtfeedback_3.ReadOnly = True

        txtfeedback_1.BackColor = Drawing.Color.Lavender
        txtfeedback_2.BackColor = Drawing.Color.Lavender
        txtfeedback_3.BackColor = Drawing.Color.Lavender

        txtfeedback_1.BorderWidth = 0
        txtfeedback_2.BorderWidth = 0
        txtfeedback_3.BorderWidth = 0

        txtTolAchievement_1.ReadOnly = True
        txtTolSkill_1.ReadOnly = True
        txtTolScore_1.ReadOnly = True
        cboRanking_1.Enabled = False

        txtTolAchievement_1.BackColor = Drawing.Color.Lavender
        txtTolSkill_1.BackColor = Drawing.Color.Lavender
        txtTolScore_1.BackColor = Drawing.Color.Lavender
        cboRanking_1.BackColor = Drawing.Color.Lavender

        txtTolAchievement_1.BorderWidth = 0
        txtTolSkill_1.BorderWidth = 0
        txtTolScore_1.BorderWidth = 0


        ddlTrain1.Enabled = False
        txtTrain1_no.ReadOnly = True
        ddlTrain2.enabled = False
        ddlTypeTrain.Enabled = False
        txtotherTrain.ReadOnly = True
        txtTrainTopic.ReadOnly = True
        txtMonitor.ReadOnly = True

        txtTrain1_no.BackColor = Drawing.Color.Lavender
        txtotherTrain.BackColor = Drawing.Color.Lavender
        txtTrainTopic.BackColor = Drawing.Color.Lavender
        txtMonitor.BackColor = Drawing.Color.Lavender
        ddlTrain1.BackColor = Drawing.Color.Lavender
        ddlTrain2.BackColor = Drawing.Color.Lavender
        ddlTypeTrain.BackColor = Drawing.Color.Lavender

        txtTrain1_no.BorderWidth = 0
        txtotherTrain.BorderWidth = 0
        txtTrainTopic.BorderWidth = 0
        txtMonitor.BorderWidth = 0

        txtOverallComment_2nd_1.ReadOnly = True
        txtOverallComment_2nd_2.ReadOnly = True
        txtOverallComment_2nd_3.ReadOnly = True

        txtOverallComment_2nd_1.BackColor = Drawing.Color.Lavender
        txtOverallComment_2nd_2.BackColor = Drawing.Color.Lavender
        txtOverallComment_2nd_3.BackColor = Drawing.Color.Lavender

        txtOverallComment_2nd_1.BorderWidth = 0
        txtOverallComment_2nd_2.BorderWidth = 0
        txtOverallComment_2nd_3.BorderWidth = 0


        txtTolAchievement_2.ReadOnly = True
        txtTolSkill_2.ReadOnly = True
        txtTolScore_2.ReadOnly = True
        cboRanking_2.Enabled = False

        txtTolAchievement_2.BackColor = Drawing.Color.Lavender
        txtTolSkill_2.BackColor = Drawing.Color.Lavender
        txtTolScore_2.BackColor = Drawing.Color.Lavender
        cboRanking_2.BackColor = Drawing.Color.Lavender

        txtTolAchievement_2.BorderWidth = 0
        txtTolSkill_2.BorderWidth = 0
        txtTolScore_2.BorderWidth = 0

        btnOK.Enabled = False

        If strAppraisalStatus.ToUpper = "NEXT" Then

            If optPersonal.Checked And bIsAppraisee And bOpenAppraisee Then
                txtTarget1_1.ReadOnly = False
                txtTarget1_2.ReadOnly = False
                txtTarget1_3.ReadOnly = False
                txtTarget1_4.ReadOnly = False
                txtTarget1_5.ReadOnly = False
                txtTarget1_6.ReadOnly = False
                txtTarget1_7.ReadOnly = False
                txtTarget1_8.ReadOnly = False

                txtTarget1_1.BackColor = Nothing
                txtTarget1_2.BackColor = Nothing
                txtTarget1_3.BackColor = Nothing
                txtTarget1_4.BackColor = Nothing
                txtTarget1_5.BackColor = Nothing
                txtTarget1_6.BackColor = Nothing
                txtTarget1_7.BackColor = Nothing
                txtTarget1_8.BackColor = Nothing

                txtTarget1_1.BorderWidth = Nothing
                txtTarget1_2.BorderWidth = Nothing
                txtTarget1_3.BorderWidth = Nothing
                txtTarget1_4.BorderWidth = Nothing
                txtTarget1_5.BorderWidth = Nothing
                txtTarget1_6.BorderWidth = Nothing
                txtTarget1_7.BorderWidth = Nothing
                txtTarget1_8.BorderWidth = Nothing

                cboWeighting_1.Enabled = True
                cboWeighting_1.BackColor = Nothing

                txtTarget2_1.ReadOnly = False
                txtTarget2_2.ReadOnly = False
                txtTarget2_3.ReadOnly = False
                txtTarget2_4.ReadOnly = False
                txtTarget2_5.ReadOnly = False
                txtTarget2_6.ReadOnly = False
                txtTarget2_7.ReadOnly = False
                txtTarget2_8.ReadOnly = False

                txtTarget2_1.BackColor = Nothing
                txtTarget2_2.BackColor = Nothing
                txtTarget2_3.BackColor = Nothing
                txtTarget2_4.BackColor = Nothing
                txtTarget2_5.BackColor = Nothing
                txtTarget2_6.BackColor = Nothing
                txtTarget2_7.BackColor = Nothing
                txtTarget2_8.BackColor = Nothing

                txtTarget2_1.BorderWidth = Nothing
                txtTarget2_2.BorderWidth = Nothing
                txtTarget2_3.BorderWidth = Nothing
                txtTarget2_4.BorderWidth = Nothing
                txtTarget2_5.BorderWidth = Nothing
                txtTarget2_6.BorderWidth = Nothing
                txtTarget2_7.BorderWidth = Nothing
                txtTarget2_8.BorderWidth = Nothing

                cboWeighting_2.Enabled = True
                cboWeighting_2.BackColor = Nothing

                txtTarget3_1.ReadOnly = False
                txtTarget3_2.ReadOnly = False
                txtTarget3_3.ReadOnly = False
                txtTarget3_4.ReadOnly = False
                txtTarget3_5.ReadOnly = False
                txtTarget3_6.ReadOnly = False
                txtTarget3_7.ReadOnly = False
                txtTarget3_8.ReadOnly = False

                txtTarget3_1.BackColor = Nothing
                txtTarget3_2.BackColor = Nothing
                txtTarget3_3.BackColor = Nothing
                txtTarget3_4.BackColor = Nothing
                txtTarget3_5.BackColor = Nothing
                txtTarget3_6.BackColor = Nothing
                txtTarget3_7.BackColor = Nothing
                txtTarget3_8.BackColor = Nothing

                txtTarget3_1.BorderWidth = Nothing
                txtTarget3_2.BorderWidth = Nothing
                txtTarget3_3.BorderWidth = Nothing
                txtTarget3_4.BorderWidth = Nothing
                txtTarget3_5.BorderWidth = Nothing
                txtTarget3_6.BorderWidth = Nothing
                txtTarget3_7.BorderWidth = Nothing
                txtTarget3_8.BorderWidth = Nothing

                cboWeighting_3.Enabled = True
                cboWeighting_3.BackColor = Nothing

                'btnOK.Enabled = True
            End If

            If Not optPersonal.Checked And hfLevel.Value = 1 And bOpen1stAppraiser Then
                If Val(cboWeighting_1.SelectedValue) > 0 Then
                    cboDifficulty1_1stAppraiser.Enabled = True
                    cboDifficulty1_1stAppraiser.BackColor = Nothing
                End If

                If Val(cboWeighting_2.SelectedValue) > 0 Then
                    cboDifficulty2_1stAppraiser.Enabled = True
                    cboDifficulty2_1stAppraiser.BackColor = Nothing
                End If

                If Val(cboWeighting_3.SelectedValue) > 0 Then
                    cboDifficulty3_1stAppraiser.Enabled = True
                    cboDifficulty3_1stAppraiser.BackColor = Nothing
                End If

                If Val(cboWeighting_1.SelectedValue) > 0 Or Val(cboWeighting_2.SelectedValue) > 0 Or Val(cboWeighting_3.SelectedValue) > 0 Then
                    txtCommentT_1.ReadOnly = False
                    txtCommentT_2.ReadOnly = False
                    txtCommentT_3.ReadOnly = False
                    txtCommentT_4.ReadOnly = False
                    txtCommentT_5.ReadOnly = False
                    txtCommentT_6.ReadOnly = False
                    txtCommentT_7.ReadOnly = False
                    txtCommentT_8.ReadOnly = False

                    txtCommentT_1.BackColor = Nothing
                    txtCommentT_2.BackColor = Nothing
                    txtCommentT_3.BackColor = Nothing
                    txtCommentT_4.BackColor = Nothing
                    txtCommentT_5.BackColor = Nothing
                    txtCommentT_6.BackColor = Nothing
                    txtCommentT_7.BackColor = Nothing
                    txtCommentT_8.BackColor = Nothing

                    txtCommentT_1.BorderWidth = Nothing
                    txtCommentT_2.BorderWidth = Nothing
                    txtCommentT_3.BorderWidth = Nothing
                    txtCommentT_4.BorderWidth = Nothing
                    txtCommentT_5.BorderWidth = Nothing
                    txtCommentT_6.BorderWidth = Nothing
                    txtCommentT_7.BorderWidth = Nothing
                    txtCommentT_8.BorderWidth = Nothing

                    btnOK.Enabled = True
                End If
            End If

            If hfLevel.Value = "3" And Not optPersonal.Checked Then
                txtTarget1_1.ReadOnly = False
                txtTarget1_2.ReadOnly = False
                txtTarget1_3.ReadOnly = False
                txtTarget1_4.ReadOnly = False
                txtTarget1_5.ReadOnly = False
                txtTarget1_6.ReadOnly = False
                txtTarget1_7.ReadOnly = False
                txtTarget1_8.ReadOnly = False

                txtTarget1_1.BackColor = Nothing
                txtTarget1_2.BackColor = Nothing
                txtTarget1_3.BackColor = Nothing
                txtTarget1_4.BackColor = Nothing
                txtTarget1_5.BackColor = Nothing
                txtTarget1_6.BackColor = Nothing
                txtTarget1_7.BackColor = Nothing
                txtTarget1_8.BackColor = Nothing

                txtTarget1_1.BorderWidth = Nothing
                txtTarget1_2.BorderWidth = Nothing
                txtTarget1_3.BorderWidth = Nothing
                txtTarget1_4.BorderWidth = Nothing
                txtTarget1_5.BorderWidth = Nothing
                txtTarget1_6.BorderWidth = Nothing
                txtTarget1_7.BorderWidth = Nothing
                txtTarget1_8.BorderWidth = Nothing

                cboWeighting_1.Enabled = True
                cboWeighting_1.BackColor = Nothing

                txtTarget2_1.ReadOnly = False
                txtTarget2_2.ReadOnly = False
                txtTarget2_3.ReadOnly = False
                txtTarget2_4.ReadOnly = False
                txtTarget2_5.ReadOnly = False
                txtTarget2_6.ReadOnly = False
                txtTarget2_7.ReadOnly = False
                txtTarget2_8.ReadOnly = False

                txtTarget2_1.BackColor = Nothing
                txtTarget2_2.BackColor = Nothing
                txtTarget2_3.BackColor = Nothing
                txtTarget2_4.BackColor = Nothing
                txtTarget2_5.BackColor = Nothing
                txtTarget2_6.BackColor = Nothing
                txtTarget2_7.BackColor = Nothing
                txtTarget2_8.BackColor = Nothing

                txtTarget2_1.BorderWidth = Nothing
                txtTarget2_2.BorderWidth = Nothing
                txtTarget2_3.BorderWidth = Nothing
                txtTarget2_4.BorderWidth = Nothing
                txtTarget2_5.BorderWidth = Nothing
                txtTarget2_6.BorderWidth = Nothing
                txtTarget2_7.BorderWidth = Nothing
                txtTarget2_8.BorderWidth = Nothing

                cboWeighting_2.Enabled = True
                cboWeighting_2.BackColor = Nothing

                txtTarget3_1.ReadOnly = False
                txtTarget3_2.ReadOnly = False
                txtTarget3_3.ReadOnly = False
                txtTarget3_4.ReadOnly = False
                txtTarget3_5.ReadOnly = False
                txtTarget3_6.ReadOnly = False
                txtTarget3_7.ReadOnly = False
                txtTarget3_8.ReadOnly = False

                txtTarget3_1.BackColor = Nothing
                txtTarget3_2.BackColor = Nothing
                txtTarget3_3.BackColor = Nothing
                txtTarget3_4.BackColor = Nothing
                txtTarget3_5.BackColor = Nothing
                txtTarget3_6.BackColor = Nothing
                txtTarget3_7.BackColor = Nothing
                txtTarget3_8.BackColor = Nothing

                txtTarget3_1.BorderWidth = Nothing
                txtTarget3_2.BorderWidth = Nothing
                txtTarget3_3.BorderWidth = Nothing
                txtTarget3_4.BorderWidth = Nothing
                txtTarget3_5.BorderWidth = Nothing
                txtTarget3_6.BorderWidth = Nothing
                txtTarget3_7.BorderWidth = Nothing
                txtTarget3_8.BorderWidth = Nothing

                cboWeighting_3.Enabled = True
                cboWeighting_3.BackColor = Nothing

                txtCommentT_1.ReadOnly = False
                txtCommentT_2.ReadOnly = False
                txtCommentT_3.ReadOnly = False
                txtCommentT_4.ReadOnly = False
                txtCommentT_5.ReadOnly = False
                txtCommentT_6.ReadOnly = False
                txtCommentT_7.ReadOnly = False
                txtCommentT_8.ReadOnly = False

                txtCommentT_1.BackColor = Nothing
                txtCommentT_2.BackColor = Nothing
                txtCommentT_3.BackColor = Nothing
                txtCommentT_4.BackColor = Nothing
                txtCommentT_5.BackColor = Nothing
                txtCommentT_6.BackColor = Nothing
                txtCommentT_7.BackColor = Nothing
                txtCommentT_8.BackColor = Nothing

                txtCommentT_1.BorderWidth = Nothing
                txtCommentT_2.BorderWidth = Nothing
                txtCommentT_3.BorderWidth = Nothing
                txtCommentT_4.BorderWidth = Nothing
                txtCommentT_5.BorderWidth = Nothing
                txtCommentT_6.BorderWidth = Nothing
                txtCommentT_7.BorderWidth = Nothing
                txtCommentT_8.BorderWidth = Nothing

                cboDifficulty1_1stAppraiser.Enabled = True
                cboDifficulty1_1stAppraiser.BackColor = Nothing

                cboDifficulty2_1stAppraiser.Enabled = True
                cboDifficulty2_1stAppraiser.BackColor = Nothing

                cboDifficulty3_1stAppraiser.Enabled = True
                cboDifficulty3_1stAppraiser.BackColor = Nothing

                btnOK.Enabled = True
            End If
        ElseIf strAppraisalStatus.ToUpper = "CURRENT" Then
            If optPersonal.Checked And bIsAppraisee And bOpenAppraisee Then
                txtDuties_1.ReadOnly = False
                txtDuties_2.ReadOnly = False
                txtDuties_3.ReadOnly = False

                txtDuties_1.BackColor = Nothing
                txtDuties_2.BackColor = Nothing
                txtDuties_3.BackColor = Nothing

                txtDuties_1.BorderWidth = Nothing
                txtDuties_2.BorderWidth = Nothing
                txtDuties_3.BorderWidth = Nothing

                txtAchievement1_1.ReadOnly = False
                txtAchievement1_2.ReadOnly = False
                txtAchievement1_3.ReadOnly = False
                txtAchievement1_4.ReadOnly = False
                txtAchievement1_5.ReadOnly = False
                txtAchievement1_6.ReadOnly = False
                txtAchievement1_7.ReadOnly = False
                txtAchievement1_8.ReadOnly = False

                txtAchievement1_1.BackColor = Nothing
                txtAchievement1_2.BackColor = Nothing
                txtAchievement1_3.BackColor = Nothing
                txtAchievement1_4.BackColor = Nothing
                txtAchievement1_5.BackColor = Nothing
                txtAchievement1_6.BackColor = Nothing
                txtAchievement1_7.BackColor = Nothing
                txtAchievement1_8.BackColor = Nothing

                txtAchievement1_1.BorderWidth = Nothing
                txtAchievement1_2.BorderWidth = Nothing
                txtAchievement1_3.BorderWidth = Nothing
                txtAchievement1_4.BorderWidth = Nothing
                txtAchievement1_5.BorderWidth = Nothing
                txtAchievement1_6.BorderWidth = Nothing
                txtAchievement1_7.BorderWidth = Nothing
                txtAchievement1_8.BorderWidth = Nothing

                cboPoints1_Appraisee.Enabled = True
                cboPoints1_Appraisee.BackColor = Nothing

                If cboWeighting_2.SelectedValue <> "" Then

                    txtAchievement2_1.ReadOnly = False
                    txtAchievement2_2.ReadOnly = False
                    txtAchievement2_3.ReadOnly = False
                    txtAchievement2_4.ReadOnly = False
                    txtAchievement2_5.ReadOnly = False
                    txtAchievement2_6.ReadOnly = False
                    txtAchievement2_7.ReadOnly = False
                    txtAchievement2_8.ReadOnly = False

                    txtAchievement2_1.BackColor = Nothing
                    txtAchievement2_2.BackColor = Nothing
                    txtAchievement2_3.BackColor = Nothing
                    txtAchievement2_4.BackColor = Nothing
                    txtAchievement2_5.BackColor = Nothing
                    txtAchievement2_6.BackColor = Nothing
                    txtAchievement2_7.BackColor = Nothing
                    txtAchievement2_8.BackColor = Nothing

                    txtAchievement2_1.BorderWidth = Nothing
                    txtAchievement2_2.BorderWidth = Nothing
                    txtAchievement2_3.BorderWidth = Nothing
                    txtAchievement2_4.BorderWidth = Nothing
                    txtAchievement2_5.BorderWidth = Nothing
                    txtAchievement2_6.BorderWidth = Nothing
                    txtAchievement2_7.BorderWidth = Nothing
                    txtAchievement2_8.BorderWidth = Nothing

                    cboPoints2_Appraisee.Enabled = True
                    cboPoints2_Appraisee.BackColor = Nothing
                End If

                If cboWeighting_3.SelectedValue <> "" Then
                    txtAchievement3_1.ReadOnly = False
                    txtAchievement3_2.ReadOnly = False
                    txtAchievement3_3.ReadOnly = False
                    txtAchievement3_4.ReadOnly = False
                    txtAchievement3_5.ReadOnly = False
                    txtAchievement3_6.ReadOnly = False
                    txtAchievement3_7.ReadOnly = False
                    txtAchievement3_8.ReadOnly = False

                    txtAchievement3_1.BackColor = Nothing
                    txtAchievement3_2.BackColor = Nothing
                    txtAchievement3_3.BackColor = Nothing
                    txtAchievement3_4.BackColor = Nothing
                    txtAchievement3_5.BackColor = Nothing
                    txtAchievement3_6.BackColor = Nothing
                    txtAchievement3_7.BackColor = Nothing
                    txtAchievement3_8.BackColor = Nothing


                    txtAchievement3_1.BorderWidth = Nothing
                    txtAchievement3_2.BorderWidth = Nothing
                    txtAchievement3_3.BorderWidth = Nothing
                    txtAchievement3_4.BorderWidth = Nothing
                    txtAchievement3_5.BorderWidth = Nothing
                    txtAchievement3_6.BorderWidth = Nothing
                    txtAchievement3_7.BorderWidth = Nothing
                    txtAchievement3_8.BorderWidth = Nothing

                    cboPoints3_Appraisee.Enabled = True
                    cboPoints3_Appraisee.BackColor = Nothing
                End If

                'btnOK.Enabled = True
            End If

            If Not optPersonal.Checked Then

                If hfLevel.Value = 1 And bOpen1stAppraiser Then
                    If Val(cboWeighting_1.SelectedValue) > 0 Then
                        cboPoints1_1stAppraiser.Enabled = True
                        cboPoints1_1stAppraiser.BackColor = Nothing
                        cboDifficulty1_1stAppraiser.Enabled = True
                        cboDifficulty1_1stAppraiser.BackColor = Nothing
                    End If

                    If Val(cboWeighting_2.SelectedValue) > 0 Then
                        cboPoints2_1stAppraiser.Enabled = True
                        cboPoints2_1stAppraiser.BackColor = Nothing
                        cboDifficulty2_1stAppraiser.Enabled = True
                        cboDifficulty2_1stAppraiser.BackColor = Nothing
                    End If

                    If Val(cboWeighting_3.SelectedValue) > 0 Then
                        cboPoints3_1stAppraiser.Enabled = True
                        cboPoints3_1stAppraiser.BackColor = Nothing
                        cboDifficulty3_1stAppraiser.Enabled = True
                        cboDifficulty3_1stAppraiser.BackColor = Nothing
                    End If

                    If Val(cboWeighting_1.SelectedValue) > 0 Or Val(cboWeighting_2.SelectedValue) > 0 Or Val(cboWeighting_3.SelectedValue) > 0 Then
                        txtCommentA_1.ReadOnly = False
                        txtCommentA_2.ReadOnly = False
                        txtCommentA_3.ReadOnly = False
                        txtCommentA_4.ReadOnly = False
                        txtCommentA_5.ReadOnly = False
                        txtCommentA_6.ReadOnly = False
                        txtCommentA_7.ReadOnly = False
                        txtCommentA_8.ReadOnly = False

                        txtCommentA_1.BackColor = Nothing
                        txtCommentA_2.BackColor = Nothing
                        txtCommentA_3.BackColor = Nothing
                        txtCommentA_4.BackColor = Nothing
                        txtCommentA_5.BackColor = Nothing
                        txtCommentA_6.BackColor = Nothing
                        txtCommentA_7.BackColor = Nothing
                        txtCommentA_8.BackColor = Nothing

                        txtCommentA_1.BorderWidth = Nothing
                        txtCommentA_2.BorderWidth = Nothing
                        txtCommentA_3.BorderWidth = Nothing
                        txtCommentA_4.BorderWidth = Nothing
                        txtCommentA_5.BorderWidth = Nothing
                        txtCommentA_6.BorderWidth = Nothing
                        txtCommentA_7.BorderWidth = Nothing
                        txtCommentA_8.BorderWidth = Nothing

                        txtOverallComment_1st_1.ReadOnly = False
                        txtOverallComment_1st_2.ReadOnly = False
                        txtOverallComment_1st_3.ReadOnly = False

                        txtOverallComment_1st_1.BackColor = Nothing
                        txtOverallComment_1st_2.BackColor = Nothing
                        txtOverallComment_1st_3.BackColor = Nothing

                        txtOverallComment_1st_1.BorderWidth = Nothing
                        txtOverallComment_1st_2.BorderWidth = Nothing
                        txtOverallComment_1st_3.BorderWidth = Nothing

                        txtFeedBack_1.ReadOnly = False
                        txtFeedBack_2.ReadOnly = False
                        txtFeedBack_3.ReadOnly = False

                        txtFeedBack_1.BackColor = Nothing
                        txtFeedBack_2.BackColor = Nothing
                        txtFeedBack_3.BackColor = Nothing

                        txtFeedBack_1.BorderWidth = Nothing
                        txtFeedBack_2.BorderWidth = Nothing
                        txtFeedBack_3.BorderWidth = Nothing

                        cboRanking_1.Enabled = True

                        cboRanking_1.BackColor = Nothing

                        txtTolAchievement_1.BorderWidth = Nothing
                        txtTolSkill_1.BorderWidth = Nothing
                        txtTolScore_1.BorderWidth = Nothing

                        ddlTrain1.Enabled = True
                        txtTrain1_no.ReadOnly = False
                        ddlTrain2.Enabled = True
                        ddlTypeTrain.Enabled = True
                        txtotherTrain.ReadOnly = False
                        txtTrainTopic.ReadOnly = False
                        txtMonitor.ReadOnly = False

                        txtTrain1_no.BackColor = Nothing
                        txtotherTrain.BackColor = Nothing
                        txtTrainTopic.BackColor = Nothing
                        txtMonitor.BackColor = Nothing
                        ddlTrain1.BackColor = Nothing
                        ddlTrain2.BackColor = Nothing
                        ddlTypeTrain.BackColor = Nothing

                        txtTrain1_no.BorderWidth = Nothing
                        txtotherTrain.BorderWidth = Nothing
                        txtTrainTopic.BorderWidth = Nothing
                        txtMonitor.BorderWidth = Nothing

                        btnOK.Enabled = True
                    End If
                ElseIf hfLevel.Value = 2 And bOpen2ndAppraiser Then
                    If Val(cboWeighting_1.SelectedValue) > 0 Then
                        cboPoints1_2ndAppraiser.Enabled = True
                        cboPoints1_2ndAppraiser.BackColor = Nothing
                        cboDifficulty1_2ndAppraiser.Enabled = True
                        cboDifficulty1_2ndAppraiser.BackColor = Nothing
                    End If

                    If Val(cboWeighting_2.SelectedValue) > 0 Then
                        cboPoints2_2ndAppraiser.Enabled = True
                        cboPoints2_2ndAppraiser.BackColor = Nothing
                        cboDifficulty2_2ndAppraiser.Enabled = True
                        cboDifficulty2_2ndAppraiser.BackColor = Nothing
                    End If

                    If Val(cboWeighting_3.SelectedValue) > 0 Then
                        cboPoints3_2ndAppraiser.Enabled = True
                        cboPoints3_2ndAppraiser.BackColor = Nothing
                        cboDifficulty3_2ndAppraiser.Enabled = True
                        cboDifficulty3_2ndAppraiser.BackColor = Nothing
                    End If

                    If Val(cboWeighting_1.SelectedValue) > 0 Or Val(cboWeighting_2.SelectedValue) > 0 Or Val(cboWeighting_3.SelectedValue) > 0 Then
                        txtOverallComment_2nd_1.ReadOnly = False
                        txtOverallComment_2nd_2.ReadOnly = False
                        txtOverallComment_2nd_3.ReadOnly = False

                        txtOverallComment_2nd_1.BackColor = Nothing
                        txtOverallComment_2nd_2.BackColor = Nothing
                        txtOverallComment_2nd_3.BackColor = Nothing

                        txtOverallComment_2nd_1.BorderWidth = Nothing
                        txtOverallComment_2nd_2.BorderWidth = Nothing
                        txtOverallComment_2nd_3.BorderWidth = Nothing

                        cboRanking_2.Enabled = True

                        cboRanking_2.BackColor = Nothing

                        txtTolAchievement_2.BorderWidth = Nothing
                        txtTolSkill_2.BorderWidth = Nothing
                        txtTolScore_2.BorderWidth = Nothing

                        btnOK.Enabled = True
                    End If
                End If

                If hfLevel.Value = "3" Then
                    txtTarget1_1.ReadOnly = False
                    txtTarget1_2.ReadOnly = False
                    txtTarget1_3.ReadOnly = False
                    txtTarget1_4.ReadOnly = False
                    txtTarget1_5.ReadOnly = False
                    txtTarget1_6.ReadOnly = False
                    txtTarget1_7.ReadOnly = False
                    txtTarget1_8.ReadOnly = False

                    txtTarget1_1.BackColor = Nothing
                    txtTarget1_2.BackColor = Nothing
                    txtTarget1_3.BackColor = Nothing
                    txtTarget1_4.BackColor = Nothing
                    txtTarget1_5.BackColor = Nothing
                    txtTarget1_6.BackColor = Nothing
                    txtTarget1_7.BackColor = Nothing
                    txtTarget1_8.BackColor = Nothing

                    txtTarget1_1.BorderWidth = Nothing
                    txtTarget1_2.BorderWidth = Nothing
                    txtTarget1_3.BorderWidth = Nothing
                    txtTarget1_4.BorderWidth = Nothing
                    txtTarget1_5.BorderWidth = Nothing
                    txtTarget1_6.BorderWidth = Nothing
                    txtTarget1_7.BorderWidth = Nothing
                    txtTarget1_8.BorderWidth = Nothing

                    cboWeighting_1.Enabled = True
                    cboWeighting_1.BackColor = Nothing

                    txtTarget2_1.ReadOnly = False
                    txtTarget2_2.ReadOnly = False
                    txtTarget2_3.ReadOnly = False
                    txtTarget2_4.ReadOnly = False
                    txtTarget2_5.ReadOnly = False
                    txtTarget2_6.ReadOnly = False
                    txtTarget2_7.ReadOnly = False
                    txtTarget2_8.ReadOnly = False

                    txtTarget2_1.BackColor = Nothing
                    txtTarget2_2.BackColor = Nothing
                    txtTarget2_3.BackColor = Nothing
                    txtTarget2_4.BackColor = Nothing
                    txtTarget2_5.BackColor = Nothing
                    txtTarget2_6.BackColor = Nothing
                    txtTarget2_7.BackColor = Nothing
                    txtTarget2_8.BackColor = Nothing

                    txtTarget2_1.BorderWidth = Nothing
                    txtTarget2_2.BorderWidth = Nothing
                    txtTarget2_3.BorderWidth = Nothing
                    txtTarget2_4.BorderWidth = Nothing
                    txtTarget2_5.BorderWidth = Nothing
                    txtTarget2_6.BorderWidth = Nothing
                    txtTarget2_7.BorderWidth = Nothing
                    txtTarget2_8.BorderWidth = Nothing

                    cboWeighting_2.Enabled = True
                    cboWeighting_2.BackColor = Nothing

                    txtTarget3_1.ReadOnly = False
                    txtTarget3_2.ReadOnly = False
                    txtTarget3_3.ReadOnly = False
                    txtTarget3_4.ReadOnly = False
                    txtTarget3_5.ReadOnly = False
                    txtTarget3_6.ReadOnly = False
                    txtTarget3_7.ReadOnly = False
                    txtTarget3_8.ReadOnly = False

                    txtTarget3_1.BackColor = Nothing
                    txtTarget3_2.BackColor = Nothing
                    txtTarget3_3.BackColor = Nothing
                    txtTarget3_4.BackColor = Nothing
                    txtTarget3_5.BackColor = Nothing
                    txtTarget3_6.BackColor = Nothing
                    txtTarget3_7.BackColor = Nothing
                    txtTarget3_8.BackColor = Nothing

                    txtTarget3_1.BorderWidth = Nothing
                    txtTarget3_2.BorderWidth = Nothing
                    txtTarget3_3.BorderWidth = Nothing
                    txtTarget3_4.BorderWidth = Nothing
                    txtTarget3_5.BorderWidth = Nothing
                    txtTarget3_6.BorderWidth = Nothing
                    txtTarget3_7.BorderWidth = Nothing
                    txtTarget3_8.BorderWidth = Nothing

                    txtCommentT_1.ReadOnly = False
                    txtCommentT_2.ReadOnly = False
                    txtCommentT_3.ReadOnly = False
                    txtCommentT_4.ReadOnly = False
                    txtCommentT_5.ReadOnly = False
                    txtCommentT_6.ReadOnly = False
                    txtCommentT_7.ReadOnly = False
                    txtCommentT_8.ReadOnly = False

                    txtCommentT_1.BackColor = Nothing
                    txtCommentT_2.BackColor = Nothing
                    txtCommentT_3.BackColor = Nothing
                    txtCommentT_4.BackColor = Nothing
                    txtCommentT_5.BackColor = Nothing
                    txtCommentT_6.BackColor = Nothing
                    txtCommentT_7.BackColor = Nothing
                    txtCommentT_8.BackColor = Nothing

                    txtCommentT_1.BorderWidth = Nothing
                    txtCommentT_2.BorderWidth = Nothing
                    txtCommentT_3.BorderWidth = Nothing
                    txtCommentT_4.BorderWidth = Nothing
                    txtCommentT_5.BorderWidth = Nothing
                    txtCommentT_6.BorderWidth = Nothing
                    txtCommentT_7.BorderWidth = Nothing
                    txtCommentT_8.BorderWidth = Nothing

                    cboWeighting_3.Enabled = True
                    cboWeighting_3.BackColor = Nothing

                    txtDuties_1.ReadOnly = False
                    txtDuties_2.ReadOnly = False
                    txtDuties_3.ReadOnly = False

                    txtDuties_1.BackColor = Nothing
                    txtDuties_2.BackColor = Nothing
                    txtDuties_3.BackColor = Nothing

                    txtDuties_1.BorderWidth = Nothing
                    txtDuties_2.BorderWidth = Nothing
                    txtDuties_3.BorderWidth = Nothing

                    txtAchievement1_1.ReadOnly = False
                    txtAchievement1_2.ReadOnly = False
                    txtAchievement1_3.ReadOnly = False
                    txtAchievement1_4.ReadOnly = False
                    txtAchievement1_5.ReadOnly = False
                    txtAchievement1_6.ReadOnly = False
                    txtAchievement1_7.ReadOnly = False
                    txtAchievement1_8.ReadOnly = False

                    txtAchievement1_1.BackColor = Nothing
                    txtAchievement1_2.BackColor = Nothing
                    txtAchievement1_3.BackColor = Nothing
                    txtAchievement1_4.BackColor = Nothing
                    txtAchievement1_5.BackColor = Nothing
                    txtAchievement1_6.BackColor = Nothing
                    txtAchievement1_7.BackColor = Nothing
                    txtAchievement1_8.BackColor = Nothing

                    txtAchievement1_1.BorderWidth = Nothing
                    txtAchievement1_2.BorderWidth = Nothing
                    txtAchievement1_3.BorderWidth = Nothing
                    txtAchievement1_4.BorderWidth = Nothing
                    txtAchievement1_5.BorderWidth = Nothing
                    txtAchievement1_6.BorderWidth = Nothing
                    txtAchievement1_7.BorderWidth = Nothing
                    txtAchievement1_8.BorderWidth = Nothing

                    cboPoints1_Appraisee.Enabled = True
                    cboPoints1_Appraisee.BackColor = Nothing

                    txtAchievement2_1.ReadOnly = False
                    txtAchievement2_2.ReadOnly = False
                    txtAchievement2_3.ReadOnly = False
                    txtAchievement2_4.ReadOnly = False
                    txtAchievement2_5.ReadOnly = False
                    txtAchievement2_6.ReadOnly = False
                    txtAchievement2_7.ReadOnly = False
                    txtAchievement2_8.ReadOnly = False

                    txtAchievement2_1.BackColor = Nothing
                    txtAchievement2_2.BackColor = Nothing
                    txtAchievement2_3.BackColor = Nothing
                    txtAchievement2_4.BackColor = Nothing
                    txtAchievement2_5.BackColor = Nothing
                    txtAchievement2_6.BackColor = Nothing
                    txtAchievement2_7.BackColor = Nothing
                    txtAchievement2_8.BackColor = Nothing

                    txtAchievement2_1.BorderWidth = Nothing
                    txtAchievement2_2.BorderWidth = Nothing
                    txtAchievement2_3.BorderWidth = Nothing
                    txtAchievement2_4.BorderWidth = Nothing
                    txtAchievement2_5.BorderWidth = Nothing
                    txtAchievement2_6.BorderWidth = Nothing
                    txtAchievement2_7.BorderWidth = Nothing
                    txtAchievement2_8.BorderWidth = Nothing

                    cboPoints2_Appraisee.Enabled = True
                    cboPoints2_Appraisee.BackColor = Nothing

                    txtAchievement3_1.ReadOnly = False
                    txtAchievement3_2.ReadOnly = False
                    txtAchievement3_3.ReadOnly = False
                    txtAchievement3_4.ReadOnly = False
                    txtAchievement3_5.ReadOnly = False
                    txtAchievement3_6.ReadOnly = False
                    txtAchievement3_7.ReadOnly = False
                    txtAchievement3_8.ReadOnly = False

                    txtAchievement3_1.BackColor = Nothing
                    txtAchievement3_2.BackColor = Nothing
                    txtAchievement3_3.BackColor = Nothing
                    txtAchievement3_4.BackColor = Nothing
                    txtAchievement3_5.BackColor = Nothing
                    txtAchievement3_6.BackColor = Nothing
                    txtAchievement3_7.BackColor = Nothing
                    txtAchievement3_8.BackColor = Nothing


                    txtAchievement3_1.BorderWidth = Nothing
                    txtAchievement3_2.BorderWidth = Nothing
                    txtAchievement3_3.BorderWidth = Nothing
                    txtAchievement3_4.BorderWidth = Nothing
                    txtAchievement3_5.BorderWidth = Nothing
                    txtAchievement3_6.BorderWidth = Nothing
                    txtAchievement3_7.BorderWidth = Nothing
                    txtAchievement3_8.BorderWidth = Nothing

                    cboPoints3_Appraisee.Enabled = True
                    cboPoints3_Appraisee.BackColor = Nothing

                    cboPoints1_1stAppraiser.Enabled = True
                    cboPoints1_1stAppraiser.BackColor = Nothing
                    cboDifficulty1_1stAppraiser.Enabled = True
                    cboDifficulty1_1stAppraiser.BackColor = Nothing

                    cboPoints2_1stAppraiser.Enabled = True
                    cboPoints2_1stAppraiser.BackColor = Nothing
                    cboDifficulty2_1stAppraiser.Enabled = True
                    cboDifficulty2_1stAppraiser.BackColor = Nothing

                    cboPoints3_1stAppraiser.Enabled = True
                    cboPoints3_1stAppraiser.BackColor = Nothing
                    cboDifficulty3_1stAppraiser.Enabled = True
                    cboDifficulty3_1stAppraiser.BackColor = Nothing

                    txtCommentA_1.ReadOnly = False
                    txtCommentA_2.ReadOnly = False
                    txtCommentA_3.ReadOnly = False
                    txtCommentA_4.ReadOnly = False
                    txtCommentA_5.ReadOnly = False
                    txtCommentA_6.ReadOnly = False
                    txtCommentA_7.ReadOnly = False
                    txtCommentA_8.ReadOnly = False

                    txtCommentA_1.BackColor = Nothing
                    txtCommentA_2.BackColor = Nothing
                    txtCommentA_3.BackColor = Nothing
                    txtCommentA_4.BackColor = Nothing
                    txtCommentA_5.BackColor = Nothing
                    txtCommentA_6.BackColor = Nothing
                    txtCommentA_7.BackColor = Nothing
                    txtCommentA_8.BackColor = Nothing

                    txtCommentA_1.BorderWidth = Nothing
                    txtCommentA_2.BorderWidth = Nothing
                    txtCommentA_3.BorderWidth = Nothing
                    txtCommentA_4.BorderWidth = Nothing
                    txtCommentA_5.BorderWidth = Nothing
                    txtCommentA_6.BorderWidth = Nothing
                    txtCommentA_7.BorderWidth = Nothing
                    txtCommentA_8.BorderWidth = Nothing

                    txtOverallComment_1st_1.ReadOnly = False
                    txtOverallComment_1st_2.ReadOnly = False
                    txtOverallComment_1st_3.ReadOnly = False

                    txtOverallComment_1st_1.BackColor = Nothing
                    txtOverallComment_1st_2.BackColor = Nothing
                    txtOverallComment_1st_3.BackColor = Nothing

                    txtOverallComment_1st_1.BorderWidth = Nothing
                    txtOverallComment_1st_2.BorderWidth = Nothing
                    txtOverallComment_1st_3.BorderWidth = Nothing

                    txtFeedBack_1.ReadOnly = False
                    txtFeedBack_2.ReadOnly = False
                    txtFeedBack_3.ReadOnly = False

                    txtFeedBack_1.BackColor = Nothing
                    txtFeedBack_2.BackColor = Nothing
                    txtFeedBack_3.BackColor = Nothing

                    txtFeedBack_1.BorderWidth = Nothing
                    txtFeedBack_2.BorderWidth = Nothing
                    txtFeedBack_3.BorderWidth = Nothing

                    cboRanking_1.Enabled = True

                    cboRanking_1.BackColor = Nothing

                    txtTolAchievement_1.BorderWidth = Nothing
                    txtTolSkill_1.BorderWidth = Nothing
                    txtTolScore_1.BorderWidth = Nothing

                    ddlTrain1.Enabled = True
                    txtTrain1_no.ReadOnly = False
                    ddlTrain2.Enabled = True
                    ddlTypeTrain.Enabled = True
                    txtotherTrain.ReadOnly = False
                    txtTrainTopic.ReadOnly = False
                    txtMonitor.ReadOnly = False

                    txtTrain1_no.BackColor = Nothing
                    txtotherTrain.BackColor = Nothing
                    txtTrainTopic.BackColor = Nothing
                    txtMonitor.BackColor = Nothing
                    ddlTrain1.BackColor = Nothing
                    ddlTrain2.BackColor = Nothing
                    ddlTypeTrain.BackColor = Nothing

                    txtTrain1_no.BorderWidth = Nothing
                    txtotherTrain.BorderWidth = Nothing
                    txtTrainTopic.BorderWidth = Nothing
                    txtMonitor.BorderWidth = Nothing


                    cboPoints1_2ndAppraiser.Enabled = True
                    cboPoints1_2ndAppraiser.BackColor = Nothing
                    cboDifficulty1_2ndAppraiser.Enabled = True
                    cboDifficulty1_2ndAppraiser.BackColor = Nothing

                    cboPoints2_2ndAppraiser.Enabled = True
                    cboPoints2_2ndAppraiser.BackColor = Nothing
                    cboDifficulty2_2ndAppraiser.Enabled = True
                    cboDifficulty2_2ndAppraiser.BackColor = Nothing

                    cboPoints3_2ndAppraiser.Enabled = True
                    cboPoints3_2ndAppraiser.BackColor = Nothing
                    cboDifficulty3_2ndAppraiser.Enabled = True
                    cboDifficulty3_2ndAppraiser.BackColor = Nothing

                    txtOverallComment_2nd_1.ReadOnly = False
                    txtOverallComment_2nd_2.ReadOnly = False
                    txtOverallComment_2nd_3.ReadOnly = False

                    txtOverallComment_2nd_1.BackColor = Nothing
                    txtOverallComment_2nd_2.BackColor = Nothing
                    txtOverallComment_2nd_3.BackColor = Nothing

                    txtOverallComment_2nd_1.BorderWidth = Nothing
                    txtOverallComment_2nd_2.BorderWidth = Nothing
                    txtOverallComment_2nd_3.BorderWidth = Nothing

                    cboRanking_2.Enabled = True

                    cboRanking_2.BackColor = Nothing

                    txtTolAchievement_2.BorderWidth = Nothing
                    txtTolSkill_2.BorderWidth = Nothing
                    txtTolScore_2.BorderWidth = Nothing


                    btnOK.Enabled = True
                End If
            End If
        End If

        If hfLevel.Value = 1 And ddlTrain1.SelectedValue <> "NO" Then
            txtTrain1_no.ReadOnly = True
            txtTrain1_no.BackColor = Drawing.Color.Lavender
            txtTrain1_no.BorderWidth = 0
        End If

        If hfLevel.Value = 1 And ddlTrain2.SelectedValue <> "YES" Then
            ddlTypeTrain.Enabled = False
            ddlTypeTrain.BackColor = Drawing.Color.Lavender
            ddlTypeTrain.BorderWidth = 0
            txtotherTrain.ReadOnly = True
            txtotherTrain.BackColor = Drawing.Color.Lavender
            txtotherTrain.BorderWidth = 0
            txtMonitor.ReadOnly = True
            txtMonitor.BackColor = Drawing.Color.Lavender
            txtMonitor.BorderWidth = 0
        End If

        If hfLevel.Value = 1 And ddlTypeTrain.SelectedValue <> "E" Then
            txtTrainTopic.ReadOnly = True
            txtTrainTopic.BackColor = Drawing.Color.Lavender
            txtTrainTopic.BorderWidth = 0
        End If

    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

    End Sub

    Private Sub Page_PreRenderComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRenderComplete


    End Sub


    Protected Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        If bValidate() Then

            Submit()

        End If

    End Sub

    Private Sub Submit()

        'If optPersonal.Checked Then
        '    mMyCommand.CommandText = "sp_ap_InsUpdDel_EmpAppraisal_Duties '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
        '                           Replace(Trim(txtDuties_1.Text), "'", "''") & "','" & Replace(Trim(txtDuties_2.Text), "'", "''") & "','" & Replace(Trim(txtDuties_3.Text), "'", "''") & "','" & "ADD" & "'"
        '    mMyDataReader = mMyCommand.ExecuteReader
        '    mMyDataReader.Close()


        '    mMyCommand.CommandText = "sp_ap_InsUpdDel_EmpAppraisal_Target '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'," & _
        '                          "1" & ",'" & _
        '                          Replace(Trim(txtTarget1_1.Text), "'", "''") & "','" & Replace(Trim(txtTarget1_2.Text), "'", "''") & "','" & _
        '                          Replace(Trim(txtTarget1_3.Text), "'", "''") & "','" & Replace(Trim(txtTarget1_4.Text), "'", "''") & "','" & _
        '                          Replace(Trim(txtTarget1_5.Text), "'", "''") & "','" & Replace(Trim(txtTarget1_6.Text), "'", "''") & "','" & _
        '                          Replace(Trim(txtTarget1_7.Text), "'", "''") & "','" & Replace(Trim(txtTarget1_8.Text), "'", "''") & "'," & _
        '                          Val(cboWeighting_1.SelectedValue) & ",'" & _
        '                          Replace(Trim(txtAchievement1_1.Text), "'", "''") & "','" & Replace(Trim(txtAchievement1_2.Text), "'", "''") & "','" & _
        '                          Replace(Trim(txtAchievement1_3.Text), "'", "''") & "','" & Replace(Trim(txtAchievement1_4.Text), "'", "''") & "','" & _
        '                          Replace(Trim(txtAchievement1_5.Text), "'", "''") & "','" & Replace(Trim(txtAchievement1_6.Text), "'", "''") & "','" & _
        '                          Replace(Trim(txtAchievement1_7.Text), "'", "''") & "','" & Replace(Trim(txtAchievement1_8.Text), "'", "''") & "'," & _
        '                          Val(cboPoints1_Appraisee.SelectedValue) & ",'" & "ADD" & "'"
        '    mMyDataReader = mMyCommand.ExecuteReader
        '    mMyDataReader.Close()

        '    mMyCommand.CommandText = "sp_ap_InsUpdDel_EmpAppraisal_Target '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'," & _
        '                  "2" & ",'" & _
        '                  Replace(Trim(txtTarget2_1.Text), "'", "''") & "','" & Replace(Trim(txtTarget2_2.Text), "'", "''") & "','" & _
        '                  Replace(Trim(txtTarget2_3.Text), "'", "''") & "','" & Replace(Trim(txtTarget2_4.Text), "'", "''") & "','" & _
        '                  Replace(Trim(txtTarget2_5.Text), "'", "''") & "','" & Replace(Trim(txtTarget2_6.Text), "'", "''") & "','" & _
        '                  Replace(Trim(txtTarget2_7.Text), "'", "''") & "','" & Replace(Trim(txtTarget2_8.Text), "'", "''") & "'," & _
        '                  Val(cboWeighting_2.SelectedValue) & ",'" & _
        '                  Replace(Trim(txtAchievement2_1.Text), "'", "''") & "','" & Replace(Trim(txtAchievement2_2.Text), "'", "''") & "','" & _
        '                  Replace(Trim(txtAchievement2_3.Text), "'", "''") & "','" & Replace(Trim(txtAchievement2_4.Text), "'", "''") & "','" & _
        '                  Replace(Trim(txtAchievement2_5.Text), "'", "''") & "','" & Replace(Trim(txtAchievement2_6.Text), "'", "''") & "','" & _
        '                  Replace(Trim(txtAchievement2_7.Text), "'", "''") & "','" & Replace(Trim(txtAchievement2_8.Text), "'", "''") & "'," & _
        '                  Val(cboPoints2_Appraisee.SelectedValue) & ",'" & "ADD" & "'"
        '    mMyDataReader = mMyCommand.ExecuteReader
        '    mMyDataReader.Close()

        '    mMyCommand.CommandText = "sp_ap_InsUpdDel_EmpAppraisal_Target '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'," & _
        '                "3" & ",'" & _
        '                Replace(Trim(txtTarget3_1.Text), "'", "''") & "','" & Replace(Trim(txtTarget3_2.Text), "'", "''") & "','" & _
        '                Replace(Trim(txtTarget3_3.Text), "'", "''") & "','" & Replace(Trim(txtTarget3_4.Text), "'", "''") & "','" & _
        '                Replace(Trim(txtTarget3_5.Text), "'", "''") & "','" & Replace(Trim(txtTarget3_6.Text), "'", "''") & "','" & _
        '                Replace(Trim(txtTarget3_7.Text), "'", "''") & "','" & Replace(Trim(txtTarget3_8.Text), "'", "''") & "'," & _
        '                Val(cboWeighting_3.SelectedValue) & ",'" & _
        '                Replace(Trim(txtAchievement3_1.Text), "'", "''") & "','" & Replace(Trim(txtAchievement3_2.Text), "'", "''") & "','" & _
        '                Replace(Trim(txtAchievement3_3.Text), "'", "''") & "','" & Replace(Trim(txtAchievement3_4.Text), "'", "''") & "','" & _
        '                Replace(Trim(txtAchievement3_5.Text), "'", "''") & "','" & Replace(Trim(txtAchievement3_6.Text), "'", "''") & "','" & _
        '                Replace(Trim(txtAchievement3_7.Text), "'", "''") & "','" & Replace(Trim(txtAchievement3_8.Text), "'", "''") & "'," & _
        '                Val(cboPoints3_Appraisee.SelectedValue) & ",'" & "ADD" & "'"
        '    mMyDataReader = mMyCommand.ExecuteReader
        '    mMyDataReader.Close()

        'Else
        If hfLevel.Value = 1 Then
            'mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraiser_Points " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & _
            '                 cboAppraisal.SelectedValue & "'," & "1" & "," & Val(cboPoints1_1stAppraiser.SelectedValue) & ",'" & _
            '                 cboDifficulty1_1stAppraiser.SelectedValue & "','" & "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()

            'mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraiser_Points " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & _
            '        cboAppraisal.SelectedValue & "'," & "2" & "," & Val(cboPoints2_1stAppraiser.SelectedValue) & ",'" & _
            '        cboDifficulty2_1stAppraiser.SelectedValue & "','" & "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()

            'mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraiser_Points " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & _
            '       cboAppraisal.SelectedValue & "'," & "3" & "," & Val(cboPoints3_1stAppraiser.SelectedValue) & ",'" & _
            '       cboDifficulty3_1stAppraiser.SelectedValue & "','" & "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()


            'mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraiser_Comment " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
            '                "T" & "','" & Replace(Trim(txtCommentT_1.Text), "'", "''") & "','" & Replace(Trim(txtCommentT_2.Text), "'", "''") & "','" & Replace(Trim(txtCommentT_3.Text), "'", "''") & "','" & Replace(Trim(txtCommentT_4.Text), "'", "''") & "','" & _
            '                Replace(Trim(txtCommentT_5.Text), "'", "''") & "','" & Replace(Trim(txtCommentT_6.Text), "'", "''") & "','" & Replace(Trim(txtCommentT_7.Text), "'", "''") & "','" & Replace(Trim(txtCommentT_8.Text), "'", "''") & "','" & _
            '                "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()



            'mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraiser_Comment " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
            '                       "A" & "','" & Replace(Trim(txtCommentA_1.Text), "'", "''") & "','" & Replace(Trim(txtCommentA_2.Text), "'", "''") & "','" & Replace(Trim(txtCommentA_3.Text), "'", "''") & "','" & Replace(Trim(txtCommentA_4.Text), "'", "''") & "','" & _
            '                       Replace(Trim(txtCommentA_5.Text), "'", "''") & "','" & Replace(Trim(txtCommentA_6.Text), "'", "''") & "','" & Replace(Trim(txtCommentA_7.Text), "'", "''") & "','" & Replace(Trim(txtCommentA_8.Text), "'", "''") & "','" & _
            '                       "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()


            mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraisal_OverallComment " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
                                     Replace(Trim(txtOverallComment_1st_1.Text), "'", "''") & "','" & Replace(Trim(txtOverallComment_1st_2.Text), "'", "''") & "','" & Replace(Trim(txtOverallComment_1st_3.Text), "'", "''") & "','" & "ADD" & "'"
            mMyDataReader = mMyCommand.ExecuteReader
            mMyDataReader.Close()

            mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraisal_FeedBack " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
                                     Replace(Trim(txtFeedBack_1.Text), "'", "''") & "','" & Replace(Trim(txtFeedBack_2.Text), "'", "''") & "','" & Replace(Trim(txtFeedBack_3.Text), "'", "''") & "','" & "ADD" & "'"
            mMyDataReader = mMyCommand.ExecuteReader
            mMyDataReader.Close()

            mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraisal_Rating " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
                                     Replace(Trim(cboRanking_1.SelectedValue), "'", "''") & "','" & "ADD" & "'"
            mMyDataReader = mMyCommand.ExecuteReader
            mMyDataReader.Close()

            mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraisal_Training " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
                                     Replace(Trim(ddlTrain1.SelectedValue), "'", "''") & "','" & Replace(Trim(txtTrain1_no.Text), "'", "''") & "','" & Replace(Trim(ddlTrain2.SelectedValue), "'", "''") & _
                                     "','" & Replace(Trim(ddlTypeTrain.SelectedValue), "'", "''") & "','" & Replace(Trim(txtotherTrain.Text), "'", "''") & "','" & Replace(Trim(txtTrainTopic.Text), "'", "''") & _
                                     "','" & Replace(Trim(txtMonitor.Text), "'", "''") & "','" & "ADD" & "'"
            mMyDataReader = mMyCommand.ExecuteReader
            mMyDataReader.Close()

        ElseIf hfLevel.Value = 2 Then
            'mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraiser_Points " & "2" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & _
            '             cboAppraisal.SelectedValue & "'," & "1" & "," & Val(cboPoints1_2ndAppraiser.SelectedValue) & ",'" & _
            '             cboDifficulty1_2ndAppraiser.SelectedValue & "','" & "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()

            'mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraiser_Points " & "2" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & _
            '           cboAppraisal.SelectedValue & "'," & "2" & "," & Val(cboPoints2_2ndAppraiser.SelectedValue) & ",'" & _
            '           cboDifficulty2_2ndAppraiser.SelectedValue & "','" & "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()

            'mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraiser_Points " & "2" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & _
            '           cboAppraisal.SelectedValue & "'," & "3" & "," & Val(cboPoints3_2ndAppraiser.SelectedValue) & ",'" & _
            '           cboDifficulty3_2ndAppraiser.SelectedValue & "','" & "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()

            mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraisal_OverallComment " & "2" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
                                     Replace(Trim(txtOverallComment_2nd_1.Text), "'", "''") & "','" & Replace(Trim(txtOverallComment_2nd_2.Text), "'", "''") & "','" & Replace(Trim(txtOverallComment_2nd_3.Text), "'", "''") & "','" & "ADD" & "'"
            mMyDataReader = mMyCommand.ExecuteReader
            mMyDataReader.Close()

            mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraisal_Rating " & "2" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
                                     Replace(Trim(cboRanking_2.SelectedValue), "'", "''") & "','" & "ADD" & "'"
            mMyDataReader = mMyCommand.ExecuteReader
            mMyDataReader.Close()
        ElseIf hfLevel.Value = "3" Then
            'mMyCommand.CommandText = "sp_ap_InsUpdDel_EmpAppraisal_Duties '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
            '                   Replace(Trim(txtDuties_1.Text), "'", "''") & "','" & Replace(Trim(txtDuties_2.Text), "'", "''") & "','" & Replace(Trim(txtDuties_3.Text), "'", "''") & "','" & "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()


            'mMyCommand.CommandText = "sp_ap_InsUpdDel_EmpAppraisal_Target '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'," & _
            '                      "1" & ",'" & _
            '                      Replace(Trim(txtTarget1_1.Text), "'", "''") & "','" & Replace(Trim(txtTarget1_2.Text), "'", "''") & "','" & _
            '                      Replace(Trim(txtTarget1_3.Text), "'", "''") & "','" & Replace(Trim(txtTarget1_4.Text), "'", "''") & "','" & _
            '                      Replace(Trim(txtTarget1_5.Text), "'", "''") & "','" & Replace(Trim(txtTarget1_6.Text), "'", "''") & "','" & _
            '                      Replace(Trim(txtTarget1_7.Text), "'", "''") & "','" & Replace(Trim(txtTarget1_8.Text), "'", "''") & "'," & _
            '                      Val(cboWeighting_1.SelectedValue) & ",'" & _
            '                      Replace(Trim(txtAchievement1_1.Text), "'", "''") & "','" & Replace(Trim(txtAchievement1_2.Text), "'", "''") & "','" & _
            '                      Replace(Trim(txtAchievement1_3.Text), "'", "''") & "','" & Replace(Trim(txtAchievement1_4.Text), "'", "''") & "','" & _
            '                      Replace(Trim(txtAchievement1_5.Text), "'", "''") & "','" & Replace(Trim(txtAchievement1_6.Text), "'", "''") & "','" & _
            '                      Replace(Trim(txtAchievement1_7.Text), "'", "''") & "','" & Replace(Trim(txtAchievement1_8.Text), "'", "''") & "'," & _
            '                      Val(cboPoints1_Appraisee.SelectedValue) & ",'" & "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()

            'mMyCommand.CommandText = "sp_ap_InsUpdDel_EmpAppraisal_Target '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'," & _
            '              "2" & ",'" & _
            '              Replace(Trim(txtTarget2_1.Text), "'", "''") & "','" & Replace(Trim(txtTarget2_2.Text), "'", "''") & "','" & _
            '              Replace(Trim(txtTarget2_3.Text), "'", "''") & "','" & Replace(Trim(txtTarget2_4.Text), "'", "''") & "','" & _
            '              Replace(Trim(txtTarget2_5.Text), "'", "''") & "','" & Replace(Trim(txtTarget2_6.Text), "'", "''") & "','" & _
            '              Replace(Trim(txtTarget2_7.Text), "'", "''") & "','" & Replace(Trim(txtTarget2_8.Text), "'", "''") & "'," & _
            '              Val(cboWeighting_2.SelectedValue) & ",'" & _
            '              Replace(Trim(txtAchievement2_1.Text), "'", "''") & "','" & Replace(Trim(txtAchievement2_2.Text), "'", "''") & "','" & _
            '              Replace(Trim(txtAchievement2_3.Text), "'", "''") & "','" & Replace(Trim(txtAchievement2_4.Text), "'", "''") & "','" & _
            '              Replace(Trim(txtAchievement2_5.Text), "'", "''") & "','" & Replace(Trim(txtAchievement2_6.Text), "'", "''") & "','" & _
            '              Replace(Trim(txtAchievement2_7.Text), "'", "''") & "','" & Replace(Trim(txtAchievement2_8.Text), "'", "''") & "'," & _
            '              Val(cboPoints2_Appraisee.SelectedValue) & ",'" & "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()

            'mMyCommand.CommandText = "sp_ap_InsUpdDel_EmpAppraisal_Target '" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "'," & _
            '            "3" & ",'" & _
            '            Replace(Trim(txtTarget3_1.Text), "'", "''") & "','" & Replace(Trim(txtTarget3_2.Text), "'", "''") & "','" & _
            '            Replace(Trim(txtTarget3_3.Text), "'", "''") & "','" & Replace(Trim(txtTarget3_4.Text), "'", "''") & "','" & _
            '            Replace(Trim(txtTarget3_5.Text), "'", "''") & "','" & Replace(Trim(txtTarget3_6.Text), "'", "''") & "','" & _
            '            Replace(Trim(txtTarget3_7.Text), "'", "''") & "','" & Replace(Trim(txtTarget3_8.Text), "'", "''") & "'," & _
            '            Val(cboWeighting_3.SelectedValue) & ",'" & _
            '            Replace(Trim(txtAchievement3_1.Text), "'", "''") & "','" & Replace(Trim(txtAchievement3_2.Text), "'", "''") & "','" & _
            '            Replace(Trim(txtAchievement3_3.Text), "'", "''") & "','" & Replace(Trim(txtAchievement3_4.Text), "'", "''") & "','" & _
            '            Replace(Trim(txtAchievement3_5.Text), "'", "''") & "','" & Replace(Trim(txtAchievement3_6.Text), "'", "''") & "','" & _
            '            Replace(Trim(txtAchievement3_7.Text), "'", "''") & "','" & Replace(Trim(txtAchievement3_8.Text), "'", "''") & "'," & _
            '            Val(cboPoints3_Appraisee.SelectedValue) & ",'" & "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()

            'mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraiser_Points " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & _
            '                 cboAppraisal.SelectedValue & "'," & "1" & "," & Val(cboPoints1_1stAppraiser.SelectedValue) & ",'" & _
            '                 cboDifficulty1_1stAppraiser.SelectedValue & "','" & "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()

            'mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraiser_Points " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & _
            '        cboAppraisal.SelectedValue & "'," & "2" & "," & Val(cboPoints2_1stAppraiser.SelectedValue) & ",'" & _
            '        cboDifficulty2_1stAppraiser.SelectedValue & "','" & "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()

            'mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraiser_Points " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & _
            '       cboAppraisal.SelectedValue & "'," & "3" & "," & Val(cboPoints3_1stAppraiser.SelectedValue) & ",'" & _
            '       cboDifficulty3_1stAppraiser.SelectedValue & "','" & "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()


            'mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraiser_Comment " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
            '                "T" & "','" & Replace(Trim(txtCommentT_1.Text), "'", "''") & "','" & Replace(Trim(txtCommentT_2.Text), "'", "''") & "','" & Replace(Trim(txtCommentT_3.Text), "'", "''") & "','" & Replace(Trim(txtCommentT_4.Text), "'", "''") & "','" & _
            '                Replace(Trim(txtCommentT_5.Text), "'", "''") & "','" & Replace(Trim(txtCommentT_6.Text), "'", "''") & "','" & Replace(Trim(txtCommentT_7.Text), "'", "''") & "','" & Replace(Trim(txtCommentT_8.Text), "'", "''") & "','" & _
            '                "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()



            'mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraiser_Comment " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
            '                       "A" & "','" & Replace(Trim(txtCommentA_1.Text), "'", "''") & "','" & Replace(Trim(txtCommentA_2.Text), "'", "''") & "','" & Replace(Trim(txtCommentA_3.Text), "'", "''") & "','" & Replace(Trim(txtCommentA_4.Text), "'", "''") & "','" & _
            '                       Replace(Trim(txtCommentA_5.Text), "'", "''") & "','" & Replace(Trim(txtCommentA_6.Text), "'", "''") & "','" & Replace(Trim(txtCommentA_7.Text), "'", "''") & "','" & Replace(Trim(txtCommentA_8.Text), "'", "''") & "','" & _
            '                       "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()


            mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraisal_OverallComment " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
                                     Replace(Trim(txtOverallComment_1st_1.Text), "'", "''") & "','" & Replace(Trim(txtOverallComment_1st_2.Text), "'", "''") & "','" & Replace(Trim(txtOverallComment_1st_3.Text), "'", "''") & "','" & "ADD" & "'"
            mMyDataReader = mMyCommand.ExecuteReader
            mMyDataReader.Close()

            mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraisal_FeedBack " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
                                     Replace(Trim(txtFeedBack_1.Text), "'", "''") & "','" & Replace(Trim(txtFeedBack_2.Text), "'", "''") & "','" & Replace(Trim(txtFeedBack_3.Text), "'", "''") & "','" & "ADD" & "'"
            mMyDataReader = mMyCommand.ExecuteReader
            mMyDataReader.Close()

            mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraisal_Rating " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
                                     Replace(Trim(cboRanking_1.SelectedValue), "'", "''") & "','" & "ADD" & "'"
            mMyDataReader = mMyCommand.ExecuteReader
            mMyDataReader.Close()

            mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraisal_Training " & "1" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
                                     Replace(Trim(ddlTrain1.SelectedValue), "'", "''") & "','" & Replace(Trim(txtTrain1_no.Text), "'", "''") & "','" & Replace(Trim(ddlTrain2.SelectedValue), "'", "''") & _
                                     "','" & Replace(Trim(ddlTypeTrain.SelectedValue), "'", "''") & "','" & Replace(Trim(txtotherTrain.Text), "'", "''") & "','" & Replace(Trim(txtTrainTopic.Text), "'", "''") & _
                                     "','" & Replace(Trim(txtMonitor.Text), "'", "''") & "','" & "ADD" & "'"
            mMyDataReader = mMyCommand.ExecuteReader
            mMyDataReader.Close()

            'mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraiser_Points " & "2" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & _
            '             cboAppraisal.SelectedValue & "'," & "1" & "," & Val(cboPoints1_2ndAppraiser.SelectedValue) & ",'" & _
            '             cboDifficulty1_2ndAppraiser.SelectedValue & "','" & "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()

            'mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraiser_Points " & "2" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & _
            '           cboAppraisal.SelectedValue & "'," & "2" & "," & Val(cboPoints2_2ndAppraiser.SelectedValue) & ",'" & _
            '           cboDifficulty2_2ndAppraiser.SelectedValue & "','" & "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()

            'mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraiser_Points " & "2" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & _
            '           cboAppraisal.SelectedValue & "'," & "3" & "," & Val(cboPoints3_2ndAppraiser.SelectedValue) & ",'" & _
            '           cboDifficulty3_2ndAppraiser.SelectedValue & "','" & "ADD" & "'"
            'mMyDataReader = mMyCommand.ExecuteReader
            'mMyDataReader.Close()

            mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraisal_OverallComment " & "2" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
                                     Replace(Trim(txtOverallComment_2nd_1.Text), "'", "''") & "','" & Replace(Trim(txtOverallComment_2nd_2.Text), "'", "''") & "','" & Replace(Trim(txtOverallComment_2nd_3.Text), "'", "''") & "','" & "ADD" & "'"
            mMyDataReader = mMyCommand.ExecuteReader
            mMyDataReader.Close()

            mMyCommand.CommandText = "sp_ap_InsUpdDel_Appraisal_Rating " & "2" & ",'" & Session("EmpID") & "','" & mstrEmpID & "','" & cboAppraisal.SelectedValue & "','" & _
                                     Replace(Trim(cboRanking_2.SelectedValue), "'", "''") & "','" & "ADD" & "'"
            mMyDataReader = mMyCommand.ExecuteReader
            mMyDataReader.Close()
        End If
        'End If

    End Sub

    Private Function bValidate() As Boolean

        bValidate = True

        lblError.Text = ""
        lblError.Visible = False

        'If optPersonal.Checked Then
        'If txtTarget1_1.ReadOnly = False Then
        '    If (txtTarget1_1.Text & txtTarget1_2.Text & txtTarget1_3.Text & txtTarget1_4.Text & _
        '       txtTarget1_5.Text & txtTarget1_6.Text & txtTarget1_7.Text & txtTarget1_8.Text) = "" Or _
        '       cboWeighting_1.SelectedValue = "" Then

        '        lblError.Text = lblError.Text & lblTarget1.Text & " incomplete. "
        '        mbError = True
        '        GoTo ErrorHandle
        '    End If

        '    If Val(cboWeighting_1.SelectedValue) > 0 And Val(cboWeighting_1.SelectedValue) <> 100 Then
        '        If (txtTarget2_1.Text & txtTarget2_2.Text & txtTarget2_3.Text & txtTarget2_4.Text & _
        '           txtTarget2_5.Text & txtTarget2_6.Text & txtTarget2_7.Text & txtTarget2_8.Text) = "" Or _
        '           cboWeighting_2.SelectedValue = "" Then

        '            lblError.Text = lblError.Text & lblTarget2.Text & " incomplete. "
        '            mbError = True
        '            GoTo ErrorHandle
        '        End If
        '    End If

        '    If Val(cboWeighting_1.SelectedValue) + Val(cboWeighting_2.SelectedValue) < 100 Then
        '        If (txtTarget3_1.Text & txtTarget3_2.Text & txtTarget3_3.Text & txtTarget3_4.Text & _
        '           txtTarget3_5.Text & txtTarget3_6.Text & txtTarget3_7.Text & txtTarget3_8.Text) = "" Or _
        '           cboWeighting_3.SelectedValue = "" Then

        '            lblError.Text = lblError.Text & lblTarget3.Text & " incomplete. "
        '            mbError = True
        '            GoTo ErrorHandle
        '        End If
        '    End If

        '    If Val(cboWeighting_1.SelectedValue) + Val(cboWeighting_2.SelectedValue) + Val(cboWeighting_3.SelectedValue) <> 100 Then
        '        lblError.Text = "Total Weighting must be 100. "
        '        mbError = True
        '        GoTo ErrorHandle
        '    End If

        'ElseIf txtAchievement1_1.ReadOnly = False Then
        '    If (txtDuties_1.Text & txtDuties_2.Text & txtDuties_3.Text) = "" Then
        '        lblError.Text = lblError.Text & lblDuties.Text & " incomplete. "
        '        mbError = True
        '        GoTo ErrorHandle
        '    End If

        '    If (txtAchievement1_1.Text & txtAchievement1_2.Text & txtAchievement1_3.Text & txtAchievement1_4.Text & _
        '      txtAchievement1_5.Text & txtAchievement1_6.Text & txtAchievement1_7.Text & txtAchievement1_8.Text) = "" Or _
        '      cboPoints1_Appraisee.SelectedValue = "" Then

        '        lblError.Text = lblError.Text & lblAchievement1.Text & " incomplete. "
        '        mbError = True
        '        GoTo ErrorHandle
        '    End If

        '    If Val(cboWeighting_2.SelectedValue) > 0 Then
        '        If (txtAchievement2_1.Text & txtAchievement2_2.Text & txtAchievement2_3.Text & txtAchievement2_4.Text & _
        '           txtAchievement2_5.Text & txtAchievement2_6.Text & txtAchievement2_7.Text & txtAchievement2_8.Text) = "" Or _
        '           cboPoints2_Appraisee.SelectedValue = "" Then

        '            lblError.Text = lblError.Text & lblAchievement2.Text & " incomplete. "
        '            mbError = True
        '            GoTo ErrorHandle
        '        End If
        '    End If

        '    If Val(cboWeighting_3.SelectedValue) > 0 Then
        '        If (txtAchievement3_1.Text & txtAchievement3_2.Text & txtAchievement3_3.Text & txtAchievement3_4.Text & _
        '           txtAchievement3_5.Text & txtAchievement3_6.Text & txtAchievement3_7.Text & txtAchievement3_8.Text) = "" Or _
        '           cboPoints3_Appraisee.SelectedValue = "" Then

        '            lblError.Text = lblError.Text & lblAchievement3.Text & " incomplete. "
        '            mbError = True
        '            GoTo ErrorHandle
        '        End If
        '    End If
        'End If
        'Else
        If hfLevel.Value = 1 Then
            'If Val(cboWeighting_1.SelectedValue) > 0 And cboDifficulty1_1stAppraiser.SelectedValue = "" Then
            '    lblError.Text = lblTarget1.Text & " - Difficulty incomplete. "
            '    mbError = True

            '    GoTo ErrorHandle
            'End If

            'If Val(cboWeighting_1.SelectedValue) > 0 And cboPoints1_1stAppraiser.Enabled = True And Val(cboPoints1_1stAppraiser.SelectedValue) = 0 Then
            '    lblError.Text = lblAchievement1.Text & " - Points incomplete. "
            '    mbError = True
            '    GoTo ErrorHandle
            'End If

            'If Val(cboWeighting_2.SelectedValue) > 0 And cboDifficulty2_1stAppraiser.SelectedValue = "" Then
            '    lblError.Text = lblTarget2.Text & " - Difficulty incomplete. "
            '    mbError = True
            '    GoTo ErrorHandle
            'End If

            'If Val(cboWeighting_2.SelectedValue) > 0 And cboPoints2_1stAppraiser.Enabled = True And Val(cboPoints2_1stAppraiser.SelectedValue) = 0 Then
            '    lblError.Text = lblAchievement2.Text & " - Points incomplete. "
            '    mbError = True
            '    GoTo ErrorHandle
            'End If

            'If Val(cboWeighting_3.SelectedValue) > 0 And cboDifficulty3_1stAppraiser.SelectedValue = "" Then
            '    lblError.Text = lblTarget3.Text & " - Difficulty incomplete. "
            '    mbError = True
            '    GoTo ErrorHandle
            'End If

            'If Val(cboWeighting_3.SelectedValue) > 0 And cboPoints3_1stAppraiser.Enabled = True And Val(cboPoints3_1stAppraiser.SelectedValue) = 0 Then
            '    lblError.Text = lblAchievement3.Text & " - Points incomplete. "
            '    mbError = True
            '    GoTo ErrorHandle
            'End If

            'If txtCommentT_1.ReadOnly = False Then
            '    If (txtCommentT_1.Text & txtCommentT_2.Text & txtCommentT_3.Text & txtCommentT_4.Text & _
            '     txtCommentT_5.Text & txtCommentT_6.Text & txtCommentT_7.Text & txtCommentT_8.Text) = "" Then

            '        lblError.Text = "1st Appraiser's Comment(s) - " & lblComment_Target.Text & " incomplete. "
            '        mbError = True
            '        GoTo ErrorHandle
            '    End If
            'End If

            'If txtCommentA_1.ReadOnly = False Then
            '    If (txtCommentA_1.Text & txtCommentA_2.Text & txtCommentA_3.Text & txtCommentA_4.Text & _
            '     txtCommentA_5.Text & txtCommentA_6.Text & txtCommentA_7.Text & txtCommentA_8.Text) = "" Then

            '        lblError.Text = "1st Appraiser's Comment(s) - " & lblComment_Achievement.Text & " incomplete. "
            '        mbError = True
            '        GoTo ErrorHandle
            '    End If
            'End If


            If txtOverallComment_1st_1.ReadOnly = False Then
                If (txtOverallComment_1st_1.Text & txtOverallComment_1st_2.Text & txtOverallComment_1st_3.Text) = "" Then

                    lblError.Text = lblOverallComment_1st.Text & " incomplete. "
                    mbError = True
                    GoTo ErrorHandle
                End If
            End If

            If Val(cboRanking_1.SelectedValue) = 0 Then
                lblError.Text = lblOverallComment_1st.Text & " - Raking incomplete. "
                mbError = True
                GoTo ErrorHandle
            End If

            If ddlTrain1.SelectedValue = "" Then
                lblError.Text = lblTrain1.Text & " incomplete. "
                mbError = True
                GoTo ErrorHandle
            ElseIf ddlTrain1.SelectedValue = "NO" And txtTrain1_no.Text = "" Then
                lblError.Text = lblTrain1_no.Text & " incomplete. "
                mbError = True
                GoTo ErrorHandle
            End If

            If ddlTrain2.SelectedValue = "" Then
                lblError.Text = lblTrain2.Text & " incomplete. "
                mbError = True
                GoTo ErrorHandle
            ElseIf ddlTrain2.SelectedValue = "YES" And ddlTypeTrain.SelectedValue = "" And txtotherTrain.Text = "" Then
                lblError.Text = lblTrain2.Text & " - Type of training or others incompleted"
                mbError = True
                GoTo ErrorHandle
            ElseIf ddlTrain2.SelectedValue = "YES" And ddlTypeTrain.SelectedValue = "E" And txtTrainTopic.Text = "" Then
                lblError.Text = lblTrain_Ex.Text & " incompleted"
                mbError = True
                GoTo ErrorHandle
            ElseIf ddlTrain2.SelectedValue = "YES" And txtMonitor.Text = "" Then
                lblError.Text = lblMonitor.Text & " incompleted"
                mbError = True
                GoTo ErrorHandle
            End If
        ElseIf hfLevel.Value = 2 Then
            'If Val(cboWeighting_1.SelectedValue) > 0 And cboDifficulty1_2ndAppraiser.SelectedValue = "" Then
            '    lblError.Text = lblTarget1.Text & " - Difficulty incomplete. "
            '    mbError = True
            '    GoTo ErrorHandle
            'End If

            'If Val(cboWeighting_1.SelectedValue) > 0 And Val(cboPoints1_2ndAppraiser.SelectedValue) = 0 Then
            '    lblError.Text = lblAchievement1.Text & " - Points incomplete. "
            '    mbError = True
            '    GoTo ErrorHandle
            'End If

            'If Val(cboWeighting_2.SelectedValue) > 0 And cboDifficulty2_2ndAppraiser.SelectedValue = "" Then
            '    lblError.Text = lblTarget2.Text & " - Difficulty incomplete. "
            '    mbError = True
            '    GoTo ErrorHandle
            'End If

            'If Val(cboWeighting_2.SelectedValue) > 0 And Val(cboPoints2_2ndAppraiser.SelectedValue) = 0 Then
            '    lblError.Text = lblAchievement2.Text & " - Points incomplete. "
            '    mbError = True
            '    GoTo ErrorHandle
            'End If

            'If Val(cboWeighting_3.SelectedValue) > 0 And cboDifficulty3_2ndAppraiser.SelectedValue = "" Then
            '    lblError.Text = lblTarget3.Text & " - Difficulty incomplete. "
            '    mbError = True
            '    GoTo ErrorHandle
            'End If

            'If Val(cboWeighting_3.SelectedValue) > 0 And Val(cboPoints3_2ndAppraiser.SelectedValue) = 0 Then
            '    lblError.Text = lblAchievement3.Text & " - Points incomplete. "
            '    mbError = True
            '    GoTo ErrorHandle
            'End If

            If txtOverallComment_2nd_1.ReadOnly = False Then
                If (txtOverallComment_2nd_1.Text & txtOverallComment_2nd_2.Text & txtOverallComment_2nd_3.Text) = "" Then

                    lblError.Text = lblOverallComment_2nd.Text & " incomplete. "
                    mbError = True
                    GoTo ErrorHandle
                End If
            End If

            If Val(cboRanking_2.SelectedValue) = 0 Then
                lblError.Text = lblOverallComment_2nd.Text & " - Raking incomplete. "
                mbError = True
                GoTo ErrorHandle
            End If
        End If
        'End If

ErrorHandle:

        If mbError Then
            lblError.Visible = True
            lblError.BackColor = Drawing.Color.Red
            lblError.ForeColor = Drawing.Color.White

            If optPersonal.Checked Then
                If txtTarget1_1.ReadOnly = False Then
                    mstrTarget_1_1 = txtTarget1_1.Text
                    mstrTarget_1_2 = txtTarget1_2.Text
                    mstrTarget_1_3 = txtTarget1_3.Text
                    mstrTarget_1_4 = txtTarget1_4.Text
                    mstrTarget_1_5 = txtTarget1_5.Text
                    mstrTarget_1_6 = txtTarget1_6.Text
                    mstrTarget_1_7 = txtTarget1_7.Text
                    mstrTarget_1_8 = txtTarget1_8.Text
                    mstrWeighting_1 = cboWeighting_1.SelectedValue

                    mstrTarget_2_1 = txtTarget2_1.Text
                    mstrTarget_2_2 = txtTarget2_2.Text
                    mstrTarget_2_3 = txtTarget2_3.Text
                    mstrTarget_2_4 = txtTarget2_4.Text
                    mstrTarget_2_5 = txtTarget2_5.Text
                    mstrTarget_2_6 = txtTarget2_6.Text
                    mstrTarget_2_7 = txtTarget2_7.Text
                    mstrTarget_2_8 = txtTarget2_8.Text
                    mstrWeighting_2 = cboWeighting_2.SelectedValue

                    mstrTarget_3_1 = txtTarget3_1.Text
                    mstrTarget_3_2 = txtTarget3_2.Text
                    mstrTarget_3_3 = txtTarget3_3.Text
                    mstrTarget_3_4 = txtTarget3_4.Text
                    mstrTarget_3_5 = txtTarget3_5.Text
                    mstrTarget_3_6 = txtTarget3_6.Text
                    mstrTarget_3_7 = txtTarget3_7.Text
                    mstrTarget_3_8 = txtTarget3_8.Text
                    mstrWeighting_3 = cboWeighting_3.SelectedValue
                ElseIf txtAchievement1_1.ReadOnly = False Then
                    mstrDuties_1 = txtDuties_1.Text
                    mstrDuties_2 = txtDuties_2.Text
                    mstrDuties_3 = txtDuties_3.Text

                    mstrAchievement_1_1 = txtAchievement1_1.Text
                    mstrAchievement_1_2 = txtAchievement1_2.Text
                    mstrAchievement_1_3 = txtAchievement1_3.Text
                    mstrAchievement_1_4 = txtAchievement1_4.Text
                    mstrAchievement_1_5 = txtAchievement1_5.Text
                    mstrAchievement_1_6 = txtAchievement1_6.Text
                    mstrAchievement_1_7 = txtAchievement1_7.Text
                    mstrAchievement_1_8 = txtAchievement1_8.Text
                    mstrPoints_1 = cboPoints1_Appraisee.SelectedValue

                    mstrAchievement_2_1 = txtAchievement2_1.Text
                    mstrAchievement_2_2 = txtAchievement2_2.Text
                    mstrAchievement_2_3 = txtAchievement2_3.Text
                    mstrAchievement_2_4 = txtAchievement2_4.Text
                    mstrAchievement_2_5 = txtAchievement2_5.Text
                    mstrAchievement_2_6 = txtAchievement2_6.Text
                    mstrAchievement_2_7 = txtAchievement2_7.Text
                    mstrAchievement_2_8 = txtAchievement2_8.Text
                    mstrPoints_2 = cboPoints2_Appraisee.SelectedValue

                    mstrAchievement_3_1 = txtAchievement3_1.Text
                    mstrAchievement_3_2 = txtAchievement3_2.Text
                    mstrAchievement_3_3 = txtAchievement3_3.Text
                    mstrAchievement_3_4 = txtAchievement3_4.Text
                    mstrAchievement_3_5 = txtAchievement3_5.Text
                    mstrAchievement_3_6 = txtAchievement3_6.Text
                    mstrAchievement_3_7 = txtAchievement3_7.Text
                    mstrAchievement_3_8 = txtAchievement3_8.Text
                    mstrPoints_3 = cboPoints3_Appraisee.SelectedValue
                End If
            Else
                If hfLevel.Value = 1 Then
                    mstrDifficulty1_1stAppraiser = cboDifficulty1_1stAppraiser.SelectedValue
                    mstrPoints1_1stAppraiser = cboPoints1_1stAppraiser.SelectedValue

                    mstrDifficulty2_1stAppraiser = cboDifficulty2_1stAppraiser.SelectedValue
                    mstrPoints2_1stAppraiser = cboPoints2_1stAppraiser.SelectedValue

                    mstrDifficulty3_1stAppraiser = cboDifficulty3_1stAppraiser.SelectedValue
                    mstrPoints3_1stAppraiser = cboPoints3_1stAppraiser.SelectedValue

                    mstrComment_Target1 = txtCommentT_1.Text
                    mstrComment_Target2 = txtCommentT_2.Text
                    mstrComment_Target3 = txtCommentT_3.Text
                    mstrComment_Target4 = txtCommentT_4.Text
                    mstrComment_Target5 = txtCommentT_5.Text
                    mstrComment_Target6 = txtCommentT_6.Text
                    mstrComment_Target7 = txtCommentT_7.Text
                    mstrComment_Target8 = txtCommentT_8.Text

                    mstrComment_Achievement1 = txtCommentA_1.Text
                    mstrComment_Achievement2 = txtCommentA_2.Text
                    mstrComment_Achievement3 = txtCommentA_3.Text
                    mstrComment_Achievement4 = txtCommentA_4.Text
                    mstrComment_Achievement5 = txtCommentA_5.Text
                    mstrComment_Achievement6 = txtCommentA_6.Text
                    mstrComment_Achievement7 = txtCommentA_7.Text
                    mstrComment_Achievement8 = txtCommentA_8.Text

                    mstrOverall_1st_1 = txtOverallComment_1st_1.Text
                    mstrOverall_1st_2 = txtOverallComment_1st_2.Text
                    mstrOverall_1st_3 = txtOverallComment_1st_3.Text

                    mstrFeedback_1st_1 = txtFeedBack_1.Text
                    mstrFeedback_1st_2 = txtFeedBack_2.Text
                    mstrFeedback_1st_3 = txtFeedBack_3.Text

                    mstrAchievement1 = txtTolAchievement_1.Text
                    mstrSkill1 = txtTolSkill_1.Text
                    mstrTotal1 = txtTolScore_1.Text
                    mstrRating1 = cboRanking_1.SelectedValue

                    mstrTrainDesc1 = ddlTrain1.SelectedValue
                    mstrTrainDesc2 = txtTrain1_no.Text
                    mstrTrainDesc3 = ddlTrain2.SelectedValue
                    mstrTrainDesc4 = ddlTypeTrain.SelectedValue
                    mstrTrainDesc5 = txtotherTrain.Text
                    mstrTrainDesc6 = txtTrainTopic.Text
                    mstrTrainDesc7 = txtMonitor.Text
                ElseIf hfLevel.Value = 2 Then
                    mstrDifficulty1_2ndAppraiser = cboDifficulty1_2ndAppraiser.SelectedValue
                    mstrPoints1_2ndAppraiser = cboPoints1_2ndAppraiser.SelectedValue

                    mstrDifficulty2_2ndAppraiser = cboDifficulty2_2ndAppraiser.SelectedValue
                    mstrPoints2_2ndAppraiser = cboPoints2_2ndAppraiser.SelectedValue

                    mstrDifficulty3_2ndAppraiser = cboDifficulty3_2ndAppraiser.SelectedValue
                    mstrPoints3_2ndAppraiser = cboPoints3_2ndAppraiser.SelectedValue

                    mstrOverall_2nd_1 = txtOverallComment_2nd_1.Text
                    mstrOverall_2nd_2 = txtOverallComment_2nd_2.Text
                    mstrOverall_2nd_3 = txtOverallComment_2nd_3.Text

                    mstrAchievement2 = txtTolAchievement_2.Text
                    mstrSkill2 = txtTolSkill_2.Text
                    mstrTotal2 = txtTolScore_2.Text
                    mstrRating2 = cboRanking_2.SelectedValue

                End If
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

        mstrDuties_1 = ""
        mstrDuties_2 = ""
        mstrDuties_3 = ""

        mstrTarget_1_1 = ""
        mstrTarget_1_2 = ""
        mstrTarget_1_3 = ""
        mstrTarget_1_4 = ""
        mstrTarget_1_5 = ""
        mstrTarget_1_6 = ""
        mstrTarget_1_7 = ""
        mstrTarget_1_8 = ""
        mstrWeighting_1 = ""

        mstrTarget_2_1 = ""
        mstrTarget_2_2 = ""
        mstrTarget_2_3 = ""
        mstrTarget_2_4 = ""
        mstrTarget_2_5 = ""
        mstrTarget_2_6 = ""
        mstrTarget_2_7 = ""
        mstrTarget_2_8 = ""
        mstrWeighting_2 = ""

        mstrTarget_3_1 = ""
        mstrTarget_3_2 = ""
        mstrTarget_3_3 = ""
        mstrTarget_3_4 = ""
        mstrTarget_3_5 = ""
        mstrTarget_3_6 = ""
        mstrTarget_3_7 = ""
        mstrTarget_3_8 = ""
        mstrWeighting_3 = ""

        mstrAchievement_1_1 = ""
        mstrAchievement_1_2 = ""
        mstrAchievement_1_3 = ""
        mstrAchievement_1_4 = ""
        mstrAchievement_1_5 = ""
        mstrAchievement_1_6 = ""
        mstrAchievement_1_7 = ""
        mstrAchievement_1_8 = ""
        mstrPoints_1 = ""

        mstrAchievement_2_1 = ""
        mstrAchievement_2_2 = ""
        mstrAchievement_2_3 = ""
        mstrAchievement_2_4 = ""
        mstrAchievement_2_5 = ""
        mstrAchievement_2_6 = ""
        mstrAchievement_2_7 = ""
        mstrAchievement_2_8 = ""
        mstrPoints_2 = ""

        mstrAchievement_3_1 = ""
        mstrAchievement_3_2 = ""
        mstrAchievement_3_3 = ""
        mstrAchievement_3_4 = ""
        mstrAchievement_3_5 = ""
        mstrAchievement_3_6 = ""
        mstrAchievement_3_7 = ""
        mstrAchievement_3_8 = ""
        mstrPoints_3 = ""

        mstrDifficulty1_1stAppraiser = ""
        mstrPoints1_1stAppraiser = ""

        mstrDifficulty2_1stAppraiser = ""
        mstrPoints2_1stAppraiser = ""

        mstrDifficulty3_1stAppraiser = ""
        mstrPoints3_1stAppraiser = ""

        mstrComment_Target1 = ""
        mstrComment_Target2 = ""
        mstrComment_Target3 = ""
        mstrComment_Target4 = ""
        mstrComment_Target5 = ""
        mstrComment_Target6 = ""
        mstrComment_Target7 = ""
        mstrComment_Target8 = ""

        mstrComment_Achievement1 = ""
        mstrComment_Achievement2 = ""
        mstrComment_Achievement3 = ""
        mstrComment_Achievement4 = ""
        mstrComment_Achievement5 = ""
        mstrComment_Achievement6 = ""
        mstrComment_Achievement7 = ""
        mstrComment_Achievement8 = ""

        mstrDifficulty1_2ndAppraiser = ""
        mstrPoints1_2ndAppraiser = ""

        mstrDifficulty2_2ndAppraiser = ""
        mstrPoints2_2ndAppraiser = ""

        mstrDifficulty3_2ndAppraiser = ""
        mstrPoints3_2ndAppraiser = ""

        mstrOverall_1st_1 = ""
        mstrOverall_1st_2 = ""
        mstrOverall_1st_3 = ""

        mstrFeedback_1st_1 = ""
        mstrFeedback_1st_2 = ""
        mstrFeedback_1st_3 = ""

        mstrAchievement1 = ""
        mstrSkill1 = ""
        mstrTotal1 = ""
        mstrRating1 = ""

        mstrTrainDesc1 = ""
        mstrTrainDesc2 = ""
        mstrTrainDesc3 = ""
        mstrTrainDesc4 = ""
        mstrTrainDesc5 = ""
        mstrTrainDesc6 = ""
        mstrTrainDesc7 = ""

        mstrOverall_2nd_1 = ""
        mstrOverall_2nd_2 = ""
        mstrOverall_2nd_3 = ""

        mstrAchievement1 = ""
        mstrSkill1 = ""
        mstrTotal1 = ""
        mstrRating1 = ""

        bValidate = True

    End Function
    Protected Sub optPersonal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPersonal.CheckedChanged

        cboSubordinate.Items.Clear()
        cboSubordinate.Enabled = False

        mbError = False
        lblError.Text = ""
        lblError.Visible = False


    End Sub

    Protected Sub optSubordinate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSubordinate.CheckedChanged

        GetSubordinate()

        mstrEmpID = cboSubordinate.SelectedValue

        mbError = False
        lblError.Text = ""
        lblError.Visible = False

    End Sub

    Private Sub lnkChangePwd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkChangePwd.Click

        Response.Redirect("changepwd.aspx", True)

    End Sub

    Private Sub lnkLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogout.Click

        Session("EmpID") = ""
        Response.Redirect("../Global/SessionTimeOut.aspx", True)

    End Sub

    Private Sub lnkbtnSkills_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnSkills.Click

        Response.Redirect("staffskills.aspx", True)

    End Sub

    Protected Sub lnkbtnComment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnComment.Click
        Response.Redirect("staffComment.aspx", True)
    End Sub

    Private Sub lnkbtnTarget_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnTarget.Click

        Response.Redirect("stafftarget.aspx", True)

    End Sub

    Private Sub cboSubordinate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSubordinate.SelectedIndexChanged

        mbError = False
        lblError.Text = ""
        lblError.Visible = False

    End Sub

    Private Sub cboAppraisal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAppraisal.SelectedIndexChanged

        Dim iLoop As Integer

        If optSubordinate.Checked Then
            GetSubordinate()

            For iLoop = 0 To cboSubordinate.Items.Count - 1
                If cboSubordinate.Items(iLoop).Value = mstrEmpID Then
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
            Response.Redirect("appraisee manual.pdf", True)
        End If
        mMyDataReader.Close()

    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload

        mMyConnection.Close()


    End Sub

    Private Sub lnkReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkReport.Click

        Response.Redirect("appraisalreport.aspx", True)

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

    Protected Sub ddlTrain1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTrain1.SelectedIndexChanged
        If hfLevel.Value = 1 And ddlTrain1.SelectedValue = "NO" Then
            txtTrain1_no.ReadOnly = False
            txtTrain1_no.BackColor = Nothing
            txtTrain1_no.BorderWidth = Nothing
        Else
            txtTrain1_no.ReadOnly = True
            txtTrain1_no.BackColor = Drawing.Color.Lavender
            txtTrain1_no.BorderWidth = 0
        End If
        changeTrain = "Y"
        mstrTrainDesc1 = ddlTrain1.SelectedValue
        mstrTrainDesc2 = txtTrain1_no.Text
        mstrTrainDesc3 = ddlTrain2.SelectedValue
        mstrTrainDesc4 = ddlTypeTrain.SelectedValue
        mstrTrainDesc5 = txtotherTrain.Text
        mstrTrainDesc6 = txtTrainTopic.Text
        mstrTrainDesc7 = txtMonitor.Text
        mstrOverall_1st_1 = txtOverallComment_1st_1.Text
        mstrOverall_1st_2 = txtOverallComment_1st_2.Text
        mstrOverall_1st_3 = txtOverallComment_1st_3.Text
        mstrOverall_2nd_1 = txtOverallComment_2nd_1.Text
        mstrOverall_2nd_2 = txtOverallComment_2nd_2.Text
        mstrOverall_2nd_3 = txtOverallComment_2nd_3.Text
        mstrFeedback_1st_1 = txtFeedBack_1.Text
        mstrFeedback_1st_2 = txtFeedBack_2.Text
        mstrFeedback_1st_3 = txtFeedBack_3.Text
        mstrRating1 = cboRanking_1.SelectedValue
        mstrRating2 = cboRanking_2.SelectedValue 

    End Sub

    Protected Sub ddlTrain2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTrain2.SelectedIndexChanged
        If hfLevel.Value = 1 And ddlTrain2.SelectedValue = "YES" Then
            ddlTypeTrain.Enabled = True
            ddlTypeTrain.BackColor = Nothing
            ddlTypeTrain.BorderWidth = Nothing
            txtotherTrain.ReadOnly = False
            txtotherTrain.BackColor = Nothing
            txtotherTrain.BorderWidth = Nothing
            txtMonitor.ReadOnly = False
            txtMonitor.BackColor = Nothing
            txtMonitor.BorderWidth = Nothing
        Else
            ddlTypeTrain.Enabled = False
            ddlTypeTrain.BackColor = Drawing.Color.Lavender
            ddlTypeTrain.BorderWidth = 0
            txtotherTrain.ReadOnly = True
            txtotherTrain.BackColor = Drawing.Color.Lavender
            txtotherTrain.BorderWidth = 0
            txtMonitor.ReadOnly = True
            txtMonitor.BackColor = Drawing.Color.Lavender
            txtMonitor.BorderWidth = 0
        End If
        changeTrain = "Y"
        mstrTrainDesc1 = ddlTrain1.SelectedValue
        mstrTrainDesc2 = txtTrain1_no.Text
        mstrTrainDesc3 = ddlTrain2.SelectedValue
        mstrTrainDesc4 = ddlTypeTrain.SelectedValue
        mstrTrainDesc5 = txtotherTrain.Text
        mstrTrainDesc6 = txtTrainTopic.Text
        mstrTrainDesc7 = txtMonitor.Text
        mstrOverall_1st_1 = txtOverallComment_1st_1.Text
        mstrOverall_1st_2 = txtOverallComment_1st_2.Text
        mstrOverall_1st_3 = txtOverallComment_1st_3.Text
        mstrOverall_2nd_1 = txtOverallComment_2nd_1.Text
        mstrOverall_2nd_2 = txtOverallComment_2nd_2.Text
        mstrOverall_2nd_3 = txtOverallComment_2nd_3.Text
        mstrFeedback_1st_1 = txtFeedBack_1.Text
        mstrFeedback_1st_2 = txtFeedBack_2.Text
        mstrFeedback_1st_3 = txtFeedBack_3.Text
        mstrRating1 = cboRanking_1.SelectedValue
        mstrRating2 = cboRanking_2.SelectedValue
    End Sub

    Protected Sub ddlTypeTrain_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTypeTrain.SelectedIndexChanged
        If hfLevel.Value = 1 And ddlTypeTrain.SelectedValue = "E" Then
            txtTrainTopic.ReadOnly = False
            txtTrainTopic.BackColor = Nothing
            txtTrainTopic.BorderWidth = Nothing
        Else
            txtTrainTopic.ReadOnly = True
            txtTrainTopic.BackColor = Drawing.Color.Lavender
            txtTrainTopic.BorderWidth = 0
        End If
        changeTrain = "Y"
        mstrTrainDesc1 = ddlTrain1.SelectedValue
        mstrTrainDesc2 = txtTrain1_no.Text
        mstrTrainDesc3 = ddlTrain2.SelectedValue
        mstrTrainDesc4 = ddlTypeTrain.SelectedValue
        mstrTrainDesc5 = txtotherTrain.Text
        mstrTrainDesc6 = txtTrainTopic.Text
        mstrTrainDesc7 = txtMonitor.Text
        mstrOverall_1st_1 = txtOverallComment_1st_1.Text
        mstrOverall_1st_2 = txtOverallComment_1st_2.Text
        mstrOverall_1st_3 = txtOverallComment_1st_3.Text
        mstrOverall_2nd_1 = txtOverallComment_2nd_1.Text
        mstrOverall_2nd_2 = txtOverallComment_2nd_2.Text
        mstrOverall_2nd_3 = txtOverallComment_2nd_3.Text
        mstrFeedback_1st_1 = txtFeedBack_1.Text
        mstrFeedback_1st_2 = txtFeedBack_2.Text
        mstrFeedback_1st_3 = txtFeedBack_3.Text
        mstrRating1 = cboRanking_1.SelectedValue
        mstrRating2 = cboRanking_2.SelectedValue
    End Sub
End Class