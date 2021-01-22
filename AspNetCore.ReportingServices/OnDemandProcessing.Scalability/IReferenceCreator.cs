using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public interface IReferenceCreator
	{
		bool TryCreateReference(IStorable refTarget, out BaseReference newReference);

		bool TryCreateReference(ObjectType referenceObjectType, out BaseReference newReference);
	}
}
