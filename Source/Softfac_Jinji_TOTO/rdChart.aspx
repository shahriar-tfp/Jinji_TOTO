<%@ Page Language="vb" CodeBehind="rdChart.aspx.vb" AutoEventWireup="false" Inherits="rdWeb.rdChart" %>
<%@ Import Namespace="ChartDirector" %>
<%
If Not isNothing(Request("rdChartDef")) then 
	Dim cb as new rdServer.ChartBuilder
	Call cb.BuildChart()
else
	If Not isNothing(Request("rdDrillDownID")) then 
		'Return an image map.
		Response.Write (session(Request("rdDrillDownID")))
		Response.End
	End If	
End If
%>
