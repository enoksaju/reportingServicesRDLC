using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class TableGroupInstanceList : ArrayList
	{
		public new TableGroupInstance this[int index]
		{
			get
			{
				return (TableGroupInstance)base[index];
			}
		}

		public TableGroupInstanceList()
		{
		}

		public TableGroupInstanceList(int capacity)
			: base(capacity)
		{
		}
	}
}
