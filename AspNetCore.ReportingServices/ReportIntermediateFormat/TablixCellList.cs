using System;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	public sealed class TablixCellList : CellList
	{
		public new TablixCell this[int index]
		{
			get
			{
				return (TablixCell)base[index];
			}
		}

		public TablixCellList()
		{
		}

		public TablixCellList(int capacity)
			: base(capacity)
		{
		}
	}
}
