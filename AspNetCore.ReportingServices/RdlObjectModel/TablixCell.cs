using System.ComponentModel;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class TablixCell : DataRegionCell
	{
		public class Definition : DefinitionStore<TablixCell, Definition.Properties>
		{
			public enum Properties
			{
				CellContents,
				DataElementName,
				DataElementOutput
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

		[DefaultValue("")]
		public string DataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		[DefaultValue(DataElementOutputTypes.ContentsOnly)]
		[ValidEnumValues("TablixCellDataElementOutputTypes")]
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(2);
			}
			set
			{
				base.PropertyStore.SetInteger(2, (int)value);
			}
		}

		public TablixCell()
		{
		}

		public TablixCell(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.DataElementOutput = DataElementOutputTypes.ContentsOnly;
		}
	}
}
