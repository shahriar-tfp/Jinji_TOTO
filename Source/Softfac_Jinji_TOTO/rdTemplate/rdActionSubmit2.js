
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
		
		sOldTarget = document.rdForm.target
		if (sTarget != '') {
			document.rdForm.target=sTarget
		} else {
			document.rdForm.target='_self'
		}
		
		if (sPage.search("rdRequestForwarding=Form") == -1) {
		    //No RequestForwarding, remove all RequestForwarding elements.
			while (true) {
				var eleForward = document.getElementById("rdHiddenRequestForwarding")
				if (eleForward) {
					eleForward.parentNode.removeChild(eleForward)
				} else {
					break
				}
			}
		} else {
		    //RequestForwarding, remove elements that are in the request.
	        var eleForwards = document.getElementsByTagName("INPUT")
	        for (var i=0; i < eleForwards.length; i++) {
	            var eleForward = eleForwards[i]
	            if (eleForward.type=="hidden") {
	                //Is the var in the request string?
	                if (sPage.indexOf("?" + eleForward.name + "=")!=-1 || sPage.indexOf("&" + eleForward.name + "=")!=-1) {
	                    eleForward.parentNode.removeChild(eleForward)
	                }
	            }
            }
		}
		
		if (sPage.indexOf("rdSubmitScroll") != -1) {
			sPage=sPage.replace("rdSubmitScroll","rdScrollX=" + rdGetScroll('x') + "&rdScrollY=" + rdGetScroll('y') )
		}

		rdSaveInputCookies()

		var hiddenRnd=document.createElement("INPUT");
		hiddenRnd.type="HIDDEN"
		hiddenRnd.id="rdRnd"
		hiddenRnd.name="rdRnd"
		hiddenRnd.value=Math.floor(Math.random() * 100000)
		document.rdForm.appendChild(hiddenRnd);
		
		document.rdForm.action=sPage
		document.rdForm.submit()
		
		document.rdForm.target = sOldTarget
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
		
		if (sUrl.toLowerCase().indexOf("javascript:") == 0) {
			//Not submitting the page, run javascript instead.  This works with Target.Link.
			var runScript = new Function(sUrl.substr(11))
			runScript()
			return
		}
		
		//If the URL has a ? at the end, remove it.
		if (sUrl.substring(sUrl.length-1,sUrl.length) == "?") {
			sUrl = sUrl.substring(0,sUrl.length - 1)
		}
		//Replace + with %20.
		var pattern = /\+/ig;
		sUrl = sUrl.replace(pattern,"%20");
		//Replace # with %23.
		var pattern = /\#/ig;
		sUrl = sUrl.replace(pattern,"%23");

		rdSaveInputCookies()

		switch (sTarget) {
			case '_parent':
				window.parent.location.href = sUrl
				break;
			case '_top':
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

	function SubmitFormCrawlerFriendly(sPage, sTarget, bValidate, sConfirm) {
		sPage = unescape(sPage)
		SubmitForm(sPage, sTarget, bValidate, sConfirm)
	}

	function NavigateCrawlerFriendly(sUrl, sTarget, bValidate, sFeatures, sConfirm) {
		sUrl = unescape(sUrl)
		NavigateLink2(sUrl, sTarget, bValidate, sFeatures, sConfirm)
	}


