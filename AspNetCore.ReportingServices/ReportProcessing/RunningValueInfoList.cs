using System;
using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	[Serializable]
	public sealed class RunningValueInfoList : ArrayList
	{
		public new RunningValueInfo this[int index]
		{
			get
			{
				return (RunningValueInfo)base[index];
			}
		}

		public RunningValueInfoList()
		{
		}

		public RunningValueInfoList(int capacity)
			: base(capacity)
		{
		}
	}
}
