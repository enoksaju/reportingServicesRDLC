using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class DataSetPublishingException : ReportProcessingException
	{
		public DataSetPublishingException(ProcessingMessageList messages)
			: base(messages)
		{
		}
	}
}
