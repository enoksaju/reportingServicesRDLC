namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapPoint : MapSpatialElement
	{
		public new class Definition : DefinitionStore<MapPoint, Definition.Properties>
		{
			public enum Properties
			{
				VectorData,
				MapFields,
				UseCustomPointTemplate,
				MapPointTemplate,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> UseCustomPointTemplate
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		public MapPointTemplate MapPointTemplate
		{
			get
			{
				return (MapPointTemplate)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		public MapPoint()
		{
		}

		public MapPoint(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}
