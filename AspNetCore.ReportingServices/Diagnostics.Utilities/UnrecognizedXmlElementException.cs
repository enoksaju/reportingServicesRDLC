using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class UnrecognizedXmlElementException : ReportCatalogException
	{
		public UnrecognizedXmlElementException(string elementName)
			: base(ErrorCode.rsUnrecognizedXmlElement, ErrorStrings.rsUnrecognizedXmlElement(elementName), null, null)
		{
		}

		private UnrecognizedXmlElementException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
