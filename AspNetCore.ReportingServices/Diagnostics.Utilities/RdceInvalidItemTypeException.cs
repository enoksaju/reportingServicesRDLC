using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class RdceInvalidItemTypeException : RSException
	{
		public RdceInvalidItemTypeException(string type)
			: base(ErrorCode.rsRdceInvalidItemTypeError, ErrorStrings.rsRdceInvalidItemTypeError(type), null, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, null)
		{
		}

		private RdceInvalidItemTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
