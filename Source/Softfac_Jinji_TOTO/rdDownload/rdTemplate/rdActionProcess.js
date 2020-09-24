
function RunProcess(sActionsXml, bValidate, sConfirm, sTarget) {

	if (bValidate == "true") {
		var sErrorMsg = rdValidateForm()
		if (sErrorMsg) {
			alert(sErrorMsg)
			return
		}
	}
	
	if (sConfirm) {
		if (sConfirm.length != 0) {
			if (!confirm(sConfirm)) {
				return
			}
		}
	}
		
	rdSaveInputCookies()
	
	if (sTarget) {
		sOldTarget = document.rdForm.target
		if (sTarget != '') {
			document.rdForm.target=sTarget
		} else {
			document.rdForm.target='_self'
		}
	}


	//Add the actions to a new Hidden form field.  First, make sure that the hiddent element doesn't already exist.  (Happens with Opera Back button.)
	var hiddenProcessAction=document.getElementById("rdProcessAction")
	if (hiddenProcessAction) { 
		document.rdForm.removeChild(hiddenProcessAction)
	}
	
	hiddenProcessAction=document.createElement("INPUT");
	hiddenProcessAction.type="HIDDEN"
	hiddenProcessAction.id="rdProcessAction"
	hiddenProcessAction.name="rdProcessAction"
	hiddenProcessAction.value=encodeURIComponent(sActionsXml)
	document.rdForm.appendChild(hiddenProcessAction);
			
	var hiddenRnd=document.createElement("INPUT");
	hiddenRnd.type="HIDDEN"
	hiddenRnd.id="rdRnd"
	hiddenRnd.name="rdRnd"
	hiddenRnd.value=Math.floor(Math.random() * 100000)
	document.rdForm.appendChild(hiddenRnd);
			
	var hiddenScrollX=document.createElement("INPUT");
	hiddenScrollX.type="HIDDEN"
	hiddenScrollX.id="rdScrollX"
	hiddenScrollX.name="rdScrollX"
	hiddenScrollX.value=rdGetScroll('x')
	document.rdForm.appendChild(hiddenScrollX);
	var hiddenScrollY=document.createElement("INPUT");
	hiddenScrollY.type="HIDDEN"
	hiddenScrollY.id="rdScrollY"
	hiddenScrollY.name="rdScrollY"
	hiddenScrollY.value=rdGetScroll('y')
	document.rdForm.appendChild(hiddenScrollY);
			
	//document.rdForm.action="rdProcess.aspx?&rdRnd=" + Math.floor(Math.random() * 100000)
	document.rdForm.action="rdProcess.aspx?"
	document.rdForm.submit()
	
	if (sTarget) {
		document.rdForm.target = sOldTarget
	}

}

