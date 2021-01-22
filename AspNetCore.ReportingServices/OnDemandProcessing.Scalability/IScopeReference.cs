using AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing;
using System.Diagnostics;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public abstract class IScopeReference : Reference<IScope>
	{
		public IScopeReference()
		{
		}

		[DebuggerStepThrough]
		public IScope Value()
		{
			return (IScope)base.InternalValue();
		}
	}
}
