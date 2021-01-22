namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapColorPaletteRule : MapColorRule
	{
		public new class Definition : DefinitionStore<MapColorPaletteRule, Definition.Properties>
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
				ShowInColorScale,
				Palette,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue(typeof(MapPalettes), MapPalettes.Random)]
		public ReportExpression<MapPalettes> Palette
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapPalettes>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		public MapColorPaletteRule()
		{
		}

		public MapColorPaletteRule(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.Palette = MapPalettes.Random;
		}
	}
}
