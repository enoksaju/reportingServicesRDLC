using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class RdceInvalidCacheOptionException : RSException
	{
		public RdceInvalidCacheOptionException()
			: base(ErrorCode.rsRdceInvalidCacheOptionError, ErrorStrings.rsRdceInvalidCacheOptionError, null, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, null)
		{
		}

		private RdceInvalidCacheOptionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
