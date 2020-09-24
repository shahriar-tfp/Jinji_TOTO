Imports System
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
    ''' Summary description for Delete.
    ''' </summary>
    Partial Public Class Delete
        Inherits System.Web.UI.Page
        Private articleid As String = "empty"
        Private commentid As Integer = 0
        Private psize As Integer = 20
        Private name As String = ""
        Private type As String = ""
        Private company As String = ""
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            ' Put user code to initialize the page here
            If Request.QueryString("id") IsNot Nothing Then
                articleid = Request.QueryString("id")
            End If

            If Request.QueryString("name") IsNot Nothing Then
                name = Request.QueryString("name")
            End If

            If Request.QueryString("CId") IsNot Nothing Then
                commentid = Convert.ToInt32(Request.QueryString("CId"))
            End If

            If Request.QueryString("pagesize") IsNot Nothing Then
                psize = Convert.ToInt32(Request.QueryString("pagesize"))
            End If

            If Request.QueryString("company") IsNot Nothing Then
                company = Request.QueryString("company")
            End If

            If Request.QueryString("type") IsNot Nothing Then
                type = Request.QueryString("type")
            End If

            'Try


            Dim [myclass] As New clsDataAccess()
            [myclass].openConnection()
            Dim myReturn As Boolean = [myclass].UpdateArticleID(articleid + " [" + name + "]", commentid, type)
            [myclass].closeConnection()

            Response.Redirect("Forum.aspx?id=" + articleid + "&pagesize=" + Convert.ToString(psize) + "&name=" + name + "&company=" + company)
            'Catch generatedExceptionName As Exception

            'Response.Write("<h2> Unexpected error ! Try slamming your head into your computer monitor :)</h2>")
            'End Try


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
    End Class
End Namespace
