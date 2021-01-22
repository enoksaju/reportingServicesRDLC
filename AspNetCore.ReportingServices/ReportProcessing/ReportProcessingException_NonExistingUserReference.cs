using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportProcessingException_NonExistingUserReference : Exception
	{
		public ReportProcessingException_NonExistingUserReference(string propName)
			: base(string.Format(CultureInfo.CurrentCulture, RPRes.rsNonExistingUserReference(propName)))
		{
		}

		public ReportProcessingException_NonExistingUserReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
