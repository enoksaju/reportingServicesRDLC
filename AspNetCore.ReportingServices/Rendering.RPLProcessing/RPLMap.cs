namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public sealed class RPLMap : RPLItem
	{
		public RPLMap()
		{
			base.m_rplElementProps = new RPLMapProps();
			base.m_rplElementProps.Definition = new RPLItemPropsDef();
		}

		public RPLMap(long startOffset, RPLContext context)
			: base(startOffset, context)
		{
		}
	}
}
