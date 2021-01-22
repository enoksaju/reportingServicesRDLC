namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public sealed class RPLChart : RPLItem
	{
		public RPLChart()
		{
			base.m_rplElementProps = new RPLChartProps();
			base.m_rplElementProps.Definition = new RPLItemPropsDef();
		}

		public RPLChart(long startOffset, RPLContext context)
			: base(startOffset, context)
		{
		}
	}
}
