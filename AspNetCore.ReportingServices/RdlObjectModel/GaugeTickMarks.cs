namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class GaugeTickMarks : TickMarkStyle
	{
		public new class Definition : DefinitionStore<GaugeTickMarks, Definition.Properties>
		{
			public enum Properties
			{
				Style,
				DistanceFromScale,
				Placement,
				EnableGradient,
				GradientDensity,
				TickMarkImage,
				Length,
				Width,
				Shape,
				Hidden,
				Interval,
				IntervalOffset,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Interval
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> IntervalOffset
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		public GaugeTickMarks()
		{
		}

		public GaugeTickMarks(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
