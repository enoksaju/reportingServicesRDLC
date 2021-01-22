using System.Drawing.Drawing2D;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class GaugeGraphState
	{
		public GraphicsState state;

		public float width;

		public float height;

		public GaugeGraphState(GraphicsState state, float width, float height)
		{
			this.state = state;
			this.width = width;
			this.height = height;
		}
	}
}
