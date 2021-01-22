using AspNetCore.ReportingServices.OnDemandProcessing;
using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public interface IRowConsumer
	{
		string ReportDataSetName
		{
			get;
		}

		void SetProcessingDataReader(IProcessingDataReader dataReader);

		void NextRow(AspNetCore.ReportingServices.ReportIntermediateFormat.RecordRow row);
	}
}
