using System;
using System.Collections;

namespace AspNetCore.Reporting.Map.WebForms
{
	internal class RealTimeValueCollection : CollectionBase
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

		public int Add(string inputValueName, double value, DateTime timeStamp)
		{
			return this.Add(new RealTimeValue(inputValueName, value, timeStamp));
		}

		public void Remove(RealTimeValue value)
		{
			base.List.Remove(value);
		}
	}
}
