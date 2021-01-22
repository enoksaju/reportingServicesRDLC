namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class FrameBackground : ReportObject
	{
		public class Definition : DefinitionStore<FrameBackground, Definition.Properties>
		{
			public enum Properties
			{
				Style,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		public FrameBackground()
		{
		}

		public FrameBackground(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
