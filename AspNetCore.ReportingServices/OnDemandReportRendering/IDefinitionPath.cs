namespace AspNetCore.ReportingServices.OnDemandReportRendering
{
	public interface IDefinitionPath
	{
		string DefinitionPath
		{
			get;
		}

		IDefinitionPath ParentDefinitionPath
		{
			get;
		}
	}
}
