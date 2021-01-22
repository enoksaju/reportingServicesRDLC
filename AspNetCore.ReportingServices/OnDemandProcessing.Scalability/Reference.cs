using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public abstract class Reference<T> : BaseReference, IReference<T>, IReference, IStorable, IPersistable, IDisposable where T : IStorable
	{
		public Reference()
		{
		}

		T IReference<T>.Value()
		{
			return (T)base.InternalValue();
		}
	}
}
