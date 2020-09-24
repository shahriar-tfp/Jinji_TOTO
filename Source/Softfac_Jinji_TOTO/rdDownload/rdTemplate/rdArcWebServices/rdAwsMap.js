
var gMapID

//loads the ArcWeb Explorer SWF map  
function rdAwsLoad(sMapID, sAwsKey)
{
    gMapID = sMapID
    AWUtils.insertMap(sMapID, sAwsKey );
    //AWUtils.insertMap(sMapID, sAwsKey,{glt:"mapImageGroupLayer", ds:"ArcWeb:TC.Traffic.US"} );
    //AWUtils.insertMap(sMapID, sAwsKey,{glt:"mapImageGroupLayer", ds:"LogiXML:RoadsAndSchoolDistricts"} );
    //AWUtils.insertMap(sMapID, sAwsKey,{glt:"mapImageGroupLayer", ds:"LogiXML:CassCo Shool Districts"} );
    //AWUtils.insertMap(sMapID, sAwsKey,{glt:"mapImageGroupLayer", ds:"LogiXML:Hydrants5"} );
        
    
    // The following is a hack for IE to enable a flash map object in a FORM tag.
    // This is _not_ needed for FireFox :-)
    AWUtils.addFlashCallbacks(sMapID);

}

function onCreationComplete() {
    //creating an instance of AWMap
    var map = new AWMap(gMapID);
    

//            //setting the visiblity for various layers in the datasource.
//            var mylayerVisiblity = [
//                {name:"U.S. Highways", visibility:"off"},
//                {name:"U.S. Counties", visibility:"off"},
//                {name:"Oceans and Seas Names", visibility:"off"},
//                {name:"Satellite Imagery (150m)", visibility:"off"},
//                {name:"Satellite Imagery (1km)", visibility:"off"},
//                {name:"U.S. States", visibility:"off"},
//                {name:"Satellite Imagery (5km)", visibility:"off"},
//                {name:"Country Boundaries", visibility:"off"},
//                {name:"Current Weather", visibility:"on"},
//                {name:"CA Cities", visibility:"off"},
//                {name:"States and Provinces", visibility:"off"},
//                {name:"U.S. Cities", visibility:"off"},
//                {name:"CA Highways", visibility:"off"}
//            ];
//    

    var latMin = 90
    var latMax = -90
    var lngMin = 180
    var lngMax = -180
    
    var aMarkers = new Array()
    var aMapMarkerRows = document.getElementsByTagName(gMapID + "_rdMapMarker");
    for (var i=0; i < aMapMarkerRows.length; i++) {
        var eleMapMarkerRow = aMapMarkerRows[i]
        //Validate the marker.
        var latMarker = parseFloat(eleMapMarkerRow.getAttribute("Latitude"))
        var lngMarker = parseFloat(eleMapMarkerRow.getAttribute("Longitude"))
        if (isNaN(latMarker) || isNaN(lngMarker)){continue}
        if (latMarker==0 && lngMarker==0) {continue}

        latMin = Math.min(latMin,latMarker)         
        latMax = Math.max(latMax,latMarker)         
        lngMin = Math.min(lngMin,lngMarker)         
        lngMax = Math.max(lngMax,lngMarker)
                 
        //Create the marker style
        var markerStyle = new AWImgMarkerStyle();
        markerStyle.id = "style_" + i
        
        //Marker icon
        var sTooltip
        var eleMarkerImage = document.getElementById("rdMapMarkerImage_Row" + (i + 1))
        if (eleMarkerImage) {
            var widthImage = eleMarkerImage.getAttribute("width")
            var heightImage = eleMarkerImage.getAttribute("height")
            if (widthImage==0) { //IE?
                widthImage = parseInt(eleMarkerImage.currentStyle.width)
                heightImage = parseInt(eleMarkerImage.currentStyle.height)
            }
            markerStyle.source = eleMarkerImage.getAttribute("src")
            markerStyle.width = widthImage
            markerStyle.height = heightImage
            markerStyle.anchorX = widthImage / 2  //middle.
            markerStyle.anchorY = heightImage     //bottom.
            //markerStyle.alpha = 50
            
            //Action (Click event)
            var eleMarker = document.getElementById(eleMapMarkerRow.getAttribute("rdMarkerID"))
            if (eleMarker) {
            
                if (eleMarker.getAttribute("rdActionMapMarkerInfo")=="true") {
                    alert("Action.MapMarkerInfo not supported for ArcWebServices maps.")
        
                } else {
                    var sMarkerActionSpanID = eleMapMarkerRow.getAttribute("rdMarkerActionSpanID")
                    if (sMarkerActionSpanID) {
                        var eleActionSpan = document.getElementById(sMarkerActionSpanID)
                        var sHref = eleActionSpan.parentElement.getAttribute("href")
                        if (sHref) {
                            sHref = unescape(sHref.replace("javascript:",""))
                            markerStyle.mouseClick = sHref
                        }
                    } else {
                        //No Action for the image.
                    }
                }
            }

//            //Prototype code for popup bubble window    
//                var eleA = eleActionParent.firstChild
//                if (eleA) {
//                    var sHref = eleA.getAttribute("href")
//                    if (sHref) {
//                        sHref = unescape(sHref.replace("javascript:",""))
//                        if (eleAction.innerHTML.indexOf("rdMapMarkerActionLabel") != -1) {
//                            markerStyle.mouseClick = sHref
//                        } else {
//                            //A bubble-style Info window.
//                            markerStyle.mouseClick = function() {
//                                //ShowElement(eleInfo.id,eleInfo.id,'Show') 
//                                cFrames = eleInfo.getElementsByTagName("IFRAME");
//			                    for (var i = 0; i < cFrames.length; i++) {
//			                        var cFrame = cFrames[i];
//			                        var sSrc = cFrame.getAttribute("HiddenSource");
//			                        if (sSrc != null) {   //There is no HiddenSource if the element was initially visible.
//				                        if (cFrame.getAttribute("src") == null) {   //For nonIE
//					                        cFrame.setAttribute("src", sSrc + "&rdRnd=" + Math.floor(Math.random() * 100000));
//				                        }										    //For IE.
//				                        if (cFrame.getAttribute("src").indexOf(sSrc) == -1) {
//					                        cFrame.setAttribute("src", sSrc + "&rdRnd=" + Math.floor(Math.random() * 100000));
//				                        }
//			                        }
//		                        }
//                                
//                                marker.openInfoWindowHtml(eleInfo.innerHTML)
//                            }  // end function()
//                        }
//                    }
//                }
//            }
            
            
            
            
            //Tooltip
            if (eleMarkerImage.title) {
                sTooltip = eleMarkerImage.title
            }
            
        }

        map.addMarkerStyle(markerStyle)
        
        //Create the marker 
        var marker = new AWMarker()
        marker.id = eleMapMarkerRow.getAttribute("rdMarkerID")
        marker.latlon = new AWLatLon(latMarker,lngMarker)
        marker.markerStyleId = markerStyle.id
        if (sTooltip) {
            marker.data = {label:sTooltip}
        }
        
        
//        var eleInfo = {
//            type : "text",
//            text : "some text",
//            multiline : true,
//            maxWidth : 100,
//            border : true,
//            useSS : true,
//            url : element[j].getAttribute("url"),
//            target : element[j].getAttribute("target")
//        };
////        var eleInfo = {
////            type : "text",
////            text : "details about this 7-11",
////            multiline : true,
////            border : false,
////            useSS : true
////        };

//        var aInfos = new Array()
//        aInfos.push(eleInfo)
        
        
        
//        //marker.data = {dropShadow:true, label:sTooltip, desc:"xxxxxxxxxxxxx"}
//        //marker.data = {dropShadow:true, label:sTooltip, elements:aInfos}
//        var lbl = {
//            text : sTooltip,
//            icon : "_Images/7-11.jpg"
//        };
////        var lbl = {
////            text,
////            icon
////        };
////   
//     var lbl
////        lbl.sTooltip = sTooltip
////        lbl.icon = "_Images/7-11.jpg"

//        marker.data = {dropShadow:true, label:lbl, elements:aInfos}

        aMarkers.push(marker)

    }
    if (aMarkers.length != 0) {
        map.addMarker(aMarkers)
    }

    var latCenter = (latMin + latMax) / 2;
    var lngCenter = (lngMin + lngMax) / 2;
    var extent = new AWLatLonExtent(latMin,lngMin,latMax,lngMax)
    var scale = map.getScaleForLatLonExtent(extent)
    map.centerAndScale(new AWLatLon(latCenter,lngCenter), scale * 1.4)
    

    map.showWidget(AWMap.WIDGET_WIDGETBAR,0,0)
    

}