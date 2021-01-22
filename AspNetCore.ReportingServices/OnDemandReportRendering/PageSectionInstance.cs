using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class PageSectionInstance : ReportElementInstance
	{
		public string UniqueName
		{
			get
			{
				if (base.m_reportElementDef.IsOldSnapshot)
				{
					return this.PageSectionDefinition.PageDefinition.Instance.UniqueName + this.PageSectionDefinition.RenderReportItem.UniqueName;
				}
				string str = this.PageSectionDefinition.IsHeader ? "xH" : "xF";
				AspNetCore.ReportingServices.ReportIntermediateFormat.PageSection pageSection = (AspNetCore.ReportingServices.ReportIntermediateFormat.PageSection)this.PageSectionDefinition.ReportItemDef;
				return InstancePathItem.GenerateUniqueNameString(pageSection.ID, pageSection.InstancePath) + str;
			}
		}

		public PageSection PageSectionDefinition
		{
			get
			{
				return (PageSection)base.m_reportElementDef;
			}
		}

		public PageSectionInstance(PageSection pageSectionDef)
			: base(pageSectionDef)
		{
		}
	}
}
