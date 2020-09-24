Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.web.Configuration

Partial Class Pages_Reports_Report_IncrementLetter
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
            PagePreload()
            'BindGrid()
        End If

    End Sub

    Sub PagePreload()
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")
        Session("FilterField") = ""
        Session("FilterCriteria") = ""
        Session("Module") = "REPORTS"
        Session("action") = ""
        ClearText()
        SetFieldToTrue()
        SetVisible()
        invisibleLst()
        lstleft.Items.Clear()
        lstright.Items.Clear()
        Session("action_edit") = "no-value"
        AutoAdjustPosition("2")
        imgBtnPrint.CssClass = buttonPosition1

        pnlEdit.Visible = True

        'get Page Title
        ssql = "exec sp_sa_GetPageTitle '" & Form.ID & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            lblTitle.Text = myDS.Tables(0).Rows(0).Item(0)
            lblTitle.CssClass = "wordstyle4"
        End If
        myDS = Nothing

        'label for list box
        lbllstleft.Text = "Available List"
        lbllstright.Text = "Selected List"
        lbllstleft.CssClass = "wordstyle5"
        lbllstright.CssClass = "wordstyle6"

        mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourDef, "btnPrint.png")

        mySetting.GetBtnImgUrl(imgBtnAddAll, Session("Company").ToString, btnColourDef, "addall.png")
        mySetting.GetBtnImgUrl(imgBtnAddItem, Session("Company").ToString, btnColourDef, "additem.png")
        mySetting.GetBtnImgUrl(imgBtnRemoveAll, Session("Company").ToString, btnColourDef, "removeall.png")
        mySetting.GetBtnImgUrl(imgBtnRemoveItem, Session("Company").ToString, btnColourDef, "removeitem.png")
        'mySetting.GetImgBtnUrl(imgbtnGROUP_CODE2, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        'body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

        'lookup field value seting
        'ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "COMPANY_PROFILE_CODE" & """," & """" & """," & """" & Session("EmpID").ToString & """"
        'mySetting.GetLookupValue_ImageButton(imgbtnCOMPANY_PROFILE_CODE, Form.ID, "txtCOMPANY_PROFILE_CODE", "CodeName", ssql)

        mySetting.GetImgTypeUrl(imgTop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Filter.png")
        mySetting.GetImgTypeUrl(imgBottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")

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
                    pnlEdit.Visible = False
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

                imgBtnPrint.Visible = AllowPrint

            Else
                lblresult2.Text = "[Page Setting Error]: No setting found for this page!"
                Exit Sub
            End If

            If myDT2.Rows.Count > 0 Then

                For i = 0 To myDT2.Rows.Count - 1
                    Dim myLabel As Label = Page.FindControl("lbl" & myDT2.Rows(i).Item(2).ToString)
                    Dim myImageButton As ImageButton = Page.FindControl("imgBtn" & myDT2.Rows(i).Item(2).ToString)
                    Dim myImageKey As Image = Page.FindControl("imgKey" & myDT2.Rows(i).Item(2).ToString)

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

                Dim ddlMonthssql As String, myDS5 As DataSet

                ddlMonthssql = "Create Table #Result([Code] nvarchar(50), [Name] nvarchar(255)); Insert Into #Result Select '','';"
                ddlMonthssql = ddlMonthssql + "Insert Into #Result Select '01','JANUARY';Insert Into #Result Select '02','FEBRUARY';Insert Into #Result Select '03','MARCH';"
                ddlMonthssql = ddlMonthssql + "Insert Into #Result Select '04','APRIL';Insert Into #Result Select '05','MAY';Insert Into #Result Select '06','JUNE';"
                ddlMonthssql = ddlMonthssql + "Insert Into #Result Select '07','JULY';Insert Into #Result Select '08','AUGUST';Insert Into #Result Select '09','SEPTEMBER';"
                ddlMonthssql = ddlMonthssql + "Insert Into #Result Select '10','OCTOBER';Insert Into #Result Select '11','NOVEMBER';Insert Into #Result Select '12','DECEMBER';"
                ddlMonthssql = ddlMonthssql + " Select * From #Result; Drop Table #Result"
                myDS5 = mySQL.ExecuteSQL(ddlMonthssql)
                If myDS5.Tables.Count > 0 Then
                    If myDS5.Tables(0).Columns.Count > 1 Then
                        ddlOPTION_MONTH.DataTextField = "Name"
                        ddlOPTION_MONTH.DataValueField = "Code"
                        ddlOPTION_MONTH.DataSource = myDS5
                        ddlOPTION_MONTH.DataBind()
                    End If
                End If
                myDS5 = Nothing

                Dim ddlYearssql As String

                ddlYearssql = "Create Table #Result([Code] nvarchar(50), [Name] nvarchar(255)); Insert Into #Result Select '',''; Insert Into #Result Select year(getdate())-7,year(getdate())-7;Insert Into #Result Select year(getdate())-6,year(getdate())-6"
                ddlYearssql = ddlYearssql + "Insert Into #Result Select year(getdate())-5,year(getdate())-5;Insert Into #Result Select year(getdate())-4,year(getdate())-4;Insert Into #Result Select year(getdate())-3,year(getdate())-3;Insert Into #Result Select year(getdate())-2,year(getdate())-2;"
                ddlYearssql = ddlYearssql + "Insert Into #Result Select year(getdate())-1,year(getdate())-1;Insert Into #Result Select year(getdate()),year(getdate());Insert Into #Result Select year(getdate())+1,year(getdate())+1;"
                ddlYearssql = ddlYearssql + " Select * From #Result; Drop Table #Result"
                myDS5 = mySQL.ExecuteSQL(ddlYearssql)
                If myDS5.Tables.Count > 0 Then
                    If myDS5.Tables(0).Columns.Count > 1 Then
                        ddlOPTION_YEAR.DataTextField = "Name"
                        ddlOPTION_YEAR.DataValueField = "Code"
                        ddlOPTION_YEAR.DataSource = myDS5
                        ddlOPTION_YEAR.DataBind()
                    End If
                End If
                myDS5 = Nothing
            End If
            myDS = Nothing
            myDT1 = Nothing
            myDT2 = Nothing
        Else
            lblresult2.Text = "[Field Setting Error]: No setting found for this page!"
            Exit Sub
        End If

    End Sub

#End Region

#Region "Panel Edit"

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

#Region "Panel Action"

#End Region

#Region "Sub & Function"

    Private Sub ShowMessage(ByVal message As String)

        Dim scriptString As String = "<script language=JavaScript>"
        scriptString += "alert('" + message + "');"
        scriptString += "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "ShowMessage", scriptString)

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
                        Dim myImage As Image = Page.FindControl("imgKey" & Trim(myDS.Tables(1).Rows(i).Item(code)))
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
            lblresult2.Text = "[SetVisible]Error: " & ex.Message
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
        'imgbtnGROUP_CODE2.Visible = False
        'imgbtnGROUP_CODE.Visible = True

        'get field position
        ssql = "exec sp_sa_get_fields_position '" & Form.ID & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        autonum = 0
        If myDS.Tables(0).Rows.Count > 0 Then
            For i = 0 To myDS.Tables(0).Rows.Count - 1

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_YEAR" Then
                    If lblOPTION_YEAR.Visible = True And ddlOPTION_YEAR.Visible = True Then
                        autonum += 1
                        imgKeyOPTION_YEAR.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyOPTION_YEAR.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyOPTION_YEAR.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_YEAR.CssClass = "wordstyle12"
                        lblOPTION_YEAR.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_YEAR.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_YEAR.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_YEAR.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_YEAR.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_YEAR.Style.Add("position", "absolute")
                        ddlOPTION_YEAR.Width = txtWidth
                        autonum += 1
                        imgbtnOPTION_YEAR.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_YEAR.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_YEAR.Style.Add("position", "absolute")
                    End If
                End If
                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_MONTH" Then
                    If lblOPTION_MONTH.Visible = True And ddlOPTION_MONTH.Visible = True Then
                        autonum += 1
                        imgKeyOPTION_MONTH.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyOPTION_MONTH.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyOPTION_MONTH.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_MONTH.CssClass = "wordstyle12"
                        lblOPTION_MONTH.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_MONTH.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_MONTH.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_MONTH.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_MONTH.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_MONTH.Style.Add("position", "absolute")
                        ddlOPTION_MONTH.Width = txtWidth
                        autonum += 1
                        imgbtnOPTION_MONTH.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_MONTH.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_MONTH.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_TYPE" Then
                    If lblOPTION_TYPE.Visible = True And ddlOPTION_TYPE.Visible = True Then
                        autonum += 1
                        imgKeyOPTION_TYPE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyOPTION_TYPE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyOPTION_TYPE.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_TYPE.CssClass = "wordstyle12"
                        lblOPTION_TYPE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_TYPE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_TYPE.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_TYPE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_TYPE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_TYPE.Style.Add("position", "absolute")
                        ddlOPTION_TYPE.Width = txtWidth
                        autonum += 1
                        imgbtnOPTION_TYPE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_TYPE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_TYPE.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "GROUP_CODE" Then
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

                        autonum += 44
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
        imgTop.CssClass = "Display_0"
        imgBottom.CssClass = panelPosition & autonum

    End Sub

    Sub ClearText()

        Try
            Dim code As Integer = 2
            Dim dt As Integer = 6
            lblresult2.Text = ""

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
            lblresult2.Text = "[ClearText]Error: " & ex.Message
            SetFieldToFalse()
        End Try

    End Sub

    Sub SetFieldToTrue()

        Try
            Dim code As Integer = 2
            Dim dt As Integer = 6
            pnlEdit.Visible = True
            lblresult2.Visible = True

            myDS = mySetting.GetLabelDescription_(Form.ID)
            If CInt(myDS.Tables.Count) > 1 Then
                If CInt(myDS.Tables(1).Rows.Count) > 0 Then
                    For i = 0 To myDS.Tables(1).Rows.Count - 1
                        Dim myImage As Image = Page.FindControl("imgKey" & Trim(myDS.Tables(1).Rows(i).Item(code).ToString))
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
            lblresult2.Text = "[SetFieldToTrue]Error: " & ex.Message
            SetFieldToFalse()
        End Try

    End Sub

    Sub SetFieldToFalse()
        pnlEdit.Visible = True
    End Sub

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

#End Region

    Protected Sub imgBtnPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnPrint.Click
        Try
            Dim clsRpt As New clsReportServices, myTargetURL As String = ""
            Dim spid As String = mySQL.GetSPID
            'Dim itemCount As Integer = lstright.Items.Count
            myTargetURL = clsRpt.GetPDFReportURL()
            ssql = "select dbo.fn_GetEmpID(N'" & Session("EmpID").ToString & "'),'" & ddlOPTION_MONTH.Text & "','" & ddlOPTION_YEAR.Text & "',@@spid"
            myDS = New DataSet()
            myDS = mySQL.ExecuteSQL(ssql)
            If ValidatePrint(myDS) = True Then
                If myDS.Tables(0).Rows.Count = 1 Then
                    'For k = 0 To itemCount - 1
                    ssql1 = "exec Sp_Rpt_InsDelReportControlTemp '" & Session("Company").ToString & "','" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & spid & "','DEL','" & Session("EmpID").ToString & "'"
                    myDS1 = New DataSet()
                    myDS1 = mySQL.ExecuteSQL(ssql1)

                    myDS1 = Nothing
                    ssql1 = Nothing

                    ssql1 = "exec Sp_Rpt_InsDelReportControlTemp '" & Session("Company").ToString & "','" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & spid & "','ADD','" & Session("EmpID").ToString & "'"
                    myDS1 = New DataSet()
                    myDS1 = mySQL.ExecuteSQL(ssql1)

                    myDS1 = Nothing
                    ssql1 = Nothing
                    'Next
                    myTargetURL = myTargetURL & "&Company_Profile_Code=" & Session("Company").ToString & "&Month=" & myDS.Tables(0).Rows(0).Item(1).ToString & "&Year=" & myDS.Tables(0).Rows(0).Item(2).ToString & "&spid=" & spid & "&User_Profile_Code=" & Session("EmpID").ToString & "&rdTemplate=" & ddlOPTION_TYPE.SelectedValue

                    Response.Write("<script>window.open('" & myTargetURL & "');</script>")
                End If
            End If
            ssql = Nothing
            myDS = Nothing
        Catch ex As Exception
            lblresult2.Text = "[Print]Error: " & ex.Message
            myDS = Nothing
            myDS1 = Nothing
            ssql = Nothing
            ssql1 = Nothing
        End Try
    End Sub

    Private Function ValidatePrint(ByVal myds As DataSet) As Boolean
        If ddlOPTION_MONTH.SelectedValue = "" Then
            lblresult2.Text = "[" & lblOPTION_MONTH.Text & "] Is A Required Field!"
            ddlOPTION_MONTH.Focus()
            Return False
        ElseIf ddlOPTION_YEAR.SelectedValue = "" Then
            lblresult2.Text = "[" & lblOPTION_YEAR.Text & "] Is A Required Field!"
            ddlOPTION_YEAR.Focus()
            Return False
        Else
            Return True
        End If
    End Function
End Class