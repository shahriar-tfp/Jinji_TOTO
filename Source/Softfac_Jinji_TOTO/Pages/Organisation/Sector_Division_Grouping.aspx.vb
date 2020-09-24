Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.web.Configuration

Partial Class Pages_Organisation_Sector_Division
    Inherits System.Web.UI.Page

#Region "Public Declaration"
    Private WithEvents mySQL As New clsSQL, myDS, myDS1, myDS2, myDS3, myDS4, myDS5, myDST, myDST2 As New DataSet, mySetting As New clsGlobalSetting, myMsg As New clsMessage
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Public ssql As String, i, j, k As Integer, SearchByPage As Boolean, RecFound As Boolean, CountRecord As Integer
    Public AllowView, AllowInsert, AllowUpdate, AllowDelete, AllowPrint As Boolean
    Public _currentPageNumber As Integer, _totalPage As Double = 1, _totalRecords As Integer
    Public myDE As New DictionaryEntry, myHT As Hashtable
    Public myDataType As New clsGlobalSetting.DataType
#End Region

#Region "Private Declaration"
    Private WithEvents myDT1 As New DataTable, myDT2 As New DataTable
    Private WithEvents myDSDefault, myDSInsert, myDSUpdate, myDSDelete As New DataSet
    Dim myDR1 As DataRow, myDR2 As DataRow, rcPerPage As Integer, autonum As Integer
    Dim ssql1, ssql2, ssql3, ssql4, ssql5, ssql6, ssql7, ssql8, ssql9, ssql10 As String
    Dim rowPosition As String = "R_", rowPositionM As String = "RM_", panelPosition As String = "Display_"
    Dim buttonPosition1 As String = "B1_", buttonPosition2 As String = "B2_", buttonPosition3 As String = "B3_", labelPosition1 As String = "L1_"
    Dim logic As Boolean
    Private _Ascending As Boolean = True
    Dim strPath As String = "../../Images"
    Dim btnColourDef, btnColourAlt As String
#End Region

