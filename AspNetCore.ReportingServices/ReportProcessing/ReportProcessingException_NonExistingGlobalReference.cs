using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportProcessingException_NonExistingGlobalReference : Exception
	{
		public ReportProcessingException_NonExistingGlobalReference(string globalName)
			: base(string.Format(CultureInfo.CurrentCulture, RPRes.rsNonExistingGlobalReference(globalName)))
		{
		}

		public ReportProcessingException_NonExistingGlobalReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
