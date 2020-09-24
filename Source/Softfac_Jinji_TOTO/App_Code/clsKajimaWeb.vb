Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web.SessionState.HttpSessionState
Imports System.Web.UI.Page


Public Class clsKajimaWeb

    Public Const gstrDomain As String = "smtp.office365.com"
    Public Const gstrLoginPage As String = "http://ess.kajima.com.my"

    Public gMyConnection As New System.Data.SqlClient.SqlConnection
    Public gMyCommand As New System.Data.SqlClient.SqlCommand
    Public gMyDataReader As System.Data.SqlClient.SqlDataReader
    Public gMyDataAdapter As System.Data.SqlClient.SqlDataAdapter

    Public gstrConnectionString As String

    Public SmtpMail As New System.Net.Mail.SmtpClient
    Public SmtpMsg As New System.Net.Mail.MailMessage
    Public sysNetworkCredential As New System.Net.NetworkCredential()

    Public gstrServer As String
    Public gstrUID As String
    Public gstrPwd As String
    Public gstrDatabase As String

    Public gstrEmpID As String
    Public gstrEmpName As String
    Public gstrCommandText As String
    Public gstrField() As String
    Public gbRecFound As Boolean

    Public Sub ExecuteCommand(ByVal strCommand As String, Optional ByRef objControl As Object = Nothing, Optional ByVal bSelected As Boolean = False)

        gMyConnection.Open()
        gMyCommand.Connection = gMyConnection

        If Not gMyDataReader Is Nothing Then
            gMyDataReader.Close()
        End If

        gMyCommand.CommandText = strCommand
        gMyDataReader = gMyCommand.ExecuteReader '(System.Data.CommandBehavior.CloseConnection)

        Dim SystemType As System.Type
        Dim strType As String = ""

        If Not objControl Is Nothing Then
            SystemType = objControl.GetType()
            strType = SystemType.Name

            If strType.ToUpper = "COMBOBOX" Or strType.ToUpper = "LISTBOX" Or strType.ToUpper = "CHECKEDLISTBOX" Then
                objControl.Items.Clear()

                If strType.ToUpper = "COMBOBOX" Then
                    objControl.text = ""
                End If
            ElseIf strType.ToUpper = "DATAGRID" Then
                objControl.Rows.Clear()
            End If
        End If

        If gMyDataReader.HasRows Then
            gbRecFound = True

            Dim iFieldCount As Integer = gMyDataReader.FieldCount
            ReDim gstrField(iFieldCount)

            Dim iRowLoop As Integer
            Dim iColLoop As Integer

            If objControl Is Nothing Then
                gMyDataReader.Read()

                For iColLoop = 0 To iFieldCount - 1
                    gstrField(iColLoop) = gMyDataReader.Item(iColLoop)
                Next
            Else
                If strType.ToUpper = "COMBOBOX" Or strType.ToUpper = "LISTBOX" Or strType.ToUpper = "CHECKEDLISTBOX" Then
                    While gMyDataReader.Read
                        objControl.Items.Add(gMyDataReader.Item(0))
                    End While

                    If bSelected Then
                        If gbRecFound Then
                            objControl.SelectedIndex = 0
                        End If
                    End If
                ElseIf strType.ToUpper = "DATAGRID" Then
                    While gMyDataReader.Read()
                        objControl.Rows.Add()
                        objControl.Rows(iRowLoop).Cells(0).Value = iRowLoop + 1

                        For iColLoop = 0 To iFieldCount - 1
                            If objControl.Rows(iRowLoop).Cells(iColLoop + 1).ValueType.FullName = "System.Boolean" Then
                                objControl.Rows(iRowLoop).Cells(iColLoop + 1).Value = CBool(gMyDataReader.Item(iColLoop))
                            Else
                                objControl.Rows(iRowLoop).Cells(iColLoop + 1).Value = gMyDataReader.Item(iColLoop)
                            End If


                            'If gMyDataReader.Item(iColLoop) = "True" Then
                            '    objControl.Rows(iRowLoop).Cells(iColLoop + 1).Value = True
                            'ElseIf gMyDataReader.Item(iColLoop) = "False" Then
                            '    objControl.Rows(iRowLoop).Cells(iColLoop + 1).Value = False
                            'Else
                            '    objControl.Rows(iRowLoop).Cells(iColLoop + 1).Value = gMyDataReader.Item(iColLoop)
                            'End If

                            'objControl.Rows(iRowLoop).Cells(iColLoop + 1).Value = 1
                        Next
                        iRowLoop = iRowLoop + 1
                    End While

                    'SetGridColour(objControl) ' not applicable for web
                    objControl.RowHeadersWidth = 20
                    objControl.ClearSelection()
                    'objControl.Rows(0).Selected = True
                End If
            End If
        Else
            gbRecFound = False

            'If Not objControl Is Nothing Then
            '    MsgBox("No record found !", MsgBoxStyle.Information, "No Record")
            'End If
        End If

        gMyConnection.Close()

    End Sub

    Public Sub SetComboItemSelected(ByVal strValue As String, ByRef cboObject As DropDownList, Optional ByVal strSide As String = "", Optional ByVal iLength As Integer = 0)

        Dim iLoop As Integer

        'Dim strSide As String = ""
        'Dim iLength As Integer

        For iLoop = 0 To cboObject.Items.Count - 1
            If strSide = "" And iLength = 0 Then
                If cboObject.Items.Item(iLoop).Value = strValue Then
                    cboObject.SelectedIndex = iLoop
                    Exit For
                End If
            Else
                If strSide.ToUpper = "L" And iLength > 0 Then
                    If Trim(Left(cboObject.Items.Item(iLoop).Value, iLength)) = strValue Then
                        cboObject.SelectedIndex = iLoop
                        Exit For
                    End If
                ElseIf strSide.ToUpper = "R" And iLength > 0 Then
                    If Trim(Right(cboObject.Items.Item(iLoop).Value, iLength)) = strValue Then
                        cboObject.SelectedIndex = iLoop
                        Exit For
                    End If
                End If
            End If
        Next


    End Sub
    Public Function EncryptedText(ByVal strText As String) As String


        Dim strEncrKey As String = "&%#@?,:*"

        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

        '        byKey() = System.Text.Encoding.UTF8.GetBytes(Left(strEncrKey, 8))

        byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey)

        Dim des As New System.Security.Cryptography.DESCryptoServiceProvider()
        Dim inputByteArray() As Byte = System.Text.Encoding.UTF8.GetBytes(strText)

        Dim ms As New System.IO.MemoryStream()
        Dim cs As New System.Security.Cryptography.CryptoStream(ms, des.CreateEncryptor(byKey, IV), System.Security.Cryptography.CryptoStreamMode.Write)
        cs.Write(inputByteArray, 0, inputByteArray.Length)
        cs.FlushFinalBlock()

        EncryptedText = Convert.ToBase64String(ms.ToArray())
        '        Return Convet.ToBase64String(ms.ToArray())


    End Function
    Public Function DecryptedText(ByVal strText As String) As String

        Dim strDecrKey As String = "&%#@?,:*"

        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim inputByteArray(strText.Length) As Byte

        byKey = System.Text.Encoding.UTF8.GetBytes(strDecrKey)
        Dim des As New System.Security.Cryptography.DESCryptoServiceProvider()
        inputByteArray = Convert.FromBase64String(strText)
        Dim ms As New System.IO.MemoryStream()
        Dim cs As New System.Security.Cryptography.CryptoStream(ms, des.CreateDecryptor(byKey, IV), System.Security.Cryptography.CryptoStreamMode.Write)

        cs.Write(inputByteArray, 0, inputByteArray.Length)
        cs.FlushFinalBlock()
        Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8

        DecryptedText = encoding.GetString(ms.ToArray())

        'Return encoding.GetString(ms.ToArray())

    End Function

    Public Sub SelectListItem(ByRef lstAvailable As ListBox, ByRef lstSelected As ListBox, ByVal strType As String)

        Dim iLoop As Integer

        'lstAvailable.Sorted = True  ' not applicable for web
        'lstSelected.Sorted = True   ' not applicable for web

        If lstAvailable.Items.Count > 0 Then
            If strType = "ONE" Then
                For iLoop = 0 To lstAvailable.Items.Count - 1
                    If lstAvailable.SelectedIndex = iLoop Then
                        lstSelected.Items.Add(lstAvailable.SelectedItem)
                        lstAvailable.Items.Remove(lstAvailable.SelectedItem)
                        iLoop = iLoop - 1
                    End If
                Next
            Else
                For iLoop = 0 To lstAvailable.Items.Count - 1
                    lstSelected.Items.Add(lstAvailable.Items(iLoop))
                Next

                lstAvailable.Items.Clear()
            End If
        End If

    End Sub

    Public Sub DeselectListItem(ByRef lstSelected As ListBox, ByRef lstAvailable As ListBox, ByVal strType As String)

        Dim iLoop As Integer

        'lstAvailable.Sorted = True   ' not applicable for web
        'lstSelected.Sorted = True    ' not applicable for web

        If lstSelected.Items.Count > 0 Then
            If strType = "ONE" Then
                For iLoop = 0 To lstSelected.Items.Count - 1
                    If lstSelected.SelectedIndex = iLoop Then
                        lstAvailable.Items.Add(lstSelected.SelectedItem)
                        lstSelected.Items.Remove(lstSelected.SelectedItem)
                    End If
                Next
            Else
                For iLoop = 0 To lstSelected.Items.Count - 1
                    lstAvailable.Items.Add(lstSelected.Items(iLoop))
                Next

                lstSelected.Items.Clear()
            End If
        End If

    End Sub
    Public Function EncryptedTextParam(ByVal strText As String, ByVal strEncrKey As String) As String


        'Dim strEncrKey As String = "&%#@?,:*"

        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

        '        byKey() = System.Text.Encoding.UTF8.GetBytes(Left(strEncrKey, 8))

        byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey)

        Dim des As New System.Security.Cryptography.DESCryptoServiceProvider()
        Dim inputByteArray() As Byte = System.Text.Encoding.UTF8.GetBytes(strText)

        Dim ms As New System.IO.MemoryStream()
        Dim cs As New System.Security.Cryptography.CryptoStream(ms, des.CreateEncryptor(byKey, IV), System.Security.Cryptography.CryptoStreamMode.Write)
        cs.Write(inputByteArray, 0, inputByteArray.Length)
        cs.FlushFinalBlock()

        EncryptedTextParam = Convert.ToBase64String(ms.ToArray())
        '        Return Convet.ToBase64String(ms.ToArray())


    End Function
    Public Function DecryptedPassedValue(ByVal strText As String) As String

        Dim strDecrKey As String

        Dim strDay, strMonth, strYear As String

        strDay = Now.Day.ToString
        strMonth = Now.Month.ToString
        strYear = Now.Year.ToString

        If strDay.Length = 1 Then strDay = "0" & strDay

        If strMonth.Length = 1 Then strMonth = "0" & strMonth

        strDecrKey = strDay & strMonth & strYear

        strDecrKey = "&%#@?,:*"

        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim inputByteArray(strText.Length) As Byte

        byKey = System.Text.Encoding.UTF8.GetBytes(strDecrKey)
        Dim des As New System.Security.Cryptography.DESCryptoServiceProvider()
        inputByteArray = Convert.FromBase64String(strText)
        Dim ms As New System.IO.MemoryStream()
        Dim cs As New System.Security.Cryptography.CryptoStream(ms, des.CreateDecryptor(byKey, IV), System.Security.Cryptography.CryptoStreamMode.Write)

        cs.Write(inputByteArray, 0, inputByteArray.Length)
        cs.FlushFinalBlock()
        Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8

        DecryptedPassedValue = encoding.GetString(ms.ToArray())

        'Return encoding.GetString(ms.ToArray())

    End Function

    'Public Function NumericOnly(ByVal objControl As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs, Optional ByVal bDecimal As Boolean = False, Optional ByVal iDecimalLength As Short = 0, Optional ByVal bNegative As Boolean = False) As Short ' not applicable for web

    '    'Dim strString As String
    '    Dim KeyAscii As Short

    '    KeyAscii = Asc(e.KeyChar)

    '    'strString = objControl.text.ToString.Substring(0, objControl.SelectionStart) & Chr(KeyAscii) & objControl.text.ToString.Substring(objControl.SelectionStart)

    '    If KeyAscii = 45 Then ' negative
    '        If bNegative Then
    '            If InStr(objControl.text, "-") > 0 Then
    '                KeyAscii = 0
    '            ElseIf objControl.SelectionStart > 0 Then
    '                KeyAscii = 0
    '            End If
    '        Else
    '            KeyAscii = 0
    '        End If
    '    ElseIf KeyAscii = 46 Then ' dot
    '        If bDecimal Then
    '            If InStr(objControl.text, ".") > 0 Then
    '                KeyAscii = 0
    '            ElseIf objControl.SelectionStart = 0 Then
    '                objControl.text = "0." & objControl.text
    '                objControl.SelectionStart = 2
    '                Exit Function
    '            ElseIf objControl.SelectionStart < objControl.TextLength - iDecimalLength Then
    '                KeyAscii = 0
    '            End If
    '        Else
    '            KeyAscii = 0
    '        End If
    '    ElseIf KeyAscii >= 48 And KeyAscii <= 57 Then '  0-9
    '        If objControl.TextLength = objControl.maxlength - iDecimalLength - 1 And InStr(objControl.text, ".") = 0 Then
    '            KeyAscii = 0
    '        ElseIf InStr(objControl.text, ".") > 0 And objControl.SelectionStart > InStr(objControl.text, ".") Then ' check if . have been entered
    '            If objControl.TextLength - InStr(objControl.text, ".") >= iDecimalLength Then ' check valid decimal point
    '                KeyAscii = 0
    '            End If
    '        End If
    '    ElseIf KeyAscii <> 8 Then ' backspace
    '        KeyAscii = 0
    '    End If

    '    NumericOnly = KeyAscii

    'End Function




End Class
