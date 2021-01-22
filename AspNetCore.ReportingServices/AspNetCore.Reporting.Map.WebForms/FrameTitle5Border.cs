using System.Drawing;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class FrameTitle5Border : FrameThin5Border
	{
		public override string Name
		{
			get
			{
				return "FrameTitle5";
			}
		}

		public FrameTitle5Border()
		{
			base.sizeLeftTop = new SizeF(base.sizeLeftTop.Width, (float)(base.defaultRadiusSize * 2.0));
			base.drawScrews = true;
		}

		public override RectangleF GetTitlePositionInBorder()
		{
			return new RectangleF((float)(base.defaultRadiusSize * 0.25), (float)(base.defaultRadiusSize * 0.25), (float)(base.defaultRadiusSize * 0.25), (float)(base.defaultRadiusSize * 1.6000000238418579));
		}
	}
}
