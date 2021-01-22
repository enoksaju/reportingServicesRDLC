using System.Collections.Generic;
using System.Xml.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Internal
{
    public sealed class Connection
	{
		public long? ConnectionOpenTime
		{
			get;
			set;
		}

		[XmlIgnore]
		public bool ConnectionOpenTimeSpecified
		{
			get
			{
				return this.ConnectionOpenTime.HasValue;
			}
		}

		public bool? ConnectionFromPool
		{
			get;
			set;
		}

		[XmlIgnore]
		public bool ConnectionFromPoolSpecified
		{
			get
			{
				return this.ConnectionFromPool.HasValue;
			}
		}

		public ModelMetadata ModelMetadata
		{
			get;
			set;
		}

		public DataSource DataSource
		{
			get;
			set;
		}

		public List<DataSet> DataSets
		{
			get;
			set;
		}

		public Connection()
		{
		}
	}
}
