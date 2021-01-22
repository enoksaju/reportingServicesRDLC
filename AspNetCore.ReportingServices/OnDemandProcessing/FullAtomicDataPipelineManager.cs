using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandProcessing
{
	public sealed class FullAtomicDataPipelineManager : AtomicDataPipelineManager
	{
		public FullAtomicDataPipelineManager(OnDemandProcessingContext odpContext, DataSet dataSet)
			: base(odpContext, dataSet)
		{
		}

		protected override RuntimeDataSourceDataProcessing CreateDataSource()
		{
			return new RuntimeDataSourceFullDataProcessing(base.m_dataSet, base.m_odpContext);
		}
	}
}
