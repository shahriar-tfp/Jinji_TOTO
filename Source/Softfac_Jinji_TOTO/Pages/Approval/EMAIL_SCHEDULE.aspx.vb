Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class PAGES_APPROVAL_EMAIL_SCHEDULE
    Inherits System.Web.UI.Page
#Region "Public Declaration"
    Private WithEvents mySQL As New clsSQL, myDS As New DataSet, myDS2 As New DataSet, mySetting As New clsGlobalSetting
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Public ssql As String, i As Integer, j As Integer, SearchByPage As Boolean, RecFound As Boolean, CountRecord As Integer
    Public AllowView As Boolean, AllowInsert As Boolean, AllowUpdate As Boolean, AllowDelete As Boolean, AllowPrint As Boolean
    Public _currentPageNumber As Integer, _totalPage As Double = 1, _totalRecords As Integer
    Public myDE As New DictionaryEntry, myHT As Hashtable
    Public myDataType As New clsGlobalSetting.DataType, SearchByFilter As Boolean = False
    Dim intColumn As Integer
#End Region
#Region "Private Declaration"
    Private WithEvents myDT1 As New DataTable, myDT2 As New DataTable
    Dim myDR1 As DataRow, myDR2 As DataRow, rcPerPage As Integer, autonum As Integer
    Dim ssql2 As String, ssql3 As String, ssql4 As String
    Dim rowPosition As String = "R_", rowPositionM As String = "RM_", panelPosition As String = "Display_"
    Dim buttonPosition1 As String = "B1_", buttonPosition2 As String = "B2_", buttonPosition3 As String = "B3_", labelPosition1 As String = "L1_"
    Dim btnColourDef, btnColourAlt As String
    Dim strPath As String = "../../Images"
    Dim logic As Boolean
