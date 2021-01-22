using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapMarkerRule : MapAppearanceRule
	{
		private MapMarkerCollection m_mapMarkers;

		public MapMarkerCollection MapMarkers
		{
			get
			{
				if (this.m_mapMarkers == null && this.MapMarkerRuleDef.MapMarkers != null)
				{
					this.m_mapMarkers = new MapMarkerCollection(this, base.m_map);
				}
				return this.m_mapMarkers;
			}
		}

		public AspNetCore.ReportingServices.ReportIntermediateFormat.MapMarkerRule MapMarkerRuleDef
		{
			get
			{
				return (AspNetCore.ReportingServices.ReportIntermediateFormat.MapMarkerRule)base.MapAppearanceRuleDef;
			}
		}

		public new MapMarkerRuleInstance Instance
		{
			get
			{
				return (MapMarkerRuleInstance)this.GetInstance();
			}
		}

		public MapMarkerRule(AspNetCore.ReportingServices.ReportIntermediateFormat.MapMarkerRule defObject, MapVectorLayer mapVectorLayer, Map map)
			: base(defObject, mapVectorLayer, map)
		{
		}

		public override MapAppearanceRuleInstance GetInstance()
		{
			if (base.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (base.m_instance == null)
			{
				base.m_instance = new MapMarkerRuleInstance(this);
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
			if (this.m_mapMarkers != null)
			{
				this.m_mapMarkers.SetNewContext();
			}
		}
	}
}
