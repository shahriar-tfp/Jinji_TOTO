<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Excel_Pivottable_Headcount_Analysis.aspx.vb" Inherits="PTtable_Excel_Pivottable_Headcount_Analysis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="div" runat="server" visible="false">
        <p>
<object classid="clsid:0002E55A-0000-0000-C000-000000000046" id="PivotTable1" style="width: 566px">
	<param name="XMLData" value="&lt;xml xmlns:x=&quot;urn:schemas-microsoft-com:office:excel&quot;&gt;
 &lt;x:PivotTable&gt;
  &lt;x:OWCVersion&gt;11.0.0.6555         &lt;/x:OWCVersion&gt;
  &lt;x:DisplayScreenTips/&gt;
  &lt;x:CubeProvider&gt;msolap.2&lt;/x:CubeProvider&gt;
  &lt;x:DisplayFieldList/&gt;
  &lt;x:FieldListTop&gt;352&lt;/x:FieldListTop&gt;
  &lt;x:FieldListLeft&gt;1070&lt;/x:FieldListLeft&gt;
  &lt;x:FieldListBottom&gt;731&lt;/x:FieldListBottom&gt;
  &lt;x:FieldListRight&gt;1270&lt;/x:FieldListRight&gt;
  &lt;x:CacheDetails/&gt;
  &lt;x:ConnectionString&gt;Provider=SQLOLEDB.1;Password=_pwd_;Persist Security Info=True;User ID=_uid_;Initial Catalog=_database_;Data Source=_server_;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;Workstation ID=ISSACCOM;Use Encryption for Data=False;Tag with column collation when possible=False&lt;/x:ConnectionString&gt;
  &lt;x:DataMember&gt;&amp;quot;_database_&amp;quot;.&amp;quot;dbo&amp;quot;.&amp;quot;Excel_Head_Count_Analysis_Vw_org&amp;quot;&lt;/x:DataMember&gt;
  &lt;x:Name&gt;Microsoft Office PivotTable 11.0&lt;/x:Name&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;EmployeeID&lt;/x:Name&gt;
   &lt;x:PLDataOrientation/&gt;
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
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
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
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
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
   &lt;x:AllIncludeExclude&gt;Exclude&lt;/x:AllIncludeExclude&gt;
   &lt;x:IncludedValue&gt;JAVA [TFP Solutions Berhad]&lt;/x:IncludedValue&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Division&lt;/x:Name&gt;
   &lt;x:Orientation&gt;Row&lt;/x:Orientation&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
   &lt;x:Expanded/&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Department&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
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
   &lt;x:PLDataOrientation/&gt;
   &lt;x:PLPosition&gt;2&lt;/x:PLPosition&gt;
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
   &lt;x:Orientation&gt;Column&lt;/x:Orientation&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
   &lt;x:Expanded/&gt;
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
   &lt;x:Name&gt;Date Join&lt;/x:Name&gt;
   &lt;x:SourceName&gt;DateJoin&lt;/x:SourceName&gt;
   &lt;x:FilterCaption&gt;DateJoin&lt;/x:FilterCaption&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
   &lt;x:GroupedWidth&gt;97&lt;/x:GroupedWidth&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Resign Date&lt;/x:Name&gt;
   &lt;x:SourceName&gt;ResignDate&lt;/x:SourceName&gt;
   &lt;x:FilterCaption&gt;ResignDate&lt;/x:FilterCaption&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
   &lt;x:AllIncludeExclude&gt;Include&lt;/x:AllIncludeExclude&gt;
   &lt;x:ExcludedValue&gt;01/01/1900&lt;/x:ExcludedValue&gt;
   &lt;x:GroupedWidth&gt;101&lt;/x:GroupedWidth&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Date Confirm&lt;/x:Name&gt;
   &lt;x:SourceName&gt;DateConfirm&lt;/x:SourceName&gt;
   &lt;x:FilterCaption&gt;DateConfirm&lt;/x:FilterCaption&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
   &lt;x:AllIncludeExclude&gt;Include&lt;/x:AllIncludeExclude&gt;
   &lt;x:ExcludedValue&gt;01/01/1900&lt;/x:ExcludedValue&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;ResignStatus&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarWChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
   &lt;x:AllIncludeExclude&gt;Include&lt;/x:AllIncludeExclude&gt;
   &lt;x:ExcludedValue&gt;RESIGN&amp;amp;PAID&lt;/x:ExcludedValue&gt;
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
   &lt;x:Name&gt;Age By Years&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Age By Month&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;YOS By Year&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;YOS By Month&lt;/x:Name&gt;
   &lt;x:EncodedType&gt;adVarChar&lt;/x:EncodedType&gt;
   &lt;x:CompareOrderedMembersBy&gt;UniqueName&lt;/x:CompareOrderedMembersBy&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Data&lt;/x:Name&gt;
   &lt;x:Orientation&gt;Column&lt;/x:Orientation&gt;
   &lt;x:Position&gt;-1&lt;/x:Position&gt;
   &lt;x:DataField/&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PivotField&gt;
   &lt;x:Name&gt;Count of EmployeeID (2)&lt;/x:Name&gt;
   &lt;x:PLName&gt;Total5&lt;/x:PLName&gt;
   &lt;x:TotalNumber&gt;4&lt;/x:TotalNumber&gt;
   &lt;x:Orientation&gt;Data&lt;/x:Orientation&gt;
   &lt;x:Position&gt;1&lt;/x:Position&gt;
   &lt;x:ParentField&gt;EmployeeID&lt;/x:ParentField&gt;
   &lt;x:Function&gt;Count&lt;/x:Function&gt;
  &lt;/x:PivotField&gt;
  &lt;x:PLTotal&gt;
   &lt;x:Name&gt;Total1&lt;/x:Name&gt;
   &lt;x:Caption&gt;Count of DateJoin&lt;/x:Caption&gt;
   &lt;x:ParentField&gt;Date Join&lt;/x:ParentField&gt;
   &lt;x:TotalNumber&gt;0&lt;/x:TotalNumber&gt;
   &lt;x:Operator&gt;Count&lt;/x:Operator&gt;
  &lt;/x:PLTotal&gt;
  &lt;x:PLTotal&gt;
   &lt;x:Name&gt;Total2&lt;/x:Name&gt;
   &lt;x:Caption&gt;Count of EmployeeID&lt;/x:Caption&gt;
   &lt;x:ParentField&gt;EmployeeID&lt;/x:ParentField&gt;
   &lt;x:TotalNumber&gt;1&lt;/x:TotalNumber&gt;
   &lt;x:Operator&gt;Count&lt;/x:Operator&gt;
  &lt;/x:PLTotal&gt;
  &lt;x:PLTotal&gt;
   &lt;x:Name&gt;Total3&lt;/x:Name&gt;
   &lt;x:Caption&gt;Count of Resign Date&lt;/x:Caption&gt;
   &lt;x:ParentField&gt;Resign Date&lt;/x:ParentField&gt;
   &lt;x:TotalNumber&gt;2&lt;/x:TotalNumber&gt;
   &lt;x:Operator&gt;Count&lt;/x:Operator&gt;
  &lt;/x:PLTotal&gt;
  &lt;x:PLTotal&gt;
   &lt;x:Name&gt;Total4&lt;/x:Name&gt;
   &lt;x:Caption&gt;Count of Date Confirm&lt;/x:Caption&gt;
   &lt;x:ParentField&gt;Date Confirm&lt;/x:ParentField&gt;
   &lt;x:TotalNumber&gt;3&lt;/x:TotalNumber&gt;
   &lt;x:Operator&gt;Count&lt;/x:Operator&gt;
  &lt;/x:PLTotal&gt;
  &lt;x:PivotData&gt;
   &lt;x:Top&gt;0.0&lt;/x:Top&gt;
   &lt;x:TopOffset&gt;0&lt;/x:TopOffset&gt;
   &lt;x:Left&gt;0.0&lt;/x:Left&gt;
   &lt;x:LeftOffset&gt;0&lt;/x:LeftOffset&gt;
   &lt;x:LeafRowMember&gt;
    &lt;x:Path&gt;!.DIV00 (JAVA) [Group Finance &amp;amp; Admin Division]&lt;/x:Path&gt;
    &lt;x:SeqNum&gt;172&lt;/x:SeqNum&gt;
   &lt;/x:LeafRowMember&gt;
   &lt;x:LeafRowMember&gt;
    &lt;x:Path&gt;!.DIV02 (JAVA) [Group Operations Division]&lt;/x:Path&gt;
    &lt;x:SeqNum&gt;173&lt;/x:SeqNum&gt;
   &lt;/x:LeafRowMember&gt;
   &lt;x:SeqNum&gt;100&lt;/x:SeqNum&gt;
   &lt;x:Expanded/&gt;
  &lt;/x:PivotData&gt;
 &lt;/x:PivotTable&gt;
