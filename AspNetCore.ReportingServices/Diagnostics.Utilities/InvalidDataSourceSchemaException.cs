using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class InvalidDataSourceSchemaException : ReportCatalogException
	{
		public InvalidDataSourceSchemaException()
			: base(ErrorCode.rsInvalidRSDSSchema, ErrorStrings.rsInvalidRSDSSchema, null, null)
		{
		}

		private InvalidDataSourceSchemaException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
