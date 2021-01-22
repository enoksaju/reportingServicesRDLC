using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapElementView : MapView
	{
		private ReportStringProperty m_layerName;

		private MapBindingFieldPairCollection m_mapBindingFieldPairs;

		public ReportStringProperty LayerName
		{
			get
			{
				if (this.m_layerName == null && this.MapElementViewDef.LayerName != null)
				{
					this.m_layerName = new ReportStringProperty(this.MapElementViewDef.LayerName);
				}
				return this.m_layerName;
			}
		}

		public MapBindingFieldPairCollection MapBindingFieldPairs
		{
			get
			{
				if (this.m_mapBindingFieldPairs == null && this.MapElementViewDef.MapBindingFieldPairs != null)
				{
					this.m_mapBindingFieldPairs = new MapBindingFieldPairCollection(this.MapElementViewDef.MapBindingFieldPairs, base.m_map);
				}
				return this.m_mapBindingFieldPairs;
			}
		}

		public AspNetCore.ReportingServices.ReportIntermediateFormat.MapElementView MapElementViewDef
		{
			get
			{
				return (AspNetCore.ReportingServices.ReportIntermediateFormat.MapElementView)base.MapViewDef;
			}
		}

		public new MapElementViewInstance Instance
		{
			get
			{
				return (MapElementViewInstance)this.GetInstance();
			}
		}

		public MapElementView(AspNetCore.ReportingServices.ReportIntermediateFormat.MapElementView defObject, Map map)
			: base(defObject, map)
		{
		}

		public override MapViewInstance GetInstance()
		{
			if (base.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (base.m_instance == null)
			{
				base.m_instance = new MapElementViewInstance(this);
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
			if (this.m_mapBindingFieldPairs != null)
			{
				this.m_mapBindingFieldPairs.SetNewContext();
			}
		}
	}
}
