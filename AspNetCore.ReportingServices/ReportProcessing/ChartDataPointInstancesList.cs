using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ChartDataPointInstancesList : ArrayList
	{
		public new ChartDataPointInstanceList this[int index]
		{
			get
			{
				return (ChartDataPointInstanceList)base[index];
			}
		}

		public ChartDataPointInstancesList()
		{
		}

		public ChartDataPointInstancesList(int capacity)
			: base(capacity)
		{
		}
	}
}
