using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class DataCellsList : ArrayList
	{
		public new DataCellList this[int index]
		{
			get
			{
				return (DataCellList)base[index];
			}
		}

		public DataCellsList()
		{
		}

		public DataCellsList(int capacity)
			: base(capacity)
		{
		}
	}
}
