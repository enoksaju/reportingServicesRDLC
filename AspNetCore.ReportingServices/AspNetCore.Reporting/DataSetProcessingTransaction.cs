using AspNetCore.ReportingServices.DataProcessing;
using System;

namespace AspNetCore.Reporting
{
	public class DataSetProcessingTransaction : IDbTransaction, IDisposable
	{
		public void Commit()
		{
		}

		public void Rollback()
		{
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
