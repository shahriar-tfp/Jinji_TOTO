Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class PAGES_ESCHEDULE_SCHEDULEAPPLICATION
    Inherits System.Web.UI.Page

    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting, MyKajima As New clsKajimaWeb
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")

    Dim mMyConnection As New System.Data.SqlClient.SqlConnection
    Dim mMyCommand As New System.Data.SqlClient.SqlCommand
    Dim mMyCommand2 As New System.Data.SqlClient.SqlCommand
    Dim mMyDataReader As System.Data.SqlClient.SqlDataReader
    Dim mMyDataReader2 As System.Data.SqlClient.SqlDataReader


    Dim mSmtpMail As New System.Net.Mail.SmtpClient
    Dim mSmtpMsg As New System.Net.Mail.MailMessage

    Dim mstrEmpID As String
    Dim mstrEmpName As String
    Dim miCount As Integer
    Dim LockDate As String
    Dim UnLockStart As String
    Dim UnLockEnd As String
    Dim ssql100 As String
    Dim myDS As DataSet


    Const mstrPlan As String = "Submit Monthly Plan"
    Const mstrApprove As String = "Submit Application"

    Dim mbApplied As Boolean
    Dim mbMonthActionAllow As Boolean = False

    Dim sysWeekDayEnabledBgColour As Drawing.Color = Drawing.Color.LightSteelBlue
    Dim sysWeekEndEnabledBgColour As Drawing.Color = Drawing.Color.IndianRed
    Dim sysOffDayEnabledBgColour As Drawing.Color = Drawing.Color.GreenYellow
    Dim sysHolidayEnabledBgColour As Drawing.Color = Drawing.Color.CadetBlue

    Dim sysTempForeColour As Drawing.Color = Drawing.Color.Black
    Dim sysTempBgColour As Drawing.Color = Drawing.Color.Yellow

    Dim sysDisabledBgColour As Drawing.Color = Nothing
    Dim sysDisabledForeColour As Drawing.Color = Nothing

    'Dim sysEnabledForeColour As Drawing.Color = Nothing
    Dim sysEnabledForeColour As Drawing.Color = Drawing.Color.White

    Dim miAllPending As Integer
    Dim mbEmpChanged As Boolean = False

    Public Sub FillDays()

        SetAllDayStatus(True)

        If cboLeave.SelectedValue = "C" Then
            SetAllDayStatus(False)
        ElseIf cboLeave.SelectedValue = "OTL" Then
            SetSingleDayStatus(btnDay1, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay2, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay3, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay4, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay5, False, sysDisabledForeColour, sysDisabledBgColour, False)

            SetSingleDayStatus(btnDay7, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay8, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay9, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay10, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay11, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay12, False, sysDisabledForeColour, sysDisabledBgColour, False)

            SetSingleDayStatus(btnDay14, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay15, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay16, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay17, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay18, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay19, False, sysDisabledForeColour, sysDisabledBgColour, False)

            SetSingleDayStatus(btnDay21, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay22, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay23, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay24, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay25, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay26, False, sysDisabledForeColour, sysDisabledBgColour, False)

            SetSingleDayStatus(btnDay28, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay29, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay30, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay31, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay32, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay33, False, sysDisabledForeColour, sysDisabledBgColour, False)

            SetSingleDayStatus(btnDay35, False, sysDisabledForeColour, sysDisabledBgColour, False)
        ElseIf cboLeave.SelectedValue = "OT" Then
            SetSingleDayStatus(btnDay1, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay2, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay3, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay4, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay5, False, sysDisabledForeColour, sysDisabledBgColour, False)


            SetSingleDayStatus(btnDay8, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay9, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay10, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay11, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay12, False, sysDisabledForeColour, sysDisabledBgColour, False)


            SetSingleDayStatus(btnDay15, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay16, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay17, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay18, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay19, False, sysDisabledForeColour, sysDisabledBgColour, False)


            SetSingleDayStatus(btnDay22, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay23, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay24, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay25, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay26, False, sysDisabledForeColour, sysDisabledBgColour, False)


            SetSingleDayStatus(btnDay29, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay30, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay31, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay32, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay33, False, sysDisabledForeColour, sysDisabledBgColour, False)


        ElseIf cboLeave.SelectedValue <> "BT" And cboLeave.SelectedValue <> "HL" And cboLeave.SelectedValue <> "CHL" And cboLeave.SelectedValue <> "OTA" And cboLeave.SelectedValue <> "HT" And cboLeave.SelectedValue <> "EHL" Then
            SetSingleDayStatus(btnDay6, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay7, False, sysDisabledForeColour, sysDisabledBgColour, False)

            SetSingleDayStatus(btnDay13, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay14, False, sysDisabledForeColour, sysDisabledBgColour, False)

            SetSingleDayStatus(btnDay20, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay21, False, sysDisabledForeColour, sysDisabledBgColour, False)

            SetSingleDayStatus(btnDay27, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay28, False, sysDisabledForeColour, sysDisabledBgColour, False)

            SetSingleDayStatus(btnDay34, False, sysDisabledForeColour, sysDisabledBgColour, False)
            SetSingleDayStatus(btnDay35, False, sysDisabledForeColour, sysDisabledBgColour, False)
        End If

        Dim iRunno As Integer
        Dim bDayActionNotAllow As Boolean

        mMyCommand.CommandText = "sp_web_ls_Sel_StaffLeave_temp '" & mstrEmpID & "'," & cboYear.SelectedValue & "," & cboMonth.SelectedValue & ",'" & _
                                "" & "','" & cboLeave.SelectedValue & "','" & "DAYS" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        While mMyDataReader.Read()
            iRunno = Val(mMyDataReader("runno"))
            bDayActionNotAllow = False

            If mMyDataReader("Days") <> "" Then
                If IsPassGraceDay(mMyDataReader("Days"), cboMonth.SelectedValue, cboYear.SelectedValue, mMyDataReader("allow")) Then
                    bDayActionNotAllow = True
                Else
                    mbMonthActionAllow = True
                End If
            End If

            Select Case mMyDataReader("runno")
                Case "1", "36"
                    If mMyDataReader("Days") = "" Then
                        btnDay1.Text = ""
                        btnDay1.Visible = False
                        lblPlanData1.Visible = False
                        lblApplyData1.Visible = False
                    Else
                        btnDay1.Text = mMyDataReader("Days")
                        btnDay1.Visible = True
                        lblPlanData1.Visible = True
                        lblApplyData1.Visible = True
                        lblDayType1.Value = mMyDataReader("DayType")
                    End If

                    If btnDay2.Enabled = True And lblDayType1.Value = "OD" Then SetSingleDayStatus(btnDay1, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay1, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "2", "37"
                    If mMyDataReader("Days") = "" Then
                        btnDay2.Text = ""
                        btnDay2.Visible = False
                        lblPlanData2.Visible = False
                        lblApplyData2.Visible = False

                    Else
                        btnDay2.Text = mMyDataReader("Days")
                        btnDay2.Visible = True
                        lblPlanData2.Visible = True
                        lblApplyData2.Visible = True
                        lblDayType2.Value = mMyDataReader("DayType")
                    End If

                    If btnDay2.Enabled = True And lblDayType2.Value = "OD" Then SetSingleDayStatus(btnDay2, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay2, False, sysDisabledForeColour, sysDisabledBgColour, False)


                Case "3", "38"
                    If mMyDataReader("Days") = "" Then
                        btnDay3.Text = ""
                        btnDay3.Visible = False
                        lblPlanData3.Visible = False
                        lblApplyData3.Visible = False
                    Else
                        btnDay3.Text = mMyDataReader("Days")
                        btnDay3.Visible = True
                        lblPlanData3.Visible = True
                        lblApplyData3.Visible = True
                        lblDayType3.Value = mMyDataReader("DayType")
                    End If

                    If btnDay3.Enabled = True And lblDayType3.Value = "OD" Then SetSingleDayStatus(btnDay3, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay3, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "4", "39"
                    If mMyDataReader("Days") = "" Then
                        btnDay4.Text = ""
                        btnDay4.Visible = False
                        lblPlanData4.Visible = False
                        lblApplyData4.Visible = False
                    Else
                        btnDay4.Text = mMyDataReader("Days")
                        btnDay4.Visible = True
                        lblPlanData4.Visible = True
                        lblApplyData4.Visible = True
                        lblDayType4.Value = mMyDataReader("DayType")
                    End If
                    If btnDay4.Enabled = True And lblDayType4.Value = "OD" Then SetSingleDayStatus(btnDay4, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay4, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "5", "40"
                    If mMyDataReader("Days") = "" Then
                        btnDay5.Text = ""
                        btnDay5.Visible = False
                        lblPlanData5.Visible = False
                        lblApplyData5.Visible = False
                    Else
                        btnDay5.Text = mMyDataReader("Days")
                        btnDay5.Visible = True
                        lblPlanData5.Visible = True
                        lblApplyData5.Visible = True
                        lblDayType5.Value = mMyDataReader("DayType")
                    End If
                    If btnDay5.Enabled = True And lblDayType5.Value = "OD" Then SetSingleDayStatus(btnDay5, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay5, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "6", "41"
                    If mMyDataReader("Days") = "" Then
                        btnDay6.Text = ""
                        btnDay6.Visible = False
                        lblPlanData6.Visible = False
                        lblApplyData6.Visible = False
                    Else
                        btnDay6.Text = mMyDataReader("Days")
                        btnDay6.Visible = True
                        lblPlanData6.Visible = True
                        lblApplyData6.Visible = True
                        lblDayType6.Value = mMyDataReader("DayType")
                    End If

                    If btnDay6.Enabled = True And lblDayType6.Value = "OD" Then SetSingleDayStatus(btnDay6, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay6, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "7", "42"
                    If mMyDataReader("Days") = "" Then
                        btnDay7.Text = ""
                        btnDay7.Visible = False
                        lblPlanData7.Visible = False
                        lblApplyData7.Visible = False
                    Else
                        btnDay7.Text = mMyDataReader("Days")
                        btnDay7.Visible = True
                        lblPlanData7.Visible = True
                        lblApplyData7.Visible = True
                        lblDayType7.Value = mMyDataReader("DayType")
                    End If

                    If btnDay7.Enabled = True And lblDayType7.Value = "OD" Then SetSingleDayStatus(btnDay7, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay7, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "8"
                    btnDay8.Text = mMyDataReader("Days")
                    lblDayType8.Value = mMyDataReader("DayType")
                    If btnDay8.Enabled = True And lblDayType8.Value = "OD" Then SetSingleDayStatus(btnDay8, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay8, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "9"
                    btnDay9.Text = mMyDataReader("Days")
                    lblDayType9.Value = mMyDataReader("DayType")
                    If btnDay9.Enabled = True And lblDayType9.Value = "OD" Then SetSingleDayStatus(btnDay9, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay9, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "10"
                    btnDay10.Text = mMyDataReader("Days")
                    lblDayType10.Value = mMyDataReader("DayType")
                    If btnDay10.Enabled = True And lblDayType10.Value = "OD" Then SetSingleDayStatus(btnDay10, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay10, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "11"
                    btnDay11.Text = mMyDataReader("Days")
                    lblDayType11.Value = mMyDataReader("DayType")
                    If btnDay11.Enabled = True And lblDayType11.Value = "OD" Then SetSingleDayStatus(btnDay11, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay11, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "12"
                    btnDay12.Text = mMyDataReader("Days")
                    lblDayType12.Value = mMyDataReader("DayType")
                    If btnDay12.Enabled = True And lblDayType12.Value = "OD" Then SetSingleDayStatus(btnDay12, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay12, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "13"
                    btnDay13.Text = mMyDataReader("Days")
                    lblDayType13.Value = mMyDataReader("DayType")
                    If btnDay13.Enabled = True And lblDayType13.Value = "OD" Then SetSingleDayStatus(btnDay13, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay13, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "14"
                    btnDay14.Text = mMyDataReader("Days")
                    lblDayType14.Value = mMyDataReader("DayType")
                    If btnDay14.Enabled = True And lblDayType14.Value = "OD" Then SetSingleDayStatus(btnDay14, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay14, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "15"
                    btnDay15.Text = mMyDataReader("Days")
                    lblDayType15.Value = mMyDataReader("DayType")
                    If btnDay15.Enabled = True And lblDayType15.Value = "OD" Then SetSingleDayStatus(btnDay15, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay15, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "16"
                    btnDay16.Text = mMyDataReader("Days")
                    lblDayType16.Value = mMyDataReader("DayType")
                    If btnDay16.Enabled = True And lblDayType16.Value = "OD" Then SetSingleDayStatus(btnDay16, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay16, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "17"
                    btnDay17.Text = mMyDataReader("Days")
                    lblDayType17.Value = mMyDataReader("DayType")
                    If btnDay17.Enabled = True And lblDayType17.Value = "OD" Then SetSingleDayStatus(btnDay17, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then
                        SetSingleDayStatus(btnDay17, False, sysDisabledForeColour, sysDisabledBgColour, False)
                    End If
                Case "18"
                    btnDay18.Text = mMyDataReader("Days")
                    lblDayType18.Value = mMyDataReader("DayType")
                    If btnDay18.Enabled = True And lblDayType18.Value = "OD" Then SetSingleDayStatus(btnDay18, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay18, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "19"
                    btnDay19.Text = mMyDataReader("Days")
                    lblDayType19.Value = mMyDataReader("DayType")
                    If btnDay19.Enabled = True And lblDayType19.Value = "OD" Then SetSingleDayStatus(btnDay19, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then
                        SetSingleDayStatus(btnDay19, False, sysDisabledForeColour, sysDisabledBgColour, False)
                    End If
                Case "20"
                    btnDay20.Text = mMyDataReader("Days")
                    lblDayType20.Value = mMyDataReader("DayType")
                    If btnDay20.Enabled = True And lblDayType20.Value = "OD" Then SetSingleDayStatus(btnDay20, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay20, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "21"
                    btnDay21.Text = mMyDataReader("Days")
                    lblDayType21.Value = mMyDataReader("DayType")
                    If btnDay21.Enabled = True And lblDayType21.Value = "OD" Then SetSingleDayStatus(btnDay21, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay21, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "22"
                    btnDay22.Text = mMyDataReader("Days")
                    lblDayType22.Value = mMyDataReader("DayType")
                    If btnDay22.Enabled = True And lblDayType22.Value = "OD" Then SetSingleDayStatus(btnDay22, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay22, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "23"
                    btnDay23.Text = mMyDataReader("Days")
                    lblDayType23.Value = mMyDataReader("DayType")
                    If btnDay23.Enabled = True And lblDayType23.Value = "OD" Then SetSingleDayStatus(btnDay23, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then
                        SetSingleDayStatus(btnDay23, False, sysDisabledForeColour, sysDisabledBgColour, False)
                    End If
                Case "24"
                    btnDay24.Text = mMyDataReader("Days")
                    lblDayType24.Value = mMyDataReader("DayType")
                    If btnDay24.Enabled = True And lblDayType24.Value = "OD" Then SetSingleDayStatus(btnDay24, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay24, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "25"
                    btnDay25.Text = mMyDataReader("Days")
                    lblDayType25.Value = mMyDataReader("DayType")
                    If btnDay25.Enabled = True And lblDayType25.Value = "OD" Then SetSingleDayStatus(btnDay25, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay25, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "26"
                    btnDay26.Text = mMyDataReader("Days")
                    lblDayType26.Value = mMyDataReader("DayType")
                    If btnDay26.Enabled = True And lblDayType26.Value = "OD" Then SetSingleDayStatus(btnDay26, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay26, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "27"
                    btnDay27.Text = mMyDataReader("Days")
                    lblDayType27.Value = mMyDataReader("DayType")
                    If btnDay27.Enabled = True And lblDayType27.Value = "OD" Then SetSingleDayStatus(btnDay27, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay27, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "28"
                    btnDay28.Text = mMyDataReader("Days")
                    lblDayType28.Value = mMyDataReader("DayType")
                    If btnDay28.Enabled = True And lblDayType28.Value = "OD" Then SetSingleDayStatus(btnDay28, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay28, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "29"
                    If mMyDataReader("Days") = "" Then
                        btnDay29.Text = ""
                        btnDay29.Visible = False
                        lblPlanData29.Visible = False
                        lblApplyData29.Visible = False
                    Else
                        btnDay29.Text = mMyDataReader("Days")
                        btnDay29.Visible = True
                        lblPlanData29.Visible = True
                        lblApplyData29.Visible = True
                    End If
                    lblDayType29.Value = mMyDataReader("DayType")
                    If btnDay29.Enabled = True And lblDayType29.Value = "OD" Then SetSingleDayStatus(btnDay29, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay29, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "30"
                    If mMyDataReader("Days") = "" Then
                        btnDay30.Text = ""
                        btnDay30.Visible = False
                        lblPlanData30.Visible = False
                        lblApplyData30.Visible = False
                    Else
                        btnDay30.Text = mMyDataReader("Days")
                        btnDay30.Visible = True
                        lblPlanData30.Visible = True
                        lblApplyData30.Visible = True
                    End If
                    lblDayType30.Value = mMyDataReader("DayType")
                    If btnDay30.Enabled = True And lblDayType30.Value = "OD" Then SetSingleDayStatus(btnDay30, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay30, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "31"
                    If mMyDataReader("Days") = "" Then
                        btnDay31.Text = ""
                        btnDay31.Visible = False
                        lblPlanData31.Visible = False
                        lblApplyData31.Visible = False
                    Else
                        btnDay31.Text = mMyDataReader("Days")
                        btnDay31.Visible = True
                        lblPlanData31.Visible = True
                        lblApplyData31.Visible = True
                    End If
                    lblDayType31.Value = mMyDataReader("DayType")
                    If btnDay31.Enabled = True And lblDayType31.Value = "OD" Then SetSingleDayStatus(btnDay31, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay31, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "32"
                    If mMyDataReader("Days") = "" Then
                        btnDay32.Text = ""
                        btnDay32.Visible = False
                        lblPlanData32.Visible = False
                        lblApplyData32.Visible = False
                    Else
                        btnDay32.Text = mMyDataReader("Days")
                        btnDay32.Visible = True
                        lblPlanData32.Visible = True
                        lblApplyData32.Visible = True
                    End If
                    lblDayType32.Value = mMyDataReader("DayType")
                    If btnDay32.Enabled = True And lblDayType32.Value = "OD" Then SetSingleDayStatus(btnDay32, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay32, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "33"
                    If mMyDataReader("Days") = "" Then
                        btnDay33.Text = ""
                        btnDay33.Visible = False
                        lblPlanData33.Visible = False
                        lblApplyData33.Visible = False
                    Else
                        btnDay33.Text = mMyDataReader("Days")
                        btnDay33.Visible = True
                        lblPlanData33.Visible = True
                        lblApplyData33.Visible = True
                    End If
                    lblDayType33.Value = mMyDataReader("DayType")
                    If btnDay33.Enabled = True And lblDayType33.Value = "OD" Then SetSingleDayStatus(btnDay33, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay33, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "34"
                    If mMyDataReader("Days") = "" Then
                        btnDay34.Text = ""
                        btnDay34.Visible = False
                        lblPlanData34.Visible = False
                        lblApplyData34.Visible = False
                    Else
                        btnDay34.Text = mMyDataReader("Days")
                        btnDay34.Visible = True
                        lblPlanData34.Visible = True
                        lblApplyData34.Visible = True
                    End If
                    lblDayType34.Value = mMyDataReader("DayType")
                    If btnDay34.Enabled = True And lblDayType34.Value = "OD" Then SetSingleDayStatus(btnDay34, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay34, False, sysDisabledForeColour, sysDisabledBgColour, False)
                Case "35"
                    If mMyDataReader("Days") = "" Then
                        btnDay35.Text = ""
                        btnDay35.Visible = False
                        lblPlanData35.Visible = False
                        lblApplyData35.Visible = False
                    Else
                        btnDay35.Text = mMyDataReader("Days")
                        btnDay35.Visible = True
                        lblPlanData35.Visible = True
                        lblApplyData35.Visible = True
                    End If
                    lblDayType35.Value = mMyDataReader("DayType")
                    If btnDay35.Enabled = True And lblDayType35.Value = "OD" Then SetSingleDayStatus(btnDay35, True, sysEnabledForeColour, sysOffDayEnabledBgColour, True)
                    If bDayActionNotAllow Then SetSingleDayStatus(btnDay35, False, sysDisabledForeColour, sysDisabledBgColour, False)
            End Select
        End While

        mMyDataReader.Close()

        SetButtonInvisible(iRunno)

        If lblBal.Text <> "-" Then
            If (cboLeave.SelectedValue = "AL" Or cboLeave.SelectedValue = "CAL" Or cboLeave.SelectedValue = "EAL" Or cboLeave.SelectedValue = "CML" Or cboLeave.SelectedValue = "OF" Or cboLeave.SelectedValue = "MC" Or cboLeave.SelectedValue <> "EHL" Or cboLeave.SelectedValue <> "HL" Or cboLeave.SelectedValue <> "CHL" Or cboLeave.SelectedValue <> "WL" Or cboLeave.SelectedValue <> "CL") Then
                If Val(lblBal.Text) = 0 Then
                    SetAllDayStatus(False)
                ElseIf Val(lblBal.Text) = 0.5 And cboPeriod.SelectedValue = "F" Then
                    SetAllDayStatus(False)
                End If
            End If
        End If

    End Sub
    Private Function IsPassGraceDay(ByVal strDay As String, ByVal strMonth As String, ByVal strYear As String, ByVal Allow As Boolean) As Boolean

        IsPassGraceDay = False

        If strDay = "" Then Exit Function

        If IsDate("13" & "/" & strMonth & "/" & strYear) Then
            If CDate(strDay.ToString & "/" & strMonth & "/" & strYear) < CDate(LockDate) Or (CDate(strDay.ToString & "/" & strMonth & "/" & strYear) >= CDate(UnLockStart) And CDate(strDay.ToString & "/" & strMonth & "/" & strYear) <= CDate(UnLockEnd)) Then
                IsPassGraceDay = True
            End If
        Else
            If CDate(strMonth & "/" & strDay.ToString & "/" & strYear) < CDate(LockDate) Or (CDate(strMonth & "/" & strDay.ToString & "/" & strYear) >= CDate(UnLockStart) And CDate(strMonth & "/" & strDay.ToString & "/" & strYear) <= CDate(UnLockEnd)) Then
                IsPassGraceDay = True
            End If
        End If

        If Allow = True Then
            IsPassGraceDay = False
        End If

    End Function
    Private Sub CheckPublicHoliday()

        Dim sysForeColor As Drawing.Color
        Dim sysBgColor As Drawing.Color
        Dim bEnabled As Boolean

        If cboLeave.SelectedValue = "OTA" Or cboLeave.SelectedValue = "BT" Or cboLeave.SelectedValue = "HL" Or cboLeave.SelectedValue = "CHL" Or cboLeave.SelectedValue = "OT" Or cboLeave.SelectedValue = "HT" Or cboLeave.SelectedValue = "EHL" Then
            sysForeColor = sysEnabledForeColour
            sysBgColor = sysHolidayEnabledBgColour
            bEnabled = True
        Else
            sysForeColor = sysDisabledForeColour
            sysBgColor = sysDisabledBgColour
            bEnabled = False
        End If

        If bIsHoliday(btnDay1) And Not IsPassGraceDay(btnDay1.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
          SetSingleDayStatus(btnDay1, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay2) And Not IsPassGraceDay(btnDay2.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay2, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay3) And Not IsPassGraceDay(btnDay3.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay3, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay4) And Not IsPassGraceDay(btnDay4.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay4, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay5) And Not IsPassGraceDay(btnDay5.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay5, bEnabled, sysForeColor, sysBgColor, bEnabled)

        If bIsHoliday(btnDay8) And Not IsPassGraceDay(btnDay8.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
          SetSingleDayStatus(btnDay8, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay9) And Not IsPassGraceDay(btnDay9.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay9, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay10) And Not IsPassGraceDay(btnDay10.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay10, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay11) And Not IsPassGraceDay(btnDay11.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay11, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay12) And Not IsPassGraceDay(btnDay12.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay12, bEnabled, sysForeColor, sysBgColor, bEnabled)

        If bIsHoliday(btnDay15) And Not IsPassGraceDay(btnDay15.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay15, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay16) And Not IsPassGraceDay(btnDay16.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay16, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay17) And Not IsPassGraceDay(btnDay17.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay17, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay18) And Not IsPassGraceDay(btnDay18.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay18, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay19) And Not IsPassGraceDay(btnDay19.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay19, bEnabled, sysForeColor, sysBgColor, bEnabled)

        If bIsHoliday(btnDay22) And Not IsPassGraceDay(btnDay22.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
           SetSingleDayStatus(btnDay22, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay23) And Not IsPassGraceDay(btnDay23.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
           SetSingleDayStatus(btnDay23, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay24) And Not IsPassGraceDay(btnDay24.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay24, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay25) And Not IsPassGraceDay(btnDay25.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay25, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay26) And Not IsPassGraceDay(btnDay26.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay26, bEnabled, sysForeColor, sysBgColor, bEnabled)

        If bIsHoliday(btnDay29) And Not IsPassGraceDay(btnDay29.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay29, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay30) And Not IsPassGraceDay(btnDay30.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay30, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay31) And Not IsPassGraceDay(btnDay31.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay31, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay32) And Not IsPassGraceDay(btnDay32.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay32, bEnabled, sysForeColor, sysBgColor, bEnabled)
        If bIsHoliday(btnDay33) And Not IsPassGraceDay(btnDay33.Text, cboMonth.SelectedValue, cboYear.SelectedValue, 0) Then _
            SetSingleDayStatus(btnDay33, bEnabled, sysForeColor, sysBgColor, bEnabled)



    End Sub
    Private Function bIsWeekend(ByRef btnday As Button) As Boolean

        Select Case btnday.ID
            Case btnDay6.ID, btnDay7.ID, btnDay13.ID, btnDay14.ID, btnDay20.ID, btnDay21.ID, btnDay27.ID, btnDay28.ID, btnDay34.ID, btnDay35.ID
                bIsWeekend = True
            Case Else
                bIsWeekend = False
        End Select

    End Function
    Private Function bIsHoliday(ByRef btnDay As Button) As Boolean

        Dim iLoop As Integer

        If btnDay.Text <> "" And btnDay.Visible Then
            If IsDate("13" & "/" & cboMonth.SelectedValue.ToString & "/" & cboYear.SelectedValue.ToString) Then
                For iLoop = 0 To lstHoliday.Items.Count - 1

                    If CDate(btnDay.Text.ToString & "/" & cboMonth.SelectedValue.ToString & "/" & cboYear.SelectedValue.ToString) = CDate(lstHoliday.Items(iLoop).Value) Then
                        bIsHoliday = True
                        Exit For
                    Else
                        bIsHoliday = False
                    End If
                Next
            Else
                For iLoop = 0 To lstHoliday.Items.Count - 1

                    If CDate(cboMonth.SelectedValue.ToString & "/" & btnDay.Text.ToString & "/" & cboYear.SelectedValue.ToString) = CDate(lstHoliday.Items(iLoop).Value.Substring(3, 2) & "/" & lstHoliday.Items(iLoop).Value.Substring(0, 2) & "/" & lstHoliday.Items(iLoop).Value.Substring(6)) Then
                        bIsHoliday = True
                        Exit For
                    Else
                        bIsHoliday = False
                    End If
                Next

            End If
        End If

    End Function
    Private Sub SetSingleDayStatus(ByRef btnDay As Button, ByVal bEnabled As Boolean, ByRef sysForeColor As Drawing.Color, ByVal sysBackColor As Drawing.Color, ByVal bFontBold As Boolean)

        btnDay.Enabled = bEnabled
        btnDay.ForeColor = sysForeColor
        btnDay.BackColor = sysBackColor
        btnDay.Font.Bold = bFontBold

    End Sub
    Private Sub SetAllDayStatus(ByVal bEnable As Boolean)

        Dim sysWeekDayBgColour As Drawing.Color
        Dim sysWeekEndBgColour As Drawing.Color
        Dim sysOffDayBgColour As Drawing.Color
        Dim sysForeColour As Drawing.Color

        If bEnable Then
            sysWeekDayBgColour = sysWeekDayEnabledBgColour
            sysWeekEndBgColour = sysWeekEndEnabledBgColour
            sysOffDayBgColour = sysOffDayEnabledBgColour

            sysForeColour = sysEnabledForeColour
        Else
            sysWeekDayBgColour = sysDisabledBgColour
            sysWeekEndBgColour = sysDisabledBgColour
            sysOffDayBgColour = sysDisabledBgColour
            sysForeColour = sysDisabledForeColour
        End If

        btnDay1.Enabled = bEnable
        btnDay2.Enabled = bEnable
        btnDay3.Enabled = bEnable
        btnDay4.Enabled = bEnable
        btnDay5.Enabled = bEnable
        btnDay6.Enabled = bEnable
        btnDay7.Enabled = bEnable
        btnDay8.Enabled = bEnable
        btnDay9.Enabled = bEnable
        btnDay10.Enabled = bEnable
        btnDay11.Enabled = bEnable
        btnDay12.Enabled = bEnable
        btnDay13.Enabled = bEnable
        btnDay14.Enabled = bEnable
        btnDay15.Enabled = bEnable
        btnDay16.Enabled = bEnable
        btnDay17.Enabled = bEnable
        btnDay18.Enabled = bEnable
        btnDay19.Enabled = bEnable
        btnDay20.Enabled = bEnable
        btnDay21.Enabled = bEnable
        btnDay22.Enabled = bEnable
        btnDay23.Enabled = bEnable
        btnDay24.Enabled = bEnable
        btnDay25.Enabled = bEnable
        btnDay26.Enabled = bEnable
        btnDay27.Enabled = bEnable
        btnDay28.Enabled = bEnable
        btnDay29.Enabled = bEnable
        btnDay30.Enabled = bEnable
        btnDay31.Enabled = bEnable
        btnDay32.Enabled = bEnable
        btnDay33.Enabled = bEnable
        btnDay34.Enabled = bEnable
        btnDay35.Enabled = bEnable

        btnDay1.BackColor = sysWeekDayBgColour
        btnDay2.BackColor = sysWeekDayBgColour
        btnDay3.BackColor = sysWeekDayBgColour
        btnDay4.BackColor = sysWeekDayBgColour
        btnDay5.BackColor = sysWeekDayBgColour
        btnDay6.BackColor = sysWeekEndBgColour
        btnDay7.BackColor = sysWeekEndBgColour

        btnDay8.BackColor = sysWeekDayBgColour
        btnDay9.BackColor = sysWeekDayBgColour
        btnDay10.BackColor = sysWeekDayBgColour
        btnDay11.BackColor = sysWeekDayBgColour
        btnDay12.BackColor = sysWeekDayBgColour
        btnDay13.BackColor = sysWeekEndBgColour
        btnDay14.BackColor = sysWeekEndBgColour

        btnDay15.BackColor = sysWeekDayBgColour
        btnDay16.BackColor = sysWeekDayBgColour
        btnDay17.BackColor = sysWeekDayBgColour
        btnDay18.BackColor = sysWeekDayBgColour
        btnDay19.BackColor = sysWeekDayBgColour
        btnDay20.BackColor = sysWeekEndBgColour
        btnDay21.BackColor = sysWeekEndBgColour

        btnDay22.BackColor = sysWeekDayBgColour
        btnDay23.BackColor = sysWeekDayBgColour
        btnDay24.BackColor = sysWeekDayBgColour
        btnDay25.BackColor = sysWeekDayBgColour
        btnDay26.BackColor = sysWeekDayBgColour
        btnDay27.BackColor = sysWeekEndBgColour
        btnDay28.BackColor = sysWeekEndBgColour

        btnDay29.BackColor = sysWeekDayBgColour
        btnDay30.BackColor = sysWeekDayBgColour
        btnDay31.BackColor = sysWeekDayBgColour
        btnDay32.BackColor = sysWeekDayBgColour
        btnDay33.BackColor = sysWeekDayBgColour
        btnDay34.BackColor = sysWeekEndBgColour
        btnDay35.BackColor = sysWeekEndBgColour

        btnDay1.ForeColor = sysForeColour
        btnDay2.ForeColor = sysForeColour
        btnDay3.ForeColor = sysForeColour
        btnDay4.ForeColor = sysForeColour
        btnDay5.ForeColor = sysForeColour
        btnDay6.ForeColor = sysForeColour
        btnDay7.ForeColor = sysForeColour
        btnDay8.ForeColor = sysForeColour
        btnDay9.ForeColor = sysForeColour
        btnDay10.ForeColor = sysForeColour
        btnDay11.ForeColor = sysForeColour
        btnDay12.ForeColor = sysForeColour
        btnDay13.ForeColor = sysForeColour
        btnDay14.ForeColor = sysForeColour
        btnDay15.ForeColor = sysForeColour
        btnDay16.ForeColor = sysForeColour
        btnDay17.ForeColor = sysForeColour
        btnDay18.ForeColor = sysForeColour
        btnDay19.ForeColor = sysForeColour
        btnDay20.ForeColor = sysForeColour
        btnDay21.ForeColor = sysForeColour
        btnDay22.ForeColor = sysForeColour
        btnDay23.ForeColor = sysForeColour
        btnDay24.ForeColor = sysForeColour
        btnDay25.ForeColor = sysForeColour
        btnDay26.ForeColor = sysForeColour
        btnDay27.ForeColor = sysForeColour
        btnDay28.ForeColor = sysForeColour
        btnDay29.ForeColor = sysForeColour
        btnDay30.ForeColor = sysForeColour
        btnDay31.ForeColor = sysForeColour
        btnDay32.ForeColor = sysForeColour
        btnDay33.ForeColor = sysForeColour
        btnDay34.ForeColor = sysForeColour
        btnDay35.ForeColor = sysForeColour

        btnDay1.Font.Bold = bEnable
        btnDay2.Font.Bold = bEnable
        btnDay3.Font.Bold = bEnable
        btnDay4.Font.Bold = bEnable
        btnDay5.Font.Bold = bEnable
        btnDay6.Font.Bold = bEnable
        btnDay7.Font.Bold = bEnable
        btnDay8.Font.Bold = bEnable
        btnDay9.Font.Bold = bEnable
        btnDay10.Font.Bold = bEnable
        btnDay11.Font.Bold = bEnable
        btnDay12.Font.Bold = bEnable
        btnDay13.Font.Bold = bEnable
        btnDay14.Font.Bold = bEnable
        btnDay15.Font.Bold = bEnable
        btnDay16.Font.Bold = bEnable
        btnDay17.Font.Bold = bEnable
        btnDay18.Font.Bold = bEnable
        btnDay19.Font.Bold = bEnable
        btnDay20.Font.Bold = bEnable
        btnDay21.Font.Bold = bEnable
        btnDay22.Font.Bold = bEnable
        btnDay23.Font.Bold = bEnable
        btnDay24.Font.Bold = bEnable
        btnDay25.Font.Bold = bEnable
        btnDay26.Font.Bold = bEnable
        btnDay27.Font.Bold = bEnable
        btnDay28.Font.Bold = bEnable
        btnDay29.Font.Bold = bEnable
        btnDay30.Font.Bold = bEnable
        btnDay31.Font.Bold = bEnable
        btnDay32.Font.Bold = bEnable
        btnDay33.Font.Bold = bEnable
        btnDay34.Font.Bold = bEnable
        btnDay35.Font.Bold = bEnable

    End Sub
    Private Sub SetButtonInvisible(ByVal iRunno As Integer)

        If iRunno < 29 Then
            btnDay29.Text = ""
            btnDay29.Visible = False
            lblPlanData29.Visible = False
            lblApplyData29.Visible = False
        End If

        If iRunno < 30 Then
            btnDay30.Text = ""
            btnDay30.Visible = False
            lblPlanData30.Visible = False
            lblApplyData30.Visible = False
        End If

        If iRunno < 31 Then
            btnDay31.Text = ""
            btnDay31.Visible = False
            lblPlanData31.Visible = False
            lblApplyData31.Visible = False
        End If

        If iRunno < 32 Then
            btnDay32.Text = ""
            btnDay32.Visible = False
            lblPlanData32.Visible = False
            lblApplyData32.Visible = False
        End If

        If iRunno < 33 Then
            btnDay33.Text = ""
            btnDay33.Visible = False
            lblPlanData33.Visible = False
            lblApplyData33.Visible = False
        End If

        If iRunno < 34 Then
            btnDay34.Text = ""
            btnDay34.Visible = False
            lblPlanData34.Visible = False
            lblApplyData34.Visible = False
        End If

        If iRunno < 35 Then
            btnDay35.Text = ""
            btnDay35.Visible = False
            lblPlanData35.Visible = False
            lblApplyData35.Visible = False
        End If

    End Sub

    Public Sub FillDetail()

        Dim strLeave As String = ""
        Dim strToolTip As String = ""
        Dim sysPlanColor As Drawing.Color
        Dim sysApplyColor As Drawing.Color
        Dim bEnabled As Boolean = False
        Dim Holiday As Boolean = False


        mMyCommand.CommandText = "sp_web_ls_Sel_StaffLeave '" & mstrEmpID & "'," & cboYear.SelectedValue & "," & cboMonth.SelectedValue & ",'" & _
                                "" & "','" & cboLeave.SelectedValue & "','" & "DETAIL" & "'"
        mMyDataReader = mMyCommand.ExecuteReader



        While mMyDataReader.Read()
            strLeave = ""

            Select Case mMyDataReader("runno")
                Case "1", "36"
                    Holiday = bIsHoliday(btnDay1)
                Case "2", "37"
                    Holiday = bIsHoliday(btnDay2)
                Case "3", "38"
                    Holiday = bIsHoliday(btnDay3)
                Case "4", "39"
                    Holiday = bIsHoliday(btnDay4)
                Case "5", "40"
                    Holiday = bIsHoliday(btnDay5)
                Case "6", "41"
                    Holiday = bIsHoliday(btnDay6)
                Case "7", "42"
                    Holiday = bIsHoliday(btnDay7)
                Case "8"
                    Holiday = bIsHoliday(btnDay8)
                Case "9"
                    Holiday = bIsHoliday(btnDay9)
                Case "10"
                    Holiday = bIsHoliday(btnDay10)
                Case "11"
                    Holiday = bIsHoliday(btnDay11)
                Case "12"
                    Holiday = bIsHoliday(btnDay12)
                Case "13"
                    Holiday = bIsHoliday(btnDay13)
                Case "14"
                    Holiday = bIsHoliday(btnDay14)
                Case "15"
                    Holiday = bIsHoliday(btnDay15)
                Case "16"
                    Holiday = bIsHoliday(btnDay16)
                Case "17"
                    Holiday = bIsHoliday(btnDay17)
                Case "18"
                    Holiday = bIsHoliday(btnDay18)
                Case "19"
                    Holiday = bIsHoliday(btnDay19)
                Case "20"
                    Holiday = bIsHoliday(btnDay20)
                Case "21"
                    Holiday = bIsHoliday(btnDay21)
                Case "22"
                    Holiday = bIsHoliday(btnDay22)
                Case "23"
                    Holiday = bIsHoliday(btnDay23)
                Case "24"
                    Holiday = bIsHoliday(btnDay24)
                Case "25"
                    Holiday = bIsHoliday(btnDay25)
                Case "26"
                    Holiday = bIsHoliday(btnDay26)
                Case "27"
                    Holiday = bIsHoliday(btnDay27)
                Case "28"
                    Holiday = bIsHoliday(btnDay28)
                Case "29"
                    Holiday = bIsHoliday(btnDay29)
                Case "30"
                    Holiday = bIsHoliday(btnDay30)
                Case "31"
                    Holiday = bIsHoliday(btnDay31)
                Case "32"
                    Holiday = bIsHoliday(btnDay32)
                Case "33"
                    Holiday = bIsHoliday(btnDay33)
                Case "34"
                    Holiday = bIsHoliday(btnDay34)
                Case "35"
                    Holiday = bIsHoliday(btnDay35)
            End Select

            If mMyDataReader("leaveid_f") = "" And mMyDataReader("leaveid_1") = "" And mMyDataReader("leaveid_2") = "" Then
                strToolTip = ""
                sysPlanColor = Nothing
                sysApplyColor = Nothing
            Else
                If mMyDataReader("Type") = "A" Then
                    mbApplied = True

                    If mMyDataReader("Status_F") <> "" Then
                        strToolTip = mMyDataReader("Status_F")
                    ElseIf mMyDataReader("Status_1") <> "" And mMyDataReader("Status_2") = "" Then
                        strToolTip = mMyDataReader("Status_1")
                    ElseIf mMyDataReader("Status_1") <> "" And mMyDataReader("Status_2") <> "" Then
                        strToolTip = "1st Half: " & mMyDataReader("Status_1") & vbCrLf & "2nd Half: " & mMyDataReader("Status_2")
                    ElseIf mMyDataReader("Status_1") = "" And mMyDataReader("Status_2") <> "" Then
                        strToolTip = mMyDataReader("Status_2")
                    End If
                Else
                    strToolTip = ""
                End If

                If mMyDataReader("LeaveID_F") <> "" Then
                    If mMyDataReader("LeaveID_F") <> "OTL" And mMyDataReader("LeaveID_F") <> "OTA" Then
                        strLeave = mMyDataReader("LeaveID_F") & "-F"
                    Else
                        strLeave = mMyDataReader("LeaveID_F") & ""
                    End If
                End If

                If mMyDataReader("LeaveID_1") <> "" Then
                    If strLeave <> "" Then strLeave = strLeave & " | "

                    strLeave = strLeave & mMyDataReader("LeaveID_1") & "-1"
                End If

                If mMyDataReader("LeaveID_2") <> "" Then
                    If strLeave <> "" Then strLeave = strLeave & " | "

                    strLeave = strLeave & mMyDataReader("LeaveID_2") & "-2"
                End If

                If mMyDataReader("Type") = cboType.SelectedValue Then
                    If cboPeriod.SelectedValue = "F" Then
                        If mMyDataReader("Type") = "P" And mMyDataReader("Leaveid_F") <> "" And mMyDataReader("Leaveid_F") <> "OTL" And mMyDataReader("Leaveid_F") <> "OTA" And cboLeave.SelectedValue = "C" And mMyDataReader("Status_F").ToString.ToUpper = "PENDING" Then
                            bEnabled = True
                        Else
                            If mMyDataReader("Leaveid_F") <> "" And mMyDataReader("LeaveID_F") <> "OTA" And mMyDataReader("LeaveID_F") <> "OTL" And cboLeave.SelectedValue = "C" And mMyDataReader("Status_F").ToString.ToUpper = "PENDING" Then
                                bEnabled = True
                            ElseIf (mMyDataReader("Leaveid_1") <> "" Or mMyDataReader("Leaveid_2") <> "") And cboLeave.SelectedValue <> "C" Then
                                bEnabled = False
                            ElseIf mMyDataReader("Leaveid_1") = "" And mMyDataReader("Leaveid_2") = "" And (mMyDataReader("Leaveid_F") = "HT" Or mMyDataReader("Leaveid_F") = "EHL") And (mMyDataReader("Leaveid_HT") = "HT" Or mMyDataReader("Leaveid_HT") = "EHL") And mMyDataReader("LeaveID_OT") = "" And mMyDataReader("LeaveID_BT") = "" And cboLeave.SelectedValue <> "C" And (cboLeave.SelectedValue = "AL" Or cboLeave.SelectedValue = "EAL" Or cboLeave.SelectedValue = "CAL") And mMyDataReader("Status_F").ToString.ToUpper = "PENDING" Then
                                If mMyDataReader("runno") Mod 7 = 0 Or mMyDataReader("runno") Mod 7 = 6 Then
                                    bEnabled = False
                                Else
                                    bEnabled = True
                                End If
                            ElseIf (mMyDataReader("Leaveid_F") = "AL" Or mMyDataReader("Leaveid_F") = "EAL" Or mMyDataReader("Leaveid_F") = "CAL") And mMyDataReader("Leaveid_HT") = "" And mMyDataReader("LeaveID_OT") = "" And mMyDataReader("LeaveID_BT") = "" And cboLeave.SelectedValue <> "C" And (cboLeave.SelectedValue = "HT" Or cboLeave.SelectedValue = "EHL") And cboLeave.SelectedValue <> "OT" And cboLeave.SelectedValue <> "BT" Then
                                bEnabled = True
                            ElseIf mMyDataReader("Leaveid_F") = "BT" And mMyDataReader("Leaveid_HT") = "" And mMyDataReader("LeaveID_OT") = "" And mMyDataReader("LeaveID_BT") = "BT" And cboLeave.SelectedValue <> "C" And (cboLeave.SelectedValue = "MC" Or cboLeave.SelectedValue = "OTA" Or cboLeave.SelectedValue = "OTL") And mMyDataReader("Status_F").ToString.ToUpper = "PENDING" Then
                                If mMyDataReader("runno") Mod 7 = 0 Or mMyDataReader("runno") Mod 7 = 6 Or Holiday Then
                                    If cboLeave.SelectedValue = "OTL" And (mMyDataReader("runno") Mod 7 = 6 Or Holiday) Then
                                        bEnabled = True
                                    ElseIf cboLeave.SelectedValue = "OTA" And (mMyDataReader("runno") Mod 7 = 0 Or Holiday) Then
                                        bEnabled = True
                                    Else
                                        bEnabled = False
                                    End If
                                Else
                                    bEnabled = True
                                End If
                            ElseIf mMyDataReader("Leaveid_HT") = "" And mMyDataReader("LeaveID_OT") = "" And mMyDataReader("LeaveID_BT") = "" And cboLeave.SelectedValue <> "C" And cboLeave.SelectedValue <> "HT" And cboLeave.SelectedValue <> "OT" And cboLeave.SelectedValue = "BT" And mMyDataReader("Leaveid_F") = "MC" Then
                                bEnabled = True
                            ElseIf mMyDataReader("Leaveid_F") Like "*OT*" And mMyDataReader("Leaveid_HT") = "" And mMyDataReader("LeaveID_OT") Like "*OT*" And mMyDataReader("LeaveID_BT") = "" And cboLeave.SelectedValue = "BT" And cboLeave.SelectedValue <> "HT" And mMyDataReader("Status_F").ToString.ToUpper = "PENDING" Then
                                bEnabled = True
                            Else
                                bEnabled = False
                            End If
                        End If
                    ElseIf cboPeriod.SelectedValue = "1" Then
                        If mMyDataReader("Type") = "P" And mMyDataReader("Leaveid_1") <> "" And cboLeave.SelectedValue = "C" And mMyDataReader("Status_1").ToString.ToUpper = "PENDING" Then
                            bEnabled = True
                        ElseIf mMyDataReader("Leaveid_1") = "" And mMyDataReader("Leaveid_f") = "" And cboLeave.SelectedValue <> "C" And (Val(lblBal.Text) >= 0.5 Or lblBal.Text = "-") Then
                            bEnabled = True
                        Else
                            If mMyDataReader("Leaveid_1") <> "" And cboLeave.SelectedValue = "C" And mMyDataReader("Status_1").ToString.ToUpper = "PENDING" Then
                                bEnabled = True
                            ElseIf (mMyDataReader("Leaveid_1") = "AL" Or mMyDataReader("Leaveid_1") = "EAL" Or mMyDataReader("Leaveid_1") = "CAL") And (mMyDataReader("Leaveid_HT") = "") And (cboLeave.SelectedValue = "HT" Or cboLeave.SelectedValue = "EHL") Then
                                bEnabled = True
                            ElseIf (mMyDataReader("Leaveid_1") = "HT" Or mMyDataReader("Leaveid_1") = "EHL") And (mMyDataReader("Leaveid_HT") <> "") And (cboLeave.SelectedValue = "AL" Or cboLeave.SelectedValue = "EAL" Or cboLeave.SelectedValue = "CAL") Then
                                bEnabled = True
                            ElseIf mMyDataReader("Leaveid_1") = "" And mMyDataReader("Leaveid_f") = "" And cboLeave.SelectedValue <> "C" And (Val(lblBal.Text) >= 0.5 Or lblBal.Text = "-") Then
                                bEnabled = True
                            ElseIf mMyDataReader("Leaveid_1") = "" And mMyDataReader("Leaveid_2") = "" And mMyDataReader("Leaveid_f") Like "*OT*" And cboLeave.SelectedValue <> "C" And (Val(lblBal.Text) >= 0.5 Or lblBal.Text = "-") Then
                                If mMyDataReader("runno") Mod 7 = 0 Or mMyDataReader("runno") Mod 7 = 6 Then
                                    If (cboLeave.SelectedValue = "BT" Or cboLeave.SelectedValue = "HT" Or cboLeave.SelectedValue = "EHL") Then
                                        bEnabled = True
                                    Else
                                        bEnabled = False
                                    End If
                                Else
                                    bEnabled = False
                                End If
                            ElseIf mMyDataReader("Leaveid_1") = "" And mMyDataReader("Leaveid_f") = "" And (mMyDataReader("Leaveid_HT") = "HT" Or mMyDataReader("Leaveid_HT") = "EHL") And mMyDataReader("Leaveid_OT") = "" And mMyDataReader("Leaveid_BT") = "" And cboLeave.SelectedValue <> "C" And (cboLeave.SelectedValue = "AL" Or cboLeave.SelectedValue = "EAL" Or cboLeave.SelectedValue = "CAL") And (Val(lblBal.Text) >= 0.5 Or lblBal.Text = "-") Then
                                bEnabled = True
                            Else
                                bEnabled = False
                            End If
                        End If
                    ElseIf cboPeriod.SelectedValue = "2" Then
                        If mMyDataReader("Type") = "P" And mMyDataReader("Leaveid_2") <> "" And cboLeave.SelectedValue = "C" And mMyDataReader("Status_2").ToString.ToUpper = "PENDING" Then
                            bEnabled = True
                        ElseIf mMyDataReader("Leaveid_2") = "" And mMyDataReader("Leaveid_f") = "" And cboLeave.SelectedValue <> "C" And (Val(lblBal.Text) >= 0.5 Or lblBal.Text = "-") Then
                            bEnabled = True
                        Else
                            If mMyDataReader("Leaveid_2") <> "" And cboLeave.SelectedValue = "C" And mMyDataReader("Status_2").ToString.ToUpper = "PENDING" Then
                                bEnabled = True
                            ElseIf (mMyDataReader("Leaveid_2") = "AL" Or mMyDataReader("Leaveid_2") = "EAL" Or mMyDataReader("Leaveid_2") = "CAL") And (mMyDataReader("Leaveid_HT") = "") And (cboLeave.SelectedValue = "HT" Or cboLeave.SelectedValue = "EHL") Then
                                bEnabled = True
                            ElseIf (mMyDataReader("Leaveid_2") = "HT" Or mMyDataReader("Leaveid_2") = "EHL") And (mMyDataReader("Leaveid_HT") <> "") And (cboLeave.SelectedValue = "AL" Or cboLeave.SelectedValue = "EAL" Or cboLeave.SelectedValue = "CAL") Then
                                bEnabled = True
                            ElseIf mMyDataReader("Leaveid_2") = "" And mMyDataReader("Leaveid_f") = "" And cboLeave.SelectedValue <> "C" And (Val(lblBal.Text) >= 0.5 Or lblBal.Text = "-") Then
                                bEnabled = True
                            ElseIf mMyDataReader("Leaveid_2") = "" And mMyDataReader("Leaveid_1") = "" And mMyDataReader("Leaveid_f") Like "*OT*" And cboLeave.SelectedValue <> "C" And (Val(lblBal.Text) >= 0.5 Or lblBal.Text = "-") Then
                                If mMyDataReader("runno") Mod 7 = 0 Or mMyDataReader("runno") Mod 7 = 6 Then
                                    If (cboLeave.SelectedValue = "BT" Or cboLeave.SelectedValue = "HT" Or cboLeave.SelectedValue = "EHL") Then
                                        bEnabled = True
                                    Else
                                        bEnabled = False
                                    End If
                                Else
                                    bEnabled = False
                                End If
                            ElseIf mMyDataReader("Leaveid_2") = "" And mMyDataReader("Leaveid_f") = "" And (mMyDataReader("Leaveid_HT") = "HT" Or mMyDataReader("Leaveid_HT") = "EHL") And mMyDataReader("Leaveid_OT") = "" And mMyDataReader("Leaveid_BT") = "" And cboLeave.SelectedValue <> "C" And (cboLeave.SelectedValue = "AL" Or cboLeave.SelectedValue = "EAL" Or cboLeave.SelectedValue = "CAL") And (Val(lblBal.Text) >= 0.5 Or lblBal.Text = "-") Then
                                bEnabled = True
                            Else
                                bEnabled = False
                            End If
                        End If
                    ElseIf cboPeriod.SelectedValue = "N" Then
                        If mMyDataReader("Type") = "P" And mMyDataReader("Type") = "P" And mMyDataReader("Leaveid_F") <> "" And mMyDataReader("Leaveid_F") = "OTA" And mMyDataReader("Leaveid_F") = "OTL" And cboLeave.SelectedValue = "C" And mMyDataReader("Status_F").ToString.ToUpper = "PENDING" Then
                            bEnabled = True
                        Else
                            If mMyDataReader("Leaveid_F") <> "" And mMyDataReader("LeaveID_F") Like "*OT*" And mMyDataReader("LeaveID_OT") Like "*OT*" And cboLeave.SelectedValue = "C" And mMyDataReader("Status_F").ToString.ToUpper = "PENDING" Then
                                bEnabled = True
                            ElseIf (mMyDataReader("Leaveid_1") = "HT" Or mMyDataReader("Leaveid_2") = "HT" Or mMyDataReader("Leaveid_1") = "EHL" Or mMyDataReader("Leaveid_2") = "EHL") And mMyDataReader("Leaveid_F") <> "HT" And mMyDataReader("Leaveid_F") <> "EHL" And (mMyDataReader("Leaveid_HT") = "HT" Or mMyDataReader("Leaveid_HT") = "EHL") And mMyDataReader("LeaveID_OT") = "" And mMyDataReader("LeaveID_BT") = "" And cboLeave.SelectedValue <> "C" And cboLeave.SelectedValue <> "HT" And cboLeave.SelectedValue <> "EHL" And (cboLeave.SelectedValue = "OTL" Or cboLeave.SelectedValue = "OTA") And cboLeave.SelectedValue <> "BT" Then
                                If cboLeave.SelectedValue = "OTL" And (mMyDataReader("runno") Mod 7 = 6 Or Holiday) Then
                                    bEnabled = True
                                ElseIf cboLeave.SelectedValue = "OTA" Then
                                    bEnabled = True
                                Else
                                    bEnabled = False
                                End If
                            ElseIf (mMyDataReader("Leaveid_F") = "BT" Or mMyDataReader("Leaveid_1") = "BT" Or mMyDataReader("Leaveid_2") = "BT") And mMyDataReader("Leaveid_HT") = "" And mMyDataReader("LeaveID_OT") = "" And mMyDataReader("LeaveID_BT") = "BT" And cboLeave.SelectedValue <> "C" And cboLeave.SelectedValue <> "HT" And cboLeave.SelectedValue <> "EHL" And cboLeave.SelectedValue <> "BT" Then
                                If cboLeave.SelectedValue = "OTL" And (mMyDataReader("runno") Mod 7 = 6 Or Holiday) Then
                                    bEnabled = True
                                ElseIf cboLeave.SelectedValue = "OTA" Then
                                    bEnabled = True
                                Else
                                    bEnabled = False
                                End If
                            ElseIf mMyDataReader("Leaveid_F") Like "*OT*" And mMyDataReader("Leaveid_HT") = "" And mMyDataReader("LeaveID_OT") Like "*OT*" And mMyDataReader("LeaveID_BT") = "" And cboLeave.SelectedValue <> "C" And (cboLeave.SelectedValue = "OTA" And cboLeave.SelectedValue = "OTL") And cboLeave.SelectedValue <> "HT" And cboLeave.SelectedValue <> "EHL" And mMyDataReader("Status_F").ToString.ToUpper = "PENDING" Then
                                bEnabled = True
                            ElseIf mMyDataReader("Leaveid_F") = "" And (mMyDataReader("Leaveid_1") <> "" Or mMyDataReader("Leaveid_2") <> "") And (cboLeave.SelectedValue = "OTA" Or cboLeave.SelectedValue = "OTL") Then
                                bEnabled = False
                            Else
                                bEnabled = False
                            End If
                        End If
                    End If

                    'If cboLeave.SelectedValue = "OTA" Or cboLeave.SelectedValue = "OTL" Or cboLeave.SelectedValue = "BT" Or cboLeave.SelectedValue = "HT" Then
                    '    If mMyDataReader("Leaveid_OT") Like "*OT*" And (cboLeave.SelectedValue = "OTA" Or cboLeave.SelectedValue = "OTL") Then
                    '        bEnabled = False
                    '    ElseIf mMyDataReader("Leaveid_BT") Like "*BT*" And (cboLeave.SelectedValue = "BT") Then
                    '        bEnabled = False
                    '    ElseIf mMyDataReader("Leaveid_BT") Like "*BT*" And (cboLeave.SelectedValue <> "BT" Or cboLeave.SelectedValue <> "OTA" Or cboLeave.SelectedValue <> "OTL") Then
                    '        bEnabled = False
                    '    ElseIf mMyDataReader("Leaveid_HT") Like "*HT*" And (cboLeave.SelectedValue = "HT" Or cboLeave.SelectedValue = "BT" Or cboLeave.SelectedValue = "OTL" Or cboLeave.SelectedValue = "OTA") Then
                    '        bEnabled = False
                    '    Else
                    '        bEnabled = True
                    '    End If
                    '    If mMyDataReader("runno") Mod 7 <> 6 And cboLeave.SelectedValue = "OTL" Then
                    '        bEnabled = False
                    '    End If
                    'End If

                    'If cboLeave.SelectedValue <> "OTA" And cboLeave.SelectedValue <> "OTL" And cboLeave.SelectedValue <> "BT" And cboLeave.SelectedValue <> "HT" Then
                    '    If mMyDataReader("Leaveid_HT") Like "*HT*" And mMyDataReader("Leaveid_F") = "HT" Then
                    '        bEnabled = True
                    '    End If
                    '    If mMyDataReader("Leaveid_BT") Like "*BT*" And mMyDataReader("Leaveid_F") = "BT" Then
                    '        bEnabled = True
                    '    End If
                    '    If mMyDataReader("Leaveid_OT") Like "*OT*" And mMyDataReader("Leaveid_F") = "OT" Then
                    '        bEnabled = True
                    '    End If
                    'End If
                    sysPlanColor = Nothing
                    sysApplyColor = Nothing
                End If
            End If



            Select Case mMyDataReader("runno")
                Case "1", "36"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData1, strLeave, strToolTip, sysPlanColor, btnDay1, bEnabled)
                    Else
                        FillDetail2(lblApplyData1, strLeave, strToolTip, sysApplyColor, btnDay1, bEnabled)
                    End If
                Case "2", "37"

                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData2, strLeave, strToolTip, sysPlanColor, btnDay2, bEnabled)
                    Else
                        FillDetail2(lblApplyData2, strLeave, strToolTip, sysApplyColor, btnDay2, bEnabled)
                    End If

                Case "3", "38"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData3, strLeave, strToolTip, sysPlanColor, btnDay3, bEnabled)
                    Else
                        FillDetail2(lblApplyData3, strLeave, strToolTip, sysApplyColor, btnDay3, bEnabled)
                    End If
                Case "4", "39"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData4, strLeave, strToolTip, sysPlanColor, btnDay4, bEnabled)
                    Else
                        FillDetail2(lblApplyData4, strLeave, strToolTip, sysApplyColor, btnDay4, bEnabled)
                    End If
                Case "5", "40"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData5, strLeave, strToolTip, sysPlanColor, btnDay5, bEnabled)
                    Else
                        FillDetail2(lblApplyData5, strLeave, strToolTip, sysApplyColor, btnDay5, bEnabled)
                    End If
                Case "6", "41"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData6, strLeave, strToolTip, sysPlanColor, btnDay6, bEnabled)
                    Else
                        FillDetail2(lblApplyData6, strLeave, strToolTip, sysApplyColor, btnDay6, bEnabled)
                    End If
                Case "7", "42"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData7, strLeave, strToolTip, sysPlanColor, btnDay7, bEnabled)
                    Else
                        FillDetail2(lblApplyData7, strLeave, strToolTip, sysApplyColor, btnDay7, bEnabled)
                    End If
                Case "8"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData8, strLeave, strToolTip, sysPlanColor, btnDay8, bEnabled)
                    Else
                        FillDetail2(lblApplyData8, strLeave, strToolTip, sysApplyColor, btnDay8, bEnabled)
                    End If
                Case "9"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData9, strLeave, strToolTip, sysPlanColor, btnDay9, bEnabled)
                    Else
                        FillDetail2(lblApplyData9, strLeave, strToolTip, sysApplyColor, btnDay9, bEnabled)
                    End If
                Case "10"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData10, strLeave, strToolTip, sysPlanColor, btnDay10, bEnabled)
                    Else
                        FillDetail2(lblApplyData10, strLeave, strToolTip, sysApplyColor, btnDay10, bEnabled)
                    End If
                Case "11"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData11, strLeave, strToolTip, sysPlanColor, btnDay11, bEnabled)
                    Else
                        FillDetail2(lblApplyData11, strLeave, strToolTip, sysApplyColor, btnDay11, bEnabled)
                    End If
                Case "12"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData12, strLeave, strToolTip, sysPlanColor, btnDay12, bEnabled)
                    Else
                        FillDetail2(lblApplyData12, strLeave, strToolTip, sysApplyColor, btnDay12, bEnabled)
                    End If
                Case "13"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData13, strLeave, strToolTip, sysPlanColor, btnDay13, bEnabled)
                    Else
                        FillDetail2(lblApplyData13, strLeave, strToolTip, sysApplyColor, btnDay13, bEnabled)
                    End If
                Case "14"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData14, strLeave, strToolTip, sysPlanColor, btnDay14, bEnabled)
                    Else
                        FillDetail2(lblApplyData14, strLeave, strToolTip, sysApplyColor, btnDay14, bEnabled)
                    End If
                Case "15"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData15, strLeave, strToolTip, sysPlanColor, btnDay15, bEnabled)
                    Else
                        FillDetail2(lblApplyData15, strLeave, strToolTip, sysApplyColor, btnDay15, bEnabled)
                    End If
                Case "16"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData16, strLeave, strToolTip, sysPlanColor, btnDay16, bEnabled)
                    Else
                        FillDetail2(lblApplyData16, strLeave, strToolTip, sysApplyColor, btnDay16, bEnabled)
                    End If
                Case "17"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData17, strLeave, strToolTip, sysPlanColor, btnDay17, bEnabled)
                    Else
                        FillDetail2(lblApplyData17, strLeave, strToolTip, sysApplyColor, btnDay17, bEnabled)
                    End If
                Case "18"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData18, strLeave, strToolTip, sysPlanColor, btnDay18, bEnabled)
                    Else
                        FillDetail2(lblApplyData18, strLeave, strToolTip, sysApplyColor, btnDay18, bEnabled)
                    End If
                Case "19"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData19, strLeave, strToolTip, sysPlanColor, btnDay19, bEnabled)
                    Else
                        FillDetail2(lblApplyData19, strLeave, strToolTip, sysApplyColor, btnDay19, bEnabled)
                    End If
                Case "20"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData20, strLeave, strToolTip, sysPlanColor, btnDay20, bEnabled)
                    Else
                        FillDetail2(lblApplyData20, strLeave, strToolTip, sysApplyColor, btnDay20, bEnabled)
                    End If
                Case "21"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData21, strLeave, strToolTip, sysPlanColor, btnDay21, bEnabled)
                    Else
                        FillDetail2(lblApplyData21, strLeave, strToolTip, sysApplyColor, btnDay21, bEnabled)
                    End If
                Case "22"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData22, strLeave, strToolTip, sysPlanColor, btnDay22, bEnabled)
                    Else
                        FillDetail2(lblApplyData22, strLeave, strToolTip, sysApplyColor, btnDay22, bEnabled)
                    End If
                Case "23"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData23, strLeave, strToolTip, sysPlanColor, btnDay23, bEnabled)
                    Else
                        FillDetail2(lblApplyData23, strLeave, strToolTip, sysApplyColor, btnDay23, bEnabled)
                    End If
                Case "24"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData24, strLeave, strToolTip, sysPlanColor, btnDay24, bEnabled)
                    Else
                        FillDetail2(lblApplyData24, strLeave, strToolTip, sysApplyColor, btnDay24, bEnabled)
                    End If
                Case "25"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData25, strLeave, strToolTip, sysPlanColor, btnDay25, bEnabled)
                    Else
                        FillDetail2(lblApplyData25, strLeave, strToolTip, sysApplyColor, btnDay25, bEnabled)
                    End If
                Case "26"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData26, strLeave, strToolTip, sysPlanColor, btnDay26, bEnabled)
                    Else
                        FillDetail2(lblApplyData26, strLeave, strToolTip, sysApplyColor, btnDay26, bEnabled)
                    End If
                Case "27"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData27, strLeave, strToolTip, sysPlanColor, btnDay27, bEnabled)
                    Else
                        FillDetail2(lblApplyData27, strLeave, strToolTip, sysApplyColor, btnDay27, bEnabled)
                    End If
                Case "28"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData28, strLeave, strToolTip, sysPlanColor, btnDay28, bEnabled)
                    Else
                        FillDetail2(lblApplyData28, strLeave, strToolTip, sysApplyColor, btnDay28, bEnabled)
                    End If
                Case "29"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData29, strLeave, strToolTip, sysPlanColor, btnDay29, bEnabled)
                    Else
                        FillDetail2(lblApplyData29, strLeave, strToolTip, sysApplyColor, btnDay29, bEnabled)
                    End If
                Case "30"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData30, strLeave, strToolTip, sysPlanColor, btnDay30, bEnabled)
                    Else
                        FillDetail2(lblApplyData30, strLeave, strToolTip, sysApplyColor, btnDay30, bEnabled)
                    End If
                Case "31"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData31, strLeave, strToolTip, sysPlanColor, btnDay31, bEnabled)
                    Else
                        FillDetail2(lblApplyData31, strLeave, strToolTip, sysApplyColor, btnDay31, bEnabled)
                    End If
                Case "32"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData32, strLeave, strToolTip, sysPlanColor, btnDay32, bEnabled)
                    Else
                        FillDetail2(lblApplyData32, strLeave, strToolTip, sysApplyColor, btnDay32, bEnabled)
                    End If
                Case "33"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData33, strLeave, strToolTip, sysPlanColor, btnDay33, bEnabled)
                    Else
                        FillDetail2(lblApplyData33, strLeave, strToolTip, sysApplyColor, btnDay33, bEnabled)
                    End If
                Case "34"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData34, strLeave, strToolTip, sysPlanColor, btnDay34, bEnabled)
                    Else
                        FillDetail2(lblApplyData34, strLeave, strToolTip, sysApplyColor, btnDay34, bEnabled)
                    End If
                Case "35"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2(lblPlanData35, strLeave, strToolTip, sysPlanColor, btnDay35, bEnabled)
                    Else
                        FillDetail2(lblApplyData35, strLeave, strToolTip, sysApplyColor, btnDay35, bEnabled)
                    End If
            End Select
        End While

        mMyDataReader.Close()



    End Sub
    Public Sub FillDetail_Temp()

        Dim strLeave As String = ""
        Dim strToolTip As String = ""
        Dim sysPlanColor As Drawing.Color
        Dim sysApplyColor As Drawing.Color
        Dim bEnabled As Boolean = False
        Dim Holiday As Boolean = False

        mMyCommand.CommandText = "sp_web_ls_Sel_StaffLeave_Temp '" & mstrEmpID & "'," & cboYear.SelectedValue & "," & cboMonth.SelectedValue & ",'" & _
                                "" & "','" & cboLeave.SelectedValue & "','" & "DETAIL" & "'"
        mMyDataReader = mMyCommand.ExecuteReader



        While mMyDataReader.Read()
            strLeave = ""

            Select Case mMyDataReader("runno")
                Case "1", "36"
                    Holiday = bIsHoliday(btnDay1)
                Case "2", "37"
                    Holiday = bIsHoliday(btnDay2)
                Case "3", "38"
                    Holiday = bIsHoliday(btnDay3)
                Case "4", "39"
                    Holiday = bIsHoliday(btnDay4)
                Case "5", "40"
                    Holiday = bIsHoliday(btnDay5)
                Case "6", "41"
                    Holiday = bIsHoliday(btnDay6)
                Case "7", "42"
                    Holiday = bIsHoliday(btnDay7)
                Case "8"
                    Holiday = bIsHoliday(btnDay8)
                Case "9"
                    Holiday = bIsHoliday(btnDay9)
                Case "10"
                    Holiday = bIsHoliday(btnDay10)
                Case "11"
                    Holiday = bIsHoliday(btnDay11)
                Case "12"
                    Holiday = bIsHoliday(btnDay12)
                Case "13"
                    Holiday = bIsHoliday(btnDay13)
                Case "14"
                    Holiday = bIsHoliday(btnDay14)
                Case "15"
                    Holiday = bIsHoliday(btnDay15)
                Case "16"
                    Holiday = bIsHoliday(btnDay16)
                Case "17"
                    Holiday = bIsHoliday(btnDay17)
                Case "18"
                    Holiday = bIsHoliday(btnDay18)
                Case "19"
                    Holiday = bIsHoliday(btnDay19)
                Case "20"
                    Holiday = bIsHoliday(btnDay20)
                Case "21"
                    Holiday = bIsHoliday(btnDay21)
                Case "22"
                    Holiday = bIsHoliday(btnDay22)
                Case "23"
                    Holiday = bIsHoliday(btnDay23)
                Case "24"
                    Holiday = bIsHoliday(btnDay24)
                Case "25"
                    Holiday = bIsHoliday(btnDay25)
                Case "26"
                    Holiday = bIsHoliday(btnDay26)
                Case "27"
                    Holiday = bIsHoliday(btnDay27)
                Case "28"
                    Holiday = bIsHoliday(btnDay28)
                Case "29"
                    Holiday = bIsHoliday(btnDay29)
                Case "30"
                    Holiday = bIsHoliday(btnDay30)
                Case "31"
                    Holiday = bIsHoliday(btnDay31)
                Case "32"
                    Holiday = bIsHoliday(btnDay32)
                Case "33"
                    Holiday = bIsHoliday(btnDay33)
                Case "34"
                    Holiday = bIsHoliday(btnDay34)
                Case "35"
                    Holiday = bIsHoliday(btnDay35)
            End Select

            If mMyDataReader("leaveid_f") = "" And mMyDataReader("leaveid_1") = "" And mMyDataReader("leaveid_2") = "" Then
                strToolTip = ""
                sysPlanColor = Nothing
                sysApplyColor = Nothing
            Else
                If mMyDataReader("Type") = "A" Then
                    mbApplied = True

                    If mMyDataReader("Status_F") <> "" Then
                        strToolTip = mMyDataReader("Status_F")
                    ElseIf mMyDataReader("Status_1") <> "" And mMyDataReader("Status_2") = "" Then
                        strToolTip = mMyDataReader("Status_1")
                    ElseIf mMyDataReader("Status_1") <> "" And mMyDataReader("Status_2") <> "" Then
                        strToolTip = "1st Half: " & mMyDataReader("Status_1") & vbCrLf & "2nd Half: " & mMyDataReader("Status_2")
                    ElseIf mMyDataReader("Status_1") = "" And mMyDataReader("Status_2") <> "" Then
                        strToolTip = mMyDataReader("Status_2")
                    End If
                Else
                    strToolTip = ""
                End If

                If mMyDataReader("LeaveID_F") <> "" Then
                    If mMyDataReader("LeaveID_F") <> "OTA" And mMyDataReader("LeaveID_F") <> "OTL" Then
                        strLeave = mMyDataReader("LeaveID_F") & "-F"
                    Else
                        strLeave = mMyDataReader("LeaveID_F") & ""
                    End If
                End If

                If mMyDataReader("LeaveID_1") <> "" Then
                    If strLeave <> "" Then strLeave = strLeave & " | "

                    strLeave = strLeave & mMyDataReader("LeaveID_1") & "-1"
                End If

                If mMyDataReader("LeaveID_2") <> "" Then
                    If strLeave <> "" Then strLeave = strLeave & " | "

                    strLeave = strLeave & mMyDataReader("LeaveID_2") & "-2"
                End If

                If mMyDataReader("Type") = cboType.SelectedValue Then
                    If cboPeriod.SelectedValue = "F" Then
                        If mMyDataReader("Leaveid_F") <> "" And mMyDataReader("LeaveID_F") <> "OTA" And mMyDataReader("LeaveID_F") <> "OTL" And cboLeave.SelectedValue = "C" And mMyDataReader("Status_F").ToString.ToUpper = "PENDING" Then
                            bEnabled = True
                        ElseIf (mMyDataReader("Leaveid_1") <> "" Or mMyDataReader("Leaveid_2") <> "") And cboLeave.SelectedValue <> "C" Then
                            bEnabled = False
                        ElseIf mMyDataReader("Leaveid_1") = "" And mMyDataReader("Leaveid_2") = "" And (mMyDataReader("Leaveid_F") = "HT" Or mMyDataReader("Leaveid_F") = "EHL") And (mMyDataReader("Leaveid_HT") = "HT" Or mMyDataReader("Leaveid_HT") = "EHL") And mMyDataReader("LeaveID_OT") = "" And mMyDataReader("LeaveID_BT") = "" And cboLeave.SelectedValue <> "C" And (cboLeave.SelectedValue = "AL" Or cboLeave.SelectedValue = "EAL" Or cboLeave.SelectedValue = "CAL") And mMyDataReader("Status_F").ToString.ToUpper = "PENDING" Then
                            If mMyDataReader("runno") Mod 7 = 0 Or mMyDataReader("runno") Mod 7 = 6 Then
                                bEnabled = False
                            Else
                                bEnabled = True
                            End If
                        ElseIf (mMyDataReader("Leaveid_F") = "AL" Or mMyDataReader("Leaveid_F") = "EAL" Or mMyDataReader("Leaveid_F") = "CAL") And mMyDataReader("Leaveid_HT") = "" And mMyDataReader("LeaveID_OT") = "" And mMyDataReader("LeaveID_BT") = "" And cboLeave.SelectedValue <> "C" And (cboLeave.SelectedValue = "HT" Or cboLeave.SelectedValue = "EHL") And cboLeave.SelectedValue <> "OTA" And cboLeave.SelectedValue <> "OTL" And cboLeave.SelectedValue <> "BT" Then
                            bEnabled = True
                        ElseIf mMyDataReader("Leaveid_F") = "BT" And mMyDataReader("Leaveid_HT") = "" And mMyDataReader("LeaveID_OT") = "" And mMyDataReader("LeaveID_BT") = "BT" And cboLeave.SelectedValue <> "C" And (cboLeave.SelectedValue = "MC" Or cboLeave.SelectedValue = "OTA" Or cboLeave.SelectedValue = "OTL") And mMyDataReader("Status_F").ToString.ToUpper = "PENDING" Then
                            If mMyDataReader("runno") Mod 7 = 0 Or mMyDataReader("runno") Mod 7 = 6 Or Holiday Then
                                If cboLeave.SelectedValue = "OTL" And (mMyDataReader("runno") Mod 7 = 6 Or Holiday) Then
                                    bEnabled = True
                                ElseIf cboLeave.SelectedValue = "OTA" And (mMyDataReader("runno") Mod 7 = 0 Or Holiday) Then
                                    bEnabled = True
                                Else
                                    bEnabled = False
                                End If
                            Else
                                bEnabled = True
                            End If
                        ElseIf mMyDataReader("Leaveid_HT") = "" And mMyDataReader("LeaveID_OT") = "" And mMyDataReader("LeaveID_BT") = "" And cboLeave.SelectedValue <> "C" And cboLeave.SelectedValue <> "HT" And cboLeave.SelectedValue <> "EHL" And cboLeave.SelectedValue <> "OT" And cboLeave.SelectedValue = "BT" And mMyDataReader("Leaveid_F") = "MC" Then
                            bEnabled = True
                        ElseIf mMyDataReader("Leaveid_F") Like "*OT*" And mMyDataReader("Leaveid_HT") = "" And mMyDataReader("LeaveID_OT") Like "*OT*" And mMyDataReader("LeaveID_BT") = "" And cboLeave.SelectedValue = "BT" And cboLeave.SelectedValue <> "HT" And cboLeave.SelectedValue <> "EHL" And mMyDataReader("Status_F").ToString.ToUpper = "PENDING" Then
                            bEnabled = True
                        Else
                            bEnabled = False
                        End If
                    ElseIf cboPeriod.SelectedValue = "1" Then
                        If mMyDataReader("Leaveid_1") <> "" And cboLeave.SelectedValue = "C" And mMyDataReader("Status_1").ToString.ToUpper = "PENDING" Then
                            bEnabled = True
                        ElseIf (mMyDataReader("Leaveid_1") = "AL" Or mMyDataReader("Leaveid_1") = "EAL" Or mMyDataReader("Leaveid_1") = "CAL") And (mMyDataReader("Leaveid_HT") = "") And (cboLeave.SelectedValue = "HT" Or cboLeave.SelectedValue = "EHL") Then
                            bEnabled = True
                        ElseIf (mMyDataReader("Leaveid_1") = "HT" Or mMyDataReader("Leaveid_1") = "EHL") And (mMyDataReader("Leaveid_HT") <> "") And (cboLeave.SelectedValue = "AL" Or cboLeave.SelectedValue = "EAL" Or cboLeave.SelectedValue = "CAL") Then
                            bEnabled = True
                        ElseIf mMyDataReader("Leaveid_1") = "" And mMyDataReader("Leaveid_f") = "" And cboLeave.SelectedValue <> "C" And (Val(lblBal.Text) >= 0.5 Or lblBal.Text = "-") Then
                            bEnabled = True
                        ElseIf mMyDataReader("Leaveid_1") = "" And mMyDataReader("Leaveid_2") = "" And mMyDataReader("Leaveid_f") Like "*OT*" And cboLeave.SelectedValue <> "C" And (Val(lblBal.Text) >= 0.5 Or lblBal.Text = "-") Then
                            If mMyDataReader("runno") Mod 7 = 0 Or mMyDataReader("runno") Mod 7 = 6 Then
                                If (cboLeave.SelectedValue = "BT" Or cboLeave.SelectedValue = "HT" Or cboLeave.SelectedValue = "EHL") Then
                                    bEnabled = True
                                Else
                                    bEnabled = False
                                End If
                            Else
                                bEnabled = False
                            End If
                        ElseIf mMyDataReader("Leaveid_1") = "" And mMyDataReader("Leaveid_f") = "" And (mMyDataReader("Leaveid_HT") = "HT" Or mMyDataReader("Leaveid_HT") = "EHL") And mMyDataReader("Leaveid_OT") = "" And mMyDataReader("Leaveid_BT") = "" And cboLeave.SelectedValue <> "C" And (cboLeave.SelectedValue = "AL" Or cboLeave.SelectedValue = "EAL" Or cboLeave.SelectedValue = "CAL") And (Val(lblBal.Text) >= 0.5 Or lblBal.Text = "-") Then
                            bEnabled = True
                        Else
                            bEnabled = False
                        End If
                    ElseIf cboPeriod.SelectedValue = "2" Then
                        If mMyDataReader("Leaveid_2") <> "" And cboLeave.SelectedValue = "C" And mMyDataReader("Status_2").ToString.ToUpper = "PENDING" Then
                            bEnabled = True
                        ElseIf (mMyDataReader("Leaveid_2") = "AL" Or mMyDataReader("Leaveid_2") = "EAL" Or mMyDataReader("Leaveid_2") = "CAL") And (mMyDataReader("Leaveid_HT") = "") And (cboLeave.SelectedValue = "HT" Or cboLeave.SelectedValue = "EHL") Then
                            bEnabled = True
                        ElseIf (mMyDataReader("Leaveid_2") = "HT" Or mMyDataReader("Leaveid_2") = "EHL") And (mMyDataReader("Leaveid_HT") <> "") And (cboLeave.SelectedValue = "AL" Or cboLeave.SelectedValue = "EAL" Or cboLeave.SelectedValue = "CAL") Then
                            bEnabled = True
                        ElseIf mMyDataReader("Leaveid_2") = "" And mMyDataReader("Leaveid_f") = "" And cboLeave.SelectedValue <> "C" And (Val(lblBal.Text) >= 0.5 Or lblBal.Text = "-") Then
                            bEnabled = True
                        ElseIf mMyDataReader("Leaveid_2") = "" And mMyDataReader("Leaveid_1") = "" And mMyDataReader("Leaveid_f") Like "*OT*" And cboLeave.SelectedValue <> "C" And (Val(lblBal.Text) >= 0.5 Or lblBal.Text = "-") Then
                            If mMyDataReader("runno") Mod 7 = 0 Or mMyDataReader("runno") Mod 7 = 6 Then
                                If (cboLeave.SelectedValue = "BT" Or cboLeave.SelectedValue = "HT" Or cboLeave.SelectedValue = "EHL") Then
                                    bEnabled = True
                                Else
                                    bEnabled = False
                                End If
                            Else
                                bEnabled = False
                            End If
                        ElseIf mMyDataReader("Leaveid_2") = "" And mMyDataReader("Leaveid_f") = "" And (mMyDataReader("Leaveid_HT") = "HT" Or mMyDataReader("Leaveid_HT") = "EHL") And mMyDataReader("Leaveid_OT") = "" And mMyDataReader("Leaveid_BT") = "" And cboLeave.SelectedValue <> "C" And (cboLeave.SelectedValue = "AL" Or cboLeave.SelectedValue = "EAL" Or cboLeave.SelectedValue = "CAL") And (Val(lblBal.Text) >= 0.5 Or lblBal.Text = "-") Then
                            bEnabled = True
                        Else
                            bEnabled = False
                        End If
                    ElseIf cboPeriod.SelectedValue = "N" Then
                        If mMyDataReader("Leaveid_F") <> "" And mMyDataReader("LeaveID_F") Like "*OT*" And mMyDataReader("LeaveID_OT") Like "*OT*" And cboLeave.SelectedValue = "C" And mMyDataReader("Status_F").ToString.ToUpper = "PENDING" Then
                            bEnabled = True
                        ElseIf (mMyDataReader("Leaveid_1") = "HT" Or mMyDataReader("Leaveid_2") = "HT" Or mMyDataReader("Leaveid_1") = "EHL" Or mMyDataReader("Leaveid_2") = "EHL") And mMyDataReader("Leaveid_F") <> "HT" And mMyDataReader("Leaveid_HT") = "HT" And mMyDataReader("LeaveID_OT") = "" And mMyDataReader("LeaveID_BT") = "" And cboLeave.SelectedValue <> "C" And cboLeave.SelectedValue <> "HT" And cboLeave.SelectedValue <> "EHL" And (cboLeave.SelectedValue = "OTL" Or cboLeave.SelectedValue = "OTA") And cboLeave.SelectedValue <> "BT" Then
                            If cboLeave.SelectedValue = "OTL" And (mMyDataReader("runno") Mod 7 = 6 Or Holiday) Then
                                bEnabled = True
                            ElseIf cboLeave.SelectedValue = "OTA" Then
                                bEnabled = True
                            Else
                                bEnabled = False
                            End If
                        ElseIf (mMyDataReader("Leaveid_F") = "BT" Or mMyDataReader("Leaveid_1") = "BT" Or mMyDataReader("Leaveid_2") = "BT") And mMyDataReader("Leaveid_HT") = "" And mMyDataReader("LeaveID_OT") = "" And mMyDataReader("LeaveID_BT") = "BT" And cboLeave.SelectedValue <> "C" And cboLeave.SelectedValue <> "HT" And cboLeave.SelectedValue <> "EHL" And cboLeave.SelectedValue <> "BT" Then
                            If cboLeave.SelectedValue = "OTL" And (mMyDataReader("runno") Mod 7 = 6 Or Holiday) Then
                                bEnabled = True
                            ElseIf cboLeave.SelectedValue = "OTA" Then
                                bEnabled = True
                            Else
                                bEnabled = False
                            End If
                        ElseIf mMyDataReader("Leaveid_F") Like "*OT*" And mMyDataReader("Leaveid_HT") = "" And mMyDataReader("LeaveID_OT") Like "*OT*" And mMyDataReader("LeaveID_BT") = "" And cboLeave.SelectedValue <> "C" And (cboLeave.SelectedValue = "OTA" And cboLeave.SelectedValue = "OTL") And cboLeave.SelectedValue <> "HT" And cboLeave.SelectedValue <> "EHL" And mMyDataReader("Status_F").ToString.ToUpper = "PENDING" Then
                            If cboLeave.SelectedValue = "OTL" And (mMyDataReader("runno") Mod 7 = 6 Or Holiday) Then
                                bEnabled = True
                            ElseIf cboLeave.SelectedValue = "OTA" Then
                                bEnabled = True
                            Else
                                bEnabled = False
                            End If
                            'ElseIf mMyDataReader("Leaveid_F") = "" And (mMyDataReader("Leaveid_1") <> "" Or mMyDataReader("Leaveid_2") <> "") And (cboLeave.SelectedValue = "OTA" Or cboLeave.SelectedValue = "OTL") Then
                            '    bEnabled = True
                        Else
                            bEnabled = False
                        End If
                    End If
                End If

                sysPlanColor = Nothing
                sysApplyColor = Nothing
            End If



            Select Case mMyDataReader("runno")
                Case "1", "36"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData1, strLeave, strToolTip, sysPlanColor, btnDay1, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData1, strLeave, strToolTip, sysApplyColor, btnDay1, bEnabled)
                    End If
                Case "2", "37"

                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData2, strLeave, strToolTip, sysPlanColor, btnDay2, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData2, strLeave, strToolTip, sysApplyColor, btnDay2, bEnabled)
                    End If

                Case "3", "38"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData3, strLeave, strToolTip, sysPlanColor, btnDay3, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData3, strLeave, strToolTip, sysApplyColor, btnDay3, bEnabled)
                    End If
                Case "4", "39"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData4, strLeave, strToolTip, sysPlanColor, btnDay4, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData4, strLeave, strToolTip, sysApplyColor, btnDay4, bEnabled)
                    End If
                Case "5", "40"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData5, strLeave, strToolTip, sysPlanColor, btnDay5, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData5, strLeave, strToolTip, sysApplyColor, btnDay5, bEnabled)
                    End If
                Case "6", "41"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData6, strLeave, strToolTip, sysPlanColor, btnDay6, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData6, strLeave, strToolTip, sysApplyColor, btnDay6, bEnabled)
                    End If
                Case "7", "42"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData7, strLeave, strToolTip, sysPlanColor, btnDay7, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData7, strLeave, strToolTip, sysApplyColor, btnDay7, bEnabled)
                    End If
                Case "8"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData8, strLeave, strToolTip, sysPlanColor, btnDay8, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData8, strLeave, strToolTip, sysApplyColor, btnDay8, bEnabled)
                    End If
                Case "9"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData9, strLeave, strToolTip, sysPlanColor, btnDay9, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData9, strLeave, strToolTip, sysApplyColor, btnDay9, bEnabled)
                    End If
                Case "10"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData10, strLeave, strToolTip, sysPlanColor, btnDay10, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData10, strLeave, strToolTip, sysApplyColor, btnDay10, bEnabled)
                    End If
                Case "11"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData11, strLeave, strToolTip, sysPlanColor, btnDay11, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData11, strLeave, strToolTip, sysApplyColor, btnDay11, bEnabled)
                    End If
                Case "12"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData12, strLeave, strToolTip, sysPlanColor, btnDay12, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData12, strLeave, strToolTip, sysApplyColor, btnDay12, bEnabled)
                    End If
                Case "13"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData13, strLeave, strToolTip, sysPlanColor, btnDay13, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData13, strLeave, strToolTip, sysApplyColor, btnDay13, bEnabled)
                    End If
                Case "14"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData14, strLeave, strToolTip, sysPlanColor, btnDay14, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData14, strLeave, strToolTip, sysApplyColor, btnDay14, bEnabled)
                    End If
                Case "15"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData15, strLeave, strToolTip, sysPlanColor, btnDay15, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData15, strLeave, strToolTip, sysApplyColor, btnDay15, bEnabled)
                    End If
                Case "16"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData16, strLeave, strToolTip, sysPlanColor, btnDay16, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData16, strLeave, strToolTip, sysApplyColor, btnDay16, bEnabled)
                    End If
                Case "17"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData17, strLeave, strToolTip, sysPlanColor, btnDay17, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData17, strLeave, strToolTip, sysApplyColor, btnDay17, bEnabled)
                    End If
                Case "18"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData18, strLeave, strToolTip, sysPlanColor, btnDay18, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData18, strLeave, strToolTip, sysApplyColor, btnDay18, bEnabled)
                    End If
                Case "19"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData19, strLeave, strToolTip, sysPlanColor, btnDay19, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData19, strLeave, strToolTip, sysApplyColor, btnDay19, bEnabled)
                    End If
                Case "20"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData20, strLeave, strToolTip, sysPlanColor, btnDay20, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData20, strLeave, strToolTip, sysApplyColor, btnDay20, bEnabled)
                    End If
                Case "21"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData21, strLeave, strToolTip, sysPlanColor, btnDay21, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData21, strLeave, strToolTip, sysApplyColor, btnDay21, bEnabled)
                    End If
                Case "22"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData22, strLeave, strToolTip, sysPlanColor, btnDay22, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData22, strLeave, strToolTip, sysApplyColor, btnDay22, bEnabled)
                    End If
                Case "23"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData23, strLeave, strToolTip, sysPlanColor, btnDay23, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData23, strLeave, strToolTip, sysApplyColor, btnDay23, bEnabled)
                    End If
                Case "24"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData24, strLeave, strToolTip, sysPlanColor, btnDay24, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData24, strLeave, strToolTip, sysApplyColor, btnDay24, bEnabled)
                    End If
                Case "25"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData25, strLeave, strToolTip, sysPlanColor, btnDay25, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData25, strLeave, strToolTip, sysApplyColor, btnDay25, bEnabled)
                    End If
                Case "26"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData26, strLeave, strToolTip, sysPlanColor, btnDay26, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData26, strLeave, strToolTip, sysApplyColor, btnDay26, bEnabled)
                    End If
                Case "27"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData27, strLeave, strToolTip, sysPlanColor, btnDay27, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData27, strLeave, strToolTip, sysApplyColor, btnDay27, bEnabled)
                    End If
                Case "28"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData28, strLeave, strToolTip, sysPlanColor, btnDay28, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData28, strLeave, strToolTip, sysApplyColor, btnDay28, bEnabled)
                    End If
                Case "29"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData29, strLeave, strToolTip, sysPlanColor, btnDay29, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData29, strLeave, strToolTip, sysApplyColor, btnDay29, bEnabled)
                    End If
                Case "30"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData30, strLeave, strToolTip, sysPlanColor, btnDay30, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData30, strLeave, strToolTip, sysApplyColor, btnDay30, bEnabled)
                    End If
                Case "31"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData31, strLeave, strToolTip, sysPlanColor, btnDay31, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData31, strLeave, strToolTip, sysApplyColor, btnDay31, bEnabled)
                    End If
                Case "32"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData32, strLeave, strToolTip, sysPlanColor, btnDay32, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData32, strLeave, strToolTip, sysApplyColor, btnDay32, bEnabled)
                    End If
                Case "33"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData33, strLeave, strToolTip, sysPlanColor, btnDay33, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData33, strLeave, strToolTip, sysApplyColor, btnDay33, bEnabled)
                    End If
                Case "34"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData34, strLeave, strToolTip, sysPlanColor, btnDay34, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData34, strLeave, strToolTip, sysApplyColor, btnDay34, bEnabled)
                    End If
                Case "35"
                    If mMyDataReader("Type") = "P" Then
                        FillDetail2_Temp(lblPlanData35, strLeave, strToolTip, sysPlanColor, btnDay35, bEnabled)
                    Else
                        FillDetail2_Temp(lblApplyData35, strLeave, strToolTip, sysApplyColor, btnDay35, bEnabled)
                    End If
            End Select
        End While

        mMyDataReader.Close()



    End Sub
    Private Sub FillDetail2(ByRef lblData As Label, ByVal strLeave As String, ByVal strToolTip As String, ByVal sysColor As Drawing.Color, ByRef btnDay As Button, ByVal bEnabled As Boolean)

        Dim strType As String = ""

        If btnDay.Enabled = False Then
            bEnabled = False
        End If
        lblData.Text = strLeave
        lblData.ToolTip = strToolTip
        lblData.BackColor = sysColor

        If lblData.Text <> "" Then
            Select Case lblData.ID
                Case lblPlanData1.ID, lblPlanData2.ID, lblPlanData3.ID, lblPlanData4.ID, lblPlanData5.ID, lblPlanData6.ID, lblPlanData7.ID, _
                     lblPlanData8.ID, lblPlanData9.ID, lblPlanData10.ID, lblPlanData11.ID, lblPlanData12.ID, lblPlanData13.ID, lblPlanData14.ID, _
                     lblPlanData15.ID, lblPlanData16.ID, lblPlanData17.ID, lblPlanData18.ID, lblPlanData19.ID, lblPlanData20.ID, lblPlanData21.ID, _
                     lblPlanData22.ID, lblPlanData23.ID, lblPlanData24.ID, lblPlanData25.ID, lblPlanData26.ID, lblPlanData27.ID, lblPlanData28.ID, _
                     lblPlanData29.ID, lblPlanData30.ID, lblPlanData31.ID, lblPlanData32.ID, lblPlanData33.ID, lblPlanData34.ID, lblPlanData35.ID

                    strType = "P"
                Case Else
                    strType = "A"
            End Select
        End If

        If lblData.Text <> "" And cboType.SelectedValue = strType Then
            If bEnabled Then
                If bIsHoliday(btnDay) Then
                    SetSingleDayStatus(btnDay, bEnabled, sysEnabledForeColour, sysHolidayEnabledBgColour, bEnabled)
                ElseIf bIsWeekend(btnDay) Then
                    SetSingleDayStatus(btnDay, bEnabled, sysEnabledForeColour, sysWeekEndEnabledBgColour, bEnabled)
                Else
                    SetSingleDayStatus(btnDay, bEnabled, sysEnabledForeColour, sysWeekDayEnabledBgColour, bEnabled)
                End If
            Else
                SetSingleDayStatus(btnDay, bEnabled, sysDisabledForeColour, sysDisabledBgColour, bEnabled)
            End If
        End If



        'End If


    End Sub
    Private Sub FillDetail2_Temp(ByRef lblData As Label, ByVal strLeave As String, ByVal strToolTip As String, ByVal sysColor As Drawing.Color, ByRef btnDay As Button, ByVal bEnabled As Boolean)

        Dim strType As String = ""

        If strLeave <> "" Then lblData.Text = strLeave
        'lblData.ToolTip = strToolTip
        lblData.BackColor = sysColor

        If lblData.Text <> "" Then
            Select Case lblData.ID
                Case lblPlanData1.ID, lblPlanData2.ID, lblPlanData3.ID, lblPlanData4.ID, lblPlanData5.ID, lblPlanData6.ID, lblPlanData7.ID, _
                     lblPlanData8.ID, lblPlanData9.ID, lblPlanData10.ID, lblPlanData11.ID, lblPlanData12.ID, lblPlanData13.ID, lblPlanData14.ID, _
                     lblPlanData15.ID, lblPlanData16.ID, lblPlanData17.ID, lblPlanData18.ID, lblPlanData19.ID, lblPlanData20.ID, lblPlanData21.ID, _
                     lblPlanData22.ID, lblPlanData23.ID, lblPlanData24.ID, lblPlanData25.ID, lblPlanData26.ID, lblPlanData27.ID, lblPlanData28.ID, _
                     lblPlanData29.ID, lblPlanData30.ID, lblPlanData31.ID, lblPlanData32.ID, lblPlanData33.ID, lblPlanData34.ID, lblPlanData35.ID

                    strType = "P"
                Case Else
                    strType = "A"
            End Select
        End If

        If strLeave <> "" And cboType.SelectedValue = strType Then
            'If bEnabled Then
            If bIsHoliday(btnDay) Then
                SetSingleDayStatus(btnDay, bEnabled, sysTempForeColour, sysTempBgColour, bEnabled)
            ElseIf bIsWeekend(btnDay) Then
                SetSingleDayStatus(btnDay, bEnabled, sysTempForeColour, sysTempBgColour, bEnabled)
            Else
                SetSingleDayStatus(btnDay, bEnabled, sysTempForeColour, sysTempBgColour, bEnabled)
            End If
            'Else
            '    SetSingleDayStatus(btnDay, bEnabled, sysDisabledForeColour, sysDisabledBgColour, bEnabled)
            'End If
        End If



        'End If


    End Sub
    Private Sub FillEntitlement()

        'lblLeaveType.Text = "-"
        lblEnt.Text = "-"
        lblPending.Text = "-"
        lblApproved.Text = "-"
        lblBal.Text = "-"

        'If cboLeave.SelectedValue = "AL" Or cboLeave.SelectedValue = "OL" Or cboLeave.SelectedValue = "MC" Then
        mMyCommand.CommandText = "sp_web_ls_Sel_StaffLeave_temp '" & mstrEmpID & "'," & cboYear.SelectedValue & "," & _
                                 cboMonth.SelectedValue & ",'" & "" & "','" & cboLeave.SelectedValue & "','" & "ENT" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read() Then
            lblCutOff.Text = "Period: " & mMyDataReader("Cutoff1") & " - " & mMyDataReader("Cutoff2")

            If Val(mMyDataReader("Ent")) <> 99 Then
                lblEnt.Text = Val(mMyDataReader("Ent"))
                lblBal.Text = Val(mMyDataReader("Bal"))
            End If

            lblPending.Text = Val(mMyDataReader("Pending"))
            lblApproved.Text = Val(mMyDataReader("Approved"))

            If cboLeave.SelectedValue <> "AL" And cboLeave.SelectedValue <> "OF" And cboLeave.SelectedValue <> "MC" And cboLeave.SelectedValue <> "EHL" And cboLeave.SelectedValue <> "EAL" And cboLeave.SelectedValue <> "CAL" And cboLeave.SelectedValue <> "CHL" And cboLeave.SelectedValue <> "CML" And cboLeave.SelectedValue <> "HL" And cboLeave.SelectedValue <> "WL" And cboLeave.SelectedValue <> "CL" Then
                lblEnt.Text = "-"
                lblBal.Text = "-"
            End If

            'If cboLeave.SelectedValue = "OT" Then
            '    lblPending.Text = "-"
            '    lblApproved.Text = "-"
            'End If
        End If
        'End If
        mMyDataReader.Close()

        mMyCommand.CommandText = "sp_web_ls_Sel_StaffLeave_temp '" & mstrEmpID & "'," & cboYear.SelectedValue & "," & cboMonth.SelectedValue & ",'" & _
                                  "" & "','" & cboLeave.SelectedValue & "','" & "PENDING_EXIST" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read() Then
            miAllPending = Val(mMyDataReader("Pending"))
        End If
        mMyDataReader.Close()

    End Sub

    Private Sub FillEmail()

        mMyCommand.CommandText = "sp_ls_email_LeaveApplication '" & "" & "','" & mstrEmpID & "','" & "" & "'," & "0" & "," & "0" & ",'" & "EMAIL_NAME" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        'mMyCommand.CommandText = "sp_web_ls_Sel_StaffLeave '" & mstrEmpID & "'," & cboYear.SelectedValue & "," & cboMonth.SelectedValue & ",'" & _
        '                        "" & "','" & cboLeave.SelectedValue & "','" & "EMAIL_NAME" & "'"
        'mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read() Then
            lblEmailTo.Text = mMyDataReader("To")
            lblEmailTo.ToolTip = lblEmailTo.Text
            lblEmailCc.Text = mMyDataReader("CC")
            lblEmailCc.ToolTip = lblEmailCc.Text
        End If
        mMyDataReader.Close()

    End Sub
    Private Sub FillHoliday()

        Dim lstItem As ListItem

        lstHoliday.Items.Clear()

        mMyCommand.CommandText = "sp_ls_sel_Holiday " & cboYear.SelectedValue & "," & cboMonth.SelectedValue & ",'" & mstrEmpID & "','" & "EMP" & "'"

        mMyDataReader = mMyCommand.ExecuteReader

        While mMyDataReader.Read()
            lstItem = New ListItem
            lstItem.Text = mMyDataReader("holiday")
            lstItem.Value = mMyDataReader("date")
            lstHoliday.Items.Add(lstItem)
        End While
        mMyDataReader.Close()

    End Sub

    Private Sub FillOffset()

        lstOffsetLeave.Items.Clear()
        Dim totalday As Double = 0

        mMyCommand.CommandText = "sp_web_ls_Sel_StaffLeave_temp '" & mstrEmpID & "'," & cboOffsetYear.SelectedValue & "," & cboMonth.SelectedValue & ",'" & _
                                "" & "','" & cboLeave.SelectedValue & "','" & "OFFSET" & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        While mMyDataReader.Read()
            lstOffsetLeave.Items.Add(mMyDataReader("date"))
            totalday = totalday + mMyDataReader("Days")
        End While
        mMyDataReader.Close()

        lblTotalOffset.Text = totalday & " Day(s)"

    End Sub
    Public Sub AddDeleteTransaction(ByVal iDate As Integer)
        Dim ssql1 As String


        'If txtReason.Enabled Then
        '    If Trim(txtReason.Text) = "" Then
        '        lblRequired.Visible = True

        '        Exit Sub
        '    Else
        '        lblRequired.Visible = False
        '    End If
        'Else
        '    lblRequired.Visible = False
        'End If

        Dim strDestination As String


        If txtDestination.Enabled Then
            If Trim(txtDestination.Text) = "" Then
                txtDestination.BackColor = Drawing.Color.LightSalmon

                txtDestination.Focus()

                If txtReason.Enabled Then
                    If Trim(txtReason.Text) = "" Then
                        txtReason.BackColor = Drawing.Color.LightSalmon
                    End If
                End If

                Exit Sub
            End If
        End If

        If txtReason.Enabled Then
            If Trim(txtReason.Text) = "" Then
                txtReason.BackColor = Drawing.Color.LightSalmon
                txtReason.Focus()

                Exit Sub
            End If
        End If

        Dim iEnt, iBalance As Decimal
        Dim strDate As String

        iDate = iDate

        strDate = iDate.ToString & "/" & cboMonth.SelectedValue.ToString & "/" & cboYear.SelectedValue

        mMyCommand.CommandText = "sp_web_ls_Sel_StaffLeave_temp '" & mstrEmpID & "'," & cboYear.SelectedValue & "," & _
                                   cboMonth.SelectedValue & ",'" & strDate & "','" & cboLeave.SelectedValue & "','" & "ENT2" & "'"
        mMyDataReader = mMyCommand.ExecuteReader


        If mMyDataReader.Read() Then
            iEnt = Val(mMyDataReader("Ent"))
            iBalance = Val(mMyDataReader("Bal"))
        End If
        mMyDataReader.Close()



        If iEnt = 99 Or iBalance > 0 Or (cboLeave.SelectedValue <> "AL" And cboLeave.SelectedValue <> "CAL" And cboLeave.SelectedValue <> "EAL" And cboLeave.SelectedValue <> "CML" And cboLeave.SelectedValue <> "OF" And cboLeave.SelectedValue <> "MC") Then



            mMyCommand.CommandText = "sp_ls_web_InsDel_LeaveApplication '" & mstrEmpID & "','" & cboType.SelectedValue & "','" & strDate & "','" & _
                                     cboLeave.SelectedValue & "','" & cboPeriod.SelectedValue & "','" & Replace(txtReason.Text, "'", "''") & "','" & _
                                     Replace(txtDestination.Text, "'", "''") & "','" & Session("EmpID") & "','" & Now & _
                                     "','" & "" & "','" & "DEL" & "'"

            mMyDataReader = mMyCommand.ExecuteReader
            mMyDataReader.Close()

            If cboType.SelectedValue = "P" Then
                ssql1 = "Exec sp_ls_web_InsDel_LeaveApplication '" & mstrEmpID & "','" & cboType.SelectedValue & "','" & strDate & "','" & _
                                        cboLeave.SelectedValue & "','" & cboPeriod.SelectedValue & "','" & Replace(txtReason.Text, "'", "''") & "','" & _
                                        Replace(txtDestination.Text, "'", "''") & "','" & Session("EmpID") & "','" & Now & _
                                        "','" & "" & "','" & "DEL2" & "'"
                mySQL.ExecuteSQL(ssql1, Session("Company").ToString, Session("EmpID").ToString)

            End If

            If cboLeave.SelectedValue <> "C" Then
                If Trim(txtDestination.Text) = "" Then
                    strDestination = "---"
                Else
                    strDestination = Replace(Trim(txtDestination.Text), "'", "''")
                End If

                mMyCommand.CommandText = "sp_ls_web_InsDel_LeaveApplication '" & mstrEmpID & "','" & cboType.SelectedValue & "','" & strDate & "','" & _
                                        cboLeave.SelectedValue & "','" & cboPeriod.SelectedValue & "','" & Replace(txtReason.Text, "'", "''") & "','" & _
                                        strDestination & "','" & Session("EmpID") & "','" & Now & "','" & _
                                        "P" & "','" & "ADD" & "'"
                mMyDataReader = mMyCommand.ExecuteReader
            End If
            mMyDataReader.Close()

            'txtDestination.Text = ""
            'txtReason.Text = ""
        Else
            Dim strScript As String

            strScript = "<script language=JavaScript>"
            strScript += "alert(""" & "No balance, application not done." & """);"
            strScript += "</script>"

            If (Not ClientScript.IsStartupScriptRegistered("balance")) Then
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "balance", strScript)
            End If

        End If

    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        mMyConnection.ConnectionString = mySQL.GetConnectionString()
        mMyConnection.Open()
        mMyCommand.Connection = mMyConnection
        mMyCommand2.Connection = mMyConnection

    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'btnDay29.Attributes.Add("onClick", "return confirm('Proceed');")

        'lnkbtnLeaveSchedule.Attributes.Add("onclick", "window.close();")

        'Login1.Attributes.Add("onclick", "window.close();")

        Dim iYear, iMonth, iDay As Integer
        Dim lstItem As New ListItem

        If Session("EmpID") = "" Then
            If Page.ClientQueryString = "" Then
                Response.Redirect("../Global/SessionTimeOut.aspx")
            Else
                Session("EmpID") = MyKajima.DecryptedText(Strings.Mid(Page.Request.RawUrl, Strings.InStr(Page.Request.RawUrl, "?") + 1))
            End If

            'Response.Write(Page.ClientQueryString & "," & DecryptedPassedValue(Page.ClientQueryString))
        End If

        If Session("EmpID") <> "" Then
            Session("PreviousPage") = Request.RawUrl

            mMyCommand.CommandText = "select empname from is_empmaster where empid = '" & Session("EmpID") & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            If mMyDataReader.Read Then
                Session("username") = mMyDataReader("empname")
            End If
            mMyDataReader.Close()

            lblUser.Text = Session("username")

            If chkEmp.Checked Then
                mstrEmpID = cboEmp.SelectedValue
            Else
                mstrEmpID = Session("EmpID")
            End If

            mMyCommand.CommandText = "select empname from is_empmaster where empid = '" & mstrEmpID & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            If mMyDataReader.Read Then
                mstrEmpName = mMyDataReader("empname")
            End If
            mMyDataReader.Close()

            iYear = Year(Now)
            iMonth = Month(Now)
            iDay = Day(Now)

            If Not IsPostBack Then
                EmailSetting()

                cboYear.Items.Add(iYear - 1)
                cboYear.Items.Add(iYear)
                cboYear.Items.Add(iYear + 1)

                cboOffsetYear.Items.Add(iYear - 1)
                cboOffsetYear.Items.Add(iYear)
                cboOffsetYear.Items.Add(iYear + 1)

                mMyCommand.CommandText = "select IDDesc, ID from sa_reference where type = 'LeavePeriod' order by seq"
                mMyDataReader = mMyCommand.ExecuteReader

                While mMyDataReader.Read()
                    lstItem = New ListItem

                    If mMyDataReader("ID") <> "N" Then
                        lstItem.Text = mMyDataReader("ID") & " | " & mMyDataReader("IDDesc")
                    Else
                        lstItem.Text = mMyDataReader("IDDesc")
                    End If

                    lstItem.Value = mMyDataReader("ID")
                    cboPeriod.Items.Add(lstItem)
                End While
                mMyDataReader.Close()

                cboType.SelectedIndex = 1
                cboYear.SelectedValue = iYear
                cboMonth.SelectedValue = iMonth

                cboOffsetYear.SelectedValue = iYear
            End If

            If cboType.SelectedValue = "P" Or cboLeave.SelectedValue = "C" Then
                txtReason.Enabled = False
                txtReason.Text = ""

                txtDestination.Enabled = False
                txtDestination.Text = ""
                'lblRequired.Visible = False
            Else
                txtReason.Enabled = True
                txtReason.Focus()
            End If

            If cboType.SelectedValue = "A" And (cboLeave.SelectedValue = "BT" Or cboLeave.SelectedValue = "HT" Or cboLeave.SelectedValue = "EHL") Then
                txtDestination.Focus()
                txtDestination.Enabled = True
            Else
                txtDestination.Enabled = False
            End If

            txtReason.BackColor = Drawing.Color.White
            txtDestination.BackColor = Drawing.Color.White
        End If

    End Sub
    Private Sub EmailSetting()

        MyKajima.SmtpMail.Port = mySQL.GetConnectionStringDetails(clsSQL.SQLInfo._MailPort)
        MyKajima.SmtpMail.Host = mySQL.GetConnectionStringDetails(clsSQL.SQLInfo._MailServer)
        MyKajima.sysNetworkCredential.UserName = mySQL.GetConnectionStringDetails(clsSQL.SQLInfo._MailUser)
        MyKajima.sysNetworkCredential.Password = mySQL.GetConnectionStringDetails(clsSQL.SQLInfo._MailPass)
        MyKajima.SmtpMail.Credentials = MyKajima.sysNetworkCredential

    End Sub


    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        Dim strType As String = ""

        Dim iYear, iMonth, iDay As Integer
        Dim lstItem As New ListItem

        iYear = Year(Now)
        iMonth = Month(Now)
        iDay = Day(Now)


        If hfEmp.Value <> mstrEmpID Then
            cboLeave.Items.Clear()

            mMyCommand.CommandText = "sp_sa_Sel_LeaveType '" & mstrEmpID & "','" & "Web_Apply" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            'mMyCommand.CommandText = "select LeaveDesc, LeaveID from sa_leavetype where leaveid <> 'ET' order by seq"
            'mMyDataReader = mMyCommand.ExecuteReader

            While mMyDataReader.Read()
                lstItem = New ListItem

                lstItem.Text = mMyDataReader("LeaveID") & " | " & mMyDataReader("LeaveDesc")
                lstItem.Value = mMyDataReader("LeaveID")
                cboLeave.Items.Add(lstItem)
            End While
            mMyDataReader.Close()

            lstItem = New ListItem
            lstItem.Text = "<Cancel>"
            lstItem.Value = "C"
            cboLeave.Items.Add(lstItem)
        End If

        hfEmp.Value = mstrEmpID

        If IsDate("13" & "/" & iMonth & "/" & iYear) Then
            ssql100 = "Exec sp_Web_ChkLockTranssaction '" & Session("Company").ToString & "','" & mstrEmpID & "','MONTHLY','CHK'"
        Else
            ssql100 = "Exec sp_Web_ChkLockTranssaction '" & Session("Company").ToString & "','" & mstrEmpID & "','MONTHLY','CHK2'"
        End If
        myDS = mySQL.ExecuteSQL(ssql100, Session("Company").ToString, Session("EmpID").ToString)

        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                LockDate = myDS.Tables(0).Rows(0).Item(0).ToString
                UnLockStart = myDS.Tables(0).Rows(0).Item(1).ToString
                UnLockEnd = myDS.Tables(0).Rows(0).Item(2).ToString
            End If

        End If

        GetEmpOnBehalf()
        FillHoliday()
        FillOffset()
        FillEntitlement()
        FillDays()
        CheckPublicHoliday()
        FillDetail()
        FillDetail_Temp()
        FillEmail()

        If cboType.SelectedValue = "P" Then
            If CDate("01/" & cboMonth.SelectedValue.ToString & "/" & cboYear.SelectedValue.ToString) <= CDate("01/" & iMonth.ToString & "/" & iYear.ToString) Then
                SetAllDayStatus(False)
                btnApproval.Enabled = False
            Else
                btnApproval.Enabled = True
            End If

            btnApproval.Text = mstrPlan


            strType = "SUBMIT_PLAN"

            lblTo.Visible = False
            lblCc.Visible = False

            lblEmailTo.Visible = False
            lblEmailCc.Visible = False
        Else
            btnApproval.Text = mstrApprove

            If miAllPending > 0 Then
                btnApproval.Enabled = True
                btnApproval.Text = btnApproval.Text & " (" & miAllPending & ")"
            Else
                btnApproval.Enabled = False
            End If

            'lblSubmit.Text = ""
            strType = "SUBMIT_APPROVAL"

            lblTo.Visible = True
            lblCc.Visible = True

            lblEmailTo.Visible = True
            lblEmailCc.Visible = True
        End If

        'If cboType.SelectedValue = "P" Then
        '    mMyCommand.CommandText = "sp_web_ls_Sel_StaffLeave_temp '" & mstrEmpID & "'," & cboYear.SelectedValue & "," & cboMonth.SelectedValue & ",'" & _
        '                            "" & "','" & cboLeave.SelectedValue & "','" & strType & "'"
        'Else
        '    mMyCommand.CommandText = "sp_web_ls_Sel_StaffLeave_temp '" & mstrEmpID & "'," & "1900" & "," & "1" & ",'" & _
        '                             "" & "','" & cboLeave.SelectedValue & "','" & strType & "'"
        'End If
        'mMyDataReader = mMyCommand.ExecuteReader

        'If mMyDataReader.Read Then
        '    If cboType.SelectedValue = "P" Then
        '        lblSubmit.Text = "Planned On " & mMyDataReader("Date").ToString & ""
        '    ElseIf cboType.SelectedValue = "A" Then
        '        If miAllPending > 0 Then
        '            lblSubmit.Text = "Sent On " & mMyDataReader("Date").ToString & ""
        '        Else
        '            lblSubmit.Text = ""
        '        End If
        '    End If
        'Else
        '    lblSubmit.Text = ""
        'End If
        'mMyDataReader.Close()



        'txtReason.Text = ""

    End Sub
    Private Sub GetEmpOnBehalf()

        Dim lstItem, lstSelectedItem As New ListItem
        Dim iLoop As Integer

        If cboEmp.SelectedIndex <> -1 Then
            lstSelectedItem = cboEmp.Items(cboEmp.SelectedIndex)
        End If

        cboEmp.Items.Clear()

        mMyCommand.CommandText = "sp_is_sel_EmpOnBehalf '" & Session("EmpID") & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        While mMyDataReader.Read()
            lstItem = New ListItem
            lstItem.Text = mMyDataReader("empname")
            lstItem.Value = mMyDataReader("OnBehalf")
            cboEmp.Items.Add(lstItem)
        End While
        mMyDataReader.Close()

        For iLoop = 0 To cboEmp.Items.Count - 1
            If cboEmp.Items(iLoop).Value = lstSelectedItem.Value Then
                cboEmp.Items(iLoop).Selected = True
                Exit For
            End If
        Next

        If cboEmp.Items.Count > 0 Then
            chkEmp.Visible = True
            lblEmp.Visible = True
            cboEmp.Visible = True
        Else
            chkEmp.Checked = False
            chkEmp.Visible = False
            lblEmp.Visible = False
            cboEmp.Visible = False
        End If

        If chkEmp.Checked Then
            cboEmp.Enabled = True

            If cboEmp.SelectedIndex = -1 Then cboEmp.SelectedIndex = 0
        Else
            cboEmp.Enabled = False

            If cboEmp.Items.Count > 0 Then
                For iLoop = 0 To cboEmp.Items.Count - 1
                    cboEmp.Items(iLoop).Enabled = False
                Next
                cboEmp.SelectedIndex = -1
            End If
        End If


    End Sub

    Private Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload

        mMyConnection.Close()

    End Sub

    Private Sub btnDay1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay1.Click

        AddDeleteTransaction(btnDay1.Text)

    End Sub

    Private Sub btnDay10_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay10.Click

        AddDeleteTransaction(btnDay10.Text)

    End Sub

    Private Sub btnDay11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay11.Click

        AddDeleteTransaction(btnDay11.Text)

    End Sub


    Private Sub btnDay2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay2.Click

        AddDeleteTransaction(btnDay2.Text)

    End Sub


    Private Sub btnDay3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay3.Click

        AddDeleteTransaction(btnDay3.Text)

    End Sub


    Private Sub btnDay8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay8.Click

        AddDeleteTransaction(btnDay8.Text)

    End Sub

    Private Sub btnDay4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay4.Click

        AddDeleteTransaction(btnDay4.Text)

    End Sub

    Private Sub btnDay5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay5.Click

        AddDeleteTransaction(btnDay5.Text)

    End Sub

    Private Sub btnDay6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay6.Click

        AddDeleteTransaction(btnDay6.Text)

    End Sub

    Private Sub btnDay7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay7.Click

        AddDeleteTransaction(btnDay7.Text)

    End Sub

    Private Sub btnDay9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay9.Click

        AddDeleteTransaction(btnDay9.Text)

    End Sub

    Protected Sub btnApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApproval.Click

        Submit()

    End Sub
    Private Sub Submit()

        Dim strDate As String, ssql As String

        If cboType.SelectedValue = "P" Then
            strDate = "01" & "/" & cboMonth.SelectedValue & "/" & cboYear.SelectedValue
        Else
            strDate = "01" & "/" & "01" & "/" & "1900"
        End If

        ssql = "Exec sp_ls_web_InsDel_LeaveApplication '" & mstrEmpID & "','" & cboType.SelectedValue & "','" & strDate & "','" & _
                                    "" & "','" & "" & "','" & Replace(txtReason.Text, "'", "''") & "','" & Replace(txtDestination.Text, "'", "''") & "','" & Session("EmpID") & "','" & Now & "','" & "" & _
                                    "','" & "SUBMIT" & "'"
        mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)

        If cboType.SelectedValue = "A" Then
            SendEmail()
        End If

        txtReason.Text = ""
        txtDestination.Text = ""

    End Sub
    Private Sub SendEmail()

        Dim strURL As String = ""
        Dim strSupervisorID As String = ""
        Dim strEmpName As String = ""
        Dim strHtmlBody As String = ""

        Dim strMessage As String = ""
        Dim strScript As String = ""

        strURL = mySQL.GetConnectionStringDetails(clsSQL.SQLInfo._URLAddress)
        mSmtpMail.Port = mySQL.GetConnectionStringDetails(clsSQL.SQLInfo._MailPort)
        mSmtpMail.Host = mySQL.GetConnectionStringDetails(clsSQL.SQLInfo._MailServer)
        mSmtpMail.Credentials = MyKajima.sysNetworkCredential
        mSmtpMsg.IsBodyHtml = True

        'mSmtpMsg.From = New System.Net.Mail.MailAddress("wc.chung@kajima.com.my", "Chung Way Chong")
        'mSmtpMsg.To.Add(New System.Net.Mail.MailAddress("wc.chung@kajima.com.my", "Chung Way Chong"))
        'mSmtpMsg.CC.Add(New System.Net.Mail.MailAddress("wc.chung@kajima.com.my", "Chung Way Chong"))


        mSmtpMsg.To.Clear()
        mSmtpMsg.CC.Clear()
        mSmtpMsg.Bcc.Clear()


        'mMyCommand.CommandText = "sp_web_ls_Sel_StaffLeave '" & mstrEmpID & "'," & cboYear.SelectedValue & "," & _
        '                        cboMonth.SelectedValue & ",'" & "" & "','" & cboLeave.SelectedValue & "','" & "SEND_EMAIL" & "'"
        'mMyDataReader = mMyCommand.ExecuteReader



        mSmtpMsg.From = New System.Net.Mail.MailAddress(mySQL.GetConnectionStringDetails(clsSQL.SQLInfo._MailUser), "eSchedule")
        mSmtpMsg.Bcc.Add(New System.Net.Mail.MailAddress(mySQL.GetConnectionStringDetails(clsSQL.SQLInfo._MailUser), "eSchedule"))

        mMyCommand.CommandText = "select supervisorid from ls_empapproval where [type] = 'To' and empid = '" & "" & mstrEmpID & "'"
        mMyDataReader = mMyCommand.ExecuteReader

        If mMyDataReader.Read() Then
            strSupervisorID = mMyDataReader("supervisorid")
        End If
        mMyDataReader.Close()

        If strSupervisorID.ToUpper <> "" Then
            mMyCommand.CommandText = "sp_ls_email_LeaveApplication '" & "" & "','" & mstrEmpID & "','" & "" & "'," & "0" & "," & "0" & ",'" & "SEND_EMAIL" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            While mMyDataReader.Read()
                If mMyDataReader("type").ToString.ToUpper = "FROM" Then
                    mSmtpMsg.CC.Add(New System.Net.Mail.MailAddress(mMyDataReader("email"), mMyDataReader("empname")))
                ElseIf mMyDataReader("type").ToString.ToUpper = "TO" Then
                    strSupervisorID = mMyDataReader("empid")


                    mSmtpMsg.To.Add(New System.Net.Mail.MailAddress(mMyDataReader("email"), mMyDataReader("empname")))
                Else
                    mSmtpMsg.CC.Add(New System.Net.Mail.MailAddress(mMyDataReader("email"), mMyDataReader("empname")))
                End If
            End While
            mMyDataReader.Close()

            mSmtpMsg.Body = "<html>" & _
                             "<body>" & _
                             "<table>" & _
                             "<tr>" & _
                             "<td>" & "Dear Sir/Madam," & "</td>" & _
                             "</tr>" & _
                             "<tr>" & _
                             "<td>&nbsp;</td>" & _
                             "</tr>" & _
                             "<tr>" & _
                             "<td>" & "Please refer to Pending Schedule Application below for <u>" & mstrEmpName & "</u>  for your approval." & "</td>" & _
                             "</tr>" & _
                             "</table>" & _
                             "<br/>" & _
                             "<table  border=""1""  cellspacing=""0"" cellpadding=""5"">" & _
                             "<tr>" & _
                             "<td colspan=""6"" align=""center"" style=""background-color: darkcyan; color:White; font-weight: bold"">" & "Schedule Application - Pending" & "</td>" & _
                             "</tr>" & _
                             "<tr>" & _
                            "<td align=""center"" style=""background-color: darkcyan; width:30px; color:White; font-weight: bold"">No.</td>" & _
                            "<td align=""center"" style=""background-color: darkcyan; width:175px; color:White; font-weight: bold"">From Date</td>" & _
                            "<td align=""center"" style=""background-color: darkcyan; width:175px; color:White; font-weight: bold"">To Date</td>" & _
                            "<td align=""center"" style=""background-color: darkcyan; width:100px; color:White; font-weight: bold"">Schedule</td>" & _
                            "<td align=""center"" style=""background-color: darkcyan; width:250px; color:White; font-weight: bold"">Destination</td>" & _
                            "<td align=""center"" style=""background-color: darkcyan; width:500px; color:White; font-weight: bold"">Reason / Purpose</td>" & _
                            "</tr>"

            mMyCommand.CommandText = "sp_ls_email_LeaveApplication '" & strSupervisorID & "','" & mstrEmpID & "','" & "P" & "'," & cboMonth.SelectedValue & "," & cboYear.SelectedValue & ",'" & "P" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            If mMyDataReader.HasRows Then
                While mMyDataReader.Read()
                    If Val(mMyDataReader("no")) Mod 2 = 0 Then
                        strHtmlBody = strHtmlBody & _
                               "<tr style=""font-size:smaller"">" & _
                            "<td align=""center"" style=""background-color: lavender"">" & mMyDataReader("no") & "</td>" & _
                            "<td align=""center"" style=""background-color: lavender"">" & mMyDataReader("startdate") & "</td>" & _
                            "<td align=""center"" style=""background-color: lavender"">" & mMyDataReader("enddate") & "</td>" & _
                            "<td align=""center"" style=""background-color: lavender"">" & mMyDataReader("leave") & "</td>" & _
                            "<td align=""center"" style=""background-color: lavender"">" & mMyDataReader("destination") & "</td>" & _
                            "<td style=""background-color: lavender"">" & mMyDataReader("reason") & "</td>" & _
                            "</tr>" & _
                            "<tr>" & _
                            "<td colspan=""6"">&nbsp;</td>" & _
                            "</tr>"


                    Else
                        strHtmlBody = strHtmlBody & _
                            "<tr style=""font-size:smaller"">" & _
                            "<td align=""center"" style=""background-color: honeydew"">" & mMyDataReader("no") & "</td>" & _
                            "<td align=""center"" style=""background-color: honeydew"">" & mMyDataReader("startdate") & "</td>" & _
                            "<td align=""center"" style=""background-color: honeydew"">" & mMyDataReader("enddate") & "</td>" & _
                            "<td align=""center"" style=""background-color: honeydew"">" & mMyDataReader("leave") & "</td>" & _
                            "<td align=""center"" style=""background-color: honeydew"">" & mMyDataReader("destination") & "</td>" & "</td>" & _
                            "<td style=""background-color: honeydew"">" & mMyDataReader("reason") & "</td>" & _
                            "</tr>" & _
                            "<tr>" & _
                            "<td colspan=""6"">&nbsp;</td>" & _
                            "</tr>"

                        '"<td colspan=""2"" align=""center"" style=""border: black thin solid; background-color: darkcyan; color:White; font-weight: bold"">Reason / Purpose</td>" & _
                        '        "<td colspan=""7"" style=""background-color: honeydew; border: black thin solid"">" & mMyDataReader("reason") & "</td>" & _


                    End If
                End While


                mSmtpMsg.Body = mSmtpMsg.Body & strHtmlBody
                mSmtpMsg.Body = mSmtpMsg.Body & _
                               "</table>" & _
                               "<table>" & _
                               "<tr>" & _
                               "<td>&nbsp;</td>" & _
                               "</tr>" & _
                               "<tr>" & _
                               "<td>" & "Kindly click on " & _
                               "<a href=""" & strURL & """ runat=""server"">" & strURL & "</a>" & _
                               " to approve." & _
                               "</td>" & _
                               "</tr>" & _
                               "<tr>" & _
                               "<td>&nbsp;</td>" & _
                               "</tr>" & _
                               "<tr>" & _
                               "<td>" & "Thank you." & "</td>" & _
                               "</tr>" & _
                               "<tr>" & _
                               "<td>&nbsp;</td>" & _
                               "</tr>" & _
                               "<tr>" & _
                               "<td>" & "eSchedule" & "</td>" & _
                               "</tr>" & _
                               "<tr>" & _
                               "<td>&nbsp;</td>" & _
                               "</tr>" & _
                               "<tr>" & _
                               "<td style=""font-size:smaller"">" & "This is computer generated email, reply is not required." & "</td>" & _
                               "</tr>" & _
                               "</td>" & _
                               "</table>" & _
                               "</body>" & _
                               "</html>"

                ' "<tr>" & _
                '"<td>" & "On behalf of " & mstrEmpName & "</td>" & _
                '"</tr>" & _
                mSmtpMsg.Subject = "Schedule Application for " & mstrEmpName & " - Pending"

                'mSmtpMsg.Attachments.Add(New System.Net.Mail.Attachment("D:\Leave Application.txt"))
                mSmtpMail.Send(mSmtpMsg)
            End If
            mMyDataReader.Close()

            strMessage = "Application has sent for approval."
            'finishes server processing, returns to client.

        Else
            mMyCommand.CommandText = "sp_ls_email_LeaveApplication '" & "" & "','" & mstrEmpID & "','" & "" & "'," & "0" & "," & "0" & ",'" & "SEND_EMAIL" & "'"
            mMyDataReader = mMyCommand.ExecuteReader

            While mMyDataReader.Read()
                If mMyDataReader("type") = "From" Then
                    mSmtpMsg.To.Add(New System.Net.Mail.MailAddress(mMyDataReader("email"), mMyDataReader("empname")))
                ElseIf mMyDataReader("type") = "To" Then
                    mSmtpMsg.CC.Add(New System.Net.Mail.MailAddress(mMyDataReader("email"), mMyDataReader("empname")))
                Else
                    mSmtpMsg.CC.Add(New System.Net.Mail.MailAddress(mMyDataReader("email"), mMyDataReader("empname")))
                End If
            End While
            mMyDataReader.Close()

            ssql100 = "Exec sp_ls_InsUpdDel_LeaveApproval '" & mstrEmpName & "','" & "Date" & "','" & "Leave" & "','" & _
                                     "Period" & "','" & "A" & " ','" & "Remark" & " ','" & "SA" & "','" & "AUTO_APPROVE" & "'"
            mySQL.ExecuteSQL(ssql100, Session("Company").ToString, Session("EmpID").ToString)

            Dim strStatus As String = ""
            Dim strSubject As String = ""
            Dim strHeader As String = ""
            Dim strColSupervisor As String = ""
            Dim strColDate As String = ""
            Dim strEmpID As String = ""

            strSubject = "Schedule Application for "
            strStatus = "approved."
            strHeader = "Schedule Application- Approved"
            strColSupervisor = "Approved By"
            strColDate = "Approved On"

            mSmtpMsg.Body = "<html>" & _
             "<body>" & _
             "<table>" & _
             "<tr>" & _
             "<td>" & "Dear Sir/Madam," & "</td>" & _
             "</tr>" & _
             "<tr>" & _
             "<td>&nbsp;</td>" & _
             "</tr>" & _
             "<tr>" & _
             "<td>" & "Please be informed that Schedule Application below has been " & strStatus & "</td>" & _
             "</tr>" & _
             "</table>" & _
             "<br/>" & _
             "<table  border=""1""  cellspacing=""0"" cellpadding=""5"">" & _
             "<tr>" & _
             "<td colspan=""6"" align=""center"" style=""background-color: darkcyan; color:White; font-weight: bold"">" & strHeader & "</td>" & _
             "</tr>" & _
             "<tr>" & _
            "<td align=""center"" style=""background-color: darkcyan; width:30px; color:White; font-weight: bold"">No.</td>" & _
            "<td align=""center"" style=""background-color: darkcyan; width:175px; color:White; font-weight: bold"">From Date</td>" & _
            "<td align=""center"" style=""background-color: darkcyan; width:175px; color:White; font-weight: bold"">To Date</td>" & _
            "<td align=""center"" style=""background-color: darkcyan; width:100px; color:White; font-weight: bold"">Schedule</td>" & _
            "<td align=""center"" style=""background-color: darkcyan; width:250px; color:White; font-weight: bold"">Destination</td>" & _
            "<td align=""center"" style=""background-color: darkcyan; width:500px; color:White; font-weight: bold"">Reason / Purpose</td>" & _
            "</tr>"

            mMyCommand.CommandText = "sp_ls_email_LeaveApplication '" & "SA" & "','" & mstrEmpID & "','" & "A" & "'," & _
                                  cboMonth.SelectedValue & "," & cboYear.SelectedValue & ",'" & "A" & " '"
            mMyDataReader = mMyCommand.ExecuteReader

            'If mMyDataReader.HasRows Then
            While mMyDataReader.Read()
                If Val(mMyDataReader("no")) Mod 2 = 0 Then
                    strHtmlBody = strHtmlBody & _
                            "<tr style=""font-size:smaller"">" & _
                            "<td align=""center"" style=""background-color: lavender"">" & mMyDataReader("no") & "</td>" & _
                            "<td align=""center"" style=""background-color: lavender"">" & mMyDataReader("startdate") & "</td>" & _
                            "<td align=""center"" style=""background-color: lavender"">" & mMyDataReader("enddate") & "</td>" & _
                            "<td align=""center"" style=""background-color: lavender"">" & mMyDataReader("leave") & "</td>" & _
                            "<td align=""center"" style=""background-color: lavender"">" & mMyDataReader("destination") & "</td>" & _
                            "<td style=""background-color: lavender"">" & mMyDataReader("reason") & "</td>" & _
                            "</tr>" & _
                            "<tr>" & _
                            "<td colspan=""6"">&nbsp;</td>" & _
                            "</tr>"
                Else
                    strHtmlBody = strHtmlBody & _
                            "<tr style=""font-size:smaller"">" & _
                            "<td align=""center"" style=""background-color: honeydew"">" & mMyDataReader("no") & "</td>" & _
                            "<td align=""center"" style=""background-color: honeydew"">" & mMyDataReader("startdate") & "</td>" & _
                            "<td align=""center"" style=""background-color: honeydew"">" & mMyDataReader("enddate") & "</td>" & _
                            "<td align=""center"" style=""background-color: honeydew"">" & mMyDataReader("leave") & "</td>" & _
                            "<td align=""center"" style=""background-color: honeydew"">" & mMyDataReader("destination") & "</td>" & "</td>" & _
                            "<td style=""background-color: honeydew"">" & mMyDataReader("reason") & "</td>" & _
                            "</tr>" & _
                            "<tr>" & _
                            "<td colspan=""6"">&nbsp;</td>" & _
                            "</tr>"
                End If
            End While
            mMyDataReader.Close()

            mSmtpMsg.Body = mSmtpMsg.Body & strHtmlBody
            mSmtpMsg.Body = mSmtpMsg.Body & _
                           "</table>" & _
                           "<table>" & _
                           "<tr>" & _
                           "<td>&nbsp;</td>" & _
                           "</tr>" & _
                           "<tr>" & _
                           "<td>" & "Thank you." & "</td>" & _
                           "</tr>" & _
                           "<tr>" & _
                           "<td>&nbsp;</td>" & _
                           "</tr>" & _
                           "<tr>" & _
                           "<td>" & "eSchedule" & "</td>" & _
                           "</tr>" & _
                           "<tr>" & _
                           "<td>&nbsp;</td>" & _
                           "</tr>" & _
                           "<tr>" & _
                           "<td style=""font-size:smaller"">" & "This is computer generated email, reply is not required." & "</td>" & _
                           "</tr>" & _
                           "</table>" & _
                           "</body>" & _
                           "</html>"
            mSmtpMsg.Subject = strSubject & mstrEmpName & " - Approved"



            'mSmtpMsg.Attachments.Add(New System.Net.Mail.Attachment("D:\Leave Application.txt"))
            mSmtpMail.Send(mSmtpMsg)
            'End If
            ssql100 = "Exec sp_ls_InsUpdDel_LeaveApproval '" & mstrEmpName & "','" & "Date" & "','" & "Leave" & "','" & _
                                        "Period" & "','" & "A" & "','" & "" & "','" & "SA" & "','" & "AUTO_UPD" & "'"
            mySQL.ExecuteSQL(ssql100, Session("Company").ToString, Session("EmpID").ToString)


            strMessage = "Application has sent for information."
        End If


        'finishes server processing, returns to client.
        strScript = "<script language=JavaScript>"
        strScript += "alert(""" & strMessage & """);"
        strScript += "</script>"

        If (Not ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript", strScript)
        End If


        'Response.Write(mSmtpMsg.From.Address)

    End Sub

    Private Sub btnDay12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay12.Click

        AddDeleteTransaction(btnDay12.Text)

    End Sub

    Private Sub btnDay13_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay13.Click

        AddDeleteTransaction(btnDay13.Text)

    End Sub

    Private Sub btnDay14_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay14.Click

        AddDeleteTransaction(btnDay14.Text)

    End Sub

    Private Sub btnDay15_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay15.Click

        AddDeleteTransaction(btnDay15.Text)

    End Sub

    Private Sub btnDay16_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay16.Click

        AddDeleteTransaction(btnDay16.Text)

    End Sub

    Private Sub btnDay17_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay17.Click

        AddDeleteTransaction(btnDay17.Text)

    End Sub

    Private Sub btnDay18_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay18.Click

        AddDeleteTransaction(btnDay18.Text)

    End Sub

    Private Sub btnDay19_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay19.Click

        AddDeleteTransaction(btnDay19.Text)

    End Sub

    Private Sub btnDay20_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay20.Click

        AddDeleteTransaction(btnDay20.Text)

    End Sub

    Private Sub btnDay21_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay21.Click

        AddDeleteTransaction(btnDay21.Text)

    End Sub

    Private Sub btnDay22_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay22.Click

        AddDeleteTransaction(btnDay22.Text)

    End Sub

    Private Sub btnDay23_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay23.Click

        AddDeleteTransaction(btnDay23.Text)

    End Sub

    Private Sub btnDay24_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay24.Click

        AddDeleteTransaction(btnDay24.Text)

    End Sub

    Private Sub btnDay25_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay25.Click

        AddDeleteTransaction(btnDay25.Text)

    End Sub

    Private Sub btnDay26_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay26.Click

        AddDeleteTransaction(btnDay26.Text)

    End Sub

    Private Sub btnDay27_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay27.Click

        AddDeleteTransaction(btnDay27.Text)

    End Sub

    Private Sub btnDay28_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay28.Click

        AddDeleteTransaction(btnDay28.Text)

    End Sub

    Private Sub btnDay29_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay29.Click

        AddDeleteTransaction(btnDay29.Text)

    End Sub

    Private Sub btnDay30_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay30.Click

        AddDeleteTransaction(btnDay30.Text)

    End Sub

    Private Sub btnDay31_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay31.Click

        AddDeleteTransaction(btnDay31.Text)

    End Sub

    Private Sub btnDay32_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay32.Click

        AddDeleteTransaction(btnDay32.Text)

    End Sub

    Private Sub btnDay33_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay33.Click

        AddDeleteTransaction(btnDay33.Text)

    End Sub

    Private Sub btnDay34_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay34.Click

        AddDeleteTransaction(btnDay34.Text)

    End Sub

    Private Sub btnDay35_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDay35.Click

        AddDeleteTransaction(btnDay35.Text)

    End Sub

    Private Sub cboPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriod.SelectedIndexChanged

        If cboPeriod.SelectedValue = "N" And cboLeave.SelectedValue <> "C" Then
            cboLeave.SelectedValue = "OTA"
        ElseIf cboLeave.SelectedValue = "OTA" Or cboLeave.SelectedValue = "OTL" Or cboLeave.SelectedValue = "OT" Then
            cboLeave.SelectedIndex = 0
        End If


    End Sub

    Private Sub cboLeave_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLeave.SelectedIndexChanged

        If cboLeave.SelectedValue = "OTL" Or cboLeave.SelectedValue = "OTA" Or cboLeave.SelectedValue = "OT" Then
            cboPeriod.SelectedValue = "N"
        ElseIf cboPeriod.SelectedValue = "N" Then
            cboPeriod.SelectedIndex = 0
        End If

        txtReason.Text = ""
        txtDestination.Text = ""

    End Sub
    Protected Sub lnkbtnLeaveApplication_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkbtnLeaveApplication.Click

        Response.Redirect("scheduleapplication.aspx", True)

    End Sub

    Protected Sub lblLeaveApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkbtnLeaveApproval.Click

        Response.Redirect("scheduleapproval.aspx", True)

    End Sub



    Public Sub New()

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub lnkbtnLeaveSchedule_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLeaveSchedule.Click

        Response.Redirect("viewschedule.aspx", True)

    End Sub

    Protected Sub chkEmp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEmp.CheckedChanged

        If chkEmp.Checked Then cboEmp.Focus()

    End Sub

    Private Sub lnkChangePwd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkChangePwd.Click

        Response.Redirect("changepwd.aspx", True)

    End Sub

    Protected Sub cboType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged



    End Sub

    Private Sub lnkLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogout.Click

        Session("EmpID") = ""
        Response.Redirect("../Global/SessionTimeOut.aspx")

    End Sub

    Private Sub lnkbtnLeaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLeaveReport.Click

        Response.Redirect("schedulereport.aspx", True)

    End Sub

    Protected Sub lnkMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkMain.Click

        Dim strURL As String
        Dim strCurrentDay, strCurrentMonth, strCurrentYear As String


        strCurrentDay = Now.Day.ToString
        strCurrentMonth = Now.Month.ToString
        strCurrentYear = Now.Year.ToString

        If strCurrentDay.Length = 1 Then strCurrentDay = "0" & strCurrentDay

        If strCurrentMonth.Length = 1 Then strCurrentMonth = "0" & strCurrentMonth

        strURL = "/home/main.aspx?" & MyKajima.EncryptedText(Session("EmpID"))

        Session("EmpID") = ""
        Response.Redirect(strURL, True)

    End Sub
End Class