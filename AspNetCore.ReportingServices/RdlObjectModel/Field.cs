using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class Field : ReportObject, INamedObject
	{
		public class Definition : DefinitionStore<Field, Definition.Properties>
		{
			public enum Properties
			{
				Name,
				DataField,
				Value
			}
		}

		[XmlAttribute(typeof(string))]
		public string Name
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		public string DataField
		{
			get
			{
				return (string)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		[ReportExpressionDefaultValue]
		public ReportExpression Value
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		public Field()
		{
		}

		public Field(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
