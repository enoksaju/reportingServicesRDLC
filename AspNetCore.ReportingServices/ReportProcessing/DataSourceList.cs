using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class DataSourceList : ArrayList
	{
		public new DataSource this[int index]
		{
			get
			{
				return (DataSource)base[index];
			}
		}

		public DataSourceList()
		{
		}

		public DataSourceList(int capacity)
			: base(capacity)
		{
		}
	}
}
