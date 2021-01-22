using System.Collections.Generic;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public interface IHierarchy
	{
		IEnumerable<IHierarchyMember> Members
		{
			get;
		}
	}
}
