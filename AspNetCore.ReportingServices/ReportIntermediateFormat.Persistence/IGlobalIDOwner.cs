namespace AspNetCore.ReportingServices.ReportIntermediateFormat.Persistence
{
	public interface IGlobalIDOwner
	{
		int GlobalID
		{
			get;
			set;
		}
	}
}
