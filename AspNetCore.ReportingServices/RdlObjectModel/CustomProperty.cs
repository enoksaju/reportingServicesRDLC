namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class CustomProperty : ReportObject
	{
		public class Definition : DefinitionStore<CustomProperty, Definition.Properties>
		{
			public enum Properties
			{
				Name,
				Value
			}

			private Definition()
			{
			}
		}

		public ReportExpression Name
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

		public ReportExpression Value
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public CustomProperty()
		{
		}

		public CustomProperty(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
