var OxOa1cf=["top","hiddenDirectory","hiddenFile","hiddenAlert","hiddenAction","hiddenActionData","This function is disabled in the demo mode.","disabled","[[Disabled]]","[[SpecifyNewFolderName]]","","value","createdir","[[CopyMoveto]]","/","move","copy","[[AreyouSureDelete]]","parentNode","text","isdir","true",".","[[SpecifyNewFileName]]","rename","True","False",":","path","FoldersAndFiles","TR","length","onmouseover","this.bgColor=\x27#eeeeee\x27;","onmouseout","this.bgColor=\x27\x27;","nodeName","INPUT","changedir","url","TargetUrl","htmlcode","onload","getElementsByTagName","table","sortable"," ","className","id","rows","cells","innerHTML","\x3Ca href=\x22#\x22 onclick=\x22ts_resortTable(this);return false;\x22\x3E","\x3Cspan class=\x22sortarrow\x22\x3E\x26nbsp;\x3C/span\x3E\x3C/a\x3E","string","undefined","innerText","childNodes","nodeValue","nodeType","span","cellIndex","TABLE","sortdir","down","\x26uarr;","up","\x26darr;","sortbottom","tBodies","sortarrow","\x26nbsp;","20","19","Form1","Image1","FolderDescription","CreateDir","Copy","Move","Delete","DoRefresh","name_Cell","size_Cell","op_Cell","divpreview","Table3","Table4","sel_target","inp_color","inp_color_preview","inc_class","inp_id","inp_index","inp_access","Table8","inp_title","Button1","Button2","btn_zoom_in","btn_zoom_out","btn_Actualsize","a","editor","color","style","backgroundColor","class","title","target","tabIndex","accessKey","href","href_cetemp",".jpeg",".jpg",".gif",".png","\x3CIMG src=\x27","\x27\x3E",".bmp","\x26nbsp;\x3Cembed src=\x22","\x22 quality=\x22high\x22 width=\x22200\x22 height=\x22200\x22 type=\x22application/x-shockwave-flash\x22 pluginspage=\x22http://www.macromedia.com/go/getflashplayer\x22\x3E\x3C/embed\x3E\x0A",".swf",".avi",".mpg",".mp3","\x26nbsp;\x3Cembed name=\x22MediaPlayer1\x22 src=\x22","\x22 autostart=-1 showcontrols=-1  type=\x22application/x-mplayer2\x22 width=\x22240\x22 height=\x22200\x22 pluginspage=\x22http://www.microsoft.com/Windows/MediaPlayer\x22 \x3E\x3C/embed\x3E\x0A",".mpeg","\x3Cdiv\x3E\x3C/div\x3E","\x3Cdiv\x3E\x26nbsp;\x3C/div\x3E","\x3Cdiv\x3E\x26#160;\x3C/div\x3E","\x3Cp\x3E\x3C/p\x3E","\x3Cp\x3E\x26#160;\x3C/p\x3E","\x3Cp\x3E\x26nbsp;\x3C/p\x3E","name","zoom","onclick","display","none","align","absmiddle","wrapupPrompt","iepromptfield","body","div","IEPromptBox","promptBlackout","border","1px solid #b0bec7","#f0f0f0","position","absolute","width","330px","zIndex","100","\x3Cdiv style=\x22width: 100%; padding-top:3px;background-color: #DCE7EB; font-family: verdana; font-size: 10pt; font-weight: bold; height: 22px; text-align:center; background:url(Load.ashx?type=image\x26file=formbg2.gif) repeat-x left top;\x22\x3E[[InputRequired]]\x3C/div\x3E","\x3Cdiv style=\x22padding: 10px\x22\x3E","\x3CBR\x3E\x3CBR\x3E","\x3Cform action=\x22\x22 onsubmit=\x22return wrapupPrompt()\x22\x3E","\x3Cinput id=\x22iepromptfield\x22 name=\x22iepromptdata\x22 type=text size=46 value=\x22","\x22\x3E","\x3Cbr\x3E\x3Cbr\x3E\x3Ccenter\x3E","\x3Cinput type=\x22submit\x22 value=\x22\x26nbsp;\x26nbsp;\x26nbsp;[[OK]]\x26nbsp;\x26nbsp;\x26nbsp;\x22\x3E","\x26nbsp;\x26nbsp;\x26nbsp;\x26nbsp;\x26nbsp;\x26nbsp;","\x3Cinput type=\x22button\x22 onclick=\x22wrapupPrompt(true)\x22 value=\x22\x26nbsp;[[Cancel]]\x26nbsp;\x22\x3E","\x3C/form\x3E\x3C/div\x3E","100px","left","offsetWidth","px","block","CuteEditor_ColorPicker_ButtonOver(this)"];function Window_GetDialogTop(Ox1a1){return Ox1a1[OxOa1cf[0]];} ;var hiddenDirectory=Window_GetElement(window,OxOa1cf[1],true);var hiddenFile=Window_GetElement(window,OxOa1cf[2],true);var hiddenAlert=Window_GetElement(window,OxOa1cf[3],true);var hiddenAction=Window_GetElement(window,OxOa1cf[4],true);var hiddenActionData=Window_GetElement(window,OxOa1cf[5],true);function CreateDir_click(){if(isDemoMode){alert(OxOa1cf[6]);return false;} ;if(Event_GetSrcElement()[OxOa1cf[7]]){alert(OxOa1cf[8]);return false;} ;if(Browser_IsIE7()){IEprompt(Ox218,OxOa1cf[9],OxOa1cf[10]);function Ox218(Ox379){if(Ox379){hiddenActionData[OxOa1cf[11]]=Ox379;hiddenAction[OxOa1cf[11]]=OxOa1cf[12];window.PostBackAction();return true;} else {return false;} ;} ;return Event_CancelEvent();} else {var Ox379=prompt(OxOa1cf[9],OxOa1cf[10]);if(Ox379){hiddenActionData[OxOa1cf[11]]=Ox379;return true;} else {return false;} ;return false;} ;} ;function Move_click(){if(isDemoMode){alert(OxOa1cf[6]);return false;} ;if(Event_GetSrcElement()[OxOa1cf[7]]){alert(OxOa1cf[8]);return false;} ;if(Browser_IsIE7()){IEprompt(Ox218,OxOa1cf[13],OxOa1cf[14]);function Ox218(Ox379){if(Ox379){hiddenActionData[OxOa1cf[11]]=Ox379;hiddenAction[OxOa1cf[11]]=OxOa1cf[15];window.PostBackAction();return true;} else {return false;} ;} ;return Event_CancelEvent();} else {var Ox379=prompt(OxOa1cf[13],OxOa1cf[14]);if(Ox379){hiddenActionData[OxOa1cf[11]]=Ox379;return true;} else {return false;} ;return false;} ;} ;function Copy_click(){if(isDemoMode){alert(OxOa1cf[6]);return false;} ;if(Event_GetSrcElement()[OxOa1cf[7]]){alert(OxOa1cf[8]);return false;} ;if(Browser_IsIE7()){IEprompt(Ox218,OxOa1cf[13],OxOa1cf[14]);function Ox218(Ox379){if(Ox379){hiddenActionData[OxOa1cf[11]]=Ox379;hiddenAction[OxOa1cf[11]]=OxOa1cf[16];window.PostBackAction();return true;} else {return false;} ;} ;return Event_CancelEvent();} else {var Ox379=prompt(OxOa1cf[13],OxOa1cf[14]);if(Ox379){hiddenActionData[OxOa1cf[11]]=Ox379;return true;} else {return false;} ;return false;} ;} ;function Delete_click(){if(isDemoMode){alert(OxOa1cf[6]);return false;} ;if(Event_GetSrcElement()[OxOa1cf[7]]){alert(OxOa1cf[8]);return false;} ;return confirm(OxOa1cf[17]);} ;function EditImg_click(img){if(isDemoMode){alert(OxOa1cf[6]);return false;} ;if(img[OxOa1cf[7]]){alert(OxOa1cf[8]);return false;} ;var Ox37e=img[OxOa1cf[18]][OxOa1cf[18]];var Ox37f=Ox37e[OxOa1cf[19]];var name;var Ox380;if(Browser_IsOpera()){Ox380=Ox37e.getAttribute(OxOa1cf[20])==OxOa1cf[21];} else {Ox380=Ox37e[OxOa1cf[20]];} ;if(Browser_IsIE7()){var Oxc3;if(Ox380){IEprompt(Ox218,OxOa1cf[9],Ox37f);} else {var i=Ox37f.lastIndexOf(OxOa1cf[22]);Oxc3=Ox37f.substr(i);var Ox12=Ox37f.substr(0,Ox37f.lastIndexOf(OxOa1cf[22]));IEprompt(Ox218,OxOa1cf[23],Ox12);} ;function Ox218(Ox379){if(Ox379&&Ox379!=Ox37e[OxOa1cf[19]]){if(!Ox380){Ox379=Ox379+Oxc3;} ;hiddenAction[OxOa1cf[11]]=OxOa1cf[24];hiddenActionData[OxOa1cf[11]]=(Ox380?OxOa1cf[25]:OxOa1cf[26])+OxOa1cf[27]+Ox37e[OxOa1cf[28]]+OxOa1cf[27]+Ox379;window.PostBackAction();} ;} ;} else {if(Ox380){name=prompt(OxOa1cf[9],Ox37f);} else {var i=Ox37f.lastIndexOf(OxOa1cf[22]);var Oxc3=Ox37f.substr(i);var Ox12=Ox37f.substr(0,Ox37f.lastIndexOf(OxOa1cf[22]));name=prompt(OxOa1cf[23],Ox12);if(name){name=name+Oxc3;} ;} ;if(name&&name!=Ox37e[OxOa1cf[19]]){hiddenAction[OxOa1cf[11]]=OxOa1cf[24];hiddenActionData[OxOa1cf[11]]=(Ox380?OxOa1cf[25]:OxOa1cf[26])+OxOa1cf[27]+Ox37e[OxOa1cf[28]]+OxOa1cf[27]+name;window.PostBackAction();} ;} ;return Event_CancelEvent();} ;setMouseOver();function setMouseOver(){var FoldersAndFiles=Window_GetElement(window,OxOa1cf[29],true);var Ox383=FoldersAndFiles.getElementsByTagName(OxOa1cf[30]);for(var i=0;i<Ox383[OxOa1cf[31]];i++){var Ox37e=Ox383[i];Ox37e[OxOa1cf[32]]= new Function(OxOa1cf[10],OxOa1cf[33]);Ox37e[OxOa1cf[34]]= new Function(OxOa1cf[10],OxOa1cf[35]);} ;} ;function row_click(Ox37e){var Ox380;if(Browser_IsOpera()){Ox380=Ox37e.getAttribute(OxOa1cf[20])==OxOa1cf[21];} else {Ox380=Ox37e[OxOa1cf[20]];} ;if(Ox380){if(Event_GetSrcElement()[OxOa1cf[36]]==OxOa1cf[37]){return ;} ;hiddenAction[OxOa1cf[11]]=OxOa1cf[38];hiddenActionData[OxOa1cf[11]]=Ox37e.getAttribute(OxOa1cf[28]);window.PostBackAction();} else {var Ox102=Ox37e.getAttribute(OxOa1cf[28]);hiddenFile[OxOa1cf[11]]=Ox102;var Ox280=Ox37e.getAttribute(OxOa1cf[39]);Window_GetElement(window,OxOa1cf[40],true)[OxOa1cf[11]]=Ox280;var htmlcode=Ox37e.getAttribute(OxOa1cf[41]);if(htmlcode!=OxOa1cf[10]&&htmlcode!=null){do_preview(htmlcode);} else {try{Actualsize();} catch(x){do_preview();} ;} ;} ;} ;function do_preview(){} ;function reset_hiddens(){if(hiddenAlert[OxOa1cf[11]]){alert(hiddenAlert.value);} ;if(TargetUrl[OxOa1cf[11]]!=OxOa1cf[10]&&TargetUrl[OxOa1cf[11]]!=null){do_preview();} ;hiddenAlert[OxOa1cf[11]]=OxOa1cf[10];hiddenAction[OxOa1cf[11]]=OxOa1cf[10];hiddenActionData[OxOa1cf[11]]=OxOa1cf[10];} ;Event_Attach(window,OxOa1cf[42],reset_hiddens);function RequireFileBrowseScript(){} ;Event_Attach(window,OxOa1cf[42],sortables_init);var SORT_COLUMN_INDEX;function sortables_init(){if(!document[OxOa1cf[43]]){return ;} ;var Ox388=document.getElementsByTagName(OxOa1cf[44]);for(var Ox389=0;Ox389<Ox388[OxOa1cf[31]];Ox389++){var Ox38a=Ox388[Ox389];if(((OxOa1cf[46]+Ox38a[OxOa1cf[47]]+OxOa1cf[46]).indexOf(OxOa1cf[45])!=-1)&&(Ox38a[OxOa1cf[48]])){ts_makeSortable(Ox38a);} ;} ;} ;function ts_makeSortable(Ox38c){if(Ox38c[OxOa1cf[49]]&&Ox38c[OxOa1cf[49]][OxOa1cf[31]]>0){var Ox38d=Ox38c[OxOa1cf[49]][0];} ;if(!Ox38d){return ;} ;for(var i=2;i<4;i++){var Ox38e=Ox38d[OxOa1cf[50]][i];var Ox27b=ts_getInnerText(Ox38e);Ox38e[OxOa1cf[51]]=OxOa1cf[52]+Ox27b+OxOa1cf[53];} ;} ;function ts_getInnerText(Ox27){if( typeof Ox27==OxOa1cf[54]){return Ox27;} ;if( typeof Ox27==OxOa1cf[55]){return Ox27;} ;if(Ox27[OxOa1cf[56]]){return Ox27[OxOa1cf[56]];} ;var Ox24=OxOa1cf[10];var Ox33a=Ox27[OxOa1cf[57]];var Ox11=Ox33a[OxOa1cf[31]];for(var i=0;i<Ox11;i++){switch(Ox33a[i][OxOa1cf[59]]){case 1:Ox24+=ts_getInnerText(Ox33a[i]);break ;;case 3:Ox24+=Ox33a[i][OxOa1cf[58]];break ;;} ;} ;return Ox24;} ;function ts_resortTable(Ox391){var Ox29e;for(var Ox392=0;Ox392<Ox391[OxOa1cf[57]][OxOa1cf[31]];Ox392++){if(Ox391[OxOa1cf[57]][Ox392][OxOa1cf[36]]&&Ox391[OxOa1cf[57]][Ox392][OxOa1cf[36]].toLowerCase()==OxOa1cf[60]){Ox29e=Ox391[OxOa1cf[57]][Ox392];} ;} ;var Ox393=ts_getInnerText(Ox29e);var Ox1dd=Ox391[OxOa1cf[18]];var Ox394=Ox1dd[OxOa1cf[61]];var Ox38c=getParent(Ox1dd,OxOa1cf[62]);if(Ox38c[OxOa1cf[49]][OxOa1cf[31]]<=1){return ;} ;var Ox395=ts_getInnerText(Ox38c[OxOa1cf[49]][1][OxOa1cf[50]][Ox394]);var Ox396=ts_sort_caseinsensitive;if(Ox395.match(/^\d\d[\/-]\d\d[\/-]\d\d\d\d$/)){Ox396=ts_sort_date;} ;if(Ox395.match(/^\d\d[\/-]\d\d[\/-]\d\d$/)){Ox396=ts_sort_date;} ;if(Ox395.match(/^[?]/)){Ox396=ts_sort_currency;} ;if(Ox395.match(/^[\d\.]+$/)){Ox396=ts_sort_numeric;} ;SORT_COLUMN_INDEX=Ox394;var Ox38d= new Array();var Ox397= new Array();for(var i=0;i<Ox38c[OxOa1cf[49]][0][OxOa1cf[31]];i++){Ox38d[i]=Ox38c[OxOa1cf[49]][0][i];} ;for(var j=1;j<Ox38c[OxOa1cf[49]][OxOa1cf[31]];j++){Ox397[j-1]=Ox38c[OxOa1cf[49]][j];} ;Ox397.sort(Ox396);if(Ox29e.getAttribute(OxOa1cf[63])==OxOa1cf[64]){var Ox398=OxOa1cf[65];Ox397.reverse();Ox29e.setAttribute(OxOa1cf[63],OxOa1cf[66]);} else {Ox398=OxOa1cf[67];Ox29e.setAttribute(OxOa1cf[63],OxOa1cf[64]);} ;for(i=0;i<Ox397[OxOa1cf[31]];i++){if(!Ox397[i][OxOa1cf[47]]||(Ox397[i][OxOa1cf[47]]&&(Ox397[i][OxOa1cf[47]].indexOf(OxOa1cf[68])==-1))){Ox38c[OxOa1cf[69]][0].appendChild(Ox397[i]);} ;} ;for(i=0;i<Ox397[OxOa1cf[31]];i++){if(Ox397[i][OxOa1cf[47]]&&(Ox397[i][OxOa1cf[47]].indexOf(OxOa1cf[68])!=-1)){Ox38c[OxOa1cf[69]][0].appendChild(Ox397[i]);} ;} ;var Ox399=document.getElementsByTagName(OxOa1cf[60]);for(var Ox392=0;Ox392<Ox399[OxOa1cf[31]];Ox392++){if(Ox399[Ox392][OxOa1cf[47]]==OxOa1cf[70]){if(getParent(Ox399[Ox392],OxOa1cf[44])==getParent(Ox391,OxOa1cf[44])){Ox399[Ox392][OxOa1cf[51]]=OxOa1cf[71];} ;} ;} ;Ox29e[OxOa1cf[51]]=Ox398;} ;function getParent(Ox27,Ox39b){if(Ox27==null){return null;} else {if(Ox27[OxOa1cf[59]]==1&&Ox27[OxOa1cf[36]].toLowerCase()==Ox39b.toLowerCase()){return Ox27;} else {return getParent(Ox27.parentNode,Ox39b);} ;} ;} ;function ts_sort_date(Oxe7,b){var Ox39d=ts_getInnerText(Oxe7[OxOa1cf[50]][SORT_COLUMN_INDEX]);var Ox39e=ts_getInnerText(b[OxOa1cf[50]][SORT_COLUMN_INDEX]);if(Ox39d[OxOa1cf[31]]==10){var Ox39f=Ox39d.substr(6,4)+Ox39d.substr(3,2)+Ox39d.substr(0,2);} else {var Ox3a0=Ox39d.substr(6,2);if(parseInt(Ox3a0)<50){Ox3a0=OxOa1cf[72]+Ox3a0;} else {Ox3a0=OxOa1cf[73]+Ox3a0;} ;var Ox39f=Ox3a0+Ox39d.substr(3,2)+Ox39d.substr(0,2);} ;if(Ox39e[OxOa1cf[31]]==10){var Ox3a1=Ox39e.substr(6,4)+Ox39e.substr(3,2)+Ox39e.substr(0,2);} else {Ox3a0=Ox39e.substr(6,2);if(parseInt(Ox3a0)<50){Ox3a0=OxOa1cf[72]+Ox3a0;} else {Ox3a0=OxOa1cf[73]+Ox3a0;} ;var Ox3a1=Ox3a0+Ox39e.substr(3,2)+Ox39e.substr(0,2);} ;if(Ox39f==Ox3a1){return 0;} ;if(Ox39f<Ox3a1){return -1;} ;return 1;} ;function ts_sort_currency(Oxe7,b){var Ox39d=ts_getInnerText(Oxe7[OxOa1cf[50]][SORT_COLUMN_INDEX]).replace(/[^0-9.]/g,OxOa1cf[10]);var Ox39e=ts_getInnerText(b[OxOa1cf[50]][SORT_COLUMN_INDEX]).replace(/[^0-9.]/g,OxOa1cf[10]);return parseFloat(Ox39d)-parseFloat(Ox39e);} ;function ts_sort_numeric(Oxe7,b){var Ox39d=parseFloat(ts_getInnerText(Oxe7[OxOa1cf[50]][SORT_COLUMN_INDEX]));if(isNaN(Ox39d)){Ox39d=0;} ;var Ox39e=parseFloat(ts_getInnerText(b[OxOa1cf[50]][SORT_COLUMN_INDEX]));if(isNaN(Ox39e)){Ox39e=0;} ;return Ox39d-Ox39e;} ;function ts_sort_caseinsensitive(Oxe7,b){var Ox39d=ts_getInnerText(Oxe7[OxOa1cf[50]][SORT_COLUMN_INDEX]).toLowerCase();var Ox39e=ts_getInnerText(b[OxOa1cf[50]][SORT_COLUMN_INDEX]).toLowerCase();if(Ox39d==Ox39e){return 0;} ;if(Ox39d<Ox39e){return -1;} ;return 1;} ;function ts_sort_default(Oxe7,b){var Ox39d=ts_getInnerText(Oxe7[OxOa1cf[50]][SORT_COLUMN_INDEX]);var Ox39e=ts_getInnerText(b[OxOa1cf[50]][SORT_COLUMN_INDEX]);if(Ox39d==Ox39e){return 0;} ;if(Ox39d<Ox39e){return -1;} ;return 1;} [sortables_init];RequireFileBrowseScript();var Form1=Window_GetElement(window,OxOa1cf[74],true);var hiddenDirectory=Window_GetElement(window,OxOa1cf[1],true);var hiddenFile=Window_GetElement(window,OxOa1cf[2],true);var hiddenAlert=Window_GetElement(window,OxOa1cf[3],true);var hiddenAction=Window_GetElement(window,OxOa1cf[4],true);var hiddenActionData=Window_GetElement(window,OxOa1cf[5],true);var Image1=Window_GetElement(window,OxOa1cf[75],true);var FolderDescription=Window_GetElement(window,OxOa1cf[76],true);var CreateDir=Window_GetElement(window,OxOa1cf[77],true);var Copy=Window_GetElement(window,OxOa1cf[78],true);var Move=Window_GetElement(window,OxOa1cf[79],true);var FoldersAndFiles=Window_GetElement(window,OxOa1cf[29],true);var Delete=Window_GetElement(window,OxOa1cf[80],true);var DoRefresh=Window_GetElement(window,OxOa1cf[81],true);var name_Cell=Window_GetElement(window,OxOa1cf[82],true);var size_Cell=Window_GetElement(window,OxOa1cf[83],true);var op_Cell=Window_GetElement(window,OxOa1cf[84],true);var divpreview=Window_GetElement(window,OxOa1cf[85],true);var Table3=Window_GetElement(window,OxOa1cf[86],true);var Table4=Window_GetElement(window,OxOa1cf[87],true);var sel_target=Window_GetElement(window,OxOa1cf[88],true);var inp_color=Window_GetElement(window,OxOa1cf[89],true);var inp_color_preview=Window_GetElement(window,OxOa1cf[90],true);var inc_class=Window_GetElement(window,OxOa1cf[91],true);var inp_id=Window_GetElement(window,OxOa1cf[92],true);var inp_index=Window_GetElement(window,OxOa1cf[93],true);var inp_access=Window_GetElement(window,OxOa1cf[94],true);var Table8=Window_GetElement(window,OxOa1cf[95],true);var TargetUrl=Window_GetElement(window,OxOa1cf[40],true);var inp_title=Window_GetElement(window,OxOa1cf[96],true);var Button1=Window_GetElement(window,OxOa1cf[97],true);var Button2=Window_GetElement(window,OxOa1cf[98],true);var btn_zoom_in=Window_GetElement(window,OxOa1cf[99],true);var btn_zoom_out=Window_GetElement(window,OxOa1cf[100],true);var btn_Actualsize=Window_GetElement(window,OxOa1cf[101],true);var obj=Window_GetDialogArguments(window);var element=null;if(obj){element=obj[OxOa1cf[102]];} ;var editor=obj[OxOa1cf[103]];var htmlcode=OxOa1cf[10];if(element[OxOa1cf[105]][OxOa1cf[104]]){inp_color[OxOa1cf[11]]=revertColor(element[OxOa1cf[105]].color);inp_color[OxOa1cf[105]][OxOa1cf[106]]=inp_color[OxOa1cf[11]];inp_color_preview[OxOa1cf[105]][OxOa1cf[106]]=inp_color[OxOa1cf[11]];} ;if(element[OxOa1cf[47]]==OxOa1cf[10]){element.removeAttribute(OxOa1cf[47]);} ;if(element[OxOa1cf[47]]==OxOa1cf[10]){element.removeAttribute(OxOa1cf[107]);} ;if(element[OxOa1cf[108]]){inp_title[OxOa1cf[11]]=element[OxOa1cf[108]];} ;if(element[OxOa1cf[109]]){sel_target[OxOa1cf[11]]=element[OxOa1cf[109]];} ;if(element[OxOa1cf[110]]){inp_index[OxOa1cf[11]]=element[OxOa1cf[110]];} ;if(element[OxOa1cf[111]]){inp_access[OxOa1cf[11]]=element[OxOa1cf[111]];} ;var src=OxOa1cf[10];if(element.getAttribute(OxOa1cf[112])){src=element.getAttribute(OxOa1cf[112]);} ;if(element.getAttribute(OxOa1cf[113])){src=element.getAttribute(OxOa1cf[113]);} ;if(TargetUrl[OxOa1cf[11]]){Actualsize();} else {if(element&&src){TargetUrl[OxOa1cf[11]]=src;} ;} ;inp_id[OxOa1cf[11]]=element[OxOa1cf[48]];var divpreview=Window_GetElement(window,OxOa1cf[85],true);do_preview();function do_preview(Ox27a){if(Ox27a!=OxOa1cf[10]&&Ox27a!=null){htmlcode=Ox27a;divpreview[OxOa1cf[51]]=Ox27a;return ;} ;divpreview[OxOa1cf[51]]=OxOa1cf[10];var Ox280=TargetUrl[OxOa1cf[11]];if(Ox280==OxOa1cf[10]){return ;} ;var Oxc3=Ox280.substring(Ox280.lastIndexOf(OxOa1cf[22])).toLowerCase();switch(Oxc3){case OxOa1cf[114]:;case OxOa1cf[115]:;case OxOa1cf[116]:;case OxOa1cf[117]:;case OxOa1cf[120]:divpreview[OxOa1cf[51]]=OxOa1cf[118]+Ox280+OxOa1cf[119];break ;;case OxOa1cf[123]:var Ox3c2=OxOa1cf[121]+Ox280+OxOa1cf[122];divpreview[OxOa1cf[51]]=Ox3c2+OxOa1cf[71];break ;;case OxOa1cf[124]:;case OxOa1cf[125]:;case OxOa1cf[126]:;case OxOa1cf[129]:var Ox3c3=OxOa1cf[127]+Ox280+OxOa1cf[128];divpreview[OxOa1cf[51]]=Ox3c3+OxOa1cf[71];break ;;} ;} ;function do_insert(){element[OxOa1cf[47]]=inc_class[OxOa1cf[11]];element[OxOa1cf[109]]=sel_target[OxOa1cf[11]];element[OxOa1cf[108]]=inp_title[OxOa1cf[11]];if(TargetUrl[OxOa1cf[11]]){element[OxOa1cf[112]]=TargetUrl[OxOa1cf[11]];element.setAttribute(OxOa1cf[113],TargetUrl.value);} ;element[OxOa1cf[110]]=inp_index[OxOa1cf[11]];element[OxOa1cf[111]]=inp_access[OxOa1cf[11]];element[OxOa1cf[48]]=inp_id[OxOa1cf[11]];if(element[OxOa1cf[108]]==OxOa1cf[10]){element.removeAttribute(OxOa1cf[108]);} ;if(element[OxOa1cf[109]]==OxOa1cf[10]){element.removeAttribute(OxOa1cf[109]);} ;if(element[OxOa1cf[47]]==OxOa1cf[10]){element.removeAttribute(OxOa1cf[47]);} ;if(element[OxOa1cf[47]]==OxOa1cf[10]){element.removeAttribute(OxOa1cf[107]);} ;if(element[OxOa1cf[110]]==OxOa1cf[10]){element.removeAttribute(OxOa1cf[110]);} ;if(element[OxOa1cf[111]]==OxOa1cf[10]){element.removeAttribute(OxOa1cf[111]);} ;if(element[OxOa1cf[48]]==OxOa1cf[10]){element.removeAttribute(OxOa1cf[48]);} ;try{element[OxOa1cf[105]][OxOa1cf[104]]=inp_color[OxOa1cf[11]];} catch(er){element[OxOa1cf[105]][OxOa1cf[104]]=OxOa1cf[10];} ;var Ox27a=element[OxOa1cf[51]];switch(Ox27a.toLowerCase()){case OxOa1cf[130]:;case OxOa1cf[131]:;case OxOa1cf[132]:;case OxOa1cf[133]:;case OxOa1cf[134]:;case OxOa1cf[135]:element[OxOa1cf[51]]=OxOa1cf[10];break ;;default:break ;;} ;if(element[OxOa1cf[51]]==OxOa1cf[10]){element[OxOa1cf[51]]=element[OxOa1cf[108]]||TargetUrl[OxOa1cf[11]]||element[OxOa1cf[136]]||OxOa1cf[10];} ;Window_SetDialogReturnValue(window,element);Window_CloseDialog(window);} ;function do_Close(){Window_SetDialogReturnValue(window,null);Window_CloseDialog(window);} ;function Zoom_In(){if(divpreview[OxOa1cf[105]][OxOa1cf[137]]!=0){divpreview[OxOa1cf[105]][OxOa1cf[137]]*=1.2;} else {divpreview[OxOa1cf[105]][OxOa1cf[137]]=1.2;} ;} ;function Zoom_Out(){if(divpreview[OxOa1cf[105]][OxOa1cf[137]]!=0){divpreview[OxOa1cf[105]][OxOa1cf[137]]*=0.8;} else {divpreview[OxOa1cf[105]][OxOa1cf[137]]=0.8;} ;} ;function Actualsize(){divpreview[OxOa1cf[105]][OxOa1cf[137]]=1;do_preview();} ;inp_color[OxOa1cf[138]]=inp_color_preview[OxOa1cf[138]]=function inp_color_onclick(){SelectColor(inp_color,inp_color_preview);} ;if(!Browser_IsWinIE()){btn_zoom_in[OxOa1cf[105]][OxOa1cf[139]]=btn_zoom_out[OxOa1cf[105]][OxOa1cf[139]]=btn_Actualsize[OxOa1cf[105]][OxOa1cf[139]]=OxOa1cf[140];inp_color_preview.setAttribute(OxOa1cf[141],OxOa1cf[142]);} ;if(Browser_IsIE7()){var _dialogPromptID=null;function IEprompt(Ox218,Ox219,Ox21a){that=this;this[OxOa1cf[143]]=function (Ox21b){val=document.getElementById(OxOa1cf[144])[OxOa1cf[11]];_dialogPromptID[OxOa1cf[105]][OxOa1cf[139]]=OxOa1cf[140];document.getElementById(OxOa1cf[144])[OxOa1cf[11]]=OxOa1cf[10];if(Ox21b){val=OxOa1cf[10];} ;Ox218(val);return false;} ;if(Ox21a==undefined){Ox21a=OxOa1cf[10];} ;if(_dialogPromptID==null){var Ox21c=document.getElementsByTagName(OxOa1cf[145])[0];tnode=document.createElement(OxOa1cf[146]);tnode[OxOa1cf[48]]=OxOa1cf[147];Ox21c.appendChild(tnode);_dialogPromptID=document.getElementById(OxOa1cf[147]);tnode=document.createElement(OxOa1cf[146]);tnode[OxOa1cf[48]]=OxOa1cf[148];Ox21c.appendChild(tnode);_dialogPromptID[OxOa1cf[105]][OxOa1cf[149]]=OxOa1cf[150];_dialogPromptID[OxOa1cf[105]][OxOa1cf[106]]=OxOa1cf[151];_dialogPromptID[OxOa1cf[105]][OxOa1cf[152]]=OxOa1cf[153];_dialogPromptID[OxOa1cf[105]][OxOa1cf[154]]=OxOa1cf[155];_dialogPromptID[OxOa1cf[105]][OxOa1cf[156]]=OxOa1cf[157];} ;var Ox21d=OxOa1cf[158];Ox21d+=OxOa1cf[159]+Ox219+OxOa1cf[160];Ox21d+=OxOa1cf[161];Ox21d+=OxOa1cf[162]+Ox21a+OxOa1cf[163];Ox21d+=OxOa1cf[164];Ox21d+=OxOa1cf[165];Ox21d+=OxOa1cf[166];Ox21d+=OxOa1cf[167];Ox21d+=OxOa1cf[168];_dialogPromptID[OxOa1cf[51]]=Ox21d;_dialogPromptID[OxOa1cf[105]][OxOa1cf[0]]=OxOa1cf[169];_dialogPromptID[OxOa1cf[105]][OxOa1cf[170]]=parseInt((document[OxOa1cf[145]][OxOa1cf[171]]-315)/2)+OxOa1cf[172];_dialogPromptID[OxOa1cf[105]][OxOa1cf[139]]=OxOa1cf[173];var Ox21e=document.getElementById(OxOa1cf[144]);try{var Ox21f=Ox21e.createTextRange();Ox21f.collapse(false);Ox21f.select();} catch(x){Ox21e.focus();} ;} ;} ;if(CreateDir){CreateDir[OxOa1cf[32]]= new Function(OxOa1cf[174]);} ;if(Copy){Copy[OxOa1cf[32]]= new Function(OxOa1cf[174]);} ;if(Move){Move[OxOa1cf[32]]= new Function(OxOa1cf[174]);} ;if(Delete){Delete[OxOa1cf[32]]= new Function(OxOa1cf[174]);} ;if(DoRefresh){DoRefresh[OxOa1cf[32]]= new Function(OxOa1cf[174]);} ;if(btn_zoom_in){btn_zoom_in[OxOa1cf[32]]= new Function(OxOa1cf[174]);} ;if(btn_zoom_out){btn_zoom_out[OxOa1cf[32]]= new Function(OxOa1cf[174]);} ;if(btn_Actualsize){btn_Actualsize[OxOa1cf[32]]= new Function(OxOa1cf[174]);} ;