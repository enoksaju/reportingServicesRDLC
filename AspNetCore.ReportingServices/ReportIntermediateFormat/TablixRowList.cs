using System;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	public sealed class TablixRowList : RowList
	{
		public new TablixRow this[int index]
		{
			get
			{
				return (TablixRow)base[index];
			}
		}

		public TablixRowList()
		{
		}

		public TablixRowList(int capacity)
			: base(capacity)
		{
		}
	}
}
