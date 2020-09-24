function iframeResize(o){
  if (o.src != "") {
    if (frames[o.id]) {
		//IE
		var h=1, d=frames[o.id].document;
		if (d.body) {
		    if (d.body.offsetHeight==1) { //The height is set to 1 when the developer doesn't set a specific height.
			    if(d.body.scrollHeight!=undefined)
				    h=d.body.scrollHeight;
			    else if(d.height!=undefined)
				    h=document.height;

			    if(d.body.currentStyle){
				    var cS=d.body.currentStyle;
				    h += parseInt(cS.marginTop,10),
				    h += parseInt(cS.marginBottom,10);
				    if (isNaN(h)) {
					    h = 200;  //default value when we can't figure out the actual height.  (Frames?)
				    }
			    }else if (window.getComputedStyle){
				    h += parseInt(window.getComputedStyle(d.body,"").getPropertyValue("margin-top"),10);
				    h += parseInt(window.getComputedStyle(d.body,"").getPropertyValue("margin-bottom"),10);
			    }

			    
                if (d.body.offsetWidth!=1) {
                    if (d.body.clientWidth < d.body.scrollWidth) {
				        if (o.clientWidth != 0) {
					        // There's a horizontal scroll bar.  Add to the height to make up for it.
					        h += 17   //Constant size for scrollbar.
				        }
			        }
			    }
    			
			    if (o.frameBorder != "No")
				    h += 4   //Add some height for the border.
    			
			    o.height = h + "px";
    			
			}
			
			
			
		    if (d.body.offsetWidth==1) { //The width is set to 1 when the developer doesn't set a specific width.
			
			    if(d.body.scrollWidth!=undefined)
				    w=d.body.scrollWidth;
			    else if(d.width!=undefined)
				    w=document.width;

			    if(d.body.currentStyle){
				    var cS=d.body.currentStyle;
				    w += parseInt(cS.marginLeft,10),
				    w += parseInt(cS.marginRight,10);
				    if (isNaN(h)) {  
				        return  //Got an error
				    }
			    }else if (window.getComputedStyle){
				    w += parseInt(window.getComputedStyle(d.body,"").getPropertyValue("margin-left"),10);
				    w += parseInt(window.getComputedStyle(d.body,"").getPropertyValue("margin-right"),10);
			    }

                if (d.body.clientHeight < d.body.scrollHeight) {
			        if (o.clientHeight != 0) {
				        // There's a vertical scroll bar.  Add to the width to make up for it.
				        w += 17   //Constant size for scrollbar.
			        }
		        }

			    if (o.frameBorder != "No")
				    w += 4   //Add some width for the border.
    			
			    o.width = w + "px";
			}
		
		}
	} else {
		// Non-IE.
		    //Height
		    if (o.height==1) {
			    if(o.contentDocument.body.scrollHeight) {
				    o.height=o.contentDocument.body.scrollHeight;
                    if (o.width!=1) {
				        if (o.scrollWidth < o.contentDocument.body.scrollWidth) {
			                if (o.scrollWidth != 0) {
				                // There's a horizontal scroll bar.  Add to the height to make up for it.
				                o.height = parseInt(o.height) + 17   //Constant size for scrollbar.
			                }
			            }
                    }
			    }
			    else {
				    o.height=o.contentDocument.height + 4
			    }
			}
			
			//Width
		    if (o.width==1) {
			    if(o.contentDocument.body.scrollWidth) {
				    o.width=o.contentDocument.body.scrollWidth;
				    if (o.scrollHeight < o.contentDocument.body.scrollHeight) {
			            if (o.scrollHeight != 0) {
				            // There's a vertical scroll bar.  Add to the width to make up for it.
				            o.width = parseInt(o.width) + 17   //Constant size for scrollbar.
			            }
			        }
		        }   

			    else {
				    o.width=o.contentDocument.width + 4
			    }
			}
			
		}
	}
	
	//Does this frame have a parent that needs to be resized?
	if (frameElement) {
		if (parent.iframeResize) {
			parent.iframeResize(frameElement)
		}
	}
	
}
