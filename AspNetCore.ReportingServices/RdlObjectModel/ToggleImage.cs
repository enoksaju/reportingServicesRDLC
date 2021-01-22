namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class ToggleImage : ReportObject
	{
		public class Definition : DefinitionStore<ToggleImage, Definition.Properties>
		{
			public enum Properties
			{
				InitialState
			}

			private Definition()
			{
			}
		}

		public ReportExpression<bool> InitialState
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		public ToggleImage()
		{
		}

		public ToggleImage(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
