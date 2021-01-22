using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class DataAggregateObjResultsList : ArrayList
	{
		public new DataAggregateObjResult[] this[int index]
		{
			get
			{
				return (DataAggregateObjResult[])base[index];
			}
		}
	}
}
