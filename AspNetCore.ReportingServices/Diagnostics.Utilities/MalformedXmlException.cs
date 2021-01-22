using System;
using System.Runtime.Serialization;
using System.Xml;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class MalformedXmlException : ReportCatalogException
	{
		public MalformedXmlException(XmlException ex)
			: base(ErrorCode.rsMalformedXml, ErrorStrings.rsMalformedXml(ex.Message), ex, null)
		{
		}

		private MalformedXmlException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
