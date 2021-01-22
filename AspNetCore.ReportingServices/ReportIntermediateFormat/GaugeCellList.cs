namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public sealed class GaugeCellList : CellList
	{
		public new GaugeCell this[int index]
		{
			get
			{
				return (GaugeCell)base[index];
			}
		}
	}
}
