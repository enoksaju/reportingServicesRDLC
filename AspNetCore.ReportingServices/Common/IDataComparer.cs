using System.Collections;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.Common
{
	public interface IDataComparer : IEqualityComparer, IEqualityComparer<object>, IComparer, IComparer<object>
	{
		int Compare(object x, object y, bool extendedTypeComparisons);

		int Compare(object x, object y, bool throwExceptionOnComparisonFailure, bool extendedTypeComparisons, out bool validComparisonResult);
	}
}
