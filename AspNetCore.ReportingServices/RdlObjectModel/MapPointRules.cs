namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class MapPointRules : ReportObject
	{
		public class Definition : DefinitionStore<MapPointRules, Definition.Properties>
		{
			public enum Properties
			{
				MapSizeRule,
				MapColorRule,
				MapMarkerRule,
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

		public MapMarkerRule MapMarkerRule
		{
			get
			{
				return (MapMarkerRule)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		public MapPointRules()
		{
		}

		public MapPointRules(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}
	}
}
