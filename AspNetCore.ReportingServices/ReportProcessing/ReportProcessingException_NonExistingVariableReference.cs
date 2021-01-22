using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportProcessingException_NonExistingVariableReference : Exception
	{
		public ReportProcessingException_NonExistingVariableReference(string varName)
			: base(string.Format(CultureInfo.CurrentCulture, RPRes.rsNonExistingVariableReference(varName)))
		{
		}

		private ReportProcessingException_NonExistingVariableReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
