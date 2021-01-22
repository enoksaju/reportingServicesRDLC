using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class ValidValues : ReportObject
	{
		public class Definition : DefinitionStore<ValidValues, Definition.Properties>
		{
			public enum Properties
			{
				DataSetReference,
				ParameterValues,
				ValidationExpression
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

		[XmlElement(typeof(RdlCollection<ParameterValue>))]
		public IList<ParameterValue> ParameterValues
		{
			get
			{
				return (IList<ParameterValue>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		[ReportExpressionDefaultValue]
		public ReportExpression ValidationExpression
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

		public ValidValues()
		{
		}

		public ValidValues(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.ParameterValues = new RdlCollection<ParameterValue>();
		}
	}
}
