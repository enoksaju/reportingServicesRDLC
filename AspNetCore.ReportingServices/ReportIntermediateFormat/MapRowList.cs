namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public sealed class MapRowList : RowList
	{
		public new MapRow this[int index]
		{
			get
			{
				return (MapRow)base[index];
			}
		}

		public MapRowList()
		{
		}

		public MapRowList(int capacity)
			: base(capacity)
		{
		}
	}
}
