using System;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class DataSampleRC : HistoryEntry
	{
		public bool Invalid = true;

		public override DateTime Timestamp
		{
			get
			{
				return base.Timestamp;
			}
			set
			{
				base.Timestamp = value;
				this.Invalid = false;
			}
		}

		public override double Value
		{
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;
				this.Invalid = false;
			}
		}

		public void Assign(DataSampleRC data)
		{
			this.Timestamp = data.Timestamp;
			this.Value = data.Value;
		}
	}
}
