namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class ChartLegendColumnHeader : ReportObject
	{
		public class Definition : DefinitionStore<ChartLegendColumnHeader, Definition.Properties>
		{
			public enum Properties
			{
				Value,
				Style,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue]
		public ReportExpression Value
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

		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public ChartLegendColumnHeader()
		{
		}

		public ChartLegendColumnHeader(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
