using AspNetCore.ReportingServices.ReportIntermediateFormat;
using System;

namespace AspNetCore.ReportingServices.OnDemandProcessing
{
	public sealed class RuntimeLiveDataReaderDataSet : RuntimeIncrementalDataSet
	{
		public RuntimeLiveDataReaderDataSet(DataSource dataSource, DataSet dataSet, DataSetInstance dataSetInstance, OnDemandProcessingContext odpContext)
			: base(dataSource, dataSet, dataSetInstance, odpContext)
		{
		}

		public RecordRow ReadNextRow()
		{
			try
			{
				int num = default(int);
				return base.ReadOneRow(out num);
			}
			catch (Exception)
			{
				this.CleanupForException();
				this.FinalCleanup();
				throw;
			}
		}

		protected override void InitializeBeforeProcessingRows(bool aReaderExtensionsSupported)
		{
		}

		protected override void ProcessExtendedPropertyMappings()
		{
		}
	}
}
