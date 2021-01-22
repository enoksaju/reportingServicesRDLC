using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapShapefile : MapSpatialData
	{
		private ReportStringProperty m_source;

		private MapFieldNameCollection m_mapFieldNames;

		public ReportStringProperty Source
		{
			get
			{
				if (this.m_source == null && this.MapShapefileDef.Source != null)
				{
					this.m_source = new ReportStringProperty(this.MapShapefileDef.Source);
				}
				return this.m_source;
			}
		}

		public MapFieldNameCollection MapFieldNames
		{
			get
			{
				if (this.m_mapFieldNames == null && this.MapShapefileDef.MapFieldNames != null)
				{
					this.m_mapFieldNames = new MapFieldNameCollection(this.MapShapefileDef.MapFieldNames, base.m_map);
				}
				return this.m_mapFieldNames;
			}
		}

		public AspNetCore.ReportingServices.ReportIntermediateFormat.MapShapefile MapShapefileDef
		{
			get
			{
				return (AspNetCore.ReportingServices.ReportIntermediateFormat.MapShapefile)base.MapSpatialDataDef;
			}
		}

		public new MapShapefileInstance Instance
		{
			get
			{
				return (MapShapefileInstance)this.GetInstance();
			}
		}

		public MapShapefile(MapVectorLayer mapVectorLayer, Map map)
			: base(mapVectorLayer, map)
		{
		}

		public override MapSpatialDataInstance GetInstance()
		{
			if (base.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (base.m_instance == null)
			{
				base.m_instance = new MapShapefileInstance(this);
			}
			return base.m_instance;
		}

		public override void SetNewContext()
		{
			base.SetNewContext();
			if (base.m_instance != null)
			{
				base.m_instance.SetNewContext();
			}
			if (this.m_mapFieldNames != null)
			{
				this.m_mapFieldNames.SetNewContext();
			}
		}
	}
}
