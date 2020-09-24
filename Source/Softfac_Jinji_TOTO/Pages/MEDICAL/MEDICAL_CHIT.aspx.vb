Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class Pages_MEDICAL_MEDICAL_CHIT
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
        Session("Module") = "MEDICAL"
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")

        mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourDef, "btnPrint.png")

        mySetting.GetImgTypeUrl(imgTop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Add.png")
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
        EnableAll()
        AutoAdjustPosition("2")

        imgBtnPrint.CssClass = buttonPosition1

    End Sub

    Protected Sub imgBtnPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnPrint.Click
        Dim clsRpt As New clsReportServices, myTargetURL As String = ""
        myTargetURL = clsRpt.GetPDFReportURL()
        ssql = "select dbo.fn_ReturnCompanyProfileCode(N'" & txtCOMPANY_PROFILE_CODE.Text & "')," & _
        "dbo.fn_ReturnEmpIDByCodeName('" & Session("Company").ToString & "','" & txtEMPLOYEE_PROFILE_ID.Text & "')," & _
        "dbo.fn_GetCode_MEDICAL_CLAIMTYPE_Vw('" & txtCLAIMTYPE.Text & "')," & _
        "dbo.fn_GetCode_MEDICAL_PANELDOCTOR_Vw('" & txtPANELDOCTOR_PROFILE.Text & "')"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

        If ValidatePrint(myDS) Then
            If myDS.Tables(0).Rows.Count > 0 Then
                ssql1 = "exec sp_md_GenerateMedical '" & myDS.Tables(0).Rows(0).Item(0).ToString & "','" & _
                myDS.Tables(0).Rows(0).Item(1).ToString & "','" & myDS.Tables(0).Rows(0).Item(2).ToString & _
                "','" & myDS.Tables(0).Rows(0).Item(3).ToString & "','" & ddlBENEFICIARYVISITER.SelectedValue & "','" & _
                mySetting.ConvertDateToDecimal(Trim(txtVISITDATE.Text), Session("Company").ToString, Session("Module").ToString) & "','" & _
                txtREASON.Text & "'," & txtENTITLEMENTAMT.Text & "," & txtUPTODATE_BALANCE.Text & "," & txtCLAIMAMT.Text & ",'" & _
                Session("EmpID").ToString & "','GENERATE'"
                myDS1 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)

                If myDS1.Tables.Count > 0 Then
                    If myDS1.Tables(0).Rows.Count > 0 Then
                        myTargetURL = myTargetURL & "&Type=CHIT&AutoApprovalNo=" & myDS1.Tables(0).Rows(0).Item(0).ToString & "&rdTemplate=MedicalChit"
                        Response.Write("<script>window.open('" & myTargetURL & "');</script>")
                        ProcessCompleted()
                    End If
                End If
                ssql1 = Nothing
                myDS1 = Nothing
            End If
        End If
    End Sub

    Private Sub ShowMessage(ByVal message As String)
        Dim scriptString As String = "<script language=JavaScript>"
        scriptString += "alert('" + message + "');"
        scriptString += "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "ShowMessage", scriptString)
    End Sub

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
            imgTop.CssClass = "Display_0"
            imgBottom.CssClass = panelPosition & autonum
        Catch ex As Exception
            lblresult2.Text = "[AutoAdjustPosition]Error: " & ex.Message
            'SetFieldToFalse()
        End Try

    End Sub

    Private Function ValidatePrint(ByVal myds As DataSet) As Boolean
        If myds.Tables(0).Rows(0).Item(0).ToString = "" Then
            lblresult2.Text = "[" & lblCOMPANY_PROFILE_CODE.Text & "] Is A Required Field!"
            txtCOMPANY_PROFILE_CODE.Focus()
            Return False
        ElseIf myds.Tables(0).Rows(0).Item(1).ToString = "" Then
            lblresult2.Text = "[" & lblEMPLOYEE_PROFILE_ID.Text & "] Is A Required Field!"
            txtEMPLOYEE_PROFILE_ID.Focus()
            Return False
        ElseIf myds.Tables(0).Rows(0).Item(2).ToString = "" Then
            lblresult2.Text = "[" & lblCLAIMTYPE.Text & "] Is A Required Field!"
            txtCLAIMTYPE.Focus()
            Return False
        ElseIf myds.Tables(0).Rows(0).Item(3).ToString = "" Then
            lblresult2.Text = "[" & lblPANELDOCTOR_PROFILE.Text & "] Is A Required Field!"
            txtPANELDOCTOR_PROFILE.Focus()
            Return False
        ElseIf IsNumeric(mySetting.ConvertDateToDecimal(Trim(txtVISITDATE.Text), Session("Company").ToString, Session("Module").ToString)) = False Then
            lblresult2.Text = "[" & lblVISITDATE.Text & "] Is A Required Field! Invalid Input DateTime Format!"
            txtVISITDATE.Focus()
            Return False
        ElseIf IsNumeric(txtENTITLEMENTAMT.Text) = False Then
            lblresult2.Text = "[" & lblENTITLEMENTAMT.Text & "] Is Invalid Input Numeric Format!"
            txtENTITLEMENTAMT.Focus()
            Return False
        ElseIf IsNumeric(txtUPTODATE_BALANCE.Text) = False Then
            lblresult2.Text = "[" & lblUPTODATE_BALANCE.Text & "] Is Invalid Input Numeric Format!"
            txtUPTODATE_BALANCE.Focus()
            Return False
        ElseIf IsNumeric(txtCLAIMAMT.Text) = False Then
            lblresult2.Text = "[" & lblCLAIMAMT.Text & "] Is Invalid Input Numeric Format!"
            txtCLAIMAMT.Focus()
            Return False
        Else
            Return True
        End If
    End Function
    Private Function ValidatePrint1(ByVal myds As DataSet) As Boolean
        If myds.Tables(0).Rows(0).Item(0).ToString = "" Then
            lblresult2.Text = "[" & lblCOMPANY_PROFILE_CODE.Text & "] Is A Required Field!"
            txtCOMPANY_PROFILE_CODE.Focus()
            Return False
        ElseIf myds.Tables(0).Rows(0).Item(1).ToString = "" Then
            lblresult2.Text = "[" & lblEMPLOYEE_PROFILE_ID.Text & "] Is A Required Field!"
            txtEMPLOYEE_PROFILE_ID.Focus()
            Return False
        ElseIf myds.Tables(0).Rows(0).Item(2).ToString = "" Then
            lblresult2.Text = "[" & lblCLAIMTYPE.Text & "] Is A Required Field!"
            txtCLAIMTYPE.Focus()
            Return False
        Else
            Return True
        End If
    End Function

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
            If myDS.Tables(0).Rows(i).Item(0).ToString.Trim = "VISITDATE" Then
                myImageButton.Enabled = False
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

    Protected Sub txtEMPLOYEE_PROFILE_ID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEMPLOYEE_PROFILE_ID.TextChanged
        mySetting.GetLookupValue_ImageButton(imgbtnCLAIMTYPE, Form.ID, "txtCLAIMTYPE", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "CLAIMTYPE" & """," & """" & """," & """" & Session("EmpID").ToString & """", txtEMPLOYEE_PROFILE_ID.Text)
    End Sub

    Protected Sub txtCLAIMTYPE_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCLAIMTYPE.TextChanged
        ssql = "select dbo.fn_ReturnCompanyProfileCode(N'" & txtCOMPANY_PROFILE_CODE.Text & "'),dbo.fn_ReturnEmpIDByCodeName(N'" & Session("Company").ToString & "',N'" & txtEMPLOYEE_PROFILE_ID.Text & "'), dbo.fn_GetCode_MEDICAL_CLAIMTYPE_Vw(N'" & txtCLAIMTYPE.Text & "')"
        myDS = mySQL.ExecuteSQL(ssql)

        If ValidatePrint1(myDS) = True Then
            If myDS.Tables(0).Rows.Count = 1 Then
                ssql1 = "Exec sp_md_insBeneficiaryVisiter """ & myDS.Tables(0).Rows(0).Item(0).ToString & """,""" & myDS.Tables(0).Rows(0).Item(1).ToString & """,""" & myDS.Tables(0).Rows(0).Item(2).ToString & """"
                myDS2 = mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)

                If myDS2.Tables.Count > 0 Then
                    If myDS2.Tables(0).Rows.Count > 0 Then
                        ddlBENEFICIARYVISITER.DataTextField = "Name"
                        ddlBENEFICIARYVISITER.DataValueField = "Code"
                        ddlBENEFICIARYVISITER.DataSource = myDS2
                        ddlBENEFICIARYVISITER.DataBind()
                    End If
                End If
            End If
        End If
        txtVISITDATE.Enabled = False
        txtENTITLEMENTAMT.Enabled = False
        txtUPTODATE_BALANCE.Enabled = False
        txtCLAIMAMT.Enabled = False
    End Sub
    Private Sub ProcessCompleted()

        Dim strScript As String = "<script language=" & """" & "javascript" & """" & ">"
        strScript &= "alert('Generate Completed!');"
        strScript &= "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "Generate Completed!", strScript)
        ClearText()
        'pnlEdit.Visible = False
        'pnldescription.Visible = True
    End Sub
    Private Sub ClearText()
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL("SELECT Code,Option_Data_Type FROM Table_Field Where Table_Profile_Code='" & Form.ID & "' AND Option_View_Card='YES' ORDER BY SEQUENCE_NO", Session("Company").ToString, Session("EmpID").ToString)
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
                    myTXT.Enabled = True
                    myTXT = Nothing
            End Select
        Next
        myDS = Nothing
    End Sub

End Class
