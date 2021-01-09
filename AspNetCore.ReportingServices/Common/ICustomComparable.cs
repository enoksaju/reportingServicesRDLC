using System.Collections;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Common
{
	internal interface ICustomComparable
	{
		int GetHashCode(IEqualityComparer comparer);

		int CompareTo(ICustomComparable other, IComparer<object> comparer);
	}
}
