using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class CachingNotEnabledException : ReportCatalogException
	{
		public CachingNotEnabledException(string itemPath)
			: base(ErrorCode.rsCachingNotEnabled, ErrorStrings.rsCachingNotEnabled(itemPath), null, null)
		{
		}

		private CachingNotEnabledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
