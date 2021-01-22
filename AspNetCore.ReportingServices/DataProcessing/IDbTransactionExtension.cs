using System;

namespace AspNetCore.ReportingServices.DataProcessing
{
	public interface IDbTransactionExtension : IDbTransaction, IDisposable
	{
		bool AllowMultiConnection
		{
			get;
		}
	}
}
