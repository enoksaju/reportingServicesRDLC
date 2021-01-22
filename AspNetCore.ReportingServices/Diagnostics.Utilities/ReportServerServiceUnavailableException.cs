using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class ReportServerServiceUnavailableException : ReportCatalogException
	{
		public ReportServerServiceUnavailableException(string serviceName)
			: base(ErrorCode.rsReportServerServiceUnavailable, ErrorStrings.rsReportServerServiceUnavailable(serviceName), null, null)
		{
		}

		private ReportServerServiceUnavailableException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
