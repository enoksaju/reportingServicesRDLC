using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class MapSpatialElement : MapObjectCollectionItem
	{
		protected Map m_map;

		protected MapVectorLayer m_mapVectorLayer;

		private AspNetCore.ReportingServices.ReportIntermediateFormat.MapSpatialElement m_defObject;

		private MapFieldCollection m_mapFields;

		public string VectorData
		{
			get
			{
				return this.m_defObject.VectorData;
			}
		}

		public MapFieldCollection MapFields
		{
			get
			{
				if (this.m_mapFields == null && this.m_defObject.MapFields != null)
				{
					this.m_mapFields = new MapFieldCollection(this, this.m_map);
				}
				return this.m_mapFields;
			}
		}

		public IReportScope ReportScope
		{
			get
			{
				return this.m_mapVectorLayer.ReportScope;
			}
		}

		public Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		public AspNetCore.ReportingServices.ReportIntermediateFormat.MapSpatialElement MapSpatialElementDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		public MapSpatialElementInstance Instance
		{
			get
			{
				return this.GetInstance();
			}
		}

		public MapSpatialElement(AspNetCore.ReportingServices.ReportIntermediateFormat.MapSpatialElement defObject, MapVectorLayer mapVectorLayer, Map map)
		{
			this.m_defObject = defObject;
			this.m_mapVectorLayer = mapVectorLayer;
			this.m_map = map;
		}

		public abstract MapSpatialElementInstance GetInstance();

		public override void SetNewContext()
		{
			if (base.m_instance != null)
			{
				base.m_instance.SetNewContext();
			}
			if (this.m_mapFields != null)
			{
				this.m_mapFields.SetNewContext();
			}
		}
	}
}
