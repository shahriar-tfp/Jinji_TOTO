<%@ Page language="VB" Inherits="Forum.NewMessage" CodeFile="NewMessage.aspx.vb" ValidateRequest="false" AutoEventWireup="true"  %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd html 4.0 transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" >
	<head>
		<title>Add New Comment </title>
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
			var elem = document.getElementById("table2");
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
	<body onload="setPosition()">
		<form id="Form1" method="post" runat="server">
		<input type="text" id="tocctxt" name="cctotxt" runat="server"  visible ="false" />
		<input type="text" id="showusertxt" name="showusertxt"  runat="server"  visible ="false" />
		<input type="text" id="employeename" name="employeename" runat="server" visible="false" />
		<input type="text" id="foldernametxt" name="foldernametxt" runat="server" visible="false" />
		<input type="text" id="sessionidtxt" name="sessionidtxt" value="" runat="server" visible="false" />
		<input type="text" id="uploadtf" name="uploadtf" runat="server" value="" visible = "false" />
		<div id="showuser" class="showuser" runat="server" visible="false">		   
		    <center>
		        <div>
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
		<div align="center" style="width:100%">
			<table id="table1" border="0">
				<tr id="firsttr1" runat="server" visible="false" >
					<td>&nbsp;
					</td>
					<td>
						<asp:label id="lblStatus" runat="server" Visible="false"  CssClass="reportTitle_style">Add a comment</asp:label>
					</td>
				</tr>
				<tr>
					<td>&nbsp;
					</td>
					<td>
						<table id="table2" style="height:370;width:100%" border="0" cellspacing="1"  class="dgstyle_i">
							<tr>
								<td colspan="2"  class="wordstyle4"><b> Create New Message</b> 
										<hr/>		
								</td>
							</tr>
							<tr valign="middle">
								<td valign="middle"  align="left" width="10%">Message 
										Type:</td>
								<td valign="top"> 
								
										<input type="radio" name="MsgType" id="MsgType_4" value="4" runat="server"/>
										<label for="MsgType_4"><img alt="" title='Question' align="middle" src='../../Images/default/question.gif'/>
											Question &nbsp;&nbsp;</label> 
											
										<input type="radio" name="MsgType" id="MsgType_1" value="1" checked runat="server"/>
										<label for="MsgType_1"><img  alt=""  title='General Comment' align="middle" src='../../Images/default/general.gif'/>
											General &nbsp;&nbsp;</label> 
											
										<input type="radio" name="MsgType" id="MsgType_2" value="2" runat="server"/>
										<label for="MsgType_2"><img  alt=""  title='News' align="middle" src='../../Images/default/info.gif'/>
											News &nbsp;&nbsp;</label> 
											
										<!--<input type="radio" name="MsgType" ID="MsgType_3" value="3" runat="server">
										<label for="MsgType_8"><img title='Answer' align="absMiddle" src='images/answer.gif'>
											Answer &nbsp;&nbsp;</label> -->
											 
                                        </td>
							</tr>
							<!--<tr>
								<td align="left">&nbsp;</td>
								<td><asp:Button ID="CSUploadButton" runat="server" Text="Upload Capture Screen" Width="140px" CausesValidation="false"   CssClass="button_style" OnClick="OpenCSUpload" />
								 Click Here to Upload Capture Screen Image </td>
							</tr>-->							
							<tr>
								<td align="left">Attactment Files </td>
								<td valign="middle" >
                                    <table border="0" cellspacing="1">
                                        <tr>
                                            <td colspan="2">
                                                <asp:FileUpload ID="upload1" runat="server" Width="300px"/>
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
								<td><asp:textbox id="txtto" runat="server" Width="500px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Name required" ControlToValidate="txtto"
											font-Size="10pt"></asp:requiredfieldvalidator></td>
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
							
							<tr valign="middle">
								<td width="10%" align="left">Title</td>
								<td valign="bottom">
									<asp:textbox id="txtsubject" runat="server" Width="500px"></asp:textbox>
									<asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" font-Names="Arial" font-Size="10pt"
										ControlToValidate="txtsubject" ErrorMessage="Subject Required"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<td valign="middle" align="left">Description:</td>
								<td><asp:textbox id="txtcomment" runat="server" font-Size="10pt" Height="104px" Width="500px" TextMode="MultiLine"
											font-Names="Arial"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Comment required" ControlToValidate="txtcomment"
											font-Size="10pt"></asp:requiredfieldvalidator></td>
							</tr>
							<tr>
								<td align="left"></td>
								<td><asp:button id="Button1" CssClass="button_style" runat="server" Height="24" Text="Add Reply" onclick="Button1_Click"></asp:button>&nbsp;&nbsp;
													<asp:Button id="Button2"  CssClass="button_style" Width="85px" runat="server" Height="24px" Text="Go To Forum" CausesValidation="False" onclick="Button2_Click"></asp:Button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			</div>
		</form>
		<script language="JavaScript1.2" defer="defer" type="text/javascript" >
						editor_generate('txtcomment');
        </script>
	</body>
</html>
