using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	public sealed class SecureStoreInvalidLookupContextException : ReportCatalogException
	{
		public SecureStoreInvalidLookupContextException()
			: base(ErrorCode.rsSecureStoreInvalidLookupContext, ErrorStrings.rsSecureStoreInvalidLookupContext, null, null)
		{
		}

		private SecureStoreInvalidLookupContextException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
