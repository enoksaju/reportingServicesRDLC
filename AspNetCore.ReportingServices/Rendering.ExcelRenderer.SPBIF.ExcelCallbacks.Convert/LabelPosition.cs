namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.SPBIF.ExcelCallbacks.Convert
{
	public sealed class LabelPosition
	{
		private string m_label;

		private long m_position;

		private long m_startPosition;

		public string Label
		{
			get
			{
				return this.m_label;
			}
		}

		public long Position
		{
			get
			{
				return this.m_position;
			}
		}

		public long StartPosition
		{
			get
			{
				return this.m_startPosition;
			}
		}

		public LabelPosition(string label, long position)
		{
			this.m_label = label;
			this.m_position = position;
		}

		public LabelPosition(string label, long position, long startPosition)
		{
			this.m_label = label;
			this.m_position = position;
			this.m_startPosition = startPosition;
		}
	}
}
