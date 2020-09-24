Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.web.Configuration

Partial Class Pages_System_Manager_Pass_Control
    Inherits System.Web.UI.Page

#Region "Public Declaration"
    Private WithEvents mySQL As New clsSQL, myDS, myDS1, myDS2, myDS3, myDS4, myDS10 As New DataSet, mySetting As New clsGlobalSetting, myMsg As New clsMessage
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Public ssql As String, i As Integer, j As Integer, SearchByPage As Boolean, RecFound As Boolean, CountRecord As Integer
    Public AllowView, AllowInsert, AllowUpdate, AllowDelete, AllowPrint As Boolean
    Public _currentPageNumber As Integer, _totalPage As Double = 1, _totalRecords As Integer
    Public myDE As New DictionaryEntry, myHT As Hashtable
    Public myDataType As New clsGlobalSetting.DataType
    Dim strPath As String = "../../Images"
#End Region

#Region "Private Declaration"
    Private WithEvents myDT1 As New DataTable, myDT2 As New DataTable
    Dim myDR1 As DataRow, myDR2 As DataRow, rcPerPage As Integer, autonum As Integer
    Dim ssql1, ssql2, ssql3, ssql4 As String
    Dim rowPosition As String = "R_", rowPositionM As String = "RM_", panelPosition As String = "Display_"
    Dim buttonPosition1 As String = "B1_", buttonPosition2 As String = "B2_", buttonPosition3 As String = "B3_", labelPosition1 As String = "L1_"
    Dim logic As Boolean
    Dim grdItem As GridViewRow
    Dim chkSelected As CheckBox
    Dim dtSelectedDate As DateTime
    Dim stPeriod, stDateApplyOn, PrevLeaveID, LeaveID, iID, StopChecking, mstBalance As String
    Dim ApplicationEnd, ContinueLoop As Boolean
    Dim Day, TotalDay As Decimal
    Dim Err1, Err2, Err3, Err4, Err5, Err6, vDate1, vDate2, vDate3, vDate4, vDate5, vDate6, Message As String
    Dim vCount, vCounter, CountDay, CountSequence, ContinueCount As Integer
    Dim Action, DateApplyFor, strempid, CurrentDate, filterBy, DefaultPass As String
    Dim Counter, CurrentYear, SysYear As Integer
    Dim btnColourDef, btnColourAlt As String
#End Region

