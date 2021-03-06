namespace AspNetCore.ReportingServices.Rendering.SPBProcessing
{
	public class PageItemRepeatWithHelper : PageItemHelper
	{
		private double m_relativeTop;

		private double m_relativeBottom;

		private double m_relativeTopToBottom;

		private int m_dataRegionIndex;

		private ItemSizes m_renderItemSize;

		public double RelativeTop
		{
			get
			{
				return this.m_relativeTop;
			}
			set
			{
				this.m_relativeTop = value;
			}
		}

		public double RelativeBottom
		{
			get
			{
				return this.m_relativeBottom;
			}
			set
			{
				this.m_relativeBottom = value;
			}
		}

		public double RelativeTopToBottom
		{
			get
			{
				return this.m_relativeTopToBottom;
			}
			set
			{
				this.m_relativeTopToBottom = value;
			}
		}

		public int DataRegionIndex
		{
			get
			{
				return this.m_dataRegionIndex;
			}
			set
			{
				this.m_dataRegionIndex = value;
			}
		}

		public ItemSizes RenderItemSize
		{
			get
			{
				return this.m_renderItemSize;
			}
			set
			{
				this.m_renderItemSize = value;
			}
		}

		public PageItemRepeatWithHelper(byte type)
			: base(type)
		{
		}
	}
}
