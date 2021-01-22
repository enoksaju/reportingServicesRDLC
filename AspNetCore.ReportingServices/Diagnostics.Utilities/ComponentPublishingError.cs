using System;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	public sealed class ComponentPublishingError : RSException
	{
		public ComponentPublishingError(Exception innerException)
			: base(ErrorCode.rsComponentPublishingError, ErrorStrings.rsComponentPublishingError, innerException, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, null)
		{
		}
	}
}
