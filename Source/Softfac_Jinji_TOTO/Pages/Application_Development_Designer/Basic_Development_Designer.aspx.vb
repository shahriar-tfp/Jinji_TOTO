Imports System.Data
Imports System.IO

Partial Class Pages_Application_Development_Designer_Basic_Development_Designer
    Inherits System.Web.UI.Page
#Region "Declaration"
    Private WithEvents mySQL As New clsSQL, myAutoGenerate As New clsAutoGenerate, myArray As New ArrayList
    Private WithEvents myDS As New DataSet, mySetting As New clsGlobalSetting
    Dim ssql As String, i As Integer, j As Integer, autonum As Integer
    Dim rowPosition As String = "R_", rowPositionM As String = "RM_", panelPosition As String = "Display_"
    Dim buttonPosition1 As String = "B1_", buttonPosition2 As String = "B2_", buttonPosition3 As String = "B3_", labelPosition1 As String = "L1_"
    Public strWebFileToCreate As String, strVBFileToCreate As String
    Dim btnColourDef, btnColourAlt As String
    Dim strPath As String = "../Images"
    Dim logic As Boolean
    Const AddDefaultField As Boolean = True
    Public Enum ErrorCode
        ModuleExist = 1
        TableFieldExist = 2
        ModuleAndTableExist = 3
        TableExist = 4
    End Enum
    Public Enum PositionType
        _Left
        _Top
    End Enum
#End Region

