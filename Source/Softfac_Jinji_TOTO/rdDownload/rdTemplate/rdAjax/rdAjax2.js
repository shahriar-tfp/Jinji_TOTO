var req;

var bDoingRequest = false

function rdAjaxRequest(commandParams) {

	if (commandParams.search("rdRequestForwarding=Form") != -1) {
		rdAjaxRequestWithFormVars(commandParams)
		return
	}

    if (bDoingRequest) {
        setTimeout('rdAjaxRequest("' + commandParams + '")', Math.floor(Math.random() * 1000))  //Wait a random amount of time between 0 and 1 second.
        return    
    }
    bDoingRequest = true 
    
	try {
		var url = "rdTemplate/rdAjax/rdAjax.aspx"
		req = YAHOO.util.Connect.asyncRequest('POST', url, callback, commandParams);
	}
	catch (e) {
		commandParams = commandParams.replace('rdAjaxCommand','rdAjaxAbort') 
		window.open(url + "?" + commandParams,'_self')
	}
}

var handleSuccess = function(o){
	if(o.responseText !== undefined){
        rdUpdatePage(o.responseXML, o.responseText)
	}
}
var handleFailure = function(o){
    window.status = "AJAX error: " + o.statusText
    //commandParams = commandParams.replace('rdAjaxCommand','rdAjaxAbort') 
    //window.open(url + "?" + commandParams,'_self')

}
var callback =
{
  success:handleSuccess,
  failure: handleFailure,
  argument: ['rdArg']
};


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
		//Don't forward security stuff - it's already in session vars.
		if (frm.elements[i].name == "rdUsername") {continue}
		if (frm.elements[i].name == "rdPassword") {continue}
		if (frm.elements[i].name == "rdFormLogon") {continue}
		
		//Don't forward a variable that's already in the list, perhaps from LinkParams.
		if (commandURL.indexOf("&" + frm.elements[i].name + "=")!=-1) {continue}
		
		switch (frm.elements[i].type) {
			case 'hidden':  
			case 'text':  
			case 'textarea':  
			case 'password':  
			case 'select-one':  
			case 'file':  
				var sValue = rdGetFormFieldValue(frm.elements[i])
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
					var sValue = rdGetFormFieldValue(frm.elements[i])
					commandURL += '&' + frm.elements[i].name + "=" + encodeURI(sValue)
				}
				break;
			case 'radio': 
				var sRadioId = 'rdRadioButtonGroup' + frm.elements[i].name
				if (sPrevRadioId != sRadioId) {
					sPrevRadioId = sRadioId
					var sValue = rdGetFormFieldValue(document.getElementById(sRadioId))
					commandURL += '&' + frm.elements[i].name + "=" + encodeURI(sValue)
				}
				break;
		}
	}
    commandURL = commandURL.replace("rdRequestForwarding=Form","") //Don't loop back here again from rdAjaxRequest()
	rdAjaxRequest(commandURL)
}

function rdUpdatePage(xmlResponse, sResponse) {
	if (sResponse.length != 0) {
	    if (!xmlResponse.documentElement) {
	        rdReportResponseError(sResponse)
	        return
	    }
	    if (!xmlResponse.documentElement.getAttribute("rdAjaxCommand")) {
	        rdReportResponseError(sResponse)
	    }
	    
	    window.status =""

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
							if (!rdAjaxIframe.contentDocument) {
								//IE, Opera	
								var eleNew = xmlResponse.selectSingleNode("//*[@id='" + sElementIDs[i] + "']")
								if (eleNew) {
									eleOld.outerHTML = eleNew.xml
								}
							} else {
								//Mozilla
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
						    if (window.rdInitDashboardPanels) {
						        rdInitDashboardPanels()
						    }
//						    Not working because other elements need to be reloaded too.
//						    if (window.rdGmapLoad) {
//						        //May need to reload the map.
//						        if (eleOld.getAttribute("GoogleMapTypes")) {
//						            rdGmapLoad(sElementIDs[i])
//						        }
//						    }
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
					
				case 'UpdateMapImage':
				    //Used by AWS Map Images
					var sImageID = xmlResponse.documentElement.getAttribute('id')
					var eleImage = document.getElementById(sImageID)
					if (eleImage) {
					    //Update the image SRC.
		                var sImageSrc = xmlResponse.documentElement.getAttribute('rdSrc')
		                eleImage.setAttribute("src",sImageSrc)
                    }
                    break;
					
				case 'RequestRefreshElement':
				    //Request back to the server so that just this element is refreshed.
				    
					var sElementID = xmlResponse.documentElement.getAttribute('ElementID')
					var sReport = xmlResponse.documentElement.getAttribute('rdReport')
				    rdAjaxRequest('rdAjaxCommand=RefreshElement&rdRefreshElementID=' + sElementID + '&rdReport=' + sReport)
					break;

				case 'RequestRefreshPage':
				    window.location.href = window.location.href
					break;


				case 'ShowStatus':
				    window.status = xmlResponse.documentElement.getAttribute("Status")
					
		}
	
		if (typeof window.rdRepositionSliders != 'undefined') {
			//Move CellColorSliders, if there are any.
			rdRepositionSliders()
		}
	}
	
	//May need to run some script.
    rdAjaxRunOnLoad(xmlResponse)   
	    
	bDoingRequest = false
}

function rdAjaxRunOnLoad(xml) {
    var scripts = xml.getElementsByTagName('SCRIPT')
    for (var i=0; i < scripts.length; i++) {
        var attrRun = scripts[i].attributes.getNamedItem('rdAjaxRunOnLoad')
        if (attrRun) {
            if (attrRun.value == 'True') {
                eval(scripts[i].text)
            }
        }
    }
}

function rdGetFormFieldValue(fld) {
	
	var sValue

	if (fld.tagName == "RDRADIOBUTTONGROUP") {
		// Radio buttons
		sFieldId = fld.id.replace(/rdRadioButtonGroup/g, '')
		var cInputs = document.getElementsByTagName("INPUT")
		for (var i = 0; i < cInputs.length; i++) {
			if (cInputs[i].name == sFieldId) {
				if (cInputs[i].checked) {
					sValue = cInputs[i].value
					break
				}
			}
		}
		if (sValue == undefined) {
				sValue = ''
			}

	} else {
		// All other fields
		if (fld.value.length == 0) {
			sValue = ''
		} else {
			sValue = fld.value
		}
	}
	return sValue	
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

function rdReportResponseError(sResponse) {
//    if (sResponse.indexOf('<rdErrorMsgContent>') != -1) {
        document.body.innerHTML = sResponse
//      }
}
