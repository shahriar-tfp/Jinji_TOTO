<SCRIPT TYPE="text/javascript"> 
	function rdBodyLoad() {
        //rdBodyLoadFunctions
	}
	function rdValidateForm() {
        //rdInputValidations
	}
	function rdSaveInputCookies() {
        //rdSaveInputCookies
	}

	function rdBodyPressEnter(sID) {
		var ele = document.getElementById(sID);
		if (ele) {
		    if (ele.tagName=="INPUT") {  //button
		        ele.click();
		        //button
		    } else {
		        //span or image
			    window.location.assign(ele.parentNode.href);
		    }
		}
	}
</SCRIPT>


