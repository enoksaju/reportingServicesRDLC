using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class ReportServerHttpRuntimeInternalException : ReportCatalogException
	{
		public ReportServerHttpRuntimeInternalException(Exception innerException, string appDomain, string additionalTraceMessage)
			: base(ErrorCode.rsHttpRuntimeInternalError, ErrorStrings.rsHttpRuntimeInternalError(appDomain), innerException, additionalTraceMessage)
		{
		}

		public ReportServerHttpRuntimeInternalException(string appDomain, string additionalTraceMessage)
			: base(ErrorCode.rsHttpRuntimeInternalError, ErrorStrings.rsHttpRuntimeInternalError(appDomain), null, additionalTraceMessage)
		{
		}

		private ReportServerHttpRuntimeInternalException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
