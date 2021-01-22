using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class SubreportFromSnapshotException : ReportCatalogException
	{
		public SubreportFromSnapshotException()
			: base(ErrorCode.rsSubreportFromSnapshot, ErrorStrings.rsSubreportFromSnapshot, null, null)
		{
		}

		private SubreportFromSnapshotException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
