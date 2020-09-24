
function rdMovePanel(sPanelID, sDirection) {
	//Record the panel movement, with the first character of these directions: Left, Right, Up, Down.
	rdPanelSetCookie("rdMovePanel", sPanelID + ":" + sDirection)
	location.href=location.href 
}

function rdCollapsePanel(sPanelID) {
	rdPanelSetCookie("rdDashboardPanelCollapsed-" + sPanelID, "True")
	location.href=location.href  
}

function rdExpandPanel(sPanelID) {
	rdPanelSetCookie("rdDashboardPanelCollapsed-" + sPanelID, "False")
	location.href=location.href  
}



function rdPanelSetCookie(sName, sValue)
{
	//Get a date a year from now.
	var expire,expireString;
	var month,year;
	expire=new Date();
	month = expire.getMonth();
	year  = expire.getFullYear();

	//expire.setMonth(month) + 1;
	expire.setFullYear(year + 1);
	expireString = expire.toGMTString();

	document.cookie = sName + "=" + encodeURI(sValue) + "; expires=" + expireString + ";Path=/"
	//The Path is necessary so that this cookie can be deleted on the .NET side.
}
