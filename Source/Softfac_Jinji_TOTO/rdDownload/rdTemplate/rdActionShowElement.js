
function ShowElement(sParentId,sElementId,sAction) {
	var sIds = sElementId.split(",")
	for (k=0; k < sIds.length; k++) {

		var sId = sIds[k]
		//When in a data table, the sParentID will have a row number.
		//It gets appended to the ID of the element so that only that row is affected.
		if (sParentId) {
			if (sParentId.indexOf("_Row") != -1) {
			    if (sId.indexOf("_Row") == -1) {
				    sId = sId + "_Row" + sParentId.substr(sParentId.indexOf("_Row") + 4)
			    }
			}
		}
		var c = document.getElementById(sId);
		
		if (c==null) {
			if (sId.indexOf("_Row") != -1) {
				c = document.getElementById(sId.substr(0,sId.indexOf("_Row")));
			}
		}
		
		if (c==null) {
			alert ("There was an error while processing your request.  The ID value " + sId + " was not found.")
			return
		}
		
		if(c.nodeName == "COL" && navigator.product == "Gecko" && navigator.productSub && navigator.productSub > "20041010" && (navigator.userAgent.indexOf("rv:1.8") != -1 || navigator.userAgent.indexOf("rv:1.9") != -1)) {
			//Allow table column hiding for Mozilla.
			c.style.display=""
			if (sAction=="Show"){
				c.style.visibility="";
			}else if (sAction=="Hide") {
				c.style.visibility="collapse";
			} else {
				c.style.visibility=(c.style.visibility=="" ? "collapse":"");  //Toggle.
			}
		} else {
			if (sAction=="Show"){
				c.style.display="";
			}else if (sAction=="Hide") {
				c.style.display="none";
			} else {
				c.style.display=(c.style.display=="" ? "none":"");  //Toggle.
			}
		}
		
		
		var rdShowElementHistory = document.getElementById("rdShowElementHistory")
		if (rdShowElementHistory) {
			rdShowElementHistory.value = rdShowElementHistory.value + sId + "=" + (c.style.display=="" ? "Show":"Hide") + ","
		}
		
		if (c.style.display != "none") {
			
			//Special handling for any IFrame subelements.
			//Set the SRC attribute of all subordinate IFrames so that the requested pages are downloaded now.
			
			cFrames = c.getElementsByTagName("IFRAME");
			for (var i = 0; i < cFrames.length; i++) 
			{
				var cFrame = cFrames[i];
				if (isParentVisible(cFrame,c)) 
				{
					if (true) 
					{
						var sSrc = cFrame.getAttribute("HiddenSource");
						if (sSrc != null) {   //There is no HiddenSource if the element was initiall visible.
							if (cFrame.getAttribute("src") == null) {   //For nonIE
								cFrame.setAttribute("src", sSrc + "&rdRnd=" + Math.floor(Math.random() * 100000));
							}										    //For IE.
							if (cFrame.getAttribute("src").indexOf(sSrc) == -1) {
								cFrame.setAttribute("src", sSrc + "&rdRnd=" + Math.floor(Math.random() * 100000));
							}
						}
					} 
					else {
						if (cFrame.height < 2) {
							cFrame.src = cFrame.src;  // The frame hasn't been shown yet.  Refresh it.
						}
					}
				}
			}
		}

		//More special IFrame handling.  If this page is in a frame,
		//the frame needs to be resized from the parent window.
		if (frameElement) {
			if (frameElement.contentWindow) {
				if (parent.iframeResize) {
					parent.iframeResize(frameElement)
				}
			}
		}

	} //Next ID.
	
	if (typeof window.rdRepositionSliders != 'undefined') {
		//Move CellColorSliders, if there are any.
		rdRepositionSliders()
	}
	
}


function isParentVisible(cChild,cShowing) {
	// See if there are any parent elements, above the element that we're showing,
	// that are invisible.  Don't want to load an IncludeFrame in that case.
	//var cParent = cChild.parentElement
	var cParent = cChild.parentNode
	while (cParent.id != cShowing.id) {
		if (cParent.style.display == "none") {
			return false 
		} 
		cParent = cParent.parentNode
	}
	return true
}


function rdShowElementsFromHistory() {
	var hiddenShowElementHistory = document.getElementById("rdShowElementHistory")
	if (hiddenShowElementHistory) {
		var sHistory = hiddenShowElementHistory.value
		var sEvents = sHistory.split(",")
		for (i=0; i < sEvents.length; i++) {
			var sElementID = sEvents[i].split("=")[0]
			var sAction = sEvents[i].split("=")[1]
			if (document.getElementById(sElementID)) {
				ShowElement(null,sElementID,sAction)
			}
		}
		hiddenShowElementHistory.value = sHistory
	}
}

function rdColumnDisplayVisibility() {
	if(navigator.product == "Gecko" && navigator.productSub && navigator.productSub > "20041010" && (navigator.userAgent.indexOf("rv:1.8") != -1 || navigator.userAgent.indexOf("rv:1.9") != -1)) {
		var cCols = document.getElementsByTagName("COL")
		for (var i = 0; i < cCols.length; i++) {
		    if (cCols[i].style.display == "none") {
			    cCols[i].style.display = null
			    cCols[i].style.visibility = "collapse"
		    }
		}
	}
}

