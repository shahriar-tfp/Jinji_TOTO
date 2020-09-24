<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>JINJI : Login Page </title>
<meta name="generator" content="editplus" />
<meta name="author" content="" />
<meta name="keywords" content="" />
<meta name="description" content="" />
<link id="pageCss1" runat="server" rel="stylesheet" type="text/css" />
<link id="pageCss2" runat="server" type="text/css" rel="stylesheet" />
<style type="text/css">
body, img, div, table, td {
	behavior: url(images/theme1/iepngfix.htc);
}
</style>
<script type="text/javascript" language="javascript">
        window.onload = maxWindow;
//        window.onbeforeunload = confirmExit;
    function confirmExit()
    {
        return confirm('Are you sure you want to exit?');
    }
    
    function maxWindow()
    {
        window.moveTo(0,0);
        if (document.all)
        {
            top.window.resizeTo(screen.availWidth,screen.availHeight);
        }
        else if (document.layers||document.getElementById)
        {
            if (top.window.outerHeight<screen.availHeight||top.window.outerWidth<screen.availWidth)
            {
                top.window.outerHeight = screen.availHeight;
                top.window.outerWidth = screen.availWidth;
            }
        }
    }
</script>
<script language="javascript" type="text/javascript">
<!-- hide from older browser
function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}

function RefreshPage(){
document.location=document.location;
}

