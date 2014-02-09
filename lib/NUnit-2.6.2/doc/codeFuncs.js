window.onload = init;

var langElements = new Array();

function init() {
	var els = document.getElementsByTagName( 'pre' );
	var elsLen = els.length;
	var pattern = new RegExp('(^|\\s)(cs|vb|mc|js)(\\s|$)');
	for (i = 0, j = 0; i < elsLen; i++) {
		if ( pattern.test(els[i].className) ) {
		   //els[i].style.background = "#fcc";
		   langElements[j] = els[i];
		   j++;
		}
	}
	
	var lang = getCookie( "lang" );
	if ( lang == null ) lang = "cs";
	showLang(lang);
}

function getCookie(name) {
 	var cname = name + "=";
	var dc = document.cookie;
	if ( dc.length > 0 ) {
	   begin = dc.indexOf(cname);
	   if ( begin != -1 ) {
	   	  begin += cname.length;
		  end = dc.indexOf(";",begin);
		  if (end == -1) end = dc.length;
		  return unescape(dc.substring(begin, end) );
	   }
	}
}

function setCookie(name,value,expires) {
	document.cookie = name + "=" + escape(value) + "; path=/" +
	((expires == null) ? "" : "; expires=" + expires.toGMTString());
}

function showLang(lang) {
	var pattern = new RegExp('(^|\\s)'+lang+'(\\s|$)');
	var elsLen = langElements.length;
	for (i = 0; i < elsLen; i++ )
	{
	 	var el = langElements[i];
		if ( pattern.test( el.className ) )
		   el.style.display = "";
		else
		   el.style.display = "none";
	}
	setCookie("lang",lang);
}

function Show( id ) {
	document.getElementById(id).style.display = "";
}

function Hide( id ) {
	document.getElementById(id).style.display = "none";
}

function ShowCS() {
	showLang('cs');
}

function ShowVB() {
	showLang('vb');
}

function ShowMC() {
	showLang('mc');
}

function ShowJS() {
	showLang('js');
}
