using System.Drawing;
using System.Drawing.Drawing2D;

namespace AspNetCore.Reporting.Chart.WebForms
{
	public class HotRegion
	{
		private GraphicsPath path;

		private bool relativeCoordinates = true;

		private RectangleF boundingRectangle = RectangleF.Empty;

		private object selectedObject;

		private int pointIndex = -1;

		private string seriesName = "";

		private ChartElementType type;

		private object selectedSubObject;

		public GraphicsPath Path
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = value;
			}
		}

		public bool RelativeCoordinates
		{
			get
			{
				return this.relativeCoordinates;
			}
			set
			{
				this.relativeCoordinates = value;
			}
		}

		public RectangleF BoundingRectangle
		{
			get
			{
				return this.boundingRectangle;
			}
			set
			{
				this.boundingRectangle = value;
			}
		}

		public object SelectedObject
		{
			get
			{
				return this.selectedObject;
			}
			set
			{
				this.selectedObject = value;
			}
		}

		public object SelectedSubObject
		{
			get
			{
				return this.selectedSubObject;
			}
			set
			{
				this.selectedSubObject = value;
			}
		}

		public int PointIndex
		{
			get
			{
				return this.pointIndex;
			}
			set
			{
				this.pointIndex = value;
			}
		}

		public string SeriesName
		{
			get
			{
				return this.seriesName;
			}
			set
			{
				this.seriesName = value;
			}
		}

		public ChartElementType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}
	}
}
