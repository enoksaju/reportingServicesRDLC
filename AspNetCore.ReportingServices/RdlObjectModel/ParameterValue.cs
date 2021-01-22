using System.ComponentModel;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class ParameterValue : ReportObject
	{
		public class Definition : DefinitionStore<ParameterValue, Definition.Properties>
		{
			public enum Properties
			{
				Value,
				Label,
				LabelLocID
			}

			private Definition()
			{
			}
		}

		[DefaultValue(null)]
		public ReportExpression? Value
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression?>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		[ReportExpressionDefaultValue]
		public ReportExpression Label
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

		public ParameterValue()
		{
		}

		public ParameterValue(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
