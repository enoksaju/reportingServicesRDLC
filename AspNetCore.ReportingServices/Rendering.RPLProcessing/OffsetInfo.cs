namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public class OffsetInfo : IRPLObjectFactory
	{
		protected long m_endOffset = -1L;

		public RPLContext m_context;

		public long EndOffset
		{
			get
			{
				return this.m_endOffset;
			}
		}

		public OffsetInfo(long endOffset, RPLContext context)
		{
			this.m_endOffset = endOffset;
			this.m_context = context;
		}
	}
}
