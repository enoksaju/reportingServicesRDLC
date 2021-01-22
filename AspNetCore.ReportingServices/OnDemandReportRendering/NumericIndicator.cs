using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class NumericIndicator : GaugePanelItem
	{
		public AspNetCore.ReportingServices.ReportIntermediateFormat.NumericIndicator NumericIndicatorDef
		{
			get
			{
				return (AspNetCore.ReportingServices.ReportIntermediateFormat.NumericIndicator)base.m_defObject;
			}
		}

		public new NumericIndicatorInstance Instance
		{
			get
			{
				return (NumericIndicatorInstance)this.GetInstance();
			}
		}

		public NumericIndicator(AspNetCore.ReportingServices.ReportIntermediateFormat.NumericIndicator defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
		}

		public override BaseInstance GetInstance()
		{
			if (base.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (base.m_instance == null)
			{
				base.m_instance = new NumericIndicatorInstance(this);
			}
			return base.m_instance;
		}

		public override void SetNewContext()
		{
			base.SetNewContext();
		}
	}
}
