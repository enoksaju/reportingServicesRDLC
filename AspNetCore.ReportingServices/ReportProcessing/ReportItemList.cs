using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportItemList : ArrayList
	{
		public new ReportItem this[int index]
		{
			get
			{
				return (ReportItem)base[index];
			}
		}

		public ReportItemList()
		{
		}

		public ReportItemList(int capacity)
			: base(capacity)
		{
		}
	}
}
