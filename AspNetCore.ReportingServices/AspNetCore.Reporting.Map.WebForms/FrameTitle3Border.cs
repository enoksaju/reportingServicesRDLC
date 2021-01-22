using System.Drawing;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class FrameTitle3Border : FrameThin3Border
	{
		public override string Name
		{
			get
			{
				return "FrameTitle3";
			}
		}

		public FrameTitle3Border()
		{
			base.sizeLeftTop = new SizeF(base.sizeLeftTop.Width, (float)(base.defaultRadiusSize * 2.0));
		}

		public override RectangleF GetTitlePositionInBorder()
		{
			return new RectangleF((float)(base.defaultRadiusSize * 0.25), (float)(base.defaultRadiusSize * 0.25), (float)(base.defaultRadiusSize * 0.25), (float)(base.defaultRadiusSize * 1.6000000238418579));
		}
	}
}
