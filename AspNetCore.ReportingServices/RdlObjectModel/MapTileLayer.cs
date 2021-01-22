using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapTileLayer : MapLayer
	{
		public new class Definition : DefinitionStore<MapTileLayer, Definition.Properties>
		{
			public enum Properties
			{
				Name,
				VisibilityMode,
				MinimumZoom,
				MaximumZoom,
				Transparency,
				TileStyle,
				MapTiles,
				UserSecureConnection,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue(typeof(MapTileStyles), MapTileStyles.Road)]
		public ReportExpression<MapTileStyles> TileStyle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapTileStyles>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		[XmlElement(typeof(RdlCollection<MapTile>))]
		public IList<MapTile> MapTiles
		{
			get
			{
				return (IList<MapTile>)base.PropertyStore.GetObject(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		public ReportExpression<bool> UseSecureConnection
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		public MapTileLayer()
		{
		}

		public MapTileLayer(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.TileStyle = MapTileStyles.Road;
			this.MapTiles = new RdlCollection<MapTile>();
		}
	}
}
