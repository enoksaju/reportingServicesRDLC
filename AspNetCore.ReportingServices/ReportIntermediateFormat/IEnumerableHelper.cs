using System.Collections;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	internal class IEnumerableHelper
	{
		internal static IEnumerable<T> ConvertToTyped<T>(IEnumerable aEnum)
		{
			foreach (object item in aEnum)
			{
				yield return (T)item;
			}
		}
	}
}
