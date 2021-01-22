using System;
using System.Collections;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public interface IDecumulator<T> : IEnumerator<T>, IDisposable, IEnumerator
	{
		void RemoveCurrent();
	}
}
