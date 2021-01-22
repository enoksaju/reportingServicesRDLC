using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class OperationNotSupportedNativeModeException : ReportCatalogException
	{
		public OperationNotSupportedNativeModeException()
			: base(ErrorCode.rsOperationNotSupportedNativeMode, ErrorStrings.rsOperationNotSupportedNativeMode, null, null)
		{
		}

		private OperationNotSupportedNativeModeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
