using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class ReportNotReadyException : ReportCatalogException
	{
		public ReportNotReadyException()
			: base(ErrorCode.rsReportNotReady, ErrorStrings.rsReportNotReady, null, null)
		{
		}

		private ReportNotReadyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
