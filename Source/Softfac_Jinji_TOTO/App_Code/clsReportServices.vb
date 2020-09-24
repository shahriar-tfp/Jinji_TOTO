Imports Microsoft.VisualBasic

Public Class clsReportServices
    'Function GetReportURL(ByVal strCompanyID As String, ByVal strUserID As String, ByVal strModule As String, ByVal strTableID As String, ByVal intPageSize As Integer, ByVal intCurrentPage As Integer, _
    'ByVal strReportName As String, ByVal strImageURL As String, ByVal strCompanyName As String) As String
    '    Dim myTargetURL As String = ""

    '    myTargetURL = "http://hcrm.softfac.com:8383/HCRMReports/rdPage.aspx?&ImageURL=" & strImageURL.ToString.Trim & _
    '            "&CompanyName=" & strCompanyName.ToString.Trim & "&CompanyID=" & strCompanyID.ToString.Trim & _
    '            "&UserID=" & strUserID.ToString.Trim & "&rdReport=" & strModule.ToString.Trim & ".RPT_" & strTableID.ToString & _
    '            "&ModuleName=" & strModule.ToString.Trim & "&TableName=" & strTableID.ToString.Trim & _
    '            "&PageSize=" & intPageSize & "&CurrentPage=" & intCurrentPage.ToString & _
    '            "&ReportName=" & strReportName.ToString.Trim & "&CurrentDate=" & Now.ToString
    '    Dim strScript As String = "<script language=""""javascript"""" runat=""""server"""">"
    '    strScript &= "window.open('" & myTargetURL & "');" & "</script>"

    '    'Page.ClientScript.RegisterStartupScript(GetType(Page), "ShowReport", strScript)
    '    'System.Diagnostics.Process.Start(myTargetURL)
    '    Return myTargetURL
    'End Function
    Function GetReportURL(ByVal strCompany As String, ByVal strUserID As String, _
                ByVal strModule As String, ByVal strTable As String, _
                ByVal strLanguage As String, ByVal strFilterField As String, _
                ByVal strfiltercriteria As String, ByVal intPageSize As Integer, _
                ByVal intCurrentPage As Integer) As String
        Dim myTargetURL As String = ""
        Dim ReadIniFile As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "\Setup\Setup.ini")
        Dim strDebugMode As String = ReadIniFile.GetString("Setup", "DebugMode", "(none)")
        If strDebugMode Is Nothing Then
            strDebugMode = "FALSE"
        End If
        Dim booDebugMode As Boolean, strDomain As String, strPort As String
        'strDomain = ReadIniFile.GetString("Report", "Domain", "(none)")
        'strPort = ReadIniFile.GetString("Report", "Port", "(none)")
        strDomain = ""
        strPort = ""

        'If strDomain Is Nothing Or strDomain = "" Then
        '    strDomain = "hcrm.softfac.com"
        'End If
        'If strPort Is Nothing Or strPort = "" Then
        '    strPort = "8383"
        'End If
        booDebugMode = CType(strDebugMode, Boolean)
        'booDebugMode = False
        If booDebugMode = False Then
            If strPort = "" Then
                'myTargetURL = "http://" & strDomain & "/Reports/Default.aspx?" & _
                'myTargetURL = "http://" & strDomain & "/Reports.aspx?" & _
                'System.AppDomain.CurrentDomain.BaseDirectory
                myTargetURL = "../../Reports.aspx?" & _
                                "Report=" & strCompany & ".STANDARD." & strModule & "." & strTable & _
                                "&Company=" & strCompany & _
                                "&EmpID=" & strUserID & _
                                "&Table=" & strTable & _
                                "&Module=" & strModule & _
                                "&Language=" & strLanguage & _
                                "&FilterField=" & strFilterField & _
                                "&FilterCriteria=" & strfiltercriteria.Replace("&", "%26") & _
                                "&PageSize=" & intPageSize.ToString & _
                                "&CurrentPage=" & intCurrentPage.ToString & _
                                "&DebugMode=FALSE"
            Else
                'myTargetURL = "http://" & strDomain & ":" & strPort & "/Reports/Default.aspx?" & _
                'myTargetURL = "http://" & strDomain & ":" & strPort & "/Reports.aspx?" & _
                myTargetURL = "../../Reports.aspx?" & _
                                "Report=" & strCompany & ".STANDARD." & strModule & "." & strTable & _
                                "&Company=" & strCompany & _
                                "&EmpID=" & strUserID & _
                                "&Table=" & strTable & _
                                "&Module=" & strModule & _
                                "&Language=" & strLanguage & _
                                "&FilterField=" & strFilterField & _
                                "&FilterCriteria=" & strfiltercriteria.Replace("&", "%26") & _
                                "&PageSize=" & intPageSize.ToString & _
                                "&CurrentPage=" & intCurrentPage.ToString & _
                                "&DebugMode=FALSE"
            End If

        Else
            If strPort = "" Then
                'myTargetURL = "http://" & strDomain & "/Reports/Default.aspx?" & _
                'myTargetURL = "http://" & strDomain & "/Reports.aspx?" & _
                myTargetURL = "../../Reports.aspx?" & _
                    "Report=" & strCompany & ".STANDARD." & strModule & "." & strTable & _
                    "&Company=" & strCompany & _
                    "&EmpID=" & strUserID & _
                    "&Table=" & strTable & _
                    "&Module=" & strModule & _
                    "&Language=" & strLanguage & _
                    "&FilterField=" & strFilterField & _
                    "&FilterCriteria=" & strfiltercriteria.Replace("&", "%26") & _
                    "&PageSize=" & intPageSize.ToString & _
                    "&CurrentPage=" & intCurrentPage.ToString & _
                    "&DebugMode=TRUE"
            Else
                'myTargetURL = "http://" & strDomain & ":" & strPort & "/Reports/Default.aspx?" & _
                'myTargetURL = "http://" & strDomain & ":" & strPort & "/Reports.aspx?" & _
                myTargetURL = "../../Reports.aspx?" & _
                    "Report=" & strCompany & ".STANDARD." & strModule & "." & strTable & _
                    "&Company=" & strCompany & _
                    "&EmpID=" & strUserID & _
                    "&Table=" & strTable & _
                    "&Module=" & strModule & _
                    "&Language=" & strLanguage & _
                    "&FilterField=" & strFilterField & _
                    "&FilterCriteria=" & strfiltercriteria.Replace("&", "%26") & _
                    "&PageSize=" & intPageSize.ToString & _
                    "&CurrentPage=" & intCurrentPage.ToString & _
                    "&DebugMode=TRUE"
            End If

        End If
        Return myTargetURL
    End Function

    Function GetPDFReportURL() As String
        Dim myTargetURL As String = ""
        Dim ReadIniFile As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "\Setup\Setup.ini")
        Dim strDebugMode As String = ReadIniFile.GetString("Setup", "DebugMode", "(none)")
        If strDebugMode Is Nothing Then
            strDebugMode = "FALSE"
        End If
        Dim booDebugMode As Boolean, strDomain As String, strPort As String
        'strDomain = ReadIniFile.GetString("Report", "Domain", "(none)")
        'strPort = ReadIniFile.GetString("Report", "Port", "(none)")
        strDomain = ""
        strPort = ""

        booDebugMode = CType(strDebugMode, Boolean)

        If strPort = "" Then
            'myTargetURL = "http://" & strDomain & "/Reports/rdPage.aspx?"
            myTargetURL = "../../rdPage.aspx?"
        Else
            'myTargetURL = "http://" & strDomain & ":" & strPort & "/Reports/rdPage.aspx?"
            myTargetURL = "../../rdPage.aspx?"
        End If

        Return myTargetURL
    End Function
End Class
