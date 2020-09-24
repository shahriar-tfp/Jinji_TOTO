
<html>
	<head>
		<title>Logon</title>
		<style type="text/css"> <!-- .logonStyle { font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 8pt; font-style: normal; font-weight: bold}
	--></style>
	</head>
	<body onkeypress="if (event.keyCode==13){frmLogon.submit()}" vLink="#666666" aLink="#666666" link="#666666" bgColor="#d1d9ad" leftMargin="0" topMargin="0" onload="document.all('rdUsername').focus()" marginheight="0" marginwidth="0">
		<form id="frmLogon" action='<%=Session("rdLogonReturnUrl") %>' method="post">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr vAlign="center" align="middle">
					<td>
						<table height="230" cellSpacing="0" cellPadding="1" width="400" border="0">
							<tr vAlign="center" align="middle" bgColor="#9ccb00">
								<td height="239">
									<table height="230" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td height="62">
												<div align="center"><font face="Verdana, Arial, Helvetica, sans-serif" color="#ffffff" size="4">Logon</font></div>
											</td>
										</tr>
										<tr vAlign="center" align="middle">
											<td bgColor="#ffffff" height="148">
												<TABLE cellSpacing="0" cellPadding="3" width="340" border="0">
													<tr>
														<td class="logonStyle" vAlign="center" align="right" width="40%">Username:</td>
														<td width="60%"><font face="Verdana, Arial, Helvetica, sans-serif" size="2"><input id="rdUsername" type="text" size="20" name="rdUsername"/><input id="rdFormLogon" type="hidden" name="rdFormLogon" value="True"/>&#160;
															</font>
														</td>
													</tr>
													<tr>
														<td class="logonStyle" vAlign="center" align="right" width="40%">Password:</td>
														<td width="60%"><font face="Verdana, Arial, Helvetica, sans-serif" size="2"><input id="rdPassword" type="password" size="20" name="rdPassword"/>
															</font>
														</td>
													</tr>
													<tr>
														<td vAlign="center" align="right" width="40%" height="23">&#160;</td>
														<td vAlign="bottom" width="60%" height="23"><input id="Submit1" type="submit" value="Logon" name="Submit1"/>
														</td>
													</tr>
													<tr>
														<td vAlign="center" align="middle" width="40%" colSpan="2" height="27"><font class="logonStyle" color="#ff0000" valign="bottom"><%=Session("rdLogonFailMessage") %>
															</font>
														</td>
													</tr>
												</TABLE>
											</td>
										</tr>
									</table>
									&#160;
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
