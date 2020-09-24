Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration
Imports System.Drawing
Partial Class Pages_TOTO_KPI_IDENTIFY_SKILL_PERSON
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
            blindDirect()
            blindDepartment()
            blindSection()

        End If
    End Sub

    Private Sub PagePreload()
        Session("Module") = "TOTO_KPI"
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")

        mySetting.GetBtnImgUrl(imgBtnPreview, Session("Company").ToString, btnColourDef, "btnPreview.png")
        mySetting.GetBtnImgUrl(imgBtnPrint, Session("Company").ToString, btnColourDef, "btnPrint.png")
        mySetting.GetBtnImgUrl(imgBtnClear, Session("Company").ToString, btnColourDef, "btnClear.png")


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

                'imgBtnPrint.Visible = AllowPrint
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
                Dim ddlYearssql As String, myDS5 As DataSet

                ddlYearssql = "exec sp_is_GetYear '-50'"
                myDS5 = mySQL.ExecuteSQL(ddlYearssql)
                If myDS5.Tables.Count > 0 Then
                    If myDS5.Tables(0).Columns.Count > 1 Then
                        ddlYEAR.DataTextField = "Name"
                        ddlYEAR.DataValueField = "Code"
                        ddlYEAR.DataSource = myDS5
                        ddlYEAR.DataBind()
                        ddlYEAR.SelectedValue = Year(Now())
                    End If
                End If
                myDS5 = Nothing
            End If
            myDS = Nothing
            myDT1 = Nothing
            myDT2 = Nothing
        End If

        AutoAdjustPosition("2")

        imgBtnPreview.CssClass = buttonPosition1
        imgBtnPrint.CssClass = buttonPosition2
        imgBtnClear.CssClass = buttonPosition3

    End Sub
    Protected Sub blindDepartment()
        ssql1 = "Exec sp_KPI_SelRptDropDownlist '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & ddlCATEGORY.SelectedValue & "','','" & ddlOCP_ID_SECTION.SelectedValue & "','','','','','','BLINDDept','" & ddlYEAR.SelectedValue & "'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlOCP_ID_DEPARTMENT.DataSource = myDS1.Tables(0)
            ddlOCP_ID_DEPARTMENT.DataTextField = "Name"
            ddlOCP_ID_DEPARTMENT.DataValueField = "Code"
            ddlOCP_ID_DEPARTMENT.DataBind()
        End If
    End Sub

    Protected Sub blindSection()
        ssql1 = "Exec sp_KPI_SelRptDropDownlist '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & ddlCATEGORY.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','','','','','','','BLINDSect','" & ddlYEAR.SelectedValue & "'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlOCP_ID_SECTION.DataSource = myDS1.Tables(0)
            ddlOCP_ID_SECTION.DataTextField = "Name"
            ddlOCP_ID_SECTION.DataValueField = "Code"
            ddlOCP_ID_SECTION.DataBind()
        End If
    End Sub

    Protected Sub blindProcess()
        Dim processselect As String

        processselect = ddlPROCESS.SelectedValue

        ssql1 = "Exec sp_KPI_SelRptDropDownlist '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & ddlCATEGORY.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','" & ddlOCP_ID_SECTION.SelectedValue & "','" & ddlPROCESS.SelectedValue & "','" & ddlITEM.SelectedValue & "','" & ddlMODEL.SelectedValue & "','" & ddlCOUNTRY.SelectedValue & "','" & ddlSTATION.SelectedValue & "','BLINDProcess','" & ddlYEAR.SelectedValue & "'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlPROCESS.DataSource = myDS1.Tables(0)
            ddlPROCESS.DataTextField = "Name"
            ddlPROCESS.DataValueField = "Code"
            ddlPROCESS.DataBind()
        End If

        If processselect <> "" Then
            ddlPROCESS.SelectedValue = processselect
        End If
    End Sub
    Protected Sub blindItem()
        Dim itemselect As String

        itemselect = ddlITEM.SelectedValue
        ssql1 = "Exec sp_KPI_SelRptDropDownlist '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & ddlCATEGORY.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','" & ddlOCP_ID_SECTION.SelectedValue & "','" & ddlPROCESS.SelectedValue & "','" & ddlITEM.SelectedValue & "','" & ddlMODEL.SelectedValue & "','" & ddlCOUNTRY.SelectedValue & "','" & ddlSTATION.SelectedValue & "','BLINDItem','" & ddlYEAR.SelectedValue & "'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlITEM.DataSource = myDS1.Tables(0)
            ddlITEM.DataTextField = "Name"
            ddlITEM.DataValueField = "Code"
            ddlITEM.DataBind()
        End If

        If itemselect <> "" Then
            ddlITEM.SelectedValue = itemselect
        End If
    End Sub

    Protected Sub blindModel()
        Dim modelselect As String

        modelselect = ddlMODEL.SelectedValue

        ssql1 = "Exec sp_KPI_SelRptDropDownlist '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & ddlCATEGORY.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','" & ddlOCP_ID_SECTION.SelectedValue & "','" & ddlPROCESS.SelectedValue & "','" & ddlITEM.SelectedValue & "','" & ddlMODEL.SelectedValue & "','" & ddlCOUNTRY.SelectedValue & "','" & ddlSTATION.SelectedValue & "','BLINDModel','" & ddlYEAR.SelectedValue & "'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlMODEL.DataSource = myDS1.Tables(0)
            ddlMODEL.DataTextField = "Name"
            ddlMODEL.DataValueField = "Code"
            ddlMODEL.DataBind()
        End If

        If modelselect <> "" Then
            ddlMODEL.SelectedValue = modelselect
        End If
    End Sub

    Protected Sub blindCountry()
        Dim countryselect As String

        countryselect = ddlCOUNTRY.SelectedValue

        ssql1 = "Exec sp_KPI_SelRptDropDownlist '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & ddlCATEGORY.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','" & ddlOCP_ID_SECTION.SelectedValue & "','" & ddlPROCESS.SelectedValue & "','" & ddlITEM.SelectedValue & "','" & ddlMODEL.SelectedValue & "','','" & ddlSTATION.SelectedValue & "','BLINDCountry','" & ddlYEAR.SelectedValue & "'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlCOUNTRY.DataSource = myDS1.Tables(0)
            ddlCOUNTRY.DataTextField = "Name"
            ddlCOUNTRY.DataValueField = "Code"
            ddlCOUNTRY.DataBind()
        End If

        If countryselect <> "" Then
            ddlCOUNTRY.SelectedValue = countryselect
        End If
    End Sub

    Protected Sub blindStation()
        Dim stationselect As String

        stationselect = ddlSTATION.SelectedValue

        ssql1 = "Exec sp_KPI_SelRptDropDownlist '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & ddlCATEGORY.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','" & ddlOCP_ID_SECTION.SelectedValue & "','" & ddlPROCESS.SelectedValue & "','" & ddlITEM.SelectedValue & "','" & ddlMODEL.SelectedValue & "','" & ddlCOUNTRY.SelectedValue & "','','BLINDStation','" & ddlYEAR.SelectedValue & "'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlSTATION.DataSource = myDS1.Tables(0)
            ddlSTATION.DataTextField = "Name"
            ddlSTATION.DataValueField = "Code"
            ddlSTATION.DataBind()
        End If

        If stationselect <> "" Then
            ddlSTATION.SelectedValue = stationselect
        End If
    End Sub

    Protected Sub blindDirect()
        Dim direct As String
        direct = ddlCATEGORY.SelectedValue

        ssql1 = "Exec sp_KPI_SelRptDropDownlist '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','','','','','','','','','BLINDDirect','" & ddlYEAR.SelectedValue & "'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlCATEGORY.DataSource = myDS1.Tables(0)
            ddlCATEGORY.DataTextField = "Name"
            ddlCATEGORY.DataValueField = "Code"
            ddlCATEGORY.DataBind()

            If direct <> "" Then
                ddlCATEGORY.SelectedValue = direct
            Else
                ddlCATEGORY.SelectedValue = "DIRECT"
            End If
        End If
    End Sub

   
    Protected Sub blindFinal()

        ssql1 = "Exec sp_KPI_RptIdentifySkillPerson '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & ddlCATEGORY.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','" & ddlOCP_ID_SECTION.SelectedValue & "','" & ddlPROCESS.SelectedValue & "','" & ddlITEM.SelectedValue & "','" & ddlMODEL.SelectedValue & "','" & ddlCOUNTRY.SelectedValue & "','" & ddlSTATION.SelectedValue & "','VIEW','" & ddlYEAR.SelectedValue & "'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count = 1 Then
            gv1.DataSource = myDS1.Tables(0)
            gv1.DataBind()
        End If
        ssql1 = Nothing
        myDS1 = Nothing
    End Sub
    Protected Sub imgBtnPreview_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnPreview.Click
        If ValidatePrint() Then
            blindFinal()
        End If
    End Sub
    Protected Sub imgBtnPrint_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnPrint.Click
        If gv1.Rows.Count > 0 Then
            ExportToExcel(sender, e)
        Else
            ShowMessage("Need to Preview First!")
        End If
    End Sub

    Protected Sub ExportToExcel(ByVal sender As Object, ByVal e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=" & lblTitle.Text & ".xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages

            PrepareForExport(gv1)
            'PrepareForExport(gv2)

            Dim tb As New Table()
            Dim tr1 As New TableRow()
            Dim cell1 As New TableCell()
            cell1.Controls.Add(gv1)
            tr1.Cells.Add(cell1)
            'Dim cell3 As New TableCell()
            'cell3.Controls.Add(gv2)
            Dim cell2 As New TableCell()
            cell2.Text = "&nbsp;"
            Dim cell4 As New TableCell()
            cell4.Text = lblgv1.Text
            cell4.ForeColor = Color.Red

            'Dim cell5 As New TableCell()
            'cell5.Text = "Evaluation Form Skill"
            'cell5.ForeColor = Color.Red

            Dim tr2 As New TableRow()
            tr2.Cells.Add(cell2)
            'Dim tr3 As New TableRow()
            'tr3.Cells.Add(cell3)
            Dim tr4 As New TableRow()
            tr4.Cells.Add(cell4)
            'Dim tr5 As New TableRow()
            'tr5.Cells.Add(cell5)
            tb.Rows.Add(tr4)
            tb.Rows.Add(tr1)
            tb.Rows.Add(tr2)
            'tb.Rows.Add(tr5)
            'tb.Rows.Add(tr3)

            tb.RenderControl(hw)

            'style to format numbers to string 
            Dim style As String = "<style> .textmode { mso-number-format:\@; } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()
        End Using
    End Sub

    Protected Sub PrepareForExport(ByVal Gridview As GridView)

        'Change the Header Row back to white color
        Gridview.HeaderRow.Style.Add("background-color", "#FFFFFF")

        'Apply style to Individual Cells
        For k As Integer = 0 To Gridview.HeaderRow.Cells.Count - 1
            Gridview.HeaderRow.Cells(k).Style.Add("background-color", "#988675")
        Next

        For i As Integer = 0 To Gridview.Rows.Count - 1
            Dim row As GridViewRow = Gridview.Rows(i)

            'Change Color back to white
            row.BackColor = System.Drawing.Color.White

            'Apply text style to each Row
            row.Attributes.Add("class", "dgstyle_i")

            'Apply style to Individual Cells of Alternating Row
            If i Mod 2 <> 0 Then
                For j As Integer = 0 To Gridview.Rows(i).Cells.Count - 1
                    row.Cells(j).Style.Add("background-color", "#F2F4FF")
                Next
            End If
        Next
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Verifies that the control is rendered
    End Sub

    Private Sub ShowMessage(ByVal message As String)
        Dim scriptString As String = "<script language=JavaScript>"
        scriptString += "alert('" + message + "');"
        scriptString += "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(GetType(Page), "ShowMessage", scriptString)
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
                    Dim myImage As System.Web.UI.WebControls.Image = Page.FindControl("imgKey" & Trim(myDS.Tables(0).Rows(i).Item(code)))
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

    Private Function ValidatePrint() As Boolean
        If ddlCATEGORY.SelectedValue = "" Then
            lblresult2.Text = "[" & lblCATEGORY.Text & "] Is A Required Field!"
            ShowMessage(lblresult2.Text)
            ddlCATEGORY.Focus()
            Return False
        ElseIf ddlOCP_ID_DEPARTMENT.SelectedValue = "" Then
            lblresult2.Text = "[" & lblOCP_ID_DEPARTMENT.Text & "] Is A Required Field!"
            ShowMessage(lblresult2.Text)
            ddlOCP_ID_DEPARTMENT.Focus()
            Return False
        ElseIf ddlOCP_ID_SECTION.SelectedValue = "" And ddlOCP_ID_SECTION.Items.Count > 1 Then
            lblresult2.Text = "[" & lblOCP_ID_SECTION.Text & "] Is A Required Field!"
            ShowMessage(lblresult2.Text)
            ddlOCP_ID_SECTION.Focus()
            Return False
        ElseIf ddlPROCESS.SelectedValue = "" And ddlPROCESS.Items.Count > 1 Then
            lblresult2.Text = "[" & lblPROCESS.Text & "] Is A Required Field!"
            ShowMessage(lblresult2.Text)
            ddlPROCESS.Focus()
            Return False
        ElseIf ddlITEM.SelectedValue = "" And ddlITEM.Items.Count > 1 Then
            lblresult2.Text = "[" & lblITEM.Text & "] Is A Required Field!"
            ShowMessage(lblresult2.Text)
            ddlITEM.Focus()
            Return False
        ElseIf ddlMODEL.SelectedValue = "" And ddlMODEL.Items.Count > 1 Then
            lblresult2.Text = "[" & lblMODEL.Text & "] Is A Required Field!"
            ShowMessage(lblresult2.Text)
            ddlMODEL.Focus()
            Return False
        ElseIf ddlCOUNTRY.SelectedValue = "" And ddlCOUNTRY.Items.Count > 1 Then
            lblresult2.Text = "[" & lblCOUNTRY.Text & "] Is A Required Field!"
            ShowMessage(lblresult2.Text)
            ddlCOUNTRY.Focus()
            Return False
        ElseIf ddlSTATION.SelectedValue = "" And ddlSTATION.Items.Count > 1 Then
            lblresult2.Text = "[" & lblSTATION.Text & "] Is A Required Field!"
            ShowMessage(lblresult2.Text)
            ddlSTATION.Focus()
            Return False
        Else
            Return True
        End If
    End Function


    Protected Sub ddlCATEGORY_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCATEGORY.SelectedIndexChanged
        ddlPROCESS.Items.Clear()
        ddlITEM.Items.Clear()
        ddlMODEL.Items.Clear()
        ddlCOUNTRY.Items.Clear()
        ddlSTATION.Items.Clear()
        ddlOCP_ID_DEPARTMENT.Items.Clear()
        ddlOCP_ID_SECTION.Items.Clear()
        blindDepartment()
        blindSection()
    End Sub

    Protected Sub ddlOCP_ID_DEPARTMENT_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOCP_ID_DEPARTMENT.SelectedIndexChanged

        ssql1 = "Exec sp_KPI_SelRptDropDownlist '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & ddlCATEGORY.SelectedValue & "','" & ddlOCP_ID_DEPARTMENT.SelectedValue & "','','','','','','','BLINDSect','" & ddlYEAR.SelectedValue & "'"
        myDS1 = New DataSet()
        myDS1 = mySQL.ExecuteSQL(ssql1)
        If myDS1.Tables.Count > 0 Then
            ddlOCP_ID_SECTION.DataSource = myDS1.Tables(0)
            ddlOCP_ID_SECTION.DataTextField = "Name"
            ddlOCP_ID_SECTION.DataValueField = "Code"
            ddlOCP_ID_SECTION.DataBind()

            If myDS1.Tables(0).Rows.Count = 2 And ddlOCP_ID_DEPARTMENT.SelectedValue <> "" Then
                ddlOCP_ID_SECTION.SelectedIndex = 1
                ddlOCP_ID_SECTION_SelectedIndexChanged(Nothing, Nothing)
            End If
        End If


        ssql1 = Nothing
        myDS1 = Nothing

        If ddlCATEGORY.SelectedValue = "INDIRECT" And ddlOCP_ID_SECTION.Items.Count < 2 Then
            ddlPROCESS.Items.Clear()
            blindProcess()
        ElseIf ddlOCP_ID_SECTION.SelectedValue = "" Then
            ddlPROCESS.Items.Clear()
        End If
    End Sub

    Protected Sub ddlOCP_ID_SECTION_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOCP_ID_SECTION.SelectedIndexChanged
        Dim department As String

        department = ddlOCP_ID_DEPARTMENT.SelectedValue

        If department = "" Then
            ssql1 = "Exec sp_KPI_SelRptDropDownlist '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & ddlCATEGORY.SelectedValue & "','','" & ddlOCP_ID_SECTION.SelectedValue & "','','','','','','BLINDDect','" & ddlYEAR.SelectedValue & "'"
            myDS1 = New DataSet()
            myDS1 = mySQL.ExecuteSQL(ssql1)
            If myDS1.Tables.Count > 0 Then
                ddlOCP_ID_DEPARTMENT.DataSource = myDS1.Tables(0)
                ddlOCP_ID_DEPARTMENT.DataTextField = "Name"
                ddlOCP_ID_DEPARTMENT.DataValueField = "Code"
                ddlOCP_ID_DEPARTMENT.DataBind()

                If myDS1.Tables(0).Rows.Count = 2 And ddlOCP_ID_SECTION.SelectedValue <> "" Then
                    ddlOCP_ID_DEPARTMENT.SelectedIndex = 1
                End If
            End If

            ssql1 = Nothing
            myDS1 = Nothing
        End If
        If ddlCATEGORY.SelectedValue = "DIRECT" Then
            ddlPROCESS.Items.Clear()
            ddlITEM.Items.Clear()
            ddlMODEL.Items.Clear()
            ddlCOUNTRY.Items.Clear()
            ddlSTATION.Items.Clear()
            blindProcess()
            blindItem()
            blindModel()
            blindCountry()
            blindStation()
        Else
            ddlPROCESS.Items.Clear()
            ddlITEM.Items.Clear()
            ddlMODEL.Items.Clear()
            ddlCOUNTRY.Items.Clear()
            ddlSTATION.Items.Clear()
            blindProcess()
            blindItem()
        End If
    End Sub

    Protected Sub ddlPROCESS_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPROCESS.SelectedIndexChanged

        blindItem()
        blindModel()
        blindCountry()
        blindStation()

    End Sub

    Protected Sub ddlMODEL_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMODEL.SelectedIndexChanged
        

        blindCountry()
        blindItem()
        blindProcess()
        blindStation()


    End Sub

    Protected Sub ddlITEM_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlITEM.SelectedIndexChanged
        
        blindModel()
        blindCountry()
        blindStation()
        blindProcess()

    End Sub

    Protected Sub ddlCOUNTRY_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCOUNTRY.SelectedIndexChanged
        
        blindStation()
        blindModel()
        blindItem()
        blindProcess()

    End Sub

    Protected Sub imgBtnClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnClear.Click
        ddlPROCESS.Items.Clear()
        ddlITEM.Items.Clear()
        ddlMODEL.Items.Clear()
        ddlCOUNTRY.Items.Clear()
        ddlSTATION.Items.Clear()
        ddlOCP_ID_DEPARTMENT.Items.Clear()
        ddlOCP_ID_SECTION.Items.Clear()
        blindDepartment()
        blindSection()
        gv1.DataBind()
    End Sub

    Protected Sub ddlSTATION_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSTATION.SelectedIndexChanged
        blindCountry()
        blindModel()
        blindItem()
        blindProcess()
    End Sub
    Protected Sub ddlYEAR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlYEAR.SelectedIndexChanged
        blindDirect()
        blindDepartment()
        blindSection()
    End Sub
End Class

