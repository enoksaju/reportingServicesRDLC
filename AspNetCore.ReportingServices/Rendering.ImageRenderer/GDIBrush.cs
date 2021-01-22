using System.Collections.Generic;
using System.Drawing;

namespace AspNetCore.ReportingServices.Rendering.ImageRenderer
{
	public sealed class GDIBrush
	{
		private GDIBrush()
		{
		}

		public static Brush GetBrush(Dictionary<string, Brush> brushes, Color color)
		{
			string key = color.ToString();
			Brush brush = default(Brush);
			if (brushes.TryGetValue(key, out brush))
			{
				return brush;
			}
			brush = new SolidBrush(color);
			brushes.Add(key, brush);
			return brush;
		}
	}
}
