namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IReportScopeInstance
	{
		IReportScope ReportScope
		{
			get;
		}

		string UniqueName
		{
			get;
		}

		bool IsNewContext
		{
			get;
			set;
		}
	}
}
