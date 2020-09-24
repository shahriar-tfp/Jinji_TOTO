Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.OleDb
Imports System.data.SqlClient
Imports System.Diagnostics
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Windows.Forms
Imports System.Web.SessionState.HttpSessionState
Imports System.Web.UI.Page


Public Class clsSQL
    Friend ConnectionString As IO.StreamReader
    Private arrConn() As Hashtable
    Private arrtempt As New Hashtable
    Private Const key As Long = 1234567890  'Or any other postive integer
    Private salt As Boolean = False
    Dim strServer As String, strDatabase As String, strUserid As String, strPassword As String, strMailServer As String
    Dim strMailPort As String, strMailUser As String, strMailPassword As String, strUseDefaultCredentials As String, strURLAddress As String
    Private strPath As String, strConnection As String
    Const CopyRight As String = "Copyright @ 1997-2006 Softfac Technology Sdn. Bhd. All Rights Reserved"
    Const SystemNameS As String = "HRLS"
    Const SystemNameL As String = "HRSA"
    Const SystemLanguage As String = "Eng"
    Public SqlConnectionString As String = GetConnectionString()
    Const SQLTimeOut As Integer = 8640000
    Public _Err As String = ""

    Enum SQLInfo
        _Server
        _Database
        _UserID
        _Password
        _MailServer
        _MailPort
        _MailUser
        _MailPass
        _MailUseDefaultCredentials
        _URLAddress
    End Enum
    Public Function getSystemNameS() As String
        Return SystemNameS
    End Function
    Public Function getSystemNameL() As String
        Return SystemNameL
    End Function
    Public Function getSqlConnectionString() As String
        Return SqlConnectionString
    End Function
    Public Function getSystemLanguage() As String
        Return SystemLanguage
    End Function
    Public Sub ExecuteSQLNonQuery(ByVal ssql As String, ByVal strCompanyID As String, ByVal strUserID As String)
        Dim MyConnection As New SqlConnection
        Dim MyCommand As New SqlCommand

        Try
            If SqlConnectionString = Nothing Then
                SqlConnectionString = GetConnectionString()
            Else
                MyConnection = New SqlConnection(SqlConnectionString)
            End If

            MyCommand.Connection = MyConnection
            MyConnection.Open()
            MyCommand.CommandTimeout = SQLTimeOut

            MyCommand.CommandText = "Select Company_Profile_Code='" & strCompanyID & "',User_Profile_Code='" & strUserID & "' Into #Audit"
            MyCommand.ExecuteNonQuery()

            MyCommand.CommandText = ssql
            MyCommand.ExecuteNonQuery()

        Catch ex As Exception
            'Session("Error") = ex.Message.ToString
        Finally
            If MyConnection.State = ConnectionState.Open Then
                MyConnection.Close()
            End If
        End Try
    End Sub
    Public Function ExecuteSQL(ByVal ssql As String, Optional ByVal strCompanyID As String = "", Optional ByVal strUserID As String = "") As DataSet
        Dim MyConnection As New SqlConnection
        Dim MyDataAdapter As New SqlDataAdapter
        Dim MyCommand As New SqlCommand
        Dim MyDS As New DataSet

        Try
            If SqlConnectionString = Nothing Then
                SqlConnectionString = GetConnectionString()
            Else
                MyConnection = New SqlConnection(SqlConnectionString)
            End If


            MyCommand.Connection = MyConnection
            MyConnection.Open()
            MyCommand.CommandTimeout = SQLTimeOut

            'MyCommand.CommandText = "Select Company_Profile_Code='" & strCompanyID & "',User_Profile_Code='" & strUserID & "' Into #Audit"
            'MyCommand.ExecuteNonQuery()

            MyCommand.CommandText = "Select Company_Profile_Code='" & strCompanyID & "',User_Profile_Code='" & strUserID & "' Into #Audit;" + ssql
            MyDataAdapter = New SqlDataAdapter(MyCommand)
            MyDataAdapter.Fill(MyDS)
            Return MyDS
        Catch ex As Exception
            'Session("Error") = ex.Message.ToString
            'MessageBox.Show(ex.Message.ToString)
            _Err = ex.Message.ToString
            Return Nothing
        Finally
            If MyConnection.State = ConnectionState.Open Then
                MyConnection.Close()
            End If
        End Try
    End Function
    'DanielSong Added this function for integrating Reports project. 2008-08-18
    Function RetrieveSQLDataTable(ByVal ssql As String) As DataTable
        Dim MyConnection As New SqlConnection
        Dim MyDataAdapter As New SqlDataAdapter
        Dim MyCommand As New SqlCommand
        Dim MyDT As New DataTable

        Try
            If SqlConnectionString = Nothing Then
                SqlConnectionString = GetConnectionString()
            Else
                MyConnection = New SqlConnection(SqlConnectionString)
            End If


            MyCommand.Connection = MyConnection
            MyConnection.Open()
            MyCommand.CommandTimeout = SQLTimeOut

            MyCommand.CommandText = ssql
            MyDataAdapter = New SqlDataAdapter(MyCommand)
            MyDataAdapter.Fill(MyDT)
            Return MyDT
        Catch ex As Exception
            'Session("Error") = ex.Message.ToString
            _Err = ex.Message.ToString
            Return Nothing
        Finally
            If MyConnection.State = ConnectionState.Open Then
                MyConnection.Close()
            End If
        End Try
    End Function
    'DanielSong Added this function for integrating Reports project. 2008-08-18
    Public Function GetProviderString() As String
        Dim ProviderString As String = ""
        Call GetProjectPath()

        If File.Exists(strPath & "Setup\Setup.ini") Then
            Dim ReadIniFile As New clsReadFile(strPath & "\Setup\Setup.ini")
            strServer = ReadIniFile.GetString("Setup", "Server", "(none)")
            strDatabase = ReadIniFile.GetString("Setup", "Database", "(none)")
            strUserid = ReadIniFile.GetString("Setup", "UID", "(none)")
            strPassword = ReadIniFile.GetString("Setup", "PWD", "(none)")
        Else

        End If

        strUserid = EncodeDecode("DECODE", strUserid)
        strPassword = EncodeDecode("DECODE", strPassword)

        ProviderString = "Provider=SQLOLEDB.1;Persist Security Info=True;Data Source=" & strServer & ";" & _
                              "Initial Catalog=" & strDatabase & ";" & _
                              "User ID=" & strUserid & ";" & _
                              "Password=" & strPassword

        Return ProviderString
    End Function
    Public Function ExecuteSQLByReturnRow(ByVal ssql As String) As DataRow
        Dim MyConnection As New SqlConnection
        Dim MyDataAdapter As New SqlDataAdapter
        Dim MyCommand As New SqlCommand
        Dim MyDT As New DataTable
        Dim myDR As DataRow
        Try
            If SqlConnectionString = Nothing Then
                SqlConnectionString = GetConnectionString()
            Else
                MyConnection = New SqlConnection(SqlConnectionString)
            End If

            MyCommand.CommandText = ssql
            MyCommand.Connection = MyConnection
            MyConnection.Open()
            MyDataAdapter = New SqlDataAdapter(MyCommand)
            MyDataAdapter.Fill(MyDT)
            myDR = MyDT.Rows(0)
            Return myDR
        Catch ex As Exception
            'Session("Error") = ex.Message.ToString
            Return Nothing
        Finally
            If MyConnection.State = ConnectionState.Open Then
                MyConnection.Close()
            End If
        End Try
    End Function
    ' --- DanielSong 2008-07-03 Process_Batch_Payroll ---
    Public Function AddBatchQuery(ByVal query As StringBuilder, ByVal EmpID As String, ByVal count As Integer, ByVal batchdate As String, ByVal Company As String, ByVal strYear As String, ByVal strMonth As String, ByVal strCycle As String) As String
        Dim MyConnection As New SqlConnection
        Dim myCommand As New SqlCommand()
        Dim newSqlParam As New SqlParameter()

        Try
            If SqlConnectionString = Nothing Then
                SqlConnectionString = GetConnectionString()
            Else
                MyConnection = New SqlConnection(SqlConnectionString)
            End If

            myCommand.Connection = MyConnection
            MyConnection.Open()
            myCommand.CommandTimeout = SQLTimeOut

            newSqlParam.ParameterName = "@query"
            newSqlParam.SqlDbType = SqlDbType.Text
            'newSqlParam.Direction = ParameterDirection.Input
            newSqlParam.Value = query.ToString()
            myCommand.Parameters.Add(newSqlParam)

            myCommand.CommandText = "insert into Payroll_Batch_Process(query,querydate,userid,count,Company_Profile_Code,ProcessYear,ProcessMonth,Remark) values(@query,'" + batchdate.ToString() + "','" + EmpID.ToString() + "','" + count.ToString() + "','" + Company.ToString() + "','" + strYear.ToString() + "','" + strMonth.ToString() + "','" + strCycle.ToString() + "')"
            myCommand.ExecuteNonQuery()

            Return "true"
        Catch ex As Exception
            Return "false"
        Finally
            If MyConnection.State = ConnectionState.Open Then
                MyConnection.Close()
            End If
        End Try
    End Function
    Public Sub ExecuteSQLTransaction(ByVal ssql1 As String, ByVal TransactionName As String, ByVal strCompanyID As String, ByVal strUserID As String)
        Dim MyConnection As New SqlConnection
        Dim MyDataAdapter As New SqlDataAdapter
        Dim arr() As String = ssql1.Split(";"), i As Integer

        If SqlConnectionString = Nothing Then
            SqlConnectionString = GetConnectionString()
        Else
            MyConnection = New SqlConnection(SqlConnectionString)
        End If
        MyConnection.Open()
        Dim myCommand As SqlCommand = MyConnection.CreateCommand
        Dim myTransaction As SqlTransaction

        myTransaction = MyConnection.BeginTransaction(TransactionName)
        myCommand.Connection = MyConnection
        myCommand.Transaction = myTransaction
        Try
            myCommand.CommandText = "Select Company_Profile_Code='" & strCompanyID & "',User_Profile_Code='" & strUserID & "' Into #Audit"
            myCommand.ExecuteNonQuery()
            For i = 0 To arr.Length - 1
                myCommand.CommandText = arr(i).ToString.Trim
                myCommand.ExecuteNonQuery()
            Next
            myTransaction.Commit()
            MyConnection.Close()
        Catch ex As Exception
            ' Attempt to roll back the transaction.
            myTransaction.Rollback()
        Finally
            If MyConnection.State <> ConnectionState.Closed Then
                MyConnection.Close()
            End If
        End Try
    End Sub
    Public Function ExecuteSQLTransactionByHashtable(ByVal htb As Hashtable, ByVal TransactionName As String, ByVal strCompanyID As String, ByVal strUserID As String) As Integer
        Dim MyConnection As New SqlConnection
        Dim MyDataAdapter As New SqlDataAdapter
        Dim de As DictionaryEntry

        If SqlConnectionString = Nothing Then
            SqlConnectionString = GetConnectionString()
        Else
            MyConnection = New SqlConnection(SqlConnectionString)
        End If
        MyConnection.Open()
        Dim myCommand As SqlCommand = MyConnection.CreateCommand
        Dim myTransaction As SqlTransaction

        myTransaction = MyConnection.BeginTransaction(TransactionName)
        myCommand.Connection = MyConnection
        myCommand.Transaction = myTransaction
        Try
            myCommand.CommandText = "Select Company_Profile_Code='" & strCompanyID & "',User_Profile_Code='" & strUserID & "' Into #Audit"
            myCommand.ExecuteNonQuery()
            For Each de In htb
                If de.Value <> "" Then
                    myCommand.CommandText = de.Value.ToString.Replace("&amp;", "&")
                    myCommand.ExecuteNonQuery()
                End If
            Next
            myTransaction.Commit()
            MyConnection.Close()
            Return 0
        Catch ex As Exception
            ' Attempt to roll back the transaction.
            myTransaction.Rollback()
            Return -1
        Finally
            If MyConnection.State <> ConnectionState.Closed Then
                MyConnection.Close()
            End If
        End Try
    End Function
    Public Sub ExecuteSQL2Transaction(ByVal ssql1 As String, ByVal ssql2 As String, ByVal TransactionName As String, _
                                        Optional ByVal strCompanyID As String = "", Optional ByVal strUserID As String = "")
        Dim MyConnection As New SqlConnection
        Dim MyDataAdapter As New SqlDataAdapter
        Dim MyDS As New DataSet
        If SqlConnectionString = Nothing Then
            SqlConnectionString = GetConnectionString()
        Else
            MyConnection = New SqlConnection(SqlConnectionString)
        End If
        MyConnection.Open()
        Dim myCommand As SqlCommand = MyConnection.CreateCommand
        Dim myTransaction As SqlTransaction

        myTransaction = MyConnection.BeginTransaction(TransactionName)
        myCommand.Connection = MyConnection
        myCommand.Transaction = myTransaction
        Try
            myCommand.CommandText = "Select Company_Profile_Code='" & strCompanyID & "',User_Profile_Code='" & strUserid & "' Into #Audit;" + ssql1
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql2
            myCommand.ExecuteNonQuery()
            myTransaction.Commit()
            MyConnection.Close()
            Console.WriteLine("Both records are written to database.")

        Catch ex As Exception
            Console.WriteLine("Exception Type: {0}", ex.GetType())
            Console.WriteLine("  Message: {0}", ex.Message)

            ' Attempt to roll back the transaction.
            Try
                myTransaction.Rollback()

            Catch ex2 As Exception
                ' This catch block will handle any errors that may have occurred
                ' on the server that would cause the rollback to fail, such as
                ' a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                Console.WriteLine("  Message: {0}", ex2.Message)
            End Try
        End Try
    End Sub
    Public Sub ExecuteSQL3Transaction(ByVal ssql1 As String, ByVal ssql2 As String, ByVal ssql3 As String, ByVal TransactionName As String, _
                                        Optional ByVal strCompanyID As String = "", Optional ByVal strUserID As String = "")
        Dim MyConnection As New SqlConnection
        Dim MyDataAdapter As New SqlDataAdapter
        Dim MyDS As New DataSet
        If SqlConnectionString = Nothing Then
            SqlConnectionString = GetConnectionString()
        Else
            MyConnection = New SqlConnection(SqlConnectionString)
        End If
        MyConnection.Open()
        Dim myCommand As SqlCommand = MyConnection.CreateCommand
        Dim myTransaction As SqlTransaction

        myTransaction = MyConnection.BeginTransaction(TransactionName)
        myCommand.Connection = MyConnection
        myCommand.Transaction = myTransaction
        Try
            myCommand.CommandText = "Select Company_Profile_Code='" & strCompanyID & "',User_Profile_Code='" & strUserid & "' Into #Audit;" + ssql1
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql2
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql3
            myCommand.ExecuteNonQuery()
            myTransaction.Commit()
            MyConnection.Close()
            Console.WriteLine("All records are written to database.")

        Catch ex As Exception
            Console.WriteLine("Exception Type: {0}", ex.GetType())
            Console.WriteLine("  Message: {0}", ex.Message)

            ' Attempt to roll back the transaction.
            Try
                myTransaction.Rollback()

            Catch ex2 As Exception
                ' This catch block will handle any errors that may have occurred
                ' on the server that would cause the rollback to fail, such as
                ' a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                Console.WriteLine("  Message: {0}", ex2.Message)
            End Try
        End Try
    End Sub
    Public Sub ExecuteSQL4Transaction(ByVal ssql1 As String, ByVal ssql2 As String, ByVal ssql3 As String, ByVal ssql4 As String, ByVal TransactionName As String, _
                                        Optional ByVal strCompanyID As String = "", Optional ByVal strUserID As String = "")
        Dim MyConnection As New SqlConnection
        Dim MyDataAdapter As New SqlDataAdapter
        Dim MyDS As New DataSet
        If SqlConnectionString = Nothing Then
            SqlConnectionString = GetConnectionString()
        Else
            MyConnection = New SqlConnection(SqlConnectionString)
        End If
        MyConnection.Open()
        Dim myCommand As SqlCommand = MyConnection.CreateCommand
        Dim myTransaction As SqlTransaction

        myTransaction = MyConnection.BeginTransaction(TransactionName)
        myCommand.Connection = MyConnection
        myCommand.Transaction = myTransaction
        Try
            myCommand.CommandText = "Select Company_Profile_Code='" & strCompanyID & "',User_Profile_Code='" & strUserid & "' Into #Audit;" + ssql1
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql2
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql3
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql4
            myCommand.ExecuteNonQuery()
            myTransaction.Commit()
            MyConnection.Close()
            Console.WriteLine("All records are written to database.")

        Catch ex As Exception
            Console.WriteLine("Exception Type: {0}", ex.GetType())
            Console.WriteLine("  Message: {0}", ex.Message)

            ' Attempt to roll back the transaction.
            Try
                myTransaction.Rollback()

            Catch ex2 As Exception
                ' This catch block will handle any errors that may have occurred
                ' on the server that would cause the rollback to fail, such as
                ' a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                Console.WriteLine("  Message: {0}", ex2.Message)
            End Try
        End Try
    End Sub
    Public Sub ExecuteSQL5Transaction(ByVal ssql1 As String, ByVal ssql2 As String, ByVal ssql3 As String, ByVal ssql4 As String, ByVal ssql5 As String, ByVal TransactionName As String, _
                                        Optional ByVal strCompanyID As String = "", Optional ByVal strUserID As String = "")
        Dim MyConnection As New SqlConnection
        Dim MyDataAdapter As New SqlDataAdapter
        Dim MyDS As New DataSet
        If SqlConnectionString = Nothing Then
            SqlConnectionString = GetConnectionString()
        Else
            MyConnection = New SqlConnection(SqlConnectionString)
        End If
        MyConnection.Open()
        Dim myCommand As SqlCommand = MyConnection.CreateCommand
        Dim myTransaction As SqlTransaction

        myTransaction = MyConnection.BeginTransaction(TransactionName)
        myCommand.Connection = MyConnection
        myCommand.Transaction = myTransaction
        Try
            myCommand.CommandText = "Select Company_Profile_Code='" & strCompanyID & "',User_Profile_Code='" & strUserid & "' Into #Audit;" + ssql1
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql2
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql3
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql4
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql5
            myCommand.ExecuteNonQuery()
            myTransaction.Commit()
            MyConnection.Close()
            Console.WriteLine("All records are written to database.")

        Catch ex As Exception
            Console.WriteLine("Exception Type: {0}", ex.GetType())
            Console.WriteLine("  Message: {0}", ex.Message)

            ' Attempt to roll back the transaction.
            Try
                myTransaction.Rollback()

            Catch ex2 As Exception
                ' This catch block will handle any errors that may have occurred
                ' on the server that would cause the rollback to fail, such as
                ' a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                Console.WriteLine("  Message: {0}", ex2.Message)
            End Try
        End Try
    End Sub
    Public Sub ExecuteSQL7Transaction(ByVal ssql1 As String, ByVal ssql2 As String, ByVal ssql3 As String, ByVal ssql4 As String, ByVal ssql5 As String, ByVal ssql6 As String, ByVal ssql7 As String, ByVal TransactionName As String, _
                                        Optional ByVal strCompanyID As String = "", Optional ByVal strUserID As String = "")
        Dim MyConnection As New SqlConnection
        Dim MyDataAdapter As New SqlDataAdapter
        Dim MyDS As New DataSet
        If SqlConnectionString = Nothing Then
            SqlConnectionString = GetConnectionString()
        Else
            MyConnection = New SqlConnection(SqlConnectionString)
        End If
        MyConnection.Open()
        Dim myCommand As SqlCommand = MyConnection.CreateCommand
        Dim myTransaction As SqlTransaction

        myTransaction = MyConnection.BeginTransaction(TransactionName)
        myCommand.Connection = MyConnection
        myCommand.Transaction = myTransaction
        Try
            myCommand.CommandText = "Select Company_Profile_Code='" & strCompanyID & "',User_Profile_Code='" & strUserid & "' Into #Audit;" + ssql1
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql2
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql3
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql4
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql5
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql6
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql7
            myCommand.ExecuteNonQuery()
            myTransaction.Commit()
            MyConnection.Close()
        Catch ex As Exception
            Console.WriteLine("Exception Type: {0}", ex.GetType())
            Console.WriteLine("  Message: {0}", ex.Message)

            ' Attempt to roll back the transaction.
            Try
                myTransaction.Rollback()

            Catch ex2 As Exception
                ' This catch block will handle any errors that may have occurred
                ' on the server that would cause the rollback to fail, such as
                ' a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                Console.WriteLine("  Message: {0}", ex2.Message)
            End Try
        End Try
    End Sub
    Public Sub ExecuteSQL8Transaction(ByVal ssql1 As String, ByVal ssql2 As String, ByVal ssql3 As String, _
                                        ByVal ssql4 As String, ByVal ssql5 As String, ByVal ssql6 As String, _
                                        ByVal ssql7 As String, ByVal ssql8 As String, ByVal TransactionName As String, _
                                        Optional ByVal strCompanyID As String = "", Optional ByVal strUserID As String = "")
        Dim MyConnection As New SqlConnection
        Dim MyDataAdapter As New SqlDataAdapter
        Dim MyDS As New DataSet
        If SqlConnectionString = Nothing Then
            SqlConnectionString = GetConnectionString()
        Else
            MyConnection = New SqlConnection(SqlConnectionString)
        End If
        MyConnection.Open()
        Dim myCommand As SqlCommand = MyConnection.CreateCommand
        Dim myTransaction As SqlTransaction

        myTransaction = MyConnection.BeginTransaction(TransactionName)
        myCommand.Connection = MyConnection
        myCommand.Transaction = myTransaction
        Try
            myCommand.CommandText = "Select Company_Profile_Code='" & strCompanyID & "',User_Profile_Code='" & strUserID & "' Into #Audit;" + ssql1
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql2
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql3
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql4
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql5
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql6
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql7
            myCommand.ExecuteNonQuery()
            myCommand.CommandText = ssql8
            myCommand.ExecuteNonQuery()
            myTransaction.Commit()
            MyConnection.Close()
        Catch ex As Exception
            Console.WriteLine("Exception Type: {0}", ex.GetType())
            Console.WriteLine("  Message: {0}", ex.Message)

            ' Attempt to roll back the transaction.
            Try
                myTransaction.Rollback()

            Catch ex2 As Exception
                ' This catch block will handle any errors that may have occurred
                ' on the server that would cause the rollback to fail, such as
                ' a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType())
                Console.WriteLine("  Message: {0}", ex2.Message)
            End Try
        End Try
    End Sub
    Public Function ConnectSQLServer() As Boolean
        Dim MyConnection As New SqlConnection
        Try
            MyConnection.ConnectionString = SqlConnectionString
            MyConnection.Open()
            MyConnection.Close()
            Return True
        Catch ex As Exception
            Return False
        Finally
            If MyConnection.State <> ConnectionState.Closed Then
                MyConnection.Close()
            End If
        End Try
    End Function
    Public Function GetConnectionStringDetails(ByVal enmSQLInfo As SQLInfo) As String
        Dim ReadIniFile As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "\Setup\Setup.ini")
        strServer = ReadIniFile.GetString("Setup", "Server", "(none)")
        strDatabase = ReadIniFile.GetString("Setup", "Database", "(none)")
        strUserid = ReadIniFile.GetString("Setup", "UID", "(none)")
        strPassword = ReadIniFile.GetString("Setup", "PWD", "(none)")
        strURLAddress = ReadIniFile.GetString("Mail", "URL", "(none)")
        strMailServer = ReadIniFile.GetString("Mail", "ExcServer", "(none)")
        strMailPort = ReadIniFile.GetString("Mail", "AdminPort", "(none)")
        strMailUser = ReadIniFile.GetString("Mail", "AdminEmail", "(none)")
        strMailPassword = ReadIniFile.GetString("Mail", "AdminPass", "(none)")
        strUseDefaultCredentials = ReadIniFile.GetString("Mail", "seDefaultCredentials", "(none)")
        Dim strOutput As String = ""
        Select Case enmSQLInfo
            Case SQLInfo._Server
                strOutput = strServer
            Case SQLInfo._Database
                strOutput = strDatabase
            Case SQLInfo._UserID
                strOutput = strUserid
            Case SQLInfo._Password
                strOutput = strPassword
            Case SQLInfo._MailServer
                strOutput = strMailServer
            Case SQLInfo._MailPort
                strOutput = strMailPort
            Case SQLInfo._MailUser
                strOutput = strMailUser
            Case SQLInfo._MailPass
                strOutput = strMailPassword
            Case SQLInfo._MailUseDefaultCredentials
                strOutput = strUseDefaultCredentials
            Case SQLInfo._URLAddress
                strOutput = strURLAddress
        End Select
        Return strOutput
    End Function
    Public Function GetConnectionString() As String

        Call GetProjectPath()

        If File.Exists(strPath & "Setup\Setup.ini") Then
            Dim ReadIniFile As New clsReadFile(strPath & "\Setup\Setup.ini")
            strServer = ReadIniFile.GetString("Setup", "Server", "(none)")
            strDatabase = ReadIniFile.GetString("Setup", "Database", "(none)")
            strUserid = ReadIniFile.GetString("Setup", "UID", "(none)")
            strPassword = ReadIniFile.GetString("Setup", "PWD", "(none)")
        Else
            'ShowMessage("Setup.ini File Not Found At " & strPath & "\bin")
        End If

        'Decode pass
        'strUserid = EncodeDecode("ENCODE", strUserid)
        'strPassword = EncodeDecode("ENCODE", strPassword)
        'strServer = EncodeDecode("DECODE", strServer)
        'strDatabase = EncodeDecode("DECODE", strDatabase)
        strUserid = EncodeDecode("DECODE", strUserid)
        strPassword = EncodeDecode("DECODE", strPassword)

        SqlConnectionString = "Server=" & strServer & ";" & _
                              "Initial Catalog=" & strDatabase & ";" & _
                              "UID=" & strUserid & ";" & _
                              "PWD=" & strPassword

        If ConnectSQLServer() = True Then
            'ShowMessage("Connected To Database Server!")
            Return SqlConnectionString
        Else
            Return ""
        End If
    End Function
    Public Function GetProjectPath() As String
        strPath = System.AppDomain.CurrentDomain.BaseDirectory()
        strPath = Replace(strPath, "/", "\")
        Return strPath
    End Function
    Public Function EncodeDecode(ByVal Choice As String, ByVal Expression As String) As String
        Dim Answer As String = "", tmpAnswer As String = "", Position As Long = 1, Counter As Long = 0

        If Choice = "ENCODE" Then
            For Counter = 0 To Len(Expression) - 1
                If Mid(Expression, Position, 1) = " " Then
                    tmpAnswer = "("
                ElseIf Mid(Expression, Position, 1) = "!" Then
                    tmpAnswer = ")"
                ElseIf Mid(Expression, Position, 1) = """" Then
                    tmpAnswer = "*"
                ElseIf Mid(Expression, Position, 1) = "#" Then
                    tmpAnswer = "+"
                ElseIf Mid(Expression, Position, 1) = "$" Then
                    tmpAnswer = ","
                ElseIf Mid(Expression, Position, 1) = "%" Then
                    tmpAnswer = "4"
                ElseIf Mid(Expression, Position, 1) = "&" Then
                    tmpAnswer = "["
                ElseIf Mid(Expression, Position, 1) = "'" Then
                    tmpAnswer = "\"
                ElseIf Mid(Expression, Position, 1) = "(" Then
                    tmpAnswer = "]"
                ElseIf Mid(Expression, Position, 1) = ")" Then
                    tmpAnswer = "^"
                ElseIf Mid(Expression, Position, 1) = "*" Then
                    tmpAnswer = "_"
                ElseIf Mid(Expression, Position, 1) = "+" Then
                    tmpAnswer = "@"
                ElseIf Mid(Expression, Position, 1) = "," Then
                    tmpAnswer = "{"
                ElseIf Mid(Expression, Position, 1) = "-" Then
                    tmpAnswer = "|"
                ElseIf Mid(Expression, Position, 1) = "." Then
                    tmpAnswer = "}"
                ElseIf Mid(Expression, Position, 1) = "/" Then
                    tmpAnswer = "~"
                ElseIf Mid(Expression, Position, 1) = "0" Then
                    tmpAnswer = "1"
                ElseIf Mid(Expression, Position, 1) = "1" Then
                    tmpAnswer = "2"
                ElseIf Mid(Expression, Position, 1) = "2" Then
                    tmpAnswer = ";"
                ElseIf Mid(Expression, Position, 1) = "3" Then
                    tmpAnswer = "<"
                ElseIf Mid(Expression, Position, 1) = "4" Then
                    tmpAnswer = "="
                ElseIf Mid(Expression, Position, 1) = "5" Then
                    tmpAnswer = ">"
                ElseIf Mid(Expression, Position, 1) = "6" Then
                    tmpAnswer = "5"
                ElseIf Mid(Expression, Position, 1) = "7" Then
                    tmpAnswer = "6"
                ElseIf Mid(Expression, Position, 1) = "8" Then
                    tmpAnswer = "?"
                ElseIf Mid(Expression, Position, 1) = "9" Then
                    tmpAnswer = " "
                ElseIf Mid(Expression, Position, 1) = ":" Then
                    tmpAnswer = "!"
                ElseIf Mid(Expression, Position, 1) = ";" Then
                    tmpAnswer = """"
                ElseIf Mid(Expression, Position, 1) = "<" Then
                    tmpAnswer = "#"
                ElseIf Mid(Expression, Position, 1) = "=" Then
                    tmpAnswer = "$"
                ElseIf Mid(Expression, Position, 1) = ">" Then
                    tmpAnswer = "%"
                ElseIf Mid(Expression, Position, 1) = "?" Then
                    tmpAnswer = "&"
                ElseIf Mid(Expression, Position, 1) = "@" Then
                    tmpAnswer = "'"
                ElseIf Mid(Expression, Position, 1) = "A" Then
                    tmpAnswer = "j"
                ElseIf Mid(Expression, Position, 1) = "B" Then
                    tmpAnswer = "k"
                ElseIf Mid(Expression, Position, 1) = "C" Then
                    tmpAnswer = "l"
                ElseIf Mid(Expression, Position, 1) = "D" Then
                    tmpAnswer = "m"
                ElseIf Mid(Expression, Position, 1) = "E" Then
                    tmpAnswer = "n"
                ElseIf Mid(Expression, Position, 1) = "F" Then
                    tmpAnswer = "a"
                ElseIf Mid(Expression, Position, 1) = "G" Then
                    tmpAnswer = "b"
                ElseIf Mid(Expression, Position, 1) = "H" Then
                    tmpAnswer = "o"
                ElseIf Mid(Expression, Position, 1) = "I" Then
                    tmpAnswer = "p"
                ElseIf Mid(Expression, Position, 1) = "J" Then
                    tmpAnswer = "q"
                ElseIf Mid(Expression, Position, 1) = "K" Then
                    tmpAnswer = "r"
                ElseIf Mid(Expression, Position, 1) = "L" Then
                    tmpAnswer = "s"
                ElseIf Mid(Expression, Position, 1) = "M" Then
                    tmpAnswer = "t"
                ElseIf Mid(Expression, Position, 1) = "N" Then
                    tmpAnswer = "c"
                ElseIf Mid(Expression, Position, 1) = "O" Then
                    tmpAnswer = "d"
                ElseIf Mid(Expression, Position, 1) = "P" Then
                    tmpAnswer = "e"
                ElseIf Mid(Expression, Position, 1) = "Q" Then
                    tmpAnswer = "f"
                ElseIf Mid(Expression, Position, 1) = "R" Then
                    tmpAnswer = "g"
                ElseIf Mid(Expression, Position, 1) = "S" Then
                    tmpAnswer = "u"
                ElseIf Mid(Expression, Position, 1) = "T" Then
                    tmpAnswer = "v"
                ElseIf Mid(Expression, Position, 1) = "U" Then
                    tmpAnswer = "w"
                ElseIf Mid(Expression, Position, 1) = "V" Then
                    tmpAnswer = "x"
                ElseIf Mid(Expression, Position, 1) = "W" Then
                    tmpAnswer = "y"
                ElseIf Mid(Expression, Position, 1) = "X" Then
                    tmpAnswer = "z"
                ElseIf Mid(Expression, Position, 1) = "Y" Then
                    tmpAnswer = "h"
                ElseIf Mid(Expression, Position, 1) = "Z" Then
                    tmpAnswer = "i"
                ElseIf Mid(Expression, Position, 1) = "a" Then
                    tmpAnswer = "J"
                ElseIf Mid(Expression, Position, 1) = "b" Then
                    tmpAnswer = "K"
                ElseIf Mid(Expression, Position, 1) = "c" Then
                    tmpAnswer = "L"
                ElseIf Mid(Expression, Position, 1) = "d" Then
                    tmpAnswer = "M"
                ElseIf Mid(Expression, Position, 1) = "e" Then
                    tmpAnswer = "N"
                ElseIf Mid(Expression, Position, 1) = "f" Then
                    tmpAnswer = "A"
                ElseIf Mid(Expression, Position, 1) = "g" Then
                    tmpAnswer = "B"
                ElseIf Mid(Expression, Position, 1) = "h" Then
                    tmpAnswer = "O"
                ElseIf Mid(Expression, Position, 1) = "i" Then
                    tmpAnswer = "P"
                ElseIf Mid(Expression, Position, 1) = "j" Then
                    tmpAnswer = "Q"
                ElseIf Mid(Expression, Position, 1) = "k" Then
                    tmpAnswer = "R"
                ElseIf Mid(Expression, Position, 1) = "l" Then
                    tmpAnswer = "S"
                ElseIf Mid(Expression, Position, 1) = "m" Then
                    tmpAnswer = "T"
                ElseIf Mid(Expression, Position, 1) = "n" Then
                    tmpAnswer = "C"
                ElseIf Mid(Expression, Position, 1) = "o" Then
                    tmpAnswer = "D"
                ElseIf Mid(Expression, Position, 1) = "p" Then
                    tmpAnswer = "E"
                ElseIf Mid(Expression, Position, 1) = "q" Then
                    tmpAnswer = "F"
                ElseIf Mid(Expression, Position, 1) = "r" Then
                    tmpAnswer = "G"
                ElseIf Mid(Expression, Position, 1) = "s" Then
                    tmpAnswer = "U"
                ElseIf Mid(Expression, Position, 1) = "t" Then
                    tmpAnswer = "V"
                ElseIf Mid(Expression, Position, 1) = "u" Then
                    tmpAnswer = "W"
                ElseIf Mid(Expression, Position, 1) = "v" Then
                    tmpAnswer = "X"
                ElseIf Mid(Expression, Position, 1) = "w" Then
                    tmpAnswer = "Y"
                ElseIf Mid(Expression, Position, 1) = "x" Then
                    tmpAnswer = "Z"
                ElseIf Mid(Expression, Position, 1) = "y" Then
                    tmpAnswer = "H"
                ElseIf Mid(Expression, Position, 1) = "z" Then
                    tmpAnswer = "I"
                ElseIf Mid(Expression, Position, 1) = "[" Then
                    tmpAnswer = "7"
                ElseIf Mid(Expression, Position, 1) = "\" Then
                    tmpAnswer = "8"
                ElseIf Mid(Expression, Position, 1) = "]" Then
                    tmpAnswer = "9"
                ElseIf Mid(Expression, Position, 1) = "^" Then
                    tmpAnswer = ":"
                ElseIf Mid(Expression, Position, 1) = "_" Then
                    tmpAnswer = "3"
                ElseIf Mid(Expression, Position, 1) = "`" Then
                    tmpAnswer = "-"
                ElseIf Mid(Expression, Position, 1) = "{" Then
                    tmpAnswer = "."
                ElseIf Mid(Expression, Position, 1) = "|" Then
                    tmpAnswer = "/"
                ElseIf Mid(Expression, Position, 1) = "}" Then
                    tmpAnswer = "0"
                ElseIf Mid(Expression, Position, 1) = "~" Then
                    tmpAnswer = "`"
                End If

                Answer = Answer & tmpAnswer
                Position = Position + 1
            Next
        End If

        If Choice = "DECODE" Then
            Answer = ""
            tmpAnswer = ""
            Position = 1
            Counter = 0
            For Counter = 0 To Len(Expression) - 1
                If Mid(Expression, Position, 1) = "(" Then
                    tmpAnswer = " "
                ElseIf Mid(Expression, Position, 1) = ")" Then
                    tmpAnswer = "!"
                ElseIf Mid(Expression, Position, 1) = "*" Then
                    tmpAnswer = """"
                ElseIf Mid(Expression, Position, 1) = "+" Then
                    tmpAnswer = "#"
                ElseIf Mid(Expression, Position, 1) = "," Then
                    tmpAnswer = "$"
                ElseIf Mid(Expression, Position, 1) = "4" Then
                    tmpAnswer = "%"
                ElseIf Mid(Expression, Position, 1) = "[" Then
                    tmpAnswer = "&"
                ElseIf Mid(Expression, Position, 1) = "\" Then
                    tmpAnswer = "'"
                ElseIf Mid(Expression, Position, 1) = "]" Then
                    tmpAnswer = "("
                ElseIf Mid(Expression, Position, 1) = "^" Then
                    tmpAnswer = ")"
                ElseIf Mid(Expression, Position, 1) = "_" Then
                    tmpAnswer = "*"
                ElseIf Mid(Expression, Position, 1) = "@" Then
                    tmpAnswer = "+"
                ElseIf Mid(Expression, Position, 1) = "{" Then
                    tmpAnswer = ","
                ElseIf Mid(Expression, Position, 1) = "|" Then
                    tmpAnswer = "-"
                ElseIf Mid(Expression, Position, 1) = "}" Then
                    tmpAnswer = "."
                ElseIf Mid(Expression, Position, 1) = "~" Then
                    tmpAnswer = "/"
                ElseIf Mid(Expression, Position, 1) = "1" Then
                    tmpAnswer = "0"
                ElseIf Mid(Expression, Position, 1) = "2" Then
                    tmpAnswer = "1"
                ElseIf Mid(Expression, Position, 1) = ";" Then
                    tmpAnswer = "2"
                ElseIf Mid(Expression, Position, 1) = "<" Then
                    tmpAnswer = "3"
                ElseIf Mid(Expression, Position, 1) = "=" Then
                    tmpAnswer = "4"
                ElseIf Mid(Expression, Position, 1) = ">" Then
                    tmpAnswer = "5"
                ElseIf Mid(Expression, Position, 1) = "5" Then
                    tmpAnswer = "6"
                ElseIf Mid(Expression, Position, 1) = "6" Then
                    tmpAnswer = "7"
                ElseIf Mid(Expression, Position, 1) = "?" Then
                    tmpAnswer = "8"
                ElseIf Mid(Expression, Position, 1) = " " Then
                    tmpAnswer = "9"
                ElseIf Mid(Expression, Position, 1) = "!" Then
                    tmpAnswer = ":"
                ElseIf Mid(Expression, Position, 1) = """" Then
                    tmpAnswer = ";"
                ElseIf Mid(Expression, Position, 1) = "#" Then
                    tmpAnswer = "<"
                ElseIf Mid(Expression, Position, 1) = "$" Then
                    tmpAnswer = "="
                ElseIf Mid(Expression, Position, 1) = "%" Then
                    tmpAnswer = ">"
                ElseIf Mid(Expression, Position, 1) = "&" Then
                    tmpAnswer = "?"
                ElseIf Mid(Expression, Position, 1) = "'" Then
                    tmpAnswer = "@"
                ElseIf Mid(Expression, Position, 1) = "j" Then
                    tmpAnswer = "A"
                ElseIf Mid(Expression, Position, 1) = "k" Then
                    tmpAnswer = "B"
                ElseIf Mid(Expression, Position, 1) = "l" Then
                    tmpAnswer = "C"
                ElseIf Mid(Expression, Position, 1) = "m" Then
                    tmpAnswer = "D"
                ElseIf Mid(Expression, Position, 1) = "n" Then
                    tmpAnswer = "E"
                ElseIf Mid(Expression, Position, 1) = "a" Then
                    tmpAnswer = "F"
                ElseIf Mid(Expression, Position, 1) = "b" Then
                    tmpAnswer = "G"
                ElseIf Mid(Expression, Position, 1) = "o" Then
                    tmpAnswer = "H"
                ElseIf Mid(Expression, Position, 1) = "p" Then
                    tmpAnswer = "I"
                ElseIf Mid(Expression, Position, 1) = "q" Then
                    tmpAnswer = "K"
                ElseIf Mid(Expression, Position, 1) = "r" Then
                    tmpAnswer = "K"
                ElseIf Mid(Expression, Position, 1) = "s" Then
                    tmpAnswer = "L"
                ElseIf Mid(Expression, Position, 1) = "t" Then
                    tmpAnswer = "M"
                ElseIf Mid(Expression, Position, 1) = "c" Then
                    tmpAnswer = "N"
                ElseIf Mid(Expression, Position, 1) = "d" Then
                    tmpAnswer = "O"
                ElseIf Mid(Expression, Position, 1) = "e" Then
                    tmpAnswer = "P"
                ElseIf Mid(Expression, Position, 1) = "f" Then
                    tmpAnswer = "Q"
                ElseIf Mid(Expression, Position, 1) = "g" Then
                    tmpAnswer = "R"
                ElseIf Mid(Expression, Position, 1) = "u" Then
                    tmpAnswer = "S"
                ElseIf Mid(Expression, Position, 1) = "v" Then
                    tmpAnswer = "T"
                ElseIf Mid(Expression, Position, 1) = "w" Then
                    tmpAnswer = "U"
                ElseIf Mid(Expression, Position, 1) = "x" Then
                    tmpAnswer = "V"
                ElseIf Mid(Expression, Position, 1) = "y" Then
                    tmpAnswer = "W"
                ElseIf Mid(Expression, Position, 1) = "z" Then
                    tmpAnswer = "X"
                ElseIf Mid(Expression, Position, 1) = "h" Then
                    tmpAnswer = "Y"
                ElseIf Mid(Expression, Position, 1) = "i" Then
                    tmpAnswer = "Z"
                ElseIf Mid(Expression, Position, 1) = "J" Then
                    tmpAnswer = "a"
                ElseIf Mid(Expression, Position, 1) = "K" Then
                    tmpAnswer = "b"
                ElseIf Mid(Expression, Position, 1) = "L" Then
                    tmpAnswer = "c"
                ElseIf Mid(Expression, Position, 1) = "M" Then
                    tmpAnswer = "d"
                ElseIf Mid(Expression, Position, 1) = "N" Then
                    tmpAnswer = "e"
                ElseIf Mid(Expression, Position, 1) = "A" Then
                    tmpAnswer = "f"
                ElseIf Mid(Expression, Position, 1) = "B" Then
                    tmpAnswer = "g"
                ElseIf Mid(Expression, Position, 1) = "O" Then
                    tmpAnswer = "h"
                ElseIf Mid(Expression, Position, 1) = "P" Then
                    tmpAnswer = "i"
                ElseIf Mid(Expression, Position, 1) = "Q" Then
                    tmpAnswer = "j"
                ElseIf Mid(Expression, Position, 1) = "R" Then
                    tmpAnswer = "k"
                ElseIf Mid(Expression, Position, 1) = "S" Then
                    tmpAnswer = "l"
                ElseIf Mid(Expression, Position, 1) = "T" Then
                    tmpAnswer = "m"
                ElseIf Mid(Expression, Position, 1) = "C" Then
                    tmpAnswer = "n"
                ElseIf Mid(Expression, Position, 1) = "D" Then
                    tmpAnswer = "o"
                ElseIf Mid(Expression, Position, 1) = "E" Then
                    tmpAnswer = "p"
                ElseIf Mid(Expression, Position, 1) = "F" Then
                    tmpAnswer = "q"
                ElseIf Mid(Expression, Position, 1) = "G" Then
                    tmpAnswer = "r"
                ElseIf Mid(Expression, Position, 1) = "U" Then
                    tmpAnswer = "s"
                ElseIf Mid(Expression, Position, 1) = "V" Then
                    tmpAnswer = "t"
                ElseIf Mid(Expression, Position, 1) = "W" Then
                    tmpAnswer = "u"
                ElseIf Mid(Expression, Position, 1) = "X" Then
                    tmpAnswer = "v"
                ElseIf Mid(Expression, Position, 1) = "Y" Then
                    tmpAnswer = "w"
                ElseIf Mid(Expression, Position, 1) = "Z" Then
                    tmpAnswer = "x"
                ElseIf Mid(Expression, Position, 1) = "H" Then
                    tmpAnswer = "y"
                ElseIf Mid(Expression, Position, 1) = "I" Then
                    tmpAnswer = "z"
                ElseIf Mid(Expression, Position, 1) = "7" Then
                    tmpAnswer = "["
                ElseIf Mid(Expression, Position, 1) = "8" Then
                    tmpAnswer = "\"
                ElseIf Mid(Expression, Position, 1) = "9" Then
                    tmpAnswer = "]"
                ElseIf Mid(Expression, Position, 1) = ":" Then
                    tmpAnswer = "^"
                ElseIf Mid(Expression, Position, 1) = "3" Then
                    tmpAnswer = "_"
                ElseIf Mid(Expression, Position, 1) = "-" Then
                    tmpAnswer = "`"
                ElseIf Mid(Expression, Position, 1) = "." Then
                    tmpAnswer = "{"
                ElseIf Mid(Expression, Position, 1) = "/" Then
                    tmpAnswer = "|"
                ElseIf Mid(Expression, Position, 1) = "0" Then
                    tmpAnswer = "}"
                ElseIf Mid(Expression, Position, 1) = "`" Then
                    tmpAnswer = "~"
                End If
                Answer = Answer & tmpAnswer
                Position = Position + 1
            Next
        End If

        EncodeDecode = Answer
    End Function
    Public Shared Sub OpenNewWindow(ByVal Opener As System.Web.UI.WebControls.WebControl, ByVal strURL As String)

        Dim ClientScript As String

        ClientScript = "window.open('" & strURL & "')"

        Opener.Attributes.Add("OnClick", ClientScript)

    End Sub
    Public Function GetErrorMessage() As String
        Return _Err
    End Function
    Public Function GetSPID() As String
        Dim myDS As New DataSet
        myDS = ExecuteSQL("Select @@spid As SPID")
        If Not myDS Is Nothing Then
            Return myDS.Tables(0).Rows(0).Item(0).ToString
        Else
            Return ""
        End If
    End Function

    Public Function fn_DateDiff(ByVal type As String, ByVal date1 As String, ByVal date2 As String) As Integer
        Dim myDS As New DataSet
        myDS = ExecuteSQL("Select dbo.fn_DateDiff('" + type + "','" + date1 + "','" + date2 + "')")
        If Not myDS Is Nothing Then
            Return CInt(myDS.Tables(0).Rows(0).Item(0).ToString)
        Else
            Return 0
        End If
    End Function

    Public Function fn_DateAdd(ByVal type As String, ByVal increment As String, ByVal date1 As String) As Long
        Dim myDS As New DataSet
        Dim sql1 As String
        sql1 = "Select dbo.fn_DateAdd('" + type + "'," + increment + ",'" + date1 + "')"
        myDS = ExecuteSQL("Select dbo.fn_DateAdd('" + type + "'," + increment + ",'" + date1 + "')")
        If Not myDS Is Nothing Then
            Return CLng(myDS.Tables(0).Rows(0).Item(0).ToString)
        Else
            Return 19000101000000
        End If
    End Function
    Public Function GetDataTable(ByVal strFileName As String, ByVal Company As String, ByVal spid As String) As DataTable
        Dim MyDS As New DataSet
        Dim strConn As String = ("Provider=Microsoft.ace.OleDb.12.0; Data Source = " + System.IO.Path.GetDirectoryName(strFileName) + ";Extended Properties = ""Text;HDR=NO;FMT=TabDelimited""")
        Dim myData As New OleDbDataAdapter("SELECT '" + Company + "' as Company,*,'" + spid + "' as SPID FROM [" + System.IO.Path.GetFileName(strFileName) + "]", strConn)
        myData.Fill(MyDS)
        Dim MyConnection As New SqlConnection
        Dim MyDataAdapter As New SqlDataAdapter
        Dim MyUpdData As New DataSet
        Dim bc As SqlBulkCopy

        Try
            If SqlConnectionString = Nothing Then
                SqlConnectionString = GetConnectionString()
            Else
                MyConnection = New SqlConnection(SqlConnectionString)
            End If

            bc = New SqlBulkCopy(MyConnection)
            bc.DestinationTableName = "Employee_Attendance_Clock_Temp_Process"
            bc.ColumnMappings.Add("Company", "Company_Profile_Code")
            bc.ColumnMappings.Add("SPID", "SPID")
            bc.ColumnMappings.Add("F1", "Clock")
            MyConnection.Open()
            bc.WriteToServer(MyDS.Tables(0))
            'Return MyDS
        Catch ex As Exception
            'Session("Error") = ex.Message.ToString
            'MessageBox.Show(ex.Message.ToString)
            _Err = ex.Message.ToString
            Return Nothing
        Finally
            If MyConnection.State = ConnectionState.Open Then
                MyConnection.Close()
            End If
        End Try
        Return MyDS.Tables(0)
    End Function
End Class
