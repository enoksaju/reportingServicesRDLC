using AspNetCore.ReportingServices.ReportIntermediateFormat;
using AspNetCore.ReportingServices.ReportProcessing;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class GaugeLabelCollection : GaugePanelObjectCollectionBase<GaugeLabel>
	{
		private GaugePanel m_gaugePanel;

		public GaugeLabel this[string name]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					AspNetCore.ReportingServices.ReportIntermediateFormat.GaugeLabel gaugeLabel = this.m_gaugePanel.GaugePanelDef.GaugeLabels[i];
					if (string.CompareOrdinal(name, gaugeLabel.Name) == 0)
					{
						return base[i];
					}
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsNotInCollection, name);
			}
		}

		public override int Count
		{
			get
			{
				if (this.m_gaugePanel.GaugePanelDef.GaugeLabels != null)
				{
					return this.m_gaugePanel.GaugePanelDef.GaugeLabels.Count;
				}
				return 0;
			}
		}

		public GaugeLabelCollection(GaugePanel gaugePanel)
		{
			this.m_gaugePanel = gaugePanel;
		}

		protected override GaugeLabel CreateGaugePanelObject(int index)
		{
			return new GaugeLabel(this.m_gaugePanel.GaugePanelDef.GaugeLabels[index], this.m_gaugePanel);
		}
	}
}
