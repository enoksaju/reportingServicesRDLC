using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class CustomReportItemCellInstanceList : ArrayList
	{
		public new CustomReportItemCellInstance this[int index]
		{
			get
			{
				return (CustomReportItemCellInstance)base[index];
			}
		}

		public CustomReportItemCellInstanceList()
		{
		}

		public CustomReportItemCellInstanceList(int capacity)
			: base(capacity)
		{
		}
	}
}
