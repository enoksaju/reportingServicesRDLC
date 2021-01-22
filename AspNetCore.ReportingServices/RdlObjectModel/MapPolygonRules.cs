namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapPolygonRules : ReportObject
	{
		public class Definition : DefinitionStore<MapPolygonRules, Definition.Properties>
		{
			public enum Properties
			{
				MapColorRule,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		public MapColorRule MapColorRule
		{
			get
			{
				return (MapColorRule)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		public MapPolygonRules()
		{
		}

		public MapPolygonRules(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}
