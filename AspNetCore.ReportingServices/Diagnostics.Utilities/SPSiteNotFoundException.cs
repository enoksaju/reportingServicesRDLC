using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class SPSiteNotFoundException : ReportCatalogException
	{
		public SPSiteNotFoundException(string siteId)
			: base(ErrorCode.rsSPSiteNotFound, ErrorStrings.rsSPSiteNotFound(siteId), null, null)
		{
		}

		private SPSiteNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
