using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class CatalogItemContentInvalidException : ReportCatalogException
	{
		public CatalogItemContentInvalidException(string itemPath)
			: base(ErrorCode.rsItemContentInvalid, ErrorStrings.rsItemContentInvalid(itemPath), null, null)
		{
		}

		private CatalogItemContentInvalidException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
