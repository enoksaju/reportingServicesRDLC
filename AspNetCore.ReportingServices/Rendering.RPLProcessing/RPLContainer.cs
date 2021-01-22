namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public class RPLContainer : RPLItem
	{
		private RPLItemMeasurement[] m_children;

		public RPLItemMeasurement[] Children
		{
			get
			{
				return this.m_children;
			}
			set
			{
				this.m_children = value;
			}
		}

		public RPLContainer()
		{
		}

		public RPLContainer(long startOffset, RPLContext context, RPLItemMeasurement[] children)
			: base(startOffset, context)
		{
			this.m_children = children;
		}

		public RPLContainer(RPLItemProps rplElementProps)
			: base(rplElementProps)
		{
		}

		public static RPLItem CreateItem(long offset, RPLContext context, RPLItemMeasurement[] children, byte type)
		{
			switch (type)
			{
			case 4:
			case 5:
				return new RPLHeaderFooter(offset, context, children);
			case 6:
				return new RPLBody(offset, context, children);
			case 12:
				return new RPLSubReport(offset, context, children);
			case 10:
				return new RPLRectangle(offset, context, children);
			default:
				return new RPLContainer(offset, context, children);
			}
		}
	}
}
