namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class DataValue : ReportObject
	{
		public class Definition : DefinitionStore<DataValue, Definition.Properties>
		{
			public enum Properties
			{
				Name,
				Value
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue]
		public ReportExpression Name
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
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

		public DataValue()
		{
		}

		public DataValue(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
