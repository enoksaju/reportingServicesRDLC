namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class ChartLegendTitle : ReportObject
	{
		public class Definition : DefinitionStore<ChartLegendTitle, Definition.Properties>
		{
			public enum Properties
			{
				Caption,
				CaptionLocID,
				TitleSeparator,
				Style,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		public ReportExpression Caption
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

		[ReportExpressionDefaultValue(typeof(ChartTitleSeparatorTypes), ChartTitleSeparatorTypes.None)]
		public ReportExpression<ChartTitleSeparatorTypes> TitleSeparator
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartTitleSeparatorTypes>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		public ChartLegendTitle()
		{
		}

		public ChartLegendTitle(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
