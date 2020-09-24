var rdDropZoneId
var rdAddedRemovedPanels = ""

YAHOO.namespace ("rd");  
YAHOO.rd.DDApp = function() {
    return {
        init: function(dd) {
/////
            dd.startDrag = function(e) {
				var pnlDragged = this.getEl()
				rdSetZIndex(pnlDragged,1)
				rdSetOpacity(pnlDragged,.75)
				pnlDragged.style.cursor = "move"
				
				rdSetAppletVisibility("hidden")
            };
            
/////
            dd.endDrag = function(e) {
                //endDrag occurs after DragDrop
				var pnlDragged = this.getEl()
				pnlDragged.style.cursor = "auto"
				rdSetZIndex(pnlDragged,0)
				rdSetOpacity(pnlDragged,1)
				
			    pnlDragged.style.left = "0px"
                pnlDragged.style.top = "0px"
                
                if (rdDropZoneId) {
                    dd.onDragDrop(e,rdDropZoneId)
                }

				rdSetAppletVisibility("")
            }
            
            dd.onDragOver = function(e, id) {

                var x 
                x = 1

                if (id.indexOf("rdDashboardColumn") != 0) {
                    return  //not over a dashboard column.
                }

                //Find the closest DropZone that's above the current position in the same column.
                var colDropped = document.getElementById(id)
				var pnlDragged = this.getEl()
                var eleDropZone
                var eleClosestDropZone
                var nClosestDistance
                var elePanelChild
                for (var i=0; i < colDropped.childNodes.length; i++) {
                    if (colDropped.childNodes[i].id.indexOf("rdDashboardDropZone") != -1) {
                        var eleDropZone = colDropped.childNodes[i]
                        var yDragged = rdGetDbPanelHeight(pnlDragged)
                        var yDropZone = rdGetDbPanelHeight(eleDropZone)
                        if (!eleClosestDropZone) {
                            eleClosestDropZone = eleDropZone
                            nClosestDistance = Math.abs(yDragged - yDropZone)
                        } else {
                            if (Math.abs(yDragged - yDropZone) < nClosestDistance) {
                                eleClosestDropZone = eleDropZone
                                nClosestDistance = Math.abs(yDragged - yDropZone)
                            }
                        }
                    }
                }
                
                if (eleClosestDropZone) {
                    rdSetDropZone(eleClosestDropZone)
                }

            }
            
 /////
/*
           dd.onDragOut = function(e, id) {
                // unhighlight the drop zone.
                if (rdDropZoneId) {
                        var eleDropZone = document.getElementById(rdDropZoneId)
                        eleDropZone.style.backgroundColor=""
                        rdDropZoneId=null
                }
            }
*/
/////
            dd.onDragDrop = function(e, id) {
                if (!rdDropZoneId) {
                    return
                }
                //Move the dragged panel
                var eleDropZone = document.getElementById(rdDropZoneId)  //The drop zone where it was dropped.
                if (!eleDropZone) {
                    return
                }
                
                var pnlDragged = this.getEl()
                if (pnlDragged.id.replace("rdDashboardPanel","rdDashboardDropZone") == rdDropZoneId) {
                    //Dropped on the current panel's drop zone.  Put the panel back.
				    rdSetDropZone(null)
			        pnlDragged.style.left = "0px"
                    pnlDragged.style.top = "0px"
				    //rdAnimateHome(pnlDragged.id)
				    return
                }
                
                pnlDragged.style.left = "0px"
                pnlDragged.style.top = "0px"
                
                var eleDropZoneBelow = document.getElementById(pnlDragged.id.replace("rdDashboardPanel","rdDashboardDropZone")) //The drop zone below the panel.
                
                //Move the panel and its sibling drop zone.
                if (eleDropZone.nextSibling) {
                    eleDropZone.parentNode.insertBefore(eleDropZoneBelow,eleDropZone.nextSibling)
                    eleDropZone.parentNode.insertBefore(pnlDragged,eleDropZone.nextSibling)
                } else {
                    eleDropZone.parentNode.appendChild(pnlDragged)
                    eleDropZone.parentNode.appendChild(eleDropZoneBelow)
                }
                
                rdSetDropZone(null)
                rdSaveDashboardOrder()

            }
        }
    }
} ();

function rdSetDropZone(eleDropZone) {
    if (rdDropZoneId) {
        var eleOldDropZone = document.getElementById(rdDropZoneId)
        if (eleOldDropZone) {
            eleOldDropZone.firstChild.firstChild.firstChild.style.backgroundColor=""  //All these children get to the table's cell.
        }
    }

    if (eleDropZone) {
        eleDropZone.firstChild.firstChild.firstChild.style.backgroundColor="silver"  //All these children get to the table's cell.
        rdDropZoneId = eleDropZone.id
    } else {
        rdDropZoneId = null
    }
}


