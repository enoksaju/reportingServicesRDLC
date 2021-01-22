using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ChartDataPointList : ArrayList
	{
		public new ChartDataPoint this[int index]
		{
			get
			{
				return (ChartDataPoint)base[index];
			}
		}

		public ChartDataPointList()
		{
		}

		public ChartDataPointList(int capacity)
			: base(capacity)
		{
		}
	}
}
