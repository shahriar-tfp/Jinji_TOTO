Imports System
Imports System.Data
Imports System.Data.SqlClient
Partial Class Pages_System_Manager_Explorer_Profile
    Inherits System.Web.UI.Page

    Enum ControlPosition
        _Left
        _Top
    End Enum

#Region "Public Declaration"
    Private WithEvents mySQL As New clsSQL, myDS As New DataSet, myDS2 As New DataSet, mySetting As New clsGlobalSetting
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Public ssql As String, i As Integer, j As Integer, SearchByPage As Boolean, RecFound As Boolean, CountRecord As Integer
    Public AllowView, AllowInsert, AllowUpdate, AllowDelete, AllowPrint As Boolean
    Public _currentPageNumber As Integer, _totalPage As Double = 1, _totalRecords As Integer
    Public myDE As New DictionaryEntry, myHT As Hashtable
    Public myDataType As New clsGlobalSetting.DataType, SearchByFilter As Boolean = False
    Dim intColumn As Integer
    Dim strPath As String = "../../Images"
#End Region

#Region "Private Declaration"
    Private WithEvents myDT1 As New DataTable, myDT2 As New DataTable
    Private WithEvents myDSDefault, myDSInsert, myDSUpdate, myDSDelete, myDS1 As New DataSet
    Dim myDR1 As DataRow, myDR2 As DataRow, rcPerPage As Integer, autonum As Integer
    Dim ssql2 As String, ssql3 As String, ssql4 As String
    Dim rowPosition As String = "R_", rowPositionM As String = "RM_", panelPosition As String = "Display_"
    Dim buttonPosition1 As String = "B1_", buttonPosition2 As String = "B2_", buttonPosition3 As String = "B3_", labelPosition1 As String = "L1_"
    Dim btnColourDef, btnColourAlt As String
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.IsNewSession Then
            Response.Redirect("../Global/SessionTimeOut.aspx")
        End If
        If Not IsPostBack Then
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
            pnlGridview.Width = CInt(Session("GVwidth"))
            pnlGridview.Height = CInt(Session("GVheight")) - 20 'setting for this page only
            PagePreload()
            BindGrid()
        Else
            lblResult.Text = ""
            lblResult2.Text = ""
        End If
    End Sub

    Private Sub PagePreload()
        Session("Module") = "SYSTEM_MANAGER"
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")

        'get Page Title
        ssql = "exec sp_sa_GetPageTitle '" & Form.ID & "'"
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            lblTitle.Text = myDS.Tables(0).Rows(0).Item(0)
            lblTitle.CssClass = "wordstyle4"
        End If
        myDS = Nothing

        mySetting.GetBtnImgUrl(imgbtnUPDATE, Session("Company").ToString, btnColourDef, "btnUpdate.png")
        mySetting.GetImgUrl(imgKeyMODULE_PROFILE_CODE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgUrl(imgKeyLEVEL, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

        lblMODULE_PROFILE_CODE.Text = "Module Profile Code"
        lblLEVEL.Text = "Level"

        lblMODULE_PROFILE_CODE.CssClass = "wordstyle3"
        lblLEVEL.CssClass = "wordstyle3"

        tblLinkButton.Visible = False
        lblResult.CssClass = "wordstyle2"
        lblResult2.CssClass = "wordstyle2"
        pnlEdit.Visible = False
        mySetting.GetImgTypeUrl(imgtop, Session("Company"), Session("strTheme"), "backgroundTitleTop_Edit.png")
        mySetting.GetImgTypeUrl(imgbottom, Session("Company"), Session("strTheme"), "backgroundTitleBottom.png")

        'bind module profile code dropdownlist
        'ssql = "Create Table #Result([Code] nvarchar(50), [Name] nvarchar(255)); Insert Into #Result Select '',''; Insert Into #Result Select Code, [Name] From Module_Profile Order By [Name]; Select * From #Result; Drop Table #Result"
        ssql = "Create Table #Result([Code] nvarchar(50), [Name] nvarchar(255)); Insert Into #Result Select '',''; Insert Into #Result Select Code,Name From Explorer_Profile Where Indentation='2' And Explorer_Profile.Module_Profile_Code In (Select Code From Module_Profile) Order By [Name]; Select * From #Result; Drop Table #Result"
        myDS = mySQL.ExecuteSQL(ssql)
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Columns.Count > 1 Then
                ddlMODULE_PROFILE_CODE.DataTextField = "Name"
                ddlMODULE_PROFILE_CODE.DataValueField = "Code"
                ddlMODULE_PROFILE_CODE.DataSource = myDS
                ddlMODULE_PROFILE_CODE.DataBind()
            End If
        End If
        myDS = Nothing

        'get field position
        Dim txtWidth As Integer = 0
        Dim ddlWidth As Integer = 0
        Dim AblWidth As Integer = 0
        Dim EctWidth As Integer = 0

        'get width
        If CInt(Session("GVwidth")) > 680 Then
            AblWidth = (CInt(Session("GVwidth")) - 680)
            EctWidth = AblWidth / 2
            txtWidth = 150 + EctWidth
            ddlWidth = 157 + EctWidth
        Else
            txtWidth = 150
            ddlWidth = 157
            EctWidth = 0
        End If

        ssql = "Exec sp_sa_GetExplorerProfileDetails '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','','','','FIELD'"
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql)
        Dim count As Integer = 0
        count = 16
        If Not myDS Is Nothing Then
            For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1
                Dim myImageKey As ImageButton = Page.FindControl("imgKey" & myDS.Tables(0).Rows(i).Item(0).ToString)
                count += 1
                If Not myImageKey Is Nothing Then
                    mySetting.GetImgBtnUrl(myImageKey, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
                    myImageKey.Style.Add("left", mySetting.GetObjPosition(EctWidth, count, "X"))
                    myImageKey.Style.Add("top", mySetting.GetObjPosition(EctWidth, count, "Y"))
                    myImageKey.Style.Add("position", "absolute")
                    If myDS.Tables(0).Rows(i).Item(3).ToString = "YES" Or myDS.Tables(0).Rows(i).Item(4).ToString = "YES" Then
                        myImageKey.Visible = True
                    Else
                        myImageKey.Visible = False
                    End If
                End If
                count += 1
                Dim myLabel As Label = Page.FindControl("lbl" & myDS.Tables(0).Rows(i).Item(0).ToString)
                If Not myLabel Is Nothing Then
                    myLabel.Style.Add("left", mySetting.GetObjPosition(EctWidth, count, "X"))
                    myLabel.Style.Add("top", mySetting.GetObjPosition(EctWidth, count, "Y"))
                    myLabel.Style.Add("position", "absolute")
                    myLabel.Text = myDS.Tables(0).Rows(i).Item(1).ToString
                    myLabel.Style.Add("color", "#000000")
                    myLabel.Style.Add("font-family", "Arial Unicode MS")
                    myLabel.Style.Add("font-weight", "bold")
                    myLabel.Style.Add("font-size", "11px")
                End If
                count += 1
                Select Case myDS.Tables(0).Rows(i).Item(2).ToString.ToUpper
                    Case "OPTION"
                        Dim myDDL As DropDownList = Page.FindControl("ddl" & myDS.Tables(0).Rows(i).Item(0).ToString)
                        If Not myDDL Is Nothing Then
                            myDDL.Style.Add("left", mySetting.GetObjPosition(EctWidth, count, "X"))
                            myDDL.Style.Add("top", mySetting.GetObjPosition(EctWidth, count, "Y"))
                            myDDL.Style.Add("position", "absolute")
                            myDDL.Width = ddlWidth
                            mySetting.GetDropdownlistValue(Form.ID, myDS.Tables(0).Rows(i).Item(0).ToString, myDDL)
                        End If
                    Case Else
                        Dim myTXT As TextBox = Page.FindControl("txt" & myDS.Tables(0).Rows(i).Item(0).ToString)
                        If Not myTXT Is Nothing Then
                            myTXT.Style.Add("left", mySetting.GetObjPosition(EctWidth, count, "X"))
                            myTXT.Style.Add("top", mySetting.GetObjPosition(EctWidth, count, "Y"))
                            myTXT.Style.Add("position", "absolute")
                            myTXT.Width = txtWidth
                        End If
                End Select
                count += 1
                Dim myImageButton As ImageButton = Page.FindControl("imgbtn" & myDS.Tables(0).Rows(i).Item(0).ToString)
                If Not myImageButton Is Nothing Then
                    mySetting.GetImgBtnUrl(myImageButton, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
                    myImageButton.Style.Add("left", mySetting.GetObjPosition(EctWidth, count, "X"))
                    myImageButton.Style.Add("top", mySetting.GetObjPosition(EctWidth, count, "Y"))
                    myImageButton.Style.Add("position", "absolute")
                End If
                Select Case myDS.Tables(0).Rows(i).Item(2).ToString.ToUpper
                    Case "LOOKUP"
                        If Not myImageButton Is Nothing Then
                            myImageButton.Visible = True
                        End If
                    Case Else
                        If Not myImageButton Is Nothing Then
                            myImageButton.Visible = False
                        End If
                End Select
            Next
        End If
        tblBottom.Style.Add("top", mySetting.GetObjPosition(EctWidth, count + 8, "Y"))
        tblBottom.Style.Add("position", "absolute")
    End Sub

    Private Sub BindGrid()
        ssql = "Exec sp_sa_GetExplorerProfileDetails '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','" & ddlMODULE_PROFILE_CODE.SelectedValue.ToString & "','','" & ddlLEVEL.SelectedValue.ToString & "','GET'"
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql)
        If Not myDS Is Nothing Then
            pnlGridview.Visible = True
            tblLinkButton.Visible = True
            pnlList.Visible = True
            pnlEdit.Visible = False
            myGridView.DataSource = myDS.Tables(0).DefaultView
            myGridView.DataBind()
            If myDS.Tables(0).Rows.Count > 0 Then
                lnkbtnViewLIST.Enabled = True
                lnkBtnViewEDIT.Enabled = True
                lnkBtnViewLIST_Click()
            End If
        End If
        myDS = Nothing
    End Sub

    Private Sub BindGrid_Blank()
        lnkbtnViewLIST.Enabled = False
        lnkBtnViewEDIT.Enabled = False
        ssql = "Exec sp_sa_GetExplorerProfileDetails '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "','','','','GET'"
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql)
        If Not myDS Is Nothing Then
            pnlGridview.Visible = True
            tblLinkButton.Visible = True
            pnlList.Visible = True
            pnlEdit.Visible = False
            myGridView.DataSource = myDS.Tables(0).DefaultView
            myGridView.DataBind()
            If myDS.Tables(0).Rows.Count = 0 Then
                lnkbtnViewLIST.Enabled = False
                lnkBtnViewEDIT.Enabled = False
                lnkBtnCloseLIST_Click()
            End If
        End If
        myDS = Nothing
    End Sub

    Protected Sub lnkbtnViewLIST_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnViewLIST.Click
        lnkBtnViewLIST_Click()
        pnlEdit.Visible = False
        pnlList.Visible = True
    End Sub

    Protected Sub lnkBtnViewEDIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnViewEDIT.Click
        If ValidateEdit() = True Then
            pnlEdit.Visible = True
            pnlList.Visible = False

            txtCODE.Enabled = False
            txtINDENTATION.Enabled = False
            For i As Integer = 0 To myGridView.Rows.Count - 1
                Dim chkBox As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
                If chkBox.Checked = True Then
                    mySetting.ArrangeDDLSelectedIndex(ddlOPTION_TYPE, clsGlobalSetting.DDLSelection.SelectedValue, myGridView.Rows(i).Cells(1).Text.ToString)
                    txtCODE.Text = myGridView.Rows(i).Cells(2).Text.ToString
                    txtNAME.Text = myGridView.Rows(i).Cells(3).Text.ToString
                    txtINDENTATION.Text = myGridView.Rows(i).Cells(4).Text.ToString
                    txtSEQUENCE_NO.Text = myGridView.Rows(i).Cells(5).Text.ToString
                    mySetting.ArrangeDDLSelectedIndex(ddlOPTION_EXPENDABLE, clsGlobalSetting.DDLSelection.SelectedValue, myGridView.Rows(i).Cells(6).Text.ToString)
                    mySetting.ArrangeDDLSelectedIndex(ddlOPTION_SHOW, clsGlobalSetting.DDLSelection.SelectedValue, myGridView.Rows(i).Cells(7).Text.ToString)
                End If
                chkBox.Checked = False
                lnkBtnViewEDIT_Click()
            Next
        Else
            lblResult.Visible = True
        End If
    End Sub

    Private Function ValidateSearch() As Boolean

        If ddlMODULE_PROFILE_CODE.SelectedValue.ToString = "" Then
            lblResult.Text = lblMODULE_PROFILE_CODE.Text & " is a required field!"
            ddlMODULE_PROFILE_CODE.Focus()
            Return False
        End If
        If ddlLEVEL.SelectedValue.ToString = "" Then
            lblResult.Text = lblLEVEL.Text & " is a required field!"
            ddlLEVEL.Focus()
            Return False
        End If
        Return True

    End Function

    Private Function ValidateEdit() As Boolean
        Dim RecFound As Boolean = False, RecCount As Integer
        For i As Integer = 0 To myGridView.Rows.Count - 1
            Dim chkBox As CheckBox = myGridView.Rows(i).Cells(0).Controls(1)
            If chkBox.Checked = True Then
                RecFound = True
                RecCount += 1
            End If
        Next
        If RecFound = False Then
            lblResult.Text = "Please select one row to proceed!"
            Return False
        End If
        If RecFound = True And RecCount > 1 Then
            lblResult.Text = "You select " & RecCount.ToString & " records for editing which is invalid! Please select only one record!"
            Return False
        End If
        Return True
    End Function

    Protected Sub imgbtnUPDATE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnUPDATE.Click
        If ValidateUpdate() = True Then
            ssql = "Update Explorer_Profile Set Name=N'" & txtNAME.Text.ToString & "',Sequence_No=" & txtSEQUENCE_NO.Text.ToString & ",Option_Expendable=N'" & ddlOPTION_EXPENDABLE.SelectedValue.ToString & "'," & _
                    "Option_Show=N'" & ddlOPTION_SHOW.SelectedValue.ToString & "' Where Code=N'" & txtCODE.Text.ToString & "' And Indentation=" & txtINDENTATION.Text.ToString
            mySQL.ExecuteSQLNonQuery(ssql, Session("Company").ToString, Session("EmpID").ToString)
            BindGrid()
        Else
            lblResult2.Visible = True
        End If
    End Sub

    Private Function ValidateUpdate() As Boolean
        If Not IsNumeric(txtSEQUENCE_NO.Text) Then
            lblResult2.Text = "[Sequence No] Must Be In Integer Format!"
            txtSEQUENCE_NO.Focus()
            Return False
        End If
        If CInt(txtSEQUENCE_NO.Text.ToString) < 1 Then
            lblResult2.Text = "[Sequence No] Must Be Greater Than 0!"
            txtSEQUENCE_NO.Focus()
            Return False
        End If
        If txtNAME.Text.ToString.Trim = "" Then
            lblResult2.Text = "You must specify a name for [Name]!"
            txtNAME.Focus()
            Return False
        End If
        If ddlOPTION_TYPE.SelectedValue = "" Then
            lblResult2.Text = "[Option Type] Is A Required Field!"
            ddlOPTION_TYPE.Focus()
            Return False
        End If
        If ddlOPTION_EXPENDABLE.SelectedValue = "" Then
            lblResult2.Text = "[Option Expendable] Is A Required Field!"
            ddlOPTION_EXPENDABLE.Focus()
            Return False
        End If
        If ddlOPTION_SHOW.SelectedValue = "" Then
            lblResult2.Text = "[Option Show] Is A Required Field!"
            ddlOPTION_SHOW.Focus()
            Return False
        End If
        Return True
    End Function

    Protected Sub myGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles myGridView.RowDataBound
        e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor; this.style.backgroundColor='silver';")
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;")
    End Sub

    Protected Sub ddlMODULE_PROFILE_CODE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMODULE_PROFILE_CODE.SelectedIndexChanged

        Dim tempLevel As Integer = ddlLEVEL.SelectedIndex

        'bind module profile code dropdownlist
        ssql = "Create Table #Result([Code] nvarchar(50), [Name] nvarchar(255)); Insert Into #Result Select '',''; Insert Into #Result Select Distinct Indentation,Indentation From Explorer_Profile Where Module_Profile_Code = '" & ddlMODULE_PROFILE_CODE.SelectedValue.ToString & "' ; Select * From #Result; Drop Table #Result"
        myDS = mySQL.ExecuteSQL(ssql)
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Columns.Count > 1 Then
                ddlLEVEL.DataTextField = "Name"
                ddlLEVEL.DataValueField = "Code"
                ddlLEVEL.DataSource = myDS
                ddlLEVEL.DataBind()
            End If
        End If
        myDS = Nothing

        If (ddlLEVEL.SelectedIndex = tempLevel) = True Then
            ddlLEVEL.SelectedIndex = tempLevel
        Else
            ddlLEVEL.SelectedIndex = 0
            BindGrid_Blank()
        End If

        If ddlMODULE_PROFILE_CODE.SelectedIndex > 0 And ddlLEVEL.SelectedIndex > 0 Then
            If ValidateSearch() = True Then
                BindGrid()
            Else
                BindGrid_Blank()
            End If
        ElseIf ddlMODULE_PROFILE_CODE.SelectedIndex > 0 Then
            BindGrid_Blank()
        End If

    End Sub

    Protected Sub ddlLEVEL_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLEVEL.SelectedIndexChanged

        If ddlMODULE_PROFILE_CODE.SelectedIndex > 0 And ddlLEVEL.SelectedIndex > 0 Then
            If ValidateSearch() = True Then
                BindGrid()
            Else
                BindGrid_Blank()
            End If
        Else
            BindGrid_Blank()
        End If

    End Sub

    Public Sub lnkBtnViewLIST_Click()

        lnkbtnViewLIST.Visible = False
        lnkBtnCloseLIST.Visible = True
        lnkBtnViewEDIT.Visible = True
        lnkBtnCloseEDIT.Visible = False

    End Sub

    Public Sub lnkBtnCloseLIST_Click()

        lnkbtnViewLIST.Visible = True
        lnkBtnCloseLIST.Visible = False

    End Sub

    Public Sub lnkBtnViewEDIT_Click()

        lnkbtnViewLIST.Visible = True
        lnkBtnCloseLIST.Visible = False
        lnkBtnViewEDIT.Visible = False
        lnkBtnCloseEDIT.Visible = True

    End Sub

    Public Sub lnkBtnCloseEDIT_Click()

        lnkBtnViewEDIT.Visible = True
        lnkBtnCloseEDIT.Visible = False

    End Sub

End Class
