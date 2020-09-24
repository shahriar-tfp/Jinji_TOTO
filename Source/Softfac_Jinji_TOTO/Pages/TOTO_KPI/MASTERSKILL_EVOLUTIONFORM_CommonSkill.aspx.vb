Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Partial Class PAGES_TOTO_KPI_MASTERSKILL_EVOLUTIONFORM_CommonSkill
    Inherits System.Web.UI.Page

#Region "Public Declaration"
    Private WithEvents mySQL As New clsSQL, myDS, myDS1, myDS2, myDS3 As New DataSet, mySetting As New clsGlobalSetting, myMsg As New clsMessage
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Public ssql As String, i As Integer, j As Integer, k As Integer, SearchByPage As Boolean, RecFound As Boolean, CountRecord As Integer
    Public AllowView, AllowInsert, AllowUpdate, AllowDelete, AllowPrint As Boolean
    Public _currentPageNumber As Integer, _totalPage As Double = 1, _totalRecords As Integer
    Public myDE As New DictionaryEntry, myHT As Hashtable
    Public myDataType As New clsGlobalSetting.DataType
    Dim q As HttpRequest
    Dim n As NameValueCollection
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
            q = Request
            n = q.QueryString
            If (n.HasKeys()) Then
                txtEMPLOYEE_PROFILE_ID.Text = n.Get(0)
                txtEMPLOYEE_PROFILE_ID_TextChanged(Nothing, Nothing)
                ddlOPTION_YEAR.SelectedValue = n.Get(1)
                ddlEVALUATION.SelectedValue = n.Get(6)
                ddlEVALUATION_SelectedIndexChanged(Nothing, Nothing)
                ddlOPTION_LEVEL.SelectedValue = n.Get(2)
                ddlOPTION_LEVEL_SelectedIndexChanged(Nothing, Nothing)
                ddlOPTION_PROCESS.SelectedValue = n.Get(3)
                ddlOPTION_PROCESS_SelectedIndexChanged(Nothing, Nothing)
                ddlOPTION_CATEGORY.SelectedValue = n.Get(4)
                ddlOPTION_CATEGORY_SelectedIndexChanged(Nothing, Nothing)
                ddlGROUP_CODE.SelectedValue = n.Get(5)
                ddlGROUP_CODE_SelectedIndexChanged(Nothing, Nothing)
            End If
        End If
        If TimerH.Value <> "" Then
            lblTimer.Text = TimerH.Value
        End If

    End Sub

    Sub PagePreload()
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")
        Session("FilterField") = ""
        Session("FilterCriteria") = ""
        Session("Module") = "TOTO_KPI"
        Session("action") = ""
        ClearText()
        tblSpecialSkill.Visible = "false"
        tblSpecialSkill2.Visible = "false"
        lblSpecialHeader.Visible = "false"
        SetFieldToTrue()
        SetVisible()
        invisibleLst()
        lstleft.Items.Clear()
        lstright.Items.Clear()
        Session("action_edit") = "no-value"
        AutoAdjustPosition("2")
        imgBtnUpdate.CssClass = buttonPosition1
        imgBtnCancel.CssClass = buttonPosition2
        imgBtnDelete.CssClass = buttonPosition3

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

        Dim ssqlSpecial As String
        ssqlSpecial = "create table #result(code nvarchar(50),Name nvarchar(200));insert into #result select '','';insert into #result select 'PASS','Pass';insert into #result select 'FAIL','Fail'; select code, name from #result;"
        myDS = mySQL.ExecuteSQL(ssqlSpecial, Session("Company").ToString, Session("EmpID").ToString)

        If myDS.Tables.Count > 0 Then
            ddlSPSKILL_1.DataSource = myDS.Tables(0)
            ddlSPSKILL_1.DataTextField = "Name"
            ddlSPSKILL_1.DataValueField = "Code"
            ddlSPSKILL_1.DataBind()
            ddlSPSKILL_2.DataSource = myDS.Tables(0)
            ddlSPSKILL_2.DataTextField = "Name"
            ddlSPSKILL_2.DataValueField = "Code"
            ddlSPSKILL_2.DataBind()
            ddlSPSKILL_3.DataSource = myDS.Tables(0)
            ddlSPSKILL_3.DataTextField = "Name"
            ddlSPSKILL_3.DataValueField = "Code"
            ddlSPSKILL_3.DataBind()
            ddlSPSKILL_4.DataSource = myDS.Tables(0)
            ddlSPSKILL_4.DataTextField = "Name"
            ddlSPSKILL_4.DataValueField = "Code"
            ddlSPSKILL_4.DataBind()
            ddlSPSKILL_5.DataSource = myDS.Tables(0)
            ddlSPSKILL_5.DataTextField = "Name"
            ddlSPSKILL_5.DataValueField = "Code"
            ddlSPSKILL_5.DataBind()
            ddlSPSKILL_6.DataSource = myDS.Tables(0)
            ddlSPSKILL_6.DataTextField = "Name"
            ddlSPSKILL_6.DataValueField = "Code"
            ddlSPSKILL_6.DataBind()
            ddlSPSKILL_7.DataSource = myDS.Tables(0)
            ddlSPSKILL_7.DataTextField = "Name"
            ddlSPSKILL_7.DataValueField = "Code"
            ddlSPSKILL_7.DataBind()
            ddlSPSKILL_8.DataSource = myDS.Tables(0)
            ddlSPSKILL_8.DataTextField = "Name"
            ddlSPSKILL_8.DataValueField = "Code"
            ddlSPSKILL_8.DataBind()
            ddlSPSKILL_9.DataSource = myDS.Tables(0)
            ddlSPSKILL_9.DataTextField = "Name"
            ddlSPSKILL_9.DataValueField = "Code"
            ddlSPSKILL_9.DataBind()
            ddlSPSKILL_10.DataSource = myDS.Tables(0)
            ddlSPSKILL_10.DataTextField = "Name"
            ddlSPSKILL_10.DataValueField = "Code"
            ddlSPSKILL_10.DataBind()
        End If
        mySetting.GetBtnImgUrl(imgBtnUpdate, Session("Company").ToString, btnColourDef, "btnUpdate.png")
        mySetting.GetBtnImgUrl(imgBtnCancel, Session("Company").ToString, btnColourDef, "btnCancel.png")
        mySetting.GetBtnImgUrl(imgBtnDelete, Session("Company").ToString, btnColourDef, "btnDelete.png")

        mySetting.GetBtnImgUrl(imgBtnAddAll, Session("Company").ToString, btnColourDef, "addall.png")
        mySetting.GetBtnImgUrl(imgBtnAddItem, Session("Company").ToString, btnColourDef, "additem.png")
        mySetting.GetBtnImgUrl(imgBtnRemoveAll, Session("Company").ToString, btnColourDef, "removeall.png")
        mySetting.GetBtnImgUrl(imgBtnRemoveItem, Session("Company").ToString, btnColourDef, "removeitem.png")
        'body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

        'lookup field value seting
        'ssql = "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "COMPANY_PROFILE_CODE" & """," & """" & """," & """" & Session("EmpID").ToString & """"
        'mySetting.GetLookupValue_ImageButton(imgbtnCOMPANY_PROFILE_CODE, Form.ID, "txtCOMPANY_PROFILE_CODE", "CodeName", ssql)
        'mySetting.GetLookupValue_ImageButton(imgbtnGROUP_CODE, Form.ID, "txtGROUP_CODE", "CodeName", "exec sp_KPI_selLevelProcessCategory '" & myDS1.Tables(0).Rows(0).Item(0).ToString & "','" & ddlOPTION_LEVEL.SelectedValue & "','" & ddlOPTION_PROCESS.SelectedValue & "','" & ddlOPTION_CATEGORY.SelectedValue & "','MasterSkill'")
        mySetting.GetImgTypeUrl(imgTop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Filter.png")
        mySetting.GetImgTypeUrl(imgBottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")

        myDS = mySetting.GetPageFieldSetting(Session("Company"), Form.ID, Session("EmpID"))
        If myDS.Tables.Count > 1 Then
            myDT1 = myDS.Tables(0)
            myDT2 = myDS.Tables(1)

            Dim ddlMonthssql As String, myDS5 As DataSet

            ddlMonthssql = "Create Table #Result([Code] nvarchar(50), [Name] nvarchar(255)); Insert Into #Result Select '',''; "
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
                    ddlOPTION_MONTH.SelectedIndex = Month(Today())
                End If
            End If
            myDS5 = Nothing

            Dim ddlYearssql As String

            ddlYearssql = "Create Table #Result([Code] nvarchar(50), [Name] nvarchar(255)); Insert Into #Result Select '',''; Insert Into #Result Select year(getdate())-3,year(getdate())-3;Insert Into #Result Select year(getdate())-2,year(getdate())-2"
            ddlYearssql = ddlYearssql + "Insert Into #Result Select year(getdate())-1,year(getdate())-1;Insert Into #Result Select year(getdate()),year(getdate());Insert Into #Result Select year(getdate())+1,year(getdate())+1;"
            ddlYearssql = ddlYearssql + " Select * From #Result; Drop Table #Result"
            myDS5 = mySQL.ExecuteSQL(ddlYearssql)
            If myDS5.Tables.Count > 0 Then
                If myDS5.Tables(0).Columns.Count > 1 Then
                    ddlOPTION_YEAR.DataTextField = "Name"
                    ddlOPTION_YEAR.DataValueField = "Code"
                    ddlOPTION_YEAR.DataSource = myDS5
                    ddlOPTION_YEAR.DataBind()
                    ddlOPTION_YEAR.SelectedValue = Year(Today())
                End If
            End If
            myDS5 = Nothing


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
                                'Dim myDDL As DropDownList = Page.FindControl("ddl" & myDT2.Rows(i).Item(2).ToString)
                                'mySetting.GetDropdownlistValue(Form.ID, myDT2.Rows(i).Item(2).ToString, myDDL)
                                'myDDL = Nothing
                            Case "LOOKUP"
                                mySetting.GetLookupValue_ImageButton(myImageButton, Form.ID, "txt" & myDT2.Rows(i).Item(2).ToString, "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & myDT2.Rows(i).Item(2).ToString & """," & """" & """,""" & Session("EmpID").ToString & """")
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


    Protected Sub txtEMPLOYEE_PROFILE_ID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEMPLOYEE_PROFILE_ID.TextChanged
        If checkWeightage() Then
            bindStatus()
            bindCommonSkill()
            bindSpecialSkill()
            bindLevel()

            bindProcess()
            bindCategory()

        End If

    End Sub
    Protected Function checkWeightage() As Boolean
        ssql = "select employee_profile_id from Employee_CodeName_Vw where codename = '" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                ssql1 = "exec sp_KPI_chkWeightage '" & myDS.Tables(0).Rows(0).Item(0).ToString & "'," & ddlOPTION_YEAR.SelectedValue & ",'CHK'"
                myDS2 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
                If myDS2.Tables.Count > 0 Then
                    If myDS2.Tables(0).Rows.Count > 0 Then
                        If myDS2.Tables(0).Rows(0).Item(0).ToString = "ERROR" Then
                            lblresult2.Text = "Weightage for Special Skill is not fully filled up or not equal to 50%!"
                            ShowMessage(lblresult2.Text)
                            Return False
                            Exit Function
                        End If
                    End If
                End If
            End If
        End If
        Return True
    End Function
    Protected Sub calculateCommonSkill()
        Dim point1 As Double = 0.0
        Dim point2 As Double = 0.0
        Dim point3 As Double = 0.0
        Dim point4 As Double = 0.0
        Dim point5 As Double = 0.0
        Dim point6 As Double = 0.0
        Dim point7 As Double = 0.0
        Dim point8 As Double = 0.0
        Dim point9 As Double = 0.0
        Dim point10 As Double = 0.0
        Dim TotalSkill As Double = 0.0
        TotalSkill = CommonSkillCount.Value
        If IsNumeric(txtSkill1N.Text) = True Then
            point1 = point1 + CDbl(txtSkill1N.Text) * Skill1.Value
        End If
        If IsNumeric(txtSkill2N.Text) = True Then
            point2 = point2 + CDbl(txtSkill2N.Text) * Skill2.Value
        End If

        If IsNumeric(txtSkill3N.Text) = True Then
            point3 = point3 + CDbl(txtSkill3N.Text) * Skill3.Value
        End If

        If IsNumeric(txtSkill4N.Text) = True Then
            point4 = point4 + CDbl(txtSkill4N.Text) * Skill4.Value
        End If

        If IsNumeric(txtSkill5N.Text) = True Then
            point5 = point5 + CDbl(txtSkill5N.Text) * Skill5.Value
        End If

        If IsNumeric(txtSkill6N.Text) = True Then
            point6 = point6 + CDbl(txtSkill6N.Text) * Skill6.Value
        End If

        If IsNumeric(txtSkill7N.Text) = True Then
            point7 = point7 + CDbl(txtSkill7N.Text) * Skill7.Value
        End If

        If IsNumeric(txtSkill8N.Text) = True Then
            point8 = point8 + CDbl(txtSkill8N.Text) * Skill8.Value
        End If

        If IsNumeric(txtSkill9N.Text) = True Then
            point9 = point9 + CDbl(txtSkill9N.Text) * Skill9.Value
        End If

        If IsNumeric(txtSkill10N.Text) = True Then
            point10 = point10 + CDbl(txtSkill10N.Text) * Skill10.Value
        End If

        ssql2 = "select dbo.fn_GetEmpIDUsingCodeName(N'" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "')"
        myDS1 = mySQL.ExecuteSQL(ssql2, Session("Company").ToString, Session("EmpID").ToString)
        If myDS1.Tables(0).Rows.Count > 0 Then
            ssql = "Exec sp_KPI_selPoint '" & myDS1.Tables(0).Rows(0).Item(0).ToString & "','" & ddlGROUP_CODE.SelectedValue & "','" & ddlOPTION_YEAR.SelectedValue & "','CPOINT','','" & point1 & "','" & point2 & _
            "','" & point3 & "','" & point4 & "','" & point5 & "','" & point6 & "','" & point7 & "','" & point8 & "','" & point9 & "','" & point10 & "'"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables(0).Rows.Count > 0 Then
                If Convert.ToDecimal(myDS.Tables(0).Rows(0).Item(0).ToString) > 0 Then
                    txtCommonSkillN.Text = String.Format("{0:0.000}", Convert.ToDecimal(myDS.Tables(0).Rows(0).Item(0).ToString))
                Else
                    txtCommonSkillN.Text = ""
                End If
            End If
        End If

        If IsNumeric(txtSpecialSkillN.Text) = True And IsNumeric(txtCommonSkillN.Text) = True Then
            txtTotalN.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillN.Text) + Convert.ToDecimal(txtCommonSkillN.Text))
            txtSpecialSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillN.Text) - Convert.ToDecimal(txtSpecialSkillLY.Text))
            txtCommonSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtCommonSkillN.Text) - Convert.ToDecimal(txtCommonSkillLY.Text))
        ElseIf IsNumeric(txtSpecialSkillN.Text) = True And IsNumeric(txtCommonSkillN.Text) = False Then
            txtTotalN.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillN.Text))
            txtSpecialSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillN.Text) - Convert.ToDecimal(txtSpecialSkillLY.Text))
            txtCommonSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtCommonSkillC.Text) - Convert.ToDecimal(txtCommonSkillLY.Text))
        ElseIf IsNumeric(txtSpecialSkillN.Text) = False And IsNumeric(txtCommonSkillN.Text) = True Then
            txtTotalN.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtCommonSkillN.Text))
            txtCommonSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtCommonSkillN.Text) - Convert.ToDecimal(txtCommonSkillLY.Text))
            txtSpecialSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillC.Text) - Convert.ToDecimal(txtSpecialSkillLY.Text))
        Else
            txtTotalN.Text = "0.000"
            txtCommonSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtCommonSkillC.Text) - Convert.ToDecimal(txtCommonSkillLY.Text))
            txtSpecialSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillC.Text) - Convert.ToDecimal(txtSpecialSkillLY.Text))
        End If
        If Convert.ToDecimal(txtSpecialSkillLY.Text) = 0 Then
            txtSpecialSkillI.Text = "0.000"
        End If

        If Convert.ToDecimal(txtCommonSkillLY.Text) = 0 Then
            txtCommonSkillI.Text = "0.000"
        End If

        txtTotalI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillI.Text) + Convert.ToDecimal(txtCommonSkillI.Text))

        If Convert.ToDecimal(txtTotalI.Text) >= 0.13 Then
            If Jobgrade.Value = "C1" Or Jobgrade.Value = "C2" Or Jobgrade.Value = "C3" Or Jobgrade.Value = "C4" Or Jobgrade.Value = "M7" Or Jobgrade.Value = "M6" Or Jobgrade.Value = "M5" Or Jobgrade.Value = "M4" Then
                imgBtnUpdate.OnClientClick = "return confirm('Improvement point >0.13. Proceed?')"
            Else
                imgBtnUpdate.OnClientClick = ""
            End If

            txtTotalI.BackColor = Drawing.Color.Purple
            txtTotalI.ForeColor = Drawing.Color.White

        Else
            If Jobgrade.Value = "C1" Then
                If Convert.ToDecimal(txtTotalI.Text) >= 0.13 Then
                    txtTotalI.BackColor = Drawing.Color.Purple
                    txtTotalI.ForeColor = Drawing.Color.White
                    imgBtnUpdate.OnClientClick = "return confirm('Improvement point >=0.13. Proceed?')"
                Else
                    txtTotalI.BackColor = Drawing.Color.Empty
                    txtTotalI.ForeColor = Drawing.Color.Empty
                    imgBtnUpdate.OnClientClick = ""
                End If
            ElseIf Jobgrade.Value = "M6" Or Jobgrade.Value = "M5" Or Jobgrade.Value = "M4" Then
                If Convert.ToDecimal(txtTotalI.Text) >= 0.13 Then
                    txtTotalI.BackColor = Drawing.Color.Purple
                    txtTotalI.ForeColor = Drawing.Color.White
                    imgBtnUpdate.OnClientClick = "return confirm('Improvement point >=0.13. Proceed?')"
                Else
                    txtTotalI.BackColor = Drawing.Color.Empty
                    txtTotalI.ForeColor = Drawing.Color.Empty
                    imgBtnUpdate.OnClientClick = ""
                End If
            Else
                txtTotalI.BackColor = Drawing.Color.Empty
                txtTotalI.ForeColor = Drawing.Color.Empty
                imgBtnUpdate.OnClientClick = ""
            End If
        End If

        If Convert.ToDecimal(txtTotalN.Text) > Convert.ToDecimal(topoint.Value) And (Convert.ToDecimal(PromotionPeriod.Value) >= Convert.ToDecimal(retentionperiod.Value)) Then
            txtTotalN.BackColor = Drawing.Color.Green
            txtTotalN.ForeColor = Drawing.Color.White
            imgBtnUpdate.OnClientClick = "return confirm('Total Skill Map Point is entitle for Promotion. Proceed?')"
        ElseIf Convert.ToDecimal(txtTotalN.Text) > Convert.ToDecimal(topoint.Value) And (Convert.ToDecimal(PromotionPeriod.Value) >= Convert.ToDecimal(specialperiod.Value)) Then
            txtTotalN.BackColor = Drawing.Color.Yellow
            txtTotalN.ForeColor = Drawing.Color.Black
            imgBtnUpdate.OnClientClick = "return confirm('Total Skill Map Point is entitle for Promotion. Proceed?')"
        ElseIf Convert.ToDecimal(txtTotalN.Text) > Convert.ToDecimal(topoint.Value) And (Convert.ToDecimal(PromotionPeriod.Value) < Convert.ToDecimal(specialperiod.Value)) Then
            txtTotalN.BackColor = Drawing.Color.Red
            txtTotalN.ForeColor = Drawing.Color.White
            imgBtnUpdate.OnClientClick = ""
        Else
            txtTotalN.BackColor = Drawing.Color.Empty
            txtTotalN.ForeColor = Drawing.Color.Empty
            imgBtnUpdate.OnClientClick = ""
        End If

    End Sub

    Protected Sub bindCommonSkill()
        ssql = "select employee_profile_id from Employee_CodeName_Vw where codename = '" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                ssql1 = "exec sp_KPI_SelCommonSkill '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','COMMON'"
                myDS2 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)

                If myDS2.Tables.Count > 0 Then
                    If myDS2.Tables(0).Rows.Count > 0 Then
                        For i = 1 To 10
                            Dim myLabel As Label = Page.FindControl("lblSkill" & CStr(i) & "C")
                            Dim myTextbox1 As TextBox = Page.FindControl("txtSkill" & CStr(i) & "E")
                            Dim myTextbox2 As TextBox = Page.FindControl("txtSkill" & CStr(i) & "LY")
                            Dim myTextbox3 As TextBox = Page.FindControl("txtSkill" & CStr(i) & "C")
                            Dim myTextbox4 As TextBox = Page.FindControl("txtSkill" & CStr(i) & "N")

                            If myDS2.Tables(0).Rows(0).Item(i - 1).ToString = "" Then
                                myLabel.Visible = False
                                myTextbox1.Visible = False
                                myTextbox2.Visible = False
                                myTextbox3.Visible = False
                                myTextbox4.Visible = False
                            Else
                                myLabel.Visible = True
                                myLabel.Text = myDS2.Tables(0).Rows(0).Item(i - 1).ToString
                                myTextbox1.Visible = True
                                myTextbox1.Enabled = False
                                myTextbox2.Visible = True
                                myTextbox2.Enabled = False
                                myTextbox3.Visible = True
                                myTextbox3.Enabled = False
                                myTextbox4.Visible = True
                                myTextbox4.Enabled = False
                                myTextbox1.MaxLength = 5
                                CommonSkillCount.Value = i.ToString()
                            End If
                        Next
                    End If
                End If
                ssql1 = "exec sp_KPI_SelCommonSkill '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','WEIGHTAGE'"
                myDS2 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)

                If myDS2.Tables.Count > 0 Then
                    If myDS2.Tables(0).Rows.Count > 0 Then
                        For i = 1 To 10
                            Dim myHidden As HiddenField = Page.FindControl("Skill" & CStr(i))

                            If myDS2.Tables(0).Rows(0).Item(i - 1).ToString <> "" Then
                                myHidden.Value = myDS2.Tables(0).Rows(0).Item(i - 1).ToString
                            End If
                        Next
                        SpecialSkill1.Value = myDS2.Tables(0).Rows(0).Item(10).ToString
                    End If
                End If
            End If
        End If
    End Sub
    Protected Sub bindSpecialSkill()
        ssql = "select employee_profile_id from Employee_CodeName_Vw where codename = '" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                ssql1 = "exec sp_KPI_SelCommonSkill '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','Special'"
                myDS2 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)

                If myDS2.Tables.Count > 0 Then
                    If myDS2.Tables(0).Rows.Count > 0 Then
                        If myDS2.Tables(0).Rows(0).Item(0).ToString.ToUpper = "DIRECT" Then
                            tblSpecialSkill.Visible = "true"
                            tblSpecialSkill2.Visible = "false"
                            lblSpecialHeader.Visible = "true"
                            divButton.Style.Add("top", "550px")
                        Else
                            tblSpecialSkill.Visible = "false"
                            tblSpecialSkill2.Visible = "true"
                            lblSpecialHeader.Visible = "true"
                            divButton.Style.Add("top", "450px")
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Protected Sub ClearCommonSkill()
        For i = 1 To 10
            Dim myLabel As Label = Page.FindControl("lblSkill" & CStr(i) & "C")
            Dim myTextbox1 As TextBox = Page.FindControl("txtSkill" & CStr(i) & "E")
            Dim myTextbox2 As TextBox = Page.FindControl("txtSkill" & CStr(i) & "LY")
            Dim myTextbox3 As TextBox = Page.FindControl("txtSkill" & CStr(i) & "C")
            Dim myTextbox4 As TextBox = Page.FindControl("txtSkill" & CStr(i) & "N")
            Dim myDropDownList As DropDownList = Page.FindControl("ddlSPSKILL_" & CStr(i))
            Dim myHiddden As HiddenField = Page.FindControl("Skill" & CStr(i))

            myTextbox1.Text = ""
            myTextbox2.Text = ""
            myTextbox3.Text = ""
            myTextbox4.Text = ""
            myHiddden.Value = ""
            myDropDownList.SelectedIndex = -1
        Next
        SpecialSkill1.Value = ""
        txtSpecialSkillLY.Text = ""
        txtSpecialSkillC.Text = ""
        txtSpecialSkillN.Text = ""
        txtSpecialSkillI.Text = ""
        txtCommonSkillLY.Text = ""
        txtCommonSkillC.Text = ""
        txtCommonSkillN.Text = ""
        txtCommonSkillI.Text = ""
        txtTotalLY.Text = ""
        txtTotalC.Text = ""
        txtTotalN.Text = ""
        txtTotalI.Text = ""
        txtTotalI.BackColor = Drawing.Color.Empty
        txtTotalI.ForeColor = Drawing.Color.Empty
        txtTotalN.BackColor = Drawing.Color.Empty
        txtTotalN.ForeColor = Drawing.Color.Empty
        Jobgrade.Value = ""
        PromotionPeriod.Value = ""
        frompoint.Value = ""
        topoint.Value = ""
        retentionperiod.Value = ""
        specialperiod.Value = ""
        levelup.Value = ""
        txtSSkillE.Text = ""
        txtSSkillLY.Text = ""
        txtSSkillN.Text = ""
        txtSSkillC.Text = ""
    End Sub
    Protected Sub bindLevel()
        ssql = "select employee_profile_id from Employee_CodeName_Vw where codename = '" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                If ddlEVALUATION.SelectedIndex = -1 Then
                    ssql1 = "exec sp_KPI_selLevelProcessCategory '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & ddlOPTION_YEAR.SelectedValue & "','','','','LEVEL'"
                Else
                    ssql1 = "exec sp_KPI_selLevelProcessCategory '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & ddlOPTION_YEAR.SelectedValue & "','','','','LEVEL_1','" & ddlEVALUATION.SelectedValue & "'"
                End If

                myDS2 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)

                If myDS2.Tables.Count > 0 Then
                    If myDS2.Tables(0).Columns.Count > 1 Then
                        ddlOPTION_LEVEL.DataTextField = "Name"
                        ddlOPTION_LEVEL.DataValueField = "Code"
                        ddlOPTION_LEVEL.DataSource = myDS2
                        ddlOPTION_LEVEL.DataBind()
                    End If
                End If
                myDS2 = Nothing
            End If
        End If
    End Sub

    Protected Sub bindStatus()
        ssql = "select employee_profile_id from Employee_CodeName_Vw where codename = '" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                ssql1 = "exec sp_KPI_selLevelProcessCategory '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & ddlOPTION_YEAR.SelectedValue & "','','','','EVALUA'"
                myDS2 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)

                If myDS2.Tables.Count > 0 Then
                    If myDS2.Tables(0).Columns.Count > 1 Then
                        ddlEVALUATION.DataTextField = "Name"
                        ddlEVALUATION.DataValueField = "Code"
                        ddlEVALUATION.DataSource = myDS2
                        ddlEVALUATION.DataBind()
                    End If
                End If
                myDS2 = Nothing
            End If
        End If
    End Sub

    Protected Sub bindProcess()
        ssql = "select employee_profile_id from Employee_CodeName_Vw where codename = '" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                If ddlOPTION_LEVEL.SelectedIndex = -1 Then
                    ssql1 = "exec sp_KPI_selLevelProcessCategory '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & ddlOPTION_YEAR.SelectedValue & "','','','','Process'"
                Else
                    ssql1 = "exec sp_KPI_selLevelProcessCategory '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & ddlOPTION_YEAR.SelectedValue & "','" & ddlOPTION_LEVEL.SelectedValue & "','','','Process_1','" & ddlEVALUATION.SelectedValue & "'"
                End If

                myDS2 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)

                If myDS2.Tables.Count > 0 Then
                    If myDS2.Tables(0).Columns.Count > 1 Then
                        ddlOPTION_PROCESS.DataTextField = "Name"
                        ddlOPTION_PROCESS.DataValueField = "Code"
                        ddlOPTION_PROCESS.DataSource = myDS2
                        ddlOPTION_PROCESS.DataBind()
                    End If
                End If
                myDS2 = Nothing
            End If
        End If
    End Sub

    Protected Sub bindCategory()
        ssql = "select employee_profile_id from Employee_CodeName_Vw where codename = '" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                If ddlOPTION_LEVEL.SelectedIndex = -1 Then
                    ssql1 = "exec sp_KPI_selLevelProcessCategory '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & ddlOPTION_YEAR.SelectedValue & "','','','','Category'"
                ElseIf ddlOPTION_PROCESS.SelectedIndex = -1 Then
                    ssql1 = "exec sp_KPI_selLevelProcessCategory '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & ddlOPTION_YEAR.SelectedValue & "','" & ddlOPTION_LEVEL.SelectedValue & "','','','Category'"
                Else
                    ssql1 = "exec sp_KPI_selLevelProcessCategory '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & ddlOPTION_YEAR.SelectedValue & "','" & ddlOPTION_LEVEL.SelectedValue & "','" & ddlOPTION_PROCESS.SelectedValue & "','','Category_1','" & ddlEVALUATION.SelectedValue & "'"
                End If

                myDS2 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)

                If myDS2.Tables.Count > 0 Then
                    If myDS2.Tables(0).Columns.Count > 1 Then
                        ddlOPTION_CATEGORY.DataTextField = "Name"
                        ddlOPTION_CATEGORY.DataValueField = "Code"
                        ddlOPTION_CATEGORY.DataSource = myDS2
                        ddlOPTION_CATEGORY.DataBind()
                        ddlOPTION_CATEGORY_SelectedIndexChanged(Nothing, Nothing)
                    End If
                End If
                myDS2 = Nothing
            End If
        End If
    End Sub
    'Protected Sub imgbtnGROUP_CODE2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnGROUP_CODE2.Click

    '    lstleft.Items.Clear()
    '    lstright.Items.Clear()
    '    If Validatefield() Then
    '        ssql2 = "select dbo.fn_GetEmpIDUsingCodeName(N'" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "')"
    '        myDS1 = mySQL.ExecuteSQL(ssql2, Session("Company").ToString, Session("EmpID").ToString)
    '        If myDS1.Tables(0).Rows.Count > 0 Then
    '            ssql = "exec sp_KPI_selLevelProcessCategory '" & myDS1.Tables(0).Rows(0).Item(0).ToString & "','" & ddlOPTION_LEVEL.SelectedValue & "','" & ddlOPTION_PROCESS.SelectedValue & "','" & ddlOPTION_CATEGORY.SelectedValue & "','MasterSkill'"
    '            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
    '            If myDS.Tables(0).Rows.Count > 0 Or myDS.Tables(1).Rows.Count > 0 Then
    '                lstleft.DataSource = myDS.Tables(0)
    '                lstleft.DataTextField = "Name"
    '                lstleft.DataValueField = "Code"
    '                lstleft.DataBind()
    '                lstright.DataSource = myDS.Tables(1)
    '                lstright.DataTextField = "Name"
    '                lstright.DataValueField = "Code"
    '                lstright.DataBind()
    '            End If
    '        End If

    '    End If

    '    'disableTxt()
    '    'enableLst()
    '    lblresult2.Text = ""

    'End Sub

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

        'get field position
        ssql = "exec sp_sa_get_fields_position '" & Form.ID & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        autonum = 0
        If myDS.Tables(0).Rows.Count > 0 Then
            For i = 0 To myDS.Tables(0).Rows.Count - 1

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
                        ddlOPTION_YEAR.Width = ddlWidth
                        autonum += 1
                        imgbtnOPTION_YEAR.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_YEAR.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_YEAR.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_MONTH" Then
                    If lblOPTION_MONTH.Visible = True And ddlOPTION_MONTH.Visible = True Then
                        autonum += 1
                        imgkeyOPTION_MONTH.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgkeyOPTION_MONTH.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgkeyOPTION_MONTH.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_MONTH.CssClass = "wordstyle12"
                        lblOPTION_MONTH.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_MONTH.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_MONTH.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_MONTH.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_MONTH.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_MONTH.Style.Add("position", "absolute")
                        ddlOPTION_MONTH.Width = ddlWidth
                        autonum += 1
                        imgBtnOPTION_MONTH.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgBtnOPTION_MONTH.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgBtnOPTION_MONTH.Style.Add("position", "absolute")
                    End If
                End If
                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "EVALUATION" Then
                    If lblEVALUATION.Visible = True And ddlEVALUATION.Visible = True Then
                        autonum += 1
                        imgKeyEVALUATION.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyEVALUATION.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyEVALUATION.Style.Add("position", "absolute")
                        autonum += 1
                        lblEVALUATION.CssClass = "wordstyle12"
                        lblEVALUATION.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblEVALUATION.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblEVALUATION.Style.Add("position", "absolute")
                        autonum += 1
                        ddlEVALUATION.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlEVALUATION.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlEVALUATION.Style.Add("position", "absolute")
                        ddlEVALUATION.Width = ddlWidth
                        autonum += 1
                        imgbtnEVALUATION.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnEVALUATION.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnEVALUATION.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_LEVEL" Then
                    If lblOPTION_LEVEL.Visible = True And ddlOPTION_LEVEL.Visible = True Then
                        autonum += 1
                        imgKeyOPTION_LEVEL.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyOPTION_LEVEL.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyOPTION_LEVEL.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_LEVEL.CssClass = "wordstyle12"
                        lblOPTION_LEVEL.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_LEVEL.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_LEVEL.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_LEVEL.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_LEVEL.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_LEVEL.Style.Add("position", "absolute")
                        ddlOPTION_LEVEL.Width = ddlWidth
                        autonum += 1
                        imgbtnOPTION_LEVEL.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_LEVEL.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_LEVEL.Style.Add("position", "absolute")
                    End If
                End If


                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_PROCESS" Then
                    If lblOPTION_PROCESS.Visible = True And ddlOPTION_PROCESS.Visible = True Then
                        autonum += 1
                        imgKeyOPTION_PROCESS.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyOPTION_PROCESS.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyOPTION_PROCESS.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_PROCESS.CssClass = "wordstyle12"
                        lblOPTION_PROCESS.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_PROCESS.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_PROCESS.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_PROCESS.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_PROCESS.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_PROCESS.Style.Add("position", "absolute")
                        ddlOPTION_PROCESS.Width = ddlWidth
                        autonum += 1
                        imgbtnOPTION_PROCESS.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_PROCESS.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_PROCESS.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_CATEGORY" Then
                    If lblOPTION_CATEGORY.Visible = True And ddlOPTION_CATEGORY.Visible = True Then
                        autonum += 1
                        imgKeyOPTION_CATEGORY.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyOPTION_CATEGORY.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyOPTION_CATEGORY.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_CATEGORY.CssClass = "wordstyle12"
                        lblOPTION_CATEGORY.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_CATEGORY.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_CATEGORY.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_CATEGORY.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_CATEGORY.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_CATEGORY.Style.Add("position", "absolute")
                        ddlOPTION_CATEGORY.Width = ddlWidth
                        autonum += 1
                        imgbtnOPTION_CATEGORY.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_CATEGORY.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_CATEGORY.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "GROUP_CODE" Then
                    If lblGROUP_CODE.Visible = True And ddlGROUP_CODE.Visible = True Then
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
                        ddlGROUP_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlGROUP_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlGROUP_CODE.Style.Add("position", "absolute")
                        ddlGROUP_CODE.Width = ddlWidth
                        autonum += 1
                        imgbtnGROUP_CODE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnGROUP_CODE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnGROUP_CODE.Style.Add("position", "absolute")
                    End If
                End If
                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "WEIGHTAGE" Then
                    If lblWEIGHTAGE.Visible = True And txtWEIGHTAGE.Visible = True Then
                        autonum += 1
                        imgKeyWEIGHTAGE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyWEIGHTAGE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyWEIGHTAGE.Style.Add("position", "absolute")
                        autonum += 1
                        lblWEIGHTAGE.CssClass = "wordstyle12"
                        lblWEIGHTAGE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblWEIGHTAGE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblWEIGHTAGE.Style.Add("position", "absolute")
                        autonum += 1
                        txtWEIGHTAGE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        txtWEIGHTAGE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        txtWEIGHTAGE.Style.Add("position", "absolute")
                        txtWEIGHTAGE.Width = txtWidth
                        autonum += 1
                        imgbtnWEIGHTAGE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnWEIGHTAGE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnWEIGHTAGE.Style.Add("position", "absolute")
                        autonum += 4
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
    Private Function DeleteValidate() As Boolean
        DeleteValidate = False
        If ddlOPTION_LEVEL.SelectedValue = "" Then
            lblresult2.Text = lblOPTION_LEVEL.Text & " field is required..."
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf txtEMPLOYEE_PROFILE_ID.Text = "" Then
            lblresult2.Text = lblEMPLOYEE_PROFILE_ID.Text & " field is required..."
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf ddlGROUP_CODE.Text = "" Then
            lblresult2.Text = lblGROUP_CODE.Text & " field is required..."
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf ddlOPTION_YEAR.Text = "" Then
            lblresult2.Text = lblOPTION_YEAR.Text & " field is required..."
            ShowMessage(lblresult2.Text)
            Exit Function
        End If

        DeleteValidate = True
    End Function

    Private Function Validatefield() As Boolean
        Validatefield = False

        If ddlOPTION_LEVEL.SelectedValue = "" Then
            lblresult2.Text = lblOPTION_LEVEL.Text & " field is required..."
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf txtEMPLOYEE_PROFILE_ID.Text = "" Then
            lblresult2.Text = lblEMPLOYEE_PROFILE_ID.Text & " field is required..."
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf ddlGROUP_CODE.Text = "" Then
            lblresult2.Text = lblGROUP_CODE.Text & " field is required..."
            ShowMessage(lblresult2.Text)
            Exit Function
        Else
            Dim sqlChk As String
            Dim dsChk As DataSet

            sqlChk = "exec sp_KPI_ChkModify '" & Session("Company").ToString & "','" & ddlOPTION_YEAR.SelectedValue & "'"
            dsChk = mySQL.ExecuteSQL(sqlChk)

            If dsChk.Tables.Count > 0 Then
                If dsChk.Tables(0).Rows.Count > 0 Then
                    If dsChk.Tables(0).Rows(0).Item(0).ToString = "ERROR" Then
                        lblresult2.Text = "This " & ddlOPTION_YEAR.SelectedValue & " Year Already Been lock. Cannot Do any changes!"
                        ShowMessage(lblresult2.Text)
                        Exit Function
                    End If
                End If
            End If
        End If

        If tblSpecialSkill.Visible = True Then
            If ddlSPSKILL_1.SelectedValue <> "PASS" And ddlSPSKILL_1.Visible = True Then
                lblresult2.Text = "Special Skill must be Pass."
                ShowMessage(lblresult2.Text)
                ddlSPSKILL_1.Focus()
                Exit Function
            ElseIf ddlSPSKILL_2.SelectedValue <> "PASS" And ddlSPSKILL_2.Visible = True Then
                lblresult2.Text = "Special Skill must be Pass."
                ShowMessage(lblresult2.Text)
                ddlSPSKILL_2.Focus()
                Exit Function
            ElseIf ddlSPSKILL_3.SelectedValue <> "PASS" And ddlSPSKILL_3.Visible = True Then
                lblresult2.Text = "Special Skill must be Pass."
                ShowMessage(lblresult2.Text)
                ddlSPSKILL_3.Focus()
                Exit Function
            ElseIf ddlSPSKILL_4.SelectedValue <> "PASS" And ddlSPSKILL_4.Visible = True Then
                lblresult2.Text = "Special Skill must be Pass."
                ShowMessage(lblresult2.Text)
                ddlSPSKILL_4.Focus()
                Exit Function
            ElseIf ddlSPSKILL_5.SelectedValue <> "PASS" And ddlSPSKILL_5.Visible = True Then
                lblresult2.Text = "Special Skill must be Pass."
                ShowMessage(lblresult2.Text)
                ddlSPSKILL_5.Focus()
                Exit Function
            ElseIf ddlSPSKILL_6.SelectedValue <> "PASS" And ddlSPSKILL_6.Visible = True Then
                lblresult2.Text = "Special Skill must be Pass."
                ShowMessage(lblresult2.Text)
                ddlSPSKILL_6.Focus()
                Exit Function
            ElseIf ddlSPSKILL_7.SelectedValue <> "PASS" And ddlSPSKILL_7.Visible = True Then
                lblresult2.Text = "Special Skill must be Pass."
                ShowMessage(lblresult2.Text)
                ddlSPSKILL_7.Focus()
                Exit Function
            ElseIf ddlSPSKILL_8.SelectedValue <> "PASS" And ddlSPSKILL_8.Visible = True Then
                lblresult2.Text = "Special Skill must be Pass."
                ShowMessage(lblresult2.Text)
                ddlSPSKILL_8.Focus()
                Exit Function
            ElseIf ddlSPSKILL_9.SelectedValue <> "PASS" And ddlSPSKILL_9.Visible = True Then
                lblresult2.Text = "Special Skill must be Pass."
                ShowMessage(lblresult2.Text)
                ddlSPSKILL_9.Focus()
                Exit Function
            ElseIf ddlSPSKILL_10.SelectedValue <> "PASS" And ddlSPSKILL_10.Visible = True Then
                lblresult2.Text = "Special Skill must be Pass."
                ShowMessage(lblresult2.Text)
                ddlSPSKILL_10.Focus()
                Exit Function
            Else
                If lblWTS.Text = "Point" Then
                    If CDbl(txtActualWTS.Text) <> 100 Then
                        lblresult2.Text = "Special Skill Point Must be 100 Point..."
                        ShowMessage(lblresult2.Text)
                        Exit Function
                    End If
                ElseIf lblWTS.Text = "WTS (second)" Then
                    If IsNumeric(lblTimer.Text) = False Then
                        lblresult2.Text = "The Timer not running Yet!"
                        ShowMessage(lblresult2.Text)
                        Exit Function
                    ElseIf CDbl(txtActualWTS.Text) < CDbl(lblTimer.Text) Then
                        lblresult2.Text = "Fail The WTS Test!"
                        ShowMessage(lblresult2.Text)
                        Exit Function
                    End If
                End If
            End If
        End If


        If tblSpecialSkill2.Visible = True And txtSSkillE.Text = "" Then
            lblresult2.Text = "Special Skill Point field is required..."
            ShowMessage(lblresult2.Text)
            txtSSkillE.Focus()
            Exit Function
        End If

        If txtSkill1E.Visible = True And txtSkill1E.Text = "" Then
            lblresult2.Text = lblSkill1C.Text & " field is required..."
            ShowMessage(lblresult2.Text)
            txtSkill1E.Focus()
            Exit Function
        ElseIf txtSkill2E.Visible = True And txtSkill2E.Text = "" Then
            lblresult2.Text = lblSkill2C.Text & " field is required..."
            ShowMessage(lblresult2.Text)
            txtSkill2E.Focus()
            Exit Function
        ElseIf txtSkill3E.Visible = True And txtSkill3E.Text = "" Then
            lblresult2.Text = lblSkill3C.Text & " field is required..."
            ShowMessage(lblresult2.Text)
            txtSkill3E.Focus()
            Exit Function
        ElseIf txtSkill4E.Visible = True And txtSkill4E.Text = "" Then
            lblresult2.Text = lblSkill4C.Text & " field is required..."
            ShowMessage(lblresult2.Text)
            txtSkill4E.Focus()
            Exit Function
        ElseIf txtSkill5E.Visible = True And txtSkill5E.Text = "" Then
            lblresult2.Text = lblSkill5C.Text & " field is required..."
            ShowMessage(lblresult2.Text)
            txtSkill5E.Focus()
            Exit Function
        ElseIf txtSkill6E.Visible = True And txtSkill6E.Text = "" Then
            lblresult2.Text = lblSkill6C.Text & " field is required..."
            ShowMessage(lblresult2.Text)
            txtSkill6E.Focus()
            Exit Function
        ElseIf txtSkill7E.Visible = True And txtSkill7E.Text = "" Then
            lblresult2.Text = lblSkill7C.Text & " field is required..."
            ShowMessage(lblresult2.Text)
            txtSkill7E.Focus()
            Exit Function
        ElseIf txtSkill8E.Visible = True And txtSkill8E.Text = "" Then
            lblresult2.Text = lblSkill8C.Text & " field is required..."
            ShowMessage(lblresult2.Text)
            txtSkill8E.Focus()
            Exit Function
        ElseIf txtSkill9E.Visible = True And txtSkill9E.Text = "" Then
            lblresult2.Text = lblSkill9C.Text & " field is required..."
            ShowMessage(lblresult2.Text)
            txtSkill9E.Focus()
            Exit Function
        ElseIf txtSkill10E.Visible = True And txtSkill10E.Text = "" Then
            lblresult2.Text = lblSkill1C.Text & " field is required..."
            ShowMessage(lblresult2.Text)
            txtSkill10E.Focus()
            Exit Function

        ElseIf Jobgrade.Value = "C1" And Convert.ToDecimal(txtTotalI.Text) > 0.4 Then
            lblresult2.Text = "Total Improvement Point cannot More than 0.4! This Evaluation Rejected!"
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf Jobgrade.Value = "C2" And Convert.ToDecimal(txtTotalI.Text) > 0.4 Then
            lblresult2.Text = "Total Improvement Point cannot More than 0.4! This Evaluation Rejected!"
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf Jobgrade.Value = "C3" And Convert.ToDecimal(txtTotalI.Text) > 0.4 Then
            lblresult2.Text = "Total Improvement Point cannot More than 0.4! This Evaluation Rejected!"
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf Jobgrade.Value = "C4" And Convert.ToDecimal(txtTotalI.Text) > 0.4 Then
            lblresult2.Text = "Total Improvement Point cannot More than 0.4! This Evaluation Rejected!"
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf Jobgrade.Value = "F1" And Convert.ToDecimal(txtTotalI.Text) > 0.1 Then
            lblresult2.Text = "Total Improvement Point cannot More than 0.1! This Evaluation Rejected!"
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf Jobgrade.Value = "F2" And Convert.ToDecimal(txtTotalI.Text) > 0.1 Then
            lblresult2.Text = "Total Improvement Point cannot More than 0.1! This Evaluation Rejected!"
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf Jobgrade.Value = "M7" And Convert.ToDecimal(txtTotalI.Text) > 0.3 Then
            lblresult2.Text = "Total Improvement Point cannot More than 0.3! This Evaluation Rejected!"
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf Jobgrade.Value = "F7" And Convert.ToDecimal(txtTotalI.Text) > 0.1 Then
            lblresult2.Text = "Total Improvement Point cannot More than 0.1! This Evaluation Rejected!"
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf Jobgrade.Value = "M6" And Convert.ToDecimal(txtTotalI.Text) > 0.3 Then
            lblresult2.Text = "Total Improvement Point cannot More than 0.3! This Evaluation Rejected!"
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf Jobgrade.Value = "M5" And Convert.ToDecimal(txtTotalI.Text) > 0.2 Then
            lblresult2.Text = "Total Improvement Point cannot More than 0.2! This Evaluation Rejected!"
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf Jobgrade.Value = "M4" And Convert.ToDecimal(txtTotalI.Text) > 0.2 Then
            lblresult2.Text = "Total Improvement Point cannot More than 0.2! This Evaluation Rejected!"
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf Jobgrade.Value = "M3" And Convert.ToDecimal(txtTotalI.Text) > 0.2 Then
            lblresult2.Text = "Total Improvement Point cannot More than 0.2! This Evaluation Rejected!"
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf Jobgrade.Value = "M2" And Convert.ToDecimal(txtTotalI.Text) > 0.2 Then
            lblresult2.Text = "Total Improvement Point cannot More than 0.2! This Evaluation Rejected!"
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf Jobgrade.Value = "M1" And Convert.ToDecimal(txtTotalI.Text) > 0.2 Then
            lblresult2.Text = "Total Improvement Point cannot More than 0.2! This Evaluation Rejected!"
            ShowMessage(lblresult2.Text)
            Exit Function
        ElseIf txtTotalN.BackColor = Drawing.Color.Red Then
            lblresult2.Text = "New total point exceed current job grade and not meet rentention period!"
            ShowMessage(lblresult2.Text)
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
        TimerH.Value = ""
        ddlGROUP_CODE.Items.Clear()
        txtActualWTS.Text = ""
        ClearCommonSkill()
        imgBtnUpdate.CssClass = buttonPosition1
        imgBtnCancel.CssClass = buttonPosition2
    End Sub


    Protected Sub imgBtnUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnUpdate.Click
        Dim itemCount As Integer = lstright.Items.Count
        Dim spid As String = mySQL.GetSPID
        Dim abc As String = TimerH.Value
        Dim Point As String = "0.000"

        If abc = "" Then
            abc = "0.00"
        End If

        If Validatefield() Then
            ssql1 = "Select dbo.fn_GetEmpIDUsingCodeName(N'" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "')"
            myDS1 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
            If myDS1.Tables(0).Rows.Count > 0 Then
                If tblSpecialSkill.Visible = True Then
                    Point = txtActualWTS.Text
                End If
                If tblSpecialSkill2.Visible = True Then
                    Point = txtSSkillE.Text
                End If
                If tblSpecialSkill2.Visible = True Then
                    ssql = "Exec sp_KPI_insUpdDelCommonSkill N'" & Session("Company").ToString & "',N'" & myDS1.Tables(0).Rows(0).Item(0).ToString & "',N'" & ddlGROUP_CODE.SelectedValue & "','" & ddlOPTION_LEVEL.SelectedValue & "','" & txtSkill1E.Text & "','" & txtSkill2E.Text & "','" & txtSkill3E.Text & "','" & txtSkill4E.Text & "','" & txtSkill5E.Text & "','" & txtSkill6E.Text & "','" & txtSkill7E.Text & "','" & txtSkill8E.Text & "','" & txtSkill9E.Text & "','" & txtSkill10E.Text & "','" & Point & "','0','" & Session("EmpID").ToString & "','UPD1','" & ddlOPTION_YEAR.SelectedValue & "','" & ddlOPTION_MONTH.SelectedValue & "'"
                Else
                    ssql = "Exec sp_KPI_insUpdDelCommonSkill N'" & Session("Company").ToString & "',N'" & myDS1.Tables(0).Rows(0).Item(0).ToString & "',N'" & ddlGROUP_CODE.SelectedValue & "','" & ddlOPTION_LEVEL.SelectedValue & "','" & txtSkill1E.Text & "','" & txtSkill2E.Text & "','" & txtSkill3E.Text & "','" & txtSkill4E.Text & "','" & txtSkill5E.Text & "','" & txtSkill6E.Text & "','" & txtSkill7E.Text & "','" & txtSkill8E.Text & "','" & txtSkill9E.Text & "','" & txtSkill10E.Text & "','" & Point & "','" & abc & "','" & Session("EmpID").ToString & "','UPD2','" & ddlOPTION_YEAR.SelectedValue & "','" & ddlOPTION_MONTH.SelectedValue & "'"
                End If

                mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                ProcessCompleted()
                ClearCommonSkill()
                bindCommonSkill()
                ddlOPTION_CATEGORY_SelectedIndexChanged(Nothing, Nothing)
                imgBtnUpdate.OnClientClick = ""
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
        lblresult2.Text = ""
    End Sub

    Private Sub DeleteCompleted()
        'txtDateStart.Text = ""
        'txtDateEnd.Text = ""
        'chkOverwrite.Checked = False

        Dim strScript As String = "<script language=" & """" & "javascript" & """" & ">"
        strScript &= "alert('Delete Completed!');"
        strScript &= "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "Delete Completed!", strScript)
        lblresult2.Text = ""
    End Sub

    Protected Sub ddlOPTION_LEVEL_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOPTION_LEVEL.SelectedIndexChanged

        bindProcess()
        bindCategory()

    End Sub

    Protected Sub ddlOPTION_PROCESS_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOPTION_PROCESS.SelectedIndexChanged
        bindCategory()
        Dim rowcount As Integer
        ssql2 = "select dbo.fn_GetEmpIDUsingCodeName(N'" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "')"
        myDS1 = mySQL.ExecuteSQL(ssql2, Session("Company").ToString, Session("EmpID").ToString)
        If myDS1.Tables(0).Rows.Count > 0 Then
            ssql = "exec sp_KPI_selLevelProcessCategory '" & myDS1.Tables(0).Rows(0).Item(0).ToString & "','" & ddlOPTION_YEAR.SelectedValue & "','" & ddlOPTION_LEVEL.SelectedValue & "','" & ddlOPTION_PROCESS.SelectedValue & "','','SPECIAL'"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables(0).Rows.Count > 0 Then
                rowcount = myDS.Tables(0).Rows.Count - 1
                While rowcount > -1
                    If rowcount = 0 Then
                        lblPass.Text = "Pass / Fail"
                        btnStart.Style.Add("display", "inline")
                        lblSPSkill_1.Text = myDS.Tables(0).Rows(rowcount).Item(0).ToString
                        lblSPSkill_1.Visible = True
                        ddlSPSKILL_1.Visible = True
                    End If
                    If rowcount = 1 Then
                        lblSPSkill_2.Text = myDS.Tables(0).Rows(rowcount).Item(0).ToString
                        lblSPSkill_2.Visible = True
                        ddlSPSKILL_2.Visible = True
                    End If
                    If rowcount = 2 Then
                        lblSPSkill_3.Text = myDS.Tables(0).Rows(rowcount).Item(0).ToString
                        lblSPSkill_3.Visible = True
                        ddlSPSKILL_3.Visible = True
                    End If
                    If rowcount = 3 Then
                        lblSPSkill_4.Text = myDS.Tables(0).Rows(rowcount).Item(0).ToString
                        lblSPSkill_4.Visible = True
                        ddlSPSKILL_4.Visible = True
                    End If
                    If rowcount = 4 Then
                        lblSPSkill_5.Text = myDS.Tables(0).Rows(rowcount).Item(0).ToString
                        lblSPSkill_5.Visible = True
                        ddlSPSKILL_5.Visible = True
                    End If
                    If rowcount = 5 Then
                        lblSPSkill_6.Text = myDS.Tables(0).Rows(rowcount).Item(0).ToString
                        lblSPSkill_6.Visible = True
                        ddlSPSKILL_6.Visible = True
                    End If
                    If rowcount = 6 Then
                        lblSPSkill_7.Text = myDS.Tables(0).Rows(rowcount).Item(0).ToString
                        lblSPSkill_7.Visible = True
                        ddlSPSKILL_7.Visible = True
                    End If
                    If rowcount = 7 Then
                        lblSPSkill_8.Text = myDS.Tables(0).Rows(rowcount).Item(0).ToString
                        lblSPSkill_8.Visible = True
                        ddlSPSKILL_8.Visible = True
                    End If
                    If rowcount = 8 Then
                        lblSPSkill_9.Text = myDS.Tables(0).Rows(rowcount).Item(0).ToString
                        lblSPSkill_9.Visible = True
                        ddlSPSKILL_9.Visible = True
                    End If
                    If rowcount = 9 Then
                        lblSPSkill_10.Text = myDS.Tables(0).Rows(rowcount).Item(0).ToString
                        lblSPSkill_10.Visible = True
                        ddlSPSKILL_10.Visible = True
                    End If
                    rowcount = rowcount - 1
                End While
            Else
                lblSPSkill_1.Visible = False
                ddlSPSKILL_1.Visible = False
                lblSPSkill_2.Visible = False
                ddlSPSKILL_2.Visible = False
                lblSPSkill_3.Visible = False
                ddlSPSKILL_3.Visible = False
                lblSPSkill_4.Visible = False
                ddlSPSKILL_4.Visible = False
                lblSPSkill_5.Visible = False
                ddlSPSKILL_5.Visible = False
                lblSPSkill_6.Visible = False
                ddlSPSKILL_6.Visible = False
                lblSPSkill_7.Visible = False
                ddlSPSKILL_7.Visible = False
                lblSPSkill_8.Visible = False
                ddlSPSKILL_8.Visible = False
                lblSPSkill_9.Visible = False
                ddlSPSKILL_9.Visible = False
                lblSPSkill_10.Visible = False
                ddlSPSKILL_10.Visible = False
                lblPass.Text = " "
                btnStart.Style.Add("display", "none")
            End If
            ssql = Nothing
            myDS = Nothing
        End If
    End Sub

    Protected Sub ddlOPTION_CATEGORY_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOPTION_CATEGORY.SelectedIndexChanged
        ssql2 = "select dbo.fn_GetEmpIDUsingCodeName(N'" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "')"
        myDS1 = mySQL.ExecuteSQL(ssql2, Session("Company").ToString, Session("EmpID").ToString)
        If myDS1.Tables(0).Rows.Count > 0 Then
            ssql = "exec sp_KPI_selLevelProcessCategory '" & myDS1.Tables(0).Rows(0).Item(0).ToString & "','" & ddlOPTION_YEAR.SelectedValue & "','" & ddlOPTION_LEVEL.SelectedValue & "','" & ddlOPTION_PROCESS.SelectedValue & "','" & ddlOPTION_CATEGORY.SelectedValue & "','MasterSkill2','" & ddlEVALUATION.SelectedValue & "'"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables(0).Rows.Count > 0 And ddlOPTION_CATEGORY.SelectedValue <> "" Then
                ddlGROUP_CODE.DataSource = myDS.Tables(0)
                ddlGROUP_CODE.DataTextField = "Name"
                ddlGROUP_CODE.DataValueField = "Code"
                ddlGROUP_CODE.DataBind()
                txtSkill1E.Enabled = True
                txtSkill2E.Enabled = True
                txtSkill3E.Enabled = True
                txtSkill4E.Enabled = True
                txtSkill5E.Enabled = True
                txtSkill6E.Enabled = True
                txtSkill7E.Enabled = True
                txtSkill8E.Enabled = True
                txtSkill9E.Enabled = True
                txtSkill10E.Enabled = True
                txtSSkillE.Enabled = True
                imgBtnUpdate.Enabled = True
                ddlGROUP_CODE_SelectedIndexChanged(Nothing, Nothing)
            Else
                ddlGROUP_CODE.DataSource = myDS.Tables(0)
                ddlGROUP_CODE.DataTextField = "Name"
                ddlGROUP_CODE.DataValueField = "Code"
                ddlGROUP_CODE.DataBind()
                txtSkill1E.Enabled = False
                txtSkill1E.Enabled = False
                txtSkill3E.Enabled = False
                txtSkill4E.Enabled = False
                txtSkill5E.Enabled = False
                txtSkill6E.Enabled = False
                txtSkill7E.Enabled = False
                txtSkill8E.Enabled = False
                txtSkill9E.Enabled = False
                txtSkill10E.Enabled = False
                txtSSkillE.Enabled = False
                imgBtnUpdate.Enabled = False
                ddlGROUP_CODE_SelectedIndexChanged(Nothing, Nothing)
            End If
        End If
    End Sub

    Protected Sub ddlGROUP_CODE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGROUP_CODE.SelectedIndexChanged
        ssql2 = "select dbo.fn_GetEmpIDUsingCodeName(N'" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "')"
        myDS1 = mySQL.ExecuteSQL(ssql2, Session("Company").ToString, Session("EmpID").ToString)
        If myDS1.Tables(0).Rows.Count > 0 Then
            ssql = "exec sp_KPI_selWTS '" & myDS1.Tables(0).Rows(0).Item(0).ToString & "','" & ddlOPTION_LEVEL.SelectedValue & "','" & ddlOPTION_PROCESS.SelectedValue & "','" & ddlOPTION_CATEGORY.SelectedValue & "','" & ddlGROUP_CODE.SelectedValue & "','WTS'"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables(0).Rows.Count > 0 Then
                txtActualWTS.Text = myDS.Tables(0).Rows(0).Item(0).ToString
                If txtActualWTS.Text = "0" Then
                    txtActualWTS.Enabled = False
                    lblWTS.Text = "Point"
                Else
                    txtActualWTS.Enabled = False
                    lblWTS.Text = "WTS (second)"
                End If
            Else
                txtActualWTS.Text = ""
            End If
            ssql = Nothing
            myDS = Nothing
            ssql = "Exec sp_KPI_selPoint '" & myDS1.Tables(0).Rows(0).Item(0).ToString & "','" & ddlGROUP_CODE.SelectedValue & "','" & ddlOPTION_YEAR.SelectedValue & "','POINT','','','','','','','','','','',''"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables(0).Rows.Count > 0 Then
                txtSpecialSkillLY.Text = myDS.Tables(0).Rows(0).Item(2).ToString
                txtSpecialSkillC.Text = myDS.Tables(0).Rows(0).Item(3).ToString
                txtSkill1LY.Text = myDS.Tables(0).Rows(0).Item(4).ToString
                txtSkill1C.Text = myDS.Tables(0).Rows(0).Item(5).ToString
                txtSkill2LY.Text = myDS.Tables(0).Rows(0).Item(6).ToString
                txtSkill2C.Text = myDS.Tables(0).Rows(0).Item(7).ToString
                txtSkill3LY.Text = myDS.Tables(0).Rows(0).Item(8).ToString
                txtSkill3C.Text = myDS.Tables(0).Rows(0).Item(9).ToString
                txtSkill4LY.Text = myDS.Tables(0).Rows(0).Item(10).ToString
                txtSkill4C.Text = myDS.Tables(0).Rows(0).Item(11).ToString
                txtSkill5LY.Text = myDS.Tables(0).Rows(0).Item(12).ToString
                txtSkill5C.Text = myDS.Tables(0).Rows(0).Item(13).ToString
                txtSkill6LY.Text = myDS.Tables(0).Rows(0).Item(14).ToString
                txtSkill6C.Text = myDS.Tables(0).Rows(0).Item(15).ToString
                txtSkill7LY.Text = myDS.Tables(0).Rows(0).Item(16).ToString
                txtSkill7C.Text = myDS.Tables(0).Rows(0).Item(17).ToString
                txtSkill8LY.Text = myDS.Tables(0).Rows(0).Item(18).ToString
                txtSkill8C.Text = myDS.Tables(0).Rows(0).Item(19).ToString
                txtSkill9LY.Text = myDS.Tables(0).Rows(0).Item(20).ToString
                txtSkill9C.Text = myDS.Tables(0).Rows(0).Item(21).ToString
                txtSkill10LY.Text = myDS.Tables(0).Rows(0).Item(22).ToString
                txtSkill10C.Text = myDS.Tables(0).Rows(0).Item(23).ToString
                txtCommonSkillLY.Text = myDS.Tables(0).Rows(0).Item(24).ToString
                txtCommonSkillC.Text = myDS.Tables(0).Rows(0).Item(25).ToString
                txtTotalLY.Text = Decimal.Round(Convert.ToDecimal(txtSpecialSkillLY.Text) + Convert.ToDecimal(txtCommonSkillLY.Text), 3)
                txtTotalC.Text = Decimal.Round(Convert.ToDecimal(txtSpecialSkillC.Text) + Convert.ToDecimal(txtCommonSkillC.Text), 3)
                Jobgrade.Value = myDS.Tables(0).Rows(0).Item(26).ToString
                PromotionPeriod.Value = myDS.Tables(0).Rows(0).Item(27).ToString
                frompoint.Value = myDS.Tables(0).Rows(0).Item(28).ToString
                topoint.Value = myDS.Tables(0).Rows(0).Item(29).ToString
                retentionperiod.Value = myDS.Tables(0).Rows(0).Item(30).ToString
                specialperiod.Value = myDS.Tables(0).Rows(0).Item(31).ToString
                levelup.Value = myDS.Tables(0).Rows(0).Item(32).ToString
                txtSSkillLY.Text = myDS.Tables(0).Rows(0).Item(33).ToString
                txtSSkillC.Text = myDS.Tables(0).Rows(0).Item(34).ToString
                txtWEIGHTAGE.Text = myDS.Tables(0).Rows(0).Item(35).ToString
                calculateCommonSkill()
                'ddlSPSKILL_1.SelectedValue = "PASS"
                'ddlSPSKILL_2.SelectedValue = "PASS"
                'ddlSPSKILL_3.SelectedValue = "PASS"
                'ddlSPSKILL_4.SelectedValue = "PASS"
                'ddlSPSKILL_5.SelectedValue = "PASS"
                'ddlSPSKILL_6.SelectedValue = "PASS"
                'ddlSPSKILL_7.SelectedValue = "PASS"
                'ddlSPSKILL_8.SelectedValue = "PASS"
                'ddlSPSKILL_9.SelectedValue = "PASS"
                'ddlSPSKILL_10.SelectedValue = "PASS"
                'GetPoint()
            End If
        End If
    End Sub
    Protected Sub GetPoint()
        Dim total, chk As Integer
        Dim pass As Integer = 0

        If ddlSPSKILL_10.Visible = True Then
            total = 10
        ElseIf ddlSPSKILL_9.Visible = True Then
            total = 9
        ElseIf ddlSPSKILL_8.Visible = True Then
            total = 8
        ElseIf ddlSPSKILL_7.Visible = True Then
            total = 7
        ElseIf ddlSPSKILL_6.Visible = True Then
            total = 6
        ElseIf ddlSPSKILL_5.Visible = True Then
            total = 5
        ElseIf ddlSPSKILL_4.Visible = True Then
            total = 4
        ElseIf ddlSPSKILL_3.Visible = True Then
            total = 3
        ElseIf ddlSPSKILL_2.Visible = True Then
            total = 2
        ElseIf ddlSPSKILL_1.Visible = True Then
            total = 1
        Else
            total = 1
        End If
        chk = total

        If ddlSPSKILL_1.SelectedValue = "PASS" And ddlSPSKILL_1.Visible = True Then
            pass = pass + 1
        End If
        If ddlSPSKILL_2.SelectedValue = "PASS" And ddlSPSKILL_2.Visible = True Then
            pass = pass + 1
        End If
        If ddlSPSKILL_3.SelectedValue = "PASS" And ddlSPSKILL_3.Visible = True Then
            pass = pass + 1
        End If
        If ddlSPSKILL_4.SelectedValue = "PASS" And ddlSPSKILL_4.Visible = True Then
            pass = pass + 1
        End If
        If ddlSPSKILL_5.SelectedValue = "PASS" And ddlSPSKILL_5.Visible = True Then
            pass = pass + 1
        End If
        If ddlSPSKILL_6.SelectedValue = "PASS" And ddlSPSKILL_6.Visible = True Then
            pass = pass + 1
        End If
        If ddlSPSKILL_7.SelectedValue = "PASS" And ddlSPSKILL_7.Visible = True Then
            pass = pass + 1
        End If
        If ddlSPSKILL_8.SelectedValue = "PASS" And ddlSPSKILL_8.Visible = True Then
            pass = pass + 1
        End If
        If ddlSPSKILL_9.SelectedValue = "PASS" And ddlSPSKILL_9.Visible = True Then
            pass = pass + 1
        End If
        If ddlSPSKILL_10.SelectedValue = "PASS" And ddlSPSKILL_10.Visible = True Then
            pass = pass + 1
        End If

        Dim value, ActualPoint As Double

        If lblWTS.Text = "Point" Then
            txtActualWTS.Text = pass / total * 100
            ActualPoint = pass / total * 100
        End If

        If Double.TryParse(1, value) And ActualPoint = 100 Then
            If value < 1.0 Or value > 5.0 Then
                ShowMessage("Point Must Key in Between 1.000 until 5.000!")
            Else
                ssql2 = "select dbo.fn_GetEmpIDUsingCodeName(N'" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "')"
                myDS1 = mySQL.ExecuteSQL(ssql2, Session("Company").ToString, Session("EmpID").ToString)
                If myDS1.Tables(0).Rows.Count > 0 Then
                    ssql = "Exec sp_KPI_selPoint '" & myDS1.Tables(0).Rows(0).Item(0).ToString & "','" & ddlGROUP_CODE.SelectedValue & "','" & ddlOPTION_YEAR.SelectedValue & "','SPOINT','" & value & "','','','','','','','','','',''"
                    myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                    If myDS.Tables(0).Rows.Count > 0 Then
                        txtSpecialSkillN.Text = String.Format("{0:0.000}", Convert.ToDecimal(myDS.Tables(0).Rows(0).Item(0).ToString))
                        txtSkill1E.Focus()
                    End If
                End If
            End If
        End If

        If IsNumeric(txtSpecialSkillN.Text) = True And IsNumeric(txtCommonSkillN.Text) = True Then
            txtTotalN.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillN.Text) + Convert.ToDecimal(txtCommonSkillN.Text))
            txtSpecialSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillN.Text) - Convert.ToDecimal(txtSpecialSkillLY.Text))
            txtCommonSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtCommonSkillN.Text) - Convert.ToDecimal(txtCommonSkillLY.Text))
        ElseIf IsNumeric(txtSpecialSkillN.Text) = True And IsNumeric(txtCommonSkillN.Text) = False Then
            txtTotalN.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillN.Text))
            txtSpecialSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillN.Text) - Convert.ToDecimal(txtSpecialSkillLY.Text))
            txtCommonSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtCommonSkillC.Text) - Convert.ToDecimal(txtCommonSkillLY.Text))
        ElseIf IsNumeric(txtSpecialSkillN.Text) = False And IsNumeric(txtCommonSkillN.Text) = True Then
            txtTotalN.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtCommonSkillN.Text))
            txtCommonSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtCommonSkillN.Text) - Convert.ToDecimal(txtCommonSkillLY.Text))
            txtSpecialSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillC.Text) - Convert.ToDecimal(txtSpecialSkillLY.Text))
        Else
            txtTotalN.Text = "0.000"
            txtSpecialSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillC.Text) - Convert.ToDecimal(txtSpecialSkillLY.Text))
            If IsNumeric(txtCommonSkillN.Text) = True Then
                txtCommonSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtCommonSkillN.Text) - Convert.ToDecimal(txtCommonSkillLY.Text))
            End If

        End If

            If Convert.ToDecimal(txtSpecialSkillLY.Text) = 0 Then
                txtSpecialSkillI.Text = "0.000"
            End If

            If Convert.ToDecimal(txtCommonSkillLY.Text) = 0 Then
                txtCommonSkillI.Text = "0.000"
            End If
            txtTotalI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillI.Text) + Convert.ToDecimal(txtCommonSkillI.Text))

        If Convert.ToDecimal(txtTotalI.Text) >= 0.13 Then
            If Jobgrade.Value = "C1" Or Jobgrade.Value = "C2" Or Jobgrade.Value = "C3" Or Jobgrade.Value = "C4" Or Jobgrade.Value = "M7" Or Jobgrade.Value = "M6" Or Jobgrade.Value = "M5" Or Jobgrade.Value = "M4" Then
                imgBtnUpdate.OnClientClick = "return confirm('Improvement point >0.13. Proceed?')"
            Else
                imgBtnUpdate.OnClientClick = ""
            End If
            txtTotalI.BackColor = Drawing.Color.Purple
            txtTotalI.ForeColor = Drawing.Color.White
        Else
            If Jobgrade.Value = "C1" Then
                If Convert.ToDecimal(txtTotalI.Text) >= 0.13 Then
                    txtTotalI.BackColor = Drawing.Color.Purple
                    txtTotalI.ForeColor = Drawing.Color.White
                    imgBtnUpdate.OnClientClick = "return confirm('Improvement point >=0.13. Proceed?')"
                Else
                    txtTotalI.BackColor = Drawing.Color.Empty
                    txtTotalI.ForeColor = Drawing.Color.Empty
                    imgBtnUpdate.OnClientClick = ""
                End If
            ElseIf Jobgrade.Value = "M6" Or Jobgrade.Value = "M5" Or Jobgrade.Value = "M4" Then
                If Convert.ToDecimal(txtTotalI.Text) >= 0.13 Then
                    txtTotalI.BackColor = Drawing.Color.Purple
                    txtTotalI.ForeColor = Drawing.Color.White
                    imgBtnUpdate.OnClientClick = "return confirm('Improvement point >=0.13. Proceed?')"
                Else
                    txtTotalI.BackColor = Drawing.Color.Empty
                    txtTotalI.ForeColor = Drawing.Color.Empty
                    imgBtnUpdate.OnClientClick = ""
                End If
            Else
                txtTotalI.BackColor = Drawing.Color.Empty
                txtTotalI.ForeColor = Drawing.Color.Empty
                imgBtnUpdate.OnClientClick = ""
            End If
        End If

        If Convert.ToDecimal(txtTotalN.Text) > Convert.ToDecimal(topoint.Value) And (Convert.ToDecimal(PromotionPeriod.Value) >= Convert.ToDecimal(retentionperiod.Value)) Then
            txtTotalN.BackColor = Drawing.Color.Green
            txtTotalN.ForeColor = Drawing.Color.White
            imgBtnUpdate.OnClientClick = "return confirm('Total Skill Map Point is entitle for Promotion. Proceed?')"
        ElseIf Convert.ToDecimal(txtTotalN.Text) > Convert.ToDecimal(topoint.Value) And (Convert.ToDecimal(PromotionPeriod.Value) >= Convert.ToDecimal(specialperiod.Value)) Then
            txtTotalN.BackColor = Drawing.Color.Yellow
            txtTotalN.ForeColor = Drawing.Color.Black
            imgBtnUpdate.OnClientClick = "return confirm('Total Skill Map Point is entitle for Promotion. Proceed?')"
        ElseIf Convert.ToDecimal(txtTotalN.Text) > Convert.ToDecimal(topoint.Value) And (Convert.ToDecimal(PromotionPeriod.Value) < Convert.ToDecimal(specialperiod.Value)) Then
            txtTotalN.BackColor = Drawing.Color.Red
            txtTotalN.ForeColor = Drawing.Color.White
            imgBtnUpdate.OnClientClick = ""
        Else
            txtTotalN.BackColor = Drawing.Color.Empty
            txtTotalN.ForeColor = Drawing.Color.Empty
            imgBtnUpdate.OnClientClick = ""
        End If

    End Sub

    Protected Sub ddlSPSKILL_1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSPSKILL_1.SelectedIndexChanged
        GetPoint()
    End Sub

    Protected Sub ddlSPSKILL_10_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSPSKILL_10.SelectedIndexChanged
        GetPoint()
    End Sub

    Protected Sub ddlSPSKILL_2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSPSKILL_2.SelectedIndexChanged
        GetPoint()
    End Sub

    Protected Sub ddlSPSKILL_3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSPSKILL_3.SelectedIndexChanged
        GetPoint()
    End Sub

    Protected Sub ddlSPSKILL_4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSPSKILL_4.SelectedIndexChanged
        GetPoint()
    End Sub

    Protected Sub ddlSPSKILL_5_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSPSKILL_5.SelectedIndexChanged
        GetPoint()
    End Sub

    Protected Sub ddlSPSKILL_6_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSPSKILL_6.SelectedIndexChanged
        GetPoint()
    End Sub

    Protected Sub ddlSPSKILL_7_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSPSKILL_7.SelectedIndexChanged
        GetPoint()
    End Sub

    Protected Sub ddlSPSKILL_8_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSPSKILL_8.SelectedIndexChanged
        GetPoint()
    End Sub

    Protected Sub ddlSPSKILL_9_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSPSKILL_9.SelectedIndexChanged
        GetPoint()
    End Sub

    Protected Sub txtSkill1E_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSkill1E.TextChanged
        Dim value As Double
        If txtSkill1E.Visible = True Then
            If Double.TryParse(txtSkill1E.Text, value) Then
                If value < 1.0 Or value > 5.0 Then
                    ShowMessage("Point Must Key in Between 1.000 until 5.000!")
                    txtSkill1E.Text = ""
                    txtSkill1E.Focus()
                Else
                    txtSkill1E.Text = String.Format("{0:0.000}", value)
                    txtSkill1N.Text = txtSkill1E.Text
                    calculateCommonSkill()
                    txtSkill2E.Focus()
                End If

            Else
                txtSkill1E.Text = ""
                txtSkill1E.Focus()
            End If
        End If
    End Sub
    Protected Sub txtSkill2E_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSkill2E.TextChanged
        Dim value As Double
        If txtSkill2E.Visible = True Then
            If Double.TryParse(txtSkill2E.Text, value) Then
                If value < 1.0 Or value > 5.0 Then
                    ShowMessage("Point Must Key in Between 1.000 until 5.000!")
                    txtSkill2E.Text = ""
                    txtSkill2E.Focus()
                Else
                    txtSkill2E.Text = String.Format("{0:0.000}", value)
                    txtSkill2N.Text = txtSkill2E.Text
                    calculateCommonSkill()
                    txtSkill3E.Focus()
                End If

            Else
                txtSkill2E.Text = ""
                txtSkill2E.Focus()
            End If
        End If
    End Sub
    Protected Sub txtSkill3E_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSkill3E.TextChanged
        Dim value As Double
        If txtSkill3E.Visible = True Then
            If Double.TryParse(txtSkill3E.Text, value) Then
                If value < 1.0 Or value > 5.0 Then
                    ShowMessage("Point Must Key in Between 1.000 until 5.000!")
                    txtSkill3E.Text = ""
                    txtSkill3E.Focus()
                Else
                    txtSkill3E.Text = String.Format("{0:0.000}", value)
                    txtSkill3N.Text = txtSkill3E.Text
                    calculateCommonSkill()
                    txtSkill4E.Focus()
                End If

            Else
                txtSkill3E.Text = ""
                txtSkill3E.Focus()
            End If
        End If
    End Sub
    Protected Sub txtSkill4E_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSkill4E.TextChanged
        Dim value As Double
        If txtSkill4E.Visible = True Then
            If Double.TryParse(txtSkill4E.Text, value) Then
                If value < 1.0 Or value > 5.0 Then
                    ShowMessage("Point Must Key in Between 1.000 until 5.000!")
                    txtSkill4E.Text = ""
                    txtSkill4E.Focus()
                Else
                    txtSkill4E.Text = String.Format("{0:0.000}", value)
                    txtSkill4N.Text = txtSkill4E.Text
                    calculateCommonSkill()
                    txtSkill5E.Focus()
                End If

            Else
                txtSkill4E.Text = ""
                txtSkill4E.Focus()
            End If
        End If
    End Sub
    Protected Sub txtSkill5E_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSkill5E.TextChanged
        Dim value As Double
        If txtSkill5E.Visible = True Then
            If Double.TryParse(txtSkill5E.Text, value) Then
                If value < 1.0 Or value > 5.0 Then
                    ShowMessage("Point Must Key in Between 1.000 until 5.000!")
                    txtSkill5E.Text = ""
                    txtSkill5E.Focus()
                Else
                    txtSkill5E.Text = String.Format("{0:0.000}", value)
                    txtSkill5N.Text = txtSkill5E.Text
                    calculateCommonSkill()
                    txtSkill6E.Focus()
                End If

            Else
                txtSkill5E.Text = ""
                txtSkill5E.Focus()
            End If
        End If
    End Sub
    Protected Sub txtSkill6E_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSkill6E.TextChanged
        Dim value As Double
        If txtSkill6E.Visible = True Then
            If Double.TryParse(txtSkill6E.Text, value) Then
                If value < 1.0 Or value > 5.0 Then
                    ShowMessage("Point Must Key in Between 1.000 until 5.000!")
                    txtSkill6E.Text = ""
                    txtSkill6E.Focus()
                Else
                    txtSkill6E.Text = String.Format("{0:0.000}", value)
                    txtSkill6N.Text = txtSkill6E.Text
                    calculateCommonSkill()
                    txtSkill7E.Focus()
                End If

            Else
                txtSkill6E.Text = ""
                txtSkill6E.Focus()
            End If
        End If
    End Sub
    Protected Sub txtSkill7E_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSkill7E.TextChanged
        Dim value As Double
        If txtSkill7E.Visible = True Then
            If Double.TryParse(txtSkill7E.Text, value) Then
                If value < 1.0 Or value > 5.0 Then
                    ShowMessage("Point Must Key in Between 1.000 until 5.000!")
                    txtSkill7E.Text = ""
                    txtSkill7E.Focus()
                Else
                    txtSkill7E.Text = String.Format("{0:0.000}", value)
                    txtSkill7N.Text = txtSkill7E.Text
                    calculateCommonSkill()
                    txtSkill8E.Focus()
                End If

            Else
                txtSkill7E.Text = ""
                txtSkill7E.Focus()
            End If
        End If
    End Sub
    Protected Sub txtSkill8E_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSkill8E.TextChanged
        Dim value As Double
        If txtSkill8E.Visible = True Then
            If Double.TryParse(txtSkill8E.Text, value) Then
                If value < 1.0 Or value > 5.0 Then
                    ShowMessage("Point Must Key in Between 1.000 until 5.000!")
                    txtSkill8E.Text = ""
                    txtSkill8E.Focus()
                Else
                    txtSkill8E.Text = String.Format("{0:0.000}", value)
                    txtSkill8N.Text = txtSkill8E.Text
                    calculateCommonSkill()
                    txtSkill9E.Focus()
                End If

            Else
                txtSkill8E.Text = ""
                txtSkill8E.Focus()
            End If
        End If
    End Sub
    Protected Sub txtSkill9E_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSkill9E.TextChanged
        Dim value As Double
        If txtSkill9E.Visible = True Then
            If Double.TryParse(txtSkill9E.Text, value) Then
                If value < 1.0 Or value > 5.0 Then
                    ShowMessage("Point Must Key in Between 1.000 until 5.000!")
                    txtSkill9E.Text = ""
                    txtSkill9E.Focus()
                Else
                    txtSkill9E.Text = String.Format("{0:0.000}", value)
                    txtSkill9N.Text = txtSkill9E.Text
                    calculateCommonSkill()
                    txtSkill10E.Focus()
                End If

            Else
                txtSkill9E.Text = ""
                txtSkill9E.Focus()
            End If
        End If
    End Sub
    Protected Sub txtSkill10E_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSkill10E.TextChanged
        Dim value As Double
        If txtSkill10E.Visible = True Then
            If Double.TryParse(txtSkill10E.Text, value) Then
                If value < 1.0 Or value > 5.0 Then
                    ShowMessage("Point Must Key in Between 1.000 until 5.000!")
                    txtSkill10E.Text = ""
                    txtSkill10E.Focus()
                Else
                    txtSkill10E.Text = String.Format("{0:0.000}", value)
                    txtSkill10N.Text = txtSkill10E.Text
                    calculateCommonSkill()
                    txtSkill10E.Focus()
                End If

            Else
                txtSkill10E.Text = ""
                txtSkill10E.Focus()
            End If
        End If
    End Sub

    Protected Sub txtSSkillE_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSSkillE.TextChanged
        Dim value As Double

        If Double.TryParse(txtSSkillE.Text, value) Then
            If value < 1.0 Or value > 5.0 Then
                ShowMessage("Point Must Key in Between 1.000 until 5.000!")
                txtSSkillE.Text = ""
                txtSSkillE.Focus()
            Else
                txtSSkillE.Text = String.Format("{0:0.000}", value)
                txtSSkillN.Text = String.Format("{0:0.000}", value)
                ssql2 = "select dbo.fn_GetEmpIDUsingCodeName(N'" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "')"
                myDS1 = mySQL.ExecuteSQL(ssql2, Session("Company").ToString, Session("EmpID").ToString)
                If myDS1.Tables(0).Rows.Count > 0 Then
                    ssql = "Exec sp_KPI_selPoint '" & myDS1.Tables(0).Rows(0).Item(0).ToString & "','" & ddlGROUP_CODE.SelectedValue & "','" & ddlOPTION_YEAR.SelectedValue & "','SPOINT','" & value & "','','','','','','','','','',''"
                    myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                    If myDS.Tables(0).Rows.Count > 0 Then
                        txtSpecialSkillN.Text = String.Format("{0:0.000}", Convert.ToDecimal(myDS.Tables(0).Rows(0).Item(0).ToString))
                        txtSkill1E.Focus()
                    Else
                        txtSSkillE.Text = ""
                        txtSSkillE.Focus()
                    End If
                Else
                    txtSSkillE.Text = ""
                    txtSSkillE.Focus()
                End If
            End If
        Else
            txtSSkillE.Text = ""
            txtSSkillE.Focus()
        End If

        If IsNumeric(txtSpecialSkillN.Text) = True And IsNumeric(txtCommonSkillN.Text) = True Then
            txtTotalN.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillN.Text) + Convert.ToDecimal(txtCommonSkillN.Text))
            txtSpecialSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillN.Text) - Convert.ToDecimal(txtSpecialSkillLY.Text))
            txtCommonSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtCommonSkillN.Text) - Convert.ToDecimal(txtCommonSkillLY.Text))
        ElseIf IsNumeric(txtSpecialSkillN.Text) = True And IsNumeric(txtCommonSkillN.Text) = False Then
            txtTotalN.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillN.Text))
            txtSpecialSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillN.Text) - Convert.ToDecimal(txtSpecialSkillLY.Text))
            txtCommonSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtCommonSkillC.Text) - Convert.ToDecimal(txtCommonSkillLY.Text))
        ElseIf IsNumeric(txtSpecialSkillN.Text) = False And IsNumeric(txtCommonSkillN.Text) = True Then
            txtTotalN.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtCommonSkillN.Text))
            txtCommonSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtCommonSkillN.Text) - Convert.ToDecimal(txtCommonSkillLY.Text))
            txtSpecialSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillC.Text) - Convert.ToDecimal(txtSpecialSkillLY.Text))
        Else
            txtTotalN.Text = "0.000"
            txtSpecialSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillC.Text) - Convert.ToDecimal(txtSpecialSkillLY.Text))
            txtCommonSkillI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtCommonSkillN.Text) - Convert.ToDecimal(txtCommonSkillLY.Text))
        End If

        If Convert.ToDecimal(txtSpecialSkillLY.Text) = 0 Then
            txtSpecialSkillI.Text = "0.000"
        End If

        If Convert.ToDecimal(txtCommonSkillLY.Text) = 0 Then
            txtCommonSkillI.Text = "0.000"
        End If
        txtTotalI.Text = String.Format("{0:0.000}", Convert.ToDecimal(txtSpecialSkillI.Text) + Convert.ToDecimal(txtCommonSkillI.Text))

        If Convert.ToDecimal(txtTotalI.Text) > 0.13 Then
            If Jobgrade.Value = "C1" Or Jobgrade.Value = "C2" Or Jobgrade.Value = "C3" Or Jobgrade.Value = "C4" Or Jobgrade.Value = "M6" Or Jobgrade.Value = "M7" Or Jobgrade.Value = "M5" Or Jobgrade.Value = "M4" Then
                imgBtnUpdate.OnClientClick = "return confirm('Improvement point >0.13. Proceed?')"
            Else
                imgBtnUpdate.OnClientClick = ""
            End If
            txtTotalI.BackColor = Drawing.Color.Purple
            txtTotalI.ForeColor = Drawing.Color.White
        Else
            txtTotalI.BackColor = Drawing.Color.Empty
            txtTotalI.ForeColor = Drawing.Color.Empty
            imgBtnUpdate.OnClientClick = ""
        End If

        If Convert.ToDecimal(txtTotalN.Text) > Convert.ToDecimal(topoint.Value) And (PromotionPeriod.Value >= retentionperiod.Value) Then
            txtTotalN.BackColor = Drawing.Color.Green
            txtTotalN.ForeColor = Drawing.Color.White
            imgBtnUpdate.OnClientClick = "return confirm('Total Skill Map Point is entitle for Promotion. Proceed?')"
        ElseIf Convert.ToDecimal(txtTotalN.Text) > Convert.ToDecimal(topoint.Value) And (PromotionPeriod.Value >= specialperiod.Value) Then
            txtTotalN.BackColor = Drawing.Color.Yellow
            txtTotalN.ForeColor = Drawing.Color.Black
            imgBtnUpdate.OnClientClick = "return confirm('Total Skill Map Point is entitle for Promotion. Proceed?')"
        ElseIf Convert.ToDecimal(txtTotalN.Text) > Convert.ToDecimal(topoint.Value) And (PromotionPeriod.Value < specialperiod.Value) Then
            txtTotalN.BackColor = Drawing.Color.Red
            txtTotalN.ForeColor = Drawing.Color.White
            imgBtnUpdate.OnClientClick = ""
        Else
            txtTotalN.BackColor = Drawing.Color.Empty
            txtTotalN.ForeColor = Drawing.Color.Empty
            imgBtnUpdate.OnClientClick = ""
        End If

    End Sub

    Protected Sub ddlOPTION_YEAR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOPTION_YEAR.SelectedIndexChanged
        If ddlOPTION_YEAR.SelectedValue <= Year(Today()) - 1 Then
            ddlOPTION_YEAR.SelectedValue = Year(Today()) - 1
            ddlOPTION_MONTH.SelectedValue = "12"
            ddlOPTION_MONTH.Enabled = False
        Else
            ddlOPTION_MONTH.Enabled = True
            Dim mon As String
            mon = Month(Today())
            If Len(mon) = 1 Then
                mon = "0" + mon
            End If
            ddlOPTION_MONTH.SelectedValue = mon
        End If

        If checkWeightage() Then
            bindStatus()
            bindCommonSkill()
            bindSpecialSkill()
            bindLevel()

            bindProcess()
            bindCategory()
            If ddlGROUP_CODE.SelectedIndex >= 0 Then
                ddlGROUP_CODE_SelectedIndexChanged(Nothing, Nothing)
                If tblSpecialSkill2.Visible = True Then
                    txtSSkillE_TextChanged(Nothing, Nothing)
                End If
                calculateCommonSkill()
            End If
        End If
    End Sub

    
    Protected Sub imgBtnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnDelete.Click
        If DeleteValidate() Then
            ssql1 = "Select dbo.fn_GetEmpIDUsingCodeName(N'" & txtEMPLOYEE_PROFILE_ID.Text.Replace("'", "''") & "')"
            myDS1 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)
            If myDS1.Tables(0).Rows.Count > 0 Then
                ssql = "Exec sp_KPI_insUpdDelCommonSkill N'" & Session("Company").ToString & "',N'" & myDS1.Tables(0).Rows(0).Item(0).ToString & "',N'" & ddlGROUP_CODE.SelectedValue & "','" & ddlOPTION_LEVEL.SelectedValue & "','" & txtSkill1E.Text & "','" & txtSkill2E.Text & "','" & txtSkill3E.Text & "','" & txtSkill4E.Text & "','" & txtSkill5E.Text & "','" & txtSkill6E.Text & "','" & txtSkill7E.Text & "','" & txtSkill8E.Text & "','" & txtSkill9E.Text & "','" & txtSkill10E.Text & "','0','0','" & Session("EmpID").ToString & "','DEL','" & ddlOPTION_YEAR.SelectedValue & "','" & ddlOPTION_MONTH.SelectedValue & "'"
                mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

                DeleteCompleted()
                ClearCommonSkill()
                bindCommonSkill()
                ddlOPTION_CATEGORY_SelectedIndexChanged(Nothing, Nothing)
            End If
        End If
    End Sub

    Protected Sub ddlEVALUATION_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEVALUATION.SelectedIndexChanged
        bindLevel()
        'ddlOPTION_CATEGORY_SelectedIndexChanged(Nothing, Nothing)

    End Sub
End Class