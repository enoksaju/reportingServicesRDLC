using System;
using System.Collections;

namespace AspNetCore.Reporting.Gauge.WebForms
{
	public class RealTimeValueCollection : CollectionBase
	{
		public RealTimeValue this[int index]
		{
			get
			{
				return (RealTimeValue)base.List[index];
			}
			set
			{
				base.List.Insert(index, value);
			}
		}

		public int Add(string inputValueName, double value)
		{
			return this.Add(new RealTimeValue(inputValueName, value, DateTime.Now));
		}

		public int Add(RealTimeValue value)
		{
			return base.List.Add(value);
		}

		public int Add(string inputValueName, double value, DateTime timestamp)
		{
			return this.Add(new RealTimeValue(inputValueName, value, timestamp));
		}

		public void Remove(RealTimeValue value)
		{
			base.List.Remove(value);
		}

		public bool Contains(RealTimeValue value)
		{
			return base.List.Contains(value);
		}

		public void Insert(int index, RealTimeValue value)
		{
			base.List.Insert(index, value);
		}

		public int IndexOf(RealTimeValue value)
		{
			return base.List.IndexOf(value);
		}
	}
}
