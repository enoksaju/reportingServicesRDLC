using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	public sealed class SecureStoreAmbiguousCredentialFieldsException : ReportCatalogException
	{
		public SecureStoreAmbiguousCredentialFieldsException(string appId)
			: base(ErrorCode.rsSecureStoreAmbiguousCredentialFields, ErrorStrings.rsSecureStoreAmbiguousCredentialFields(appId), null, null)
		{
		}

		private SecureStoreAmbiguousCredentialFieldsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
