namespace AspNetCore.ReportingServices.Rendering.SPBProcessing
{
	public class PageItemContainerHelper : PageItemHelper
	{
		private bool m_itemsCreated;

		private int[] m_indexesLeftToRight;

		private int[] m_indexesTopToBottom;

		private PageItemHelper[] m_repeatWithItems;

		private PageItemHelper m_rightEdgeItem;

		private PageItemHelper[] m_children;

		public bool ItemsCreated
		{
			get
			{
				return this.m_itemsCreated;
			}
			set
			{
				this.m_itemsCreated = value;
			}
		}

		public int[] IndexesLeftToRight
		{
			get
			{
				return this.m_indexesLeftToRight;
			}
			set
			{
				this.m_indexesLeftToRight = value;
			}
		}

		public int[] IndexesTopToBottom
		{
			get
			{
				return this.m_indexesTopToBottom;
			}
			set
			{
				this.m_indexesTopToBottom = value;
			}
		}

		public PageItemHelper[] RepeatWithItems
		{
			get
			{
				return this.m_repeatWithItems;
			}
			set
			{
				this.m_repeatWithItems = value;
			}
		}

		public PageItemHelper RightEdgeItem
		{
			get
			{
				return this.m_rightEdgeItem;
			}
			set
			{
				this.m_rightEdgeItem = value;
			}
		}

		public PageItemHelper[] Children
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

		public PageItemContainerHelper(byte type)
			: base(type)
		{
		}
	}
}
