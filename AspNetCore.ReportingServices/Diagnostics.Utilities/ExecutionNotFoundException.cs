using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class ExecutionNotFoundException : ReportCatalogException
	{
		public ExecutionNotFoundException(string ExecutionID)
			: base(ErrorCode.rsExecutionNotFound, ErrorStrings.rsExecutionNotFound(ExecutionID), null, null)
		{
		}

		private ExecutionNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
