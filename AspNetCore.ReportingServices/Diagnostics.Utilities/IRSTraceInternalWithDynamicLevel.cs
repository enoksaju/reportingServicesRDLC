using System.Diagnostics;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	public interface IRSTraceInternalWithDynamicLevel : IRSTraceInternal
	{
		void SetTraceLevel(TraceLevel traceLevel);
	}
}
