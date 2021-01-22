using AspNetCore.ReportingServices.ReportIntermediateFormat;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public sealed class ReportSectionInstance : BaseInstance, IReportScopeInstance
	{
		private bool m_isNewContext;

		public ReportSection SectionDef
		{
			get
			{
				return (ReportSection)base.m_reportScope;
			}
		}

		IReportScope IReportScopeInstance.ReportScope
		{
			get
			{
				return base.m_reportScope;
			}
		}

		public string UniqueName
		{
			get
			{
				if (this.SectionDef.IsOldSnapshot)
				{
					return this.SectionDef.Report.RenderReport.UniqueName + "xE";
				}
				AspNetCore.ReportingServices.ReportIntermediateFormat.ReportSection sectionDef = this.SectionDef.SectionDef;
				return InstancePathItem.GenerateUniqueNameString(sectionDef.ID, sectionDef.InstancePath);
			}
		}

		bool IReportScopeInstance.IsNewContext
		{
			get
			{
				return this.m_isNewContext;
			}
			set
			{
				this.m_isNewContext = value;
			}
		}

		public ReportSectionInstance(ReportSection sectionDef)
			: base(sectionDef)
		{
		}

		protected override void ResetInstanceCache()
		{
		}

		public override void SetNewContext()
		{
			this.m_isNewContext = true;
			base.SetNewContext();
		}
	}
}
