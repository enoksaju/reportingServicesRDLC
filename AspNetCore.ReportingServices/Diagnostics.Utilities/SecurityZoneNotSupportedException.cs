using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class SecurityZoneNotSupportedException : ReportCatalogException
	{
		public SecurityZoneNotSupportedException()
			: base(ErrorCode.rsSecurityZoneNotSupported, ErrorStrings.rsSecurityZoneNotSupported, null, null)
		{
		}

		private SecurityZoneNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
