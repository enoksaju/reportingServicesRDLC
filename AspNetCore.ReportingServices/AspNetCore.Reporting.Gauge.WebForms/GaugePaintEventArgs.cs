using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class GaugePaintEventArgs : EventArgs
	{
		private GaugeContainer gauge;

		private GaugeGraphics graphics;

		public GaugeContainer Gauge
		{
			get
			{
				return this.gauge;
			}
		}

		public GaugeGraphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		public GaugePaintEventArgs(GaugeContainer gauge, GaugeGraphics graphics)
		{
			this.gauge = gauge;
			this.graphics = graphics;
		}
	}
}
