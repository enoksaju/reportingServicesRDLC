namespace AspNetCore.ReportingServices.Rendering.HPBProcessing
{
	public sealed class ItemBoundaries
	{
		private long m_startOffset;

		private long m_endOffset;

		public long StartOffset
		{
			get
			{
				return this.m_startOffset;
			}
		}

		public long EndOffset
		{
			get
			{
				return this.m_endOffset;
			}
		}

		public ItemBoundaries(long start, long end)
		{
			this.m_startOffset = start;
			this.m_endOffset = end;
		}
	}
}
