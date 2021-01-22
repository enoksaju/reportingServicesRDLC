using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class ReportParameterTypeMismatchException : ReportCatalogException
	{
		public ReportParameterTypeMismatchException(string parameterName)
			: base(ErrorCode.rsReportParameterTypeMismatch, ErrorStrings.rsReportParameterTypeMismatch(parameterName), null, null)
		{
		}

		private ReportParameterTypeMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
