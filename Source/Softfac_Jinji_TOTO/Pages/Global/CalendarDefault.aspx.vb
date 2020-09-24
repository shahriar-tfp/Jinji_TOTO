Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.web.Configuration

Partial Class Pages_Global_CalendarDefault
    Inherits System.Web.UI.Page

    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting, myDS As New DataSet, ssql As String
    Private WithEvents myReadFile As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory() & "App_GlobalResources\Setup\Setup.ini")
    Dim DateType As String
    Dim strYear, strMonth, strDay, curYear, curMonth, curDay, selYear, selMonth, selDay As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            PagePreload()
        End If
    End Sub

    Private Sub PagePreload()

        'Bind Data ddlYear
        DateType = HttpContext.Current.Request.QueryString("datetype")
        If DateType = "ExpiryDate" Then
            ssql = "Exec sp_calendar_GenerateYear " & 0 & "," & 20
        ElseIf DateType = "EndDate" Then
            strYear = CInt(Left(Trim(Session("StartDate").ToString), 4))
            curYear = DatePart(DateInterval.Year, Now())
            ssql = "Exec sp_calendar_GenerateYear " & curYear - strYear & "," & 20
        Else
            ssql = "Exec sp_calendar_GenerateYear " & 80 & "," & 20
        End If
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
        DateType = HttpContext.Current.Request.QueryString("datetype")
        If DateType = "ExpiryDate" Then
            curMonth = DatePart(DateInterval.Month, Now())
            ssql = "Exec sp_calendar_GenerateMonth " & curMonth & ",12"
        ElseIf DateType = "EndDate" Then
            If ddlYear.SelectedValue = strYear Then
                strMonth = CInt(Mid(Trim(Session("StartDate").ToString), 5, 2))
            Else
                strMonth = 1
            End If
            ssql = "Exec sp_calendar_GenerateMonth " & strMonth & ",12"
        Else
            ssql = "Exec sp_calendar_GenerateMonth 1,12"
        End If
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                ddlMonth.DataValueField = "month"
                ddlMonth.DataTextField = "desc"
                ddlMonth.DataSource = myDS.Tables(0).DefaultView
                ddlMonth.DataBind()
            End If
        End If
        If DateType = "EndDate" Then
            mySetting.ArrangeDDLSelectedIndex(ddlMonth, clsGlobalSetting.DDLSelection.SelectedValue, strMonth)
        Else
            mySetting.ArrangeDDLSelectedIndex(ddlMonth, clsGlobalSetting.DDLSelection.SelectedValue, DatePart(DateInterval.Month, Now()))
        End If

    End Sub

    Protected Sub calCalendar_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles calCalendar.VisibleMonthChanged
        mySetting.ArrangeDDLSelectedIndex(ddlYear, clsGlobalSetting.DDLSelection.SelectedValue, e.NewDate.Year.ToString)
        mySetting.ArrangeDDLSelectedIndex(ddlMonth, clsGlobalSetting.DDLSelection.SelectedValue, e.NewDate.Month.ToString)
    End Sub

    Protected Sub calCalendar_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calCalendar.SelectionChanged
        Dim myDate As DateTime
        Dim sDay As String = "", sMonth As String = "", sYear As String = ""
        Dim FinalDate As String = ""
        Dim strjscript As String = ""

        Try
            myDS = mySQL.ExecuteSQL("select string_value1,string_value2,string_value3 from parameter where company_profile_code='" & Session("Company") & "' and module_profile_code='" & Session("Module") & "' and code='REGIONAL_SETTING'", Session("Company").ToString, Session("EmpID").ToString)
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
                End Select

                'Get Date by Type
                DateType = HttpContext.Current.Request.QueryString("datetype")
                If DateType = "StartDate" Then
                    Session("StartDate") = sYear & sMonth & sDay
                End If

                'Return Value
                strjscript = "<script language=""javascript"">"
                strjscript = strjscript & "window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = '" & FinalDate & "';window.close();"
                strjscript = strjscript & "<" & "/script>"
                ltrDate.Text = strjscript
                myDS = Nothing
                Exit Sub

            Else
                GoTo DefaultSetting
            End If
        Catch ex As Exception
            GoTo DefaultSetting
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

    Protected Sub calCalendar_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles calCalendar.DayRender
        'strDay = CInt(Right(Session("StartDate").ToString, 2))
        'Dim temp1 As String = DateTime.Now().ToString("d")
        If e.Day.Date = DateTime.Now().ToString("d") Then
            'e.Cell.BackColor = System.Drawing.Color.LightGray
        End If
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlYear.SelectedIndexChanged

        If HttpContext.Current.Request.QueryString("datetype") = "EndDate" Then
            'Bind Data ddlMonth
            selYear = ddlYear.SelectedValue
            If selYear = CInt(Left(Trim(Session("StartDate").ToString), 4)) Then
                strMonth = CInt(Mid(Trim(Session("StartDate").ToString), 5, 2))
            Else
                strMonth = 1
            End If
            ssql = "Exec sp_calendar_GenerateMonth " & strMonth & ",12"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables.Count > 0 Then
                If myDS.Tables(0).Rows.Count > 0 Then
                    ddlMonth.DataValueField = "month"
                    ddlMonth.DataTextField = "desc"
                    ddlMonth.DataSource = myDS.Tables(0).DefaultView
                    ddlMonth.DataBind()
                End If
            End If
            mySetting.ArrangeDDLSelectedIndex(ddlMonth, clsGlobalSetting.DDLSelection.SelectedValue, strMonth)
        Else
            mySetting.ArrangeDDLSelectedIndex(ddlMonth, clsGlobalSetting.DDLSelection.SelectedValue, 1)
        End If

        If HttpContext.Current.Request.QueryString("datetype") = "ExpiryDate" Then
            'Bind Data ddlMonth
            selYear = ddlYear.SelectedValue
            If selYear = DatePart(DateInterval.Year, Now()) Then
                strMonth = DatePart(DateInterval.Month, Now())
            Else
                strMonth = 1
            End If
            ssql = "Exec sp_calendar_GenerateMonth " & strMonth & ",12"
            myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
            If myDS.Tables.Count > 0 Then
                If myDS.Tables(0).Rows.Count > 0 Then
                    ddlMonth.DataValueField = "month"
                    ddlMonth.DataTextField = "desc"
                    ddlMonth.DataSource = myDS.Tables(0).DefaultView
                    ddlMonth.DataBind()
                End If
            End If
            mySetting.ArrangeDDLSelectedIndex(ddlMonth, clsGlobalSetting.DDLSelection.SelectedValue, strMonth)
        Else
            mySetting.ArrangeDDLSelectedIndex(ddlMonth, clsGlobalSetting.DDLSelection.SelectedValue, 1)
        End If
        calCalendar.VisibleDate = New Date(ddlYear.SelectedValue, ddlMonth.SelectedValue, 1)

    End Sub

    Protected Sub ddlMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMonth.SelectedIndexChanged
        calCalendar.VisibleDate = New Date(ddlYear.SelectedValue, ddlMonth.SelectedValue, 1)
    End Sub

End Class
