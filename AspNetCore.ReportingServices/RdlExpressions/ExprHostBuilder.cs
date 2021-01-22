using AspNetCore.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportProcessing;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace AspNetCore.ReportingServices.RdlExpressions
{
	public sealed class ExprHostBuilder
	{
		public enum ErrorSource
		{
			Expression,
			CodeModuleClassInstanceDecl,
			CustomCode,
			Unknown
		}

		public enum DataRegionMode
		{
			Tablix,
			Chart,
			GaugePanel,
			CustomReportItem,
			MapDataRegion,
			DataShape
		}

		private static class Constants
		{
			public const string ReportObjectModelNS = "AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel";

			public const string ExprHostObjectModelNS = "AspNetCore.ReportingServices.RdlExpressions.ExpressionHostObjectModel";

			public const string ReportExprHost = "ReportExprHost";

			public const string IndexedExprHost = "IndexedExprHost";

			public const string ReportParamExprHost = "ReportParamExprHost";

			public const string CalcFieldExprHost = "CalcFieldExprHost";

			public const string DataSourceExprHost = "DataSourceExprHost";

			public const string DataSetExprHost = "DataSetExprHost";

			public const string ReportItemExprHost = "ReportItemExprHost";

			public const string ActionExprHost = "ActionExprHost";

			public const string ActionInfoExprHost = "ActionInfoExprHost";

			public const string TextBoxExprHost = "TextBoxExprHost";

			public const string ImageExprHost = "ImageExprHost";

			public const string ParamExprHost = "ParamExprHost";

			public const string SubreportExprHost = "SubreportExprHost";

			public const string SortExprHost = "SortExprHost";

			public const string FilterExprHost = "FilterExprHost";

			public const string GroupExprHost = "GroupExprHost";

			public const string StyleExprHost = "StyleExprHost";

			public const string AggregateParamExprHost = "AggregateParamExprHost";

			public const string LookupExprHost = "LookupExprHost";

			public const string LookupDestExprHost = "LookupDestExprHost";

			public const string ReportSectionExprHost = "ReportSectionExprHost";

			public const string JoinConditionExprHost = "JoinConditionExprHost";

			public const string IncludeParametersParam = "includeParameters";

			public const string ParametersOnlyParam = "parametersOnly";

			public const string CustomCodeProxy = "CustomCodeProxy";

			public const string CustomCodeProxyBase = "CustomCodeProxyBase";

			public const string ReportObjectModelParam = "reportObjectModel";

			public const string SetReportObjectModel = "SetReportObjectModel";

			public const string Code = "Code";

			public const string CodeProxyBase = "m_codeProxyBase";

			public const string CodeParam = "code";

			public const string Report = "Report";

			public const string RemoteArrayWrapper = "RemoteArrayWrapper";

			public const string RemoteMemberArrayWrapper = "RemoteMemberArrayWrapper";

			public const string LabelExpr = "LabelExpr";

			public const string ValueExpr = "ValueExpr";

			public const string NoRowsExpr = "NoRowsExpr";

			public const string ParameterHosts = "m_parameterHostsRemotable";

			public const string IndexParam = "index";

			public const string FilterHosts = "m_filterHostsRemotable";

			public const string SortHost = "m_sortHost";

			public const string GroupHost = "m_groupHost";

			public const string VisibilityHiddenExpr = "VisibilityHiddenExpr";

			public const string SortDirectionHosts = "SortDirectionHosts";

			public const string DataValueHosts = "m_dataValueHostsRemotable";

			public const string CustomPropertyHosts = "m_customPropertyHostsRemotable";

			public const string VariableValueHosts = "VariableValueHosts";

			public const string ReportLanguageExpr = "ReportLanguageExpr";

			public const string AutoRefreshExpr = "AutoRefreshExpr";

			public const string AggregateParamHosts = "m_aggregateParamHostsRemotable";

			public const string ReportParameterHosts = "m_reportParameterHostsRemotable";

			public const string DataSourceHosts = "m_dataSourceHostsRemotable";

			public const string DataSetHosts = "m_dataSetHostsRemotable";

			public const string PageSectionHosts = "m_pageSectionHostsRemotable";

			public const string PageHosts = "m_pageHostsRemotable";

			public const string ReportSectionHosts = "m_reportSectionHostsRemotable";

			public const string LineHosts = "m_lineHostsRemotable";

			public const string RectangleHosts = "m_rectangleHostsRemotable";

			public const string TextBoxHosts = "m_textBoxHostsRemotable";

			public const string ImageHosts = "m_imageHostsRemotable";

			public const string SubreportHosts = "m_subreportHostsRemotable";

			public const string TablixHosts = "m_tablixHostsRemotable";

			public const string ChartHosts = "m_chartHostsRemotable";

			public const string GaugePanelHosts = "m_gaugePanelHostsRemotable";

			public const string CustomReportItemHosts = "m_customReportItemHostsRemotable";

			public const string LookupExprHosts = "m_lookupExprHostsRemotable";

			public const string LookupDestExprHosts = "m_lookupDestExprHostsRemotable";

			public const string ReportInitialPageName = "InitialPageNameExpr";

			public const string ConnectStringExpr = "ConnectStringExpr";

			public const string FieldHosts = "m_fieldHostsRemotable";

			public const string QueryParametersHost = "QueryParametersHost";

			public const string QueryCommandTextExpr = "QueryCommandTextExpr";

			public const string JoinConditionHosts = "m_joinConditionExprHostsRemotable";

			public const string PromptExpr = "PromptExpr";

			public const string ValidValuesHost = "ValidValuesHost";

			public const string ValidValueLabelsHost = "ValidValueLabelsHost";

			public const string ValidationExpressionExpr = "ValidationExpressionExpr";

			public const string ActionInfoHost = "ActionInfoHost";

			public const string ActionHost = "ActionHost";

			public const string ActionItemHosts = "m_actionItemHostsRemotable";

			public const string BookmarkExpr = "BookmarkExpr";

			public const string ToolTipExpr = "ToolTipExpr";

			public const string ToggleImageInitialStateExpr = "ToggleImageInitialStateExpr";

			public const string UserSortExpressionsHost = "UserSortExpressionsHost";

			public const string MIMETypeExpr = "MIMETypeExpr";

			public const string TagExpr = "TagExpr";

			public const string OmitExpr = "OmitExpr";

			public const string HyperlinkExpr = "HyperlinkExpr";

			public const string DrillThroughReportNameExpr = "DrillThroughReportNameExpr";

			public const string DrillThroughParameterHosts = "m_drillThroughParameterHostsRemotable";

			public const string DrillThroughBookmakLinkExpr = "DrillThroughBookmarkLinkExpr";

			public const string BookmarkLinkExpr = "BookmarkLinkExpr";

			public const string FilterExpressionExpr = "FilterExpressionExpr";

			public const string ParentExpressionsHost = "ParentExpressionsHost";

			public const string ReGroupExpressionsHost = "ReGroupExpressionsHost";

			public const string DataValueExprHost = "DataValueExprHost";

			public const string DataValueNameExpr = "DataValueNameExpr";

			public const string DataValueValueExpr = "DataValueValueExpr";

			public const string TablixExprHost = "TablixExprHost";

			public const string DataShapeExprHost = "DataShapeExprHost";

			public const string ChartExprHost = "ChartExprHost";

			public const string GaugePanelExprHost = "GaugePanelExprHost";

			public const string CustomReportItemExprHost = "CustomReportItemExprHost";

			public const string MapDataRegionExprHost = "MapDataRegionExprHost";

			public const string TablixMemberExprHost = "TablixMemberExprHost";

			public const string DataShapeMemberExprHost = "DataShapeMemberExprHost";

			public const string ChartMemberExprHost = "ChartMemberExprHost";

			public const string GaugeMemberExprHost = "GaugeMemberExprHost";

			public const string DataGroupExprHost = "DataGroupExprHost";

			public const string TablixCellExprHost = "TablixCellExprHost";

			public const string DataShapeIntersectionExprHost = "DataShapeIntersectionExprHost";

			public const string ChartDataPointExprHost = "ChartDataPointExprHost";

			public const string GaugeCellExprHost = "GaugeCellExprHost";

			public const string DataCellExprHost = "DataCellExprHost";

			public const string MemberTreeHosts = "m_memberTreeHostsRemotable";

			public const string DataCellHosts = "m_cellHostsRemotable";

			public const string MapMemberExprHost = "MapMemberExprHost";

			public const string TablixCornerCellHosts = "m_cornerCellHostsRemotable";

			public const string ChartTitleExprHost = "ChartTitleExprHost";

			public const string ChartAxisTitleExprHost = "ChartAxisTitleExprHost";

			public const string ChartLegendTitleExprHost = "ChartLegendTitleExprHost";

			public const string ChartLegendExprHost = "ChartLegendExprHost";

			public const string ChartTitleHost = "TitleHost";

			public const string ChartNoDataMessageHost = "NoDataMessageHost";

			public const string ChartLegendTitleHost = "TitleExprHost";

			public const string PaletteExpr = "PaletteExpr";

			public const string PaletteHatchBehaviorExpr = "PaletteHatchBehaviorExpr";

			public const string ChartAreaExprHost = "ChartAreaExprHost";

			public const string ChartNoMoveDirectionsExprHost = "ChartNoMoveDirectionsExprHost";

			public const string ChartNoMoveDirectionsHost = "NoMoveDirectionsHost";

			public const string UpExpr = "UpExpr";

			public const string DownExpr = "DownExpr";

			public const string LeftExpr = "LeftExpr";

			public const string RightExpr = "RightExpr";

			public const string UpLeftExpr = "UpLeftExpr";

			public const string UpRightExpr = "UpRightExpr";

			public const string DownLeftExpr = "DownLeftExpr";

			public const string DownRightExpr = "DownRightExpr";

			public const string ChartSmartLabelExprHost = "ChartSmartLabelExprHost";

			public const string ChartSmartLabelHost = "SmartLabelHost";

			public const string AllowOutSidePlotAreaExpr = "AllowOutSidePlotAreaExpr";

			public const string CalloutBackColorExpr = "CalloutBackColorExpr";

			public const string CalloutLineAnchorExpr = "CalloutLineAnchorExpr";

			public const string CalloutLineColorExpr = "CalloutLineColorExpr";

			public const string CalloutLineStyleExpr = "CalloutLineStyleExpr";

			public const string CalloutLineWidthExpr = "CalloutLineWidthExpr";

			public const string CalloutStyleExpr = "CalloutStyleExpr";

			public const string HideOverlappedExpr = "HideOverlappedExpr";

			public const string MarkerOverlappingExpr = "MarkerOverlappingExpr";

			public const string MaxMovingDistanceExpr = "MaxMovingDistanceExpr";

			public const string MinMovingDistanceExpr = "MinMovingDistanceExpr";

			public const string DisabledExpr = "DisabledExpr";

			public const string ChartAxisScaleBreakExprHost = "ChartAxisScaleBreakExprHost";

			public const string ChartAxisScaleBreakHost = "AxisScaleBreakHost";

			public const string ChartBorderSkinExprHost = "ChartBorderSkinExprHost";

			public const string ChartBorderSkinHost = "BorderSkinHost";

			public const string TitleSeparatorExpr = "TitleSeparatorExpr";

			public const string ColumnTypeExpr = "ColumnTypeExpr";

			public const string MinimumWidthExpr = "MinimumWidthExpr";

			public const string MaximumWidthExpr = "MaximumWidthExpr";

			public const string SeriesSymbolWidthExpr = "SeriesSymbolWidthExpr";

			public const string SeriesSymbolHeightExpr = "SeriesSymbolHeightExpr";

			public const string CellTypeExpr = "CellTypeExpr";

			public const string TextExpr = "TextExpr";

			public const string CellSpanExpr = "CellSpanExpr";

			public const string ImageWidthExpr = "ImageWidthExpr";

			public const string ImageHeightExpr = "ImageHeightExpr";

			public const string SymbolHeightExpr = "SymbolHeightExpr";

			public const string SymbolWidthExpr = "SymbolWidthExpr";

			public const string AlignmentExpr = "AlignmentExpr";

			public const string TopMarginExpr = "TopMarginExpr";

			public const string BottomMarginExpr = "BottomMarginExpr";

			public const string LeftMarginExpr = "LeftMarginExpr";

			public const string RightMarginExpr = "RightMarginExpr";

			public const string VisibleExpr = "VisibleExpr";

			public const string MarginExpr = "MarginExpr";

			public const string IntervalExpr = "IntervalExpr";

			public const string IntervalTypeExpr = "IntervalTypeExpr";

			public const string IntervalOffsetExpr = "IntervalOffsetExpr";

			public const string IntervalOffsetTypeExpr = "IntervalOffsetTypeExpr";

			public const string MarksAlwaysAtPlotEdgeExpr = "MarksAlwaysAtPlotEdgeExpr";

			public const string ReverseExpr = "ReverseExpr";

			public const string LocationExpr = "LocationExpr";

			public const string InterlacedExpr = "InterlacedExpr";

			public const string InterlacedColorExpr = "InterlacedColorExpr";

			public const string LogScaleExpr = "LogScaleExpr";

			public const string LogBaseExpr = "LogBaseExpr";

			public const string HideLabelsExpr = "HideLabelsExpr";

			public const string AngleExpr = "AngleExpr";

			public const string ArrowsExpr = "ArrowsExpr";

			public const string PreventFontShrinkExpr = "PreventFontShrinkExpr";

			public const string PreventFontGrowExpr = "PreventFontGrowExpr";

			public const string PreventLabelOffsetExpr = "PreventLabelOffsetExpr";

			public const string PreventWordWrapExpr = "PreventWordWrapExpr";

			public const string AllowLabelRotationExpr = "AllowLabelRotationExpr";

			public const string IncludeZeroExpr = "IncludeZeroExpr";

			public const string LabelsAutoFitDisabledExpr = "LabelsAutoFitDisabledExpr";

			public const string MinFontSizeExpr = "MinFontSizeExpr";

			public const string MaxFontSizeExpr = "MaxFontSizeExpr";

			public const string OffsetLabelsExpr = "OffsetLabelsExpr";

			public const string HideEndLabelsExpr = "HideEndLabelsExpr";

			public const string ChartTickMarksExprHost = "ChartTickMarksExprHost";

			public const string ChartTickMarksHost = "TickMarksHost";

			public const string ChartGridLinesExprHost = "ChartGridLinesExprHost";

			public const string ChartGridLinesHost = "GridLinesHost";

			public const string ChartDataPointInLegendExprHost = "ChartDataPointInLegendExprHost";

			public const string ChartDataPointInLegendHost = "DataPointInLegendHost";

			public const string ChartEmptyPointsExprHost = "ChartEmptyPointsExprHost";

			public const string ChartEmptyPointsHost = "EmptyPointsHost";

			public const string AxisLabelExpr = "AxisLabelExpr";

			public const string LegendTextExpr = "LegendTextExpr";

			public const string ChartLegendColumnHeaderExprHost = "ChartLegendColumnHeaderExprHost";

			public const string ChartLegendColumnHeaderHost = "ChartLegendColumnHeaderHost";

			public const string ChartCustomPaletteColorExprHost = "ChartCustomPaletteColorExprHost";

			public const string ChartCustomPaletteColorHosts = "m_customPaletteColorHostsRemotable";

			public const string ChartLegendCustomItemCellExprHost = "ChartLegendCustomItemCellExprHost";

			public const string ChartLegendCustomItemCellsHosts = "m_legendCustomItemCellHostsRemotable";

			public const string ChartDerivedSeriesExprHost = "ChartDerivedSeriesExprHost";

			public const string ChartDerivedSeriesCollectionHosts = "m_derivedSeriesCollectionHostsRemotable";

			public const string SourceChartSeriesNameExpr = "SourceChartSeriesNameExpr";

			public const string DerivedSeriesFormulaExpr = "DerivedSeriesFormulaExpr";

			public const string SizeExpr = "SizeExpr";

			public const string TypeExpr = "TypeExpr";

			public const string SubtypeExpr = "SubtypeExpr";

			public const string LegendNameExpr = "LegendNameExpr";

			public const string ChartAreaNameExpr = "ChartAreaNameExpr";

			public const string ValueAxisNameExpr = "ValueAxisNameExpr";

			public const string CategoryAxisNameExpr = "CategoryAxisNameExpr";

			public const string ChartStripLineExprHost = "ChartStripLineExprHost";

			public const string ChartStripLinesHosts = "m_stripLinesHostsRemotable";

			public const string ChartSeriesExprHost = "ChartSeriesExprHost";

			public const string ChartSeriesHost = "ChartSeriesHost";

			public const string TitleExpr = "TitleExpr";

			public const string TitleAngleExpr = "TitleAngleExpr";

			public const string StripWidthExpr = "StripWidthExpr";

			public const string StripWidthTypeExpr = "StripWidthTypeExpr";

			public const string HiddenExpr = "HiddenExpr";

			public const string ChartFormulaParameterExprHost = "ChartFormulaParameterExprHost";

			public const string ChartFormulaParametersHosts = "m_formulaParametersHostsRemotable";

			public const string ChartLegendColumnExprHost = "ChartLegendColumnExprHost";

			public const string ChartLegendColumnsHosts = "m_legendColumnsHostsRemotable";

			public const string ChartLegendCustomItemExprHost = "ChartLegendCustomItemExprHost";

			public const string ChartLegendCustomItemsHosts = "m_legendCustomItemsHostsRemotable";

			public const string SeparatorExpr = "SeparatorExpr";

			public const string SeparatorColorExpr = "SeparatorColorExpr";

			public const string ChartValueAxesHosts = "m_valueAxesHostsRemotable";

			public const string ChartCategoryAxesHosts = "m_categoryAxesHostsRemotable";

			public const string ChartTitlesHosts = "m_titlesHostsRemotable";

			public const string ChartLegendsHosts = "m_legendsHostsRemotable";

			public const string ChartAreasHosts = "m_chartAreasHostsRemotable";

			public const string ChartAxisExprHost = "ChartAxisExprHost";

			public const string MemberLabelExpr = "MemberLabelExpr";

			public const string MemberStyleHost = "MemberStyleHost";

			public const string DataLabelStyleHost = "DataLabelStyleHost";

			public const string StyleHost = "StyleHost";

			public const string MarkerStyleHost = "MarkerStyleHost";

			public const string CaptionExpr = "CaptionExpr";

			public const string CategoryAxisHost = "CategoryAxisHost";

			public const string PlotAreaHost = "PlotAreaHost";

			public const string AxisMinExpr = "AxisMinExpr";

			public const string AxisMaxExpr = "AxisMaxExpr";

			public const string AxisCrossAtExpr = "AxisCrossAtExpr";

			public const string AxisMajorIntervalExpr = "AxisMajorIntervalExpr";

			public const string AxisMinorIntervalExpr = "AxisMinorIntervalExpr";

			public const string ChartDataPointValueXExpr = "DataPointValuesXExpr";

			public const string ChartDataPointValueYExpr = "DataPointValuesYExpr";

			public const string ChartDataPointValueSizeExpr = "DataPointValuesSizeExpr";

			public const string ChartDataPointValueHighExpr = "DataPointValuesHighExpr";

			public const string ChartDataPointValueLowExpr = "DataPointValuesLowExpr";

			public const string ChartDataPointValueStartExpr = "DataPointValuesStartExpr";

			public const string ChartDataPointValueEndExpr = "DataPointValuesEndExpr";

			public const string ChartDataPointValueMeanExpr = "DataPointValuesMeanExpr";

			public const string ChartDataPointValueMedianExpr = "DataPointValuesMedianExpr";

			public const string ChartDataPointValueHighlightXExpr = "DataPointValuesHighlightXExpr";

			public const string ChartDataPointValueHighlightYExpr = "DataPointValuesHighlightYExpr";

			public const string ChartDataPointValueHighlightSizeExpr = "DataPointValuesHighlightSizeExpr";

			public const string ChartDataPointValueFormatXExpr = "ChartDataPointValueFormatXExpr";

			public const string ChartDataPointValueFormatYExpr = "ChartDataPointValueFormatYExpr";

			public const string ChartDataPointValueFormatSizeExpr = "ChartDataPointValueFormatSizeExpr";

			public const string ChartDataPointValueCurrencyLanguageXExpr = "ChartDataPointValueCurrencyLanguageXExpr";

			public const string ChartDataPointValueCurrencyLanguageYExpr = "ChartDataPointValueCurrencyLanguageYExpr";

			public const string ChartDataPointValueCurrencyLanguageSizeExpr = "ChartDataPointValueCurrencyLanguageSizeExpr";

			public const string ColorExpr = "ColorExpr";

			public const string BorderSkinTypeExpr = "BorderSkinTypeExpr";

			public const string LengthExpr = "LengthExpr";

			public const string EnabledExpr = "EnabledExpr";

			public const string BreakLineTypeExpr = "BreakLineTypeExpr";

			public const string CollapsibleSpaceThresholdExpr = "CollapsibleSpaceThresholdExpr";

			public const string MaxNumberOfBreaksExpr = "MaxNumberOfBreaksExpr";

			public const string SpacingExpr = "SpacingExpr";

			public const string AxesViewExpr = "AxesViewExpr";

			public const string CursorExpr = "CursorExpr";

			public const string InnerPlotPositionExpr = "InnerPlotPositionExpr";

			public const string ChartAlignTypePositionExpr = "ChartAlignTypePositionExpr";

			public const string EquallySizedAxesFontExpr = "EquallySizedAxesFontExpr";

			public const string AlignOrientationExpr = "AlignOrientationExpr";

			public const string Chart3DPropertiesExprHost = "Chart3DPropertiesExprHost";

			public const string Chart3DPropertiesHost = "Chart3DPropertiesHost";

			public const string LayoutExpr = "LayoutExpr";

			public const string DockOutsideChartAreaExpr = "DockOutsideChartAreaExpr";

			public const string TitleExprHost = "TitleExprHost";

			public const string AutoFitTextDisabledExpr = "AutoFitTextDisabledExpr";

			public const string HeaderSeparatorExpr = "HeaderSeparatorExpr";

			public const string HeaderSeparatorColorExpr = "HeaderSeparatorColorExpr";

			public const string ColumnSeparatorExpr = "ColumnSeparatorExpr";

			public const string ColumnSeparatorColorExpr = "ColumnSeparatorColorExpr";

			public const string ColumnSpacingExpr = "ColumnSpacingExpr";

			public const string InterlacedRowsExpr = "InterlacedRowsExpr";

			public const string InterlacedRowsColorExpr = "InterlacedRowsColorExpr";

			public const string EquallySpacedItemsExpr = "EquallySpacedItemsExpr";

			public const string ReversedExpr = "ReversedExpr";

			public const string MaxAutoSizeExpr = "MaxAutoSizeExpr";

			public const string TextWrapThresholdExpr = "TextWrapThresholdExpr";

			public const string DockingExpr = "DockingExpr";

			public const string ChartTitlePositionExpr = "ChartTitlePositionExpr";

			public const string DockingOffsetExpr = "DockingOffsetExpr";

			public const string ChartLegendPositionExpr = "ChartLegendPositionExpr";

			public const string DockOutsideChartArea = "DockOutsideChartArea";

			public const string AutoFitTextDisabled = "AutoFitTextDisabled";

			public const string MinFontSize = "MinFontSize";

			public const string HeaderSeparator = "HeaderSeparator";

			public const string HeaderSeparatorColor = "HeaderSeparatorColor";

			public const string ColumnSeparator = "ColumnSeparator";

			public const string ColumnSeparatorColor = "ColumnSeparatorColor";

			public const string ColumnSpacing = "ColumnSpacing";

			public const string InterlacedRows = "InterlacedRows";

			public const string InterlacedRowsColor = "InterlacedRowsColor";

			public const string EquallySpacedItems = "EquallySpacedItems";

			public const string HideInLegendExpr = "HideInLegendExpr";

			public const string ShowOverlappedExpr = "ShowOverlappedExpr";

			public const string MajorChartTickMarksHost = "MajorTickMarksHost";

			public const string MinorChartTickMarksHost = "MinorTickMarksHost";

			public const string MajorChartGridLinesHost = "MajorGridLinesHost";

			public const string MinorChartGridLinesHost = "MinorGridLinesHost";

			public const string RotationExpr = "RotationExpr";

			public const string ProjectionModeExpr = "ProjectionModeExpr";

			public const string InclinationExpr = "InclinationExpr";

			public const string PerspectiveExpr = "PerspectiveExpr";

			public const string DepthRatioExpr = "DepthRatioExpr";

			public const string ShadingExpr = "ShadingExpr";

			public const string GapDepthExpr = "GapDepthExpr";

			public const string WallThicknessExpr = "WallThicknessExpr";

			public const string ClusteredExpr = "ClusteredExpr";

			public const string ChartDataLabelExprHost = "ChartDataLabelExprHost";

			public const string ChartDataLabelPositionExpr = "ChartDataLabelPositionExpr";

			public const string UseValueAsLabelExpr = "UseValueAsLabelExpr";

			public const string ChartDataLabelHost = "DataLabelHost";

			public const string ChartMarkerExprHost = "ChartMarkerExprHost";

			public const string ChartMarkerHost = "ChartMarkerHost";

			public const string VariableAutoIntervalExpr = "VariableAutoIntervalExpr";

			public const string LabelIntervalExpr = "LabelIntervalExpr";

			public const string LabelIntervalTypeExpr = "LabelIntervalTypeExpr";

			public const string LabelIntervalOffsetExpr = "LabelIntervalOffsetExpr";

			public const string LabelIntervalOffsetTypeExpr = "LabelIntervalOffsetTypeExpr";

			public const string DynamicWidthExpr = "DynamicWidthExpr";

			public const string DynamicHeightExpr = "DynamicHeightExpr";

			public const string TextOrientationExpr = "TextOrientationExpr";

			public const string ChartElementPositionExprHost = "ChartElementPositionExprHost";

			public const string ChartElementPositionHost = "ChartElementPositionHost";

			public const string ChartInnerPlotPositionHost = "ChartInnerPlotPositionHost";

			public const string BaseGaugeImageExprHost = "BaseGaugeImageExprHost";

			public const string BaseGaugeImageHost = "BaseGaugeImageHost";

			public const string SourceExpr = "SourceExpr";

			public const string TransparentColorExpr = "TransparentColorExpr";

			public const string CapImageExprHost = "CapImageExprHost";

			public const string CapImageHost = "CapImageHost";

			public const string TopImageHost = "TopImageHost";

			public const string TopImageExprHost = "TopImageExprHost";

			public const string HueColorExpr = "HueColorExpr";

			public const string OffsetXExpr = "OffsetXExpr";

			public const string OffsetYExpr = "OffsetYExpr";

			public const string FrameImageExprHost = "FrameImageExprHost";

			public const string FrameImageHost = "FrameImageHost";

			public const string IndicatorImageExprHost = "IndicatorImageExprHost";

			public const string IndicatorImageHost = "IndicatorImageHost";

			public const string TransparencyExpr = "TransparencyExpr";

			public const string ClipImageExpr = "ClipImageExpr";

			public const string PointerImageExprHost = "PointerImageExprHost";

			public const string PointerImageHost = "PointerImageHost";

			public const string BackFrameExprHost = "BackFrameExprHost";

			public const string BackFrameHost = "BackFrameHost";

			public const string FrameStyleExpr = "FrameStyleExpr";

			public const string FrameShapeExpr = "FrameShapeExpr";

			public const string FrameWidthExpr = "FrameWidthExpr";

			public const string GlassEffectExpr = "GlassEffectExpr";

			public const string FrameBackgroundExprHost = "FrameBackgroundExprHost";

			public const string FrameBackgroundHost = "FrameBackgroundHost";

			public const string CustomLabelExprHost = "CustomLabelExprHost";

			public const string FontAngleExpr = "FontAngleExpr";

			public const string UseFontPercentExpr = "UseFontPercentExpr";

			public const string GaugeExprHost = "GaugeExprHost";

			public const string ClipContentExpr = "ClipContentExpr";

			public const string GaugeImageExprHost = "GaugeImageExprHost";

			public const string AspectRatioExpr = "AspectRatioExpr";

			public const string GaugeInputValueExprHost = "GaugeInputValueExprHost";

			public const string FormulaExpr = "FormulaExpr";

			public const string MinPercentExpr = "MinPercentExpr";

			public const string MaxPercentExpr = "MaxPercentExpr";

			public const string MultiplierExpr = "MultiplierExpr";

			public const string AddConstantExpr = "AddConstantExpr";

			public const string GaugeLabelExprHost = "GaugeLabelExprHost";

			public const string AntiAliasingExpr = "AntiAliasingExpr";

			public const string AutoLayoutExpr = "AutoLayoutExpr";

			public const string ShadowIntensityExpr = "ShadowIntensityExpr";

			public const string TileLanguageExpr = "TileLanguageExpr";

			public const string TextAntiAliasingQualityExpr = "TextAntiAliasingQualityExpr";

			public const string GaugePanelItemExprHost = "GaugePanelItemExprHost";

			public const string TopExpr = "TopExpr";

			public const string HeightExpr = "HeightExpr";

			public const string GaugePointerExprHost = "GaugePointerExprHost";

			public const string BarStartExpr = "BarStartExpr";

			public const string MarkerLengthExpr = "MarkerLengthExpr";

			public const string MarkerStyleExpr = "MarkerStyleExpr";

			public const string SnappingEnabledExpr = "SnappingEnabledExpr";

			public const string SnappingIntervalExpr = "SnappingIntervalExpr";

			public const string GaugeScaleExprHost = "GaugeScaleExprHost";

			public const string LogarithmicExpr = "LogarithmicExpr";

			public const string LogarithmicBaseExpr = "LogarithmicBaseExpr";

			public const string TickMarksOnTopExpr = "TickMarksOnTopExpr";

			public const string GaugeTickMarksExprHost = "GaugeTickMarksExprHost";

			public const string LinearGaugeExprHost = "LinearGaugeExprHost";

			public const string OrientationExpr = "OrientationExpr";

			public const string LinearPointerExprHost = "LinearPointerExprHost";

			public const string LinearScaleExprHost = "LinearScaleExprHost";

			public const string StartMarginExpr = "StartMarginExpr";

			public const string EndMarginExpr = "EndMarginExpr";

			public const string NumericIndicatorExprHost = "NumericIndicatorExprHost";

			public const string PinLabelExprHost = "PinLabelExprHost";

			public const string AllowUpsideDownExpr = "AllowUpsideDownExpr";

			public const string RotateLabelExpr = "RotateLabelExpr";

			public const string PointerCapExprHost = "PointerCapExprHost";

			public const string OnTopExpr = "OnTopExpr";

			public const string ReflectionExpr = "ReflectionExpr";

			public const string CapStyleExpr = "CapStyleExpr";

			public const string RadialGaugeExprHost = "RadialGaugeExprHost";

			public const string PivotXExpr = "PivotXExpr";

			public const string PivotYExpr = "PivotYExpr";

			public const string RadialPointerExprHost = "RadialPointerExprHost";

			public const string NeedleStyleExpr = "NeedleStyleExpr";

			public const string RadialScaleExprHost = "RadialScaleExprHost";

			public const string RadiusExpr = "RadiusExpr";

			public const string StartAngleExpr = "StartAngleExpr";

			public const string SweepAngleExpr = "SweepAngleExpr";

			public const string ScaleLabelsExprHost = "ScaleLabelsExprHost";

			public const string RotateLabelsExpr = "RotateLabelsExpr";

			public const string ShowEndLabelsExpr = "ShowEndLabelsExpr";

			public const string ScalePinExprHost = "ScalePinExprHost";

			public const string EnableExpr = "EnableExpr";

			public const string ScaleRangeExprHost = "ScaleRangeExprHost";

			public const string DistanceFromScaleExpr = "DistanceFromScaleExpr";

			public const string StartWidthExpr = "StartWidthExpr";

			public const string EndWidthExpr = "EndWidthExpr";

			public const string InRangeBarPointerColorExpr = "InRangeBarPointerColorExpr";

			public const string InRangeLabelColorExpr = "InRangeLabelColorExpr";

			public const string InRangeTickMarksColorExpr = "InRangeTickMarksColorExpr";

			public const string BackgroundGradientTypeExpr = "BackgroundGradientTypeExpr";

			public const string PlacementExpr = "PlacementExpr";

			public const string StateIndicatorExprHost = "StateIndicatorExprHost";

			public const string ThermometerExprHost = "ThermometerExprHost";

			public const string BulbOffsetExpr = "BulbOffsetExpr";

			public const string BulbSizeExpr = "BulbSizeExpr";

			public const string ThermometerStyleExpr = "ThermometerStyleExpr";

			public const string TickMarkStyleExprHost = "TickMarkStyleExprHost";

			public const string EnableGradientExpr = "EnableGradientExpr";

			public const string GradientDensityExpr = "GradientDensityExpr";

			public const string GaugeMajorTickMarksHost = "GaugeMajorTickMarksHost";

			public const string GaugeMinorTickMarksHost = "GaugeMinorTickMarksHost";

			public const string GaugeMaximumPinHost = "MaximumPinHost";

			public const string GaugeMinimumPinHost = "MinimumPinHost";

			public const string GaugeInputValueHost = "GaugeInputValueHost";

			public const string WidthExpr = "WidthExpr";

			public const string ZIndexExpr = "ZIndexExpr";

			public const string PositionExpr = "PositionExpr";

			public const string ShapeExpr = "ShapeExpr";

			public const string ScaleLabelsHost = "ScaleLabelsHost";

			public const string ScalePinHost = "ScalePinHost";

			public const string PinLabelHost = "PinLabelHost";

			public const string PointerCapHost = "PointerCapHost";

			public const string ThermometerHost = "ThermometerHost";

			public const string TickMarkStyleHost = "TickMarkStyleHost";

			public const string ResizeModeExpr = "ResizeModeExpr";

			public const string TextShadowOffsetExpr = "TextShadowOffsetExpr";

			public const string CustomLabelsHosts = "m_customLabelsHostsRemotable";

			public const string GaugeImagesHosts = "m_gaugeImagesHostsRemotable";

			public const string GaugeLabelsHosts = "m_gaugeLabelsHostsRemotable";

			public const string LinearGaugesHosts = "m_linearGaugesHostsRemotable";

			public const string RadialGaugesHosts = "m_radialGaugesHostsRemotable";

			public const string LinearPointersHosts = "m_linearPointersHostsRemotable";

			public const string RadialPointersHosts = "m_radialPointersHostsRemotable";

			public const string LinearScalesHosts = "m_linearScalesHostsRemotable";

			public const string RadialScalesHosts = "m_radialScalesHostsRemotable";

			public const string ScaleRangesHosts = "m_scaleRangesHostsRemotable";

			public const string NumericIndicatorsHosts = "m_numericIndicatorsHostsRemotable";

			public const string StateIndicatorsHosts = "m_stateIndicatorsHostsRemotable";

			public const string GaugeInputValuesHosts = "m_gaugeInputValueHostsRemotable";

			public const string NumericIndicatorRangesHosts = "m_numericIndicatorRangesHostsRemotable";

			public const string IndicatorStatesHosts = "m_indicatorStatesHostsRemotable";

			public const string NumericIndicatorHost = "NumericIndicatorHost";

			public const string DecimalDigitColorExpr = "DecimalDigitColorExpr";

			public const string DigitColorExpr = "DigitColorExpr";

			public const string DecimalDigitsExpr = "DecimalDigitsExpr";

			public const string DigitsExpr = "DigitsExpr";

			public const string NonNumericStringExpr = "NonNumericStringExpr";

			public const string OutOfRangeStringExpr = "OutOfRangeStringExpr";

			public const string ShowDecimalPointExpr = "ShowDecimalPointExpr";

			public const string ShowLeadingZerosExpr = "ShowLeadingZerosExpr";

			public const string IndicatorStyleExpr = "IndicatorStyleExpr";

			public const string ShowSignExpr = "ShowSignExpr";

			public const string LedDimColorExpr = "LedDimColorExpr";

			public const string SeparatorWidthExpr = "SeparatorWidthExpr";

			public const string NumericIndicatorRangeExprHost = "NumericIndicatorRangeExprHost";

			public const string NumericIndicatorRangeHost = "NumericIndicatorRangeHost";

			public const string StateIndicatorHost = "StateIndicatorHost";

			public const string IndicatorStateExprHost = "IndicatorStateExprHost";

			public const string IndicatorStateHost = "IndicatorStateHost";

			public const string TransformationTypeExpr = "TransformationTypeExpr";

			public const string MapViewExprHost = "MapViewExprHost";

			public const string MapViewHost = "MapViewHost";

			public const string ZoomExpr = "ZoomExpr";

			public const string MapElementViewExprHost = "MapElementViewExprHost";

			public const string MapElementViewHost = "MapElementViewHost";

			public const string LayerNameExpr = "LayerNameExpr";

			public const string MapDataBoundViewExprHost = "MapDataBoundViewExprHost";

			public const string MapDataBoundViewHost = "MapDataBoundViewHost";

			public const string MapCustomViewExprHost = "MapCustomViewExprHost";

			public const string MapCustomViewHost = "MapCustomViewHost";

			public const string CenterXExpr = "CenterXExpr";

			public const string CenterYExpr = "CenterYExpr";

			public const string MapBorderSkinExprHost = "MapBorderSkinExprHost";

			public const string MapBorderSkinHost = "MapBorderSkinHost";

			public const string MapBorderSkinTypeExpr = "MapBorderSkinTypeExpr";

			public const string MapDataRegionNameExpr = "MapDataRegionNameExpr";

			public const string MapTileLayerExprHost = "MapTileLayerExprHost";

			public const string MapTileLayerHost = "MapTileLayerHost";

			public const string ServiceUrlExpr = "ServiceUrlExpr";

			public const string TileStyleExpr = "TileStyleExpr";

			public const string MapTileExprHost = "MapTileExprHost";

			public const string MapTileHost = "MapTileHost";

			public const string UseSecureConnectionExpr = "UseSecureConnectionExpr";

			public const string MapPolygonLayerExprHost = "MapPolygonLayerExprHost";

			public const string MapPointLayerExprHost = "MapPointLayerExprHost";

			public const string MapLineLayerExprHost = "MapLineLayerExprHost";

			public const string MapSpatialDataSetExprHost = "MapSpatialDataSetExprHost";

			public const string DataSetNameExpr = "DataSetNameExpr";

			public const string SpatialFieldExpr = "SpatialFieldExpr";

			public const string MapSpatialDataRegionExprHost = "MapSpatialDataRegionExprHost";

			public const string VectorDataExpr = "VectorDataExpr";

			public const string MapSpatialDataExprHost = "MapSpatialDataExprHost";

			public const string MapSpatialDataHost = "MapSpatialDataHost";

			public const string SimplificationResolutionExpr = "SimplificationResolutionExpr";

			public const string MapShapefileExprHost = "MapShapefileExprHost";

			public const string MapLayerExprHost = "MapLayerExprHost";

			public const string MapLayerHost = "MapLayerHost";

			public const string VisibilityModeExpr = "VisibilityModeExpr";

			public const string MapFieldNameExprHost = "MapFieldNameExprHost";

			public const string MapFieldNameHost = "MapFieldNameHost";

			public const string NameExpr = "NameExpr";

			public const string MapFieldDefinitionExprHost = "MapFieldDefinitionExprHost";

			public const string MapFieldDefinitionHost = "MapFieldDefinitionHost";

			public const string MapPointExprHost = "MapPointExprHost";

			public const string MapPointHost = "MapPointHost";

			public const string MapSpatialElementExprHost = "MapSpatialElementExprHost";

			public const string MapSpatialElementHost = "MapSpatialElementHost";

			public const string MapPolygonExprHost = "MapPolygonExprHost";

			public const string MapPolygonHost = "MapPolygonHost";

			public const string UseCustomPolygonTemplateExpr = "UseCustomPolygonTemplateExpr";

			public const string UseCustomPointTemplateExpr = "UseCustomPointTemplateExpr";

			public const string MapLineExprHost = "MapLineExprHost";

			public const string MapLineHost = "MapLineHost";

			public const string UseCustomLineTemplateExpr = "UseCustomLineTemplateExpr";

			public const string MapFieldExprHost = "MapFieldExprHost";

			public const string MapFieldHost = "MapFieldHost";

			public const string MapPointTemplateExprHost = "MapPointTemplateExprHost";

			public const string MapPointTemplateHost = "MapPointTemplateHost";

			public const string MapMarkerTemplateExprHost = "MapMarkerTemplateExprHost";

			public const string MapMarkerTemplateHost = "MapMarkerTemplateHost";

			public const string MapPolygonTemplateExprHost = "MapPolygonTemplateExprHost";

			public const string MapPolygonTemplateHost = "MapPolygonTemplateHost";

			public const string ScaleFactorExpr = "ScaleFactorExpr";

			public const string CenterPointOffsetXExpr = "CenterPointOffsetXExpr";

			public const string CenterPointOffsetYExpr = "CenterPointOffsetYExpr";

			public const string ShowLabelExpr = "ShowLabelExpr";

			public const string MapLineTemplateExprHost = "MapLineTemplateExprHost";

			public const string MapLineTemplateHost = "MapLineTemplateHost";

			public const string MapCustomColorRuleExprHost = "MapCustomColorRuleExprHost";

			public const string MapCustomColorExprHost = "MapCustomColorExprHost";

			public const string MapCustomColorHost = "MapCustomColorHost";

			public const string MapPointRulesExprHost = "MapPointRulesExprHost";

			public const string MapPointRulesHost = "MapPointRulesHost";

			public const string MapMarkerRuleExprHost = "MapMarkerRuleExprHost";

			public const string MapMarkerRuleHost = "MapMarkerRuleHost";

			public const string MapMarkerExprHost = "MapMarkerExprHost";

			public const string MapMarkerHost = "MapMarkerHost";

			public const string MapMarkerStyleExpr = "MapMarkerStyleExpr";

			public const string MapMarkerImageExprHost = "MapMarkerImageExprHost";

			public const string MapMarkerImageHost = "MapMarkerImageHost";

			public const string MapSizeRuleExprHost = "MapSizeRuleExprHost";

			public const string MapSizeRuleHost = "MapSizeRuleHost";

			public const string StartSizeExpr = "StartSizeExpr";

			public const string EndSizeExpr = "EndSizeExpr";

			public const string MapPolygonRulesExprHost = "MapPolygonRulesExprHost";

			public const string MapPolygonRulesHost = "MapPolygonRulesHost";

			public const string MapLineRulesExprHost = "MapLineRulesExprHost";

			public const string MapLineRulesHost = "MapLineRulesHost";

			public const string MapColorRuleExprHost = "MapColorRuleExprHost";

			public const string MapColorRuleHost = "MapColorRuleHost";

			public const string ShowInColorScaleExpr = "ShowInColorScaleExpr";

			public const string MapColorRangeRuleExprHost = "MapColorRangeRuleExprHost";

			public const string StartColorExpr = "StartColorExpr";

			public const string MiddleColorExpr = "MiddleColorExpr";

			public const string EndColorExpr = "EndColorExpr";

			public const string MapColorPaletteRuleExprHost = "MapColorPaletteRuleExprHost";

			public const string MapBucketExprHost = "MapBucketExprHost";

			public const string MapBucketHost = "MapBucketHost";

			public const string MapAppearanceRuleExprHost = "MapAppearanceRuleExprHost";

			public const string MapAppearanceRuleHost = "MapAppearanceRuleHost";

			public const string DataValueExpr = "DataValueExpr";

			public const string DistributionTypeExpr = "DistributionTypeExpr";

			public const string BucketCountExpr = "BucketCountExpr";

			public const string StartValueExpr = "StartValueExpr";

			public const string EndValueExpr = "EndValueExpr";

			public const string MapLegendTitleExprHost = "MapLegendTitleExprHost";

			public const string MapLegendTitleHost = "MapLegendTitleHost";

			public const string TitleSeparatorColorExpr = "TitleSeparatorColorExpr";

			public const string MapLegendExprHost = "MapLegendExprHost";

			public const string MapLegendHost = "MapLegendHost";

			public const string MapLocationExprHost = "MapLocationExprHost";

			public const string MapLocationHost = "MapLocationHost";

			public const string MapSizeExprHost = "MapSizeExprHost";

			public const string MapSizeHost = "MapSizeHost";

			public const string UnitExpr = "UnitExpr";

			public const string MapGridLinesExprHost = "MapGridLinesExprHost";

			public const string MapMeridiansHost = "MapMeridiansHost";

			public const string MapParallelsHost = "MapParallelsHost";

			public const string ShowLabelsExpr = "ShowLabelsExpr";

			public const string LabelPositionExpr = "LabelPositionExpr";

			public const string MapHosts = "m_mapHostsRemotable";

			public const string MapDataRegionHosts = "m_mapDataRegionHostsRemotable";

			public const string MapDockableSubItemExprHost = "MapDockableSubItemExprHost";

			public const string MapDockableSubItemHost = "MapDockableSubItemHost";

			public const string DockOutsideViewportExpr = "DockOutsideViewportExpr";

			public const string MapSubItemExprHost = "MapSubItemExprHost";

			public const string MapSubItemHost = "MapSubItemHost";

			public const string MapBindingFieldPairExprHost = "MapBindingFieldPairExprHost";

			public const string MapBindingFieldPairHost = "MapBindingFieldPairHost";

			public const string FieldNameExpr = "FieldNameExpr";

			public const string BindingExpressionExpr = "BindingExpressionExpr";

			public const string ZoomEnabledExpr = "ZoomEnabledExpr";

			public const string MapViewportExprHost = "MapViewportExprHost";

			public const string MapViewportHost = "MapViewportHost";

			public const string MapCoordinateSystemExpr = "MapCoordinateSystemExpr";

			public const string MapProjectionExpr = "MapProjectionExpr";

			public const string ProjectionCenterXExpr = "ProjectionCenterXExpr";

			public const string ProjectionCenterYExpr = "ProjectionCenterYExpr";

			public const string MaximumZoomExpr = "MaximumZoomExpr";

			public const string MinimumZoomExpr = "MinimumZoomExpr";

			public const string ContentMarginExpr = "ContentMarginExpr";

			public const string GridUnderContentExpr = "GridUnderContentExpr";

			public const string MapBindingFieldPairsHosts = "m_mapBindingFieldPairsHostsRemotable";

			public const string MapLimitsExprHost = "MapLimitsExprHost";

			public const string MapLimitsHost = "MapLimitsHost";

			public const string MinimumXExpr = "MinimumXExpr";

			public const string MinimumYExpr = "MinimumYExpr";

			public const string MaximumXExpr = "MaximumXExpr";

			public const string MaximumYExpr = "MaximumYExpr";

			public const string LimitToDataExpr = "LimitToDataExpr";

			public const string MapColorScaleExprHost = "MapColorScaleExprHost";

			public const string MapColorScaleHost = "MapColorScaleHost";

			public const string TickMarkLengthExpr = "TickMarkLengthExpr";

			public const string ColorBarBorderColorExpr = "ColorBarBorderColorExpr";

			public const string LabelFormatExpr = "LabelFormatExpr";

			public const string LabelPlacementExpr = "LabelPlacementExpr";

			public const string LabelBehaviorExpr = "LabelBehaviorExpr";

			public const string RangeGapColorExpr = "RangeGapColorExpr";

			public const string NoDataTextExpr = "NoDataTextExpr";

			public const string MapColorScaleTitleExprHost = "MapColorScaleTitleExprHost";

			public const string MapColorScaleTitleHost = "MapColorScaleTitleHost";

			public const string MapDistanceScaleExprHost = "MapDistanceScaleExprHost";

			public const string MapDistanceScaleHost = "MapDistanceScaleHost";

			public const string ScaleColorExpr = "ScaleColorExpr";

			public const string ScaleBorderColorExpr = "ScaleBorderColorExpr";

			public const string MapTitleExprHost = "MapTitleExprHost";

			public const string MapTitleHost = "MapTitleHost";

			public const string MapLegendsHosts = "m_mapLegendsHostsRemotable";

			public const string MapTitlesHosts = "m_mapTitlesHostsRemotable";

			public const string MapMarkersHosts = "m_mapMarkersHostsRemotable";

			public const string MapBucketsHosts = "m_mapBucketsHostsRemotable";

			public const string MapCustomColorsHosts = "m_mapCustomColorsHostsRemotable";

			public const string MapPointsHosts = "m_mapPointsHostsRemotable";

			public const string MapPolygonsHosts = "m_mapPolygonsHostsRemotable";

			public const string MapLinesHosts = "m_mapLinesHostsRemotable";

			public const string MapTileLayersHosts = "m_mapTileLayersHostsRemotable";

			public const string MapTilesHosts = "m_mapTilesHostsRemotable";

			public const string MapPointLayersHosts = "m_mapPointLayersHostsRemotable";

			public const string MapPolygonLayersHosts = "m_mapPolygonLayersHostsRemotable";

			public const string MapLineLayersHosts = "m_mapLineLayersHostsRemotable";

			public const string MapFieldNamesHosts = "m_mapFieldNamesHostsRemotable";

			public const string MapExprHost = "MapExprHost";

			public const string DataElementLabelExpr = "DataElementLabelExpr";

			public const string ParagraphExprHost = "ParagraphExprHost";

			public const string ParagraphHosts = "m_paragraphHostsRemotable";

			public const string LeftIndentExpr = "LeftIndentExpr";

			public const string RightIndentExpr = "RightIndentExpr";

			public const string HangingIndentExpr = "HangingIndentExpr";

			public const string SpaceBeforeExpr = "SpaceBeforeExpr";

			public const string SpaceAfterExpr = "SpaceAfterExpr";

			public const string ListStyleExpr = "ListStyleExpr";

			public const string ListLevelExpr = "ListLevelExpr";

			public const string TextRunExprHost = "TextRunExprHost";

			public const string TextRunHosts = "m_textRunHostsRemotable";

			public const string MarkupTypeExpr = "MarkupTypeExpr";

			public const string LookupSourceExpr = "SourceExpr";

			public const string LookupDestExpr = "DestExpr";

			public const string LookupResultExpr = "ResultExpr";

			public const string PageBreakExprHost = "PageBreakExprHost";

			public const string PageBreakDisabledExpr = "DisabledExpr";

			public const string PageBreakPageNameExpr = "PageNameExpr";

			public const string PageBreakResetPageNumberExpr = "ResetPageNumberExpr";

			public const string JoinConditionForeignKeyExpr = "ForeignKeyExpr";

			public const string JoinConditionPrimaryKeyExpr = "PrimaryKeyExpr";
		}

		private abstract class TypeDecl
		{
			public CodeTypeDeclaration Type;

			public string BaseTypeName;

			public TypeDecl Parent;

			public CodeConstructor Constructor;

			public bool HasExpressions;

			public CodeExpressionCollection DataValues;

			protected readonly bool m_setCode;

			public void NestedTypeAdd(string name, CodeTypeDeclaration nestedType)
			{
				this.ConstructorCreate();
				this.Type.Members.Add(nestedType);
				this.Constructor.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), name), this.CreateTypeCreateExpression(nestedType.Name)));
			}

			public int NestedTypeColAdd(string name, string baseTypeName, ref CodeExpressionCollection initializers, CodeTypeDeclaration nestedType)
			{
				this.Type.Members.Add(nestedType);
				this.TypeColInit(name, baseTypeName, ref initializers);
				return initializers.Add(this.CreateTypeCreateExpression(nestedType.Name));
			}

			protected TypeDecl(string typeName, string baseTypeName, TypeDecl parent, bool setCode)
			{
				this.BaseTypeName = baseTypeName;
				this.Parent = parent;
				this.m_setCode = setCode;
				this.Type = this.CreateType(typeName, baseTypeName);
			}

			protected void ConstructorCreate()
			{
				if (this.Constructor == null)
				{
					this.Constructor = this.CreateConstructor();
					this.Type.Members.Add(this.Constructor);
				}
			}

			protected virtual CodeConstructor CreateConstructor()
			{
				CodeConstructor codeConstructor = new CodeConstructor();
				codeConstructor.Attributes = MemberAttributes.Public;
				return codeConstructor;
			}

			protected CodeAssignStatement CreateTypeColInitStatement(string name, string baseTypeName, ref CodeExpressionCollection initializers)
			{
				CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression();
				codeObjectCreateExpression.CreateType = new CodeTypeReference((name == "m_memberTreeHostsRemotable") ? "RemoteMemberArrayWrapper" : "RemoteArrayWrapper", new CodeTypeReference(baseTypeName));
				if (initializers != null)
				{
					codeObjectCreateExpression.Parameters.AddRange(initializers);
				}
				initializers = codeObjectCreateExpression.Parameters;
				return new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), name), codeObjectCreateExpression);
			}

			protected virtual CodeTypeDeclaration CreateType(string name, string baseType)
			{
				Global.Tracer.Assert(name != null, "(name != null)");
				CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(name);
				if (baseType != null)
				{
					codeTypeDeclaration.BaseTypes.Add(new CodeTypeReference(baseType));
				}
				codeTypeDeclaration.Attributes = (MemberAttributes)24578;
				return codeTypeDeclaration;
			}

			private void TypeColInit(string name, string baseTypeName, ref CodeExpressionCollection initializers)
			{
				this.ConstructorCreate();
				if (initializers == null)
				{
					this.Constructor.Statements.Add(this.CreateTypeColInitStatement(name, baseTypeName, ref initializers));
				}
			}

			private CodeObjectCreateExpression CreateTypeCreateExpression(string typeName)
			{
				if (this.m_setCode)
				{
					return new CodeObjectCreateExpression(typeName, new CodeArgumentReferenceExpression("Code"));
				}
				return new CodeObjectCreateExpression(typeName);
			}
		}

		private sealed class RootTypeDecl : TypeDecl
		{
			public CodeExpressionCollection Aggregates;

			public CodeExpressionCollection PageSections;

			public CodeExpressionCollection ReportParameters;

			public CodeExpressionCollection DataSources;

			public CodeExpressionCollection DataSets;

			public CodeExpressionCollection Lines;

			public CodeExpressionCollection Rectangles;

			public CodeExpressionCollection TextBoxes;

			public CodeExpressionCollection Images;

			public CodeExpressionCollection Subreports;

			public CodeExpressionCollection Tablices;

			public CodeExpressionCollection DataShapes;

			public CodeExpressionCollection Charts;

			public CodeExpressionCollection GaugePanels;

			public CodeExpressionCollection CustomReportItems;

			public CodeExpressionCollection Lookups;

			public CodeExpressionCollection LookupDests;

			public CodeExpressionCollection Pages;

			public CodeExpressionCollection ReportSections;

			public CodeExpressionCollection Maps;

			public RootTypeDecl(bool setCode)
				: base("ReportExprHostImpl", "ReportExprHost", null, setCode)
			{
			}

			protected override CodeConstructor CreateConstructor()
			{
				CodeConstructor codeConstructor = base.CreateConstructor();
				codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(bool), "includeParameters"));
				codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(bool), "parametersOnly"));
				CodeParameterDeclarationExpression value = new CodeParameterDeclarationExpression(typeof(object), "reportObjectModel");
				codeConstructor.Parameters.Add(value);
				codeConstructor.BaseConstructorArgs.Add(new CodeArgumentReferenceExpression("reportObjectModel"));
				this.ReportParameters = new CodeExpressionCollection();
				this.DataSources = new CodeExpressionCollection();
				this.DataSets = new CodeExpressionCollection();
				return codeConstructor;
			}

			protected override CodeTypeDeclaration CreateType(string name, string baseType)
			{
				CodeTypeDeclaration codeTypeDeclaration = base.CreateType(name, baseType);
				if (base.m_setCode)
				{
					CodeMemberField codeMemberField = new CodeMemberField("CustomCodeProxy", "Code");
					codeMemberField.Attributes = (MemberAttributes)20482;
					codeTypeDeclaration.Members.Add(codeMemberField);
				}
				return codeTypeDeclaration;
			}

			public void CompleteConstructorCreation()
			{
				if (base.HasExpressions)
				{
					if (base.Constructor == null)
					{
						base.ConstructorCreate();
					}
					else
					{
						CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
						codeConditionStatement.Condition = new CodeBinaryOperatorExpression(new CodeArgumentReferenceExpression("parametersOnly"), CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(true));
						codeConditionStatement.TrueStatements.Add(new CodeMethodReturnStatement());
						base.Constructor.Statements.Insert(0, codeConditionStatement);
						if (this.ReportParameters.Count > 0)
						{
							CodeConditionStatement codeConditionStatement2 = new CodeConditionStatement();
							codeConditionStatement2.Condition = new CodeBinaryOperatorExpression(new CodeArgumentReferenceExpression("includeParameters"), CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(true));
							codeConditionStatement2.TrueStatements.Add(base.CreateTypeColInitStatement("m_reportParameterHostsRemotable", "ReportParamExprHost", ref this.ReportParameters));
							base.Constructor.Statements.Insert(0, codeConditionStatement2);
						}
						if (this.DataSources.Count > 0)
						{
							base.Constructor.Statements.Insert(0, base.CreateTypeColInitStatement("m_dataSourceHostsRemotable", "DataSourceExprHost", ref this.DataSources));
						}
						if (this.DataSets.Count > 0)
						{
							base.Constructor.Statements.Insert(0, base.CreateTypeColInitStatement("m_dataSetHostsRemotable", "DataSetExprHost", ref this.DataSets));
						}
					}
					Global.Tracer.Assert(null != base.Constructor, "Invalid EH constructor");
					this.CreateCustomCodeInitialization();
				}
			}

			private void CreateCustomCodeInitialization()
			{
				if (base.m_setCode)
				{
					base.Constructor.Statements.Insert(0, new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "m_codeProxyBase"), new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Code")));
					base.Constructor.Statements.Insert(0, new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Code"), new CodeObjectCreateExpression("CustomCodeProxy", new CodeThisReferenceExpression())));
				}
			}
		}

		private sealed class NonRootTypeDecl : TypeDecl
		{
			public CodeExpressionCollection Parameters;

			public CodeExpressionCollection Filters;

			public CodeExpressionCollection Actions;

			public CodeExpressionCollection Fields;

			public CodeExpressionCollection ValueAxes;

			public CodeExpressionCollection CategoryAxes;

			public CodeExpressionCollection ChartTitles;

			public CodeExpressionCollection ChartLegends;

			public CodeExpressionCollection ChartAreas;

			public CodeExpressionCollection TablixMembers;

			public CodeExpressionCollection DataShapeMembers;

			public CodeExpressionCollection ChartMembers;

			public CodeExpressionCollection GaugeMembers;

			public CodeExpressionCollection DataGroups;

			public CodeExpressionCollection TablixCells;

			public CodeExpressionCollection DataShapeIntersections;

			public CodeExpressionCollection DataPoints;

			public CodeExpressionCollection DataCells;

			public CodeExpressionCollection ChartLegendCustomItemCells;

			public CodeExpressionCollection ChartCustomPaletteColors;

			public CodeExpressionCollection ChartStripLines;

			public CodeExpressionCollection ChartSeriesCollection;

			public CodeExpressionCollection ChartDerivedSeriesCollection;

			public CodeExpressionCollection ChartFormulaParameters;

			public CodeExpressionCollection ChartLegendColumns;

			public CodeExpressionCollection ChartLegendCustomItems;

			public CodeExpressionCollection Paragraphs;

			public CodeExpressionCollection TextRuns;

			public CodeExpressionCollection GaugeCells;

			public CodeExpressionCollection CustomLabels;

			public CodeExpressionCollection GaugeImages;

			public CodeExpressionCollection GaugeLabels;

			public CodeExpressionCollection LinearGauges;

			public CodeExpressionCollection RadialGauges;

			public CodeExpressionCollection RadialPointers;

			public CodeExpressionCollection LinearPointers;

			public CodeExpressionCollection LinearScales;

			public CodeExpressionCollection RadialScales;

			public CodeExpressionCollection ScaleRanges;

			public CodeExpressionCollection NumericIndicators;

			public CodeExpressionCollection StateIndicators;

			public CodeExpressionCollection GaugeInputValues;

			public CodeExpressionCollection NumericIndicatorRanges;

			public CodeExpressionCollection IndicatorStates;

			public CodeExpressionCollection MapMembers;

			public CodeExpressionCollection MapBindingFieldPairs;

			public CodeExpressionCollection MapLegends;

			public CodeExpressionCollection MapTitles;

			public CodeExpressionCollection MapMarkers;

			public CodeExpressionCollection MapBuckets;

			public CodeExpressionCollection MapCustomColors;

			public CodeExpressionCollection MapPoints;

			public CodeExpressionCollection MapPolygons;

			public CodeExpressionCollection MapLines;

			public CodeExpressionCollection MapTileLayers;

			public CodeExpressionCollection MapTiles;

			public CodeExpressionCollection MapPointLayers;

			public CodeExpressionCollection MapPolygonLayers;

			public CodeExpressionCollection MapLineLayers;

			public CodeExpressionCollection MapFieldNames;

			public CodeExpressionCollection JoinConditions;

			public ReturnStatementList IndexedExpressions;

			public NonRootTypeDecl(string typeName, string baseTypeName, TypeDecl parent, bool setCode)
				: base(typeName, baseTypeName, parent, setCode)
			{
				if (setCode)
				{
					base.ConstructorCreate();
				}
			}

			protected override CodeConstructor CreateConstructor()
			{
				CodeConstructor codeConstructor = base.CreateConstructor();
				if (base.m_setCode)
				{
					codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression("CustomCodeProxy", "code"));
					codeConstructor.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Code"), new CodeArgumentReferenceExpression("code")));
				}
				return codeConstructor;
			}

			protected override CodeTypeDeclaration CreateType(string name, string baseType)
			{
				CodeTypeDeclaration codeTypeDeclaration = base.CreateType(string.Format(CultureInfo.InvariantCulture, "{0}_{1}", name, baseType), baseType);
				if (base.m_setCode)
				{
					CodeMemberField codeMemberField = new CodeMemberField("CustomCodeProxy", "Code");
					codeMemberField.Attributes = (MemberAttributes)20482;
					codeTypeDeclaration.Members.Add(codeMemberField);
				}
				return codeTypeDeclaration;
			}
		}

		private sealed class CustomCodeProxyDecl : TypeDecl
		{
			public CustomCodeProxyDecl(TypeDecl parent)
				: base("CustomCodeProxy", "CustomCodeProxyBase", parent, false)
			{
				base.ConstructorCreate();
			}

			protected override CodeConstructor CreateConstructor()
			{
				CodeConstructor codeConstructor = base.CreateConstructor();
				codeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(IReportObjectModelProxyForCustomCode), "reportObjectModel"));
				codeConstructor.BaseConstructorArgs.Add(new CodeArgumentReferenceExpression("reportObjectModel"));
				return codeConstructor;
			}

			public void AddClassInstance(string className, string instanceName, int id)
			{
				string fileName = "CMCID" + id.ToString(CultureInfo.InvariantCulture) + "end";
				CodeMemberField codeMemberField = new CodeMemberField(className, "m_" + instanceName);
				codeMemberField.Attributes = (MemberAttributes)20482;
				codeMemberField.InitExpression = new CodeObjectCreateExpression(className);
				codeMemberField.LinePragma = new CodeLinePragma(fileName, 0);
				base.Type.Members.Add(codeMemberField);
				CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
				codeMemberProperty.Type = new CodeTypeReference(className);
				codeMemberProperty.Name = instanceName;
				codeMemberProperty.Attributes = (MemberAttributes)24578;
				codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), codeMemberField.Name)));
				codeMemberProperty.LinePragma = new CodeLinePragma(fileName, 2);
				base.Type.Members.Add(codeMemberProperty);
			}

			public void AddCode(string code)
			{
				CodeTypeMember codeTypeMember = new CodeSnippetTypeMember(code);
				codeTypeMember.LinePragma = new CodeLinePragma("CustomCode", 0);
				base.Type.Members.Add(codeTypeMember);
			}
		}

		private sealed class ReturnStatementList
		{
			private ArrayList m_list = new ArrayList();

			public CodeMethodReturnStatement this[int index]
			{
				get
				{
					return (CodeMethodReturnStatement)this.m_list[index];
				}
			}

			public int Count
			{
				get
				{
					return this.m_list.Count;
				}
			}

			public int Add(CodeMethodReturnStatement retStatement)
			{
                return this.m_list.Add(retStatement);
			}
		}

		public const string RootType = "ReportExprHostImpl";

		public const int InvalidExprHostId = -1;

		private const string EndSrcMarker = "end";

		private const string ExprSrcMarker = "Expr";

		private const string CustomCodeSrcMarker = "CustomCode";

		private const string CodeModuleClassInstanceDeclSrcMarker = "CMCID";

		private RootTypeDecl m_rootTypeDecl;

		private TypeDecl m_currentTypeDecl;

		private bool m_setCode;

		private static readonly Regex m_findExprNumber = new Regex("^Expr([0-9]+)end", RegexOptions.Compiled);

		private static readonly Regex m_findCodeModuleClassInstanceDeclNumber = new Regex("^CMCID([0-9]+)end", RegexOptions.Compiled);

		public bool HasExpressions
		{
			get
			{
				if (this.m_rootTypeDecl != null)
				{
					return this.m_rootTypeDecl.HasExpressions;
				}
				return false;
			}
		}

		public bool CustomCode
		{
			get
			{
				return this.m_setCode;
			}
		}

		public ExprHostBuilder()
		{
		}

		public void SetCustomCode()
		{
			this.m_setCode = true;
		}

		public CodeCompileUnit GetExprHost(ProcessingIntermediateFormatVersion version, bool refusePermissions)
		{
			Global.Tracer.Assert(this.m_rootTypeDecl != null && this.m_currentTypeDecl.Parent == null, "(m_rootTypeDecl != null && m_currentTypeDecl.Parent == null)");
			CodeCompileUnit codeCompileUnit = null;
			if (this.HasExpressions)
			{
				codeCompileUnit = new CodeCompileUnit();
                //codeCompileUnit.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Runtime.CompilerServices.InternalsVisibleTo", new CodeAttributeArgument(new CodePrimitiveExpression(version.ToString()))));
				if (refusePermissions)
				{
                    //codeCompileUnit.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Security.Permissions.SecurityPermission", new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SecurityAction)), "RequestMinimum")), new CodeAttributeArgument("Execution", new CodePrimitiveExpression(true))));
                    //codeCompileUnit.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration("System.Security.Permissions.SecurityPermission", new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SecurityAction)), "RequestOptional")), new CodeAttributeArgument("Execution", new CodePrimitiveExpression(true))));
                }
				CodeNamespace codeNamespace = new CodeNamespace("ExpressionHost");
				codeCompileUnit.Namespaces.Add(codeNamespace);               
                codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
				codeNamespace.Imports.Add(new CodeNamespaceImport("System.Convert"));
				codeNamespace.Imports.Add(new CodeNamespaceImport("System.Math"));
                codeNamespace.Imports.Add(new CodeNamespaceImport("System.Runtime.CompilerServices"));
                codeNamespace.Imports.Add(new CodeNamespaceImport("Microsoft.VisualBasic"));
				//codeNamespace.Imports.Add(new CodeNamespaceImport("Microsoft.SqlServer.Types"));
				codeNamespace.Imports.Add(new CodeNamespaceImport("AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel"));
				codeNamespace.Imports.Add(new CodeNamespaceImport("AspNetCore.ReportingServices.RdlExpressions.ExpressionHostObjectModel"));
				codeNamespace.Types.Add(this.m_rootTypeDecl.Type);
			}
            this.m_rootTypeDecl = null;
			return codeCompileUnit;
		}

		public ErrorSource ParseErrorSource(CompilerError error, out int id)
		{
			Global.Tracer.Assert(error.FileName != null, "(error.FileName != null)");
			id = -1;
			if (error.FileName.StartsWith("CustomCode", StringComparison.Ordinal))
			{
				return ErrorSource.CustomCode;
			}
			try
			{
				Match match = ExprHostBuilder.m_findCodeModuleClassInstanceDeclNumber.Match(error.FileName);
				if (match.Success)
				{
					id = int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
					return ErrorSource.CodeModuleClassInstanceDecl;
				}
				match = ExprHostBuilder.m_findExprNumber.Match(error.FileName);
				if (match.Success)
				{
					id = int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
					return ErrorSource.Expression;
				}
			}
			catch (FormatException)
			{
			}
			return ErrorSource.Unknown;
		}

		public void SharedDataSetStart()
		{
			this.m_currentTypeDecl = (this.m_rootTypeDecl = new RootTypeDecl(this.m_setCode));
		}

		public void SharedDataSetEnd()
		{
			this.m_rootTypeDecl.CompleteConstructorCreation();
		}

		public void ReportStart()
		{
			this.m_currentTypeDecl = (this.m_rootTypeDecl = new RootTypeDecl(this.m_setCode));
		}

		public void ReportEnd()
		{
			this.m_rootTypeDecl.CompleteConstructorCreation();
		}

		public void ReportLanguage(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ReportLanguageExpr", expression);
		}

		public void ReportAutoRefresh(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AutoRefreshExpr", expression);
		}

		public void ReportInitialPageName(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InitialPageNameExpr", expression);
		}

		public void GenericLabel(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelExpr", expression);
		}

		public void GenericValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		public void GenericNoRows(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("NoRowsExpr", expression);
		}

		public void GenericVisibilityHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("VisibilityHiddenExpr", expression);
		}

		public void AggregateParamExprAdd(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.AggregateStart();
			this.GenericValue(expression);
			expression.ExprHostID = this.AggregateEnd();
		}

		public void CustomCodeProxyStart()
		{
			Global.Tracer.Assert(this.m_setCode, "(m_setCode)");
			this.m_currentTypeDecl = new CustomCodeProxyDecl(this.m_currentTypeDecl);
			this.m_currentTypeDecl.HasExpressions = true;
		}

		public void CustomCodeProxyEnd()
		{
			this.m_rootTypeDecl.Type.Members.Add(this.m_currentTypeDecl.Type);
			this.TypeEnd(this.m_rootTypeDecl);
		}

		public void CustomCodeClassInstance(string className, string instanceName, int id)
		{
			((CustomCodeProxyDecl)this.m_currentTypeDecl).AddClassInstance(className, instanceName, id);
		}

		public void ReportCode(string code)
		{
			((CustomCodeProxyDecl)this.m_currentTypeDecl).AddCode(code);
		}

		public void ReportParameterStart(string name)
		{
			this.TypeStart(name, "ReportParamExprHost");
		}

		public int ReportParameterEnd()
		{
			this.ExprIndexerCreate();
			return this.TypeEnd((TypeDecl)this.m_rootTypeDecl, "m_reportParameterHostsRemotable", ref this.m_rootTypeDecl.ReportParameters);
		}

		public void ReportParameterValidationExpression(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValidationExpressionExpr", expression);
		}

		public void ReportParameterPromptExpression(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PromptExpr", expression);
		}

		public void ReportParameterDefaultValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void ReportParameterValidValuesStart()
		{
			this.TypeStart("ReportParameterValidValues", "IndexedExprHost");
		}

		public void ReportParameterValidValuesEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ValidValuesHost");
		}

		public void ReportParameterValidValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void ReportParameterValidValueLabelsStart()
		{
			this.TypeStart("ReportParameterValidValueLabels", "IndexedExprHost");
		}

		public void ReportParameterValidValueLabelsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ValidValueLabelsHost");
		}

		public void ReportParameterValidValueLabel(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void CalcFieldStart(string name)
		{
			this.TypeStart(name, "CalcFieldExprHost");
		}

		public int CalcFieldEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_fieldHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).Fields);
		}

		public void QueryParametersStart()
		{
			this.TypeStart("QueryParameters", "IndexedExprHost");
		}

		public void QueryParametersEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "QueryParametersHost");
		}

		public void QueryParameterValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void DataSourceStart(string name)
		{
			this.TypeStart(name, "DataSourceExprHost");
		}

		public int DataSourceEnd()
		{
			return this.TypeEnd((TypeDecl)this.m_rootTypeDecl, "m_dataSourceHostsRemotable", ref this.m_rootTypeDecl.DataSources);
		}

		public void DataSourceConnectString(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ConnectStringExpr", expression);
		}

		public void DataSetStart(string name)
		{
			this.TypeStart(name, "DataSetExprHost");
		}

		public int DataSetEnd()
		{
			return this.TypeEnd((TypeDecl)this.m_rootTypeDecl, "m_dataSetHostsRemotable", ref this.m_rootTypeDecl.DataSets);
		}

		public void DataSetQueryCommandText(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("QueryCommandTextExpr", expression);
		}

		public void PageSectionStart()
		{
			this.TypeStart(this.CreateTypeName("PageSection", this.m_rootTypeDecl.PageSections), "StyleExprHost");
		}

		public int PageSectionEnd()
		{
			return this.TypeEnd((TypeDecl)this.m_rootTypeDecl, "m_pageSectionHostsRemotable", ref this.m_rootTypeDecl.PageSections);
		}

		public void PageStart()
		{
			this.TypeStart(this.CreateTypeName("Page", this.m_rootTypeDecl.Pages), "StyleExprHost");
		}

		public int PageEnd()
		{
			return this.TypeEnd((TypeDecl)this.m_rootTypeDecl, "m_pageHostsRemotable", ref this.m_rootTypeDecl.Pages);
		}

		public void ReportSectionStart()
		{
			this.TypeStart(this.CreateTypeName("ReportSection", this.m_rootTypeDecl.ReportSections), "ReportSectionExprHost");
		}

		public int ReportSectionEnd()
		{
			return this.TypeEnd((TypeDecl)this.m_rootTypeDecl, "m_reportSectionHostsRemotable", ref this.m_rootTypeDecl.ReportSections);
		}

		public void ParameterOmit(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OmitExpr", expression);
		}

		public void StyleAttribute(string name, AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd(name + "Expr", expression);
		}

		public void ActionInfoStart()
		{
			this.TypeStart("ActionInfo", "ActionInfoExprHost");
		}

		public void ActionInfoEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ActionInfoHost");
		}

		public void ActionStart()
		{
			this.TypeStart(this.CreateTypeName("Action", ((NonRootTypeDecl)this.m_currentTypeDecl).Actions), "ActionExprHost");
		}

		public int ActionEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_actionItemHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).Actions);
		}

		public void ActionHyperlink(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HyperlinkExpr", expression);
		}

		public void ActionDrillThroughReportName(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DrillThroughReportNameExpr", expression);
		}

		public void ActionDrillThroughBookmarkLink(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DrillThroughBookmarkLinkExpr", expression);
		}

		public void ActionBookmarkLink(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BookmarkLinkExpr", expression);
		}

		public void ActionDrillThroughParameterStart()
		{
			this.ParameterStart();
		}

		public int ActionDrillThroughParameterEnd()
		{
			return this.ParameterEnd("m_drillThroughParameterHostsRemotable");
		}

		public void ReportItemBookmark(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BookmarkExpr", expression);
		}

		public void ReportItemToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void LineStart(string name)
		{
			this.TypeStart(name, "ReportItemExprHost");
		}

		public int LineEnd()
		{
			return this.ReportItemEnd("m_lineHostsRemotable", ref this.m_rootTypeDecl.Lines);
		}

		public void RectangleStart(string name)
		{
			this.TypeStart(name, "ReportItemExprHost");
		}

		public int RectangleEnd()
		{
			return this.ReportItemEnd("m_rectangleHostsRemotable", ref this.m_rootTypeDecl.Rectangles);
		}

		public void TextBoxStart(string name)
		{
			this.TypeStart(name, "TextBoxExprHost");
		}

		public int TextBoxEnd()
		{
			return this.ReportItemEnd("m_textBoxHostsRemotable", ref this.m_rootTypeDecl.TextBoxes);
		}

		public void TextBoxToggleImageInitialState(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToggleImageInitialStateExpr", expression);
		}

		public void UserSortExpressionsStart()
		{
			this.TypeStart("UserSort", "IndexedExprHost");
		}

		public void UserSortExpressionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "UserSortExpressionsHost");
		}

		public void UserSortExpression(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void ImageStart(string name)
		{
			this.TypeStart(name, "ImageExprHost");
		}

		public int ImageEnd()
		{
			return this.ReportItemEnd("m_imageHostsRemotable", ref this.m_rootTypeDecl.Images);
		}

		public void ImageMIMEType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MIMETypeExpr", expression);
		}

		public void ImageTag(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TagExpr", expression);
		}

		public void SubreportStart(string name)
		{
			this.TypeStart(name, "SubreportExprHost");
		}

		public int SubreportEnd()
		{
			return this.ReportItemEnd("m_subreportHostsRemotable", ref this.m_rootTypeDecl.Subreports);
		}

		public void SubreportParameterStart()
		{
			this.ParameterStart();
		}

		public int SubreportParameterEnd()
		{
			return this.ParameterEnd("m_parameterHostsRemotable");
		}

		public void SortStart()
		{
			this.TypeStart("Sort", "SortExprHost");
		}

		public void SortEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "m_sortHost");
		}

		public void SortExpression(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void SortDirectionsStart()
		{
			this.TypeStart("SortDirections", "IndexedExprHost");
		}

		public void SortDirectionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "SortDirectionHosts");
		}

		public void SortDirection(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void FilterStart()
		{
			this.TypeStart(this.CreateTypeName("Filter", ((NonRootTypeDecl)this.m_currentTypeDecl).Filters), "FilterExprHost");
		}

		public int FilterEnd()
		{
			this.ExprIndexerCreate();
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_filterHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).Filters);
		}

		public void FilterExpression(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FilterExpressionExpr", expression);
		}

		public void FilterValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void GroupStart(string typeName)
		{
			this.TypeStart(typeName, "GroupExprHost");
		}

		public void GroupEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "m_groupHost");
		}

		public void GroupExpression(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void GroupParentExpressionsStart()
		{
			this.TypeStart("Parent", "IndexedExprHost");
		}

		public void GroupParentExpressionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ParentExpressionsHost");
		}

		public void GroupParentExpression(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void ReGroupExpressionsStart()
		{
			this.TypeStart("ReGroup", "IndexedExprHost");
		}

		public void ReGroupExpressionsEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ReGroupExpressionsHost");
		}

		public void ReGroupExpression(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void VariableValuesStart()
		{
			this.TypeStart("Variables", "IndexedExprHost");
		}

		public void VariableValuesEnd()
		{
			this.ExprIndexerCreate();
			this.TypeEnd(this.m_currentTypeDecl.Parent, "VariableValueHosts");
		}

		public void VariableValueExpression(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.IndexedExpressionAdd(expression);
		}

		public void DataRegionStart(DataRegionMode mode, string dataregionName)
		{
			switch (mode)
			{
			case DataRegionMode.Tablix:
				this.TypeStart(dataregionName, "TablixExprHost");
				break;
			case DataRegionMode.DataShape:
				this.TypeStart(dataregionName, "DataShapeExprHost");
				break;
			case DataRegionMode.Chart:
				this.TypeStart(dataregionName, "ChartExprHost");
				break;
			case DataRegionMode.GaugePanel:
				this.TypeStart(dataregionName, "GaugePanelExprHost");
				break;
			case DataRegionMode.CustomReportItem:
				this.TypeStart(dataregionName, "CustomReportItemExprHost");
				break;
			case DataRegionMode.MapDataRegion:
				this.TypeStart(dataregionName, "MapDataRegionExprHost");
				break;
			}
		}

		public int DataRegionEnd(DataRegionMode mode)
		{
			int result = -1;
			switch (mode)
			{
			case DataRegionMode.Tablix:
				result = this.ReportItemEnd("m_tablixHostsRemotable", ref this.m_rootTypeDecl.Tablices);
				break;
			case DataRegionMode.DataShape:
				result = this.ReportItemEnd("DataShapeExprHost", ref this.m_rootTypeDecl.DataShapes);
				break;
			case DataRegionMode.Chart:
				result = this.ReportItemEnd("m_chartHostsRemotable", ref this.m_rootTypeDecl.Charts);
				break;
			case DataRegionMode.GaugePanel:
				result = this.ReportItemEnd("m_gaugePanelHostsRemotable", ref this.m_rootTypeDecl.GaugePanels);
				break;
			case DataRegionMode.CustomReportItem:
				result = this.ReportItemEnd("m_customReportItemHostsRemotable", ref this.m_rootTypeDecl.CustomReportItems);
				break;
			case DataRegionMode.MapDataRegion:
				result = this.ReportItemEnd("m_mapDataRegionHostsRemotable", ref this.m_rootTypeDecl.CustomReportItems);
				break;
			}
			return result;
		}

		public void DataGroupStart(DataRegionMode mode, bool column)
		{
			string str = column ? "Column" : "Row";
			switch (mode)
			{
			case DataRegionMode.Tablix:
				this.TypeStart(this.CreateTypeName("TablixMember" + str, ((NonRootTypeDecl)this.m_currentTypeDecl).TablixMembers), "TablixMemberExprHost");
				break;
			case DataRegionMode.DataShape:
				this.TypeStart(this.CreateTypeName("DataShapeMember" + str, ((NonRootTypeDecl)this.m_currentTypeDecl).DataShapeMembers), "DataShapeMemberExprHost");
				break;
			case DataRegionMode.Chart:
				this.TypeStart(this.CreateTypeName("ChartMember" + str, ((NonRootTypeDecl)this.m_currentTypeDecl).ChartMembers), "ChartMemberExprHost");
				break;
			case DataRegionMode.GaugePanel:
				this.TypeStart(this.CreateTypeName("GaugeMember" + str, ((NonRootTypeDecl)this.m_currentTypeDecl).GaugeMembers), "GaugeMemberExprHost");
				break;
			case DataRegionMode.CustomReportItem:
				this.TypeStart(this.CreateTypeName("DataGroup" + str, ((NonRootTypeDecl)this.m_currentTypeDecl).DataGroups), "DataGroupExprHost");
				break;
			case DataRegionMode.MapDataRegion:
				this.TypeStart(this.CreateTypeName("MapMember" + str, ((NonRootTypeDecl)this.m_currentTypeDecl).MapMembers), "MapMemberExprHost");
				break;
			}
		}

		public int DataGroupEnd(DataRegionMode mode, bool column)
		{
			switch (mode)
			{
			case DataRegionMode.Tablix:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_memberTreeHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).TablixMembers);
			case DataRegionMode.DataShape:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_memberTreeHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).DataShapeMembers);
			case DataRegionMode.Chart:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_memberTreeHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartMembers);
			case DataRegionMode.GaugePanel:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_memberTreeHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).GaugeMembers);
			case DataRegionMode.CustomReportItem:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_memberTreeHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).DataGroups);
			case DataRegionMode.MapDataRegion:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_memberTreeHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapMembers);
			default:
				return -1;
			}
		}

		public void DataCellStart(DataRegionMode mode)
		{
			switch (mode)
			{
			case DataRegionMode.MapDataRegion:
				break;
			case DataRegionMode.Tablix:
				this.TypeStart(this.CreateTypeName("TablixCell", ((NonRootTypeDecl)this.m_currentTypeDecl).TablixCells), "TablixCellExprHost");
				break;
			case DataRegionMode.DataShape:
				this.TypeStart(this.CreateTypeName("DataShapeIntersection", ((NonRootTypeDecl)this.m_currentTypeDecl).DataShapeIntersections), "DataShapeIntersectionExprHost");
				break;
			case DataRegionMode.Chart:
				this.TypeStart(this.CreateTypeName("ChartDataPoint", ((NonRootTypeDecl)this.m_currentTypeDecl).DataPoints), "ChartDataPointExprHost");
				break;
			case DataRegionMode.GaugePanel:
				this.TypeStart(this.CreateTypeName("GaugeCell", ((NonRootTypeDecl)this.m_currentTypeDecl).GaugeCells), "GaugeCellExprHost");
				break;
			case DataRegionMode.CustomReportItem:
				this.TypeStart(this.CreateTypeName("DataCell", ((NonRootTypeDecl)this.m_currentTypeDecl).DataCells), "DataCellExprHost");
				break;
			}
		}

		public int DataCellEnd(DataRegionMode mode)
		{
			switch (mode)
			{
			case DataRegionMode.Tablix:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_cellHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).TablixCells);
			case DataRegionMode.DataShape:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_cellHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).DataShapeIntersections);
			case DataRegionMode.Chart:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_cellHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).DataPoints);
			case DataRegionMode.GaugePanel:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_cellHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).GaugeCells);
			case DataRegionMode.CustomReportItem:
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_cellHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).DataCells);
			default:
				return -1;
			}
		}

		public void MarginExpression(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, string marginPosition)
		{
			this.ExpressionAdd(marginPosition + "Expr", expression);
		}

		public void ChartTitleStart(string titleName)
		{
			this.TypeStart(this.CreateTypeName("ChartTitle" + titleName, ((NonRootTypeDecl)this.m_currentTypeDecl).ChartTitles), "ChartTitleExprHost");
		}

		public void ChartTitlePosition(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartTitlePositionExpr", expression);
		}

		public void ChartTitleHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		public void ChartTitleDocking(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DockingExpr", expression);
		}

		public void ChartTitleDockOffset(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DockingOffsetExpr", expression);
		}

		public void ChartTitleDockOutsideChartArea(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DockOutsideChartAreaExpr", expression);
		}

		public void ChartTitleToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void ChartTitleTextOrientation(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextOrientationExpr", expression);
		}

		public int ChartTitleEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_titlesHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartTitles);
		}

		public void ChartNoDataMessageStart()
		{
			this.TypeStart("ChartTitle", "ChartTitleExprHost");
		}

		public void ChartNoDataMessageEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "NoDataMessageHost");
		}

		public void ChartCaption(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CaptionExpr", expression);
		}

		public void ChartAxisTitleStart()
		{
			this.TypeStart("ChartAxisTitle", "ChartAxisTitleExprHost");
		}

		public void ChartAxisTitleTextOrientation(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextOrientationExpr", expression);
		}

		public void ChartAxisTitleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "TitleHost");
		}

		public void ChartLegendTitleStart()
		{
			this.TypeStart("ChartLegendTitle", "ChartLegendTitleExprHost");
		}

		public void ChartLegendTitleSeparator(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TitleSeparatorExpr", expression);
		}

		public void ChartLegendTitleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "TitleExprHost");
		}

		public void AxisMin(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisMinExpr", expression);
		}

		public void AxisMax(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisMaxExpr", expression);
		}

		public void AxisCrossAt(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisCrossAtExpr", expression);
		}

		public void AxisMajorInterval(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisMajorIntervalExpr", expression);
		}

		public void AxisMinorInterval(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisMinorIntervalExpr", expression);
		}

		public void ChartPalette(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PaletteExpr", expression);
		}

		public void ChartPaletteHatchBehavior(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PaletteHatchBehaviorExpr", expression);
		}

		public void DynamicWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DynamicWidthExpr", expression);
		}

		public void DynamicHeight(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DynamicHeightExpr", expression);
		}

		public void ChartAxisStart(string axisName, bool isValueAxis)
		{
			if (isValueAxis)
			{
				this.TypeStart(this.CreateTypeName("ValueAxis" + axisName, ((NonRootTypeDecl)this.m_currentTypeDecl).ValueAxes), "ChartAxisExprHost");
			}
			else
			{
				this.TypeStart(this.CreateTypeName("CategoryAxis" + axisName, ((NonRootTypeDecl)this.m_currentTypeDecl).CategoryAxes), "ChartAxisExprHost");
			}
		}

		public void ChartAxisVisible(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("VisibleExpr", expression);
		}

		public void ChartAxisMargin(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MarginExpr", expression);
		}

		public void ChartAxisInterval(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalExpr", expression);
		}

		public void ChartAxisIntervalType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalTypeExpr", expression);
		}

		public void ChartAxisIntervalOffset(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetExpr", expression);
		}

		public void ChartAxisIntervalOffsetType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetTypeExpr", expression);
		}

		public void ChartAxisMarksAlwaysAtPlotEdge(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MarksAlwaysAtPlotEdgeExpr", expression);
		}

		public void ChartAxisReverse(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ReverseExpr", expression);
		}

		public void ChartAxisLocation(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LocationExpr", expression);
		}

		public void ChartAxisInterlaced(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InterlacedExpr", expression);
		}

		public void ChartAxisInterlacedColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InterlacedColorExpr", expression);
		}

		public void ChartAxisLogScale(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LogScaleExpr", expression);
		}

		public void ChartAxisLogBase(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LogBaseExpr", expression);
		}

		public void ChartAxisHideLabels(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HideLabelsExpr", expression);
		}

		public void ChartAxisAngle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AngleExpr", expression);
		}

		public void ChartAxisArrows(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ArrowsExpr", expression);
		}

		public void ChartAxisPreventFontShrink(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PreventFontShrinkExpr", expression);
		}

		public void ChartAxisPreventFontGrow(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PreventFontGrowExpr", expression);
		}

		public void ChartAxisPreventLabelOffset(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PreventLabelOffsetExpr", expression);
		}

		public void ChartAxisPreventWordWrap(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PreventWordWrapExpr", expression);
		}

		public void ChartAxisAllowLabelRotation(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AllowLabelRotationExpr", expression);
		}

		public void ChartAxisIncludeZero(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IncludeZeroExpr", expression);
		}

		public void ChartAxisLabelsAutoFitDisabled(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelsAutoFitDisabledExpr", expression);
		}

		public void ChartAxisMinFontSize(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinFontSizeExpr", expression);
		}

		public void ChartAxisMaxFontSize(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaxFontSizeExpr", expression);
		}

		public void ChartAxisOffsetLabels(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OffsetLabelsExpr", expression);
		}

		public void ChartAxisHideEndLabels(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HideEndLabelsExpr", expression);
		}

		public void ChartAxisVariableAutoInterval(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("VariableAutoIntervalExpr", expression);
		}

		public void ChartAxisLabelInterval(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelIntervalExpr", expression);
		}

		public void ChartAxisLabelIntervalType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelIntervalTypeExpr", expression);
		}

		public void ChartAxisLabelIntervalOffset(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelIntervalOffsetExpr", expression);
		}

		public void ChartAxisLabelIntervalOffsetType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelIntervalOffsetTypeExpr", expression);
		}

		public int ChartAxisEnd(bool isValueAxis)
		{
			if (isValueAxis)
			{
				return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_valueAxesHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).ValueAxes);
			}
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_categoryAxesHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).CategoryAxes);
		}

		public void ChartGridLinesStart(bool isMajor)
		{
			this.TypeStart("ChartGridLines" + (isMajor ? "MajorGridLinesHost" : "MinorGridLinesHost"), "ChartGridLinesExprHost");
		}

		public void ChartGridLinesEnd(bool isMajor)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, isMajor ? "MajorGridLinesHost" : "MinorGridLinesHost");
		}

		public void ChartGridLinesIntervalOffsetType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetTypeExpr", expression);
		}

		public void ChartGridLinesIntervalOffset(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetExpr", expression);
		}

		public void ChartGridLinesEnabledIntervalType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalTypeExpr", expression);
		}

		public void ChartGridLinesInterval(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalExpr", expression);
		}

		public void ChartGridLinesEnabled(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EnabledExpr", expression);
		}

		public void ChartLegendStart(string legendName)
		{
			this.TypeStart(this.CreateTypeName("ChartLegend" + legendName, ((NonRootTypeDecl)this.m_currentTypeDecl).ChartLegends), "ChartLegendExprHost");
		}

		public void ChartLegendHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		public void ChartLegendPosition(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartLegendPositionExpr", expression);
		}

		public void ChartLegendLayout(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LayoutExpr", expression);
		}

		public void ChartLegendDockOutsideChartArea(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DockOutsideChartAreaExpr", expression);
		}

		public void ChartLegendAutoFitTextDisabled(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AutoFitTextDisabledExpr", expression);
		}

		public void ChartLegendMinFontSize(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinFontSizeExpr", expression);
		}

		public void ChartLegendHeaderSeparator(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HeaderSeparatorExpr", expression);
		}

		public void ChartLegendHeaderSeparatorColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HeaderSeparatorColorExpr", expression);
		}

		public void ChartLegendColumnSeparator(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ColumnSeparatorExpr", expression);
		}

		public void ChartLegendColumnSeparatorColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ColumnSeparatorColorExpr", expression);
		}

		public void ChartLegendColumnSpacing(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ColumnSpacingExpr", expression);
		}

		public void ChartLegendInterlacedRows(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InterlacedRowsExpr", expression);
		}

		public void ChartLegendInterlacedRowsColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InterlacedRowsColorExpr", expression);
		}

		public void ChartLegendEquallySpacedItems(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EquallySpacedItemsExpr", expression);
		}

		public void ChartLegendReversed(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ReversedExpr", expression);
		}

		public void ChartLegendMaxAutoSize(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaxAutoSizeExpr", expression);
		}

		public void ChartLegendTextWrapThreshold(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextWrapThresholdExpr", expression);
		}

		public int ChartLegendEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_legendsHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartLegends);
		}

		public void ChartSeriesStart()
		{
			this.TypeStart("ChartSeries", "ChartSeriesExprHost");
		}

		public void ChartSeriesType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TypeExpr", expression);
		}

		public void ChartSeriesSubtype(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SubtypeExpr", expression);
		}

		public void ChartSeriesLegendName(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LegendNameExpr", expression);
		}

		public void ChartSeriesLegendText(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LegendTextExpr", expression);
		}

		public void ChartSeriesChartAreaName(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartAreaNameExpr", expression);
		}

		public void ChartSeriesValueAxisName(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueAxisNameExpr", expression);
		}

		public void ChartSeriesCategoryAxisName(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CategoryAxisNameExpr", expression);
		}

		public void ChartSeriesHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		public void ChartSeriesHideInLegend(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HideInLegendExpr", expression);
		}

		public void ChartSeriesToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void ChartSeriesEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ChartSeriesHost");
		}

		public void ChartNoMoveDirectionsStart()
		{
			this.TypeStart("ChartNoMoveDirections", "ChartNoMoveDirectionsExprHost");
		}

		public void ChartNoMoveDirectionsUp(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UpExpr", expression);
		}

		public void ChartNoMoveDirectionsDown(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DownExpr", expression);
		}

		public void ChartNoMoveDirectionsLeft(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LeftExpr", expression);
		}

		public void ChartNoMoveDirectionsRight(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RightExpr", expression);
		}

		public void ChartNoMoveDirectionsUpLeft(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UpLeftExpr", expression);
		}

		public void ChartNoMoveDirectionsUpRight(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UpRightExpr", expression);
		}

		public void ChartNoMoveDirectionsDownLeft(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DownLeftExpr", expression);
		}

		public void ChartNoMoveDirectionsDownRight(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DownRightExpr", expression);
		}

		public void ChartNoMoveDirectionsEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "NoMoveDirectionsHost");
		}

		public void ChartElementPositionStart(bool innerPlot)
		{
			this.TypeStart(innerPlot ? "ChartInnerPlotPosition" : "ChartElementPosition", "ChartElementPositionExprHost");
		}

		public void ChartElementPositionEnd(bool innerPlot)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, innerPlot ? "ChartInnerPlotPositionHost" : "ChartElementPositionHost");
		}

		public void ChartElementPositionTop(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TopExpr", expression);
		}

		public void ChartElementPositionLeft(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LeftExpr", expression);
		}

		public void ChartElementPositionHeight(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HeightExpr", expression);
		}

		public void ChartElementPositionWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WidthExpr", expression);
		}

		public void ChartSmartLabelStart()
		{
			this.TypeStart("ChartSmartLabel", "ChartSmartLabelExprHost");
		}

		public void ChartSmartLabelAllowOutSidePlotArea(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AllowOutSidePlotAreaExpr", expression);
		}

		public void ChartSmartLabelCalloutBackColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CalloutBackColorExpr", expression);
		}

		public void ChartSmartLabelCalloutLineAnchor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CalloutLineAnchorExpr", expression);
		}

		public void ChartSmartLabelCalloutLineColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CalloutLineColorExpr", expression);
		}

		public void ChartSmartLabelCalloutLineStyle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CalloutLineStyleExpr", expression);
		}

		public void ChartSmartLabelCalloutLineWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CalloutLineWidthExpr", expression);
		}

		public void ChartSmartLabelCalloutStyle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CalloutStyleExpr", expression);
		}

		public void ChartSmartLabelShowOverlapped(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShowOverlappedExpr", expression);
		}

		public void ChartSmartLabelMarkerOverlapping(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MarkerOverlappingExpr", expression);
		}

		public void ChartSmartLabelDisabled(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DisabledExpr", expression);
		}

		public void ChartSmartLabelMaxMovingDistance(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaxMovingDistanceExpr", expression);
		}

		public void ChartSmartLabelMinMovingDistance(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinMovingDistanceExpr", expression);
		}

		public void ChartSmartLabelEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "SmartLabelHost");
		}

		public void ChartAxisScaleBreakStart()
		{
			this.TypeStart("ChartAxisScaleBreak", "ChartAxisScaleBreakExprHost");
		}

		public void ChartAxisScaleBreakEnabled(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EnabledExpr", expression);
		}

		public void ChartAxisScaleBreakBreakLineType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BreakLineTypeExpr", expression);
		}

		public void ChartAxisScaleBreakCollapsibleSpaceThreshold(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CollapsibleSpaceThresholdExpr", expression);
		}

		public void ChartAxisScaleBreakMaxNumberOfBreaks(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaxNumberOfBreaksExpr", expression);
		}

		public void ChartAxisScaleBreakSpacing(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SpacingExpr", expression);
		}

		public void ChartAxisScaleBreakIncludeZero(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IncludeZeroExpr", expression);
		}

		public void ChartAxisScaleBreakEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "AxisScaleBreakHost");
		}

		public void ChartBorderSkinStart()
		{
			this.TypeStart("ChartBorderSkin", "ChartBorderSkinExprHost");
		}

		public void ChartBorderSkinBorderSkinType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BorderSkinTypeExpr", expression);
		}

		public void ChartBorderSkinEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "BorderSkinHost");
		}

		public void ChartItemInLegendStart()
		{
			this.TypeStart("ChartItemInLegend", "ChartDataPointInLegendExprHost");
		}

		public void ChartItemInLegendLegendText(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LegendTextExpr", expression);
		}

		public void ChartItemInLegendToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void ChartItemInLegendHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		public void ChartItemInLegendEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "DataPointInLegendHost");
		}

		public void ChartTickMarksStart(bool isMajor)
		{
			this.TypeStart("ChartTickMarks" + (isMajor ? "MajorTickMarksHost" : "MinorTickMarksHost"), "ChartTickMarksExprHost");
		}

		public void ChartTickMarksEnd(bool isMajor)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, isMajor ? "MajorTickMarksHost" : "MinorTickMarksHost");
		}

		public void ChartTickMarksEnabled(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EnabledExpr", expression);
		}

		public void ChartTickMarksType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TypeExpr", expression);
		}

		public void ChartTickMarksLength(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LengthExpr", expression);
		}

		public void ChartTickMarksInterval(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalExpr", expression);
		}

		public void ChartTickMarksIntervalType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalTypeExpr", expression);
		}

		public void ChartTickMarksIntervalOffset(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetExpr", expression);
		}

		public void ChartTickMarksIntervalOffsetType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetTypeExpr", expression);
		}

		public void ChartEmptyPointsStart()
		{
			this.TypeStart("ChartEmptyPoints", "ChartEmptyPointsExprHost");
		}

		public void ChartEmptyPointsAxisLabel(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisLabelExpr", expression);
		}

		public void ChartEmptyPointsToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void ChartEmptyPointsEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "EmptyPointsHost");
		}

		public void ChartLegendColumnHeaderStart()
		{
			this.TypeStart("ChartLegendColumnHeader", "ChartLegendColumnHeaderExprHost");
		}

		public void ChartLegendColumnHeaderValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		public void ChartLegendColumnHeaderEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ChartLegendColumnHeaderHost");
		}

		public void ChartCustomPaletteColorStart(int index)
		{
			this.TypeStart(this.CreateTypeName("ChartCustomPaletteColor" + index.ToString(CultureInfo.InvariantCulture), ((NonRootTypeDecl)this.m_currentTypeDecl).ChartCustomPaletteColors), "ChartCustomPaletteColorExprHost");
		}

		public int ChartCustomPaletteColorEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_customPaletteColorHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartCustomPaletteColors);
		}

		public void ChartCustomPaletteColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ColorExpr", expression);
		}

		public void ChartLegendCustomItemCellStart(string name)
		{
			this.TypeStart(this.CreateTypeName("ChartLegendCustomItemCell" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).ChartLegendCustomItemCells), "ChartLegendCustomItemCellExprHost");
		}

		public void ChartLegendCustomItemCellCellType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CellTypeExpr", expression);
		}

		public void ChartLegendCustomItemCellText(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextExpr", expression);
		}

		public void ChartLegendCustomItemCellCellSpan(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CellSpanExpr", expression);
		}

		public void ChartLegendCustomItemCellToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void ChartLegendCustomItemCellImageWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ImageWidthExpr", expression);
		}

		public void ChartLegendCustomItemCellImageHeight(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ImageHeightExpr", expression);
		}

		public void ChartLegendCustomItemCellSymbolHeight(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SymbolHeightExpr", expression);
		}

		public void ChartLegendCustomItemCellSymbolWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SymbolWidthExpr", expression);
		}

		public void ChartLegendCustomItemCellAlignment(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AlignmentExpr", expression);
		}

		public void ChartLegendCustomItemCellTopMargin(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TopMarginExpr", expression);
		}

		public void ChartLegendCustomItemCellBottomMargin(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BottomMarginExpr", expression);
		}

		public void ChartLegendCustomItemCellLeftMargin(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LeftMarginExpr", expression);
		}

		public void ChartLegendCustomItemCellRightMargin(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RightMarginExpr", expression);
		}

		public int ChartLegendCustomItemCellEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_legendCustomItemCellHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartLegendCustomItemCells);
		}

		public void ChartDerivedSeriesStart(int index)
		{
			this.TypeStart(this.CreateTypeName("ChartDerivedSeries" + index.ToString(CultureInfo.InvariantCulture), ((NonRootTypeDecl)this.m_currentTypeDecl).ChartDerivedSeriesCollection), "ChartDerivedSeriesExprHost");
		}

		public void ChartDerivedSeriesSourceChartSeriesName(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SourceChartSeriesNameExpr", expression);
		}

		public void ChartDerivedSeriesDerivedSeriesFormula(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DerivedSeriesFormulaExpr", expression);
		}

		public int ChartDerivedSeriesEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_derivedSeriesCollectionHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartDerivedSeriesCollection);
		}

		public void ChartStripLineStart(int index)
		{
			this.TypeStart(this.CreateTypeName("ChartStripLine" + index.ToString(CultureInfo.InvariantCulture), ((NonRootTypeDecl)this.m_currentTypeDecl).ChartStripLines), "ChartStripLineExprHost");
		}

		public void ChartStripLineTitle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TitleExpr", expression);
		}

		public void ChartStripLineTitleAngle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TitleAngleExpr", expression);
		}

		public void ChartStripLineTextOrientation(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextOrientationExpr", expression);
		}

		public void ChartStripLineToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void ChartStripLineInterval(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalExpr", expression);
		}

		public void ChartStripLineIntervalType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalTypeExpr", expression);
		}

		public void ChartStripLineIntervalOffset(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetExpr", expression);
		}

		public void ChartStripLineIntervalOffsetType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetTypeExpr", expression);
		}

		public void ChartStripLineStripWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StripWidthExpr", expression);
		}

		public void ChartStripLineStripWidthType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StripWidthTypeExpr", expression);
		}

		public int ChartStripLineEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_stripLinesHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartStripLines);
		}

		public void ChartFormulaParameterStart(string name)
		{
			this.TypeStart(this.CreateTypeName("ChartFormulaParameter" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).ChartFormulaParameters), "ChartFormulaParameterExprHost");
		}

		public void ChartFormulaParameterValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		public int ChartFormulaParameterEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_formulaParametersHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartFormulaParameters);
		}

		public void ChartLegendColumnStart(string name)
		{
			this.TypeStart(this.CreateTypeName("ChartLegendColumn" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).ChartLegendColumns), "ChartLegendColumnExprHost");
		}

		public void ChartLegendColumnColumnType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ColumnTypeExpr", expression);
		}

		public void ChartLegendColumnValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		public void ChartLegendColumnToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void ChartLegendColumnMinimumWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinimumWidthExpr", expression);
		}

		public void ChartLegendColumnMaximumWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaximumWidthExpr", expression);
		}

		public void ChartLegendColumnSeriesSymbolWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SeriesSymbolWidthExpr", expression);
		}

		public void ChartLegendColumnSeriesSymbolHeight(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SeriesSymbolHeightExpr", expression);
		}

		public int ChartLegendColumnEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_legendColumnsHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartLegendColumns);
		}

		public void ChartLegendCustomItemStart(string name)
		{
			this.TypeStart(this.CreateTypeName("ChartLegendCustomItem" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).ChartLegendCustomItems), "ChartLegendCustomItemExprHost");
		}

		public void ChartLegendCustomItemSeparator(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SeparatorExpr", expression);
		}

		public void ChartLegendCustomItemSeparatorColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SeparatorColorExpr", expression);
		}

		public void ChartLegendCustomItemToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public int ChartLegendCustomItemEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_legendCustomItemsHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartLegendCustomItems);
		}

		public void ChartAreaStart(string chartAreaName)
		{
			this.TypeStart(this.CreateTypeName("ChartArea" + chartAreaName, ((NonRootTypeDecl)this.m_currentTypeDecl).ChartAreas), "ChartAreaExprHost");
		}

		public void Chart3DPropertiesStart()
		{
			this.TypeStart("Chart3DProperties", "Chart3DPropertiesExprHost");
		}

		public void Chart3DPropertiesEnabled(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EnabledExpr", expression);
		}

		public void Chart3DPropertiesRotation(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RotationExpr", expression);
		}

		public void Chart3DPropertiesProjectionMode(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ProjectionModeExpr", expression);
		}

		public void Chart3DPropertiesInclination(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InclinationExpr", expression);
		}

		public void Chart3DPropertiesPerspective(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PerspectiveExpr", expression);
		}

		public void Chart3DPropertiesDepthRatio(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DepthRatioExpr", expression);
		}

		public void Chart3DPropertiesShading(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShadingExpr", expression);
		}

		public void Chart3DPropertiesGapDepth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("GapDepthExpr", expression);
		}

		public void Chart3DPropertiesWallThickness(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WallThicknessExpr", expression);
		}

		public void Chart3DPropertiesClustered(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ClusteredExpr", expression);
		}

		public void Chart3DPropertiesEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "Chart3DPropertiesHost");
		}

		public void ChartAreaHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		public void ChartAreaAlignOrientation(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AlignOrientationExpr", expression);
		}

		public void ChartAreaEquallySizedAxesFont(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EquallySizedAxesFontExpr", expression);
		}

		public void ChartAlignTypePosition(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartAlignTypePositionExpr", expression);
		}

		public void ChartAlignTypeInnerPlotPosition(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InnerPlotPositionExpr", expression);
		}

		public void ChartAlignTypCursor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CursorExpr", expression);
		}

		public void ChartAlignTypeAxesView(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AxesViewExpr", expression);
		}

		public int ChartAreaEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_chartAreasHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).ChartAreas);
		}

		public void ChartDataPointValueX(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesXExpr", expression);
		}

		public void ChartDataPointValueY(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesYExpr", expression);
		}

		public void ChartDataPointValueSize(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesSizeExpr", expression);
		}

		public void ChartDataPointValueHigh(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesHighExpr", expression);
		}

		public void ChartDataPointValueLow(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesLowExpr", expression);
		}

		public void ChartDataPointValueStart(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesStartExpr", expression);
		}

		public void ChartDataPointValueEnd(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesEndExpr", expression);
		}

		public void ChartDataPointValueMean(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesMeanExpr", expression);
		}

		public void ChartDataPointValueMedian(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesMedianExpr", expression);
		}

		public void ChartDataPointValueHighlightX(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesHighlightXExpr", expression);
		}

		public void ChartDataPointValueHighlightY(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesHighlightYExpr", expression);
		}

		public void ChartDataPointValueHighlightSize(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataPointValuesHighlightSizeExpr", expression);
		}

		public void ChartDataPointValueFormatX(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartDataPointValueFormatXExpr", expression);
		}

		public void ChartDataPointValueFormatY(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartDataPointValueFormatYExpr", expression);
		}

		public void ChartDataPointValueFormatSize(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartDataPointValueFormatSizeExpr", expression);
		}

		public void ChartDataPointValueCurrencyLanguageX(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartDataPointValueCurrencyLanguageXExpr", expression);
		}

		public void ChartDataPointValueCurrencyLanguageY(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartDataPointValueCurrencyLanguageYExpr", expression);
		}

		public void ChartDataPointValueCurrencyLanguageSize(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartDataPointValueCurrencyLanguageSizeExpr", expression);
		}

		public void ChartDataPointAxisLabel(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AxisLabelExpr", expression);
		}

		public void ChartDataPointToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void DataLabelStart()
		{
			this.TypeStart("DataLabel", "ChartDataLabelExprHost");
		}

		public void DataLabelLabel(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelExpr", expression);
		}

		public void DataLabelVisible(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("VisibleExpr", expression);
		}

		public void DataLabelPosition(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ChartDataLabelPositionExpr", expression);
		}

		public void DataLabelRotation(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RotationExpr", expression);
		}

		public void DataLabelUseValueAsLabel(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseValueAsLabelExpr", expression);
		}

		public void ChartDataLabelToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void DataLabelEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "DataLabelHost");
		}

		public void DataPointStyleStart()
		{
			this.StyleStart("Style");
		}

		public void DataPointStyleEnd()
		{
			this.StyleEnd("StyleHost");
		}

		public void DataPointMarkerStart()
		{
			this.TypeStart("ChartMarker", "ChartMarkerExprHost");
		}

		public void DataPointMarkerSize(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SizeExpr", expression);
		}

		public void DataPointMarkerType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TypeExpr", expression);
		}

		public void DataPointMarkerEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ChartMarkerHost");
		}

		public void ChartMemberLabel(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MemberLabelExpr", expression);
		}

		public void ChartMemberStyleStart()
		{
			this.StyleStart("MemberStyle");
		}

		public void ChartMemberStyleEnd()
		{
			this.StyleEnd("MemberStyleHost");
		}

		public void DataValueStart()
		{
			this.TypeStart(this.CreateTypeName("DataValue", this.m_currentTypeDecl.DataValues), "DataValueExprHost");
		}

		public int DataValueEnd(bool isCustomProperty)
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, isCustomProperty ? "m_customPropertyHostsRemotable" : "m_dataValueHostsRemotable", ref this.m_currentTypeDecl.Parent.DataValues);
		}

		public void DataValueName(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataValueNameExpr", expression);
		}

		public void DataValueValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataValueValueExpr", expression);
		}

		public void BaseGaugeImageSource(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SourceExpr", expression);
		}

		public void BaseGaugeImageValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		public void BaseGaugeImageMIMEType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MIMETypeExpr", expression);
		}

		public void BaseGaugeImageTransparentColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TransparentColorExpr", expression);
		}

		public void CapImageStart()
		{
			this.TypeStart("CapImage", "CapImageExprHost");
		}

		public void CapImageEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "CapImageHost");
		}

		public void CapImageHueColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HueColorExpr", expression);
		}

		public void CapImageOffsetX(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OffsetXExpr", expression);
		}

		public void CapImageOffsetY(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OffsetYExpr", expression);
		}

		public void FrameImageStart()
		{
			this.TypeStart("FrameImage", "FrameImageExprHost");
		}

		public void FrameImageEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "FrameImageHost");
		}

		public void FrameImageHueColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HueColorExpr", expression);
		}

		public void FrameImageTransparency(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TransparencyExpr", expression);
		}

		public void FrameImageClipImage(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ClipImageExpr", expression);
		}

		public void PointerImageStart()
		{
			this.TypeStart("PointerImage", "PointerImageExprHost");
		}

		public void PointerImageEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "PointerImageHost");
		}

		public void PointerImageHueColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HueColorExpr", expression);
		}

		public void PointerImageTransparency(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TransparencyExpr", expression);
		}

		public void PointerImageOffsetX(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OffsetXExpr", expression);
		}

		public void PointerImageOffsetY(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OffsetYExpr", expression);
		}

		public void TopImageStart()
		{
			this.TypeStart("TopImage", "TopImageExprHost");
		}

		public void TopImageEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "TopImageHost");
		}

		public void TopImageHueColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HueColorExpr", expression);
		}

		public void BackFrameStart()
		{
			this.TypeStart("BackFrame", "BackFrameExprHost");
		}

		public void BackFrameEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "BackFrameHost");
		}

		public void BackFrameFrameStyle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FrameStyleExpr", expression);
		}

		public void BackFrameFrameShape(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FrameShapeExpr", expression);
		}

		public void BackFrameFrameWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FrameWidthExpr", expression);
		}

		public void BackFrameGlassEffect(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("GlassEffectExpr", expression);
		}

		public void FrameBackgroundStart()
		{
			this.TypeStart("FrameBackground", "FrameBackgroundExprHost");
		}

		public void FrameBackgroundEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "FrameBackgroundHost");
		}

		public void CustomLabelStart(string name)
		{
			this.TypeStart(this.CreateTypeName("CustomLabel" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).CustomLabels), "CustomLabelExprHost");
		}

		public int CustomLabelEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_customLabelsHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).CustomLabels);
		}

		public void CustomLabelText(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextExpr", expression);
		}

		public void CustomLabelAllowUpsideDown(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AllowUpsideDownExpr", expression);
		}

		public void CustomLabelDistanceFromScale(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DistanceFromScaleExpr", expression);
		}

		public void CustomLabelFontAngle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FontAngleExpr", expression);
		}

		public void CustomLabelPlacement(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PlacementExpr", expression);
		}

		public void CustomLabelRotateLabel(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RotateLabelExpr", expression);
		}

		public void CustomLabelValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		public void CustomLabelHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		public void CustomLabelUseFontPercent(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseFontPercentExpr", expression);
		}

		public void GaugeClipContent(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ClipContentExpr", expression);
		}

		public void GaugeImageStart(string name)
		{
			this.TypeStart(this.CreateTypeName("GaugeImage" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).GaugeImages), "GaugeImageExprHost");
		}

		public int GaugeImageEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_gaugeImagesHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).GaugeImages);
		}

		public void GaugeAspectRatio(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AspectRatioExpr", expression);
		}

		public void GaugeInputValueStart(int index)
		{
			this.TypeStart(this.CreateTypeName("GaugeInputValue" + index.ToString(CultureInfo.InvariantCulture), ((NonRootTypeDecl)this.m_currentTypeDecl).GaugeInputValues), "GaugeInputValueExprHost");
		}

		public int GaugeInputValueEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_gaugeInputValueHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).GaugeInputValues);
		}

		public void GaugeInputValueValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		public void GaugeInputValueFormula(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FormulaExpr", expression);
		}

		public void GaugeInputValueMinPercent(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinPercentExpr", expression);
		}

		public void GaugeInputValueMaxPercent(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaxPercentExpr", expression);
		}

		public void GaugeInputValueMultiplier(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MultiplierExpr", expression);
		}

		public void GaugeInputValueAddConstant(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AddConstantExpr", expression);
		}

		public void GaugeLabelStart(string name)
		{
			this.TypeStart(this.CreateTypeName("GaugeLabel" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).GaugeLabels), "GaugeLabelExprHost");
		}

		public int GaugeLabelEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_gaugeLabelsHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).GaugeLabels);
		}

		public void GaugeLabelText(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextExpr", expression);
		}

		public void GaugeLabelAngle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AngleExpr", expression);
		}

		public void GaugeLabelResizeMode(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ResizeModeExpr", expression);
		}

		public void GaugeLabelTextShadowOffset(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextShadowOffsetExpr", expression);
		}

		public void GaugeLabelUseFontPercent(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseFontPercentExpr", expression);
		}

		public void GaugePanelAntiAliasing(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AntiAliasingExpr", expression);
		}

		public void GaugePanelAutoLayout(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AutoLayoutExpr", expression);
		}

		public void GaugePanelShadowIntensity(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShadowIntensityExpr", expression);
		}

		public void GaugePanelTextAntiAliasingQuality(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextAntiAliasingQualityExpr", expression);
		}

		public void GaugePanelItemTop(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TopExpr", expression);
		}

		public void GaugePanelItemLeft(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LeftExpr", expression);
		}

		public void GaugePanelItemHeight(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HeightExpr", expression);
		}

		public void GaugePanelItemWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WidthExpr", expression);
		}

		public void GaugePanelItemZIndex(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ZIndexExpr", expression);
		}

		public void GaugePanelItemHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		public void GaugePanelItemToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void GaugePointerBarStart(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BarStartExpr", expression);
		}

		public void GaugePointerDistanceFromScale(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DistanceFromScaleExpr", expression);
		}

		public void GaugePointerMarkerLength(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MarkerLengthExpr", expression);
		}

		public void GaugePointerMarkerStyle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MarkerStyleExpr", expression);
		}

		public void GaugePointerPlacement(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PlacementExpr", expression);
		}

		public void GaugePointerSnappingEnabled(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SnappingEnabledExpr", expression);
		}

		public void GaugePointerSnappingInterval(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SnappingIntervalExpr", expression);
		}

		public void GaugePointerToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void GaugePointerHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		public void GaugePointerWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WidthExpr", expression);
		}

		public void GaugeScaleInterval(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalExpr", expression);
		}

		public void GaugeScaleIntervalOffset(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetExpr", expression);
		}

		public void GaugeScaleLogarithmic(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LogarithmicExpr", expression);
		}

		public void GaugeScaleLogarithmicBase(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LogarithmicBaseExpr", expression);
		}

		public void GaugeScaleMultiplier(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MultiplierExpr", expression);
		}

		public void GaugeScaleReversed(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ReversedExpr", expression);
		}

		public void GaugeScaleTickMarksOnTop(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TickMarksOnTopExpr", expression);
		}

		public void GaugeScaleToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void GaugeScaleHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		public void GaugeScaleWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WidthExpr", expression);
		}

		public void GaugeTickMarksStart(bool isMajor)
		{
			this.TypeStart("GaugeTickMarks" + (isMajor ? "GaugeMajorTickMarksHost" : "GaugeMinorTickMarksHost"), "GaugeTickMarksExprHost");
		}

		public void GaugeTickMarksEnd(bool isMajor)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, isMajor ? "GaugeMajorTickMarksHost" : "GaugeMinorTickMarksHost");
		}

		public void TickMarkStyleStart()
		{
			this.TypeStart("TickMarkStyle", "TickMarkStyleExprHost");
		}

		public void TickMarkStyleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "TickMarkStyleHost");
		}

		public void GaugeTickMarksInterval(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalExpr", expression);
		}

		public void GaugeTickMarksIntervalOffset(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetExpr", expression);
		}

		public void LinearGaugeStart(string name)
		{
			this.TypeStart(this.CreateTypeName("LinearGauge" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).LinearGauges), "LinearGaugeExprHost");
		}

		public int LinearGaugeEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_linearGaugesHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).LinearGauges);
		}

		public void LinearGaugeOrientation(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OrientationExpr", expression);
		}

		public void LinearPointerStart(string name)
		{
			this.TypeStart(this.CreateTypeName("LinearPointer" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).LinearPointers), "LinearPointerExprHost");
		}

		public int LinearPointerEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_linearPointersHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).LinearPointers);
		}

		public void LinearPointerType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TypeExpr", expression);
		}

		public void LinearScaleStart(string name)
		{
			this.TypeStart(this.CreateTypeName("LinearScale" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).LinearScales), "LinearScaleExprHost");
		}

		public int LinearScaleEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_linearScalesHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).LinearScales);
		}

		public void LinearScaleStartMargin(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StartMarginExpr", expression);
		}

		public void LinearScaleEndMargin(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EndMarginExpr", expression);
		}

		public void LinearScalePosition(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PositionExpr", expression);
		}

		public void NumericIndicatorStart(string name)
		{
			this.TypeStart(this.CreateTypeName("NumericIndicator" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).NumericIndicators), "NumericIndicatorExprHost");
		}

		public int NumericIndicatorEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_numericIndicatorsHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).NumericIndicators);
		}

		public void NumericIndicatorDecimalDigitColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DecimalDigitColorExpr", expression);
		}

		public void NumericIndicatorDigitColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DigitColorExpr", expression);
		}

		public void NumericIndicatorUseFontPercent(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseFontPercentExpr", expression);
		}

		public void NumericIndicatorDecimalDigits(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DecimalDigitsExpr", expression);
		}

		public void NumericIndicatorDigits(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DigitsExpr", expression);
		}

		public void NumericIndicatorMultiplier(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MultiplierExpr", expression);
		}

		public void NumericIndicatorNonNumericString(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("NonNumericStringExpr", expression);
		}

		public void NumericIndicatorOutOfRangeString(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OutOfRangeStringExpr", expression);
		}

		public void NumericIndicatorResizeMode(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ResizeModeExpr", expression);
		}

		public void NumericIndicatorShowDecimalPoint(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShowDecimalPointExpr", expression);
		}

		public void NumericIndicatorShowLeadingZeros(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShowLeadingZerosExpr", expression);
		}

		public void NumericIndicatorIndicatorStyle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IndicatorStyleExpr", expression);
		}

		public void NumericIndicatorShowSign(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShowSignExpr", expression);
		}

		public void NumericIndicatorSnappingEnabled(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SnappingEnabledExpr", expression);
		}

		public void NumericIndicatorSnappingInterval(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SnappingIntervalExpr", expression);
		}

		public void NumericIndicatorLedDimColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LedDimColorExpr", expression);
		}

		public void NumericIndicatorSeparatorWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SeparatorWidthExpr", expression);
		}

		public void NumericIndicatorSeparatorColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SeparatorColorExpr", expression);
		}

		public void NumericIndicatorRangeStart(string name)
		{
			this.TypeStart(this.CreateTypeName("NumericIndicatorRange" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).NumericIndicatorRanges), "NumericIndicatorRangeExprHost");
		}

		public int NumericIndicatorRangeEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_numericIndicatorRangesHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).NumericIndicatorRanges);
		}

		public void NumericIndicatorRangeDecimalDigitColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DecimalDigitColorExpr", expression);
		}

		public void NumericIndicatorRangeDigitColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DigitColorExpr", expression);
		}

		public void PinLabelStart()
		{
			this.TypeStart("PinLabel", "PinLabelExprHost");
		}

		public void PinLabelEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "PinLabelHost");
		}

		public void PinLabelText(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextExpr", expression);
		}

		public void PinLabelAllowUpsideDown(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AllowUpsideDownExpr", expression);
		}

		public void PinLabelDistanceFromScale(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DistanceFromScaleExpr", expression);
		}

		public void PinLabelFontAngle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FontAngleExpr", expression);
		}

		public void PinLabelPlacement(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PlacementExpr", expression);
		}

		public void PinLabelRotateLabel(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RotateLabelExpr", expression);
		}

		public void PinLabelUseFontPercent(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseFontPercentExpr", expression);
		}

		public void PointerCapStart()
		{
			this.TypeStart("PointerCap", "PointerCapExprHost");
		}

		public void PointerCapEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "PointerCapHost");
		}

		public void PointerCapOnTop(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OnTopExpr", expression);
		}

		public void PointerCapReflection(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ReflectionExpr", expression);
		}

		public void PointerCapCapStyle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CapStyleExpr", expression);
		}

		public void PointerCapHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		public void PointerCapWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WidthExpr", expression);
		}

		public void RadialGaugeStart(string name)
		{
			this.TypeStart(this.CreateTypeName("RadialGauge" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).RadialGauges), "RadialGaugeExprHost");
		}

		public int RadialGaugeEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_radialGaugesHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).RadialGauges);
		}

		public void RadialGaugePivotX(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PivotXExpr", expression);
		}

		public void RadialGaugePivotY(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PivotYExpr", expression);
		}

		public void RadialPointerStart(string name)
		{
			this.TypeStart(this.CreateTypeName("RadialPointer" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).RadialPointers), "RadialPointerExprHost");
		}

		public int RadialPointerEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_radialPointersHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).RadialPointers);
		}

		public void RadialPointerType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TypeExpr", expression);
		}

		public void RadialPointerNeedleStyle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("NeedleStyleExpr", expression);
		}

		public void RadialScaleStart(string name)
		{
			this.TypeStart(this.CreateTypeName("RadialScale" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).RadialScales), "RadialScaleExprHost");
		}

		public int RadialScaleEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_radialScalesHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).RadialScales);
		}

		public void RadialScaleRadius(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RadiusExpr", expression);
		}

		public void RadialScaleStartAngle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StartAngleExpr", expression);
		}

		public void RadialScaleSweepAngle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SweepAngleExpr", expression);
		}

		public void ScaleLabelsStart()
		{
			this.TypeStart("ScaleLabels", "ScaleLabelsExprHost");
		}

		public void ScaleLabelsEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ScaleLabelsHost");
		}

		public void ScaleLabelsInterval(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalExpr", expression);
		}

		public void ScaleLabelsIntervalOffset(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalOffsetExpr", expression);
		}

		public void ScaleLabelsAllowUpsideDown(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AllowUpsideDownExpr", expression);
		}

		public void ScaleLabelsDistanceFromScale(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DistanceFromScaleExpr", expression);
		}

		public void ScaleLabelsFontAngle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FontAngleExpr", expression);
		}

		public void ScaleLabelsPlacement(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PlacementExpr", expression);
		}

		public void ScaleLabelsRotateLabels(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RotateLabelsExpr", expression);
		}

		public void ScaleLabelsShowEndLabels(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShowEndLabelsExpr", expression);
		}

		public void ScaleLabelsHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		public void ScaleLabelsUseFontPercent(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseFontPercentExpr", expression);
		}

		public void ScalePinStart(bool isMaximum)
		{
			this.TypeStart("ScalePin" + (isMaximum ? "MaximumPinHost" : "MinimumPinHost"), "ScalePinExprHost");
		}

		public void ScalePinEnd(bool isMaximum)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, isMaximum ? "MaximumPinHost" : "MinimumPinHost");
		}

		public void ScalePinLocation(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LocationExpr", expression);
		}

		public void ScalePinEnable(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EnableExpr", expression);
		}

		public void ScaleRangeStart(string name)
		{
			this.TypeStart(this.CreateTypeName("ScaleRange" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).ScaleRanges), "ScaleRangeExprHost");
		}

		public int ScaleRangeEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_scaleRangesHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).ScaleRanges);
		}

		public void ScaleRangeDistanceFromScale(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DistanceFromScaleExpr", expression);
		}

		public void ScaleRangeStartWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StartWidthExpr", expression);
		}

		public void ScaleRangeEndWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EndWidthExpr", expression);
		}

		public void ScaleRangeInRangeBarPointerColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InRangeBarPointerColorExpr", expression);
		}

		public void ScaleRangeInRangeLabelColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InRangeLabelColorExpr", expression);
		}

		public void ScaleRangeInRangeTickMarksColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InRangeTickMarksColorExpr", expression);
		}

		public void ScaleRangeBackgroundGradientType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BackgroundGradientTypeExpr", expression);
		}

		public void ScaleRangePlacement(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PlacementExpr", expression);
		}

		public void ScaleRangeToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void ScaleRangeHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		public void IndicatorImageStart()
		{
			this.TypeStart("IndicatorImage", "IndicatorImageExprHost");
		}

		public void IndicatorImageEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "IndicatorImageHost");
		}

		public void IndicatorImageHueColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HueColorExpr", expression);
		}

		public void IndicatorImageTransparency(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TransparencyExpr", expression);
		}

		public void StateIndicatorStart(string name)
		{
			this.TypeStart(this.CreateTypeName("StateIndicator" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).StateIndicators), "StateIndicatorExprHost");
		}

		public int StateIndicatorEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_stateIndicatorsHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).StateIndicators);
		}

		public void StateIndicatorIndicatorStyle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IndicatorStyleExpr", expression);
		}

		public void StateIndicatorScaleFactor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ScaleFactorExpr", expression);
		}

		public void StateIndicatorResizeMode(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ResizeModeExpr", expression);
		}

		public void StateIndicatorAngle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AngleExpr", expression);
		}

		public void StateIndicatorTransformationType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TransformationTypeExpr", expression);
		}

		public void ThermometerStart()
		{
			this.TypeStart("Thermometer", "ThermometerExprHost");
		}

		public void ThermometerEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "ThermometerHost");
		}

		public void ThermometerBulbOffset(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BulbOffsetExpr", expression);
		}

		public void ThermometerBulbSize(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BulbSizeExpr", expression);
		}

		public void ThermometerThermometerStyle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ThermometerStyleExpr", expression);
		}

		public void TickMarkStyleDistanceFromScale(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DistanceFromScaleExpr", expression);
		}

		public void TickMarkStylePlacement(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PlacementExpr", expression);
		}

		public void TickMarkStyleEnableGradient(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EnableGradientExpr", expression);
		}

		public void TickMarkStyleGradientDensity(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("GradientDensityExpr", expression);
		}

		public void TickMarkStyleLength(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LengthExpr", expression);
		}

		public void TickMarkStyleWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WidthExpr", expression);
		}

		public void TickMarkStyleShape(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShapeExpr", expression);
		}

		public void TickMarkStyleHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		public void IndicatorStateStart(string name)
		{
			this.TypeStart(this.CreateTypeName("IndicatorState" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).IndicatorStates), "IndicatorStateExprHost");
		}

		public int IndicatorStateEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_indicatorStatesHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).IndicatorStates);
		}

		public void IndicatorStateColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ColorExpr", expression);
		}

		public void IndicatorStateScaleFactor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ScaleFactorExpr", expression);
		}

		public void IndicatorStateIndicatorStyle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IndicatorStyleExpr", expression);
		}

		public void MapViewZoom(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ZoomExpr", expression);
		}

		public void MapElementViewStart()
		{
			this.TypeStart("MapElementView", "MapElementViewExprHost");
		}

		public void MapElementViewEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapViewHost");
		}

		public void MapElementViewLayerName(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LayerNameExpr", expression);
		}

		public void MapCustomViewStart()
		{
			this.TypeStart("MapCustomView", "MapCustomViewExprHost");
		}

		public void MapCustomViewEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapViewHost");
		}

		public void MapCustomViewCenterX(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CenterXExpr", expression);
		}

		public void MapCustomViewCenterY(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CenterYExpr", expression);
		}

		public void MapDataBoundViewStart()
		{
			this.TypeStart("MapDataBoundView", "MapDataBoundViewExprHost");
		}

		public void MapDataBoundViewEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapViewHost");
		}

		public void MapBorderSkinStart()
		{
			this.TypeStart("MapBorderSkin", "MapBorderSkinExprHost");
		}

		public void MapBorderSkinEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapBorderSkinHost");
		}

		public void MapBorderSkinMapBorderSkinType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MapBorderSkinTypeExpr", expression);
		}

		public void MapAntiAliasing(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AntiAliasingExpr", expression);
		}

		public void MapTextAntiAliasingQuality(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextAntiAliasingQualityExpr", expression);
		}

		public void MapShadowIntensity(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShadowIntensityExpr", expression);
		}

		public void MapTileLanguage(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TileLanguageExpr", expression);
		}

		public void MapVectorLayerMapDataRegionName(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MapDataRegionNameExpr", expression);
		}

		public void MapTileLayerStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapTileLayer" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).MapTileLayers), "MapTileLayerExprHost");
		}

		public int MapTileLayerEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapTileLayersHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapTileLayers);
		}

		public void MapTileLayerServiceUrl(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ServiceUrlExpr", expression);
		}

		public void MapTileLayerTileStyle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TileStyleExpr", expression);
		}

		public void MapTileLayerUseSecureConnection(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseSecureConnectionExpr", expression);
		}

		public void MapTileStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapTile" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).MapTiles), "MapTileExprHost");
		}

		public int MapTileEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapTilesHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapTiles);
		}

		public void MapPointLayerStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapPointLayer" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).MapPointLayers), "MapPointLayerExprHost");
		}

		public int MapPointLayerEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapPointLayersHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapPointLayers);
		}

		public void MapSpatialDataSetStart()
		{
			this.TypeStart("MapSpatialDataSet", "MapSpatialDataSetExprHost");
		}

		public void MapSpatialDataSetEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapSpatialDataHost");
		}

		public void MapSpatialDataSetDataSetName(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataSetNameExpr", expression);
		}

		public void MapSpatialDataSetSpatialField(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SpatialFieldExpr", expression);
		}

		public void MapSpatialDataRegionStart()
		{
			this.TypeStart("MapSpatialDataRegion", "MapSpatialDataRegionExprHost");
		}

		public void MapSpatialDataRegionEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapSpatialDataHost");
		}

		public void MapSpatialDataRegionVectorData(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("VectorDataExpr", expression);
		}

		public void MapPolygonLayerStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapPolygonLayer" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).MapPolygonLayers), "MapPolygonLayerExprHost");
		}

		public int MapPolygonLayerEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapPolygonLayersHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapPolygonLayers);
		}

		public void MapShapefileStart()
		{
			this.TypeStart("MapShapefile", "MapShapefileExprHost");
		}

		public void MapShapefileEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapSpatialDataHost");
		}

		public void MapShapefileSource(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SourceExpr", expression);
		}

		public void MapLineLayerStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapLineLayer" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).MapLineLayers), "MapLineLayerExprHost");
		}

		public int MapLineLayerEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapLineLayersHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapLineLayers);
		}

		public void MapLayerVisibilityMode(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("VisibilityModeExpr", expression);
		}

		public void MapLayerMinimumZoom(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinimumZoomExpr", expression);
		}

		public void MapLayerMaximumZoom(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaximumZoomExpr", expression);
		}

		public void MapLayerTransparency(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TransparencyExpr", expression);
		}

		public void MapFieldNameStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapFieldName" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).MapFieldNames), "MapFieldNameExprHost");
		}

		public int MapFieldNameEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapFieldNamesHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapFieldNames);
		}

		public void MapFieldNameName(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("NameExpr", expression);
		}

		public void MapPointStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapPoint" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).MapPoints), "MapPointExprHost");
		}

		public int MapPointEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapPointsHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapPoints);
		}

		public void MapPointUseCustomPointTemplate(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseCustomPointTemplateExpr", expression);
		}

		public void MapPolygonStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapPolygon" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).MapPolygons), "MapPolygonExprHost");
		}

		public int MapPolygonEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapPolygonsHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapPolygons);
		}

		public void MapPolygonUseCustomPolygonTemplate(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseCustomPolygonTemplateExpr", expression);
		}

		public void MapPolygonUseCustomCenterPointTemplate(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseCustomPointTemplateExpr", expression);
		}

		public void MapLineStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapLine" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).MapLines), "MapLineExprHost");
		}

		public int MapLineEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapLinesHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapLines);
		}

		public void MapLineUseCustomLineTemplate(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UseCustomLineTemplateExpr", expression);
		}

		public void MapSpatialElementTemplateHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		public void MapSpatialElementTemplateOffsetX(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OffsetXExpr", expression);
		}

		public void MapSpatialElementTemplateOffsetY(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("OffsetYExpr", expression);
		}

		public void MapSpatialElementTemplateLabel(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelExpr", expression);
		}

		public void MapSpatialElementTemplateDataElementLabel(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataElementLabelExpr", expression);
		}

		public void MapSpatialElementTemplateToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void MapPointTemplateSize(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SizeExpr", expression);
		}

		public void MapPointTemplateLabelPlacement(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelPlacementExpr", expression);
		}

		public void MapMarkerTemplateStart()
		{
			this.TypeStart("MapMarkerTemplate", "MapMarkerTemplateExprHost");
		}

		public void MapMarkerTemplateEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapPointTemplateHost");
		}

		public void MapPolygonTemplateStart()
		{
			this.TypeStart("MapPolygonTemplate", "MapPolygonTemplateExprHost");
		}

		public void MapPolygonTemplateEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapPolygonTemplateHost");
		}

		public void MapPolygonTemplateScaleFactor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ScaleFactorExpr", expression);
		}

		public void MapPolygonTemplateCenterPointOffsetX(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CenterPointOffsetXExpr", expression);
		}

		public void MapPolygonTemplateCenterPointOffsetY(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CenterPointOffsetYExpr", expression);
		}

		public void MapPolygonTemplateShowLabel(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShowLabelExpr", expression);
		}

		public void MapPolygonTemplateLabelPlacement(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelPlacementExpr", expression);
		}

		public void MapLineTemplateStart()
		{
			this.TypeStart("MapLineTemplate", "MapLineTemplateExprHost");
		}

		public void MapLineTemplateEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapLineTemplateHost");
		}

		public void MapLineTemplateWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WidthExpr", expression);
		}

		public void MapLineTemplateLabelPlacement(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelPlacementExpr", expression);
		}

		public void MapCustomColorRuleStart()
		{
			this.TypeStart("MapCustomColorRule", "MapCustomColorRuleExprHost");
		}

		public void MapCustomColorRuleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapColorRuleHost");
		}

		public void MapCustomColorStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapCustomColor" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).MapCustomColors), "MapCustomColorExprHost");
		}

		public int MapCustomColorEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapCustomColorsHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapCustomColors);
		}

		public void MapCustomColorColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ColorExpr", expression);
		}

		public void MapPointRulesStart()
		{
			this.TypeStart("MapPointRules", "MapPointRulesExprHost");
		}

		public void MapPointRulesEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapPointRulesHost");
		}

		public void MapMarkerRuleStart()
		{
			this.TypeStart("MapMarkerRule", "MapMarkerRuleExprHost");
		}

		public void MapMarkerRuleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapMarkerRuleHost");
		}

		public void MapMarkerStart()
		{
			this.TypeStart("MapMarker", "MapMarkerExprHost");
		}

		public void MapMarkerEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapMarkerHost");
		}

		public void MapMarkerInCollectionStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapMarker" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).MapMarkers), "MapMarkerExprHost");
		}

		public int MapMarkerInCollectionEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapMarkersHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapMarkers);
		}

		public void MapMarkerMapMarkerStyle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MapMarkerStyleExpr", expression);
		}

		public void MapMarkerImageStart()
		{
			this.TypeStart("MapMarkerImage", "MapMarkerImageExprHost");
		}

		public void MapMarkerImageEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapMarkerImageHost");
		}

		public void MapMarkerImageSource(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SourceExpr", expression);
		}

		public void MapMarkerImageValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		public void MapMarkerImageMIMEType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MIMETypeExpr", expression);
		}

		public void MapMarkerImageTransparentColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TransparentColorExpr", expression);
		}

		public void MapMarkerImageResizeMode(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ResizeModeExpr", expression);
		}

		public void MapSizeRuleStart()
		{
			this.TypeStart("MapSizeRule", "MapSizeRuleExprHost");
		}

		public void MapSizeRuleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapSizeRuleHost");
		}

		public void MapSizeRuleStartSize(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StartSizeExpr", expression);
		}

		public void MapSizeRuleEndSize(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EndSizeExpr", expression);
		}

		public void MapPolygonRulesStart()
		{
			this.TypeStart("MapPolygonRules", "MapPolygonRulesExprHost");
		}

		public void MapPolygonRulesEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapPolygonRulesHost");
		}

		public void MapLineRulesStart()
		{
			this.TypeStart("MapLineRules", "MapLineRulesExprHost");
		}

		public void MapLineRulesEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapLineRulesHost");
		}

		public void MapColorRuleShowInColorScale(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShowInColorScaleExpr", expression);
		}

		public void MapColorRangeRuleStart()
		{
			this.TypeStart("MapColorRangeRule", "MapColorRangeRuleExprHost");
		}

		public void MapColorRangeRuleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapColorRuleHost");
		}

		public void MapColorRangeRuleStartColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StartColorExpr", expression);
		}

		public void MapColorRangeRuleMiddleColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MiddleColorExpr", expression);
		}

		public void MapColorRangeRuleEndColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EndColorExpr", expression);
		}

		public void MapColorPaletteRuleStart()
		{
			this.TypeStart("MapColorPaletteRule", "MapColorPaletteRuleExprHost");
		}

		public void MapColorPaletteRuleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapColorRuleHost");
		}

		public void MapColorPaletteRulePalette(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PaletteExpr", expression);
		}

		public void MapBucketStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapBucket" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).MapBuckets), "MapBucketExprHost");
		}

		public int MapBucketEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapBucketsHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapBuckets);
		}

		public void MapBucketStartValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StartValueExpr", expression);
		}

		public void MapBucketEndValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EndValueExpr", expression);
		}

		public void MapAppearanceRuleDataValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DataValueExpr", expression);
		}

		public void MapAppearanceRuleDistributionType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DistributionTypeExpr", expression);
		}

		public void MapAppearanceRuleBucketCount(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BucketCountExpr", expression);
		}

		public void MapAppearanceRuleStartValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("StartValueExpr", expression);
		}

		public void MapAppearanceRuleEndValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EndValueExpr", expression);
		}

		public void MapAppearanceRuleLegendText(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LegendTextExpr", expression);
		}

		public void MapLegendTitleStart()
		{
			this.TypeStart("MapLegendTitle", "MapLegendTitleExprHost");
		}

		public void MapLegendTitleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapLegendTitleHost");
		}

		public void MapLegendTitleCaption(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CaptionExpr", expression);
		}

		public void MapLegendTitleTitleSeparator(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TitleSeparatorExpr", expression);
		}

		public void MapLegendTitleTitleSeparatorColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TitleSeparatorColorExpr", expression);
		}

		public void MapLegendStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapLegend" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).MapLegends), "MapLegendExprHost");
		}

		public int MapLegendEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapLegendsHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapLegends);
		}

		public void MapLegendLayout(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LayoutExpr", expression);
		}

		public void MapLegendAutoFitTextDisabled(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AutoFitTextDisabledExpr", expression);
		}

		public void MapLegendMinFontSize(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinFontSizeExpr", expression);
		}

		public void MapLegendInterlacedRows(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InterlacedRowsExpr", expression);
		}

		public void MapLegendInterlacedRowsColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("InterlacedRowsColorExpr", expression);
		}

		public void MapLegendEquallySpacedItems(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("EquallySpacedItemsExpr", expression);
		}

		public void MapLegendTextWrapThreshold(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextWrapThresholdExpr", expression);
		}

		public void MapTitleStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapTitle" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).MapTitles), "MapTitleExprHost");
		}

		public int MapTitleEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapTitlesHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapTitles);
		}

		public void MapTitleText(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextExpr", expression);
		}

		public void MapTitleAngle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("AngleExpr", expression);
		}

		public void MapTitleTextShadowOffset(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TextShadowOffsetExpr", expression);
		}

		public void MapDistanceScaleStart()
		{
			this.TypeStart("MapDistanceScale", "MapDistanceScaleExprHost");
		}

		public void MapDistanceScaleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapDistanceScaleHost");
		}

		public void MapDistanceScaleScaleColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ScaleColorExpr", expression);
		}

		public void MapDistanceScaleScaleBorderColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ScaleBorderColorExpr", expression);
		}

		public void MapColorScaleTitleStart()
		{
			this.TypeStart("MapColorScaleTitle", "MapColorScaleTitleExprHost");
		}

		public void MapColorScaleTitleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapColorScaleTitleHost");
		}

		public void MapColorScaleTitleCaption(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("CaptionExpr", expression);
		}

		public void MapColorScaleStart()
		{
			this.TypeStart("MapColorScale", "MapColorScaleExprHost");
		}

		public void MapColorScaleEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapColorScaleHost");
		}

		public void MapColorScaleTickMarkLength(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TickMarkLengthExpr", expression);
		}

		public void MapColorScaleColorBarBorderColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ColorBarBorderColorExpr", expression);
		}

		public void MapColorScaleLabelInterval(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelIntervalExpr", expression);
		}

		public void MapColorScaleLabelFormat(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelFormatExpr", expression);
		}

		public void MapColorScaleLabelPlacement(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelPlacementExpr", expression);
		}

		public void MapColorScaleLabelBehavior(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelBehaviorExpr", expression);
		}

		public void MapColorScaleHideEndLabels(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HideEndLabelsExpr", expression);
		}

		public void MapColorScaleRangeGapColor(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RangeGapColorExpr", expression);
		}

		public void MapColorScaleNoDataText(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("NoDataTextExpr", expression);
		}

		public void MapStart(string name)
		{
			this.TypeStart(name, "MapExprHost");
		}

		public int MapEnd()
		{
			return this.ReportItemEnd("m_mapHostsRemotable", ref this.m_rootTypeDecl.Maps);
		}

		public void MapLocationStart()
		{
			this.TypeStart("MapLocation", "MapLocationExprHost");
		}

		public void MapLocationEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapLocationHost");
		}

		public void MapLocationLeft(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LeftExpr", expression);
		}

		public void MapLocationTop(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TopExpr", expression);
		}

		public void MapLocationUnit(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UnitExpr", expression);
		}

		public void MapSizeStart()
		{
			this.TypeStart("MapSize", "MapSizeExprHost");
		}

		public void MapSizeEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapSizeHost");
		}

		public void MapSizeWidth(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("WidthExpr", expression);
		}

		public void MapSizeHeight(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HeightExpr", expression);
		}

		public void MapSizeUnit(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("UnitExpr", expression);
		}

		public void MapGridLinesStart(bool isMeridian)
		{
			this.TypeStart("MapGridLines" + (isMeridian ? "MapMeridiansHost" : "MapParallelsHost"), "MapGridLinesExprHost");
		}

		public void MapGridLinesEnd(bool isMeridian)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, isMeridian ? "MapMeridiansHost" : "MapParallelsHost");
		}

		public void MapGridLinesHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		public void MapGridLinesInterval(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("IntervalExpr", expression);
		}

		public void MapGridLinesShowLabels(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ShowLabelsExpr", expression);
		}

		public void MapGridLinesLabelPosition(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LabelPositionExpr", expression);
		}

		public void MapDockableSubItemPosition(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PositionExpr", expression);
		}

		public void MapDockableSubItemDockOutsideViewport(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DockOutsideViewportExpr", expression);
		}

		public void MapDockableSubItemHidden(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HiddenExpr", expression);
		}

		public void MapDockableSubItemToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void MapSubItemLeftMargin(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LeftMarginExpr", expression);
		}

		public void MapSubItemRightMargin(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RightMarginExpr", expression);
		}

		public void MapSubItemTopMargin(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("TopMarginExpr", expression);
		}

		public void MapSubItemBottomMargin(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BottomMarginExpr", expression);
		}

		public void MapSubItemZIndex(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ZIndexExpr", expression);
		}

		public void MapBindingFieldPairStart(string name)
		{
			this.TypeStart(this.CreateTypeName("MapBindingFieldPair" + name, ((NonRootTypeDecl)this.m_currentTypeDecl).MapBindingFieldPairs), "MapBindingFieldPairExprHost");
		}

		public int MapBindingFieldPairEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_mapBindingFieldPairsHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).MapBindingFieldPairs);
		}

		public void MapBindingFieldPairFieldName(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("FieldNameExpr", expression);
		}

		public void MapBindingFieldPairBindingExpression(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("BindingExpressionExpr", expression);
		}

		public void MapViewportStart()
		{
			this.TypeStart("MapViewport", "MapViewportExprHost");
		}

		public void MapViewportEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapViewportHost");
		}

		public void MapViewportSimplificationResolution(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SimplificationResolutionExpr", expression);
		}

		public void MapViewportMapCoordinateSystem(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MapCoordinateSystemExpr", expression);
		}

		public void MapViewportMapProjection(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MapProjectionExpr", expression);
		}

		public void MapViewportProjectionCenterX(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ProjectionCenterXExpr", expression);
		}

		public void MapViewportProjectionCenterY(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ProjectionCenterYExpr", expression);
		}

		public void MapViewportMaximumZoom(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaximumZoomExpr", expression);
		}

		public void MapViewportMinimumZoom(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinimumZoomExpr", expression);
		}

		public void MapViewportContentMargin(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ContentMarginExpr", expression);
		}

		public void MapViewportGridUnderContent(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("GridUnderContentExpr", expression);
		}

		public void MapLimitsStart()
		{
			this.TypeStart("MapLimits", "MapLimitsExprHost");
		}

		public void MapLimitsEnd()
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, "MapLimitsHost");
		}

		public void MapLimitsMinimumX(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinimumXExpr", expression);
		}

		public void MapLimitsMinimumY(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MinimumYExpr", expression);
		}

		public void MapLimitsMaximumX(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaximumXExpr", expression);
		}

		public void MapLimitsMaximumY(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MaximumYExpr", expression);
		}

		public void MapLimitsLimitToData(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LimitToDataExpr", expression);
		}

		public void ParagraphStart(int index)
		{
			this.TypeStart(this.CreateTypeName("Paragraph" + index.ToString(CultureInfo.InvariantCulture), ((NonRootTypeDecl)this.m_currentTypeDecl).Paragraphs), "ParagraphExprHost");
		}

		public int ParagraphEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_paragraphHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).Paragraphs);
		}

		public void ParagraphLeftIndent(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("LeftIndentExpr", expression);
		}

		public void ParagraphRightIndent(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("RightIndentExpr", expression);
		}

		public void ParagraphHangingIndent(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("HangingIndentExpr", expression);
		}

		public void ParagraphSpaceBefore(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SpaceBeforeExpr", expression);
		}

		public void ParagraphSpaceAfter(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SpaceAfterExpr", expression);
		}

		public void ParagraphListStyle(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ListStyleExpr", expression);
		}

		public void ParagraphListLevel(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ListLevelExpr", expression);
		}

		public void TextRunStart(int index)
		{
			this.TypeStart(this.CreateTypeName("TextRun" + index.ToString(CultureInfo.InvariantCulture), ((NonRootTypeDecl)this.m_currentTypeDecl).TextRuns), "TextRunExprHost");
		}

		public int TextRunEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_textRunHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).TextRuns);
		}

		public void TextRunToolTip(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ToolTipExpr", expression);
		}

		public void TextRunValue(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ValueExpr", expression);
		}

		public void TextRunMarkupType(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("MarkupTypeExpr", expression);
		}

		public void LookupStart()
		{
			this.TypeStart(this.CreateTypeName("Lookup", this.m_rootTypeDecl.Lookups), "LookupExprHost");
		}

		public void LookupSourceExpr(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("SourceExpr", expression);
		}

		public void LookupResultExpr(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ResultExpr", expression);
		}

		public int LookupEnd()
		{
			return this.TypeEnd((TypeDecl)this.m_rootTypeDecl, "m_lookupExprHostsRemotable", ref this.m_rootTypeDecl.Lookups);
		}

		public void LookupDestStart()
		{
			this.TypeStart(this.CreateTypeName("LookupDest", this.m_rootTypeDecl.LookupDests), "LookupDestExprHost");
		}

		public void LookupDestExpr(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DestExpr", expression);
		}

		public int LookupDestEnd()
		{
			return this.TypeEnd((TypeDecl)this.m_rootTypeDecl, "m_lookupDestExprHostsRemotable", ref this.m_rootTypeDecl.LookupDests);
		}

		public void PageBreakStart()
		{
			this.TypeStart("PageBreak", "PageBreakExprHost");
		}

		public bool PageBreakEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "PageBreakExprHost");
		}

		public void Disabled(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("DisabledExpr", expression);
		}

		public void PageName(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PageNameExpr", expression);
		}

		public void ResetPageNumber(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ResetPageNumberExpr", expression);
		}

		public void JoinConditionStart()
		{
			this.TypeStart(this.CreateTypeName("JoinCondition", ((NonRootTypeDecl)this.m_currentTypeDecl).JoinConditions), "JoinConditionExprHost");
		}

		public void JoinConditionForeignKeyExpr(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("ForeignKeyExpr", expression);
		}

		public void JoinConditionPrimaryKeyExpr(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			this.ExpressionAdd("PrimaryKeyExpr", expression);
		}

		public int JoinConditionEnd()
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, "m_joinConditionExprHostsRemotable", ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).JoinConditions);
		}

		private void TypeStart(string typeName, string baseType)
		{
			this.m_currentTypeDecl = new NonRootTypeDecl(typeName, baseType, this.m_currentTypeDecl, this.m_setCode);
		}

		private int TypeEnd(TypeDecl container, string name, ref CodeExpressionCollection initializers)
		{
			int result = -1;
			if (this.m_currentTypeDecl.HasExpressions)
			{
				result = container.NestedTypeColAdd(name, this.m_currentTypeDecl.BaseTypeName, ref initializers, this.m_currentTypeDecl.Type);
			}
			this.TypeEnd(container);
			return result;
		}

		private bool TypeEnd(TypeDecl container, string name)
		{
			bool hasExpressions = this.m_currentTypeDecl.HasExpressions;
			if (hasExpressions)
			{
				container.NestedTypeAdd(name, this.m_currentTypeDecl.Type);
			}
			this.TypeEnd(container);
			return hasExpressions;
		}

		private void TypeEnd(TypeDecl container)
		{
			Global.Tracer.Assert(this.m_currentTypeDecl.Parent != null && container != null, "(m_currentTypeDecl.Parent != null && container != null)");
			container.HasExpressions |= this.m_currentTypeDecl.HasExpressions;
			this.m_currentTypeDecl = this.m_currentTypeDecl.Parent;
		}

		private int ReportItemEnd(string name, ref CodeExpressionCollection initializers)
		{
			return this.TypeEnd((TypeDecl)this.m_rootTypeDecl, name, ref initializers);
		}

		private void ParameterStart()
		{
			this.TypeStart(this.CreateTypeName("Parameter", ((NonRootTypeDecl)this.m_currentTypeDecl).Parameters), "ParamExprHost");
		}

		private int ParameterEnd(string propName)
		{
			return this.TypeEnd(this.m_currentTypeDecl.Parent, propName, ref ((NonRootTypeDecl)this.m_currentTypeDecl.Parent).Parameters);
		}

		private void StyleStart(string typeName)
		{
			this.TypeStart(typeName, "StyleExprHost");
		}

		private void StyleEnd(string propName)
		{
			this.TypeEnd(this.m_currentTypeDecl.Parent, propName);
		}

		private void AggregateStart()
		{
			this.TypeStart(this.CreateTypeName("Aggregate", this.m_rootTypeDecl.Aggregates), "AggregateParamExprHost");
		}

		private int AggregateEnd()
		{
			return this.TypeEnd((TypeDecl)this.m_rootTypeDecl, "m_aggregateParamHostsRemotable", ref this.m_rootTypeDecl.Aggregates);
		}

		private string CreateTypeName(string template, CodeExpressionCollection initializers)
		{
			return template + ((initializers == null) ? "0" : initializers.Count.ToString(CultureInfo.InvariantCulture));
		}

		private void ExprIndexerCreate()
		{
			NonRootTypeDecl nonRootTypeDecl = (NonRootTypeDecl)this.m_currentTypeDecl;
			if (nonRootTypeDecl.IndexedExpressions != null)
			{
				Global.Tracer.Assert(nonRootTypeDecl.IndexedExpressions.Count > 0, "(currentTypeDecl.IndexedExpressions.Count > 0)");
				CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
				codeMemberProperty.Name = "Item";
				codeMemberProperty.Attributes = (MemberAttributes)24580;
				codeMemberProperty.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "index"));
				codeMemberProperty.Type = new CodeTypeReference(typeof(object));
				nonRootTypeDecl.Type.Members.Add(codeMemberProperty);
				int count = nonRootTypeDecl.IndexedExpressions.Count;
				if (count == 1)
				{
					codeMemberProperty.GetStatements.Add(nonRootTypeDecl.IndexedExpressions[0]);
				}
				else
				{
					codeMemberProperty.GetStatements.Add(this.ExprIndexerTree(nonRootTypeDecl.IndexedExpressions, 0, count - 1));
				}
			}
		}

		private CodeStatement ExprIndexerTree(ReturnStatementList indexedExpressions, int leftIndex, int rightIndex)
		{
			if (leftIndex == rightIndex)
			{
				return indexedExpressions[leftIndex];
			}
			int num = rightIndex - leftIndex >> 1;
			CodeConditionStatement codeConditionStatement = new CodeConditionStatement();
			codeConditionStatement.Condition = new CodeBinaryOperatorExpression(new CodeArgumentReferenceExpression("index"), CodeBinaryOperatorType.LessThanOrEqual, new CodePrimitiveExpression(leftIndex + num));
			codeConditionStatement.TrueStatements.Add(this.ExprIndexerTree(indexedExpressions, leftIndex, leftIndex + num));
			codeConditionStatement.FalseStatements.Add(this.ExprIndexerTree(indexedExpressions, leftIndex + num + 1, rightIndex));
			return codeConditionStatement;
		}

		private void IndexedExpressionAdd(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			if (expression.Type == AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Expression)
			{
				NonRootTypeDecl nonRootTypeDecl = (NonRootTypeDecl)this.m_currentTypeDecl;
				if (nonRootTypeDecl.IndexedExpressions == null)
				{
					nonRootTypeDecl.IndexedExpressions = new ReturnStatementList();
				}
				nonRootTypeDecl.HasExpressions = true;
				expression.ExprHostID = nonRootTypeDecl.IndexedExpressions.Add(this.CreateExprReturnStatement(expression));
			}
		}

		private void ExpressionAdd(string name, AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			if (expression.Type == AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Expression)
			{
				CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
				codeMemberProperty.Name = name;
				codeMemberProperty.Type = new CodeTypeReference(typeof(object));
				codeMemberProperty.Attributes = (MemberAttributes)24580;
				codeMemberProperty.GetStatements.Add(this.CreateExprReturnStatement(expression));
				this.m_currentTypeDecl.Type.Members.Add(codeMemberProperty);
				this.m_currentTypeDecl.HasExpressions = true;
			}
		}

		private CodeMethodReturnStatement CreateExprReturnStatement(AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression)
		{
			CodeMethodReturnStatement codeMethodReturnStatement = new CodeMethodReturnStatement(new CodeSnippetExpression(expression.TransformedExpression));
			codeMethodReturnStatement.LinePragma = new CodeLinePragma("Expr" + expression.CompileTimeID.ToString(CultureInfo.InvariantCulture) + "end", 0);
			return codeMethodReturnStatement;
		}
	}
}
