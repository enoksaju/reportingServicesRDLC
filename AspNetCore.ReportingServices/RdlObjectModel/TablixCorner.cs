using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class TablixCorner : ReportObject
	{
		public class Definition : DefinitionStore<TablixCorner, Definition.Properties>
		{
			public enum Properties
			{
				TablixCornerRows
			}

			private Definition()
			{
			}
		}

		[XmlElement(typeof(RdlCollection<IList<TablixCornerCell>>))]
		[XmlArrayItem("TablixCornerRow", typeof(TablixCornerRow))]
		public IList<IList<TablixCornerCell>> TablixCornerRows
		{
			get
			{
				return (IList<IList<TablixCornerCell>>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		public TablixCorner()
		{
		}

		public TablixCorner(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.TablixCornerRows = new RdlCollection<IList<TablixCornerCell>>();
		}
	}
}
