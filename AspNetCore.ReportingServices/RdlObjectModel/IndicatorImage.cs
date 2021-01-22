namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class IndicatorImage : BaseGaugeImage
	{
		public new class Definition : DefinitionStore<IndicatorImage, Definition.Properties>
		{
			public enum Properties
			{
				Source,
				Value,
				MIMEType,
				TransparentColor,
				HueColor,
				Transparency,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> HueColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Transparency
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		public IndicatorImage()
		{
		}

		public IndicatorImage(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
