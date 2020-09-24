<%@ Page language="VB" Inherits="Forum.Reply" CodeFile="Reply.aspx.vb"  ValidateRequest="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" >
	<head>
		<title>NewMessage</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="VB" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<script language="Javascript1.2" type="text/javascript" ><!-- // load htmlarea
_editor_url = "htmlarea/";                     // URL to htmlarea files
var win_ie_ver = parseFloat(navigator.appVersion.split("MSIE")[1]);
if (navigator.userAgent.indexOf('Mac')        >= 0) { win_ie_ver = 0; }
if (navigator.userAgent.indexOf('Windows CE') >= 0) { win_ie_ver = 0; }
if (navigator.userAgent.indexOf('Opera')      >= 0) { win_ie_ver = 0; }
if (win_ie_ver >= 5.5) {
 document.write('<scr' + 'ipt src="' +_editor_url+ 'editor.js"');
 document.write(' language="Javascript1.2"></scr' + 'ipt>');  
} else { document.write('<scr'+'ipt>function editor_generate() { return false; }</scr'+'ipt>'); }
// --></script>
		<link href="../../App_Themes/hcrmStyles1.css" type="text/css" rel="stylesheet"/>	
    <script language="javascript" type="text/javascript">
    	function setPosition()
    	{
			var elem = document.getElementById("lblStatus");
			var pos = getElementPosition(elem);

			showuser.style.left = pos.x;
			showuser.style.top = pos.y;
    	}
    	function getElementPosition(theElement)
    	{  
    		var posX = 0;  
    		var posY = 0;                
    		while(theElement != null)
    		{    
    			posX += theElement.offsetLeft;    
    			posY += theElement.offsetTop;    
    			theElement = theElement.offsetParent;  
    		}                        		         
    		return {x:posX,y: posY};
    	}
    </script>				
		</head>
	<body>
		<form id="Form1" method="post" runat="server">
		<input type="text" id="tocctxt" name="cctotxt" runat="server"  visible ="false" />		
		<input type="text" id="showusertxt" name="showusertxt"  runat="server"  visible ="false" />
		<input type="text" id="sessionidtxt" name="sessionidtxt" value="" runat="server" visible="false" />
		<input type="text" id="uploadtf" name="uploadtf" runat="server" value="" visible = "false" />		
		<div id="showuser" class="showuser" runat="server" visible="false">		   
		    <center>
		        <div style="font-family:Arial Unicode MS;font-size:11px;font-weight:700;">
                    <label id="filtercodelabel">Code&nbsp;&nbsp;:&nbsp</label>
                    <input type="text" id="filtercodetxt" runat="server" class="filtertxt" />
                    <asp:Button id="filterbtn"  CssClass="button_style" runat="server" Text="Filter" CausesValidation="False" onclick="filterUserList"></asp:Button>
                    <br />
                    <label id="filternamelabel">Name&nbsp;:&nbsp;</label>
                    <input type="text" id="filternametxt" style="width:150px" runat="server" class="filtertxt" />
                    <br />		        
                    <label id="filtercompanylabel" runat="server" >Company Code&nbsp;:&nbsp;</label>
                    <input type="text" id="filtercompanytxt" style="width:100px" runat="server" class="filtertxt" />
                    <br />                    
		        </div>		

			    <asp:ListBox Rows="15" runat="server" id="ListBox1" OnSelectedIndexChanged="setUserValue1" AutoPostBack="true" Visible = "false" >
			    </asp:ListBox>
			    <asp:ListBox Rows="15" runat="server" id="ListBox2" OnSelectedIndexChanged="setUserValue2" AutoPostBack="true"  Visible = "false" >
			    </asp:ListBox>			    
			    <br />
			    <asp:Button ID="closeshowuser" runat="server" OnClick="closeShowUserDialog" Text="Close" CssClass="button_style"  CausesValidation="False" />		    
		    </center>
		</div>		

		<div align="center" style="width:100%;font-family:Arial Unicode MS;font-size:11px;font-weight:700;" >		
			<table id="table1" border="0" class="wordstyle4">
				<tr>
					<td></td>
					<td>
						<asp:Label id="lblStatus" runat="server" CssClass="reportTitle_style" Visible="false" >Add a comment</asp:Label></td>
				</tr>
				<tr>
					<td>&nbsp;
					</td>
					<td valign="top">
			            <table width="100%" border="0"  class="dgstyle_i">
				            <tr>
					            <td class="wordstyle4">
					                <asp:LinkButton ID="viewcommentlbtn" runat="server" Font-Underline="false" CausesValidation="false"  OnClick="viewcommentshow">
					                    View Comment
					                </asp:LinkButton>
							            <hr/>
										
					            </td>
				            </tr>
				            <tr>
				                <td>
                                    <table border="0" id="viewcommenttable" runat="server"  visible="false"  class="dgstyle_i">
				                        <tr>
					                        <td width="10%">Title:</td>
					                        <td><asp:label id="lbltitle" runat="server" CssClass="dgstyle_i">Label</asp:label></td>
				                        </tr>
				                        <tr>
					                        <td width="10%">Comments:</td>
					                        <td><asp:label id="lblComment" runat="server" CssClass="dgstyle_i">Label</asp:label></td>
				                        </tr>
				                        <tr>
					                        <td width="10%">Name:</td>
					                        <td><asp:label id="lblname" runat="server" CssClass="dgstyle_i">Label</asp:label></td>
				                        </tr>
				                        <tr>
					                        <td width="10%">DateAdded:</td>
					                        <td><asp:label id="lblDate" runat="server" CssClass="dgstyle_i">Label</asp:label></td>
				                        </tr>
				                        <tr>
					                        <td width="10%">Attact File: </td>
					                        <td><asp:Button ID="AttactFileButton" runat="server" Text="View And Download" OnClick="attachfile" CausesValidation="false" CssClass="button_style" Width="120px"  /></td>
				                        </tr>							                                    
                                    </table>				                
				                </td>
				            </tr>
			            </table>					            
					</td>
				</tr>

				<tr>
					<td>&nbsp;
					</td>
					<td>
						<table id="table2" width="100%" border="0"  class="dgstyle_i">
							<tr>
								<td colspan="2"  class="wordstyle4">
								    <asp:LinkButton ID="replycommentbtn1" runat="server" Font-Underline="false" OnClick="replycommentshow" CausesValidation="false" >
								        <b>Reply to&nbsp;Comment</b> 								    
								    </asp:LinkButton>
										<hr />
								</td>
							</tr>
							<tr valign="middle">
								<td valign="middle" align="left" width="10%">Message 
										Type:</td>
								<td valign="top"> <input type="radio" name="MsgType" id="MsgType_4" value="4" runat="server"/>
										<label for="MsgType_4"><img alt="" title='Question' align="middle"  src='../../Images/default/question.gif'/>
											Question &nbsp;&nbsp;</label> <input type="radio" name="MsgType" id="MsgType_1" value="1" checked runat="server"/>
										<label for="MsgType_1"><img alt="" title='General Comment' align="middle" src='../../Images/default/general.gif'/>
											General &nbsp;&nbsp;</label> <input type="radio" name="MsgType" id="MsgType_2" value="2" runat="server" onserverchange="MsgType_2_ServerChange"/>
										<label for="MsgType_2"><img alt="" title='News' align="middle" src='../../Images/default/info.gif'/> News 
											&nbsp;&nbsp;</label> <input type="radio" name="MsgType" id="MsgType_3" value="3" runat="server" onserverchange="MsgType_3_ServerChange"/>
										<label for="MsgType_8"><img alt="" title='Answer' align="middle" src='../../Images/default/answer.gif'/>
											Answer &nbsp;&nbsp;</label>  </td>
							</tr>
							<tr>
							    <td colspan="2">
							        <table id="replycommenttable" runat="server"  border="0" class="dgstyle_i" visible="false" >
							            <tr>
								            <td align="left">Attactment Files </td>
								            <td valign="middle" >
                                                <table border="0" cellspacing="1"  class="dgstyle_i">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:FileUpload ID="upload1" runat="server" Width="300px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
	                                                        <asp:ListBox Rows="3" runat="server" Width="240px" id="fileuploadListBox" >
	                                                        </asp:ListBox>                
                                                        </td>
                                                        <td>
	                                                        <asp:Button ID="fileAdd" runat="server" Text="Add" OnClick="uploadfileAdd" CausesValidation="false" CssClass="button_style"  />
	                                                        <br />
	                                                        <asp:Button ID="fileDelete" runat="server" Text="Remove" OnClick="uploadfileDelete" CausesValidation="false" CssClass="button_style"  />                
                                                        </td>
                                                    </tr>
                                                </table>
                                                 
								            </td>
							            </tr>								
							            <tr>
								            <td align="left"><asp:Button id="buttonto"  CssClass="button_style" runat="server" Height="20px" Text="To..." CausesValidation="False" OnClick="showto"></asp:Button></td>
								            <td><asp:textbox id="txtto" runat="server" Width="500px"></asp:textbox></td>
							            </tr>
							            <tr>
								            <td align="left"><asp:Button id="buttoncc"  CssClass="button_style" runat="server" Height="20px" Text="Cc..." CausesValidation="False" OnClick="showcc"></asp:Button></td>
								            <td>
									            <asp:textbox id="txtcc" runat="server" Width="500px"></asp:textbox></td>
							            </tr>
							            <tr>
								            <td align="left"><asp:Button id="buttonother"  CssClass="button_style" runat="server" Height="20px" Text="Other..." CausesValidation="False" OnClick="showother"></asp:Button></td>
								            <td>
									            <asp:textbox id="txtother" runat="server" Width="500px"></asp:textbox></td>
							            </tr>
							        
							        </table>
							    </td>
							</tr>	
											
							<tr>
								<td width="10%" valign="top" align="center">Title</td>
								<td valign="middle">
									<asp:textbox id="txtsubject" width="500px" runat="server"></asp:textbox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" font-Names="Arial" font-Size="10pt"
										ControlToValidate="txtsubject" ErrorMessage="Subject Required"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<td valign="middle" width="10%" align="center" >Reply:</td>
								<td>
										<asp:textbox id="txtcomment" runat="server" font-Names="Arial" font-Size="10pt" TextMode="MultiLine"
											width="500px" Height="104px"></asp:textbox>
										<asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" font-Size="10pt" ControlToValidate="txtcomment"
											ErrorMessage="Comment required"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<td width="10%"></td>
								<td>
													<asp:button id="Button1"  CssClass="button_style"  runat="server" Height="24px" Text="Add Reply" onclick="Button1_Click"></asp:button>&nbsp;&nbsp;
													<asp:Button id="Button2"  CssClass="button_style"  runat="server" Height="24px" Text="Back" CausesValidation="False" onclick="Button2_Click"></asp:Button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			</div>
			<script language="JavaScript1.2" defer="defer" type="text/javascript" >
						editor_generate('txtcomment');
</script>
		</form>
	</body>
</html>
