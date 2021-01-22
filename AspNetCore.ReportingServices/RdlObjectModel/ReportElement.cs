namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public abstract class ReportElement : ReportObject
	{
		public class Definition : DefinitionStore<ReportElement, Definition.Properties>
		{
			public enum Properties
			{
				Style
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

		public ReportElement()
		{
		}

		public ReportElement(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
