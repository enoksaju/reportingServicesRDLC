using System.Drawing;

namespace AspNetCore.Reporting.Chart.WebForms
{
	public class CircularChartAreaAxis
	{
		public float AxisPosition;

		public float AxisSectorSize;

		public string Title = string.Empty;

		public Color TitleColor = Color.Empty;

		public CircularChartAreaAxis()
		{
		}

		public CircularChartAreaAxis(float axisPosition, float axisSectorSize)
		{
			this.AxisPosition = axisPosition;
			this.AxisSectorSize = axisSectorSize;
		}
	}
}
