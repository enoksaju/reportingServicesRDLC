using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class TimerData : ICloneable
	{
		public TimeSpan ticks;

		public DateTime timestamp;

		public TimerData()
			: this(TimeSpan.Zero, DateTime.Now)
		{
		}

		public TimerData(TimeSpan ticks, DateTime timestamp)
		{
			this.timestamp = timestamp;
			this.ticks = ticks;
		}

		public object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
