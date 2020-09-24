Imports System
Imports System.Data
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Net


Partial Class Home
    Inherits System.Web.UI.Page
    Private WithEvents mySQL As New clsSQL, mySetting As New clsGlobalSetting
    Private WithEvents myDS As New DataSet
    Dim ssql As String
    Dim a As Integer, b As Integer, c As Integer, d As Integer, i As Integer, j As Integer, k As Integer, x As Long, y As Long, z As Long
    Dim strPath As String = "Images"

    Dim DS As New DataSet
    Dim imgPath As String = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\"
    Dim imgPath2 As String = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\default\imgright.gif"
    Dim imgPath3 As String = System.AppDomain.CurrentDomain.BaseDirectory() & "Images\Company\default\imgright.jpg"
    Dim m_bmpRepresentation As Bitmap
    Dim imgHeight As Integer = 0
    Dim imgWidth As Integer = 0


    Private Sub WriteTreeNode()
        Dim cntTable As Integer
        myDS = mySQL.ExecuteSQL("Exec sp_sa_WriteTreeNodeWithSecurity '" & Session("Company").ToString & "','" & Session("EmpID").ToString & "'")
        cntTable = myDS.Tables.Count
        If cntTable > 0 Then
            Dim myDT0 As New DataTable
            myDT0 = myDS.Tables(0)
            For x = 0 To myDT0.Rows.Count - 1
                If myDT0.Rows(x).Item(7).ToString = "YES" Then
                    Dim masterNode As New TreeNode(myDT0.Rows(x).Item(1) & " - " & Session("EmpID").ToString)
                    myTV.Nodes.Add(masterNode)
                    mySetting.GetTreeViewSetting(masterNode, myDT0.Rows(x).Item(0), myDT0.Rows(x).Item(3), myDT0.Rows(x).Item(4), myDT0.Rows(x).Item(5), myDT0.Rows(x).Item(1), myDT0.Rows(x).Item(6), Session("Company"), False)
                    If cntTable > 1 Then
                        Dim myDT1 As New DataTable
                        myDT1 = myDS.Tables(1)
                        For y = 0 To myDT1.Rows.Count - 1
                            If myDT1.Rows(y).Item(2) = myDT0.Rows(x).Item(0) Then
                                If myDT1.Rows(y).Item(7).ToString = "YES" Then
                                    Dim childNode1 As New TreeNode(myDT1.Rows(y).Item(1))
                                    masterNode.ChildNodes.Add(childNode1)
                                    mySetting.GetTreeViewSetting(childNode1, myDT1.Rows(y).Item(0), myDT1.Rows(y).Item(3), myDT1.Rows(y).Item(4), myDT1.Rows(y).Item(5), myDT1.Rows(y).Item(1), myDT1.Rows(y).Item(6), Session("Company"))
                                    If cntTable > 2 Then
                                        Dim myDT2 As New DataTable
                                        myDT2 = myDS.Tables(2)
                                        For z = 0 To myDT2.Rows.Count - 1
                                            If myDT2.Rows(z).Item(2) = myDT1.Rows(y).Item(0) Then
                                                If myDT2.Rows(z).Item(7).ToString = "YES" Then
                                                    Dim childNode2 As New TreeNode(myDT2.Rows(z).Item(1))
                                                    childNode1.ChildNodes.Add(childNode2)
                                                    mySetting.GetTreeViewSetting(childNode2, myDT2.Rows(z).Item(0), myDT2.Rows(z).Item(3), myDT2.Rows(z).Item(4), myDT2.Rows(z).Item(5), myDT2.Rows(z).Item(0), myDT2.Rows(z).Item(6), Session("Company"))
                                                    If cntTable > 3 Then
                                                        Dim myDT3 As New DataTable
                                                        myDT3 = myDS.Tables(3)
                                                        For i = 0 To myDT3.Rows.Count - 1
                                                            If myDT3.Rows(i).Item(2) = myDT2.Rows(z).Item(0) Then
                                                                If myDT3.Rows(i).Item(7).ToString = "YES" Then
                                                                    Dim childNode3 As New TreeNode(myDT3.Rows(i).Item(1))
                                                                    childNode2.ChildNodes.Add(childNode3)
                                                                    mySetting.GetTreeViewSetting(childNode3, myDT3.Rows(i).Item(0), myDT3.Rows(i).Item(3), myDT3.Rows(i).Item(4), myDT3.Rows(i).Item(5), myDT3.Rows(i).Item(1), myDT3.Rows(i).Item(6), Session("Company"))
                                                                    If cntTable > 4 Then
                                                                        Dim myDT4 As New DataTable
                                                                        myDT4 = myDS.Tables(4)
                                                                        For j = 0 To myDT4.Rows.Count - 1
                                                                            If myDT4.Rows(j).Item(2) = myDT3.Rows(i).Item(0) Then
                                                                                If myDT4.Rows(j).Item(7).ToString = "YES" Then
                                                                                    Dim childNode4 As New TreeNode(myDT4.Rows(j).Item(1))
                                                                                    childNode3.ChildNodes.Add(childNode4)
                                                                                    mySetting.GetTreeViewSetting(childNode4, myDT4.Rows(j).Item(0), myDT4.Rows(j).Item(3), myDT4.Rows(j).Item(4), myDT4.Rows(j).Item(5), myDT4.Rows(j).Item(1), myDT4.Rows(j).Item(6), Session("Company"))
                                                                                    If cntTable > 5 Then
                                                                                        Dim myDT5 As New DataTable
                                                                                        myDT5 = myDS.Tables(5)
                                                                                        For k = 0 To myDT5.Rows.Count - 1
                                                                                            If myDT5.Rows(k).Item(2) = myDT4.Rows(j).Item(0) Then
                                                                                                If myDT5.Rows(k).Item(7).ToString = "YES" Then
                                                                                                    Dim childNode5 As New TreeNode(myDT5.Rows(k).Item(1))
                                                                                                    childNode4.ChildNodes.Add(childNode5)
                                                                                                    mySetting.GetTreeViewSetting(childNode5, myDT5.Rows(k).Item(0), myDT5.Rows(k).Item(3), myDT5.Rows(k).Item(4), myDT5.Rows(k).Item(5), myDT5.Rows(k).Item(1), myDT5.Rows(k).Item(6), Session("Company"))
                                                                                                    If cntTable > 6 Then
                                                                                                        Dim myDT6 As New DataTable
                                                                                                        myDT6 = myDS.Tables(6)
                                                                                                        For a = 0 To myDT6.Rows.Count - 1
                                                                                                            If myDT6.Rows(a).Item(2) = myDT5.Rows(k).Item(0) Then
                                                                                                                If myDT6.Rows(a).Item(7).ToString = "YES" Then
                                                                                                                    Dim childNode6 As New TreeNode(myDT6.Rows(a).Item(1))
                                                                                                                    childNode5.ChildNodes.Add(childNode6)
                                                                                                                    mySetting.GetTreeViewSetting(childNode6, myDT6.Rows(a).Item(0), myDT6.Rows(a).Item(3), myDT6.Rows(a).Item(4), myDT6.Rows(a).Item(5), myDT6.Rows(a).Item(1), myDT6.Rows(a).Item(6), Session("Company"))
                                                                                                                    If cntTable > 7 Then
                                                                                                                        Dim myDT7 As New DataTable
                                                                                                                        myDT7 = myDS.Tables(7)
                                                                                                                        For b = 0 To myDT7.Rows.Count - 1
                                                                                                                            If myDT7.Rows(b).Item(2) = myDT6.Rows(a).Item(0) Then
                                                                                                                                If myDT7.Rows(b).Item(7).ToString = "YES" Then
                                                                                                                                    Dim childNode7 As New TreeNode(myDT7.Rows(b).Item(1))
                                                                                                                                    childNode6.ChildNodes.Add(childNode7)
                                                                                                                                    mySetting.GetTreeViewSetting(childNode7, myDT7.Rows(b).Item(0), myDT7.Rows(b).Item(3), myDT7.Rows(b).Item(4), myDT7.Rows(b).Item(5), myDT7.Rows(b).Item(1), myDT7.Rows(b).Item(6), Session("Company"))
                                                                                                                                    If cntTable > 8 Then
                                                                                                                                        Dim myDT8 As New DataTable
                                                                                                                                        myDT8 = myDS.Tables(8)
                                                                                                                                        For c = 0 To myDT8.Rows.Count - 1
                                                                                                                                            If myDT8.Rows(c).Item(2) = myDT7.Rows(b).Item(0) Then
                                                                                                                                                If myDT8.Rows(c).Item(7).ToString = "YES" Then
                                                                                                                                                    Dim childNode8 As New TreeNode(myDT8.Rows(c).Item(1))
                                                                                                                                                    childNode7.ChildNodes.Add(childNode8)
                                                                                                                                                    mySetting.GetTreeViewSetting(childNode8, myDT8.Rows(c).Item(0), myDT8.Rows(c).Item(3), myDT8.Rows(c).Item(4), myDT8.Rows(c).Item(5), myDT8.Rows(c).Item(1), myDT8.Rows(c).Item(6), Session("Company"))
                                                                                                                                                    If cntTable > 9 Then
                                                                                                                                                        Dim myDT9 As New DataTable
                                                                                                                                                        myDT9 = myDS.Tables(9)
                                                                                                                                                        For d = 0 To myDT9.Rows.Count - 1
                                                                                                                                                            If myDT9.Rows(d).Item(2) = myDT8.Rows(c).Item(0) Then
                                                                                                                                                                If myDT9.Rows(d).Item(7).ToString = "YES" Then
                                                                                                                                                                    Dim childNode9 As New TreeNode(myDT9.Rows(d).Item(1))
                                                                                                                                                                    childNode9.ChildNodes.Add(childNode9)
                                                                                                                                                                    mySetting.GetTreeViewSetting(childNode9, myDT9.Rows(d).Item(0), myDT9.Rows(d).Item(3), myDT9.Rows(d).Item(4), myDT9.Rows(d).Item(5), myDT9.Rows(d).Item(1), myDT9.Rows(d).Item(6), Session("Company"))
                                                                                                                                                                End If
                                                                                                                                                            End If
                                                                                                                                                        Next
                                                                                                                                                    End If
                                                                                                                                                End If
                                                                                                                                            End If
                                                                                                                                        Next
                                                                                                                                    End If
                                                                                                                                End If
                                                                                                                            End If
                                                                                                                        Next
                                                                                                                    End If
                                                                                                                End If
                                                                                                            End If
                                                                                                        Next
                                                                                                    End If
                                                                                                End If
                                                                                            End If
                                                                                        Next
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                        Next
                                                                    End If
                                                                End If
                                                            End If
                                                        Next
                                                    End If
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If
            Next
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            'set Resolution
            If Session("ScreenResolution") = "" Then
                Session("PagePath") = "../../Home.aspx" 'determaine ur path here
                Response.Redirect("Pages/Global/DetectScreen.aspx")
            Else
                Dim nsArray() As String = Split(Session("ScreenResolution").ToString(), "x", 2)
                Session("ScreenWidth") = CInt(nsArray(0))
                Session("ScreenHeight") = CInt(nsArray(1))
                If Session("ScreenWidth") > 350 Then
                    Session("GVwidth") = Session("ScreenWidth") - 350
                Else
                    Session("GVwidth") = Session("ScreenWidth")
                End If

                Session("GVheight") = (Session("ScreenHeight") / 2) - 50

                If CInt(nsArray(0)) < 1024 And CInt(nsArray(1)) < 768 Then
                    ShowMessage("Please SET your Window resolution to 1024x768 or above for better view...")
                End If
                'lblscreenresolution.Text = CInt(nsArray(0)) & " (px) in width and " & CInt(nsArray(1)) & " (px) in height..."
                End If

                'myTV.ShowLines = True
                WriteTreeNode()
                PagePreload()
                GetUserEmpInfo()

                'Date format setting
                ssql = "select string_value1 from parameter where company_profile_code = '" & Session("Company") & "' and module_profile_code = 'system_manager' and code = 'regional_setting'"
                myDS = mySQL.ExecuteSQL(ssql)
                If myDS.Tables(0).Rows.Count > 0 Then
                    Session("DateFormat") = LCase(myDS.Tables(0).Rows(0).Item(0).ToString)
                Else
                    Session("DateFormat") = "dd/mm/yyyy"
                End If

        Else
                'Do nothing
        End If
    End Sub

    Sub PagePreload()

        'setting company image/name
        Dim imgPathName As String = Session("Company") & "\" & Session("Company") & ".png"
        imgPath = imgPath & imgPathName
        If File.Exists(imgPath) Then
            LoadImage(imgPath)
            imgHeight = getImgHeight()
            imgWidth = getImgWidth()

            If imgHeight > 80 Then
                imgCompanyLogo.Height = 80
            Else
                imgCompanyLogo.Height = imgHeight
                imgHeight = ((80 - imgHeight) / 2)
                imgBlank.Height = imgHeight
            End If

            'If imgWidth > 140 Then
            '    imgCompanyLogo.Width = 140
            'Else
            '    imgCompanyLogo.Width = imgWidth
            'End If

            imgPathName = Replace(imgPathName, "\", "/")
            imgCompanyLogo.ImageUrl = "Images/Company/" & imgPathName

        Else
            imgCompanyLogo.ImageUrl = "Images/Company/Default/default.png"
            imgBlank.Height = 1
        End If

        ssql = "Select [Name] from company_profile where Code='" & Session("Company") & "'"
        DS = mySQL.ExecuteSQL(ssql)
        i = DS.Tables(0).Rows.Count
        If i > 0 Then
            lblCompanyName.Text = DS.Tables(0).Rows(0).Item(0).ToString
            lblCompanyName.CssClass = "titlestyle1"
        Else
            lblCompanyName.Text = "Company name NOT specified..."
            lblCompanyName.CssClass = "wordstyle2"
        End If
        DS = Nothing


        'Load Css
        pageCss1.Href = strPath & "/" & Session("strTheme") & "/Style.css"
        pageCss21.Href = "App_Themes/hcrmStyles1.css"

        'Load height setting
        Dim intInc As Integer = 0
        If CInt(Session("ScreenHeight")) > 800 Then
            intInc = CInt(Session("ScreenHeight")) - 800
        End If
        If CInt(Session("ScreenHeight")) > 1023 Then
            intInc = intInc * 0.8
        ElseIf CInt(Session("ScreenHeight")) > 900 Then
            intInc = intInc * 0.9
        End If

        trLayout01.Height = 690 + intInc
        imgLayout10.Height = 690 + intInc
        trLayout02.Height = 530 + intInc

        trLayout03.Height = 510 + intInc
        imgLayout22.Height = 510 + intInc
        trLayout04.Height = 510 + intInc
        tdLayout21.Height = 510 + intInc
        trLayout05.Height = 510 + intInc
        imgLayout28.Height = 510 + intInc
        imgLayout29.Height = 510 + intInc

        trLayout06.Height = 650 + intInc
        imgLayout44.Height = 650 + intInc
        imgLayout45.Height = 650 + intInc


        'Load Style/Image setting
        tdLayout01.Style("background-image") = strPath & "/" & Session("strTheme") & "/shadow_topmid.png"
        tdLayout01.Style("width") = "100%"
        tdLayout01.Style("height") = "5px"
        tdLayout02.Style("background-image") = strPath & "/" & Session("strTheme") & "/inner2_topleft.png"
        tdLayout03.Style("background-image") = strPath & "/" & Session("strTheme") & "/inner2_topmid.png"
        tdLayout04.Style("background-image") = strPath & "/" & Session("strTheme") & "/inner2_topright.png"
        tdLayout05.Style("background-image") = strPath & "/" & Session("strTheme") & "/inner_topmid.png"
        tdLayout06.Style("background-image") = strPath & "/" & Session("strTheme") & "/inner_leftmid.png"
        tdLayout06.Style("width") = "9px"
        tdLayout07.Style("background-image") = strPath & "/" & Session("strTheme") & "/shadow2_topmid.png"
        tdLayout08.Style("background-image") = strPath & "/" & Session("strTheme") & "/inner2_leftmid.png"
        tdLayout09.Style("background-image") = strPath & "/" & Session("strTheme") & "/inner2_rightmid.png"
        tdLayout10.Style("background-image") = strPath & "/" & Session("strTheme") & "/inner2_bottomleft.png"
        tdLayout11.Style("background-image") = strPath & "/" & Session("strTheme") & "/inner2_bottommid.png"
        tdLayout12.Style("background-image") = strPath & "/" & Session("strTheme") & "/inner2_bottomright.png"
        tdLayout13.Style("background-image") = strPath & "/" & Session("strTheme") & "/shadow2_leftmid.png"
        tdLayout14.Style("background-image") = strPath & "/" & Session("strTheme") & "/shadow2_rightmid.png"
        tdLayout15.Style("background-image") = strPath & "/" & Session("strTheme") & "/shadow2_bottommid.png"
        tdLayout16.Style("background-image") = strPath & "/" & Session("strTheme") & "/inner_rightmid.png"
        tdLayout17.Style("background-image") = strPath & "/" & Session("strTheme") & "/inner_bottommid.png"
        tdLayout18.Style("background-image") = strPath & "/" & Session("strTheme") & "/shadow_leftmid.png"
        tdLayout19.Style("background-image") = strPath & "/" & Session("strTheme") & "/shadow_rightmid.png"
        tdLayout20.Style("background-image") = strPath & "/" & Session("strTheme") & "/shadow_bottommid.png"

        imgLayout01.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout02.Src = strPath & "/" & Session("strTheme") & "/shadow_topleft.png"
        imgLayout03.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout04.Src = strPath & "/" & Session("strTheme") & "/shadow_topright.png"
        imgLayout05.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout06.Src = strPath & "/" & Session("strTheme") & "/shadow_lefttop.png"
        imgLayout07.Src = strPath & "/" & Session("strTheme") & "/inner_topleft.png"
        imgLayout08.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout09.Src = strPath & "/" & Session("strTheme") & "/inner_topright.png"
        imgLayout10.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout11.Src = strPath & "/" & Session("strTheme") & "/plus.gif"

        imgLayout13.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout14.Src = strPath & "/" & Session("strTheme") & "/shadow2_topleft.png"
        imgLayout15.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout16.Src = strPath & "/" & Session("strTheme") & "/shadow2_topright.png"
        imgLayout17.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout18.Src = strPath & "/" & Session("strTheme") & "/shadow2_lefttop.png"
        imgLayout19.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout20.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout21.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout22.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout23.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout24.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout25.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout26.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout27.Src = strPath & "/" & Session("strTheme") & "/shadow2_righttop.png"
        imgLayout28.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout29.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout30.Src = strPath & "/" & Session("strTheme") & "/shadow2_leftbottom.png"
        imgLayout31.Src = strPath & "/" & Session("strTheme") & "/shadow2_rightbottom.png"
        imgLayout32.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout33.Src = strPath & "/" & Session("strTheme") & "/shadow2_bottomleft.png"
        imgLayout34.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout35.Src = strPath & "/" & Session("strTheme") & "/shadow2_bottomright.png"
        imgLayout36.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout37.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout38.Src = strPath & "/" & Session("strTheme") & "/softfac_small_logo.gif"
        imgLayout39.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout40.Src = strPath & "/" & Session("strTheme") & "/inner_bottomleft.png"
        imgLayout41.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout42.Src = strPath & "/" & Session("strTheme") & "/inner_bottomright.png"
        imgLayout43.Src = strPath & "/" & Session("strTheme") & "/shadow_righttop.png"
        imgLayout44.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout45.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout46.Src = strPath & "/" & Session("strTheme") & "/shadow_leftbottom.png"
        imgLayout47.Src = strPath & "/" & Session("strTheme") & "/shadow_rightbottom.png"
        imgLayout48.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout49.Src = strPath & "/" & Session("strTheme") & "/shadow_bottomleft.png"
        imgLayout50.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLayout51.Src = strPath & "/" & Session("strTheme") & "/shadow_bottomright.png"
        imgLayout52.Src = strPath & "/" & Session("strTheme") & "/blank.gif"
        imgLogo.Src = strPath & "/" & Session("strTheme") & "/logo.gif"

        imgHome.Height = 15
        imgHome.Src = strPath & "/" & Session("strTheme") & "/btnHome.png"
        imgLogout.Height = 15
        imgLogout.Src = strPath & "/" & Session("strTheme") & "/btnLogout.png"
        'imgforum.Height = 15
        'imgforum.Src = strPath & "/" & Session("strTheme") & "/btnForum.png"
        'imgLayout53.ImageUrl = strPath & "/Company/Default/" & Session("strTheme") & "/blank.png"
        imgBlank.ImageUrl = strPath & "/Company/Default/" & Session("strTheme") & "/blank.png"

    End Sub

    Sub GetUserEmpInfo()

        lblWelcome.Text = "Welcome, " & Session("EmpID").ToString & "."

    End Sub

    Private Sub ShowMessage(ByVal message As String)

        Dim scriptString As String = "<script language=JavaScript>"
        scriptString += "alert('" + message + "');"
        scriptString += "<" & "/script>"
        Page.ClientScript.RegisterStartupScript(GetType(Page), "ShowMessage", scriptString)

    End Sub

    Sub LoadImage(ByVal strImageFile As String)
        m_bmpRepresentation = New Bitmap(strImageFile, False)
    End Sub

    Public Function getImgHeight() As Integer
        Return m_bmpRepresentation.Height
    End Function

    Public Function getImgWidth() As Integer
        Return m_bmpRepresentation.Width
    End Function

    Public Function Horizontal() As Integer
        Return m_bmpRepresentation.HorizontalResolution
    End Function

    Public Function Vertical() As Integer
        Return m_bmpRepresentation.VerticalResolution
    End Function

End Class
