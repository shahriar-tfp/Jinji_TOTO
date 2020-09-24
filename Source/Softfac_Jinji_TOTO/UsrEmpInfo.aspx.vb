Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class Top
    Inherits System.Web.UI.Page

    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting, myDS, myDS1, myDS2 As New DataSet, MyDT As DataTable
    Dim ssql, ssql1, ssql2 As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then
            PagePreload()
            LoadPersonalInfo()
        End If

    End Sub

    Private Sub PagePreload()
        Try
            ssql = "Select [Name],Employee_Profile_ID From User_Profile Where Company_Profile_Code = '" & Session("Company") & "' And Code ='" & Session("EmpID") & "'"
            myDS = mySQL.ExecuteSQL(ssql)
            If myDS.Tables.Count > 0 Then
                If myDS.Tables(0).Rows.Count > 0 Then
                    lblName.Text = "Name : " & myDS.Tables(0).Rows(0).Item(0).ToString
                    lblCode.Text = "Code : " & UCase(Session("EmpID"))

                    If Trim(myDS.Tables(0).Rows(0).Item(1).ToString) <> "" Or Trim(myDS.Tables(0).Rows(0).Item(1).ToString) = "0" Then
                        ssql2 = "Select Code From Employee_Profile Where ID = '" & Trim(myDS.Tables(0).Rows(0).Item(1).ToString) & "' And Company_Profile_Code = '" & Session("Company") & "'"
                        myDS2 = mySQL.ExecuteSQL(ssql2)
                        If myDS2.Tables.Count > 0 Then
                            If myDS2.Tables(0).Rows.Count > 0 Then
                                pnlEmpInfo.Visible = True
                                ssql1 = "Exec sp_sa_leave_user_info '" & Session("Company") & "','" & myDS2.Tables(0).Rows(0).Item(0).ToString & "'"
                                myDS1 = mySQL.ExecuteSQL(ssql1)
                                If myDS1.Tables.Count > 0 Then
                                    If myDS1.Tables(0).Rows.Count > 0 Then
                                        lblName.Text = "Name : " & myDS1.Tables(0).Rows(0).Item(0).ToString
                                        lblDepartment.Text = "Deparment : " & myDS1.Tables(0).Rows(0).Item(6).ToString
                                        lblJobGrade.Text = "Job Grade : " & myDS1.Tables(0).Rows(0).Item(8).ToString
                                        lblJobTitle.Text = "Job Title : " & myDS1.Tables(0).Rows(0).Item(9).ToString
                                        lblSupervisor.Text = "Supervisor : " & myDS1.Tables(0).Rows(0).Item(10).ToString
                                        lblLeaveApprovalLevel1.Text = "ApprovalLevel : " & myDS1.Tables(0).Rows(0).Item(11).ToString
                                        lblLeaveApprovalUser1.Text = "Approval User : " & myDS1.Tables(0).Rows(0).Item(12).ToString
                                        lblLeaveApprovalLevel2.Text = "ApprovalLevel : " & myDS1.Tables(0).Rows(0).Item(13).ToString
                                        lblLeaveApprovalUser2.Text = "Approval User : " & myDS1.Tables(0).Rows(0).Item(14).ToString
                                    End If
                                End If
                                myDS1 = Nothing
                            End If
                        End If
                        myDS2 = Nothing
                    End If
                End If
            End If
            myDS = Nothing
        Catch ex As Exception
            myDS = Nothing
            myDS1 = Nothing
        End Try
    End Sub

    Private Sub LoadPersonalInfo()

        ssql = "Select Image_Name,Image_Path From Employee_Photo Where Company_Profile_Code='" & Session("Company").ToString & _
                "' And Employee_Profile_ID='" & mySetting.GetEmployeeIDbyCode_ReturnString(Session("Company").ToString, Session("EmpID").ToString.Replace("&amp;", "&")) & "'"
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If Not myDS Is Nothing Then
            If myDS.Tables(0).Rows.Count > 0 Then
                imgPreview.Visible = True
                imgPreview.ImageUrl = myDS.Tables(0).Rows(0).Item(1).ToString
            Else
                imgPreview.Visible = False
            End If
        Else
            imgPreview.Visible = False
        End If
    End Sub

End Class
