using System.Collections.Generic;

namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	public interface IPersistable
	{
		void Serialize(IntermediateFormatWriter writer);

		void Deserialize(IntermediateFormatReader reader);

		void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems);

		ObjectType GetObjectType();
	}
}
