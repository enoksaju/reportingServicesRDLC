using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class TablixBody : DataRegionBody
	{
		public class Definition : DefinitionStore<TablixBody, Definition.Properties>
		{
			public enum Properties
			{
				TablixColumns,
				TablixRows
			}

			private Definition()
			{
			}
		}

		[XmlElement(typeof(RdlCollection<TablixColumn>))]
		public IList<TablixColumn> TablixColumns
		{
			get
			{
				return (IList<TablixColumn>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		[XmlElement(typeof(RdlCollection<TablixRow>))]
		public IList<TablixRow> TablixRows
		{
			get
			{
				return (IList<TablixRow>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		public TablixBody()
		{
		}

		public TablixBody(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.TablixColumns = new RdlCollection<TablixColumn>();
			this.TablixRows = new RdlCollection<TablixRow>();
		}
	}
}
