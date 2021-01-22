using AspNetCore.ReportingServices.Diagnostics.Utilities;
using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class DataSetExecutionException : RSException
	{
		public DataSetExecutionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public DataSetExecutionException(ErrorCode code)
			: base(code, RPRes.Keys.GetString(code.ToString()), null, Global.Tracer, null)
		{
		}

		public DataSetExecutionException(string dataSetName, Exception innerException)
			: base(ErrorCode.rsDataSetExecutionError, string.Format(CultureInfo.CurrentCulture, ErrorStrings.rsDataSetExecutionError(dataSetName)), innerException, Global.Tracer, null)
		{
		}
	}
}
