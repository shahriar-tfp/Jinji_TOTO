Imports System.Data
Partial Class News
    Inherits System.Web.UI.Page
    Private WithEvents mySQL As New clsSQL, myDS As New DataSet
    Dim ssql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetNews()
        End If
    End Sub

    Sub GetNews()
        'ltrMessage.Text = "<marquee scrollamount='2' scrolldelay='10' direction='up'>" 

        ssql = "Select * From News_Profile Where Company_Profile_Code='" & Session("Company").ToString & "' Order By Sequence_No Asc"
        myDS = mySQL.ExecuteSQL(ssql)
        If Not myDS Is Nothing Then
            For i As Integer = 0 To myDS.Tables(0).Rows.Count - 1

                Dim dr As DataRow = myDS.Tables(0).Rows(i)
                'dr(0)=Company_Profile_Code
                'dr(1)=Message_Title
                'dr(2)=Message_Description
                'dr(3)=Sequence_No
                'dr(4)=Option_Hyperlink
                'dr(5)=Hyperlink_URL
                'dr(6)=Option_Show
                'dr(7)=Effective_Date
                If dr(6) = "YES" Then
                    If dr(4).ToString = "YES" Then
                        ltrMessage.Text &= "<a style='font-size:11px; font-weight:bold; color:Blue; font-family:Arial Unicode MS' target='_blank' href='" & dr(5).ToString & "'>" & _
                                dr(1).ToString & "</a>" & "</br><a style='font-size:11px; font-family:Arial Unicode MS'>" & dr(2).ToString & "</a>"
                    Else
                        ltrMessage.Text &= "<a style='font-size:11px; font-weight:bold; color:Blue; font-family:Arial Unicode MS'>" & _
                                dr(1).ToString & "</a>" & "</br><a style='font-size:11px; font-family:Arial Unicode MS'>" & dr(2).ToString & "</a>"
                    End If
                    If i < myDS.Tables(0).Rows.Count - 1 Then
                        ltrMessage.Text &= "</br></br>"
                    End If
                End If
            Next
        End If
        '        ltrMessage.Text &= "</marquee>"
        'Response.Write("<marquee scrollamount='2' scrolldelay='10' direction='up'><a style='font-weight:bold; color:Blue; font-family:Arial Unicode MS' target='_blank' href='https://mail.softfac.com/owa'>Team Building</a></br><a>Team Building at Bukit Tinggi</a></br></br><a style='font-weight:bold; color:Blue; font-family:Arial Unicode MS'>Upcoming Meeting</a></br><a>Upcoming Meeting with SoftFac Management on 31/07/2007 2:00pm</a></br></br><a style='font-weight:bold; color:Blue; font-family:Arial Unicode MS' target='_blank' href='http://www.yahoo.com'>National Day Celebration</a></br><a>National Day Celebration on 30/08/2007 at RebBox Karaoke (Sunway Pyramid)</a></marquee>")
    End Sub


End Class
