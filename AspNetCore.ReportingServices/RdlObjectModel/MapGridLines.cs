namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapGridLines : ReportObject
	{
		public class Definition : DefinitionStore<MapGridLines, Definition.Properties>
		{
			public enum Properties
			{
				Style,
				Hidden,
				Interval,
				ShowLabels,
				LabelPosition,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public ReportExpression<double> Interval
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		public ReportExpression<bool> ShowLabels
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(MapLabelPositions), MapLabelPositions.Near)]
		public ReportExpression<MapLabelPositions> LabelPosition
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapLabelPositions>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		public MapGridLines()
		{
		}

		public MapGridLines(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.LabelPosition = MapLabelPositions.Near;
		}
	}
}
