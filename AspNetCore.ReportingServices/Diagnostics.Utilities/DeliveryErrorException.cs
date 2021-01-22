using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class DeliveryErrorException : ReportCatalogException
	{
		public DeliveryErrorException(Exception innerException)
			: base(ErrorCode.rsDeliveryError, ErrorStrings.rsDeliverError, innerException, null)
		{
		}

		private DeliveryErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
