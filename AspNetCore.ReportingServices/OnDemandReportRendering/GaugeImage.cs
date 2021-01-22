using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class GaugeImage : GaugePanelItem
	{
		public AspNetCore.ReportingServices.ReportIntermediateFormat.GaugeImage GaugeImageDef
		{
			get
			{
				return (AspNetCore.ReportingServices.ReportIntermediateFormat.GaugeImage)base.m_defObject;
			}
		}

		public new GaugeImageInstance Instance
		{
			get
			{
				return (GaugeImageInstance)this.GetInstance();
			}
		}

		public GaugeImage(AspNetCore.ReportingServices.ReportIntermediateFormat.GaugeImage defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
			base.m_defObject = defObject;
			base.m_gaugePanel = gaugePanel;
		}

		public override BaseInstance GetInstance()
		{
			if (base.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (base.m_instance == null)
			{
				base.m_instance = new GaugeImageInstance(this);
			}
			return base.m_instance;
		}

		public override void SetNewContext()
		{
			base.SetNewContext();
			if (base.m_instance != null)
			{
				base.m_instance.SetNewContext();
			}
		}
	}
}
