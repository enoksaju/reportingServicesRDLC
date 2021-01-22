namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public class RPLItemMeasurement : RPLMeasurement
	{
		private IRPLItemFactory m_rplElement;

		public RPLItem Element
		{
			get
			{
				if (this.m_rplElement is OffsetItemInfo)
				{
					this.m_rplElement = this.m_rplElement.GetRPLItem();
				}
				return (RPLItem)this.m_rplElement;
			}
			set
			{
				this.m_rplElement = value;
			}
		}

		public override OffsetInfo OffsetInfo
		{
			get
			{
				return this.m_rplElement as OffsetItemInfo;
			}
		}

		public RPLItemMeasurement()
		{
		}

		public RPLItemMeasurement(RPLMeasurement measurement)
		{
			base.m_top = measurement.Top;
			base.m_left = measurement.Left;
			base.m_height = measurement.Height;
			base.m_width = measurement.Width;
			base.m_state = measurement.State;
			base.m_zindex = measurement.ZIndex;
		}

		public RPLItemMeasurement(RPLItem rplElement)
		{
			this.m_rplElement = rplElement;
		}

		public override void SetOffset(long offset, RPLContext context)
		{
			if (offset >= 0)
			{
				this.m_rplElement = new OffsetItemInfo(offset, context);
			}
		}
	}
}
