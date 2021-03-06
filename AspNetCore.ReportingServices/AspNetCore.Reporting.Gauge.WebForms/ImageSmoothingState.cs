using System.Drawing.Drawing2D;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class ImageSmoothingState
	{
		public GaugeGraphics g;

		private SmoothingMode oldSmoothingMode;

		private CompositingQuality compositingQuality;

		private InterpolationMode oldInterpolationMode;

		public ImageSmoothingState(GaugeGraphics g)
		{
			this.g = g;
		}

		public void Set()
		{
			this.oldSmoothingMode = this.g.Graphics.SmoothingMode;
			this.compositingQuality = this.g.Graphics.CompositingQuality;
			this.oldInterpolationMode = this.g.Graphics.InterpolationMode;
			this.g.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			this.g.Graphics.CompositingQuality = CompositingQuality.HighQuality;
			this.g.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		}

		public void Restore()
		{
			this.g.Graphics.SmoothingMode = this.oldSmoothingMode;
			this.g.Graphics.CompositingQuality = this.compositingQuality;
			this.g.Graphics.InterpolationMode = this.oldInterpolationMode;
		}
	}
}
