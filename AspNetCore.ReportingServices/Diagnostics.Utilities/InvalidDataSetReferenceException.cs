using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class InvalidDataSetReferenceException : ReportCatalogException
	{
		public InvalidDataSetReferenceException(string dataSetName)
			: base(ErrorCode.rsInvalidDataSetReference, ErrorStrings.rsInvalidDataSetReference(dataSetName), null, null)
		{
		}

		private InvalidDataSetReferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
