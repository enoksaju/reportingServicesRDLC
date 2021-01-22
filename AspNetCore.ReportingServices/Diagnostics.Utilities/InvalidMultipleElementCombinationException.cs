using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class InvalidMultipleElementCombinationException : ReportCatalogException
	{
		public InvalidMultipleElementCombinationException(string elementName1, string elementName2, string elementName3)
			: base(ErrorCode.rsInvalidMultipleElementCombination, ErrorStrings.rsInvalidMultipleElementCombination(elementName1, elementName2, elementName3), null, null)
		{
		}

		private InvalidMultipleElementCombinationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
