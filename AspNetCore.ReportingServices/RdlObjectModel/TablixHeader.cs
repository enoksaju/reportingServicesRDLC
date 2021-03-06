namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class TablixHeader : ReportObject
	{
		public class Definition : DefinitionStore<TablixHeader, Definition.Properties>
		{
			public enum Properties
			{
				Size,
				CellContents
			}

			private Definition()
			{
			}
		}

		public ReportSize Size
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

		public CellContents CellContents
		{
			get
			{
				return (CellContents)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public TablixHeader()
		{
		}

		public TablixHeader(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.Size = Constants.DefaultZeroSize;
			this.CellContents = new CellContents();
		}
	}
}
