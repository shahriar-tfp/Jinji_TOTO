

function rdValidateDate(sFieldId, sDateFormat, sErrorMsg, sErrorClass) {
	var eleInput = document.getElementById(sFieldId)
	if (eleInput) {
		if (rdIsInputVisible(sFieldId)) {
			rdRestoreNonErrorClass(eleInput)
			if (eleInput.value.length != 0) {
				if (isDate(eleInput.value, sDateFormat) == false) {
					rdSetErrorClass(eleInput,sErrorClass)
					return sErrorMsg
				}
			}
		}
	} else {
		//Element not found.  Look to see if it's in a DataTable.
		if (sFieldId.indexOf("_Row") == -1) {  //Be sure not already checking a Row.
			for (var i = 1; i < 10000; i++) {
				eleInput = document.getElementById(sFieldId + "_Row" + i)
				if (!eleInput) {
					break
				}
				var sRowErrorMsg = rdValidateDate(sFieldId + "_Row" + i, sDateFormat, sErrorMsg, sErrorClass, sErrorClass)
				if (sRowErrorMsg) {
					return sErrorMsg
				}
			}  
		}
	}
}

function rdValidateNumeric(sFieldId, sLocaleDecimalChar, sErrorMsg, sErrorClass) {
	var eleInput = document.getElementById(sFieldId)
	if (eleInput) {
		if (rdIsInputVisible(sFieldId)) {
			rdRestoreNonErrorClass(eleInput)
			var sValue = rdGetInputValue(sFieldId)
			if (sValue.length != 0) {
				//Get the value into the invariant style that JScript understands.
				if (sLocaleDecimalChar == ",") {    
					sValue = sValue.replace(/\./g, '')  //Remove periods.
					sValue = sValue.replace(/,/g, '.')  //Replace comma decimal points with periods.
				} else {						
					sValue = sValue.replace(/,/g, '')   //Remove commas.
				}
				if (parseFloat(sValue) != sValue) {
					rdSetErrorClass(eleInput,sErrorClass)
					return sErrorMsg
				}
			}
		}
	} else {
		//Element not found.  Look to see if it's in a DataTable.
		if (sFieldId.indexOf("_Row") == -1) {  //Be sure not already checking a Row.
			for (var i = 1; i < 10000; i++) {
				eleInput = document.getElementById(sFieldId + "_Row" + i)
				if (!eleInput) {
					break
				}
				var sRowErrorMsg = rdValidateNumeric(sFieldId + "_Row" + i, sLocaleDecimalChar, sErrorMsg, sErrorClass)
				if (sRowErrorMsg) {
					return sErrorMsg
				}
			}  
		}
	}
}


function rdValidateRequired(sFieldId, sErrorMsg, sErrorClass) {
	var eleInput = document.getElementById(sFieldId)
	if (eleInput) {
		if (rdIsInputVisible(sFieldId)) {
			rdRestoreNonErrorClass(eleInput)
			var sValue = rdGetInputValue(sFieldId)
			if (sValue.length == 0) {
				rdSetErrorClass(eleInput,sErrorClass)
				return sErrorMsg
			}
		}
	} else {
		//Element not found.  Look to see if it's in a DataTable.
		if (sFieldId.indexOf("_Row") == -1) {  //Be sure not already checking a Row.
			for (var i = 1; i < 10000; i++) {
				eleInput = document.getElementById(sFieldId + "_Row" + i)
				if (!eleInput) {
					break
				}
				var sRowErrorMsg = rdValidateRequired(sFieldId + "_Row" + i, sErrorMsg, sErrorClass)
				if (sRowErrorMsg) {
					return sErrorMsg
				}
			}  
		}
	}
}

function rdValidateLength(sFieldId, MinLength, MaxLength, sErrorMsg, sErrorClass) {
	var eleInput = document.getElementById(sFieldId)
	if (eleInput) {
		if (rdIsInputVisible(sFieldId)) {
			rdRestoreNonErrorClass(eleInput)
			var sValue = rdGetInputValue(sFieldId)
			if (sValue.length < MinLength || sValue.length > MaxLength ) {
				rdSetErrorClass(eleInput,sErrorClass)
				return sErrorMsg
			}
		}
	} else {
		//Element not found.  Look to see if it's in a DataTable.
		if (sFieldId.indexOf("_Row") == -1) {  //Be sure not already checking a Row.
			for (var i = 1; i < 10000; i++) {
				eleInput = document.getElementById(sFieldId + "_Row" + i)
				if (!eleInput) {
					break
				}
				var sRowErrorMsg = rdValidateLength(sFieldId + "_Row" + i, MinLength, MaxLength, sErrorMsg, sErrorClass)
				if (sRowErrorMsg) {
					return sErrorMsg
				}
			}  
		}
	}
}

function rdGetInputValue(sFieldId) {
	var fld = document.getElementById(sFieldId)
	if (!fld) {
		fld = document.getElementsByName(sFieldId)[0]
	}
	
	var sValue

	if (fld.tagName == "RDRADIOBUTTONGROUP") {
		// Radio buttons
		sFieldId = sFieldId.replace(/rdRadioButtonGroup/g, '')
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

function rdIsInputVisible(sFieldId) {
	var fld = document.getElementById(sFieldId)
	if (!fld) {
		fld = document.getElementsByName(sFieldId)[0]
	}
	if (!fld) {
		return false
	}
		
	var par = fld.parentNode
	while (par) {
		if (par.style) {
			if (par.style.display=="none") {
				return false
			}
		}
		par = par.parentNode
	} 
	
	return true	
}

function rdChangeFlag(sChangeFlagId) {
	if (document.getElementById(sChangeFlagId)) {
		document.getElementById(sChangeFlagId).value = "True"
	}
}

function rdSetErrorClass(eleInput,sErrorClass) {
	if (sErrorClass.length > 0) {
		if (eleInput.getAttribute("rdNonErrorClass")==null) {
			eleInput.setAttribute("rdNonErrorClass",eleInput.className)
		}
		if (eleInput.tagName == "RDRADIOBUTTONGROUP") {
				eleInput.parentNode.className = sErrorClass
		} else {
			eleInput.className = sErrorClass
		}
	}
}
function rdRestoreNonErrorClass(eleInput) {
	if (eleInput.getAttribute("rdNonErrorClass")!=null) {
		if (eleInput.tagName == "RDRADIOBUTTONGROUP") {
			eleInput.parentNode.className = eleInput.getAttribute("rdNonErrorClass")
		} else {
			eleInput.className = eleInput.getAttribute("rdNonErrorClass")
		}
	}
}

