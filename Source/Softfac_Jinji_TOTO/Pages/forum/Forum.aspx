<%@ Page language="VB" Inherits="Forum.Forum" CodeFile="Forum.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" >
	<head>
		<title>Forum</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="VB" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet"/>			 
	</head>

	<body onload="InitializePage()" style="MARGIN: 0px" text="black" vlink="navy" alink="red" link="blue"  bgcolor="white">

		<script language="JavaScript" type="text/javascript">
		onerror = report;
var Selected = 1;


function OnOffPost(e)
{

   if ( !e ) e = window.event;
   var target = e.target ? e.target : e.srcElement;
   

if (!target) return;
   
 while (target.id.indexOf('LinkTrigger')<0)
 {
	//alert(target.id + target.id.indexOf('LinkTrigger')+target.parentNode);
	
     target = target.parentNode;
     if (target.id ==null) return;
     }
  if ( target.id.indexOf('LinkTrigger')<0 )
   return;
   

   if (Selected)
   {
      var body = document.getElementById(Selected + "ON");
      if (body)
         body.style.display = 'none';
      var head = document.getElementById(Selected + "OFF");
      if (head)
         head.bgColor = '#EDF8F4';
   }

   if (Selected == target.name) // just collapse
      Selected="";
   else
   {
      Selected = target.name;
      var body = document.getElementById(Selected + "ON");
      if (body)
      {
         if (body.style.display=='none')
            body.style.display='';
         else
            body.style.display = 'none';
      }
      var head = document.getElementById(Selected + "OFF");
      if (head)
         head.bgColor = '#B7DFD5';

      if ( body && head && body.style.display != 'none' )
      {
         document.body.scrollTop = FindPosition(head, "Top") - document.body.clientHeight/10;
         OpenMessage(target.name, true);
      }
   }

   if ( e.preventDefault )
      e.preventDefault();
   else
      e.returnValue = false;
   return false;
}

// does its best to make a message visible on-screen (vs. scrolled off somewhere).
function OpenMessage(msgID, bShowTop) {
   var msgHeader = document.getElementById(msgID + "OFF");
   var msgBody = document.getElementById(msgID + "ON");
   var msgTXT = document.getElementById(msgID + "TXT");
   var msgAID = document.getElementById(msgID + "AID");
   var msgIID = document.getElementById(msgID + "ID");   

   // determine scroll position of top and bottom
   var MyBody = document.body;
   var top = FindPosition(msgHeader, 'Top');
   var bottom = FindPosition(msgBody, 'Top') + msgBody.offsetHeight;

   // if not already visible, scroll to make it so
   if ( MyBody.scrollTop > top && !bShowTop)
      MyBody.scrollTop = top - document.body.clientHeight/10;
   if ( MyBody.scrollTop+MyBody.clientHeight < bottom )
      MyBody.scrollTop = bottom-MyBody.clientHeight;
   if ( MyBody.scrollTop > top && bShowTop)
      MyBody.scrollTop = top - document.body.clientHeight/10;
      
   var temptxt = msgTXT.value;
   var tempid = msgIID.value;
   var tempaid = msgAID.value;

   if (temptxt == 'new')
   {
     setOpen(tempid,tempaid);
      
      var msgDIV = document.getElementById(msgID + "IMG");      
      msgDIV.style.display = "none";              
      
       var NMnum = document.getElementById("newmessagelabel").innerHTML;
       if((NMnum -1) >=0)
       {
            var realvalue = NMnum - 1
            document.getElementById("newmessagelabel").innerHTML = realvalue;
        }
      
   }
}

// utility
function FindPosition(i,which)
{
   iPos = 0
   while (i!=null)
   {
      iPos += i["offset" + which];
      i = i.offsetParent;
   }
   return iPos
}

	var service;

	// Page initialization code, called from Body onLoad event handler
	function InitializePage() {
		service = document.getElementById("service");	
			
		// Create an instance of the web service and call it svcOlap
		service.useService("/callwebservice/forumservice.asmx?WSDL","forumservice");	
	}

	function setOpen(id,aid) {	
		// Purpose:  Call Web Service method to initialize an empty PivotTable        
		var iCallID = service.forumservice.callService(onchangeNewState, 'setValueToOpen', id, aid);
	}
	
	function onchangeNewState(result) {
		// Purpose:	This function handles the wsOLAP.InitializePivotTableXML() web service result
		var text = result.value; // result string
		//if(text=='false')
		//    alert('database error');
		// Evaluate return result
	}

function folderOpen(name)
{
    window.open("FolderList.aspx?id=" + name , "uploadlist","menubar=0,resizable=0,width=650,height=250,scrollbars=yes").focus(); 
    //alert(subname)

}
function screenOpen(name)
{
    window.open("ShowScreen.aspx?foldername=" + name + "&view=yes", "UploadAndView","menubar=0,resizable=1,width=800,height=600,scrollbars=yes").focus(); 

}
function report(message,url,line) {
    alert('Error : ' + message + ' at line ' + line + ' in ' + url);
}

