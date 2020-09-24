Imports System
Imports System.Data

Partial Class Main
    Inherits System.Web.UI.Page

    Private WithEvents mySQL As New clsSQL, myDS As New DataSet
    Dim ssql As String
    Dim strPath As String = "Images"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            pageCss11.Href = strPath & "/" & Session("strTheme") & "/style2.css"
            'Load Image
            imgLayout01.Src = strPath & "/" & Session("strTheme") & "/welcome.png"
            imgLayout02.Src = strPath & "/" & Session("strTheme") & "/headerbar_left.gif"
            imgLayout03.Src = strPath & "/" & Session("strTheme") & "/headerbar_right.gif"
            imgLayout04.Src = strPath & "/" & Session("strTheme") & "/headerbar_left.gif"
            imgLayout05.Src = strPath & "/" & Session("strTheme") & "/headerbar_right.gif"
            imgLayout05.Src = strPath & "/" & Session("strTheme") & "/headerbar_right.gif"
            imgLayout06.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
            body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

            PagePreload()
            AdjustTablePos()
        End If

    End Sub

    Private Sub PagePreload()
        ssql = "Exec sp_rpt_DisplayMessage N'" & Session("Company").ToString & "',N'" & Session("EmpID").ToString & _
                 "',N'" & DatePart(DateInterval.Year, Now()) & "',N'" & DatePart(DateInterval.Month, Now()) & "',N'MAIN_MESSAGE'"
        myDS = mySQL.ExecuteSQL(ssql)
        If Not myDS Is Nothing Then
            '--Retrieve Active Employee Only
            If myDS.Tables.Count > 0 Then
                lblTotalEmployee.Text = "Total Employee (" & myDS.Tables(0).Rows(0).Item(0).ToString & ")"
            End If

            'For i As Integer = 1 To myDS.Tables.Count - 1
            '    Dim gv As New GridView
            '    gv.AutoGenerateColumns = True
            '    gv.DataSource = myDS.Tables(i)
            '    gv.DataBind()
            '    pnlInfo.Controls.Add(gv)
            'Next
            If myDS.Tables.Count > 1 Then
                Dim ds1 As New DataTable

                'dc.ColumnName = "Month " & Month(Now()).ToString & "'2007(By Employee)"
                ds1.Columns.Add(New DataColumn("Month " & Month(Now()).ToString & "'" & Year(Now()).ToString & "(By Employee)"))
                For i As Integer = 0 To myDS.Tables(1).Rows.Count - 1
                    Dim dr As DataRow = ds1.NewRow
                    dr(0) = myDS.Tables(1).Rows(i).Item(0).ToString & myDS.Tables(1).Rows(i).Item(1).ToString
                    ds1.Rows.Add(dr)
                    'ds1.AcceptChanges()
                Next
                ds1.AcceptChanges()
                gv1.Width = "200"
                gv1.DataSource = ds1
                gv1.DataBind()
            End If
            If myDS.Tables.Count > 2 Then
                gv2.DataSource = myDS.Tables(2)
                gv2.DataBind()
            End If
            If myDS.Tables.Count > 3 Then
                gv3.DataSource = myDS.Tables(3)
                gv3.DataBind()
            End If
            If myDS.Tables.Count > 4 Then
                gv4.DataSource = myDS.Tables(4)
                gv4.DataBind()
            End If
        End If
    End Sub

    Private Sub AdjustTablePos()

        Dim intA As Integer = 0
        Dim intB As Integer = 0
        Dim pTop As Integer = 50
        Dim pRight As Integer = 0
        Dim pBottom As Integer = 0
        Dim pLeft As Integer = 40
        Dim strTop, strRight, strBottom, strLeft As String

        If Session("ScreenHeight") = "768" Then
            intA = -10
        ElseIf CInt(Session("ScreenHeight")) > 800 Then
            intB = CInt(Session("ScreenHeight")) - 800
        End If

        If CInt(Session("ScreenHeight")) > 1023 Then
            intB = intB * 0.85
        End If

        If CInt(Session("ScreenWidth")) > 1024 Then
            pTop = 60
            pLeft = pLeft + (CInt(Session("ScreenWidth")) - 1024)
            pLeft = pLeft * 0.9
        End If

        imgLayout01.Width = 237 + intA
        imgLayout01.Height = 129 + intA
        imgLayout06.Width = intB
        imgLayout06.Height = intB

        strTop = pTop.ToString & "px"
        strRight = pRight.ToString & "px"
        strBottom = pBottom.ToString & "px"
        strLeft = pLeft.ToString & "px"
        tdLayout01.Style.Add("padding-top", strTop)
        tdLayout01.Style.Add("padding-right", strRight)
        tdLayout01.Style.Add("padding-bottom", strBottom)
        tdLayout01.Style.Add("padding-left", strLeft)

    End Sub


End Class
