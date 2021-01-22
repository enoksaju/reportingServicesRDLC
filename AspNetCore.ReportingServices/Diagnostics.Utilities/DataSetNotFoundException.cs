using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class DataSetNotFoundException : ReportCatalogException
	{
		public DataSetNotFoundException(string dataSet)
			: base(ErrorCode.rsDataSetNotFound, ErrorStrings.rsDataSetNotFound(dataSet), null, null)
		{
		}

		private DataSetNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