function rdSetZIndex(ele, nZIndex) {
    var nOldZIndex = ele.style.zIndex
    ele.style.zIndex = nZIndex
    //For IE we need to fix cssText too.
    if (ele.style.cssText) {
        ele.style.cssText = ele.style.cssText.replace("Z-INDEX: " + nOldZIndex,"Z-INDEX: " + nZIndex)
    }
}

function rdSetOpacity(element, alpha) {

    var style = element.style;
    if( style.MozOpacity != undefined ) { //Moz and older
        style.MozOpacity = alpha;
    }
    else if( style.filter != undefined ) { //IE
        style.height=element.offsetHeight; //Workaround to make filter work.
        style.filter = "alpha(opacity=" + (alpha * 100) + ")";
        element.filters.alpha.opacity = ( alpha * 100 );
    }
    else if( style.opacity != undefined ) { //Opera
        style.opacity = alpha;
    }
}

//function rdAnimateHome(elementId) {
//    //Nudge the panel back were it belongs.
//    var ele = document.getElementById(elementId)
//    var left =  parseInt(ele.style.left.replace("px",""))
//    var top = parseInt(ele.style.top.replace("px",""))
//    left = left / 2.01
//    top = top / 2.01
//    ele.style.left = left + "px"
//    ele.style.top = top + "px"
//    if (Math.abs(left) + Math.abs(top) > 0) {
//        setTimeout("rdAnimateHome('" + elementId + "')", 33)
//    }
//}

function rdGetDbPanelHeight(eleObject) { 
    return(eleObject.offsetParent ? (rdGetDbPanelHeight(eleObject.offsetParent) + eleObject.offsetTop) : eleObject.offsetTop); 
}

function rdSetAppletVisibility(sVis) {
    //Hide objects that will intefere with DnD.
    //applets
    var eleApplets = document.getElementsByTagName("applet")  
    for (var i=0; i < eleApplets.length; i++) {
        var eleApplet = eleApplets[i]
        eleApplet.style.visibility = sVis
    }
    //Flash with IE
    eleApplets = document.getElementsByTagName("object")  
    for (var i=0; i < eleApplets.length; i++) {
        var eleApplet = eleApplets[i]
        eleApplet.style.visibility = sVis
    }
    //Flash with Mozilla.
    eleApplets = document.getElementsByTagName("embed")  
    for (var i=0; i < eleApplets.length; i++) {
        var eleApplet = eleApplets[i]
        eleApplet.style.visibility = sVis
    }
}

function rdInitDashboardPanels() {
    //Is the dashboard adjustable?
    var eleAdjustable =  document.getElementById("rdDashboardAdjustable") 
    if (eleAdjustable.innerHTML=="False") {
        return
    }

    //Make the panels draggable.
    var elePanels = document.getElementsByTagName("DIV")  
    for (var i=0; i < elePanels.length; i++) {
        var elePanel = elePanels[i]
        if (elePanel.id.indexOf("rdDashboardPanel-") == 0) {
            var dd=new YAHOO.util.DD(elePanel.id);dd.setHandleElId(elePanel.id.replace("rdDashboardPanel-","rdDashboardPanelTitle-"));YAHOO.rd.DDApp.init(dd);
        }
    }
    
    //Make the columns droppable.
    var eleCols = document.getElementsByTagName("TD")  
    for (var i=0; i < eleCols.length; i++) {
        var eleCol = eleCols[i]
        if (eleCol.id.indexOf("rdDashboardColumn") == 0) {
            YAHOO.rd.DDApp.init(new YAHOO.util.DDTarget(eleCol.id));
        }
    }

} 

function rdSaveDashboardOrder() { 
    var eleHiddenPanelOrder = document.getElementById("rdDashboardPanelOrder")
    
    eleHiddenPanelOrder.value = ""
    var elePanels = document.getElementsByTagName("DIV")  
    for (var i=0; i < elePanels.length; i++) {
        var elePanel = elePanels[i]
        if (elePanel.id.indexOf("rdDashboardPanel") == 0) {
            eleHiddenPanelOrder.value += "," + elePanel.id.replace("rdDashboardPanel-","")
            //Add the column number
            var nColNr = elePanel.parentNode.id.replace("rdDashboardColumn","")
            eleHiddenPanelOrder.value += ":" + nColNr
        }
    }
    
    var rdPanelParams = "&rdReport=" + document.getElementById("rdDashboardDefinition").value
    
    window.status = "Saving dashboard panel positions."
    rdAjaxRequestWithFormVars('rdAjaxCommand=UpdateDashboardPanelOrder' + rdPanelParams)
}

function rdSaveDashboardPanels(sPanelId,eEvent) {
    if (rdAddedRemovedPanels.length == 0) {
        //No changes
        ShowElement(null,'rdDashboardList,rdDashboardPanels,rdAddPanels,rdShowPanels','');
        return
    }
    rdPanelsChanged = false
    
    var rdParams = "&rdReport=" + document.getElementById("rdDashboardDefinition").value
    rdParams += "&rdAddedRemovedPanels=" + rdAddedRemovedPanels
    rdAjaxRequestWithFormVars('rdAjaxCommand=SaveDashboardPanels&' + rdParams)
    //Ajax will response with a RefreshPage.
}

