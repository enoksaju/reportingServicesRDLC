using System;
using System.Runtime.Serialization;

namespace AspNetCore.Reporting
{
	[Serializable]
	internal sealed class LocalProcessingException : ReportViewerException
	{
		internal LocalProcessingException(Exception processingException)
			: base(CommonStrings.LocalProcessingErrors, processingException)
		{
		}

		internal LocalProcessingException(string message, Exception processingException)
			: base(message, processingException)
		{
		}

		internal LocalProcessingException(string message)
			: base(message)
		{
		}

		private LocalProcessingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
