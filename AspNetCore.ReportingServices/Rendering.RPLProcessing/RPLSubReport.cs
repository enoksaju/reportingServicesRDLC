namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public class RPLSubReport : RPLContainer
	{
		public RPLSubReport()
		{
			base.m_rplElementProps = new RPLSubReportProps();
			base.m_rplElementProps.Definition = new RPLSubReportPropsDef();
		}

		public RPLSubReport(long startOffset, RPLContext context, RPLItemMeasurement[] children)
			: base(startOffset, context, children)
		{
		}

		public RPLSubReport(RPLItemProps rplElementProps)
			: base(rplElementProps)
		{
		}
	}
}
