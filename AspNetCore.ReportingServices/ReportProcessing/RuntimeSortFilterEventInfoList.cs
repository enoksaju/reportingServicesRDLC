using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class RuntimeSortFilterEventInfoList : ArrayList
	{
		public new RuntimeSortFilterEventInfo this[int index]
		{
			get
			{
				return (RuntimeSortFilterEventInfo)base[index];
			}
		}

		public RuntimeSortFilterEventInfoList()
		{
		}
	}
}
