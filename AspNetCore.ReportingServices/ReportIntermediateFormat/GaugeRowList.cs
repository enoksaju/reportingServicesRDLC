namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public sealed class GaugeRowList : RowList
	{
		public new GaugeRow this[int index]
		{
			get
			{
				return (GaugeRow)base[index];
			}
		}

		public GaugeRowList()
		{
		}

		public GaugeRowList(int capacity)
			: base(capacity)
		{
		}
	}
}
