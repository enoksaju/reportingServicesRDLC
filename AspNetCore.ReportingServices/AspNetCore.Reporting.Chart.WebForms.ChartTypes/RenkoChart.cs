using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;

namespace AspNetCore.Reporting.Chart.WebForms.ChartTypes
{
	internal class RenkoChart : IChartType
	{
		public virtual string Name
		{
			get
			{
				return "Renko";
			}
		}

		public virtual bool Stacked
		{
			get
			{
				return false;
			}
		}

		public virtual bool SupportStackedGroups
		{
			get
			{
				return false;
			}
		}

		public bool StackSign
		{
			get
			{
				return false;
			}
		}

		public virtual bool RequireAxes
		{
			get
			{
				return true;
			}
		}

		public bool SecondYScale
		{
			get
			{
				return false;
			}
		}

		public bool CircularChartArea
		{
			get
			{
				return false;
			}
		}

		public virtual bool SupportLogarithmicAxes
		{
			get
			{
				return true;
			}
		}

		public virtual bool SwitchValueAxes
		{
			get
			{
				return false;
			}
		}

		public bool SideBySideSeries
		{
			get
			{
				return false;
			}
		}

		public virtual bool DataPointsInLegend
		{
			get
			{
				return false;
			}
		}

		public virtual bool ZeroCrossing
		{
			get
			{
				return false;
			}
		}

		public virtual bool ApplyPaletteColorsToPoints
		{
			get
			{
				return false;
			}
		}

		public virtual bool ExtraYValuesConnectedToYAxis
		{
			get
			{
				return true;
			}
		}

		public virtual bool HundredPercent
		{
			get
			{
				return false;
			}
		}

		public virtual bool HundredPercentSupportNegative
		{
			get
			{
				return false;
			}
		}

		public virtual int YValuesPerPoint
		{
			get
			{
				return 1;
			}
		}

		internal static void PrepareData(Series series, IServiceContainer serviceContainer)
		{
			if (string.Compare(series.ChartTypeName, "Renko", StringComparison.OrdinalIgnoreCase) == 0 && series.IsVisible())
			{
				Chart chart = (Chart)serviceContainer.GetService(typeof(Chart));
				if (chart == null)
				{
					throw new InvalidOperationException(SR.ExceptionRenkoNullReference);
				}
				ChartArea chartArea = chart.ChartAreas[series.ChartArea];
				foreach (Series item in chart.Series)
				{
					if (item.IsVisible() && item != series && chartArea == chart.ChartAreas[item.ChartArea])
					{
						throw new InvalidOperationException(SR.ExceptionRenkoCanNotCobine);
					}
				}
				Series series3 = new Series("RENKO_ORIGINAL_DATA_" + series.Name, series.YValuesPerPoint);
				series3.Enabled = false;
				series3.ShowInLegend = false;
				chart.Series.Add(series3);
				foreach (DataPoint point in series.Points)
				{
					series3.Points.Add(point);
				}
				series.Points.Clear();
				if (series.IsAttributeSet("TempDesignData"))
				{
					((DataPointAttributes)series3)["TempDesignData"] = "true";
				}
				((DataPointAttributes)series)["OldXValueIndexed"] = series.XValueIndexed.ToString(CultureInfo.InvariantCulture);
				((DataPointAttributes)series)["OldYValuesPerPoint"] = series.YValuesPerPoint.ToString(CultureInfo.InvariantCulture);
				series.ChartType = SeriesChartType.RangeColumn;
				series.XValueIndexed = true;
				series.YValuesPerPoint = 2;
				if (series.ChartArea.Length > 0 && series.IsXValueDateTime())
				{
					Axis axis = chartArea.GetAxis(AxisName.X, series.XAxisType, series.XSubAxisName);
					if (axis.Interval == 0.0 && axis.IntervalType == DateTimeIntervalType.Auto)
					{
						bool flag = false;
						double num = 1.7976931348623157E+308;
						double num2 = -1.7976931348623157E+308;
						foreach (DataPoint point2 in series3.Points)
						{
							if (!point2.Empty)
							{
								if (point2.XValue != 0.0)
								{
									flag = true;
								}
								if (point2.XValue > num2)
								{
									num2 = point2.XValue;
								}
								if (point2.XValue < num)
								{
									num = point2.XValue;
								}
							}
						}
						if (flag)
						{
							((DataPointAttributes)series)["OldAutomaticXAxisInterval"] = "true";
							DateTimeIntervalType intervalType = DateTimeIntervalType.Auto;
							axis.interval = ((AxisScale)axis).CalcInterval(num, num2, true, out intervalType, series.XValueType);
							axis.intervalType = intervalType;
						}
					}
				}
				RenkoChart.FillRenkoData(series, series3);
			}
		}

