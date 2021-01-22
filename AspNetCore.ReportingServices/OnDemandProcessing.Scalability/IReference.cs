using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public interface IReference : IStorable, IPersistable
	{
		ReferenceID Id
		{
			get;
		}

		IDisposable PinValue();

		void UnPinValue();

		void Free();

		void UpdateSize(int sizeDeltaBytes);

		IReference TransferTo(IScalabilityCache scaleCache);
	}
	public interface IReference<T> : IReference, IStorable, IPersistable
	{
		T Value();
	}
}
