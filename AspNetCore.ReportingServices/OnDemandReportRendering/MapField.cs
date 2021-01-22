using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapField : MapObjectCollectionItem
	{
		private Map m_map;

		private AspNetCore.ReportingServices.ReportIntermediateFormat.MapField m_defObject;

		public string Name
		{
			get
			{
				return this.m_defObject.Name;
			}
		}

		public string Value
		{
			get
			{
				return this.m_defObject.Value;
			}
		}

		public Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		public AspNetCore.ReportingServices.ReportIntermediateFormat.MapField MapFieldDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		public MapFieldInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (base.m_instance == null)
				{
					base.m_instance = new MapFieldInstance(this);
				}
				return (MapFieldInstance)base.m_instance;
			}
		}

		public MapField(AspNetCore.ReportingServices.ReportIntermediateFormat.MapField defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		public override void SetNewContext()
		{
			if (base.m_instance != null)
			{
				base.m_instance.SetNewContext();
			}
		}
	}
}
