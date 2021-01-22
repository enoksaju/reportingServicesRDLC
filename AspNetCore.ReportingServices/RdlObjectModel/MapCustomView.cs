namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapCustomView : MapView
	{
		public new class Definition : DefinitionStore<MapCustomView, Definition.Properties>
		{
			public enum Properties
			{
				Zoom,
				CenterX,
				CenterY,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue(typeof(double), "50")]
		public ReportExpression<double> CenterX
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

		[ReportExpressionDefaultValue(typeof(double), "50")]
		public ReportExpression<double> CenterY
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

		public MapCustomView()
		{
		}

		public MapCustomView(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.CenterX = 50.0;
			this.CenterY = 50.0;
		}
	}
}
