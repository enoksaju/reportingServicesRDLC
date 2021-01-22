using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportProcessingException_NonExistingLookupReference : Exception
	{
		public ReportProcessingException_NonExistingLookupReference()
			: base(string.Format(CultureInfo.CurrentCulture, RPRes.rsNonExistingLookupReference))
		{
		}

		public ReportProcessingException_NonExistingLookupReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
