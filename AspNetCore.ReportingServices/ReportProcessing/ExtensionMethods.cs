using System.Globalization;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	internal static class ExtensionMethods
	{
		public static bool HasFlag(this CompareOptions value, CompareOptions flag)
		{
			return (value & flag) == flag;
		}

		public static bool IsNullOrWhiteSpace(this string str)
		{
			if (str != null && str.Length > 0)
			{
				for (int i = 0; i < str.Length; i++)
				{
					if (!char.IsWhiteSpace(str[i]))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}
	}
}
