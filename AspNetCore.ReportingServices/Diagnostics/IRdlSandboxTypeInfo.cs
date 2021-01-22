namespace AspNetCore.ReportingServices.Diagnostics
{
	public interface IRdlSandboxTypeInfo
	{
		string Namespace
		{
			get;
		}

		bool AllowNew
		{
			get;
		}

		string Name
		{
			get;
		}
	}
}
