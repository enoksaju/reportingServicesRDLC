namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class ScalePin : TickMarkStyle
	{
		public new class Definition : DefinitionStore<ScalePin, Definition.Properties>
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
				Location,
				Enable,
				PinLabel,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue(typeof(double), 5.0)]
		public ReportExpression<double> Location
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

		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Enable
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		public PinLabel PinLabel
		{
			get
			{
				return (PinLabel)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		public ScalePin()
		{
		}

		public ScalePin(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.Location = 5.0;
		}
	}
}
