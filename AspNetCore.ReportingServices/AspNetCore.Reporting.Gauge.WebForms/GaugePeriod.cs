using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class GaugePeriod : ICloneable
	{
		private double duration;

		private PeriodType periodType;

		private TimeSpan timeSpan;

		private bool ivalidated;

		public double Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				if (value < 0.0)
				{
					throw new ArgumentException(Utils.SRGetStr("ExceptionPeriodNegative"));
				}
				if (value == 0.0)
				{
					value = double.NaN;
				}
				this.duration = value;
				this.ivalidated = true;
			}
		}

		public PeriodType PeriodType
		{
			get
			{
				return this.periodType;
			}
			set
			{
				this.periodType = value;
				this.ivalidated = true;
			}
		}

		public GaugePeriod()
		{
			this.duration = double.NaN;
			this.periodType = PeriodType.Seconds;
			this.ivalidated = true;
		}

		public GaugePeriod(double duration, PeriodType periodType)
		{
			this.duration = duration;
			this.periodType = periodType;
			this.ivalidated = true;
		}

		public TimeSpan ToTimeSpan()
		{
			if (this.ivalidated)
			{
				this.timeSpan = GaugePeriod.PeriodToTimeSpan(this.duration, this.periodType);
				this.ivalidated = false;
			}
			return this.timeSpan;
		}

		public static TimeSpan PeriodToTimeSpan(double timeTicks, PeriodType period)
		{
			if (double.IsNaN(timeTicks))
			{
				timeTicks = 0.0;
			}
			double num;
			switch (period)
			{
			case PeriodType.Days:
				num = timeTicks * 24.0 * 60.0 * 60.0 * 1000.0;
				break;
			case PeriodType.Hours:
				num = timeTicks * 60.0 * 60.0 * 1000.0;
				break;
			case PeriodType.Minutes:
				num = timeTicks * 60.0 * 1000.0;
				break;
			case PeriodType.Seconds:
				num = timeTicks * 1000.0;
				break;
			case PeriodType.Milliseconds:
				num = timeTicks;
				break;
			default:
				throw new ArgumentException(Utils.SRGetStr("ExceptionPeriodTimespanArgument"));
			}
			return new TimeSpan((long)Math.Floor(num * 10000.0));
		}

		public GaugePeriod Clone()
		{
			return (GaugePeriod)base.MemberwiseClone();
		}

		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
