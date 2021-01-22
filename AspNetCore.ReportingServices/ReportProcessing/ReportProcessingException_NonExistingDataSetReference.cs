using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ReportProcessingException_NonExistingDataSetReference : Exception
	{
		public ReportProcessingException_NonExistingDataSetReference(string dataSetName)
			: base(string.Format(CultureInfo.CurrentCulture, RPRes.rsNonExistingDataSetReference(dataSetName)))
		{
		}

		private ReportProcessingException_NonExistingDataSetReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
