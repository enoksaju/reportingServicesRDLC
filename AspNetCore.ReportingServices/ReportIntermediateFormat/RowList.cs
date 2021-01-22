using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	public class RowList : ArrayList
	{
		public new Row this[int index]
		{
			get
			{
				return (Row)base[index];
			}
		}

		public RowList()
		{
		}

		public RowList(int capacity)
			: base(capacity)
		{
		}
	}
}
