namespace AspNetCore.ReportingServices.RdlObjectModel
{
	internal interface IContainedObject
	{
		IContainedObject Parent
		{
			get;
			set;
		}
	}
}
