using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportProcessingException_MissingAggregateDependency : Exception
	{
		public ReportProcessingException_MissingAggregateDependency()
		{
		}

		private ReportProcessingException_MissingAggregateDependency(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
