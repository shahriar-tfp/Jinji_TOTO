
	function SubmitForm(sPage, sTarget, bValidate, sConfirm) {

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
		
		if (sTarget != '') {
			document.rdForm.target=sTarget
		} else {
			document.rdForm.target='_self'
		}
		
		//If there's no RequestForwarding, need to remove all RequestForwarding elements.
		if (sPage.search("rdRequestForwarding=Form") == -1) {
			while (true) {
				var eleRequestVar = document.getElementById("rdHiddenRequestForwarding")
				if (eleRequestVar) {
					eleRequestVar.parentNode.removeChild(eleRequestVar)
				} else {
					break
				}
			}
		}

		rdSaveInputCookies()

		var hiddenRnd=document.createElement("INPUT");
		hiddenRnd.type="HIDDEN"
		hiddenRnd.id="rdRnd"
		hiddenRnd.name="rdRnd"
		hiddenRnd.value=Math.floor(Math.random() * 100000)
		document.rdForm.appendChild(hiddenRnd);
			
		//document.rdForm.action=sPage + "&rdRnd=" + Math.floor(Math.random() * 100000)
		document.rdForm.action=sPage
		document.rdForm.submit()
	}

	function SubmitSort(sPage, RowCnt, SortRowLimit, SortRowLimitMsg) {
		if (SortRowLimit.length != 0) {
			nRowCnt = parseInt(RowCnt,10)
			nSortRowLimit = parseInt(SortRowLimit,10)
			if (nRowCnt > nSortRowLimit) {
				alert(SortRowLimitMsg)
				return
			}
		}

		SubmitForm(sPage,'')
	}


	function NavigateLink2(sUrl, sTarget, bValidate, sFeatures, sConfirm) {
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
		
		//If the URL has a ? at the end, remove it.
		if (sUrl.substring(sUrl.length-1,sUrl.length) == "?") {
			sUrl = sUrl.substring(0,sUrl.length - 1)
		}
		//Replace + with %20.
		var pattern = /\+/ig;
		sUrl = sUrl.replace(pattern,"%20");

		rdSaveInputCookies()

		switch (sTarget) {
			case '_parent':
				//window.parent.navigate(sUrl)
				window.parent.location.href = sUrl
				break;
			case '_top':
				//window.top.navigate(sUrl)
				window.top.location.href = sUrl
				break;
			case '_modal':
				window.showModalDialog(sUrl,'',sFeatures)
				break;
			case '':
				window.open(sUrl,'_self')
				break;
			default:
				window.open(sUrl,sTarget,sFeatures)
				break;
		}
	}


