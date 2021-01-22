using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class UnknownDataSetParameterException : ReportCatalogException
	{
		public UnknownDataSetParameterException(string parameterName)
			: base(ErrorCode.rsUnknownDataSetParameter, ErrorStrings.rsUnknownDataSetParameter(parameterName), null, null)
		{
		}

		private UnknownDataSetParameterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
