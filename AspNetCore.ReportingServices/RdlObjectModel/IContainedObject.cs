namespace AspNetCore.ReportingServices.RdlObjectModel
{
	public interface IContainedObject
	{
		IContainedObject Parent
		{
			get;
			set;
		}
	}
}
