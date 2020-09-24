Imports System.Xml

Partial Class rdAjax
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'If HttpContext.Current.Request("rdAjaxCommand") = "UpdateMapImage" Then
        '    Dim rdAwsMap As New rdServer.rdArcWebMap(Nothing, Nothing)
        '    Call rdAwsMap.UpdateMap()
        '    'We won't return from UpdateMap().
        '    Exit Sub
        'End If

        ' ''Dim sXmlRequest As String = HttpContext.Current.Request("rdRequestXML")
        If Not IsNothing(HttpContext.Current.Request("rdAjaxAbort")) Then
            'Ajax isn't working.  
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.PathAndQuery.Replace("rdTemplate/rdAjax/rdAjax.aspx", "rdPage.aspx"))
            Exit Sub
        End If

        Dim rb As New rdServer.ResponseBuilder()
        rb.isAjaxRequest = True
        Call rb.BuildResponse()  'This will send a response and end.

    End Sub


End Class
