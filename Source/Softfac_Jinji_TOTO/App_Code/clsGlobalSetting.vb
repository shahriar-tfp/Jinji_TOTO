Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Diagnostics
Public Class clsGlobalSetting
    Inherits System.Web.UI.Page
    Private WithEvents mySQL As New clsSQL
    Private WithEvents myDS, myDS2, myDS3, myDS4, myDST As New DataSet
    Private WithEvents myDT, myDT1 As New DataTable
    Dim i, j, k As Integer, ssql, ssql1, ssql2, ssql3, ssql4, ssql5, ssql6, imgPath As String, RecFound As Boolean
    Dim OCP_Type As String = "''"
    Private WithEvents myIniFile As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory() & "Setup\Setup.ini")
    Dim _Err As String
    Enum DDLSelection
        SelectedText
        SelectedValue
    End Enum

    Enum ButtonAction
        INSERT
        SAVE
        EDIT
        UPDATE
        DELETE
        CANCEL
        MORE
    End Enum

    Enum ImageType
        _KEY
        _LOOKUP
        _DATE
        _DATETIME
        _TIME
        _CREATE
        _SEARCH
        _BLANK
    End Enum

    Enum AvailableColumn
        CodeName
        Code
        Name
    End Enum

    Enum SQLAction
        SELECT_Statement
        INSERT_Statement
        UPDATE_Statement
        DELETE_Statement
        DELETE_Statement2
        DELETE_Statement3
    End Enum

    Enum DataType
        _CHARACTER
        _DATE
        _DATETIME
        _TIME
        _DECIMAL
        _INTEGER
        _LOOKUP
        _OPTION
    End Enum

    Public Sub SetTextBoxPressEnterGoToButton(ByVal myTextBox As System.Web.UI.WebControls.TextBox, ByVal myButton As System.Web.UI.WebControls.Button)
        myTextBox.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + myButton.UniqueID + "').click();return false;}} else {return true}")
    End Sub

    Public Sub SetTextBoxPressEnterGoToImageButton(ByVal myTextBox As System.Web.UI.WebControls.TextBox, ByVal myImageButton As System.Web.UI.WebControls.ImageButton)
        myTextBox.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + myImageButton.UniqueID + "').click();return false;}} else {return true}")
    End Sub

    Public Sub PopUpTime_ImageButton(ByVal myImageButton As System.Web.UI.WebControls.ImageButton, ByVal myPageName As String, ByVal myTextBoxID As String)
        myImageButton.Attributes.Add("onclick", "javascript:PopUpTimePicker=window.open('../Global/TimePicker.aspx?formname=" & myPageName & "." & myTextBoxID & "','TimePicker','width=160,height=140,left=400,top=250');PopUpTimePicker.focus()")
    End Sub

    Public Sub PopUpCalendar_ImageButton(ByVal myImageButton As System.Web.UI.WebControls.ImageButton, ByVal myPageName As String, ByVal myTextBoxID As String, Optional ByVal strFilter As String = "", Optional ByVal strFilter2 As String = "")
        myImageButton.Attributes.Add("onclick", "javascript:PopUpCalendar=window.open('../Global/Calendar.aspx?formname=" & myPageName & "." & myTextBoxID & "&filter=" & strFilter & "&filter2=" & strFilter2 & "','Calendar','width=200,height=230,left=400,top=250');PopUpCalendar.focus()")
    End Sub

    Public Sub PopUpCalendar2_ImageButton(ByVal myImageButton As System.Web.UI.WebControls.ImageButton, ByVal myPageName As String, ByVal myTextBoxID As String, ByVal myDateType As String)
        myImageButton.Attributes.Add("onclick", "javascript:PopUpCalendar=window.open('../Global/CalendarDefault.aspx?formname=" & myPageName & "." & myTextBoxID & "&datetype=" & myDateType & "','Calendar','width=200,height=230,left=400,top=250');PopUpCalendar.focus()")
    End Sub

    Public Sub PopUpCalendar_Button(ByVal myButton As System.Web.UI.WebControls.Button, ByVal myPageName As String, ByVal myTextBoxID As String)
        myButton.Attributes.Add("onclick", "javascript:PopUpCalendar=window.open('../Global/Calendar.aspx?formname=" & myPageName & "." & myTextBoxID & "','Calendar','width=200,height=230,left=400,top=250');PopUpCalendar.focus()")
    End Sub

    Public Sub DateTimePicker_ImageButton(ByVal myImageButton As System.Web.UI.WebControls.ImageButton, ByVal myPageName As String, ByVal myTextBoxID As String)
        myImageButton.Attributes.Add("onclick", "javascript:DateTimePicker=window.open('../Global/DateTimePicker.aspx?formname=" & myPageName & "." & myTextBoxID & "','DateTimePicker','width=200,height=400,left=400,top=250');DateTimePicker.focus()")
    End Sub

    Public Sub DateTimePicker_Button(ByVal myButton As System.Web.UI.WebControls.ImageButton, ByVal myPageName As String, ByVal myTextBoxID As String)
        myButton.Attributes.Add("onclick", "javascript:DateTimePicker=window.open('../Global/DateTimePicker.aspx?formname=" & myPageName & "." & myTextBoxID & "','DateTimePicker','width=200,height=400,left=400,top=250');DateTimePicker.focus()")
    End Sub

    Public Sub ConfirmDeleteRecord_Button(ByVal myButton As System.Web.UI.WebControls.Button)
        myButton.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to delete these records?')")
    End Sub

    Public Sub ConfirmDeleteRecord_ImageButton(ByVal myImageButton As System.Web.UI.WebControls.ImageButton)
        myImageButton.Attributes.Add("onClick", "javascript:return confirm('Are you sure you want to delete these records?')")
    End Sub

    Public Sub GetLookupValue_ImageButton(ByVal myImageButton As System.Web.UI.WebControls.ImageButton, ByVal myPageName As String, ByVal myTextBoxID As String, ByVal SelectedColumn As String, ByVal ssql As String, Optional ByVal strFilter As String = "", Optional ByVal strFilter2 As String = "", Optional ByVal strFilter3 As String = "")
        'myImageButton.Attributes.Add("onclick", "javascript:Lookup=window.open('Lookup.aspx?formname=" & myPageName & "." & myTextBoxID & "','Lookup','toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=0,width=370,height=250,left=280,top=250');Lookup.focus()")
        Dim strQuery As String
        strQuery = "javascript:Lookup=window.open('../Global/Lookup.aspx?formname=" & myPageName & "." & myTextBoxID & "&selectedcolumn=" & SelectedColumn & "&filter=" & strFilter & "&filter2=" & strFilter2 & "&filter3=" & strFilter3 & "&query=" & ssql & "','Lookup','toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=0,width=390,height=305,left=280,top=235');Lookup.focus()"
        myImageButton.Attributes.Add("onclick", strQuery)
    End Sub

    Public Sub GetDataGridSetting(ByVal CompanyProfileCode As String, ByVal ModuleProfileCode As String, ByVal Code As String, ByVal PageCode As String, ByVal Type As String, ByVal myDG As System.Web.UI.WebControls.DataGrid)
        ssql = "Exec sp_sa_GetPageSetting '" & CompanyProfileCode & "','" & ModuleProfileCode & "','" & Code & "','" & PageCode & "','" & Type & "'"
        myDS = mySQL.ExecuteSQL(ssql)
        If myDS.Tables(0).Rows.Count > 0 Then
            With myDG
                .PageSize = myDS.Tables(0).Rows(0).Item(0)
            End With
        End If
    End Sub

    Public Sub GetGridViewSetting(ByVal strCompanyID As String, ByVal strModule As String, ByVal Code As String, ByVal TableName As String, ByVal Type As String, ByVal myGV As System.Web.UI.WebControls.GridView)
        'Exec sp_sa_GetPageSetting 'WDM','System_Manager','No_Of_Record','User_Category_Group','GetGridViewRecord'
        ssql = "Exec sp_sa_GetPageSetting '" & strCompanyID & "','" & strModule & "','" & Code & "','" & TableName & "','" & Type & "'"
        myDS = mySQL.ExecuteSQL(ssql)
        If myDS.Tables(0).Rows.Count > 0 Then
            With myGV
                .PageSize = myDS.Tables(0).Rows(0).Item(0)
            End With
        End If
    End Sub

    Public Function GetArrayList(ByVal myDS As DataSet, ByVal myTable As Integer, ByVal SelectedColumn As AvailableColumn) As ICollection
        Dim i As Long, myArrayLst As New ArrayList
        If myDS.Tables(myTable).Rows.Count > 0 Then
            Select Case SelectedColumn
                Case AvailableColumn.CodeName
                    For i = 0 To myDS.Tables(myTable).Rows.Count - 1
                        myArrayLst.Add(myDS.Tables(myTable).Rows(i).Item(0))
                    Next
                Case AvailableColumn.Code
                    For i = 0 To myDS.Tables(myTable).Rows.Count - 1
                        myArrayLst.Add(myDS.Tables(myTable).Rows(i).Item(1))
                    Next
                Case AvailableColumn.Name
                    For i = 0 To myDS.Tables(myTable).Rows.Count - 1
                        myArrayLst.Add(myDS.Tables(myTable).Rows(i).Item(2))
                    Next
            End Select
        End If
        Return myArrayLst
    End Function

    Public Function GetLabelDescription(ByVal TableName As String) As DataSet
        ssql = "Exec sp_sa_GetDescription '" & TableName & "'"
        myDS = mySQL.ExecuteSQL(ssql)
        Return myDS
    End Function

    Public Function GetLabelDescription_(ByVal TableName As String) As DataSet
        ssql = "Exec sp_sa_GetDescription_ '" & TableName & "'"
        myDS = mySQL.ExecuteSQL(ssql)
        Return myDS
    End Function

    Public Function GetPageFieldSetting(ByVal Company As String, ByVal TableName As String, ByVal UsrEmpCode As String) As DataSet
        ssql = "Exec sp_sa_GetPageFieldSetting '" & Company & "','" & TableName & "','" & UsrEmpCode & "'"
        myDS = mySQL.ExecuteSQL(ssql)
        Return myDS
    End Function

    Public Function GetGridViewWidth(ByVal TableName As String) As DataSet
        ssql = "Exec sp_sa_GetGridViewWidth '" & TableName & "'"
        myDS = mySQL.ExecuteSQL(ssql)
        Return myDS
    End Function

    Public Sub GetImageURL(ByVal myImage As System.Web.UI.WebControls.Image, ByVal ImagePicture As ImageType)
        Select Case ImagePicture
            Case ImageType._KEY
                myImage.ImageUrl = "../../Images/Default/png/key.png"
            Case ImageType._LOOKUP
                myImage.ImageUrl = "../../Images/Default/png/lookup.png"
            Case ImageType._DATE
                myImage.ImageUrl = "../../Images/Default/png/calendar.png"
            Case ImageType._DATETIME
                myImage.ImageUrl = "../../Images/Default/png/datetime.png"
            Case ImageType._TIME
                myImage.ImageUrl = "../../Images/Default/png/time.png"
        End Select
    End Sub

    Public Sub GetImageButtonURL(ByVal myImageButton As System.Web.UI.WebControls.ImageButton, ByVal ImagePicture As ImageType)
        Select Case ImagePicture
            Case ImageType._KEY
                myImageButton.ImageUrl = "../../Images/Default/icon/key.ico"
            Case ImageType._LOOKUP
                myImageButton.ImageUrl = "../../Images/Default/icon/lookup.ico"
            Case ImageType._DATE
                myImageButton.ImageUrl = "../../Images/Default/icon/calendar.ico"
            Case ImageType._DATETIME
                myImageButton.ImageUrl = "../../Images/Default/icon/datetime.ico"
            Case ImageType._TIME
                myImageButton.ImageUrl = "../../Images/Default/icon/time.ico"
        End Select
    End Sub

    Public Sub GetImgUrl(ByVal myImage As System.Web.UI.WebControls.Image, ByVal ImagePicture As ImageType, ByVal Company As String)

        imgPath = ""
        Select Case ImagePicture
            Case ImageType._KEY
                imgPath = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\" & Company & "\Png\key.png"
                If File.Exists(imgPath) = True Then
                    myImage.ImageUrl = "../../Images/Company/" & Company & "/Png/key.png"
                Else
                    myImage.ImageUrl = "../../Images/Company/DEFAULT/Png/key.png"
                End If
            Case ImageType._LOOKUP
                imgPath = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\" & Company & "\Png\lookup.png"
                If File.Exists(imgPath) = True Then
                    myImage.ImageUrl = "../../Images/Company/" & Company & "/Png/lookup.png"
                Else
                    myImage.ImageUrl = "../../Images/Company/DEFAULT/Png/lookup.png"
                End If
            Case ImageType._DATE
                imgPath = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\" & Company & "\Png\calendar.png"
                If File.Exists(imgPath) = True Then
                    myImage.ImageUrl = "../../Images/Company/" & Company & "/Png/calendar.png"
                Else
                    myImage.ImageUrl = "../../Images/Company/DEFAULT/Png/calendar.png"
                End If
            Case ImageType._DATETIME
                imgPath = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\" & Company & "\Png\datetime.png"
                If File.Exists(imgPath) = True Then
                    myImage.ImageUrl = "../../Images/Company/" & Company & "/Png/datetime.png"
                Else
                    myImage.ImageUrl = "../../Images/Company/DEFAULT/Png/datetime.png"
                End If
            Case ImageType._TIME
                imgPath = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\" & Company & "\Png\time.png"
                If File.Exists(imgPath) = True Then
                    myImage.ImageUrl = "../../Images/Company/" & Company & "/Png/time.png"
                Else
                    myImage.ImageUrl = "../../Images/Company/DEFAULT/Png/time.png"
                End If
            Case ImageType._BLANK
                imgPath = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\" & Company & "\Png\blank.png"
                If File.Exists(imgPath) = True Then
                    myImage.ImageUrl = "../../Images/Company/" & Company & "/Png/blank.png"
                Else
                    myImage.ImageUrl = "../../Images/Company/DEFAULT/Png/blank.png"
                End If
        End Select

        myImage.Width = 16
        myImage.Height = 16

    End Sub

    Public Sub GetImgBtnUrl(ByVal myImageButton As System.Web.UI.WebControls.ImageButton, ByVal ImagePicture As ImageType, ByVal Company As String)

        imgPath = ""
        Select Case ImagePicture
            Case ImageType._KEY
                imgPath = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\" & Company & "\Png\key.png"
                If File.Exists(imgPath) = True Then
                    myImageButton.ImageUrl = "../../Images/Company/" & Company & "/Png/key.png"
                Else
                    myImageButton.ImageUrl = "../../Images/Company/DEFAULT/Png/key.png"
                End If
            Case ImageType._LOOKUP
                imgPath = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\" & Company & "\Png\lookup.png"
                If File.Exists(imgPath) = True Then
                    myImageButton.ImageUrl = "../../Images/Company/" & Company & "/Png/lookup.png"
                Else
                    myImageButton.ImageUrl = "../../Images/Company/DEFAULT/Png/lookup.png"
                End If
            Case ImageType._DATE
                imgPath = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\" & Company & "\Png\calendar.png"
                If File.Exists(imgPath) = True Then
                    myImageButton.ImageUrl = "../../Images/Company/" & Company & "/Png/calendar.png"
                Else
                    myImageButton.ImageUrl = "../../Images/Company/DEFAULT/Png/calendar.png"
                End If
            Case ImageType._DATETIME
                imgPath = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\" & Company & "\Png\datetime.png"
                If File.Exists(imgPath) = True Then
                    myImageButton.ImageUrl = "../../Images/Company/" & Company & "/Png/datetime.png"
                Else
                    myImageButton.ImageUrl = "../../Images/Company/DEFAULT/Png/datetime.png"
                End If
            Case ImageType._TIME
                imgPath = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\" & Company & "\Png\time.png"
                If File.Exists(imgPath) = True Then
                    myImageButton.ImageUrl = "../../Images/Company/" & Company & "/Png/time.png"
                Else
                    myImageButton.ImageUrl = "../../Images/Company/DEFAULT/Png/time.png"
                End If
            Case ImageType._SEARCH
                imgPath = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\" & Company & "\Png\search.png"
                If File.Exists(imgPath) = True Then
                    myImageButton.ImageUrl = "../../Images/Company/" & Company & "/Png/search.png"
                Else
                    myImageButton.ImageUrl = "../../Images/Company/DEFAULT/Png/search.png"
                End If
        End Select

        myImageButton.Width = 16
        myImageButton.Height = 16

    End Sub

    Public Sub GetImgTypeUrl(ByVal myImage As System.Web.UI.WebControls.Image, ByVal Company As String, ByVal btnType As String, ByVal imgName As String)

        imgPath = ""
        imgPath = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\" & Company & "\" & btnType & "\" & imgName
        If File.Exists(imgPath) = True Then
            myImage.ImageUrl = "../../Images/Company/" & Company & "/" & btnType & "/" & imgName
        Else
            myImage.ImageUrl = "../../Images/Company/DEFAULT/" & btnType & "/" & imgName
        End If

    End Sub

    Public Sub GetBtnImgUrl(ByVal myImageButton As System.Web.UI.WebControls.ImageButton, ByVal Company As String, ByVal btnColour As String, ByVal imgBtnName As String)

        imgPath = ""
        imgPath = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\" & Company & "\" & btnColour & "\" & imgBtnName
        If File.Exists(imgPath) = True Then
            myImageButton.ImageUrl = "../../Images/Company/" & Company & "/" & btnColour & "/" & imgBtnName
        Else
            myImageButton.ImageUrl = "../../Images/Company/DEFAULT/" & btnColour & "/" & imgBtnName
        End If

    End Sub

    Public Function CheckTextNull(ByVal strText As String) As Boolean
        If Not Trim(strText) = "" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function CheckTextNullAndReplaceSingleQuote(ByVal myTextBox As TextBox) As Boolean
        If Not myTextBox.Text.ToString.Trim = "" Then
            myTextBox.Text = myTextBox.Text.ToString.Replace("'", "''")
            Return True
        Else
            Return False
        End If
    End Function

    Public Function CheckDropdownlistNull(ByVal myDropDownList As System.Web.UI.WebControls.DropDownList) As Boolean
        If Trim(myDropDownList.SelectedValue) = "" Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function CheckIsDateNull(ByVal strDate As String) As Boolean
        If Not IsDate(Format(strDate, "dd/mm/yyyy")) Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function CheckIsNumeric(ByVal intNumber As Integer) As Boolean
        If IsNumeric(intNumber) Then
            Return True
        Else
            Return False
        End If
    End Function

    'Public Function ConfirmBeforeExit()
    '    Dim strScript = "<script language=" & """" & "JavaScript" & """" & ">"
    '      window.onbeforeunload = confirmExit;
    '      function confirmExit()
    '      {
    '        return message to display in dialog box;
    '      }
    '    </script>


    '    <script language="JavaScript">
    '      window.onbeforeunload = confirmExit;
    '      function confirmExit()
    '      {
    '        return message to display in dialog box;
    '      }
    '    </script>

    'End Function

    Public Function GetOptionCode(ByVal tableProfileCode As String, ByVal tableFieldCode As String, ByVal name As String) As DataSet
        ssql = "select Code from [option] where table_profile_code = '" & tableProfileCode & "' And table_field_code = '" & tableFieldCode & "' And [Name] = '" & name & "'"
        myDS = mySQL.ExecuteSQL(ssql)
        Return myDS
    End Function

    Public Function GetOptionName(ByVal tableProfileCode As String, ByVal tableFieldCode As String, ByVal code As String) As DataSet
        myDS = mySQL.ExecuteSQL("select Name from [option] where table_profile_code = '" & tableProfileCode & "' And table_field_code = '" & tableFieldCode & "' And [Name] = '" & code & "'")
        Return (myDS.Tables(0).Rows(0).Item(0))
    End Function

    Public Function GetEmployeeCode(ByVal com As String, ByVal name As String) As DataSet
        myDS = mySQL.ExecuteSQL("Select Code From Employee_Profile Where [ID] In( Select Top 1 Employee_Profile_ID From Employee_Name Where [Name] = N'" & name & "' And Effective_Date <= dbo.fn_GetCurrentDate(getDate()) Order By Effective_Date Desc) And Company_Profile_Code = N'" & com & "'")
        Return myDS
    End Function
    Public Function GetEmployeeCodeByCodeName_ReturnString(ByVal strCompany As String, ByVal strEmpCodeName As String) As String
        myDS = mySQL.ExecuteSQL("Select Code From Employee_CodeName_Vw Where Company_Profile_Code=N'" & strCompany & "' And CodeName=N'" & strEmpCodeName & "'")
        Return myDS.Tables(0).Rows(0).Item(0).ToString
    End Function

    Public Function GetEmployeeName(ByVal com As String, ByVal code As String) As DataSet
        myDS = mySQL.ExecuteSQL("Select [Name] from Employee_Name Where Employee_Profile_ID In (Select Distinct [ID] From Employee_Profile Where Code =N'" & code & "' And Company_Profile_Code = N'" & com & "') And Effective_Date <= dbo.fn_GetCurrentDate(getDate())")
        Return myDS
    End Function

    Public Function GetEmployeeIDbyCode(ByVal com As String, ByVal code As String) As DataSet
        myDS = mySQL.ExecuteSQL("Select [ID] From Employee_Profile Where Code = N'" & code & "' And Company_Profile_Code = N'" & com & "'")
        Return myDS
    End Function

    Public Function GetEmployeeIDbyName(ByVal com As String, ByVal name As String) As DataSet
        myDS = mySQL.ExecuteSQL("Select [ID] From Employee_Profile Where [ID] In (Select Employee_Profile_ID from Employee_Name Where [Name] = N'" & name & "' And Effective_Date <= dbo.fn_GetCurrentDate(getDate())) And Company_Profile_Code = N'" & com & "'")
        Return myDS
    End Function

    Public Function GetOCPIDbyCode(ByVal com As String, ByVal code As String) As DataSet
        myDS = mySQL.ExecuteSQL("select [ID] from Organisation_Code_Profile_Vw where code = N'" & code & "' And Effective_Date <= dbo.fn_GetCurrentDate(getDate()) And Company_Profile_Code = N'" & com & "'")
        Return myDS
    End Function

    Public Function GetOCPIDbyCodeName(ByVal com As String, ByVal codename As String) As DataSet
        myDS = mySQL.ExecuteSQL("select [ID] from Organisation_Code_Profile_Vw where CodeName = '" & codename & "' And Effective_Date <= dbo.fn_GetCurrentDate(getDate()) And Company_Profile_Code = '" & com & "'")
        Return myDS
    End Function
    Public Function GetOCPIDbyCodeName_ReturnString(ByVal com As String, ByVal codename As String) As String
        myDS = mySQL.ExecuteSQL("select [ID] from Organisation_Code_Profile_Vw where CodeName = '" & codename & "' And Effective_Date <= dbo.fn_GetCurrentDate(getDate()) And Company_Profile_Code = '" & com & "'")
        Return myDS.Tables(0).Rows(0).Item(0).ToString
    End Function

    Public Function GetCodeNamebyOCPID(ByVal com As String, ByVal ocpid As String) As DataSet
        myDS = mySQL.ExecuteSQL("select CodeName from Organisation_Code_Profile_Vw where [ID] = '" & ocpid & "' And Effective_Date <= dbo.fn_GetCurrentDate(getDate()) And Company_Profile_Code = '" & com & "'")
        Return myDS
    End Function

    Public Sub GetTreeViewSetting(ByVal myTreeNode As System.Web.UI.WebControls.TreeNode, ByVal myNodeType As String, ByVal Expandable As String, ByVal myGoToLink As String, ByVal myImageName As String, ByVal myDisplayName As String, ByVal ModuleName As String, ByVal CompanyName As String, Optional ByVal DefineSelectAction As Boolean = True)
        Dim strDefaultPath As String = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\Default\"
        Dim strCustomPath As String = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\" & CompanyName & "\"
        Dim strDefaultImagesPath As String = "Images/Company/Default/"
        Dim strCustomImagesPath As String = "Images/Company/" & CompanyName & "/"

        Select Case Right(myImageName, 3)
            Case "bmp", "BMP"
                strDefaultPath += "bmp\"
                strCustomPath += "bmp\"
                strDefaultImagesPath += "bmp/"
                strCustomImagesPath += "bmp/"
            Case "gif", "GIF"
                strDefaultPath += "gif\"
                strCustomPath += "gif\"
                strDefaultImagesPath += "gif/"
                strCustomImagesPath += "gif/"
            Case "ico", "ICO"
                strDefaultPath += "icon\"
                strCustomPath += "icon\"
                strDefaultImagesPath += "icon/"
                strCustomImagesPath += "icon/"
            Case "jpg", "jpg"
                strDefaultPath += "jpg\"
                strCustomPath += "jpg\"
                strDefaultImagesPath += "jpg/"
                strCustomImagesPath += "jpg/"
            Case "png", "PNG"
                strDefaultPath += "png\"
                strCustomPath += "png\"
                strDefaultImagesPath += "png/"
                strCustomImagesPath += "png/"
        End Select

        If File.Exists(strCustomPath & myImageName) Then
            myTreeNode.ImageUrl = strCustomImagesPath & myImageName
        ElseIf File.Exists(strDefaultPath & myImageName) Then
            myTreeNode.ImageUrl = strDefaultImagesPath & myImageName
            'Else
            '    myTreeNode.ImageUrl = "Images/Company/Default/Png/empty_2525.png"
        End If

        If Trim(myGoToLink) <> "0" Then
            myTreeNode.Target = "RightFrame"
            If DefineSelectAction = False Then
                myTreeNode.SelectAction = TreeNodeSelectAction.Select
            Else
                myTreeNode.SelectAction = TreeNodeSelectAction.Expand
            End If

            Select Case ModuleName.ToUpper
                Case "SOFTFAC"
                    If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory() & myGoToLink & ".aspx") Then
                        myTreeNode.NavigateUrl = myGoToLink & ".aspx"
                    End If
                Case Else
                    If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory() & "Pages\" & ModuleName & "\" & myGoToLink & ".aspx") Then
                        myTreeNode.NavigateUrl = "Pages/" & ModuleName & "/" & myGoToLink & ".aspx"
                    End If
            End Select

            'Select Case ModuleName.ToUpper
            '    Case "SOFTFAC"
            '        If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory() & myGoToLink & ".aspx") Then
            '            myTreeNode.NavigateUrl = myGoToLink & ".aspx"
            '        End If
            '    Case "SYSTEM_MANAGER"
            '        If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory() & "Pages\System\" & myGoToLink & ".aspx") Then
            '            myTreeNode.NavigateUrl = "Pages/System/" & myGoToLink & ".aspx"
            '        End If
            '    Case "INFORMATION_SYSTEM"
            '        If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory() & "Pages\InformationSystem\" & myGoToLink & ".aspx") Then
            '            myTreeNode.NavigateUrl = "Pages/InformationSystem/" & myGoToLink & ".aspx"
            '        End If
            '    Case "LEAVE"
            '        If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory() & "Pages\Leave\" & myGoToLink & ".aspx") Then
            '            myTreeNode.NavigateUrl = "Pages/Leave/" & myGoToLink & ".aspx"
            '        End If
            '    Case "PAYROLL"
            '        If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory() & "Pages\Payroll\" & myGoToLink & ".aspx") Then
            '            myTreeNode.NavigateUrl = "Pages/Payroll/" & myGoToLink & ".aspx"
            '        End If
            '    Case "ORGANISATION"
            '        If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory() & "Pages\Organisation\" & myGoToLink & ".aspx") Then
            '            myTreeNode.NavigateUrl = "Pages/Organisation/" & myGoToLink & ".aspx"
            '        End If
            '    Case "ROSTER"
            '        If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory() & "Pages\Roster\" & myGoToLink & ".aspx") Then
            '            myTreeNode.NavigateUrl = "Pages/Roster/" & myGoToLink & ".aspx"
            '        End If
            '    Case "WEB_ADMIN"
            '        If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory() & "Pages\CustomizeWeb\" & myGoToLink & ".aspx") Then
            '            myTreeNode.NavigateUrl = "Pages/CustomizeWeb/" & myGoToLink & ".aspx"
            '        End If
            '    Case "APPAREL"
            '        If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory() & "Pages\Apparel\" & myGoToLink & ".aspx") Then
            '            myTreeNode.NavigateUrl = "Pages/Apparel/" & myGoToLink & ".aspx"
            '        End If
            'End Select
        Else
            myTreeNode.SelectAction = TreeNodeSelectAction.Expand
        End If

        If Expandable = "YES" Then
            myTreeNode.Expand()
        Else
            myTreeNode.Collapse()
        End If
    End Sub

    Public Sub ReturnConfirm_Update(ByVal myButton As System.Web.UI.WebControls.Button)
        myButton.Attributes.Add("onclick", "return confirm('Are you sure you want to update?')")
    End Sub

    Public Function GetControlEnableTrueFalse(ByVal FormID As String) As DataSet
        myDS = mySQL.ExecuteSQL("Select * From Table_Field Where Table_Profile_Code='" & FormID & "' Order By Sequence_No")
        Return myDS
    End Function

    Function GetSQLParameter(ByVal FormID As String, ByVal SQLParameter As clsGlobalSetting.SQLAction, ByVal myGridView As System.Web.UI.WebControls.GridView, ByVal RowIndex As Integer, ByVal strCompanyID As String, ByVal strModule As String) As String
        Dim myDS As New DataSet
        Select Case SQLParameter
            '//////////////////////////////////////////////////////////////////
            Case clsGlobalSetting.SQLAction.SELECT_Statement
                myDS = mySQL.ExecuteSQL("Select Code From Table_Field Where Table_Profile_Code='" & FormID & "' And Option_View='YES' Order By Table_Profile_Code,Sequence_No")
                ssql = "Select "
                If myDS.Tables(0).Rows.Count > 0 Then
                    For i = 0 To myDS.Tables(0).Rows.Count - 1
                        ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                    Next
                    myDS = Nothing
                    ssql = Left(ssql, Len(ssql) - 1) & " From " & FormID
                End If
                If ssql = "Select " Then ssql = Nothing
                '//////////////////////////////////////////////////////////////////

                '//////////////////////////////////////////////////////////////////
            Case clsGlobalSetting.SQLAction.INSERT_Statement
                myDS = mySQL.ExecuteSQL("Select Code,Sequence_No From Table_Field Where Table_Profile_Code='" & FormID & "' And Option_View='YES' Order By Table_Profile_Code,Sequence_No")
                ssql = "Insert Into " & FormID & "("
                ssql2 = ") Values("
                If myDS.Tables(0).Rows.Count > 0 Then
                    For i = 0 To myDS.Tables(0).Rows.Count - 1
                        ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                        ssql2 = ssql2 & "N'" & myGridView.Rows(i).Cells(1 + CInt(myDS.Tables(0).Rows(i).Item(1))).Text & "',"
                    Next
                    ssql = Left(ssql, Len(ssql) - 1) & Left(ssql2, Len(ssql2) - 1) & ")"
                    myDS = Nothing
                End If
                If ssql = "Insert Into " & FormID & "(" Then ssql = Nothing
                '//////////////////////////////////////////////////////////////////

                '//////////////////////////////////////////////////////////////////
            Case clsGlobalSetting.SQLAction.UPDATE_Statement
                myDS = mySQL.ExecuteSQL("Select Code,Sequence_No,Option_Data_Type From Table_Field Where Table_Profile_Code='" & FormID & "' And Option_Primary_Key='NO' And Option_View='YES' Order By Table_Profile_Code,Sequence_No")
                ssql = "Update " & FormID & " Set "
                If myDS.Tables(0).Rows.Count > 0 Then
                    For i = 0 To myDS.Tables(0).Rows.Count - 1
                        Dim myTextBox As TextBox = myGridView.Rows(RowIndex).Cells(1 + CInt(myDS.Tables(0).Rows(i).Item(1))).Controls(0)
                        Select Case myDS.Tables(0).Rows(i).Item(2).ToString
                            Case "DATE", "DATETIME"
                                ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=N'" & ConvertDateToDecimal(myTextBox.Text, strCompanyID, strModule) & "',"
                            Case Else
                                ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=N'" & myTextBox.Text & "',"
                        End Select
                    Next
                    ssql = Left(ssql, Len(ssql) - 1) & " Where "
                    myDS = Nothing

                    myDS2 = mySQL.ExecuteSQL("Select Code,Sequence_No From Table_Field Where Table_Profile_Code='" & FormID & "' And Option_Primary_Key='YES' And Option_View='YES' Order By Table_Profile_Code,Sequence_No")
                    If myDS2.Tables(0).Rows.Count > 0 Then
                        For i = 0 To myDS2.Tables(0).Rows.Count - 1
                            Dim myTextBox As TextBox = myGridView.Rows(RowIndex).Cells(1 + CInt(myDS2.Tables(0).Rows(i).Item(1))).Controls(0)
                            ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "='" & myTextBox.Text & "' And "
                        Next
                    End If
                    ssql = Left(ssql, Len(ssql) - 5)
                    myDS2 = Nothing
                End If
                '//////////////////////////////////////////////////////////////////

                '//////////////////////////////////////////////////////////////////
            Case clsGlobalSetting.SQLAction.DELETE_Statement
                myDS = New DataSet
                myDS = mySQL.ExecuteSQL("Select Code,Name,Option_Data_Type,Option_Default_Value From Table_Field Where Table_Profile_Code='" & FormID & "' And Option_Primary_Key='YES' Order By Table_Profile_Code,Sequence_No")
                ssql = "Delete From " & FormID & " Where "
                If myDS.Tables(0).Rows.Count > 0 Then
                    For i = 0 To myDS.Tables(0).Rows.Count - 1
                        For j = 1 To myGridView.Rows(RowIndex).Cells.Count - 1
                            If myDS.Tables(0).Rows(i).Item(1).ToString = myGridView.HeaderRow.Cells(j).Text.ToString Then
                                Select Case myDS.Tables(0).Rows(i).Item(2).ToString
                                    Case "DATE"
                                        ssql = ssql & myDS.Tables(0).Rows(i).Item(0).ToString & "='" & ConvertDateToDecimal(Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString), strCompanyID, strModule) & "' And "
                                    Case "DATETIME"
                                        ssql = ssql & myDS.Tables(0).Rows(i).Item(0).ToString & "='" & UnDisplayDateTime(Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString), strCompanyID, strModule) & "' And "
                                    Case "TIME"
                                        ssql = ssql & myDS.Tables(0).Rows(i).Item(0).ToString & "='" & UnDisplayTime(Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString), strCompanyID, strModule) & "' And "
                                    Case "OPTION"
                                        If FormID.ToUpper = "SALARY_ENTITLEMENT" Or FormID.ToUpper = "OT_RATE" Then
                                            If myDS.Tables(0).Rows(i).Item(0).ToString = "OPTION_OCP_TYPE" Or myDS.Tables(0).Rows(i).Item(0).ToString = "OPTION_OCP_ID_TYPE" Then
                                                OCP_Type = "DBO.FN_GETOPTIONCODE(N'" & FormID & "',N'" & myDS.Tables(0).Rows(i).Item(0).ToString & "',N'" & Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString) & "')"
                                            End If
                                        End If
                                        If (FormID.ToUpper = "MASTER_SKILL_SETUP" Or FormID.ToUpper = "MASTERSKILL_HOD") And (myDS.Tables(0).Rows(i).Item(0).ToString = "OCP_ID_DEPARTMENT" Or myDS.Tables(0).Rows(i).Item(0).ToString = "OCP_ID_SECTION") Then
                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0).ToString & "=isnull(DBO.FN_GETCOMOCPIDBYCODENAME(N'" & Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString) & "',N'" & myDS.Tables(0).Rows(i).Item(0).ToString.Replace("OCP_ID_", "") & "',N'" & strCompanyID & "'),'') And "
                                        ElseIf FormID.ToUpper = "EMPLOYEE_OT_ADJUSTMENT" And myDS.Tables(0).Rows(i).Item(0).ToString.ToUpper = "YEAR" Then
                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0).ToString & "='" & Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString) & "' And "
                                        Else

                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0).ToString & "=DBO.FN_GETOPTIONCODE(N'" & FormID & "',N'" & myDS.Tables(0).Rows(i).Item(0).ToString & "',N'" & Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString) & "') And "
                                        End If
                                    Case Else
                                        If FormID.ToUpper = "EMPLOYEE_PROFILE" And myDS.Tables(0).Rows(i).Item(0).ToString = "CODE" Then
                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=dbo.fn_ReturnEmpCodeByCodeName(N'" & strCompanyID & "',N'" & Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString.Replace("&amp;", "&").Replace("'", "''")) & "') And "
                                            Exit Select
                                        End If
                                        If myDS.Tables(0).Rows(i).Item(0).ToString.ToUpper = "EMPLOYEE_PROFILE_ID" Then
                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=dbo.fn_ReturnEmpIDByCodeName(N'" & strCompanyID & "',N'" & Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString.Replace("&amp;", "&").Replace("'", "''")) & "') And "
                                        Else
                                            myDS2 = New DataSet
                                            myDS2 = mySQL.ExecuteSQL("Select Function_Name,Default_Value,Default_Value2 From User_Define_Function Where Company_Profile_Code='" & strCompanyID & "' And Module_Profile_Code='" & strModule & "' And Table_Profile_Code='" & FormID & "' And Table_Field_Code='" & myDS.Tables(0).Rows(i).Item(0) & "' And Query_Action='DELETE'")
                                            If myDS2.Tables(0).Rows.Count > 0 Then
                                                If myDS2.Tables(0).Rows(0).Item(2).ToString = "COMPANY" Then
                                                    If myDS2.Tables(0).Rows(0).Item(1).ToString = "" Then
                                                        ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=" & myDS2.Tables(0).Rows(0).Item(0).ToString & "('" & Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString.Replace("&amp;", "&").Replace("'", "''")) & "','" & strCompanyID & "') And "
                                                    Else
                                                        If FormID.ToUpper = "SALARY_ENTITLEMENT" Or FormID.ToUpper = "OT_RATE" Then
                                                            If myDS.Tables(0).Rows(i).Item(0).ToString = "OCP_ID" Then
                                                                ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=" & myDS2.Tables(0).Rows(0).Item(0).ToString & "('" & Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString.Replace("&amp;", "&").Replace("'", "''")) & "'," & OCP_Type & ",'" & strCompanyID & "') And "
                                                            Else
                                                                ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=" & myDS2.Tables(0).Rows(0).Item(0).ToString & "('" & Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString.Replace("&amp;", "&").Replace("'", "''")) & "','" & myDS2.Tables(0).Rows(0).Item(1).ToString & "','" & strCompanyID & "') And "
                                                            End If
                                                        Else
                                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=" & myDS2.Tables(0).Rows(0).Item(0).ToString & "('" & Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString.Replace("&amp;", "&").Replace("'", "''")) & "','" & myDS2.Tables(0).Rows(0).Item(1).ToString & "','" & strCompanyID & "') And "
                                                        End If
                                                    End If
                                                Else
                                                    If myDS2.Tables(0).Rows(0).Item(1).ToString = "" Then
                                                        ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=" & myDS2.Tables(0).Rows(0).Item(0).ToString & "('" & Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString.Replace("&amp;", "&").Replace("'", "''")) & "') And "
                                                    Else
                                                        If FormID.ToUpper = "SALARY_ENTITLEMENT" Or FormID.ToUpper = "OT_RATE" Then
                                                            If myDS.Tables(0).Rows(i).Item(0).ToString = "OCP_ID" Then
                                                                ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=" & myDS2.Tables(0).Rows(0).Item(0).ToString & "('" & Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString.Replace("&amp;", "&").Replace("'", "''")) & "'," & OCP_Type & ") And "
                                                            Else
                                                                ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=" & myDS2.Tables(0).Rows(0).Item(0).ToString & "('" & Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString.Replace("&amp;", "&").Replace("'", "''")) & "','" & myDS2.Tables(0).Rows(0).Item(1).ToString & "') And "
                                                            End If
                                                        Else
                                                            ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "=" & myDS2.Tables(0).Rows(0).Item(0).ToString & "('" & Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString.Replace("&amp;", "&").Replace("'", "''")) & "','" & myDS2.Tables(0).Rows(0).Item(1).ToString & "') And "
                                                        End If

                                                    End If
                                                End If
                                            Else
                                                ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & "='" & Server.HtmlDecode(myGridView.Rows(RowIndex).Cells(j).Text.ToString.ToString.Replace("&amp;", "").Replace("'", "''")) & "' And "
                                            End If
                                            myDS2 = Nothing
                                        End If
                                End Select
                            End If
                        Next
                    Next
                    ssql = Left(ssql, Len(ssql) - 5)
                    ssql = ssql.Replace("&amp;", "&")
                    ssql = ssql.Replace("&nbsp;", "&")
                End If
                myDS = Nothing
                If ssql = "Delete From " & FormID & " Where " Then ssql = Nothing
                '//////////////////////////////////////////////////////////////////
        End Select
        ssql = Replace(ssql, "&amp;", "&")
        ssql = Replace(ssql, "&nbsp;", "")
        Return ssql
    End Function

    Public Function GetEmployeeIDbyCodeName(ByVal com As String, ByVal codeName As String) As DataSet
        ssql = "Select Employee_Profile_ID From Employee_CodeName_Vw Where (CodeName = N'" & codeName & "' Or Code = N'" & codeName & "') And Company_Profile_Code = '" & com & "'"
        myDS = mySQL.ExecuteSQL(ssql)
        Return myDS
    End Function
    Public Function GetEmployeeIDbyCodeName_ReturnString(ByVal com As String, ByVal codeName As String) As String
        ssql = "Select Employee_Profile_ID From Employee_CodeName_Vw Where (CodeName = N'" & codeName & "' Or Code = N'" & codeName & "') And Company_Profile_Code = '" & com & "'"
        myDS = mySQL.ExecuteSQL(ssql)
        Return myDS.Tables(0).Rows(0).Item(0).ToString
    End Function
    Public Function GetCompanyCodebyCodeName_ReturnString(ByVal codeName As String) As String
        ssql = "Select dbo.fn_ReturnCompanyProfileCode('" & codeName & "')"
        myDS = mySQL.ExecuteSQL(ssql)
        Return myDS.Tables(0).Rows(0).Item(0).ToString
    End Function
    Public Function GetEmployeeIDbyCode_ReturnString(ByVal com As String, ByVal codeName As String) As String
        Try
            Dim strOutput As String = ""
            ssql = "Select Employee_Profile_ID From Employee_CodeName_Vw Where (Code = N'" & codeName & "' Or Code = N'" & codeName & "') And Company_Profile_Code = '" & com & "'"
            myDS = mySQL.ExecuteSQL(ssql)
            If Not myDS Is Nothing Then
                If myDS.Tables(0).Rows.Count > 0 Then
                    strOutput = myDS.Tables(0).Rows(0).Item(0).ToString
                End If
            End If
            Return strOutput
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Sub ResetUppercase(ByVal myTextBox As System.Web.UI.WebControls.TextBox)
        myTextBox.Attributes.Remove("onKeyUp")
    End Sub


    Function GetSQLParameter2(ByVal FormID As String, ByVal SQLParameter As clsGlobalSetting.SQLAction, ByVal myGridView As System.Web.UI.WebControls.GridView, ByVal RowIndex As Integer, ByVal ComName As String, ByVal ComMod As String) As String
        Select Case SQLParameter
            '//////////////////////////////////////////////////////////////////
            Case clsGlobalSetting.SQLAction.SELECT_Statement
                myDS = mySQL.ExecuteSQL("Select Code From Table_Field Where Table_Profile_Code='" & FormID & "' And Option_View='YES' Order By Table_Profile_Code,Sequence_No")
                ssql = "Select "
                If myDS.Tables(0).Rows.Count > 0 Then
                    For i = 0 To myDS.Tables(0).Rows.Count - 1
                        ssql = ssql & myDS.Tables(0).Rows(i).Item(0) & ","
                    Next
                    myDS = Nothing
                    ssql = Left(ssql, Len(ssql) - 1) & " From " & FormID
                End If
                If ssql = "Select " Then ssql = Nothing
                '//////////////////////////////////////////////////////////////////

                '//////////////////////////////////////////////////////////////////
            Case clsGlobalSetting.SQLAction.INSERT_Statement
                myDS3 = mySQL.ExecuteSQL("Select Code,Sequence_No From Table_Field Where Table_Profile_Code='" & FormID & "' And Option_View='YES' Order By Table_Profile_Code,Sequence_No")
                ssql = "Insert Into [" & FormID & "] ("
                ssql2 = ") Values("
                If myDS3.Tables(0).Rows.Count > 0 Then
                    For i = 0 To myDS3.Tables(0).Rows.Count - 1
                        ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & ","
                        ssql2 = ssql2 & "'" & myGridView.Rows(i).Cells(1 + CInt(myDS3.Tables(0).Rows(i).Item(1))).Text & "',"
                    Next
                    ssql = Left(ssql, Len(ssql) - 1) & Left(ssql2, Len(ssql2) - 1) & ")"
                    myDS3 = Nothing
                End If
                If ssql = "Insert Into " & FormID & "(" Then ssql = Nothing
                '//////////////////////////////////////////////////////////////////

                '//////////////////////////////////////////////////////////////////
            Case clsGlobalSetting.SQLAction.UPDATE_Statement
                myDS3 = mySQL.ExecuteSQL("Select Code,Sequence_No,Option_Data_Type From Table_Field Where Table_Profile_Code='" & FormID & "' And Option_Primary_Key='NO' And Option_View_Card ='YES' Order By Table_Profile_Code,Sequence_No")
                ssql = "Update [" & FormID & "] Set "
                If myDS3.Tables(0).Rows.Count > 0 Then
                    For i = 0 To myDS3.Tables(0).Rows.Count - 1
                        Dim myTextBox As TextBox = myGridView.Rows(RowIndex).Cells(1 + CInt(myDS3.Tables(0).Rows(i).Item(1))).Controls(0)
                        If myDS3.Tables(0).Rows(i).Item(2) = "DATETIME" Then
                            'data format convertion
                            Dim tempDate As String = ConvertDateToDecimal(myTextBox.Text, ComName, ComMod)
                            ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "='" & tempDate & "',"
                        ElseIf myDS3.Tables(0).Rows(i).Item(2) = "OPTION" Then
                            ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "='" & Replace(Replace(Trim(UCase(myTextBox.Text)), "amp;", ""), " ", "_") & "',"
                        Else
                            ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "='" & Replace(Trim(myTextBox.Text), "amp;", "") & "',"
                        End If
                    Next
                    ssql = Left(ssql, Len(ssql) - 1) & " Where "
                    myDS3 = Nothing

                    myDS2 = mySQL.ExecuteSQL("Select Code,Sequence_No,Option_Data_Type From Table_Field Where Table_Profile_Code='" & FormID & "' And Option_Primary_Key='YES' And Option_View='YES' Order By Table_Profile_Code,Sequence_No")
                    If myDS2.Tables(0).Rows.Count > 0 Then
                        For i = 0 To myDS2.Tables(0).Rows.Count - 1
                            Dim myTextBox As TextBox = myGridView.Rows(RowIndex).Cells(1 + CInt(myDS2.Tables(0).Rows(i).Item(1))).Controls(0)
                            If myDS2.Tables(0).Rows(i).Item(2) = "DATETIME" Then
                                'data format convertion
                                Dim tempDate As String = Replace(myTextBox.Text, "amp;", "")
                                tempDate = ConvertDateToDecimal(tempDate, ComName, ComMod)
                                ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "='" & tempDate & "' And "
                            ElseIf myDS2.Tables(0).Rows(i).Item(2) = "OPTION" Then
                                ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "='" & Replace(Replace(Trim(UCase(myTextBox.Text)), "amp;", ""), " ", "_") & "' And "
                            Else
                                ssql = ssql & myDS2.Tables(0).Rows(i).Item(0) & "='" & Replace(Trim(myTextBox.Text), "amp;", "") & "' And "
                            End If
                        Next
                    End If
                    ssql = Left(ssql, Len(ssql) - 5)
                    myDS2 = Nothing
                End If
                '//////////////////////////////////////////////////////////////////

                '//////////////////////////////////////////////////////////////////
            Case clsGlobalSetting.SQLAction.DELETE_Statement
                myDS3 = mySQL.ExecuteSQL("Select Code,Sequence_No,Option_Data_Type From Table_Field Where Table_Profile_Code='" & FormID & "' And Option_Primary_Key='YES' And Option_View='YES' Order By Table_Profile_Code,Sequence_No")
                ssql = "Delete From [" & FormID & "] Where "
                If myDS3.Tables(0).Rows.Count > 0 Then
                    For i = 0 To myDS3.Tables(0).Rows.Count - 1
                        If myDS3.Tables(0).Rows(i).Item(2) = "DATETIME" Then
                            'data format convertion
                            Dim tempDate As String = Replace(myGridView.Rows(RowIndex).Cells(1 + CInt(myDS3.Tables(0).Rows(i).Item(1))).Text, "amp;", "")
                            tempDate = ConvertDateToDecimal(tempDate, ComName, ComMod)
                            ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "='" & tempDate & "' And "
                        Else
                            ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "='" & Replace(Replace(myGridView.Rows(RowIndex).Cells(1 + CInt(myDS3.Tables(0).Rows(i).Item(1))).Text.Replace("'", "''"), "amp;", ""), "&nbsp;", "") & "' And "
                        End If
                    Next
                    ssql = Left(ssql, Len(ssql) - 5)
                    myDS3 = Nothing
                End If
                If ssql = "Delete From " & FormID & " Where " Then ssql = Nothing

            Case clsGlobalSetting.SQLAction.DELETE_Statement2
                myDS3 = mySQL.ExecuteSQL("Select Code,Sequence_No,Option_Data_Type From Table_Field Where Table_Profile_Code='" & FormID & "' And Option_Primary_Key='YES' Order By Table_Profile_Code,Sequence_No")
                Dim FormID2 As String = FormID
                If UCase(FormID) = "OCP_CODE_VW" Then
                    FormID2 = "OCP_CODE"
                ElseIf UCase(FormID) = "OCP_NAME_VW" Then
                    FormID2 = "OCP_NAME"
                ElseIf UCase(FormID) = "OCP_PIC_VW" Then
                    FormID2 = "OCP_PIC"
                End If
                ssql = "Delete From [" & FormID2 & "] Where "
                If myDS3.Tables(0).Rows.Count > 0 Then
                    For i = 0 To myDS3.Tables(0).Rows.Count - 1
                        If myDS3.Tables(0).Rows(i).Item(2) = "DATE" Then
                            'data format convertion
                            Dim tempDate As String = Replace(myGridView.Rows(RowIndex).Cells(CInt(myDS3.Tables(0).Rows(i).Item(1))).Text, "amp;", "")
                            tempDate = ConvertDateToDecimal(tempDate, ComName, ComMod)
                            ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "='" & tempDate & "' And "
                        Else
                            ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "='" & Replace(Replace(myGridView.Rows(RowIndex).Cells(CInt(myDS3.Tables(0).Rows(i).Item(1))).Text.Replace("'", "''"), "amp;", ""), "&nbsp;", "") & "' And "
                        End If
                    Next
                    ssql = Left(ssql, Len(ssql) - 5)
                    myDS3 = Nothing
                End If
                If ssql = "Delete From " & FormID2 & " Where " Then ssql = Nothing

            Case clsGlobalSetting.SQLAction.DELETE_Statement3
                myDS3 = mySQL.ExecuteSQL("Select Code,Sequence_No,Option_Data_Type From Table_Field Where Table_Profile_Code='" & FormID & "' And Option_Primary_Key='YES' Order By Table_Profile_Code,Sequence_No")
                ssql = "Delete From [" & FormID & "] Where "
                If myDS3.Tables(0).Rows.Count > 0 Then
                    For i = 0 To myDS3.Tables(0).Rows.Count - 1
                        If myDS3.Tables(0).Rows(i).Item(2) = "DATE" Then
                            'data format convertion
                            Dim tempDate As String = Replace(myGridView.Rows(RowIndex).Cells(CInt(myDS3.Tables(0).Rows(i).Item(1))).Text, "amp;", "")
                            tempDate = ConvertDateToDecimal(tempDate, ComName, ComMod)
                            ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "='" & tempDate & "' And "

                        ElseIf myDS3.Tables(0).Rows(i).Item(2) = "TIME" Then
                            'time format convertion
                            Dim tempTime As String = Replace(Replace(myGridView.Rows(RowIndex).Cells(CInt(myDS3.Tables(0).Rows(i).Item(1))).Text, "amp;", ""), "&nbsp;", "")
                            If tempTime <> "" Then
                                tempTime = UnDisplayTime(tempTime, ComName, ComMod)
                            Else
                                tempTime = ""
                            End If
                            ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "='" & tempTime & "' And "

                        ElseIf myDS3.Tables(0).Rows(i).Item(2) = "DATETIME" Then
                            'datetime format convertion
                            Dim tempDateTime As String = Replace(myGridView.Rows(RowIndex).Cells(CInt(myDS3.Tables(0).Rows(i).Item(1))).Text, "amp;", "")
                            tempDateTime = UnDisplayDateTime(tempDateTime, ComName, ComMod)
                            ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "='" & tempDateTime & "' And "

                        ElseIf myDS3.Tables(0).Rows(i).Item(2) = "OPTION" Then
                            ssql1 = "Select dbo.fn_GetOptionCode('" & FormID & "','" & myDS3.Tables(0).Rows(i).Item(0) & "','" & Replace(Replace(myGridView.Rows(RowIndex).Cells(CInt(myDS3.Tables(0).Rows(i).Item(1))).Text, "amp;", ""), "&nbsp;", "") & "')"
                            myDS4 = mySQL.ExecuteSQL(ssql1)
                            If myDS4.Tables(0).Rows.Count > 0 Then
                                ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "='" & myDS4.Tables(0).Rows(0).Item(0).ToString & "' And "
                            End If
                            myDS4 = Nothing

                        ElseIf myDS3.Tables(0).Rows(i).Item(2) = "LOOKUP" Then
                            If myDS3.Tables(0).Rows(i).Item(0).ToString.ToUpper = "EMPLOYEE_PROFILE_ID" Then
                                ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "=dbo.fn_ReturnEmpIDByCodeName(N'" & ComName & "',N'" & myGridView.Rows(RowIndex).Cells(CInt(myDS3.Tables(0).Rows(i).Item(1))).Text.Replace("&amp;", "&").Replace("'", "''") & "') And "
                            Else
                                Dim myDST As New DataSet
                                ssql5 = "Select Function_Name,Default_Value,Default_Value2,Default_Value3 From User_Define_Function Where Company_Profile_Code='" & ComName & "' And Module_Profile_Code='" & ComMod & "' And Table_Profile_Code='" & FormID & "' And Table_Field_Code='" & myDS3.Tables(0).Rows(i).Item(0).ToString & "' And Query_Action='DELETE'"
                                myDST = mySQL.ExecuteSQL(ssql5)
                                If myDST.Tables(0).Rows.Count > 0 Then
                                    If myDST.Tables(0).Rows(0).Item(3).ToString <> "" Then
                                        ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "=" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & Replace(Replace(myGridView.Rows(RowIndex).Cells(CInt(myDS3.Tables(0).Rows(i).Item(1))).Text.Replace("'", "''"), "amp;", ""), "&nbsp;", "") & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString & "',N'" & myDST.Tables(0).Rows(0).Item(2).ToString & "',N'" & myDST.Tables(0).Rows(0).Item(3).ToString & "') And "
                                    ElseIf myDST.Tables(0).Rows(0).Item(2).ToString <> "" Then
                                        If UCase(myDST.Tables(0).Rows(0).Item(2).ToString) = "COMPANY" Then
                                            If myDST.Tables(0).Rows(0).Item(1).ToString = "" Then
                                                ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "=" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & Replace(Replace(myGridView.Rows(RowIndex).Cells(CInt(myDS3.Tables(0).Rows(i).Item(1))).Text.Replace("'", "''"), "amp;", ""), "&nbsp;", "") & "',N'" & ComName & "') And "
                                            Else
                                                ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "=" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & Replace(Replace(myGridView.Rows(RowIndex).Cells(CInt(myDS3.Tables(0).Rows(i).Item(1))).Text.Replace("'", "''"), "amp;", ""), "&nbsp;", "") & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString & "',N'" & ComName & "') And "
                                            End If
                                        Else
                                            ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "=" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & Replace(Replace(myGridView.Rows(RowIndex).Cells(CInt(myDS3.Tables(0).Rows(i).Item(1))).Text.Replace("'", "''"), "amp;", ""), "&nbsp;", "") & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString & "',N'" & myDST.Tables(0).Rows(0).Item(2).ToString & "') And "
                                        End If
                                    ElseIf myDST.Tables(0).Rows(0).Item(1).ToString <> "" Then
                                        ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "=" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & Replace(Replace(myGridView.Rows(RowIndex).Cells(CInt(myDS3.Tables(0).Rows(i).Item(1))).Text.Replace("'", "''"), "amp;", ""), "&nbsp;", "") & "',N'" & myDST.Tables(0).Rows(0).Item(1).ToString & "') And "
                                    Else
                                        ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "=" & myDST.Tables(0).Rows(0).Item(0).ToString & "(N'" & Replace(Replace(myGridView.Rows(RowIndex).Cells(CInt(myDS3.Tables(0).Rows(i).Item(1))).Text.Replace("'", "''"), "amp;", ""), "&nbsp;", "") & "') And "
                                    End If
                                Else
                                    ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "=N'" & Replace(Replace(myGridView.Rows(RowIndex).Cells(CInt(myDS3.Tables(0).Rows(i).Item(1))).Text.Replace("'", "''"), "amp;", ""), "&nbsp;", "") & "' And "
                                End If
                                myDST = Nothing
                            End If
                        Else
                            ssql = ssql & myDS3.Tables(0).Rows(i).Item(0) & "='" & Replace(Replace(myGridView.Rows(RowIndex).Cells(CInt(myDS3.Tables(0).Rows(i).Item(1))).Text.Replace("'", "''"), "amp;", ""), "&nbsp;", "") & "' And "
                        End If
                    Next
                    ssql = Left(ssql, Len(ssql) - 5)
                    myDS3 = Nothing
                End If
                If ssql = "Delete From " & FormID & " Where " Then ssql = Nothing
                '//////////////////////////////////////////////////////////////////

        End Select
        Return ssql
    End Function

    Sub ConvertUppercase(ByVal myTextBox As System.Web.UI.WebControls.TextBox)
        myTextBox.Attributes.Add("onKeyUp", "this.value=this.value.toUpperCase();")
    End Sub

    Sub ConvertLowercase(ByVal myTextBox As System.Web.UI.WebControls.TextBox)
        myTextBox.Attributes.Add("onKeyUp", "this.value=this.value.toLowerCase();")
    End Sub

    Function ValidateInput(ByVal myControl As Control, ByVal strDataType As String) As Boolean
        Dim myDataType As DataType
        Select Case strDataType
            Case "CHARACTER"
                myDataType = DataType._CHARACTER
            Case "LOOKUP"
                myDataType = DataType._LOOKUP
            Case "OPTION"
                myDataType = DataType._OPTION
            Case "DECIMAL"
                myDataType = DataType._DECIMAL
            Case "INTEGER"
                myDataType = DataType._INTEGER
            Case "DATETIME"
                myDataType = DataType._DATETIME
        End Select

        Select Case myDataType
            Case DataType._CHARACTER, DataType._LOOKUP, DataType._OPTION
                If TypeOf (myControl) Is TextBox Then
                    Dim myTextBox As TextBox = myControl
                    myTextBox.Text = Replace(myTextBox.Text, "'", "''")
                    'If Not Trim(myTextBox.Text) = "" Then
                    '    Return True
                    'Else
                    '    Return False
                    'End If
                    Return True
                ElseIf TypeOf (myControl) Is DropDownList Then
                    Dim myDropdownlist As DropDownList = myControl
                    'If Not Trim(myDropdownlist.SelectedValue) = "" Then
                    '    Return True
                    'Else
                    '    Return False
                    'End If
                    Return True
                End If
            Case DataType._DATETIME
                If TypeOf (myControl) Is TextBox Then
                    Dim myTextBox As TextBox = myControl
                    If IsDate(Format(Trim(myTextBox.Text), "dd/mm/yyyy")) Then
                        Return True
                    Else
                        Return False
                    End If
                ElseIf TypeOf (myControl) Is DropDownList Then
                    Dim myDropdownlist As DropDownList = myControl
                    If Not IsDate(Format(Trim(myDropdownlist.SelectedValue), "dd/mm/yyyy")) Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Case DataType._DECIMAL, DataType._INTEGER
                If TypeOf (myControl) Is TextBox Then
                    Dim myTextBox As TextBox = myControl
                    If IsNumeric(Trim(myTextBox.Text)) Then
                        Return True
                    Else
                        Return False
                    End If
                ElseIf TypeOf (myControl) Is DropDownList Then
                    Dim myDropdownlist As DropDownList = myControl
                    If Not IsNumeric(Trim(myDropdownlist.SelectedValue)) Then
                        Return True
                    Else
                        Return False
                    End If
                End If
        End Select

    End Function

    Sub GetDropdownlistValue(ByVal FormID As String, ByVal FieldID As String, ByVal myDropdownlist As System.Web.UI.WebControls.DropDownList)
        myDS = mySQL.ExecuteSQL("Create Table #Result([Code] nvarchar(50), [Name] nvarchar(255)); Insert Into #Result Select '',''; Insert Into #Result Select Replace(Code,'''',('''' + '''')),Name From [Option] where Table_Profile_Code='" & FormID & "' And Table_Field_Code='" & FieldID & "' Order By Sort_Key,Name; Select * From #Result; Drop Table #Result")
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Columns.Count > 1 Then
                myDropdownlist.DataTextField = "Name"
                myDropdownlist.DataValueField = "Code"
                myDropdownlist.DataSource = myDS
                myDropdownlist.DataBind()
            End If
        End If
    End Sub

    Sub ArrangeDropdownlistSelectedIndex(ByVal myDropdownlist As System.Web.UI.WebControls.DropDownList, ByVal strValueToMatch As String)
        RecFound = False
        For i = 0 To myDropdownlist.Items.Count - 1
            If myDropdownlist.Items(i).Text = strValueToMatch Then
                myDropdownlist.SelectedIndex = i
                RecFound = True
                Exit For
            End If
        Next
        If RecFound = False Then
            myDropdownlist.SelectedIndex = -1
        End If
    End Sub

    Sub ArrangeDDLSelectedIndex(ByVal myDropdownlist As System.Web.UI.WebControls.DropDownList, ByVal Value As DDLSelection, ByVal strMatch As String)
        RecFound = False
        For i = 0 To myDropdownlist.Items.Count - 1
            Select Case Value
                Case DDLSelection.SelectedText
                    If myDropdownlist.Items(i).Text = strMatch Then
                        myDropdownlist.SelectedIndex = i
                        RecFound = True
                        Exit For
                    End If
                Case DDLSelection.SelectedValue
                    If myDropdownlist.Items(i).Value = strMatch Then
                        myDropdownlist.SelectedIndex = i
                        RecFound = True
                        Exit For
                    End If
            End Select
        Next
        If RecFound = False Then
            myDropdownlist.SelectedIndex = 0
        End If
    End Sub

    Public Function ConvertDateToDecimal(ByVal strDate As String, ByVal strCom As String, ByVal strMod As String) As String

        Dim strDay, strMon, strYear, yearNow, DftDateSeparator, DftTimeCycle As String
        Dim DftDateFormat As String = ""
        Dim x As Integer

        If Len(strDate) = 10 Or Len(strDate) = 8 Then    ' start if _0
            myDS = mySQL.ExecuteSQL("select string_value1,string_value2,string_value3 from parameter where company_profile_code='" & strCom & "' and module_profile_code='" & strMod & "' and code='REGIONAL_SETTING'")
            If myDS.Tables.Count > 0 And myDS.Tables(0).Rows.Count > 0 And myDS.Tables(0).Columns.Count > 2 Then
                DftDateFormat = myDS.Tables(0).Rows(0).Item(0).ToString
                DftDateSeparator = myDS.Tables(0).Rows(0).Item(1).ToString
                DftTimeCycle = myDS.Tables(0).Rows(0).Item(2).ToString
            End If
            myDS = Nothing

            'remove seperator & convert date to decimal format
            strDate = Replace(strDate, "/", "")
            strDate = Replace(strDate, "-", "")
            strDate = Replace(strDate, "@", "")
            strDate = Replace(strDate, "\", "")
            strDate = Replace(strDate, "|", "")

            If Len(strDate) = 8 Then       ' start if _1
                Select Case DftDateFormat  ' select start
                    Case "DD/MM/YYYY"
                        strDay = Left(strDate, 2)
                        strMon = Mid(strDate, 3, 2)
                        strYear = Right(strDate, 4)
                    Case "MM/DD/YYYY"
                        strMon = Left(strDate, 2)
                        strDay = Mid(strDate, 3, 2)
                        strYear = Right(strDate, 4)
                    Case "YYYY/MM/DD"
                        strYear = Left(strDate, 4)
                        strMon = Mid(strDate, 5, 2)
                        strDay = Right(strDate, 2)
                    Case "YYYY/DD/MM"
                        strYear = Left(strDate, 4)
                        strDay = Mid(strDate, 5, 2)
                        strMon = Right(strDate, 2)
                    Case Else
                        strDay = Left(strDate, 2)
                        strMon = Mid(strDate, 3, 2)
                        strYear = Right(strDate, 4)
                End Select                  ' select end
                If Len(strYear) > 4 Then
                    strDate = "0"
                    Return strDate
                    Exit Function
                End If
                If CInt(strMon) < 13 Then
                    If CInt(strMon) = 1 Or CInt(strMon) = 3 Or CInt(strMon) = 5 Or CInt(strMon) = 7 Or CInt(strMon) = 8 Or CInt(strMon) = 10 Or CInt(strMon) = 12 Then
                        If CInt(strDay) < 32 Then
                            strDate = strYear & strMon & strDay
                        Else
                            strDate = "0"
                            Return strDate
                            Exit Function
                        End If
                    Else
                        If CInt(strDay) < 31 Then
                            strDate = strYear & strMon & strDay
                        Else
                            strDate = "0"
                            Return strDate
                            Exit Function
                        End If
                    End If
                Else
                    strDate = "0"
                    Return strDate
                    Exit Function
                End If

            ElseIf Len(strDate) = 6 Then
                Select Case DftDateFormat
                    Case "DD/MM/YY"
                        strDay = Left(strDate, 2)
                        strMon = Mid(strDate, 3, 2)
                        strYear = Right(strDate, 2)
                    Case "MM/DD/YY"
                        strMon = Left(strDate, 2)
                        strDay = Mid(strDate, 3, 2)
                        strYear = Right(strDate, 2)
                    Case "YY/MM/DD"
                        strYear = Left(strDate, 2)
                        strMon = Mid(strDate, 3, 2)
                        strDay = Right(strDate, 2)
                    Case "YY/DD/MM"
                        strYear = Left(strDate, 2)
                        strDay = Mid(strDate, 3, 2)
                        strMon = Right(strDate, 2)
                    Case Else
                        strDay = Left(strDate, 2)
                        strMon = Mid(strDate, 3, 2)
                        strYear = Right(strDate, 2)
                End Select

                yearNow = DatePart(DateInterval.Year, Now())
                Dim yearNow_ As String = Right(yearNow, 2)
                yearNow = Left(yearNow, 2)
                If strYear > yearNow_ Then
                    yearNow = CInt(yearNow) - 1
                End If

                If strMon < 13 Then
                    If strMon = "1" Or strMon = "3" Or strMon = "5" Or strMon = "7" Or strMon = "8" Or strMon = "10" Or strMon = "12" Then
                        If strDay < 32 Then
                            strDate = yearNow & strYear & strMon & strDay
                        Else
                            strDate = "0"
                            Return strDate
                            Exit Function
                        End If
                    Else
                        If strDay < 31 Then
                            strDate = yearNow & strYear & strMon & strDay
                        Else
                            strDate = "0"
                            Return strDate
                            Exit Function
                        End If
                    End If
                Else
                    strDate = "0"
                    Return strDate
                    Exit Function
                End If
            Else
                strDate = "0"
                Return strDate
                Exit Function
            End If   ' end if _1

            For x = 0 To 13
                If Len(strDate) = 14 Then
                    Exit For
                Else
                    strDate = strDate & "0"
                End If
            Next
        End If      ' end if _0

        Return strDate

    End Function

    Public Sub SetGridViewEditable(ByVal TableProfileCode As String, ByVal myGridView As GridView, ByVal EditRow As Integer)
        myDS = mySQL.ExecuteSQL("SELECT [NAME],OPTION_EDITABLE FROM TABLE_FIELD WHERE TABLE_PROFILE_CODE='" & TableProfileCode & "' AND OPTION_VIEW='YES' ORDER BY SEQUENCE_NO")
        For i = 0 To myDS.Tables(0).Rows.Count - 1
            If myDS.Tables(0).Rows(i).Item(1).ToString = "NO" Then
                myGridView.Rows(EditRow).Cells(i + 2).Enabled = False
            End If
        Next
    End Sub

    Public Function GetCurrentDate(ByVal strCompanyID As String, ByVal strModule As String) As String
        Dim myDate As DateTime
        Dim sDay As String = "", sMonth As String = "", sYear As String = ""
        Dim FinalDate As String = ""
        Dim strjscript As String = ""

        Try
            myDS = mySQL.ExecuteSQL("select string_value1,string_value2,string_value3 from parameter where company_profile_code='" & strCompanyID & "' and module_profile_code='" & strModule & "' and code='REGIONAL_SETTING'")
            If myDS.Tables.Count > 0 And myDS.Tables(0).Rows.Count > 0 And myDS.Tables(0).Columns.Count > 2 Then
                Dim DefaultDateFormat As String = myDS.Tables(0).Rows(0).Item(0).ToString
                Dim DefaultDateSeparator As String = myDS.Tables(0).Rows(0).Item(1).ToString
                Dim DefaultDateCycle As String = myDS.Tables(0).Rows(0).Item(2).ToString

                myDate = Now()
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
                myDS = Nothing
                Return FinalDate
            Else
                GoTo DefaultSetting
            End If
        Catch ex As Exception
            GoTo DefaultSetting
        End Try

DefaultSetting:
        myDate = Now()
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
        Return FinalDate
    End Function

    Public Sub SetGridViewUpdatingCondition(ByVal myGridView As GridView, ByVal RowIndex As Integer, ByVal strTableName As String)
        myDS = mySQL.ExecuteSQL("Select Option_Primary_Key,Option_Editable,Option_Password,Name From Table_Field Where Table_Profile_Code ='" & strTableName & "' And Option_View='YES' Order By Sequence_No")
        If myDS.Tables(0).Rows.Count > 0 Then
            For i = 0 To myDS.Tables(0).Rows.Count - 1
                For j = 2 To myGridView.HeaderRow.Cells.Count - 1
                    If myDS.Tables(0).Rows(i).Item(3).ToString = myGridView.HeaderRow.Cells(j).Text.ToString Then
                        Dim myTextBox As TextBox = myGridView.Rows(RowIndex).Cells(i + 2).Controls(0)
                        If myDS.Tables(0).Rows(i).Item(2).ToString = "YES" Then
                            myTextBox.TextMode = TextBoxMode.Password
                        End If
                        If myDS.Tables(0).Rows(i).Item(0).ToString = "YES" Or myDS.Tables(0).Rows(i).Item(1) = "NO" Then
                            myTextBox.Enabled = False
                            Exit For
                        End If
                    End If
                Next
            Next
        End If
    End Sub

    Public Function FirstDayOfMonth(ByVal myDate As DateTime) As String
        Dim dtOutput As DateTime, sDay As String = "", sMonth As String = "", sYear As String = ""
        'sDay = DatePart(DateInterval.Day, myDate)
        'If Len(sDay) = 1 And Len(sDay) < 2 Then
        '    sDay = "0" & sDay
        'End If
        sDay = "01"
        sMonth = DatePart(DateInterval.Month, myDate)
        If Len(sMonth) = 1 And Len(sMonth) < 2 Then
            sMonth = "0" & sMonth
        End If
        sYear = DatePart(DateInterval.Year, myDate)
        dtOutput = CDate(sDay & "/" & sMonth & "/" & sYear)
        Return dtOutput
    End Function

    Public Function FirstDayOfWeek(ByVal myDate As Date) As Integer
        Return 0
    End Function

    Public Function UnDisplayTime(ByVal strTimeValue As String, ByVal strCompanyID As String, ByVal strModule As String) As String
        Dim strOutput As String, strDateTimeFormat As String, strTimeFormat As String, strHour As String
        Dim strMinute As String, strSecond As String, strSeparator As String, strShowSecond As String, strAmPm As String
        Dim myDR As DataRow
        myDR = mySQL.ExecuteSQLByReturnRow("Select String_Value1,':',String_Value3,String_Value4,'AM' From Parameter Where Code='REGIONAL_SETTING' And Company_Profile_Code='" & strCompanyID.ToString & "' And Module_Profile_Code='" & strModule & "'")
        If Not myDR Is Nothing Then
            'strOutput = ""
            strDateTimeFormat = myDR(0).ToString
            strSeparator = myDR(1).ToString
            strTimeFormat = myDR(2).ToString
            strShowSecond = myDR(3).ToString
            strAmPm = myDR(4).ToString

            If strTimeFormat = "12" Then
                strAmPm = Right(strTimeValue, 2)
                strTimeValue = Left(strTimeValue, Len(strTimeValue) - 3)
                If strShowSecond = "Y" Then
                    strHour = Left(strTimeValue, 2)
                    strMinute = Mid(strTimeValue, 4, 2)
                    strSecond = Mid(strTimeValue, 7, 2)
                Else
                    strHour = Left(strTimeValue, 2)
                    strMinute = Mid(strTimeValue, 4, 2)
                    strSecond = "00"
                End If
                If strAmPm = "PM" Then
                    If CInt(strHour) <> 12 Then
                        strHour = CInt(strHour) + 12
                    End If
                End If
                If strAmPm = "AM" Then
                    If CInt(strHour) = 12 Then
                        strHour = "00"
                    End If
                End If
                strOutput = strHour & strMinute & strSecond
            Else 'If strTimeFormat = "24" Then
                If strShowSecond = "Y" Then
                    strHour = Left(strTimeValue, 2)
                    strMinute = Mid(strTimeValue, 4, 2)
                    strSecond = Mid(strTimeValue, 7, 2)
                Else
                    strHour = Left(strTimeValue, 2)
                    strMinute = Mid(strTimeValue, 4, 2)
                    strSecond = "00"
                End If
                strOutput = strHour & strMinute & strSecond
            End If
        Else
            strOutput = ""
        End If
        Return strOutput
    End Function

    Public Function UnDisplayDateTime(ByVal strDateTimeValue As String, ByVal strCompanyID As String, ByVal strModule As String) As String
        Dim strOutput As String, strDateTimeFormat As String, strTimeFormat As String, strHour As String
        Dim strMinute As String, strSecond As String, strSeparator As String, strShowSecond As String
        Dim strDay As String, strMonth As String, strYear As String, strTimeSeparator As String = ":"
        Dim myDR As DataRow, strDefaultYear As String = "20"
        myDR = mySQL.ExecuteSQLByReturnRow("Select String_Value1,String_Value2,String_Value3,String_Value4 From Parameter Where Code='REGIONAL_SETTING' And Company_Profile_Code='" & strCompanyID.ToString & "' And Module_Profile_Code='" & strModule & "'")
        If Not myDR Is Nothing Then
            strOutput = ""
            strDateTimeFormat = myDR(0).ToString
            strSeparator = myDR(1).ToString
            strTimeFormat = myDR(2).ToString
            strShowSecond = myDR(3).ToString
            'strAmPm = myDR(4).ToString

            strDateTimeValue = Replace(strDateTimeValue, strSeparator, "")
            strDateTimeValue = Replace(strDateTimeValue, strTimeSeparator, "")
            strDateTimeValue = Replace(strDateTimeValue, " ", "")

            If strShowSecond = "Y" Then
                strSecond = Right(Replace(Replace(strDateTimeValue, "AM", ""), "PM", ""), 2)
            Else
                strSecond = "00"
            End If

            If strDateTimeFormat = "DD/MM/YYYY" Then
                strDay = Mid(strDateTimeValue, 1, 2)
                strMonth = Mid(strDateTimeValue, 3, 2)
                strYear = Mid(strDateTimeValue, 5, 4)
                strHour = Mid(strDateTimeValue, 9, 2)
                strMinute = Mid(strDateTimeValue, 11, 2)
            ElseIf strDateTimeFormat = "DD/YYYY/MM" Then
                strDay = Mid(strDateTimeValue, 1, 2)
                strMonth = Mid(strDateTimeValue, 7, 2)
                strYear = Mid(strDateTimeValue, 3, 4)
                strHour = Mid(strDateTimeValue, 9, 2)
                strMinute = Mid(strDateTimeValue, 11, 2)
            ElseIf strDateTimeFormat = "MM/DD/YYYY" Then
                strDay = Mid(strDateTimeValue, 3, 2)
                strMonth = Mid(strDateTimeValue, 1, 2)
                strYear = Mid(strDateTimeValue, 5, 4)
                strHour = Mid(strDateTimeValue, 9, 2)
                strMinute = Mid(strDateTimeValue, 11, 2)
            ElseIf strDateTimeFormat = "MM/YYYY/DD" Then
                strDay = Mid(strDateTimeValue, 7, 2)
                strMonth = Mid(strDateTimeValue, 1, 2)
                strYear = Mid(strDateTimeValue, 3, 4)
                strHour = Mid(strDateTimeValue, 9, 2)
                strMinute = Mid(strDateTimeValue, 11, 2)
            ElseIf strDateTimeFormat = "YYYY/MM/DD" Then
                strDay = Mid(strDateTimeValue, 7, 2)
                strMonth = Mid(strDateTimeValue, 5, 2)
                strYear = Mid(strDateTimeValue, 1, 4)
                strHour = Mid(strDateTimeValue, 9, 2)
                strMinute = Mid(strDateTimeValue, 11, 2)
            ElseIf strDateTimeFormat = "YYYY/DD/MM" Then
                strDay = Mid(strDateTimeValue, 5, 2)
                strMonth = Mid(strDateTimeValue, 7, 2)
                strYear = Mid(strDateTimeValue, 1, 4)
                strHour = Mid(strDateTimeValue, 9, 2)
                strMinute = Mid(strDateTimeValue, 11, 2)
            ElseIf strDateTimeFormat = "DD/MM/YY" Then
                strDay = Mid(strDateTimeValue, 1, 2)
                strMonth = Mid(strDateTimeValue, 3, 2)
                strYear = strDefaultYear + Mid(strDateTimeValue, 5, 2)
                strHour = Mid(strDateTimeValue, 7, 2)
                strMinute = Mid(strDateTimeValue, 9, 2)
            ElseIf strDateTimeFormat = "DD/YY/MM" Then
                strDay = Mid(strDateTimeValue, 1, 2)
                strMonth = Mid(strDateTimeValue, 3, 2)
                strYear = strDefaultYear + Mid(strDateTimeValue, 5, 4)
                strHour = Mid(strDateTimeValue, 7, 2)
                strMinute = Mid(strDateTimeValue, 9, 2)
            ElseIf strDateTimeFormat = "MM/DD/YY" Then
                strDay = Mid(strDateTimeValue, 3, 2)
                strMonth = Mid(strDateTimeValue, 1, 2)
                strYear = strDefaultYear + Mid(strDateTimeValue, 5, 2)
                strHour = Mid(strDateTimeValue, 7, 2)
                strMinute = Mid(strDateTimeValue, 9, 2)
            ElseIf strDateTimeFormat = "MM/YY/DD" Then
                strDay = Mid(strDateTimeValue, 5, 2)
                strMonth = Mid(strDateTimeValue, 1, 2)
                strYear = strDefaultYear + Mid(strDateTimeValue, 3, 2)
                strHour = Mid(strDateTimeValue, 7, 2)
                strMinute = Mid(strDateTimeValue, 9, 2)
            ElseIf strDateTimeFormat = "YY/MM/DD" Then
                strDay = Mid(strDateTimeValue, 5, 2)
                strMonth = Mid(strDateTimeValue, 3, 2)
                strYear = strDefaultYear + Mid(strDateTimeValue, 1, 2)
                strHour = Mid(strDateTimeValue, 7, 2)
                strMinute = Mid(strDateTimeValue, 9, 2)
            ElseIf strDateTimeFormat = "YY/DD/MM" Then
                strDay = Mid(strDateTimeValue, 3, 2)
                strMonth = Mid(strDateTimeValue, 5, 2)
                strYear = strDefaultYear + Mid(strDateTimeValue, 1, 2)
                strHour = Mid(strDateTimeValue, 7, 2)
                strMinute = Mid(strDateTimeValue, 9, 2)
            Else
                strDay = ""
                strMonth = ""
                strYear = ""
                strHour = ""
                strMinute = ""
            End If
            If strMinute = "" Then
                strMinute = "00"
            End If
            If strHour = "" Then
                strHour = "00"
            End If
            If strTimeFormat = "12" Then
                If Right(strDateTimeValue, 2) = "PM" Then
                    If CInt(strHour) <> 12 Then
                        strHour = CStr(CInt(strHour) + 12)
                    End If
                    If Len(strHour) = 1 Then
                        strHour = "0" & strHour
                    End If
                End If
                If Right(strDateTimeValue, 2) = "AM" Then
                    If CInt(strHour) = 12 Then
                        strHour = "00"
                    End If
                    If Len(strHour) = 1 Then
                        strHour = "0" & strHour
                    End If
                End If
                strOutput = strYear & strMonth & strDay & strHour & strMinute & strSecond
            ElseIf strTimeFormat = "24" Then
                strOutput = strYear & strMonth & strDay & strHour & strMinute & strSecond
            End If
        Else
            strOutput = ""
        End If
        Return strOutput
    End Function
    Public Function ProcessQuerySyntax(ByVal strInput As String) As String
        Dim arrWords As String() = New String() {">=", "<=", ">", "<", "|", "%", "~", ""}
        Dim strOutput As String = strInput
        Dim strWord As String, RecFound As Boolean = False
        For Each strWord In arrWords
            If strInput.IndexOf(strWord, 0) >= 0 Then
                strOutput = strWord
                RecFound = True
                Exit For
            End If
        Next
        If RecFound = True Then
            Return strOutput
        Else
            Return ""
        End If
    End Function
    Function DeleteQuerySyntax(ByVal strInput As String, ByVal strDataType As DataType) As Boolean
        Select Case strDataType
            Case DataType._DECIMAL, DataType._INTEGER
            Case DataType._CHARACTER, DataType._LOOKUP, DataType._LOOKUP
            Case DataType._DATE, DataType._DATETIME, DataType._TIME
        End Select
        Return True
    End Function
    Public Function ValidateQuerySyntax(ByVal strInput As String) As Boolean
        Dim arrWords As String() = New String() {">=", "<=", ">", "<", "|", "%", "~"}
        Dim strWord As String, intCount As Integer = 0, intPos As Integer = 0, intLeftMidRight As Integer
        Dim tmpInput As String = strInput, tmpSyntax As String = "", intLoop As Integer = 0

        For i = 0 To arrWords.Length - 1
            strWord = arrWords(i)
            If tmpInput.IndexOf(strWord, 0) >= 0 Then
                intLoop += 1
                If tmpSyntax = "" Then
                    intCount += 1
                    tmpSyntax = strWord
                End If
                intPos = InStr(tmpInput, strWord)
                If intPos = 1 Then
                    intLeftMidRight = -1
                ElseIf intPos > 1 And intPos < Len(strInput) Then
                    intLeftMidRight = 0
                ElseIf intPos = Len(strInput) Then
                    intLeftMidRight = 1
                End If
                Select Case intLeftMidRight
                    Case -1
                        tmpInput = Mid(tmpInput, intPos * Len(strWord) + 1, Len(strInput))
                    Case 0
                        tmpInput = Mid(tmpInput, 1, intPos * Len(strWord) - 1) & Mid(tmpInput, intPos * Len(strWord) + intLoop, Len(tmpInput))
                    Case 1
                        tmpInput = Mid(tmpInput, 1, Len(strInput) - Len(strWord))
                End Select
                i = 0
            End If
        Next
        'If intCount <= 1 Then
        '    Return True
        'Else
        '    Return False
        'End If
        If intLoop <= 1 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Sub AddQuerySyntax(ByVal myArr As ArrayList, ByVal strInput As String)
        Dim arrWords As String() = New String() {">=", "<=", ">", "<", "|", "%", "~"}
        Dim strWord As String, intCount As Integer = 0, intPos As Integer = 0, intLeftMidRight As Integer
        Dim tmpInput As String = strInput
        myArr.Clear()
        For i = 0 To arrWords.Length - 1
            strWord = arrWords(i)
            If tmpInput.IndexOf(strWord, 0) >= 0 Then
                intCount = Len(strInput) - Len(Replace(strInput, strWord, ""))
                intCount = intCount / Len(strWord)
                intPos = InStr(tmpInput, strWord)
                If intPos = 1 Then
                    intLeftMidRight = -1
                ElseIf intPos > 1 And intPos < Len(strInput) Then
                    intLeftMidRight = 0
                ElseIf intPos = Len(strInput) Then
                    intLeftMidRight = 1
                End If
                Select Case intLeftMidRight
                    Case -1
                        myArr.Add(Mid(tmpInput, intPos * Len(strWord) + 1, Len(strInput)))
                        tmpInput = Mid(tmpInput, intPos * Len(strWord) + 1, Len(strInput))
                        Exit For
                    Case 0
                        For j = 0 To intCount
                            myArr.Add(Mid(tmpInput, 1, intPos * Len(strWord) - 1))
                            tmpInput = Mid(tmpInput, intPos * Len(strWord) + j + 1, Len(tmpInput))
                        Next
                        Exit For
                    Case 1
                        myArr.Add(Mid(tmpInput, 1, Len(strInput) - Len(strWord)))
                        tmpInput = Mid(tmpInput, 1, Len(strInput) - Len(strWord))
                        Exit For
                End Select
            End If
        Next
    End Sub
    Public Function GetDefaultValue(ByVal strCompany As String, ByVal strModule As String, ByVal strTableProfileCode As String, ByVal strTableFieldCode As String, ByVal strOptionDataType As String) As String
        Dim myDR As DataRow
        ssql = "Select Default_Value_"
        Select Case strOptionDataType
            Case "DATE"
                ssql = ssql & strOptionDataType
            Case "DATETIME"
                ssql = ssql & strOptionDataType
            Case "TIME"
                ssql = ssql & strOptionDataType
            Case "OPTION"
                'Do nothing
            Case "DECIMAL"
                ssql = ssql & strOptionDataType
            Case "INTEGER"
                ssql = ssql & strOptionDataType
            Case "LOOKUP"
                'Do nothing
            Case "CHARACTER"
                ssql = ssql & strOptionDataType
        End Select

        If ssql <> "Select Default_Value_" Then
            ssql = ssql & " From Table_Field Where Table_Profile_Code'" & strTableProfileCode & "' And Code='" & strTableFieldCode & "'"
            myDR = mySQL.ExecuteSQLByReturnRow(ssql)
            If Not myDR Is Nothing Then
                If myDR(0).ToString.Trim = "" Then
                    ssql = "Select Function_Name,Default_Value From User_Define_Functon Where Company_Profile_Code='" & _
                            strCompany & "' And Module_Profile_Code='" & strModule & "' And Table_Profile_Code='" & _
                            strTableProfileCode & "' And Table_Field_Code='" & strTableFieldCode & "'"
                    myDR = mySQL.ExecuteSQLByReturnRow(ssql)
                    If Not myDR Is Nothing Then
                        Dim dr As DataRow = mySQL.ExecuteSQLByReturnRow("Select " & myDR.Item(0).ToString & "(" & myDR.Item(1).ToString & ")")
                        Return dr(0).ToString
                    Else
                        Return ""
                    End If
                Else
                    Return myDR(0).ToString.Trim
                End If
            Else
                Return ""
            End If
        Else
            Return ""
        End If
    End Function
    Function SessionTimeOut() As Integer
        Dim ReadIniFile As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "Setup\Setup.ini")
        Return (ReadIniFile.GetString("Setting", "SessionTimeOut", "(none)"))
    End Function
    Sub BindListBox(ByVal myListBox As ListBox, ByVal ssql As String, ByVal colCodeBehind As Integer, ByVal colDisplayText As Integer)
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql)
        If Not myDS Is Nothing Then
            If myDS.Tables.Count > 0 Then
                For i = 0 To myDS.Tables(0).Rows.Count - 1
                    myListBox.Items.Add(New ListItem(myDS.Tables(0).Rows(i).Item(colDisplayText).ToString, myDS.Tables(0).Rows(i).Item(colCodeBehind).ToString))
                Next
            End If
        End If
    End Sub
    Sub ClearListBox(ByVal myListBox As ListBox)
        myListBox.Items.Clear()
    End Sub
    Sub AddDeleteListBoxItem(ByVal lstBoxFrom As ListBox, ByVal lstBoxTo As ListBox)
        Dim i As Integer
        For i = 0 To lstBoxFrom.Items.Count - 1
            If lstBoxFrom.Items(i).Selected = True Then
                lstBoxTo.Items.Add(lstBoxFrom.Items(i))
            End If
        Next
        For i = lstBoxFrom.Items.Count - 1 To 0 Step -1
            If lstBoxFrom.Items(i).Selected = True Then
                lstBoxFrom.Items.Remove(lstBoxFrom.Items(i))
            End If
        Next
    End Sub
    Sub AddDeleteAllListBoxItem(ByVal lstBoxFrom As ListBox, ByVal lstBoxTo As ListBox)
        Dim i As Integer
        For i = 0 To lstBoxFrom.Items.Count - 1
            lstBoxTo.Items.Add(lstBoxFrom.Items(i))
        Next
        lstBoxFrom.Items.Clear()
    End Sub
    Sub SetDropdownlistDefaultIndex(ByVal myDDL As DropDownList, ByVal intIndex As Integer)
        If myDDL.Items.Count >= intIndex Then
            myDDL.SelectedIndex = intIndex
        End If
    End Sub
    Function CheckIfUserIsEmployee(ByVal strCompany As String, ByVal strUserID As String) As Boolean
        Try
            myDS = New DataSet
            myDS = mySQL.ExecuteSQL("Select [dbo].[fn_IsEmployee]('" & strCompany & "','" & strUserID & "')")
            If Not myDS Is Nothing Then
                If myDS.Tables(0).Rows.Count > 0 Then
                    If myDS.Tables(0).Rows(0).Item(0).ToString.ToUpper = "TRUE" Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            _Err = ex.Message.ToString
            Return False
        Finally
            If Not myDS Is Nothing Then
                myDS = Nothing
            End If
        End Try
    End Function

    Sub GetListBoxValue2(ByVal myListBox_Left As System.Web.UI.WebControls.ListBox, ByVal myListBox_Right As System.Web.UI.WebControls.ListBox, ByVal ssql As String)

        myDS = mySQL.ExecuteSQL(ssql)
        If myDS.Tables.Count > 0 Then
            If myDS.Tables(0).Columns.Count > 1 Then
                myListBox_Left.DataTextField = "Name"
                myListBox_Left.DataValueField = "Code"
                myListBox_Left.DataSource = myDS.Tables(0)
                myListBox_Left.DataBind()
            End If
        End If

        If myDS.Tables.Count > 0 Then
            If myDS.Tables(1).Columns.Count > 1 Then
                myListBox_Right.DataTextField = "Name"
                myListBox_Right.DataValueField = "Code"
                myListBox_Right.DataSource = myDS.Tables(1)
                myListBox_Right.DataBind()
            End If
        End If
        myDS = Nothing

    End Sub

    Public Function GetObjPosition(ByVal EctWidth As Integer, ByVal count As Integer, ByVal pos As String) As String

        'Currently only for M type 2 positioning
        'Set default value here

        Dim TolWidth As Integer = 0
        Dim TolHeight As Integer = 0
        Dim DefH As Integer = 91
        Dim DefHAdd As Integer = 32
        Dim DefW1 As Integer = 10
        Dim DefW2 As Integer = 23
        Dim DefW3 As Integer = 136
        Dim DefW4 As Integer = 158
        Dim DefW5 As Integer = 25
        Dim Result As String = ""

        'get position
        Select Case pos
            Case "Y"
                TolHeight = DefH + (DefHAdd * (Math.Round((count / 8) - 0.59, 0)))
                Result = TolHeight & "px"
                'Select Case count
                '    Case 1 To 8
                '        TolHeight = DefH + (DefHAdd * 0)
                '        Result = TolHeight & "px"
                '    Case 9 To 16
                '        TolHeight = DefH + (DefHAdd * 1)
                '        Result = TolHeight & "px"
                '    Case 17 To 24
                '        TolHeight = DefH + (DefHAdd * 2)
                '        Result = TolHeight & "px"
                '    Case 25 To 32
                '        TolHeight = DefH + (DefHAdd * 3)
                '        Result = TolHeight & "px"
                '    Case 33 To 40
                '        TolHeight = DefH + (DefHAdd * 4)
                '        Result = TolHeight & "px"
                '    Case 41 To 48
                '        TolHeight = DefH + (DefHAdd * 5)
                '        Result = TolHeight & "px"
                '    Case 49 To 56
                '        TolHeight = DefH + (DefHAdd * 6)
                '        Result = TolHeight & "px"
                '    Case 57 To 64
                '        TolHeight = DefH + (DefHAdd * 7)
                '        Result = TolHeight & "px"
                '    Case 65 To 72
                '        TolHeight = DefH + (DefHAdd * 8)
                '        Result = TolHeight & "px"
                '    Case 73 To 80
                '        TolHeight = DefH + (DefHAdd * 9)
                '        Result = TolHeight & "px"
                '    Case 81 To 88
                '        TolHeight = DefH + (DefHAdd * 10)
                '        Result = TolHeight & "px"
                '    Case 89 To 96
                '        TolHeight = DefH + (DefHAdd * 11)
                '        Result = TolHeight & "px"
                '    Case 97 To 104
                '        TolHeight = DefH + (DefHAdd * 12)
                '        Result = TolHeight & "px"
                '    Case 105 To 112
                '        TolHeight = DefH + (DefHAdd * 13)
                '        Result = TolHeight & "px"
                '    Case 113 To 120
                '        TolHeight = DefH + (DefHAdd * 14)
                '        Result = TolHeight & "px"
                '    Case 121 To 128
                '        TolHeight = DefH + (DefHAdd * 15)
                '        Result = TolHeight & "px"
                '    Case 129 To 136
                '        TolHeight = DefH + (DefHAdd * 16)
                '        Result = TolHeight & "px"
                '    Case 137 To 144
                '        TolHeight = DefH + (DefHAdd * 17)
                '        Result = TolHeight & "px"
                '    Case 145 To 152
                '        TolHeight = DefH + (DefHAdd * 18)
                '        Result = TolHeight & "px"
                '    Case 153 To 160
                '        TolHeight = DefH + (DefHAdd * 19)
                '        Result = TolHeight & "px"
                'End Select
            Case "X"
                Select Case (count Mod 8)
                    Case 1
                        TolWidth = DefW1
                        Result = TolWidth & "px"
                    Case 2
                        TolWidth = DefW1 + DefW2
                        Result = TolWidth & "px"
                    Case 3
                        TolWidth = DefW1 + DefW2 + DefW3
                        Result = TolWidth & "px"
                    Case 4
                        TolWidth = DefW1 + DefW2 + DefW3 + DefW4 + EctWidth
                        Result = TolWidth & "px"
                    Case 5
                        TolWidth = DefW1 + DefW2 + DefW3 + DefW4 + DefW5 + EctWidth
                        Result = TolWidth & "px"
                    Case 6
                        TolWidth = DefW1 + (DefW2 * 2) + DefW3 + DefW4 + DefW5 + EctWidth
                        Result = TolWidth & "px"
                    Case 7
                        TolWidth = DefW1 + (DefW2 * 2) + (DefW3 * 2) + DefW4 + DefW5 + EctWidth
                        Result = TolWidth & "px"
                    Case 0
                        TolWidth = DefW1 + (DefW2 * 2) + (DefW3 * 2) + (DefW4 * 2) + DefW5 + (EctWidth * 2)
                        Result = TolWidth & "px"
                End Select
        End Select
        Return Result

    End Function

    Public Function GetObjPositionRL(ByVal EctWidth As Integer, ByVal count As Integer, ByVal pos As String) As String

        'Currently only for M type 2 positioning
        'Set default value here

        Dim TolHeight As Integer = 0
        Dim DefHAdd As Integer = 32
        Dim DefY1 As Integer = 116
        Dim DefY2 As Integer = 141
        Dim DefY3 As Integer = 166
        Dim DefY4 As Integer = 191
        Dim DefY5 As Integer = 91
        Dim DefY6 As Integer = 91
        Dim Result As String = ""

        Dim TolWidth As Integer = 0
        Dim DefX1 As Integer = 360
        Dim DefX2 As Integer = 360
        Dim DefX3 As Integer = 360
        Dim DefX4 As Integer = 360
        Dim DefX5 As Integer = 415
        Dim DefX6 As Integer = 577

        'get position
        Select Case pos
            Case "Y"
                Select Case EctWidth
                    Case 1
                        TolHeight = DefY1 + (DefHAdd * (count - 1))
                    Case 2
                        TolHeight = DefY2 + (DefHAdd * (count - 1))
                    Case 3
                        TolHeight = DefY3 + (DefHAdd * (count - 1))
                    Case 4
                        TolHeight = DefY4 + (DefHAdd * (count - 1))
                    Case 5
                        TolHeight = DefY5 + (DefHAdd * (count - 1))
                    Case 6
                        TolHeight = DefY6 + (DefHAdd * (count - 1))
                End Select
                Result = TolHeight & "px"

            Case "X"
                Select Case count
                    Case 1
                        TolWidth = DefX1 + EctWidth
                    Case 2
                        TolWidth = DefX2 + EctWidth
                    Case 3
                        TolWidth = DefX3 + EctWidth
                    Case 4
                        TolWidth = DefX4 + EctWidth
                    Case 5
                        TolWidth = DefX5 + EctWidth
                    Case 6
                        TolWidth = DefX6 + (EctWidth * 2)
                End Select
                Result = TolWidth & "px"

        End Select
        Return Result

    End Function

End Class
