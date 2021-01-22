using System;

namespace AspNetCore.ReportingServices.Diagnostics
{
	public interface IDataShapeAbortHelper : IAbortHelper, IDisposable
	{
		event EventHandler ProcessingAbortEvent;

		void ThrowIfAborted(CancelationTrigger cancelationTrigger);
	}
}
