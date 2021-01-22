using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class CannotPrepareQueryException : ReportCatalogException
	{
		public CannotPrepareQueryException(Exception innerException, string additionalTraceMessage)
			: base(ErrorCode.rsCannotPrepareQuery, ErrorStrings.rsCannotPrepareQuery, innerException, additionalTraceMessage)
		{
		}

		public CannotPrepareQueryException(Exception innerException)
			: this(innerException, null)
		{
		}

		public CannotPrepareQueryException(string additionalTraceMessage)
			: this(null, additionalTraceMessage)
		{
		}

		private CannotPrepareQueryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
