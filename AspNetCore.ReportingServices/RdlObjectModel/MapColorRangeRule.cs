using System.Globalization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapColorRangeRule : MapColorRule
	{
		public new class Definition : DefinitionStore<MapColorRangeRule, Definition.Properties>
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
				StartColor,
				MiddleColor,
				EndColor,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue(typeof(ReportColor), "Green")]
		public ReportExpression<ReportColor> StartColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(ReportColor), "Yellow")]
		public ReportExpression<ReportColor> MiddleColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(ReportColor), "Red")]
		public ReportExpression<ReportColor> EndColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		public MapColorRangeRule()
		{
		}

		public MapColorRangeRule(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.StartColor = new ReportExpression<ReportColor>("Green", CultureInfo.InvariantCulture);
			this.MiddleColor = new ReportExpression<ReportColor>("Yellow", CultureInfo.InvariantCulture);
			this.EndColor = new ReportExpression<ReportColor>("Red", CultureInfo.InvariantCulture);
		}
	}
}
