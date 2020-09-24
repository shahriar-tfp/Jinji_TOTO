Imports System.Data
Imports System.IO

Partial Class Pages_Roster_Download_Attendance_Batch
    Inherits System.Web.UI.Page
    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting, myDS As New DataSet
    Private WithEvents myHTB As New Hashtable, myAutoGenerate As New clsAutoGenerate
    Dim btnColourDef, btnColourAlt As String
    Dim strPath As String = "../../Images"
    Dim logic As Boolean
    Dim ssql As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PagePreload()
        Else
            lblMessage.Text = ""
        End If
    End Sub
    Private Sub PagePreload()
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")
        Session("HTB") = myHTB
        lblMessage.CssClass = "wordstyle2"
        lblDateStart.Text = "Date Start (dd/mm/yyyy)"
        lblDateEnd.Text = "Date End (dd/mm/yyyy)"
        lblDateStart.CssClass = "wordstyle10"
        lblDateEnd.CssClass = "wordstyle10"
        chkOverwrite.Text = "Overwrite Old Data"
        lblBrowse.CssClass = "wordstyle4"
        lblBrowse.Text = "File Location :"
        ssql = "exec sp_sa_GetPageTitle '" & Form.ID & "'"
        myDS = New DataSet

        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            lblTitle.Text = myDS.Tables(0).Rows(0).Item(0)
            lblTitle.CssClass = "wordstyle4"
        End If
        myDS = Nothing
        mySetting.GetBtnImgUrl(imgbtnAdd, Session("Company").ToString, btnColourDef, "btnAdd.png")
        mySetting.GetBtnImgUrl(imgbtnDelete, Session("Company").ToString, btnColourDef, "btnDelete.png")
        mySetting.GetBtnImgUrl(imgbtnProcess, Session("Company").ToString, btnColourDef, "btnProcess.png")
        mySetting.GetImgUrl(imgkeyDateStart, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgkeyDateEnd, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnDateStart, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnDateEnd, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)

        mySetting.PopUpCalendar_ImageButton(imgbtnDateStart, Form.ID, "txtDateStart")
        mySetting.PopUpCalendar_ImageButton(imgbtnDateEnd, Form.ID, "txtDateEnd")
        body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

    End Sub
    Private Function ValidateFileFormat() As Boolean
        If uplFile.HasFile = False Then
            lblMessage.Text = "Please browse for a text file!"
            Return False
        End If
        Dim ext As String = uplFile.FileName.Substring(uplFile.FileName.LastIndexOf("."))
        If ext.ToLower <> ".txt" Then
            lblMessage.Text = "Invalid file format. Please browse for a text file only!"
            Return False
        End If
        For i As Integer = 0 To lstTextFile.Items.Count - 1
            If uplFile.FileName.ToString = lstTextFile.Items(i).Value Then
                lblMessage.Text = "Duplicate file name founded! Please rename your file and upload again!"
                Return False
            End If
        Next
        Return True
    End Function

    Protected Sub imgbtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnAdd.Click
        If ValidateFileFormat() = True Then
            Dim postedFile As HttpPostedFile = uplFile.PostedFile
            Dim orginFileName As String = uplFile.FileName.ToString
            Dim uploadFilePath As String = ReturnNewFileName()
            postedFile.SaveAs(uploadFilePath)

            ssql = "Insert Into Download_Attendance_Data_Temp_Process(Company_Profile_Code,File_Name,File_Path,Date_Start,Date_End,Option_Overwrite,Option_Status,Date_Time_Created,User_Profile_Code_Created) Values (" & _
                    "N'" & Session("Company").ToString & "',N'" & orginFileName & "',N'" & uploadFilePath & "',N'" & txtDateStart.Text.ToString.Trim & "',N'" & txtDateEnd.Text.ToString.Trim & "',N'" & _
                    "0',N'PENDING',dbo.fn_GetCurrentDateTime(GetDate()),N'" & Session("EmpID").ToString.Trim & "'"

            'Dim myArray As New ArrayList
            'myAutoGenerate.ReadFileContent(myArray, uploadFilePath)

            'CType(Session("HTB"), Hashtable).Add(orginFileName, ssql)

            lstTextFile.Items.Add(New ListItem(uplFile.FileName.ToString, uploadFilePath))
        End If
    End Sub
    Private Sub ReadFileContent(ByVal strPath As String)
        If File.Exists(strPath) Then
            Dim objReader As New StreamReader(strPath)
            Dim sLine As String = "", htb As New Hashtable, i As Integer = 0
            Try
                Do
                    sLine = objReader.ReadLine()
                    i += 1
                    Dim ClockData As String = ""
                    If Not sLine Is Nothing Then
                        ClockData = sLine
                        ssql = "Exec sp_tms_insDelTempClock N'" & Session("Company").ToString & "',N'" & ClockData & "',N'ADD'"
                        htb.Add(i, ssql)
                    End If

                Loop Until sLine Is Nothing
                If Not htb Is Nothing Then
                    mySQL.ExecuteSQLTransactionByHashtable(htb, "Insert Attendance", Session("Company").ToString, Session("Module").ToString)
                End If
            Catch ex As Exception
                'Error handling
                lblMessage.Text = ex.Message.ToString
            Finally
                objReader.Close()
            End Try
        Else
            Exit Sub
        End If
    End Sub
    Private Function ReturnNewFileName() As String
        Dim strFileName As String = Session("Company").ToString & DatePart(DateInterval.Year, Now()).ToString

        If Len(DatePart(DateInterval.Month, Now()).ToString) < 2 Then
            strFileName &= "0" & DatePart(DateInterval.Month, Now()).ToString
        Else
            strFileName &= DatePart(DateInterval.Month, Now()).ToString
        End If
        If Len(DatePart(DateInterval.Day, Now()).ToString) < 2 Then
            strFileName &= "0" & DatePart(DateInterval.Day, Now()).ToString
        Else
            strFileName &= DatePart(DateInterval.Day, Now()).ToString
        End If
        If Len(DatePart(DateInterval.Hour, Now()).ToString) < 2 Then
            strFileName &= "0" & DatePart(DateInterval.Hour, Now()).ToString
        Else
            strFileName &= DatePart(DateInterval.Hour, Now()).ToString
        End If
        If Len(DatePart(DateInterval.Minute, Now()).ToString) < 2 Then
            strFileName &= "0" & DatePart(DateInterval.Minute, Now()).ToString
        Else
            strFileName &= DatePart(DateInterval.Minute, Now()).ToString
        End If
        If Len(DatePart(DateInterval.Second, Now()).ToString) < 2 Then
            strFileName &= "0" & DatePart(DateInterval.Second, Now()).ToString
        Else
            strFileName &= DatePart(DateInterval.Second, Now()).ToString
        End If
        Dim strOriginalFileName As String = uplFile.FileName.ToString

        If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "Download_Atten_Data\" & strFileName & strOriginalFileName) Then
            strFileName = (System.AppDomain.CurrentDomain.BaseDirectory & "Download_Atten_Data\" & strFileName & "_" & strOriginalFileName)
        Else
            strFileName = (System.AppDomain.CurrentDomain.BaseDirectory & "Download_Atten_Data\" & strFileName & "_" & strOriginalFileName)
        End If
        Return strFileName
    End Function

    Private Sub CreateCompanyFolder()
        If Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "Download_Atten_Data\" & Session("Company").ToString) Then
            'Do nothing
        Else
            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "Download_Atten_Data\" & Session("Company").ToString)
        End If
    End Sub

    Protected Sub imgbtnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnDelete.Click
        If lstTextFile.Items.Count = 0 Then
            lblMessage.Text = "There is no item to delete!"
            Exit Sub
        End If
        If ValidateDelete() = True Then
            For i As Integer = lstTextFile.Items.Count - 1 To 0 Step -1
                If lstTextFile.Items(i).Selected = True Then
                    lstTextFile.Items.Remove(lstTextFile.Items(i))
                End If
            Next
        Else
            lblMessage.Text = "Please select item(s) to delete!"
        End If
    End Sub
    Private Function ValidateDelete() As Boolean
        For i As Integer = 0 To lstTextFile.Items.Count - 1
            If lstTextFile.Items(i).Selected = True Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Function ValidateProcess() As Boolean
        If txtDateStart.Text.ToString.Trim = "" Then
            lblMessage.Text = "[Date Start] is a required field!"
            txtDateStart.Focus()
            Return False
        End If
        If txtDateEnd.Text.ToString.Trim = "" Then
            lblMessage.Text = "[Date End] is a required field!"
            txtDateEnd.Focus()
            Return False
        End If
        If Not IsNumeric(mySetting.ConvertDateToDecimal(txtDateStart.Text.ToString, Session("Company").ToString, "ROSTER")) Then
            lblMessage.Text = "Invalid format for [Date Start]. It must be in [dd/mm/yyyy] format!"
            txtDateStart.Focus()
            Return False
        End If
        If Not IsNumeric(mySetting.ConvertDateToDecimal(txtDateEnd.Text.ToString, Session("Company").ToString, "ROSTER")) Then
            lblMessage.Text = "Invalid format for [Date End]. It must be in [dd/mm/yyyy] format!"
            txtDateEnd.Focus()
            Return False
        End If
        Return True
    End Function

    Protected Sub imgbtnProcess_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnProcess.Click
        If ValidateProcess() = True Then
            'mySQL.ExecuteSQLTransactionByHashtable(CType(Session("HTB"), Hashtable), "InsertDownloadAttendance", Session("Company").ToString, Session("EmpID").ToString)
            ssql = "Exec sp_tms_insDelTempClock N'" & Session("Company").ToString & "',N'','DEL'"
            mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            For i As Integer = 0 To lstTextFile.Items.Count - 1
                ReadFileContent(lstTextFile.Items(i).Value.ToString)
            Next

            'Process Clock In Out
            ssql = "Exec sp_tms_chkclockprovider N'" & Session("Company").ToString & "'"
            mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

            'Process OT
            If chkOverwrite.Checked = True Then
                ssql = "Exec sp_tms_GetInOutAttendence N'" & Session("Company").ToString & _
                        "',N'" & mySetting.ConvertDateToDecimal(txtDateStart.Text.ToString, Session("Company").ToString, Session("Module").ToString) & "',N'" & _
                        mySetting.ConvertDateToDecimal(txtDateEnd.Text.ToString, Session("Company").ToString, Session("Module").ToString) & "','1'"
            Else
                ssql = "Exec sp_tms_GetInOutAttendence N'" & Session("Company").ToString & _
                       "',N'" & mySetting.ConvertDateToDecimal(txtDateStart.Text.ToString, Session("Company").ToString, Session("Module").ToString) & "',N'" & _
                       mySetting.ConvertDateToDecimal(txtDateEnd.Text.ToString, Session("Company").ToString, Session("Module").ToString) & "','0'"
            End If
            mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            ProcessCompleted()
        End If
    End Sub
    Private Sub ProcessCompleted()
        lstTextFile.Items.Clear()
        txtDateStart.Text = ""
        txtDateEnd.Text = ""
        chkOverwrite.Checked = False

        Dim strScript As String = "<script language=" & """" & "javascript" & """" & ">"
        strScript &= "alert('Process Completed!');"
        strScript &= "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "Process Completed!", strScript)
    End Sub
End Class
