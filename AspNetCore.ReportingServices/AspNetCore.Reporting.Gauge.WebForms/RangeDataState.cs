namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class RangeDataState
	{
		public bool IsInRange;

		public bool IsTimerExceed;

		public DataAttributes data;

		public bool IsRangeActive
		{
			get
			{
				if (this.IsInRange)
				{
					return this.IsTimerExceed;
				}
				return false;
			}
		}

		public RangeDataState(Range range, DataAttributes data)
		{
			this.data = data;
		}
	}
}
