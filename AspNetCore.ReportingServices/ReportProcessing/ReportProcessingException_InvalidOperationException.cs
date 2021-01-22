using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportProcessingException_InvalidOperationException : Exception
	{
		public ReportProcessingException_InvalidOperationException()
		{
		}

		private ReportProcessingException_InvalidOperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
