using AspNetCore.ReportingServices.RdlObjectModel;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel2005
{
	internal class TableRow2005 : ReportObject
	{
		internal class Definition : DefinitionStore<TableRow2005, Definition.Properties>
		{
			internal enum Properties
			{
				TableCells,
				Height,
				Visibility
			}

			private Definition()
			{
			}
		}

		[XmlArrayItem("TableCell", typeof(TableCell2005))]
		[XmlElement(typeof(RdlCollection<TableCell2005>))]
		public IList<TableCell2005> TableCells
		{
			get
			{
				return (IList<TableCell2005>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		public ReportSize Height
		{
			get
			{
				return base.PropertyStore.GetSize(1);
			}
			set
			{
				base.PropertyStore.SetSize(1, value);
			}
		}

		public Visibility Visibility
		{
			get
			{
				return (Visibility)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		public TableRow2005()
		{
		}

		public TableRow2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.TableCells = new RdlCollection<TableCell2005>();
			this.Height = Constants.DefaultZeroSize;
		}
	}
}