// cause an <B style="COLOR: black; BACKGROUND-COLOR: #ffff66">error</B>:
		</script>
		<br />
				<input type="text" id="employeename" name="employeename" runat="server" visible="false" />
				<div id="service" style="background: url(servicesupport.htc)"></div>
		<table cellspacing="0" cellpadding="0" width="100%" border="0">
			<tbody valign="middle" align="center">
				<tr valign="top">
					<td class="ContentPane">
						<!-- Main Page Contents Start -->
						<div id="divforum" onclick="OnOffPost(event)" class="divforum">
							<table cellspacing="0" cellpadding="0" width="100%" border="0" class="wordstyle4">
								<tbody>
									<tr>
										<td width="100%">
											<form name="myform" id="myform" runat="server">
												<table id="ForumTable" cellspacing="0" cellpadding="0" width="100%"  class="wordstyle4">
													<tbody>
														<tr bgcolor="#f1f1f1">
															<td><a name="xx0xx"></a>
																<div id="divpagesize" class="divpagesize">
																<table cellpadding="2" width="100%" border="0"  class="wordstyle4">
																	<tr>
																		<td><asp:Button id="newmessagebtn" CssClass="button_style" Width="85px" runat="server" Text="New Message" onclick="newMessage"></asp:Button>
																		    &nbsp;&nbsp;<asp:Button id="btnGetMessages" CssClass="button_style" runat="server" Text="Refresh" onclick="getMessages"></asp:Button></td>
																		<td id="tdnewmessage" runat="server" align="center" ></td>
																		<td align="right">
																		    Per page&nbsp;
																			<asp:DropDownList id="txtpagesize" runat="server" AutoPostBack="true" onselectedindexchanged="txtpageSize_SelectedIndexChanged">
																				<asp:ListItem Value="5">5</asp:ListItem>
																				<asp:ListItem Value="10">10</asp:ListItem>
																				<asp:ListItem Value="20" Selected="True">20</asp:ListItem>
																				<asp:ListItem Value="30">30</asp:ListItem>
																				<asp:ListItem Value="40">40</asp:ListItem>
																				<asp:ListItem Value="50">50</asp:ListItem>
																			</asp:DropDownList>
																			<asp:Button id="btnsetpaging" CssClass="button_style" runat="server" Text="Set Pagesize" onclick="btnsetpaging_Click" Visible="false" ></asp:Button>
                                                                            <asp:Button id="btnhistory" CssClass="button_style" runat="server" Text="History" onclick="btnhistory_Click"></asp:Button>
																		</td>
																	</tr>
																</table>
																</div>
															</td>
														</tr>
														<tr bgcolor="white">
															<td>
																<table cellspacing="0" cellpadding="0" width="100%" border="0" >
																	<tbody>
																		<tr>
																			<td width="100%">
																				<div id="divtopcontent" class="divtopbottomcontent">
																				<table cellspacing="0" width="100%" cellpadding="2" border="0"  class="wordstyle4">
																					<tr >
																						<td style="width:55%;">&nbsp;&nbsp;Subject&nbsp;</td>
																						<td style="width:2%;" id="tdscreen" runat="server"  align="center"></td>
																						<td style="width:9%;" id="tdattach" runat="server"  align="center"></td>
																						<td  style="width:15%;" align="center" >User&nbsp;</td>
																						<td  style="width:19%;" align="center">Date&nbsp;</td>
																					</tr>
																				</table>
																				</div>
																			</td>
																		</tr>
																		<tr>
																			<td colspan="1"><img height="5" src="/script/images/t.gif" width="1" border="0" alt=""/>
																			</td>
																		</tr>
																		<asp:literal id="ltlPost" runat="server" oninit="ltlPost_Init"></asp:literal>
																		<tr>
																			<td colspan="1"><img height="5" src="/script/images/t.gif" width="1" border="0" alt=""/></td>
																		</tr>
																	</tbody>
																</table>
															</td>
														</tr>
														<tr>
															<td>
																<div id="divbottomcontent" class="divtopbottomcontent">
																<table cellpadding="0" width="100%" border="0">
																	<tr>
																		<td valign="middle" align="center" width="40%"  class="wordstyle4" ><asp:label id="lblPaging" runat="server">Label</asp:label></td>
																	</tr>
																</table>
																</div>
															</td>
														</tr>
													</tbody>
												</table>
											</form>
										</td>
									</tr>
								</tbody>
							</table>
						</div>
					</td>
				</tr>
			</tbody>
		</table>
		<table width='100%'>
			<tr>
				<td align="center">
				    &nbsp;
				</td>
			</tr>
		</table>
		<asp:TextBox ID="statetext" runat="server" Visible="false" Text="show"></asp:TextBox>
	</body>
</html>