#Region "Page Setting"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Session.IsNewSession Then
            Response.Redirect("../Global/SessionTimeOut.aspx")
        End If

        If Not Page.IsPostBack Then
            Response.CacheControl = "no-cache"
            Response.AddHeader("Pragma", "no-cache")
            Response.Expires = -1

            If Session("ScreenWidth") = 0 Then
                Session("ScreenWidth") = "1024"
                Session("GVwidth") = Session("ScreenWidth") - 360
            End If
            If Session("ScreenHeight") = 0 Then
                Session("ScreenHeight") = "768"
                Session("GVheight") = (Session("ScreenHeight") / 2) - 50
            End If
            pnlgridview.Width = CInt(Session("GVwidth"))
            pnlgridview.Height = CInt(Session("GVheight"))
            PagePreload()
            BindGrid()
        Else
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
        Session("Module") = "Organisation"
        Session("FilterField") = ""
        Session("FilterCriteria") = ""
        Session("action") = ""
        pnledit.Visible = False
        SearchByPage = False
        _currentPageNumber = 1
        Session("currentpage") = _currentPageNumber
        FirstPage.Enabled = False
        PrevPage.Enabled = False
        NextPage.Enabled = False
        LastPage.Enabled = False

        'get Page Title
        ssql = "exec sp_sa_GetPageTitle '" & Form.ID & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            lblTitle.Text = myDS.Tables(0).Rows(0).Item(0)
            lblTitle.CssClass = "wordstyle4"
            lblTitle2.Text = myDS.Tables(0).Rows(0).Item(0)
            lblTitle2.CssClass = "wordstyle4"
        End If
        myDS = Nothing

        'label for list box
        lbllstleft.Text = "Available List"
        lbllstright.Text = "Selected List"
        lbllstleft.CssClass = "wordstyle5"
        lbllstright.CssClass = "wordstyle6"

        mySetting.GetBtnImgUrl(imgBtnSubmit, Session("Company").ToString, btnColourDef, "btnSubmit.png")
        mySetting.GetBtnImgUrl(imgBtnSearch, Session("Company").ToString, btnColourDef, "btnSearch.png")
        mySetting.GetBtnImgUrl(imgBtnClear, Session("Company").ToString, btnColourDef, "btnClear.png")
        mySetting.GetBtnImgUrl(imgBtnCancel, Session("Company").ToString, btnColourDef, "btnCancel.png")
        mySetting.GetBtnImgUrl(imgBtnAdd, Session("Company").ToString, btnColourDef, "btnAdd.png")
        mySetting.GetBtnImgUrl(imgBtnFilter, Session("Company").ToString, btnColourDef, "btnFilter.png")
        'mySetting.GetBtnImgUrl(imgBtnDelete, Session("Company").ToString, btnColourDef, "btnDelete.png")
        'mySetting.GetBtnImgUrl(imgBtnUpdate, Session("Company").ToString, btnColourDef, "btnUpdate.png")
        mySetting.GetBtnImgUrl(imgBtnEdit, Session("Company").ToString, btnColourDef, "btnEdit.png")
        mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourDef, "btnPrint.png")
        mySetting.GetBtnImgUrl(imgBtnGoToPage, Session("Company").ToString, btnColourDef, "btngo.png")

        mySetting.GetBtnImgUrl(imgBtnAddAll, Session("Company").ToString, btnColourDef, "addall.png")
        mySetting.GetBtnImgUrl(imgBtnAddItem, Session("Company").ToString, btnColourDef, "additem.png")
        mySetting.GetBtnImgUrl(imgBtnRemoveAll, Session("Company").ToString, btnColourDef, "removeall.png")
        mySetting.GetBtnImgUrl(imgBtnRemoveItem, Session("Company").ToString, btnColourDef, "removeitem.png")
        mySetting.GetImgBtnUrl(imgBtnOCP_ID_DIVISION2, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.SetTextBoxPressEnterGoToImageButton(txtGoToPage, imgBtnGoToPage)
        body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

        'lookup field setting
        ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_SECTOR" & """," & """" & """," & """" & Session("EmpID").ToString & """"
        mySetting.GetLookupValue_ImageButton(imgBtnOCP_ID_Sector, Form.ID, "txtOCP_ID_SECTOR", "CodeName", ssql)

        myDS = mySetting.GetPageFieldSetting(Session("Company"), Form.ID, Session("EmpID"))
        If myDS.Tables.Count > 1 Then
            myDT1 = myDS.Tables(0)
            myDT2 = myDS.Tables(1)

            If myDT1.Rows.Count > 0 Then
                myDR1 = myDT1.Rows(0)

                Page.Title = myDR1(1)
                If myDR1(3) = "YES" Then
                    AllowView = True
                Else
                    AllowView = False
                    pnlaction.Visible = False
                    pnledit.Visible = False
                    pnlgridview.Visible = False
                    pnlmain.Visible = False
                    pnlprevnext.Visible = False
                    pnlresult.Visible = False
                    ShowMessage("You are not allow to view this page!")
                    Exit Sub
                End If

                If myDR1(4) = "YES" Then AllowInsert = True Else AllowInsert = False
                If myDR1(5) = "YES" Then AllowUpdate = True Else AllowUpdate = False
                If myDR1(6) = "YES" Then AllowDelete = True Else AllowDelete = False
                If myDR1(7) = "YES" Then AllowPrint = True Else AllowPrint = False

                Session("AllowView") = AllowView
                Session("AllowInsert") = AllowInsert
                Session("AllowUpdate") = AllowUpdate
                Session("AllowDelete") = AllowDelete
                Session("AllowPrint") = AllowPrint

                imgBtnAdd.Visible = AllowInsert
                imgBtnSubmit.Visible = AllowInsert
                imgBtnEdit.Visible = AllowUpdate
                'imgBtnUpdate.Visible = AllowUpdate
                'imgBtnDelete.Visible = AllowDelete
                imgBtnSearch.Visible = AllowView
                imgBtnFilter.Visible = AllowView
                imgBtnClear.Visible = AllowView
                imgBtnCancel.Visible = AllowView
                imgBtnPrint.Visible = AllowPrint

                'Get GV page per record
                If myDR1(8) <= 0 Then
                    ssql = "exec sp_sa_getParameter '" & Session("Company") & "', '" & Session("Module") & "', 'NO_OF_RECORD'"
                    myDS2 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                    If myDS2.Tables(0).Rows.Count > 0 Then
                        Session("rcPerPage") = myDS2.Tables(0).Rows(0).Item(9)
                    End If
                    myDS2 = Nothing
                Else
                    Session("rcPerPage") = myDR1(8)
                End If
                myGridView.PageSize = Session("rcPerPage")
            Else
                lblresult.Text = "[Page Setting Error]: No setting found for this page!"
                Exit Sub
            End If

            If myDT2.Rows.Count > 0 Then

                For i = 0 To myDT2.Rows.Count - 1
                    Dim myLabel As Label = Page.FindControl("lbl" & myDT2.Rows(i).Item(2).ToString)
                    Dim myImageButton As ImageButton = Page.FindControl("imgBtn" & myDT2.Rows(i).Item(2).ToString)
                    Dim myImageKey As Image = Page.FindControl("img" & myDT2.Rows(i).Item(2).ToString)

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
                    Else
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
                        Select Case myDT2.Rows(i).Item(6).ToString
                            Case "OPTION"
                                Dim myDDL As DropDownList = Page.FindControl("ddl" & myDT2.Rows(i).Item(2).ToString)
                                mySetting.GetDropdownlistValue(Form.ID, myDT2.Rows(i).Item(2).ToString, myDDL)
                                myDDL = Nothing
                            Case "LOOKUP"
                                '    mySetting.GetLookupValue_ImageButton(myImageButton, Form.ID, "txt" & myDT2.Rows(i).Item(2).ToString, "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & myDT2.Rows(i).Item(2).ToString & """," & """" & """")
                                '    Dim myTextBox As TextBox = Page.FindControl("txt" & myDT2.Rows(i).Item(2).ToString)
                                '    myTextBox.MaxLength = CInt(myDT2.Rows(i).Item(7).ToString)
                                '    myTextBox = Nothing
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
                    End If
                    myLabel = Nothing
                    myImageKey = Nothing
                    myImageButton = Nothing
                Next

                'Page Editable Setting
                Dim priNumCount As String = ""
                Dim edtNumCount As String = ""
                Dim compareNum1 As String = ""
                Dim compareNum0 As String = ""
                For i = 0 To myDT2.Rows.Count - 1
                    If myDT2.Rows(i).Item(4) = "YES" Then
                        priNumCount = priNumCount & 1
                    Else
                        priNumCount = priNumCount & 0
                    End If
                    If myDT2.Rows(i).Item(11) = "YES" Then
                        edtNumCount = edtNumCount & 1
                    Else
                        edtNumCount = edtNumCount & 0
                    End If
                Next
                For i = 1 To Len(priNumCount)
                    compareNum1 = compareNum1 & 1
                Next
                For i = 1 To Len(edtNumCount)
                    compareNum0 = compareNum0 & 0
                Next
                If priNumCount = compareNum1 Or edtNumCount = compareNum0 Then
                    Session("pageNotEditable") = "YES"
                Else
                    Session("pageNotEditable") = "NO"
                End If

                'Page Filterable Setting
                priNumCount = ""
                Dim compareNum As String = ""
                For i = 0 To myDT2.Rows.Count - 1
                    If myDT2.Rows(i).Item(13) = "NO" Then
                        priNumCount = priNumCount & 1
                    Else
                        priNumCount = priNumCount & 0
                    End If
                Next
                For i = 1 To Len(priNumCount)
                    compareNum = compareNum & 1
                Next
                If priNumCount = compareNum Then
                    Session("pageNotFilterable") = "YES"
                    imgBtnFilter.Visible = False
                Else
                    Session("pageNotFilterable") = "NO"
                    imgBtnFilter.Visible = True
                End If
            End If
            myDS = Nothing
            myDT1 = Nothing
            myDT2 = Nothing
        Else
            lblresult.Text = "[Field Setting Error]: No setting found for this page!"
            Exit Sub
        End If

    End Sub

    Sub BindGrid()

        Try
            If Session("action") = "search" Then
                ssql = "exec sp_Generate_Query_Filter_WithSecurity '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module") & "','" & Session("ssql1") & "', '" & Session("ssql2") & "' ,'" & Session("ssql3") & "','" & myGridView.PageSize & "','" & Session("currentpage") & "'"
                ssql1 = "exec sp_Compare_Query_Filter_WithSecurity '" & Session("Company") & "','" & Session("EmpID").ToString & "','" & Session("Module") & "','" & Session("ssql1") & "', '" & Session("ssql2") & "' ,'" & Session("ssql3") & "','" & myGridView.PageSize & "','" & Session("currentpage") & "'"
            Else
                ssql = "Exec sp_sa_GetTableRecordsWithSecurity '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','" & "Sector_Division_Grouping_Vw" & "','" & myGridView.PageSize & "','" & Session("currentpage") & "'"
                ssql1 = "exec sp_sa_compareTableRecords '" & Session("Company") & "','" & Session("EmpID") & "','" & Session("Module") & "','" & "Sector_Division_Grouping_Vw" & "','" & myGridView.PageSize & "','" & Session("currentpage") & "'"
            End If

            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            myDS1 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
            pnldescription.Visible = False
            pnllstleft.Visible = False
            pnllstright.Visible = False
            btnColourDef = Session("strTheme")
            btnColourAlt = Session("strThemeAlt")

            If myDS.Tables(1).Rows.Count = 0 Then
                If Session("action") = "search" Then
                    pnledit.Visible = True
                    pnlmain.Visible = False
                    pnldescription.Visible = True
                    lblresult2.Visible = True
                    lblresult2.Text = "No Data Found..."
                Else
                    pnledit.Visible = False
                    pnlmain.Visible = True
                    pnldescription.Visible = False
                    'imgBtnDelete.Enabled = False
                    imgBtnFilter.Enabled = False
                    imgBtnPrint.Enabled = False
                    imgBtnGoToPage.Enabled = False
                    imgBtnEdit.Enabled = False
                    mySetting.GetBtnImgUrl(imgBtnEdit, Session("Company").ToString, btnColourAlt, "btnEdit.png")
                    'mySetting.GetBtnImgUrl(imgBtnDelete, Session("Company").ToString, btnColourAlt, "btnDelete.png")
                    mySetting.GetBtnImgUrl(imgBtnFilter, Session("Company").ToString, btnColourAlt, "btnFilter.png")
                    mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourAlt, "btnPrint.png")
                    mySetting.GetBtnImgUrl(imgBtnGoToPage, Session("Company").ToString, btnColourAlt, "btnGo.png")
                    CurrentPage.Text = myDS.Tables(0).Rows(0).Item(0)
                    TotalPages.Text = myDS.Tables(0).Rows(0).Item(0)
                    lbltotal.Text = " ( " & myDS.Tables(0).Rows(0).Item(0) & " record(s) ) "
                    Session("currentPage") = "0"
                    myGridView.DataSource = myDS.Tables(1)
                    myGridView.DataBind()
                End If
                Exit Sub
            Else
                pnledit.Visible = False
                pnlmain.Visible = True
                pnldescription.Visible = False
                imgBtnAdd.Enabled = True
                'imgBtnDelete.Enabled = True
                imgBtnFilter.Enabled = True
                imgBtnPrint.Enabled = True
                imgBtnGoToPage.Enabled = True
                imgBtnEdit.Enabled = True
                mySetting.GetBtnImgUrl(imgBtnAdd, Session("Company").ToString, btnColourDef, "btnAdd.png")
                mySetting.GetBtnImgUrl(imgBtnEdit, Session("Company").ToString, btnColourDef, "btnEdit.png")
                'mySetting.GetBtnImgUrl(imgBtnDelete, Session("Company").ToString, btnColourDef, "btnDelete.png")
                mySetting.GetBtnImgUrl(imgBtnFilter, Session("Company").ToString, btnColourDef, "btnFilter.png")
                mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourDef, "btnPrint.png")
                mySetting.GetBtnImgUrl(imgBtnGoToPage, Session("Company").ToString, btnColourDef, "btnGo.png")
            End If

            If myDS.Tables.Count > 1 And CInt(myDS.Tables(0).Rows(0).Item(0)) > 0 Then
                myGridView.DataSource = myDS.Tables(1)
                myGridView.DataBind()
                myGridView1.DataSource = myDS1.Tables(1)
                myGridView1.DataBind()

                'GridView Column Width Setting
                logic = False
                myDS1 = mySetting.GetGridViewWidth(Form.ID)
                myDT1 = myDS1.Tables(0)
                myDT2 = myDS1.Tables(1)
                Dim value As Decimal
                If myDT2.Rows.Count > 0 Then

                    If CInt(myDT1.Rows(0).Item(1).ToString) < CInt(Session("GVwidth")) Then
                        If CInt(myDT1.Rows(0).Item(1).ToString) = 0 Then
                            logic = True
                        Else
                            value = CInt(Session("GVwidth")) / CInt(myDT1.Rows(0).Item(1).ToString)
                        End If
                    Else
                        value = 1
                        myGridView.Width = CInt(myDT1.Rows(0).Item(1).ToString)
                    End If
                    If logic = False Then
                        For i = 0 To myDT2.Rows.Count - 1
                            j = CInt(myDT2.Rows(i).Item(0).ToString)
                            If CInt(myDT2.Rows(i).Item(2).ToString) = 0 Then
                                myGridView.Rows(0).Cells(j).Width = 50 * value
                            Else
                                myGridView.Rows(0).Cells(j).Width = CInt(myDT2.Rows(i).Item(2).ToString) * value
                            End If
                        Next
                    End If
                End If
                myDS1 = Nothing
                myDT1 = Nothing
                myDT2 = Nothing

                lbltotal.Text = " ( " & myDS.Tables(0).Rows(0).Item(0) & " record(s) ) "
                CurrentPage.Text = Session("currentPage")

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
                    Session("TotalPages") = TotalPages.Text
                End If

                If Session("currentpage") = 1 Then
                    FirstPage.Enabled = False
                    PrevPage.Enabled = False
                    If _totalRecords > myGridView.PageSize Then
                        NextPage.Enabled = True
                        LastPage.Enabled = True
                    Else
                        NextPage.Enabled = False
                        LastPage.Enabled = False
                    End If
                ElseIf Session("currentpage") > 1 Then
                    FirstPage.Enabled = True
                    PrevPage.Enabled = True
                    If Session("currentpage") = Session("TotalPages") Then
                        NextPage.Enabled = False
                        LastPage.Enabled = False
                    Else
                        NextPage.Enabled = True
                        LastPage.Enabled = True
                    End If
                End If
                'If Session("currentpage") + 1 = Session("TotalPages") Then
                '    NextPage.Enabled = False
                'End If
                'If Session("currentpage") - 1 = 1 Then
                '    PrevPage.Enabled = False
                'End If
                myDS = Nothing
            End If
        Catch ex As Exception
            myDS = Nothing
            myDS1 = Nothing
            pnlresult.Visible = True
            lblresult.Text = "[BindGrid]Error: " & ex.Message
            pnlgridview.Visible = False
            pnlprevnext.Visible = False
            pnlaction.Visible = False
            Exit Sub
        End Try
    End Sub

