namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public sealed class RPLBody : RPLContainer
	{
		public RPLBody()
		{
			base.m_rplElementProps = new RPLItemProps();
			base.m_rplElementProps.Definition = new RPLItemPropsDef();
		}

		public RPLBody(RPLItemProps props)
			: base(props)
		{
		}

		public RPLBody(long startOffset, RPLContext context, RPLItemMeasurement[] children)
			: base(startOffset, context, children)
		{
		}
	}
}
