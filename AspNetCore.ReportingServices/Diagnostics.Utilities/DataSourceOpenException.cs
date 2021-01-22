using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class DataSourceOpenException : ReportCatalogException
	{
		public DataSourceOpenException(string datasourceName, Exception innerException)
			: base(ErrorCode.rsErrorOpeningConnection, ErrorStrings.rsErrorOpeningConnection(datasourceName), innerException, null)
		{
		}

		private DataSourceOpenException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
