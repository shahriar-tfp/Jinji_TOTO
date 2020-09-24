function rdSetScroll(){
	var x = rdGetScrollVar('rdScrollX');
	var y = rdGetScrollVar('rdScrollY');
    if((x > 0)||(y > 0)){ 
        if((top.opera)&&(typeof window.pageYOffset != 'undefined')){ 
            window.pageYOffset = x; 
            window.pageXOffset = y; 
        }else if((window.document.compatMode)&& 
                  (window.document.compatMode != 'BackCompat')){ 
            window.document.documentElement.scrollLeft = x; 
            window.document.documentElement.scrollTop = y; 
        }else if((window.document.body)&& 
              (typeof window.document.body.scrollTop != 'undefined')){ 
            window.document.body.scrollLeft = x; 
            window.document.body.scrollTop = y; 
        }else{ 
            window.scrollTo(x, y); 
        } 
    } 

}

function rdGetScroll(sAxis) { 
	var x;
	var y;
    if(typeof window.pageXOffset != 'undefined'){ 
        x = window.pageXOffset; 
        y = window.pageYOffset; 
    }else{ 
        if((!window.document.compatMode)|| 
          (window.document.compatMode == 'BackCompat')){ 
            x = window.document.body.scrollLeft; 
            y = window.document.body.scrollTop; 
        }else{ 
            x = window.document.documentElement.scrollLeft; 
            y = window.document.documentElement.scrollTop; 
        } 
    }
    if(sAxis=='y') {
		return y;
	} else {
		return x;
	}
}

function rdGetScrollVar(s){ 
  var temp = self.document.location.href; 
  if(temp.indexOf(s) >= 0){ 
    temp = 
      temp.substring((temp.indexOf(s)+ 
      (s.length+1)), temp.length); 
    temp = 
      temp.substring(0, (((temp.indexOf('&') >= 0)? 
      temp.indexOf('&'):temp.length))); 
  }else{ 
    temp = ''; 
  } 
  return unescape(temp); 
} 
 