Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO


Partial Class PAGES_DOCUMENT_DISPLAYURL
    Inherits System.Web.UI.Page
#Region "Public Declaration"
    Private WithEvents mySQL As New clsSQL, myDS As New DataSet, myDS2 As New DataSet, mySetting As New clsGlobalSetting
    Private WithEvents myFileReader As New clsReadFile(System.AppDomain.CurrentDomain.BaseDirectory & "App_GlobalResources\Setup\Setup.ini")
    Public ssql As String, i As Integer, j As Integer, SearchByPage As Boolean, RecFound As Boolean, CountRecord As Integer
    Public AllowView As Boolean, AllowInsert As Boolean, AllowUpdate As Boolean, AllowDelete As Boolean, AllowPrint As Boolean
    Public _currentPageNumber As Integer, _totalPage As Double = 1, _totalRecords As Integer
    Public myDE As New DictionaryEntry, myHT As Hashtable
    Public myDataType As New clsGlobalSetting.DataType, SearchByFilter As Boolean = False
    Dim intColumn As Integer
#End Region
#Region "Private Declaration"
    Private WithEvents myDT1 As New DataTable, myDT2 As New DataTable
    Dim myDR1 As DataRow, myDR2 As DataRow, rcPerPage As Integer, autonum As Integer
    Dim ssql2 As String, ssql3 As String, ssql4 As String
    Dim rowPosition As String = "R_", rowPositionM As String = "RM_", panelPosition As String = "Display_"
    Dim buttonPosition1 As String = "B1_", buttonPosition2 As String = "B2_", buttonPosition3 As String = "B3_", labelPosition1 As String = "L1_"
    Dim btnColourDef, btnColourAlt As String
    Dim strPath As String = "../../Images"
    Dim logic As Boolean
#End Region
#Region "Page Setting"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.IsNewSession Then
            Response.Redirect("../Global/SessionTimeOut.aspx")
        End If
        If Not IsPostBack Then
            Response.CacheControl = "no-cache"
            Response.AddHeader("Pragma", "no-cache")
            Response.Expires = -1
            Session("Module") = "DOCUMENT"

            If Session("ScreenWidth") = 0 Then
                Session("ScreenWidth") = "1024"
                Session("GVwidth") = Session("ScreenWidth") - 360
            End If
            If Session("ScreenHeight") = 0 Then
                Session("ScreenHeight") = "768"
                Session("GVheight") = (Session("ScreenHeight") / 2) - 50
            End If

            PagePreload()
            BindGrid()
        End If
    End Sub

    Sub PagePreload()
        btnColourDef = Session("strTheme")
        btnColourAlt = Session("strThemeAlt")
        Session("SearchByFilter") = "False"
        Session("SearchSQL") = ""
        Session("SearchSQL1") = ""
        Session("FilterField") = ""
        Session("FilterCriteria") = ""
        body.Style("background-image") = strPath & "/" & Session("strTheme") & "/background2.jpg"

        'get Page Title
        ssql = "exec sp_sa_GetPageTitle '" & Form.ID & "'"
        myDS = New DataSet
        myDS = mySQL.ExecuteSQL(ssql, Session("Company").ToString, Session("EmpID").ToString)
        If myDS.Tables(0).Rows.Count > 0 Then
            lblTitle.Text = myDS.Tables(0).Rows(0).Item(0)
            lblTitle.CssClass = "wordstyle4"
        End If
        myDS = Nothing

        SearchByPage = False
        _currentPageNumber = 1

        myDS = New DataSet
        myDS = mySetting.GetPageFieldSetting(Session("Company"), Form.ID, Session("EmpID"))
        If myDS.Tables.Count > 1 Then
            myDT1 = New DataTable
            myDT1 = myDS.Tables(0)
            myDT2 = New DataTable
            myDT2 = myDS.Tables(1)

            If myDT1.Rows.Count > 0 Then
                myDR1 = myDT1.Rows(0)
                Page.Title = myDR1(1)
                If myDR1(3) = "YES" Then
                    AllowView = True
                    Session("View") = "TRUE"
                Else
                    AllowView = False
                    Session("View") = "FALSE"
                    pnlaction.Visible = False
                    pnlGridview.Visible = False
                    pnlMain.Visible = False
                    pnlPrevNext.Visible = False
                    pnlresult.Visible = False
                    Exit Sub
                End If

                If myDR1(4) = "YES" Then
                    AllowInsert = True
                    Session("Insert") = "TRUE"
                Else
                    AllowInsert = False
                    Session("Insert") = "FALSE"
                End If
                If myDR1(5) = "YES" Then
                    AllowUpdate = True
                    Session("Update") = "TRUE"
                Else
                    AllowUpdate = False
                    Session("Update") = "FALSE"
                End If
                If myDR1(6) = "YES" Then
                    AllowDelete = True
                    Session("Delete") = "TRUE"
                Else
                    AllowDelete = False
                    Session("Delete") = "FALSE"
                End If
                If myDR1(7) = "YES" Then
                    AllowPrint = True
                    Session("Print") = "TRUE"
                Else
                    AllowPrint = False
                    Session("Print") = "FALSE"
                End If

            End If

            myDS = Nothing
            myDT1 = Nothing
            myDT2 = Nothing
            mySetting.GetGridViewSetting(Session("Company").ToString, Session("Module").ToString, "No_Of_Record", Form.ID, "GetGridViewRecord", myGridView)
            mySetting.GetGridViewSetting(Session("Company").ToString, Session("Module").ToString, "No_Of_Record", Form.ID, "GetGridViewRecord", gvHistory)
        End If
    End Sub

    Sub BindGrid()
        Try
            ssql = "Exec sp_dc_selURL """ & Session("Company").ToString & """"

            myDS = mySQL.ExecuteSQL(ssql)
            If Not myDS Is Nothing Then
                If myDS.Tables.Count > 1 And CInt(myDS.Tables(0).Rows(0).Item(0)) > 0 Then
                    myGridView.DataSource = myDS.Tables(1)
                    myGridView.DataBind()

                    gvHistory.DataSource = myDS.Tables(1)
                    gvHistory.DataBind()
                    myDS2 = Nothing

                    pnlMain.Visible = True
                    pnlPrevNext.Enabled = True

                    If Not IsPostBack Then
                        _totalRecords = myDS.Tables(0).Rows(0).Item(0)
                        _totalPage = _totalRecords / myGridView.PageSize
                    Else
                        _totalRecords = myDS.Tables(0).Rows(0).Item(0)
                        _totalPage = _totalRecords / myGridView.PageSize
                    End If

                Else
                    If Not myDS Is Nothing Then
                        If myDS.Tables.Count >= 2 Then
                            myGridView.DataSource = myDS.Tables(1)
                            myGridView.DataBind()
                        End If
                    End If

                    pnlPrevNext.Enabled = False
                End If
            End If
        Catch ex As Exception
            'Error Handling
            Response.Write("Error : " & ex.ToString)
        End Try
    End Sub

    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim filePath As String = CType(sender, LinkButton).CommandArgument
        Dim fileName As String = CType(sender, LinkButton).Text
        Response.ContentType = ContentType
        Response.AppendHeader("Content-Disposition", ("attachment; filename=" & fileName & Path.GetExtension(filePath)))
        Response.WriteFile(Server.MapPath("~/PAGES/DOCUMENT/Upload/") & filePath)
        Response.End()
    End Sub
#End Region

End Class