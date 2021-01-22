using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ChartColumnList : ArrayList
	{
		public new ChartColumn this[int index]
		{
			get
			{
				return (ChartColumn)base[index];
			}
		}

		public ChartColumnList()
		{
		}

		public ChartColumnList(int capacity)
			: base(capacity)
		{
		}
	}
}
