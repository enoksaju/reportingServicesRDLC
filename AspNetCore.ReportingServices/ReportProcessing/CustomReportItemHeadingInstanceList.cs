using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class CustomReportItemHeadingInstanceList : ArrayList
	{
		[NonSerialized]
		private CustomReportItemHeadingInstance m_lastHeadingInstance;

		public new CustomReportItemHeadingInstance this[int index]
		{
			get
			{
				return (CustomReportItemHeadingInstance)base[index];
			}
		}

		public CustomReportItemHeadingInstanceList()
		{
		}

		public CustomReportItemHeadingInstanceList(int capacity)
			: base(capacity)
		{
		}

		public void Add(CustomReportItemHeadingInstance headingInstance, ReportProcessing.ProcessingContext pc)
		{
			if (this.m_lastHeadingInstance != null)
			{
				this.m_lastHeadingInstance.HeadingSpan = headingInstance.HeadingCellIndex - this.m_lastHeadingInstance.HeadingCellIndex;
			}
			base.Add(headingInstance);
			this.m_lastHeadingInstance = headingInstance;
		}

		public void SetLastHeadingSpan(int currentCellIndex, ReportProcessing.ProcessingContext pc)
		{
			if (this.m_lastHeadingInstance != null)
			{
				this.m_lastHeadingInstance.HeadingSpan = currentCellIndex - this.m_lastHeadingInstance.HeadingCellIndex;
			}
		}
	}
}
