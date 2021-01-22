using System.Collections;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public class IComparerToGeneric<T> : IComparer<T>
	{
		private IComparer m_comparer;

		public IComparerToGeneric(IComparer comparer)
		{
			this.m_comparer = comparer;
		}

		public int Compare(T x, T y)
		{
			return this.m_comparer.Compare(x, y);
		}
	}
}
