using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class ReferenceIDEqualityComparer : IEqualityComparer<ReferenceID>, IComparer<ReferenceID>
	{
		public static readonly ReferenceIDEqualityComparer Instance = new ReferenceIDEqualityComparer();

		private ReferenceIDEqualityComparer()
		{
		}

		public bool Equals(ReferenceID x, ReferenceID y)
		{
			return x == y;
		}

		public int GetHashCode(ReferenceID obj)
		{
			return (int)obj.Value;
		}

		public int Compare(ReferenceID x, ReferenceID y)
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
}
