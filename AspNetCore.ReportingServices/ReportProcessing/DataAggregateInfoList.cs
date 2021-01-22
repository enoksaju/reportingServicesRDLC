using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class DataAggregateInfoList : ArrayList
	{
		public new DataAggregateInfo this[int index]
		{
			get
			{
				return (DataAggregateInfo)base[index];
			}
		}

		public DataAggregateInfoList()
		{
		}

		public DataAggregateInfoList(int capacity)
			: base(capacity)
		{
		}
	}
}
