using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	[Serializable]
	public class RichTextParseException : Exception
	{
		public RichTextParseException(string message)
			: base(message)
		{
		}

		protected RichTextParseException(SerializationInfo serializationInfo, StreamingContext context)
			: base(serializationInfo, context)
		{
		}
	}
}
