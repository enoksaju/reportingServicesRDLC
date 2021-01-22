using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportProcessingException_NoRowsFieldAccess : Exception
	{
		public ReportProcessingException_NoRowsFieldAccess()
			: base(string.Format(CultureInfo.CurrentCulture, RPRes.rsNoRowsFieldAccess))
		{
		}

		private ReportProcessingException_NoRowsFieldAccess(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
