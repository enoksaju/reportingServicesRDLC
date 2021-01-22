using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportItemInstanceList : ArrayList
	{
		public new ReportItemInstance this[int index]
		{
			get
			{
				return (ReportItemInstance)base[index];
			}
		}

		public ReportItemInstanceList()
		{
		}

		public ReportItemInstanceList(int capacity)
			: base(capacity)
		{
		}
	}
}
