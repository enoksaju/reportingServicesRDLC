using Microsoft.Cloud.Platform.Utils;
using AspNetCore.ReportingServices.Diagnostics;
using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportProcessing;
using AspNetCore.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using System;

namespace AspNetCore.ReportingServices.OnDemandProcessing
{
	public sealed class SharedDataSetProcessReportParameters : ProcessReportParameters
	{
		private DataSetCore m_dataSetCore;

		public override bool IsReportParameterProcessing
		{
			get
			{
				return false;
			}
		}

		public SharedDataSetProcessReportParameters(DataSetCore dataSetCore, OnDemandProcessingContext odpContext)
			: base(odpContext)
		{
			this.m_dataSetCore = dataSetCore;
			if (odpContext.IsRdlSandboxingEnabled())
			{
				IRdlSandboxConfig rdlSandboxing = odpContext.Configuration.RdlSandboxing;
				base.m_maxStringResultLength = rdlSandboxing.MaxStringResultLength;
			}
		}

		public OnDemandProcessingContext GetOnDemandContext()
		{
			return (OnDemandProcessingContext)base.ProcessingContext;
		}

		public override IParameterDef GetParameterDef(int aParamIndex)
		{
			return this.m_dataSetCore.Query.Parameters[aParamIndex] as DataSetParameterValue;
		}

		protected override void AssertAreSameParameterByName(ParameterInfo paramInfo, IParameterDef paramDef)
		{
			DataSetParameterValue dataSetParameterValue = (DataSetParameterValue)paramDef;
			Global.Tracer.Assert(0 == string.Compare(paramInfo.Name, dataSetParameterValue.UniqueName, StringComparison.OrdinalIgnoreCase), "paramInfo.Name == dataSetParamDef.UniqueName, parameter {0}", paramInfo.Name.MarkAsPrivate());
		}

		protected override string ApplySandboxStringRestriction(string value, string paramName, string propertyName)
		{
			return ProcessReportParameters.ApplySandboxRestriction(ref value, paramName, propertyName, this.GetOnDemandContext(), base.m_maxStringResultLength);
		}

		public override void InitParametersContext(ParameterInfoCollection parameters)
		{
			OnDemandProcessingContext onDemandContext = this.GetOnDemandContext();
			Global.Tracer.Assert(onDemandContext.ReportObjectModel != null && onDemandContext.ReportRuntime != null);
			if (onDemandContext.ReportRuntime.ReportExprHost != null)
			{
				this.m_dataSetCore.SetExprHost(onDemandContext.ReportRuntime.ReportExprHost, onDemandContext.ReportObjectModel);
			}
		}

		public override void Cleanup()
		{
		}

		public override void AddToRuntime(ParameterInfo aParamInfo)
		{
			ParameterImpl parameter = new ParameterImpl(aParamInfo);
			this.GetOnDemandContext().ReportObjectModel.ParametersImpl.Add(aParamInfo.Name, parameter);
		}

		public override void SetupExprHost(IParameterDef aParamDef)
		{
		}

		public override object EvaluateDefaultValueExpr(IParameterDef aParamDef, int aIndex)
		{
			DataSetParameterValue dataSetParameterValue = aParamDef as DataSetParameterValue;
			return dataSetParameterValue.EvaluateQueryParameterValue(this.GetOnDemandContext(), this.m_dataSetCore.ExprHost);
		}

		public override object EvaluateValidValueExpr(IParameterDef aParamDef, int aIndex)
		{
			throw new NotSupportedException();
		}

		public override object EvaluateValidValueLabelExpr(IParameterDef aParamDef, int aIndex)
		{
			throw new NotSupportedException();
		}

		public override string EvaluatePromptExpr(ParameterInfo aParamInfo, IParameterDef aParamDef)
		{
			throw new NotSupportedException();
		}

		public override bool NeedPrompt(IParameterDataSource paramDS)
		{
			return false;
		}

		public override void ThrowExceptionForQueryBackedParameter(ReportProcessingException_FieldError aError, string aParamName, int aDataSourceIndex, int aDataSetIndex, int aFieldIndex, string propertyName)
		{
			throw new NotSupportedException();
		}

		public override ReportParameterDataSetCache ProcessReportParameterDataSet(ParameterInfo aParam, IParameterDef aParamDef, IParameterDataSource paramDS, bool aRetrieveValidValues, bool aRetrievalDefaultValues)
		{
			throw new NotSupportedException();
		}
	}
}
