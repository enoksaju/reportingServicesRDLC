namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public sealed class RPLTextRun : RPLElement
	{
		private RPLSizes m_contentSizes;

		public RPLSizes ContentSizes
		{
			get
			{
				return this.m_contentSizes;
			}
			set
			{
				this.m_contentSizes = value;
			}
		}

		public RPLTextRun()
		{
			base.m_rplElementProps = new RPLTextRunProps();
			base.m_rplElementProps.Definition = new RPLTextRunPropsDef();
		}

		public RPLTextRun(RPLTextRunProps rplElementProps)
			: base(rplElementProps)
		{
		}
	}
}
