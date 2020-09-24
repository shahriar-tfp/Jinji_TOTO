'*****************************************************************************************************************
'Class name : clsDataAccess
'Defination : This class is the most important class as it does all the updating,Inserting,deleting,Retrieving
'part in the database.The methods in this class is called by all the other methods in other classes.Connection
'with the database is set up here only.We have used Granth SQL database.We added this class after our analysis
'phase.As you will go through the code we have not used any attributes in any class the only thing we have used is
'a query and nothing else.
'Date Added : 11/11/05
'Please keep the comments if you distribute
'Author : Quartz (Rajesh Lal - connectrajesh@hotmail.com)
'*****************************************************************************************************************

Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Public Class clsDataAccess
    ' Class defination
    Public Sub New()
    End Sub
    Private mycon As New SqlConnection()

    Public Function openConnection() As Boolean
        ' Opens database connection with Granth in SQL SERVER
        Dim cs As New clsSQL
        mycon.ConnectionString = cs.GetConnectionString()
        If mycon.State = ConnectionState.Closed Then
            mycon.Open()
        End If

        Return True
    End Function
    Public Sub closeConnection()
        ' Closes database connection with Granth in SQL SERVER

        mycon.Close()
        mycon = Nothing
    End Sub
    Public Function getData(ByVal query As String) As SqlDataReader
        ' Getdata from the table required(given in query)in datareader
        Dim sqlCommand As New SqlCommand()
        sqlCommand.CommandText = query
        sqlCommand.Connection = mycon
        Dim myr As SqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        Return myr

    End Function
    Public Function getFolderName() As String
        Dim sqlCommand As New SqlCommand()
        sqlCommand.CommandText = "sp_forum_createtempfolder"
        sqlCommand.Connection = mycon
        Dim myr As SqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        If myr.HasRows Then
            While myr.Read()
                Return myr("num").ToString()
            End While
        End If
        Return "0"
    End Function
    Public Function getForumData(ByVal ArticleId As String, ByVal type As String) As SqlDataReader
        ' Getdata from the table required(given in query)in datareader
        Dim sqlCommand As New SqlCommand()
        sqlCommand.CommandType = CommandType.StoredProcedure
        sqlCommand.CommandText = "sp_forum_showhierarchy"
        Dim newSqlParam As New SqlParameter()
        newSqlParam.ParameterName = "@ArticleId"
        newSqlParam.SqlDbType = SqlDbType.VarChar
        newSqlParam.Direction = ParameterDirection.Input
        newSqlParam.Value = ArticleId
        sqlCommand.Parameters.Add(newSqlParam)

        Dim newSqlParam2 As New SqlParameter()
        newSqlParam2.ParameterName = "@Root"
        newSqlParam2.SqlDbType = SqlDbType.Int
        newSqlParam2.Direction = ParameterDirection.Input
        newSqlParam2.Value = 0
        sqlCommand.Parameters.Add(newSqlParam2)

        Dim newSqlParam3 As New SqlParameter()
        newSqlParam3.ParameterName = "@type"
        newSqlParam3.SqlDbType = SqlDbType.VarChar
        newSqlParam3.Direction = ParameterDirection.Input
        newSqlParam3.Value = type
        sqlCommand.Parameters.Add(newSqlParam3)
        sqlCommand.Connection = mycon



        Dim myr As SqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        Return myr

    End Function
    Public Function DeleteForumData(ByVal ArticleId As String, ByVal root As Integer) As Boolean
        ' Getdata from the table required(given in query)in datareader
        Dim sqlCommand As New SqlCommand()
        sqlCommand.CommandType = CommandType.StoredProcedure

        sqlCommand.CommandText = "sp_forum_deletehierarchy"
        Dim newSqlParam As New SqlParameter()
        newSqlParam.ParameterName = "@ArticleId"
        newSqlParam.SqlDbType = SqlDbType.VarChar
        newSqlParam.Direction = ParameterDirection.Input
        newSqlParam.Value = ArticleId
        sqlCommand.Parameters.Add(newSqlParam)

        Dim newSqlParam2 As New SqlParameter()
        newSqlParam2.ParameterName = "@Root"
        newSqlParam2.SqlDbType = SqlDbType.Int
        newSqlParam2.Direction = ParameterDirection.Input
        newSqlParam2.Value = root
        sqlCommand.Parameters.Add(newSqlParam2)
        sqlCommand.Connection = mycon

        Dim i As Integer = sqlCommand.ExecuteNonQuery()
        If i = 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function OpenMessage(ByVal id As String, ByVal aid As String) As String
        ' Getdata from the table required(given in query)in datareader
        Dim sqlCommand As New SqlCommand()
        sqlCommand.CommandType = CommandType.StoredProcedure

        sqlCommand.CommandText = "sp_forum_openmessage"
        Dim newSqlParam As New SqlParameter()
        newSqlParam.ParameterName = "@id"
        newSqlParam.SqlDbType = SqlDbType.VarChar
        newSqlParam.Direction = ParameterDirection.Input
        newSqlParam.Value = id
        sqlCommand.Parameters.Add(newSqlParam)

        Dim newSqlParam2 As New SqlParameter()
        newSqlParam2.ParameterName = "@aid"
        newSqlParam2.SqlDbType = SqlDbType.VarChar
        newSqlParam2.Direction = ParameterDirection.Input
        newSqlParam2.Value = aid
        sqlCommand.Parameters.Add(newSqlParam2)
        sqlCommand.Connection = mycon

        Dim i As Integer = sqlCommand.ExecuteNonQuery()
        If i = 0 Then
            Return "true"
        Else
            Return "false"
        End If

    End Function

    Public Function UpdateArticleID(ByVal ArticleId As String, ByVal root As Integer, ByVal type As String) As Boolean
        ' Getdata from the table required(given in query)in datareader
        Dim sqlCommand As New SqlCommand()
        sqlCommand.CommandType = CommandType.StoredProcedure

        sqlCommand.CommandText = "sp_forum_getrootparent"
        Dim newSqlParam As New SqlParameter()
        newSqlParam.ParameterName = "@ArticleId"
        newSqlParam.SqlDbType = SqlDbType.VarChar
        newSqlParam.Direction = ParameterDirection.Input
        newSqlParam.Value = ArticleId
        sqlCommand.Parameters.Add(newSqlParam)

        Dim newSqlParam2 As New SqlParameter()
        newSqlParam2.ParameterName = "@Root"
        newSqlParam2.SqlDbType = SqlDbType.Int
        newSqlParam2.Direction = ParameterDirection.Input
        newSqlParam2.Value = root
        sqlCommand.Parameters.Add(newSqlParam2)

        Dim newSqlParam3 As New SqlParameter()
        newSqlParam3.ParameterName = "@type"
        newSqlParam3.SqlDbType = SqlDbType.VarChar
        newSqlParam3.Direction = ParameterDirection.Input
        newSqlParam3.Value = type.ToString()
        sqlCommand.Parameters.Add(newSqlParam3)
        sqlCommand.Connection = mycon

        sqlCommand.ExecuteNonQuery()

    End Function



End Class
