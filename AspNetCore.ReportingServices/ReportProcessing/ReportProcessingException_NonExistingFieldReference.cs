using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportProcessingException_NonExistingFieldReference : Exception
	{
		public ReportProcessingException_NonExistingFieldReference(string fieldName)
			: base(string.Format(CultureInfo.CurrentCulture, RPRes.rsNonExistingFieldReferenceByName(fieldName)))
		{
		}

		public ReportProcessingException_NonExistingFieldReference()
			: base(string.Format(CultureInfo.CurrentCulture, RPRes.rsNonExistingFieldReference))
		{
		}

		private ReportProcessingException_NonExistingFieldReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
