using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class GaugeDuration : ICloneable
	{
		private DurationType durationType;

		private double count;

		private TimeSpan timeSpan;

		private bool ivalidated;

		public double Count
		{
			get
			{
				return this.count;
			}
			set
			{
				if (value < 0.0)
				{
					throw new ArgumentException(Utils.SRGetStr("ExceptionDurationNegative"));
				}
				this.count = value;
				this.ivalidated = true;
			}
		}

		public DurationType DurationType
		{
			get
			{
				return this.durationType;
			}
			set
			{
				this.durationType = value;
				this.ivalidated = true;
			}
		}

		public bool IsTimeBased
		{
			get
			{
				if (this.durationType != DurationType.Count)
				{
					return this.durationType != DurationType.Infinite;
				}
				return false;
			}
		}

		public bool IsCountBased
		{
			get
			{
				return this.durationType == DurationType.Count;
			}
		}

		public bool IsInfinity
		{
			get
			{
				return this.durationType == DurationType.Infinite;
			}
		}

		public bool IsEmpty
		{
			get
			{
				if (this.IsInfinity)
				{
					return false;
				}
				if (!double.IsNaN(this.count))
				{
					return this.count == 0.0;
				}
				return true;
			}
		}

		public GaugeDuration()
		{
			this.count = 0.0;
			this.durationType = DurationType.Count;
			this.ivalidated = true;
		}

		public GaugeDuration(double count, DurationType durationType)
		{
			this.count = count;
			this.durationType = durationType;
			this.ivalidated = true;
		}

		public static PeriodType MapToPeriodType(DurationType type)
		{
			switch (type)
			{
			case DurationType.Days:
				return PeriodType.Days;
			case DurationType.Hours:
				return PeriodType.Hours;
			case DurationType.Minutes:
				return PeriodType.Minutes;
			case DurationType.Seconds:
				return PeriodType.Seconds;
			case DurationType.Milliseconds:
				return PeriodType.Milliseconds;
			default:
				throw new ArgumentException(Utils.SRGetStr("ExceptionMapPeriodTypeArgument"));
			}
		}

		public TimeSpan ToTimeSpan()
		{
			if (this.IsTimeBased && !this.IsEmpty)
			{
				if (this.IsInfinity)
				{
					return TimeSpan.MaxValue;
				}
				if (this.ivalidated)
				{
					this.timeSpan = GaugePeriod.PeriodToTimeSpan(this.count, GaugeDuration.MapToPeriodType(this.durationType));
					this.ivalidated = false;
				}
				return this.timeSpan;
			}
			return TimeSpan.Zero;
		}

		public void Extend(GaugeDuration extend, DateTime topDate, DateTime btmDate)
		{
			if (extend.IsInfinity)
			{
				this.DurationType = DurationType.Infinite;
			}
			else if (extend.IsCountBased && this.Count < extend.Count)
			{
				this.Count = extend.Count;
			}
			else if (extend.IsTimeBased)
			{
				DateTime t = topDate - extend.ToTimeSpan();
				if (btmDate > t)
				{
					this.Count += 1.0;
				}
			}
		}

		public object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
