using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class MixedTasksException : ReportCatalogException
	{
		public MixedTasksException()
			: base(ErrorCode.rsMixedTasks, ErrorStrings.rsMixedTasks, null, null)
		{
		}

		private MixedTasksException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
