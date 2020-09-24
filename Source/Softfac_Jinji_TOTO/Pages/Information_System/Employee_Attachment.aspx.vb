Imports System.Data
Imports System.IO
Partial Class Pages_Information_System_Employee_Attachment
    Inherits System.Web.UI.Page
    Dim ssql As String
    Private WithEvents myDS As New DataSet, mySQL As New clsSQL, mySetting As New clsGlobalSetting
    Public _currentPageNumber As Integer, _totalPage As Double = 1, _totalRecords As Integer
    Dim btnColourDef, btnColourAlt As String
    Dim strPath As String = "../../Images"
    Dim logic As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.IsNewSession Then
            Response.Redirect("../Global/SessionTimeOut.aspx")
        End If
        If Not IsPostBack Then
            Response.CacheControl = "no-cache"
            Response.AddHeader("Pragma", "no-cache")
            Response.Expires = -1
            CheckIfDirectoriesExists()
            PagePreload()
        Else
            lblMessage.Visible = False
        End If
    End Sub
    Private Sub CheckIfDirectoriesExists()
        If Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "Images\Company\" & Session("Company").ToString & "\EmpPhoto\") Then
            'Do nothing
        Else
            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "Images\Company\" & Session("Company").ToString & "\EmpPhoto\")
        End If
        If Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "Images\Company\" & Session("Company").ToString & "\EmpAttachment\") Then
            'Do nothing
        Else
            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "Images\Company\" & Session("Company").ToString & "\EmpAttachment\")
        End If
        If Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "Temp\") Then
            'Do nothing
        Else
            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "Temp\")
        End If
        If Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "Temp\Images\") Then
            'Do nothing
        Else
            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "Temp\Images\")
        End If
        If Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "Temp\Attachments\") Then
            'Do nothing
        Else
            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "Temp\Attachments\")
        End If
    End Sub
    Private Sub PagePreload()
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")
        Session("Module") = "INFORMATION_SYSTEM"
        'mySetting.GetBtnImgUrl(imgbtnSubmit, Session("Company").ToString, btnColourDef, "btnSubmit.gif")

        'get Page Title
        ssql = "exec sp_sa_GetPageTitle '" & Form.ID.ToString & "'"
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            lblTitle.Text = myDS.Tables(0).Rows(0).Item(0)
            lblTitle.CssClass = "wordstyle4"
        End If
        myDS = Nothing
        mySetting.GetImgTypeUrl(imgtop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Add.png")
        mySetting.GetImgTypeUrl(imgbottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")

        lblMessage.CssClass = "wordstyle2"

        mySetting.GetBtnImgUrl(imgBtnSelect, Session("Company").ToString, btnColourDef, "btnSelect.png")
        mySetting.GetBtnImgUrl(imgBtnDelete, Session("Company").ToString, btnColourDef, "btnDelete.png")
        mySetting.GetBtnImgUrl(imgBtnGoToPage1, Session("Company").ToString, btnColourDef, "btnGo.png")

        mySetting.GetBtnImgUrl(imgbtnSelectAll, Session("Company").ToString, btnColourDef, "addall.png")
        mySetting.GetBtnImgUrl(imgbtnSelectOne, Session("Company").ToString, btnColourDef, "additem.png")
        mySetting.GetBtnImgUrl(imgbtnRemoveAll, Session("Company").ToString, btnColourDef, "removeall.png")
        mySetting.GetBtnImgUrl(imgbtnRemoveOne, Session("Company").ToString, btnColourDef, "removeitem.png")

        mySetting.GetDropdownlistValue("LEAVE_APPROVAL", "Option_Type", ddlDEPARTMENT_CODE)
        mySetting.GetImgUrl(imgKeyDEPARTMENT, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnSEARCH_DEPARTMENT, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetLookupValue_ImageButton(imgbtnSEARCH_DEPARTMENT, Form.ID, "txtEMPLOYEE_PROFILE_ID", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "EMPLOYEE_PROFILE_ID" & """," & """" & """")

        body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

        lblDEPARTMENT.Style.Add("font-family ", "Arial Unicode MS,Verdana,Arial,Tahoma")
        lblDEPARTMENT.Style.Add("font-weight", "bold")
        lblDEPARTMENT.Style.Add("font-size", "11px")
        lblDEPARTMENT.Style.Add("color", "#000000")
        lblDEPARTMENT.Text = "Department Code "
        txtDEPARTMENT_NAME.Text = " Department Name "
        txtDEPARTMENT_NAME.CssClass = "wordstyle7"
        txtDEPARTMENT_NAME.Attributes.Add("onFocus", "focus_txtdepartmentname()")
        txtDEPARTMENT_NAME.Attributes.Add("onblur", "blur_txtdepartmentname()")
        mySetting.SetTextBoxPressEnterGoToImageButton(txtDEPARTMENT_NAME, imgbtnSEARCH_DEPARTMENT)


        mySetting.GetImgUrl(imgKeySEARCH_EMPLOYEE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnSEARCH_EMPLOYEE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        lblSEARCH_EMPLOYEE.Style.Add("font-family ", "Arial Unicode MS,Verdana,Arial,Tahoma")
        lblSEARCH_EMPLOYEE.Style.Add("font-weight", "bold")
        lblSEARCH_EMPLOYEE.Style.Add("font-size", "11px")
        lblSEARCH_EMPLOYEE.Style.Add("color", "#000000")
        lblSEARCH_EMPLOYEE.Text = " Employee Code "
        txtSEARCH_EMPLOYEE_CODE.Text = " Employee Code "
        txtSEARCH_EMPLOYEE_CODE.CssClass = "wordstyle7"
        txtSEARCH_EMPLOYEE_NAME.Text = " Employee Name "
        txtSEARCH_EMPLOYEE_NAME.CssClass = "wordstyle7"
        txtSEARCH_EMPLOYEE_CODE.Attributes.Add("onFocus", "focus_txtempid()")
        txtSEARCH_EMPLOYEE_CODE.Attributes.Add("onblur", "blur_txtempid()")
        txtSEARCH_EMPLOYEE_NAME.Attributes.Add("onFocus", "focus_txtempname()")
        txtSEARCH_EMPLOYEE_NAME.Attributes.Add("onblur", "blur_txtempname()")
        mySetting.SetTextBoxPressEnterGoToImageButton(txtGoToPage1, imgBtnGoToPage1)
        mySetting.SetTextBoxPressEnterGoToImageButton(txtSEARCH_EMPLOYEE_CODE, imgbtnSEARCH_EMPLOYEE)
        mySetting.SetTextBoxPressEnterGoToImageButton(txtSEARCH_EMPLOYEE_NAME, imgbtnSEARCH_EMPLOYEE)

        lblEFFECTIVE_DATE.Style.Add("font-family ", "Arial Unicode MS,Verdana,Arial,Tahoma")
        lblEFFECTIVE_DATE.Style.Add("font-weight", "bold")
        lblEFFECTIVE_DATE.Style.Add("font-size", "11px")
        lblEFFECTIVE_DATE.Style.Add("color", "#000000")
        lblEFFECTIVE_DATE.Text = "Effective Date"
        mySetting.GetImgUrl(imgKeyEFFECTIVE_DATE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnEFFECTIVE_DATE, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
        mySetting.PopUpCalendar_ImageButton(imgbtnEFFECTIVE_DATE, Form.ID, "txtEFFECTIVE_DATE")

        rdobtnByEmployee.Checked = True
        rdobtnByDepartment.Checked = False
        imgKeyDEPARTMENT.Enabled = False
        lblDEPARTMENT.Enabled = False
        ddlDEPARTMENT_CODE.Enabled = False
        txtDEPARTMENT_NAME.Enabled = False
        imgbtnSEARCH_DEPARTMENT.Enabled = False

        lblEMPLOYEE_PROFILE_ID.Text = "Employee Profile ID"
        lblEMPLOYEE_PROFILE_ID.Style.Add("font-family ", "Arial Unicode MS,Verdana,Arial,Tahoma")
        lblEMPLOYEE_PROFILE_ID.Style.Add("font-weight", "bold")
        lblEMPLOYEE_PROFILE_ID.Style.Add("font-size", "11px")
        lblEMPLOYEE_PROFILE_ID.Style.Add("color", "#000000")

        mySetting.GetLookupValue_ImageButton(imgbtnEMPLOYEE_PROFILE_ID, Form.ID, "txtEMPLOYEE_PROFILE_ID", "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "EMPLOYEE_PROFILE_ID" & """," & """" & """")
        mySetting.GetImgUrl(imgKeyEMPLOYEE_PROFILE_ID, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnEMPLOYEE_PROFILE_ID, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        imgKeyEMPLOYEE_PROFILE_ID.Enabled = False

        imgbtnEMPLOYEE_PROFILE_ID.Visible = False
        txtEMPLOYEE_PROFILE_ID.Enabled = False
        pnlEmployee.Visible = False
        tblUpload.Visible = False

        Dim dt As New DataTable
        'dt.Columns.Add("Select")
        dt.Columns.Add("Image")
        dt.Columns.Add("Employee Profile ID")
        dt.Columns.Add("Effective Date")
        dt.PrimaryKey = New DataColumn() {dt.Columns(0), dt.Columns(1), dt.Columns(2)}
        Session("MyDT") = dt
        If Not Session("MyDT") Is Nothing Then
            myGridView.DataSource = CType(Session("MyDT"), DataTable).DefaultView
            myGridView.DataBind()
        End If
    End Sub

    Private Sub BindGrid()
        Try

            If Trim(txtSEARCH_EMPLOYEE_CODE.Text) <> "Employee Code" And Trim(txtSEARCH_EMPLOYEE_NAME.Text) <> "Employee Name" Then
                ssql = "Exec sp_Generate_Query_Filter_WithSecurity '" & Session("Company") & "','" & Session("EmpID") & "','" & Session("Module") & "','Employee_File_Attachment_Vw@@Employee_File_Attachment_Vw','code@@Name','" & Trim(txtSEARCH_EMPLOYEE_CODE.Text) & "@@" & Trim(txtSEARCH_EMPLOYEE_NAME.Text) & "','" & myGridView1.PageSize & "','" & Session("currentpage1") & "'"
            ElseIf Trim(txtSEARCH_EMPLOYEE_CODE.Text) <> "Employee Code" Then
                ssql = "Exec sp_Generate_Query_Filter_WithSecurity '" & Session("Company") & "','" & Session("EmpID") & "','" & Session("Module") & "','Employee_File_Attachment_Vw','code','" & Trim(txtSEARCH_EMPLOYEE_CODE.Text) & "','" & myGridView1.PageSize & "','" & Session("currentpage1") & "'"
            ElseIf Trim(txtSEARCH_EMPLOYEE_NAME.Text) <> "Employee Name" Then
                ssql = "Exec sp_Generate_Query_Filter_WithSecurity '" & Session("Company") & "','" & Session("EmpID") & "','" & Session("Module") & "','Employee_File_Attachment_Vw','name','" & Trim(txtSEARCH_EMPLOYEE_NAME.Text) & "','" & myGridView1.PageSize & "','" & Session("currentpage1") & "'"
            End If

            pnlEmployee.Visible = False
            tblUpload.Visible = False

            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables.Count > 1 And CInt(myDS.Tables(0).Rows(0).Item(0)) > 0 Then
                myGridView1.DataSource = myDS.Tables(1)
                myGridView1.DataBind()
                pnlgridview1.Visible = True
                pnlprevnext1.Visible = True
                lblMessage.Text = ""
                imgBtnSelect.Enabled = True
                imgBtnDelete.Enabled = True
                imgBtnGoToPage1.Enabled = True

                lbltotal1.Text = " ( " & myDS.Tables(0).Rows(0).Item(0) & " record(s) ) "
                CurrentPage1.Text = Session("currentPage1")

                _totalRecords = myDS.Tables(0).Rows(0).Item(0)
                _totalPage = _totalRecords / myGridView1.PageSize
                TotalPages1.Text = (System.Math.Ceiling(_totalPage)).ToString()
                _totalPage = Double.Parse(TotalPages1.Text)
                Session("TotalPages1") = TotalPages1.Text

                If Session("currentpage1") = 1 Then
                    FirstPage1.Enabled = False
                    PrevPage1.Enabled = False
                    If _totalRecords > myGridView1.PageSize Then
                        NextPage1.Enabled = True
                        LastPage1.Enabled = True
                    Else
                        NextPage1.Enabled = False
                        LastPage1.Enabled = False
                    End If
                ElseIf Session("currentpage1") > 1 Then
                    FirstPage1.Enabled = True
                    PrevPage1.Enabled = True
                    If Session("currentpage1") = Session("TotalPages1") Then
                        NextPage1.Enabled = False
                        LastPage1.Enabled = False
                    Else
                        NextPage1.Enabled = True
                        LastPage1.Enabled = True
                    End If
                End If

                If CInt(myDS.Tables(0).Rows(0).Item(0).ToString) = 1 Then
                    Dim grdItem As GridViewRow
                    For Each grdItem In myGridView1.Rows
                        'txtSEARCH_EMPLOYEE_CODE.Text = " Employee Code "
                        'txtSEARCH_EMPLOYEE_NAME.Text = " Employee Name "
                        'txtEMPLOYEE_PROFILE_ID.Text = CType(grdItem.FindControl("grvlblcode"), Label).Text & " [" & CType(grdItem.FindControl("grvlblname"), Label).Text & "]"
                        txtEFFECTIVE_DATE.Text = Now.Date.ToString("dd/MM/yyyy")
                        LoadFiles()
                        pnlgridview1.Visible = False
                        pnlprevnext1.Visible = False
                        pnlEmployee.Visible = True
                        tblUpload.Visible = True
                        Exit For
                    Next
                End If
            Else
                pnlgridview1.Visible = False
                pnlprevnext1.Visible = False
                imgBtnSelect.Enabled = False
                imgBtnDelete.Enabled = False
                imgBtnGoToPage1.Enabled = False
                lblMessage.Text = "No data found..."
            End If
            myDS = Nothing
        Catch ex As Exception
            myDS = Nothing
            lblMessage.Text = "[BindGrid]Error: " & ex.Message.ToString
        End Try
    End Sub
    Private Sub LoadFiles()
        ssql = "Select * From Employee_File_Attachment_Vw Where Company_Profile_Code='" & Session("Company").ToString & "' And Employee_Profile_ID='" & _
                mySetting.GetEmployeeIDbyCodeName_ReturnString(Session("Company").ToString, txtEMPLOYEE_PROFILE_ID.Text.ToString.Replace("&amp;", "&")) & "'"
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If Not myDS Is Nothing Then
            If myDS.Tables(0).Rows.Count > 0 Then
                imgPreview.Visible = True
                imgPreview.ImageUrl = "../../" & myDS.Tables(0).Rows(0).Item(1).ToString
            Else
                imgPreview.Visible = False
            End If
        Else
            imgPreview.Visible = False
        End If
    End Sub
    Public Sub NavigationLink1_Click(ByVal sender As Object, ByVal e As CommandEventArgs)

        Select Case e.CommandName
            Case "First"
                Session("currentpage1") = 1
            Case "Prev"
                Session("currentpage1") = Integer.Parse(CurrentPage1.Text) - 1
            Case "Next"
                Session("currentpage1") = Integer.Parse(CurrentPage1.Text) + 1
            Case "Last"
                Session("currentpage1") = Integer.Parse(TotalPages1.Text)
        End Select

        BindGrid()
    End Sub

    Private Function ValidatePreviewFile() As Boolean
        Try
            lblMessage.Text = ""
            If filUpload.HasFile = False Then
                lblMessage.Text = "Please select an file."
                filUpload.Focus()
                Return False
            End If
            If txtEMPLOYEE_PROFILE_ID.Text.ToString.Trim = "" Then
                lblMessage.Text = "Please select an employee to proceed"
                imgbtnEMPLOYEE_PROFILE_ID.Focus()
                Return False
            End If
            If txtEFFECTIVE_DATE.Text.ToString.Trim = "" Then
                lblMessage.Text = "Please select an effective date to proceed"
                imgbtnEFFECTIVE_DATE.Focus()
                Return False
            End If
            If txtEFFECTIVE_DATE.Text.ToString < Now.Date.ToString("dd/MM/yyyy") Then
                lblMessage.Text = "Effective Date must be greater or equal to current date!"
                imgbtnEFFECTIVE_DATE.Focus()
                Return False
            End If
            Return (True)
        Catch ex As Exception
            lblMessage.Text = ex.Message.ToString
            Return False
        Finally
            If lblMessage.Text.ToString.Trim <> "" Then
                lblMessage.Visible = True
            End If
        End Try
    End Function

    Private Function ValidateUploadFile() As Boolean
        Try
            lblMessage.Text = ""
            'If txtEMPLOYEE_PROFILE_ID.Text.ToString.Trim = "" Then
            '    lblMessage.Text = "Please select an employee to proceed"
            '    imgbtnEMPLOYEE_PROFILE_ID.Focus()
            '    Return False
            'End If
            If txtEFFECTIVE_DATE.Text.ToString.Trim = "" Then
                lblMessage.Text = "Please select an effective date to proceed"
                imgbtnEFFECTIVE_DATE.Focus()
                Return False
            End If
            If txtEFFECTIVE_DATE.Text.ToString < Now.Date.ToString("dd/MM/yyyy") Then
                lblMessage.Text = "Effective Date must be greater or equal to current date!"
                imgbtnEFFECTIVE_DATE.Focus()
                Return False
            End If
            Return (True)
        Catch ex As Exception
            lblMessage.Text = ex.Message.ToString
            Return False
        Finally
            If lblMessage.Text.ToString.Trim <> "" Then
                lblMessage.Visible = True
            End If
        End Try
    End Function

    Protected Sub btnPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If ValidatePreviewFile() = True Then
            Try
                Dim postedFile As HttpPostedFile = filUpload.PostedFile
                'Dim strFileName As String = postedFile.FileName
                Dim strFileName As String, strExtension As String

                strFileName = Mid(postedFile.FileName.ToString, postedFile.FileName.ToString.LastIndexOf("\") + 2, (postedFile.FileName.ToString.LastIndexOf(".") - postedFile.FileName.ToString.LastIndexOf("\") - 1))
                strExtension = Mid(postedFile.FileName.ToString, postedFile.FileName.ToString.IndexOf(".") + 1, Len((postedFile.FileName.ToString.Length) - postedFile.FileName.ToString.IndexOf(".")))
                'If mySetting.CheckIfUserIsEmployee(Session("Company").ToString, Session("EmpID").ToString) = True Then
                '    strFileName = Session("EmpID").ToString & Mid(postedFile.FileName.ToString, postedFile.FileName.ToString.IndexOf(".") + 1, Len((postedFile.FileName.ToString.Length) - postedFile.FileName.ToString.IndexOf(".")))
                'Else
                '    strFileName = Session("EmpID").ToString & Mid(postedFile.FileName.ToString, postedFile.FileName.ToString.IndexOf(".") + 1, Len((postedFile.FileName.ToString.Length) - postedFile.FileName.ToString.IndexOf(".")))
                'End If

                Dim contentType As String = postedFile.ContentType
                Dim contentLength As Integer = postedFile.ContentLength
                postedFile.SaveAs(System.AppDomain.CurrentDomain.BaseDirectory & "Temp\Attachments\" & strFileName & strExtension)

                If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "Temp\Attachments\" & strFileName & strExtension) = True Then
                    Dim myDR As DataRow
                    If Not Session("MyDT") Is Nothing Then
                        myDR = CType(Session("MyDT"), DataTable).NewRow
                        myDR(0) = "../../Temp/Attachments/" & strFileName & strExtension
                        myDR(1) = txtEMPLOYEE_PROFILE_ID.Text.ToString
                        myDR(2) = txtEFFECTIVE_DATE.Text.ToString

                        Dim objFindValues(2) As Object
                        objFindValues(0) = myDR(0).ToString
                        objFindValues(1) = myDR(1).ToString
                        objFindValues(2) = myDR(2).ToString

                        Dim tmpDR As DataRow = CType(Session("MyDT"), DataTable).Rows.Find(objFindValues)
                        If Not tmpDR Is Nothing Then
                            lblMessage.Visible = True
                            lblMessage.Text = "Duplicate File found!"
                            Exit Sub
                        End If
                        CType(Session("MyDT"), DataTable).Rows.Add(myDR)

                        Dim dt As New DataTable
                        dt.Columns.Add("File Name")
                        dt.Columns.Add("Employee Profile ID")
                        dt.Columns.Add("Effective Date")
                        Dim i As Integer = CType(Session("MyDT"), DataTable).Rows.Count - 1
                        Do While i >= 0
                            Dim dr As DataRow = dt.NewRow
                            dr(0) = CType(Session("MyDT"), DataTable).Rows(i).Item(0).ToString
                            dr(1) = CType(Session("MyDT"), DataTable).Rows(i).Item(1).ToString
                            dr(2) = CType(Session("MyDT"), DataTable).Rows(i).Item(2).ToString
                            dt.Rows.Add(dr)
                            dr = Nothing
                            i -= 1
                        Loop
                        Session("FinalDT") = dt
                        gvFiles.DataSource = CType(Session("FinalDT"), DataTable).DefaultView
                        gvFiles.DataBind()
                        gvFiles.Visible = True
                    End If
                End If
                System.Threading.Thread.Sleep(1000)
            Catch ex As Exception
                lblMessage.Visible = True
                lblMessage.Text = "[Error While Previewing File]: " & ex.Message.ToString
            End Try
        End If
    End Sub

    Protected Sub gvFiles_UploadFile(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFiles.RowCommand
        If e.CommandName.ToString.ToUpper = "UPLOAD" Then
            If ValidateUploadFile() = True Then
                Try
                    Dim intRow As Integer = CInt(e.CommandArgument.ToString)
                    Dim str As String = gvFiles.Rows(intRow).Cells(0).Text
                    Dim strURL As String = str.ToString
                    Dim strFileName As String, strExtension As String

                    strFileName = Mid(strURL, strURL.LastIndexOf("/") + 2, (strURL.LastIndexOf(".") - strURL.LastIndexOf("/") - 1))
                    strExtension = Mid(strURL.ToString, strURL.LastIndexOf(".") + 1, Len((strURL.Length) - strURL.LastIndexOf(".")))

                    Dim strUserID As String = mySetting.GetEmployeeCodeByCodeName_ReturnString(Session("Company").ToString, gvFiles.Rows(intRow).Cells(1).Text.ToString.Replace("&amp;", "&"))

                    If strUserID.Trim = "" Then
                        lblMessage.Visible = True
                        lblMessage.Text = "[Error In Retrieve Employee Profile ID] for [" & "txtEMPLOYEE_PROFILE_ID.Text.ToString" & "]"
                        Exit Sub
                    End If
                    If Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "Images\Company\" & Session("Company").ToString & "\EmpAttachment\" & strUserID) Then
                        'Do nothing
                    Else
                        Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "Images\Company\" & Session("Company").ToString & "\EmpAttachment\" & strUserID)
                    End If
                    If Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "Images\Company\" & Session("Company").ToString & "\EmpAttachment\" & strUserID & "\Temp") Then
                        'Do nothing
                    Else
                        Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "Images\Company\" & Session("Company").ToString & "\EmpAttachment\" & strUserID & "\Temp")
                    End If
                    If Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "Images\Company\" & Session("Company").ToString & "\EmpAttachment\" & strUserID & "\Default") Then
                        'Do nothing
                    Else
                        Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "Images\Company\" & Session("Company").ToString & "\EmpAttachment\" & strUserID & "\Default")
                    End If

                    'Dim s As String
                    'For Each s In Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory & "Images\Company\" & Session("Company").ToString & "\EmpAttachment\" & strUserID & "\Default\")
                    '    File.Delete(s)
                    'Next s

                    File.Copy(System.AppDomain.CurrentDomain.BaseDirectory & "Temp\Attachments\" & strFileName & strExtension, System.AppDomain.CurrentDomain.BaseDirectory & "Images\Company\" & Session("Company").ToString & "\EmpAttachment\" & strUserID & "\Default\" & strFileName & strExtension, True)
                    File.Copy(System.AppDomain.CurrentDomain.BaseDirectory & "Temp\Attachments\" & strFileName & strExtension, System.AppDomain.CurrentDomain.BaseDirectory & "Images\Company\" & Session("Company").ToString & "\EmpAttachment\" & strUserID & "\Temp\" & strFileName & strExtension, True)


                    Dim strEmpIDForUploadFile As String = mySetting.GetEmployeeIDbyCodeName_ReturnString(Session("Company").ToString, gvFiles.Rows(intRow).Cells(1).Text.ToString.Replace("&amp;", "&"))
                    ssql = "Exec sp_is_UploadAttachment '" & Session("Company").ToString & "','" & strEmpIDForUploadFile & "','" & Session("Module").ToString & "','" & _
                            mySetting.ConvertDateToDecimal(txtEFFECTIVE_DATE.Text.ToString.Trim, Session("Company").ToString, Session("Module").ToString) & "','" & strFileName & strExtension & "','" & _
                            "Images/Company/" & Session("Company").ToString & "/EmpAttachment/" & strUserID & "/Default/" & strFileName & strExtension & "','','" & Session("EmpID").ToString & "'"
                    mySQL.ExecuteSQLNonQuery(ssql, Session("Company").ToString, Session("EmpID").ToString)

                    Dim intFound As Integer = 0
                    For i As Integer = 0 To gvFiles.Rows.Count - 1
                        If gvFiles.Rows(i).Cells(1).Text.ToString = strURL.ToString Then
                            intFound += 1
                        End If
                    Next

                    If intFound = 1 Then
                        If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "Temp\Attachments\" & strFileName & strExtension) Then
                            File.Delete(System.AppDomain.CurrentDomain.BaseDirectory & "Temp\Attachments\" & strFileName & strExtension)
                        End If
                    End If

                    Dim objFindValues(2) As Object
                    objFindValues(0) = strURL
                    objFindValues(1) = gvFiles.Rows(intRow).Cells(1).Text.ToString.Replace("&amp;", "&")
                    objFindValues(2) = gvFiles.Rows(intRow).Cells(2).Text.ToString.Replace("&amp;", "&")

                    Dim tmpDR As DataRow = CType(Session("MyDT"), DataTable).Rows.Find(objFindValues)
                    If Not tmpDR Is Nothing Then
                        CType(Session("MyDT"), DataTable).Rows.Remove(tmpDR)
                        CType(Session("MyDT"), DataTable).AcceptChanges()
                    End If

                    CType(Session("FinalDT"), DataTable).Rows(intRow).Delete()
                    CType(Session("FinalDT"), DataTable).AcceptChanges()
                    gvFiles.DataSource = CType(Session("FinalDT"), DataTable).DefaultView
                    gvFiles.DataBind()

                    If gvFiles.Rows.Count = 0 Then
                        gvFiles.Visible = False
                    End If

                    lblMessage.Visible = True
                    lblMessage.Text = "File uploaded successfully!"
                    LoadFiles()
                Catch ex As Exception
                    lblMessage.Visible = True
                    lblMessage.Text = "[Error While Uploading File]: " & ex.Message.ToString
                End Try
            End If
        End If
    End Sub

    Protected Sub myGridView_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles myGridView.RowDeleting
        Try
            Dim img As Image = myGridView.Rows(e.RowIndex).Cells(1).Controls(0)
            Dim strURL As String = img.ImageUrl.ToString
            Dim strFileName As String, strExtension As String

            strFileName = Mid(strURL, strURL.LastIndexOf("/") + 2, (strURL.LastIndexOf(".") - strURL.LastIndexOf("/") - 1))
            strExtension = Mid(strURL.ToString, strURL.LastIndexOf(".") + 1, Len((strURL.Length) - strURL.LastIndexOf(".")))
            Dim intFound As Integer = 0
            For i As Integer = 0 To myGridView.Rows.Count - 1
                If CType(myGridView.Rows(i).Cells(1).Controls(0), Image).ImageUrl.ToString = strURL Then
                    intFound += 1
                End If
            Next

            If intFound = 1 Then
                If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "Temp\Attachments\" & strFileName & strExtension) Then
                    File.Delete(System.AppDomain.CurrentDomain.BaseDirectory & "Temp\Attachments\" & strFileName & strExtension)
                End If
            End If

            Dim objFindValues(2) As Object
            objFindValues(0) = strURL
            objFindValues(1) = myGridView.Rows(e.RowIndex).Cells(2).Text.ToString.Replace("&amp;", "&")
            objFindValues(2) = myGridView.Rows(e.RowIndex).Cells(3).Text.ToString.Replace("&amp;", "&")

            Dim tmpDR As DataRow = CType(Session("MyDT"), DataTable).Rows.Find(objFindValues)
            If Not tmpDR Is Nothing Then
                CType(Session("MyDT"), DataTable).Rows.Remove(tmpDR)
                CType(Session("MyDT"), DataTable).AcceptChanges()
            End If

            CType(Session("FinalDT"), DataTable).Rows(e.RowIndex).Delete()
            CType(Session("FinalDT"), DataTable).AcceptChanges()
            myGridView.DataSource = CType(Session("FinalDT"), DataTable).DefaultView
            myGridView.DataBind()

            If myGridView.Rows.Count = 0 Then
                myGridView.Visible = False
            End If
            lblMessage.Visible = True
            lblMessage.Text = "Image deleted succesfully!"
        Catch ex As Exception
            lblMessage.Visible = True
            lblMessage.Text = "[Error Deleting] : " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub imgbtnSEARCH_EMPLOYEE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSEARCH_EMPLOYEE.Click
        If Trim(txtSEARCH_EMPLOYEE_CODE.Text) = "Employee Code" And Trim(txtSEARCH_EMPLOYEE_NAME.Text) = "Employee Name" Then
            lblMessage.Visible = True
            lblMessage.Text = "lblEMPLOYEE_PROFILE_ID.Text" & " is a require filed..."
            Exit Sub
            'txtEmployee_Name.Text = "%"
        End If
        lblResult.Text = ""
        Session("currentpage1") = 1
        BindGrid()
    End Sub

    Protected Sub imgBtnGoToPage1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnGoToPage1.Click
        lblMessage.Text = ""
        If txtGoToPage1.Text = "" Then
            lblMessage.Visible = True
            lblMessage.Text = "Request failed! Value required..."
            txtGoToPage1.Focus()
            Exit Sub
        End If

        Try
            Dim pagestr As String = Trim(txtGoToPage1.Text)
            Dim pagenum As Integer = CInt(pagestr)
            Dim firstnum As Integer = 0
            Dim lastnum As Integer = 0

            If pagenum <= CInt(Session("TotalPages1")) And pagenum > 0 Then
                If pagenum = CInt(Session("currentPage1")) Then
                    lblResult.Text = "Request failed! You are looking for the same page..."
                    txtGoToPage1.Focus()
                Else
                    lblResult.Text = ""
                    Session("currentpage1") = pagenum
                    BindGrid()
                End If
                txtGoToPage1.Text = ""
            Else
                If pagenum = 1 Or pagenum > CInt(Session("TotalPages1")) And CInt(Session("TotalPages1")) = 1 Then
                    lblResult.Text = "Request failed! Only one page available..."
                Else
                    If CInt(Session("currentPage1")) = 1 Then
                        firstnum = 1
                    Else
                        firstnum = 0
                    End If
                    If CInt(Session("currentPage1")) = CInt(Session("TotalPages1")) Then
                        lastnum = CInt(Session("TotalPages1"))
                    Else
                        lastnum = CInt(Session("TotalPages1")) + 1
                    End If
                    lblResult.Text = "Request failed! Page number must between " & firstnum & " and " & lastnum & "..."
                End If
                txtGoToPage1.Text = ""
                txtGoToPage1.Focus()
            End If

        Catch ex As Exception
            lblMessage.Text = "Request failed! Invalid value enter..."
            txtGoToPage1.Text = ""
            txtGoToPage1.Focus()
        Finally
            If lblMessage.Text.ToString.Trim <> "" Then
                lblMessage.Visible = True
            End If
        End Try

    End Sub

    Protected Sub imgBtnSelect_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSelect.Click
        Try
            Dim getnum As Integer = CheckForSelectGv1()
            If getnum = 0 Then
                lblResult.Text = "Invalid action: No row selected..."
                Exit Sub
            ElseIf getnum = 1 Then
                Try
                    'Dim chkSelected As CheckBox
                    'For Each grdItem As GridViewRow In myGridView1.Rows
                    '    chkSelected = grdItem.FindControl("chkSelect1")
                    '    If chkSelected.Checked Then
                    '        txtSEARCH_EMPLOYEE_CODE.Text = " Employee Code "
                    '        txtSEARCH_EMPLOYEE_NAME.Text = " Employee Name "
                    '        'txtEMPLOYEE_PROFILE_ID.Text = CType(grdItem.FindControl("grvlblcode"), Label).Text & " [" & CType(grdItem.FindControl("grvlblname"), Label).Text & "]"
                    '        txtEFFECTIVE_DATE.Text = Now.Date.ToString("dd/MM/yyyy")
                    Dim chkSelected As CheckBox, strImageName As String, strImagePath As String
                    For Each grdItem As GridViewRow In myGridView1.Rows
                        chkSelected = grdItem.FindControl("chkSelect1")
                        If chkSelected.Checked Then
                            txtSEARCH_EMPLOYEE_CODE.Text = CType(grdItem.FindControl("grvlblCode"), Label).Text
                            txtSEARCH_EMPLOYEE_NAME.Text = CType(grdItem.FindControl("grvlblName"), Label).Text
                            txtEMPLOYEE_PROFILE_ID.Text = CType(grdItem.FindControl("grvlblEmployee_Profile_ID"), Label).Text
                            'txtEMPLOYEE_PROFILE_ID.Text = CType(grdItem.FindControl("grvlblcode"), Label).Text & " [" & CType(grdItem.FindControl("grvlblname"), Label).Text & "]"
                            txtEFFECTIVE_DATE.Text = Now.Date.ToString("dd/MM/yyyy")
                            strImageName = CType(grdItem.FindControl("grvlblFileName"), Label).Text
                            strImagePath = CType(grdItem.FindControl("grvlblServerPath"), Label).Text
                            If strImagePath <> "" Then
                                strImagePath = Mid(strImagePath, 7, Len(strImagePath))
                            End If

                            If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory & strImagePath.Replace("/", "\")) Then
                                imgPreview.Visible = True
                                imgPreview.ImageUrl = "../../" & strImagePath
                            Else
                                imgPreview.Visible = False
                            End If


                            LoadFiles()
                            chkSelected.Checked = False
                            pnlgridview1.Visible = False
                            pnlprevnext1.Visible = False
                            pnlEmployee.Visible = True
                            tblUpload.Visible = True
                            Exit For
                        End If
                    Next
                Catch ex As Exception
                    lblResult.Text = "Error: " & ex.Message
                End Try
            ElseIf getnum > 1 Then
                lblResult.Text = "Invalid action: Only one row can be selected..."
            End If
        Catch ex As Exception
            lblMessage.Text = "[imgBtnSelect_Click]Error: " & ex.Message
        End Try
    End Sub

    Private Function CheckForSelectGv1() As Integer

        Dim v_num As Integer = 0

        Try
            lblMessage.Text = ""
            Dim chkSelected As CheckBox
            For Each grdItem As GridViewRow In myGridView1.Rows
                chkSelected = grdItem.FindControl("chkSelect1")
                If chkSelected.Checked = True Then
                    v_num += 1
                End If
            Next
            Return v_num
        Catch ex As Exception
            lblMessage.Text = "[CheckForSelectGv1]Error: " & ex.Message
        Finally
            If lblMessage.Text.ToString.Trim <> "" Then
                lblMessage.Visible = True
            End If
        End Try

    End Function
    Private Function CheckForDeleteGv() As Integer

        Dim v_num As Integer = 0

        Try
            lblMessage.Text = ""
            Dim chkSelected As CheckBox
            For Each grdItem As GridViewRow In myGridView1.Rows
                chkSelected = grdItem.FindControl("chkDelete")
                If chkSelected.Checked = True Then
                    v_num += 1
                End If
            Next
            Return v_num
        Catch ex As Exception
            lblMessage.Text = "[CheckForDeleteGv]Error: " & ex.Message
        Finally
            If lblMessage.Text.ToString.Trim <> "" Then
                lblMessage.Visible = True
            End If
        End Try

    End Function
    Private Sub DownloadFile(ByVal strFileName As String, ByVal strServerFilePath As String, ByVal strSystemFilePath As String)
        'Dim strRequest As String = Request.QueryString("file") '-- if something was passed to the file querystring  
        If strFileName <> "" Then 'get absolute path of the file  
            Dim path As String = Server.MapPath(strServerFilePath) 'get file object as FileInfo  
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path) '-- if the file exists on the server  
            If file.Exists Then 'set appropriate headers  
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(file.FullName)
                Response.End() 'if file does not exist  
            Else
                Response.Write("This file does not exist.")
            End If 'nothing in the URL as HTTP GET  
        Else
            Response.Write("Please provide a file to download.")
        End If
    End Sub

    'Protected Sub imgbtnSelectOne_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSelectOne.Click
    '    mySetting.AddDeleteListBoxItem(lstLeft, lstRight)
    'End Sub

    'Protected Sub imgbtnSelectAll_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSelectAll.Click
    '    mySetting.AddDeleteAllListBoxItem(lstLeft, lstRight)
    'End Sub

    'Protected Sub imgbtnRemoveOne_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnRemoveOne.Click
    '    mySetting.AddDeleteListBoxItem(lstRight, lstLeft)
    'End Sub

    'Protected Sub imgbtnRemoveAll_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnRemoveAll.Click
    '    mySetting.AddDeleteAllListBoxItem(lstRight, lstLeft)
    'End Sub

    Protected Sub rdobtnByEmployee_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdobtnByEmployee.CheckedChanged
        If rdobtnByEmployee.Checked = True Then
            rdobtnByDepartment.Checked = False
            imgKeyDEPARTMENT.Enabled = False
            lblDEPARTMENT.Enabled = False
            ddlDEPARTMENT_CODE.Enabled = False
            txtDEPARTMENT_NAME.Enabled = False
            imgbtnSEARCH_DEPARTMENT.Enabled = False

            lblSEARCH_EMPLOYEE.Enabled = True
            txtSEARCH_EMPLOYEE_CODE.Enabled = True
            txtSEARCH_EMPLOYEE_NAME.Enabled = True
            imgbtnSEARCH_EMPLOYEE.Enabled = True
        End If
    End Sub

    Protected Sub rdobtnByDepartment_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdobtnByDepartment.CheckedChanged
        If rdobtnByDepartment.Checked = True Then
            rdobtnByEmployee.Checked = False
            imgKeySEARCH_EMPLOYEE.Enabled = False
            lblSEARCH_EMPLOYEE.Enabled = False
            txtSEARCH_EMPLOYEE_CODE.Enabled = False
            txtSEARCH_EMPLOYEE_NAME.Enabled = False
            imgbtnSEARCH_EMPLOYEE.Enabled = False

            lblDEPARTMENT.Enabled = True
            ddlDEPARTMENT_CODE.Enabled = True
            txtDEPARTMENT_NAME.Enabled = True
            imgbtnSEARCH_DEPARTMENT.Enabled = True
        End If
    End Sub

    Protected Sub imgBtnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnDelete.Click
        Dim i As Integer, chkdelete As CheckBox


        Dim getnum As Integer = CheckForDeleteGv()
        If getnum = 0 Then
            lblResult.Text = "Invalid action: No row selected..."
            Exit Sub
        ElseIf getnum > 0 Then
            For Each grdItem As GridViewRow In myGridView1.Rows
                chkdelete = grdItem.FindControl("chkDelete")
                If chkdelete.Checked Then
                    ssql = "Exec sp_is_delAttachment """ & Session("Company").ToString & """,""" & CType(grdItem.FindControl("grvlblEmployee_Profile_ID"), Label).Text & """,""" _
                    & CType(grdItem.FindControl("grvlblEffectiveDate"), Label).Text & """,""" & CType(grdItem.FindControl("grvlblFileName"), Label).Text & """"
                    mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

                    lblResult.Text = "Delete Successful!"
                End If
            Next
        end if
        BindGrid()
    End Sub
End Class
