using AspNetCore.Reporting.Chart.WebForms.ChartTypes;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;

namespace AspNetCore.Reporting.Chart.WebForms.Utilities
{
	internal class CustomAttributeRegistry : IServiceProvider
	{
		private IServiceContainer serviceContainer;

		internal ArrayList registeredCustomAttributes = new ArrayList();

		private CustomAttributeRegistry()
		{
		}

		public CustomAttributeRegistry(IServiceContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException(SR.ExceptionInvalidServiceContainer);
			}
			this.serviceContainer = container;
			this.RegisterAttributes();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public object GetService(Type serviceType)
		{
			if (serviceType == typeof(CustomAttributeRegistry))
			{
				return this;
			}
			throw new ArgumentException(SR.ExceptionCustomAttributesRegistryUnsupportedType(serviceType.ToString()));
		}

		private void RegisterAttributes()
		{
			SeriesChartType[] array = null;
			CustomAttributeInfo customAttributeInfo = null;
			array = new SeriesChartType[1]
			{
				SeriesChartType.Column
			};
			this.registeredCustomAttributes.Add(new CustomAttributeInfo("ShowColumnAs", typeof(ColumnChartExtensions), "Normal", SR.DescriptionCustomAttributeColumnChartShowColumnAs, array, true, false));
			this.registeredCustomAttributes.Add(new CustomAttributeInfo("HistogramSegmentIntervalNumber", typeof(int), 20, SR.DescriptionCustomAttributeHistogramSegmentIntervalNumber, array, true, false));
			this.registeredCustomAttributes.Add(new CustomAttributeInfo("HistogramSegmentIntervalWidth", typeof(double), 0.0, SR.DescriptionCustomAttributeHistogramSegmentIntervalWidth, array, true, false));
			this.registeredCustomAttributes.Add(new CustomAttributeInfo("HistogramShowPercentOnSecondaryYAxis", typeof(bool), true, SR.DescriptionCustomAttributeHistogramShowPercentOnSecondaryYAxis, array, true, false));
			array = new SeriesChartType[6]
			{
				SeriesChartType.Bar,
				SeriesChartType.Column,
				SeriesChartType.RangeColumn,
				SeriesChartType.BoxPlot,
				SeriesChartType.Gantt,
				SeriesChartType.ErrorBar
			};
			customAttributeInfo = new CustomAttributeInfo("DrawSideBySide", typeof(AxisEnabled), "Auto", SR.DescriptionCustomAttributeDrawSideBySide, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[15]
			{
				SeriesChartType.Point,
				SeriesChartType.Bubble,
				SeriesChartType.Line,
				SeriesChartType.Spline,
				SeriesChartType.StepLine,
				SeriesChartType.Column,
				SeriesChartType.RangeColumn,
				SeriesChartType.Gantt,
				SeriesChartType.Radar,
				SeriesChartType.Range,
				SeriesChartType.SplineRange,
				SeriesChartType.Polar,
				SeriesChartType.Area,
				SeriesChartType.SplineArea,
				SeriesChartType.Bar
			};
			this.registeredCustomAttributes.Add(new CustomAttributeInfo("EmptyPointValue", typeof(EmptyPointTypes), "Average", SR.DescriptionCustomAttributeEmptyPointValue, array, true, false));
			array = new SeriesChartType[3]
			{
				SeriesChartType.StackedBar,
				SeriesChartType.StackedBar100,
				SeriesChartType.Gantt
			};
			customAttributeInfo = new CustomAttributeInfo("BarLabelStyle", typeof(BarValueLabelDrawingStyle), "Center", SR.DescriptionCustomAttributeBarLabelStyle, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[4]
			{
				SeriesChartType.StackedBar,
				SeriesChartType.StackedBar100,
				SeriesChartType.StackedColumn,
				SeriesChartType.StackedColumn100
			};
			customAttributeInfo = new CustomAttributeInfo("StackedGroupName", typeof(string), string.Empty, SR.DescriptionCustomAttributeStackedGroupName, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[1]
			{
				SeriesChartType.Bar
			};
			customAttributeInfo = new CustomAttributeInfo("BarLabelStyle", typeof(BarValueLabelDrawingStyle), "Outside", SR.DescriptionCustomAttributeBarLabelStyle, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[8]
			{
				SeriesChartType.Bar,
				SeriesChartType.Column,
				SeriesChartType.StackedBar,
				SeriesChartType.StackedBar100,
				SeriesChartType.StackedColumn,
				SeriesChartType.StackedColumn100,
				SeriesChartType.Gantt,
				SeriesChartType.RangeColumn
			};
			customAttributeInfo = new CustomAttributeInfo("DrawingStyle", typeof(BarDrawingStyle), "Default", SR.DescriptionCustomAttributeDrawingStyle, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[12]
			{
				SeriesChartType.Bar,
				SeriesChartType.Candlestick,
				SeriesChartType.Column,
				SeriesChartType.StackedBar,
				SeriesChartType.StackedBar100,
				SeriesChartType.StackedColumn,
				SeriesChartType.StackedColumn100,
				SeriesChartType.Stock,
				SeriesChartType.BoxPlot,
				SeriesChartType.ErrorBar,
				SeriesChartType.Gantt,
				SeriesChartType.RangeColumn
			};
			customAttributeInfo = new CustomAttributeInfo("PointWidth", typeof(float), 0.8f, SR.DescriptionCustomAttributePointWidth, array, true, false);
			customAttributeInfo.MinValue = 0f;
			customAttributeInfo.MaxValue = 2f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("PixelPointWidth", typeof(int), 0, SR.DescriptionCustomAttributePixelPointWidth, array, true, false);
			customAttributeInfo.MinValue = 0;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("MinPixelPointWidth", typeof(int), 0, SR.DescriptionCustomAttributeMinPixelPointWidth, array, true, false);
			customAttributeInfo.MinValue = 0;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("MaxPixelPointWidth", typeof(int), 0, SR.DescriptionCustomAttributeMaxPixelPointWidth, array, true, false);
			customAttributeInfo.MinValue = 0;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[1]
			{
				SeriesChartType.Candlestick
			};
			customAttributeInfo = new CustomAttributeInfo("PriceUpColor", typeof(Color), "", SR.DescriptionCustomAttributeCandlePriceUpColor, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("PriceDownColor", typeof(Color), "", SR.DescriptionCustomAttributePriceDownColor, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[2]
			{
				SeriesChartType.Stock,
				SeriesChartType.Candlestick
			};
			customAttributeInfo = new CustomAttributeInfo("LabelValueType", typeof(StockLabelValueTypes), "Close", SR.DescriptionCustomAttributeLabelValueType, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[1]
			{
				SeriesChartType.Stock
			};
			customAttributeInfo = new CustomAttributeInfo("OpenCloseStyle", typeof(StockOpenCloseMarkStyle), "Line", SR.DescriptionCustomAttributeOpenCloseStyle, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("ShowOpenClose", typeof(StockShowOpenCloseTypes), "Both", SR.DescriptionCustomAttributeShowOpenClose, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[1]
			{
				SeriesChartType.Bubble
			};
			customAttributeInfo = new CustomAttributeInfo("BubbleScaleMin", typeof(float), 0f, SR.DescriptionCustomAttributeBubbleScaleMin, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("BubbleScaleMax", typeof(float), 0f, SR.DescriptionCustomAttributeBubbleScaleMax, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("BubbleMaxSize", typeof(float), 15f, SR.DescriptionCustomAttributeBubbleMaxSize, array, true, false);
			customAttributeInfo.MinValue = 0f;
			customAttributeInfo.MaxValue = 100f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("BubbleMinSize", typeof(float), 3f, SR.DescriptionCustomAttributeBubbleMaxSize, array, true, false);
			customAttributeInfo.MinValue = 0f;
			customAttributeInfo.MaxValue = 100f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("BubbleUseSizeForLabel", typeof(bool), false, SR.DescriptionCustomAttributeBubbleUseSizeForLabel, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[2]
			{
				SeriesChartType.Pie,
				SeriesChartType.Doughnut
			};
			customAttributeInfo = new CustomAttributeInfo("PieDrawingStyle", typeof(PieDrawingStyle), "Default", SR.DescriptionCustomAttributePieDrawingStyle, array, true, false);
			customAttributeInfo.AppliesTo3D = false;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("CollectedStyle", typeof(CollectedPieStyle), "None", SR.DescriptionCustomAttributeCollectedStyle, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("CollectedThreshold", typeof(double), 5.0, SR.DescriptionCustomAttributeCollectedThreshold, array, true, false);
			customAttributeInfo.MinValue = 0.0;
			customAttributeInfo.MaxValue = 1.7976931348623157E+308;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("CollectedThresholdUsePercent", typeof(bool), true, SR.DescriptionCustomAttributeCollectedThresholdUsePercent, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("CollectedSliceExploded", typeof(bool), false, SR.DescriptionCustomAttributeCollectedSliceExploded, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("CollectedLabel", typeof(string), SR.DescriptionCustomAttributeCollectedLabelDefaultText, SR.DescriptionCustomAttributeCollectedLabel, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("CollectedLegendText", typeof(string), SR.DescriptionCustomAttributeCollectedLegendDefaultText, SR.DescriptionCustomAttributeCollectedLegendText, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("CollectedToolTip", typeof(string), string.Empty, SR.DescriptionCustomAttributeCollectedToolTip, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("CollectedColor", typeof(Color), "", SR.DescriptionCustomAttributeCollectedColor, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			this.registeredCustomAttributes.Add(new CustomAttributeInfo("CollectedChartShowLegend", typeof(bool), false, SR.DescriptionCustomAttributeShowCollectedLegend, array, true, false));
			this.registeredCustomAttributes.Add(new CustomAttributeInfo("CollectedChartShowLabels", typeof(bool), false, SR.DescriptionCustomAttributeShowCollectedPointLabels, array, true, false));
			customAttributeInfo = new CustomAttributeInfo("PieStartAngle", typeof(int), 0, SR.DescriptionCustomAttributePieStartAngle, array, true, false);
			customAttributeInfo.MinValue = 0;
			customAttributeInfo.MaxValue = 360;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			this.registeredCustomAttributes.Add(new CustomAttributeInfo("Exploded", typeof(bool), false, SR.DescriptionCustomAttributePieDonutExploded, array, false, true));
			customAttributeInfo = new CustomAttributeInfo("LabelsRadialLineSize", typeof(float), 1f, SR.DescriptionCustomAttributeLabelsRadialLineSize, array, true, true);
			customAttributeInfo.AppliesTo3D = false;
			customAttributeInfo.MinValue = 0f;
			customAttributeInfo.MaxValue = 100f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("LabelsHorizontalLineSize", typeof(float), 1f, SR.DescriptionCustomAttributeLabelsHorizontalLineSize, array, true, true);
			customAttributeInfo.AppliesTo3D = false;
			customAttributeInfo.MinValue = 0f;
			customAttributeInfo.MaxValue = 100f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			this.registeredCustomAttributes.Add(new CustomAttributeInfo("PieLabelStyle", typeof(PieLabelStyle), "Inside", SR.DescriptionCustomAttributePieLabelStyle, array, true, true));
			customAttributeInfo = new CustomAttributeInfo("MinimumRelativePieSize", typeof(float), 30f, SR.DescriptionCustomAttributeMinimumRelativePieSize, array, true, false);
			customAttributeInfo.MinValue = 10f;
			customAttributeInfo.MaxValue = 70f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("3DLabelLineSize", typeof(float), 100f, SR.DescriptionCustomAttribute_3DLabelLineSize, array, true, false);
			customAttributeInfo.AppliesTo2D = false;
			customAttributeInfo.MinValue = 30f;
			customAttributeInfo.MaxValue = 200f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("PieLineColor", typeof(Color), "", SR.DescriptionCustomAttributePieLineColor, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("DoughnutRadius", typeof(float), 60f, SR.DescriptionCustomAttributeDoughnutRadius, new SeriesChartType[1]
			{
				SeriesChartType.Doughnut
			}, true, false);
			customAttributeInfo.MinValue = 0f;
			customAttributeInfo.MaxValue = 99f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[12]
			{
				SeriesChartType.Point,
				SeriesChartType.Column,
				SeriesChartType.Bubble,
				SeriesChartType.Line,
				SeriesChartType.Spline,
				SeriesChartType.StepLine,
				SeriesChartType.Area,
				SeriesChartType.SplineArea,
				SeriesChartType.Range,
				SeriesChartType.SplineRange,
				SeriesChartType.Radar,
				SeriesChartType.Polar
			};
			this.registeredCustomAttributes.Add(new CustomAttributeInfo("LabelStyle", typeof(LabelAlignments), "Auto", SR.DescriptionCustomAttributeLabelStyle, array, true, true));
			array = new SeriesChartType[7]
			{
				SeriesChartType.Line,
				SeriesChartType.Spline,
				SeriesChartType.StepLine,
				SeriesChartType.Area,
				SeriesChartType.SplineArea,
				SeriesChartType.Range,
				SeriesChartType.SplineRange
			};
			customAttributeInfo = new CustomAttributeInfo("ShowMarkerLines", typeof(bool), false, SR.DescriptionCustomAttributeShowMarkerLines, array, true, true);
			customAttributeInfo.AppliesTo2D = false;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[3]
			{
				SeriesChartType.Spline,
				SeriesChartType.SplineArea,
				SeriesChartType.SplineRange
			};
			customAttributeInfo = new CustomAttributeInfo("LineTension", typeof(float), 0.5f, SR.DescriptionCustomAttributeLineTension, array, true, false);
			customAttributeInfo.MinValue = 0f;
			customAttributeInfo.MaxValue = 2f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[28]
			{
				SeriesChartType.Area,
				SeriesChartType.Bar,
				SeriesChartType.Bubble,
				SeriesChartType.Candlestick,
				SeriesChartType.Column,
				SeriesChartType.Line,
				SeriesChartType.Point,
				SeriesChartType.Spline,
				SeriesChartType.SplineArea,
				SeriesChartType.StackedArea,
				SeriesChartType.StackedArea100,
				SeriesChartType.StackedBar,
				SeriesChartType.StackedBar100,
				SeriesChartType.StackedColumn,
				SeriesChartType.StackedColumn100,
				SeriesChartType.StepLine,
				SeriesChartType.Stock,
				SeriesChartType.ThreeLineBreak,
				SeriesChartType.BoxPlot,
				SeriesChartType.ErrorBar,
				SeriesChartType.Gantt,
				SeriesChartType.Kagi,
				SeriesChartType.PointAndFigure,
				SeriesChartType.Range,
				SeriesChartType.RangeColumn,
				SeriesChartType.Renko,
				SeriesChartType.SplineRange,
				SeriesChartType.FastLine
			};
			customAttributeInfo = new CustomAttributeInfo("PixelPointDepth", typeof(int), 0, SR.DescriptionCustomAttributePixelPointDepth, array, true, false);
			customAttributeInfo.MinValue = 0;
			customAttributeInfo.AppliesTo2D = false;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("PixelPointGapDepth", typeof(int), 0, SR.DescriptionCustomAttributePixelPointGapDepth, array, true, false);
			customAttributeInfo.MinValue = 0;
			customAttributeInfo.AppliesTo2D = false;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[2]
			{
				SeriesChartType.FastLine,
				SeriesChartType.FastPoint
			};
			array = new SeriesChartType[1]
			{
				SeriesChartType.Polar
			};
			customAttributeInfo = new CustomAttributeInfo("AreaDrawingStyle", typeof(CircularAreaDrawingStyles), "Circle", SR.DescriptionCustomAttributePolarAreaDrawingStyle, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("CircularLabelsStyle", typeof(CircularAxisLabelsStyle), "Auto", SR.DescriptionCustomAttributePolarCircularLabelsStyle, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("PolarDrawingStyle", typeof(PolarDrawingStyles), "Line", SR.DescriptionCustomAttributePolarDrawingStyle, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[1]
			{
				SeriesChartType.Radar
			};
			customAttributeInfo = new CustomAttributeInfo("AreaDrawingStyle", typeof(CircularAreaDrawingStyles), "Circle", SR.DescriptionCustomAttributeRadarAreaDrawingStyle, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("CircularLabelsStyle", typeof(CircularAxisLabelsStyle), "Auto", SR.DescriptionCustomAttributeRadarCircularLabelsStyle, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("RadarDrawingStyle", typeof(RadarDrawingStyle), "Area", SR.DescriptionCustomAttributeRadarDrawingStyle, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[1]
			{
				SeriesChartType.BoxPlot
			};
			customAttributeInfo = new CustomAttributeInfo("BoxPlotPercentile", typeof(float), 25f, SR.DescriptionCustomAttributeBoxPlotPercentile, array, true, false);
			customAttributeInfo.MinValue = 0f;
			customAttributeInfo.MaxValue = 1000f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("BoxPlotWhiskerPercentile", typeof(float), 10f, SR.DescriptionCustomAttributeBoxPlotWhiskerPercentile, array, true, false);
			customAttributeInfo.MinValue = 0f;
			customAttributeInfo.MaxValue = 1000f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("BoxPlotShowAverage", typeof(bool), true, SR.DescriptionCustomAttributeBoxPlotShowAverage, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("BoxPlotShowMedian", typeof(bool), true, SR.DescriptionCustomAttributeBoxPlotShowMedian, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("BoxPlotShowUnusualValues", typeof(bool), false, SR.DescriptionCustomAttributeBoxPlotShowUnusualValues, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("BoxPlotSeries", typeof(string), "", SR.DescriptionCustomAttributeBoxPlotSeries, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[1]
			{
				SeriesChartType.ErrorBar
			};
			customAttributeInfo = new CustomAttributeInfo("ErrorBarStyle", typeof(ErrorBarStyle), "Both", SR.DescriptionCustomAttributeErrorBarStyle, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("ErrorBarCenterMarkerStyle", typeof(ErrorBarMarkerStyles), "Line", SR.DescriptionCustomAttributeErrorBarCenterMarkerStyle, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("ErrorBarSeries", typeof(string), "", SR.DescriptionCustomAttributeErrorBarSeries, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("ErrorBarType", typeof(string), string.Format(CultureInfo.InvariantCulture, "{0}({1:N0})", ErrorBarType.StandardError, ErrorBarChart.DefaultErrorBarTypeValue(ErrorBarType.StandardError)), SR.DescriptionCustomAttributeErrorBarType, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[1]
			{
				SeriesChartType.PointAndFigure
			};
			customAttributeInfo = new CustomAttributeInfo("UsedYValueHigh", typeof(int), 0, SR.DescriptionCustomAttributeUsedYValueHigh, array, true, false);
			customAttributeInfo.MinValue = 0;
			customAttributeInfo.MaxValue = 20;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("UsedYValueLow", typeof(int), 1, SR.DescriptionCustomAttributeUsedYValueLow, array, true, false);
			customAttributeInfo.MinValue = 0;
			customAttributeInfo.MaxValue = 20;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("PriceUpColor", typeof(Color), "", SR.DescriptionCustomAttributeBarsPriceUpColor, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("BoxSize", typeof(string), "4%", SR.DescriptionCustomAttributePointFigureBoxSize, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("ProportionalSymbols", typeof(bool), true, SR.DescriptionCustomAttributeProportionalSymbols, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("ReversalAmount", typeof(int), "3", SR.DescriptionCustomAttributeReversalAmount, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[1]
			{
				SeriesChartType.Kagi
			};
			customAttributeInfo = new CustomAttributeInfo("UsedYValue", typeof(int), 0, SR.DescriptionCustomAttributeUsedYValue, array, true, false);
			customAttributeInfo.MinValue = 0;
			customAttributeInfo.MaxValue = 20;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("PriceUpColor", typeof(Color), "", SR.DescriptionCustomAttributeBarsPriceUpColor, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("ReversalAmount", typeof(string), "3%", SR.DescriptionCustomAttributeKagiReversalAmount, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[1]
			{
				SeriesChartType.Renko
			};
			customAttributeInfo = new CustomAttributeInfo("UsedYValue", typeof(int), 0, SR.DescriptionCustomAttributeRenkoUsedYValue, array, true, false);
			customAttributeInfo.MinValue = 0;
			customAttributeInfo.MaxValue = 20;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("PriceUpColor", typeof(Color), "", SR.DescriptionCustomAttributeBarsPriceUpColor, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("BoxSize", typeof(string), "4%", SR.DescriptionCustomAttributeBoxSize, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[1]
			{
				SeriesChartType.ThreeLineBreak
			};
			customAttributeInfo = new CustomAttributeInfo("UsedYValue", typeof(int), 0, SR.DescriptionCustomAttributeThreeLineBreakUsedYValue, array, true, false);
			customAttributeInfo.MinValue = 0;
			customAttributeInfo.MaxValue = 20;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("PriceUpColor", typeof(Color), "", SR.DescriptionCustomAttributeBarsPriceUpColor, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("NumberOfLinesInBreak", typeof(int), 3, SR.DescriptionCustomAttributeNumberOfLinesInBreak, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[1]
			{
				SeriesChartType.Funnel
			};
			customAttributeInfo = new CustomAttributeInfo("FunnelLabelStyle", typeof(FunnelLabelStyle), "OutsideInColumn", SR.DescriptionCustomAttributeFunnelLabelStyle, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("FunnelNeckWidth", typeof(float), 5f, SR.DescriptionCustomAttributeFunnelNeckWidth, array, true, false);
			customAttributeInfo.MinValue = 0f;
			customAttributeInfo.MaxValue = 100f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("FunnelNeckHeight", typeof(float), 5f, SR.DescriptionCustomAttributeFunnelNeckHeight, array, true, false);
			customAttributeInfo.MinValue = 0f;
			customAttributeInfo.MaxValue = 100f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("FunnelMinPointHeight", typeof(float), 0f, SR.DescriptionCustomAttributeFunnelMinPointHeight, array, true, false);
			customAttributeInfo.MinValue = 0f;
			customAttributeInfo.MaxValue = 100f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("Funnel3DRotationAngle", typeof(float), 5f, SR.DescriptionCustomAttributeFunnel3DRotationAngle, array, true, false);
			customAttributeInfo.AppliesTo2D = false;
			customAttributeInfo.MinValue = -10f;
			customAttributeInfo.MaxValue = 10f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("FunnelPointGap", typeof(float), 0f, SR.DescriptionCustomAttributeFunnelPointGap, array, true, false);
			customAttributeInfo.MinValue = 0f;
			customAttributeInfo.MaxValue = 100f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("Funnel3DDrawingStyle", typeof(Funnel3DDrawingStyle), "CircularBase", SR.DescriptionCustomAttributeFunnel3DDrawingStyle, array, true, false);
			customAttributeInfo.AppliesTo2D = false;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("FunnelStyle", typeof(FunnelStyle), "YIsHeight", SR.DescriptionCustomAttributeFunnelStyle, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("FunnelInsideLabelAlignment", typeof(FunnelLabelVerticalAlignment), "Center", SR.DescriptionCustomAttributeFunnelInsideLabelAlignment, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("FunnelOutsideLabelPlacement", typeof(FunnelLabelPlacement), "Right", SR.DescriptionCustomAttributeFunnelOutsideLabelPlacement, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("CalloutLineColor", typeof(Color), "Black", SR.DescriptionCustomAttributeCalloutLineColor, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[1]
			{
				SeriesChartType.Pyramid
			};
			customAttributeInfo = new CustomAttributeInfo("PyramidLabelStyle", typeof(FunnelLabelStyle), "OutsideInColumn", SR.DescriptionCustomAttributePyramidLabelStyle, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("PyramidMinPointHeight", typeof(float), 0f, SR.DescriptionCustomAttributePyramidMinPointHeight, array, true, false);
			customAttributeInfo.MinValue = 0f;
			customAttributeInfo.MaxValue = 100f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("Pyramid3DRotationAngle", typeof(float), 5f, SR.DescriptionCustomAttributePyramid3DRotationAngle, array, true, false);
			customAttributeInfo.AppliesTo2D = false;
			customAttributeInfo.MinValue = -10f;
			customAttributeInfo.MaxValue = 10f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("PyramidPointGap", typeof(float), 0f, SR.DescriptionCustomAttributePyramidPointGap, array, true, false);
			customAttributeInfo.MinValue = 0f;
			customAttributeInfo.MaxValue = 100f;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("Pyramid3DDrawingStyle", typeof(Funnel3DDrawingStyle), "SquareBase", SR.DescriptionCustomAttributePyramid3DDrawingStyle, array, true, false);
			customAttributeInfo.AppliesTo2D = false;
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("PyramidInsideLabelAlignment", typeof(FunnelLabelVerticalAlignment), "Center", SR.DescriptionCustomAttributePyramidInsideLabelAlignment, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("PyramidOutsideLabelPlacement", typeof(FunnelLabelPlacement), "Right", SR.DescriptionCustomAttributePyramidOutsideLabelPlacement, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("CalloutLineColor", typeof(Color), "Black", SR.DescriptionCustomAttributeCalloutLineColor, array, true, true);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			customAttributeInfo = new CustomAttributeInfo("PyramidValueType", typeof(PyramidValueType), "Linear", SR.DescriptionCustomAttributePyramidValueType, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
			array = new SeriesChartType[4]
			{
				SeriesChartType.Pie,
				SeriesChartType.Doughnut,
				SeriesChartType.Funnel,
				SeriesChartType.Pyramid
			};
			customAttributeInfo = new CustomAttributeInfo("SkipPaletteColorForEmptyPoint", typeof(bool), true, SR.DescriptionCustomAttributeSkipPaletteColorForEmptyPoint, array, true, false);
			this.registeredCustomAttributes.Add(customAttributeInfo);
		}

		public void Register(CustomAttributeInfo customAttributeInfo)
		{
			this.registeredCustomAttributes.Add(customAttributeInfo);
		}
	}
}
