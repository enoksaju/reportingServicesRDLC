using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class InvalidDataSourceCountException : ReportCatalogException
	{
		public InvalidDataSourceCountException(string reportPath)
			: base(ErrorCode.rsInvalidDataSourceCount, ErrorStrings.rsInvalidDataSourceCount(reportPath), null, null)
		{
		}

		private InvalidDataSourceCountException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
