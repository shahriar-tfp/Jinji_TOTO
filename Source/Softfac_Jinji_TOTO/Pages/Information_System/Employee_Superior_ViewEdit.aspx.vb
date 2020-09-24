Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.web.Configuration

Partial Class Pages_InformationSystem_Employee_Superior_ViewEdit
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
        Session("Module") = "INFORMATION_SYSTEM"
        Session("action") = ""
        ClearText()
        SetFieldToTrue()
        SetVisible()
        invisibleLst()
        lstleft.Items.Clear()
        lstright.Items.Clear()
        Session("action_edit") = "no-value"
        AutoAdjustPosition("2")
        imgBtnUpdate.CssClass = buttonPosition1
        imgBtnCancel.CssClass = buttonPosition2

        imgKeyGROUP_CODE.Enabled = True
        lblGROUP_CODE.Enabled = True

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

        mySetting.GetBtnImgUrl(imgBtnUpdate, Session("Company").ToString, btnColourDef, "btnUpdate.png")
        mySetting.GetBtnImgUrl(imgBtnCancel, Session("Company").ToString, btnColourDef, "btnCancel.png")

        mySetting.GetBtnImgUrl(imgBtnAddAll, Session("Company").ToString, btnColourDef, "addall.png")
        mySetting.GetBtnImgUrl(imgBtnAddItem, Session("Company").ToString, btnColourDef, "additem.png")
        mySetting.GetBtnImgUrl(imgBtnRemoveAll, Session("Company").ToString, btnColourDef, "removeall.png")
        mySetting.GetBtnImgUrl(imgBtnRemoveItem, Session("Company").ToString, btnColourDef, "removeitem.png")
        mySetting.GetImgBtnUrl(imgbtnGROUP_CODE2, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
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

                imgBtnUpdate.Visible = AllowUpdate
                imgBtnCancel.Visible = AllowView

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
                                mySetting.GetLookupValue_ImageButton(myImageButton, Form.ID, "txt" & myDT2.Rows(i).Item(2).ToString, "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & myDT2.Rows(i).Item(2).ToString & """," & """" & """")
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
            lblresult2.Text = "[Field Setting Error]: No setting found for this page!"
            Exit Sub
        End If

    End Sub

#End Region

#Region "Panel Edit"

    Protected Sub imgbtnGROUP_CODE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnGROUP_CODE.Click

        If Session("action") <> "filter" Then
            'If ActionValidateFieldCompany() = False Then
            '    lblresult2.Text = lblCOMPANY_PROFILE_CODE.Text & " field is required..."
            '    Exit Sub
            'End If
            visibleLst()
            lstleft.Items.Clear()
            lstright.Items.Clear()
            txtGROUP_CODE.Text = ""
            Session("action_edit") = "value"
            AutoAdjustPosition("2")
            imgBtnUpdate.CssClass = buttonPosition1
            imgBtnCancel.CssClass = buttonPosition2
        End If
        lblresult2.Text = ""

    End Sub

    Protected Sub ddlOPTION_BYLIST_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOPTION_BYLIST.SelectedIndexChanged
        lblEMPLOYEE_PROFILE_ID.Text = ddlOPTION_BYLIST.SelectedValue & " ID"
        lblEMPLOYEE_PROFILE_ID_VALUE.Text = "CHANGE TO NEW " & ddlOPTION_BYLIST.SelectedItem.ToString & " ID"
        If ddlOPTION_BYLIST.SelectedValue = "SUPERVISOR" Or ddlOPTION_BYLIST.SelectedValue = "PIC" Or ddlOPTION_BYLIST.SelectedValue = "BEHALF" Then
            ddlOPTION_TYPE.SelectedValue = ddlOPTION_BYLIST.SelectedValue
            ddlOPTION_TYPE.Enabled = False
            lblGROUP_CODE.Text = "Employee ID"
        Else
            ddlOPTION_TYPE.Enabled = True
            lblGROUP_CODE.Text = ddlOPTION_TYPE.SelectedItem.ToString & " ID"
        End If
    End Sub

    Protected Sub ddlOPTION_TYPE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOPTION_TYPE.SelectedIndexChanged
        If ddlOPTION_TYPE.Text = "" Then
            lblGROUP_CODE.Text = "Employee ID"
        Else
            lblGROUP_CODE.Text = ddlOPTION_TYPE.SelectedItem.ToString & " ID"
        End If
    End Sub


    Protected Sub txtEMPLOYEE_PROFILE_ID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEMPLOYEE_PROFILE_ID.TextChanged
        txtEMPLOYEE_PROFILE_ID_VALUE.Text = txtEMPLOYEE_PROFILE_ID.Text
    End Sub

    Protected Sub imgbtnGROUP_CODE2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnGROUP_CODE2.Click

        lstleft.Items.Clear()
        lstright.Items.Clear()
        If Validatefield() Then
            ssql2 = "select dbo.fn_GetEmpIDUsingCodeName(N'" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "')"
            myDS1 = mySQL.ExecuteSQL(ssql2, Session("Company").ToString, Session("EmpID").ToString)
            If myDS1.Tables(0).Rows.Count > 0 Then
                ssql = "Exec sp_sa_getListBoxValue 'Superior','" & Session("Company") & "','" & ddlOPTION_BYLIST.SelectedValue & "','" & myDS1.Tables(0).Rows(0).Item(0).ToString & "','" & ddlOPTION_TYPE.SelectedValue & "','','',''"
                myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                If myDS.Tables(0).Rows.Count > 0 Then
                    lstleft.DataSource = myDS.Tables(0)
                    lstleft.DataTextField = "Name"
                    lstleft.DataValueField = "Code"
                    lstleft.DataBind()
                    lstright.DataSource = myDS.Tables(1)
                    lstright.DataTextField = "Name"
                    lstright.DataValueField = "Code"
                    lstright.DataBind()
                End If
            End If

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
        imgbtnGROUP_CODE2.Visible = False
        imgbtnGROUP_CODE.Visible = True

        'get field position
        ssql = "exec sp_sa_get_fields_position '" & Form.ID & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        autonum = 0
        If myDS.Tables(0).Rows.Count > 0 Then
            For i = 0 To myDS.Tables(0).Rows.Count - 1

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_BYLIST" Then
                    If lblOPTION_BYLIST.Visible = True And ddlOPTION_BYLIST.Visible = True Then
                        autonum += 1
                        imgKeyOPTION_BYLIST.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyOPTION_BYLIST.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyOPTION_BYLIST.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_BYLIST.CssClass = "wordstyle12"
                        lblOPTION_BYLIST.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_BYLIST.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_BYLIST.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_BYLIST.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_BYLIST.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_BYLIST.Style.Add("position", "absolute")
                        ddlOPTION_BYLIST.Width = ddlWidth
                        autonum += 1
                        imgbtnOPTION_BYLIST.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_BYLIST.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_BYLIST.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "EMPLOYEE_PROFILE_ID" Then
                    If lblEMPLOYEE_PROFILE_ID.Visible = True And txtEMPLOYEE_PROFILE_ID.Visible = True Then
                        autonum += 1
                        imgKeyEMPLOYEE_PROFILE_ID.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyEMPLOYEE_PROFILE_ID.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyEMPLOYEE_PROFILE_ID.Style.Add("position", "absolute")
                        autonum += 1
                        lblEMPLOYEE_PROFILE_ID.CssClass = "wordstyle12"
                        lblEMPLOYEE_PROFILE_ID.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblEMPLOYEE_PROFILE_ID.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblEMPLOYEE_PROFILE_ID.Style.Add("position", "absolute")
                        autonum += 1
                        txtEMPLOYEE_PROFILE_ID.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        txtEMPLOYEE_PROFILE_ID.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        txtEMPLOYEE_PROFILE_ID.Style.Add("position", "absolute")
                        txtEMPLOYEE_PROFILE_ID.Width = txtWidth
                        autonum += 1
                        imgbtnEMPLOYEE_PROFILE_ID.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnEMPLOYEE_PROFILE_ID.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnEMPLOYEE_PROFILE_ID.Style.Add("position", "absolute")
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
                        ddlOPTION_TYPE.Width = ddlWidth
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

                        imgKeyGROUP_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyGROUP_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyGROUP_CODE.Style.Add("position", "absolute")
                        autonum += 1
                        lblGROUP_CODE.CssClass = "wordstyle12"
                        lblGROUP_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblGROUP_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblGROUP_CODE.Style.Add("position", "absolute")
                        autonum += 1

                        txtGROUP_CODE.Visible = False

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

                        imgbtnGROUP_CODE2.Visible = True
                        imgbtnGROUP_CODE.Visible = False
                        imgbtnGROUP_CODE2.Style.Add("left", mySetting.GetObjPositionRL(EctWidth, 6, "X"))
                        imgbtnGROUP_CODE2.Style.Add("top", mySetting.GetObjPositionRL(6, autonum2, "Y"))
                        imgbtnGROUP_CODE2.Style.Add("position", "absolute")
                        autonum += 44
                    Else
                        If lblGROUP_CODE.Visible = True Then
                            autonum += 1
                            imgKeyGROUP_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                            imgKeyGROUP_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                            imgKeyGROUP_CODE.Style.Add("position", "absolute")
                            autonum += 1
                            lblGROUP_CODE.CssClass = "wordstyle12"
                            lblGROUP_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                            lblGROUP_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                            lblGROUP_CODE.Style.Add("position", "absolute")
                            autonum += 1
                            txtGROUP_CODE.Visible = True
                            txtGROUP_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                            txtGROUP_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                            txtGROUP_CODE.Style.Add("position", "absolute")
                            txtGROUP_CODE.Width = txtWidth
                            autonum += 1
                            imgbtnGROUP_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                            imgbtnGROUP_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                            imgbtnGROUP_CODE.Style.Add("position", "absolute")
                        End If
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "EMPLOYEE_PROFILE_ID_VALUE" Then
                    If lblEMPLOYEE_PROFILE_ID_VALUE.Visible = True And txtEMPLOYEE_PROFILE_ID_VALUE.Visible = True Then
                        autonum += 1
                        imgKeyEMPLOYEE_PROFILE_ID_VALUE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyEMPLOYEE_PROFILE_ID_VALUE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyEMPLOYEE_PROFILE_ID_VALUE.Style.Add("position", "absolute")
                        autonum += 1
                        lblEMPLOYEE_PROFILE_ID_VALUE.CssClass = "wordstyle12"
                        lblEMPLOYEE_PROFILE_ID_VALUE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblEMPLOYEE_PROFILE_ID_VALUE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblEMPLOYEE_PROFILE_ID_VALUE.Style.Add("position", "absolute")
                        autonum += 1
                        txtEMPLOYEE_PROFILE_ID_VALUE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        txtEMPLOYEE_PROFILE_ID_VALUE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        txtEMPLOYEE_PROFILE_ID_VALUE.Style.Add("position", "absolute")
                        txtEMPLOYEE_PROFILE_ID_VALUE.Width = txtWidth
                        autonum += 1
                        imgbtnEMPLOYEE_PROFILE_ID_VALUE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnEMPLOYEE_PROFILE_ID_VALUE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnEMPLOYEE_PROFILE_ID_VALUE.Style.Add("position", "absolute")
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
        lblresult2.Text = "Click on Cancel Button to Clear Data..."
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

    Private Function Validatefield() As Boolean
        Validatefield = False

        If ddlOPTION_BYLIST.SelectedValue = "" Then
            lblresult2.Text = lblOPTION_BYLIST.Text & " field is required..."
            Exit Function
        ElseIf txtEMPLOYEE_PROFILE_ID.Text = "" Then
            lblresult2.Text = lblEMPLOYEE_PROFILE_ID.Text & " field is required..."
            Exit Function
        ElseIf ddlOPTION_TYPE.Text = "" Then
            lblresult2.Text = lblOPTION_TYPE.Text & " field is required..."
            Exit Function
        End If
        Validatefield = True
    End Function

#End Region

    Protected Sub imgBtnCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnCancel.Click
        Session("FilterField") = ""
        Session("FilterCriteria") = ""
        Session("action") = ""
        ClearText()
        SetFieldToTrue()
        SetVisible()
        invisibleLst()
        lstleft.Items.Clear()
        lstright.Items.Clear()
        Session("action_edit") = "no-value"
        AutoAdjustPosition("2")
        imgBtnUpdate.CssClass = buttonPosition1
        imgBtnCancel.CssClass = buttonPosition2
    End Sub


    Protected Sub imgBtnUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnUpdate.Click
        Dim itemCount As Integer = lstright.Items.Count
        Dim spid As String = mySQL.GetSPID
        If Validatefield() Then
            If lstleft.Visible = True Then
                If lstright.Visible = True And lstright.Items.Count > 0 Then
                    ssql1 = "Select dbo.fn_GetEmpIDUsingCodeName(N'" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "'),dbo.fn_GetEmpIDUsingCodeName(N'" & txtEMPLOYEE_PROFILE_ID_VALUE.Text.Replace("'", "''") & "')"
                    myDS1 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
                    If myDS1.Tables(0).Rows.Count > 0 Then

                        ssql2 = "exec Sp_Rpt_InsDelReportControlTemp '" & Session("Company").ToString & "','','" & spid & "','DEL','" & Session("EmpID").ToString & "'"
                        myDS2 = New DataSet()
                        myDS2 = mySQL.ExecuteSQL(ssql1)

                        myDS2 = Nothing
                        ssql2 = Nothing
                        For k = 0 To itemCount - 1
                            ssql2 = "exec Sp_Rpt_InsDelReportControlTemp '" & Session("Company").ToString & "','" & lstright.Items(k).Value & "','" & spid & "','ADD','" & Session("EmpID").ToString & "'"
                            myDS2 = New DataSet()
                            myDS2 = mySQL.ExecuteSQL(ssql2)

                            myDS2 = Nothing
                            ssql2 = Nothing
                        Next
                        ssql = "Exec sp_is_insupddelempSuperior N'UPD',N'" & Session("Company").ToString & "',N'" & myDS1.Tables(0).Rows(0).Item(0).ToString & "',N'" & ddlOPTION_BYLIST.SelectedValue & "','" & ddlOPTION_TYPE.SelectedValue & "','" & myDS1.Tables(0).Rows(0).Item(1).ToString & "'," & spid & ",'" & Session("EmpID").ToString & "'"
                        mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                        ssql2 = "exec Sp_Rpt_InsDelReportControlTemp '" & Session("Company").ToString & "','','" & spid & "','DEL','" & Session("EmpID").ToString & "'"
                        myDS2 = New DataSet()
                        myDS2 = mySQL.ExecuteSQL(ssql2)
                        ProcessCompleted()
                        imgBtnCancel_Click(Nothing, Nothing)
                    End If
                Else
                    lblresult2.Text = lblGROUP_CODE.Text & " Selected List is required..."
                End If
            Else
                lblresult2.Text = lblGROUP_CODE.Text & " field is required..."
            End If
        End If
    End Sub
    Private Sub ProcessCompleted()
        'txtDateStart.Text = ""
        'txtDateEnd.Text = ""
        'chkOverwrite.Checked = False

        Dim strScript As String = "<script language=" & """" & "javascript" & """" & ">"
        strScript &= "alert('Update Completed!');"
        strScript &= "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "Update Completed!", strScript)
    End Sub
End Class