#End Region

#Region "Panel Edit"

    Protected Sub imgBtnSubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSubmit.Click

        If ValidateInsertUpdate() = True Then
            lblresult.Text = "Data add/edit successfully..."
            Session("currentpage") = "1"
            BindGrid()
        End If

    End Sub

    Protected Sub imgBtnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSearch.Click

        Try
            If ValidateSearch() = True Then
                Session("currentpage") = "1"
                lblresult.Text = ""
                BindGrid()
            End If
        Catch ex As Exception
            pnledit.Visible = False
            pnlmain.Visible = True
            lblresult.Text = "[imgBtnSearch_Click]Error: " & ex.Message
        End Try

    End Sub

    Protected Sub imgBtnClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnClear.Click

        Try
            Dim code As Integer = 2
            Dim dt As Integer = 6
            lblresult2.Text = ""
            lstleft.Items.Clear()
            lstright.Items.Clear()

            myDS = mySetting.GetLabelDescription_(Form.ID)
            If CInt(myDS.Tables.Count) > 1 Then
                If CInt(myDS.Tables(1).Rows.Count) > 0 Then
                    For i = 0 To myDS.Tables(1).Rows.Count - 1
                        Dim myLabel As Label = Page.FindControl("lbl" & Trim(myDS.Tables(1).Rows(i).Item(code).ToString))
                        Select Case UCase(Trim(myDS.Tables(1).Rows(i).Item(dt).ToString))
                            Case "OPTION"
                                Dim myDropdownlist As DropDownList = Page.FindControl("ddl" & Trim(myDS.Tables(1).Rows(i).Item(code).ToString))
                                If myLabel.Enabled = True Then
                                    myDropdownlist.SelectedIndex = 0
                                End If
                            Case Else
                                Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(1).Rows(i).Item(code).ToString))
                                If myLabel.Enabled = True Then
                                    myTextBox.Text = ""
                                End If
                        End Select
                    Next
                End If
            End If
            myDS = Nothing
        Catch ex As Exception
            lblresult.Text = "[imgBtnClear_Click]Error: " & ex.Message
            SetFieldToFalse()
        End Try

    End Sub

    Protected Sub imgBtnCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnCancel.Click

        Session("action") = "cancel"
        Session("action_edit") = ""
        BindGrid()

    End Sub

    Protected Sub imgBtnOCP_ID_Sector_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnOCP_ID_Sector.Click

        Session("flowNumber") = 2
        Session("runNumber") = 2
        lstleft.Items.Clear()
        lstright.Items.Clear()
        txtOCP_ID_Sector.Text = ""
        Session("action_edit") = ""
        AutoAdjustPosition("2")
        imgBtnCancel.CssClass = buttonPosition3
        imgBtnClear.CssClass = buttonPosition2
        imgBtnSubmit.CssClass = buttonPosition1
        pnllstleft.Visible = False
        pnllstright.Visible = False
        invisibleLst()
        disableTxt()
        If Session("action") = "filter" Then
            txtOCP_ID_DIVISION.Enabled = True
        End If
        txtOCP_ID_Sector.Enabled = True
        lblresult2.Text = ""
        txtOCP_ID_Division.Text = ""

    End Sub

    Protected Sub imgBtnOCP_ID_Division_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnOCP_ID_Division.Click

        Session("flowNumber") = 3
        logic = False
        If CInt(Session("runNumber")) > CInt(Session("flowNumber")) Then
            logic = True
        End If
        If CInt(Session("runNumber")) < CInt(Session("flowNumber")) And Session("action") <> "filter" Then
            If txtOCP_ID_SECTOR.Text = "" Then
                lblresult2.Text = GetErrorField(Session("runNumber")) & " is a required field..."
                Exit Sub
            End If
            logic = True
        End If
        If logic = True Then
            Session("runNumber") = 3
            visibleLst()
            lstleft.Items.Clear()
            lstright.Items.Clear()
            txtOCP_ID_Division.Text = ""
            Session("action_edit") = "division"
            AutoAdjustPosition("2")
            imgBtnCancel.CssClass = buttonPosition3
            imgBtnClear.CssClass = buttonPosition2
            imgBtnSubmit.CssClass = buttonPosition1
            'imgtop.ImageUrl = "../../Images/Default/gif/imgtop_add.gif"
            'imgbottom.ImageUrl = "../../Images/Default/gif/imgbottom.gif"
            disableLst()
            disableTxt()
            'lstleft.Enabled = True
            lstright.Enabled = True
            lblresult2.Text = ""
        End If

    End Sub

    Protected Sub imgBtnOCP_ID_Division2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnOCP_ID_Division2.Click

        If ActionValidateFieldSector() = False Then
            lblresult2.Text = lblOCP_ID_Sector.Text & " field is required..."
            Exit Sub
        End If
        ssql = "Exec sp_sa_getListBoxValue 'Sector_Division','" & Session("Company") & "','" & txtOCP_ID_Sector.Text & "','','','','',''"
        mySetting.GetListBoxValue2(lstleft, lstright, ssql)
        disableTxt()
        'lstright.Enabled = True
        enableLst()
        lblresult2.Text = ""

    End Sub

    Protected Sub imgBtnAddAll_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAddAll.Click
        AddRemoveAll(lstleft, lstright)
        lblresult2.Text = ""
    End Sub

    Protected Sub imgBtnAddItem_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAddItem.Click
        AddRemoveItem(lstleft, lstright)
        lblresult2.Text = ""
    End Sub

    Protected Sub imgBtnRemoveItem_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnRemoveItem.Click
        AddRemoveItem(lstright, lstleft)
        lblresult2.Text = ""
    End Sub

    Protected Sub imgBtnRemoveAll_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnRemoveAll.Click
        AddRemoveAll(lstright, lstleft)
        lblresult2.Text = ""
    End Sub

    Sub disableLst()
        lstleft.Enabled = False
        lstright.Enabled = False
        imgBtnAddAll.Enabled = False
        imgBtnAddItem.Enabled = False
        imgBtnRemoveAll.Enabled = False
        imgBtnRemoveItem.Enabled = False
    End Sub

    Sub enableLst()
        lstleft.Enabled = True
        lstright.Enabled = True
        imgBtnAddAll.Enabled = True
        imgBtnAddItem.Enabled = True
        imgBtnRemoveAll.Enabled = True
        imgBtnRemoveItem.Enabled = True
    End Sub

    Sub visibleLst()
        lstleft.Visible = True
        lstright.Visible = True
        imgBtnAddAll.Visible = True
        imgBtnAddItem.Visible = True
        imgBtnRemoveAll.Visible = True
        imgBtnRemoveItem.Visible = True
        lbllstleft.Visible = True
        lbllstright.Visible = True
    End Sub

    Sub invisibleLst()
        lstleft.Visible = False
        lstright.Visible = False
        imgBtnAddAll.Visible = False
        imgBtnAddItem.Visible = False
        imgBtnRemoveAll.Visible = False
        imgBtnRemoveItem.Visible = False
        lbllstleft.Visible = False
        lbllstright.Visible = False
    End Sub

    Sub disableTxt()
        txtOCP_ID_Sector.Enabled = False
        txtOCP_ID_Division.Enabled = False
    End Sub

