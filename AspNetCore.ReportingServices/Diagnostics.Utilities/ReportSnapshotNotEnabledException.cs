using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class ReportSnapshotNotEnabledException : ReportCatalogException
	{
		public ReportSnapshotNotEnabledException()
			: base(ErrorCode.rsReportSnapshotNotEnabled, ErrorStrings.rsReportSnapshotNotEnabled, null, null)
		{
		}

		private ReportSnapshotNotEnabledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
