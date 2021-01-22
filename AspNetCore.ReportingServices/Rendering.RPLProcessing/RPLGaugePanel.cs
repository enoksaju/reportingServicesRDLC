namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public sealed class RPLGaugePanel : RPLItem
	{
		public RPLGaugePanel()
		{
			base.m_rplElementProps = new RPLGaugePanelProps();
			base.m_rplElementProps.Definition = new RPLItemPropsDef();
		}

		public RPLGaugePanel(long startOffset, RPLContext context)
			: base(startOffset, context)
		{
		}
	}
}
