using System;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	public sealed class DataCellList : CellList
	{
		public new DataCell this[int index]
		{
			get
			{
				return (DataCell)base[index];
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
