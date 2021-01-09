using System;

namespace AspNetCore.ReportingServices.Diagnostics
{
	internal interface ICounter : IDisposable
	{
		void Increment();

		void IncrementBy(long val);

		void Decrement();

		void DecrementBy(long val);
	}
}
