using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class ReportServerHttpRuntimeException : ReportCatalogException
	{
		public ReportServerHttpRuntimeException(Exception innerException, string appDomain, string additionalTraceMessage)
			: base(ErrorCode.rsHttpRuntimeError, ErrorStrings.rsHttpRuntimeError(appDomain), innerException, additionalTraceMessage)
		{
		}

		public ReportServerHttpRuntimeException(string appDomain, string additionalTraceMessage)
			: base(ErrorCode.rsHttpRuntimeError, ErrorStrings.rsHttpRuntimeError(appDomain), null, additionalTraceMessage)
		{
		}

		private ReportServerHttpRuntimeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
