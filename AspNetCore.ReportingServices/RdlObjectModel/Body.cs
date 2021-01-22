using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class Body : ReportElement
	{
		public new class Definition : DefinitionStore<Report, Definition.Properties>
		{
			public enum Properties
			{
				Style,
				ReportItems,
				Height,
				PropertyCount
			}

			private Definition()
			{
			}
		}

		[XmlElement(typeof(RdlCollection<ReportItem>))]
		public IList<ReportItem> ReportItems
		{
			get
			{
				return (IList<ReportItem>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}
        [DefaultValue(0.0)]
		public ReportSize Height
		{
			get
			{
				return base.PropertyStore.GetSize(2);
			}
			set
			{
				base.PropertyStore.SetSize(2, value);
			}
		}

		public Body()
		{
		}

		public Body(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
			this.ReportItems = new RdlCollection<ReportItem>();
			this.Height = Constants.DefaultZeroSize;
		}
	}
}
