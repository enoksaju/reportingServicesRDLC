namespace AspNetCore.ReportingServices.Rendering.HPBProcessing
{
	public sealed class CachedSharedImageInfo
	{
		private string m_streamName;

		private ItemBoundaries m_itemBoundaries;

		public string StreamName
		{
			get
			{
				return this.m_streamName;
			}
		}

		public ItemBoundaries ImageBounderies
		{
			get
			{
				return this.m_itemBoundaries;
			}
		}

		public CachedSharedImageInfo(string streamName, ItemBoundaries itemBoundaries)
		{
			this.m_streamName = streamName;
			this.m_itemBoundaries = itemBoundaries;
		}
	}
}
