using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class CustomReportItemCellInstancesList : ArrayList
	{
		public new CustomReportItemCellInstanceList this[int index]
		{
			get
			{
				return (CustomReportItemCellInstanceList)base[index];
			}
		}

		public CustomReportItemCellInstancesList()
		{
		}

		public CustomReportItemCellInstancesList(int capacity)
			: base(capacity)
		{
		}
	}
}
