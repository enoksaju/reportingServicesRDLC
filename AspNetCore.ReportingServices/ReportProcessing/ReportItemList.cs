using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	internal sealed class ReportItemList : ArrayList
	{
		internal new ReportItem this[int index]
		{
			get
			{
				return (ReportItem)base[index];
			}
		}

		internal ReportItemList()
		{
		}

		internal ReportItemList(int capacity)
			: base(capacity)
		{
		}
	}
}
