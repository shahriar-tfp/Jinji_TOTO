    <%@ Page Language="vb" %>
    <!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
    <HTML>
	    <HEAD id="headToFixThemesError" runat="server">
		    <title>Choose a Date</title>
		    <script runat="server">

		        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		            Threading.Thread.CurrentThread.CurrentCulture = Globalization.CultureInfo.CreateSpecificCulture(HttpContext.Current.Request.UserLanguages(0))
		            Dim dtSelected As String = Trim(HttpContext.Current.Request.QueryString("SelectedDate"))
            
		            Dim nFirstDayOfWeek As Integer = Val(HttpContext.Current.Application("rdConstant-FirstDayOfWeek"))
		            If nFirstDayOfWeek > -1 And nFirstDayOfWeek < 7 Then
		                rdCal.FirstDayOfWeek = nFirstDayOfWeek
		            End If

		            Dim sDateFormat As String = "Short Date"

		            If HttpContext.Current.Request.QueryString("DateFormat") <> Nothing Then
		                sDateFormat = HttpContext.Current.Request.QueryString("DateFormat")
		            End If
            
		            If sDateFormat = "Short Date" Then
		                sDateFormat = Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern()
		            End If

		            Dim MyDateTime As DateTime

		            Try
		                MyDateTime = DateTime.ParseExact(dtSelected, sDateFormat, Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat)
		                rdCal.SelectedDate = MyDateTime
		            Catch ex As Exception
		                Try
		                    rdCal.VisibleDate = dtSelected
		                Catch : End Try
		            End Try

		            rdCal.VisibleDate = rdCal.SelectedDate
            
		            'Go back to the invariant culture.
		            'Threading.Thread.CurrentThread.CurrentCulture = Globalization.CultureInfo.InvariantCulture()

		            'if isdate(dtSelected) then
		            '    rdCal.VisibleDate = dtSelected
		            'end if

		        End Sub

		        Private Sub rdCal_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
		            Dim sSelectedDate As String = rdCal.SelectedDate
		            If HttpContext.Current.Request.QueryString("DateFormat").Length <> 0 Then
		                sSelectedDate = Format(rdCal.SelectedDate, HttpContext.Current.Request.QueryString("DateFormat"))
		            End If

		            Dim strjscript As String = ""
		            strjscript &= "<script language=""javascript"">"
		            strjscript &= ";"
		            strjscript &= "if (window.opener.document." & HttpContext.Current.Request.QueryString("formname") & "[0]) {"  'If there's a hidden control with the same name (might happen with RequestForwarding) set the date for the first one only.
		            strjscript &= "  window.opener.document." & HttpContext.Current.Request.QueryString("formname") & "[0].value = '" & sSelectedDate & "';"
		            strjscript &= "} else {"  'Only one control with this name.
		            strjscript &= "  window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = '" & sSelectedDate & "';"
		            strjscript &= "}"
		            If Not IsNothing(HttpContext.Current.Request.QueryString("ChangeFlagElementID")) Then
		                strjscript &= "window.opener.document.rdForm." & HttpContext.Current.Request.QueryString("ChangeFlagElementID") & ".value = 'True';"
		            End If
		            strjscript &= "window.close();"
		            strjscript &= "if (window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".onchange) {"
		            strjscript &= "   window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".onchange({type: 'change'});}"
		            strjscript &= "<" & "/script>"  'Looks silly, but fixes an editor bug.
                Literal1.text = strjscript
            End Sub

        Private Sub rdCal_DayRender(sender As Object, e As System.Web.UI.WebControls.DayRenderEventArgs)
            dim dtSelected as string = Httpcontext.Current.Request.Querystring("SelectedDate")
            if isdate(dtSelected) then
                'rdCal.Year = dtSelected.Year
                'rdCal.Month = dtSelected.Month
                if e.day.date = cdate(dtSelected)
                    e.Cell.BackColor = System.Drawing.Color.LightGray
                End If
            else
                'Not date, use the today's date.
                'If e.Day.Date = datetime.now().tostring("d") Then
                If e.Day.Date = datetime.now() Then
                    e.Cell.BackColor = System.Drawing.Color.LightGray
                End If
            end if
        End Sub

		    </script>
		    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		    <style>A { TEXT-DECORATION: none }
		    </style>
	    </HEAD>
	    <body bottommargin="0" bgcolor="#deebff" leftmargin="0" topmargin="0" rightmargin="0">
		    <div style="TEXT-DECORATION: none">
			    <form id="Form1" runat="server">
				    <asp:calendar id="rdCal" runat="server" BorderWidth="0px" BorderStyle="None" Font-Size="9pt" Font-Names="Arial" OnSelectionChanged="rdCal_SelectionChanged" OnDayRender="rdCal_dayrender" showtitle="true" DayNameFormat="FirstTwoLetters" SelectionMode="Day" BackColor="#DEEBFF" FirstDayOfWeek="Sunday" BorderColor="#000000" ForeColor="#000000" Height="100%" Width="100%">
					    <NextPrevStyle forecolor="White" backcolor="Gray"></NextPrevStyle>
					    <DayHeaderStyle font-bold="True" forecolor="White" backcolor="#639AFF"></DayHeaderStyle>
					    <TitleStyle font-bold="True" forecolor="White" backcolor="Gray"></TitleStyle>
					    <OtherMonthDayStyle forecolor="Silver"></OtherMonthDayStyle>
				    </asp:calendar>
				    <p>
				    </p>
				    <p align="center">
					    <asp:literal id="Literal1" runat="server"></asp:literal>
				    </p>
			    </form>
		    </div>
	    </body>
    </HTML>
