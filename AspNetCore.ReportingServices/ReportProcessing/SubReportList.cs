using AspNetCore.ReportingServices.ReportProcessing.Persistence;
using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	[ArrayOfReferences]
	public sealed class SubReportList : ArrayList
	{
		public new SubReport this[int index]
		{
			get
			{
				return (SubReport)base[index];
			}
		}

		public SubReportList()
		{
		}

		public SubReportList(int capacity)
			: base(capacity)
		{
		}

		public new SubReportList Clone()
		{
			int count = this.Count;
			SubReportList subReportList = new SubReportList(count);
			for (int i = 0; i < count; i++)
			{
				subReportList.Add(this[i]);
			}
			return subReportList;
		}
	}
}
