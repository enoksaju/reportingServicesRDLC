using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class ReportParameterValueNotSetException : ReportCatalogException
	{
		public ReportParameterValueNotSetException(string parameterName)
			: base(ErrorCode.rsReportParameterValueNotSet, ErrorStrings.rsReportParameterValueNotSet(parameterName), null, null)
		{
		}

		private ReportParameterValueNotSetException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
