using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapTile : ReportObject, INamedObject
	{
		public class Definition : DefinitionStore<MapTile, Definition.Properties>
		{
			public enum Properties
			{
				Name,
				TileData,
				MIMEType,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[XmlElement("Name")]
		public string Name
		{
			get
			{
				return base.PropertyStore.GetObject<string>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		public string TileData
		{
			get
			{
				return base.PropertyStore.GetObject<string>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public string MIMEType
		{
			get
			{
				return base.PropertyStore.GetObject<string>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		public MapTile()
		{
		}

		public MapTile(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}
