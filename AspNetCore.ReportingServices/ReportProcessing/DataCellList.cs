using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class DataCellList : ArrayList
	{
		public new DataValueCRIList this[int index]
		{
			get
			{
				return (DataValueCRIList)base[index];
			}
		}

		public DataCellList()
		{
		}

		public DataCellList(int capacity)
			: base(capacity)
		{
		}
	}
}
