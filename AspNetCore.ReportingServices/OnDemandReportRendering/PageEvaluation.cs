namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public abstract class PageEvaluation
	{
		protected int m_currentPageNumber = 1;

		protected int m_totalPages = 1;

		protected int m_overallTotalPages = 1;

		protected int m_currentOverallPageNumber = 1;

		protected Report m_romReport;

		protected string m_pageName;

		protected PageEvaluation(Report report)
		{
			this.m_romReport = report;
		}

		public virtual void Reset(ReportSection section, int newPageNumber, int newTotalPages, int newOverallPageNumber, int newOverallTotalPages)
		{
			this.m_currentPageNumber = newPageNumber;
			this.m_totalPages = newTotalPages;
			this.m_currentOverallPageNumber = newOverallPageNumber;
			this.m_overallTotalPages = newOverallTotalPages;
		}

		public virtual void SetPageName(string pageName)
		{
			this.m_pageName = pageName;
		}

		public abstract void Add(string textboxName, object textboxValue);

		public abstract void UpdatePageSections(ReportSection section);
	}
}
