Imports System.Data

Partial Class Pages_Roster_Calendar_Profile
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
            Session("OCP_ID_CALENDAR") = ""
            Response.CacheControl = "no-cache"
            Response.AddHeader("Pragma", "no-cache")
            Response.Expires = -1
            Session("Module") = "ROSTER"

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
                    txtOCP_ID_DAY_TYPE.Text = myLabel.Text
                    'mySetting.ArrangeDDLSelectedIndex(ddlOCP_ID_DAY_TYPE, clsGlobalSetting.DDLSelection.SelectedText, Trim(myLabel.Text))
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
            If Trim(txtFromDate.Text.ToString) = "" Then
                txtFromDate.Text = sDay & "/" & sMonth & "/" & sYear
            Else
                If Trim(txtToDate.Text.ToString) = "" Then
                    txtToDate.Text = sDay & "/" & sMonth & "/" & sYear
                Else
                    txtFromDate.Text = sDay & "/" & sMonth & "/" & sYear
                End If
            End If

            pnlEdit.Visible = True
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
        If Trim(txtFromDate.Text) = "" Then
            lblResult.Visible = True
            lblResult.Text = "[FromDate] cannot be blank!"
            Return False
        ElseIf Trim(txtFromDate.Text) <> "" Then
            If Not IsNumeric(mySetting.ConvertDateToDecimal(txtFromDate.Text.ToString, Session("Company"), "DATETIME")) Then
                txtFromDate.Focus()
                lblResult.Visible = True
                lblResult.Text = "Invalid Format [dd/mm/yyyy] For [FromDate]!"
                Return False
            End If
        End If
        If Trim(txtToDate.Text) <> "" Then
            If Not IsNumeric(mySetting.ConvertDateToDecimal(txtToDate.Text.ToString, Session("Company"), "DATETIME")) Then
                txtToDate.Focus()
                lblResult.Visible = True
                lblResult.Text = "Invalid Format [dd/mm/yyyy] For [ToDate]!"
                Return False
            End If
        End If
        If Trim(txtFromDate.Text.ToString) <> "" And Trim(txtToDate.Text.ToString) <> "" Then
            If mySetting.ConvertDateToDecimal(Trim(txtFromDate.Text.ToString), Session("Company").ToString, Session("Module").ToString) > mySetting.ConvertDateToDecimal(Trim(txtToDate.Text.ToString), Session("Company").ToString, Session("Module").ToString) Then
                lblResult.Visible = True
                lblResult.Text = "[DateTo] must be greater than or equal to [DateFrom]"
                Return False
            End If
        End If

        If ddlOptionType.SelectedIndex = 0 Then
            If Trim(txtOCP_ID_DAY_TYPE.Text.ToString) = "" Then
                lblResult.Visible = True
                lblResult.Text = "Please select a day type!"
                txtOCP_ID_DAY_TYPE.Focus()
                Return False
            End If
        ElseIf ddlOptionType.SelectedIndex = 1 Then

        ElseIf ddlOptionType.SelectedIndex = 2 Then

        End If

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
            mySetting.GetBtnImgUrl(btnSubmit, Session("Company").ToString, btnColourDef, "btnSubmit.png")
            mySetting.GetBtnImgUrl(btnCancel, Session("Company").ToString, btnColourDef, "btnClear.png")

            mySetting.GetImgUrl(imgbtnFromDate, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
            mySetting.GetImgUrl(imgbtnToDate, clsGlobalSetting.ImageType._DATE, Session("Company").ToString)
            mySetting.GetImgUrl(imgbtnOCP_ID_CALENDAR, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
            mySetting.GetImgUrl(imgbtnOCP_ID_DAY_TYPE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)

            mySetting.PopUpCalendar_ImageButton(imgbtnFromDate, Form.ID, "txtFromDate")
            mySetting.PopUpCalendar_ImageButton(imgbtnToDate, Form.ID, "txtToDate")

            mySetting.GetLookupValue_ImageButton(imgbtnOCP_ID_CALENDAR, Form.ID, txtOCP_ID_CALENDAR.ID, "CodeName", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_CALENDAR" & """," & """" & "" & """," & """" & Session("Module").ToString & """")
            mySetting.GetLookupValue_ImageButton(imgbtnOCP_ID_DAY_TYPE, Form.ID, txtOCP_ID_DAY_TYPE.ID, "Code", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "OCP_ID_DAY_TYPE" & """," & """" & "" & """," & """" & Session("Module").ToString & """")

            body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

            ssql = "Select Code,Name From Table_Field Where Table_Profile_Code='" & Form.ID & _
                    "' And Code In ('OCP_ID_CALENDAR','OCP_ID_DAY_TYPE') Order By Sequence_No"
            myDS = New DataSet
            myDS = mySQL.ExecuteSQL(ssql)
            For i = 0 To myDS.Tables(0).Rows.Count - 1
                Dim myLBL As Label = Page.FindControl("lbl" & myDS.Tables(0).Rows(i).Item(0).ToString)
                myLBL.Text = myDS.Tables(0).Rows(i).Item(1).ToString
            Next
            myDS = Nothing

            'bind setting type dropdownlist
            lblOptionType.Text = "Pattern Type"
            ddlOptionType.Items.Add("")
            ddlOptionType.Items.Add("Calendar Pattern")
            ddlOptionType.Items.Add("Off Day Pattern")
            'ddlOptionType.Items.Add("Excluded")

            ddlOptionType.SelectedIndex = 0
            pnlTypeA.Visible = False
            pnlTypeB.Visible = False
            pnlTypeC.Visible = True
            pnlTypeD.Visible = True
            pnlTypeE.Visible = True

            txtTypeA1.MaxLength = 3
            txtTypeA2.MaxLength = 3
            txtTypeA3.MaxLength = 3

            lblTypeA1.Text = "1st Type"
            lblTypeA2.Text = "2nd Type"
            lblTypeA3.Text = "3rd Type"
            lblMon.Text = "Monday"
            lblTue.Text = "Tuesday"
            lblWed.Text = "Wednesday"
            lblThu.Text = "Thursday"
            lblFri.Text = "Friday"
            lblSat.Text = "Saturday"
            lblSun.Text = "Sunday"

            'Bind Calendar Partern Control
            ssql = "Create Table #Result([Code] nvarchar(50), [Name] nvarchar(255)); Insert Into #Result Select '',''; Insert Into #Result Select ID, [Name] From Organisation_Code_Profile_Vw Where (Code Not Like '%~~%' or Name Not Like '%~~%') And Option_Type IN ('DAY_TYPE') And Company_Profile_Code = N'" & Session("company").ToString & "' Order By [Name]; Select * From #Result; Drop Table #Result"
            myDS = mySQL.ExecuteSQL(ssql)
            If myDS.Tables.Count > 0 Then
                If myDS.Tables(0).Columns.Count > 1 Then
                    ddlTypeA1.DataTextField = "Name"
                    ddlTypeA1.DataValueField = "Code"
                    ddlTypeA1.DataSource = myDS
                    ddlTypeA1.DataBind()

                    ddlTypeA2.DataTextField = "Name"
                    ddlTypeA2.DataValueField = "Code"
                    ddlTypeA2.DataSource = myDS
                    ddlTypeA2.DataBind()

                    ddlTypeA3.DataTextField = "Name"
                    ddlTypeA3.DataValueField = "Code"
                    ddlTypeA3.DataSource = myDS
                    ddlTypeA3.DataBind()

                    ddlMon.DataTextField = "Name"
                    ddlMon.DataValueField = "Code"
                    ddlMon.DataSource = myDS
                    ddlMon.DataBind()

                    ddlTue.DataTextField = "Name"
                    ddlTue.DataValueField = "Code"
                    ddlTue.DataSource = myDS
                    ddlTue.DataBind()

                    ddlWed.DataTextField = "Name"
                    ddlWed.DataValueField = "Code"
                    ddlWed.DataSource = myDS
                    ddlWed.DataBind()

                    ddlThu.DataTextField = "Name"
                    ddlThu.DataValueField = "Code"
                    ddlThu.DataSource = myDS
                    ddlThu.DataBind()

                    ddlFri.DataTextField = "Name"
                    ddlFri.DataValueField = "Code"
                    ddlFri.DataSource = myDS
                    ddlFri.DataBind()

                    ddlSat.DataTextField = "Name"
                    ddlSat.DataValueField = "Code"
                    ddlSat.DataSource = myDS
                    ddlSat.DataBind()

                    ddlSun.DataTextField = "Name"
                    ddlSun.DataValueField = "Code"
                    ddlSun.DataSource = myDS
                    ddlSun.DataBind()
                End If
            End If
            myDS = Nothing

            'Bind Data ddlYear
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
            ssql = "Exec sp_tms_GetCalendarRecords '" & Session("Company").ToString & "','" & txtOCP_ID_CALENDAR.Text.ToString.Trim & "','','','" & Session("SelectedYear").ToString & "','" & Session("SelectedMonth").ToString & "','" & Form.ID.ToString & "'"
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

    Protected Sub imgbtnFromDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnFromDate.Click
        BindCalendar()
    End Sub

    Protected Sub imgbtnToDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnToDate.Click
        BindCalendar()
    End Sub

    Private Sub ClearText()

        If ddlOptionType.SelectedIndex = 0 Then
            txtFromDate.Text = ""
            txtToDate.Text = ""
            txtOCP_ID_DAY_TYPE.Text = ""
        ElseIf ddlOptionType.SelectedIndex = 1 Then
            txtTypeA1.Text = ""
            txtTypeA2.Text = ""
            txtTypeA3.Text = ""
            ddlTypeA1.SelectedIndex = 0
            ddlTypeA2.SelectedIndex = 0
            ddlTypeA3.SelectedIndex = 0
            txtFromDate.Text = ""
            txtToDate.Text = ""
        ElseIf ddlOptionType.SelectedIndex = 2 Then
            chkMon.Checked = False
            chkTue.Checked = False
            chkWed.Checked = False
            chkThu.Checked = False
            chkFri.Checked = False
            chkSat.Checked = False
            chkSun.Checked = False
            ddlMon.SelectedIndex = 0
            ddlTue.SelectedIndex = 0
            ddlWed.SelectedIndex = 0
            ddlThu.SelectedIndex = 0
            ddlFri.SelectedIndex = 0
            ddlSat.SelectedIndex = 0
            ddlSun.SelectedIndex = 0
            txtFromDate.Text = ""
            txtToDate.Text = ""
        End If

    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSubmit.Click
        Try
            If ValidateInput() Then
                myDS = New DataSet
                If ddlOptionType.SelectedIndex = 0 Then
                    ssql = "Exec sp_tms_InsUpdDelCalendar '" & Session("Company").ToString & "','" & Trim(txtFromDate.Text.ToString) & "','" & Trim(txtToDate.Text.ToString) & "','" & txtOCP_ID_CALENDAR.Text.ToString & "','" & txtOCP_ID_DAY_TYPE.Text.ToString.Trim & "','','','','Calendar_Profile'"
                ElseIf ddlOptionType.SelectedIndex = 1 Then
                    ssql = "Exec sp_tms_InsUpdDelCalendar '" & Session("Company").ToString & "','" & Trim(txtFromDate.Text.ToString) & "','" & Trim(txtToDate.Text.ToString) & "','" & txtOCP_ID_CALENDAR.Text.ToString & "','" & txtTypeA1.Text.Trim & "','" & ddlTypeA1.SelectedValue.ToString & "','" & txtTypeA2.Text.Trim & "','" & ddlTypeA2.SelectedValue.ToString & "','Calendar_Profile2','" & txtTypeA3.Text.Trim & "','" & ddlTypeA3.SelectedValue.ToString & "'"
                ElseIf ddlOptionType.SelectedIndex = 2 Then
                    Dim valMon, valTue, valWed, valThu, valFri, valSat, valSun As String
                    If chkMon.Checked = True Then
                        valMon = "YES"
                    Else
                        valMon = "NO"
                    End If
                    If chkTue.Checked = True Then
                        valTue = "YES"
                    Else
                        valTue = "NO"
                    End If
                    If chkWed.Checked = True Then
                        valWed = "YES"
                    Else
                        valWed = "NO"
                    End If
                    If chkThu.Checked = True Then
                        valThu = "YES"
                    Else
                        valThu = "NO"
                    End If
                    If chkFri.Checked = True Then
                        valFri = "YES"
                    Else
                        valFri = "NO"
                    End If
                    If chkSat.Checked = True Then
                        valSat = "YES"
                    Else
                        valSat = "NO"
                    End If
                    If chkSun.Checked = True Then
                        valSun = "YES"
                    Else
                        valSun = "NO"
                    End If
                    ssql = "Exec sp_tms_InsUpdDelCalendar '" & Session("Company").ToString & "','" & Trim(txtFromDate.Text.ToString) & "','" & Trim(txtToDate.Text.ToString) & "','" & txtOCP_ID_CALENDAR.Text.ToString & "','" & valMon & "','" & ddlMon.SelectedValue.ToString & "','" & valTue & "','" & ddlTue.SelectedValue.ToString & "','Calendar_Profile3','" & valWed & "','" & ddlWed.SelectedValue.ToString & "','" & valThu & "','" & ddlThu.SelectedValue.ToString & "','" & valFri & "','" & ddlFri.SelectedValue.ToString & "','" & valSat & "','" & ddlSat.SelectedValue.ToString & "','" & valSun & "','" & ddlSun.SelectedValue.ToString & "'"
                End If

                mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                ClearText()
            End If
            BindCalendar()
        Catch ex As Exception
            lblResult.Visible = True
            lblResult.Text = "Error Occur In Submit Process." & vbCrLf & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCancel.Click
        BindCalendar()
        ClearText()
    End Sub

    Protected Sub imgbtnOCP_ID_CALENDAR_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnOCP_ID_CALENDAR.Click
        BindCalendar()
    End Sub

    Protected Sub ddlOptionType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOptionType.SelectedIndexChanged
        If ddlOptionType.SelectedIndex = 1 Then
            pnlTypeA.Visible = True
            pnlTypeB.Visible = False
            pnlTypeC.Visible = True
            pnlTypeD.Visible = False
            pnlTypeE.Visible = True
        ElseIf ddlOptionType.SelectedIndex = 2 Then
            pnlTypeA.Visible = False
            pnlTypeB.Visible = True
            pnlTypeC.Visible = True
            pnlTypeD.Visible = False
            pnlTypeE.Visible = True
        Else
            pnlTypeA.Visible = False
            pnlTypeB.Visible = False
            pnlTypeC.Visible = True
            pnlTypeD.Visible = True
            pnlTypeE.Visible = True
        End If
    End Sub
End Class

