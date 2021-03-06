namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapTitleCollection : MapObjectCollectionBase<MapTitle>
	{
		private Map m_map;

		public override int Count
		{
			get
			{
				return this.m_map.MapDef.MapTitles.Count;
			}
		}

		public MapTitleCollection(Map map)
		{
			this.m_map = map;
		}

		protected override MapTitle CreateMapObject(int index)
		{
			return new MapTitle(this.m_map.MapDef.MapTitles[index], this.m_map);
		}
	}
}
