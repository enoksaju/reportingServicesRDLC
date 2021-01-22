using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class ExecuteQueriesFailureException : ReportCatalogException
	{
		public ExecuteQueriesFailureException(string dataSourceName, ErrorCode errorCode, Exception innerException)
			: base(errorCode, ErrorStrings.rsExecuteQueriesFailure(dataSourceName), innerException, null)
		{
		}

		private ExecuteQueriesFailureException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
