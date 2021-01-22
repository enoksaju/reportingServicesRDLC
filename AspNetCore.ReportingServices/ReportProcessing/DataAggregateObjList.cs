using System.Collections;

namespace AspNetCore.ReportingServices.ReportProcessing
{
	public sealed class DataAggregateObjList : ArrayList
	{
		public new DataAggregateObj this[int index]
		{
			get
			{
				return (DataAggregateObj)base[index];
			}
		}
	}
}
