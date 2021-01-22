using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	public sealed class ClaimsToWindowsTokenException : RSException
	{
		public ClaimsToWindowsTokenException(Exception innerException)
			: base(ErrorCode.rsClaimsToWindowsTokenError, ErrorStrings.rsClaimsToWindowsTokenError, innerException, RSTrace.IsTraceInitialized ? RSTrace.WebServerTracer : null, null)
		{
		}

		private ClaimsToWindowsTokenException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
