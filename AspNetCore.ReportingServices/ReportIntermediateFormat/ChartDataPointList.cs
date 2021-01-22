using System;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	public sealed class ChartDataPointList : CellList
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
