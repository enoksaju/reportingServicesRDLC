using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class TableColumnList : ArrayList
	{
		public new TableColumn this[int index]
		{
			get
			{
				return (TableColumn)base[index];
			}
		}

		public TableColumnList()
		{
		}

		public TableColumnList(int capacity)
			: base(capacity)
		{
		}
	}
}
