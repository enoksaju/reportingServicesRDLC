using System;
using System.Runtime.Serialization;

namespace AspNetCore.Reporting
{
	[Serializable]
	public sealed class LocalProcessingException : ReportViewerException
	{
		public LocalProcessingException(Exception processingException)
			: base(CommonStrings.LocalProcessingErrors, processingException)
		{
		}

		public LocalProcessingException(string message, Exception processingException)
			: base(message, processingException)
		{
		}

		public LocalProcessingException(string message)
			: base(message)
		{
		}

		private LocalProcessingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
