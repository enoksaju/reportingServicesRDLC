using System.Collections.Generic;

namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public interface IHierarchyMember
	{
		Group Group
		{
			get;
			set;
		}

		IList<SortExpression> SortExpressions
		{
			get;
			set;
		}

		IEnumerable<IHierarchyMember> Members
		{
			get;
		}
	}
}