#Region "Sub & Function"
    Private Sub PagePreload()
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")
        Session("Module") = "APPLICATION_DEVELOPMENT_DESIGNER"
        'Session("Company") = "softfac"
        'Session("EmpID") = "hrsa"

        'Set Create Button Visible = False
        imgbtnFieldCREATE.Visible = False
        imgbtnOptionCREATE.Visible = False
        imgbtnLookupCREATE.Visible = False
        imgbtnSecurityCreate.Visible = False
        'Get Page Title
        ssql = "Exec sp_sa_GetPageTitle '" & Form.ID & "'"
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            lblTitle.Text = myDS.Tables(0).Rows(0).Item(0)
            lblTitle.CssClass = "wordstyle4"

            lblTitlePage.Text = "[Create Page]"
            lblTitlePage.CssClass = "wordstyle4"

            lblTitleField.Text = "[Creating Fields]"
            lblTitleField.CssClass = "wordstyle4"

            lblTitleOption.Text = "[Creating Option]"
            lblTitleOption.CssClass = "wordstyle4"

            lblTitleLookup.Text = "[Creating Lookup]"
            lblTitleLookup.CssClass = "wordstyle4"

            lblTitleSecurity.Text = "[Assign Permission]"
            lblTitleSecurity.CssClass = "wordstyle4"
        End If
        myDS = Nothing

        'Get Image Key
        imgKeyMODULE_PROFILE_CODE.Enabled = False
        imgKeyTABLE.Enabled = False
        imgKeyFIELD_CODE.Enabled = False
        imgKeyFIELD_NAME.Enabled = False
        imgKeySEQUENCE_NO.Enabled = False
        imgKeyOPTION_PRIMARY_KEY.Enabled = False
        imgKeyOPTION_PRIMARY_KEY.Enabled = False
        imgKeyOPTION_DATA_TYPE.Enabled = False
        imgKeyLENGTH.Enabled = False
        imgKeyOPTION_ENABLED.Enabled = False
        imgKeyOPTION_FIELD_NAME.Enabled = False
        imgKeyOPTION_CODE.Enabled = False
        imgKeyOPTION_NAME.Enabled = False
        imgKeyOPTION_DEFAULT.Enabled = False
        imgKeyOPTION_EDITABLE.Enabled = False
        imgKeyOPTION_MANDATORY.Enabled = False
        imgKeyOPTION_PASSWORD.Enabled = False
        imgKeyOPTION_SET_FILTER.Enabled = False
        imgKeyOPTION_VIEW_LIST.Enabled = False
        imgKeyOPTION_VIEW_CARD.Enabled = False
        imgKeyLOOKUP_FIELD_NAME.Enabled = False
        imgKeyLOOKUP_TABLE_VIEW.Enabled = False
        imgKeyLOOKUP_OPTION_TYPE.Enabled = False
        imgKeySECURITY_OPTION_ALLOW_VIEW.Enabled = False
        imgKeySECURITY_OPTION_ALLOW_INSERT.Enabled = False
        imgKeySECURITY_OPTION_ALLOW_UPDATE.Enabled = False
        imgKeySECURITY_OPTION_ALLOW_DELETE.Enabled = False
        imgKeySECURITY_OPTION_ALLOW_PRINT.Enabled = False

        mySetting.GetImgBtnUrl(imgKeyMODULE_PROFILE_CODE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyTABLE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyFIELD_CODE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyFIELD_NAME, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeySEQUENCE_NO, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyOPTION_PRIMARY_KEY, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyOPTION_DATA_TYPE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyLENGTH, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyOPTION_ENABLED, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyOPTION_FIELD_NAME, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyOPTION_CODE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyOPTION_NAME, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyOPTION_DEFAULT, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyOPTION_EDITABLE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyOPTION_MANDATORY, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyOPTION_PASSWORD, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyOPTION_SET_FILTER, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyOPTION_VIEW_LIST, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyOPTION_VIEW_CARD, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyLOOKUP_FIELD_NAME, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyLOOKUP_TABLE_VIEW, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeyLOOKUP_OPTION_TYPE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeySECURITY_OPTION_ALLOW_VIEW, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeySECURITY_OPTION_ALLOW_INSERT, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeySECURITY_OPTION_ALLOW_UPDATE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeySECURITY_OPTION_ALLOW_DELETE, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgKeySECURITY_OPTION_ALLOW_PRINT, clsGlobalSetting.ImageType._KEY, Session("Company").ToString)

        'Set Disabled
        imgKeySEQUENCE_NO.Enabled = False
        imgKeyFIELD_CODE.Enabled = False
        imgKeyFIELD_NAME.Enabled = False
        imgKeyOPTION_PRIMARY_KEY.Enabled = False
        imgKeyOPTION_DATA_TYPE.Enabled = False
        imgKeyLENGTH.Enabled = False
        imgKeyOPTION_ENABLED.Enabled = False

        'Get Image Lookup
        mySetting.GetImgBtnUrl(imgbtnMODULE_PROFILE_CODE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnTABLE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnFIELD_CODE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnFIELD_NAME, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnSEQUENCE_NO, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnOPTION_PRIMARY_KEY, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnOPTION_DATA_TYPE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnLENGTH, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnOPTION_ENABLED, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnOPTION_FIELD_NAME, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnOPTION_CODE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnOPTION_NAME, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnOPTION_DEFAULT, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnOPTION_EDITABLE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnOPTION_MANDATORY, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnOPTION_PASSWORD, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnOPTION_SET_FILTER, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnOPTION_VIEW_LIST, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnOPTION_VIEW_CARD, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnLOOKUP_FIELD_NAME, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnLOOKUP_TABLE_VIEW, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnLOOKUP_OPTION_TYPE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnSECURITY_OPTION_ALLOW_VIEW, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnSECURITY_OPTION_ALLOW_INSERT, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnSECURITY_OPTION_ALLOW_UPDATE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnSECURITY_OPTION_ALLOW_DELETE, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)
        mySetting.GetImgBtnUrl(imgbtnSECURITY_OPTION_ALLOW_PRINT, clsGlobalSetting.ImageType._LOOKUP, Session("Company").ToString)

        'Get Lookup Value
        mySetting.GetLookupValue_ImageButton(imgbtnMODULE_PROFILE_CODE, Form.ID, "txtMODULE_PROFILE_CODE", "Code", "Exec sp_sa_GetLookupCodeName " & """" & Session("Company").ToString & """," & """" & Session("Module").ToString & """," & """" & Form.ID & """," & """" & "MODULE_PROFILE_CODE" & """," & """" & """," & """" & Session("EmpID").ToString & """")

        'Get Dropdownlist Value
        mySetting.GetDropdownlistValue("TABLE_FIELD", "OPTION_PRIMARY_KEY", ddlOPTION_PRIMARY_KEY)
        mySetting.GetDropdownlistValue("TABLE_FIELD", "OPTION_DATA_TYPE", ddlOPTION_DATA_TYPE)
        mySetting.GetDropdownlistValue("TABLE_FIELD", "OPTION_ENABLED", ddlOPTION_ENABLED)
        mySetting.GetDropdownlistValue("TABLE_FIELD", "OPTION_EDITABLE", ddlOPTION_EDITABLE)
        mySetting.GetDropdownlistValue("TABLE_FIELD", "OPTION_MANDATORY", ddlOPTION_MANDATORY)
        mySetting.GetDropdownlistValue("TABLE_FIELD", "OPTION_PASSWORD", ddlOPTION_PASSWORD)
        mySetting.GetDropdownlistValue("TABLE_FIELD", "OPTION_SET_FILTER", ddlOPTION_SET_FILTER)
        mySetting.GetDropdownlistValue("TABLE_FIELD", "OPTION_VIEW_LIST", ddlOPTION_VIEW_LIST)
        mySetting.GetDropdownlistValue("TABLE_FIELD", "OPTION_VIEW_CARD", ddlOPTION_VIEW_CARD)
        mySetting.GetDropdownlistValue("OPTION", "OPTION_DEFAULT", ddlOPTION_DEFAULT)
        mySetting.GetDropdownlistValue("ORGANISATION_CODE_PROFILE", "OPTION_TYPE", ddlLOOKUP_OPTION_TYPE)
        mySetting.GetDropdownlistValue("ASSIGN_PAGE_SECURITY", "OPTION_ALLOW_VIEW", ddlSECURITY_OPTION_ALLOW_VIEW)
        mySetting.GetDropdownlistValue("ASSIGN_PAGE_SECURITY", "OPTION_ALLOW_INSERT", ddlSECURITY_OPTION_ALLOW_INSERT)
        mySetting.GetDropdownlistValue("ASSIGN_PAGE_SECURITY", "OPTION_ALLOW_UPDATE", ddlSECURITY_OPTION_ALLOW_UPDATE)
        mySetting.GetDropdownlistValue("ASSIGN_PAGE_SECURITY", "OPTION_ALLOW_DELETE", ddlSECURITY_OPTION_ALLOW_DELETE)
        mySetting.GetDropdownlistValue("ASSIGN_PAGE_SECURITY", "OPTION_ALLOW_PRINT", ddlSECURITY_OPTION_ALLOW_PRINT)

        'Set Invisibility For Lookup
        imgbtnTABLE.Visible = False
        imgbtnFIELD_CODE.Visible = False
        imgbtnFIELD_NAME.Visible = False
        imgbtnSEQUENCE_NO.Visible = False
        imgbtnOPTION_PRIMARY_KEY.Visible = False
        imgbtnOPTION_DATA_TYPE.Visible = False
        imgbtnLENGTH.Visible = False
        imgbtnOPTION_ENABLED.Visible = False
        imgbtnOPTION_FIELD_NAME.Visible = False
        imgbtnOPTION_CODE.Visible = False
        imgbtnOPTION_NAME.Visible = False
        imgbtnOPTION_DEFAULT.Visible = False
        imgbtnOPTION_EDITABLE.Visible = False
        imgbtnOPTION_MANDATORY.Visible = False
        imgbtnOPTION_PASSWORD.Visible = False
        imgbtnOPTION_SET_FILTER.Visible = False
        imgbtnOPTION_VIEW_LIST.Visible = False
        imgbtnOPTION_VIEW_CARD.Visible = False
        imgbtnLOOKUP_FIELD_NAME.Visible = False
        imgbtnLOOKUP_TABLE_VIEW.Visible = False
        imgbtnLOOKUP_OPTION_TYPE.Visible = False
        imgbtnSECURITY_OPTION_ALLOW_VIEW.Visible = False
        imgbtnSECURITY_OPTION_ALLOW_INSERT.Visible = False
        imgbtnSECURITY_OPTION_ALLOW_UPDATE.Visible = False
        imgbtnSECURITY_OPTION_ALLOW_DELETE.Visible = False
        imgbtnSECURITY_OPTION_ALLOW_PRINT.Visible = False

        pnlLookupSub.Visible = False

        'Set Image Path For Action Button
        mySetting.GetBtnImgUrl(imgbtnFieldSubmit, Session("Company").ToString, btnColourDef, "btnSubmit.png")
        mySetting.GetBtnImgUrl(imgbtnFieldRefresh, Session("Company").ToString, btnColourDef, "btnRefresh.png")
        mySetting.GetBtnImgUrl(imgbtnFieldCancel, Session("Company").ToString, btnColourDef, "btnCancel.png")
        mySetting.GetBtnImgUrl(imgBtnFieldAdd, Session("Company").ToString, btnColourDef, "btnAdd.png")
        mySetting.GetBtnImgUrl(imgBtnFieldDelete, Session("Company").ToString, btnColourDef, "btnDelete.png")
        mySetting.GetBtnImgUrl(imgbtnFieldUpdate, Session("Company").ToString, btnColourDef, "btnUpdate.png")
        mySetting.GetBtnImgUrl(imgBtnFieldEdit, Session("Company").ToString, btnColourDef, "btnEdit.png")
        mySetting.GetBtnImgUrl(imgbtnFieldCREATE, Session("Company").ToString, btnColourDef, "btnCreate.png")
        mySetting.GetBtnImgUrl(imgbtnOptionSubmit, Session("Company").ToString, btnColourDef, "btnSubmit.png")
        mySetting.GetBtnImgUrl(imgbtnOptionRefresh, Session("Company").ToString, btnColourDef, "btnRefresh.png")
        mySetting.GetBtnImgUrl(imgbtnOptionCancel, Session("Company").ToString, btnColourDef, "btnCancel.png")
        mySetting.GetBtnImgUrl(imgBtnOptionAdd, Session("Company").ToString, btnColourDef, "btnAdd.png")
        mySetting.GetBtnImgUrl(imgBtnOptionDelete, Session("Company").ToString, btnColourDef, "btnDelete.png")
        mySetting.GetBtnImgUrl(imgbtnOptionUpdate, Session("Company").ToString, btnColourDef, "btnUpdate.png")
        mySetting.GetBtnImgUrl(imgBtnOptionEdit, Session("Company").ToString, btnColourDef, "btnEdit.png")
        mySetting.GetBtnImgUrl(imgbtnOptionCREATE, Session("Company").ToString, btnColourDef, "btnCreate.png")
        mySetting.GetBtnImgUrl(imgbtnLookupSubmit, Session("Company").ToString, btnColourDef, "btnSubmit.png")
        mySetting.GetBtnImgUrl(imgbtnLookupRefresh, Session("Company").ToString, btnColourDef, "btnRefresh.png")
        mySetting.GetBtnImgUrl(imgbtnLookupCancel, Session("Company").ToString, btnColourDef, "btnCancel.png")
        mySetting.GetBtnImgUrl(imgBtnLookupAdd, Session("Company").ToString, btnColourDef, "btnAdd.png")
        mySetting.GetBtnImgUrl(imgBtnLookupDelete, Session("Company").ToString, btnColourDef, "btnDelete.png")
        mySetting.GetBtnImgUrl(imgbtnLookupUpdate, Session("Company").ToString, btnColourDef, "btnUpdate.png")
        mySetting.GetBtnImgUrl(imgBtnLookupEdit, Session("Company").ToString, btnColourDef, "btnEdit.png")
        mySetting.GetBtnImgUrl(imgbtnLookupCREATE, Session("Company").ToString, btnColourDef, "btnCreate.png")
        mySetting.GetBtnImgUrl(imgbtnSecuritySubmit, Session("Company").ToString, btnColourDef, "btnSubmit.png")
        mySetting.GetBtnImgUrl(imgbtnSecurityRefresh, Session("Company").ToString, btnColourDef, "btnRefresh.png")
        mySetting.GetBtnImgUrl(imgbtnSecurityCancel, Session("Company").ToString, btnColourDef, "btnCancel.png")
        mySetting.GetBtnImgUrl(imgbtnSecurityAdd, Session("Company").ToString, btnColourDef, "btnAdd.png")
        mySetting.GetBtnImgUrl(imgbtnSecurityDelete, Session("Company").ToString, btnColourDef, "btnDelete.png")
        mySetting.GetBtnImgUrl(imgbtnSecurityUpdate, Session("Company").ToString, btnColourDef, "btnUpdate.png")
        mySetting.GetBtnImgUrl(imgbtnSecurityEdit, Session("Company").ToString, btnColourDef, "btnEdit.png")
        mySetting.GetBtnImgUrl(imgbtnSecurityCreate, Session("Company").ToString, btnColourDef, "btnCreate.png")

        'Set Invisibility
        'pnlFieldMain.Visible = False
        'pnlOptionMain.Visible = False
        'pnlLookupMain.Visible = False
        'pnlSecurityMain.Visible = False

        'Get Image URL For List
        mySetting.GetBtnImgUrl(imgbtnSelectAll, Session("Company").ToString, btnColourDef, "addall.png")
        mySetting.GetBtnImgUrl(imgbtnSelectOne, Session("Company").ToString, btnColourDef, "additem.png")
        mySetting.GetBtnImgUrl(imgbtnRemoveAll, Session("Company").ToString, btnColourDef, "removeall.png")
        mySetting.GetBtnImgUrl(imgbtnRemoveOne, Session("Company").ToString, btnColourDef, "removeitem.png")

        'Set Uppercase
        mySetting.ConvertUppercase(txtMODULE_PROFILE_CODE)
        mySetting.ConvertUppercase(txtTABLE)
        mySetting.ConvertUppercase(txtFIELD_CODE)
        mySetting.ConvertUppercase(txtOPTION_CODE)
    End Sub
    Private Sub BindGridField()
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")
        If Not IsPostBack Then
            Dim dt As New DataTable
            dt.Columns.Add("Sequence No", System.Type.GetType("System.Int32"))
            dt.Columns.Add("Field Code")
            dt.Columns.Add("Field Name")
            dt.Columns.Add("Option Primary Key")
            dt.Columns.Add("Option Data Type")
            dt.Columns.Add("Data Length")
            dt.Columns.Add("Option Enabled")
            dt.Columns.Add("Option Editable")
            dt.Columns.Add("Option Mandatory")
            dt.Columns.Add("Option Password")
            dt.Columns.Add("Option Set Filter")
            dt.Columns.Add("Option View List")
            dt.Columns.Add("Option View Card")


            dt.PrimaryKey = New DataColumn() {dt.Columns(1)}
            Session("dtField") = dt
            gvField.DataSource = Session("dtField")
            gvField.DataBind()
            dt = Nothing
        Else
            Dim dt As DataTable
            dt = CType(Session("dtField"), DataTable)
            Dim dr As DataRow = dt.NewRow
            dr(0) = dt.Rows.Count + 1
            dr(1) = txtFIELD_CODE.Text.ToString.ToUpper.Trim
            dr(2) = txtFIELD_NAME.Text.ToString.Trim
            dr(3) = ddlOPTION_PRIMARY_KEY.SelectedItem.Text.ToString
            dr(4) = ddlOPTION_DATA_TYPE.SelectedItem.Text.ToString
            dr(5) = txtLENGTH.Text.ToString
            dr(6) = ddlOPTION_ENABLED.SelectedItem.Text.ToString
            dr(7) = ddlOPTION_EDITABLE.SelectedItem.Text.ToString
            dr(8) = ddlOPTION_MANDATORY.SelectedItem.Text.ToString
            dr(9) = ddlOPTION_PASSWORD.SelectedItem.Text.ToString
            dr(10) = ddlOPTION_SET_FILTER.SelectedItem.Text.ToString
            dr(11) = ddlOPTION_VIEW_LIST.SelectedItem.Text.ToString
            dr(12) = ddlOPTION_VIEW_CARD.SelectedItem.Text.ToString

            dt.Rows.Add(dr)
            dt.AcceptChanges()
            Session("dtField") = dt
            gvField.DataSource = Session("dtField")
            gvField.DataBind()
            dt = Nothing
        End If
    End Sub
    Private Sub BindGridOption(ByVal intChoice As Integer)
        Select Case intChoice
            Case 1
                Dim dt As New DataTable
                dt.Columns.Add("Sequence No")
                dt.Columns.Add("Field Name")
                dt.Columns.Add("Option Code")
                dt.Columns.Add("Option Name")
                dt.Columns.Add("Option Enabled")

                Session("dtOption") = dt
                CType(Session("dtOption"), DataTable).PrimaryKey = New DataColumn() {dt.Columns(1), dt.Columns(2)}
                gvOption.DataSource = dt.DefaultView
                gvOption.DataBind()
            Case 2
                Dim myDT As New DataTable
                Dim myDR As DataRow = CType(Session("MyDTOption"), DataTable).NewRow

                myDT.Columns.Add(Session("MyDTOption").Columns(0).ToString)
                myDT.Columns.Add(Session("MyDTOption").Columns(1).ToString)
                myDT.Columns.Add(Session("MyDTOption").Columns(2).ToString)
                myDT.Columns.Add(Session("MyDTOption").Columns(3).ToString)
                myDT.Columns.Add(Session("MyDTOption").Columns(4).ToString)

                myDR(0) = gvOption.Rows.Count + 1
                myDR(1) = ddlOPTION_FIELD_NAME.SelectedItem.Text.ToString
                myDR(2) = txtOPTION_CODE.Text.ToString.Trim
                myDR(3) = txtOPTION_NAME.Text.ToString.Trim
                myDR(4) = ddlOPTION_DEFAULT.SelectedValue.ToString

                If Not Session("MyDTOption") Is Nothing Then
                    CType(Session("MyDTOption"), DataTable).Rows.Add(myDR)
                    CType(Session("MyDTOption"), DataTable).AcceptChanges()
                End If
                If Not Session("MyDTOption") Is Nothing Then
                    gvOption.DataSource = CType(Session("MyDTOption"), DataTable).DefaultView
                    gvOption.DataBind()
                End If
        End Select
    End Sub
    Private Sub BindGridLookup(ByVal intChoice As Integer)
        Select Case intChoice
            Case 1
                Dim dt As New DataTable
                dt.Columns.Add("Field Name")
                dt.Columns.Add("Table/View To Lookup")
                dt.Columns.Add("Option Type")

                Session("dtLookup") = dt
                CType(Session("dtLookup"), DataTable).PrimaryKey = New DataColumn() {dt.Columns(0)}
                gvLookup.DataSource = dt.DefaultView
                gvLookup.DataBind()
        End Select
    End Sub
    Private Sub BindGridSecurity(ByVal intChoice As Integer)
        Try
            Select Case intChoice
                Case 1
                    Dim dt As New DataTable
                    dt.Columns.Add("Security Role Profile Name")
                    dt.Columns.Add("Option Allow View")
                    dt.Columns.Add("Option Allow Insert")
                    dt.Columns.Add("Option Allow Update")
                    dt.Columns.Add("Option Allow Delete")
                    dt.Columns.Add("Option Allow Print")
                    Session("dtSecurity") = dt
                    CType(Session("dtSecurity"), DataTable).PrimaryKey = New DataColumn() {dt.Columns(0)}
                    gvSecurity.DataSource = dt.DefaultView
                    gvSecurity.DataBind()
                    dt = Nothing
            End Select
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SetInvisibilityForNextButton(ByVal intChoice As Integer)
        Select Case intChoice
            Case 1
                'lnkbtnPageNext_Click
                pnlPage.Visible = True
                pnlPage.Enabled = False
                pnlField.Visible = True
                pnlField.Enabled = True
                pnlOption.Visible = False
                pnlOption.Enabled = False
                pnlLookup.Visible = False
                pnlLookup.Enabled = False
                pnlSecurity.Visible = False
                pnlSecurity.Enabled = False
            Case 2
                'lnkbtnFieldNext_Click
                pnlPage.Visible = True
                pnlPage.Enabled = False
                pnlField.Visible = False
                pnlField.Enabled = False
                pnlOption.Visible = True
                pnlOption.Enabled = True
                pnlLookup.Visible = False
                pnlLookup.Enabled = False
                pnlSecurity.Visible = False
                pnlSecurity.Enabled = False
            Case 3
                'lnkbtnOptionNext_Click
                pnlPage.Visible = True
                pnlPage.Enabled = False
                pnlField.Visible = False
                pnlField.Enabled = False
                pnlOption.Visible = False
                pnlOption.Enabled = False
                pnlLookup.Visible = True
                pnlLookup.Enabled = True
                pnlSecurity.Visible = False
                pnlSecurity.Enabled = False
            Case 4
                'lnkbtnLookupNext_Click
                pnlPage.Visible = True
                pnlPage.Enabled = False
                pnlField.Visible = False
                pnlField.Enabled = False
                pnlOption.Visible = False
                pnlOption.Enabled = False
                pnlLookup.Visible = False
                pnlLookup.Enabled = False
                pnlSecurity.Visible = True
                pnlSecurity.Enabled = True
            Case 5
                'lnkbtnSecurityNext_Click
                'pnlPage.Visible = True
                'pnlField.Visible = False
                'pnlOption.Visible = False
                'pnlLookup.Visible = False
                'pnlSecurity.Visible = True
        End Select
    End Sub
    Private Sub BindListBox(ByVal lstBox As ListBox)
        mySetting.BindListBox(lstBox, "Select Code,Name,CodeName From Security_Role_Profile_Vw Where Company_Profile_Code='" & Session("Company").ToString & "' Order By CodeName", 0, 2)
    End Sub
    Private Sub BindOptionDDL()
        ddlOPTION_FIELD_NAME.Items.Clear()
        ddlOPTION_FIELD_NAME.Items.Add(New ListItem("", ""))
        For i = 0 To gvField.Rows.Count - 1
            If gvField.Rows(i).Cells(5).Text.ToString.ToUpper = "OPTION" Then
                ddlOPTION_FIELD_NAME.Items.Add(New ListItem(gvField.Rows(i).Cells(3).Text.ToString, gvField.Rows(i).Cells(2).Text.ToString.ToUpper))
            End If
        Next
    End Sub
    Private Sub BindLookupDDL()
        ddlLOOKUP_FIELD_NAME.Items.Clear()
        ddlLOOKUP_FIELD_NAME.Items.Add(New ListItem("", ""))
        For i = 0 To gvField.Rows.Count - 1
            If gvField.Rows(i).Cells(5).Text.ToString.ToUpper = "LOOKUP" Then
                If gvField.Rows(i).Cells(2).Text.ToString.ToUpper = "COMPANY_PROFILE_CODE" Then
                    GoTo Skip
                End If
                If gvField.Rows(i).Cells(2).Text.ToString.ToUpper = "EMPLOYEE_PROFILE_ID" Then
                    GoTo Skip
                End If

                ddlLOOKUP_FIELD_NAME.Items.Add(New ListItem(gvField.Rows(i).Cells(3).Text.ToString, gvField.Rows(i).Cells(2).Text.ToString.ToUpper))
Skip:
                'Skip Company Profile Code & Employee Profile ID
                'Dont need to add
            End If
        Next
    End Sub
    Private Sub UpdateSequenceNo(ByVal gv As GridView, ByVal intJ As Integer)
        For i As Integer = 0 To gv.Rows.Count - 1
            gv.Rows(i).Cells(intJ).Text = i + 1
        Next
    End Sub
    Private Sub GetAvailableLookupField()
        Dim ds As New DataSet
        ssql = "Exec sp_sa_GetAvailableLookupField '" & Session("Company").ToString & "','" & _
                Session("Module").ToString & "','','','" & Session("EmpID").ToString & "',0,'CODE,NAME'"
        ds = mySQL.ExecuteSQL(ssql)
        If Not ds Is Nothing Then
            For i = 0 To ds.Tables(0).Rows.Count - 1
                ddlLOOKUP_TABLE_VIEW.Items.Add(New ListItem(ds.Tables(0).Rows(i).Item(0).ToString, ds.Tables(0).Rows(i).Item(0).ToString.ToUpper))
            Next
        End If
    End Sub
    Private Sub ClearListBox(ByVal lstBox As ListBox)
        lstBox.Items.Clear()
    End Sub
    Private Sub EnabledPrevNext(ByVal booYesNo As Boolean)
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")
        Select Case booYesNo
            Case True
                imgbtnSelectOne.Enabled = True
                imgbtnSelectAll.Enabled = True
                imgbtnRemoveOne.Enabled = True
                imgbtnRemoveAll.Enabled = True
                mySetting.GetBtnImgUrl(imgbtnSelectAll, Session("Company").ToString, btnColourDef, "addall.png")
                mySetting.GetBtnImgUrl(imgbtnSelectOne, Session("Company").ToString, btnColourDef, "additem.png")
                mySetting.GetBtnImgUrl(imgbtnRemoveAll, Session("Company").ToString, btnColourDef, "removeall.png")
                mySetting.GetBtnImgUrl(imgbtnRemoveOne, Session("Company").ToString, btnColourDef, "removeitem.png")
            Case False
                imgbtnSelectOne.Enabled = False
                imgbtnSelectAll.Enabled = False
                imgbtnRemoveOne.Enabled = False
                imgbtnRemoveAll.Enabled = False
                'mySetting.GetBtnImgUrl(imgbtnSelectAll, Session("Company").ToString, btnColourAlt, "addall.png")
                'mySetting.GetBtnImgUrl(imgbtnSelectOne, Session("Company").ToString, btnColourAlt, "additem.png")
                'mySetting.GetBtnImgUrl(imgbtnRemoveAll, Session("Company").ToString, btnColourAlt, "removeall.png")
                'mySetting.GetBtnImgUrl(imgbtnRemoveOne, Session("Company").ToString, btnColourAlt, "removeitem.png")
        End Select
    End Sub
#End Region

#Region "Event Procedure"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session.IsNewSession Then
        '    Response.Redirect("../Global/SessionTimeOut.aspx")
        'End If
        If Not IsPostBack Then
            'If Session("ScreenWidth") = 0 Then
            '    Session("ScreenWidth") = "1024"
            '    Session("GVwidth") = Session("ScreenWidth") - 270
            'End If
            'If Session("ScreenHeight") = 0 Then
            '    Session("ScreenHeight") = "768"
            '    Session("GVheight") = (Session("ScreenHeight") / 2) - 80
            'End If
            PagePreload()
            BindGridField()
            BindGridOption(1)
            BindGridLookup(1)
            BindGridSecurity(1)
            BindListBox(lstLeft)
            GetAvailableLookupField()
            mnuStep.Items(0).Selected = True
            SetActiveViewIndex(0)
        Else
            lblPageMessage.Text = ""
            lblFieldMessage.Text = ""
            lblOptionMessage.Text = ""
            lblLookupMessage.Text = ""
            lblSecurityMessage.Text = ""
            lblMessage.Text = ""
        End If
    End Sub
    Protected Sub mnuStep_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mnuStep.MenuItemClick
        SetActiveViewIndex(Int32.Parse(e.Item.Value))
    End Sub
#Region "NextButton_Click"
    Protected Sub lnkbtnPageNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnPageNext.Click
        If ValidatePageNext() = True Then
            SetActiveViewIndex(1)
        End If
    End Sub
    Protected Sub lnkbtnFieldNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnFieldNext.Click
        If ValidateFieldNext() = True Then
            Dim intViewIndex As Integer = 0
            If ddlOPTION_FIELD_NAME.Items.Count > 1 Then
                SetActiveViewIndex(2)
                Exit Sub
            End If
            If ddlLOOKUP_FIELD_NAME.Items.Count > 1 Then
                SetActiveViewIndex(3)
                Exit Sub
            End If
            SetActiveViewIndex(4)
        End If
    End Sub
    Protected Sub lnkbtnOptionNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnOptionNext.Click
        If ValidateOptionNext() = True Then
            SetActiveViewIndex(3)
        End If
    End Sub
    Protected Sub lnkbtnLookupNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLookupNext.Click
        If ValidateLookupNext() = True Then
            SetActiveViewIndex(4)
        End If
    End Sub

    Private Sub SetActiveViewIndex(ByVal intIndex As Integer)
        mtvStep.ActiveViewIndex = intIndex
        Select Case intIndex
            Case 0
                mnuStep.Items(0).Enabled = False
                mnuStep.Items(1).Enabled = True
                mnuStep.Items(2).Enabled = True
                mnuStep.Items(3).Enabled = True
                mnuStep.Items(4).Enabled = True
            Case 1
                imgBtnFieldAdd_Click(Nothing, Nothing)
                mnuStep.Items(0).Enabled = True
                mnuStep.Items(1).Enabled = False
                mnuStep.Items(2).Enabled = True
                mnuStep.Items(3).Enabled = True
                mnuStep.Items(4).Enabled = True
            Case 2
                mnuStep.Items(0).Enabled = True
                mnuStep.Items(1).Enabled = True
                mnuStep.Items(2).Enabled = False
                mnuStep.Items(3).Enabled = True
                mnuStep.Items(4).Enabled = True
                If ddlOPTION_FIELD_NAME.Items.Count < 2 Then
                    pnlOptionButton.Enabled = False
                    pnlOptionMain.Enabled = False
                    imgbtnOptionUpdate.Visible = False
                    lblOptionMessage.Text = "No collection(s) to be add! None of the row in [Step 2] has value with data type 'Option'!"
                Else
                    pnlOptionMain.Enabled = True
                    imgbtnOptionUpdate.Visible = False
                    pnlOptionButton.Enabled = True
                    'imgBtnOptionAdd_Click(Nothing, Nothing)
                End If
            Case 3
                mnuStep.Items(0).Enabled = True
                mnuStep.Items(1).Enabled = True
                mnuStep.Items(2).Enabled = True
                mnuStep.Items(3).Enabled = False
                mnuStep.Items(4).Enabled = True
                If ddlLOOKUP_FIELD_NAME.Items.Count < 2 Then
                    pnlLookupButton.Enabled = False
                    pnlLookupMain.Enabled = False
                    imgbtnLookupUpdate.Visible = False
                    lblLookupMessage.Text = "No collection(s) to be add! None of the row in [Step 2] has value with data type 'Lookup'!"
                Else
                    pnlLookupMain.Enabled = True
                    imgbtnLookupUpdate.Visible = False
                    pnlLookupButton.Enabled = True
                    'imgBtnLookupAdd_Click(Nothing, Nothing)
                End If
            Case 4
                imgbtnSecurityAdd_Click(Nothing, Nothing)
                mnuStep.Items(0).Enabled = True
                mnuStep.Items(1).Enabled = True
                mnuStep.Items(2).Enabled = True
                mnuStep.Items(3).Enabled = True
                mnuStep.Items(4).Enabled = False
        End Select
    End Sub
#End Region
#Region "BackButton_Click"
    Protected Sub lnkbtnFieldBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnFieldBack.Click
        SetActiveViewIndex(0)
    End Sub

    Protected Sub lnkbtnOptionBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnOptionBack.Click
        SetActiveViewIndex(1)
    End Sub

    Protected Sub lnkbtnLookupBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLookupBack.Click
        If ddlOPTION_FIELD_NAME.Items.Count > 1 Then
            SetActiveViewIndex(2)
            Exit Sub
        End If
        SetActiveViewIndex(1)
    End Sub

    Protected Sub lnkbtnSecurityBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnSecurityBack.Click
        If ddlLOOKUP_FIELD_NAME.Items.Count > 1 Then
            SetActiveViewIndex(3)
            Exit Sub
        End If
        If ddlOPTION_FIELD_NAME.Items.Count > 1 Then
            SetActiveViewIndex(2)
            Exit Sub
        End If
        SetActiveViewIndex(1)
    End Sub
#End Region
#End Region

#Region "Step 1 Module [Event & Procedure]"
    Private Function ValidatePageNext() As Boolean
        If txtMODULE_PROFILE_CODE.Text.ToString.Trim = "" Then
            lblPageMessage.Text = "[Module Profile Code] Is A Required Field! Please click to select from list or enter new entry!"
            txtMODULE_PROFILE_CODE.Focus()
            Return False
        End If
        If txtTABLE.Text.ToString.Trim = "" Then
            lblPageMessage.Text = "[Page Name] Is A Required Field! Please specify a name for it!"
            txtTABLE.Focus()
            Return False
        End If
        If txtTABLE.Text.ToString.IndexOf(" ") > 0 Then
            lblPageMessage.Text = "[Page Name] must not contain any spaces! Please use underscore to replace the spaces!"
            txtTABLE.Focus()
            Return False
        End If
        Return True
    End Function
#End Region

#Region "Step 2 Field [Event & Procedure]"
    Protected Sub imgBtnFieldAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnFieldAdd.Click
        pnlFieldMain.Visible = True
        SetDefaultValueField()
        imgbtnFieldSubmit.Visible = True
        imgbtnFieldUpdate.Visible = False
        imgbtnFieldRefresh.Visible = True
        txtFIELD_CODE.Enabled = True
        txtFIELD_CODE.Focus()
    End Sub
    Protected Sub imgBtnFieldEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnFieldEdit.Click
        If ValidateEdit(gvField, lblFieldMessage) = True Then
            pnlFieldMain.Visible = True
            imgbtnFieldSubmit.Visible = False
            imgbtnFieldUpdate.Visible = True
            imgbtnFieldRefresh.Visible = False
            txtFIELD_CODE.Enabled = False
            For i As Integer = 0 To gvField.Rows.Count - 1
                If CType(gvField.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True Then
                    txtSEQUENCE_NO.Text = gvField.Rows(i).Cells(1).Text.ToString
                    txtFIELD_CODE.Text = gvField.Rows(i).Cells(2).Text.ToString
                    txtFIELD_NAME.Text = gvField.Rows(i).Cells(3).Text.ToString
                    mySetting.ArrangeDDLSelectedIndex(ddlOPTION_PRIMARY_KEY, clsGlobalSetting.DDLSelection.SelectedText, gvField.Rows(i).Cells(4).Text.ToString)
                    mySetting.ArrangeDDLSelectedIndex(ddlOPTION_DATA_TYPE, clsGlobalSetting.DDLSelection.SelectedText, gvField.Rows(i).Cells(5).Text.ToString)
                    txtLENGTH.Text = gvField.Rows(i).Cells(6).Text.ToString
                    mySetting.ArrangeDDLSelectedIndex(ddlOPTION_ENABLED, clsGlobalSetting.DDLSelection.SelectedText, gvField.Rows(i).Cells(7).Text.ToString)
                    Exit For
                End If
            Next
        End If
    End Sub
    Protected Sub imgBtnFieldDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnFieldDelete.Click
        If ValidateDelete(gvField, lblFieldMessage) = True Then
            For i As Integer = 0 To gvField.Rows.Count - 1
                If CType(gvField.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True Then
                    If gvField.Rows(i).Cells(2).Text.ToString.ToUpper = "COMPANY_PROFILE_CODE" Then
                        chkCompanyProfileCode.Checked = False
                    End If
                    If gvField.Rows(i).Cells(2).Text.ToString.ToUpper = "EMPLOYEE_PROFILE_ID" Then
                        chkEmployeeProfileID.Checked = False
                    End If
                    'Debug Chew
                    If gvField.Rows(i).Cells(5).Text.ToString.ToUpper = "LOOKUP" Then
                        ddlLOOKUP_FIELD_NAME.Items.Remove(gvField.Rows(i).Cells(2).Text.ToString.ToUpper)
                        Dim dt As DataTable = CType(Session("dtLookup"), DataTable)

                        Dim findTheseVals(0) As Object
                        findTheseVals(0) = gvField.Rows(i).Cells(2).Text.ToString.ToUpper

                        Dim findRow As DataRow = dt.Rows.Find(findTheseVals)
                        If Not findRow Is Nothing Then
                            dt.Rows.Remove(findRow)
                        End If
                        dt.AcceptChanges()
                        Session("dtLookup") = dt
                        gvLookup.DataSource = dt
                        gvLookup.DataBind()
                        dt = Nothing
                    End If
                    Dim foundRow As DataRow = CType(Session("dtField"), DataTable).Rows.Find(gvField.Rows(i).Cells(2).Text.ToString)
                    If Not foundRow Is Nothing Then
                        CType(Session("dtField"), DataTable).Rows.Remove(foundRow)
                    End If
                End If
            Next
            CType(Session("dtField"), DataTable).AcceptChanges()
            CType(Session("dtField"), DataTable).DefaultView.Sort = "[Sequence No] ASC"
            gvField.DataSource = Session("dtField")
            gvField.DataBind()
            BindOptionDDL()
            BindLookupDDL()
            SetDefaultValueField()
        End If
    End Sub
    Protected Sub imgbtnFieldRefresh_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnFieldRefresh.Click
        SetDefaultValueField()
    End Sub
    Protected Sub imgbtnFieldCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnFieldCancel.Click
        'pnlFieldMain.Visible = False
    End Sub
    Protected Sub imgbtnFieldSubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnFieldSubmit.Click
        If ValidateInsertField() = True Then
            'Dim maxSeqNo As Integer = 0
            'For i As Integer = 0 To gvField.Rows.Count - 1
            '    If CInt(gvField.Rows(i).Cells(1).Text.ToString) > maxSeqNo Then
            '        maxSeqNo = CInt(gvField.Rows(i).Cells(1).Text.ToString)
            '    End If
            'Next

            Dim dr As DataRow = CType(Session("dtField"), DataTable).NewRow
            dr(0) = txtSEQUENCE_NO.Text.ToString.Trim
            dr(1) = txtFIELD_CODE.Text.ToString.Trim.ToUpper
            dr(2) = txtFIELD_NAME.Text.ToString.Trim
            dr(3) = ddlOPTION_PRIMARY_KEY.SelectedItem.Text.ToString
            dr(4) = ddlOPTION_DATA_TYPE.SelectedItem.ToString
            dr(5) = txtLENGTH.Text.ToString.Trim
            dr(6) = ddlOPTION_ENABLED.SelectedItem.ToString
            dr(7) = ddlOPTION_EDITABLE.SelectedItem.ToString
            dr(8) = ddlOPTION_MANDATORY.SelectedItem.ToString
            dr(9) = ddlOPTION_PASSWORD.SelectedItem.ToString
            dr(10) = ddlOPTION_SET_FILTER.SelectedItem.ToString
            dr(11) = ddlOPTION_VIEW_LIST.SelectedItem.ToString
            dr(12) = ddlOPTION_VIEW_CARD.SelectedItem.ToString

            CType(Session("dtField"), DataTable).Rows.Add(dr)
            CType(Session("dtField"), DataTable).AcceptChanges()
            CType(Session("dtField"), DataTable).DefaultView.Sort = "[Sequence No] ASC"

            gvField.DataSource = Session("dtField")
            gvField.DataBind()
            imgbtnFieldCancel_Click(Nothing, Nothing)

            BindOptionDDL()
            BindLookupDDL()
            SetDefaultValueField()
        End If
    End Sub
    Protected Sub imgbtnFieldUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnFieldUpdate.Click
        If ValidateUpdateField() = True Then
            Dim dt As DataTable = CType(Session("dtField"), DataTable)
            Dim foundRow As DataRow = dt.Rows.Find(txtFIELD_CODE.Text.ToString)
            If Not foundRow Is Nothing Then
                foundRow(0) = txtSEQUENCE_NO.Text.ToString.Trim
                'foundRow(1) = txtFIELD_CODE.Text.ToString.Trim
                foundRow(2) = txtFIELD_NAME.Text.ToString.Trim
                foundRow(3) = ddlOPTION_PRIMARY_KEY.SelectedItem.Text.ToString.Trim
                foundRow(4) = ddlOPTION_DATA_TYPE.SelectedItem.Text.ToString.Trim
                foundRow(5) = txtLENGTH.Text.ToString.Trim
                foundRow(6) = ddlOPTION_ENABLED.SelectedItem.Text.ToString.Trim
                foundRow(7) = ddlOPTION_EDITABLE.SelectedItem.Text.ToString.Trim
                foundRow(8) = ddlOPTION_MANDATORY.SelectedItem.Text.ToString.Trim
                foundRow(9) = ddlOPTION_PASSWORD.SelectedItem.Text.ToString.Trim
                foundRow(10) = ddlOPTION_SET_FILTER.SelectedItem.Text.ToString.Trim
                foundRow(11) = ddlOPTION_VIEW_LIST.SelectedItem.Text.ToString.Trim
                foundRow(12) = ddlOPTION_VIEW_CARD.SelectedItem.Text.ToString.Trim

                dt.AcceptChanges()
            End If
            dt.DefaultView.Sort = "[Sequence No] ASC"
            Session("dtField") = dt
            gvField.DataSource = dt
            gvField.DataBind()
            dt = Nothing
            imgbtnFieldCancel_Click(Nothing, Nothing)
            BindOptionDDL()
            BindLookupDDL()
            SetDefaultValueField()
        End If
    End Sub
    Protected Sub imgbtnFieldCREATE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnFieldCREATE.Click

    End Sub
    Protected Sub chkCompanyProfileCode_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCompanyProfileCode.CheckedChanged
        If chkCompanyProfileCode.Checked = True Then
            Dim dt As DataTable
            dt = CType(Session("dtField"), DataTable)
            Dim maxSeqNo As Integer = 0
            For i As Integer = 0 To gvField.Rows.Count - 1
                If CInt(gvField.Rows(i).Cells(1).Text.ToString) > maxSeqNo Then
                    maxSeqNo = CInt(gvField.Rows(i).Cells(1).Text.ToString)
                End If
            Next

            Dim dr As DataRow = dt.NewRow
            dr(0) = maxSeqNo + 10
            dr(1) = "COMPANY_PROFILE_CODE"
            dr(2) = "Company Profile Code"
            dr(3) = "Yes"
            dr(4) = "Lookup"
            dr(5) = "50"
            dr(6) = "Yes"
            dr(7) = "Yes"
            dr(8) = "Yes"
            dr(9) = "No"
            dr(10) = "Yes"
            dr(11) = "Yes"
            dr(12) = "Yes"

            dt.Rows.Add(dr)
            dt.AcceptChanges()
            Session("dtField") = dt
            gvField.DataSource = Session("dtField")
            gvField.DataBind()
            dt = Nothing
            SetDefaultValueField()
        Else
            Dim dt As DataTable
            dt = CType(Session("dtField"), DataTable)
            Dim foundRow As DataRow = dt.Rows.Find("COMPANY_PROFILE_CODE")
            If Not foundRow Is Nothing Then
                CType(Session("dtField"), DataTable).Rows.Remove(foundRow)
                CType(Session("dtField"), DataTable).AcceptChanges()
            End If
            gvField.DataSource = Session("dtField")
            gvField.DataBind()
            dt = Nothing
            SetDefaultValueField()
        End If
    End Sub
    Protected Sub chkEmployeeProfileID_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEmployeeProfileID.CheckedChanged
        If chkEmployeeProfileID.Checked = True Then
            Dim maxSeqNo As Integer = 0
            For i As Integer = 0 To gvField.Rows.Count - 1
                If CInt(gvField.Rows(i).Cells(1).Text.ToString) > maxSeqNo Then
                    maxSeqNo = CInt(gvField.Rows(i).Cells(1).Text.ToString)
                End If
            Next

            Dim dt As DataTable
            dt = CType(Session("dtField"), DataTable)
            Dim dr As DataRow = dt.NewRow
            dr(0) = maxSeqNo + 10
            dr(1) = "EMPLOYEE_PROFILE_ID"
            dr(2) = "Employee Profile ID"
            dr(3) = "Yes"
            dr(4) = "Lookup"
            dr(5) = "50"
            dr(6) = "Yes"
            dr(7) = "Yes"
            dr(8) = "Yes"
            dr(9) = "No"
            dr(10) = "Yes"
            dr(11) = "Yes"
            dr(12) = "Yes"

            dt.Rows.Add(dr)
            dt.AcceptChanges()
            Session("dtField") = dt
            gvField.DataSource = Session("dtField")
            gvField.DataBind()
            dt = Nothing
            SetDefaultValueField()
        Else
            Dim dt As DataTable
            dt = CType(Session("dtField"), DataTable)
            Dim foundRow As DataRow = dt.Rows.Find("EMPLOYEE_PROFILE_ID")
            If Not foundRow Is Nothing Then
                CType(Session("dtField"), DataTable).Rows.Remove(foundRow)
                CType(Session("dtField"), DataTable).AcceptChanges()
            End If
            gvField.DataSource = Session("dtField")
            gvField.DataBind()
            dt = Nothing
            SetDefaultValueField()
        End If

    End Sub
    Private Function ValidateInsertField() As Boolean
        If txtSEQUENCE_NO.Text.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Sequence No] Is A Required Field!"
            txtSEQUENCE_NO.Focus()
            Return False
        End If
        If Not IsNumeric(txtSEQUENCE_NO.Text.ToString.Trim) Then
            lblFieldMessage.Text = "Please enter numeric only value for [Sequence No]!"
            txtSEQUENCE_NO.Focus()
            Return False
        End If
        If txtFIELD_CODE.Text.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Field Code] Is A Required Field!"
            txtFIELD_CODE.Focus()
            Return False
        End If
        If Len(txtFIELD_CODE.Text.ToString.Trim) > 50 Then
            lblFieldMessage.Text = "Max Length for [Field Code] Is 50 characters only!"
            txtFIELD_CODE.Focus()
            Return False
        End If
        If txtFIELD_CODE.Text.ToString.Trim.IndexOf(" ") > 0 Then
            lblFieldMessage.Text = "[Field Code] must not contain any spaces! Please use underscore to replace the spaces!"
            txtFIELD_CODE.Focus()
            Return False
        End If
        If txtFIELD_NAME.Text.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Field Name] Is A Required Field!"
            txtFIELD_NAME.Focus()
            Return False
        End If
        If Len(txtFIELD_NAME.Text.ToString.Trim) > 100 Then
            lblFieldMessage.Text = "Max Length for [Field Name] Is 100 characters only!"
            txtFIELD_NAME.Focus()
            Return False
        End If
        If ddlOPTION_PRIMARY_KEY.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Is Primary Key] Is A Required Field!"
            ddlOPTION_PRIMARY_KEY.Focus()
            Return False
        End If
        If ddlOPTION_DATA_TYPE.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Option Data Type] Is A Required Field!"
            ddlOPTION_DATA_TYPE.Focus()
            Return False
        End If
        If txtLENGTH.Text.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Max Length] Is A Required Field!"
            txtLENGTH.Focus()
            Return False
        End If
        If Not IsNumeric(txtLENGTH.Text.ToString.Trim) Then
            lblFieldMessage.Text = "Please enter numeric only value for [Max Length]!"
            txtLENGTH.Focus()
            Return False
        End If
        If CInt(txtLENGTH.Text.ToString.Trim) > 255 Then
            lblFieldMessage.Text = "[Max Length] cannot greater than 255!"
            txtLENGTH.Focus()
            Return False
        End If
        If ddlOPTION_ENABLED.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Option Enabled] Is A Required Field!"
            ddlOPTION_ENABLED.Focus()
            Return False
        End If
        If ddlOPTION_EDITABLE.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Option Editable] Is A Required Field!"
            ddlOPTION_EDITABLE.Focus()
            Return False
        End If
        If ddlOPTION_MANDATORY.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Option Mandatory] Is A Required Field!"
            ddlOPTION_MANDATORY.Focus()
            Return False
        End If
        If ddlOPTION_PASSWORD.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Option Password] Is A Required Field!"
            ddlOPTION_PASSWORD.Focus()
            Return False
        End If
        If ddlOPTION_SET_FILTER.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Option Set Filter] Is A Required Field!"
            ddlOPTION_SET_FILTER.Focus()
            Return False
        End If
        If ddlOPTION_VIEW_LIST.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Option View List] Is A Required Field!"
            ddlOPTION_VIEW_LIST.Focus()
            Return False
        End If
        If ddlOPTION_VIEW_CARD.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Option View Card] Is A Required Field!"
            ddlOPTION_VIEW_CARD.Focus()
            Return False
        End If
        Dim dt As DataTable = CType(Session("dtField"), DataTable)
        Dim findTheseVals(0) As Object
        findTheseVals(0) = txtFIELD_CODE.Text.ToString.Trim

        Dim foundRow As DataRow = dt.Rows.Find(findTheseVals)
        If Not foundRow Is Nothing Then
            lblFieldMessage.Text = "Duplicate records found for value [" & txtFIELD_CODE.Text.ToString.Trim & "]!"
            txtFIELD_CODE.Focus()
            Return False
        End If

        Return True
    End Function
    Private Function ValidateUpdateField() As Boolean
        If txtSEQUENCE_NO.Text.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Sequence No] Is A Required Field!"
            txtSEQUENCE_NO.Focus()
            Return False
        End If
        If Not IsNumeric(txtSEQUENCE_NO.Text.ToString.Trim) Then
            lblFieldMessage.Text = "Please enter numeric only value for [Sequence No]!"
            txtSEQUENCE_NO.Focus()
            Return False
        End If
        If txtFIELD_CODE.Text.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Field Code] Is A Required Field!"
            txtFIELD_CODE.Focus()
            Return False
        End If
        If Len(txtFIELD_CODE.Text.ToString.Trim) > 50 Then
            lblFieldMessage.Text = "Max Length for [Field Code] Is 50 characters only!"
            txtFIELD_CODE.Focus()
            Return False
        End If
        If txtFIELD_CODE.Text.ToString.Trim.IndexOf(" ") > 0 Then
            lblFieldMessage.Text = "[Field Code] must not contain any spaces! Please use underscore to replace the spaces!"
            txtFIELD_CODE.Focus()
            Return False
        End If
        If txtFIELD_NAME.Text.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Field Name] Is A Required Field!"
            txtFIELD_NAME.Focus()
            Return False
        End If
        If Len(txtFIELD_NAME.Text.ToString.Trim) > 100 Then
            lblFieldMessage.Text = "Max Length for [Field Name] Is 100 characters only!"
            txtFIELD_NAME.Focus()
            Return False
        End If
        If ddlOPTION_PRIMARY_KEY.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Is Primary Key] Is A Required Field!"
            ddlOPTION_PRIMARY_KEY.Focus()
            Return False
        End If
        If ddlOPTION_DATA_TYPE.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Option Data Type] Is A Required Field!"
            ddlOPTION_DATA_TYPE.Focus()
            Return False
        End If
        If txtLENGTH.Text.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Max Length] Is A Required Field!"
            txtLENGTH.Focus()
            Return False
        End If
        If Not IsNumeric(txtLENGTH.Text.ToString.Trim) Then
            lblFieldMessage.Text = "Please enter numeric only value for [Max Length]!"
            txtLENGTH.Focus()
            Return False
        End If
        If CInt(txtLENGTH.Text.ToString.Trim) > 255 Then
            lblFieldMessage.Text = "[Max Length] cannot greater than 255!"
            txtLENGTH.Focus()
            Return False
        End If
        If ddlOPTION_ENABLED.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Option Enabled] Is A Required Field!"
            ddlOPTION_ENABLED.Focus()
            Return False
        End If
        If ddlOPTION_EDITABLE.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Option Editable] Is A Required Field!"
            ddlOPTION_EDITABLE.Focus()
            Return False
        End If
        If ddlOPTION_MANDATORY.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Option Mandatory] Is A Required Field!"
            ddlOPTION_MANDATORY.Focus()
            Return False
        End If
        If ddlOPTION_PASSWORD.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Option Password] Is A Required Field!"
            ddlOPTION_PASSWORD.Focus()
            Return False
        End If
        If ddlOPTION_SET_FILTER.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Option Set Filter] Is A Required Field!"
            ddlOPTION_SET_FILTER.Focus()
            Return False
        End If
        If ddlOPTION_VIEW_LIST.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Option View List] Is A Required Field!"
            ddlOPTION_VIEW_LIST.Focus()
            Return False
        End If
        If ddlOPTION_VIEW_CARD.SelectedValue.ToString.Trim = "" Then
            lblFieldMessage.Text = "[Option View Card] Is A Required Field!"
            ddlOPTION_VIEW_CARD.Focus()
            Return False
        End If
        Return True
    End Function
    Private Function ValidateEdit(ByVal gv As GridView, ByVal lblMsg As Label) As Boolean
        Dim cnt As Integer = 0
        For i As Integer = 0 To gv.Rows.Count - 1
            If CType(gv.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True Then
                cnt += 1
            End If
        Next
        If cnt = 0 Then
            lblMsg.Text = "Please select a row to edit!"
            Return False
        End If
        If cnt > 1 Then
            lblMsg.Text = "You cannot select " & cnt.ToString & " rows to edit at the same time! Please select only one row to edit!"
            Return False
        End If
        Return True
    End Function
    Private Function ValidateDelete(ByVal gv As GridView, ByVal lblMsg As Label) As Boolean
        Dim cnt As Integer = 0
        For i As Integer = 0 To gv.Rows.Count - 1
            If CType(gv.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True Then
                cnt += 1
            End If
        Next
        If cnt = 0 Then
            lblMsg.Text = "Please select at least one row to delete!"
            Return False
        End If
        Return True
    End Function
    Sub SetDefaultValueField()
        txtFIELD_CODE.Text = ""
        txtFIELD_NAME.Text = ""

        Dim maxSeqNo As Integer = 0
        For i As Integer = 0 To gvField.Rows.Count - 1
            If CInt(gvField.Rows(i).Cells(1).Text.ToString) > maxSeqNo Then
                maxSeqNo = CInt(gvField.Rows(i).Cells(1).Text.ToString)
            End If
        Next

        'maxSeqNo = (((maxSeqNo / 10) * 10) + 10) - (maxSeqNo Mod 10)
        maxSeqNo = ((maxSeqNo \ 10) + 1) * 10
        txtSEQUENCE_NO.Text = maxSeqNo

        If ddlOPTION_PRIMARY_KEY.Items.Count > 0 Then
            mySetting.ArrangeDDLSelectedIndex(ddlOPTION_PRIMARY_KEY, clsGlobalSetting.DDLSelection.SelectedValue, "NO")
        End If
        If ddlOPTION_DATA_TYPE.Items.Count > 0 Then
            mySetting.ArrangeDDLSelectedIndex(ddlOPTION_DATA_TYPE, clsGlobalSetting.DDLSelection.SelectedValue, "CHARACTER")
        End If
        txtLENGTH.Text = "50"
        If ddlOPTION_ENABLED.Items.Count > 0 Then
            mySetting.ArrangeDDLSelectedIndex(ddlOPTION_ENABLED, clsGlobalSetting.DDLSelection.SelectedValue, "YES")
        End If
        If ddlOPTION_EDITABLE.Items.Count > 0 Then
            mySetting.ArrangeDDLSelectedIndex(ddlOPTION_EDITABLE, clsGlobalSetting.DDLSelection.SelectedValue, "YES")
        End If
        If ddlOPTION_MANDATORY.Items.Count > 0 Then
            mySetting.ArrangeDDLSelectedIndex(ddlOPTION_MANDATORY, clsGlobalSetting.DDLSelection.SelectedValue, "YES")
        End If
        If ddlOPTION_PASSWORD.Items.Count > 0 Then
            mySetting.ArrangeDDLSelectedIndex(ddlOPTION_PASSWORD, clsGlobalSetting.DDLSelection.SelectedValue, "NO")
        End If
        If ddlOPTION_SET_FILTER.Items.Count > 0 Then
            mySetting.ArrangeDDLSelectedIndex(ddlOPTION_SET_FILTER, clsGlobalSetting.DDLSelection.SelectedValue, "YES")
        End If
        If ddlOPTION_VIEW_LIST.Items.Count > 0 Then
            mySetting.ArrangeDDLSelectedIndex(ddlOPTION_VIEW_LIST, clsGlobalSetting.DDLSelection.SelectedValue, "YES")
        End If
        If ddlOPTION_VIEW_CARD.Items.Count > 0 Then
            mySetting.ArrangeDDLSelectedIndex(ddlOPTION_VIEW_CARD, clsGlobalSetting.DDLSelection.SelectedValue, "YES")
        End If
    End Sub
    Private Function ValidateFieldNext() As Boolean
        If gvField.Rows.Count = 0 Then
            lblFieldMessage.Text = "No field was created for the new page!"
            imgBtnFieldAdd_Click(Nothing, Nothing)
            Return False
        End If
        Return True
    End Function
#End Region

#Region "Step 2(a) Option [Event & Procedure]"
    Protected Sub imgBtnOptionAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnOptionAdd.Click
        pnlOptionMain.Visible = True
        SetDefaultValueOption()
        imgbtnOptionSubmit.Visible = True
        imgbtnOptionUpdate.Visible = False
        imgbtnOptionRefresh.Visible = True
        ddlOPTION_FIELD_NAME.Enabled = True
        txtOPTION_CODE.Enabled = True
        txtOPTION_CODE.Focus()
    End Sub
    Protected Sub imgBtnOptionEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnOptionEdit.Click
        If ValidateEdit(gvOption, lblOptionMessage) = True Then
            pnlOptionMain.Visible = True
            imgbtnOptionSubmit.Visible = False
            imgbtnOptionUpdate.Visible = True
            imgbtnOptionRefresh.Visible = False
            ddlOPTION_FIELD_NAME.Enabled = False
            txtOPTION_CODE.Enabled = False
            For i As Integer = 0 To gvOption.Rows.Count - 1
                If CType(gvOption.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True Then
                    mySetting.ArrangeDDLSelectedIndex(ddlOPTION_FIELD_NAME, clsGlobalSetting.DDLSelection.SelectedValue, gvOption.Rows(i).Cells(2).Text.ToString)
                    txtOPTION_CODE.Text = gvOption.Rows(i).Cells(3).Text.ToString
                    txtOPTION_NAME.Text = gvOption.Rows(i).Cells(4).Text.ToString
                    mySetting.ArrangeDDLSelectedIndex(ddlOPTION_DEFAULT, clsGlobalSetting.DDLSelection.SelectedText, gvOption.Rows(i).Cells(5).Text.ToString)
                    Exit For
                End If
            Next
        End If
    End Sub
    Protected Sub imgBtnOptionDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnOptionDelete.Click
        If ValidateDelete(gvOption, lblOptionMessage) = True Then
            For i As Integer = 0 To gvOption.Rows.Count - 1
                If CType(gvOption.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True Then
                    Dim findTheseVals(1) As Object
                    findTheseVals(0) = gvOption.Rows(i).Cells(2).Text.ToString
                    findTheseVals(1) = gvOption.Rows(i).Cells(3).Text.ToString

                    Dim foundRow As DataRow = CType(Session("dtOption"), DataTable).Rows.Find(findTheseVals)
                    If Not foundRow Is Nothing Then
                        CType(Session("dtOption"), DataTable).Rows.Remove(foundRow)
                    End If
                End If
            Next
            CType(Session("dtOption"), DataTable).AcceptChanges()
            CType(Session("dtOption"), DataTable).DefaultView.Sort = "[Field Name] ASC,[Sequence No] ASC"
            gvOption.DataSource = Session("dtOption")
            gvOption.DataBind()
            UpdateSequenceNo(gvOption, 1)
            SetDefaultValueOption()
        End If
    End Sub
    Protected Sub imgbtnOptionRefresh_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnOptionRefresh.Click
        SetDefaultValueOption()
    End Sub
    Protected Sub imgbtnOptionCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnOptionCancel.Click
        'pnlOptionMain.Visible = False
    End Sub
    Protected Sub imgbtnOptionCREATE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnOptionCREATE.Click

    End Sub
    Protected Sub imgbtnOptionSubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnOptionSubmit.Click
        If ValidateInsertOption() = True Then
            Dim dr As DataRow = CType(Session("dtOption"), DataTable).NewRow
            dr(0) = gvOption.Rows.Count + 1
            dr(1) = ddlOPTION_FIELD_NAME.SelectedItem.Value.ToString
            dr(2) = txtOPTION_CODE.Text.ToString.Trim.ToUpper
            dr(3) = txtOPTION_NAME.Text.ToString.Trim
            dr(4) = ddlOPTION_DEFAULT.SelectedItem.Text.ToString

            CType(Session("dtOption"), DataTable).Rows.Add(dr)
            CType(Session("dtOption"), DataTable).AcceptChanges()
            CType(Session("dtOption"), DataTable).DefaultView.Sort = "[Field Name] ASC,[Sequence No] ASC"
            gvOption.DataSource = Session("dtOption")
            gvOption.DataBind()
            imgbtnOptionCancel_Click(Nothing, Nothing)
            SetDefaultValueOption()
        End If
    End Sub
    Protected Sub imgbtnOptionUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnOptionUpdate.Click
        If ValidateUpdateOption() = True Then
            Dim dt As DataTable = CType(Session("dtOption"), DataTable)

            Dim findTheseVals(1) As Object
            findTheseVals(0) = ddlOPTION_FIELD_NAME.SelectedValue.ToString
            findTheseVals(1) = txtOPTION_CODE.Text.ToString.Trim

            Dim foundRow As DataRow = dt.Rows.Find(findTheseVals)
            If Not foundRow Is Nothing Then
                foundRow(3) = txtOPTION_NAME.Text.ToString.Trim
                foundRow(4) = ddlOPTION_DEFAULT.SelectedItem.Text.ToString.Trim
                dt.AcceptChanges()
            End If
            dt.DefaultView.Sort = "[Field Name] ASC,[Sequence No] ASC"
            Session("dtOption") = dt
            gvOption.DataSource = dt
            gvOption.DataBind()
            dt = Nothing
            imgbtnOptionCancel_Click(Nothing, Nothing)
            SetDefaultValueOption()
        End If
    End Sub
    Private Function ValidateInsertOption() As Boolean
        If ddlOPTION_FIELD_NAME.SelectedValue.ToString.Trim = "" Then
            lblOptionMessage.Text = "[Field Name] Is A Required Field!"
            ddlOPTION_FIELD_NAME.Focus()
            Return False
        End If
        If txtOPTION_CODE.Text.ToString.Trim = "" Then
            lblOptionMessage.Text = "[Option Code] Is A Required Field!"
            txtOPTION_CODE.Focus()
            Return False
        End If
        If txtOPTION_CODE.Text.ToString.Trim.IndexOf(" ") > 0 Then
            lblOptionMessage.Text = "[Option Code] must not contain any spaces! Please use underscore to replace the spaces!"
            txtOPTION_CODE.Focus()
            Return False
        End If
        If txtOPTION_NAME.Text.ToString.Trim = "" Then
            lblOptionMessage.Text = "[Option Name] Is A Required Field!"
            txtOPTION_NAME.Focus()
            Return False
        End If
        If ddlOPTION_DEFAULT.SelectedValue.ToString.Trim = "" Then
            lblOptionMessage.Text = "[Set As Default] Is A Required Field!"
            ddlOPTION_DEFAULT.Focus()
            Return False
        End If
        Dim dt As DataTable = CType(Session("dtOption"), DataTable)
        Dim findTheseVals(1) As Object
        findTheseVals(0) = ddlOPTION_FIELD_NAME.SelectedItem.Value.ToString
        findTheseVals(1) = txtOPTION_CODE.Text.ToString.Trim

        Dim foundRow As DataRow = dt.Rows.Find(findTheseVals)
        If Not foundRow Is Nothing Then
            lblOptionMessage.Text = "Duplicate records found for combination of values [" & ddlOPTION_FIELD_NAME.SelectedItem.Text.ToString & "] + [" & txtOPTION_CODE.Text.ToString.ToUpper.Trim & "]!"
            txtOPTION_CODE.Focus()
            Return False
        End If

        Return True
    End Function
    Private Function ValidateUpdateOption() As Boolean
        If ddlOPTION_FIELD_NAME.SelectedValue.ToString.Trim = "" Then
            lblOptionMessage.Text = "[Field Name] Is A Required Field!"
            ddlOPTION_FIELD_NAME.Focus()
            Return False
        End If
        If txtOPTION_CODE.Text.ToString.Trim = "" Then
            lblOptionMessage.Text = "[Option Code] Is A Required Field!"
            txtOPTION_CODE.Focus()
            Return False
        End If
        If txtOPTION_CODE.Text.ToString.Trim.IndexOf(" ") > 0 Then
            lblOptionMessage.Text = "[Option Code] must not contain any spaces! Please use underscore to replace the spaces!"
            txtOPTION_CODE.Focus()
            Return False
        End If
        If txtOPTION_NAME.Text.ToString.Trim = "" Then
            lblOptionMessage.Text = "[Option Name] Is A Required Field!"
            txtOPTION_NAME.Focus()
            Return False
        End If
        If ddlOPTION_DEFAULT.SelectedValue.ToString.Trim = "" Then
            lblOptionMessage.Text = "[Set As Default] Is A Required Field!"
            ddlOPTION_DEFAULT.Focus()
            Return False
        End If

        Return True
    End Function
    Sub SetDefaultValueOption()
        If ddlOPTION_FIELD_NAME.Items.Count > 1 Then
            ddlOPTION_FIELD_NAME.SelectedIndex = 1
        End If
        txtOPTION_CODE.Text = ""
        txtOPTION_NAME.Text = ""

        If ddlOPTION_DEFAULT.Items.Count > 0 Then
            mySetting.ArrangeDDLSelectedIndex(ddlOPTION_DEFAULT, clsGlobalSetting.DDLSelection.SelectedValue, "NO")
        End If
    End Sub
    Private Function ValidateOptionNext() As Boolean
        If gvOption.Rows.Count = 0 And ddlOPTION_FIELD_NAME.Items.Count > 1 Then
            lblOptionMessage.Text = "No field was created for the [Option]!"
            imgBtnOptionAdd_Click(Nothing, Nothing)
            Return False
        End If
        Return True
    End Function
    
#End Region

#Region "Step 2(b) Lookup [Event & Procedure]"
    Protected Sub imgBtnLookupAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnLookupAdd.Click
        pnlLookupMain.Visible = True
        SetDefaultValueLookup()
        imgbtnLookupSubmit.Visible = True
        imgbtnLookupUpdate.Visible = False
        imgbtnLookupRefresh.Visible = True
        ddlLOOKUP_FIELD_NAME.Enabled = True
        ddlLOOKUP_TABLE_VIEW.Enabled = True
        ddlLOOKUP_OPTION_TYPE.Enabled = True
        ddlLOOKUP_FIELD_NAME.Focus()
    End Sub
    Protected Sub imgbtnLookupCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnLookupCancel.Click
        'pnlLookupMain.Visible = False
    End Sub
    Protected Sub imgbtnLookupCREATE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnLookupCREATE.Click

    End Sub
    Protected Sub imgBtnLookupDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnLookupDelete.Click
        If ValidateDelete(gvLookup, lblLookupMessage) = True Then
            For i As Integer = 0 To gvLookup.Rows.Count - 1
                If CType(gvLookup.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True Then
                    Dim findTheseVals(0) As Object
                    findTheseVals(0) = gvLookup.Rows(i).Cells(1).Text.ToString

                    Dim foundRow As DataRow = CType(Session("dtLookup"), DataTable).Rows.Find(findTheseVals)
                    If Not foundRow Is Nothing Then
                        CType(Session("dtLookup"), DataTable).Rows.Remove(foundRow)
                    End If
                End If
            Next
            CType(Session("dtLookup"), DataTable).AcceptChanges()
            gvLookup.DataSource = Session("dtLookup")
            gvLookup.DataBind()
            SetDefaultValueLookup()
        End If
    End Sub
    Protected Sub imgBtnLookupEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnLookupEdit.Click
        If ValidateEdit(gvLookup, lblLookupMessage) = True Then
            pnlLookupMain.Visible = True
            imgbtnLookupSubmit.Visible = False
            imgbtnLookupUpdate.Visible = True
            imgbtnLookupRefresh.Visible = False
            ddlLookup_FIELD_NAME.Enabled = False

            For i As Integer = 0 To gvLookup.Rows.Count - 1
                If CType(gvLookup.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True Then
                    mySetting.ArrangeDDLSelectedIndex(ddlLOOKUP_FIELD_NAME, clsGlobalSetting.DDLSelection.SelectedValue, gvLookup.Rows(i).Cells(1).Text.ToString)
                    mySetting.ArrangeDDLSelectedIndex(ddlLOOKUP_TABLE_VIEW, clsGlobalSetting.DDLSelection.SelectedValue, gvLookup.Rows(i).Cells(2).Text.ToString)
                    If gvLookup.Rows(i).Cells(2).Text.ToString.ToUpper = "ORGANISATION_CODE_PROFILE_VW" Then
                        pnlLookupSub.Visible = True
                        mySetting.ArrangeDDLSelectedIndex(ddlLOOKUP_OPTION_TYPE, clsGlobalSetting.DDLSelection.SelectedValue, gvLookup.Rows(i).Cells(3).Text.ToString)
                    End If
                    Exit For
                End If
            Next
        End If
    End Sub
    Protected Sub imgbtnLookupRefresh_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnLookupRefresh.Click
        SetDefaultValueLookup()
    End Sub
    Protected Sub imgbtnLookupSubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnLookupSubmit.Click
        If ValidateInsertLookup() = True Then
            Dim dr As DataRow = CType(Session("dtLookup"), DataTable).NewRow
            dr(0) = ddlLOOKUP_FIELD_NAME.SelectedItem.Value.ToString
            dr(1) = ddlLOOKUP_TABLE_VIEW.SelectedItem.Value.ToString
            If pnlLookupSub.Visible = True Then
                dr(2) = ddlLOOKUP_OPTION_TYPE.SelectedItem.Value.ToString
            Else
                dr(2) = ""
            End If

            CType(Session("dtLookup"), DataTable).Rows.Add(dr)
            CType(Session("dtLookup"), DataTable).AcceptChanges()
            gvLookup.DataSource = Session("dtLookup")
            gvLookup.DataBind()
            imgbtnLookupCancel_Click(Nothing, Nothing)
            SetDefaultValueLookup()
        End If
    End Sub
    Protected Sub imgbtnLookupUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnLookupUpdate.Click
        If ValidateUpdateLookup() = True Then
            Dim dt As DataTable = CType(Session("dtLookup"), DataTable)

            Dim findTheseVals(0) As Object
            findTheseVals(0) = ddlLOOKUP_FIELD_NAME.SelectedValue.ToString

            Dim foundRow As DataRow = dt.Rows.Find(findTheseVals)
            If Not foundRow Is Nothing Then
                foundRow(1) = ddlLOOKUP_TABLE_VIEW.SelectedItem.Text.ToString.ToUpper.Trim
                If ddlLOOKUP_TABLE_VIEW.SelectedItem.Value.ToString.ToUpper = "ORGANISATION_CODE_PROFILE_VW" Then
                    foundRow(2) = ddlLOOKUP_OPTION_TYPE.SelectedItem.Text.ToString.ToUpper.Trim
                Else
                    foundRow(2) = ""
                End If
                dt.AcceptChanges()
            End If
            Session("dtLookup") = dt
            gvLookup.DataSource = dt
            gvLookup.DataBind()
            dt = Nothing
            imgbtnLookupCancel_Click(Nothing, Nothing)
            SetDefaultValueLookup()
        End If
    End Sub
    Protected Sub ddlLOOKUP_TABLE_VIEW_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLOOKUP_TABLE_VIEW.SelectedIndexChanged
        If ddlLOOKUP_TABLE_VIEW.SelectedItem.Value.ToString.ToUpper = "ORGANISATION_CODE_PROFILE_VW" Then
            pnlLookupSub.Visible = True
            If ddlLOOKUP_OPTION_TYPE.Items.Count > 1 Then
                ddlLOOKUP_OPTION_TYPE.SelectedIndex = 1
            End If
        Else
            pnlLookupSub.Visible = False
        End If
    End Sub
    Private Function ValidateInsertLookup() As Boolean
        If ddlLOOKUP_FIELD_NAME.SelectedValue.ToString.Trim = "" Then
            lblLookupMessage.Text = "[Field Name] Is A Required Field!"
            ddlLOOKUP_FIELD_NAME.Focus()
            Return False
        End If
        If ddlLOOKUP_TABLE_VIEW.SelectedValue.ToString.Trim = "" Then
            lblLookupMessage.Text = "[Table/View To Lookup] Is A Required Field!"
            ddlLOOKUP_TABLE_VIEW.Focus()
            Return False
        End If
        If ddlLOOKUP_OPTION_TYPE.Visible = True And ddlLOOKUP_OPTION_TYPE.SelectedValue.ToString.Trim = "" Then
            lblLookupMessage.Text = "[Option Type] Is A Required Field!"
            ddlLOOKUP_OPTION_TYPE.Focus()
            Return False
        End If
        Dim dt As DataTable = CType(Session("dtLookup"), DataTable)
        Dim findTheseVals(0) As Object
        findTheseVals(0) = ddlLOOKUP_FIELD_NAME.SelectedValue.ToString

        Dim foundRow As DataRow = dt.Rows.Find(findTheseVals)
        If Not foundRow Is Nothing Then
            lblLookupMessage.Text = "Duplicate records found for value [" & ddlLOOKUP_FIELD_NAME.SelectedItem.Text.ToString & "]!"
            ddlLOOKUP_FIELD_NAME.Focus()
            Return False
        End If

        Return True
    End Function
    Private Function ValidateUpdateLookup() As Boolean
        If ddlLOOKUP_FIELD_NAME.SelectedValue.ToString.Trim = "" Then
            lblLookupMessage.Text = "[Field Name] Is A Required Field!"
            ddlLOOKUP_FIELD_NAME.Focus()
            Return False
        End If
        If ddlLOOKUP_TABLE_VIEW.SelectedValue.ToString.Trim = "" Then
            lblLookupMessage.Text = "[Table/View To Lookup] Is A Required Field!"
            ddlLOOKUP_TABLE_VIEW.Focus()
            Return False
        End If
        If ddlLOOKUP_OPTION_TYPE.Visible = True And ddlLOOKUP_OPTION_TYPE.SelectedValue.ToString.Trim = "" Then
            lblLookupMessage.Text = "[Option Type] Is A Required Field!"
            ddlLOOKUP_OPTION_TYPE.Focus()
            Return False
        End If

        Return True
    End Function
    Sub SetDefaultValueLookup()
        If ddlLOOKUP_FIELD_NAME.Items.Count > 1 Then
            ddlLOOKUP_FIELD_NAME.SelectedIndex = 1
        End If
        If ddlLOOKUP_TABLE_VIEW.Items.Count > 0 Then
            ddlLOOKUP_TABLE_VIEW.SelectedIndex = 0
        End If
        If ddlLOOKUP_OPTION_TYPE.Items.Count > 0 Then
            ddlLOOKUP_OPTION_TYPE.SelectedIndex = 0
        End If
    End Sub
    Private Function ValidateLookupNext() As Boolean
        If gvLookup.Rows.Count = 0 And ddlLOOKUP_FIELD_NAME.Items.Count > 1 Then
            lblLookupMessage.Text = "No field was created for the [Lookup]!"
            imgBtnLookupAdd_Click(Nothing, Nothing)
            Return False
        End If
        Return True
    End Function
#End Region

#Region "Step 3 Security [Event & Procedure]"
    Private Function ValidateInsertSecurity() As Boolean
        If lstRight.Items.Count = 0 Then
            lblSecurityMessage.Text = "Please select at least one item!"
            Return False
        End If
        If ddlSECURITY_OPTION_ALLOW_VIEW.SelectedValue.ToString.Trim = "" Then
            lblSecurityMessage.Text = "[Option Allow View] Is A Required Field!"
            ddlSECURITY_OPTION_ALLOW_VIEW.Focus()
            Return False
        End If
        If ddlSECURITY_OPTION_ALLOW_INSERT.SelectedValue.ToString.Trim = "" Then
            lblSecurityMessage.Text = "[Option Allow Insert] Is A Required Field!"
            ddlSECURITY_OPTION_ALLOW_INSERT.Focus()
            Return False
        End If
        If ddlSECURITY_OPTION_ALLOW_UPDATE.SelectedValue.ToString.Trim = "" Then
            lblSecurityMessage.Text = "[Option Allow Update] Is A Required Field!"
            ddlSECURITY_OPTION_ALLOW_UPDATE.Focus()
            Return False
        End If
        If ddlSECURITY_OPTION_ALLOW_DELETE.SelectedValue.ToString.Trim = "" Then
            lblSecurityMessage.Text = "[Option Allow Delete] Is A Required Field!"
            ddlSECURITY_OPTION_ALLOW_DELETE.Focus()
            Return False
        End If
        If ddlSECURITY_OPTION_ALLOW_PRINT.SelectedValue.ToString.Trim = "" Then
            lblSecurityMessage.Text = "[Option Allow Print] Is A Required Field!"
            ddlSECURITY_OPTION_ALLOW_PRINT.Focus()
            Return False
        End If

        For i As Integer = 0 To lstRight.Items.Count - 1
            Dim dt As DataTable = CType(Session("dtSecurity"), DataTable)
            Dim findTheseVals(0) As Object
            findTheseVals(0) = lstRight.Items(i).Value.ToString

            Dim foundRow As DataRow = dt.Rows.Find(findTheseVals)
            If Not foundRow Is Nothing Then
                lblSecurityMessage.Text = "Duplicate records found for value [" & lstRight.Items(i).Text.ToString & "]!"
                lstRight.Items(i).Selected = True
                Return False
            End If
        Next

        Return True
    End Function
    Private Function ValidateUpdateSecurity() As Boolean
        If lstRight.Items.Count = 0 Then
            lblSecurityMessage.Text = "Please select at least one item!"
            lstRight.Focus()
            Return False
        End If
        If lstRight.Items.Count > 1 Then
            lblSecurityMessage.Text = "Please select only one item!"
            lstRight.Focus()
            Return False
        End If
        If ddlSECURITY_OPTION_ALLOW_VIEW.SelectedValue.ToString.Trim = "" Then
            lblSecurityMessage.Text = "[Option Allow View] Is A Required Field!"
            ddlSECURITY_OPTION_ALLOW_VIEW.Focus()
            Return False
        End If
        If ddlSECURITY_OPTION_ALLOW_INSERT.SelectedValue.ToString.Trim = "" Then
            lblSecurityMessage.Text = "[Option Allow Insert] Is A Required Field!"
            ddlSECURITY_OPTION_ALLOW_INSERT.Focus()
            Return False
        End If
        If ddlSECURITY_OPTION_ALLOW_UPDATE.SelectedValue.ToString.Trim = "" Then
            lblSecurityMessage.Text = "[Option Allow Update] Is A Required Field!"
            ddlSECURITY_OPTION_ALLOW_UPDATE.Focus()
            Return False
        End If
        If ddlSECURITY_OPTION_ALLOW_DELETE.SelectedValue.ToString.Trim = "" Then
            lblSecurityMessage.Text = "[Option Allow Delete] Is A Required Field!"
            ddlSECURITY_OPTION_ALLOW_DELETE.Focus()
            Return False
        End If
        If ddlSECURITY_OPTION_ALLOW_PRINT.SelectedValue.ToString.Trim = "" Then
            lblSecurityMessage.Text = "[Option Allow Print] Is A Required Field!"
            ddlSECURITY_OPTION_ALLOW_PRINT.Focus()
            Return False
        End If

        Return True
    End Function
    Protected Sub imgbtnSelectOne_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSelectOne.Click
        mySetting.AddDeleteListBoxItem(lstLeft, lstRight)
    End Sub
    Protected Sub imgbtnSelectAll_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSelectAll.Click
        mySetting.AddDeleteAllListBoxItem(lstLeft, lstRight)
    End Sub
    Protected Sub imgbtnRemoveOne_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnRemoveOne.Click
        mySetting.AddDeleteListBoxItem(lstRight, lstLeft)
    End Sub
    Protected Sub imgbtnRemoveAll_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnRemoveAll.Click
        mySetting.AddDeleteAllListBoxItem(lstRight, lstLeft)
    End Sub
    Protected Sub imgbtnSecurityAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSecurityAdd.Click
        pnlSecurityMain.Visible = True
        SetDefaltValueSecurity()
        imgbtnSelectOne.Enabled = True
        imgbtnSelectAll.Enabled = True
        imgbtnRemoveOne.Enabled = True
        imgbtnRemoveAll.Enabled = True
        lstLeft.Enabled = True
    End Sub
    Protected Sub imgbtnSecurityCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSecurityCancel.Click
        'pnlSecurityMain.Visible = False
    End Sub
    Protected Sub imgbtnSecurityCreate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSecurityCreate.Click

    End Sub
    Protected Sub imgbtnSecurityDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSecurityDelete.Click
        If ValidateDelete(gvSecurity, lblSecurityMessage) = True Then
            For i As Integer = 0 To gvSecurity.Rows.Count - 1
                If CType(gvSecurity.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True Then
                    Dim findTheseVals(0) As Object
                    findTheseVals(0) = gvSecurity.Rows(i).Cells(1).Text.ToString

                    Dim foundRow As DataRow = CType(Session("dtSecurity"), DataTable).Rows.Find(findTheseVals)
                    If Not foundRow Is Nothing Then
                        CType(Session("dtSecurity"), DataTable).Rows.Remove(foundRow)
                    End If
                End If
            Next
            CType(Session("dtSecurity"), DataTable).AcceptChanges()
            gvSecurity.DataSource = Session("dtSecurity")
            gvSecurity.DataBind()
            SetDefaltValueSecurity()
        End If
    End Sub
    Protected Sub imgbtnSecurityEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSecurityEdit.Click
        If ValidateEdit(gvSecurity, lblSecurityMessage) = True Then
            pnlSecurityMain.Visible = True
            imgbtnSecuritySubmit.Visible = False
            imgbtnSecurityUpdate.Visible = True
            imgbtnSecurityRefresh.Visible = False
            ClearListBox(lstLeft)
            ClearListBox(lstRight)
            BindListBox(lstLeft)
            For i As Integer = 0 To gvSecurity.Rows.Count - 1
                If CType(gvSecurity.Rows(i).Cells(0).Controls(1), CheckBox).Checked = True Then
                    For j As Integer = 0 To lstLeft.Items.Count - 1
                        If lstLeft.Items(j).Value.ToString = gvSecurity.Rows(i).Cells(1).Text.ToString Then
                            lstLeft.Items(j).Selected = True
                            mySetting.AddDeleteListBoxItem(lstLeft, lstRight)
                            Exit For
                        End If
                    Next
                    mySetting.ArrangeDDLSelectedIndex(ddlSECURITY_OPTION_ALLOW_VIEW, clsGlobalSetting.DDLSelection.SelectedText, Replace(Replace(gvSecurity.Rows(i).Cells(2).Text.ToString.Trim, "&nbsp;", "&"), "&amp;", "&"))
                    mySetting.ArrangeDDLSelectedIndex(ddlSECURITY_OPTION_ALLOW_INSERT, clsGlobalSetting.DDLSelection.SelectedText, Replace(Replace(gvSecurity.Rows(i).Cells(3).Text.ToString.Trim, "&nbsp;", "&"), "&amp;", "&"))
                    mySetting.ArrangeDDLSelectedIndex(ddlSECURITY_OPTION_ALLOW_UPDATE, clsGlobalSetting.DDLSelection.SelectedText, Replace(Replace(gvSecurity.Rows(i).Cells(4).Text.ToString.Trim, "&nbsp;", "&"), "&amp;", "&"))
                    mySetting.ArrangeDDLSelectedIndex(ddlSECURITY_OPTION_ALLOW_DELETE, clsGlobalSetting.DDLSelection.SelectedText, Replace(Replace(gvSecurity.Rows(i).Cells(5).Text.ToString.Trim, "&nbsp;", "&"), "&amp;", "&"))
                    mySetting.ArrangeDDLSelectedIndex(ddlSECURITY_OPTION_ALLOW_PRINT, clsGlobalSetting.DDLSelection.SelectedText, Replace(Replace(gvSecurity.Rows(i).Cells(6).Text.ToString.Trim, "&nbsp;", "&"), "&amp;", "&"))
                    Exit For
                End If
            Next
            imgbtnSelectOne.Enabled = False
            imgbtnSelectAll.Enabled = False
            imgbtnRemoveOne.Enabled = False
            imgbtnRemoveAll.Enabled = False
            lstLeft.Enabled = False
        End If
    End Sub
    Protected Sub imgbtnSecurityRefresh_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSecurityRefresh.Click
        SetDefaltValueSecurity()
    End Sub
    Protected Sub imgbtnSecuritySubmit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSecuritySubmit.Click
        If ValidateInsertSecurity() = True Then
            For i As Integer = 0 To lstRight.Items.Count - 1
                Dim dr As DataRow = CType(Session("dtSecurity"), DataTable).NewRow
                dr(0) = lstRight.Items(i).Value.ToString
                dr(1) = ddlSECURITY_OPTION_ALLOW_VIEW.SelectedItem.Text.ToString
                dr(2) = ddlSECURITY_OPTION_ALLOW_INSERT.SelectedItem.Text.ToString
                dr(3) = ddlSECURITY_OPTION_ALLOW_UPDATE.SelectedItem.Text.ToString
                dr(4) = ddlSECURITY_OPTION_ALLOW_DELETE.SelectedItem.Text.ToString
                dr(5) = ddlSECURITY_OPTION_ALLOW_PRINT.SelectedItem.Text.ToString
                CType(Session("dtSecurity"), DataTable).Rows.Add(dr)
            Next
            CType(Session("dtSecurity"), DataTable).AcceptChanges()
            gvSecurity.DataSource = Session("dtSecurity")
            gvSecurity.DataBind()
            imgbtnSecurityCancel_Click(Nothing, Nothing)
            ClearListBox(lstLeft)
            ClearListBox(lstRight)
            BindListBox(lstLeft)
            EnabledPrevNext(True)
            SetDefaltValueSecurity()
        End If
    End Sub
    Protected Sub imgbtnSecurityUpdate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnSecurityUpdate.Click
        If ValidateUpdateSecurity() = True Then
            Dim dt As DataTable = CType(Session("dtSecurity"), DataTable)

            Dim findTheseVals(0) As Object
            findTheseVals(0) = lstRight.Items(0).Value.ToString

            Dim foundRow As DataRow = dt.Rows.Find(findTheseVals)
            If Not foundRow Is Nothing Then
                foundRow(1) = ddlSECURITY_OPTION_ALLOW_VIEW.SelectedItem.Text.ToString.Trim
                foundRow(2) = ddlSECURITY_OPTION_ALLOW_INSERT.SelectedItem.Text.ToString.Trim
                foundRow(3) = ddlSECURITY_OPTION_ALLOW_UPDATE.SelectedItem.Text.ToString.Trim
                foundRow(4) = ddlSECURITY_OPTION_ALLOW_DELETE.SelectedItem.Text.ToString.Trim
                foundRow(5) = ddlSECURITY_OPTION_ALLOW_PRINT.SelectedItem.Text.ToString.Trim
                dt.AcceptChanges()
            End If
            Session("dtSecurity") = dt
            gvSecurity.DataSource = dt
            gvSecurity.DataBind()
            dt = Nothing
            imgbtnSecurityCancel_Click(Nothing, Nothing)
            SetDefaltValueSecurity()
        End If
    End Sub
    Private Sub SetDefaltValueSecurity()
        mySetting.SetDropdownlistDefaultIndex(ddlSECURITY_OPTION_ALLOW_VIEW, 1)
        mySetting.SetDropdownlistDefaultIndex(ddlSECURITY_OPTION_ALLOW_INSERT, 1)
        mySetting.SetDropdownlistDefaultIndex(ddlSECURITY_OPTION_ALLOW_UPDATE, 1)
        mySetting.SetDropdownlistDefaultIndex(ddlSECURITY_OPTION_ALLOW_DELETE, 1)
        mySetting.SetDropdownlistDefaultIndex(ddlSECURITY_OPTION_ALLOW_PRINT, 1)
    End Sub
    Private Function ValidateSecurityNext() As Boolean
        If gvSecurity.Rows.Count = 0 Then
            lblSecurityMessage.Text = "No field was created for the [Security]!"
            imgbtnSecurityAdd_Click(Nothing, Nothing)
            Return False
        End If
        Return True
    End Function
#End Region

#Region "Generate Page"
    Private Function ValidateGenerate() As Boolean
        If ValidatePageNext() = False Then
            ValidatePageNext()
            lblMessage.Text = lblPageMessage.Text
            SetActiveViewIndex(0)
            Return False
        End If
        If ValidateFieldNext() = False Then
            ValidateFieldNext()
            lblMessage.Text = lblFieldMessage.Text
            SetActiveViewIndex(1)
            Return False
        End If
        If ValidateOptionNext() = False Then
            ValidateOptionNext()
            lblMessage.Text = lblOptionMessage.Text
            SetActiveViewIndex(2)
            Return False
        End If
        If ValidateLookupNext() = False Then
            ValidateLookupNext()
            lblMessage.Text = lblLookupMessage.Text
            SetActiveViewIndex(3)
            Return False
        End If
        If ValidateSecurityNext() = False Then
            ValidateSecurityNext()
            lblMessage.Text = lblSecurityMessage.Text
            SetActiveViewIndex(4)
            Return False
        End If
        If ValidateTableExists() = False Then
            lblMessage.Text = lblFieldMessage.Text
            SetActiveViewIndex(0)
            Return False
        End If
        strWebFileToCreate = System.AppDomain.CurrentDomain.BaseDirectory & "Pages\" & txtMODULE_PROFILE_CODE.Text.ToString & "\" & txtTABLE.Text.ToString & ".aspx"
        If File.Exists(strWebFileToCreate) Then
            lblMessage.Text = "[" & strWebFileToCreate & "] already exists!"
            Return False
        End If
        strVBFileToCreate = System.AppDomain.CurrentDomain.BaseDirectory & "Pages\" & txtMODULE_PROFILE_CODE.Text.ToString & "\" & txtTABLE.Text.ToString & ".aspx.vb"
        If File.Exists(strVBFileToCreate) Then
            lblMessage.Text = "[" & strVBFileToCreate & "] already exists!"
            Return False
        End If

        Return True
    End Function
    Private Function ValidateTableExists() As Boolean
        ssql = "Exec sp_sa_CreateNewPage '" & Session("Company").ToString & "','" & _
                txtMODULE_PROFILE_CODE.Text.ToString.Trim & "','" & txtTABLE.Text.ToString.Trim & "','VALIDATE'"
        Dim ds As New DataSet
        ds = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If Not ds Is Nothing Then
            Select Case ds.Tables(0).Rows(0).Item(0).ToString
                Case ErrorCode.ModuleExist
                    'Do nothing
                Case ErrorCode.TableFieldExist
                    lblFieldMessage.Text = "Duplicate data in [Table Field]!"
                    txtTABLE.Focus()
                    Return False
                Case ErrorCode.TableExist
                    lblFieldMessage.Text = "[Table] with name (" & txtTABLE.Text.ToString & ") already exists! Duplicate record found!"
                    txtTABLE.Focus()
                    Return False
                Case ErrorCode.ModuleAndTableExist
                    'Do nothing
            End Select
        Else
            lblMessage.Text = "Error occur in validating input before create new page!"
        End If
        Return True
    End Function
    Protected Sub lnkbtnSecurityNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnSecurityNext.Click
        If ValidateGenerate() = True Then
            lblMessage.Text = "Success"
            Dim strFilePath As String = System.AppDomain.CurrentDomain.BaseDirectory & "Pages\" & Session("Module").ToString & "\"
            GenerateTableFieldSQL(strFilePath)
            'GenerateASPX_VB(strFilePath)
            GenerateOptionSQL()
            GenerateLookupSQL()
            GenerateLookupViewIfFieldCodeNameExists()
            GenerateSecuritySQL()
        End If
        If lblMessage.Text.ToString.Trim = "" Then
            Response.Redirect(Form.ID.ToString & ".aspx")
            Exit Sub
        End If
        If lblMessage.Text.ToString.Trim = "Process done successfully." Then
            Response.Redirect(Form.ID.ToString & ".aspx")
            Exit Sub
        End If
    End Sub
    Private Sub GenerateLookupSQL()
        Dim htb As New Hashtable
        For i = 0 To gvLookup.Rows.Count - 1
            If gvLookup.Rows(i).Cells(3).Text.ToString.Trim.Replace("&nbsp;", "") = "" Then
                htb.Add("ssql" & CStr(i), "Insert Into [Lookup_Profile] (Company_Profile_Code,Module_Profile_Code,Table_Profile_Code,Table_Field_Code,Table_Profile_Code_Lookup,Type)  " & _
                        "Values(N'" & Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.ToUpper.Trim & "',N'" & txtTABLE.Text.ToString.ToUpper.Trim & "',N'" & _
                        gvLookup.Rows(i).Cells(1).Text.ToString.ToUpper & "',N'" & gvLookup.Rows(i).Cells(2).Text.ToString.ToUpper & "','')")
            Else
                htb.Add("ssql" & CStr(i), "Insert Into [Lookup_Profile] (Company_Profile_Code,Module_Profile_Code,Table_Profile_Code,Table_Field_Code,Table_Profile_Code_Lookup,Type)  " & _
                        "Values(N'" & Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.ToUpper.Trim & "',N'" & txtTABLE.Text.ToString.ToUpper.Trim & "',N'" & _
                        gvLookup.Rows(i).Cells(1).Text.ToString.ToUpper & "',N'" & gvLookup.Rows(i).Cells(2).Text.ToString.ToUpper & "','''" & gvLookup.Rows(i).Cells(3).Text.ToString.ToUpper & "''')")
            End If
        Next
        If htb.Count > 0 Then
            mySQL.ExecuteSQLTransactionByHashtable(htb, "Insert Lookup", Session("Company").ToString, Session("EmpID").ToString)
        End If
        htb = Nothing
        htb = New Hashtable
        For i As Integer = 0 To gvLookup.Rows.Count - 1
            If gvLookup.Rows(i).Cells(3).Text.ToString.Trim.Replace("&nbsp;", "") <> "" Then
                htb.Add(i.ToString & "Select", "INSERT INTO USER_DEFINE_FUNCTION(Company_Profile_code,Module_Profile_Code,Table_Profile_Code,Table_Field_Code,Query_Action,Function_Name,Default_Value,Default_Value2,Default_Value3,Remark) Values(N'" & _
                Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & "',N'" & txtTABLE.Text.ToString.Trim & "',N'" & _
                gvLookup.Rows(i).Cells(1).Text.ToString.ToUpper & "',N'SELECT',N'dbo.fn_GetCOMOCPCodeNameByID',N'',N'COMPANY',N'',N'')")

                htb.Add(i.ToString & "Insert", "INSERT INTO USER_DEFINE_FUNCTION(Company_Profile_code,Module_Profile_Code,Table_Profile_Code,Table_Field_Code,Query_Action,Function_Name,Default_Value,Default_Value2,Default_Value3,Remark) Values(N'" & _
                Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & "',N'" & txtTABLE.Text.ToString.Trim & "',N'" & _
                gvLookup.Rows(i).Cells(1).Text.ToString.ToUpper & "',N'INSERT',N'dbo.fn_GetCOMOCPIDByCodeName',N'" & gvLookup.Rows(i).Cells(3).Text.ToString.ToUpper & "',N'COMPANY',N'',N'')")

                htb.Add(i.ToString & "Update", "INSERT INTO USER_DEFINE_FUNCTION(Company_Profile_code,Module_Profile_Code,Table_Profile_Code,Table_Field_Code,Query_Action,Function_Name,Default_Value,Default_Value2,Default_Value3,Remark) Values(N'" & _
                Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & "',N'" & txtTABLE.Text.ToString.Trim & "',N'" & _
                gvLookup.Rows(i).Cells(1).Text.ToString.ToUpper & "',N'UPDATE',N'dbo.fn_GetCOMOCPIDByCodeName',N'" & gvLookup.Rows(i).Cells(3).Text.ToString.ToUpper & "',N'COMPANY',N'',N'')")

                htb.Add(i.ToString & "Delete", "INSERT INTO USER_DEFINE_FUNCTION(Company_Profile_code,Module_Profile_Code,Table_Profile_Code,Table_Field_Code,Query_Action,Function_Name,Default_Value,Default_Value2,Default_Value3,Remark) Values(N'" & _
                Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & "',N'" & txtTABLE.Text.ToString.Trim & "',N'" & _
                gvLookup.Rows(i).Cells(1).Text.ToString.ToUpper & "',N'DELETE',N'dbo.fn_GetCOMOCPIDByCodeName',N'" & gvLookup.Rows(i).Cells(3).Text.ToString.ToUpper & "',N'COMPANY',N'',N'')")
            Else
                htb.Add(i.ToString & "Select", "INSERT INTO USER_DEFINE_FUNCTION(Company_Profile_code,Module_Profile_Code,Table_Profile_Code,Table_Field_Code,Query_Action,Function_Name,Default_Value,Default_Value2,Default_Value3,Remark) Values(N'" & _
                Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & "',N'" & txtTABLE.Text.ToString.Trim & "',N'" & _
                gvLookup.Rows(i).Cells(1).Text.ToString.ToUpper & "',N'SELECT',N'dbo.fn_GetCodeName_" & gvLookup.Rows(i).Cells(2).Text.ToString & "_Vw',N'',N'',N'',N'')")

                htb.Add(i.ToString & "Insert", "INSERT INTO USER_DEFINE_FUNCTION(Company_Profile_code,Module_Profile_Code,Table_Profile_Code,Table_Field_Code,Query_Action,Function_Name,Default_Value,Default_Value2,Default_Value3,Remark) Values(N'" & _
                Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & "',N'" & txtTABLE.Text.ToString.Trim & "',N'" & _
                gvLookup.Rows(i).Cells(1).Text.ToString.ToUpper & "',N'INSERT',N'dbo.fn_GetCode_" & gvLookup.Rows(i).Cells(2).Text.ToString & "_Vw',N'',N'',N'',N'')")

                htb.Add(i.ToString & "Update", "INSERT INTO USER_DEFINE_FUNCTION(Company_Profile_code,Module_Profile_Code,Table_Profile_Code,Table_Field_Code,Query_Action,Function_Name,Default_Value,Default_Value2,Default_Value3,Remark) Values(N'" & _
                Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & "',N'" & txtTABLE.Text.ToString.Trim & "',N'" & _
                gvLookup.Rows(i).Cells(1).Text.ToString.ToUpper & "',N'UPDATE',N'dbo.fn_GetCode_" & gvLookup.Rows(i).Cells(2).Text.ToString & "_Vw',N'',N'',N'',N'')")

                htb.Add(i.ToString & "Delete", "INSERT INTO USER_DEFINE_FUNCTION(Company_Profile_code,Module_Profile_Code,Table_Profile_Code,Table_Field_Code,Query_Action,Function_Name,Default_Value,Default_Value2,Default_Value3,Remark) Values(N'" & _
                Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & "',N'" & txtTABLE.Text.ToString.Trim & "',N'" & _
                gvLookup.Rows(i).Cells(1).Text.ToString.ToUpper & "',N'DELETE',N'dbo.fn_GetCode_" & gvLookup.Rows(i).Cells(2).Text.ToString & "_Vw',N'',N'',N'',N'')")
            End If
        Next
        If htb.Count > 0 Then
            mySQL.ExecuteSQLTransactionByHashtable(htb, "Insert Lookup", Session("Company").ToString, Session("EmpID").ToString)
        End If
    End Sub
    Private Sub GenerateLookupViewIfFieldCodeNameExists()
        Dim FieldCodeExist As Boolean = False, FieldNameExist As Boolean = False

        For i As Integer = 0 To gvField.Rows.Count - 1
            If gvField.Rows(i).Cells(2).Text.ToString.ToUpper.Trim = "CODE" Then
                FieldCodeExist = True
                Exit For
            End If
        Next
        For i As Integer = 0 To gvField.Rows.Count - 1
            If gvField.Rows(i).Cells(2).Text.ToString.ToUpper.Trim = "NAME" Then
                FieldNameExist = True
                Exit For
            End If
        Next
        If FieldCodeExist = True And FieldNameExist = True Then
            mySQL.ExecuteSQLNonQuery("CREATE VIEW [dbo].[" & txtTABLE.Text.ToString.Trim & "_Vw]" & vbCrLf & _
                            "AS " & vbCrLf & _
                            "SELECT Code + ' [' + Name + ']' AS CodeName, Code, Name" & vbCrLf & _
                            "FROM dbo.[" & txtTABLE.Text.ToString.Trim & "]", Session("Company").ToString, Session("EmpID").ToString)

            mySQL.ExecuteSQLNonQuery("CREATE FUNCTION [dbo].[fn_GetCode_" & txtTABLE.Text.ToString.Trim & "_Vw](@CodeName nvarchar(100))" & vbCrLf & _
                    "RETURNS nvarchar(100)" & vbCrLf & _
                    "AS " & vbCrLf & _
                    "BEGIN" & vbCrLf & _
                    vbTab & "RETURN(SELECT Distinct Code From " & txtTABLE.Text.ToString.Trim & "_Vw Where CodeName = @CodeName Or Code = @CodeName Or [Name] = @CodeName)" & vbCrLf & _
                    "END", Session("Company").ToString, Session("EmpID").ToString)

            mySQL.ExecuteSQLNonQuery("CREATE FUNCTION [dbo].[fn_GetCodeName_" & txtTABLE.Text.ToString.Trim & "_Vw](@Code nvarchar(100))" & vbCrLf & _
               "RETURNS nvarchar(100)" & vbCrLf & _
               "AS " & vbCrLf & _
               "BEGIN" & vbCrLf & _
               vbTab & "RETURN(SELECT Distinct CodeName From " & txtTABLE.Text.ToString.Trim & "_Vw Where Code = @Code)" & vbCrLf & _
               "END", Session("Company").ToString, Session("EmpID").ToString)
        End If
        
    End Sub
    Private Sub GenerateOptionSQL()
        Dim htb As New Hashtable
        For i = 0 To gvOption.Rows.Count - 1
            htb.Add("ssql" & CStr(i), "Insert Into [Option] (Table_Profile_Code,Table_Field_Code,Code,Name,Sort_Key,Option_Default) " & _
                    "Values(N'" & txtTABLE.Text.ToString.Trim & "',N'" & gvOption.Rows(i).Cells(2).Text.ToString.ToUpper.Trim & "',N'" & _
                    gvOption.Rows(i).Cells(3).Text.ToString.ToUpper.Trim & "',N'" & _
                    gvOption.Rows(i).Cells(4).Text.ToString.Trim & "',N'" & _
                    gvOption.Rows(i).Cells(1).Text.ToString.ToUpper.Trim & "',N'" & gvOption.Rows(i).Cells(5).Text.ToString.ToUpper.Trim & "')")
        Next
        If htb.Count > 0 Then
            mySQL.ExecuteSQLTransactionByHashtable(htb, "Insert Option", Session("Company").ToString, Session("EmpID").ToString)
        End If
    End Sub
    Private Sub GenerateSecuritySQL()
        Dim htb As New Hashtable
        For i = 0 To gvSecurity.Rows.Count - 1
            htb.Add("ssql" & CStr(i), "Exec sp_sa_ADD_UserSecurity '" & Session("Company").ToString & "','" & txtMODULE_PROFILE_CODE.Text.ToString & "','" & _
                    gvSecurity.Rows(i).Cells(1).Text.ToString & "','" & txtTABLE.Text.ToString & "','" & gvSecurity.Rows(i).Cells(2).Text.ToString.ToUpper & "','" & _
                    gvSecurity.Rows(i).Cells(3).Text.ToString.ToUpper & "','" & gvSecurity.Rows(i).Cells(4).Text.ToString.ToUpper & "','" & _
                    gvSecurity.Rows(i).Cells(5).Text.ToString.ToUpper & "','" & gvSecurity.Rows(i).Cells(6).Text.ToString.ToUpper & "','INSERT'")
        Next
        If htb.Count > 0 Then
            mySQL.ExecuteSQLTransactionByHashtable(htb, "Insert User Security", Session("Company").ToString, Session("EmpID").ToString)
        End If
    End Sub
    Private Sub GenerateTableFieldSQL(ByVal strFilePath As String)
        Try
            Dim IsEmployeeFieldExists As Boolean = False, IsCompanyFieldExists As Boolean = False
            ssql = "Exec sp_sa_CreateNewPage '" & Session("Company").ToString & _
                    "','" & txtMODULE_PROFILE_CODE.Text.ToString & "','" & _
                    txtTABLE.Text.ToString & "','INSERT'"
            Dim htb As New Hashtable
            htb.Add("ssql1", ssql)
            Dim strInsertSQL As String, strInsertSQL2 As String, strInsertSQLView As String, strInsertSQLView2 As String
            strInsertSQL = "Insert Into Table_Field(Table_Profile_Code,Sequence_No," & _
                        "Code,Name,Option_Primary_Key,Physical_Name,Option_Data_Type," & _
                        "Length,No_of_Decimal,Option_View,Option_Mandatory,Option_Editable," & _
                        "Option_Password,Option_Set_Filter,Min_Value_Integer," & _
                        "Max_Value_Integer,Min_Value_Decimal,Max_Value_Decimal," & _
                        "Option_Default_Value,Default_Value_Character,Default_Value_Date," & _
                        "Default_Value_DateTime,Default_Value_Time,Default_Value_Integer," & _
                        "Default_Value_Decimal,Column_Width,Field_Position,Option_View_List," & _
                        "Option_View_Card,Default_Value_Lookup,Option_Enabled) "
            strInsertSQLView = strInsertSQL
            strInsertSQL2 = ""
            Dim ssqlCreateView As String = "Create View [dbo].[" & txtTABLE.Text.ToString.Trim & "_CARD_VW] As SELECT "
            For i = 0 To gvField.Rows.Count - 1

                strInsertSQL2 = " Values(N'" & txtTABLE.Text.ToString & "','" & _
                    gvField.Rows(i).Cells(1).Text.ToString & "',N'" & gvField.Rows(i).Cells(2).Text.ToString & _
                    "',N'" & gvField.Rows(i).Cells(3).Text.ToString & "',N'" & gvField.Rows(i).Cells(4).Text.ToString.ToUpper & _
                    "',N'" & gvField.Rows(i).Cells(2).Text.ToString & "',N'" & gvField.Rows(i).Cells(5).Text.ToString.ToUpper & "',"

                strInsertSQLView2 = " Values(N'" & txtTABLE.Text.ToString & "_CARD_VW','" & _
                    gvField.Rows(i).Cells(1).Text.ToString & "',N'" & gvField.Rows(i).Cells(2).Text.ToString & _
                    "',N'" & gvField.Rows(i).Cells(3).Text.ToString & "',N'" & gvField.Rows(i).Cells(4).Text.ToString.ToUpper & _
                    "',N'" & gvField.Rows(i).Cells(2).Text.ToString & "',N'" & gvField.Rows(i).Cells(5).Text.ToString.ToUpper & "',"

                Select Case gvField.Rows(i).Cells(5).Text.ToString.ToUpper
                    Case "OPTION"
                        strInsertSQL2 = strInsertSQL2 & _
                            "'" & gvField.Rows(i).Cells(6).Text.ToString.ToUpper.ToUpper & "','0','YES','" & gvField.Rows(i).Cells(9).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(8).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(10).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(11).Text.ToString.ToUpper & "','0','0','0','0','NO','','','','','0','0','0','0','" & gvField.Rows(i).Cells(12).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(13).Text.ToString.ToUpper & "','','YES')"
                        strInsertSQLView2 = strInsertSQLView2 & _
                            "'" & gvField.Rows(i).Cells(6).Text.ToString.ToUpper & "','0','YES','" & gvField.Rows(i).Cells(9).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(8).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(10).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(11).Text.ToString.ToUpper & "','0','0','0','0','NO','','','','','0','0','0','0','" & gvField.Rows(i).Cells(12).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(13).Text.ToString.ToUpper & "','','YES')"
                    Case "DATE"
                        strInsertSQL2 = strInsertSQL2 & _
                            "'" & gvField.Rows(i).Cells(6).Text.ToString.ToUpper & "','0','YES','" & gvField.Rows(i).Cells(9).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(8).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(10).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(11).Text.ToString.ToUpper & "','0','0','0','0','NO','','19000101000000','','','0','0','0','0','" & gvField.Rows(i).Cells(12).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(13).Text.ToString.ToUpper & "','','YES')"
                        strInsertSQLView2 = strInsertSQLView2 & _
                            "'" & gvField.Rows(i).Cells(6).Text.ToString.ToUpper & "','0','YES','" & gvField.Rows(i).Cells(9).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(8).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(10).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(11).Text.ToString.ToUpper & "','0','0','0','0','NO','','19000101000000','','','0','0','0','0','" & gvField.Rows(i).Cells(12).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(13).Text.ToString.ToUpper & "','','YES')"
                    Case "DATETIME"
                        strInsertSQL2 = strInsertSQL2 & _
                            "'" & gvField.Rows(i).Cells(6).Text.ToString.ToUpper & "','0','YES','" & gvField.Rows(i).Cells(9).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(8).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(10).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(11).Text.ToString.ToUpper & "','0','0','0','0','NO','','','19000101000000','','0','0','0','0','" & gvField.Rows(i).Cells(12).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(13).Text.ToString.ToUpper & "','','YES')"
                        strInsertSQLView2 = strInsertSQLView2 & _
                            "'" & gvField.Rows(i).Cells(6).Text.ToString.ToUpper & "','0','YES','" & gvField.Rows(i).Cells(9).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(8).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(10).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(11).Text.ToString.ToUpper & "','0','0','0','0','NO','','','19000101000000','','0','0','0','0','" & gvField.Rows(i).Cells(12).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(13).Text.ToString.ToUpper & "','','YES')"
                    Case "TIME"
                        strInsertSQL2 = strInsertSQL2 & _
                            "'" & gvField.Rows(i).Cells(6).Text.ToString.ToUpper & "','0','YES','" & gvField.Rows(i).Cells(9).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(8).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(10).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(11).Text.ToString.ToUpper & "','0','0','0','0','NO','','','','000000','0','0','0','0','" & gvField.Rows(i).Cells(12).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(13).Text.ToString.ToUpper & "','','YES')"
                        strInsertSQLView2 = strInsertSQLView2 & _
                            "'" & gvField.Rows(i).Cells(6).Text.ToString.ToUpper & "','0','YES','" & gvField.Rows(i).Cells(9).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(8).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(10).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(11).Text.ToString.ToUpper & "','0','0','0','0','NO','','','','000000','0','0','0','0','" & gvField.Rows(i).Cells(12).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(13).Text.ToString.ToUpper & "','','YES')"
                    Case "LOOKUP"
                        strInsertSQL2 = strInsertSQL2 & _
                            "'" & gvField.Rows(i).Cells(6).Text.ToString.ToUpper & "','0','YES','" & gvField.Rows(i).Cells(9).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(8).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(10).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(11).Text.ToString.ToUpper & "','0','0','0','0','NO','','','','','0','0','0','0','" & gvField.Rows(i).Cells(12).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(13).Text.ToString.ToUpper & "','','YES')"
                        strInsertSQLView2 = strInsertSQLView2 & _
                            "'" & gvField.Rows(i).Cells(6).Text.ToString.ToUpper & "','0','YES','" & gvField.Rows(i).Cells(9).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(8).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(10).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(11).Text.ToString.ToUpper & "','0','0','0','0','NO','','','','','0','0','0','0','" & gvField.Rows(i).Cells(12).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(13).Text.ToString.ToUpper & "','','YES')"
                    Case "CHARACTER"
                        strInsertSQL2 = strInsertSQL2 & _
                            "'" & gvField.Rows(i).Cells(6).Text.ToString.ToUpper & "','0','YES','" & gvField.Rows(i).Cells(9).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(8).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(10).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(11).Text.ToString.ToUpper & "','0','0','0','0','NO','','','','','0','0','0','0','" & gvField.Rows(i).Cells(12).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(13).Text.ToString.ToUpper & "','','YES')"
                        strInsertSQLView2 = strInsertSQLView2 & _
                            "'" & gvField.Rows(i).Cells(6).Text.ToString.ToUpper & "','0','YES','" & gvField.Rows(i).Cells(9).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(8).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(10).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(11).Text.ToString.ToUpper & "','0','0','0','0','NO','','','','','0','0','0','0','" & gvField.Rows(i).Cells(12).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(13).Text.ToString.ToUpper & "','','YES')"
                    Case "INTEGER"
                        strInsertSQL2 = strInsertSQL2 & _
                            "'" & gvField.Rows(i).Cells(6).Text.ToString.ToUpper & "','0','YES','" & gvField.Rows(i).Cells(9).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(8).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(10).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(11).Text.ToString.ToUpper & "','0','0','0','0','NO','','','','','0','0','0','0','" & gvField.Rows(i).Cells(12).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(13).Text.ToString.ToUpper & "','','YES')"
                        strInsertSQLView2 = strInsertSQLView2 & _
                            "'" & gvField.Rows(i).Cells(6).Text.ToString.ToUpper & "','0','YES','" & gvField.Rows(i).Cells(9).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(8).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(10).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(11).Text.ToString.ToUpper & "','0','0','0','0','NO','','','','','0','0','0','0','" & gvField.Rows(i).Cells(12).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(13).Text.ToString.ToUpper & "','','YES')"
                    Case "DECIMAL"
                        strInsertSQL2 = strInsertSQL2 & _
                            "'" & gvField.Rows(i).Cells(6).Text.ToString.ToUpper & "','0','YES','" & gvField.Rows(i).Cells(9).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(8).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(10).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(11).Text.ToString.ToUpper & "','0','0','0','0','NO','','','','','0','0','0','0','" & gvField.Rows(i).Cells(12).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(13).Text.ToString.ToUpper & "','','YES')"
                        strInsertSQLView2 = strInsertSQLView2 & _
                            "'" & gvField.Rows(i).Cells(6).Text.ToString.ToUpper & "','0','YES','" & gvField.Rows(i).Cells(9).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(8).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(10).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(11).Text.ToString.ToUpper & "','0','0','0','0','NO','','','','','0','0','0','0','" & gvField.Rows(i).Cells(12).Text.ToString.ToUpper & "','" & gvField.Rows(i).Cells(13).Text.ToString.ToUpper & "','','YES')"
                End Select

                Select Case gvField.Rows(i).Cells(2).Text.ToString.ToUpper
                    Case "EMPLOYEE_PROFILE_ID"
                        IsEmployeeFieldExists = True
                        htb.Add("ssqlUDF" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & (i + 1), "Insert Into User_Define_Function(Company_Profile_Code,Module_Profile_Code," & _
                                "Table_Profile_Code,Table_Field_Code,Query_Action,Function_Name,Default_Value,Default_Value2,Default_Value3,Remark) " & _
                                "Values(N'" & Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & _
                                "',N'" & txtTABLE.Text.ToString.ToUpper.Trim & "',N'" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & _
                                "',N'SELECT',N'dbo.fn_GetEmpCodeName','','','','')")
                        htb.Add("ssqlUDF" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & (i + 2), "Insert Into User_Define_Function(Company_Profile_Code,Module_Profile_Code," & _
                                "Table_Profile_Code,Table_Field_Code,Query_Action,Function_Name,Default_Value,Default_Value2,Default_Value3,Remark) " & _
                                "Values(N'" & Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & _
                                "',N'" & txtTABLE.Text.ToString.ToUpper.Trim & "',N'" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & _
                                "',N'INSERT',N'dbo.fn_ReturnEmpIDByCodeName','','','','')")
                        htb.Add("ssqlUDF" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & (i + 3), "Insert Into User_Define_Function(Company_Profile_Code,Module_Profile_Code," & _
                                "Table_Profile_Code,Table_Field_Code,Query_Action,Function_Name,Default_Value,Default_Value2,Default_Value3,Remark) " & _
                                "Values(N'" & Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & _
                                "',N'" & txtTABLE.Text.ToString.ToUpper.Trim & "',N'" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & _
                                "',N'UPDATE','dbo.fn_ReturnEmpIDByCodeName','','','','')")
                        htb.Add("ssqlUDF" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & (i + 4), "Insert Into User_Define_Function(Company_Profile_Code,Module_Profile_Code," & _
                                "Table_Profile_Code,Table_Field_Code,Query_Action,Function_Name,Default_Value,Default_Value2,Default_Value3,Remark) " & _
                                "Values(N'" & Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & _
                                "',N'" & txtTABLE.Text.ToString.ToUpper.Trim & "',N'" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & _
                                "',N'DELETE','dbo.fn_ReturnEmpIDByCodeName','','','','')")
                    Case "COMPANY_PROFILE_CODE"
                        IsCompanyFieldExists = True
                        htb.Add("ssqlUDF" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & (i + 1), "Insert Into User_Define_Function(Company_Profile_Code,Module_Profile_Code," & _
                                "Table_Profile_Code,Table_Field_Code,Query_Action,Function_Name,Default_Value,Default_Value2,Default_Value3,Remark) " & _
                                "Values(N'" & Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & _
                                "',N'" & txtTABLE.Text.ToString.ToUpper.Trim & "',N'" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & _
                                "',N'SELECT',N'dbo.fn_GetCompanyProfileCodeName','','','','')")
                        htb.Add("ssqlUDF" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & (i + 2), "Insert Into User_Define_Function(Company_Profile_Code,Module_Profile_Code," & _
                                "Table_Profile_Code,Table_Field_Code,Query_Action,Function_Name,Default_Value,Default_Value2,Default_Value3,Remark) " & _
                                "Values(N'" & Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & _
                                "',N'" & txtTABLE.Text.ToString.ToUpper.Trim & "',N'" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & _
                                "',N'INSERT',N'dbo.fn_ReturnCompanyProfileCode','','','','')")
                        htb.Add("ssqlUDF" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & (i + 3), "Insert Into User_Define_Function(Company_Profile_Code,Module_Profile_Code," & _
                                "Table_Profile_Code,Table_Field_Code,Query_Action,Function_Name,Default_Value,Default_Value2,Default_Value3,Remark) " & _
                                "Values(N'" & Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & _
                                "',N'" & txtTABLE.Text.ToString.ToUpper.Trim & "',N'" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & _
                                "',N'UPDATE',N'dbo.fn_ReturnCompanyProfileCode','','','','')")
                        htb.Add("ssqlUDF" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & (i + 4), "Insert Into User_Define_Function(Company_Profile_Code,Module_Profile_Code," & _
                                "Table_Profile_Code,Table_Field_Code,Query_Action,Function_Name,Default_Value,Default_Value2,Default_Value3,Remark) " & _
                                "Values(N'" & Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & _
                                "',N'" & txtTABLE.Text.ToString.ToUpper.Trim & "',N'" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & _
                                "',N'DELETE',N'dbo.fn_ReturnCompanyProfileCode','','','','')")
                End Select

                Select Case gvField.Rows(i).Cells(5).Text.ToString.ToUpper
                    Case "LOOKUP"
                        Select Case gvField.Rows(i).Cells(2).Text.ToString.ToUpper
                            Case "EMPLOYEE_PROFILE_ID"
                                htb.Add("ssqlLOOKUP" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & (i + 1), "Insert Into Lookup_Profile(company_profile_code,module_profile_code,table_profile_code,table_field_code,table_profile_code_lookup,type) " & _
                                    "Values(N'" & Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & _
                                    "',N'" & txtTABLE.Text.ToString.Trim & "',N'" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & _
                                    "',N'Employee_CodeName_Vw','')")
                                ssqlCreateView = ssqlCreateView & "dbo.fn_GetEmpCodeNameByID(dbo.[Employee_CodeName_Vw].[Company_Profile_Code],dbo.[" & txtTABLE.Text.ToString.Trim & "]." & "[" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & "]) As " & "[" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & "],"
                            Case "COMPANY_PROFILE_CODE"
                                htb.Add("ssqlLOOKUP" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & (i + 1), "Insert Into Lookup_Profile(company_profile_code,module_profile_code,table_profile_code,table_field_code,table_profile_code_lookup,type) " & _
                                    "Values(N'" & Session("Company").ToString & "',N'" & txtMODULE_PROFILE_CODE.Text.ToString.Trim & _
                                    "',N'" & txtTABLE.Text.ToString.Trim & "',N'" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & _
                                    "',N'COMPANY_PROFILE','')")
                                ssqlCreateView = ssqlCreateView & "dbo.fn_GetCompanyProfileCodeName(dbo.[" & txtTABLE.Text.ToString.Trim & "].[" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & "]) As " & "[" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & "],"
                            Case Else
                                ssqlCreateView = ssqlCreateView & "dbo.[" & txtTABLE.Text.ToString.Trim & "].[" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & "],"
                        End Select
                    Case Else
                        ssqlCreateView = ssqlCreateView & "dbo.[" & txtTABLE.Text.ToString.Trim & "].[" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & "],"
                End Select
                htb.Add("ssql" & (i + 2), strInsertSQL & strInsertSQL2)
                htb.Add("ssqlVw" & (i + 1), strInsertSQLView & strInsertSQLView2)
            Next
            If AddDefaultField = True Then
                ssqlCreateView = ssqlCreateView & "[USER_PROFILE_CODE_CREATE],"
                ssqlCreateView = ssqlCreateView & "[DATETIME_CREATE],"
                ssqlCreateView = ssqlCreateView & "[USER_PROFILE_CODE_MODIFY],"
                ssqlCreateView = ssqlCreateView & "[DATETIME_MODIFY],"

                strInsertSQL2 = " Values(N'" & txtTABLE.Text.ToString & "','" & _
                        ((gvField.Rows.Count + 1) * 10) & "',N'" & "USER_PROFILE_CODE_CREATE" & _
                        "',N'" & "User Profile Code Create" & "',N'" & "NO" & _
                        "',N'" & "User_Profile_Code_Create" & "',N'" & "LOOKUP" & "'," & _
                        "'50','0','YES','YES','YES','NO','YES','0','0','0','0','NO','','','','','0','0','0','0','NO','NO','','YES')"
                htb.Add("ssqlDefaultField1", strInsertSQL & strInsertSQL2)

                strInsertSQL2 = " Values(N'" & txtTABLE.Text.ToString & "','" & _
                    ((gvField.Rows.Count + 2) * 10) & "',N'" & "DATETIME_CREATE" & _
                    "',N'" & "Datetime Create" & "',N'" & "NO" & _
                    "',N'" & "Datetime_Code_Create" & "',N'" & "DATETIME" & "'," & _
                    "'50','0','YES','YES','YES','NO','YES','0','0','0','0','NO','','','','','0','0','0','0','NO','NO','','YES')"
                htb.Add("ssqlDefaultField2", strInsertSQL & strInsertSQL2)

                strInsertSQL2 = " Values(N'" & txtTABLE.Text.ToString & "','" & _
                    ((gvField.Rows.Count + 3) * 10) & "',N'" & "USER_PROFILE_CODE_MODIFY" & _
                    "',N'" & "User Profile Code Modify" & "',N'" & "NO" & _
                    "',N'" & "User_Profile_Code_Modify" & "',N'" & "LOOKUP" & "'," & _
                    "'50','0','YES','YES','YES','NO','YES','0','0','0','0','NO','','','','','0','0','0','0','NO','NO','','YES')"
                htb.Add("ssqlDefaultField3", strInsertSQL & strInsertSQL2)

                strInsertSQL2 = " Values(N'" & txtTABLE.Text.ToString & "','" & _
                    ((gvField.Rows.Count + 4) * 10) & "',N'" & "DATETIME_MODIFY" & _
                    "',N'" & "Datetime Modify" & "',N'" & "NO" & _
                    "',N'" & "Datetime_Modify" & "',N'" & "DATETIME" & "'," & _
                    "'50','0','YES','YES','YES','NO','YES','0','0','0','0','NO','','','','','0','0','0','0','NO','NO','','YES')"
                htb.Add("ssqlDefaultField4", strInsertSQL & strInsertSQL2)

                strInsertSQLView2 = " Values(N'" & txtTABLE.Text.ToString & "_CARD_VW','" & _
                        ((gvField.Rows.Count + 1) * 10) & "',N'" & "USER_PROFILE_CODE_CREATE" & _
                        "',N'" & "User Profile Code Create" & "',N'" & "NO" & _
                        "',N'" & "User_Profile_Code_Create" & "',N'" & "LOOKUP" & "'," & _
                        "'50','0','YES','YES','YES','NO','YES','0','0','0','0','NO','','','','','0','0','0','0','NO','NO','','YES')"
                htb.Add("ssqlDefaultView1", strInsertSQLView & strInsertSQLView2)

                strInsertSQLView2 = " Values(N'" & txtTABLE.Text.ToString & "_CARD_VW','" & _
                    ((gvField.Rows.Count + 2) * 10) & "',N'" & "DATETIME_CREATE" & _
                    "',N'" & "Datetime Create" & "',N'" & "NO" & _
                    "',N'" & "Datetime_Code_Create" & "',N'" & "DATETIME" & "'," & _
                    "'50','0','YES','YES','YES','NO','YES','0','0','0','0','NO','','','','','0','0','0','0','NO','NO','','YES')"
                htb.Add("ssqlDefaultView2", strInsertSQLView & strInsertSQLView2)

                strInsertSQLView2 = " Values(N'" & txtTABLE.Text.ToString & "_CARD_VW','" & _
                    ((gvField.Rows.Count + 3) * 10) & "',N'" & "USER_PROFILE_CODE_MODIFY" & _
                    "',N'" & "User Profile Code Modify" & "',N'" & "NO" & _
                    "',N'" & "User_Profile_Code_Modify" & "',N'" & "LOOKUP" & "'," & _
                    "'50','0','YES','YES','YES','NO','YES','0','0','0','0','NO','','','','','0','0','0','0','NO','NO','','YES')"
                htb.Add("ssqlDefaultView3", strInsertSQLView & strInsertSQLView2)

                strInsertSQLView2 = " Values(N'" & txtTABLE.Text.ToString & "_CARD_VW','" & _
                    ((gvField.Rows.Count + 4) * 10) & "',N'" & "DATETIME_MODIFY" & _
                    "',N'" & "Datetime Modify" & "',N'" & "NO" & _
                    "',N'" & "Datetime_Modify" & "',N'" & "DATETIME" & "'," & _
                    "'50','0','YES','YES','YES','NO','YES','0','0','0','0','NO','','','','','0','0','0','0','NO','NO','','YES')"
                htb.Add("ssqlDefaultView4", strInsertSQLView & strInsertSQLView2)
            End If

            Dim ssqlCreateTable As String = "Create Table [dbo].[" & txtTABLE.Text.ToString.Trim & "]("
            Dim ssqlOnPrimary As String = ""

            For i = 0 To gvField.Rows.Count - 1
                Select Case gvField.Rows(i).Cells(5).Text.ToString.ToUpper
                    Case "OPTION"
                        ssqlCreateTable = ssqlCreateTable & "[" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & "] [nvarchar](50),"
                    Case "DATE"
                        ssqlCreateTable = ssqlCreateTable & "[" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & "] [nvarchar](14),"
                    Case "DATETIME"
                        ssqlCreateTable = ssqlCreateTable & "[" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & "] [nvarchar](14),"
                    Case "TIME"
                        ssqlCreateTable = ssqlCreateTable & "[" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & "] [nvarchar](6),"
                    Case "LOOKUP"
                        ssqlCreateTable = ssqlCreateTable & "[" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & "] [nvarchar](50),"
                    Case "CHARACTER"
                        If gvField.Rows(i).Cells(2).Text.ToString.ToUpper = "REMARK" Or gvField.Rows(i).Cells(2).Text.ToString.ToUpper = "REMARKS" Then
                            ssqlCreateTable = ssqlCreateTable & "[" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & "] [nvarchar](255),"
                        Else
                            ssqlCreateTable = ssqlCreateTable & "[" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & "] [nvarchar](100),"
                        End If
                    Case "INTEGER"
                        ssqlCreateTable = ssqlCreateTable & "[" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & "] [int],"
                    Case "DECIMAL"
                        ssqlCreateTable = ssqlCreateTable & "[" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & "] [decimal](18, 6),"
                End Select

                If gvField.Rows(i).Cells(4).Text.ToString.ToUpper = "YES" Then
                    ssqlOnPrimary = ssqlOnPrimary & "[" & gvField.Rows(i).Cells(2).Text.ToString.ToUpper & "] ASC,"
                End If
            Next
            If AddDefaultField = True Then
                ssqlCreateTable = ssqlCreateTable & "[USER_PROFILE_CODE_CREATE] [nvarchar](50),"
                ssqlCreateTable = ssqlCreateTable & "[DATETIME_CREATE] [nvarchar](50),"
                ssqlCreateTable = ssqlCreateTable & "[USER_PROFILE_CODE_MODIFY] [nvarchar](50),"
                ssqlCreateTable = ssqlCreateTable & "[DATETIME_MODIFY] [nvarchar](50),"
            End If
            If ssqlOnPrimary <> "" Then
                ssqlOnPrimary = "CONSTRAINT [PK_" & txtTABLE.Text.ToString & "] PRIMARY KEY CLUSTERED (" & ssqlOnPrimary
                ssqlOnPrimary = Left(ssqlOnPrimary, Len(ssqlOnPrimary) - 1)
                ssqlOnPrimary = ssqlOnPrimary & ")WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]"
                ssqlCreateTable = ssqlCreateTable & ssqlOnPrimary
            Else
                ssqlCreateTable = Left(ssqlCreateTable, Len(ssqlCreateTable) - 1) & ") ON [PRIMARY]"
            End If
            ssqlCreateView = Left(ssqlCreateView, Len(ssqlCreateView) - 1)
            If IsEmployeeFieldExists = True Then
                If IsCompanyFieldExists = True Then
                    ssqlCreateView = ssqlCreateView & " From [dbo].[" & txtTABLE.Text.ToString.Trim & "] Left Outer Join dbo.[Employee_CodeName_Vw] On dbo.[" & txtTABLE.Text.ToString.Trim & "].Employee_Profile_ID=dbo.[Employee_CodeName_Vw].[Employee_Profile_ID]"
                Else
                    ssqlCreateView = ssqlCreateView & " From [dbo].[" & txtTABLE.Text.ToString.Trim & "] Left Outer Join dbo.[Employee_CodeName_Vw] On dbo.[" & txtTABLE.Text.ToString.Trim & "].Employee_Profile_ID=dbo.[Employee_CodeName_Vw].[Employee_Profile_ID]"
                End If
            Else
                ssqlCreateView = ssqlCreateView & " From [dbo].[" & txtTABLE.Text.ToString.Trim & "]"
            End If


            Dim myHTB As New Hashtable

            myHTB.Add("ssqlCreateTable", ssqlCreateTable)

            htb.Add("ssqlCreateView", ssqlCreateView)
            htb.Add("ssqlUpdate", "Update Table_Field Set Option_Editable='NO' Where Option_Primary_Key='YES' And Table_Profile_Code In ('" & txtTABLE.Text.ToString.Trim & "','" & txtTABLE.Text.ToString.Trim & "_CARD_VW')")
            htb.Add("ssqlADDPageProfile", "Insert Into ADD_Page_Profile Values(N'" & Session("Company").ToString & "',N'" & _
                    txtMODULE_PROFILE_CODE.Text.ToString.ToUpper.Trim & "',N'" & Session("EmpID").ToString & "',N'" & _
                    txtTABLE.Text.ToUpper.Trim & "','ACTIVE',dbo.fn_GetCurrentDateTime(GetDate()))")
            If mySQL.ExecuteSQLTransactionByHashtable(myHTB, "Create New Page", Session("Company").ToString, Session("EmpID").ToString) = -1 Then
                'Error handling
                Session("ErrorFound") = "true"
                lblMessage.Text = "Error occur while creating new page!"
            Else
                If mySQL.ExecuteSQLTransactionByHashtable(htb, "Create New Table And View", Session("Company").ToString, Session("EmpID").ToString) = -1 Then
                    'Error handling
                    mySQL.ExecuteSQL("IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Address_City]') AND type in (N'U')) DROP TABLE [" & txtTABLE.Text.ToString.Trim & "]", Session("Company").ToString, Session("EmpID").ToString)
                    Session("ErrorFound") = "true"
                    lblMessage.Text = "Error occur while creating table and view!"
                Else
                    GenerateLogiXMLReport()
                    GenerateASPX_VB(strFilePath)
                End If
            End If
            htb = Nothing
        Catch ex As Exception
            lblMessage.Visible = True
            lblMessage.Text = "Error Create SQL: " & ex.Message.ToString
        End Try
    End Sub
    Private Sub GenerateLogiXMLReport()
        Try
            Dim strReportPath As String = System.AppDomain.CurrentDomain.BaseDirectory & "Reports\_Definitions\_Reports\"
            'Create LogiXML Folder Directory For Company If Not Exists
            If Not File.Exists(strReportPath & Session("Company").ToString & ".fld") Then
                File.Create(strReportPath & Session("Company").ToString & ".fld")
            End If

            'Create LogiXML Folder For Government Report If Not Exists
            If Not File.Exists(strReportPath & Session("Company").ToString & ".Government.fld") Then
                File.Create(strReportPath & Session("Company").ToString & ".Government.fld")
            End If

            'Create LogiXML Folder For Standard Report If Not Exists
            If Not File.Exists(strReportPath & Session("Company").ToString & ".Standard.fld") Then
                File.Create(strReportPath & Session("Company").ToString & ".Standard.fld")
            End If

            'Create LogiXML Folder For Module If Not Exists
            If Not File.Exists(strReportPath & Session("Company").ToString & ".Standard." & txtMODULE_PROFILE_CODE.Text.ToString & ".fld") Then
                File.Create(strReportPath & Session("Company").ToString & ".Standard." & txtMODULE_PROFILE_CODE.Text.ToString.ToUpper & ".fld")
            End If

            'Copy Template Report, Rename It And Place To The Company\Standard\Module 
            File.Copy(strReportPath & "Default.Standard.Template.lgx", strReportPath & Session("Company").ToString & ".Standard." & txtMODULE_PROFILE_CODE.Text.ToString.ToUpper & "." & txtTABLE.Text.ToString.ToUpper & ".lgx")
        Catch ex As Exception
            lblMessage.Text = "Error Create Report: " & ex.Message.ToString
            lblMessage.Visible = True
        End Try

    End Sub
    Private Sub GenerateASPX_VB(ByVal strFilePath As String)
        Try
            If Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "Pages\" & txtMODULE_PROFILE_CODE.Text.ToString) Then
                'Do nothing
            Else
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "Pages\" & txtMODULE_PROFILE_CODE.Text.ToString)
            End If

            Dim WebFileWriter As StreamWriter, WebFileReader As StreamReader
            If Not File.Exists(strWebFileToCreate) Then
                WebFileWriter = File.CreateText(strWebFileToCreate)

                myAutoGenerate = New clsAutoGenerate
                myArray = New ArrayList
                myAutoGenerate.ReadFileContent(myArray, strFilePath & "WebHeader.txt")
                myArray.Item(myArray.Count - 1) = myArray.Item(myArray.Count - 1) & _
                        """" & txtTABLE.Text.ToString.ToUpper.Trim & ".aspx.vb"" " & _
                        "Inherits=" & """" & "PAGES_" & txtMODULE_PROFILE_CODE.Text.ToString.ToUpper.Trim & _
                        "_" & txtTABLE.Text.ToString.ToUpper.Trim & """" & " %>" & vbCrLf & _
                        "<!DOCTYPE html PUBLIC " & """" & "-//W3C//DTD XHTML 1.0 Transitional//EN" & """" & """" & " " & "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" & """" & ">" & vbCrLf & _
                        "<html xmlns=" & """" & "http://www.w3.org/1999/xhtml" & """" & " >" & vbCrLf & _
                        "<head id=" & """" & "Head1" & """" & " runat=" & """" & "server" & """" & ">" & vbCrLf & _
                        "<title>" & txtTABLE.Text.ToString.Trim & "</title>" & vbCrLf & _
                        "<link href=" & """" & "../../App_Themes/hcrmStyles1.css" & """" & " type=" & """" & "text/css" & """" & " rel=" & """" & "stylesheet" & """" & " />" & vbCrLf & _
                        "</head>" & vbCrLf & "<body id=" & """" & "body" & """" & " runat=" & """" & "server" & """" & "> " & vbCrLf & _
                        "<form id=" & """" & txtTABLE.Text.ToString.ToUpper.Trim & """" & " runat=" & """" & "server" & """" & ">" & vbCrLf & _
                        "<div>" & vbCrLf & "<asp:Panel ID=" & """" & "pnlEdit" & """" & " runat=" & """" & "server" & """" & ">" & vbCrLf
                WebFileWriter.Write(myArray(0))
                myArray = Nothing
                myAutoGenerate = Nothing

                For i = 0 To gvField.Rows.Count - 1
                    WebFileWriter.WriteLine("<!--// " & i + 1 & " //-->")
                    WebFileWriter.WriteLine("<asp:imagebutton id=" & """" & "imgKey" & gvField.Rows(i).Cells(2).Text.ToString & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "21px" & """" & " Height=" & """" & "21px" & """" & " ></asp:imagebutton>")
                    WebFileWriter.WriteLine("<asp:label id=" & """" & "lbl" & gvField.Rows(i).Cells(2).Text.ToString & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "140px" & """" & " ></asp:label>")
                    Select Case gvField.Rows(i).Cells(5).Text.ToString.ToUpper
                        Case "OPTION"
                            WebFileWriter.WriteLine("<asp:dropdownlist id=" & """" & "ddl" & gvField.Rows(i).Cells(2).Text.ToString & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "150px" & """" & " ></asp:dropdownlist>")
                        Case Else
                            WebFileWriter.WriteLine("<asp:textbox id=" & """" & "txt" & gvField.Rows(i).Cells(2).Text.ToString & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "150px" & """" & " ></asp:textbox>")
                    End Select
                    WebFileWriter.WriteLine("<asp:imagebutton id=" & """" & "imgbtn" & gvField.Rows(i).Cells(2).Text.ToString & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "21px" & """" & " Height=" & """" & "21px" & """" & " ></asp:imagebutton>")
                    WebFileWriter.WriteLine("")
                Next
                If AddDefaultField = True Then
                    WebFileWriter.WriteLine("<asp:imagebutton id=" & """" & "imgKey" & "USER_PROFILE_CODE_CREATE" & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "21px" & """" & " Height=" & """" & "21px" & """" & " ></asp:imagebutton>")
                    WebFileWriter.WriteLine("<asp:label id=" & """" & "lbl" & "USER_PROFILE_CODE_CREATE" & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "140px" & """" & " ></asp:label>")
                    WebFileWriter.WriteLine("<asp:textbox id=" & """" & "txt" & "USER_PROFILE_CODE_CREATE" & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "150px" & """" & " ></asp:textbox>")
                    WebFileWriter.WriteLine("<asp:imagebutton id=" & """" & "imgbtn" & "USER_PROFILE_CODE_CREATE" & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "21px" & """" & " Height=" & """" & "21px" & """" & " ></asp:imagebutton>")
                    WebFileWriter.WriteLine("")

                    WebFileWriter.WriteLine("<asp:imagebutton id=" & """" & "imgKey" & "DATETIME_CREATE" & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "21px" & """" & " Height=" & """" & "21px" & """" & " ></asp:imagebutton>")
                    WebFileWriter.WriteLine("<asp:label id=" & """" & "lbl" & "DATETIME_CREATE" & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "140px" & """" & " ></asp:label>")
                    WebFileWriter.WriteLine("<asp:textbox id=" & """" & "txt" & "DATETIME_CREATE" & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "150px" & """" & " ></asp:textbox>")
                    WebFileWriter.WriteLine("<asp:imagebutton id=" & """" & "imgbtn" & "DATETIME_CREATE" & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "21px" & """" & " Height=" & """" & "21px" & """" & " ></asp:imagebutton>")
                    WebFileWriter.WriteLine("")

                    WebFileWriter.WriteLine("<asp:imagebutton id=" & """" & "imgKey" & "USER_PROFILE_CODE_MODIFY" & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "21px" & """" & " Height=" & """" & "21px" & """" & " ></asp:imagebutton>")
                    WebFileWriter.WriteLine("<asp:label id=" & """" & "lbl" & "USER_PROFILE_CODE_MODIFY" & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "140px" & """" & " ></asp:label>")
                    WebFileWriter.WriteLine("<asp:textbox id=" & """" & "txt" & "USER_PROFILE_CODE_MODIFY" & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "150px" & """" & " ></asp:textbox>")
                    WebFileWriter.WriteLine("<asp:imagebutton id=" & """" & "imgbtn" & "USER_PROFILE_CODE_MODIFY" & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "21px" & """" & " Height=" & """" & "21px" & """" & " ></asp:imagebutton>")
                    WebFileWriter.WriteLine("")

                    WebFileWriter.WriteLine("<asp:imagebutton id=" & """" & "imgKey" & "DATETIME_MODIFY" & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "21px" & """" & " Height=" & """" & "21px" & """" & " ></asp:imagebutton>")
                    WebFileWriter.WriteLine("<asp:label id=" & """" & "lbl" & "DATETIME_MODIFY" & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "140px" & """" & " ></asp:label>")
                    WebFileWriter.WriteLine("<asp:textbox id=" & """" & "txt" & "DATETIME_MODIFY" & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "150px" & """" & " ></asp:textbox>")
                    WebFileWriter.WriteLine("<asp:imagebutton id=" & """" & "imgbtn" & "DATETIME_MODIFY" & """" & " runat=" & """" & "server" & """" & " Width=" & """" & "21px" & """" & " Height=" & """" & "21px" & """" & " ></asp:imagebutton>")
                    WebFileWriter.WriteLine("")
                End If
                WebFileReader = File.OpenText(strFilePath & "WebFooter.txt")
                WebFileWriter.Write(WebFileReader.ReadToEnd)
                WebFileReader.Close()
                WebFileReader.Dispose()
                WebFileWriter.Close()
                WebFileWriter.Dispose()
            End If
            WebFileWriter = Nothing
            WebFileReader = Nothing

            Dim VBFileWriter As StreamWriter, VBFileReader As StreamReader
            If Not File.Exists(strVBFileToCreate) Then
                VBFileWriter = File.CreateText(strVBFileToCreate)

                VBFileWriter.WriteLine("Imports System")
                VBFileWriter.WriteLine("Imports System.Data")
                VBFileWriter.WriteLine("Imports System.Data.SqlClient")
                VBFileWriter.WriteLine("")
                VBFileWriter.WriteLine("Partial Class " & "PAGES_" & txtMODULE_PROFILE_CODE.Text.ToString & "_" & txtTABLE.Text.ToString)

                VBFileReader = File.OpenText(strFilePath & "VBHeader.txt")
                VBFileWriter.Write(VBFileReader.ReadToEnd)
                VBFileReader.Close()
                VBFileReader.Dispose()

                VBFileWriter.WriteLine("")
                VBFileWriter.WriteLine(vbTab & vbTab & vbTab & "Session(" & """" & "Module" & """" & ")=" & """" & txtMODULE_PROFILE_CODE.Text.ToString.ToUpper.Trim & """")

                VBFileReader = File.OpenText(strFilePath & "VBFooter.txt")
                VBFileWriter.Write(VBFileReader.ReadToEnd)
                VBFileReader.Close()
                VBFileReader.Dispose()
                VBFileWriter.Close()
                VBFileWriter.Dispose()
            End If
            VBFileWriter = Nothing
            VBFileReader = Nothing

            lblMessage.Text = "Process done successfully."
        Catch ex As Exception
            lblMessage.Text = "Error Create ASPX: " & ex.Message.ToString
            lblMessage.Visible = True
        End Try
    End Sub
#End Region
    
End Class
