namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class TablixColumn : ReportObject
	{
		public class Definition : DefinitionStore<TablixColumn, Definition.Properties>
		{
			public enum Properties
			{
				Width
			}

			private Definition()
			{
			}
		}

		public ReportSize Width
		{
			get
			{
				return base.PropertyStore.GetSize(0);
			}
			set
			{
				base.PropertyStore.SetSize(0, value);
			}
		}

		public TablixColumn()
		{
		}

		public TablixColumn(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.Width = Constants.DefaultZeroSize;
		}
	}
}
