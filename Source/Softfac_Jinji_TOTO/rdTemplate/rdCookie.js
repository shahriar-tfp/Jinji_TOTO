function rdSaveInputCookie(sElementId)
{
	//Get a date a month from now.
	var expire,expireString;
	var month,year;
	expire=new Date();
	month = expire.getMonth();
	year  = expire.getFullYear();
	if(month == 11)
	{
		month = 0;
		year ++;
	}else{     month++;
	}
	expire.setMonth(month);
	expire.setFullYear(year);
	expireString = expire.toGMTString();

	//Set the cookie.
	var sValue 
	var ele = document.getElementById(sElementId)
	if (!ele) {return false}
	if (ele.type == "checkbox") {
		if (ele.checked) {
			sValue = ele.value
		}
		
	} else if (ele.tagName == "RDRADIOBUTTONGROUP") {
		var sElementId = ele.id.replace(/rdRadioButtonGroup/g, '')
		var cInputs = document.getElementsByTagName("INPUT")
		for (var i = 0; i < cInputs.length; i++) {
			if (cInputs[i].name == sElementId) {
				if (cInputs[i].checked) {
					sValue = cInputs[i].value
					break
				}
			}
		}

	} else {
		sValue = ele.value
	}
	SetCookie(sElementId,sValue,expireString)
}

function SetCookie(sName, sValue, sExpires)
{
	document.cookie = sName + "=" + encodeURI(sValue) + "; expires=" + sExpires + ";"
}




/**
 * Sets a Cookie with the given name and value.
 *
 * name       Name of the cookie
 * value      Value of the cookie
 * [expires]  Expiration date of the cookie (default: end of current session)
 * [path]     Path where the cookie is valid (default: path of calling document)
 * [domain]   Domain where the cookie is valid
 *              (default: domain of calling document)
 * [secure]   Boolean value indicating if the cookie transmission requires a
 *              secure transmission
 */
function setCookie2(name, value, expires, path, domain, secure)
{
    document.cookie= name + "=" + escape(value) +
        ((expires) ? "; expires=" + expires.toGMTString() : "") +
        ((path) ? "; path=" + path : "") +
        ((domain) ? "; domain=" + domain : "") +
        ((secure) ? "; secure" : "");
}

/**
 * Gets the value of the specified cookie.
 *
 * name  Name of the desired cookie.
 *
 * Returns a string containing value of specified cookie,
 *   or null if cookie does not exist.
 */
function getCookie2(name)
{
    var dc = document.cookie;
    var prefix = name + "=";
    var begin = dc.indexOf("; " + prefix);
    if (begin == -1)
    {
        begin = dc.indexOf(prefix);
        if (begin != 0) return null;
    }
    else
    {
        begin += 2;
    }
    var end = document.cookie.indexOf(";", begin);
    if (end == -1)
    {
        end = dc.length;
    }
    return unescape(dc.substring(begin + prefix.length, end));
}

/**
 * Deletes the specified cookie.
 *
 * name      name of the cookie
 * [path]    path of the cookie (must be same as path used to create cookie)
 * [domain]  domain of the cookie (must be same as domain used to create cookie)
 */
function deleteCookie2(name, path, domain)
{
    if (getCookie(name))
    {
        document.cookie = name + "=" + 
            ((path) ? "; path=" + path : "") +
            ((domain) ? "; domain=" + domain : "") +
            "; expires=Thu, 01-Jan-70 00:00:01 GMT";
    }
}
