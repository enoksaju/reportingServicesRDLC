namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapLineRules : ReportObject
	{
		public class Definition : DefinitionStore<MapLineRules, Definition.Properties>
		{
			public enum Properties
			{
				MapSizeRule,
				MapColorRule,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		public MapSizeRule MapSizeRule
		{
			get
			{
				return (MapSizeRule)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		public MapColorRule MapColorRule
		{
			get
			{
				return (MapColorRule)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public MapLineRules()
		{
		}

		public MapLineRules(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}
