using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ChartDataPointInstanceList : ArrayList
	{
		public new ChartDataPointInstance this[int index]
		{
			get
			{
				return (ChartDataPointInstance)base[index];
			}
		}

		public ChartDataPointInstanceList()
		{
		}

		public ChartDataPointInstanceList(int capacity)
			: base(capacity)
		{
		}
	}
}
