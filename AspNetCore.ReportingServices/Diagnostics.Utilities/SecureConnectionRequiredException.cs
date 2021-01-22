using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class SecureConnectionRequiredException : ReportCatalogException
	{
		public SecureConnectionRequiredException()
			: base(ErrorCode.rsSecureConnectionRequired, ErrorStrings.rsSecureConnectionRequired, null, null)
		{
		}

		private SecureConnectionRequiredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
