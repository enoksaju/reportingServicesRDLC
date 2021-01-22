using AspNetCore.ReportingServices.Common;
using AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing;
using AspNetCore.ReportingServices.OnDemandReportRendering;
using AspNetCore.ReportingServices.RdlExpressions;
using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportProcessing;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AspNetCore.ReportingServices.OnDemandProcessing
{
	public abstract class OnDemandStateManager
	{
		protected readonly OnDemandProcessingContext m_odpContext;

		private List<IDisposable> m_sequentialDataReadersAndIdcDataManagers;

		private BaseIdcDataManager[] m_idcDataManagers;

		public abstract IReportScopeInstance LastROMInstance
		{
			get;
		}

		public abstract IRIFReportScope LastTablixProcessingReportScope
		{
			get;
			set;
		}

		public abstract IInstancePath LastRIFObject
		{
			get;
			set;
		}

		public abstract QueryRestartInfo QueryRestartInfo
		{
			get;
		}

		public abstract ExecutedQueryCache ExecutedQueryCache
		{
			get;
		}

		public OnDemandStateManager(OnDemandProcessingContext odpContext)
		{
			this.m_odpContext = odpContext;
		}

		public abstract ExecutedQueryCache SetupExecutedQueryCache();

		public abstract void ResetOnDemandState();

		public abstract int RecursiveLevel(string scopeName);

		public abstract bool InScope(string scopeName);

		public abstract Dictionary<string, object> GetCurrentSpecialGroupingValues();

		public abstract void RestoreContext(IInstancePath originalObject);

		public abstract void SetupContext(IInstancePath rifObject, IReportScopeInstance romInstance);

		public abstract void SetupContext(IInstancePath rifObject, IReportScopeInstance romInstance, int moveNextInstanceIndex);

		public abstract void BindNextMemberInstance(IInstancePath rifObject, IReportScopeInstance romInstance, int moveNextInstanceIndex);

		public abstract bool CalculateAggregate(string aggregateName);

		public abstract bool CalculateLookup(LookupInfo lookup);

		public abstract bool PrepareFieldsCollectionForDirectFields();

		public abstract void EvaluateScopedFieldReference(string scopeName, int fieldIndex, ref AspNetCore.ReportingServices.RdlExpressions.VariantResult result);

		public abstract IRecordRowReader CreateSequentialDataReader(AspNetCore.ReportingServices.ReportIntermediateFormat.DataSet dataSet, out AspNetCore.ReportingServices.ReportIntermediateFormat.DataSetInstance dataSetInstance);

		public abstract bool ShouldStopPipelineAdvance(bool rowAccepted);

		public abstract void CreatedScopeInstance(IRIFReportDataScope scope);

		public virtual void FreeResources()
		{
			this.ShutdownSequentialReadersAndIdcDataManagers();
		}

		protected OnDemandProcessingContext GetOdpWorkerContextForTablixProcessing()
		{
			if (this.m_odpContext.IsPageHeaderFooter)
			{
				return this.m_odpContext.ParentContext;
			}
			return this.m_odpContext;
		}

		protected void ShutdownSequentialReadersAndIdcDataManagers()
		{
			if (this.m_sequentialDataReadersAndIdcDataManagers != null)
			{
				for (int i = 0; i < this.m_sequentialDataReadersAndIdcDataManagers.Count; i++)
				{
					try
					{
						this.m_sequentialDataReadersAndIdcDataManagers[i].Dispose();
					}
					catch (ReportProcessingException ex)
					{
						if (ex.InnerException != null && AsynchronousExceptionDetection.IsStoppingException(ex.InnerException))
						{
							throw;
						}
						Global.Tracer.Trace(TraceLevel.Error, "Error cleaning up request: {0}", ex);
					}
				}
				this.m_sequentialDataReadersAndIdcDataManagers = null;
				this.m_idcDataManagers = null;
			}
		}

		protected void RegisterDisposableDataReaderOrIdcDataManager(IDisposable dataReaderOrIdcDataManager)
		{
			if (this.m_sequentialDataReadersAndIdcDataManagers == null)
			{
				this.m_sequentialDataReadersAndIdcDataManagers = new List<IDisposable>();
			}
			this.m_sequentialDataReadersAndIdcDataManagers.Add(dataReaderOrIdcDataManager);
		}

		public abstract bool CheckForPrematureServerAggregate(string aggregateName);

		public abstract bool ProcessOneRow(IRIFReportDataScope scope);

		protected BaseIdcDataManager GetOrCreateIdcDataManager(IRIFReportDataScope scope)
		{
			BaseIdcDataManager baseIdcDataManager = default(BaseIdcDataManager);
			if (!this.TryGetIdcDataManager(scope, out baseIdcDataManager))
			{
				baseIdcDataManager = ((!scope.IsDataIntersectionScope) ? ((BaseIdcDataManager)new IdcDataManager(this.m_odpContext, scope)) : ((BaseIdcDataManager)new CellIdcDataManager(this.m_odpContext, scope)));
				this.RegisterDisposableDataReaderOrIdcDataManager(baseIdcDataManager);
				this.AddIdcDataManager(scope, baseIdcDataManager);
			}
			return baseIdcDataManager;
		}

		private bool TryGetIdcDataManager(IRIFReportDataScope scope, out BaseIdcDataManager idcDataManager)
		{
			return this.TryGetIdcDataManager(scope.DataScopeInfo.DataPipelineID, out idcDataManager);
		}

		protected bool TryGetNonStructuralIdcDataManager(AspNetCore.ReportingServices.ReportIntermediateFormat.DataSet targetDataSet, out NonStructuralIdcDataManager nsIdcDataManager)
		{
			BaseIdcDataManager baseIdcDataManager = default(BaseIdcDataManager);
			if (this.TryGetIdcDataManager(targetDataSet.IndexInCollection, out baseIdcDataManager))
			{
				nsIdcDataManager = (NonStructuralIdcDataManager)baseIdcDataManager;
				return true;
			}
			nsIdcDataManager = null;
			return false;
		}

		private bool TryGetIdcDataManager(int dataPipelineId, out BaseIdcDataManager idcDataManager)
		{
			if (this.m_idcDataManagers == null)
			{
				idcDataManager = null;
				return false;
			}
			idcDataManager = this.m_idcDataManagers[dataPipelineId];
			return idcDataManager != null;
		}

		protected void AddNonStructuralIdcDataManager(AspNetCore.ReportingServices.ReportIntermediateFormat.DataSet targetDataSet, NonStructuralIdcDataManager idcDataManager)
		{
			this.AddIdcDataManager(targetDataSet.IndexInCollection, idcDataManager);
		}

		private void AddIdcDataManager(IRIFReportDataScope scope, BaseIdcDataManager idcDataManager)
		{
			this.AddIdcDataManager(scope.DataScopeInfo.DataPipelineID, idcDataManager);
		}

		private void AddIdcDataManager(int dataPipelineId, BaseIdcDataManager idcDataManager)
		{
			if (this.m_idcDataManagers == null)
			{
				this.m_idcDataManagers = new BaseIdcDataManager[this.m_odpContext.ReportDefinition.DataPipelineCount];
			}
			this.m_idcDataManagers[dataPipelineId] = idcDataManager;
		}

		public BaseIdcDataManager GetIdcDataManager(IRIFReportDataScope scope)
		{
			BaseIdcDataManager result = default(BaseIdcDataManager);
			if (!this.TryGetIdcDataManager(scope, out result))
			{
				Global.Tracer.Assert(false, "Missing expected IDCDataManager.");
			}
			return result;
		}
	}
}
