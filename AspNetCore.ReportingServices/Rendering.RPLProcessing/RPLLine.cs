namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public sealed class RPLLine : RPLItem
	{
		public RPLLine()
		{
			base.m_rplElementProps = new RPLLineProps();
			base.m_rplElementProps.Definition = new RPLLinePropsDef();
		}

		public RPLLine(long startOffset, RPLContext context)
			: base(startOffset, context)
		{
		}
	}
}
