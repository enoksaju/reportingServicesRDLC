namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	public interface IGloballyReferenceable : IGlobalIDOwner
	{
		ObjectType GetObjectType();
	}
}
