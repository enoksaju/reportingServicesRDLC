namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public sealed class RPLRectangle : RPLContainer
	{
		public RPLRectangle()
		{
			base.m_rplElementProps = new RPLItemProps();
			base.m_rplElementProps.Definition = new RPLRectanglePropsDef();
		}

		public RPLRectangle(long startOffset, RPLContext context, RPLItemMeasurement[] children)
			: base(startOffset, context, children)
		{
		}

		public RPLRectangle(RPLItemProps rplElementProps)
			: base(rplElementProps)
		{
		}
	}
}
