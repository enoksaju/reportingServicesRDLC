using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class MapTile : MapObjectCollectionItem
	{
		private Map m_map;

		private AspNetCore.ReportingServices.ReportIntermediateFormat.MapTile m_defObject;

		public string Name
		{
			get
			{
				return this.m_defObject.Name;
			}
		}

		public string TileData
		{
			get
			{
				return this.m_defObject.TileData;
			}
		}

		public string MIMEType
		{
			get
			{
				return this.m_defObject.MIMEType;
			}
		}

		public Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		public AspNetCore.ReportingServices.ReportIntermediateFormat.MapTile MapTileDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		public MapTileInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (base.m_instance == null)
				{
					base.m_instance = new MapTileInstance(this);
				}
				return (MapTileInstance)base.m_instance;
			}
		}

		public MapTile(AspNetCore.ReportingServices.ReportIntermediateFormat.MapTile defObject, Map map)
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
