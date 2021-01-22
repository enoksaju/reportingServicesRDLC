using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class GetExternalImagesFailureException : ReportCatalogException
	{
		public GetExternalImagesFailureException(string message, ErrorCode errorCode)
			: this(message, errorCode, null)
		{
		}

		public GetExternalImagesFailureException(string message, ErrorCode errorCode, Exception innerException)
			: base(errorCode, message, innerException, null)
		{
		}

		private GetExternalImagesFailureException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
