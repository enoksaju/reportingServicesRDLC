namespace AspNetCore.Reporting.Chart.WebForms
{
	public class ObjectInfo
	{
		public ChartArea ChartArea;

		public Legend Legend;

		public LegendItem LegendItem;

		public Axis Axis;

		public object AxisLabel;

		public Axis AxisLabelAxis;

		public Series Series;

		public DataPoint DataPoint;

		public int PointIndex = -1;

		public Title Title;

		public Annotation Annotation;

		public ContextElementTypes ElementType;

		public object InspectedObject = new HitTestResult();

		public ObjectInfo()
		{
		}

		private static ObjectInfo Inspect(object o, Chart chart, HitTestResult r)
		{
			ObjectInfo objectInfo = new ObjectInfo();
			if (o != null)
			{
				if (o is ChartArea)
				{
					objectInfo.ChartArea = (ChartArea)o;
					objectInfo.ElementType = ContextElementTypes.ChartArea;
				}
				else if (o is CustomLabel || o is Label)
				{
					if (o is CustomLabel)
					{
						objectInfo.AxisLabelAxis = ((CustomLabel)o).GetAxis();
					}
					if (o is Label)
					{
						objectInfo.AxisLabelAxis = ((Label)o).GetAxis();
					}
					objectInfo.AxisLabel = o;
					objectInfo.Axis = objectInfo.AxisLabelAxis;
					objectInfo.ElementType = ContextElementTypes.AxisLabel;
				}
				else
				{
					if (o is Axis)
					{
						objectInfo.Axis = (Axis)o;
						objectInfo.ElementType = ContextElementTypes.Axis;
						{
							foreach (ChartArea chartArea in chart.ChartAreas)
							{
								Axis[] axes = chartArea.Axes;
								foreach (Axis axis in axes)
								{
									if (axis == objectInfo.Axis)
									{
										objectInfo.ChartArea = chartArea;
										break;
									}
								}
							}
							return objectInfo;
						}
					}
					if (o is Grid)
					{
						objectInfo = ObjectInfo.Inspect(((Grid)o).axis, chart, r);
					}
					else
					{
						if (o is Series)
						{
							objectInfo.Series = (Series)o;
							objectInfo.ElementType = ContextElementTypes.Series;
							try
							{
								objectInfo.ChartArea = chart.ChartAreas[objectInfo.Series.ChartArea];
								return objectInfo;
							}
							catch
							{
								return objectInfo;
							}
						}
						if (o is DataPoint)
						{
							objectInfo.DataPoint = (DataPoint)o;
							if (r != null)
							{
								objectInfo.Series = r.Series;
								objectInfo.PointIndex = r.PointIndex;
							}
							else
							{
								foreach (Series item in chart.Series)
								{
									objectInfo.PointIndex = item.Points.IndexOf((DataPoint)o);
									if (objectInfo.PointIndex >= 0)
									{
										objectInfo.Series = item;
										break;
									}
								}
							}
							if (objectInfo.Series != null)
							{
								objectInfo.ChartArea = chart.ChartAreas[objectInfo.Series.ChartArea];
								objectInfo.ElementType = ContextElementTypes.Series;
							}
						}
						else if (o is Legend || o is LegendItem)
						{
							if (o is LegendItem)
							{
								objectInfo.LegendItem = (LegendItem)o;
								objectInfo.Legend = objectInfo.LegendItem.Legend;
								objectInfo.ElementType = ContextElementTypes.Legend;
								string seriesName = ((LegendItem)o).SeriesName;
								if (seriesName != string.Empty)
								{
									try
									{
										objectInfo.Series = chart.Series[seriesName];
									}
									catch
									{
									}
								}
							}
							else
							{
								objectInfo.Legend = (Legend)o;
								objectInfo.ElementType = ContextElementTypes.Legend;
							}
							if (objectInfo.Series != null)
							{
								objectInfo.ChartArea = chart.ChartAreas[objectInfo.Series.ChartArea];
							}
						}
						else if (o is Title)
						{
							objectInfo.Title = (Title)o;
							objectInfo.ElementType = ContextElementTypes.Title;
						}
						else if (o is Annotation)
						{
							objectInfo.Annotation = (Annotation)o;
							objectInfo.ElementType = ContextElementTypes.Annotation;
						}
					}
				}
			}
			return objectInfo;
		}

		public static ObjectInfo Get(object o, Chart chart)
		{
			ObjectInfo objectInfo = new ObjectInfo();
			objectInfo.InspectedObject = o;
			if (o != null)
			{
				if (o is HitTestResult)
				{
					if (((HitTestResult)o).ChartElementType == ChartElementType.Nothing && !(((HitTestResult)o).Object is Series))
					{
						return objectInfo;
					}
					objectInfo = ObjectInfo.Inspect(((HitTestResult)o).Object, chart, (HitTestResult)o);
				}
				else
				{
					objectInfo = ObjectInfo.Inspect(o, chart, null);
				}
				if (o is HitTestResult && objectInfo.ElementType == ContextElementTypes.None)
				{
					HitTestResult hitTestResult = (HitTestResult)o;
					switch (hitTestResult.ChartElementType)
					{
					case ChartElementType.StripLines:
						objectInfo = ObjectInfo.Inspect(hitTestResult.Axis.chartArea, chart, hitTestResult);
						break;
					case ChartElementType.Gridlines:
						objectInfo = ObjectInfo.Inspect(hitTestResult.Axis.chartArea, chart, hitTestResult);
						break;
					case ChartElementType.AxisTitle:
						objectInfo = ObjectInfo.Inspect(hitTestResult.Axis, chart, hitTestResult);
						break;
					case ChartElementType.AxisLabelImage:
						objectInfo = ObjectInfo.Inspect(hitTestResult.Object, chart, hitTestResult);
						break;
					case ChartElementType.AxisLabels:
						objectInfo = ObjectInfo.Inspect(hitTestResult.Object, chart, hitTestResult);
						break;
					case ChartElementType.TickMarks:
						objectInfo = ObjectInfo.Inspect(hitTestResult.Axis, chart, hitTestResult);
						break;
					case ChartElementType.DataPointLabel:
						objectInfo = ObjectInfo.Inspect(hitTestResult.Series, chart, hitTestResult);
						break;
					case ChartElementType.DataPoint:
						objectInfo = ObjectInfo.Inspect(hitTestResult.Series, chart, hitTestResult);
						break;
					}
				}
			}
			objectInfo.InspectedObject = o;
			return objectInfo;
		}

		public object GetContextObject()
		{
			object result = null;
			switch (this.ElementType)
			{
			case ContextElementTypes.ChartArea:
				result = this.ChartArea;
				break;
			case ContextElementTypes.Series:
				result = this.Series;
				break;
			case ContextElementTypes.Axis:
				result = this.Axis;
				break;
			case ContextElementTypes.AxisLabel:
				result = this.AxisLabel;
				break;
			case ContextElementTypes.Title:
				result = this.Title;
				break;
			case ContextElementTypes.Annotation:
				result = this.Annotation;
				break;
			case ContextElementTypes.Legend:
				result = this.Legend;
				break;
			}
			return result;
		}

		public Axis GetAxis()
		{
			if (this.Axis != null)
			{
				return this.Axis;
			}
			if (this.AxisLabelAxis != null)
			{
				return this.AxisLabelAxis;
			}
			return null;
		}

		public object GetContextObjectNoLabel()
		{
			ContextElementTypes elementType = this.ElementType;
			if (elementType == ContextElementTypes.AxisLabel)
			{
				return this.AxisLabelAxis;
			}
			return this.GetContextObject();
		}

		public bool IsAplicable(ContextElementTypes types)
		{
			bool flag = false;
			if (types == ContextElementTypes.None)
			{
				return false;
			}
			if ((types & ContextElementTypes.Any) == ContextElementTypes.Any)
			{
				return true;
			}
			if ((types & ContextElementTypes.ChartArea) == ContextElementTypes.ChartArea)
			{
				flag = (flag || this.ChartArea != null);
			}
			if ((types & ContextElementTypes.Axis) == ContextElementTypes.Axis)
			{
				flag = (flag || this.Axis != null);
			}
			if ((types & ContextElementTypes.AxisLabel) == ContextElementTypes.AxisLabel)
			{
				flag = (flag || this.AxisLabel != null);
			}
			if ((types & ContextElementTypes.Legend) == ContextElementTypes.Legend)
			{
				flag = (flag || this.Legend != null);
			}
			if ((types & ContextElementTypes.Series) == ContextElementTypes.Series)
			{
				flag = (flag || this.Series != null);
			}
			if ((types & ContextElementTypes.Annotation) == ContextElementTypes.Annotation)
			{
				flag = (flag || this.Annotation != null);
			}
			if ((types & ContextElementTypes.Title) == ContextElementTypes.Title)
			{
				flag = (flag || this.Title != null);
			}
			return flag;
		}

		public ChartElementType GetChartElementType()
		{
			ChartElementType result = ChartElementType.Nothing;
			switch (this.ElementType)
			{
			case ContextElementTypes.ChartArea:
				result = ChartElementType.PlottingArea;
				break;
			case ContextElementTypes.Series:
				result = ChartElementType.DataPoint;
				break;
			case ContextElementTypes.Axis:
				result = ChartElementType.Axis;
				break;
			case ContextElementTypes.AxisLabel:
				result = ChartElementType.Axis;
				break;
			case ContextElementTypes.Title:
				result = ChartElementType.Title;
				break;
			case ContextElementTypes.Annotation:
				result = ChartElementType.Annotation;
				break;
			case ContextElementTypes.Legend:
				result = ChartElementType.LegendArea;
				break;
			}
			return result;
		}
	}
}
