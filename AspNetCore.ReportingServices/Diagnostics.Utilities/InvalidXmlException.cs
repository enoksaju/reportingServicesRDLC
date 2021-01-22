using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class InvalidXmlException : ReportCatalogException
	{
		public InvalidXmlException()
			: base(ErrorCode.rsInvalidXml, ErrorStrings.rsInvalidXml, null, null)
		{
		}

		private InvalidXmlException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
