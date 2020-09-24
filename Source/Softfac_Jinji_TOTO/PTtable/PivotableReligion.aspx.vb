
Partial Class Pivot_PivotableReligion
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim MySQL As New clsSQL

        Dim strServer, strDatabase, strUID, strPwd As String
        strServer = MySQL.GetConnectionStringDetails(clsSQL.SQLInfo._Server)
        strDatabase = MySQL.GetConnectionStringDetails(clsSQL.SQLInfo._Database)
        strUID = MySQL.GetConnectionStringDetails(clsSQL.SQLInfo._UserID)
        strPwd = MySQL.GetConnectionStringDetails(clsSQL.SQLInfo._Password)

        strUID = MySQL.EncodeDecode("DECODE", strUID)
        strPwd = MySQL.EncodeDecode("DECODE", strPwd)
        Dim str As String
        str = div.InnerHtml()
        str = str.Replace("_pwd_", strPwd)
        str = str.Replace("_uid_", strUID)
        str = str.Replace("_database_", strDatabase)
        str = str.Replace("_server_", strServer)
        Response.Write(str)
    End Sub
End Class
