using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class TablixRow : ReportObject
	{
		public class Definition : DefinitionStore<TablixRow, Definition.Properties>
		{
			public enum Properties
			{
				Height,
				TablixCells
			}

			private Definition()
			{
			}
		}

		public ReportSize Height
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

		[XmlElement(typeof(RdlCollection<TablixCell>))]
		public IList<TablixCell> TablixCells
		{
			get
			{
				return (IList<TablixCell>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public TablixRow()
		{
		}

		public TablixRow(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.Height = Constants.DefaultZeroSize;
			this.TablixCells = new RdlCollection<TablixCell>();
		}
	}
}
