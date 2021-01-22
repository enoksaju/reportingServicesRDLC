using AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence;

namespace AspNetCore.ReportingServices.OnDemandProcessing.Scalability
{
	public interface IStaticReferenceable
	{
		int ID
		{
			get;
		}

		void SetID(int id);

		ObjectType GetObjectType();
	}
}
