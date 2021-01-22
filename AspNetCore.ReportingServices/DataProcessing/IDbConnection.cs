using AspNetCore.ReportingServices.Interfaces;
using System;

namespace AspNetCore.ReportingServices.DataProcessing
{
	public interface IDbConnection : IDisposable, IExtension
	{
		string ConnectionString
		{
			get;
			set;
		}

		int ConnectionTimeout
		{
			get;
		}

		void Open();

		void Close();

		IDbCommand CreateCommand();

		IDbTransaction BeginTransaction();
	}
}
