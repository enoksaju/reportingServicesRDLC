using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class SchedulerNotRespondingPreventsPinningException : ReportCatalogException
	{
		public SchedulerNotRespondingPreventsPinningException()
			: base(ErrorCode.rsSchedulerNotResponding, ErrorStrings.rsSchedulerNotRespondingPreventsPinning, null, null)
		{
		}

		private SchedulerNotRespondingPreventsPinningException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
