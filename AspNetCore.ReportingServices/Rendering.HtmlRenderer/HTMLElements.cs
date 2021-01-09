using System.Text;

namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	internal static class HTMLElements
	{
      static  UTF8Encoding uTF8Encoding = new UTF8Encoding();
        internal static string m_standardLineBreak = "\n";
        internal static string m_quoteString = "\"";
        internal static string m_spaceString = " ";
        internal static string m_closeQuoteString = "\">";
        internal static string m_blank = "_blank";
        internal static string m_hiddenString = "hidden";
        internal static string m_containString = "contain";
        internal static string m_reportItemCustomAttrStr = "data-reportitem";
        internal static string m_reportItemDataName = "data-name";
        internal static string m_resize100WidthClassName = "resize100Width";
        internal static string m_resize100HeightClassName = "resize100Height";
        internal static string m_mapPrefixString = "Map";
        internal static string m_hrefString = " href=\"";
        internal static string m_flexStart = "flex-start;";
        internal static string m_msFlexStart = "start;";
        internal static string m_flexCenter = "center;";
        internal static string m_flexEnd = "flex-end;";
        internal static string m_msFlexEnd = "end;";
        internal static string m_hashTag = "#";
        internal static string m_role = "role";
        internal static string m_navigationRole = "navigation";
        internal static string m_presentationRole = "presentation";
        internal static string m_ariaLabel = "aria-label";
        internal static string m_ariaLabeledBy = " aria-labelledby=\"";
        internal static string m_ariaExpanded = " aria-expanded=\"";
        internal static string m_ariaSuffix = "_aria";
        internal static string m_ariaLive = "aria-live";
        internal static string m_ariaLivePoliteString = "polite";
        internal static string m_useMapName = "USEMAP";
        internal static string Ie5ContentType = "\n\t<META HTTP-EQUIV=\"X-UA-Compatible\" CONTENT=\"IE=5\">\n";
        internal static string Ie7ContentType = "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=7\" />";
        internal static string EdgeContentType = "\n\t<META HTTP-EQUIV=\"X-UA-Compatible\" CONTENT=\"IE=edge\">\n";
        internal static string Html40DocType = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\" >\n";
        internal static string XhtmlStrictDocType = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">";
        internal static string Utf8Charset = "\n\t<meta charset=\"utf-8\">\n";
        internal static string HtmlStandardsDocType = "<!DOCTYPE html>\n";
        internal static string SegoeUi = "Segoe UI";

        internal static byte[] m_newLine = uTF8Encoding.GetBytes("\r\n");
        internal static byte[] m_openTable = uTF8Encoding.GetBytes("<TABLE CELLSPACING=\"0\" CELLPADDING=\"0\"");
        internal static byte[] m_zeroBorder = uTF8Encoding.GetBytes(" BORDER=\"0\"");
        internal static byte[] m_zeroPoint = uTF8Encoding.GetBytes("0pt");
        internal static byte[] m_smallPoint = uTF8Encoding.GetBytes("1px");
        internal static byte[] m_cols = uTF8Encoding.GetBytes(" COLS=\"");
        internal static byte[] m_colSpan = uTF8Encoding.GetBytes(" COLSPAN=\"");
        internal static byte[] m_rowSpan = uTF8Encoding.GetBytes(" ROWSPAN=\"");
        internal static byte[] m_headers = uTF8Encoding.GetBytes(" HEADERS=\"");
        internal static byte[] m_closeBracket = uTF8Encoding.GetBytes(">");
        internal static byte[] m_closeTable = uTF8Encoding.GetBytes("</TABLE>");
        internal static byte[] m_openDiv = uTF8Encoding.GetBytes("<div");
        internal static byte[] m_closeDiv = uTF8Encoding.GetBytes("</div>");
        internal static byte[] m_openBody = uTF8Encoding.GetBytes("<body");
        internal static byte[] m_closeBody = uTF8Encoding.GetBytes("</body>");
        internal static byte[] m_openHtml = uTF8Encoding.GetBytes("<html>");
        internal static byte[] m_closeHtml = uTF8Encoding.GetBytes("</html>");
        internal static byte[] m_openHead = uTF8Encoding.GetBytes("<head>");
        internal static byte[] m_closeHead = uTF8Encoding.GetBytes("</head>");
        internal static byte[] m_openTitle = uTF8Encoding.GetBytes("<title>");
        internal static byte[] m_closeTitle = uTF8Encoding.GetBytes("</title>");
        internal static byte[] m_firstTD = uTF8Encoding.GetBytes("<TR><TD");
        internal static byte[] m_lastTD = uTF8Encoding.GetBytes("</TD></TR>");
        internal static byte[] m_openTD = uTF8Encoding.GetBytes("<TD");
        internal static byte[] m_closeTD = uTF8Encoding.GetBytes("</TD>");
        internal static byte[] m_closeTR = uTF8Encoding.GetBytes("</TR>");
        internal static byte[] m_openTR = uTF8Encoding.GetBytes("<TR");
        internal static byte[] m_valign = uTF8Encoding.GetBytes(" VALIGN=\"");
        internal static byte[] m_openSpan = uTF8Encoding.GetBytes("<span");
        internal static byte[] m_closeSpan = uTF8Encoding.GetBytes("</span>");
        internal static byte[] m_closeSingleTag = uTF8Encoding.GetBytes("/>");
        internal static byte[] m_id = uTF8Encoding.GetBytes(" ID=\"");
        internal static byte[] m_mm = uTF8Encoding.GetBytes("mm");
        internal static byte[] m_px = uTF8Encoding.GetBytes("px");
        internal static byte[] m_zeroWidth = uTF8Encoding.GetBytes(" WIDTH=\"0\"");
        internal static byte[] m_zeroHeight = uTF8Encoding.GetBytes(" HEIGHT=\"0\"");
        internal static byte[] m_closeTag = uTF8Encoding.GetBytes("\"/>");
        internal static byte[] m_openA = uTF8Encoding.GetBytes("<a");
        internal static byte[] m_target = uTF8Encoding.GetBytes(" TARGET=\"");
        internal static byte[] m_closeA = uTF8Encoding.GetBytes("</a>");
        internal static byte[] m_nohref = uTF8Encoding.GetBytes(" nohref=\"true\"");
        internal static byte[] m_inlineHeight = uTF8Encoding.GetBytes(" HEIGHT=\"");
        internal static byte[] m_inlineWidth = uTF8Encoding.GetBytes(" WIDTH=\"");
        internal static byte[] m_img = uTF8Encoding.GetBytes("<img");
        internal static byte[] m_imgOnError = uTF8Encoding.GetBytes(" onerror=\"this.errored=true;\"");
        internal static byte[] m_src = uTF8Encoding.GetBytes(" src=\"");
        internal static byte[] m_topValue = uTF8Encoding.GetBytes("top");
        internal static byte[] m_leftValue = uTF8Encoding.GetBytes("left");
        internal static byte[] m_rightValue = uTF8Encoding.GetBytes("right");
        internal static byte[] m_centerValue = uTF8Encoding.GetBytes("center");
        internal static byte[] m_classID = uTF8Encoding.GetBytes(" CLASSID=\"CLSID:");
        internal static byte[] m_codeBase = uTF8Encoding.GetBytes(" CODEBASE=\"");
        internal static byte[] m_title = uTF8Encoding.GetBytes(" TITLE=\"");
        internal static byte[] m_alt = uTF8Encoding.GetBytes(" ALT=\"");
        internal static byte[] m_openObject = uTF8Encoding.GetBytes("<OBJECT");
        internal static byte[] m_closeObject = uTF8Encoding.GetBytes("</OBJECT>");
        internal static byte[] m_paramObject = uTF8Encoding.GetBytes("<PARAM NAME=\"");
        internal static byte[] m_valueObject = uTF8Encoding.GetBytes(" VALUE=\"");
        internal static byte[] m_equal = uTF8Encoding.GetBytes("=");
        internal static byte[] m_encodedAmp = uTF8Encoding.GetBytes("&amp;");
        internal static byte[] m_nbsp = uTF8Encoding.GetBytes("&nbsp;");
        internal static byte[] m_questionMark = uTF8Encoding.GetBytes("?");
        internal static byte[] m_none = uTF8Encoding.GetBytes("none");
        internal static byte[] m_displayNone = uTF8Encoding.GetBytes("display: none;");
        internal static byte[] m_styleDisplayInlineBlock = uTF8Encoding.GetBytes("display: inline-block;");
        internal static byte[] m_styleDisplayFlex = uTF8Encoding.GetBytes("display: -ms-flexbox;display: -webkit-flex;display: flex;");
        internal static byte[] m_checkForEnterKey = uTF8Encoding.GetBytes("if(event.keyCode == 13 || event.which == 13){");
        internal static byte[] m_percent = uTF8Encoding.GetBytes("100%");
        internal static byte[] m_ninetyninepercent = uTF8Encoding.GetBytes("99%");
        internal static byte[] m_degree90 = uTF8Encoding.GetBytes("90");
        internal static byte[] m_closeBrace = uTF8Encoding.GetBytes(")");
        internal static byte[] m_rtlDir = uTF8Encoding.GetBytes(" dir=\"RTL\"");
        internal static byte[] m_ltrDir = uTF8Encoding.GetBytes(" dir=\"LTR\"");
        internal static byte[] m_br = uTF8Encoding.GetBytes("<br/>");
        internal static byte[] m_tabIndex = uTF8Encoding.GetBytes(" tabindex=\"");
        internal static byte[] m_useMap = uTF8Encoding.GetBytes(" USEMAP=\"");
        internal static byte[] m_openMap = uTF8Encoding.GetBytes("<MAP ");
        internal static byte[] m_closeMap = uTF8Encoding.GetBytes("</MAP>");
        internal static byte[] m_mapArea = uTF8Encoding.GetBytes("<AREA ");
        internal static byte[] m_mapCoords = uTF8Encoding.GetBytes(" COORDS=\"");
        internal static byte[] m_mapShape = uTF8Encoding.GetBytes(" SHAPE=\"");
        internal static byte[] m_name = uTF8Encoding.GetBytes(" NAME=\"");
        internal static byte[] m_dataName = uTF8Encoding.GetBytes(" data-name=\"");
        internal static byte[] m_circleShape = uTF8Encoding.GetBytes("circle");
        internal static byte[] m_polyShape = uTF8Encoding.GetBytes("poly");
        internal static byte[] m_rectShape = uTF8Encoding.GetBytes("rect");
        internal static byte[] m_comma = uTF8Encoding.GetBytes(",");
        internal static byte[] m_openLi = uTF8Encoding.GetBytes("<li");
        internal static byte[] m_closeLi = uTF8Encoding.GetBytes("</li>");
        internal static byte[] m_firstNonHeaderPostfix = uTF8Encoding.GetBytes("_FNHR");
        internal static byte[] m_fixedMatrixCornerPostfix = uTF8Encoding.GetBytes("_MCC");
        internal static byte[] m_fixedRowGroupingHeaderPostfix = uTF8Encoding.GetBytes("_FRGH");
        internal static byte[] m_fixedColumnGroupingHeaderPostfix = uTF8Encoding.GetBytes("_FCGH");
        internal static byte[] m_fixedRowHeaderPostfix = uTF8Encoding.GetBytes("_FRH");
        internal static byte[] m_fixedColumnHeaderPostfix = uTF8Encoding.GetBytes("_FCH");
        internal static byte[] m_fixedTableCornerPostfix = uTF8Encoding.GetBytes("_FCC");
        internal static byte[] m_dot = uTF8Encoding.GetBytes(".");
        internal static byte[] m_percentSizes = uTF8Encoding.GetBytes("r1");
        internal static byte[] m_percentSizesOverflow = uTF8Encoding.GetBytes("r2");
        internal static byte[] m_percentHeight = uTF8Encoding.GetBytes("r3");
        internal static byte[] m_ignoreBorder = uTF8Encoding.GetBytes("r4");
        internal static byte[] m_ignoreBorderL = uTF8Encoding.GetBytes("r5");
        internal static byte[] m_ignoreBorderR = uTF8Encoding.GetBytes("r6");
        internal static byte[] m_ignoreBorderT = uTF8Encoding.GetBytes("r7");
        internal static byte[] m_ignoreBorderB = uTF8Encoding.GetBytes("r8");
        internal static byte[] m_layoutFixed = uTF8Encoding.GetBytes("r9");
        internal static byte[] m_layoutBorder = uTF8Encoding.GetBytes("r10");
        internal static byte[] m_percentWidthOverflow = uTF8Encoding.GetBytes("r11");
        internal static byte[] m_popupAction = uTF8Encoding.GetBytes("r12");
        internal static byte[] m_styleAction = uTF8Encoding.GetBytes("r13");
        internal static byte[] m_emptyTextBox = uTF8Encoding.GetBytes("r14");
        internal static byte[] m_classPercentSizes = uTF8Encoding.GetBytes(" class=\"r1\"");
        internal static byte[] m_classPercentSizesOverflow = uTF8Encoding.GetBytes(" class=\"r2\"");
        internal static byte[] m_classPercentHeight = uTF8Encoding.GetBytes(" class=\"r3\"");
        internal static byte[] m_classLayoutBorder = uTF8Encoding.GetBytes(" class=\"r10");
        internal static byte[] m_classPopupAction = uTF8Encoding.GetBytes(" class=\"r12\"");
        internal static byte[] m_classAction = uTF8Encoding.GetBytes(" class=\"r13\"");
        internal static byte[] m_rtlEmbed = uTF8Encoding.GetBytes("r15");
        internal static byte[] m_classRtlEmbed = uTF8Encoding.GetBytes(" class=\"r15\"");
        internal static byte[] m_noVerticalMarginClassName = uTF8Encoding.GetBytes("r16");
        internal static byte[] m_classNoVerticalMargin = uTF8Encoding.GetBytes(" class=\"r16\"");
        internal static byte[] m_percentSizeInlineTable = uTF8Encoding.GetBytes("r17");
        internal static byte[] m_classPercentSizeInlineTable = uTF8Encoding.GetBytes(" class=\"r17\"");
        internal static byte[] m_percentHeightInlineTable = uTF8Encoding.GetBytes("r18");
        internal static byte[] m_classPercentHeightInlineTable = uTF8Encoding.GetBytes(" class=\"r18\"");
        internal static byte[] m_classCanGrowVerticalTextBox = uTF8Encoding.GetBytes(" class=\"canGrowVerticalTextBox\"");
        internal static byte[] m_classCanShrinkVerticalTextBox = uTF8Encoding.GetBytes(" class=\"canShrinkVerticalTextBox\"");
        internal static byte[] m_classCanGrowBothTextBox = uTF8Encoding.GetBytes(" class=\"canGrowVerticalTextBox canShrinkVerticalTextBox\"");
        internal static byte[] m_classCannotGrowTextBoxInTablix = uTF8Encoding.GetBytes(" cannotGrowTextBoxInTablix");
        internal static byte[] m_classCannotShrinkTextBoxInTablix = uTF8Encoding.GetBytes(" cannotShrinkTextBoxInTablix");
        internal static byte[] m_classCanGrowTextBoxInTablix = uTF8Encoding.GetBytes(" canGrowTextBoxInTablix");
        internal static byte[] m_classCanShrinkTextBoxInTablix = uTF8Encoding.GetBytes(" canShrinkTextBoxInTablix");
        internal static byte[] m_underscore = uTF8Encoding.GetBytes("_");
        internal static byte[] m_openAccol = uTF8Encoding.GetBytes("{");
        internal static byte[] m_closeAccol = uTF8Encoding.GetBytes("}");
        internal static byte[] m_closeParenthesis = uTF8Encoding.GetBytes(")");
        internal static byte[] m_classStyle = uTF8Encoding.GetBytes(" class=\"");
        internal static byte[] m_openStyle = uTF8Encoding.GetBytes(" style=\"");
        internal static byte[] m_styleHeight = uTF8Encoding.GetBytes("height:");
        internal static byte[] m_styleMinHeight = uTF8Encoding.GetBytes("min-height:");
        internal static byte[] m_styleMaxHeight = uTF8Encoding.GetBytes("max-height:");
        internal static byte[] m_styleWidth = uTF8Encoding.GetBytes("width:");
        internal static byte[] m_styleMinWidth = uTF8Encoding.GetBytes("min-width:");
        internal static byte[] m_styleMaxWidth = uTF8Encoding.GetBytes("max-width:");
        internal static byte[] m_zeroBorderWidth = uTF8Encoding.GetBytes("border-width:0px");
        internal static byte[] m_border = uTF8Encoding.GetBytes("border:");
        internal static byte[] m_borderLeft = uTF8Encoding.GetBytes("border-left:");
        internal static byte[] m_borderTop = uTF8Encoding.GetBytes("border-top:");
        internal static byte[] m_borderBottom = uTF8Encoding.GetBytes("border-bottom:");
        internal static byte[] m_borderRight = uTF8Encoding.GetBytes("border-right:");
        internal static byte[] m_borderColor = uTF8Encoding.GetBytes("border-color:");
        internal static byte[] m_borderStyle = uTF8Encoding.GetBytes("border-style:");
        internal static byte[] m_borderWidth = uTF8Encoding.GetBytes("border-width:");
        internal static byte[] m_borderBottomColor = uTF8Encoding.GetBytes("border-bottom-color:");
        internal static byte[] m_borderBottomStyle = uTF8Encoding.GetBytes("border-bottom-style:");
        internal static byte[] m_borderBottomWidth = uTF8Encoding.GetBytes("border-bottom-width:");
        internal static byte[] m_borderLeftColor = uTF8Encoding.GetBytes("border-left-color:");
        internal static byte[] m_borderLeftStyle = uTF8Encoding.GetBytes("border-left-style:");
        internal static byte[] m_borderLeftWidth = uTF8Encoding.GetBytes("border-left-width:");
        internal static byte[] m_borderRightColor = uTF8Encoding.GetBytes("border-right-color:");
        internal static byte[] m_borderRightStyle = uTF8Encoding.GetBytes("border-right-style:");
        internal static byte[] m_borderRightWidth = uTF8Encoding.GetBytes("border-right-width:");
        internal static byte[] m_borderTopColor = uTF8Encoding.GetBytes("border-top-color:");
        internal static byte[] m_borderTopStyle = uTF8Encoding.GetBytes("border-top-style:");
        internal static byte[] m_borderTopWidth = uTF8Encoding.GetBytes("border-top-width:");
        internal static byte[] m_boxSizingBorderBox = uTF8Encoding.GetBytes("box-sizing: border-box;");
        internal static byte[] m_semiColon = uTF8Encoding.GetBytes(";");
        internal static byte[] m_wordWrap = uTF8Encoding.GetBytes("word-wrap:break-word");
        internal static byte[] m_wordBreak = uTF8Encoding.GetBytes("word-break:break-word");
        internal static byte[] m_wordWrapNormal = uTF8Encoding.GetBytes("word-wrap:normal;");
        internal static byte[] m_wordBreakAll = uTF8Encoding.GetBytes("word-break:break-all;");
        internal static byte[] m_whiteSpacePreWrap = uTF8Encoding.GetBytes("white-space:pre-wrap");
        internal static byte[] m_overflow = uTF8Encoding.GetBytes("overflow:");
        internal static byte[] m_overflowHidden = uTF8Encoding.GetBytes("overflow:hidden");
        internal static byte[] m_overflowXHidden = uTF8Encoding.GetBytes("overflow-x:hidden");
        internal static byte[] m_borderCollapse = uTF8Encoding.GetBytes("border-collapse:collapse");
        internal static byte[] m_tableLayoutFixed = uTF8Encoding.GetBytes("table-layout:fixed");
        internal static byte[] m_paddingLeft = uTF8Encoding.GetBytes("padding-left:");
        internal static byte[] m_paddingRight = uTF8Encoding.GetBytes("padding-right:");
        internal static byte[] m_paddingTop = uTF8Encoding.GetBytes("padding-top:");
        internal static byte[] m_paddingBottom = uTF8Encoding.GetBytes("padding-bottom:");
        internal static byte[] m_backgroundColor = uTF8Encoding.GetBytes("background-color:");
        internal static byte[] m_backgroundImage = uTF8Encoding.GetBytes("background-image:url(");
        internal static byte[] m_backgroundRepeat = uTF8Encoding.GetBytes("background-repeat:");
        internal static byte[] m_backgroundSize = uTF8Encoding.GetBytes("background-size:");
        internal static byte[] m_fontStyle = uTF8Encoding.GetBytes("font-style:");
        internal static byte[] m_fontFamily = uTF8Encoding.GetBytes("font-family:");
        internal static byte[] m_fontSize = uTF8Encoding.GetBytes("font-size:");
        internal static byte[] m_fontWeight = uTF8Encoding.GetBytes("font-weight:");
        internal static byte[] m_textDecoration = uTF8Encoding.GetBytes("text-decoration:");
        internal static byte[] m_textAlign = uTF8Encoding.GetBytes("text-align:");
        internal static byte[] m_verticalAlign = uTF8Encoding.GetBytes("vertical-align:");
        internal static byte[] m_flexAlignItems = uTF8Encoding.GetBytes("align-items:");
        internal static byte[] m_msFlexAlignItems = uTF8Encoding.GetBytes("-ms-flex-align:");
        internal static byte[] m_webkitFlexAlignItems = uTF8Encoding.GetBytes("-webkit-align-items:");
        internal static byte[] m_flexJustifyContent = uTF8Encoding.GetBytes("justify-content:");
        internal static byte[] m_msFlexJustifyContent = uTF8Encoding.GetBytes("-ms-flex-pack:");
        internal static byte[] m_webkitFlexJustifyContent = uTF8Encoding.GetBytes("-webkit-justify-content:");
        internal static byte[] m_flexFlowRow = uTF8Encoding.GetBytes("-ms-flex-flow: row;-webkit-flex-flow: row;flex-flow: row;");
        internal static byte[] m_flexFlowRowReverse = uTF8Encoding.GetBytes("-ms-flex-flow: row-reverse;-webkit-flex-flow: row-reverse;flex-flow: row-reverse;");
        internal static byte[] m_color = uTF8Encoding.GetBytes("color:");
        internal static byte[] m_lineHeight = uTF8Encoding.GetBytes("line-height:");
        internal static byte[] m_direction = uTF8Encoding.GetBytes("direction:");
        internal static byte[] m_unicodeBiDi = uTF8Encoding.GetBytes("unicode-bidi:");
        internal static byte[] m_writingMode = uTF8Encoding.GetBytes("writing-mode:");
        internal static byte[] m_msoRotation = uTF8Encoding.GetBytes("mso-rotate:");
        internal static byte[] m_opacity = uTF8Encoding.GetBytes("opacity:");
        internal static byte[] m_tbrl = uTF8Encoding.GetBytes("tb-rl");
        internal static byte[] m_btrl = uTF8Encoding.GetBytes("bt-rl");
        internal static byte[] m_lrtb = uTF8Encoding.GetBytes("lr-tb");
        internal static byte[] m_rltb = uTF8Encoding.GetBytes("rl-tb");
        internal static byte[] m_rotate180deg = uTF8Encoding.GetBytes("transform: rotate(180deg);");
        internal static byte[] m_webkit_vertical = uTF8Encoding.GetBytes("-webkit-writing-mode: vertical-rl;");
        internal static byte[] m_ms_vertical = uTF8Encoding.GetBytes("-ms-writing-mode: tb-rl;");
        internal static byte[] m_ms_verticalRTL = uTF8Encoding.GetBytes("-ms-writing-mode: bt-rl;");
        internal static byte[] m_ff_vertical = uTF8Encoding.GetBytes("writing-mode: vertical-rl;");
        internal static byte[] m_layoutFlow = uTF8Encoding.GetBytes("layout-flow:");
        internal static byte[] m_verticalIdeographic = uTF8Encoding.GetBytes("vertical-ideographic");
        internal static byte[] m_horizontal = uTF8Encoding.GetBytes("horizontal");
        internal static byte[] m_cursorHand = uTF8Encoding.GetBytes("cursor:pointer");
        internal static byte[] m_filter = uTF8Encoding.GetBytes("filter:");
        internal static byte[] m_language = uTF8Encoding.GetBytes(" LANG=\"");
        internal static byte[] m_marginLeft = uTF8Encoding.GetBytes("margin-left:");
        internal static byte[] m_marginTop = uTF8Encoding.GetBytes("margin-top:");
        internal static byte[] m_marginBottom = uTF8Encoding.GetBytes("margin-bottom:");
        internal static byte[] m_marginRight = uTF8Encoding.GetBytes("margin-right:");
        internal static byte[] m_textIndent = uTF8Encoding.GetBytes("text-indent:");
        internal static byte[] m_percentSizeString = uTF8Encoding.GetBytes("%");
        internal static byte[] m_onLoadFitProportionalPv = uTF8Encoding.GetBytes(" onload=\"this.fitproportional=true;this.pv=");
        internal static byte[] m_basicImageRotation180 = uTF8Encoding.GetBytes("progid:DXImageTransform.Microsoft.BasicImage(rotation=2)");
        internal static byte[] m_openVGroup = uTF8Encoding.GetBytes("<v:group coordsize=\"100,100\" coordorigin=\"0,0\"");
        internal static byte[] m_openVLine = uTF8Encoding.GetBytes("<v:line from=\"0,");
        internal static byte[] m_strokeColor = uTF8Encoding.GetBytes(" strokecolor=\"");
        internal static byte[] m_strokeWeight = uTF8Encoding.GetBytes(" strokeWeight=\"");
        internal static byte[] m_dashStyle = uTF8Encoding.GetBytes("<v:stroke dashstyle=\"");
        internal static byte[] m_slineStyle = uTF8Encoding.GetBytes(" slineStyle=\"");
        internal static byte[] m_closeVGroup = uTF8Encoding.GetBytes("</v:line></v:group>");
        internal static byte[] m_rightSlant = uTF8Encoding.GetBytes("100\" to=\"100,0\"");
        internal static byte[] m_leftSlant = uTF8Encoding.GetBytes("0\" to=\"100,100\"");
        internal static byte[] m_pageBreakDelimiter = uTF8Encoding.GetBytes("<div style=\"page-break-after:always\"><hr/></div>");
        internal static byte[] m_stylePositionAbsolute = uTF8Encoding.GetBytes("position:absolute;");
        internal static byte[] m_stylePositionRelative = uTF8Encoding.GetBytes("position:relative;");
        internal static byte[] m_styleTop = uTF8Encoding.GetBytes("top:");
        internal static byte[] m_styleLeft = uTF8Encoding.GetBytes("left:");
        internal static byte[] m_closeUL = uTF8Encoding.GetBytes("</ul>");
        internal static byte[] m_closeOL = uTF8Encoding.GetBytes("</ol>");
        internal static byte[] m_olArabic = uTF8Encoding.GetBytes("<ol");
        internal static byte[] m_olRoman = uTF8Encoding.GetBytes("<ol type=\"i\"");
        internal static byte[] m_olAlpha = uTF8Encoding.GetBytes("<ol type=\"a\"");
        internal static byte[] m_ulDisc = uTF8Encoding.GetBytes("<ul type=\"disc\"");
        internal static byte[] m_ulSquare = uTF8Encoding.GetBytes("<ul type=\"square\"");
        internal static byte[] m_ulCircle = uTF8Encoding.GetBytes("<ul type=\"circle\"");

        internal static byte[] m_openInlineStyle = uTF8Encoding.GetBytes("<style type=\"text/css\">");
        internal static byte[] m_closeInlineStyle = uTF8Encoding.GetBytes("</style>");
        internal static byte[] m_openInlineJavaScript = uTF8Encoding.GetBytes("<script type=\"text/javascript\">");
        internal static byte[] m_closeInlineJavaScript = uTF8Encoding.GetBytes("</script>");
        internal static byte[] m_reportItemCustomAttr = uTF8Encoding.GetBytes(" data-reportitem=\"");
        internal static byte[] m_noRepeat = uTF8Encoding.GetBytes("no-repeat");
        internal static byte[] m_defaultPixelSize = uTF8Encoding.GetBytes("5px");
        internal static byte[] m_auto = uTF8Encoding.GetBytes("auto");
        internal static byte[] m_checked = null;
        internal static byte[] m_unchecked = null;
        internal static byte[] m_showHideOnClick = null;
        internal static byte[] m_space = uTF8Encoding.GetBytes(HTMLElements.m_spaceString);
        internal static byte[] m_quote = uTF8Encoding.GetBytes(HTMLElements.m_quoteString);
		internal static byte[] m_closeQuote = uTF8Encoding.GetBytes(HTMLElements.m_closeQuoteString);
		internal static byte[] m_href = uTF8Encoding.GetBytes(HTMLElements.m_hrefString);
		internal static byte[] m_lineBreak = uTF8Encoding.GetBytes(HTMLElements.m_standardLineBreak);			 
		internal static byte[] m_mapPrefix = uTF8Encoding.GetBytes(HTMLElements.m_mapPrefixString);
 
	}
}