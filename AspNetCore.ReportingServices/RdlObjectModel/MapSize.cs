namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapSize : ReportObject
	{
		public class Definition : DefinitionStore<MapSize, Definition.Properties>
		{
			public enum Properties
			{
				Width,
				Height,
				Unit,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		public ReportExpression<double> Width
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		public ReportExpression<double> Height
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		[ReportExpressionDefaultValue(typeof(MapUnits), MapUnits.Percentage)]
		public ReportExpression<MapUnits> Unit
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapUnits>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		public MapSize()
		{
		}

		public MapSize(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.Unit = MapUnits.Percentage;
		}
	}
}