function rdRemoveDashboardPanel(sPanelId,eEvent) {

    //Remove the panel from the page. 
    var elePanel = document.getElementById(sPanelId)
    if (elePanel) {  //If the user clicks a lot on the same button, this may not exist.
        var eleDropZoneBelow = document.getElementById(sPanelId.replace("rdDashboardPanel","rdDashboardDropZone")) //The drop zone below the panel.

        elePanel.parentNode.removeChild(elePanel)
        eleDropZoneBelow.parentNode.removeChild(eleDropZoneBelow)
        
        //Clear the checkbox.
        sPanelId = sPanelId.replace("rdDashboardPanel-","")
        var eleChecks = document.getElementsByTagName("INPUT")  
        for (var i=0; i < eleChecks.length; i++) {
            var eleCheck = eleChecks[i]
            if (eleCheck.parentNode.innerHTML.indexOf('&quot;,' + sPanelId + '&quot;') != -1) {
                eleCheck.checked = false
            }
        }

        var rdPanelParams = "&rdReport=" + document.getElementById("rdDashboardDefinition").value
        rdAjaxRequestWithFormVars('rdAjaxCommand=RemoveDashboardPanel&PanelID=' + sPanelId + rdPanelParams)
    }
}

var rdPanelParams
var rdPanelParamNames
function rdSaveDashboardParams(sPanelId) {

	var sErrorMsg = rdValidateForm()
	if (sErrorMsg) {
		alert(sErrorMsg)
		return
	}

    var elePanel = document.getElementById(sPanelId)
    rdPanelParams = ""
    rdPanelParamIDs = ""
    rdGetRecursiveInputValues(elePanel)
    rdPanelParams += "&rdReport=" + document.getElementById("rdDashboardDefinition").value
    
    //window.status = "Saving dashboard panel parameters."
    rdAjaxRequest('rdAjaxCommand=SaveDashboardParams&PanelID=' + sPanelId.replace("rdDashboardPanel-","") + rdPanelParams + "&ParamIDs=" + rdPanelParamIDs)
}

function rdDashboardHidePanelParams(sPanelId) {
    //Hide all open parameter panels.
    var eleParams = document.getElementsByTagName("TR")  
    for (var i=0; i < eleParams.length; i++) {
        var eleParam = eleParams[i]
        if (eleParam.id) {
            if (eleParam.id.indexOf("rdDashboard2PanelParams-") == 0) {
                if (eleParam.style.display!='none') {
                    var sId = eleParam.id.substr(24)
                    ShowElement(this.id,'rdDashboardEdit-' + sId + ',rdDashboardCancel-' + sId + ',rdDashboard2PanelParams-' + sId,'Toggle');
                }
            }
        }
    }
}

function rdGetRecursiveInputValues(eleParent) {
	var sPrevRadioId
	for (var i = 0; i < eleParent.childNodes.length; i++) {
	    var eleCurr = eleParent.childNodes[i]
	    //if (eleCurr.nodeName == "INPUT") {
	    switch (eleCurr.type) {
			case 'hidden':  
			case 'text':  
			case 'textarea':  
			case 'password':  
			case 'select-one':  
			case 'file':  
				var sValue = rdGetFormFieldValue(eleCurr)
				rdPanelParams += '&' + eleCurr.name + "=" + encodeURI(sValue)
				rdPanelParamIDs += ":" + eleCurr.name
				break;
			case 'select-multiple':
				var selectedItems = new Array(); 
				for (var k = 0; k < eleCurr.length; k++) { 
					if (eleCurr.options[k].selected) {
						selectedItems[selectedItems.length] = eleCurr.options[k].value
					}
				} 
				var sValue = selectedItems.join(',')
				rdPanelParams += '&' + eleCurr.name + "=" + encodeURI(sValue)
				rdPanelParamIDs += ":" + eleCurr.name
				break;
			case 'checkbox':
				if (eleCurr.checked) {
					var sValue = rdGetFormFieldValue(eleCurr)
					rdPanelParams += '&' + eleCurr.name + "=" + encodeURI(sValue)
				    rdPanelParamIDs += ":" + eleCurr.name
				}
				break;
			case 'radio': 
				var sRadioId = 'rdRadioButtonGroup' + eleCurr.name
				if (sPrevRadioId != sRadioId) {
					sPrevRadioId = sRadioId
					var sValue = rdGetFormFieldValue(document.getElementById(sRadioId))
					rdPanelParams += '&' + eleCurr.name + "=" + encodeURI(sValue)
    				rdPanelParamIDs += ":" + eleCurr.name
				}
			default:
			    //Not an input element.
    	        rdGetRecursiveInputValues(eleCurr)
				break;
		}
	}
}
