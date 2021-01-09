using AspNetCore.Reporting.Gauge.WebForms;
using AspNetCore.ReportingServices.Common;
using AspNetCore.ReportingServices.Diagnostics.Utilities;
using AspNetCore.ReportingServices.Interfaces;
using AspNetCore.ReportingServices.ReportProcessing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	internal class GaugeMapper : MapperBase, IGaugeMapper, IDVMappingLayer, IDisposable
	{
		private class InputValueOwnerInfo
		{
			public object[] CoreGaugeElements;

			public GaugeInputValue[] GaugeInputValues;

			public InputValue[] CoreInputValues;

			public InputValueOwnerType InputValueOwnerType;

			public object InputValueOwnerDef;
		}

		private enum InputValueOwnerType
		{
			Pointer,
			Scale,
			Range,
			NumericIndicator,
			NumericIndicatorRange,
			StateIndicator,
			IndicatorState
		}

		private static class FormulaHelper
		{
			public static double[] Percentile(double[] values, double[] requiredPercentile)
			{
				double[] array = new double[requiredPercentile.Length];
				Array.Sort(values);
				int num = 0;
				foreach (double num2 in requiredPercentile)
				{
					double num3 = ((double)values.Length - 1.0) / 100.0 * num2;
					double num4 = Math.Floor(num3);
					double num5 = num3 - num4;
					array[num] = 0.0;
					if ((int)num4 < values.Length)
					{
						array[num] += (1.0 - num5) * values[(int)num4];
					}
					if ((int)(num4 + 1.0) < values.Length)
					{
						array[num] += num5 * values[(int)num4 + 1];
					}
					num++;
				}
				return array;
			}

			public static double Variance(double[] values, bool sampleVariance)
			{
				double num = 0.0;
				double num2 = FormulaHelper.Mean(values);
				foreach (double num3 in values)
				{
					num += (num3 - num2) * (num3 - num2);
				}
				if (sampleVariance)
				{
					return num / (double)(values.Length - 1);
				}
				return num / (double)values.Length;
			}

			public static double Median(double[] values)
			{
				FormulaHelper.Sort(ref values);
				int num = values.Length / 2;
				if (values.Length % 2 == 0)
				{
					return (values[num - 1] + values[num]) / 2.0;
				}
				return values[num];
			}

			public static double Mean(double[] values)
			{
				double num = 0.0;
				foreach (double num2 in values)
				{
					num += num2;
				}
				return num / (double)values.Length;
			}

			private static void Sort(ref double[] values)
			{
				for (int i = 0; i < values.Length; i++)
				{
					for (int j = i + 1; j < values.Length; j++)
					{
						if (values[i] > values[j])
						{
							double num = values[i];
							values[i] = values[j];
							values[j] = num;
						}
					}
				}
			}
		}

		private class TraceContext : ITraceContext
		{
			private DateTime m_startTime;

			private DateTime m_lastOperation;

			public bool TraceEnabled
			{
				get
				{
					return true;
				}
			}

			public TraceContext()
			{
				this.m_startTime = (this.m_lastOperation = DateTime.Now);
			}

			public void Write(string category, string message)
			{
				RSTrace.ProcessingTracer.Trace(category + "; " + message + "; " + (DateTime.Now - this.m_startTime).TotalMilliseconds + "; " + (DateTime.Now - this.m_lastOperation).TotalMilliseconds);
				this.m_lastOperation = DateTime.Now;
			}
		}

		private GaugePanel m_gaugePanel;

		private GaugeContainer m_coreGaugeContainer;

		private ActionInfoWithDynamicImageMapCollection m_actions = new ActionInfoWithDynamicImageMapCollection();

		private Formatter m_formatter;

		private static string m_CircularGaugesName = "CircularGauges";

		private static string m_RadialGaugesName = "RadialGauges";

		private List<InputValueOwnerInfo> m_inputValueOwnerInfoList;

		private List<InputValueOwnerInfo> InputValueOwnerInfoList
		{
			get
			{
				if (this.m_inputValueOwnerInfoList == null)
				{
					this.m_inputValueOwnerInfoList = new List<InputValueOwnerInfo>();
				}
				return this.m_inputValueOwnerInfoList;
			}
		}

		public GaugeMapper(GaugePanel gaugePanel, string defaultFontFamily)
			: base(defaultFontFamily)
		{
			this.m_gaugePanel = gaugePanel;
		}

		public void RenderGaugePanel()
		{
			try
			{
				if (this.m_gaugePanel != null)
				{
					this.InitializeGaugePanel();
					this.RenderBackFrame(this.m_gaugePanel.BackFrame, this.m_coreGaugeContainer.BackFrame, this.m_coreGaugeContainer);
					this.RenderGaugeLabels();
					this.RenderLinearGauges();
					this.RenderRadialGauges();
					this.RenderStateIndicators();
					this.RenderGaugePanelStyle();
					this.RenderGaugePanelTopImage();
					this.SetGaugePanelProperties();
					this.RenderData();
				}
			}
			catch (RSException)
			{
				throw;
			}
			catch (Exception ex2)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex2))
				{
					throw;
				}
				throw new RenderingObjectModelException(ex2);
			}
		}

		public void RenderDataGaugePanel()
		{
			this.RenderGaugePanel();
			this.AssignGaugeElementValues();
		}

		public Stream GetCoreXml()
		{
			try
			{
				this.m_coreGaugeContainer.Serializer.Content = SerializationContent.All;
				this.m_coreGaugeContainer.Serializer.NonSerializableContent = "";
				MemoryStream memoryStream = new MemoryStream();
				this.m_coreGaugeContainer.Serializer.Save(memoryStream);
				memoryStream.Position = 0L;
				return memoryStream;
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				Global.Tracer.Trace(TraceLevel.Verbose, ex.Message);
				return null;
			}
		}

		public Stream GetImage(DynamicImageInstance.ImageType imageType)
		{
			try
			{
				if (this.m_coreGaugeContainer == null)
				{
					return null;
				}
				GaugeImageFormat format = GaugeImageFormat.Png;
				Stream stream = null;
				switch (imageType)
				{
				case DynamicImageInstance.ImageType.EMF:
					format = GaugeImageFormat.Emf;
					stream = this.m_gaugePanel.RenderingContext.OdpContext.CreateStreamCallback(this.m_gaugePanel.Name, "emf", null, "image/emf", true, StreamOper.CreateOnly);
					break;
				case DynamicImageInstance.ImageType.PNG:
					format = GaugeImageFormat.Png;
					stream = new MemoryStream();
					break;
				}
				GaugeContainer coreGaugeContainer = this.m_coreGaugeContainer;
				coreGaugeContainer.FormatNumberHandler = (FormatNumberHandler)Delegate.Combine(coreGaugeContainer.FormatNumberHandler, new FormatNumberHandler(this.FormatNumber));
				this.m_coreGaugeContainer.MapEnabled = true;
				this.m_coreGaugeContainer.ImageResolution = base.DpiX;
				this.m_coreGaugeContainer.SaveAsImage(stream, format);
				stream.Position = 0L;
				return stream;
			}
			catch (RSException)
			{
				throw;
			}
			catch (Exception ex2)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex2))
				{
					throw;
				}
				throw new RenderingObjectModelException(ex2);
			}
		}

		public ActionInfoWithDynamicImageMapCollection GetImageMaps()
		{
			return MappingHelper.GetImageMaps(this.GetMapAreaInfoList(), this.m_actions, this.m_gaugePanel);
		}

		internal IEnumerable<MappingHelper.MapAreaInfo> GetMapAreaInfoList()
		{
			float width = (float)this.m_coreGaugeContainer.Width;
			float height = (float)this.m_coreGaugeContainer.Height;
			foreach (MapArea mapArea in this.m_coreGaugeContainer.MapAreas)
			{
				yield return new MappingHelper.MapAreaInfo(mapArea.ToolTip, mapArea.Tag, this.GetMapAreaShape(mapArea.Shape), MappingHelper.ConvertCoordinatesToRelative(mapArea.Coordinates, width, height));
			}
		}

		private void InitializeGaugePanel()
		{
			this.m_coreGaugeContainer = new GaugeContainer();
			if (RSTrace.ProcessingTracer.TraceVerbose)
			{
				TraceManager traceManager = (TraceManager)this.m_coreGaugeContainer.gauge.GetService(typeof(TraceManager));
				traceManager.TraceContext = new TraceContext();
				traceManager.TraceContext.Write("GaugeMapper", RPRes.rsTraceGaugePanelInitialized);
			}
		}

		private void RenderRadialGauges()
		{
			if (this.m_gaugePanel.RadialGauges != null)
			{
				foreach (RadialGauge radialGauge in this.m_gaugePanel.RadialGauges)
				{
					this.RenderRadialGauge(radialGauge);
				}
			}
		}

		private void RenderLinearGauges()
		{
			if (this.m_gaugePanel.LinearGauges != null)
			{
				foreach (LinearGauge linearGauge in this.m_gaugePanel.LinearGauges)
				{
					this.RenderLinearGauge(linearGauge);
				}
			}
		}

		private void RenderGaugeLabels()
		{
			if (this.m_gaugePanel.GaugeLabels != null)
			{
				foreach (GaugeLabel gaugeLabel in this.m_gaugePanel.GaugeLabels)
				{
					this.RenderGaugeLabel(gaugeLabel);
				}
			}
		}

		private void RenderStateIndicators()
		{
			if (this.m_gaugePanel.StateIndicators != null)
			{
				foreach (StateIndicator stateIndicator in this.m_gaugePanel.StateIndicators)
				{
					this.RenderStateIndicator(stateIndicator);
				}
			}
		}

		private void RenderRadialScales(RadialScaleCollection scaleCollection, CircularGauge circularGauge)
		{
			if (scaleCollection != null)
			{
				foreach (RadialScale item in scaleCollection)
				{
					this.RenderRadialScale(item, circularGauge);
				}
			}
		}

		private void RenderRadialPointers(RadialPointerCollection pointers, CircularScale circularScale, CircularGauge circularGauge)
		{
			if (pointers != null)
			{
				foreach (RadialPointer pointer in pointers)
				{
					this.RenderRadialPointer(pointer, circularScale, circularGauge);
				}
			}
		}

		private void RenderRadialScaleRanges(ScaleRangeCollection ranges, CircularScale circularScale, CircularGauge circularGauge)
		{
			if (ranges != null)
			{
				foreach (ScaleRange range in ranges)
				{
					this.RenderRadialRange(range, circularScale, circularGauge);
				}
			}
		}

		private void RenderLinearScales(LinearScaleCollection scaleCollection, AspNetCore.Reporting.Gauge.WebForms.LinearGauge linearGauge)
		{
			if (scaleCollection != null)
			{
				foreach (LinearScale item in scaleCollection)
				{
					this.RenderLinearScale(item, linearGauge);
				}
			}
		}

		private void RenderLinearPointers(LinearPointerCollection pointers, AspNetCore.Reporting.Gauge.WebForms.LinearScale linearScale, AspNetCore.Reporting.Gauge.WebForms.LinearGauge linearGauge)
		{
			if (pointers != null)
			{
				foreach (LinearPointer pointer in pointers)
				{
					this.RenderLinearPointer(pointer, linearScale, linearGauge);
				}
			}
		}

		private void RenderLinearScaleRanges(ScaleRangeCollection ranges, AspNetCore.Reporting.Gauge.WebForms.LinearScale linearScale, AspNetCore.Reporting.Gauge.WebForms.LinearGauge linearGauge)
		{
			if (ranges != null)
			{
				foreach (ScaleRange range in ranges)
				{
					this.RenderLinearRange(range, linearScale, linearGauge);
				}
			}
		}

		private void RenderCustomLabels(CustomLabelCollection customLabels, ScaleBase scaleBase)
		{
			if (customLabels != null)
			{
				foreach (CustomLabel customLabel in customLabels)
				{
					this.RenderCustomLabel(customLabel, scaleBase);
				}
			}
		}

		private void RenderRadialGauge(RadialGauge radialGauge)
		{
			CircularGauge circularGauge = new CircularGauge();
			this.m_coreGaugeContainer.CircularGauges.Add(circularGauge);
			this.RenderGauge(radialGauge, circularGauge);
			this.SetRadialGaugeProperties(radialGauge, circularGauge);
			this.RenderRadialScales(radialGauge.GaugeScales, circularGauge);
		}

		private void RenderLinearGauge(LinearGauge linearGauge)
		{
			AspNetCore.Reporting.Gauge.WebForms.LinearGauge linearGauge2 = new AspNetCore.Reporting.Gauge.WebForms.LinearGauge();
			this.m_coreGaugeContainer.LinearGauges.Add(linearGauge2);
			this.RenderGauge(linearGauge, linearGauge2);
			this.SetLinearGaugeProperties(linearGauge, linearGauge2);
			this.RenderLinearScales(linearGauge.GaugeScales, linearGauge2);
		}

		private void RenderGauge(Gauge gauge, GaugeBase gaugeBase)
		{
			this.SetGaugeProperties(gauge, gaugeBase);
			this.RenderActionInfo(gauge.ActionInfo, gaugeBase.ToolTip, gaugeBase);
			this.RenderBackFrame(gauge.BackFrame, gaugeBase.BackFrame, gaugeBase);
			this.RenderGaugeTopImage(gauge.TopImage, gaugeBase);
		}

		private void RenderGaugeLabel(GaugeLabel gaugeLabel)
		{
			AspNetCore.Reporting.Gauge.WebForms.GaugeLabel gaugeLabel2 = new AspNetCore.Reporting.Gauge.WebForms.GaugeLabel();
			this.m_coreGaugeContainer.Labels.Add(gaugeLabel2);
			this.SetGaugeLabelProperties(gaugeLabel, gaugeLabel2);
			this.RenderActionInfo(gaugeLabel.ActionInfo, gaugeLabel2.ToolTip, gaugeLabel2);
			this.RenderGaugeLabelStyle(gaugeLabel, gaugeLabel2);
		}

		private void RenderStateIndicator(StateIndicator stateIndicator)
		{
			AspNetCore.Reporting.Gauge.WebForms.StateIndicator stateIndicator2 = new AspNetCore.Reporting.Gauge.WebForms.StateIndicator();
			this.m_coreGaugeContainer.StateIndicators.Add(stateIndicator2);
			this.SetStateIndicatorProperties(stateIndicator, stateIndicator2);
			this.RenderStateIndicatorStyle(stateIndicator, stateIndicator2);
			this.RenderActionInfo(stateIndicator.ActionInfo, stateIndicator2.ToolTip, stateIndicator2);
			this.RenderIndicatorStates(stateIndicator.IndicatorStates, stateIndicator2);
		}

		private void RenderStateIndicatorStyle(StateIndicator stateIndicator, AspNetCore.Reporting.Gauge.WebForms.StateIndicator coreIndicator)
		{
			Style style = stateIndicator.Style;
			if (style != null)
			{
				StyleInstance style2 = stateIndicator.Instance.Style;
				coreIndicator.FillColor = MappingHelper.GetStyleBackgroundColor(style, style2);
				coreIndicator.ShadowOffset = (float)this.GetValidShadowOffset(MappingHelper.GetStyleShadowOffset(style, style2, base.DpiX));
				coreIndicator.ShowBorder = (MappingHelper.GetStyleBorderStyle(style.Border) == BorderStyles.Solid);
			}
		}

		private void SetStateIndicatorProperties(StateIndicator stateIndicator, AspNetCore.Reporting.Gauge.WebForms.StateIndicator coreIndicator)
		{
			coreIndicator.Name = stateIndicator.Name;
			if (stateIndicator.ParentItem != null)
			{
				coreIndicator.Parent = this.GetParentName(stateIndicator.ParentItem);
			}
			else
			{
				coreIndicator.Parent = "";
			}
			coreIndicator.Location.X = this.GetPanelItemLeft(stateIndicator);
			coreIndicator.Location.Y = this.GetPanelItemTop(stateIndicator);
			coreIndicator.Size.Width = this.GetPanelItemWidth(stateIndicator);
			coreIndicator.Size.Height = this.GetPanelItemHeight(stateIndicator);
			coreIndicator.Visible = !this.GetPanelItemHidden(stateIndicator);
			int zOrder = default(int);
			if (this.GetPanelItemZIndex((GaugePanelItem)stateIndicator, out zOrder))
			{
				coreIndicator.ZOrder = zOrder;
			}
			string toolTip = default(string);
			if (this.GetPanelItemToolTip((GaugePanelItem)stateIndicator, out toolTip))
			{
				coreIndicator.ToolTip = toolTip;
			}
			ReportDoubleProperty angle = stateIndicator.Angle;
			if (angle != null)
			{
				if (!angle.IsExpression)
				{
					coreIndicator.Angle = (float)angle.Value;
				}
				else
				{
					coreIndicator.Angle = (float)stateIndicator.Instance.Angle;
				}
			}
			ReportEnumProperty<GaugeStateIndicatorStyles> indicatorStyle = stateIndicator.IndicatorStyle;
			GaugeStateIndicatorStyles gaugeStateIndicatorStyles = GaugeStateIndicatorStyles.Circle;
			if (indicatorStyle != null)
			{
				gaugeStateIndicatorStyles = (indicatorStyle.IsExpression ? stateIndicator.Instance.IndicatorStyle : indicatorStyle.Value);
			}
			coreIndicator.IndicatorStyle = this.GetIndicatorStyle(gaugeStateIndicatorStyles);
			if (gaugeStateIndicatorStyles == GaugeStateIndicatorStyles.Image)
			{
				this.RenderIndicatorImage(stateIndicator.IndicatorImage, coreIndicator);
			}
			ReportDoubleProperty scaleFactor = stateIndicator.ScaleFactor;
			if (scaleFactor != null)
			{
				if (!scaleFactor.IsExpression)
				{
					coreIndicator.ScaleFactor = (float)scaleFactor.Value;
				}
				else
				{
					coreIndicator.ScaleFactor = (float)stateIndicator.Instance.ScaleFactor;
				}
			}
			ReportEnumProperty<GaugeResizeModes> resizeMode = stateIndicator.ResizeMode;
			if (resizeMode != null)
			{
				if (!resizeMode.IsExpression)
				{
					coreIndicator.ResizeMode = this.GetResizeMode(resizeMode.Value);
				}
				else
				{
					coreIndicator.ResizeMode = this.GetResizeMode(stateIndicator.Instance.ResizeMode);
				}
			}
			InputValueOwnerInfo inputValueOwnerInfo = null;
			if (stateIndicator.GaugeInputValue != null)
			{
				inputValueOwnerInfo = this.InitializeStateIndicatorInputValues(stateIndicator, coreIndicator);
				InputValue inputValue = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue);
				inputValueOwnerInfo.CoreInputValues[0] = inputValue;
				inputValueOwnerInfo.GaugeInputValues[0] = stateIndicator.GaugeInputValue;
			}
			if (stateIndicator.MinimumValue != null)
			{
				if (inputValueOwnerInfo == null)
				{
					inputValueOwnerInfo = this.InitializeStateIndicatorInputValues(stateIndicator, coreIndicator);
				}
				InputValue inputValue2 = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue2);
				inputValueOwnerInfo.CoreInputValues[1] = inputValue2;
				inputValueOwnerInfo.GaugeInputValues[1] = stateIndicator.MinimumValue;
			}
			if (stateIndicator.MaximumValue != null)
			{
				if (inputValueOwnerInfo == null)
				{
					inputValueOwnerInfo = this.InitializeStateIndicatorInputValues(stateIndicator, coreIndicator);
				}
				InputValue inputValue3 = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue3);
				inputValueOwnerInfo.CoreInputValues[2] = inputValue3;
				inputValueOwnerInfo.GaugeInputValues[2] = stateIndicator.MaximumValue;
			}
			ReportEnumProperty<GaugeTransformationType> transformationType = stateIndicator.TransformationType;
			GaugeTransformationType gaugeTransformationType = GaugeTransformationType.Percentage;
			if (transformationType != null)
			{
				gaugeTransformationType = (transformationType.IsExpression ? stateIndicator.Instance.TransformationType : transformationType.Value);
			}
			coreIndicator.IsPercentBased = (gaugeTransformationType == GaugeTransformationType.Percentage);
		}

		private InputValueOwnerInfo InitializeStateIndicatorInputValues(StateIndicator stateIndicator, AspNetCore.Reporting.Gauge.WebForms.StateIndicator coreIndicator)
		{
			InputValueOwnerInfo inputValueOwnerInfo = this.CreateInputValueOwnerInfo(3);
			inputValueOwnerInfo.CoreGaugeElements = new object[1]
			{
				coreIndicator
			};
			inputValueOwnerInfo.InputValueOwnerType = InputValueOwnerType.StateIndicator;
			inputValueOwnerInfo.InputValueOwnerDef = stateIndicator;
			return inputValueOwnerInfo;
		}

		private void RenderIndicatorImage(IndicatorImage indicatorImage, AspNetCore.Reporting.Gauge.WebForms.StateIndicator coreIndicator)
		{
			if (indicatorImage != null)
			{
				coreIndicator.Image = this.AddNamedImage(indicatorImage);
				Color color = default(Color);
				if (this.GetBaseGaugeImageTransparentColor((BaseGaugeImage)indicatorImage, out color))
				{
					coreIndicator.ImageTransColor = color;
				}
				ReportColorProperty hueColor = indicatorImage.HueColor;
				if (hueColor != null)
				{
					if (MappingHelper.GetColorFromReportColorProperty(hueColor, ref color))
					{
						coreIndicator.ImageHueColor = color;
					}
					else
					{
						ReportColor hueColor2 = indicatorImage.Instance.HueColor;
						if (hueColor2 != null)
						{
							coreIndicator.ImageHueColor = hueColor2.ToColor();
						}
					}
				}
				ReportDoubleProperty transparency = indicatorImage.Transparency;
				if (transparency != null)
				{
					if (!transparency.IsExpression)
					{
						coreIndicator.ImageTransparency = (float)transparency.Value;
					}
					else
					{
						coreIndicator.ImageTransparency = (float)indicatorImage.Instance.Transparency;
					}
				}
			}
		}

		private StateIndicatorStyle GetIndicatorStyle(GaugeStateIndicatorStyles style)
		{
			switch (style)
			{
			case GaugeStateIndicatorStyles.ArrowDown:
				return StateIndicatorStyle.ArrowDown;
			case GaugeStateIndicatorStyles.ArrowDownIncline:
				return StateIndicatorStyle.ArrowDownIncline;
			case GaugeStateIndicatorStyles.ArrowSide:
				return StateIndicatorStyle.ArrowSide;
			case GaugeStateIndicatorStyles.ArrowUp:
				return StateIndicatorStyle.ArrowUp;
			case GaugeStateIndicatorStyles.ArrowUpIncline:
				return StateIndicatorStyle.ArrowUpIncline;
			case GaugeStateIndicatorStyles.BoxesAllFilled:
				return StateIndicatorStyle.BoxesAllFilled;
			case GaugeStateIndicatorStyles.BoxesNoneFilled:
				return StateIndicatorStyle.BoxesNoneFilled;
			case GaugeStateIndicatorStyles.BoxesOneFilled:
				return StateIndicatorStyle.BoxesOneFilled;
			case GaugeStateIndicatorStyles.BoxesThreeFilled:
				return StateIndicatorStyle.BoxesThreeFilled;
			case GaugeStateIndicatorStyles.BoxesTwoFilled:
				return StateIndicatorStyle.BoxesTwoFilled;
			case GaugeStateIndicatorStyles.ButtonPause:
				return StateIndicatorStyle.ButtonPause;
			case GaugeStateIndicatorStyles.ButtonPlay:
				return StateIndicatorStyle.ButtonPlay;
			case GaugeStateIndicatorStyles.ButtonStop:
				return StateIndicatorStyle.ButtonStop;
			case GaugeStateIndicatorStyles.FaceFrown:
				return StateIndicatorStyle.FaceFrown;
			case GaugeStateIndicatorStyles.FaceNeutral:
				return StateIndicatorStyle.FaceNeutral;
			case GaugeStateIndicatorStyles.FaceSmile:
				return StateIndicatorStyle.FaceSmile;
			case GaugeStateIndicatorStyles.Flag:
				return StateIndicatorStyle.Flag;
			case GaugeStateIndicatorStyles.Image:
				return StateIndicatorStyle.Image;
			case GaugeStateIndicatorStyles.LightArrowDown:
				return StateIndicatorStyle.LightArrowDown;
			case GaugeStateIndicatorStyles.LightArrowDownIncline:
				return StateIndicatorStyle.LightArrowDownIncline;
			case GaugeStateIndicatorStyles.LightArrowSide:
				return StateIndicatorStyle.LightArrowSide;
			case GaugeStateIndicatorStyles.LightArrowUp:
				return StateIndicatorStyle.LightArrowUp;
			case GaugeStateIndicatorStyles.LightArrowUpIncline:
				return StateIndicatorStyle.LightArrowUpIncline;
			case GaugeStateIndicatorStyles.None:
				return StateIndicatorStyle.None;
			case GaugeStateIndicatorStyles.QuartersAllFilled:
				return StateIndicatorStyle.QuartersAllFilled;
			case GaugeStateIndicatorStyles.QuartersNoneFilled:
				return StateIndicatorStyle.QuartersNoneFilled;
			case GaugeStateIndicatorStyles.QuartersOneFilled:
				return StateIndicatorStyle.QuartersOneFilled;
			case GaugeStateIndicatorStyles.QuartersThreeFilled:
				return StateIndicatorStyle.QuartersThreeFilled;
			case GaugeStateIndicatorStyles.QuartersTwoFilled:
				return StateIndicatorStyle.QuartersTwoFilled;
			case GaugeStateIndicatorStyles.SignalMeterFourFilled:
				return StateIndicatorStyle.SignalMeterFourFilled;
			case GaugeStateIndicatorStyles.SignalMeterNoneFilled:
				return StateIndicatorStyle.SignalMeterNoneFilled;
			case GaugeStateIndicatorStyles.SignalMeterOneFilled:
				return StateIndicatorStyle.SignalMeterOneFill;
			case GaugeStateIndicatorStyles.SignalMeterThreeFilled:
				return StateIndicatorStyle.SignalMeterThreeFilled;
			case GaugeStateIndicatorStyles.SignalMeterTwoFilled:
				return StateIndicatorStyle.SignalMeterTwoFilled;
			case GaugeStateIndicatorStyles.StarQuartersAllFilled:
				return StateIndicatorStyle.StarQuartersAllFilled;
			case GaugeStateIndicatorStyles.StarQuartersNoneFilled:
				return StateIndicatorStyle.StarQuartersNoneFilled;
			case GaugeStateIndicatorStyles.StarQuartersOneFilled:
				return StateIndicatorStyle.StarQuartersOneFilled;
			case GaugeStateIndicatorStyles.StarQuartersThreeFilled:
				return StateIndicatorStyle.StarQuartersThreeFilled;
			case GaugeStateIndicatorStyles.StarQuartersTwoFilled:
				return StateIndicatorStyle.StarQuartersTwoFilled;
			case GaugeStateIndicatorStyles.ThreeSignsCircle:
				return StateIndicatorStyle.ThreeSignsCircle;
			case GaugeStateIndicatorStyles.ThreeSignsDiamond:
				return StateIndicatorStyle.ThreeSignsDiamond;
			case GaugeStateIndicatorStyles.ThreeSignsTriangle:
				return StateIndicatorStyle.ThreeSignsTriangle;
			case GaugeStateIndicatorStyles.ThreeSymbolCheck:
				return StateIndicatorStyle.ThreeSymbolCheck;
			case GaugeStateIndicatorStyles.ThreeSymbolCross:
				return StateIndicatorStyle.ThreeSymbolCross;
			case GaugeStateIndicatorStyles.ThreeSymbolExclamation:
				return StateIndicatorStyle.ThreeSymbolExclamation;
			case GaugeStateIndicatorStyles.ThreeSymbolUnCircledCheck:
				return StateIndicatorStyle.ThreeSymbolUnCircledCheck;
			case GaugeStateIndicatorStyles.ThreeSymbolUnCircledCross:
				return StateIndicatorStyle.ThreeSymbolUnCircledCross;
			case GaugeStateIndicatorStyles.ThreeSymbolUnCircledExclamation:
				return StateIndicatorStyle.ThreeSymbolUnCircledExclamation;
			case GaugeStateIndicatorStyles.TrafficLight:
				return StateIndicatorStyle.TrafficLight;
			case GaugeStateIndicatorStyles.TrafficLightUnrimmed:
				return StateIndicatorStyle.TrafficLightUnrimmed;
			case GaugeStateIndicatorStyles.TriangleDash:
				return StateIndicatorStyle.TriangleDash;
			case GaugeStateIndicatorStyles.TriangleDown:
				return StateIndicatorStyle.TriangleDown;
			case GaugeStateIndicatorStyles.TriangleUp:
				return StateIndicatorStyle.TriangleUp;
			default:
				return StateIndicatorStyle.Circle;
			}
		}

		private void RenderIndicatorStates(IndicatorStateCollection states, AspNetCore.Reporting.Gauge.WebForms.StateIndicator coreIndicator)
		{
			if (states != null)
			{
				foreach (IndicatorState state2 in states)
				{
					State state = new State();
					coreIndicator.States.Add(state);
					this.RenderIndicatorState(state2, state);
				}
			}
		}

		private void RenderIndicatorState(IndicatorState state, State coreState)
		{
			coreState.Name = state.Name;
			ReportColorProperty color = state.Color;
			if (color != null)
			{
				if (!color.IsExpression)
				{
					coreState.FillColor = color.Value.ToColor();
				}
				else
				{
					coreState.FillColor = state.Instance.Color.ToColor();
				}
			}
			ReportEnumProperty<GaugeStateIndicatorStyles> indicatorStyle = state.IndicatorStyle;
			GaugeStateIndicatorStyles gaugeStateIndicatorStyles = GaugeStateIndicatorStyles.Circle;
			if (indicatorStyle != null)
			{
				gaugeStateIndicatorStyles = (indicatorStyle.IsExpression ? state.Instance.IndicatorStyle : indicatorStyle.Value);
			}
			coreState.IndicatorStyle = this.GetIndicatorStyle(gaugeStateIndicatorStyles);
			if (gaugeStateIndicatorStyles == GaugeStateIndicatorStyles.Image)
			{
				this.RenderIndicatorImage(state.IndicatorImage, coreState);
			}
			ReportDoubleProperty scaleFactor = state.ScaleFactor;
			if (scaleFactor != null)
			{
				if (!scaleFactor.IsExpression)
				{
					coreState.ScaleFactor = (float)scaleFactor.Value;
				}
				else
				{
					coreState.ScaleFactor = (float)state.Instance.ScaleFactor;
				}
			}
			InputValueOwnerInfo inputValueOwnerInfo = null;
			coreState.StartValue = double.NegativeInfinity;
			coreState.EndValue = double.PositiveInfinity;
			if (state.StartValue != null)
			{
				inputValueOwnerInfo = this.CreateInputValueOwnerInfo(2);
				inputValueOwnerInfo.CoreGaugeElements = new object[1]
				{
					coreState
				};
				InputValue inputValue = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue);
				inputValueOwnerInfo.CoreInputValues[0] = inputValue;
				inputValueOwnerInfo.GaugeInputValues[0] = state.StartValue;
				inputValueOwnerInfo.InputValueOwnerType = InputValueOwnerType.IndicatorState;
				inputValueOwnerInfo.InputValueOwnerDef = state;
			}
			if (state.EndValue != null)
			{
				if (inputValueOwnerInfo == null)
				{
					inputValueOwnerInfo = this.CreateInputValueOwnerInfo(2);
				}
				inputValueOwnerInfo.CoreGaugeElements = new object[1]
				{
					coreState
				};
				InputValue inputValue2 = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue2);
				inputValueOwnerInfo.CoreInputValues[1] = inputValue2;
				inputValueOwnerInfo.GaugeInputValues[1] = state.EndValue;
				inputValueOwnerInfo.InputValueOwnerType = InputValueOwnerType.IndicatorState;
				inputValueOwnerInfo.InputValueOwnerDef = state;
			}
		}

		private void RenderIndicatorImage(IndicatorImage indicatorImage, State coreState)
		{
			if (indicatorImage != null)
			{
				coreState.Image = this.AddNamedImage(indicatorImage);
				Color color = default(Color);
				if (this.GetBaseGaugeImageTransparentColor((BaseGaugeImage)indicatorImage, out color))
				{
					coreState.ImageTransColor = color;
				}
				ReportColorProperty hueColor = indicatorImage.HueColor;
				if (hueColor != null)
				{
					if (MappingHelper.GetColorFromReportColorProperty(hueColor, ref color))
					{
						coreState.ImageHueColor = color;
					}
					else
					{
						ReportColor hueColor2 = indicatorImage.Instance.HueColor;
						if (hueColor2 != null)
						{
							coreState.ImageHueColor = hueColor2.ToColor();
						}
					}
				}
			}
		}

		private void RenderRadialScale(RadialScale radialScale, CircularGauge circularGauge)
		{
			CircularScale circularScale = new CircularScale();
			circularGauge.Scales.Add(circularScale);
			this.RenderGaugeScale(radialScale, circularScale);
			this.SetRadialScaleProperties(radialScale, circularScale);
			this.RenderRadialScaleLabels(radialScale.ScaleLabels, circularScale.LabelStyle);
			this.RenderTickMarks(radialScale.GaugeMajorTickMarks, circularScale.MajorTickMark);
			this.RenderTickMarks(radialScale.GaugeMinorTickMarks, circularScale.MinorTickMark);
			this.RenderRadialPointers(radialScale.GaugePointers, circularScale, circularGauge);
			this.RenderRadialScaleRanges(radialScale.ScaleRanges, circularScale, circularGauge);
			this.RenderRadialScalePin(radialScale.MaximumPin, circularScale.MaximumPin);
			this.RenderRadialScalePin(radialScale.MinimumPin, circularScale.MinimumPin);
		}

		private void RenderLinearScale(LinearScale linearScale, AspNetCore.Reporting.Gauge.WebForms.LinearGauge linearGauge)
		{
			AspNetCore.Reporting.Gauge.WebForms.LinearScale linearScale2 = new AspNetCore.Reporting.Gauge.WebForms.LinearScale();
			linearGauge.Scales.Add(linearScale2);
			this.RenderGaugeScale(linearScale, linearScale2);
			this.SetLinearScaleProperties(linearScale, linearScale2);
			this.RenderLinearScaleLabels(linearScale.ScaleLabels, linearScale2.LabelStyle);
			this.RenderTickMarks(linearScale.GaugeMajorTickMarks, linearScale2.MajorTickMark);
			this.RenderTickMarks(linearScale.GaugeMinorTickMarks, linearScale2.MinorTickMark);
			this.RenderLinearPointers(linearScale.GaugePointers, linearScale2, linearGauge);
			this.RenderLinearScaleRanges(linearScale.ScaleRanges, linearScale2, linearGauge);
			this.RenderLinearScalePin(linearScale.MaximumPin, linearScale2.MaximumPin);
			this.RenderLinearScalePin(linearScale.MinimumPin, linearScale2.MinimumPin);
		}

		private void RenderGaugeScale(GaugeScale gaugeScale, ScaleBase scaleBase)
		{
			this.SetScaleProperties(gaugeScale, scaleBase);
			this.RenderActionInfo(gaugeScale.ActionInfo, scaleBase.ToolTip, scaleBase);
			this.RenderCustomLabels(gaugeScale.CustomLabels, scaleBase);
			this.RenderScaleStyle(gaugeScale, scaleBase);
		}

		private void RenderRadialPointer(RadialPointer radialPointer, CircularScale circularScale, CircularGauge circularGauge)
		{
			CircularPointer circularPointer = new CircularPointer();
			circularGauge.Pointers.Add(circularPointer);
			this.RenderGaugePointer(radialPointer, circularPointer, circularScale);
			this.SetRadialPointerProperties(radialPointer, circularPointer);
			this.RenderPointerCap(radialPointer.PointerCap, circularPointer);
		}

		private void RenderRadialRange(ScaleRange scaleRange, CircularScale circularScale, CircularGauge circularGauge)
		{
			CircularRange circularRange = new CircularRange();
			circularGauge.Ranges.Add(circularRange);
			this.RenderScaleRange(scaleRange, circularRange, circularScale);
		}

		private void RenderLinearPointer(LinearPointer linearPointer, AspNetCore.Reporting.Gauge.WebForms.LinearScale linearScale, AspNetCore.Reporting.Gauge.WebForms.LinearGauge linearGauge)
		{
			AspNetCore.Reporting.Gauge.WebForms.LinearPointer linearPointer2 = new AspNetCore.Reporting.Gauge.WebForms.LinearPointer();
			linearGauge.Pointers.Add(linearPointer2);
			this.RenderGaugePointer(linearPointer, linearPointer2, linearScale);
			this.RenderThermometer(linearPointer.Thermometer, linearPointer2);
			this.SetLinearPointerProperties(linearPointer, linearPointer2);
		}

		private void RenderThermometer(Thermometer thermometer, AspNetCore.Reporting.Gauge.WebForms.LinearPointer coreLinearPointer)
		{
			if (thermometer != null)
			{
				this.SetThermometerProperties(thermometer, coreLinearPointer);
				this.RenderThermometerStyle(thermometer, coreLinearPointer);
			}
		}

		private void RenderLinearRange(ScaleRange scaleRange, AspNetCore.Reporting.Gauge.WebForms.LinearScale linearScale, AspNetCore.Reporting.Gauge.WebForms.LinearGauge linearGauge)
		{
			LinearRange linearRange = new LinearRange();
			linearGauge.Ranges.Add(linearRange);
			this.RenderScaleRange(scaleRange, linearRange, linearScale);
		}

		private void RenderCustomLabel(CustomLabel customLabel, ScaleBase scaleBase)
		{
			AspNetCore.Reporting.Gauge.WebForms.CustomLabel customLabel2 = new AspNetCore.Reporting.Gauge.WebForms.CustomLabel();
			scaleBase.CustomLabels.Add(customLabel2);
			this.SetCustomLabelProperties(customLabel, customLabel2);
			this.RenderCustomLabelStyle(customLabel, customLabel2);
			this.RenderTickMarkStyle(customLabel.TickMarkStyle, customLabel2.TickMarkStyle);
		}

		private void RenderGaugePointer(GaugePointer gaugePointer, PointerBase pointerBase, ScaleBase parentScale)
		{
			this.SetGaugePointerProperties(gaugePointer, pointerBase, parentScale);
			this.RenderGaugePointerStyle(gaugePointer, pointerBase);
			this.RenderGaugePointerImage(gaugePointer.PointerImage, pointerBase);
			this.RenderActionInfo(gaugePointer.ActionInfo, pointerBase.ToolTip, pointerBase);
		}

		private void RenderScaleRange(ScaleRange scaleRange, RangeBase rangeBase, ScaleBase parentScale)
		{
			this.SetScaleRangeProperties(scaleRange, rangeBase, parentScale);
			this.RenderScaleRangeStyle(scaleRange, rangeBase);
			this.RenderActionInfo(scaleRange.ActionInfo, rangeBase.ToolTip, rangeBase);
		}

		private void RenderBackFrame(BackFrame backFrame, AspNetCore.Reporting.Gauge.WebForms.BackFrame coreBackFrame, object parent)
		{
			if (backFrame != null)
			{
				this.SetBackFramePropreties(backFrame, coreBackFrame);
				this.RenderBackFrameStyle(backFrame, coreBackFrame);
				this.RenderFrameBackGroundStyle(backFrame.FrameBackground, coreBackFrame);
				this.RenderFrameImage(backFrame.FrameImage, coreBackFrame);
			}
		}

		private void RenderRadialScalePin(ScalePin scalePin, CircularSpecialPosition circularSpecialPosition)
		{
			if (scalePin != null)
			{
				this.RenderTickMarkStyle(scalePin, circularSpecialPosition);
				this.SetScalePinProperties(scalePin, circularSpecialPosition);
				this.RenderRadialPinLabel(scalePin.PinLabel, circularSpecialPosition.LabelStyle);
			}
		}

		private void RenderRadialPinLabel(PinLabel pinLabel, CircularPinLabel circularPinLabel)
		{
			if (pinLabel != null)
			{
				this.RenderPinLabel(pinLabel, circularPinLabel);
				this.SetRadialPinLabelProperties(pinLabel, circularPinLabel);
			}
		}

		private void RenderLinearScalePin(ScalePin scalePin, LinearSpecialPosition linearSpecialPosition)
		{
			if (scalePin != null)
			{
				this.RenderTickMarkStyle(scalePin, linearSpecialPosition);
				this.SetScalePinProperties(scalePin, linearSpecialPosition);
				this.RenderLinearPinLabel(scalePin.PinLabel, linearSpecialPosition.LabelStyle);
			}
		}

		private void RenderLinearPinLabel(PinLabel pinLabel, LinearPinLabel linearPinLabel)
		{
			if (pinLabel != null)
			{
				this.RenderPinLabel(pinLabel, linearPinLabel);
			}
		}

		private void RenderTickMarkStyle(TickMarkStyle tickMarkStyle, CustomTickMark customTickMark)
		{
			if (tickMarkStyle != null)
			{
				this.SetTickMarkStyleProperties(tickMarkStyle, customTickMark);
				this.RenderTickMarkStyleStyle(tickMarkStyle, customTickMark);
				this.RenderTickMarkImage(tickMarkStyle.TickMarkImage, customTickMark);
			}
		}

		private void RenderPinLabel(PinLabel pinLabel, LinearPinLabel corePinLabel)
		{
			if (pinLabel != null)
			{
				this.SetPinLabelProperties(pinLabel, corePinLabel);
				this.RenderPinLabelStyle(pinLabel, corePinLabel);
			}
		}

		private void RenderTickMarks(GaugeTickMarks tickMarks, TickMark coreTickMarks)
		{
			if (tickMarks != null)
			{
				this.RenderTickMarkStyle(tickMarks, coreTickMarks);
				this.SetGaugeTickMarksProperties(tickMarks, coreTickMarks);
			}
		}

		private void RenderRadialScaleLabels(ScaleLabels scaleLabels, CircularLabelStyle labelStyle)
		{
			if (scaleLabels != null)
			{
				this.RenderScaleLabels(scaleLabels, labelStyle);
				this.SetRadialScaleLabelsProperties(scaleLabels, labelStyle);
			}
		}

		private void RenderLinearScaleLabels(ScaleLabels scaleLabels, LinearLabelStyle labelStyle)
		{
			if (scaleLabels != null)
			{
				this.RenderScaleLabels(scaleLabels, labelStyle);
			}
		}

		private void RenderScaleLabels(ScaleLabels scaleLabels, LinearLabelStyle labelStyle)
		{
			if (scaleLabels != null)
			{
				this.SetScaleLabelsProperties(scaleLabels, labelStyle);
				this.RenderScaleLabelsStyle(scaleLabels, labelStyle);
			}
		}

		private void RenderPointerCap(PointerCap pointerCap, CircularPointer circularPointer)
		{
			if (pointerCap != null)
			{
				this.SetPointerCapProperties(pointerCap, circularPointer);
				this.RenderPointerCapImage(pointerCap.CapImage, circularPointer);
				this.RenderPointerCapStyle(pointerCap, circularPointer);
			}
		}

		private void SetGaugePanelProperties()
		{
			if (this.m_gaugePanel.AntiAliasing != null)
			{
				if (!this.m_gaugePanel.AntiAliasing.IsExpression)
				{
					this.m_coreGaugeContainer.AntiAliasing = this.GetAntiAliasing(this.m_gaugePanel.AntiAliasing.Value);
				}
				else
				{
					this.m_coreGaugeContainer.AntiAliasing = this.GetAntiAliasing(this.m_gaugePanel.Instance.AntiAliasing);
				}
			}
			int width = 300;
			if (base.WidthOverrideInPixels.HasValue)
			{
				width = base.WidthOverrideInPixels.Value;
			}
			else if (this.m_gaugePanel.Width != null)
			{
				width = MappingHelper.ToIntPixels(this.m_gaugePanel.Width, base.DpiX);
			}
			this.m_coreGaugeContainer.Width = width;
			int height = 300;
			if (base.HeightOverrideInPixels.HasValue)
			{
				height = base.HeightOverrideInPixels.Value;
			}
			else if (this.m_gaugePanel.Height != null)
			{
				height = MappingHelper.ToIntPixels(this.m_gaugePanel.Height, base.DpiY);
			}
			this.m_coreGaugeContainer.Height = height;
			if (this.m_gaugePanel.ShadowIntensity != null)
			{
				if (!this.m_gaugePanel.ShadowIntensity.IsExpression)
				{
					this.m_coreGaugeContainer.ShadowIntensity = (float)this.m_gaugePanel.ShadowIntensity.Value;
				}
				else
				{
					this.m_coreGaugeContainer.ShadowIntensity = (float)this.m_gaugePanel.Instance.ShadowIntensity;
				}
			}
			if (this.m_gaugePanel.TextAntiAliasingQuality != null)
			{
				if (!this.m_gaugePanel.TextAntiAliasingQuality.IsExpression)
				{
					this.m_coreGaugeContainer.TextAntiAliasingQuality = this.GetTextAntiAliasingQuality(this.m_gaugePanel.TextAntiAliasingQuality.Value);
				}
				else
				{
					this.m_coreGaugeContainer.TextAntiAliasingQuality = this.GetTextAntiAliasingQuality(this.m_gaugePanel.Instance.TextAntiAliasingQuality);
				}
			}
			if (this.m_gaugePanel.AutoLayout != null)
			{
				if (!this.m_gaugePanel.AutoLayout.IsExpression)
				{
					this.m_coreGaugeContainer.AutoLayout = this.m_gaugePanel.AutoLayout.Value;
				}
				else
				{
					this.m_coreGaugeContainer.AutoLayout = this.m_gaugePanel.Instance.AutoLayout;
				}
			}
			else
			{
				this.m_coreGaugeContainer.AutoLayout = false;
			}
		}

		private void SetGaugeProperties(Gauge gauge, GaugeBase gaugeBase)
		{
			gaugeBase.Name = gauge.Name;
			if (gauge.ParentItem != null)
			{
				gaugeBase.Parent = this.GetParentName(gauge.ParentItem);
			}
			else
			{
				gaugeBase.Parent = "";
			}
			gaugeBase.Location.X = this.GetPanelItemLeft(gauge);
			gaugeBase.Location.Y = this.GetPanelItemTop(gauge);
			gaugeBase.Size.Width = this.GetPanelItemWidth(gauge);
			gaugeBase.Size.Height = this.GetPanelItemHeight(gauge);
			gaugeBase.Visible = !this.GetPanelItemHidden(gauge);
			int zOrder = default(int);
			if (this.GetPanelItemZIndex((GaugePanelItem)gauge, out zOrder))
			{
				gaugeBase.ZOrder = zOrder;
			}
			string toolTip = default(string);
			if (this.GetPanelItemToolTip((GaugePanelItem)gauge, out toolTip))
			{
				gaugeBase.ToolTip = toolTip;
			}
			if (gauge.ClipContent != null)
			{
				if (!gauge.ClipContent.IsExpression)
				{
					gaugeBase.ClipContent = gauge.ClipContent.Value;
				}
				else
				{
					gaugeBase.ClipContent = gauge.Instance.ClipContent;
				}
			}
			else
			{
				gaugeBase.ClipContent = false;
			}
			if (gauge.AspectRatio != null)
			{
				if (!gauge.AspectRatio.IsExpression)
				{
					gaugeBase.AspectRatio = (float)gauge.AspectRatio.Value;
				}
				else
				{
					gaugeBase.AspectRatio = (float)gauge.Instance.AspectRatio;
				}
			}
		}

		private void SetRadialGaugeProperties(RadialGauge radialGauge, CircularGauge circularGauge)
		{
			if (radialGauge.PivotX != null)
			{
				if (!radialGauge.PivotX.IsExpression)
				{
					circularGauge.PivotPoint.X = (float)radialGauge.PivotX.Value;
				}
				else
				{
					circularGauge.PivotPoint.X = (float)radialGauge.Instance.PivotX;
				}
			}
			if (radialGauge.PivotY != null)
			{
				if (!radialGauge.PivotY.IsExpression)
				{
					circularGauge.PivotPoint.Y = (float)radialGauge.PivotY.Value;
				}
				else
				{
					circularGauge.PivotPoint.Y = (float)radialGauge.Instance.PivotY;
				}
			}
		}

		private void SetLinearGaugeProperties(LinearGauge linearGauge, AspNetCore.Reporting.Gauge.WebForms.LinearGauge coreLinearGauge)
		{
			ReportEnumProperty<GaugeOrientations> orientation = linearGauge.Orientation;
			if (orientation != null)
			{
				if (!orientation.IsExpression)
				{
					coreLinearGauge.Orientation = this.GetGaugeOrientation(orientation.Value);
				}
				else
				{
					coreLinearGauge.Orientation = this.GetGaugeOrientation(linearGauge.Instance.Orientation);
				}
			}
		}

		private void SetGaugeLabelProperties(GaugeLabel gaugeLabel, AspNetCore.Reporting.Gauge.WebForms.GaugeLabel coreGaugeLabel)
		{
			coreGaugeLabel.Name = gaugeLabel.Name;
			if (gaugeLabel.ParentItem != null)
			{
				coreGaugeLabel.Parent = this.GetParentName(gaugeLabel.ParentItem);
			}
			coreGaugeLabel.Location.X = this.GetPanelItemLeft(gaugeLabel);
			coreGaugeLabel.Location.Y = this.GetPanelItemTop(gaugeLabel);
			coreGaugeLabel.Size.Width = this.GetPanelItemWidth(gaugeLabel);
			coreGaugeLabel.Size.Height = this.GetPanelItemHeight(gaugeLabel);
			coreGaugeLabel.Visible = !this.GetPanelItemHidden(gaugeLabel);
			int zOrder = default(int);
			if (this.GetPanelItemZIndex((GaugePanelItem)gaugeLabel, out zOrder))
			{
				coreGaugeLabel.ZOrder = zOrder;
			}
			string toolTip = default(string);
			if (this.GetPanelItemToolTip((GaugePanelItem)gaugeLabel, out toolTip))
			{
				coreGaugeLabel.ToolTip = toolTip;
			}
			ReportDoubleProperty angle = gaugeLabel.Angle;
			if (angle != null)
			{
				if (!angle.IsExpression)
				{
					coreGaugeLabel.Angle = (float)angle.Value;
				}
				else
				{
					coreGaugeLabel.Angle = (float)gaugeLabel.Instance.Angle;
				}
			}
			ReportBoolProperty useFontPercent = gaugeLabel.UseFontPercent;
			if (useFontPercent != null)
			{
				if (!useFontPercent.IsExpression)
				{
					coreGaugeLabel.FontUnit = this.GetFontUnit(useFontPercent.Value);
				}
				else
				{
					coreGaugeLabel.FontUnit = this.GetFontUnit(gaugeLabel.Instance.UseFontPercent);
				}
			}
			else
			{
				coreGaugeLabel.FontUnit = FontUnit.Default;
			}
			ReportStringProperty text = gaugeLabel.Text;
			if (text != null)
			{
				if (!text.IsExpression)
				{
					if (text.Value != null)
					{
						coreGaugeLabel.Text = text.Value;
					}
				}
				else
				{
					string text2 = gaugeLabel.Instance.Text;
					if (text2 != null)
					{
						coreGaugeLabel.Text = text2;
					}
				}
			}
			ReportSizeProperty textShadowOffset = gaugeLabel.TextShadowOffset;
			if (textShadowOffset != null)
			{
				int shadowOffset = textShadowOffset.IsExpression ? MappingHelper.ToIntPixels(gaugeLabel.Instance.TextShadowOffset, base.DpiX) : MappingHelper.ToIntPixels(textShadowOffset.Value, base.DpiX);
				coreGaugeLabel.TextShadowOffset = this.GetValidShadowOffset(shadowOffset);
			}
			ReportEnumProperty<GaugeResizeModes> resizeMode = gaugeLabel.ResizeMode;
			if (resizeMode != null)
			{
				if (!resizeMode.IsExpression)
				{
					coreGaugeLabel.ResizeMode = this.GetResizeMode(resizeMode.Value);
				}
				else
				{
					coreGaugeLabel.ResizeMode = this.GetResizeMode(gaugeLabel.Instance.ResizeMode);
				}
			}
		}

		private void SetScaleProperties(GaugeScale gaugeScale, ScaleBase scaleBase)
		{
			scaleBase.Name = gaugeScale.Name;
			if (gaugeScale.Hidden != null)
			{
				if (!gaugeScale.Hidden.IsExpression)
				{
					scaleBase.Visible = !gaugeScale.Hidden.Value;
				}
				else
				{
					scaleBase.Visible = !gaugeScale.Instance.Hidden;
				}
			}
			if (gaugeScale.Interval != null)
			{
				if (!gaugeScale.Interval.IsExpression)
				{
					scaleBase.Interval = gaugeScale.Interval.Value;
				}
				else
				{
					scaleBase.Interval = gaugeScale.Instance.Interval;
				}
			}
			if (gaugeScale.IntervalOffset != null)
			{
				if (!gaugeScale.IntervalOffset.IsExpression)
				{
					scaleBase.IntervalOffset = gaugeScale.IntervalOffset.Value;
				}
				else
				{
					scaleBase.IntervalOffset = gaugeScale.Instance.IntervalOffset;
				}
			}
			else
			{
				scaleBase.IntervalOffset = 0.0;
			}
			if (gaugeScale.Logarithmic != null)
			{
				if (!gaugeScale.Logarithmic.IsExpression)
				{
					scaleBase.Logarithmic = gaugeScale.Logarithmic.Value;
				}
				else
				{
					scaleBase.Logarithmic = gaugeScale.Instance.Logarithmic;
				}
			}
			if (gaugeScale.LogarithmicBase != null)
			{
				double num = gaugeScale.LogarithmicBase.IsExpression ? gaugeScale.Instance.LogarithmicBase : gaugeScale.LogarithmicBase.Value;
				if (num >= 1.0)
				{
					scaleBase.LogarithmicBase = num;
				}
			}
			if (gaugeScale.Multiplier != null)
			{
				if (!gaugeScale.Multiplier.IsExpression)
				{
					scaleBase.Multiplier = gaugeScale.Multiplier.Value;
				}
				else
				{
					scaleBase.Multiplier = gaugeScale.Instance.Multiplier;
				}
			}
			if (gaugeScale.Reversed != null)
			{
				if (!gaugeScale.Reversed.IsExpression)
				{
					scaleBase.Reversed = gaugeScale.Reversed.Value;
				}
				else
				{
					scaleBase.Reversed = gaugeScale.Instance.Reversed;
				}
			}
			if (gaugeScale.TickMarksOnTop != null)
			{
				if (!gaugeScale.TickMarksOnTop.IsExpression)
				{
					scaleBase.TickMarksOnTop = gaugeScale.TickMarksOnTop.Value;
				}
				else
				{
					scaleBase.TickMarksOnTop = gaugeScale.Instance.TickMarksOnTop;
				}
			}
			if (gaugeScale.Width != null)
			{
				if (!gaugeScale.Width.IsExpression)
				{
					scaleBase.Width = (float)gaugeScale.Width.Value;
				}
				else
				{
					scaleBase.Width = (float)gaugeScale.Instance.Width;
				}
			}
			InputValueOwnerInfo inputValueOwnerInfo = null;
			if (gaugeScale.MinimumValue != null)
			{
				inputValueOwnerInfo = this.CreateInputValueOwnerInfo(2);
				inputValueOwnerInfo.CoreGaugeElements = new object[1]
				{
					scaleBase
				};
				InputValue inputValue = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue);
				inputValueOwnerInfo.CoreInputValues[0] = inputValue;
				inputValueOwnerInfo.GaugeInputValues[0] = gaugeScale.MinimumValue;
				inputValueOwnerInfo.InputValueOwnerType = InputValueOwnerType.Scale;
				inputValueOwnerInfo.InputValueOwnerDef = gaugeScale;
			}
			if (gaugeScale.MaximumValue != null)
			{
				if (inputValueOwnerInfo == null)
				{
					inputValueOwnerInfo = this.CreateInputValueOwnerInfo(2);
				}
				inputValueOwnerInfo.CoreGaugeElements = new object[1]
				{
					scaleBase
				};
				InputValue inputValue2 = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue2);
				inputValueOwnerInfo.CoreInputValues[1] = inputValue2;
				inputValueOwnerInfo.GaugeInputValues[1] = gaugeScale.MaximumValue;
				inputValueOwnerInfo.InputValueOwnerType = InputValueOwnerType.Scale;
				inputValueOwnerInfo.InputValueOwnerDef = gaugeScale;
			}
			ReportStringProperty toolTip = gaugeScale.ToolTip;
			if (toolTip != null)
			{
				if (!toolTip.IsExpression)
				{
					if (toolTip.Value != null)
					{
						scaleBase.ToolTip = toolTip.Value;
					}
				}
				else
				{
					string toolTip2 = gaugeScale.Instance.ToolTip;
					if (toolTip2 != null)
					{
						scaleBase.ToolTip = toolTip2;
					}
				}
			}
		}

		private InputValueOwnerInfo CreateInputValueOwnerInfo(int index)
		{
			List<InputValueOwnerInfo> inputValueOwnerInfoList = this.InputValueOwnerInfoList;
			InputValueOwnerInfo inputValueOwnerInfo = new InputValueOwnerInfo();
			inputValueOwnerInfo.GaugeInputValues = new GaugeInputValue[index];
			inputValueOwnerInfo.CoreInputValues = new InputValue[index];
			for (int i = 0; i < index; i++)
			{
				inputValueOwnerInfo.GaugeInputValues[i] = null;
				inputValueOwnerInfo.CoreInputValues[i] = null;
			}
			inputValueOwnerInfoList.Add(inputValueOwnerInfo);
			return inputValueOwnerInfo;
		}

		private void SetRadialScaleProperties(RadialScale radialScale, CircularScale circularScale)
		{
			if (radialScale.Radius != null)
			{
				if (!radialScale.Radius.IsExpression)
				{
					circularScale.Radius = (float)radialScale.Radius.Value;
				}
				else
				{
					circularScale.Radius = (float)radialScale.Instance.Radius;
				}
			}
			if (radialScale.StartAngle != null)
			{
				if (!radialScale.StartAngle.IsExpression)
				{
					circularScale.StartAngle = (float)radialScale.StartAngle.Value;
				}
				else
				{
					circularScale.StartAngle = (float)radialScale.Instance.StartAngle;
				}
			}
			if (radialScale.SweepAngle != null)
			{
				if (!radialScale.SweepAngle.IsExpression)
				{
					circularScale.SweepAngle = (float)radialScale.SweepAngle.Value;
				}
				else
				{
					circularScale.SweepAngle = (float)radialScale.Instance.SweepAngle;
				}
			}
		}

		private void SetLinearScaleProperties(LinearScale linearScale, AspNetCore.Reporting.Gauge.WebForms.LinearScale coreLinearScale)
		{
			ReportDoubleProperty startMargin = linearScale.StartMargin;
			if (startMargin != null)
			{
				if (!startMargin.IsExpression)
				{
					coreLinearScale.StartMargin = (float)startMargin.Value;
				}
				else
				{
					coreLinearScale.StartMargin = (float)linearScale.Instance.StartMargin;
				}
			}
			startMargin = linearScale.EndMargin;
			if (startMargin != null)
			{
				if (!startMargin.IsExpression)
				{
					coreLinearScale.EndMargin = (float)startMargin.Value;
				}
				else
				{
					coreLinearScale.EndMargin = (float)linearScale.Instance.EndMargin;
				}
			}
			startMargin = linearScale.Position;
			if (startMargin != null)
			{
				if (!startMargin.IsExpression)
				{
					coreLinearScale.Position = (float)startMargin.Value;
				}
				else
				{
					coreLinearScale.Position = (float)linearScale.Instance.Position;
				}
			}
		}

		private void SetGaugePointerProperties(GaugePointer gaugePointer, PointerBase pointerBase, ScaleBase parentScale)
		{
			pointerBase.Name = parentScale.Name + gaugePointer.Name;
			pointerBase.ScaleName = parentScale.Name;
			if (gaugePointer.BarStart != null)
			{
				if (!gaugePointer.BarStart.IsExpression)
				{
					pointerBase.BarStart = this.GetBarStart(gaugePointer.BarStart.Value);
				}
				else
				{
					pointerBase.BarStart = this.GetBarStart(gaugePointer.Instance.BarStart);
				}
			}
			if (gaugePointer.DistanceFromScale != null)
			{
				if (!gaugePointer.DistanceFromScale.IsExpression)
				{
					pointerBase.DistanceFromScale = (float)gaugePointer.DistanceFromScale.Value;
				}
				else
				{
					pointerBase.DistanceFromScale = (float)gaugePointer.Instance.DistanceFromScale;
				}
			}
			if (gaugePointer.MarkerLength != null)
			{
				if (!gaugePointer.MarkerLength.IsExpression)
				{
					pointerBase.MarkerLength = (float)gaugePointer.MarkerLength.Value;
				}
				else
				{
					pointerBase.MarkerLength = (float)gaugePointer.Instance.MarkerLength;
				}
			}
			if (gaugePointer.MarkerStyle != null)
			{
				if (!gaugePointer.MarkerStyle.IsExpression)
				{
					pointerBase.MarkerStyle = this.GetMarkerStyle(gaugePointer.MarkerStyle.Value);
				}
				else
				{
					pointerBase.MarkerStyle = this.GetMarkerStyle(gaugePointer.Instance.MarkerStyle);
				}
			}
			else
			{
				pointerBase.MarkerStyle = MarkerStyle.Triangle;
			}
			if (gaugePointer.SnappingEnabled != null)
			{
				if (!gaugePointer.SnappingEnabled.IsExpression)
				{
					pointerBase.SnappingEnabled = gaugePointer.SnappingEnabled.Value;
				}
				else
				{
					pointerBase.SnappingEnabled = gaugePointer.Instance.SnappingEnabled;
				}
			}
			if (gaugePointer.SnappingInterval != null)
			{
				if (!gaugePointer.SnappingInterval.IsExpression)
				{
					pointerBase.SnappingInterval = gaugePointer.SnappingInterval.Value;
				}
				else
				{
					pointerBase.SnappingInterval = gaugePointer.Instance.SnappingInterval;
				}
			}
			ReportStringProperty toolTip = gaugePointer.ToolTip;
			if (toolTip != null)
			{
				if (!toolTip.IsExpression)
				{
					if (toolTip.Value != null)
					{
						pointerBase.ToolTip = toolTip.Value;
					}
				}
				else
				{
					string toolTip2 = gaugePointer.Instance.ToolTip;
					if (toolTip2 != null)
					{
						pointerBase.ToolTip = toolTip2;
					}
				}
			}
			if (gaugePointer.Hidden != null)
			{
				if (!gaugePointer.Hidden.IsExpression)
				{
					pointerBase.Visible = !gaugePointer.Hidden.Value;
				}
				else
				{
					pointerBase.Visible = !gaugePointer.Instance.Hidden;
				}
			}
			if (gaugePointer.Width != null)
			{
				if (!gaugePointer.Width.IsExpression)
				{
					pointerBase.Width = (float)gaugePointer.Width.Value;
				}
				else
				{
					pointerBase.Width = (float)gaugePointer.Instance.Width;
				}
			}
			if (gaugePointer.GaugeInputValue != null)
			{
				InputValueOwnerInfo inputValueOwnerInfo = this.CreateInputValueOwnerInfo(1);
				inputValueOwnerInfo.CoreGaugeElements = new object[1]
				{
					pointerBase
				};
				InputValue inputValue = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue);
				inputValueOwnerInfo.CoreInputValues[0] = inputValue;
				inputValueOwnerInfo.GaugeInputValues[0] = gaugePointer.GaugeInputValue;
				inputValueOwnerInfo.InputValueOwnerType = InputValueOwnerType.Pointer;
				inputValueOwnerInfo.InputValueOwnerDef = gaugePointer;
			}
		}

		private void SetRadialPointerProperties(RadialPointer radialPointer, CircularPointer circularPointer)
		{
			if (radialPointer.Placement != null)
			{
				if (!radialPointer.Placement.IsExpression)
				{
					circularPointer.Placement = this.GetPlacement(radialPointer.Placement.Value);
				}
				else
				{
					circularPointer.Placement = this.GetPlacement(radialPointer.Instance.Placement);
				}
			}
			if (radialPointer.Type != null)
			{
				if (!radialPointer.Type.IsExpression)
				{
					circularPointer.Type = this.GetCircularPointerType(radialPointer.Type.Value);
				}
				else
				{
					circularPointer.Type = this.GetCircularPointerType(radialPointer.Instance.Type);
				}
			}
			if (radialPointer.NeedleStyle != null)
			{
				if (!radialPointer.NeedleStyle.IsExpression)
				{
					circularPointer.NeedleStyle = this.GetNeedleStyle(radialPointer.NeedleStyle.Value);
				}
				else
				{
					circularPointer.NeedleStyle = this.GetNeedleStyle(radialPointer.Instance.NeedleStyle);
				}
			}
		}

		private void SetLinearPointerProperties(LinearPointer linearPointer, AspNetCore.Reporting.Gauge.WebForms.LinearPointer coreLinearPointer)
		{
			ReportEnumProperty<GaugePointerPlacements> placement = linearPointer.Placement;
			if (placement != null)
			{
				if (!placement.IsExpression)
				{
					coreLinearPointer.Placement = this.GetPlacement(placement.Value);
				}
				else
				{
					coreLinearPointer.Placement = this.GetPlacement(linearPointer.Instance.Placement);
				}
			}
			ReportEnumProperty<LinearPointerTypes> type = linearPointer.Type;
			if (type != null)
			{
				if (!type.IsExpression)
				{
					coreLinearPointer.Type = this.GetLinearPointerType(type.Value);
				}
				else
				{
					coreLinearPointer.Type = this.GetLinearPointerType(linearPointer.Instance.Type);
				}
			}
		}

		private void SetScaleRangeProperties(ScaleRange scaleRange, RangeBase rangeBase, ScaleBase scaleBase)
		{
			rangeBase.Name = scaleBase.Name + scaleRange.Name;
			rangeBase.ScaleName = scaleBase.Name;
			if (scaleRange.BackgroundGradientType != null)
			{
				if (!scaleRange.BackgroundGradientType.IsExpression)
				{
					rangeBase.FillGradientType = this.GetRangeGradientType(scaleRange.BackgroundGradientType.Value);
				}
				else
				{
					rangeBase.FillGradientType = this.GetRangeGradientType(scaleRange.Instance.BackgroundGradientType);
				}
			}
			if (scaleRange.DistanceFromScale != null)
			{
				if (!scaleRange.DistanceFromScale.IsExpression)
				{
					rangeBase.DistanceFromScale = (float)scaleRange.DistanceFromScale.Value;
				}
				else
				{
					rangeBase.DistanceFromScale = (float)scaleRange.Instance.DistanceFromScale;
				}
			}
			if (scaleRange.StartWidth != null)
			{
				if (!scaleRange.StartWidth.IsExpression)
				{
					rangeBase.StartWidth = (float)scaleRange.StartWidth.Value;
				}
				else
				{
					rangeBase.StartWidth = (float)scaleRange.Instance.StartWidth;
				}
			}
			if (scaleRange.EndWidth != null)
			{
				if (!scaleRange.EndWidth.IsExpression)
				{
					rangeBase.EndWidth = (float)scaleRange.EndWidth.Value;
				}
				else
				{
					rangeBase.EndWidth = (float)scaleRange.Instance.EndWidth;
				}
			}
			Color empty = Color.Empty;
			if (scaleRange.InRangeBarPointerColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(scaleRange.InRangeBarPointerColor, ref empty))
				{
					rangeBase.InRangeBarPointerColor = empty;
				}
				else if (scaleRange.Instance.InRangeBarPointerColor != null)
				{
					rangeBase.InRangeBarPointerColor = scaleRange.Instance.InRangeBarPointerColor.ToColor();
				}
			}
			if (scaleRange.InRangeLabelColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(scaleRange.InRangeLabelColor, ref empty))
				{
					rangeBase.InRangeLabelColor = empty;
				}
				else if (scaleRange.Instance.InRangeLabelColor != null)
				{
					rangeBase.InRangeLabelColor = scaleRange.Instance.InRangeLabelColor.ToColor();
				}
			}
			if (scaleRange.InRangeTickMarksColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(scaleRange.InRangeTickMarksColor, ref empty))
				{
					rangeBase.InRangeTickMarkColor = empty;
				}
				else if (scaleRange.Instance.InRangeTickMarksColor != null)
				{
					rangeBase.InRangeTickMarkColor = scaleRange.Instance.InRangeTickMarksColor.ToColor();
				}
			}
			if (scaleRange.Placement != null)
			{
				if (!scaleRange.Placement.IsExpression)
				{
					rangeBase.Placement = this.GetPlacement(scaleRange.Placement.Value);
				}
				else
				{
					rangeBase.Placement = this.GetPlacement(scaleRange.Instance.Placement);
				}
			}
			ReportStringProperty toolTip = scaleRange.ToolTip;
			if (toolTip != null)
			{
				if (!toolTip.IsExpression)
				{
					if (toolTip.Value != null)
					{
						rangeBase.ToolTip = toolTip.Value;
					}
				}
				else
				{
					string toolTip2 = scaleRange.Instance.ToolTip;
					if (toolTip2 != null)
					{
						rangeBase.ToolTip = toolTip2;
					}
				}
			}
			if (scaleRange.Hidden != null)
			{
				if (!scaleRange.Hidden.IsExpression)
				{
					rangeBase.Visible = !scaleRange.Hidden.Value;
				}
				else
				{
					rangeBase.Visible = !scaleRange.Instance.Hidden;
				}
			}
			InputValueOwnerInfo inputValueOwnerInfo = null;
			if (scaleRange.StartValue != null)
			{
				inputValueOwnerInfo = this.CreateInputValueOwnerInfo(2);
				inputValueOwnerInfo.CoreGaugeElements = new object[1]
				{
					rangeBase
				};
				InputValue inputValue = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue);
				inputValueOwnerInfo.CoreInputValues[0] = inputValue;
				inputValueOwnerInfo.GaugeInputValues[0] = scaleRange.StartValue;
				inputValueOwnerInfo.InputValueOwnerType = InputValueOwnerType.Range;
				inputValueOwnerInfo.InputValueOwnerDef = scaleRange;
			}
			if (scaleRange.EndValue != null)
			{
				if (inputValueOwnerInfo == null)
				{
					inputValueOwnerInfo = this.CreateInputValueOwnerInfo(2);
				}
				inputValueOwnerInfo.CoreGaugeElements = new object[1]
				{
					rangeBase
				};
				InputValue inputValue2 = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue2);
				inputValueOwnerInfo.CoreInputValues[1] = inputValue2;
				inputValueOwnerInfo.GaugeInputValues[1] = scaleRange.EndValue;
				inputValueOwnerInfo.InputValueOwnerType = InputValueOwnerType.Range;
				inputValueOwnerInfo.InputValueOwnerDef = scaleRange;
			}
		}

		private void SetBackFramePropreties(BackFrame backFrame, AspNetCore.Reporting.Gauge.WebForms.BackFrame coreBackFrame)
		{
			if (backFrame.FrameStyle != null)
			{
				if (!backFrame.FrameStyle.IsExpression)
				{
					coreBackFrame.FrameStyle = this.GetFrameStyle(backFrame.FrameStyle.Value);
				}
				else
				{
					coreBackFrame.FrameStyle = this.GetFrameStyle(backFrame.Instance.FrameStyle);
				}
			}
			else
			{
				coreBackFrame.FrameStyle = BackFrameStyle.None;
			}
			if (backFrame.FrameShape != null)
			{
				if (!backFrame.FrameShape.IsExpression)
				{
					coreBackFrame.FrameShape = this.GetFrameShape(backFrame.FrameShape.Value);
				}
				else
				{
					coreBackFrame.FrameShape = this.GetFrameShape(backFrame.Instance.FrameShape);
				}
			}
			if (backFrame.FrameWidth != null)
			{
				if (!backFrame.FrameWidth.IsExpression)
				{
					coreBackFrame.FrameWidth = (float)backFrame.FrameWidth.Value;
				}
				else
				{
					coreBackFrame.FrameWidth = (float)backFrame.Instance.FrameWidth;
				}
			}
			if (backFrame.GlassEffect != null)
			{
				if (!backFrame.GlassEffect.IsExpression)
				{
					coreBackFrame.GlassEffect = this.GetGlassEffect(backFrame.GlassEffect.Value);
				}
				else
				{
					coreBackFrame.GlassEffect = this.GetGlassEffect(backFrame.Instance.GlassEffect);
				}
			}
		}

		private void SetScaleLabelsProperties(ScaleLabels scaleLabels, LinearLabelStyle labelStyle)
		{
			ReportDoubleProperty distanceFromScale = scaleLabels.DistanceFromScale;
			if (distanceFromScale != null)
			{
				if (!distanceFromScale.IsExpression)
				{
					labelStyle.DistanceFromScale = (float)distanceFromScale.Value;
				}
				else
				{
					labelStyle.DistanceFromScale = (float)scaleLabels.Instance.DistanceFromScale;
				}
			}
			distanceFromScale = scaleLabels.FontAngle;
			if (distanceFromScale != null)
			{
				if (!distanceFromScale.IsExpression)
				{
					labelStyle.FontAngle = (float)distanceFromScale.Value;
				}
				else
				{
					labelStyle.FontAngle = (float)scaleLabels.Instance.FontAngle;
				}
			}
			distanceFromScale = scaleLabels.Interval;
			if (distanceFromScale != null)
			{
				if (!distanceFromScale.IsExpression)
				{
					labelStyle.Interval = distanceFromScale.Value;
				}
				else
				{
					labelStyle.Interval = scaleLabels.Instance.Interval;
				}
			}
			distanceFromScale = scaleLabels.IntervalOffset;
			if (distanceFromScale != null)
			{
				if (!distanceFromScale.IsExpression)
				{
					labelStyle.IntervalOffset = distanceFromScale.Value;
				}
				else
				{
					labelStyle.IntervalOffset = scaleLabels.Instance.IntervalOffset;
				}
			}
			else
			{
				labelStyle.IntervalOffset = 0.0;
			}
			ReportEnumProperty<GaugeLabelPlacements> placement = scaleLabels.Placement;
			if (placement != null)
			{
				if (!placement.IsExpression)
				{
					labelStyle.Placement = this.GetPlacement(placement.Value);
				}
				else
				{
					labelStyle.Placement = this.GetPlacement(scaleLabels.Instance.Placement);
				}
			}
			ReportBoolProperty hidden = scaleLabels.Hidden;
			if (hidden != null)
			{
				if (!hidden.IsExpression)
				{
					labelStyle.Visible = !hidden.Value;
				}
				else
				{
					labelStyle.Visible = !scaleLabels.Instance.Hidden;
				}
			}
			else
			{
				labelStyle.Visible = true;
			}
			hidden = scaleLabels.ShowEndLabels;
			if (hidden != null)
			{
				if (!hidden.IsExpression)
				{
					labelStyle.ShowEndLabels = hidden.Value;
				}
				else
				{
					labelStyle.ShowEndLabels = scaleLabels.Instance.ShowEndLabels;
				}
			}
			else
			{
				labelStyle.ShowEndLabels = false;
			}
			hidden = scaleLabels.UseFontPercent;
			if (hidden != null)
			{
				if (!hidden.IsExpression)
				{
					labelStyle.FontUnit = this.GetFontUnit(hidden.Value);
				}
				else
				{
					labelStyle.FontUnit = this.GetFontUnit(scaleLabels.Instance.UseFontPercent);
				}
			}
			else
			{
				labelStyle.FontUnit = FontUnit.Default;
			}
		}

		private void SetRadialScaleLabelsProperties(ScaleLabels scaleLabels, CircularLabelStyle labelStyle)
		{
			ReportBoolProperty allowUpsideDown = scaleLabels.AllowUpsideDown;
			if (allowUpsideDown != null)
			{
				if (!allowUpsideDown.IsExpression)
				{
					labelStyle.AllowUpsideDown = allowUpsideDown.Value;
				}
				else
				{
					labelStyle.AllowUpsideDown = scaleLabels.Instance.AllowUpsideDown;
				}
			}
			else
			{
				labelStyle.AllowUpsideDown = false;
			}
			allowUpsideDown = scaleLabels.RotateLabels;
			if (allowUpsideDown != null)
			{
				if (!allowUpsideDown.IsExpression)
				{
					labelStyle.RotateLabels = allowUpsideDown.Value;
				}
				else
				{
					labelStyle.RotateLabels = scaleLabels.Instance.RotateLabels;
				}
			}
			else
			{
				labelStyle.RotateLabels = false;
			}
		}

		private void SetPointerCapProperties(PointerCap pointerCap, CircularPointer circularPointer)
		{
			ReportEnumProperty<GaugeCapStyles> capStyle = pointerCap.CapStyle;
			if (capStyle != null)
			{
				if (!capStyle.IsExpression)
				{
					circularPointer.CapStyle = this.GetCapStyle(capStyle.Value);
				}
				else
				{
					circularPointer.CapStyle = this.GetCapStyle(pointerCap.Instance.CapStyle);
				}
			}
			ReportBoolProperty hidden = pointerCap.Hidden;
			if (hidden != null)
			{
				if (!hidden.IsExpression)
				{
					circularPointer.CapVisible = !hidden.Value;
				}
				else
				{
					circularPointer.CapVisible = !pointerCap.Instance.Hidden;
				}
			}
			else
			{
				circularPointer.CapVisible = true;
			}
			hidden = pointerCap.OnTop;
			if (hidden != null)
			{
				if (!hidden.IsExpression)
				{
					circularPointer.CapOnTop = hidden.Value;
				}
				else
				{
					circularPointer.CapOnTop = pointerCap.Instance.OnTop;
				}
			}
			else
			{
				circularPointer.CapOnTop = false;
			}
			hidden = pointerCap.Reflection;
			if (hidden != null)
			{
				if (!hidden.IsExpression)
				{
					circularPointer.CapReflection = hidden.Value;
				}
				else
				{
					circularPointer.CapReflection = pointerCap.Instance.Reflection;
				}
			}
			else
			{
				circularPointer.CapReflection = false;
			}
			ReportDoubleProperty width = pointerCap.Width;
			if (width != null)
			{
				if (!width.IsExpression)
				{
					circularPointer.CapWidth = (float)width.Value;
				}
				else
				{
					circularPointer.CapWidth = (float)pointerCap.Instance.Width;
				}
			}
		}

		private void SetTickMarkStyleProperties(TickMarkStyle tickMarkStyle, CustomTickMark customTickMark)
		{
			ReportDoubleProperty distanceFromScale = tickMarkStyle.DistanceFromScale;
			if (distanceFromScale != null)
			{
				if (!distanceFromScale.IsExpression)
				{
					customTickMark.DistanceFromScale = (float)distanceFromScale.Value;
				}
				else
				{
					customTickMark.DistanceFromScale = (float)tickMarkStyle.Instance.DistanceFromScale;
				}
			}
			distanceFromScale = tickMarkStyle.GradientDensity;
			if (distanceFromScale != null)
			{
				if (!distanceFromScale.IsExpression)
				{
					customTickMark.GradientDensity = (float)distanceFromScale.Value;
				}
				else
				{
					customTickMark.GradientDensity = (float)tickMarkStyle.Instance.GradientDensity;
				}
			}
			distanceFromScale = tickMarkStyle.Length;
			if (distanceFromScale != null)
			{
				if (!distanceFromScale.IsExpression)
				{
					customTickMark.Length = (float)distanceFromScale.Value;
				}
				else
				{
					customTickMark.Length = (float)tickMarkStyle.Instance.Length;
				}
			}
			distanceFromScale = tickMarkStyle.Width;
			if (distanceFromScale != null)
			{
				if (!distanceFromScale.IsExpression)
				{
					customTickMark.Width = (float)distanceFromScale.Value;
				}
				else
				{
					customTickMark.Width = (float)tickMarkStyle.Instance.Width;
				}
			}
			ReportEnumProperty<GaugeLabelPlacements> placement = tickMarkStyle.Placement;
			if (placement != null)
			{
				if (!placement.IsExpression)
				{
					customTickMark.Placement = this.GetPlacement(placement.Value);
				}
				else
				{
					customTickMark.Placement = this.GetPlacement(tickMarkStyle.Instance.Placement);
				}
			}
			else
			{
				customTickMark.Placement = Placement.Inside;
			}
			ReportBoolProperty enableGradient = tickMarkStyle.EnableGradient;
			if (enableGradient != null)
			{
				if (!enableGradient.IsExpression)
				{
					customTickMark.EnableGradient = enableGradient.Value;
				}
				else
				{
					customTickMark.EnableGradient = tickMarkStyle.Instance.EnableGradient;
				}
			}
			else
			{
				customTickMark.EnableGradient = false;
			}
			enableGradient = tickMarkStyle.Hidden;
			if (enableGradient != null)
			{
				if (!enableGradient.IsExpression)
				{
					customTickMark.Visible = !enableGradient.Value;
				}
				else
				{
					customTickMark.Visible = !tickMarkStyle.Instance.Hidden;
				}
			}
			else
			{
				customTickMark.Visible = true;
			}
			ReportEnumProperty<GaugeTickMarkShapes> shape = tickMarkStyle.Shape;
			if (shape != null)
			{
				if (!shape.IsExpression)
				{
					customTickMark.Shape = this.GetMarkerStyle(shape.Value);
				}
				else
				{
					customTickMark.Shape = this.GetMarkerStyle(tickMarkStyle.Instance.Shape);
				}
			}
			else
			{
				customTickMark.Shape = MarkerStyle.Rectangle;
			}
		}

		private void SetScalePinProperties(ScalePin scalePin, SpecialPosition specialPosition)
		{
			ReportDoubleProperty location = scalePin.Location;
			if (location != null)
			{
				if (!location.IsExpression)
				{
					specialPosition.Location = (float)location.Value;
				}
				else
				{
					specialPosition.Location = (float)scalePin.Instance.Location;
				}
			}
			ReportBoolProperty enable = scalePin.Enable;
			if (enable != null)
			{
				if (!enable.IsExpression)
				{
					specialPosition.Enable = enable.Value;
				}
				else
				{
					specialPosition.Enable = scalePin.Instance.Enable;
				}
			}
			else
			{
				specialPosition.Enable = false;
			}
		}

		private void SetGaugeTickMarksProperties(GaugeTickMarks tickMarks, TickMark coreTickMarks)
		{
			ReportDoubleProperty interval = tickMarks.Interval;
			if (interval != null)
			{
				if (!interval.IsExpression)
				{
					coreTickMarks.Interval = interval.Value;
				}
				else
				{
					coreTickMarks.Interval = tickMarks.Instance.Interval;
				}
			}
			interval = tickMarks.IntervalOffset;
			if (interval != null)
			{
				if (!interval.IsExpression)
				{
					coreTickMarks.IntervalOffset = interval.Value;
				}
				else
				{
					coreTickMarks.IntervalOffset = tickMarks.Instance.IntervalOffset;
				}
			}
			else
			{
				coreTickMarks.IntervalOffset = 0.0;
			}
		}

		private void SetPinLabelProperties(PinLabel pinLabel, LinearPinLabel corePinLabel)
		{
			ReportDoubleProperty distanceFromScale = pinLabel.DistanceFromScale;
			if (distanceFromScale != null)
			{
				if (!distanceFromScale.IsExpression)
				{
					corePinLabel.DistanceFromScale = (float)distanceFromScale.Value;
				}
				else
				{
					corePinLabel.DistanceFromScale = (float)pinLabel.Instance.DistanceFromScale;
				}
			}
			distanceFromScale = pinLabel.FontAngle;
			if (distanceFromScale != null)
			{
				if (!distanceFromScale.IsExpression)
				{
					corePinLabel.FontAngle = (float)distanceFromScale.Value;
				}
				else
				{
					corePinLabel.FontAngle = (float)pinLabel.Instance.FontAngle;
				}
			}
			ReportBoolProperty useFontPercent = pinLabel.UseFontPercent;
			if (useFontPercent != null)
			{
				if (!useFontPercent.IsExpression)
				{
					corePinLabel.FontUnit = this.GetFontUnit(useFontPercent.Value);
				}
				else
				{
					corePinLabel.FontUnit = this.GetFontUnit(pinLabel.Instance.UseFontPercent);
				}
			}
			else
			{
				corePinLabel.FontUnit = FontUnit.Default;
			}
			ReportStringProperty text = pinLabel.Text;
			if (text != null)
			{
				if (!text.IsExpression)
				{
					if (text.Value != null)
					{
						corePinLabel.Text = text.Value;
					}
				}
				else
				{
					string text2 = pinLabel.Instance.Text;
					if (text2 != null)
					{
						corePinLabel.Text = text2;
					}
				}
			}
			ReportEnumProperty<GaugeLabelPlacements> placement = pinLabel.Placement;
			if (placement != null)
			{
				if (!placement.IsExpression)
				{
					corePinLabel.Placement = this.GetPlacement(placement.Value);
				}
				else
				{
					corePinLabel.Placement = this.GetPlacement(pinLabel.Placement.Value);
				}
			}
		}

		private void SetRadialPinLabelProperties(PinLabel pinLabel, CircularPinLabel circularPinLabel)
		{
			ReportBoolProperty allowUpsideDown = pinLabel.AllowUpsideDown;
			if (allowUpsideDown != null)
			{
				if (!allowUpsideDown.IsExpression)
				{
					circularPinLabel.AllowUpsideDown = allowUpsideDown.Value;
				}
				else
				{
					circularPinLabel.AllowUpsideDown = pinLabel.Instance.AllowUpsideDown;
				}
			}
			else
			{
				circularPinLabel.AllowUpsideDown = false;
			}
			allowUpsideDown = pinLabel.RotateLabel;
			if (allowUpsideDown != null)
			{
				if (!allowUpsideDown.IsExpression)
				{
					circularPinLabel.RotateLabel = allowUpsideDown.Value;
				}
				else
				{
					circularPinLabel.RotateLabel = pinLabel.Instance.RotateLabel;
				}
			}
			else
			{
				circularPinLabel.RotateLabel = false;
			}
		}

		private void SetCustomLabelProperties(CustomLabel customLabel, AspNetCore.Reporting.Gauge.WebForms.CustomLabel coreCustomLabel)
		{
			if (customLabel.Name != null)
			{
				coreCustomLabel.Name = customLabel.Name;
			}
			ReportBoolProperty allowUpsideDown = customLabel.AllowUpsideDown;
			if (allowUpsideDown != null)
			{
				if (!allowUpsideDown.IsExpression)
				{
					coreCustomLabel.AllowUpsideDown = allowUpsideDown.Value;
				}
				else
				{
					coreCustomLabel.AllowUpsideDown = customLabel.Instance.AllowUpsideDown;
				}
			}
			else
			{
				coreCustomLabel.AllowUpsideDown = false;
			}
			allowUpsideDown = customLabel.RotateLabel;
			if (allowUpsideDown != null)
			{
				if (!allowUpsideDown.IsExpression)
				{
					coreCustomLabel.RotateLabel = allowUpsideDown.Value;
				}
				else
				{
					coreCustomLabel.RotateLabel = customLabel.Instance.RotateLabel;
				}
			}
			else
			{
				coreCustomLabel.RotateLabel = false;
			}
			allowUpsideDown = customLabel.Hidden;
			if (allowUpsideDown != null)
			{
				if (!allowUpsideDown.IsExpression)
				{
					coreCustomLabel.Visible = !allowUpsideDown.Value;
				}
				else
				{
					coreCustomLabel.Visible = !customLabel.Instance.Hidden;
				}
			}
			else
			{
				coreCustomLabel.Visible = true;
			}
			allowUpsideDown = customLabel.UseFontPercent;
			if (allowUpsideDown != null)
			{
				if (!allowUpsideDown.IsExpression)
				{
					coreCustomLabel.FontUnit = this.GetFontUnit(allowUpsideDown.Value);
				}
				else
				{
					coreCustomLabel.FontUnit = this.GetFontUnit(customLabel.Instance.UseFontPercent);
				}
			}
			else
			{
				coreCustomLabel.FontUnit = FontUnit.Default;
			}
			ReportDoubleProperty distanceFromScale = customLabel.DistanceFromScale;
			if (distanceFromScale != null)
			{
				if (!distanceFromScale.IsExpression)
				{
					coreCustomLabel.DistanceFromScale = (float)distanceFromScale.Value;
				}
				else
				{
					coreCustomLabel.DistanceFromScale = (float)customLabel.Instance.DistanceFromScale;
				}
			}
			distanceFromScale = customLabel.FontAngle;
			if (distanceFromScale != null)
			{
				if (!distanceFromScale.IsExpression)
				{
					coreCustomLabel.FontAngle = (float)distanceFromScale.Value;
				}
				else
				{
					coreCustomLabel.FontAngle = (float)customLabel.Instance.FontAngle;
				}
			}
			distanceFromScale = customLabel.Value;
			if (distanceFromScale != null)
			{
				if (!distanceFromScale.IsExpression)
				{
					coreCustomLabel.Value = distanceFromScale.Value;
				}
				else
				{
					coreCustomLabel.Value = customLabel.Instance.Value;
				}
			}
			ReportStringProperty text = customLabel.Text;
			if (text != null)
			{
				if (!text.IsExpression)
				{
					if (text.Value != null)
					{
						coreCustomLabel.Text = text.Value;
					}
				}
				else
				{
					string text2 = customLabel.Instance.Text;
					if (text2 != null)
					{
						coreCustomLabel.Text = text2;
					}
				}
			}
			ReportEnumProperty<GaugeLabelPlacements> placement = customLabel.Placement;
			if (placement != null)
			{
				if (!placement.IsExpression)
				{
					coreCustomLabel.Placement = this.GetPlacement(placement.Value);
				}
				else
				{
					coreCustomLabel.Placement = this.GetPlacement(customLabel.Placement.Value);
				}
			}
		}

		private void SetThermometerProperties(Thermometer thermometer, AspNetCore.Reporting.Gauge.WebForms.LinearPointer linearPointer)
		{
			ReportDoubleProperty bulbOffset = thermometer.BulbOffset;
			if (bulbOffset != null)
			{
				if (!bulbOffset.IsExpression)
				{
					linearPointer.ThermometerBulbOffset = (float)bulbOffset.Value;
				}
				else
				{
					linearPointer.ThermometerBulbOffset = (float)thermometer.Instance.BulbOffset;
				}
			}
			bulbOffset = thermometer.BulbSize;
			if (bulbOffset != null)
			{
				if (!bulbOffset.IsExpression)
				{
					linearPointer.ThermometerBulbSize = (float)bulbOffset.Value;
				}
				else
				{
					linearPointer.ThermometerBulbSize = (float)thermometer.Instance.BulbSize;
				}
			}
			ReportEnumProperty<GaugeThermometerStyles> thermometerStyle = thermometer.ThermometerStyle;
			if (thermometerStyle != null)
			{
				if (!thermometerStyle.IsExpression)
				{
					linearPointer.ThermometerStyle = this.GetThermometerStyle(thermometerStyle.Value);
				}
				else
				{
					linearPointer.ThermometerStyle = this.GetThermometerStyle(thermometer.Instance.ThermometerStyle);
				}
			}
		}

		private void RenderGaugePanelStyle()
		{
			Style style = this.m_gaugePanel.Style;
			this.m_coreGaugeContainer.BackColor = Color.Transparent;
			if (style != null)
			{
				if (MappingHelper.IsStylePropertyDefined(style.BackgroundColor))
				{
					this.m_coreGaugeContainer.BackColor = MappingHelper.GetStyleBackgroundColor(style, this.m_gaugePanel.Instance.Style);
				}
				this.m_coreGaugeContainer.RightToLeft = MappingHelper.GetStyleDirection(style, this.m_gaugePanel.Instance.Style);
			}
		}

		private void RenderBackFrameStyle(BackFrame backFrame, AspNetCore.Reporting.Gauge.WebForms.BackFrame coreBackFrame)
		{
			Style style = backFrame.Style;
			if (style != null)
			{
				StyleInstance style2 = backFrame.Instance.Style;
				coreBackFrame.FrameColor = MappingHelper.GetStyleBackgroundColor(style, style2);
				coreBackFrame.FrameGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
				coreBackFrame.FrameGradientType = this.GetGradientType(style, style2);
				if (MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType))
				{
					coreBackFrame.FrameHatchStyle = this.GetHatchStyle(style, style2);
				}
				coreBackFrame.ShadowOffset = (float)this.GetValidShadowOffset(MappingHelper.GetStyleShadowOffset(style, style2, base.DpiX));
				Border border = style.Border;
				if (border != null)
				{
					coreBackFrame.BorderColor = MappingHelper.GetStyleBorderColor(border);
					coreBackFrame.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
					if (MappingHelper.IsStylePropertyDefined(border.Style))
					{
						coreBackFrame.BorderStyle = this.GetDashStyle(border);
					}
				}
			}
		}

		private void RenderFrameBackGroundStyle(FrameBackground frameBackground, AspNetCore.Reporting.Gauge.WebForms.BackFrame coreBackFrame)
		{
			if (frameBackground != null)
			{
				Style style = frameBackground.Style;
				if (style != null)
				{
					StyleInstance style2 = frameBackground.Instance.Style;
					coreBackFrame.BackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
					coreBackFrame.BackGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
					coreBackFrame.BackGradientType = this.GetGradientType(style, style2);
					if (MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType))
					{
						coreBackFrame.BackHatchStyle = this.GetHatchStyle(style, style2);
					}
				}
			}
		}

		private void RenderGaugeLabelStyle(GaugeLabel gaugeLabel, AspNetCore.Reporting.Gauge.WebForms.GaugeLabel coreGaugeLabel)
		{
			Style style = gaugeLabel.Style;
			if (style != null)
			{
				StyleInstance style2 = gaugeLabel.Instance.Style;
				coreGaugeLabel.BackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
				coreGaugeLabel.BackGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
				coreGaugeLabel.BackGradientType = this.GetGradientType(style, style2);
				if (MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType))
				{
					coreGaugeLabel.BackHatchStyle = this.GetHatchStyle(style, style2);
				}
				coreGaugeLabel.BackShadowOffset = this.GetValidShadowOffset(MappingHelper.GetStyleShadowOffset(style, style2, base.DpiX));
				Border border = style.Border;
				if (border != null)
				{
					coreGaugeLabel.BorderColor = MappingHelper.GetStyleBorderColor(border);
					coreGaugeLabel.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
					if (MappingHelper.IsStylePropertyDefined(border.Style))
					{
						coreGaugeLabel.BorderStyle = this.GetDashStyle(border);
					}
				}
				coreGaugeLabel.TextColor = MappingHelper.GetStyleColor(style, style2);
				coreGaugeLabel.Font = base.GetFont(style, style2);
				coreGaugeLabel.TextAlignment = MappingHelper.GetStyleContentAlignment(style, style2);
			}
		}

		private void RenderScaleStyle(GaugeScale scale, ScaleBase scaleBase)
		{
			Style style = scale.Style;
			if (style != null)
			{
				StyleInstance style2 = scale.Instance.Style;
				scaleBase.FillColor = MappingHelper.GetStyleBackgroundColor(style, style2);
				scaleBase.FillGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
				scaleBase.FillGradientType = this.GetGradientType(style, style2);
				if (MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType))
				{
					scaleBase.FillHatchStyle = this.GetHatchStyle(style, style2);
				}
				scaleBase.ShadowOffset = (float)this.GetValidShadowOffset(MappingHelper.GetStyleShadowOffset(style, style2, base.DpiX));
				Border border = style.Border;
				if (border != null)
				{
					scaleBase.BorderColor = MappingHelper.GetStyleBorderColor(border);
					scaleBase.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
					if (MappingHelper.IsStylePropertyDefined(border.Style))
					{
						scaleBase.BorderStyle = this.GetDashStyle(border);
					}
				}
			}
		}

		private void RenderGaugePointerStyle(GaugePointer gaugePointer, PointerBase pointerBase)
		{
			Style style = gaugePointer.Style;
			if (style != null)
			{
				StyleInstance style2 = gaugePointer.Instance.Style;
				pointerBase.FillColor = MappingHelper.GetStyleBackgroundColor(style, style2);
				pointerBase.FillGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
				pointerBase.FillGradientType = this.GetGradientType(style, style2);
				if (MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType))
				{
					pointerBase.FillHatchStyle = this.GetHatchStyle(style, style2);
				}
				pointerBase.ShadowOffset = (float)this.GetValidShadowOffset(MappingHelper.GetStyleShadowOffset(style, style2, base.DpiX));
				Border border = style.Border;
				if (border != null)
				{
					pointerBase.BorderColor = MappingHelper.GetStyleBorderColor(border);
					pointerBase.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
					if (MappingHelper.IsStylePropertyDefined(border.Style))
					{
						pointerBase.BorderStyle = this.GetDashStyle(border);
					}
				}
			}
		}

		private void RenderPointerCapStyle(PointerCap pointerCap, CircularPointer circularPointer)
		{
			Style style = pointerCap.Style;
			if (style != null)
			{
				StyleInstance style2 = pointerCap.Instance.Style;
				circularPointer.CapFillColor = MappingHelper.GetStyleBackgroundColor(style, style2);
				circularPointer.CapFillGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
				circularPointer.CapFillGradientType = this.GetGradientType(style, style2);
				if (MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType))
				{
					circularPointer.CapFillHatchStyle = this.GetHatchStyle(style, style2);
				}
			}
		}

		private void RenderScaleRangeStyle(ScaleRange scaleRange, RangeBase rangeBase)
		{
			Style style = scaleRange.Style;
			if (style != null)
			{
				StyleInstance style2 = scaleRange.Instance.Style;
				rangeBase.FillColor = MappingHelper.GetStyleBackgroundColor(style, style2);
				rangeBase.FillGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
				if (MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType))
				{
					rangeBase.FillHatchStyle = this.GetHatchStyle(style, style2);
				}
				rangeBase.ShadowOffset = (float)this.GetValidShadowOffset(MappingHelper.GetStyleShadowOffset(style, style2, base.DpiX));
				Border border = style.Border;
				if (border != null)
				{
					rangeBase.BorderColor = MappingHelper.GetStyleBorderColor(border);
					rangeBase.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
					if (MappingHelper.IsStylePropertyDefined(border.Style))
					{
						rangeBase.BorderStyle = this.GetDashStyle(border);
					}
				}
			}
		}

		private void RenderScaleLabelsStyle(ScaleLabels scaleLabels, LinearLabelStyle labelStyle)
		{
			Style style = scaleLabels.Style;
			if (style != null)
			{
				StyleInstance style2 = scaleLabels.Instance.Style;
				labelStyle.TextColor = MappingHelper.GetStyleColor(style, style2);
				labelStyle.Font = base.GetFont(style, style2);
				if (MappingHelper.IsStylePropertyDefined(style.Format))
				{
					labelStyle.FormatString = MappingHelper.GetStyleFormat(style, style2);
				}
			}
		}

		private void RenderCustomLabelStyle(CustomLabel customLabel, AspNetCore.Reporting.Gauge.WebForms.CustomLabel coreCustomLabel)
		{
			Style style = customLabel.Style;
			if (style != null)
			{
				StyleInstance style2 = customLabel.Instance.Style;
				coreCustomLabel.TextColor = MappingHelper.GetStyleColor(style, style2);
				coreCustomLabel.Font = base.GetFont(style, style2);
			}
		}

		private void RenderPinLabelStyle(PinLabel pinLabel, LinearPinLabel corePinLabel)
		{
			Style style = pinLabel.Style;
			if (style != null)
			{
				StyleInstance style2 = pinLabel.Instance.Style;
				corePinLabel.TextColor = MappingHelper.GetStyleColor(style, style2);
				corePinLabel.Font = base.GetFont(style, style2);
			}
		}

		private void RenderTickMarkStyleStyle(TickMarkStyle tickMarkStyle, CustomTickMark customTickMark)
		{
			Style style = tickMarkStyle.Style;
			if (style != null)
			{
				StyleInstance style2 = tickMarkStyle.Instance.Style;
				customTickMark.FillColor = MappingHelper.GetStyleBackgroundColor(style, style2);
				Border border = style.Border;
				if (border != null)
				{
					customTickMark.BorderColor = MappingHelper.GetStyleBorderColor(border);
					customTickMark.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
					if (MappingHelper.IsStylePropertyDefined(border.Style))
					{
						customTickMark.BorderStyle = this.GetDashStyle(border);
					}
				}
			}
		}

		private void RenderThermometerStyle(Thermometer thermometer, AspNetCore.Reporting.Gauge.WebForms.LinearPointer linearPointer)
		{
			Style style = thermometer.Style;
			if (style != null)
			{
				StyleInstance style2 = thermometer.Instance.Style;
				linearPointer.ThermometerBackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
				linearPointer.ThermometerBackGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
				linearPointer.ThermometerBackGradientType = this.GetGradientType(style, style2);
				if (MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType))
				{
					linearPointer.ThermometerBackHatchStyle = this.GetHatchStyle(style, style2);
				}
			}
		}

		private void RenderActionInfo(ActionInfo actionInfo, string toolTip, IImageMapProvider imageMapProvider)
		{
			if (actionInfo == null && string.IsNullOrEmpty(toolTip))
			{
				return;
			}
			string text = default(string);
			ActionInfoWithDynamicImageMap actionInfoWithDynamicImageMap = MappingHelper.CreateActionInfoDynamic((ReportItem)this.m_gaugePanel, actionInfo, toolTip, out text);
			if (actionInfoWithDynamicImageMap != null)
			{
				if (text != null)
				{
					imageMapProvider.Href = text;
				}
				int count = this.m_actions.Count;
				this.m_actions.InternalList.Add(actionInfoWithDynamicImageMap);
				imageMapProvider.Tag = count;
			}
		}

		private ImageMapArea.ImageMapAreaShape GetMapAreaShape(MapAreaShape shape)
		{
			if (shape == MapAreaShape.Rectangle)
			{
				return ImageMapArea.ImageMapAreaShape.Rectangle;
			}
			if (MapAreaShape.Circle == shape)
			{
				return ImageMapArea.ImageMapAreaShape.Circle;
			}
			return ImageMapArea.ImageMapAreaShape.Polygon;
		}

		private void RenderGaugePanelTopImage()
		{
			if (this.m_gaugePanel.TopImage != null)
			{
				TopImage topImage = this.m_gaugePanel.TopImage;
				this.m_coreGaugeContainer.TopImage = this.AddNamedImage(topImage);
				Color color = default(Color);
				if (this.GetBaseGaugeImageTransparentColor((BaseGaugeImage)topImage, out color))
				{
					this.m_coreGaugeContainer.TopImageTransColor = color;
				}
				ReportColorProperty hueColor = topImage.HueColor;
				if (hueColor != null)
				{
					if (MappingHelper.GetColorFromReportColorProperty(hueColor, ref color))
					{
						this.m_coreGaugeContainer.TopImageHueColor = color;
					}
					else
					{
						ReportColor hueColor2 = topImage.Instance.HueColor;
						if (hueColor2 != null)
						{
							this.m_coreGaugeContainer.TopImageHueColor = hueColor2.ToColor();
						}
					}
				}
			}
		}

		private void RenderPointerCapImage(CapImage capImage, CircularPointer circularPointer)
		{
			if (capImage != null)
			{
				circularPointer.CapImage = this.AddNamedImage(capImage);
				Color color = default(Color);
				if (this.GetBaseGaugeImageTransparentColor((BaseGaugeImage)capImage, out color))
				{
					circularPointer.CapImageTransColor = color;
				}
				ReportColorProperty hueColor = capImage.HueColor;
				if (hueColor != null)
				{
					if (MappingHelper.GetColorFromReportColorProperty(hueColor, ref color))
					{
						circularPointer.CapImageHueColor = color;
					}
					else
					{
						ReportColor hueColor2 = capImage.Instance.HueColor;
						if (hueColor2 != null)
						{
							circularPointer.CapImageHueColor = hueColor2.ToColor();
						}
					}
				}
				Point capImageOrigin = default(Point);
				ReportSizeProperty offsetX = capImage.OffsetX;
				if (offsetX != null)
				{
					if (!offsetX.IsExpression)
					{
						capImageOrigin.X = MappingHelper.ToIntPixels(offsetX.Value, base.DpiX);
					}
					else
					{
						capImageOrigin.X = MappingHelper.ToIntPixels(capImage.Instance.OffsetX, base.DpiX);
					}
				}
				offsetX = capImage.OffsetY;
				if (offsetX != null)
				{
					if (!offsetX.IsExpression)
					{
						capImageOrigin.Y = MappingHelper.ToIntPixels(offsetX.Value, base.DpiY);
					}
					else
					{
						capImageOrigin.Y = MappingHelper.ToIntPixels(capImage.Instance.OffsetY, base.DpiY);
					}
				}
				circularPointer.CapImageOrigin = capImageOrigin;
			}
		}

		private void RenderGaugeTopImage(TopImage topImage, GaugeBase gaugeBase)
		{
			if (topImage != null)
			{
				gaugeBase.TopImage = this.AddNamedImage(topImage);
				Color color = default(Color);
				if (this.GetBaseGaugeImageTransparentColor((BaseGaugeImage)topImage, out color))
				{
					gaugeBase.TopImageTransColor = color;
				}
				ReportColorProperty hueColor = topImage.HueColor;
				if (hueColor != null)
				{
					if (MappingHelper.GetColorFromReportColorProperty(hueColor, ref color))
					{
						gaugeBase.TopImageHueColor = color;
					}
					else
					{
						ReportColor hueColor2 = topImage.Instance.HueColor;
						if (hueColor2 != null)
						{
							gaugeBase.TopImageHueColor = hueColor2.ToColor();
						}
					}
				}
			}
		}

		private void RenderGaugePointerImage(PointerImage pointerImage, PointerBase pointerBase)
		{
			if (pointerImage != null)
			{
				pointerBase.Image = this.AddNamedImage(pointerImage);
				Color color = default(Color);
				if (this.GetBaseGaugeImageTransparentColor((BaseGaugeImage)pointerImage, out color))
				{
					pointerBase.ImageTransColor = color;
				}
				ReportColorProperty hueColor = pointerImage.HueColor;
				if (hueColor != null)
				{
					if (MappingHelper.GetColorFromReportColorProperty(hueColor, ref color))
					{
						pointerBase.ImageHueColor = color;
					}
					else
					{
						ReportColor hueColor2 = pointerImage.Instance.HueColor;
						if (hueColor2 != null)
						{
							pointerBase.ImageHueColor = hueColor2.ToColor();
						}
					}
				}
				Point imageOrigin = default(Point);
				ReportSizeProperty offsetX = pointerImage.OffsetX;
				if (offsetX != null)
				{
					if (!offsetX.IsExpression)
					{
						imageOrigin.X = MappingHelper.ToIntPixels(offsetX.Value, base.DpiX);
					}
					else
					{
						imageOrigin.X = MappingHelper.ToIntPixels(pointerImage.Instance.OffsetX, base.DpiX);
					}
				}
				offsetX = pointerImage.OffsetY;
				if (offsetX != null)
				{
					if (!offsetX.IsExpression)
					{
						imageOrigin.Y = MappingHelper.ToIntPixels(offsetX.Value, base.DpiY);
					}
					else
					{
						imageOrigin.Y = MappingHelper.ToIntPixels(pointerImage.Instance.OffsetY, base.DpiY);
					}
				}
				pointerBase.ImageOrigin = imageOrigin;
				ReportDoubleProperty transparency = pointerImage.Transparency;
				if (transparency != null)
				{
					if (!transparency.IsExpression)
					{
						pointerBase.ImageTransparency = (float)transparency.Value;
					}
					else
					{
						pointerBase.ImageTransparency = (float)pointerImage.Instance.Transparency;
					}
				}
			}
		}

		private void RenderFrameImage(FrameImage frameImage, AspNetCore.Reporting.Gauge.WebForms.BackFrame coreBackFrame)
		{
			if (frameImage != null)
			{
				coreBackFrame.Image = this.AddNamedImage(frameImage);
				Color color = default(Color);
				if (this.GetBaseGaugeImageTransparentColor((BaseGaugeImage)frameImage, out color))
				{
					coreBackFrame.ImageTransColor = color;
				}
				ReportColorProperty hueColor = frameImage.HueColor;
				if (hueColor != null)
				{
					if (MappingHelper.GetColorFromReportColorProperty(hueColor, ref color))
					{
						coreBackFrame.ImageHueColor = color;
					}
					else
					{
						ReportColor hueColor2 = frameImage.Instance.HueColor;
						if (hueColor2 != null)
						{
							coreBackFrame.ImageHueColor = hueColor2.ToColor();
						}
					}
				}
			}
		}

		private void RenderTickMarkImage(TopImage tickMarkImage, CustomTickMark customTickMark)
		{
			if (tickMarkImage != null)
			{
				customTickMark.Image = this.AddNamedImage(tickMarkImage);
				Color color = default(Color);
				if (this.GetBaseGaugeImageTransparentColor((BaseGaugeImage)tickMarkImage, out color))
				{
					customTickMark.ImageTransColor = color;
				}
				ReportColorProperty hueColor = tickMarkImage.HueColor;
				if (hueColor != null)
				{
					if (MappingHelper.GetColorFromReportColorProperty(hueColor, ref color))
					{
						customTickMark.ImageHueColor = color;
					}
					else
					{
						ReportColor hueColor2 = tickMarkImage.Instance.HueColor;
						if (hueColor2 != null)
						{
							customTickMark.ImageHueColor = hueColor2.ToColor();
						}
					}
				}
			}
		}

		private bool GetBaseGaugeImageTransparentColor(BaseGaugeImage baseImage, out Color color)
		{
			ReportColorProperty transparentColor = baseImage.TransparentColor;
			color = Color.Empty;
			if (transparentColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(transparentColor, ref color))
				{
					return true;
				}
				ReportColor transparentColor2 = baseImage.Instance.TransparentColor;
				if (transparentColor2 != null)
				{
					color = transparentColor2.ToColor();
					return true;
				}
			}
			return false;
		}

		private void RenderData()
		{
			DateTime now = DateTime.Now;
			this.RenderGrouping(this.m_gaugePanel.GaugeMember, ref now);
			this.AssignInputValues();
		}

		private void RenderGrouping(GaugeMember gaugeMember, ref DateTime timeStamp)
		{
			if (!gaugeMember.IsStatic)
			{
				GaugeDynamicMemberInstance gaugeDynamicMemberInstance = (GaugeDynamicMemberInstance)gaugeMember.Instance;
				gaugeDynamicMemberInstance.ResetContext();
				while (gaugeDynamicMemberInstance.MoveNext())
				{
					if (gaugeMember.ChildGaugeMember != null)
					{
						this.RenderGrouping(gaugeMember.ChildGaugeMember, ref timeStamp);
					}
					else
					{
						this.RenderCell(ref timeStamp);
					}
				}
			}
			else if (gaugeMember.ChildGaugeMember != null)
			{
				this.RenderGrouping(gaugeMember.ChildGaugeMember, ref timeStamp);
			}
			else
			{
				this.RenderCell(ref timeStamp);
			}
		}

		private void RenderCell(ref DateTime timeStamp)
		{
			if (this.m_inputValueOwnerInfoList != null)
			{
				foreach (InputValueOwnerInfo inputValueOwnerInfo in this.m_inputValueOwnerInfoList)
				{
					GaugeInputValue[] gaugeInputValues = inputValueOwnerInfo.GaugeInputValues;
					InputValue[] coreInputValues = inputValueOwnerInfo.CoreInputValues;
					for (int i = 0; i < gaugeInputValues.Length; i++)
					{
						GaugeInputValue gaugeInputValue = gaugeInputValues[i];
						InputValue inputValue = coreInputValues[i];
						if (inputValue != null)
						{
							double num;
							if (!gaugeInputValue.Value.IsExpression)
							{
								num = MappingHelper.ConvertToDouble(gaugeInputValue.Value.Value, true, false);
							}
							else
							{
								object value = gaugeInputValue.Instance.Value;
								if (gaugeInputValue.Instance.ErrorOccured)
								{
									if (RSTrace.ProcessingTracer.TraceError)
									{
										RSTrace.ProcessingTracer.Trace(RPRes.rsGaugePanelInvalidData(this.m_gaugePanel.Name));
									}
									throw new RenderingObjectModelException(RPRes.rsGaugePanelInvalidData(this.m_gaugePanel.Name));
								}
								num = MappingHelper.ConvertToDouble(value, true, false);
							}
							if (!double.IsNaN(num))
							{
								inputValue.HistoryDepth += 1L;
								inputValue.SetValue(num, timeStamp);
							}
						}
					}
				}
				timeStamp = timeStamp.AddMilliseconds(1.0);
			}
		}

		private GaugeInputValueFormulas GetFormula(GaugeInputValue gaugeInputValue)
		{
			if (gaugeInputValue.Formula != null)
			{
				if (!gaugeInputValue.Formula.IsExpression)
				{
					return gaugeInputValue.Formula.Value;
				}
				return gaugeInputValue.Instance.Formula;
			}
			return GaugeInputValueFormulas.None;
		}

		private double GetAddConstant(GaugeInputValue gaugeInputValue)
		{
			if (gaugeInputValue.AddConstant != null)
			{
				if (!gaugeInputValue.AddConstant.IsExpression)
				{
					return gaugeInputValue.AddConstant.Value;
				}
				return gaugeInputValue.Instance.AddConstant;
			}
			return 0.0;
		}

		private double GetMultiplier(GaugeInputValue gaugeInputValue)
		{
			if (gaugeInputValue.Multiplier != null)
			{
				if (!gaugeInputValue.Multiplier.IsExpression)
				{
					return gaugeInputValue.Multiplier.Value;
				}
				return gaugeInputValue.Instance.Multiplier;
			}
			return 1.0;
		}

		private bool GetMinPercent(GaugeInputValue gaugeInputValue, out double minPercent)
		{
			if (gaugeInputValue.MinPercent != null)
			{
				if (!gaugeInputValue.MinPercent.IsExpression)
				{
					minPercent = gaugeInputValue.MinPercent.Value;
				}
				else
				{
					minPercent = gaugeInputValue.Instance.MinPercent;
				}
				return true;
			}
			minPercent = double.NaN;
			return false;
		}

		private bool IsSampleVariance(GaugeInputValue gaugeInputValue)
		{
			return false;
		}

		private bool GetMaxPercent(GaugeInputValue gaugeInputValue, out double maxPercent)
		{
			if (gaugeInputValue.MaxPercent != null)
			{
				if (!gaugeInputValue.MaxPercent.IsExpression)
				{
					maxPercent = gaugeInputValue.MaxPercent.Value;
				}
				else
				{
					maxPercent = gaugeInputValue.Instance.MaxPercent;
				}
				return true;
			}
			maxPercent = double.NaN;
			return false;
		}

		private void AssignGaugeElementValues()
		{
			if (this.m_inputValueOwnerInfoList != null)
			{
				foreach (InputValueOwnerInfo inputValueOwnerInfo in this.m_inputValueOwnerInfoList)
				{
					this.AssignGaugeElementValuesToInputValues(inputValueOwnerInfo);
				}
			}
		}

		private void AssignGaugeElementValuesToInputValues(InputValueOwnerInfo inputValueOwnerInfo)
		{
			switch (inputValueOwnerInfo.InputValueOwnerType)
			{
			case InputValueOwnerType.NumericIndicator:
			case InputValueOwnerType.NumericIndicatorRange:
				break;
			case InputValueOwnerType.Pointer:
				this.AssignPointerElementValue(inputValueOwnerInfo);
				break;
			case InputValueOwnerType.Scale:
				this.AssignScaleElementValues(inputValueOwnerInfo);
				break;
			case InputValueOwnerType.Range:
				this.AssignRangeElementValues(inputValueOwnerInfo);
				break;
			case InputValueOwnerType.StateIndicator:
				this.AssignStateIndicatorElementValue(inputValueOwnerInfo);
				break;
			case InputValueOwnerType.IndicatorState:
				this.AssignIndicatorStateElementValues(inputValueOwnerInfo);
				break;
			}
		}

		private void AssignStateIndicatorElementValue(InputValueOwnerInfo inputValueOwnerInfo)
		{
			AspNetCore.Reporting.Gauge.WebForms.StateIndicator stateIndicator = (AspNetCore.Reporting.Gauge.WebForms.StateIndicator)inputValueOwnerInfo.CoreGaugeElements[0];
			RSTrace.ProcessingTracer.Assert(inputValueOwnerInfo.GaugeInputValues.Length == 3, "Unexpected amount of GaugeInputValue objects" + inputValueOwnerInfo.GaugeInputValues.Length);
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[0], stateIndicator.IsPercentBased ? stateIndicator.GetValueInPercents() : stateIndicator.Value);
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[1], stateIndicator.Minimum);
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[2], stateIndicator.Maximum);
			State currentState = stateIndicator.GetCurrentState();
			if (currentState != null)
			{
				((StateIndicator)inputValueOwnerInfo.InputValueOwnerDef).CompiledStateName = currentState.Name;
			}
		}

		private void AssignIndicatorStateElementValues(InputValueOwnerInfo inputValueOwnerInfo)
		{
			State state = (State)inputValueOwnerInfo.CoreGaugeElements[0];
			RSTrace.ProcessingTracer.Assert(inputValueOwnerInfo.GaugeInputValues.Length == 2, "Unexpected amount of GaugeInputValue objects" + inputValueOwnerInfo.GaugeInputValues.Length);
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[0], state.StartValue);
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[1], state.EndValue);
		}

		private void AssignPointerElementValue(InputValueOwnerInfo inputValueOwnerInfo)
		{
			if (inputValueOwnerInfo.CoreGaugeElements.Length == 1)
			{
				PointerBase pointerBase = (PointerBase)inputValueOwnerInfo.CoreGaugeElements[0];
				RSTrace.ProcessingTracer.Assert(inputValueOwnerInfo.GaugeInputValues.Length == 1, "Unexpected amount of GaugeInputValue objects" + inputValueOwnerInfo.GaugeInputValues.Length);
				this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[0], pointerBase.Value, pointerBase.ValueSource);
			}
			else
			{
				CompiledGaugePointerInstance[] array = new CompiledGaugePointerInstance[inputValueOwnerInfo.CoreGaugeElements.Length];
				for (int i = 0; i < inputValueOwnerInfo.CoreGaugeElements.Length; i++)
				{
					PointerBase pointerBase2 = (PointerBase)inputValueOwnerInfo.CoreGaugeElements[i];
					CompiledGaugePointerInstance compiledGaugePointerInstance = new CompiledGaugePointerInstance();
					CompiledGaugeInputValueInstance compiledGaugeInputValueInstance2 = compiledGaugePointerInstance.GaugeInputValue = new CompiledGaugeInputValueInstance(pointerBase2.Value);
					array[i] = compiledGaugePointerInstance;
				}
				((GaugePointer)inputValueOwnerInfo.InputValueOwnerDef).CompiledInstances = array;
			}
		}

		private void AssignScaleElementValues(InputValueOwnerInfo inputValueOwnerInfo)
		{
			ScaleBase scaleBase = (ScaleBase)inputValueOwnerInfo.CoreGaugeElements[0];
			RSTrace.ProcessingTracer.Assert(inputValueOwnerInfo.GaugeInputValues.Length == 2, "Unexpected amount of GaugeInputValue objects" + inputValueOwnerInfo.GaugeInputValues.Length);
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[0], scaleBase.Minimum);
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[1], scaleBase.Maximum);
		}

		private void AssignRangeElementValues(InputValueOwnerInfo inputValueOwnerInfo)
		{
			RangeBase rangeBase = (RangeBase)inputValueOwnerInfo.CoreGaugeElements[0];
			RSTrace.ProcessingTracer.Assert(inputValueOwnerInfo.GaugeInputValues.Length == 2, "Unexpected amount of GaugeInputValue objects" + inputValueOwnerInfo.GaugeInputValues.Length);
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[0], rangeBase.StartValue);
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[1], rangeBase.EndValue);
		}

		private void AssignCoreElementValue(GaugeInputValue gaugeInputValue, double gaugeElementValue)
		{
			this.AssignCoreElementValue(gaugeInputValue, gaugeElementValue, null);
		}

		private void AssignCoreElementValue(GaugeInputValue gaugeInputValue, double gaugeElementValue, string valueSource)
		{
			if (gaugeInputValue != null && gaugeInputValue.Instance.Value != null)
			{
				if (gaugeInputValue.Instance.Value is string && (string)gaugeInputValue.Instance.Value == string.Empty)
				{
					return;
				}
				double num = gaugeElementValue;
				if (!string.IsNullOrEmpty(valueSource))
				{
					num = this.GetInputValueValue(this.m_coreGaugeContainer.Values[valueSource.Split('.')[0]]);
				}
				gaugeInputValue.CompiledInstance = new CompiledGaugeInputValueInstance(num);
			}
		}

		private void AssignInputValues()
		{
			if (this.m_inputValueOwnerInfoList != null)
			{
				foreach (InputValueOwnerInfo inputValueOwnerInfo in this.m_inputValueOwnerInfoList)
				{
					this.AssignInputValuesToGaugeElement(inputValueOwnerInfo);
				}
			}
		}

		private void AssignInputValuesToGaugeElement(InputValueOwnerInfo inputValueOwnerInfo)
		{
			switch (inputValueOwnerInfo.InputValueOwnerType)
			{
			case InputValueOwnerType.NumericIndicator:
			case InputValueOwnerType.NumericIndicatorRange:
				break;
			case InputValueOwnerType.Pointer:
				this.AssignPointerValue(inputValueOwnerInfo);
				break;
			case InputValueOwnerType.Scale:
				this.AssignScaleValues(inputValueOwnerInfo);
				break;
			case InputValueOwnerType.Range:
				this.AssignRangeValues(inputValueOwnerInfo);
				break;
			case InputValueOwnerType.StateIndicator:
				this.AssignStateIndicatorValue(inputValueOwnerInfo);
				break;
			case InputValueOwnerType.IndicatorState:
				this.AssignIndicatorStateValues(inputValueOwnerInfo);
				break;
			}
		}

		private void AssignRangeValues(InputValueOwnerInfo inputValueOwnerInfo)
		{
			double num = double.NaN;
			double num2 = double.NaN;
			RangeBase rangeBase = (RangeBase)inputValueOwnerInfo.CoreGaugeElements[0];
			if (inputValueOwnerInfo.CoreInputValues.Length > 0 && inputValueOwnerInfo.CoreInputValues[0] != null)
			{
				num = this.GetValue(inputValueOwnerInfo.CoreInputValues[0], inputValueOwnerInfo.GaugeInputValues[0]);
			}
			if (!double.IsNaN(num))
			{
				rangeBase.StartValue = num;
			}
			if (inputValueOwnerInfo.CoreInputValues.Length > 1 && inputValueOwnerInfo.CoreInputValues[1] != null)
			{
				num2 = this.GetValue(inputValueOwnerInfo.CoreInputValues[1], inputValueOwnerInfo.GaugeInputValues[1]);
			}
			if (!double.IsNaN(num2))
			{
				rangeBase.EndValue = num2;
			}
		}

		private void AssignScaleValues(InputValueOwnerInfo inputValueOwnerInfo)
		{
			double num = double.NaN;
			double num2 = double.NaN;
			ScaleBase scaleBase = (ScaleBase)inputValueOwnerInfo.CoreGaugeElements[0];
			if (inputValueOwnerInfo.CoreInputValues.Length > 0 && inputValueOwnerInfo.CoreInputValues[0] != null)
			{
				num = this.GetValue(inputValueOwnerInfo.CoreInputValues[0], inputValueOwnerInfo.GaugeInputValues[0]);
			}
			if (inputValueOwnerInfo.CoreInputValues.Length > 1 && inputValueOwnerInfo.CoreInputValues[1] != null)
			{
				num2 = this.GetValue(inputValueOwnerInfo.CoreInputValues[1], inputValueOwnerInfo.GaugeInputValues[1]);
			}
			if (!double.IsNaN(num) && !double.IsNaN(num2) && num >= scaleBase.Maximum)
			{
				scaleBase.Maximum = num + 1.0;
			}
			if (num >= num2)
			{
				if (RSTrace.ProcessingTracer.TraceError)
				{
					RSTrace.ProcessingTracer.Trace(RPRes.rsGaugePanelInvalidMinMaxScale(this.m_gaugePanel.Name));
				}
				throw new RenderingObjectModelException(RPRes.rsGaugePanelInvalidMinMaxScale(this.m_gaugePanel.Name));
			}
			if (!double.IsNaN(num))
			{
				scaleBase.Minimum = num;
			}
			if (!double.IsNaN(num2))
			{
				scaleBase.Maximum = num2;
			}
		}

		private void AssignPointerValue(InputValueOwnerInfo inputValueOwnerInfo)
		{
			if (inputValueOwnerInfo.CoreInputValues.Length > 0)
			{
				GaugeInputValueFormulas formula = this.GetFormula(inputValueOwnerInfo.GaugeInputValues[0]);
				if (this.IsBuiltInFormula(formula))
				{
					((PointerBase)inputValueOwnerInfo.CoreGaugeElements[0]).ValueSource = this.GetBuiltInFormulaValueSourceName(inputValueOwnerInfo.CoreInputValues[0], inputValueOwnerInfo.GaugeInputValues[0], formula);
				}
				else
				{
					this.AssignPointerToCustomFormula(inputValueOwnerInfo, formula);
				}
			}
		}

		private void AssignStateIndicatorValue(InputValueOwnerInfo inputValueOwnerInfo)
		{
			AspNetCore.Reporting.Gauge.WebForms.StateIndicator stateIndicator = (AspNetCore.Reporting.Gauge.WebForms.StateIndicator)inputValueOwnerInfo.CoreGaugeElements[0];
			if (inputValueOwnerInfo.CoreInputValues.Length > 0)
			{
				InputValue inputValue = inputValueOwnerInfo.CoreInputValues[0];
				if (inputValue != null)
				{
					stateIndicator.Value = this.GetValue(inputValue, inputValueOwnerInfo.GaugeInputValues[0]);
				}
			}
			if (!stateIndicator.IsPercentBased)
			{
				return;
			}
			double num = double.NaN;
			double num2 = double.NaN;
			if (inputValueOwnerInfo.CoreInputValues.Length > 1)
			{
				InputValue inputValue = inputValueOwnerInfo.CoreInputValues[1];
				if (inputValue != null)
				{
					num = this.GetValue(inputValue, inputValueOwnerInfo.GaugeInputValues[1]);
				}
			}
			if (inputValueOwnerInfo.CoreInputValues.Length > 2)
			{
				InputValue inputValue = inputValueOwnerInfo.CoreInputValues[2];
				if (inputValue != null)
				{
					num2 = this.GetValue(inputValue, inputValueOwnerInfo.GaugeInputValues[2]);
				}
			}
			if (!double.IsNaN(num) && !double.IsNaN(num2) && num >= stateIndicator.Maximum)
			{
				stateIndicator.Maximum = num + 1.0;
			}
			if (!(num > num2) && (num != num2 || num2 == stateIndicator.Value))
			{
				if (!double.IsNaN(num))
				{
					stateIndicator.Minimum = num;
				}
				if (!double.IsNaN(num2))
				{
					stateIndicator.Maximum = num2;
				}
				return;
			}
			string text = RPRes.rsStateIndicatorInvalidMinMax(this.m_gaugePanel.Name, stateIndicator.Name);
			if (RSTrace.ProcessingTracer.TraceError)
			{
				RSTrace.ProcessingTracer.Trace(text);
			}
			throw new RenderingObjectModelException(text);
		}

		private void AssignIndicatorStateValues(InputValueOwnerInfo inputValueOwnerInfo)
		{
			double num = double.NaN;
			double num2 = double.NaN;
			State state = (State)inputValueOwnerInfo.CoreGaugeElements[0];
			if (inputValueOwnerInfo.CoreInputValues.Length > 0 && inputValueOwnerInfo.CoreInputValues[0] != null)
			{
				num = this.GetValue(inputValueOwnerInfo.CoreInputValues[0], inputValueOwnerInfo.GaugeInputValues[0]);
			}
			if (!double.IsNaN(num))
			{
				state.StartValue = num;
			}
			if (inputValueOwnerInfo.CoreInputValues.Length > 1 && inputValueOwnerInfo.CoreInputValues[1] != null)
			{
				num2 = this.GetValue(inputValueOwnerInfo.CoreInputValues[1], inputValueOwnerInfo.GaugeInputValues[1]);
			}
			if (!double.IsNaN(num2))
			{
				state.EndValue = num2;
			}
		}

		private bool IsBuiltInFormula(GaugeInputValueFormulas formula)
		{
			if (formula != GaugeInputValueFormulas.Integral && formula != GaugeInputValueFormulas.Linear && formula != GaugeInputValueFormulas.Max && formula != GaugeInputValueFormulas.Min && formula != 0)
			{
				return formula == GaugeInputValueFormulas.RateOfChange;
			}
			return true;
		}

		private void AssignPointerToCustomFormula(InputValueOwnerInfo inputValueOwnerInfo, GaugeInputValueFormulas formula)
		{
			PointerBase pointerBase = (PointerBase)inputValueOwnerInfo.CoreGaugeElements[0];
			GaugeInputValue gaugeInputValue = inputValueOwnerInfo.GaugeInputValues[0];
			InputValue inputValue = inputValueOwnerInfo.CoreInputValues[0];
			switch (formula)
			{
			case GaugeInputValueFormulas.Linear:
			case GaugeInputValueFormulas.Max:
			case GaugeInputValueFormulas.Min:
				break;
			case GaugeInputValueFormulas.Median:
				if (inputValue.History.Count > 0)
				{
					pointerBase.Value = FormulaHelper.Median(this.GetValues(inputValue.History));
				}
				break;
			case GaugeInputValueFormulas.Variance:
				if (inputValue.History.Count > 0)
				{
					pointerBase.Value = FormulaHelper.Variance(this.GetValues(inputValue.History), this.IsSampleVariance(gaugeInputValue));
				}
				break;
			case GaugeInputValueFormulas.Percentile:
				if (inputValue.History.Count > 0)
				{
					double[] percentileParameters = this.GetPercentileParameters(gaugeInputValue);
					if (percentileParameters != null)
					{
						double[] values = FormulaHelper.Percentile(this.GetValues(inputValue.History), percentileParameters);
						PointerBase[] array2 = (PointerBase[])(inputValueOwnerInfo.CoreGaugeElements = this.CreateMultiplePointers(pointerBase, values));
					}
				}
				break;
			case GaugeInputValueFormulas.Average:
				if (inputValue.History.Count > 0)
				{
					pointerBase.Value = FormulaHelper.Mean(this.GetValues(inputValue.History));
				}
				break;
			case GaugeInputValueFormulas.OpenClose:
				if (inputValue.History.Count > 0)
				{
					PointerBase[] array = (PointerBase[])(inputValueOwnerInfo.CoreGaugeElements = this.CreateMultiplePointers(pointerBase, new double[2]
					{
						inputValue.History[0].Value,
						inputValue.History[inputValue.History.Count - 1].Value
					}));
				}
				break;
			}
		}

		private PointerBase[] CreateMultiplePointers(PointerBase pointer, double[] values)
		{
			pointer.Value = values[0];
			PointerBase[] array = new PointerBase[values.Length];
			array[0] = pointer;
			if (pointer is CircularPointer)
			{
				CircularGauge circularGauge = (CircularGauge)pointer.ParentElement;
				for (int i = 1; i < values.Length; i++)
				{
					CircularPointer circularPointer = (CircularPointer)pointer.Clone();
					circularPointer.Name = circularGauge.Pointers.GenerateUniqueName(circularPointer);
					circularPointer.Value = values[i];
					circularGauge.Pointers.Add(circularPointer);
					array[i] = circularPointer;
				}
			}
			else if (pointer is AspNetCore.Reporting.Gauge.WebForms.LinearPointer)
			{
				AspNetCore.Reporting.Gauge.WebForms.LinearGauge linearGauge = (AspNetCore.Reporting.Gauge.WebForms.LinearGauge)pointer.ParentElement;
				for (int j = 1; j < values.Length; j++)
				{
					AspNetCore.Reporting.Gauge.WebForms.LinearPointer linearPointer = (AspNetCore.Reporting.Gauge.WebForms.LinearPointer)pointer.Clone();
					linearPointer.Name = linearGauge.Pointers.GenerateUniqueName(linearPointer);
					linearPointer.Value = values[j];
					linearGauge.Pointers.Add(linearPointer);
					array[j] = linearPointer;
				}
			}
			return array;
		}

		public override void Dispose()
		{
			if (this.m_coreGaugeContainer != null)
			{
				this.m_coreGaugeContainer.Dispose();
			}
			this.m_coreGaugeContainer = null;
			base.Dispose();
		}

		private System.Drawing.Image GetImageFromStream(BaseGaugeImage baseGaugeImage)
		{
			if (baseGaugeImage.Instance.ImageData == null)
			{
				return null;
			}
			MemoryStream stream = new MemoryStream(baseGaugeImage.Instance.ImageData, false);
			return System.Drawing.Image.FromStream(stream);
		}

		private string GetBuiltInFormulaValueSourceName(InputValue inputValue, GaugeInputValue gaugeInputValue, GaugeInputValueFormulas formula)
		{
			this.CreateBuiltInFormula(inputValue, gaugeInputValue, formula);
			string text = inputValue.Name;
			if (inputValue.CalculatedValues.Count != 0)
			{
				text = text + "." + inputValue.CalculatedValues[0].Name;
			}
			return text;
		}

		private double GetBuiltInFormulaValue(InputValue inputValue, GaugeInputValue gaugeInputValue, GaugeInputValueFormulas formula)
		{
			this.CreateBuiltInFormula(inputValue, gaugeInputValue, formula);
			return this.GetInputValueValue(inputValue);
		}

		private double GetInputValueValue(InputValue inputValue)
		{
			if (inputValue.CalculatedValues.Count == 0)
			{
				return inputValue.Value;
			}
			return inputValue.CalculatedValues[0].Value;
		}

		private void CreateBuiltInFormula(InputValue inputValue, GaugeInputValue gaugeInputValue, GaugeInputValueFormulas formula)
		{
			switch (formula)
			{
			case GaugeInputValueFormulas.Median:
			case GaugeInputValueFormulas.OpenClose:
			case GaugeInputValueFormulas.Percentile:
			case GaugeInputValueFormulas.Variance:
				break;
			case GaugeInputValueFormulas.Linear:
			{
				CalculatedValueLinear calculatedValueLinear = new CalculatedValueLinear();
				calculatedValueLinear.AddConstant = this.GetAddConstant(gaugeInputValue);
				calculatedValueLinear.Multiplier = this.GetMultiplier(gaugeInputValue);
				inputValue.CalculatedValues.Add(calculatedValueLinear);
				break;
			}
			case GaugeInputValueFormulas.Min:
				inputValue.CalculatedValues.Add(new CalculatedValueMin());
				break;
			case GaugeInputValueFormulas.Max:
				inputValue.CalculatedValues.Add(new CalculatedValueMax());
				break;
			case GaugeInputValueFormulas.Integral:
			{
				CalculatedValueIntegral value2 = new CalculatedValueIntegral();
				inputValue.CalculatedValues.Add(value2);
				break;
			}
			case GaugeInputValueFormulas.RateOfChange:
			{
				CalculatedValueRateOfChange value = new CalculatedValueRateOfChange();
				inputValue.CalculatedValues.Add(value);
				break;
			}
			}
		}

		private double GetValue(InputValue inputValue, GaugeInputValue gaugeInputValue)
		{
			GaugeInputValueFormulas formula = this.GetFormula(gaugeInputValue);
			if (this.IsBuiltInFormula(formula))
			{
				return this.GetBuiltInFormulaValue(inputValue, gaugeInputValue, formula);
			}
			switch (formula)
			{
			case GaugeInputValueFormulas.Median:
				if (inputValue.History.Count <= 0)
				{
					break;
				}
				return FormulaHelper.Median(this.GetValues(inputValue.History));
			case GaugeInputValueFormulas.Variance:
				if (inputValue.History.Count <= 0)
				{
					break;
				}
				return FormulaHelper.Variance(this.GetValues(inputValue.History), this.IsSampleVariance(gaugeInputValue));
			case GaugeInputValueFormulas.Percentile:
			{
				if (inputValue.History.Count <= 0)
				{
					break;
				}
				double[] percentileParameters = this.GetPercentileParameters(gaugeInputValue);
				if (percentileParameters == null)
				{
					break;
				}
				return FormulaHelper.Percentile(this.GetValues(inputValue.History), percentileParameters)[0];
			}
			case GaugeInputValueFormulas.Average:
				if (inputValue.History.Count <= 0)
				{
					break;
				}
				return FormulaHelper.Mean(this.GetValues(inputValue.History));
			case GaugeInputValueFormulas.OpenClose:
				if (inputValue.History.Count <= 0)
				{
					break;
				}
				return inputValue.History[0].Value;
			}
			return double.NaN;
		}

		private double[] GetPercentileParameters(GaugeInputValue gaugeInputValue)
		{
			double num = double.NaN;
			double num2 = double.NaN;
			double[] result = null;
			bool minPercent = this.GetMinPercent(gaugeInputValue, out num);
			bool maxPercent = this.GetMaxPercent(gaugeInputValue, out num2);
			if (minPercent && maxPercent)
			{
				result = new double[2]
				{
					num,
					num2
				};
			}
			else if (minPercent)
			{
				result = new double[1]
				{
					num
				};
			}
			else if (maxPercent)
			{
				result = new double[1]
				{
					num2
				};
			}
			return result;
		}

		private double[] GetValues(HistoryCollection historyColletion)
		{
			double[] array = new double[historyColletion.Count];
			for (int i = 0; i < historyColletion.Count; i++)
			{
				array[i] = historyColletion[i].Value;
			}
			return array;
		}

		private float GetPanelItemLeft(GaugePanelItem gaugePanelItem)
		{
			if (gaugePanelItem.Left != null)
			{
				if (!gaugePanelItem.Left.IsExpression)
				{
					return (float)gaugePanelItem.Left.Value;
				}
				return (float)gaugePanelItem.Instance.Left;
			}
			return 0f;
		}

		private float GetPanelItemTop(GaugePanelItem gaugePanelItem)
		{
			if (gaugePanelItem.Top != null)
			{
				if (!gaugePanelItem.Top.IsExpression)
				{
					return (float)gaugePanelItem.Top.Value;
				}
				return (float)gaugePanelItem.Instance.Top;
			}
			return 0f;
		}

		private float GetPanelItemWidth(GaugePanelItem gaugePanelItem)
		{
			if (gaugePanelItem.Width != null)
			{
				if (!gaugePanelItem.Width.IsExpression)
				{
					return (float)gaugePanelItem.Width.Value;
				}
				return (float)gaugePanelItem.Instance.Width;
			}
			return 0f;
		}

		private float GetPanelItemHeight(GaugePanelItem gaugePanelItem)
		{
			if (gaugePanelItem.Height != null)
			{
				if (!gaugePanelItem.Height.IsExpression)
				{
					return (float)gaugePanelItem.Height.Value;
				}
				return (float)gaugePanelItem.Instance.Height;
			}
			return 0f;
		}

		private bool GetPanelItemHidden(GaugePanelItem gaugePanelItem)
		{
			if (gaugePanelItem.Hidden != null)
			{
				if (!gaugePanelItem.Hidden.IsExpression)
				{
					return gaugePanelItem.Hidden.Value;
				}
				return gaugePanelItem.Instance.Hidden;
			}
			return false;
		}

		private bool GetPanelItemZIndex(GaugePanelItem gaugePanelItem, out int zIndex)
		{
			if (gaugePanelItem.ZIndex != null)
			{
				if (!gaugePanelItem.ZIndex.IsExpression)
				{
					zIndex = gaugePanelItem.ZIndex.Value;
				}
				else
				{
					zIndex = gaugePanelItem.Instance.ZIndex;
				}
				return true;
			}
			zIndex = 0;
			return false;
		}

		private bool GetPanelItemToolTip(GaugePanelItem gaugePanelItem, out string toolTip)
		{
			toolTip = null;
			ReportStringProperty toolTip2 = gaugePanelItem.ToolTip;
			if (toolTip2 != null)
			{
				if (!toolTip2.IsExpression)
				{
					toolTip = toolTip2.Value;
				}
				else
				{
					toolTip = gaugePanelItem.Instance.ToolTip;
				}
			}
			return toolTip != null;
		}

		private AntiAliasing GetAntiAliasing(GaugeAntiAliasings gaugeAntiAliasing)
		{
			switch (gaugeAntiAliasing)
			{
			case GaugeAntiAliasings.Graphics:
				return AntiAliasing.Graphics;
			case GaugeAntiAliasings.None:
				return AntiAliasing.None;
			case GaugeAntiAliasings.Text:
				return AntiAliasing.Text;
			default:
				return AntiAliasing.All;
			}
		}

		private TextAntiAliasingQuality GetTextAntiAliasingQuality(TextAntiAliasingQualities textAntiAliasingQuality)
		{
			switch (textAntiAliasingQuality)
			{
			case TextAntiAliasingQualities.Normal:
				return TextAntiAliasingQuality.Normal;
			case TextAntiAliasingQualities.SystemDefault:
				return TextAntiAliasingQuality.SystemDefault;
			default:
				return TextAntiAliasingQuality.High;
			}
		}

		private BarStart GetBarStart(GaugeBarStarts barStart)
		{
			if (barStart == GaugeBarStarts.Zero)
			{
				return BarStart.Zero;
			}
			return BarStart.ScaleStart;
		}

		private MarkerStyle GetMarkerStyle(GaugeMarkerStyles markerStyle)
		{
			switch (markerStyle)
			{
			case GaugeMarkerStyles.Circle:
				return MarkerStyle.Circle;
			case GaugeMarkerStyles.Diamond:
				return MarkerStyle.Diamond;
			case GaugeMarkerStyles.None:
				return MarkerStyle.None;
			case GaugeMarkerStyles.Pentagon:
				return MarkerStyle.Pentagon;
			case GaugeMarkerStyles.Rectangle:
				return MarkerStyle.Rectangle;
			case GaugeMarkerStyles.Star:
				return MarkerStyle.Star;
			case GaugeMarkerStyles.Trapezoid:
				return MarkerStyle.Trapezoid;
			case GaugeMarkerStyles.Wedge:
				return MarkerStyle.Wedge;
			default:
				return MarkerStyle.Triangle;
			}
		}

		private Placement GetPlacement(GaugePointerPlacements placement)
		{
			switch (placement)
			{
			case GaugePointerPlacements.Inside:
				return Placement.Inside;
			case GaugePointerPlacements.Outside:
				return Placement.Outside;
			default:
				return Placement.Cross;
			}
		}

		private Placement GetPlacement(ScaleRangePlacements placement)
		{
			switch (placement)
			{
			case ScaleRangePlacements.Cross:
				return Placement.Cross;
			case ScaleRangePlacements.Outside:
				return Placement.Outside;
			default:
				return Placement.Inside;
			}
		}

		private Placement GetPlacement(GaugeLabelPlacements placement)
		{
			switch (placement)
			{
			case GaugeLabelPlacements.Cross:
				return Placement.Cross;
			case GaugeLabelPlacements.Outside:
				return Placement.Outside;
			default:
				return Placement.Inside;
			}
		}

		private CircularPointerType GetCircularPointerType(RadialPointerTypes pointerType)
		{
			switch (pointerType)
			{
			case RadialPointerTypes.Bar:
				return CircularPointerType.Bar;
			case RadialPointerTypes.Marker:
				return CircularPointerType.Marker;
			default:
				return CircularPointerType.Needle;
			}
		}

		private NeedleStyle GetNeedleStyle(RadialPointerNeedleStyles needleStyle)
		{
			switch (needleStyle)
			{
			case RadialPointerNeedleStyles.Rectangular:
				return NeedleStyle.Style2;
			case RadialPointerNeedleStyles.TaperedWithTail:
				return NeedleStyle.Style3;
			case RadialPointerNeedleStyles.Tapered:
				return NeedleStyle.Style4;
			case RadialPointerNeedleStyles.ArrowWithTail:
				return NeedleStyle.Style5;
			case RadialPointerNeedleStyles.Arrow:
				return NeedleStyle.Style6;
			case RadialPointerNeedleStyles.StealthArrowWithTail:
				return NeedleStyle.Style7;
			case RadialPointerNeedleStyles.StealthArrow:
				return NeedleStyle.Style8;
			case RadialPointerNeedleStyles.TaperedWithStealthArrow:
				return NeedleStyle.Style9;
			case RadialPointerNeedleStyles.StealthArrowWithWideTail:
				return NeedleStyle.Style10;
			case RadialPointerNeedleStyles.TaperedWithRoundedPoint:
				return NeedleStyle.Style11;
			default:
				return NeedleStyle.Style1;
			}
		}

		private BackFrameStyle GetFrameStyle(GaugeFrameStyles gaugeFrameStyles)
		{
			switch (gaugeFrameStyles)
			{
			case GaugeFrameStyles.Edged:
				return BackFrameStyle.Edged;
			case GaugeFrameStyles.Simple:
				return BackFrameStyle.Simple;
			default:
				return BackFrameStyle.None;
			}
		}

		private BackFrameShape GetFrameShape(GaugeFrameShapes gaugeFrameShapes)
		{
			switch (gaugeFrameShapes)
			{
			case GaugeFrameShapes.Rectangular:
				return BackFrameShape.Rectangular;
			case GaugeFrameShapes.RoundedRectangular:
				return BackFrameShape.RoundedRectangular;
			case GaugeFrameShapes.AutoShape:
				return BackFrameShape.AutoShape;
			case GaugeFrameShapes.CustomCircular1:
				return BackFrameShape.CustomCircular1;
			case GaugeFrameShapes.CustomCircular2:
				return BackFrameShape.CustomCircular2;
			case GaugeFrameShapes.CustomCircular3:
				return BackFrameShape.CustomCircular3;
			case GaugeFrameShapes.CustomCircular4:
				return BackFrameShape.CustomCircular4;
			case GaugeFrameShapes.CustomCircular5:
				return BackFrameShape.CustomCircular5;
			case GaugeFrameShapes.CustomCircular6:
				return BackFrameShape.CustomCircular6;
			case GaugeFrameShapes.CustomCircular7:
				return BackFrameShape.CustomCircular7;
			case GaugeFrameShapes.CustomCircular8:
				return BackFrameShape.CustomCircular8;
			case GaugeFrameShapes.CustomCircular9:
				return BackFrameShape.CustomCircular9;
			case GaugeFrameShapes.CustomCircular10:
				return BackFrameShape.CustomCircular10;
			case GaugeFrameShapes.CustomCircular11:
				return BackFrameShape.CustomCircular11;
			case GaugeFrameShapes.CustomCircular12:
				return BackFrameShape.CustomCircular12;
			case GaugeFrameShapes.CustomCircular13:
				return BackFrameShape.CustomCircular13;
			case GaugeFrameShapes.CustomCircular14:
				return BackFrameShape.CustomCircular14;
			case GaugeFrameShapes.CustomCircular15:
				return BackFrameShape.CustomCircular15;
			case GaugeFrameShapes.CustomSemiCircularN1:
				return BackFrameShape.CustomSemiCircularN1;
			case GaugeFrameShapes.CustomSemiCircularN2:
				return BackFrameShape.CustomSemiCircularN2;
			case GaugeFrameShapes.CustomSemiCircularN3:
				return BackFrameShape.CustomSemiCircularN3;
			case GaugeFrameShapes.CustomSemiCircularN4:
				return BackFrameShape.CustomSemiCircularN4;
			case GaugeFrameShapes.CustomSemiCircularS1:
				return BackFrameShape.CustomSemiCircularS1;
			case GaugeFrameShapes.CustomSemiCircularS2:
				return BackFrameShape.CustomSemiCircularS2;
			case GaugeFrameShapes.CustomSemiCircularS3:
				return BackFrameShape.CustomSemiCircularS3;
			case GaugeFrameShapes.CustomSemiCircularS4:
				return BackFrameShape.CustomSemiCircularS4;
			case GaugeFrameShapes.CustomSemiCircularE1:
				return BackFrameShape.CustomSemiCircularE1;
			case GaugeFrameShapes.CustomSemiCircularE2:
				return BackFrameShape.CustomSemiCircularE2;
			case GaugeFrameShapes.CustomSemiCircularE3:
				return BackFrameShape.CustomSemiCircularE3;
			case GaugeFrameShapes.CustomSemiCircularE4:
				return BackFrameShape.CustomSemiCircularE4;
			case GaugeFrameShapes.CustomSemiCircularW1:
				return BackFrameShape.CustomSemiCircularW1;
			case GaugeFrameShapes.CustomSemiCircularW2:
				return BackFrameShape.CustomSemiCircularW2;
			case GaugeFrameShapes.CustomSemiCircularW3:
				return BackFrameShape.CustomSemiCircularW3;
			case GaugeFrameShapes.CustomSemiCircularW4:
				return BackFrameShape.CustomSemiCircularW4;
			case GaugeFrameShapes.CustomQuarterCircularNW1:
				return BackFrameShape.CustomQuarterCircularNW1;
			case GaugeFrameShapes.CustomQuarterCircularNW2:
				return BackFrameShape.CustomQuarterCircularNW2;
			case GaugeFrameShapes.CustomQuarterCircularNW3:
				return BackFrameShape.CustomQuarterCircularNW3;
			case GaugeFrameShapes.CustomQuarterCircularNW4:
				return BackFrameShape.CustomQuarterCircularNW4;
			case GaugeFrameShapes.CustomQuarterCircularNE1:
				return BackFrameShape.CustomQuarterCircularNE1;
			case GaugeFrameShapes.CustomQuarterCircularNE2:
				return BackFrameShape.CustomQuarterCircularNE2;
			case GaugeFrameShapes.CustomQuarterCircularNE3:
				return BackFrameShape.CustomQuarterCircularNE3;
			case GaugeFrameShapes.CustomQuarterCircularNE4:
				return BackFrameShape.CustomQuarterCircularNE4;
			case GaugeFrameShapes.CustomQuarterCircularSW1:
				return BackFrameShape.CustomQuarterCircularSW1;
			case GaugeFrameShapes.CustomQuarterCircularSW2:
				return BackFrameShape.CustomQuarterCircularSW2;
			case GaugeFrameShapes.CustomQuarterCircularSW3:
				return BackFrameShape.CustomQuarterCircularSW3;
			case GaugeFrameShapes.CustomQuarterCircularSW4:
				return BackFrameShape.CustomQuarterCircularSW4;
			case GaugeFrameShapes.CustomQuarterCircularSE1:
				return BackFrameShape.CustomQuarterCircularSE1;
			case GaugeFrameShapes.CustomQuarterCircularSE2:
				return BackFrameShape.CustomQuarterCircularSE2;
			case GaugeFrameShapes.CustomQuarterCircularSE3:
				return BackFrameShape.CustomQuarterCircularSE3;
			case GaugeFrameShapes.CustomQuarterCircularSE4:
				return BackFrameShape.CustomQuarterCircularSE4;
			default:
				return BackFrameShape.Circular;
			}
		}

		private GlassEffect GetGlassEffect(GaugeGlassEffects gaugeGlassEffects)
		{
			if (gaugeGlassEffects == GaugeGlassEffects.Simple)
			{
				return GlassEffect.Simple;
			}
			return GlassEffect.None;
		}

		private FontUnit GetFontUnit(bool useFontAsPercent)
		{
			if (useFontAsPercent)
			{
				return FontUnit.Percent;
			}
			return FontUnit.Default;
		}

		private CapStyle GetCapStyle(GaugeCapStyles capStyle)
		{
			switch (capStyle)
			{
			case GaugeCapStyles.Rounded:
				return CapStyle.CustomCap1;
			case GaugeCapStyles.RoundedLight:
				return CapStyle.CustomCap2;
			case GaugeCapStyles.RoundedWithAdditionalTop:
				return CapStyle.CustomCap3;
			case GaugeCapStyles.RoundedWithWideIndentation:
				return CapStyle.CustomCap4;
			case GaugeCapStyles.FlattenedWithIndentation:
				return CapStyle.CustomCap5;
			case GaugeCapStyles.FlattenedWithWideIndentation:
				return CapStyle.CustomCap6;
			case GaugeCapStyles.RoundedGlossyWithIndentation:
				return CapStyle.CustomCap7;
			case GaugeCapStyles.RoundedWithIndentation:
				return CapStyle.CustomCap8;
			default:
				return CapStyle.Simple;
			}
		}

		private MarkerStyle GetMarkerStyle(GaugeTickMarkShapes shape)
		{
			switch (shape)
			{
			case GaugeTickMarkShapes.Circle:
				return MarkerStyle.Circle;
			case GaugeTickMarkShapes.Diamond:
				return MarkerStyle.Diamond;
			case GaugeTickMarkShapes.None:
				return MarkerStyle.None;
			case GaugeTickMarkShapes.Pentagon:
				return MarkerStyle.Pentagon;
			case GaugeTickMarkShapes.Star:
				return MarkerStyle.Star;
			case GaugeTickMarkShapes.Trapezoid:
				return MarkerStyle.Trapezoid;
			case GaugeTickMarkShapes.Triangle:
				return MarkerStyle.Triangle;
			case GaugeTickMarkShapes.Wedge:
				return MarkerStyle.Wedge;
			default:
				return MarkerStyle.Rectangle;
			}
		}

		private GaugeDashStyle GetDashStyle(Border border)
		{
			switch (MappingHelper.GetStyleBorderStyle(border))
			{
			case BorderStyles.DashDot:
				return GaugeDashStyle.DashDot;
			case BorderStyles.DashDotDot:
				return GaugeDashStyle.DashDotDot;
			case BorderStyles.Dashed:
				return GaugeDashStyle.Dash;
			case BorderStyles.Dotted:
				return GaugeDashStyle.Dot;
			case BorderStyles.Solid:
			case BorderStyles.Double:
				return GaugeDashStyle.Solid;
			default:
				return GaugeDashStyle.NotSet;
			}
		}

		private GaugeHatchStyle GetHatchStyle(Style style, StyleInstance styleInstance)
		{
			switch (MappingHelper.GetStyleBackgroundHatchType(style, styleInstance))
			{
			case BackgroundHatchTypes.BackwardDiagonal:
				return GaugeHatchStyle.BackwardDiagonal;
			case BackgroundHatchTypes.Cross:
				return GaugeHatchStyle.Cross;
			case BackgroundHatchTypes.DarkDownwardDiagonal:
				return GaugeHatchStyle.DarkDownwardDiagonal;
			case BackgroundHatchTypes.DarkHorizontal:
				return GaugeHatchStyle.DarkHorizontal;
			case BackgroundHatchTypes.DarkUpwardDiagonal:
				return GaugeHatchStyle.DarkUpwardDiagonal;
			case BackgroundHatchTypes.DarkVertical:
				return GaugeHatchStyle.DarkVertical;
			case BackgroundHatchTypes.DashedDownwardDiagonal:
				return GaugeHatchStyle.DashedDownwardDiagonal;
			case BackgroundHatchTypes.DashedHorizontal:
				return GaugeHatchStyle.DashedHorizontal;
			case BackgroundHatchTypes.DashedUpwardDiagonal:
				return GaugeHatchStyle.DashedUpwardDiagonal;
			case BackgroundHatchTypes.DashedVertical:
				return GaugeHatchStyle.DashedVertical;
			case BackgroundHatchTypes.DiagonalBrick:
				return GaugeHatchStyle.DiagonalBrick;
			case BackgroundHatchTypes.DiagonalCross:
				return GaugeHatchStyle.DiagonalCross;
			case BackgroundHatchTypes.Divot:
				return GaugeHatchStyle.Divot;
			case BackgroundHatchTypes.DottedDiamond:
				return GaugeHatchStyle.DottedDiamond;
			case BackgroundHatchTypes.DottedGrid:
				return GaugeHatchStyle.DottedGrid;
			case BackgroundHatchTypes.ForwardDiagonal:
				return GaugeHatchStyle.ForwardDiagonal;
			case BackgroundHatchTypes.Horizontal:
				return GaugeHatchStyle.Horizontal;
			case BackgroundHatchTypes.HorizontalBrick:
				return GaugeHatchStyle.HorizontalBrick;
			case BackgroundHatchTypes.LargeCheckerBoard:
				return GaugeHatchStyle.LargeCheckerBoard;
			case BackgroundHatchTypes.LargeConfetti:
				return GaugeHatchStyle.LargeConfetti;
			case BackgroundHatchTypes.LargeGrid:
				return GaugeHatchStyle.LargeGrid;
			case BackgroundHatchTypes.LightDownwardDiagonal:
				return GaugeHatchStyle.LightDownwardDiagonal;
			case BackgroundHatchTypes.LightHorizontal:
				return GaugeHatchStyle.LightHorizontal;
			case BackgroundHatchTypes.LightUpwardDiagonal:
				return GaugeHatchStyle.LightUpwardDiagonal;
			case BackgroundHatchTypes.LightVertical:
				return GaugeHatchStyle.LightVertical;
			case BackgroundHatchTypes.NarrowHorizontal:
				return GaugeHatchStyle.NarrowHorizontal;
			case BackgroundHatchTypes.NarrowVertical:
				return GaugeHatchStyle.NarrowVertical;
			case BackgroundHatchTypes.OutlinedDiamond:
				return GaugeHatchStyle.OutlinedDiamond;
			case BackgroundHatchTypes.Percent05:
				return GaugeHatchStyle.Percent05;
			case BackgroundHatchTypes.Percent10:
				return GaugeHatchStyle.Percent10;
			case BackgroundHatchTypes.Percent20:
				return GaugeHatchStyle.Percent20;
			case BackgroundHatchTypes.Percent25:
				return GaugeHatchStyle.Percent25;
			case BackgroundHatchTypes.Percent30:
				return GaugeHatchStyle.Percent30;
			case BackgroundHatchTypes.Percent40:
				return GaugeHatchStyle.Percent40;
			case BackgroundHatchTypes.Percent50:
				return GaugeHatchStyle.Percent50;
			case BackgroundHatchTypes.Percent60:
				return GaugeHatchStyle.Percent60;
			case BackgroundHatchTypes.Percent70:
				return GaugeHatchStyle.Percent70;
			case BackgroundHatchTypes.Percent75:
				return GaugeHatchStyle.Percent75;
			case BackgroundHatchTypes.Percent80:
				return GaugeHatchStyle.Percent80;
			case BackgroundHatchTypes.Percent90:
				return GaugeHatchStyle.Percent90;
			case BackgroundHatchTypes.Plaid:
				return GaugeHatchStyle.Plaid;
			case BackgroundHatchTypes.Shingle:
				return GaugeHatchStyle.Shingle;
			case BackgroundHatchTypes.SmallCheckerBoard:
				return GaugeHatchStyle.SmallCheckerBoard;
			case BackgroundHatchTypes.SmallConfetti:
				return GaugeHatchStyle.SmallConfetti;
			case BackgroundHatchTypes.SmallGrid:
				return GaugeHatchStyle.SmallGrid;
			case BackgroundHatchTypes.SolidDiamond:
				return GaugeHatchStyle.SolidDiamond;
			case BackgroundHatchTypes.Sphere:
				return GaugeHatchStyle.Sphere;
			case BackgroundHatchTypes.Trellis:
				return GaugeHatchStyle.Trellis;
			case BackgroundHatchTypes.Vertical:
				return GaugeHatchStyle.Vertical;
			case BackgroundHatchTypes.Wave:
				return GaugeHatchStyle.Wave;
			case BackgroundHatchTypes.Weave:
				return GaugeHatchStyle.Weave;
			case BackgroundHatchTypes.WideDownwardDiagonal:
				return GaugeHatchStyle.WideDownwardDiagonal;
			case BackgroundHatchTypes.WideUpwardDiagonal:
				return GaugeHatchStyle.WideUpwardDiagonal;
			case BackgroundHatchTypes.ZigZag:
				return GaugeHatchStyle.ZigZag;
			default:
				return GaugeHatchStyle.None;
			}
		}

		private GradientType GetGradientType(Style style, StyleInstance styleInstance)
		{
			switch (MappingHelper.GetStyleBackGradientType(style, styleInstance))
			{
			case BackgroundGradients.Center:
				return GradientType.Center;
			case BackgroundGradients.DiagonalLeft:
				return GradientType.DiagonalLeft;
			case BackgroundGradients.DiagonalRight:
				return GradientType.DiagonalRight;
			case BackgroundGradients.HorizontalCenter:
				return GradientType.HorizontalCenter;
			case BackgroundGradients.LeftRight:
				return GradientType.LeftRight;
			case BackgroundGradients.TopBottom:
				return GradientType.TopBottom;
			case BackgroundGradients.VerticalCenter:
				return GradientType.VerticalCenter;
			default:
				return GradientType.None;
			}
		}

		private RangeGradientType GetRangeGradientType(BackgroundGradientTypes gradient)
		{
			switch (gradient)
			{
			case BackgroundGradientTypes.Center:
				return RangeGradientType.Center;
			case BackgroundGradientTypes.DiagonalLeft:
				return RangeGradientType.DiagonalLeft;
			case BackgroundGradientTypes.DiagonalRight:
				return RangeGradientType.DiagonalRight;
			case BackgroundGradientTypes.HorizontalCenter:
				return RangeGradientType.HorizontalCenter;
			case BackgroundGradientTypes.LeftRight:
				return RangeGradientType.LeftRight;
			case BackgroundGradientTypes.TopBottom:
				return RangeGradientType.TopBottom;
			case BackgroundGradientTypes.VerticalCenter:
				return RangeGradientType.VerticalCenter;
			case BackgroundGradientTypes.StartToEnd:
				return RangeGradientType.StartToEnd;
			default:
				return RangeGradientType.None;
			}
		}

		private GaugeOrientation GetGaugeOrientation(GaugeOrientations gaugeOrientation)
		{
			switch (gaugeOrientation)
			{
			case GaugeOrientations.Horizontal:
				return GaugeOrientation.Horizontal;
			case GaugeOrientations.Vertical:
				return GaugeOrientation.Vertical;
			default:
				return GaugeOrientation.Auto;
			}
		}

		private LinearPointerType GetLinearPointerType(LinearPointerTypes type)
		{
			switch (type)
			{
			case LinearPointerTypes.Bar:
				return LinearPointerType.Bar;
			case LinearPointerTypes.Thermometer:
				return LinearPointerType.Thermometer;
			default:
				return LinearPointerType.Marker;
			}
		}

		private ThermometerStyle GetThermometerStyle(GaugeThermometerStyles thermometerStyle)
		{
			if (thermometerStyle == GaugeThermometerStyles.Flask)
			{
				return ThermometerStyle.Flask;
			}
			return ThermometerStyle.Standard;
		}

		private ResizeMode GetResizeMode(GaugeResizeModes resizeMode)
		{
			if (resizeMode == GaugeResizeModes.None)
			{
				return ResizeMode.None;
			}
			return ResizeMode.AutoFit;
		}

		private string GetParentName(string parentItemName)
		{
			string[] array = parentItemName.Split('.');
			if (array.Length == 2 && array[0] == GaugeMapper.m_RadialGaugesName)
			{
				return GaugeMapper.m_CircularGaugesName + '.' + array[1];
			}
			return parentItemName;
		}

		private string AddNamedImage(BaseGaugeImage topImage)
		{
			System.Drawing.Image imageFromStream = this.GetImageFromStream(topImage);
			if (imageFromStream == null)
			{
				return "";
			}
			string text = "image" + this.m_coreGaugeContainer.NamedImages.Count.ToString(CultureInfo.InvariantCulture);
			NamedImage value = new NamedImage(text, imageFromStream);
			this.m_coreGaugeContainer.NamedImages.Add(value);
			return text;
		}

		private int GetValidShadowOffset(int shadowOffset)
		{
			return Math.Min(shadowOffset, 100);
		}

		private string FormatNumber(object sender, double value, string format)
		{
			if (this.m_formatter == null)
			{
				this.m_formatter = new Formatter(this.m_gaugePanel.GaugePanelDef.StyleClass, this.m_gaugePanel.RenderingContext.OdpContext, ObjectType.GaugePanel, this.m_gaugePanel.Name);
			}
			return this.m_formatter.FormatValue(value, format, TypeCode.Double);
		}
	}
}
