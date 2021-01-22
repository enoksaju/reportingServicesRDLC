using System.ComponentModel;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class CellContents : ReportObject
	{
		public class Definition : DefinitionStore<CellContents, Definition.Properties>
		{
			public enum Properties
			{
				ReportItem,
				ColSpan,
				RowSpan
			}

			private Definition()
			{
			}
		}

		public ReportItem ReportItem
		{
			get
			{
				return (ReportItem)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		[ValidValues(1, 2147483647)]
		[DefaultValue(1)]
		public int ColSpan
		{
			get
			{
				return base.PropertyStore.GetInteger(1);
			}
			set
			{
				((IntProperty)DefinitionStore<CellContents, Definition.Properties>.GetProperty(1)).Validate(this, value);
				base.PropertyStore.SetInteger(1, value);
			}
		}

		[DefaultValue(1)]
		[ValidValues(1, 2147483647)]
		public int RowSpan
		{
			get
			{
				return base.PropertyStore.GetInteger(2);
			}
			set
			{
				((IntProperty)DefinitionStore<CellContents, Definition.Properties>.GetProperty(2)).Validate(this, value);
				base.PropertyStore.SetInteger(2, value);
			}
		}

		public CellContents()
		{
		}

		public CellContents(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.ColSpan = 1;
			this.RowSpan = 1;
		}
	}
}
