using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class MissingRequiredPropertyForItemTypeException : ReportCatalogException
	{
		public MissingRequiredPropertyForItemTypeException(string propertyName)
			: base(ErrorCode.rsMissingRequiredPropertyForItemType, ErrorStrings.rsMissingRequiredPropertyForItemType(propertyName), null, null)
		{
		}

		private MissingRequiredPropertyForItemTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
