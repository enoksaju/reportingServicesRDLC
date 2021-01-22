using System;
using System.Runtime.Serialization;

namespace AspNetCore.Reporting
{
	[Serializable]
	public sealed class HttpHandlerInputException : ReportViewerException
	{
		public HttpHandlerInputException(Exception e)
			: base(new ArgumentException().Message, e)
		{
		}

		public HttpHandlerInputException(string message)
			: base(message)
		{
		}

		private HttpHandlerInputException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
