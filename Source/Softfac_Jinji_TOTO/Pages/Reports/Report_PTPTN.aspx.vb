Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class Pages_Reports_Report_PTPTN
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

        mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourDef, "btnPrint.png")
        'mySetting.GetBtnImgUrl(imgBtnSaveAs, Session("Company").ToString, btnColourDef, "btnSave.png")

        mySetting.GetImgUrl(imgKeyYEAR_MONTH, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyCOMPANY_PROFILE_CODE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyOPTION_PAY_TYPE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnYEAR_MONTH, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnCOMPANY_PROFILE_CODE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgUrl(imgbtnOPTION_PAY_TYPE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)

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
                'imgBtnSaveAs.Visible = AllowPrint
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
        'imgBtnSaveAs.CssClass = buttonPosition2

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

    Protected Sub imgBtnPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnPrint.Click
        Dim clsRpt As New clsReportServices, myTargetURL As String = ""
        myTargetURL = clsRpt.GetPDFReportURL()
        ssql = "select dbo.fn_ReturnCompanyProfileCode(N'" & txtCOMPANY_PROFILE_CODE.Text & "'),dbo.fn_datepart('month',dbo.fn_UnDisplayDate('" & txtYEAR_MONTH.Text & "',dbo.fn_ReturnCompanyProfileCode(N'" & txtCOMPANY_PROFILE_CODE.Text & "'),'" & Session("Module").ToString & "')),dbo.fn_datepart('year',dbo.fn_UnDisplayDate('" & txtYEAR_MONTH.Text & "',dbo.fn_ReturnCompanyProfileCode(N'" & txtCOMPANY_PROFILE_CODE.Text & "'),'" & Session("Module").ToString & "')),@@spid"
        myDS = New DataSet()
        myDS = mySQL.ExecuteSQL(ssql)
        If ValidatePrint(myDS) = True Then
            If myDS.Tables(0).Rows.Count = 1 Then
                If ddlOPTION_REPORT.SelectedValue = "PTPTN" Then
                    myTargetURL = myTargetURL & "&OrganID=" & myDS.Tables(0).Rows(0).Item(0).ToString & "&month=" & myDS.Tables(0).Rows(0).Item(1).ToString & "&year=" & myDS.Tables(0).Rows(0).Item(2).ToString & "&SPID=" & myDS.Tables(0).Rows(0).Item(3).ToString & "&Type=ADD&paytype=" & ddlOPTION_PAY_TYPE.SelectedValue & "&Location=" & ddlOPTION_LOCATION.SelectedValue & "&Module=" & Session("Module").ToString & "&userID=" & Session("EmpID").ToString & "&rdTemplate=pr_GovPTPTN&UserPrepay=" & txtPIC.Text & "&UserPosition=" & txtPIC_POS.Text & "&UserTelNo=" & txtPIC_TEL.Text & "&UserEmail=" & txtPIC_EMAIL.Text & "&Cheque=" & txtCHEQUE.Text
                    Response.Write("<script>window.open('" & myTargetURL & "');</script>")
                    'myTargetURL = clsRpt.GetPDFReportURL()
                    'myTargetURL = myTargetURL & "&OrganID=" & myDS.Tables(0).Rows(0).Item(0).ToString & "&month=" & myDS.Tables(0).Rows(0).Item(1).ToString & "&year=" & myDS.Tables(0).Rows(0).Item(2).ToString & "&SPID=" & myDS.Tables(0).Rows(0).Item(3).ToString & "&Type=ADD&paytype=" & ddlOPTION_PAY_TYPE.SelectedValue & "&Location=" & ddlOPTION_LOCATION.SelectedValue & "&Module=" & Session("Module").ToString & "&userID=" & Session("EmpID").ToString & "&rdTemplate=pr_GovPTPTND"
                    'Response.Write("<script>window.open('" & myTargetURL & "');</script>")
                    'ElseIf ddlOPTION_REPORT.SelectedValue = "CP39A" Then
                    '    myTargetURL = myTargetURL & "&OrganID=" & myDS.Tables(0).Rows(0).Item(0).ToString & "&month=" & myDS.Tables(0).Rows(0).Item(1).ToString & "&year=" & myDS.Tables(0).Rows(0).Item(2).ToString & "&SPID=" & myDS.Tables(0).Rows(0).Item(3).ToString & "&Type=ADD&paytype=" & ddlOPTION_PAY_TYPE.SelectedValue & "&Location=" & ddlOPTION_LOCATION.SelectedValue & "&Module=" & Session("Module").ToString & "&userID=" & Session("EmpID").ToString & "&rdTemplate=pr_GovCP39A"
                    '    Response.Write("<script>window.open('" & myTargetURL & "');</script>")
                    'Else
                    '    myTargetURL = myTargetURL & "&OrganID=" & myDS.Tables(0).Rows(0).Item(0).ToString & "&month=" & myDS.Tables(0).Rows(0).Item(1).ToString & "&year=" & myDS.Tables(0).Rows(0).Item(2).ToString & "&SPID=" & myDS.Tables(0).Rows(0).Item(3).ToString & "&Type=ADD&paytype=" & ddlOPTION_PAY_TYPE.SelectedValue & "&Location=" & ddlOPTION_LOCATION.SelectedValue & "&Module=" & Session("Module").ToString & "&userID=" & Session("EmpID").ToString & "&rdTemplate=pr_GovPCBH"
                    '    Response.Write("<script>window.open('" & myTargetURL & "');</script>")
                End If
            End If
        End If
        ssql = Nothing
        myDS = Nothing
        'myTargetURL = myTargetURL & "&OrganID=JAVA&month=1&year=2008&SPID=50&Type=ADD&paytype=MONTHLYt&userID=hrsa&rdTemplate=pr_GovEPF"
        'Response.Write("<script>window.open('" & myTargetURL & "');</script>")
    End Sub

    'Protected Sub imgBtnSaveAs_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSaveAs.Click
    '    ssql = "select dbo.fn_ReturnCompanyProfileCode(N'" & txtCOMPANY_PROFILE_CODE.Text & "'),dbo.fn_datepart('month',dbo.fn_UnDisplayDate('" & txtYEAR_MONTH.Text & "',dbo.fn_ReturnCompanyProfileCode(N'" & txtCOMPANY_PROFILE_CODE.Text & "'),'" & Session("Module").ToString & "')),dbo.fn_datepart('year',dbo.fn_UnDisplayDate('" & txtYEAR_MONTH.Text & "',dbo.fn_ReturnCompanyProfileCode(N'" & txtCOMPANY_PROFILE_CODE.Text & "'),'" & Session("Module").ToString & "')),@@spid"
    '    myDS = New DataSet()
    '    myDS = mySQL.ExecuteSQL(ssql)
    '    If ValidatePrint(myDS) = True Then
    '        If myDS.Tables(0).Rows.Count = 1 Then
    '            Dim str As New StringBuilder
    '            If ddlOPTION_REPORT.SelectedValue = "PCB" Then
    '                ssql1 = "Exec sp_pr_SelPCBDetail '" & myDS.Tables(0).Rows(0).Item(0).ToString & "'," & myDS.Tables(0).Rows(0).Item(1).ToString & "," & myDS.Tables(0).Rows(0).Item(2).ToString
    '                myDS1 = New DataSet
    '                myDS1 = mySQL.ExecuteSQL(ssql1)
    '                Response.ContentType = "Text/plain"
    '                Response.AppendHeader("Content-Disposition", "attachment; filename=FileName.txt")

    '                For i = 0 To myDS1.Tables(0).Rows.Count - 1
    '                    str.Append(myDS1.Tables(0).Rows(i).Item(0).ToString)
    '                    str.AppendLine()
    '                Next
    '                Response.Write(str.ToString)
    '                Response.TransmitFile(Server.MapPath("~/Pages/Reports/FileName.txt"))
    '                Response.End()
    '                myDS1 = Nothing
    '                ssql1 = Nothing
    '                str = Nothing
    '            ElseIf ddlOPTION_REPORT.SelectedValue = "CP39A" Then
    '                ssql1 = "Exec sp_pr_SelCP39ADetail '" & myDS.Tables(0).Rows(0).Item(0).ToString & "'," & myDS.Tables(0).Rows(0).Item(1).ToString & "," & myDS.Tables(0).Rows(0).Item(2).ToString
    '                myDS1 = New DataSet
    '                myDS1 = mySQL.ExecuteSQL(ssql1)
    '                Response.ContentType = "Text/plain"
    '                Response.AppendHeader("Content-Disposition", "attachment; filename=FileName.txt")

    '                For i = 0 To myDS1.Tables(0).Rows.Count - 1
    '                    str.Append(myDS1.Tables(0).Rows(i).Item(0).ToString)
    '                    str.AppendLine()
    '                Next
    '                Response.Write(str.ToString)
    '                Response.TransmitFile(Server.MapPath("~/Pages/Reports/FileName.txt"))
    '                Response.End()
    '                myDS1 = Nothing
    '                ssql1 = Nothing
    '                str = Nothing
    '            Else
    '                ssql1 = "Exec sp_pr_SelPCBDetail '" & myDS.Tables(0).Rows(0).Item(0).ToString & "'," & myDS.Tables(0).Rows(0).Item(1).ToString & "," & myDS.Tables(0).Rows(0).Item(2).ToString
    '                myDS1 = New DataSet
    '                myDS1 = mySQL.ExecuteSQL(ssql1)
    '                Response.ContentType = "Text/plain"
    '                Response.AppendHeader("Content-Disposition", "attachment; filename=FileName.txt")

    '                For i = 0 To myDS1.Tables(0).Rows.Count - 1
    '                    str.Append(myDS1.Tables(0).Rows(i).Item(0).ToString)
    '                    str.AppendLine()
    '                Next
    '                Response.Write(str.ToString)
    '                Response.TransmitFile(Server.MapPath("~/Pages/Reports/FileName.txt"))
    '                Response.End()
    '                myDS1 = Nothing
    '                ssql1 = Nothing
    '                str = Nothing
    '            End If
    '        End If
    '    End If
    '    myDS = Nothing
    '    ssql = Nothing
    'End Sub

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
            imgtop.CssClass = "Display_0"
            imgbottom.CssClass = panelPosition & autonum
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
        ElseIf IsNumeric(mySetting.ConvertDateToDecimal(txtYEAR_MONTH.Text, Session("Company").ToString, Session("Module").ToString)) = False Then
            lblresult2.Text = "Invalid Input Date Format For [" & lblYEAR_MONTH.Text & "] !"
            txtYEAR_MONTH.Focus()
            Return False
        ElseIf ddlOPTION_PAY_TYPE.SelectedValue = "" Then
            lblresult2.Text = "[" & lblOPTION_PAY_TYPE.Text & "] Is A Required Field!"
            ddlOPTION_PAY_TYPE.Focus()
            Return False
        ElseIf ddlOPTION_LOCATION.SelectedValue = "" Then
            lblresult2.Text = "[" & lblOPTION_LOCATION.Text & "] Is A Required Field!"
            ddlOPTION_LOCATION.Focus()
            Return False
        Else
            Return True
        End If
    End Function
End Class
