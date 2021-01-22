using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	public sealed class SecureStoreCannotRetrieveCredentialsException : ReportCatalogException
	{
		public SecureStoreCannotRetrieveCredentialsException(Exception innerException)
			: base(ErrorCode.rsSecureStoreCannotRetrieveCredentials, ErrorStrings.rsSecureStoreCannotRetrieveCredentials(innerException.Message), innerException, null)
		{
		}

		private SecureStoreCannotRetrieveCredentialsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
