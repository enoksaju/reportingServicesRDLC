using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class DataFieldList : ArrayList
	{
		public new Field this[int index]
		{
			get
			{
				return (Field)base[index];
			}
		}

		public DataFieldList()
		{
		}

		public DataFieldList(int capacity)
			: base(capacity)
		{
		}
	}
}
