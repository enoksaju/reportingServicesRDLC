using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	public sealed class ClaimsToWindowsTokenLoginTypeException : RSException
	{
		public ClaimsToWindowsTokenLoginTypeException()
			: base(ErrorCode.rsClaimsToWindowsTokenLoginTypeError, ErrorStrings.rsClaimsToWindowsTokenLoginTypeError, null, RSTrace.IsTraceInitialized ? RSTrace.WebServerTracer : null, null)
		{
		}

		private ClaimsToWindowsTokenLoginTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
