using System;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class RestrictedItemException : ReportCatalogException
	{
		public RestrictedItemException(string itemPath)
			: base(ErrorCode.rsItemNotFound, ErrorStrings.rsRestrictedItem(itemPath), null, null)
		{
		}
	}
}
