using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class FilterList : ArrayList
	{
		public new Filter this[int index]
		{
			get
			{
				return (Filter)base[index];
			}
		}

		public FilterList()
		{
		}

		public FilterList(int capacity)
			: base(capacity)
		{
		}
	}
}
