using AspNetCore.ReportingServices.OnDemandProcessing.TablixProcessing;
using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public class RuntimeMapDataRegionObjReference : RuntimeChartCriObjReference, IReference<RuntimeMapDataRegionObj>, IReference, IStorable, IPersistable
	{
		public RuntimeMapDataRegionObjReference()
		{
		}

		public override ObjectType GetObjectType()
		{
			return ObjectType.RuntimeMapDataRegionObjReference;
		}

		public new RuntimeMapDataRegionObj Value()
		{
			return (RuntimeMapDataRegionObj)base.InternalValue();
		}
	}
}
