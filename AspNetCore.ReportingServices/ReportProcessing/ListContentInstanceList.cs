using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ListContentInstanceList : ArrayList
	{
		public new ListContentInstance this[int index]
		{
			get
			{
				return (ListContentInstance)base[index];
			}
		}

		public ListContentInstanceList()
		{
		}

		public ListContentInstanceList(int capacity)
			: base(capacity)
		{
		}
	}
}
