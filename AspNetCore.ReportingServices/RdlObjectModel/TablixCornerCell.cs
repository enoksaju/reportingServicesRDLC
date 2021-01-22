namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class TablixCornerCell : ReportObject
	{
		public class Definition : DefinitionStore<TablixCornerCell, Definition.Properties>
		{
			public enum Properties
			{
				CellContents
			}

			private Definition()
			{
			}
		}

		public CellContents CellContents
		{
			get
			{
				return (CellContents)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		public TablixCornerCell()
		{
		}

		public TablixCornerCell(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