//-->
</script>
</head>
<body>
<form id="form1" runat="server">
<div id="divLogin" runat="server">
<table border="0" cellpadding="0" cellspacing="0" width="510">
	<tr style="height:5px">
		<td align="right" valign="top" style="width:5px">
		    <img id="imgLayout01" runat="server" border="0" width="5" height="5" vspace="0" hspace="0" src="" alt="" /><br/>
		</td>
		<td align="right" valign="top" style="width:9px">
		    <img id="imgLayout02" runat="server" border="0" width="9" height="5" vspace="0" hspace="0" src="" alt="" /><br/>
		</td>
		<td id="tdLayout01" runat="server">
		    <img id="imgLayout03" runat="server" border="0" vspace="0" hspace="0" height="5" width="500" src="" alt="" /><br/>
		</td>
		<td align="left" valign="top" style="width:9px">
		    <img id="imgLayout04" runat="server" border="0" width="9" height="5" vspace="0" hspace="0" src="" alt="" /><br/>
		</td>
		<td align="left" valign="top" style="width:5px">
		    <img id="imgLayout05" runat="server" border="0" width="5" height="5" vspace="0" hspace="0" src="" alt="" /><br/>
		</td>
	</tr>

	<tr>
		<td align="right" valign="top" style="width:5px">
		    <img id="imgLayout06" runat="server" border="0" width="5" height="9" vspace="0" hspace="0" src="" alt="" /><br/>
		</td>
		<td width="100%" rowspan="3" colspan="3" valign="top">
		<table border="0" cellpadding="0" cellspacing="0" width="100%" class="login_background">
			<tr style="height:9px">
				<td align="left" valign="top" style="width:9px">
				    <img id="imgLayout07" runat="server" border="0" width="9" height="9" vspace="0" hspace="0" src="" alt="" /><br/>
				</td>
				<td id="tdLayout02" runat="server" align="left" valign="top" >
				    <img id="imgLayout08" runat="server" border="0" width="482" height="9" vspace="0" hspace="0" src="" alt="" /><br/>
				</td>
				<td align="right" valign="top" style="width:9px">
				    <img id="imgLayout09" runat="server" border="0" width="9" height="9" vspace="0" hspace="0" src="" alt="" /><br/>
				</td>
			</tr>

			<tr style="height:330px">
				<td id="tdLayout03" runat="server" align="left" valign="top" >
				    <img id="imgLayout10" runat="server" border="0" width="9" height="330" vspace="0" hspace="0" src="" alt="" /><br/>
				</td>
				<td align="left" valign="top" width="100%">
				<table border="0" cellpadding="0" cellspacing="0" width="100%">
					<tr>
						<td colspan="2" align="right" valign="top" width="100%">
						    <img id="imgLayout11" runat="server" border="0" width="241" height="137" vspace="10" hspace="20" alt="Softfac Human Capital Resources Partner" /><br/>
						</td>
					</tr>
					<tr>
						<td align="left" valign="top" width="30%">
						    <img id="imgLayout12" runat="server" border="0" width="150" height="100" vspace="0" hspace="0" src="" alt="" /><br/>
						</td>
						<td align="left" valign="middle" width="70%">
						<asp:label id="lblTitle" runat="server" CssClass="wordstyle11"></asp:label>
						<br/><br/>
						<table border="0" cellpadding="0" cellspacing="0" width="95%">
						    <asp:Panel id="pnlEmp" runat="server" Visible="false">
							<tr>
								<td align="left">
								    <asp:label id="lblEmployeeID" runat="server" Text="User/Employee Code" Width="130px" CssClass="wordstyle3"></asp:label>
								</td>
								<td align="left">
								    <asp:textbox id="txtEmployeeID" tabIndex="1" runat="server" Width="150px" CssClass="wordstyle3"></asp:textbox>
								</td>
							</tr>
							</asp:Panel>
							<asp:Panel ID="pnlPass" runat="server" Visible="false">
							<tr>
								<td align="left">
								    <asp:label id="lblPassword" runat="server" Width="130px" CssClass="wordstyle3" Text="Password"></asp:label>
								</td>
								<td align="left">
								    <asp:textbox id="txtPassword" tabIndex="2" runat="server" Width="150px" TextMode="Password" CssClass="wordstyle3"></asp:textbox>
								</td>
							</tr>
							</asp:Panel>
                            <asp:Panel ID="pnlNewPass" runat="server" Visible="false">
							<tr>
								<td align="left">
								    <asp:label id="lblNewPassword" runat="server" Width="130px" CssClass="wordstyle3" Text="New Password" Font-Bold="true"></asp:label>
								</td>
								<td align="left">
								    <asp:textbox id="txtNewPassword" tabIndex="2" runat="server" Width="150px" CssClass="wordstyle3" TextMode="Password"></asp:textbox>
								</td>
							</tr>
                            <tr>
								<td align="left">
								    <asp:label id="lblCNewPassword" runat="server" Width="130px" CssClass="wordstyle3" Text="Confirm New Password"></asp:label>
								</td>
								<td align="left">
								    <asp:textbox id="txtCNewPassword" tabIndex="2" runat="server" Width="150px" CssClass="wordstyle3" TextMode="Password"></asp:textbox>
								</td>
							</tr>
							</asp:Panel>
                            <asp:Panel ID="pnlCom" runat="server" Visible="false">
							<tr>
								<td align="left">
								    <asp:label id="lblCompany" runat="server" Width="130px" CssClass="wordstyle3" Text="Company"></asp:label>
								</td>
								<td align="left">
								    <asp:textbox id="txtCompany" tabIndex="2" runat="server" Width="150px" CssClass="wordstyle3"></asp:textbox>
								    <asp:dropdownlist id="ddlCompany" tabIndex="2" runat="server" Width="150px" CssClass="wordstyle3"></asp:dropdownlist>
								</td>
							</tr>
							</asp:Panel>
							<tr>
								<td align="left"><b>&nbsp;</b><br /></td>
								<td align="left">
								    <asp:ImageButton id="imgBtnLogin" tabIndex="5" runat="server"></asp:ImageButton>
							        <asp:ImageButton id="imgBtnSubmit" tabIndex="6" runat="server"></asp:ImageButton>
							        <asp:ImageButton id="imgBtnYes" tabIndex="7" runat="server"></asp:ImageButton>
							        <asp:ImageButton id="imgBtnNo" tabIndex="8" runat="server"></asp:ImageButton>
							        <asp:ImageButton id="imgBtnOk" tabIndex="8" runat="server"></asp:ImageButton>
							        &nbsp;<a href="forgot_password.aspx" style="vertical-align:top; text-decoration:none">[ Forgot password ]</a><br />
								</td>
							</tr>
							<asp:Panel ID="pnlResult" runat="server" Visible="true">
							<tr>
							    <td align="left" colspan="2">
							        <asp:Label ID="lblError" runat="server" CssClass="wordstyle2"></asp:Label>
							    </td>
							</tr>
							</asp:Panel>
						</table>	
						</td>
					</tr>
					<tr>
						<td colspan="2" align="center" valign="top" width="100%" class="copyright">
						<br />Click here to change the theme:<br />
						<asp:ImageButton ID="imgBtnTheme1" runat="server" />
						<img id="imgLayout13" runat="server" border="0" width="10" height="10" vspace="0" hspace="0" src="" alt="" />
						<asp:ImageButton ID="imgBtnTheme2" runat="server" />
						<img id="imgLayout29" runat="server" border="0" width="10" height="10" vspace="0" hspace="0" src="" alt="" />
						<asp:ImageButton ID="imgBtnTheme3" runat="server" />
						<img id="imgLayout30" runat="server" border="0" width="10" height="10" vspace="0" hspace="0" src="" alt="" />
						<asp:ImageButton ID="imgBtnTheme4" runat="server" />
						<img id="imgLayout31" runat="server" border="0" width="10" height="10" vspace="0" hspace="0" src="" alt="" />
						<asp:ImageButton ID="imgBtnTheme5" runat="server" />
						<img id="imgLayout32" runat="server" border="0" width="10" height="10" vspace="0" hspace="0" src="" alt="" />
						<asp:ImageButton ID="imgBtnTheme6" runat="server" />
						
						<div id="footer">
						<asp:Label ID="lblCopyRight" CssClass="wordstyle9" runat="server">Copyright &copy; 2008 SoftFac Technology Sdn Bhd. <i>All Rights Reserved</i> Powered by <img id="imgLayout14" runat="server" border="0" width="23" height="13" alt="Softfac" style="vertical-align:middle;" /><br />
					    </asp:Label>
                        </div>
						</td>
					</tr>

				</table>
				</td>
				<td id="tdLayout04" runat="server" align="right" valign="top" >
				    <img id="imgLayout15" runat="server" border="0" width="9" height="330" vspace="0" hspace="0" src="" alt="" /><br/>
				</td>
			</tr>

			<tr style="height:9px">
				<td align="left" valign="bottom" width="9">
				    <img id="imgLayout16" runat="server" border="0" width="9" height="9" vspace="0" hspace="0" src="" alt="" /><br/>
				</td>
				<td id="tdLayout05" runat="server" align="left" valign="bottom">
				    <img id="imgLayout17" runat="server" border="0" width="482" height="9" vspace="0" hspace="0" src="" alt="" /><br/>
				</td>
				<td align="right" valign="bottom" width="9">
				    <img id="imgLayout18" runat="server" border="0" width="9" height="9" vspace="0" hspace="0" src="" alt="" /><br/>
				</td>
			</tr>
		</table>
		</td>
		<td align="left" valign="top" width="5">
		    <img id="imgLayout19" runat="server" border="0" width="5" height="9" vspace="0" hspace="0" alt=""/><br/>
		</td>
	</tr>

	<tr style="height:330px">
		<td id="tdLayout06" runat="server" align="right" valign="top" >
		    <img id="imgLayout20" runat="server" border="0" width="5" height="330" vspace="0" hspace="0" src="" alt="" /><br/>
		</td>
		<td id="tdLayout07" runat="server" align="left" valign="top" >
		    <img id="imgLayout21" runat="server" border="0" width="5" height="330" vspace="0" hspace="0" src="" alt="" /><br/>
		</td>
	</tr>

	<tr>
		<td align="right" valign="top" width="5">
		    <img id="imgLayout22" runat="server" border="0" width="5" height="9" vspace="0" hspace="0" src="" alt="" /><br/>
		</td>
		<td align="left" valign="top" width="5">
		    <img id="imgLayout23" runat="server" border="0" width="5" height="9" vspace="0" hspace="0" src="" alt="" /><br/>
		</td>
	</tr>

	<tr style="height:5px">
		<td align="right" valign="top" width="5">
		    <img id="imgLayout24" runat="server" border="0" width="5" height="5" vspace="0" hspace="0" src="" alt="" /><br/>
		</td>
		<td align="right" valign="top" width="9">
		    <img id="imgLayout25" runat="server" border="0" width="9" height="5" vspace="0" hspace="0" src="" alt="" /><br/>
		</td>
		<td id="tdLayout08" runat="server">
		    <img id="imgLayout26" runat="server" border="0" vspace="0" hspace="0" height="5" width="500" src="" alt="" /><br/>
		</td>
		<td align="left" valign="top" width="9">
		    <img id="imgLayout27" runat="server" border="0" width="9" height="5" vspace="0" hspace="0" src="" alt="" /><br/>
		</td>
		<td align="left" valign="top" width="5">
		    <img id="imgLayout28" runat="server" border="0" width="5" height="5" vspace="0" hspace="0" src="" alt="" /><br/>
		</td>
	</tr>
</table>
</div>
</form>
</body>
</html>

