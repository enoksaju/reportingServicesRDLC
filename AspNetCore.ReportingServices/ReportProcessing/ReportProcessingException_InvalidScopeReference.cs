using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportProcessingException_InvalidScopeReference : Exception
	{
		public ReportProcessingException_InvalidScopeReference(string scopeName)
			: base(string.Format(CultureInfo.CurrentCulture, RPRes.rsInvalidRuntimeScopeReference(scopeName)))
		{
		}

		private ReportProcessingException_InvalidScopeReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
