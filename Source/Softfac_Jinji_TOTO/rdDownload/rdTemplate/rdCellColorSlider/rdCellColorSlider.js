function rdInitSlider(sSliderId, sSpectrum1Id, sSpectrum2Id, sRankColumnID, sColorColumnID, sColorLow, sColorMed, sColorHi, sColorAttribute, bForegroundBlackAndWhite) {
	if (sSliderId.length != 0) {
		var eleSlider = document.getElementById(sSliderId)
		var eleSpectrum1 = document.getElementById(sSpectrum1Id)
		var eleSpectrum2 = document.getElementById(sSpectrum2Id)
		var nSpectrumWidth = eleSpectrum1.width * 2
		
		if (nSpectrumWidth==0) {return}  //BODY isn't loaded yet.

		//Move the slider to the center.
		eleSlider.style.position = 'absolute'
		var xSpectrum = getObjectX(eleSpectrum1)
		var ySpectrum = getObjectY(eleSpectrum1)
		eleSlider.style.left = (xSpectrum + (eleSpectrum1.width) - (eleSlider.width / 2)) 
		eleSlider.style.top = ySpectrum - 2
		
		Drag.init(eleSlider, null, xSpectrum, xSpectrum + eleSpectrum1.width * 2, ySpectrum - 2, ySpectrum - 2);
		
		eleSlider.onDrag = function(x, y) {
			var nSlider = (eleSlider.lastMouseX - eleSlider.minMouseX) / (eleSlider.maxMouseX - eleSlider.minMouseX)
			rdDragCellColorSlider(nSlider, sRankColumnID, sColorColumnID, sSpectrum1Id, sSpectrum2Id, sColorLow, sColorMed, sColorHi, sColorAttribute, bForegroundBlackAndWhite)
		}
	}
	rdDragCellColorSlider(.5, sRankColumnID, sColorColumnID, sSpectrum1Id, sSpectrum2Id, sColorLow, sColorMed, sColorHi, sColorAttribute, bForegroundBlackAndWhite)
	rdRepositionSliders()
}

function rdDragCellColorSlider(nSlider, sRankColumnID, sColorColumnID, sSpectrum1Id, sSpectrum2Id, sColorLow, sColorMed, sColorHi, sColorAttribute, bForegroundBlackAndWhite) {
	//value is a number between 0 and 1.
	
	//Stretch and shrink the spectrum images.
	if (sSpectrum1Id.length != 0) {
		var eleSpectrum1 = document.getElementById(sSpectrum1Id)
		var eleSpectrum2 = document.getElementById(sSpectrum2Id)
		var nSpectrumWidth = eleSpectrum1.width + eleSpectrum2.width
		eleSpectrum1.width = nSpectrumWidth * nSlider
		eleSpectrum2.width = nSpectrumWidth - eleSpectrum1.width
	}	
	
	//Set the colors for all cells.
	var aHiddens = document.getElementsByTagName("rdCellSliderValue");
	for (var i=0; i < aHiddens.length; i++) {
		var eleHiddenValue = aHiddens[i]
		var sHiddenId = eleHiddenValue.parentNode.getAttribute("id")
		
		if (sHiddenId.indexOf(sRankColumnID + "_Row") != -1) { 
			var nCellSliderValue = eleHiddenValue.getAttribute("VALUE")
			var eleBackground = eleHiddenValue.parentNode.parentNode.parentNode  //This is the TD.
			var sCellColor = getCellColor(nSlider, parseFloat(nCellSliderValue), sColorLow, sColorMed, sColorHi)  
			
			if (sColorAttribute=='foreground') {
				if (eleBackground.getElementsByTagName('FONT')[0]) {
					eleBackground.getElementsByTagName('FONT')[0].color = sCellColor  //Works with colored <a> tags with IE and Mozilla
				} else {
					eleBackground.style.color = sCellColor  //In case the above doesn't work.
				}
			} else {    //background
				eleBackground.style.backgroundColor = sCellColor
				if (bForegroundBlackAndWhite) {
					//Standard formula for determining brightness.  Colors have different weights.
					var nBrightness = parseInt(sCellColor.substring(1,3),16) * .244627436 + parseInt(sCellColor.substring(3,5),16) * .672045616 + parseInt(sCellColor.substring(5,7),16) * .083326949
					if (nBrightness < 255 * .5) {
						eleBackground.style.color = "#FFFFFF"
					} else {
						eleBackground.style.color = "#000000"
					}
				}
			}
		}
	}
}


