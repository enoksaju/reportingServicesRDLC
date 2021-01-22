using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class MissingSessionIdException : ReportCatalogException
	{
		public MissingSessionIdException()
			: base(ErrorCode.rsMissingSessionId, ErrorStrings.rsMissingSessionId, null, null)
		{
		}

		private MissingSessionIdException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
