using System;

namespace AspNetCore.ReportingServices.DataProcessing
{
	public interface IDbTransaction : IDisposable
	{
		void Commit();

		void Rollback();
	}
}
