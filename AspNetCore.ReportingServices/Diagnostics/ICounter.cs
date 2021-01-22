using System;

namespace AspNetCore.ReportingServices.Diagnostics
{
	public interface ICounter : IDisposable
	{
		void Increment();

		void IncrementBy(long val);

		void Decrement();

		void DecrementBy(long val);
	}
}
