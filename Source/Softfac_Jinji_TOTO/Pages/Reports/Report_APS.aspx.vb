Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Partial Class Pages_Reports_Report_APS
    Inherits System.Web.UI.Page
#Region "Public Declaration"
    Private WithEvents mySQL As New clsSQL, myDS As New DataSet, myDS1 As New DataSet, myDS2 As New DataSet, mySetting As New clsGlobalSetting
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
    Dim ssql1 As String, ssql2 As String, ssql3 As String, ssql4 As String
    Dim rowPosition As String = "R_", rowPositionM As String = "RM_", panelPosition As String = "Display_"
    Dim buttonPosition1 As String = "B1_", buttonPosition2 As String = "B2_", buttonPosition3 As String = "B3_", labelPosition1 As String = "L1_"
    Dim btnColourDef, btnColourAlt As String
    Dim strPath As String = "../Images"
    Dim logic As Boolean
    Dim sysYear As Integer, counter As Integer
#End Region

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
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")

        mySetting.GetBtnImgUrl(imgBtnSaveAs, Session("Company").ToString, btnColourDef, "btnSave.png")

        mySetting.GetImgUrl(imgKeyYEAR_MONTH, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyCOMPANY_PROFILE_CODE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgEMPLOYEE_PROFILE_ID, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        'mySetting.GetImgUrl(imgKeyOPTION_PAY_TYPE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnYEAR_MONTH, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnCOMPANY_PROFILE_CODE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgBtnEMPLOYEE_PROFILE_ID, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgBtnEMPLOYEE_PROFILE_ID2, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        'mySetting.GetImgUrl(imgbtnOPTION_PAY_TYPE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetBtnImgUrl(imgBtnAddAll, Session("Company").ToString, btnColourDef, "addall.png")
        mySetting.GetBtnImgUrl(imgBtnAddItem, Session("Company").ToString, btnColourDef, "additem.png")
        mySetting.GetBtnImgUrl(imgBtnRemoveAll, Session("Company").ToString, btnColourDef, "removeall.png")
        mySetting.GetBtnImgUrl(imgBtnRemoveItem, Session("Company").ToString, btnColourDef, "removeitem.png")


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
        lblOPTION_TYPE.Visible = True
        imgOPTION_TYPE.Visible = False
        imgBtnOPTION_TYPE.Visible = False
        lblOPTION_TYPE.Text = "Group by"
        lblOCP_ID.Visible = True
        imgOCP_ID.Visible = False
        imgBtnOCP_ID.Visible = False
        lblOCP_ID.Text = "OCP ID"
        mySetting.GetDropdownlistValue("User_Approval_VW", "OPTION_TYPE", ddlOPTION_TYPE)
        ddlOPTION_TYPE.Visible = True
        ddlOCP_ID.Visible = True
        txtEMPLOYEE_PROFILE_ID.Visible = True
        lblEMPLOYEE_PROFILE_ID.Text = "Employee ID"
        imgBtnEMPLOYEE_PROFILE_ID.Visible = True
        If imgBtnEMPLOYEE_PROFILE_ID.Visible = True And lstleft.Visible = False Then
            invisibleLst()
            Session("action_edit") = ""
        End If

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

                imgBtnSaveAs.Visible = AllowPrint
            End If

            If myDT2.Rows.Count > 0 Then
                For Me.i = 0 To myDT2.Rows.Count - 1
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

        imgBtnSaveAs.CssClass = buttonPosition1

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
        imgBtnEMPLOYEE_PROFILE_ID2.Visible = False
        imgBtnEMPLOYEE_PROFILE_ID.Visible = True

        'get field position
        ssql = "exec sp_sa_get_fields_position '" & Form.ID & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        autonum = 0
        If myDS.Tables(0).Rows.Count > 0 Then
            For Me.i = 0 To myDS.Tables(0).Rows.Count - 1

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

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "YEAR_MONTH" Then
                    If lblYEAR_MONTH.Visible = True And txtYEAR_MONTH.Visible = True Then
                        autonum += 1
                        imgKeyYEAR_MONTH.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyYEAR_MONTH.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyYEAR_MONTH.Style.Add("position", "absolute")
                        autonum += 1
                        lblYEAR_MONTH.CssClass = "wordstyle12"
                        lblYEAR_MONTH.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblYEAR_MONTH.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblYEAR_MONTH.Style.Add("position", "absolute")
                        autonum += 1
                        txtYEAR_MONTH.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        txtYEAR_MONTH.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        txtYEAR_MONTH.Style.Add("position", "absolute")
                        txtYEAR_MONTH.Width = txtWidth
                        autonum += 1
                        imgbtnYEAR_MONTH.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnYEAR_MONTH.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnYEAR_MONTH.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_BANK" Then
                    If lblOPTION_BANK.Visible = True And ddlOPTION_BANK.Visible = True Then
                        autonum += 1
                        imgKeyOPTION_BANK.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyOPTION_BANK.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyOPTION_BANK.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_BANK.CssClass = "wordstyle12"
                        lblOPTION_BANK.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_BANK.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_BANK.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_BANK.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_BANK.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_BANK.Style.Add("position", "absolute")
                        ddlOPTION_BANK.Width = txtWidth
                        autonum += 1
                        imgbtnOPTION_BANK.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_BANK.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_BANK.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_PAY_TYPE" Then
                    If lblOPTION_PAY_TYPE.Visible = True And ddlOPTION_PAY_TYPE.Visible = True Then
                        autonum += 1
                        imgKeyOPTION_PAY_TYPE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyOPTION_PAY_TYPE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyOPTION_PAY_TYPE.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_PAY_TYPE.CssClass = "wordstyle12"
                        lblOPTION_PAY_TYPE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_PAY_TYPE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_PAY_TYPE.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_PAY_TYPE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_PAY_TYPE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_PAY_TYPE.Style.Add("position", "absolute")
                        ddlOPTION_PAY_TYPE.Width = txtWidth
                        autonum += 1
                        imgbtnOPTION_PAY_TYPE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_PAY_TYPE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_PAY_TYPE.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_PAY_CYCLE" Then
                    If lblOPTION_PAY_CYCLE.Visible = True And ddlOPTION_PAY_CYCLE.Visible = True Then
                        autonum += 1
                        imgKeyOPTION_PAY_CYCLE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyOPTION_PAY_CYCLE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyOPTION_PAY_CYCLE.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_PAY_CYCLE.CssClass = "wordstyle12"
                        lblOPTION_PAY_CYCLE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_PAY_CYCLE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_PAY_CYCLE.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_PAY_CYCLE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_PAY_CYCLE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_PAY_CYCLE.Style.Add("position", "absolute")
                        ddlOPTION_PAY_CYCLE.Width = txtWidth
                        autonum += 1
                        imgbtnOPTION_PAY_CYCLE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_PAY_CYCLE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_PAY_CYCLE.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "CREDIT_DATE" Then
                    If lblCREDIT_DATE.Visible = True And txtCREDIT_DATE.Visible = True Then
                        autonum += 1
                        imgKeyCREDIT_DATE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyCREDIT_DATE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyCREDIT_DATE.Style.Add("position", "absolute")
                        autonum += 1
                        lblCREDIT_DATE.CssClass = "wordstyle12"
                        lblCREDIT_DATE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblCREDIT_DATE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblCREDIT_DATE.Style.Add("position", "absolute")
                        autonum += 1
                        txtCREDIT_DATE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        txtCREDIT_DATE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        txtCREDIT_DATE.Style.Add("position", "absolute")
                        txtCREDIT_DATE.Width = txtWidth
                        autonum += 1
                        imgbtnCREDIT_DATE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnCREDIT_DATE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnCREDIT_DATE.Style.Add("position", "absolute")
                    End If
                End If

                If UCase(myDS.Tables(0).Rows(i).Item(1).ToString) = "OPTION_REPORT" Then
                    If lblOPTION_REPORT.Visible = True And ddlOPTION_REPORT.Visible = True Then
                        autonum += 1
                        imgKeyOPTION_REPORT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgKeyOPTION_REPORT.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgKeyOPTION_REPORT.Style.Add("position", "absolute")
                        autonum += 1
                        lblOPTION_REPORT.CssClass = "wordstyle12"
                        lblOPTION_REPORT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        lblOPTION_REPORT.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        lblOPTION_REPORT.Style.Add("position", "absolute")
                        autonum += 1
                        ddlOPTION_REPORT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        ddlOPTION_REPORT.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        ddlOPTION_REPORT.Style.Add("position", "absolute")
                        ddlOPTION_REPORT.Width = txtWidth
                        autonum += 1
                        imgbtnOPTION_REPORT.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                        imgbtnOPTION_REPORT.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                        imgbtnOPTION_REPORT.Style.Add("position", "absolute")
                    End If
                End If

            Next
        End If

        If txtEMPLOYEE_PROFILE_ID.Visible = True Then
            autonum += 4
            If lblOPTION_TYPE.Visible = True And ddlOPTION_TYPE.Visible = True Then
                autonum += 1
                imgOPTION_TYPE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                imgOPTION_TYPE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                imgOPTION_TYPE.Style.Add("position", "absolute")
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
                imgBtnOPTION_TYPE.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                imgBtnOPTION_TYPE.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                imgBtnOPTION_TYPE.Style.Add("position", "absolute")
            End If

            If lblOCP_ID.Visible = True And ddlOCP_ID.Visible = True Then
                autonum += 1
                imgOCP_ID.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                imgOCP_ID.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                imgOCP_ID.Style.Add("position", "absolute")
                autonum += 1
                lblOCP_ID.CssClass = "wordstyle12"
                lblOCP_ID.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                lblOCP_ID.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                lblOCP_ID.Style.Add("position", "absolute")
                autonum += 1
                ddlOCP_ID.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                ddlOCP_ID.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                ddlOCP_ID.Style.Add("position", "absolute")
                ddlOCP_ID.Width = ddlWidth
                autonum += 1
                imgBtnOCP_ID.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                imgBtnOCP_ID.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                imgBtnOCP_ID.Style.Add("position", "absolute")
            End If
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

                imgEMPLOYEE_PROFILE_ID.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                imgEMPLOYEE_PROFILE_ID.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                imgEMPLOYEE_PROFILE_ID.Style.Add("position", "absolute")
                autonum += 1
                lblEMPLOYEE_PROFILE_ID.CssClass = "wordstyle12"
                lblEMPLOYEE_PROFILE_ID.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                lblEMPLOYEE_PROFILE_ID.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                lblEMPLOYEE_PROFILE_ID.Style.Add("position", "absolute")
                autonum += 1

                txtEMPLOYEE_PROFILE_ID.Visible = False

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

                imgBtnEMPLOYEE_PROFILE_ID2.Visible = True
                imgBtnEMPLOYEE_PROFILE_ID.Visible = False
                imgBtnEMPLOYEE_PROFILE_ID2.Style.Add("left", mySetting.GetObjPositionRL(EctWidth, 6, "X"))
                imgBtnEMPLOYEE_PROFILE_ID2.Style.Add("top", mySetting.GetObjPositionRL(6, autonum2, "Y"))
                imgBtnEMPLOYEE_PROFILE_ID2.Style.Add("position", "absolute")
                autonum += 44
            Else
                If lblEMPLOYEE_PROFILE_ID.Visible = True Then
                    autonum += 1
                    imgEMPLOYEE_PROFILE_ID.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                    imgEMPLOYEE_PROFILE_ID.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                    imgEMPLOYEE_PROFILE_ID.Style.Add("position", "absolute")
                    autonum += 1
                    lblEMPLOYEE_PROFILE_ID.CssClass = "wordstyle12"
                    lblEMPLOYEE_PROFILE_ID.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                    lblEMPLOYEE_PROFILE_ID.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                    lblEMPLOYEE_PROFILE_ID.Style.Add("position", "absolute")
                    autonum += 1
                    txtEMPLOYEE_PROFILE_ID.Visible = True
                    txtEMPLOYEE_PROFILE_ID.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                    txtEMPLOYEE_PROFILE_ID.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                    txtEMPLOYEE_PROFILE_ID.Style.Add("position", "absolute")
                    txtEMPLOYEE_PROFILE_ID.Width = txtWidth
                    autonum += 1
                    imgBtnEMPLOYEE_PROFILE_ID.Style.Add("left", mySetting.GetObjPosition(EctWidth, autonum, "X"))
                    imgBtnEMPLOYEE_PROFILE_ID.Style.Add("top", mySetting.GetObjPosition(EctWidth, autonum, "Y"))
                    imgBtnEMPLOYEE_PROFILE_ID.Style.Add("position", "absolute")
                End If
            End If
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
        'lblresult2.Text = "Click on Cancel Button to return..."
        imgTop.CssClass = "Display_0"
        imgBottom.CssClass = panelPosition & autonum

    End Sub

    Private Function ValidatePrint(ByVal myds As DataSet) As Boolean
        If myds.Tables(0).Rows(0).Item(0).ToString = "" Then
            lblresult2.Text = "[" & lblCOMPANY_PROFILE_CODE.Text & "] Is A Required Field!"
            txtCOMPANY_PROFILE_CODE.Focus()
            Return False
        ElseIf IsNumeric(mySetting.ConvertDateToDecimal(txtYEAR_MONTH.Text, Session("Company").ToString, Session("Module").ToString)) = False Then
            lblresult2.Text = "Invalid Input Date Format For [" & lblYEAR_MONTH.Text & "] !"
            txtYEAR_MONTH.Focus()
            Return False
        ElseIf IsNumeric(mySetting.ConvertDateToDecimal(txtCREDIT_DATE.Text, Session("Company").ToString, Session("Module").ToString)) = False Then
            lblresult2.Text = "Invalid Input Date Format For [" & lblCREDIT_DATE.Text & "] !"
            txtYEAR_MONTH.Focus()
            Return False
        ElseIf ddlOPTION_PAY_TYPE.SelectedValue = "" Then
            lblresult2.Text = "[" & lblOPTION_PAY_TYPE.Text & "] Is A Required Field!"
            ddlOPTION_PAY_TYPE.Focus()
            Return False
        ElseIf ddlOPTION_PAY_CYCLE.SelectedValue = "" Then
            lblresult2.Text = "[" & lblOPTION_PAY_CYCLE.Text & "] Is A Required Field!"
            ddlOPTION_PAY_CYCLE.Focus()
            Return False
        ElseIf ddlOPTION_BANK.SelectedValue = "" Then
            lblresult2.Text = "[" & lblOPTION_BANK.Text & "] Is A Required Field!"
            ddlOPTION_BANK.Focus()
            Return False
        Else
            Return True
        End If
    End Function

    Protected Sub ddlOPTION_PAY_TYPE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOPTION_PAY_TYPE.SelectedIndexChanged
        If ddlOPTION_PAY_TYPE.SelectedValue = "MONTHLY" Then
            ddlOPTION_PAY_CYCLE.SelectedValue = "MONTHLY"
        End If
    End Sub

    Protected Sub imgBtnSaveAs_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSaveAs.Click
        Dim itemCount As Integer = lstright.Items.Count
        Dim k As Integer
        Dim spid As String = mySQL.GetSPID
        ssql = "select dbo.fn_ReturnCompanyProfileCode(N'" & txtCOMPANY_PROFILE_CODE.Text & "'),dbo.fn_datepart('month',dbo.fn_UnDisplayDate('" & txtYEAR_MONTH.Text & "',dbo.fn_ReturnCompanyProfileCode(N'" & txtCOMPANY_PROFILE_CODE.Text & "'),'" & Session("Module").ToString & "')),dbo.fn_datepart('year',dbo.fn_UnDisplayDate('" & txtYEAR_MONTH.Text & "',dbo.fn_ReturnCompanyProfileCode(N'" & txtCOMPANY_PROFILE_CODE.Text & "'),'" & Session("Module").ToString & "')),@@spid,dbo.fn_UnDisplayDate('" & txtCREDIT_DATE.Text & "',dbo.fn_ReturnCompanyProfileCode(N'" & txtCOMPANY_PROFILE_CODE.Text & "'),'" & Session("Module").ToString & "')"
        myDS = New DataSet()
        myDS = mySQL.ExecuteSQL(ssql)
        If ValidatePrint(myDS) = True Then
            If myDS.Tables(0).Rows.Count = 1 Then
                ssql1 = "exec sp_pr_InsUpdDelEmployeePayrollProcessTemp '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','','" & myDS.Tables(0).Rows(0).Item(2).ToString & "','" & myDS.Tables(0).Rows(0).Item(1).ToString & "','" & ddlOPTION_PAY_CYCLE.SelectedValue & "'," & spid & ",'DEL','" & Session("EmpID").ToString & "'"
                myDS1 = New DataSet()
                myDS1 = mySQL.ExecuteSQL(ssql1)

                myDS1 = Nothing
                ssql1 = Nothing
                For k = 0 To itemCount - 1
                    'If ddlOPTION_REPORT.SelectedValue = "BONUS" Then
                    ssql1 = "exec sp_pr_InsUpdDelEmployeePayrollProcessTemp '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & lstright.Items(k).Value & "','" & myDS.Tables(0).Rows(0).Item(2).ToString & "','" & myDS.Tables(0).Rows(0).Item(1).ToString & "','" & ddlOPTION_PAY_CYCLE.SelectedValue & "'," & spid & ",'ADD','" & Session("EmpID").ToString & "'"
                    myDS1 = New DataSet()
                    myDS1 = mySQL.ExecuteSQL(ssql1)

                    myDS1 = Nothing
                    ssql1 = Nothing
                Next
                If ddlOPTION_REPORT.SelectedValue = "PAY" Then
                    ssql1 = "Exec sp_pr_InsAps '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & ddlOPTION_BANK.SelectedValue & "','" & myDS.Tables(0).Rows(0).Item(1).ToString & "'," & myDS.Tables(0).Rows(0).Item(2).ToString & ",'" & ddlOPTION_PAY_TYPE.SelectedValue & "','" & ddlOPTION_PAY_CYCLE.SelectedValue & "','PAYROLL','" & myDS.Tables(0).Rows(0).Item(4).ToString & "',''," & spid
                ElseIf ddlOPTION_REPORT.SelectedValue = "ADVANCE" Then
                    ssql1 = "Exec sp_pr_InsAps '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & ddlOPTION_BANK.SelectedValue & "','" & myDS.Tables(0).Rows(0).Item(1).ToString & "'," & myDS.Tables(0).Rows(0).Item(2).ToString & ",'" & ddlOPTION_PAY_TYPE.SelectedValue & "','" & ddlOPTION_PAY_CYCLE.SelectedValue & "','ADVANCE','" & myDS.Tables(0).Rows(0).Item(4).ToString & "',''," & spid
                Else
                    ssql1 = "Exec sp_pr_InsBonusAps '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & ddlOPTION_BANK.SelectedValue & "','" & myDS.Tables(0).Rows(0).Item(1).ToString & "'," & myDS.Tables(0).Rows(0).Item(2).ToString & ",'" & ddlOPTION_PAY_TYPE.SelectedValue & "','" & ddlOPTION_PAY_CYCLE.SelectedValue & "','PAYROLL','" & myDS.Tables(0).Rows(0).Item(4).ToString & "'"
                End If
                myDS1 = New DataSet
                myDS1 = mySQL.ExecuteSQL(ssql1)
                Response.ContentType = "text/plain"
                Response.AppendHeader("Content-Disposition", "attachment; filename=FileName.txt")

                Dim str As New StringBuilder

                For Me.i = 0 To myDS1.Tables(0).Rows.Count - 1
                    str.Append(myDS1.Tables(0).Rows(i).Item(0).ToString)
                    str.AppendLine()
                Next
                Response.Write(str.ToString)
                ssql1 = "exec sp_pr_InsUpdDelEmployeePayrollProcessTemp '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','','" & myDS.Tables(0).Rows(0).Item(2).ToString & "','" & myDS.Tables(0).Rows(0).Item(1).ToString & "','" & ddlOPTION_PAY_CYCLE.SelectedValue & "'," & spid & ",'DEL','" & Session("EmpID").ToString & "'"
                myDS1 = New DataSet()
                myDS1 = mySQL.ExecuteSQL(ssql1)
                Response.TransmitFile(Server.MapPath("~/Pages/Reports/FileName.txt"))
                Response.End()
                myDS1 = Nothing
                ssql1 = Nothing
                str = Nothing
            End If
        End If
        myDS = Nothing
        ssql = Nothing
    End Sub
    Protected Sub ddlOPTION_TYPE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOPTION_TYPE.SelectedIndexChanged
        ssql = "exec [sp_org_getOCPIDObject] '" & ddlOPTION_TYPE.SelectedValue & "_0_1','" & Session("Company") & "','','','','','','',''"
        myDS2 = mySQL.ExecuteSQL(ssql)
        If myDS2.Tables.Count > 0 Then
            If myDS2.Tables(0).Columns.Count > 1 Then
                ddlOCP_ID.DataTextField = "Name"
                ddlOCP_ID.DataValueField = "Code"
                ddlOCP_ID.DataSource = myDS2
                ddlOCP_ID.DataBind()
            End If
        End If
        myDS2 = Nothing
    End Sub

    Protected Sub imgBtnEMPLOYEE_PROFILE_ID_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnEMPLOYEE_PROFILE_ID.Click
        visibleLst()
        lstleft.Items.Clear()
        lstright.Items.Clear()
        txtEMPLOYEE_PROFILE_ID.Text = ""
        Session("action_edit") = "value"
        AutoAdjustPosition("2")
        imgBtnSaveAs.CssClass = buttonPosition1
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

    Private Sub AddRemoveAll(ByVal aSource As ListBox, ByVal aTarget As ListBox)

        Try
            For Me.i = 0 To aSource.Items.Count - 1
                aTarget.Items.Add(aSource.Items(i))
            Next
            aSource.Items.Clear()
        Catch ex As Exception
            lblresult2.Text = "Error: " + ex.Message
        End Try


    End Sub

    Private Sub AddRemoveItem(ByVal aSource As ListBox, ByVal aTarget As ListBox)

        Try
            For Me.i = 0 To aSource.Items.Count - 1
                If aSource.Items(i).Selected Then
                    aTarget.Items.Add(aSource.Items(i))
                End If
            Next
            For Me.i = aSource.Items.Count - 1 To 0 Step -1
                If aSource.Items(i).Selected = True Then
                    aSource.Items.Remove(aSource.Items(i))
                End If
            Next

        Catch ex As Exception
            lblresult2.Text = "Error: " + ex.Message
        End Try

    End Sub

    Protected Sub imgbtnEMPLOYEE_PROFILE_ID2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnEMPLOYEE_PROFILE_ID2.Click
        lstleft.Items.Clear()
        lstright.Items.Clear()
        ssql = "Exec sp_sa_getListBoxValue 'getGroupbyOCPAPS','" & Session("Company") & "','" & ddlOPTION_TYPE.SelectedValue & "','" & ddlOCP_ID.SelectedValue & "','" & Session("EmpID").ToString & "','','',''"
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
End Class
