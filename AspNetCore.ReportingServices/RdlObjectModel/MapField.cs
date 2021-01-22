using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapField : ReportObject, INamedObject
	{
		public class Definition : DefinitionStore<MapField, Definition.Properties>
		{
			public enum Properties
			{
				Name,
				Value,
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

		public string Value
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

		public MapField()
		{
		}

		public MapField(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}