		internal static bool UnPrepareData(Series series, IServiceContainer serviceContainer)
		{
			if (series.Name.StartsWith("RENKO_ORIGINAL_DATA_", StringComparison.Ordinal))
			{
				Chart chart = (Chart)serviceContainer.GetService(typeof(Chart));
				if (chart == null)
				{
					throw new InvalidOperationException(SR.ExceptionRenkoNullReference);
				}
				Series series2 = chart.Series[series.Name.Substring(20)];
				series2.Points.Clear();
				if (!series.IsAttributeSet("TempDesignData"))
				{
					foreach (DataPoint point in series.Points)
					{
						series2.Points.Add(point);
					}
				}
				series2.ChartType = SeriesChartType.Renko;
				try
				{
					series2.XValueIndexed = bool.Parse(((DataPointAttributes)series2)["OldXValueIndexed"]);
					series2.YValuesPerPoint = int.Parse(((DataPointAttributes)series2)["OldYValuesPerPoint"], CultureInfo.InvariantCulture);
				}
				catch
				{
				}
				series2.DeleteAttribute("OldXValueIndexed");
				series2.DeleteAttribute("OldYValuesPerPoint");
				((DataPointAttributes)series)["OldAutomaticXAxisInterval"] = "true";
				if (series2.IsAttributeSet("OldAutomaticXAxisInterval"))
				{
					series2.DeleteAttribute("OldAutomaticXAxisInterval");
					if (series2.ChartArea.Length > 0)
					{
						ChartArea chartArea = chart.ChartAreas[series2.ChartArea];
						Axis axis = chartArea.GetAxis(AxisName.X, series2.XAxisType, series2.XSubAxisName);
						axis.interval = 0.0;
						axis.intervalType = DateTimeIntervalType.Auto;
					}
				}
				chart.Series.Remove(series);
				return true;
			}
			if (series.IsAttributeSet("CurrentBoxSize"))
			{
				series.DeleteAttribute("CurrentBoxSize");
			}
			return false;
		}

		private static double GetBoxSize(Series series, Series originalData, int yValueIndex)
		{
			double num = 1.0;
			double num2 = 4.0;
			bool flag = true;
			if (series.IsAttributeSet("BoxSize"))
			{
				string text = ((DataPointAttributes)series)["BoxSize"].Trim();
				bool flag2 = text.EndsWith("%", StringComparison.Ordinal);
				if (flag2)
				{
					text = text.Substring(0, text.Length - 1);
				}
				try
				{
					if (flag2)
					{
						num2 = double.Parse(text, CultureInfo.InvariantCulture);
						flag = false;
					}
					else
					{
						num = double.Parse(text, CultureInfo.InvariantCulture);
						num2 = 0.0;
					}
				}
				catch
				{
					throw new InvalidOperationException(SR.ExceptionRenkoBoxSizeFormatInvalid);
				}
			}
			if (num2 > 0.0)
			{
				num = 1.0;
				double num3 = -1.7976931348623157E+308;
				double num4 = 1.7976931348623157E+308;
				foreach (DataPoint point in originalData.Points)
				{
					if (!point.Empty)
					{
						if (point.YValues[yValueIndex] > num3)
						{
							num3 = point.YValues[yValueIndex];
						}
						if (point.YValues[yValueIndex] < num4)
						{
							num4 = point.YValues[yValueIndex];
						}
					}
				}
				num = ((num4 != num3) ? ((!(num3 - num4 < 1E-06)) ? ((num3 - num4) * (num2 / 100.0)) : 1E-06) : 1.0);
				if (flag)
				{
					double[] array = new double[30]
					{
						1E-06,
						1E-05,
						0.0001,
						0.001,
						0.01,
						0.1,
						0.25,
						0.5,
						1.0,
						2.0,
						2.5,
						3.0,
						4.0,
						5.0,
						7.5,
						10.0,
						15.0,
						20.0,
						25.0,
						50.0,
						100.0,
						200.0,
						500.0,
						1000.0,
						5000.0,
						10000.0,
						50000.0,
						100000.0,
						1000000.0,
						1000000.0
					};
					for (int i = 1; i < array.Length; i++)
					{
						if (num > array[i - 1] && num < array[i])
						{
							num = array[i];
						}
					}
				}
			}
			((DataPointAttributes)series)["CurrentBoxSize"] = num.ToString(CultureInfo.InvariantCulture);
			return num;
		}

