using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class ReportServerStorageSingleRefreshConnectionExpectedException : ReportCatalogException
	{
		public ReportServerStorageSingleRefreshConnectionExpectedException(long modelId, int actualCount)
			: base(ErrorCode.rsReportServerStorageSingleRefreshConnectionExpected, ErrorStrings.rsReportServerStorageSingleRefreshConnectionExpected(modelId.ToString(), actualCount.ToString()), null, null)
		{
		}

		private ReportServerStorageSingleRefreshConnectionExpectedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
