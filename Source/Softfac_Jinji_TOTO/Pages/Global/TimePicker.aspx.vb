Imports System.Data
Imports System.Data.SqlClient

Partial Class Pages_Global_TimePicker
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
            ddlSecond.Visible = True
            lblSecond.Visible = True
        Else
            ddlSecond.Visible = False
            lblSecond.Visible = False
        End If
        'imgBtnSelect.ImageUrl = "../../Images/Company/Default/BtnBlue/btnSelect.gif"
    End Sub

    Protected Sub imgBtnSelect_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSelect.Click
        If ValidateSelect() Then
            Dim strOutput As String
            If Session("TimeFormat").ToString = "12" Then
                If Session("ShowSecond").ToString = "Y" Then
                    strOutput = Trim(ddlHour.SelectedValue) & ":" & Trim(ddlMinute.SelectedValue) & " " & Trim(ddlAmPm.SelectedValue)
                Else
                    strOutput = Trim(ddlHour.SelectedValue) & ":" & Trim(ddlMinute.SelectedValue) & Trim(ddlSecond.SelectedValue) & " " & Trim(ddlAmPm.SelectedValue)
                End If
            Else
                If Session("ShowSecond").ToString = "Y" Then
                    strOutput = Trim(ddlHour.SelectedValue) & ":" & Trim(ddlMinute.SelectedValue) & ":" & Trim(ddlSecond.SelectedValue)
                Else
                    strOutput = Trim(ddlHour.SelectedValue) & ":" & Trim(ddlMinute.SelectedValue)
                End If
            End If

            strjscript = "<script language=""javascript"">"
            strjscript = strjscript & "window.opener.document." & HttpContext.Current.Request.QueryString("formname") & ".value = '" & strOutput & "';window.close();"
            strjscript = strjscript & "<" & "/script>"
            ltrDate.Text = strjscript
        End If
    End Sub
    Private Function ValidateSelect() As Boolean
        If ddlHour.Visible = True And ddlHour.SelectedValue = "" Then
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

End Class
