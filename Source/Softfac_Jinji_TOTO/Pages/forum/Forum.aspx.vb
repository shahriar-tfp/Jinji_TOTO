Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Text
Imports System.Configuration


Namespace Forum
    ''' <summary>
    ''' Summary description for Forum.
    ''' </summary>
    Partial Public Class Forum
        Inherits System.Web.UI.Page
        Private articleId As String = "empty"
        Private currentCount As Integer = 1
        Private name As String = "empty"
        Private company As String = "empty"
        'private int pagesize= 20;
        Private cs As New clsSQL
        Private sUrl As String = System.AppDomain.CurrentDomain.BaseDirectory() + "\Pages\forum\"
        Public Property PageSize() As Integer
            Get
                Dim o As Object = ViewState("PageSize")
                If (o Is Nothing) Then
                    Return 20
                End If
                Return Integer.Parse(o.ToString())
            End Get
            Set(ByVal value As Integer)
                ViewState("PageSize") = Value
            End Set
        End Property


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            ' Put user code to initialize the page here

            If Request.QueryString("name") IsNot Nothing Then
                name = Request.QueryString("name")
            Else
                name = employeename.Value.ToString()
            End If

            If Request.QueryString("company") IsNot Nothing Then
                company = Request.QueryString("company")
            Else
                company = Session("Company").ToString()
            End If

            If Request.QueryString("state") IsNot Nothing Then
                statetext.Text = Request.QueryString("state")
                btnhistory.Text = "Forum"
            Else
                statetext.Text = "show"
                btnhistory.Text = "History"
            End If

            If Request.QueryString("id") IsNot Nothing Then
                articleId = Request.QueryString("id")
            Else
                articleId = Session("EmpID").ToString()
            End If

            If Page.IsPostBack Then
                'Response.Write ("<h1>" +txtpagesize.SelectedItem.Text + "::" + PageSize +"</h1>");
                PageSize = Convert.ToInt32(txtpagesize.SelectedItem.Text)
            Else

                If Request.QueryString("pagesize") IsNot Nothing Then
                    Me.PageSize = Convert.ToInt32(Request.QueryString("pagesize"))
                End If
                Dim myC As New SqlConnection()
                myC.ConnectionString = cs.GetConnectionString()
                Dim myQuery As String = ""
                myQuery = "select Name from User_Profile where Code='" + articleId.ToString() + "' "

                myC.Open()
                Dim myCommand As New SqlCommand(myQuery, myC)
                Dim myReader As SqlDataReader = myCommand.ExecuteReader()

                If myReader.HasRows Then
                    If myReader.Read() Then
                        employeename.Value = myReader("Name").ToString()
                        name = employeename.Value.ToString()
                        'Page.RegisterStartupScript("adsd", "<script>alert('" + name.ToString() + "')</script>")
                    End If
                End If
                myReader.Close()

            End If

            txtpagesize.ClearSelection()
            txtpagesize.Items.FindByText(Me.PageSize.ToString()).Selected = True

            'Response.Write ("<h1>" +PageSize +"</h1>");



            LoadData()
        End Sub
        Private Sub LoadData()
            Dim lastVisit As DateTime = DateTime.Now
            Dim sb As New StringBuilder()
            'string myQuery ="";


            'Response.Write ("<hr>" + myQuery + "<hr>");

            'lblnewmessage.Text = "<A title='Add a new message to the Article " + articleId + "' href='newmessage.aspx?id=" + articleId + "'><b><FONT>New Message</FONT></b></A>&nbsp;||&nbsp;<A title='Add a TEST message to the Article " + articleId + "' href='newmessage.aspx?id=" + articleId + "&amp;Test=true'><b><FONT>Test Message</FONT></b></A>"

            Dim myclas As New clsDataAccess()
            myclas.openConnection()



            Dim myReader As SqlDataReader = myclas.getForumData(articleId, statetext.Text.ToString())

            Dim mycount As Integer = 1
            Dim myNewMessage As Integer = 0


            While myReader.Read()

                Dim dt1 As DateTime = DateTime.Now
                Dim dt2 As DateTime = Convert.ToDateTime(myReader("DateAdded").ToString())
                If mycount = 1 Then
                    lastVisit = Convert.ToDateTime(myReader("DateAdded").ToString())
                Else
                    If DateTime.Compare(lastVisit, dt2) < 0 Then
                        lastVisit = dt2
                    End If
                End If


                Dim ts As TimeSpan = dt1.Subtract(dt2)

                Dim mytimeago As String = ""
                If Convert.ToInt32(ts.TotalDays) <> 0 Then
                    mytimeago = "" + Convert.ToString(Math.Abs(Convert.ToInt32(ts.TotalDays))) + " Days ago"
                Else
                    If (Convert.ToInt32(ts.TotalMinutes) < 5) AndAlso (Convert.ToInt32(ts.TotalHours) = 0) Then
                        mytimeago = "Just Posted"
                    ElseIf (Convert.ToInt32(ts.TotalMinutes) > 5) AndAlso (Convert.ToInt32(ts.TotalHours) = 0) Then
                        mytimeago = Convert.ToString(Convert.ToInt32(ts.TotalMinutes) Mod 60) + " Mins ago"
                    ElseIf Convert.ToInt32(ts.TotalHours) <> 0 Then
                        mytimeago = Convert.ToInt32(ts.TotalHours).ToString() + " Hours " + Convert.ToString(Convert.ToInt32(ts.TotalMinutes) Mod 60) + " Mins ago"
                    Else
                        mytimeago = Convert.ToString(Convert.ToInt32(ts.TotalMinutes) Mod 60) + " Mins ago"
                    End If
                End If

                Dim newimg As String = ""
                'If [String].Compare(mytimeago, "Just Posted") = 0 And myReader("UserName") <> articleId.ToString() + " [" + name.ToString() + "]" Then
                If myReader("NewMessage").ToString() = "new" Then
                    newimg = "<img id='K1745932k" + Convert.ToString(mycount) + "kIMG' src='../../Images/default/new.gif' border='0' alt=''>"
                    myNewMessage = myNewMessage + 1
                End If


                'if(mycount==1)
                'sb.Append("<tr bgcolor='#b7dfd5' id='K1745932k" + mycount + "kOFF'>");
                'else

                If Request.QueryString("current") IsNot Nothing Then
                    currentCount = Convert.ToInt32(Request.QueryString("current"))
                Else
                    currentCount = 1
                End If

                Dim myMaxCount As Integer = currentCount + Convert.ToInt32(Me.PageSize)
                Dim myStartCount As Integer = currentCount

                If currentCount = -1 Then
                    myStartCount = 0
                    myMaxCount = 999
                End If



                If mycount < myMaxCount AndAlso mycount >= myStartCount Then
                    sb.Append("<tr bgcolor='#EDF8F4' id='K1745932k" + Convert.ToString(mycount) + "kOFF' class='dgstyle_i'  >")

                    sb.Append("<td width='100%' colspan='1'>")

                    sb.Append("<input type='text' id='K1745932k" + Convert.ToString(mycount) + "kTXT' style='display:none' value='" + myReader("NewMessage").ToString() + "' />")
                    sb.Append("<input type='text' id='K1745932k" + Convert.ToString(mycount) + "kAID' style='display:none' value='" + articleId + "' />")
                    sb.Append("<input type='text' id='K1745932k" + Convert.ToString(mycount) + "kID' style='display:none' value='" + myReader("Id").ToString() + "' />")


                    sb.Append("<table border='0' cellspacing='0' cellpadding='0' width='100%'  class='dgstyle_i' >")
                    sb.Append("<tr>")

                    Dim myindent As Integer = 4
                    If Convert.ToInt32(myReader("Indent")) <= 4 Then
                        myindent = 16 * Convert.ToInt32(myReader("Indent"))
                    ElseIf Convert.ToInt32(myReader("Indent")) <= 8 Then
                        myindent = 15 * Convert.ToInt32(myReader("Indent"))
                    ElseIf Convert.ToInt32(myReader("Indent")) <= 16 Then
                        myindent = 14 * Convert.ToInt32(myReader("Indent"))
                    ElseIf Convert.ToInt32(myReader("Indent")) <= 20 Then
                        myindent = Convert.ToInt32(13.5 * Convert.ToDouble(myReader("Indent")))
                    ElseIf Convert.ToInt32(myReader("Indent")) <= 24 Then
                        myindent = 13 * Convert.ToInt32(myReader("Indent"))
                    ElseIf Convert.ToInt32(myReader("Indent")) <= 28 Then
                        myindent = Convert.ToInt32(12.7 * Convert.ToDouble(myReader("Indent")))
                    ElseIf Convert.ToInt32(myReader("Indent")) <= 32 Then
                        myindent = Convert.ToInt32(12.4 * Convert.ToDouble(myReader("Indent")))
                    End If

                    'sb.Append("<td width='10%' bgcolor='white' align='middle'><a name='xxK1745932k" + Convert.ToString(mycount) + "kxx'></a><img height='1' width='" + Convert.ToString(myindent) + "' src='../../Images/default/ind.gif' alt=''>")
                    sb.Append("<td width='50%' colspan='2'  ><a name='xxK1745932k" + Convert.ToString(mycount) + "kxx'></a><img height='1' width='" + Convert.ToString(myindent) + "' src='../../Images/default/ind.gif' alt=''>")


                    If Convert.ToInt32(myReader("CommentType").ToString()) = 1 Then
                        sb.Append("<img  src='../../Images/default/general.gif' alt=''/>&nbsp;")
                    End If
                    If Convert.ToInt32(myReader("CommentType").ToString()) = 2 Then
                        sb.Append("<img  src='../../Images/default/info.gif' alt=''/>&nbsp;")
                    End If
                    If Convert.ToInt32(myReader("CommentType").ToString()) = 3 Then
                        sb.Append("<img  src='../../Images/default/answer.gif' alt=''/>&nbsp;")
                    End If
                    If Convert.ToInt32(myReader("CommentType").ToString()) = 4 Then
                        sb.Append("<img  src='../../Images/default/question.gif' alt=''/>&nbsp;")
                    End If
                    If Convert.ToInt32(myReader("CommentType").ToString()) = 5 Then
                        sb.Append("<img  src='../../Images/default/game.gif' alt=''/>&nbsp;")
                    End If



                    Dim deleteRestore As String = " "
                    If statetext.Text.ToString() = "show" And Convert.ToInt32(myReader("Indent")) = 0 Then
                        deleteRestore = " <a href='Delete.aspx?id=" + articleId + "&amp;company=" + company + "&amp;name=" + name + "&amp;CID=" + myReader("ID").ToString() + "&amp;pagesize=" + Convert.ToString(Me.PageSize) + "&amp;type=minus' "
                        deleteRestore += "title='Delete this current thread'><img border='0' align='middle' src='../../Images/default/delete.gif' alt='Delete'/></a>"
                    ElseIf statetext.Text.ToString() <> "show" And Convert.ToInt32(myReader("Indent")) = 0 Then
                        deleteRestore = " <a href='Delete.aspx?id=" + articleId + "&amp;company=" + company + "&amp;name=" + name + "&amp;CID=" + myReader("ID").ToString() + "&amp;pagesize=" + Convert.ToString(Me.PageSize) + "&amp;type=addminus' "
                        deleteRestore += "title='Delete this current thread'><img border='0' align='middle' src='../../Images/default/restore.gif' alt='Restore'/></a>"
                    End If

                    sb.Append(deleteRestore + "&nbsp;")
                    'sb.Append("</td>")

                    'sb.Append("<td width='45%'  style='font-family:Arial Unicode MS;font-size:11px;font-weight:700;' ><a id='LinkTrigger" + Convert.ToString(mycount) + "' name='K1745932k" + Convert.ToString(mycount) + "k' href='K1745932#xxK1745932k" + Convert.ToString(mycount) + "kxx'>")
                    sb.Append("<a id='LinkTrigger" + Convert.ToString(mycount) + "' name='K1745932k" + Convert.ToString(mycount) + "k' href='K1745932#xxK1745932k" + Convert.ToString(mycount) + "kxx'>")





                    If Convert.ToInt32(myReader("Indent")) = 0 Then
                        sb.Append("&nbsp;" + myReader("Title").ToString() + "</a>" + newimg + "</td>")
                    Else
                        sb.Append("&nbsp;" + myReader("Title").ToString() + "<!-- : " + Convert.ToString(myindent) + "::" + Convert.ToString(myReader("Indent")) + "--></a>" + newimg + "</td>")
                    End If



                    If statetext.Text.ToString() = "show" Then
                        sb.Append("<td style='width:2%;' align='center'>")
                        If System.IO.File.Exists(sUrl + "Upload//" + Convert.ToString(myReader("AttachmentID")) + "//ScreenCapture.jpg") Then
                            sb.Append("<img src='../../Images/default/info.gif' alt='See Screen Capture Image' border='0' width='14' style='cursor:hand;' height='15' onclick='screenOpen(" + Convert.ToString(myReader("AttachmentID")) + ")' />")
                        End If
                        sb.Append("&nbsp;</td>")
                        If Not myReader("ASessionId").ToString() = "" Then
                            sb.Append("<td style='width:9%;' align='center'><img src='../../Images/default/general.gif' alt='See Upload Files' border='0' width='14' style='cursor:hand;' height='15' onclick='folderOpen(" + Convert.ToString(myReader("ID")) + ")' /></td>")
                        Else
                            sb.Append("<td style='width:9%;' align='center'>&nbsp;</td>")
                        End If


                        'tdscreen.InnerText = "Screen Capture"
                        tdattach.InnerText = "Attachment Files"
                    Else
                        sb.Append("<td style='width:2%;' align='center'>&nbsp;</td><td style='width:9%;' align='center'>&nbsp;</td>")
                        tdscreen.InnerText = ""
                        tdattach.InnerText = ""
                    End If




                    Dim userImgURL As String = myReader("Image_Path").ToString()

                    If userImgURL = Nothing Then
                        sb.Append("<td align='center'  width='15%'  ><img src='../../Images/default/UserPhoto.png' alt='' border='0' width='20' />&nbsp;")
                    Else
                        sb.Append("<td align='center' width='15%'  ><img src='../../" + userImgURL.ToString() + "' alt='' border='0' width='20' />&nbsp;")
                    End If


                    sb.Append("" + myReader("UserName").ToString() + "&nbsp;</td>")
                    sb.Append("<td style='width:19%;' align='center'>" + mytimeago)
                    sb.Append("&nbsp;</td>")
                    sb.Append("</tr>")
                    sb.Append("</table>")
                    sb.Append("</td>")
                    sb.Append("</tr>")

                    sb.Append("<tr id='K1745932k" + Convert.ToString(mycount) + "kON' style='DISPLAY:none'>")

                    sb.Append("<td colspan='1' width='100%'>")
                    sb.Append("<table border='0' cellspacing='0' cellpadding='0' width='100%'>")
                    sb.Append("<tr>")
                    sb.Append("<td><img height='1' width='" + Convert.ToString(myindent) + "' src='../../Images/default/ind.gif' alt=''><img align='middle' src='../../Images/default/lblank.gif' height='15' width='15' alt=''>&nbsp;</td>")
                    sb.Append("<td bgcolor='#EDF8F4' width='100%'><table border='0' cellspacing='5' cellpadding='0' width='100%'>")
                    sb.Append("<tr>")
                    sb.Append("<td>")
                    sb.Append("<table border='0' cellspacing='0' cellpadding='0' width='100%'>")
                    sb.Append("<tr>")
                    sb.Append("<td colspan='2'>")
                    sb.Append("<font>" + myReader("Description").ToString() + "</font>")
                    '" Time Now:" + dt1 + " DBTime:" + dt2 +"
                    sb.Append("<br>")
                    sb.Append("&nbsp;</td>")
                    sb.Append("</tr>")
                    sb.Append("<tr valign='top'>")



                    If statetext.Text.ToString() = "show" Then
                        sb.Append("<td  class='dgstyle_i' >[<a href='Reply.aspx?id=" + articleId + "&amp;CID=" + myReader("ID").ToString() + "&amp;name=" + name + "&amp;company=" + company + "' title='Reply to this current thread'>Reply</a>]")
                    Else
                        sb.Append("<td>")
                    End If
                    sb.Append("</td>")

                    'If statetext.Text.ToString() = "show" Then
                    '    sb.Append("<td align='right' >[<a href='Delete.aspx?id=" + articleId + "&amp;company=" + company + "&amp;name=" + name + "&amp;CID=" + myReader("ID").ToString() + "&amp;pagesize=" + Convert.ToString(Me.PageSize) + "&amp;type=minus' ")
                    '    sb.Append("title='Delete this current thread'><font>Delete</font></a>]")
                    'Else
                    '    sb.Append("<td align='right' >[<a href='Delete.aspx?id=" + articleId + "&amp;company=" + company + "&amp;name=" + name + "&amp;CID=" + myReader("ID").ToString() + "&amp;pagesize=" + Convert.ToString(Me.PageSize) + "&amp;type=addminus' ")
                    '    sb.Append("title='Delete this current thread'><font>Restore</font></a>]")
                    'End If

                    sb.Append("<td>")
                    sb.Append("</td>")
                    sb.Append("</tr>")
                    sb.Append("</table>")
                    sb.Append("</td>")
                    sb.Append("</tr>")
                    sb.Append("</table>")
                    sb.Append("</td>")
                    sb.Append("</tr>")
                    sb.Append("</table>")
                    sb.Append("</td>")
                    sb.Append("</tr>")
                    sb.Append("<tr>")
                    sb.Append("<td colspan='1'><img src='../../Images/default/t.gif' border='0' width='1' height='6' alt=''></td>")
                    sb.Append("</tr>")
                End If
                mycount += 1
            End While

            myReader.Close()
            myclas.closeConnection()

            mycount = mycount - 1
            If currentCount = 1 Then
                lblPaging.Text = "First&nbsp;&nbsp;Prev&nbsp;&nbsp;"
            Else
                lblPaging.Text = "<a href='Forum.aspx?id=" + articleId + "&amp;name=" + name + "&amp;company=" + company + "&amp;current=1&amp;pagesize=" + Convert.ToString(Me.PageSize) + "'>First</a>&nbsp;&nbsp;<a href='Forum.aspx?id=" + articleId + "&amp;name=" + name + "&amp;company=" + company + "&amp;current=" + Convert.ToString(currentCount - Convert.ToInt32(Me.PageSize)) + "&amp;pagesize=" + Convert.ToString(Me.PageSize) + "'>Prev</a>&nbsp&nbsp;"
            End If

            lblPaging.Text += "" + Convert.ToString(Convert.ToInt32(currentCount / Me.PageSize) + 1)
            If mycount Mod Me.PageSize = 0 Then
                lblPaging.Text += "&nbsp;(&nbsp;" + Convert.ToString(Convert.ToInt32(mycount / Me.PageSize)) + "&nbsp;)&nbsp;&nbsp;"
            Else
                lblPaging.Text += "&nbsp;(&nbsp;" + Convert.ToString(Math.Floor(mycount / Me.PageSize) + 1) + "&nbsp;)&nbsp;&nbsp;"
            End If


            If mycount >= (Convert.ToInt32(Me.PageSize) + currentCount) Then
                lblPaging.Text += "<a href='Forum.aspx?id=" + articleId + "&amp;name=" + name + "&amp;company=" + company + "&amp;pagesize=" + Convert.ToString(Me.PageSize) + "&amp;current=" + Convert.ToString(Convert.ToInt32(Me.PageSize) + currentCount) + "'>Next</a>&nbsp;&nbsp;<a href='Forum.aspx?id=" + articleId + "&amp;name=" + name + "&amp;company=" + company + "&amp;pagesize=" + Convert.ToString(Me.PageSize) + "&amp;current=" + Convert.ToString(mycount - ((mycount - 1) Mod Convert.ToInt32(Me.PageSize))) + "'>Last</a>&nbsp;&nbsp;"
            Else
                lblPaging.Text += "Next&nbsp;&nbsp;Last&nbsp;&nbsp;"
            End If


            ltlPost.Text = sb.ToString()

            'lbldate.Text = "Last Visit: " + lastVisit.ToLongTimeString() + ", " + lastVisit.ToLongDateString()

            If myNewMessage <> 0 Then
                tdnewmessage.InnerHtml = "New Message : <label id='newmessagelabel'>" + myNewMessage.ToString() + "</label>"
            End If


        End Sub
        Protected Sub btnhistory_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            If statetext.Text.ToString() = "show" Then
                Response.Redirect("Forum.aspx?id=" + articleId + "&pagesize=" + Convert.ToString(Me.PageSize) + "&name=" + name + "&company=" + company + "&state=history")
            Else
                Response.Redirect("Forum.aspx?id=" + articleId + "&pagesize=" + Convert.ToString(Me.PageSize) + "&name=" + name + "&company=" + company)
            End If
        End Sub
        Protected Sub getMessages(ByVal sender As Object, ByVal e As System.EventArgs)

        End Sub
        Protected Sub newMessage(ByVal sender As Object, ByVal e As System.EventArgs)
            Response.Redirect("NewMessage.aspx")
        End Sub

#Region "Web Form Designer generated code"
        Protected Overloads Overrides Sub OnInit(ByVal e As EventArgs)
            '
            ' CODEGEN: This call is required by the ASP.NET Web Form Designer.
            '
            InitializeComponent()
            MyBase.OnInit(e)
        End Sub

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()

        End Sub
#End Region

        Protected Sub ltlPost_Init(ByVal sender As Object, ByVal e As System.EventArgs)

        End Sub

        Protected Sub btnsetpaging_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        End Sub

        Protected Sub txtpageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        End Sub


    End Class
End Namespace