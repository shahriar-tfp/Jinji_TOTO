Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class clsAutoGenerate
    Dim _Err As String = ""
    Dim strCompany As String = "", strModule As String = "", strTable As String = "", strField As String = ""
#Region "Sub"

#End Region
#Region "Function"
    Sub ReadFileContent(ByVal myArray As ArrayList, ByVal strPath As String)
        If File.Exists(strPath) Then
            Dim objReader As New StreamReader(strPath)
            Dim sLine As String = ""
            Try
                Do
                    sLine = objReader.ReadLine()
                    If Not sLine Is Nothing Then
                        myArray.Add(sLine)
                    End If
                Loop Until sLine Is Nothing
            Catch ex As Exception
                _Err = ex.Message.ToString
            Finally
                objReader.Close()
            End Try
        Else
            _Err = "File not exists."
            Exit Sub
        End If
    End Sub
#End Region
End Class
