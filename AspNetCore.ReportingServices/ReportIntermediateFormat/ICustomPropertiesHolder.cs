namespace AspNetCore.ReportingServices.ReportIntermediateFormat
{
	public interface ICustomPropertiesHolder
	{
		IInstancePath InstancePath
		{
			get;
		}

		DataValueList CustomProperties
		{
			get;
		}
	}
}
