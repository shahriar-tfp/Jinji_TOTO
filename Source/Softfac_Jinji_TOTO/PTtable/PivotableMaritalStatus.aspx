<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PivotableMaritalStatus.aspx.vb" Inherits="Pivot_PivotableMaritalStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" visible="false" id="div" runat="server">
<object classid="clsid:0002E55D-0000-0000-C000-000000000046" id="ChartSpace1">
	<param name="XMLData" value="&lt;xml xmlns:x=&quot;urn:schemas-microsoft-com:office:excel&quot;&gt;
 &lt;x:ChartSpace&gt;
  &lt;x:OWCVersion&gt;11.0.0.5531         &lt;/x:OWCVersion&gt;
  &lt;x:Width&gt;15240&lt;/x:Width&gt;
  &lt;x:Height&gt;10160&lt;/x:Height&gt;
  &lt;x:DataSource&gt;
   &lt;x:Type&gt;PivotList&lt;/x:Type&gt;
   &lt;x:Name&gt;PivotTable1&lt;/x:Name&gt;
  &lt;/x:DataSource&gt;
  &lt;x:BoundSeries&gt;
   &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
  &lt;/x:BoundSeries&gt;
  &lt;x:Category&gt;
   &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
  &lt;/x:Category&gt;
  &lt;x:BoundCharts&gt;
   &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
  &lt;/x:BoundCharts&gt;
  &lt;x:FormatValue&gt;
   &lt;x:DataSourceIndex&gt;-3&lt;/x:DataSourceIndex&gt;
   &lt;x:Data&gt;2&lt;/x:Data&gt;
  &lt;/x:FormatValue&gt;
  &lt;x:PivotAggOrientation&gt;None&lt;/x:PivotAggOrientation&gt;
  &lt;x:DisplayFieldList/&gt;
  &lt;x:Palette&gt;
   &lt;x:Entry&gt;#000000&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#000000&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#000000&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#000000&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#000000&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#000000&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#000000&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#000000&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#000000&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#000000&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#000000&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#000000&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#000000&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#000000&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#000000&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#000000&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#8080FF&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#802060&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#FFFFA0&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#A0E0E0&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#600080&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#FF8080&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#008080&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#C0C0FF&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#000080&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#FF00FF&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#80FFFF&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#0080FF&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#FF8080&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#C0FF80&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#FFC0FF&lt;/x:Entry&gt;
   &lt;x:Entry&gt;#FF80FF&lt;/x:Entry&gt;
  &lt;/x:Palette&gt;
  &lt;x:DefaultFont&gt;Arial&lt;/x:DefaultFont&gt;
  &lt;x:Border&gt;
   &lt;x:Color&gt;#0000FF&lt;/x:Color&gt;
  &lt;/x:Border&gt;
  &lt;x:Chart&gt;
   &lt;x:PlotArea&gt;
    &lt;x:Graph&gt;
     &lt;x:SubType&gt;Clustered&lt;/x:SubType&gt;
     &lt;x:Type&gt;Column&lt;/x:Type&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;FEMALE - MARRIED&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;FEMALE - MARRIED&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;0&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:FormatValue&gt;
       &lt;x:DataSourceIndex&gt;-3&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;2&lt;/x:Data&gt;
      &lt;/x:FormatValue&gt;
      &lt;x:Marker&gt;
       &lt;x:Symbol&gt;None&lt;/x:Symbol&gt;
      &lt;/x:Marker&gt;
      &lt;x:Explode&gt;0&lt;/x:Explode&gt;
      &lt;x:Thickness&gt;10&lt;/x:Thickness&gt;
      &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;x:Identifier&gt;!.FEMALE.MARRIED&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;FEMALE - OTHER&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;FEMALE - OTHER&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;2&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:FormatValue&gt;
       &lt;x:DataSourceIndex&gt;-3&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;2&lt;/x:Data&gt;
      &lt;/x:FormatValue&gt;
      &lt;x:Marker&gt;
       &lt;x:Symbol&gt;None&lt;/x:Symbol&gt;
      &lt;/x:Marker&gt;
      &lt;x:Explode&gt;0&lt;/x:Explode&gt;
      &lt;x:Thickness&gt;10&lt;/x:Thickness&gt;
      &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;x:Identifier&gt;!.FEMALE.OTHER&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;FEMALE - SINGLE&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;FEMALE - SINGLE&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;3&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:FormatValue&gt;
       &lt;x:DataSourceIndex&gt;-3&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;2&lt;/x:Data&gt;
      &lt;/x:FormatValue&gt;
      &lt;x:Marker&gt;
       &lt;x:Symbol&gt;None&lt;/x:Symbol&gt;
      &lt;/x:Marker&gt;
      &lt;x:Explode&gt;0&lt;/x:Explode&gt;
      &lt;x:Thickness&gt;10&lt;/x:Thickness&gt;
      &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;x:Identifier&gt;!.FEMALE.SINGLE&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;MALE - MARRIED&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;MALE - MARRIED&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;1&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:FormatValue&gt;
       &lt;x:DataSourceIndex&gt;-3&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;2&lt;/x:Data&gt;
      &lt;/x:FormatValue&gt;
      &lt;x:Marker&gt;
       &lt;x:Symbol&gt;None&lt;/x:Symbol&gt;
      &lt;/x:Marker&gt;
      &lt;x:Explode&gt;0&lt;/x:Explode&gt;
      &lt;x:Thickness&gt;10&lt;/x:Thickness&gt;
      &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;x:Identifier&gt;!.MALE.MARRIED&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;MALE - OTHER&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;MALE - OTHER&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;4&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:FormatValue&gt;
       &lt;x:DataSourceIndex&gt;-3&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;2&lt;/x:Data&gt;
      &lt;/x:FormatValue&gt;
      &lt;x:Marker&gt;
       &lt;x:Symbol&gt;None&lt;/x:Symbol&gt;
      &lt;/x:Marker&gt;
      &lt;x:Explode&gt;0&lt;/x:Explode&gt;
      &lt;x:Thickness&gt;10&lt;/x:Thickness&gt;
      &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;x:Identifier&gt;!.MALE.OTHER&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;MALE - SINGLE&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;MALE - SINGLE&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;5&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:FormatValue&gt;
       &lt;x:DataSourceIndex&gt;-3&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;2&lt;/x:Data&gt;
      &lt;/x:FormatValue&gt;
      &lt;x:Marker&gt;
       &lt;x:Symbol&gt;None&lt;/x:Symbol&gt;
      &lt;/x:Marker&gt;
      &lt;x:Explode&gt;0&lt;/x:Explode&gt;
      &lt;x:Thickness&gt;10&lt;/x:Thickness&gt;
      &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;x:Identifier&gt;!.MALE.SINGLE&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Dimension&gt;
      &lt;x:ScaleID&gt;185184064&lt;/x:ScaleID&gt;
      &lt;x:Index&gt;Categories&lt;/x:Index&gt;
     &lt;/x:Dimension&gt;
     &lt;x:Dimension&gt;
      &lt;x:ScaleID&gt;185182660&lt;/x:ScaleID&gt;
      &lt;x:Index&gt;Value&lt;/x:Index&gt;
     &lt;/x:Dimension&gt;
     &lt;x:Dimension&gt;
      &lt;x:ScaleID&gt;185182864&lt;/x:ScaleID&gt;
      &lt;x:Index&gt;FormatValue&lt;/x:Index&gt;
     &lt;/x:Dimension&gt;
     &lt;x:Overlap&gt;0&lt;/x:Overlap&gt;
     &lt;x:GapWidth&gt;150&lt;/x:GapWidth&gt;
     &lt;x:FirstSliceAngle&gt;0&lt;/x:FirstSliceAngle&gt;
    &lt;/x:Graph&gt;
    &lt;x:Axis&gt;
     &lt;x:AxisID&gt;185177076&lt;/x:AxisID&gt;
     &lt;x:ScaleID&gt;185184064&lt;/x:ScaleID&gt;
     &lt;x:Type&gt;Category&lt;/x:Type&gt;
     &lt;x:MajorTick&gt;Outside&lt;/x:MajorTick&gt;
     &lt;x:MinorTick&gt;None&lt;/x:MinorTick&gt;
     &lt;x:Placement&gt;Bottom&lt;/x:Placement&gt;
     &lt;x:GroupingEnum&gt;Auto&lt;/x:GroupingEnum&gt;
    &lt;/x:Axis&gt;
    &lt;x:Axis&gt;
     &lt;x:AxisID&gt;185177564&lt;/x:AxisID&gt;
     &lt;x:ScaleID&gt;185182660&lt;/x:ScaleID&gt;
     &lt;x:Type&gt;Value&lt;/x:Type&gt;
     &lt;x:MajorGridlines&gt;
     &lt;/x:MajorGridlines&gt;
     &lt;x:MajorTick&gt;Outside&lt;/x:MajorTick&gt;
     &lt;x:MinorTick&gt;None&lt;/x:MinorTick&gt;
     &lt;x:Placement&gt;Left&lt;/x:Placement&gt;
    &lt;/x:Axis&gt;
   &lt;/x:PlotArea&gt;
   &lt;x:Identifier&gt;&lt;/x:Identifier&gt;
  &lt;/x:Chart&gt;
  &lt;x:Scaling&gt;
   &lt;x:ScaleID&gt;185184064&lt;/x:ScaleID&gt;
  &lt;/x:Scaling&gt;
  &lt;x:Scaling&gt;
   &lt;x:ScaleID&gt;185182660&lt;/x:ScaleID&gt;
  &lt;/x:Scaling&gt;
  &lt;x:Scaling&gt;
   &lt;x:ScaleID&gt;185182864&lt;/x:ScaleID&gt;
  &lt;/x:Scaling&gt;
  &lt;x:DisplayToolbar/&gt;
 &lt;/x:ChartSpace&gt;
