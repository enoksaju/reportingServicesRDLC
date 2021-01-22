using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class ScheduleNotFoundException : ReportCatalogException
	{
		public ScheduleNotFoundException(string idOrData)
			: base(ErrorCode.rsScheduleNotFound, ErrorStrings.rsScheduleNotFound(idOrData), null, null)
		{
		}

		private ScheduleNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
