Imports System.Data

Partial Class Reports
    Inherits System.Web.UI.Page

    Private WithEvents mySQL As New clsSQL, myDS As DataSet, myDT As DataTable
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            HttpContext.Current.Session("Report") = Request.QueryString("Report")
            HttpContext.Current.Session("Company") = Request.QueryString("Company")
            HttpContext.Current.Session("EmpID") = Request.QueryString("EmpID")
            HttpContext.Current.Session("Module") = Request.QueryString("Module")
            HttpContext.Current.Session("Table") = Request.QueryString("Table")
            HttpContext.Current.Session("Language") = Request.QueryString("Language")
            HttpContext.Current.Session("FilterField") = Request.QueryString("FilterField")
            HttpContext.Current.Session("FilterCriteria") = Request.QueryString("FilterCriteria")
            'HttpContext.Current.Session("OrderByField") = Request.QueryString("OrderByField")
            HttpContext.Current.Session("OrderByField") = ""
            HttpContext.Current.Session("SelectColumns") = ""
            'HttpContext.Current.Session("SelectColumns") = Request.QueryString("SelectColumns")
            HttpContext.Current.Session("PageSize") = Request.QueryString("PageSize")
            HttpContext.Current.Session("CurrentPage") = Request.QueryString("CurrentPage")
            HttpContext.Current.Session("DebugMode") = Request.QueryString("DebugMode")

            If Not Session("Report") Is Nothing Then
                BindGrid()
            End If
            imgbtnPrint.ImageUrl = "~/_Images/btnPrint.bmp"
            imgbtnPreview.ImageUrl = "~/_Images/btnPreview.jpg"
        Else
            HttpContext.Current.Session("Report") = Request.QueryString("Report")
            HttpContext.Current.Session("Company") = Request.QueryString("Company")
            HttpContext.Current.Session("EmpID") = Request.QueryString("EmpID")
            HttpContext.Current.Session("Module") = Request.QueryString("Module")
            HttpContext.Current.Session("Table") = Request.QueryString("Table")
            HttpContext.Current.Session("Language") = Request.QueryString("Language")
            HttpContext.Current.Session("FilterField") = Request.QueryString("FilterField")
            HttpContext.Current.Session("FilterCriteria") = Request.QueryString("FilterCriteria")
            'HttpContext.Current.Session("OrderByField") = Request.QueryString("OrderByField")
            HttpContext.Current.Session("OrderByField") = ""
            HttpContext.Current.Session("SelectColumns") = ""
            'HttpContext.Current.Session("SelectColumns") = Request.QueryString("SelectColumns")
            HttpContext.Current.Session("PageSize") = Request.QueryString("PageSize")
            HttpContext.Current.Session("CurrentPage") = Request.QueryString("CurrentPage")
            HttpContext.Current.Session("DebugMode") = Request.QueryString("DebugMode")
        End If
    End Sub
    Private Sub BindGrid()
        myDT = New DataTable
        myDT = mySQL.RetrieveSQLDataTable("SELECT Code,Name FROM Table_Field Where Table_Profile_Code=N'" & HttpContext.Current.Session("Table") & "' ORDER BY Sequence_No")
        If Not myDT Is Nothing Then
            myGridView.DataSource = myDT
            myGridView.DataBind()

            For i As Integer = 0 To myGridView.Rows.Count - 1
                myGridView.Rows(i).Cells(1).Visible = False
            Next
            myGridView.HeaderRow.Cells(1).Visible = False
        End If
        myDT = Nothing
    End Sub
    'Protected Sub SubmitButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SubmitButton.Click
    '    Dim cols As String = GetFilterField()
    '    HttpContext.Current.Session("SelectColumns") = cols
    '    If cols <> "" Then
    '        Dim myTargetURL As String = "rdPage.aspx?rdReport=" & Session("Report").ToString
    '        Response.Redirect(myTargetURL)
    '    End If
    'End Sub
    'Protected Sub PrintViewButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PrnViewButton.Click
    '    Dim cols As String = GetFilterField()

    '    If cols <> "" Then
    '        Dim url As String = ""
    '        url = "http://" + Request.ServerVariables("SERVER_NAME") + ":8383/Sample4Calvin/rdPage.aspx?rdReport=Plugin.SetDataTableColumns&rdPaging=Printable&rdShowModes=Printable&rdRequestForwarding=Form&dySelect=" & cols
    '        Response.Redirect(url)
    '    End If
    'End Sub
    Function GetFilterField() As String
        Dim strFilterField As String = ""
        For i As Integer = 0 To myGridView.Rows.Count - 1
            If CType(myGridView.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True Then
                strFilterField &= myGridView.Rows(i).Cells(1).Text.ToString & ","
            End If
        Next
        If Len(strFilterField) > 0 Then
            If Right(strFilterField, 1) = "," Then
                strFilterField = Left(strFilterField, Len(strFilterField) - 1)
            End If
        End If
        Return strFilterField
    End Function

    Protected Sub chkSelectAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        If chkSelectAll.Checked = True Then
            For i As Integer = 0 To myGridView.Rows.Count - 1
                CType(myGridView.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True
            Next
            chkDeselectAll.Checked = False
        End If
    End Sub

    Protected Sub chkDeselectAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDeselectAll.CheckedChanged
        If chkDeselectAll.Checked = True Then
            For i As Integer = 0 To myGridView.Rows.Count - 1
                CType(myGridView.Rows(i).Cells(0).Controls(1), CheckBox).Checked = False
            Next
            chkSelectAll.Checked = False
        End If
    End Sub

    Protected Sub imgbtnPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnPrint.Click
        Dim cols As String = GetFilterField()
        HttpContext.Current.Session("SelectColumns") = cols
        If cols <> "" Then
            Dim myTargetURL As String = "rdPage.aspx?rdReport=Report"
            Response.Redirect(myTargetURL)
        End If
    End Sub

    Protected Sub imgbtnPreview_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnPreview.Click
        Dim cols As String = GetFilterField()
        HttpContext.Current.Session("SelectColumns") = cols
        If cols <> "" Then
            Dim url As String = ""
            Dim myTargetURL As String = "rdPage.aspx?rdReport=Report&rdPaging=Printable&rdShowModes=Printable&rdRequestForwarding=Form&SelectColumn=" & cols
            'url = "http://" + Request.ServerVariables("SERVER_NAME") + ":8383/Sample4Calvin/rdPage.aspx?rdReport=Plugin.SetDataTableColumns&rdPaging=Printable&rdShowModes=Printable&rdRequestForwarding=Form&dySelect=" & cols
            Response.Redirect(myTargetURL)
        End If
    End Sub

End Class
