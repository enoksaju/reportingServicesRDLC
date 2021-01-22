using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	public sealed class SecureStoreUnsupportedSharePointVersionException : ReportCatalogException
	{
		public SecureStoreUnsupportedSharePointVersionException()
			: base(ErrorCode.rsSecureStoreUnsupportedSharePointVersion, ErrorStrings.rsSecureStoreUnsupportedSharePointVersion, null, null)
		{
		}

		private SecureStoreUnsupportedSharePointVersionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