#End Region

#Region "Panel GridView"

    Protected Sub myGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles myGridView.RowDataBound

        e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor; this.style.backgroundColor='silver';")
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")

    End Sub

#End Region

#Region "Panel Action"

    Public Sub NavigationLink_Click(ByVal sender As Object, ByVal e As CommandEventArgs)

        Select Case e.CommandName
            Case "First"
                Session("currentpage") = 1
            Case "Prev"
                Session("currentpage") = Integer.Parse(CurrentPage.Text) - 1
            Case "Next"
                Session("currentpage") = Integer.Parse(CurrentPage.Text) + 1
            Case "Last"
                Session("currentpage") = Integer.Parse(TotalPages.Text)
        End Select
        lblresult.Text = ""
        BindGrid()

    End Sub

    Protected Sub imgBtnGoToPage_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnGoToPage.Click

        If txtGoToPage.Text = "" Then
            lblresult.Text = "Request failed! Value required..."
            txtGoToPage.Focus()
            Exit Sub
        End If

        Try
            Dim pagestr As String = Trim(txtGoToPage.Text)
            Dim pagenum As Integer = CInt(pagestr)
            Dim firstnum As Integer = 0
            Dim lastnum As Integer = 0

            If pagenum <= CInt(Session("TotalPages")) And pagenum > 0 Then
                If pagenum = CInt(Session("currentPage")) Then
                    lblresult.Text = "Request failed! You are looking for the same page..."
                    txtGoToPage.Focus()
                Else
                    lblresult.Text = ""
                    Session("currentpage") = pagenum
                    BindGrid()
                End If
                txtGoToPage.Text = ""
            Else
                If pagenum = 1 Or pagenum > CInt(Session("TotalPages")) And CInt(Session("TotalPages")) = 1 Then
                    lblresult.Text = "Request failed! Only one page available..."
                Else
                    If CInt(Session("currentPage")) = 1 Then
                        firstnum = 1
                    Else
                        firstnum = 0
                    End If
                    If CInt(Session("currentPage")) = CInt(Session("TotalPages")) Then
                        lastnum = CInt(Session("TotalPages"))
                    Else
                        lastnum = CInt(Session("TotalPages")) + 1
                    End If
                    lblresult.Text = "Request failed! Page number must between " & firstnum & " and " & lastnum & "..."
                End If
                txtGoToPage.Text = ""
                txtGoToPage.Focus()
            End If

        Catch ex As Exception
            lblresult.Text = "Request failed! Invalid value enter..."
            txtGoToPage.Text = ""
            txtGoToPage.Focus()
            Exit Sub
        End Try

    End Sub

    Protected Sub imgBtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAdd.Click

        If ActionValidateEdit() = True Then
            Session("action") = "add"
            Session("runNumber") = 3
            ClearText()
            SetFieldToTrue()
            SetVisible()
            imgBtnSearch.Visible = False
            imgBtnSubmit.Visible = True
            visibleLst()
            lstleft.Items.Clear()
            lstright.Items.Clear()
            Session("action_edit") = "section"
            pnldescription.Visible = True
            AutoAdjustPosition("2")

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
                                    If Trim(myGridView1.Rows(i).Cells(num).Text) = "&nbsp;" Then
                                        myDropdownlist.SelectedValue = ""
                                    Else
                                        mySetting.ArrangeDropdownlistSelectedIndex(myDropdownlist, myGridView1.Rows(i).Cells(num).Text)
                                    End If
                                Case Else
                                    Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS1.Tables(0).Rows(j).Item(code)))
                                    If Trim(myGridView1.Rows(i).Cells(num).Text) = "&nbsp;" Then
                                        myTextBox.Text = ""
                                    Else
                                        myTextBox.Text = Replace(myGridView1.Rows(i).Cells(num).Text, "amp;", "")
                                    End If
                            End Select

                        Next
                    End If
                    myDS1 = Nothing
                    ssql = "Exec sp_sa_getListBoxValue 'Sector_Division','" & Session("Company") & "','" & txtOCP_ID_SECTOR.Text & "','','','','',''"
                    mySetting.GetListBoxValue2(lstleft, lstright, ssql)
                    Exit For
                End If
            Next
        Else
            Session("action") = "add"
            Session("action_edit") = ""
            ClearText()
            SetFieldToTrue()
            SetVisible()
            imgBtnSearch.Visible = False
            imgBtnSubmit.Visible = True
            invisibleLst()
            disableLst()
            pnldescription.Visible = True
            AutoAdjustPosition("2")

            'get field default value
            Dim num As Integer = 0
            Dim code As Integer = 1
            Dim dt As Integer = 2
            Dim op As Integer = 3
            Dim dv As Integer = 4
            lblresult.Text = ""
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
        End If

        'check for addable field
        If autonum = 0 Then
            pnledit.Visible = False
            pnlmain.Visible = True
            ShowMessage("No addable item found for this page...")
            Exit Sub
        End If

        imgBtnCancel.CssClass = buttonPosition3
        imgBtnClear.CssClass = buttonPosition2
        imgBtnSubmit.CssClass = buttonPosition1
        mySetting.GetImgTypeUrl(imgtop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Add.png")
        mySetting.GetImgTypeUrl(imgbottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")
        If Session("currentpage") = 0 Then
            Session("currentpage") = "1"
        End If

    End Sub

    Protected Sub imgBtnEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnEdit.Click

        If Session("pageNotEditable") = "YES" Then
            lblresult.Text = "Field(s) not editable..."
            Exit Sub
        End If

        If ActionValidateEdit() Then
            Session("action") = "edit"
            ClearText()
            SetFieldToTrue()
            SetVisible()
            imgBtnSearch.Visible = False
            imgBtnSubmit.Visible = True
            'imgBtnUpdate.Visible = True
            visibleLst()
            lstleft.Items.Clear()
            lstright.Items.Clear()
            Session("action_edit") = "division"
            pnldescription.Visible = True
            AutoAdjustPosition("2")

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
                                    If Trim(myGridView1.Rows(i).Cells(num).Text) = "&nbsp;" Then
                                        myDropdownlist.SelectedValue = ""
                                    Else
                                        mySetting.ArrangeDropdownlistSelectedIndex(myDropdownlist, myGridView1.Rows(i).Cells(num).Text)
                                    End If
                                Case Else
                                    Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS1.Tables(0).Rows(j).Item(code)))
                                    If Trim(myGridView1.Rows(i).Cells(num).Text) = "&nbsp;" Then
                                        myTextBox.Text = ""
                                    Else
                                        myTextBox.Text = Replace(myGridView1.Rows(i).Cells(num).Text, "amp;", "")
                                    End If
                            End Select

                        Next
                    End If
                    myDS1 = Nothing

                    ssql = "Exec sp_sa_getListBoxValue 'Sector_Division','" & Session("Company") & "','" & txtOCP_ID_Sector.Text & "','','','','',''"
                    mySetting.GetListBoxValue2(lstleft, lstright, ssql)
                    Exit For
                End If
            Next

            'check for editable field
            If autonum = 0 Then
                pnledit.Visible = False
                pnlmain.Visible = True
                ShowMessage("No editable item found for this page...")
                Exit Sub
            End If

            imgBtnCancel.CssClass = buttonPosition3
            imgBtnClear.CssClass = buttonPosition2
            imgBtnSubmit.CssClass = buttonPosition1
            mySetting.GetImgTypeUrl(imgtop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Edit.png")
            mySetting.GetImgTypeUrl(imgbottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")
        End If

    End Sub

    Protected Sub imgBtnFilter_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnFilter.Click

        If Session("pageNotFilterable") = "YES" Then
            lblresult.Text = "Field(s) not filterable..."
            Exit Sub
        End If

        Session("action") = "filter"
        Session("action_edit") = ""
        ClearText()
        SetFieldToTrue()
        SetVisible()
        imgBtnSearch.Visible = True
        imgBtnSubmit.Visible = False
        invisibleLst()
        pnldescription.Visible = True
        AutoAdjustPosition("2")

        ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_SECTOR" & """," & """" & """," & """" & Session("EmpID").ToString & """"
        mySetting.GetLookupValue_ImageButton(imgBtnOCP_ID_SECTOR, Form.ID, "txtOCP_ID_SECTOR", "CodeName", ssql)
        ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_DIVISION" & """," & """" & """," & """" & Session("EmpID").ToString & """"
        mySetting.GetLookupValue_ImageButton(imgBtnOCP_ID_DIVISION, Form.ID, "txtOCP_ID_DIVISION", "CodeName", ssql)
        ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_DEPARTMENT" & """," & """" & """," & """" & Session("EmpID").ToString & """"

        'check for addable field
        If autonum = 0 Then
            pnledit.Visible = False
            pnlmain.Visible = True
            ShowMessage("No addable item found for this page...")
            Exit Sub
        End If

        imgBtnCancel.CssClass = buttonPosition3
        imgBtnClear.CssClass = buttonPosition2
        imgBtnSearch.CssClass = buttonPosition1
        mySetting.GetImgTypeUrl(imgtop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Filter.png")
        mySetting.GetImgTypeUrl(imgbottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")

    End Sub

#End Region

#Region "Sub & Function"

    Private Function GetErrorField(ByVal strerror As String) As String

        If strerror = "2" Then
            strerror = lblOCP_ID_Sector.Text
        ElseIf strerror = "3" Then
            strerror = lblOCP_ID_Division.Text
        End If
        Return strerror

    End Function

    Private Sub ShowMessage(ByVal message As String)

        Dim scriptString As String = "<script language=JavaScript>"
        scriptString += "alert('" + message + "');"
        scriptString += "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "ShowMessage", scriptString)

    End Sub

    Private Function ActionValidateFieldSector() As Boolean

        If txtOCP_ID_Sector.Text <> "" Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function ActionValidateEdit() As Boolean
        RecFound = False
        CountRecord = 0
        lblresult.Text = ""
        For i = 0 To myGridView.Rows.Count - 1
            Dim chkEdit As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
            If chkEdit.Checked Then
                RecFound = True
                CountRecord = CountRecord + 1
            End If
        Next
        If RecFound = True Then
            If CountRecord > 1 Then
                lblresult.Text = "Invalid action: Only 1 row can be selected for editing..."
                Return False
            Else
                Return True
            End If
        Else
            lblresult.Text = "Invalid action: No row selected for editing..."
            Return False
        End If
    End Function

    Sub SetVisible()

        Try 'ModifyOn051106
            myDS = mySetting.GetPageFieldSetting(Session("Company"), Form.ID, Session("EmpID"))
            If CInt(myDS.Tables.Count) > 1 Then
                If CInt(myDS.Tables(1).Rows.Count) > 0 Then

                    Dim en, vv, vw, dt, md, pr, et, fl, ps, code, name, maxlength As Integer

                    For i = 0 To myDS.Tables(1).Columns.Count - 1
                        If UCase(myDS.Tables(1).Columns(i).ToString) = "CODE" Then
                            code = i
                        ElseIf UCase(myDS.Tables(1).Columns(i).ToString) = "NAME" Then
                            name = i
                        ElseIf UCase(myDS.Tables(1).Columns(i).ToString) = "OPTION_PRIMARY_KEY" Then
                            pr = i
                        ElseIf UCase(myDS.Tables(1).Columns(i).ToString) = "OPTION_DATA_TYPE" Then
                            dt = i
                        ElseIf UCase(myDS.Tables(1).Columns(i).ToString) = "LENGTH" Then
                            maxlength = i
                        ElseIf UCase(myDS.Tables(1).Columns(i).ToString) = "OPTION_MANDATORY" Then
                            md = i
                        ElseIf UCase(myDS.Tables(1).Columns(i).ToString) = "OPTION_PASSWORD" Then
                            ps = i
                        ElseIf UCase(myDS.Tables(1).Columns(i).ToString) = "OPTION_EDITABLE" Then
                            et = i
                        ElseIf UCase(myDS.Tables(1).Columns(i).ToString) = "OPTION_SET_FILTER" Then
                            fl = i
                        ElseIf UCase(myDS.Tables(1).Columns(i).ToString) = "OPTION_VIEW_LIST" Then
                            vv = i
                        ElseIf UCase(myDS.Tables(1).Columns(i).ToString) = "OPTION_VIEW_CARD" Then
                            vw = i
                        ElseIf UCase(myDS.Tables(1).Columns(i).ToString) = "OPTION_ENABLED" Then
                            en = i
                        End If
                    Next

                    For i = 0 To myDS.Tables(1).Rows.Count - 1
                        Dim myImage As Image = Page.FindControl("img" & Trim(myDS.Tables(1).Rows(i).Item(code)))
                        Dim myLabel As Label = Page.FindControl("lbl" & Trim(myDS.Tables(1).Rows(i).Item(code)))
                        Dim myImageButton As ImageButton = Page.FindControl("imgBtn" & Trim(myDS.Tables(1).Rows(i).Item(code)))
                        'labelling
                        myLabel.Text = myDS.Tables(1).Rows(i).Item(name).ToString
                        Select Case UCase(Trim(myDS.Tables(1).Rows(i).Item(dt).ToString))

                            Case "OPTION"
                                Dim myDropdownlist As DropDownList = Page.FindControl("ddl" & Trim(myDS.Tables(1).Rows(i).Item(code)))
                                logic = False
                                If UCase(Trim(myDS.Tables(1).Rows(i).Item(vw).ToString)) = "YES" Then
                                    If UCase(Trim(myDS.Tables(1).Rows(i).Item(dt).ToString)) <> "LOOKUP" And UCase(Trim(myDS.Tables(1).Rows(i).Item(dt).ToString)) <> "DATETIME" Then
                                        myImageButton.Visible = False
                                    End If
                                    If Session("action") = "add" Or Session("action") = "edit" Then
                                        If UCase(Trim(myDS.Tables(1).Rows(i).Item(md).ToString)) = "NO" Then
                                            myImage.Visible = False
                                        End If
                                        If UCase(Trim(myDS.Tables(1).Rows(i).Item(en).ToString)) = "NO" Then
                                            myLabel.Enabled = False
                                            myDropdownlist.Enabled = False
                                            myImageButton.Enabled = False
                                        End If
                                    End If
                                    If Session("action") = "edit" Then
                                        If UCase(Trim(myDS.Tables(1).Rows(i).Item(pr).ToString)) = "YES" Or UCase(Trim(myDS.Tables(1).Rows(i).Item(et).ToString)) = "NO" Then
                                            myLabel.Enabled = False
                                            myDropdownlist.Enabled = False
                                            myImageButton.Enabled = False
                                        End If
                                    End If
                                    If Session("action") = "filter" Then
                                        If UCase(Trim(myDS.Tables(1).Rows(i).Item(fl).ToString)) = "NO" Then
                                            logic = True
                                        End If
                                        myImage.Visible = False
                                    End If
                                Else
                                    logic = True
                                End If
                                If logic = True Then
                                    myImage.Visible = False
                                    myLabel.Visible = False
                                    myDropdownlist.Visible = False
                                    myImageButton.Visible = False
                                End If

                            Case Else
                                Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(1).Rows(i).Item(code)))
                                logic = False
                                If UCase(Trim(myDS.Tables(1).Rows(i).Item(vw).ToString)) = "YES" Then
                                    If UCase(Trim(myDS.Tables(1).Rows(i).Item(dt).ToString)) <> "LOOKUP" And UCase(Trim(myDS.Tables(1).Rows(i).Item(dt).ToString)) <> "DATETIME" And UCase(Trim(myDS.Tables(1).Rows(i).Item(dt).ToString)) <> "DATE" And UCase(Trim(myDS.Tables(1).Rows(i).Item(dt).ToString)) <> "TIME" Then
                                        myImageButton.Visible = False
                                    End If
                                    If Session("action") = "add" Or Session("action") = "edit" Then
                                        If UCase(Trim(myDS.Tables(1).Rows(i).Item(md).ToString)) = "NO" Then
                                            myImage.Visible = False
                                        End If
                                        If UCase(Trim(myDS.Tables(1).Rows(i).Item(en).ToString)) = "NO" Then
                                            myLabel.Enabled = False
                                            myTextBox.Enabled = False
                                            myImageButton.Enabled = False
                                        End If
                                    End If
                                    If Session("action") = "edit" Then
                                        If UCase(Trim(myDS.Tables(1).Rows(i).Item(pr).ToString)) = "YES" Or UCase(Trim(myDS.Tables(1).Rows(i).Item(et).ToString)) = "NO" Then
                                            myLabel.Enabled = False
                                            myTextBox.Enabled = False
                                            myImageButton.Enabled = False
                                        End If
                                    End If
                                    If Session("action") = "filter" Then
                                        If UCase(Trim(myDS.Tables(1).Rows(i).Item(fl).ToString)) = "NO" Then
                                            logic = True
                                        End If
                                        myImage.Visible = False
                                    End If
                                Else
                                    logic = True
                                End If
                                If logic = True Then
                                    myImage.Visible = False
                                    myLabel.Visible = False
                                    myTextBox.Visible = False
                                    myImageButton.Visible = False
                                End If
                                'maxlength setting
                                myTextBox.MaxLength = CInt(myDS.Tables(1).Rows(i).Item(maxlength).ToString)
                                If Session("action") = "filter" Then
                                    mySetting.ResetUppercase(myTextBox)
                                Else
                                    'upper case setting
                                    If UCase(Trim(myDS.Tables(1).Rows(i).Item(pr).ToString)) = "YES" Then
                                        mySetting.ConvertUppercase(myTextBox)
                                    End If
                                End If
                                'password setting
                                If UCase(Trim(myDS.Tables(1).Rows(i).Item(ps).ToString)) = "YES" Then
                                    myTextBox.TextMode = TextBoxMode.Password
                                End If

                        End Select
                    Next

                End If
            End If
            myDS = Nothing
        Catch ex As Exception
            lblresult.Text = "[SetVisible]Error: " & ex.Message
            SetFieldToFalse()
        End Try

    End Sub

    Sub AutoAdjustPosition(ByVal strSelect As String)

        Dim myPosition As String
        Dim autonum2 As Integer = 0
        Select Case strSelect
            Case "1"
                myPosition = rowPositionM
            Case "2"
                myPosition = rowPosition
            Case Else
                myPosition = rowPosition
        End Select

        'get field position
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

        ' set all button 2 to false & button 1 to true
        imgBtnOCP_ID_DIVISION2.Visible = False
        If Session("action") <> "filter" Then
            imgBtnOCP_ID_DIVISION.Visible = True
        End If

        'get field position
        ssql = "exec sp_sa_get_fields_position '" & Form.ID & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        autonum = 0
        If myDS.Tables(0).Rows.Count > 0 Then
            For i = 0 To myDS.Tables(0).Rows.Count - 1

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OCP_ID_SECTOR" Then
                    If lblOCP_ID_Sector.Visible = True Then
                        autonum += 1
                        imgOCP_ID_SECTOR.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgOCP_ID_SECTOR.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgOCP_ID_SECTOR.Style.Add("position", "absolute")
                        autonum += 1
                        lblOCP_ID_SECTOR.CssClass = "wordstyle12"
                        lblOCP_ID_SECTOR.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOCP_ID_SECTOR.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOCP_ID_SECTOR.Style.Add("position", "absolute")
                        autonum += 1
                        txtOCP_ID_SECTOR.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        txtOCP_ID_SECTOR.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        txtOCP_ID_SECTOR.Style.Add("position", "absolute")
                        txtOCP_ID_SECTOR.Width = txtWidth
                        autonum += 1
                        imgBtnOCP_ID_SECTOR.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgBtnOCP_ID_SECTOR.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgBtnOCP_ID_SECTOR.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OCP_ID_DIVISION" Then
                    If Session("action_edit") = "division" Then
                        ' set lst pnl 
                        pnllstleft.Visible = True
                        pnllstright.Visible = True
                        enableLst()

                        autonum2 = autonum
                        autonum2 = autonum2 / 4
                        If autonum2 Mod 2 <> 0 Then
                            autonum2 = (autonum2 + 3) / 2
                            autonum = autonum + 5
                        Else
                            autonum2 = (autonum2 + 2) / 2
                            autonum = autonum + 1
                        End If

                        imgOCP_ID_DIVISION.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgOCP_ID_DIVISION.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgOCP_ID_DIVISION.Style.Add("position", "absolute")
                        autonum += 1
                        lblOCP_ID_DIVISION.CssClass = "wordstyle12"
                        lblOCP_ID_DIVISION.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOCP_ID_DIVISION.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOCP_ID_DIVISION.Style.Add("position", "absolute")
                        autonum += 1

                        txtOCP_ID_DIVISION.Visible = False

                        pnllstleft.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        pnllstleft.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        pnllstleft.Style.Add("position", "absolute")
                        pnllstleft.Width = txtWidth
                        lbllstleft.Width = txtWidth
                        lstleft.Width = ddlWidth
                        autonum += 1
                        imgBtnAddAll.Style.Add("left", mySetting.GetObjPositionRL(EctWidth, 1, "X"))
                        imgBtnAddAll.Style.Add("top", mySetting.GetObjPositionRL(1, autonum2, "Y"))
                        imgBtnAddAll.Style.Add("position", "absolute")
                        imgBtnAddItem.Style.Add("left", mySetting.GetObjPositionRL(EctWidth, 2, "X"))
                        imgBtnAddItem.Style.Add("top", mySetting.GetObjPositionRL(2, autonum2, "Y"))
                        imgBtnAddItem.Style.Add("position", "absolute")
                        imgBtnRemoveItem.Style.Add("left", mySetting.GetObjPositionRL(EctWidth, 3, "X"))
                        imgBtnRemoveItem.Style.Add("top", mySetting.GetObjPositionRL(3, autonum2, "Y"))
                        imgBtnRemoveItem.Style.Add("position", "absolute")
                        imgBtnRemoveAll.Style.Add("left", mySetting.GetObjPositionRL(EctWidth, 4, "X"))
                        imgBtnRemoveAll.Style.Add("top", mySetting.GetObjPositionRL(4, autonum2, "Y"))
                        imgBtnRemoveAll.Style.Add("position", "absolute")
                        pnllstright.Style.Add("left", mySetting.GetObjPositionRL(EctWidth, 5, "X"))
                        pnllstright.Style.Add("top", mySetting.GetObjPositionRL(5, autonum2, "Y"))
                        pnllstright.Style.Add("position", "absolute")
                        pnllstright.Width = txtWidth
                        lbllstright.Width = txtWidth
                        lstright.Width = ddlWidth

                        imgBtnOCP_ID_DIVISION2.Visible = True
                        imgBtnOCP_ID_DIVISION.Visible = False
                        imgBtnOCP_ID_DIVISION2.Style.Add("left", mySetting.GetObjPositionRL(EctWidth, 6, "X"))
                        imgBtnOCP_ID_DIVISION2.Style.Add("top", mySetting.GetObjPositionRL(6, autonum2, "Y"))
                        imgBtnOCP_ID_DIVISION2.Style.Add("position", "absolute")
                        autonum += 44
                    Else
                        If lblOCP_ID_DIVISION.Visible = True Then
                            autonum += 1
                            imgOCP_ID_DIVISION.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                            imgOCP_ID_DIVISION.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                            imgOCP_ID_DIVISION.Style.Add("position", "absolute")
                            autonum += 1
                            lblOCP_ID_DIVISION.CssClass = "wordstyle12"
                            lblOCP_ID_DIVISION.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                            lblOCP_ID_DIVISION.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                            lblOCP_ID_DIVISION.Style.Add("position", "absolute")
                            autonum += 1
                            txtOCP_ID_DIVISION.Visible = True
                            txtOCP_ID_DIVISION.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                            txtOCP_ID_DIVISION.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                            txtOCP_ID_DIVISION.Style.Add("position", "absolute")
                            txtOCP_ID_DIVISION.Width = txtWidth
                            autonum += 1
                            imgBtnOCP_ID_DIVISION.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                            imgBtnOCP_ID_DIVISION.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                            imgBtnOCP_ID_DIVISION.Style.Add("position", "absolute")
                        End If
                    End If
                End If
            Next
        End If
        myDS = Nothing

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

    End Sub

    Sub ClearText()

        Try
            Dim code As Integer = 2
            Dim dt As Integer = 6
            lblresult.Text = ""

            myDS = mySetting.GetLabelDescription_(Form.ID)
            If CInt(myDS.Tables.Count) > 1 Then
                If CInt(myDS.Tables(1).Rows.Count) > 0 Then
                    For i = 0 To myDS.Tables(1).Rows.Count - 1
                        Select Case UCase(Trim(myDS.Tables(1).Rows(i).Item(dt).ToString))
                            Case "OPTION"
                                Dim myDropdownlist As DropDownList = Page.FindControl("ddl" & Trim(myDS.Tables(1).Rows(i).Item(code).ToString))
                                myDropdownlist.SelectedIndex = 0
                            Case Else
                                Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(1).Rows(i).Item(code).ToString))
                                myTextBox.Text = ""
                        End Select
                    Next
                End If
            End If
            myDS = Nothing
        Catch ex As Exception
            lblresult.Text = "[ClearText]Error: " & ex.Message
            SetFieldToFalse()
        End Try

    End Sub

    Sub SetFieldToTrue()

        Try
            Dim code As Integer = 2
            Dim dt As Integer = 6
            pnledit.Visible = True
            pnlmain.Visible = False
            lblresult2.Visible = True

            myDS = mySetting.GetLabelDescription_(Form.ID)
            If CInt(myDS.Tables.Count) > 1 Then
                If CInt(myDS.Tables(1).Rows.Count) > 0 Then
                    For i = 0 To myDS.Tables(1).Rows.Count - 1
                        Dim myImage As Image = Page.FindControl("img" & Trim(myDS.Tables(1).Rows(i).Item(code).ToString))
                        Dim myLabel As Label = Page.FindControl("lbl" & Trim(myDS.Tables(1).Rows(i).Item(code).ToString))
                        Dim myImageButton As ImageButton = Page.FindControl("imgBtn" & Trim(myDS.Tables(1).Rows(i).Item(code).ToString))
                        Select Case UCase(Trim(myDS.Tables(1).Rows(i).Item(dt).ToString))
                            Case "OPTION"
                                Dim myDropdownlist As DropDownList = Page.FindControl("ddl" & Trim(myDS.Tables(1).Rows(i).Item(code).ToString))
                                myDropdownlist.Visible = True
                                myDropdownlist.Enabled = True
                            Case Else
                                Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(1).Rows(i).Item(code).ToString))
                                myTextBox.Visible = True
                                myTextBox.Enabled = True
                        End Select
                        myImage.Visible = True
                        myLabel.Visible = True
                        myImageButton.Visible = True
                        myLabel.Enabled = True
                        myLabel.Enabled = True
                        myImageButton.Enabled = True
                    Next
                End If
            End If
            myDS = Nothing
        Catch ex As Exception
            lblresult.Text = "[SetFieldToTrue]Error: " & ex.Message
            SetFieldToFalse()
        End Try

    End Sub

    Sub SetFieldToFalse()
        pnledit.Visible = False
        pnlmain.Visible = True
    End Sub

    Private Function ValidateInsertUpdate() As Boolean

        Try
            myDS = mySQL.ExecuteSQL("Select Code,Option_Data_Type,Option_Primary_Key,[Name] From Table_Field Where Table_Profile_Code='" & Form.ID & "' And Option_View_Card='YES' Order By Table_Profile_Code,Sequence_No", Session("Company").ToString, Session("EmpID").ToString)
            ssql1 = "Insert Into " & Form.ID & "("
            ssql2 = ") Values("
            ssql6 = "Delete From " & Form.ID & " Where "
            ssql3 = ""
            ssql4 = ""
            If myDS.Tables(0).Rows.Count > 0 Then
                For i = 0 To myDS.Tables(0).Rows.Count - 1
                    Select Case myDS.Tables(0).Rows(i).Item(1).ToString
                        Case "OPTION"
                            logic = False
                            Dim myDropdownlist As DropDownList = Page.FindControl("ddl" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                            Dim myImg As Image = Page.FindControl("img" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                            If myImg.Visible = True Then
                                If mySetting.CheckTextNull(myDropdownlist.SelectedValue) = True Then
                                    logic = True
                                Else
                                    lblresult2.Text = Trim(myDS.Tables(0).Rows(i).Item(3)) & " field is required..."
                                    myDropdownlist.Focus()
                                    Return False
                                End If
                            Else
                                logic = True
                            End If
                            If logic = True Then
                                If mySetting.ValidateInput(myDropdownlist, myDS.Tables(0).Rows(i).Item(1).ToString) = True Then
                                    ssql1 = ssql1 & myDS.Tables(0).Rows(i).Item(0) & ","
                                    ssql2 = ssql2 & "'" & Trim(myDropdownlist.SelectedValue) & "',"
                                    ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "='" & Trim(myDropdownlist.SelectedValue) & "' And "
                                    ssql4 = ssql4 & myDS.Tables(0).Rows(i).Item(0) & "=" & Trim(myDropdownlist.SelectedValue) & ","
                                Else
                                    lblresult2.Text = "Invalid selection for " & Trim(myDS.Tables(0).Rows(i).Item(3)) & " !"
                                    myDropdownlist.Focus()
                                    Return False
                                End If
                            End If
                        Case "CHARACTER", "LOOKUP", "DECIMAL", "INTEGER"
                            logic = False
                            If i = myDS.Tables(0).Rows.Count - 1 Then
                                'get data for division
                                lblresult2.Text = "Checking level grouping relationship..."
                                ssql10 = "Select [ID] From Organisation_Code_Profile_Vw Where Company_Profile_Code = '" & Session("Company") & _
                                "' And Option_Type = 'Division' And [ID] In (Select OCP_ID_Division From Division_Department_Grouping where ocp_id_sector = '" & mySetting.GetOCPIDbyCodeName_ReturnString(Session("Company"), txtOCP_ID_SECTOR.Text) & "')"
                                myDS1 = mySQL.ExecuteSQL(ssql10, Session("Company").ToString, Session("EmpID").ToString)
                                'checking lower grouping relation
                                If lstleft.Items.Count <> 0 Then
                                    For j = 0 To lstleft.Items.Count - 1
                                        For k = 0 To myDS1.Tables(0).Rows.Count - 1
                                            If Trim(lstleft.Items(j).Value) = myDS1.Tables(0).Rows(k).Item(0).ToString() Then
                                                lblresult2.Text = "Please remove all the lower level grouping for " & Trim(lstleft.Items(j).Text) & " before continue..."
                                                'reset data to default
                                                lstleft.Items.Clear()
                                                lstright.Items.Clear()
                                                ssql = "Exec sp_sa_getListBoxValue 'Sector_Division','" & Session("Company") & "','" & txtOCP_ID_Sector.Text & "','','','','',''"
                                                myDS3 = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                                                If myDS3.Tables(0).Rows.Count > 0 Then
                                                    lstleft.DataSource = myDS3.Tables(0)
                                                    lstleft.DataTextField = "Name"
                                                    lstleft.DataValueField = "Code"
                                                    lstleft.DataBind()
                                                End If
                                                If myDS3.Tables(1).Rows.Count > 0 Then
                                                    lstright.DataSource = myDS3.Tables(1)
                                                    lstright.DataTextField = "Name"
                                                    lstright.DataValueField = "Code"
                                                    lstright.DataBind()
                                                End If
                                                myDS = Nothing
                                                myDS1 = Nothing
                                                myDS2 = Nothing
                                                myDS3 = Nothing
                                                Return False
                                            End If
                                        Next
                                    Next
                                End If 'checking code end here
                                'add or update selected items
                                If lstright.Items.Count <> 0 Then
                                    ssql1 = ssql1 & myDS.Tables(0).Rows(i).Item(0)
                                    ssql7 = ssql7 & Left(ssql3, Len(ssql3) - 5)
                                    ssql3 = ssql6 & Left(ssql3, Len(ssql3) - 5)
                                    mySQL.ExecuteSQL(ssql3, Session("Company").ToString, Session("EmpID").ToString)
                                    ssql4 = ssql4 & myDS.Tables(0).Rows(i).Item(0) & "="
                                    For j = 0 To lstright.Items.Count - 1
                                        ssql = ssql2 & "'" & Trim(lstright.Items(j).Value) & "'"
                                        ssql = ssql1 & ssql & ")"
                                        mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                                        ssql5 = ssql4 & Trim(lstright.Items(j).Value)
                                        If j = lstright.Items.Count - 1 Then
                                            ssql4 = ssql5
                                        End If
                                    Next
                                Else
                                    ssql3 = ssql6 & Left(ssql3, Len(ssql3) - 5)
                                    mySQL.ExecuteSQL(ssql3, Session("Company").ToString, Session("EmpID").ToString)
                                End If
                                'lblresult.Text = "Data edit successfully..."
                                Exit Select
                            End If

                            'for non last field
                            Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                            Dim myImg As Image = Page.FindControl("img" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                            If myImg.Visible = True Then
                                If mySetting.CheckTextNull(myTextBox.Text) = True Then
                                    logic = True
                                Else
                                    lblresult2.Text = Trim(myDS.Tables(0).Rows(i).Item(3)) & " field is required..."
                                    myTextBox.Focus()
                                    Return False
                                End If
                            Else
                                logic = True
                            End If
                            If logic = True Then
                                If mySetting.ValidateInput(myTextBox, myDS.Tables(0).Rows(i).Item(1).ToString) = True Then
                                    ssql1 = ssql1 & myDS.Tables(0).Rows(i).Item(0) & ","
                                    'start ------->>
                                    ssql5 = "Select Function_Name,Default_Value,Default_Value2 From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='UPDATE'"
                                    myDST = mySQL.ExecuteSQL(ssql5, Session("Company").ToString, Session("EmpID").ToString)
                                    If myDST.Tables(0).Rows.Count > 0 Then
                                        If myDST.Tables(0).Rows(0).Item(2).ToString.Trim = "COMPANY" Then
                                            ssql8 = "Select " & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString & "',N'" & Session("Company").ToString & "')"
                                        ElseIf myDST.Tables(0).Rows(0).Item(2).ToString.Trim <> "" Then
                                            ssql8 = "Select " & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString & "',N'" & myDST.Tables(0).Rows(0).Item(2).ToString & "')"
                                        ElseIf myDST.Tables(0).Rows(0).Item(1).ToString.Trim <> "" Then
                                            ssql8 = "Select " & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString & "')"
                                        Else
                                            ssql8 = "Select " & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "')"
                                        End If
                                        myDS1 = mySQL.ExecuteSQL(ssql8, Session("Company").ToString, Session("EmpID").ToString)
                                        If myDS1.Tables(0).Rows(0).Item(0).ToString <> "" Then
                                            ssql2 = ssql2 & "'" & myDS1.Tables(0).Rows(0).Item(0).ToString() & "',"
                                            ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "='" & myDS1.Tables(0).Rows(0).Item(0).ToString() & "' And "
                                            ssql4 = ssql4 & myDS.Tables(0).Rows(i).Item(0) & "=" & myDS1.Tables(0).Rows(0).Item(0).ToString() & ","
                                        Else
                                            lblresult2.Text = "Data Error for [" & Trim(myDS.Tables(0).Rows(i).Item(3)) & "] field! Make sure [" & myTextBox.Text.ToString & "] exist in Database..."
                                            myTextBox.Focus()
                                            myDS1 = Nothing
                                            myDST = Nothing
                                            myDS = Nothing
                                            Return False
                                        End If
                                        myDS1 = Nothing
                                    Else
                                        ssql2 = ssql2 & "ISNULL(N'" & Trim(myTextBox.Text.ToString) & "',''),"
                                        ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(N'" & Trim(myTextBox.Text.ToString) & "','') And "
                                        ssql4 = ssql4 & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(N'" & Trim(myTextBox.Text.ToString) & "',''),"
                                    End If
                                    myDST = Nothing
                                    'end ------->>
                                Else
                                    lblresult2.Text = "Invalid input format for " & Trim(myDS.Tables(0).Rows(i).Item(3)) & " !"
                                    myTextBox.Focus()
                                    Return False
                                End If
                            End If
                        Case "DATETIME", "DATE"
                            logic = False
                            Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                            Dim myImg As Image = Page.FindControl("img" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                            If myImg.Visible = True Then
                                If mySetting.CheckTextNull(myTextBox.Text) = True Then
                                    logic = True
                                Else
                                    lblresult2.Text = Trim(myDS.Tables(0).Rows(i).Item(3)) & " field is required..."
                                    myTextBox.Focus()
                                    Return False
                                End If
                            Else
                                logic = True
                            End If
                            If logic = True Then
                                Dim strDate As String = Trim(myTextBox.Text)
                                strDate = mySetting.ConvertDateToDecimal(myTextBox.Text, Session("Company"), Session("Module"))
                                If Len(strDate) = 14 Then
                                    ssql1 = ssql1 & myDS.Tables(0).Rows(i).Item(0) & ","
                                    ssql2 = ssql2 & "'" & strDate & "',"
                                    ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "='" & strDate & "' And "
                                    ssql4 = ssql4 & myDS.Tables(0).Rows(i).Item(0) & "=" & strDate & ","
                                Else
                                    lblresult2.Text = "Invalid input format for " & Trim(myDS.Tables(0).Rows(i).Item(3)) & " !"
                                    myTextBox.Focus()
                                    Return False
                                End If
                            End If
                    End Select
                Next
                myDS = Nothing
                Session("insertArgument") = ssql4
            End If
            Return True
        Catch ex As Exception
            lblresult2.Text = "[ValidateInsertUpdate]Error: " & ex.Message
            myDS = Nothing
            myDS1 = Nothing
            myDS2 = Nothing
            myDS3 = Nothing
            Return False
        End Try

    End Function

    Private Sub AddRemoveAll(ByVal aSource As ListBox, ByVal aTarget As ListBox)

        Try
            For i = 0 To aSource.Items.Count - 1
                aTarget.Items.Add(aSource.Items(i))
            Next
            aSource.Items.Clear()
        Catch ex As Exception
            lblresult2.Text = "Error: " + ex.Message
        End Try


    End Sub

    Private Sub AddRemoveItem(ByVal aSource As ListBox, ByVal aTarget As ListBox)

        Try
            For i = 0 To aSource.Items.Count - 1
                If aSource.Items(i).Selected Then
                    aTarget.Items.Add(aSource.Items(i))
                End If
            Next
            For i = aSource.Items.Count - 1 To 0 Step -1
                If aSource.Items(i).Selected = True Then
                    aSource.Items.Remove(aSource.Items(i))
                End If
            Next

        Catch ex As Exception
            lblresult2.Text = "Error: " + ex.Message
        End Try

    End Sub

    Private Function ValidateSearch() As Boolean
        Dim tmpssql As String = "", strSplit As String = ""
        lblresult2.Visible = True
        ssql = ""
        ssql2 = ""
        ssql3 = ""
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL("Select Code,Name,Option_Data_Type From Table_Field Where Table_Profile_Code='" & Form.ID & "' And Option_View_Card='YES' Order By Table_Profile_Code,Sequence_No", Session("Company").ToString, Session("EmpID").ToString)
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
                                ssql3 = ssql3 & Trim(myTxt.Text.ToString) & "@@"
                            End If
                            tmpssql = ""
                            myTxt = Nothing
                        Case "LOOKUP", "CHARACTER"
                                Dim myTxt As TextBox = Page.FindControl("txt" & myDR(0).ToString)
                                If myTxt.Visible = True And Trim(myTxt.Text) <> "" Then
                                    ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                                    ssql2 = ssql2 & myDR(0).ToString & "@@"
                                    ssql3 = ssql3 & Trim(myTxt.Text.ToString) & "@@"
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

                If ssql <> "" And ssql2 <> "" And ssql3 <> "" Then
                    Session("ssql1") = ssql
                    Session("ssql2") = ssql2
                    Session("ssql3") = ssql3
                    Session("action") = "search"
                Else
                    Session("action") = "any"
                End If

            End If
        End If
        myDS = Nothing
        Return True
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