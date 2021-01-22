using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class InvalidSessionCatalogItemsException : ReportCatalogException
	{
		public InvalidSessionCatalogItemsException(Exception innerException, string errorString)
			: base(ErrorCode.rsInvalidSessionCatalogItems, ErrorStrings.rsInvalidSessionCatalogItems(errorString), innerException, null)
		{
		}

		private InvalidSessionCatalogItemsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
