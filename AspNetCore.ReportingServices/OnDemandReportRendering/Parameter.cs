using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class Parameter
	{
		private string m_name;

		private ReportVariantProperty m_value;

		private ReportBoolProperty m_omit;

		private ParameterInstance m_instance;

		private ActionDrillthrough m_actionDef;

		private AspNetCore.ReportingServices.ReportIntermediateFormat.ParameterValue m_parameterDef;

		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		public ReportVariantProperty Value
		{
			get
			{
				if (this.m_value == null)
				{
					this.m_value = new ReportVariantProperty(this.m_parameterDef.Value);
				}
				return this.m_value;
			}
		}

		public ReportBoolProperty Omit
		{
			get
			{
				if (this.m_omit == null)
				{
					this.m_omit = new ReportBoolProperty(this.m_parameterDef.Omit);
				}
				return this.m_omit;
			}
		}

		public AspNetCore.ReportingServices.ReportIntermediateFormat.ParameterValue ParameterDef
		{
			get
			{
				return this.m_parameterDef;
			}
		}

		public ActionDrillthrough ActionDef
		{
			get
			{
				return this.m_actionDef;
			}
		}

		public ParameterInstance Instance
		{
			get
			{
				RenderingContext renderingContext = this.m_actionDef.Owner.RenderingContext;
				if (renderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				return this.m_instance;
			}
		}

		public Parameter(ActionDrillthrough actionDef, AspNetCore.ReportingServices.ReportIntermediateFormat.ParameterValue parameterDef)
		{
			this.m_name = parameterDef.Name;
			this.m_actionDef = actionDef;
			this.m_parameterDef = parameterDef;
			this.m_instance = new ParameterInstance(this);
		}

		public Parameter(ActionDrillthrough actionDef, AspNetCore.ReportingServices.ReportProcessing.ParameterValue parameterDef, ActionItemInstance actionInstance, int index)
		{
			this.m_name = parameterDef.Name;
			this.m_value = new ReportVariantProperty(parameterDef.Value);
			this.m_omit = new ReportBoolProperty(parameterDef.Omit);
			this.m_actionDef = actionDef;
			this.m_instance = new ParameterInstance(actionInstance, index);
		}

		public void Update(ActionItemInstance actionInstance, int index)
		{
			if (this.m_instance != null)
			{
				this.m_instance.Update(actionInstance, index);
			}
		}

		public void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		public void ConstructParameterDefinition()
		{
			ParameterInstance instance = this.Instance;
			Global.Tracer.Assert(instance != null);
			if (instance.Value != null)
			{
				this.m_parameterDef.Value = AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression((string)instance.Value);
			}
			else
			{
				this.m_parameterDef.Value = AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			}
			this.m_value = null;
			if (instance.IsOmitAssined)
			{
				this.m_parameterDef.Omit = AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(instance.Omit);
			}
			else
			{
				this.m_parameterDef.Omit = AspNetCore.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			}
			this.m_omit = null;
		}
	}
}
