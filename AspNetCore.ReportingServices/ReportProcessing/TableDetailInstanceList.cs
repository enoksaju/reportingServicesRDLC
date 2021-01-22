using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class TableDetailInstanceList : ArrayList
	{
		public new TableDetailInstance this[int index]
		{
			get
			{
				return (TableDetailInstance)base[index];
			}
		}

		public TableDetailInstanceList()
		{
		}

		public TableDetailInstanceList(int capacity)
			: base(capacity)
		{
		}
	}
}
