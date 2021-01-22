using AspNetCore.ReportingServices.RdlObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel2005
{
	public class Footer2005 : ReportObject
	{
		public class Definition : DefinitionStore<Footer2005, Definition.Properties>
		{
			public enum Properties
			{
				TableRows,
				RepeatOnNewPage
			}

			private Definition()
			{
			}
		}

		[XmlArrayItem("TableRow", typeof(TableRow2005))]
		[XmlElement(typeof(RdlCollection<TableRow2005>))]
		public IList<TableRow2005> TableRows
		{
			get
			{
				return (IList<TableRow2005>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		[DefaultValue(false)]
		public bool RepeatOnNewPage
		{
			get
			{
				return base.PropertyStore.GetBoolean(1);
			}
			set
			{
				base.PropertyStore.SetBoolean(1, value);
			}
		}

		public Footer2005()
		{
		}

		public Footer2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.TableRows = new RdlCollection<TableRow2005>();
		}
	}
}
