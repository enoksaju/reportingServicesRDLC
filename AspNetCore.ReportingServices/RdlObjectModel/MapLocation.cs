namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapLocation : ReportObject
	{
		public class Definition : DefinitionStore<MapLocation, Definition.Properties>
		{
			public enum Properties
			{
				Left,
				Top,
				Unit,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue(typeof(double), "0")]
		public ReportExpression<double> Left
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

		[ReportExpressionDefaultValue(typeof(double), "0")]
		public ReportExpression<double> Top
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

		public MapLocation()
		{
		}

		public MapLocation(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.Left = 0.0;
			this.Top = 0.0;
			this.Unit = MapUnits.Percentage;
		}
	}
}
