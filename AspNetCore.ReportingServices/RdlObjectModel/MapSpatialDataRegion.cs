namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapSpatialDataRegion : MapSpatialData
	{
		public class Definition : DefinitionStore<MapSpatialDataRegion, Definition.Properties>
		{
			public enum Properties
			{
				VectorData,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		public ReportExpression VectorData
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		public MapSpatialDataRegion()
		{
		}

		public MapSpatialDataRegion(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}
