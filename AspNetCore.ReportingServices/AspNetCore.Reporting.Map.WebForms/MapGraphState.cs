using System.Drawing.Drawing2D;

namespace AspNetCore.Reporting.Map.WebForms
{
	public class MapGraphState
	{
		public GraphicsState state;

		public int width;

		public int height;

		public MapGraphState(GraphicsState state, int width, int height)
		{
			this.state = state;
			this.width = width;
			this.height = height;
		}
	}
}
