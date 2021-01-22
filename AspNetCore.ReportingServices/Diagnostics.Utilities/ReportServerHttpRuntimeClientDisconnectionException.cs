using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class ReportServerHttpRuntimeClientDisconnectionException : RSException
	{
		public ReportServerHttpRuntimeClientDisconnectionException(Exception innerException, string appDomain, int hr)
			: base(ErrorCode.rsHttpRuntimeClientDisconnectionError, ErrorStrings.rsHttpRuntimeClientDisconnectionError(appDomain, hr.ToString("X", CultureInfo.CurrentCulture)), innerException, RSTrace.IsTraceInitialized ? RSTrace.HttpRuntimeTracer : null, null, TraceLevel.Verbose)
		{
		}

		private ReportServerHttpRuntimeClientDisconnectionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
