using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;
using System.Collections.Generic;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public interface IScalabilityObjectCreator
	{
		bool TryCreateObject(ObjectType objectType, out IPersistable newObject);

		List<Declaration> GetDeclarations();
	}
}
