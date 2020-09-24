var isIE = false;
var req;

// retrieve XML document (reusable generic function);
// parameter is URL string (relative or complete) to
// an .xml file whose Content-Type is a valid XML
// type, such as text/xml; XML source must be from
// same domain as HTML file
function rdAjaxRequest(commandURL) {
	try {
		//var x = 1 \ 0
		var url='rdTemplate/rdAjax/rdAjax.aspx?' + commandURL
		// branch for native XMLHttpRequest object
	if (window.XMLHttpRequest) {
			req = new XMLHttpRequest();
			req.onreadystatechange = processReqChange;
			req.open("GET", url, true);
			//req.overrideMimeType('text/xml; charset=utf-8');
			req.send(null);
		// branch for IE/Windows ActiveX version
		} else if (window.ActiveXObject) {
		isIE = true;
			req = new ActiveXObject("Microsoft.XMLHTTP");
			if (req) {
				req.onreadystatechange = processReqChange;
				req.open("GET", url, true);
				req.send("nocache");
			}
		} else {
			//No XMLHttpRequest available.
			url = url.replace('rdAjaxCommand','rdAjaxAbort') 
			window.open(url,'_self')
		}
	}
	catch (e) {
		url = url.replace('rdAjaxCommand','rdAjaxAbort') 
		window.open(url,'_self')
	}
}

// handle onreadystatechange event of req object
function processReqChange() {
    // only if req shows "loaded"
    if (req.readyState == 4) {
        // only if "OK"
        if (req.status == 200) {
			if (req.responseText.indexOf('<meta name="rdDebug"') != -1) {
				document.write(req.responseText)  //Display the error.
				return
			}
			var sResponse
			if (isIE) {
				sResponse = req.responseXML.xml
			} else {
				sResponse = req.responseText
			}
			rdUpdatePage(req.responseXML, sResponse)
         } else {
            alert("There was a problem retrieving the data:\n" + req.status + ": " + req.statusText);
			document.write(req.responseText)  //Display the error.
         }
    }
}

function rdAjaxRequestWithFormVars(commandURL) {
	//Build the request URL.
	var commandURL
	//Form vars:
	var sPrevRadioId
	var frm = document.forms[0]
	for (var i=0; i < frm.elements.length; i++) { 
		if (frm.elements[i].name.lastIndexOf("-PageNr") != -1) {
			if (frm.elements[i].name.lastIndexOf("-PageNr") == frm.elements[i].name.length-7) {
				continue  //Don't forward the interactive page nr.
			}
		}
		
		switch (frm.elements[i].type) {
			case 'hidden':  
			case 'text':  
			case 'textarea':  
			case 'password':  
			case 'select-one':  
			case 'file':  
				var sValue = rdGetInputValue(frm.elements[i].id)
				commandURL += '&' + frm.elements[i].name + "=" + encodeURI(sValue)
				break;
				case 'select-multiple':
					var selectedItems = new Array(); 
					for (var k = 0; k < frm.elements[i].length; k++) { 
						if (frm.elements[i].options[k].selected) {
							selectedItems[selectedItems.length] = frm.elements[i].options[k].value
						}
					} 
					var sValue = selectedItems.join(',')
					commandURL += '&' + frm.elements[i].name + "=" + encodeURI(sValue)
					break;
			case 'checkbox':
				if (frm.elements[i].checked) {
					var sValue = rdGetInputValue(frm.elements[i].id)
					commandURL += '&' + frm.elements[i].name + "=" + encodeURI(sValue)
				}
				break;
			case 'radio': 
				var sRadioId = 'rdRadioButtonGroup' + frm.elements[i].name
				if (sPrevRadioId != sRadioId) {
					sPrevRadioId = sRadioId
					var sValue = rdGetInputValue(sRadioId)
					commandURL += '&' + frm.elements[i].name + "=" + encodeURI(sValue)
				}
				break;
		}
	}
	
	rdAjaxRequest(commandURL)
}

function rdUpdatePage(xmlResponse, sResponse) {
	if (sResponse.length != 0) {
		switch (xmlResponse.documentElement.getAttribute("rdAjaxCommand")) {
		
				case 'RefreshDataTable':
					//Find the HTML TABLE's DIV.
					var sTableDivID = xmlResponse.documentElement.getAttribute('id')
					var eleTableDiv = document.getElementById(sTableDivID)
					if (eleTableDiv) {
						//Remove the outer response's DIV.
						sResponse = sResponse.substr(sResponse.indexOf(">") + 1)
						sResponse = sResponse.substr(0,sResponse.lastIndexOf("<"))
						
						//Write the response html to the page, replacing the original table.
						eleTableDiv.innerHTML = sResponse
					}
					break;
					
				case 'RefreshElement':
					var sElementIDs = xmlResponse.documentElement.getAttribute('rdRefreshElementID').split(",")
					for (var i=0; i <  sElementIDs.length; i++) { 
						var eleOld = document.getElementById(sElementIDs[i])
						if (eleOld) {
							if (isIE) {
								//ie	
								var eleNew = xmlResponse.selectSingleNode("//*[@id='" + sElementIDs[i] + "']")
								if (eleNew) {
									eleOld.outerHTML = eleNew.xml
								}
							} else {
								var doc = rdAjaxIframe.contentDocument
								doc.body.innerHTML = sResponse
								var eleNew = doc.getElementById(sElementIDs[i])
								if (eleNew) {
									var range = document.createRange(); 
									range.selectNode(eleOld); 
									var docFrag = range.createContextualFragment(getMozillaOuterHtml(eleNew)); 
									eleOld.parentNode.replaceChild(docFrag, eleOld) 
								}
							}
						}
					}
					break;



				case 'UpdateTreeBranchRows':
					//Find the end position of the clicked table row.
					var sRowGUID =  xmlResponse.documentElement.getAttribute('rdRowGUID')
					var sTableDivID = xmlResponse.documentElement.getAttribute('id')
					var eleTableDiv = document.getElementById(sTableDivID)
					if (eleTableDiv) {
						var sTable = eleTableDiv.innerHTML
						var nInsertPos = sTable.indexOf(sRowGUID)
						nInsertPos = sTable.indexOf("rdRowEnd",nInsertPos)
						nInsertPos = sTable.indexOf("</tr>",nInsertPos) + 5

						//Remove the response's outer DIV.
						sResponse = sResponse.substr(sResponse.indexOf(">") + 1)
						sResponse = sResponse.substr(0,sResponse.lastIndexOf("<"))

						//Insert the returned rows.
						eleTableDiv.innerHTML = sTable.substr(0,nInsertPos) + sResponse + sTable.substr(nInsertPos)
						////Remove the outer DIV.
						//sResponse = sResponse.substr(sResponse.indexOf(">") + 1)
						//sResponse = sResponse.substr(0,sResponse.lastIndexOf("<"))
						
					}
					break;
					
					
		}
	}
}

function getMozillaOuterHtml(ele) {
	var sHtml = "<" + ele.nodeName
	for (var i=0; i < ele.attributes.length; i++) { 
		sHtml += ' ' + ele.attributes[i].name + '="' + ele.attributes[i].value + '"'
	}
	sHtml += ">"
	sHtml += ele.innerHTML
	sHtml += "</" + ele.nodeName + ">"
	return sHtml
}

