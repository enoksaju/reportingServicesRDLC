using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class LogClientTraceEventsException : ReportCatalogException
	{
		public LogClientTraceEventsException(string message, ErrorCode errorCode)
			: this(message, errorCode, null)
		{
		}

		public LogClientTraceEventsException(string message, ErrorCode errorCode, Exception innerException)
			: base(errorCode, message, innerException, null)
		{
		}

		private LogClientTraceEventsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
