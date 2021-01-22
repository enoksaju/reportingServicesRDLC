using System;

namespace AspNetCore.ReportingServices.Diagnostics
{
	public interface IDataShapeQueryAbortHelper : IDataShapeAbortHelper, IAbortHelper, IDisposable
	{
		IDataShapeAbortHelper CreateDataShapeAbortHelper();
	}
}
