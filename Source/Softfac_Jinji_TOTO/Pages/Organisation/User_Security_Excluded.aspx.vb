Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.web.Configuration

Partial Class Pages_Organisation_User_Security_Excluded
    Inherits System.Web.UI.Page

#Region "Public Declaration"
    Private WithEvents mySQL As New clsSQL, myDS, myDS1, myDS2, myDS3 As New DataSet, mySetting As New clsGlobalSetting, myMsg As New clsMessage
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Public ssql As String, i As Integer, j As Integer, k As Integer, SearchByPage As Boolean, RecFound As Boolean, CountRecord As Integer
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
    Dim strPath As String = "../Images"
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
            If Session("action") = "filter" Then
                If txtTABLE_PROFILE_CODE.Text <> "" Then
                    ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "TABLE_FIELD_CODE" & """," & """" & Trim(txtTABLE_PROFILE_CODE.Text) & """," & """" & Session("EmpID").ToString & """"
                    mySetting.GetLookupValue_ImageButton(imgbtnTABLE_FIELD_CODE, Form.ID, "txtTABLE_FIELD_CODE", "CodeName", ssql)
                Else
                    ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "TABLE_FIELD_CODE" & """," & """" & """," & """" & Session("EmpID").ToString & """"
                    mySetting.GetLookupValue_ImageButton(imgbtnTABLE_FIELD_CODE, Form.ID, "txtTABLE_FIELD_CODE", "CodeName", ssql)
                    txtTABLE_FIELD_CODE.Text = ""
                End If
            End If
        End If

    End Sub

    Sub PagePreload()
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")
        Session("FilterField") = ""
        Session("FilterCriteria") = ""
        Session("Module") = "ORGANISATION"
        Session("action") = ""
        pnlEdit.Visible = False
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
        mySetting.GetBtnImgUrl(imgBtnDelete, Session("Company").ToString, btnColourDef, "btnDelete.png")
        'mySetting.GetBtnImgUrl(imgBtnUpdate, Session("Company").ToString, btnColourDef, "btnUpdate.png")
        mySetting.GetBtnImgUrl(imgBtnEdit, Session("Company").ToString, btnColourDef, "btnEdit.png")
        mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourDef, "btnPrint.png")
        mySetting.GetBtnImgUrl(imgBtnGoToPage, Session("Company").ToString, btnColourDef, "btngo.png")
        mySetting.SetTextBoxPressEnterGoToImageButton(txtGoToPage, imgBtnGoToPage)

        mySetting.GetBtnImgUrl(imgBtnAddAll, Session("Company").ToString, btnColourDef, "addall.png")
        mySetting.GetBtnImgUrl(imgBtnAddItem, Session("Company").ToString, btnColourDef, "additem.png")
        mySetting.GetBtnImgUrl(imgBtnRemoveAll, Session("Company").ToString, btnColourDef, "removeall.png")
        mySetting.GetBtnImgUrl(imgBtnRemoveItem, Session("Company").ToString, btnColourDef, "removeitem.png")
        mySetting.GetImgBtnUrl(imgbtnTABLE_FIELD_CODE2, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)

        'lookup field value seting
        ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "COMPANY_PROFILE_CODE" & """," & """" & """," & """" & Session("EmpID").ToString & """"
        mySetting.GetLookupValue_ImageButton(imgbtnCOMPANY_PROFILE_CODE, Form.ID, "txtCOMPANY_PROFILE_CODE", "CodeName", ssql)
        ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "USER_PROFILE_CODE" & """," & """" & """," & """" & Session("EmpID").ToString & """"
        mySetting.GetLookupValue_ImageButton(imgbtnUSER_PROFILE_CODE, Form.ID, "txtUSER_PROFILE_CODE", "CodeName", ssql)
        ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "TABLE_PROFILE_CODE" & """," & """" & """," & """" & Session("EmpID").ToString & """"
        mySetting.GetLookupValue_ImageButton(imgbtnTABLE_PROFILE_CODE, Form.ID, "txtTABLE_PROFILE_CODE", "CodeName", ssql)

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
                    pnlEdit.Visible = False
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
                imgBtnDelete.Visible = AllowDelete
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
                                'mySetting.GetLookupValue_ImageButton(myImageButton, Form.ID, "txt" & myDT2.Rows(i).Item(2).ToString, "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & myDT2.Rows(i).Item(2).ToString & """," & """" & """")
                                'Dim myTextBox As TextBox = Page.FindControl("txt" & myDT2.Rows(i).Item(2).ToString)
                                'myTextBox.MaxLength = CInt(myDT2.Rows(i).Item(7).ToString)
                                'myTextBox = Nothing
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
                Else
                    Session("pageNotFilterable") = "NO"
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
                ssql = "Exec sp_sa_GetTableRecordsWithSecurity '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & Session("Module").ToString & "','" & Form.ID & "','" & myGridView.PageSize & "','" & Session("currentpage") & "'"
                ssql1 = "exec sp_sa_compareTableRecords '" & Session("Company") & "','" & Session("EmpID") & "','" & Session("Module") & "','" & Form.ID & "','" & myGridView.PageSize & "','" & Session("currentpage") & "'"
            End If

            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            myDS1 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
            pnldescription.Visible = False
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
                    imgBtnEdit.Enabled = False
                    imgBtnDelete.Enabled = False
                    imgBtnFilter.Enabled = False
                    imgBtnPrint.Enabled = False
                    imgBtnGoToPage.Enabled = False
                    mySetting.GetBtnImgUrl(imgBtnEdit, Session("Company").ToString, btnColourAlt, "btnEdit.png")
                    mySetting.GetBtnImgUrl(imgBtnDelete, Session("Company").ToString, btnColourAlt, "btnDelete.png")
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
                imgBtnEdit.Enabled = True
                imgBtnDelete.Enabled = True
                imgBtnFilter.Enabled = True
                imgBtnPrint.Enabled = True
                imgBtnGoToPage.Enabled = True
                mySetting.GetBtnImgUrl(imgBtnAdd, Session("Company").ToString, btnColourDef, "btnAdd.png")
                mySetting.GetBtnImgUrl(imgBtnEdit, Session("Company").ToString, btnColourDef, "btnEdit.png")
                mySetting.GetBtnImgUrl(imgBtnDelete, Session("Company").ToString, btnColourDef, "btnDelete.png")
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
            Session("action") = "clear"
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

    Protected Sub imgbtnTABLE_FIELD_CODE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnTABLE_FIELD_CODE.Click

        If Session("action") <> "filter" Then
            If ActionValidateFieldCompany() = False Then
                lblresult2.Text = lblCOMPANY_PROFILE_CODE.Text & " field is required..."
                Exit Sub
            End If
            If ActionValidateFieldUserProfile() = False Then
                lblresult2.Text = lblUSER_PROFILE_CODE.Text & " field is required..."
                Exit Sub
            End If
            If ActionValidateFieldTableProfile() = False Then
                lblresult2.Text = lblTABLE_PROFILE_CODE.Text & " field is required..."
                Exit Sub
            End If
            visibleLst()
            lstleft.Items.Clear()
            lstright.Items.Clear()
            txtTABLE_FIELD_CODE.Text = ""
            Session("action_edit") = "value"
            AutoAdjustPosition("2")
            imgBtnCancel.CssClass = buttonPosition3
            imgBtnClear.CssClass = buttonPosition2
            imgBtnSubmit.CssClass = buttonPosition1
        Else
            If ActionValidateFieldTableProfile() = False Then
                lblresult2.Text = lblTABLE_PROFILE_CODE.Text & " field is required..."
                Exit Sub
            End If
        End If
        lblresult2.Text = ""

    End Sub

    Protected Sub imgbtnTABLE_FIELD_CODE2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnTABLE_FIELD_CODE2.Click

        lstleft.Items.Clear()
        lstright.Items.Clear()
        ssql = "Exec sp_sa_getListBoxValue 'getGroupByTFCUSE','" & Session("Company") & "','" & txtTABLE_PROFILE_CODE.Text & "','" & txtUSER_PROFILE_CODE.Text & "','','','',''"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            lstleft.DataSource = myDS.Tables(0)
            lstleft.DataTextField = "Name"
            lstleft.DataValueField = "Code"
            lstleft.DataBind()
        End If
        If myDS.Tables(1).Rows.Count > 0 Then
            lstright.DataSource = myDS.Tables(1)
            lstright.DataTextField = "Name"
            lstright.DataValueField = "Code"
            lstright.DataBind()
        End If
        'disableTxt()
        'enableLst()
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

        Session("action") = "add"
        Session("action_edit") = ""
        ClearText()
        SetFieldToTrue()
        SetVisible()
        imgBtnSearch.Visible = False
        imgBtnSubmit.Visible = True
        pnldescription.Visible = True
        invisibleLst()
        disableLst()
        AutoAdjustPosition("2")

        If ActionValidateEdit() = True Then
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
                    Exit For
                End If
            Next
        Else
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
        End If

        'check for addable field
        If autonum = 0 Then
            pnlEdit.Visible = False
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

        'If Session("pageNotEditable") = "YES" Then
        '    lblresult.Text = "Field(s) not editable..."
        '    Exit Sub
        'End If

        If ActionValidateEdit() Then
            Session("action") = "edit"
            ClearText()
            SetFieldToTrue()
            SetVisible()
            imgBtnSearch.Visible = False
            imgBtnSubmit.Visible = True
            'imgBtnUpdate.Visible = True
            pnldescription.Visible = True
            visibleLst()
            lstleft.Items.Clear()
            lstright.Items.Clear()
            Session("action_edit") = "value"
            AutoAdjustPosition("2")

            imgTABLE_FIELD_CODE.Enabled = True
            lblTABLE_FIELD_CODE.Enabled = True

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

                    ssql = "Exec sp_sa_getListBoxValue 'getGroupByTFCUSE','" & Session("Company") & "','" & txtTABLE_PROFILE_CODE.Text & "','" & txtUSER_PROFILE_CODE.Text & "','','','',''"
                    myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                    If myDS.Tables(0).Rows.Count > 0 Then
                        lstleft.DataSource = myDS.Tables(0)
                        lstleft.DataTextField = "Name"
                        lstleft.DataValueField = "Code"
                        lstleft.DataBind()
                    End If
                    If myDS.Tables(1).Rows.Count > 0 Then
                        lstright.DataSource = myDS.Tables(1)
                        lstright.DataTextField = "Name"
                        lstright.DataValueField = "Code"
                        lstright.DataBind()
                    End If
                    lblresult2.Text = ""

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
            'imgBtnUpdate.CssClass = buttonPosition1
            imgBtnSubmit.CssClass = buttonPosition1
            mySetting.GetImgTypeUrl(imgtop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Edit.png")
            mySetting.GetImgTypeUrl(imgbottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")
        End If

    End Sub

    Protected Sub imgBtnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnDelete.Click

        If ActionValidateDelete() Then
            Dim getnum As Integer = 0
            For i = 0 To myGridView.Rows.Count - 1
                Dim chkDelete As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
                If chkDelete.Checked Then
                    getnum += 1
                    ssql = mySetting.GetSQLParameter2(Form.ID, clsGlobalSetting.SQLAction.DELETE_Statement3, myGridView1, i, Session("Company"), Session("Module"))
                    If ssql <> Nothing Then
                        mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                        ssql = Nothing
                    End If
                    chkDelete.Checked = False
                End If
            Next
            Session("currentpage") = "1"
            Session("action") = "cancel"
            lblresult.Text = getnum & " data(s) delete successfully..."
            BindGrid()
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

        'check for addable field
        If autonum = 0 Then
            pnlEdit.Visible = False
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

    Private Sub ShowMessage(ByVal message As String)

        Dim scriptString As String = "<script language=JavaScript>"
        scriptString += "alert('" + message + "');"
        scriptString += "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "ShowMessage", scriptString)

    End Sub

    Private Function ActionValidateEdit() As Boolean
        RecFound = False
        CountRecord = 0
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

    Private Function ActionValidateDelete() As Boolean

        RecFound = False
        For i = 0 To myGridView.Rows.Count - 1
            Dim chkDelete As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
            If chkDelete.Checked Then
                RecFound = True
                Exit For
            End If
        Next
        If RecFound = True Then
            Return True
        Else
            lblresult.Text = "Invalid action: No row selected for deleting..."
            Return False
        End If

    End Function

    Private Function ActionValidateFieldCompany() As Boolean
        If txtCompany_Profile_Code.Text <> "" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function ActionValidateFieldUserProfile() As Boolean
        If txtUSER_PROFILE_CODE.Text <> "" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function ActionValidateFieldTableProfile() As Boolean
        If txtTABLE_PROFILE_CODE.Text <> "" Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub CheckAll()
        For i = 0 To myGridView.Rows.Count - 1
            Dim chkDelete As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
            chkDelete.Checked = True
        Next
    End Sub

    Sub UncheckAll()
        For i = 0 To myGridView.Rows.Count - 1
            Dim chkDelete As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
            chkDelete.Checked = False
        Next
    End Sub

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
        imgbtnTABLE_FIELD_CODE2.Visible = False
        imgbtnTABLE_FIELD_CODE.Visible = True

        'get field position
        ssql = "exec sp_sa_get_fields_position '" & Form.ID & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        autonum = 0
        If myDS.Tables(0).Rows.Count > 0 Then
            For i = 0 To myDS.Tables(0).Rows.Count - 1

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "COMPANY_PROFILE_CODE" Then
                    If lblCompany_Profile_Code.Visible = True And txtCompany_Profile_Code.Visible = True Then
                        autonum += 1
                        imgCOMPANY_PROFILE_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgCOMPANY_PROFILE_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgCOMPANY_PROFILE_CODE.Style.Add("position", "absolute")
                        autonum += 1
                        lblCOMPANY_PROFILE_CODE.CssClass = "wordstyle12"
                        lblCOMPANY_PROFILE_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblCOMPANY_PROFILE_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblCOMPANY_PROFILE_CODE.Style.Add("position", "absolute")
                        autonum += 1
                        txtCOMPANY_PROFILE_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        txtCOMPANY_PROFILE_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        txtCOMPANY_PROFILE_CODE.Style.Add("position", "absolute")
                        txtCOMPANY_PROFILE_CODE.Width = txtWidth
                        autonum += 1
                        imgbtnCOMPANY_PROFILE_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnCOMPANY_PROFILE_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnCOMPANY_PROFILE_CODE.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "USER_PROFILE_CODE" Then
                    If lblUSER_PROFILE_CODE.Visible = True And txtUSER_PROFILE_CODE.Visible = True Then
                        autonum += 1
                        imgUSER_PROFILE_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgUSER_PROFILE_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgUSER_PROFILE_CODE.Style.Add("position", "absolute")
                        autonum += 1
                        lblUSER_PROFILE_CODE.CssClass = "wordstyle12"
                        lblUSER_PROFILE_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblUSER_PROFILE_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblUSER_PROFILE_CODE.Style.Add("position", "absolute")
                        autonum += 1
                        txtUSER_PROFILE_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        txtUSER_PROFILE_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        txtUSER_PROFILE_CODE.Style.Add("position", "absolute")
                        txtUSER_PROFILE_CODE.Width = txtWidth
                        autonum += 1
                        imgbtnUSER_PROFILE_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnUSER_PROFILE_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnUSER_PROFILE_CODE.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "TABLE_PROFILE_CODE" Then
                    If lblTABLE_PROFILE_CODE.Visible = True And txtTABLE_PROFILE_CODE.Visible = True Then
                        autonum += 1
                        imgTABLE_PROFILE_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgTABLE_PROFILE_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgTABLE_PROFILE_CODE.Style.Add("position", "absolute")
                        autonum += 1
                        lblTABLE_PROFILE_CODE.CssClass = "wordstyle12"
                        lblTABLE_PROFILE_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblTABLE_PROFILE_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblTABLE_PROFILE_CODE.Style.Add("position", "absolute")
                        autonum += 1
                        txtTABLE_PROFILE_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        txtTABLE_PROFILE_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        txtTABLE_PROFILE_CODE.Style.Add("position", "absolute")
                        txtTABLE_PROFILE_CODE.Width = txtWidth
                        autonum += 1
                        imgbtnTABLE_PROFILE_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnTABLE_PROFILE_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnTABLE_PROFILE_CODE.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "TABLE_FIELD_CODE" Then
                    If Session("action_edit") = "value" Then
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

                        imgTABLE_FIELD_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgTABLE_FIELD_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgTABLE_FIELD_CODE.Style.Add("position", "absolute")
                        autonum += 1
                        lblTABLE_FIELD_CODE.CssClass = "wordstyle12"
                        lblTABLE_FIELD_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblTABLE_FIELD_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblTABLE_FIELD_CODE.Style.Add("position", "absolute")
                        autonum += 1

                        txtTABLE_FIELD_CODE.Visible = False

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

                        imgbtnTABLE_FIELD_CODE2.Visible = True
                        imgbtnTABLE_FIELD_CODE.Visible = False
                        imgbtnTABLE_FIELD_CODE2.Style.Add("left", mySetting.GetObjPositionRL(EctWidth, 6, "X"))
                        imgbtnTABLE_FIELD_CODE2.Style.Add("top", mySetting.GetObjPositionRL(6, autonum2, "Y"))
                        imgbtnTABLE_FIELD_CODE2.Style.Add("position", "absolute")
                        autonum += 44
                    Else
                        If lblTABLE_FIELD_CODE.Visible = True Then
                            autonum += 1
                            imgTABLE_FIELD_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                            imgTABLE_FIELD_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                            imgTABLE_FIELD_CODE.Style.Add("position", "absolute")
                            autonum += 1
                            lblTABLE_FIELD_CODE.CssClass = "wordstyle12"
                            lblTABLE_FIELD_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                            lblTABLE_FIELD_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                            lblTABLE_FIELD_CODE.Style.Add("position", "absolute")
                            autonum += 1
                            txtTABLE_FIELD_CODE.Visible = True
                            txtTABLE_FIELD_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                            txtTABLE_FIELD_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                            txtTABLE_FIELD_CODE.Style.Add("position", "absolute")
                            txtTABLE_FIELD_CODE.Width = txtWidth
                            autonum += 1
                            imgbtnTABLE_FIELD_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                            imgbtnTABLE_FIELD_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                            imgbtnTABLE_FIELD_CODE.Style.Add("position", "absolute")
                        End If
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_PERMISSION_INSERT" Then
                    If lblOPTION_PERMISSION_INSERT.Visible = True And ddlOPTION_PERMISSION_INSERT.Visible = True Then
                        autonum += 1
                        imgOPTION_PERMISSION_INSERT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgOPTION_PERMISSION_INSERT.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgOPTION_PERMISSION_INSERT.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_PERMISSION_INSERT.CssClass = "wordstyle12"
                        lblOPTION_PERMISSION_INSERT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_PERMISSION_INSERT.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_PERMISSION_INSERT.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_PERMISSION_INSERT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_PERMISSION_INSERT.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_PERMISSION_INSERT.Style.Add("position", "absolute")
                        ddlOPTION_PERMISSION_INSERT.Width = ddlWidth
                        autonum += 1
                        imgbtnOPTION_PERMISSION_INSERT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_PERMISSION_INSERT.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_PERMISSION_INSERT.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_PERMISSION_MODIFY" Then
                    If lblOPTION_PERMISSION_MODIFY.Visible = True And ddlOPTION_PERMISSION_MODIFY.Visible = True Then
                        autonum += 1
                        imgOPTION_PERMISSION_MODIFY.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgOPTION_PERMISSION_MODIFY.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgOPTION_PERMISSION_MODIFY.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_PERMISSION_MODIFY.CssClass = "wordstyle12"
                        lblOPTION_PERMISSION_MODIFY.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_PERMISSION_MODIFY.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_PERMISSION_MODIFY.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_PERMISSION_MODIFY.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_PERMISSION_MODIFY.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_PERMISSION_MODIFY.Style.Add("position", "absolute")
                        ddlOPTION_PERMISSION_MODIFY.Width = ddlWidth
                        autonum += 1
                        imgbtnOPTION_PERMISSION_MODIFY.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_PERMISSION_MODIFY.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_PERMISSION_MODIFY.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_PERMISSION_DELETE" Then
                    If lblOPTION_PERMISSION_DELETE.Visible = True And ddlOPTION_PERMISSION_DELETE.Visible = True Then
                        autonum += 1
                        imgOPTION_PERMISSION_DELETE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgOPTION_PERMISSION_DELETE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgOPTION_PERMISSION_DELETE.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_PERMISSION_DELETE.CssClass = "wordstyle12"
                        lblOPTION_PERMISSION_DELETE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_PERMISSION_DELETE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_PERMISSION_DELETE.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_PERMISSION_DELETE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_PERMISSION_DELETE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_PERMISSION_DELETE.Style.Add("position", "absolute")
                        ddlOPTION_PERMISSION_DELETE.Width = ddlWidth
                        autonum += 1
                        imgbtnOPTION_PERMISSION_DELETE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_PERMISSION_DELETE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_PERMISSION_DELETE.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_PERMISSION_PREVIEW" Then
                    If lblOPTION_PERMISSION_PREVIEW.Visible = True And ddlOPTION_PERMISSION_PREVIEW.Visible = True Then
                        autonum += 1
                        imgOPTION_PERMISSION_PREVIEW.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgOPTION_PERMISSION_PREVIEW.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgOPTION_PERMISSION_PREVIEW.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_PERMISSION_PREVIEW.CssClass = "wordstyle12"
                        lblOPTION_PERMISSION_PREVIEW.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_PERMISSION_PREVIEW.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_PERMISSION_PREVIEW.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_PERMISSION_PREVIEW.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_PERMISSION_PREVIEW.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_PERMISSION_PREVIEW.Style.Add("position", "absolute")
                        ddlOPTION_PERMISSION_PREVIEW.Width = ddlWidth
                        autonum += 1
                        imgbtnOPTION_PERMISSION_PREVIEW.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_PERMISSION_PREVIEW.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_PERMISSION_PREVIEW.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_PERMISSION_PRINT" Then
                    If lblOPTION_PERMISSION_PRINT.Visible = True And ddlOPTION_PERMISSION_PRINT.Visible = True Then
                        autonum += 1
                        imgOPTION_PERMISSION_PRINT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgOPTION_PERMISSION_PRINT.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgOPTION_PERMISSION_PRINT.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_PERMISSION_PRINT.CssClass = "wordstyle12"
                        lblOPTION_PERMISSION_PRINT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_PERMISSION_PRINT.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_PERMISSION_PRINT.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_PERMISSION_PRINT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_PERMISSION_PRINT.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_PERMISSION_PRINT.Style.Add("position", "absolute")
                        ddlOPTION_PERMISSION_PRINT.Width = ddlWidth
                        autonum += 1
                        imgbtnOPTION_PERMISSION_PRINT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_PERMISSION_PRINT.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_PERMISSION_PRINT.Style.Add("position", "absolute")
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
            Dim itemCount As Integer = lstright.Items.Count
            For k = 0 To itemCount - 1
                myDS = mySQL.ExecuteSQL("Select Code,Option_Data_Type,[Name],Option_Primary_Key From Table_Field Where Table_Profile_Code='" & Form.ID & "' And Option_View_Card='YES' Order By Table_Profile_Code,Sequence_No", Session("Company").ToString, Session("EmpID").ToString)
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
                                        lblresult2.Text = Trim(myDS.Tables(0).Rows(i).Item(2)) & " field is required..."
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
                                        If UCase(myDS.Tables(0).Rows(i).Item(3).ToString) = "YES" Then
                                            ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "='" & Trim(myDropdownlist.SelectedValue) & "' And "
                                            ssql4 = ssql4 & myDS.Tables(0).Rows(i).Item(0) & "=" & Trim(myDropdownlist.SelectedValue) & ","
                                        End If
                                    Else
                                        lblresult2.Text = "Invalid selection for " & Trim(myDS.Tables(0).Rows(i).Item(2)) & " !"
                                        myDropdownlist.Focus()
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
                                        lblresult2.Text = Trim(myDS.Tables(0).Rows(i).Item(2)) & " field is required..."
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
                                        ssql1 = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                                        ssql2 = ssql2 & "'" & strDate & "',"
                                        If UCase(myDS.Tables(0).Rows(i).Item(3).ToString) = "YES" Then
                                            ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "='" & strDate & "' And "
                                            ssql4 = ssql4 & myDS.Tables(0).Rows(i).Item(0) & "=" & strDate & ","
                                        End If
                                    Else
                                        lblresult2.Text = "Invalid input format for " & Trim(myDS.Tables(0).Rows(i).Item(2)) & " !"
                                        myTextBox.Focus()
                                        Return False
                                    End If
                                End If

                            Case "DECIMAL", "INTEGER"
                                logic = False
                                Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                                Dim myImg As Image = Page.FindControl("img" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                                If myImg.Visible = True Then
                                    If mySetting.CheckTextNull(myTextBox.Text) = True Then
                                        logic = True
                                    Else
                                        lblresult2.Text = Trim(myDS.Tables(0).Rows(i).Item(2)) & " field is required..."
                                        myTextBox.Focus()
                                        Return False
                                    End If
                                Else
                                    logic = True
                                End If
                                If logic = True Then
                                    If IsNumeric(myTextBox.Text) Then
                                        ssql1 = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                                        ssql2 = ssql2 & "'" & myTextBox.Text.ToString & "',"
                                        If UCase(myDS.Tables(0).Rows(i).Item(3).ToString) = "YES" Then
                                            ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "='" & myTextBox.Text.ToString & "'And "
                                            ssql4 = ssql4 & myDS.Tables(0).Rows(i).Item(0) & "='" & myTextBox.Text.ToString & ","
                                        End If
                                    Else
                                        lblresult2.Text = "Invalid input format for " & Trim(myDS.Tables(0).Rows(i).Item(2)) & " !"
                                        myTextBox.Focus()
                                        Return False
                                    End If
                                End If

                            Case Else 'character & lookup
                                logic = False

                                If UCase(Trim(myDS.Tables(0).Rows(i).Item(0))) = "TABLE_FIELD_CODE" Then
                                    txtTABLE_FIELD_CODE.Text = lstright.Items(k).Value
                                End If

                                'for non last field
                                Dim myTextBox As TextBox = Page.FindControl("txt" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                                Dim myImg As Image = Page.FindControl("img" & Trim(myDS.Tables(0).Rows(i).Item(0)))
                                If myImg.Visible = True Then
                                    If mySetting.CheckTextNull(myTextBox.Text) = True Then
                                        logic = True
                                    Else
                                        lblresult2.Text = Trim(myDS.Tables(0).Rows(i).Item(2)) & " field is required..."
                                        myTextBox.Focus()
                                        Return False
                                    End If
                                Else
                                    logic = True
                                End If

                                If logic = True Then
                                    If mySetting.ValidateInput(myTextBox, myDS.Tables(0).Rows(i).Item(1).ToString) = True Then
                                        ssql1 = ssql1 & myDS.Tables(0).Rows(i).Item(0) & ","
                                        Dim myDST As New DataSet
                                        ssql5 = "Select Function_Name,Default_Value,Default_Value2,Default_Value3 From User_Define_Function Where Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "' And Table_Profile_Code='" & Form.ID & "' And Table_Field_Code='" & myDS.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='UPDATE'"
                                        myDST = mySQL.ExecuteSQL(ssql5, Session("Company").ToString, Session("EmpID").ToString)
                                        If myDST.Tables(0).Rows.Count > 0 Then
                                            If myDST.Tables(0).Rows(0).Item(3).ToString.Trim <> "" Then
                                                ssql2 = ssql2 & "ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString & "',N'" & myDST.Tables(0).Rows(0).Item(2).ToString & "',N'" & myDST.Tables(0).Rows(0).Item(3).ToString & "'),''),"
                                                If UCase(myDS.Tables(0).Rows(i).Item(3).ToString) = "YES" Then
                                                    ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString & "',N'" & myDST.Tables(0).Rows(0).Item(2).ToString & "',N'" & myDST.Tables(0).Rows(0).Item(3).ToString & "'),'') And "
                                                    ssql4 = ssql4 & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString & "',N'" & myDST.Tables(0).Rows(0).Item(2).ToString & "',N'" & myDST.Tables(0).Rows(0).Item(3).ToString & "'),''),"
                                                End If
                                            ElseIf myDST.Tables(0).Rows(0).Item(2).ToString.Trim <> "" Then
                                                ssql2 = ssql2 & "ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString & "',N'" & myDST.Tables(0).Rows(0).Item(2).ToString & "'),''),"
                                                If UCase(myDS.Tables(0).Rows(i).Item(3).ToString) = "YES" Then
                                                    ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString & "',N'" & myDST.Tables(0).Rows(0).Item(2).ToString & "'),'') And "
                                                    ssql4 = ssql4 & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString & "',N'" & myDST.Tables(0).Rows(0).Item(2).ToString & "'),''),"
                                                End If
                                            ElseIf myDST.Tables(0).Rows(0).Item(1).ToString.Trim <> "" Then
                                                ssql2 = ssql2 & "ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString & "'),''),"
                                                If UCase(myDS.Tables(0).Rows(i).Item(3).ToString) = "YES" Then
                                                    ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString & "'),'') And "
                                                    ssql4 = ssql4 & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString & "'),''),"
                                                End If

                                            Else
                                                ssql2 = ssql2 & "ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "'),''),"
                                                If UCase(myDS.Tables(0).Rows(i).Item(3).ToString) = "YES" Then
                                                    ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "'),'') And "
                                                    ssql4 = ssql4 & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & myTextBox.Text.ToString & "'),''),"
                                                End If
                                            End If
                                        Else
                                            ssql2 = ssql2 & "ISNULL(N'" & Trim(myTextBox.Text.ToString) & "',''),"
                                            If UCase(myDS.Tables(0).Rows(i).Item(3).ToString) = "YES" Then
                                                ssql3 = ssql3 & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(N'" & Trim(myTextBox.Text.ToString) & "','') And "
                                                ssql4 = ssql4 & myDS.Tables(0).Rows(i).Item(0) & "=ISNULL(N'" & Trim(myTextBox.Text.ToString) & "',''),"
                                            End If
                                        End If
                                        myDST = Nothing
                                    Else
                                        lblresult2.Text = "Invalid input format for " & Trim(myDS.Tables(0).Rows(i).Item(2)) & " !"
                                        myTextBox.Focus()
                                        Return False
                                    End If
                                End If

                        End Select
                    Next

                    'add or update selected items
                    If lstright.Items.Count <> 0 Then
                        ssql3 = ssql6 & Left(ssql3, Len(ssql3) - 5)
                        mySQL.ExecuteSQL(ssql3, Session("Company").ToString, Session("EmpID").ToString)
                        ssql = Left(ssql1, Len(ssql1) - 1) & Left(ssql2, Len(ssql2) - 1) & ")"
                        mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                    Else
                        ssql3 = ssql6 & Left(ssql3, Len(ssql3) - 5)
                        mySQL.ExecuteSQL(ssql3, Session("Company").ToString, Session("EmpID").ToString)
                    End If

                    myDS = Nothing
                End If

            Next 'loop for lst

            Return True
        Catch ex As Exception
            lblresult2.Text = "[ValidateInsertUpdate]Error: " & ex.Message
            myDS = Nothing
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
        Dim htbFilterCriteria As New Hashtable
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
                                ssql3 = ssql3 & Trim(myTxt.Text.ToString) & "@@"
                                htbFilterCriteria.Add(myDR(0).ToString.Trim, myTxt.Text.ToString.Trim.Replace("'", "''"))
                            End If
                            tmpssql = ""
                            myTxt = Nothing
                        Case "LOOKUP", "CHARACTER"
                                Dim myTxt As TextBox = Page.FindControl("txt" & myDR(0).ToString)
                                If myTxt.Visible = True And Trim(myTxt.Text) <> "" Then
                                    ssql = ssql & Form.ID & "_Card_Vw" & "@@"
                                    ssql2 = ssql2 & myDR(0).ToString & "@@"
                                    ssql3 = ssql3 & Trim(myTxt.Text.ToString) & "@@"
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

                If ssql <> "" And ssql2 <> "" And ssql3 <> "" Then
                    Session("ssql1") = ssql
                    Session("ssql2") = ssql2
                    Session("ssql3") = ssql3
                    Session("action") = "search"

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
