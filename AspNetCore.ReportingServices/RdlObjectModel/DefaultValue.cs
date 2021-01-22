using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class DefaultValue : ReportObject
	{
		public class Definition : DefinitionStore<DefaultValue, Definition.Properties>
		{
			public enum Properties
			{
				DataSetReference,
				Values
			}

			private Definition()
			{
			}
		}

		public DataSetReference DataSetReference
		{
			get
			{
				return (DataSetReference)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		[XmlElement(typeof(RdlCollection<ReportExpression?>))]
		[XmlArrayItem("Value", typeof(ReportExpression), IsNullable = true)]
		public IList<ReportExpression?> Values
		{
			get
			{
				return (IList<ReportExpression?>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public DefaultValue()
		{
		}

		public DefaultValue(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.Values = new RdlCollection<ReportExpression?>();
		}
	}
}