&lt;/xml&gt;" />
	<param name="ScreenUpdating" value="-1" />
	<param name="EnableEvents" value="-1" />
</object>
<br />
<object classid="clsid:0002E55A-0000-0000-C000-000000000046" id="PivotTable1">
	<param name="XMLData" value="&lt;xml xmlns:x=&quot;urn:schemas-microsoft-com:office:excel&quot;&gt;
 &lt;x:PivotTable&gt;
  &lt;x:OWCVersion&gt;11.0.0.5531         &lt;/x:OWCVersion&gt;
  &lt;x:DisplayScreenTips/&gt;
  &lt;x:CubeProvider&gt;msolap.2&lt;/x:CubeProvider&gt;
  &lt;x:DisplayFieldList/&gt;
  &lt;x:FieldListTop&gt;305&lt;/x:FieldListTop&gt;
  &lt;x:FieldListLeft&gt;814&lt;/x:FieldListLeft&gt;
  &lt;x:FieldListBottom&gt;674&lt;/x:FieldListBottom&gt;
  &lt;x:FieldListRight&gt;1014&lt;/x:FieldListRight&gt;
  &lt;x:CacheDetails/&gt;
  &lt;x:ConnectionString&gt;Provider=SQLOLEDB.1;Password=_pwd_;Persist Security Info=True;User ID=_uid_;Initial Catalog=_database_;Data Source=_server_;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;Workstation ID=ISSACCOM;Use Encryption for Data=False;Tag with column collation when possible=False&lt;/x:ConnectionString&gt;
  &lt;x:DataMember&gt;&amp;quot;_database_&amp;quot;.&amp;quot;dbo&amp;quot;.&amp;quot;Excel_Head_Count_Analysis_Vw_org&amp;quot;&lt;/x:DataMember&gt;
  &lt;x:Name&gt;Contractor Ratio&lt;/x:Name&gt;
  &lt;x:DataAxisEmpty/&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;EmployeeID&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Name&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Alias&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Title&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;MaritalStatus&lt;/x:Name&gt;
   &lt;x:Orientation&gt;Column&lt;/x:Orientation&gt;
   &lt;x:Position&gt;2&lt;/x:Position&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
   &lt;x:Expanded/&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;NewIC&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;City&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;State&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Race&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Religion&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Sex&lt;/x:Name&gt;
   &lt;x:Orientation&gt;Column&lt;/x:Orientation&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
   &lt;x:Expanded/&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Birthday&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Citizenship&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;EPF&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Socso&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;IncomeTaxNo&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Organisation&lt;/x:Name&gt;
   &lt;x:Orientation&gt;Page&lt;/x:Orientation&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Division&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Department&lt;/x:Name&gt;
   &lt;x:Orientation&gt;Row&lt;/x:Orientation&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
   &lt;x:Expanded/&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Section&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Line&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;CostCentre&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;TMSID&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;JobGrade&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;JobTitle&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Type&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;DateJoin&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;DateConfirm&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;ResignStatus&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;ResignDate&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;PresentAddr1&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;PresentAddr2&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;PresentPostCode&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;PresentCity&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;PresentState&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;PresentCountry&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;PresentTelNo&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;PermanentAddr1&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;PPermanentAddr2&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;PermanentPostCode&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;PermanentCity&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;PermanentState&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;PermanentCountry&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;PermanentTelNo&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Email&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;HandPhone&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Age&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;YOS&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Data&lt;/x:Name&gt;
   &lt;x:Orientation&gt;Column&lt;/x:Orientation&gt;
   &lt;x:Position&gt;-1&lt;/x:Position&gt;
   &lt;x:DataField/&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotData&gt;
   &lt;x:Top&gt;0.0&lt;/x:Top&gt;
   &lt;x:TopOffset&gt;0&lt;/x:TopOffset&gt;
   &lt;x:Left&gt;0.0.0&lt;/x:Left&gt;
   &lt;x:LeftOffset&gt;0&lt;/x:LeftOffset&gt;
   &lt;x:LeafColumnMember&gt;
    &lt;x:Path&gt;!.FEMALE&lt;/x:Path&gt;
    &lt;x:SeqNum&gt;55&lt;/x:SeqNum&gt;
    &lt;x:Expanded/&gt;
   &lt;/x:LeafColumnMember&gt;
   &lt;x:SeqNum&gt;33&lt;/x:SeqNum&gt;
   &lt;x:Expanded/&gt;
  &lt;/x:PivotData&gt;
  &lt;x:PivotView&gt;
   &lt;x:IsNotFiltered/&gt;
   &lt;x:Label Style='font-family:Arial;font-size:10pt;background:royalblue'&gt;
    &lt;x:Caption&gt;Contractor Ratio&lt;/x:Caption&gt;
   &lt;/x:Label&gt;
  &lt;/x:PivotView&gt;
 &lt;/x:PivotTable&gt;
&lt;/xml&gt;" />
</object>
</div>
    </form>
</body>
</html>
