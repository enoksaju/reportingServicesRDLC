using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class ElementTypeMismatchException : ReportCatalogException
	{
		public ElementTypeMismatchException(string elementName)
			: base(ErrorCode.rsElementTypeMismatch, ErrorStrings.rsElementTypeMismatch(elementName), null, null)
		{
		}

		public ElementTypeMismatchException(string elementName, Exception innerException)
			: base(ErrorCode.rsElementTypeMismatch, ErrorStrings.rsElementTypeMismatch(elementName), innerException, null)
		{
		}

		private ElementTypeMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
