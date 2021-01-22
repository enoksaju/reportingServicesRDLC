using AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System.Diagnostics;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class RuntimeHierarchyObjReference : RuntimeDataRegionObjReference, IReference<IHierarchyObj>, IReference<RuntimeHierarchyObj>, IReference, IStorable, IPersistable
	{
		public RuntimeHierarchyObjReference()
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.RuntimeHierarchyObjReference;
		}

		[DebuggerStepThrough]
		IHierarchyObj IReference<IHierarchyObj>.Value()
		{
			return (IHierarchyObj)base.InternalValue();
		}

		[DebuggerStepThrough]
		public new RuntimeHierarchyObj Value()
		{
			return (RuntimeHierarchyObj)base.InternalValue();
		}
	}
}
