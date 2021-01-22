using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class OperationNotSupportedSharePointModeException : ReportCatalogException
	{
		public OperationNotSupportedSharePointModeException()
			: base(ErrorCode.rsOperationNotSupportedSharePointMode, ErrorStrings.rsOperationNotSupportedSharePointMode, null, null)
		{
		}

		private OperationNotSupportedSharePointModeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
