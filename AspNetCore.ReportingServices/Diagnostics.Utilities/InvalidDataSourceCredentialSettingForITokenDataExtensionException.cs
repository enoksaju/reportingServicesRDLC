using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class InvalidDataSourceCredentialSettingForITokenDataExtensionException : ReportCatalogException
	{
		public InvalidDataSourceCredentialSettingForITokenDataExtensionException()
			: base(ErrorCode.rsInvalidDataSourceCredentialSettingForITokenDataExtension, ErrorStrings.rsInvalidDataSourceCredentialSettingForITokenDataExtension, null, null)
		{
		}

		private InvalidDataSourceCredentialSettingForITokenDataExtensionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
