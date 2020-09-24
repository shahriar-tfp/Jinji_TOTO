Imports System
Imports System.Data

Partial Class Pages_Global_Lookup
    Inherits System.Web.UI.Page

    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting
    Private myDS As New DataSet, tmpDS As New DataSet, htb As New Hashtable, htbValue As New Hashtable, de As DictionaryEntry
    Dim ssql As String, i As Integer, j As Integer
    Dim strOriginalColour As String = "'#FFFFFF'"
    Dim btnColourDef As String = "BtnBlue"
    Dim btnColourAlt As String = "BtnGrey"
    'Public Property GridViewSortDirection() As SortDirection
    '    Get
    '        If Not ViewState("sortDirection") Is Nothing Then
    '            ViewState("sortDirection") = SortDirection.Ascending
    '        End If
    '    End Get
    '    Set(ByVal value As SortDirection)
    '        ViewState("sortDirection") = value
    '    End Set
    'End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session.IsNewSession Then
            SelfClose()
        End If
        If Not Page.IsPostBack Then
            PagePreload()
            BindData()
        Else
            lblMessage.Text = ""
        End If
    End Sub
    Private Sub PagePreload()
        lblSearchCode.Text = "Search For Code:"
        lblSearchName.Text = "Search For Name:"
        lblSearchCode.Visible = False
        lblSearchName.Visible = False
        lblSearchCode.CssClass = "wordstyle3"
        lblSearchName.CssClass = "wordstyle3"
        txtSearchCode.CssClass = "wordstyle3"
        txtSearchName.CssClass = "wordstyle3"
        lblMessage.CssClass = "wordstyle2"
        'mySetting.GetBtnImgUrl(imgbtnSearch, Session("Company").ToString, btnColourDef, "btnSearch.gif")
        'mySetting.GetBtnImgUrl(imgbtnCancel, Session("Company").ToString, btnColourDef, "btnCancel.gif")
        mySetting.GetImgBtnUrl(imgbtnFilter, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.SetTextBoxPressEnterGoToImageButton(txtSearchCode, imgbtnFilter)
        mySetting.SetTextBoxPressEnterGoToImageButton(txtSearchName, imgbtnFilter)
        txtSearchCode.Focus()
    End Sub
    Private Sub SelfClose()
        Dim strFormName As String = "", strControlName As String = ""
        Dim posSplit As Integer = Request.QueryString("formname").ToString.IndexOf(".")
        Dim strjscript As String = "<script language=""javascript"">"

        strFormName = Left(Request.QueryString("formname").ToString, posSplit)
        Dim strScript As String = ""

        strScript = "<script language=" & """javascript" & """>" & _
                    "self.close();window.opener.location='SessionTimeOut.aspx';" & _
                    ";</" & "script>"
        Page.ClientScript.RegisterStartupScript(GetType(Page), "ClosePage", strScript)
    End Sub
    Sub BindData()
        Dim IsEmp As Boolean = False, test As String

        If Mid(Request.QueryString(0).ToString.ToUpper, Request.QueryString(0).ToString.IndexOf(".") + 5, Len(Request.QueryString(0).ToString)) = "EMPLOYEE_PROFILE_ID" Then
            myDS = New DataSet
            myDS = mySQL.ExecuteSQL("Select dbo.fn_IsEmployee('" & Session("Company").ToString & "','" & Session("EmpID").ToString & "')")
            If Not myDS Is Nothing Then
                If myDS.Tables(0).Rows(0).Item(0).ToString = "TRUE" Then
                    IsEmp = True
                Else
                    IsEmp = False
                End If
            End If
            myDS = Nothing
            myDS = New DataSet
        End If
        test = Request.QueryString("query").ToString.Replace("""", "'")
        myDS = mySQL.ExecuteSQL(Request.QueryString("query").ToString.Replace("""", "'"), Session("Company").ToString, Session("EmpID").ToString)
        If Not myDS Is Nothing Then
            If myDS.Tables(0).Rows.Count > 0 Then
                If IsEmp = True Then
                    Dim tmpDS As New DataTable, tmpDR As DataRow

                    tmpDS.Columns.Add(myDS.Tables(0).Columns(0).ToString)
                    tmpDS.Columns.Add(myDS.Tables(0).Columns(1).ToString)
                    tmpDS.Columns.Add(myDS.Tables(0).Columns(2).ToString)
                    For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                        If myDS.Tables(0).Rows(i).Item(1).ToString.ToUpper = Session("EmpID").ToString.ToUpper Then
                            tmpDR = tmpDS.NewRow
                            tmpDR.Item(0) = myDS.Tables(0).Rows(i).Item(0).ToString
                            tmpDR.Item(1) = myDS.Tables(0).Rows(i).Item(1).ToString
                            tmpDR.Item(2) = myDS.Tables(0).Rows(i).Item(2).ToString
                            tmpDS.Rows.Add(tmpDR)
                        End If
                    Next
                    If tmpDS.Rows.Count > 0 Then
                        tmpDS.PrimaryKey = New DataColumn() {tmpDS.Columns("CodeName"), tmpDS.Columns("Code")}
                    End If

                    Session("DS") = tmpDS.DefaultView
                    grdLookup.DataSource = tmpDS.DefaultView
                    grdLookup.DataBind()
                    AdjustTextBoxWidth
                Else
                    If myDS.Tables(0).Rows.Count > 0 Then
                        myDS.Tables(0).PrimaryKey = New DataColumn() {myDS.Tables(0).Columns("CodeName"), myDS.Tables(0).Columns("Code")}
                    End If
                    Session("DS") = myDS
                    grdLookup.DataSource = myDS
                    grdLookup.DataBind()
                    AdjustTextBoxWidth
                End If
            End If
        End If
        myDS = Nothing
    End Sub

    Protected Sub grdLookup_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdLookup.PageIndexChanging

        grdLookup.PageIndex = e.NewPageIndex
        SearchData()
        'BindData()

    End Sub

    Protected Sub grdLookup_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdLookup.RowDataBound

        If e.Row.RowType = ListItemType.Item Or e.Row.RowType = ListItemType.AlternatingItem Then

            If e.Row.RowType = ListItemType.AlternatingItem Then
                strOriginalColour = "'#F2F4FF'"
            End If

            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='pink'; this.style.cursor='hand'")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=" & strOriginalColour)

            e.Row.Attributes.Add("onClick", "return mainValues();")
            Dim button As LinkButton = CType(e.Row.Cells(0).Controls(0), LinkButton)
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(button, "")
        End If

    End Sub

    Protected Sub grdLookup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdLookup.SelectedIndexChanged
        Dim CodeName As String, Code As String, Name As String
        Code = Replace(Server.HtmlDecode(grdLookup.SelectedRow.Cells(3).Text), "&amp;", "&")
        Name = Replace(Server.HtmlDecode(grdLookup.SelectedRow.Cells(4).Text), "&amp;", "&")
        CodeName = Replace(Server.HtmlDecode(grdLookup.SelectedRow.Cells(2).Text), "&amp;", "&")
        
        Dim strFormName As String = "", strControlName As String = ""
        Dim posSplit As Integer = Request.QueryString("formname").ToString.IndexOf(".")
        Dim strjscript As String = "<script language=""javascript"">"

        strFormName = Left(Request.QueryString("formname").ToString, posSplit)
        strControlName = Right(Request.QueryString("formname").ToString, Len(Request.QueryString("formname").ToString) - (posSplit + 4))
        GetOptionDataType(strFormName)
        Select Case Request.QueryString("selectedcolumn")
            Case "Code"
                ssql = "Exec sp_sa_GetAutoFillData N'" & Replace(Session("Company").ToString, "'", "''") & "',N'" & _
                                Replace(Session("Module").ToString, "'", "''") & "',N'" & Replace(Code, "'", "''") & "',N'" & Replace(strFormName, "'", "''") & "',N'" & Replace(strControlName, "'", "''") & "',N'AUTOFILL',N'" & _
                                Request.QueryString("filter").ToString & "',N'" & Request.QueryString("filter2").ToString & "',N'" & Request.QueryString("filter3").ToString & "'"
            Case "CodeName"
                ssql = "Exec sp_sa_GetAutoFillData N'" & Replace(Session("Company").ToString, "'", "''") & "',N'" & _
                                Replace(Session("Module").ToString, "'", "''") & "',N'" & Replace(CodeName, "'", "''") & "',N'" & Replace(strFormName, "'", "''") & "',N'" & Replace(strControlName, "'", "''") & "',N'AUTOFILL',N'" & _
                                Request.QueryString("filter").ToString & "',N'" & Request.QueryString("filter2").ToString & "',N'" & Request.QueryString("filter3").ToString & "'"
            Case "Name"
                ssql = "Exec sp_sa_GetAutoFillData N'" & Replace(Session("Company").ToString, "'", "''") & "',N'" & _
                                Replace(Session("Module").ToString, "'", "''") & "',N'" & Replace(Name, "'", "''") & "',N'" & Replace(strFormName, "'", "''") & "',N'" & Replace(strControlName, "'", "''") & "',N'AUTOFILL',N'" & _
                                Request.QueryString("filter").ToString & "',N'" & Request.QueryString("filter2").ToString & "',N'" & Request.QueryString("filter3").ToString & "'"
        End Select
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

        If Not myDS Is Nothing Then
            If myDS.Tables.Count > 0 Then
                If myDS.Tables(0).Rows.Count > 0 Then
                    strjscript = strjscript & "window.opener.document." & Request.QueryString("formname") & ".value = " & """" & CodeName & """" & ";"
                    For Me.i = 0 To myDS.Tables(0).Columns.Count - 1
                        strjscript = strjscript & "window.opener.document." & strFormName & _
                                ReturnControlName(myDS.Tables(0).Columns(i).ColumnName.ToString.ToUpper) & _
                                ReturnValue(myDS.Tables(0).Columns(i).ColumnName.ToString.ToUpper) & _
                                myDS.Tables(0).Rows(0).Item(i).ToString & """;"
                    Next
                    strjscript = strjscript & "self.close();" & "window.opener.document." & strFormName & ".submit();"
                    strjscript = strjscript & "<" & "/script>"
                    strjscript = Replace(Server.HtmlDecode(strjscript), "&amp;", "&")
                    strjscript = Replace(Server.HtmlDecode(strjscript), "&nbsp;", "&")
                Else
                    Select Case Request.QueryString("selectedcolumn")
                        Case "CodeName"
                            strjscript = strjscript & "window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = " & """" & CodeName & """" & ";"
                        Case "Code"
                            strjscript = strjscript & "window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = " & """" & Code & """" & ";"
                        Case "Name"
                            strjscript = strjscript & "window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = " & """" & Name & """" & ";"
                    End Select
                    strjscript = strjscript & "self.close();" & "window.opener.document." & strFormName & ".submit();"
                    strjscript = strjscript & "<" & "/script>"
                    strjscript = Replace(Server.HtmlDecode(strjscript), "&amp;", "&")
                    strjscript = Replace(Server.HtmlDecode(strjscript), "&nbsp;", "&")
                End If
            Else
                Select Case Request.QueryString("selectedcolumn")
                    Case "CodeName"
                        strjscript = strjscript & "window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = " & """" & CodeName & """" & ";"
                    Case "Code"
                        strjscript = strjscript & "window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = " & """" & Code & """" & ";"
                    Case "Name"
                        strjscript = strjscript & "window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = " & """" & Name & """" & ";"
                End Select
                strjscript = strjscript & "self.close();" & "window.opener.document." & strFormName & ".submit();"
                strjscript = strjscript & "<" & "/script>"
                strjscript = Replace(Server.HtmlDecode(strjscript), "&amp;", "&")
                strjscript = Replace(Server.HtmlDecode(strjscript), "&nbsp;", "&")
            End If
        Else
            Select Case Request.QueryString("selectedcolumn")
                Case "CodeName"
                    strjscript = strjscript & "window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = " & """" & CodeName & """" & ";"
                Case "Code"
                    strjscript = strjscript & "window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = " & """" & Code & """" & ";"
                Case "Name"
                    strjscript = strjscript & "window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = " & """" & Name & """" & ";"
            End Select
            strjscript = strjscript & "self.close();" & "window.opener.document." & strFormName & ".submit();"
            strjscript = strjscript & "<" & "/script>"
            strjscript = Replace(Server.HtmlDecode(strjscript), "&amp;", "&")
            strjscript = Replace(Server.HtmlDecode(strjscript), "&nbsp;", "&")
        End If
        ltrValue.Text = strjscript

    End Sub
    Private Sub GetOptionDataType(ByVal TableProfileCode As String)
        tmpDS = New DataSet
        tmpDS = mySQL.ExecuteSQL("Select Code,Name,Option_Data_Type From Table_Field Where Table_Profile_Code='" & TableProfileCode & "' Order By Sequence_No", Session("Company").ToString, Session("EmpID").ToString)
        htb = New Hashtable
        htbValue = New Hashtable
        For Me.j = 0 To tmpDS.Tables(0).Rows.Count - 1
            If tmpDS.Tables(0).Rows(j).Item(1).ToString = "OPTION" Then
                htb.Add(tmpDS.Tables(0).Rows(j).Item(0).ToString.ToUpper, ".ddl" & tmpDS.Tables(0).Rows(j).Item(0).ToString)
                htbValue.Add(tmpDS.Tables(0).Rows(j).Item(0).ToString.ToUpper, ".SelectedValue = """)
            Else
                htb.Add(tmpDS.Tables(0).Rows(j).Item(0).ToString.ToUpper, ".txt" & tmpDS.Tables(0).Rows(j).Item(0).ToString)
                htbValue.Add(tmpDS.Tables(0).Rows(j).Item(0).ToString.ToUpper, ".value = """)
            End If
        Next
        'htb = Nothing
        'htbValue = Nothing
        tmpDS = Nothing
    End Sub
    Private Function ReturnControlName(ByVal TableFieldCode As String) As String
        Dim strOutput As String = ""
        de = New DictionaryEntry
        For Each de In htb
            If de.Key = TableFieldCode Then
                strOutput = de.Value
                Exit For
            End If
        Next
        de = Nothing
        Return strOutput
    End Function
    Private Function ReturnValue(ByVal TableFieldCode As String) As String
        Dim strOutput As String = ""
        de = New DictionaryEntry
        For Each de In htbValue
            If de.Key = TableFieldCode Then
                strOutput = de.Value
                Exit For
            End If
        Next
        de = Nothing
        Return strOutput
    End Function

    'Protected Sub imgbtnCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnCancel.Click
    '    txtSearchCode.Text = ""
    '    txtSearchName.Text = ""
    '    txtSearchCode.Focus()
    'End Sub

    'Protected Sub imgbtnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSearch.Click
    '    If ValidateSearch() = True Then
    '        SearchData()
    '    End If
    'End Sub
    Private Sub SearchData()
        Dim booSearch As Boolean = True
        Dim ds As New DataSet, strSelect As String = ""
        ds = CType(Session("DS"), DataSet)
        Dim dr As DataRow()
        '">=", "<=", ">", "<", "|", "%", "~", ""
        If txtSearchCode.Text.ToString.Trim <> "" Then
            strSelect = "Code Like '" & Server.HtmlDecode(txtSearchCode.Text.ToString.Replace("'", "''").Replace("&amp;", "&")) & "%' AND "
        End If
        If txtSearchName.Text.ToString.Trim <> "" Then
            strSelect &= "Name Like '" & Server.HtmlDecode(txtSearchName.Text.ToString.Replace("'", "''").Replace("&amp;", "&")) & "%' AND "
        End If
        If strSelect = "" Then
            booSearch = False
        End If
        If Right(strSelect, 5) = " AND " Then
            strSelect = Left(strSelect, Len(strSelect) - 5)
        End If
        If booSearch = True Then
            dr = ds.Tables(0).Select(strSelect)
            Dim dtresult As New DataTable
            dtresult.Columns.Add("CodeName")
            dtresult.Columns.Add("Code")
            dtresult.Columns.Add("Name")

            For i As Integer = 0 To dr.Length - 1
                Dim newDR As DataRow = dtresult.NewRow
                newDR(0) = dr(i).Item(0).ToString
                newDR(1) = dr(i).Item(1).ToString
                newDR(2) = dr(i).Item(2).ToString
                dtresult.Rows.Add(newDR)
            Next
            dtresult.AcceptChanges()
            If Not dtresult Is Nothing Then
                grdLookup.DataSource = dtresult
                grdLookup.DataBind()
                AdjustTextBoxWidth()
            End If
        Else
            grdLookup.DataSource = Session("DS")
            grdLookup.DataBind()
            AdjustTextBoxWidth()
        End If
    End Sub
    Private Function ValidateSearch() As Boolean
        If txtSearchCode.Text.ToString.Trim = "" And txtSearchName.Text.ToString.Trim = "" Then
            'lblMessage.Text = "Please enter a value to search, either in [Search For Code] or [Search For Name]!"
            'txtSearchCode.Focus()
            'Return False
            Return True
        End If
        If mySetting.ValidateQuerySyntax(txtSearchCode.Text.ToString.Trim) = False Then
            lblMessage.Text = "Combination of more than one queries syntax was found! Please enter only one syntax to search for [Code]!"
            txtSearchCode.Focus()
            Return False
        End If
        If mySetting.ValidateQuerySyntax(txtSearchName.Text.ToString.Trim) = False Then
            lblMessage.Text = "Combination of more than one queries syntax was found! Please enter only one syntax to search for [Name]!"
            txtSearchName.Focus()
            Return False
        End If
        Return True
    End Function

    'Protected Sub grdLookup_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdLookup.Sorting
    '    Dim sortExpression As String = e.SortExpression
    '    If GridViewSortDirection = SortDirection.Ascending Then
    '        GridViewSortDirection = SortDirection.Descending
    '    Else
    '        GridViewSortDirection = SortDirection.Ascending
    '    End If
    'End Sub
    'Public Sub SortGridView(ByVal sortExpression As String, ByVal direction As String)

    'End Sub
    Private Sub AdjustTextBoxWidth()
        txtSearchCode.Width = "55"
        txtSearchName.Width = "220"
    End Sub

    Protected Sub imgbtnFilter_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnFilter.Click
        If ValidateSearch() = True Then
            SearchData()
        End If
    End Sub
End Class

