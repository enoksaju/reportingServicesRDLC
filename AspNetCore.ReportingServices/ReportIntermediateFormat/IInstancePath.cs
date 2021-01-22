using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public interface IInstancePath
	{
		List<InstancePathItem> InstancePath
		{
			get;
		}

		IInstancePath ParentInstancePath
		{
			get;
		}

		InstancePathItem InstancePathItem
		{
			get;
		}

		string UniqueName
		{
			get;
		}
	}
}
