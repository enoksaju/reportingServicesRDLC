using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class AccessDeniedException : ReportCatalogException
	{
		public AccessDeniedException(string userName, ErrorCode errorCode = ErrorCode.rsAccessDenied)
			: base(errorCode, ErrorStrings.rsAccessDenied(userName), null, null)
		{
		}

		private AccessDeniedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
