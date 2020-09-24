<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        rdMaint.Maintenance.AppStart()
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs

    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
        Session("Language") = "ENG"
        
        'DanielSong added this for integrating Reports project. 2008-08-18
        Dim sql As New clsSQL
        HttpContext.Current.Session("ConnectionString") = sql.GetConnectionString()
        HttpContext.Current.Session("ProviderString") = sql.GetProviderString()
        
        'HttpContext.Current.Session("ConnectionString") = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
        'HttpContext.Current.Session("ProviderString") = System.Configuration.ConfigurationManager.AppSettings("ProviderString")
      

        
        Call rdServer.rdSession.SessionStart()
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
</script>