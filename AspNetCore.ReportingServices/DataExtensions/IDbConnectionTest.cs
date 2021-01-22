using AspNetCore.ReportingServices.DataProcessing;
using AspNetCore.ReportingServices.Interfaces;
using System;

namespace AspNetCore.ReportingServices.DataExtensions
{
	public interface IDbConnectionTest : IDbConnection, IDisposable, IExtension
	{
		void TestConnection();
	}
}
