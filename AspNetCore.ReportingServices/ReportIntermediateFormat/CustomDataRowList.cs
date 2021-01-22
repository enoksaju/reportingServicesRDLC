using System;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	[Serializable]
	public sealed class CustomDataRowList : RowList
	{
		public new CustomDataRow this[int index]
		{
			get
			{
				return (CustomDataRow)base[index];
			}
		}

		public CustomDataRowList()
		{
		}

		public CustomDataRowList(int capacity)
			: base(capacity)
		{
		}
	}
}
