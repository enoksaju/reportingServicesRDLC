using AspNetCore.ReportingServices.Rendering.RPLProcessing;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Rendering.ExcelRenderer.SPBIFReader.Callbacks
{
	public abstract class ALayout
	{
		public static readonly int TablixStructGenerationOffset = 100000;

		public static readonly int TablixStructStart = -2147483648;

		protected RPLReport m_report;

		public abstract bool HeaderInBody
		{
			get;
		}

		public abstract bool FooterInBody
		{
			get;
		}

		public abstract bool? SummaryRowAfter
		{
			get;
			set;
		}

		public abstract bool? SummaryColumnAfter
		{
			get;
			set;
		}

		public RPLReport RPLReport
		{
			get
			{
				return this.m_report;
			}
		}

		public ALayout(RPLReport report)
		{
			this.m_report = report;
		}

		public abstract void AddReportItem(object rplSource, int top, int left, int width, int height, int generationIndex, byte state, string subreportLanguage, Dictionary<string, ToggleParent> toggleParents);

		public abstract void AddStructuralItem(int top, int left, int width, int height, bool isToggglable, int generationIndex, RPLTablixMemberCell member, TogglePosition togglePosition);

		public abstract void AddStructuralItem(int top, int left, int width, int height, int generationIndex, int rowHeaderWidth, int columnHeaderHeight, bool rtl);

		public abstract ALayout GetPageHeaderLayout(float width, float height);

		public abstract ALayout GetPageFooterLayout(float width, float height);

		public abstract void CompletePage();

		public abstract void CompleteSection();

		public abstract void SetIsLastSection(bool isLastSection);
	}
}
