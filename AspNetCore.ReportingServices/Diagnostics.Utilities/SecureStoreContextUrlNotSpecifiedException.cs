using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	public sealed class SecureStoreContextUrlNotSpecifiedException : ReportCatalogException
	{
		public SecureStoreContextUrlNotSpecifiedException()
			: base(ErrorCode.rsSecureStoreContextUrlNotSpecified, ErrorStrings.rsSecureStoreContextUrlNotSpecified, null, null)
		{
		}

		private SecureStoreContextUrlNotSpecifiedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
