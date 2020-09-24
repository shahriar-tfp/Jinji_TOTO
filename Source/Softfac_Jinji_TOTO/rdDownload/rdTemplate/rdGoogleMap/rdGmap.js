function rdGmapLoad(sMapID) {

    if (GBrowserIsCompatible()) {
        var spanMap = document.getElementById(sMapID)
        
        var nMapTypes = 0
        var sMapTypes = spanMap.getAttribute("GoogleMapTypes")
        if (sMapTypes) {
            var asMapTypes = spanMap.getAttribute("GoogleMapTypes").split(",")
            var aMapTypes = new Array()
            for (var i=0; i < asMapTypes.length; i++) {
                switch (asMapTypes[i]) {
                    case 'Map':
                        aMapTypes[i] = G_NORMAL_MAP
                        nMapTypes += 1
                        break;
                    case 'Satellite':
                        aMapTypes[i] = G_SATELLITE_MAP
                        nMapTypes += 1
                        break;
                    case 'Hybrid':
                        aMapTypes[i] = G_HYBRID_MAP
                        nMapTypes += 1
                        break;
                    default:
                        aMapTypes[i] = G_DEFAULT_MAP_TYPES
                        nMapTypes = 999
                        break;
                }
            }
        } else {
            aMapTypes = G_DEFAULT_MAP_TYPES
            nMapTypes = 999
        }
        
        var map = new GMap2(document.getElementById(sMapID),{mapTypes:aMapTypes});
        map.setCenter(new GLatLng(0, 0), 0);  //Init the map.
        
        //Add controls
        switch (spanMap.getAttribute("GoogleMapControl")) {
            case 'None':
                break
            case 'SmallZoomControl':
                map.addControl(new GSmallZoomControl())
                break
            case 'SmallMapControl':
                map.addControl(new GSmallMapControl())
                break
            default:  // 'LargeMapControl'
                map.addControl(new GLargeMapControl())
                break
        }
        if (spanMap.getAttribute("MapScale")=="True") {
            map.addControl(new GScaleControl())
        }
        
        if (nMapTypes > 1){
            map.addControl(new GMapTypeControl());
        }

        
        var bounds = new GLatLngBounds() 
        
        var aMapMarkerRows = document.getElementsByTagName(sMapID + "_rdMapMarker");
        for (var i=0; i < aMapMarkerRows.length; i++) {
	        var eleMapMarkerRow = aMapMarkerRows[i]
	        //Validate the marker.
	        var lat = parseFloat(eleMapMarkerRow.getAttribute("Latitude"))
	        var lng = parseFloat(eleMapMarkerRow.getAttribute("Longitude"))
	        if (isNaN(lat) || isNaN(lng)){continue}
	        if (lat==0 && lng==0) {continue}
	        
	        var options = new Object()  //GMarkerOptions object
	        
            var point = new GLatLng(lat, lng)
            
//          //Marker image.
            var eleMarkerImage = document.getElementById("rdMapMarkerImage_Row" + (i + 1))
            if (eleMarkerImage) {
                var widthImage = eleMarkerImage.getAttribute("width")
                var heightImage = eleMarkerImage.getAttribute("height")
                if (widthImage==0) { //IE?
                    widthImage = parseInt(eleMarkerImage.currentStyle.width)
                    heightImage = parseInt(eleMarkerImage.currentStyle.height)
                }
                var icon = new GIcon()
                icon.image = eleMarkerImage.getAttribute("src")
                icon.iconSize = new GSize(widthImage, heightImage)
                //icon.shadow = eleMarkerImage.getAttribute("src")
                //icon.shadowSize = new GSize(widthImage, heightImage)
                icon.iconAnchor = new GPoint(widthImage / 2, heightImage) //bottom middle.
                icon.infoWindowAnchor = new GPoint(widthImage / 2, 0)     //top middle.
                options.icon = icon
                
                //Tooltip
                if (eleMarkerImage.title) {
                    options.title = eleMarkerImage.title
                }
                
                //Don't want this to appear in the Info bubble.
                eleMarkerImage.parentNode.removeChild(eleMarkerImage)
            }
            
            
            var eleMarker = document.getElementById(eleMapMarkerRow.getAttribute("rdMarkerID"))
            var marker = new GMarker(point, options);
            rdCreateMarkerAction(marker, eleMarker, eleMapMarkerRow.getAttribute("rdMarkerActionSpanID"))
            map.addOverlay(marker)
            
            //Add the marker to the bounds, extending the bounds.
            bounds.extend(point); 

        }
        
        //Set the location
        var zoom = map.getBoundsZoomLevel(bounds)
        map.setZoom(zoom); 
        map.setCenter(bounds.getCenter()); 
      
        //Googles getBoundsZoomLevel() isn't perfect.  This adds calcs some margin and zooms out if necessary.
        var bZoomOut = false 
        var point = map.fromLatLngToDivPixel(bounds.getNorthEast())
        if (point.y - spanMap.clientHeight * .1 < 0) {bZoomOut = true}
        if (point.x > spanMap.clientHeight * 1.1) {bZoomOut = true}
        var point = map.fromLatLngToDivPixel(bounds.getSouthWest())
        if (point.x - spanMap.clientHeight * .1 < 0) {bZoomOut = true}
        if (point.y > spanMap.clientHeight * 1.1) {bZoomOut = true}
        if (bZoomOut) {
            map.setZoom(zoom - 1)
            map.setCenter(bounds.getCenter())
        }


        //Google Maps needs to unload.     
        if (window.attachEvent) { 
            window.attachEvent("onunload", function() { 
                try {
                    GUnload();      // IE - The try was added because of a GoogleMaps bug on April 3, 07.
                }
                catch (e) {}
            }); 
        } else { 
            window.addEventListener("unload", function() { 
                GUnload(); // Firefox and standard browsers 
            }, false); 
        } 
            
    }
}

function rdCreateMarkerAction(marker, eleMarker, sMarkerActionSpanID) {
    if (eleMarker) {
        if (eleMarker.getAttribute("rdActionMapMarkerInfo")=="true") {
            //A bubble-style Info window.
            GEvent.addListener(marker, "click", function() {
                //This is for IFRAMES (SubReports) that may be in the info panel.
                cFrames = eleMarker.getElementsByTagName("IFRAME");
			    for (var i = 0; i < cFrames.length; i++) {
			        var cFrame = cFrames[i];
			        var sSrc = cFrame.getAttribute("HiddenSource");
			        if (sSrc != null) {   //There is no HiddenSource if the element was initially visible.
				        if (cFrame.getAttribute("src") == null) {   //For nonIE
					        cFrame.setAttribute("src", sSrc + "&rdRnd=" + Math.floor(Math.random() * 100000));
				        }										    //For IE.
				        if (cFrame.getAttribute("src").indexOf(sSrc) == -1) {
					        cFrame.setAttribute("src", sSrc + "&rdRnd=" + Math.floor(Math.random() * 100000));
				        }
			        }
		        }
                
            marker.openInfoWindowHtml(eleMarker.innerHTML)
            });
            
        } else if (sMarkerActionSpanID) {
            var eleActionSpan = document.getElementById(sMarkerActionSpanID)
            if (eleActionSpan) {
                GEvent.addListener(marker, "click", function() {eleActionSpan.click()} )
            }
        } else {
            //No Action for the image.
        }
    }
}