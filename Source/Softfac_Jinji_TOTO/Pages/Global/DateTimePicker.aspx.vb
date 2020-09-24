Imports System.Data
Imports System.Data.SqlClient

Partial Class Pages_Global_DateTimePicker
    Inherits System.Web.UI.Page
    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting, myDS As New DataSet
    Dim myDR As DataRow, ssql As String, i As Integer
    Dim strjscript As String
    Dim strDateTimeFormat As String, strTimeFormat As String
    Dim strSeparator As String, strShowSecond As String, strAmPm As String
    Dim btnColourDef, btnColourAlt As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")
        If Session.IsNewSession Then
            SelfClose()
        End If
        If Not IsPostBack Then
            PagePreload()
            Session("FinalDate") = ""
            calCalendar.SelectedDate = Date.Now().Date()
            calCalendar_SelectionChanged(Nothing, Nothing)
        End If
        lblError.Visible = False
    End Sub
    Private Sub SelfClose()
        Dim strFormName As String = "", strControlName As String = ""
        Dim posSplit As Integer = Request.QueryString("formname").ToString.IndexOf(".")
        Dim strjscript As String = "<script language=""javascript"">"

        strFormName = Left(Request.QueryString("formname").ToString, posSplit)
        Dim strScript As String = ""

        strScript = "<script language=" & """javascript" & """>" & _
                    "self.close();window.opener.location='SessionTimeOut.aspx';" & _
                    ";</" & "script>"
        Page.ClientScript.RegisterStartupScript(GetType(Page), "ClosePage", strScript)
    End Sub
    Private Sub PagePreload()
        mySetting.GetBtnImgUrl(imgBtnSelect, Session("Company").ToString, btnColourDef, "btnSelect.png")

        'Bind Data ddlYear
        ssql = "Exec sp_tms_GenerateYear " & 80 & "," & 20
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                ddlYear.DataValueField = "year"
                ddlYear.DataTextField = "year"
                ddlYear.DataSource = myDS.Tables(0).DefaultView
                ddlYear.DataBind()
            End If
        End If
        mySetting.ArrangeDDLSelectedIndex(ddlYear, clsGlobalSetting.DDLSelection.SelectedValue, DatePart(DateInterval.Year, Now()))

        'Bind Data ddlMonth
        ssql = "Exec sp_tms_GenerateMonth 1,12"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                ddlMonth.DataValueField = "month"
                ddlMonth.DataTextField = "desc"
                ddlMonth.DataSource = myDS.Tables(0).DefaultView
                ddlMonth.DataBind()
            End If
        End If
        mySetting.ArrangeDDLSelectedIndex(ddlMonth, clsGlobalSetting.DDLSelection.SelectedValue, DatePart(DateInterval.Month, Now()))

        myDR = mySQL.ExecuteSQLByReturnRow("Select String_Value1,':',String_Value3,String_Value4,'AM' From Parameter Where Code='REGIONAL_SETTING' And Company_Profile_Code='" & Session("Company").ToString & "' And Module_Profile_Code='" & Session("Module").ToString & "'")
        If Not myDR Is Nothing Then
            Session("DateTimeFormat") = myDR(0).ToString
            Session("DateSeparator") = myDR(1).ToString
            Session("TimeFormat") = myDR(2).ToString
            Session("ShowSecond") = myDR(3).ToString
            strAmPm = myDR(4).ToString
            strDateTimeFormat = myDR(0).ToString
            strSeparator = myDR(1).ToString
            strTimeFormat = myDR(2).ToString
            strShowSecond = myDR(3).ToString
            strAmPm = myDR(4).ToString
        Else
            strDateTimeFormat = ""
            strSeparator = ""
            strTimeFormat = ""
            strShowSecond = ""
            strAmPm = ""
        End If
        myDR = Nothing

        Dim strDataText As String, strDataValue As String
        If strTimeFormat = "12" Then
            ddlHour.Items.Add(New ListItem("", ""))
            For i = 1 To 12
                If i < 10 Then
                    strDataText = "0" & i
                    strDataValue = "0" & i
                Else
                    strDataText = i
                    strDataValue = i
                End If
                ddlHour.Items.Add(New ListItem(strDataText, strDataValue))
            Next
            ddlAmPm.Items.Add(New ListItem("AM", "AM"))
            ddlAmPm.Items.Add(New ListItem("PM", "PM"))
            ddlAmPm.Visible = True
            lblAmPm.Visible = True
        Else
            ddlHour.Items.Add(New ListItem("", ""))
            For i = 0 To 23
                If i < 10 Then
                    strDataText = "0" & i
                    strDataValue = "0" & i
                Else
                    strDataText = i
                    strDataValue = i
                End If
                ddlHour.Items.Add(New ListItem(strDataText, strDataValue))
            Next
            ddlAmPm.Visible = False
            lblAmPm.Visible = False
            ddlHour.SelectedValue = DateTime.Now().Hour()
        End If
        ddlMinute.Items.Add(New ListItem("", ""))
        For i = 0 To 59
            If i < 10 Then
                strDataText = "0" & i
                strDataValue = "0" & i
            Else
                strDataText = i
                strDataValue = i
            End If
            ddlMinute.Items.Add(New ListItem(strDataText, strDataValue))
        Next
        ddlMinute.SelectedIndex = 1
        ddlSecond.Items.Add(New ListItem("", ""))
        If strShowSecond = "Y" Then
            For i = 0 To 59
                If i < 10 Then
                    strDataText = "0" & i
                    strDataValue = "0" & i
                Else
                    strDataText = i
                    strDataValue = i
                End If
                ddlSecond.Items.Add(New ListItem(strDataText, strDataValue))
            Next
            ddlSecond.SelectedIndex = 1
            'ddlSecond.Visible = True
            'lblSecond.Visible = True
            pnlsecond.Visible = True
        Else
            'ddlSecond.Visible = False
            'lblSecond.Visible = False
            pnlsecond.Visible = False
        End If
    End Sub

    Protected Sub calCalendar_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles calCalendar.DayRender
        If e.Day.Date = DateTime.Now().ToString("d") Then
            'e.Cell.BackColor = System.Drawing.Color.LightGray
        End If
    End Sub

    Protected Sub calCalendar_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calCalendar.SelectionChanged
        Dim myDate As DateTime
        Dim sDay As String = "", sMonth As String = "", sYear As String = ""
        Dim FinalDate As String = ""

        Try
            myDS = mySQL.ExecuteSQL("Select String_Value1,String_Value2,String_Value3 From Parameter Where Company_Profile_Code='" & Session("Company") & "' And Module_Profile_Code='" & Session("Module") & "' And Code='REGIONAL_SETTING'", Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables.Count > 0 And myDS.Tables(0).Rows.Count > 0 And myDS.Tables(0).Columns.Count > 2 Then
                Dim DefaultDateFormat As String = myDS.Tables(0).Rows(0).Item(0).ToString
                Dim DefaultDateSeparator As String = myDS.Tables(0).Rows(0).Item(1).ToString
                Dim DefaultDateCycle As String = myDS.Tables(0).Rows(0).Item(2).ToString

                myDate = calCalendar.SelectedDate
                sDay = DatePart(DateInterval.Day, myDate)

                If Len(sDay) = 1 And Len(sDay) < 2 Then
                    sDay = "0" & sDay
                End If
                sMonth = DatePart(DateInterval.Month, myDate)
                If Len(sMonth) = 1 And Len(sMonth) < 2 Then
                    sMonth = "0" & sMonth
                End If
                sYear = DatePart(DateInterval.Year, myDate)

                DefaultDateFormat = Replace(DefaultDateFormat, DefaultDateSeparator, "/")
                Select Case DefaultDateFormat
                    Case "DD/MM/YYYY"
                        FinalDate = sDay & DefaultDateSeparator & sMonth & DefaultDateSeparator & sYear
                    Case "MM/DD/YYYY"
                        FinalDate = sMonth & DefaultDateSeparator & sDay & DefaultDateSeparator & sYear
                    Case "YYYY/MM/DD"
                        FinalDate = sYear & DefaultDateSeparator & sMonth & DefaultDateSeparator & sDay
                    Case "YYYY/DD/MM"
                        FinalDate = sYear & DefaultDateSeparator & sDay & DefaultDateSeparator & sMonth
                    Case Else
                        FinalDate = sDay & DefaultDateSeparator & sMonth & DefaultDateSeparator & sYear
                        'FinalDate = sDay & "/" & sMonth & "/" & sYear
                End Select
                txtCalendar.Text = FinalDate
                Session("FinalDate") = FinalDate
                GetSelectedDateTime()
                myDS = Nothing
                Exit Sub

            Else
                'GoTo DefaultSetting
            End If
        Catch ex As Exception
            'GoTo DefaultSetting
        End Try

DefaultSetting:
        myDate = calCalendar.SelectedDate
        sDay = DatePart(DateInterval.Day, myDate)
        If Len(sDay) = 1 And Len(sDay) < 2 Then
            sDay = "0" & sDay
        End If
        sMonth = DatePart(DateInterval.Month, myDate)
        If Len(sMonth) = 1 And Len(sMonth) < 2 Then
            sMonth = "0" & sMonth
        End If
        sYear = DatePart(DateInterval.Year, myDate)
        FinalDate = sDay & "/" & sMonth & "/" & sYear
        strjscript = "<script language=""javascript"">"
        strjscript = strjscript & "window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = '" & FinalDate & "';window.close();"
        strjscript = strjscript & "<" & "/script>" 'Don't Ask, Tool Bug
        ltrDate.Text = strjscript
    End Sub

    Protected Sub calCalendar_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles calCalendar.VisibleMonthChanged
        mySetting.ArrangeDDLSelectedIndex(ddlYear, clsGlobalSetting.DDLSelection.SelectedValue, e.NewDate.Year.ToString)
        mySetting.ArrangeDDLSelectedIndex(ddlMonth, clsGlobalSetting.DDLSelection.SelectedValue, e.NewDate.Month.ToString)
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlYear.SelectedIndexChanged
        calCalendar.VisibleDate = New Date(ddlYear.SelectedValue, ddlMonth.SelectedValue, 1)
    End Sub

    Protected Sub ddlMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMonth.SelectedIndexChanged
        calCalendar.VisibleDate = New Date(ddlYear.SelectedValue, ddlMonth.SelectedValue, 1)
    End Sub

    Protected Sub imgBtnSelect_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSelect.Click
        If ValidateSelect() Then
            Dim strOutput As String
            If Session("TimeFormat").ToString = "12" Then
                If Session("ShowSecond").ToString = "Y" Then
                    strOutput = Trim(Session("FinalDate").ToString) & " " & Trim(ddlHour.SelectedValue) & ":" & Trim(ddlMinute.SelectedValue) & " " & Trim(ddlAmPm.SelectedValue)
                Else
                    strOutput = Trim(Session("FinalDate").ToString) & " " & Trim(ddlHour.SelectedValue) & ":" & Trim(ddlMinute.SelectedValue) & Trim(ddlSecond.SelectedValue) & " " & Trim(ddlAmPm.SelectedValue)
                End If
            Else
                If Session("ShowSecond").ToString = "Y" Then
                    strOutput = Trim(Session("FinalDate").ToString) & " " & Trim(ddlHour.SelectedValue) & ":" & Trim(ddlMinute.SelectedValue) & ":" & Trim(ddlSecond.SelectedValue)
                Else
                    strOutput = Trim(Session("FinalDate").ToString) & " " & Trim(ddlHour.SelectedValue) & ":" & Trim(ddlMinute.SelectedValue)
                End If
            End If

            strjscript = "<script language=""javascript"">"
            strjscript = strjscript & "window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = '" & strOutput & "';window.close();"
            strjscript = strjscript & "<" & "/script>"
            ltrDate.Text = strjscript
        End If
    End Sub

    Private Function ValidateSelect() As Boolean
        If Trim(Session("FinalDate").ToString) = "" Then
            calCalendar.Focus()
            lblError.Text = "Please click on calendar to select a date!"
            lblError.Visible = True
            Return False
        ElseIf Not IsNumeric(mySetting.ConvertDateToDecimal(Session("FinalDate").ToString, Session("Company").ToString, Session("Module").ToString)) Then
            txtCalendar.Focus()
            lblError.Text = "Invalid Format For Date."
            lblError.Visible = True
            Return False
        ElseIf ddlHour.Visible = True And ddlHour.SelectedValue = "" Then
            ddlHour.Focus()
            lblError.Text = "Please select a hour."
            lblError.Visible = True
            Return False
        ElseIf ddlMinute.Visible = True And ddlMinute.SelectedValue = "" Then
            ddlMinute.Focus()
            lblError.Text = "Please select a minute."
            lblError.Visible = True
            Return False
        ElseIf ddlSecond.Visible = True And ddlSecond.SelectedValue = "" Then
            ddlSecond.Focus()
            lblError.Text = "Please select a second."
            lblError.Visible = True
            Return False
        End If
        Return True
    End Function

    Protected Sub ddlHour_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlHour.SelectedIndexChanged
        GetSelectedDateTime()
    End Sub

    Protected Sub ddlMinute_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMinute.SelectedIndexChanged
        GetSelectedDateTime()
    End Sub

    Protected Sub ddlSecond_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSecond.SelectedIndexChanged
        GetSelectedDateTime()
    End Sub

    Protected Sub ddlAmPm_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAmPm.SelectedIndexChanged
        GetSelectedDateTime()
    End Sub

    Sub GetSelectedDateTime()
        txtCalendar.ReadOnly = True
        txtCalendar.Text = Trim(Session("FinalDate").ToString)
        If ddlHour.SelectedValue <> "" Then
            txtCalendar.Text = txtCalendar.Text & " " & ddlHour.SelectedValue
        Else
            txtCalendar.Text = txtCalendar.Text & " " & "00"
        End If

        If ddlMinute.SelectedValue <> "" Then
            txtCalendar.Text = txtCalendar.Text & ":" & ddlMinute.SelectedValue
        Else
            txtCalendar.Text = txtCalendar.Text & ":00"
        End If

        If ddlSecond.SelectedValue <> "" Then
            txtCalendar.Text = txtCalendar.Text & ":" & ddlSecond.SelectedValue & " " & ddlAmPm.SelectedValue
        Else
            txtCalendar.Text = txtCalendar.Text & " " & ddlAmPm.SelectedValue
        End If
        txtCalendar.Text = Trim(txtCalendar.Text)
    End Sub

End Class
