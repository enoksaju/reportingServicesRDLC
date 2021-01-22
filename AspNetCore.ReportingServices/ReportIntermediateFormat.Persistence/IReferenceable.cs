namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	public interface IReferenceable
	{
		int ID
		{
			get;
		}

		ObjectType GetObjectType();
	}
}