		private static void FillRenkoData(Series series, Series originalData)
		{
			int num = 0;
			if (series.IsAttributeSet("UsedYValue"))
			{
				try
				{
					num = int.Parse(((DataPointAttributes)series)["UsedYValue"], CultureInfo.InvariantCulture);
				}
				catch
				{
					throw new InvalidOperationException(SR.ExceptionRenkoUsedYValueFormatInvalid);
				}
				if (num >= series.YValuesPerPoint)
				{
					throw new InvalidOperationException(SR.ExceptionRenkoUsedYValueOutOfRange);
				}
			}
			double boxSize = RenkoChart.GetBoxSize(series, originalData, num);
			double num2 = double.NaN;
			double num3 = double.NaN;
			int num4 = 0;
			foreach (DataPoint point in originalData.Points)
			{
				if (!point.Empty)
				{
					int num5 = 0;
					bool flag = true;
					if (!double.IsNaN(num2) && !double.IsNaN(num3))
					{
						Color color = Color.Transparent;
						string text = ((DataPointAttributes)point)["PriceUpColor"];
						if (text == null)
						{
							text = ((DataPointAttributes)series)["PriceUpColor"];
						}
						if (text != null)
						{
							try
							{
								ColorConverter colorConverter = new ColorConverter();
								color = (Color)colorConverter.ConvertFromString(null, CultureInfo.InvariantCulture, text);
							}
							catch
							{
								throw new InvalidOperationException(SR.ExceptionRenkoUpBrickColorInvalid);
							}
						}
						if (point.YValues[num] >= num3 + boxSize)
						{
							flag = true;
							num5 = (int)Math.Floor((point.YValues[num] - num3) / boxSize);
						}
						else if (point.YValues[num] <= num2 - boxSize)
						{
							flag = false;
							num5 = (int)Math.Floor((num2 - point.YValues[num]) / boxSize);
						}
						while (num5 > 0)
						{
							DataPoint dataPoint2 = point.Clone();
							((DataPointAttributes)dataPoint2)["OriginalPointIndex"] = num4.ToString(CultureInfo.InvariantCulture);
							dataPoint2.series = series;
							dataPoint2.YValues = new double[2];
							dataPoint2.XValue = point.XValue;
							if (flag)
							{
								dataPoint2.YValues[1] = num3;
								dataPoint2.YValues[0] = num3 + boxSize;
								num2 = num3;
								num3 = num2 + boxSize;
								dataPoint2.Color = color;
								if (dataPoint2.BorderWidth < 1)
								{
									dataPoint2.BorderWidth = 1;
								}
								if (dataPoint2.BorderStyle == ChartDashStyle.NotSet)
								{
									dataPoint2.BorderStyle = ChartDashStyle.Solid;
								}
								if ((dataPoint2.BorderColor == Color.Empty || dataPoint2.BorderColor == Color.Transparent) && (dataPoint2.Color == Color.Empty || dataPoint2.Color == Color.Transparent))
								{
									dataPoint2.BorderColor = series.Color;
								}
							}
							else
							{
								dataPoint2.YValues[1] = num2;
								dataPoint2.YValues[0] = num2 - boxSize;
								num3 = num2;
								num2 = num3 - boxSize;
							}
							series.Points.Add(dataPoint2);
							num5--;
						}
						goto IL_029c;
					}
					num3 = point.YValues[num];
					num2 = point.YValues[num];
					num4++;
					continue;
				}
				goto IL_029c;
				IL_029c:
				num4++;
			}
		}

		public virtual void Paint(ChartGraphics graph, CommonElements common, ChartArea area, Series seriesToDraw)
		{
		}

		public virtual LegendImageStyle GetLegendImageStyle(Series series)
		{
			return LegendImageStyle.Rectangle;
		}

		public virtual Image GetImage(ChartTypeRegistry registry)
		{
			return (Image)registry.ResourceManager.GetObject(this.Name + "ChartType");
		}

		public virtual double GetYValue(CommonElements common, ChartArea area, Series series, DataPoint point, int pointIndex, int yValueIndex)
		{
			return point.YValues[yValueIndex];
		}

		public void AddSmartLabelMarkerPositions(CommonElements common, ChartArea area, Series series, ArrayList list)
		{
		}
	}
}