&lt;/xml&gt;" />
</object>
<object classid="clsid:0002E55D-0000-0000-C000-000000000046" id="ChartSpace1" class="style1">
	<param name="XMLData" value="&lt;xml xmlns:x=&quot;urn:schemas-microsoft-com:office:excel&quot;&gt;
 &lt;x:ChartSpace&gt;
  &lt;x:OWCVersion&gt;11.0.0.6555         &lt;/x:OWCVersion&gt;
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
  &lt;x:Value&gt;
   &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
   &lt;x:Data&gt;Total6&lt;/x:Data&gt;
  &lt;/x:Value&gt;
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
  &lt;x:Chart&gt;
   &lt;x:PlotArea&gt;
    &lt;x:Graph&gt;
     &lt;x:SubType&gt;Clustered&lt;/x:SubType&gt;
     &lt;x:Type&gt;Column&lt;/x:Type&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;10 [10]&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;10 [10]&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;0&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:Value&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;Total6&lt;/x:Data&gt;
      &lt;/x:Value&gt;
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
      &lt;x:Identifier&gt;!.10 [10]&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;11 [11]&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;11 [11]&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;1&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:Value&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;Total6&lt;/x:Data&gt;
      &lt;/x:Value&gt;
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
      &lt;x:Identifier&gt;!.11 [11]&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;12 [12]&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;12 [12]&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;2&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:Value&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;Total6&lt;/x:Data&gt;
      &lt;/x:Value&gt;
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
      &lt;x:Identifier&gt;!.12 [12]&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;13 [13]&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;13 [13]&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;3&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:Value&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;Total6&lt;/x:Data&gt;
      &lt;/x:Value&gt;
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
      &lt;x:Identifier&gt;!.13 [13]&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;15 [15]&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;15 [15]&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;4&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:Value&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;Total6&lt;/x:Data&gt;
      &lt;/x:Value&gt;
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
      &lt;x:Identifier&gt;!.15 [15]&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;17 [17]&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;17 [17]&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;5&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:Value&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;Total6&lt;/x:Data&gt;
      &lt;/x:Value&gt;
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
      &lt;x:Identifier&gt;!.17 [17]&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;18 [18]&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;18 [18]&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;6&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:Value&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;Total6&lt;/x:Data&gt;
      &lt;/x:Value&gt;
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
      &lt;x:Identifier&gt;!.18 [18]&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;19 [19]&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;19 [19]&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;7&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:Value&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;Total6&lt;/x:Data&gt;
      &lt;/x:Value&gt;
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
      &lt;x:Identifier&gt;!.19 [19]&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;5 [5]&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;5 [5]&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;8&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:Value&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;Total6&lt;/x:Data&gt;
      &lt;/x:Value&gt;
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
      &lt;x:Identifier&gt;!.5 [5]&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;6 [6]&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;6 [6]&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;9&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:Value&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;Total6&lt;/x:Data&gt;
      &lt;/x:Value&gt;
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
      &lt;x:Identifier&gt;!.6 [6]&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;7 [7]&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;7 [7]&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;10&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:Value&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;Total6&lt;/x:Data&gt;
      &lt;/x:Value&gt;
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
      &lt;x:Identifier&gt;!.7 [7]&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;8 [8]&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;8 [8]&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;11&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:Value&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;Total6&lt;/x:Data&gt;
      &lt;/x:Value&gt;
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
      &lt;x:Identifier&gt;!.8 [8]&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Series&gt;
      &lt;x:FormatMap&gt;
      &lt;/x:FormatMap&gt;
      &lt;x:Name&gt;9 [9]&lt;/x:Name&gt;
      &lt;x:Caption&gt;
       &lt;x:DataSourceIndex&gt;-1&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;&amp;quot;9 [9]&amp;quot;&lt;/x:Data&gt;
      &lt;/x:Caption&gt;
      &lt;x:Index&gt;12&lt;/x:Index&gt;
      &lt;x:Category&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
      &lt;/x:Category&gt;
      &lt;x:Value&gt;
       &lt;x:DataSourceIndex&gt;0&lt;/x:DataSourceIndex&gt;
       &lt;x:Data&gt;Total6&lt;/x:Data&gt;
      &lt;/x:Value&gt;
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
      &lt;x:Identifier&gt;!.9 [9]&lt;/x:Identifier&gt;
     &lt;/x:Series&gt;
     &lt;x:Dimension&gt;
      &lt;x:ScaleID&gt;153124344&lt;/x:ScaleID&gt;
      &lt;x:Index&gt;Categories&lt;/x:Index&gt;
     &lt;/x:Dimension&gt;
     &lt;x:Dimension&gt;
      &lt;x:ScaleID&gt;153124548&lt;/x:ScaleID&gt;
      &lt;x:Index&gt;Value&lt;/x:Index&gt;
     &lt;/x:Dimension&gt;
     &lt;x:Dimension&gt;
      &lt;x:ScaleID&gt;153124140&lt;/x:ScaleID&gt;
      &lt;x:Index&gt;FormatValue&lt;/x:Index&gt;
     &lt;/x:Dimension&gt;
     &lt;x:Overlap&gt;0&lt;/x:Overlap&gt;
     &lt;x:GapWidth&gt;150&lt;/x:GapWidth&gt;
     &lt;x:FirstSliceAngle&gt;0&lt;/x:FirstSliceAngle&gt;
    &lt;/x:Graph&gt;
    &lt;x:Axis&gt;
     &lt;x:AxisID&gt;153124752&lt;/x:AxisID&gt;
     &lt;x:ScaleID&gt;153124344&lt;/x:ScaleID&gt;
     &lt;x:Type&gt;Category&lt;/x:Type&gt;
     &lt;x:MajorTick&gt;Outside&lt;/x:MajorTick&gt;
     &lt;x:MinorTick&gt;None&lt;/x:MinorTick&gt;
     &lt;x:Placement&gt;Bottom&lt;/x:Placement&gt;
     &lt;x:GroupingEnum&gt;Auto&lt;/x:GroupingEnum&gt;
    &lt;/x:Axis&gt;
    &lt;x:Axis&gt;
     &lt;x:AxisID&gt;153125476&lt;/x:AxisID&gt;
     &lt;x:ScaleID&gt;153124548&lt;/x:ScaleID&gt;
     &lt;x:Type&gt;Value&lt;/x:Type&gt;
     &lt;x:Number&gt;
      &lt;x:FormatString&gt;General&lt;/x:FormatString&gt;
     &lt;/x:Number&gt;
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
   &lt;x:ScaleID&gt;153124344&lt;/x:ScaleID&gt;
  &lt;/x:Scaling&gt;
  &lt;x:Scaling&gt;
   &lt;x:ScaleID&gt;153124548&lt;/x:ScaleID&gt;
  &lt;/x:Scaling&gt;
  &lt;x:Scaling&gt;
   &lt;x:ScaleID&gt;153124140&lt;/x:ScaleID&gt;
  &lt;/x:Scaling&gt;
 &lt;/x:ChartSpace&gt;
&lt;/xml&gt;" />
	<param name="ScreenUpdating" value="-1" />
	<param name="EnableEvents" value="-1" />
</object>
</p>
    </div>
    </form>
</body>
</html>
