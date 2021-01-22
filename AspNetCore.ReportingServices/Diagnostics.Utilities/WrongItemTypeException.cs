using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class WrongItemTypeException : ReportCatalogException
	{
		public WrongItemTypeException(string itemPathOrType)
			: base(ErrorCode.rsWrongItemType, ErrorStrings.rsWrongItemType(itemPathOrType), null, null)
		{
		}

		private WrongItemTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
