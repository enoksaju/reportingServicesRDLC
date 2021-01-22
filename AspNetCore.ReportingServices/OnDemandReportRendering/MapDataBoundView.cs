using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapDataBoundView : MapView
	{
		public AspNetCore.ReportingServices.ReportIntermediateFormat.MapDataBoundView MapDataBoundViewDef
		{
			get
			{
				return (AspNetCore.ReportingServices.ReportIntermediateFormat.MapDataBoundView)base.MapViewDef;
			}
		}

		public new MapDataBoundViewInstance Instance
		{
			get
			{
				return (MapDataBoundViewInstance)this.GetInstance();
			}
		}

		public MapDataBoundView(AspNetCore.ReportingServices.ReportIntermediateFormat.MapDataBoundView defObject, Map map)
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
				base.m_instance = new MapDataBoundViewInstance(this);
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
		}
	}
}
