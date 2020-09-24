Imports System.Data
Imports System.IO

Partial Class Pages_Roster_Process_Attendance_Error
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
        ssql = "exec sp_sa_GetPageTitle '" & Form.ID & "'"
        myDS = New DataSet

        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            lblTitle.Text = myDS.Tables(0).Rows(0).Item(0)
            lblTitle.CssClass = "wordstyle4"
        End If
        myDS = Nothing
        mySetting.GetBtnImgUrl(imgbtnProcess, Session("Company").ToString, btnColourDef, "btnProcess.png")
        mySetting.GetImgUrl(imgkeyDateStart, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgkeyDateEnd, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnDateStart, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnDateEnd, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)

        mySetting.PopUpCalendar_ImageButton(imgbtnDateStart, Form.ID, "txtDateStart")
        mySetting.PopUpCalendar_ImageButton(imgbtnDateEnd, Form.ID, "txtDateEnd")
        body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

    End Sub
    
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
        Dim dates, dates1 As String
        If ValidateProcess() = True Then
            dates = mySetting.ConvertDateToDecimal(txtDateStart.Text.ToString, Session("Company").ToString, "ROSTER")
            dates1 = mySetting.ConvertDateToDecimal(txtDateEnd.Text.ToString, Session("Company").ToString, "ROSTER")

            Do While (mySQL.fn_DateDiff("Day", dates, dates1) >= 0)
                ssql = "Exec sp_tms_insdelTmsError """ & Session("Company").ToString & """,""" & dates & """"
                mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

                dates = CStr(mySQL.fn_DateAdd("Day", "1", dates))
                'dates = DateAdd("d", 1, dates)
            Loop
            ProcessCompleted()
        End If
    End Sub
    Private Sub ProcessCompleted()
        txtDateStart.Text = ""
        txtDateEnd.Text = ""

        Dim strScript As String = "<script language=" & """" & "javascript" & """" & ">"
        strScript &= "alert('Process Completed!');"
        strScript &= "<" & "/script>"
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "Process Completed!", strScript)
    End Sub
End Class