function getCellColor(nSlider,nCellValue, sColorLow, sColorMed, sColorHi) {

	//Adjust the cell value based according to the slider.
	if (nCellValue == 1) {
		
	}else if (nCellValue == 0) {
		 
	}else if (nCellValue <= nSlider) {
		nCellValue =   (.5 * nCellValue / nSlider ) 

	}else{
		nCellValue = 1 - (.5 / (1 - nSlider) * (1 - nCellValue))
	}

	var rLow = parseInt(sColorLow.substring(1,3),16)
	var gLow = parseInt(sColorLow.substring(3,5),16)
	var bLow = parseInt(sColorLow.substring(5,7),16)
	var rMed = parseInt(sColorMed.substring(1,3),16)
	var gMed = parseInt(sColorMed.substring(3,5),16)
	var bMed = parseInt(sColorMed.substring(5,7),16)
	var rHi = parseInt(sColorHi.substring(1,3),16)
	var gHi = parseInt(sColorHi.substring(3,5),16)
	var bHi = parseInt(sColorHi.substring(5,7),16)
	
	var factorLow = Math.sin((nCellValue + .5) * Math.PI)
	var factorMed = Math.sin(nCellValue * Math.PI)
	var factorHi = Math.sin((nCellValue - .5) * Math.PI)
	if (factorLow < 0) {factorLow=0}
	if (factorMed < 0) {factorMed=0}
	if (factorHi < 0) {factorHi=0}
	
	var r = (rLow * factorLow) + (rMed * factorMed) + (rHi * factorHi)
	var g = (gLow * factorLow) + (gMed * factorMed) + (gHi * factorHi)
	var b = (bLow * factorLow) + (bMed * factorMed) + (bHi * factorHi)
	
	return "#" + int2Hex(parseInt(r)) + int2Hex(parseInt(g)) + int2Hex(parseInt(b))

}


// [0-255] --> [00-ff] 
function int2Hex(nb) { 
    nb = (nb > 255)? 255: (nb < 0)? 0:nb; 
    n_ = Math.floor(nb/16); 
    n__ = nb % 16; 
    return  n_.toString(16) + n__.toString(16) 
} 

function getObjectX(eleObject) { 
  return(eleObject.offsetParent ? (getObjectX(eleObject.offsetParent) + eleObject.offsetLeft) : eleObject.offsetLeft); 
} 
function getObjectY(eleObject) { 
  return(eleObject.offsetParent ? (getObjectY(eleObject.offsetParent) + eleObject.offsetTop) : eleObject.offsetTop); 
} 

function rdRepositionSliders() {
	var aImages = document.getElementsByTagName("IMG");
	for (var i=0; i < aImages.length; i++) {
		var sSrc = aImages[i].getAttribute("src")
		if (sSrc) {
			if (sSrc.indexOf('rdCellColorSlider.png') != -1) { 
				var eleSlider = aImages[i]
				var sSliderId = eleSlider.getAttribute("id")	//sSliderId is something like:  rdCellColorSlider-da6b4e9c-ebb3-4d4d-b084-1b207d693dc0
				var sSpectrum1Id = sSliderId.replace('rdCellColorSlider','rdColorSpectrum1')
				var eleSpectrum1 = document.getElementById(sSpectrum1Id)
				var xSpectrum = getObjectX(eleSpectrum1)
				var ySpectrum = getObjectY(eleSpectrum1)
				eleSlider.style.left = xSpectrum + eleSpectrum1.width
				eleSlider.style.top = ySpectrum - 2
				var nWidth = eleSlider.maxX - eleSlider.minX
				eleSlider.minX = xSpectrum
				eleSlider.maxX = xSpectrum + nWidth
				eleSlider.minY = ySpectrum - 2
				eleSlider.maxY = ySpectrum - 2
			}
		}
	}
	
	
}
	