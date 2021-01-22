using System.Collections;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Common
{
	public interface ICustomComparable
	{
		int GetHashCode(IEqualityComparer comparer);

		int CompareTo(ICustomComparable other, IComparer<object> comparer);
	}
}
