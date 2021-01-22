namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class ChartBorderSkin : ReportObject
	{
		public class Definition : DefinitionStore<ChartBorderSkin, Definition.Properties>
		{
			public enum Properties
			{
				ChartBorderSkinType,
				Style,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue(typeof(ChartBorderSkinTypes), ChartBorderSkinTypes.None)]
		public ReportExpression<ChartBorderSkinTypes> ChartBorderSkinType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartBorderSkinTypes>>(0);
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

		public ChartBorderSkin()
		{
		}

		public ChartBorderSkin(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
