using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class ChartCodeParameter : ReportObject, INamedObject
	{
		public class Definition : DefinitionStore<ChartCodeParameter, Definition.Properties>
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

		public ReportExpression Value
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public ChartCodeParameter()
		{
		}

		public ChartCodeParameter(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
