Imports System.Data

Partial Class Pages_Training_TD_UserCourseCalendar
    Inherits System.Web.UI.Page
    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting, myDS As New DataSet
    Dim ssql As String = "", i As Integer
    Dim dtSelectedDate As DateTime
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
            Session("Module") = "TRAINING"

            If Session("ScreenWidth") = 0 Then
                Session("ScreenWidth") = "1024"
                Session("GVwidth") = Session("ScreenWidth") - 360
            End If
            If Session("ScreenHeight") = 0 Then
                Session("ScreenHeight") = "768"
                Session("GVheight") = (Session("ScreenHeight") / 2) - 50
            End If

            PagePreload()
            BindCalendar()
        Else
            lblResult.Visible = False
            BindCalendar()
        End If
    End Sub

    Protected Sub calEventCalendar_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles calEventCalendar.DayRender
        Try
            Dim strEvents As New StringBuilder
            Dim strEdit As New StringBuilder
            Dim dr As DataRow
            i = i + 1

            strEvents.Append("<span>")
            Dim myLabel As New Label
            myLabel.ID = "lbl" & i
            dr = myDS.Tables(0).Rows.Find(e.Day.Date.ToString)
            If Not dr Is Nothing Then
                strEvents.Append("<br>&nbsp;")
                myLabel.Text = dr("Code").ToString
            Else
                strEvents.Append("<br>&nbsp;")
                myLabel.Text = ""
            End If
            strEvents.Append("</span>")
            e.Cell.Controls.Add(New LiteralControl(strEvents.ToString))
            e.Cell.Controls.Add(myLabel)
            If dtSelectedDate = e.Day.Date Then
                dtSelectedDate = Nothing
            End If
        Catch ex As Exception
            lblResult.Visible = True
            lblResult.Text = "Error Occur In DayRender() Function." & vbCrLf & ex.Message.ToString
        End Try
    End Sub

    Protected Sub calEventCalendar_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calEventCalendar.SelectionChanged
        Try
            Dim myDate As DateTime, sDay As String, sMonth As String, sYear As String
            dtSelectedDate = calEventCalendar.SelectedDate
            myDate = calEventCalendar.SelectedDate
            sDay = DatePart(DateInterval.Day, myDate)

            If Len(sDay) = 1 And Len(sDay) < 2 Then
                sDay = "0" & sDay
            End If
            sMonth = DatePart(DateInterval.Month, myDate)
            If Len(sMonth) = 1 And Len(sMonth) < 2 Then
                sMonth = "0" & sMonth
            End If
            sYear = DatePart(DateInterval.Year, myDate)

            BindCalendar()
        Catch ex As Exception
            lblResult.Visible = True
            lblResult.Text = "Error Occur In SelectionChange() Function Of Calendar." & vbCrLf & ex.Message.ToString
        End Try
    End Sub

    Protected Sub calEventCalendar_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles calEventCalendar.VisibleMonthChanged
        Try
            Session("SelectedYear") = e.NewDate.Year.ToString
            Session("SelectedMonth") = e.NewDate.Month.ToString
            BindCalendar()
            mySetting.ArrangeDDLSelectedIndex(ddlYear, clsGlobalSetting.DDLSelection.SelectedValue, e.NewDate.Year.ToString)
            mySetting.ArrangeDDLSelectedIndex(ddlMonth, clsGlobalSetting.DDLSelection.SelectedValue, e.NewDate.Month.ToString)
        Catch ex As Exception
            lblResult.Visible = True
            lblResult.Text = "Error Occur In VisibleMonthChange() Function Of Calendar." & vbCrLf & ex.Message.ToString
        End Try
    End Sub

    Private Function ValidateInput() As Boolean
        Return True
    End Function

    Private Sub PagePreload()
        Try
            btnColourDef = Session("strTheme")
            btnColourAlt = Session("strThemeAlt")
            'Get Page Title
            ssql = "Exec sp_sa_GetPageTitle '" & Form.ID & "'"
            myDS = mySQL.ExecuteSQL(ssql)
            If myDS.Tables(0).Rows.Count > 0 Then
                lblTitle.Text = myDS.Tables(0).Rows(0).Item(0)
                lblTitle.CssClass = "wordstyle4"
            End If

            body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

            ssql = "Exec sp_tms_GenerateYear 50,10"
            myDS = New DataSet
            myDS = mySQL.ExecuteSQL(ssql)
            If myDS.Tables.Count > 0 Then
                If myDS.Tables(0).Rows.Count > 0 Then
                    ddlYear.DataValueField = "year"
                    ddlYear.DataTextField = "year"
                    ddlYear.DataSource = myDS.Tables(0).DefaultView
                    ddlYear.DataBind()
                End If
            End If
            myDS = Nothing

            mySetting.ArrangeDDLSelectedIndex(ddlYear, clsGlobalSetting.DDLSelection.SelectedValue, DatePart(DateInterval.Year, Now()))
            Session("SelectedYear") = ddlYear.SelectedValue

            'Bind Data ddlMonth
            ssql = "Exec sp_tms_GenerateMonth 1,12"
            myDS = New DataSet
            myDS = mySQL.ExecuteSQL(ssql)
            If myDS.Tables.Count > 0 Then
                If myDS.Tables(0).Rows.Count > 0 Then
                    ddlMonth.DataValueField = "month"
                    ddlMonth.DataTextField = "desc"
                    ddlMonth.DataSource = myDS.Tables(0).DefaultView
                    ddlMonth.DataBind()
                End If
            End If
            myDS = Nothing
            mySetting.ArrangeDDLSelectedIndex(ddlMonth, clsGlobalSetting.DDLSelection.SelectedValue, DatePart(DateInterval.Month, Now()))
            Session("SelectedMonth") = ddlMonth.SelectedValue

        Catch ex As Exception
            lblResult.Visible = True
            lblResult.Text = "Error Occur In PagePreload Process." & vbCrLf & ex.Message.ToString
        End Try
    End Sub

    Private Sub BindCalendar()
        Try
            'Bind Data calEventCalendar
            ssql = "Exec sp_tms_GetCalendarRecords '" & Session("Company").ToString & "','','','','" & Session("SelectedYear").ToString & "','" & Session("SelectedMonth").ToString & "','" & Form.ID.ToString & "'"
            myDS = New DataSet
            myDS = mySQL.ExecuteSQL(ssql)
            If myDS.Tables.Count > 0 Then
                If myDS.Tables(0).Rows.Count > 0 Then
                    myDS.Tables(0).PrimaryKey = New DataColumn() {myDS.Tables(0).Columns("date")}
                End If
            End If
        Catch ex As Exception
            lblResult.Visible = True
            lblResult.Text = "Error Occur In BindCalendar() Function." & vbCrLf & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlYear.SelectedIndexChanged
        Session("SelectedYear") = ddlYear.SelectedValue
        BindCalendar()
        calEventCalendar.VisibleDate = New Date(ddlYear.SelectedValue, ddlMonth.SelectedValue, 1)
    End Sub

    Protected Sub ddlMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMonth.SelectedIndexChanged
        Session("SelectedMonth") = ddlMonth.SelectedValue
        BindCalendar()
        calEventCalendar.VisibleDate = New Date(ddlYear.SelectedValue, ddlMonth.SelectedValue, 1)
    End Sub
    
End Class

