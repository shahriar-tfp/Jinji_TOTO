Imports Microsoft.VisualBasic
Imports System.Data

Public Class RptCommonFunctions
    Public Sub New()
    End Sub
    Public Function GetRptDataSource(displayList As DataTable, groupList As DataTable) As DataTable
        Dim sortBy As String = ""

        For i As Integer = 0 To groupList.Columns.Count - 1
            If sortBy.Length > 0 Then
                sortBy += ", "
            End If
            sortBy += groupList.Columns(i).ColumnName
        Next

        Dim dv As DataView = displayList.DefaultView
        dv.Sort = sortBy

        Return dv.ToTable()
    End Function
End Class