var OxO17c7=["","sel_align","sel_valign","sel_justify","sel_letter","tb_letter","sel_letter_unit","sel_line","tb_line","sel_line_unit","tb_indent","sel_indent_unit","sel_direction","sel_writingmode","outer","div_demo","disabled","selectedIndex","cssText","style","value","textAlign","verticalAlign","textJustify","letterSpacing","length","options","lineHeight","textIndent","direction","writingMode"];function ParseFloatToString(Ox24){var Ox8=parseFloat(Ox24);if(isNaN(Ox8)){return OxO17c7[0];} ;return Ox8+OxO17c7[0];} ;var sel_align=Window_GetElement(window,OxO17c7[1],true);var sel_valign=Window_GetElement(window,OxO17c7[2],true);var sel_justify=Window_GetElement(window,OxO17c7[3],true);var sel_letter=Window_GetElement(window,OxO17c7[4],true);var tb_letter=Window_GetElement(window,OxO17c7[5],true);var sel_letter_unit=Window_GetElement(window,OxO17c7[6],true);var sel_line=Window_GetElement(window,OxO17c7[7],true);var tb_line=Window_GetElement(window,OxO17c7[8],true);var sel_line_unit=Window_GetElement(window,OxO17c7[9],true);var tb_indent=Window_GetElement(window,OxO17c7[10],true);var sel_indent_unit=Window_GetElement(window,OxO17c7[11],true);var sel_direction=Window_GetElement(window,OxO17c7[12],true);var sel_writingmode=Window_GetElement(window,OxO17c7[13],true);var outer=Window_GetElement(window,OxO17c7[14],true);var div_demo=Window_GetElement(window,OxO17c7[15],true);UpdateState=function UpdateState_Text(){tb_letter[OxO17c7[16]]=sel_letter_unit[OxO17c7[16]]=(sel_letter[OxO17c7[17]]>0);tb_line[OxO17c7[16]]=sel_line_unit[OxO17c7[16]]=(sel_line[OxO17c7[17]]>0);div_demo[OxO17c7[19]][OxO17c7[18]]=element[OxO17c7[19]][OxO17c7[18]];} ;SyncToView=function SyncToView_Text(){sel_align[OxO17c7[20]]=element[OxO17c7[19]][OxO17c7[21]];sel_valign[OxO17c7[20]]=element[OxO17c7[19]][OxO17c7[22]];sel_justify[OxO17c7[20]]=element[OxO17c7[19]][OxO17c7[23]];sel_letter[OxO17c7[20]]=element[OxO17c7[19]][OxO17c7[24]];sel_letter_unit[OxO17c7[17]]=0;if(sel_letter[OxO17c7[17]]==-1){if(ParseFloatToString(element[OxO17c7[19]].letterSpacing)){tb_letter[OxO17c7[20]]=ParseFloatToString(element[OxO17c7[19]].letterSpacing);for(var i=0;i<sel_letter_unit[OxO17c7[26]][OxO17c7[25]];i++){var Ox13b=sel_letter_unit[OxO17c7[26]][i][OxO17c7[20]];if(Ox13b&&element[OxO17c7[19]][OxO17c7[24]].indexOf(Ox13b)!=-1){sel_letter_unit[OxO17c7[17]]=i;break ;} ;} ;} ;} ;sel_line[OxO17c7[20]]=element[OxO17c7[19]][OxO17c7[27]];sel_line_unit[OxO17c7[17]]=0;if(sel_line[OxO17c7[17]]==-1){if(ParseFloatToString(element[OxO17c7[19]].lineHeight)){tb_line[OxO17c7[20]]=ParseFloatToString(element[OxO17c7[19]].lineHeight);for(var i=0;i<sel_line_unit[OxO17c7[26]][OxO17c7[25]];i++){var Ox13b=sel_line_unit[OxO17c7[26]][i][OxO17c7[20]];if(Ox13b&&element[OxO17c7[19]][OxO17c7[27]].indexOf(Ox13b)!=-1){sel_line_unit[OxO17c7[17]]=i;break ;} ;} ;} ;} ;sel_indent_unit[OxO17c7[17]]=0;if(!isNaN(ParseFloatToString(element[OxO17c7[19]].textIndent))){tb_indent[OxO17c7[20]]=ParseFloatToString(element[OxO17c7[19]].textIndent);for(var i=0;i<sel_indent_unit[OxO17c7[26]][OxO17c7[25]];i++){var Ox13b=sel_indent_unit[OxO17c7[26]][i][OxO17c7[20]];if(Ox13b&&element[OxO17c7[19]][OxO17c7[28]].indexOf(Ox13b)!=-1){sel_indent_unit[OxO17c7[17]]=i;break ;} ;} ;} ;sel_direction[OxO17c7[20]]=element[OxO17c7[19]][OxO17c7[29]];sel_writingmode[OxO17c7[20]]=element[OxO17c7[19]][OxO17c7[30]];} ;SyncTo=function SyncTo_Text(element){element[OxO17c7[19]][OxO17c7[21]]=sel_align[OxO17c7[20]];element[OxO17c7[19]][OxO17c7[22]]=sel_valign[OxO17c7[20]];element[OxO17c7[19]][OxO17c7[23]]=sel_justify[OxO17c7[20]];if(sel_letter[OxO17c7[17]]>0){element[OxO17c7[19]][OxO17c7[24]]=sel_letter[OxO17c7[20]];} else {if(ParseFloatToString(tb_letter.value)){element[OxO17c7[19]][OxO17c7[24]]=ParseFloatToString(tb_letter.value)+sel_letter_unit[OxO17c7[20]];} else {element[OxO17c7[19]][OxO17c7[24]]=OxO17c7[0];} ;} ;if(sel_line[OxO17c7[17]]>0){element[OxO17c7[19]][OxO17c7[27]]=sel_line[OxO17c7[20]];} else {if(ParseFloatToString(tb_line.value)){element[OxO17c7[19]][OxO17c7[27]]=ParseFloatToString(tb_line.value)+sel_line_unit[OxO17c7[20]];} else {element[OxO17c7[19]][OxO17c7[27]]=OxO17c7[0];} ;} ;if(ParseFloatToString(tb_indent.value)){element[OxO17c7[19]][OxO17c7[28]]=ParseFloatToString(tb_indent.value)+sel_indent_unit[OxO17c7[20]];} else {element[OxO17c7[19]][OxO17c7[28]]=OxO17c7[0];} ;element[OxO17c7[19]][OxO17c7[29]]=sel_direction[OxO17c7[20]]||OxO17c7[0];element[OxO17c7[19]][OxO17c7[30]]=sel_writingmode[OxO17c7[20]]||OxO17c7[0];} ;