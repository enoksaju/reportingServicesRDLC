using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	[Serializable]
	public class InvalidSectionException : Exception
	{
		protected InvalidSectionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public InvalidSectionException()
			: base(RenderRes.rrInvalidSectionError)
		{
		}
	}
}