#End Region
#Region "Page Setting"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.IsNewSession Then
            Response.Redirect("../Global/SessionTimeOut.aspx")
        End If
        If Not IsPostBack Then
            Response.CacheControl = "no-cache"
            Response.AddHeader("Pragma", "no-cache")
            Response.Expires = -1
			Session("Module")="APPROVAL"

            If Session("ScreenWidth") = 0 Then
                Session("ScreenWidth") = "1024"
                Session("GVwidth") = Session("ScreenWidth") - 360
            End If
            If Session("ScreenHeight") = 0 Then
                Session("ScreenHeight") = "768"
                Session("GVheight") = (Session("ScreenHeight") / 2) - 50
            End If
            pnlGridview.Width = CInt(Session("GVwidth"))
            pnlGridview.Height = CInt(Session("GVheight"))
            
            PagePreload()
            BindGrid()
        Else
            lblresult.Visible = False
            lblresult2.Visible = False
            If IsNumeric(CurrentPage.Text) Then
                _currentPageNumber = CInt(CurrentPage.Text)
            Else
                _currentPageNumber = 1
            End If
        End If
    End Sub
    
    Sub PagePreload()
		btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")
		Session("SearchByFilter") = "False"
        Session("SearchSQL") = ""
        Session("SearchSQL1") = ""
        Session("FilterField") = ""
        Session("FilterCriteria") = ""
        mySetting.GetBtnImgUrl(imgBtnSubmit, Session("Company").ToString, btnColourDef, "btnSubmit.png")
        mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourDef, "btnSearch.png")
        mySetting.GetBtnImgUrl(imgBtnClear, Session("Company").ToString, btnColourDef, "btnClear.png")
        mySetting.GetBtnImgUrl(imgBtnCancel, Session("Company").ToString, btnColourDef, "btnCancel.png")
        mySetting.GetBtnImgUrl(imgBtnAdd, Session("Company").ToString, btnColourDef, "btnAdd.png")
        mySetting.GetBtnImgUrl(imgBtnFilter, Session("Company").ToString, btnColourDef, "btnFilter.png")
        mySetting.GetBtnImgUrl(imgBtnDelete, Session("Company").ToString, btnColourDef, "btnDelete.png")
        mySetting.GetBtnImgUrl(imgBtnUpdate, Session("Company").ToString, btnColourDef, "btnUpdate.png")
        mySetting.GetBtnImgUrl(imgBtnEdit, Session("Company").ToString, btnColourDef, "btnEdit.png")
        mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourDef, "btnPrint.png")
        mySetting.GetBtnImgUrl(imgBtnGoToPage, Session("Company").ToString, btnColourDef, "btngo.png")
        mySetting.SetTextBoxPressEnterGoToImageButton(txtGoToPage, imgBtnGoToPage)
		body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

        'get Page Title
        ssql = "exec sp_sa_GetPageTitle '" & Form.ID & "'"
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            lblTitle.Text = myDS.Tables(0).Rows(0).Item(0)
            lblTitle.CssClass = "wordstyle4"
            lblTitle2.Text = myDS.Tables(0).Rows(0).Item(0)
            lblTitle2.CssClass = "wordstyle4"
        End If
        myDS = Nothing

        pnlEdit.Visible = False
        SearchByPage = False
        _currentPageNumber = 1

        myDS = New DataSet
        myDS = mySetting.GetPageFieldSetting(Session("Company"), Form.ID, Session("EmpID"))
        If myDS.Tables.Count > 1 Then
            myDT1 = New DataTable
            myDT1 = myDS.Tables(0)
            myDT2 = New DataTable
            myDT2 = myDS.Tables(1)

            If myDT1.Rows.Count > 0 Then
                myDR1 = myDT1.Rows(0)
                Page.Title = myDR1(1)
                If myDR1(3) = "YES" Then
                    AllowView = True
                    Session("View") = "TRUE"
                Else
                    AllowView = False
                    Session("View") = "FALSE"
                    pnlaction.Visible = False
                    pnlEdit.Visible = False
                    pnlGridview.Visible = False
                    pnlMain.Visible = False
                    pnlPrevNext.Visible = False
                    pnlresult.Visible = False
                    ShowMessage("You are not allow to view this page!")
                    Exit Sub
                End If

                If myDR1(4) = "YES" Then
                    AllowInsert = True
                    Session("Insert") = "TRUE"
                Else
                    AllowInsert = False
                    Session("Insert") = "FALSE"
                End If
                If myDR1(5) = "YES" Then
                    AllowUpdate = True
                    Session("Update") = "TRUE"
                Else
                    AllowUpdate = False
                    Session("Update") = "FALSE"
                End If
                If myDR1(6) = "YES" Then
                    AllowDelete = True
                    Session("Delete") = "TRUE"
                Else
                    AllowDelete = False
                    Session("Delete") = "FALSE"
                End If
                If myDR1(7) = "YES" Then
                    AllowPrint = True
                    Session("Print") = "TRUE"
                Else
                    AllowPrint = False
                    Session("Print") = "FALSE"
                End If

                imgBtnAdd.Visible = AllowInsert
                imgBtnEdit.Visible = AllowUpdate
                imgBtnDelete.Visible = AllowDelete
                imgBtnSubmit.Visible = AllowUpdate
                imgBtnUpdate.Visible = AllowUpdate
                imgBtnSearch.Visible = AllowView
                imgBtnFilter.Visible = AllowView
                imgBtnClear.Visible = AllowView
                imgBtnCancel.Visible = AllowView
                imgBtnPrint.Visible = AllowPrint
            End If

            If myDT2.Rows.Count > 0 Then
                For i = 0 To myDT2.Rows.Count - 1
                    Dim myLabel As Label = Page.FindControl("lbl" & myDT2.Rows(i).Item(2).ToString)
                    Dim myImageButton As ImageButton = Page.FindControl("imgbtn" & myDT2.Rows(i).Item(2).ToString)
                    Dim myImageKey As ImageButton = Page.FindControl("imgkey" & myDT2.Rows(i).Item(2).ToString)

                    myLabel.Text = myDT2.Rows(i).Item(3).ToString
                    mySetting.GetImgUrl(myImageKey, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
                    Select Case myDT2.Rows(i).Item(6).ToString
                        Case "DATE"
                            mySetting.GetImgBtnUrl(myImageButton, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
                        Case "DATETIME"
                            mySetting.GetImgBtnUrl(myImageButton, clsGlobalSetting.ImageType._DATETIME, Session("Company").ToString)
                        Case "TIME"
                            mySetting.GetImgBtnUrl(myImageButton, clsGlobalSetting.ImageType._TIME, Session("Company").ToString)
                        Case Else
                            mySetting.GetImgBtnUrl(myImageButton, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
                    End Select
                    If myDT2.Rows(i).Item(9).ToString = "NO" Then
                        myLabel.Visible = False
                        myImageKey.Visible = False
                        myImageButton.Visible = False
                        Select Case myDT2.Rows(i).Item(6).ToString
                            Case "OPTION"
                                Dim myDDL As DropDownList = Page.FindControl("ddl" & myDT2.Rows(i).Item(2).ToString)
                                mySetting.GetDropdownlistValue(Form.ID, myDT2.Rows(i).Item(2).ToString, myDDL)
                                myDDL.Visible = False
                                myDDL = Nothing
                            Case Else
                                Dim myTextBox As TextBox = Page.FindControl("txt" & myDT2.Rows(i).Item(2).ToString)
                                myTextBox.Visible = False
                                myTextBox = Nothing
                        End Select
					End If
					'Else
                    Select Case myDT2.Rows(i).Item(6).ToString
                        Case "OPTION"
                            Dim myDDL As DropDownList = Page.FindControl("ddl" & myDT2.Rows(i).Item(2).ToString)
                            mySetting.GetDropdownlistValue(Form.ID, myDT2.Rows(i).Item(2).ToString, myDDL)
                            myDDL = Nothing
                        Case "LOOKUP"
                            mySetting.GetLookupValue_ImageButton(myImageButton, Form.ID, "txt" & myDT2.Rows(i).Item(2).ToString, "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & myDT2.Rows(i).Item(2).ToString & """," & """" & """," & """" & Session("EmpID").ToString & """")
                            Dim myTextBox As TextBox = Page.FindControl("txt" & myDT2.Rows(i).Item(2).ToString)
                            myTextBox.MaxLength = CInt(myDT2.Rows(i).Item(7).ToString)
                            myTextBox = Nothing
                        Case "DATE"
                            mySetting.PopUpCalendar_ImageButton(myImageButton, Form.ID, "txt" & myDT2.Rows(i).Item(2).ToString)
                            Dim myTextBox As TextBox = Page.FindControl("txt" & myDT2.Rows(i).Item(2).ToString)
                            myTextBox.MaxLength = CInt(myDT2.Rows(i).Item(7).ToString)
                            myTextBox = Nothing
                        Case "DATETIME"
                            mySetting.DateTimePicker_ImageButton(myImageButton, Form.ID, "txt" & myDT2.Rows(i).Item(2).ToString)
                            Dim myTextBox As TextBox = Page.FindControl("txt" & myDT2.Rows(i).Item(2).ToString)
                            myTextBox.MaxLength = CInt(myDT2.Rows(i).Item(7).ToString)
                            myTextBox = Nothing
                        Case "TIME"
                            mySetting.PopUpTime_ImageButton(myImageButton, Form.ID, "txt" & myDT2.Rows(i).Item(2).ToString)
                            Dim myTextBox As TextBox = Page.FindControl("txt" & myDT2.Rows(i).Item(2).ToString)
                            myTextBox.MaxLength = CInt(myDT2.Rows(i).Item(7).ToString)
                            myTextBox = Nothing
                        Case "DECIMAL", "INTEGER"
                            Dim myTextBox As TextBox = Page.FindControl("txt" & myDT2.Rows(i).Item(2).ToString)
                            myTextBox.MaxLength = CInt(myDT2.Rows(i).Item(7).ToString)
                            myTextBox = Nothing
                        Case Else
                            Dim myTextBox As TextBox = Page.FindControl("txt" & myDT2.Rows(i).Item(2).ToString)
                            myTextBox.MaxLength = CInt(myDT2.Rows(i).Item(7).ToString)
                            If myDT2.Rows(i).Item(11).ToString.Trim = "YES" Then
                                If myDT2.Rows(i).Item(12).ToString = "YES" Then
                                    myTextBox.TextMode = TextBoxMode.Password
                                End If
                                If myDT2.Rows(i).Item(4).ToString = "YES" Then
                                    mySetting.ConvertUppercase(myTextBox)
                                End If
                            End If
                            myTextBox = Nothing
                    End Select
                    myLabel = Nothing
                    myImageKey = Nothing
                    myImageButton = Nothing
                Next
            End If
            myDS = Nothing
            myDT1 = Nothing
            myDT2 = Nothing
            mySetting.GetGridViewSetting(Session("Company").ToString, Session("Module").ToString, "No_Of_Record", Form.ID, "GetGridViewRecord", myGridView)
            mySetting.GetGridViewSetting(Session("Company").ToString, Session("Module").ToString, "No_Of_Record", Form.ID, "GetGridViewRecord", gvHistory)
        End If
    End Sub
    
    Sub BindGrid()
        Try
            pnldescription.Visible = False

            If CType(Session("SearchByFilter"), Boolean) = True Then
                ssql = Session("SearchSQL").ToString.Trim & _currentPageNumber.ToString  & "'"
                ssql2 = Session("SearchSQL1").ToString.Trim & _currentPageNumber.ToString.Trim & "'"
                '
            Else
                If SearchByPage = True Then
                    ssql = "Exec sp_sa_GetTableRecordsWithSecurity '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','" & Form.ID & "','" & myGridView.PageSize & "','" & txtGoToPage.Text & "'"
                    ssql2 = "Exec sp_sa_CompareTableRecords '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','" & Form.ID & "','" & gvHistory.PageSize & "','" & txtGoToPage.Text & "'"
                Else
                    ssql = "Exec sp_sa_GetTableRecordsWithSecurity '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','" & Form.ID & "','" & myGridView.PageSize & "','" & _currentPageNumber & "'"
                    ssql2 = "Exec sp_sa_CompareTableRecords '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','" & Form.ID & "','" & gvHistory.PageSize & "','" & _currentPageNumber & "'"
                End If
            End If

            myDS = mySQL.ExecuteSQL(ssql)
            If Not myDS Is Nothing Then
                If myDS.Tables.Count > 1 And CInt(myDS.Tables(0).Rows(0).Item(0)) > 0 Then
                    myGridView.DataSource = myDS.Tables(1)
                    myGridView.DataBind()

                    myDS2 = New DataSet
                    myDS2 = mySQL.ExecuteSQL(ssql2)
                    gvHistory.DataSource = myDS2.Tables(1)
                    gvHistory.DataBind()
                    myDS2 = Nothing

                    pnlEdit.Visible = False
                    pnlMain.Visible = True
                    pnlPrevNext.Enabled = True

                    If CInt(myDS.Tables(0).Rows(0).Item(0)) > 0 And CInt(myDS.Tables(0).Rows(0).Item(0)) < 2 Then
                        lblTotal.Text = "(" & myDS.Tables(0).Rows(0).Item(0) & " record.)"
                    ElseIf CInt(myDS.Tables(0).Rows(0).Item(0)) > 1 Then
                        lblTotal.Text = "(" & myDS.Tables(0).Rows(0).Item(0) & " records.)"
                    End If
                    If SearchByPage = True Then
                        CurrentPage.Text = txtGoToPage.Text & " "
                    Else
                        CurrentPage.Text = _currentPageNumber.ToString() & " "
                    End If
                    If Not IsPostBack Then
                        _totalRecords = myDS.Tables(0).Rows(0).Item(0)
                        _totalPage = _totalRecords / myGridView.PageSize
                        TotalPages.Text = (System.Math.Ceiling(_totalPage)).ToString()
                        Session("TotalPages") = TotalPages.Text
                    Else
                        _totalRecords = myDS.Tables(0).Rows(0).Item(0)
                        _totalPage = _totalRecords / myGridView.PageSize
                        TotalPages.Text = (System.Math.Ceiling(_totalPage)).ToString()
                        _totalPage = Double.Parse(TotalPages.Text)
                    End If

                    If SearchByPage = True Then
                        If CInt(txtGoToPage.Text) = 1 And CInt(Session("TotalPages")) = 1 Then
                            lnkbtnFirstPage.Enabled = False
                            lnkbtnPrevPage.Enabled = False
                            lnkbtnNextPage.Enabled = False
                            lnkbtnLastPage.Enabled = False
                        ElseIf CInt(txtGoToPage.Text) = 1 And CInt(txtGoToPage.Text) <> CInt(Session("TotalPages")) Then
                            lnkbtnFirstPage.Enabled = False
                            lnkbtnPrevPage.Enabled = False
                            lnkbtnNextPage.Enabled = True
                            lnkbtnLastPage.Enabled = True
                        ElseIf CInt(txtGoToPage.Text) > 1 And CInt(txtGoToPage.Text) < CInt(Session("TotalPages")) Then
                            lnkbtnFirstPage.Enabled = True
                            lnkbtnPrevPage.Enabled = True
                            lnkbtnNextPage.Enabled = True
                            lnkbtnLastPage.Enabled = True
                        ElseIf CInt(txtGoToPage.Text) > 1 And CInt(txtGoToPage.Text) = CInt(Session("TotalPages")) Then
                            lnkbtnFirstPage.Enabled = True
                            lnkbtnPrevPage.Enabled = True
                            lnkbtnNextPage.Enabled = False
                            lnkbtnLastPage.Enabled = False
                        End If
                    Else    'When user key in txtGoToPage and press enter or click at imgGoToPage
                        If CInt(_currentPageNumber) = 1 And CInt((System.Math.Ceiling(_totalPage)).ToString()) = 1 Then
                            lnkbtnFirstPage.Enabled = False
                            lnkbtnPrevPage.Enabled = False
                            lnkbtnNextPage.Enabled = False
                            lnkbtnLastPage.Enabled = False
                        ElseIf CInt(_currentPageNumber) = 1 And CInt(_currentPageNumber) <> CInt((System.Math.Ceiling(_totalPage)).ToString()) Then
                            lnkbtnFirstPage.Enabled = False
                            lnkbtnPrevPage.Enabled = False
                            lnkbtnNextPage.Enabled = True
                            lnkbtnLastPage.Enabled = True
                        ElseIf CInt(_currentPageNumber) > 1 And CInt(_currentPageNumber) < CInt((System.Math.Ceiling(_totalPage)).ToString()) Then
                            lnkbtnFirstPage.Enabled = True
                            lnkbtnPrevPage.Enabled = True
                            lnkbtnNextPage.Enabled = True
                            lnkbtnLastPage.Enabled = True
                        ElseIf CInt(_currentPageNumber) > 1 And CInt(_currentPageNumber) = CInt((System.Math.Ceiling(_totalPage)).ToString()) Then
                            lnkbtnFirstPage.Enabled = True
                            lnkbtnPrevPage.Enabled = True
                            lnkbtnNextPage.Enabled = False
                            lnkbtnLastPage.Enabled = False
                        End If
                    End If
                Else
                    If Not myDS Is Nothing Then
                        If myDS.Tables.Count >= 2 Then
                            myGridView.DataSource = myDS.Tables(1)
                            myGridView.DataBind()
                        End If
                    End If

                    lblTotal.Text = "(0 record)"
                    CurrentPage.Text = ""
                    pnlPrevNext.Enabled = False
                    lblresult2.Visible = True
                    lblresult2.Text = "No record found!"
                End If
            Else
                lblresult2.Visible = True
                lblresult2.Text = mySQL.GetErrorMessage
            End If
        Catch ex As Exception
            'Error Handling
            Response.Write("Error : " & ex.ToString)
        End Try
    End Sub
    
#End Region

#Region "System Generated Code"
#Region "Panel Edit"
    Protected Sub imgBtnSubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSubmit.Click
        lblresult2.Text = ""
        If ValidateInsert() = True Then
            mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            pnlEdit.Visible = False
            pnlMain.Visible = True
            BindGrid()
        End If
    End Sub
    Protected Sub imgBtnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSearch.Click
        If ValidateSearch() = True Then
            If ssql <> "" Then
                SearchByFilter = True
            Else
                SearchByFilter = False
            End If
            _currentPageNumber = 1
            txtGoToPage.Text = ""
            BindGrid()
        End If
    End Sub
    Protected Sub imgBtnUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnUpdate.Click
        If ValidateUpdate() Then
            mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            pnlEdit.Visible = False
            pnlMain.Visible = True
            BindGrid()
        End If
    End Sub
    Protected Sub imgBtnClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnClear.Click

    End Sub
    Protected Sub imgBtnCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnCancel.Click
        pnlEdit.Visible = False
        pnlMain.Visible = True
        lblGoToPage.Text = ""
        pnldescription.Visible = False
        'EnableAll()
    End Sub
#End Region

#Region "Panel GridView"
    Protected Sub myGridView_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles myGridView.RowCancelingEdit
        myGridView.EditIndex = -1
        BindGrid()
    End Sub
    Protected Sub myGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles myGridView.RowDataBound
        e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor; this.style.backgroundColor='silver';")
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
    End Sub
    Protected Sub myGridView_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles myGridView.RowDeleted

    End Sub
    Protected Sub myGridView_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles myGridView.RowDeleting
        Try
            ssql = mySetting.GetSQLParameter(Form.ID, clsGlobalSetting.SQLAction.DELETE_Statement, myGridView, e.RowIndex, Session("Company").ToString, Session("Module").ToString)
            If ssql <> Nothing Then
                mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                ssql = Nothing
                BindGrid()
            End If
        Catch ex As Exception
            lblresult.Text = ex.ToString
            e.Cancel = True
        End Try
    End Sub
    Protected Sub myGridView_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles myGridView.RowEditing
        myGridView.EditIndex = e.NewEditIndex
        BindGrid()
        mySetting.SetGridViewUpdatingCondition(myGridView, e.NewEditIndex, Form.ID)
    End Sub
    Protected Sub myGridView_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles myGridView.RowUpdated

    End Sub
    Protected Sub myGridView_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles myGridView.RowUpdating
        Try
            'ssql = mySetting.GetSQLParameter("Employee_Transfer", clsGlobalSetting.SQLAction.UPDATE_Statement, gvEmployeeTransfer, e.RowIndex, Session("Company").ToString, Session("Module").ToString)
            If myGridViewValidateUpdate(e.RowIndex) Then
                mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                ssql = Nothing
                lblresult2.Visible = False
                myGridView.EditIndex = -1
                BindGrid()
            End If
        Catch ex As Exception
            lblresult2.Text = ex.ToString
            e.Cancel = True
        End Try
    End Sub
    Private Function myGridViewValidateUpdate(ByVal RowIndex As Integer) As Boolean
        lblresult2.Visible = True
        myDS = mySQL.ExecuteSQL("Select Code,Name,Option_Data_Type,Option_Mandatory From Table_Field Where Table_Profile_Code='" & Form.ID & "' And Option_Primary_Key='NO' And Option_View='YES' Order By Table_Profile_Code,Sequence_No")
        ssql = "Update " & Form.ID & " Set "
        If myDS.Tables(0).Rows.Count > 0 Then
            For i = 0 To myDS.Tables(0).Rows.Count - 1
                For j = 2 To myGridView.HeaderRow.Cells.Count - 1
                    If myDS.Tables(0).Rows(i).Item(1).ToString = myGridView.HeaderRow.Cells(j).Text.ToString Then
                        Dim myTextBox As TextBox = myGridView.Rows(RowIndex).Cells(j).Controls(0)
                        If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" And mySetting.CheckTextNull(myTextBox.Text) = False Then
                            lblresult2.Text = myDS.Tables(0).Rows(i).Item(1).ToString & " Is A Required Field!"
                            myTextBox.Focus()
                            Return False
                        Else
                            Select Case myDS.Tables(0).Rows(i).Item(2).ToString
                                Case "DATE", "DATETIME"
                                    If IsNumeric(mySetting.ConvertDateToDecimal(myTextBox.Text, Session("Company").ToString, Session("Module").ToString)) Then
                                        Dim myDST As New DataSet
                                        myDST = mySQL.ExecuteSQL("Select Function_Name,Default_Value,Default_Value2 From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='UPDATE'")
                                        If myDST.Tables(0).Rows.Count > 0 Then
                                            If myDST.Tables(0).Rows(0).Item(1).ToString.Trim = "" Then
                                                ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=N'" & mySetting.ConvertDateToDecimal(myTextBox.Text.ToString, Session("Company").ToString, Session("Module").ToString) & "',"
                                            Else
                                                ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=N'" & mySetting.ConvertDateToDecimal(myTextBox.Text.ToString, Session("Company").ToString, Session("Module").ToString) & "',"
                                            End If
                                        Else
                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=N'" & mySetting.ConvertDateToDecimal(myTextBox.Text.ToString, Session("Company").ToString, Session("Module").ToString) & "',"
                                        End If
                                    Else
                                        lblresult2.Text = "Invalid Input Format For " & myDS.Tables(0).Rows(i).Item(1).ToString
                                        Return False
                                    End If
                                Case "OPTION"
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0).ToString & "=DBO.FN_GETOPTIONCODE(N'" & Form.ID & "',N'" & myDS.Tables(0).Rows(i).Item(0).ToString & "',N'" & myTextBox.Text.ToString & "'),"
                                Case "DECIMAL", "INTEGER"
                                    If IsNumeric(myTextBox.Text) Then
                                        ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "='" & myTextBox.Text.ToString & "',"
                                    Else
                                        lblresult2.Text = "Invalid Input Format For " & myDS.Tables(0).Rows(i).Item(1).ToString
                                        Return False
                                    End If
                                Case Else
                                    Dim myDST As New DataSet
                                    myDST = mySQL.ExecuteSQL("Select Function_Name,Default_Value,Default_Value2 From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='UPDATE'")
                                    If myDST.Tables(0).Rows.Count > 0 Then
                                        If myDST.Tables(0).Rows(0).Item(1).ToString.Trim = "" Then
                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(1).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "'),''),"
                                        Else
                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(1).Rows(0).Item(0).ToString & "(" & myDST.Tables(1).Rows(0).Item(1).ToString & "),''),"
                                        End If
                                    Else
                                        ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(N'" & myTextBox.Text.ToString & "',''),"
                                    End If
                            End Select
                            Exit For
                        End If
                    End If
                Next
            Next
            ssql = Left(ssql, Len(ssql) - 1) & " WHERE "
            myDS = Nothing

            myDS2 = mySQL.ExecuteSQL("Select Code,Name,Option_Data_Type,Option_Mandatory From Table_Field Where Table_Profile_Code='" & Form.ID & "' And Option_Primary_Key='YES' And Option_View='YES' Order By Table_Profile_Code,Sequence_No")
            If myDS2.Tables(0).Rows.Count > 0 Then
                For i = 0 To myDS2.Tables(0).Rows.Count - 1
                    For j = 2 To myGridView.HeaderRow.Cells.Count - 1
                        If myDS2.Tables(0).Rows(i).Item(1).ToString = myGridView.HeaderRow.Cells(j).Text.ToString Then
                            Dim myTextBox As TextBox = myGridView.Rows(RowIndex).Cells(j).Controls(0)
                            If myDS2.Tables(0).Rows(i).Item(3).ToString = "YES" And mySetting.CheckTextNull(myTextBox.Text) = False Then
                                lblresult2.Text = myDS2.Tables(0).Rows(i).Item(1).ToString & " Is A Required Field!"
                                myTextBox.Focus()
                                Return False
                            Else
                                Select Case myDS2.Tables(0).Rows(i).Item(2).ToString
                                    Case "DATE", "DATETIME"
                                        If IsNumeric(mySetting.ConvertDateToDecimal(myTextBox.Text, Session("Company").ToString, Session("Module").ToString)) Then
                                            ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "='" & mySetting.ConvertDateToDecimal(myTextBox.Text.ToString, Session("Company").ToString, Session("Module").ToString) & "' And "
                                        Else
                                            lblresult2.Text = "Invalid Input Format For " & myDS2.Tables(0).Rows(i).Item(1).ToString
                                            Return False
                                        End If
                                    Case "OPTION"
                                        ssql = ssql & myDS2.Tables(0).Rows(i).Item(0).ToString & "=DBO.FN_GETOPTIONCODE(N'" & Form.ID & "',N'" & myDS2.Tables(0).Rows(i).Item(0).ToString & "',N'" & myTextBox.Text.ToString & "') And "
                                    Case "DECIMAL", "INTEGER"
                                        If IsNumeric(myTextBox.Text.ToString) Then
                                            ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "=N'" & myTextBox.Text.ToString & "' And "
                                        Else
                                            lblresult2.Text = "Invalid Input Format For " & myDS2.Tables(0).Rows(i).Item(1).ToString
                                            Return False
                                        End If
                                    Case Else
                                        Dim myDST2 As New DataSet
                                        myDST2 = mySQL.ExecuteSQL("Select Count(*) From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS2.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='UPDATE'; Select Function_Name From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS2.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='UPDATE'")
                                        If CInt(myDST2.Tables(0).Rows(0).Item(0).ToString) > 0 Then
                                            ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST2.Tables(1).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "'),'') And "
                                        Else
                                            ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "=ISNULL(N'" & myTextBox.Text.ToString & "','') And "
                                        End If
                                End Select
                                Exit For
                            End If
                        End If
                    Next
                Next
            End If
            ssql = Left(ssql, Len(ssql) - 5)
            myDS2 = Nothing
        End If

        Return True
    End Function
#End Region

#Region "Panel Action"

    Protected Sub lnkbtnFirstPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnFirstPage.Click
        _currentPageNumber = 1
        SearchByPage = False
        BindGrid()
    End Sub
    
    Protected Sub lnkbtnPrevPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnPrevPage.Click
        _currentPageNumber = Integer.Parse(CurrentPage.Text) - 1
        SearchByPage = False
        BindGrid()
    End Sub
    
    Protected Sub lnkbtnNextPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnNextPage.Click
        _currentPageNumber = Integer.Parse(CurrentPage.Text) + 1
        SearchByPage = False
        BindGrid()
    End Sub
    
    Protected Sub lnkbtnLastPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLastPage.Click
        _currentPageNumber = Integer.Parse(TotalPages.Text)
        SearchByPage = False
        BindGrid()
    End Sub
    
    Protected Sub imgBtnGoToPage_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnGoToPage.Click
        If Trim(txtGoToPage.Text) <> "" Then
            If CInt(txtGoToPage.Text) <= CInt(Session("TotalPages")) And txtGoToPage.Text > 0 Then
                SearchByPage = True
                BindGrid()
            Else
                ShowMessage("Invalid page! \nPage to search for must be greater than 0 and less than or equal to " & Session("TotalPages") & " !")
                'lblresult.Text = "Invalid page! Page to search for must be greater than 0 and less than or equal to " & Session("TotalPages") & " !"
            End If
        End If
    End Sub
    
	Protected Sub imgBtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAdd.Click
		Dim myDSDefault As New DataSet,myDS1 As New DataSet
        'apply copy or default value function
        CountRecord = 0
        ClearText()
        For i = 0 To myGridView.Rows.Count - 1
            Dim chkEdit As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
            If chkEdit.Checked Then
                CountRecord = CountRecord + 1
            End If
        Next
        If CountRecord = 0 Then
            'get field default value
            Dim num As Integer = 0
            Dim code As Integer = 1
            Dim dt As Integer = 2
            Dim op As Integer = 3
            Dim dv As Integer = 4
            ssql = "exec sp_sa_get_fields_default_value '" & Session("Company") & "','" & Session("Module") & "','" & Form.ID & "'"
            myDSDefault = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

            If myDSDefault.Tables(0).Rows.Count > 0 Then

                For j = 0 To myDSDefault.Tables(0).Rows.Count - 1
                    num = CInt(myDSDefault.Tables(0).Rows(j).Item(0).ToString)

                    If UCase(Trim(myDSDefault.Tables(0).Rows(j).Item(op).ToString)) = "YES" Then
                        Select Case UCase(Trim(myDSDefault.Tables(0).Rows(j).Item(dt).ToString))
                            Case "OPTION"
                                Dim myDropdownlist As DropDownList = Page.FindControl("ddl" & Trim(myDSDefault.Tables(0).Rows(j).Item(code)))
                                myDropdownlist.SelectedValue = Trim(myDSDefault.Tables(0).Rows(j).Item(dv).ToString)
                            Case "CHARACTER"
                                Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDSDefault.Tables(0).Rows(j).Item(code)))
                                myTextBox.Text = Trim(myDSDefault.Tables(0).Rows(j).Item(dv).ToString)
                            Case "DATE"
                                Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDSDefault.Tables(0).Rows(j).Item(code)))
                                myTextBox.Text = Trim(myDSDefault.Tables(0).Rows(j).Item(dv).ToString)
                            Case "DATETIME"
                                Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDSDefault.Tables(0).Rows(j).Item(code)))
                                myTextBox.Text = Trim(myDSDefault.Tables(0).Rows(j).Item(dv).ToString)
                            Case "TIME"
                                Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDSDefault.Tables(0).Rows(j).Item(code)))
                                myTextBox.Text = Trim(myDSDefault.Tables(0).Rows(j).Item(dv).ToString)
                            Case "INTEGER"
                                Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDSDefault.Tables(0).Rows(j).Item(code)))
                                myTextBox.Text = Trim(myDSDefault.Tables(0).Rows(j).Item(dv).ToString)
                            Case "DECIMAL"
                                Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDSDefault.Tables(0).Rows(j).Item(code)))
                                myTextBox.Text = Trim(myDSDefault.Tables(0).Rows(j).Item(dv).ToString)
                            Case Else
                                Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDSDefault.Tables(0).Rows(j).Item(code)))
                                myTextBox.Text = ""
                        End Select
                    End If

                Next
            End If
            myDSDefault = Nothing
        ElseIf CountRecord = 1 Then
            For i = 0 To myGridView.Rows.Count - 1
                Dim chkEdit As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
                If chkEdit.Checked Then
                    chkEdit.Checked = False

                    'get field position
                    Dim num As Integer = 0
                    Dim code As Integer = 1
                    Dim dt As Integer = 3
                    ssql = "exec sp_sa_get_fields_position '" & Form.ID & "'"
                    myDS1 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

                    If myDS1.Tables(0).Rows.Count > 0 Then
                        For j = 0 To myDS1.Tables(0).Rows.Count - 1
                            num = CInt(myDS1.Tables(0).Rows(j).Item(0).ToString)

                            Select Case UCase(Trim(myDS1.Tables(0).Rows(j).Item(dt).ToString))
                                Case "OPTION"
                                    Dim myDropdownlist As DropDownList = Page.FindControl("ddl" & Trim(myDS1.Tables(0).Rows(j).Item(code)))
                                    If Trim(gvHistory.Rows(i).Cells(num).Text) = "&nbsp;" Then
                                        myDropdownlist.SelectedValue = ""
                                    Else
                                        mySetting.ArrangeDropdownlistSelectedIndex(myDropdownlist, Server.HtmlDecode(gvHistory.Rows(i).Cells(num).Text))
                                    End If
                                Case Else
                                    Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS1.Tables(0).Rows(j).Item(code)))
                                    If Trim(gvHistory.Rows(i).Cells(num).Text) = "&nbsp;" Then
                                        myTextBox.Text = ""
                                    Else
                                        myTextBox.Text = Replace(Server.HtmlDecode(gvHistory.Rows(i).Cells(num).Text), "amp;", "")
                                    End If
                            End Select

                        Next
                    End If
                    myDS1 = Nothing
                    Exit For
                End If
            Next
        ElseIf CountRecord > 1 Then
            lblresult.Visible = True
            lblresult.Text = "Invalid action: Only 1 row can be selected for adding..."
            Exit Sub
        End If

        pnlEdit.Visible = True
        pnldescription.Visible = True
        pnlMain.Visible = False
        lblresult2.Visible = True
        EnableAll()

        imgBtnSearch.Visible = False
        imgBtnUpdate.Visible = False
        imgBtnSubmit.Visible = True

        AutoAdjustPosition("2")

        imgBtnCancel.CssClass = buttonPosition3
        imgBtnClear.CssClass = buttonPosition2
        imgBtnSubmit.CssClass = buttonPosition1

        'panel setting
        imgtop.CssClass = "Display_0"
        imgbottom.CssClass = panelPosition & autonum
        mySetting.GetImgTypeUrl(imgtop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Add.png")
        mySetting.GetImgTypeUrl(imgbottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")

    End Sub
    
    Protected Sub imgBtnEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnEdit.Click
        If ActionValidateEdit() Then
            pnlEdit.Visible = True
            pnldescription.Visible = True
            pnlMain.Visible = False

            imgBtnSearch.Visible = False
            imgBtnSubmit.Visible = False
            imgBtnUpdate.Visible = True

            'LoadKeyImage()
            SetEditable()
            For i = 0 To myGridView.Rows.Count - 1
                Dim chkEdit As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
                If chkEdit.Checked Then
                    chkEdit.Checked = False
                    myDS = New DataSet
                    myDS = mySQL.ExecuteSQL("SELECT Code,Option_Data_Type,Name FROM Table_Field Where Table_Profile_Code='" & Form.ID & "' ORDER BY SEQUENCE_NO")
                    For j = 0 To myDS.Tables(0).Rows.Count - 1
                        For intColumn = 1 To gvHistory.HeaderRow.Cells.Count - 1
                            If gvHistory.HeaderRow.Cells(intColumn).Text.ToString = myDS.Tables(0).Rows(j).Item(2).ToString Then
                                Select Case myDS.Tables(0).Rows(j).Item(1).ToString.Trim
                                    Case "OPTION"
                                        Dim myDDL As DropDownList = Page.FindControl("ddl" & myDS.Tables(0).Rows(j).Item(0).ToString.Trim)
                                        mySetting.ArrangeDDLSelectedIndex(myDDL, clsGlobalSetting.DDLSelection.SelectedText, Replace(Replace(server.HtmlDecode(gvHistory.Rows(i).Cells(intColumn).Text.ToString.Trim), "&nbsp;", "&"), "&amp;", "&"))
                                        myDDL = Nothing
                                    Case Else
                                        Dim myTXT As TextBox = Page.FindControl("txt" & myDS.Tables(0).Rows(j).Item(0).ToString.Trim)
                                        If gvHistory.Rows(i).Cells(j + 1).Text.Trim = "&nbsp;" Then
                                            myTXT.Text = ""
                                        Else
                                            myTXT.Text = Replace(Replace(server.HtmlDecode(gvHistory.Rows(i).Cells(intColumn).Text.ToString.Trim), "&nbsp;", "&"), "&amp;", "&")
                                        End If
                                        myTXT = Nothing
                                End Select
                            End If
                        Next
                    Next
                    myDS = Nothing
                    chkEdit = Nothing
                    Exit For
                End If
            Next

            AutoAdjustPosition("3")

            'check for editable field
            If autonum = 0 Then
                ShowMessage("No editable item found for this page...")
                Exit Sub
            End If

            imgBtnCancel.CssClass = buttonPosition3
            imgBtnClear.CssClass = buttonPosition2
            imgBtnUpdate.CssClass = buttonPosition1
            mySetting.GetImgTypeUrl(imgtop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Edit.png")
            mySetting.GetImgTypeUrl(imgbottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")
        End If
    End Sub
    
    Protected Sub imgBtnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnDelete.Click
        If ActionValidateDelete() Then
            For i = 0 To myGridView.Rows.Count - 1
                Dim chkDelete As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
                If chkDelete.Checked Then
                    ssql = mySetting.GetSQLParameter(Form.ID, clsGlobalSetting.SQLAction.DELETE_Statement, gvHistory, i, Session("Company").ToString, Session("Module").ToString)
                    If ssql <> Nothing Then
                        mySQL.ExecuteSQLNonQuery(ssql, Session("Company").ToString, Session("EmpID").ToString)
                        ssql = Nothing
                    End If
                    chkDelete.Checked = False
                End If
            Next
            BindGrid()
        End If
    End Sub
    
    Protected Sub imgBtnFilter_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnFilter.Click
        pnlEdit.Visible = True
        pnldescription.Visible = True
        pnlMain.Visible = False
        imgBtnSubmit.Visible = False
        imgBtnUpdate.Visible = False
        imgBtnSearch.Visible = True

        ClearText()
        EnableAll()
        AutoAdjustPosition("1")

        'check for addable field
        If autonum = 0 Then
            ShowMessage("No addable item found for this page...")
            pnlEdit.Visible = False
            pnlMain.Visible = True
            Exit Sub
        End If

        imgBtnCancel.CssClass = buttonPosition3
        imgBtnClear.CssClass = buttonPosition2
        imgBtnSearch.CssClass = buttonPosition1

        mySetting.GetImgTypeUrl(imgtop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Filter.png")
        mySetting.GetImgTypeUrl(imgbottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")

    End Sub
#End Region
#End Region

#Region "Sub & Function"
    Private Function ActionValidateDelete() As Boolean
        RecFound = False
        For i = 0 To myGridView.Rows.Count - 1
            Dim chkDelete As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
            If chkDelete.Checked Then
                RecFound = True
                Exit For
            End If
            chkDelete = Nothing
        Next
        If RecFound = True Then
            Return True
        Else
            ShowMessage("No row selected for deleting. Please select at least one row!")
            Return False
        End If
    End Function
    
    Private Function ActionValidateEdit() As Boolean
        RecFound = False
        CountRecord = 0
        For i = 0 To myGridView.Rows.Count - 1
            Dim chkEdit As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
            If chkEdit.Checked Then
                RecFound = True
                CountRecord = CountRecord + 1
            End If
            chkEdit = Nothing
        Next
        If RecFound = True Then
            If CountRecord > 1 Then
                ShowMessage("You select " & CStr(CountRecord) & " rows for editing, which is invalid. Please select only 1 row!")
                Return False
            Else
                Return True
            End If
        Else
            ShowMessage("No row selected for editing. Please select at least one row!")
            Return False
        End If
    End Function
    
    Private Function ActionValidateUpdate() As Boolean
        RecFound = False
        CountRecord = 0
        For i = 0 To myGridView.Rows.Count - 1
            Dim chkUpdate As CheckBox = myGridView.Rows(i).Cells(1).Controls(1)
            If chkUpdate.Checked Then
                RecFound = True
                CountRecord = CountRecord + 1
            End If
            chkUpdate = Nothing
        Next
        If RecFound = True Then
            If CountRecord > 1 Then
                ShowMessage("You select " & CStr(CountRecord) & " rows for updating, which is invalid. Please select only one row!")
                Return False
            Else
                Return True
            End If
        Else
            ShowMessage("No row selected for updating. Please select at least one row!")
            Return False
        End If
    End Function
    
    Sub AutoAdjustPosition(ByVal strSelect As String)

        '//-- Description --------------------------------------------------------------------------//
        'AutoPositioning actually is the enhance version of AutoAdjustPosition,
        'It used to replace previous version of function.

        '//-----------------------------------------------------------------------------------------//

        Try
            'get field position
            Dim code As Integer = 1
            Dim dt As Integer = 3
            Dim posType As Integer = 4
            Dim autonum2 As Integer = 0
            Dim txtnum As Integer = 0
            Dim txtWidth As Integer = 0
            Dim ddlWidth As Integer = 0
            Dim AblWidth As Integer = 0
            Dim EctWidth As Integer = 0

            'get width
            If CInt(Session("GVwidth")) > 680 Then
                AblWidth = (CInt(Session("GVwidth")) - 680)
                EctWidth = AblWidth / 2
                txtWidth = 150 + EctWidth
                ddlWidth = 157 + EctWidth
            Else
                txtWidth = 150
                ddlWidth = 157
                EctWidth = 0
            End If

            'get positioning
            ssql = "exec sp_sa_get_fields_position '" & Form.ID & "'"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            autonum = 0
            If myDS.Tables(0).Rows.Count > 0 Then
                For i = 0 To myDS.Tables(0).Rows.Count - 1
                    Dim myImage As Image = Page.FindControl("imgKey" & Trim(myDS.Tables(0).Rows(i).Item(code)))
                    Dim myLabel As Label = Page.FindControl("lbl" & Trim(myDS.Tables(0).Rows(i).Item(code)))
                    Dim myImageButton As ImageButton = Page.FindControl("imgBtn" & Trim(myDS.Tables(0).Rows(i).Item(code)))
                    Select Case UCase(Trim(myDS.Tables(0).Rows(i).Item(dt).ToString))

                        Case "OPTION"
                            Dim myDropdownlist As DropDownList = Page.FindControl("ddl" & Trim(myDS.Tables(0).Rows(i).Item(code)))
                            Select Case UCase(Trim(myDS.Tables(0).Rows(i).Item(posType).ToString))
                                Case "0"
                                    If myLabel.Visible = True And myDropdownlist.Visible = True Then
                                        autonum = autonum + 1
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myLabel.CssClass = "wordstyle12"
                                        myLabel.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myLabel.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myLabel.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myDropdownlist.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myDropdownlist.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myDropdownlist.Style.Add("position", "absolute")
                                        myDropdownlist.Width = ddlWidth
                                        autonum = autonum + 1
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                    End If
                                Case "1"
                                    If myLabel.Visible = True And myDropdownlist.Visible = True Then
                                        autonum2 = autonum
                                        autonum2 = autonum2 / 4
                                        If autonum2 Mod 2 <> 0 Then
                                            autonum2 = (autonum2 + 3) / 2
                                            autonum = autonum + 5
                                        Else
                                            autonum2 = (autonum2 + 2) / 2
                                            autonum = autonum + 1
                                        End If
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myLabel.CssClass = "wordstyle12"
                                        myLabel.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myLabel.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myLabel.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myDropdownlist.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myDropdownlist.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myDropdownlist.Style.Add("position", "absolute")
                                        myDropdownlist.Width = ddlWidth
                                        autonum = autonum + 1
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                    End If
                                Case "2", "3", "4", "5", "6", "7", "8", "9", "10"
                                    txtnum = CInt(Trim(myDS.Tables(0).Rows(i).Item(posType).ToString))
                                    txtnum = txtnum * 4 'number increase in 4x (per item)
                                    If myLabel.Visible = True And myDropdownlist.Visible = True Then
                                        autonum2 = autonum
                                        autonum2 = autonum2 / 4
                                        If autonum2 Mod 2 <> 0 Then
                                            autonum2 = (autonum2 + 3) / 2
                                            autonum = autonum + 5
                                        Else
                                            autonum2 = (autonum2 + 2) / 2
                                            autonum = autonum + 1
                                        End If
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myLabel.CssClass = "wordstyle12"
                                        myLabel.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myLabel.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myLabel.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myDropdownlist.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myDropdownlist.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myDropdownlist.Style.Add("position", "absolute")
                                        myDropdownlist.Width = ddlWidth
                                        autonum = autonum + 1
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                        autonum = autonum + txtnum
                                    End If
                                Case "MIXED"
                                    If myLabel.Visible = True And myDropdownlist.Visible = True Then
                                        autonum = autonum - 3
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 3
                                        myLabel.Visible = False
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                        myDropdownlist.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myDropdownlist.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myDropdownlist.Style.Add("position", "absolute")
                                        myDropdownlist.Width = ddlWidth
                                        autonum = autonum + 4
                                    End If
                                Case Else
                                    If myLabel.Visible = True And myDropdownlist.Visible = True Then
                                        autonum = autonum + 1
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myLabel.CssClass = "wordstyle12"
                                        myLabel.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myLabel.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myLabel.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myDropdownlist.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myDropdownlist.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myDropdownlist.Style.Add("position", "absolute")
                                        myDropdownlist.Width = ddlWidth
                                        autonum = autonum + 1
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                    End If
                            End Select

                        Case Else
                            Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(0).Rows(i).Item(code)))
                            Select Case UCase(Trim(myDS.Tables(0).Rows(i).Item(posType).ToString))
                                Case "0"
                                    If myLabel.Visible = True And myTextBox.Visible = True Then
                                        autonum = autonum + 1
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myLabel.CssClass = "wordstyle12"
                                        myLabel.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myLabel.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myLabel.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myTextBox.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myTextBox.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myTextBox.Style.Add("position", "absolute")
                                        myTextBox.Width = txtWidth
                                        autonum = autonum + 1
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                    End If
                                Case "1"
                                    If myLabel.Visible = True And myTextBox.Visible = True Then
                                        autonum2 = autonum
                                        autonum2 = autonum2 / 4
                                        If autonum2 Mod 2 <> 0 Then
                                            autonum2 = (autonum2 + 3) / 2
                                            autonum = autonum + 5
                                        Else
                                            autonum2 = (autonum2 + 2) / 2
                                            autonum = autonum + 1
                                        End If
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myLabel.CssClass = "wordstyle12"
                                        myLabel.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myLabel.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myLabel.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myTextBox.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myTextBox.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myTextBox.Style.Add("position", "absolute")
                                        myTextBox.Width = txtWidth
                                        autonum = autonum + 1
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                    End If
                                Case "2", "3", "4", "5", "6", "7", "8", "9", "10"
                                    txtnum = CInt(Trim(myDS.Tables(0).Rows(i).Item(posType).ToString))
                                    txtnum = txtnum * 4 'number increase in 4x (per item)
                                    If myLabel.Visible = True And myTextBox.Visible = True Then
                                        autonum2 = autonum
                                        autonum2 = autonum2 / 4
                                        If autonum2 Mod 2 <> 0 Then
                                            autonum2 = (autonum2 + 3) / 2
                                            autonum = autonum + 5
                                        Else
                                            autonum2 = (autonum2 + 2) / 2
                                            autonum = autonum + 1
                                        End If
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myLabel.CssClass = "wordstyle12"
                                        myLabel.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myLabel.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myLabel.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myTextBox.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myTextBox.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myTextBox.Style.Add("position", "absolute")
                                        myTextBox.Width = txtWidth
                                        autonum = autonum + 1
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                        autonum = autonum + txtnum
                                    End If
                                Case "MIXED"
                                    If myLabel.Visible = True And myTextBox.Visible = True Then
                                        autonum = autonum - 3
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 3
                                        myLabel.Visible = False
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                        myTextBox.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myTextBox.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myTextBox.Style.Add("position", "absolute")
                                        myTextBox.Width = txtWidth
                                        autonum = autonum + 4
                                    End If
                                Case Else
                                    If myLabel.Visible = True And myTextBox.Visible = True Then
                                        autonum = autonum + 1
                                        myImage.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImage.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImage.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myLabel.CssClass = "wordstyle12"
                                        myLabel.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myLabel.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myLabel.Style.Add("position", "absolute")
                                        autonum = autonum + 1
                                        myTextBox.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myTextBox.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myTextBox.Style.Add("position", "absolute")
                                        myTextBox.Width = txtWidth
                                        autonum = autonum + 1
                                        myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                                        myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                                        myImageButton.Style.Add("position", "absolute")
                                    End If
                            End Select
                    End Select
                Next
            End If

            autonum = autonum / 4
            If autonum Mod 2 <> 0 Then
                autonum = autonum + 1
            End If
            autonum = autonum / 2

            buttonPosition1 = buttonPosition1 & autonum - 1
            buttonPosition2 = buttonPosition2 & autonum - 1
            buttonPosition3 = buttonPosition3 & autonum - 1

            labelPosition1 = labelPosition1 & autonum - 1
            lblresult2.CssClass = labelPosition1
            lblresult2.Text = "Click on Cancel Button to return..."
            imgtop.CssClass = "Display_0"
            imgbottom.CssClass = panelPosition & autonum
        Catch ex As Exception
            lblresult.Text = "[AutoAdjustPosition]Error: " & ex.Message
            SetFieldToFalse()
        End Try

    End Sub
    
    Sub SetFieldToFalse()
        pnledit.Visible = False
        pnlmain.Visible = True
    End Sub
    
    Private Sub CheckAll()
        For i = 0 To myGridView.Rows.Count - 1
            Dim chkDelete As CheckBox = myGridView.Rows(i).Cells(1).Controls(1)
            chkDelete.Checked = True
            chkDelete = Nothing
        Next
        For i = 0 To gvHistory.Rows.Count - 1
            Dim chkDelete As CheckBox = gvHistory.Rows(i).Cells(1).Controls(1)
            chkDelete.Checked = True
            chkDelete = Nothing
        Next
    End Sub
    
    Private Sub ClearText()
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL("SELECT Code,Option_Data_Type FROM Table_Field Where Table_Profile_Code='" & Form.ID & "' AND Option_View_Card='YES' ORDER BY SEQUENCE_NO")
        For i = 0 To myDS.Tables(0).Rows.Count - 1
            Select Case myDS.Tables(0).Rows(i).Item(1).ToString.Trim
                Case "OPTION"
                    Dim myDDL As DropDownList = Page.FindControl("ddl" & myDS.Tables(0).Rows(i).Item(0).ToString.Trim)
                    If myDDL.Items.Count > 0 Then
                        myDDL.SelectedIndex = -1
                    End If
                    myDDL = Nothing
                Case Else
                    Dim myTXT As TextBox = Page.FindControl("txt" & myDS.Tables(0).Rows(i).Item(0).ToString.Trim)
                    myTXT.Text = ""
                    myTXT = Nothing
            End Select
        Next
        myDS = Nothing
    End Sub
    
    Private Sub EnableAll()
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL("SELECT Code,Option_Primary_Key,Option_Data_Type,Option_Mandatory FROM Table_Field Where Table_Profile_Code='" & Form.ID & "' AND Option_View_Card='YES' ORDER BY SEQUENCE_NO")
        For i = 0 To myDS.Tables(0).Rows.Count - 1
            Dim myImageKey As ImageButton = Page.FindControl("imgKey" & myDS.Tables(0).Rows(i).Item(0).ToString.Trim)
            Dim myImageButton As ImageButton = Page.FindControl("imgbtn" & myDS.Tables(0).Rows(i).Item(0).ToString.Trim)
            Dim myLabel As Label = Page.FindControl("lbl" & myDS.Tables(0).Rows(i).Item(0).ToString.Trim)

            If myDS.Tables(0).Rows(i).Item(2).ToString.Trim = "LOOKUP" Or _
                myDS.Tables(0).Rows(i).Item(2).ToString.Trim = "DATE" Or _
                myDS.Tables(0).Rows(i).Item(2).ToString.Trim = "DATETIME" Or _
                myDS.Tables(0).Rows(i).Item(2).ToString.Trim = "TIME" Then
                myImageButton.Visible = True
                myImageButton.Enabled = True
            Else
                myImageButton.Visible = False
            End If
            myImageButton = Nothing

            If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" Then 'Option_Mandatory
                myImageKey.Visible = True
            Else
                myImageKey.Visible = False
            End If
            myImageKey = Nothing

            Select Case myDS.Tables(0).Rows(i).Item(2).ToString.Trim
                Case "OPTION"
                    Dim myDDL As DropDownList = Page.FindControl("ddl" & myDS.Tables(0).Rows(i).Item(0).ToString.Trim)
                    myDDL.Enabled = True
                    myDDL.Visible = True
                    myDDL = Nothing
                Case Else
                    Dim myTXT As TextBox = Page.FindControl("txt" & myDS.Tables(0).Rows(i).Item(0).ToString.Trim)
                    myTXT.Enabled = True
                    myTXT.Visible = True
                    myTXT = Nothing
            End Select
        Next
        myDS = Nothing
    End Sub
    
    Private Sub LoadKeyImage()
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL("SELECT Code,Option_Mandatory FROM Table_Field Where Table_Profile_Code='" & Form.ID & "' ORDER BY SEQUENCE_NO")
        For i = 0 To myDS.Tables(0).Rows.Count - 1
            Dim myImgKey As ImageButton = Page.FindControl("imgKey" & myDS.Tables(0).Rows(i).Item(0).ToString)
            If myDS.Tables(0).Rows(i).Item(1).ToString = "YES" Then
                myImgKey.Visible = True
            Else
                myImgKey.Visible = False
            End If
            myImgKey = Nothing
        Next
        myDS = Nothing
    End Sub
    
    Private Sub SetEditable()
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL("Exec sp_sa_GetPageFieldSetting '" & Session("Company").ToString & "','" & Form.ID & "','" & Session("EmpID").ToString & "','EDIT'")
        For i = 0 To myDS.Tables(0).Rows.Count - 1
            Dim myImageKey As ImageButton = Page.FindControl("imgKey" & myDS.Tables(0).Rows(i).Item(0).ToString.Trim)
            Dim myImageButton As ImageButton = Page.FindControl("imgbtn" & myDS.Tables(0).Rows(i).Item(0).ToString.Trim)

            If myDS.Tables(0).Rows(i).Item(1).ToString = "YES" Then 'Option_Primary_Key
                myImageButton.Visible = False
            Else
                If myDS.Tables(0).Rows(i).Item(2).ToString.Trim = "LOOKUP" Or myDS.Tables(0).Rows(i).Item(2).ToString.Trim = "DATE" Or myDS.Tables(0).Rows(i).Item(2).ToString.Trim = "DATETIME" Then
                    myImageButton.Visible = True
                    myImageButton.Enabled = True
                Else
                    myImageButton.Visible = False
                End If
            End If
            If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" And (myDS.Tables(0).Rows(i).Item(2).ToString.Trim = "LOOKUP" Or myDS.Tables(0).Rows(i).Item(2).ToString.Trim = "DATE" Or myDS.Tables(0).Rows(i).Item(2).ToString.Trim = "DATETIME") Then 'Option_Editable
                myImageButton.Visible = True
            Else
                myImageButton.Visible = False
            End If
            myImageButton = Nothing

            If myDS.Tables(0).Rows(i).Item(4).ToString = "YES" Then 'Option_Mandatory
                myImageKey.Visible = True
            Else
                myImageKey.Visible = False
            End If
            myImageKey = Nothing

            Select Case myDS.Tables(0).Rows(i).Item(2).ToString.Trim
                Case "OPTION"
                    Dim myDDL As DropDownList = Page.FindControl("ddl" & myDS.Tables(0).Rows(i).Item(0).ToString.Trim)
                    If myDS.Tables(0).Rows(i).Item(3).ToString.Trim = "YES" Then
                        myDDL.Enabled = True
                    Else
                        myDDL.Enabled = False
                    End If
                    myDDL = Nothing
                Case Else
                    Dim myTXT As TextBox = Page.FindControl("txt" & myDS.Tables(0).Rows(i).Item(0).ToString.Trim)
                    If myDS.Tables(0).Rows(i).Item(3).ToString.Trim = "YES" Then
                        myTXT.Enabled = True
                    Else
                        myTXT.Enabled = False
                    End If
                    myTXT = Nothing
            End Select
        Next
        myDS = Nothing
    End Sub
    
    Private Sub ShowMessage(ByVal message As String)
        Dim scriptString As String = "<script language=JavaScript>"
        scriptString += "alert('" + message + "');"
        scriptString += "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "ShowMessage", scriptString)
    End Sub
    
    Private Sub UncheckAll()
        For i = 0 To myGridView.Rows.Count - 1
            Dim chkDelete As CheckBox = myGridView.Rows(i).Cells(1).Controls(1)
            chkDelete.Checked = False
            chkDelete = Nothing
        Next
        For i = 0 To gvHistory.Rows.Count - 1
            Dim chkDelete As CheckBox = gvHistory.Rows(i).Cells(1).Controls(1)
            chkDelete.Checked = False
            chkDelete = Nothing
        Next
    End Sub
    
    Private Function ValidateInsert() As Boolean
        lblresult2.Visible = True
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL("Select Code,Option_Data_Type,Option_Editable,Option_Mandatory,Option_Default_Value,Name,Option_Primary_Key From Table_Field Where Table_Profile_Code='" & Form.ID & "' And Option_View_Card='YES' Order By Table_Profile_Code,Sequence_No")
        ssql = "Insert Into " & Form.ID & "("
        ssql2 = ") Values("
        ssql3 = "Select * From " & Form.ID & " Where "
        If myDS.Tables(0).Rows.Count > 0 Then
            For i = 0 To myDS.Tables(0).Rows.Count - 1
                Select Case myDS.Tables(0).Rows(i).Item(1).ToString
                    Case "DATE"
                        Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                        If myTextBox.Visible = True Then
                            If mySetting.CheckTextNullAndReplaceSingleQuote(myTextBox) = True Then
                                If IsNumeric(mySetting.ConvertDateToDecimal(myTextBox.Text, Session("Company").ToString, Session("Module").ToString)) = True Then
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                                    Dim myDST As New DataSet
                                    myDST = mySQL.ExecuteSQL("Select Function_Name,Default_Value From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='INSERT'")
                                    If myDST.Tables(0).Rows.Count > 0 Then
                                        If myDST.Tables(0).Rows(0).Item(1).ToString.Trim = "" Then
                                            ssql2 = ssql2 & "ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "'),'0'),"
                                        Else
                                            ssql2 = ssql2 & "ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(" & myDST.Tables(0).Rows(0).Item(1).ToString.Trim & "),'0'),"
                                        End If
                                    Else
                                        ssql2 = ssql2 & "'" & mySetting.ConvertDateToDecimal(Trim(myTextBox.Text), Session("Company").ToString, Session("Module").ToString) & "',"
                                    End If
                                    myDST = Nothing
                                    If myDS.Tables(0).Rows(i).Item(6).ToString = "YES" Then
                                        ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "='" & mySetting.ConvertDateToDecimal(myTextBox.Text, Session("Company").ToString, Session("Module").ToString) & "' And "
                                    End If
                                Else
                                    If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" Then
                                        lblresult2.Text = "Invalid Input Date Format For [" & Trim(myDS.Tables(0).Rows(i).Item(5)) & "] !"
                                        myTextBox.Focus()
                                        Return False
                                    Else
                                        ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                                        ssql2 = ssql2 & "'" & myTextBox.Text & "',"
                                    End If
                                End If
                            Else
                                If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" Then
                                    lblresult2.Text = "[" & Trim(myDS.Tables(0).Rows(i).Item(5)) & "] Is A Required Field!"
                                    myTextBox.Focus()
                                    Return False
                                Else
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                                    ssql2 = ssql2 & "'" & myTextBox.Text & "',"
                                End If
                            End If
                        End If
                        myTextBox = Nothing
                    Case "DATETIME"
                        Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                        If myTextBox.Visible = True Then
                            If mySetting.CheckTextNullAndReplaceSingleQuote(myTextBox) = True Then
                                If IsNumeric(mySetting.UnDisplayDateTime(myTextBox.Text, Session("Company").ToString, Session("Module").ToString)) = True Then
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                                    Dim myDST As New DataSet
                                    myDST = mySQL.ExecuteSQL("Select Function_Name,Default_Value From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='INSERT'")
                                    If myDST.Tables(0).Rows.Count > 0 Then
                                        If myDST.Tables(0).Rows(0).Item(1).ToString.Trim = "" Then
                                            ssql2 = ssql2 & "ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "'),'0'),"
                                        Else
                                            ssql2 = ssql2 & "ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(" & myDST.Tables(0).Rows(0).Item(1).ToString.Trim & "),'0'),"
                                        End If
                                    Else
                                        ssql2 = ssql2 & "'" & mySetting.UnDisplayDateTime(Trim(myTextBox.Text), Session("Company").ToString, Session("Module").ToString) & "',"
                                    End If
                                    myDST = Nothing
                                    If myDS.Tables(0).Rows(i).Item(6).ToString = "YES" Then
                                        ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "='" & mySetting.UnDisplayDateTime(myTextBox.Text, Session("Company").ToString, Session("Module").ToString) & "' And "
                                    End If
                                Else
                                    If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" Then
                                        lblresult2.Text = "Invalid Input DateTime Format For [" & Trim(myDS.Tables(0).Rows(i).Item(5)) & "] !"
                                        myTextBox.Focus()
                                        Return False
                                    Else
                                        ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                                        ssql2 = ssql2 & "'" & myTextBox.Text & "',"
                                    End If
                                End If
                            Else
                                If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" Then
                                    lblresult2.Text = "[" & Trim(myDS.Tables(0).Rows(i).Item(5)) & "] Is A Required Field!"
                                    myTextBox.Focus()
                                    Return False
                                Else
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                                    ssql2 = ssql2 & "'" & myTextBox.Text & "',"
                                End If
                            End If
                        End If
                        myTextBox = Nothing
                    Case "TIME"
                        Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                        If myTextBox.Visible = True Then
                            If mySetting.CheckTextNullAndReplaceSingleQuote(myTextBox) = True Then
                                If IsNumeric(mySetting.UnDisplayTime(myTextBox.Text.ToString, Session("Company").ToString, Session("Module").ToString)) = True Then
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                                    Dim myDST As New DataSet
                                    myDST = mySQL.ExecuteSQL("Select Function_Name,Default_Value From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='INSERT'")
                                    If myDST.Tables(0).Rows.Count > 0 Then
                                        If myDST.Tables(0).Rows(0).Item(1).ToString.Trim = "" Then
                                            ssql2 = ssql2 & "ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "'),'0'),"
                                        Else
                                            ssql2 = ssql2 & "ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(" & myDST.Tables(0).Rows(0).Item(1).ToString.Trim & "),'0'),"
                                        End If
                                    Else
                                        ssql2 = ssql2 & "'" & mySetting.UnDisplayTime(Trim(myTextBox.Text), Session("Company").ToString, Session("Module").ToString) & "',"
                                    End If
                                    myDST = Nothing
                                    If myDS.Tables(0).Rows(i).Item(6).ToString = "YES" Then
                                        ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "='" & mySetting.UnDisplayTime(myTextBox.Text, Session("Company").ToString, Session("Module").ToString) & "' And "
                                    End If
                                Else
                                    If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" Then
                                        lblresult2.Text = "Invalid Input Time Format For [" & Trim(myDS.Tables(0).Rows(i).Item(5)) & "] !"
                                        myTextBox.Focus()
                                        Return False
                                    Else
                                        ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                                        ssql2 = ssql2 & "'" & myTextBox.Text & "',"
                                    End If
                                End If
                            Else
                                If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" Then
                                    lblresult2.Text = "[" & Trim(myDS.Tables(0).Rows(i).Item(5)) & "] Is A Required Field!"
                                    myTextBox.Focus()
                                    Return False
                                Else
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                                    ssql2 = ssql2 & "'" & myTextBox.Text & "',"
                                End If
                            End If
                        End If
                        myTextBox = Nothing
                    Case "OPTION"
                        Dim myDropdownlist As DropDownList = Page.FindControl("ddl" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                        If myDropdownlist.Visible = True Then
                            If mySetting.CheckTextNull(myDropdownlist.SelectedValue) = True Then
                                If mySetting.ValidateInput(myDropdownlist, myDS.Tables(0).Rows(i).Item(1).ToString) = True Then
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                                    ssql2 = ssql2 & "'" & Trim(myDropdownlist.SelectedValue) & "',"
                                    If myDS.Tables(0).Rows(i).Item(6).ToString = "YES" Then
                                        ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "='" & Trim(myDropdownlist.SelectedValue) & "' And "
                                    End If
                                Else
                                    lblresult2.Text = "Invalid Selection For [" & Trim(myDS.Tables(0).Rows(i).Item(5)) & "] !"
                                    myDropdownlist.Focus()
                                    Return False
                                End If
                            Else
                                If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" Then
                                    lblresult2.Text = "[" & Trim(myDS.Tables(0).Rows(i).Item(5)) & "] Is A Required Field!"
                                    myDropdownlist.Focus()
                                    Return False
                                Else
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                                    ssql2 = ssql2 & "'" & Trim(myDropdownlist.SelectedValue) & "',"
                                End If
                            End If
                        End If
                        myDropdownlist = Nothing
                    Case "DECIMAL", "INTEGER"
                        Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                        If myTextBox.Visible = True Then
                            If mySetting.CheckTextNullAndReplaceSingleQuote(myTextBox) = True Then
                                If IsNumeric(myTextBox.Text) = True Then
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                                    ssql2 = ssql2 & "'" & Trim(myTextBox.Text) & "',"
                                    If myDS.Tables(0).Rows(i).Item(6).ToString = "YES" Then
                                        ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "='" & Trim(myTextBox.Text) & "' And "
                                    End If
                                Else
                                    lblresult2.Text = "Invalid Input Numeric Format For [" & Trim(myDS.Tables(0).Rows(i).Item(5)) & "] !"
                                    myTextBox.Focus()
                                    Return False
                                End If
                            Else
                                If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" Then
                                    lblresult2.Text = "[" & Trim(myDS.Tables(0).Rows(i).Item(5)) & "] Is A Required Field!"
                                    myTextBox.Focus()
                                    Return False
                                Else
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                                    ssql2 = ssql2 & "'0',"
                                End If
                            End If
                        End If
                        myTextBox = Nothing
                    Case Else   '"CHARACTER", "LOOKUP"
                        Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                        If myTextBox.Visible = True Then
                            If mySetting.CheckTextNullAndReplaceSingleQuote(myTextBox) = True Then
                                ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                                If myDS.Tables(0).Rows(i).Item(0).ToString.ToUpper = "EMPLOYEE_PROFILE_ID" Then
                                    ssql2 = ssql2 & "ISNULL(dbo.fn_ReturnEmpIDByCodeName('" & Session("Company").ToString & "','" & myTextBox.Text.ToString & "'),''),"
                                    ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(dbo.fn_ReturnEmpIDByCodeName(N'" & Session("Company").ToString & "',N'" & myTextBox.Text.ToString & "'),'') And "
                                Else
                                    Dim myDST As New DataSet
                                    myDST = mySQL.ExecuteSQL("Select Function_Name,Default_Value From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='INSERT'")
                                    If myDST.Tables(0).Rows.Count > 0 Then
                                        If myDST.Tables(0).Rows(0).Item(1).ToString.Trim = "" Then
                                            ssql2 = ssql2 & "ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "'),''),"
                                        Else
                                            ssql2 = ssql2 & "ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString.Trim & "'),''),"
                                        End If
                                        If myDS.Tables(0).Rows(i).Item(6).ToString = "YES" Then
                                            If myDST.Tables(0).Rows(0).Item(1).ToString.Trim = "" Then
                                                ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "'),0) And "
                                            Else
                                                ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString.Trim & "'),0) And "
                                            End If
                                        End If
                                    Else
                                        ssql2 = ssql2 & "'" & Trim(myTextBox.Text) & "',"
                                        If myDS.Tables(0).Rows(i).Item(6).ToString = "YES" Then
                                            ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "='" & Trim(myTextBox.Text) & "' And "
                                        End If
                                    End If
                                    myDST = Nothing
                                End If
                            Else
                                If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" Then
                                    lblresult2.Text = "[" & Trim(myDS.Tables(0).Rows(i).Item(5)) & "] Is A Required Field!"
                                    myTextBox.Focus()
                                    Return False
                                Else
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                                    ssql2 = ssql2 & "'" & Trim(myTextBox.Text) & "',"
                                End If
                            End If
                        End If
                        myTextBox = Nothing
                End Select
            Next
            myDS = Nothing

            Dim DefaultDS As New DataSet
            DefaultDS = mySQL.ExecuteSQL("SELECT Code,Option_Data_Type,Default_Value_Date,Default_Value_DateTime,Default_Value_Time,Default_Value_Integer,Default_Value_Decimal,Default_Value_Character,Option_Primary_Key FROM Table_Field Where Table_Profile_Code='" & Form.ID & "' AND Option_View_Card='NO' AND Option_Default_Value='YES' ORDER BY Sequence_No")
            If Not DefaultDS Is Nothing Then
                If DefaultDS.Tables.Count > 0 Then
                    If DefaultDS.Tables(0).Rows.Count > 0 Then
                        For i = 0 To DefaultDS.Tables(0).Rows.Count - 1
                            Dim DefaultDR As DataRow = DefaultDS.Tables(0).Rows(i)
                            Select Case DefaultDR(1).ToString
                                Case "DATE"
                                    If DefaultDR("Default_Value_Date").ToString.Trim <> "" Then
                                        ssql = ssql & DefaultDR(0).ToString & ","
                                        ssql2 = ssql2 & "'" & DefaultDR("Default_Value_Date").ToString.Trim & "',"
                                        If DefaultDR(8).ToString = "YES" Then
                                            ssql3 = ssql3 & DefaultDR(0).ToString & "='" & DefaultDR("Default_Value_Date").ToString.Trim & "' And "
                                        End If
                                    Else
                                        ssql = ssql & DefaultDR(0).ToString & ","
                                        ssql2 = ssql2 & "dbo.fn_GetCurrentDate(GetDate()),"
                                        If DefaultDR(8).ToString = "YES" Then
                                            ssql3 = ssql3 & DefaultDR(0).ToString & "=dbo.fn_GetCurrentDate(GetDate()) And "
                                        End If
                                    End If
                                Case "DATETIME"
                                    If DefaultDR("Default_Value_DateTime").ToString.Trim <> "" Then
                                        ssql = ssql & DefaultDR(0).ToString & ","
                                        ssql2 = ssql2 & "'" & DefaultDR("Default_Value_DateTime").ToString.Trim & "',"
                                        If DefaultDR(8).ToString = "YES" Then
                                            ssql3 = ssql3 & DefaultDR(0).ToString & "='" & DefaultDR("Default_Value_DateTime").ToString.Trim & "' And "
                                        End If
                                    Else
                                        ssql = ssql & DefaultDR(0).ToString & ","
                                        ssql2 = ssql2 & "dbo.fn_GetCurrentDateTime(GetDate()),"
                                        If DefaultDR(8).ToString = "YES" Then
                                            ssql3 = ssql3 & DefaultDR(0).ToString & "=dbo.fn_GetCurrentDateTime(GetDate()) And "
                                        End If
                                    End If
                                Case "TIME"
                                    If DefaultDR("Default_Value_Time").ToString.Trim <> "" Then
                                        ssql = ssql & DefaultDR(0).ToString & ","
                                        ssql2 = ssql2 & "'" & DefaultDR("Default_Value_Time").ToString.Trim & "',"
                                        If DefaultDR(8).ToString = "YES" Then
                                            ssql3 = ssql3 & DefaultDR(0).ToString & "='" & DefaultDR("Default_Value_Time").ToString.Trim & "' And "
                                        End If
                                    Else
                                        ssql = ssql & DefaultDR(0).ToString & ","
                                        ssql2 = ssql2 & "dbo.fn_GetCurrentTime(GetDate()),"
                                        If DefaultDR(8).ToString = "YES" Then
                                            ssql3 = ssql3 & DefaultDR(0).ToString & "=dbo.fn_GetCurrentTime(GetDate()) And "
                                        End If
                                    End If
                                Case "OPTION"
                                    'Do nothing
                                Case "LOOKUP"
                                    'Do nothing
                                Case "CHARACTER"
                                    If DefaultDR("Default_Value_Character").ToString.Trim <> "" Then
                                        ssql = ssql & DefaultDR(0).ToString & ","
                                        ssql2 = ssql2 & "'" & DefaultDR("Default_Value_Character").ToString.Trim & "',"
                                        If DefaultDR(8).ToString = "YES" Then
                                            ssql3 = ssql3 & DefaultDR(0).ToString & "='" & DefaultDR("Default_Value_Character").ToString.Trim & "' And "
                                        End If
                                    Else
                                        ssql = ssql & DefaultDR(0).ToString & ","
                                        ssql2 = ssql2 & "'',"
                                        If DefaultDR(8).ToString = "YES" Then
                                            ssql3 = ssql3 & DefaultDR(0).ToString & "='' And "
                                        End If
                                    End If
                                Case "INTEGER"
                                    If DefaultDR("Default_Value_Integer").ToString.Trim <> "" Then
                                        ssql = ssql & DefaultDR(0).ToString & ","
                                        ssql2 = ssql2 & "'" & DefaultDR("Default_Value_Integer").ToString.Trim & "',"
                                        If DefaultDR(8).ToString = "YES" Then
                                            ssql3 = ssql3 & DefaultDR(0).ToString & "='" & DefaultDR("Default_Value_Integer").ToString.Trim & "' And "
                                        End If
                                    Else
                                        ssql = ssql & DefaultDR(0).ToString & ","
                                        ssql2 = ssql2 & "'0',"
                                        If DefaultDR(8).ToString = "YES" Then
                                            ssql3 = ssql3 & DefaultDR(0).ToString & "='0' And "
                                        End If
                                    End If
                                Case "DECIMAL"
                                    If DefaultDR("Default_Value_Decimal").ToString.Trim <> "" Then
                                        ssql = ssql & DefaultDR(0).ToString & ","
                                        ssql2 = ssql2 & "'" & DefaultDR("Default_Value_Decimal").ToString.Trim & "',"
                                        If DefaultDR(8).ToString = "YES" Then
                                            ssql3 = ssql3 & DefaultDR(0).ToString & "='" & DefaultDR("Default_Value_Decimal").ToString.Trim & "' And "
                                        End If
                                    Else
                                        ssql = ssql & DefaultDR(0).ToString & ","
                                        ssql2 = ssql2 & "'0.00',"
                                        If DefaultDR(8).ToString = "YES" Then
                                            ssql3 = ssql3 & DefaultDR(0).ToString & "='0.00' And "
                                        End If
                                    End If
                            End Select
                            DefaultDR = Nothing
                        Next
                    End If
                End If
            End If
            DefaultDS = Nothing
			Dim myTXT As TextBox
            myTXT = Me.FindControl("txtUSER_PROFILE_CODE_CREATE")
            If Not myTXT Is Nothing Then '
                ssql = ssql & "USER_PROFILE_CODE_CREATE,DATETIME_CREATE,USER_PROFILE_CODE_MODIFY,DATETIME_MODIFY,"
                ssql2 = ssql2 & "'" & Session("EmpID").ToString & "',dbo.fn_GetCurrentDateTime(GetDate()),'" & Session("EmpID").ToString & "',dbo.fn_GetCurrentDateTime(GetDate()),"
            End If
            
            ssql = Left(ssql, Len(ssql) - 1) & Left(ssql2, Len(ssql2) - 1) & ")"
            ssql3 = Left(ssql3, Len(ssql3) - 5)
        End If

        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql3)
        If myDS.Tables(0).Rows.Count > 0 Then
            lblresult2.Text = "Duplicate Record Found!"
            Return False
        End If
        myDS = Nothing
        Return True

    End Function
    Private Function ValidateSearch() As Boolean
		Dim htbFilterCriteria As New Hashtable
        Dim tmpssql As String = "", strSplit As String = ""
        Dim SearchCriteriaFound As Boolean = False
        lblresult2.Visible = True
        ssql = ""
        ssql2 = ""
        ssql3 = ""
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL("Select Code,Name,Option_Data_Type From Table_Field Where Table_Profile_Code='" & Form.ID & "' And Option_View_Card='YES' Order By Table_Profile_Code,Sequence_No")
        If Not myDS Is Nothing Then
            If myDS.Tables.Count > 0 Then
                For i = 0 To myDS.Tables(0).Rows.Count - 1
                    Dim myDR As DataRow = myDS.Tables(0).Rows(i)
                    Select Case myDR(2).ToString
                        Case "OPTION"
                            Dim myDDL As DropDownList = Page.FindControl("ddl" & myDR(0).ToString)
                            If myDDL.Visible = True And myDDL.SelectedValue <> "" Then
                                ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                                ssql2 = ssql2 & myDR(0).ToString & "@@"
                                ssql3 = ssql3 & Trim(myDDL.SelectedValue) & "@@"
                                htbFilterCriteria.Add(myDR(0).ToString.Trim, myDDL.SelectedValue.ToString.Trim)
                            End If
                            myDDL = Nothing
                        Case "DATE"
                            Dim myTxt As TextBox = Page.FindControl("txt" & myDR(0).ToString)
                            If myTxt.Visible = True Then
                                If Trim(myTxt.Text.ToString) <> "" Then
                                    If mySetting.ValidateQuerySyntax(myTxt.Text.ToString) = True Then
                                        strSplit = mySetting.ProcessQuerySyntax(myTxt.Text.ToString)
                                        Dim intPos As Integer, arr As New ArrayList, intLeftMidRight As Integer
                                        intPos = InStr(myTxt.Text.ToString, strSplit)
                                        If intPos = 1 Then
                                            intLeftMidRight = -1
                                        ElseIf intPos > 1 And intPos < Len(myTxt.Text.ToString) Then
                                            intLeftMidRight = 0
                                        ElseIf intPos = Len(myTxt.Text.ToString) Then
                                            intLeftMidRight = 1
                                        End If
                                        mySetting.AddQuerySyntax(arr, myTxt.Text.ToString)
										If arr.Count = 0 Then
                                            arr.Add(myTxt.Text.ToString.Trim)
                                        End If
                                        For j = 0 To arr.Count - 1
                                            If Trim(arr(j).ToString) <> "" Then
                                                If Not IsNumeric(mySetting.ConvertDateToDecimal(arr(j).ToString, Session("Company").ToString, Session("Module").ToString)) = True Then
                                                    lblresult2.Text = "Invalid Input [Date] Format For [" & myDR(1).ToString & "] With Value [" & arr(j).ToString & "]"
                                                    myTxt.Focus()
                                                    Return False
                                                Else
                                                    Select Case intLeftMidRight
                                                        Case -1
                                                            tmpssql = tmpssql & strSplit & mySetting.ConvertDateToDecimal(arr(j).ToString, Session("Company").ToString, Session("Module").ToString)
                                                        Case 0
                                                            tmpssql = tmpssql & mySetting.ConvertDateToDecimal(arr(j).ToString, Session("Company").ToString, Session("Module").ToString) & strSplit
                                                        Case 1
                                                            tmpssql = tmpssql & mySetting.ConvertDateToDecimal(arr(j).ToString, Session("Company").ToString, Session("Module").ToString) & strSplit
                                                    End Select
                                                End If
                                            End If
                                        Next
                                        Select Case intLeftMidRight
                                            Case -1
                                                'Do nothing
                                            Case 0
                                                If Right(tmpssql, Len(strSplit)) = strSplit Then
                                                    tmpssql = Left(tmpssql, Len(tmpssql) - Len(strSplit))
                                                End If
                                            Case 1
                                                'Do nothing
                                        End Select
                                        ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                                        ssql2 = ssql2 & myDR(0).ToString & "@@"
                                        ssql3 = ssql3 & tmpssql & "@@"
                                        htbFilterCriteria.Add(myDR(0).ToString.Trim, tmpssql)
                                    Else
                                        lblresult2.Text = "More than 1 syntax found in the [" & myDR(1).ToString & "]"
                                        myTxt.Focus()
                                        Return False
                                    End If
                                End If
                            End If
                            myTxt = Nothing
                            tmpssql = ""
                        Case "DATETIME"
                            Dim myTxt As TextBox = Page.FindControl("txt" & myDR(0).ToString)
                            If myTxt.Visible = True Then
                                If Trim(myTxt.Text.ToString) <> "" Then
                                    If mySetting.ValidateQuerySyntax(myTxt.Text.ToString) = True Then
                                        strSplit = mySetting.ProcessQuerySyntax(myTxt.Text.ToString)
                                        Dim intPos As Integer, arr As New ArrayList, intLeftMidRight As Integer
                                        intPos = InStr(myTxt.Text.ToString, strSplit)
                                        If intPos = 1 Then
                                            intLeftMidRight = -1
                                        ElseIf intPos > 1 And intPos < Len(myTxt.Text.ToString) Then
                                            intLeftMidRight = 0
                                        ElseIf intPos = Len(myTxt.Text.ToString) Then
                                            intLeftMidRight = 1
                                        End If
                                        mySetting.AddQuerySyntax(arr, myTxt.Text.ToString)
										If arr.Count = 0 Then
                                            arr.Add(myTxt.Text.ToString.Trim)
                                        End If
                                        For j = 0 To arr.Count - 1
                                            If Trim(arr(j).ToString) <> "" Then
                                                If Not IsNumeric(mySetting.UnDisplayDateTime(arr(j).ToString, Session("Company").ToString, Session("Module").ToString)) = True Then
                                                    lblresult2.Text = "Invalid Input [Date] Format For [" & myDR(1).ToString & "] With Value [" & arr(j).ToString & "]"
                                                    myTxt.Focus()
                                                    Return False
                                                Else
                                                    Select Case intLeftMidRight
                                                        Case -1
                                                            tmpssql = tmpssql & strSplit & mySetting.UnDisplayDateTime(arr(j).ToString, Session("Company").ToString, Session("Module").ToString)
                                                        Case 0
                                                            tmpssql = tmpssql & mySetting.UnDisplayDateTime(arr(j).ToString, Session("Company").ToString, Session("Module").ToString) & strSplit
                                                        Case 1
                                                            tmpssql = tmpssql & mySetting.UnDisplayDateTime(arr(j).ToString, Session("Company").ToString, Session("Module").ToString) & strSplit
                                                    End Select
                                                End If
                                            End If
                                        Next
                                        Select Case intLeftMidRight
                                            Case -1
                                                'Do nothing
                                            Case 0
                                                If Right(tmpssql, Len(strSplit)) = strSplit Then
                                                    tmpssql = Left(tmpssql, Len(tmpssql) - Len(strSplit))
                                                End If
                                            Case 1
                                                'Do nothing
                                        End Select
                                        ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                                        ssql2 = ssql2 & myDR(0).ToString & "@@"
                                        ssql3 = ssql3 & tmpssql & "@@"
                                        htbFilterCriteria.Add(myDR(0).ToString.Trim, tmpssql)
                                    Else
                                        lblresult2.Text = "More than 1 syntax found in the [" & myDR(1).ToString & "]"
                                        myTxt.Focus()
                                        Return False
                                    End If
                                End If
                            End If
                            myTxt = Nothing
                            tmpssql = ""
                        Case "TIME"
                            Dim myTxt As TextBox = Page.FindControl("txt" & myDR(0).ToString)
                            If myTxt.Visible = True Then
                                If Trim(myTxt.Text.ToString) <> "" Then
                                    If mySetting.ValidateQuerySyntax(myTxt.Text.ToString) = True Then
                                        strSplit = mySetting.ProcessQuerySyntax(myTxt.Text.ToString)
                                        Dim intPos As Integer, arr As New ArrayList, intLeftMidRight As Integer
                                        intPos = InStr(myTxt.Text.ToString, strSplit)
                                        If intPos = 1 Then
                                            intLeftMidRight = -1
                                        ElseIf intPos > 1 And intPos < Len(myTxt.Text.ToString) Then
                                            intLeftMidRight = 0
                                        ElseIf intPos = Len(myTxt.Text.ToString) Then
                                            intLeftMidRight = 1
                                        End If
                                        mySetting.AddQuerySyntax(arr, myTxt.Text.ToString)
										If arr.Count = 0 Then
                                            arr.Add(myTxt.Text.ToString.Trim)
                                        End If
                                        For j = 0 To arr.Count - 1
                                            If Trim(arr(j).ToString) <> "" Then
                                                If Not IsNumeric(mySetting.UnDisplayTime(arr(j).ToString, Session("Company").ToString, Session("Module").ToString)) = True Then
                                                    lblresult2.Text = "Invalid Input [Date] Format For [" & myDR(1).ToString & "] With Value [" & arr(j).ToString & "]"
                                                    myTxt.Focus()
                                                    Return False
                                                Else
                                                    Select Case intLeftMidRight
                                                        Case -1
                                                            tmpssql = tmpssql & strSplit & mySetting.UnDisplayTime(arr(j).ToString, Session("Company").ToString, Session("Module").ToString)
                                                        Case 0
                                                            tmpssql = tmpssql & mySetting.UnDisplayTime(arr(j).ToString, Session("Company").ToString, Session("Module").ToString) & strSplit
                                                        Case 1
                                                            tmpssql = tmpssql & mySetting.UnDisplayTime(arr(j).ToString, Session("Company").ToString, Session("Module").ToString) & strSplit
                                                    End Select
                                                End If
                                            End If
                                        Next
                                        Select Case intLeftMidRight
                                            Case -1
                                                'Do nothing
                                            Case 0
                                                If Right(tmpssql, Len(strSplit)) = strSplit Then
                                                    tmpssql = Left(tmpssql, Len(tmpssql) - Len(strSplit))
                                                End If
                                            Case 1
                                                'Do nothing
                                        End Select
                                        ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                                        ssql2 = ssql2 & myDR(0).ToString & "@@"
                                        ssql3 = ssql3 & tmpssql & "@@"
                                        htbFilterCriteria.Add(myDR(0).ToString.Trim, tmpssql)
                                    Else
                                        lblresult2.Text = "More than 1 syntax found in the [" & myDR(1).ToString & "]"
                                        myTxt.Focus()
                                        Return False
                                    End If
                                End If
                            End If
                            myTxt = Nothing
                            tmpssql = ""
                        Case "INTEGER", "DECIMAL"
                            Dim myTxt As TextBox = Page.FindControl("txt" & myDR(0).ToString)
                            If myTxt.Visible = True And Trim(myTxt.Text) <> "" Then
                                'Change Search Numeric Start
                                'Dim arr() As String = myTxt.Text.ToString.Split("~")
                                'For j = 0 To arr.Length - 1
                                'If Trim(arr(j).ToString) <> "" Then
                                '    If Not IsNumeric(Trim(myTxt.Text.ToString)) Then
                                If mySetting.ValidateQuerySyntax(myTxt.Text) = True Then
                                    strSplit = mySetting.ProcessQuerySyntax(myTxt.Text.ToString)
                                    Dim intPos As Integer, arr As New ArrayList, intLeftMidRight As Integer
                                    intPos = InStr(myTxt.Text.ToString, strSplit)
                                    If intPos = 1 Then
                                        intLeftMidRight = -1
                                    ElseIf intPos > 1 And intPos < Len(myTxt.Text.ToString) Then
                                        intLeftMidRight = 0
                                    ElseIf intPos = Len(myTxt.Text.ToString) Then
                                        intLeftMidRight = 1
                                    End If
                                    mySetting.AddQuerySyntax(arr, myTxt.Text.ToString)
                                    If arr.Count = 0 Then
                                        arr.Add(myTxt.Text.ToString.Trim)
                                    End If

                                    For j = 0 To arr.Count - 1
                                        If Trim(arr(j).ToString) <> "" Then
                                            If Not IsNumeric(Trim(arr(j).ToString)) Then
                                                'Change Search Numeric End
                                                lblresult2.Text = "Invalid Input [Numeric] Format For " & myDR(1).ToString & " With Value " & arr(j).ToString
                                                myTxt.Focus()
                                                Return False
                                            Else
                                                tmpssql = tmpssql & arr(j).ToString & "~"
                                            End If
                                        End If
                                    Next
                                    If Right(tmpssql, 1) = "~" Then
                                        tmpssql = Left(tmpssql, Len(tmpssql) - 1)
                                    End If
                                End If
                                ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                                ssql2 = ssql2 & myDR(0).ToString & "@@"
                                ssql3 = ssql3 & Trim(myTxt.Text.ToString.Replace("'", "''")) & "@@"
                                htbFilterCriteria.Add(myDR(0).ToString.Trim, myTxt.Text.ToString.Trim.Replace("'", "''"))
                            End If
                            tmpssql = ""
                            myTxt = Nothing
                        Case "LOOKUP", "CHARACTER"
                                Dim myTxt As TextBox = Page.FindControl("txt" & myDR(0).ToString)
                                If myTxt.Visible = True And Trim(myTxt.Text) <> "" Then
                                    ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                                    ssql2 = ssql2 & myDR(0).ToString & "@@"
                                    ssql3 = ssql3 & Trim(myTxt.Text.ToString.Replace("'", "''")) & "@@"
                                    htbFilterCriteria.Add(myDR(0).ToString.Trim, myTxt.Text.ToString.Trim.Replace("'", "''"))
                                End If
                    End Select
                    myDR = Nothing
                Next
                If Len(ssql) > 2 Then
                    If Right(ssql, 2) = "@@" Then
                        ssql = Left(ssql, Len(ssql) - 2)
                    End If
                End If
                If Len(ssql2) > 2 Then
                    If Right(ssql2, 2) = "@@" Then
                        ssql2 = Left(ssql2, Len(ssql2) - 2)
                    End If
                End If
                If Len(ssql3) > 2 Then
                    If Right(ssql3, 2) = "@@" Then
                        ssql3 = Left(ssql3, Len(ssql3) - 2)
                    End If
                End If
                If ssql = "" And ssql2 = "" And ssql3 = "" Then
                    lblresult2.Text = "No search criteria was specified!"
                    SearchCriteriaFound = False
                    'Return False
                Else
                    SearchCriteriaFound = True
					ssql = "Exec sp_Generate_Query_Filter_WithSecurity N'" & Session("Company").ToString & "',N'" & Session("EmpID").ToString & "',N'" & _
                            Session("Module").ToString & "',N'" & ssql & "',N'" & ssql2 & "',N'" & ssql3 & _
                            "','" & myGridView.PageSize & "','" 
                    ssql2 = ssql.ToString.Replace("sp_Generate_Query_Filter_WithSecurity", "sp_Compare_query_filter_WithSecurity")
                    Session("SearchByFilter") = "True"
					Session("SearchSQL") = ssql
					Session("SearchSQL1") = ssql2
					
					
                    Dim strFilterField As String = "", strFilterCriteria As String = ""
                    For Each de As DictionaryEntry In htbFilterCriteria
                        strFilterField &= de.Key.ToString & "@@"
                    Next
                    For Each de As DictionaryEntry In htbFilterCriteria
                        strFilterCriteria &= de.Value.ToString & "@@"
                    Next
                    If Len(strFilterField) > 2 Then
                        If Right(strFilterField, 2) = "@@" Then
                            strFilterField = Mid(strFilterField, 1, Len(strFilterField) - 2)
                        End If
                    End If
                    If Len(strFilterCriteria) > 2 Then
                        If Right(strFilterCriteria, 2) = "@@" Then
                            strFilterCriteria = Mid(strFilterCriteria, 1, Len(strFilterCriteria) - 2)
                        End If
                    End If
                    Session("FilterField") = strFilterField
                    Session("FilterCriteria") = strFilterCriteria
                End If
            End If
        End If
        myDS = Nothing
        Return True
    End Function
    Private Function ValidateUpdate() As Boolean
        lblresult2.Visible = True
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL("Select Code,Name,Option_Data_Type,Option_Mandatory From Table_Field Where Table_Profile_Code='" & Form.ID & "' And Option_Primary_Key='NO' And Option_View_Card='YES' Order By Table_Profile_Code,Sequence_No")
        ssql = "Update " & Form.ID & " Set "
        If myDS.Tables(0).Rows.Count > 0 Then
            For i = 0 To myDS.Tables(0).Rows.Count - 1
                Select Case myDS.Tables(0).Rows(i).Item(2).ToString
                    Case "OPTION"
                        Dim myDropdownlist As DropDownList = Page.FindControl("ddl" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                        If myDropdownlist.Visible = True Then
                            If mySetting.CheckTextNull(myDropdownlist.SelectedValue) = True Then
                                If mySetting.ValidateInput(myDropdownlist, myDS.Tables(0).Rows(i).Item(1).ToString) = True Then
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=N'" & Trim(myDropdownlist.SelectedValue) & "',"
                                Else
                                    lblresult2.Text = "Invalid Selection For " & Trim(myDS.Tables(0).Rows(i).Item(0)) & " !"
                                    myDropdownlist.Focus()
                                    Return False
                                End If
                            Else
                                If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" Then
                                    lblresult2.Text = "[" & Trim(myDS.Tables(0).Rows(i).Item(1)) & "] Is A Required Field!"
                                    myDropdownlist.Focus()
                                    Return False
                                Else
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=N'" & Trim(myDropdownlist.SelectedValue) & "',"
                                End If
                            End If
                        End If
                        myDropdownlist = Nothing
                    Case "DATE"
                        Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                        If myTextBox.Visible = True Then
                            If mySetting.CheckTextNullAndReplaceSingleQuote(myTextBox) = True Then
                                If IsNumeric(mySetting.ConvertDateToDecimal(myTextBox.Text, Session("Company").ToString, Session("Module").ToString)) = True Then
                                    Dim myDST As New DataSet
                                    myDST = mySQL.ExecuteSQL("Select Function_Name,Default_Value,Default_Value2 From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='UPDATE'")
                                    If myDST.Tables(0).Rows.Count > 0 Then
                                        If myDST.Tables(0).Rows(0).Item(1).ToString.Trim = "" Then
                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & mySetting.ConvertDateToDecimal(myTextBox.Text, Session("Company").ToString, Session("Module").ToString) & "'),'0'),"
                                        Else
                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(" & myDST.Tables(0).Rows(0).Item(1).ToString.Trim & "),'0'),"
                                        End If
                                    Else
                                        ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "='" & mySetting.ConvertDateToDecimal(myTextBox.Text, Session("Company").ToString, Session("Module").ToString) & "',"
                                    End If
                                    myDST = Nothing
                                Else
                                    lblresult2.Text = "Invalid Selection For " & Trim(myDS.Tables(0).Rows(i).Item(0)) & " !"
                                    myTextBox.Focus()
                                    Return False
                                End If
                            Else
                                If myDS.Tables(0).Rows(i).Item(2).ToString = "YES" Then
                                    lblresult2.Text = Trim(myDS.Tables(0).Rows(i).Item(0)) & " Is A Required Field!"
                                    myTextBox.Focus()
                                    Return False
                                Else
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "='" & myTextBox.Text.ToString.Trim & "',"
                                End If
                            End If
                        End If
                        myTextBox = Nothing
                    Case "DATETIME"
                        Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                        If myTextBox.Visible = True Then
                            If mySetting.CheckTextNullAndReplaceSingleQuote(myTextBox) = True Then
                                If IsNumeric(mySetting.UnDisplayDateTime(myTextBox.Text, Session("Company").ToString, Session("Module").ToString)) = True Then
                                    Dim myDST As New DataSet
                                    myDST = mySQL.ExecuteSQL("Select Function_Name,Default_Value,Default_Value2 From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='UPDATE'")
                                    If myDST.Tables(0).Rows.Count > 0 Then
                                        If myDST.Tables(0).Rows(0).Item(1).ToString.Trim = "" Then
                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & mySetting.UnDisplayDateTime(myTextBox.Text, Session("Company").ToString, Session("Module").ToString) & "'),'0'),"
                                        Else
                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(" & myDST.Tables(0).Rows(0).Item(1).ToString.Trim & "),'0'),"
                                        End If
                                    Else
                                        ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "='" & mySetting.UnDisplayDateTime(myTextBox.Text, Session("Company").ToString, Session("Module").ToString) & "',"
                                    End If
                                    myDST = Nothing
                                Else
                                    lblresult2.Text = "Invalid Selection For " & Trim(myDS.Tables(0).Rows(i).Item(0)) & " !"
                                    myTextBox.Focus()
                                    Return False
                                End If
                            Else
                                If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" Then
                                    lblresult2.Text = "[" & Trim(myDS.Tables(0).Rows(i).Item(1)) & "] Is A Required Field!"
                                    myTextBox.Focus()
                                    Return False
                                Else
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "='" & myTextBox.Text.ToString.Trim & "',"
                                End If
                            End If
                        End If
                        myTextBox = Nothing
                    Case "TIME"
                        Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                        If myTextBox.Visible = True Then
                            If mySetting.CheckTextNullAndReplaceSingleQuote(myTextBox) = True Then
                                If IsNumeric(mySetting.UnDisplayTime(myTextBox.Text, Session("Company").ToString, Session("Module").ToString)) = True Then
                                    Dim myDST As New DataSet
                                    myDST = mySQL.ExecuteSQL("Select Function_Name,Default_Value,Default_Value2 From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='UPDATE'")
                                    If myDST.Tables(0).Rows.Count > 0 Then
                                        If myDST.Tables(0).Rows(0).Item(1).ToString.Trim = "" Then
                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & mySetting.UnDisplayTime(myTextBox.Text, Session("Company").ToString, Session("Module").ToString) & "'),'0'),"
                                        Else
                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(" & myDST.Tables(0).Rows(0).Item(1).ToString.Trim & "),'0'),"
                                        End If
                                    Else
                                        ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "='" & mySetting.UnDisplayTime(myTextBox.Text, Session("Company").ToString, Session("Module").ToString) & "',"
                                    End If
                                    myDST = Nothing
                                Else
                                    lblresult2.Text = "Invalid Selection For " & Trim(myDS.Tables(0).Rows(i).Item(0)) & " !"
                                    myTextBox.Focus()
                                    Return False
                                End If
                            Else
                                If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" Then
                                    lblresult2.Text = "[" & Trim(myDS.Tables(0).Rows(i).Item(1)) & "] Is A Required Field!"
                                    myTextBox.Focus()
                                    Return False
                                Else
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "='" & myTextBox.Text.ToString.Trim & "',"
                                End If
                            End If
                        End If
                        myTextBox = Nothing
                    Case "DECIMAL", "INTEGER"
                        Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                        If myTextBox.Visible = True Then
                            If mySetting.CheckTextNullAndReplaceSingleQuote(myTextBox) = True Then
                                If IsNumeric(myTextBox.Text) Then
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=N'" & myTextBox.Text.ToString & "',"
                                Else
                                    lblresult2.Text = "Invalid Input Format For " & myDS.Tables(0).Rows(i).Item(1).ToString
                                    Return False
                                End If
                            Else
                                If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" Then
                                    lblresult2.Text = "[" & Trim(myDS.Tables(0).Rows(i).Item(1)) & "] Is A Required Field!"
                                    myTextBox.Focus()
                                    Return False
                                Else
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "='0',"
                                End If
                            End If
                        End If
                        myTextBox = Nothing
                    Case Else '"CHARACTER","LOOKUP"
                        Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                        If myTextBox.Visible = True Then
                            If mySetting.CheckTextNullAndReplaceSingleQuote(myTextBox) = True Then
                                If myDS.Tables(0).Rows(i).Item(0).ToString.ToUpper = "EMPLOYEE_PROFILE_ID" Then
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(dbo.fn_ReturnEmpIDByCodeName(N'" & Session("Company").ToString & "',N'" & myTextBox.Text.ToString & "'),''),"
                                Else
                                    Dim myDST As New DataSet
                                    myDST = mySQL.ExecuteSQL("Select Function_Name,Default_Value,Default_Value2 From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='UPDATE'")
                                    If myDST.Tables(0).Rows.Count > 0 Then
                                        If myDST.Tables(0).Rows(0).Item(1).ToString.Trim = "" Then
                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "'),''),"
                                        Else
                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString.Trim & "'),''),"
                                        End If
                                    Else
                                        ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(N'" & myTextBox.Text.ToString & "',''),"
                                    End If
                                    myDST = Nothing
                                End If
                            Else
                                If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" Then
                                    lblresult2.Text = "[" & Trim(myDS.Tables(0).Rows(i).Item(1)) & "] Is A Required Field!"
                                    myTextBox.Focus()
                                    Return False
                                Else
                                    ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(N'" & myTextBox.Text.ToString.Trim & "',''),"
                                End If
                            End If
                        End If
                        myTextBox = Nothing
                End Select
            Next
            Dim myTXT As TextBox
            myTXT = Me.FindControl("txtUSER_PROFILE_CODE_CREATE")
            If Not myTXT Is Nothing Then '
                ssql = ssql & "USER_PROFILE_CODE_MODIFY=N'" & Session("EmpID").ToString & "',DATETIME_MODIFY=dbo.fn_GetCurrentDateTime(GetDate()),"
            End If
            ssql = Left(ssql, Len(ssql) - 1) & " Where "
            myDS = Nothing

            myDS2 = New DataSet
            myDS2 = mySQL.ExecuteSQL("Select Code,Name,Option_Data_Type,Option_Mandatory From Table_Field Where Table_Profile_Code='" & Form.ID & "' And Option_Primary_Key='YES' And Option_View_Card='YES' Order By Table_Profile_Code,Sequence_No")
            If myDS2.Tables(0).Rows.Count > 0 Then
                For i = 0 To myDS2.Tables(0).Rows.Count - 1
                    Select Case myDS2.Tables(0).Rows(i).Item(2).ToString
                        Case "OPTION"
                            Dim myDropdownlist As DropDownList = Page.FindControl("ddl" & Trim(myDS2.Tables(0).Rows(i).Item(0)))
                            If myDropdownlist.Visible = True Then
                                If mySetting.CheckTextNull(myDropdownlist.SelectedValue) = True Then
                                    If mySetting.ValidateInput(myDropdownlist, myDS2.Tables(0).Rows(i).Item(1).ToString) = True Then
                                        ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "='" & myDropdownlist.SelectedValue & "' And "
                                    Else
                                        lblresult2.Text = "Invalid Selection For " & Trim(myDS2.Tables(0).Rows(i).Item(0)) & " !"
                                        myDropdownlist.Focus()
                                        Return False
                                    End If
                                Else
                                    lblresult2.Text = Trim(myDS2.Tables(0).Rows(i).Item(0)) & " Is A Required Field!"
                                    myDropdownlist.Focus()
                                    Return False
                                End If
                            End If
                            myDropdownlist = Nothing
                        Case "DATE"
                            Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS2.Tables(0).Rows(i).Item(0)))
                            If myTextBox.Visible = True Then
                                If mySetting.CheckTextNullAndReplaceSingleQuote(myTextBox) = True Then
                                    If IsNumeric(mySetting.ConvertDateToDecimal(myTextBox.Text, Session("Company").ToString, Session("Module").ToString)) = True Then
                                        Dim myDST2 As New DataSet
                                        myDST2 = mySQL.ExecuteSQL("Select Function_Name,Default_Value,Default_Value2 From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS2.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='UPDATE'")
                                        If myDST2.Tables(0).Rows.Count > 0 Then
                                            If myDST2.Tables(0).Rows(0).Item(1).ToString.Trim = "" Then
                                                ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST2.Tables(1).Rows(0).Item(0).ToString & "(N'" & mySetting.ConvertDateToDecimal(myTextBox.Text, Session("Company").ToString, Session("Module").ToString) & "'),'') And "
                                            Else
                                                ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST2.Tables(1).Rows(0).Item(0).ToString & "(" & myDST2.Tables(0).Rows(0).Item(1).ToString.Trim & "),'') And "
                                            End If
                                        Else
                                            ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "='" & mySetting.ConvertDateToDecimal(myTextBox.Text, Session("Company").ToString, Session("Module").ToString) & "' And "
                                        End If
                                        myDST2 = Nothing
                                    Else
                                        lblresult2.Text = "Invalid Selection For " & Trim(myDS2.Tables(0).Rows(i).Item(0)) & " !"
                                        myTextBox.Focus()
                                        Return False
                                    End If
                                Else
                                    lblresult2.Text = Trim(myDS2.Tables(0).Rows(i).Item(0)) & " Is A Required Field!"
                                    myTextBox.Focus()
                                    Return False
                                End If
                            End If
                            myTextBox = Nothing
                        Case "DATETIME"
                            Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS2.Tables(0).Rows(i).Item(0)))
                            If myTextBox.Visible = True Then
                                If mySetting.CheckTextNullAndReplaceSingleQuote(myTextBox) = True Then
                                    If IsNumeric(mySetting.UnDisplayDateTime(myTextBox.Text, Session("Company").ToString, Session("Module").ToString)) = True Then
                                        Dim myDST2 As New DataSet
                                        myDST2 = mySQL.ExecuteSQL("Select Function_Name,Default_Value,Default_Value2 From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS2.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='UPDATE'")
                                        If myDST2.Tables(0).Rows.Count > 0 Then
                                            If myDST2.Tables(0).Rows(0).Item(1).ToString.Trim = "" Then
                                                ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST2.Tables(1).Rows(0).Item(0).ToString & "(N'" & mySetting.UnDisplayDateTime(myTextBox.Text, Session("Company").ToString, Session("Module").ToString) & "'),'') And "
                                            Else
                                                ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST2.Tables(1).Rows(0).Item(0).ToString & "(" & myDST2.Tables(0).Rows(0).Item(1).ToString.Trim & "),'') And "
                                            End If
                                        Else
                                            ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "='" & mySetting.UnDisplayDateTime(myTextBox.Text, Session("Company").ToString, Session("Module").ToString) & "' And "
                                        End If
                                        myDST2 = Nothing
                                    Else
                                        lblresult2.Text = "Invalid Selection For " & Trim(myDS2.Tables(0).Rows(i).Item(0)) & " !"
                                        myTextBox.Focus()
                                        Return False
                                    End If
                                Else
                                    lblresult2.Text = Trim(myDS2.Tables(0).Rows(i).Item(0)) & " Is A Required Field!"
                                    myTextBox.Focus()
                                    Return False
                                End If
                            End If
                            myTextBox = Nothing
                        Case "TIME"
                            Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS2.Tables(0).Rows(i).Item(0)))
                            If myTextBox.Visible = True Then
                                If mySetting.CheckTextNullAndReplaceSingleQuote(myTextBox) = True Then
                                    If IsNumeric(mySetting.UnDisplayTime(myTextBox.Text, Session("Company").ToString, Session("Module").ToString)) = True Then
                                        Dim myDST2 As New DataSet
                                        myDST2 = mySQL.ExecuteSQL("Select Function_Name,Default_Value,Default_Value2 From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS2.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='UPDATE'")
                                        If myDST2.Tables(0).Rows.Count > 0 Then
                                            If myDST2.Tables(0).Rows(0).Item(1).ToString.Trim = "" Then
                                                ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST2.Tables(1).Rows(0).Item(0).ToString & "(N'" & mySetting.UnDisplayTime(myTextBox.Text, Session("Company").ToString, Session("Module").ToString) & "'),'') And "
                                            Else
                                                ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST2.Tables(1).Rows(0).Item(0).ToString & "(" & myDST2.Tables(0).Rows(0).Item(1).ToString.Trim & "),'') And "
                                            End If
                                        Else
                                            ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "='" & mySetting.UnDisplayTime(myTextBox.Text, Session("Company").ToString, Session("Module").ToString) & "' And "
                                        End If
                                        myDST2 = Nothing
                                    Else
                                        lblresult2.Text = "Invalid Selection For " & Trim(myDS2.Tables(0).Rows(i).Item(0)) & " !"
                                        myTextBox.Focus()
                                        Return False
                                    End If
                                Else
                                    lblresult2.Text = Trim(myDS2.Tables(0).Rows(i).Item(0)) & " Is A Required Field!"
                                    myTextBox.Focus()
                                    Return False
                                End If
                            End If
                            myTextBox = Nothing
                        Case "DECIMAL", "INTEGER"
                            Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS2.Tables(0).Rows(i).Item(0)))
                            If IsNumeric(myTextBox.Text.ToString) Then
                                ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "=N'" & myTextBox.Text.ToString & "' And "
                            Else
                                lblresult2.Text = "Invalid Input Format For " & myDS2.Tables(0).Rows(i).Item(1).ToString
                                Return False
                            End If
                            myTextBox = Nothing
                        Case Else '"CHARACTER","LOOKUP"
                            Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS2.Tables(0).Rows(i).Item(0)))
                            If myTextBox.Visible = True Then
                                If mySetting.CheckTextNullAndReplaceSingleQuote(myTextBox) = True Then
                                    If myDS2.Tables(0).Rows(i).Item(0).ToString.ToUpper = "EMPLOYEE_PROFILE_ID" Then
                                        ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "=ISNULL(dbo.fn_ReturnEmpIDByCodeName(N'" & Session("Company").ToString & "',N'" & myTextBox.Text.ToString & "'),'') And "
                                    Else
                                        Dim myDST2 As New DataSet
                                        myDST2 = mySQL.ExecuteSQL("Select Function_Name,Default_Value,Default_Value2 From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS2.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='UPDATE'")
                                        If myDST2.Tables(0).Rows.Count > 0 Then
                                            If myDST2.Tables(0).Rows(0).Item(1).ToString.Trim = "" Then
                                                ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST2.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "'),'') And "
                                            Else
                                                ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST2.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "',N'" & myDST2.Tables(0).Rows(0).Item(1).ToString.Trim & "'),'') And "
                                            End If
                                        Else
                                            ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "=ISNULL(N'" & myTextBox.Text.ToString & "','') And "
                                        End If
                                        myDST2 = Nothing
                                    End If
                                Else
                                    lblresult2.Text = Trim(myDS2.Tables(0).Rows(i).Item(0)) & " Is A Required Field!"
                                    myTextBox.Focus()
                                    Return False
                                End If
                            End If
                            myTextBox = Nothing
                    End Select
                Next
            End If
            ssql = Left(ssql, Len(ssql) - 5)
            myDS2 = Nothing
        End If
        Return True
    End Function
    Private Function ReturnSessionValue(ByVal strRequest As String) As String
        Dim strOutput As String = ""
        Select Case strRequest.ToUpper
            Case "COMPANY"
                strOutput = Session("Company").ToString
            Case "EMPLOYEE_PROFILE_ID"
                strOutput = Session("EmpID").ToString
            Case "MODULE_PROFILE_CODE"
                strOutput = Session("Module").ToString
        End Select
        Return strOutput
    End Function
#End Region
    Protected Sub imgBtnPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnPrint.Click
        Dim clsRpt As New clsReportServices, myTargetURL As String = ""
        myTargetURL = clsRpt.GetReportURL(Session("Company").ToString, Session("EmpID").ToString, _
                    Session("Module").ToString, Form.ID.ToString, Session("Language").ToString, _
                     Session("FilterField").ToString, Session("FilterCriteria").ToString, _
                      myGridView.PageSize, 1)
        'Page.ClientScript.RegisterStartupScript(GetType(Page), "ShowReport", myTargetURL)
        Response.Write("<script>window.open('" & myTargetURL & "');</script>")
    End Sub
End Class