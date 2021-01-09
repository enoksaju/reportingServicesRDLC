using System;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	internal sealed class DataSetPublishingException : ReportProcessingException
	{
		public DataSetPublishingException(ProcessingMessageList messages)
			: base(messages)
		{
		}
	}
}
