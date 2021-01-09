using AspNetCore.ReportingServices.RdlObjectModel.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	[XmlElementClass("MapColorPaletteRule", typeof(MapColorPaletteRule))]
	[XmlElementClass("MapCustomColorRule", typeof(MapCustomColorRule))]
	[XmlElementClass("MapColorRangeRule", typeof(MapColorRangeRule))]
	internal abstract class MapColorRule : MapAppearanceRule
	{
		internal new class Definition : DefinitionStore<MapColorRule, Definition.Properties>
		{
			internal enum Properties
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
				ShowInColorScale
			}

			private Definition()
			{
			}
		}

		public ReportExpression<bool> ShowInColorScale
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		public MapColorRule()
		{
		}

		internal MapColorRule(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}
