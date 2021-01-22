using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class CustomReportItemInstance : ReportItemInstance
	{
		public bool NoRows
		{
			get
			{
				if (base.m_reportElementDef.IsOldSnapshot)
				{
					return ((CustomReportItem)base.m_reportElementDef).RenderCri.CustomData.NoRows;
				}
				base.m_reportElementDef.RenderingContext.OdpContext.SetupContext(base.m_reportElementDef.ReportItemDef, this.ReportScopeInstance);
				return ((AspNetCore.ReportingServices.ReportIntermediateFormat.DataRegion)base.m_reportElementDef.ReportItemDef).NoRows;
			}
		}

		public CustomReportItemInstance(CustomReportItem reportItemDef)
			: base(reportItemDef)
		{
		}
	}
}
