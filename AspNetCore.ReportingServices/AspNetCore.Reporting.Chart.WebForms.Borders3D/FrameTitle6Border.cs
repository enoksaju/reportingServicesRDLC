using System.Drawing;

namespace AspNetCore.Reporting.Chart.WebForms.Borders3D
{
	public class FrameTitle6Border : FrameThin6Border
	{
		public override string Name
		{
			get
			{
				return "FrameTitle6";
			}
		}

		public override float Resolution
		{
			set
			{
				base.Resolution = value;
				base.sizeLeftTop = new SizeF(base.sizeLeftTop.Width, (float)(base.defaultRadiusSize * 2.0));
			}
		}

		public FrameTitle6Border()
		{
			base.sizeLeftTop = new SizeF(base.sizeLeftTop.Width, (float)(base.defaultRadiusSize * 2.0));
		}

		public override RectangleF GetTitlePositionInBorder()
		{
			return new RectangleF((float)(base.defaultRadiusSize * 0.25), (float)(base.defaultRadiusSize * 0.25), (float)(base.defaultRadiusSize * 0.25), (float)(base.defaultRadiusSize * 1.6000000238418579));
		}
	}
}
