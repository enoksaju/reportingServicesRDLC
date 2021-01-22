using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class HotRegionList : MapObject
	{
		private ArrayList list = new ArrayList();

		private Dictionary<object, int> selectedObjectIndex = new Dictionary<object, int>();

		private float scaleFactorX = 1f;

		private float scaleFactorY = 1f;

		public ArrayList List
		{
			get
			{
				return this.list;
			}
		}

		public float ScaleFactorX
		{
			get
			{
				return this.scaleFactorX;
			}
			set
			{
				this.scaleFactorX = value;
			}
		}

		public float ScaleFactorY
		{
			get
			{
				return this.scaleFactorY;
			}
			set
			{
				this.scaleFactorY = value;
			}
		}

		public HotRegionList(object parent)
			: base(parent)
		{
		}

		public int FindHotRegionOfObject(object obj)
		{
			int result = default(int);
			if (this.selectedObjectIndex.TryGetValue(obj, out result))
			{
				return result;
			}
			return -1;
		}

		public void RemoveHotRegionOfObject(object obj)
		{
			int num = 0;
			HotRegion hotRegion;
			while (true)
			{
				if (num < this.list.Count)
				{
					hotRegion = (HotRegion)this.list[num];
					if (hotRegion.SelectedObject != obj)
					{
						num++;
						continue;
					}
					break;
				}
				return;
			}
			this.list.RemoveAt(num);
			this.selectedObjectIndex.Remove(hotRegion.SelectedObject);
			hotRegion.Dispose();
		}

		public void SetHotRegion(MapGraphics g, object selectedObject, params GraphicsPath[] pathArray)
		{
			this.SetHotRegion(g, selectedObject, PointF.Empty, pathArray);
		}

		public void SetHotRegion(MapGraphics g, object selectedObject, PointF pinPoint, params GraphicsPath[] pathArray)
		{
			GraphicsPath[] array = new GraphicsPath[pathArray.Length];
			for (int i = 0; i < pathArray.Length; i++)
			{
				if (pathArray[i] != null)
				{
					array[i] = (GraphicsPath)pathArray[i].Clone();
				}
			}
			HotRegion hotRegion;
			if (!this.selectedObjectIndex.ContainsKey(selectedObject))
			{
				hotRegion = new HotRegion();
				int value = this.list.Add(hotRegion);
				this.selectedObjectIndex[selectedObject] = value;
			}
			else
			{
				int index = this.selectedObjectIndex[selectedObject];
				hotRegion = (HotRegion)this.list[index];
			}
			hotRegion.SelectedObject = selectedObject;
			Matrix transform = g.Transform;
			if (transform != null)
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] != null)
					{
						try
						{
							array[j].Transform(transform);
						}
						catch
						{
							return;
						}
					}
				}
			}
			hotRegion.Paths = array;
			if (!pinPoint.IsEmpty)
			{
				pinPoint.X += transform.OffsetX;
				pinPoint.Y += transform.OffsetY;
			}
			hotRegion.PinPoint = pinPoint;
			hotRegion.BuildMatrices(g);
		}

		public HotRegion[] CheckHotRegions(int x, int y, Type[] objectTypes, bool needTooltipOnly)
		{
			ArrayList arrayList = new ArrayList();
			for (int num = this.list.Count - 1; num >= 0; num--)
			{
				HotRegion hotRegion = (HotRegion)this.list[num];
				Shape shape;
				Path path;
				Symbol symbol;
				GridAttributes gridAttributes;
				if (this.IsOfType(objectTypes, hotRegion.SelectedObject) && (!needTooltipOnly || (hotRegion.SelectedObject is IToolTipProvider && !(((IToolTipProvider)hotRegion.SelectedObject).GetToolTip() == string.Empty))))
				{
					shape = (hotRegion.SelectedObject as Shape);
					path = (hotRegion.SelectedObject as Path);
					symbol = (hotRegion.SelectedObject as Symbol);
					gridAttributes = (hotRegion.SelectedObject as GridAttributes);
					if (shape == null && path == null && symbol == null && gridAttributes == null)
					{
						goto IL_0146;
					}
					RectangleF rectangleF = new RectangleF(this.Common.MapCore.Viewport.GetAbsoluteLocation(), this.Common.MapCore.Viewport.GetAbsoluteSize());
					rectangleF.X *= this.ScaleFactorX;
					rectangleF.Y *= this.ScaleFactorY;
					rectangleF.Width *= this.ScaleFactorX;
					rectangleF.Height *= this.ScaleFactorY;
					if (rectangleF.Contains((float)x, (float)y))
					{
						goto IL_0146;
					}
				}
				continue;
				IL_0146:
				GraphicsPath[] paths = ((HotRegion)this.list[num]).Paths;
				foreach (GraphicsPath graphicsPath in paths)
				{
					if (graphicsPath != null)
					{
						GraphicsPath graphicsPath2 = graphicsPath;
						float x2 = (float)x;
						float y2 = (float)y;
						bool flag = false;
						if (shape != null || path != null || gridAttributes != null)
						{
							RectangleF bounds = graphicsPath.GetBounds();
							float num2 = Math.Max(bounds.Width, bounds.Height);
							if (num2 > 1000.0)
							{
								float num3 = (float)(num2 / 1000.0);
								PointF[] pathPoints = graphicsPath.PathPoints;
								for (int j = 0; j < pathPoints.Length; j++)
								{
									pathPoints[j].X /= num3;
									pathPoints[j].Y /= num3;
								}
								graphicsPath2 = new GraphicsPath(pathPoints, graphicsPath.PathTypes, graphicsPath.FillMode);
								flag = true;
								x2 = (float)x / num3;
								y2 = (float)y / num3;
							}
						}
						if (path != null)
						{
							using (Pen pen = path.GetBorderPen())
							{
								if (pen != null)
								{
									if (pen.Width < 7.0)
									{
										pen.Width = 7f;
									}
									if (graphicsPath2.IsOutlineVisible(x2, y2, pen))
									{
										if (flag)
										{
											graphicsPath2.Dispose();
										}
										arrayList.Add(hotRegion);
										goto IL_03ae;
									}
								}
							}
						}
						else if (gridAttributes != null)
						{
							using (Pen pen2 = gridAttributes.GetPen())
							{
								if (pen2 != null)
								{
									if (pen2.Width < 5.0)
									{
										pen2.Width = 5f;
									}
									if (graphicsPath2.IsOutlineVisible(x2, y2, pen2))
									{
										if (flag)
										{
											graphicsPath2.Dispose();
										}
										arrayList.Add(hotRegion);
										goto IL_03ae;
									}
								}
							}
						}
						else if (symbol != null)
						{
							RectangleF bounds2 = graphicsPath2.GetBounds();
							if (bounds2.Width < 3.0)
							{
								bounds2.Inflate((float)(3.0 - bounds2.Width), 0f);
							}
							if (bounds2.Height < 3.0)
							{
								bounds2.Inflate(0f, (float)(3.0 - bounds2.Height));
							}
							if (bounds2.Contains(x2, y2))
							{
								if (flag)
								{
									graphicsPath2.Dispose();
								}
								arrayList.Add(hotRegion);
								break;
							}
						}
						if (gridAttributes == null && graphicsPath2.IsVisible(x2, y2))
						{
							if (flag)
							{
								graphicsPath2.Dispose();
							}
							arrayList.Add(hotRegion);
							break;
						}
					}
				}
				IL_03ae:;
			}
			if (arrayList.Count > 0)
			{
				return (HotRegion[])arrayList.ToArray(typeof(HotRegion));
			}
			return null;
		}

		public bool IsOfType(Type[] objectTypes, object obj)
		{
			if (objectTypes.Length == 0)
			{
				return true;
			}
			foreach (Type type in objectTypes)
			{
				if (type.IsInstanceOfType(obj))
				{
					return true;
				}
			}
			return false;
		}

		public void ClearContentElements()
		{
			for (int num = this.list.Count - 1; num >= 0; num--)
			{
				HotRegion hotRegion = this.list[num] as HotRegion;
				if (hotRegion != null && (hotRegion.SelectedObject is IContentElement || hotRegion.SelectedObject is GridAttributes))
				{
					this.list.RemoveAt(num);
					this.selectedObjectIndex.Remove(hotRegion.SelectedObject);
					hotRegion.Dispose();
				}
			}
		}

		public void Clear()
		{
			foreach (HotRegion item in this.list)
			{
				item.Dispose();
			}
			this.list.Clear();
			this.selectedObjectIndex.Clear();
		}
	}
}
