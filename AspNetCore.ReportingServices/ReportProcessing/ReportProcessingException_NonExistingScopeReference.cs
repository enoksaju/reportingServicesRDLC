using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportProcessingException_NonExistingScopeReference : Exception
	{
		public ReportProcessingException_NonExistingScopeReference(string scopeName)
			: base(string.Format(CultureInfo.CurrentCulture, RPRes.rsNonExistingScopeReference(scopeName)))
		{
		}

		private ReportProcessingException_NonExistingScopeReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
