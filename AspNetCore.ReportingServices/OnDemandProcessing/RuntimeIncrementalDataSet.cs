using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportProcessing;
using System;

namespace AspNetCore.ReportingServices.OnDemandProcessing
{
	public abstract class RuntimeIncrementalDataSet : RuntimeDataSet
	{
		protected override bool ShouldCancelCommandDuringCleanup
		{
			get
			{
				return true;
			}
		}

		protected RuntimeIncrementalDataSet(AspNetCore.ReportingServices.ReportIntermediateFormat.DataSource dataSource, AspNetCore.ReportingServices.ReportIntermediateFormat.DataSet dataSet, DataSetInstance dataSetInstance, OnDemandProcessingContext odpContext)
			: base(dataSource, dataSet, dataSetInstance, odpContext, true)
		{
		}

		public void Initialize(ExecutedQuery existingQuery)
		{
			try
			{
				this.InitializeDataSet();
				if (base.m_dataSet.IsReferenceToSharedDataSet)
				{
					Global.Tracer.Assert(false, "Shared data sets cannot be used with a RuntimeIncrementalDataSet");
				}
				else if (existingQuery != null)
				{
					base.InitializeAndRunFromExistingQuery(existingQuery);
				}
				else
				{
					base.InitializeAndRunLiveQuery();
				}
			}
			catch (Exception)
			{
				this.CleanupForException();
				this.FinalCleanup();
				throw;
			}
		}

		public void Teardown()
		{
			try
			{
				this.CleanupProcess();
				this.TeardownDataSet();
			}
			catch (Exception)
			{
				this.CleanupForException();
				throw;
			}
			finally
			{
				this.FinalCleanup();
			}
		}

		public void RecordSkippedRowCount(long rowCount)
		{
			base.m_executionMetrics.AddSkippedRowCount(rowCount);
		}
	}
}
