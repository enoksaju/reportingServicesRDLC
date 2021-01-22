using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	public class EqualityComparers
	{
		private class ObjectTypeEqualityComparer : IEqualityComparer<ObjectType>
		{
			public ObjectTypeEqualityComparer()
			{
			}

			public bool Equals(ObjectType x, ObjectType y)
			{
				return x == y;
			}

			public int GetHashCode(ObjectType obj)
			{
				return (int)obj;
			}
		}

		private class StringEqualityComparer : IEqualityComparer<string>
		{
			public StringEqualityComparer()
			{
			}

			public bool Equals(string str1, string str2)
			{
				return string.Equals(str1, str2, StringComparison.Ordinal);
			}

			public int GetHashCode(string str)
			{
				return str.GetHashCode();
			}
		}

		public class Int32EqualityComparer : IEqualityComparer<int>, IComparer<int>
		{
			public Int32EqualityComparer()
			{
			}

			public bool Equals(int x, int y)
			{
				return x == y;
			}

			public int GetHashCode(int obj)
			{
				return obj;
			}

			public int Compare(int x, int y)
			{
				return x - y;
			}
		}

		public class ReversedInt32EqualityComparer : IEqualityComparer<int>, IComparer<int>
		{
			public ReversedInt32EqualityComparer()
			{
			}

			public bool Equals(int x, int y)
			{
				return x == y;
			}

			public int GetHashCode(int obj)
			{
				return obj;
			}

			public int Compare(int x, int y)
			{
				return y - x;
			}
		}

		public class Int64EqualityComparer : IEqualityComparer<long>, IComparer<long>
		{
			public Int64EqualityComparer()
			{
			}

			public bool Equals(long x, long y)
			{
				return x == y;
			}

			public int GetHashCode(long obj)
			{
				return (int)obj;
			}

			public int Compare(long x, long y)
			{
				if (x < y)
				{
					return -1;
				}
				if (x > y)
				{
					return 1;
				}
				return 0;
			}
		}

		public static readonly IEqualityComparer<ObjectType> ObjectTypeComparerInstance = new ObjectTypeEqualityComparer();

		public static readonly Int32EqualityComparer Int32ComparerInstance = new Int32EqualityComparer();

		public static readonly ReversedInt32EqualityComparer ReversedInt32ComparerInstance = new ReversedInt32EqualityComparer();

		public static readonly Int64EqualityComparer Int64ComparerInstance = new Int64EqualityComparer();

		public static readonly IEqualityComparer<string> StringComparerInstance = new StringEqualityComparer();
	}
}
