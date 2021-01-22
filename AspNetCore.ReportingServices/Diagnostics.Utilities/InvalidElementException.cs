using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class InvalidElementException : ReportCatalogException
	{
		public InvalidElementException(string elementName, Exception innerException)
			: base(ErrorCode.rsInvalidElement, ErrorStrings.rsInvalidElement(elementName), innerException, null)
		{
		}

		public InvalidElementException(string elementName)
			: this(elementName, null)
		{
		}

		private InvalidElementException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
