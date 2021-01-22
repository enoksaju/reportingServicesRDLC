using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class MultiChartInstanceList : ArrayList
	{
		public new MultiChartInstance this[int index]
		{
			get
			{
				return (MultiChartInstance)base[index];
			}
		}

		public MultiChartInstanceList()
		{
		}

		public MultiChartInstanceList(int capacity)
			: base(capacity)
		{
		}
	}
}
