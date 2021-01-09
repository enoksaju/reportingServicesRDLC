using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	internal sealed class DataCellsList : ArrayList
	{
		internal new DataCellList this[int index]
		{
			get
			{
				return (DataCellList)base[index];
			}
		}

		internal DataCellsList()
		{
		}

		internal DataCellsList(int capacity)
			: base(capacity)
		{
		}
	}
}
