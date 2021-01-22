using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class Parameter : ReportObject, INamedObject
	{
		public class Definition : DefinitionStore<Parameter, Definition.Properties>
		{
			public enum Properties
			{
				Name,
				Value,
				Omit
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

		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Omit
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		public Parameter()
		{
		}

		public Parameter(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
