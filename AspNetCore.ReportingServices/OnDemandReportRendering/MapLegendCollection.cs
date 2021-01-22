namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapLegendCollection : MapObjectCollectionBase<MapLegend>
	{
		private Map m_map;

		public override int Count
		{
			get
			{
				return this.m_map.MapDef.MapLegends.Count;
			}
		}

		public MapLegendCollection(Map map)
		{
			this.m_map = map;
		}

		protected override MapLegend CreateMapObject(int index)
		{
			return new MapLegend(this.m_map.MapDef.MapLegends[index], this.m_map);
		}
	}
}
