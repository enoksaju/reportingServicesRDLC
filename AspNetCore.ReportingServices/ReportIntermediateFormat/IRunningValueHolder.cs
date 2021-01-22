using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public interface IRunningValueHolder
	{
		DataScopeInfo DataScopeInfo
		{
			get;
		}

		List<RunningValueInfo> GetRunningValueList();

		void ClearIfEmpty();
	}
}
