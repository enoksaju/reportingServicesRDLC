namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapDataBoundView : MapView
	{
		public new class Definition : DefinitionStore<MapDataBoundView, Definition.Properties>
		{
			public enum Properties
			{
				Zoom,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		public MapDataBoundView()
		{
		}

		public MapDataBoundView(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}
