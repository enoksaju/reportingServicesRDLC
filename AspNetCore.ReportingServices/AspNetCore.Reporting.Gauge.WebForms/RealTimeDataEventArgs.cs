using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class RealTimeDataEventArgs : EventArgs
	{
		private RealTimeValueCollection realTimeValues;

		public RealTimeValueCollection RealTimeValues
		{
			get
			{
				return this.realTimeValues;
			}
		}

		public RealTimeDataEventArgs()
		{
			this.realTimeValues = new RealTimeValueCollection();
		}
	}
}
