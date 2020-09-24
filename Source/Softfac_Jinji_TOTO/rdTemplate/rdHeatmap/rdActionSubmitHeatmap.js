	function SubmitHeatmap(sPage, sTarget, bValidate, sConfirm) {
		//These replace values from the data.
		sPage = sPage.replace('hm_amp;','%26')
		sPage = sPage.replace('hm_apos;',"'")
		SubmitForm(sPage, sTarget, bValidate, sConfirm)
	}

