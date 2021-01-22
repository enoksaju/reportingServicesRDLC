using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class FiltersList : ArrayList
	{
		public new Filters this[int index]
		{
			get
			{
				return (Filters)base[index];
			}
		}
	}
}
