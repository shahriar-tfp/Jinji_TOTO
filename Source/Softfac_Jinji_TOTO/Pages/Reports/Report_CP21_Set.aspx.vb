Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class Pages_Reports_Report_CP21_Set

    Inherits System.Web.UI.Page
#Region "Public Declaration"
    Private WithEvents mySQL As New clsSQL, myDS As New DataSet, myDS1 As New DataSet, myDS2 As New DataSet, mySetting As New clsGlobalSetting
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Public ssql As String, i As Integer, j As Integer, k As Integer, SearchByPage As Boolean, RecFound As Boolean, CountRecord As Integer
    Public AllowView As Boolean, AllowInsert As Boolean, AllowUpdate As Boolean, AllowDelete As Boolean, AllowPrint As Boolean
    Public _currentPageNumber As Integer, _totalPage As Double = 1, _totalRecords As Integer
    Public myDE As New DictionaryEntry, myHT As Hashtable
    Public myDataType As New clsGlobalSetting.DataType, SearchByFilter As Boolean = False
    Dim intColumn As Integer
#End Region

#Region "Panel Edit"

    Protected Sub imgbtnGROUP_CODE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnGROUP_CODE.Click

        If Session("action") <> "filter" Then
            If ActionValidateFieldCompany() = False Then
                lblresult2.Text = lblCOMPANY_PROFILE_CODE.Text & " field is required..."
                Exit Sub
            End If
            If ActionValidateFieldGroup() = False Then
                lblresult2.Text = lblOPTION_FIELD.Text & " field is required..."
                Exit Sub
            End If
            visibleLst()
            lstleft.Items.Clear()
            lstright.Items.Clear()
            txtGROUP_CODE.Text = ""
            Session("action_edit") = "value"
            AutoAdjustPosition("2")
            imgBtnPrint.CssClass = buttonPosition1
        End If
        lblresult2.Text = ""

    End Sub

    Protected Sub imgbtnGROUP_CODE2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnGROUP_CODE2.Click

        lstleft.Items.Clear()
        lstright.Items.Clear()
        ssql = "Exec sp_sa_getListBoxValue 'cp21_set','" & Session("Company") & "','" & ddlOPTION_FIELD.SelectedValue & "','" & ddlOPTION_CATEGORIZE.SelectedValue & "','" & """" & "','','',''"

        Session("Option1") = ddlOPTION_FIELD.SelectedValue
        Session("Option2") = ddlOPTION_CATEGORIZE.SelectedValue

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
#Region "Private Declaration"
    Private WithEvents myDT1 As New DataTable, myDT2 As New DataTable
    Dim myDR1 As DataRow, myDR2 As DataRow, rcPerPage As Integer, autonum As Integer
    Dim ssql1 As String, ssql2 As String, ssql3 As String, ssql4 As String
    Dim rowPosition As String = "R_", rowPositionM As String = "RM_", panelPosition As String = "Display_"
    Dim buttonPosition1 As String = "B1_", buttonPosition2 As String = "B2_", buttonPosition3 As String = "B3_", labelPosition1 As String = "L1_"
    Dim btnColourDef, btnColourAlt As String
    Dim strPath As String = "../Images"
    Dim logic As Boolean
    Dim sysYear As Integer, counter As Integer
#End Region

    Private Function ActionValidateFieldCompany() As Boolean
        If txtCOMPANY_PROFILE_CODE.Text <> "" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function ActionValidateFieldGroup() As Boolean
        If ddlOPTION_FIELD.SelectedValue <> "" Then
            Return True
        Else
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



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.IsNewSession Then
            Response.Redirect("../Global/SessionTimeOut.aspx")
        End If
        If Not IsPostBack Then
            If Session("ScreenWidth") = 0 Then
                Session("ScreenWidth") = "1024"
                Session("GVwidth") = Session("ScreenWidth") - 360
            End If
            If Session("ScreenHeight") = 0 Then
                Session("ScreenHeight") = "768"
                Session("GVheight") = (Session("ScreenHeight") / 2) - 50
            End If
            'pnlGridview.Width = CInt(Session("GVwidth")) - 20
            'pnlGridview.Height = CInt(Session("GVheight"))

            PagePreload()
        End If
    End Sub

    Private Sub PagePreload()
        Session("Module") = "REPORTS"

        invisibleLst()
        lstleft.Items.Clear()
        lstright.Items.Clear()
        Session("action_edit") = "no-value"
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")

        mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourDef, "btnUpdate.png")


        mySetting.GetBtnImgUrl(imgBtnAddAll, Session("Company").ToString, btnColourDef, "addall.png")
        mySetting.GetBtnImgUrl(imgBtnAddItem, Session("Company").ToString, btnColourDef, "additem.png")
        mySetting.GetBtnImgUrl(imgBtnRemoveAll, Session("Company").ToString, btnColourDef, "removeall.png")
        mySetting.GetBtnImgUrl(imgBtnRemoveItem, Session("Company").ToString, btnColourDef, "removeitem.png")
        mySetting.GetImgBtnUrl(imgbtnGROUP_CODE2, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)


        mySetting.GetImgUrl(imgKeyCOMPANY_PROFILE_CODE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnCOMPANY_PROFILE_CODE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        '       mySetting.GetImgUrl(imgbtnOPTION_PAY_TYPE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)

        mySetting.GetImgTypeUrl(imgTop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Filter.png")
        mySetting.GetImgTypeUrl(imgBottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")

        'get Page Title
        ssql = "exec sp_sa_GetPageTitle '" & Form.ID & "'"
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            lblTitle.Text = myDS.Tables(0).Rows(0).Item(0)
            lblTitle.CssClass = "wordstyle4"
        End If
        myDS = Nothing

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
                    pnlEdit.Visible = False
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

                imgBtnPrint.Visible = AllowPrint
            End If

            If myDT2.Rows.Count > 0 Then
                For i = 0 To myDT2.Rows.Count - 1
                    Dim myLabel As Label = Page.FindControl("lbl" & myDT2.Rows(i).Item(2).ToString)
                    Dim myImageButton As ImageButton = Page.FindControl("imgbtn" & myDT2.Rows(i).Item(2).ToString)
                    Dim myImageKey As ImageButton = Page.FindControl("imgkey" & myDT2.Rows(i).Item(2).ToString)

                    myLabel.Text = myDT2.Rows(i).Item(3).ToString
                    myLabel.CssClass = "wordstyle12"
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
                                myImageButton.Visible = False
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
                            myImageButton.Visible = False
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
        End If

        AutoAdjustPosition("2")

        imgBtnPrint.CssClass = buttonPosition1


    End Sub

    Protected Sub imgBtnPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnPrint.Click

        If ((Session("Option1") = ddlOPTION_FIELD.SelectedValue) And (Session("Option2") = ddlOPTION_CATEGORIZE.SelectedValue)) Then
            Dim itemCount As Integer = lstright.Items.Count
            ssql = "select dbo.fn_ReturnCompanyProfileCode(N'" & txtCOMPANY_PROFILE_CODE.Text & "')"
            myDS = New DataSet()
            myDS = mySQL.ExecuteSQL(ssql)

            If ValidatePrint(myDS) = True Then
                ssql1 = "exec sp_pr_insReport_Field '" & Form.ID & "','" & myDS.Tables(0).Rows(0).Item(0).ToString & "','','" & ddlOPTION_FIELD.SelectedValue & "','" & ddlOPTION_CATEGORIZE.SelectedValue() & "','DEL'"
                myDS1 = New DataSet()
                myDS1 = mySQL.ExecuteSQL(ssql1)
                myDS1 = Nothing
                ssql1 = Nothing
                For k = 0 To itemCount - 1
                    ssql1 = "exec sp_pr_insReport_Field '" & Form.ID & "','" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & lstright.Items(k).Value & "','" & ddlOPTION_FIELD.SelectedValue & "','" & ddlOPTION_CATEGORIZE.SelectedValue() & "','DEL'"
                    myDS1 = New DataSet()
                    myDS1 = mySQL.ExecuteSQL(ssql1)
                    myDS1 = Nothing
                    ssql1 = Nothing
                Next

            End If
            ssql = Nothing
            myDS = Nothing
            lblresult2.Text = "Update completed !"
            
        Else
            lblresult2.Text = "You can't update."
        End If
        'myTargetURL = myTargetURL & "&OrganID=JAVA&month=1&year=2008&SPID=50&Type=ADD&paytype=MONTHLYt&userID=hrsa&rdTemplate=pr_GovEPF"
        'Response.Write("<script>window.open('" & myTargetURL & "');</script>")
    End Sub

    Private Sub ShowMessage(ByVal message As String)
        Dim scriptString As String = "<script language=JavaScript>"
        scriptString += "alert('" + message + "');"
        scriptString += "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "ShowMessage", scriptString)
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

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "COMPANY_PROFILE_CODE" Then
                    If lblCOMPANY_PROFILE_CODE.Visible = True And txtCOMPANY_PROFILE_CODE.Visible = True Then
                        autonum += 1
                        imgKeyCOMPANY_PROFILE_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyCOMPANY_PROFILE_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyCOMPANY_PROFILE_CODE.Style.Add("position", "absolute")
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


                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_FIELD" Then
                    If lblOPTION_FIELD.Visible = True And ddlOPTION_FIELD.Visible = True Then
                        autonum += 1
                        imgKeyOPTION_FIELD.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyOPTION_FIELD.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyOPTION_FIELD.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_FIELD.CssClass = "wordstyle12"
                        lblOPTION_FIELD.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_FIELD.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_FIELD.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_FIELD.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_FIELD.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_FIELD.Style.Add("position", "absolute")
                        ddlOPTION_FIELD.Width = ddlWidth
                        autonum += 1
                        imgbtnOPTION_FIELD.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_FIELD.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_FIELD.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_CATEGORIZE" Then
                    If lblOPTION_CATEGORIZE.Visible = True And ddlOPTION_CATEGORIZE.Visible = True Then
                        autonum += 1
                        imgKeyOPTION_CATEGORIZE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyOPTION_CATEGORIZE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyOPTION_CATEGORIZE.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_CATEGORIZE.CssClass = "wordstyle12"
                        lblOPTION_CATEGORIZE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_CATEGORIZE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_CATEGORIZE.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_CATEGORIZE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_CATEGORIZE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_CATEGORIZE.Style.Add("position", "absolute")
                        ddlOPTION_CATEGORIZE.Width = ddlWidth
                        autonum += 1
                        imgbtnOPTION_CATEGORIZE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_CATEGORIZE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_CATEGORIZE.Style.Add("position", "absolute")
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
        lblresult2.Text = ""
        imgTop.CssClass = "Display_0"
        imgBottom.CssClass = panelPosition & autonum

    End Sub

    Private Function ValidatePrint(ByVal myds As DataSet) As Boolean
        Return True
    End Function

End Class
