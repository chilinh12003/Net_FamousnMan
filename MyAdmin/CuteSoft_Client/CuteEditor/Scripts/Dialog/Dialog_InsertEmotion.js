var OxO91ea=[""," ","=\x22","\x22","src","^[a-z]*:[/][/][^/]*","Edit","\x3CIMG border=\x220\x22 align=\x22absmiddle\x22 src=\x22","\x22 src_cetemp=\x22","\x22\x3E","ImageTable","IMG","length","className","dialogButton","onmouseover","CuteEditor_ColorPicker_ButtonOver(this)","onclick","insert(this)"];var editor=Window_GetDialogArguments(window);function attr(name,Ox4e){if(!Ox4e||Ox4e==OxO91ea[0]){return OxO91ea[0];} ;return OxO91ea[1]+name+OxO91ea[2]+Ox4e+OxO91ea[3];} ;function insert(img){if(img){var src=img[OxO91ea[4]];src=src.replace( new RegExp(OxO91ea[5],OxO91ea[0]),OxO91ea[0]);var Ox2b=OxO91ea[0];if(editor.GetActiveTab()==OxO91ea[6]){Ox2b=OxO91ea[7]+src+OxO91ea[8]+src+OxO91ea[9];} else {Ox2b=OxO91ea[7]+src+OxO91ea[9];} ;editor.PasteHTML(Ox2b);Window_CloseDialog(window);} ;} ;function do_Close(){Window_CloseDialog(window);} ;var ImageTable=Window_GetElement(window,OxO91ea[10],true);var images=ImageTable.getElementsByTagName(OxO91ea[11]);var len=images[OxO91ea[12]];for(var i=0;i<len;i++){var img=images[i];img[OxO91ea[13]]=OxO91ea[14];img[OxO91ea[15]]= new Function(OxO91ea[16]);img[OxO91ea[17]]= new Function(OxO91ea[18]);} ;