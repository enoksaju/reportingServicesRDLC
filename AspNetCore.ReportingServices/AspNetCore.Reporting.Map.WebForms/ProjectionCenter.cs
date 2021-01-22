using System.ComponentModel;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class ProjectionCenter : MapObject
	{
		private bool xIsNaN = true;

		private double x = double.NaN;

		private bool yIsNaN = true;

		private double y = double.NaN;

		[SRDescription("DescriptionAttributeProjectionCenter_X")]
		[NotifyParentProperty(true)]
		[SRCategory("CategoryAttribute_Coordinates")]
		[TypeConverter(typeof(DoubleAutoValueConverter))]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(double.NaN)]
		public double X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
				this.xIsNaN = double.IsNaN(value);
				this.Invalidate();
			}
		}

		[SRCategory("CategoryAttribute_Coordinates")]
		[TypeConverter(typeof(DoubleAutoValueConverter))]
		[DefaultValue(double.NaN)]
		[SRDescription("DescriptionAttributeProjectionCenter_Y")]
		[NotifyParentProperty(true)]
		[RefreshProperties(RefreshProperties.All)]
		public double Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
				this.yIsNaN = double.IsNaN(value);
				this.Invalidate();
			}
		}

		public ProjectionCenter()
			: this(null)
		{
		}

		public ProjectionCenter(object parent)
			: base(parent)
		{
		}

		public override void Invalidate()
		{
			MapCore mapCore = (MapCore)this.Parent;
			if (mapCore != null)
			{
				mapCore.InvalidateCachedPaths();
				mapCore.ResetCachedBoundsAfterProjection();
				mapCore.InvalidateDistanceScalePanel();
				mapCore.InvalidateViewport();
			}
		}

		public bool IsXNaN()
		{
			return this.xIsNaN;
		}

		public bool IsYNaN()
		{
			return this.yIsNaN;
		}
	}
}
