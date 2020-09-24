<%@ Page Language="vb" AutoEventWireup="false" Codebehind="rdProcess.aspx.vb" Inherits="rdWeb.rdActionProcess" %>
<%@ OutputCache Duration="1" Location="None" %>
<%
Dim ap as new rdServer.ActionProcessor
Call ap.ProcessAction()
%>
