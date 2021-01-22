namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public sealed class RPLHeaderFooter : RPLContainer
	{
		public RPLHeaderFooter()
		{
			base.m_rplElementProps = new RPLItemProps();
			base.m_rplElementProps.Definition = new RPLHeaderFooterPropsDef();
		}

		public RPLHeaderFooter(long startOffset, RPLContext context, RPLItemMeasurement[] children)
			: base(startOffset, context, children)
		{
		}
	}
}
