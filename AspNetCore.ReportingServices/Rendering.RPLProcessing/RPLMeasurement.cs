namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public class RPLMeasurement : RPLSizes
	{
		protected int m_zindex;

		protected byte m_state;

		protected IRPLObjectFactory m_offsetInfo;

		public int ZIndex
		{
			get
			{
				return this.m_zindex;
			}
			set
			{
				this.m_zindex = value;
			}
		}

		public byte State
		{
			get
			{
				return this.m_state;
			}
			set
			{
				this.m_state = value;
			}
		}

		public virtual OffsetInfo OffsetInfo
		{
			get
			{
				return this.m_offsetInfo as OffsetInfo;
			}
		}

		public RPLMeasurement()
		{
		}

		public RPLMeasurement(RPLMeasurement measures)
			: base(measures.Top, measures.Left, measures.Height, measures.Width)
		{
			this.m_state = measures.State;
			this.m_zindex = measures.ZIndex;
		}

		public virtual void SetOffset(long offset, RPLContext context)
		{
			if (offset >= 0)
			{
				this.m_offsetInfo = new OffsetInfo(offset, context);
			}
		}
	}
}
