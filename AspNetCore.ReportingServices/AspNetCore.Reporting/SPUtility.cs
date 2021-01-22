using System.Text.RegularExpressions;

namespace AspNetCore.Reporting
{
	public static class SPUtility
	{
		public static bool IsValidStringInput(string regexp, string newValue)
		{
			Regex regex = new Regex(regexp);
			if (!string.IsNullOrEmpty(newValue) && regex.IsMatch(newValue))
			{
				return true;
			}
			return false;
		}
	}
}
