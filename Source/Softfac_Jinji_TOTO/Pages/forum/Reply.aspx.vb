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
    Partial Public Class Reply
        Inherits System.Web.UI.Page

        Private articleid As String = "empty"
        Private myindent As Integer = 0
        Private commentid As Integer = 1
        Private name As String = "empty"
        Private company As String = "empty"
        Private folderName As String = "0"
        Private cs As New clsSQL
        Private sUrl As String = System.AppDomain.CurrentDomain.BaseDirectory() + "\Pages\forum\"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            ' Put user code to initialize the page here
            LoadComment()
        End Sub
        Private Sub LoadComment()
            Dim topicsubject As String = ""

            If Request.QueryString("id") IsNot Nothing Then
                articleid = Request.QueryString("id")
            End If

            If Request.QueryString("name") IsNot Nothing Then
                name = Request.QueryString("name")
            Else

            End If

            If Request.QueryString("company") IsNot Nothing Then
                company = Request.QueryString("company")
            End If



            If Request.QueryString("CId") IsNot Nothing Then
                commentid = Convert.ToInt32(Request.QueryString("CId"))

                Dim myC As New SqlConnection()
                myC.ConnectionString = cs.GetConnectionString()
                Dim myQuery As String = ""

                myQuery = "SELECT ReportTitle, UserName, Title, Description, DateAdded, AttachmentID, Indent,ASessionId FROM FORUM_Comments  WHERE ID=" + Convert.ToString(commentid)
                myC.Open()
                Dim myCommand As New SqlCommand(myQuery, myC)

                Dim myReader As SqlDataReader = myCommand.ExecuteReader()
                If myReader.HasRows Then
                    While myReader.Read()
                        lblStatus.Text = myReader("ReportTitle").ToString()
                        lblname.Text = myReader("UserName").ToString()
                        lbltitle.Text = myReader("Title").ToString()
                        topicsubject = "Re: " + lbltitle.Text
                        lblComment.Text = myReader("Description").ToString()
                        lblDate.Text = myReader("DateAdded").ToString()
                        folderName = myReader("AttachmentID").ToString()
                        myindent = Convert.ToInt32(myReader("Indent").ToString()) + 1

                        If myReader("ASessionId").ToString() = "" Then
                            AttactFileButton.Enabled = False
                        Else
                            AttactFileButton.Enabled = True
                        End If
                    End While
                End If
                myReader.Close()
                myC.Close()


                If Page.IsPostBack() Then

                Else
                    txtsubject.Text = topicsubject


                    'Dim fileLists As String() = Directory.GetFiles(sUrl + "Upload\" + folderName + "\files", "*.*")
                    'Dim uItem As System.Web.UI.WebControls.ListItem
                    'For Each fn As String In fileLists
                    '    fn = Path.GetFileName(fn)
                    '    uItem = New System.Web.UI.WebControls.ListItem
                    '    uItem.Value = fn.ToString()
                    '    uItem.Text = fn.ToString()
                    '    fileuploadListBox.Items.Add(uItem)
                    '    uItem = Nothing
                    'Next
                    sessionidtxt.Value = Session.SessionID().ToString()


                End If
            End If
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
            Dim mParentId As Integer = commentid
            Dim mArticleId As String = articleid

            Dim mTitle As String = "Test"
            Dim mDescription As String = "Test Description"
            Dim mIndent As Integer = myindent
            Dim mProfile As String = ""
            Dim strSessionId As String = ""

            Try
                mTitle = txtsubject.Text
                mDescription = txtcomment.Text

                Dim mCommentType As Integer = 1

                If MsgType_2.Checked Then
                    mCommentType = 2
                End If
                If MsgType_3.Checked Then
                    mCommentType = 3
                End If
                If MsgType_4.Checked Then
                    mCommentType = 4
                End If

                If uploadtf.Value.ToString() = "true" Then
                    strSessionId = sessionidtxt.Value.ToString()
                End If


                If IsValid Then
                    Dim myC As New SqlConnection()
                    myC.ConnectionString = cs.GetConnectionString()
                    Dim sqlQuery As String = ""
                    sqlQuery = "DECLARE @ai varchar(500),@rt varchar(500),@at varchar(500) "
                    sqlQuery += " SET @ai=(select ArticleId from FORUM_Comments where ID='" + mParentId.ToString() + "') "
                    sqlQuery += " SET @rt=(select ReportTitle from FORUM_Comments where ID='" + mParentId.ToString() + "') "
                    sqlQuery += " SET @at=(select AttachmentID from FORUM_Comments where ID='" + mParentId.ToString() + "') "
                    sqlQuery += " INSERT into FORUM_Comments(ParentId,ArticleId,ReportTitle,Title,UserName,Description,Indent,CommentType,AttachmentID,ASessionId,NewMessage) VALUES (N'" + Convert.ToString(mParentId) + "',@ai,@rt,@title,N'" + articleid + " [" + name + "]',@description,N'" + Convert.ToString(mIndent) + "',N'" + Convert.ToString(mCommentType) + "',@at,N'" + strSessionId.ToString() + "', N'" + articleid + "')"
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

                    myCommand.CommandText = sqlQuery
                    myCommand.Connection = myC
                    Dim i As Integer = myCommand.ExecuteNonQuery()
                    myC.Close()

                    Dim toother As String = ""
                    If txtcc.Text.ToString() <> "" Then
                        toother = "; " + txtcc.Text.ToString()
                    End If
                    If txtother.Text.ToString() <> "" Then
                        toother += "; " + txtother.Text.ToString()
                    End If
                    If toother <> "" Then
                        Dim [myclass] As New clsDataAccess()
                        [myclass].openConnection()
                        Dim myReturn As Boolean = [myclass].UpdateArticleID(toother, commentid, "add")
                        [myclass].closeConnection()
                    End If
                    lblStatus.ForeColor = Color.Green
                    lblStatus.Text = "Status: Success"

                    Response.Redirect("Forum.aspx?id=" + articleid + "&name=" + name + "&company=" + company)
                End If
            Catch generatedExceptionName As Exception

                lblStatus.ForeColor = Color.Red
                lblStatus.Text = "Status: Error"
            End Try
        End Sub

        Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Response.Redirect("Forum.aspx?id=" + articleid + "&name=" + name + "&company=" + company)
        End Sub

        Protected Sub txtProfile_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        End Sub

        Protected Sub MsgType_5_ServerChange(ByVal sender As Object, ByVal e As System.EventArgs)

        End Sub

        Protected Sub MsgType_2_ServerChange(ByVal sender As Object, ByVal e As System.EventArgs)

        End Sub

        Protected Sub MsgType_3_ServerChange(ByVal sender As Object, ByVal e As System.EventArgs)

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
            showuser.Visible = False

            If txtother.Text = "" Then
                txtother.Text = ListBox2.SelectedItem.Text
            Else
                txtother.Text += "; " + ListBox2.SelectedItem.Text
            End If
        End Sub
        Private Sub setUserList(ByVal ishow As String)
            Dim condition As String = " 1 = 1 "
            If showusertxt.Value.ToString() <> ishow Then

                showusertxt.Value = ishow

                If ishow = "1" Then
                    condition = " a.Company_Profile_Code = '" + company + "'"
                    ListBox1.Visible = True
                    ListBox2.Visible = False
                    If Convert.ToInt32(ListBox1.Items.Count.ToString()) >= 1 Then
                        Return
                    End If
                Else
                    condition = " a.Company_Profile_Code <> '" + company + "'"
                    ListBox1.Visible = False
                    ListBox2.Visible = True
                    If Convert.ToInt32(ListBox2.Items.Count.ToString()) >= 1 Then
                        Return
                    End If
                End If

                Dim myC As New SqlConnection()
                myC.ConnectionString = cs.GetConnectionString()
                Dim myQuery As String = ""

                myQuery = "select distinct c.Employee_Profile_ID, c.Company_Profile_Code, c.Code, c.Name, c.CodeName  from dbo.User_Employee_Group as a inner join dbo.Employee_Group as b on a.Employee_Group_ID=b.Employee_Group_ID inner join dbo.Employee_CodeName_Vw as c on b.Employee_Profile_ID = c.Employee_Profile_ID where "
                myQuery += condition + " and c.Code <> '" + articleid + "'"

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
        Protected Sub filterUserList(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim condition As String = ""

            If filtercodetxt.Value.ToString() <> "" Then
                condition = " c.Code LIKE '%" + filtercodetxt.Value.ToString() + "%'"
            End If

            If filternametxt.Value.ToString() <> "" Then
                If condition = "" Then
                    condition = " c.Name LIKE '%" + filternametxt.Value.ToString() + "%'"
                Else
                    condition += " and " + " c.Name LIKE '%" + filternametxt.Value.ToString() + "%' "
                End If
            End If

            If showusertxt.Value.ToString() = "1" Then
                ListBox1.Items.Clear()
                If condition = "" Then
                    condition = " a.Company_Profile_Code = '" + company + "'"
                Else
                    condition += " and a.Company_Profile_Code = '" + company + "'"
                End If
            Else
                If filtercompanytxt.Value.ToString() <> "" Then
                    If condition = "" Then
                        condition = " c.Company_Profile_Code LIKE '%" + filtercompanytxt.Value.ToString() + "%'"
                    Else
                        condition += " and c.Company_Profile_Code LIKE '%" + filtercompanytxt.Value.ToString() + "%'"
                    End If
                Else
                    If condition = "" Then
                        condition = " a.Company_Profile_Code <> '" + company + "'"
                    Else
                        condition = " and a.Company_Profile_Code <> '" + company + "'"
                    End If
                End If
                ListBox2.Items.Clear()
            End If

            Dim myC As New SqlConnection()
            myC.ConnectionString = cs.GetConnectionString()
            Dim myQuery As String = ""

            myQuery = "select distinct c.Employee_Profile_ID, c.Company_Profile_Code, c.Code, c.Name, c.CodeName  from dbo.User_Employee_Group as a inner join dbo.Employee_Group as b on a.Employee_Group_ID=b.Employee_Group_ID inner join dbo.Employee_CodeName_Vw as c on b.Employee_Profile_ID = c.Employee_Profile_ID where "
            myQuery += condition + " and c.Code <> '" + articleid + "'"

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
        Protected Sub closeShowUserDialog(ByVal sender As Object, ByVal e As System.EventArgs)
            showuser.Visible = False
        End Sub
        Protected Sub uploadfileAdd(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                If Not Directory.Exists("") Then
                    Directory.CreateDirectory(sUrl + "Upload\\" + folderName.ToString() + "\\files\\" + sessionidtxt.Value.ToString())
                    uploadtf.Value = "true"
                End If

                Dim filename As String = upload1.FileName.ToString()
                upload1.SaveAs(sUrl + "Upload\\" + folderName + "\\files\\" + sessionidtxt.Value.ToString() + "\\" + filename)

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
        Protected Sub viewcommentshow(ByVal sender As Object, ByVal e As System.EventArgs)
            If viewcommenttable.Visible = False Then
                viewcommenttable.Visible = True
            Else
                viewcommenttable.Visible = False
            End If


        End Sub
        Protected Sub replycommentshow(ByVal sender As Object, ByVal e As System.EventArgs)
            If replycommenttable.Visible = False Then
                replycommenttable.Visible = True
            Else
                replycommenttable.Visible = False
            End If


        End Sub

        Protected Sub attachfile(ByVal sender As Object, ByVal e As System.EventArgs)
            Page.RegisterStartupScript("attachmentopen", "<script>window.open('FolderList.aspx?id=" + commentid.ToString() + "', 'aaa','menubar=0,resizable=0,width=650,height=250,scrollbars=yes');</script> ")

        End Sub
    End Class
End Namespace