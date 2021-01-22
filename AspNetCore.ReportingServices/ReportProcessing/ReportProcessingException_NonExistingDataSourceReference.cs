using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportProcessingException_NonExistingDataSourceReference : Exception
	{
		public ReportProcessingException_NonExistingDataSourceReference(string dataSourceName)
			: base(string.Format(CultureInfo.CurrentCulture, RPRes.rsNonExistingDataSourceReference(dataSourceName)))
		{
		}

		private ReportProcessingException_NonExistingDataSourceReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
