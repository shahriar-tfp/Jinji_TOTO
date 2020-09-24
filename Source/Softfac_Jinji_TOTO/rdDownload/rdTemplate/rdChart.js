
function rdChartLoad(imgChart) {
	if (imgChart.width == 243 && imgChart.height == 35) {
		//There was an error creating the chart.
		//We know because the image size matches the size of rdChartError.gif
		//Redirect the target URL to the chart that generated an error.
		//But this time the chart is run against the top-level window, so all of error page will be visible.
		if (imgChart.parentNode.tagName != "A") {
			parentChart = imgChart.parentNode
			//Create a link pointing to the error page.
			var aLink = document.createElement("A")
			aLink.href = imgChart.src + "&rdChartDebug=True"
			//Make a new IMG inside of the anchor that points to the error GIF.
			var imgError = document.createElement("IMG")
			imgError.src = "rdTemplate/rdChartError.gif"
			aLink.appendChild(imgError)
			parentChart.appendChild(aLink)
			//Remove the chart image.
			parentChart.removeChild(imgChart)
		}
	} else {
		//Not an error.  Does this chart support drill-down?
		if (imgChart.getAttribute("useMap")) { 
			rdMakeDrilldownAfterBodyLoad(imgChart.getAttribute("rdDrillDownID"))
		}
	}
}

function rdMakeDrilldownAfterBodyLoad(sFrameID) {
	if (document.rdBodyLoaded) {
		rdLoadChartMap(sFrameID)
	} else {
		setTimeout("rdMakeDrilldownAfterBodyLoad('" + sFrameID + "')", 1000)
	}
}

function rdLoadChartMap(sFrameID) {
	//Create an IFrame control that will get the image map.
	var fraImageMap=document.createElement("IFRAME");
	fraImageMap.id = sFrameID
	fraImageMap.height = 0; fraImageMap.width = 0
	//Some browsers like onload, others onreadystatechange. 
	fraImageMap.onload= new Function("chartLoadImageMap('" + sFrameID + "')")
	fraImageMap.onreadystatechange= new Function("chartLoadImageMap('" + sFrameID + "')")
	fraImageMap.src = "rdChart.aspx?rdDrillDownID=" + sFrameID
	document.body.appendChild(fraImageMap)
}

function chartLoadImageMap(sFrameID) {
	
	var fraImageMap = document.getElementById(sFrameID)
	var sAreaHtml
	if (document.getElementById(sFrameID).contentWindow) {
		//IE, Mozilla
		sAreaHtml = fraImageMap.contentWindow.window.document.firstChild.innerHTML
	} else {
		if (fraImageMap.document.firstChild.firstChild.tagName=="MAP") {
			//Opera
			sAreaHtml = fraImageMap.document.firstChild.firstChild.innerHTML
		} else {
			//Safari
			sAreaHtml = fraImageMap.document.firstChild.firstChild.firstChild.innerHTML
		}
	}
	var map = document.createElement("MAP")
	map.name = sFrameID
	map.innerHTML = sAreaHtml
	document.body.appendChild(map)	
	fraImageMap.style.display = "none"
}