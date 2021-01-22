using System;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.Diagnostics.Utilities
{
	[Serializable]
	public sealed class ParameterTypeMismatchException : ReportCatalogException
	{
		public ParameterTypeMismatchException(string parameterName)
			: base(ErrorCode.rsParameterTypeMismatch, ErrorStrings.rsParameterTypeMismatch(parameterName), null, null)
		{
		}

		public ParameterTypeMismatchException(string parameterName, Exception innerException)
			: base(ErrorCode.rsParameterTypeMismatch, ErrorStrings.rsParameterTypeMismatch(parameterName), innerException, null)
		{
		}

		private ParameterTypeMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
