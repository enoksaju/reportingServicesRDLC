using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class MapSpatialData
	{
		protected Map m_map;

		protected MapVectorLayer m_mapVectorLayer;

		private AspNetCore.ReportingServices.ReportIntermediateFormat.MapSpatialData m_defObject;

		protected MapSpatialDataInstance m_instance;

		public Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		public AspNetCore.ReportingServices.ReportIntermediateFormat.MapSpatialData MapSpatialDataDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		public MapSpatialDataInstance Instance
		{
			get
			{
				return this.GetInstance();
			}
		}

		public MapSpatialData(MapVectorLayer mapVectorLayer, Map map)
		{
			this.m_defObject = mapVectorLayer.MapVectorLayerDef.MapSpatialData;
			this.m_mapVectorLayer = mapVectorLayer;
			this.m_map = map;
		}

		public abstract MapSpatialDataInstance GetInstance();

		public virtual void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}
	}
}
