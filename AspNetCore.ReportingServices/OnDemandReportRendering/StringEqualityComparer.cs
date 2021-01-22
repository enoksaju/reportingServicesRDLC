using System;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public class StringEqualityComparer : IEqualityComparer<string>
	{
		public static readonly IEqualityComparer<string> Instance = new StringEqualityComparer();

		private StringEqualityComparer()
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
}
