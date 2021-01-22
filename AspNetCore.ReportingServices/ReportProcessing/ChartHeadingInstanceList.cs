using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class ChartHeadingInstanceList : ArrayList
	{
		[NonSerialized]
		private ChartHeadingInstance m_lastHeadingInstance;

		public new ChartHeadingInstance this[int index]
		{
			get
			{
				return (ChartHeadingInstance)base[index];
			}
		}

		public ChartHeadingInstanceList()
		{
		}

		public ChartHeadingInstanceList(int capacity)
			: base(capacity)
		{
		}

		public void Add(ChartHeadingInstance chartHeadingInstance, ReportProcessing.ProcessingContext pc)
		{
			if (this.m_lastHeadingInstance != null)
			{
				this.m_lastHeadingInstance.InstanceInfo.HeadingSpan = chartHeadingInstance.InstanceInfo.HeadingCellIndex - this.m_lastHeadingInstance.InstanceInfo.HeadingCellIndex;
				pc.ChunkManager.AddInstance(this.m_lastHeadingInstance.InstanceInfo, this.m_lastHeadingInstance, pc.InPageSection);
			}
			base.Add(chartHeadingInstance);
			this.m_lastHeadingInstance = chartHeadingInstance;
		}

		public void SetLastHeadingSpan(int currentCellIndex, ReportProcessing.ProcessingContext pc)
		{
			if (this.m_lastHeadingInstance != null)
			{
				this.m_lastHeadingInstance.InstanceInfo.HeadingSpan = currentCellIndex - this.m_lastHeadingInstance.InstanceInfo.HeadingCellIndex;
				pc.ChunkManager.AddInstance(this.m_lastHeadingInstance.InstanceInfo, this.m_lastHeadingInstance, pc.InPageSection);
			}
		}
	}
}
