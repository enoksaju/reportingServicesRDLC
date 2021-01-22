using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportItemIndexerList : ArrayList
	{
		public new ReportItemIndexer this[int index]
		{
			get
			{
				return (ReportItemIndexer)base[index];
			}
		}

		public ReportItemIndexerList()
		{
		}

		public ReportItemIndexerList(int capacity)
			: base(capacity)
		{
		}
	}
}
