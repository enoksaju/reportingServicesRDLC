using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class TopImageInstance : BaseGaugeImageInstance
	{
		private ReportColor m_hueColor;

		public ReportColor HueColor
		{
			get
			{
				if (this.m_hueColor == null)
				{
					this.m_hueColor = new ReportColor(((AspNetCore.ReportingServices.ReportIntermediateFormat.TopImage)base.m_defObject.BaseGaugeImageDef).EvaluateHueColor(this.ReportScopeInstance, base.m_defObject.GaugePanelDef.RenderingContext.OdpContext), true);
				}
				return this.m_hueColor;
			}
		}

		public TopImageInstance(TopImage defObject)
			: base(defObject)
		{
		}

		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_hueColor = null;
		}
	}
}
