using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class DataExtensionNotFoundException : ReportCatalogException
	{
		public DataExtensionNotFoundException(string extension)
			: base(ErrorCode.rsDataExtensionNotFound, ErrorStrings.rsDataExtensionNotFound(extension), null, null)
		{
		}

		private DataExtensionNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
