namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class CapImage : BaseGaugeImage
	{
		public new class Definition : DefinitionStore<CapImage, Definition.Properties>
		{
			public enum Properties
			{
				Source,
				Value,
				MIMEType,
				TransparentColor,
				HueColor,
				OffsetX,
				OffsetY,
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

		[ReportExpressionDefaultValue(typeof(ReportSize))]
		public ReportExpression<ReportSize> OffsetX
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(ReportSize))]
		public ReportExpression<ReportSize> OffsetY
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		public CapImage()
		{
		}

		public CapImage(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
