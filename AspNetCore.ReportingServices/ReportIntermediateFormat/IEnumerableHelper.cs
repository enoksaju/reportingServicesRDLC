using System.Collections;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public class IEnumerableHelper
	{
		public static IEnumerable<T> ConvertToTyped<T>(IEnumerable aEnum)
		{
			foreach (object item in aEnum)
			{
				yield return (T)item;
			}
		}
	}
}
