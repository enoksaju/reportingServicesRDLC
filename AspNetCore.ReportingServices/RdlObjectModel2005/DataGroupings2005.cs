using AspNetCore.ReportingServices.RdlObjectModel;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.RdlObjectModel2005
{
	public class DataGroupings2005 : DataHierarchy
	{
		[XmlElement(typeof(RdlCollection<DataMember>))]
		[XmlArrayItem("DataGrouping", typeof(DataGrouping2005))]
		public IList<DataMember> DataGroupings
		{
			get
			{
				return base.DataMembers;
			}
			set
			{
				base.DataMembers = value;
			}
		}

		public DataGroupings2005()
		{
		}

		public DataGroupings2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}
	}
}