#Region "Page Setting"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Session.IsNewSession Then
            Response.Redirect("../Global/SessionTimeOut.aspx")
        End If

        If Not Page.IsPostBack Then
            Response.CacheControl = "no-cache"
            Response.AddHeader("Pragma", "no-cache")
            Response.Expires = -1

            If Session("ScreenWidth") = 0 Then
                Session("ScreenWidth") = "1024"
                Session("GVwidth") = Session("ScreenWidth") - 360
            End If
            If Session("ScreenHeight") = 0 Then
                Session("ScreenHeight") = "768"
                Session("GVheight") = (Session("ScreenHeight") / 2) - 50
            End If
            pnlTitle.Width = CInt(Session("GVwidth")) - 10
            PagePreload()
        End If

    End Sub

    Sub PagePreload()

        Session("Module") = "System_Manager"
        Session("action") = ""
        _currentPageNumber = 1
        Session("currentpage1") = _currentPageNumber
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")

        'bind into ddlOption_Type
        mySetting.GetDropdownlistValue(Form.ID, "OPTION_CHANGE_PASSWORD_ON_NEXT_LOGON", ddlChange_Pass)
        mySetting.GetDropdownlistValue(Form.ID, "OPTION_PASS_COMPLEXITY", ddlPass_Complexity)

        'get Page Title
        ssql = "exec sp_sa_GetPageTitle '" & Form.ID & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            lblTitle.Text = myDS.Tables(0).Rows(0).Item(0)
            lblTitle.CssClass = "wordstyle4"
        End If
        myDS = Nothing

        'get Company Code & Name
        lblCompany_Profile_ID.Text = "Organisation"
        txtCompany_Profile_ID.Text = Session("Company")
        ssql = "Select Name From Company_Profile Where Code ='" & Session("Company") & "'"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            txtCompany_Name.Text = myDS.Tables(0).Rows(0).Item(0)
        Else
            txtCompany_Name.Text = ""
        End If
        myDS = Nothing
        txtCompany_Profile_ID.ReadOnly = True
        txtCompany_Name.ReadOnly = True
        imgBtnCompany_Profile_ID.Visible = False
        lblCompany_Profile_ID.CssClass = "wordstyle3"

        'get panel part 3 name
        CheckBoxDefault.Text = "Filter by Employee"
        CheckBoxManual.Text = "Filter by User"

        'label for list box
        lbllstleft.Text = "Available List"
        lbllstright.Text = "Selected List"
        lbllstleft.CssClass = "wordstyle5"
        lbllstright.CssClass = "wordstyle6"

        'panel control & get panel part 2 name
        lnkBtnViewPassSetting.Enabled = True
        lnkBtnViewResetPass.Enabled = True
        lnkBtnViewResetPass_Click()
        pnlAuto.Visible = True
        pnlManual.Visible = True
        lblPassword.Enabled = False
        txtPassword.Enabled = False

        'get image 
        mySetting.GetImgUrl(imgCompany_Profile_ID, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgTypeUrl(imgBlank02, Session("Company").ToString, "Png", "blank.png")
        'mySetting.GetImgTypeUrl(imgBlank03, Session("Company").ToString, "Gif", "blank10.gif")

        'get image button
        mySetting.GetImgBtnUrl(imgBtnCompany_Profile_ID, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)

        mySetting.GetBtnImgUrl(imgBtnSubmit, Session("Company").ToString, btnColourDef, "btnSubmit.png")
        mySetting.GetBtnImgUrl(imgBtnClear, Session("Company").ToString, btnColourDef, "btnClear.png")

        mySetting.GetBtnImgUrl(imgBtnAddAll, Session("Company").ToString, btnColourDef, "addall.png")
        mySetting.GetBtnImgUrl(imgBtnAddItem, Session("Company").ToString, btnColourDef, "additem.png")
        mySetting.GetBtnImgUrl(imgBtnRemoveAll, Session("Company").ToString, btnColourDef, "removeall.png")
        mySetting.GetBtnImgUrl(imgBtnRemoveItem, Session("Company").ToString, btnColourDef, "removeitem.png")
        body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

    End Sub

    Sub BindList()
        ssql = "Exec sp_sa_getPassControlInfo '" & Session("Company") & "','" & Session("Module") & "','" & filterBy & "',''"
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Rows.Count > 0 Then
                lblPass_Length.Text = myDS.Tables(0).Rows(0).Item(1).ToString
                lblPass_Max_Length.Text = myDS.Tables(0).Rows(1).Item(1).ToString
                lblPass_Age.Text = myDS.Tables(0).Rows(2).Item(1).ToString
                lblPass_Complexity.Text = myDS.Tables(0).Rows(3).Item(1).ToString
                lblExpired_Alert.Text = myDS.Tables(0).Rows(4).Item(1).ToString
                lblPass_Level.Text = myDS.Tables(0).Rows(5).Item(1).ToString
                lblDefault_Pass.Text = myDS.Tables(0).Rows(6).Item(1).ToString

                txtPass_Length.Text = myDS.Tables(0).Rows(0).Item(2).ToString
                txtPass_Max_Length.Text = myDS.Tables(0).Rows(1).Item(2).ToString
                txtPass_Age.Text = myDS.Tables(0).Rows(2).Item(2).ToString
                ddlPass_Complexity.SelectedValue = myDS.Tables(0).Rows(3).Item(2).ToString
                txtExpired_Alert.Text = myDS.Tables(0).Rows(4).Item(2).ToString
                txtPass_Level.Text = myDS.Tables(0).Rows(5).Item(2).ToString
                txtDefault_Pass.Text = myDS.Tables(0).Rows(6).Item(2).ToString
                DefaultPass = txtDefault_Pass.Text

                lblPassword.Text = myDS.Tables(0).Rows(7).Item(1).ToString
                lblChange_Pass.Text = myDS.Tables(0).Rows(8).Item(1).ToString
            End If

            'Clear list box
            lstleft.Items.Clear()
            lstright.Items.Clear()
            If myDS.Tables(1).Rows.Count > 0 Then
                lstleft.DataSource = myDS.Tables(1)
                lstleft.DataTextField = "Name"
                lstleft.DataValueField = "Code"
                lstleft.DataBind()
            End If
        End If
        myDS = Nothing
    End Sub

#End Region

#Region "Panel Action"

    Protected Sub lnkBtnViewPassSetting_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnViewPassSetting.Click
        lnkBtnViewPassSetting_Click()
    End Sub

    Protected Sub lnkBtnViewResetPass_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnViewResetPass.Click
        lnkBtnViewResetPass_Click()
    End Sub

    Protected Sub CheckBoxDefault_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxDefault.CheckedChanged

        If CheckBoxDefault.Checked = True Then
            CheckBoxManual.Checked = False
            pnlAuto.Visible = True
            pnlManual.Visible = True
            lblPassword.Enabled = False
            txtPassword.Enabled = False
        Else
            pnlAuto.Visible = False
            pnlManual.Visible = False
        End If
        filterBy = "emp"
        lblresult.Text = ""
        BindList()

    End Sub

    Protected Sub CheckBoxManual_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxManual.CheckedChanged

        If CheckBoxManual.Checked = True Then
            CheckBoxDefault.Checked = False
            pnlAuto.Visible = True
            pnlManual.Visible = True
            lblPassword.Enabled = True
            txtPassword.Enabled = True
        Else
            pnlAuto.Visible = False
            pnlManual.Visible = False
        End If
        filterBy = "usr"
        lblresult.Text = ""
        BindList()

    End Sub

    Protected Sub imgBtnSubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnSubmit.Click

        Dim strUserCode As String = "'"
        Dim CodeCount As Integer = 0
        Dim htb As New Hashtable

        If lnkBtnClosePassSetting.Visible = True Then

            If CInt(txtPass_Length.Text) > CInt(txtPass_Max_Length.Text) Then
                lblresult.Text = lblPass_Length.Text & " should not be greater than " & lblPass_Max_Length.Text
                Exit Sub
            End If

            ssql = "Update Parameter Set String_Value1 = '" & txtPass_Age.Text & "' Where Company_Profile_Code = '" & Session("Company").ToString & "' And Module_Profile_Code = 'SYSTEM_MANAGER' And Code = 'PASS_AGE'"
            htb.Add(0, ssql)
            ssql = "Update Parameter Set String_Value1 = '" & txtPass_Length.Text & "' Where Company_Profile_Code = '" & Session("Company").ToString & "' And Module_Profile_Code = 'SYSTEM_MANAGER' And Code = 'PASS_LENGTH'"
            htb.Add(1, ssql)
            ssql = "Update Parameter Set String_Value1 = '" & ddlPass_Complexity.SelectedValue & "' Where Company_Profile_Code = '" & Session("Company").ToString & "' And Module_Profile_Code = 'SYSTEM_MANAGER' And Code = 'PASS_COMPLEXITY'"
            htb.Add(2, ssql)
            ssql = "Update Parameter Set String_Value1 = '" & txtExpired_Alert.Text & "' Where Company_Profile_Code = '" & Session("Company").ToString & "' And Module_Profile_Code = 'SYSTEM_MANAGER' And Code = 'PASS_EXPIRED_ALERT'"
            htb.Add(3, ssql)
            ssql = "Update Parameter Set String_Value1 = '" & txtPass_Level.Text & "' Where Company_Profile_Code = '" & Session("Company").ToString & "' And Module_Profile_Code = 'SYSTEM_MANAGER' And Code = 'PASS_LEVEL'"
            htb.Add(4, ssql)
            ssql = "Update Parameter Set String_Value1 = '" & txtDefault_Pass.Text & "' Where Company_Profile_Code = '" & Session("Company").ToString & "' And Module_Profile_Code = 'SYSTEM_MANAGER' And Code = 'DEFAULT_PASS'"
            htb.Add(5, ssql)
            ssql = "Update Parameter Set String_Value1 = '" & txtPass_Max_Length.Text & "' Where Company_Profile_Code = '" & Session("Company").ToString & "' And Module_Profile_Code = 'SYSTEM_MANAGER' And Code = 'PASS_MAX_LENGTH'"
            htb.Add(6, ssql)
            ssql = "Update Table_Field Set Length = '" & txtPass_Max_Length.Text & "' Where Table_Profile_Code In ('User_Profile','User_Profile_Card_Vw') And Code = 'PASSWORD'"
            htb.Add(7, ssql)
            mySQL.ExecuteSQLTransactionByHashtable(htb, "Update", Session("Company").ToString, Session("EmpID").ToString)
            myDS = Nothing
            lblresult.Text = "Data(s) update successfully..."

        ElseIf lnkBtnCloseResetPass.Visible = True Then
            CodeCount = lstright.Items.Count

            If txtPassword.Text = "" Then
                DefaultPass = txtDefault_Pass.Text
            Else
                DefaultPass = txtPassword.Text
            End If

            If CodeCount <> 0 Then
                If CheckBoxDefault.Checked = True Then
                    For i = 0 To CodeCount - 1
                        strUserCode = strUserCode & lstright.Items(i).Value.ToString & "','"
                    Next
                    strUserCode = Left(strUserCode, Len(strUserCode) - 2)
                    ssql = "Delete From User_Profile Where Company_Profile_code = '" & Session("Company").ToString & "' And Code In (" & strUserCode & _
                           "); Delete From User_Security Where Company_Profile_code = '" & Session("Company").ToString & "' And User_Profile_Code = " & strUserCode
                    myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                    myDS = Nothing
                    lblresult.Text = "Data(s) update successfully..."
                ElseIf CheckBoxManual.Checked = True Then
                    For i = 0 To CodeCount - 1
                        ssql = "Exec sp_sa_validateSystemLogin 'Get_login_Info','" & Session("Company") & "','','" & DefaultPass & "','',''"
                        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
                        If myDS.Tables.Count > 0 Then
                            If myDS.Tables(0).Rows.Count > 0 Then
                                ssql1 = "Update User_Profile Set Password = '" & Replace(myDS.Tables(0).Rows(0).Item(7).ToString(), "'", "' + '''' + '") & "', Password_Last_Update_Date = '" & _
                                myDS.Tables(0).Rows(0).Item(3).ToString() & "', Last_Login_Date_Time = '" & myDS.Tables(0).Rows(0).Item(6).ToString() & _
                                "', Password_Due_Day = '" & myDS.Tables(0).Rows(0).Item(4).ToString() & "', Login_Count = '1', Login_Fail_Count = '0', Expiry_Date = '" & _
                                myDS.Tables(0).Rows(0).Item(5).ToString() & "', Option_Change_Password_On_Next_Logon = '" & ddlChange_Pass.SelectedValue & _
                                "' Where Company_Profile_code = '" & Session("Company").ToString & "' And Code = '" & lstright.Items(i).Value & "'"
                                htb.Add(i, ssql1)
                            End If
                        Else
                            myDS = Nothing
                            lblresult.Text = "Data can not be retrieve!"
                            Exit Sub
                        End If
                        myDS = Nothing
                    Next
                    mySQL.ExecuteSQLTransactionByHashtable(htb, "Update", Session("Company").ToString, Session("EmpID").ToString)
                    lblresult.Text = "Data(s) update successfully..."
                End If
                lstright.Items.Clear()
            Else
                lblresult.Text = "No item(s) selected!"
                Exit Sub
            End If
        End If

    End Sub

    Protected Sub imgBtnClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnClear.Click

        If lnkBtnClosePassSetting.Enabled = True Then
            txtPass_Age.Text = ""
            txtPass_Length.Text = ""
            txtPass_Max_Length.Text = ""
            ddlPass_Complexity.SelectedIndex = 0
            txtExpired_Alert.Text = ""
            txtPass_Level.Text = ""
            txtDefault_Pass.Text = ""
            lblresult.Text = ""
        ElseIf lnkBtnCloseResetPass.Enabled = True Then
            lblresult.Text = ""
        End If

    End Sub

#End Region

#Region "Sub & Function"

    Public Sub lnkBtnViewPassSetting_Click()

        ClearField()

        lnkBtnViewPassSetting.Visible = False
        lnkBtnClosePassSetting.Visible = True
        lnkBtnViewResetPass.Visible = True
        lnkBtnCloseResetPass.Visible = False

        pnlpart1.Visible = True
        pnlpart2.Visible = True
        pnlpart3.Visible = False

    End Sub

    Public Sub lnkBtnClosePassSetting_Click()

        lnkBtnViewPassSetting.Visible = True
        lnkBtnClosePassSetting.Visible = False

    End Sub

    Public Sub lnkBtnViewResetPass_Click()

        ClearField()

        lnkBtnViewPassSetting.Visible = True
        lnkBtnClosePassSetting.Visible = False
        lnkBtnViewResetPass.Visible = False
        lnkBtnCloseResetPass.Visible = True

        pnlpart1.Visible = True
        pnlpart2.Visible = False
        pnlpart3.Visible = True

        filterBy = "emp"
        BindList()
        CheckBoxDefault.Checked = True
        ddlChange_Pass.SelectedIndex = 1

    End Sub

    Public Sub lnkBtnCloseResetPass_Click()

        lnkBtnViewResetPass.Visible = True
        lnkBtnCloseResetPass.Visible = False

    End Sub

    Public Sub ClearField()

        lblresult.Text = ""
        CheckBoxDefault.Checked = False
        CheckBoxManual.Checked = False

    End Sub

    Protected Sub imgBtnAddAll_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAddAll.Click
        AddRemoveAll(lstleft, lstright)
        lblresult.Text = ""
    End Sub

    Protected Sub imgBtnAddItem_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAddItem.Click
        AddRemoveItem(lstleft, lstright)
        lblresult.Text = ""
    End Sub

    Protected Sub imgBtnRemoveItem_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnRemoveItem.Click
        AddRemoveItem(lstright, lstleft)
        lblresult.Text = ""
    End Sub

    Protected Sub imgBtnRemoveAll_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnRemoveAll.Click
        AddRemoveAll(lstright, lstleft)
        lblresult.Text = ""
    End Sub

    Private Sub AddRemoveAll(ByVal aSource As ListBox, ByVal aTarget As ListBox)

        Try
            For i = 0 To aSource.Items.Count - 1
                aTarget.Items.Add(aSource.Items(i))
            Next
            aSource.Items.Clear()
        Catch ex As Exception
            lblresult.Text = "Error: " + ex.Message
        End Try


    End Sub

    Private Sub AddRemoveItem(ByVal aSource As ListBox, ByVal aTarget As ListBox)

        Try
            For i = 0 To aSource.Items.Count - 1
                If aSource.Items(i).Selected Then
                    aTarget.Items.Add(aSource.Items(i))
                End If
            Next
            For i = aSource.Items.Count - 1 To 0 Step -1
                If aSource.Items(i).Selected = True Then
                    aSource.Items.Remove(aSource.Items(i))
                End If
            Next

        Catch ex As Exception
            lblresult.Text = "Error: " + ex.Message
        End Try

    End Sub

#End Region

End Class
