using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class DataValueInstanceList : ArrayList
	{
		public new DataValueInstance this[int index]
		{
			get
			{
				return (DataValueInstance)base[index];
			}
		}

		public DataValueInstanceList()
		{
		}

		public DataValueInstanceList(int capacity)
			: base(capacity)
		{
		}
	}
}
