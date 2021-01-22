using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	public sealed class FaultedDataSourceException : ReportCatalogException
	{
		public FaultedDataSourceException(ErrorCode errorCode, string errorString)
			: base(errorCode, errorString, null, null)
		{
		}

		private FaultedDataSourceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
