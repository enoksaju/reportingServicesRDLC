using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	internal sealed class FilterList : ArrayList
	{
		internal new Filter this[int index]
		{
			get
			{
				return (Filter)base[index];
			}
		}

		internal FilterList()
		{
		}

		internal FilterList(int capacity)
			: base(capacity)
		{
		}
	}
}
