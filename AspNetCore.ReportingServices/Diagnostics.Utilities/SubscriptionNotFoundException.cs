using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class SubscriptionNotFoundException : ReportCatalogException
	{
		public SubscriptionNotFoundException(string idOrData)
			: base(ErrorCode.rsSubscriptionNotFound, ErrorStrings.rsSubscriptionNotFound(idOrData), null, null)
		{
		}

		private SubscriptionNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
