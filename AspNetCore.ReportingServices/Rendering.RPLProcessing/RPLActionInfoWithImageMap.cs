namespace AspNetCore.ReportingServices.Rendering.RPLProcessing
{
	public sealed class RPLActionInfoWithImageMap : RPLActionInfo
	{
		private RPLImageMapCollection m_imageMaps;

		public RPLImageMapCollection ImageMaps
		{
			get
			{
				return this.m_imageMaps;
			}
			set
			{
				this.m_imageMaps = value;
			}
		}

		public RPLActionInfoWithImageMap()
		{
		}

		public RPLActionInfoWithImageMap(int actionCount)
			: base(actionCount)
		{
		}
	}
}
