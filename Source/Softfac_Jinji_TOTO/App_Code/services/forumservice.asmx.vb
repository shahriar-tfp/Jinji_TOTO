Imports System.Web.Services
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient


Namespace Forum


    <WebService(Namespace:="http://tempuri.org/OLAPReport/Service1")> _
    Public Class forumservice
        Inherits System.Web.Services.WebService

#Region " Web Services Designer Generated Code "

        Public Sub New()
            MyBase.New()

            'This call is required by the Web Services Designer.
            InitializeComponent()

            'Add your own initialization code after the InitializeComponent() call

        End Sub

        'Required by the Web Services Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Web Services Designer
        'It can be modified using the Web Services Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            'CODEGEN: This procedure is required by the Web Services Designer
            'Do not modify it using the code editor.
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#End Region

        <WebMethod()> Public Function setValueToOpen(ByVal id As String, ByVal aid As String) As String
            Dim db As New clsDataAccess()
            Dim result As String = ""
            db.openConnection()
            result = db.OpenMessage(id, aid)
            db.closeConnection()
            Return result

        End Function


    End Class

End Namespace
