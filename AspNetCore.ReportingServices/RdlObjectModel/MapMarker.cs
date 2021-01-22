namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapMarker : ReportObject
	{
		public class Definition : DefinitionStore<MapMarker, Definition.Properties>
		{
			public enum Properties
			{
				MapMarkerStyle,
				MapMarkerImage,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue(typeof(MapMarkerStyles), MapMarkerStyles.None)]
		public ReportExpression<MapMarkerStyles> MapMarkerStyle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapMarkerStyles>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		public MapMarkerImage MapMarkerImage
		{
			get
			{
				return (MapMarkerImage)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public MapMarker()
		{
		}

		public MapMarker(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.MapMarkerStyle = MapMarkerStyles.None;
		}
	}
}
