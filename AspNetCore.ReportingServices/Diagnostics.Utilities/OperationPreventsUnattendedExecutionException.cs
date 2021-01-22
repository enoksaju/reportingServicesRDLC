using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class OperationPreventsUnattendedExecutionException : ReportCatalogException
	{
		public OperationPreventsUnattendedExecutionException()
			: base(ErrorCode.rsOperationPreventsUnattendedExecution, ErrorStrings.rsOperationPreventsUnattendedExecution, null, null)
		{
		}

		private OperationPreventsUnattendedExecutionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
