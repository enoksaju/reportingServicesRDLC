namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class RadialGauge : Gauge
	{
		public new class Definition : DefinitionStore<RadialGauge, Definition.Properties>
		{
			public enum Properties
			{
				Name,
				Style,
				Top,
				Left,
				Height,
				Width,
				ZIndex,
				Hidden,
				ToolTip,
				ActionInfo,
				ParentItem,
				GaugeScales,
				BackFrame,
				ClipContent,
				TopImage,
				AspectRatio,
				PivotX,
				PivotY,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue(typeof(double), 50.0)]
		public ReportExpression<double> PivotX
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(double), 50.0)]
		public ReportExpression<double> PivotY
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(17);
			}
			set
			{
				base.PropertyStore.SetObject(17, value);
			}
		}

		public RadialGauge()
		{
		}

		public RadialGauge(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.PivotX = 50.0;
			this.PivotY = 50.0;
		}
	}
}
