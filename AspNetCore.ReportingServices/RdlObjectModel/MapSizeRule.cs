namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapSizeRule : MapAppearanceRule
	{
		public new class Definition : DefinitionStore<MapSizeRule, Definition.Properties>
		{
			public enum Properties
			{
				DataValue,
				DistributionType,
				BucketCount,
				StartValue,
				EndValue,
				MapBuckets,
				LegendName,
				LegendText,
				DataElementName,
				DataElementOutput,
				StartSize,
				EndSize,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		public ReportExpression<ReportSize> StartSize
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		public ReportExpression<ReportSize> EndSize
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		public MapSizeRule()
		{
		}

		public MapSizeRule(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}
