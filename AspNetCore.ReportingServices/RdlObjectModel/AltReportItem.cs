using System;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public class AltReportItem : ReportObject
	{
		public class Definition : DefinitionStore<AltReportItem, Definition.Properties>
		{
			public enum Properties
			{
				ReportItem
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
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.PropertyStore.SetObject(0, value);
			}
		}

		public AltReportItem()
		{
		}

		public AltReportItem(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
