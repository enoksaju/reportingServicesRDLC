using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class DataSetList : ArrayList
	{
		public new DataSet this[int index]
		{
			get
			{
				return (DataSet)base[index];
			}
		}

		public DataSetList()
		{
		}

		public DataSetList(int capacity)
			: base(capacity)
		{
		}
	}
}
