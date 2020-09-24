Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Partial Class FolderList
    Inherits System.Web.UI.Page
    Private foldername As String = "temp"
    Private subname As String = "temp"
    Private topicId As String = ""
    Private sUrl As String = System.AppDomain.CurrentDomain.BaseDirectory() + "\Pages\forum\"
    Private cs As New clsSQL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id") IsNot Nothing Then
            topicId = Request.QueryString("id").ToString()
        End If
        If Page.IsPostBack Then

        Else


            Dim myC As New SqlConnection()
            myC.ConnectionString = cs.GetConnectionString()
            Dim myQuery As String = ""

            myQuery = "select AttachmentID, ASessionId FROM FORUM_Comments  WHERE ID=" + topicId.ToString()
            myC.Open()
            Dim myCommand As New SqlCommand(myQuery, myC)

            Dim myReader As SqlDataReader = myCommand.ExecuteReader()
            If myReader.HasRows Then
                While myReader.Read()
                    foldername = myReader("AttachmentID").ToString()
                    subname = myReader("ASessionId").ToString()
                End While
            End If
            myReader.Close()
            myC.Close()


            Dim dirInfo As New DirectoryInfo(sUrl + "upload/" + foldername + "/files/" + subname)

            fileList.DataSource = dirInfo.GetFiles("*.*")
            fileList.DataBind()
        End If
    End Sub
    Sub MyDataList_ItemCommand(ByVal Sender As Object, ByVal E As DataListCommandEventArgs)

        Dim path As String = fileList.DataKeys(E.Item.ItemIndex)
        Dim Command As String = E.CommandName
        If Command = "Download" Then
            Download(path)
        End If
    End Sub
    Public Sub Download(ByVal Path As String)
        Dim res As System.Web.HttpResponse = Page.Response
        res.Clear()
        res.ContentType = "Application/Unknown"
        res.AddHeader("Content-Disposition", "attachment;filename=" + System.IO.Path.GetFileName(Path))
        res.WriteFile(Path)
        res.Flush()
        res.End()
    End Sub



End Class
