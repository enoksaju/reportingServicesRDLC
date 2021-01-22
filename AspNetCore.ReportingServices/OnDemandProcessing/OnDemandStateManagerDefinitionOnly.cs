using AspNetCore.ReportingServices.OnDemandReportRendering;
using AspNetCore.ReportingServices.RdlExpressions;
using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportProcessing;
using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandProcessing
{
	public sealed class OnDemandStateManagerDefinitionOnly : OnDemandStateManager
	{
		public override IReportScopeInstance LastROMInstance
		{
			get
			{
				this.FireAssert("LastROMInstance");
				return null;
			}
		}

		public override IRIFReportScope LastTablixProcessingReportScope
		{
			get
			{
				this.FireAssert("LastTablixProcessingReportScope");
				return null;
			}
			set
			{
				this.FireAssert("LastTablixProcessingReportScope");
			}
		}

		public override IInstancePath LastRIFObject
		{
			get
			{
				this.FireAssert("LastRIFObject");
				return null;
			}
			set
			{
				this.FireAssert("LastRIFObject");
			}
		}

		public override QueryRestartInfo QueryRestartInfo
		{
			get
			{
				return null;
			}
		}

		public override ExecutedQueryCache ExecutedQueryCache
		{
			get
			{
				return null;
			}
		}

		public OnDemandStateManagerDefinitionOnly(OnDemandProcessingContext odpContext)
			: base(odpContext)
		{
		}

		public override ExecutedQueryCache SetupExecutedQueryCache()
		{
			return this.ExecutedQueryCache;
		}

		public override void ResetOnDemandState()
		{
		}

		public override int RecursiveLevel(string scopeName)
		{
			this.FireAssert("RecursiveLevel");
			return -1;
		}

		public override bool InScope(string scopeName)
		{
			this.FireAssert("InScope");
			return false;
		}

		public override Dictionary<string, object> GetCurrentSpecialGroupingValues()
		{
			this.FireAssert("GetCurrentSpecialGroupingValues");
			return null;
		}

		public override void RestoreContext(IInstancePath originalObject)
		{
			this.FireAssert("RestoreContext");
		}

		public override void SetupContext(IInstancePath rifObject, IReportScopeInstance romInstance)
		{
			this.FireAssert("SetupContext");
		}

		public override void SetupContext(IInstancePath rifObject, IReportScopeInstance romInstance, int moveNextInstanceIndex)
		{
			this.FireAssert("SetupContext");
		}

		public override bool CalculateAggregate(string aggregateName)
		{
			this.FireAssert("CalculateAggregate");
			return false;
		}

		public override bool CalculateLookup(LookupInfo lookup)
		{
			this.FireAssert("CalculateLookup");
			return false;
		}

		public override bool PrepareFieldsCollectionForDirectFields()
		{
			this.FireAssert("PrepareFieldsCollectionForDirectFields");
			return false;
		}

		public override void EvaluateScopedFieldReference(string scopeName, int fieldIndex, ref AspNetCore.ReportingServices.RdlExpressions.VariantResult result)
		{
			this.FireAssert("EvaluateScopedFieldReference");
		}

		private void FireAssert(string methodOrPropertyName)
		{
			Global.Tracer.Assert(false, methodOrPropertyName + " should not be called in Definition-only mode.");
		}

		public override IRecordRowReader CreateSequentialDataReader(AspNetCore.ReportingServices.ReportIntermediateFormat.DataSet dataSet, out AspNetCore.ReportingServices.ReportIntermediateFormat.DataSetInstance dataSetInstance)
		{
			this.FireAssert("CreateSequentialDataReader");
			throw new InvalidOperationException("This method is not valid for this StateManager type.");
		}

		public override void BindNextMemberInstance(IInstancePath rifObject, IReportScopeInstance romInstance, int moveNextInstanceIndex)
		{
			this.FireAssert("BindNextMemberInstance");
			throw new InvalidOperationException("This method is not valid for this StateManager type.");
		}

		public override bool ShouldStopPipelineAdvance(bool rowAccepted)
		{
			this.FireAssert("ShouldStopPipelineAdvance");
			throw new InvalidOperationException("This method is not valid for this StateManager type.");
		}

		public override void CreatedScopeInstance(IRIFReportDataScope scope)
		{
			this.FireAssert("CreateScopeInstance");
			throw new InvalidOperationException("This method is not valid for this StateManager type.");
		}

		public override bool ProcessOneRow(IRIFReportDataScope scope)
		{
			this.FireAssert("ProcessOneRow");
			throw new InvalidOperationException("This method is not valid for this StateManager type.");
		}

		public override bool CheckForPrematureServerAggregate(string aggregateName)
		{
			this.FireAssert("CheckForPrematureServerAggregate");
			throw new InvalidOperationException("This method is not valid for this StateManager type.");
		}
	}
}
