Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.SectionReportModel
Imports System.Data
Imports System.Linq

Public Class rptHorizontal 
    Inherits GrapeCity.ActiveReports.SectionReport

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        AddHandler Me.Detail.Format, AddressOf detail_Format
        AddHandler Me.ReportStart, AddressOf rpthorizontal_ReportStart
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region " ActiveReports Designer generated code "
    'NOTE: The following procedure is required by the ActiveReports Designer
    'It can be modified using the ActiveReports Designer.
    'Do not modify it using the code editor.
    Private WithEvents PageHeader As GrapeCity.ActiveReports.SectionReportModel.PageHeader
    Private WithEvents Detail As GrapeCity.ActiveReports.SectionReportModel.Detail
    Private WithEvents reportInfo1 As GrapeCity.ActiveReports.SectionReportModel.ReportInfo
    Private WithEvents reportInfo2 As GrapeCity.ActiveReports.SectionReportModel.ReportInfo
    Private WithEvents PageFooter As GrapeCity.ActiveReports.SectionReportModel.PageFooter
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptHorizontal))
        Me.PageHeader = New GrapeCity.ActiveReports.SectionReportModel.PageHeader()
        Me.Detail = New GrapeCity.ActiveReports.SectionReportModel.Detail()
        Me.PageFooter = New GrapeCity.ActiveReports.SectionReportModel.PageFooter()
        Me.reportInfo1 = New GrapeCity.ActiveReports.SectionReportModel.ReportInfo()
        Me.reportInfo2 = New GrapeCity.ActiveReports.SectionReportModel.ReportInfo()
        CType(Me.reportInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.reportInfo2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PageHeader
        '
        Me.PageHeader.Height = 0.8229167!
        Me.PageHeader.Name = "PageHeader"
        '
        'Detail
        '
        Me.Detail.Name = "Detail"
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.reportInfo1, Me.reportInfo2})
        Me.PageFooter.Name = "PageFooter"
        '
        'reportInfo1
        '
        Me.reportInfo1.FormatString = "Page {PageNumber} of {PageCount}"
        Me.reportInfo1.Height = 0.2!
        Me.reportInfo1.Left = 0.0!
        Me.reportInfo1.Name = "reportInfo1"
        Me.reportInfo1.Style = ""
        Me.reportInfo1.Top = 0.0!
        Me.reportInfo1.Width = 1.947!
        '
        'reportInfo2
        '
        Me.reportInfo2.FormatString = "Pinted: {RunDateTime:}"
        Me.reportInfo2.Height = 0.2!
        Me.reportInfo2.Left = 8.448!
        Me.reportInfo2.Name = "reportInfo2"
        Me.reportInfo2.Style = "text-align: right"
        Me.reportInfo2.Top = 0.0!
        Me.reportInfo2.Width = 3.052001!
        '
        'rptHorizontal
        '
        Me.MasterReport = False
        Me.PageSettings.DefaultPaperSize = False
        Me.PageSettings.Margins.Bottom = 0.24!
        Me.PageSettings.Margins.Left = 0.17!
        Me.PageSettings.Margins.Right = 0.17!
        Me.PageSettings.Margins.Top = 0.24!
        Me.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape
        Me.PageSettings.PaperHeight = 11.69291!
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.PageSettings.PaperWidth = 8.267716!
        Me.PrintWidth = 11.5!
        Me.Sections.Add(Me.PageHeader)
        Me.Sections.Add(Me.Detail)
        Me.Sections.Add(Me.PageFooter)
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" & _
            "l; font-size: 10pt; color: Black; ddo-char-set: 204", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" & _
            "lic", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"))
        CType(Me.reportInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.reportInfo2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
#End Region

    Private m_companyName As String
    Public WriteOnly Property CompanyName() As String
        Set(value As String)
            m_companyName = value
        End Set
    End Property

    Private m_reportTitle As String
    Public WriteOnly Property ReportTitle() As String
        Set(value As String)
            m_reportTitle = value
        End Set
    End Property

    Private m_dataTable As DataTable
    Public WriteOnly Property OutputList() As DataTable
        Set(value As DataTable)
            m_dataTable = value
        End Set
    End Property

    Private m_groupTable As DataTable
    Public WriteOnly Property GroupList() As DataTable
        Set(value As DataTable)
            m_groupTable = value
        End Set
    End Property

    Private m_defaultHeight As Single = 0.2F
    Private m_currentX As Single = 0.0F
    Private m_currentY As Single = 0.0F
    Private m_txtWidth As Single = 0.0F
    Private m_lblWidth As Single = 0.0F
    'Set up report formatting and add fields based on user choices
    Private Sub constructReport()
        Try
            Me.detail.CanGrow = True
            Me.detail.CanShrink = True
            Me.detail.KeepTogether = True
            If m_groupTable IsNot Nothing Then
                Dim m_groupCount As Integer = 0
                Dim m_groupHeaderName As String = ""
                Dim m_groupFooterName As String = ""
                For i As Integer = 0 To m_groupTable.Columns.Count - 1
                    m_groupCount += 1
                    m_groupHeaderName = "GroupHeader" & Convert.ToString(m_groupCount)
                    m_groupFooterName = "GroupFooter" & Convert.ToString(m_groupCount)
                    'If the user wants grouping, add a group header and footer and set the grouping field
                    Me.Sections.InsertGroupHF()
                    DirectCast(Me.Sections(m_groupHeaderName), GroupHeader).DataField = m_groupTable.Columns(i).ColumnName
                    Me.Sections(m_groupHeaderName).BackColor = System.Drawing.Color.Gray
                    Me.Sections(m_groupHeaderName).CanGrow = True
                    Me.Sections(m_groupHeaderName).CanShrink = True
                    DirectCast(Me.Sections(m_groupHeaderName), GroupHeader).RepeatStyle = RepeatStyle.OnPageIncludeNoDetail
                    Me.Sections(m_groupFooterName).Height = 0
                    'Add a label to display group's name
                    Dim lbl As New Label()
                    lbl.Text = m_groupTable.Columns(i).Caption + " : "
                    lbl.Location = New System.Drawing.PointF(0.0F, 0)
                    lbl.Width = (1.0F / 11.0F) * CSng(lbl.Text.Length)
                    lbl.Height = 0.2F
                    lbl.Style = "font-weight: bold; font-size: 12pt;"
                    Me.Sections(m_groupHeaderName).Controls.Add(lbl)
                    'Add a textbox to display the group's value
                    Dim txt As New TextBox()
                    txt.DataField = m_groupTable.Columns(i).ColumnName
                    txt.Location = New System.Drawing.PointF(lbl.Width, 0)
                    txt.Width = (1.0F / 11.0F) * CSng(m_groupTable.Columns(i).MaxLength)
                    txt.Height = 0.2F
                    txt.Style = "font-weight: bold; font-size: 12pt;"
                    Me.Sections(m_groupHeaderName).Controls.Add(txt)
                Next

                Dim dtCopy As DataTable = m_dataTable.Copy()
                Dim col As DataColumn
                For Each col In dtCopy.Columns

                    Dim colCounts = (From x In m_groupTable.Columns Where x.ColumnName = col.ColumnName Select x.ColumnName).Count()

                    If colCounts > 0 Then
                        m_dataTable.Columns.Remove(col.ColumnName)
                    End If
                Next
            End If
            Dim lblCompany As New Label()
            lblCompany.Text = m_companyName
            lblCompany.Location = New System.Drawing.PointF(0.0F, m_currentY)
            lblCompany.Width = (1.0F / 11.0F) * CSng(m_companyName.Length)
            lblCompany.Height = 0.2F
            lblCompany.Style = "font-weight: bold; font-size: 11pt;"
            Me.pageHeader.Controls.Add(lblCompany)

            m_currentY += lblCompany.Height

            Dim lblTitle As New Label()
            lblTitle.Text = m_reportTitle
            'Set the location of each label//(m_currentY gets the height of each control added on each iteration)
            lblTitle.Location = New System.Drawing.PointF(0.0F, m_currentY)
            lblTitle.Width = (1.0F / 7.0F) * CSng(m_reportTitle.Length)
            lblTitle.Height = 0.35F
            lblTitle.Style = "font-weight: bold; font-size: 18pt;"
            Me.pageHeader.Controls.Add(lblTitle)

            m_currentY += lblTitle.Height

            Dim l1 As New Line()
            l1.X1 = 0.0F
            l1.X2 = 11.5F
            l1.Y1 = m_currentY
            l1.Y2 = m_currentY
            Me.pageHeader.Controls.Add(l1)

            m_currentY += 0.02F

            Dim l2 As New Line()
            l2.X1 = 0.0F
            l2.X2 = 11.5F
            l2.Y1 = m_currentY + m_defaultHeight + 0.02F
            l2.Y2 = m_currentY + m_defaultHeight + 0.02F
            Me.pageHeader.Controls.Add(l2)

            For i As Integer = 0 To m_dataTable.Columns.Count - 1

                Dim txt As New TextBox()
                'Set the textbox to display data 
                txt.DataField = m_dataTable.Columns(i).ColumnName
                'Set the location of the textbox
                txt.Location = New System.Drawing.PointF(m_currentX, 0.0F)
                txt.Width = (1.0F / 13.0F) * CSng(m_dataTable.Columns(i).MaxLength)
                txt.Height = m_defaultHeight
                Me.detail.Controls.Add(txt)
                'Set the textbox to use currency formatting if the field is UnitPrice
                If m_dataTable.Columns(i).DataType.Equals(System.Type.[GetType]("System.Decimal")) OrElse m_dataTable.Columns(i).DataType.Equals(System.Type.[GetType]("System.Double")) Then
                    txt.OutputFormat = "###,###,###.00"
                    txt.Width = (1.0F / 13.0F) * CSng(txt.OutputFormat.Length)
                End If
                If m_dataTable.Columns(i).DataType.Equals(System.Type.[GetType]("System.integer")) OrElse m_dataTable.Columns(i).DataType.Equals(System.Type.[GetType]("System.Int16")) OrElse m_dataTable.Columns(i).DataType.Equals(System.Type.[GetType]("System.Int32")) OrElse m_dataTable.Columns(i).DataType.Equals(System.Type.[GetType]("System.Int64")) Then
                    txt.OutputFormat = "###0"
                    txt.Width = (1.0F / 13.0F) * CSng(txt.OutputFormat.Length)
                End If
                If m_dataTable.Columns(i).DataType.Equals(System.Type.[GetType]("System.Boolean")) Then
                    txt.Width = (1.0F / 13.0F) * 5.0F
                End If

                m_lblWidth = (1.0F / 13.0F) * CSng(m_dataTable.Columns(i).Caption.Length)
                m_txtWidth = txt.Width

                Dim lbl As New Label()
                'Set the label to display the name of the selected field
                lbl.Text = m_dataTable.Columns(i).Caption
                'Set the location of each label//(m_currentY gets the height of each control added on each iteration)
                lbl.Location = New System.Drawing.PointF(m_currentX, m_currentY)
                If m_lblWidth > m_txtWidth Then
                    lbl.Width = m_lblWidth
                Else
                    lbl.Width = m_txtWidth
                End If
                lbl.Height = m_defaultHeight
                lbl.Style = "font-weight: bold; font-size: 10pt;"
                Me.pageHeader.Controls.Add(lbl)

                'Increment the horizontal location by adding the height of the added controls
                If m_lblWidth > m_txtWidth Then
                    m_currentX = m_currentX + m_lblWidth
                Else
                    m_currentX = m_currentX + m_txtWidth

                End If
            Next

        Catch ex As Exception
            Throw New NotImplementedException(ex.Message)
        End Try
    End Sub

    Private m_count As Integer
    Private Sub detail_Format(sender As Object, e As EventArgs)
        If m_count Mod 2 = 0 Then
            Me.Detail.BackColor = System.Drawing.Color.SlateGray
        Else
            Me.Detail.BackColor = System.Drawing.Color.Gainsboro
        End If
        m_count += 1
    End Sub

    Private Sub rptHorizontal_ReportStart(ByVal sender As Object, ByVal e As EventArgs)
        constructReport()
    End Sub

End Class
