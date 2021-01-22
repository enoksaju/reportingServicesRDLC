namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class TickMarkImage : BaseGaugeImage
	{
		public new class Definition : DefinitionStore<TickMarkImage, Definition.Properties>
		{
			public enum Properties
			{
				Source,
				Value,
				MIMEType,
				TransparentColor,
				HueColor,
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

		public TickMarkImage()
		{
		}

		public TickMarkImage(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
