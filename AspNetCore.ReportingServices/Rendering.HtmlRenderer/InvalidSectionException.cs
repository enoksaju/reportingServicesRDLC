using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Rendering.HtmlRenderer
{
	[Serializable]
	internal class InvalidSectionException : Exception
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
