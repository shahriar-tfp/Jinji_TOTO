Imports System.Data
Partial Class Pages_Global_Calendar
    Inherits System.Web.UI.Page
    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting, myDS As New DataSet, ssql As String
    Private WithEvents myReadFile As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory() & "App_GlobalResources\Setup\Setup.ini")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        If Session.IsNewSession Then
            SelfClose()
        End If
        If Not IsPostBack Then
            PagePreload()
        End If
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
                        'FinalDate = sDay & "/" & sMonth & "/" & sYear
                End Select
                strjscript = "<script language=""javascript"">"
                strjscript = strjscript & "window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = '" & FinalDate & "';"
                Dim strFormName As String = "", strControlName As String = ""
                Dim posSplit As Integer = Request.QueryString("formname").ToString.IndexOf(".")


                strFormName = Left(Request.QueryString("formname").ToString, posSplit)
                strControlName = Right(Request.QueryString("formname").ToString, Len(Request.QueryString("formname").ToString) - (posSplit + 4))
                ssql = "Exec sp_sa_GetAutoFillData '" & Session("Company").ToString & "','" & _
                        Session("Module").ToString & "','" & mySetting.ConvertDateToDecimal(FinalDate, Session("Company").ToString, Session("Module").ToString) & "','" & _
                        strFormName & "','" & strControlName & "','AUTOFILL','" & Request.QueryString("filter").ToString & "','" & Request.QueryString("filter2").ToString & "'"
                Dim tmpDS As New DataSet, i As Integer
                tmpDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                If Not tmpDS Is Nothing Then
                    If tmpDS.Tables.Count > 0 Then
                        If tmpDS.Tables(0).Rows.Count > 0 Then
                            For i = 0 To tmpDS.Tables(0).Columns.Count - 1
                                strjscript = strjscript & "window.opener.document." & strFormName & ".txt" & _
                                    tmpDS.Tables(0).Columns(i).ColumnName.ToString.ToUpper() & ".value='" & tmpDS.Tables(0).Rows(0).Item(i).ToString & "';"
                            Next
                        End If
                    End If
                End If
                tmpDS = Nothing
                strjscript = strjscript & "window.close();" & "window.opener.document." & strFormName & ".submit();"
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
        If e.Day.Date = DateTime.Now().ToString("d") Then
            'e.Cell.BackColor = System.Drawing.Color.LightGray
        End If
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlYear.SelectedIndexChanged
        calCalendar.VisibleDate = New Date(ddlYear.SelectedValue, ddlMonth.SelectedValue, 1)
    End Sub

    Protected Sub ddlMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMonth.SelectedIndexChanged
        calCalendar.VisibleDate = New Date(ddlYear.SelectedValue, ddlMonth.SelectedValue, 1)
    End Sub
End Class
