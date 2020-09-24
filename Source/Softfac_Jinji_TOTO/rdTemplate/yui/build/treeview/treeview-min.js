/*                                                                                                                                                      Copyright (c) 2006, Yahoo! Inc. All rights reserved.                                                                                                    Code licensed under the BSD License:                                                                                                                    http://developer.yahoo.net/yui/license.txt                                                                                                              version: 0.11.0                                                                                                                                         */ YAHOO.widget.TreeView=function(id){if(id){this.init(id);}};YAHOO.widget.TreeView.nodeCount=0;YAHOO.widget.TreeView.prototype={id:null,_el:null,_nodes:null,locked:false,_expandAnim:null,_collapseAnim:null,_animCount:0,maxAnim:2,setExpandAnim:function(_2){if(YAHOO.widget.TVAnim.isValid(_2)){this._expandAnim=_2;}},setCollapseAnim:function(_3){if(YAHOO.widget.TVAnim.isValid(_3)){this._collapseAnim=_3;}},animateExpand:function(el){if(this._expandAnim&&this._animCount<this.maxAnim){var _5=this;var a=YAHOO.widget.TVAnim.getAnim(this._expandAnim,el,function(){_5.expandComplete();});if(a){++this._animCount;a.animate();}return true;}return false;},animateCollapse:function(el){if(this._collapseAnim&&this._animCount<this.maxAnim){var _7=this;var a=YAHOO.widget.TVAnim.getAnim(this._collapseAnim,el,function(){_7.collapseComplete();});if(a){++this._animCount;a.animate();}return true;}return false;},expandComplete:function(){--this._animCount;},collapseComplete:function(){--this._animCount;},init:function(id){this.id=id;if("string"!==typeof id){this._el=id;this.id=this.generateId(id);}this._nodes=[];YAHOO.widget.TreeView.trees[this.id]=this;this.root=new YAHOO.widget.RootNode(this);},draw:function(){var _8=this.root.getHtml();this.getEl().innerHTML=_8;this.firstDraw=false;},getEl:function(){if(!this._el){this._el=document.getElementById(this.id);}return this._el;},regNode:function(_9){this._nodes[_9.index]=_9;},getRoot:function(){return this.root;},setDynamicLoad:function(_10,_11){this.root.setDynamicLoad(_10,_11);},expandAll:function(){if(!this.locked){this.root.expandAll();}},collapseAll:function(){if(!this.locked){this.root.collapseAll();}},getNodeByIndex:function(_12){var n=this._nodes[_12];return (n)?n:null;},getNodeByProperty:function(_14,_15){for(var i in this._nodes){var n=this._nodes[i];if(n.data&&_15==n.data[_14]){return n;}}return null;},getNodesByProperty:function(_17,_18){var _19=[];for(var i in this._nodes){var n=this._nodes[i];if(n.data&&_18==n.data[_17]){_19.push(n);}}return (_19.length)?_19:null;},removeNode:function(_20,_21){if(_20.isRoot()){return false;}var p=_20.parent;if(p.parent){p=p.parent;}this._deleteNode(_20);if(_21&&p&&p.childrenRendered){p.refresh();}return true;},removeChildren:function(_23){while(_23.children.length){this._deleteNode(_23.children[0]);}_23.childrenRendered=false;_23.dynamicLoadComplete=false;_23.expand();_23.collapse();},_deleteNode:function(_24){this.removeChildren(_24);this.popNode(_24);},popNode:function(_25){var p=_25.parent;var a=[];for(var i=0,len=p.children.length;i<len;++i){if(p.children[i]!=_25){a[a.length]=p.children[i];}}p.children=a;p.childrenRendered=false;if(_25.previousSibling){_25.previousSibling.nextSibling=_25.nextSibling;}if(_25.nextSibling){_25.nextSibling.previousSibling=_25.previousSibling;}delete this._nodes[_25.index];},toString:function(){return "TreeView "+this.id;},generateId:function(el){var id=el.id;if(!id){id="yui-tv-auto-id-"+YAHOO.widget.TreeView.counter;YAHOO.widget.TreeView.counter++;}return id;},onExpand:function(_26){},onCollapse:function(_27){}};YAHOO.widget.TreeView.trees=[];YAHOO.widget.TreeView.counter=0;YAHOO.widget.TreeView.getTree=function(_28){var t=YAHOO.widget.TreeView.trees[_28];return (t)?t:null;};YAHOO.widget.TreeView.getNode=function(_30,_31){var t=YAHOO.widget.TreeView.getTree(_30);return (t)?t.getNodeByIndex(_31):null;};YAHOO.widget.TreeView.addHandler=function(el,_32,fn,_34){_34=(_34)?true:false;if(el.addEventListener){el.addEventListener(_32,fn,_34);}else{if(el.attachEvent){el.attachEvent("on"+_32,fn);}else{el["on"+_32]=fn;}}};YAHOO.widget.TreeView.preload=function(_35){_35=_35||"ygtv";var _36=["tn","tm","tmh","tp","tph","ln","lm","lmh","lp","lph","loading"];var sb=[];for(var i=0;i<_36.length;++i){sb[sb.length]="<span class=\""+_35+_36[i]+"\">&#160;</span>";}var f=document.createElement("DIV");var s=f.style;s.position="absolute";s.top="-1000px";s.left="-1000px";f.innerHTML=sb.join("");document.body.appendChild(f);};YAHOO.widget.TreeView.addHandler(window,"load",YAHOO.widget.TreeView.preload);YAHOO.widget.Node=function(_40,_41,_42){if(_40){this.init(_40,_41,_42);}};YAHOO.widget.Node.prototype={index:0,children:null,tree:null,data:null,parent:null,depth:-1,href:null,target:"_self",expanded:false,multiExpand:true,renderHidden:false,childrenRendered:false,dynamicLoadComplete:false,previousSibling:null,nextSibling:null,_dynLoad:false,dataLoader:null,isLoading:false,hasIcon:true,iconMode:0,_type:"Node",init:function(_43,_44,_45){this.data=_43;this.children=[];this.index=YAHOO.widget.TreeView.nodeCount;++YAHOO.widget.TreeView.nodeCount;this.expanded=_45;if(_44){_44.appendChild(this);}},applyParent:function(_46){if(!_46){return false;}this.tree=_46.tree;this.parent=_46;this.depth=_46.depth+1;if(!this.href){this.href="javascript:"+this.getToggleLink();}if(!this.multiExpand){this.multiExpand=_46.multiExpand;}this.tree.regNode(this);_46.childrenRendered=false;for(var i=0,len=this.children.length;i<len;++i){this.children[i].applyParent(this);}return true;},appendChild:function(_47){if(this.hasChildren()){var sib=this.children[this.children.length-1];sib.nextSibling=_47;_47.previousSibling=sib;}this.children[this.children.length]=_47;_47.applyParent(this);return _47;},appendTo:function(_49){return _49.appendChild(this);},insertBefore:function(_50){var p=_50.parent;if(p){if(this.tree){this.tree.popNode(this);}var _51=_50.isChildOf(p);p.children.splice(_51,0,this);if(_50.previousSibling){_50.previousSibling.nextSibling=this;}this.previousSibling=_50.previousSibling;this.nextSibling=_50;_50.previousSibling=this;this.applyParent(p);}return this;},insertAfter:function(_52){var p=_52.parent;if(p){if(this.tree){this.tree.popNode(this);}var _53=_52.isChildOf(p);if(!_52.nextSibling){return this.appendTo(p);}p.children.splice(_53+1,0,this);_52.nextSibling.previousSibling=this;this.previousSibling=_52;this.nextSibling=_52.nextSibling;_52.nextSibling=this;this.applyParent(p);}return this;},isChildOf:function(_54){if(_54&&_54.children){for(var i=0,len=_54.children.length;i<len;++i){if(_54.children[i]===this){return i;}}}return -1;},getSiblings:function(){return this.parent.children;},showChildren:function(){if(!this.tree.animateExpand(this.getChildrenEl())){if(this.hasChildren()){this.getChildrenEl().style.display="";}}},hideChildren:function(){if(!this.tree.animateCollapse(this.getChildrenEl())){this.getChildrenEl().style.display="none";}},getElId:function(){return "ygtv"+this.index;},getChildrenElId:function(){return "ygtvc"+this.index;},getToggleElId:function(){return "ygtvt"+this.index;},getEl:function(){return document.getElementById(this.getElId());},getChildrenEl:function(){return document.getElementById(this.getChildrenElId());},getToggleEl:function(){return document.getElementById(this.getToggleElId());},getToggleLink:function(){return "YAHOO.widget.TreeView.getNode('"+this.tree.id+"',"+this.index+").toggle()";},collapse:function(){if(!this.expanded){return;}var ret=this.tree.onCollapse(this);if("undefined"!=typeof ret&&!ret){return;}if(!this.getEl()){this.expanded=false;return;}this.hideChildren();this.expanded=false;if(this.hasIcon){this.getToggleEl().className=this.getStyle();}},expand:function(){if(this.expanded){return;}var ret=this.tree.onExpand(this);if("undefined"!=typeof ret&&!ret){return;}if(!this.getEl()){this.expanded=true;return;}if(!this.childrenRendered){this.getChildrenEl().innerHTML=this.renderChildren();}else{}this.expanded=true;if(this.hasIcon){this.getToggleEl().className=this.getStyle();}if(this.isLoading){this.expanded=false;return;}if(!this.multiExpand){var _56=this.getSiblings();for(var i=0;i<_56.length;++i){if(_56[i]!=this&&_56[i].expanded){_56[i].collapse();}}}this.showChildren();},getStyle:function(){if(this.isLoading){return "ygtvloading";}else{var loc=(this.nextSibling)?"t":"l";var _58="n";if(this.hasChildren(true)||(this.isDynamic()&&!this.getIconMode())){_58=(this.expanded)?"m":"p";}return "ygtv"+loc+_58;}},getHoverStyle:function(){var s=this.getStyle();if(this.hasChildren(true)&&!this.isLoading){s+="h";}return s;},expandAll:function(){for(var i=0;i<this.children.length;++i){var c=this.children[i];if(c.isDynamic()){alert("Not supported (lazy load + expand all)");break;}else{if(!c.multiExpand){alert("Not supported (no multi-expand + expand all)");break;}else{c.expand();c.expandAll();}}}},collapseAll:function(){for(var i=0;i<this.children.length;++i){this.children[i].collapse();this.children[i].collapseAll();}},setDynamicLoad:function(_60,_61){if(_60){this.dataLoader=_60;this._dynLoad=true;}else{this.dataLoader=null;this._dynLoad=false;}if(_61){this.iconMode=_61;}},isRoot:function(){return (this==this.tree.root);},isDynamic:function(){var _62=(!this.isRoot()&&(this._dynLoad||this.tree.root._dynLoad));return _62;},getIconMode:function(){return (this.iconMode||this.tree.root.iconMode);},hasChildren:function(_63){return (this.children.length>0||(_63&&this.isDynamic()&&!this.dynamicLoadComplete));},toggle:function(){if(!this.tree.locked&&(this.hasChildren(true)||this.isDynamic())){if(this.expanded){this.collapse();}else{this.expand();}}},getHtml:function(){var sb=[];sb[sb.length]="<div class=\"ygtvitem\" id=\""+this.getElId()+"\">";sb[sb.length]=this.getNodeHtml();sb[sb.length]=this.getChildrenHtml();sb[sb.length]="</div>";return sb.join("");},getChildrenHtml:function(){var sb=[];sb[sb.length]="<div class=\"ygtvchildren\"";sb[sb.length]=" id=\""+this.getChildrenElId()+"\"";if(!this.expanded){sb[sb.length]=" style=\"display:none;\"";}sb[sb.length]=">";if((this.hasChildren(true)&&this.expanded)||(this.renderHidden&&!this.isDynamic())){sb[sb.length]=this.renderChildren();}sb[sb.length]="</div>";return sb.join("");},renderChildren:function(){var _64=this;if(this.isDynamic()&&!this.dynamicLoadComplete){this.isLoading=true;this.tree.locked=true;if(this.dataLoader){setTimeout(function(){_64.dataLoader(_64,function(){_64.loadComplete();});},10);}else{if(this.tree.root.dataLoader){setTimeout(function(){_64.tree.root.dataLoader(_64,function(){_64.loadComplete();});},10);}else{return "Error: data loader not found or not specified.";}}return "";}else{return this.completeRender();}},completeRender:function(){var sb=[];for(var i=0;i<this.children.length;++i){this.children[i].childrenRendered=false;sb[sb.length]=this.children[i].getHtml();}this.childrenRendered=true;return sb.join("");},loadComplete:function(){this.getChildrenEl().innerHTML=this.completeRender();this.dynamicLoadComplete=true;this.isLoading=false;this.expand();this.tree.locked=false;},getAncestor:function(_65){if(_65>=this.depth||_65<0){return null;}var p=this.parent;while(p.depth>_65){p=p.parent;}return p;},getDepthStyle:function(_66){return (this.getAncestor(_66).nextSibling)?"ygtvdepthcell":"ygtvblankdepthcell";},getNodeHtml:function(){return "";},refresh:function(){this.getChildrenEl().innerHTML=this.completeRender();if(this.hasIcon){var el=this.getToggleEl();if(el){el.className=this.getStyle();}}},toString:function(){return "Node ("+this.index+")";}};YAHOO.widget.RootNode=function(_67){this.init(null,null,true);this.tree=_67;};YAHOO.widget.RootNode.prototype=new YAHOO.widget.Node();YAHOO.widget.RootNode.prototype.getNodeHtml=function(){return "";};YAHOO.widget.RootNode.prototype.toString=function(){return "RootNode";};YAHOO.widget.RootNode.prototype.loadComplete=function(){this.tree.draw();};YAHOO.widget.TextNode=function(_68,_69,_70){if(_68){this.init(_68,_69,_70);this.setUpLabel(_68);}};YAHOO.widget.TextNode.prototype=new YAHOO.widget.Node();YAHOO.widget.TextNode.prototype.labelStyle="ygtvlabel";YAHOO.widget.TextNode.prototype.labelElId=null;YAHOO.widget.TextNode.prototype.label=null;YAHOO.widget.TextNode.prototype.setUpLabel=function(_71){if(typeof _71=="string"){_71={label:_71};}this.label=_71.label;if(_71.href){this.href=_71.href;}if(_71.target){this.target=_71.target;}if(_71.style){this.labelStyle=_71.style;}this.labelElId="ygtvlabelel"+this.index;};YAHOO.widget.TextNode.prototype.getLabelEl=function(){return document.getElementById(this.labelElId);};YAHOO.widget.TextNode.prototype.getNodeHtml=function(){var sb=[];sb[sb.length]="<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";sb[sb.length]="<tr>";for(i=0;i<this.depth;++i){sb[sb.length]="<td class=\""+this.getDepthStyle(i)+"\">&#160;</td>";}var _72="YAHOO.widget.TreeView.getNode('"+this.tree.id+"',"+this.index+")";sb[sb.length]="<td";sb[sb.length]=" id=\""+this.getToggleElId()+"\"";sb[sb.length]=" class=\""+this.getStyle()+"\"";if(this.hasChildren(true)){sb[sb.length]=" onmouseover=\"this.className=";sb[sb.length]=_72+".getHoverStyle()\"";sb[sb.length]=" onmouseout=\"this.className=";sb[sb.length]=_72+".getStyle()\"";}sb[sb.length]=" onclick=\"javascript:"+this.getToggleLink()+"\">";sb[sb.length]="&#160;";sb[sb.length]="</td>";sb[sb.length]="<td>";sb[sb.length]="<a";sb[sb.length]=" id=\""+this.labelElId+"\"";sb[sb.length]=" class=\""+this.labelStyle+"\"";sb[sb.length]=" href=\""+this.href+"\"";sb[sb.length]=" target=\""+this.target+"\"";sb[sb.length]=" onclick=\"return "+_72+".onLabelClick("+_72+")\"";if(this.hasChildren(true)){sb[sb.length]=" onmouseover=\"document.getElementById('";sb[sb.length]=this.getToggleElId()+"').className=";sb[sb.length]=_72+".getHoverStyle()\"";sb[sb.length]=" onmouseout=\"document.getElementById('";sb[sb.length]=this.getToggleElId()+"').className=";sb[sb.length]=_72+".getStyle()\"";}sb[sb.length]=" >";sb[sb.length]=this.label;sb[sb.length]="</a>";sb[sb.length]="</td>";sb[sb.length]="</tr>";sb[sb.length]="</table>";return sb.join("");};YAHOO.widget.TextNode.prototype.onLabelClick=function(me){};YAHOO.widget.TextNode.prototype.toString=function(){return "TextNode ("+this.index+") "+this.label;};YAHOO.widget.MenuNode=function(_74,_75,_76){if(_74){this.init(_74,_75,_76);this.setUpLabel(_74);}this.multiExpand=false;};YAHOO.widget.MenuNode.prototype=new YAHOO.widget.TextNode();YAHOO.widget.MenuNode.prototype.toString=function(){return "MenuNode ("+this.index+") "+this.label;};YAHOO.widget.HTMLNode=function(_77,_78,_79,_80){if(_77){this.init(_77,_78,_79);this.initContent(_77,_80);}};YAHOO.widget.HTMLNode.prototype=new YAHOO.widget.Node();YAHOO.widget.HTMLNode.prototype.contentStyle="ygtvhtml";YAHOO.widget.HTMLNode.prototype.contentElId=null;YAHOO.widget.HTMLNode.prototype.content=null;YAHOO.widget.HTMLNode.prototype.initContent=function(_81,_82){if(typeof _81=="string"){_81={html:_81};}this.html=_81.html;this.contentElId="ygtvcontentel"+this.index;this.hasIcon=_82;};YAHOO.widget.HTMLNode.prototype.getContentEl=function(){return document.getElementById(this.contentElId);};YAHOO.widget.HTMLNode.prototype.getNodeHtml=function(){var sb=[];sb[sb.length]="<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";sb[sb.length]="<tr>";for(i=0;i<this.depth;++i){sb[sb.length]="<td class=\""+this.getDepthStyle(i)+"\">&#160;</td>";}if(this.hasIcon){sb[sb.length]="<td";sb[sb.length]=" id=\""+this.getToggleElId()+"\"";sb[sb.length]=" class=\""+this.getStyle()+"\"";sb[sb.length]=" onclick=\"javascript:"+this.getToggleLink()+"\"";if(this.hasChildren(true)){sb[sb.length]=" onmouseover=\"this.className=";sb[sb.length]="YAHOO.widget.TreeView.getNode('";sb[sb.length]=this.tree.id+"',"+this.index+").getHoverStyle()\"";sb[sb.length]=" onmouseout=\"this.className=";sb[sb.length]="YAHOO.widget.TreeView.getNode('";sb[sb.length]=this.tree.id+"',"+this.index+").getStyle()\"";}sb[sb.length]=">&#160;</td>";}sb[sb.length]="<td";sb[sb.length]=" id=\""+this.contentElId+"\"";sb[sb.length]=" class=\""+this.contentStyle+"\"";sb[sb.length]=" >";sb[sb.length]=this.html;sb[sb.length]="</td>";sb[sb.length]="</tr>";sb[sb.length]="</table>";return sb.join("");};YAHOO.widget.HTMLNode.prototype.toString=function(){return "HTMLNode ("+this.index+")";};YAHOO.widget.TVAnim=function(){return {FADE_IN:"TVFadeIn",FADE_OUT:"TVFadeOut",getAnim:function(_83,el,_84){if(YAHOO.widget[_83]){return new YAHOO.widget[_83](el,_84);}else{return null;}},isValid:function(_85){return (YAHOO.widget[_85]);}};}();YAHOO.widget.TVFadeIn=function(el,_86){this.el=el;this.callback=_86;};YAHOO.widget.TVFadeIn.prototype={animate:function(){var _87=this;var s=this.el.style;s.opacity=0.1;s.filter="alpha(opacity=10)";s.display="";var dur=0.4;var a=new YAHOO.util.Anim(this.el,{opacity:{from:0.1,to:1,unit:""}},dur);a.onComplete.subscribe(function(){_87.onComplete();});a.animate();},onComplete:function(){this.callback();},toString:function(){return "TVFadeIn";}};YAHOO.widget.TVFadeOut=function(el,_89){this.el=el;this.callback=_89;};YAHOO.widget.TVFadeOut.prototype={animate:function(){var _90=this;var dur=0.4;var a=new YAHOO.util.Anim(this.el,{opacity:{from:1,to:0.1,unit:""}},dur);a.onComplete.subscribe(function(){_90.onComplete();});a.animate();},onComplete:function(){var s=this.el.style;s.display="none";s.filter="alpha(opacity=100)";this.callback();},toString:function(){return "TVFadeOut";}};