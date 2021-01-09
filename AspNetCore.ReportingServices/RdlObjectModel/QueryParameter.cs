using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	internal class QueryParameter : ReportObject, INamedObject
	{
		internal class Definition : DefinitionStore<QueryParameter, Definition.Properties>
		{
			internal enum Properties
			{
				Name,
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

		public QueryParameter()
		{
		}

		internal QueryParameter(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
