Imports System
Imports System.IO
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Configuration
Namespace Forum
    ''' <summary>
    ''' Summary description for NewMessage.
    ''' </summary>
    Partial Public Class NewMessage
        Inherits System.Web.UI.Page
        Private commentid As Integer = 1
        Private articleid As String = "empty"
        Private name As String = "empty"
        Private company As String = "empty"
        Private mReportTitle As String = "This is test article."
        Private folderName As String = "0"
        Private cs As New clsSQL
        Private sUrl As String = System.AppDomain.CurrentDomain.BaseDirectory() + "Pages\forum\"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            ' Put user code to initialize the page here
            If Request.QueryString("reporttitle") IsNot Nothing Then
                mReportTitle = Request.QueryString("reporttitle").ToString()
                lblStatus.Text = mReportTitle
            End If

            If Request.QueryString("id") IsNot Nothing Then
                articleid = Request.QueryString("id")
            Else
                articleid = Session("EmpID").ToString()
            End If

            If Request.QueryString("name") IsNot Nothing Then
                name = Request.QueryString("name")
            Else
                name = employeename.Value.ToString()
            End If

            If Request.QueryString("company") IsNot Nothing Then
                company = Request.QueryString("company")
            Else
                company = Session("Company").ToString()
            End If

            If Request.QueryString("foldername") IsNot Nothing Then
                folderName = Request.QueryString("foldername")
            Else
                folderName = foldernametxt.Value.ToString()
            End If

            If Page.IsPostBack Then

            Else
                Dim myC As New SqlConnection()
                myC.ConnectionString = cs.GetConnectionString()
                Dim myQuery As String = ""
                myQuery = "select Name from User_Profile where Code='" + articleid.ToString() + "' "

                myC.Open()
                Dim myCommand As New SqlCommand(myQuery, myC)
                Dim myReader As SqlDataReader = myCommand.ExecuteReader()

                If myReader.HasRows Then
                    If myReader.Read() Then
                        employeename.Value = myReader("Name").ToString()
                        name = employeename.Value.ToString()
                    End If
                End If
                myReader.Close()
                myC.Close()

                Dim myclas As New clsDataAccess()
                myclas.openConnection()
                Dim fn As String = myclas.getFolderName()
                myclas.closeConnection()
                Directory.CreateDirectory(sUrl + "Upload\" + fn)
                Directory.CreateDirectory(sUrl + "Upload\" + fn + "\files")
                foldernametxt.Value = fn.ToString()
                folderName = fn

                sessionidtxt.Value = Session.SessionID().ToString()
            End If

        End Sub
        Private Sub LoadComment()

        End Sub


#Region "Web Form Designer generated code"
        Protected Overloads Overrides Sub OnInit(ByVal e As EventArgs)
            '
            ' CODEGEN: This call is required by the ASP.NET Web Form Designer.
            '
            InitializeComponent()
            MyBase.OnInit(e)
        End Sub

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()

        End Sub
#End Region

        Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim mParentId As Integer = 0
            Dim mArticleId As String = articleid
            Dim mTitle As String = ""
            Dim mUserTo As String = ""
            Dim mUserCc As String = ""
            Dim mDescription As String = "Test Description"
            Dim mIndent As Integer = 0
            Dim strSessionId As String = ""


            Try
                'mReportTitle = "This is test article."
                mTitle = txtsubject.Text
                mUserTo = txtto.Text
                mUserCc = txtcc.Text
                mDescription = txtcomment.Text
                Dim mCommentType As Integer = 1
                If MsgType_2.Checked Then
                    mCommentType = 2
                End If
                'if (MsgType_3.Checked)
                ' mCommentType = 3;
                If MsgType_4.Checked Then
                    mCommentType = 4
                End If

                If uploadtf.Value.ToString() = "true" Then
                    strSessionId = sessionidtxt.Value.ToString()
                End If




                If IsValid Then
                    Dim myC As New SqlConnection()
                    myC.ConnectionString = cs.GetConnectionString()
                    Dim sqlQuery As String = "INSERT into FORUM_Comments(ParentId,ArticleId,ReportTitle, Title,UserName,Description,Indent,CommentType,AttachmentID,ASessionId,NewMessage) VALUES (N'" + Convert.ToString(mParentId) + "',@Article,N'" + mReportTitle + "',@title,N'" + mArticleId.ToString() + " [" + name + "]',@description,N'" + Convert.ToString(mIndent) + "',N'" + Convert.ToString(mCommentType) + "',N'" + folderName.ToString() + "',N'" + strSessionId.ToString() + "', N'" + articleid + "')"

                    myC.Open()
                    Dim myCommand As New SqlCommand()

                    Dim newSqlParam As New SqlParameter()
                    newSqlParam.ParameterName = "@title"
                    newSqlParam.SqlDbType = SqlDbType.NVarChar
                    'newSqlParam.Direction = ParameterDirection.Input
                    newSqlParam.Value = mTitle
                    myCommand.Parameters.Add(newSqlParam)
                    Dim newSqlParam2 As New SqlParameter()
                    newSqlParam2.ParameterName = "@description"
                    newSqlParam2.SqlDbType = SqlDbType.NVarChar
                    'newSqlParam2.Direction = ParameterDirection.Input
                    newSqlParam2.Value = mDescription
                    myCommand.Parameters.Add(newSqlParam2)

                    Dim newSqlParam3 As New SqlParameter()
                    newSqlParam3.ParameterName = "@Article"
                    newSqlParam3.SqlDbType = SqlDbType.NVarChar
                    'newSqlParam3.Direction = ParameterDirection.Input
                    newSqlParam3.Value = mArticleId + " [" + name + "]; " + txtto.Text + "; " + txtcc.Text + "; " + txtother.Text
                    myCommand.Parameters.Add(newSqlParam3)

                    myCommand.CommandText = sqlQuery
                    myCommand.Connection = myC

                    Dim i As Integer = myCommand.ExecuteNonQuery()
                    myC.Close()

                    lblStatus.ForeColor = Color.Green
                    lblStatus.Text = "Status: Success"
                    Response.Redirect("Forum.aspx?id=" + articleid + "&name=" + name + "&company=" + company)
                End If
            Catch generatedExceptionName As Exception

                lblStatus.ForeColor = Color.Red
                lblStatus.Text = generatedExceptionName.ToString()
            End Try
        End Sub

        Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Directory.Delete(sUrl + "Upload\" + folderName, True)
            Response.Redirect("Forum.aspx?id=" + articleid + "&name=" + name + "&company=" + company)
        End Sub
        Protected Sub showto(ByVal sender As Object, ByVal e As System.EventArgs)
            tocctxt.Value = "1"
            setUserList("1")
            filtercompanylabel.Visible = False
            filtercompanytxt.Visible = False
            showuser.Visible = True
        End Sub
        Protected Sub showcc(ByVal sender As Object, ByVal e As System.EventArgs)
            tocctxt.Value = "2"
            setUserList("1")
            filtercompanylabel.Visible = False
            filtercompanytxt.Visible = False
            showuser.Visible = True
        End Sub
        Protected Sub showother(ByVal sender As Object, ByVal e As System.EventArgs)
            setUserList("2")
            filtercompanylabel.Visible = True
            filtercompanytxt.Visible = True
            showuser.Visible = True
        End Sub
        Protected Sub closeShowUserDialog(ByVal sender As Object, ByVal e As System.EventArgs)
            showuser.Visible = False
        End Sub
        Protected Sub setUserValue1(ByVal sender As Object, ByVal e As System.EventArgs)
            showuser.Visible = False
            If tocctxt.Value = "1" Then
                If txtto.Text = "" Then
                    txtto.Text = ListBox1.SelectedItem.Text
                Else
                    txtto.Text += "; " + ListBox1.SelectedItem.Text
                End If
                Return
            End If

            If tocctxt.Value = "2" Then
                If txtcc.Text = "" Then
                    txtcc.Text = ListBox1.SelectedItem.Text
                Else
                    txtcc.Text += "; " + ListBox1.SelectedItem.Text
                End If
            End If
        End Sub
        Protected Sub setUserValue2(ByVal sender As Object, ByVal e As System.EventArgs)
            If txtother.Text = "" Then
                txtother.Text = ListBox2.SelectedItem.Text
            Else
                txtother.Text += "; " + ListBox2.SelectedItem.Text
            End If

            showuser.Visible = False
        End Sub
        Protected Sub filterUserList(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim condition As String = " 1 = 1 "
            Dim SQcondition As String = " 1 = 1 "

            If filtercodetxt.Value.ToString() <> "" Then
                condition = " c.Code LIKE '%" + filtercodetxt.Value.ToString() + "%'"
                SQcondition = " Code LIKE '%" + filtercodetxt.Value.ToString() + "%'"
            End If

            If filternametxt.Value.ToString() <> "" Then
                If condition = "" Then
                    condition = " c.Name LIKE '%" + filternametxt.Value.ToString() + "%'"
                    SQcondition = " Name LIKE '%" + filternametxt.Value.ToString() + "%'"
                Else
                    condition += " and " + " c.Name LIKE '%" + filternametxt.Value.ToString() + "%' "
                    SQcondition += " and " + " Name LIKE '%" + filternametxt.Value.ToString() + "%' "
                End If
            End If

            If showusertxt.Value.ToString() = "1" Then
                ListBox1.Items.Clear()
                If condition = "" Then
                    condition = " a.Company_Profile_Code = '" + company + "'"
                    SQcondition = " Company_Profile_Code = '" + company + "'"
                Else
                    condition += " and a.Company_Profile_Code = '" + company + "'"
                    SQcondition += " and Company_Profile_Code = '" + company + "'"
                End If
            Else
                If filtercompanytxt.Value.ToString() <> "" Then
                    If condition = "" Then
                        condition = " c.Company_Profile_Code LIKE '%" + filtercompanytxt.Value.ToString() + "%'"
                        SQcondition = " Company_Profile_Code LIKE '%" + filtercompanytxt.Value.ToString() + "%'"
                    Else
                        condition += " and c.Company_Profile_Code LIKE '%" + filtercompanytxt.Value.ToString() + "%'"
                        SQcondition += " and Company_Profile_Code LIKE '%" + filtercompanytxt.Value.ToString() + "%'"
                    End If
                Else
                    If condition = "" Then
                        condition = " a.Company_Profile_Code <> '" + company + "'"
                        SQcondition = " Company_Profile_Code <> '" + company + "'"
                    Else
                        condition += " and a.Company_Profile_Code <> '" + company + "'"
                        SQcondition += " and Company_Profile_Code <> '" + company + "'"
                    End If
                End If
                ListBox2.Items.Clear()
            End If

            Dim myC As New SqlConnection()
            myC.ConnectionString = cs.GetConnectionString()
            Dim myQuery As String = ""

            myQuery = "select distinct  c.CodeName  from dbo.User_Employee_Group as a inner join dbo.Employee_Group as b on a.Employee_Group_ID=b.Employee_Group_ID inner join dbo.Employee_CodeName_Vw as c on b.Employee_Profile_ID = c.Employee_Profile_ID where "
            myQuery += condition + " and c.Code <> '" + articleid + "'"

            myQuery += " union  select Code+' ['+Name+']' from user_profile where Code<>'" + articleid + "' and " + SQcondition


            myC.Open()
            Dim myCommand As New SqlCommand(myQuery, myC)

            Dim myReader As SqlDataReader = myCommand.ExecuteReader()

            Dim uItem As System.Web.UI.WebControls.ListItem
            If myReader.HasRows Then
                While myReader.Read()
                    uItem = New System.Web.UI.WebControls.ListItem
                    uItem.Value = myReader("CodeName").ToString()
                    uItem.Text = myReader("CodeName").ToString()

                    If showusertxt.Value.ToString() = "1" Then
                        ListBox1.Items.Add(uItem)
                    Else
                        ListBox2.Items.Add(uItem)
                    End If

                    uItem = Nothing
                End While
            End If
            myReader.Close()
            myC.Close()

        End Sub
        Private Sub setUserList(ByVal ishow As String)
            Dim condition As String = " 1 = 1 "
            Dim SQcondition As String = " 1 = 1 "
            If showusertxt.Value.ToString() <> ishow Then

                showusertxt.Value = ishow

                If ishow = "1" Then
                    condition = " a.Company_Profile_Code = '" + company + "'"
                    SQcondition = " Company_Profile_Code = '" + company + "'"
                    ListBox1.Visible = True
                    ListBox2.Visible = False
                    If Convert.ToInt32(ListBox1.Items.Count.ToString()) >= 1 Then
                        Return
                    End If
                Else
                    condition = " a.Company_Profile_Code <> '" + company + "'"
                    SQcondition = " Company_Profile_Code <> '" + company + "'"
                    ListBox1.Visible = False
                    ListBox2.Visible = True
                    If Convert.ToInt32(ListBox2.Items.Count.ToString()) >= 1 Then
                        Return
                    End If
                End If

                Dim myC As New SqlConnection()
                myC.ConnectionString = cs.GetConnectionString()
                Dim myQuery As String = ""

                myQuery = "select distinct  c.CodeName  from dbo.User_Employee_Group as a inner join dbo.Employee_Group as b on a.Employee_Group_ID=b.Employee_Group_ID inner join dbo.Employee_CodeName_Vw as c on b.Employee_Profile_ID = c.Employee_Profile_ID where "
                myQuery += condition + " and c.Code <> '" + articleid + "'"

                myQuery += " union select Code+' ['+Name+']' from user_profile where Code<>'" + articleid + "' and " + SQcondition

                myC.Open()
                Dim myCommand As New SqlCommand(myQuery, myC)

                Dim myReader As SqlDataReader = myCommand.ExecuteReader()

                Dim uItem As System.Web.UI.WebControls.ListItem
                If myReader.HasRows Then
                    While myReader.Read()
                        uItem = New System.Web.UI.WebControls.ListItem
                        uItem.Value = myReader("CodeName").ToString()
                        uItem.Text = myReader("CodeName").ToString()

                        If showusertxt.Value.ToString() = "1" Then
                            ListBox1.Items.Add(uItem)
                        Else
                            ListBox2.Items.Add(uItem)
                        End If

                        uItem = Nothing
                    End While
                End If
                myReader.Close()
                myC.Close()
            End If
        End Sub
        Protected Sub uploadfileAdd(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                If Not Directory.Exists("") Then
                    Directory.CreateDirectory(sUrl + "Upload\" + foldernametxt.Value.ToString() + "\files\" + sessionidtxt.Value.ToString())
                    uploadtf.Value = "true"
                End If

                Dim filename As String = upload1.FileName.ToString()
                upload1.SaveAs(sUrl + "Upload\" + folderName + "\files\" + sessionidtxt.Value.ToString() + "\" + filename)
                Dim uItem As System.Web.UI.WebControls.ListItem = New System.Web.UI.WebControls.ListItem
                uItem.Value = filename
                uItem.Text = filename
                fileuploadListBox.Items.Add(uItem)
                uItem = Nothing
            Catch ex As Exception

            End Try

        End Sub
        Protected Sub uploadfileDelete(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Dim deletename As String = fileuploadListBox.SelectedItem.Value.ToString()
                fileuploadListBox.Items.RemoveAt(fileuploadListBox.SelectedIndex)
                File.Delete(sUrl + "Upload\\" + folderName + "\\files\\" + sessionidtxt.Value.ToString() + "\\" + deletename)
            Catch ex As Exception

            End Try

        End Sub
        Protected Sub OpenCSUpload(ByVal sender As Object, ByVal e As System.EventArgs)
            Page.RegisterStartupScript("UploadSC2", "<script>window.open('ShowScreen.aspx?foldername=" + folderName + "&view=no', 'UploadAndView','menubar=0,resizable=1,width=800,height=600,scrollbars=yes').focus(); </script>")

        End Sub
        Protected Sub txtProfile_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        End Sub

        Protected Sub MsgType_5_ServerChange(ByVal sender As Object, ByVal e As System.EventArgs)

        End Sub
    End Class
End Namespace
