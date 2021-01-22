using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapFieldDefinition : ReportObject, INamedObject
	{
		public class Definition : DefinitionStore<MapFieldDefinition, Definition.Properties>
		{
			public enum Properties
			{
				Name,
				DataType,
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

		public MapDataTypes DataType
		{
			get
			{
				return (MapDataTypes)base.PropertyStore.GetInteger(1);
			}
			set
			{
				base.PropertyStore.SetInteger(1, (int)value);
			}
		}

		public MapFieldDefinition()
		{
		}

		public MapFieldDefinition(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}
