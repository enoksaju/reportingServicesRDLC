using System;

namespace AspNetCore.ReportingServices.DataProcessing
{
	public interface IDataReaderExtension : IDataReader, IDisposable
	{
		bool IsAggregateRow
		{
			get;
		}

		int AggregationFieldCount
		{
			get;
		}

		bool IsAggregationField(int index);
	}
}
